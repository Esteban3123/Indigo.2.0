'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface ISettingPortfolio
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' id de la nota credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeCreditNotesId As Integer?
    ''' <summary>
    ''' id de la nota debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeDebitNotesId As Integer?
    ''' <summary>
    ''' id del traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeTranslationId As Integer?
    ''' <summary>
    ''' id d ela provision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeProvisionId As Integer?
    ''' <summary>
    ''' id de radicacion de cuentas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeFilingAccountId As Integer?
    ''' <summary>
    ''' obtiene o establece el id del tipo de comprobante para el documento de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeDocumentAccountReceivableId As Integer

    ''' <summary>
    ''' Deterioro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeDeteriorationAccountId As Integer?

    ''' <summary>
    ''' obtiene o establece el estado
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Property State As Boolean

    ''' <summary>
    ''' establece el eestado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' obtiene o establece los tipos de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeCreditNotesXPO As DevExpress.Xpo.XPInstantFeedbackSource
    Property JournalVoucherTypeDebitNotesXPO As DevExpress.Xpo.XPInstantFeedbackSource
    Property JournalVoucherTypeTranslationXPO As DevExpress.Xpo.XPInstantFeedbackSource
    Property JournalVoucherTypeProvisionXPO As DevExpress.Xpo.XPInstantFeedbackSource
    Property JournalVoucherTypeDeteriorationAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource
    Property JournalVoucherTypeFilingAccountXPO As DevExpress.Xpo.XPInstantFeedbackSource
    Property JournalVoucherTypeDocumentAccountReceivableXpo As DevExpress.Xpo.XPInstantFeedbackSource
    Property NameMinimumAgeRange As String

    Property NameMaximumAgeRange As String

    ''' <summary>
    ''' Porcentaje de deterioro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeteriorationPercentage As Decimal

    ''' <summary>
    ''' Porcentaje de provisión
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProvisionPercentage As Decimal

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property UnReconciledInvoice As Byte?

    ''' <summary>
    ''' Propiedad que indica si aplican las reglas de deterioro por clasificación
    ''' </summary>
    ''' <returns></returns>
    Property DeteriorationByClasification As Boolean

    ''' <summary>
    ''' Id de la cuenta contable para los movimientos débitos de deterioro 
    ''' </summary>
    ''' <returns></returns>
    Property DebitDeteriorationAccount As Integer

    ''' <summary>
    ''' Id de la cuenta contable para los movimiento créditos de deterioro
    ''' </summary>
    ''' <returns></returns>
    Property CreditDeteriorationAccount As Integer

    ''' <summary>
    ''' Id de la cuenta contable para la reversión del deterioro de cartera
    ''' </summary>
    ''' <returns></returns>
    Property ReversalDeteriorationAccount As Integer

    ''' <summary>
    ''' Id de la cuenta contable para el periodo previo a la reversión del deterioro de cartera
    ''' </summary>
    ''' <returns></returns>
    Property PreviousPeriodReversalAccount As Integer

#Region "Budget Interface"

    ''' <summary>
    ''' Id de la entidad de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityId As Integer?

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer?

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la dependencia presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DependencyId As Integer?

    ''' <summary>
    ''' Datasource de las dependencias presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DependencyXpo As XPInstantFeedbackSource

#End Region

End Interface
