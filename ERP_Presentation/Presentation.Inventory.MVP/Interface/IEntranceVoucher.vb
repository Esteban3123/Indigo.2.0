'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 13/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface IEntranceVoucher
    Inherits IcrudBase

#Region "Propiedades de la Entidad y Formulario"
    Property Code As String

    Property DocumentDate As DateTime?

    Property SupplierId As Integer

    Property SupplierDistributionLineId As Integer?

    Property SupplierTypeId As Integer?

    Property WarehouseId As Integer?

    Property AccountPayableId As Integer?

    Property Descripcion As String

    Property ResourceType As Byte?

    Property RoundService As Integer?

    Property IcaPercentage As Decimal?

    Property InvoiceNumber As String

    Property InvoiceDate As DateTime?

    Property DayPeriod As Integer?

    Property FreightValue As Decimal?

    Property FreightIVAPercentage As Decimal

    Property FreightIVAValue As Decimal

    Property Value As Decimal

    Property ValueDiscount As Decimal

    Property ValueTax As Decimal

    Property WithholdingTax As Decimal

    Property WithholdingICA As Decimal

    Property RetentionSource As Decimal

    Property RetentionOther As Decimal

    Property DeductionOther As Decimal

    Property DistrictTax As Decimal

    Property TotalValue As Decimal

    Property InvoiceValue As Decimal

    Property Status As Byte

    Property ListRetention As List(Of Domain.Entities.OtherWithholdingDeduction)

    Property ListDeduction As List(Of Domain.Entities.OtherWithholdingDeduction)

    Property NetoValue As Decimal

    ''' <summary>
    ''' Registro IVA
    ''' </summary>
    ''' <returns></returns>
    Property TaxRegistration As Byte?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

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
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' obtiene o establece el Id de la moneda del documento
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer
#End Region

#Region "DataSource"

    ''' <summary>
    ''' Maneja las actividades económicas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EconomicActivityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDatasource As XPInstantFeedbackSource


    ''' <summary>
    ''' Datasource del compromiso
    ''' </summary>
    ''' <returns></returns>
    Property CommitmentDetailXpo As XPCollection

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPCollection

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection

    ''' <summary>
    ''' Propiedad que contiene el listado de autorizaciones de documento soporte xpo
    ''' </summary>
    Property DocumentSupportXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListSupplier As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListWareHouse As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de lineas de distribucion de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeXpo As List(Of Domain.Entities.SupplierType)
#End Region

End Interface
