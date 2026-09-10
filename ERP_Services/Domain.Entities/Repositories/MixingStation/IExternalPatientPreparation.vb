'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Andrea Pahola Coqueco
' Created          : 19/10/2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IExternalPatientPreparation
    Inherits IRepository(Of ExternalPatientPreparation)

    ''' <summary>
    ''' Obtiene una preparación de un paciente externo por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetExternalPatientPreparationById(id As String, Optional tracking As Boolean = True) As ExternalPatientPreparation

End Interface

