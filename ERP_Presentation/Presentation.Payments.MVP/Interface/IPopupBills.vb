'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

Public Interface IPopupBills
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece el proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccount As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' Esta propiedad contiene el centro de costo
    ''' </summary>
    Property IdCostCenter As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount As Integer

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillNumber As String

    ''' <summary>
    ''' Obtiene o establece el plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Term As Integer

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Comment As String

    ''' <summary>
    ''' Obtiene o establece las cuotas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Shares As Integer

    ''' <summary>
    ''' Maneja las actividades económicas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EconomicActivityId As Integer?
    Property EconomicActivityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el valor de la factura cuando no tiene permiso de causacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueBill As Decimal

    ''' <summary>
    ''' Obtiene o establece las horas laboradas por el empleado independiente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Hours As Integer

    ''' <summary>
    ''' Maneja iva descontable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeductibleIva As Boolean?

    ''' <summary>
    ''' Id de la moneda de la cxp
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional Abbreviation As String = Nothing) As Integer?

#Region "Budget Interface"

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPCollection

    ''' <summary>
    ''' Datasource de de las resoluciones de documento soporte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentSupportXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection

    ''' <summary>
    ''' Compromisos 
    ''' </summary>
    ''' <returns></returns>
    Property ListAccountPayableCommitment As List(Of AccountPayableCommitments)

#End Region

End Interface
