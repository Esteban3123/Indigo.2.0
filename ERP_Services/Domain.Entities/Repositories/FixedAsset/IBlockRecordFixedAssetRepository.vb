'************************************************************
' Assembly         : Domain.FisedAssets.IBlockRecordFixedAssets
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 08-04-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region
Public Interface IBlockRecordFixedAssetRepository
    Inherits IRepository(Of BlockRecordFixedAsset)

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBlockRecordFixedAssetByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordFixedAsset
End Interface
