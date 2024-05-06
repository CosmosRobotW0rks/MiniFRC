#pragma once



namespace PacketClient
{

    typedef void (*PacketCallback)(uint8_t *packetBuf, size_t len);

    bool Connect(IPAddress IP, uint16_t port, uint32_t timeoutMS);
    void Disconnect();

    bool SendPacket(uint8_t packetID, void*packet, uint len);
    bool RegisterPacket(uint8_t packetId, uint16_t packetSize, PacketCallback callback);

}