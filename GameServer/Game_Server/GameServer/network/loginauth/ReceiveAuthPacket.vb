Public MustInherit Class ReceiveAuthPacket
    Private _packet() As Byte
    Private _offset As Integer
    Public login As AuthThread
    Public Sub makeme(ByVal login As AuthThread, ByVal packet() As Byte)
        Me.login = login
        _packet = packet
        _offset = 1
        read()
    End Sub

    Public Function readC() As Byte
        Dim result As Byte = _packet(_offset)
        _offset += 1
        Return result
    End Function

    Public Function readH() As Short
        Dim result As Short = BitConverter.ToInt16(_packet, _offset)
        _offset += 2
        Return result
    End Function

    Public Function readD() As Integer
        Dim result As Integer = BitConverter.ToInt32(_packet, _offset)
        _offset += 4
        Return result
    End Function

    Public Function readQ() As Long
        Dim result As Long = BitConverter.ToInt64(_packet, _offset)
        _offset += 8
        Return result
    End Function

    Public Function readB(ByVal Length As Integer) As Byte()
        Dim result(Length - 1) As Byte
        Array.Copy(_packet, _offset, result, 0, Length)
        _offset += Length
        Return result
    End Function

    Public Function readF() As Double
        Dim result As Double = BitConverter.ToDouble(_packet, _offset)
        _offset += 8
        Return result
    End Function

    Public Function readS() As String
        Dim result As String = ""
        Try
            result = System.Text.Encoding.Unicode.GetString(_packet, _offset, _packet.Length - _offset)
            Dim idx As Integer = result.IndexOf(ChrW(&H0))
            If idx <> -1 Then
                result = result.Substring(0, idx)
            End If
            _offset += (result.Length * 2) + 2
        Catch ex As Exception
            Console.WriteLine("чтение строки из пакета, " & ex.Message & " " & ex.StackTrace)
        End Try
        Return result
    End Function

    Public MustOverride Sub read()
    Public MustOverride Sub run()
End Class
