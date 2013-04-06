<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Advices.aspx.vb" Inherits="Advices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 103px;
        }
        .style2
        {
            width: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table border="1px" style="width:100%;">
            <tr>
                <td class="style1">
                    <strong>Parameters</strong></td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblParam" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>System Advices</strong></td>
                <td class="style2">
                    :</td>
                <td>
            <asp:GridView ID="gvResult" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="Both">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle HorizontalAlign="Left" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        
            </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>Advice</strong></td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblAdvice" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
