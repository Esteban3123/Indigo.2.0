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
    Implements IAuthorizationServiceDashboardContractCoverage

    Public Function SP_SaveContractCoverage(listTuple As List(Of Tuple(Of Integer, Integer, String)), audit As AuditMessage) As ActionResult(Of SP_SaveContractCoverage_Result) Implements IAuthorizationServiceDashboardContractCoverage.SP_SaveContractCoverage
        Using service As IDashboardContractCoverageAdminService = Container.Current.Resolve(Of IDashboardContractCoverageAdminService)()
            Return service.SP_SaveContractCoverage(listTuple, audit)
        End Using
    End Function

End Class
