Public Class InventoryStaticServices

    ''' <summary>
    ''' Calcula el precio de venta
    ''' </summary>
    Public Shared Function CalculateGrandTotalValue(ByVal saleprice As Decimal, ByVal quantity As Decimal, ByVal discountValue As Decimal)
        Return (saleprice - discountValue) * quantity
    End Function

    Public Shared Function CalculateDiscountValue(ByVal value As Decimal, ByVal percentage As Decimal)
        Return value * percentage / 100
    End Function

    Public Shared Function CalculateDiscountPercentage(ByVal value As Decimal, ByVal discountValue As Decimal)
        If value <> 0 Then
            Return (discountValue / value) * 100
        Else
            Return 0
        End If
    End Function

    Shared Function CalculateTotalValue(ByVal saleprice As Decimal, ByVal discountValue As Decimal) As Decimal
        Return (saleprice - discountValue)
    End Function

#Region "Enums"

    Public Enum MovementType
        Input = 1
        OutPut = 2
    End Enum

#End Region
    

End Class
