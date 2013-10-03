'20:55:10
'03.10.2013
'Autor: LargoWinch

'packet type id 0x0E
'format:	cdbd

Friend Class ProtocolVersion
    Inherits GameServerNetworkRequest
    Public Sub New(ByVal client As GameClient, ByVal data() As Byte)
        MyBase.makeme(client, data)
    End Sub

    Private _protocol As Integer
    Public Overrides Sub read()
        _protocol = readD()
    End Sub

    Public Overrides Sub run()
        If _protocol <> Config.MIN_PROTOCOL AndAlso _protocol <> Config.MAX_PROTOCOL Then
            Logger.error("сбой протокола. " & _protocol)
            getClient().sendPacket(New KeyPacket(getClient(), 0))
            getClient().termination()
            Return
        ElseIf _protocol = -1 Then
            Logger.extra_info("получаемый пинг " & _protocol)
            getClient().sendPacket(New KeyPacket(getClient(), 0))
            getClient().termination()
            Return
        End If

        Logger.info("принимаемый " & _protocol & " клиент")

        getClient().sendPacket(New KeyPacket(getClient(), 1))
        getClient().Protocol = _protocol


    End Sub
End Class
