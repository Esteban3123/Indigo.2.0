'************************************************************
' Assembly         : Domain.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 25-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Common.Entities
Imports Domain.Base
Imports Domain.Base.Entities

Public Interface IThirdPartyRepository
    Inherits IRepository(Of ThirdParty)

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyByNit(nit As String, Optional tracking As Boolean = True) As ThirdParty

    ''' <summary>
    ''' Obtiene el tipo de telefono
    ''' </summary>
    ''' <returns></returns>
    Function GetPhoneType() As PhoneType

    ''' <summary>
    ''' Consulta el tercero por nit con los agregados de email, telefono y dirección
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetThirdPartyByNitWithAgregates(nit As String, Optional tracking As Boolean = True) As ThirdParty

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    Function ListAllThirdParty() As List(Of ThirdParty)

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyById(idThirdParty As Integer, Optional tracking As Boolean = True) As ThirdParty

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateLenghtNit(ByVal ThirdPartyNit As String, ByVal IdentificationAcronyms As String) As ActionResult(Of Boolean)

    ''' <summary>
    ''' Lista todos los terceros por nit 
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    Function ListAllThirdParty(ByVal ThirdPartyNitList As List(Of String)) As List(Of ThirdParty)

End Interface
