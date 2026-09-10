Public Class InteropCostStaticService

#Region "Static Methods"

    ''' <summary>
    ''' Calcula el valor de la proporción
    ''' </summary>
    Public Shared Function CalculateRateValue(ByVal count As Integer, ByVal ValueToDistribute As Decimal) As Decimal
        If count = 0 Then
            Throw New ArgumentNullException("Valor de Parámetro inválido count")
        End If
        Return ValueToDistribute / count
    End Function

    Public Shared Function CalculateDistributedValue(ByVal item1 As Decimal, ByVal item2 As Decimal) As Decimal
        Return item1 + item2
    End Function

#End Region
    
End Class
