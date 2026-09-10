'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IHealthAdministratorRepository
    Inherits IRepository(Of HealthAdministrator)

    ''' <summary>
    ''' Obtiene una entidad administradora de salud
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHealthAdministrator(code As String) As HealthAdministrator

    ''' <summary>
    ''' Obtiene una entidad administradora de salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHealthAdministratorById(id As Integer) As HealthAdministrator

    ''' <summary>
    ''' Gets the first type of the health administrator by entity.
    ''' </summary>
    ''' <param name="p1">The p1.</param>
    ''' <returns></returns>
    Function GetFirstHealthAdministratorByEntityType(entityType As Byte) As HealthAdministrator

    ''' <summary>
    ''' Obtiene el tercero que tiene asociado la entidad Administradora
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyHealthAdministratorById(id As Integer, Optional tracking As Boolean = True) As ThirdParty

End Interface
