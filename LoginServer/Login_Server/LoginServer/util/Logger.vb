'21:47:03 
'29.09.2013
'Autor: LargoWinch

Imports System.IO
Friend Class Logger
    Private Shared cf As Boolean = False
    Private Shared tw As TextWriter
    Private Shared name As String

    Public Shared Sub warning(ByVal text As String)
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine(text)
        If cf Then
            tw.WriteLine(text)
        End If
        Console.ResetColor()
    End Sub

    Public Shared Sub [error](ByVal text As String)
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine(text)
        If cf Then
            tw.WriteLine(text)
        End If
        Console.ResetColor()
    End Sub

    Public Shared Sub info(ByVal text As String)
        Console.WriteLine(text)
        If cf Then
            tw.WriteLine(text)
        End If
    End Sub

    Public Shared Sub extra_info(ByVal text As String)
        Console.ForegroundColor = ConsoleColor.DarkGray
        Console.WriteLine(text)
        If cf Then
            tw.WriteLine(text)
        End If
        Console.ResetColor()
    End Sub

    Public Shared Sub form()
        If Not cf Then
            Return
        End If

        name = "log\" & Date.Now.ToString("yyyy-MM-dd HH-mm-ss") & ".log"
        tw = File.CreateText(name)
        extra_info("Лог файл создан " & name)
    End Sub
End Class

