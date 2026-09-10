'***********************************************************************
' Assembly         : Presentation.AccountManagement
' Author           : Anthony Ocampo
' Created          : 10-06-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : Reporte de Gestión de Cuentas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Reporter
Imports Presentation.AccountManagement.MVP
Imports Infrastructure.Data.Xpo.AccountManagementRespository

#End Region

Public Class FrmReportAccountManagement

#Region "Datasource"

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingFolioStatus As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property FillingFolioStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingFolioStatus Is Nothing Then
                _FillingFolioStatus = New List(Of Tuple(Of Integer, String))
                _FillingFolioStatus.Add(New Tuple(Of Integer, String)(1, "Pendiente por facturar"))
                _FillingFolioStatus.Add(New Tuple(Of Integer, String)(2, "Facturado"))
            End If
            Return _FillingFolioStatus
        End Get
    End Property

#End Region

#Region "BarraBotones"

    ''' <summary>
    ''' Inicializa y actualiza los permisos de la barra de botones según el perfil del usuario
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Muestra mensajes al usuario según el tipo de icono (Advertencia, Información, Error)
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Valida que las fechas y el tipo de reporte sean obligatorios y cumplan reglas de negocio
    ''' </summary>
    Private Function ValidateControlsReports() As Boolean

        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        ' Validar que la fecha inicial sea obligatoria
        If INDDeDateInitial.EditValue Is Nothing Then
            errors.AppendLine("La Fecha Inicial es obligatoria.")
            Me.INDDeDateInitial.Focus()
            Validations = False
        End If

        ' Validar que la fecha final sea obligatoria
        If INDDeDateFinal.EditValue Is Nothing Then
            errors.AppendLine("La Fecha Final es obligatoria.")
            If Validations Then Me.INDDeDateFinal.Focus()
            Validations = False
        End If

        ' Validar que la fecha inicial no sea mayor que la fecha final
        If INDDeDateInitial.EditValue IsNot Nothing AndAlso INDDeDateFinal.EditValue IsNot Nothing Then
            If Me.INDDeDateInitial.EditValue > INDDeDateFinal.EditValue Then
                errors.AppendLine("La Fecha Inicial no puede ser mayor que la Fecha Final.")
                Me.INDDeDateInitial.Focus()
                Validations = False
            End If
        End If

        ' Validar que el tipo de reporte sea obligatorio
        If INDGleTypeReport.EditValue Is Nothing Then
            errors.AppendLine("El Tipo de Reporte es obligatorio.")
            If Validations Then Me.INDGleTypeReport.Focus()
            Validations = False
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

#Region "Selector"

    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' NOTA: Tercero usa "Nit" como key para filtrar por PatientCode en la vista
    ''' NOTA: Usuario usa "UserCode" como key para filtrar por CurrentOwnerCode en la vista
    ''' </summary>
    Private _selectorThird As SelectorCache = New SelectorCache("Nit", "FullName")
    Private _selectorEntity As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorCareGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorUsers As SelectorCache = New SelectorCache("UserCode", "PersonFullName")
    Private _selectorManagementArea As SelectorCache = New SelectorCache("Id", "Code")

    ''' <summary>
    ''' Maneja el pintado de checkboxes en las columnas de selección múltiple
    ''' </summary>
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvThirdparty.CustomUnboundColumnData, INDGvEntity.CustomUnboundColumnData, INDGvCareGroup.CustomUnboundColumnData, INDGvUsers.CustomUnboundColumnData, INDGvManagementArea.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvThirdparty" Then
                e.Value = _selectorThird.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvEntity" Then
                e.Value = _selectorEntity.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvCareGroup" Then
                e.Value = _selectorCareGroup.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvUsers" Then
                e.Value = _selectorUsers.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvManagementArea" Then
                e.Value = _selectorManagementArea.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Maneja el click en checkboxes para agregar/quitar elementos de la selección múltiple
    ''' </summary>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvThirdparty.RowCellClick, INDGvEntity.RowCellClick, INDGvCareGroup.RowCellClick, INDGvUsers.RowCellClick, INDGvManagementArea.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvThirdparty" Then
                selector = _selectorThird

            ElseIf view.Name = "INDGvEntity" Then
                selector = _selectorEntity

            ElseIf view.Name = "INDGvCareGroup" Then
                selector = _selectorCareGroup

            ElseIf view.Name = "INDGvUsers" Then
                selector = _selectorUsers

            ElseIf view.Name = "INDGvManagementArea" Then
                selector = _selectorManagementArea

            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    ''' Actualiza el texto de los controles SearchLookUpEdit con los códigos seleccionados al cerrar el popup
    ''' </summary>
    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleThirdparty.Closed, INDSleEntity.Closed, INDSleCareGroup.Closed, INDSleAssignmentUser.Closed, INDSleManagementAreas.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleThirdparty" Then
            searchLookupEdit.Properties.NullText = _selectorThird.ToString()

        ElseIf searchLookupEdit.Name = "INDSleEntity" Then
            searchLookupEdit.Properties.NullText = _selectorEntity.ToString()

        ElseIf searchLookupEdit.Name = "INDSleCareGroup" Then
            searchLookupEdit.Properties.NullText = _selectorCareGroup.ToString()

        ElseIf searchLookupEdit.Name = "INDSleAssignmentUser" Then
            searchLookupEdit.Properties.NullText = _selectorUsers.ToString()

        ElseIf searchLookupEdit.Name = "INDSleManagementAreas" Then
            searchLookupEdit.Properties.NullText = _selectorManagementArea.ToString()

        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' Carga los datos aplicando filtros para la exportación a Excel
    ''' </summary>
    Private Sub loadDatasource()
        Try
            Dim filter As String = Nothing

            'Filtro por fechas (obligatorio) - usando AdmissionDate de la vista
            If INDDeDateInitial.EditValue IsNot Nothing AndAlso INDDeDateFinal.EditValue IsNot Nothing Then
                Dim fechaInicio As Date = CDate(INDDeDateInitial.EditValue).Date
                Dim fechaFin As Date = CDate(INDDeDateFinal.EditValue).Date.AddDays(1).AddSeconds(-1)

                filter = "AdmissionDate >= #" & fechaInicio.ToString("yyyy-MM-dd HH:mm:ss") & "# AND AdmissionDate <= #" & fechaFin.ToString("yyyy-MM-dd HH:mm:ss") & "#"
            End If

            'Si filtra por Estado Folio (opcional)
            If INDGleFolioStatus.EditValue IsNot Nothing Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("FolioStatus = {0}", INDGleFolioStatus.EditValue)
            End If

            'Si filtra por Centro de Atención (opcional)
            If INDSleAttentionCenter.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(INDSleAttentionCenter.EditValue.ToString()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("AttentionCenterCode = '{0}'", INDSleAttentionCenter.EditValue)
            End If

            'Si filtra por Tercero (Paciente) - usa NITs/Códigos de paciente
            If Not String.IsNullOrEmpty(_selectorThird.GetKeys()) Then
                Dim nits = _selectorThird.GetKeys().Split(","c)
                Dim nitsQuoted = String.Join(",", nits.Select(Function(n) "'" & n.Trim() & "'"))
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("PatientCode IN ({0})", nitsQuoted)
            End If

            'Si filtra por Entidad (opcional, múltiple)
            If Not String.IsNullOrEmpty(_selectorEntity.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("HealthAdministrationId IN ({0})", _selectorEntity.GetKeys())
            End If

            'Si filtra por Grupo de Atención (opcional, múltiple)
            If Not String.IsNullOrEmpty(_selectorCareGroup.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("CareGroupId IN ({0})", _selectorCareGroup.GetKeys())
            End If

            'Si filtra por Usuario Asignado (opcional, múltiple) - usa UserCode
            If Not String.IsNullOrEmpty(_selectorUsers.GetKeys()) Then
                Dim userCodes = _selectorUsers.GetKeys().Split(","c)
                Dim userCodesQuoted = String.Join(",", userCodes.Select(Function(u) "'" & u.Trim() & "'"))
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("CurrentOwnerCode IN ({0})", userCodesQuoted)
            End If

            'Si filtra por Área de Gestión (opcional, múltiple)
            If Not String.IsNullOrEmpty(_selectorManagementArea.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("ManagementAreaId IN ({0})", _selectorManagementArea.GetKeys())
            End If

            'Cargar datos desde la vista
            Dim listAccountManagementReport = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountManagementService.GetCollection(Of ViewReportAccountManagementXpo)(Nothing, filter)

            'Asignar los datos al GridControl para exportar
            INDGcExportExcel.DataSource = listAccountManagementReport

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Genera archivo Excel con columnas personalizadas según el tipo de reporte seleccionado (Resumido o Detallado)
    ''' </summary>
    Private Sub generateExcel()
        Dim _gridControl = Me.INDGcExportExcel
        If _gridControl IsNot Nothing Then
            Dim gridView As GridView = TryCast(_gridControl.MainView, GridView)
            If gridView IsNot Nothing Then
                ' Limpiar columnas existentes
                gridView.Columns.Clear()

                ' Configurar columnas según tipo de reporte
                If INDGleTypeReport.EditValue = 1 Then
                    ' REPORTE RESUMIDO - 5 columnas
                    ConfigurarColumnasResumido(gridView)
                Else
                    ' REPORTE DETALLADO - 11 columnas
                    ConfigurarColumnasDetallado(gridView)
                End If

                ' Configurar vista
                gridView.BestFitColumns()
                gridView.OptionsView.ColumnAutoWidth = False

                ' Exportar a Excel
                Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
                Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
                _gridControl.ExportToXlsx(fileName, param)

                ' Abrir archivo
                If System.IO.File.Exists(fileName) Then
                    System.Diagnostics.Process.Start(fileName)
                End If
            End If
        End If

        ' Limpiar DataSource
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Configura las columnas del GridView para el reporte RESUMIDO (5 columnas)
    ''' </summary>
    Private Sub ConfigurarColumnasResumido(gridView As GridView)
        ' 1. Área de Gestión
        Dim colArea = gridView.Columns.AddField("ManagementAreaName")
        colArea.VisibleIndex = 0
        colArea.Caption = "Área de Gestión"
        colArea.Width = 150

        ' 2. Estado del Folio
        Dim colEstado = gridView.Columns.AddField("FolioStatusDescription")
        colEstado.VisibleIndex = 1
        colEstado.Caption = "Estado del Folio"
        colEstado.Width = 120

        ' 3. Grupo de Atención
        Dim colGrupo = gridView.Columns.AddField("CareGroupName")
        colGrupo.VisibleIndex = 2
        colGrupo.Caption = "Grupo de Atención"
        colGrupo.Width = 150

        ' 4. Nombre Entidad
        Dim colEntidad = gridView.Columns.AddField("HealthAdministrationName")
        colEntidad.VisibleIndex = 3
        colEntidad.Caption = "Nombre Entidad"
        colEntidad.Width = 200

        ' 5. Valor Folio
        Dim colValor = gridView.Columns.AddField("FolioTotalValue")
        colValor.VisibleIndex = 4
        colValor.Caption = "Valor Folio"
        colValor.Width = 120
        colValor.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colValor.DisplayFormat.FormatString = "c0"
    End Sub

    ''' <summary>
    ''' Configura las columnas del GridView para el reporte DETALLADO (11 columnas)
    ''' </summary>
    Private Sub ConfigurarColumnasDetallado(gridView As GridView)
        ' 1. Paciente
        Dim colPaciente = gridView.Columns.AddField("PatientCodeName")
        colPaciente.VisibleIndex = 0
        colPaciente.Caption = "Paciente"
        colPaciente.Width = 150

        ' 2. Ingreso
        Dim colIngreso = gridView.Columns.AddField("AdmissionNumber")
        colIngreso.VisibleIndex = 1
        colIngreso.Caption = "Ingreso"
        colIngreso.Width = 100

        ' 3. Fecha de Ingreso
        Dim colFecha = gridView.Columns.AddField("AdmissionDate")
        colFecha.VisibleIndex = 2
        colFecha.Caption = "Fecha de Ingreso"
        colFecha.Width = 120
        colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colFecha.DisplayFormat.FormatString = "dd/MM/yyyy"

        ' 4. Área de Gestión
        Dim colArea = gridView.Columns.AddField("ManagementAreaName")
        colArea.VisibleIndex = 3
        colArea.Caption = "Área de Gestión"
        colArea.Width = 150

        ' 5. Unidad Funcional
        Dim colUnidad = gridView.Columns.AddField("FunctionalUnitName")
        colUnidad.VisibleIndex = 4
        colUnidad.Caption = "Unidad Funcional"
        colUnidad.Width = 150

        ' 6. Grupo de Atención
        Dim colGrupo = gridView.Columns.AddField("CareGroupName")
        colGrupo.VisibleIndex = 5
        colGrupo.Caption = "Grupo de Atención"
        colGrupo.Width = 150

        ' 7. Nombre Entidad
        Dim colEntidad = gridView.Columns.AddField("HealthAdministrationName")
        colEntidad.VisibleIndex = 6
        colEntidad.Caption = "Nombre Entidad"
        colEntidad.Width = 200

        ' 8. Asignado
        Dim colAsignado = gridView.Columns.AddField("CurrentOwnerFullName")
        colAsignado.VisibleIndex = 7
        colAsignado.Caption = "Asignado"
        colAsignado.Width = 150

        ' 9. Estado del Folio
        Dim colEstado = gridView.Columns.AddField("FolioStatusDescription")
        colEstado.VisibleIndex = 8
        colEstado.Caption = "Estado del Folio"
        colEstado.Width = 120

        ' 10. Número de Factura
        Dim colFactura = gridView.Columns.AddField("InvoiceNumber")
        colFactura.VisibleIndex = 9
        colFactura.Caption = "Número de Factura"
        colFactura.Width = 120

        ' 11. Valor Folio
        Dim colValor = gridView.Columns.AddField("FolioTotalValue")
        colValor.VisibleIndex = 10
        colValor.Caption = "Valor Folio"
        colValor.Width = 120
        colValor.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colValor.DisplayFormat.FormatString = "c0"
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Inicializa los datasources de los controles y establece valores por defecto al mostrar el formulario
    ''' </summary>
    Private Sub FrmReportAccountManagement_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleFolioStatus.Properties.DataSource = FillingFolioStatus
        'Dar un valor por defecto al Tipo de Reporte (Resumido)
        Me.INDGleTypeReport.EditValue = 1
        'Estado de Folio y Centro de Atención son opcionales, no se establece valor por defecto
    End Sub

    ''' <summary>
    ''' Establece el foco inicial en el campo Fecha Inicial al cargar el formulario
    ''' </summary>
    Private Sub FrmReportAccountManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateInitial.Focus()
    End Sub

    ''' <summary>
    ''' Libera recursos y limpia referencias al cerrar el formulario
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
        _FillingFolioStatus = Nothing
        _selectorThird = Nothing
        _selectorEntity = Nothing
        _selectorCareGroup = Nothing
        _selectorUsers = Nothing
        _selectorManagementArea = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Carga el datasource del Centro de Atención
    ''' Campo opcional - no obligatorio
    ''' </summary>
    Private Sub INDSleAttentionCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAttentionCenter.QueryPopUp
        If INDSleAttentionCenter.Properties.DataSource Is Nothing Then
            INDSleAttentionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.HisContainer).CrystalService.GetAllCareCenter()
        End If
    End Sub

    ''' <summary>
    ''' Carga los grupos de atención la primera vez que se abre el popup (carga diferida)
    ''' </summary>
    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Properties.DataSource Is Nothing Then
            INDSleCareGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Carga las entidades (administradoras de salud) la primera vez que se abre el popup (carga diferida)
    ''' </summary>
    Private Sub INDSleEntity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEntity.QueryPopUp
        If INDSleEntity.Properties.DataSource Is Nothing Then
            INDSleEntity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListHealthAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' Carga los terceros (pacientes/clientes) la primera vez que se abre el popup (carga diferida)
    ''' </summary>
    Private Sub INDSleThird_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdparty.QueryPopUp
        If INDSleThirdparty.Properties.DataSource Is Nothing Then
            INDSleThirdparty.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListThirdPartyReport()
        End If
    End Sub

    ''' <summary>
    ''' Carga los usuarios administrativos activos la primera vez que se abre el popup (carga diferida)
    ''' </summary>
    Private Sub INDSleAssignmentUser_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAssignmentUser.QueryPopUp
        If INDSleAssignmentUser.Properties.DataSource Is Nothing Then
            INDSleAssignmentUser.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListActiveAdministrativeUsers()
        End If
    End Sub

    ''' <summary>
    ''' Carga las áreas de gestión la primera vez que se abre el popup (carga diferida)
    ''' </summary>
    Private Sub INDSleManagementArea_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleManagementAreas.QueryPopUp
        If INDSleManagementAreas.Properties.DataSource Is Nothing Then
            INDSleManagementAreas.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountManagementService.ListManagementAreas()
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' Genera el reporte (Resumido o Detallado) según el tipo seleccionado y lo muestra en el visor de documentos
    ''' </summary>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            'Determinar qué reporte crear según el tipo seleccionado
            Dim reporte As Object
            If INDGleTypeReport.EditValue = 1 Then
                'Reporte Resumido
                reporte = New rptAccountManagementReportSummary
            Else
                'Reporte Detallado
                reporte = New rptAccountManagementReportDetailed
            End If

            'Pasar parámetros al reporte
            reporte.ParametrosReporte = New Object() {
                INDDeDateInitial.EditValue,          ' 0 = Fecha Inicial
                INDDeDateFinal.EditValue,            ' 1 = Fecha Final
                INDGleFolioStatus.EditValue,         ' 2 = Estado Folio
                INDSleAttentionCenter.EditValue,     ' 3 = Centro de Atención
                _selectorThird.GetKeys(),            ' 4 = IDs Terceros
                _selectorEntity.GetKeys(),           ' 5 = IDs Entidades
                _selectorCareGroup.GetKeys(),        ' 6 = IDs Grupos de Atención
                _selectorUsers.GetKeys(),            ' 7 = IDs Usuarios
                _selectorManagementArea.GetKeys()    ' 8 = IDs Áreas de Gestión
            }

            'Asignar reporte al visor de documentos
            INDDvDocumentViewer.DocumentSource = reporte

            'Cargar datos del reporte de forma asíncrona
            Await reporte.CargarDataSourceAsync()

            'Generar el documento si hay datos
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If

            AsyncLoader(False)

            'Mostrar el reporte o mensaje de advertencia
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                'Ocultar panel de filtros y mostrar el visor de reporte
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                'Mostrar mensaje si no hay datos
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeDateInitial.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Oculta el visor de reporte y vuelve a mostrar el panel de filtros
    ''' </summary>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' Exporta los datos filtrados a Excel con formato según el tipo de reporte seleccionado
    ''' </summary>
    Private Async Sub INDSbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await Task.Factory.StartNew(AddressOf loadDatasource)

                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                    generateExcel()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

End Class

