Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports System.Data

Public Class DAO
    Public Shared ConnString As String = "Data Source=DRC.WORLD;User Id=ITAPPLN_UAT;Password=alltel;"

    Private Shared Function ExecuteDataSet(ByVal pScript As String) As DataSet

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

    Private Shared Function ExecuteDataTable(ByVal pScript As String) As DataTable

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


    Public Shared Sub AddSharedPoolData(ByRef d As DataTable)
        Dim tble As DataTable = ExecuteDataTable("select * from v$sgastat where pool = 'shared pool' and lower(name) = 'free memory'")
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            dRow(0) = "Shared Pool"
            dRow(1) = " - "
            dRow(2) = tble.Rows(0)("BYTES").ToString + " BYTE"
            d.Rows.Add(dRow)

        End If
    End Sub

    Public Shared Sub AddCacheHitRatio(ByRef d As DataTable)
        Dim sql As String = "SELECT ROUND((1-(phy.VALUE / (cur.VALUE + con.VALUE)))*100,2) as Cache_Hit_Ratio FROM v$sysstat cur, v$sysstat con, v$sysstat phy WHERE cur.name = 'db block gets' AND con.name = 'consistent gets' AND phy.name = 'physical reads'"

        Dim tble As DataTable = ExecuteDataTable(sql)
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            dRow(0) = "Cache Hit Ratio"
            dRow(1) = " 95% "
            dRow(2) = tble.Rows(0)("Cache_Hit_Ratio").ToString + "%"
            d.Rows.Add(dRow)

        End If
    End Sub

    Public Shared Sub AddRedoBufferEntry(ByRef d As DataTable)
        Dim sql As String = "select ROUND(a.value/b.value * 100,5) redo_buffer_entries_ratio from v$sysstat a, v$sysstat b where a.name = 'redo buffer allocation retries' and b.name = 'redo entries'"

        Dim tble As DataTable = ExecuteDataTable(sql)
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            dRow(0) = "Redo Buffer Entries Ratio"
            dRow(1) = " Not Greater Than 1 "
            dRow(2) = tble.Rows(0)("redo_buffer_entries_ratio").ToString + "%"
            d.Rows.Add(dRow)

        End If
    End Sub


    Public Shared Sub AddSortingArea(ByRef d As DataTable)
        Dim sql As String = "SELECT ROUND(mem.value*100/(disk.value + mem.value),4) as SortArea FROM v$sysstat disk, v$sysstat mem WHERE mem.name = 'sorts (memory)' AND disk.name = 'sorts (disk)'"

        Dim tble As DataTable = ExecuteDataTable(sql)
        If tble.Rows.Count > 0 Then
            Dim dRow As DataRow = d.NewRow
            dRow(0) = "Sorting Area"
            dRow(1) = "  "
            dRow(2) = tble.Rows(0)("SortArea").ToString
            d.Rows.Add(dRow)

        End If
    End Sub


    Public Shared Function GetDataBaseParameters() As DataTable
        Dim d As New DataTable
        d.Columns.Add("ParametersName")
        d.Columns.Add("OptimalValue")
        d.Columns.Add("CurrentValue")
        d.Columns.Add("Edit")

        AddSharedPoolData(d)
        AddCacheHitRatio(d)
        AddRedoBufferEntry(d)
        AddSortingArea(d)
        Return d
    End Function
End Class
