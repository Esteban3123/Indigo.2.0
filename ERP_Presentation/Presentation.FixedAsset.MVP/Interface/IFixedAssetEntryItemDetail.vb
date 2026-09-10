'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/04/2016
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
Imports Domain.Entities

#End Region

Public Interface IFixedAssetEntryItemDetail
    Inherits IcrudBase

    ''' <summary>
    ''' Placa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Plate As String

    ''' <summary>
    ''' Serial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Serie As String

    ''' <summary>
    '''Id responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleId As Integer?

    ''' <summary>
    ''' Datasource del responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id localización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationId As Integer?

    ''' <summary>
    ''' Datasource localización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationXpo As XPCollection

    ''' <summary>
    ''' Fecha adquisicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdquisitionDate As DateTime?

    ''' <summary>
    ''' Permite saber si deprecia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Depreciate As Boolean?

    ''' <summary>
    ''' Permite saber si amortiza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Amortize As Boolean

    ''' <summary>
    ''' Permite saber si tiene garantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesWarranty As Boolean?

    ''' <summary>
    ''' Permite saber si valida menor cuantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidSmallerAmount As Boolean?
    ''' <summary>
    ''' Fecha vencimiento garantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarrantyExpirationDate As DateTime?

    ''' <summary>
    ''' Estado del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusAssetId As Integer?

    ''' <summary>
    ''' Datasource estado del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusAssetXpo As XPInstantFeedbackSource

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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


    ''' <summary>
    ''' Parametros de activos fijos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SettingsFixedAsset As SettingFixedAsset


End Interface
