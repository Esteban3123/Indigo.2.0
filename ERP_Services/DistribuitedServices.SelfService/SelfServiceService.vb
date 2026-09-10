#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
#End Region

<UnityMessageInspectorServiceBehavior()> _
<UnityInstanceProviderServiceBehavior()> _
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
Public Class SelfServiceService
    Implements ISelfServiceService

    Private _selftServiceAdminService As ISelfServiceRequestVacation
    Public Sub New(selftServiceAdminService As ISelfServiceRequestVacation)
        If selftServiceAdminService Is Nothing Then
            Throw New ArgumentException("El selftServiceAdminService no puede ser nulo")
        End If
        _selftServiceAdminService = selftServiceAdminService
    End Sub

End Class