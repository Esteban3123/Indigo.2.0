'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 30-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports DevExpress.Utils.Design.DesignTimeTools
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.CloudAgent
Imports Presentation.Controls
Imports Presentation.Payroll.MVP

#End Region
''' <summary>
''' Clase que contiene el formulario de los cuadros de turno
''' </summary>
''' <remarks></remarks>
Public Class FrmSchedule1
    Implements Ischedule

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene el presenter
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PSchedule

    ''' <summary>
    ''' Objeto que se utiliza para cargar la unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Dim _FunctionalUnit As FunctionalUnit

    ''' <summary>
    ''' Variable que contiene el periodo del cuadro de turno
    ''' </summary>
    Dim _period As String

    ''' <summary>
    ''' Variable que contiene la fecha del cuadro de turno, empezando desde el primer dia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ScheduleDate As Date

    ''' <summary>
    ''' Contiene un listado de los festivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _List_Holidays As List(Of Domain.Entities.Holiday)

    ''' <summary>
    ''' Variable para almacenar la entidad Schedule No 1
    ''' </summary>
    ''' <remarks></remarks> 
    Dim _ScheduleN1 As Schedule

    ''' <summary>
    ''' Variable para almacenar la entidad Schedule No 2
    ''' </summary>
    ''' <remarks></remarks> 
    Dim _ScheduleN2 As Schedule

    ''' <summary>
    ''' Variable que contiene el diccionario con los detalles de los turnos del primer empleado seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _DictionaryScheduleDetail_First As Dictionary(Of Integer, ScheduleDetail)

    ''' <summary>
    ''' Variable que contiene el diccionario con los detalles de los turnos del segundo empleado seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _DictionaryScheduleDetail_Second As Dictionary(Of Integer, ScheduleDetail)

    ''' <summary>
    ''' Listado de id´s del los empleados seleccionados
    ''' </summary>
    Dim _ScheduleIdListChecked As New List(Of Integer)

    ''' <summary>
    ''' Objeto que contiene los registros a mostrar 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ScheduleDatasource As List(Of Schedule)

    ''' <summary>
    ''' Variable que contiene los schedule detail del empleado No 1 en todas las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ScheduleInPeriodN1 As List(Of ScheduleDetail)

    ''' <summary>
    ''' Variable que contiene los schedule detail del empleado No 2 en todas las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ScheduleInPeriodN2 As List(Of ScheduleDetail)

    ''' <summary>
    ''' Variable que contiene los valores de sesion
    ''' </summary>
    ''' <remarks></remarks>
    Private Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que contiene la CultureInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ci As System.Globalization.CultureInfo

    ''' <summary>
    ''' Variable que contiene el DateTimeFormatInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtfi As System.Globalization.DateTimeFormatInfo = Nothing

    Dim FlagConsultEmployee As Boolean = False

    Dim EmployeeTmp As Employee = New Employee

    Dim InitialDatePayroll As Date

    Dim EndDatePayroll As Date

    Dim PermissionEdit As Boolean = False

    Dim _ListSchedule As List(Of Schedule)

    'VARIABLES DE PERMISOS DEL CUADRO DE TURNO
    'Variable para saber si tiene permiso a Todas Las Unidades Funcionales o no
    Dim PermissionAllFunctionalUnit As Boolean = False

    'Variable para saber si tiene permiso a Todos los Cargos o no
    Dim PermissionAllPosition As Boolean = False

    Dim ServerDate As DateTime


#End Region

#Region "Properties"

#Region "Globals"

    ''' <summary>
    ''' Propiedad que muestra u oculta el menu de opciones
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ShowOptionsMenu As Boolean Implements Ischedule.ShowOptionsMenu
        Set(value As Boolean)
            If value = True Then
                INDLyItemOptionMenu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLyItemOptionMenu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    ''' <summary>
    '''Propiedad que contiene la el control calendario
    ''' </summary>
    Public ReadOnly Property CtrCalendar As Presentation.Controls.CtrCalendar Implements Ischedule.CtrCalendar
        Get
            Return INDCalendar
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de las unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FunctionalUnit_Datasource As DevExpress.Xpo.XPInstantFeedbackSource Implements Ischedule.FunctionalUnit_Datasource
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el control grid control de los cuadro de turnos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property ScheduleGridControl As DevExpress.XtraGrid.GridControl Implements Ischedule.ScheduleGridControl
        Get
            Return INDGcScheduleDetail1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el control grid view de los cuadro de turnos
    ''' </summary>
    Public ReadOnly Property ScheduleGridView As DevExpress.XtraGrid.Views.Grid.GridView Implements Ischedule.ScheduleGridView
        Get
            Return INDGvScheduleDetail1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad solo lectura que contiene el grid view de el search look up de las unidades funcionales
    ''' </summary>
    Public ReadOnly Property FunctionalUnitGridView As DevExpress.XtraGrid.Views.Grid.GridView Implements Ischedule.FunctionalUnitGridView
        Get
            Return INDGvFunctionalUnit
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el layout del panel de detalle completo
    ''' </summary>
    Public ReadOnly Property PanelScheduleDetailComplete As DevExpress.XtraLayout.LayoutControlItem Implements Ischedule.PanelScheduleDetailComplete
        Get
            Return INDLyItemScheduleDetailPanelComplete
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene la entidad unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnit As FunctionalUnit Implements Ischedule.FunctionalUnit
        Get
            Return _FunctionalUnit
        End Get
        Set(value As FunctionalUnit)
            _FunctionalUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el periodo del cuadro de turno (MM/YYYY)
    ''' </summary>
    Public ReadOnly Property Period As String Implements Ischedule.Period
        Get
            Dim _month As String
            Dim _year As String
            _year = CStr(INDCalendar.YearControl)
            _month = CStr(INDCalendar.MonthControl)
            If _month.Length = 1 Then
                _month = "0" & _month
            End If
            _period = _month & "/" & _year
            Return _period
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene una lista de los festivos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property List_Holiday As List(Of Domain.Entities.Holiday) Implements Ischedule.List_Holiday
        Get
            Return _List_Holidays
        End Get
        Set(value As List(Of Domain.Entities.Holiday))
            _List_Holidays = value
            CtrCalendar.ListHoliday = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el control list de los empleados
    ''' </summary>
    Public ReadOnly Property ListEmployee As DevExpress.XtraEditors.CheckedListBoxControl Implements Ischedule.ListEmployee
        Get
            Return INDChkLbcEmployee
        End Get
    End Property

    Public WriteOnly Property ListPosition As List(Of Position) Implements Ischedule.PositionDatasource
        Set(value As List(Of Position))
            INDSlPosition.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la entidad Schedule No 1
    ''' </summary>
    Public Property ScheduleN1 As Schedule Implements Ischedule.ScheduleN1
        Get
            Return _ScheduleN1
        End Get
        Set(value As Schedule)
            _ScheduleN1 = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la entidad Schedule No 2
    ''' </summary>
    Public Property ScheduleN2 As Schedule Implements Ischedule.ScheduleN2
        Get
            Return _ScheduleN2
        End Get
        Set(value As Schedule)
            _ScheduleN2 = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el diccionario del schedule del primer registro
    ''' </summary>
    Public Property DictionaryScheduleDetail_First As Dictionary(Of Integer, ScheduleDetail) Implements Ischedule.DictionaryScheduleDetail_First
        Get
            Return _DictionaryScheduleDetail_First
        End Get
        Set(value As Dictionary(Of Integer, ScheduleDetail))
            _DictionaryScheduleDetail_First = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene en diccionario los schedule detail de la entidad schedule del segundo empleado
    ''' </summary>
    Public Property DictionaryScheduleDetail_Second As Dictionary(Of Integer, ScheduleDetail) Implements Ischedule.DictionaryScheduleDetail_Second
        Get
            Return _DictionaryScheduleDetail_Second
        End Get
        Set(value As Dictionary(Of Integer, ScheduleDetail))
            _DictionaryScheduleDetail_Second = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para acceder a el label del total de horas para el primero
    ''' </summary>
    Public ReadOnly Property SDLabelTotalHours1 As LabelControl Implements Ischedule.SDLabelTotalHours1
        Get
            Return INDLcTotalHoursNumber1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para acceder al control label que contiene el texto "Total Horas" para el primero
    ''' </summary>
    Public ReadOnly Property SDLabelTotalHoursText1 As LabelControl Implements Ischedule.SDLabelTotalHoursText1
        Get
            Return INDLcTotalHours1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para acceder a el label del total de horas para el primero
    ''' </summary>
    Public ReadOnly Property SDLabelTotalHours2 As LabelControl Implements Ischedule.SDLabelTotalHours2
        Get
            Return INDLcTotalHoursNumber2
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para acceder al control label que contiene el texto "Total Horas" para el primero
    ''' </summary>
    Public ReadOnly Property SDLabelTotalHoursText2 As LabelControl Implements Ischedule.SDLabelTotalHoursText2
        Get
            Return INDLcTotalHours2
        End Get
    End Property

    ''' <summary>
    ''' Accion de los controles cuando se va a comparar el detalle de 2 empleados
    ''' </summary>
    Public WriteOnly Property ActionOnDetailToCompare As Boolean Implements Ischedule.ActionOnDetailToCompare
        Set(value As Boolean)
            INDLcEmployee2.Visible = value
            INDLcEmployee1.Visible = value
            INDLcTotalHours2.Visible = value
            INDLcTotalHoursNumber2.Visible = value
            INDDDBEmployee1.Visible = value
            INDDDBEmployee2.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el label control para colocar el nombre del funcionario No 1
    ''' </summary>
    Public ReadOnly Property SDLabelEmployee1 As LabelControl Implements Ischedule.SDLabelEmployee1
        Get
            Return INDLcEmployee1
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el label control para colocar el nombre del funcionario No 2
    ''' </summary>
    Public ReadOnly Property SDLabelEmployee2 As LabelControl Implements Ischedule.SDLabelEmployee2
        Get
            Return INDLcEmployee2
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los registros a mostrar 
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ScheduleDatasource As List(Of Schedule) Implements Ischedule.ScheduleDatasource
        Get
            Return _ScheduleDatasource
        End Get
        Set(value As List(Of Schedule))
            _ScheduleDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de los ID´s de los empleados seleccionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ScheduleIdListChecked As List(Of Integer) Implements Ischedule.ScheduleIdListChecked
        Get
            Return _ScheduleIdListChecked
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que establece la accion de las vistas del frontal (vista calendario o vista rejilla)
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ViewGridSchedule As Boolean Implements Ischedule.ViewGridSchedule
        Set(value As Boolean)
            If value = True Then
                INDLyItemCalendar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSpace1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSpace2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemScheduleDetailPanelComplete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemTxtEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemEmployeeList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemLayoutDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDBarMarkAsDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                INDLyItemScheduleDetailGrid1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLyItemCalendar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSpace1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSpace2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyItemScheduleDetailPanelComplete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyItemTxtEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyItemEmployeeList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Eliminar) = True Then
                '    INDBarMarkAsDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                'End If
                INDLyItemScheduleDetailGrid1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado los schedule en todas las unidades funcionales de el empelado No 1
    ''' </summary>
    Public Property ScheduleInPeriodN1 As List(Of ScheduleDetail) Implements Ischedule.ScheduleInPeriodN1
        Get
            Return _ScheduleInPeriodN1
        End Get
        Set(value As List(Of ScheduleDetail))
            _ScheduleInPeriodN1 = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado los schedule en todas las unidades funcionales de el empelado No 2
    ''' </summary>
    Public Property ScheduleInPeriodN2 As List(Of ScheduleDetail) Implements Ischedule.ScheduleInPeriodN2
        Get
            Return _ScheduleInPeriodN2
        End Get
        Set(value As List(Of ScheduleDetail))
            _ScheduleInPeriodN2 = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String ' Implements IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece la accion en los controles de marcar para eliminar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionOnControlsDelete As Boolean
        Set(value As Boolean)
            CtrCalendar.ModeDelete = value
            INDLyDelete.Visible = value
            If value = False Then
                INDLyItemLayoutDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDLyItemLayoutDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End Set
    End Property

    ''' <summary>
    ''' Variable q contiene el listado de los schedule detail
    ''' </summary>
    ''' <remarks></remarks>
    Private _ScheduleComplete As List(Of List(Of ScheduleDetail))
    ''' <summary>
    ''' Contiene o Establece el listado de todos los schedule detail de todos los empleados cargados por unidad funcional
    ''' </summary>
    Public Property ScheduleComplete As List(Of List(Of ScheduleDetail)) Implements Ischedule.ScheduleComplete
        Get
            Return _ScheduleComplete
        End Get
        Set(value As List(Of List(Of ScheduleDetail)))
            _ScheduleComplete = value
        End Set
    End Property

    ''' <summary>
    ''' Establece si se reduce el tamaño de los controles de informacion de detalle de horas
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetLittleControlsDetail() As Boolean Implements Ischedule.SetLittleControlsDetail
        Set(value As Boolean)
            If value = True Then
                INDLyItemScheduleDetailPanelComplete.MaxSize = New System.Drawing.Size(843, 20)
                INDLyItemScheduleDetailPanelComplete.MinSize = New System.Drawing.Size(843, 20)
                INDPcDetailEm2.Location = New System.Drawing.Point(11, -43)
                INDLcEmployee2.Location = New System.Drawing.Point(115, 0)
                INDPcDetailEm1.Location = New System.Drawing.Point(738, -43)
                INDLcEmployee1.Location = New System.Drawing.Point(418, 0)
                INDLcEmployee1.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
                INDLcEmployee2.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
                INDLcTotalHours1.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 10.0!)
                INDLcTotalHours2.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 10.0!)
                INDLcEmployee1.ForeColor = CtrCalendar.RegColor1
                INDLcEmployee2.ForeColor = CtrCalendar.RegColor2
                INDLcTotalHours1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
                INDLcTotalHours2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
                INDLcTotalHours1.Font = New System.Drawing.Font(INDLcTotalHours1.Font, System.Drawing.FontStyle.Underline)
                INDLcTotalHours2.Font = New System.Drawing.Font(INDLcTotalHours1.Font, System.Drawing.FontStyle.Underline)
                INDLcTotalHours1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                INDLcTotalHours2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Else
                INDLyItemScheduleDetailPanelComplete.MaxSize = New System.Drawing.Size(843, 68)
                INDLyItemScheduleDetailPanelComplete.MinSize = New System.Drawing.Size(843, 68)
                INDPcDetailEm2.Location = New System.Drawing.Point(11, 3)
                INDLcEmployee2.Location = New System.Drawing.Point(115, 43)
                INDPcDetailEm1.Location = New System.Drawing.Point(738, 3)
                INDLcEmployee1.Location = New System.Drawing.Point(418, 43)
                INDLcEmployee1.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 12.0!)
                INDLcEmployee2.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 12.0!)
                INDLcTotalHours1.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 12.0!)
                INDLcTotalHours2.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 12.0!)
                INDLcEmployee1.ForeColor = System.Drawing.Color.Gray
                INDLcEmployee2.ForeColor = System.Drawing.Color.Gray
                INDLcTotalHours1.ForeColor = System.Drawing.Color.Gray
                INDLcTotalHours2.ForeColor = System.Drawing.Color.Gray
                INDLcTotalHours1.Font = New System.Drawing.Font(INDLcTotalHours1.Font, System.Drawing.FontStyle.Regular)
                INDLcTotalHours2.Font = New System.Drawing.Font(INDLcTotalHours1.Font, System.Drawing.FontStyle.Regular)
                INDLcTotalHours1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
                INDLcTotalHours2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            End If
        End Set
    End Property

#End Region

