'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 30-12-2014
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
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Payroll
Imports Presentation.Payroll.MVP

#End Region

Public Class FrmDistributionLabor
    Implements IManpower

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmGeneralExpenses"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrDistributionLabor()
        _tmpCurrentPeriod.MaximunHours = "0"
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
        model = New MDistributionManpower(Me.Tag)
        _presenter = New PManpower(Me)
    End Sub
#End Region

#Region "Properties and Variables"
    

#Region "Properties Entity"
    ''' <summary>
    ''' Horas trabajadas
    ''' </summary>
    ''' <value>
    ''' The hours worked.
    ''' </value>
    Public Property HoursWorked As Integer
        Get
            Return _distributionManpower.HoursWorked
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.MaximunHours = value
            If _distributionManpower IsNot Nothing Then
                _distributionManpower.HoursWorked = value
            End If
        End Set
    End Property
    ''' <summary>
    ''' Especifica el total devengado (Sin ningun tipo de deducciones) del empleado en el periodo especifico
    ''' </summary>
    Public Property TotalAccrued As Decimal Implements IManpower.TotalAccrued
        Get
            Return _distributionManpower.TotalAccrued
        End Get
        Set(value As Decimal)
            _tmpCurrentPeriod.TotalAccrued = value
            If _distributionManpower IsNot Nothing Then
                _distributionManpower.TotalAccrued = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Especifica el total de los aportes patronales
    ''' </summary>
    Public Property TotalEmployerContribution As Decimal Implements IManpower.TotalEmployerContribution
        Get
            Return _distributionManpower.TotalEmployerContribution
        End Get
        Set(value As Decimal)
            If _distributionManpower IsNot Nothing Then
                _distributionManpower.TotalEmployerContribution = value
            End If
        End Set
    End Property
    ''' <summary>
    ''' Especifica el total de los parafiscales del empleado en el periodo seleccionado
    ''' </summary>
    Public Property TotalParafiscal As Decimal Implements IManpower.TotalParafiscal
        Get
            Return _distributionManpower.TotalParafiscal
        End Get
        Set(value As Decimal)
            If _distributionManpower IsNot Nothing Then
                _distributionManpower.TotalParafiscal = value
            End If
        End Set
    End Property
    ''' <summary>
    ''' Especifica el total de las provisiones del periodo actual
    ''' </summary>
    Public Property TotalProvision As Decimal Implements IManpower.TotalProvision
        Get
            Return _distributionManpower.TotalProvision
        End Get
        Set(value As Decimal)
            If _distributionManpower IsNot Nothing Then
                _distributionManpower.TotalProvision = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto
    ''' </summary>
    Public Property Code As String Implements IManpower.Code
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
    ''' Descripcion de la distribución de mano de obra
    ''' </summary>
    Public Property Description As String Implements IManpower.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    Public Property EmployeeId As Integer Implements IManpower.EmployeeId
        Get
            Return INDsleEmployee.EditValue
        End Get
        Set(value As Integer)
            INDsleEmployee.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución de mano de obra
    ''' </summary>
    Public Property Month As Integer Implements IManpower.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Año de la distribución de mano de obra
    ''' </summary>
    Public Property Year As Integer Implements IManpower.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As String Implements IManpower.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property
#End Region

#Region "Datasource"
    ''' <summary>
    ''' Datasource de los empleados
    ''' </summary>
    Public Property EmployeeDatasource As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollEmployeeXpo) Implements IManpower.EmployeeDatasource
        Get
            Return CType(INDsleEmployee.Properties.DataSource, XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollEmployeeXpo))
        End Get
        Set(value As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollEmployeeXpo))
            INDsleEmployee.Properties.DataSource = value
        End Set
    End Property

    Public Property ProductionCenterDataSource As XPInstantFeedbackSource Implements IManpower.ProductionCenterDataSource
        Get
            Return CType(INDsleProductionCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProductionCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los centros de producción para el repositorio de la rejilla
    ''' </summary>
    Public Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource Implements IManpower.ProductionCenterDataSourceRerpository
        Get
            Return CType(INDrpsleProductionCenter.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleProductionCenter.DataSource = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"

    ''' <summary>
    ''' parametros de costos
    ''' </summary>
    Private _settingsCost As InteropCostSetting

    ''' <summary>
    ''' control de fecha
    ''' </summary>
    Private _tmpCurrentPeriod As CtrDistributionLabor

    Private model As MDistributionManpower

    ''' <summary>
    ''' Listado del detalle de distribución por mano de obra
    ''' </summary>
    Public Property ListDistributionManpowerDetail As List(Of DistributionManpowerDetail) Implements IManpower.ListDistributionManpowerDetail
        Get
            Return CType(INDgcDistributionManpowerDetail.DataSource, List(Of DistributionManpowerDetail))
        End Get
        Set(value As List(Of DistributionManpowerDetail))
            INDgcDistributionManpowerDetail.DataSource = value
        End Set
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
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInteropCost

    ''' <summary>
    ''' Entidad de distribución de mano de obra
    ''' </summary>
    Private _distributionManpower As DistributionManpower

    ''' <summary>
    ''' presentador
    ''' </summary>
    Private _presenter As PManpower

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IManpower.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IManpower.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As Domain.Entities.InteropCostSecuence Implements IManpower.Sequence
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
    ''' Obtiene los parámetros de costos
    ''' </summary>
    Public Property SettingsCost As Domain.Entities.InteropCostSetting Implements IManpower.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As Domain.Entities.InteropCostSetting)
            _tmpCurrentPeriod.SettingCost = value
            _tmpCurrentPeriod.Year = value.Year
            _tmpCurrentPeriod.Month = value.Month


            CtrDateNavigator1.SetMonth = value.Month
            CtrDateNavigator1.SetYear = value.Year
            '_tmpCurrentPeriod.LoadDate()
            _settingsCost = value
            If value IsNot Nothing AndAlso value.Id Then
                If value.CostEstimateLabor = 1 Then
                    'Devengado
                    ColDistributeValueAccrued.Visible = True
                    ColDistributeValueAccrued.Width = 300
                    ColDistributeValueAccrued.VisibleIndex = 3
                Else
                    'Devengado mas patronales
                    ColDistributeValueAccruedPatronus.Visible = True
                    ColDistributeValueAccruedPatronus.Width = 300
                    ColDistributeValueAccruedPatronus.VisibleIndex = 3
                End If
                INDcolPercentage.VisibleIndex = 4
            End If
        End Set
    End Property

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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IManpower.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleEmployee.Enabled = value
            INDgcDistributionManpowerDetail.Enabled = value

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
        _settingsCost = Nothing
        _tmpCurrentPeriod = Nothing
        model = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        _distributionManpower = Nothing
        _presenter = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmDistributionLabor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionLabor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        'IndigoGridControl1.RefreshGrid(INDgcDistributionManpowerDetail)
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDistributionManpowerDetail, _listActions)
        For Each col As GridColumn In INDgvDistributionManpowerDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next

        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        _presenter.InitializeProductionCenter()
        _presenter.LoadSettingCost()

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmDistributionLabor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionLabor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Shown"
    Private Sub FrmDistributionLabor_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
    Private Sub FrmDistributionLabor_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
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
                    Me.NewDistributionLabor()
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

    Private Sub INDrpspnHour_KeyDown(sender As Object, e As KeyEventArgs) Handles INDrpspnHour.KeyDown
        CalculatePercentage()
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
        If Me._distributionManpower IsNot Nothing AndAlso Me._distributionManpower.Id > 0 Then
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

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleEmployee control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEmployee_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEmployee.QueryPopUp
        If INDsleEmployee.Properties.DataSource Is Nothing Then
            INDsleEmployee.Properties.DataSource = model.ListEmployeeWithActiveContract()
        End If
    End Sub
