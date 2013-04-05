<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q2.aspx.vb" Inherits="Q2" MasterPageFile="~/Site.Master" EnableSessionState="True" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">



    <style type="text/css">
        .style2
        {
            text-align: center;
        }
    </style>



</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<table border="1px" cellpadding="0px" cellspacing="0px" border="1px" style="height:280px; width: 900px">
    <tr>
        <td style="width:40%;"><strong>Database Parameters</strong></td>
        <td style="width:20%; background-color:Green; color:Black;" class="style2"><strong>GREEN </strong></td>
        <td style="width:20%; background-color:Yellow; color:Black;" class="style2"><strong>YELLOW</strong></td>
        <td style="width:20%; background-color:RED; color:Black;" class="style2"><strong>RED</strong></td>
        
 
    </tr>

    <tr>
        <td class="style3">Shared Pool (%)</td>
        <td class="style2"><strong><asp:TextBox ID="SP_GREEN_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
            &nbsp;to <asp:TextBox ID="SP_GREEN_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SP_YELLOW_S" runat="server" 
                Width="70px" BackColor="#CCFFFF" MaxLength="2">86</asp:TextBox>
            &nbsp;to <asp:TextBox ID="SP_YELLOW_E" runat="server" Width="70px" 
                BackColor="#CCFFFF" MaxLength="2">94</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong><asp:TextBox ID="SP_RED_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">95</asp:TextBox>
            &nbsp;to <asp:TextBox ID="SP_RED_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">100</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <tr>
        <td class="style3">Redo Log Buffer/Files</td>
        <td class="style2"><strong><asp:TextBox ID="RB_GREEN_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
            &nbsp;to <asp:TextBox ID="RB_GREEN_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">0.5</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="RB_YELLOW_S" runat="server" 
                Width="70px" BackColor="#CCFFFF" MaxLength="2">0.6</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="RB_YELLOW_E" runat="server" Width="70px" 
                BackColor="#CCFFFF" MaxLength="4">0.75</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong><asp:TextBox ID="RB_RED_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">0.75</asp:TextBox>
            &nbsp;to <asp:TextBox ID="RB_RED_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">1</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <tr>
        <td style="width:40%;"><strong>Database Parameters</strong></td>
        <td style="width:20%;background-color:Red; color:Black;" class="style2"><strong>RED </strong></td>
        <td style="width:20%;background-color:Yellow; color:Black;" class="style2"><strong>YELLOW</strong></td>
        <td style="width:20%; background-color:Green; color:Black;" class="style2"><strong>GREEN</strong></td>
        
 
    </tr>

    <tr>
        <td class="style3">Buffer Cache (%)</td>
        <td class="style2"><strong><asp:TextBox ID="BC_RED_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">1</asp:TextBox>
            &nbsp;to <asp:TextBox ID="BC_RED_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">69</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="BC_YELLOW_S" runat="server" 
                Width="70px" BackColor="#CCFFFF" MaxLength="2">70</asp:TextBox>
            &nbsp;to <asp:TextBox ID="BC_YELLOW_E" runat="server" Width="70px" 
                BackColor="#CCFFFF" MaxLength="2">89</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong><asp:TextBox ID="BC_GREEN_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">90</asp:TextBox>
            &nbsp;to <asp:TextBox ID="BC_GREEN_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">100</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <tr>
        <td class="style3">Memory area used for Sorting</td>
        <td class="style2"><strong><asp:TextBox ID="SORT_RED_S" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">1</asp:TextBox>
            &nbsp;to <asp:TextBox ID="SORT_RED_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">69</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SORT_YELLOW_S" runat="server" 
                Width="70px" BackColor="#CCFFFF" MaxLength="2">70</asp:TextBox>
            &nbsp;to <asp:TextBox ID="SORT_YELLOW_E" runat="server" Width="70px" 
                BackColor="#CCFFFF" MaxLength="2">89</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SORT_GREEN_S" runat="server" 
                Width="70px" BackColor="#FFFFCC" ReadOnly="True">90</asp:TextBox>
            &nbsp;to <asp:TextBox ID="SORT_GREEN_E" runat="server" Width="70px" 
                BackColor="#FFFFCC" ReadOnly="True">100</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <asp:Label ID="lblRemarks" runat="server" Text="-"></asp:Label>

</table>
<p />
<asp:Button runat="server" Text="Save Configuration" Width="200px" ID="btnSave" 
        onclick="btnSave_Click" />
</asp:Content>

