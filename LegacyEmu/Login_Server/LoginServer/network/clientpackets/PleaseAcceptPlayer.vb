﻿Public Class PleaseAcceptPlayer
    Inherits L_SendServerPacket
    Private time As String

    Protected Friend Overrides Sub write()
        writeC(&HA7)
        writeS(time)
    End Sub
End Class
