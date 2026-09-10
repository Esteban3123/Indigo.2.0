'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 29-02-2016
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

Public Class FrmCostGeneralExpenses
	Implements ICostGeneralExpenses

#Region "Builder"

	Public Sub New()
		InitializeComponent()

	End Sub

#End Region

#Region "Properties"

	''' <summary>
	''' Obtiene o establece el codigo del gasto
	''' </summary>
	Public Property Code As String Implements ICostGeneralExpenses.Code
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
	''' Obtiene o establece el nombre del gasto
	''' </summary>
	Public Property NameGeneralExpense As String Implements ICostGeneralExpenses.NameGeneralExpense
		Get
			Return INDtxtName.Text
		End Get
		Set(value As String)
			INDtxtName.Text = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el estado
	''' </summary>
	Public Property Status As Boolean Implements ICostGeneralExpenses.Status
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
	''' Base de distribución
	''' </summary>
	Public Property MultipleBase As Byte Implements ICostGeneralExpenses.MultipleBase
		Get
			Return CType(INDgleDistribution.EditValue, Byte)
		End Get
		Set(value As Byte)
			INDgleDistribution.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Id de la categoria
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property CategoryId As Integer Implements ICostGeneralExpenses.CategoryId
		Get
			Return INDsleCategory.EditValue
		End Get
		Set(value As Integer)
			INDsleCategory.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Datasource de la categoria
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property CategoryXpo As XPCollection Implements ICostGeneralExpenses.CategoryXpo
		Get
			Return INDsleCategory.Properties.DataSource
		End Get
		Set(value As XPCollection)
			INDsleCategory.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Tipo de costo
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property ElementCostType As Byte Implements ICostGeneralExpenses.ElementCostType
		Get
			Return INDSleElementCostType.EditValue
		End Get
		Set(value As Byte)
			INDSleElementCostType.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Tipo de gasto
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property ExpenseType As Byte Implements ICostGeneralExpenses.ExpenseType
		Get
			Return INDsleExpenseType.EditValue
		End Get
		Set(value As Byte)
			INDsleExpenseType.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Tipo de gasto
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property DistributionType As Byte Implements ICostGeneralExpenses.DistributionType
		Get
			Return INDSleDistributionType.EditValue
		End Get
		Set(value As Byte)
			INDSleDistributionType.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Indica si se debe generar Cuenta por Pagar
	''' </summary>
	''' <returns></returns>
	Public Property GenerateAccountPayable As Boolean?
		Get
			Return Convert.ToBoolean(INDSleGenerateAccountPayable.EditValue)
		End Get
		Set(value As Boolean?)
			INDSleGenerateAccountPayable.EditValue = value
		End Set
	End Property

#End Region

