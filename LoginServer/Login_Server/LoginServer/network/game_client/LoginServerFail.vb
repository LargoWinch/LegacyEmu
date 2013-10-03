'19:12:36
'30.09.2013
'Autor: LargoWinch

Friend Class LoginServerFail
    Inherits L_SendServerPacket
    Private code As String
    Public Sub New(ByVal code As String)
        Me.code = code
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&HA5)
        writeS(code)
    End Sub
End Class
