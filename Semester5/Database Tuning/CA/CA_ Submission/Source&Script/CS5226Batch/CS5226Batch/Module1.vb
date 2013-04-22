Imports Oracle.DataAccess.Client
Imports System.Net
Imports System.IO

Module Module1

    Public Const ConnString As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=137.132.247.154)(PORT=3306))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=cs5226;Password=cs5226;"


    Private tblParamColor As DataTable

    Public Avg As Double


    Public Sub SendSms(txt As String, Hp As String)
        Dim wrGETURL As WebRequest
        wrGETURL = WebRequest.Create("http://137.132.247.154:8888/SMS/send.aspx?rep=" + Hp + "&msg=" + txt + "")
        Dim objStream As Stream
        objStream = wrGETURL.GetResponse.GetResponseStream()

    End Sub

    Public Function TCDBL(ByVal pInput As String) As Double
        Dim c As Double
        Try
            c = Convert.ToDouble(pInput)
        Catch ex As Exception
            c = 0
        End Try
        Return c
    End Function

    Public Function GetColor(ByVal pParam As String, ByVal pVal As Double, ByRef agg As Double) As String

        Dim dRows As DataRow() = tblParamColor.Select(" PARAM='" + pParam + "' ")
        If pVal >= TCDBL(dRows(0)("Green_S").ToString) And pVal <= TCDBL(dRows(0)("Green_E").ToString) Then
            Return "#27F68B"
        ElseIf pVal >= TCDBL(dRows(0)("Yellow_S").ToString) And pVal <= TCDBL(dRows(0)("Yellow_E").ToString) Then
            agg = agg + 1
            Return "#FCFC0C"
        ElseIf pVal >= TCDBL(dRows(0)("Red_S").ToString) And pVal <= TCDBL(dRows(0)("Red_E").ToString) Then
            agg = agg + 2
            Return "#F6274D"
        End If
        Return ""
    End Function


    Public Function GetDBHealth()
        Dim db As DataTable = ExecuteDataTable("SELECT * FROM DASH_PARAM")
        'sharedpool
        Dim sp As String = "select round ((info.bytes - stat.bytes)*100/info.bytes, 2) ratio  from v$sgainfo info, v$sgastat stat  where info.name = 'Shared Pool Size' and stat.pool = 'shared pool' and lower(stat.name) = 'free memory'"
        sp = ExecuteSingleVal(sp, "ratio")
        Dim bc As String = "SELECT ROUND((1-(phy.VALUE / (cur.VALUE + con.VALUE)))*100,2) as ratio FROM v$sysstat cur, v$sysstat con, v$sysstat phy WHERE cur.name = 'db block gets' AND con.name = 'consistent gets' AND phy.name = 'physical reads'"
        bc = ExecuteSingleVal(bc, "ratio")
        Dim rb As String = "select round (a.value/b.value * 100, 2) buffer from v$sysstat a, v$sysstat b where a.name = 'redo buffer allocation retries' and b.name = 'redo entries'"
        rb = ExecuteSingleVal(rb, "buffer")
        Dim sort As String = "SELECT round (mem.value*100/(disk.value + mem.value), 2) sort FROM v$sysstat disk, v$sysstat mem WHERE mem.name = 'sorts (memory)' AND disk.name = 'sorts (disk)'"
        sort = ExecuteSingleVal(sort, "buffer")

    End Function
    Sub Main()





        Dim sqlConn As OracleConnection = New OracleConnection(ConnString)

        Dim command As OracleCommand
        Dim cnt As Integer
        Using connection As New OracleConnection(ConnString)
            connection.Open()

            command = New OracleCommand("INSERT INTO SYS.DASH_PARAM_V VALUES ('SP',(select round ((info.bytes - stat.bytes)*100/info.bytes, 2) ratio  from v$sgainfo info, v$sgastat stat  where info.name = 'Shared Pool Size' and stat.pool = 'shared pool' and lower(stat.name) = 'free memory'),sysdate)", connection)
            cnt = command.ExecuteNonQuery()

            command = New OracleCommand("INSERT INTO SYS.DASH_PARAM_V VALUES ('RB',(select round (a.value/b.value * 100, 2) buffer from v$sysstat a, v$sysstat b where a.name = 'redo buffer allocation retries' and b.name = 'redo entries'),sysdate)", connection)
            cnt = command.ExecuteNonQuery()


            command = New OracleCommand("INSERT INTO SYS.DASH_PARAM_V VALUES ('SORT',(SELECT round (mem.value*100/(disk.value + mem.value), 2) sort FROM v$sysstat disk, v$sysstat mem WHERE mem.name = 'sorts (memory)' AND disk.name = 'sorts (disk)'),sysdate)", connection)
            cnt = command.ExecuteNonQuery()


            command = New OracleCommand("INSERT INTO SYS.DASH_PARAM_V VALUES ('BC',(SELECT ROUND((1-(phy.VALUE / (cur.VALUE + con.VALUE)))*100,2) as ratio FROM v$sysstat cur, v$sysstat con, v$sysstat phy WHERE cur.name = 'db block gets' AND con.name = 'consistent gets' AND phy.name = 'physical reads'),sysdate)", connection)
            cnt = command.ExecuteNonQuery()

            If connection.State = ConnectionState.Broken Or connection.State = ConnectionState.Open Then
                connection.Close()
            End If

        End Using









    End Sub

    Public Function ExecuteDataSet(ByVal pScript As String) As DataSet

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

    Public Function ExecuteSingleVal(ByVal pScript As String, ByVal ColName As String) As String

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

    Public Function ExecuteDataTable(ByVal pScript As String) As DataTable

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

End Module
