#Region "Imports"

Imports Domain.Entities

#End Region

Public Interface IFixedAssetPhysicalAssetPartAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPhysicalAssetPartById(ByVal Id As Integer) As FixedAssetPhysicalAssetParts

End Interface
