'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/06/2016
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Payroll.MVP

#End Region

Public Class FrmPopupResumptionHoliday
    Implements IPopupResumptionHoliday

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrResumptionHoliday()
        ctrTmp.SetTotalValues(AddressOf getDays)
        ctrTmp.RefreshTotalValues()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getDays() As Tuple(Of Integer)
        Return New Tuple(Of Integer)(SumValue)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Fecha fin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements IPopupResumptionHoliday.EndDate
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntryDate As Date? Implements IPopupResumptionHoliday.EntryDate
        Get
            Return INDdteEntryDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEntryDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha inicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements IPopupResumptionHoliday.InitialDate
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Dias solicitados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequestedDays As Integer Implements IPopupResumptionHoliday.RequestedDays
        Get
            Return INDtxtRequestedDays.EditValue
        End Get
        Set(value As Integer)
            INDtxtRequestedDays.EditValue = value
        End Set
    End Property

    Private _EmployeeId As Integer
    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeId As Integer
        Get
            Return _EmployeeId
        End Get
        Set(value As Integer)
            _EmployeeId = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim ResumptionHoliday As ResumptionHoliday

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PResumptionHoliday

    ''' <summary>
    ''' Listado de reanudaciones por empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListResumptionHolidayXpo As List(Of ResumptionHolidayXpo)

    ''' <summary>
    ''' Sumatoria de los días pendientes aplazados
    ''' </summary>
    ''' <remarks></remarks>
    Dim SumValue As Integer

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrResumptionHoliday

#End Region

#Region "Event"
    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddResumptionHolidayEventArgs()

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        If ListResumptionHolidayXpo IsNot Nothing AndAlso ListResumptionHolidayXpo.Count > 0 Then 'Si ya hay registros de reanudación se valida que la fecha no este en algun rango
            Dim cont = (From l In ListResumptionHolidayXpo
                            Where ((InitialDate >= l.InitialDate AndAlso InitialDate <= l.EndDate) _
                                 OrElse (EndDate >= l.InitialDate AndAlso EndDate <= l.EndDate) _
                                 OrElse (InitialDate < l.InitialDate) AndAlso (EndDate > l.EndDate))).Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha inicial y la fecha final ya existe en la lista principal"
                Exit Sub
            End If
        End If
        Try
            AsyncLoader(True)
            AssigningValues()
            Using Model As New MResumptionHoliday()
                Dim result = Await Model.SaveResumptionHoliday(ResumptionHoliday)
                If result.StateResult Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    RaiseEvent AddResumptionHolidayEventArgs()
                    Deshacer()
                    LoadList()
                    INDtxtRequestedDays.Focus()
                Else
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    ElseIf result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ResumptionHoliday = Nothing
        Presenter = Nothing
        ListResumptionHolidayXpo = Nothing
        SumValue = Nothing
        ctrTmp = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupResumptionHoliday_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.StatusRecordVisible = True
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar los valores de los controles de dias solicitados y fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtRequestedDays_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtRequestedDays.EditValueChanged, INDdteInitialDate.EditValueChanged
        If RequestedDays <> Nothing AndAlso RequestedDays > 0 AndAlso InitialDate IsNot Nothing Then
            Await CalculateDates()
        Else
            EndDate = Nothing
            EntryDate = Nothing
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupResumptionHoliday_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupResumptionHoliday_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            AsyncLoader(True)
            Presenter = New PResumptionHoliday()
            ResumptionHoliday = New ResumptionHoliday
            LoadList()
            AsyncLoader(False)
            INDtxtRequestedDays.Focus()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Calcula las fechas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CalculateDates() As Task
        Using model As New MVacation("")
            AsyncLoader(True)
            Dim result As ActionResult(Of Tuple(Of Date, Date)) = Await model.CalculateDatesResumption(EmployeeId, RequestedDays, InitialDate)
            AsyncLoader(False)
            If result.StateResult = False Then
                If result.StatusCode = eStatusResult.EXCEPTION Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                ElseIf result.StatusCode = eStatusResult.WARNING Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            Else
                EndDate = result.ObjectEmbbeded.Item1
                EntryDate = result.ObjectEmbbeded.Item2
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With ResumptionHoliday
            .EmployeeId = EmployeeId
            .RequestedDays = RequestedDays
            .InitialDate = InitialDate
            .EndDate = EndDate
            .EntryDate = EntryDate
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        RequestedDays = Nothing
        InitialDate = Nothing
        EndDate = Nothing
        EntryDate = Nothing
        ResumptionHoliday = New ResumptionHoliday
    End Sub

    ''' <summary>
    ''' Carga los listados para validar las fechas y ademas carga la cantidad de días aplazados disponibles para el empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadList()
        ListResumptionHolidayXpo = Presenter.ListResumptionHoliday(EmployeeId)
        Dim ListDays = Presenter.ListVacationsByEmployeeId(EmployeeId)
        SumValue = 0
        If ListDays IsNot Nothing AndAlso ListDays.Count > 0 Then
            SumValue = (From v In ListDays Where v IsNot Nothing Select v.DaysDeferredPending).Sum()
        End If
        ctrTmp.RefreshTotalValues()
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

End Class