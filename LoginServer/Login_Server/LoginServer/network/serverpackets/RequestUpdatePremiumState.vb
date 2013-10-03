'06:22:59
'03.10.2013
'Autor: LargoWinch

Friend Class RequestUpdatePremiumState
    Inherits L_ReceiveServerPacket
    Private account As String
    Private status As Byte
    Private points As Long
    Public Sub New(ByVal server As ServerThread, ByVal data() As Byte)
        MyBase.makeme(server, data)
    End Sub

    Public Overrides Sub read()
        account = readS()
        status = readC()
        points = readQ()
    End Sub

    Public Overrides Sub run()
        AccountManager.getInstance().UpdatePremium(account, status, points)
    End Sub
End Class
