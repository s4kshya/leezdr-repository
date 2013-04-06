Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports System.Data
Imports System.Drawing.Color

Public Class DAO

    Public Shared ConnString As String = "Data Source=orcl;User Id=APP_USER;Password=d"
    ' Public Shared ConnString As String = "Data Source=DRC.WORLD;User Id=ITAPPLN_UAT;Password=alltel;"


    'Public Shared ConnString As String = "Data Source=137.132.247.154:3306;User Id=cs5226;Password=cs5226"
    ' Public Shared ConnString As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=137.132.247.154)(PORT=3306))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=cs5226;Password=cs5226;"


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

    Public Shared Sub AddSharedPoolData(ByRef d As DataTable)
        Dim sql As String = "select round ((info.bytes - stat.bytes)*100/info.bytes, 2) ratio  from v$sgainfo info, v$sgastat stat  where info.name = 'Shared Pool Size' and stat.pool = 'shared pool' and lower(stat.name) = 'free memory'"
        Dim tble As DataTable = ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("RATIO").ToString)
            color = DAO.GetColor("SP", val)


            dRow(0) = "Shared Pool"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "SP"
            d.Rows.Add(dRow)


        End If

    End Sub

    Public Shared Sub AddCacheHitRatio(ByRef d As DataTable)
        Dim sql As String = "SELECT ROUND((1-(phy.VALUE / (cur.VALUE + con.VALUE)))*100,2) as ratio FROM v$sysstat cur, v$sysstat con, v$sysstat phy WHERE cur.name = 'db block gets' AND con.name = 'consistent gets' AND phy.name = 'physical reads'"
        Dim tble As DataTable = DAO.ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("ratio").ToString)
            color = DAO.GetColor("BC", val)
            dRow(0) = "Buffer Cache"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "BC"
            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Sub AddRedoBuffer(ByRef d As DataTable)
        Dim sql As String = "select round (a.value/b.value * 100, 2) buffer from v$sysstat a, v$sysstat b where a.name = 'redo buffer allocation retries' and b.name = 'redo entries'"
        Dim tble As DataTable = DAO.ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("buffer").ToString)
            color = DAO.GetColor("RB", val)
            dRow(0) = "Redo Log Buffer/Files"
            dRow(1) = val.ToString
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "RB"
            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Sub AddSortingArea(ByRef d As DataTable)
        Dim sql As String = "SELECT round (mem.value*100/(disk.value + mem.value), 2) sort FROM v$sysstat disk, v$sysstat mem WHERE mem.name = 'sorts (memory)' AND disk.name = 'sorts (disk)'"
        Dim tble As DataTable = DAO.ExecuteDataTable(sql)
        Dim val As Double
        Dim color As String = String.Empty
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            val = cDeci(tble.Rows(0)("sort").ToString)
            'determine color
            color = DAO.GetColor("SORT", val)
            dRow(0) = "Memory area used for sorting"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            dRow(4) = "SORT"

            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Function GetDataBaseParameters() As DataTable
        Dim d As New DataTable
        d.Columns.Add("ParametersName")
        d.Columns.Add("CurrentValue")
        d.Columns.Add("Indicator")
        d.Columns.Add("Sql")
        d.Columns.Add("Advice")

        'Load Param Color Table
        tblParamColor = DAO.GetPramColor


        AddSharedPoolData(d)
        AddRedoBuffer(d)
        AddCacheHitRatio(d)
        AddSortingArea(d)
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
        Return DAO.ExecuteDataTable("SELECT * FROM SYS.DASH_PARAM")

    End Function

    Public Shared Function GetColor(ByVal pParam As String, ByVal pVal As Double) As String

        Dim dRows As DataRow() = tblParamColor.Select(" PARAM='" + pParam + "' ")
        If pVal >= UTIL.TCDBL(dRows(0)("Green_S").ToString) And pVal <= UTIL.TCDBL(dRows(0)("Green_E").ToString) Then
            Return "Green"
        ElseIf pVal >= UTIL.TCDBL(dRows(0)("Yellow_S").ToString) And pVal <= UTIL.TCDBL(dRows(0)("Yellow_E").ToString) Then
            Return "Yellow"
        ElseIf pVal >= UTIL.TCDBL(dRows(0)("Red_S").ToString) And pVal <= UTIL.TCDBL(dRows(0)("Red_E").ToString) Then
            Return "Red"
        End If
        Return ""
    End Function

    Public Shared Function GetAdvice(ByVal param As String) As DataTable
        Return DAO.ExecuteDataTable("SELECT * FROM SYS.DASH_SQL WHERE PARAM='" + param + "' ")
    End Function

End Class
