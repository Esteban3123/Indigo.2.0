'************************************************************
' Assembly         : Domain.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Common.Entities
Imports Domain.Base
Public Interface IPersonRepository
    Inherits IRepository(Of Person)

    ''' <summary>
    ''' Busca una persona atraves de su identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de identificacion de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPersonByIdentification(identificationNumber As String, Optional tracking As Boolean = True) As Person

    ''' <summary>
    ''' Lista todas las personas
    ''' </summary>
    ''' <returns>Lista de personas</returns>
    ''' <remarks></remarks>
    Function ListAllPerson() As List(Of Person)

    ''' <summary>
    ''' Eliminar el Tercero,  direcciones, telefonos, Correos y persona
    ''' </summary>
    ''' <param name="NitThirdParty">Nit del tercero</param>
    ''' <returns></returns>
    Function SP_DeleteThirdParty(NitThirdParty As String) As SP_DeleteThirdParty_Result

End Interface