#Region "PopUpDetail"

    ''' <summary>
    ''' Propiedad que establece el nombre del empleado
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpEmployeeName As String
        Set(value As String)
            INDPopupLcEmployee.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero del dia
    ''' </summary>
    ''' <remarks></remarks>
    Private _popupNumDay As Integer
    Public WriteOnly Property PopupNumDay As Integer
        Set(value As Integer)
            _popupNumDay = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero del empleadoa mostrar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private _popupEmployeeNumber As String
    Public WriteOnly Property PopUpEmployeeNumber As String
        Set(value As String)
            _popupEmployeeNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece Propiedad que establece la letra del turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpLetter As String
        Set(value As String)
            PopUpTemplate = GetNameTemplate(value)
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre e informacion de la plantilla de  turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpTemplate As String
        Set(value As String)
            INDPopupLcTxtTemplate.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el numero de horasen el pop up de detalle de schedule
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpTemplateHoursNumber As String
        Set(value As String)
            INDPopupLcTxtTemplateHoursNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la fecha
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpDate As Date
        Set(value As Date)
            If IsDesignMode = False Then
                dtfi = ci.DateTimeFormat
            Else
                dtfi = New Globalization.DateTimeFormatInfo
            End If
            INDPopupLcNumberDay.Text = dtfi.GetDayName(value.DayOfWeek) & ", " & value.Day & " de " & dtfi.GetMonthName(value.Month) & " de " & value.Year
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la rejilla de los compañeros de turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpMatches As List(Of Schedule)
        Set(value As List(Of Schedule))
            If value IsNot Nothing Then
                If value.Count > 0 Then
                    INDPopupGcMatches.DataSource = value
                Else
                    INDPopupGcMatches.DataSource = Nothing
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la rejilla de los compañeros de turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpDetHours As TrackableCollection(Of ScheduleDetailHour)
        Set(value As TrackableCollection(Of ScheduleDetailHour))
            If value IsNot Nothing Then
                If value.Count > 0 Then
                    INDgcDetHours.DataSource = value
                    If value.Where(Function(x) x.Event = True).FirstOrDefault() IsNot Nothing Then 'si es un turno donde hay eventos, muestre la columna de aprobado
                        INDColApproval.Visible = True
                    End If
                Else
                    INDgcDetHours.DataSource = Nothing
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la visibilidad del boton de editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpVisibilityButtonEdit As Boolean
        Set(value As Boolean)
            If value = True Then
                INDLyItemDetailEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLyItemDetailEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el Drop down button actual segun el dia
    ''' </summary>
    ''' <remarks></remarks>
    Private _popUpDropDownButton As DevExpress.XtraEditors.DropDownButton
    Public Property PopUpDropDownButton As DevExpress.XtraEditors.DropDownButton
        Get
            Return _popUpDropDownButton
        End Get
        Set(value As DevExpress.XtraEditors.DropDownButton)
            _popUpDropDownButton = value
        End Set
    End Property
#End Region

#Region "PopUpHours"
    ''' <summary>
    ''' Propiedad que contiene el nombre del empleado en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursEmployee As String
        Set(value As String)
            INDPopupHoursLcEmployee.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero de horas maxima en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursMaxNumber As String
        Set(value As String)
            INDPopupHoursMaxNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero de horas minima en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursMinNumber As String
        Set(value As String)
            INDPopupHoursMinNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero de horas trabajadas en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursWorkedNumber As String
        Set(value As String)
            INDPopupHoursWorkedNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el porcentaje de trabajo en el mes en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursWorkedPercentage As String
        Set(value As String)
            INDPopupHoursWorkedPercentage.Text = value & "%"
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de las horas en detalle en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursDetailDatasource As List(Of DetailHoursByConcept)
        Set(value As List(Of DetailHoursByConcept))
            INDPopupHoursGcHoursDetail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de las horas en detalle en el pop  up de Detalle de hora
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopupHoursDetailPendingDatasource As List(Of DetailHoursByConcept)
        Set(value As List(Of DetailHoursByConcept))
            INDPopupHoursGcHoursPendingDetail.DataSource = value
        End Set
    End Property
#End Region

#Region "PopUpMore"

    ''' <summary>
    ''' Propiedad que contiene el Drop down button actual segun el dia
    ''' </summary>
    ''' <remarks></remarks>
    Private _popUpMoreDropDownButton As DevExpress.XtraEditors.DropDownButton
    Public Property PopUpMoreDropDownButton As DevExpress.XtraEditors.DropDownButton
        Get
            Return _popUpMoreDropDownButton
        End Get
        Set(value As DevExpress.XtraEditors.DropDownButton)
            _popUpMoreDropDownButton = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el numero del dia
    ''' </summary>
    ''' <remarks></remarks>
    Private _popupMoreNumDay As Integer
    Public WriteOnly Property PopupMoreNumDay As Integer
        Set(value As Integer)
            _popupMoreNumDay = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el nombre del empleado
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpMoreEmployeeName As String
        Set(value As String)
            INDPopupMoreLcEmployee.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la fecha
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpMoreDate As Date
        Set(value As Date)
            If IsDesignMode = False Then
                dtfi = ci.DateTimeFormat
            Else
                dtfi = New Globalization.DateTimeFormatInfo
            End If
            INDPopupMoreLcNumberDay.Text = dtfi.GetDayName(value.DayOfWeek) & ", " & value.Day & " de " & dtfi.GetMonthName(value.Month) & " de " & value.Year
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el numero de horasen el pop up de los turnos en otras unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpMoreTemplateHoursNumber As String
        Set(value As String)
            INDPopupMoreLcTxtOtherSchHoursNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la rejilla de los turnos en otras unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PopUpMoreSchDet As List(Of ScheduleDetailInOtherFU)
        Set(value As List(Of ScheduleDetailInOtherFU))
            If value IsNot Nothing Then
                If value.Count > 0 Then
                    INDPopupMoreGcHoursDetail.DataSource = value
                Else
                    INDPopupMoreGcHoursDetail.DataSource = Nothing
                End If
            End If
        End Set
    End Property

