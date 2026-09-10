'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/03/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Dynamic
Imports System.Windows.Forms
Imports DevExpress.Data.PLinq
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Docking2010.Views.Widget
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraLayout
Imports DevExpress.XtraSplashScreen
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.FrmPopupReleaseLine
Imports Presentation.MixingStation.MVP
#End Region

Public Class CtrCampaign

#Region "Builders"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(_formOwner As ICampaign, Optional document As Document = Nothing)
        InitializeComponent()

        _documentParent = document
        FormOwner = _formOwner
    End Sub

#End Region

#Region "Variables"

    Private waitForm As New SplashScreenManager(Me, GetType(wfMain), False, True, ParentType.UserControl)
    ''' <summary>
    ''' Fuente de datos
    ''' </summary>
    Private _datasource As PLinqServerModeSource

    ''' <summary>
    ''' Fuente de datos
    ''' </summary>
    Private _datasourceProcessRequest As List(Of Domain.Entities.SP_ListViewItemsCampaigns_Result)

    ''' <summary>
    ''' Referencia al documento padre quien aloja el control
    ''' </summary>
    Private WithEvents _documentParent As Document

    ''' <summary>
    ''' Formulario padre, donde se encuentra alojado el control
    ''' </summary>
    Public Property FormOwner As ICampaign

    ''' <summary>
    ''' No. de la campaña
    ''' </summary>
    Private CampaignNumber As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

#End Region

#Region "Events"

    ''' <summary>
    ''' Ocurre cuando se requiere mandar a recargar un listado de  campañas
    ''' </summary>
    Public Event RequiereReloadCampaignList(ByVal sender As Object, ByVal e As RequiereReloadCampaignListEventArgs)
    Public Event BeginReloadCampaignDetailIdStatus(ByVal sender As Object, campaignDetailId As Integer)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el LayoutControlGroup del cuerpo del folio
    ''' </summary>
    ''' <returns>LayoutControlGroup del cuerpo del folio</returns>
    Public ReadOnly Property LayoutBodyFolio As LayoutControlGroup
        Get
            Return Me.INDlygBody
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del AsyncOperationBar
    ''' </summary>
    ''' <returns>LayoutControlItem del AsyncOperationBar</returns>
    Public ReadOnly Property LayoutAsyncOperationBar As LayoutControlItem
        Get
            Return Me.LyciAsyncOperationBar
        End Get
    End Property

    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me.FormOwner)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me.FormOwner)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the show message.
    ''' </summary>
    ''' <value>
    ''' The show message.
    ''' </value>
    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me.FormOwner)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me.FormOwner)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property


    Private _ProductionBasketId As Integer?
    ''' <summary>
    ''' Id de la central de mezcla escogida en el form principal
    ''' </summary>
    Public Property ProductionBasketId As Integer?
        Get
            Return _ProductionBasketId
        End Get
        Set(value As Integer?)
            _ProductionBasketId = value
        End Set
    End Property

    Private _cmConfigurationId As Integer
    ''' <summary>
    ''' Id de la central de mezcla escogida en el form principal
    ''' </summary>
    Public Property CMConfigurationId As Integer
        Get
            Return _cmConfigurationId
        End Get
        Set(value As Integer)
            _cmConfigurationId = value
        End Set
    End Property

    Private _campaignId As Integer
    ''' <summary>
    ''' Id de la cabecera de la campaña, viene desde el form principal
    ''' </summary>
    Public Property CampaignId As Integer
        Get
            Return _campaignId
        End Get
        Set(value As Integer)
            _campaignId = value
        End Set
    End Property

    Private _campaignXpo As CampaignXpo
    ''' <summary>
    ''' Entidad xpo de la campaña
    ''' </summary>
    Public Property CampaignXpo As CampaignXpo
        Get
            Return _campaignXpo
        End Get
        Set(value As CampaignXpo)
            _campaignXpo = value
        End Set
    End Property


    Private _objCampaignDetailXpo As CampaignDetailXpo
    ''' <summary>
    ''' Entidad xpo del Detalle de la campaña
    ''' </summary>
    Public Property objCampaignDetailXpo As CampaignDetailXpo
        Get
            Return _objCampaignDetailXpo
        End Get
        Set(value As CampaignDetailXpo)
            _objCampaignDetailXpo = value
        End Set
    End Property



    Private _campaignDetailId As Integer
    ''' <summary>
    ''' Id del detalle que representa la campaña
    ''' </summary>
    Public ReadOnly Property CampaignDetailId As Integer
        Get
            Return _campaignDetailId
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el documento padre que alojará el control
    ''' </summary>
    ''' <value>Documento padre</value>
    ''' <returns>El documento padre</returns>
    Public Property DocumentParent As Document
        Get
            Return Me._documentParent
        End Get
        Set(value As Document)
            Me._documentParent = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el titulo del documento
    ''' </summary>
    ''' <returns>Número de la factura</returns>
    Public ReadOnly Property TitleDocument As String
        Get
            Return INDlblTitle.Text
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la linea de producción
    ''' </summary>
    ''' <returns>Número de la factura</returns>
    Public ReadOnly Property ProductionLineCodeName As String
        Get
            Return INDsleProductionLine.Text
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tipo de dosis unitaria
    ''' </summary>
    ''' <returns>Número de la factura</returns>
    Public ReadOnly Property UnitDoseTypeCodeName As String
        Get
            Return INDsleUnitDoseType.Text
        End Get
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Termina una campaña
    ''' </summary>
    Private Async Sub EndCampaign()
        Try
            Dim campaignDetailId As Integer = 0

            If TypeOf INDviewItems.GetFocusedRow() Is Domain.Entities.SP_ListViewItemsCampaigns_Result Then
                Dim infoXpo = INDviewItems.GetFocusedObject(Of Domain.Entities.SP_ListViewItemsCampaigns_Result)
                campaignDetailId = infoXpo.CampaignDetailId
            Else
                Dim infoXpo = INDviewItems.GetFocusedObject(Of ViewListCampaignDetailWithRequestsXpo)
                campaignDetailId = infoXpo.CampaignDetailId
            End If

            Using model As New MCampaign(Tag)
                IsAsyncOperation()

                Dim resultValidation As ActionResult = Await model.ValidateCampaignToEnd(campaignDetailId)
                If resultValidation.StateResult Then
                    Dim resultDialog = ShowObservationsPopup()
                    If resultDialog.result = Windows.Forms.DialogResult.OK Then
                        Dim result As ActionResult = Await model.EndCampaign(campaignDetailId, resultDialog.observation)
                        If result.StateResult Then
                            RaiseEvent RequiereReloadCampaignList(Me, New RequiereReloadCampaignListEventArgs({campaignDetailId}.ToList()))
                            RaiseEvent BeginReloadCampaignDetailIdStatus(Me, campaignDetailId)
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End If
                Else
                    ShowMessage(EeventViewerImages.Advertencia) = resultValidation.Message
                End If
                IsAsyncOperation(False)
            End Using
        Catch ex As Exception
            IsAsyncOperation(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' abre modal para escribir la observacion
    ''' </summary>
    Private Function ShowObservationsPopup() As (result As Windows.Forms.DialogResult, observation As String)
        Using formulario As New FrmObservationModal()
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = Windows.Forms.Cursors.Default
            Return (transparent.ShowDialog(Me), formulario.Observations)
        End Using
    End Function

    ''' <summary>
    ''' Método para cambiar el item seleccionado en la campaña correspondiente
    ''' </summary>
    Private Sub OpenFormSelectedCampaign()
        Using Formulario As New FrmSelectedCampaign()
            AddHandler Formulario.AddSelected, AddressOf SaveChangeItemCampaign
            Formulario.ListCampaignDetail = (From x In _campaignXpo.CampaignDetailXpo
                                             Where x.ProductionLineId.Id = INDsleProductionLine.EditValue AndAlso x.UnitDoseTypeId.Id = INDsleUnitDoseType.EditValue AndAlso x.CampaignNumber <> CampaignNumber AndAlso x.CampaignStatus = 1
                                             Select x).ToList()
            Formulario.ProductionLineId = INDsleProductionLine.EditValue
            Formulario.UnitDoseTypeId = INDsleUnitDoseType.EditValue
            Formulario.ListItems = (From x In INDviewItems.GetSelectedRows() Select DirectCast(INDviewItems.GetRow(x), ViewListCampaignDetailWithRequestsXpo)).ToList()
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 750
            Formulario.Height = 500
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método para Gestionar materia prima a Paquete elaborado 
    ''' </summary>
    Private Sub OpenFormManageRawMaterial()

    End Sub
    ''' <summary>
    ''' Retorno de la selección de campaña
    ''' </summary>
    Private Sub SaveChangeItemCampaign(ByVal e As AddSelectedCampaign)
        ChangeItemCampaign(e)
    End Sub

    ''' <summary>
    ''' Procesa una devolución de materia prima
    ''' </summary>
    Private Sub DevolutionProcess()
        Using formulario As New FrmRawMaterialDevolution()
            formulario.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            AddHandler formulario.Load, Sub()
                                            formulario.AsyncLoader(True)
                                        End Sub
            AddHandler formulario.LoadEndForm, Async Sub() Await formulario.LoadRawMaterialDevolutionModal(_objCampaignDetailXpo.Id, _objCampaignDetailXpo.FullTitle)
            AddHandler formulario.ActionCompleted, Sub() formulario.Close()
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Guarda los items
    ''' </summary>
    Private Async Sub ChangeItemCampaign(ByVal e As AddSelectedCampaign)
        Dim myListDetail = AssigningInfo(e.ListItems)
        Dim args As Object = New ExpandoObject()
        args.Id = CampaignId
        args.CMConfigurationId = CMConfigurationId
        args.ProductionLineId = INDsleProductionLine.EditValue
        args.UnitDoseTypeId = INDsleUnitDoseType.EditValue
        args.CampaignDetailId = e.CampaignDetailId
        args.Details = myListDetail

        Me.IsAsyncOperation()
        Try
            Using model As New MCampaign("")
                Dim result = Await model.SaveCampaign(args)
                If result.StateResult Then
                    ShowMessage(EeventViewerImages.Informacion) = "Se asignó el item a la campaña correctamente"
                    ReloadCampaignEventArgs(Nothing, Nothing)
                Else
                    Me.IsAsyncOperation(False)
                    ShowMessage(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.IsAsyncOperation(False)
            ShowMessage(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Function AssigningInfo(listItems As List(Of ViewListCampaignDetailWithRequestsXpo)) As ConcurrentBag(Of Object)
        Dim myListDetail As New ConcurrentBag(Of Object)()

        For Each item In listItems
            Dim detail As Object = New ExpandoObject()
            detail.SourceType = item.RequestType
            detail.StringIds = item.StringIds
            detail.CampaingDetailOriginId = item.CampaignDetailId
            detail.RequestMixingStationDetailId = item.RequestMixingStationDetailId
            myListDetail.Add(detail)
        Next

        Return myListDetail
    End Function

    ''' <summary>
    ''' Abre el form de detalles de la solicitud
    ''' </summary>
    Private Sub OpenFormRawMaterial()
        Using formulario As New FrmRawMaterial()
            'AddHandler formulario.ReloadPrincipalGridArgs, AddressOf ReturnAddEventArgs
            formulario.PermissionsForm = FormOwner.PermissionsForm
            formulario.objCampaignDetailXpo = objCampaignDetailXpo
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            ReloadCampaignEventArgs(Nothing, Nothing)
        End Using
    End Sub

    ''' <summary>
    ''' Verifica permisos de usuario y asigna el menu
    ''' </summary>
    Private Sub SetListActions()

        If INDviewItems.Columns.FirstOrDefault(Function(m) m.Name = "colActions") Is Nothing Then
            IndigoGridView1.SetListAcction(INDviewItems, {eAcciones.ShowRequestDetail,
                                           eAcciones.SendTo,
                                       eAcciones.ProcessfinishedProduct,
                                       eAcciones.AssignReadjustment}.ToList())
        End If

        Dim col = INDviewItems.Columns.FirstOrDefault(Function(m) m.Name = "colActions")

        If col IsNot Nothing Then col.Width = 70

        Dim preparationStatus = Presenter.GetSumQuantityByPreparationStatus(objCampaignDetailXpo.Id)
        Dim allControls As DevExpress.XtraBars.BarButtonItem() = {
                                                                    INDBbiAdequacyPlanNPT, MbtnProcessRawMaterial, INDBbiLineRelease, INDBtnKardexCampaign, INDBbiDevolutionProcess,
                                                                    INDBtnProcessSettings, INDBbiEndCampaign, INDBbiDefectClassification, INDBbiProcessFinishedProductMassive
                                                                  }

        SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, allControls)

        Select Case objCampaignDetailXpo.CampaignStatus
            Case 1, 3, 4
                SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh)
            Case 2
                SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh, MbtnProcessRawMaterial)
            Case 5
                MbtnProcessRawMaterial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                'Si ya confirmó orden de traslado del procesar materia prima
                If objCampaignDetailXpo.Status = 4 Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh, MbtnProcessRawMaterial, INDBtnKardexCampaign, INDBbiDevolutionProcess)

                    If objCampaignDetailXpo.CampaignDetailUsersXpo.Any(Function(m) m.UserCode = SessionValues.Instance.UserIndigo) Then
                        INDBbiLineRelease.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    End If
                End If

                'Liberación de la línea OK
                If Not objCampaignDetailXpo.ReleaseLines.Any(Function(x) x.AdequacyItem1 = False OrElse x.ConditioningItem1 = False) Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, INDBbiProcessFinishedProductMassive)
                End If

                'Todos los productos terminados
                If preparationStatus.FinishedQuantity = preparationStatus.RequestedQuantity Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, INDBbiDefectClassification, INDBtnProcessSettings)
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, INDBbiProcessFinishedProductMassive)
                End If

                'Todos los productos liberados, rechazados o anulados
                If preparationStatus.RequestedQuantity = (preparationStatus.ReleasedQuantity + preparationStatus.RejectedQuantity + preparationStatus.CancelledQuantity) Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnProcessRawMaterial, INDBbiLineRelease, INDBtnKardexCampaign, INDBbiDevolutionProcess,
                                         INDBbiDefectClassification, INDBtnProcessSettings, INDBbiEndCampaign)
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, INDBbiProcessFinishedProductMassive)
                End If
            Case 6
                SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh, INDBtnKardexCampaign)
        End Select

        If (_objCampaignDetailXpo.UnitDoseTypeId.MSClass = 2) Then
            INDBbiAdequacyPlanNPT.Visibility = If(_objCampaignDetailXpo.CampaignStatus = 2, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        End If

    End Sub

    ''' <summary>
    ''' Cambiar estado de los botones del menú de manera masiva
    ''' </summary>
    Private Sub SetBarItemVisibility(ByVal visibility As DevExpress.XtraBars.BarItemVisibility, ParamArray controls() As DevExpress.XtraBars.BarButtonItem)
        For Each control In controls
            control.Visibility = visibility
        Next
    End Sub

    ''' <summary>
    ''' Bloquea los folios a liquidar
    ''' </summary>
    Private Sub LockFoliosToLiquidate(campaignDetailIds As List(Of Integer), lock As Boolean)
        Me.IsAsyncOperation(lock)
    End Sub

    ''' <summary>
    ''' Indica si el folio esta realizando una operación asíncrona
    ''' </summary>
    ''' <param name="isAsyncOperation">Valor que indica si se esta realizando una operación asíncrona</param>
    Public Sub IsAsyncOperation(Optional ByVal isAsyncOperation As Boolean = True)
        If isAsyncOperation Then
            DdbActions.Enabled = False
            LayoutBodyFolio.Enabled = False
            LayoutAsyncOperationBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.DdbActions.Enabled = True
            LayoutBodyFolio.Enabled = True
            LayoutAsyncOperationBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource de la rejilla
    ''' </summary>
    ''' <param name="_detailId"></param>
    Public Sub SetDatasourceAsync(_detailId As Integer)
        _campaignDetailId = _detailId
        If Not BgwSetDatasourceAsync.IsBusy Then
            BgwSetDatasourceAsync.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' Show/Hide loading del gridView
    ''' </summary>
    Public Sub SetLoadingGrid(_isVisibility As Boolean)
        If _isVisibility Then
            IsAsyncOperation()
            INDviewItems.ShowLoadingPanel()
        Else
            IsAsyncOperation(False)
            INDviewItems.HideLoadingPanel()
        End If
    End Sub

    ''' <summary>
    ''' Carga la información de los controles
    ''' </summary>
    Public Sub SetInformationControls(_entityXpo As CampaignDetailXpo)
        INDlblTitle.Text = "Campaña # " & _entityXpo.CampaignNumber
        INDlblTitle.ToolTip = "Campaña # " & _entityXpo.CampaignNumber

        INDsleProductionLine.EditValue = _entityXpo.ProductionLineId.Id
        INDsleProductionLine.Properties.NullText = _entityXpo.ProductionLineId.CodeName

        INDsleUnitDoseType.EditValue = _entityXpo.UnitDoseTypeId.Id
        INDsleUnitDoseType.Properties.NullText = _entityXpo.UnitDoseTypeId.CodeDescription
        CampaignNumber = _entityXpo.CampaignNumber

        'Alerta 
        Dim textOntooltips As String = String.Empty
        If _entityXpo.CampaignStatus = 5 AndAlso _entityXpo.ReleaseLines.Any(Function(x) x.AdequacyItem1 = False OrElse x.ConditioningItem1 = False) Then
            LCIPictureBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Dim releaseLine = _entityXpo.ReleaseLines.Where(Function(x) x.AdequacyItem1 = False OrElse x.ConditioningItem1 = False).LastOrDefault
            If Not releaseLine.AdequacyItem1 Then
                textOntooltips = "Pendiente liberar área de adecuación"
            ElseIf Not releaseLine.ConditioningItem1 Then
                textOntooltips = "Pendiente liberar área de acondicionamiento"
            Else
                textOntooltips = "Pendiente liberar áreas de trabajo"
            End If
            ToolTip1.SetToolTip(PictureBoxAletCampaing, textOntooltips)
        Else
            LCIPictureBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' DoWork Establece el Datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BgwSetDatasourceAsync_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BgwSetDatasourceAsync.DoWork
        Using model As New MCampaign("")
            objCampaignDetailXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MixingStationService.GetXPOObject(Of CampaignDetailXpo)($"Id = {CampaignDetailId}")
            'si el estado de la campaña es Procesada(Campaign status = 5) Realiza la consulta al SP_ListViewItemsCampaigns_Result para traer las cantidades con productos en proceso y terminado
            If Not {5, 6}.Contains(objCampaignDetailXpo.CampaignStatus) Then
                _datasource = model.ListCampaignDetailWithRequests(_campaignDetailId)
            Else
                Dim result = model.GetItemsByCampaigns(_campaignDetailId)
                If result Is Nothing Then
                    ShowMessage(EeventViewerImages.Advertencia) = "La consulta no produjo ningún resultado"
                    Exit Sub
                End If
                If result.StateResult = False Then
                    ShowMessage(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
                _datasourceProcessRequest = result.Data.ToList()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Complete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BgwSetDatasourceAsync_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgwSetDatasourceAsync.RunWorkerCompleted
        SetLoadingGrid(False)
        SetListActions()
        CampaignId = objCampaignDetailXpo.CampaignId.Id
        SetInformationControls(objCampaignDetailXpo)
        ProductionBasketId = objCampaignDetailXpo.ProductionBasketId

        Window.Utils.SetValueToProperty(INDgcItems, "DataSource", Nothing)
        If Me._datasource IsNot Nothing AndAlso DirectCast(Me._datasource.Source, List(Of ViewListCampaignDetailWithRequestsXpo)).Count > 0 Then
            Window.Utils.SetValueToProperty(INDgcItems, "DataSource", DirectCast(Me._datasource.Source, List(Of ViewListCampaignDetailWithRequestsXpo)))
        End If
        If _datasourceProcessRequest IsNot Nothing AndAlso _datasourceProcessRequest.Count > 0 Then
            Window.Utils.SetValueToProperty(INDgcItems, "DataSource", _datasourceProcessRequest)
        End If
        ColumnVisibility(objCampaignDetailXpo.CampaignStatus)
        IsAsyncOperation(False)
    End Sub

    ''' <summary>
    ''' Abre el formulario para ver los pacientes asociados
    ''' </summary>
    ''' <param name="infoXpo"></param>
    Private Sub ShowDetail(infoXpo As ViewListCampaignDetailWithRequestsXpo)
        Using formulario As New FrmDetailRequest()
            AddHandler formulario.ReloadCampaignArgs, AddressOf ReloadCampaignEventArgs
            formulario.PermissionsForm = FormOwner.PermissionsForm
            formulario.RequestMixingStationDetailId = infoXpo.RequestMixingStationDetailId
            formulario.SetTitleWindow = String.Format("Detalles de Solicitud #{0}", infoXpo.RequestCode)
            formulario.SetLabelGroup = infoXpo.ItemCodeName
            formulario.CampaignStatus = infoXpo.CampaignStatus
            formulario.InternalState = objCampaignDetailXpo.Status
            formulario.IsWorkingAreaAssigned = objCampaignDetailXpo.ReleaseLines.Any()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.6)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub ViewListDetail(view As GridView)
        Dim infoXpo As Object
        'si el estado de la campaña es Procesada se consulta el detalle de la solicitud
        If {5, 6}.Contains(objCampaignDetailXpo.CampaignStatus) Then
            SetLoadingGrid(True)
            infoXpo = CType(view.GetFocusedRow(), Domain.Entities.SP_ListViewItemsCampaigns_Result)
            Await Task.Factory.StartNew(Sub()
                                            infoXpo = Presenter.SingleCampaingDetailWithRequests(infoXpo.Code.ToString())
                                        End Sub)
            SetLoadingGrid(False)
        Else
            infoXpo = CType(view.GetFocusedRow(), ViewListCampaignDetailWithRequestsXpo)
        End If
        ShowDetail(infoXpo)
    End Sub

    ''' <summary>
    ''' Procesa producto terminado para todos los items
    ''' </summary>
    Private Async Sub ProcessFinishedProduct()
        If MessageIndigo.Show("¿Esta seguro de cambiar a estado producto terminado todos los items de la solicitud?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            IsAsyncOperation()
            'Dim row = CType(INDviewItems.GetFocusedRow(), SP_ListViewItemsCampaigns_Result)
            Using model As New MCampaign(Tag)
                Dim requestMixingStationDetailIds = (From x In INDviewItems.GetSelectedRows() Select Convert.ToInt32(DirectCast(INDviewItems.GetRow(x), SP_ListViewItemsCampaigns_Result).code)).ToList()
                Dim result = Await model.ProcessFinishedProductAsync(requestMixingStationDetailIds)

                If result.ObjectEmbbeded IsNot Nothing Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbeded)
                        formulario.Title = "Resultado de mensajes"
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                    IsAsyncOperation(False)
                Else
                    SetDatasourceAsync(_campaignDetailId)
                End If
            End Using
        Catch ex As Exception
            IsAsyncOperation(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Recarga la campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReloadCampaignEventArgs(sender As Object, e As EventArgs)
        Me.IsAsyncOperation(True)
        SetDatasourceAsync(_campaignDetailId)
    End Sub

    ''' <summary>
    ''' Ajusta la rejilla Para ocultar una u otra columna en funcion del estado de la campaña como parametro de entrada
    ''' </summary>
    ''' <param name="StatusCampaing"></param>
    Private Sub ColumnVisibility(StatusCampaing As Integer?)
        If StatusCampaing Is Nothing Then
            Exit Sub
        End If
        If StatusCampaing = 5 Then
            With INDColPP
                .Visible = True
                .VisibleIndex = 5
            End With
            With INDColPT
                .Visible = True
                .VisibleIndex = 6
            End With
            With INDColPL
                .Visible = True
                .VisibleIndex = 7
            End With
            With INDColPR
                .Visible = True
                .VisibleIndex = 8
            End With
            With INDColPA
                .Visible = True
                .VisibleIndex = 9
            End With
        ElseIf StatusCampaing = 6 Then
            With INDColPL
                .Visible = True
                .VisibleIndex = 5
            End With
            With INDColPR
                .Visible = True
                .VisibleIndex = 6
            End With
            With INDColPA
                .Visible = True
                .VisibleIndex = 7
            End With
        Else
            With INDColPP
                .Visible = False
            End With
            With INDColPT
                .Visible = False
            End With
            With INDColPL
                .Visible = False
            End With
            With INDColPR
                .Visible = False
            End With
        End If
    End Sub

    ''' <summary>
    ''' funcion para asignar readecuaciones
    ''' </summary>
    Private Sub AssignReadjustment(RequestMixingStationDetailId As Integer)
        Using formulario As New FrmAssignReadjustment(RequestMixingStationDetailId)
            AddHandler formulario.AddSelected, AddressOf ReloadCampaignEventArgs
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.5)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    'Private Sub ReturnAddEventArgs(sender As Object, e As EventArgs)
    '    BeginReloadDatasource()
    'End Sub
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Se dispara al cargar el control de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CtrCampaign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PCampaigns
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProductionLine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionLine.QueryPopUp
        If INDsleProductionLine.Properties.DataSource Is Nothing Then
            Using model As New MCampaign("")
                INDsleProductionLine.Properties.DataSource = model.InitializeProductionLine()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al deslpegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUnitDoseType.QueryPopUp
        If INDsleUnitDoseType.Properties.DataSource Is Nothing Then
            Using model As New MCampaign("")
                INDsleUnitDoseType.Properties.DataSource = model.InitializeUnitDoseType()
            End Using
        End If
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Grid Event -Asignar valores Campaign Detail Status 2 - 4 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewItems_CustomUnboundColumnData(sender As Object, e As Views.Base.CustomColumnDataEventArgs) Handles INDviewItems.CustomUnboundColumnData
        If TypeOf e.Row Is SP_ListViewItemsCampaigns_Result Then
            Dim row = DirectCast(e.Row, SP_ListViewItemsCampaigns_Result)
            Select Case e.Column.Name
                Case INDColPP.Name
                    e.Value = row.StatusPP + row.StatusPPR
                Case INDcolStatusCrystal.Name
                    If e.IsGetData AndAlso {3, 4, 7}.Contains(row.StatusHCPRESCRA) Then
                        e.Value = GetImage(My.Resources.Alerta)
                    End If
                    If row.IsReadjustment = 1 AndAlso Not {3, 4, 7}.Contains(row.StatusHCPRESCRA) Then
                        e.Value = GetImage(My.Resources.Readecuacion)
                    End If
            End Select
        ElseIf TypeOf e.Row Is ViewListCampaignDetailWithRequestsXpo Then
            Dim row = DirectCast(e.Row, ViewListCampaignDetailWithRequestsXpo)
            If e.Column.Name = INDcolStatusCrystal.Name AndAlso e.IsGetData AndAlso {3, 4, 7}.Contains(row.StatusHCPRESCRA) Then
                e.Value = GetImage(My.Resources.Alerta)
            End If
            If row.IsReadjustment = 1 AndAlso Not {3, 4, 7}.Contains(row.StatusHCPRESCRA) Then
                e.Value = GetImage(My.Resources.Readecuacion)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Click procesar materia prima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub MbtnProcessRawMaterial_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnProcessRawMaterial.ItemClick
        Using model As New MCampaign("")
            Me.IsAsyncOperation()
            Dim result = Await model.GetCampaignDetailById(_objCampaignDetailXpo.Id, True)
            Me.IsAsyncOperation(False)

            If result IsNot Nothing AndAlso result.StateResult = False Then
                ShowMessage(EeventViewerImages.Advertencia) = result.Message
                Exit Sub
            End If
            If (result.ObjectEmbbeded.CampaignStatus <> _objCampaignDetailXpo.CampaignStatus) OrElse (result.ObjectEmbbeded.Status <> _objCampaignDetailXpo.Status) Then
                ShowMessage(EeventViewerImages.Advertencia) = "La campaña tiene un estado desactualizado, Refresque el Dashboard"
                Exit Sub
            End If
            If Not {2, 5}.Contains(result.ObjectEmbbeded.CampaignStatus) Then
                ShowMessage(EeventViewerImages.Advertencia) = "La campaña debe estar en estado Cerrada o Procesada para Procesar Materia Prima"
                Exit Sub
            End If
        End Using
        OpenFormRawMaterial()
    End Sub

    ''' <summary>
    ''' Click en procesar materia prima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiDevolutionProcess_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiDevolutionProcess.ItemClick
        DevolutionProcess()
    End Sub

    ''' <summary>
    ''' Click refrescar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MbtnRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRefresh.ItemClick
        ReloadCampaignEventArgs(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Click para mostrar el Kardex de la campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnKardexCampaign_ItemClickAsync(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnKardexCampaign.ItemClick
        If Await INDBindRequestAsync() = False Then Exit Sub
        Using formulario As New FrmKardexCampaign()
            AddHandler formulario.ReloadCampaignArgs, AddressOf ReloadCampaignEventArgs
            formulario.PermissionsForm = FormOwner.PermissionsForm
            formulario.CampaignDetailId = objCampaignDetailXpo.Id
            formulario.SetTitleWindow = String.Format("   Kardex Materia Prima Campaña Nro {0}", objCampaignDetailXpo.CampaignNumber)
            formulario.CampaignStatus = objCampaignDetailXpo.CampaignStatus
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' validar control.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function INDBindRequestAsync() As Task(Of Boolean)
        Using model As New MCampaign("")
            Me.IsAsyncOperation()
            Dim result = Await model.GetCampaignDetailById(_objCampaignDetailXpo.Id, False)
            Me.IsAsyncOperation(False)

            If result IsNot Nothing AndAlso result.StateResult = False Then
                ShowMessage(EeventViewerImages.Advertencia) = result.Message
                Return False
            End If
            If result.ObjectEmbbeded.Status <> _objCampaignDetailXpo.Status Then
                ShowMessage(EeventViewerImages.Advertencia) = "La campaña tiene un estado desactualizado, Refresque el Dashboard"
                Return False
            End If
            If Not {4}.Contains(result.ObjectEmbbeded.Status) Then
                ShowMessage(EeventViewerImages.Informacion) = "La validación de lotes debe haber culminado con por lo menos una orden de traslado"
                Return False
            End If
        End Using
        Return True
    End Function

    ''' <summary>
    ''' Click para mostrar el Ajuste de kardex
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnProcessSettings_ItemClickAsync(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnProcessSettings.ItemClick
        If Await INDBindRequestAsync() = False Then Exit Sub
        Using formulario As New FrmProcessSetting()
            AddHandler formulario.ReloadCampaignArgs, AddressOf ReloadCampaignEventArgs
            formulario.PermissionsForm = FormOwner.PermissionsForm
            formulario.CampaignDetailId = objCampaignDetailXpo.Id
            formulario.CMConfigurationId = objCampaignDetailXpo.CampaignId.CMConfigurationId
            formulario.SetTitleWindow = String.Format("   Ajuste Materia Prima Campaña Nro {0}", objCampaignDetailXpo.CampaignNumber)
            formulario.CampaignStatus = objCampaignDetailXpo.CampaignStatus
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Terminar campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiEndCampaign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiEndCampaign.ItemClick
        If MessageIndigo.Show("¿Está seguro que desea terminar la campaña?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            EndCampaign()
        End If
    End Sub

    Private Sub INDBbiLineRelease_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiLineRelease.ItemClick
        Dim userId = SessionValues.Instance.UserIndigoId

        Using frm As New FrmPopupReleaseLine()
            frm.PermissionsForm = FormOwner.PermissionsForm
            frm.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.9
            frm.CMConfigurationId = objCampaignDetailXpo.CampaignId.CMConfigurationId
            frm.ProductionLineCodeName = objCampaignDetailXpo.ProductionLineId.CodeName
            frm.UnitDoseTypeCodeName = objCampaignDetailXpo.UnitDoseTypeId.CodeDescription
            frm.UnitDoseTypeMSClass = objCampaignDetailXpo.UnitDoseTypeId.MSClass
            frm.CampaignDetailId = objCampaignDetailXpo.Id
            frm.CampaignDetailStatus = objCampaignDetailXpo.CampaignStatus
            frm.StartPosition = Windows.Forms.FormStartPosition.CenterParent

            If objCampaignDetailXpo.CampaignId.CMConfiguration.IdDirector = userId Then
                frm.CurrentUserRole = UserRoleMixingStation.DirectorTecnico
            Else
                frm.CurrentUserRole = DirectCast([Enum].Parse(GetType(UserRoleMixingStation), objCampaignDetailXpo.CampaignDetailUsersXpo.FirstOrDefault(Function(m) m.UserId = userId).UserRole), UserRoleMixingStation)
            End If

            frm.AuxiliarExists = objCampaignDetailXpo.CampaignDetailUsersXpo.Any(Function(m) m.UserRole = 3)
            Dim tr As New FrmTransparent(frm, False)
            If tr.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim campaignDetailId As Integer = INDviewItems.GetFocusedRow().CampaignDetailId
                RaiseEvent BeginReloadCampaignDetailIdStatus(Me, campaignDetailId)
            End If
        End Using
    End Sub
#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento que se dispara para sacar el menú de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewItems_PopupMenuShowing(sender As Object, e As Views.Grid.PopupMenuShowingEventArgs) Handles INDviewItems.PopupMenuShowing
        IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(m) m.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)

        Dim buttonShowDetail = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ShowRequestDetail)))
        Dim buttonSendTo = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.SendTo)))
        Dim finishProduct = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ProcessfinishedProduct)))
        Dim readjustment = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.AssignReadjustment)))

        buttonShowDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        If _objCampaignDetailXpo.CampaignStatus = 1 AndAlso buttonSendTo IsNot Nothing Then
            buttonSendTo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If _objCampaignDetailXpo.CampaignStatus = 5 AndAlso finishProduct IsNot Nothing Then
            finishProduct.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If _objCampaignDetailXpo.CampaignStatus = 1 AndAlso readjustment IsNot Nothing AndAlso
            TypeOf INDviewItems.GetFocusedRow() Is ViewListCampaignDetailWithRequestsXpo AndAlso INDviewItems.GetFocusedObject(Of ViewListCampaignDetailWithRequestsXpo).RequestType = 1 _
            AndAlso Not INDviewItems.GetFocusedObject(Of ViewListCampaignDetailWithRequestsXpo).HasReadjustment Then
            readjustment.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

    End Sub

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(m) m.Visible = False)

        Dim buttonShowDetail = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ShowRequestDetail)))
        buttonShowDetail.Visible = True

        Dim count As Integer = 1
        Dim buttonSendTo = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.SendTo)))
        If _objCampaignDetailXpo.CampaignStatus = 1 AndAlso buttonSendTo IsNot Nothing Then
            count += 1
            buttonSendTo.Visible = True
        End If

        Dim finishProduct = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ProcessfinishedProduct)))
        If _objCampaignDetailXpo.CampaignStatus = 5 AndAlso finishProduct IsNot Nothing Then
            count += 1
            finishProduct.Visible = True
        End If

        Dim AssignReadjustment = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AssignReadjustment)))
        If _objCampaignDetailXpo.CampaignStatus = 1 AndAlso AssignReadjustment IsNot Nothing AndAlso
            TypeOf INDviewItems.GetFocusedRow() Is ViewListCampaignDetailWithRequestsXpo AndAlso INDviewItems.GetFocusedObject(Of ViewListCampaignDetailWithRequestsXpo).RequestType = 1 _
            AndAlso Not INDviewItems.GetFocusedObject(Of ViewListCampaignDetailWithRequestsXpo).HasReadjustment Then
            count += 1
            AssignReadjustment.Visible = True
        End If

        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

    ''' <summary>
    ''' show drop down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DdbActions_ShowDropDownControl(sender As Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs) Handles DdbActions.ShowDropDownControl
        Dim preparationStatus = Presenter.GetSumQuantityByPreparationStatus(objCampaignDetailXpo.Id)
        Dim allControls As DevExpress.XtraBars.BarButtonItem() = {
                                                                    INDBbiAdequacyPlanNPT, MbtnProcessRawMaterial, INDBbiLineRelease, INDBtnKardexCampaign, INDBbiDevolutionProcess,
                                                                    INDBtnProcessSettings, INDBbiEndCampaign, INDBbiDefectClassification, INDBbiProcessFinishedProductMassive
                                                                  }

        SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, allControls)

        Select Case objCampaignDetailXpo.CampaignStatus
            Case 1, 3, 4
                SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh)
            Case 2
                SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh, MbtnProcessRawMaterial)
            Case 5
                MbtnProcessRawMaterial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                'Si ya confirmó orden de traslado del procesar materia prima
                If objCampaignDetailXpo.Status = 4 Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh, MbtnProcessRawMaterial, INDBtnKardexCampaign, INDBbiDevolutionProcess)

                    If objCampaignDetailXpo.CampaignDetailUsersXpo.Any(Function(m) m.UserCode = SessionValues.Instance.UserIndigo) Then
                        INDBbiLineRelease.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    End If
                End If

                'Liberación de la línea OK
                If objCampaignDetailXpo.ReleaseLines.Any(Function(x) x.AdequacyItem1 = True AndAlso x.ConditioningItem1 = True) Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, INDBbiProcessFinishedProductMassive)
                End If

                'Todos los productos terminados
                If preparationStatus.FinishedQuantity = preparationStatus.RequestedQuantity Then
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, INDBbiDefectClassification, INDBtnProcessSettings)
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, INDBbiProcessFinishedProductMassive)
                End If

                'Todos los productos liberados, rechazados o anulados
                If preparationStatus.RequestedQuantity = (preparationStatus.ReleasedQuantity + preparationStatus.RejectedQuantity + preparationStatus.CancelledQuantity) Then
                    If objCampaignDetailXpo.CampaignDetailUsersXpo.Any(Function(m) m.UserCode = SessionValues.Instance.UserIndigo) Then
                        INDBbiLineRelease.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    End If

                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnProcessRawMaterial, INDBtnKardexCampaign, INDBbiDevolutionProcess,
                                            INDBbiDefectClassification, INDBtnProcessSettings, INDBbiEndCampaign)
                    SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, INDBbiProcessFinishedProductMassive)
                End If
            Case 6
                SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Always, MbtnRefresh, INDBtnKardexCampaign)
        End Select

        If (_objCampaignDetailXpo.UnitDoseTypeId.MSClass = 2) Then
            INDBbiAdequacyPlanNPT.Visibility = If(_objCampaignDetailXpo.CampaignStatus = 2, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        End If

        'Valido Solicitudes
        If Me.INDgcItems.DataSource Is Nothing Then
            SetBarItemVisibility(DevExpress.XtraBars.BarItemVisibility.Never, allControls)
        End If
    End Sub

    Private Sub INDBbiDefectClassification_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiDefectClassification.ItemClick
        Try
            If Not waitForm.IsSplashFormVisible Then waitForm.ShowWaitForm()

            Dim any = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
                .MixingStationService.GetXPOObject(Of ViewDefectClassificationToReportXpo)($"CampaignDetailId = {objCampaignDetailXpo.Id}")

            If any Is Nothing Then
                If waitForm.IsSplashFormVisible Then waitForm.CloseWaitForm()
                ShowMessage(EeventViewerImages.Advertencia) = $"No se han clasificado defectos para la campaña ({objCampaignDetailXpo.CampaignNumber})"
                Return
            End If

            Dim reportDef As New Reporter.rptDefectClassification
            AddHandler reportDef.AfterPrint, Sub()
                                                 If waitForm.IsSplashFormVisible Then waitForm.CloseWaitForm()
                                             End Sub
            ReportHelper.ExecuteReport(reportDef, Me.FormOwner, Me.FormOwner.PermissionsForm, objCampaignDetailXpo.Id)
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetImage(img As Image) As Byte()
        Return DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(img, ImageFormat.Gif)
    End Function

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim tag = sender.Tag.ToString()

        Select Case tag
            Case NameOf(eAcciones.ShowRequestDetail)
                ViewListDetail(INDviewItems)
            Case NameOf(eAcciones.SendTo)
                OpenFormSelectedCampaign()
            Case NameOf(eAcciones.ProcessfinishedProduct)
                ProcessFinishedProduct()
            Case NameOf(eAcciones.AssignReadjustment)
                AssignReadjustment(TryCast(INDviewItems.GetFocusedRow(), ViewListCampaignDetailWithRequestsXpo)?.RequestMixingStationDetailId)
        End Select
    End Sub

    Private Sub INDBbiProcessFinishedProduct_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcessFinishedProduct.ItemClick
        ProcessFinishedProduct()
    End Sub

    Private Sub INDBbiView_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiView.ItemClick
        ViewListDetail(INDviewItems)
    End Sub

    Private Sub INDBbiSendTo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiSendTo.ItemClick
        OpenFormSelectedCampaign()
    End Sub

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If Not e.SelectedControl Is INDgcItems Then Return

        Dim info As ToolTipControlInfo = Nothing
        Dim view As GridView = INDgcItems.GetViewAt(e.ControlMousePosition)

        If view Is Nothing Then Return

        Dim hi As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
        Dim row = view.GetRow(hi.RowHandle)
        Dim text As String = String.Empty

        If hi.Column IsNot Nothing AndAlso hi.Column.Name = INDcolStatusCrystal.Name Then
            Dim statusHCPRESCRA As Integer = 0
            Dim isReadjustment As Integer = 0

            If TypeOf row Is SP_ListViewItemsCampaigns_Result Then
                Dim item = DirectCast(row, SP_ListViewItemsCampaigns_Result)
                statusHCPRESCRA = item.StatusHCPRESCRA
                isReadjustment = item.IsReadjustment

            ElseIf TypeOf row Is ViewListCampaignDetailWithRequestsXpo Then
                Dim item = DirectCast(row, ViewListCampaignDetailWithRequestsXpo)
                statusHCPRESCRA = item.StatusHCPRESCRA 'StatusNameHCPRESCRA
                isReadjustment = item.IsReadjustment 'StatusNameHCPRESCRA
            End If

            text = GetStatusNameHCPrescra(statusHCPRESCRA, isReadjustment)
        Else
            text = view.GetRowCellValue(hi.RowHandle, hi.Column)
        End If

        'An object that uniquely identifies a row indicator cell
        info = New ToolTipControlInfo(row, text)
        'Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
        If Not info Is Nothing Then e.Info = info
    End Sub

    Private Function GetStatusNameHCPrescra(status As Integer, isReadjustment As Integer)
        Dim statusText = ""
        Select Case status
            Case 1
                statusText = "Iniciado"
            Case 2
                statusText = "Ciclo completado"
            Case 3
                statusText = "Tratamiento descontinuado"
            Case 4
                statusText = "Tratamiento suspendido"
            Case 5
                statusText = "Plan de manejo externo"
            Case 6
                statusText = "Medicamentos solicitados sin existencia actual en el kardex"
            Case 7
                statusText = "Tratamiento terminado por salida del paciente"
            Case Else
                statusText = ""
        End Select
        If isReadjustment = 1 And Not {3, 4, 7}.Contains(status) Then
            statusText = "Readecuación"
        End If
        Return statusText
    End Function

    Private Sub Validations()
        Dim val = (From x In CType(INDgcItems.DataSource, IEnumerable(Of SP_ListViewItemsCampaigns_Result))).ToList()
        If Not val.Any(Function(x) x.HasDefectsProduction) Then
            ShowMessage(EeventViewerImages.Advertencia) = "Importante: Realizar inspección básica a los productos terminados"
        End If

    End Sub

    ''' <summary>
    ''' Función para enviar por paquetes los cuales se han menores 
    ''' de 50.000 y si son mayores se envia en un paquete solo.
    ''' </summary>
    Private Function SplitPackages(items As IEnumerable(Of SP_ListViewItemsCampaigns_Result)) As List(Of List(Of Integer))
        Dim paquetes As New List(Of List(Of Integer))()
        Dim paqueteActual As New List(Of Integer)()
        Dim totalRequestQuantity As Integer = 0
        Dim count As Integer = 0

        For Each item As SP_ListViewItemsCampaigns_Result In items
            ' Si el RequestQuantity excede 50,000, agregar un nuevo paquete
            If item.RequestQuantity > 50000 Then
                If paqueteActual.Count > 0 Then
                    paquetes.Add(paqueteActual)
                End If
                paqueteActual = New List(Of Integer)()
                paqueteActual.Add(Convert.ToInt32(item.code))
                paquetes.Add(paqueteActual)
                paqueteActual = New List(Of Integer)()
                count = 0
            Else
                ' Si el RequestQuantity es menor o igual a 10,000, agregar al paquete actual
                Dim suma = totalRequestQuantity + item.RequestQuantity
                If suma < 50000 Then
                    paqueteActual.Add(Convert.ToInt32(item.code)) '2
                    totalRequestQuantity += item.RequestQuantity
                    ' Si ya se han agregado 5 solicitudes al paquete, agregarlo a la lista de paquetes y crear un nuevo paquete
                Else
                    paquetes.Add(paqueteActual)
                    paqueteActual = New List(Of Integer)() '1
                    paqueteActual.Add(Convert.ToInt32(item.code))
                    totalRequestQuantity = item.RequestQuantity
                End If
            End If
        Next

        ' Agregar el último paquete si no está vacío
        If paqueteActual.Count > 0 Then
            paquetes.Add(paqueteActual)
        End If

        Return paquetes
    End Function

    Private Async Sub INDBbiProcessFinishedProductMassive_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcessFinishedProductMassive.ItemClick
        Dim ListErrors As New List(Of Tuple(Of String, Integer))

        If MessageIndigo.Show("¿Esta seguro de cambiar a estado producto terminado todos los items de todas las solicitudes?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Try
            IsAsyncOperation()
            Using model As New MCampaign(Tag)

                Dim listaItems As IEnumerable(Of SP_ListViewItemsCampaigns_Result) = CType(INDgcItems.DataSource, IEnumerable(Of SP_ListViewItemsCampaigns_Result))

                Dim paquetes As List(Of List(Of Integer)) = SplitPackages(listaItems)
                ' Procesar los paquetes uno por uno
                For Each paquete As List(Of Integer) In paquetes
                    Dim result = Await model.ProcessFinishedProductAsync(paquete)
                    If result.ObjectEmbbeded.Count > 0 Then
                        ListErrors.AddRange(result.ObjectEmbbeded)
                    End If
                Next

                If ListErrors.Count > 0 Then
                    Using formulario As New FrmListErrors(ListErrors)
                        formulario.Title = "Resultado de mensajes"
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                    IsAsyncOperation(False)
                End If

                Dim Validar = ListErrors.FirstOrDefault(Function(x) x.Item2 = 1)
                If Validar IsNot Nothing Then
                    SetDatasourceAsync(_campaignDetailId)
                    Validations()
                End If
            End Using
        Catch ex As Exception
            IsAsyncOperation(False)
            Throw ex
        End Try
    End Sub

    Private Sub INDBbiAdequacyPlanNPT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiAdequacyPlanNPT.ItemClick
        Dim frm As New FrmPopupNPTProductionPlan()
        frm.CampaignDetailId = CampaignDetailId
        frm.AdequacyPlanNPT = True
        frm.Text = "Plan de adecuación NPT"
        Dim transparent As New FrmTransparent(frm, False)
        If transparent.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            RaiseEvent RequiereReloadCampaignList(Me, New RequiereReloadCampaignListEventArgs({CampaignDetailId}.ToList()))
            RaiseEvent BeginReloadCampaignDetailIdStatus(Me, CampaignDetailId)
        End If
    End Sub

#End Region

#End Region

End Class

Public Class RequiereReloadCampaignListEventArgs
    Inherits EventArgs

#Region "Members"

    Public Property ListCampaignDetailId As List(Of Integer)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal _listCampaignDetailId As List(Of Integer))
        ListCampaignDetailId = _listCampaignDetailId
    End Sub

#End Region

End Class
