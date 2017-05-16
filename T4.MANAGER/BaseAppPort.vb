Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common

Namespace Data

    Public Class BaseAppPort
        Private m_SqlConnection As SqlConnection
        Private m_Cmd As SqlCommand
        Private m_SqlSelectCommand As SqlCommand
        Private m_SqlInsertCommand As SqlCommand
        Private m_SqlUpdateCommand As SqlCommand
        Private m_SqlDeleteCommand As SqlCommand
        Private m_CommandTimeout As Integer

#Region "Properties"
        Public Property SQLConnection() As SqlConnection
            Get
                Return m_SqlConnection
            End Get
            Set(ByVal value As SqlConnection)
                m_SqlConnection = value
            End Set
        End Property

        Public Property SQLCommand() As SqlCommand
            Get
                Return m_Cmd
            End Get
            Set(ByVal value As SqlCommand)
                m_Cmd = value
            End Set
        End Property

        Public Property SQLSelectCommand() As SqlCommand
            Get
                Return m_SqlSelectCommand
            End Get
            Set(ByVal value As SqlCommand)
                m_SqlSelectCommand = value
            End Set
        End Property

        Public Property SQLInsertCommand() As SqlCommand
            Get
                Return m_SqlInsertCommand
            End Get
            Set(ByVal value As SqlCommand)
                m_SqlInsertCommand = value
            End Set
        End Property

        Public Property SQLUpdateCommand() As SqlCommand
            Get
                Return m_SqlUpdateCommand
            End Get
            Set(ByVal value As SqlCommand)
                m_SqlUpdateCommand = value
            End Set
        End Property

        Public Property SQLDeleteCommand() As SqlCommand
            Get
                Return m_SqlDeleteCommand
            End Get
            Set(ByVal value As SqlCommand)
                m_SqlDeleteCommand = value
            End Set
        End Property

        Public Property CommandTimeOut() As Integer
            Get
                Return m_CommandTimeout
            End Get
            Set(ByVal value As Integer)
                If value <= 0 Then
                    Throw New ArgumentException("Command Time Out must be greater than 0")
                Else
                    m_CommandTimeout = value
                End If

            End Set
        End Property

#End Region
#Region "Constructors"
        ''' Last Modified: March 05, 2010
        ''' <summary>
        ''' A constructor taking 2 params
        ''' </summary>
        ''' <param name="sqlConnectionObj">an object of SqlConnection</param>
        ''' <param name="commandTimeout">wait time in seconds before terminating the attempt to execute 
        ''' a command and generating an error.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal sqlConnectionObj As SqlConnection, ByVal commandTimeout As Integer)
            Me.CommandTimeOut = commandTimeout
            SQLConnection = sqlConnectionObj
            CreateSQLCommands(True)

        End Sub
        ''' Last Modified: March 05, 2010
        ''' <summary>
        ''' A constructor taking 2 parameters
        ''' </summary>
        ''' <param name="appConnectionString">connection string in string format</param>
        ''' <param name="commandTimeout">wait time in seconds before terminating the attempt to execute 
        ''' a command and generating an error.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal appConnectionString As String, ByVal commandTimeout As Integer)

            Me.CommandTimeOut = commandTimeout
            'Assign connection string to sqlconnection object, m_SqlConnection
            If Not (String.IsNullOrEmpty(appConnectionString)) Then
                SQLConnection = New SqlConnection(appConnectionString)
            Else
                Throw New ArgumentException("Connection string cannnot be empty or null")
            End If
            CreateSQLCommands(True)

        End Sub

        Public Sub New(ByVal connectionString As String)
            'Assign connection string to sqlconnection object, m_SqlConnection
            If Not (String.IsNullOrEmpty(connectionString)) Then
                SQLConnection = New SqlConnection(connectionString)
            Else
                Throw New ArgumentException("Connection string cannnot be empty or null")
            End If
            CreateSQLCommands(False)
        End Sub
#End Region

