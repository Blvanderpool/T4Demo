Imports T4.MANAGER.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.ComponentModel
Imports System.IO

Public Class UserMgtWebService
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sNewPassword = HttpContext.Current.Request.QueryString("newpass")
        Dim sOldPassword = HttpContext.Current.Request.QueryString("oldpass")

        Dim xOutput As String = SetNewPassword(sNewPassword, sOldPassword)
        Response.Write(xOutput)

    End Sub

    <WebMethod()> _
    Public Function SetNewPassword(newpass As String, oldpass As String) As String

        Dim nUserNo As Integer = 3          'NO LOGIN: so always uses UserNo  3  

        Dim connstring As String = System.Configuration.ConfigurationManager.ConnectionStrings("T4ConnectionString").ConnectionString
        Dim sqlConnect As New SqlConnection(connstring)

        Dim cmd As New SqlCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "user_change_password"

        cmd.Parameters.Add("@userno", SqlDbType.Int).Value = CType(nUserNo, Integer)
        cmd.Parameters.Add("@newpassword", SqlDbType.VarChar).Value = newpass.Trim()
        cmd.Parameters.Add("@oldpassword", SqlDbType.VarChar).Value = oldpass.Trim()
        cmd.Connection = sqlConnect

        Try
            sqlConnect.Open()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex

        Finally
            sqlConnect.Close()
            sqlConnect.Dispose()
        End Try

        Return "DONE"
    End Function


    <WebMethod()> _
    Public Function SetPass(newpassword As String, oldpassword As String) As String
        ' Dim i As Integer = DumpFile(newpassword + " " + oldpassword)
        Return "Saving: " + newpassword + " " + oldpassword
    End Function



    Public Function DumpFile(ByVal ltext As String) As Integer

        ' Set a variable to the My Documents path.
        Dim mydocpath As String = Environment.GetFolderPath("S:\BRENT")   ' Environment.SpecialFolder.Desktop)

        ' Write the string array to a new file named "WriteLines.txt".
        Using outputFile As New StreamWriter(mydocpath & Convert.ToString("\0000VANDERDUMP.txt"))
            'For Each line As String In lines
            outputFile.WriteLine("Hello " + ltext)
            'Next
        End Using

        Return 1
    End Function


End Class


