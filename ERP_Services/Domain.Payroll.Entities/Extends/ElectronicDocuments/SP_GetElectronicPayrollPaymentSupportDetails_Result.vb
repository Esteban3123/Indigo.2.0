Imports Infrastructure.CrossCutting.Root

Partial Public Class SP_GetElectronicPayrollPaymentSupportDetails_Result

#Region "Methods"

    ''' <summary>
    ''' Retorna el valor formateado a dos decimales
    ''' </summary>
    ''' <returns></returns>
    Public Function getValueFormated() As String
        Return Utils.GetFormatNumberWithTwoDecimals(Me.Value)
    End Function

#End Region

End Class
