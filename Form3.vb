Imports System.ComponentModel
Imports System.IO
Imports System.Management
Imports System.Threading
Imports Azeroth_StartUp.EnumModels
Imports Bunifu.UI.WinForms

Public Class Form3


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False
        Data.LoadSettings()
        TimerWacher.Start()
        TimerApplications.Start()
        LabelServerTime.Text = Date.Now.ToString("dd/MM/yyyy - HH:mm:ss") ' 24 hour time
    End Sub
    Private Sub PutDetails()

    End Sub
    Private Sub TimerWacher_Tick(sender As Object, e As EventArgs) Handles TimerWacher.Tick
        'LabelCPUUsage.Text = Convert.ToInt32(PerfCounterRAM.NextValue()).ToString() + "Mb"
        'Dim totalmemory = String.Format("{0} MB", Math.Round(My.Computer.Info.TotalPhysicalMemory / (1024 * 1024)), 2).ToString()
        'Dim usedmemory = String.Format("{0} MB", Math.Round(My.Computer.Info.AvailablePhysicalMemory / (1024 * 1024)), 2).ToString()
        'LabelRAMUsage.Text = usedmemory + "/" + totalmemory

        'Dim fcpu As Single = PerfCounterCPU.NextValue()
        'Dim fram As Single = PerfCounterRAM.NextValue()
        'Dim framtotal As Single = PerfCounterRamTotal.NextValue()

        'PCResorcePbarCPU.Value = CInt(fcpu)
        'PCResorcePbarRAM.Value = CInt(fram)
        'LabelCPUUsage.Text = String.Format("{0:0.00}%", fcpu)
        'LabelRAMUsage.Text = String.Format("{0:0.00}%", fram)

        Try
            Dim PCResorceUsageThread As New Thread(Sub()
                                                       Dim CurrentRamUsage As Integer = SystemWatcher.TotalRam() - SystemWatcher.CurentPcRamUsage()
                                                       'Start Global CPU & Memory
                                                       PCResorcePbarRAM.Maximum = SystemWatcher.TotalRam()
                                                       PCResorcePbarRAM.Value = CurrentRamUsage
                                                       PCResorcePbarCPU.Value = SystemWatcher.MachineCpuUtilization()

                                                       Dim xy = String.Format("{0} MB", Math.Round(My.Computer.Info.TotalPhysicalMemory / (1024 * 1024)), 2).ToString 'ok
                                                       Dim qq As String = String.Format("{0} MB", CurrentRamUsage).ToString.Replace(".", "") 'ok
                                                       LabelRAMUsage.Text = qq + " / " + xy 'ok
                                                       LabelCPUUsage.Text = String.Format("{0} %", SystemWatcher.MachineCpuUtilization()).ToString
                                                       'Start World Server CPU & Memory
                                                       PCWorldPbarRAM.Maximum = CurrentRamUsage
                                                       PCWorldPbarRAM.Value = SystemWatcher.ApplicationRamUsage("worldserver")
                                                       PCWorldPbarCPU.Value = SystemWatcher.ApplicationCPUUsage("worldserver")

                                                       Dim aa = String.Format("{0} MB", Math.Round(My.Computer.Info.TotalPhysicalMemory / (1024 * 1024)), 2).ToString 'ok
                                                       Dim bb As String = String.Format("{0} MB", SystemWatcher.ApplicationRamUsage("worldserver")).ToString.Replace(".", "") 'ok
                                                       LabelWorldRAMUsage.Text = bb + " / " + aa 'ok
                                                       LabelWorldCPUUsage.Text = String.Format("{0} %", SystemWatcher.ApplicationCPUUsage("worldserver")).ToString

                                                       'Start Auth Server CPU & Memory
                                                       PCLoginPbarRAM.Maximum = CurrentRamUsage
                                                       PCLoginPbarRAM.Value = SystemWatcher.ApplicationRamUsage("authserver")
                                                       PCLoginPbarCPU.Value = SystemWatcher.ApplicationCPUUsage("authserver")

                                                       Dim cc = String.Format("{0} MB", Math.Round(My.Computer.Info.TotalPhysicalMemory / (1024 * 1024)), 2).ToString 'ok
                                                       Dim dd As String = String.Format("{0} MB", SystemWatcher.ApplicationRamUsage("authserver")).ToString.Replace(".", "") 'ok
                                                       LabelAuthRAMUsage.Text = dd + " / " + cc 'ok
                                                       LabelAuthCPUUsage.Text = String.Format("{0} %", SystemWatcher.ApplicationCPUUsage("authserver")).ToString
                                                   End Sub)
            PCResorceUsageThread.Start()
        Catch
        End Try

    End Sub

    Private Sub Form3_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        TimerApplications.Stop()
        TimerWacher.Stop()
        DateTime.Stop()
    End Sub

    Private Sub TimerApplications_Tick(sender As Object, e As EventArgs) Handles TimerApplications.Tick
        PictureWorldServer.Image = If(SystemWatcher.CheckIfRunning("worldserver"), My.Resources.circle_green2, My.Resources.circle_red2)
        PictureAuthServer.Image = If(SystemWatcher.CheckIfRunning("authserver"), My.Resources.circle_green2, My.Resources.circle_red2)
        PictureMySQL.Image = If(SystemWatcher.CheckIfRunning("mysqld"), My.Resources.circle_green2, My.Resources.circle_red2)
        If SystemWatcher.CheckIfRunning("worldserver") Then
            ButtonStopWorldServer.Enabled = True
            ButtonStartWorldServer.Enabled = False
            Data.Server.WorldServerStatus = ServerStatus.Running
        Else
            ButtonStopWorldServer.Enabled = False
            ButtonStartWorldServer.Enabled = True
            Data.Server.WorldServerStatus = ServerStatus.NotRunning
        End If
        If SystemWatcher.CheckIfRunning("authserver") Then
            ButtonStopAuthServer.Enabled = True
            ButtonStartAuthServer.Enabled = False
            Data.Server.AuthServerStatus = ServerStatus.Running
        Else
            ButtonStopAuthServer.Enabled = False
            ButtonStartAuthServer.Enabled = True
            Data.Server.AuthServerStatus = ServerStatus.NotRunning
        End If
        If SystemWatcher.CheckIfRunning("mysqld") Then
            ButtonStopMySQL.Enabled = True
            ButtonStartMySQL.Enabled = False
            Data.Server.MysqlServerStatus = ServerStatus.Running
        Else
            ButtonStopMySQL.Enabled = False
            ButtonStartMySQL.Enabled = True
            Data.Server.MysqlServerStatus = ServerStatus.NotRunning
        End If
    End Sub

    Private Sub BtnCLose_Click(sender As Object, e As EventArgs) Handles btnCLose.Click
        Application.Exit()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles panel2.Paint
        ControlPaint.DrawBorder(e.Graphics, panel2.ClientRectangle, Color.FromArgb(78, 46, 207), ButtonBorderStyle.Solid)
    End Sub

    Private Sub PanelHeader_Paint(sender As Object, e As PaintEventArgs) Handles panelHeader.Paint
        ControlPaint.DrawBorder(e.Graphics, panelHeader.ClientRectangle, Color.FromArgb(78, 46, 207), ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles panel1.Paint
        ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle, Color.FromArgb(78, 46, 207), ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles panel4.Paint
        ControlPaint.DrawBorder(e.Graphics, panel4.ClientRectangle, Color.FromArgb(78, 46, 207), ButtonBorderStyle.Solid)
    End Sub

    Private Sub MainPanel_Paint(sender As Object, e As PaintEventArgs) Handles MainPanel.Paint
        ControlPaint.DrawBorder(e.Graphics, MainPanel.ClientRectangle, Color.FromArgb(78, 46, 207), ButtonBorderStyle.Solid)
    End Sub

    Private Sub DateTime_Tick(sender As Object, e As EventArgs) Handles DateTime.Tick
        LabelServerTime.Text = Date.Now.ToString("dd/MM/yyyy - HH:mm:ss") ' 24 hour time
    End Sub

    Private Sub ButtonUpdateSoftware_Click(sender As Object, e As EventArgs) Handles ButtonUpdateSoftware.Click
        'Custom Notification PopUp
        BunifuSnackbar1.Show(Me, "Snackbar on the top left side of the form",
            BunifuSnackbar.MessageTypes.Information, 3000, "",
            BunifuSnackbar.Positions.BottomRight,
            BunifuSnackbar.Hosts.FormOwner)
    End Sub

    Private Sub ButtonStartMySQL_Click(sender As Object, e As EventArgs) Handles ButtonStartMySQL.Click
        If Data.Server.MysqlServerStatus = ServerStatus.NotRunning Then
            SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutablePath, Data.Settings.MySQLExecutableName, False)
        End If
    End Sub

    Private Sub ButtonStopMySQL_Click(sender As Object, e As EventArgs) Handles ButtonStopMySQL.Click
        If Data.Server.MysqlServerStatus = ServerStatus.Running Then
            SystemWatcher.ApplicationKill(Data.Settings.MySQLExecutableName.Replace(".exe", ""))
        End If
    End Sub

    Private Sub ButtonStartWorldServer_Click(sender As Object, e As EventArgs) Handles ButtonStartWorldServer.Click
        'MsgBox(Data.Server.WorldServerStatus = ServerStatus.NotRunning)
        If Data.Server.WorldServerStatus = ServerStatus.NotRunning Then
            SystemWatcher.ApplicationStart(Data.Settings.WorldExecutableLocation, Data.Settings.WorldExecutableName, False)
        End If
    End Sub

    Private Sub ButtonStopWorldServer_Click(sender As Object, e As EventArgs) Handles ButtonStopWorldServer.Click
        'MsgBox(Data.Server.WorldServerStatus = ServerStatus.Running)
        If Data.Server.WorldServerStatus = ServerStatus.Running Then
            SystemWatcher.ApplicationKill(Data.Settings.WorldExecutableName.Replace(".exe", ""))
        End If
    End Sub

    Private Sub ButtonStartAuthServer_Click(sender As Object, e As EventArgs) Handles ButtonStartAuthServer.Click
        If Data.Server.AuthServerStatus = ServerStatus.NotRunning Then
            SystemWatcher.ApplicationStart(Data.Settings.AuthExecutableLocation, Data.Settings.AuthExecutableName, False)
        End If
    End Sub

    Private Sub ButtonStopAuthServer_Click(sender As Object, e As EventArgs) Handles ButtonStopAuthServer.Click
        If Data.Server.AuthServerStatus = ServerStatus.Running Then
            SystemWatcher.ApplicationKill(Data.Settings.AuthExecutableName.Replace(".exe", ""))
        End If
    End Sub
End Class