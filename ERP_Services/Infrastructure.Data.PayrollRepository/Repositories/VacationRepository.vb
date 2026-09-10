'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 09-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class VacationRepository
    Inherits GenericRepository(Of Vacation)
    Implements IVacationRepository

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
    ''' Funcion la cual obtiene las vacaciones de una fecha de liquidacion especifica
    ''' </summary>
    ''' <param name="dateLiquidation">Fecha de liquidacion de nomina</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationLiquidationDate(dateLiquidation As Date, state As Byte) As List(Of Vacation) Implements IVacationRepository.GetVacationLiquidationDate
        Dim query = From e In _context.Vacation.Include("VacationPeriod")
                    Where e.LiquidationDate = dateLiquidation And state = state
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Funcion para listar todas las vacaciones de tengan cierto estado de incorporacion
    ''' </summary>
    ''' <param name="stateIncorporation">Estado de incorporacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationStateIncorporation(stateIncorporation As Byte) As List(Of Vacation) Implements IVacationRepository.GetVacationStateIncorporation
        Dim query = From e In _context.Vacation
                    Where e.StateIncorporation = stateIncorporation
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationBetweenDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Vacation) Implements IVacationRepository.GetVacationBetweenDate
        Dim query = From e In _context.Vacation.Include("VacationPeriod")
                    Where e.VacationPeriod.EmployeeId = employeeId And
                    ((initialDate >= e.VacationStartDate And endDate <= e.IncorporationDateReal) Or
                    (initialDate >= e.VacationStartDate And initialDate <= e.IncorporationDateReal) Or
                    (endDate >= e.VacationStartDate And endDate <= e.IncorporationDateReal) Or
                    (initialDate <= e.VacationStartDate And endDate >= e.IncorporationDateReal)
                    )
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas y contratos no liquidados
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function GetVacationBetweenDateAndActiveContract(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Vacation) Implements IVacationRepository.GetVacationBetweenDateAndActiveContract
        Dim query = From e In _context.Vacation.Include("VacationPeriod").Include("VacationPeriod.Contract")
                    Where e.VacationPeriod.EmployeeId = employeeId And e.VacationPeriod.Contract.Status <> 2 And
                    ((initialDate >= e.VacationStartDate And endDate <= e.IncorporationDateReal) Or
                    (initialDate >= e.VacationStartDate And initialDate <= e.IncorporationDateReal) Or
                    (endDate >= e.VacationStartDate And endDate <= e.IncorporationDateReal) Or
                    (initialDate <= e.VacationStartDate And endDate >= e.IncorporationDateReal)
                    )
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de conceptos de vacaciones por clase
    ''' que ocurrieron dentro del rango de fechas especificado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="conceptClass">Clase del Concepto (Ej: 049 para Prima de Vacaciones)</param>
    ''' <param name="dateInitial">Fecha Inicial del período</param>
    ''' <param name="dateEnd">Fecha Final del período</param>
    ''' <returns>Lista de VacationDetail con la clase de concepto especificada</returns>
    ''' <remarks></remarks>
    Public Function GetVacationIncentiveByDateRange(employeeId As Integer, conceptClass As String, dateInitial As Date, dateEnd As Date) As List(Of VacationDetail) Implements IVacationRepository.GetVacationIncentiveByDateRange
        Dim query = From vd In _context.VacationDetail.Include("Vacation").Include("Concept")
                    Join v In _context.Vacation On vd.IdVacation Equals v.Id
                    Join vp In _context.VacationPeriod On v.VacationPeriodId Equals vp.Id
                    Join c In _context.Concept On vd.IdConcept Equals c.Id
                    Where vp.EmployeeId = employeeId _
                    AndAlso c.ConceptClass = conceptClass _
                    AndAlso v.State = 2 _
                    AndAlso (v.VacationStartDate >= dateInitial AndAlso v.VacationStartDate <= dateEnd)
                    Order By v.VacationStartDate Descending
                    Select vd

        If query.Count > 0 Then
            Return query.ToList()
        Else
            Return New List(Of VacationDetail)()
        End If
    End Function

End Class
