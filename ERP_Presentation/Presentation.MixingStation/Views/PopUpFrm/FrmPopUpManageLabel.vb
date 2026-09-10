Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

Public Class FrmPopUpManageLabel

    ''' <summary>
    ''' Evento de confirmación de etiquetado con los datos necesarios
    ''' </summary>
    Public Class ConfirmLabelEventArgs
        Inherits EventArgs

        Public Property CampaignDetailId As Integer
        Public Property LabelData As List(Of MixingLabelModel)

    End Class

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(campaignDetailId As Integer, unitDoseTypeClass As Integer)
        InitializeComponent()
        _campaignDetailId = campaignDetailId
        _unitDoseTypeClass = unitDoseTypeClass
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Evento de confirmación de etiquetado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ConfirmLabel(sender As Object, e As ConfirmLabelEventArgs)

    ''' <summary>
    ''' Id de la campaña
    ''' </summary>
    Private _campaignDetailId As Integer

    ''' <summary>
    ''' Clase que permite identificar el tipo de dosis de la campaña
    ''' </summary>
    Private _unitDoseTypeClass As Integer

    ''' <summary>
    ''' Items de la rejilla
    ''' </summary>
    ''' <returns></returns>
    Private Property ItemsDatasource As List(Of ViewListCampaignDetailWithRequestsXpo)
        Get
            Return INDGcItems.DataSource
        End Get
        Set(value As List(Of ViewListCampaignDetailWithRequestsXpo))
            INDGcItems.DataSource = value
            INDGcItems.RefreshDataSource()
        End Set
    End Property
#End Region

#Region "Functions"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub InitForm()
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False

        LoadLabelTypes()
        ShowItemsDatasource()
    End Sub

    ''' <summary>
    ''' Carga los tipos de etiqueta
    ''' </summary>
    Private Sub LoadLabelTypes()
        INDRptLabelTypes.DataSource = {
            New Tuple(Of Byte, String)(1, "Bolsa"),
            New Tuple(Of Byte, String)(2, "Nutrición parenteral"),
            New Tuple(Of Byte, String)(3, "Jeringa 10 CC"),
            New Tuple(Of Byte, String)(4, "Tabletería 4x4 cm"),
            New Tuple(Of Byte, String)(5, "Magistral")
        }.ToList()
    End Sub

    ''' <summary>
    ''' Carga los datos en la rejilla
    ''' </summary>
    Private Sub ShowItemsDatasource()
        ToolBars.Enabled = False
        INDGvItems.ShowLoadingPanel()
        Task.Factory.StartNew(AddressOf LoadDatasource)
    End Sub

    ''' <summary>
    ''' Async loader
    ''' </summary>
    ''' <param name="State"></param>
    Public Overrides Sub AsyncLoader(State As Boolean)
        MyBase.AsyncLoader(State)
        INDGcItems.Enabled = Not State
    End Sub
#End Region

#Region "Privates"
    ''' <summary>
    ''' Consulta los datos a mostrar
    ''' </summary>
    Private Sub LoadDatasource()
        Dim data = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
            .MixingStationService _
            .GetCollection(Of ViewListCampaignDetailWithRequestsXpo)(Nothing, $"CampaignDetailId = {_campaignDetailId} And ManageQuantity > 0")

        If _unitDoseTypeClass = EUnitDoseTypeClass.Repackaging OrElse _unitDoseTypeClass = EUnitDoseTypeClass.Refilling Then
            For Each item In data
                item.LabelType = 4
            Next
        End If

        Me.SafeInvoke(Sub()
                          ItemsDatasource = data
                          INDGvItems.HideLoadingPanel()
                          ToolBars.Enabled = True
                      End Sub)
    End Sub

    ''' <summary>
    ''' Confirma el etiquetado
    ''' </summary>
    Private Sub Confirm()
        If MessageIndigo.Show("¿Está seguro que desea confirmar el etiquetado?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If ItemsDatasource.Exists(Function(x) x.LabelType Is Nothing) Then
            MessageIndigo.Show("Existen solicitudes que no tienen asignado un tipo de etiqueta", MessageType.Errores, Me.Text)
            Exit Sub
        End If

        RaiseEvent ConfirmLabel(Me, New ConfirmLabelEventArgs With {
                               .CampaignDetailId = _campaignDetailId,
                               .LabelData = ItemsDatasource.Select(Function(m) New MixingLabelModel() With {
                                    .RequestMixingStationDetailId = m.RequestMixingStationDetailId,
                                    .LabelType = m.LabelType
                                }).ToList()
        })
    End Sub
#End Region

#Region "BarButtonEvents"
    ''' <summary>
    ''' Click en confirmar
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirm()
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpManageLabel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitForm()
    End Sub

    ''' <summary>
    ''' Cierra el formulario al dar escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpManageLabel_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Query Popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceMoreInfo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceMoreInfo.QueryPopUp
        Dim selectedItem = INDGvItems.GetFocusedObject(Of ViewListCampaignDetailWithRequestsXpo)()

        If selectedItem IsNot Nothing Then
            CtrMoreInfoElaborationParameter1.SetData(selectedItem.RequestMixingStationDetailId)
        End If
    End Sub

    ''' <summary>
    ''' Open change labelType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiManageLabel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiManageLabel.ItemClick
        Using frm As New PopUpChangeLabel()
            frm.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            AddHandler frm.AcceptLabelType, AddressOf AcceptLabelType
            Dim trns As New FrmTransparent(frm, False)
            trns.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Accept label type
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AcceptLabelType(sender As Object, e As PopUpChangeLabel.LabelTypeEventArgs)
        Dim listItems = INDGvItems.GetSelectedRows().Select(Of ViewListCampaignDetailWithRequestsXpo)(Function(m) INDGvItems.GetRow(m)).ToList()

        If listItems IsNot Nothing AndAlso listItems.Any() Then
            listItems.ForEach(Sub(m)
                                  m.LabelType = e.LabelType
                              End Sub)
            INDGcItems.RefreshDataSource()
            CType(sender, PopUpChangeLabel).Close()
        End If
    End Sub

    ''' <summary>
    ''' Popup menu showing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvItems_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvItems.PopupMenuShowing
        Dim View = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        PopupMenu1.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub
#End Region

End Class