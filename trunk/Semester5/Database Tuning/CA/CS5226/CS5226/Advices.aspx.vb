Imports System.Data
Partial Class Advices
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindData()
    End Sub

    Public Sub BindData()

        If Not Request.QueryString("param") = Nothing Then
            Dim p As String = Request.QueryString("param").ToString
            Dim dTbl As DataTable = DAO.GetAdvice(p)
            Dim sql As String = dTbl.Rows(0)("SQL").ToString()
            lblParam.Text = dTbl.Rows(0)("DESC").ToString()
            lblAdvice.Text = dTbl.Rows(0)("ADVICE").ToString()
            gvResult.DataSource = DAO.ExecuteDataTable(sql)
            gvResult.DataBind()
        End If
    End Sub
End Class
