'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 27-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IUnemployedLiquidationAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Metodo que liquida las cesantías anuales
    ''' </summary>
    ''' <param name="EmployeeToLiquidate">Empleado a liquidar, si la liq es parcial</param>
    ''' <param name="ListGroups">Listado de grupos a liquidar</param>
    ''' <param name="InitialDate">Fecha inicio de cesantías</param>
    ''' <param name="EndingDate">Fecha Fin de cesantías</param>
    ''' <param name="Confirm">Si confirma la liquidación</param>
    ''' <param name="Sanction">Si se tienen en cuenta los días de sanción</param>
    ''' <param name="audit">Variable de auditoria</param>
    ''' <returns>el ActionMessageResult</returns>
    ''' <remarks></remarks>
    Function UnemployedLiquidation(EmployeeToLiquidate As Employee, ListGroups As List(Of Group), InitialDate As Date, EndingDate As Date, Confirm As Boolean, Sanction As Boolean, SessionValues As SessionValues, Optional AuthorizationDate As Date = Nothing, Optional UnemployedRetirementReazon As String = "", Optional ResolutionNumber As String = "", Optional ByVal UnemployedInterestPaidWithPayroll As Boolean = 0) As ActionMessageResult(Of List(Of UnemployedLiquidation))

    ''' <summary>
    ''' Metodo que consulta las liquidaciones de grupos o empleados
    ''' </summary>
    ''' <param name="EmployeeToConsult">empleado a consultar</param>
    ''' <param name="ListGroups">listado de grupos a consultar</param>
    ''' <param name="Period">periodo</param>
    ''' <returns>los registros de liquidación de cesantías</returns>
    ''' <remarks></remarks>
    Function ConsultUnemployedLiquidation(EmployeeToConsult As Employee, ListGroups As List(Of Group), Optional Period As Integer = 0) As List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación si Afecta para Cesantias y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="AffectUnemployement">Afecta Cesantías</param>
    ''' <returns> List(Of LiquidationDetail)</returns>
    Function GetLiquidationDetailByContractIdConceptAffectUnemployement(ByVal Year As Integer, InitialContractNumber As Integer, AffectUnemployement As Boolean) As List(Of LiquidationDetail)

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación por Clase de Concepto y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="conceptClass">Clase del Concepto</param>
    ''' <returns>List(Of LiquidationDetail)</returns>
    Function GetLiquidationDetailByContractIdConceptClass(ByVal Year As Integer, InitialContractNumber As Integer, conceptClass As String) As List(Of LiquidationDetail)

    Function ConfirmUnemployedLiquidation(ListUnemploymentLiquidation As List(Of UnemployedLiquidation), SessionValues As SessionValues) As ActionResult(Of List(Of UnemployedLiquidation))

End Interface
