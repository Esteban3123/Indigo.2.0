#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface IPhysicalRiskService


    <OperationContract()> _
    Function ListAllPhysicalRisk(Empresa As String) As List(Of PhysicalRisk)
End Interface
