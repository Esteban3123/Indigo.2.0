
#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetItemTypeAdminService
    Inherits IDisposable


    ''' <summary>
    ''' funcion que sirve para lñistar todas las tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEquipmentType() As List(Of FixedAssetItemType)


    ''' <summary>
    ''' funcion que sirve para eliminar jun tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteEquipmentType(ByVal EquipmentType As FixedAssetItemType, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar un tipo de equipo
    ''' </summary>
    ''' <param name="EquipmentType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipmentType(ByVal EquipmentType As List(Of FixedAssetItemType), ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funciona que sirve para listar un tipo de equipo
    ''' </summary>
    ''' <param name="codeEquipmentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEquipmentType(ByVal codeEquipmentType As String) As FixedAssetItemType


    ''' <summary>
    ''' listar los tipos de equipos dependiendo del tipo de inventario seleccionado
    ''' </summary>
    ''' <param name="IdInventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListEquipmentTypeInventoryType(IdInventoryType As Integer) As List(Of FixedAssetItemType)

    ''' <summary>
    ''' funciona que sirve para listar un tipo de equipo
    ''' </summary>
    ''' <param name="IdEquipmentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEquipmentTypeById(ByVal IdEquipmentType As Integer) As FixedAssetItemType

    Function SaveFixedAssetItemType(EquipmentType As FixedAssetItemType, audit As AuditMessage) As ActionResult
End Interface
