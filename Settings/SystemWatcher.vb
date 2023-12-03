Imports System.Dynamic
Imports System.IO
Imports System.Management
Imports System.Threading

Public Class SystemWatcher
    Public Shared Function TotalRam() As Integer
        Dim totalRamuse As Integer = 0
        Dim wql As New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        Dim searcher As New ManagementObjectSearcher(wql)
        Dim results As ManagementObjectCollection = searcher.[Get]()
        Dim res As Double

        For Each result As ManagementObject In results.Cast(Of ManagementObject)()
            res = Convert.ToDouble(result("TotalVisibleMemorySize"))
            Dim fres As Double = Math.Round((res / 1024.0R))
            totalRamuse = Convert.ToInt32(fres)
        Next
        Return totalRamuse
    End Function

    Public Shared Function MachineCpuUtilization() As Integer
        'old
        'Dim cpuCounter = New PerformanceCounter("Processor Information", "% Processor Utility", "_Total", Environment.MachineName)
        'cpuCounter.NextValue()
        'Thread.Sleep(1000)
        'Return CInt(cpuCounter.NextValue())
        'New method
        Using cpuCounter As New PerformanceCounter("Processor Information", "% Processor Utility", "_Total", Environment.MachineName)
            Dim firstvalue As CounterSample = cpuCounter.NextSample()
            Thread.Sleep(1000)
            Dim secondvalue As CounterSample = cpuCounter.NextSample()
            Dim cpuUsage As String = CounterSample.Calculate(firstvalue, secondvalue).ToString("0.0")
            Return CInt(cpuUsage)
        End Using
    End Function

    Public Shared Function CurentPcRamUsage() As Integer
        Dim curentram As Integer = 0
        Dim wql As New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        Dim searcher As New ManagementObjectSearcher(wql)
        Dim results As ManagementObjectCollection = searcher.[Get]()
        Dim res As Double

        For Each result As ManagementObject In results.Cast(Of ManagementObject)()
            res = Convert.ToDouble(result("FreePhysicalMemory"))
            Dim fres As Double = Math.Round(res / 1024.0R)
            curentram = Convert.ToInt32(fres)
        Next

        Return curentram
    End Function

    Public Shared Function ApplicationRuning(ByVal ApplicationName As String) As EnumModels.ServerStatus
        Dim pname As Process() = Process.GetProcessesByName(ApplicationName)

        If pname.Length <= 0 Then
            Return EnumModels.ServerStatus.NotRunning
        Else
            Return EnumModels.ServerStatus.Running
        End If
    End Function

    Public Shared Function ApplicationKill(ByVal ApplicationName As String) As EnumModels.ServerStatus
        For Each processuse In Process.GetProcessesByName(ApplicationName)
            Try
                processuse.Kill()
                processuse.WaitForExit()
                Return EnumModels.ServerStatus.NotRunning
            Catch __unusedException1__ As Exception
                Return EnumModels.ServerStatus.Running
            End Try
        Next
        Return EnumModels.ServerStatus.Running
    End Function

    Public Shared Sub ApplicationStart(ByVal ApplicationDir As String, ByVal ApplicationName As String, ByVal HideWindw As Boolean)
        Try
            Using p As New Process
                p.StartInfo.WorkingDirectory = ApplicationDir
                p.StartInfo.FileName = ApplicationName
                p.StartInfo.UseShellExecute = True
                If HideWindw = False Then
                    p.StartInfo.CreateNoWindow = False
                    p.Start()
                Else
                    p.StartInfo.CreateNoWindow = True
                    p.Start()
                End If
            End Using
        Catch ex As Exception
            Data.Message = ex.Message
        End Try
    End Sub

    Public Shared Function ApplicationRamUsage(ByVal ApplicationName As String) As Integer
        If ApplicationRuning(ApplicationName) = EnumModels.ServerStatus.Running Then
            Try
                Using PC As New PerformanceCounter With {
                        .CategoryName = "Process",
                        .CounterName = "Working Set - Private",
                        .InstanceName = ApplicationName
                    }
                    Dim memsize As Integer = Convert.ToInt32(PC.NextValue()) / CInt((1024 * 1024))
                    PC.Close()
                    PC.Dispose()
                    Return memsize
                End Using
            Catch
                Return 0
            End Try
        End If
        Return 0
    End Function

    Public Shared Function ApplicationCPUUsage(ByVal ApplicationName As String) As Integer
        If ApplicationRuning(ApplicationName) = EnumModels.ServerStatus.Running Then
            Try
                Using cpuCounter As New PerformanceCounter("Process", "% Processor Time", ApplicationName, Environment.MachineName)
                    Dim firstvalue As CounterSample = cpuCounter.NextSample()
                    Thread.Sleep(1000)
                    Dim secondvalue As CounterSample = cpuCounter.NextSample()
                    Dim cpuUsage As String = CounterSample.Calculate(firstvalue, secondvalue).ToString("0.0")
                    Dim memsize As Integer = Convert.ToInt32(cpuUsage / Environment.ProcessorCount)
                    Return CInt(memsize)
                End Using
            Catch ex As Exception
                Return 0
            End Try
        End If
        Return 0
    End Function

    Public Shared Function CheckIfRunning(ByVal ProgramName As String) As Boolean
        Try
            If Process.GetProcessesByName(ProgramName).Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