#End Region

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _FunctionalUnit = Nothing
        _period = Nothing
        _ScheduleDate = Nothing
        _List_Holidays = Nothing
        _ScheduleN1 = Nothing
        _ScheduleN2 = Nothing
        _DictionaryScheduleDetail_First = Nothing
        _DictionaryScheduleDetail_Second = Nothing
        _ScheduleIdListChecked = Nothing
        _ScheduleDatasource = Nothing
        _ScheduleInPeriodN1 = Nothing
        _ScheduleInPeriodN2 = Nothing
        ci = Nothing
        dtfi = Nothing
        FlagConsultEmployee = Nothing
        EmployeeTmp = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub FrmSchedule1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PSchedule(Me)
        Controls_Size()
        ci = Indigo.Culture
        AddHandler CtrCalendar.OnDoubleClickDays, AddressOf DoubleClick_Days
        AddHandler CtrCalendar.OnClickDays, AddressOf Click_Days
        AddHandler CtrCalendar.OnChangeDate, AddressOf Change_Date
        AddHandler INDChkLbcEmployee.ItemCheck, AddressOf INDChkLbcEmployee_ItemCheck
        AddHandler CtrCalendar.OnMouseHoverDays, AddressOf OnMouseHoverDays
        AddHandler CtrCalendar.OnMouseHoverMore, AddressOf OnMouseHoverMore
        Presenter.Load_Holidays()

        'Consulto los permisos de Usuario y Rol
        LoadPermissionSchedule()

        'Consulto Permisos del barraBotones
        Dim pBarra = New Presentation.Controls.MVP.PBarraBotones(BarraBotones)
        pBarra.ConsultarPermisos(CStr(MyBase.Tag))
    End Sub

    Private Sub LoadPermissionSchedule()
        Dim ContractPermiso = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(Indigo.UserIndigo, Indigo.UserRol, CStr(MyBase.Tag), Indigo)

        For i As Integer = 0 To ContractPermiso.Count() - 1
            If ContractPermiso.Item(i).TagButton = 86 Then
                Presenter.FunctionalUnit_Datasource()
                PermissionAllFunctionalUnit = True
                PermissionAllPosition = True
                PermissionEdit = True
            End If

            If ContractPermiso.Item(i).TagButton = 43 Then
                PermissionEdit = True
            End If

        Next

        If PermissionAllFunctionalUnit = False Then
            Presenter.FunctionalUnit_DatasourceUser(Indigo.UserIndigoId)
        End If

    End Sub


    ''' <summary>
    ''' Evento que controla cuando se seleccionan unidades funcionales
    ''' </summary>
    Private Async Sub INDSleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFunctionalUnit.EditValueChanged
        Try
            AsyncLoader(True)
            If INDSleFunctionalUnit.EditValue IsNot Nothing And INDSleFunctionalUnit.EditValue IsNot "" Then
                If FlagConsultEmployee = False Then
                    _ScheduleIdListChecked = New List(Of Integer)
                    ListEmployee.Items.Clear()
                    Presenter.Clean_Schedule_Detail_Calendar()
                    INDBarMarkEmployee.Checked = False
                End If
                'AsyncLoader(True)
                Using model As New MFunctionalUnit(MFunctionalUnit.TAG)
                    FunctionalUnit = Await model.GetFuncUnitAsync(INDSleFunctionalUnit.EditValue)
                    If FunctionalUnit Is Nothing Then
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    Await Me.Presenter.Load_Employee(PermissionAllPosition, Indigo)
                End Using
                INDDDBFunctionalUnit.Text = FunctionalUnit.Name & " - " & FunctionalUnit.BranchOffice.Name

                If FlagConsultEmployee = True Then
                    CheckearUsuario()
                End If

            End If
            FlagConsultEmployee = False
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    Private Sub CheckearUsuario()
        Try

            If EmployeeTmp.Id > 0 Then
                For i As Integer = 0 To INDChkLbcEmployee.ItemCount - 1
                    If INDChkLbcEmployee.Items(i).Value = EmployeeTmp.Id Then
                        INDChkLbcEmployee.Items(i).CheckState = System.Windows.Forms.CheckState.Checked
                        'FlagConsultEmployee = True
                    End If
                Next
            End If

        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que controla los check de los usuarios en el list box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDChkLbcEmployee_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs)

        If e IsNot Nothing Then
            If e.State = System.Windows.Forms.CheckState.Checked Then
                ScheduleIdListChecked.Add(INDChkLbcEmployee.Items.Item(e.Index).Value)
            ElseIf e.State = System.Windows.Forms.CheckState.Unchecked Then
                ScheduleIdListChecked.Remove(INDChkLbcEmployee.Items.Item(e.Index).Value)
            End If
        End If

        RemoveHandler INDBarMarkEmployee.CheckedChanged, AddressOf INDBarMarkEmployee_CheckedChanged
        If ScheduleIdListChecked.Count = INDChkLbcEmployee.Items.Count Then
            INDBarMarkEmployee.Checked = True
        Else
            INDBarMarkEmployee.Checked = False
        End If
        AddHandler INDBarMarkEmployee.CheckedChanged, AddressOf INDBarMarkEmployee_CheckedChanged

        If ScheduleIdListChecked.Count > 0 Then
            If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Eliminar) = True Then
                INDBarDeleteAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                If ScheduleIdListChecked.Count < 3 Then
                    INDBarMarkAsDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If
        End If

        Select Case ScheduleIdListChecked.Count
            'no hay empelados seleccionados
            Case Is = 0
                INDBarDeleteAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                INDBarMarkAsDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ViewGridSchedule = False
                Presenter.Clean_Schedule_Detail_Calendar()
                Exit Sub

                '1 empleado seleccionado, se muestra en el calendar
            Case Is = 1
                AsyncLoader(True)
                Await Presenter.Load_ScheduleDetailComplete(ScheduleIdListChecked)
                Presenter.Load_Schedule_Detail_Calendar(ScheduleIdListChecked)
                ViewGridSchedule = False
                AsyncLoader(False)

                '2 empleados seleccionados, se muestran en el calendar y se comparan
            Case Is = 2
                AsyncLoader(True)
                Await Presenter.Load_ScheduleDetailComplete(ScheduleIdListChecked)
                Presenter.Load_Schedule_Detail_Calendar(ScheduleIdListChecked)
                ViewGridSchedule = False
                AsyncLoader(False)

                'mas de 2 empleados seleccionados, se muestra en la rejilla
            Case Is > 2
                If INDLyItemScheduleDetailGrid1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                    ViewGridSchedule = True
                    AsyncLoader(True)
                    Await Presenter.Load_ScheduleDetailComplete(ScheduleIdListChecked)
                    Presenter.Load_Schedule_Detail_Grid()
                    AsyncLoader(False)
                Else
                    AsyncLoader(True)
                    Await Presenter.Load_ScheduleDetailComplete(ScheduleIdListChecked)
                    Presenter.Load_Schedule_Detail_Grid()
                    AsyncLoader(False)
                End If

        End Select

    End Sub

    Private Async Function ValidateScheduleBlock(group As Group) As Task(Of Boolean)

        If PermissionAllFunctionalUnit = True Then
            Return True
        End If

        Dim BlockInitialDate As DateTime
        Dim BlockEndDate As DateTime
        Dim DateSelected As New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, IIf(CtrCalendar.DaySelected = 0, 1, CtrCalendar.DaySelected))

        InitialDatePayroll = group.NextDateLiquidation

        Dim InitialDate As New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, 1)
        Dim EndDate As New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, Date.DaysInMonth(CtrCalendar.YearControl, CtrCalendar.MonthControl))

        If InitialDate <= InitialDatePayroll And InitialDatePayroll <= EndDate Then

            If INDSleFunctionalUnit.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado ninguna Unidad Funcional"
                AsyncLoader(False)
                Return False
            End If

            Using model As New MFunctionalUnit(MFunctionalUnit.TAG)
                FunctionalUnit = Await model.GetFuncUnitAsync(INDSleFunctionalUnit.EditValue)
                If FunctionalUnit Is Nothing Then
                    AsyncLoader(False)
                End If

                Using modelblock As New MBlockSchedule("")

                    ServerDate = Await modelblock.GetServerDate()

                    Dim ListBlock = Await modelblock.ListBlockScheduleAsync()

                    If ListBlock IsNot Nothing Then

                        Dim ObjBlockScheduleC = ListBlock.Item1

                        If ObjBlockScheduleC IsNot Nothing Then

                            If ObjBlockScheduleC.BlockType = 1 Then

                                If (group.Liquidation = 1 And ObjBlockScheduleC.PayrollType = 2) Or (group.Liquidation = 2 And ObjBlockScheduleC.PayrollType = 1) Then
                                    Mensaje(EeventViewerImages.Advertencia) = "El tipo de Nómina no coincide con el bloqueo que se ha parametrizado"
                                End If

                                'Es automático, va por Fechas
                                If ObjBlockScheduleC.PayrollType = 1 Then
                                    'Nómina Mensual
                                    BlockInitialDate = New DateTime(InitialDatePayroll.Year, InitialDatePayroll.Month, ObjBlockScheduleC.MonthBlockDay, ObjBlockScheduleC.MonthInitialBlockTime.Value.Hours, ObjBlockScheduleC.MonthInitialBlockTime.Value.Minutes, ObjBlockScheduleC.MonthInitialBlockTime.Value.Seconds)
                                    BlockEndDate = New DateTime(InitialDatePayroll.Year, InitialDatePayroll.Month, Date.DaysInMonth(InitialDatePayroll.Year, InitialDatePayroll.Month), 23, 59, 59)

                                Else

                                    If InitialDatePayroll.Day = 1 Then
                                        'Primera Quincena
                                        BlockInitialDate = New DateTime(InitialDatePayroll.Year, InitialDatePayroll.Month, ObjBlockScheduleC.FirstFortnightDayBlockTime, ObjBlockScheduleC.FirstFortnighHourBlockTyme.Value.Hours, ObjBlockScheduleC.FirstFortnighHourBlockTyme.Value.Minutes, ObjBlockScheduleC.FirstFortnighHourBlockTyme.Value.Seconds)
                                        BlockEndDate = New DateTime(InitialDatePayroll.Year, InitialDatePayroll.Month, 15, 23, 59, 59)
                                    Else
                                        'Segunda Quincena
                                        BlockInitialDate = New DateTime(InitialDatePayroll.Year, InitialDatePayroll.Month, ObjBlockScheduleC.SecondFortnightDayBlockTime, ObjBlockScheduleC.SecondFortnighHourBlockTyme.Value.Hours, ObjBlockScheduleC.SecondFortnighHourBlockTyme.Value.Minutes, ObjBlockScheduleC.SecondFortnighHourBlockTyme.Value.Seconds)
                                        BlockEndDate = New DateTime(InitialDatePayroll.Year, InitialDatePayroll.Month, Date.DaysInMonth(InitialDatePayroll.Year, InitialDatePayroll.Month), 23, 59, 59)

                                    End If

                                End If

                                If ServerDate >= BlockInitialDate And ServerDate <= BlockEndDate And DateSelected <= BlockEndDate Then
                                    Mensaje(EeventViewerImages.Advertencia) = "La Unidad Funcional se encuentra bloqueada. No se puede modificar el Cuadro de Turnos"
                                    AsyncLoader(False)
                                    Return False
                                End If

                            Else
                                    'Bloque por Unidad Funcional
                                    If ListBlock.Item2.Any(Function(x) x.FunctionalUnitId = FunctionalUnit.Id) Then
                                    Mensaje(EeventViewerImages.Advertencia) = "La Unidad Funcional se encuentra bloqueada. No se puede modificar el Cuadro de Turnos"
                                    AsyncLoader(False)
                                    Return False
                                End If
                            End If

                        End If

                    End If

                End Using


                'If FunctionalUnit.BlockSchedule.Count > 0 AndAlso PermissionAllFunctionalUnit = False Then
                '    AsyncLoader(False)

                'End If

            End Using

        End If

        Return True

    End Function

    ''' <summary>
    ''' Evento que se dispara cuando se cambia de fecha
    ''' </summary>
    Private Async Sub Change_Date(sender As Object, e As EventArgs)
        'AsyncLoader(True)
        Await RefreshForm()
        'AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo para refrescar el schedule
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function RefreshForm() As Task
        'AsyncLoader(True)
        Presenter.Load_Holidays()
        If INDSleFunctionalUnit.EditValue IsNot Nothing Then
            ListEmployee.Items.Clear()
            Presenter.Clean_Schedule_Detail_Calendar()
            AsyncLoader(True)
            Await Me.Presenter.Load_Employee(PermissionAllPosition, Indigo)
            AsyncLoader(False)
            SetValuesAgain()
        End If
        'AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Evento para controlar el click sobre los dias y seleccionar el dia
    ''' </summary>
    Private Async Sub Click_Days(sender As Object, e As EventArgs)

        If CtrCalendar.ModeDelete = False Then
            Dim ThisLabel As LabelControl = sender
            Presenter.HighlightNumDay(ThisLabel.Tag)
            Dim NumDay As Integer = ThisLabel.Tag
            If ScheduleIdListChecked.Count > 0 Then
                Await Process_Save_ScheduleDetail(NumDay, ScheduleIdListChecked, ScheduleIdListChecked.Item(0))
            End If

            If ScheduleIdListChecked.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado ningún empleado"
                Exit Sub
            End If

        End If
    End Sub

    ''' <summary>
    ''' Evento que controla el doble click en el dia del calendario para mostrar popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DoubleClick_Days(sender As Object, e As EventArgs)
    End Sub

    ''' <summary>
    ''' Evento que controla el mouse hover en los turnos de mas que tenga el empleado
    ''' </summary>
    Public Async Sub OnMouseHoverMore(ByVal sender As DevExpress.XtraEditors.LabelControl, ByVal e As EventArgs)
        If CtrCalendar.ModeDelete = False AndAlso ScheduleIdListChecked.Count = 1 AndAlso sender.Text <> "" Then
            CtrCalendar.HighLightPopUpDayClose(Nothing, EventArgs.Empty)
            Clean_Values_Popup_More()
            Dim list_in_other_FU As List(Of ScheduleDetailInOtherFU)
            Dim PanelControl As PanelControl = sender.Parent
            PopUpMoreDropDownButton = PanelControl.Controls.Item("DDB" & PanelControl.Tag)
            PopUpMoreDropDownButton.HideDropDown()
            Dim EmployeeName As String = ScheduleN1.Employee.ThirdParty.Name
            If ScheduleInPeriodN1 IsNot Nothing AndAlso ScheduleInPeriodN1.Count > 0 Then
                list_in_other_FU = New List(Of ScheduleDetailInOtherFU)
                Dim totHours As Decimal = 0
                For Each SDet As ScheduleDetail In ScheduleInPeriodN1.FindAll(Function(x) x.ScheduleFunctionalUnitId <> FunctionalUnit.Id And x.DateDetail = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, PanelControl.Tag))
                    Using mFU As New MFunctionalUnit(MFunctionalUnit.TAG)
                        Dim tempFU As FunctionalUnit = Await mFU.GetFunctionalUnitByIdAsync(SDet.ScheduleFunctionalUnitId)
                        If tempFU IsNot Nothing Then
                            list_in_other_FU.Add(New ScheduleDetailInOtherFU() With {.DetMoreFU = tempFU.Name & " - " & tempFU.BranchOffice.Name, .DetMoreT = GetNameTemplate(SDet.Letter)})
                        Else
                            Exit Sub
                        End If
                    End Using
                    totHours += SDet.TotalNumberHours
                Next
                If list_in_other_FU.Count > 0 Then
                    'Inicializar Valores en el pop up
                    PopUpMoreEmployeeName = EmployeeName
                    PopupMoreNumDay = sender.Tag
                    PopUpMoreTemplateHoursNumber = totHours
                    PopUpMoreDate = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, sender.Tag)
                    PopUpMoreSchDet = list_in_other_FU
                    PopUpMoreDropDownButton.ShowDropDown()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla el mouse hover en los iconos de los schedule
    ''' </summary>
    Public Async Sub OnMouseHoverDays(ByVal sender As Object, ByVal e As EventArgs)
        If CtrCalendar.ModeDelete = False Then
            CtrCalendar.HighLightPopUpDayClose(Nothing, EventArgs.Empty)
            Clean_Values_Popup_Detail()
            Dim _Control As System.Windows.Forms.Control = sender
            Dim PanelControl As PanelControl = _Control.Parent
            PopUpDropDownButton = PanelControl.Controls.Item("DDB" & PanelControl.Tag)
            PopUpDropDownButton.HideDropDown()
            Dim NormalView As Boolean = True
            Dim NumEmployee As Integer
            Dim MatesView As Boolean = False 'si se muestran los compañeros de turno o los turnos en otra unidad funcional
            If _Control.GetType().ToString = "DevExpress.XtraEditors.LabelControl" Then
                If _Control.Text = "" Then
                    Exit Sub
                End If
            End If
            If _Control.Tag IsNot "" Or ScheduleN1 IsNot Nothing Then
                If _Control.Tag IsNot "" Then
                    NumEmployee = _Control.Tag
                Else
                    NumEmployee = 0
                End If
                Dim NumDay As Integer = PanelControl.Tag
                Dim EmployeeName As String = ""
                Dim EmployeeId As Integer
                Dim EmployeeColor As System.Drawing.Color
                Dim ScheduleDetailCurrent As ScheduleDetail
                If NumEmployee = 1 Then
                    ScheduleDetailCurrent = DictionaryScheduleDetail_First(NumDay)
                    EmployeeName = ScheduleN1.Employee.ThirdParty.Name
                    EmployeeColor = CtrCalendar.RegColor1
                    EmployeeId = ScheduleN1.EmployeeId
                    PopUpEmployeeNumber = "1"
                ElseIf NumEmployee = 2 Then
                    ScheduleDetailCurrent = DictionaryScheduleDetail_Second(NumDay)
                    EmployeeName = ScheduleN2.Employee.ThirdParty.Name
                    EmployeeColor = CtrCalendar.RegColor2
                    EmployeeId = ScheduleN2.EmployeeId
                    PopUpEmployeeNumber = "2"
                Else
                    ScheduleDetailCurrent = DictionaryScheduleDetail_First(NumDay)
                    EmployeeName = ScheduleN1.Employee.ThirdParty.Name
                    EmployeeColor = System.Drawing.Color.White
                    EmployeeId = ScheduleN1.EmployeeId
                    PopUpEmployeeNumber = "1"
                End If
                If ScheduleDetailCurrent IsNot Nothing Then
                    PopUpDropDownButton.ShowDropDown()
                    Using ModelSchedule As New MSchedule
                        ScheduleDetailCurrent = Await ModelSchedule.GetScheduleDetail(ScheduleDetailCurrent.Id)
                    End Using
                    If ScheduleDetailCurrent IsNot Nothing Then
                        'Muestro el pop up, y asincronamente cargo los demas valores
                        Dim _scheduleTemplateTemp As ScheduleTemplate
                        Dim _listMatchesSchedule As New List(Of Schedule)
                        If ScheduleDetailCurrent.ScheduleTemplateId IsNot Nothing Then ' cuando es un turno normal
                            Using model As New MScheduleControl
                                _scheduleTemplateTemp = Await model.GetScheduleTemplateByIdAsync(ScheduleDetailCurrent.ScheduleTemplateId)
                            End Using
                        Else ' cuando es una novedad
                            NormalView = False
                            Dim NameTemplate As String
                            INDTcgDetailsSchedule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never 'oculto los tabs de compañeros de turno y detalle de horas
                            Select Case ScheduleDetailCurrent.Letter
                                Case Is = "I"
                                    NameTemplate = "Incapacidad"
                                Case Is = "L"
                                    NameTemplate = "Licencia"
                                Case Is = "S"
                                    NameTemplate = "Sanción"
                                Case Else
                                    NameTemplate = ""
                            End Select
                            _scheduleTemplateTemp = New ScheduleTemplate() With {.Letter = ScheduleDetailCurrent.Letter, .Name = NameTemplate}
                        End If

                        If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Actualizar) = False Then ' valido que tenga permisos para modificar, y mostrar el boton de editar
                            NormalView = False
                        End If

                        Using model As New MSchedule
                            _listMatchesSchedule = model.GetScheduleByPeriodDay(Period, NumDay, FunctionalUnit.Id)
                            For Each item As Schedule In _listMatchesSchedule.FindAll(Function(x) x.EmployeeId = EmployeeId)
                                _listMatchesSchedule.Remove(item)
                            Next
                        End Using

                        'Inicializar Valores en el pop up
                        PopUpVisibilityButtonEdit = NormalView
                        PopUpEmployeeName = EmployeeName
                        PopupNumDay = NumDay
                        PopUpLetter = _scheduleTemplateTemp.Letter
                        PopUpTemplateHoursNumber = ScheduleDetailCurrent.TotalNumberHours
                        PopUpDate = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, NumDay)
                        PopUpMatches = _listMatchesSchedule
                        PopUpDetHours = ScheduleDetailCurrent.ScheduleDetailHour
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento Click para editar un turno
    ''' </summary>
    Private Async Sub INDPopupSBEdit_Click(sender As Object, e As EventArgs) Handles INDPopupSBEdit.Click
        PopUpDropDownButton.HideDropDown()
        Await EditScheduleDetail(_popupEmployeeNumber, _popupNumDay, ScheduleIdListChecked)
    End Sub

    ''' <summary>
    ''' Evento click del boton de las unidades funcionales para desplegar pop up
    ''' </summary>
    Private Sub INDDDBFunctionalUnit_Click(sender As Object, e As EventArgs) Handles INDDDBFunctionalUnit.Click
        If INDSleFunctionalUnit.IsPopupOpen = True Then
            INDSleFunctionalUnit.ClosePopup()
        Else
            INDSleFunctionalUnit.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento para mostrar el detalle de horas en pop up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDLcTotalHoursNumber_MouseHover(sender As Object, e As EventArgs) Handles INDLcTotalHoursNumber1.MouseHover, INDLcTotalHoursNumber2.MouseHover, INDLcTotalHours1.MouseHover, INDLcTotalHours2.MouseHover
        Clean_Values_PopupHours()
        Dim _control As System.Windows.Forms.Control = sender
        Dim _Detail_NumEmployee As Integer = _control.Tag
        Dim _schedule As Schedule
        Dim _listScheduleDetail As List(Of ScheduleDetail)
        Dim DropDownButton As DevExpress.XtraEditors.DropDownButton
        Select Case _Detail_NumEmployee
            Case Is = 1
                _schedule = ScheduleN1

                If ScheduleN1.TotalHour > 0 Then
                    If ScheduleInPeriodN1 IsNot Nothing Then
                        _listScheduleDetail = ScheduleInPeriodN1
                    End If
                End If
                DropDownButton = INDDDBEmployee1
                DropDownButton.HideDropDown()
            Case Is = 2
                _schedule = ScheduleN2
                If ScheduleN2.TotalHour > 0 Then
                    If ScheduleInPeriodN2 IsNot Nothing Then
                        _listScheduleDetail = ScheduleInPeriodN2 '.FindAll(Function(x) x.ScheduleFunctionalUnitId = FunctionalUnit.Id)
                    End If
                End If
                DropDownButton = INDDDBEmployee2
                DropDownButton.HideDropDown()
        End Select

        Dim totHours As Decimal = 0
        Dim _contract As Contract = _schedule.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault ' obtengo el contrato del empleado
        Dim _workedPercentage As Decimal = 0
        'Bloque para sacar el listado de las horas que se le pagan con su respectivo concepto
        Dim _listDetailHour As List(Of ScheduleDetailHour) = New List(Of ScheduleDetailHour)
        Dim _listDetailConcept As List(Of ScheduleDetailConcept) = New List(Of ScheduleDetailConcept)
        Dim _listDetailHoursByConcept As List(Of DetailHoursByConcept) = New List(Of DetailHoursByConcept) ' listado para las horas normales
        Dim _listDetailHoursByConceptPending As List(Of DetailHoursByConcept) = New List(Of DetailHoursByConcept) 'listado para las horas pendientes
        If _listScheduleDetail IsNot Nothing Then


            For Each item As ScheduleDetail In _listScheduleDetail 'primero recorro todos los turnos
                For Each itemHour As ScheduleDetailHour In item.ScheduleDetailHour 'recorro cada detalle de horario
                    'Validacion para no sumar las horas de los conceptos que no esten aprobados
                    If itemHour.Event = False Then
                        totHours += itemHour.TotalNumberHours
                    ElseIf itemHour.Approved = True Then
                        totHours += itemHour.TotalNumberHours
                    End If

                    If itemHour.AppliedLiquidationConcept = True Then
                        For Each itemConcept As ScheduleDetailConcept In itemHour.ScheduleDetailConcept.Where(Function(x) x.ConceptType = 1)
                            _listDetailConcept.Add(itemConcept)
                        Next
                    Else
                        For Each itemConcept As ScheduleDetailConcept In itemHour.ScheduleDetailConcept.Where(Function(x) x.ConceptType = 0)
                            _listDetailConcept.Add(itemConcept)
                        Next
                    End If
                    _listDetailHour.Add(itemHour)
                Next
            Next

            'Asigno los conceptos con sus horas respectivas a la entidad nueva <DetailHoursByCOncept>
            For Each _dc As ScheduleDetailConcept In _listDetailConcept
                If _dc.ScheduleDetailHour.Event = True AndAlso _dc.ScheduleDetailHour.Approved = False Then 'agrego al listado de horas pendientes
                    Dim _detailHoursByConcept As DetailHoursByConcept
                    _detailHoursByConcept = _listDetailHoursByConceptPending.Find(Function(x) x.ConceptId = _dc.ConceptId)
                    If _detailHoursByConcept IsNot Nothing Then
                        _detailHoursByConcept.HoursNumber += _dc.ScheduleDetailHour.TotalNumberHours
                    Else
                        _detailHoursByConcept = New DetailHoursByConcept
                        With _detailHoursByConcept
                            .ConceptId = _dc.ConceptId
                            .ConceptName = _dc.Concept.Name
                            .HoursNumber = _dc.ScheduleDetailHour.TotalNumberHours
                        End With
                        _listDetailHoursByConceptPending.Add(_detailHoursByConcept)
                    End If
                Else 'agrego al listado de horas normales
                    Dim _detailHoursByConcept As DetailHoursByConcept
                    _detailHoursByConcept = _listDetailHoursByConcept.Find(Function(x) x.ConceptId = _dc.ConceptId)
                    If _detailHoursByConcept IsNot Nothing Then
                        _detailHoursByConcept.HoursNumber += _dc.ScheduleDetailHour.TotalNumberHours
                    Else
                        _detailHoursByConcept = New DetailHoursByConcept
                        With _detailHoursByConcept
                            .ConceptId = _dc.ConceptId
                            .ConceptName = _dc.Concept.Name
                            .HoursNumber = _dc.ScheduleDetailHour.TotalNumberHours
                        End With
                        _listDetailHoursByConcept.Add(_detailHoursByConcept)
                    End If
                End If
            Next

            'porcentaje de trabajo
            _workedPercentage = (totHours * 100) / _contract.Position.MinHourAmount
            If _workedPercentage > 100 Then
                _workedPercentage = 100
            End If
            _workedPercentage = Decimal.Round(_workedPercentage, 2)

        End If
        'Inicializo los valores en el pop up de detalle de horas
        PopupHoursEmployee = _schedule.Employee.ThirdParty.Name
        PopupHoursMinNumber = _contract.Position.MinHourAmount.ToString
        PopupHoursMaxNumber = _contract.Position.MaxHourAmount.ToString
        PopupHoursWorkedPercentage = _workedPercentage.ToString
        PopupHoursWorkedNumber = totHours.ToString
        PopupHoursDetailDatasource = _listDetailHoursByConcept
        PopupHoursDetailPendingDatasource = _listDetailHoursByConceptPending
        DropDownButton.ShowDropDown()
    End Sub

    ''' <summary>
    ''' Evento para establecer los colores en la lista de los empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDChkLbcEmployee_DrawItem(sender As Object, e As ListBoxDrawItemEventArgs) Handles INDChkLbcEmployee.DrawItem
        'Dim _schedule_Id = ScheduleDatasource.Find(Function(x) x.EmployeeId = INDChkLbcEmployee.Items.Item(e.Index).Value).Id
        'Dim _colorPar As System.Drawing.Color = System.Drawing.Color.White
        'Dim _colorImpar As System.Drawing.Color = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        'e.Appearance.ForeColor = System.Drawing.Color.DimGray
        'If ScheduleIdListChecked.Count > 2 Then
        '    e.Appearance.ForeColor = System.Drawing.Color.DimGray
        '    If e.Index Mod 2 = 0 Then
        '        e.Appearance.BackColor = _colorPar
        '    Else
        '        e.Appearance.BackColor = _colorImpar
        '    End If
        'Else
        '    'If e.State = System.Windows.Forms.DrawItemState.Selected Then
        '    '    e.Appearance.ForeColor = System.Drawing.Color.White
        '    '    e.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        '    'End If
        'End If

    End Sub

    ''' <summary>
    ''' Evento para hacer el resize del pop up de las unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPcMainControls_Resize(sender As Object, e As EventArgs) Handles INDPcMainControls.Resize
        INDSleFunctionalUnit.Properties.PopupFormSize = New System.Drawing.Size(INDPcMainControls.Width, 280)
    End Sub

    ''' <summary>
    ''' Evento que Coloca las letras en cada registro schedule 
    ''' </summary>
    Private Sub INDGvScheduleDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvScheduleDetail1.CustomColumnDisplayText
        'Pintar letra del turno
        For i As Integer = 1 To 31
            Dim fieldName As String
            If i = 1 Then
                fieldName = "ScheduleDetail"
            Else
                fieldName = "ScheduleDetail" & i - 1
            End If

            'mostrar letras
            If e.ListSourceRowIndex > -1 Then
                Dim _sch As Schedule = INDGvScheduleDetail1.GetRow(e.ListSourceRowIndex)
                If e.Column.FieldName = fieldName And e.Value IsNot Nothing Then

                    If _sch.Apply = True Then
                        e.DisplayText = CType(e.Value, ScheduleDetail).Letter
                    Else
                        e.DisplayText = " "
                    End If
                End If

                If e.Column.Name = "INDColTHour" Then
                    If _sch.Apply = True Then
                        e.DisplayText = _sch.TotalHour
                    Else
                        e.DisplayText = ""
                    End If
                End If
            End If
            'mostrar solo los seleccionados
        Next

        'Reclacular Total de Horas
        If e.Column.FieldName = "TotalHour" Then
            Dim THours As Decimal = 0
            For i As Integer = 1 To 31
                Dim fieldName As String
                If i = 1 Then
                    fieldName = "ScheduleDetail"
                Else
                    fieldName = "ScheduleDetail" & i - 1
                End If
                Dim _detail As ScheduleDetail = INDGvScheduleDetail1.GetRowCellValue(e.ListSourceRowIndex, fieldName)
                If _detail IsNot Nothing Then
                    If _detail.ScheduleTemplateId > 0 Then
                        THours += _detail.TotalNumberHours
                    End If
                End If
            Next

            If e.ListSourceRowIndex > -1 Then
                Dim _sch As Schedule = INDGvScheduleDetail1.GetRow(e.ListSourceRowIndex)
                If _sch IsNot Nothing Then
                    If _sch.Apply = True Then
                        e.DisplayText = THours
                    Else
                        e.DisplayText = " "
                    End If
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' Evento para abrir pop up de registro de un scheduleDetail cuando se da doble click en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDGvScheduleDetail_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvScheduleDetail1.RowCellClick
        If e.Clicks = 2 Then
            If Not (e.Column.Name = "Apply" Or e.Column.Name = "INDColEmployee" Or e.Column.Name = "INDColLocation" Or e.Column.Name = "INDColTHour") Then
                Dim NumDay As Integer = e.Column.Tag
                Dim _schedule As Schedule = INDGvScheduleDetail1.GetRow(e.RowHandle)
                Await Process_Save_ScheduleDetail(NumDay, ScheduleIdListChecked, _schedule.EmployeeId)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para controlar cuando seleccionen empleados en la vista rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvScheduleDetail1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles INDGvScheduleDetail1.CellValueChanging
        If e.Column.Name = "Apply" Then

            Dim _sch As Schedule = INDGvScheduleDetail1.GetRow(e.RowHandle)

            Dim _val As Object = _sch.EmployeeId
            _sch.Apply = e.Value
            If e.Value = False Then
                INDChkLbcEmployee.Items.Item(_val).CheckState = System.Windows.Forms.CheckState.Unchecked

            Else
                INDChkLbcEmployee.Items.Item(_val).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End If
        INDGvScheduleDetail1.FocusedColumn = INDGvScheduleDetail1.VisibleColumns(2)
    End Sub

    ''' <summary>
    ''' Evento Click para cancelar el marcar para eliminar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDDelSBCancel_Click(sender As Object, e As EventArgs) Handles INDDelSBCancel.Click
        ActionOnControlsDelete = False
    End Sub

    ''' <summary>
    ''' Evento para eliminar los turnos seleccionados por persona
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDDelSBFinish_Click(sender As Object, e As EventArgs) Handles INDDelSBFinish.Click
        If Await DeleteScheduleDetail(ScheduleIdListChecked, CtrCalendar.ListDaysToDelete) Then
            Await Me.Presenter.Load_Employee(PermissionAllPosition, Indigo)
            SetValuesAgain()
            ActionOnControlsDelete = False
            'INDBarMarkEmployee.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Evento Click para eliminar todos los schedule de los empleados que se tengan seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBarDeleteAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarDeleteAll.ItemClick
        Dim listDaysToDelete As List(Of Integer) = New List(Of Integer)
        For i = 1 To CtrCalendar.DaysInThisMonth
            listDaysToDelete.Add(i)
        Next

        For Each _int As Integer In ScheduleIdListChecked
            Dim _thisSch As Schedule = ScheduleDatasource.Find(Function(x) x.EmployeeId = _int)
            Dim _contract As Contract = _thisSch.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
            Dim ContractLastDateLiquidation As Date?

            'Es porque es renovación
            If _contract.RowType = 2 Then
                ContractLastDateLiquidation = _thisSch.Employee.Contract.Select(Function(x) x.LastLiquidationDate).Max()
            Else
                ContractLastDateLiquidation = _contract.LastLiquidationDate
            End If


            Dim NewDate As Date = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, 1)
            Dim NewDateMonth As Date = DateAdd(DateInterval.Month, 1, NewDate)
            Dim DateComparision As Date = DateAdd(DateInterval.Day, -1, NewDateMonth)


            If ContractLastDateLiquidation IsNot Nothing AndAlso DateComparision <= ContractLastDateLiquidation Then
                _NumWrongEmployee += 1
                If _NumWrongEmployee = 1 Then
                    _employeeWrong = _thisSch.Employee.ThirdParty.Name
                End If
            End If

            If _NumWrongEmployee > 0 Then
                If _NumWrongEmployee > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadosLiquidados, CuadroDeTurno), _employeeWrong, (_NumWrongEmployee - 1))
                    Exit Sub
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadoLiquidado, CuadroDeTurno), _employeeWrong)
                    Exit Sub
                End If
            End If
        Next


        If Await DeleteScheduleDetail(ScheduleIdListChecked, listDaysToDelete) Then
            Await Me.Presenter.Load_Employee(PermissionAllPosition, Indigo)
            SetValuesAgain()
        End If

    End Sub

    ''' <summary>
    ''' Evento para seleccionar o deseleccionar los registros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBarMarkEmployee_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarMarkEmployee.CheckedChanged
        RemoveHandler INDChkLbcEmployee.ItemCheck, AddressOf INDChkLbcEmployee_ItemCheck
        If INDBarMarkEmployee.Checked = True Then
            INDChkLbcEmployee.CheckAll()
            _ScheduleIdListChecked = New List(Of Integer)
            For i As Integer = 0 To INDChkLbcEmployee.Items.Count - 1
                _ScheduleIdListChecked.Add(INDChkLbcEmployee.Items.Item(i).Value)
            Next
        Else
            _ScheduleIdListChecked = New List(Of Integer)
            INDChkLbcEmployee.UnCheckAll()
        End If
        INDChkLbcEmployee_ItemCheck(INDChkLbcEmployee, Nothing)
        AddHandler INDChkLbcEmployee.ItemCheck, AddressOf INDChkLbcEmployee_ItemCheck
    End Sub

    ''' <summary>
    ''' Evento Click del menu de opciones para eliminar seleccionar turnos a eliminar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBarMarkAsDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarMarkAsDelete.ItemClick
        ActionOnControlsDelete = True
    End Sub

    Private Sub INDBarEmployee_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarEmployee.ItemClick
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.AllEmployees
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Id", .FieldName = "Id", .Visible = False}, New ColumnInfo() With {.Caption = "Cedula", .FieldName = "Nit"}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="VarReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Function ReturnValue(ByVal VarReturnValue As Integer, ByVal ReturnObject As Object) As Task

        Try
            AsyncLoader(True)
            Using model As New MSchedule
                EmployeeTmp = Await model.GetEmployeeByIdForContractLiquidation(VarReturnValue)
            End Using

            If EmployeeTmp IsNot Nothing Then
                If EmployeeTmp.Contract IsNot Nothing And EmployeeTmp.Contract.Count > 0 Then

                    Dim ObjContract = EmployeeTmp.Contract.Where(Function(x) x.Valid = True And x.Status = 1).FirstOrDefault()

                    INDSleFunctionalUnit.EditValue = Nothing
                    INDSleFunctionalUnit.EditValue = ObjContract.FunctionalUnit.Code

                End If

                FlagConsultEmployee = True
            End If
            AsyncLoader(False)

        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Function

    ''' <summary>
    ''' Evento para controlar las columnas que son dias festivos y dominicales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvScheduleDetail1_CustomDrawColumnHeader(sender As Object, e As DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs) Handles INDGvScheduleDetail1.CustomDrawColumnHeader
        Dim Col As DevExpress.XtraGrid.Columns.GridColumn
        Col = e.Column
        If Col IsNot Nothing Then
            If Col.Tag > 0 Then
                If Find_Holiday(Col.Tag) = True Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que refresca el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        Await RefreshForm()
    End Sub

    ''' <summary>
    ''' Evento que captura el cambio en el tamaño del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSchedule1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim _height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height
        If _height < 900 Then
            SetLittleControlsDetail = True
        Else
            SetLittleControlsDetail = False
        End If
    End Sub

    ''' <summary>
    ''' Evento para escribir la hora inicio y fin en la rejilla de detalle de horas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvDetHours_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvDetHours.CustomColumnDisplayText
        Dim _thisRow As ScheduleDetailHour = INDgvDetHours.GetRow(e.ListSourceRowIndex)
        If _thisRow IsNot Nothing Then
            If e.Column.Name = INDColStart.Name Then
                e.DisplayText = _thisRow.DateTimeInitial.ToString("h:mm tt", Indigo.Culture) '_thisRow.DateTimeInitial.TimeOfDay 
            End If
            If e.Column.Name = INDColEnd.Name Then
                e.DisplayText = _thisRow.DateTimeEnding.ToString("h:mm tt", Indigo.Culture)
            End If
        End If
    End Sub

