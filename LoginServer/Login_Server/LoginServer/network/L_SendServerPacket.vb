'19:05:36
'30.09.2013
'Autor: LargoWinch

Imports System.IO

Public MustInherit Class L_SendServerPacket
    Private memorystream As MemoryStream

    Public Sub New()
        memorystream = New MemoryStream()
    End Sub


    Protected Sub writeB(ByVal value() As Byte)
        memorystream.Write(value, 0, value.Length)
    End Sub

    Protected Sub writeB(ByVal value() As Byte, ByVal Offset As Integer, ByVal Length As Integer)
        memorystream.Write(value, Offset, Length)
    End Sub

    Protected Sub writeD(ByVal value As UInteger)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeD(ByVal value As Integer)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeH(ByVal value As Short)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeC(ByVal value As Byte)
        memorystream.WriteByte(value)
    End Sub

    Protected Sub writeC(ByVal value As Integer)
        memorystream.WriteByte(CByte(value))
    End Sub

    Protected Sub writeF(ByVal value As Double)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeS(ByVal value As String)
        If value IsNot Nothing Then
            writeB(System.Text.Encoding.Unicode.GetBytes(value))
        End If

        memorystream.WriteByte(0)
        memorystream.WriteByte(0)
    End Sub

    Protected Sub writeQ(ByVal value As Long)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Public Function ToByteArray() As Byte()
        Return memorystream.ToArray()
    End Function

    Public ReadOnly Property Length() As Long
        Get
            Return memorystream.Length
        End Get
    End Property

    Protected Friend MustOverride Sub write()
End Class
