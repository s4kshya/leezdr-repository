Imports System.Data

Partial Class Q2
    Inherits System.Web.UI.Page


 
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadData()
            LoadXYData()
        End If

    End Sub

    Protected Sub LoadData()
        Dim tbl As DataTable = DAO.ExecuteDataTable("SELECT * FROM DASH_PARAM")
        For Each r As DataRow In tbl.Rows
            CType(Me.Master.FindControl("MainContent").FindControl(r("PARAM").ToString + "_GREEN_S"), TextBox).Text = r("GREEN_S").ToString
            CType(Me.Master.FindControl("MainContent").FindControl(r("PARAM").ToString + "_GREEN_E"), TextBox).Text = r("GREEN_E").ToString
            CType(Me.Master.FindControl("MainContent").FindControl(r("PARAM").ToString + "_YELLOW_S"), TextBox).Text = r("YELLOW_S").ToString
            CType(Me.Master.FindControl("MainContent").FindControl(r("PARAM").ToString + "_YELLOW_E"), TextBox).Text = r("YELLOW_E").ToString
            CType(Me.Master.FindControl("MainContent").FindControl(r("PARAM").ToString + "_RED_S"), TextBox).Text = r("RED_S").ToString
            CType(Me.Master.FindControl("MainContent").FindControl(r("PARAM").ToString + "_RED_E"), TextBox).Text = r("RED_E").ToString
        Next

 
    End Sub

    Protected Sub LoadXYData()
        'Load XY Interval
        Dim xInt As Integer = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'XVAL'", "kval")
        Dim yInt As Integer = DAO.ExecuteSingleVal("SELECT kval FROM SYS.DASH_CONFIG WHERE K = 'YVAL'", "kval")
        ddXInt.SelectedIndex = ((xInt / 60) - 1)
        PopYInt()
        ddYHrInt.SelectedIndex = ((Convert.ToInt16(yInt / 60)))
        ddYMinInt.SelectedIndex = ((yInt Mod 60) / 10)
    End Sub
    Public Sub PopYInt()

        ddYHrInt.Items.Clear()
        ddYHrInt.Items.Add(New ListItem("0 Hour", "0"))

        For i As Integer = 0 To ddXInt.SelectedIndex - 1
            ddYHrInt.Items.Add(New ListItem(ddXInt.Items(i).Text, ddXInt.Items(i).Value))
        Next
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim err As String = Valid()
        If err.Length > 0 Then
            lblRemarks.ForeColor = Drawing.Color.Red
            lblRemarks.Text = "<br>Database Parameters Color indicate Settings cannot be saved.<br>" + err + "<br>"
        Else
            UpdatePage()
            lblRemarks.ForeColor = Drawing.Color.Blue
            lblRemarks.Text = "Database Parameters Color indicate Settings Saved Successfully.<br>"
        End If
    End Sub

    Protected Sub btnXYSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave0.Click
        Dim x As Integer = Convert.ToInt16(ddXInt.SelectedValue)
        Dim y As Integer = Convert.ToInt16(ddYHrInt.SelectedValue) + Convert.ToInt16(ddYMinInt.SelectedValue)
        Dim err As String = DAO.SaveXY(x, y)
        If err.Length > 0 Then
            lblRemarks.ForeColor = Drawing.Color.Red
            lblRemarks.Text = "<br>SNAPSHOT X & Y INTERVAL  SETTINGS FOR REPORT  cannot be saved.<br>" + err + "<br>"
        Else
            lblRemarks.ForeColor = Drawing.Color.Blue
            lblRemarks.Text = "SNAPSHOT X & Y INTERVAL  SETTINGS FOR REPORT Saved Successfully.<br>"
        End If
    End Sub

    Protected Function Valid() As String
        Dim err As String = ""
        If UTIL.isPercent(SP_YELLOW_S.Text) = False Then
            err = err + " - Invalid Input, Shared Pool (%) - Yellow Start<br>"
        End If
        If UTIL.isPercent(SP_YELLOW_E.Text) = False Then
            err = err + " - Invalid Input, Shared Pool (%) - Yellow End<br>"
        End If

        If UTIL.isPercent(RB_YELLOW_S.Text) = False Then
            err = err + " - Invalid Input, Redo Log Buffer/Files - Yellow Start<br>"
        End If
        If UTIL.isPercent(RB_YELLOW_E.Text) = False Then
            err = err + " - Invalid Input, Redo Log Buffer/Files- Yellow End<br>"
        End If

        If UTIL.isPercent(BC_YELLOW_S.Text) = False Then
            err = err + " - Invalid Input, Buffer Cache (%)- Yellow Start<br>"
        End If
        If UTIL.isPercent(BC_YELLOW_E.Text) = False Then
            err = err + " - Invalid Input, Buffer Cache (%) - Yellow End<br>"
        End If

        If UTIL.isPercent(SORT_YELLOW_S.Text) = False Then
            err = err + " - Invalid Input, Memory area used for Sorting- Yellow Start<br>"
        End If
        If UTIL.isPercent(SORT_YELLOW_E.Text) = False Then
            err = err + " - Invalid Input, Memory area used for Sorting - Yellow End<br>"
        End If

        Return err
    End Function

    Protected Sub UpdatePage()

        DAO.ExecuteNonQuery("UPDATE DASH_PARAM SET RED_S='" + (UTIL.TCDBL(SP_YELLOW_E.Text) + 0.01).ToString + "', GREEN_E='" + (UTIL.TCDBL(SP_YELLOW_S.Text) - 0.01).ToString + "', YELLOW_S='" + SP_YELLOW_S.Text + "', YELLOW_E='" + SP_YELLOW_E.Text + "' WHERE PARAM='SP' ")
        DAO.ExecuteNonQuery("UPDATE DASH_PARAM SET RED_S='" + (UTIL.TCDBL(RB_YELLOW_E.Text) + 0.01).ToString + "', GREEN_E='" + (UTIL.TCDBL(RB_YELLOW_S.Text) - 0.01).ToString + "', YELLOW_S='" + RB_YELLOW_S.Text + "', YELLOW_E='" + RB_YELLOW_E.Text + "' WHERE PARAM='RB' ")
        DAO.ExecuteNonQuery("UPDATE DASH_PARAM SET GREEN_S='" + (UTIL.TCDBL(BC_YELLOW_E.Text) + 0.01).ToString + "', RED_E='" + (UTIL.TCDBL(BC_YELLOW_S.Text) - 0.01).ToString + "', YELLOW_S='" + BC_YELLOW_S.Text + "', YELLOW_E='" + BC_YELLOW_E.Text + "' WHERE PARAM='BC'")
        DAO.ExecuteNonQuery("UPDATE DASH_PARAM SET GREEN_S='" + (UTIL.TCDBL(SORT_YELLOW_E.Text) + 0.01).ToString + "', RED_E='" + (UTIL.TCDBL(SORT_YELLOW_S.Text) - 0.01).ToString + "', YELLOW_S='" + SORT_YELLOW_S.Text + "', YELLOW_E='" + SORT_YELLOW_E.Text + "' WHERE PARAM='SORT' ")


        LoadData()
    End Sub

    Protected Sub ddXInt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddXInt.SelectedIndexChanged
        PopYInt()
    End Sub
End Class

