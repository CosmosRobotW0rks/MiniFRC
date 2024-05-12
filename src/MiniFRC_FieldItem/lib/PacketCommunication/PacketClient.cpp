#include <Arduino.h>
#include "AsyncTCP.h"
#include "PacketClient.h"

bool PacketClient::Connect(IPAddress IP, uint16_t port, uint32_t timeoutMS)
{
    Serial.println(IP.toString());
    Serial.println(port);

    if (client == nullptr)
    {
        client = new AsyncClient();

        client->onData(std::bind(&PacketClient::DataReceived, this, std::placeholders::_1, std::placeholders::_2, std::placeholders::_3, std::placeholders::_4));

        packetBuffer = (uint8_t *)malloc(1024);
    }

    if (client->connected())
        client->stop();
    
    client->connect(IP, port);
    Serial.println("Sent cli connect");

    uint64_t start = millis();

    delay(100);


    while (!client->connected() || millis() - start < timeoutMS)
    {
        delay(100);
    }

    return client->connected();
}

void PacketClient::Disconnect()
{
    client->stop();
}

bool PacketClient::SendPacket(uint8_t packetID, void *packet, uint len)
{
    uint packetLen = len + 1;

    uint8_t temp[packetLen];

    temp[0] = packetID;
    memcpy(&temp[1], packet, len);

    return client->write((char *)temp, packetLen) == packetLen;
}

bool PacketClient::RegisterPacket(uint8_t packetId, uint16_t packetSize, PacketCallback callback)
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

inline uint8_t PacketClient::GetPacketIndexByID(uint8_t id)
{
    for (uint8_t i = 0; i < packetCount; i++)
    {
        if (packetIDs[i] == id)
            return i;
    }

    return 255;
}

inline void PacketClient::HandlePacket(uint8_t packetid, uint8_t *packetBuf, size_t len)
{
    uint8_t packetIndex = GetPacketIndexByID(packetid);
    if (packetIndex == 255)
        return; // PACKET NOT FOUND

    ulong time = millis() - temp;
    Serial.printf("Time took: %d\n", time);

    packetCallbacks[packetIndex](packetBuf, len);
}

void PacketClient::DataReceived(void *idk, AsyncClient *__client, void *data, size_t len)
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