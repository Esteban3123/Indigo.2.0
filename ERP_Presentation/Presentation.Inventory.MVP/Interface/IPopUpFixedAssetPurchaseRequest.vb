'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 26/04/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

Public Interface IPopUpFixedAssetPurchaseRequest
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ItemId As Integer

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
    Property TrademarkId As Integer

    ''' <summary>
    ''' Datasource de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TrademarkXpo As XPInstantFeedbackSource

End Interface
