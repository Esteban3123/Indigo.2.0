'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2016
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

Public Interface IFixedAssetActiveOutputDetail
    Inherits IcrudBase

    ''' <summary>
    ''' Activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetId As Integer?

    ''' <summary>
    ''' Datasource activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Tipo de salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OutputType As Integer?

    ''' <summary>
    ''' Tipo de baja
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LowType As Integer?

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LowTypeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Valor de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValue As Decimal

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Tipo: Activo o Parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ActiveType As Integer?

    ''' <summary>
    ''' Id de la parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetPartId As Integer?

    ''' <summary>
    ''' Datasource partes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetPartXpo As XPInstantFeedbackSource

End Interface
