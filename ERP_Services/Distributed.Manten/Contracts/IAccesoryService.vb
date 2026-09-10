#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IAccesoryService

    <OperationContract()> _
    Function ListAllAccessory(Empresa As String) As List(Of Accessory)

    <OperationContract()> _
    Function DeleteAccessory(Empresa As String, ByVal Accessory As Accessory, ByVal audit As AuditMessage) As Boolean

    <OperationContract()>
    Function SaveAccessory(Accessory As Domain.Entities.Accessory, session As SessionValues) As ActionResult(Of Domain.Entities.Accessory)

    <OperationContract()> _
    Function GetAccessory(Empresa As String, ByVal codeAccessory As String) As Accessory

    <OperationContract()>
    Function Change_StateAccessory(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.Accessory)

End Interface
