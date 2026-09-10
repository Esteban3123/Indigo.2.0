'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports DistributedServices.MixingStation
Imports DistributedServices.Authentication
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class MixingStationService
    Implements IMixingStationService

End Class
