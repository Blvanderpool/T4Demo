<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="T4Dashboard.aspx.vb" Inherits="T4.WEB.T4Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/t4Style.css" rel="stylesheet" />
    
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>T4 Dashboard</title>
	<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link href="Content/css/jq-ui-style.css" rel="stylesheet" />
	<style>
		label, input { display:block; }
		input.text { margin-bottom:12px; width:95%; padding: .4em; }
		fieldset { padding:0; border:0; margin-top:25px; }
	    #lnkChangePasswordModal
	    {
           margin-top : 20px;
           margin-right :25px;
           color: #dc7477;
	    }
		h1 { font-size: 1.2em; margin: .6em 0; }
		.ui-dialog .ui-state-error { padding: .3em; }
		
	</style>
	<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
	<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
	<script>
	    $(function () {
	        var dialog, form;

	        $("#msgbox").hide();
	   
	        dialog = $("#dialog-form").dialog({
	            autoOpen: false,
	            height: 400,
	            width: 350,
	            modal: true,
	            buttons: {
	                Close: function () { dialog.dialog("close"); }
	            },
	            close: function () {
	                form[0].reset();	              
	            }
	        });

            
            //change-password-dialog
	        $("#lnkChangePasswordModal").button().on("click", function () {

	           // dialog.dialog("open");

	            dialog.dialog({
	                autoOpen: true,
	                height: 600,
	                width: 600,
	                modal: true
	            });
	            return false;
	        });


	        $("#btnSaveAndContinue").button().on("click", function () {

	            var nPassword = document.getElementById("newuserpassword").value;
	            var oPassword = document.getElementById("olduserpassword").value;
	            SetPassword(nPassword, oPassword);
	        });



	        function SetPassword(newpswd, oldpswd) {
	           
	            $.ajax({
	                type: "POST",
	                url: "UserMgtWebService.aspx?newpass=" + newpswd + "&oldpass=" + oldpswd,
	                dataType: "text",                                   
	                success: function (data) {
	                    console.log(data);
	                    $("#msgbox").show();                    
	                }
	            });

	        };
            
	    });
	</script>

</head>
<body>
    <div id="t4logo" class="globalheader">                    
           <div class="right"><a id="lnkChangePasswordModal">Change Password</a> </div>
        <span class="t4Logo" ></span>
    </div>
    
    <form id="form1" runat="server">
     <div id="dialog-form" title="Change User Password">
	        <p class="validateTips">All form fields are required.</p>

	        <!-- form  -->
		        <fieldset>
                    <legend></legend>

                    <div id="msgbox" class="greenbox"> Password changes are saved</div><br />
			        <label for="newpassword">New Password</label>
			        <input type="password" name="newuserpassword" id="newuserpassword" value="" class="text ui-widget-content ui-corner-all" />
			        <label for="oldpassword">Old Password</label>
			        <input type="password" name="olduserpassword" id="olduserpassword" value="" class="text ui-widget-content ui-corner-all" />
                    <br />
                    
                     <button id="btnSaveAndContinue" class="t4Button" style="color:white; ">SAVE</button> <br /><br />

			        <!-- Allow form submission with keyboard without duplicating the dialog button -->
			        <input type="submit" tabindex="-1" style="position:absolute; top:-1000px">
		        </fieldset>
                
	        <!-- /! -->
    </div>


    <div class="ui-widget">    
        <br /><br /><br /><br />
     <div class="full" style="display:none;">
         <button id="btnChangePasswordM"  class="t4Button" >Change Password>
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
        
    <div style="width: 300px;"> 
        <h3 class="redbox" style="width:13px;display: inline;" ><asp:Label ID="lblScheduleCount" runat="server" /></h3> <h3 class="bold_text" style="color:blue; display:inline;  ">Schedule</h3> </div></div>        
    <br />
    <div class="full">
              <div style="overflow-y: scroll; height:400px;">
                <asp:GridView ID="gvSchedules" CssClass="table table.T4GridView th" runat="server" AutoGenerateColumns="False" DataKeyNames="ticketsID" >
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

    <div class="full">      
         <div class="pull-left" style="height:175px; width: 400px;" >
            <asp:GridView CssClass="table bulletinGrid " style="height:250px; display: block;" ID="gvPassword" DataSourceID="sqldsPassword"  runat="server"></asp:GridView>
        </div>
        <div class="pull-right">
                 <asp:Button ID="btnRefresh" CssClass="left" runat="server"  style="vertical-align:top;" Text="Refresh" />
        </div>
    </div>
    </form>
    <asp:SqlDataSource ID="sqldsPassword" runat="server" ConnectionString="<%$ ConnectionStrings:T4ConnectionString %>" SelectCommand="SELECT [userno], [username], [fullname], [userpassword] FROM [users] WHERE ([userno] = 3)"></asp:SqlDataSource>
</body>
</html>