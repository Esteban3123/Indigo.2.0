'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

Public Class DistributionManpowerRepository
    Inherits GenericRepository(Of DistributionManpower)
    Implements IDistributionManpowerRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una distribucion de mano de obra por codigo
    ''' </summary>
    Public Function GetDistributionManpower(code As String) As DistributionManpower Implements IDistributionManpowerRepository.GetDistributionManpower
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.DistributionManpower.Include("DistributionManpowerDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Dim thirdId = (From ge In _context.Employee Where ge.Id = query.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
            query.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            query.OriginalValue = (From d In _context.DistributionManpower.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionManpower()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id
    ''' </summary>
    Public Function GetDistributionManpowerById(id As Integer) As DistributionManpower Implements IDistributionManpowerRepository.GetDistributionManpowerById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.DistributionManpower Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionManpower.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New DistributionManpower()
        End If
    End Function

    ''' <summary>
    ''' Lista las distribuciones de mano de obra por año y mes
    ''' </summary>
    Public Function ListDistributionManpowerByYearMonth(year As Integer, month As Integer) As List(Of DistributionManpower) Implements IDistributionManpowerRepository.ListDistributionManpowerByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim ListDistribManpower = (From d In _context.DistributionManpower.Include("DistributionManpowerDetail") Where d.Year = year AndAlso d.Month = month Select d).ToList()
        For Each item As DistributionManpower In ListDistribManpower
            'Dim xx = (From e In _context.Employee.AsNoTracking() Join t In _context.ThirdParty.AsNoTracking() On e.ThirdPartyId Equals t.Id Join c In _context.Contract.AsNoTracking() On e.Id Equals c.)
            Dim thirdId = (From e In _context.Employee.AsNoTracking() Where e.Id = item.EmployeeId Select e.ThirdPartyId).FirstOrDefault()
            item.FullNameEmployee = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            'item.FullNameEmployeeGroup = (From g In _context.Contract.AsNoTracking() Where ite)
        Next
        Return ListDistribManpower
    End Function

    ''' <summary>
    ''' Obtiene una distribución de mano de obra por id del empleado
    ''' </summary>
    Public Function GetDistributionManpowerByEmployeeIdAndYearMonth(employeeId As Integer, year As Integer, month As Integer, Optional tracking As Boolean = True) As DistributionManpower Implements IDistributionManpowerRepository.GetDistributionManpowerByEmployeeIdAndYearMonth
        If employeeId = 0 Then
            Throw New ArgumentNullException("employeeId")
        End If
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim query As DistributionManpower = Nothing
        If tracking Then
            query = (From d In _context.DistributionManpower.Include("DistributionManpowerDetail") Where d.EmployeeId = employeeId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        Else
            query = (From d In _context.DistributionManpower.AsNoTracking().Include("DistributionManpowerDetail").AsNoTracking() Where d.EmployeeId = employeeId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.DistributionManpower.AsNoTracking() Where d.EmployeeId = employeeId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
            Dim thirdId = (From ge In _context.Employee Where ge.Id = query.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
            query.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            Return query
        Else
            Return New DistributionManpower()
        End If
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements IDistributionManpowerRepository.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim listData As List(Of DistributionManpower) = (From d In _context.DistributionManpower Where d.Year * 100 + d.Month <= year * 100 + month Select d).ToList()
        Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
    End Function

    ''' <summary>
    ''' Guarda masivamente la distribucion de mano de obra
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConfirmMasiveInteropCostDistributionManpower(XmlObject As String, Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_ConfirmMasiveInteropCostDistributionManpower_Result Implements IDistributionManpowerRepository.SP_ConfirmMasiveInteropCostDistributionManpower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmMasiveInteropCostDistributionManpower(XmlObject, Year, Month, OperatingUnitId, CodeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene los datos necesarios para poder exportar a excel
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ExportExcelInteropCostDistributionManPower(XmlObject As String, Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As List(Of SP_ExportExcelInteropCostDistributionManPower_Result) Implements IDistributionManpowerRepository.SP_ExportExcelInteropCostDistributionManPower
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ExportExcelInteropCostDistributionManPower(XmlObject, Year, Month, OperatingUnitId, CodeUser).ToList
    End Function


End Class