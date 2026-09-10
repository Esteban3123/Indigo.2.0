Imports Infrastructure.CrossCutting.Base

Partial Public Class ElectronicPayrollPaymentSupport

#Region "For ElectronicPayroll"

    Public MoreInformation As SP_GetElectronicPayrollPaymentSupport_Result

    Public Details As List(Of SP_GetElectronicPayrollPaymentSupportDetails_Result)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el valor total de los devengos
    ''' </summary>
    ''' <returns></returns>
    Public Function getAcrualValue() As Decimal
        Dim total As Decimal = 0
        If Me.Details IsNot Nothing Then
            total = Me.Details.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
        End If
        Return total
    End Function

    ''' <summary>
    ''' Obtiene el valor total de las deducciones
    ''' </summary>
    ''' <returns></returns>
    Public Function getDeductionValue() As Decimal
        Dim total As Decimal = 0
        If Me.Details IsNot Nothing Then
            total = Me.Details.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
        End If
        Return total
    End Function

    ''' <summary>
    ''' Obtiene el valor total
    ''' </summary>
    ''' <returns></returns>
    Public Function getTotalValue() As Decimal
        Return Me.getAcrualValue() - Me.getDeductionValue()
    End Function

#End Region

End Class
