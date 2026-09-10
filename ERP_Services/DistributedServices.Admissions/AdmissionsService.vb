#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Admissions
Imports System.ServiceModel.Activation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DistribuitedServices.Authorization
Imports DistributedServices.Authentication

#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class AdmissionsService
    Implements IAdmissionsService

End Class
