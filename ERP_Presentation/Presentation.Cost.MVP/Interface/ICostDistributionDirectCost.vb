'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 26-02-2016
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
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.CostRepository

#End Region

Public Interface ICostDistributionDirectCost
    Inherits IcrudBase

#Region "Fields"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Property Sequence As CostSecuence

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    WriteOnly Property SettingsCost As CostSetting

#End Region

#Region "Properties"

#Region "Main Data"

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto directo
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el id del gasto directo
    ''' </summary>
    Property GeneralExpenseId As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la linea de distribución relacionada con el proveedor
    ''' </summary>
    Property SuppliersDistributionLinesId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Codigo y Nombre de la linea de distribución
    ''' </summary>
    Property DistributionLineCodeName As String

    ''' <summary>
    ''' Obtiene o establece el Codigo y Nombre del cargo
    ''' </summary>
    Property PositionCodeName As String

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    Property SupplierId As Integer?

    ''' <summary>
    ''' Representa el id del cargo
    ''' </summary>
    ''' <remarks></remarks>
    Property PositionId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    Property ThirdPartyId As Integer

    ''' <summary>
    ''' Id de la cuenta por pagar
    ''' </summary>
    Property AccountPayableId As Integer?

    ''' <summary>
    ''' Valor de la Factura
    ''' </summary>
    Property Value As Decimal

    ''' <summary>
    ''' Indica si el registro va a ser con IVA descontable o no
    ''' </summary>
    Property DeductibleIVA As Boolean

    ''' <summary>
    ''' Observaciones
    ''' </summary>
    Property Observation As String

    ''' <summary>
    ''' propiedad que obtiene o establece la moneda del documento
    ''' </summary>
    ''' <param name="CurrencyAbbreviation"></param>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer
#End Region

#Region "Bill Data"

    ''' <summary>
    ''' Numero Factura
    ''' </summary>
    Property BillNumber As String

    ''' <summary>
    ''' Numero Factura
    ''' </summary>
    Property BillDate As Date?

    ''' <summary>
    ''' Plazo
    ''' </summary>
    Property Term As Integer?

    ''' <summary>
    ''' Horas
    ''' </summary>
    Property Hours As Integer?

    ''' <summary>
    ''' Cuenta Contable
    ''' </summary>
    Property MainAccountId As Integer?

    ''' <summary>
    ''' Centro de Costo
    ''' </summary>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Fecha de Radicación
    ''' </summary>
    Property ServicePeriodDate As Date?

    ''' <summary>
    ''' Unidad de Radiación
    ''' </summary>
    Property FilingUnitId As Integer?

    ''' <summary>
    ''' Tipo de proveedor
    ''' </summary>
    Property SupplierTypeId As Integer?

    ''' <summary>
    ''' CXP mismo Proveedor
    ''' </summary>
    ''' <returns></returns>
    Property AccountPayableSameSupplier As Boolean?

#End Region

#Region "Other Information"

    ''' <summary>
    ''' Año de la distribución del gasto Directo
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes de la distribución del gasto Directo
    ''' </summary>
    Property Month As Integer

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Property Status As String

#End Region

#End Region

#Region "XPO"

#Region "Individual XPO"

    Property GeneralExpense As Infrastructure.Data.Xpo.CostRepository.CostGeneralExpenseXpo

#End Region

    ''' <summary>
    ''' Obtiene los gastos generales
    ''' </summary>
    Property GeneralExpenseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de tercero
    ''' </summary>
    ''' <value></value>
    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de factura
    ''' </summary>
    ''' <value></value>
    Property AccountPayableXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de centros de costo
    ''' </summary>
    ''' <value></value>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de unidades de radicación
    ''' </summary>
    ''' <value></value>
    Property FilingUnitXpo As List(Of FilingUnit)

    ''' <summary>
    ''' Datasource de detalles del elemento de costo
    ''' </summary>
    Property CostDistributionBaseDetailsXpo As List(Of CostDistributionBaseDetailXpo)

    ''' <summary>
    ''' Datasource unidad de medida
    ''' </summary>
    Property MeasurementUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de unidades de medida para el repositorio
    ''' </summary>
    Property MeasureUnitDataSourceRerpository As XPInstantFeedbackSource
    ''' <summary>
    ''' Lista de tarifas iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateIvaXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' datasource de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los productos
    ''' </summary>
    ''' <returns></returns>
    Property InventoryProductRepositoryXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los terceros
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyRepositoryXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los cargos
    ''' </summary>
    ''' <returns></returns>
    Property PositionRepositoryXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los conceptos de retención
    ''' </summary>
    ''' <returns></returns>
    Property RetentionConceptXpo As XPInstantFeedbackSource

#End Region

End Interface