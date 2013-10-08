
Friend Class CharCreateFail
    Inherits GameServerPacket

    'Назначение: сообщает клиенту что попытка создания чара авершилась неудачей 
    'Формат: 
    '1A 
    'XX XX XX XX // Причина: 
    '// Creation Failed 
    '// Слишком много чаров на акке 
    '// Имя чара уже существует 
    '// 16 eng chars. (слишком длинное имя ???) 
    Public Enum CharCreateFailReason
        JUST_FAILED = 0
        TOO_MANY_CHARS_ON_ACCOUNT = 2
        NAME_EXISTS = 2
        TOO_LONG_16_CHARS = 3
        INCORRECT_NAME = 4
        CHAR_CREATION_BLOCKED = 5
        CREATION_RESTRICTION = 6
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

    Private _reason As CharCreateFailReason
    Public Sub New(ByVal client As GameClient, ByVal reason As CharCreateFailReason)
        _reason = reason
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&H10)
        writeD(CInt(Fix(_reason)))
    End Sub
End Class


