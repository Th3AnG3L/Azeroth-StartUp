Imports System.ServiceProcess
Imports System.Management
Imports MySqlConnector

Module Settings_MySQL
    Public Function CheckMySqlConnection() As Boolean
        Try
            Dim serverName As String = Form2.TextHostname.Text
            Dim userName As String = Form2.TextUsername.Text
            Dim port As String = Form2.TextPort.Text
            Dim password As String = Form2.TextPassword.Text
            Dim dbName As String = Form2.TextWorld.Text

            Dim conStr As String = "Server=" + serverName + ";Uid=" + userName + ";Database=" + dbName + ";Port=" + port + ";Pwd=" + password + ";"
            Using con As New MySqlConnection(conStr)
                con.Open()
                Return True
            End Using
        Catch ex As MySqlException
            Select Case ex.Number
                Case 0 'Invalid hostname/port
                    MessageBox.Show("Cannot connect to server, try different settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case 1045 'Invalid username/password
                    MessageBox.Show("Invalid username/password, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case 1042 'Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                    MessageBox.Show("Invalid server/port, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case Else
                    Exit Select
            End Select
            Return False
        End Try
    End Function
    Public Function CheckMySqlConnectionWorldDB() As Boolean
        Try
            Dim serverName As String = Data.Settings.MySQLServerHost
            Dim userName As String = Data.Settings.MySQLServerUser
            Dim port As String = Data.Settings.MySQLServerPort
            Dim password As String = Data.Settings.MySQLServerPassword
            Dim dbName As String = Data.Settings.WorldDatabase

            Dim conStr As String = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";"
            Using con = New MySqlConnection(conStr)
                con.Open()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CheckMySqlConnectionAuth() As Boolean
        Try
            Dim serverName As String = Data.Settings.MySQLServerHost
            Dim userName As String = Data.Settings.MySQLServerUser
            Dim port As String = Data.Settings.MySQLServerPort
            Dim password As String = Data.Settings.MySQLServerPassword
            Dim dbName As String = Data.Settings.AuthDatabase

            Dim conStr As String = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";"
            Using con = New MySqlConnection(conStr)
                con.Open()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CheckMySqlConnectionAll() As Boolean
        Try
            Dim serverName As String = Data.Settings.MySQLServerHost
            Dim userName As String = Data.Settings.MySQLServerUser
            Dim port As String = Data.Settings.MySQLServerPort
            Dim password As String = Data.Settings.MySQLServerPassword
            Dim dbName As String = Data.Settings.WorldDatabase

            Dim conStr As String = "Server=" + serverName + ";Uid=" + userName + ";Database=" + dbName + ";Port=" + port + ";Pwd=" + password + ";"
            Using con As New MySqlConnection(conStr)
                con.Open()
                Return True
            End Using
        Catch ex As MySqlException
            Select Case ex.Number
                Case 0 'Invalid hostname/port
                    MessageBox.Show("Cannot connect to server, try different settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case 1045 'Invalid username/password
                    MessageBox.Show("Invalid username/password, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case 1042 'Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                    MessageBox.Show("Invalid server/port, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case Else
                    Exit Select
            End Select
            Return False
        End Try
    End Function
    Public Sub CheckMySQLProccess()
        Dim pname As Process() = Process.GetProcessesByName("mysqld")
        If pname.Length = 0 Then
            Form2.PictureMySQLStatus.Image = My.Resources.circle_red
            Form2.LabelMySQLStatus.Text = "MySQL Server is not running…"
            Form2.LabelMySQLStatus.ForeColor = Color.OrangeRed
            Form2.LabelMySQLON.Text = "OFFLINE"
            Form2.LabelMySQLON.ForeColor = Color.OrangeRed
            'Disable do stuff
            Form2.TextHostname.Enabled = False
            Form2.TextUsername.Enabled = False
            Form2.TextPort.Enabled = False
            Form2.TextPassword.Enabled = False
            Form2.TextAuth.Enabled = False
            Form2.TextWorld.Enabled = False
            Form2.ButtonCheckDB.Enabled = False
        Else
            Form2.PictureMySQLStatus.Image = My.Resources.circle_green
            Form2.LabelMySQLStatus.Text = "MySQL Server is running…"
            Form2.LabelMySQLStatus.ForeColor = Color.Green
            Form2.LabelMySQLON.Text = "ONLINE"
            Form2.LabelMySQLON.ForeColor = Color.Green
            'Allow do stuff
            Form2.TextHostname.Enabled = True
            Form2.TextUsername.Enabled = True
            Form2.TextPort.Enabled = True
            Form2.TextPassword.Enabled = True
            Form2.TextAuth.Enabled = True
            Form2.TextWorld.Enabled = True
            Form2.ButtonCheckDB.Enabled = True
        End If
    End Sub
    Public Sub CheckMySQLRunningService()
        Try
            Dim mo As New ManagementObject("Win32_Service.Name=’MySQLD’")
            Form2.LabelMySQLStatus.Text = "Busy…"
            'Check if MySQL Service is installed. If not it will close the form.
            Try
                mo.Get()
            Catch ex As Exception
                Form2.LabelMySQLStatus.Text = "Need to install MySQL Server"
            End Try

            'Check status of MySQL Server
            'If the service is running all is fine
            'Else it will wait for the Server to run, or attempt to start the Server
            'The status will be updated in the ToolStripStatusLabel tsStatus

            'ServiceControllerStatus Meanings
            '1 = Stopped – The Service is not running.
            '2 = StartPending – The Service is starting.
            '3 = StopPending – The Service is stopping.
            '4 = Running – The Service is running.
            '5 = ContinuePending – The Service continue is pending.
            '6 = PausePending – The Service pause is pending.
            '7 = Paused – The service is paused.
            Dim sc As New ServiceController("MySQL")
            Select Case sc.Status
                Case 1
                    Form2.LabelMySQLStatus.Text = "MySQL Server is not running, please wait…"
                    sc.Start()
                    Form2.LabelMySQLStatus.Text = "Starting MySQL Server, please wait…"
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Form2.LabelMySQLStatus.Text = "Ready"
                Case 2
                    Form2.LabelMySQLStatus.Text = "MySQL Server is starting, please wait…"
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Form2.LabelMySQLStatus.Text = "Ready"
                Case 3
                    Form2.LabelMySQLStatus.Text = "MySQL Server is stopping, please wait…"
                    sc.WaitForStatus(ServiceControllerStatus.Stopped)
                    Form2.LabelMySQLStatus.Text = "Starting MySQL Server, please wait…"
                    sc.Start()
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Form2.LabelMySQLStatus.Text = "Ready"
                Case 4
                    Form2.LabelMySQLStatus.Text = "Ready"
                Case 5, 6, 7
                    Form2.LabelMySQLStatus.Text = "MySQL Server is stopping, please wait…"
                    sc.Stop()
                    sc.WaitForStatus(ServiceControllerStatus.Stopped)
                    Form2.LabelMySQLStatus.Text = "Starting MySQL Server, please wait…"
                    sc.Start()
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Form2.LabelMySQLStatus.Text = "Ready"
                Case Else
                    Exit Select
            End Select
        Catch ex As Exception
            Form2.LabelMySQLStatus.Text = "MySQL Server Error"
        End Try
    End Sub
End Module
