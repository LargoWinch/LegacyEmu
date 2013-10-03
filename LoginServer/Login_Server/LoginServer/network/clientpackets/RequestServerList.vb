'03:10:49
'03.10.2013
'Autor: LargoWinch

Friend Class RequestServerList
    Inherits L_ReceiveBasePacket
    Public Sub New(ByVal Client As L_LoginClient, ByVal data() As Byte)
        MyBase.makeme(Client, data)
    End Sub

    Private login1, login2 As Integer
    Public Overrides Sub read()
        login1 = readD()
        login2 = readD()
    End Sub

    Public Overrides Sub run()
        If getClient().login1 <> login1 AndAlso getClient().login2 <> login2 Then
            getClient().sendPacket(New SM_LOGIN_FAIL(getClient(), SM_LOGIN_FAIL.LoginFailReason.ACCESS_FAILED_TRY_AGAIN))
            Return
        End If

        getClient().sendPacket(New ServerList(getClient()))
    End Sub
End Class
