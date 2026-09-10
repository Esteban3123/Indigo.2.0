#Region "Imports"
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface IFixedAssetItemTypeService



    <OperationContract()> _
    Function ListAllEquipmentType(Empresa As String) As List(Of FixedAssetItemType)


    <OperationContract()> _
    Function DeleteEquipmentType(Empresa As String, ByVal EquipmentType As FixedAssetItemType, ByVal audit As AuditMessage) As Boolean


    <OperationContract()> _
    Function SaveEquipmentType(Empresa As String, ByVal EquipmentType As List(Of FixedAssetItemType), ByVal audit As AuditMessage) As Boolean

    <OperationContract()> _
    Function GetEquipmentType(Empresa As String, ByVal codeEquipmentType As String) As FixedAssetItemType



    ''' <summary>
    ''' listar los tipos de equipos dependiendo del tipo de inventario seleccionado
    ''' </summary>
    ''' <param name="IdInventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <OperationContract()> _
    Function ListEquipmentTypeInventoryType(Empresa As String, IdInventoryType As Integer) As List(Of FixedAssetItemType)

    <OperationContract()>
    Function GetEquipmentTypeById(Empresa As String, ByVal IdEquipmentType As Integer) As FixedAssetItemType

    <OperationContract()>
    Function SaveFixedAssetItemType(EquipmentType As FixedAssetItemType, audit As AuditMessage) As ActionResult


End Interface
