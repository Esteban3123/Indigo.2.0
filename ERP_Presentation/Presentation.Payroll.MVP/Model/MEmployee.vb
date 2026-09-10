'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 09-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Realiza la conexion con los servicios de talento humano
''' </summary>
Public Class MEmployee
    Inherits ModelBase
    Implements IDisposable

    Public Const TAG As String = "529"

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene el talento humano de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo</param>
    ''' <returns>Talento humano</returns>
    Public Async Function GetEmployeeAsync(ByVal code As String) As Task(Of Employee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el talento humano de acuerdo al codigo 
    ''' </summary>
    ''' <param name="id">Id</param>
    ''' <returns>Talento humano</returns>
    Public Async Function GetEmployeeByIdAsync(ByVal id As Integer) As Task(Of Employee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el talento humano de acuerdo al codigo 
    ''' </summary>
    ''' <param name="id">Id</param>
    ''' <returns>Talento humano</returns>
    Public Async Function GetEmployeeByIdForContractLiquidation(ByVal id As Integer) As Task(Of Employee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeByIdForContractLiquidationAsync(id, Indigo)
    End Function



    ''' <summary>
    ''' Obtiene el listado de talento humano 
    ''' </summary>
    ''' <returns>Listado de talento humano</returns>
    Public Async Function ListAllEmployeeAsync() As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllEmployeeAsync(Indigo)
    End Function

    ''' <summary>
    ''' Retorna lista de empleados filtrados por unidad funcional
    ''' </summary>
    ''' <param name="FunctionalUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEmployeesByFunctionalUnitAsync(ByVal FunctionalUnitId As Integer) As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeesByFunctionalUnitAsync(FunctionalUnitId, Indigo)
    End Function

    ''' <summary>
    ''' Retorna lista de empleados filtrados por group
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEmployeesByGroupIdAsync(ByVal GroupId As Integer) As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeesByGroupIdAsync(GroupId, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios de talento humano
    ''' </summary>
    ''' <param name="Employee">Talento humano</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveEmployeeAsync(ByVal Employee As Employee, listExemptIncome As List(Of Domain.Entities.ExemptIncome), listExemptIncomeTodelete As List(Of Domain.Entities.ExemptIncome)) As Task(Of ActionMessageResult(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveEmployeeAsync(Employee, Indigo, listExemptIncome, listExemptIncomeTodelete)
    End Function
    ''' <summary>
    ''' Funcion que guarda el detalle de renta exenta
    ''' </summary>
    ''' <param name="ExemptIncome"></param>
    ''' <returns></returns>
    Public Async Function SaveExemptIncomeAsync(ByVal ExemptIncome As List(Of Domain.Entities.ExemptIncome)) As Task(Of ActionMessageResult(Of Domain.Entities.ExemptIncome))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveExemptIncomeAsync(ExemptIncome, Indigo)
    End Function
    ''' <summary>
    ''' Funcion que elimina lel detalle de rentas exentas
    ''' </summary>
    ''' <param name="ExemptIncome"></param>
    ''' <returns></returns>
    Public Async Function DeleteExemptIncomeAsync(ByVal ExemptIncome As List(Of Domain.Entities.ExemptIncome)) As Task(Of ActionMessageResult(Of Domain.Entities.ExemptIncome))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteExemptIncomeAsync(ExemptIncome, Indigo)
    End Function
    ''' <summary>
    ''' Borra los cambios de talento humano
    ''' </summary>
    ''' <param name="Employee">Talento humano</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteEmployeeAsync(ByVal Employee As Employee) As Task(Of ActionMessageResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteEmployeeAsync(Employee, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Employee", Indigo)
    End Function

    ''' <summary>
    ''' Borra los cambios de talento humano
    ''' </summary>
    ''' <param name="Employee">Talento humano</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function GetEmployeePensionary() As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeePensionaryAsync(Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el reporte de talento humano
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="employeeId">ID del empleado (opcional)</param>
    ''' <returns>ActionResult con la lista de resultados</returns>
    Public Async Function GetReportHumanTalentAsync(initialDate As Date, finalDate As Date, employeeId As Integer?) As Task(Of ActionResult(Of List(Of SP_ReportHumanTalent_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetReportHumanTalentAsync(
        initialDate,
        finalDate,
        Indigo,
        employeeId)
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
