'***********************************************************************
' Assembly         : DistributedService.Payroll
' Author           : Cristhian Salazar
' Created          : 07-04-2013
'
' Last Modified By : Daniel Arevalo
' Last Modified On : 07-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel.Activation
Imports DistributedServices.Authentication
Imports DistributedServices.Payroll
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<JwtMessageServiceBehavior>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class PayrollService
    Implements IPayrollService

End Class
