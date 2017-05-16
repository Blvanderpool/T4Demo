<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="T4.WEB.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/css/t4Style.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>
</head>
<body>
    <div id="t4logo" class="globalheader">        
            <button class="batchsearch" style="width: 150px;" >BATCH SEARCH</button> 
        <span class="t4Logo" ></span>
    </div>
    
    <form id="form1" runat="server">
    <div>     
        
     <div class="full">
         <button>Change Password</button> <br /><br /><br />
     </div> 

     <div class="full right">
          <div class="left" style="width: 300px;"><h3 class="redbox" style="width:13px;display: inline;" > <asp:Label runat="server" ID="lblTicketsCount"></asp:Label> </h3> <h3 class="bold_text" style="color:blue; display:inline;  ">Tickets</h3> </div>
            <asp:DropDownList ID="ddlMonths"  OnSelectedIndexChanged="ddlMonths_OnSelectedIndexChanged" AutoPostBack="true" EnableViewState="true"  CssClass="dropdown_select select right" style="width: 150px;" runat="server"  >
                <asp:ListItem Value="1" Text="JANUARY" />
                <asp:ListItem Value="2" Text="FEBUARY" />
                <asp:ListItem Value="3" Text="MARCH" />
                <asp:ListItem Value="4" Text="APRIL" />
                <asp:ListItem Value="5" Text="MAY" />
                <asp:ListItem Value="6" Text="JUNE" />
                <asp:ListItem Value="7" Text="JULY" />
                <asp:ListItem Value="8" Text="AUGUST" />
                <asp:ListItem Value="9" Text="SEPTEMBER" />
                <asp:ListItem Value="10" Text="OCTOBER" />
                <asp:ListItem Value="11" Text="NOVEMBER" />
                <asp:ListItem Value="12" Text="DECEMBER" />
            </asp:DropDownList>
        <br />
    </div>
      
    <br /><br />
    <div class="full">
        <div style="overflow-y: scroll; height:200px;">
	     <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False"  
		      HorizontalAlign="Center" CssClass="table table.T4GridView th" Width="100%" >
	     <RowStyle CssClass="gridview_row" />
	     <AlternatingRowStyle CssClass="gridview_alternaterow" />
	     <Columns>
		     <%-- First column displaying "DELETE" link --%>
		
		      <asp:TemplateField HeaderText="Granted To" >
			       <ItemTemplate>
				      <asp:LinkButton ID="btnlinkCreate" CommandName="ADD" CommandArgument='<%#Eval("TicketID")%>' runat="server">ADD
				      </asp:LinkButton>
			       </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Fifth column displaying TicketNo --%>
		      <asp:TemplateField HeaderText="ID"  >
				    <ItemTemplate><%# Eval("TicketNo")%>
				    </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Fifth column displaying TemplateName --%>
		      <asp:TemplateField HeaderText="Event Name" >
				    <ItemTemplate><%# Eval("EventText")%>
				    </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Sixth column displaying Audience --%>
		      <asp:TemplateField HeaderText="Batch Code" >
				    <ItemTemplate><%# Eval("BatchCode")%>
				    </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Seventh column displaying EmailType --%>
		      <asp:TemplateField HeaderText="Location Name"  >
			       <ItemTemplate><%# Eval("LocationName")%>
			       </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Eighth column displaying EmailType --%>
		      <asp:TemplateField HeaderText="Volume" >
			       <ItemTemplate><%# Eval("BatchVolume")%>
			       </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Nineth column displaying BatchDate --%>
		      <asp:TemplateField HeaderText="Date" >
			       <ItemTemplate><%# Eval("BatchDate", "{0:MM/dd/yyyy}")%>
			       </ItemTemplate>
		      </asp:TemplateField>
		      <%-- Tenth column displaying RevisionDate --%>
		 
		      <asp:TemplateField HeaderText="Supplier" >
			       <ItemTemplate><%# Eval("SupplierName")%>
			       </ItemTemplate>
		      </asp:TemplateField>
              <%-- 11th column displaying EmailType --%>
		      <asp:TemplateField HeaderText="Consignee"  >
			       <ItemTemplate><%# Eval("ConsigneeName")%>
			       </ItemTemplate>
		      </asp:TemplateField>

	    </Columns>
	    <EmptyDataTemplate><span class="notice">No Data Found</span></EmptyDataTemplate>
    </asp:GridView>
        </div> 
           <br />
    </div>
        
    <br /><hr />
        
    <div style="width: 300px;"> <h3 class="redbox" style="width:13px;display: inline;" ><asp:Label ID="lblTicketCount" Text='<%#TicketCount%>' runat="server" /></h3> <h3 class="bold_text" style="color:blue; display:inline;  ">Schedule</h3> </div>        
    <br />
    <div class="full">
              <div style="overflow-y: scroll; height:200px;">
                <asp:GridView ID="GridView2" CssClass="table table.T4GridView th" runat="server" AutoGenerateColumns="False" DataKeyNames="ticketsID" >
                    <Columns>
                        <asp:BoundField DataField="ticketsID" HeaderText="ticketsID" InsertVisible="False" ReadOnly="True" SortExpression="ticketsID" />
                        <asp:BoundField DataField="batch_code" HeaderText="batch_code" SortExpression="batch_code" />
                        <asp:BoundField DataField="batch_date" HeaderText="batch_date" SortExpression="batch_date" />
                        <asp:BoundField DataField="batch_volume" HeaderText="batch_volume" SortExpression="batch_volume" />
                        <asp:BoundField DataField="ticket_no" HeaderText="ticket_no" SortExpression="ticket_no" />
                    </Columns>
                </asp:GridView>
              </div> 
         <br />
    </div>
    <br /> <br />


    </div>
    </form>
    
</body>
</html>
