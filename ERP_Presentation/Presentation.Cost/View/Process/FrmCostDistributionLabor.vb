'***********************************************************************
' Assembly         : Presentacion.Cost
' Author           : Diego Andrés Roldán
' Created          : 01-03-2016
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
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Cost.MVP

#End Region

Public Class FrmCostDistributionLabor
    Implements ICostManpower

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrCostDistributionLabor()
        _tmpCurrentPeriod.MaximunHours = "0"
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
    End Sub

#End Region

#Region "Variables and Properties"

#Region "Variables"

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICostManpower.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostManpower.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' control de fecha
    ''' </summary>
    Private _tmpCurrentPeriod As CtrCostDistributionLabor

    ''' <summary>
    ''' presentador
    ''' </summary>
    Private _presenter As PCostManpower

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As CostSecuence

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As CostSecuence Implements ICostManpower.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As CostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As CostSecuenceDetail In Me._sequence.CostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordCost

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' parametros de costos
    ''' </summary>
    Private _settingsCost As CostSetting

    ''' <summary>
    ''' Obtiene los parámetros de costos
    ''' </summary>
    Public Property SettingsCost As CostSetting Implements ICostManpower.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As CostSetting)
            _settingsCost = value

            _tmpCurrentPeriod.SettingCost = value
            _tmpCurrentPeriod.Year = value.Year
            _tmpCurrentPeriod.Month = value.Month

            CtrDateNavigator1.SetMonth = value.Month
            CtrDateNavigator1.SetYear = value.Year
        End Set
    End Property

    Private _isLoading As Boolean

    Private _isOpenImport As Boolean

    ''' <summary>
    ''' Entidad de distribución de mano de obra
    ''' </summary>
    Private _distributionManpower As CostDistributionManpower

    ''' <summary>
    ''' Listado del detalle de distribución por mano de obra
    ''' </summary>
    Public Property ListDistributionManpowerDetail As List(Of CostDistributionManpowerDetail) Implements ICostManpower.ListDistributionManpowerDetail
        Get
            Return CType(INDgcDistributionManpowerDetail.DataSource, List(Of CostDistributionManpowerDetail))
        End Get
        Set(value As List(Of CostDistributionManpowerDetail))
            INDgcDistributionManpowerDetail.DataSource = value
        End Set
    End Property

#End Region

