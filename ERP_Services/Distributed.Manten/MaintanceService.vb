Imports System.ServiceModel
Imports System.ServiceModel.Activation

<UnityMessageInspectorServiceBehavior()>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class MaintanceService
    Implements IMaintenanceService

End Class

