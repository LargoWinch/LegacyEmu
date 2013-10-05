Friend Class EnterWorld
    Inherits GameServerRequest
    Public Sub New(ByVal client As GameClient, ByVal data() As Byte)
        MyBase.makeme(client, data)
    End Sub

    Private tracert(4)() As Integer

    Public Overrides Sub read()
        readB(32)
        readD()
        readD()
        readD()
        readD()
        readB(32)
        readD()

        For i As Integer = 0 To 4
            tracert(i) = New Integer(3) {}
            For o As Integer = 0 To 3
                tracert(i)(o) = readC()
            Next o
        Next i
    End Sub

    Public Overrides Sub run()
       
    End Sub
End Class
