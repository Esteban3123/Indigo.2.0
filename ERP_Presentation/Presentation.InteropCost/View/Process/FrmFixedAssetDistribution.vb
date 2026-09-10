'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 26-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.InteropCost.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Controls.MVP
Imports DevExpress.Utils.Menu
Imports System.ComponentModel
Imports DevExpress.Data.PLinq
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

#End Region

Public Class FrmFixedAssetDistribution
    Implements IFixedAssetDistribution

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmFixedAssetDistribution"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrDistributionFixedAsset()
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
        _presenter = New PDistributionFixedAsset(Me)
        model = New MDistributionFixedAsset(Me.Tag)
        mProductionCenter = New MProductionCenter(Me.Tag)
    End Sub
#End Region

#Region "Properties and Variables"

#Region "Entity Properties"
    ''' <summary>
    ''' Código de la distribución de Activos Fijos
    ''' </summary>
    Public Property Code As String Implements IFixedAssetDistribution.Code
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

    Public Property DeprecationValue As Decimal
        Get
            Return _tmpCurrentPeriod.DeprecationValue
        End Get
        Set(value As Decimal)
            _tmpCurrentPeriod.DeprecationValue = value
            If _distriutionFixedAsset IsNot Nothing Then
                _distriutionFixedAsset.DepreciationValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Descripción de la distribución de Activos Fijos
    ''' </summary>
    Public Property Description As String Implements IFixedAssetDistribution.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Código del Activo fijo a distribuir
    ''' </summary>
    Public Property FixedAssetCode As String Implements IFixedAssetDistribution.FixedAssetCode

    ''' <summary>
    ''' Id del Activo fijo a distribuir
    ''' </summary>
    Public Property FixedAssetId As Integer Implements IFixedAssetDistribution.FixedAssetId
        Get
            Return CType(INDsleFixetAsset.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleFixetAsset.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución de Activos Fijos
    ''' </summary>
    Public Property Month As Integer Implements IFixedAssetDistribution.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As Domain.Entities.InteropCostSecuence Implements IFixedAssetDistribution.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InteropCostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InteropCostSecuenceDetail In Me._sequence.InteropCostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As Domain.Entities.InteropCostSetting Implements IFixedAssetDistribution.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As Domain.Entities.InteropCostSetting)
            _tmpCurrentPeriod.Month = value.Month
            _tmpCurrentPeriod.Year = value.Year
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' Estado de la distribución de Activos Fijos
    ''' </summary>
    Public Property Status As String Implements IFixedAssetDistribution.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Año de la distribución de Activos Fijos
    ''' </summary>
    Public Property Year As Integer Implements IFixedAssetDistribution.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property
#End Region

#Region "Datasource Entities"
    ''' <summary>
    ''' Gets or sets the production center data source.
    ''' </summary>
    Public Property ProductionCenterDataSource As XPInstantFeedbackSource Implements IFixedAssetDistribution.ProductionCenterDataSource
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
    Public Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource Implements IFixedAssetDistribution.ProductionCenterDataSourceRerpository
        Get
            Return CType(INDrpsleProductionCenter.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleProductionCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de activos fijos del erp a interfazar
    ''' </summary>
    Public Property FixedAssetDatasource As XPInstantFeedbackSource Implements IFixedAssetDistribution.FixedAssetDatasource
        Get
            Return CType(INDsleFixetAsset.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFixetAsset.Properties.DataSource = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"

    ''' <summary>
    ''' The _TMP current period
    ''' </summary>
    Private _tmpCurrentPeriod As CtrDistributionFixedAsset

    ''' <summary>
    ''' The _settings cost
    ''' </summary>
    Private _settingsCost As InteropCostSetting

    Private model As MDistributionFixedAsset
    Private mProductionCenter As MProductionCenter

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IFixedAssetDistribution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetDistribution.MyTag
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
    Private _sequence As Domain.Entities.InteropCostSecuence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PDistributionFixedAsset

    ''' <summary>
    ''' entidad de gastos generales
    ''' </summary>
    Private _distriutionFixedAsset As DistributionFixedAsset

    ''' <summary>
    ''' Listado de detalle de distribución de activos fijos
    ''' </summary>
    Property ListDistributionFixedAssetDetail As List(Of DistributionFixedAssetDetail)
        Get
            Return CType(INDgcDetail.DataSource, List(Of DistributionFixedAssetDetail))
        End Get
        Set(value As List(Of DistributionFixedAssetDetail))
            INDgcDetail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInteropCost

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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetDistribution.ActionsOnControls
        Set(value As Boolean)

            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleFixetAsset.Enabled = value
            INDpceAddDetail.Enabled = value
            INDgcDetail.Enabled = value
            DdbActions.Enabled = value
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
        model = Nothing
        mProductionCenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        _distriutionFixedAsset = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmFixedAssetDistribution control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmFixedAssetDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmDistributionDirectExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionDirectExpenses_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Shown"
    Private Sub FrmDistributionDirectExpenses_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
#End Region

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionDirectExpenses_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
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
                    Me.NewDistributionFixedAsset()
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
    Private Sub INDrpsleProductionCenter_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpsleProductionCenter.EditValueChanging
        If Not ValidateProductionCenterInList(e.NewValue) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDrpspnValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpspnValue.EditValueChanging
        If Not String.IsNullOrEmpty(e.NewValue) Then
            If Not ValidateProportionValue(CDec(e.NewValue.ToString().Replace(".", ",")), CDec(INDgvDetail.GetFocusedRow().Proportion)) Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleFixetAsset control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleFixetAsset_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFixetAsset.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If
        If FixedAssetId <> 0 AndAlso INDgvFixedAsset.GetFocusedRow() IsNot Nothing Then
            Dim _afnCalDep As Infrastructure.Data.Xpo.InteropCostRepository.AFNCALDEPXpo = CType(CType(INDgvFixedAsset.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InteropCostRepository.AFNCALDEPXpo)
            FixedAssetCode = _afnCalDep.AFNACTIVO.AACCODACT
            Dim dprecationValue As Decimal = Await model.GetDeprecationValue(FixedAssetId, Year, Month)
            DeprecationValue = dprecationValue
            Await GenerateFixedAssetDetails(_afnCalDep.CTNCENCOS.OID, String.Concat(_afnCalDep.CTNCENCOS.CCCODIGO.Trim(), " - ", _afnCalDep.CTNCENCOS.CCNOMBRE.Trim()))
        End If
    End Sub
#End Region

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDirectCostDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDetail.DataSourceChanged
        INDsleFixetAsset.Properties.ReadOnly = ListDistributionFixedAssetDetail IsNot Nothing AndAlso ListDistributionFixedAssetDetail.Count > 0
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmProductionCenter
                OpenFormLocal(formulario)
                _presenter.InitializeProductionCenter()
            End Using
        End If
    End Sub
#End Region

#Region "ItemClick"

    Private Async Sub MBtnAddProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnAddProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            Dim listProductionCenter As List(Of ProductionCenter) = Await mProductionCenter.ListProductionCenter()
            If listProductionCenter IsNot Nothing AndAlso listProductionCenter.Count > 0 Then
                If ListDistributionFixedAssetDetail IsNot Nothing AndAlso ListDistributionFixedAssetDetail.Count > 0 Then
                    For Each item As DistributionFixedAssetDetail In ListDistributionFixedAssetDetail
                        listProductionCenter.Remove(listProductionCenter.Where(Function(x) x.Id = item.ProductionCenterId).FirstOrDefault())
                    Next
                End If
                For Each item As ProductionCenter In listProductionCenter
                    Dim _distribFixedAssetDetail As New DistributionFixedAssetDetail()
                    _distriutionFixedAsset.DistributionFixedAssetDetail.Add(_distribFixedAssetDetail)
                    With _distribFixedAssetDetail
                        .ProductionCenterId = item.Id
                        .SetProportion = 0
                        '.Proportion = 0
                    End With
                Next
                ListDistributionFixedAssetDetail = _distriutionFixedAsset.DistributionFixedAssetDetail.ToList()
                INDgcDetail.RefreshDataSource()
            End If
            AsyncLoader(False)
            INDlcRoot.EndUpdate()
        End If
    End Sub

    Private Sub MBtnRemoveProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnRemoveProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _distriutionFixedAsset.DistributionFixedAssetDetail IsNot Nothing AndAlso _distriutionFixedAsset.DistributionFixedAssetDetail.Count > 0 Then
                While _distriutionFixedAsset.DistributionFixedAssetDetail.Count > 0
                    _distriutionFixedAsset.DistributionFixedAssetDetail(_distriutionFixedAsset.DistributionFixedAssetDetail.Count - 1).MarkAsDeleted()
                End While
                ListDistributionFixedAssetDetail = _distriutionFixedAsset.DistributionFixedAssetDetail.ToList()
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
            AddDistributionFixedAssetDetail()
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
            Dim _distribFixedAssetDetail As DistributionFixedAssetDetail = CType(INDgvDetail.GetFocusedRow(), DistributionFixedAssetDetail)
            _distribFixedAssetDetail.MarkAsDeleted()
            If _distriutionFixedAsset.ChangeTracker.State <> ObjectState.Added Then
                _distriutionFixedAsset.MarkAsModified()
            End If
            ListDistributionFixedAssetDetail = _distriutionFixedAsset.DistributionFixedAssetDetail.ToList()
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
    Private Sub INDsleFixetAsset_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFixetAsset.QueryPopUp
        If INDsleFixetAsset.Properties.DataSource Is Nothing Then
            INDsleFixetAsset.Properties.DataSource = model.ListDepreciationByYearMonth(_tmpCurrentPeriod.Year, _tmpCurrentPeriod.Month)
        End If
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
        If Me._distriutionFixedAsset IsNot Nothing AndAlso Me._distriutionFixedAsset.Id > 0 Then
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

#Region "Methods"

    ''' <summary>
    ''' Método que exporta los datos a excel para que el usuario mire con anterioridad como va a quedar la distribución
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ExportExcel() As Task
        Me.BarraBotones.Focus()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result)) = Await model.SP_ExportExcelDistributionFixedAsset(indigo.InteropCostContainer, Year, Month)
            If result.StatusCode = eStatusResult.EXCEPTION OrElse result.StatusCode = eStatusResult.WARNING Then
                AsyncLoader(False)
                Mensaje(result.StatusCode) = result.Message
                Exit Function
            End If
            If result.ObjectEmbbeded Is Nothing OrElse result.ObjectEmbbeded.Count = 0 Then
                AsyncLoader(False)
                Mensaje(eStatusResult.WARNING) = "No se encontraron datos para exportar a excel"
                Exit Function
            End If

            'Se llena el datasource de la rejilla de exportación
            INDgcExport.DataSource = Nothing
            INDgcExport.DataSource = result.ObjectEmbbeded.ToList

            'Se exporta la vista a excel
            'Dim fileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\DistributionFixedAssetInteropCost.xlsx"
            Dim fileName As String = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder() + "\DistributionFixedAssetInteropCost.xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            INDviewExport.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Método que confirma masivamente la distribución
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ConfirmMasive() As Task
        'Se pregunta si se quiere confirmar
        If MessageIndigo.Show("Desea confirmar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Function
        End If
        Me.BarraBotones.Focus()
        Try
            AsyncLoader(True)
            Dim result = Await model.SP_ConfirmMasiveDistributionFixedAsset(indigo.InteropCostContainer, Year, Month)
            If result.StatusCode = eStatusResult.SUCCESS Then
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
    End Function

    ''' <summary>
    ''' News the distribution fixed asset.
    ''' </summary>
    Private Async Sub NewDistributionFixedAsset()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        _presenter.LoadSettingCost()
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        Me._distriutionFixedAsset = New DistributionFixedAsset()
        If Me._sequence.IsManual Then
            If Me.BarraBotones.FilterDataSource Is Nothing Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.InteropCostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    If Me.BarraBotones.FilterDataSource Is Nothing Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense.ContainsKey(CInt(Me._idCurrentSequence)) = True AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                        If Me.BarraBotones.FilterDataSource Is Nothing Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        End If
                    Else
                        Using model As New MCommonInteropCost(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            If Me.BarraBotones.FilterDataSource Is Nothing Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            End If
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Sub
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    If Me.BarraBotones.FilterDataSource Is Nothing Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    End If
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                If Me.BarraBotones.FilterDataSource Is Nothing Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        Me.ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Valida el valor de los agregados contra el valor contable
    ''' </summary>
    ''' <param name="oldValue">valor anterior (solo pasa cuando se modifica de la rejilla)</param>
    ''' <returns></returns>
    Private Function ValidateProportionValue(maximumAmmount As Decimal, Optional oldValue As Decimal = 0) As Boolean
        If ListDistributionFixedAssetDetail IsNot Nothing AndAlso ListDistributionFixedAssetDetail.Count > 0 Then
            If InteropCostStaticService.CalculateDistributedValue(ListDistributionFixedAssetDetail.Sum(Function(x) x.Proportion) - oldValue, maximumAmmount) > 100 Then
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
        Dim _settingsCostQ As InteropCostSetting
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            Using ModelSetting As New MInteropCostSetting(Me.Tag)
                _settingsCostQ = ModelSetting.GetInteropCostSetting()
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
            Dim _listPeriods As List(Of String) = Await model.ListPeriodWithDataByMaximumPeriod(_previusYear, _previusMonth)
            If _listPeriods Is Nothing OrElse _listPeriods.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos de periodos anteriores para importar"
                Exit Sub
            End If
            Using frm As New FrmSelectPeriod
                frm.DatasourceType = FrmSelectPeriod.eDatasourceType.DistributionFixedAsset
                frm.PreviusMonth = _previusMonth
                frm.PreviusYear = _previusYear
                frm.LisPeriods = _listPeriods
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                frm.MinimizeBox = False
                frm.MaximizeBox = False
                AddHandler frm.AddImportDataFixedAsset, AddressOf ImportData
                'frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
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
    Private Async Sub ImportData(dataImpor As List(Of DistributionFixedAsset))
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                Dim res = (From ou As InteropCostSecuenceDetail In Me._sequence.InteropCostSecuenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
                If res IsNot Nothing AndAlso res.Count > 0 Then
                    Me._idCurrentSequence = res(0).Id
                Else
                    Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail(0).Id
                End If
            End If
            Dim errorList As New StringBuilder()
            If dataImpor IsNot Nothing Then
                Dim _listMessage As New List(Of Tuple(Of String, Integer))
                For Each item As DistributionFixedAsset In dataImpor
                    Dim _distrib As New DistributionFixedAsset()
                    With _distrib
                        .FixedAssetId = item.FixedAssetId
                        .FixedAssetCode = item.FixedAssetCode
                        .DepreciationValue = item.DepreciationValue
                        .Description = item.Description
                        .Month = Month
                        .Year = Year
                        .Status = item.Status
                        For Each itemDetail As DistributionFixedAssetDetail In item.DistributionFixedAssetDetail
                            Dim _distribDetail As New DistributionFixedAssetDetail()
                            .DistributionFixedAssetDetail.Add(_distribDetail)
                            With _distribDetail
                                .ProductionCenterId = itemDetail.ProductionCenterId
                                .DepreciationValue = itemDetail.DepreciationValue
                                .Proportion = itemDetail.Proportion
                            End With
                        Next
                    End With
                    Dim result = Await model.SaveDistributionFixedAsset(_distrib, Me._idCurrentSequence)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionFixedAssetImportCorrect", MODULE_NAME), item.FullNameFixedAsset), 1))
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-001" Then
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionFixedAssetImportAdvertence", MODULE_NAME), item.FullNameFixedAsset), 3))
                        Else
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionFixedAssetImportError", MODULE_NAME), item.FullNameFixedAsset, result.Message), 2))
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
    Private Sub AddDistributionFixedAssetDetail()
        If ValidateProductionCenterInList(CType(INDsleProductionCenter.EditValue, Integer)) Then
            Dim _distributionFixAssetDetail As New DistributionFixedAssetDetail()
            _distriutionFixedAsset.DistributionFixedAssetDetail.Add(_distributionFixAssetDetail)
            With _distributionFixAssetDetail
                .ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer)
                .SetProportion = CType(INDtxtMaximumAmount.EditValue, Decimal)
                '.Proportion = CType(INDtxtMaximumAmount.EditValue, Decimal)
            End With
            INDgcDetail.DataSource = Nothing
            ListDistributionFixedAssetDetail = _distriutionFixedAsset.DistributionFixedAssetDetail.ToList()
            INDgcDetail.RefreshDataSource()
            CleanControlsDetail()
        End If
    End Sub

    ''' <summary>
    ''' Valida que el centro de produccion a agregar no esté ya agregado
    ''' </summary>
    Private Function ValidateProductionCenterInList(productionCenter As Integer) As Boolean
        If ListDistributionFixedAssetDetail IsNot Nothing AndAlso ListDistributionFixedAssetDetail.Count > 0 Then
            If ListDistributionFixedAssetDetail.Where(Function(x) x.ProductionCenterId = productionCenter).Count() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductionCenterRepeat", MODULE_NAME)
                Return False
            End If
            'If INDsleProductionCenter.EditValue IsNot Nothing Then
            '    If ListDistributionFixedAssetDetail.Where(Function(x) x.ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer)).Count() > 0 Then
            '        Return False
            '    End If
            'Else
            '    Dim _distributionDetail As DistributionFixedAssetDetail = CType(INDgvDetail.GetFocusedRow(), DistributionFixedAssetDetail)
            '    If ListDistributionFixedAssetDetail.Where(Function(x) x.ProductionCenterId = CType(_distributionDetail.ProductionCenterId, Integer)).Count() > 1 Then
            '        Return False
            '    End If
            'End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonInteropCost As New MCommonInteropCost(Me.Tag)
                Await ModelCommonInteropCost.DeleteBlockRecordInteropCost(_record)
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
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._distriutionFixedAsset.Code, Me._distriutionFixedAsset.Description), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._distriutionFixedAsset.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distriutionFixedAsset.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._distriutionFixedAsset.Code, Me._distriutionFixedAsset.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distriutionFixedAsset.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IFixedAssetDistribution.AssigningValues
        With _distriutionFixedAsset
            .Code = Code
            .FixedAssetCode = FixedAssetCode
            .FixedAssetId = FixedAssetId
            .Description = Description
            .Year = Year
            .Month = Month
            .DepreciationValue = DeprecationValue
            .Status = If(Status Is Nothing, False, CType(Status, Boolean))
        End With
    End Sub


    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IFixedAssetDistribution.CleanControls
        INDlcRoot.BeginUpdate()
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        Code = String.Empty
        FixedAssetCode = Nothing
        FixedAssetId = Nothing
        Description = Nothing
        DeprecationValue = Nothing

        ListDistributionFixedAssetDetail = Nothing
        INDsleFixetAsset.Properties.ReadOnly = False

        INDsleFixetAsset.Properties.NullText = String.Empty
        _distriutionFixedAsset = Nothing
        Me.BarraBotones.SetPermitirNavegacion(Nothing)

        _tmpCurrentPeriod.DeprecationValue = 0D
        Me.BarraBotones.FilterDataSource = Nothing
        '_tmpCurrentPeriod.Year = 0
        '_tmpCurrentPeriod.Month = 0

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
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ModoNavegacion) = False
        INDlcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements IFixedAssetDistribution.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            _isLoading = True
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            If _distriutionFixedAsset Is Nothing Then
                Dim res = Await model.GetDistributionFixedAsset(Me.Code)
                If res.StatusCode <> eStatusResult.SUCCESS Then
                    Mensaje(res.StatusCode) = res.Message
                    AsyncLoader(False)
                    Exit Sub
                End If
                _distriutionFixedAsset = res.ObjectEmbbeded
            End If
            If _distriutionFixedAsset IsNot Nothing AndAlso _distriutionFixedAsset.Id > 0 Then
                Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, _distriutionFixedAsset.Id)
                    With _distriutionFixedAsset
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        Year = .Year
                        Month = .Month
                        FixedAssetCode = .FixedAssetCode
                        FixedAssetId = .FixedAssetId
                        Description = .Description
                        DeprecationValue = .DepreciationValue
                        If .Status Then
                            Status = "1"
                        Else
                            Status = "0"
                        End If
                    End With

                    INDsleFixetAsset.Properties.NullText = _distriutionFixedAsset.FullNameFixedAsset
                    ListDistributionFixedAssetDetail = _distriutionFixedAsset.DistributionFixedAssetDetail.ToList()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._distriutionFixedAsset.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _distriutionFixedAsset.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    If Me.BarraBotones.FilterDataSource Is Nothing Then
                        Me.BarraBotones.SetDocuments(_distriutionFixedAsset.Id, MyTag, Nothing, GetType(DistributionFixedAsset).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    End If
                    If _distriutionFixedAsset.Status Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                    Dim _settingsCostQ As InteropCostSetting
                    Using ModelSetting As New MInteropCostSetting(Me.Tag)
                        _settingsCostQ = ModelSetting.GetInteropCostSetting()
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
                    Me.NewDistributionFixedAsset()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            _isLoading = False
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
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me._distriutionFixedAsset IsNot Nothing AndAlso Me._distriutionFixedAsset.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteDistributionFixedAsset(Me._distriutionFixedAsset)
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
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
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
            Dim result = Await model.SaveDistributionFixedAsset(Me._distriutionFixedAsset, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _distriutionFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._distriutionFixedAsset = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                If Me.BarraBotones.FilterDataSource Is Nothing Then
                    Me.Deshacer()
                End If
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

    Public Async Function GuardarTask() As Task(Of Boolean)
        If ValidateControls() = True Then
            If INDgvDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo un detalle"
                Return False
            End If
        Else
            Return False
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveDistributionFixedAsset(Me._distriutionFixedAsset, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _distriutionFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._distriutionFixedAsset = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                If Me.BarraBotones.FilterDataSource Is Nothing Then
                    Me.Deshacer()
                End If
                Mensaje(result.StatusCode) = result.Message
                Return True
            Else
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Mensaje(result.StatusCode) = result.Message
                Return False
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Dim state As Boolean
                Select Case Status
                    Case eActionsStatusRecords.Active
                        state = True
                    Case eActionsStatusRecords.Inactive
                        state = False
                End Select
                AsyncLoader(True)
                Dim result = Await model.UpdateStateDistributionFixedAsset(Me._distriutionFixedAsset.Code, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._distriutionFixedAsset = result.ObjectEmbbeded
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
            Me.NewDistributionFixedAsset()
        End If
    End Sub

    Private _isLoading As Boolean
    Public Async Sub Nuevo(afnCaldep As Object)
        NewDistributionFixedAsset()
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 AndAlso _settingsCost IsNot Nothing AndAlso _settingsCost.Id > 0 Then
            ActionsOnControls = True
            INDbteCode.Enabled = False
            INDpceAddDetail.Enabled = True
            DdbActions.Enabled = True
            _isLoading = True
            AsyncLoader(True)
            FixedAssetId = CInt(afnCaldep.AFNACTIVO.OID)
            FixedAssetCode = afnCaldep.AFNACTIVO.AACCODACT
            Dim dprecationValue As Decimal = Await model.GetDeprecationValue(FixedAssetId, Year, Month)
            DeprecationValue = dprecationValue
            INDsleFixetAsset.Properties.NullText = String.Concat(afnCaldep.AFNACTIVO.AACCODACT, " - ", afnCaldep.AFNACTIVO.AFNPRODUC.APRNOMBRE)

            Await GenerateFixedAssetDetails(CInt(afnCaldep.CTNCENCOS.OID), String.Concat(afnCaldep.CTNCENCOS.CCCODIGO.Trim(), " - ", afnCaldep.CTNCENCOS.CCNOMBRE.Trim()))

            AsyncLoader(False)
            _isLoading = False
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "AFNACTIVO.AACCODACT", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Activo", .FieldName = "AFNACTIVO.AFNPRODUC.APRNOMBRE", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Fecha Depreciación", .FieldName = "AFNDEPRECI.ACAFECCHCI", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2, .ColumnFormat = "dd \de MMMM \de yyyy", .ColumnFormatType = DevExpress.Utils.FormatType.DateTime}}.ToList
            .ValorSolicitado = "OID"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDepreciationByYearMonth
            .SearchParameters = parameters
            .ImportDataButton = True
            .FormParent = Me
            .ShowSearch()
        End With

        'Se toman las depreciaciones de la tabla AFNDEPRECI

        If indigo.UserViewMode = True And Me.ViewModeEditHold = False Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ModoNavegacion) = False
        End If
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        _distriutionFixedAsset = (Await model.GetDistributionFixedAssetByActivoAndYearMonth(CInt(ReturnObject.AFNACTIVO.OID), _tmpCurrentPeriod.Year, _tmpCurrentPeriod.Month)).ObjectEmbbeded
        DeleteBlockedRecord()

        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        If _distriutionFixedAsset IsNot Nothing AndAlso _distriutionFixedAsset.Id > 0 Then
            INDbteCode.Text = _distriutionFixedAsset.Code
            If INDbteCode.Text <> String.Empty Then
                LoadControls()
            End If
        Else
            If Me._sequence.IsManual Then
                Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            Else
                Nuevo(ReturnObject)
            End If
        End If

        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        'INDbteCode.Text = ReturnValue
        'If INDbteCode.Text <> String.Empty Then
        '    LoadControls()
        'End If
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

    Private Sub BarraBotones_Click_ModoNavegacion() Handles BarraBotones.Click_ModoNavegacion
        OpenNavigationMode()
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

    Public Async Function ResultRecordNavigation() As Task(Of Boolean)
        If MessageIndigo.Show("Al navegar al siguiente registro se perderán los cambios. Desea " & IIf(_distriutionFixedAsset.Id = 0, "Guardar", "Actualizar") & " antes de continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Return Await GuardarTask()
        Else
            Return True
        End If
    End Function

    Private Sub OpenNavigationMode()
        If _sequence Is Nothing OrElse _sequence.Id = 0 OrElse _sequence.IsManual Then
            Mensaje(eStatusResult.WARNING) = "La secuencia debe estar configurada como automática para activar esta opción"
            Exit Sub
        End If
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            _presenter.LoadSettingCost()
        End If
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            Exit Sub
        End If
        Me.BarraBotones.SetPermitirNavegacion(AddressOf ResultRecordNavigation)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Dim depreci As XPCollection(Of Infrastructure.Data.Xpo.InteropCostRepository.AFNDEPRECIXpo) = model.GetRegistroDepreciacionByYearMont(Year, Month)
        If depreci Is Nothing OrElse depreci.Count = 0 Then
            Mensaje(eStatusResult.WARNING) = "No se encontró depreciación confirmada para el periodo de costos"
            Exit Sub
        End If
        AsyncLoader(True)
        Dim listAFNCALDEP As XPCollection(Of Infrastructure.Data.Xpo.InteropCostRepository.AFNCALDEPXpo) = model.GetCollectionDepreciacionByYearMont(Year, Month)
        Me.BarraBotones.ColumnInfo = {New ColumnInfo() With {.Caption = "Codigo", .FieldName = "AFNACTIVO.AACCODACT"},
                                      New ColumnInfo() With {.Caption = "Activo", .FieldName = "AFNACTIVO.AFNPRODUC.APRNOMBRE"},
                                      New ColumnInfo() With {.Caption = "Fecha Depreciación", .FieldName = "AFNDEPRECI.ACAFECCHCI"}}.ToList()
        Me.BarraBotones.FilterDataSource = listAFNCALDEP.ToList()
        AsyncLoader(False)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GuardarConfirmar, "Exportar")
    End Sub

    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent

        Me._idEntity = String.Empty
        If FormSearchObjects IsNot Nothing Then
            FormSearchObjects.Cancelar()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
        CleanControlsNavigation()
        Dim afnCaldep = CType(Record, Infrastructure.Data.Xpo.InteropCostRepository.AFNCALDEPXpo)
        INDtxtDescription.Text = afnCaldep.AFNACTIVO.CodeProduct
        ReturnValue(afnCaldep.OID, afnCaldep)
    End Sub

    Public Sub CleanControlsNavigation()
        INDlcRoot.BeginUpdate()
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        Code = String.Empty
        FixedAssetCode = Nothing
        FixedAssetId = Nothing
        Description = Nothing
        DeprecationValue = Nothing

        ListDistributionFixedAssetDetail = Nothing
        INDsleFixetAsset.Properties.ReadOnly = False

        INDsleFixetAsset.Properties.NullText = String.Empty
        _distriutionFixedAsset = Nothing

        _tmpCurrentPeriod.DeprecationValue = 0D

        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        
        INDlcRoot.EndUpdate()
    End Sub

    Private Async Function GenerateFixedAssetDetails(costCenterId As Integer, costCenterCodeName As String) As Task
        'Validar cual centro de produccion contiene ese centro de costo
        Dim pCenter As ProductionCenter = Await mProductionCenter.GetProductionCenterByCostCenterOid(costCenterId)
        If pCenter Is Nothing OrElse pCenter.Id = 0 Then
            Mensaje(eStatusResult.WARNING) = String.Format("No se puede Distribuir el activo {0} debido a que no se encontró centro de producción relacionado al centro de costo {1}", INDsleFixetAsset.Text, costCenterCodeName)
            DeprecationValue = 0
            INDpceAddDetail.Enabled = False
            DdbActions.Enabled = False
            FixedAssetId = Nothing
            FixedAssetCode = Nothing
            INDsleFixetAsset.Properties.NullText = ""
            Exit Function
        End If
        Dim _distributionFixedAssetDetail As New DistributionFixedAssetDetail()
        _distriutionFixedAsset.DistributionFixedAssetDetail.Add(_distributionFixedAssetDetail)
        With _distributionFixedAssetDetail
            .ProductionCenterId = pCenter.Id
            .SetProportion = 100
        End With
        ListDistributionFixedAssetDetail = _distriutionFixedAsset.DistributionFixedAssetDetail.ToList()
    End Function

    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Await ExportExcel()
    End Sub

    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await ConfirmMasive()
    End Sub

End Class