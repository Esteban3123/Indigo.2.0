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

Public Interface IPopUpOtherPurchaseRequest
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Nombre As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property UnitId As Integer

    ''' <summary>
    ''' Datasource de unidades de medida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListMeasureUnit As XPInstantFeedbackSource
       

End Interface
