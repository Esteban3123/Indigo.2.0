Imports Domain.Payroll.Entities
Imports System.Drawing
Imports Presentation.Common.MVP
Imports Domain.Entities
Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base


Public Class FrmForcedEntry

#Region "Builder"

    Public Sub New(initialDateParm As Date, endDateParm As Date, CompanyTypeParm As Integer, vacationStateParm As Integer)

        initialDate = initialDateParm
        endDate = endDateParm
        CompanyType = CompanyTypeParm
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        VacationState = vacationStateParm
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Variable para almacenar los festivos
    ''' </summary>
    ''' <remarks></remarks>
    Private holidays As List(Of Holiday) = New List(Of Holiday)()

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Accept As Boolean = False

    Private initialDate As Date

    Private endDate As Date

    Private CompanyType As Integer

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListIncomeType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Representa al estado de las vacaciones, se usa para que el valor del tipo de aplazamiento no se deje modificar segun corresponda, 
    ''' con valor 1 no se deja modificar y el valor es aplazamiento, con valor 2 se deja modificar a como el usuario escoja
    ''' </summary>
    Private VacationState As Integer

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        holidays = Nothing
        Accept = Nothing
        initialDate = Nothing
        endDate = Nothing
        CompanyType = Nothing
        ListIncomeType = Nothing
        VacationState = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando todo esta cargado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmForcedEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AsyncLoader(True)
        INDdeDateEntry.Properties.MaxValue = endDate
        INDdeDateEntry.Properties.MinValue = initialDate
        Using model As New MHoliday
            holidays = model.ListHolidayBetweenDate(initialDate, endDate)
        End Using
        AsyncLoader(False)

        If CompanyType = 2 Then
            INDLyiResolutionDateForceEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciResolutionNumberForceIngress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        InitializeTuple()

    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        Accept = False
        Me.Close()
    End Sub

    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        Dim errors As New StringBuilder
        If INDdeDateEntry.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha de ingreso")
        End If
        If INDsleIncomeType.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de ingreso")
        End If
        If errors.Length > 0 Then
            Mensaje(Base.EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If
        Accept = True
        Me.Close()
    End Sub

#End Region

#Region "DrawItem"

    Private Sub INDdeDateEntry_DrawItem(sender As Object, e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs) Handles INDdeDateEntry.DrawItem
        If Not e.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.MonthInfo Then Return
        Dim isHoliday = holidays.FindAll(Function(x) x.Holiday1 = e.Date).Count
        If isHoliday > 0 Or e.Date.DayOfWeek = DayOfWeek.Sunday Then
            If e.Selected Then
                e.Graphics.FillRectangle(e.Style.GetBackBrush(e.Cache), e.Bounds)
            End If
            Dim brush As Brush = IIf(e.Highlighted, Brushes.Red, Brushes.Coral)
            'specify formatting attributes for drawing text
            Dim strFormat As StringFormat = New StringFormat()
            strFormat.Alignment = StringAlignment.Far
            strFormat.LineAlignment = StringAlignment.Far

            'draw the day number
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, brush, e.Bounds, strFormat)
            e.Handled = True
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmForcedEntry_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDdeDateEntry.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListIncomeType = New List(Of Tuple(Of Integer, String))
        ListIncomeType.Add(New Tuple(Of Integer, String)(1, "Por Interrupción"))
        ListIncomeType.Add(New Tuple(Of Integer, String)(2, "Aplazamiento"))
        INDsleIncomeType.Properties.DataSource = ListIncomeType.ToList
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

End Class