#Region "Variables"

	''' <summary>
	''' Constante que contiene  el nombre del modulo
	''' </summary>
	Public Const MODULE_NAME As String = "Cost"

	''' <summary>
	''' parametros de costos
	''' </summary>
	Private _settingsCost As CostSetting

	''' <summary>
	''' Parámetros de costos
	''' </summary>
	Public Property SettingsCost As CostSetting Implements ICostGeneralExpenses.SettingsCost
		Get
			Return _settingsCost
		End Get
		Set(value As CostSetting)
			_settingsCost = value
		End Set
	End Property

	''' <summary>
	''' Gets my layout control.
	''' </summary>
	Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICostGeneralExpenses.MyLayoutControl
		Get
			Return Me.LayoutControls
		End Get
	End Property

	''' <summary>
	''' Obtiene el tag del frontal
	''' </summary>
	Public ReadOnly Property MyTag As Object Implements ICostGeneralExpenses.MyTag
		Get
			Return Me.Tag
		End Get
	End Property

	''' <summary>
	''' Gets or sets the sequence.
	''' </summary>
	Public Property Sequence As CostSecuence Implements ICostGeneralExpenses.Sequence
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
	''' opciones de distribución
	''' </summary>
	Private _distributionOptions As List(Of Tuple(Of Integer, Tuple(Of Image, String)))

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

	''' <summary>
	''' variable que contiene el presentador
	''' </summary>
	Dim _presenter As PCostGeneralExpenses

	Private model As MCostGeneralExpenses

	''' <summary>
	''' entidad de gastos generales
	''' </summary>
	Private _generalExpenses As CostGeneralExpense

	Private _listDistributionOptions As List(Of Tuple(Of Integer, String))

	''' <summary>
	''' entidad que almacena el registro bloqueado
	''' </summary>
	Dim _record As BlockRecordCost

	''' <summary>
	''' Listado del detalle de base de distribución
	''' </summary>
	Property ListDistributionBase As List(Of CostDistributionBase)
		Get
			Return CType(INDgcDsitribution.DataSource, List(Of CostDistributionBase))
		End Get
		Set(value As List(Of CostDistributionBase))
			INDgcDsitribution.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Propiedad que almacena el tipo de Comprobante Contable
	''' </summary>
	''' <returns></returns>
	Property DirectLaborDistribution As Integer?
		Get
			Return INDSleDirectLaborDistribution.EditValue
		End Get
		Set(value As Integer?)
			INDSleDirectLaborDistribution.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Propiedad que almacena el tipo de Comprobante Contable para Reversión
	''' </summary>
	''' <returns></returns>
	Property ReversalDirectLaborDistribution As Integer?
		Get
			Return INDSleReversalDirectLaborDistribution.EditValue
		End Get
		Set(value As Integer?)
			INDSleReversalDirectLaborDistribution.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Establece el Datasource del Tipo de Comprobante Contable
	''' </summary>
	''' <returns></returns>
	Public Property DirectLaborDistributionJournalVoucherTypeXpo As XPInstantFeedbackSource
		Get
			Return INDSleDirectLaborDistribution.Properties.DataSource
		End Get
		Set(value As XPInstantFeedbackSource)
			INDSleDirectLaborDistribution.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Establece el Datasource del Tipo de Comprobante Contable para la reversión
	''' </summary>
	''' <returns></returns>
	Public Property ReversalDirectLaborDistributionJournalVoucherTypeXpo As XPInstantFeedbackSource
		Get
			Return INDSleReversalDirectLaborDistribution.Properties.DataSource
		End Get
		Set(value As XPInstantFeedbackSource)
			INDSleReversalDirectLaborDistribution.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Listado de los centros de produccón a los cuales se les va a hacer la distribución
	''' </summary>
	Private ListDistributionBaseDetail As List(Of CostDistributionBaseDetail)

	''' <summary>
	''' Esta propiedad se utiliza para Registrar en el visor de eventos
	''' </summary>
	Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostGeneralExpenses.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDSleElementCostType.Enabled = value
            INDSleGenerateAccountPayable.Enabled = value
            INDSleDirectLaborDistribution.Enabled = value
            INDSleReversalDirectLaborDistribution.Enabled = value
            INDsleExpenseType.Enabled = value
            INDsleCategory.Enabled = value
            INDgleDistribution.Enabled = value
            INDsbAddDistribution.Enabled = value
            INDgcDsitribution.Enabled = value
            INDSleDistributionType.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
    Private CurrencyAbbreviation As String

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
		_settingsCost = Nothing
		_distributionOptions = Nothing
		_idOperativeUnit = Nothing
		_sequence = Nothing
		_idCurrentSequence = Nothing
		_presenter = Nothing
		model = Nothing
		_generalExpenses = Nothing
		_listDistributionOptions = Nothing
		_record = Nothing
	End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmGeneralExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmGeneralExpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        model = New MCostGeneralExpenses(Me.Tag)

        INDlygJournalVoucherGrouping.HideControl
        IndigoGridControl1.RefreshGrid(INDgcDsitribution)
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        _listActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvDistributionBase, _listActions)
        For Each col As GridColumn In INDgvDistributionBase.Columns
            If col.Name = "colActions" Then
                col.Width = 80
            End If
        Next
        _presenter = New PCostGeneralExpenses(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        InitializeTuples()
        CreateDistributionOptions()
        LoadStatus()
        Deshacer()
        CurrencyAbbreviation = _presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation
        SetCurrencyFormat(CurrencyAbbreviation)
    End Sub

    ''' <summary>
    ''' Establece el formato de moneda en los controles del formulario.
    ''' </summary>
    ''' <param name="_currencyAbbreviation">Abreviación de la moneda.</param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmGeneralExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmGeneralExpenses_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		DeleteBlockedRecord()
	End Sub

#End Region

#Region "Shown"

	Private Sub FrmGeneralExpenses_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
		If INDbteCode.Enabled Then
			INDbteCode.Focus()
		End If
		_presenter.InitializeCategory()
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
					Me.NewGeneralExpenses()
				Else
					LoadControls()
				End If
			End If
		ElseIf e.KeyCode = Keys.F4 Then
			OpenSearch()
		End If
	End Sub

#End Region

#Region "ButtonClick"

	Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			OpenForm("602", Nothing, True)
		End If
	End Sub

#End Region

#Region "Double Click"

	''' <summary>
	''' Handles the DoubleClick event of the INDgcDsitribution control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
	Private Sub INDgcDsitribution_DoubleClick(sender As Object, e As EventArgs) Handles INDgcDsitribution.DoubleClick
		EditDistributionBase()
	End Sub

#End Region

#Region "QueryPopup"

	''' <summary>
	''' Evento que se dispara al desplegar el control de categoria
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDsleCategory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCategory.QueryPopUp
		If CategoryXpo Is Nothing Then

		End If
	End Sub

#End Region

#Region "Click"

	''' <summary>
	''' Handles the Click event of the INDsbAddDistribution control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
	Private Sub INDsbAddDistribution_Click(sender As Object, e As EventArgs) Handles INDsbAddDistribution.Click
		OpenFormDistributionBase(False)
	End Sub

#End Region

#Region "Actions"

	''' <summary>
	''' Handles the ButtonAction event of the IndigoGridView1_Click control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
	Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
		Select Case (sender.Tag)
			Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
				EditDistributionBase()
			Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
				If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
					DeleteDistributionBase()
				End If
		End Select
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
		If Me._generalExpenses IsNot Nothing AndAlso Me._generalExpenses.Id > 0 Then
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

#End Region

#Region "Methods"

	''' <summary>
	''' Inicializa las tuplas
	''' </summary>
	''' <remarks></remarks>
	Private Sub InitializeTuples()
		Dim _listElementCostType As New List(Of Tuple(Of Integer, String))
		_listElementCostType.Add(New Tuple(Of Integer, String)(1, "Mano de obra Directa"))
		_listElementCostType.Add(New Tuple(Of Integer, String)(2, "Mano de obra Indirecta"))
		_listElementCostType.Add(New Tuple(Of Integer, String)(3, "Materiales Directos"))
		_listElementCostType.Add(New Tuple(Of Integer, String)(4, "Materiales Indirectos"))
		_listElementCostType.Add(New Tuple(Of Integer, String)(5, "Otros Gastos"))
		INDSleElementCostType.Properties.DataSource = _listElementCostType

		Dim _expenseType As New List(Of Tuple(Of Integer, String))()
		_expenseType.Add(New Tuple(Of Integer, String)(1, "Fijo"))
		_expenseType.Add(New Tuple(Of Integer, String)(2, "Variable"))
		INDsleExpenseType.Properties.DataSource = _expenseType


		Dim _DistributionType As New List(Of Tuple(Of Byte, String))
		_DistributionType.Add(New Tuple(Of Byte, String)(1, "Distribución estándar"))
		_DistributionType.Add(New Tuple(Of Byte, String)(2, "Distribución mano obra"))
		_DistributionType.Add(New Tuple(Of Byte, String)(3, "Distribución gastos generales"))
		_DistributionType.Add(New Tuple(Of Byte, String)(4, "Distribución productos"))
		INDSleDistributionType.Properties.DataSource = _DistributionType

		Dim _generateAccountPayable As New List(Of Tuple(Of Byte, String))
		_generateAccountPayable.Add(New Tuple(Of Byte, String)(1, "Sí"))
		_generateAccountPayable.Add(New Tuple(Of Byte, String)(0, "No"))
		INDSleGenerateAccountPayable.Properties.DataSource = _generateAccountPayable
	End Sub

	''' <summary>
	''' News the general expenses.
	''' </summary>
	Private Async Sub NewGeneralExpenses()
		If Sequence Is Nothing OrElse Sequence.Id = 0 Then
			Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
			Exit Sub
		End If
		Using mSettings As New MCostSetting(Me.MyTag)
			_settingsCost = Await mSettings.GetCostSettingAsync()
			If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
				Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", MODULE_NAME)
				Exit Sub
			End If
			Me.SettingsCost = _settingsCost
		End Using
		Me._generalExpenses = New CostGeneralExpense() With {.Status = True}
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
	''' Validates the main accounts.
	''' </summary>
	''' <returns></returns>
	Private Async Function ValidateMainAccounts() As Task(Of Boolean)
		'Dim generalExp As CostGeneralExpense = Await model.GetCostGeneralExpenseByMainAccountId(MainAccountId)
		'If generalExp IsNot Nothing AndAlso generalExp.Id > 0 AndAlso generalExp.Id <> _generalExpenses.Id Then
		'    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("MainAccountUsedByGeneralExpense", MODULE_NAME), generalExp.MainAccountNumber, String.Concat(generalExp.Code, " - ", generalExp.Name))
		'    Return False
		'End If
		Return True
	End Function

	''' <summary>
	''' Abre el formulario de base distribución
	''' </summary>
	Private Sub OpenFormDistributionBase(ByVal edit As Boolean, Optional ByVal distribBase As CostDistributionBase = Nothing)
		If edit OrElse ValidateDistributionBase() Then
			Dim costGeneralExpense = _generalExpenses.CloneEntity()

			Dim frmDistributionBase As New PopUpCostDistributionBase()
            With frmDistributionBase
                .FormBorderStyle = FormBorderStyle.Sizable
                .FormParentName = PopUpCostDistributionBase.eParent.GeneralExpenses
                .INDliDistributionType.Enabled = Me.DistributionType = 1


                If costGeneralExpense.CostDistributionBase IsNot Nothing AndAlso costGeneralExpense.CostDistributionBase.Count > 0 Then
                    .EditMode = edit
                    .CountDistributionBase = costGeneralExpense.CostDistributionBase.Count
                    .ListDistributionBaseDetail = costGeneralExpense.CostDistributionBase(0).CostDistributionBaseDetail.ToList()
                    .ListDistributionBaseMeasurementUnit = costGeneralExpense.CostDistributionBase(0).CostDistributionBaseMeasurementUnit.ToList()
                End If
                .CurrencyAbbreviation = CurrencyAbbreviation
                If edit Then
                    .LoadDistributionBase(distribBase.CloneEntity())
                Else
                    .MultipleBase = MultipleBase
                End If
            End With
            AddHandler frmDistributionBase.AddDistributionBase, AddressOf AddDistributionBase
			OpenFormLocal(frmDistributionBase)
		End If
	End Sub

	''' <summary>
	''' Adds the distribution base.
	''' </summary>
	''' <param name="distributionBase">The distribution base.</param>
	Private Sub AddDistributionBase(ByVal edit As Boolean, distributionBase As CostDistributionBase)
		distributionBase.DistributionBaseName = String.Format(ResourceManager.GetString("DistributionBaseName", MODULE_NAME), _listDistributionOptions(distributionBase.MultipleBase - 1).Item2)
		If edit Then
			Dim costDistributionBase = _generalExpenses.CostDistributionBase.Where(Function(b) b.MultipleBase = distributionBase.MultipleBase).FirstOrDefault
			costDistributionBase.DistributionType = distributionBase.DistributionType
			costDistributionBase.DistributionTypeName = distributionBase.DistributionTypeName
			costDistributionBase.ImpactPoints = distributionBase.ImpactPoints
			costDistributionBase.MeasurementUnit = distributionBase.MeasurementUnit
			costDistributionBase.MeasureUnitName = distributionBase.MeasureUnitName
			costDistributionBase.Description = distributionBase.Description
			costDistributionBase.Area = distributionBase.Area
			costDistributionBase.OfficialHours = distributionBase.OfficialHours
			costDistributionBase.SupplyValue = distributionBase.SupplyValue
			costDistributionBase.WorkmanshipValue = distributionBase.WorkmanshipValue
			costDistributionBase.AssetValue = distributionBase.AssetValue
			costDistributionBase.Sales = distributionBase.Sales

			For Each item In distributionBase.CostDistributionBaseDetail
				Dim costDistributionBaseDetail = costDistributionBase.CostDistributionBaseDetail.Where(Function(d) d.ProductionCenterId = item.ProductionCenterId AndAlso d.MainAccountId = item.MainAccountId AndAlso ((d.CostCenterId Is Nothing AndAlso item.CostCenterId Is Nothing) OrElse (d.CostCenterId = item.CostCenterId))).FirstOrDefault()
				If costDistributionBaseDetail Is Nothing Then
					costDistributionBaseDetail = New CostDistributionBaseDetail()
					costDistributionBase.CostDistributionBaseDetail.Add(costDistributionBaseDetail)
				End If

				costDistributionBaseDetail.ProductionCenterId = item.ProductionCenterId
				costDistributionBaseDetail.MainAccountId = item.MainAccountId
				costDistributionBaseDetail.CodeNameMainAccount = item.CodeNameMainAccount
				costDistributionBaseDetail.CostCenterId = item.CostCenterId
				costDistributionBaseDetail.CodeNameCostCenter = item.CodeNameCostCenter
				costDistributionBaseDetail.Quantity = item.Quantity
			Next

			For Each item In distributionBase.CostDistributionBaseMeasurementUnit
				Dim costDistributionBaseMeasurementUnit = costDistributionBase.CostDistributionBaseMeasurementUnit.Where(Function(d) d.MeasurementUnitId = item.MeasurementUnitId).FirstOrDefault()
				If costDistributionBaseMeasurementUnit Is Nothing Then
					costDistributionBaseMeasurementUnit = New CostDistributionBaseMeasurementUnit()
					costDistributionBaseMeasurementUnit.MeasurementUnitId = item.MeasurementUnitId
					costDistributionBaseMeasurementUnit.CodeNameMeasureUnit = item.CodeNameMeasureUnit
					costDistributionBase.CostDistributionBaseMeasurementUnit.Add(costDistributionBaseMeasurementUnit)
				End If
			Next
		Else
			_generalExpenses.CostDistributionBase.Add(distributionBase)
		End If

		ListDistributionBase = _generalExpenses.CostDistributionBase.ToList()
		INDgcDsitribution.RefreshDataSource()
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
		transparent.ShowDialog(Me)
	End Sub

	''' <summary>
	''' Validates the distribution base.
	''' </summary>
	''' <returns></returns>
	Private Function ValidateDistributionBase() As Boolean
		If ListDistributionBase IsNot Nothing AndAlso ListDistributionBase.Count > 0 Then
			Dim numeroMaximo As Integer = ListDistributionBase.Max(Function(x) x.MultipleBase)
			If numeroMaximo = 4 Then
				Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DistributionBaseComplete", MODULE_NAME)
				Return False
			End If

			If ListDistributionBase(0).DistributionType = Presentation.Cost.PopUpCostDistributionBase.eDistributionType.Direct Then
				Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("GeneralExpenseNoAddDistributionBase", MODULE_NAME)
				Return False
			End If

			If MultipleBase <= numeroMaximo OrElse (MultipleBase - numeroMaximo) <> 1 Then
				Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectDistributionBaseType", MODULE_NAME), _listDistributionOptions.ElementAt(numeroMaximo).Item2)
				Return False
			End If
		Else
			If MultipleBase <> eMultipleBase.DistributionA Then
				Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitializeDistributionBase", MODULE_NAME)
				Return False
			End If
		End If
		Return True
	End Function

	''' <summary>
	''' Creates the distribution options.
	''' </summary>
	Private Sub CreateDistributionOptions()
		_distributionOptions = New List(Of Tuple(Of Integer, Tuple(Of Image, String)))()
		_distributionOptions.Add(New Tuple(Of Integer, Tuple(Of Image, String))(1, New Tuple(Of Image, String)(ImageCollection1.Images.Item(0), "Distribución (1)")))
		_distributionOptions.Add(New Tuple(Of Integer, Tuple(Of Image, String))(2, New Tuple(Of Image, String)(ImageCollection1.Images.Item(1), "Distribución (2)")))
		_distributionOptions.Add(New Tuple(Of Integer, Tuple(Of Image, String))(3, New Tuple(Of Image, String)(ImageCollection1.Images.Item(2), "Distribución (3)")))
		_distributionOptions.Add(New Tuple(Of Integer, Tuple(Of Image, String))(4, New Tuple(Of Image, String)(ImageCollection1.Images.Item(3), "Distribución (4)")))

		_listDistributionOptions = New List(Of Tuple(Of Integer, String))()
		_listDistributionOptions.Add(New Tuple(Of Integer, String)(1, "1"))
		_listDistributionOptions.Add(New Tuple(Of Integer, String)(2, "2"))
		_listDistributionOptions.Add(New Tuple(Of Integer, String)(3, "3"))
		_listDistributionOptions.Add(New Tuple(Of Integer, String)(4, "4"))

		'INDgleDistribution.Properties.DataSource = _distributionOptions
	End Sub

	''' <summary>
	''' Cargamos los estados de la barra
	''' </summary>
	Private Sub LoadStatus()
		Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
	End Sub

	''' <summary>
	''' Elimina el registro bloqueado
	''' </summary>
	Public Async Sub DeleteBlockedRecord()
		If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
			Using ModelCommonInteropCost As New MCommonCost(Me.Tag)
				Await ModelCommonInteropCost.DeleteBlockRecordCost(_record)
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
				.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._generalExpenses.Code, Me._generalExpenses.Name),
				.CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
				.IdEntity = "$#" & Me.Tag & "_" & Me._generalExpenses.Code & "#$", .IdForm = Me.Tag,
				.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._generalExpenses.Code),
				.Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
		Else
			Me._doc.Update = dateServer
			Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
			Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._generalExpenses.Code, Me._generalExpenses.Name)
			Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._generalExpenses.Code)
		End If
		Return Me._doc
	End Function

	''' <summary>
	''' Asigna los valores a los campos de la entidad
	''' </summary>
	Public Sub AssigningValues() Implements ICostGeneralExpenses.AssigningValues
		With _generalExpenses
			.Code = Code
			.Name = NameGeneralExpense
			.CostGeneralExpenseCategoryId = INDsleCategory.EditValue
			.ElementCostType = INDSleElementCostType.EditValue
			If DistributionType <> 2 Or ElementCostType <> 1 Then
				.GenerateAccountPayable = Nothing
				.JournalVoucherTypesId = Nothing
				.ReversalJournalVoucherTypesId = Nothing
			Else
				.GenerateAccountPayable = GenerateAccountPayable
				If Not GenerateAccountPayable Then
					.JournalVoucherTypesId = DirectLaborDistribution
					.ReversalJournalVoucherTypesId = ReversalDirectLaborDistribution
				Else
					.JournalVoucherTypesId = Nothing
					.ReversalJournalVoucherTypesId = Nothing
				End If
			End If
			.ExpenditureType = INDsleExpenseType.EditValue
			.DistributionType = DistributionType
		End With
	End Sub

	''' <summary>
	''' Limpia los controles
	''' </summary>
	Public Sub CleanControls() Implements ICostGeneralExpenses.CleanControls
		INDlcRoot.BeginUpdate()

		ReadOnlyControls(False, INDlcRoot)
		INDbteCode.Text = String.Empty
		INDtxtName.Text = String.Empty
		ElementCostType = Nothing
		GenerateAccountPayable = 1
		DirectLaborDistribution = Nothing
		ReversalDirectLaborDistribution = Nothing
		ExpenseType = Nothing
		DistributionType = Nothing
		CategoryId = Nothing
		INDsleCategory.Properties.NullText = String.Empty
		INDgleDistribution.EditValue = Nothing
		ActionsOnControls = False
		ListDistributionBase = Nothing
		_generalExpenses = Nothing
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
		End If

		INDlyItemGenerateAccountPayable.HideControl
		INDlcRoot.EndUpdate()
	End Sub

	''' <summary>
	''' Carga los datos en los controles
	''' </summary>
	Public Async Sub LoadControls() Implements ICostGeneralExpenses.LoadControls
		If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
			If Me.BarraBotones.PermiteConsultar = False Then
				Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
				Exit Sub
			End If
			INDlcRoot.BeginUpdate()
			AsyncLoader(True)
			Dim resGeneralExpense = Await model.GetCostGeneralExpense(Me.Code)
			If resGeneralExpense.StatusCode <> eStatusResult.SUCCESS Then
				Mensaje(resGeneralExpense.StatusCode) = resGeneralExpense.Message
				AsyncLoader(False)
				Exit Sub
			End If
			_generalExpenses = resGeneralExpense.ObjectEmbbeded
			If _generalExpenses IsNot Nothing AndAlso _generalExpenses.Id > 0 Then
				Using mCommon As New MCommonCost(Me.Tag)
					Dim result = Await mCommon.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _generalExpenses.Id)
					With _generalExpenses
						Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
						Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
						Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
						Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
						Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
						Code = .Code
						NameGeneralExpense = .Name
						ElementCostType = .ElementCostType
						GenerateAccountPayable = IIf(.GenerateAccountPayable.IsNull, True, .GenerateAccountPayable)
						If Not .GenerateAccountPayable AndAlso .JournalVoucherTypesId Is Nothing AndAlso .ReversalJournalVoucherTypesId Is Nothing Then
							Me.Mensaje(EeventViewerImages.Advertencia) = "Debe parametrizar el segmento de Comprobantes Contables"
						End If
                        DirectLaborDistribution = .JournalVoucherTypesId
                        INDSleDirectLaborDistribution.Properties.NullText = .DirectLaborDistributionCodeName
                        ReversalDirectLaborDistribution = .ReversalJournalVoucherTypesId
                        INDSleReversalDirectLaborDistribution.Properties.NullText = .ReversalDirectLaborDistributionCodeName
                        INDsleCategory.Properties.NullText = .CategoryCodeName
						INDsleCategory.EditValue = .CostGeneralExpenseCategoryId
						ExpenseType = .ExpenditureType
						Status = .Status
						DistributionType = .DistributionType
					End With

					ListDistributionBase = _generalExpenses.CostDistributionBase.ToList()

					Me.GetDocumentIndexed(Me.Tag & "_" & Me._generalExpenses.Code)
					If result.Id = 0 Then
						Dim state = New Domain.Base.Entities.ObjectChangeTracker
						state.State = Domain.Base.Entities.ObjectState.Added
						_record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _generalExpenses.Id}
						Dim operation = Await mCommon.SaveBlockRecordCost(_record)
						_record = operation.ObjectEmbbeded
					Else
						_record = result
						Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
						Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
					End If
					Me.BarraBotones.SetDocuments(_generalExpenses.Id, MyTag, Nothing, GetType(CostGeneralExpense).Name)
					Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

					Dim _settingsCostQ As CostSetting
					Using mSettings As New MCostSetting(Me.Tag)
						_settingsCostQ = mSettings.GetCostSetting()
					End Using
					AsyncLoader(False)
					ActionsOnControls = True
				End Using
			Else
				AsyncLoader(False)
				If Me._sequence.IsManual Then
					Me.NewGeneralExpenses()
				Else
					Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
					Deshacer()
				End If
			End If
			INDgleDistribution.SelectedIndex = 0
			INDlcRoot.EndUpdate()
		End If
	End Sub

	''' <summary>
	''' Deletes the distribution base.
	''' </summary>
	Private Sub DeleteDistributionBase()
		Dim _distribBase As CostDistributionBase = CType(INDgvDistributionBase.GetFocusedRow(), CostDistributionBase)
		If ListDistributionBase.Where(Function(x) x.MultipleBase > _distribBase.MultipleBase).Count() > 0 Then
			Mensaje(EeventViewerImages.Advertencia) = String.Format("Debe seleccionar una Distribución con una Base de Tipo ({0})", _listDistributionOptions.ElementAt(_distribBase.MultipleBase).Item2)
		Else
			'Marca como eliminado los detalles si existen
			If _distribBase.CostDistributionBaseDetail IsNot Nothing AndAlso _distribBase.CostDistributionBaseDetail.Count > 0 Then
				While _distribBase.CostDistributionBaseDetail.Count > 0
					_distribBase.CostDistributionBaseDetail.Item(_distribBase.CostDistributionBaseDetail.Count() - 1).MarkAsDeleted()
				End While
			End If
			While _distribBase.CostDistributionBaseMeasurementUnit.Count > 0
				_distribBase.CostDistributionBaseMeasurementUnit(0).MarkAsDeleted()
			End While
			_distribBase.MarkAsDeleted()
			If _generalExpenses.ChangeTracker.State <> ObjectState.Added Then
				_generalExpenses.MarkAsModified()
			End If
			ListDistributionBase = _generalExpenses.CostDistributionBase.ToList()
			INDgcDsitribution.RefreshDataSource()
		End If
	End Sub

	''' <summary>
	''' Edits the distribution base.
	''' </summary>
	Private Sub EditDistributionBase()
		Dim _distribBase As CostDistributionBase = CType(INDgvDistributionBase.GetFocusedRow(), CostDistributionBase)
		OpenFormDistributionBase(True, _distribBase)
	End Sub

	Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

	End Sub

#End Region

#Region "ICrud"

	''' <summary>
	''' METODO: Item buscar del control de usuarios.
	''' </summary>
	Public Sub Buscar() Implements ICrudBase.Buscar
		OpenSearch()
	End Sub

	''' <summary>
	''' METODO: Item Deshacer del control de usuarios.
	''' </summary>
	Public Sub Deshacer() Implements ICrudBase.Deshacer
		CleanControls()
	End Sub

	''' <summary>
	''' METODO: Item Eliminar del control de usuarios.
	''' </summary>
	Public Async Sub Eliminar() Implements ICrudBase.Eliminar
		If Me._generalExpenses IsNot Nothing AndAlso Me._generalExpenses.Id > 0 Then
			If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
				Try
					AsyncLoader(True)
					Dim result = Await model.DeleteCostGeneralExpense(Me._generalExpenses)
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
	Public Async Sub Guardar() Implements ICrudBase.Guardar
		Dim errorList As New StringBuilder()

		If DistributionType = 0 Then
			errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de Distribución"))
		End If

		If ElementCostType = 0 Then
			errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de Costo"))
		End If
		If ExpenseType = 0 Then
			errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Clase de costo"))
		End If
		If CategoryId = 0 Then
			errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Categoría"))
		End If

		If INDgvDistributionBase.RowCount = 0 Then
			errorList.AppendLine("Debe agregar mínimo una base de distribución")
		End If

		If GenerateAccountPayable IsNot Nothing AndAlso Not GenerateAccountPayable Then
			If DirectLaborDistribution Is Nothing OrElse ReversalDirectLaborDistribution Is Nothing Then
				errorList.AppendLine("Debe parametrizar el segmento de Comprobantes Contables")
			End If
		End If
		If errorList.Length > 0 Then
			Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
			Exit Sub
		End If

		If Not ValidateControls() Then
			Exit Sub
		End If

		AssigningValues()
		Try
			AsyncLoader(True)
			Dim result As ActionResult(Of CostGeneralExpense) = Await model.SaveCostGeneralExpense(Me._generalExpenses, Me._idCurrentSequence)
			If result.StatusCode = eStatusResult.SUCCESS Then
				If _generalExpenses.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
					If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
						Me.DicSequense(Me._sequence.CostSecuenceDetail(0).Id).RemoveAt(0)
					End If
				End If
				Me._generalExpenses = result.ObjectEmbbeded
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
			Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
		End Try
	End Sub

	''' <summary>
	''' METODO: Item Nuevo del control de usuarios.
	''' </summary>
	Public Sub Nuevo() Implements ICrudBase.Nuevo
		If _sequence Is Nothing OrElse _sequence.Id = 0 Then
			Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
			Exit Sub
		End If
		If Me._sequence.IsManual Then
			Deshacer()
		Else
			Me.NewGeneralExpenses()
			INDgleDistribution.SelectedIndex = 0
		End If
	End Sub

	''' <summary>
	''' este metodo abre el frontal de busqueda
	''' </summary>
	Public Sub OpenSearch() Implements ICrudBase.OpenSearch
		FormSearchObjects = New FrmBusqueda
		AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
		With FormSearchObjects
			.ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33},
							  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.34},
							  New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33}}.ToList
			.ValorSolicitado = "Code"
			.ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostGeneralExpenses
			.FormParent = Me
			.ShowSearch()
		End With
	End Sub

	''' <summary>
	''' Devuelve el valor del OpenSearch.
	''' </summary>
	''' <param name="ReturnValue">The return value.</param>
	''' <param name="ReturnObject">The return object.</param>
	Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
		DeleteBlockedRecord()
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

#Region "Enums"

	Public Enum eMultipleBase
		DistributionA = 1
		DistributionB = 2
		DistributionC = 3
		DistributionD = 4
	End Enum

#End Region

	Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
		UpdateState()
	End Sub

	''' <summary>
	''' Updates the state.
	''' </summary>
	Public Async Sub UpdateState()
		If Not String.IsNullOrEmpty(Me._generalExpenses.Code) Then
			Try
				Using model As New MCostGeneralExpenses(Me.Tag)
					AsyncLoader(True)
					Dim stateGeneralExpense As Boolean = Not Me._generalExpenses.Status
					Dim Result = Await model.UpdateStateCostGeneralExpense(Me._generalExpenses.Code, stateGeneralExpense)
					AsyncLoader(False)
					Select Case Result.StatusCode
						Case eStatusResult.SUCCESS
							Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
							_generalExpenses = Result.ObjectEmbbeded
							Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
						Case eStatusResult.WARNING
							Mensaje(EeventViewerImages.Advertencia) = Result.Message
						Case eStatusResult.EXCEPTION
							Mensaje(EeventViewerImages.MensajeError) = Result.Message
					End Select
				End Using
			Catch ex As Exception
				Throw ex
				AsyncLoader(False)
			End Try
		Else
			Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
		End If
	End Sub

	''' <summary>
	''' Evento para habilitar el campo Generar Cuenta por Pagar
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleDistributionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleDistributionType.EditValueChanged
		If DistributionType = 2 And ElementCostType = 1 Then
			INDlyItemGenerateAccountPayable.HideControl(False)
			If Not GenerateAccountPayable Then
				INDlygJournalVoucherGrouping.HideControl(False)
			End If
		Else
			INDlyItemGenerateAccountPayable.HideControl
			INDlygJournalVoucherGrouping.HideControl
		End If
	End Sub
	''' <summary>
	''' Evento para habilitar el campo Generar Cuenta por Pagar
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleElementCostType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleElementCostType.EditValueChanged
		If DistributionType = 2 And ElementCostType = 1 Then
			INDlyItemGenerateAccountPayable.HideControl(False)
			If Not GenerateAccountPayable Then
				INDlygJournalVoucherGrouping.HideControl(False)
			End If
		Else
			INDlyItemGenerateAccountPayable.HideControl
			INDlygJournalVoucherGrouping.HideControl
		End If
	End Sub

	''' <summary>
	''' Evento para controlar visibilidad del segmento Comprobantes Contables
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleGenerateAccountPayable_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleGenerateAccountPayable.EditValueChanged
		If Not GenerateAccountPayable Then
			INDlygJournalVoucherGrouping.HideControl(False)
		Else
			INDlygJournalVoucherGrouping.HideControl
		End If
	End Sub

	''' <summary>
	''' Evento para cargar el Datasource de los Tipos de Comprobantes Contables
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleDirectLaborDistribution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDirectLaborDistribution.QueryPopUp
		If DirectLaborDistributionJournalVoucherTypeXpo Is Nothing Then
			DirectLaborDistributionJournalVoucherTypeXpo = _presenter.InitializeVoucherType()
		End If
	End Sub

	''' <summary>
	''' Evento para cargar el Datasource de los Tipos de Comprobantes Contables de Reversión
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleReversalDirectLaborDistribution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleReversalDirectLaborDistribution.QueryPopUp
		If ReversalDirectLaborDistributionJournalVoucherTypeXpo Is Nothing Then
			ReversalDirectLaborDistributionJournalVoucherTypeXpo = _presenter.InitializeVoucherType()
		End If
	End Sub

	''' <summary>
	''' Evento que abre el formulario de Tipos de Comprobantes Contables
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleDirectLaborDistribution_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleDirectLaborDistribution.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			OpenForm(607, Nothing, True)
			DirectLaborDistributionJournalVoucherTypeXpo = _presenter.InitializeVoucherType()
		End If
	End Sub

	''' <summary>
	''' Evento que abre el formulario de Tipos de Comprobantes Contables
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub INDSleReversalDirectLaborDistribution_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleReversalDirectLaborDistribution.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			OpenForm(607, Nothing, True)
			ReversalDirectLaborDistributionJournalVoucherTypeXpo = _presenter.InitializeVoucherType()
		End If
	End Sub
End Class