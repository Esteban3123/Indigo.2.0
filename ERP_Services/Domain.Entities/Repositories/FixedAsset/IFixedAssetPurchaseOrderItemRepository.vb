'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Oscar stiven Astudillo reyes
' Created          : 2024-11-26
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetPurchaseOrderItemRepository
    Inherits IRepository(Of FixedAssetPurchaseOrderItem)

    ''' <summary>
    ''' Obtiene un registro por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetFixedAssetPurchaseOrderItemById(id As Integer) As FixedAssetPurchaseOrderItem


End Interface