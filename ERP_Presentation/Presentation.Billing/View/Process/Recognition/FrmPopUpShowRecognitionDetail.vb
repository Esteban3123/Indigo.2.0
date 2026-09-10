Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Billing.MVP
''' <summary>
''' 
''' </summary>
''' <seealso cref="System.Windows.Forms.Form" />
Public Class FrmPopUpShowRecognitionDetail

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmPopUpShowRecognitionDetail"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        IndigoGridControl1.SetHideNoRecords(INDGcRecognitionDetails, True)
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Sets the title.
    ''' </summary>
    ''' <value>
    ''' The title.
    ''' </value>
    Public WriteOnly Property Title As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property
    ''' <summary>
    ''' The care group identifier
    ''' </summary>
    Private _careGroupId As Integer
    ''' <summary>
    ''' Gets or sets the care group identifier.
    ''' </summary>
    ''' <value>
    ''' The care group identifier.
    ''' </value>
    Public Property CareGroupId As Integer
        Get
            Return _careGroupId
        End Get
        Set(value As Integer)
            _careGroupId = value
        End Set
    End Property

    'Public Property DateRange As Tuple(Of Date?, Date?)

    ''' <summary>
    ''' Gets or sets the operative unit identifier.
    ''' </summary>
    ''' <value>
    ''' The operative unit identifier.
    ''' </value>
    Public Property OperativeUnitId As Integer
    ''' <summary>
    ''' Gets a value indicating whether this instance is procesar.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if this instance is procesar; otherwise, <c>false</c>.
    ''' </value>
    Public Property IsProcesar As Boolean
#End Region

#Region "Handlers"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _careGroupId = Nothing
        OperativeUnitId = Nothing
        IsProcesar = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmPopUpShowRecognitionDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpShowRecognitionDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchData()
    End Sub

    ''' <summary>
    ''' Handles the PopupMenuShowing event of the INDGvRecognitionDetails control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDGvRecognitionDetails_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvRecognitionDetails.PopupMenuShowing
        If Not e.HitInfo.InFilterPanel AndAlso Not e.HitInfo.InGroupPanel AndAlso e.HitInfo.InRow Then
            'If Not e.HitInfo.InFilterPanel AndAlso Not e.HitInfo.InGroupColumn AndAlso Not e.HitInfo.InGroupPanel AndAlso Not e.HitInfo.InGroupRow AndAlso e.HitInfo.InRow AndAlso e.HitInfo.InDataRow Then
            Dim hitInfo As GridHitInfo = INDGvRecognitionDetails.CalcHitInfo(e.Point)
            'If hitInfo.InRowCell Then
            INDGvRecognitionDetails.FocusedRowHandle = hitInfo.RowHandle
            PopupMenuActions.ShowPopup(INDGvRecognitionDetails.GridControl.PointToScreen(e.Point))
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the INDBbiExpand control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub INDBbiExpand_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiExpand.ItemClick
        INDGvRecognitionDetails.ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the INDBbiCollapse control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub INDBbiCollapse_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCollapse.ItemClick
        INDGvRecognitionDetails.CollapseAllGroups()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmPopUpShowRecognitionDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpShowRecognitionDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Searches the data.
    ''' </summary>
    Private Sub searchData()
        INDGvRecognitionDetails.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Using model As New MRecognition(Me.Tag)
                                      Dim dts = Nothing
                                      If Me.IsProcesar Then
                                          dts = model.GetRecognitionEntranceDetailData(_careGroupId, OperativeUnitId)
                                      Else
                                          dts = model.ListReverseRecognitionDetail(_careGroupId)
                                      End If
                                      INDGcRecognitionDetails.BeginInvoke(Sub()
                                                                              INDGcRecognitionDetails.DataSource = dts
                                                                              INDGvRecognitionDetails.HideLoadingPanel()
                                                                          End Sub)
                                  End Using
                              End Sub)
    End Sub
#End Region

End Class