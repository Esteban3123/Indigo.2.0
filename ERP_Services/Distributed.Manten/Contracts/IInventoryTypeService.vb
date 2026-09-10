#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IInventoryTypeService



    <OperationContract()> _
    Function ListAllInventoryType(Empresa As String) As List(Of InventoryType)


    <OperationContract()> _
    Function DeleteInventoryType(Empresa As String, ByVal inventoryType As InventoryType, ByVal audit As AuditMessage) As Boolean


    <OperationContract()> _
    Function SaveInventoryType(InventoryType As Domain.Maintenance.Entities.InventoryType, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of InventoryType)

    <OperationContract()> _
    Function GetInventoryType(Empresa As String, ByVal codeInventoryType As String) As InventoryType

    <OperationContract()> _
    Function Change_StateInventoryType(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Maintenance.Entities.InventoryType)
End Interface
