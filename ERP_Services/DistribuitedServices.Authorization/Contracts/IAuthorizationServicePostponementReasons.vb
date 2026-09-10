Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IAuthorizationServicePostponementReasons

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPostponementReasonsById(id As Integer, audit As AuditMessage) As Domain.Entities.PostponementReasons

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPostponementReasons(code As String, audit As AuditMessage) As Domain.Entities.PostponementReasons

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePostponementReasons(PostponementReasons As Domain.Entities.PostponementReasons, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PostponementReasons)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStatePostponementReasons(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PostponementReasons)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePostponementReasons(PostponementReasons As Domain.Entities.PostponementReasons, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface
