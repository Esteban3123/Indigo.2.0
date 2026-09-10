'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 2025-01-15
'
' Last Modified By : 
' Last Modified On : 
' Description      : Reconocimiento de Costo Proveedores de la Salud (Causaciones)
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MedicalFees.MVP
#End Region

''' <summary>
''' Reconocimiento de Costo Proveedores de la Salud (Causaciones)
''' </summary>
''' <seealso cref="FormBase" />
''' <seealso cref="ICausation" />
Public Class FrmCausation
    Implements ICausation

#Region "Builder"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()

        InitializeComponent()
        Me._ctrRangeDate = New CtrlSelectRangeDate()
        AdditionalControlPanel.Controls.Add(_ctrRangeDate)
        Me._ctrRangeDate.Dock = DockStyle.Fill

        ' Calcular altura considerando DPI/escala (125% en este caso)
        Dim scaleFactor As Single = Me.DeviceDpi / 96.0F
        Dim kpiHeight As Integer = CInt(120 * scaleFactor)

        ' Agregar control de KPIs al panel base (INDPanelControlBase)
        Me._ctrKPIs = New CtrlCausationKPIs()
        Me._ctrKPIs.Dock = DockStyle.None
        Me._ctrKPIs.Height = kpiHeight
        Me._ctrKPIs.Width = Me.INDPanelControlBase.Width
        Me._ctrKPIs.Location = New System.Drawing.Point(0, 0)
        Me._ctrKPIs.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' Agregar al panel principal
        Me.INDPanelControlBase.Controls.Add(Me._ctrKPIs)
        Me._ctrKPIs.BringToFront()

        ' Ajustar el LayoutControl para que esté debajo de los KPIs
        Me.INDLcRoot.Dock = DockStyle.None
        Me.INDLcRoot.Location = New System.Drawing.Point(0, kpiHeight + 5)
        Me.INDLcRoot.Width = Me.INDPanelControlBase.Width
        Me.INDLcRoot.Height = Me.INDPanelControlBase.Height - kpiHeight - 5
        Me.INDLcRoot.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        Me._presenter = New PCausation(Me)
        IndigoGridControl1.SetHideNoRecords(INDGcCausation, True)
        _IsProcesar = True

    End Sub

#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' Presentador de causaciones
    ''' </summary>
    Dim _presenter As PCausation
    ''' <summary>
    ''' Control de seleccion de rango de fechas
    ''' </summary>
    Dim _ctrRangeDate As CtrlSelectRangeDate
    ''' <summary>
    ''' Control de KPIs de causaciones
    ''' </summary>
    Dim _ctrKPIs As CtrlCausationKPIs
    ''' <summary>
    ''' Panel para controles adicionales (Unidad Operativa, etc.)
    ''' </summary>
    Dim _pnlAdditionalControls As Panel
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    Private _IsProcesar As Boolean

    ''' <summary>
    ''' Enum para identificar la vista actual
    ''' </summary>
    Private Enum ECausationView
        ''' <summary>No Reconocidos - Vista para liquidar</summary>
        NoReconocidos = 1
        ''' <summary>Reconocidos - Vista para reversar</summary>
        Reconocidos = 2
        ''' <summary>Pendiente por Causar - Vista con errores</summary>
        PendientePorCausar = 3
    End Enum

    ''' <summary>
    ''' Vista actual del formulario
    ''' </summary>
    Private _currentView As ECausationView = ECausationView.NoReconocidos

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As String Implements ICausation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property IdOperativeUnit As Integer Implements ICausation.IdOperativeUnit
        Get
            Return _idOperativeUnit
        End Get
        Set(value As Integer)
            _idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the causation datasource.
    ''' </summary>
    ''' <value>
    ''' The causation datasource.
    ''' </value>
    Public Property CausationDatasource As XPInstantFeedbackSource Implements ICausation.CausationDatasource
        Get
            Return CType(INDGcCausation.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcCausation.SafeInvoke(Sub()
                                          INDGcCausation.DataSource = value
                                          INDGcCausation.RefreshDataSource()
                                          INDGcCausation.Refresh()
                                          INDGvCausation.HideLoadingPanel()
                                      End Sub)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the date initialize.
    ''' </summary>
    ''' <value>
    ''' The date initialize.
    ''' </value>
    Public Property DateInit As Date Implements ICausation.DateInit
        Get
            Return Me._ctrRangeDate.InitialDate
        End Get
        Set(value As Date)
            Me._ctrRangeDate.InitialDate = value
        End Set
    End Property

#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _ctrRangeDate = Nothing
        _ctrKPIs = Nothing
        _pnlAdditionalControls = Nothing
        _idOperativeUnit = Nothing
        _IsProcesar = Nothing
    End Sub

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmCausation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.ControlHideStatus = False
        Me.Deshacer()
    End Sub

#End Region

#Region "Methods"

    Private Async Function ProcesarReversar() As Task
        INDGvCausation.ShowLoadingPanel()
        Me._presenter.ProcesarReversar(_idOperativeUnit)

        Await ActualizarKPIsAsync()
    End Function

    ''' <summary>
    ''' Procesar y Causar
    ''' </summary>
    Public Async Function ProcesarCausar() As Task
        INDGvCausation.ShowLoadingPanel()
        Me._presenter.ProcesarCausar(_idOperativeUnit)

        Await ActualizarKPIsAsync()
    End Function

    ''' <summary>
    ''' Procesar Pendiente por Causar - Muestra causaciones con errores
    ''' </summary>
    Private Sub ProcesarPendientePorCausar()
        INDGvCausation.ShowLoadingPanel()
        Me._presenter.ProcesarPendientePorCausar(_idOperativeUnit)
    End Sub

    ''' <summary>
    ''' Controla la visibilidad de las columnas según la vista activa
    ''' </summary>
    Private Sub UpdateColumnsVisibility()
        ' La columna "Errores" solo es visible en la vista "Pendiente por Causar"
        ColErrors.Visible = (_currentView = ECausationView.PendientePorCausar)
    End Sub

    ''' <summary>
    ''' Obtiene los SupplierId únicos de las causaciones actuales
    ''' </summary>
    Private Function GetUniqueSuppliersFromGrid() As List(Of SupplierInfo)
        Dim suppliers As New List(Of SupplierInfo)()

        Try
            ' Obtener la lista completa de causaciones de la rejilla
            Dim session As New Session(XpoDefault.DataLayer)
            Dim query = From causation In session.Query(Of ViewListCausationwithoutRecognition)()
                        Where causation.OperatingUnitId = Me._idOperativeUnit
                        Group causation By causation.SupplierId, causation.SupplierName Into Group
                        Select New SupplierInfo With {
                            .SupplierId = SupplierId,
                            .SupplierName = SupplierName,
                            .TotalValue = Group.Sum(Function(x) x.CausationValue),
                            .ServiceCount = Group.Count()
                        }

            suppliers = query.ToList()
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al obtener proveedores: " & ex.Message
        End Try

        Return suppliers
    End Function

    ''' <summary>
    ''' Actualiza los KPIs del formulario luego de los procesos de liquidar y reversar
    ''' </summary>
    Public Async Function ActualizarKPIsAsync(Optional ct As Threading.CancellationToken = Nothing) As Task
        If _ctrKPIs Is Nothing Then Return

        Dim dl = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).DataLayer
        If dl Is Nothing Then
            ' Volver a UI para limpiar
            If _ctrKPIs.IsHandleCreated AndAlso _ctrKPIs.InvokeRequired Then
                _ctrKPIs.BeginInvoke(Sub() _ctrKPIs.LimpiarKPIs())
            Else
                _ctrKPIs.LimpiarKPIs()
            End If
            Return
        End If

        Dim result As KpiResult = Nothing

        Try
            result = Await Task.Run(Function()
                                        ct.ThrowIfCancellationRequested()

                                        ' --- Cálculo en background ---
                                        Using session As New DevExpress.Xpo.Session(dl)
                                            ' Fechas de corte (rango cerrado-abierto por mes)
                                            Dim hoy As Date = Date.Today
                                            Dim inicioMesActual As New Date(hoy.Year, hoy.Month, 1)
                                            Dim inicioMesSiguiente As Date = inicioMesActual.AddMonths(1)
                                            Dim inicioMesAnterior As Date = inicioMesActual.AddMonths(-1)

                                            ' Query base por unidad operativa
                                            Dim crBase = session.Query(Of CausationRecognitionXpo)() _
                                                            .Where(Function(cr) cr.OperativeUnitId = Me._idOperativeUnit).ToList()

                                            ' Reconocido mes actual y anterior (rangos con índice)
                                            Dim crMesActual = crBase.Where(Function(cr) cr.CreationDate >= inicioMesActual AndAlso cr.CreationDate < inicioMesSiguiente).ToList()
                                            Dim crMesAnterior = crBase.Where(Function(cr) cr.CreationDate >= inicioMesAnterior AndAlso cr.CreationDate < inicioMesActual).ToList()

                                            ' Totales (forzamos 0 si no hay filas para evitar doble ronda con Any())
                                            Dim totalActual As Decimal = crMesActual.Sum(Function(x) x.TotalSupplier)
                                            Dim totalAnterior As Decimal = crMesAnterior.Sum(Function(x) x.TotalSupplier)

                                            ' Growth rate
                                            Dim growthRate As Decimal = 0D
                                            If totalAnterior > 0D Then
                                                growthRate = ((totalActual - totalAnterior) / totalAnterior) * 100D
                                            ElseIf totalActual > 0D Then
                                                growthRate = 100D
                                            End If

                                            ' Items causados en el mes actual (detalles unidos a reconocidos del mes actual)
                                            Dim crdMesActual = crMesActual _
                                                                .SelectMany(Function(cr) cr.CausationRecognitionDetailXpo) _
                                                                .Select(Function(d) d.Id) _
                                                                .Distinct() _
                                                                .ToList()

                                            Dim countCausados As Integer = crdMesActual.Count()

                                            ' Pendientes del mes actual (si aplica, también filtra por unidad si existe el campo)
                                            Dim cpMesActual = session.Query(Of CausationPendingXpo)() _
                                                                .Where(Function(cp) cp.CreationDate >= inicioMesActual AndAlso cp.CreationDate < inicioMesSiguiente)
                                            Dim countPendientes As Integer = cpMesActual.Count()

                                            Dim totalItems As Integer = countCausados + countPendientes

                                            ' Porcentajes
                                            Dim pctReconocido As Decimal = 0D
                                            Dim pctPendiente As Decimal = 0D
                                            If totalItems > 0 Then
                                                pctReconocido = (CDec(countCausados) * 100D) / CDec(totalItems)
                                                pctPendiente = (CDec(countPendientes) * 100D) / CDec(totalItems)
                                            End If

                                            Return New KpiResult With {
                                            .ValorReconocido = totalActual,
                                            .PorcentajeReconocido = pctReconocido,
                                            .PorcentajeFallidas = pctPendiente,
                                            .GrowthRate = growthRate,
                                            .OrdenesSinCausar = countPendientes
                                        }
                                        End Using
                                    End Function, ct).ConfigureAwait(True) ' volvemos al contexto de sincronización de WinForms
        Catch ex As OperationCanceledException
            ' Cancelado: no tocar UI
            Return
        Catch
            ' Ante error, limpiamos en UI
            If _ctrKPIs IsNot Nothing Then
                If _ctrKPIs.IsHandleCreated AndAlso _ctrKPIs.InvokeRequired Then
                    _ctrKPIs.BeginInvoke(Sub() _ctrKPIs.LimpiarKPIs())
                Else
                    _ctrKPIs.LimpiarKPIs()
                End If
            End If
            Return
        End Try

        ' --- Actualizar UI (si el control sigue vivo) ---
        If _ctrKPIs Is Nothing Then Return
        If _ctrKPIs.IsHandleCreated AndAlso _ctrKPIs.InvokeRequired Then
            _ctrKPIs.BeginInvoke(Sub() _ctrKPIs.ActualizarKPIs(
                                 valorReconocido:=result.ValorReconocido,
                                 porcentajeReconocido:=result.PorcentajeReconocido,
                                 porcentajeFallidas:=result.PorcentajeFallidas,
                                 growthRate:=result.GrowthRate,
                                 ordenesSinCausar:=result.OrdenesSinCausar))
        Else
            _ctrKPIs.ActualizarKPIs(
            valorReconocido:=result.ValorReconocido,
            porcentajeReconocido:=result.PorcentajeReconocido,
            porcentajeFallidas:=result.PorcentajeFallidas,
            growthRate:=result.GrowthRate,
            ordenesSinCausar:=result.OrdenesSinCausar)
        End If
    End Function

#End Region

#Region "ICausation"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

        Me.BarraBotones.BarBtnConfirmar.Caption = "Liquidar"
        Me.BarraBotones.BarBtnDesconfirmar.Caption = "Reversar"

        Me.BarraBotones.BarbtnRefreshGrid.Caption = "Reconocidos"
        Me.BarraBotones.BarBtnRejillaEliminar.Caption = "No Reconocidos"
        Me.BarraBotones.BarBtnRejillaModificar.Caption = "Pendiente por Causar"

        Me._presenter.CleanControls()

        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        ' Mostrar siempre los botones de navegación entre vistas
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = False
        Me.BarraBotones.BarBtnRejillaModificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        ' Mostrar botón Liquidar si tiene permisos
        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "BarButtonEvents"
    ''' <summary>
    ''' Barras the botones load.
    ''' </summary>
    Private Sub BarraBotones_Load() Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' Barras the botones click Liquidar.
    ''' Agrupa por SupplierId y procesa cada proveedor independientemente
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If INDGcCausation.DataSource Is Nothing Then
            Exit Sub
        End If

        ' Obtener los proveedores únicos de las causaciones actuales
        Dim suppliers = GetUniqueSuppliersFromGrid()

        If suppliers.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron causaciones pendientes por reconocer"
            Exit Sub
        End If

        If MessageIndigo.Show($"¿Está seguro que desea reconocer las causaciones de {suppliers.Count} proveedor(es)?",
                              MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.AsyncLoaderOnlyBar(True)

            Dim msgError As New StringBuilder()
            Dim msgSuccess As New StringBuilder()
            Dim totalProcessed As Integer = 0
            Dim totalErrors As Integer = 0

            ' ForEach por proveedor (similar a FrmRecognition que hace ForEach por CareGroup)
            For Each supplier In suppliers
                ' Llamar al servicio por cada proveedor
                ' El SP consultará internamente la vista filtrada por este SupplierId
                Dim result As ActionResult = Await Me._presenter.GenerateRecognitionCausationsBySupplier(
                    supplier.SupplierId,      ' SupplierId específico
                    Me._idOperativeUnit,
                    Me._ctrRangeDate.InitialDate)

                If result.StateResult Then
                    msgSuccess.AppendLine(result.Message)
                    msgSuccess.AppendLine()
                    totalProcessed += 1
                Else
                    msgError.AppendLine($"{supplier.SupplierName}: {result.Message}")
                    msgSuccess.AppendLine()
                    totalErrors += 1
                End If
            Next

            Me.AsyncLoader(False)

            ' Mostrar resultados
            Dim finalMessage As New StringBuilder()
            If totalProcessed > 0 Then
                finalMessage.AppendLine($"{totalProcessed} proveedor(es) procesado(s) exitosamente:")
                finalMessage.AppendLine(msgSuccess.ToString())
            End If

            If totalErrors > 0 Then
                finalMessage.AppendLine()
                finalMessage.AppendLine($"{totalErrors} proveedor(es) con error:")
                finalMessage.AppendLine(msgError.ToString())
            End If

            If finalMessage.Length > 0 Then
                If totalErrors > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = finalMessage.ToString()
                Else
                    Mensaje(EeventViewerImages.Informacion) = finalMessage.ToString()
                End If
            End If

            Await ProcesarCausar()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los reconocimientos únicos para revertir (agrupados por CausationRecognitionId)
    ''' Cada CausationRecognitionId representa UN reconocimiento de UN proveedor con su propio comprobante contable
    ''' La relación es: 1 CausationRecognitionId = 1 SupplierId = 1 Comprobante Contable
    ''' </summary>
    Private Function GetRecognitionsForReverse() As List(Of RecognitionInfo)
        Dim recognitions As New List(Of RecognitionInfo)()

        Try
            ' Crear sesión temporal para hacer la consulta de agrupación
            Dim session As New Session(XpoDefault.DataLayer)

            ' IMPORTANTE: Agrupamos solo por CausationRecognitionId porque:
            ' - Cada reconocimiento YA está asociado a un único proveedor
            ' - Cada reconocimiento tiene su propio comprobante contable
            ' - Al reversar, generamos un comprobante de reversión por cada reconocimiento
            Dim query = From causation In session.Query(Of ViewListCausationwithRecognition)()
                        Where causation.OperatingUnitId = Me._idOperativeUnit
                        Group causation By causation.CausationRecognitionId, causation.SupplierId, causation.SupplierName, causation.RecognitionDate, causation.JournalVoucherConsecutive Into Group
                        Select New RecognitionInfo With {
                            .CausationRecognitionId = CausationRecognitionId,
                            .SupplierId = SupplierId,
                            .SupplierName = SupplierName,
                            .RecognitionDate = RecognitionDate,
                            .JournalVoucherConsecutive = JournalVoucherConsecutive,
                            .TotalValue = Group.Sum(Function(x) x.TotalAmountPayable),
                            .ServiceCount = Group.Count()
                        }

            recognitions = query.ToList()
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al obtener reconocimientos para reversión: " & ex.Message
        End Try

        Return recognitions
    End Function

    ''' <summary>
    ''' Barras the botones click Reversar.
    ''' Agrupa por CausationRecognitionId y procesa cada reconocimiento independientemente
    ''' </summary>
    Private Async Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        If INDGcCausation.DataSource Is Nothing Then
            Exit Sub
        End If

        ' Obtener los reconocimientos únicos para reversar (agrupados por CausationRecognitionId)
        Dim recognitions = GetRecognitionsForReverse()

        If recognitions.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron causaciones reconocidas para reversar"
            Exit Sub
        End If

        ' Agrupar por proveedor para mostrar mensaje más claro
        Dim supplierCount = recognitions.Select(Function(r) r.SupplierId).Distinct().Count()
        Dim totalValue = recognitions.Sum(Function(r) r.TotalValue)

        ' Mensaje detallado explicando que se reversarán comprobantes contables individuales
        Dim confirmMessage As New StringBuilder()
        confirmMessage.AppendLine($"¿Está seguro que desea reversar {recognitions.Count} reconocimiento(s)?")

        If MessageIndigo.Show(confirmMessage.ToString(),
                              MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.AsyncLoaderOnlyBar(True)

            Dim msgError As New StringBuilder()
            Dim msgSuccess As New StringBuilder()
            Dim totalProcessed As Integer = 0
            Dim totalErrors As Integer = 0

            For Each recognition In recognitions
                ' Llamar al servicio por cada reconocimiento
                ' El SP reversará internamente:
                ' 1. Todas las causaciones de este reconocimiento (limpia CausationRecognitionId)
                ' 2. Genera un comprobante contable de reversión
                ' 3. Actualiza el estado del reconocimiento a "Reversado"
                Dim result As ActionResult = Await Me._presenter.ReversarCausacionRecognition(
                    recognition.CausationRecognitionId)

                If result.StateResult Then
                    msgSuccess.AppendLine($"    {recognition.SupplierName}")
                    msgSuccess.AppendLine($"  - Comprobante Original: {recognition.JournalVoucherConsecutive}")
                    msgSuccess.AppendLine($"  - Valor: ${recognition.TotalValue:N2} ({recognition.ServiceCount} servicios)")
                    msgSuccess.AppendLine($"  - {result.Message}")
                    msgSuccess.AppendLine()
                    totalProcessed += 1
                Else
                    msgError.AppendLine($"  {recognition.SupplierName} (Reconocimiento #{recognition.CausationRecognitionId})")
                    msgError.AppendLine($"  - Comprobante: {recognition.JournalVoucherConsecutive}")
                    msgError.AppendLine($"  - Error: {result.Message}")
                    msgError.AppendLine()
                    totalErrors += 1
                End If
            Next

            Me.AsyncLoader(False)

            ' Mostrar resultados
            Dim finalMessage As New StringBuilder()
            If totalProcessed > 0 Then
                finalMessage.AppendLine($"{totalProcessed} reconocimiento(s) reversado(s) exitosamente:")
                finalMessage.AppendLine(msgSuccess.ToString())
            End If

            If totalErrors > 0 Then
                finalMessage.AppendLine()
                finalMessage.AppendLine($"{totalErrors} reconocimiento(s) con error:")
                finalMessage.AppendLine(msgError.ToString())
            End If

            If finalMessage.Length > 0 Then
                If totalErrors > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = finalMessage.ToString()
                Else
                    Mensaje(EeventViewerImages.Informacion) = finalMessage.ToString()
                End If
            End If

            Await ProcesarReversar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones click refresh grid - Vista Reconocidos.
    ''' </summary>
    Private Async Sub BarraBotones_Click_RefreshGrid() Handles BarraBotones.Click_RefreshGrid
        _IsProcesar = False
        _currentView = ECausationView.Reconocidos

        ' Actualizar visibilidad de columnas
        UpdateColumnsVisibility()

        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        End If

        Await Me.ProcesarReversar()
    End Sub

    ''' <summary>
    ''' Barras the botones click eliminar rejilla - Vista No Reconocidos.
    ''' </summary>
    Private Async Sub BarraBotones_ClickEliminarRejilla() Handles BarraBotones.ClickEliminarRejilla
        _IsProcesar = True
        _currentView = ECausationView.NoReconocidos

        ' Actualizar visibilidad de columnas
        UpdateColumnsVisibility()

        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        End If

        Await Me.ProcesarCausar()
    End Sub

    ''' <summary>
    ''' Barras the botones click integrar - Vista Pendiente por Causar.
    ''' Esta vista muestra causaciones con errores y NO requiere acciones de Liquidar o Reversar
    ''' </summary>
    Private Sub BarraBotones_ClickModificarRejilla() Handles BarraBotones.ClickModificarRejilla
        _IsProcesar = True
        _currentView = ECausationView.PendientePorCausar

        ' Actualizar visibilidad de columnas (muestra columna "Errores")
        UpdateColumnsVisibility()

        ' En la vista "Pendiente por Causar" NO se muestran los botones de Liquidar ni Reversar
        ' Esta es una vista de consulta/información de causaciones con errores
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True

        ' Cargar la vista de Pendiente por Causar
        Me.ProcesarPendientePorCausar()
    End Sub

    ''' <summary>
    ''' Barras the botones click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id > 0 Then
            Me._idOperativeUnit = operatingUnit.Id

            ' Recargar la vista actual según el estado
            Select Case _currentView
                Case ECausationView.NoReconocidos
                    Await Me.ProcesarCausar()
                Case ECausationView.Reconocidos
                    Await Me.ProcesarReversar()
                Case ECausationView.PendientePorCausar
                    Me.ProcesarPendientePorCausar()
            End Select
        End If
    End Sub

#End Region

End Class

''' <summary>
''' Clase auxiliar para almacenar información de proveedores únicos
''' </summary>
Public Class SupplierInfo
    Public Property SupplierId As Integer
    Public Property SupplierName As String
    Public Property TotalValue As Decimal
    Public Property ServiceCount As Integer
End Class

''' <summary>
''' Clase auxiliar para almacenar información de reconocimientos únicos (para reversión)
''' Representa un reconocimiento completo que agrupa causaciones de UN solo proveedor
''' Cada reconocimiento tiene su propio comprobante contable que debe reversarse individualmente
''' </summary>
Public Class RecognitionInfo
    ''' <summary>ID único del reconocimiento (PK de CausationRecognition)</summary>
    Public Property CausationRecognitionId As Integer

    ''' <summary>ID del proveedor (médico o agremiación)</summary>
    Public Property SupplierId As Integer

    ''' <summary>Nombre del proveedor</summary>
    Public Property SupplierName As String

    ''' <summary>Fecha del reconocimiento original</summary>
    Public Property RecognitionDate As DateTime

    ''' <summary>Consecutivo del comprobante contable original</summary>
    Public Property JournalVoucherConsecutive As Long

    ''' <summary>Valor total del reconocimiento</summary>
    Public Property TotalValue As Decimal

    ''' <summary>Cantidad de servicios/causaciones en este reconocimiento</summary>
    Public Property ServiceCount As Integer
End Class

' DTO para transportar resultados del hilo de trabajo
Public Class KpiResult
    Public Property ValorReconocido As Decimal
    Public Property PorcentajeReconocido As Decimal
    Public Property PorcentajeFallidas As Decimal
    Public Property GrowthRate As Decimal
    Public Property OrdenesSinCausar As Integer
End Class

