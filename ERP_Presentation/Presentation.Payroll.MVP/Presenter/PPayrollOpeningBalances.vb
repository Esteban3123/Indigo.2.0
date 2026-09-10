'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 09-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Domain.Payroll.Entities

Public Class PPayrollOpeningBalances

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPayrollOpeningBalances

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPayrollOpeningBalances)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del empleado
    ''' </summary>
    Public Sub InitializeEmployee()
        Dim modelo As New MBusqueda
        View.EmployeeXpo = modelo.ConsultarEntidades(eDataSource.AllEmployeesWithContractToLiquidate)
    End Sub

    ''' <summary>
    ''' Devuelve el listado de conceptos para los diferentes controles de concepto
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConceptPayrollXpo() As DevExpress.Xpo.XPInstantFeedbackSource
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListConceptsPayrollByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene los datos del empleado seleccionado en el buscador de empleados
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    Public Async Function GetSelectedEmployeeById(id As Integer) As Task(Of Employee)
        Dim e As Employee
        Using model As New MEmployee(MEmployee.TAG)
            e = Await model.GetEmployeeByIdForContractLiquidation(id)
        End Using
        Return e
    End Function

End Class
