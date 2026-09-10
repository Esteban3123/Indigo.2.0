'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/04/2016
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

Public Interface IFixedAssetInitialBalanceItemParts
    Inherits IcrudBase

    ''' <summary>
    ''' Id de la parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PartAccesoriesConsumableId As Integer?

    ''' <summary>
    ''' Datasource de la parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PartAccesoriesConsumableXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Permite saber si deprecia la parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciatePart As Boolean?

    ''' <summary>
    ''' Valor historico de la parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HistoricalValuePart As Decimal

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
    ''' Valor depreciado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciatedValue As Decimal

End Interface
