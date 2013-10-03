'05:10:41
'03.10.2013
'Autor: LargoWinch

Imports System.Threading

Friend Class RequestLoginAuth
    Inherits L_ReceiveServerPacket
    Private port As Short
    Private host As String
    Private info As String
    Private code As String
    Private curp As Integer
    Private maxp As Short
    Private gmonly As Byte
    Private test As Byte

    Public Sub New(ByVal server As ServerThread, ByVal data() As Byte)
        MyBase.makeme(server, data)
    End Sub

    Public Overrides Sub read()
        port = readH()
        host = readS()
        info = readS()
        code = readS()
        curp = readD()
        maxp = readH()
        gmonly = readC()
        test = readC()
    End Sub

    Public Overrides Sub run()
        Dim server As L_L2Server = Nothing
        For Each srv As L_L2Server In ServerThreadPool.getInstance().servers
            If srv.code = code Then
                srv.thread = Thread
                server = srv
                Exit For
            End If
        Next srv

        If server Is Nothing Then
            Logger.error("code '" & code & "' for server was not found. closing")
            Thread.close(New LoginServerFail("wrong code"))
            Return
        End If

        Thread.id = server.id
        Thread.info = info
        Thread.wan = host
        Thread.port = port
        Thread.maxp = maxp
        Thread.gmonly = gmonly = 1
        Thread.testMode = test = 1
        Thread.connected = True

        Thread.sendPacket(New ServerLoginOk())
        Logger.extra_info("AuthThread: Server #" & server.id & " connected")
    End Sub
End Class

