Imports Domain.Entities
Imports DevExpress.Xpo

''' <summary>
''' evento utilizado para los conceptos de egreso
''' </summary>
Public Class AddExpenseConceptEventArgs
    Inherits EventArgs

    Property ListDischargeBillDelete As List(Of DischargeBill)
    Property VoucherTransactionD As VoucherTransactionDetails
    Property ListDischargeBill As List(Of DischargeBill)
    Property IsEditMode As Boolean
    Property ValueAdvancePayment As Decimal
    Property AdvanceDetail As String
    Property ListRefund As List(Of Refunds)
    Property ListVoucherTransactionAdvance As List(Of VoucherTransactionAdvance)

End Class

''' <summary>
''' Evento que se ejecuta para actualizar el datasource de las facturas de la programacion de pagos
''' </summary>
Public Class UpdateDatasourceEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Obtiene o establece el datasource nuevo del detalle de las facturas
    ''' </summary>
    ''' <value>
    ''' The invoice share datasource new.
    ''' </value>
    Public Property NewInvoiceShareDatasource As List(Of SP_SchedulePayment_Result)
    ''' <summary>
    ''' obtiene o establece el datasource actual del detalle de las facturas
    ''' </summary>
    ''' <value>
    ''' The current invoice share datasource.
    ''' </value>
    Public Property CurrentInvoiceShareDatasource As List(Of SP_SchedulePayment_Result)

End Class

''' <summary>
''' Evento utilizado para pagar el porcentaje a las facturas de la programacion de pagos
''' </summary>
Public Class PayPercentEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' valor del porcentaje a pagar
    ''' </summary>
    Public Property percentValue As Integer
    ''' <summary>
    ''' Obtiene o establece el tipo de opcion (1 - Pago total, 2 - pago por porcentaje)
    ''' </summary>
    Public Property Type As Integer
    ''' <summary>
    ''' obtiene o establece el id del concepto de pago
    ''' </summary>
    Public Property PaymentConceptId As Integer

End Class
