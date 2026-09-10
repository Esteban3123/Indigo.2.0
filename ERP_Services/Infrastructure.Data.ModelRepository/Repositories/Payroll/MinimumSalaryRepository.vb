'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Juan Pablo Daza Medina 
' Created          : 26-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Public Class MinimumSalaryRepository
    Inherits GenericRepository(Of MinimumSalary)
    Implements IMinimumSalaryRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todos los salarios minimimos 
    ''' </summary>
    ''' <returns>Lista salaarios minimos</returns>
    ''' <remarks></remarks>
    Public Function ListAllMinimumSalary() As List(Of MinimumSalary) Implements IMinimumSalaryRepository.ListAllMinimumSalary
        Dim minimumSalary = From e In _context.MinimumSalary
                            Select e
        Return minimumSalary.ToList
    End Function

    ''' <summary>
    ''' Obtiene un año especifivo
    ''' </summary>
    ''' <param name="year">año</param>
    ''' <returns>año</returns>
    ''' <remarks></remarks>
    Public Function GetMinimunSalaryByYear(Year As String, Optional tracking As Boolean = True) As MinimumSalary Implements IMinimumSalaryRepository.GetMinimunSalaryByYear
        Dim minimumSalary = From e In _context.MinimumSalary
                            Where e.Year = Year
                            Select e
        If minimumSalary.Count > 0 Then
            Dim ObjminimumSalary = Nothing
            If tracking = False Then
                ObjminimumSalary = (From e In _context.MinimumSalary.AsNoTracking
                                    Where e.Year = Year
                                    Select e).SingleOrDefault
            Else
                ObjminimumSalary = minimumSalary.SingleOrDefault
            End If
            Return ObjminimumSalary
        Else
            Return New MinimumSalary()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un Salario minimo por el identificador
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetMinimunSalaryById(Id As Integer, Optional tracking As Boolean = True) As MinimumSalary Implements IMinimumSalaryRepository.GetMinimunSalaryById
        Dim minimumSalary = From e In _context.MinimumSalary
                            Where e.Id = Id
                            Select e
        If minimumSalary.Count > 0 Then
            Dim ObjminimumSalary = Nothing
            If tracking = False Then
                ObjminimumSalary = (From e In _context.MinimumSalary.AsNoTracking
                                    Where e.Id = Id
                                    Select e).SingleOrDefault
            Else
                ObjminimumSalary = minimumSalary.SingleOrDefault
            End If
            Return ObjminimumSalary
        End If
        Return New MinimumSalary()
    End Function
End Class
