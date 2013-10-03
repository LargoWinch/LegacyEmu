'02:22:36
'03.10.2013
'Autor: LargoWinch

Friend Class ServerLoginOk
    Inherits L_SendServerPacket
    Protected Friend Overrides Sub write()
        writeC(&HA6)
        writeS("auth complete")
    End Sub
End Class
