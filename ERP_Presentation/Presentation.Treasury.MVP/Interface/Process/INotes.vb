'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface INotes
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo de la nota
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha de la nota
    ''' </summary>
    Property NoteDate As Date

    ''' <summary>
    ''' Obtiene o establece el tipo de la nota
    ''' </summary>
    ''' <value>
    ''' The type of the note.
    ''' </value>
    Property NoteType As Short

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The entity bank account.
    ''' </value>
    Property EntityBankAccountId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la caja
    ''' </summary>
    ''' <value>
    ''' The cash.
    ''' </value>
    Property CashRegisterId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de comprobante de egreso
    ''' </summary>
    ''' <value>
    ''' The voucher transaction identifier.
    ''' </value>
    Property VoucherTransactionId As Integer?

    Property CashReceiptId As Integer?

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    Property MainAccountId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Obtiene o establece el detalle
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece la fecha de la nota
    ''' </summary>
    ''' <value>
    ''' The date time.
    ''' </value>
    Property DateNote As Date?

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la nota
    ''' </summary>
    ''' <value>
    ''' The nature.
    ''' </value>
    Property Nature As Short?

    ''' <summary>
    ''' Gets or sets the value.
    ''' </summary>
    ''' <value>
    ''' The value.
    ''' </value>
    Property Value As Decimal?

    ''' <summary>
    ''' Obtiene o establece el estado de la nota
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state notes]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As String

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Property EntityAccountDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Property CostCenterDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The voucher transaction datasource.
    ''' </value>
    Property VoucherTransactionDatasource As XPInstantFeedbackSource

    Property CashReceiptDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource para el control múltiple de recibos de caja (tipo nota 8)
    ''' </summary>
    Property CashReceiptsDatasource As XPBaseCollection

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    Property ConsignmentId As Integer?

    Property ConsignmentDatasource As XPInstantFeedbackSource

    Property CrossAccountDatasource As XPInstantFeedbackSource

    Property CrossAccountId As Integer?

#End Region

End Interface
