#Region "Imports"
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()>
Public Interface IEquipmentFunctionService

#Region "Methods"

    <OperationContract()>
    Function GetEquipmentFunctionByCode(code As String, audit As AuditMessage) As ActionResult(Of EquipmentFunction)

    <OperationContract()>
    Function SaveEquipmentFunction(EquipmentFunction As EquipmentFunction, audit As AuditMessage, Optional idSequense As Int64 = Nothing) As ActionResult(Of EquipmentFunction)

    <OperationContract()>
    Function ListAllEquipmentFunction(Empresa As String) As List(Of EquipmentFunction)

    <OperationContract()>
    Function ChangeStateEquipmentFunction(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of EquipmentFunction)

    <OperationContract()>
    Function DeleteEquipmentFunction(id As Integer, audit As AuditMessage) As ActionResult(Of EquipmentFunction)

#End Region

End Interface
