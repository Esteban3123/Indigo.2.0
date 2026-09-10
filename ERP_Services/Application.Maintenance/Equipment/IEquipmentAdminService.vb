
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad equipo
''' </summary>
''' <remarks></remarks>
Public Interface IEquipmentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEquipment() As List(Of Equipment)

    ''' <summary>
    ''' funcion que sirve para eliminar jun tipo de equipo
    ''' </summary>
    ''' <param name="Equipment"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteEquipment(ByVal Equipment As Equipment, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funcion que sirve para guardar un tipo de equipo
    ''' </summary>
    ''' <param name="Equipment"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipment(ByVal Equipment As Equipment, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Equipment)

    ''' <summary>
    ''' funciona que sirve para listar un tipo de equipo
    ''' </summary>
    ''' <param name="codeEquipment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEquipment(ByVal codeEquipment As String) As Equipment

    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Equipment)
End Interface
