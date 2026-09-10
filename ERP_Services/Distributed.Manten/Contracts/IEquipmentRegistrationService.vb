
#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()>
Public Interface IEquipmentRegistrationService




    <OperationContract()>
    Function ListAllEquipmentRegistration(Empresa As String) As List(Of EquipmentRegistration)


    <OperationContract()>
    Function DeleteEquipmentRegistration(Empresa As String, ByVal EquipmentRegistration As EquipmentRegistration, ByVal audit As AuditMessage) As Boolean


    <OperationContract()>
    Function SaveEquipmentRegistration(Empresa As String, EquipmentRegistration As Domain.Entities.EquipmentRegistration, idSequense As Long, audit As AuditMessage) As ActionResult(Of Domain.Entities.EquipmentRegistration)

    <OperationContract()> _
    Function GetEquipmentRegistration(Empresa As String, ByVal codeEquipmentRegistration As String) As EquipmentRegistration

    ''' <summary>
    ''' funcion paranlistar los accesorios de un equipo 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <OperationContract()> _
    Function ListAccesoryEquipmentType(Empresa As String, Type As Integer) As List(Of AccesoryDetail)


    ''' <summary>
    ''' funcion paranlistar los consumibles de un equipo 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <OperationContract()> _
    Function ListConsumableEquipmentType(Empresa As String, Type As Integer) As List(Of ConsumableDetail)

    <OperationContract()>
    Function ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(Empresa As String, fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As List(Of TechnicalLogDetail)

End Interface
