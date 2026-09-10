'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 14-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports System.Linq.Expressions

Public Class BranchOfficeRepository
    Inherits GenericRepository(Of BranchOffice)
    Implements IBranchOfficeRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una sucursal especifica
    ''' </summary>
    ''' <param name="code">Codigo de la sucursal</param>
    ''' <returns>Sucursal</returns>
    ''' <remarks></remarks>
    Public Function GetBranchOffice(code As String, Optional ByVal tracking As Boolean = True) As BranchOffice Implements IBranchOfficeRepository.GetBranchOffice
        Dim branchOffice As IQueryable(Of BranchOffice)
        If tracking = True Then
            branchOffice = From e In _context.BranchOffice
                           Where e.Code = code
                           Select e
        Else
            branchOffice = From e In _context.BranchOffice.AsNoTracking()
                           Where e.Code = code
                           Select e
        End If
        If branchOffice.Count > 0 Then
            Return branchOffice.FirstOrDefault()
        Else
            Return New BranchOffice()
        End If
    End Function

    ''' <summary>
    ''' Lista todos las sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllBranchOffice() As List(Of BranchOffice) Implements IBranchOfficeRepository.ListAllBranchOffice
        Dim branchOffice = From e In _context.BranchOffice.Include("Company")
                           Select e
        Return branchOffice.ToList
    End Function

    ''' <summary>
    ''' Obtiene un listado de sucursales filtrado por el id de la empresa
    ''' </summary>
    ''' <param name="CompanyId">id de la empresa</param>
    ''' <returns>listado de sucursales</returns>
    ''' <remarks></remarks>
    Public Function GetBranchOfficeByCompanyId(CompanyId As Integer) As List(Of BranchOffice) Implements IBranchOfficeRepository.GetBranchOfficeByCompanyId
        Dim branchOffice = From e In _context.BranchOffice.Include("City")
                           Select e Where e.CompanyId = CompanyId
        Return branchOffice.ToList
    End Function

    ''' <summary>
    ''' Obtiene la sucursal por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetBranchOfficeById(Id As Integer) As BranchOffice Implements IBranchOfficeRepository.GetBranchOfficeById
        Return (From x In _context.BranchOffice.AsNoTracking() Where x.Id = Id Select x).FirstOrDefault()
    End Function

End Class
