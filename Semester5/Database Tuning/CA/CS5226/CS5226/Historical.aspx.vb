Imports System.Data
Partial Class Historical
    Inherits System.Web.UI.Page
    Dim cnt As Integer = 0
    Dim xInt As String
    Dim yInt As String
    Dim pid As String
    Dim pName As String
    Dim dt As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Request.QueryString("pid") = Nothing Then

                LoadPage()
            End If
           
        End If
    End Sub

    Public Sub LoadPage()
        'Get all values
        pid = Request.QueryString("pid").ToString
        pName = UTIL.GetPName(pid)
        Dim dTbl As DataTable

        If Not Request.QueryString("dt") = Nothing Then
            dt = Request.QueryString("dt").ToString
            xInt = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'XVAL'", "kval")
            yInt = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'YVAL'", "kval")
            lblXInt.Text = "<b>X Interval:</b> " + xInt + " Minutes, <b>Y Interval:</b> " + yInt + "Minutes"
            dTbl = DAO.GetYParam(pid, ddRec.SelectedValue, dt, xInt)
            ddRec.Enabled = False
            litLInk.Text = "<a href=""javascript:location.href='Historical.aspx?pid=" + pid + "'""><< BACK</div>"

            gvParam.Columns(4).Visible = False

        Else
            xInt = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'XVAL'", "kval")
            lblXInt.Text = "<b>X Interval:</b> " + xInt + " Minutes"

            dTbl = DAO.GetXParam(pid, ddRec.SelectedValue)
        End If



        gvParam.DataSource = dTbl
        gvParam.DataBind()
        CreateChart(dTbl, pName)

    End Sub

    Public Function GetJson(ByVal pTbl As DataTable, ByVal pName As String) As String
        Dim sb As New StringBuilder
        Dim snap_dt As DateTime
        sb.Append("['DATETIME', '" + pName + "']")
        pTbl.DefaultView.Sort = "SNAP_DT DESC"
        For Each r As DataRow In pTbl.Select("", "SNAP_DT ASC")
            snap_dt = Convert.ToDateTime(r("SNAP_DT"))
            sb.Append(",['" + snap_dt.ToString("dd MMM yyy, HH:mm") + "', " + r("VAL").ToString + "]")
        Next

        Return sb.ToString
    End Function

    Public Sub CreateChart(ByVal pTbl As DataTable, ByVal pName As String)
        Dim sb As New StringBuilder
        Dim json As String = GetJson(pTbl, pName)

        sb.Append("<script type=""text/javascript"">" + vbCrLf)
        sb.Append("    google.load(""visualization"", ""1"", {packages:[""corechart""]});" + vbCrLf)
        sb.Append("google.setOnLoadCallback(drawChart);" + vbCrLf)
        sb.Append("function drawChart() {" + vbCrLf)
        sb.Append("var data = google.visualization.arrayToDataTable([" + json + "])" + vbCrLf)

        sb.Append("  var options = {" + vbCrLf)
        sb.Append("title:  '" + pName + "'" + vbCrLf)
        sb.Append("};" + vbCrLf)

        sb.Append("var chart = new google.visualization.LineChart(document.getElementById('chart_div'));" + vbCrLf)
        sb.Append("chart.draw(data, options);" + vbCrLf)
        sb.Append("}" + vbCrLf)
        sb.Append("</script>" + vbCrLf)
        sb.Append("<div id=""chart_div"" style=""width: 900px; height: 300px;""></div>" + vbCrLf)
        litChart.Text = sb.ToString
    End Sub

    Public Sub RowBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim pDT As String = Convert.ToDateTime(e.Row.Cells(2).Text).ToString("dd MMM yyyy hh:00:00 tt")

            cnt = cnt + 1
            e.Row.Cells(0).Text = cnt.ToString
            e.Row.Cells(1).Text = pName
            e.Row.Cells(2).Text = Convert.ToDateTime(e.Row.Cells(2).Text).ToString("dd MMM yyyy, HH:mm")
            e.Row.Cells(4).Text = "<a href=""javascript:location.href='Historical.aspx?pid=" + pid + "&dt=" + pDT + "'"">Breakdown</div>"
            e.Row.Cells(5).Text = "<a href=""javascript:openRpt('X', '" + pDT + "');"">View AWR Report</div>"
        End If

    End Sub

    Protected Sub ddRec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddRec.SelectedIndexChanged
        If Not Request.QueryString("pid") = Nothing Then
            LoadPage()
        End If
    End Sub


End Class

