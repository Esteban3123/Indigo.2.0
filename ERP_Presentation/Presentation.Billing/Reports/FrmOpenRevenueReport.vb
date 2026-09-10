#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmOpenRevenueReport

#Region "Datasource"

    Public Property ProoftCloseXpoDocuments As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource
    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Centro de Atención"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Entidad"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Grupo de Atención"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(4, "Usuarios"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(5, "Estado de Folio"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado x Ingresos"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado Folios"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(3, "Resumido"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

    Private _FillingStatus As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Facturado Parcial"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports() As Boolean

        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        If INDDeDateStart.EditValue IsNot Nothing AndAlso INDDeDateEnd.EditValue Is Nothing OrElse INDDeDateStart.EditValue Is Nothing AndAlso INDDeDateEnd.EditValue IsNot Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
            Me.INDDeDateStart.Focus()
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
    ''' </summary>
    Private _selectorAttentionCenter As SelectorCache = New SelectorCache("CODCENATE", "CodeName")

    Private _selectorEntity As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorCareGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorUsers As SelectorCache = New SelectorCache("UserCode", "CodeName")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvAttentionCenter.CustomUnboundColumnData, INDGvEntity.CustomUnboundColumnData, INDGvCareGroup.CustomUnboundColumnData, INDGvUsers.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvAttentionCenter" Then
                e.Value = _selectorAttentionCenter.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvEntity" Then
                e.Value = _selectorEntity.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvCareGroup" Then
                e.Value = _selectorCareGroup.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvUsers" Then
                e.Value = _selectorUsers.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvAttentionCenter.RowCellClick, INDGvEntity.RowCellClick, INDGvCareGroup.RowCellClick, INDGvUsers.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvAttentionCenter" Then
                selector = _selectorAttentionCenter

            ElseIf view.Name = "INDGvEntity" Then
                selector = _selectorEntity

            ElseIf view.Name = "INDGvCareGroup" Then
                selector = _selectorCareGroup

            ElseIf view.Name = "INDGvUsers" Then
                selector = _selectorUsers

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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleAttentionCenter.Closed, INDSleCareGroup.Closed, INDSleAttentionCenter.Closed, INDSleUsers.Closed, INDSleEntity.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleAttentionCenter" Then
            searchLookupEdit.Properties.NullText = _selectorAttentionCenter.ToString()

        ElseIf searchLookupEdit.Name = "INDSleEntity" Then
            searchLookupEdit.Properties.NullText = _selectorEntity.ToString()

        ElseIf searchLookupEdit.Name = "INDSleCareGroup" Then
            searchLookupEdit.Properties.NullText = _selectorCareGroup.ToString()

        ElseIf searchLookupEdit.Name = "INDSleUsers" Then
            searchLookupEdit.Properties.NullText = _selectorUsers.ToString()

        End If
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDatasource()
        Try
            Dim filter As String = Nothing

            'filtro por fechas
            If INDDeDateStart.EditValue IsNot Nothing AndAlso INDDeDateEnd.EditValue IsNot Nothing Then
                Dim fechaInicio As Date = CDate(INDDeDateStart.EditValue).Date
                Dim fechaFin As Date = CDate(INDDeDateEnd.EditValue).Date.AddDays(1).AddSeconds(-1)

                filter = "DateIncome >= #" & fechaInicio.ToString("yyyy-MM-dd HH:mm:ss") & "# AND DateIncome <= #" & fechaFin.ToString("yyyy-MM-dd HH:mm:ss") & "#"
            End If

            'si filtra por Estado
            If INDGleStatus.EditValue <> 4 Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("Status IN ({0})", INDGleStatus.EditValue)
            End If

            'si filtra por grupo de atencion
            If Not String.IsNullOrEmpty(_selectorCareGroup.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format(" CareGroupId IN ({0})", _selectorCareGroup.GetKeys())
            End If

            'si filtra por Centro de atencion
            If Not String.IsNullOrEmpty(_selectorAttentionCenter.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("CodCareCenter IN ({0})", _selectorAttentionCenter.GetKeys())
            End If

            'si filtra por Entidad
            If Not String.IsNullOrEmpty(_selectorEntity.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("EntityId In ({0})", _selectorEntity.GetKeys())
            End If

            'si filtra por Usuarios
            If Not String.IsNullOrEmpty(_selectorUsers.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("CodCreationUser In ({0})", _selectorUsers.GetKeys())
            End If

            'si el ingreso es detallado por Ingreso
            If INDGleTypeReport.EditValue IsNot Nothing AndAlso INDGleTypeReport.EditValue = 1 Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("FolioOrder In ({0})", INDGleTypeReport.EditValue)
            End If

            Dim listOpenRevenueReport = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.GetCollection(Of ViewOpenRevenueReportXpo)(Nothing, filter)
            If listOpenRevenueReport IsNot Nothing AndAlso listOpenRevenueReport.Count > 0 Then
                Dim dt As New DataTable
                dt.Columns.Add("Ingreso")
                'Detallado por Folio
                If INDGleTypeReport.EditValue <> 1 Then
                    dt.Columns.Add("Número de Folio")
                    dt.Columns.Add("Tipo de Folio")
                    dt.Columns.Add("Estado Folio")
                    dt.Columns.Add("Responsable Couta Recuperación")
                    dt.Columns.Add("Observaciones X Folio")
                    dt.Columns.Add("Total Folio")
                    dt.Columns.Add("Concepto Estado Folio")
                End If
                dt.Columns.Add("Cod Grupo de Atención")
                dt.Columns.Add("Grupo de Atención")
                dt.Columns.Add("Entidad")
                dt.Columns.Add("Cod Centro Atención")
                dt.Columns.Add("Centro Atención")
                dt.Columns.Add("Identificación")
                dt.Columns.Add("Lugar Expedición")
                dt.Columns.Add("Paciente")
                dt.Columns.Add("Edad")
                dt.Columns.Add("Ciclo Vida Grupo Etareo")
                dt.Columns.Add("Grupo Etareo_RES5268")
                dt.Columns.Add("Grupo Etareo_UPC")
                dt.Columns.Add("Fecha de Ingreso", GetType(DateTime))
                dt.Columns.Add("Estado Ingreso")
                dt.Columns.Add("Unidad Funcional")
                dt.Columns.Add("Fecha de Alta Medica", GetType(DateTime))
                dt.Columns.Add("Ingreso CIE10")
                dt.Columns.Add("Diagnostico de Ingreso")
                dt.Columns.Add("Egreso CIE10")
                dt.Columns.Add("Diagnostico de Salida")
                dt.Columns.Add("Cod Usuario Creación")
                dt.Columns.Add("Usuario Creación")
                dt.Columns.Add("Fecha de Creación", GetType(DateTime))
                dt.Columns.Add("Cod Usuario Modificación")
                dt.Columns.Add("Usuario Modificación")
                dt.Columns.Add("Fecha de Modificación", GetType(DateTime))
                dt.Columns.Add("Unidad Actual")
                dt.Columns.Add("Observación Ingreso")
                dt.Columns.Add("Tipo de ingreso")
                dt.Columns.Add("Enfermedad Actual")
                dt.Columns.Add("Ubicación")
                dt.Columns.Add("Municipio")
                dt.Columns.Add("Tel Fijo")
                dt.Columns.Add("Tel Movil")
                dt.Columns.Add("Causa Ingreso")
                dt.Columns.Add("Tipo de Riesgo")
                dt.Columns.Add("Mes de Ingreso")
                dt.Columns.Add("Número Mes")
                dt.Columns.Add("Alta Medica Ingreso")
                dt.Columns.Add("Dias de Alta medica")
                dt.Columns.Add("Estado Cargues")

                For Each itemView In listOpenRevenueReport
                    If itemView IsNot Nothing AndAlso listOpenRevenueReport.Count > 0 Then
                        Dim row As DataRow = dt.NewRow()
                        row.Item("Ingreso") = itemView.Income
                        If INDGleTypeReport.EditValue <> 1 Then
                            row.Item("Número de Folio") = itemView.FolioOrder
                            row.Item("Tipo de Folio") = itemView.FolioType
                            row.Item("Estado Folio") = itemView.FolioStatusName
                            row.Item("Responsable Couta Recuperación") = itemView.ResponsibleRecoveryFeeName
                            row.Item("Observaciones X Folio") = itemView.FolioObservations
                            row.Item("Total Folio") = itemView.TotalFolio
                            row.Item("Concepto Estado Folio") = itemView.ConceptStatusFolio
                        End If
                        row.Item("Cod Grupo de Atención") = itemView.CodCareGroup
                        row.Item("Grupo de Atención") = itemView.CareGroup
                        row.Item("Entidad") = itemView.Entity
                        row.Item("Cod Centro Atención") = itemView.CodCareCenter
                        row.Item("Centro Atención") = itemView.CareCenter
                        row.Item("Identificación") = itemView.Identification
                        row.Item("Lugar Expedición") = itemView.PlaceExpedition
                        row.Item("Paciente") = itemView.Patient
                        row.Item("Edad") = itemView.Age
                        row.Item("Ciclo Vida Grupo Etareo") = itemView.LifeCycleEtareoGroup
                        row.Item("Grupo Etareo_RES5268") = itemView.GroupEtareo_RES5268
                        row.Item("Grupo Etareo_UPC") = itemView.GroupEtareoUPC
                        row.Item("Fecha de Ingreso") = itemView.DateIncome
                        row.Item("Estado Ingreso") = itemView.StatusName
                        row.Item("Unidad Funcional") = itemView.FunctionalUnit
                        row.Item("Fecha de Alta Medica") = itemView.MedicalDischargeDate
                        row.Item("Ingreso CIE10") = itemView.CIE10Income
                        row.Item("Diagnostico de Ingreso") = itemView.AdmissionDiagnosis
                        row.Item("Egreso CIE10") = itemView.CIE10Egress
                        row.Item("Diagnostico de Salida") = itemView.EgressDiagnosis
                        row.Item("Cod Usuario Creación") = itemView.CodCreationUser
                        row.Item("Usuario Creación") = itemView.CreationUser
                        row.Item("Fecha de Creación") = itemView.CreationDate
                        row.Item("Cod Usuario Modificación") = itemView.CodMedificationUser
                        row.Item("Usuario Modificación") = itemView.ModificationUser
                        row.Item("Fecha de Modificación") = itemView.ModificationDate
                        row.Item("Unidad Actual") = itemView.CurrentUnit
                        row.Item("Observación Ingreso") = itemView.Observation
                        row.Item("Tipo de ingreso") = itemView.TypeIncome
                        row.Item("Enfermedad Actual") = itemView.CurrentIllness
                        row.Item("Ubicación") = itemView.Location
                        row.Item("Municipio") = itemView.Municipality
                        row.Item("Tel Fijo") = itemView.Landline
                        row.Item("Tel Movil") = itemView.MobilePhone
                        row.Item("Causa Ingreso") = itemView.CauseIncome
                        row.Item("Tipo de Riesgo") = itemView.RiskType
                        row.Item("Mes de Ingreso") = itemView.MonthAdmissionDate
                        row.Item("Número Mes") = itemView.MonthYearEntryDate
                        row.Item("Alta Medica Ingreso") = itemView.IncomeMedicalDischarge
                        row.Item("Dias de Alta medica") = itemView.DaysMedicalDischarge
                        row.Item("Estado Cargues") = itemView.StateLoads
                        dt.Rows.Add(row)
                    End If
                Next
                INDGcExportExcell.DataSource = dt
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        If _gridView IsNot Nothing Then
            _gridView.MainView.PopulateColumns()
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmOpenRevenueReport_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleStatus.EditValue = 4
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmOpenRevenueReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingGroupBy = Nothing
        _LoadTypeReport = Nothing
        _FillingStatus = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Properties.DataSource Is Nothing Then
            INDSleCareGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEntity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEntity.QueryPopUp
        If INDSleEntity.Properties.DataSource Is Nothing Then

            INDSleEntity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListHealthAdministrator()

        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAttentionCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAttentionCenter.QueryPopUp
        If INDSleAttentionCenter.Properties.DataSource Is Nothing Then
            INDSleAttentionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.HisContainer).CrystalService.GetAllCareCenter()
        End If
    End Sub

    Private Sub INDSleUsers_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUsers.QueryPopUp
        If INDSleUsers.Properties.DataSource Is Nothing Then
            INDSleUsers.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListUserByContainer(indigo.IndigoContainerId)
        End If
    End Sub

#End Region

#Region "EditValueChange"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 3 Then
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleGroupBy.EditValue = 1
        Else
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleGroupBy.EditValue = Nothing
        End If

    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As Object
            If INDGleTypeReport.EditValue = 3 Then
                reporte = New rptOpenRevenueReportSummary
            Else
                reporte = New rptOpenRevenueReport
            End If
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                          INDDeDateEnd.EditValue,
                                                          INDGleStatus.EditValue,
                                                          _selectorCareGroup.GetKeys(),
                                                          _selectorAttentionCenter.GetKeys(),
                                                          _selectorEntity.GetKeys(),
                                                          _selectorUsers.GetKeys(),
                                                          INDGleTypeReport.EditValue,
                                                          INDGleGroupBy.EditValue}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await Task.Factory.StartNew(AddressOf chargueDatasource)

                If Me.INDGcExportExcell.DataSource IsNot Nothing Then
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