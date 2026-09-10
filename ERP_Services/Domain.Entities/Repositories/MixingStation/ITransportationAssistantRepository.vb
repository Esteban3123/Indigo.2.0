'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ITransportationAssistantRepository
    Inherits IRepository(Of TransportationAssistant)

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetTransportationAssistant(code As String, Optional tracking As Boolean = True) As TransportationAssistant

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetTransportationAssistantById(id As String, Optional tracking As Boolean = True) As TransportationAssistant

End Interface
