<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Historical.aspx.vb" Inherits="Historical" MasterPageFile="~/Site.Master" EnableSessionState="True" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

 <script type="text/javascript" src="https://www.google.com/jsapi"></script>

   <script type='text/javascript'>
   /*
       function openRpt(typ, dt) {
          
           alert(url);
           window.open(url, '', 'width=600,height=300;scrollbars=no;status=no;location=no;menubar=no;resizable=yes;toolbar=no');

       }
       */

       function openRpt(typ, dt) {
           var url = 'awr_report.aspx?typ=' + typ + '&dt=' + dt;
           myWindow = window.open('', '', 'width=800,height=600;');
           myWindow.document.write('<html><body>Please wait while the system process your request. <br/> <br/> This may take several seconds. <br/><br/> Thanks.</body></html>');
           myWindow.location = url;
           myWindow.focus();
       }




    </script>

</asp:Content>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

  <h2 class="style4">
        Historical Parameters Value
  </h2>

<table width="100%">
<tr>

<td align="left"">
    Records to display
    <asp:DropDownList AutoPostBack="true" ID="ddRec" runat="server">
    <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
    <asp:ListItem Text="20" Value="20"></asp:ListItem>
    <asp:ListItem Text="30" Value="30"></asp:ListItem>
    <asp:ListItem Text="40" Value="40"></asp:ListItem>
    <asp:ListItem Text="50" Value="50"></asp:ListItem>
    </asp:DropDownList>
   
</td>




<td align="right">
    <asp:Literal ID="lblXInt" runat="server"></asp:Literal>
</td>
</tr>

<tr><td colspan="2">
    <asp:Literal ID="litChart" runat="server"></asp:Literal>
    </td></tr>

<tr><td colspan="2">
    <asp:Literal ID="litLInk" runat="server"></asp:Literal>
    </td></tr>
<tr><td colspan="2">
    <asp:GridView  ID="gvParam" runat="server" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" 
    GridLines="None" Width="100%" OnRowDataBound="RowBound">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      
        <AlternatingRowStyle BackColor="White" />
        <RowStyle BackColor="#EFF3FB" />


        <Columns>
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderText="#" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="PARAM" HeaderText="PARAM" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="SNAP_DT" HeaderText="SNAP DATE" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="VAL" HeaderText="VALUE" />
            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Break Down"/>
            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="View AWR Report" />

        </Columns>

      
       
    
    </asp:GridView>
</td></tr>

</table>


</asp:Content>

