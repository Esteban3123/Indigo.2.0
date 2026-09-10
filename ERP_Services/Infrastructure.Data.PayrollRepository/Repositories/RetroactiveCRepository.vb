'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-12-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.Data.Base

Public Class RetroactiveCRepository
    Inherits GenericRepository(Of RetroactiveC)

    Implements IRetroactiveCRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtener el Listado de Retroactividad
    ''' </summary>
    ''' <param name="RetroactiveInitialDate">fecha Inicio Retroactivo</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    Public Function GetListRetroactive(RetroactiveInitialDate As Date, GroupId As Integer, Optional employeeId As Integer = 0) As List(Of RetroactiveC) Implements IRetroactiveCRepository.GetListRetroactive
        Dim retroactiveC As IQueryable(Of RetroactiveC)
        If employeeId > 0 Then
            retroactiveC = From e In _context.RetroactiveC.Include("RetroactiveD")
                           Where e.InitialDateRetroactive = RetroactiveInitialDate And e.IdGroup = GroupId And e.IdEmployee = employeeId
                           Select e
        Else
            retroactiveC = From e In _context.RetroactiveC.Include("RetroactiveD")
                           Where e.InitialDateRetroactive = RetroactiveInitialDate And e.IdGroup = GroupId
                           Select e
        End If
        If retroactiveC IsNot Nothing AndAlso retroactiveC.Any() Then
            Return retroactiveC.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtener el Listado de Retroactividad
    ''' </summary>
    ''' <param name="RetroactiveInitialDate">fecha Inicio Retroactivo</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    Public Function GetListRetroactiveByYear(VarYear As Integer, GroupId As Integer) As List(Of RetroactiveC) Implements IRetroactiveCRepository.GetListRetroactiveByYear
        Dim RetroactiveC = From e In _context.RetroactiveC.Include("RetroactiveD")
                           Where Year(e.InitialDateRetroactive) = VarYear And e.IdGroup = GroupId And e.Status = 2
                           Select e

        If RetroactiveC.Count > 0 Then
            Return RetroactiveC.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtener el Listado de Retroactividad por un rango de fechas para un grupo específico, donde tenga una bonificación anterior al pago de retroactividad
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="GroupId"></param>
    ''' <returns></returns>
    Public Function GetListRetroactiveByRangeOfDates(StartDate As Date, EndDate As Date, GroupId As Integer) As List(Of RetroactiveC) Implements IRetroactiveCRepository.GetListRetroactiveByRangeOfDates
        Dim RetroactiveC = From e In _context.RetroactiveC.Include("RetroactiveD")
                           Where (e.NextPayrollDate >= StartDate AndAlso e.NextPayrollDate <= EndDate) AndAlso e.IdGroup = GroupId AndAlso e.Status = 2 AndAlso _context.Liquidation.Where(Function(l) l.EmployeeId = e.IdEmployee AndAlso e.NextPayrollDate > l.PayrollDateLiquidated AndAlso (l.PayrollDateLiquidated >= StartDate AndAlso l.PayrollDateLiquidated <= EndDate And l.LiquidationDetail.Where(Function(ld) ld.ConceptClass = "046").Count() > 0)).Count() > 0
                           Select e

        If RetroactiveC.Count > 0 Then
            Return RetroactiveC.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtener el Listado de Retroactividad
    ''' </summary>
    ''' <param name="RetroactiveInitialDate">fecha Inicio Retroactivo</param>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    Public Function GetListRetroactiveByEmployeeId(VarYear As Integer, EmployeeId As Integer) As RetroactiveC Implements IRetroactiveCRepository.GetListRetroactiveByEmployeeId
        Dim RetroactiveC = From e In _context.RetroactiveC.Include("RetroactiveD").Include("RetroactiveD.Concept")
                           Where Year(e.InitialDateRetroactive) = VarYear And e.IdEmployee = EmployeeId And e.Status = 2
                           Select e

        If RetroactiveC.Count > 0 Then
            Return RetroactiveC.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetListRetroactiveByEmployeeIdBetweenDates(EmployeeId As Integer, InitialDate As Date, EndDate As Date) As List(Of RetroactiveC) Implements IRetroactiveCRepository.GetListRetroactiveByEmployeeIdBetweenDates
        'Solo retroactivos confirmados (Status = 2) que inician dentro de la ventana.
        Dim RetroactiveList = From e In _context.RetroactiveC.Include("RetroactiveD").Include("RetroactiveD.Concept")
                              Where e.IdEmployee = EmployeeId _
                                    And e.InitialDateRetroactive >= InitialDate And e.InitialDateRetroactive <= EndDate _
                                    And e.Status = 2
                              Select e

        If RetroactiveList.Count > 0 Then
            Return RetroactiveList.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetListRetroactiveByContractId(ContractId As Integer) As List(Of RetroactiveC) Implements IRetroactiveCRepository.GetListRetroactiveByContractId
        Dim RetroactiveC = From e In _context.RetroactiveC
                           Where e.IdContract = ContractId
                           Select e

        Return RetroactiveC.ToList()

    End Function


    Public Function GetListRetroactiveByPayrollDate(PayrollDate As Date, GroupId As Integer) As List(Of RetroactiveC) Implements IRetroactiveCRepository.GetListRetroactiveByPayrollDate
        Dim RetroactiveC = From e In _context.RetroactiveC
                           Where e.NextPayrollDate = PayrollDate And e.IdGroup = GroupId
                           Select e

        Return RetroactiveC.ToList()

    End Function
End Class
