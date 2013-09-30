'21:30:22
'30.09.2013
'Autor: LargoWinch

Public Class AuthGameGuard
    Inherits L_ReceiveBasePacket
    Public Sub New(ByVal Client As L_LoginClient, ByVal data() As Byte)
        MyBase.makeme(Client, data)
    End Sub

    Public Overrides Sub read()
        ' do nothing
    End Sub

    Public Overrides Sub run()
    End Sub
End Class
