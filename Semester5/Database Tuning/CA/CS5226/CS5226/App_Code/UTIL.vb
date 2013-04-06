Imports Microsoft.VisualBasic

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

End Class
