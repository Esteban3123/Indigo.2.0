
#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad tipos de inventario
''' </summary>
''' <remarks></remarks>
Public Interface IInventoryTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas los tipos de inventario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllInventoryType() As List(Of InventoryType)

    ''' <summary>
    ''' funcion que sirve para eliminar un tipo de inventario
    ''' </summary>
    ''' <param name="InventoryType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteInventoryType(ByVal InventoryType As InventoryType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funcion que sirve para guardar un tipo de inventario
    ''' </summary>
    ''' <param name="InventoryType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveInventoryType(ByVal InventoryType As InventoryType, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of InventoryType)

    ''' <summary>
    ''' funciona que sirve para listar un tipo de inventario
    ''' </summary>
    ''' <param name="codeInventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryType(ByVal codeInventoryType As String) As InventoryType

    ''' <summary>
    ''' Cambiar estado del registro
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of InventoryType)
End Interface
