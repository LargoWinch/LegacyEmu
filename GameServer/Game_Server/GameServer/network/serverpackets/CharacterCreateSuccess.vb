Friend Class CharacterCreateSuccess
    Inherits GameServerPacket
    Protected Friend Overrides Sub write()
        writeC(&HF)
        writeD(&H1)
    End Sub
End Class
