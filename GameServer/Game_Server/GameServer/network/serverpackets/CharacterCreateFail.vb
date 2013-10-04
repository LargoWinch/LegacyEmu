Friend Class CharacterCreateFail
    Inherits GameServerPacket
    Public Enum CharCreateFailReason
        REASON_CREATION_FAILED = 0
        REASON_TOO_MANY_CHARACTERS = 2
        REASON_NAME_ALREADY_EXISTS = 2
        REASON_16_ENG_CHARS = 3
        REASON_INCORRECT_NAME = 4
        REASON_CHARS_CANT_CREATED_FROM_SERVER = 5
        REASON_UNABLE_CREATE_REASON_TOO_CHAR = 6
    End Enum

    '        
    '            0 your character creation has failed
    '            1 You cannot create another character. Please delete the existing character and try again.
    '            2 This name already exists.
    '            3 Your title cannot exceed 16 characters in length.  Please try again.
    '            4 Incorrect name. please try again
    '            5 characters cannot be created from this server
    '            6 Unable to create character. You are unable to create a new character on the selected server. A restriction is in place which restricts users from creating characters on different servers where no previous character exists. Please choose another server.
    '         

    Private _error As CharCreateFailReason
    Public Sub New(ByVal client As GameClient, ByVal errorCode As CharCreateFailReason)
        _error = errorCode
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&H10)
        writeD(CInt(Fix(_error)))
    End Sub
End Class
