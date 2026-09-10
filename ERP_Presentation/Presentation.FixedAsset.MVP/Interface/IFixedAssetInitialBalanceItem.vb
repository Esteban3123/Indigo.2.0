'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface IFixedAssetInitialBalanceItem
    Inherits IcrudBase

    ''' <summary>
    ''' Tipo de adquisición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdquisitionType As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemId As Integer?

    ''' <summary>
    ''' Establece el datasource del articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la serie
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Serie As String

    ''' <summary>
    ''' Obtiene o establece la placa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Plate As String

    ''' <summary>
    ''' Obtiene o establece el id de la localización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationId As Integer?

    ''' <summary>
    ''' Establece el datasource de la localización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el id del responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleId As Integer?

    ''' <summary>
    ''' Establece el datasource del responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierId As Integer?

    ''' <summary>
    ''' Establece el datasource del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el valor historico 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HistoricalValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor razonable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FairValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el id de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TrademarkId As Integer?

    ''' <summary>
    ''' Establece el datasource de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TrademarkXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el modelo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ModelItem As String

    ''' <summary>
    ''' Obtiene o establece el id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PolicyId As Integer?

    ''' <summary>
    ''' Establece el datasource
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PolicyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece si el item maneja garantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesWarranty As Boolean

    ''' <summary>
    ''' Fecha de vencimiento de la garantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarrantyExpirationDate As DateTime?

    ''' <summary>
    ''' Fecha de adquisicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdquisitionDate As DateTime?

    ''' <summary>
    ''' Si deprecia o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Depreciate As Boolean

    ''' <summary>
    ''' valida menor cuantía o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidMinorAmount As Boolean?

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookId As Integer?

    ''' <summary>
    ''' Establece el datasource del libro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LifeTime As Integer

    ''' <summary>
    ''' Unidad de la vida util
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitLifeTimeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de depreciación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciationTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el total de unidades de produccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalProductionUnit As Int64

    ''' <summary>
    ''' Obtiene o establece el porcentaje de salvamento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageRescue As Decimal

    ''' <summary>
    ''' Días depreciados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciatedDays As Integer

    ''' <summary>
    ''' Valor historico en el libro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HistoricalValueInBook As Decimal

    ''' <summary>
    ''' Valor depreciado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciatedValue As Decimal

    ''' <summary>
    ''' Estado del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusAssetId As Integer?

    ''' <summary>
    ''' Datasource del estado del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusAssetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Si amortiza o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Amortize As Boolean

End Interface
