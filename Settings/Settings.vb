Imports System.IO
Imports System.Xml.Serialization

Public Class Data
    Public Shared Message As String = String.Empty
    Public Shared SettingsDataFile As String = $"{Directory.GetCurrentDirectory()}\Settings.xml"
    Public Shared Settings As New Settings()

    Public Shared Sub CreateSettingsFile(ByVal PopulateSettingsData As Boolean)
        Try
            If Not File.Exists(SettingsDataFile) Then
                Dim createFile = File.Create(SettingsDataFile)
                createFile.Close()
            End If

            If PopulateSettingsData = True Then
                Settings.MySQLServerHost = "localhost"
                Settings.MySQLServerUser = "root"
                Settings.MySQLServerPassword = ""
                Settings.MySQLServerPort = "3306"
                Settings.WorldDatabase = "world"
                Settings.AuthDatabase = "auth"
                Settings.MySQLExecutableName = "mysqld"
                Settings.MySQLExecutablePath = ""
                Settings.WorldExecutableLocation = ""
                Settings.AuthExecutableLocation = ""
                Settings.WorldExecutableName = "WorldServer"
                Settings.AuthExecutableName = "AuthServer"
                Settings.ServerCrash = False
                Settings.NotificationSound = True
                Settings.StayInTray = False
                Settings.RunWithWindows = False
                Settings.RunServerWithWindows = False
                Settings.SelectedCore = EnumModels.Cores.TrinityCore
                Settings.DatabaseEncryption = False
                WriteData(Settings, SettingsDataFile)
            End If
        Catch ex As Exception
            Message = ex.Message
        End Try

    End Sub

    Public Shared Sub SaveSettings()
        Try
            If File.Exists(SettingsDataFile) Then
                WriteData(Settings, SettingsDataFile)
                Settings = ReaderData(SettingsDataFile) 'Reload the strings
            End If
        Catch ex As Exception
            Message = ex.Message
        End Try
    End Sub

    Public Shared Sub LoadSettings()
        Try
            If File.Exists(SettingsDataFile) Then
                Settings = ReaderData(SettingsDataFile)
            Else
                CreateSettingsFile(True)
                Settings = ReaderData(SettingsDataFile)
            End If
        Catch ex As Exception
            Message = ex.Message
        End Try
    End Sub

    Private Shared Sub WriteData(ByVal o As Object, ByVal fileName As String)
        Dim serializer As New XmlSerializer(o.[GetType]())
        Dim writer As TextWriter = New StreamWriter(fileName)
        serializer.Serialize(writer, o)
        writer.Close()
    End Sub

    Private Shared Function ReaderData(ByVal fileName As String) As Settings
        Dim serializer As New XmlSerializer(GetType(Settings))
        Dim reader As New FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)
        Settings = CType(serializer.Deserialize(reader), Settings)
        reader.Close()
        Return Settings
    End Function

    Public Class Server
        Public Shared WorldServerStatus As EnumModels.ServerStatus
        Public Shared AuthServerStatus As EnumModels.ServerStatus
        Public Shared MysqlServerStatus As EnumModels.ServerStatus
    End Class
End Class


Public Class Settings
    Public WorldDatabase As String
    Public AuthDatabase As String
    Public MySQLServerHost As String
    Public MySQLServerUser As String
    Public MySQLServerPassword As String
    Public MySQLServerPort As String
    Public MySQLExecutableName As String
    Public MySQLExecutablePath As String
    Public WorldExecutableLocation As String
    Public AuthExecutableLocation As String
    Public WorldExecutableName As String
    Public AuthExecutableName As String
    Public ServerCrash As Boolean
    Public NotificationSound As Boolean
    Public StayInTray As Boolean
    Public RunWithWindows As Boolean
    Public RunServerWithWindows As Boolean
    Public DatabaseEncryption As Boolean
    Public SelectedCore As EnumModels.Cores
End Class
