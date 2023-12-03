Imports Azeroth_StartUp.EnumModels
Public Class Form2
    Private Shared _isConfigDone As Boolean = False
    Private Shared _WorldExecuteName As String
    Private Shared _WorldExecuteFolder As String
    Private Shared _AuthExecuteName As String
    Private Shared _AuthExecuteFolder As String
    Private Shared _MySQLExecuteName As String
    Private Shared _MySQLExecuteFolder As String
    Public Shared Property IsConfigDone As Boolean
        Get
            Return _isConfigDone
        End Get
        Set(value As Boolean)
            _isConfigDone = value
        End Set
    End Property
    Public Shared Property WorldExecuteName As String
        Get
            Return _WorldExecuteName
        End Get
        Set(value As String)
            _WorldExecuteName = value
        End Set
    End Property
    Public Shared Property WorldExecuteFolder As String
        Get
            Return _WorldExecuteFolder
        End Get
        Set(value As String)
            _WorldExecuteFolder = value
        End Set
    End Property
    Public Shared Property AuthExecuteName As String
        Get
            Return _AuthExecuteName
        End Get
        Set(value As String)
            _AuthExecuteName = value
        End Set
    End Property
    Public Shared Property AuthExecuteFolder As String
        Get
            Return _AuthExecuteFolder
        End Get
        Set(value As String)
            _AuthExecuteFolder = value
        End Set
    End Property
    Public Shared Property MySQLExecuteName As String
        Get
            Return _MySQLExecuteName
        End Get
        Set(value As String)
            _MySQLExecuteName = value
        End Set
    End Property
    Public Shared Property MySQLExecuteFolder As String
        Get
            Return _MySQLExecuteFolder
        End Get
        Set(value As String)
            _MySQLExecuteFolder = value
        End Set
    End Property
    Private Sub BtnCLose_Click(sender As Object, e As EventArgs) Handles btnCLose.Click
        Close()
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isConfigDone = False
        Timer1.Enabled = True
        CheckMySQLProccess()
        ComboServerLists.Items.AddRange([Enum].GetNames(GetType(Cores)))
        ComboServerLists.SelectedIndex = Data.Settings.SelectedCore
    End Sub

    Private Sub ButtonCheckDB_Click(sender As Object, e As EventArgs) Handles ButtonCheckDB.Click
        If CheckMySqlConnection() = True Then
            MsgBox("connected")
            ButtonGoNext.Enabled = True 'Let's go
        Else
            MsgBox("not")
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CheckMySQLProccess()
    End Sub

    Private Sub ComboServerLists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboServerLists.SelectedIndexChanged
        If ComboServerLists.SelectedIndex = 0 Then
            TextWorld.Text = "acore_world"
            TextAuth.Text = "acore_auth"
        ElseIf ComboServerLists.SelectedIndex = 1 Then
            TextWorld.Text = "trinity_world"
            TextAuth.Text = "trinity_auth"
            'Need to do others
        Else
            TextWorld.Text = ""
            TextAuth.Text = ""
        End If
    End Sub

    Private Sub ButtonGoNext_Click(sender As Object, e As EventArgs) Handles ButtonGoNext.Click
        bunifuPages1.SetPage("Settings")
        Timer1.Stop()
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        bunifuPages1.SetPage("MySQL")
        Timer1.Start()
    End Sub

    Private Sub BunifuPages1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles bunifuPages1.SelectedIndexChanged
        ' update steps
        Select Case bunifuPages1.SelectedIndex
            Case 1
                c1.Enabled = True
                c1.Checked = True
                l1.ForeColor = Color.BlueViolet
            Case 2
                c2.Enabled = True
                c2.Checked = True
                l2.ForeColor = Color.BlueViolet
            Case 3
                c4.Enabled = True
                c4.Checked = True
                l4.ForeColor = Color.BlueViolet
            Case Else
                Exit Select
        End Select
    End Sub

    Private Sub ButtonBrowseWorld_Click(sender As Object, e As EventArgs) Handles ButtonBrowseWorld.Click
        Using open As New OpenFileDialog()
            open.FileName = "Select Game World.exe"
            open.FilterIndex = 1
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            open.Filter = "Application Files (.exe)|*.exe|All Files (*.*)|*.*"
            open.Multiselect = False
            If open.ShowDialog() = DialogResult.OK Then
                TextGameAuthPath.Text = IO.Path.GetFullPath(open.FileName)
                _WorldExecuteName = IO.Path.GetFileName(open.FileName)
                _WorldExecuteFolder = open.FileName.Substring(0, open.FileName.LastIndexOf("\"))
                _WorldExecuteFolder += "\"
            End If
        End Using
    End Sub

    Private Sub ButtonBrowseAuth_Click(sender As Object, e As EventArgs) Handles ButtonBrowseAuth.Click
        Using open As New OpenFileDialog()
            open.FileName = "Select Game Auth.exe"
            open.FilterIndex = 1
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            open.Filter = "Application Files (.exe)|*.exe|All Files (*.*)|*.*"
            open.Multiselect = False
            If open.ShowDialog() = DialogResult.OK Then
                TextGameWorldPath.Text = IO.Path.GetFullPath(open.FileName)
                _AuthExecuteName = IO.Path.GetFileName(open.FileName)
                _AuthExecuteFolder = open.FileName.Substring(0, open.FileName.LastIndexOf("\"))
                _AuthExecuteFolder += "\"
            End If
        End Using
    End Sub

    Private Sub ButtonMySQLPath_Click(sender As Object, e As EventArgs) Handles ButtonMySQLPath.Click
        Using open As New OpenFileDialog()
            open.FileName = "Select MySQL.exe"
            open.FilterIndex = 1
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            open.Filter = "Application Files (.exe)|*.exe|All Files (*.*)|*.*"
            open.Multiselect = False
            If open.ShowDialog() = DialogResult.OK Then
                TextMySQLPath.Text = IO.Path.GetFullPath(open.FileName)
                _MySQLExecuteName = IO.Path.GetFileName(open.FileName)
                _MySQLExecuteFolder = open.FileName.Substring(0, open.FileName.LastIndexOf("\"))
                _MySQLExecuteFolder += "\"
            End If
        End Using
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        'Check filled fields
        If String.IsNullOrEmpty(TextGameWorldPath.Text) Then
            TextGameWorldPath.Focus()
        ElseIf String.IsNullOrEmpty(TextGameAuthPath.Text) Then
            TextGameAuthPath.Focus()
        ElseIf String.IsNullOrEmpty(TextMySQLPath.Text) Then
            TextMySQLPath.Focus()
        Else
            bunifuPages1.SetPage("Finish")
        End If

    End Sub

    Private Sub ButtonExitSettings_Click(sender As Object, e As EventArgs) Handles ButtonExitSettings.Click
        _isConfigDone = True 'Save that all settings are done
        Close()
    End Sub
End Class