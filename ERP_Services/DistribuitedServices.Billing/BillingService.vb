#Region "Imports"
Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports DistributedServices.Authentication
#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class BillingService
    Implements IBillingService

End Class
