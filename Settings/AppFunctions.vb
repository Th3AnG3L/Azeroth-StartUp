Imports Microsoft.Office.Interop.Outlook

Public Class AppFunctions
    Public Shared Sub CheckOutlookRun()
        If Process.GetProcessesByName("Outlook").Count <= 0 Then
            Using p As New Process
                p.StartInfo.FileName = "OUTLOOK"
                p.StartInfo.Arguments = "Outlook:Inbox"
                p.StartInfo.UseShellExecute = True
                p.Start()
            End Using
        End If
    End Sub
    Public Shared Function DetectOutlook() As Boolean
        If Process.GetProcessesByName("Outlook").Count <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Sub SendMail(ByVal Emails As String, ByVal CCcopy As String, ByVal Subject As String)
        Dim oEmail As MailItem = CType(New Application().CreateItem(OlItemType.olMailItem), MailItem)
        oEmail.Recipients.Add(Emails) 'Send TO
        oEmail.CC = CCcopy 'Send CC
        oEmail.Recipients.ResolveAll()
        oEmail.Subject = Subject
        oEmail.BodyFormat = OlBodyFormat.olFormatHTML
        oEmail.Body = "Hello user: "
        'oEmail.Importance = OlImportance.olImportanceNormal  'Request read email confirmation
        'oEmail.ReadReceiptRequested = True
        oEmail.Recipients.ResolveAll()
        oEmail.Save()
        oEmail.Display(False) 'Show the email message and allow for editing before sending
        oEmail.Send() 'You can automatically send the email without displaying it.
    End Sub
End Class
