'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/04/2016
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

Public Interface IFixedAssetEntryItem
    Inherits IcrudBase

    ''' <summary>
    ''' Id del articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemId As Integer?

    ''' <summary>
    ''' Datasource del articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TrademarkId As Integer?

    ''' <summary>
    ''' Datasource de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TrademarkXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Modelo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ModelItem As String

    ''' <summary>
    ''' Id de la poliza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PolicyId As Integer?

    ''' <summary>
    ''' Datasource de la poliza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PolicyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IvaId As Integer?

    ''' <summary>
    ''' Datasource del iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IvaXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Cantidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Quantity As Integer

    ''' <summary>
    ''' Valor unitario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitValue As Decimal

    ''' <summary>
    ''' Subtotal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SubTotalValue As Decimal

    ''' <summary>
    ''' % de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IvaPercentage As Decimal

    ''' <summary>
    ''' Valor iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IvaValue As Decimal

    ''' <summary>
    ''' % de descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DiscountPercentage As Decimal

    ''' <summary>
    ''' Valor del descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DiscountValue As Decimal

    ''' <summary>
    ''' Valor total
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalValue As Decimal
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
