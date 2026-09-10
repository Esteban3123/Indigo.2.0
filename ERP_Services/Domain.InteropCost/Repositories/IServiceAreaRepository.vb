'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.InteropCost.Entities

Public Interface IServiceAreaRepository
    Inherits IRepository(Of GENARESER)

    ''' <summary>
    ''' Obtiene un area de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetServiceArea(ByVal code As String) As GENARESER

    ''' <summary>
    ''' Obtiene un area de servicio por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetServiceAreaById(id As Integer) As GENARESER

End Interface