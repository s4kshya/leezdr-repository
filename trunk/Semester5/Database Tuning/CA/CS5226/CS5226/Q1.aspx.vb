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
        gvParam.DataSource = DAO.GetDataBaseParameters
        gvParam.DataBind()

    End Sub

    Public Sub RowBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(2).Text.ToUpper().Equals("RED") Then
                e.Row.Cells(4).Text = "<a href=""javascript:openAdvice('" + Context.Server.HtmlEncode(e.Row.Cells(4).Text) + "');"">View Advice</div>"
            Else
                e.Row.Cells(4).Text = "-"
            End If


            ' Display the company name in italics.
            e.Row.Cells(2).Text = "<div style=""background-color:" + e.Row.Cells(2).Text + """ >&nbsp;</div>"
            e.Row.Cells(3).Text = "<a href=""javascript:openSQL('" + Context.Server.HtmlEncode(e.Row.Cells(3).Text) + "');"">View Sql</div>"

         

        End If

    End Sub
End Class