#End Region

#Region "EditvalueChanging"
    Private Sub INDrpsleProductionCenter_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpsleProductionCenter.EditValueChanging
        If Not ValidateProductionCenterInList(e.NewValue) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDrpspnHour_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpspnHour.EditValueChanging
        If Not String.IsNullOrEmpty(e.NewValue) Then
            If Not ValidateMaximumHours(CDec(e.NewValue), CDec(INDgvDistributionManpowerDetail.GetFocusedRow().HoursQuantity)) Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
        CalculatePercentage()
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDrpspnHour_EditValueChanged(sender As Object, e As EventArgs) Handles INDrpspnHour.EditValueChanged
        'INDgvDistributionManpowerDetail.GetFocusedRow().HoursQuantity = CType(e.NewValue, Decimal)
        'INDgcDistributionManpowerDetail.RefreshDataSource()
        CalculatePercentage()
    End Sub
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEmployee control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEmployee.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If
        If EmployeeId <> 0 Then
            AsyncLoader(True)
            Await LoadMaximumHours(True)
            AsyncLoader(False)
        Else
            INDpceAddDetail.Enabled = False
            DdbActions.Enabled = False
        End If
    End Sub
#End Region

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDirectCostDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDistributionManpowerDetail.DataSourceChanged
        INDsleEmployee.Properties.ReadOnly = ListDistributionManpowerDetail IsNot Nothing AndAlso ListDistributionManpowerDetail.Count > 0
        CalculatePercentage()
    End Sub
#End Region

