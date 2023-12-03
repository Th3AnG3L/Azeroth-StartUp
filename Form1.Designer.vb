<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.BunifuProgressBar1 = New Bunifu.UI.WinForms.BunifuProgressBar()
        Me.BunifuElipse1 = New Bunifu.Framework.UI.BunifuElipse(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'BunifuProgressBar1
        '
        Me.BunifuProgressBar1.AllowAnimations = False
        Me.BunifuProgressBar1.Animation = 0
        Me.BunifuProgressBar1.AnimationSpeed = 220
        Me.BunifuProgressBar1.AnimationStep = 10
        Me.BunifuProgressBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.BunifuProgressBar1.BackgroundImage = CType(resources.GetObject("BunifuProgressBar1.BackgroundImage"), System.Drawing.Image)
        Me.BunifuProgressBar1.BorderColor = System.Drawing.Color.Transparent
        Me.BunifuProgressBar1.BorderRadius = 9
        Me.BunifuProgressBar1.BorderThickness = 1
        Me.BunifuProgressBar1.ForeColor = System.Drawing.Color.White
        Me.BunifuProgressBar1.Location = New System.Drawing.Point(122, 191)
        Me.BunifuProgressBar1.Maximum = 100
        Me.BunifuProgressBar1.MaximumValue = 100
        Me.BunifuProgressBar1.Minimum = 0
        Me.BunifuProgressBar1.MinimumValue = 0
        Me.BunifuProgressBar1.Name = "BunifuProgressBar1"
        Me.BunifuProgressBar1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BunifuProgressBar1.ProgressBackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.BunifuProgressBar1.ProgressColorLeft = System.Drawing.Color.DodgerBlue
        Me.BunifuProgressBar1.ProgressColorRight = System.Drawing.Color.MediumSeaGreen
        Me.BunifuProgressBar1.Size = New System.Drawing.Size(225, 16)
        Me.BunifuProgressBar1.TabIndex = 3
        Me.BunifuProgressBar1.Value = 0
        Me.BunifuProgressBar1.ValueByTransition = 0
        '
        'BunifuElipse1
        '
        Me.BunifuElipse1.ElipseRadius = 5
        Me.BunifuElipse1.TargetControl = Me
        '
        'Timer1
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.Azeroth_StartUp.My.Resources.Resources.logowow
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(461, 221)
        Me.ControlBox = False
        Me.Controls.Add(Me.BunifuProgressBar1)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Azeroth StartUp"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Black
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BunifuProgressBar1 As Bunifu.UI.WinForms.BunifuProgressBar
    Friend WithEvents BunifuElipse1 As Bunifu.Framework.UI.BunifuElipse
    Friend WithEvents Timer1 As Timer
End Class
