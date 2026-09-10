'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay
' Created          : 01-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface Ischedule
    'Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que muestra u oculta el menu de opciones
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ShowOptionsMenu As Boolean

    ''' <summary>
    ''' Propiedad que contiene el datasource de las unidades funcionales
    ''' </summary>
    WriteOnly Property FunctionalUnit_Datasource As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene la entidad unidad funcional
    ''' </summary>
    Property FunctionalUnit As FunctionalUnit

    ''' <summary>
    ''' Propiedad que contiene la entidad Schedule no 1
    ''' </summary>
    Property ScheduleN1 As Schedule

    ''' <summary>
    ''' Propiedad que contiene la entidad Schedule no 2
    ''' </summary>
    Property ScheduleN2 As Schedule

    ''' <summary>
    ''' Propiedad que contiene el control list de los empleados
    ''' </summary>
    ReadOnly Property ListEmployee As DevExpress.XtraEditors.CheckedListBoxControl

    ''' <summary>
    ''' Propiedad que contiene el periodo del cuadro de turnos (yyyy-m)
    ''' </summary>
    ReadOnly Property Period As String

    ''' <summary>
    ''' Propiedad que contiene la el control calendario
    ''' </summary>
    ReadOnly Property CtrCalendar As Presentation.Controls.CtrCalendar

    ''' <summary>
    ''' Propiedad que contiene el gridControl del cuadro de turnos
    ''' </summary>
    ReadOnly Property ScheduleGridControl As DevExpress.XtraGrid.GridControl

    ''' <summary>
    ''' Propiedad que contiene el grid view del cuadro de turnos
    ''' </summary>
    ReadOnly Property ScheduleGridView As DevExpress.XtraGrid.Views.Grid.GridView

    ''' <summary>
    ''' Propiedad solo lectura que contiene el grid view de el search look up de las unidades funcionales
    ''' </summary>
    ReadOnly Property FunctionalUnitGridView As DevExpress.XtraGrid.Views.Grid.GridView

    ''' <summary>
    ''' Propiedad que contiene el layout del panel de detalle completo
    ''' </summary>
    ReadOnly Property PanelScheduleDetailComplete As DevExpress.xtralayout.layoutControlItem

    ''' <summary>
    ''' Propiedad q contiene listado de los festivos en dicho mes
    ''' </summary>
    Property List_Holiday As List(Of Domain.Entities.Holiday)

    ''' <summary>
    ''' Propiedad que contiene en diccionario los schedule detail de la entidad schedule del primer empleado
    ''' </summary>
    Property DictionaryScheduleDetail_First As Dictionary(Of Integer, ScheduleDetail)

    ''' <summary>
    ''' Propiedad que contiene en diccionario los schedule detail de la entidad schedule del segundo empleado
    ''' </summary>
    Property DictionaryScheduleDetail_Second As Dictionary(Of Integer, ScheduleDetail)

    ''' <summary>
    ''' Objeto que contiene los registros a mostrar 
    ''' </summary>
    ''' <remarks></remarks>
    Property ScheduleDatasource As List(Of Schedule)


    WriteOnly Property PositionDatasource as List(Of Position)

    ''' <summary>
    ''' Contiene el listado los schedule en todas las unidades funcionales de el empelado No 1
    ''' </summary>
    Property ScheduleInPeriodN1 As List(Of ScheduleDetail)

    ''' <summary>
    ''' Contiene el listado los schedule en todas las unidades funcionales de el empelado No 2
    ''' </summary>
    Property ScheduleInPeriodN2 As List(Of ScheduleDetail)

    ''' <summary>
    ''' Contiene el todos los schedule detail de todos los empleados para esta unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ScheduleComplete As List(Of List(Of ScheduleDetail))

    ''' <summary>
    ''' Establece si se reduce el tamaño de los controles de informacion de detalle de horas
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property SetLittleControlsDetail As Boolean

#Region "Schedule Detail Panel Controls"
    ''' <summary>
    ''' Propiedad para acceder al control label de la totalidad de horas para el primero
    ''' </summary>
    ReadOnly Property SDLabelTotalHours1 As DevExpress.XtraEditors.LabelControl

    ''' <summary>
    ''' Propiedad para acceder al control label que contiene el texto "Total Horas" para el primero
    ''' </summary>
    ReadOnly Property SDLabelTotalHoursText1 As DevExpress.XtraEditors.LabelControl

    ''' <summary>
    ''' Propiedad para acceder al control label de la totalidad de horas para el segundo
    ''' </summary>
    ReadOnly Property SDLabelTotalHours2 As DevExpress.XtraEditors.LabelControl

    ''' <summary>
    ''' Propiedad para acceder al control label que contiene el texto "Total Horas" para el segundo
    ''' </summary>
    ReadOnly Property SDLabelTotalHoursText2 As DevExpress.XtraEditors.LabelControl

    ''' <summary>
    ''' Propiedad que contiene el label control para colocar el nombre del funcionario No 1
    ''' </summary>
    ReadOnly Property SDLabelEmployee1 As DevExpress.XtraEditors.LabelControl

    ''' <summary>
    ''' Propiedad que contiene el label control para colocar el nombre del funcionario No 2
    ''' </summary>
    ReadOnly Property SDLabelEmployee2 As DevExpress.XtraEditors.LabelControl

    ''' <summary>
    ''' Accion de los controles cuando se va a comparar el detalle de 2 empleados
    ''' </summary>
    WriteOnly Property ActionOnDetailToCompare As Boolean
#End Region

    ''' <summary>
    ''' Propiedad que establece la accion de las vistas del frontal (vista calendario o vista rejilla)
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ViewGridSchedule As Boolean

    ''' <summary>
    ''' Obtiene los empleados Chekeados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ScheduleIdListChecked As List(Of Integer)
    

#End Region

End Interface

