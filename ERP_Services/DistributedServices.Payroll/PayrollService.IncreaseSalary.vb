'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Threading.Tasks

Partial Class PayrollService

    ''' <summary>
    ''' Función que ejecuta el aumento de salario
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
    ''' <param name="Session"></param>
    ''' <param name="WithRetroactive"></param>
    ''' <param name="RetroactiveInitialDate"></param>
    ''' <param name="PayrollPaidRetroactive"></param>
    ''' <returns></returns>
    Public Function ExecuteIncreaseSalary(IdGroup As Integer, IdFunctionalUnit As Integer, IdPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Integer, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, Session As SessionValues, Optional WithRetroactive As Integer = 0, Optional RetroactiveInitialDate As String = Nothing, Optional PayrollPaidRetroactive As Integer = 0) As List(Of SP_IncreaseEmployeSalary_Result) Implements IPayrollIncreaseSalary.ExecuteIncreaseSalary
        Using IncreaseSalaryAdmin As IIncreaseSalaryAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IIncreaseSalaryAdminService)()
            Return IncreaseSalaryAdmin.ExecuteIncreaseSalary(IdGroup, IdFunctionalUnit, IdPosition, PercentageIncrease, Confirm, AproxValue, ModificationReasonId, InitialDateNewSalary, PayrollPaid, Session, WithRetroactive, RetroactiveInitialDate, PayrollPaidRetroactive)
        End Using
    End Function

    ''' <summary>
    ''' Función que ejecuta la Confirmación del Aumento de Salario
    ''' </summary>
    ''' <param name="IncreaseSalaryData"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function ConfirmIncreaseEmployeSalary(IncreaseSalaryData As List(Of SP_IncreaseEmployeSalary_Result), UserCode As String, PercentageIncrease As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date, Session As SessionValues) As SP_ConfirmIncreaseEmployeSalary_Result Implements IPayrollIncreaseSalary.ConfirmIncreaseEmployeSalary
        Using IncreaseSalaryAdmin As IIncreaseSalaryAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IIncreaseSalaryAdminService)()
            Return IncreaseSalaryAdmin.ConfirmIncreaseEmployeSalary(IncreaseSalaryData, UserCode, PercentageIncrease, ModificationReasonId, InitialDateNewSalary)
        End Using
    End Function

    ''' <summary>
    ''' Función que Aumenta Salario desde archivo Excel
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <param name="data"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function SetIncreaseSalaryFromFile(DataImport As List(Of ImportFileRow), data As List(Of List(Of String)), Session As SessionValues) As List(Of SP_SetIncreaseSalaryFromFile_Result) Implements IPayrollIncreaseSalary.SetIncreaseSalaryFromFile
        Using IncreaseSalaryAdmin As IIncreaseSalaryAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IIncreaseSalaryAdminService)()
            Return IncreaseSalaryAdmin.SetIncreaseSalaryFromFile(DataImport, data)
        End Using
    End Function
    ''' <summary>
    ''' Función que se ejecuta para Almacenar el nuevo Salario
    ''' </summary>
    ''' <param name="ObjListContract">Objeto Lista de Contratos</param>
    ''' <param name="ContractInitialDate">Fecha Inicio del Nuevo Contrato (OTRO SI)</param>
    ''' <param name="ContractModificationReasonId">Id Razón de Modificación de Contrato</param>
    ''' <param name="session">Variable de Sesión</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveIncreaseSalary(ObjListContract As String, ContractInitialDate As Date, ContractModificationReasonId As Integer, session As SessionValues, Optional ListRetroactiveC As List(Of RetroactiveC) = Nothing) As Boolean Implements IPayrollIncreaseSalary.SaveIncreaseSalary
        Using IncreaseSalaryAdmin As IIncreaseSalaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncreaseSalaryAdminService)()
            Return IncreaseSalaryAdmin.SaveIncreaseSalary(ObjListContract, ContractInitialDate, ContractModificationReasonId, session, ListRetroactiveC)
        End Using
    End Function

    ''' <summary>
    ''' Función que se ejecuta para Calcular el Retroactivo
    ''' </summary>
    ''' <param name="IdGroup">Id del Grupo</param>
    ''' <param name="IncreasePercentage">Porcentaje de Incremento</param>
    ''' <param name="RetroactiveInitialDate">Fecha Inicio del Retroactivo</param>
    ''' <param name="Indigo">Variable de Sesión</param>
    ''' <param name="FunctionalId">Id Unidad Funcional</param>
    ''' <param name="PositionId">Id del Cargo</param>
    ''' <returns>List(Of RetroactiveC)</returns>
    ''' <remarks></remarks>
    Public Function ExecuteRetroactive(IdGroup As Integer, IncreasePercentage As Decimal, RetroactiveInitialDate As Date, PayrollPaid As Byte, Indigo As SessionValues, Optional FunctionalId As Integer = 0, Optional PositionId As Integer = 0, Optional SpecificEmployeeId As Integer = 0) As List(Of RetroactiveC) Implements IPayrollIncreaseSalary.ExecuteRetroactive
        Using IncreaseSalaryAdmin As IIncreaseSalaryAdminService = IocFactory.Instance(Indigo.TransactionalContainer).CurrentContainer.Resolve(Of IIncreaseSalaryAdminService)()
            Return IncreaseSalaryAdmin.ExecuteRetroactive(IdGroup, IncreasePercentage, RetroactiveInitialDate, PayrollPaid, Indigo, FunctionalId, PositionId, SpecificEmployeeId)
        End Using
    End Function

End Class
