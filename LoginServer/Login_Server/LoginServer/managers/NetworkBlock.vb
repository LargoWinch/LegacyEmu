'03:15:22
'02.10.2013
'Autor: LargoWinch

Imports System.IO

Public Class NetworkBlock
    Private Shared nb As New NetworkBlock()
    Public Shared Function getInstance() As NetworkBlock
        Return nb
    End Function

    Protected blocks As New List(Of NB_interface)()

    Public Sub New()
        Dim reader As New StreamReader(New FileInfo("sq\blocks.txt").FullName)
        Do While Not reader.EndOfStream
            Dim line As String = reader.ReadLine()
            If line.Length = 0 Then
                Continue Do
            End If

            If line.StartsWith("//") Then
                Continue Do
            End If

            If line.StartsWith("d") Then
                Dim i As New NB_interface()
                i.directIp = line.Split(" "c)(1)
                i.forever = line.Split(" "c)(2).Equals("0")
                blocks.Add(i)
            ElseIf line.StartsWith("m") Then
                Dim i As New NB_interface()
                i.mask = line.Split(" "c)(1)
                i.forever = line.Split(" "c)(2).Equals("0")
                blocks.Add(i)
            End If
        Loop

        Logger.info("NetworkBlock: " & blocks.Count & " блок.")
    End Sub

    Public Function allowed(ByVal ip As String) As Boolean
        If blocks.Count = 0 Then
            Return True
        End If

        For Each nbi As NB_interface In blocks
            If nbi.directIp IsNot Nothing Then
                If nbi.directIp.Equals(ip) Then
                    If nbi.forever Then
                        Return False
                    Else
                        If nbi.timeEnd.CompareTo(Date.Now) = 1 Then
                            Return False
                        End If
                    End If
                End If
            End If

            If nbi.mask IsNot Nothing Then
                Dim a() As String = ip.Split("."c), b() As String = nbi.mask.Split("."c)
                Dim d(3) As Boolean
                For c As Byte = 0 To 3
                    d(c) = False

                    If b(c) = "*" Then
                        d(c) = True
                    ElseIf b(c) = a(c) Then
                        d(c) = True
                    ElseIf b(c).Contains("/") Then
                        Dim n As Byte = Byte.Parse(b(c).Split("/"c)(0)), x As Byte = Byte.Parse(b(c).Split("/"c)(1))
                        Dim t As Byte = Byte.Parse(a(c))
                        d(c) = t >= n AndAlso t <= x
                    End If
                Next c

                Dim cnt As Byte = 0
                For Each u As Boolean In d
                    If u Then
                        cnt += 1
                    End If
                Next u

                If cnt >= 4 Then
                    Return False
                End If
            End If
        Next nbi

        Return True
    End Function
End Class

Public Class NB_interface
    Public directIp As String = Nothing
    Public mask As String = Nothing
    Public forever As Boolean = False
    Public timeEnd As Date
End Class
