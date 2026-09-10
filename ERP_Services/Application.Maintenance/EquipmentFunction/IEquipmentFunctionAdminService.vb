
#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad funcion del equipo
''' </summary>
''' <remarks></remarks>
Public Interface IEquipmentFunctionAdminService
    Inherits IDisposable

    Function GetEquipmentFunctionByCode(code As String, audit As AuditMessage) As ActionResult(Of EquipmentFunction)

    Function SaveEquipmentFunction(EquipmentFunction As EquipmentFunction, audit As AuditMessage, Optional idSequense As Int64 = Nothing) As ActionResult(Of EquipmentFunction)

    ''' <summary>
    ''' funcion que sirve para listar todas las funciones del equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEquipmentFunction() As List(Of EquipmentFunction)

    Function ChangeStateEquipmentFunction(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of EquipmentFunction)

    Function DeleteEquipmentFunction(id As Integer, audit As AuditMessage) As ActionResult(Of EquipmentFunction)

End Interface
