Imports System.Drawing
Imports DevExpress.XtraBars.Docking2010.Customization
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraEditors.Calendar
Imports DevExpress.XtraScheduler
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Maintenance.MVP

Public Class FrmPrograming

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        INDDnProgramacion.Multiselect = False
        INDDnProgramacion.DateTime = Nothing
        INDDnProgramacion.Selection.Clear()
        INDSleResponsableM.Enabled = False
        INDSleResponsableG.Enabled = False
        indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Properties"
    Dim dateSelected As Date
    Private _readOnlyControls As Boolean
    ''' <summary>
    ''' Listado de fechas de mantenimiento y metrología; 1 - Mantenimiento, 2 - Metrología
    ''' </summary>
    Private datesMaintenanceMetrology As New List(Of Tuple(Of Date, Byte))()
    Public Property PermissionsForm As Dictionary(Of Integer, String)
    Private holidays As Task(Of List(Of Holiday))
    Private indigo As SessionValues
    Public Event ProgramingSaved()
    Private dateNow As Date
    Dim start As Boolean = True
    Public Property ProgramedId As Integer?
    Public Property IsMassive As Boolean
    Public Property FixedAssetPhysicalIdList As List(Of Tuple(Of Integer, String))
    Public Property IsMetrology As Boolean

    Private predicate As Predicate(Of System.Windows.Forms.DialogResult) = AddressOf canCloseFunc
    Private _protocolMSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_MaintenanceProtocol
    Private ResponsibleTypeId As Integer
    Private _protocolGSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_MaintenanceProtocol
    Private ResponsibleTypeGId As Integer
    Private maintenancePlanProgramated As New List(Of MaintenancePlanProgramated)()
#End Region

#Region "Handlers"
    ''' <summary>
    ''' load del modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPrograming_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using model As New Controls.MVP.MformBase()
            dateNow = model.GetDateServer()
            INDDeFechaInicialG.Properties.MinValue = dateNow
            INDDeFechaInicialM.Properties.MinValue = dateNow
        End Using
        initGridLookup()
        INDLcgMetrology.HideControl(Not Me.IsMetrology)
        If ProgramedId IsNot Nothing AndAlso ProgramedId.Value > 0 Then
            loadControls()
        End If
    End Sub

    ''' <summary>
    ''' carga datasource protocolos de mantenimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProtocoloM_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProtocoloM.QueryPopUp
        If INDSleProtocoloM.Properties.DataSource Is Nothing Then
            INDSleProtocoloM.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceProtocolByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' carga datasource de responsables de mantenimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleResponsableM_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsableM.QueryPopUp
        If INDSleResponsableM.Properties.DataSource Is Nothing Then
            INDSleResponsableM.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsibleByType(ResponsibleTypeId, True)
        End If
    End Sub

    ''' <summary>
    ''' carga datasource de protocolos de metrologia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProtocoloG_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProtocoloG.QueryPopUp
        If INDSleProtocoloG.Properties.DataSource Is Nothing Then
            INDSleProtocoloG.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceProtocolByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' carga datasource de responsables de metrología
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleResponsableG_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsableG.QueryPopUp
        If INDSleResponsableG.Properties.DataSource Is Nothing Then
            INDSleResponsableG.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsibleByType(ResponsibleTypeGId, True)
        End If
    End Sub

    ''' <summary>
    ''' rowclick para capturar el objeto antes de que pierda el foco
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvProtocolM_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDGvProtocolM.RowClick
        Dim obj = DirectCast(INDGvProtocolM.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _protocolMSelected = obj.OriginalRow
            ResponsibleTypeId = _protocolMSelected.ResponsibleTypeId.Id
            INDSleResponsableM.Properties.DataSource = Nothing
            INDSleResponsableM.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' rowclick para capturar el objeto antes de que pierda el foco
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvProtocolG_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDGvProtocolG.RowClick
        Dim obj = DirectCast(INDGvProtocolG.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _protocolGSelected = obj.OriginalRow
            ResponsibleTypeGId = _protocolGSelected.ResponsibleTypeId.Id
            INDSleResponsableG.Properties.DataSource = Nothing
            INDSleResponsableG.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' editvalue changed del protocolo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProtocoloM_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProtocoloM.EditValueChanged
        INDSleResponsableM.Enabled = INDSleProtocoloM.EditValue IsNot Nothing
        If INDSleProtocoloM.EditValue Is Nothing Then
            INDSleResponsableM.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' editvalue changed del protocolo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProtocoloG_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProtocoloG.EditValueChanged
        INDSleResponsableG.Enabled = INDSleProtocoloG.EditValue IsNot Nothing
        If INDSleProtocoloG.EditValue Is Nothing Then
            INDSleResponsableG.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' calcula en el calendario los dias con mantenimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnCalcularM_Click(sender As Object, e As EventArgs) Handles INDBtnCalcularM.Click
        Dim resultValidation = ValidateControlsMaintenance()
        If Not resultValidation.Item1 Then
            ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = resultValidation.Item2
            Return
        End If
        Dim totalActivosPorDia As Integer = 0
        Using modal As New ProgrammingOptionsModal()
            modal.ProtocolCode = INDSleProtocoloM.Text.Split("-")(0).Trim()
            modal.ResponsibleCode = INDSleResponsableM.Text.Split("-")(0).Trim()
            modal.TotalFixedAsset = FixedAssetPhysicalIdList.Count
            modal.PermissionsForm = PermissionsForm
            Dim transparent As New FrmTransparent(modal, False)
            If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If
            totalActivosPorDia = modal.TotalFixedAssetPerDay
        End Using

        Dim GE As Integer = CInt(INDGleFuncionEquipo.EditValue) + CInt(INDGleRegistroAsociadoAplicacion.EditValue) + CInt(INDGleRequerimientoMtto.EditValue) + CInt(INDGleAntecedentes.EditValue)
        Dim periodicidad As Integer = calcularFrecuencia(GE) ' Periodicidad en meses

        Dim incluyeSabados As Boolean = CBool(INDChkSaturdays.EditValue)
        Dim incluyeDomingos As Boolean = CBool(INDChkSundays.EditValue)
        Dim incluyeFestivos As Boolean = CBool(INDChkHolidays.EditValue)

        Dim fechaInicial As Date = INDDeFechaInicialM.EditValue
        Dim fechaFin As Date = INDDeFechaFinM.EditValue

        If INDDeFechaFinM.EditValue Is Nothing Then
            Dim duracion As Integer = CInt(INDSpnDuracionM.EditValue)
            Dim unidad As Integer = CInt(INDGleDuracionUnidadM.EditValue)
            Select Case unidad
                Case 1
                    fechaFin = fechaInicial.AddDays(duracion)
                Case 2
                    fechaFin = fechaInicial.AddMonths(duracion).AddDays(-1)
                Case 3
                    fechaFin = fechaInicial.AddYears(duracion).AddDays(-1)
            End Select
        End If
        datesMaintenanceMetrology.FindAll(Function(m) m.Item2 = 1) _
            .Select(Function(m) m.Item1).ToList().ForEach(Sub(item)
                                                              INDDnProgramacion.Selection.Remove(item)
                                                          End Sub)
        Await searchHolidays(fechaInicial, fechaFin)
        If Not incluyeFestivos AndAlso (holidays Is Nothing OrElse Not holidays.IsCompleted) Then
            Await holidays
        End If

        ' Elimino las fechas de mantenimiento
        datesMaintenanceMetrology.RemoveAll(Function(m) m.Item2 = 1)
        Dim myDatesCollection = INDDnProgramacion.Selection
        If myDatesCollection Is Nothing OrElse myDatesCollection.Count = 0 Then
            myDatesCollection = New DevExpress.XtraEditors.Controls.DatesCollection()
        End If
        Dim fechaCalendario As Date = fechaInicial.Date

        maintenancePlanProgramated.RemoveAll(Function(m) m.ProgramType = 1)

        Dim counter As Integer = 0
        While fechaCalendario <= fechaFin
            fechaCalendario = fechaInicial.Date.AddMonths(counter)
            If fechaCalendario <= fechaFin Then

                Dim contadorActivos As Integer = 0
                For Each physical In FixedAssetPhysicalIdList
                    Dim ajustado As Boolean = False
                    Dim fechaCalendarioIncio As Date = fechaCalendario
                    While (Not ajustado AndAlso fechaCalendario <= fechaCalendarioIncio.AddMonths(1))
                        If (incluyeSabados OrElse (Not incluyeSabados AndAlso fechaCalendario.DayOfWeek <> DayOfWeek.Saturday)) _
                            AndAlso (incluyeDomingos OrElse (Not incluyeDomingos AndAlso fechaCalendario.DayOfWeek <> DayOfWeek.Sunday)) _
                            AndAlso (incluyeFestivos OrElse (Not incluyeFestivos AndAlso (holidays.Result Is Nothing OrElse Not holidays.Result.Select(Function(m) m.Holiday1).ToList().Contains(fechaCalendario)))) Then

                            Dim programation As New Domain.Entities.MaintenancePlanProgramated()
                            With programation
                                .FixedAssetPhysicalId = physical.Item1
                                .FixedAssetPhysicalAssetName = physical.Item2
                                .TypeName = "Mantenimiento"
                                .DateProgramated = fechaCalendario
                                .ProgramType = 1
                                .Notificated = False
                                .State = 1
                            End With
                            maintenancePlanProgramated.Add(programation)
                            contadorActivos += 1
                            If Not datesMaintenanceMetrology.Any(Function(m) m.Item1 = fechaCalendario AndAlso m.Item2 = 1) Then
                                datesMaintenanceMetrology.Add(New Tuple(Of Date, Byte)(fechaCalendario, 1))
                            End If
                            If contadorActivos >= totalActivosPorDia Then
                                fechaCalendario = fechaCalendario.AddDays(1)
                                contadorActivos = 0
                            End If
                            ajustado = True
                        Else
                            fechaCalendario = fechaCalendario.AddDays(1)
                        End If
                    End While
                Next
            End If
            counter += periodicidad
        End While
        INDDnProgramacion.DateTime = Nothing
        INDDnProgramacion.Refresh()
        INDDnProgramacion.DateTime = datesMaintenanceMetrology.OrderBy(Function(m) m.Item1).FirstOrDefault().Item1.Date.AddMonths(11)
        INDDnProgramacion.Selection.Clear()
        For Each i In datesMaintenanceMetrology
            myDatesCollection.Add(i.Item1)
        Next
        INDDnProgramacion.Selection.AddRange(myDatesCollection)
        INDDnProgramacion.Refresh()
    End Sub

    ''' <summary>
    ''' calcula los dias programados en metrologia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnCalcularG_Click(sender As Object, e As EventArgs) Handles INDBtnCalcularG.Click
        Dim resultValidation = ValidateControlsMetrology()
        If Not resultValidation.Item1 Then
            ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = resultValidation.Item2
            Return
        End If
        Dim totalActivosPorDia As Integer = 0
        Using modal As New ProgrammingOptionsModal()
            modal.ProtocolCode = INDSleProtocoloG.Text.Split("-")(0).Trim()
            modal.ResponsibleCode = INDSleResponsableG.Text.Split("-")(0).Trim()
            modal.TotalFixedAsset = FixedAssetPhysicalIdList.Count
            modal.PermissionsForm = PermissionsForm
            Dim transparent As New FrmTransparent(modal, False)
            If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If
            totalActivosPorDia = modal.TotalFixedAssetPerDay
        End Using

        Dim periodicidad As Integer = CInt(INDSpnFecuencia.EditValue)
        Dim unidadP As Integer = CInt(INDGleUnidadFrecuencia.EditValue)
        'Dim incluyeFestivos As Boolean = CInt(INDGleSinoG.EditValue) = 1 'Toca revisar en nomina domingos y festivos por ahora no voy a utilizar esto

        Dim incluyeSabados As Boolean = CBool(INDChkSaturdays.EditValue)
        Dim incluyeDomingos As Boolean = CBool(INDChkSundays.EditValue)
        Dim incluyeFestivos As Boolean = CBool(INDChkHolidays.EditValue)

        Dim fechaInicial As Date = INDDeFechaInicialG.EditValue
        Dim fechaFin As Date = INDDeFechaFinG.EditValue

        If INDDeFechaFinG.EditValue Is Nothing Then
            Dim duracion As Integer = CInt(INDSpnDuracionG.EditValue)
            Dim unidad As Integer = CInt(INDGleDurationUnit.EditValue)
            Select Case unidad
                Case 1
                    fechaFin = fechaInicial.AddDays(duracion)
                Case 2
                    fechaFin = fechaInicial.AddMonths(duracion)
                Case 3
                    fechaFin = fechaInicial.AddYears(duracion)
            End Select
        End If

        datesMaintenanceMetrology.FindAll(Function(m) m.Item2 = 2) _
            .Select(Function(m) m.Item1).ToList().ForEach(Sub(item)
                                                              INDDnProgramacion.Selection.Remove(item)
                                                          End Sub)
        Await searchHolidays(fechaInicial, fechaFin)
        If Not incluyeFestivos AndAlso (holidays Is Nothing OrElse Not holidays.IsCompleted) Then
            Await holidays
        End If
        ' Elimino las fechas de mantenimiento
        datesMaintenanceMetrology.RemoveAll(Function(m) m.Item2 = 2)
        Dim myDatesCollection = INDDnProgramacion.Selection
        If myDatesCollection Is Nothing OrElse myDatesCollection.Count = 0 Then
            myDatesCollection = New DevExpress.XtraEditors.Controls.DatesCollection()
        End If
        Dim fechaCalendario As Date = fechaInicial.Date

        maintenancePlanProgramated.RemoveAll(Function(m) m.ProgramType = 2)

        ' Aca voy
        Dim counter As Integer = 0
        While fechaCalendario <= fechaFin
            Select Case unidadP
                Case 1
                    fechaCalendario = fechaInicial.Date.AddDays(counter)
                Case 2
                    fechaCalendario = fechaInicial.Date.AddMonths(counter)
                Case 3
                    fechaCalendario = fechaInicial.Date.AddYears(counter)
            End Select
            If fechaCalendario <= fechaFin Then

                Dim contadorActivos As Integer = 0
                For Each physical In FixedAssetPhysicalIdList

                    Dim ajustado As Boolean = False
                    Dim fechaCalendarioIncio As Date = fechaCalendario
                    While (Not ajustado AndAlso fechaCalendario <= fechaCalendarioIncio.AddMonths(1))
                        If (incluyeSabados OrElse (Not incluyeSabados AndAlso fechaCalendario.DayOfWeek <> DayOfWeek.Saturday)) _
                            AndAlso (incluyeDomingos OrElse (Not incluyeDomingos AndAlso fechaCalendario.DayOfWeek <> DayOfWeek.Sunday)) _
                            AndAlso (incluyeFestivos OrElse (Not incluyeFestivos AndAlso (holidays.Result Is Nothing OrElse Not holidays.Result.Select(Function(m) m.Holiday1).ToList().Contains(fechaCalendario)))) Then

                            Dim programation As New Domain.Entities.MaintenancePlanProgramated()
                            With programation
                                .FixedAssetPhysicalId = physical.Item1
                                .FixedAssetPhysicalAssetName = physical.Item2
                                .TypeName = "Metrología"
                                .DateProgramated = fechaCalendario
                                .ProgramType = 2 ' Metrología
                                .Notificated = False
                                .State = 1
                            End With
                            maintenancePlanProgramated.Add(programation)
                            contadorActivos += 1
                            If Not datesMaintenanceMetrology.Any(Function(m) m.Item1 = fechaCalendario AndAlso m.Item2 = 2) Then
                                datesMaintenanceMetrology.Add(New Tuple(Of Date, Byte)(fechaCalendario, 2))
                            End If
                            If contadorActivos >= totalActivosPorDia Then
                                fechaCalendario = fechaCalendario.AddDays(1)
                                contadorActivos = 0
                            End If
                            ajustado = True
                        Else
                            fechaCalendario = fechaCalendario.AddDays(1)
                        End If
                    End While

                Next

                'datesMaintenanceMetrology.Add(New Tuple(Of Date, Byte)(fechaCalendario, 2))
            End If
            counter += periodicidad
        End While


        INDDnProgramacion.DateTime = Nothing
        INDDnProgramacion.Refresh()
        INDDnProgramacion.DateTime = datesMaintenanceMetrology.OrderBy(Function(m) m.Item1).FirstOrDefault().Item1.Date.AddMonths(11)
        INDDnProgramacion.Selection.Clear()

        For Each i In datesMaintenanceMetrology
            myDatesCollection.Add(i.Item1)
        Next

        INDDnProgramacion.Selection.AddRange(myDatesCollection)
        INDDnProgramacion.Refresh()
    End Sub

    ''' <summary>
    ''' click nuevo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnNuevo_Click(sender As Object, e As EventArgs) Handles INDBtnNuevo.Click
        CleanControls()
    End Sub

    ''' <summary>
    ''' muestro los colores de los dias programados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDnProgramacion_CustomDrawDayNumberCell(sender As Object, e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs) Handles INDDnProgramacion.CustomDrawDayNumberCell
        Dim rect As New Rectangle(New Point(e.Bounds.Location.X, e.Bounds.Location.Y), e.Bounds.Size)
        Dim offset As Integer = 4
        If e.Date.Day < 10 Then
            offset = 10
        End If

        Dim dn As DateNavigator = CType(sender, DevExpress.XtraScheduler.DateNavigator)
        If dn.Selection IsNot Nothing Then
            For Each dt As Date In dn.Selection
                If e.Date = dt.Date AndAlso Not start Then
                    'aca aparecen las fechas seleccionadas
                    e.Style.Font = New Font(e.Style.Font, FontStyle.Regular)

                    If datesMaintenanceMetrology.FindAll(Function(m) m.Item1 = e.Date).Count = 2 Then
                        Dim rect1 As New Rectangle(New Point(e.Bounds.Location.X, e.Bounds.Location.Y), New Size(e.Bounds.Size.Width, e.Bounds.Size.Height / 2))
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(23, 105, 170)), rect1) ' Mtto

                        Dim rect2 As New Rectangle(New Point(e.Bounds.Location.X, e.Bounds.Location.Y + 7.5), New Size(e.Bounds.Size.Width, e.Bounds.Size.Height / 2))
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(178, 42, 0)), rect2) ' Mtto

                    ElseIf datesMaintenanceMetrology.FindAll(Function(m) m.Item2 = 1).Select(Function(m) m.Item1).Contains(e.Date) Then
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(23, 105, 170)), rect) ' Mtto
                    Else
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(178, 42, 0)), rect) ' Metrologia
                    End If

                    e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, New SolidBrush(Color.White), New PointF(rect.Location.X + offset, rect.Location.Y))
                    e.Handled = True
                    Return
                End If
            Next
        End If

        Dim incluyeSabados As Boolean = CBool(INDChkSaturdays.EditValue)
        Dim incluyeDomingos As Boolean = CBool(INDChkSundays.EditValue)
        Dim incluyeFestivos As Boolean = CBool(INDChkHolidays.EditValue)
        If (incluyeSabados OrElse (Not incluyeSabados AndAlso e.Date.DayOfWeek <> DayOfWeek.Saturday)) _
                    AndAlso (incluyeDomingos OrElse (Not incluyeDomingos AndAlso e.Date.DayOfWeek <> DayOfWeek.Sunday)) _
                    AndAlso (incluyeFestivos OrElse (Not incluyeFestivos AndAlso (holidays Is Nothing OrElse holidays.Result Is Nothing OrElse Not holidays.Result.Select(Function(m) m.Holiday1).ToList().Contains(e.Date)))) Then

        Else
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, New SolidBrush(Color.Red), New PointF(rect.Location.X + offset, rect.Location.Y + 1))
            e.Handled = True
            Return
        End If

        If e.Date = Date.Now.Date OrElse e.Date = INDDnProgramacion.DateTime Then
            e.Style.Font = New Font(e.Style.Font, FontStyle.Regular)
            e.Graphics.FillRectangle(New SolidBrush(Color.White), rect)
            e.Graphics.DrawString(e.Date.Day.ToString() & " ", e.Style.Font, New SolidBrush(Color.Black), New PointF(rect.Location.X + offset, rect.Location.Y))
            e.Handled = True
            start = False
        End If

    End Sub

    ''' <summary>
    ''' mouse click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDnProgramacion_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDDnProgramacion.MouseClick
        Dim hi As CalendarHitInfo = INDDnProgramacion.GetHitInfo(e)
        If e.Button = System.Windows.Forms.MouseButtons.Right AndAlso Not ReadOnlyControls Then
            If INDDnProgramacion.Selection.Contains(hi.HitDate) Then
                If datesMaintenanceMetrology.FindAll(Function(m) m.Item1 = hi.HitDate).Count = 2 Then
                    INDBbiMantenimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    INDBbiMetrologia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                ElseIf datesMaintenanceMetrology.FindAll(Function(m) m.Item2 = 1).Select(Function(m) m.Item1).Contains(hi.HitDate) Then
                    INDBbiMantenimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    INDBbiMetrologia.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Else
                    INDBbiMantenimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    INDBbiMetrologia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
                dateSelected = hi.HitDate
                INDPmActions.ShowPopup(INDDnProgramacion.PointToScreen(e.Location))
            End If
        ElseIf e.Button = System.Windows.Forms.MouseButtons.Left AndAlso INDDnProgramacion.Selection.Contains(hi.HitDate) Then
            If hi.HitObject IsNot Nothing Then
                INDGcActivos.DataSource = maintenancePlanProgramated.FindAll(Function(m) m.DateProgramated = hi.HitDate)
                INDFlyActivos.ShowBeakForm(INDDnProgramacion.PointToScreen(e.Location))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Mover mantenimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiMantenimiento_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiMantenimiento.ItemClick
        Using frm As New MoveDate()
            frm.DateSelected = dateSelected
            Dim frmTransparent As New FrmTransparent(frm, False)
            If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim itemToRemove = datesMaintenanceMetrology.Find(Function(m) m.Item1 = dateSelected AndAlso m.Item2 = 1)
                If datesMaintenanceMetrology.FindAll(Function(m) m.Item1 = itemToRemove.Item1).Count = 1 Then
                    INDDnProgramacion.Selection.Remove(dateSelected)
                End If
                INDDnProgramacion.Selection.Add(frm.NewDateSelected)

                datesMaintenanceMetrology.Remove(New Tuple(Of Date, Byte)(dateSelected, 1))
                datesMaintenanceMetrology.Add(New Tuple(Of Date, Byte)(frm.NewDateSelected, 1))
                INDDnProgramacion.Refresh()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' mover metrologia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiMetrologia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiMetrologia.ItemClick
        Using frm As New MoveDate()
            frm.DateSelected = dateSelected
            Dim frmTransparent As New FrmTransparent(frm, False)
            If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim itemToRemove = datesMaintenanceMetrology.Find(Function(m) m.Item1 = dateSelected AndAlso m.Item2 = 2)
                If datesMaintenanceMetrology.FindAll(Function(m) m.Item1 = itemToRemove.Item1).Count = 1 Then
                    INDDnProgramacion.Selection.Remove(dateSelected)
                End If
                INDDnProgramacion.Selection.Add(frm.NewDateSelected)

                datesMaintenanceMetrology.Remove(New Tuple(Of Date, Byte)(dateSelected, 2))
                datesMaintenanceMetrology.Add(New Tuple(Of Date, Byte)(frm.NewDateSelected, 2))
                INDDnProgramacion.Refresh()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnSave_Click(sender As Object, e As EventArgs) Handles INDBtnSave.Click
        If Not ValidarControles() Then
            Exit Sub
        End If
        If ShowCloseAction("¿Esta seguro que desea guardar la programación?") = System.Windows.Forms.DialogResult.Yes Then
            Try
                INDPnlProgress.BringToFront()
                INDPnlData.SendToBack()

                Dim programingMaintenance As New MaintenancePlanAndMetrology() 'MaintenancePlanProgramated()
                With programingMaintenance
                    '.FixedAssetPhysicalId = FixedAssetPhysicalIdList(0).Item1
                    If datesMaintenanceMetrology.Any(Function(o) o.Item2 = 1) Then
                        .InitialDateMaintenance = INDDeFechaInicialM.EditValue
                        .ProtocolMaintenanceId = INDSleProtocoloM.EditValue
                        .ResponsibleMaintenanceId = INDSleResponsableM.EditValue
                        .ObservationMaintenance = INDTxtObservacionesM.Text
                        .EquipmentFunction = CByte(INDGleFuncionEquipo.EditValue)
                        .RegisterApplication = CByte(INDGleRegistroAsociadoAplicacion.EditValue)
                        .MaintenanceRequirement = CByte(INDGleRequerimientoMtto.EditValue)
                        .Backgrounds = CByte(INDGleAntecedentes.EditValue)
                        .DurationValueMaintenance = IIf(INDSpnDuracionM.EditValue Is Nothing, Nothing, CInt(INDSpnDuracionM.EditValue))
                        .DurationUnitMaintenance = IIf(INDGleDuracionUnidadM.EditValue Is Nothing, Nothing, CByte(INDGleDuracionUnidadM.EditValue))
                        .EndDateMaintenance = INDDeFechaFinM.EditValue
                        .SaturdaysMaintenance = CBool(INDChkSaturdays.EditValue)
                        .SundaysMaintenance = CBool(INDChkSundays.EditValue)
                        .HolidaysMaintenance = CBool(INDChkHolidays.EditValue)
                    End If
                    If datesMaintenanceMetrology.Any(Function(o) o.Item2 = 2) Then
                        .InitialDateMetrology = INDDeFechaInicialG.EditValue
                        .ProtocolMetrologyId = CInt(INDSleProtocoloG.EditValue)
                        .ResponsibleMetrologyId = CInt(INDSleResponsableG.EditValue)
                        .ObservationsMetrology = INDTxtObservacionesG.Text
                        .FrequenceMetrologyValue = CInt(INDSpnFecuencia.EditValue)
                        .FrequenceMetrologyUnit = CByte(INDGleUnidadFrecuencia.EditValue)
                        If INDGleDurationUnit.EditValue IsNot Nothing Then
                            .DurationValueMetrology = CInt(INDSpnDuracionG.EditValue)
                            .DurationUnitMetrology = CByte(INDGleDurationUnit.EditValue)
                        Else
                            .EndDateMetrology = INDDeFechaFinG.EditValue
                        End If
                        '.SaturdaysMetrology = CBool(INDChkSaturdayG.EditValue)
                        '.SundaysMetrology = CBool(INDChkSundayG.EditValue)
                        '.HolidaysMetrology = CBool(INDChkHolidayG.EditValue)
                    End If

                End With
                For Each item In maintenancePlanProgramated
                    'Dim programDate As New MaintenancePlanProgramated()
                    'With programDate
                    '    .DateProgramated = item.Item1
                    '    .ProgramType = item.Item2
                    '    .Notificated = False
                    '    .State = 1
                    'End With
                    programingMaintenance.MaintenancePlanProgramated.Add(item)
                Next

                Using model As New MMaintenancePlanAndMetrology()
                    Dim res = Await model.SaveMaintenancePlanAndMetrology(programingMaintenance)
                    If res.StateResult Then
                        ShowMessage(eStatusResult.SUCCESS) = "Programación Guardada con éxito!!"
                        RaiseEvent ProgramingSaved()
                        Me.Close()
                    Else
                        ShowMessage(eStatusResult.WARNING) = res.Message
                    End If
                    INDPnlProgress.SendToBack()
                    INDPnlData.BringToFront()
                End Using
            Catch ex As Exception
                INDPnlProgress.SendToBack()
                INDPnlData.BringToFront()
                If ex.InnerException.Message IsNot Nothing Then
                    ShowMessage(eStatusResult.WARNING) = ex.InnerException.Message
                Else
                    ShowMessage(eStatusResult.WARNING) = ex.Message
                End If
            End Try
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' busca en la BD los días festivos
    ''' </summary>
    ''' <param name="dateInit"></param>
    ''' <param name="dateEnd"></param>
    Private Function searchHolidays(dateInit As Date, dateEnd As Date) As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MHoliday()
                                             holidays = model.ListHolidayBetweenDateAsync(dateInit, dateEnd) '.ListAllHolidaysbyYearsAsync(year)
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' carga los controles
    ''' </summary>
    Private Async Sub loadControls()
        Try
            INDPnlProgress.BringToFront()
            INDPnlData.SendToBack()
            Using model As New MMaintenancePlanAndMetrology()
                Dim res = Await model.GetMaintenancePlanAndMetrologyById(Me.ProgramedId)
                If res IsNot Nothing Then
                    maintenancePlanProgramated = res.MaintenancePlanProgramated.ToList()
                    With res
                        'FixedAssetPhysicalId = .FixedAssetPhysicalId
                        INDDeFechaInicialM.EditValue = .InitialDateMaintenance
                        INDSleProtocoloM.EditValue = .ProtocolMaintenanceId
                        INDSleResponsableM.EditValue = .ResponsibleMaintenanceId
                        INDTxtObservacionesM.Text = .ObservationMaintenance
                        INDGleFuncionEquipo.EditValue = .EquipmentFunction
                        INDGleRegistroAsociadoAplicacion.EditValue = .RegisterApplication
                        INDGleRequerimientoMtto.EditValue = .MaintenanceRequirement
                        INDGleAntecedentes.EditValue = .Backgrounds
                        INDChkSaturdays.EditValue = .SaturdaysMaintenance
                        INDChkSundays.EditValue = .SundaysMaintenance
                        INDChkHolidays.EditValue = .HolidaysMaintenance
                        If .DurationValueMaintenance IsNot Nothing Then
                            INDRgMaintenance.EditValue = CByte(1)
                        Else
                            INDRgMaintenance.EditValue = CByte(2)
                        End If
                        INDSpnDuracionM.EditValue = .DurationValueMaintenance
                        INDGleDuracionUnidadM.EditValue = .DurationUnitMaintenance
                        INDDeFechaFinM.EditValue = .EndDateMaintenance
                        INDDeFechaInicialG.EditValue = .InitialDateMetrology
                        INDSleProtocoloG.EditValue = .ProtocolMetrologyId
                        INDSleResponsableG.EditValue = .ResponsibleMetrologyId
                        INDTxtObservacionesG.Text = .ObservationsMetrology
                        INDSpnFecuencia.EditValue = .FrequenceMetrologyValue
                        INDGleUnidadFrecuencia.EditValue = .FrequenceMetrologyUnit
                        If .DurationValueMetrology IsNot Nothing Then
                            INDRgMetrology.EditValue = CByte(1)
                        Else
                            INDRgMetrology.EditValue = CByte(2)
                        End If
                        INDSpnDuracionG.EditValue = .DurationValueMetrology
                        INDGleDurationUnit.EditValue = .DurationUnitMetrology
                        INDDeFechaFinG.EditValue = .EndDateMetrology
                        INDSleProtocoloM.Properties.NullText = .ProtocolMaintenanceCodeName
                        INDSleResponsableM.Properties.NullText = .ResponsibleMaintenanceCodeName
                        INDSleProtocoloG.Properties.NullText = .ProtocolMetrologyCodeName
                        INDSleResponsableG.Properties.NullText = .ResponsibleMetrologyCodeName
                    End With
                    ReadOnlyControls = True
                    start = False
                    INDDnProgramacion.SuspendLayout()
                    INDDnProgramacion.BeginUpdate()
                    INDDnProgramacion.DateTime = res.MaintenancePlanProgramated.OrderBy(Function(m) m.DateProgramated).FirstOrDefault().DateProgramated.Date.AddMonths(11)
                    INDDnProgramacion.Refresh()
                    INDDnProgramacion.Selection.Clear()
                    Dim myDatesCollection = INDDnProgramacion.Selection
                    For Each item In res.MaintenancePlanProgramated
                        myDatesCollection.Add(item.DateProgramated)
                        datesMaintenanceMetrology.Add(New Tuple(Of Date, Byte)(item.DateProgramated, item.ProgramType))
                    Next
                    INDDnProgramacion.Selection.AddRange(myDatesCollection)
                    INDDnProgramacion.Refresh()
                    INDDnProgramacion.EndUpdate()
                    INDDnProgramacion.ResumeLayout()
                Else
                    ShowMessage(eStatusResult.WARNING) = "No se encontró una programación para el ítem seleccionado"
                End If
            End Using
        Finally
            INDPnlProgress.SendToBack()
            INDPnlData.BringToFront()
        End Try
    End Sub

    ''' <summary>
    ''' evento que cierra la ventana de confirmación
    ''' </summary>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    Private Shared Function canCloseFunc(parameter As System.Windows.Forms.DialogResult) As Boolean
        Return parameter <> System.Windows.Forms.DialogResult.Cancel
    End Function

    ''' <summary>
    ''' Muestra una ventana de confirmación
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    Private Function ShowCloseAction(Optional msg As String = "") As System.Windows.Forms.DialogResult
        Dim closeAction As New FlyoutAction()
        closeAction.Caption = "VIE ERP"
        closeAction.Description = If(String.IsNullOrEmpty(msg), "¿Realmente desea Salir del programa?", msg)
        closeAction.Commands.Add(FlyoutCommand.Yes)
        closeAction.Commands.Add(FlyoutCommand.No)

        Dim properties As New FlyoutProperties()
        properties.ButtonSize = New Size(100, 40)
        properties.Style = FlyoutStyle.MessageBox
        Return FlyoutDialog.Show(Me, closeAction, properties, predicate)
    End Function

    ''' <summary>
    ''' Muestra un mensaje en pantalla
    ''' </summary>
    ''' <param name="StatusCode"></param>
    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            INDTxtMessages.Text = value
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga los datos de los gridlookups
    ''' </summary>
    Private Sub initGridLookup()
        Dim functionEquipments As New List(Of Tuple(Of Integer, String, String))()
        functionEquipments.Add(New Tuple(Of Integer, String, String)(10, "Terapéutico", "Soporte de Vida"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(9, "Terapéutico", "Cirugía y Cuidados Intensivos"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(8, "Terapéutico", "Terapia física y tratamiento"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(7, "Diagnóstico", "Monitoreo quirúrgico y de cuidados intensivos"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(6, "Diagnóstico", "Otros equipos para el monitoreo de variables fisiológicas y de diagnóstico"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(5, "Analítico", "Laboratorio analítico"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(4, "Analítico", "Accesorios de laboratorio"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(3, "Varios", "Sistema de cómputo y equipos"))
        functionEquipments.Add(New Tuple(Of Integer, String, String)(2, "Varios", "Equipos relacionados con los pacientes y otros equipos"))
        INDGleFuncionEquipo.Properties.DataSource = functionEquipments

        Dim riesgosAplicacion As New List(Of Tuple(Of Integer, String))()
        riesgosAplicacion.Add(New Tuple(Of Integer, String)(5, "Posible Muerte"))
        riesgosAplicacion.Add(New Tuple(Of Integer, String)(4, "Posible lesión del paciente o el usuario"))
        riesgosAplicacion.Add(New Tuple(Of Integer, String)(3, "Terapia inapropiada o falso diagnóstico"))
        riesgosAplicacion.Add(New Tuple(Of Integer, String)(2, "Daños en el equipo"))
        riesgosAplicacion.Add(New Tuple(Of Integer, String)(1, "No se detectan riesgos significativos"))
        INDGleRegistroAsociadoAplicacion.Properties.DataSource = riesgosAplicacion

        Dim requerimientoMtto As New List(Of Tuple(Of Integer, String))()
        requerimientoMtto.Add(New Tuple(Of Integer, String)(5, "Extensivo: calibración rutina y reemplazo de partes"))
        requerimientoMtto.Add(New Tuple(Of Integer, String)(4, "Superiores al promedio"))
        requerimientoMtto.Add(New Tuple(Of Integer, String)(3, "Promedio: verificación del desempeño y pruebas de seguridad"))
        requerimientoMtto.Add(New Tuple(Of Integer, String)(2, "Inferiores al promedio"))
        requerimientoMtto.Add(New Tuple(Of Integer, String)(1, "Mínimos: inspección visual"))
        INDGleRequerimientoMtto.Properties.DataSource = requerimientoMtto

        Dim antecedentes As New List(Of Tuple(Of Integer, String))()
        antecedentes.Add(New Tuple(Of Integer, String)(2, "Significativo: más de una cada seis meses"))
        antecedentes.Add(New Tuple(Of Integer, String)(1, "Moderado: una cada 6-9 meses"))
        antecedentes.Add(New Tuple(Of Integer, String)(0, "Usual: una cada 9-18 meses"))
        antecedentes.Add(New Tuple(Of Integer, String)(3, "Mínimo: una cada 18-30 meses"))
        antecedentes.Add(New Tuple(Of Integer, String)(4, "Insignificante: menos de una en los 30 meses anteriores"))
        INDGleAntecedentes.Properties.DataSource = antecedentes

        Dim units As New List(Of Tuple(Of Integer, String))()
        units.Add(New Tuple(Of Integer, String)(1, "Días"))
        units.Add(New Tuple(Of Integer, String)(2, "Meses"))
        units.Add(New Tuple(Of Integer, String)(3, "Años"))
        INDGleDuracionUnidadM.Properties.DataSource = units
        INDGleUnidadFrecuencia.Properties.DataSource = units
        INDGleDurationUnit.Properties.DataSource = units
    End Sub

    ''' <summary>
    ''' Habilita/Deshabilita los controles
    ''' </summary>
    ''' <returns></returns>
    Public Property ReadOnlyControls As Boolean
        Get
            Return _readOnlyControls
        End Get
        Set(value As Boolean)
            _readOnlyControls = value

            Me.SuspendLayout()
            INDLcRoot.BeginUpdate()

            INDDeFechaInicialM.Enabled = Not value
            INDSleProtocoloM.Enabled = Not value
            INDSleResponsableM.Enabled = Not value
            INDTxtObservacionesM.Enabled = Not value
            INDGleFuncionEquipo.Enabled = Not value
            INDGleRegistroAsociadoAplicacion.Enabled = Not value
            INDGleRequerimientoMtto.Enabled = Not value
            INDGleAntecedentes.Enabled = Not value
            INDChkSaturdays.Enabled = Not value
            INDChkSundays.Enabled = Not value
            INDChkHolidays.Enabled = Not value

            INDSpnDuracionM.Enabled = Not value
            INDGleDuracionUnidadM.Enabled = Not value
            INDDeFechaFinM.Enabled = Not value
            INDDeFechaInicialG.Enabled = Not value
            INDSleProtocoloG.Enabled = Not value
            INDSleResponsableG.Enabled = Not value
            INDTxtObservacionesG.Enabled = Not value
            INDSpnFecuencia.Enabled = Not value
            INDGleUnidadFrecuencia.Enabled = Not value
            INDSpnDuracionG.Enabled = Not value
            INDGleDurationUnit.Enabled = Not value
            INDDeFechaFinG.Enabled = Not value

            INDRgMaintenance.Enabled = Not value
            INDRgMetrology.Enabled = Not value

            INDBtnCalcularG.Enabled = Not value
            INDBtnCalcularM.Enabled = Not value
            INDBtnSave.Enabled = Not value

            INDLcRoot.EndUpdate()
            Me.ResumeLayout()
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        ReadOnlyControls = False

        INDTxtMessages.Text = String.Empty

        INDDnProgramacion.DateTime = Nothing

        INDDeFechaInicialM.EditValue = Nothing
        INDSleProtocoloM.EditValue = Nothing
        INDSleResponsableM.EditValue = Nothing
        INDTxtObservacionesM.EditValue = Nothing
        INDGleFuncionEquipo.EditValue = Nothing
        INDGleRegistroAsociadoAplicacion.EditValue = Nothing
        INDGleRequerimientoMtto.EditValue = Nothing
        INDGleAntecedentes.EditValue = Nothing
        INDChkSaturdays.EditValue = False
        INDChkSundays.EditValue = False
        INDChkHolidays.EditValue = False
        INDRgMaintenance.EditValue = Nothing
        INDSpnDuracionM.EditValue = Nothing
        INDGleDuracionUnidadM.EditValue = Nothing
        INDDeFechaFinM.EditValue = Nothing
        INDDeFechaInicialG.EditValue = Nothing
        INDSleProtocoloG.EditValue = Nothing
        INDSleResponsableG.EditValue = Nothing
        INDTxtObservacionesG.EditValue = Nothing
        INDSpnFecuencia.EditValue = Nothing
        INDGleUnidadFrecuencia.EditValue = Nothing
        INDRgMetrology.EditValue = Nothing
        INDSpnDuracionG.EditValue = Nothing
        INDGleDurationUnit.EditValue = Nothing
        INDDeFechaFinG.EditValue = Nothing

        INDSleProtocoloM.Properties.NullText = ""
        INDSleResponsableM.Properties.NullText = ""

        INDSleProtocoloG.Properties.NullText = ""
        INDSleResponsableG.Properties.NullText = ""

        ProgramedId = Nothing
        INDDnProgramacion.Selection.Clear()
        datesMaintenanceMetrology.Clear()
        maintenancePlanProgramated.Clear()
        INDDnProgramacion.Refresh()

        INDDeFechaInicialM.Focus()
    End Sub

    ''' <summary>
    ''' Calcula la frecuencia
    ''' </summary>
    ''' <param name="GE"></param>
    ''' <returns></returns>
    Private Function calcularFrecuencia(GE As Integer) As Integer 'Periodicity
        If GE < 15 Then
            Return 12 'Periodicity.Anual
        End If
        If GE >= 15 AndAlso GE <= 19 Then
            Return 6 'Periodicity.Semestral
        End If
        If GE > 19 AndAlso GE <= 22 Then
            Return 4 'Periodicity.Cuatrimestral
        End If
        If GE > 22 AndAlso GE <= 25 Then
            Return 3 'Periodicity.Trimestral
        End If
        If GE > 25 Then
            Return 2 'Periodicity.DosMeses
        End If
        Return 0 'Periodicity.NA
    End Function

    ''' <summary>
    ''' valida los controles de metrología
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsMetrology() As Tuple(Of Boolean, String)
        Dim errorM As New System.Text.StringBuilder()
        If INDDeFechaInicialG.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione una fecha Inicial para metrología")
        End If
        If INDSleProtocoloG.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un protocolo para metrología")
        End If
        If INDSleResponsableG.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un responsable para metrología")
        End If
        If INDSpnFecuencia.EditValue Is Nothing OrElse CInt(INDSpnFecuencia.EditValue) <= 0 Then
            errorM.AppendLine("El valor de la frecuencia de metrología debe ser mayor o igual a cero")
        End If
        If INDGleUnidadFrecuencia.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione una unidad de freciencia en metrología")
        End If

        If INDRgMetrology.SelectedIndex = -1 Then
            errorM.AppendLine("Seleccione una opción de Finalización en metrología")
        ElseIf INDRgMetrology.SelectedIndex = 0 Then

            If INDSpnDuracionG.EditValue Is Nothing Then
                errorM.AppendLine("Seleccione un valor de duración en metrología")
            End If
            If INDGleDurationUnit.EditValue Is Nothing Then
                errorM.AppendLine("Seleccione una unidad de duración en metrología")
            End If

        Else

            If INDDeFechaFinG.EditValue Is Nothing Then
                errorM.AppendLine("Seleccione una fecha final en metrología")
            End If

        End If

        Return New Tuple(Of Boolean, String)(errorM.Length = 0, errorM.ToString())
    End Function

    ''' <summary>
    ''' Valida los controles de mantenimiento
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsMaintenance() As Tuple(Of Boolean, String)
        Dim errorM As New System.Text.StringBuilder()
        If INDDeFechaInicialM.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione una fecha Inicial para mantenimiento")
        End If
        If INDSleProtocoloM.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un protocolo para mantenimiento")
        End If
        If INDSleResponsableM.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un responsable para mantenimiento")
        End If
        If INDGleFuncionEquipo.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione una Función del Equipo")
        End If
        If INDGleRegistroAsociadoAplicacion.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un Registro asociado a aplicación")
        End If
        If INDGleRequerimientoMtto.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un requerimiento de Mantenimiento")
        End If
        If INDGleAntecedentes.EditValue Is Nothing Then
            errorM.AppendLine("Seleccione un Antecedente")
        End If
        If INDRgMaintenance.SelectedIndex = -1 Then
            errorM.AppendLine("Seleccione una opción de Finalización en mantenimiento")
        ElseIf INDRgMaintenance.SelectedIndex = 0 Then
            If INDSpnDuracionM.EditValue Is Nothing Then
                errorM.AppendLine("Seleccione un valor de duración en Mantenimiento")
            End If
            If INDGleDuracionUnidadM.EditValue Is Nothing Then
                errorM.AppendLine("Seleccione una unidad de duración en Mantenimiento")
            End If
        Else
            If INDDeFechaFinM.EditValue Is Nothing Then
                errorM.AppendLine("Seleccione una fecha final en Mantenimiento")
            End If
        End If
        Return New Tuple(Of Boolean, String)(errorM.Length = 0, errorM.ToString())
    End Function

    ''' <summary>
    ''' radio button de opcion en mantenimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRgMaintenance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgMaintenance.SelectedIndexChanged
        If INDRgMaintenance.SelectedIndex = -1 Then
            INDSpnDuracionM.Enabled = False
            INDGleDuracionUnidadM.Enabled = False
            INDDeFechaFinM.Enabled = False
        ElseIf INDRgMaintenance.SelectedIndex = 0 Then
            INDSpnDuracionM.Enabled = True
            INDGleDuracionUnidadM.Enabled = True
            INDDeFechaFinM.Enabled = False
        Else
            INDSpnDuracionM.Enabled = False
            INDGleDuracionUnidadM.Enabled = False
            INDDeFechaFinM.Enabled = True
        End If
        INDSpnDuracionM.EditValue = Nothing
        INDGleDuracionUnidadM.EditValue = Nothing
        INDDeFechaFinM.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' radio button de opcion en metrologia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRgMetrology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgMetrology.SelectedIndexChanged
        If INDRgMetrology.SelectedIndex = -1 Then
            INDSpnDuracionG.Enabled = False
            INDGleDurationUnit.Enabled = False
            INDDeFechaFinG.Enabled = False
        ElseIf INDRgMetrology.SelectedIndex = 0 Then
            INDSpnDuracionG.Enabled = True
            INDGleDurationUnit.Enabled = True
            INDDeFechaFinG.Enabled = False
        Else
            INDSpnDuracionG.Enabled = False
            INDGleDurationUnit.Enabled = False
            INDDeFechaFinG.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' valida los controles
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidarControles() As Boolean
        Dim errors As New System.Text.StringBuilder()

        If datesMaintenanceMetrology.Count = 0 OrElse Not datesMaintenanceMetrology.Any() Then
            errors.AppendLine("Debe generar una programación para Mantenimiento o Metrología")
        End If

        'Dim resultValidationM = ValidateControlsMaintenance()
        'If Not resultValidationM.Item1 Then
        '    errors.AppendLine(resultValidationM.Item2)
        'End If
        'Dim resultValidationG = ValidateControlsMetrology()
        'If Not resultValidationG.Item1 Then
        '    errors.AppendLine(resultValidationG.Item2)
        'End If

        'If datesMaintenanceMetrology.Count = 0 OrElse Not datesMaintenanceMetrology.Any(Function(m) m.Item2 = 1) _
        '    OrElse Not datesMaintenanceMetrology.Any(Function(m) m.Item2 = 2) Then
        '    errors.AppendLine("Debe generar una programación para Mantenimiento y Metrología")
        'End If

        If errors.Length > 0 Then
            ShowMessage(eStatusResult.WARNING) = errors.ToString()
            Return False
        End If
        Return True
    End Function
#End Region

End Class