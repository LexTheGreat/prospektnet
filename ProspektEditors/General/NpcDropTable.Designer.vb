<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NpcDropTable
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.groupGameItems = New System.Windows.Forms.GroupBox()
        Me.lstGameItems = New System.Windows.Forms.ListBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lstNpcItems = New System.Windows.Forms.ListBox()
        Me.groupNpcItems = New System.Windows.Forms.GroupBox()
        Me.btnSaveNpcDrops = New System.Windows.Forms.Button()
        Me.groupGameItems.SuspendLayout()
        Me.groupNpcItems.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupGameItems
        '
        Me.groupGameItems.Controls.Add(Me.lstGameItems)
        Me.groupGameItems.Controls.Add(Me.btnAdd)
        Me.groupGameItems.Controls.Add(Me.btnRemove)
        Me.groupGameItems.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupGameItems.Location = New System.Drawing.Point(0, 0)
        Me.groupGameItems.Name = "groupGameItems"
        Me.groupGameItems.Size = New System.Drawing.Size(215, 500)
        Me.groupGameItems.TabIndex = 52
        Me.groupGameItems.TabStop = False
        Me.groupGameItems.Text = "Game Items"
        '
        'lstGameItems
        '
        Me.lstGameItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstGameItems.FormattingEnabled = True
        Me.lstGameItems.Location = New System.Drawing.Point(3, 16)
        Me.lstGameItems.Name = "lstGameItems"
        Me.lstGameItems.Size = New System.Drawing.Size(209, 435)
        Me.lstGameItems.TabIndex = 41
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAdd.Location = New System.Drawing.Point(3, 451)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(209, 23)
        Me.btnAdd.TabIndex = 43
        Me.btnAdd.Text = "Add Item"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnRemove.Location = New System.Drawing.Point(3, 474)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(209, 23)
        Me.btnRemove.TabIndex = 42
        Me.btnRemove.Text = "Remove Item"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lstNpcItems
        '
        Me.lstNpcItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstNpcItems.FormattingEnabled = True
        Me.lstNpcItems.Location = New System.Drawing.Point(3, 16)
        Me.lstNpcItems.Name = "lstNpcItems"
        Me.lstNpcItems.Size = New System.Drawing.Size(185, 458)
        Me.lstNpcItems.TabIndex = 41
        '
        'groupNpcItems
        '
        Me.groupNpcItems.Controls.Add(Me.lstNpcItems)
        Me.groupNpcItems.Controls.Add(Me.btnSaveNpcDrops)
        Me.groupNpcItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupNpcItems.Location = New System.Drawing.Point(215, 0)
        Me.groupNpcItems.Name = "groupNpcItems"
        Me.groupNpcItems.Size = New System.Drawing.Size(191, 500)
        Me.groupNpcItems.TabIndex = 53
        Me.groupNpcItems.TabStop = False
        Me.groupNpcItems.Text = "Npc Items"
        '
        'btnSaveNpcDrops
        '
        Me.btnSaveNpcDrops.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnSaveNpcDrops.Location = New System.Drawing.Point(3, 474)
        Me.btnSaveNpcDrops.Name = "btnSaveNpcDrops"
        Me.btnSaveNpcDrops.Size = New System.Drawing.Size(185, 23)
        Me.btnSaveNpcDrops.TabIndex = 42
        Me.btnSaveNpcDrops.Text = "Save"
        Me.btnSaveNpcDrops.UseVisualStyleBackColor = True
        '
        'NpcDropTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 500)
        Me.Controls.Add(Me.groupNpcItems)
        Me.Controls.Add(Me.groupGameItems)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "NpcDropTable"
        Me.Text = "Npc Drop Table"
        Me.groupGameItems.ResumeLayout(False)
        Me.groupNpcItems.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents groupGameItems As System.Windows.Forms.GroupBox
    Public WithEvents lstGameItems As System.Windows.Forms.ListBox
    Public WithEvents lstNpcItems As System.Windows.Forms.ListBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnSaveNpcDrops As System.Windows.Forms.Button
    Friend WithEvents groupNpcItems As System.Windows.Forms.GroupBox
End Class
