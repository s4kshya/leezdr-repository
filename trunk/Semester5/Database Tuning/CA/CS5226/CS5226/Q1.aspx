
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q1.aspx.vb" Inherits="Q1" MasterPageFile="~/Site.Master" %>




<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>

    <script type='text/javascript'>
        function openSQL(sql) {

            myWindow = window.open('', '', 'width=600,height=300;scrollbars=no;status=no;location=no;menubar=no;resizable=yes;toolbar=no')
           myWindow.document.write(sql)
           myWindow.focus()

        }
    
    </script>

    <script type='text/javascript'>
        google.load('visualization', '1', { packages: ['gauge'] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
          ['Label', 'Value'],
          ['DB Health', 91]
        ]);

            var options = {
                width: 250, height: 250,
                greenFrom: 0, greenTo: 59,
                yellowFrom: 60, yellowTo: 79,
                redFrom: 80, redTo: 100
            };

            var chart = new google.visualization.Gauge(document.getElementById('chart_div'));
            chart.draw(data, options);

            
        }
    </script>

</asp:Content>






<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="style4">
        Question 1 - Database Parameters
    </h2>
    <p/>
    <div id='chart_div'></div>
  









  <table>
  <tr><td></td></tr>
  </table>





    <asp:GridView  ID="gvParam" runat="server" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" 
    GridLines="None" Width="100%" OnRowDataBound="RowBound">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      
        <AlternatingRowStyle BackColor="White" />
        <RowStyle BackColor="#EFF3FB" />


        <Columns>

            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="ParametersName" HeaderText="Parameter Name" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="CurrentValue" HeaderText="Current Value" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="Indicator" HeaderText="Indicator" />
            <asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="Sql" HeaderText="" />

        </Columns>

      
       
    
    </asp:GridView>
</asp:Content>

