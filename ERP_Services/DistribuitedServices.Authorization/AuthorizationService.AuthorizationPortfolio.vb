'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Application.Authorization

Partial Class AuthorizationService
    Implements IAuthorizationServiceAuthorizationGroup

    Public Function SaveAuthorizationPortfolio(AuthorizationPortfolio As AuthorizationPortfolio, idSequense As Long, audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio) Implements IAuthorizationServiceAuthorizationPortfolio.SaveAuthorizationPortfolio
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.SaveAuthorizationPortfolio(AuthorizationPortfolio, audit, idSequense)
        End Using
    End Function

    Public Function DeleteAuthorizationPortfolio(AuthorizationPortfolio As AuthorizationPortfolio, audit As AuditMessage) As ActionResult Implements IAuthorizationServiceAuthorizationPortfolio.DeleteAuthorizationPortfolio
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.DeleteAuthorizationPortfolio(AuthorizationPortfolio, audit)
        End Using
    End Function

    Public Function GetAuthorizationPortfolio(code As String, audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio) Implements IAuthorizationServiceAuthorizationPortfolio.GetAuthorizationPortfolio
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.GetAuthorizationPortfolio(code, audit)
        End Using
    End Function

    Public Function GetAuthorizationPortfolioById(id As Integer, audit As AuditMessage) As AuthorizationPortfolio Implements IAuthorizationServiceAuthorizationPortfolio.GetAuthorizationPortfolioById
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.GetAuthorizationPortfolioById(id)
        End Using
    End Function

    Public Function ChangeStateAuthorizationPortfolio(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio) Implements IAuthorizationServiceAuthorizationPortfolio.ChangeStateAuthorizationPortfolio
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.ChangeStateAuthorizationPortfolio(code, state, audit)
        End Using
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioCareCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))) Implements IAuthorizationServiceAuthorizationPortfolio.SP_CopyAndPasteAuthorizationPortfolioCareCenter
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.SP_CopyAndPasteAuthorizationPortfolioCareCenter(data)
        End Using
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))) Implements IAuthorizationServiceAuthorizationPortfolio.SP_CopyAndPasteAuthorizationPortfolioCUPSEntity
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(data)
        End Using
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))) Implements IAuthorizationServiceAuthorizationPortfolio.SP_CopyAndPasteAuthorizationPortfolioInventoryProduct
        Using service As IAuthorizationPortfolioAdminService = Container.Current.Resolve(Of IAuthorizationPortfolioAdminService)()
            Return service.SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(data)
        End Using
    End Function

End Class
