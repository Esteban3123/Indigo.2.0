#Region "Imports"

Imports Domain.Base

#End Region

Public Interface ICommonHealthProfessionalRepository
    Inherits IRepository(Of HealthProfessional)


    Function GetHealthProfessionalByCode(identificationNumber As String) As HealthProfessional

End Interface
