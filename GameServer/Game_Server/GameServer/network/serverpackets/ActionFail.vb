Friend Class ActionFail
    Inherits GameServerPacket
    Protected Friend Overrides Sub write()
        writeC(&H1F)
    End Sub
End Class
