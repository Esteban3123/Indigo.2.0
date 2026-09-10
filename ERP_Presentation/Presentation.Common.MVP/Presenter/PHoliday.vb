'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 24-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Entities
Imports DevExpress.XtraScheduler

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de Tipo de vinculación
''' </summary>
Public Class PHoliday

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IHoliday
    ''' </summary>
    Private _view As IHoliday
    ''' <summary>
    ''' Variable que se utilizapa para tratar los festivos y dominicales como un Objeto
    ''' </summary>
    Private _Holiday As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de festivos y dominicales
    ''' </summary>
    ''' <param name="view">Vista de el frontal</param>
    Public Sub New(ByRef view As IHoliday)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Funcion que carga los appointment existentes en ese año
    ''' </summary>
    Public Async Function LoadHolidays() As Task
        Me._view.HolidaySchedulerStorage.Appointments.Clear()

        If Me._view.ControlCurrentYear <= 0 Then
            Me._view.ControlCurrentYear = Microsoft.VisualBasic.Year(DateTime.Now())
        End If

        Dim ListHoliday As New List(Of Holiday)
        Using Model As New MHoliday
            Me._view.ListHolidays = Await Model.ListAllHolidaysbyYearsAsync(Me._view.ControlCurrentYear)
        End Using
    End Function
#End Region
    


End Class