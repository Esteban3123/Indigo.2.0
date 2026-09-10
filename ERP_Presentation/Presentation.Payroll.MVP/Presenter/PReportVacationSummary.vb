'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Domain.Payroll.Entities

''' <summary>
''' Presentador del reporte de resumen de vacaciones
''' </summary>
Public Class PReportVacationSummary

    ''' <summary>
    ''' Variable para almacenar el objeto que cumple con el contrato
    ''' </summary>
    Dim view As IReportVacationSummary

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor del presentador de reporte de vacaciones
    ''' </summary>
    ''' <param name="viewForm">Vista del formulario</param>
    Sub New(viewForm As IReportVacationSummary)
        If viewForm Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.view = viewForm
            Indigo = SessionValues.Instance
        End If
    End Sub

    ''' <summary>
    ''' Carga el resumen de vacaciones
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    Public Async Function LoadVacationSummary(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?) As Task
        Using model As New MReportVacationSummary(MReportVacationSummary.TAG)
            view.VacationSummaryDataSource = Await model.GetVacationSummaryAsync(yearClosed, monthClosed, employeeId)
        End Using
    End Function

End Class