#Region "ItemClick"
    Private Async Sub MbtnAddProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnAddProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using modelProductionCenter As New MProductionCenter(Me.Tag)
                INDlcRoot.BeginUpdate()
                AsyncLoader(True)
                Dim listProductionCenter As List(Of ProductionCenter) = Await modelProductionCenter.ListProductionCenter()
                If listProductionCenter IsNot Nothing AndAlso listProductionCenter.Count > 0 Then
                    If ListDistributionManpowerDetail IsNot Nothing AndAlso ListDistributionManpowerDetail.Count > 0 Then
                        For Each item As DistributionManpowerDetail In ListDistributionManpowerDetail
                            listProductionCenter.Remove(listProductionCenter.Where(Function(x) x.Id = item.ProductionCenterId).FirstOrDefault())
                        Next
                    End If
                    For Each item As ProductionCenter In listProductionCenter
                        Dim _distributionManpowerDetail As New DistributionManpowerDetail()
                        _distributionManpower.DistributionManpowerDetail.Add(_distributionManpowerDetail)
                        With _distributionManpowerDetail
                            .ProductionCenterId = item.Id
                            .SetHoursQuantity = 0
                        End With
                    Next
                    ListDistributionManpowerDetail = _distributionManpower.DistributionManpowerDetail.ToList()
                    INDgcDistributionManpowerDetail.RefreshDataSource()
                End If
                AsyncLoader(False)
                INDlcRoot.EndUpdate()
            End Using
        End If
    End Sub

    Private Sub MbtnRemoveProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRemoveProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _distributionManpower.DistributionManpowerDetail IsNot Nothing AndAlso _distributionManpower.DistributionManpowerDetail.Count > 0 Then
                While _distributionManpower.DistributionManpowerDetail.Count > 0
                    _distributionManpower.DistributionManpowerDetail(_distributionManpower.DistributionManpowerDetail.Count - 1).MarkAsDeleted()
                End While
                ListDistributionManpowerDetail = _distributionManpower.DistributionManpowerDetail.ToList()
                INDgcDistributionManpowerDetail.RefreshDataSource()
            End If
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
            Dim _distributionLaborDetail As DistributionManpowerDetail = CType(INDgvDistributionManpowerDetail.GetFocusedRow(), DistributionManpowerDetail)
            _distributionLaborDetail.MarkAsDeleted()
            If _distributionManpower.ChangeTracker.State <> ObjectState.Added Then
                _distributionManpower.MarkAsModified()
            End If
            ListDistributionManpowerDetail = _distributionManpower.DistributionManpowerDetail.ToList()
            INDgcDistributionManpowerDetail.RefreshDataSource()
        End If
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

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEmployee control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmEmployee
                OpenFormLocal(formulario)
                model.ListEmployeeWithActiveContract()
            End Using
        End If
    End Sub

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

    ''' <summary>
    ''' Handles the Click event of the INDsbAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateControlsProductionCenter() Then
            AddDirectExpensesDetail()
        End If
    End Sub
