<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CommitData
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
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnPublish = New System.Windows.Forms.Button()
        Me.groupData = New System.Windows.Forms.GroupBox()
        Me.chkTilesets = New System.Windows.Forms.CheckBox()
        Me.chkNpcs = New System.Windows.Forms.CheckBox()
        Me.chkMaps = New System.Windows.Forms.CheckBox()
        Me.chkItems = New System.Windows.Forms.CheckBox()
        Me.chkAccounts = New System.Windows.Forms.CheckBox()
        Me.groupData.SuspendLayout()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(66, 13)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Editor Login:"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(86, 6)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(132, 20)
        Me.txtEmail.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Editor Password:"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(104, 38)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(114, 20)
        Me.txtPassword.TabIndex = 5
        '
        'btnPublish
        '
        Me.btnPublish.Location = New System.Drawing.Point(15, 77)
        Me.btnPublish.Name = "btnPublish"
        Me.btnPublish.Size = New System.Drawing.Size(203, 23)
        Me.btnPublish.TabIndex = 6
        Me.btnPublish.Text = "Publish"
        Me.btnPublish.UseVisualStyleBackColor = True
        '
        'groupData
        '
        Me.groupData.Controls.Add(Me.chkTilesets)
        Me.groupData.Controls.Add(Me.chkNpcs)
        Me.groupData.Controls.Add(Me.chkMaps)
        Me.groupData.Controls.Add(Me.chkItems)
        Me.groupData.Controls.Add(Me.chkAccounts)
        Me.groupData.Location = New System.Drawing.Point(224, 6)
        Me.groupData.Name = "groupData"
        Me.groupData.Size = New System.Drawing.Size(167, 94)
        Me.groupData.TabIndex = 7
        Me.groupData.TabStop = False
        Me.groupData.Text = "Data To Commit"
        '
        'chkTilesets
        '
        Me.chkTilesets.AutoSize = True
        Me.chkTilesets.Checked = True
        Me.chkTilesets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTilesets.Location = New System.Drawing.Point(99, 43)
        Me.chkTilesets.Name = "chkTilesets"
        Me.chkTilesets.Size = New System.Drawing.Size(67, 17)
        Me.chkTilesets.TabIndex = 4
        Me.chkTilesets.Text = "Tilessets"
        Me.chkTilesets.UseVisualStyleBackColor = True
        '
        'chkNpcs
        '
        Me.chkNpcs.AutoSize = True
        Me.chkNpcs.Checked = True
        Me.chkNpcs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNpcs.Location = New System.Drawing.Point(99, 20)
        Me.chkNpcs.Name = "chkNpcs"
        Me.chkNpcs.Size = New System.Drawing.Size(51, 17)
        Me.chkNpcs.TabIndex = 3
        Me.chkNpcs.Text = "Npcs"
        Me.chkNpcs.UseVisualStyleBackColor = True
        '
        'chkMaps
        '
        Me.chkMaps.AutoSize = True
        Me.chkMaps.Checked = True
        Me.chkMaps.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMaps.Location = New System.Drawing.Point(7, 66)
        Me.chkMaps.Name = "chkMaps"
        Me.chkMaps.Size = New System.Drawing.Size(52, 17)
        Me.chkMaps.TabIndex = 2
        Me.chkMaps.Text = "Maps"
        Me.chkMaps.UseVisualStyleBackColor = True
        '
        'chkItems
        '
        Me.chkItems.AutoSize = True
        Me.chkItems.Checked = True
        Me.chkItems.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkItems.Location = New System.Drawing.Point(7, 43)
        Me.chkItems.Name = "chkItems"
        Me.chkItems.Size = New System.Drawing.Size(51, 17)
        Me.chkItems.TabIndex = 1
        Me.chkItems.Text = "Items"
        Me.chkItems.UseVisualStyleBackColor = True
        '
        'chkAccounts
        '
        Me.chkAccounts.AutoSize = True
        Me.chkAccounts.Checked = True
        Me.chkAccounts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAccounts.Location = New System.Drawing.Point(7, 20)
        Me.chkAccounts.Name = "chkAccounts"
        Me.chkAccounts.Size = New System.Drawing.Size(71, 17)
        Me.chkAccounts.TabIndex = 0
        Me.chkAccounts.Text = "Accounts"
        Me.chkAccounts.UseVisualStyleBackColor = True
        '
        'CommitData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 108)
        Me.Controls.Add(Me.groupData)
        Me.Controls.Add(Me.btnPublish)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtEmail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CommitData"
        Me.Text = "Commit Data"
        Me.groupData.ResumeLayout(False)
        Me.groupData.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txtEmail As System.Windows.Forms.TextBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnPublish As System.Windows.Forms.Button
    Friend WithEvents groupData As System.Windows.Forms.GroupBox
    Friend WithEvents chkTilesets As System.Windows.Forms.CheckBox
    Friend WithEvents chkNpcs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMaps As System.Windows.Forms.CheckBox
    Friend WithEvents chkItems As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccounts As System.Windows.Forms.CheckBox
End Class
