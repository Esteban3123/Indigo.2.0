'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 23-07-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Presentation.Base

Public Class MScheduleControl
    Inherits ModelBase
    Implements IDisposable

    Shared TAG As String = "551"

    Sub New()
        MyBase.New(TAG)
    End Sub

    ''' <summary>
    ''' Obtiene una plantilla de turno a través del codigo
    ''' </summary>
    ''' <param name="code">codigo de la plantilla</param>
    ''' <returns>Plantilla de turno</returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleTemplateAsync(code As String) As Task(Of ScheduleTemplate)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleTemplateAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una plantilla de turno a través del id
    ''' </summary>
    ''' <param name="id">id de la plantilla</param>
    ''' <returns>Plantilla de turno</returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleTemplateByIdAsync(id As String) As Task(Of ScheduleTemplate)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleTemplateByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios sobre la entidad schedule
    ''' </summary>
    ''' <param name="schedule">Plantilla de contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function SaveScheduleTemplateAsync(schedule As ScheduleTemplate) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveScheduleTemplateAsync(schedule, Indigo)
    End Function

    ''' <summary>
    ''' Elimina una plantilla de turno
    ''' </summary>
    ''' <param name="schedule">Plantilla de turno</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function DeleteScheduleTemplateAsycn(schedule As ScheduleTemplate) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteScheduleTemplateAsync(schedule, Indigo)
    End Function

    ''' <summary>
    ''' Metodo para retornar las plantillas de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllScheduleTemplate() As Task(Of List(Of ScheduleTemplate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllScheduleTemplateAsync(Indigo)
    End Function

    ''' <summary>
    ''' Metodo para retornar las plantillas de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllScheduleTemplateByState(state As Boolean) As Task(Of List(Of ScheduleTemplate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllScheduleTemplateByStatusAsync(state, Indigo)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateScheduleTemplate(Code As String, State As Boolean) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateScheduleTemplateAsync(Code, State, Indigo)
    End Function


    'Public Async Function GetFieldsNull() As Task(Of DataSet)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetFieldsNULLAsync("Payroll", "ScheduleTemplate")
    'End Function

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
