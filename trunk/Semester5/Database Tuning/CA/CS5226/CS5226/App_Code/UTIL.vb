Imports Microsoft.VisualBasic
Imports System.Data
Public Class UTIL

    Public Shared Function isPercent(ByVal pVal As String) As Boolean
        If Not isPositive(pVal) Then
            Return False
        End If
        If Convert.ToDecimal(pVal) > 100 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function TCDBL(ByVal pInput As String) As Double
        Dim c As Double
        Try
            c = Convert.ToDouble(pInput)
        Catch ex As Exception
            c = 0
        End Try
        Return c
    End Function


    Public Shared Function isNumber(ByVal pVal As String) As Boolean
        Return Regex.Match(pVal, "^[-+]?[0-9]*\.?[0-9]+$").Success
    End Function


    Public Shared Function isPositive(ByVal pVal As String) As Boolean
        If pVal.Length = 0 Then
            Return False
        End If

        If (isNumber(pVal)) Then
            If Convert.ToDecimal(pVal) >= 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function isPositiveInteger(ByVal pVal As String) As Boolean
        Dim m As Match = Regex.Match(pVal, "^(0)$|^([1-9][0-9]*)$")
        Return m.Success
    End Function

    Public Shared Function JSON_DataTable(ByVal dt As DataTable) As String

        Dim JsonString As New StringBuilder()

        JsonString.Append("{ ")
        JsonString.Append("""TABLE"":[{ ")
        JsonString.Append("""ROW"":[ ")

        For i As Integer = 0 To dt.Rows.Count - 1

            JsonString.Append("{ ")
            JsonString.Append("""COL"":[ ")

            For j As Integer = 0 To dt.Columns.Count - 1
                If j < dt.Columns.Count - 1 Then
                    JsonString.Append("{" & """DATA"":""" & dt.Rows(i)(j).ToString() & """},")
                ElseIf j = dt.Columns.Count - 1 Then
                    JsonString.Append("{" & """DATA"":""" & dt.Rows(i)(j).ToString() & """}")
                End If
            Next
            'end Of String

            If i = dt.Rows.Count - 1 Then
                JsonString.Append("]} ")
            Else
                JsonString.Append("]}, ")
            End If
        Next
        JsonString.Append("]}]}")
        Return JsonString.ToString()

    End Function

    Public Shared Function GetPName(ByVal pP As String) As String
        Select Case pP
            Case "SP"
                Return "Shared Pool"
            Case "RB"
                Return "Redo Log Buffer"
            Case "BC"
                Return "Buffer Cache"
            Case "SORT"
                Return "Memory area used for Sortingl"
        End Select
    End Function
End Class
