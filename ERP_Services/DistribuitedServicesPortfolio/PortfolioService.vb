#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Portfolio
Imports DistribuitedServices.Portfolio
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel.Activation
Imports DistributedServices.Authentication

#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class PortfolioService
    Implements IPortfolioService

End Class
