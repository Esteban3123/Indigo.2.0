'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IExternalCareCenterRepository
    Inherits IRepository(Of ExternalCareCenter)

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetExternalCareCenter(code As String, Optional tracking As Boolean = True) As ExternalCareCenter

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetExternalCareCenterById(id As String, Optional tracking As Boolean = True) As ExternalCareCenter

End Interface