#Region "Properties Entity"

    ''' <summary>
    ''' Año de la distribución de mano de obra
    ''' </summary>
    Public Property Year As Integer Implements ICostManpower.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución de mano de obra
    ''' </summary>
    Public Property Month As Integer Implements ICostManpower.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto
    ''' </summary>
    Public Property Code As String Implements ICostManpower.Code
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
    ''' Obtiene o establece el id del control de distribución de mano de obra
    ''' </summary>
    Public Property ManpowerTypeId As String Implements ICostManpower.ManpowerTypeId
        Get
            Return INDsleCostDistributionManpower.EditValue
        End Get
        Set(value As String)
            INDsleCostDistributionManpower.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de Registro
    ''' </summary>
    Public Property ManpowerType As Byte Implements ICostManpower.ManpowerType

    ''' <summary>
    ''' Id del Registro
    ''' </summary>
    Public Property EntityId As Integer? Implements ICostManpower.EntityId

    ''' <summary>
    ''' Descripcion de la distribución de mano de obra
    ''' </summary>
    Public Property Description As String Implements ICostManpower.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Horas trabajadas
    ''' </summary>
    ''' <value>
    ''' The hours worked.
    ''' </value>
    Public Property HoursWorked As Integer Implements ICostManpower.HoursWorked
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
    Public Property TotalAccrued As Decimal Implements ICostManpower.TotalAccrued
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
    ''' Especifica el total de las provisiones del periodo actual
    ''' </summary>
    Public Property TotalProvision As Decimal Implements ICostManpower.TotalProvision
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
    ''' Especifica el total de los aportes patronales
    ''' </summary>
    Public Property TotalEmployerContribution As Decimal Implements ICostManpower.TotalEmployerContribution
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
    Public Property TotalParafiscal As Decimal Implements ICostManpower.TotalParafiscal
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
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As Boolean Implements ICostManpower.Status
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
    ''' Datasource de la rejilla moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyXpo As XPInstantFeedbackSource Implements ICostManpower.CurrencyXpo
        Get
            Return INDrpsleCurrency.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleCurrency.DataSource = value
        End Set
    End Property

#End Region

#Region "Datasource"

    Public Property ProductionCenterDataSource As XPInstantFeedbackSource Implements ICostManpower.ProductionCenterDataSource
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
    Public Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource Implements ICostManpower.ProductionCenterDataSourceRerpository
        Get
            Return CType(INDrpsleProductionCenter.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleProductionCenter.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Events"

#Region "Load"

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
        _presenter = New PCostManpower(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.InitializeCurrencyXpo()

        AsyncLoader(True)
        _presenter.GetSequence()
        _presenter.LoadSettingCost()
        _presenter.InitializeProductionCenter()
        AsyncLoader(False)

        Deshacer()
        LoadStatus()
        SetActionsGrid()
        SetOfficialCurrencyFormat(_presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation)
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.GuardarConfirmar, "Exportar")
    End Sub

    ''' <summary>
    ''' Establece a los controles 
    ''' la moneda parametrizada oficial
    ''' </summary>
    ''' <param name="_officialCurrencyAbbreviation"></param>
    Private Sub SetOfficialCurrencyFormat(_officialCurrencyAbbreviation As String)
        If String.IsNullOrEmpty(_officialCurrencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _officialCurrencyAbbreviation.GetNumberFormat()
        ColOfficialValue = Window.Utils.FormatGrid(ColOfficialValue, _officialCurrencyAbbreviation)
        _tmpCurrentPeriod.OfficialCurrencyAbbreviation = _officialCurrencyAbbreviation
    End Sub
    ''' <summary>
    ''' Establece el formato de la moneda origen
    ''' </summary>
    ''' <param name="_originCurrencyAbbreviation"></param>
    Private Sub SetOriginCurrencyFormat(_originCurrencyAbbreviation As String)
        If String.IsNullOrEmpty(_originCurrencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _originCurrencyAbbreviation.GetNumberFormat()
        ColTotalDistribuited = Window.Utils.FormatGrid(ColTotalDistribuited, _originCurrencyAbbreviation)
        _tmpCurrentPeriod.OriginCurrencyAbbreviation = _originCurrencyAbbreviation
        _tmpCurrentPeriod.PrintInfo()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmDistributionLabor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionLabor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _tmpCurrentPeriod = Nothing
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _sequence = Nothing
        _record = Nothing
        _settingsCost = Nothing
        _isLoading = Nothing
        _isOpenImport = Nothing
        _distributionManpower = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmDistributionLabor_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewDistributionLabor()
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

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostProductionCenter
                OpenFormLocal(formulario)
                _presenter.InitializeProductionCenter()
            End Using
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCostDistributionManpower control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostDistributionManpower_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostDistributionManpower.QueryPopUp
        If INDsleCostDistributionManpower.Properties.DataSource Is Nothing Then
            Using model As New MCostDistributionManpower(Me.Tag)
                INDsleCostDistributionManpower.Properties.DataSource = model.ViewCostDistributionManpowerByYearMonth(Me.Year, Me.Month)
            End Using
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
        If String.IsNullOrEmpty(e.NewValue) OrElse e.NewValue <= 0 Then
            e.Cancel = True
            Exit Sub
        End If

        If _distributionManpower Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        Dim costDistributionManpowerDetail As CostDistributionManpowerDetail = CType(INDgvDistributionManpowerDetail.GetFocusedRow, CostDistributionManpowerDetail)
        If costDistributionManpowerDetail Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        costDistributionManpowerDetail.HoursQuantity = e.NewValue
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCostDistributionManpower control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostDistributionManpower_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostDistributionManpower.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        If String.IsNullOrEmpty(ManpowerTypeId) Then
            INDpceAddDetail.Enabled = False
            DdbActions.Enabled = False
        Else
            Dim viewCostDistributionManpower = DirectCast(INDsleCostDistributionManpower.GetSelectedObject(), Infrastructure.Data.Xpo.CostRepository.ViewCostDistributionManpower)
            If viewCostDistributionManpower Is Nothing Then
                viewCostDistributionManpower = _presenter.GetViewCostDistributionManpowerByManpowerTypeId(ManpowerTypeId)
            End If
            ReturnValue(viewCostDistributionManpower.ManpowerTypeId, viewCostDistributionManpower)
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDrpspnHour control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDrpspnHour_EditValueChanged(sender As Object, e As EventArgs) Handles INDrpspnHour.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CalculateValueAndPercentage(True)
    End Sub

#End Region

#Region "Click"

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

#Region "MenuContext"

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _distributionLaborDetail As CostDistributionManpowerDetail = CType(INDgvDistributionManpowerDetail.GetFocusedRow(), CostDistributionManpowerDetail)
            _distributionLaborDetail.MarkAsDeleted()
            _distributionManpower.CostDistributionManpowerDetail.Remove(_distributionLaborDetail)

            Me.CalculateValueAndPercentage()
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
        INDsleCostDistributionManpower.Properties.ReadOnly = ListDistributionManpowerDetail IsNot Nothing AndAlso ListDistributionManpowerDetail.Count > 0
    End Sub

#End Region

#Region "ItemClick"

    Private Async Sub MbtnAddProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnAddProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)

            Try
                Using modelProductionCenter As New MCostProductionCenter(Me.Tag)
                    Dim listProductionCenter As List(Of CostProductionCenter) = Await modelProductionCenter.ListProductionCenter()
                    If listProductionCenter IsNot Nothing AndAlso listProductionCenter.Count > 0 Then
                        If ListDistributionManpowerDetail Is Nothing Then
                            ListDistributionManpowerDetail = New List(Of CostDistributionManpowerDetail)
                        End If

                        For Each item As CostProductionCenter In listProductionCenter
                            If ListDistributionManpowerDetail.Any(Function(d) d.ProductionCenterId = item.Id) Then
                                Continue For
                            End If

                            _distributionManpower.CostDistributionManpowerDetail.Add(New CostDistributionManpowerDetail() With {
                                                                                        .ProductionCenterId = item.Id,
                                                                                        .HoursQuantity = 0
                                                                                     })
                        Next

                        Me.CalculateValueAndPercentage()
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try

            AsyncLoader(False)
            INDlcRoot.EndUpdate()
        End If
    End Sub

    Private Sub MbtnRemoveProductionCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRemoveProductionCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _distributionManpower.CostDistributionManpowerDetail IsNot Nothing AndAlso _distributionManpower.CostDistributionManpowerDetail.Count > 0 Then
                While _distributionManpower.CostDistributionManpowerDetail.Count > 0
                    _distributionManpower.CostDistributionManpowerDetail(_distributionManpower.CostDistributionManpowerDetail.Count - 1).MarkAsDeleted()
                End While

                Me.CalculateValueAndPercentage()
            End If
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
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDistributionManpowerDetail, _listActions)
        For Each col As GridColumn In INDgvDistributionManpowerDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 120
            End If
        Next
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostManpower.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDsleCostDistributionManpower.Enabled = value
            INDtxtDescription.Enabled = value
            INDpceAddDetail.Enabled = value
            DdbActions.Enabled = value
            INDgcDistributionManpowerDetail.Enabled = value

            BarraBotones.StatusRecordVisible = value
            INDlcRoot.EndUpdate()
            If value Then
                INDsleCostDistributionManpower.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls(Optional isNavigation As Boolean = False) Implements ICostManpower.CleanControls
        INDlcRoot.BeginUpdate()

        ReadOnlyControls(False, INDlcRoot)
        If _settingsCost IsNot Nothing Then
            Year = _settingsCost.Year
            Month = _settingsCost.Month
        End If
        INDbteCode.Text = String.Empty
        ManpowerTypeId = Nothing
        INDsleCostDistributionManpower.Properties.NullText = String.Empty
        INDsleCostDistributionManpower.Properties.ReadOnly = False
        Description = Nothing

        ManpowerType = Nothing
        EntityId = Nothing

        HoursWorked = 0
        TotalAccrued = 0
        TotalEmployerContribution = 0
        TotalParafiscal = 0
        TotalProvision = 0
        ActionsOnControls = False

        CleanControlsDetail()
        ListDistributionManpowerDetail = Nothing
        INDpceAddDetail.Enabled = False
        DdbActions.Enabled = False

        Me._doc = Nothing
        _distributionManpower = Nothing
        DeleteBlockedRecord()
        _tmpCurrentPeriod.CleanControls()

        Me.BarraBotones.StatusRecord = Nothing
        If Not isNavigation Then
            Me.BarraBotones.FilterDataSource = Nothing
            Me.BarraBotones.SetPermitirNavegacion(Nothing)

            Me.BarraBotones.EnableBarItems()
            Me.BarraBotones.DisableBarDocument()
            Me.BarraBotones.CleanAuditBasic()
            Me.BarraBotones.ReassignOperatingUnit()

            If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
                Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                If FormSearchObjects.Visible Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                End If
            End If
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ModoNavegacion) = False
        End If

        INDlcRoot.EndUpdate()
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
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._distributionManpower.Code, Me._distributionManpower.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._distributionManpower.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distributionManpower.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._distributionManpower.Code, Me._distributionManpower.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._distributionManpower.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using mCommonCost As New MCommonCost(Me.Tag)
                Await mCommonCost.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Nueva distribucion de mano de obra
    ''' </summary>
    Private Async Function NewDistributionLabor() As Task(Of Boolean)
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Return False
        End If
        If Me._settingsCost Is Nothing OrElse Me._settingsCost.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Costos"
            Return False
        End If

        Me._distributionManpower = New CostDistributionManpower() With {.Status = True}
        If Me._sequence.IsManual Then
            If Me.BarraBotones.FilterDataSource Is Nothing Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    If Me.BarraBotones.FilterDataSource Is Nothing Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                    Return False
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
                        Using model As New MCommonCost(Me.Tag)
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
                            Return False
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
        Return True
    End Function

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements ICostManpower.LoadControls
        If Me._settingsCost Is Nothing OrElse Me._settingsCost.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Costos"
            Exit Sub
        End If

        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            _isLoading = True
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            If _distributionManpower Is Nothing Then
                Using model As New MCostDistributionManpower(Me.Tag)
                    Dim res = Await model.GetDistributionManpower(Me.Code)
                    If res.StatusCode <> eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Advertencia) = res.Message
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    _distributionManpower = res.ObjectEmbbeded
                End Using
            End If
            If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Id > 0 Then
                Using ModelCommonTreasury As New MCommonCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _distributionManpower.Id)
                    LoadDistribution(_distributionManpower)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._distributionManpower.Code)
                    If result.Id = 0 Then
                        Dim state = New ObjectChangeTracker
                        state.State = ObjectState.Added
                        _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _distributionManpower.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordCost(_record)
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

                    If Year <> _settingsCost.Year OrElse Month() <> _settingsCost.Month Then
                        Dim dtfi = indigo.Culture.DateTimeFormat
                        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RegisterNotInPeriod", MODULE_NAME), StrConv(dtfi.GetMonthName(_settingsCost.Month), VbStrConv.ProperCase), _settingsCost.Year)
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
                    Await Me.NewDistributionLabor()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            _isLoading = False
        End If
    End Sub

    Private Sub LoadDistribution(_distributionManpower As CostDistributionManpower)
        With _distributionManpower
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

            If .Id > 0 Then
                Code = .Code
            End If
            Description = .Description
            Month = .Month
            Year = .Year
            ManpowerType = .ManpowerType
            EntityId = .EntityId
            ManpowerTypeId = Nothing
            INDsleCostDistributionManpower.Properties.NullText = If(_distributionManpower.ThirdPartyNitName Is Nothing, _distributionManpower.Description, _distributionManpower.ThirdPartyNitName)
            Status = .Status

            HoursWorked = .HoursWorked
            TotalAccrued = .TotalAccrued
            TotalEmployerContribution = .TotalEmployerContribution
            TotalParafiscal = .TotalParafiscal
            TotalProvision = .TotalProvision

            _distributionManpower.CalculateDetailPercentages()
            _distributionManpower.CalculateTotalDistribuited(_settingsCost.CostEstimateLabor)
            GetCurrencyOriginFormatById(_distributionManpower.EntityCurrencyId)
            _tmpCurrentPeriod.TotalAccruedPatronales = .TotalDistribuited

            INDpceAddDetail.Enabled = True
            DdbActions.Enabled = True
            ListDistributionManpowerDetail = _distributionManpower.CostDistributionManpowerDetail.ToList()
            _tmpCurrentPeriod.ValueToDistributed = ListDistributionManpowerDetail.Sum(Function(x) x.TotalDistribuitedRevalued)
        End With
    End Sub

    ''' <summary>
    ''' Obtiene la moneda de Origen por Id
    ''' </summary>
    Private Async Sub GetCurrencyOriginFormatById(ByVal currencyId As Integer)
        Using model As New Common.MVP.MCurrency(CStr(Tag))
            Dim currency = Await model.GetCurrencyById(currencyId)
            If currency IsNot Nothing Then
                SetOriginCurrencyFormat(currency.Abbreviation)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements ICostManpower.AssigningValues
        With _distributionManpower
            .Year = Year
            .Month = Month
            .OperatingUnitId = Me._idOperativeUnit
            .Code = Code
            .ManpowerType = ManpowerType
            .EntityId = EntityId
            .Description = Description

            While .CostDistributionManpowerDetail.Where(Function(d) d.HoursQuantity = 0).Count > 0
                Dim _distributionLaborDetail = .CostDistributionManpowerDetail.First(Function(d) d.HoursQuantity = 0)
                _distributionLaborDetail.MarkAsDeleted()
                .CostDistributionManpowerDetail.Remove(_distributionLaborDetail)
            End While
        End With
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
        End If

        Return True
    End Function

    ''' <summary>
    ''' valida los controles de centros de producción
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsProductionCenter() As Boolean
        Dim errorList As New StringBuilder()
        If CType(INDsleProductionCenter.EditValue, Integer) = 0 Then
            errorList.AppendLine("Debe seleccionar un centro de producción")
        End If
        If CType(INDtxtMaximumAmount.EditValue, Decimal) = 0 Then
            errorList.AppendLine("Debe ingresar las horas laboradas en el centro de producción")
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Adds the direct expenses detail.
    ''' </summary>
    Private Sub AddDirectExpensesDetail()
        If ValidateProductionCenterInList(CType(INDsleProductionCenter.EditValue, Integer)) Then
            _distributionManpower.CostDistributionManpowerDetail.Add(New CostDistributionManpowerDetail() With {
                                                                        .ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer),
                                                                        .HoursQuantity = CType(INDtxtMaximumAmount.EditValue, Decimal)
                                                                     })

            Me.CalculateValueAndPercentage()
            Me.CleanControlsDetail()
        End If
    End Sub

    Private Async Function ConfirmMasive() As Task
        If MessageIndigo.Show("Desea confirmar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Function
        End If

        Me.BarraBotones.Focus()
        Try
            AsyncLoader(True)
            Using model As New MCostDistributionManpower(Me.Tag)
                Dim result = Await model.ConfirmMasive(Year, Month, Me._idOperativeUnit)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Calcula los porcentajes de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateValueAndPercentage(Optional ByVal edited As Boolean = False)
        _distributionManpower.CalculateDetails()
        _distributionManpower.CalculateTotalDistribuited(_settingsCost.CostEstimateLabor)
        Me.HoursWorked = _distributionManpower.HoursWorked
        If Not edited Then
            ListDistributionManpowerDetail = _distributionManpower.CostDistributionManpowerDetail.ToList()
        End If
        INDgcDistributionManpowerDetail.RefreshDataSource()
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

#Region "Navigation"

    Private Sub OpenNavigationMode()
        If _sequence Is Nothing OrElse _sequence.Id = 0 OrElse _sequence.IsManual Then
            Mensaje(EeventViewerImages.Advertencia) = "La secuencia debe estar configurada como automática para activar esta opción"
            Exit Sub
        End If

        Using model As New MCostDistributionManpower(Me.Tag)
            Dim costDistributionManpowers = model.GetCollectionViewCostDistributionManpowerByYearMonth(Year, Month)
            If costDistributionManpowers Is Nothing OrElse Not costDistributionManpowers.Any() Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró ningun registro para el periodo (" & Year & "/" & If(Month < 9, "0", "") & Month & ")"
                Exit Sub
            End If
            Me.BarraBotones.SetPermitirNavegacion(AddressOf ResultRecordNavigation)
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Tipo", .FieldName = "ManpowerTypeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                                          New ColumnInfo With {.Caption = "Tercero", .FieldName = "ThirdPartyNitName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                                          New ColumnInfo() With {.Caption = "Posición", .FieldName = "PositionCodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                                          New ColumnInfo() With {.Caption = "Grupo", .FieldName = "GroupCodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList()

            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            Me.BarraBotones.FilterDataSource = costDistributionManpowers
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
        End Using
    End Sub

    Public Async Function ResultRecordNavigation() As Task(Of Boolean)
        If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Status AndAlso MessageIndigo.Show("Al navegar al siguiente registro se perderán los cambios. ¿Desea " & IIf(_distributionManpower.Id = 0, "Guardar", "Actualizar") & " antes de continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Return Await GuardarTask()
        Else
            Return True
        End If
    End Function

#End Region

#Region "Export Data"

    ''' <summary>
    ''' Método que exporta los datos a excel para que el usuario mire con anterioridad como va a quedar la distribución
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ExportExcel() As Task
        Me.BarraBotones.Focus()
        Try
            AsyncLoader(True)
            Using model As New MCostDistributionManpower(Me.Tag)
                Dim result As ActionResult(Of List(Of SP_ExportExcelCostDistributionManPower_Result)) = Await model.SP_ExportExcelCostDistributionManPower(Year, Month)
                If result.ObjectEmbbeded Is Nothing OrElse result.ObjectEmbbeded.Count = 0 Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para exportar a excel"
                    Exit Function
                End If

                'Se llena el datasource de la rejilla de exportación
                INDgcExport.DataSource = Nothing
                INDgcExport.DataSource = result.ObjectEmbbeded.ToList
            End Using

            'Se exporta la vista a excel
            'Dim fileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\CostDistributionManpower.xlsx"
            Dim fileName As String = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder() + "\CostDistributionManpower.xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            INDviewExport.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

#End Region

#Region "Import Data"

    ''' <summary>
    ''' Imports the previus data.
    ''' </summary>
    Private Async Sub ImportPreviusData()
        If _isOpenImport Then
            Exit Sub
        End If

        _isOpenImport = True
        If _settingsCost IsNot Nothing Then
            Dim _previusMonth As Integer = _settingsCost.Month - 1
            Dim _previusYear As Integer = _settingsCost.Year
            If _previusMonth = 0 Then
                _previusMonth = 12
                _previusYear -= 1
            End If

            Dim _listPeriods As List(Of String)
            Using model As New MCostDistributionManpower(Me.Tag)
                _listPeriods = Await model.ListPeriodWithDataByMaximumPeriod(_previusYear, _previusMonth)
                If _listPeriods Is Nothing OrElse _listPeriods.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No hay datos de periodos anteriores para importar"
                    Exit Sub
                End If
            End Using

            Using form As New FrmCostSelectPeriod
                form.DatasourceType = FrmCostSelectPeriod.eDatasourceType.DistributionLabor
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
        _isOpenImport = False
    End Sub

    ''' <summary>
    ''' Imports the data.
    ''' </summary>
    Private Async Sub ImportData(dataImpor As List(Of CostDistributionManpower))
        Try
            Dim ImportIds As New List(Of Integer)
            dataImpor.ForEach(Sub(d) ImportIds.Add(d.Id))

            AsyncLoader(True)
            Using model As New MCostDistributionManpower(MyTag)
                Dim result = Await model.ImportCostDistributionManpower(Year, Month, Me._idOperativeUnit, ImportIds)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Me.BarraBotones.StatusRecordVisible = False
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        AddHandler FormSearchObjects.ImportPreviusData, AddressOf BarraBotones_Click_ImportarInformacion
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Tipo", .FieldName = "ManpowerTypeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Tercero", .FieldName = "ThirdPartyNitName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Cargo", .FieldName = "PositionCodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Grupo", .FieldName = "GroupCodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList
            .ValorSolicitado = "ManpowerTypeId"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostDistributionManpower
            .SearchParameters = {Year, Month}
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewDistributionLabor()
        End If
    End Sub

    ''' <summary>
    ''' Nuevoes the specified employee identifier.
    ''' </summary>
    Public Async Sub Nuevo(ManpowerType As Byte, EntityId As Integer)
        If Await Me.NewDistributionLabor() Then
            If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
                ActionsOnControls = True
                INDbteCode.Enabled = False
                INDpceAddDetail.Enabled = True
                DdbActions.Enabled = True

                _isLoading = True
                AsyncLoader(True)

                Try
                    Using model As New MCostDistributionManpower(Me.Tag)
                        Dim result = Await model.GetDistributionManpowerByYearMonth(Year, Month, ManpowerType, EntityId)
                        If Not result.StateResult Then
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            _isLoading = False
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                            _distributionManpower = result.ObjectEmbbeded.FirstOrDefault()
                            LoadDistribution(_distributionManpower)
                        End If
                    End Using
                Catch ex As Exception
                    Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                End Try

                AsyncLoader(False)
                _isLoading = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guardars this instance.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Await GuardarTask()
    End Sub

    Public Async Function GuardarTask() As Task(Of Boolean)
        Me.BarraBotones.Focus()
        If ValidateControls() = True Then
            If EntityId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tercero"
                Return False
            End If

            If ListDistributionManpowerDetail.Any() = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una información detallada"
                Return False
            End If

            If Not ListDistributionManpowerDetail.Any(Function(d) d.HoursQuantity > 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo un detalle con Horas Laboradas"
                Return False
            End If
        Else
            Return False
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Using model As New MCostDistributionManpower(Me.Tag)
                Dim result = Await model.SaveDistributionManpower(Me._distributionManpower, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _distributionManpower.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._sequence.CostSecuenceDetail(0).Id).RemoveAt(0)
                        End If
                    End If
                    Me._distributionManpower = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    If Me.BarraBotones.FilterDataSource Is Nothing Then
                        Me.Deshacer()
                    End If
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Return True
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Return False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        Status = Not Me._distributionManpower.Status
        If Not Await GuardarTask() Then
            Status = Me._distributionManpower.Status
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me._distributionManpower IsNot Nothing AndAlso Me._distributionManpower.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Using model As New MCostDistributionManpower(Me.Tag)
                        Dim result = Await model.DeleteDistributionManpower(Me._distributionManpower)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Try
            Using model As New MCostDistributionManpower(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(Year, Month, ReturnObject.ManpowerType, ReturnObject.EntityId)
                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If

                'Enviar parámetro para limpiar controles
                CleanControls(True)

                _distributionManpower = result.ObjectEmbbeded
                If _distributionManpower IsNot Nothing AndAlso _distributionManpower.Id > 0 Then
                    INDbteCode.Text = _distributionManpower.Code
                    Me.LoadControls()
                    INDbteCode.Enabled = False
                Else
                    If Me._sequence.IsManual Then
                        Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                    Else
                        Nuevo(ReturnObject.ManpowerType, ReturnObject.EntityId)
                    End If
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
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

    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await ConfirmMasive()
    End Sub

    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        Me._idEntity = String.Empty
        If FormSearchObjects IsNot Nothing Then
            FormSearchObjects.Cancelar()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
        CleanControls(True)
        Dim viewCostDistributionManpower = CType(Record, Infrastructure.Data.Xpo.CostRepository.ViewCostDistributionManpower)
        ReturnValue(viewCostDistributionManpower.ManpowerTypeId, viewCostDistributionManpower)
        Me.BarraBotones.Focus()
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

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

#End Region

End Class