'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 25-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Base

Public Class ThirdPartyRepository
    Inherits GenericRepository(Of ThirdParty)
    Implements IThirdPartyRepository, Inject

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByNit(nit As String, Optional tracking As Boolean = True) As ThirdParty Implements IThirdPartyRepository.GetThirdPartyByNit
        If tracking Then
            Dim thirdParty = (From e In _context.ThirdParty.Include("Person").Include("ThirdPartyBranchOffice").Include("ThirdPartyFiscalResponsibility").Include("ThirdPartyEconomicActivities").Include("ThirdPartyTaxExemptions")
                              Where e.Nit = nit
                              Select e).FirstOrDefault
            If thirdParty IsNot Nothing Then
                If thirdParty.IVARetentionConceptId IsNot Nothing Then
                    thirdParty.IVARetentionConceptDescription = (From x In _context.RetentionConcepts.AsNoTracking() Where x.Id = thirdParty.IVARetentionConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                End If

                If thirdParty.Person.IdentificacionCityId IsNot Nothing Then
                    Dim city = (From c In _context.City.AsNoTracking Where c.Id = thirdParty.Person.IdentificacionCityId Select c).FirstOrDefault
                    thirdParty.Person.CityDescription = city.Code + " - " + city.Name
                End If

                If thirdParty.ThirdPartyFiscalResponsibility IsNot Nothing AndAlso thirdParty.ThirdPartyFiscalResponsibility.Count > 0 Then
                    For Each item In thirdParty.ThirdPartyFiscalResponsibility.ToList()
                        Dim fiscalResponsability = (From fr In _context.FiscalResponsibility.AsNoTracking Where fr.Id = item.FiscalResponsibilityId Select fr).FirstOrDefault
                        item.Code = fiscalResponsability.Code
                        item.Name = fiscalResponsability.Name
                    Next
                End If

                If thirdParty.ThirdPartyEconomicActivities?.Any() Then
                    For Each item In thirdParty.ThirdPartyEconomicActivities.ToList()
                        Dim fiscalResponsability = (From ea In _context.EconomicActivity.AsNoTracking Where ea.Id = item.EconomicActivityId Select ea).FirstOrDefault
                        item.Code = fiscalResponsability.Code
                        item.Name = fiscalResponsability.Name
                    Next
                End If

                If thirdParty.ThirdPartyTaxExemptions?.Any() Then
                    For Each item In thirdParty.ThirdPartyTaxExemptions.ToList()
                        item.DocumentTypeCodeName = (From te In _context.TaxExemptions.AsNoTracking Where te.Id = item.DocumentTypeId Select String.Concat(te.Code, " - ", te.Description)).FirstOrDefault
                        item.InstitutionCodeName = (From te In _context.TaxExemptions.AsNoTracking Where te.Id = item.InstitutionId Select String.Concat(te.Code, " - ", te.Description)).FirstOrDefault
                        item.ExemptFeeCodeName = (From gl In _context.GeneralLedgerIVA.AsNoTracking Where gl.Id = item.ExemptFeeId Select String.Concat(gl.Code, " - ", gl.Name)).FirstOrDefault
                    Next
                End If

                If thirdParty?.Person?.IdentificationTypeId IsNot Nothing Then
                    Dim ADTIPOIDENTIFICA = (From x In _context.ADTIPOIDENTIFICA.AsNoTracking() Where x.ID = thirdParty.Person.IdentificationTypeId Select x).FirstOrDefault
                    thirdParty.Person.IdentificationType = Utils.IdentificationTypeObsolete(ADTIPOIDENTIFICA?.SIGLA).Item1
                    thirdParty.Person.IdentificationTypeName = $"{ADTIPOIDENTIFICA.CODIGO} - {ADTIPOIDENTIFICA.NOMBRE}"
                End If

                Return thirdParty
            Else
                Dim person = (From e In _context.Person
                              Where e.IdentificationNumber = nit
                              Select e)
                If person.Count > 0 Then
                    Return New ThirdParty() With {.Person = person.SingleOrDefault(), .PersonId = person.SingleOrDefault().Id}
                End If
                Return New ThirdParty()
            End If
        Else
            Dim thirdParty = From e In _context.ThirdParty.AsNoTracking.AsNoTracking
                             Where e.Nit = nit
                             Select e
            If thirdParty.Count > 0 Then
                Return thirdParty.SingleOrDefault
            Else
                Return New ThirdParty()
            End If
        End If

    End Function

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta dependiendo de lo parametrizado
    ''' </summary>
    Private Function ValidateLenghtNit(ByVal ThirdPartyNit As String, ByVal IdentificationAcronyms As String) As ActionResult(Of Boolean) Implements IThirdPartyRepository.ValidateLenghtNit

        Dim ADTIPOIDENTIFICA = (From x In _context.ADTIPOIDENTIFICA.AsNoTracking()
                                Where x.SIGLA = IdentificationAcronyms
                                Select x).FirstOrDefault

        Dim MensajeResult As String = "El número de " + ADTIPOIDENTIFICA.SIGLA + " ingresado no cumple con el requisito de longitud. El tipo de identificación seleccionado permite {0} {1} caracteres."
        If ADTIPOIDENTIFICA.MaximumLength IsNot Nothing Then
            If ThirdPartyNit.Length > ADTIPOIDENTIFICA.MaximumLength Then
                MensajeResult = String.Format(MensajeResult, "máximo", ADTIPOIDENTIFICA.MaximumLength.ToString())
                Return New ActionResult(Of Boolean) With {.StateResult = False, .Message = MensajeResult}
            End If
        End If

        If ADTIPOIDENTIFICA.MinimunLength IsNot Nothing Then
            If ThirdPartyNit.Length < ADTIPOIDENTIFICA.MinimunLength Then
                MensajeResult = String.Format(MensajeResult, "mínimo", ADTIPOIDENTIFICA.MinimunLength.ToString())
                Return New ActionResult(Of Boolean) With {.StateResult = False, .Message = MensajeResult}
            End If
        End If

        Return New ActionResult(Of Boolean) With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    Public Function ListAllThirdParty() As List(Of ThirdParty) Implements IThirdPartyRepository.ListAllThirdParty
        Dim thirdParty = From e In _context.ThirdParty.Include("Person")
                         Select e
        Return thirdParty.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una dependencia especifica
    ''' </summary>
    ''' <param name="idThirdParty"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyById(idThirdParty As Integer, Optional tracking As Boolean = True) As ThirdParty Implements IThirdPartyRepository.GetThirdPartyById
        If idThirdParty = 0 Then
            Throw New ArgumentNullException("idThirdParty")
        End If
        If tracking Then
            Dim res = (From d As ThirdParty In Me._context.ThirdParty.Include("Person").Include("Customer.ContractExternalClients") Where d.Id = idThirdParty Select d).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res(0)
            Else
                Return New ThirdParty()
            End If
        Else
            Dim res = (From d As ThirdParty In Me._context.ThirdParty.AsNoTracking().Include("ThirdPartyFiscalResponsibility").AsNoTracking().Include("ThirdPartyFiscalResponsibility.FiscalResponsibility").AsNoTracking().Include("Person").AsNoTracking().Include("Person.Address").AsNoTracking().Include("Person.Email").AsNoTracking().Include("Customer.ContractExternalClients").AsNoTracking() Where d.Id = idThirdParty Select d).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Dim thirdparty = res(0)
                If thirdparty.Person IsNot Nothing Then

                    Dim ADTIPOIDENTIFICA = (From x In _context.ADTIPOIDENTIFICA.AsNoTracking() Where x.ID = thirdparty.Person.IdentificationTypeId Select x)?.FirstOrDefault
                    thirdparty.Person.DocumentTypeAbbreviation = ADTIPOIDENTIFICA?.SIGLA

                    If thirdparty.Person.Address IsNot Nothing AndAlso thirdparty.Person.Address.Count > 0 Then
                        For Each address In thirdparty.Person.Address
                            If address.DepartmentId IsNot Nothing Then
                                Dim department = (From d In _context.Department.AsNoTracking.Include("Country") Where d.Id = address.DepartmentId Select d).FirstOrDefault
                                address.DepartmentCode = department.Code
                                address.DepartmentName = department.Name
                                address.CountryStandardCode = department.Country.StandardCode
                                If address.CityId IsNot Nothing Then
                                    Dim city = (From d In _context.City.AsNoTracking Where d.Id = address.CityId Select d).FirstOrDefault
                                    address.CityCode = city.Code
                                    address.CityName = city.Name
                                End If
                            End If
                        Next
                    End If
                End If
                Return thirdparty
            Else
                Return New ThirdParty()
            End If
        End If
    End Function

    ''' <summary>
    ''' Consulta el tercero por nit con los agregados de email, telefono y dirección
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetThirdPartyByNitWithAgregates(nit As String, Optional tracking As Boolean = True) As ThirdParty Implements IThirdPartyRepository.GetThirdPartyByNitWithAgregates
        If tracking Then
            Dim thirdParty = (From e In _context.ThirdParty.Include("Person").Include("Person.Email").Include("Person.Phone").Include("Person.Address")
                              Where e.Nit = nit
                              Select e).FirstOrDefault

            If thirdParty IsNot Nothing Then

                Return thirdParty
            Else
                Dim persons = (From e In _context.Person
                               Where e.IdentificationNumber = nit
                               Select e)
                If persons.Count > 0 Then
                    Dim person = persons.SingleOrDefault()
                    If person.Address IsNot Nothing AndAlso person.Address.Count > 0 Then
                        For Each address In person.Address
                            If address.DepartmentId IsNot Nothing Then
                                address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                                If address.CityId IsNot Nothing Then
                                    address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                                End If
                            End If
                        Next
                    End If
                    Return New ThirdParty() With {.Person = person, .PersonId = person.Id}
                End If
                Return New ThirdParty()
            End If
        Else
            Dim thirdPartys = From e In _context.ThirdParty.AsNoTracking.Include("Person").AsNoTracking.Include("Person.Email").AsNoTracking.Include("Person.Phone").AsNoTracking.Include("Person.Address").AsNoTracking
                              Where e.Nit = nit
                              Select e
            If thirdPartys.Count > 0 Then
                Dim thirdparty = thirdPartys.SingleOrDefault()
                If thirdparty.Person IsNot Nothing Then

                    If thirdparty.Person.Address IsNot Nothing AndAlso thirdparty.Person.Address.Count > 0 Then
                        For Each address In thirdparty.Person.Address
                            If address.DepartmentId IsNot Nothing Then
                                address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                                If address.CityId IsNot Nothing Then
                                    address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                                End If
                            End If
                        Next
                    End If
                End If
                Return thirdparty
            Else
                Return New ThirdParty()
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene el tipo de telefono
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPhoneType() As PhoneType Implements IThirdPartyRepository.GetPhoneType
        Return (From x In _context.PhoneType.AsNoTracking Select x).FirstOrDefault()
    End Function
    ''' <summary>
    ''' Lista todos los terceros por lista de nit
    ''' </summary>
    ''' <param name="thirdPartyNitList"></param>
    ''' <returns></returns>
    Public Function ListAllThirdParty(thirdPartyNitList As List(Of String)) As List(Of ThirdParty) Implements IThirdPartyRepository.ListAllThirdParty
        If Not thirdPartyNitList?.Any() Then
            Return New List(Of ThirdParty)
        End If

        Return (From e In _context.ThirdParty Where thirdPartyNitList.Contains(e.Nit) Select e).ToList()
    End Function
End Class
