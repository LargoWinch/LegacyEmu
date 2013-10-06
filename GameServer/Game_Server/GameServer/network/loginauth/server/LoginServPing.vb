Friend Class LoginServPing
    Inherits GameServerPacket
    Public version As String
    Private build As Integer
    Public Sub New(ByVal th As AuthThread)
        Me.version = th.version
        Me.build = th.build
    End Sub
    Protected Friend Overrides Sub write()
        writeC(&HA0)
        writeS(version)
        writeD(build)
    End Sub
End Class
