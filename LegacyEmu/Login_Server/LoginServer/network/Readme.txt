'10:21:09
'30.09.2013
'Autor: LargoWinch

# 0x01 loginfail2 
# 0x02 accountKicked1 
# 0x03 loginok 
# 0x04 serverlist 
# 0x05 serverfail 
# 0x06 playfail 
# 0x07 playok 
# 0x08 accountKicked 
# 0x09 blockedAccMsg // бан 
# 0x20 protocol version different 
# 0x00 VersionCheck


# 00 - Init - Назначение: передает клиенту список серверов и их состояние 
# 01 - LoginFail - Назначение: сообщает о неудачной попытке подключения к логин серверу 
# 03 - LoginOk - Назначение: высылается в подтсверждение на пакет RequestAuthLogin, в случае успешной проверки логина и пароля. 
# 04 - ServerList - Назначение: передает клиенту список серверов и их состояние 
# 04 - PlayOk - Назначение: ответ на запрос авторизации на game-сервере 
# 0B - GGAuth - Назначение: ответ на запрос GameGuard авторизации 