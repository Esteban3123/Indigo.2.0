Imports Application.Authorization
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class AuthorizationService
    Implements IAuthorizationServicePostponementReasons

    Public Function GetPostponementReasonsById(id As Integer, audit As AuditMessage) As Domain.Entities.PostponementReasons Implements IAuthorizationServicePostponementReasons.GetPostponementReasonsById
        Using service As IPostponementReasonsAdminService = Container.Current.Resolve(Of IPostponementReasonsAdminService)()
            Return service.GetPostponementReasonsById(id)
        End Using
    End Function

    Public Function GetPostponementReasons(code As String, audit As AuditMessage) As Domain.Entities.PostponementReasons Implements IAuthorizationServicePostponementReasons.GetPostponementReasons
        Using service As IPostponementReasonsAdminService = Container.Current.Resolve(Of IPostponementReasonsAdminService)()
            Return service.GetPostponementReasons(code, audit)
        End Using
    End Function

    Public Function SavePostponementReasons(PostponementReasons As Domain.Entities.PostponementReasons, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PostponementReasons) Implements IAuthorizationServicePostponementReasons.SavePostponementReasons
        Using service As IPostponementReasonsAdminService = Container.Current.Resolve(Of IPostponementReasonsAdminService)()
            Return service.SavePostponementReasons(PostponementReasons, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStatePostponementReasons(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PostponementReasons) Implements IAuthorizationServicePostponementReasons.ChangeStatePostponementReasons
        Using service As IPostponementReasonsAdminService = Container.Current.Resolve(Of IPostponementReasonsAdminService)()
            Return service.ChangeStatePostponementReasons(code, state, audit)
        End Using
    End Function

    Public Function DeletePostponementReasons(PostponementReasons As Domain.Entities.PostponementReasons, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IAuthorizationServicePostponementReasons.DeletePostponementReasons
        Using service As IPostponementReasonsAdminService = Container.Current.Resolve(Of IPostponementReasonsAdminService)()
            Return service.DeletePostponementReasons(PostponementReasons, audit)
        End Using
    End Function

End Class
