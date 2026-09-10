

Imports Domain.Base

Public Interface INPTConfigurationRepository
    Inherits IRepository(Of NPTConfiguration)

    Function ListAllNPTConfiguration() As List(Of NPTConfiguration)
End Interface
