#Region "Imports"
Imports Domain.Base
Imports Domain.Base.Entities
#End Region

Public Class TreasuryStaticServices

#Region "Static Methods"
    ''' <summary>
    ''' Convierte de valor a porcentaje con respecto al valor de la factura
    ''' </summary>
    ''' <param name="PayValue">The pay value.</param>
    ''' <returns></returns>
    Public Shared Function ConvertInvoiceValueToPercent(ByVal PayValue As Decimal, ByVal value As Decimal) As Decimal
        Dim resultOperation As Decimal
        resultOperation = Math.Round((PayValue / value) * 100, 2, MidpointRounding.AwayFromZero)
        Return resultOperation
    End Function

    ''' <summary>
    ''' Convierte el porcentaje a pagar a valor a pagar con referencia al valor de la factura
    ''' </summary>
    ''' <param name="percentValue">The percent value.</param>
    ''' <param name="value">The value.</param>
    ''' <returns></returns>
    Public Shared Function ConvertInvoicePercentToValue(ByVal percentValue As Decimal, ByVal value As Decimal) As Decimal
        Dim resultOperation As Decimal
        resultOperation = Math.Round((percentValue * value) / 100, 2, MidpointRounding.AwayFromZero)
        Return resultOperation
    End Function

    ''' <summary>
    ''' Calculates the rate by mil value.
    ''' </summary>
    Public Shared Function CalculateRateByMilValue(ByVal RateXMil As Decimal, ByVal value As Decimal) As Tuple(Of Decimal, Decimal)
        Dim resultOperation As Tuple(Of Decimal, Decimal)
        Dim rateValue As Decimal = Math.Round((RateXMil * value) / 100, 2, MidpointRounding.AwayFromZero)
        Dim voucherValue As Decimal = value - rateValue
        resultOperation = New Tuple(Of Decimal, Decimal)(rateValue, voucherValue)
        Return resultOperation
    End Function

    ''' <summary>
    ''' Calcula  el valor de la factura
    ''' </summary>
    ''' <param name="AdvanceValue">The advance value.</param>
    ''' <returns></returns>
    Public Shared Function CalculateExpenseValue(ByVal PayValueInvoice As Decimal, ByVal AdvanceValue As Decimal) As Decimal
        Return (PayValueInvoice + AdvanceValue)
    End Function

    Public Shared Function CalculateInvoiceValue(ByVal Percent As Decimal, ByVal RetentionValue As Decimal) As Decimal
        If Percent = 0 Then
            Return RetentionValue
        End If
        Return Math.Round((RetentionValue * 100) / Percent, 2, MidpointRounding.AwayFromZero)
    End Function

    ''' <summary>
    ''' metodo para obtener el valor del porcentaje 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="percent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PercentValue(value As Decimal, percent As Decimal) As Decimal
        Return Math.Round((value * percent) / 100, 2, MidpointRounding.AwayFromZero)
    End Function
    ''' <summary>
    ''' valida si el valor base es mayor que el valor pagado
    ''' </summary>
    ''' <param name="baseValue">valor base</param>
    ''' <param name="value">valor a comparar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateBaseValue(baseValue As Decimal, value As Decimal) As Boolean
        If baseValue > value Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' valida que los saldos de las facturas sean superiores a los valores a pagar del cada cuota
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ValidateBalanceAccountPayableShare(ByVal BalanceAccountPayable As Decimal, ByVal AdvanceValue As Decimal) As Boolean
        If BalanceAccountPayable < AdvanceValue Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' valida que la cuenta bancaria tenga el saldo suficiente para realizar el comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ValidateBalanceEntityBankAcount(ByVal PayValue As Decimal, ByVal BalanceEntityAccount As Decimal) As Boolean
        If PayValue > BalanceEntityAccount Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' valida que la caja tenga el saldo suficiente para realizar el comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ValidateBalanceCashRegister(ByVal PayValue As Decimal, ByVal BalanceCash As Decimal) As Boolean
        If PayValue > BalanceCash Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' valida que el reembolso que se vaya a efectuar no sea mayor a la cuantia maxima
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ValidateRefundCashRegisterAmountMax(ByVal RefundValue As Decimal, ByVal AmountMax As Decimal) As Boolean
        If RefundValue > AmountMax Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Valida el valor del concepto de nota de tesoreria
    ''' </summary>
    ''' <param name="list">The list.</param>
    ''' <returns></returns>
    Public Shared Function ValidateValueNoteConcept(ByVal list As List(Of TreasuryNoteDetail)) As ActionResult(Of Object)
        Dim diference = list.Where(Function(x) x.Nature = 1).Cast(Of TreasuryNoteDetail).ToList().Sum(Function(y) y.Value) - list.Where(Function(x) x.Nature = 2).Cast(Of TreasuryNoteDetail).ToList().Sum(Function(y) y.Value)
        If diference > 0 Then
            Dim result As Object() = {2, Math.Abs(diference)}
            Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = result}
        ElseIf diference < 0 Then
            Dim result As Object() = {1, Math.Abs(diference)}
            Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = result}
        Else
            Dim result As Object() = {0, Math.Abs(diference)}
            Return New ActionResult(Of Object) With {.StateResult = False, .Message = "El valor de la nota no es válido", .ObjectEmbbeded = result}
        End If
    End Function

#End Region

End Class
