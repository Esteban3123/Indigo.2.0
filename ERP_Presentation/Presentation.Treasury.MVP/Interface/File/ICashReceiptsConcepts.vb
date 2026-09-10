'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities

#End Region

Public Interface ICashReceiptsConcepts
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la cabecera de la secuencia
    ''' </summary>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo de conceptos de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The code cash receipts concepts.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del concepto
    ''' </summary>
    ''' <value>
    ''' The name cash receipts concepts.
    ''' </value>
    Property NameCashReceiptsConcepts As String

    ''' <summary>
    ''' obtiene o establece la afectacion
    ''' </summary>
    ''' <value>
    ''' The affectation.
    ''' </value>
    Property Affectation As Integer
    ''' <summary>
    ''' Obtiene o establece si afecta el presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affects budget]; otherwise, <c>false</c>.
    ''' </value>
    Property AffectsBudget As Boolean

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la cuenta
    ''' </summary>
    ''' <value>
    ''' The character.
    ''' </value>
    Property Character As Integer

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Property AccountAccounting As Integer

    ''' <summary>
    ''' Gets or sets a value indicating whether [state cash receipts concepts].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [state cash receipts concepts]; otherwise, <c>false</c>.
    ''' </value>
    Property StateCashReceiptsConcepts As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource para cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the rubro datasource.
    ''' </summary>
    ''' <value>
    ''' The rubro datasource.
    ''' </value>
    Property RubroDatasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property AffectCashFlowConcept As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property IdAffectCashFlowConcept As Integer?

    ''' <summary>
    ''' Datasource del concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Property CashFlowConceptDataSource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property MakeAssociateIncome As Boolean?

    ''' <summary>
    ''' Establece si vincula tercero beneficiario
    ''' </summary>
    ''' <returns></returns>
    Property LinkThirdPartyBeneficiary As Boolean

    ''' <summary>
    ''' Establece si el concepto es para anticipo factura monto fijo
    ''' </summary>
    ''' <returns></returns>
    Property IsFixedAmountInvoiceAdvance As Boolean

#End Region

End Interface
