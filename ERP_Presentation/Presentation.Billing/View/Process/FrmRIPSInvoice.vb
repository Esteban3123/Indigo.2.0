#Region "Imports"

Imports System.Text
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Presentation.Glosas

#End Region

Public Class FrmRIPSInvoice
    Implements IRIPSInvoice

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        IndigoGridControl1.SetHideNoRecords(INDGcInvoices, True)
    End Sub

#End Region

#Region "Variables"

    Private _presenter As PRIPSInvoice

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As String Implements IRIPSInvoice.MyTag
        Get
            Return Me.Tag.ToString()
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IRIPSInvoice.ShowMessage
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

#Region "Datasources"

    Private _FillingInvoiceType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingInvoiceType As List(Of Tuple(Of Byte, String))
        Get
            If _FillingInvoiceType Is Nothing Then
                _FillingInvoiceType = New List(Of Tuple(Of Byte, String))
                _FillingInvoiceType.Add(New Tuple(Of Byte, String)(1, "Factura EAPB con Contrato"))
                _FillingInvoiceType.Add(New Tuple(Of Byte, String)(2, "Factura EAPB Sin Contrato"))
                _FillingInvoiceType.Add(New Tuple(Of Byte, String)(3, "Factura Particular"))
                _FillingInvoiceType.Add(New Tuple(Of Byte, String)(5, "Control de Capitacion"))
                _FillingInvoiceType.Add(New Tuple(Of Byte, String)(99, "Todos"))
            End If
            Return _FillingInvoiceType
        End Get
    End Property

    Private _FillingReportType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Byte, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Byte, String))
                _FillingReportType.Add(New Tuple(Of Byte, String)(1, "General"))
                _FillingReportType.Add(New Tuple(Of Byte, String)(2, "Normativo"))
                _FillingReportType.Add(New Tuple(Of Byte, String)(3, "Reso 676 de 2020"))
            End If
            Return _FillingReportType
        End Get
    End Property

    Public Property CareCenterDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.CareCenterDatasource
        Get
            Return INDSleCareCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property ContractDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.ContractDatasource
        Get
            Return INDSleContract.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleContract.Properties.DataSource = value
        End Set
    End Property

    Public Property HealthAdministratorDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.HealthAdministratorDatasource
        Get
            Return INDSleHealthAdministrator.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleHealthAdministrator.Properties.DataSource = value
        End Set
    End Property

    Public Property CareGroupDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.CareGroupDatasource
        Get
            Return INDSleCareGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareGroup.Properties.DataSource = value
        End Set
    End Property

    Public Property InvoiceCategoryDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.InvoiceCategoryDatasource
        Get
            Return INDSleInvoiceCategory.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleInvoiceCategory.Properties.DataSource = value
        End Set
    End Property

    Public Property PopulationGroupDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.PopulationGroupDatasource
        Get
            Return INDSlePopulationGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePopulationGroup.Properties.DataSource = value
        End Set
    End Property

    Public Property AdmissionDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.AdmissionDatasource
        Get
            Return INDSleAdmission.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAdmission.Properties.DataSource = value
        End Set
    End Property

    Public Property IncomeCauseDatasource As XPInstantFeedbackSource Implements IRIPSInvoice.IncomeCauseDatasource
        Get
            Return INDSleIncomeCause.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleIncomeCause.Properties.DataSource = value
        End Set
    End Property

    Private _FillingIncomeCause As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingIncomeCause As List(Of Tuple(Of Integer, String))
        Get
            If _FillingIncomeCause Is Nothing Then
                _FillingIncomeCause = New List(Of Tuple(Of Integer, String))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(1, "Heridos en Combate"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(2, "Enfermedad Profesional"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(3, "Enfermedad General Adulto"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(4, "Enfermedad General Pediatría"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(5, "Odontología"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(6, "Accidente de Tránsito"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(7, "Evento Catastrófico"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(8, "Quemados"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(9, "Maternidad"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(10, "Accidente Laboral"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(11, "Cirugía Programada"))
                _FillingIncomeCause.Add(New Tuple(Of Integer, String)(19, "Evento terrorista"))
            End If
            Return _FillingIncomeCause
        End Get
    End Property

    Private _FillingTypeRisk As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingTypeRisk As List(Of Tuple(Of String, String))
        Get
            If _FillingTypeRisk Is Nothing Then
                _FillingTypeRisk = New List(Of Tuple(Of String, String))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("2", "Accidente de Tránsito"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("3", "Catástrofe"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("1", "Enfermedad General y Maternidad"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("5", "Accidente de Trabajo"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("6", "Enfermedad Profesional"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("7", "Atención Inicial de Urgencias"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("8", "Otro Tipo de Accidente"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("9", "Lesión Por Agresión"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("10", "Lesión AutoInfligida"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("11", "Maltrato Fisico"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("12", "Promoción y Prevención"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("13", "Otro"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("14", "Accidente Rábico"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("15", "Accidente Ofídico"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("16", "Sopecha de Abuso Sexual"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("17", "Sopecha de Violencia Sexual"))
                _FillingTypeRisk.Add(New Tuple(Of String, String)("18", "Sopecha de Maltrato Emocional"))
            End If
            Return _FillingTypeRisk
        End Get
    End Property

    Public Property ListInvoices As XPCollection(Of ViewRIPSInvoice) Implements IRIPSInvoice.ListInvoices
        Get
            Return INDGcInvoices.DataSource
        End Get
        Set(value As XPCollection(Of ViewRIPSInvoice))
            INDGcInvoices.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmRIPSInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PRIPSInvoice(Me)

        'Cargar GridLookUpEdit
        Me.INDGleInvoiceType.Properties.DataSource = FillingInvoiceType
        Me.INDGleReportType.Properties.DataSource = FillingReportType
        Me.INDSleTypeRisk.Properties.DataSource = FillingTypeRisk

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleInvoiceType.EditValue = 99
        Me.INDGleReportType.EditValue = 2

        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.GenerateFile) = False
        INDSleDetailPackage.EditValue = False
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing

        _FillingInvoiceType = Nothing
        _FillingReportType = Nothing
        _FillingIncomeCause = Nothing
        _FillingTypeRisk = Nothing
        INDSleDetailPackage.EditValue = False

    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDDeInitialDate.Focus()
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleCareCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareCenter.QueryPopUp
        _presenter.InitializeCareCenter()
    End Sub

    Private Sub INDSleContract_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleContract.QueryPopUp
        _presenter.InitializeContract()
    End Sub

    Private Sub INDSleHealthAdministrator_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHealthAdministrator.QueryPopUp
        _presenter.InitializeHealthAdministrator()
    End Sub

    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        _presenter.InitializeCareGroup()
    End Sub

    Private Sub INDSleInvoiceCategory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInvoiceCategory.QueryPopUp
        _presenter.InitializeInvoiceCategory()
    End Sub

    Private Sub INDSlePopulationGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePopulationGroup.QueryPopUp
        _presenter.InitializePopulationGroup()
    End Sub

    Private Sub INDSleAdmission_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAdmission.QueryPopUp
        _presenter.InitializeAdmission()
    End Sub

    Private Sub INDSleIncomeCause_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIncomeCause.QueryPopUp
        _presenter.InitializeIncomeCause()
    End Sub

#End Region

#Region "Selector"

    Private _selectorCareCenter As SelectorCache = New SelectorCache("CODCENATE", "NOMCENATE")
    Private _selectorContract As SelectorCache = New SelectorCache("Id", "CodeContractName")
    Private _selectorHealthAdministrator As SelectorCache = New SelectorCache("Id", "CodeName")
    Private _selectorCareGroup As SelectorCache = New SelectorCache("Id", "CodeName")
    Private _selectorInvoiceCategory As SelectorCache = New SelectorCache("Code", "CodeName")
    Private _selectorPopulationGroup As SelectorCache = New SelectorCache("ID", "CodigoDescripcion")
    Private _selectorIncomeCause As SelectorCache = New SelectorCache("Code", "Name")
    Private _selectorTypeRisk As SelectorCache = New SelectorCache("Item1", "Item2")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCareCenter.CustomUnboundColumnData, INDGvContract.CustomUnboundColumnData, INDGvHealthAdministrator.CustomUnboundColumnData, INDGvCareGroup.CustomUnboundColumnData, INDGvInvoiceCategory.CustomUnboundColumnData, INDGvPopulationGroup.CustomUnboundColumnData, INDGvIncomeCause.CustomUnboundColumnData, INDGvTypeRisk.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If view.Name = "INDGvCareCenter" Then
                e.Value = _selectorCareCenter.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvContract" Then
                e.Value = _selectorContract.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvHealthAdministrator" Then
                e.Value = _selectorHealthAdministrator.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvCareGroup" Then
                e.Value = _selectorCareGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvInvoiceCategory" Then
                e.Value = _selectorInvoiceCategory.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvPopulationGroup" Then
                e.Value = _selectorPopulationGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvIncomeCause" Then
                e.Value = _selectorIncomeCause.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvTypeRisk" Then
                e.Value = _selectorTypeRisk.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCareCenter.RowCellClick, INDGvContract.RowCellClick, INDGvHealthAdministrator.RowCellClick, INDGvCareGroup.RowCellClick, INDGvInvoiceCategory.RowCellClick, INDGvPopulationGroup.RowCellClick, INDGvIncomeCause.RowCellClick, INDGvTypeRisk.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If view.Name = "INDGvCareCenter" Then
                selector = _selectorCareCenter
            ElseIf view.Name = "INDGvContract" Then
                selector = _selectorContract
            ElseIf view.Name = "INDGvCareGroup" Then
                selector = _selectorCareGroup
            ElseIf view.Name = "INDGvHealthAdministrator" Then
                selector = _selectorHealthAdministrator
            ElseIf view.Name = "INDGvInvoiceCategory" Then
                selector = _selectorInvoiceCategory
            ElseIf view.Name = "INDGvPopulationGroup" Then
                selector = _selectorPopulationGroup
            ElseIf view.Name = "INDGvIncomeCause" Then
                selector = _selectorIncomeCause
            ElseIf view.Name = "INDGvTypeRisk" Then
                selector = _selectorTypeRisk
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCareCenter.Closed, INDSleContract.Closed, INDSleHealthAdministrator.Closed, INDSleCareGroup.Closed, INDSleInvoiceCategory.Closed, INDSlePopulationGroup.Closed, INDSleIncomeCause.Closed, INDSleTypeRisk.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCareCenter" Then
            searchLookupEdit.Properties.NullText = _selectorCareCenter.ToString()
        ElseIf searchLookupEdit.Name = "INDSleContract" Then
            searchLookupEdit.Properties.NullText = _selectorContract.ToString()
        ElseIf searchLookupEdit.Name = "INDSleHealthAdministrator" Then
            searchLookupEdit.Properties.NullText = _selectorHealthAdministrator.ToString()
        ElseIf searchLookupEdit.Name = "INDSleCareGroup" Then
            searchLookupEdit.Properties.NullText = _selectorCareGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleInvoiceCategory" Then
            searchLookupEdit.Properties.NullText = _selectorInvoiceCategory.ToString()
        ElseIf searchLookupEdit.Name = "INDSlePopulationGroup" Then
            searchLookupEdit.Properties.NullText = _selectorPopulationGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleIncomeCause" Then
            searchLookupEdit.Properties.NullText = _selectorIncomeCause.ToString()
        ElseIf searchLookupEdit.Name = "INDSleTypeRisk" Then
            searchLookupEdit.Properties.NullText = _selectorTypeRisk.ToString()
        End If
    End Sub

#End Region

#Region "ColumnCheckedChanged"

    Private Sub GridViewColumnHeaderExtender1_ColumnCheckedChanged(sender As Object, e As ColumnCheckedChangedEventArgs) Handles GridViewColumnHeaderExtender1.ColumnCheckedChanged
        If ListInvoices IsNot Nothing AndAlso ListInvoices.Count() > 0 Then
            Dim listFilterXpCollection = GvInvoices.DataController.GetAllFilteredAndSortedRows()
            For Each li In listFilterXpCollection
                li.CheckValue = e.Checked
            Next
        End If

        Me.INDGcInvoices.RefreshDataSource()
        Me.INDGcInvoices.Invalidate()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        'General and Reso676
        Dim value As Boolean = False
        If INDGleReportType.EditValue = 1 OrElse INDGleReportType.EditValue = 3 Then
            value = True
        End If
        INDLciCareCenter.HideControl(Not value)
        INDLciContract.HideControl(Not value)
        INDLciHealthAdministrator.HideControl(Not value)
        INDLciCareGroup.HideControl(Not value)
        INDLciInvoiceCategory.HideControl(Not value)
        INDLciAdmission.HideControl(Not value)

        'Normativo
        INDLciPopulationGroup.HideControl(Not (INDGleReportType.EditValue = 2))
        INDLciIncomeCause.HideControl(Not (INDGleReportType.EditValue = 2))
        INDLciTypeRisk.HideControl(Not (INDGleReportType.EditValue = 2))
    End Sub

    Private Sub INDRICESel_EditValueChanged(sender As Object, e As EventArgs) Handles INDRICESel.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim _invoice = TryCast(GvInvoices.GetFocusedRow(), ViewRIPSInvoice)
        If _invoice IsNot Nothing Then
            _invoice.CheckValue = checkControl.EditValue
            Me.INDGcInvoices.RefreshDataSource()
            Me.INDGcInvoices.Invalidate()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnFilter_Click(sender As Object, e As EventArgs) Handles INDBtnFilter.Click
        Try
            Using model As New MLiquidation()
                Dim errors As New StringBuilder()

                If INDDeInitialDate.EditValue Is Nothing Or INDDeEndDate.EditValue Is Nothing Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
                    Me.INDDeInitialDate.Focus()
                ElseIf Me.INDDeInitialDate.EditValue > INDDeEndDate.EditValue Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting")))
                    Me.INDDeEndDate.Focus()
                End If

                If errors.Length > 0 Then
                    Mensaje(Base.EeventViewerImages.Advertencia) = errors.ToString()
                    Exit Sub
                End If

                AsyncLoader(True)

                If INDGleReportType.EditValue = 1 Then
                    ListInvoices = model.ListRIPSInvoices(INDDeInitialDate.EditValue, INDDeEndDate.EditValue, CByte(INDGleInvoiceType.EditValue), INDDeCutoffDate.EditValue, _selectorHealthAdministrator.GetKeys(), _selectorCareGroup.GetKeys(), INDSleAdmission.EditValue, _selectorCareCenter.GetKeys(), _selectorContract.GetKeys(), _selectorInvoiceCategory.GetKeys(), Nothing, Nothing)
                ElseIf INDGleReportType.EditValue = 2 Then
                    If String.IsNullOrEmpty(_selectorPopulationGroup.GetKeys()) Then
                        ListInvoices = model.ListRIPSInvoices(INDDeInitialDate.EditValue, INDDeEndDate.EditValue, CByte(INDGleInvoiceType.EditValue), INDDeCutoffDate.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _selectorIncomeCause.GetKeys(), _selectorTypeRisk.GetKeys())
                    Else
                        ListInvoices = model.ListRIPSInvoicesPopulationGroup(INDDeInitialDate.EditValue, INDDeEndDate.EditValue, CByte(INDGleInvoiceType.EditValue), INDDeCutoffDate.EditValue, _selectorHealthAdministrator.GetKeys(), _selectorCareGroup.GetKeys(), INDSleAdmission.EditValue, _selectorCareCenter.GetKeys(), _selectorContract.GetKeys(), _selectorInvoiceCategory.GetKeys(), _selectorIncomeCause.GetKeys(), _selectorTypeRisk.GetKeys(), _selectorPopulationGroup.GetKeys())
                    End If
                ElseIf INDGleReportType.EditValue = 3 Then
                    ListInvoices = model.ListRIPSInvoices(INDDeInitialDate.EditValue, INDDeEndDate.EditValue, CByte(INDGleInvoiceType.EditValue), INDDeCutoffDate.EditValue, _selectorHealthAdministrator.GetKeys(), _selectorCareGroup.GetKeys(), INDSleAdmission.EditValue, _selectorCareCenter.GetKeys(), _selectorContract.GetKeys(), _selectorInvoiceCategory.GetKeys(), Nothing, Nothing, True)
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    Private Sub BarraBotones_GenerarArchivo() Handles BarraBotones.Click_GenerateFile
        If INDDeCutoffDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la Fecha de Corte"
            Me.INDDeCutoffDate.Focus()
            Exit Sub
        End If

        If ListInvoices Is Nothing OrElse Not ListInvoices.Any(Function(d) d.CheckValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una Factura"
            Exit Sub
        End If

        Dim listSelectedInvoices As New List(Of RIPSBilling)()
        For Each document In ListInvoices.Where(Function(li) li.CheckValue)
            listSelectedInvoices.Add(New RIPSBilling() With
            {
                .CapitationEndDate = document.CapitationEndDate,
                .CapitationInitialDate = document.CapitationInitialDate,
                .CareGroupId = document.CareGroupId,
                .DocumentType = document.DocumentType,
                .InvoiceCategoryId = document.InvoiceCategoryId,
                .InvoiceId = document.InvoiceId,
                .InvoiceRadicateId = document.InvoiceRadicateId,
                .ThirdPartyId = document.ThirdPartyId,
                .FechaCorte = INDDeCutoffDate.EditValue,
                .AdmissionNumber = document.AdmissionNumber
            })
        Next

        Dim frmPopUpRips As New FrmPopupRIPS()
        frmPopUpRips.Width = 540
        frmPopUpRips.Height = 365
        Dim frmTransparent As New FrmTransparent(frmPopUpRips, False)
        frmPopUpRips.InvoicesList = listSelectedInvoices
        frmPopUpRips.ConsecutiveRadicateInvoice = listSelectedInvoices(0).FechaCorte.ToString("yyyyMM")
        frmPopUpRips.DetailPackage = INDSleDetailPackage.EditValue
        frmTransparent.ShowDialog(Me)
    End Sub

#End Region

End Class