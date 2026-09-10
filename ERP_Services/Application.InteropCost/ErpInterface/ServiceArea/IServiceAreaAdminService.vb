'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.InteropCost.Entities

Public Interface IServiceAreaAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un area de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetServiceArea(ByVal code As String) As ActionResult(Of GENARESER)

    ''' <summary>
    ''' Obtiene un area de servicio por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetServiceAreaById(id As Integer) As ActionResult(Of GENARESER)

End Interface