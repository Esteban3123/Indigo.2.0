#Region "Imports"

Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportRequests

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Datasources"

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

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Centro de Atención"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Grupo de Atención"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(4, "Entidad Administradora"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(5, "Estado"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(6, "Usuario Asignación"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

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

    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDDteDateStart.EditValue Is Nothing Or INDDteDateEnd.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDteDateStart.Focus()
        ElseIf Me.INDDteDateStart.EditValue > INDDteDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting")))
            Me.INDDteDateStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("DateStart", INDDteDateStart.EditValue)
        criterias.Add("DateEnd", INDDteDateEnd.EditValue)
        criterias.Add("TypeReport", INDGleTypeReport.EditValue)
        criterias.Add("GroupBy", INDGleGroupBy.EditValue)

        criterias.Add("CareCenters", _selectorCareCenter.GetKeys())
        criterias.Add("FunctionalUnits", _selectorFunctionalUnit.GetKeys())
        criterias.Add("CareGroups", _selectorCareGroup.GetKeys())
        criterias.Add("HealthAdministrators", _selectorHealthAdministrator.GetKeys())
        criterias.Add("ThirdParties", _selectorThirdparty.GetKeys())
        criterias.Add("Status", INDCcbeStatus.EditValue)
        criterias.Add("Users", _selectorUser.GetKeys())

        Return True
    End Function

#Region "ToExcel"

    Private Sub chargueDataSourceSummary(ByVal dtReportRequests As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Tipo de Servicio")
        dt.Columns.Add("Cantidad", GetType(Integer))
        dt.Columns.Add("Unidad Funcional")
        dt.Columns.Add("Grupo de Atención")
        dt.Columns.Add("Entidad Responsable de pago")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Asignación")
        dt.Columns.Add("Tiempo", GetType(Integer))
        dt.Columns.Add("Unidad de Tiempo")
        dt.Columns.Add("Tiempo de Tramite", GetType(Integer))

        For Each item In dtReportRequests.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Tipo de Servicio") = item("BillingGroupCodeName")
            row.Item("Cantidad") = item("Quantity")
            row.Item("Unidad Funcional") = item("FunctionalUnitCodeName")
            row.Item("Grupo de Atención") = item("CareGroupCodeName")
            row.Item("Entidad Responsable de pago") = item("HealthAdministratorCodeName")
            row.Item("Estado") = item("StatusName")
            row.Item("Asignación") = item("AssignUser")
            row.Item("Tiempo") = item("RequestTime")
            row.Item("Unidad de Tiempo") = item("RequestUnitTime")
            row.Item("Tiempo de Tramite") = item("ElapsedTime")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    Private Sub chargueDataSourceDetailed(ByVal dtReportRequests As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Identificación")
        dt.Columns.Add("Paciente")
        dt.Columns.Add("Ingreso")
        dt.Columns.Add("Edad")
        dt.Columns.Add("Fecha Solicitud", GetType(DateTime))
        dt.Columns.Add("Tipo de Servicio")
        dt.Columns.Add("Código Servicio")
        dt.Columns.Add("Descripción Servicio")
        dt.Columns.Add("Descripción Relacionada")
        dt.Columns.Add("Cantidad", GetType(Integer))
        dt.Columns.Add("Unidad Funcional")
        dt.Columns.Add("Diagnostico")
        dt.Columns.Add("Grupo de Atención")
        dt.Columns.Add("Médico Solicitante")
        dt.Columns.Add("Entidad Responsable de Pago")
        dt.Columns.Add("Cubierto", GetType(Boolean))
        dt.Columns.Add("Contratado", GetType(Boolean))
        dt.Columns.Add("Cotizado", GetType(Boolean))
        dt.Columns.Add("Asignación")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Razón de Cancelación")
        dt.Columns.Add("Tiempo", GetType(Integer))
        dt.Columns.Add("Unidad de Tiempo")
        dt.Columns.Add("Tiempo de Tramite", GetType(Integer))

        For Each item In dtReportRequests.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Identificación") = item("PatientCode")
            row.Item("Paciente") = item("PatientName")
            row.Item("Ingreso") = item("AdmissionNumber")
            row.Item("Edad") = item("PatientAge")
            row.Item("Fecha Solicitud") = item("RequestDate")
            row.Item("Tipo de Servicio") = item("BillingGroupCodeName")
            row.Item("Código Servicio") = item("ServiceCode")
            row.Item("Descripción Servicio") = item("ServiceDescription")
            row.Item("Descripción Relacionada") = item("ContractDescriptionCodeName")
            row.Item("Cantidad") = item("Quantity")
            row.Item("Unidad Funcional") = item("FunctionalUnitCodeName")
            row.Item("Diagnostico") = item("MainDiagnostic")
            row.Item("Grupo de Atención") = item("CareGroupCodeName")
            row.Item("Médico Solicitante") = item("ProfessionalCode")
            row.Item("Entidad Responsable de Pago") = item("HealthAdministratorCodeName")
            row.Item("Cubierto") = item("IsCovered")
            row.Item("Contratado") = item("Contracted")
            row.Item("Cotizado") = item("Quoted")
            row.Item("Asignación") = item("AssignUser")
            row.Item("Estado") = item("StatusName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Razón de Cancelación") = item("CancellationReasonsObservations")
            row.Item("Tiempo") = item("RequestTime")
            row.Item("Unidad de Tiempo") = item("RequestUnitTime")
            row.Item("Tiempo de Tramite") = item("ElapsedTime")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
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
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportRequests_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleGroupBy.EditValue = 1
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
        _FillingGroupBy = Nothing

        _selectorThirdparty = Nothing
        _selectorHealthAdministrator = Nothing
        _selectorCareGroup = Nothing
        _selectorUser = Nothing
        _selectorCareCenter = Nothing
        _selectorFunctionalUnit = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleCareCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareCenter.QueryPopUp
        If INDSleCareCenter.Properties.DataSource Is Nothing Then
            INDSleCareCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.HisContainer).CrystalService.GetAllCareCenter()
        End If
    End Sub

    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If INDSleFunctionalUnit.Properties.DataSource Is Nothing Then
            INDSleFunctionalUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.GetFunctionalUnit()
        End If
    End Sub

    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Properties.DataSource Is Nothing Then
            INDSleCareGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListCareGroup()
        End If
    End Sub

    Private Sub INDSleHealthAdministrator_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleHealthAdministrator.QueryPopUp
        If INDSleHealthAdministrator.Properties.DataSource Is Nothing Then
            INDSleHealthAdministrator.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListHealthAdministrator()
        End If
    End Sub

    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.DataSource Is Nothing Then
            INDSleThirdParty.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListAllThirdParty()
        End If
    End Sub

    Private Sub INDSleUser_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If INDSleUser.Properties.DataSource Is Nothing Then
            INDSleUser.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AuthorizationService.ListViewListScheduleTemplateUsers()
        End If
    End Sub

#End Region

#Region "Selector"

    Private _selectorCareCenter As SelectorCache = New SelectorCache("CODCENATE", "CODCENATE")
    Private _selectorFunctionalUnit As SelectorCache = New SelectorCache("Codigo", "Codigo")
    Private _selectorCareGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorHealthAdministrator As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorThirdparty As SelectorCache = New SelectorCache("Nit", "Nit")
    Private _selectorUser As SelectorCache = New SelectorCache("UserCode", "UserCode")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCareCenter.CustomUnboundColumnData, INDGvFunctionalUnit.CustomUnboundColumnData, INDGvCareGroup.CustomUnboundColumnData, INDGvHealthAdministrator.CustomUnboundColumnData, INDGvThirdParty.CustomUnboundColumnData, INDGvUser.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCareCenter" Then
                e.Value = _selectorCareCenter.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvFunctionalUnit" Then
                e.Value = _selectorFunctionalUnit.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvCareGroup" Then
                e.Value = _selectorCareGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvHealthAdministrator" Then
                e.Value = _selectorHealthAdministrator.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvThirdParty" Then
                e.Value = _selectorThirdparty.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvUser" Then
                e.Value = _selectorUser.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCareCenter.RowCellClick, INDGvFunctionalUnit.RowCellClick, INDGvCareGroup.RowCellClick, INDGvHealthAdministrator.RowCellClick, INDGvThirdParty.RowCellClick, INDGvUser.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCareCenter" Then
                selector = _selectorCareCenter
            ElseIf view.Name = "INDGvFunctionalUnit" Then
                selector = _selectorFunctionalUnit
            ElseIf view.Name = "INDGvThirdParty" Then
                selector = _selectorThirdparty
            ElseIf view.Name = "INDGvCareGroup" Then
                selector = _selectorCareGroup
            ElseIf view.Name = "INDGvHealthAdministrator" Then
                selector = _selectorHealthAdministrator
            ElseIf view.Name = "INDGvUser" Then
                selector = _selectorUser
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCareCenter.Closed, INDSleFunctionalUnit.Closed, INDSleCareGroup.Closed, INDSleHealthAdministrator.Closed, INDSleThirdParty.Closed, INDSleUser.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCareCenter" Then
            searchLookupEdit.Properties.NullText = _selectorCareCenter.ToString()
        ElseIf searchLookupEdit.Name = "INDSleFunctionalUnit" Then
            searchLookupEdit.Properties.NullText = _selectorFunctionalUnit.ToString()
        ElseIf searchLookupEdit.Name = "INDSleCareGroup" Then
            searchLookupEdit.Properties.NullText = _selectorCareGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleHealthAdministrator" Then
            searchLookupEdit.Properties.NullText = _selectorHealthAdministrator.ToString()
        ElseIf searchLookupEdit.Name = "INDSleThirdParty" Then
            searchLookupEdit.Properties.NullText = _selectorThirdparty.ToString()
        ElseIf searchLookupEdit.Name = "INDSleUser" Then
            searchLookupEdit.Properties.NullText = _selectorUser.ToString()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue IsNot Nothing And INDGleTypeReport.EditValue = 2 Then
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)

                Dim reporte As Object = Nothing
                If INDGleTypeReport.EditValue = 1 Then
                    reporte = New rptReportRequestSummary()
                ElseIf INDGleTypeReport.EditValue = 2 Then
                    reporte = New rptReportRequestDetailed()
                End If

                reporte.ParametrosReporte = New Object() {criterias}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDteDateStart.Focus()
                End If
            Catch ex As Exception
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDteDateStart.Focus()
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
                Using model As New Presentation.Authorization.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportRequests(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportRequests As DataTable = ds.Tables("ReportRequests")

                        Await Task.Factory.StartNew(Sub()
                                                        Select Case INDGleTypeReport.EditValue
                                                            Case 1
                                                                chargueDataSourceSummary(dtReportRequests)
                                                            Case 2
                                                                chargueDataSourceDetailed(dtReportRequests)
                                                            Case Else
                                                                Exit Sub
                                                        End Select
                                                    End Sub)

                        If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
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