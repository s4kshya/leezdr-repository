
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q1.aspx.vb" Inherits="Q1" MasterPageFile="~/Site.Master" %>




<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>

    <script type='text/javascript'>




        function openSQL(sql) {
            myWindow = window.open('', '', 'width=600,height=300;scrollbars=no;status=no;location=no;menubar=no;resizable=yes;toolbar=no');
            myWindow.document.write(sql);
            myWindow.focus();
        }

        function openAdvice(advice) {
            var url = 'Advices.aspx?param=' + advice;
            myWindow = window.open('', '', 'width=800,height=600;scrollbar=1;status=no;location=no;menubar=no;resizable=yes;toolbar=no');
            myWindow.document.write('<html><body><img src="wait.gif" alt="Please Wait"/></body></html>');
            myWindow.location = url;
            myWindow.focus();
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
  <script type='text/javascript'>
      var count = 5;

      var counter = setInterval(timer, 1000); //1000 will  run it every 1 second

      function timer() {
          
          count = count - 1;
        
          if (count <= 0) {
              location.reload(1);
          }

          document.getElementById("sec").innerHTML = '<b>Page will refresh in ' + count + ' Seconds</b>';
     
      }


</script>
    <h2 class="style4">
       Dashboard
    </h2>
    <p/>
    <div id='chart_div'></div>
  


  <table>
  <tr><td id="sec" align="right"><b>Page will refresh in 5 Seconds</b></td></tr>
  </table>





    <asp:GridView  ID="gvParam" runat="server" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" 
    GridLines="None" Width="100%" OnRowDataBound="RowBound">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      
        <AlternatingRowStyle BackColor="White" />
        <RowStyle BackColor="#EFF3FB" />


        <Columns>
          
            <asp:BoundField ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" DataField="ParametersName" HeaderText="Parameter Name" />
            <asp:BoundField ItemStyle-Width="13%" HeaderStyle-HorizontalAlign="Left" DataField="CurrentValue" HeaderText="Current Value" />
            <asp:BoundField ItemStyle-Width="17%"  HeaderStyle-HorizontalAlign="Left" DataField="Indicator" HeaderText="Indicator" />
            <asp:BoundField ItemStyle-Width="17%"  ItemStyle-HorizontalAlign="Center" HeaderText="View Details" />            
            <asp:BoundField ItemStyle-Width="17%"  ItemStyle-HorizontalAlign="Center" DataField="Advice" HeaderText="Advice" />
            <asp:BoundField ItemStyle-Width="17%"  ItemStyle-HorizontalAlign="Center" DataField="Sql" HeaderText="Sql" />
            

        </Columns>

      
       
    
    </asp:GridView>
</asp:Content>

