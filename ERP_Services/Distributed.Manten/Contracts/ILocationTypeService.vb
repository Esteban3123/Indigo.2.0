#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface ILocationTypeService




    <OperationContract()> _
    Function ListAllLocationType(Empresa As String) As List(Of LocationType)
End Interface
