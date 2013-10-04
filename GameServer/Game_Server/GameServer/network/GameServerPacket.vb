'21:15:50
'03.10.2013
'Autor: LargoWinch

Imports System.IO

Public MustInherit Class GameServerPacket
    Private stream As New MemoryStream()

    Protected Sub writeEx(ByVal value As Integer)
        writeC(EXTENDED_PACKET)
        writeH(value)
    End Sub

    Public Shared EXTENDED_PACKET As Integer = &HFE

    Protected Sub writeB(ByVal value() As Byte)
        stream.Write(value, 0, value.Length)
    End Sub

    Protected Sub writeB(ByVal value() As Byte, ByVal Offset As Integer, ByVal Length As Integer)
        stream.Write(value, Offset, Length)
    End Sub

    Protected Sub writeD(Optional ByVal value As UInteger = 0)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeD(Optional ByVal value As Integer = 0)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeD(Optional ByVal value As Double = 0.0)
        writeB(BitConverter.GetBytes(CInt(Fix(value))))
    End Sub

    Protected Sub writeH(Optional ByVal value As UShort = 0)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeH(Optional ByVal value As Short = 0)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeH(Optional ByVal value As Integer = 0)
        writeB(BitConverter.GetBytes(CShort(Fix(value))))
    End Sub

    Protected Sub writeC(ByVal value As Byte)
        stream.WriteByte(value)
    End Sub

    Protected Sub writeC(ByVal value As Integer)
        stream.WriteByte(CByte(value))
    End Sub

    Protected Sub writeF(ByVal value As Double)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeS(ByVal value As String)
        If value IsNot Nothing Then
            writeB(System.Text.Encoding.Unicode.GetBytes(value))
        End If

        stream.WriteByte(0)
        stream.WriteByte(0)
    End Sub

    Protected Sub writeQ(ByVal value As Long)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Public Function ToByteArray() As Byte()
        Return stream.ToArray()
    End Function

    Public ReadOnly Property Length() As Long
        Get
            Return stream.Length
        End Get
    End Property

    Protected Friend MustOverride Sub write()
End Class
