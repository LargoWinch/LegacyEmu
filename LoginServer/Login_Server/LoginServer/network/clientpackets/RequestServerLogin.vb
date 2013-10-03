'03:43:56
'03.10.2013
'Autor: LargoWinch

Friend Class RequestServerLogin
    Inherits L_ReceiveBasePacket
    Public Sub New(ByVal Client As L_LoginClient, ByVal data() As Byte)
        MyBase.makeme(Client, data)
    End Sub

    Private login1, login2 As Integer
    Private serverId As Byte
    Public Overrides Sub read()
        login1 = readD()
        login2 = readD()
        serverId = readC()
    End Sub

    Public Overrides Sub run()
        If getClient().login1 <> login1 AndAlso getClient().login2 <> login2 Then
            getClient().sendPacket(New SM_LOGIN_FAIL(getClient(), SM_LOGIN_FAIL.LoginFailReason.SYSTEM_ERROR))
            Return
        End If

        Dim server As L_L2Server = ServerThreadPool.getInstance().get(serverId)
        If server Is Nothing Then
            getClient().sendPacket(New SM_LOGIN_FAIL(getClient(), SM_LOGIN_FAIL.LoginFailReason.SYSTEM_ERROR))
            Return
        End If

        If server.connected = 0 Then
            getClient().sendPacket(New SM_LOGIN_FAIL(getClient(), SM_LOGIN_FAIL.LoginFailReason.SERVER_MAINTENANCE))
        Else
            If server.gmonly AndAlso getClient().activeAccount.Builder = 0 Then
                getClient().sendPacket(New SM_LOGIN_FAIL(getClient(), SM_LOGIN_FAIL.LoginFailReason.NO_ACCESS_COUPON))
                Return
            End If

            ServerThreadPool.getInstance().SendPlayer(serverId, getClient(), Date.Now.ToLocalTime().ToString())

            Dim sqb As New Sql_Block("accounts")
            sqb.param("serverId", serverId)
            sqb.param("lastlogin", Date.Now.ToLocalTime())
            sqb.param("lastAddress", getClient()._address)
            sqb.where("account", getClient().activeAccount.Name)
            sqb.sql_update()

            getClient().sendPacket(New PlayOk(getClient()))


        End If
    End Sub
End Class

