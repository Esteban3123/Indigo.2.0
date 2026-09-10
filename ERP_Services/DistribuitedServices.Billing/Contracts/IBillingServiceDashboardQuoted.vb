'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IBillingServiceDashboardQuoted

    <OperationContract()>
    Function SP_SaveDashboardQuoted(listDashboardQuoted As List(Of DashboardQuoted), audit As AuditMessage) As ActionResult(Of SP_SaveDashboardQuoted_Result)

End Interface
