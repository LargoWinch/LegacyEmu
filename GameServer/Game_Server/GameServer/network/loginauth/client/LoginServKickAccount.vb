Friend Class LoginServKickAccount
    Inherits ReceiveAuthPacket
    Private account As String
    Public Sub New(ByVal login As AuthThread, ByVal db() As Byte)
        MyBase.makeme(login, db)
    End Sub

    Public Overrides Sub read()
        account = readS()
    End Sub

    Public Overrides Sub run()
        World.getInstance().KickAccount(account)
    End Sub
End Class
