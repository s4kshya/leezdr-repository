Imports System.Data
Partial Class Q5
    Inherits System.Web.UI.Page

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim err As String = String.Empty
        Dim dTbl As DataTable
        Dim sql As String = tbSql.Text.Trim
        dTbl = DAO.ExecuteDataTable(sql, err)
        If err.Length > 0 Then
            lblError.Text = err
        Else
            gvResult.DataSource = dTbl
            gvResult.AutoGenerateColumns = True
            gvResult.DataBind()
        End If

    End Sub
End Class
