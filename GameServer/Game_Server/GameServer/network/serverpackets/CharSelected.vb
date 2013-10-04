Public Class CharSelected
    Inherits GameServerPacket

    ' SdSddddddddddffddddddddddddddddddddddddddddddddddddddddd d

    Private session As Integer
    Private cha As L2Player

    Public Sub New(ByVal cha As L2Player, ByVal session As Integer)
        Me.cha = cha
        Me.session = session
    End Sub


    Protected Friend Overrides Sub write()

    End Sub
End Class