#Region "Class methods"
        ''' Last Modified: March 05, 2010
        ''' <summary>
        ''' This method creates commands and set command timeout for 'Select' command if necessary
        ''' </summary>
        ''' <param name="blnCommandTimeOut"></param>
        ''' <remarks></remarks>
        Private Sub CreateSQLCommands(ByVal blnCommandTimeOut As Boolean)
            SQLCommand = SQLConnection.CreateCommand()
            ' Those SqlCommand objects are required a valid open connection to issue
            ' the command to the data store
            SQLSelectCommand = SQLConnection.CreateCommand()
            'Assign commandtimeout
            If blnCommandTimeOut Then SQLSelectCommand.CommandTimeout = Me.CommandTimeOut()
            'The default timeout = 30 seconds should be sufficient for Insert, Update, and Delete
            SQLInsertCommand = SQLConnection.CreateCommand()
            SQLUpdateCommand = SQLConnection.CreateCommand()
            SQLDeleteCommand = SQLConnection.CreateCommand()
        End Sub
        ''' Last Modified: April 18, 2011
        ''' <summary>
        ''' This method creates an instance of SqlParameter
        ''' </summary>
        ''' <param name="paramName">name of a stored procedure's parameter</param>
        ''' <param name="paramValue">associated value of the parameter </param>
        ''' <param name="type">the type of parameter. It must be one of the members of System.Data.DbType</param>
        ''' <returns></returns>
        ''' <remarks> 'Let exception bubble up to the caller.</remarks>
        ''' TODO: add constraints here to limit the second argument to String, Integer, Boolean, and DateTime
        Protected Function CreateParameter(Of T)(ByVal paramName As String, ByVal paramValue As T, _
                      ByVal type As DbType) As SqlParameter

            Dim objParameter As SqlParameter = SQLCommand.CreateParameter

            If Not (String.IsNullOrEmpty(paramName)) Then
                With objParameter
                    .ParameterName = paramName
                    If paramValue Is Nothing Then
                        .Value = DBNull.Value
                    Else
                        .Value = paramValue
                    End If
                    .IsNullable = True
                    .Direction = ParameterDirection.Input
                    .DbType = type
                End With
            Else
                Throw New ArgumentException("The parameter name cannot be empty or null")
                'unreachable code after the throw statement
            End If
            Return objParameter

        End Function
        Public Sub SettingUpSQLCommand(ByVal storedProcedureName As String)
            Me.SQLCommand.Parameters.Clear()
            Me.SQLCommand.CommandText = storedProcedureName
            Me.SQLCommand.CommandType = CommandType.StoredProcedure

        End Sub
        Public Sub SettingUpSQLSelectCommand(ByVal storedProcedureName As String)
            Me.SQLSelectCommand.Parameters.Clear()
            Me.SQLSelectCommand.CommandText = storedProcedureName
            Me.SQLSelectCommand.CommandType = CommandType.StoredProcedure

        End Sub
        Public Sub SettingUpSQLInsertCommand(ByVal storedProcedureName As String)
            Me.SQLInsertCommand.Parameters.Clear()
            Me.SQLInsertCommand.CommandText = storedProcedureName
            Me.SQLInsertCommand.CommandType = CommandType.StoredProcedure

        End Sub
        Public Sub SettingUpSQLUpdateCommand(ByVal storedProcedureName As String)
            Me.SQLUpdateCommand.Parameters.Clear()
            Me.SQLUpdateCommand.CommandText = storedProcedureName
            Me.SQLUpdateCommand.CommandType = CommandType.StoredProcedure

        End Sub
        Public Sub SettingUpSQLDeleteCommand(ByVal storedProcedureName As String)
            Me.SQLDeleteCommand.Parameters.Clear()
            Me.SQLDeleteCommand.CommandText = storedProcedureName
            Me.SQLDeleteCommand.CommandType = CommandType.StoredProcedure

        End Sub
        ''' <summary>
        ''' This void method takes a sqlCommand object and determines
        ''' if it needs to open a database connection
        ''' </summary>
        ''' <param name="sqlCommand">a SqlCommand object</param>
        ''' <remarks></remarks>
        Public Sub OpenSqlConnection(ByVal sqlCommand As SqlCommand)
            If sqlCommand.Connection.State = ConnectionState.Closed Then
                sqlCommand.Connection.Open()
            End If
        End Sub
        ''' <summary>
        ''' This void method takes a sqlCommand object and determines
        ''' if it needs to close a database connection
        ''' </summary>
        ''' <param name="sqlComamnd">a SqlCommand object</param>
        ''' <remarks></remarks>
        Public Sub CloseSqlConnection(ByVal sqlComamnd As SqlCommand)
            If sqlComamnd.Connection.State <> ConnectionState.Closed Then
                sqlComamnd.Connection.Close()
            End If
        End Sub
#End Region

    End Class

End Namespace
