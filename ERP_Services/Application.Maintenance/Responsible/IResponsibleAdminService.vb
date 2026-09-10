
#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad responsable
''' </summary>
''' <remarks></remarks>
Public Interface IResponsibleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas los responsables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllResponsible() As List(Of Responsible)

    ''' <summary>
    ''' funcion que sirve para eliminar un responsable
    ''' </summary>
    ''' <param name="Responsible"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteResponsible(ByVal Responsible As Responsible, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que sirve para guardar un responsable
    ''' </summary>
    ''' <param name="Responsible"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveResponsible(ByVal Responsible As Responsible, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Responsible)

    ''' <summary>
    ''' funciona que sirve para listar un responsable
    ''' </summary>
    ''' <param name="CodeResponsible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetResponsible(ByVal CodeResponsible As String, ByVal audit As AuditMessage) As Responsible
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateResponsible(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Responsible)
End Interface
