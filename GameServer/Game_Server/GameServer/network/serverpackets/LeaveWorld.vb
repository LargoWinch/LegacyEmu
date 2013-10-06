Friend Class LeaveWorld
    Inherits GameServerPacket
    Protected Friend Overrides Sub write()
        writeC(&H84)
    End Sub
End Class
