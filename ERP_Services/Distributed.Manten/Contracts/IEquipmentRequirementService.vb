
#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface IEquipmentRequirementService



    <OperationContract()> _
    Function ListAllEquipmentRequirement(Empresa As String) As List(Of EquipmentRequirement)

End Interface
