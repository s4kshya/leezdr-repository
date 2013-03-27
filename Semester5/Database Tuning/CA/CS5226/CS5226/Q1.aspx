
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Q1.aspx.vb" Inherits="Q1" MasterPageFile="~/Site.Master" %>


<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>


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
  








    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />

        <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="true" />
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
     <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanelConfiguration" LoadingPanelID="RadAjaxLoadingPanelConfiguration">
          <div style="float: left;" class="qsf-ib">
               <telerik:RadRadialGauge runat="server" ID="RadRadialGauge1" Width="300px" Height="300px">
                    <Pointer Value="2.2">
                         <Cap Size="0.1" />
                    </Pointer>
                    <Scale Min="0" Max="6" MajorUnit="1">
                         <Labels Format="{0} bar" />
                         <Ranges>
                              <telerik:GaugeRange Color="#8dcb2a" From="1.5" To="2.5" />
                              <telerik:GaugeRange Color="#ffc700" From="2.5" To="3.5" />
                              <telerik:GaugeRange Color="#ff7a00" From="3.5" To="4.5" />
                              <telerik:GaugeRange Color="#c20000" From="4.5" To="6" />
                         </Ranges>
                    </Scale>
               </telerik:RadRadialGauge>
          </div>
          <%--Configurator Start--%>
          <div style="width: 550px; float: left; padding: 15px 0px 0px 50px;" class="qsf-ib">
               <qsf:ConfiguratorPanel runat="server" ID="RadialGaugeConfigurator" Title="Basic RadialGauge Settings"
                    Width="350px" Expanded="true">
                    Set new Pointer Value:
                    <telerik:RadNumericTextBox runat="server" ID="NewValueNumericBox" Width="200px" AllowOutOfRangeAutoCorrect="true"
                         EmptyMessage="Enter value between 0 and 6">
                    </telerik:RadNumericTextBox>
                    <br />
                    <br />
                    Pointer size:
                    <telerik:RadNumericTextBox runat="server" ID="PointerCapSizeNumericBox" AllowOutOfRangeAutoCorrect="true"
                         MaxValue="1" MinValue="0" ShowSpinButtons="true" Value="0.1">
                         <IncrementSettings Step="0.05" />
                    </telerik:RadNumericTextBox>
                    <br />
                    <br />
                    Main background color:
                    <telerik:RadColorPicker runat="server" ID="MainBackgroundColorPicker" ShowIcon="true"
                         CssClass="qsf-ib">
                    </telerik:RadColorPicker>
                    <br />
                    <br />
                    <telerik:RadButton runat="server" ID="IsReversedCheckBox" ButtonType="ToggleButton"
                         ToggleType="CheckBox" Text="Is Reversed (changes values position)" AutoPostBack="false">
                    </telerik:RadButton>
                    <br />
                    <br />
                    Labels position:
                    <telerik:RadComboBox runat="server" ID="LabelsPositionComboBox">
                         <Items>
                              <telerik:RadComboBoxItem Text="Inside" Selected="true" />
                              <telerik:RadComboBoxItem Text="Outside" />
                         </Items>
                    </telerik:RadComboBox>
                    <br />
                    <br />
                    <telerik:RadButton runat="server" ID="ConfigureButton" Text="Configure the Gauge"
                         OnClick="ConfigureButton_Click">
                    </telerik:RadButton>
               </qsf:ConfiguratorPanel>
          </div>
     </telerik:RadAjaxPanel>
     <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanelConfiguration">
     </telerik:RadAjaxLoadingPanel>











    <asp:GridView  ID="gvParam" runat="server" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" 
    GridLines="None" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      
        <AlternatingRowStyle BackColor="White" />
        <RowStyle BackColor="#EFF3FB" />


        <Columns>

            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="ParametersName" HeaderText="Parameter Name" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="OptimalValue" HeaderText="Optimal Value" />
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="CurrentValue" HeaderText="Current Value" />
            <asp:ButtonField Text="Edit" />
        </Columns>

      
       
    
    </asp:GridView>
</asp:Content>

