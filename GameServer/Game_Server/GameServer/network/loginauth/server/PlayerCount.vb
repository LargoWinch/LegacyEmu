Friend Class PlayerCount
    Inherits GameServerPacket
    Private cnt As Short
    Public Sub New(ByVal cnt As Short)
        Me.cnt = cnt
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&HA3)
        writeH(cnt)
    End Sub
End Class
