Imports System.Data
Partial Class Q1
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Public Sub LoadGrid()
        gvParam.DataSource = DAO.GetDataBaseParameters
        gvParam.DataBind()

    End Sub
End Class
