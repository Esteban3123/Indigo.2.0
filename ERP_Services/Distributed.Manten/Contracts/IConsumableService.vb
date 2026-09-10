#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IConsumableService



    <OperationContract()> _
    Function ListAllConsumable(Empresa As String) As List(Of Consumable)


    <OperationContract()> _
    Function DeleteConsumable(Empresa As String, ByVal Consumable As Consumable, ByVal audit As AuditMessage) As Boolean


    <OperationContract()>
    Function SaveConsumable(Consumable As Consumable, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Consumable)

    <OperationContract()> _
    Function GetConsumable(Empresa As String, ByVal codeConsumable As String) As Consumable

    <OperationContract()>
    Function Change_StateConsumable(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Consumable)
End Interface
