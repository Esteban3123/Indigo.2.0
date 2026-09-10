'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Threading.Tasks

<ServiceContract()> _
Public Interface IPayrollIncreaseSalary

    ''' <summary>
    ''' Función que ejecuta el Aumento de Sueldo
    ''' </summary>
    ''' <param name="IdGroup">Id del Grupo</param>
    ''' <param name="IncreasePercentage">Porcentaje de Incremento</param>
    ''' <param name="FunctionalId">Id unidad Funcional</param>
    ''' <param name="PositionId">Id Cargo</param>
    ''' <param name="WithRetroactive">Indica si tiene retroactivo</param>
    ''' <param name="RetroactiveInitialDate">Fecha inicial del retroactivo</param>
    ''' <param name="PayrollPaidRetroactive">Nómina pagada de retroactivo</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ExecuteIncreaseSalary(IdGroup As Integer, IdFunctionalUnit As Integer, IdPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Integer, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, Session As SessionValues, Optional WithRetroactive As Integer = 0, Optional RetroactiveInitialDate As String = Nothing, Optional PayrollPaidRetroactive As Integer = 0) As List(Of SP_IncreaseEmployeSalary_Result)

    ''' <summary>
    ''' Función que ejecuta la Confirmación del Aumento de Salario
    ''' </summary>
    ''' <param name="IncreaseSalaryData"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmIncreaseEmployeSalary(IncreaseSalaryData As List(Of SP_IncreaseEmployeSalary_Result), UserCode As String, PercentageIncrease As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date, Session As SessionValues) As SP_ConfirmIncreaseEmployeSalary_Result

    ''' <summary>
    ''' Función que ejecuta aumento de salarios por Excel
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetIncreaseSalaryFromFile(DataImport As List(Of ImportFileRow), data As List(Of List(Of String)), Session As SessionValues) As List(Of SP_SetIncreaseSalaryFromFile_Result)

    ''' <summary>
    ''' Función que se ejecuta para Almacenar el nuevo Salario
    ''' </summary>
    ''' <param name="ObjListContract">Objeto Lista de Contratos</param>
    ''' <param name="ContractInitialDate">Fecha Inicio del Nuevo Contrato (OTRO SI)</param>
    ''' <param name="ContractModificationReasonId">Id Razón de Modificación de Contrato</param>
    ''' <param name="session">Variable de Sesión</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveIncreaseSalary(ObjListContract As String, ContractInitialDate As Date, ContractModificationReasonId As Integer, session As SessionValues, Optional ListRetroactiveC As List(Of RetroactiveC) = Nothing) As Boolean

    ''' <summary>
    ''' Función que se utiliza para ejecutar el proceso de Retroactividad
    ''' </summary>
    ''' <param name="IdGroup">Id del Grupo</param>
    ''' <param name="IncreasePercentage">Porcentaje de Incremento</param>
    ''' <param name="RetroactiveInitialDate">Fecha Inicio del Retroactivo</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <param name="FunctionalId">Id Unidad Funcional</param>
    ''' <param name="PositionId">Id del Cargo</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ExecuteRetroactive(IdGroup As Integer, IncreasePercentage As Decimal, RetroactiveInitialDate As Date, PayrollPaid As Byte, Indigo As SessionValues, Optional FunctionalId As Integer = 0, Optional PositionId As Integer = 0, Optional SpecificEmployeeId As Integer = 0) As List(Of RetroactiveC)

End Interface
