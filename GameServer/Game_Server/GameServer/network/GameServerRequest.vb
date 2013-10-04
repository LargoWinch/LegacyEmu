'21:25:51
'03.10.2013
'Autor: LargoWinch

Public MustInherit Class GameServerRequest
    Private buffer() As Byte
    Private position As Integer
    Public Client As GameClient

    Public Function getClient() As GameClient
        Return Client
    End Function

    Public Sub makeme(ByVal Client As GameClient, ByVal packet() As Byte)
        Me.Client = Client
        buffer = packet
        position = 1
        read()
    End Sub

    Public Sub makeme(ByVal Client As GameClient, ByVal packet() As Byte, ByVal plus As Byte)
        Me.Client = Client
        buffer = packet
        position = 1 + plus
        read()
    End Sub

    Public Function readC() As Byte
        Dim result As Byte = buffer(position)
        position += 1
        Return result
    End Function

    Public Function readH() As Short
        Dim result As Short = BitConverter.ToInt16(buffer, position)
        position += 2
        Return result
    End Function

    Public Function readD() As Integer
        Dim result As Integer = BitConverter.ToInt32(buffer, position)
        position += 4
        Return result
    End Function

    Public Function readQ() As Long
        Dim result As Long = BitConverter.ToInt64(buffer, position)
        position += 8
        Return result
    End Function

    Public Function readB(ByVal len As Integer) As Byte()
        Dim result(len - 1) As Byte
        Array.Copy(buffer, position, result, 0, len)
        position += len
        Return result
    End Function

    Public Function readF() As Double
        Dim result As Double = BitConverter.ToDouble(buffer, position)
        position += 8
        Return result
    End Function

    Public Function readS() As String
        Dim result As String = ""
        Try
            result = System.Text.Encoding.Unicode.GetString(buffer, position, buffer.Length - position)
            Dim idx As Integer = result.IndexOf(ChrW(&H0))
            If idx <> -1 Then
                result = result.Substring(0, idx)
            End If
            position += (result.Length * 2) + 2
        Catch ex As Exception
            Console.WriteLine("чтение строки от пакета, " & ex.Message & " " & ex.StackTrace)
        End Try
        Return result
    End Function

    Public MustOverride Sub read()
    Public MustOverride Sub run()
End Class