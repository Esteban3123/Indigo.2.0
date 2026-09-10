'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 25-06-2013
'
'Last Modified     : Cristhian Mauricio Salzar
'Date              : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class CommonERPService

    ''' <summary>
    ''' Elimina un tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteThirdParty(thirdParty As Domain.Entities.ThirdParty, session As SessionValues) As Boolean Implements ICommonERPThirdParty.DeleteThirdParty
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.DeleteThirdParty(thirdParty, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByNit(nit As String, session As SessionValues) As Domain.Entities.ThirdParty Implements ICommonERPThirdParty.GetThirdPartyByNit
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.GetThirdPartyByNit(nit)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    Public Function ListAllThirdParty(session As SessionValues) As List(Of Domain.Entities.ThirdParty) Implements ICommonERPThirdParty.ListAllThirdParty
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.ListAllThirdParty()
        End Using
    End Function

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta
    ''' </summary>
    Public Function ValidateLenghtNit(ThirdPartyNit As String, IdentificationAcronyms As String, session As SessionValues) As ActionResult(Of Boolean) Implements ICommonERPThirdParty.ValidateLenghtNit
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.ValidateLenghtNit(ThirdPartyNit, IdentificationAcronyms)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita el tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="session">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveThirdParty(thirdParty As Domain.Entities.ThirdParty, session As SessionValues) As Boolean Implements ICommonERPThirdParty.SaveThirdParty
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.SaveThirdParty(thirdParty, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Busca una persona por el numero de identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de identificacion de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPersonByIdentification(identificationNumber As String, session As SessionValues) As Person Implements ICommonERPThirdParty.GetPersonByIdentification
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.GetPersonByIdentification(identificationNumber)
        End Using
    End Function

    ''' <summary>
    ''' Obtener un tercero por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyById(id As Integer, session As SessionValues) As Domain.Entities.ThirdParty Implements ICommonERPThirdParty.GetThirdPartyById
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.GetThirdPartyById(id, session.AuditMessageWcf)
        End Using
    End Function

    Public Function UpdateStateThirdParty(id As Integer, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements ICommonERPThirdParty.UpdateStateThirdParty
        Using thirdPartyAdmin As IThirdPartyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IThirdPartyAdminService)()
            Return thirdPartyAdmin.UpdateStateThirdParty(id, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
