''' <summary>
''' 1|Solicitud de aprovisionamiento, 2|Tenant activo, 3|Tenant suspendido, 4|Tenant inactivo
''' </summary>
Public Enum eTenantStatus As Byte
    ProvisioningRequest = 1
    ActiveTenant = 2
    SuspendedTenant = 3
    InactiveTenant = 4
End Enum