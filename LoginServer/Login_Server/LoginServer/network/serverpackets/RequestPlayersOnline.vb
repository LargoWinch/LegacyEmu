'06:02:00
'03.10.2013
'Autor: LargoWinch

Friend Class RequestPlayersOnline
    Inherits L_ReceiveServerPacket
    Private cnt As Short
    Public Sub New(ByVal server As ServerThread, ByVal data() As Byte)
        MyBase.makeme(server, data)
    End Sub

    Public Overrides Sub read()
        cnt = readH()
    End Sub

    Public Overrides Sub run()
        thread.curp = cnt
    End Sub
End Class