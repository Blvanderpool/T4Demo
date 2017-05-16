Imports T4.MANAGER.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services

Public Class T4Dashboard
    Inherits System.Web.UI.Page

    Private ticketCollection As New TicketList
    Dim connstr As String = System.Configuration.ConfigurationManager.ConnectionStrings("T4ConnectionString").ConnectionString
    Dim objApp As New T4ManagerAppPort(connstr)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ' initially to appear before the first roundtrip.
            Dim dt As DataTable = objApp.GetTicketsAll()

            ' Start with January = Month 1
            LoadTicketListing(1)
            Me.lblTicketsCount.Text = "5"

            Me.gvSchedules.DataSource = dt
            Me.gvSchedules.DataBind()
            Me.lblScheduleCount.Text = gvSchedules.Rows.Count.ToString

            Page.DataBind()
        End If

    End Sub

    ''' <summary>
    ''' The procedure populates the tickets gridview
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTicketListing(Optional ByVal monthValue As Integer = 0)

        ticketCollection.Clear()
        Dim bDone As Boolean

        If (monthValue > 0) Then
            bDone = ticketCollection.LoadAll(monthValue, objApp)
        Else
            bDone = ticketCollection.LoadAll(objApp)
        End If

        gvTickets.DataSource = ticketCollection
        gvTickets.DataBind()
        Me.lblTicketsCount.Text = gvTickets.Rows.Count.ToString

    End Sub

    Protected Sub ddlMonths_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadTicketListing(ddlMonths.SelectedValue)
    End Sub


   


    <WebMethod()> _
    Public Shared Function RefreshTickets() As TicketList   ' List(Of Ticket)
        Dim cnstr As String = System.Configuration.ConfigurationManager.ConnectionStrings("T4ConnectionString").ConnectionString
        Dim oApp As New T4ManagerAppPort(cnstr)
        Dim refreshTicketList As New TicketList
        Dim bDone As Boolean = refreshTicketList.LoadAll(oApp)

        Return refreshTicketList
    End Function



    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.gvPassword.DataSourceID = "sqldsPassword"
        Me.gvPassword.DataBind()
    End Sub
End Class