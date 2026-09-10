'************************************************************
' Assembly         : Domain.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-04
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base

Public Interface IRIPSServiceGroupsRepository
    Inherits IRepository(Of RIPSServiceGroups)

    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista con grupos de servicios RIPS</returns>
    Function ListAllRIPSServiceGroups() As List(Of RIPSServiceGroups)

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Function GetRIPSServiceGroupById(ByVal Id As Integer, Optional tracking As Boolean = True) As RIPSServiceGroups

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Function GetRIPSServiceGroupByCode(ByVal code As String) As RIPSServiceGroups
End Interface
