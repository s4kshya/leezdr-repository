<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q5.aspx.vb" Inherits="Q5" MasterPageFile="~/Site.Master" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
       Debug Inteface
    </h2>
   <table width="100%">
    <tr><td>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Height="100%" 
            Width="100%"></asp:Label>
        <asp:TextBox ID="tbSql" runat="server" BackColor="#E9FDFC" 
            TextMode="MultiLine" Width="100%" BorderColor="Black" BorderStyle="Solid" 
            Height="287px"></asp:TextBox>
        <br />
        <asp:Button ID="btnSend" runat="server" Text="Execute" Width="100px" />
        <br />
    </td></tr>
    <tr>
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
   
   </table>
</asp:Content>
