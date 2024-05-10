#pragma once
#include <Arduino.h>
#include "AsyncTCP.h"

typedef void (*PacketCallback)(uint8_t *packetBuf, size_t len);

class PacketClient
{
private:    
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

    inline uint8_t GetPacketIndexByID(uint8_t id);
    inline void HandlePacket(uint8_t packetid, uint8_t *packetBuf, size_t len);
public:
    PacketClient(/* args */);
    ~PacketClient();

    void DataReceived(void *idk, AsyncClient *__client, void *data, size_t len);
    bool Connect(IPAddress IP, uint16_t port, uint32_t timeoutMS);
    void Disconnect();
    bool SendPacket(uint8_t packetID, void*packet, uint len);
    bool RegisterPacket(uint8_t packetId, uint16_t packetSize, PacketCallback callback);
};