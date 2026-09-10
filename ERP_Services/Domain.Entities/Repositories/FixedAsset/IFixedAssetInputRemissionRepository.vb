'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05/02/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetFixedAssetRemissionEntranceRepository

    Inherits IRepository(Of FixedAssetRemissionEntrance)

    ''' <summary>
    ''' Obtiene una Ubicación por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetFixedAssetRemissionEntranceByCode(code As String, Optional tracking As Boolean = True) As FixedAssetRemissionEntrance

    Function GetFixedAssetFixedAssetRemissionEntranceById(Id As Integer, Optional tracking As Boolean = True) As FixedAssetRemissionEntrance

    Function GetSupplierBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As Integer

    Function GetMainAccountWithItemId(ItemId As Integer, AdquisitionType As Integer) As Integer

End Interface
