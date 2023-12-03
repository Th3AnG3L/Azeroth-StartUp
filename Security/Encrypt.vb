Imports System.Security.Cryptography
Public Class Encrypt
    Public Shared Function EncryptPassword(password As String) As String
        If Data.Settings.DatabaseEncryption = 1 Then
            'Simple Encryption
            Dim PasswordBytes As Byte() = Text.Encoding.UTF8.GetBytes(password)
            Dim EncryptedPassword As String = Convert.ToBase64String(PasswordBytes)
            Return EncryptedPassword
        ElseIf Data.Settings.DatabaseEncryption = 2 Then
            'SHA1 Encryption
            Dim sha1 As New SHA1CryptoServiceProvider()
            Dim PasswordBytes As Byte() = Text.Encoding.UTF8.GetBytes(password)
            Dim EncryptedPassword As String = Text.Encoding.UTF8.GetString(sha1.ComputeHash(PasswordBytes))
            Return EncryptedPassword
        ElseIf Data.Settings.DatabaseEncryption = 3 Then
            'MD5 Encryption
            Dim md5 As New MD5CryptoServiceProvider
            Dim PasswordBytes As Byte() = Text.Encoding.UTF8.GetBytes(password)
            Dim EncryptedPassword As String = Text.Encoding.UTF8.GetString(md5.ComputeHash(PasswordBytes))
            Return EncryptedPassword
        Else
            Return password
        End If
    End Function
End Class
