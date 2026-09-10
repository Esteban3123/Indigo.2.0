'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Julian Cardozo
' Created          : 06-04-2013
'
' Last Modified By : Julian Cardozo
' Last Modified On : 06-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel.Activation
Imports DistributedServices.Authentication

<JwtMessageServiceBehavior>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class GlosasService
    Implements IGlosasService

End Class
