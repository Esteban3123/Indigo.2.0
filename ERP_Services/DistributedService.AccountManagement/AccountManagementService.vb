#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.AccountManagement
Imports System.ServiceModel.Activation
Imports DistributedServices.AccountManagement
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DistributedServices.Authentication

#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class AccountManagementService
    Implements IAccountManagementService ', IDisposable

End Class