'05:32:56
'03.10.2013
'Autor: LargoWinch

Friend Class RequestPlayerInGame
    Inherits L_ReceiveServerPacket
    Private account As String
    Private status As Byte
    Public Sub New(ByVal server As ServerThread, ByVal data() As Byte)
        MyBase.makeme(server, data)
    End Sub

    Public Overrides Sub read()
        account = readS()
        status = readC()
    End Sub

    Public Overrides Sub run()
        thread.AccountInGame(account, status)
    End Sub
End Class
