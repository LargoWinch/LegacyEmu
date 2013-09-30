Friend Class LoginServerPing
    Inherits L_SendServerPacket

    Protected Friend Overrides Sub write()
        writeC(&HA1)
        writeS("это я, я жив, спасибо, что спросили")
    End Sub
End Class
