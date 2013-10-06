Imports System.Net.Sockets

Friend Class GameServer
    Public Sub New()
        Console.Title = "C# Lineage II Freya"

        Logger.form()
        Config.init("all")


        AuthThread.getInstance()


    End Sub
End Class
