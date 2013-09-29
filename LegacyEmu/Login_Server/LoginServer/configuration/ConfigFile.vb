'21:38:44 
'29.09.2013
'Autor: LargoWinch

Imports System.IO
Friend Class ConfigFile
    Private File As FileInfo
    Public _topics As SortedList(Of String, String)

    Public Sub New(ByVal Path As String)
        File = New FileInfo(Path)
        _topics = New SortedList(Of String, String)()
        reload()
    End Sub

    Public Sub reload()
        Dim reader As New StreamReader(File.FullName)
        Do While Not reader.EndOfStream
            Dim line As String = reader.ReadLine()
            If line.Length = 0 Then
                Continue Do
            End If

            If line.StartsWith("#") Then
                Continue Do
            End If
            _topics.Add(line.Split("="c)(0), line.Split("="c)(1))
        Loop
    End Sub

    Public Function getProperty(ByVal value As String, ByVal defaultprop As String) As String
        Dim ret As String
        Try
            ret = _topics(value)
        Catch ex As Exception
            Return defaultprop
        End Try
        Return If(ret Is Nothing, defaultprop, ret)
    End Function
End Class

