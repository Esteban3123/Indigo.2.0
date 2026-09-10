'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo 
' Created          : 17-10-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IIncreaseSalaryRepository
    ''' <summary>
    ''' Ejecuta el procedimiento Almacenado de Increento Salarial
    ''' </summary>
    ''' <param name="IdGroup"></param>
    ''' <param name="IdFunctionalUnit"></param>
    ''' <param name="IdPosition"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="Confirm"></param>
    ''' <param name="AproxValue"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <param name="SessionValues"></param>
    ''' <param name="WithRetroactive"></param>
    ''' <param name="RetroactiveInitialDate"></param>
    ''' <param name="PayrollPaidRetroactive"></param>
    ''' <returns></returns>
    Function ExecuteIncreaseSalary(IdGroup As Integer, IdFunctionalUnit As Integer, IdPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Integer, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, SessionValues As SessionValues, Optional WithRetroactive As Integer = 0, Optional RetroactiveInitialDate As String = Nothing, Optional PayrollPaidRetroactive As Integer = 0) As List(Of SP_IncreaseEmployeSalary_Result)

    ''' <summary>
    ''' Ejecuta el Procedimiento Almacenado para Confirmar el Aumento del Salario
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <returns></returns>
    Function ConfirmIncreaseEmployeSalary(xmlObject As String, UserCode As String, PercentageIncrease As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date) As SP_ConfirmIncreaseEmployeSalary_Result

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado para Setear Aumento de Salario por Excel
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <returns></returns>
    Function SetIncreaseSalaryFromFile(xmlObject As String) As List(Of SP_SetIncreaseSalaryFromFile_Result)

End Interface
