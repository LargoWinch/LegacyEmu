'15:10:49
'03.10.2013
'Autor: LargoWinch

Friend Class Cfg
    Public Shared SERVER_HOST As String
    Public Shared SERVER_PORT As Short

    Public Shared DATABASE_HOST As String
    Public Shared DATABASE_NAME As String
    Public Shared DATABASE_USER As String
    Public Shared DATABASE_PASSWORD As String
    Public Shared DATABASE_PORT As Integer
    Public Shared MIN_PROTOCOL As Integer
    Public Shared MAX_PROTOCOL As Integer
    Public Shared max_buffs As Integer

    Public Shared param() As String = {"server", "options", "rates", "quest", "raidboss"}

    Public Shared rate_quest_exp As Integer
    Public Shared rate_quest_sp As Integer
    Public Shared rate_quest_adena As Integer
    Public Shared RateSp As Integer
    Public Shared RateExp As Integer
    Public Shared RateRp As Integer
    Public Shared chat_shout As chatoptions
    Public Shared chat_trade As chatoptions
    Public Shared world_visble_range_surface As Integer
    Public Shared world_visble_height_surface As Integer
    Public Shared world_visble_range_siege As Integer
    Public Shared world_visble_height_siege As Integer
    Public Shared world_visble_range_dungeon As Integer
    Public Shared world_visble_height_dungeon As Integer

    Public Shared ADD_ADENA_START As Integer
    Public Shared RATE_DROP_ITEMS As Integer

    Public Shared autoloot As Boolean
    Public Shared AUTH_HOST As String
    Public Shared AUTH_PORT As Short
    Public Shared auth_code As String
    Public Shared max_players As Short
    Public Shared gmonly As Byte
    Public Shared test As Byte
    Public Shared MinigameRankEnabled As Boolean

    Public Shared Sub init(ByVal val As String)
        If val.Equals("all") Then
            For Each type As String In param
                load(type)
            Next type
        Else
            If Not val.Equals("") Then
                load(val)
            End If
        End If
    End Sub


    Public Shared Sub load(ByVal param As String)
        Dim options As ConfigFile
        Select Case param
            Case "server"
                options = New ConfigFile("config\server.ini")

                SERVER_HOST = options.getProperty("host", "127.0.0.1")
                SERVER_PORT = Short.Parse(options.getProperty("port", "7777"))

                DATABASE_HOST = options.getProperty("DataBaseHost", "localhost")
                DATABASE_NAME = options.getProperty("DataBaseName", "freya")
                DATABASE_USER = options.getProperty("DataBaseUser", "root")
                DATABASE_PASSWORD = options.getProperty("DataBasePassword", "root")
                DATABASE_PORT = Integer.Parse(options.getProperty("DataBasePort", "3306"))

                MIN_PROTOCOL = Integer.Parse(options.getProperty("MinProtocolRevision", "216"))
                MAX_PROTOCOL = Integer.Parse(options.getProperty("MaxProtocolRevision", "216"))

                AUTH_HOST = options.getProperty("auth_host", "127.0.0.1")
                AUTH_PORT = Short.Parse(options.getProperty("auth_port", "2107"))

                auth_code = options.getProperty("auth_code", "<none>")
                max_players = Short.Parse(options.getProperty("max_players", "100"))

                gmonly = Byte.Parse(options.getProperty("status_gmonly", "0"))
                test = Byte.Parse(options.getProperty("status_testserver", "0"))

            Case "options"
                'TODO в будушем

            Case "rates"
                'TODO в будушем
        End Select
    End Sub

    Public Enum chatoptions
        [Default]
        Disabled
        GMonly
        [Global]
    End Enum
End Class
