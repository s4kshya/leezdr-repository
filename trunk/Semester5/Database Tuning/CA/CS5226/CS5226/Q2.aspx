<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q2.aspx.vb" Inherits="Q2" MasterPageFile="~/Site.Master" EnableSessionState="True" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">



    <style type="text/css">
        .style2
        {
            text-align: center;
        }
        .style3
        {
            height: 29px;
        }
        .style4
        {
            text-align: left;
            height: 29px;
        }
    </style>



</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <h2 class="style4">
        &nbsp;Database Parameters Color indicate Settings</h2>
    <p/>
<table border="1px" cellpadding="0px" cellspacing="0px" border="1px" style="height:280px; width: 900px">
    <tr>
        <td style="width:40%;"><strong>Database Parameters</strong></td>
        <td style="width:20%; background-color:Green; color:Black;" class="style2"><strong>GREEN </strong></td>
        <td style="width:20%; background-color:Yellow; color:Black;" class="style2"><strong>YELLOW</strong></td>
        <td style="width:20%; background-color:RED; color:Black;" class="style2"><strong>RED</strong></td>
        
 
    </tr>

    <tr>
        <td class="style3">Shared Pool (%)</td>
        <td class="style2"><strong>
            <asp:TextBox ID="SP_GREEN_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="SP_GREEN_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SP_YELLOW_S" runat="server" 
                Width="70px" BackColor="White" MaxLength="2">86</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="SP_YELLOW_E" runat="server" Width="70px" 
                BackColor="White" MaxLength="2">94</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SP_RED_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">95</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="SP_RED_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">100</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <tr>
        <td class="style3">Redo Log Buffer (%)</td>
        <td class="style2"><strong>
            <asp:TextBox ID="RB_GREEN_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="RB_GREEN_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">0.5</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="RB_YELLOW_S" runat="server" 
                Width="70px" BackColor="White" MaxLength="5">0.6</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="RB_YELLOW_E" runat="server" Width="70px" 
                BackColor="White" MaxLength="5">0.75</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="RB_RED_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">0.75</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="RB_RED_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">1</asp:TextBox>
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
        <td class="style2"><strong>
            <asp:TextBox ID="BC_RED_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">1</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="BC_RED_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">69</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="BC_YELLOW_S" runat="server" 
                Width="70px" BackColor="White" MaxLength="2">70</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="BC_YELLOW_E" runat="server" Width="70px" 
                BackColor="White" MaxLength="2">89</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="BC_GREEN_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">90</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="BC_GREEN_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">100</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <tr>
        <td class="style3">Memory area used for Sorting (%)</td>
        <td class="style2"><strong>
            <asp:TextBox ID="SORT_RED_S" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">1</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="SORT_RED_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">69</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SORT_YELLOW_S" runat="server" 
                Width="70px" BackColor="White" MaxLength="2">70</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="SORT_YELLOW_E" runat="server" Width="70px" 
                BackColor="White" MaxLength="2">89</asp:TextBox>
            &nbsp;</strong></td>
        <td class="style2"><strong>
            <asp:TextBox ID="SORT_GREEN_S" runat="server" 
                Width="70px" BackColor="#CCCCCC" ReadOnly="True">90</asp:TextBox>
            &nbsp;to 
            <asp:TextBox ID="SORT_GREEN_E" runat="server" Width="70px" 
                BackColor="#CCCCCC" ReadOnly="True">100</asp:TextBox>
            &nbsp;</strong></td>
    </tr>

    <asp:Label ID="lblRemarks" runat="server" Text="-"></asp:Label>

    <tr>
        <td class="style3" colspan="4">
<asp:Button runat="server" Text="Save Color Indicator Settings" Width="200px" ID="btnSave" 
        onclick="btnSave_Click" />
        </td>
    </tr>

    <tr>
        <td class="style3" colspan="4">&nbsp;</td>
    </tr>

    <tr>
        <td class="style3" colspan="4">
        
          <h2 class="style4">
            SNAPSHOT X &amp; Y INTERVAL&nbsp; SETTINGS FOR REPORT
    </h2>
        </td>
    </tr>

    <tr>
        <td class="style3" colspan="4">X INTERVAL
            <asp:DropDownList ID="ddXInt" runat="server" Width="144px" AutoPostBack="True">
                <asp:ListItem Value="60">1 Hour</asp:ListItem>
                <asp:ListItem Value="120">2 Hour</asp:ListItem>
                <asp:ListItem Value="180">3 Hour</asp:ListItem>
                <asp:ListItem Value="240">4 Hour</asp:ListItem>
                <asp:ListItem Value="300">5 Hour</asp:ListItem>
                <asp:ListItem Value="360">6 Hour</asp:ListItem>
                <asp:ListItem Value="420">7 Hour</asp:ListItem>
                <asp:ListItem Value="480">8 Hour</asp:ListItem>
                <asp:ListItem Value="540">9 Hour</asp:ListItem>
                <asp:ListItem Value="600">10 Hour</asp:ListItem>
                <asp:ListItem Value="660">11 Hour</asp:ListItem>
                <asp:ListItem Value="720">12 Hour</asp:ListItem>
                <asp:ListItem Value="780">13 Hour</asp:ListItem>
                <asp:ListItem Value="840">14 Hour</asp:ListItem>
                <asp:ListItem Value="900">15 Hour</asp:ListItem>
                <asp:ListItem Value="960">16 Hour</asp:ListItem>
                <asp:ListItem Value="1020">17 Hour</asp:ListItem>
                <asp:ListItem Value="1080">18 Hour</asp:ListItem>
                <asp:ListItem Value="1140">19 Hour</asp:ListItem>
                <asp:ListItem Value="1200">20 Hour</asp:ListItem>
                <asp:ListItem Value="1260">21 Hour</asp:ListItem>
                <asp:ListItem Value="1320">22 Hour</asp:ListItem>
                <asp:ListItem Value="1380">23 Hour</asp:ListItem>
                <asp:ListItem Value="1440">24 Hour</asp:ListItem>
            </asp:DropDownList>
&nbsp;Y INTERVAL<asp:DropDownList ID="ddYHrInt" runat="server" Width="144px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddYMinInt" runat="server" Width="144px">
                <asp:ListItem Value="10">00 Minutes</asp:ListItem>
                <asp:ListItem Value="10">10 Minutes</asp:ListItem>
                <asp:ListItem Value="20">20 Minutes</asp:ListItem>
                <asp:ListItem Value="30">30 Minutes</asp:ListItem>
                <asp:ListItem Value="40">40 Minutes</asp:ListItem>
                <asp:ListItem Value="50">50 Minutes</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td class="style3">
<asp:Button runat="server" Text="Save X &amp; Y Interval" Width="200px" ID="btnSave0" 
        onclick="btnSave_Click" />
        </td>
        <td class="style4"></td>
        <td class="style4"></td>
        <td class="style4"></td>
    </tr>

    </table>
<p />
    &nbsp;
</asp:Content>

