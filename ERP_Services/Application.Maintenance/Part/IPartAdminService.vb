
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad parte
''' </summary>
''' <remarks></remarks>
Public Interface IPartAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las partes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPart() As List(Of Part)

    ''' <summary>
    ''' funcion que sirve para eliminar un fabricante
    ''' </summary>
    ''' <param name="Part"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeletePart(ByVal Part As Part, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que sirve para guardar una parte
    ''' </summary>
    ''' <param name="Part"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePart(ByVal Part As Part, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Part)

    ''' <summary>
    ''' funciona que sirve para listar una parte
    ''' </summary>
    ''' <param name="codePart"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPart(ByVal codePart As String, audit As AuditMessage) As Part
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStatePart(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Part)
End Interface
