Partial Public Class SP_GetInvoiceDetailsByInvoiceId_Result

#Region "Properties"

    ''' <summary>
    ''' Valor de la factura mas el valor del paciente
    ''' </summary>
    Public ReadOnly Property LineExtensionAmountValue As Decimal
        Get
            Return Me.LineExtensionAmount + Me.PatientValue
        End Get
    End Property

#End Region

End Class
