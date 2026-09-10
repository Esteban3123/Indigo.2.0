'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo 
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class CompanyRepository
    Inherits GenericRepository(Of Company)
    Implements ICompanyRepository


    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCompany(nit As String, Optional tracking As Boolean = True) As Company Implements ICompanyRepository.GetCompany
        Dim company = From e In _context.Company.Include("ThirdParty").Include("ThirdParty.Person").Include("ThirdParty.Person.phone").Include("ThirdParty.Person.Address").Include("ThirdParty.Person.Email")
        Where e.Nit = nit
                       Select e

        '..Include("ThirdParty").Include("ThirdParty.Person").Include("ThirdParty.Person.phone").Include("ThirdParty.Person.Address").Include("ThirdParty.Person.Email")
        If company.Count > 0 Then
            Dim objCompany = Nothing
            If tracking = False Then
                objCompany = (From e In _context.Company.AsNoTracking
                       Where e.Nit = nit
                       Select e).SingleOrDefault
            Else
                objCompany = company.SingleOrDefault()
            End If

            If objCompany.ThirdParty IsNot Nothing AndAlso objCompany.ThirdParty.Person IsNot Nothing Then
                If objCompany.ThirdParty.Person.Address IsNot Nothing AndAlso  objCompany.ThirdParty.Person.Address.Count > 0 Then
                    For Each address In objCompany.ThirdParty.Person.Address
                        If address.DepartmentId IsNot Nothing Then
                            Dim departmentId As Integer = address.DepartmentId
                            Dim cityId As Integer = address.CityId
                            address.DepartmentName = (From d In _context.Department Where d.Id = departmentId Select d.Name).SingleOrDefault()
                            If address.CityId IsNot Nothing Then
                                address.CityName = (From d In _context.City Where d.Id = cityId Select d.Name).FirstOrDefault()
                            End If
                        End If
                    Next
                End If
            End If

            Return objCompany
        Else
            Return New Company()
        End If
    End Function

    Public Function ListAllCompany() As List(Of Company) Implements ICompanyRepository.ListAllCompany
        Dim company = From e In _context.Company.Include("ThirdParty")
                      Select e
        company.ToList().ForEach(Sub(i)
                                     i.CodeNameConcatenated = i.Nit & " - " & i.Name
                                 End Sub)
        Return company.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una compañia por id
    ''' </summary>
    ''' <param name="id">id de la compañia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompanyById(companyId As Integer) As Company Implements ICompanyRepository.GetCompanyById
        Dim queryCompany = From e In _context.Company.Include("ThirdParty").Include("Fund")
                           Where e.Id = companyId
                           Select e
        If queryCompany.Count > 0 Then
            Return queryCompany.FirstOrDefault()
        Else
            Return New Company()
        End If
    End Function

End Class
