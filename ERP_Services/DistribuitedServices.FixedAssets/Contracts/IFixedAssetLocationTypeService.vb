#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()> _
Public Interface IFixedAssetLocationTypeService

    <OperationContract()> _
    Function ListAllLocationType(Empresa As String) As List(Of FixedAssetLocationType)
End Interface
