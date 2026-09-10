'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Data.Entity.Infrastructure

Public Class UnemployedLiquidationRepository

    Inherits GenericRepository(Of UnemployedLiquidation)
    Implements IUnemployedLiquidationRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Devuelve las liquidaciones de cesantías que tenga determinado empleado en determinado periodo de tiempo
    ''' </summary>
    ''' <param name="EmployeeId">id del empleado</param>
    ''' <param name="InitialDate">fecha inicio</param>
    ''' <param name="EndingDate">fecha fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUnemployedLiquidationEmployeeByDate(ByVal EmployeeId As Integer, ByVal InitialDate As Date, ByVal EndingDate As Date) As List(Of UnemployedLiquidation) Implements IUnemployedLiquidationRepository.GetUnemployedLiquidationEmployeeByDate
        Dim unemployedLiquidation = From e In _context.UnemployedLiquidation.Include("UnemployedLiquidationDetail").Include("Employee").Include("Employee.ThirdParty")
                                    Where e.EmployeeId = EmployeeId And e.UnemployedInitialDate >= InitialDate And e.UnemployedEndingDate <= EndingDate
        If unemployedLiquidation.Count > 0 Then
            Return unemployedLiquidation.ToList()
        Else
            Return New List(Of UnemployedLiquidation)
        End If
    End Function

    ''' <summary>
    ''' Función que devuelve la liquidación de Cesantia de un Contrato con la Fecha de Pago del Interés de Cesantía
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <param name="InterestPayDay">Fecha Pago Interés</param>
    ''' <returns>Objeto Cesantia</returns>
    ''' <remarks></remarks>
    Public Function GetUnemployedLiquidationByContractIdInterestPayDay(ByVal ContractId As String, InterestPayDay As Date) As UnemployedLiquidation Implements IUnemployedLiquidationRepository.GetUnemployedLiquidationByContractIdInterestPayDay
        Dim unemployedLiquidation = From e In _context.UnemployedLiquidation
                                    Where e.ContractId = ContractId And e.UnemployedInterestPayDate = InterestPayDay
        If unemployedLiquidation.Count > 0 Then
            Return unemployedLiquidation.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetUnemploymentLiquidationByContractId(ByVal ContractId As String) As List(Of UnemployedLiquidation) Implements IUnemployedLiquidationRepository.GetUnemploymentLiquidationByContractId
        Dim unemployedLiquidation = From e In _context.UnemployedLiquidation
                                    Where e.ContractId = ContractId

        Return unemployedLiquidation.ToList()
    End Function

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación por Clase de Concepto y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="conceptClass">Clase del Concepto</param>
    ''' <returns></returns>
    Public Function GetLiquidationDetailByContractIdConceptClass(ByVal Year As Integer, InitialContractNumber As Integer, conceptClass As String) As List(Of LiquidationDetail) Implements IUnemployedLiquidationRepository.GetLiquidationDetailByContractIdConceptClass
        Dim InitialDate As New Date(Year, 1, 1)
        Dim EndDate As New Date(Year, 12, 31)
        Dim LiquidationDetail = From e In _context.LiquidationDetail.Include("Liquidation").Include("Concept")
                                Where e.PayrollDate.Year = Year _
                                    And e.Liquidation.InitialContractNumber = InitialContractNumber _
                                    And e.Concept.ConceptClass = conceptClass And e.Liquidation.RegisterStatus <> "" And e.Liquidation.PayrollDateLiquidated >= InitialDate And e.Liquidation.PayrollDateLiquidated <= EndDate

        If LiquidationDetail.Count > 0 Then
            Return LiquidationDetail.ToList()
        Else
            Return New List(Of LiquidationDetail)
        End If

    End Function

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación si Afecta para Cesantias y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="AffectUnemployement">Afecta Cesantías</param>
    ''' <returns></returns>
    Public Function GetLiquidationDetailByContractIdConceptAffectUnemployement(ByVal Year As Integer, InitialContractNumber As Integer, AffectUnemployement As Boolean) As List(Of LiquidationDetail) Implements IUnemployedLiquidationRepository.GetLiquidationDetailByContractIdConceptAffectUnemployement
        Dim InitialDate As New Date(Year, 1, 1)
        Dim EndDate As New Date(Year, 12, 31)
        Dim LiquidationDetail = From e In _context.LiquidationDetail.Include("Liquidation").Include("Concept")
                                Where e.Liquidation.InitialContractNumber = InitialContractNumber _
                                And e.Concept.AffectIBCSeverance = AffectUnemployement And e.Liquidation.RegisterStatus <> "" _
                                And e.PayrollDate.Year = Year

        'And e.Liquidation.PayrollDateLiquidated >= InitialDate And e.Liquidation.PayrollDateLiquidated <= EndDate

        If LiquidationDetail.Count > 0 Then
            Return LiquidationDetail.ToList()
        Else
            Return New List(Of LiquidationDetail)
        End If
    End Function

    Public Function SP_GenerateJournalVouchersUnemployment(GroupId As Integer, PeriodInitialDate As Date, PeriodEndDate As Date, codeUser As String) As List(Of SP_GenerateJournalVouchersUnemployment_Result) Implements IUnemployedLiquidationRepository.SP_GenerateJournalVouchersUnemployment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVouchersUnemployment(GroupId, PeriodInitialDate, PeriodEndDate, codeUser).ToList()
    End Function

    Public Function GetunemploymentLiquidationEmployee(EmployeeId As Integer, PeriodEndDate As Date, Status As Boolean) As List(Of UnemployedLiquidation) Implements IUnemployedLiquidationRepository.GetunemploymentLiquidationEmployee
        Dim unemployedLiquidation = From e In _context.UnemployedLiquidation.Include("UnemployedLiquidationDetail").Include("UnemployedConcept").Include("Contract")
                                    Where e.EmployeeId = EmployeeId And e.Contract.Status = True And e.Contract.Valid = True And e.Year = PeriodEndDate.Year And e.Status = Status
        Return unemployedLiquidation.ToList()
    End Function

    Public Function GetUnemployedLiquidationByGroup(GroupId As Integer, PeriodEndDate As Date, StatusValue As Boolean) As List(Of UnemployedLiquidation) Implements IUnemployedLiquidationRepository.GetUnemployedLiquidationByGroup
        Dim unemployedLiquidation As IQueryable(Of UnemployedLiquidation)
        unemployedLiquidation = From e In _context.UnemployedLiquidation
                                Where e.GroupId = GroupId And
                                      e.Year = PeriodEndDate.Year And
                                      e.Status = StatusValue
        Return unemployedLiquidation.ToList()
    End Function
End Class
