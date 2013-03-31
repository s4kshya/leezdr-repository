Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports System.Data

Public Class DAO

    Public Shared ConnString As String = "Data Source=orcl;User Id=APP_USER;Password=d;DBA Privilege=SYSDBA"
    Public Shared Avg As Double

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
            'determine color
            If val <= 85 Then
                color = "Green"
            ElseIf val > 85 And val < 95 Then
                color = "Yellow"
            ElseIf val >= 95 Then
                color = "Red"
            End If


            dRow(0) = "Shared Pool"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = Sql
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
            'determine color
            If val >= 90 Then
                color = "Green"
            ElseIf val >= 70 And val < 90 Then
                color = "Yellow"
            ElseIf val < 70 Then
                color = "Red"
            End If
            dRow(0) = "Buffer Cache"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
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
            'determine color
            If val < 0.5 Then
                color = "Green"
            ElseIf val >= 0.5 And val <= 0.75 Then
                color = "Yellow"
            ElseIf val > 0.75 Then
                color = "Red"
            End If
            dRow(0) = "Redo Log Buffer/Files"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
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
            If val >= 90 Then
                color = "Green"
            ElseIf val >= 70 And val < 90 Then
                color = "Yellow"
            ElseIf val < 70 Then
                color = "Red"
            End If
            dRow(0) = "Memory area used for sorting"
            dRow(1) = val.ToString + "%"
            dRow(2) = color
            dRow(3) = sql
            d.Rows.Add(dRow)
        End If

    End Sub

    Public Shared Function GetDataBaseParameters() As DataTable
        Dim d As New DataTable
        d.Columns.Add("ParametersName")
        d.Columns.Add("CurrentValue")
        d.Columns.Add("Indicator")
        d.Columns.Add("Sql")

        AddSharedPoolData(d)
        AddCacheHitRatio(d)
        AddRedoBuffer(d)
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

End Class
