'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 27-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Metodo que liquida las cesantías anuales
    ''' </summary>
    ''' <param name="EmployeeToLiquidate">Empleado a liquidar, si la liq es parcial</param>
    ''' <param name="ListGroups">Listado de grupos a liquidar</param>
    ''' <param name="InitialDate">Fecha inicio de liq cesantías</param>
    ''' <param name="EndingDate">Fecha Fin de liq de cesantías</param>
    ''' <param name="Confirm">Si confirma la liquidación</param>
    ''' <param name="Sanction">Si se tienen en cuenta los días de sanción</param>
    ''' <param name="session">Variable de session</param>
    ''' <returns>el ActionMessageResult</returns>
    ''' <remarks></remarks>
    Public Function UnemployedLiquidation(EmployeeToLiquidate As Employee, ListGroups As List(Of Group), InitialDate As Date, EndingDate As Date, Confirm As Boolean, Sanction As Boolean, session As SessionValues, Optional AuthorizationDate As Date = Nothing, Optional UnemployedRetirementReazon As String = "", Optional ResolutionNumber As String = "", Optional ByVal UnemployedInterestPaidWithPayroll As Boolean = 0) As ActionMessageResult(Of List(Of UnemployedLiquidation)) Implements IPayrollUnemployedLiquidation.UnemployedLiquidation
        Using unemployedLiquidationAdmin As IUnemployedLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IUnemployedLiquidationAdminService)()
            Return unemployedLiquidationAdmin.UnemployedLiquidation(EmployeeToLiquidate, ListGroups, InitialDate, EndingDate, Confirm, Sanction, session, AuthorizationDate, UnemployedRetirementReazon, ResolutionNumber, UnemployedInterestPaidWithPayroll)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta las liquidaciones de grupos o empleados
    ''' </summary>
    ''' <param name="EmployeeToConsult">empleado a consultar</param>
    ''' <param name="ListGroups">listado de grupos a consultar</param>
    ''' <param name="Period">periodo</param>
    ''' <returns>los registros de liquidación de cesantías</returns>
    ''' <remarks></remarks>
    Public Function ConsultUnemployedLiquidation(EmployeeToConsult As Employee, ListGroups As List(Of Group), session As SessionValues, Optional Period As Integer = 0) As List(Of UnemployedLiquidation) Implements IPayrollUnemployedLiquidation.ConsultUnemployedLiquidation
        Using unemployedLiquidationAdmin As IUnemployedLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IUnemployedLiquidationAdminService)()
            Return unemployedLiquidationAdmin.ConsultUnemployedLiquidation(EmployeeToConsult, ListGroups, Period)
        End Using
    End Function

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación por Clase de Concepto y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="conceptClass">Clase del Concepto</param>
    ''' <returns>List(Of LiquidationDetail)</returns>
    Public Function GetLiquidationDetailByContractIdConceptClass(ByVal Year As Integer, InitialContractNumber As Integer, conceptClass As String, session As SessionValues) As List(Of LiquidationDetail) Implements IPayrollUnemployedLiquidation.GetLiquidationDetailByContractIdConceptClass
        Using unemployedLiquidationAdmin As IUnemployedLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IUnemployedLiquidationAdminService)()
            Return unemployedLiquidationAdmin.GetLiquidationDetailByContractIdConceptClass(Year, InitialContractNumber, conceptClass)
        End Using
    End Function

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación si Afecta para Cesantias y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="AffectUnemployement">Afecta Cesantías</param>
    ''' <returns></returns>
    Public Function GetLiquidationDetailByContractIdConceptAffectUnemployement(ByVal Year As Integer, InitialContractNumber As Integer, AffectUnemployement As Boolean, session As SessionValues) As List(Of LiquidationDetail) Implements IPayrollUnemployedLiquidation.GetLiquidationDetailByContractIdConceptAffectUnemployement
        Using unemployedLiquidationAdmin As IUnemployedLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IUnemployedLiquidationAdminService)()
            Return unemployedLiquidationAdmin.GetLiquidationDetailByContractIdConceptAffectUnemployement(Year, InitialContractNumber, AffectUnemployement)
        End Using
    End Function

    Public Function ConfirmUnemployment(ListUnemploymentLiquidation As List(Of UnemployedLiquidation), session As SessionValues) As ActionResult(Of List(Of UnemployedLiquidation)) Implements IPayrollUnemployedLiquidation.ConfirmUnemployment
        Using unemployedLiquidationAdmin As IUnemployedLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IUnemployedLiquidationAdminService)()
            Return unemployedLiquidationAdmin.ConfirmUnemployedLiquidation(ListUnemploymentLiquidation, session)
        End Using
    End Function
End Class
