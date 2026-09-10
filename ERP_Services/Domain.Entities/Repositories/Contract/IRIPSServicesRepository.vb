'************************************************************
' Assembly         : Domain.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-12
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base

Public Interface IRIPSServicesRepository
    Inherits IRepository(Of RIPSServices)

    ''' <summary>
    ''' Lista todos los servicios RIPS
    ''' </summary>
    ''' <returns>Lista de servicios RIPS</returns>
    Function ListAllRIPSServices() As List(Of RIPSServices)

    ''' <summary>
    ''' Obtiene un servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>servicio RIPS</returns>
    Function GetRIPSServiceById(ByVal Id As Integer, Optional tracking As Boolean = True) As RIPSServices

    ''' <summary>
    ''' Obtiene un servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <returns>sServicios RIPS</returns>
    Function GetRIPSServiceByCode(ByVal code As String) As RIPSServices

End Interface
