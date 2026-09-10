'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MPayrollLiquidation
    Implements IDisposable

    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Methods"

    ''' <summary>
    ''' Devuelve las liquidaciones de un grupo, en determinada fecha
    ''' </summary>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListLiquitadionByGroupAndDateLiquidatedAsync(PayrollDateLiquidated As Date, ByVal groupId As String) As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListLiquitadionByGroupAndDateLiquidatedAsync(PayrollDateLiquidated, groupId, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para liquidar
    ''' </summary>
    ''' <param name="ListPayrollLiquidation">Lista de Liquidaciones</param>
    ''' <returns>Boolean</returns>
    Public Async Function SaveLiquidationAsync(ByVal ListPayrollLiquidation As List(Of Liquidation)) As Task(Of ActionMessageResult(Of List(Of Liquidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveLiquidationAsync(ListPayrollLiquidation, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Almacenar Liquidaciones 
    ''' </summary>
    ''' <param name="ListPayrollLiquidation">Lista de Liquidaciones</param>
    ''' <returns>Boolean</returns>
    Public Async Function SaveLiquidationOpenBalancesAsync(ByVal ListPayrollLiquidation As List(Of Liquidation)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveLiquidationOpenBalancesAsync(ListPayrollLiquidation, Indigo)
    End Function


    Public Async Function GetEmployeeLiquidatedAsync(ByVal datePayrollLiquitad As Date, ByVal groupId As String) As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListEmployeeLiquitadedAsync(datePayrollLiquitad, groupId, Indigo)
    End Function

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLiquidationMinDateAsync() As Task(Of Date)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationMinDateAsync(Indigo)
    End Function

    ''' <summary>
    ''' Gets the patronales value asynchronous.
    ''' </summary>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <param name="year">The year.</param>
    ''' <param name="month">The month.</param>
    ''' <returns></returns>
    Public Async Function GetPatronalesValueAsync(employeeId As Integer, year As Integer, month As Integer) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPatronalesValueAsync(employeeId, year, month, Indigo)
    End Function

    ''' <summary>
    ''' Gets the liquidation by identifier.
    ''' </summary>
    Public Async Function GetLiquidationById(id As Integer) As Task(Of Liquidation)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Gets the liquidation by employee identifier year month.
    ''' </summary>
    Public Async Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer) As Task(Of Liquidation)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationByEmployeeIdYearMonthAsync(employeeId, year, month, Indigo)
    End Function

    ''' <summary>
    ''' Devuelve la fecha mayor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLiquidationMaxDateAsync() As Task(Of Date)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationMaxDateAsync(Indigo)
    End Function

    ''' <summary>
    ''' Funcion para liquidar
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    Public Async Function CalculatePayrollLiquidationAsync(ByVal groupId As String, ByVal employeeId As String) As Task(Of ActionMessageResult(Of List(Of Liquidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculatePayrollLiquidationAsync(Indigo, groupId, employeeId)
    End Function

    ''' <summary>
    ''' Función para traer todas las liquidaciones de determinado grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLiquidationByGrupoIdConsultLiquidationAsync(ByVal groupId As String) As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationByGrupoIdConsultLiquidationAsync(groupId, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLiquidationDatesByGroup(groupId As Integer) As Task(Of List(Of Date))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationDatesByGroupAsync(groupId, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <param name="StrIndGroup">string Id Groups</param>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    Public Async Function GetOnlyLiquidationByGrupoIdDateLiquidation() As Task(Of Dictionary(Of String, Date))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetOnlyLiquidationByGrupoIdDateLiquidationAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista de Liquidaciones Sin Confirmar
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación </param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Async Function PayrollNotCheckLiquidatedToDelete(ByVal datePayrollLiquitad As Date, ByVal groupId As String) As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.PayrollNotCheckLiquidatedToDeleteAsync(datePayrollLiquitad, groupId, Indigo)
    End Function


    ''' <summary>
    ''' Devuelve lista de Fechas de Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLiquidationDatesConfirmPayroll() As Task(Of List(Of Date))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationDatesConfirmPayrollAsync(Indigo)
    End Function


    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLiquidationByGroupIdConsultLiquidation(groupId As Integer) As Task(Of Dynamic.ExpandoObject)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationByGroupIdConsultLiquidationAsync(groupId, Indigo)

        If res IsNot Nothing AndAlso Not res.Trim().Equals(String.Empty) Then
            Return Utils.DeserializeJsonToObject(res)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Devuelve lista de Fechas de Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetHeadLiquidation() As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetHeadLiquidationAsync(Indigo)
    End Function

    ''' <summary>
    ''' Devuelve lista de Fechas de Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDetailMessageLiquidation(IdGroups As Integer) As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetDetailMessageLiquidationAsync(IdGroups, Indigo)
    End Function

    Public Async Function CountEmployeePayrollAsync(IdGroups As Integer) As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CountEmployeePayrollAsync(IdGroups, Indigo)
    End Function

    Public Async Function GetEmployesPayrollLiquidation(IdGroup As Integer, NitEmployee As String) As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployesPayrollLiquidationAsync(IdGroup, NitEmployee, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para liquidar
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    Public Async Function CalculatePayrollLiquidationFragmentedAsync(ByVal ListEmployee As List(Of Employee), ByVal groupId As String, Optional ByVal DeleteItem As Integer = 0) As Task(Of ActionMessageResult(Of List(Of Liquidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculatePayrollLiquidationFragmentAsync(ListEmployee, groupId, Indigo, DeleteItem)
    End Function


    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación con conceptos dinámicos
    ''' </summary>
    ''' <param name="dateInitial">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="groupInitial">Id del grupo inicial para filtrar (opcional)</param>
    ''' <param name="groupFinal">Id del grupo final para filtrar (opcional)</param>
    ''' <param name="branchOfficeInitial">Id de la sucursal inicial para filtrar (opcional)</param>
    ''' <param name="branchOfficeFinal">Id de la sucursal final para filtrar (opcional)</param>
    ''' <param name="registerStatus">Estado de registro de liquidación (opcional)</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    Public Async Function GetLiquidationDetailReportAsync(dateInitial As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing) As Task(Of System.Data.DataTable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationDetailReportAsync(
        dateInitial,
        endDate,
        employeeId,
        groupInitial,
        groupFinal,
        branchOfficeInitial,
        branchOfficeFinal,
        registerStatus,
        Indigo)
    End Function


#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
