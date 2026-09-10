#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IFixedAssetPhysicalAssetPartService

    ''' <summary>
    ''' Obtiene el registro por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetPhysicalAssetPartById(Id As Integer) As Domain.Entities.FixedAssetPhysicalAssetParts

End Interface
