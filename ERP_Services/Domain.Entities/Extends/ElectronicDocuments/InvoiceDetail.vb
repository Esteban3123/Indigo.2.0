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

    ''' <summary>
    ''' Valor de la factura mas valor del paciente y descuento. Solo para facturas capitadas (DocumentType = 4).
    ''' </summary>
    Public ReadOnly Property LineExtensionAmountValueCapitation As Decimal
        Get
            Return Me.LineExtensionAmount + Me.PatientValue + If(Me.DiscountValue.HasValue, Me.DiscountValue.Value, 0)
        End Get
    End Property

#End Region

End Class