#End Region
#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que exporta los datos a excel para que el usuario mire con anterioridad como va a quedar la distribución
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ExportExcel() As Task
        Dim resultList = ReturnList()
        If resultList.StateResult = False Then
            Exit Function
        End If

        Dim ListIds = resultList.ObjectEmbbeded

        Me.BarraBotones.Focus()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of List(Of SP_ExportExcelInteropCostDistributionManPower_Result)) = Await model.SP_ExportExcelInteropCostDistributionManPower(ListIds, Year, Month, BarraBotones.OperatingUnit.Id)
            If result.ObjectEmbbeded Is Nothing OrElse result.ObjectEmbbeded.Count = 0 Then
                AsyncLoader(False)
                Mensaje(eStatusResult.WARNING) = "No se encontraron datos para exportar a excel"
                Exit Function
            End If

            'Se llena el datasource de la rejilla de exportación
            INDgcExport.DataSource = Nothing
            INDgcExport.DataSource = result.ObjectEmbbeded.ToList

            'Se exporta la vista a excel
            'Dim fileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\DistributionInteropCost.xlsx"
            Dim fileName As String = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder() + "\DistributionInteropCost.xlsx"
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
    ''' Retorna el listado de ids que son necesarios para las consultas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReturnList() As ActionResult(Of List(Of Tuple(Of Integer, Integer)))
        'Se consulta la liquidación de nómina de los empleados del mes y año correspondientes
        Dim listLiquidationIds As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollLiquidation) = model.GetCollectionLiquidationByYearMont(Year, Month)
        If listLiquidationIds Is Nothing OrElse Not listLiquidationIds.Any() Then 'Se valida que exista información en lo consultado
            Mensaje(eStatusResult.WARNING) = "No se encontró nómina para el periodo (" & Year & "/" & If(Month < 9, "0", "") & Month & ")"
            Return New ActionResult(Of List(Of Tuple(Of Integer, Integer))) With {.StateResult = False}
        End If
        'Se recorre el listado consultado para poder sacar el id de la liquidación y los ids de los empleados y enviarlos para confirmar
        'Item1 = Id de la liquidación
        'Item2 = Id del empleado
        Dim ListIds As New List(Of Tuple(Of Integer, Integer))
        For Each itemXpo As Infrastructure.Data.Xpo.PayrollRepository.PayrollLiquidation In listLiquidationIds
            ListIds.Add(New Tuple(Of Integer, Integer)(itemXpo.Id, itemXpo.EmployeeId.Id))
        Next

        'Se valida que hayan escogido una unidad operativa
        If BarraBotones.OperatingUnit Is Nothing OrElse BarraBotones.OperatingUnit.Id = Nothing Then
            Mensaje(eStatusResult.WARNING) = "Debe seleccionar una unidad operativa"
            Return New ActionResult(Of List(Of Tuple(Of Integer, Integer))) With {.StateResult = False}
        End If

        Return New ActionResult(Of List(Of Tuple(Of Integer, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListIds}
    End Function

    Public Sub CleanControlsNavigation()
        INDlcRoot.BeginUpdate()

        ReadOnlyControls(False, INDlcRoot)
        INDbteCode.Text = String.Empty
        Description = Nothing
        EmployeeId = Nothing

        INDpceAddDetail.Enabled = False
        DdbActions.Enabled = False


        TotalAccrued = 0
        TotalProvision = 0
        TotalEmployerContribution = 0
        TotalParafiscal = 0
        ActionsOnControls = False

        ListDistributionManpowerDetail = Nothing
        INDsleEmployee.Properties.NullText = String.Empty
        _tmpCurrentPeriod.MaximunHours = 0
        CleanControlsDetail()
        INDsleEmployee.Properties.ReadOnly = False

        _distributionManpower = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing

        INDlcRoot.EndUpdate()
    End Sub

    Public Async Function ResultRecordNavigation() As Task(Of Boolean)
        '_distributionManpower.ChangeTracker.State <> ObjectState.Unchanged AndAlso
        If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Status = False AndAlso MessageIndigo.Show("Al navegar al siguiente registro se perderán los cambios. Desea " & IIf(_distributionManpower.Id = 0, "Guardar", "Actualizar") & " antes de continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
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
        Dim listLiquidationIds As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.PayrollLiquidation) = model.GetCollectionLiquidationByYearMont(Year, Month)
        If listLiquidationIds Is Nothing OrElse Not listLiquidationIds.Any() Then
            Mensaje(eStatusResult.WARNING) = "No se encontró nomina para el periodo (" & Year & "/" & If(Month < 9, "0", "") & Month & ")"
            Exit Sub
        End If
        Me.BarraBotones.SetPermitirNavegacion(AddressOf ResultRecordNavigation)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Nit", .FieldName = "EmployeeId.ThirdPartyId.Nit"}, New ColumnInfo() With {.Caption = "Empleado", .FieldName = "EmployeeId.ThirdPartyId.Name"},
                                      New ColumnInfo() With {.Caption = "Grupo", .FieldName = "GroupId.CodeName"}, New ColumnInfo() With {.Caption = "Fecha de Liquidación", .FieldName = "PayrollDateLiquidated"}}.ToList()
        Me.BarraBotones.FilterDataSource = listLiquidationIds
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GuardarConfirmar, "Exportar")
    End Sub

    Private Async Function ConfirmMasive() As Task
        Dim resultList = ReturnList()
        If resultList.StateResult = False Then
            Exit Function
        End If

        Dim ListIds = resultList.ObjectEmbbeded

        'Se pregunta si se quiere confirmar
        If MessageIndigo.Show("Desea confirmar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Function
        End If

        Me.BarraBotones.Focus()
        Try
            AsyncLoader(True)
            Dim result = Await model.ConfirmMasiveInteropcost(ListIds, Year, Month, BarraBotones.OperatingUnit.Id)
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
    ''' Nueva distribucion de mano de obra
    ''' </summary>
    Private Async Sub NewDistributionLabor()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        _presenter.LoadSettingCost()
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        Me._distributionManpower = New DistributionManpower()
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
    ''' Opens the form.
    ''' </summary>
    ''' <param name="form">The form.</param>
    Private Sub OpenFormLocal(ByVal form As FormBase)
        form.ViewModeEditHold = True
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.MinimizeBox = False
        form.MaximizeBox = False
        form.Size = New Size(950, 700)
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Dim transparent As New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Valida el valor de los agregados contra el valor contable
    ''' </summary>
    ''' <param name="oldValue">valor anterior (solo pasa cuando se modifica de la rejilla)</param>
    ''' <returns></returns>
    Private Function ValidateMaximumHours(maximumAmmount As Decimal, Optional oldValue As Decimal = 0) As Boolean
        If ListDistributionManpowerDetail IsNot Nothing AndAlso ListDistributionManpowerDetail.Count > 0 Then
            If InteropCostStaticService.CalculateDistributedValue(ListDistributionManpowerDetail.Sum(Function(x) x.HoursQuantity) - oldValue, maximumAmmount) > _tmpCurrentPeriod.MaximunHours Then
                Mensaje(EeventViewerImages.Advertencia) = "Las Horas a Distribuir no pueden superar las horas máximas laboradas"
                Return False
            End If
        Else
            If maximumAmmount > _tmpCurrentPeriod.MaximunHours Then
                Mensaje(EeventViewerImages.Advertencia) = "Las Horas a Distribuir no pueden superar las horas máximas laboradas"
                Return False
            End If
        End If
        Return True
    End Function

    Private Async Sub LoadDistribution(_liquidation As Domain.Payroll.Entities.Liquidation)
        If _distributionManpower Is Nothing Then
            _distributionManpower = New DistributionManpower()
        End If
        Using mPayrollLiquidation As New MPayrollLiquidation
            'Consultar patronales
            Dim patronalesValue As Decimal = Await mPayrollLiquidation.GetPatronalesValueAsync(EmployeeId, _tmpCurrentPeriod.Year, _tmpCurrentPeriod.Month)

            HoursWorked = _liquidation.DaysWorked * _liquidation.Contract.HoursDaily '_liquidation.Contract.Position.MaxHourAmount
            TotalAccrued = _liquidation.TotalAccrued
            TotalProvision = _liquidation.ProvisionsValue
            TotalEmployerContribution = patronalesValue
            TotalParafiscal = _liquidation.ParafiscalContribution

            _tmpCurrentPeriod.TotalAccruedPatronales = _liquidation.TotalAccrued + _liquidation.ProvisionsValue + patronalesValue + _liquidation.ParafiscalContribution
        End Using
        If _liquidation.Contract.Position.HandlesTurnsChart Then
            'Buscar en Schedule (cuadro de turno)
            Dim period As String = String.Concat(If(Month < 10, "0" & Month, Month), "/", Year)
            Using modelSchedule As New MSchedule
                Dim schedule As List(Of Domain.Payroll.Entities.Schedule) = Await modelSchedule.GetScheduleByEmployeePeriodAsync(_liquidation.EmployeeId, period)
                If schedule IsNot Nothing AndAlso schedule.Count > 0 Then
                    If schedule.Where(Function(x) x.FunctionalUnit.ProductionCenterId IsNot Nothing).Sum(Function(x) x.TotalHour) > 0 Then
                        HoursWorked = schedule.Where(Function(x) x.FunctionalUnit.ProductionCenterId IsNot Nothing).Sum(Function(x) x.TotalHour)
                    End If
                    Dim errorList As New StringBuilder()
                    Dim hours As Integer = 0
                    For Each item As Domain.Payroll.Entities.Schedule In schedule
                        If item.FunctionalUnit.ProductionCenterId Is Nothing Then
                            errorList.AppendLine(String.Format("La unidad funcional {0} no tiene parametrizado centro de producción", String.Concat(item.FunctionalUnit.Code, " - ", item.FunctionalUnit.Name)))
                            Continue For
                        End If
                        If item.TotalHour = 0 Then
                            hours = _liquidation.DaysWorked * _liquidation.Contract.HoursDaily
                        Else
                            hours = item.TotalHour
                        End If
                        GenerateProductionCenterDetail(item.FunctionalUnit.ProductionCenterId, hours, _liquidation)
                    Next
                    If errorList.Length > 0 Then
                        HoursWorked = 0
                        TotalAccrued = 0
                        TotalProvision = 0
                        TotalEmployerContribution = 0
                        TotalParafiscal = 0
                        _tmpCurrentPeriod.TotalAccruedPatronales = 0
                        INDpceAddDetail.Enabled = False
                        DdbActions.Enabled = False

                        Mensaje(eStatusResult.WARNING) = String.Format("No se puede liquidar el empleado {0} por: " & errorList.ToString(), INDsleEmployee.Text)
                        Exit Sub
                    End If
                    INDpceAddDetail.Enabled = True
                    DdbActions.Enabled = True
                    INDgcDistributionManpowerDetail.RefreshDataSource()
                Else
                    GenerateProductionCenterDetail(_liquidation.Contract.FunctionalUnit.ProductionCenterId, _liquidation.DaysWorked * _liquidation.Contract.HoursDaily, _liquidation)
                    INDpceAddDetail.Enabled = True
                    DdbActions.Enabled = True
                End If
            End Using
        Else
            'Tomo horas maximas
            GenerateProductionCenterDetail(_liquidation.Contract.FunctionalUnit.ProductionCenterId, _liquidation.DaysWorked * _liquidation.Contract.HoursDaily, _liquidation)
            INDpceAddDetail.Enabled = True
            DdbActions.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Loads the maximum hours.
    ''' </summary>
    Private Async Function LoadMaximumHours(loadDetailEmployee As Boolean) As Task
        Using mPayrollLiquidation As New MPayrollLiquidation()
            Dim _liquidation As Domain.Payroll.Entities.Liquidation = Await mPayrollLiquidation.GetLiquidationByEmployeeIdYearMonth(EmployeeId, _tmpCurrentPeriod.Year, _tmpCurrentPeriod.Month)
            If _liquidation IsNot Nothing AndAlso _liquidation.Id > 0 AndAlso _liquidation.Contract IsNot Nothing AndAlso _liquidation.Contract.Id > 0 Then

                If loadDetailEmployee Then
                    LoadDistribution(_liquidation)
                Else
                    INDpceAddDetail.Enabled = True
                    DdbActions.Enabled = True
                End If

            Else
                _tmpCurrentPeriod.MaximunHours = 0
                DdbActions.Enabled = False
                INDpceAddDetail.Enabled = False
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El empleado ({0}) no tiene liquidaciones confirmadas", INDsleEmployee.Text)
            End If
        End Using
    End Function

    ''' <summary>
    ''' Imports the data.
    ''' </summary>
    Private Async Sub ImportData(dataImpor As List(Of DistributionManpower))
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
                For Each item As DistributionManpower In dataImpor
                    Dim _distrib As New DistributionManpower()
                    With _distrib
                        .EmployeeId = item.EmployeeId
                        '.Description = item.Description
                        .Month = Month
                        .Year = Year
                        .HoursWorked = item.HoursWorked
                        .TotalAccrued = item.TotalAccrued
                        .TotalProvision = item.TotalProvision
                        .TotalEmployerContribution = item.TotalEmployerContribution
                        .TotalParafiscal = item.TotalParafiscal
                        .Status = item.Status
                        For Each itemDetail As DistributionManpowerDetail In item.DistributionManpowerDetail
                            Dim _distribDetail As New DistributionManpowerDetail()
                            .DistributionManpowerDetail.Add(_distribDetail)
                            With _distribDetail
                                .ProductionCenterId = itemDetail.ProductionCenterId
                                .HoursQuantity = itemDetail.HoursQuantity
                                .TotalAccrued = itemDetail.TotalAccrued
                                .TotalEmployerContribution = itemDetail.TotalEmployerContribution
                                .TotalAccruedEmployerContribution = itemDetail.TotalAccruedEmployerContribution
                                .TotalParafiscal = itemDetail.TotalParafiscal
                                .TotalProvision = itemDetail.TotalProvision
                            End With
                        Next
                    End With
                    Dim result = Await model.SaveDistributionManpower(_distrib, Me._idCurrentSequence)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionManpowerImportCorrect", MODULE_NAME), item.FullNameEmployee), 1))
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-001" Then
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionManpowerImportAdvertence", MODULE_NAME), item.FullNameEmployee), 3))
                        Else
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionManpowerImportError", MODULE_NAME), item.FullNameEmployee, result.Message), 2))
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
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), IIf(Me._distributionManpower IsNot Nothing, Me._distributionManpower.Code, ""), IIf(Me._distributionManpower IsNot Nothing, Me._distributionManpower.Description, "")),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._distributionManpower.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distributionManpower.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), IIf(Me._distributionManpower IsNot Nothing, Me._distributionManpower.Code, ""), IIf(Me._distributionManpower IsNot Nothing, Me._distributionManpower.Description, ""))
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distributionManpower.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

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
            Using form As New FrmSelectPeriod
                form.DatasourceType = FrmSelectPeriod.eDatasourceType.DistributionLabor
                form.PreviusMonth = _previusMonth
                form.PreviusYear = _previusYear
                form.LisPeriods = _listPeriods
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                form.MinimizeBox = False
                form.MaximizeBox = False
                AddHandler form.AddImportData, AddressOf ImportData
                'form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                Dim transparent As New FrmTransparent(form, False)
                transparent.ShowDialog(Me)
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron parámetros de costos"
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IManpower.AssigningValues
        With _distributionManpower
            .Code = Code
            '.Description = Description
            .EmployeeId = EmployeeId
            .Year = Year
            .Month = Month

            'Por definir
            'Total horas trabajadas
            .HoursWorked = HoursWorked
            'total de valores devengados
            .TotalAccrued = TotalAccrued
            'Total provision
            .TotalProvision = TotalProvision
            'Aportes
            .TotalEmployerContribution = TotalEmployerContribution
            'Total parafiscales
            .TotalParafiscal = TotalParafiscal
            .Status = If(Status Is Nothing, False, CType(Status, Boolean))
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IManpower.CleanControls
        INDlcRoot.BeginUpdate()

        ReadOnlyControls(False, INDlcRoot)
        INDbteCode.Text = String.Empty
        Description = Nothing
        EmployeeId = Nothing

        INDpceAddDetail.Enabled = False
        DdbActions.Enabled = False


        TotalAccrued = 0
        TotalProvision = 0
        TotalEmployerContribution = 0
        TotalParafiscal = 0
        Me.BarraBotones.SetPermitirNavegacion(Nothing)
        ActionsOnControls = False

        ListDistributionManpowerDetail = Nothing
        INDsleEmployee.Properties.NullText = String.Empty
        _tmpCurrentPeriod.MaximunHours = 0
        CleanControlsDetail()
        INDsleEmployee.Properties.ReadOnly = False

        Me.BarraBotones.FilterDataSource = Nothing

        _distributionManpower = Nothing
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
    ''' Limpia los controles del detalle del gasto directo
    ''' </summary>
    Private Sub CleanControlsDetail()
        INDsleProductionCenter.EditValue = Nothing
        INDtxtMaximumAmount.EditValue = 0
        INDsleProductionCenter.Focus()
    End Sub

    ''' <summary>
    ''' Adds the direct expenses detail.
    ''' </summary>
    Private Sub AddDirectExpensesDetail()
        If ValidateProductionCenterInList(CType(INDsleProductionCenter.EditValue, Integer)) Then
            If ValidateMaximumHours(CType(INDtxtMaximumAmount.EditValue, Decimal)) Then
                Dim _directExpenseDetail As New DistributionManpowerDetail()
                _distributionManpower.DistributionManpowerDetail.Add(_directExpenseDetail)
                With _directExpenseDetail
                    .ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer)
                    .SetHoursQuantity = CType(INDtxtMaximumAmount.EditValue, Decimal)
                    '.HoursQuantity = CType(INDtxtMaximumAmount.EditValue, Decimal)

                    '.TotalAccrued = .GetTotalValue(_tmpCurrentPeriod.MaximunHours, TotalAccrued)
                    '.TotalProvision = .GetTotalValue(_tmpCurrentPeriod.MaximunHours, TotalProvision)
                    '.TotalEmployerContribution = .GetTotalValue(_tmpCurrentPeriod.MaximunHours, TotalEmployerContribution)
                    '.TotalParafiscal = .GetTotalValue(_tmpCurrentPeriod.MaximunHours, TotalParafiscal)
                End With
                ListDistributionManpowerDetail = _distributionManpower.DistributionManpowerDetail.ToList()
                INDgcDistributionManpowerDetail.RefreshDataSource()
                CleanControlsDetail()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida que el centro de produccion a agregar no esté ya agregado
    ''' </summary>
    Private Function ValidateProductionCenterInList(productionCenterId As Integer) As Boolean
        If ListDistributionManpowerDetail IsNot Nothing AndAlso ListDistributionManpowerDetail.Count > 0 Then
            If ListDistributionManpowerDetail.Where(Function(x) x.ProductionCenterId = productionCenterId).Count() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductionCenterRepeat", MODULE_NAME)
                Return False
            End If
            'If INDsleProductionCenter.EditValue IsNot Nothing Then
            '    If ListDistributionManpowerDetail.Where(Function(x) x.ProductionCenterId = productionCenterId).Count() > 0 Then
            '        Return False
            '    End If
            'Else
            '    Dim _productCenter As DistributionManpowerDetail = CType(INDgvDistributionManpowerDetail.GetFocusedRow(), DistributionManpowerDetail)
            '    If ListDistributionManpowerDetail.Where(Function(x) x.ProductionCenterId = CType(_productCenter.ProductionCenterId, Integer)).Count() > 1 Then
            '        Return False
            '    End If
            'End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements IManpower.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            _isLoading = True
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            If _distributionManpower Is Nothing Then
                Dim res = Await model.GetDistributionManpower(Me.Code)
                If res.StatusCode <> eStatusResult.SUCCESS Then
                    Mensaje(res.StatusCode) = res.Message
                    AsyncLoader(False)
                    Exit Sub
                End If
                _distributionManpower = res.ObjectEmbbeded
            End If
            If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Id > 0 Then
                Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, _distributionManpower.Id)
                    With _distributionManpower
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        'Description = .Description
                        Month = .Month
                        Year = .Year
                        EmployeeId = .EmployeeId
                        If .Status Then
                            Status = "1"
                        Else
                            Status = "0"
                        End If
                        'Como estas variables las lee de la entidad entonces no es necesario volverlas a asignar
                        'TotalAccrued = .TotalAccrued
                        'TotalProvision = .TotalProvision
                        'TotalEmployerContribution = .TotalEmployerContribution
                        'TotalParafiscal = .TotalParafiscal

                    End With

                    'Await LoadMaximumHours(False)

                    _tmpCurrentPeriod.TotalAccrued = _distributionManpower.TotalAccrued
                    _tmpCurrentPeriod.TotalAccruedPatronales = _distributionManpower.TotalAccrued + _distributionManpower.TotalEmployerContribution + _distributionManpower.TotalProvision + _distributionManpower.TotalParafiscal
                    _tmpCurrentPeriod.MaximunHours = _distributionManpower.HoursWorked

                    INDpceAddDetail.Enabled = True
                    DdbActions.Enabled = True

                    '_tmpCurrentPeriod.LoadDate()
                    INDsleEmployee.Properties.NullText = _distributionManpower.FullNameEmployee
                    ListDistributionManpowerDetail = _distributionManpower.DistributionManpowerDetail.ToList()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._distributionManpower.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _distributionManpower.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If

                    If Me.BarraBotones.FilterDataSource Is Nothing Then
                        Me.BarraBotones.SetDocuments(_distributionManpower.Id, MyTag, Nothing, GetType(DistributionManpower).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    End If
                    If _distributionManpower.Status = True Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                    Dim _settingsCostQ As InteropCostSetting
                    Using ModelSetting As New MInteropCostSetting(Me.Tag)
                        _settingsCostQ = ModelSetting.GetInteropCostSetting()
                    End Using
                    If Year() <> _settingsCostQ.Year OrElse Month() <> _settingsCostQ.Month Then
                        Dim dtfi = indigo.Culture.DateTimeFormat
                        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RegisterNotInPeriod", MODULE_NAME), Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(_settingsCostQ.Month), Microsoft.VisualBasic.VbStrConv.ProperCase), _settingsCostQ.Year)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, "")
                        ReadOnlyControls(True, INDlcRoot)
                    End If

                    AsyncLoader(False)
                    INDlcRoot.EndUpdate()
                    ActionsOnControls = True
                    INDbteCode.Enabled = False
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End Using
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewDistributionLabor()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            _isLoading = False
        End If
    End Sub

    ''' <summary>
    ''' Calcula los porcentajes de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculatePercentage()
        If ListDistributionManpowerDetail IsNot Nothing AndAlso ListDistributionManpowerDetail.Count > 0 Then
            Dim totalValue As Decimal
            If ColDistributeValueAccrued.Visible Then
                totalValue = ListDistributionManpowerDetail.Sum(Function(item) item.TotalAccrued)
                ListDistributionManpowerDetail.ForEach(Sub(item)
                                                           item.PercentageDistribution = Utils.CalculatePercentage(totalValue, item.TotalAccrued)
                                                       End Sub)
            ElseIf ColDistributeValueAccruedPatronus.Visible Then
                totalValue = ListDistributionManpowerDetail.Sum(Function(item) item.TotalAccruedEmployerContribution)
                ListDistributionManpowerDetail.ForEach(Sub(item)
                                                           item.PercentageDistribution = Utils.CalculatePercentage(totalValue, item.TotalAccruedEmployerContribution)
                                                       End Sub)
            End If
            INDgcDistributionManpowerDetail.RefreshDataSource()
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
        If Me._distributionManpower IsNot Nothing AndAlso Me._distributionManpower.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteDistributionManpower(Me._distributionManpower)
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
    ''' Guardars this instance.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Me.BarraBotones.Focus()
        If ValidateControls() = True Then
            If INDgvDistributionManpowerDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una información detallada"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveDistributionManpower(Me._distributionManpower, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _distributionManpower.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._distributionManpower = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                If Me.BarraBotones.FilterDataSource Is Nothing Then
                    Me.Deshacer()
                Else
                    'Code = _distributionManpower.Code
                    '_distributionManpower = Nothing
                    'LoadControls()
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
            If INDgvDistributionManpowerDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una información detallada"
                Return False
            End If
        Else
            Return False
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveDistributionManpower(Me._distributionManpower, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _distributionManpower.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._distributionManpower = result.ObjectEmbbeded
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
                Dim result = Await model.UpdateStateDistributionManpower(Me._distributionManpower.Code, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._distributionManpower = result.ObjectEmbbeded
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
            Me.NewDistributionLabor()
        End If
    End Sub

    Private _isLoading As Boolean

    ''' <summary>
    ''' Nuevoes the specified employee identifier.
    ''' </summary>
    Public Async Sub Nuevo(LiquidationId As Integer)
        Me.NewDistributionLabor()
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 AndAlso _settingsCost IsNot Nothing AndAlso _settingsCost.Id > 0 Then
            ActionsOnControls = True
            INDbteCode.Enabled = False
            INDpceAddDetail.Enabled = True
            DdbActions.Enabled = True
            _isLoading = True
            AsyncLoader(True)
            Using mPayrollLiquidation As New MPayrollLiquidation
                Dim _liquidation As Domain.Payroll.Entities.Liquidation = Await mPayrollLiquidation.GetLiquidationById(LiquidationId)
                If _liquidation IsNot Nothing AndAlso _liquidation.Id > 0 AndAlso _liquidation.Contract IsNot Nothing AndAlso _liquidation.Contract.Id > 0 Then
                    EmployeeId = _liquidation.EmployeeId
                    INDsleEmployee.Properties.NullText = _liquidation.FullNameEmployee

                    LoadDistribution(_liquidation)
                End If
            End Using
            AsyncLoader(False)
            _isLoading = False
        End If
    End Sub

    ''' <summary>
    ''' Loads the detail with out schedule.
    ''' </summary>
    ''' <param name="liquidation">The liquidation.</param>
    Private Sub GenerateProductionCenterDetail(productionCenterId As Integer?, totalHours As Integer, liquidation As Domain.Payroll.Entities.Liquidation)
        If productionCenterId Is Nothing Then
            HoursWorked = 0
            TotalAccrued = 0
            TotalProvision = 0
            TotalEmployerContribution = 0
            TotalParafiscal = 0
            _tmpCurrentPeriod.TotalAccruedPatronales = 0
            INDpceAddDetail.Enabled = False
            DdbActions.Enabled = False
            Mensaje(eStatusResult.WARNING) = String.Format("No se puede liquidar el empleado {0} debido a que la unidad funcional {1} no tiene parametrizado un centro de producción ", INDsleEmployee.Text, String.Concat(liquidation.Contract.FunctionalUnit.Code, " - ", liquidation.Contract.FunctionalUnit.Name))
            Exit Sub
        End If

        Dim _directExpenseDetail As New DistributionManpowerDetail()
        _distributionManpower.DistributionManpowerDetail.Add(_directExpenseDetail)
        With _directExpenseDetail
            .ProductionCenterId = productionCenterId.Value
            .SetHoursQuantity = totalHours
        End With
        ListDistributionManpowerDetail = _distributionManpower.DistributionManpowerDetail.ToList()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Me.BarraBotones.StatusRecordVisible = False
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        AddHandler FormSearchObjects.ImportPreviusData, AddressOf BarraBotones_Click_ImportarInformacion
        _presenter.LoadSettingCost()
        If SettingsCost Is Nothing OrElse SettingsCost.Id = 0 Then
            Exit Sub
        End If
        Dim parameters As Object() = {Year, Month}
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Nit", .FieldName = "EmployeeId.ThirdPartyId.Nit", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Empleado", .FieldName = "EmployeeId.ThirdPartyId.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Grupo", .FieldName = "GroupId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Fecha de Liquidación", .FieldName = "PayrollDateLiquidated", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Id" '.ValorSolicitado = "EmployeeId.Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListLiquidationByYearMont
            .SearchParameters = parameters
            .ImportDataButton = True
            .FormParent = Me
            .ShowSearch()
        End With
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
        'Consultamos en la tabla de distribución por el id del empleado y si no encuentra entonces nos vamos a las tablas de nomina
        _distributionManpower = (Await model.GetDistributionManpowerByEmployeeIdAndYearMonth(Convert.ToInt32(ReturnObject.EmployeeId.Id), _tmpCurrentPeriod.Year, _tmpCurrentPeriod.Month)).ObjectEmbbeded
        DeleteBlockedRecord()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Id > 0 Then
            INDbteCode.Text = _distributionManpower.Code
            If INDbteCode.Text <> String.Empty Then
                LoadControls()
            End If
        Else
            If Me._sequence.IsManual Then
                Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            Else
                Nuevo(Convert.ToInt32(ReturnValue))
            End If
        End If
    End Sub
#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Evento que se utiliza para el exportar información
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Await ExportExcel()
    End Sub

    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        'Bloquear por si el servidor esta lento no se reviente
        Me._idEntity = String.Empty
        If FormSearchObjects IsNot Nothing Then
            FormSearchObjects.Cancelar()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
        CleanControlsNavigation()
        Dim payrollLiquidationXpo = CType(Record, Infrastructure.Data.Xpo.PayrollRepository.PayrollLiquidation)
        INDtxtDescription.Text = payrollLiquidationXpo.EmployeeId.ThirdPartyId.NitName
        'INDsleEmployee.EditValue = payrollLiquidationXpo.EmployeeId.Id
        'INDsleEmployee.Properties.NullText = payrollLiquidationXpo.EmployeeId.ThirdPartyId.NitName
        ReturnValue(payrollLiquidationXpo.Id, payrollLiquidationXpo)
        Me.BarraBotones.Focus()
    End Sub

    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await ConfirmMasive()
    End Sub

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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
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
    ''' Barras the botones_ click_ modo navegacion.
    ''' </summary>
    Private Sub BarraBotones_Click_ModoNavegacion() Handles BarraBotones.Click_ModoNavegacion
        OpenNavigationMode()
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
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub
#End Region

End Class