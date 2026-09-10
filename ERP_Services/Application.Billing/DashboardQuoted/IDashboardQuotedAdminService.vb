'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IDashboardQuotedAdminService
    Inherits IDisposable

    Function SP_SaveDashboardQuoted(listDashboardQuoted As List(Of DashboardQuoted), audit As AuditMessage) As ActionResult(Of SP_SaveDashboardQuoted_Result)

End Interface
