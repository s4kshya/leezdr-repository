Imports System.Data
Imports Telerik.Web
Imports Telerik.Charting
Imports Telerik
Imports Telerik.Web.Design

Partial Class Q1
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            LoadGrid()
        End If

        ' DAO.ExecuteNonQuery("execute dbms_workload_repository.create_snapshot('ALL')")

    End Sub

    Public Sub LoadGrid()
        Dim agg As Double
        gvParam.DataSource = DAO.GetDataBaseParameters(agg)
        gvParam.DataBind()
        LoadChart(agg)
    End Sub

    Public Sub LoadChart(ByVal agg As Double)
        Dim sb As New StringBuilder

        sb.AppendLine("<script type='text/javascript'>")
        sb.AppendLine("google.load('visualization', '1', { packages: ['gauge'] });")
        sb.AppendLine("google.setOnLoadCallback(drawChart);")
        sb.AppendLine("function drawChart() {")
        sb.AppendLine("var data = google.visualization.arrayToDataTable([")
        sb.AppendLine("['Label', 'Value'],")
        sb.AppendLine("['DB Health', " + agg.ToString + "]")
        sb.AppendLine("]);")
        sb.AppendLine("var options = {")
        sb.AppendLine("width: 250, height: 250,")
        sb.AppendLine("greenFrom: 0, greenTo: 59,")
        sb.AppendLine("yellowFrom: 60, yellowTo: 79,")
        sb.AppendLine("redFrom: 80, redTo: 100")
        sb.AppendLine("};")
        sb.AppendLine(" var chart = new google.visualization.Gauge(document.getElementById('chart_div'));")
        sb.AppendLine(" chart.draw(data, options);")
        sb.AppendLine("  }")
        sb.AppendLine(" </script>")
        sb.AppendLine("<div id='chart_div'></div>")

        litChart.Text = sb.ToString

    End Sub

    Public Sub RowBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim pid As String = e.Row.Cells(4).Text
            If e.Row.Cells(2).Text.ToUpper().Equals("RED") Then
                e.Row.Cells(4).Text = "<a href=""javascript:openAdvice('" + Context.Server.HtmlEncode(e.Row.Cells(4).Text) + "');"">View Advice</div>"
            Else
                e.Row.Cells(4).Text = "-"
            End If

            e.Row.Cells(2).Text = "<div style=""background-color:" + e.Row.Cells(2).Text + """ >&nbsp;</div>"
            e.Row.Cells(5).Text = "<a href=""javascript:openSQL('" + Context.Server.HtmlEncode(e.Row.Cells(5).Text) + "');"">View Sql</div>"
            e.Row.Cells(3).Text = "<a href=""javascript:window.location.href='historical.aspx?pid=" + pid + "'"");"">View Details</div>"


         

        End If

    End Sub
End Class


