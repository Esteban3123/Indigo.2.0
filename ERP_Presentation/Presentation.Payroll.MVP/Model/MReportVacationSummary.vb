'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports IndigoReference.Payroll

Public Class MReportVacationSummary
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "89036"

    Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene el resumen de vacaciones de un empleado en un periodo específico
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    ''' <returns>Lista de SP_SummaryVacationLiquidation_Result desde el servicio WCF</returns>
    Public Async Function GetVacationSummaryAsync(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?) As Task(Of List(Of SP_SummaryVacationLiquidation_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetVacationSummaryAsync(yearClosed, monthClosed, employeeId, Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

