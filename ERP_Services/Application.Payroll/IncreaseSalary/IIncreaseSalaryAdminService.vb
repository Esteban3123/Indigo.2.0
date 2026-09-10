'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text

Public Interface IIncreaseSalaryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que ejecuta el Aumento de Sueldo
    ''' </summary>
    ''' <param name="IdGroup"></param>
    ''' <param name="IdFunctionalUnit"></param>
    ''' <param name="IdPosition"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="Confirm"></param>
    ''' <param name="AproxValue"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <param name="PayrollPaid"></param>
    ''' <param name="SessionValues"></param>
    ''' <param name="WithRetroactive"></param>
    ''' <param name="RetroactiveInitialDate"></param>
    ''' <param name="PayrollPaidRetroactive"></param>
    ''' <returns></returns>
    Function ExecuteIncreaseSalary(IdGroup As Integer, IdFunctionalUnit As Integer, IdPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Integer, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, SessionValues As SessionValues, Optional WithRetroactive As Integer = 0, Optional RetroactiveInitialDate As String = Nothing, Optional PayrollPaidRetroactive As Integer = 0) As List(Of SP_IncreaseEmployeSalary_Result)

    ''' <summary>
    ''' Función para Almacenar el Aumento de sueldo
    ''' </summary>
    ''' <param name="ObjListContract">Objeto de Lista de Contratos</param>
    ''' <param name="ContractInitialDate">Fecha de Inicio del Nuevo Contrato (OTRO SI)</param>
    ''' <param name="ContractModificationReasonId">I de la Razón de Modificación del Contrato</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveIncreaseSalary(ObjListContract As String, ContractInitialDate As Date, ContractModificationReasonId As Integer, Indigo As SessionValues, Optional ListRetroactiveC As List(Of RetroactiveC) = Nothing) As Boolean

    ''' <summary>
    ''' Función para Crear el Clón de Contrato
    ''' </summary>
    ''' <param name="Contract">Contrato</param>
    ''' <param name="ContractInitialDate">Fecha Inicio del Contrato</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <returns>Contract</returns>
    ''' <remarks></remarks>
    Function CloneContract(Contract As Contract, ContractInitialDate As Date, NewSalary As Double, Indigo As SessionValues) As Contract

    ''' <summary>
    ''' Función que ejecuta el Proceso de Retroactividad
    ''' </summary>
    ''' <param name="IdGroup">Id del Grupo</param>
    ''' <param name="IncreasePercentage">Porcentaje de Incremento</param>
    ''' <param name="FunctionalId">Id unidad Funcional</param>
    ''' <param name="PositionId">Id Cargo</param>
    ''' <param name="SpecificEmployeeId">Id del empleado específico para optimizar consultas (para importación desde archivo)</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    Function ExecuteRetroactive(IdGroup As Integer, IncreasePercentage As Decimal, RetroactiveInitialDate As Date, PayrollPaid As Byte, Indigo As SessionValues, Optional FunctionalId As Integer = 0, Optional PositionId As Integer = 0, Optional SpecificEmployeeId As Integer = 0) As List(Of RetroactiveC)

    ''' <summary>
    ''' Función que ejecuta el proceso de confirmación Aumento de Salario
    ''' </summary>
    ''' <param name="IncreaseSalaryData"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <returns></returns>
    Function ConfirmIncreaseEmployeSalary(IncreaseSalaryData As List(Of SP_IncreaseEmployeSalary_Result), UserCode As String, PercentageIncrease As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date) As SP_ConfirmIncreaseEmployeSalary_Result

    ''' <summary>
    ''' Función que toma valores para el aumento de salario desde archivo Excel
    ''' </summary>
    ''' <param name="dataimport"></param>
    ''' <param name="data"></param>
    ''' <param name="Indigo">Sesión del usuario</param>
    ''' <returns></returns>
    Function SetIncreaseSalaryFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As List(Of SP_SetIncreaseSalaryFromFile_Result)
End Interface
