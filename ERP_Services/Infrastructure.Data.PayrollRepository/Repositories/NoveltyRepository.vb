'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-07-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 20-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class NoveltyRepository

    Inherits GenericRepository(Of Novelty)
    Implements INoveltyRepository

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
    ''' Obtiene las Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeInability(employeeId As Integer) As List(Of Novelty) Implements INoveltyRepository.GetEmployeeNovelty
        Dim inability = From e In _context.Novelty
                          Where e.EmployeeId = employeeId
                          Select e
        If inability.Count > 0 Then
            Return inability.ToList()
        Else
            Return New List(Of Novelty)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    Public Function GetInability(code As String) As Novelty Implements INoveltyRepository.GetNovelty
        Dim inability = From e In _context.Novelty
                           Where e.Consecutive = code
                           Select e
        If inability.Count > 0 Then
            Return inability.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Lista las Novedades de un empleado y filtra por tipo de novedad (Sancion, Licencia o Incapacidad)
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="typeNovelty">Tipo de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityByTypeNovelty(employeeId As Integer, typeNovelty As Byte) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyByTypeNovelty
        Dim inability = From e In _context.Novelty
                        Where e.TypeNovelty = typeNovelty And e.EmployeeId = employeeId
                         Select e
        Return inability.ToList()
    End Function

    ''' <summary>
    ''' Lista las incapacidades de un empleados que esten liquidadas o no 
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="liquidate">Si esta liquidado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityLiquidate(employeeId As Integer, liquidate As Boolean) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyLiquidate
        Dim inability = From e In _context.Novelty
                        Where e.EmployeeId = employeeId And e.NoveltyLiquidate = liquidate
                         Select e
        Return inability.ToList()
    End Function

    ''' <summary>
    ''' Funcion que lista las novedades de un empleado en un rango establecido
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="initialDate">Fecha Inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByEmployeeDateInitialEnd(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyByEmployeeDateInitialEnd
        Dim inability = From e In _context.Novelty
                        Where e.EmployeeId = employeeId And ((e.RealDate >= initialDate And e.RealDate <= endDate) Or (e.EndDate >= initialDate And e.EndDate <= endDate) Or (initialDate >= e.RealDate And endDate <= e.EndDate))
                         Select e
        Return inability.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una novedad por id
    ''' </summary>
    ''' <param name="id">id de la Incapacidad</param>
    ''' <returns>Novedad</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyById(id As Integer, Optional tracking As Boolean = True) As Novelty Implements INoveltyRepository.GetNoveltyById
        Dim inability As IQueryable(Of Novelty)
        If tracking = True Then
            inability = From e In _context.Novelty.Include("Employee").Include("Employee.ThirdParty").Include("Employee.Contract").Include("Employee.Contract.Group") _
                        .Include("Employee.Contract.FunctionalUnit").Include("Employee.Contract.FunctionalUnit.BranchOffice")
                           Where e.Id = id
                           Select e
        Else
            inability = From e In _context.Novelty.AsNoTracking()
                           Where e.Id = id
                           Select e
        End If
        If inability.Count > 0 Then
            Return inability.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="noveltyId">id de la novedad que se va a exonerar</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyDistinctNoveltyBetweenDate(noveltyId As Integer, employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyDistinctNoveltyBetweenDate
        Dim inability = From e In _context.Novelty
                        Where e.Id <> noveltyId And e.EmployeeId = employeeId And ((e.RealDate >= initialDate And e.RealDate <= endDate) Or (e.EndDate >= initialDate And e.EndDate <= endDate) Or (initialDate >= e.RealDate And endDate <= e.EndDate))
                        Select e
        Return inability.ToList()
    End Function

    ''' <summary>
    ''' Obtiene la novedad que tenga la fecha mas alta del mismo codigo consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUltimateNoveltyConsecutive(consecutive As Integer) As Novelty Implements INoveltyRepository.GetUltimateNoveltyConsecutive
        Dim Querydate = From e In _context.Novelty
                   Where e.Consecutive = consecutive
                   Select e.EndDate
        Dim dateEnd = Querydate.Max()
        Dim inability = From e In _context.Novelty.Include("Employee").Include("Employee.Contract").Include("Employee.Contract.Group") _
                        .Include("Employee.Contract.FunctionalUnit").Include("Employee.Contract.FunctionalUnit.BranchOffice")
                           Where e.Consecutive = consecutive And e.EndDate = dateEnd
                           Select e
        If inability.Count > 0 Then
            Return inability.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="listEmployeeId">Lista de id de empleados</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId As List(Of Integer), initialDate As Date, endDate As Date) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyByListIdEmployeeBetweenDate
        Dim inability = From e In _context.Novelty.Include("Employee").Include("Employee.ThirdParty")
                        Where listEmployeeId.Any(Function(x) x = e.EmployeeId) = True And ((e.RealDate >= initialDate And e.RealDate <= endDate) Or (e.EndDate >= initialDate And e.EndDate <= endDate) Or (initialDate >= e.RealDate And endDate <= e.EndDate))
                        Select e
        Return inability.ToList()
    End Function

    ''' <summary>
    ''' Obtiene el Listado de Novedades de un Empleado para liquidarlas por Liquidación de Nómina. Incapacidades NO pagadas o Liquidadas PARCIALMENTE
    ''' </summary>
    ''' <param name="EmployeeId">Id Empleado</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByEmployeeIdPayrollLiquidation(EmployeeId As Integer) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyByEmployeeIdPayrollLiquidation
        Dim inability = From e In _context.Novelty
                        Where e.EmployeeId = EmployeeId And e.Status <> 1
                        Select e
        Return inability.ToList()
    End Function

    Public Function GetNoveltyUnpaidLicenses(EmployeeId As Integer) As List(Of Novelty) Implements INoveltyRepository.GetNoveltyUnpaidLicenses
        Dim inability = From e In _context.Novelty
                      Where e.EmployeeId = EmployeeId And e.LicenseClass = 2
                      Select e
        Return inability.ToList()
    End Function

    Public Function GetNoveltySanctions(EmployeeId As Integer) As List(Of Novelty) Implements INoveltyRepository.GetNoveltySanctions
        Dim inability = From e In _context.Novelty
                      Where e.EmployeeId = EmployeeId And e.TypeNovelty = 2
                      Select e
        Return inability.ToList()
    End Function

    Public Function GetListNoveltyByConsecutive(Consecutive As Integer) As List(Of Novelty) Implements INoveltyRepository.GetListNoveltyByConsecutive
        Dim inability = From e In _context.Novelty
                     Where e.Consecutive = Consecutive
                     Select e
        Return inability.ToList()
    End Function



End Class
