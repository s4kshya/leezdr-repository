<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q2.aspx.vb" Inherits="Q2" MasterPageFile="~/Site.Master" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<table border="1px" cellpadding="0px" cellspacing="0px" border="1px" style="height:200px; width: 900px">
    <tr>
        <td style="width:50%;"><strong>Database Parameters</strong></td>
        <td style="width:16%; background-color:Green; color:Black;"><strong>GREEN</strong></td>
        <td style="width:16%; background-color:Yellow; color:Black;"><strong>YELLOW</strong></td>
        <td style="width:16%; background-color:Red; color:Black;"><strong>RED</strong></td>
        
 
    </tr>

    <tr>
        <td class="style2">Shared Pool</td>
        <td class="style3"><strong>&gt;<asp:TextBox ID="p1Green" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td class="style4"><strong>&gt;<asp:TextBox ID="p1Yellow" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td><strong>&lt;<asp:TextBox ID="p1Red" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
    </tr>

    <tr>
        <td class="style2">Buffer Cache</td>
        <td class="style3"><strong>&gt;<asp:TextBox ID="p2Green" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td class="style4"><strong>&gt;<asp:TextBox ID="p2Yellow" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td><strong>&gt;<asp:TextBox ID="p2Red" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
    </tr>

    <tr>
        <td class="style2">Redo Log Buffer/Files</td>
        <td class="style3"><strong>&gt;<asp:TextBox ID="p3Green" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td class="style4"><strong>&gt;<asp:TextBox ID="p3Yellow" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td><strong>&gt;<asp:TextBox ID="p3Red" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
    </tr>

    <tr>
        <td class="style2">Memory area used for Sorting</td>
        <td class="style3"><strong>&gt;<asp:TextBox ID="p4Green" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td class="style4"><strong>&gt;<asp:TextBox ID="p4Yellow" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
        <td><strong>&gt;<asp:TextBox ID="p4Red" runat="server" Width="85px"></asp:TextBox>
            </strong></td>
    </tr>

</table>
<p />
<asp:Button runat="server" Text="Save Configuration" Width="200px" />
</asp:Content>

