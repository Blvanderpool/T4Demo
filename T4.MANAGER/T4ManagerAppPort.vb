Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Reflection
Imports System.Data.SqlTypes

Namespace Data
    Public Class T4ManagerAppPort
        Inherits Data.BaseAppPort
#Region "Constructors"
        ''' Last Modified: Nov. 23, 2009
        ''' <summary>
        ''' A constructor taking 2 arguments
        ''' </summary>
        ''' <param name="connectionString">a connection string in string format</param>
        ''' <param name="commandTimeOut">wait time in seconds before terminating the attempt to execute 
        ''' a command and generating an error.</param>
        ''' <remarks></remarks>
        Sub New(ByVal connectionString As String, ByVal commandTimeOut As Integer)
            MyBase.New(connectionString, commandTimeOut)

        End Sub
        Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)

        End Sub
#End Region


        ''' Last Modified: 
        ''' <summary>
        ''' This method is used to log a user in the system.
        ''' </summary>
        ''' <param name="UserName"> param: 'UserName'</param>
        ''' <param name="UserPassword"> param: 'UserPassword'</param>
        ''' <returns>an object of DataTable containing these columns:
        ''' userno int32, 
        ''' username nvarchar50
        ''' fullname nvarchar50
        ''' usertype nvarchar50
        ''' </returns>
        ''' <remarks></remarks>
        Function ValidateUserLogin(ByVal UserName As String, ByVal UserPassword As String) As DataTable

            Dim dt As New DataTable

            Try

                Me.SettingUpSQLSelectCommand("user_validate_login")

                'Add parameter(s) to the command object
                Me.SQLSelectCommand.Parameters.Add(CreateParameter("username", UserName, DbType.String))
                Me.SQLSelectCommand.Parameters.Add(CreateParameter("userpass", UserPassword, DbType.String))

                Dim dtDataAdapter As New SqlDataAdapter(Me.SQLSelectCommand)
                dtDataAdapter.Fill(dt)

            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
                'unreachable code in this catch block after Throw statement
            End Try

            Return dt

        End Function



        Function UpdateUserPassword(ByVal UserNo As Integer, ByVal NewPassword As String, ByVal OldPassword As String, ByVal sqlConnect As SqlConnection) As Boolean
            Dim bResult As Boolean = False
            ' Dim connstr As String = System.Configuration.ConfigurationManager.ConnectionStrings("T4ConnectionString").ConnectionString
            'Dim objApp As New T4ManagerAppPort(connstr)

            ' Dim connectionSql As New SqlConnection(connstr)
            ''UpdateUserPassword
            'Dim nUserNo As Integer = 3
            'Dim sNewPassword As String = "VANDEB"
            'Dim sOldPassword As String = "VANDEB20147"
            'Dim sUserName As String = "blvanderpool"



            ' If Not String.IsNullOrEmpty(hdnParamNew.Value) Then
            'sNewPassword = hdnParamNew.Value
            'End If
            ' If Not String.IsNullOrEmpty(hdnParamOld.Value) Then
            'sOldPassword = hdnParamOld.Value
            ' End If
            '==================================================================================

            'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
            'Dim con As New SqlConnection(strConnString)
            ' Dim sqlConnect As New SqlConnection(connstr)

            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "user_change_password"
            cmd.Parameters.Add("@userno", SqlDbType.Int).Value = UserNo

            'cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = sUserName.Trim()
            cmd.Parameters.Add("@newpassword", SqlDbType.VarChar).Value = NewPassword.Trim()
            cmd.Parameters.Add("@oldpassword", SqlDbType.VarChar).Value = OldPassword.Trim()
            cmd.Connection = sqlConnect
            Try
                sqlConnect.Open()
                'cmd.ExecuteNonQuery()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                'If (rowsAffected > 0) Then
                bResult = True
                'Label1.Text = "User Record Changed | " & sNewPassword & " | " & sOldPassword & " = " & rowsAffected.ToString
                'End If

            Catch ex As Exception
                Throw ex
            Finally
                sqlConnect.Close()
                sqlConnect.Dispose()
            End Try

            ' Return bResult


            Return bResult
        End Function

        ''' Last Modified: August 10, 2017
        ''' <summary>
        ''' This method is used to change user password.
        ''' </summary>
        ''' <param name="UserNo"></param>
        ''' <param name="NewPassword"></param>
        ''' <param name="OldPassword"></param>
        ''' <returns>an object of DataTable containing these columns:
        ''' (1) 
        ''' (8) Subsystem </returns>
        ''' <remarks></remarks>
        Function ModifyUserPassword(ByVal UserNo As Integer, ByVal NewPassword As String, ByVal OldPassword As String, ByVal sqlConnect As SqlConnection, ByVal sqlTrans As SqlTransaction) As Boolean

            Dim blnResult As Boolean = True
            Try

                Me.SQLUpdateCommand.Connection = sqlConnect
                Me.SQLUpdateCommand.Transaction = sqlTrans
                Me.SettingUpSQLUpdateCommand("user_change_password")

                Dim iUserNo As Int32 = CType(UserNo, Int32)
                'Add parameter(s) to the command object
                Me.SQLUpdateCommand.Parameters.Add(CreateParameter("userno", iUserNo, DbType.Int32))
                Me.SQLUpdateCommand.Parameters.Add(CreateParameter("newpassword", NewPassword, DbType.String))
                Me.SQLUpdateCommand.Parameters.Add(CreateParameter("oldpassword", OldPassword, DbType.String))

                'If it is close, open it.
                Me.OpenSqlConnection(Me.SQLUpdateCommand)

                ' If (SQLUpdateCommand.ExecuteNonQuery() <= 0) Then blnResult = False
                If (SQLUpdateCommand.ExecuteNonQuery() <= 0) Then
                    Debug.Print("COMMIT CHANGE PSWD")
                    blnResult = False
                End If

            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
                'unreachable code in this catch block after Throw statement
            End Try

            Return blnResult

        End Function


        ''' Last Modified: March 16, 2011
        ''' <summary>
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTicketsAll() As DataTable
            Dim dt As New DataTable

            Try
                'ensure proper conversions to corresponding DbType

                Me.SettingUpSQLSelectCommand("GetTickets")

                Dim dbDataAdapter As New SqlDataAdapter(Me.SQLSelectCommand)
                dbDataAdapter.Fill(dt)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
                'unreachable code after the throw statement
            End Try

            Return dt

        End Function

        Public Function GetTicketsByMonth(ByVal monthnumber As Integer) As DataTable
            Dim dt As New DataTable

            Try
                'ensure proper conversions to corresponding DbType
                Me.SettingUpSQLSelectCommand("GetTicketsByMonth")
                Me.SQLSelectCommand.Parameters.Add(CreateParameter("monthnumber", monthnumber, DbType.Int32))
                Dim dbDataAdapter As New SqlDataAdapter(Me.SQLSelectCommand)
                dbDataAdapter.Fill(dt)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
                'unreachable code after the throw statement
            End Try

            Return dt

        End Function



        '###############################################################

        ''' Last Modified: March 16, 2011
        ''' <summary>
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        'Public Function GetTemplateInfo(ByVal tempID As String) As DataTable
        '    Dim dt As New DataTable

        '    Try
        '        'ensure proper conversions to corresponding DbType
        '        If tempID = "" Then tempID = Nothing
        '        Me.SettingUpSQLSelectCommand("usp_fsiemail_GetTemplateInfo")
        '        Me.SQLSelectCommand.Parameters.Add(CreateParameter("TempID", tempID, DbType.Int64))
        '        Dim dbDataAdapter As New SqlDataAdapter(Me.SQLSelectCommand)
        '        dbDataAdapter.Fill(dt)
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
        '        'unreachable code after the throw statement
        '    End Try

        '    Return dt

        'End Function
        '
        ''' Last Modified: March 16, 2011
        ''' <summary>
        ''' This method updates records in the 'fsiemail_template' table
        ''' </summary>
        ''' <param name="objFSIEmailTemplate"></param>
        ''' <param name="templateID"></param>
        ''' <param name="connectSql"></param>
        ''' <param name="sqlTrans"></param>
        ''' <returns>True indicating a successful update; otherwise, it returns false</returns>
        ''' <remarks></remarks>
        '
        'Public Function UpdateEmailTemplate(ByVal objFSIEmailTemplate As FSIEmailTemplate, _
        '                       ByVal templateID As String, _
        '                       ByVal connectSql As SqlConnection, ByVal sqlTrans As SqlTransaction) As Boolean
        '    Dim blnResult As Boolean = True

        '    Try
        '        'ensure proper conversions to corresponding DbType
        '        If templateID = "" Then templateID = Nothing

        '        SQLUpdateCommand.Connection = connectSql
        '        SQLUpdateCommand.Transaction = sqlTrans
        '        SettingUpSQLUpdateCommand("usp_fsiemail_EmailTemplate_update")
        '        'Add parameter(s) to the command object
        '        SQLUpdateCommand.Parameters.Add(CreateParameter("Subsystem", objFSIEmailTemplate.System, DbType.String))
        '        'TODO: make sure Audience can handle Null 
        '        If Len(Trim(objFSIEmailTemplate.Audience)) = 0 Then
        '            objFSIEmailTemplate.Audience = Nothing
        '        End If
        '        Me.SQLUpdateCommand.Parameters.Add(CreateParameter("Audience", objFSIEmailTemplate.Audience, DbType.String))
        '        Me.SQLUpdateCommand.Parameters.Add(CreateParameter("EmailType", objFSIEmailTemplate.EmailType, DbType.String))
        '        Me.SQLUpdateCommand.Parameters.Add(CreateParameter("TemplateName", objFSIEmailTemplate.TemplateName, DbType.String))
        '        Me.SQLUpdateCommand.Parameters.Add(CreateParameter("TemplateDesc", objFSIEmailTemplate.TemplateDesc, DbType.String))
        '        Me.SQLUpdateCommand.Parameters.Add(CreateParameter("RevisionOpID", objFSIEmailTemplate.RevisionOpID, DbType.String))
        '        Me.SQLUpdateCommand.Parameters.Add(CreateParameter("TemplateID", templateID, DbType.Int64))
        '        'If it is close, open it.
        '        Me.OpenSqlConnection(Me.SQLUpdateCommand)

        '        If (SQLUpdateCommand.ExecuteNonQuery() <= 0) Then blnResult = False

        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
        '        'unreachable code in this catch block after Throw statement
        '    Finally
        '        'don't close connection in case it needs to roll back
        '    End Try

        '    Return blnResult

        'End Function
       
    End Class

End Namespace