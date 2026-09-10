'***********************************************************************
' Assembly         : Presentacion.Common.MVP
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

Imports Presentation.Controls
Imports Presentation.Base

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IHoliday
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el control del calendario para los festivos y dominicales
    ''' </summary>
    ReadOnly Property HolidaySchedulerControl As DevExpress.XtraScheduler.SchedulerControl

    ''' <summary>
    ''' Propiedad que contiene el control que almacena las fechas de el calendario
    ''' </summary>
    ReadOnly Property HolidaySchedulerStorage As DevExpress.XtraScheduler.SchedulerStorage

    ''' <summary>
    ''' Propiedad que contiene el control de navegacion del calendario
    ''' </summary>
    ReadOnly Property HolidayDateNavigator As DevExpress.XtraScheduler.DateNavigator

    ''' <summary>
    ''' Propiedad que contiene el año actual de el control
    ''' </summary>
    Property ControlCurrentYear As Integer

    Property ListHolidays As List(Of Domain.Entities.Holiday)
#End Region

End Interface
