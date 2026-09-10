#Region "Imports"

Imports Application.Common
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class CommonERPService

    Public Function DeleteCountry(country As Domain.Entities.Country, session As SessionValues) As ActionMessageResult(Of Domain.Entities.Country) Implements ICommonERPService.DeleteCountry
        Using CountryAdmin As ICountryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICountryAdminService)()
            Return CountryAdmin.DeleteCountry(country, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetCountry(code As String, session As SessionValues) As Domain.Entities.Country Implements ICommonERPService.GetCountry
        Using CountryAdmin As ICountryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICountryAdminService)()
            Return CountryAdmin.GetCountry(code)
        End Using
    End Function

    Public Function ListAllCountry(session As SessionValues) As List(Of Domain.Entities.Country) Implements ICommonERPService.ListAllCountry
        Using CountryAdmin As ICountryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICountryAdminService)()
            Return CountryAdmin.ListAllCountry()
        End Using
    End Function

    Public Function SaveCountry(country As Domain.Entities.Country, session As SessionValues, idSequence As Long) As ActionResult(Of Country) Implements ICommonERPService.SaveCountry
        Using CountryAdmin As ICountryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICountryAdminService)()
            Return CountryAdmin.SaveCountry(country, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Public Function GetCountryById(ByVal idCountry As Integer, session As SessionValues) As Domain.Entities.Country Implements ICommonERPService.GetCountryById
        Using CountryAdmin As ICountryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICountryAdminService)()
            Return CountryAdmin.GetCountryById(idCountry, session.AuditMessageWcf)
        End Using
    End Function

End Class