#End Region

#Region "Methods"



    ''' <summary>
    ''' Obtener el nombre de la plantilla por la letra
    ''' </summary>
    ''' <param name="letter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNameTemplate(letter As String) As String
        Select Case letter
            Case Is = "M"
                Return obtenerRecurso(Manana, CuadroDeTurno)
            Case Is = "MT"
                Return obtenerRecurso(MananaTarde, CuadroDeTurno)
            Case Is = "T"
                Return obtenerRecurso(Tarde, CuadroDeTurno)
            Case Is = "MN"
                Return obtenerRecurso(MananaNoche, CuadroDeTurno)
            Case Is = "TN"
                Return obtenerRecurso(TardeNoche, CuadroDeTurno)
            Case Is = "N"
                Return obtenerRecurso(Noche, CuadroDeTurno)
            Case Is = "I"
                Return obtenerRecurso(Eresources.Incapacidad, CuadroDeTurno)
            Case Is = "L"
                Return obtenerRecurso(Licencia, CuadroDeTurno)
            Case Is = "S"
                Return obtenerRecurso(Eresources.Sancion, CuadroDeTurno)
            Case Is = "V"
                Return obtenerRecurso(Eresources.Vacaciones, CuadroDeTurno)
            Case Is = "PV"
                Return obtenerRecurso(PermisoVacaciones, CuadroDeTurno)


        End Select
    End Function

    ''' <summary>
    ''' Método que valida 
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateLiquidationOfEmployees()

    End Function

    ''' <summary>
    ''' Metodo que contiene el procedimiento que se realiza cuando se quiere va a crear un turno
    ''' </summary>
    ''' <param name="numDay">numero del dia</param>
    Public Async Function Process_Save_ScheduleDetail(ByVal numDay As Integer, ByVal idSEmployee As List(Of Integer), employeeSelected As Integer) As Task
        If ScheduleIdListChecked.Count > 0 Then

            Dim ScheduleDetailCurrent As ScheduleDetail
            If idSEmployee.Count = 1 Then
                ScheduleDetailCurrent = DictionaryScheduleDetail_First(numDay)
                If ScheduleDetailCurrent IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ErrorScheduleDetailExisting, CuadroDeTurno)
                    Exit Function
                End If
            Else
                For Each _int As Integer In ScheduleIdListChecked
                    For i As Integer = 0 To ScheduleComplete.Count - 1
                        Dim _listSchDet As List(Of ScheduleDetail) = ScheduleComplete.Item(i)
                        Dim Var = From X In _listSchDet Where X.EmployeeId = _int And X.DateDetail = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, numDay)
                                  Select X
                        If Var.Count > 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = ("Para esta Fecha Alguno de los empleados seleccionados cuenta con turno, deselecciónelo de la rejilla e intente nuevamente")
                            Exit Function
                        End If
                    Next
                Next
            End If

            'Valido las fechas maximas y minimas a para validar las fechas de frecuencia en el form de registrar turnos
            Dim _maxDate As Date
            Dim _minDate As Date
            Dim _NumWrongEmployee As Integer = 0 ' para saber cuantos empleados ya tienen nominas liquidadas
            Dim _employeeWrong As String
            Dim IdEmployeeTmp As Integer
            Dim LastLiquidationContract As Date?
            Dim ObjContract As Contract
            For Each _int As Integer In idSEmployee
                Dim _thisSch As Schedule = ScheduleDatasource.Find(Function(x) x.EmployeeId = _int)
                Dim _contract As Contract = _thisSch.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
                ObjContract = _contract
                Dim ContractLastDateLiquidation As Date?
                IdEmployeeTmp = _thisSch.Employee.Id
                LastLiquidationContract = _contract.LastLiquidationDate

                If _contract.Group IsNot Nothing Then
                    If Await ValidateScheduleBlock(_contract.Group) = False Then
                        Exit Function
                    End If
                End If

                'Es porque es renovación
                If _contract.RowType = 2 Then

                    'Se obtienen por medio de xpo los contratos del empleado
                    Dim listContractsTemp = Presenter.ListContractsByEmployeeId(_int)
                    'Se asigna el valor de la fecha de ultima liquidación
                    ContractLastDateLiquidation = (From x In listContractsTemp Select x.LastLiquidationDate).Max()

                    'ContractLastDateLiquidation = _thisSch.Employee.Contract.Select(Function(x) x.LastLiquidationDate).Max()
                Else

                    'Se obtiene por medio de xpo el contrato activo que tiene el empleado
                    Dim contractTemp = Presenter.GetContractByEmployeeId(_int)
                    'Se asigna el valor de la fecha de ultima liquidacion
                    ContractLastDateLiquidation = contractTemp.LastLiquidationDate

                End If

                Dim ctrCalendarInitialSelectedDate As New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, CtrCalendar.DaySelected)

                If ContractLastDateLiquidation IsNot Nothing And ctrCalendarInitialSelectedDate < ContractLastDateLiquidation Then
                    _NumWrongEmployee += 1
                    If _NumWrongEmployee = 1 Then
                        _employeeWrong = _thisSch.Employee.ThirdParty.Name
                    End If
                End If

                If ContractLastDateLiquidation Is Nothing Then
                    If ctrCalendarInitialSelectedDate < _contract.Group.LastDateLiquidation Then
                        _NumWrongEmployee += 1
                        If _NumWrongEmployee = 1 Then
                            _employeeWrong = _thisSch.Employee.ThirdParty.Name
                        End If
                    End If
                End If

                If _maxDate = Nothing Then
                    _maxDate = _contract.ContractEndingDate
                Else
                    If _contract.ContractEndingDate > _maxDate Then
                        _maxDate = _contract.ContractEndingDate
                    End If
                End If
                If _minDate = Nothing Then
                    _minDate = _contract.JobBondingDate
                Else
                    If _contract.JobBondingDate < _minDate Then
                        _minDate = _contract.JobBondingDate
                    End If
                End If

                If PermissionAllFunctionalUnit = True And _NumWrongEmployee > 0 Then

                    Dim LastPayrollInitialDate As Date
                    Dim LastPayrollEndDate As Date

                    LastPayrollInitialDate = _contract.Group.LastDateLiquidation

                    If _contract.Group.Liquidation = 1 Then
                        'Mensual
                        LastPayrollEndDate = New Date(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month, Date.DaysInMonth(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month))
                    Else
                        'Quincenal
                        If _contract.Group.LastDateLiquidation.Day = 1 Then
                            LastPayrollEndDate = New Date(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month, 15)
                        Else
                            LastPayrollEndDate = New Date(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month, Date.DaysInMonth(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month))
                        End If

                    End If

                    If ObjContract.Group.LastDateLiquidation < New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, numDay) And LastPayrollInitialDate <= New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, numDay) And LastPayrollEndDate >= New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, numDay) Then
                        _NumWrongEmployee = 0
                    End If
                End If

            Next

            If _NumWrongEmployee > 0 Then
                If _NumWrongEmployee > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadosLiquidados, CuadroDeTurno), _employeeWrong, (_NumWrongEmployee - 1))
                    Exit Function
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadoLiquidado, CuadroDeTurno), _employeeWrong)
                    Exit Function
                End If
            End If
            AsyncLoader(True)
            Using Form As New FrmTemplate()
                Dim EmployeeName As String = ScheduleDatasource.Find(Function(x) x.EmployeeId = employeeSelected).Employee.ThirdParty.Name
                Dim group As Group = ScheduleDatasource.Find(Function(x) x.EmployeeId = employeeSelected).Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault().Group
                If idSEmployee.Count > 1 Then
                    EmployeeName = EmployeeName & " y " & (idSEmployee.Count - 1).ToString & " Más"
                End If

                With Form
                    .EmployeeId = IdEmployeeTmp
                    .ObjContract = ObjContract
                    .EditFlag = False
                    .TimeInitialOrdinaryDay = group.PayrollParameter.InitialTimeOrdinaryDay
                    .TimeEndingOrdinaryDay = group.PayrollParameter.EndTimeOrdinaryDay
                    .AllowAllPermission = PermissionAllFunctionalUnit
                    .FunctionalUnitId = FunctionalUnit.Id
                    .ScheduleTemplateDatasource = Await Presenter.ScheduleTemplate_Datasource()
                    .FunctionalUnitDatasource = Presenter.FunctiontalUnit_XPInstantFeedbackSource()
                    .EmployeeName = EmployeeName
                    .EmployeeDatasource = ScheduleDatasource
                    .EmployeeCheked = idSEmployee
                    .StartDate = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, numDay)
                    .EndDate = .StartDate
                    .SetLimitInitialDate = _minDate
                    .SetLimitEndingDate = _maxDate
                    .SchedulDetail = New ScheduleDetail
                    .FunctionalUnit = FunctionalUnit
                    .NextDateLiquidation = LastLiquidationContract
                    .SetDocumentsSchedule()
                    .CtrCalendarMini1.YearControl = CtrCalendar.YearControl
                    .CtrCalendarMini1.MonthControl = CtrCalendar.MonthControl
                    .CtrCalendarMini1.Generate_Calendar()
                    AsyncLoader(False)
                    .LogicaBotonActualizar(False)
                    Dim frmTrans As FrmTransparent = New FrmTransparent(Form, False)
                    frmTrans.ShowDialog(Me)
                    Select Case .ActionForm

                        'Guardar
                        Case Is = 1
                            Await SaveScheduleDetail(.SchedulDetail, .EmployeesChecked, .DaysToSave, .NewFunctionalUnit, .Include_Holiday)
                            'Cancelar
                        Case Is = 3
                            Exit Function
                        Case Is = 4
                            Mensaje(EeventViewerImages.Advertencia) = "No seleccionó ningun evento"

                        Case Else
                            Exit Function

                    End Select
                    .Dispose()
                End With
            End Using
        End If
    End Function


    ''' <summary>
    ''' Metodo para guardar schedule
    ''' </summary>
    ''' <param name="_scheduleDetail">el detalle de turno</param>
    ''' <param name="_employeesCheked">listado de los id's de los empleados a registrarles el turno</param>
    ''' <param name="_dayToSave">listado de los dias a insertar el registro</param>
    ''' <param name="newFunctionalUnit">La unidad funcional nueva, en el caso de que se vaya a registrar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveScheduleDetail(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean) As Task(Of Boolean)
        Dim result As ActionMessageResult(Of List(Of Schedule))
        AsyncLoader(True)
        Using model As New MSchedule
            result = Await model.SaveScheduleDetailMasiveAsync(_scheduleDetail, _employeesCheked, _dayToSave, newFunctionalUnit, Include_Holiday, FunctionalUnit, ScheduleDatasource, Period, List_Holiday)
        End Using
        AsyncLoader(False)
        Await RefreshForm()
        If result.StateResult = True Then
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Dim _listScheduleWithoutSave As List(Of DaysNotsave) = New List(Of DaysNotsave)
                For Each item As MessageResult In result.MessageResult
                    _listScheduleWithoutSave.Add(Await Generate_dayNotSave(item))
                Next

                AsyncLoader(True)
                Using FormInfoDialog As New FrmInfoDialog
                    With FormInfoDialog
                        .InfoDialogDatasource = _listScheduleWithoutSave
                        .ShowDialog()
                        .Dispose()
                    End With
                End Using
                AsyncLoader(False)
            End If
        ElseIf Not String.IsNullOrEmpty(result.Message) Then
            Mensaje(EeventViewerImages.Advertencia) = result.Message
        End If

        Return True
    End Function

    ''' <summary>
    ''' Funcion que retorna un schedule detail armado, cuando se quieren cortar por ser eventos
    ''' </summary>
    ''' <param name="detailHours">detalle de horas general</param>
    ''' <param name="DtInitial">Fecha Hora, Inicial</param>
    ''' <param name="DtEnding">Fecha Hora, Fin</param>
    ''' <returns>El schedule detail hora</returns>
    Public Function GetSchDetHourInEvents(detailHours As ScheduleDetailHour, DtInitial As DateTime, DtEnding As DateTime) As ScheduleDetailHour
        Dim newDschDetHour As ScheduleDetailHour = New ScheduleDetailHour()
        With newDschDetHour
            .Event = True
            .DateTimeInitial = DtInitial
            .DateTimeEnding = DtEnding
            .NextDay = detailHours.NextDay
            .TotalNumberHours = .DateTimeEnding.Hour - .DateTimeInitial.Hour
            If _List_Holidays.Find(Function(x) x.Holiday1 = .DateTimeInitial.Date) IsNot Nothing Or Weekday(.DateTimeInitial.Date, FirstDayOfWeek.Sunday) = 1 Then
                .AppliedLiquidationConcept = True
            Else
                .AppliedLiquidationConcept = False
            End If
        End With
        Return newDschDetHour
    End Function

    ''' <summary>
    ''' Metodo para generar el item DaysNotsave 
    ''' </summary>
    ''' <param name="_schedule">schedule general</param>
    ''' <param name="_schDet">detalle de el schedule</param>
    ''' <param name="day">fecha del dia que no registro</param>
    ''' <param name="reazon">razon por la cual no registro</param>
    ''' <returns>el item DaysNotsave</returns>
    ''' <remarks></remarks>
    Public Async Function Generate_dayNotSave(_schedule As Schedule, _schDet As ScheduleDetail, day As Date, reason As EFailureCauses) As Task(Of DaysNotsave)
        Dim dayNotSave As DaysNotsave = New DaysNotsave()
        If IsDesignMode = False Then
            dtfi = ci.DateTimeFormat
        Else
            dtfi = New Globalization.DateTimeFormatInfo
        End If
        With dayNotSave
            .EmployeeError = _schedule.Employee.ThirdParty.Name
            .DayError = dtfi.GetDayName(day.DayOfWeek) & ", " & day.Day & " de " & dtfi.GetMonthName(day.Month) & " de " & day.Year
            Select Case reason
                Case EFailureCauses.ErrorVacaciones
                    .MessegeError = obtenerRecurso(ErrorVacaciones, CuadroDeTurno)
                Case EFailureCauses.ErrorPermisoVacaciones
                    .MessegeError = obtenerRecurso(ErrorPermisoVacaciones, CuadroDeTurno)
                Case EFailureCauses.ErrorHour
                    .MessegeError = obtenerRecurso(ErrorHour, CuadroDeTurno)
                Case EFailureCauses.ErrorHoliday
                    .MessegeError = obtenerRecurso(ErrorHoliday, CuadroDeTurno)
                Case EFailureCauses.ErrorMaximumTotalHourNumber
                    .MessegeError = obtenerRecurso(ErrorMaximumTotalHourNumber, CuadroDeTurno)
                Case EFailureCauses.ErrorScheduleDetailExisting
                    .MessegeError = obtenerRecurso(ErrorScheduleDetailExisting, CuadroDeTurno)
                Case EFailureCauses.ErrorWithoutContract
                    .MessegeError = obtenerRecurso(ErrorWithoutContract, CuadroDeTurno)
                Case EFailureCauses.ErrorIncapacidad
                    .MessegeError = obtenerRecurso(ErrorIncapacidad, CuadroDeTurno)
                Case EFailureCauses.ErrorSancion
                    .MessegeError = obtenerRecurso(ErrorSancion, CuadroDeTurno)
                Case EFailureCauses.ErrorLicencia
                    .MessegeError = obtenerRecurso(ErrorLicencia, CuadroDeTurno)
                Case EFailureCauses.ErrorMaximo18Horas
                    .MessegeError = obtenerRecurso(ErrorMaximoHoras18, CuadroDeTurno)

            End Select
            Using mTemp As New MScheduleControl
                Dim temp As ScheduleTemplate = Await mTemp.GetScheduleTemplateByIdAsync(_schDet.ScheduleTemplateId)
                If temp IsNot Nothing Then
                    .TemplateError = temp.Name
                End If
            End Using
            .IconError = CtrCalendar.SetImageIconSchedule(Presenter.IconSchedule(_schDet.Letter), False)
        End With
        Return dayNotSave
    End Function

    ''' <summary>
    ''' Metodo para generar el item DaysNotsave 
    ''' </summary>
    ''' <param name="messageResult">schedule general</param>
    ''' <returns>el item DaysNotsave</returns>
    ''' <remarks></remarks>
    Public Async Function Generate_dayNotSave(messageResult As MessageResult) As Task(Of DaysNotsave)
        Dim dayNotSave As DaysNotsave = New DaysNotsave()
        If IsDesignMode = False Then
            dtfi = ci.DateTimeFormat
        Else
            dtfi = New Globalization.DateTimeFormatInfo
        End If
        With dayNotSave
            .EmployeeError = messageResult.Parameters(0)
            Dim day As New Date(CInt(messageResult.Parameters(3)), CInt(messageResult.Parameters(2)), CInt(messageResult.Parameters(1)))
            .DayError = dtfi.GetDayName(day.DayOfWeek) & ", " & day.Day & " de " & dtfi.GetMonthName(day.Month) & " de " & day.Year
            Select Case messageResult.CodeMessage
                Case "-006" 'EFailureCauses.ErrorHour
                    .MessegeError = obtenerRecurso(ErrorHour, CuadroDeTurno)
                Case "-008" 'EFailureCauses.ErrorMaximumTotalHourNumber
                    .MessegeError = obtenerRecurso(ErrorMaximumTotalHourNumber, CuadroDeTurno)
                Case "-007" 'EFailureCauses.ErrorScheduleDetailExisting
                    .MessegeError = obtenerRecurso(ErrorScheduleDetailExisting, CuadroDeTurno)
                Case "-002" 'EFailureCauses.ErrorWithoutContract
                    .MessegeError = obtenerRecurso(ErrorWithoutContract, CuadroDeTurno)
                Case "-001" 'EFailureCauses.ErrorHoliday
                    .MessegeError = obtenerRecurso(ErrorHoliday, CuadroDeTurno)
                Case "-003" 'EFailureCauses.ErrorIncapacidad
                    .MessegeError = obtenerRecurso(ErrorIncapacidad, CuadroDeTurno)
                Case "-005" 'EFailureCauses.ErrorSancion
                    .MessegeError = obtenerRecurso(ErrorSancion, CuadroDeTurno)
                Case "-004" 'EFailureCauses.ErrorLicencia
                    .MessegeError = obtenerRecurso(ErrorLicencia, CuadroDeTurno)
                Case "-009" 'EFailureCauses.ErrorVacaciones
                    .MessegeError = obtenerRecurso(ErrorVacaciones, CuadroDeTurno)
                Case "-010" 'EFailureCauses.ErrorPermisoVacaciones
                    .MessegeError = obtenerRecurso(ErrorPermisoVacaciones, CuadroDeTurno)
                Case "-011" 'EFailureCauses.ErrorPermisoVacaciones
                    .MessegeError = obtenerRecurso(ErrorMaximoHoras18, CuadroDeTurno)
            End Select
            Using mTemp As New MScheduleControl
                Dim temp As ScheduleTemplate = Await mTemp.GetScheduleTemplateByIdAsync(CInt(messageResult.Parameters(4)))
                If temp IsNot Nothing Then
                    .TemplateError = temp.Name
                    .IconError = CtrCalendar.SetImageIconSchedule(Presenter.IconSchedule(temp.Letter), False)
                End If
            End Using
        End With
        Return dayNotSave
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para editar un detalle de turno
    ''' </summary>
    ''' <param name="_numEmployee">Numero del empleado que se va a modificar, si el 1 o el 2</param>
    ''' <param name="_numDay">el dia que se va a modificar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function EditScheduleDetail(_numEmployee As Integer, _numDay As Integer, ScheduleIdListChecked As List(Of Integer)) As Task

        Dim _NumWrongEmployee As Integer = 0 ' para saber cuantos empleados ya tienen nominas liquidadas
        Dim _employeeWrong As String
        Dim Tmpcontract As Contract

        For Each _int As Integer In ScheduleIdListChecked
            Dim _thisSch As Schedule = ScheduleDatasource.Find(Function(x) x.EmployeeId = _int)
            Dim _contract As Contract = _thisSch.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
            Dim ContractLastDateLiquidation As Date?
            Tmpcontract = _contract

            If Await ValidateScheduleBlock(_contract.Group) = False Then
                Exit Function
            End If

            'Es porque es renovación
            If _contract.RowType = 2 Then

                'Se obtienen por medio de xpo los contratos del empleado
                Dim listContractsTemp = Presenter.ListContractsByEmployeeId(_int)
                'Se asigna el valor de la fecha de ultima liquidación
                ContractLastDateLiquidation = (From x In listContractsTemp Select x.LastLiquidationDate).Max()

                'ContractLastDateLiquidation = _thisSch.Employee.Contract.Select(Function(x) x.LastLiquidationDate).Max()
            Else

                'Se obtiene por medio de xpo el contrato activo que tiene el empleado
                Dim contractTemp = Presenter.GetContractByEmployeeId(_int)
                'Se asigna el valor de la fecha de ultima liquidacion
                ContractLastDateLiquidation = contractTemp.LastLiquidationDate

                'ContractLastDateLiquidation = _contract.LastLiquidationDate
            End If

            If ContractLastDateLiquidation IsNot Nothing AndAlso New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay) <= ContractLastDateLiquidation Then
                _NumWrongEmployee += 1
                If _NumWrongEmployee = 1 Then
                    _employeeWrong = _thisSch.Employee.ThirdParty.Name
                End If
            End If

            If PermissionAllFunctionalUnit = True And _NumWrongEmployee > 0 Then

                Dim LastPayrollInitialDate As Date
                Dim LastPayrollEndDate As Date

                LastPayrollInitialDate = _contract.Group.LastDateLiquidation

                If _contract.Group.Liquidation = 1 Then
                    'Mensual
                    LastPayrollEndDate = New Date(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month, Date.DaysInMonth(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month))
                Else
                    'Quincenal
                    If _contract.Group.LastDateLiquidation.Day = 1 Then
                        LastPayrollEndDate = New Date(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month, 15)
                    Else
                        LastPayrollEndDate = New Date(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month, Date.DaysInMonth(_contract.Group.LastDateLiquidation.Year, _contract.Group.LastDateLiquidation.Month))
                    End If

                End If

                If _contract.Group.LastDateLiquidation < New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay) And LastPayrollInitialDate <= New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay) And LastPayrollEndDate >= New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay) Then
                    _NumWrongEmployee = 0
                End If
            End If


            If _NumWrongEmployee > 0 Then
                If _NumWrongEmployee > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadosLiquidados, CuadroDeTurno), _employeeWrong, (_NumWrongEmployee - 1))
                    Exit Function
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadoLiquidado, CuadroDeTurno), _employeeWrong)
                    Exit Function
                End If
            End If

            'If ContractLastDateLiquidation IsNot Nothing AndAlso New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay) <= ContractLastDateLiquidation Then
            '    Mensaje(EeventViewerImages.Advertencia) = "El Empleado se Encuentra YA LIQUIDADO"
            '    Exit Function
            'End If
        Next

        Dim _employeeName As String
        Dim scheduleDetailReal As ScheduleDetail
        If _numEmployee > 1 Then 'se va a editar el schedule 2
            Using model As New MSchedule()
                scheduleDetailReal = Await model.GetScheduleDetail(DictionaryScheduleDetail_Second.Item(_numDay).Id)
            End Using
            _employeeName = ScheduleN2.Employee.ThirdParty.Name
        Else 'se va a editar el schedule 1
            Using model As New MSchedule()
                scheduleDetailReal = Await model.GetScheduleDetail(DictionaryScheduleDetail_First.Item(_numDay).Id)
            End Using
            _employeeName = ScheduleN1.Employee.ThirdParty.Name
        End If

        If scheduleDetailReal.ScheduleDetailHour.Any(Function(x) x.Event = True) And PermissionEdit = False Then
            Mensaje(EeventViewerImages.Advertencia) = "Usted no posee permisos para editar este turno porque ya tiene un evento creado"
            Exit Function
        End If



        AsyncLoader(True)
        Using Form As New FrmTemplate()
            With Form
                .ObjContract = Tmpcontract
                .EditFlag = True
                .AllowAllPermission = PermissionAllFunctionalUnit
                .FunctionalUnitId = FunctionalUnit.Id
                .ScheduleTemplateDatasource = Await Presenter.ScheduleTemplate_Datasource()
                .FunctionalUnitDatasource = Presenter.FunctiontalUnit_XPInstantFeedbackSource()
                .StartDate = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay)
                .EndDate = .StartDate
                .EmployeeName = _employeeName
                .SchedulDetail = scheduleDetailReal
                .ScheduleTemplateId = .SchedulDetail.ScheduleTemplateId
                .FunctionalUnit = FunctionalUnit
                .ActionOnControls = False
                .OcultGrEvents = False
                .INDRgEvent.EditValue = False
                .INDGcDetailHours.DataSource = .SchedulDetail.ScheduleDetailHour.ToList()
                AsyncLoader(False)
                .LogicaBotonActualizar(True)

                Dim frmTrans As FrmTransparent = New FrmTransparent(Form, False)
                frmTrans.ShowDialog(Me)
                Select Case .ActionForm

                    'Guardar
                    Case Is = 1
                        Await ModificScheduleDetail(.SchedulDetail, _numDay, .NewFunctionalUnit)

                        'Eliminar
                    Case Is = 2
                        Dim listToDeleteDays As List(Of Integer) = New List(Of Integer)
                        Dim listToDeleteEmployees As List(Of Integer) = New List(Of Integer)
                        listToDeleteDays.Add(_numDay)
                        listToDeleteEmployees.Add(scheduleDetailReal.EmployeeId)
                        Await DeleteScheduleDetail(listToDeleteEmployees, listToDeleteDays)
                        Await Me.Presenter.Load_Employee(PermissionAllPosition, Indigo)
                        SetValuesAgain()

                    Case Else
                        Exit Select

                End Select
                .Dispose()
            End With
        End Using

    End Function

    ''' <summary>
    ''' Metodo para modificar un schedule detail
    ''' </summary>
    ''' <param name="_scheduleDetailToModific">el schedule detail a modificar</param>
    ''' <param name="_numDay">numero de dia a modificar</param>
    ''' <param name="_NewFunctionalUnit">si se va a modificar en otra unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ModificScheduleDetail(_scheduleDetailToModific As ScheduleDetail, _numDay As Integer, _NewFunctionalUnit As FunctionalUnit) As Task(Of Boolean)
        Dim _schedule As Schedule
        Dim _validContract As Contract
        If _NewFunctionalUnit Is Nothing Then '  se va a modificar en la misma unidad funcional
            _schedule = ScheduleDatasource.Find(Function(x) x.EmployeeId = _scheduleDetailToModific.EmployeeId)
            _validContract = _schedule.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
            AsyncLoader(True)
            Using Model As New MSchedule
                Dim TotalHours As Decimal = 0
                For Each item As ScheduleDetailHour In _scheduleDetailToModific.ScheduleDetailHour
                    Dim hi As TimeSpan = New TimeSpan(item.DateTimeInitial.Hour, item.DateTimeInitial.Minute, item.DateTimeInitial.Second)
                    Dim he As TimeSpan = New TimeSpan(item.DateTimeEnding.Hour, item.DateTimeEnding.Minute, item.DateTimeEnding.Second)
                    Dim Hours As Decimal = 0
                    If he = New TimeSpan(0, 0, 0) Then
                        If item.NextDay = False Then
                            Hours = 24 - hi.Hours
                        Else
                            Hours = (he - hi).Hours + ((he - hi).Minutes / 60)
                        End If
                    Else
                        Hours = (he - hi).Hours + ((he - hi).Minutes / 60)
                    End If
                    If _List_Holidays.Find(Function(x) x.Holiday1 = item.DateTimeInitial.Date) IsNot Nothing Or Weekday(item.DateTimeInitial.Date, FirstDayOfWeek.Sunday) = 1 Then
                        item.AppliedLiquidationConcept = True
                    Else
                        item.AppliedLiquidationConcept = False
                    End If
                    TotalHours += Hours
                    item.TotalNumberHours = Hours
                Next
                _scheduleDetailToModific.TotalNumberHours = TotalHours
                Dim schDetByEmployee As List(Of ScheduleDetail)
                schDetByEmployee = Await Model.GetScheduleDetailByEmployeeBetweenDateAsync(_schedule.EmployeeId,
                                                New Date(_scheduleDetailToModific.DateDetail.Year, _scheduleDetailToModific.DateDetail.Month, 1),
                                                New Date(_scheduleDetailToModific.DateDetail.Year, _scheduleDetailToModific.DateDetail.Month,
                                                         DateTime.DaysInMonth(_scheduleDetailToModific.DateDetail.Year, _scheduleDetailToModific.DateDetail.Month)))
                If schDetByEmployee Is Nothing Then
                    schDetByEmployee = New List(Of ScheduleDetail)
                Else
                    Dim schCurrent As ScheduleDetail = schDetByEmployee.Find(Function(x) x.DateDetail = _scheduleDetailToModific.DateDetail And x.ScheduleFunctionalUnitId = _scheduleDetailToModific.ScheduleFunctionalUnitId)
                    If schCurrent IsNot Nothing Then
                        schDetByEmployee.Remove(schCurrent)
                    End If
                End If
                Dim ValidateScheduleToModific As EFailureCauses = ValidateScheduleDetailInSchedule(_schedule, _scheduleDetailToModific.DateDetail, True, _schedule.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault, _scheduleDetailToModific.ScheduleDetailHour, schDetByEmployee)
                If ValidateScheduleToModific = 0 Then
                    Dim ListIdEmployee As New List(Of Integer)
                    ListIdEmployee.Add(_scheduleDetailToModific.EmployeeId)

                    Dim ListDateSave As New List(Of Date)
                    ListDateSave.Add(_scheduleDetailToModific.DateDetail)

                    Dim ListScheduleSave As New List(Of Schedule)
                    ListScheduleSave.Add(_schedule)

                    Dim ObjTmpResult = Await Model.SaveScheduleDetailMasiveAsync(_scheduleDetailToModific, ListIdEmployee, ListDateSave, Nothing, True, _scheduleDetailToModific.FunctionalUnit, ListScheduleSave, _schedule.Period, List_Holiday, True)
                    If ObjTmpResult.StateResult AndAlso ObjTmpResult.MessageResult.Count = 0 Then

                        'Asignamos el nuevo objeto modificado para ser guardado
                        Dim updatedSchedule = ObjTmpResult.ObjectEmbbeded.Find(Function(x) x.Id = _schedule.Id)
                        If updatedSchedule IsNot Nothing Then
                            _schedule = updatedSchedule
                        End If
                        SetHourNumberInScheduleDetail(_schedule, _scheduleDetailToModific.TotalNumberHours, _numDay)
                        _scheduleDetailToModific.ChangeTracker.State = ObjectState.Unchanged
                        _scheduleDetailToModific.FunctionalUnit = Nothing
                        DeleteAgregatesSchedule(_schedule)
                    ElseIf ObjTmpResult.StateResult = False AndAlso ObjTmpResult.MessageResult IsNot Nothing Then
                        Dim _listScheduleWithoutSave As List(Of DaysNotsave) = New List(Of DaysNotsave)
                        If ObjTmpResult.MessageResult Is Nothing Then
                            If ObjTmpResult.Message IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = ObjTmpResult.Message.ToString()
                                AsyncLoader(False)
                                Exit Function
                            End If
                        Else
                            For Each item As MessageResult In ObjTmpResult.MessageResult
                                _listScheduleWithoutSave.Add(Await Generate_dayNotSave(item))
                            Next
                        End If
                        If _listScheduleWithoutSave.Count > 0 Then
                            Using FormInfoDialog As New FrmInfoDialog
                                With FormInfoDialog
                                    .InfoDialogDatasource = _listScheduleWithoutSave
                                    Dim frmTrans As FrmTransparent = New FrmTransparent(FormInfoDialog, False)
                                    frmTrans.ShowDialog(Me)
                                    .Dispose()
                                End With
                            End Using
                        End If
                    ElseIf (ObjTmpResult.MessageResult.FirstOrDefault().CodeMessage = "-011") Then
                        Dim _listScheduleWithoutSave As List(Of DaysNotsave) = New List(Of DaysNotsave)
                        _listScheduleWithoutSave.Add(Await Generate_dayNotSave(_schedule, _scheduleDetailToModific, _scheduleDetailToModific.DateDetail, EFailureCauses.ErrorMaximo18Horas))
                        If _listScheduleWithoutSave.Count > 0 Then
                            Using FormInfoDialog As New FrmInfoDialog
                                With FormInfoDialog
                                    .InfoDialogDatasource = _listScheduleWithoutSave
                                    Dim frmTrans As FrmTransparent = New FrmTransparent(FormInfoDialog, False)
                                    frmTrans.ShowDialog(Me)
                                    .Dispose()
                                End With
                            End Using
                        End If

                        AsyncLoader(False)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ObjTmpResult.Message
                        AsyncLoader(False)
                        Exit Function
                    End If
                    Await Me.Presenter.Load_Employee(PermissionAllPosition, Indigo)
                    SetValuesAgain()
                Else
                    Dim _listScheduleWithoutSave As List(Of DaysNotsave) = New List(Of DaysNotsave)
                    _listScheduleWithoutSave.Add(Await Generate_dayNotSave(_schedule, _scheduleDetailToModific, _scheduleDetailToModific.DateDetail, ValidateScheduleToModific))
                    If _listScheduleWithoutSave.Count > 0 Then
                        Using FormInfoDialog As New FrmInfoDialog
                            With FormInfoDialog
                                .InfoDialogDatasource = _listScheduleWithoutSave
                                Dim frmTrans As FrmTransparent = New FrmTransparent(FormInfoDialog, False)
                                frmTrans.ShowDialog(Me)
                                .Dispose()
                            End With
                        End Using
                    End If
                End If
            End Using
            AsyncLoader(False)
        End If
    End Function
    ''' <summary>
    ''' Metodo asincrono que elimina determinados schedule detail de determiados empleados
    ''' </summary>
    ''' <param name="listToDeleteEmployees">listado de empleados a eliminar los turnos</param>
    ''' <param name="listToDelete">Numero de dias</param>
    ''' <returns>si se elimino el registro</returns>
    Public Async Function DeleteScheduleDetail(listToDeleteEmployees As List(Of Integer), listToDelete As List(Of Integer)) As Task(Of Boolean)

        Dim listScheduleDetailToDelete As List(Of ScheduleDetail) = New List(Of ScheduleDetail)
        Dim ListNotDelete As New List(Of ScheduleDetail)
        For Each _intEmp As Integer In listToDeleteEmployees

            Dim _thisSch As Schedule = ScheduleDatasource.Find(Function(x) x.EmployeeId = _intEmp)
            Dim _contract As Contract = _thisSch.Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault
            Dim ContractLastDateLiquidation As Date?

            If Await ValidateScheduleBlock(_contract.Group) = False Then
                Exit Function
            End If

            'Es porque es renovación
            If _contract.RowType = 2 Then
                ContractLastDateLiquidation = _thisSch.Employee.Contract.Select(Function(x) x.LastLiquidationDate).Max()
            Else
                ContractLastDateLiquidation = _contract.LastLiquidationDate
            End If


            Dim NewDate As Date = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, 1)
            Dim NewDateMonth As Date = DateAdd(DateInterval.Month, 1, NewDate)
            Dim DateComparision As Date = DateAdd(DateInterval.Day, -1, NewDateMonth)

            Dim _NumWrongEmployee As Integer = 0
            If ContractLastDateLiquidation IsNot Nothing AndAlso DateComparision <= ContractLastDateLiquidation Then
                _NumWrongEmployee += 1
                If _NumWrongEmployee = 1 Then
                    _employeeWrong = _thisSch.Employee.ThirdParty.Name
                End If
            End If

            If _NumWrongEmployee > 0 Then
                If _NumWrongEmployee > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadosLiquidados, CuadroDeTurno), _employeeWrong, (_NumWrongEmployee - 1))
                    Exit Function
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadoLiquidado, CuadroDeTurno), _employeeWrong)
                    Exit Function
                End If
            End If


            Dim _listScheduleDetail As List(Of ScheduleDetail) = New List(Of ScheduleDetail)
            For i As Integer = 0 To ScheduleComplete.Count - 1
                Dim _listSchDet As List(Of ScheduleDetail) = ScheduleComplete.Item(i)
                If _listSchDet.Item(0).EmployeeId = _intEmp Then
                    _listScheduleDetail = _listSchDet
                    Exit For
                End If
            Next
            For Each _numDay As Integer In listToDelete
                If _listScheduleDetail IsNot Nothing AndAlso _listScheduleDetail.Count > 0 Then
                    Dim _schDetail As ScheduleDetail = _listScheduleDetail.Find(Function(x) x.DateDetail = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, _numDay) And x.ScheduleFunctionalUnitId = FunctionalUnit.Id)
                    If _schDetail IsNot Nothing Then
                        If _schDetail.ScheduleTemplateId > 0 Then
                            If PermissionEdit = True Then
                                listScheduleDetailToDelete.Add(_schDetail)
                            ElseIf PermissionEdit = False And _schDetail.ScheduleDetailHour.Any(Function(x) x.Event = False) Then
                                listScheduleDetailToDelete.Add(_schDetail)
                            ElseIf PermissionEdit = False And _schDetail.ScheduleDetailHour.Any(Function(x) x.Event = True) Then
                                ListNotDelete.Add(_schDetail)
                            End If
                        End If
                    End If
                End If
            Next
        Next

        If ListNotDelete.Count > 0 Then

            Dim FechasNotDelete As String

            If ListNotDelete.Count = 1 Then
                FechasNotDelete = ListNotDelete.Item(0).DateDetail.ToShortDateString
            Else
                For Each objScheduleDetailDelete As ScheduleDetail In ListNotDelete

                    If FechasNotDelete = String.Empty Then
                        FechasNotDelete = objScheduleDetailDelete.DateDetail.ToShortDateString
                    Else
                        FechasNotDelete = FechasNotDelete + " - " + objScheduleDetailDelete.DateDetail.ToShortDateString
                    End If
                Next
            End If


            Mensaje(EeventViewerImages.Advertencia) = "Usted no posee permisos para eliminar algunos turnos ( " + FechasNotDelete + " )  que tienen Eventos Asignados"
        End If

        If listScheduleDetailToDelete IsNot Nothing AndAlso listScheduleDetailToDelete.Count > 0 Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using model As New MSchedule
                    AsyncLoader(True)
                    Dim result = Await model.DeleteScheduleDetailMasiveAsync(listScheduleDetailToDelete)
                    AsyncLoader(False)
                    If Not result Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador)
                        Return result
                    End If
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                    Return result
                End Using
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoHayRegistrosParaEliminar)
        End If
        Return False
    End Function

    ''' <summary>
    ''' Metodo para elimianar los delegados del schedule antes de mandar a guardar
    ''' </summary>
    ''' <param name="_item"></param>
    ''' <remarks></remarks>
    Public Sub DeleteAgregatesSchedule(ByRef _item As Schedule)
        With _item
            If .FunctionalUnit IsNot Nothing Then
                .FunctionalUnitId = .FunctionalUnit.Id
                .FunctionalUnit = Nothing
            End If
            If .Employee IsNot Nothing Then
                .EmployeeId = .Employee.Id
                .Employee = Nothing
            End If
        End With
    End Sub

    ''' <summary>
    ''' Funcion para colocar el numero de horas en el schedule antes de modificarlo
    ''' </summary>
    ''' <param name="_schedule">El registro de schedule</param>
    ''' <param name="newHour">El nuevo numero de horas modificado</param>
    ''' <param name="_numDay">numero del dia a modificar la hora</param>
    Public Sub SetHourNumberInScheduleDetail(ByRef _schedule As Schedule, newHour As Decimal, _numDay As Integer)
        Select Case _numDay
            Case Is = 1
                _schedule.ScheduleDetail.TotalNumberHours = newHour
                _schedule.ScheduleDetail.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 2
                _schedule.ScheduleDetail1.TotalNumberHours = newHour
                _schedule.ScheduleDetail1.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 3
                _schedule.ScheduleDetail2.TotalNumberHours = newHour
                _schedule.ScheduleDetail2.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 4
                _schedule.ScheduleDetail3.TotalNumberHours = newHour
                _schedule.ScheduleDetail3.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 5
                _schedule.ScheduleDetail4.TotalNumberHours = newHour
                _schedule.ScheduleDetail4.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 6
                _schedule.ScheduleDetail5.TotalNumberHours = newHour
                _schedule.ScheduleDetail5.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 7
                _schedule.ScheduleDetail6.TotalNumberHours = newHour
                _schedule.ScheduleDetail6.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 8
                _schedule.ScheduleDetail7.TotalNumberHours = newHour
                _schedule.ScheduleDetail7.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 9
                _schedule.ScheduleDetail8.TotalNumberHours = newHour
                _schedule.ScheduleDetail8.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 10
                _schedule.ScheduleDetail9.TotalNumberHours = newHour
                _schedule.ScheduleDetail9.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 11
                _schedule.ScheduleDetail10.TotalNumberHours = newHour
                _schedule.ScheduleDetail10.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 12
                _schedule.ScheduleDetail11.TotalNumberHours = newHour
                _schedule.ScheduleDetail11.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 13
                _schedule.ScheduleDetail12.TotalNumberHours = newHour
                _schedule.ScheduleDetail12.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 14
                _schedule.ScheduleDetail13.TotalNumberHours = newHour
                _schedule.ScheduleDetail13.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 15
                _schedule.ScheduleDetail14.TotalNumberHours = newHour
                _schedule.ScheduleDetail14.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 16
                _schedule.ScheduleDetail15.TotalNumberHours = newHour
                _schedule.ScheduleDetail15.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 17
                _schedule.ScheduleDetail16.TotalNumberHours = newHour
                _schedule.ScheduleDetail16.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 18
                _schedule.ScheduleDetail17.TotalNumberHours = newHour
                _schedule.ScheduleDetail17.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 19
                _schedule.ScheduleDetail18.TotalNumberHours = newHour
                _schedule.ScheduleDetail18.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 20
                _schedule.ScheduleDetail19.TotalNumberHours = newHour
                _schedule.ScheduleDetail19.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 21
                _schedule.ScheduleDetail20.TotalNumberHours = newHour
                _schedule.ScheduleDetail20.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 22
                _schedule.ScheduleDetail21.TotalNumberHours = newHour
                _schedule.ScheduleDetail21.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 23
                _schedule.ScheduleDetail22.TotalNumberHours = newHour
                _schedule.ScheduleDetail22.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 24
                _schedule.ScheduleDetail23.TotalNumberHours = newHour
                _schedule.ScheduleDetail23.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 25
                _schedule.ScheduleDetail24.TotalNumberHours = newHour
                _schedule.ScheduleDetail24.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 26
                _schedule.ScheduleDetail25.TotalNumberHours = newHour
                _schedule.ScheduleDetail25.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 27
                _schedule.ScheduleDetail26.TotalNumberHours = newHour
                _schedule.ScheduleDetail26.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 28
                _schedule.ScheduleDetail27.TotalNumberHours = newHour
                _schedule.ScheduleDetail27.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 29
                _schedule.ScheduleDetail28.TotalNumberHours = newHour
                _schedule.ScheduleDetail28.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 30
                _schedule.ScheduleDetail29.TotalNumberHours = newHour
                _schedule.ScheduleDetail29.ChangeTracker.State = ObjectState.Unchanged

            Case Is = 31
                _schedule.ScheduleDetail30.TotalNumberHours = newHour
                _schedule.ScheduleDetail30.ChangeTracker.State = ObjectState.Unchanged

        End Select
    End Sub

    ''' <summary>
    ''' Metodo para el tamaño de los controles
    ''' </summary>
    Public Sub Controls_Size()
        INDLyItemScheduleDetailPanelComplete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDDDBFunctionalUnit.StyleController = Nothing
        INDDDBFunctionalUnit.Font = New System.Drawing.Font("Segoe UI", 27.75!)
        INDDDBFunctionalUnit.ForeColor = System.Drawing.Color.White
        IndigoGridControl1.SetHoldSize(INDPopupGcMatches, True)
        IndigoGridControl1.SetHoldSize(INDPopupMoreGcHoursDetail, True)
        IndigoGridControl1.SetHoldSize(INDGcScheduleDetail1, True)

    End Sub

    ''' <summary>
    ''' Se realiza despues de que se registra un schedule, y se recargan los controles.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetValuesAgain()
        'AsyncLoader(True)
        RemoveHandler INDChkLbcEmployee.ItemCheck, AddressOf INDChkLbcEmployee_ItemCheck
        Dim ListEmployeeToUnchecked As List(Of Integer) = New List(Of Integer)
        If ScheduleIdListChecked.Count > 0 Then
            For Each item As Integer In ScheduleIdListChecked
                Dim _value As Object = item
                If INDChkLbcEmployee.Items.Item(_value) IsNot Nothing Then
                    INDChkLbcEmployee.Items.Item(_value).CheckState = System.Windows.Forms.CheckState.Checked
                Else
                    ListEmployeeToUnchecked.Add(_value)
                End If
            Next
            If ListEmployeeToUnchecked.Count > 0 Then
                For Each item As Integer In ListEmployeeToUnchecked
                    Dim _value As Object = item
                    _ScheduleIdListChecked.Remove(_value)
                Next
            End If
            INDChkLbcEmployee_ItemCheck(INDChkLbcEmployee, Nothing)
        End If
        AddHandler INDChkLbcEmployee.ItemCheck, AddressOf INDChkLbcEmployee_ItemCheck
        If ScheduleIdListChecked.Count > 2 Then
            INDGcScheduleDetail1.RefreshDataSource()
        End If
        ActionOnControlsDelete = False
        'AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo para validar que no exista ya un turno para el dia especificado
    ''' </summary>
    ''' <param name="_schedule">Registro del Schedule a validar</param>
    ''' <param name="day">Dia a validar</param>
    ''' <param name="Include_Holidays">Si incluye dias festivos</param>
    ''' <returns>Si es o no Valido</returns>
    ''' <remarks></remarks>
    Public Function ValidateScheduleDetailInSchedule(ByVal _schedule As Schedule, ByVal day As Date, ByVal Include_Holidays As Boolean, _validContract As Contract, ByVal detHours As TrackableCollection(Of ScheduleDetailHour), ScheduleInPeriod As List(Of ScheduleDetail)) As EFailureCauses
        ValidateScheduleDetailInSchedule = 0
        Dim _thisDay As Date = day
        If Include_Holidays = False Then 'valido los festivos parametrizados
            If List_Holiday.Find(Function(x) x.Holiday1 = _thisDay) IsNot Nothing Then
                ValidateScheduleDetailInSchedule = EFailureCauses.ErrorHoliday
                Exit Function
            End If
            If Weekday(_thisDay, FirstDayOfWeek.Sunday) = 1 Then ' valido los domingos
                ValidateScheduleDetailInSchedule = EFailureCauses.ErrorHoliday
                Exit Function
            End If
        End If
        If day < _validContract.JobBondingDate Or day > _validContract.ContractEndingDate Then ' Valido que la fecha este entre el día del contrato
            ValidateScheduleDetailInSchedule = EFailureCauses.ErrorWithoutContract
            Exit Function
        End If

        If ValidateScheduleDetailInSchedule = 0 Then ' valido que no se crucen las horas
            If ScheduleInPeriod IsNot Nothing AndAlso ScheduleInPeriod.Count > 0 Then
                For Each schDet As ScheduleDetail In ScheduleInPeriod.FindAll(Function(x) x.DateDetail = day Or x.DateDetail = day.AddDays(-1) Or x.DateDetail = day.AddDays(1)) ' Busco solo si ya ha tenido turnos el dia anterior, el actual y el posterior a el que se va a registrar, para validar que no se cruce los horarios
                    Dim endDate As Date? = Nothing
                    Dim totalHoursToValidate As Double = 0
                    'Validar Novedades
                    For Each itemH As ScheduleDetailHour In detHours

                        If itemH.DateTimeInitial = endDate Or endDate Is Nothing Then
                            totalHoursToValidate = totalHoursToValidate + itemH.TotalNumberHours
                            endDate = itemH.DateTimeEnding
                        Else
                            totalHoursToValidate = 0
                        End If

                        If totalHoursToValidate > 18 Then
                            ValidateScheduleDetailInSchedule = EFailureCauses.ErrorMaximo18Horas
                            Exit Function
                        End If


                        Dim DiH As Date = day
                        Dim DeH As Date = day
                        Dim TiH As TimeSpan = New TimeSpan(itemH.DateTimeInitial.Hour, itemH.DateTimeInitial.Minute, itemH.DateTimeInitial.Second)
                        Dim TeH As TimeSpan = New TimeSpan(itemH.DateTimeEnding.Hour, itemH.DateTimeEnding.Minute, itemH.DateTimeEnding.Second)
                        If itemH.NextDay = True Then ' se suma un dia a la dia si es de un siguiente dia, para tener la fecha exacta
                            DiH = DiH.AddDays(1)
                            DeH = DeH.AddDays(1)
                        End If
                        Dim schDetValidNovelty As ScheduleDetail = ScheduleInPeriod.Find(Function(x) x.DateDetail = DiH.Date And x.TotalNumberHours = 0) ' Validar si hay novedad para ese día
                        If schDetValidNovelty IsNot Nothing Then
                            If schDetValidNovelty.TotalNumberHours = 0 Then
                                Select Case schDetValidNovelty.Letter
                                    Case Is = "I"
                                        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorIncapacidad
                                        Exit Function
                                    Case Is = "V"
                                        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorVacaciones
                                        Exit Function
                                    Case Is = "PV"
                                        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorPermisoVacaciones
                                        Exit Function
                                    Case Is = "L"
                                        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorLicencia
                                        Exit Function
                                    Case Is = "S"
                                        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorSancion
                                        Exit Function
                                End Select
                            End If
                        End If
                    Next

                    Dim endDateAux As Date? = Nothing
                    Dim totalHoursToValidateAux As Double = 0
                    Dim endDatePlus As Date? = Nothing
                    For Each itemSH As ScheduleDetailHour In schDet.ScheduleDetailHour 'recorro lo que encontre para compararlos con los que agregare

                        If itemSH.DateTimeInitial = endDateAux Or endDateAux Is Nothing Then
                            totalHoursToValidateAux = totalHoursToValidateAux + itemSH.TotalNumberHours
                            endDateAux = itemSH.DateTimeEnding
                            'If itemSH.NextDay Then
                            'endDatePlus = endDateAux.Value.AddDays(1)
                            'Else
                            'endDatePlus = endDateAux
                            'End If
                        Else
                            totalHoursToValidateAux = 0
                        End If

                        If totalHoursToValidateAux > 18 Then
                            ValidateScheduleDetailInSchedule = EFailureCauses.ErrorMaximo18Horas
                            Exit Function
                        End If

                        Dim Di As New Date(itemSH.DateTimeInitial.Year, itemSH.DateTimeInitial.Month, itemSH.DateTimeInitial.Day)
                        Dim De As New Date(itemSH.DateTimeEnding.Year, itemSH.DateTimeEnding.Month, itemSH.DateTimeEnding.Day)
                        Dim Ti As TimeSpan = New TimeSpan(itemSH.DateTimeInitial.Hour, itemSH.DateTimeInitial.Minute, itemSH.DateTimeInitial.Second)
                        Dim Te As TimeSpan = New TimeSpan(itemSH.DateTimeEnding.Hour, itemSH.DateTimeEnding.Minute, itemSH.DateTimeEnding.Second)
                        If itemSH.NextDay = True Then ' se suma un dia a la dia si es de un siguiente dia, para tener la fecha exacta
                            Di = Di.AddDays(1)
                            De = De.AddDays(1)
                        End If
                        For Each itemH As ScheduleDetailHour In detHours ' se recorren los detalles de hora de los que van a agregar
                            Dim DiH As Date = day
                            Dim DeH As Date = day
                            Dim TiH As TimeSpan = New TimeSpan(itemH.DateTimeInitial.Hour, itemH.DateTimeInitial.Minute, itemH.DateTimeInitial.Second)
                            Dim TeH As TimeSpan = New TimeSpan(itemH.DateTimeEnding.Hour, itemH.DateTimeEnding.Minute, itemH.DateTimeEnding.Second)
                            If itemH.NextDay = True Then ' se suma un dia a la dia si es de un siguiente dia, para tener la fecha exacta
                                DiH = DiH.AddDays(1)
                                DeH = DeH.AddDays(1)
                            End If

                            If Di.Date = DiH.Date Then ' Se Entra a verificar las horas, solo si los detalles a comparar son del mismo dia
                                For i As Integer = 1 To (TeH - TiH).Hours ' Valido si cualquiera de los horarios se cruza con otros
                                    Dim _hour As TimeSpan = TiH.Add(New TimeSpan(i, 0, 0))
                                    If _hour > Ti And _hour < Te Then
                                        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorHour
                                        Exit Function
                                    End If
                                Next
                            End If
                            Dim schDetValidOtherScheduleDetail As ScheduleDetail = ScheduleInPeriod.Find(Function(x) x.DateDetail = day And x.ScheduleFunctionalUnitId = _schedule.FunctionalUnitId) ' valido que no hayan turnos en la unidad f y fecha que se va a registrar
                            If schDetValidOtherScheduleDetail IsNot Nothing Then
                                ValidateScheduleDetailInSchedule = EFailureCauses.ErrorScheduleDetailExisting
                                Exit Function
                            End If
                        Next
                    Next

                    If schDet.DateDetail > day Then
                        If detHours.LastOrDefault.DateTimeEnding = schDet.ScheduleDetailHour.FirstOrDefault.DateTimeInitial Then
                            If totalHoursToValidate + totalHoursToValidateAux > 18 Then
                                ValidateScheduleDetailInSchedule = EFailureCauses.ErrorMaximo18Horas
                                Exit Function
                            End If
                        End If
                    Else
                        'Se comenta porque desde servicios ya esta la validacion de las 18h seguidas
                        'If (detHours.FirstOrDefault.DateTimeInitial.Hour = endDateAux.Value.Hour) And (detHours.FirstOrDefault.DateTimeInitial.Minute = endDateAux.Value.Minute) Then

                        '    If totalHoursToValidate + totalHoursToValidateAux > 18 Then
                        '        ValidateScheduleDetailInSchedule = EFailureCauses.ErrorMaximo18Horas
                        '        Exit Function
                        '    End If
                        'End If
                    End If
                Next
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo para agregar los nuevos registros de schedule detail a la entidad Schedule
    ''' </summary>
    ''' <param name="item">el registro schedule detail</param>
    ''' <param name="day">el dia del turno</param>
    ''' <remarks></remarks>
    Public Function SetScheduleDetailInSchedule(ByRef _schedule As Schedule, ByVal item As ScheduleDetail, ByVal day As Integer) As Boolean
        SetScheduleDetailInSchedule = True
        Select Case day
            Case Is = 1
                _schedule.ScheduleDetail = item

            Case Is = 2
                _schedule.ScheduleDetail1 = item

            Case Is = 3
                _schedule.ScheduleDetail2 = item

            Case Is = 4
                _schedule.ScheduleDetail3 = item

            Case Is = 5
                _schedule.ScheduleDetail4 = item

            Case Is = 6
                _schedule.ScheduleDetail5 = item

            Case Is = 7
                _schedule.ScheduleDetail6 = item

            Case Is = 8
                _schedule.ScheduleDetail7 = item

            Case Is = 9
                _schedule.ScheduleDetail8 = item

            Case Is = 10
                _schedule.ScheduleDetail9 = item

            Case Is = 11
                _schedule.ScheduleDetail10 = item

            Case Is = 12
                _schedule.ScheduleDetail11 = item

            Case Is = 13
                _schedule.ScheduleDetail12 = item

            Case Is = 14
                _schedule.ScheduleDetail13 = item

            Case Is = 15
                _schedule.ScheduleDetail14 = item

            Case Is = 16
                _schedule.ScheduleDetail15 = item

            Case Is = 17
                _schedule.ScheduleDetail16 = item

            Case Is = 18
                _schedule.ScheduleDetail17 = item

            Case Is = 19
                _schedule.ScheduleDetail18 = item

            Case Is = 20
                _schedule.ScheduleDetail19 = item

            Case Is = 21
                _schedule.ScheduleDetail20 = item

            Case Is = 22
                _schedule.ScheduleDetail21 = item

            Case Is = 23
                _schedule.ScheduleDetail22 = item

            Case Is = 24
                _schedule.ScheduleDetail23 = item

            Case Is = 25
                _schedule.ScheduleDetail24 = item

            Case Is = 26
                _schedule.ScheduleDetail25 = item

            Case Is = 27
                _schedule.ScheduleDetail26 = item

            Case Is = 28
                _schedule.ScheduleDetail27 = item

            Case Is = 29
                _schedule.ScheduleDetail28 = item

            Case Is = 30
                _schedule.ScheduleDetail29 = item

            Case Is = 31
                _schedule.ScheduleDetail30 = item

        End Select
    End Function

    ''' <summary>
    ''' Metodo para marcar como eliminados los registros de schedule detail a la entidad Schedule
    ''' </summary>
    ''' <param name="day">el dia del turno</param>
    ''' <remarks></remarks>
    Public Sub MarkDeleteScheduleDetailInSchedule(_schedule As Schedule, ByVal day As Integer)
        Select Case day
            Case Is = 1
                _schedule.ScheduleDetail.StartTracking()
                _schedule.ScheduleDetail.MarkAsDeleted()
            Case Is = 2
                _schedule.ScheduleDetail1.StartTracking()
                _schedule.ScheduleDetail1.MarkAsDeleted()
            Case Is = 3
                _schedule.ScheduleDetail2.StartTracking()
                _schedule.ScheduleDetail2.MarkAsDeleted()
            Case Is = 4
                _schedule.ScheduleDetail3.StartTracking()
                _schedule.ScheduleDetail3.MarkAsDeleted()
            Case Is = 5
                _schedule.ScheduleDetail4.StartTracking()
                _schedule.ScheduleDetail4.MarkAsDeleted()
            Case Is = 6
                _schedule.ScheduleDetail5.StartTracking()
                _schedule.ScheduleDetail5.MarkAsDeleted()
            Case Is = 7
                _schedule.ScheduleDetail6.StartTracking()
                _schedule.ScheduleDetail6.MarkAsDeleted()
            Case Is = 8
                _schedule.ScheduleDetail7.StartTracking()
                _schedule.ScheduleDetail7.MarkAsDeleted()
            Case Is = 9
                _schedule.ScheduleDetail8.StartTracking()
                _schedule.ScheduleDetail8.MarkAsDeleted()
            Case Is = 10
                _schedule.ScheduleDetail9.StartTracking()
                _schedule.ScheduleDetail9.MarkAsDeleted()
            Case Is = 11
                _schedule.ScheduleDetail10.StartTracking()
                _schedule.ScheduleDetail10.MarkAsDeleted()
            Case Is = 12
                _schedule.ScheduleDetail11.StartTracking()
                _schedule.ScheduleDetail11.MarkAsDeleted()
            Case Is = 13
                _schedule.ScheduleDetail2.StartTracking()
                _schedule.ScheduleDetail12.MarkAsDeleted()
            Case Is = 14
                _schedule.ScheduleDetail13.StartTracking()
                _schedule.ScheduleDetail13.MarkAsDeleted()
            Case Is = 15
                _schedule.ScheduleDetail14.StartTracking()
                _schedule.ScheduleDetail14.MarkAsDeleted()
            Case Is = 16
                _schedule.ScheduleDetail15.StartTracking()
                _schedule.ScheduleDetail15.MarkAsDeleted()
            Case Is = 17
                _schedule.ScheduleDetail16.StartTracking()
                _schedule.ScheduleDetail16.MarkAsDeleted()
            Case Is = 18
                _schedule.ScheduleDetail17.StartTracking()
                _schedule.ScheduleDetail17.MarkAsDeleted()
            Case Is = 19
                _schedule.ScheduleDetail18.StartTracking()
                _schedule.ScheduleDetail18.MarkAsDeleted()
            Case Is = 20
                _schedule.ScheduleDetail19.StartTracking()
                _schedule.ScheduleDetail19.MarkAsDeleted()
            Case Is = 21
                _schedule.ScheduleDetail20.StartTracking()
                _schedule.ScheduleDetail20.MarkAsDeleted()
            Case Is = 22
                _schedule.ScheduleDetail21.StartTracking()
                _schedule.ScheduleDetail21.MarkAsDeleted()
            Case Is = 23
                _schedule.ScheduleDetail22.StartTracking()
                _schedule.ScheduleDetail22.MarkAsDeleted()
            Case Is = 24
                _schedule.ScheduleDetail23.StartTracking()
                _schedule.ScheduleDetail23.MarkAsDeleted()
            Case Is = 25
                _schedule.ScheduleDetail24.StartTracking()
                _schedule.ScheduleDetail24.MarkAsDeleted()
            Case Is = 26
                _schedule.ScheduleDetail25.StartTracking()
                _schedule.ScheduleDetail25.MarkAsDeleted()
            Case Is = 27
                _schedule.ScheduleDetail26.StartTracking()
                _schedule.ScheduleDetail26.MarkAsDeleted()
            Case Is = 28
                _schedule.ScheduleDetail27.StartTracking()
                _schedule.ScheduleDetail27.MarkAsDeleted()
            Case Is = 29
                _schedule.ScheduleDetail28.StartTracking()
                _schedule.ScheduleDetail28.MarkAsDeleted()
            Case Is = 30
                _schedule.ScheduleDetail29.StartTracking()
                _schedule.ScheduleDetail29.MarkAsDeleted()
            Case Is = 31
                _schedule.ScheduleDetail30.StartTracking()
                _schedule.ScheduleDetail30.MarkAsDeleted()
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que calcula el total de Horas de un registro de turnos
    ''' </summary>
    ''' <param name="item">Schedule</param>
    ''' <returns>Numero de horas</returns>
    ''' <remarks></remarks>
    Private Function Calculate_TotalHours(ByVal item As Schedule) As Integer
        Dim totalHoras As Decimal = 0
        If item.ScheduleDetail IsNot Nothing Then
            totalHoras += item.ScheduleDetail.TotalNumberHours
        End If
        If item.ScheduleDetail1 IsNot Nothing Then
            totalHoras += item.ScheduleDetail1.TotalNumberHours
        End If
        If item.ScheduleDetail2 IsNot Nothing Then
            totalHoras += item.ScheduleDetail2.TotalNumberHours
        End If
        If item.ScheduleDetail3 IsNot Nothing Then
            totalHoras += item.ScheduleDetail3.TotalNumberHours
        End If
        If item.ScheduleDetail4 IsNot Nothing Then
            totalHoras += item.ScheduleDetail4.TotalNumberHours
        End If
        If item.ScheduleDetail5 IsNot Nothing Then
            totalHoras += item.ScheduleDetail5.TotalNumberHours
        End If
        If item.ScheduleDetail6 IsNot Nothing Then
            totalHoras += item.ScheduleDetail6.TotalNumberHours
        End If
        If item.ScheduleDetail7 IsNot Nothing Then
            totalHoras += item.ScheduleDetail7.TotalNumberHours
        End If
        If item.ScheduleDetail8 IsNot Nothing Then
            totalHoras += item.ScheduleDetail8.TotalNumberHours
        End If
        If item.ScheduleDetail9 IsNot Nothing Then
            totalHoras += item.ScheduleDetail9.TotalNumberHours
        End If
        If item.ScheduleDetail10 IsNot Nothing Then
            totalHoras += item.ScheduleDetail10.TotalNumberHours
        End If
        If item.ScheduleDetail11 IsNot Nothing Then
            totalHoras += item.ScheduleDetail11.TotalNumberHours
        End If
        If item.ScheduleDetail12 IsNot Nothing Then
            totalHoras += item.ScheduleDetail12.TotalNumberHours
        End If
        If item.ScheduleDetail13 IsNot Nothing Then
            totalHoras += item.ScheduleDetail13.TotalNumberHours
        End If
        If item.ScheduleDetail14 IsNot Nothing Then
            totalHoras += item.ScheduleDetail14.TotalNumberHours
        End If
        If item.ScheduleDetail15 IsNot Nothing Then
            totalHoras += item.ScheduleDetail15.TotalNumberHours
        End If
        If item.ScheduleDetail16 IsNot Nothing Then
            totalHoras += item.ScheduleDetail16.TotalNumberHours
        End If
        If item.ScheduleDetail17 IsNot Nothing Then
            totalHoras += item.ScheduleDetail17.TotalNumberHours
        End If
        If item.ScheduleDetail18 IsNot Nothing Then
            totalHoras += item.ScheduleDetail18.TotalNumberHours
        End If
        If item.ScheduleDetail19 IsNot Nothing Then
            totalHoras += item.ScheduleDetail19.TotalNumberHours
        End If
        If item.ScheduleDetail20 IsNot Nothing Then
            totalHoras += item.ScheduleDetail20.TotalNumberHours
        End If
        If item.ScheduleDetail21 IsNot Nothing Then
            totalHoras += item.ScheduleDetail21.TotalNumberHours
        End If
        If item.ScheduleDetail22 IsNot Nothing Then
            totalHoras += item.ScheduleDetail22.TotalNumberHours
        End If
        If item.ScheduleDetail23 IsNot Nothing Then
            totalHoras += item.ScheduleDetail23.TotalNumberHours
        End If
        If item.ScheduleDetail24 IsNot Nothing Then
            totalHoras += item.ScheduleDetail24.TotalNumberHours
        End If
        If item.ScheduleDetail25 IsNot Nothing Then
            totalHoras += item.ScheduleDetail25.TotalNumberHours
        End If
        If item.ScheduleDetail26 IsNot Nothing Then
            totalHoras += item.ScheduleDetail26.TotalNumberHours
        End If
        If item.ScheduleDetail27 IsNot Nothing Then
            totalHoras += item.ScheduleDetail27.TotalNumberHours
        End If
        If item.ScheduleDetail28 IsNot Nothing Then
            totalHoras += item.ScheduleDetail28.TotalNumberHours
        End If
        If item.ScheduleDetail29 IsNot Nothing Then
            totalHoras += item.ScheduleDetail29.TotalNumberHours
        End If
        If item.ScheduleDetail30 IsNot Nothing Then
            totalHoras += item.ScheduleDetail30.TotalNumberHours
        End If
        Return totalHoras
    End Function

    ''' <summary>
    ''' Metodo para limpiar los valores del detalle de schedule en PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Values_Popup_Detail()
        PopUpEmployeeName = String.Empty
        PopUpTemplate = String.Empty
        PopUpLetter = String.Empty
        PopUpDate = Nothing
        PopUpMatches = Nothing
        PopUpDetHours = Nothing
        INDColApproval.Visible = False
        PopUpEmployeeNumber = String.Empty
        PopupNumDay = 0
        PopUpTemplateHoursNumber = String.Empty
        PopUpDropDownButton = New DropDownButton
        INDTcgDetailsSchedule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDTcgDetailsSchedule.SelectedTabPageIndex = 0
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los valores del detalle de schedule en PopUp de turnos en otras unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Values_Popup_More()
        PopUpMoreEmployeeName = String.Empty
        PopUpMoreDate = Nothing
        PopUpMoreSchDet = Nothing
        PopupMoreNumDay = 0
        PopUpMoreTemplateHoursNumber = String.Empty
        PopUpMoreDropDownButton = New DropDownButton
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los datos del popup de detalle de horas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Values_PopupHours()
        PopupHoursEmployee = String.Empty
        PopupHoursMinNumber = String.Empty
        PopupHoursMaxNumber = String.Empty
        PopupHoursWorkedNumber = String.Empty
        PopupHoursWorkedPercentage = String.Empty
        PopupHoursDetailDatasource = New List(Of DetailHoursByConcept)
        PopupHoursDetailPendingDatasource = Nothing
        INDTcgHours.SelectedTabPageIndex = 0
    End Sub

    ''' <summary>
    ''' Metodo para encontrar los festivos dentro de el actual periodo en el cuadro de turnos
    ''' </summary>
    ''' <param name="day"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Find_Holiday(ByVal day As Integer) As Boolean
        If day <= Date.DaysInMonth(CtrCalendar.YearControl, CtrCalendar.MonthControl) Then
            Dim _date = New Date(CtrCalendar.YearControl, CtrCalendar.MonthControl, day)
            If List_Holiday.Find(Function(x) x.Holiday1 = _date) IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Private Sub INDSlPosition_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlPosition.EditValueChanged
        If INDSlPosition.EditValue IsNot Nothing And INDSlPosition.EditValue IsNot "" Then
            Presenter.LoadEmployeeListBox(INDSlPosition.EditValue)
        Else
            Presenter.LoadEmployeeListBox(Nothing)
        End If
    End Sub

#End Region



End Class

''' <summary>
''' Enumeracion de los posibles casos en los que no se registra un turno
''' </summary>
''' <remarks></remarks>
Public Enum EFailureCauses
    ErrorHour = 1
    ErrorScheduleDetailExisting = 2
    ErrorMaximumTotalHourNumber = 3
    ErrorWithoutContract = 4
    ErrorIncapacidad = 5
    ErrorLicencia = 6
    ErrorSancion = 7
    ErrorVacaciones = 8
    ErrorPermisoVacaciones = 9
    ErrorHoliday = 10
    ErrorMaximo18Horas = 11
End Enum