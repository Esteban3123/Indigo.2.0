'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IAuthorizationServiceDashboardContractCoverage

    <OperationContract()>
    Function SP_SaveContractCoverage(listTuple As List(Of Tuple(Of Integer, Integer, String)), audit As AuditMessage) As ActionResult(Of SP_SaveContractCoverage_Result)

End Interface
