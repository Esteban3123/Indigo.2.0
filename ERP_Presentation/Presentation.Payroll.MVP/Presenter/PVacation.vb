'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo
''' <summary>
''' Presentador del frontal de Vacaciones
''' </summary>
Public Class PVacation

    ''' <summary>
    ''' variable para almacenar el objeto que cumple con el contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim view As IVacation

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Contructor del presentador de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(viewForm As IVacation)
        If viewForm Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.view = viewForm
            Using model As New MVacation(MVacation.TAG)
                view.EmployeeDataSource = model.GetAllEmployee()
                view.GroupDataSource = model.GetAllGroup()
            End Using
            Indigo = SessionValues.Instance
        End If
    End Sub

    ''' <summary>
    ''' Carga los periodos de vacaciones de un empleado
    ''' </summary>
    ''' <param name="choice"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Async Function loadPeriodVacation(choice As Byte, value As String) As Task
        If value Is Nothing Or value = "" Then
            view.VacationDataSource = Nothing
            Return
        End If
        Using model As New MVacation(MVacation.TAG)
            view.VacationDataSource = Await model.ListVacationByFilterAsync(choice, value)
        End Using
    End Function

    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

End Class
