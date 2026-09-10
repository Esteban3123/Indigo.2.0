'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 09-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Interface IDispersionFunds
    Inherits Presentation.Base.IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el parametro de de pagos
    ''' </summary>
    ''' <value>
    ''' The payment concept datasource.
    ''' </value>
    Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As String

    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el codigo de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The scheduled date.
    ''' </value>
    Property ScheduledDate As Date

    ''' <summary>
    ''' Obtiene o establece el centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center identifier.
    ''' </value>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Obtiene o establece la cuenta bancaria de los comprobantes de egreso a generar
    ''' </summary>
    ''' <value>
    ''' The entity bank account identifier.
    ''' </value>
    Property EntityBankAccountId As Integer?

    ''' <summary>
    ''' Obtiene o establece el metodo de pago (cheque - Nota)
    ''' </summary>
    ''' <value>
    ''' The payment method.
    ''' </value>
    Property PaymentMethod As Byte?

    ''' <summary>
    ''' Gets or sets the payment method datasource.
    ''' </summary>
    ''' <value>
    ''' The payment method datasource.
    ''' </value>
    Property PaymentMethodDatasource As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Establece el datasource de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The schedule payment datasource.
    ''' </value>
    Property SchedulePaymentDatasource As List(Of SP_SchedulePayment_Result)

    ''' <summary>
    ''' Gets or sets the original datasource.
    ''' </summary>
    ''' <value>
    ''' The original datasource.
    ''' </value>
    Property OriginalDatasource As List(Of SP_SchedulePayment_Result)

    ''' <summary>
    ''' Obtiene o establece el datasource de las cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Property EntityBankAccountDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource del centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Property CostCenterDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la tasa por mil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [tax by mil]; otherwise, <c>false</c>.
    ''' </value>
    Property TaxByMil As Boolean?

    ''' <summary>
    ''' Obtiene o establece el estado de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Property Status As Byte

    ''' <summary>
    ''' Sets a value indicating whether [actions on controls].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
