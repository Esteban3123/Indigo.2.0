Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IPostponementReasonsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetPostponementReasonsById(ByVal id As Integer) As PostponementReasons

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPostponementReasons(ByVal code As String, ByVal audit As AuditMessage) As PostponementReasons

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePostponementReasons(ByVal PostponementReasons As PostponementReasons, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PostponementReasons)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStatePostponementReasons(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PostponementReasons)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePostponementReasons(ByVal PostponementReasons As PostponementReasons, ByVal audit As AuditMessage) As ActionResult

End Interface
