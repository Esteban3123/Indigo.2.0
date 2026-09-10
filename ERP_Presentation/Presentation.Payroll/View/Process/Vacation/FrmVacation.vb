'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports System.Threading
Imports Presentation.Reporter

Public Class FrmVacation
    Implements IVacation

#Region "Variables"

    ''' <summary>
    ''' Representa al campo de la tabla de parámetros de nómina
    ''' </summary>
    Dim HolidayWithoutAnticipateIBC As Boolean = False
    ''' <summary>
    ''' Variable donde alamacena los dias de vacaciones restantes
    ''' </summary>
    Dim RemainingVacationDays As Integer

    ''' <summary>
    ''' Variable para almacenar el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private presenter As PVacation
    ''' <summary>
    ''' Variable para almacenar los festivos
    ''' </summary>
    ''' <remarks></remarks>
    Private holidays As Dictionary(Of String, List(Of Holiday)) = New Dictionary(Of String, List(Of Holiday))

    ''' <summary>
    ''' Variable para almacenar los empleados que se van a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private ListEmployeesSave As List(Of Domain.Payroll.Entities.Employee)
    ''' <summary>
    ''' Propiedad para obtener el token que se usara para interrumpir la tarea asincronica cuando se esta digitando los dias 
    ''' solicitados en el proceso vacaciones compensadas
    ''' </summary>
    Private cancelTokenSource As CancellationTokenSource
    ''' <summary>
    ''' Muestra/oculta la pantalla de espera (splash) mientras se genera o abre el reporte.
    ''' </summary>
    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)
#End Region

#Region "ICRUD and IVacation"

    Public WriteOnly Property ActionsOnControls As Boolean Implements IVacation.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Private _vacationData As List(Of Domain.Payroll.Entities.Employee)
    ''' <summary>
    ''' Esta propiedad establece el datasource para el control de vacaciones
    ''' </summary>
    Public Property VacationDataSource As List(Of Domain.Payroll.Entities.Employee) Implements IVacation.VacationDataSource
        Get
            Return _vacationData
        End Get
        Set(value As List(Of Domain.Payroll.Entities.Employee))
            _vacationData = value
            INDgcEmployees.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el datasource para el control de empleados
    ''' </summary>
    Public WriteOnly Property EmployeeDataSource As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IVacation.EmployeeDataSource
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSLookUpEmployee.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el datasource para el control de grupo
    ''' </summary>
    Public WriteOnly Property GroupDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IVacation.GroupDataSource
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLookUpGroup.Properties.DataSource = value
        End Set
    End Property

    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch

    End Sub

    Public Async Sub Buscar() Implements Base.ICrudBase.Buscar
        Await LoadControls()
    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        INDcbCalculate.EditValue = Nothing
        INDcbTypeVacation.EditValue = Nothing
        INDcbTypePayment.EditValue = Nothing
        INDteRequestDays.EditValue = Nothing
        INDlyItemInitialDateVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDdeInitialDateVacation.EditValue = Nothing
        INDlyItemEndDateVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDdeEndDateVacation.EditValue = Nothing
        INDliItemIncorporationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDdeIncorporationDate.EditValue = Nothing
        INDlyItemEnjoyDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDteEnjoyDays.EditValue = Nothing
        INDteResolutionNumber.EditValue = Nothing
        INDDeResolutionDate.EditValue = Nothing
        INDtePendingDays.Text = String.Empty
        INDteBaseLiquidation.EditValue = Nothing
        INDGcVacationDetail.DataSource = Nothing


        INDlyItemVacationStartDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemVacationEndDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemIncorporationDateVacationReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDdteVacationStartDateReal.EditValue = Nothing
        INDdteVacationEndDateReal.EditValue = Nothing
        INDdteIncorporationDateVacationReal.EditValue = Nothing

        ListEmployeesSave = Nothing
        BarraBotonesPopup.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Funcion que guarda los periodos de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar

        Try
            If ValidateData() = False Then
                Return
            End If
            INDDropDownButtonLiquidate.HideDropDown()
            Using model As New MVacation(MyBase.Tag)
                Dim listId = (From e In ListEmployeesSave
                              Select e.Id).ToList()
                Dim initialDate As Nullable(Of Date) = INDdeInitialDateVacation.DateTime
                Dim incorporationDate As Nullable(Of Date) = INDdeIncorporationDate.DateTime
                Dim typeVacation As Byte = INDcbTypeVacation.EditValue
                Dim endDate As Nullable(Of Date)
                If typeVacation = 2 Then 'Disfrutar
                    endDate = incorporationDate.Value.AddDays(-1)
                    Dim listNovelty = Await model.GetNoveltyByListIdEmployeeBetweenDateAsync(listId, initialDate, endDate)
                    Dim listScheduleDetail = Await model.GetScheduleDetailByListEmployeeBetweenDateAsync(listId, initialDate, endDate)
                    If listNovelty.Count > 0 Then
                        Dim formVacation As New FrmVacationDetail(listNovelty)
                        formVacation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        Dim frmTransparent As New FrmTransparent(formVacation, False)
                        frmTransparent.ShowDialog()
                        Return
                    End If
                    If listScheduleDetail.Count > 0 Then 'Si hay detalles en el calendario en el rango de fechas de vacaciones
                        Dim formSchedule As New FrmVacationScheduleDetail(listScheduleDetail)
                        formSchedule.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        Dim frmTransparent As New FrmTransparent(formSchedule, False)
                        frmTransparent.ShowDialog()
                        If formSchedule.ActionOk = False Then
                            Return
                        End If
                    End If
                End If
                AsyncLoader(True)

                '' Añadimos la Fecha de REsolución y Número de la Resolución
                If ListEmployeesSave IsNot Nothing And ListEmployeesSave.Count() > 0 And indigo.IndigoCompanyType = "2" Then

                    If INDteResolutionNumber.EditValue Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "El número de Resolución no se ha diligenciado"
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    If INDDeResolutionDate.EditValue Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "La Fecha de Resolución no se ha diligenciado"
                        AsyncLoader(False)
                        Exit Sub
                    End If


                    For Each objEmployee As Domain.Payroll.Entities.Employee In ListEmployeesSave
                        For Each ObjVacationPeriod As Domain.Payroll.Entities.VacationPeriod In objEmployee.VacationPeriod
                            For Each ObjVacation As Domain.Payroll.Entities.Vacation In ObjVacationPeriod.Vacation
                                If ObjVacation.VacationStartDate = initialDate Then
                                    ObjVacation.ResolutionNumber = INDteResolutionNumber.EditValue
                                    ObjVacation.ResolutionDate = INDDeResolutionDate.EditValue
                                End If
                            Next
                        Next
                    Next

                End If

                'Se asigna los valores de las fechas reales de vacaciones
                ListEmployeesSave(0).VacationPeriod.ToList().ForEach(Sub(x)
                                                                         Dim queryVacation = x.Vacation.Where(Function(y) y.Id = 0)
                                                                         If queryVacation.Count > 0 Then
                                                                             If INDlyItemVacationStartDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                                                                 queryVacation.SingleOrDefault.VacationStartDateReal = INDdteVacationStartDateReal.EditValue
                                                                                 queryVacation.SingleOrDefault.VacationEndDateReal = INDdteVacationEndDateReal.EditValue
                                                                                 queryVacation.SingleOrDefault.IncorporationDateVacationReal = INDdteIncorporationDateVacationReal.EditValue
                                                                             Else
                                                                                 queryVacation.SingleOrDefault.VacationStartDateReal = Nothing
                                                                                 queryVacation.SingleOrDefault.VacationEndDateReal = Nothing
                                                                                 queryVacation.SingleOrDefault.IncorporationDateVacationReal = Nothing
                                                                             End If
                                                                             Exit Sub
                                                                         End If
                                                                     End Sub)

                Dim result = Await model.SaveVacationEmployeeAsync(ListEmployeesSave, typeVacation, initialDate, endDate)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    If RadioGroup1.EditValue = 1 Then
                        PrintReport()
                    End If
                Else
                    If result?.MessageResult?.Count > 0 Then
                        Dim strMessage As String = ""
                        For Each message As MessageResult In result.MessageResult
                            Select Case message.CodeMessage
                                Case "V001"
                                    strMessage &= String.Format(obtenerRecurso(EmpleadoNoPuedeDias, Eform.Vacaciones), message.Parameters) & vbCrLf
                                Case "V002"
                                    strMessage &= String.Format(obtenerRecurso(EmpleadoNotieneDiasPendientes, Eform.Vacaciones), message.Parameters) & vbCrLf
                                Case "V003"
                                    strMessage &= String.Format(obtenerRecurso(EmpleadoEnVacaciones, Eform.Vacaciones), message.Parameters) & vbCrLf
                                Case Else
                                    strMessage &= message.Parameters(0) & vbCrLf
                            End Select
                        Next
                        Mensaje(EeventViewerImages.MensajeError) = strMessage
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
                AsyncLoader(False)
            End Using
            Deshacer()
            INDlyItemLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Buscar()
        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que se ejecuta al dibujar un item del dateEdit de la fecha inicial real de vacaciones
    ''' este evento se usa para marcar los festivos de otro color
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub DrawItem(e As Calendar.CustomDrawDayNumberCellEventArgs)
        If Not e.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.MonthInfo Then Return
        Dim period = e.Date.ToString("MM/yyyy")
        If Not holidays.ContainsKey(period) Then
            Using model As New MHoliday
                Dim initialDate As Date = New Date(e.Date.Year, e.Date.Month, 1)
                Dim EndDate As Date = initialDate.AddMonths(1).AddDays(-1)
                Dim listHolidays = model.ListHolidayBetweenDate(initialDate, EndDate)
                holidays.Add(period, listHolidays)
            End Using
        End If
        Dim isHoliday = holidays(period).FindAll(Function(x) x.Holiday1 = e.Date).Count
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

    ''' <summary>
    ''' Metodo que carga los parámetros de nómina
    ''' </summary>
    Private Sub LoadPayrollSettings()
        Task.Factory.StartNew(Sub()
                                  'Se consulta con xpo los parámetros de nómina
                                  Dim payrollSettings = presenter.LoadPayrollSettings()
                                  If payrollSettings IsNot Nothing Then
                                      HolidayWithoutAnticipateIBC = payrollSettings.HolidayWithoutAnticipateIBC
                                  End If
                              End Sub)
    End Sub

    Private Async Sub CancelarSolicituds()
        Dim vacation = CType(INDgvVacationDetail.GetFocusedRow(), Vacation)
        If vacation.StateIncorporation = 1 Then 'Normal

            Dim ObjTmpContract = vacation.VacationPeriod.Employee.Contract.Where(Function(x) x.Valid = True And x.Status = 1).FirstOrDefault()

            If vacation.State = 1 Or (vacation.State = 3 And vacation.TypePayment = 2 And ObjTmpContract.Group.NextDateLiquidation = vacation.LiquidationDate) Then 'Esperando Pago o Aplazadas
                If MessageIndigo.Show(obtenerRecurso(EstaSeguroCancelarVacacion, Eform.Vacaciones), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using model As New MVacation(MyBase.Tag)
                        AsyncLoader(True)
                        Dim result = Await model.CancelRequestAsync(vacation)
                        AsyncLoader(False)
                        If result.StateResult = False And result.MessageResult.Count > 0 Then
                            Dim strMessage As String = ""
                            For Each message As MessageResult In result.MessageResult
                                If message.CodeMessage = "-001" Then
                                    strMessage = "• " & obtenerRecurso(SolicitudYaEstanPagas, Eform.Vacaciones) & vbCrLf
                                End If
                            Next
                            Mensaje(EeventViewerImages.MensajeError) = strMessage
                        Else
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(SolicitudCanceladaCorrectamente, Eform.Vacaciones)
                        End If
                    End Using
                    Buscar()
                End If
            End If
        End If
    End Sub

    Private Async Sub IngresoForzosos()
        Dim vacation = CType(INDgvVacationDetail.GetFocusedRow(), Vacation)

        Dim StarDate As Date = vacation.VacationStartDate
        Dim EndDate As Date = vacation.VacationEndDate

        If vacation.StateIncorporation = 1 Then 'Normal
            If vacation.State = 1 OrElse (vacation.State = 2 And vacation.TypeVacation <> 1 And vacation.TypeVacation <> 3) Then 'Pagadas y diferente a Liquidar y permiso con cargo a vacaciones
                If MessageIndigo.Show(obtenerRecurso(EstaSeguroReingresoVacacion, Eform.Vacaciones), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                    Using model As New MVacation(MyBase.Tag)
                        Dim ResumptionHoliday = Await model.getResumptionHolidayByEmployeeId(vacation.VacationPeriod.EmployeeId)

                        If ResumptionHoliday.ObjectEmbbeded IsNot Nothing AndAlso ResumptionHoliday.StateResult = True Then

                            If vacation.DaysDeferredPending > 0 Then
                                StarDate = ResumptionHoliday.ObjectEmbbeded.LastOrDefault.InitialDate
                                EndDate = ResumptionHoliday.ObjectEmbbeded.LastOrDefault.EndDate
                            End If

                        End If

                    End Using


                    Dim frmForced As FrmForcedEntry = New FrmForcedEntry(StarDate, EndDate, indigo.IndigoCompanyType, vacation.State)
                    frmForced.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    Dim frmTransparent As New FrmTransparent(frmForced, False)
                    frmTransparent.ShowDialog()
                    If frmForced.Accept = True Then
                        Dim dateEntry As Date = frmForced.INDdeDateEntry.DateTime
                        Dim ResolutionNumber As String = frmForced.INDTeResolutionNumberForceIngress.EditValue
                        Dim ResolutionDate As Date = frmForced.INDdeDateEntry.DateTime
                        Dim IncomeType As Integer = frmForced.INDsleIncomeType.EditValue
                        vacation.ForceEntryResolutionDate = ResolutionDate
                        vacation.ForceEntryResolutionNumber = ResolutionNumber
                        vacation.IncomeType = IncomeType
                        Using model As New MVacation(MyBase.Tag)
                            AsyncLoader(True)
                            Dim result = Await model.ForceEntryAsync(vacation, dateEntry)

                            If result.StateResult = True Then
                                Mensaje(EeventViewerImages.Informacion) = "Se actualizó correctamente"
                            End If

                            AsyncLoader(False)
                            Buscar()
                        End Using
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Imp()

    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Print)
        ListActions.Add(eAcciones.ForcedEntry)
        ListActions.Add(eAcciones.CancelRequest) 'Esta opcion se habilita para cuando las vacaciones son de estado esperando pago

        IndigoGridView1.SetListAcction(INDgvVacationDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvVacationDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Public Sub PrintReport()

        Dim DateInitialSearch As Date
        Dim DateEndSearch As Date

        If INDdeInitialDateVacation.EditValue Is Nothing Then
            DateInitialSearch = New Date(1, 1, 1)
        Else
            DateInitialSearch = INDdeInitialDateVacation.EditValue
        End If

        If INDdeEndDateVacation.EditValue Is Nothing Then
            DateEndSearch = New Date(1, 1, 1)
        Else
            DateEndSearch = INDdeEndDateVacation.EditValue
        End If

        Me.BarraBotones.PrintReport(PrintReportAction.ViewPrinting, INDSLookUpEmployee.EditValue, 0, DateInitialSearch, DateEndSearch, INDSLookUpEmployee.EditValue, Nothing, Nothing, 1)
    End Sub

    ''' <summary>
    ''' Activa o desactiva los controles necesarios para los filtros
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Private Sub ActivateFilter(value As Byte)
        Select Case value
            Case 1
                INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSLookUpGroup.EditValue = Nothing
            Case 2
                INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSLookUpEmployee.EditValue = Nothing
        End Select
    End Sub

    Private Async Function LoadControls() As Task
        AsyncLoader(True)
        If INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Await presenter.loadPeriodVacation(1, INDSLookUpEmployee.EditValue)
        ElseIf INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Await presenter.loadPeriodVacation(2, INDSLookUpGroup.EditValue)
        End If
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Funcion para validar que los campos minimos esten llenos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateData() As Boolean
        If INDcbCalculate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FaltaCampo, Eform.Incapacidades), INDlyItemCalculate.Text)
            Return False
        End If
        If INDcbTypeVacation.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FaltaCampo, Eform.Incapacidades), INDlyItemTypeVacation.Text)
            Return False
        ElseIf INDcbTypePayment.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FaltaCampo, Eform.Incapacidades), INDlyItemTypePayment.Text)
            Return False
        ElseIf INDcbTypeVacation.EditValue = 2 And INDdeInitialDateVacation.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FaltaCampo, Eform.Incapacidades), INDlyItemInitialDateVacation.Text)
            Return False
        End If
        If INDteRequestDays.EditValue Is Nothing OrElse INDteRequestDays.EditValue = "" Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.FaltaCampo, Eform.Incapacidades), INDlyItemRequestDays.Text)
            Return False
        End If
        If ListEmployeesSave Is Nothing OrElse ListEmployeesSave.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.NoHayEmpleadosSeleccionados, Eform.Vacaciones)
            Return False
        End If
        If INDlyItemVacationStartDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDdteVacationStartDateReal.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar la fecha inicial real vacaciones"
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Funcion la cual se encarga de obtener los festivos de un mes especifico
    ''' </summary>
    ''' <param name="dictionary"></param>
    ''' <param name="dateMonth"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function loadHolidayDate(dictionary As Dictionary(Of String, List(Of Holiday)), dateMonth As Date)
        Dim period = dateMonth.ToString("MM/yyyy")
        If Not dictionary.ContainsKey(period) Then
            Using model As New MHoliday
                Dim DateOne As Date = New Date(dateMonth.Year, dateMonth.Month, 1)
                Dim DateEndMonth As Date = DateOne.AddMonths(1).AddDays(-1)
                Dim listHolidays = model.ListHolidayBetweenDate(DateOne, DateEndMonth)
                holidays.Add(period, listHolidays)
            End Using
        End If
        Return dictionary
    End Function

    ''' <summary>
    ''' Funcion que obtiene la imagen que debe ir asociada a una vacacion
    ''' </summary>
    ''' <param name="vacation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getImageVacation(ByVal vacation As VacationPeriod) As Image
        If vacation IsNot Nothing Then
            If vacation.VacationDays = vacation.PendingDays Then
                Return My.Resources.verde_16x16
            ElseIf vacation.PendingDays = 0 Then
                Return My.Resources.rojo_16x16
            Else
                Return My.Resources.amarillo_16x16
            End If
        End If
    End Function

    ''' <summary>
    ''' Funcion utilizada para calcular los valores de las vacaciones de un empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CalcularVacaciones(Optional calculateSinceHolidayWithoutAnticipateIBC As Boolean = False) As Task
        If INDcbCalculate.EditValue Is Nothing Or INDcbTypeVacation.EditValue Is Nothing Or INDcbTypePayment.EditValue Is Nothing Or
            (INDteRequestDays.EditValue Is Nothing OrElse INDteRequestDays.EditValue = "" OrElse CType(INDteRequestDays.EditValue, Integer) = 0) Then
            Return
        End If
        If (INDcbTypeVacation.EditValue = 2 Or INDcbTypeVacation.EditValue = 4) And INDdeInitialDateVacation.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la fecha de inicio "
            Return
        End If
        Dim listEmployeeApply = VacationDataSource.FindAll(Function(x) x.Apply = True)
        If listEmployeeApply Is Nothing OrElse listEmployeeApply.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un empleado"
            ListEmployeesSave = Nothing
            Return
        End If
        Dim requestDays As Integer = Integer.Parse(INDteRequestDays.Text)
        Dim typeCalculate As Byte = Byte.Parse(INDcbCalculate.EditValue)
        Dim typeVacation As Byte = Byte.Parse(INDcbTypeVacation.EditValue)
        Dim typePayment As Nullable(Of Byte) = INDcbTypePayment.EditValue

        Dim initialDate As Date
        If calculateSinceHolidayWithoutAnticipateIBC Then
            initialDate = INDdteVacationStartDateReal.DateTime.Date
        Else
            initialDate = INDdeInitialDateVacation.DateTime.Date
        End If

        Dim actionResult As ActionMessageResult(Of List(Of Domain.Payroll.Entities.Employee))
        AsyncLoader(True)
        Using model As New MVacation(MyBase.Tag)
            actionResult = Await model.RequestVacationEmployees(listEmployeeApply, requestDays, typeCalculate, typeVacation, typePayment, initialDate)
        End Using
        AsyncLoader(False)
        If actionResult.StateResult = False Then
            Dim StrMessage As String = ""
            For Each message As MessageResult In actionResult.MessageResult
                Select Case message.CodeMessage
                    Case "-001"
                        StrMessage &= "• " & String.Format(obtenerRecurso(Eresources.EmpleadoNoPuedeDias, Eform.Vacaciones), message.Parameters) & vbCrLf
                    Case "-002"
                        StrMessage &= "• " & String.Format(obtenerRecurso(Eresources.EmpleadoNotieneDiasPendientes, Eform.Vacaciones), message.Parameters) & vbCrLf
                    Case "-003"
                        StrMessage &= "• " & String.Format(obtenerRecurso(Eresources.EmpleadoEnVacaciones, Eform.Vacaciones), message.Parameters) & vbCrLf
                    Case "DONTAPPLY"
                        StrMessage &= "• " & message.Parameters(0).ToString & vbCrLf
                End Select
            Next
            Mensaje(EeventViewerImages.Advertencia) = StrMessage
            If calculateSinceHolidayWithoutAnticipateIBC = False Then
                ListEmployeesSave = Nothing
            End If
            Return
        Else
            If calculateSinceHolidayWithoutAnticipateIBC = False Then
                ListEmployeesSave = actionResult.ObjectEmbbeded
            End If
        End If
        Dim vacation As Vacation = Nothing
        actionResult.ObjectEmbbeded(0).VacationPeriod.ToList().ForEach(Sub(x)
                                                                           Dim queryVacation = x.Vacation.Where(Function(y) y.Id = 0)
                                                                           If queryVacation.Count > 0 Then
                                                                               vacation = queryVacation.SingleOrDefault
                                                                               Exit Sub
                                                                           End If
                                                                       End Sub)
        If calculateSinceHolidayWithoutAnticipateIBC = False Then
            If listEmployeeApply.Count = 1 And vacation IsNot Nothing Then ' Si solo tienen un empleado seleccionado
                INDGcVacationDetail.Enabled = True
                INDGcVacationDetail.DataSource = vacation.VacationDetail.ToList()

                INDteBaseLiquidation.EditValue = vacation.BaseLiquidation
                INDteVacationValueNet.EditValue = vacation.VacationValueNet
            End If
            INDdeEndDateVacation.DateTime = vacation.VacationEndDate
            INDdeIncorporationDate.DateTime = vacation.IncorporationDate
            INDteEnjoyDays.Text = vacation.EnjoyDays
        Else
            INDdteVacationEndDateReal.DateTime = vacation.VacationEndDate
            INDdteIncorporationDateVacationReal.DateTime = vacation.IncorporationDate
        End If
    End Function

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        INDColValorVacaciones = Window.Utils.FormatGrid(INDColValorVacaciones, _currencyAbbreviation)
        INDColValorVacacionesNetas = Window.Utils.FormatGrid(INDColValorVacacionesNetas, _currencyAbbreviation)
        changeNumericFormatByCurrency(numberFormat, INDPopupControlContainerLiquidation.Controls)
        GridColumn8 = Window.Utils.FormatGrid(GridColumn8, _currencyAbbreviation)
        GridColumn9 = Window.Utils.FormatGrid(GridColumn9, _currencyAbbreviation)

    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        HolidayWithoutAnticipateIBC = Nothing
        presenter = Nothing
        holidays = Nothing
        ListEmployeesSave = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando se carga el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmVacation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PVacation(Me)

        SetCurrencyFormat(presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        'Se carga el parametro HolidayWithoutAnticipateIBC
        LoadPayrollSettings()

        Me.BarraBotones.Minimizar(True)
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndoAndPrint)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)

        'Valido si la entidad es pública para Mostrar el Control del Número de Resolución
        If indigo.IndigoCompanyType = "2" Then
            INDlyItemResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        AddActionsColumns()
    End Sub

    Private Sub FrmVacation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If RadioGroup1.Enabled Then
            RadioGroup1.Focus()
        End If
    End Sub
#End Region

#Region "SelectedIndexChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el index del radio grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroup1.SelectedIndexChanged
        Dim radio As RadioGroup = CType(sender, RadioGroup)
        ActivateFilter(radio.EditValue)
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDcbTypePayment_EditValueChanged(sender As Object, e As EventArgs) Handles INDcbTypePayment.EditValueChanged
        Await CalcularVacaciones()
        INDcbTypePayment.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el tipo de calculo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDcbCalculate_EditValueChanged(sender As Object, e As EventArgs) Handles INDcbCalculate.EditValueChanged
        Await CalcularVacaciones()
        INDcbCalculate.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar la fecha inicial de las vacaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDdeInitialDateVacation_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeInitialDateVacation.EditValueChanged
        Await CalcularVacaciones()
        INDteResolutionNumber.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha inicio real vacaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDdteVacationStartDateReal_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteVacationStartDateReal.EditValueChanged
        Await CalcularVacaciones(True)
        INDdteVacationStartDateReal.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del campo dias requeridos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDteRequestDays_EditValueChanged(sender As Object, e As EventArgs) Handles INDteRequestDays.EditValueChanged

        If cancelTokenSource IsNot Nothing Then
            cancelTokenSource.Cancel()
        End If
        ' Crear un nuevo CancellationTokenSource
        cancelTokenSource = New CancellationTokenSource()
        Dim token = cancelTokenSource.Token

        ' Esperar 1 segundo para ver si el usuario sigue escribiendo
        Try
            Await Task.Delay(1000, token)
            If Not token.IsCancellationRequested Then
                BarraBotonesPopup.BarBtnActualizar.Enabled = False
                ' Llamar a CalcularVacaciones() solo si el usuario ha dejado de escribir durante al menos 1 segundo
                Await CalcularVacaciones()
                BarraBotonesPopup.BarBtnActualizar.Enabled = True
                INDteRequestDays.Focus()
                INDteRequestDays.Select(INDteRequestDays.Text.Length, 0)
            End If
        Catch ex As TaskCanceledException
            AsyncLoader(False)
            'Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cambia un empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSLookUpEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDSLookUpEmployee.EditValueChanged
        Buscar()
        Deshacer()
        INDlyItemLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cambia un grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSLookUpGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSLookUpGroup.EditValueChanged
        Buscar()
        Deshacer()
        INDlyItemLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de el repositorio del check
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemCheckEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit2.EditValueChanged
        Deshacer()
        INDgvEmployee.FocusedColumn = INDgvEmployee.Columns(1)
        Dim listEmployeeApply = VacationDataSource.FindAll(Function(x) x.Apply = True)
        If listEmployeeApply.Count > 0 Then
            INDlyItemLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            BarraBotonesPopup.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
            If listEmployeeApply.Count = 1 Then
                INDlyGroupContributor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Dim pendingDays As Integer = 0
                listEmployeeApply.SingleOrDefault().VacationPeriod.ToList().ForEach(Sub(x)
                                                                                        pendingDays += x.PendingDays
                                                                                        RemainingVacationDays = pendingDays
                                                                                    End Sub)
                INDtePendingDays.Text = pendingDays
            Else
                INDlyGroupContributor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Else
            INDlyItemLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del combo de tipo de vacacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrgTypeVacation_EditValueChanged(sender As Object, e As EventArgs) Handles INDcbTypeVacation.EditValueChanged
        If INDcbTypeVacation.EditValue = 1 Then 'Liquidar
            INDlyItemInitialDateVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDdeInitialDateVacation.EditValue = Nothing
            INDlyItemEndDateVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDdeEndDateVacation.EditValue = Nothing
            INDliItemIncorporationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDdeIncorporationDate.EditValue = Nothing
            INDlyItemEnjoyDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDteEnjoyDays.EditValue = Nothing
        Else ' Disfrutar
            INDlyItemInitialDateVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemEndDateVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliItemIncorporationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemEnjoyDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDcbTypePayment.EditValue = Nothing
        End If
        If INDcbTypeVacation.EditValue = 2 AndAlso HolidayWithoutAnticipateIBC = True Then 'Si es disfrutar y el parámetro en nómina HolidayWithoutAnticipateIBC está en true
            INDlyItemVacationStartDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemVacationEndDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemIncorporationDateVacationReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else 'Sino se ocultan los controles
            INDlyItemVacationStartDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemVacationEndDateReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIncorporationDateVacationReal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        Await CalcularVacaciones()
        INDcbTypeVacation.Focus()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecute mientras cambia el valor de los dias requeridos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDteRequestDays_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDteRequestDays.EditValueChanging
        If VacationDataSource IsNot Nothing Then
            Dim listEmployeeApply = VacationDataSource.FindAll(Function(x) x.Apply = True)
            If listEmployeeApply.Count = 1 And e.NewValue IsNot Nothing AndAlso e.NewValue.ToString() <> "" Then
                Dim pendingDays As Integer = 0
                listEmployeeApply.Item(0).VacationPeriod.ToList.ForEach(Sub(x)
                                                                            pendingDays += x.PendingDays
                                                                            RemainingVacationDays = pendingDays
                                                                        End Sub)
                If CType(e.NewValue, Integer) > pendingDays Then
                    Mensaje(Base.EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(Eresources.NoPuedeSolicitarDias, Eform.Vacaciones), pendingDays.ToString())
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

#End Region

#Region "CustomDrawCell"

    ''' <summary>
    ''' Evento que se dispara al dibijar una celda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvVacationDetail_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles INDgvVacationDetail.CustomDrawCell
        If INDcolInitialVacation.Name = e.Column.Name Or INDcolIncorporationDate.Name = e.Column.Name Then
            Dim vacation = CType(INDgvVacationDetail.GetRow(e.RowHandle), Vacation)
            If vacation IsNot Nothing Then
                If vacation.TypeVacation = 1 Or vacation.TypeVacation = 3 Then 'Liquidar
                    e.DisplayText = obtenerRecurso(NoAplica, Eform.Empleado)
                End If
            End If
        ElseIf INDcolState.Name = e.Column.Name Then
            Select Case e.CellValue
                Case 1
                    e.DisplayText = obtenerRecurso(EsperandoPago, Eform.Vacaciones)
                Case 2
                    e.DisplayText = obtenerRecurso(Pagas, Eform.Vacaciones)
                Case 3 'Aplazado
                    e.DisplayText = "Aplazado"
                Case 4 'Interrumpido
                    e.DisplayText = "Interrumpido"
            End Select
        ElseIf INDcolTypeVacation.Name = e.Column.Name Then
            If e.CellValue IsNot Nothing Then
                If e.CellValue = 3 Then 'Permiso cargo a vacaciones
                    e.DisplayText = obtenerRecurso(PermisoCargoVacaciones, Eform.Vacaciones)
                ElseIf e.CellValue = 5 Then
                    e.DisplayText = obtenerRecurso(InterrupcionVacaciones, Eform.Vacaciones)
                Else
                    e.DisplayText = INDcbTypeVacation.Properties.Items.GetItem(e.CellValue)?.Description
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento que dispara cuando se trata de dibujar una celda en la regilla de periodos de vacaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvVacationPeriod_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles INDgvVacationPeriod.CustomDrawCell
        If e.Column.Name = INDcolVacationPeriodImage.Name Then

            Dim item As New VacationPeriod

            If INDgcEmployees.FocusedView.IsDetailView Then
                item = (TryCast((TryCast(INDgcEmployees.FocusedView, GridView)).GetFocusedRow(), VacationPeriod))
            End If

            If item IsNot Nothing Then
                Dim value = CType(CType(e.Cell, GridCellInfo).RowInfo.RowKey, VacationPeriod)
                Dim image = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
                image.Image = getImageVacation(item)
                e.CellValue = image.Image

                'image.Image = getImageVacation(item)
            End If

        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se ejecuta cuando le dan click s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepositoryPopupContainerEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDRepositoryPopupContainerEdit.ButtonClick
        Dim employee = CType(INDgvEmployee.GetFocusedRow(), Domain.Payroll.Entities.Employee)
        Dim mainView As GridView = INDgvEmployee
        Dim detailView As GridView = TryCast(INDgcEmployees.FocusedView, GridView)
        If detailView IsNot Nothing Then
            Dim vacationPeriod = CType(detailView.GetFocusedRow(), VacationPeriod)
            INDgcDetailPeriod.DataSource = vacationPeriod.Vacation
        End If
    End Sub

#End Region

#Region "DrawItem"

    ''' <summary>
    ''' Evento que se ejecuta al dibujar un item del dateEdit de la fecha inicial
    ''' este evento de usa para marcar los festivos de otro color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeInitialDateVacation_DrawItem(sender As Object, e As Calendar.CustomDrawDayNumberCellEventArgs) Handles INDdeInitialDateVacation.DrawItem
        DrawItem(e)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dibujar un item del dateEdit de la fecha inicial real de vacaiones
    ''' este evento de usa para marcar los festivos de otro color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteVacationStartDateReal_DrawItem(sender As Object, e As Calendar.CustomDrawDayNumberCellEventArgs) Handles INDdteVacationStartDateReal.DrawItem
        DrawItem(e)
    End Sub

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim vacation = CType(INDgvVacationDetail.GetFocusedRow(), Vacation)
        If vacation IsNot Nothing Then
            If vacation.StateIncorporation = 1 Then 'Normal
                Dim popUp = CType(sender, DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit)

                'Se habilita el imprimir
                e.Buttons(0).Visible = True
                e.Buttons(0).Text = "Imprimir"

                'Se deshabilita el botón de aplazar
                e.Buttons(2).Visible = False

                If vacation.State = 1 Then 'Esperando Pago
                    popUp.PopupControl.Size = New Size(popUp.PopupControl.Size.Width, 108)
                    e.Buttons(1).Visible = True
                    e.Buttons(1).Text = obtenerRecurso(CancelarSolicitud, Eform.Vacaciones)
                    'Se habilita el boton de aplazar
                    e.Buttons(2).Visible = True
                    e.Buttons(2).Text = obtenerRecurso(IngresoForzoso, Eform.Vacaciones)
                ElseIf vacation.State = 2 And vacation.TypeVacation <> 1 And vacation.TypeVacation <> 3 Then 'Pagadas y diferente a Liquidar y a permiso con cargo a vacaciones
                    popUp.PopupControl.Size = New Size(popUp.PopupControl.Size.Width, 72)
                    e.Buttons(1).Visible = True
                    e.Buttons(1).Text = obtenerRecurso(IngresoForzoso, Eform.Vacaciones)
                Else
                    popUp.PopupControl.Size = New Size(popUp.PopupControl.Size.Width, 72)
                    e.Buttons(0).Visible = True
                    e.Buttons(0).Text = "Imprimir"
                    e.Buttons(1).Visible = True
                    e.Buttons(1).Text = obtenerRecurso(CancelarSolicitud, Eform.Vacaciones)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Text.ToString
            Case "Ingreso Forzoso"
                IngresoForzosos()
            Case "Cancelar Solicitud"
                CancelarSolicituds()
            Case "Imprimir"
                Dim VacationStartDates As String = INDgvVacationDetail.GetRowCellDisplayText(INDgvVacationDetail.FocusedRowHandle, INDgvVacationDetail.Columns("VacationStartDate")).ToString
                Dim VacationEndDates As String = INDgvVacationDetail.GetRowCellDisplayText(INDgvVacationDetail.FocusedRowHandle, INDgvVacationDetail.Columns("VacationEndDate")).ToString
                If Not waitForm.IsSplashFormVisible Then
                    waitForm.ShowWaitForm()
                End If

                Dim rpt As New rptLiquidationOfSheetVacations()
                AddHandler rpt.AfterPrint, Sub()
                                               If waitForm.IsSplashFormVisible Then
                                                   waitForm.CloseWaitForm()
                                               End If
                                           End Sub
                ReportHelper.ExecuteReport(rpt, Me, Nothing, {Convert.ToDateTime(VacationStartDates), Convert.ToDateTime(VacationEndDates), INDSLookUpEmployee.EditValue, Nothing, Nothing, 1})
        End Select
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Evento que se ejecuta al realizar click en guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_ClickGuardar() Handles BarraBotonesPopup.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar completamente el control de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_Load(sender As Object, e As EventArgs) Handles BarraBotonesPopup.Load
        BarraBotonesPopup.ActualizarPermisosBarra(CType(MyBase.Tag, String))
        BarraBotonesPopup.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al realizar click en el boton deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_ClickDeshacer() Handles BarraBotonesPopup.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
        Me.BarraBotones.RibbonPagEform.Visible = False
        Me.BarraBotones.RibbonPageRejillas.Visible = False
    End Sub

    ''' <summary>
    ''' Obtiene los childsRowHandles
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function

    Private Sub INDgvVacationPeriod_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgvVacationPeriod.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            Dim view = CType(sender, GridView)
            Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Point)
            view.FocusedRowHandle = hitInfo.RowHandle

            Dim groupRow = view.GetParentRowHandle(view.FocusedRowHandle)
            Dim childRows As Integer = GetChildRowsHandles(view, groupRow)

            PopupMenu1.Manager = BarManager1
            PopupMenu1.ShowPopup(INDgvVacationPeriod.GridControl.PointToScreen(e.Point))
        End If
    End Sub

    Private Async Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Try
            Dim item As New VacationPeriod

            If INDgcEmployees.FocusedView.IsDetailView Then
                item = (TryCast((TryCast(INDgcEmployees.FocusedView, GridView)).GetFocusedRow(), VacationPeriod))
            End If

            If item IsNot Nothing AndAlso item.Id > 0 Then
                Dim ListVacation = item.Vacation.ToList()
                If ListVacation.Any(Function(x) x.State = 1 And x.TypePayment = 1) Then
                    'Contabiliza
                    If MessageIndigo.Show("Existen vacaciones pendientes de Confirmar. Al confirmar, se realizará contabilización y generarará el Comprobante de Egreso. Desea realizarlo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Dim immediatePayVacation = ListVacation.Where(Function(x) x.TypePayment = 1 And x.State = 1).ToList()
                        Using model As New MVacation(MyBase.Tag)
                            AsyncLoader(True)
                            Dim objResult = Await model.ConfirmVacationAsync(immediatePayVacation)
                            AsyncLoader(False)

                            If objResult.StateResult = True Then
                                Mensaje(EeventViewerImages.Informacion) = objResult.Message
                                Deshacer()
                                INDlyItemLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                Buscar()
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = objResult.Message
                            End If

                        End Using
                    End If
                ElseIf ListVacation.Any(Function(x) x.State = 2) Then
                    Mensaje(EeventViewerImages.Advertencia) = "El periodo de vacaciones ¡Ya Fue Confirmado!"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "¡Sólo se puede confirmar las vacaciones cuando el tipo de pago es Inmediato!"
                End If
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub




#End Region

End Class