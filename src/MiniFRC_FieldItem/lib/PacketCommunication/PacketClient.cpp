#include <Arduino.h>
#include "AsyncTCP.h"
#include "PacketClient.h"

namespace PacketClient
{
    AsyncClient *client = nullptr;

    uint8_t packetIDs[254];
    uint16_t packetSizes[254];
    PacketCallback packetCallbacks[254];
    uint8_t packetCount = 0;

    uint8_t *packetBuffer = nullptr;
    int packetBufferLen = 0;
    uint8_t packetID = 255;
    uint16_t packetSize = 0;

    ulong temp = 0;

    void DataReceived(void *idk, AsyncClient *__client, void *data, size_t len);

    bool Connect(IPAddress IP, uint16_t port, uint32_t timeoutMS)
    {
        if (client == nullptr)
        {
            client = new AsyncClient();
            client->onData(DataReceived, nullptr);
            packetBuffer = (uint8_t *)malloc(1024);
        }
        
        if (client->connected()) client->stop();
        client->connect(IP, port);
        while (!client->connected() || timeoutMS <= 0)
        {
            delay(100);
            timeoutMS -= 100;
        }

        return client->connected();
    }

    void Disconnect()
    {
        client->stop();
    }

    bool SendPacket(uint8_t packetID, void *packet, uint len)
    {
        uint packetLen = len + 1;

        uint8_t temp[packetLen];

        temp[0] = packetID;
        memcpy(&temp[1], packet, len);

        return client->write((char *)temp, packetLen) == packetLen;
    }

    bool RegisterPacket(uint8_t packetId, uint16_t packetSize, PacketCallback callback)
    {
        if (packetId == 255)
            return false; // Special ID
        if (packetCount == 254)
            return false; // To avoid overflow

        packetIDs[packetCount] = packetId;
        packetSizes[packetCount] = packetSize;
        packetCallbacks[packetCount] = callback;
        packetCount++;

        return true;
    }

    inline uint8_t GetPacketIndexByID(uint8_t id)
    {
        for (uint8_t i = 0; i < packetCount; i++)
        {
            if (packetIDs[i] == id)
                return i;
        }

        return 255;
    }

    inline void HandlePacket(uint8_t packetid, uint8_t *packetBuf, size_t len)
    {
        uint8_t packetIndex = GetPacketIndexByID(packetid);
        if (packetIndex == 255)
            return; // PACKET NOT FOUND

        ulong time = millis() - temp;
        Serial.printf("Time took: %d\n", time);

        packetCallbacks[packetIndex](packetBuf, len);
    }

    void DataReceived(void *idk, AsyncClient *__client, void *data, size_t len)
    {
        temp = millis();
        Serial.printf("Received Data Len: %d\n", len);

        if (len == 0)
            return;

        uint8_t *tcpbuffer = (uint8_t *)data;

        uint16_t handledByteLen = 0;

        while (handledByteLen < len)
        {
            if (packetID == 255)
            {
                packetID = tcpbuffer[handledByteLen];
                uint8_t packetIndex = GetPacketIndexByID(packetID);

                if (packetIndex == 255)
                {
                    packetID = 255;
                    return;
                }

                packetSize = packetSizes[packetIndex] + 1;
                packetBufferLen = 0;

                Serial.printf("PacketID: %d / PacketIndex: %d / PacketSize: %d\n", packetID, packetIndex, packetSize);
            }

            uint16_t remainingPacketBytes = packetSize - packetBufferLen;
            uint16_t remainingTcpBytes = len - handledByteLen;

            uint16_t bytesToCopy = remainingTcpBytes < remainingPacketBytes ? remainingTcpBytes : remainingPacketBytes;
            memcpy(&packetBuffer[packetBufferLen], &tcpbuffer[handledByteLen], bytesToCopy);

            handledByteLen += bytesToCopy;
            packetBufferLen += bytesToCopy;

            if (packetBufferLen == packetSize)
            {
                HandlePacket(packetID, &packetBuffer[1], packetSize - 1);

                packetID = 255;
                packetSize = 0;
                packetBufferLen = 0;
            }
        }
    }

}