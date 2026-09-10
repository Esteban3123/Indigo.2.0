Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports Presentation.Controls
Imports Presentation.InteropCost.MVP
Imports System.Text
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraLayout

Public Class FrmSelectPeriod

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmSelectPeriod"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrDateNavigatorHorizontal()
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        ControlPanelPeriod.Controls.Add(_tmpCurrentPeriod)
    End Sub
#End Region

#Region "Properties and Variables"

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"

    ''' <summary>
    ''' Evento para agregar los datos
    ''' </summary>
    Public Event AddImportData(ByVal dataImpor As List(Of DistributionManpower))

    ''' <summary>
    ''' Evento para agregar datos de activos fijos
    ''' </summary>
    Public Event AddImportDataFixedAsset(ByVal dataImpor As List(Of DistributionFixedAsset))

    ''' <summary>
    ''' Evento para agregar datos de destribución intermedia
    ''' </summary>
    Public Event AddImportDataIntermediate(ByVal dataImpor As List(Of DistributionIntermediate))

    ''' <summary>
    ''' Evento para agregar datos de destribución Secundaria
    ''' </summary>
    Public Event AddImportDataSecondary(ByVal dataImpor As List(Of DistributionSecondary))

    ''' <summary>
    ''' control de periodo
    ''' </summary>
    Private _tmpCurrentPeriod As CtrDateNavigatorHorizontal

    ''' <summary>
    ''' Tipo de fuente de datos a llenar en la rejilla
    ''' </summary>
    Property DatasourceType As eDatasourceType

    ''' <summary>
    ''' Panel de control donde se añade el control de fecha
    ''' </summary>
    Property ControlPanelPeriod As PanelControl
        Get
            Return INDpcDateControl
        End Get
        Set(value As PanelControl)
            INDpcDateControl = value
        End Set
    End Property

    WriteOnly Property LisPeriods As List(Of String)
        Set(value As List(Of String))
            SetListEnablePeriods = value
        End Set
    End Property

    ''' <summary>
    ''' Mes del periodo anterior al actual
    ''' </summary>
    Property PreviusMonth As Integer

    ''' <summary>
    ''' Año del periodo anterior al actual
    ''' </summary>
    Property PreviusYear As Integer

    ''' <summary>
    ''' estado del check total
    ''' </summary>
    Private _stateCheck As Boolean = True

    ''' <summary>
    ''' Establece los periodos activos del control de periodo
    ''' </summary>
    Private WriteOnly Property SetListEnablePeriods As List(Of String)
        Set(value As List(Of String))
            _tmpCurrentPeriod.ListEnablePeriods = value
            _tmpCurrentPeriod.SetMonth = PreviusMonth
            _tmpCurrentPeriod.SetYear = PreviusYear
            _tmpCurrentPeriod.SetDateInformation()
            AddHandler _tmpCurrentPeriod.OnChangeDate, AddressOf OnChangeDate
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el listado de datos a importar de distribucion de mano de obra
    ''' </summary>
    Public Property ImportDataDatasourceDistributionLabor As List(Of DistributionManpower)
        Get
            Return CType(INDgcImportData.DataSource, List(Of DistributionManpower))
        End Get
        Set(value As List(Of DistributionManpower))
            INDgcImportData.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el listado de datos a importar de distribución de activos fijos
    ''' </summary>
    Public Property ImportDataDatasourceDistributionFixedAsset As List(Of DistributionFixedAsset)
        Get
            Return CType(INDgcImportData.DataSource, List(Of DistributionFixedAsset))
        End Get
        Set(value As List(Of DistributionFixedAsset))
            INDgcImportData.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el listado de datos a importar de distribución Intermedia
    ''' </summary>
    Public Property ImportDataDatasourceDistributionIntermediate As List(Of DistributionIntermediate)
        Get
            Return CType(INDgcImportData.DataSource, List(Of DistributionIntermediate))
        End Get
        Set(value As List(Of DistributionIntermediate))
            INDgcImportData.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el listado de datos a importar de distribución Secundaria
    ''' </summary>
    Public Property ImportDataDatasourceDistributionSecondary As List(Of DistributionSecondary)
        Get
            Return CType(INDgcImportData.DataSource, List(Of DistributionSecondary))
        End Get
        Set(value As List(Of DistributionSecondary))
            INDgcImportData.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _tmpCurrentPeriod = Nothing
        DatasourceType = Nothing
        PreviusMonth = Nothing
        PreviusYear = Nothing
        _stateCheck = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmSelectPeriod control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSelectPeriod_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SearchDisponiblePeriods()
        INDcolEmployee.Visible = False
        INDcolFixedAsset.Visible = False
        INDcolProductionCenter.Visible = False
        ColEmployeeGroup.Visible = False
        Select Case DatasourceType
            Case eDatasourceType.DistributionLabor
                INDcolEmployee.Visible = True
                ColEmployeeGroup.Visible = True
            Case eDatasourceType.DistributionFixedAsset
                INDcolFixedAsset.Visible = True
            Case eDatasourceType.DistributionIntermediate
                INDcolProductionCenter.Visible = True
            Case eDatasourceType.DistributionSecondary
                INDcolProductionCenter.Visible = True
        End Select
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbCheckAll control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbCheckAll_Click(sender As Object, e As EventArgs)
        Select Case DatasourceType
            Case eDatasourceType.DistributionLabor
                For Each item As DistributionManpower In ImportDataDatasourceDistributionLabor
                    item.Checked = _stateCheck
                Next
            Case eDatasourceType.DistributionFixedAsset
                For Each item As DistributionFixedAsset In ImportDataDatasourceDistributionFixedAsset
                    item.Checked = _stateCheck
                Next
            Case eDatasourceType.DistributionIntermediate
                For Each item As DistributionIntermediate In ImportDataDatasourceDistributionIntermediate
                    item.Checked = _stateCheck
                Next
            Case eDatasourceType.DistributionSecondary
                For Each item As DistributionSecondary In ImportDataDatasourceDistributionSecondary
                    item.Checked = _stateCheck
                Next
        End Select
        INDgcImportData.RefreshDataSource()
        _stateCheck = Not _stateCheck
    End Sub

    ''' <summary>
    ''' Handles the DoubleClick event of the INDgvImportData control.
    ''' </summary>
    Private Sub INDgvImportData_DoubleClick(sender As Object, e As EventArgs) Handles INDgvImportData.DoubleClick
        Dim pMouse As System.Drawing.Point = INDgcImportData.PointToClient(Control.MousePosition)
        Dim hit = INDgvImportData.CalcHitInfo(pMouse)
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolCheck") Then
            'Dim itemCollection = INDgvImportData.GetFocusedRow()
            Select Case DatasourceType
                Case eDatasourceType.DistributionLabor
                    For Each item As DistributionManpower In ImportDataDatasourceDistributionLabor
                        item.Checked = _stateCheck
                    Next
                Case eDatasourceType.DistributionFixedAsset
                    For Each item As DistributionFixedAsset In ImportDataDatasourceDistributionFixedAsset
                        item.Checked = _stateCheck
                    Next
                Case eDatasourceType.DistributionIntermediate
                    For Each item As DistributionIntermediate In ImportDataDatasourceDistributionIntermediate
                        item.Checked = _stateCheck
                    Next
                Case eDatasourceType.DistributionSecondary
                    For Each item As DistributionSecondary In ImportDataDatasourceDistributionSecondary
                        item.Checked = _stateCheck
                    Next
            End Select
            INDgcImportData.RefreshDataSource()
            _stateCheck = Not _stateCheck
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        Select Case DatasourceType
            Case eDatasourceType.DistributionLabor
                If ImportDataDatasourceDistributionLabor IsNot Nothing AndAlso ImportDataDatasourceDistributionLabor.Where(Function(x) x.Checked = True).ToList().Count > 0 Then
                    If MessageIndigo.Show(ResourceManager.GetString("ImportDataQuestion", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        RaiseEvent AddImportData(ImportDataDatasourceDistributionLabor.Where(Function(x) x.Checked = True).ToList())
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoSelectedDataImport", MODULE_NAME)
                End If
            Case eDatasourceType.DistributionFixedAsset
                If ImportDataDatasourceDistributionFixedAsset IsNot Nothing AndAlso ImportDataDatasourceDistributionFixedAsset.Where(Function(x) x.Checked = True).ToList().Count > 0 Then
                    If MessageIndigo.Show(ResourceManager.GetString("ImportDataQuestion", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        RaiseEvent AddImportDataFixedAsset(ImportDataDatasourceDistributionFixedAsset.Where(Function(x) x.Checked = True).ToList())
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoSelectedDataImport", MODULE_NAME)
                End If
            Case eDatasourceType.DistributionIntermediate
                If ImportDataDatasourceDistributionIntermediate IsNot Nothing AndAlso ImportDataDatasourceDistributionIntermediate.Where(Function(x) x.Checked = True).ToList().Count > 0 Then
                    If MessageIndigo.Show(ResourceManager.GetString("ImportDataQuestion", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        RaiseEvent AddImportDataIntermediate(ImportDataDatasourceDistributionIntermediate.Where(Function(x) x.Checked = True).ToList())
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoSelectedDataImport", MODULE_NAME)
                End If
            Case eDatasourceType.DistributionSecondary
                If ImportDataDatasourceDistributionSecondary IsNot Nothing AndAlso ImportDataDatasourceDistributionSecondary.Where(Function(x) x.Checked = True).ToList().Count > 0 Then
                    If MessageIndigo.Show(ResourceManager.GetString("ImportDataQuestion", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        RaiseEvent AddImportDataSecondary(ImportDataDatasourceDistributionSecondary.Where(Function(x) x.Checked = True).ToList())
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoSelectedDataImport", MODULE_NAME)
                End If
        End Select
    End Sub
#End Region

#Region "Methods and Functions"
    ''' <summary>
    ''' Searches the disponible periods.
    ''' </summary>
    Private Async Sub SearchDisponiblePeriods()
        Dim _listPeriods As List(Of String) = Nothing
        Select Case DatasourceType
            Case eDatasourceType.DistributionLabor
                Using Model As New MDistributionManpower(Me.Tag)
                    _listPeriods = Await Model.ListPeriodWithDataByMaximumPeriod(PreviusYear, PreviusMonth)
                End Using
            Case eDatasourceType.DistributionFixedAsset
                Using Model As New MDistributionFixedAsset(Me.Tag)
                    _listPeriods = Await Model.ListPeriodWithDataByMaximumPeriod(PreviusYear, PreviusMonth)
                End Using
            Case eDatasourceType.DistributionIntermediate
                Using Model As New MDistributionIntermediate(Me.Tag)
                    _listPeriods = Await Model.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(PreviusYear, PreviusMonth)
                End Using
            Case eDatasourceType.DistributionSecondary
                Using Model As New MDistributionSecondary(Me.Tag)
                    _listPeriods = Await Model.ListPeriodWithDataByMaximumPeriodDistributionSecondary(PreviusYear, PreviusMonth)
                End Using
        End Select
        SetListEnablePeriods = _listPeriods
    End Sub

    Private _isAsyncLoader As Boolean

    Private Sub AsyncLoader(Optional state As Boolean = True)
        If _isAsyncLoader <> state Then
            _isAsyncLoader = state
            LciMarquee.ContentVisible = state
            LcRoot.Enabled = Not state
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    Private _isLoadingData As Boolean
    ''' <summary>
    ''' Called when [change date].
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub OnChangeDate(sender As Object, e As EventArgs)
        If _isLoadingData Then
            Exit Sub
        End If
        _isLoadingData = True
        AsyncLoader()
        If _tmpCurrentPeriod.GetYear <> 0 AndAlso _tmpCurrentPeriod.GetMonth <> 0 Then
            Select Case DatasourceType
                Case eDatasourceType.DistributionLabor
                    Using model As New MDistributionManpower(Me.Tag)
                        ImportDataDatasourceDistributionLabor = Await model.ListDistributionManpowerByYearMonth(_tmpCurrentPeriod.GetYear, _tmpCurrentPeriod.GetMonth)
                    End Using
                Case eDatasourceType.DistributionFixedAsset
                    Using model As New MDistributionFixedAsset(Me.Tag)
                        ImportDataDatasourceDistributionFixedAsset = Await model.ListDistributionFixedAssetByYearMonth(_tmpCurrentPeriod.GetYear, _tmpCurrentPeriod.GetMonth)
                    End Using
                Case eDatasourceType.DistributionIntermediate
                    Using model As New MDistributionIntermediate(Me.Tag)
                        ImportDataDatasourceDistributionIntermediate = Await model.ListDistributionIntermediateByYearMonth(_tmpCurrentPeriod.GetYear, _tmpCurrentPeriod.GetMonth)
                    End Using
                Case eDatasourceType.DistributionSecondary
                    Using model As New MDistributionSecondary(Me.Tag)
                        ImportDataDatasourceDistributionSecondary = Await model.ListDistributionSecondaryByYearMonth(_tmpCurrentPeriod.GetYear, _tmpCurrentPeriod.GetMonth)
                    End Using
            End Select
        End If
        AsyncLoader(False)
        _isLoadingData = False
    End Sub
#End Region

#Region "Enums"
    Public Enum eDatasourceType
        DistributionLabor
        DistributionFixedAsset
        DistributionIntermediate
        DistributionSecondary
    End Enum
#End Region

End Class