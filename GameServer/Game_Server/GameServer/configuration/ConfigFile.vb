'12:25:49
'03.10.2013
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

        Console.WriteLine("Config file " & File.Name & " loaded with " & _topics.Count & " parameters.")
    End Sub

    Public Function getProperty(ByVal value As String, ByVal defaultprop As String) As String
        Dim ret As String
        Try
            ret = _topics(value)
        Catch
            ret = Nothing
            Console.WriteLine("config: error, parameter " & value & " was not found")
        End Try

        Return If(ret Is Nothing, defaultprop, ret)
    End Function
End Class
