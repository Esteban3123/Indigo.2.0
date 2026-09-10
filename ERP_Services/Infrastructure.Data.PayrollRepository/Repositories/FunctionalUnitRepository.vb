'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 19-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class FunctionalUnitRepository
    Inherits GenericRepository(Of FunctionalUnit)
    Implements IFunctionalUnitRepository


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
    ''' Obtiene una unidad funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit(code As String, Optional tracking As Boolean = True) As FunctionalUnit Implements IFunctionalUnitRepository.GetFunctionalUnit
        Dim functionalUnit As IQueryable(Of FunctionalUnit)
        If tracking = True Then
            functionalUnit = From e In _context.FunctionalUnit.Include("BranchOffice").Include("BranchOffice.Company").Include("FunctionalUnitResponsible").Include("FunctionalUnitUser").Include("BlockSchedule").Include("FunctionalUnitUserAuthorizationRequest")
                             Where e.Code = code
                             Select e

        Else
            functionalUnit = From e In _context.FunctionalUnit.AsNoTracking
                             Where e.Code = code
                             Select e
        End If
        If functionalUnit.Count > 0 Then
            Return functionalUnit.SingleOrDefault()
        Else
            Return New FunctionalUnit()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnit() As List(Of FunctionalUnit) Implements IFunctionalUnitRepository.ListAllFunctionalUnit
        Dim functionalUnit = From e In _context.FunctionalUnit.Include("BranchOffice").Include("BranchOffice.Company")
                             Select e
        Return functionalUnit.ToList()
    End Function

  
    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <param name="id">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Function GetFunctionalUnitById(id As String, Optional tracking As Boolean = True) As FunctionalUnit Implements IFunctionalUnitRepository.GetFunctionalUnitById
        Dim functionalUnit As IQueryable(Of FunctionalUnit)
        If tracking Then
            functionalUnit = From e In _context.FunctionalUnit.Include("BranchOffice").Include("BranchOffice.Company")
                            Where e.Id = id
                            Select e
        Else
            functionalUnit = From e In _context.FunctionalUnit.AsNoTracking()
                            Where e.Id = id
                            Select e
        End If
        If functionalUnit.Count > 0 Then
            Return functionalUnit.SingleOrDefault
        Else
            Return New FunctionalUnit()
        End If
    End Function
End Class
