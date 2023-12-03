Imports System.Threading
Imports Microsoft.VisualBasic.Devices
Module GetCPUMemory
    Public TotalRamInMb As Integer
    Public TotalRamInGb As Integer
    Public UsedRam As Double
    Public UsedRamPercentage As Integer
    Public FinalResult As Single
    Public Async Sub GetCPUCounter()
        Dim processorTotal As New PerformanceCounter("Processor", "% Processor Time", "_Total")
        Dim firstValue As CounterSample = processorTotal.NextSample()
        Await Task.Delay(900) 'delay is required because first  call will always return 0
        Dim secondValue As CounterSample = processorTotal.NextSample()
        FinalResult = CounterSample.Calculate(firstValue, secondValue)
        Await Task.Delay(900)
        GetCPUCounter()
    End Sub
    Public Async Function GetCpuUsageForProcess() As Task(Of Double)
        Dim startTime = DateTime.UtcNow
        Dim startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime
        Await Task.Delay(500)
        Dim endTime = DateTime.UtcNow
        Dim endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime
        Dim cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds
        Dim totalMsPassed = (endTime - startTime).TotalMilliseconds
        Dim cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)
        Return cpuUsageTotal * 100
    End Function
    Public Function GetTotalRam() As String
        Dim CI = New ComputerInfo()
        Dim mem = ULong.Parse(CI.TotalPhysicalMemory.ToString())
        Dim Mb As Integer = Convert.ToInt16(mem / CDbl((1024 * 1024)))
        TotalRamInMb = Mb

        If Mb.ToString().Length <= 3 Then
            Return Mb.ToString() & " MB"
        Else
            Return (Convert.ToInt16(Mb / CDbl(1024))).ToString() & " GB"
            TotalRamInGb = Convert.ToInt16(Mb / CDbl(1024))
        End If
    End Function

    Public Async Sub GetUsedRam()
        Dim URam As Double
        Dim allProc As Process() = Process.GetProcesses()

        For Each proc In allProc
            URam += ((proc.PrivateMemorySize64) / CDbl((1024 * 1024)))
        Next

        UsedRam = URam
        UsedRamPercentage = (UsedRam * 100) / TotalRamInMb
        Await Task.Delay(900)
        GetUsedRam()
    End Sub
End Module
