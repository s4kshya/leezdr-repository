Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports System.Data
Imports System.Drawing.Color

Public Class DAO

    'Public Shared ConnString As String = "Data Source=orcl;User Id=APP_USER;Password=d"
    ' Public Shared ConnString As String = "Data Source=DRC.WORLD;User Id=ITAPPLN_UAT;Password=alltel;"


    'Public Shared ConnString As String = "Data Source=137.132.247.154:3306;User Id=cs5226;Password=cs5226"
    Public Shared ConnString As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=137.132.247.154)(PORT=3306))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=cs5226;Password=cs5226;"


    Private Shared tblParamColor As DataTable

    Public Shared Avg As Double

    Public Shared Function ExecuteNonQuery(ByVal pScript As String) As Integer

        Dim sqlConn As OracleConnection = New OracleConnection(ConnString)

        Using connection As New OracleConnection(ConnString)
            Dim command As New OracleCommand(pScript, connection)
            command.Connection.Open()
            Return command.ExecuteNonQuery()
        End Using

    End Function

    Public Shared Function ExecuteDataSet(ByVal pScript As String) As DataSet

        Dim ds As New DataSet
        Dim sqlConn As OracleConnection = New OracleConnection(ConnString)
        Dim sqlCmd As OracleCommand
        Try
            sqlCmd = New OracleCommand(pScript, sqlConn)
            sqlCmd.CommandTimeout = 9999

            Dim adapter As New OracleDataAdapter(sqlCmd)
            adapter.Fill(ds, "result")


        Catch ex As Exception

        Finally
            If sqlConn.State = ConnectionState.Open Or sqlConn.State = ConnectionState.Executing Then
                sqlConn.Close()
            End If
        End Try
        Return ds
    End Function

    Public Shared Function ExecuteSingleVal(ByVal pScript As String, ByVal ColName As String) As String

        Dim ds As New DataSet
        Dim sqlConn As OracleConnection = New OracleConnection(ConnString)
        Dim sqlCmd As OracleCommand
        Try
            sqlCmd = New OracleCommand(pScript, sqlConn)
            sqlCmd.CommandTimeout = 9999

            Dim adapter As New OracleDataAdapter(sqlCmd)
            adapter.Fill(ds, "result")

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0).Rows(0)(ColName).ToString

            End If
        Catch ex As Exception

        Finally
            If sqlConn.State = ConnectionState.Open Or sqlConn.State = ConnectionState.Executing Then
                sqlConn.Close()
            End If
        End Try
        Return ""
    End Function

    Public Shared Function ExecuteDataTable(ByVal pScript As String) As DataTable

        Dim ds As New DataSet
        Dim sqlConn As OracleConnection = New OracleConnection(ConnString)
        Dim sqlCmd As OracleCommand
        Try
            sqlCmd = New OracleCommand(pScript, sqlConn)
            sqlCmd.CommandTimeout = 9999

            Dim adapter As New OracleDataAdapter(sqlCmd)
            adapter.Fill(ds, "result")

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)

            End If
        Catch ex As Exception

        Finally
            If sqlConn.State = ConnectionState.Open Or sqlConn.State = ConnectionState.Executing Then
                sqlConn.Close()
            End If
        End Try
        Return New DataTable
    End Function

    Public Shared Function ExecuteDataTable(ByVal pScript As String, ByRef err As String) As DataTable

        Dim ds As New DataSet
        Dim sqlConn As OracleConnection = New OracleConnection(ConnString)
        Dim sqlCmd As OracleCommand
        Try
            sqlCmd = New OracleCommand(pScript, sqlConn)
            sqlCmd.CommandTimeout = 9999

            Dim adapter As New OracleDataAdapter(sqlCmd)
            adapter.Fill(ds, "result")

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)

            End If
        Catch ex As Exception
            err = ex.Message
        Finally
            If sqlConn.State = ConnectionState.Open Or sqlConn.State = ConnectionState.Executing Then
                sqlConn.Close()
            End If
        End Try
        Return New DataTable
    End Function

    Public Shared Sub AddSharedPoolData(ByRef d As DataTable, ByRef agg As Double)
        Dim sql As String = "select round ((info.bytes - stat.bytes)*100/info.bytes, 2) ratio  from v$sgainfo info, v$sgastat stat  where info.name = 'Shared Pool Size' and stat.pool = 'shared pool' and lower(stat.name) = 'free memory'"
        Dim tble As DataTable = ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("RATIO").ToString)
            color = DAO.GetColor("SP", val, agg)


            dRow(0) = "Shared Pool"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "SP"
            d.Rows.Add(dRow)


        End If

    End Sub

    Public Shared Sub AddCacheHitRatio(ByRef d As DataTable, ByRef agg As Double)
        Dim sql As String = "SELECT ROUND((1-(phy.VALUE / (cur.VALUE + con.VALUE)))*100,2) as ratio FROM v$sysstat cur, v$sysstat con, v$sysstat phy WHERE cur.name = 'db block gets' AND con.name = 'consistent gets' AND phy.name = 'physical reads'"
        Dim tble As DataTable = DAO.ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("ratio").ToString)
            color = DAO.GetColor("BC", val, agg)
            dRow(0) = "Buffer Cache"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "BC"
            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Sub AddRedoBuffer(ByRef d As DataTable, ByRef agg As Double)
        Dim sql As String = "select round (a.value/b.value * 100, 2) buffer from v$sysstat a, v$sysstat b where a.name = 'redo buffer allocation retries' and b.name = 'redo entries'"
        Dim tble As DataTable = DAO.ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("buffer").ToString)
            color = DAO.GetColor("RB", val, agg)
            dRow(0) = "Redo Log Buffer/Files"
            dRow(1) = val.ToString
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "RB"
            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Sub AddSortingArea(ByRef d As DataTable, ByRef agg As Double)
        Dim sql As String = "SELECT round (mem.value*100/(disk.value + mem.value), 2) sort FROM v$sysstat disk, v$sysstat mem WHERE mem.name = 'sorts (memory)' AND disk.name = 'sorts (disk)'"
        Dim tble As DataTable = DAO.ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("sort").ToString)
            'determine color
            color = DAO.GetColor("SORT", val, agg)
            dRow(0) = "Memory area used for sorting"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "SORT"

            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Function GetDataBaseParameters(ByRef agg As Double) As DataTable
        Dim d As New DataTable
        d.Columns.Add("ParametersName")
        d.Columns.Add("CurrentValue")
        d.Columns.Add("Indicator")
        d.Columns.Add("Sql")
        d.Columns.Add("Advice")

        'Load Param Color Table
        tblParamColor = DAO.GetPramColor


        AddSharedPoolData(d, agg)
        AddRedoBuffer(d, agg)
        AddCacheHitRatio(d, agg)
        AddSortingArea(d, agg)

        agg = IIf(agg = 0, 0, (agg / 8) * 100)
        Return d
    End Function

    Public Shared Function cDeci(ByVal i As String) As Double
        Try
            Return Convert.ToDouble(i)
        Catch ex As Exception
            Return 0

        End Try
    End Function

    Public Shared Function GetPramColor() As DataTable
        Return DAO.ExecuteDataTable("SELECT * FROM DASH_PARAM")

    End Function

    Public Shared Function GetColor(ByVal pParam As String, ByVal pVal As Double, ByRef agg As Double) As String

        Dim dRows As DataRow() = tblParamColor.Select(" PARAM='" + pParam + "' ")
        If pVal >= UTIL.TCDBL(dRows(0)("Green_S").ToString) And pVal <= UTIL.TCDBL(dRows(0)("Green_E").ToString) Then
            Return "Green"
        ElseIf pVal >= UTIL.TCDBL(dRows(0)("Yellow_S").ToString) And pVal <= UTIL.TCDBL(dRows(0)("Yellow_E").ToString) Then
            agg = agg + 1
            Return "Yellow"
        ElseIf pVal >= UTIL.TCDBL(dRows(0)("Red_S").ToString) And pVal <= UTIL.TCDBL(dRows(0)("Red_E").ToString) Then
            agg = agg + 2
            Return "Red"
        End If
        Return ""
    End Function

    Public Shared Function GetAdvice(ByVal param As String) As DataTable
        Return DAO.ExecuteDataTable("SELECT * FROM DASH_SQL WHERE PARAM='" + param + "' ")
    End Function

    Public Shared Function GetYParam(ByVal pParam As String, ByVal Top As String, ByVal dt As String, ByVal xInt As String) As DataTable
        Dim sql As New StringBuilder

        sql.AppendLine("SELECT * FROM")
        sql.AppendLine("(")
        sql.AppendLine("SELECT * FROM SYS.DASH_PARAM_V ")
        sql.AppendLine(" WHERE MOD(EXTRACT (HOUR FROM SNAP_DT)*60+EXTRACT (MINUTE FROM SNAP_DT),(SELECT to_number(kval) FROM SYS.DASH_CONFIG WHERE K = 'YVAL'))=0 AND PARAM='" + pParam + "'")
        sql.AppendLine(" AND SNAP_DT >= (to_timestamp('" + dt.ToString + "') - interval '" + xInt.ToString + "' MINUTE)  AND SNAP_DT <=(to_timestamp('" + dt.ToString + "') +interval '1' MINUTE)   ")
        sql.AppendLine(" ORDER BY SNAP_DT DESC")
        sql.AppendLine(") x")


        Return DAO.ExecuteDataTable(sql.ToString)
    End Function

    Public Shared Function GetXParam(ByVal pParam As String, ByVal Top As String) As DataTable
        Dim sql As New StringBuilder

        sql.AppendLine("SELECT * FROM")
        sql.AppendLine("(")
        sql.AppendLine("SELECT * FROM SYS.DASH_PARAM_V ")
        sql.AppendLine("WHERE MOD(EXTRACT (HOUR FROM SNAP_DT)*60+EXTRACT (MINUTE FROM SNAP_DT),(SELECT to_number(kval) FROM SYS.DASH_CONFIG WHERE K = 'XVAL'))=0 AND PARAM='" + pParam + "'")
        sql.AppendLine("ORDER BY SNAP_DT DESC")
        sql.AppendLine(") x")
        sql.AppendLine("WHERE ROWNUM<=" + Top + " ")


        Return DAO.ExecuteDataTable(sql.ToString)
    End Function
   
    Public Shared Sub GetSnapIDs(ByVal dt As String, ByVal Int As String, ByRef StartSnapID As String, ByRef EndSnapID As String)
        Dim sb As New StringBuilder

        sb.AppendLine("SELECT MIN(SNAP_ID) AS SS, MAX(SNAP_ID) ES FROM ")
        sb.AppendLine("dba_hist_snapshot")
        sb.AppendLine("WHERE(begin_interval_time)")
        sb.AppendLine("BETWEEN (to_timestamp('" + dt + "') - interval '" + Int.ToString + "' MINUTE ) ")
        sb.AppendLine("AND  '" + dt + "' ")

        Dim tbl As DataTable = DAO.ExecuteDataTable(sb.ToString)
        If tbl.Rows.Count > 0 Then
            StartSnapID = tbl.Rows(0)("SS").ToString
            EndSnapID = tbl.Rows(0)("ES").ToString
        End If
    End Sub

    Public Shared Function GetReport(ByVal StartSnapID As String, ByVal EndSnapID As String) As DataTable
        Dim sb As New StringBuilder

        sb.AppendLine("SELECT output FROM table(dbms_workload_repository.awr_report_html ")
        sb.AppendLine(" (")
        sb.AppendLine("(SELECT DBID FROM  v$database), ")
        sb.AppendLine("(SELECT INSTANCE_NUMBER FROM v$instance),  ")
        sb.AppendLine("" + StartSnapID + ", " + EndSnapID + ", 0)")
        sb.AppendLine(") ")

        Return DAO.ExecuteDataTable(sb.ToString)
     
    End Function

    Public Shared Function SaveXY(ByVal x As Integer, ByVal y As Integer) As String
        Dim err As String = ""
        Try
            DAO.ExecuteNonQuery("UPDATE SYS.DASH_CONFIG SET KVAL='" + x.ToString + "' WHERE K='XVAL' ")
            DAO.ExecuteNonQuery("UPDATE SYS.DASH_CONFIG SET KVAL='" + y.ToString + "' WHERE K='YVAL' ")
        Catch ex As Exception
            Return ex.Message
        End Try


        Return ""

    End Function

End Class
