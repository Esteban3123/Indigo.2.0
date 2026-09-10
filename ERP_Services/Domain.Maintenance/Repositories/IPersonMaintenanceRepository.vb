'************************************************************
' Assembly         : Domain.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Common.Entities
Imports Domain.Base
Public Interface IPersonMaintenanceRepository
    Inherits IRepository(Of Domain.Maintenance.Entities.Person)

    ''' <summary>
    ''' Busca una persona atraves de su identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de identificacion de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPersonByIdentification(identificationNumber As String) As Domain.Maintenance.Entities.Person

    ''' <summary>
    ''' Lista todas las personas
    ''' </summary>
    ''' <returns>Lista de personas</returns>
    ''' <remarks></remarks>
    Function ListAllPerson() As List(Of Domain.Maintenance.Entities.Person)

End Interface
