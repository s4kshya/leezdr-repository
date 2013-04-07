
Imports System.Data

Partial Class awr_report
    Inherits System.Web.UI.Page
    Public rptType As String = "X"
    Public EndDT As String = "07 Apr 2013 06:00:00 PM"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("typ") Is Nothing And Not Request.QueryString("dt") Is Nothing Then

            rptType = Request.QueryString("typ").ToString
            EndDT = Request.QueryString("dt").ToString

            Dim inter As String

            If rptType.ToUpper.Equals("X") Then
                inter = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'XVAL'", "kval")
            Else
                inter = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'YVAL'", "kval")
            End If

            Dim Start_Snap_ID As String = String.Empty
            Dim End_Snap_ID As String = String.Empty

            DAO.GetSnapIDs(EndDT, inter, Start_Snap_ID, End_Snap_ID)

            Dim rpt As DataTable = DAO.GetReport(Start_Snap_ID, End_Snap_ID)
            Dim sb As New StringBuilder
            For Each r As DataRow In rpt.Rows
                sb.AppendLine(r(0).ToString)

            Next
            lit.Text = sb.ToString
        End If

    End Sub
End Class
