'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 22-05-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
#End Region


Public Interface IFixedAssetPurchaseOrderImport
    
    Property ListPurchaseRequestDetail As List(Of Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result)

End Interface