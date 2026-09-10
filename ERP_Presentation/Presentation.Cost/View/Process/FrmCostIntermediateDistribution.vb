'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 15-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Columns
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Cost.MVP

#End Region

Public Class FrmCostIntermediateDistribution
    Implements ICostIntermediateDistribution

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrDateNavigatorCost()
        _tmpCurrentPeriod.ResizeToBarMenu()
        _tmpCurrentPeriod.WithEvent = False
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
        _tmpCurrentPeriod.ResizeToBarMenu()
        model = New MCostDistributionIntermediate(Me.Tag)
        _presenter = New PCostDistributionIntermediate(Me)
    End Sub
#End Region

#Region "Properties and variables"

#Region "Properties Entity"
    ''' <summary>
    ''' Código de la distribución intermedia
    ''' </summary>
    Public Property Code As String Implements ICostIntermediateDistribution.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción de la distribución intermedia
    ''' </summary>
    Public Property Description As String Implements ICostIntermediateDistribution.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución intermedia
    ''' </summary>
    Public Property Month As Integer Implements ICostIntermediateDistribution.Month
        Get
            Return _tmpCurrentPeriod.GetMonth
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.SetMonth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As CostSecuence Implements ICostIntermediateDistribution.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As CostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.CostSecuenceDetail In Me._sequence.CostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Centro de produccion
    ''' </summary>
    Public Property ProductionCenterId As Integer Implements ICostIntermediateDistribution.ProductionCenterId
        Get
            Return CType(INDsleProductionCenterHeader.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleProductionCenterHeader.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As CostSetting Implements ICostIntermediateDistribution.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As CostSetting)
            _tmpCurrentPeriod.SetMonth = value.Month
            _tmpCurrentPeriod.SetYear = value.Year
            _tmpCurrentPeriod.SetDateInformation()
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' Estado de la distribución intermedia
    ''' </summary>
    Public Property Status As Boolean Implements ICostIntermediateDistribution.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Año de la distribución intermedia
    ''' </summary>
    Public Property Year As Integer Implements ICostIntermediateDistribution.Year
        Get
            Return _tmpCurrentPeriod.GetYear
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.SetYear = value
        End Set
    End Property
#End Region

