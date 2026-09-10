'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 25-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IThirdPartyAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyByNit(nit As String) As ThirdParty

    ''' <summary>
    ''' Obtiene el tercero con los agregados de email, telefono y dirección
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    Function GetThirdPartyByNitWithAgregates(nit As String) As ThirdParty

    ''' <summary>
    ''' Obtiene el tipo de telefono
    ''' </summary>
    ''' <returns></returns>
    Function GetPhoneType() As PhoneType

    ''' <summary>
    ''' Busca una persona por el numero de identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de identificacion de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPersonByIdentification(identificationNumber As String) As Person

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    Function ListAllThirdParty() As List(Of ThirdParty)

    ''' <summary>
    ''' Guarda o edita el tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveThirdParty(thirdParty As ThirdParty, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteThirdParty(thirdParty As ThirdParty, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetThirdPartyById(ByVal idThirdParty As Integer, ByVal audit As AuditMessage) As ThirdParty


    Function UpdateStateThirdParty(id As Integer, state As Boolean, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta
    ''' </summary>
    Function ValidateLenghtNit(ByVal ThirdPartyNit As String, ByVal IdentificationAcronyms As String) As ActionResult(Of Boolean)

End Interface
