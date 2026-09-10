''' <summary>
''' Representa el estado de la integración con el
''' sistemas asistencial HIS
''' </summary>
Public Enum IntegrationStatus

    ''' <summary>
    ''' Conectado al sistema asistencia
    ''' </summary>
    Connected
    ''' <summary>
    ''' Desconectado al sistema asistencial
    ''' </summary>
    Disconnected
    ''' <summary>
    ''' No integrado al sistema asistencial
    ''' </summary>
    NonIntegrated
    ''' <summary>
    ''' Usuario Inactivo
    ''' </summary>
    InactiveUser
    ''' <summary>
    ''' Validación en progreso
    ''' </summary>
    InProgress

End Enum