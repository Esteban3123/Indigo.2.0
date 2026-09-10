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
Imports DevExpress.Xpo

#End Region

Public Interface IExpenseConcepts
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
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
    ''' Obtiene o establece el codigo del concepto de egreso
    ''' </summary>
    ''' <value>
    ''' The code expense concepts.
    ''' </value>
    Property CodeExpenseConcepts As String

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la cuenta
    ''' </summary>
    ''' <value>
    ''' The character.
    ''' </value>
    Property Nature As Integer

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Property AccountAccounting As Integer

    ''' <summary>
    ''' Obtiene o establece si afecta el presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affect budget]; otherwise, <c>false</c>.
    ''' </value>
    Property AffectBudget As Byte?

    ''' <summary>
    ''' Gets or sets the cash register.
    ''' </summary>
    ''' <value>
    ''' The cash register.
    ''' </value>
    Property CashRegister As Integer

    ''' <summary>
    ''' Propiedad maneja impuesto
    ''' </summary>
    ''' <returns></returns>
    Property TaxManagement As Boolean

    ''' <summary>
    ''' Obtiene o establece el comportamiento de los conceptos de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The behavior.
    ''' </value>
    Property Behavior As Short

    ''' <summary>
    ''' Gets or sets a value indicating whether [state expense concepts].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [state expense concepts]; otherwise, <c>false</c>.
    ''' </value>
    Property StateExpenseConcepts As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource para cajas
    ''' </summary>
    ''' <value>
    ''' The cash register datasource.
    ''' </value>
    Property CashRegisterDatasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Gets the repository code cash.
    ''' </summary>
    ''' <value>
    ''' The repository code cash.
    ''' </value>
    WriteOnly Property RepositoryCodeCashDatasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Gets the repository name cash.
    ''' </summary>
    ''' <value>
    ''' The repository name cash.
    ''' </value>
    WriteOnly Property RepositoryNameCashDatasource As XPInstantFeedbackSource

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
    Property AffectCashFlowConcept As Byte?

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
    ''' Propiedad de "Maneja Orden de Compra"
    ''' </summary>
    ''' <returns></returns>
    Property BuyOrderManagement As Boolean

#End Region

End Interface
