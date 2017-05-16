Imports T4.MANAGER.Data
Imports System.Data
Imports System.Data.SqlClient

Namespace T4.MVC
    Public Class TicketsController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Tickets/ListAll
        Function ListAll() As JsonResult
            Dim connstr As String = System.Configuration.ConfigurationManager.ConnectionStrings("T4ConnectionString").ConnectionString
            Dim objApp As New T4ManagerAppPort(connstr)
            
            Dim dbTicketList As New TicketList()
            'clear collection
            dbTicketList.Clear()
            dbTicketList.LoadAll(objApp)
            
            Return Json(New With {Key .results = dbTicketList}, JsonRequestBehavior.AllowGet)

        End Function


       

    End Class
End Namespace