#Region "Datasource Entity"
    ''' <summary>
    ''' Gets or sets the production center data source.
    ''' </summary>
    Public Property ProductionCenterDataSource As XPInstantFeedbackSource Implements ICostIntermediateDistribution.ProductionCenterDataSource
        Get
            Return CType(INDsleProductionCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProductionCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the production center data source rerpository.
    ''' </summary>
    Public Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource Implements ICostIntermediateDistribution.ProductionCenterDataSourceRerpository
        Get
            Return CType(INDrpsleProductionCenter.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleProductionCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the production center data source header.
    ''' </summary>
    Public Property ProductionCenterDataSourceHeader As XPInstantFeedbackSource Implements ICostIntermediateDistribution.ProductionCenterDataSourceHeader
        Get
            Return CType(INDsleProductionCenterHeader.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProductionCenterHeader.Properties.DataSource = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' The _TMP current period
    ''' </summary>
    Private _tmpCurrentPeriod As CtrDateNavigatorCost

    ''' <summary>
    ''' The _settings cost
    ''' </summary>
    Private _settingsCost As CostSetting

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostIntermediateDistribution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICostIntermediateDistribution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.CostSecuence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    Private model As MCostDistributionIntermediate

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PCostDistributionIntermediate

    ' ''' <summary>
    ' ''' entidad de distribucion intermedia
    ' ''' </summary>
    Private _distriutionIntermediate As CostDistributionIntermediate

    ''' <summary>
    ''' Listado de detalle de distribución de activos fijos
    ''' </summary>
    Property ListDistributionIntermediateDetail As List(Of CostDistributionIntermediateDetail)
        Get
            Return CType(INDgcDetail.DataSource, List(Of CostDistributionIntermediateDetail))
        End Get
        Set(value As List(Of CostDistributionIntermediateDetail))
            INDgcDetail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordCost

    Public WriteOnly Property Mensaje(status As eStatusResult) As String
        Set(value As String)
            If status = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf status = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf status = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostIntermediateDistribution.ActionsOnControls
        Set(value As Boolean)

            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleProductionCenterHeader.Enabled = value
            'INDpceAddDetail.Enabled = value
            'DdbActions.Enabled = value
            INDgcDetail.Enabled = value
            BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()

            If value Then
                INDtxtDescription.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property
#End Region

#Region "Events"
#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _tmpCurrentPeriod = Nothing
        _settingsCost = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        model = Nothing
        _presenter = Nothing
        _distriutionIntermediate = Nothing
        _record = Nothing
    End Sub


    ''' <summary>
    ''' Handles the Load event of the FrmIntermediateDistribution control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmIntermediateDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        'IndigoGridControl1.RefreshGrid(INDgcDetail)
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, _listActions)
        For Each col As GridColumn In INDgvDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next

        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        _presenter.InitializeProductionCenter()
        SetCurrencyFormat(_presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation)
        LoadStatus()
        Deshacer()
    End Sub

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
        changeNumericFormatByCurrency(numberFormat, INDpccAddProductionCenter.Controls)
        ''ColDepreciationValue = Window.Utils.FormatGrid(ColDepreciationValue, _currencyAbbreviation)
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmIntermediateDistribution control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmIntermediateDistribution_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Shown"
    Private Sub FrmIntermediateDistribution_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbteCode.Text) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(INDbteCode.Text) Then
                    Me.NewDistributionIntermediate()
                Else
                    LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceAddDetail.ShowPopup()
        End If
    End Sub
#End Region

#Region "EditvalueChanging"
    Private Sub INDrpspnValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpspnValue.EditValueChanging
        If Not String.IsNullOrEmpty(e.NewValue) Then
            If Not ValidateProportionValue(CDec(e.NewValue.ToString().Replace(".", ",")), e.OldValue) Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub INDrpsleProductionCenter_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpsleProductionCenter.EditValueChanging
        If Not ValidateProductionCenterInList(e.NewValue) Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDsleProductionCenterHeader_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProductionCenterHeader.EditValueChanged
        INDpceAddDetail.Enabled = INDsleProductionCenterHeader.EditValue IsNot Nothing
        DdbActions.Enabled = INDsleProductionCenterHeader.EditValue IsNot Nothing
    End Sub
#End Region

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDirectCostDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDetail.DataSourceChanged
        INDsleProductionCenterHeader.Properties.ReadOnly = ListDistributionIntermediateDetail IsNot Nothing AndAlso ListDistributionIntermediateDetail.Count > 0
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductionCenterHeader control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenterHeader_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenterHeader.ButtonClick, INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostProductionCenter
                OpenFormLocal(formulario)
                _presenter.InitializeProductionCenter()
            End Using
        End If
    End Sub
#End Region

#Region "ItemClick"
    Private Async Sub MBtnAddProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnAddProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using Model As New MCostProductionCenter(Me.Tag)
                Dim listProductionCenter As List(Of CostProductionCenter) = Await Model.ListProductionCenter()
                If listProductionCenter IsNot Nothing AndAlso listProductionCenter.Count > 0 Then
                    If ListDistributionIntermediateDetail IsNot Nothing AndAlso ListDistributionIntermediateDetail.Count > 0 Then
                        'For Each item As DistributionIntermediateDetail In ListDistributionIntermediateDetail
                        '    listProductionCenter.Remove(listProductionCenter.Where(Function(x) x.Id = item.ProductionCenterId OrElse x.Id = CInt(INDsleProductionCenterHeader.EditValue)).FirstOrDefault())
                        'Next
                        listProductionCenter.RemoveAll(Function(x) ListDistributionIntermediateDetail.Select(Function(o) o.ProductionCenterId).ToList().Contains(x.Id) OrElse x.Id = CInt(INDsleProductionCenterHeader.EditValue))
                    Else
                        listProductionCenter.RemoveAll(Function(x) x.Id = CInt(INDsleProductionCenterHeader.EditValue))
                    End If
                    For Each item As CostProductionCenter In listProductionCenter
                        Dim _distribFixedAssetDetail As New CostDistributionIntermediateDetail()
                        With _distribFixedAssetDetail
                            .ProductionCenterId = item.Id
                            .Proportion = 0
                        End With
                        _distriutionIntermediate.CostDistributionIntermediateDetail.Add(_distribFixedAssetDetail)
                    Next
                    ListDistributionIntermediateDetail = _distriutionIntermediate.CostDistributionIntermediateDetail.ToList()
                    INDgcDetail.RefreshDataSource()
                End If
            End Using
        End If
    End Sub

    Private Sub MBtnRemoveProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnRemoveProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _distriutionIntermediate.CostDistributionIntermediateDetail IsNot Nothing AndAlso _distriutionIntermediate.CostDistributionIntermediateDetail.Count > 0 Then
                While _distriutionIntermediate.CostDistributionIntermediateDetail.Count > 0
                    _distriutionIntermediate.CostDistributionIntermediateDetail(_distriutionIntermediate.CostDistributionIntermediateDetail.Count - 1).MarkAsDeleted()
                End While
                ListDistributionIntermediateDetail = _distriutionIntermediate.CostDistributionIntermediateDetail.ToList()
                INDgcDetail.RefreshDataSource()
            End If
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateControlsProductionCenter() AndAlso ValidateProportionValue(CType(INDtxtMaximumAmount.EditValue, Decimal)) Then
            AddDistributionIntermediateDetail()
        End If
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _distribIntermediateDetail As CostDistributionIntermediateDetail = CType(INDgvDetail.GetFocusedRow(), CostDistributionIntermediateDetail)
            _distribIntermediateDetail.MarkAsDeleted()
            If _distriutionIntermediate.ChangeTracker.State <> ObjectState.Added Then
                _distriutionIntermediate.MarkAsModified()
            End If
            ListDistributionIntermediateDetail = _distriutionIntermediate.CostDistributionIntermediateDetail.ToList()
            INDgcDetail.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleFixetAsset control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionCenterHeader.QueryPopUp
        'If Not _stateOpenPopUpProductionCenter Then
        '    _presenter.InitializeProductionCenter()
        '    _stateOpenPopUpProductionCenter = True
        'End If
    End Sub
#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._distriutionIntermediate IsNot Nothing AndAlso Me._distriutionIntermediate.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity =  String.Empty
    End Sub
#End Region

#Region "Closed"
    ''' <summary>
    ''' Handles the Closed event of the INDpceAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddDetail_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceAddDetail.Closed
        CleanControlsDetail()
    End Sub
#End Region
#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' News the distribution intermediate.
    ''' </summary>
    Private Async Sub NewDistributionIntermediate()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        _presenter.LoadSettingCost()
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        Me._distriutionIntermediate = New CostDistributionIntermediate() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense.ContainsKey(CInt(Me._idCurrentSequence)) = True AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MCommonCost(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Sub
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If
        Me.ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Valida el valor de los agregados contra el valor contable
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateProportionValue(maximumAmmount As Decimal, Optional oldValue As Decimal = 0) As Boolean
        If ListDistributionIntermediateDetail IsNot Nothing AndAlso ListDistributionIntermediateDetail.Count > 0 Then
            If InteropCostStaticService.CalculateDistributedValue(ListDistributionIntermediateDetail.Sum(Function(x) x.Proportion) - oldValue, maximumAmmount) > 100 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DistributedValueError", MODULE_NAME)
                Return False
            End If
        Else
            If maximumAmmount > 100 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DistributedValueError", MODULE_NAME)
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Imports the previus data.
    ''' </summary>
    Private Async Sub ImportPreviusData()
        Dim _settingsCostQ As CostSetting
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            Using ModelSetting As New MCostSetting(Me.Tag)
                _settingsCostQ = ModelSetting.GetCostSetting()
            End Using
        Else
            _settingsCostQ = SettingsCost
        End If
        If _settingsCostQ IsNot Nothing Then
            Dim _previusMonth As Integer = _settingsCostQ.Month - 1
            Dim _previusYear As Integer = _settingsCostQ.Year
            If _previusMonth = 0 Then
                _previusMonth = 12
                _previusYear -= 1
            End If
            Dim _listPeriods As List(Of String) = Await model.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(_previusYear, _previusMonth)
            If _listPeriods Is Nothing OrElse _listPeriods.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos de periodos anteriores para importar"
                Exit Sub
            End If
            Using frm As New FrmCostSelectPeriod
                frm.DatasourceType = FrmCostSelectPeriod.eDatasourceType.DistributionIntermediate
                frm.PreviusMonth = _previusMonth
                frm.PreviusYear = _previusYear
                frm.LisPeriods = _listPeriods
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                frm.MinimizeBox = False
                frm.MaximizeBox = False
                AddHandler frm.AddImportDataIntermediate, AddressOf ImportData
                Dim transparent As New FrmTransparent(frm, False)
                transparent.ShowDialog(Me)
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron parámetros de costos"
        End If
    End Sub

    ''' <summary>
    ''' Imports the data.
    ''' </summary>
    Private Async Sub ImportData(dataImpor As List(Of CostDistributionIntermediate))
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                Dim res = (From ou As CostSecuenceDetail In Me._sequence.CostSecuenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
                If res IsNot Nothing AndAlso res.Count > 0 Then
                    Me._idCurrentSequence = res(0).Id
                Else
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
                End If
            End If
            Dim errorList As New StringBuilder()
            If dataImpor IsNot Nothing Then
                Dim _listMessage As New List(Of Tuple(Of String, Integer))
                For Each item As CostDistributionIntermediate In dataImpor
                    Dim _distrib As New CostDistributionIntermediate()
                    With _distrib
                        .ProductionCenterId = item.ProductionCenterId
                        .Description = item.Description
                        .Month = Month
                        .Year = Year
                        .Status = item.Status
                        For Each itemDetail As CostDistributionIntermediateDetail In item.CostDistributionIntermediateDetail
                            Dim _distribDetail As New CostDistributionIntermediateDetail()
                            With _distribDetail
                                .ProductionCenterId = itemDetail.ProductionCenterId
                                .Proportion = itemDetail.Proportion
                            End With
                            .CostDistributionIntermediateDetail.Add(_distribDetail)
                        Next
                    End With
                    Dim result = Await model.SaveDistributionIntermediate(_distrib, Me._idCurrentSequence)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionIntermediateImportCorrect", MODULE_NAME), item.FullNameProductionCenter), 1))
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-001" Then
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionIntermediateImportAdvertence", MODULE_NAME), item.FullNameProductionCenter), 3))
                        Else
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionIntermediateImportError", MODULE_NAME), item.FullNameProductionCenter, result.Message), 2))
                        End If
                    End If
                Next
                Using Formulario As New FrmListErrors(_listMessage)
                    Formulario.Title = ResourceManager.GetString("ResultOperationMessage")
                    Formulario.MinimizeBox = False
                    Formulario.MaximizeBox = False
                    Formulario.Size = New Size(780, 500)
                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(Formulario, False)
                    transparent.ShowDialog(Me)
                End Using
                If FormSearchObjects IsNot Nothing Then
                    FormSearchObjects.UpdateDatasource()
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' Adds the distribution fixed asset detail.
    ''' </summary>
    Private Sub AddDistributionIntermediateDetail()
        If ValidateProductionCenterInList(CType(INDsleProductionCenter.EditValue, Integer)) Then
            Dim _distributionIntermediateDetail As New CostDistributionIntermediateDetail()
            With _distributionIntermediateDetail
                .ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer)
                .Proportion = CType(INDtxtMaximumAmount.EditValue, Decimal)
            End With
            _distriutionIntermediate.CostDistributionIntermediateDetail.Add(_distributionIntermediateDetail)
            ListDistributionIntermediateDetail = _distriutionIntermediate.CostDistributionIntermediateDetail.ToList()
            INDgcDetail.RefreshDataSource()
            CleanControlsDetail()
        End If
    End Sub

    ''' <summary>
    ''' Valida que el centro de produccion a agregar no esté ya agregado
    ''' </summary>
    Private Function ValidateProductionCenterInList(productionCenter As Integer) As Boolean
        If ListDistributionIntermediateDetail IsNot Nothing AndAlso ListDistributionIntermediateDetail.Count > 0 Then
            If ListDistributionIntermediateDetail.Where(Function(x) x.ProductionCenterId = productionCenter).Count() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductionCenterRepeat", MODULE_NAME)
                Return False
            End If
        Else
            If productionCenter = CType(INDsleProductionCenterHeader.EditValue, Integer) Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductionCenterHeaderRepeat", MODULE_NAME)
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonCost As New MCommonCost(Me.Tag)
                Await ModelCommonCost.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' valida los controles de centros de producción
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsProductionCenter() As Boolean
        Dim errorList As New StringBuilder()
        If CType(INDtxtMaximumAmount.EditValue, Decimal) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("MaximumQuantity", MODULE_NAME)))
        End If
        If CType(INDsleProductionCenter.EditValue, Integer) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ProductionCenter", MODULE_NAME)))
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Opens the form.
    ''' </summary>
    ''' <param name="form">The form.</param>
    Private Sub OpenFormLocal(ByVal form As FormBase)
        form.ViewModeEditHold = True
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.MinimizeBox = False
        form.MaximizeBox = False
        form.Size = New Size(800, 700)
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Dim transparent As New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles del detalle del gasto directo
    ''' </summary>
    Private Sub CleanControlsDetail()
        INDsleProductionCenter.EditValue = Nothing
        INDtxtMaximumAmount.EditValue = 0
        INDsleProductionCenter.Focus()
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._distriutionIntermediate.Code, Me._distriutionIntermediate.Description), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me._distriutionIntermediate.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distriutionIntermediate.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._distriutionIntermediate.Code, Me._distriutionIntermediate.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distriutionIntermediate.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements ICostIntermediateDistribution.AssigningValues
        With _distriutionIntermediate
            .Code = Code
            .ProductionCenterId = ProductionCenterId
            .Description = Description
            .Year = Year
            .Month = Month
        End With
    End Sub



    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements ICostIntermediateDistribution.CleanControls
        INDlcRoot.BeginUpdate()
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        Code = String.Empty
        ProductionCenterId = Nothing
        Description = Nothing
        'Year = Nothing
        'Month = Nothing


        DdbActions.Enabled = False
        INDpceAddDetail.Enabled = False
        ListDistributionIntermediateDetail = Nothing
        INDsleProductionCenterHeader.Properties.ReadOnly = False
        INDsleProductionCenterHeader.Properties.NullText = String.Empty
        _distriutionIntermediate = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            If FormSearchObjects.Visible Then
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            End If
        End If

        INDlcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements ICostIntermediateDistribution.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            Dim res = Await model.GetDistributionIntermediate(Me.Code)
            If res.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(res.StatusCode) = res.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _distriutionIntermediate = res.ObjectEmbbeded
            If _distriutionIntermediate IsNot Nothing AndAlso _distriutionIntermediate.Id > 0 Then
                Using ModelCommon As New MCommonCost(Me.Tag)
                    Dim result = Await ModelCommon.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _distriutionIntermediate.Id)
                    With _distriutionIntermediate
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        Year = .Year
                        Month = .Month
                        ProductionCenterId = .ProductionCenterId
                        Description = .Description
                        Status = .Status
                    End With

                    'INDsleFixetAsset.Properties.NullText = _distriutionFixedAsset.FullNameFixedAsset
                    ListDistributionIntermediateDetail = _distriutionIntermediate.CostDistributionIntermediateDetail.ToList()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._distriutionIntermediate.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _distriutionIntermediate.Id}
                        Dim operation = Await ModelCommon.SaveBlockRecordCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_distriutionIntermediate.Id, MyTag, Nothing, GetType(CostDistributionIntermediate).Name)
                    If _distriutionIntermediate.Status = True Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    End If

                    Dim _settingsCostQ As CostSetting
                    Using ModelSetting As New MCostSetting(Me.Tag)
                        _settingsCostQ = ModelSetting.GetCostSetting()
                    End Using
                    If Year <> _settingsCostQ.Year OrElse Month <> _settingsCostQ.Month Then
                        'Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Dim dtfi = indigo.Culture.DateTimeFormat
                        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RegisterNotInPeriod", MODULE_NAME), Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(_settingsCostQ.Month), Microsoft.VisualBasic.VbStrConv.ProperCase), _settingsCostQ.Year)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, "")
                        ReadOnlyControls(True, INDlcRoot)
                    End If
                    AsyncLoader(False)
                    INDlcRoot.EndUpdate()
                    ActionsOnControls = True
                    INDbteCode.Enabled = False
                End Using
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewDistributionIntermediate()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
        End If
    End Sub
#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminars this instance.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me._distriutionIntermediate IsNot Nothing AndAlso Me._distriutionIntermediate.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteDistributionIntermediate(Me._distriutionIntermediate)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                    End If
                    Mensaje(result.StatusCode) = result.Message
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Me.BarraBotones.Focus()
        If ValidateControls() = True Then
            If INDgvDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo un detalle"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveDistributionIntermediate(Me._distriutionIntermediate, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _distriutionIntermediate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.CostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._distriutionIntermediate = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                AsyncLoader(False)
                INDbteCode.Enabled = False
            End If
            Mensaje(result.StatusCode) = result.Message
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                AsyncLoader(True)
                Dim state As Boolean = Not Me._distriutionIntermediate.Status
                Dim result = Await model.UpdateStateDistributionIntermediate(Me._distriutionIntermediate.Code, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._distriutionIntermediate = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                End If
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Mensaje(result.StatusCode) = result.Message
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewDistributionIntermediate()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        AddHandler FormSearchObjects.ImportPreviusData, AddressOf BarraBotones_Click_ImportarInformacion
        _presenter.LoadSettingCost()
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            Exit Sub
        End If
        Dim parameters As Object() = {Year, Month}
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Mes", .FieldName = "Month", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostDistributionIntermediateByYearMonth
            .SearchParameters = parameters
            .ImportDataButton = True
            .FormParent = Me
            .ShowSearch()
        End With
        If indigo.UserViewMode = True And Me.ViewModeEditHold = False Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        End If
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub
#End Region

#Region "BarButton Events"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ importar informacion.
    ''' </summary>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        _presenter.LoadSettingCost()
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        ImportPreviusData()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub
#End Region

End Class