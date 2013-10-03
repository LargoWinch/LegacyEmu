'21:20:03 
'29.09.2013
'Autor: LargoWinch

Imports System.Text
Friend Class Config

    Public Shared SERVER_HOST As String
    Public Shared SERVER_PORT As Integer
    Public Shared SERVER_PORT_GAMESERVER As Integer

    Public Shared DATABASE_HOST, DATABASE_NAME, DATABASE_USER, DATABASE_PASSWORD As String

    Public Shared AUTO_ACCOUNTS As Boolean
    Public Shared ShowAgreement As Boolean

    Public Shared Sub load()
        Dim server As New ConfigFile("config\server.ini")

        SERVER_HOST = server.getProperty("Host", "127.0.0.1")
        SERVER_PORT = Integer.Parse(server.getProperty("Port", "2106"))
        SERVER_PORT_GAMESERVER = Integer.Parse(server.getProperty("GameServerPort", "2107"))

        DATABASE_HOST = server.getProperty("DataBaseHost", "localhost")
        DATABASE_NAME = server.getProperty("DataBaseName", "freya")
        DATABASE_USER = server.getProperty("DataBaseUser", "root")
        DATABASE_PASSWORD = server.getProperty("DataBasePassword", "default")

        AUTO_ACCOUNTS = Boolean.Parse(server.getProperty("AutoAccounts", "false"))
        ShowAgreement = Boolean.Parse(server.getProperty("ShowAgreement", "false"))
    End Sub
End Class
