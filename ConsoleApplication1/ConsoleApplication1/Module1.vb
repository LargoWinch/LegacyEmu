Module Module1

    Sub Main()
        ' StartProgram.getInstance()
        Console.ReadLine()

    End Sub

End Module

Friend Class StartProgram
    Private Shared auth As New StartProgram()
    Public Shared Function getInstance() As StartProgram
        Return auth
    End Function
    Public Sub ShowLogo()

        Console.BackgroundColor = ConsoleColor.DarkBlue
        Console.ForegroundColor = ConsoleColor.White
        FillLine("#==================================================#")
        FillLine("#                _                                 #")
        FillLine("#               /_\   ___ _ __ ___  ___            #")
        FillLine("#              //_\\ / _ \ '__/ _ \/ __|           #")
        FillLine("#             /  _  \  __/ | | (_) \__ \           #")
        FillLine("#             \_/ \_/\___|_|  \___/|___/           #")
        FillLine("#                                                  #")
        FillLine("#--------------------------------------------------#")
        FillLine("#                   C# L2 Emulator                 #")
        FillLine("#--------------------------------------------------#")
        FillLine("#        Copyright (c) 2012 Aeros Dev Team         #")
        FillLine("#==================================================#")
        Console.WriteLine()
        Console.ReadLine(ShowLogo)
    End Sub
    Public Shared Sub FillLine(p As String)
        Dim len As Integer = p.Length
        Dim total As Integer = Console.WindowWidth
        Dim start As Integer = (total / 2) - (len / 2)
        Dim i As Integer = Console.CursorLeft
        While i < start
            Console.Write(" ")
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        Console.Write(p)
        'Dim i As Integer = Console.CursorLeft
        While i < total
            Console.Write(" ")
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
    End Sub
End Class

