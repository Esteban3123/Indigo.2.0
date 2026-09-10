
#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface IEquipmentHistoryService


    <OperationContract()> _
    Function ListAllEquipmentHistory(Empresa As String) As List(Of EquipmentHistory)

End Interface
