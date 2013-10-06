Friend Class LoginAuth
    Inherits GameServerPacket
    Protected Friend Overrides Sub write()
        writeC(&HA1)
        writeH(Config.SERVER_PORT)
        writeS(Config.SERVER_HOST)
        writeS("")
        writeS(Config.auth_code)
        writeD(0)
        writeH(Config.max_players)
        writeC(Config.gmonly)
        writeC(Config.test)
    End Sub
End Class
