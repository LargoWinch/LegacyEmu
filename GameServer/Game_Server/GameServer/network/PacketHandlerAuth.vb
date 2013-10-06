Imports System.Threading

Public Class PacketHandlerAuth
    Public Shared Sub handlePacket(ByVal login As AuthThread, ByVal buff() As Byte)
        Dim id As Byte = buff(0)
        Console.WriteLine("login>gameserver: " & id)
        Dim cninfo As String = "handlepacket: request " & id.ToString("x2") & " size " & buff.Length

        Dim msg As ReceiveAuthPacket = Nothing
        Select Case id
            Case &HA1
                msg = New LoginServPingResponse(login, buff)
            Case &HA5
                msg = New LoginServLoginFail(login, buff)
            Case &HA6
                msg = New LoginServLoginOk(login, buff)
            Case &HA7
                msg = New LoginServAcceptPlayer(login, buff)
            Case &HA8
                msg = New LoginServKickAccount(login, buff)
        End Select

        If msg Is Nothing Then
            Console.WriteLine(cninfo)
            Return

        End If

        CType(New Thread(New ThreadStart(AddressOf msg.run)), Thread).Start()
    End Sub
End Class
