Imports System.IO

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BunifuProgressBar1.Value = 0
        BunifuProgressBar1.Minimum = 0
        BunifuProgressBar1.Maximum = 100
        BunifuProgressBar1.MinimumValue = 0
        BunifuProgressBar1.MaximumValue = 100

        If File.Exists($"{Directory.GetCurrentDirectory()}\Settings.xml") Then
            Data.LoadSettings() 'Load setting files then
            Timer1.Enabled = True
        Else
            Form2.ShowDialog() 'Form settings called
            If Not Application.OpenForms().OfType(Of Form2).Any Then
                If Form2.IsConfigDone = True Then
                    Data.LoadSettings() 'Create files then
                    Timer1.Enabled = True
                    Data.Settings.MySQLServerHost = Form2.TextHostname.Text
                    Data.Settings.MySQLServerUser = Form2.TextUsername.Text
                    Data.Settings.MySQLServerPassword = Form2.TextPassword.Text
                    Data.Settings.MySQLServerPort = Form2.TextPort.Text
                    Data.Settings.WorldDatabase = Form2.TextWorld.Text
                    Data.Settings.AuthDatabase = Form2.TextAuth.Text
                    Data.Settings.SelectedCore = Form2.ComboServerLists.SelectedIndex
                    Data.Settings.AuthExecutableName = Form2.AuthExecuteName
                    Data.Settings.WorldExecutableName = Form2.WorldExecuteName
                    Data.Settings.AuthExecutableLocation = Form2.AuthExecuteFolder
                    Data.Settings.WorldExecutableLocation = Form2.WorldExecuteFolder
                    Data.Settings.MySQLExecutableName = Form2.MySQLExecuteName
                    Data.Settings.MySQLExecutablePath = Form2.MySQLExecuteFolder
                    Data.SaveSettings() 'Save and reload settings
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim value As Integer = New Random().Next(0, 10)
        BunifuProgressBar1.Value += 5

        If BunifuProgressBar1.Value = BunifuProgressBar1.Maximum Then
            Timer1.Stop()
            Me.Close()
        End If
    End Sub
End Class
