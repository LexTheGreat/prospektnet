<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TextureViewer
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
        Me.groupTextureList = New System.Windows.Forms.GroupBox()
        Me.lstTextures = New System.Windows.Forms.ListBox()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.panTextures = New System.Windows.Forms.Panel()
        Me.picTexture = New System.Windows.Forms.PictureBox()
        Me.groupTextureList.SuspendLayout()
        Me.panTextures.SuspendLayout()
        CType(Me.picTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'groupTextureList
        '
        Me.groupTextureList.Controls.Add(Me.lstTextures)
        Me.groupTextureList.Controls.Add(Me.cmbType)
        Me.groupTextureList.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupTextureList.Location = New System.Drawing.Point(0, 0)
        Me.groupTextureList.Name = "groupTextureList"
        Me.groupTextureList.Size = New System.Drawing.Size(155, 310)
        Me.groupTextureList.TabIndex = 4
        Me.groupTextureList.TabStop = False
        Me.groupTextureList.Text = "Texture List"
        '
        'lstTextures
        '
        Me.lstTextures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTextures.FormattingEnabled = True
        Me.lstTextures.Location = New System.Drawing.Point(3, 37)
        Me.lstTextures.Name = "lstTextures"
        Me.lstTextures.Size = New System.Drawing.Size(149, 270)
        Me.lstTextures.TabIndex = 0
        '
        'cmbType
        '
        Me.cmbType.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(3, 16)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(149, 21)
        Me.cmbType.TabIndex = 1
        '
        'panTextures
        '
        Me.panTextures.AutoScroll = True
        Me.panTextures.BackColor = System.Drawing.Color.Transparent
        Me.panTextures.Controls.Add(Me.picTexture)
        Me.panTextures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panTextures.Location = New System.Drawing.Point(155, 0)
        Me.panTextures.Name = "panTextures"
        Me.panTextures.Size = New System.Drawing.Size(309, 310)
        Me.panTextures.TabIndex = 5
        '
        'picTexture
        '
        Me.picTexture.BackColor = System.Drawing.Color.Transparent
        Me.picTexture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picTexture.Location = New System.Drawing.Point(0, 0)
        Me.picTexture.Name = "picTexture"
        Me.picTexture.Size = New System.Drawing.Size(127, 92)
        Me.picTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picTexture.TabIndex = 11
        Me.picTexture.TabStop = False
        '
        'TextureViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 310)
        Me.Controls.Add(Me.panTextures)
        Me.Controls.Add(Me.groupTextureList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "TextureViewer"
        Me.Text = "Texture Viewer"
        Me.groupTextureList.ResumeLayout(False)
        Me.panTextures.ResumeLayout(False)
        Me.panTextures.PerformLayout()
        CType(Me.picTexture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents groupTextureList As System.Windows.Forms.GroupBox
    Friend WithEvents lstTextures As System.Windows.Forms.ListBox
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents panTextures As System.Windows.Forms.Panel
    Friend WithEvents picTexture As System.Windows.Forms.PictureBox
End Class
