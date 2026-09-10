#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetPhysicalAssetPartRepository
    Inherits IRepository(Of FixedAssetPhysicalAssetParts)

    ''' <summary>
    ''' Obtiene un activo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPhysicalAssetPartById(Id As Integer) As FixedAssetPhysicalAssetParts

End Interface
