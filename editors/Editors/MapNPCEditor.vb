Imports Prospekt.Graphics
Public Class MapNPCClass
    Private curNpc As Integer
    Public Sub Init()
        If NPCCount > 0 Then
            If Not IsNothing(NPC) Then
                MapNPCs.lstNPCs.Items.Clear()
                For I As Integer = 0 To NPCCount
                    If Not IsNothing(NPC(I)) Then
                        MapNPCs.lstNPCs.Items.Add(NPC(I).Name)
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub SelectNPC(ByVal Index As Integer)
        curNpc = Index
    End Sub

    Public Sub DrawNPC()
        If curNpc > 0 And Not IsNothing(NPC(curNpc)) And NPC(curNpc).Sprite > 0 Then
            Render.RenderTexture(Render.MapNPCWindow, texSprite(NPC(curNpc).Sprite), 0, 0, 0, 0, gTexture(texSprite(NPC(curNpc).Sprite)).Width, gTexture(texSprite(NPC(curNpc).Sprite)).Height, gTexture(texSprite(NPC(curNpc).Sprite)).Width, gTexture(texSprite(NPC(curNpc).Sprite)).Height)
        End If
    End Sub
End Class
