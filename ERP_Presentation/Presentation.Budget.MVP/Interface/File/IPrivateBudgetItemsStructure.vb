'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IPrivateBudgetItemsStructure
    Inherits IcrudBase

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad contiene el codigo de los bancos
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre de los bancos
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el id del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ParentId As Integer?

    ''' <summary>
    ''' Establece el datasource del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ParentXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <returns></returns>
    Property Type As Integer

    ''' <summary>
    ''' Valores no presupuestados
    ''' </summary>
    ''' <returns></returns>
    Property UnBudgetValues As Integer?

    ''' <summary>
    ''' Control de orden de compra
    ''' </summary>
    ''' <returns></returns>
    Property PurchaseOrderControl As Boolean?

    ''' <summary>
    ''' Email
    ''' </summary>
    ''' <returns></returns>
    Property EmailNotification As String

    ''' <summary>
    ''' Periodicidad
    ''' </summary>
    ''' <returns></returns>
    Property Periodicity As Integer?

    ''' <summary>
    ''' % ejecución para alerta
    ''' </summary>
    ''' <returns></returns>
    Property ExecutionAlertPercentage As Decimal?

    ''' <summary>
    ''' Permite exceder presupuesto
    ''' </summary>
    ''' <returns></returns>
    Property AllowExceedBudget As Boolean?

    ''' <summary>
    ''' % exceder presupuesto
    ''' </summary>
    ''' <returns></returns>
    Property ExceedBudgetPercentage As Decimal?

    ''' <summary>
    ''' Control presupuestal
    ''' </summary>
    ''' <returns></returns>
    Property BudgetControl As Integer?

    ''' <summary>
    ''' Cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Property MainAccountId As Integer?

    ''' <summary>
    ''' Datasource cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Property MainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <returns></returns>
    Property ThirdpartyId As Integer?

    ''' <summary>
    ''' datasource tercero
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de centro de costo
    ''' </summary>
    ''' <returns></returns>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' datasource centro costo
    ''' </summary>
    ''' <returns></returns>
    Property CostCenterXpo As XPInstantFeedbackSource

End Interface
