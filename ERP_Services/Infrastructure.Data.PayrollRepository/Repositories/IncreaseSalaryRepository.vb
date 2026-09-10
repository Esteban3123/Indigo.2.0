'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-10-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports System.Threading.Tasks

Public Class IncreaseSalaryRepository

    Inherits GenericRepository(Of Contract)
    Implements IIncreaseSalaryRepository

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

    Public Function ExecuteIncreaseSalary(IdGroup As Integer, IdFunctionalUnit As Integer, IdPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Integer, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, SessionValues As SessionValues, Optional WithRetroactive As Integer = 0, Optional RetroactiveInitialDate As String = Nothing, Optional PayrollPaidRetroactive As Integer = 0) As List(Of SP_IncreaseEmployeSalary_Result) Implements IIncreaseSalaryRepository.ExecuteIncreaseSalary
        Return _context.SP_IncreaseEmployeSalary(IdGroup, IdFunctionalUnit, IdPosition, PercentageIncrease, Confirm, AproxValue, ModificationReasonId, SessionValues.AuditMessageWcf.CodeUser, InitialDateNewSalary, WithRetroactive, RetroactiveInitialDate, PayrollPaidRetroactive).ToList()
    End Function

    ''' <summary>
    ''' Función para confirmar el aumento de salario
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <returns></returns>
    Public Function ConfirmIncreaseEmployeSalary(xmlObject As String, UserCode As String, PercentageIncrease As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date) As SP_ConfirmIncreaseEmployeSalary_Result Implements IIncreaseSalaryRepository.ConfirmIncreaseEmployeSalary
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim res = _context.SP_ConfirmIncreaseEmployeSalary(xmlObject, UserCode, PercentageIncrease, ModificationReasonId, InitialDateNewSalary).FirstOrDefault()
        Return res
    End Function

    ''' <summary>
    ''' Función para setear valores del aumento de salario desde Excel
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <returns></returns>
    Private Function SetIncreaseSalaryFromFile(xmlObject As String) As List(Of SP_SetIncreaseSalaryFromFile_Result) Implements IIncreaseSalaryRepository.SetIncreaseSalaryFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim res = _context.SP_SetIncreaseSalaryFromFile(xmlObject).ToList()
        Return res
    End Function
End Class
