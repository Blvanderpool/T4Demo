Imports System.Reflection
Imports System.Collections.Generic
Imports System.Web

Namespace Data
    ''' Last Modified: 
    ''' <summary>
    ''' This class represents the 'users' table 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Ticket
        Private m_TicketID As Integer
        Private m_TicketNo As String
        Private m_BatchCode As String
        Private m_BatchDate As Date
        Private m_BatchVolume As Decimal    '   [batch_volume] [decimal](18, 0) NULL,
        Private m_GrantedTo As String
        Private m_EventText As String
        Private m_LocationName As String
        Private m_SupplierName As String
        Private m_ConsigneeName As String

#Region "Constructors"
        Public Sub New(ByVal ticketID As Integer, _
                       ByVal ticketNo As String, _
                       ByVal batchCode As String, _
                       ByVal batchDate As Date, _
                       ByVal batchVolume As Decimal, _
                       ByVal grantedTo As String, _
                       ByVal eventText As String, _
                       ByVal location As String, _
                       ByVal supplier As String, _
                       ByVal consignee As String)
            m_TicketID = ticketID
            Me.TicketNo = ticketNo
            Me.BatchCode = batchCode
            Me.BatchDate = batchDate
            Me.BatchVolume = batchVolume
            Me.GrantedTo = grantedTo
            Me.EventText = eventText
            Me.LocationName = location
            Me.SupplierName = supplier
            Me.ConsigneeName = consignee

        End Sub

        Public Sub New()
            'do nothing here
        End Sub
#End Region
#Region "Properties"
        ''' <summary>
        ''' ReadOnly because the TicketID is an auto-increment field in the table
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TicketID() As Integer
            Get
                Return m_TicketID
            End Get
        End Property

        Public Property TicketNo() As String
            Get
                Return m_TicketNo
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) OrElse String.IsNullOrEmpty(value.Trim) Then
                    Throw New Exception("TicketNo cannot be null nor empty")
                Else
                    m_TicketNo = value.Trim
                End If
            End Set
        End Property

        Public Property BatchCode() As String
            Get
                Return m_BatchCode
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) OrElse String.IsNullOrEmpty(value.Trim) Then
                    Throw New Exception("BatchCode cannot be null nor empty")
                Else
                    m_BatchCode = value.Trim
                End If
            End Set
        End Property

        Public Property BatchDate() As Date
            Get
                Return m_BatchDate
            End Get
            Set(ByVal value As Date)
                m_BatchDate = value
            End Set
        End Property

        Public Property BatchVolume() As Decimal
            Get
                Return m_BatchVolume
            End Get
            Set(ByVal value As Decimal)
                If (value < 0) Then
                    Throw New Exception("BatchVolume cannot be less than zero")
                Else
                    m_BatchVolume = value
                End If

            End Set
        End Property

        Public Property GrantedTo() As String
            Get
                Return m_GrantedTo
            End Get
            Set(ByVal value As String)
                If (String.IsNullOrEmpty(value)) Then
                    m_GrantedTo = String.Empty
                Else
                    m_GrantedTo = value.Trim
                End If
            End Set
        End Property

        Public Property EventText() As String
            Get
                Return m_EventText
            End Get
            Set(ByVal value As String)
                m_EventText = value.Trim
            End Set
        End Property

        Public Property LocationName() As String
            Get
                Return m_LocationName
            End Get
            Set(ByVal value As String)
                m_LocationName = value
            End Set
        End Property

        Public Property SupplierName() As String
            Get
                Return m_SupplierName
            End Get
            Set(ByVal value As String)
                If (String.IsNullOrEmpty(value)) Then
                    m_SupplierName = String.Empty
                Else
                    m_SupplierName = value.Trim
                End If
            End Set
        End Property

        Public Property ConsigneeName() As String
            Get
                Return m_ConsigneeName
            End Get
            Set(ByVal value As String)
                If (String.IsNullOrEmpty(value)) Then
                    m_ConsigneeName = String.Empty
                Else
                    m_ConsigneeName = value.Trim
                End If
            End Set

        End Property

#End Region
#Region "Methods"

        Public Function LoadIt(ByVal ticketID As Integer, ByVal objAppPort As T4ManagerAppPort) As Boolean
            Dim blnResult As Boolean = True
            Try
                Dim dt As DataTable = objAppPort.GetTicketsAll()
                If dt.Rows.Count > 0 Then
                    Dim dr As DataRow = dt.Rows(0)
                    Me.TicketNo = DirectCast(dr("ticket_no"), String)
                    Me.EventText = TryCast(dr("event_text"), String)
                    Me.BatchCode = DirectCast(dr("batch_code"), String)
                    Me.BatchDate = DirectCast(dr("batch_date"), Date)
                    Me.BatchVolume = DirectCast(dr("batch_volume"), Decimal)
                    Me.GrantedTo = DirectCast(dr("granted_to"), String)
                    Me.LocationName = DirectCast(dr("location_name"), String)
                    Me.SupplierName = DirectCast(dr("supplier_name"), String)
                    Me.ConsigneeName = DirectCast(dr("consignee_name"), String)
                Else
                    blnResult = False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
            End Try

            Return blnResult
        End Function

        Public Function GetTicket(ByVal ticketid As Integer, ByVal objAppPort As T4ManagerAppPort) As Integer

            Dim ticketNumber As Integer = Nothing

            If Not LoadIt(ticketid, objAppPort) Then
                ticketNumber = Nothing
            Else
                ticketNumber = Me.TicketNo
            End If

            Return ticketNumber

        End Function

#End Region

    End Class

    <Serializable()> _
    Public Class TicketList
        Inherits List(Of Ticket)

        Public Function LoadAll(ByVal objAppPort As T4ManagerAppPort) As Boolean
            Dim blnResult As Boolean = False
            Try

                Dim dt As DataTable = objAppPort.GetTicketsAll()
                For Each dr As DataRow In dt.Rows
                    Dim item As New Ticket
                    'set properties for each item and add to the collectiion
                    item.TicketNo = DirectCast(dr("ticket_no"), String)
                    item.EventText = TryCast(dr("event_text"), String)
                    item.BatchCode = TryCast(dr("batch_code"), String)
                    item.BatchDate = CType(dr("batch_date"), DateTime)
                    'DirectCast(dr("batch_date"), Date)
                    item.BatchVolume = DirectCast(dr("batch_volume"), Decimal)
                    item.GrantedTo = TryCast(dr("granted_to"), String)
                    item.LocationName = TryCast(dr("location_name"), String)
                    item.SupplierName = TryCast(dr("supplier_name"), String)
                    item.ConsigneeName = TryCast(dr("consignee_name"), String)

                    Me.Add(item)
                Next

                'set to true at this point because if there is an exception, we are not able to reach this point
                blnResult = True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
            End Try

            Return blnResult

        End Function

        ''' <summary>
        '''  LoadAll - overloaded method to get tickets by month
        ''' </summary>
        ''' <param name="monthnumber"></param>
        ''' <param name="objAppPort"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function LoadAll(ByVal monthnumber As Integer, ByVal objAppPort As T4ManagerAppPort) As Boolean
            Dim blnResult As Boolean = False
            Try

                Dim dt As DataTable

                If (monthnumber > 0) Then
                    dt = objAppPort.GetTicketsByMonth(monthnumber)
                End If

                For Each dr As DataRow In dt.Rows
                    Dim item As New Ticket
                    'set properties for each item and add to the collectiion

                    Dim testDate As DateTime = CType(dr("batch_date"), DateTime)

                    item.TicketNo = DirectCast(dr("ticket_no"), String)
                    item.EventText = TryCast(dr("event_text"), String)
                    item.BatchCode = TryCast(dr("batch_code"), String)
                    item.BatchDate = CType(dr("batch_date"), DateTime)
                    item.BatchVolume = DirectCast(dr("batch_volume"), Decimal)
                    item.GrantedTo = TryCast(dr("granted_to"), String)
                    item.LocationName = TryCast(dr("location_name"), String)
                    item.SupplierName = TryCast(dr("supplier_name"), String)
                    item.ConsigneeName = TryCast(dr("consignee_name"), String)

                    Me.Add(item)
                Next

                'set to true at this point because if there is an exception, we are not able to reach this point
                blnResult = True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().Name.ToString, ex)
            End Try

            Return blnResult

        End Function

    End Class



    ''' Last Modified: May 7, 2017
    ''' <summary>
    ''' This class represents a record in the users table
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppUser
        Private m_UserNo As Integer
        Private m_UserName As String
        Private m_UserPassword As String
        Private m_FullName As String
        Private m_UserType As String
        Private m_Status As String
        Private m_Employer As String
        Private m_PswdExpireDate As Date
        Private m_LastLoginDate As Date
        Private m_LastInqryDate As Date
        Private m_LoginFailDate As Date
        Private m_LoginAttempts As Integer


#Region "Properties"
        Public Property UserNo() As Integer
            Get
                Return m_UserNo
            End Get
            Set(ByVal value As Integer)
                If value <= 0 OrElse value = Nothing Then
                    Throw New Exception("UserNo cannot be less than 0 nor null")
                Else
                    m_UserNo = value
                End If
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return m_UserName
            End Get
            Set(ByVal value As String)
                If value <= 0 OrElse value = Nothing Then
                    Throw New Exception("UserName cannot be null")
                Else
                    m_UserName = value
                End If
            End Set
        End Property

        Public Property UserPassword() As String
            Get
                Return m_UserPassword
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) OrElse String.IsNullOrEmpty(value.Trim) Then
                    Throw New Exception("UserPassword cannot be null nor empty")
                Else
                    m_UserPassword = value.Trim
                End If
            End Set
        End Property

        Public Property UserFullName() As String
            Get
                Return m_FullName
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) OrElse String.IsNullOrEmpty(value.Trim) Then
                    Throw New Exception("FullName cannot be null nor empty")
                Else
                    m_FullName = value.Trim
                End If
            End Set
        End Property

        Public Property UserType() As String
            Get
                Return m_UserType
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) OrElse String.IsNullOrEmpty(value.Trim) Then
                    Throw New Exception("UserType cannot be null nor empty")
                Else
                    m_UserType = value.Trim
                End If
            End Set
        End Property

        Public Property Status() As String
            Get
                Return m_Status
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) OrElse String.IsNullOrEmpty(value.Trim) Then
                    Throw New Exception("Status cannot be null nor empty")
                Else
                    m_Status = value.Trim
                End If
            End Set
        End Property

        Public Property PasswordExpiryDate() As Date
            Get
                Return m_PswdExpireDate
            End Get
            Set(ByVal value As Date)
                If value = Nothing Then
                    m_PswdExpireDate = Now.Date
                Else
                    m_PswdExpireDate = value
                End If
            End Set
        End Property

        Public Property LastLoginDate() As Date
            Get
                Return m_LastLoginDate
            End Get
            Set(ByVal value As Date)
                If value = Nothing Then
                    m_LastLoginDate = Now.Date
                Else
                    m_LastLoginDate = value
                End If
            End Set
        End Property

        Public Property LastInquiryDate() As Date
            Get
                Return m_LastInqryDate
            End Get
            Set(ByVal value As Date)
                If value = Nothing Then
                    m_LastInqryDate = Now.Date
                Else
                    m_LastInqryDate = value
                End If
            End Set
        End Property

        Public Property LoginFailureDate() As Date
            Get
                Return m_LoginFailDate
            End Get
            Set(ByVal value As Date)
                If value = Nothing Then
                    m_LoginFailDate = Now.Date
                Else
                    m_LoginFailDate = value
                End If
            End Set
        End Property

        Public Property LoginAttempts() As Integer
            Get
                Return m_LoginAttempts
            End Get
            Set(value As Integer)
                m_LoginAttempts = value
            End Set
        End Property


#End Region
      


    End Class


End Namespace

