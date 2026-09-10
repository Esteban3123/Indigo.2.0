'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService
    Public Function GetAccountReceivableShareById(idAccountReceivableShare As Object) As Domain.Entities.AccountReceivableShare Implements IPortfolioServiceAccountReceivableShare.GetAccountReceivableShareById
        Using service As IAccountReceivableShareAdminService = Container.Current.Resolve(Of IAccountReceivableShareAdminService)()
            Return service.GetAccountReceivableShareById(idAccountReceivableShare)
        End Using
        'Return _accountReceivableShareAdminService.GetAccountReceivableShareById(idAccountReceivableShare)
    End Function

    Public Function SaveAccountReceivableShare(AccountReceivableShare As Domain.Entities.AccountReceivableShare, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountReceivableShare) Implements IPortfolioServiceAccountReceivableShare.SaveAccountReceivableShare
        Using service As IAccountReceivableShareAdminService = Container.Current.Resolve(Of IAccountReceivableShareAdminService)()
            Return service.SaveAccountReceivableShare(AccountReceivableShare, audit)
        End Using
        'Return Me._accountReceivableShareAdminService.SaveAccountReceivableShare(AccountReceivableShare, audit)
    End Function
End Class
