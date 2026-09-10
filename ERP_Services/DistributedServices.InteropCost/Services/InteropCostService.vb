#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.InteropCost
Imports System.ServiceModel.Activation
Imports DistributedServices.Authentication

#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class InteropCostService
    Implements IInteropCostService

End Class