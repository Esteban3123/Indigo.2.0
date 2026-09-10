#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface IEquipmentTypeService



    <OperationContract()> _
    Function ListAllEquipmentType(Empresa As String) As List(Of EquipmentType)


    <OperationContract()> _
    Function DeleteEquipmentType(Empresa As String, ByVal EquipmentType As EquipmentType, ByVal audit As AuditMessage) As Boolean


    <OperationContract()> _
    Function SaveEquipmentType(Empresa As String, ByVal EquipmentType As List(Of EquipmentType), ByVal audit As AuditMessage) As Boolean

    <OperationContract()> _
    Function GetEquipmentType(Empresa As String, ByVal codeEquipmentType As String) As EquipmentType



    ''' <summary>
    ''' listar los tipos de equipos dependiendo del tipo de inventario seleccionado
    ''' </summary>
    ''' <param name="IdInventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <OperationContract()> _
    Function ListEquipmentTypeInventoryType(Empresa As String, IdInventoryType As Integer) As List(Of EquipmentType)

    <OperationContract()> _
    Function GetEquipmentTypeById(Empresa As String, ByVal IdEquipmentType As Integer) As EquipmentType

End Interface
