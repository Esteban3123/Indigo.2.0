#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()>
Public Interface IEquipmentService



    <OperationContract()>
    Function ListAllEquipment(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Equipment)


    <OperationContract()>
    Function DeleteEquipment(ByVal Equipment As Equipment, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean


    <OperationContract()>
    Function SaveEquipment(ByVal Equipment As Equipment, session As SessionValues) As ActionResult(Of Domain.Entities.Equipment)

    <OperationContract()>
    Function GetEquipment(ByVal codeEquipment As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Equipment

    <OperationContract()>
    Function Change_StateEquipment(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.Equipment)
End Interface
