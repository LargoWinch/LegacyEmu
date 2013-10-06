Friend Class LoginServPingResponse
    Inherits ReceiveAuthPacket
    Private message As String
    Public Sub New(ByVal login As AuthThread, ByVal db() As Byte)
        MyBase.makeme(login, db)
    End Sub

    Public Overrides Sub read()
        message = readS()
    End Sub

    Public Overrides Sub run()

    End Sub
End Class
