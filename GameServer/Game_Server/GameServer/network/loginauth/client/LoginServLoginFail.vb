﻿Friend Class LoginServLoginFail
    Inherits ReceiveAuthPacket
    Private code As String
    Public Sub New(ByVal login As AuthThread, ByVal db() As Byte)
        MyBase.makeme(login, db)
    End Sub

    Public Overrides Sub read()
        code = readS()
    End Sub

    Public Overrides Sub run()
        login.loginFail(code)
    End Sub
End Class
