'18:53:59
'30.09.2013
'Autor: LargoWinch


Imports System.IO
Imports System.Text
Public MustInherit Class L_SendBasePacket
    Private mstream As MemoryStream
    Public lc As L_LoginClient

    Public Sub makeme(ByVal client As L_LoginClient)
        mstream = New MemoryStream()
        lc = client
    End Sub

    Protected Sub writeB(ByVal value() As Byte)
        mstream.Write(value, 0, value.Length)
    End Sub

    Protected Sub writeD(ByVal value As Integer)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeH(ByVal value As Short)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeC(ByVal value As Byte)
        mstream.WriteByte(value)
    End Sub

    Protected Sub writeF(ByVal value As Double)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeQ(ByVal value As Long)
        writeB(BitConverter.GetBytes(value))
    End Sub

    Protected Sub writeS(ByVal value As String)
        If value IsNot Nothing Then
            writeB(Encoding.Unicode.GetBytes(value))
        End If

        writeH(0)
    End Sub

    Public Function ToByteArray() As Byte()
        Return mstream.ToArray()
    End Function

    Public ReadOnly Property Length() As Long
        Get
            Return mstream.Length
        End Get
    End Property

    Protected Friend MustOverride Sub write()
End Class
