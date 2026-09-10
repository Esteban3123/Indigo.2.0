'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Common.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

#End Region
Public Interface IUnemployedLiquidationDomain
    Inherits IDisposable

    ''' <summary>
    ''' Metodo que contiene toda la lógica de la liquidación de cesantías anuales
    ''' </summary>
    ''' <param name="listEmployees">listado de empleados a liquidar</param>
    ''' <param name="InitialDate">Fecha inicio de cesantías</param>
    ''' <param name="EndingDate">Fecha Fin de cesantías</param>
    ''' <param name="confirm">si va confirmada o no la liquidación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function YearlyLiquidation(listEmployees As List(Of Employee), InitialDate As Date, EndingDate As Date, confirm As Boolean, LiquidationType As Boolean, Sanction As Boolean, Optional AuthorizationDate As Date = Nothing, Optional UnemployedRetirementReason As String = "", Optional ResolutionNumber As String = "", Optional XtraLiquidation As Liquidation = Nothing, Optional liquidationContract As Boolean = False, Optional ByRef ReplaceFormulate As String = "", Optional ByRef ConceptFormulate As String = "", Optional BasePrimasCesantias As Decimal = 0, Optional ByVal UnemployedInterestPaidWithPayroll As Boolean = 0) As ActionMessageResult(Of List(Of UnemployedLiquidation))

    ''' <summary>
    ''' Funcion para calcular la liquidacion entre un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>detallados mes a mes de liquidación</returns>
    ''' <remarks></remarks>
    Function CalculateMonthsWorked(employeeId As Integer, initialDate As Date, endDate As Date, Optional InitialContractNumber As Integer = 0) As TrackableCollection(Of UnemployedLiquidationDetail)

    ''' <summary>
    ''' Obtiene las liquidaciones de cesantías de un empleado en determinado lapso de tiempo
    ''' </summary>
    ''' <param name="employeeId">empleado id</param>
    ''' <param name="initialDate">fecha inicio</param>
    ''' <param name="endDate">fecha fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUnemployedLiquidationEmployee(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Metodo para eliminar las liquidaciones sin confirmar cuando se liquidan de nuevo
    ''' </summary>
    ''' <param name="listUnemployedLiquidation">listado de liquidacion de cesantías a eliminar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteListUnemployedLiquidationsWithoutConfirm(listUnemployedLiquidation As List(Of UnemployedLiquidation)) As Boolean

    ''' <summary>
    ''' Metodo para Añadir registero de liquidacion en el listado final a retornar
    ''' </summary>
    ''' <param name="listUnemployedLiquidationResult">listado final a retornar</param>
    ''' <param name="messageResult">mensaje si lo hay</param>
    ''' <param name="_employee">entidad empleado</param>
    ''' <param name="newUneLiqTrue">entidad de registro de cesantía nuevo si lo hay</param>
    ''' <remarks></remarks>
    Sub ProcessItemToAdd(ByRef listUnemployedLiquidationResult As List(Of ActionMessageResult(Of UnemployedLiquidation)), messageResult As MessageResult, _employee As Employee, Optional newUneLiqTrue As UnemployedLiquidation = Nothing)
End Interface
