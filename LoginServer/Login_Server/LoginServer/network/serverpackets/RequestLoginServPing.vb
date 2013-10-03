'05:23:55
'03.10.2013
'Autor: LargoWinch


Friend Class RequestLoginServPing
    Inherits L_ReceiveServerPacket
    Private message As String
    Public Sub New(ByVal server As ServerThread, ByVal data() As Byte)
        MyBase.makeme(server, data)
    End Sub

    Public Overrides Sub read()
        message = readS()
    End Sub

    Public Overrides Sub run()
        thread.sendPacket(New LoginServerPing())
    End Sub
End Class
