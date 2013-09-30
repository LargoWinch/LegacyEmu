'00:25:40
'01.10.2013
'Autor: LargoWinch

Imports System.Net.Sockets
Imports System.Net

Module Module1

    Sub Main()
        StartProgram.getInstance()
        Process.GetCurrentProcess().WaitForExit()
    End Sub

End Module


Friend Class StartProgram

    Private Shared auth As New StartProgram()
    Public Shared Function getInstance() As StartProgram
        Return auth
    End Function

    Protected LoginListener As TcpListener

    Public Sub New()
        Console.Title = "Login server Lineage II"

        Console.WriteLine("#======================================================================#")
        Console.WriteLine("#                                                                      #")
        Console.WriteLine("#$$      $$$$$    $$$$    $$$$    $$$$   $$  $$  $$$$$   $$   $$ $$  $$#")
        Console.WriteLine("#$$      $$      $$      $$  $$  $$  $$   $$$$   $$      $$$ $$$ $$  $$#")
        Console.WriteLine("#$$      $$$$    $$ $$$  $$$$$$  $$        $$    $$$$    $$ $ $$ $$  $$#")
        Console.WriteLine("#$$      $$      $$  $$  $$  $$  $$  $$    $$    $$      $$   $$ $$  $$#")
        Console.WriteLine("#$$$$$$  $$$$$    $$$$   $$  $$   $$$$     $$    $$$$$   $$   $$  $$$$ #")
        Console.WriteLine("#----------------------------------------------------------------------#")
        Console.WriteLine("#----------------------------------------------------------------------#")
        Console.WriteLine("#                         VB.NET L2 Emulator                           #")
        Console.WriteLine("#----------------------------------------------------------------------#")
        Console.WriteLine("#              Copyright (c) 2013 Legacy Dev Team                      #")
        Console.WriteLine("#======================================================================#")
    End Sub
End Class

