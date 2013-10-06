Friend Class Config

    Public Shared SERVER_HOST As String
    Public Shared SERVER_PORT As Short

    Public Shared DB_HOST, DB_NAME, DB_USER, DB_PASS As String
    Public Shared DB_PORT As Integer
    Public Shared MIN_PROTOCOL As Integer
    Public Shared MAX_PROTOCOL As Integer
    Public Shared max_buffs As Integer

    Public Shared param() As String = {"server", "options", "rates"}

    Public Shared rate_quest_exp, rate_quest_sp, rate_quest_adena As Integer
    Public Shared RateSp, RateExp, RateRp As Integer
    Public Shared chat_shout, chat_trade As chatoptions
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

                DB_HOST = options.getProperty("dbhost", "localhost")
                DB_NAME = options.getProperty("dbname", "rabbit_cgame")
                DB_USER = options.getProperty("dbuser", "root")
                DB_PASS = options.getProperty("dbpass", "root")
                DB_PORT = Integer.Parse(options.getProperty("dbport", "3306"))

                MIN_PROTOCOL = Integer.Parse(options.getProperty("MinProtocolRevision", "146"))
                MAX_PROTOCOL = Integer.Parse(options.getProperty("MaxProtocolRevision", "152"))

                AUTH_HOST = options.getProperty("auth_host", "127.0.0.1")
                AUTH_PORT = Short.Parse(options.getProperty("auth_port", "2107"))

                auth_code = options.getProperty("auth_code", "<none>")
                max_players = Short.Parse(options.getProperty("max_players", "100"))

                gmonly = Byte.Parse(options.getProperty("status_gmonly", "0"))
                test = Byte.Parse(options.getProperty("status_testserver", "0"))

            Case "options"
                options = New ConfigFile("config\options.ini")
                ADD_ADENA_START = Integer.Parse(options.getProperty("StartingAdena", "0"))
                RATE_DROP_ITEMS = Integer.Parse(options.getProperty("RateDropItem", "1"))
                max_buffs = Integer.Parse(options.getProperty("max_buffs", "24"))
                chat_shout = CType(System.Enum.Parse(GetType(chatoptions), options.getProperty("chat_shout", "Default")), chatoptions)
                chat_trade = CType(System.Enum.Parse(GetType(chatoptions), options.getProperty("chat_trade", "Default")), chatoptions)

                world_visble_range_surface = Integer.Parse(options.getProperty("visible_surface_range", "4000"))
                world_visble_height_surface = Integer.Parse(options.getProperty("visible_surface_height", "1600"))
                world_visble_range_siege = Integer.Parse(options.getProperty("visible_surface_siege_range", "2700"))
                world_visble_height_siege = Integer.Parse(options.getProperty("visible_surface_siege_height", "1000"))
                world_visble_range_dungeon = Integer.Parse(options.getProperty("visible_surface_dungeon_range", "3000"))
                world_visble_height_dungeon = Integer.Parse(options.getProperty("visible_surface_dungeon_height", "500"))

                autoloot = Boolean.Parse(options.getProperty("autoloot", "True"))

                MinigameRankEnabled = Boolean.Parse(options.getProperty("MinigameRankEnabled", "True"))

            Case "rates"
                options = New ConfigFile("config\rates.ini")
                RateExp = Integer.Parse(options.getProperty("RateXp", "1"))
                RateSp = Integer.Parse(options.getProperty("RateSp", "1"))
                RateRp = Integer.Parse(options.getProperty("RateRp", "1"))

                rate_quest_exp = Integer.Parse(options.getProperty("quest_exp", "1"))
                rate_quest_sp = Integer.Parse(options.getProperty("quest_sp", "1"))
                rate_quest_adena = Integer.Parse(options.getProperty("quest_adena", "1"))
        End Select
    End Sub

    Public Enum chatoptions
        [Default]
        Disabled
        GMonly
        [Global]
    End Enum
End Class
