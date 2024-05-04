#pragma once



namespace PacketClient
{

    typedef void (*PacketCallback)(uint8_t *packetBuf, size_t len);

    bool Connect(char *host, uint16_t port);
    void Disconnect();

    bool SendPacket(uint8_t packetID, uint8_t *packet, uint len);
    bool RegisterPacket(uint8_t packetId, uint16_t packetSize, PacketCallback callback);

}