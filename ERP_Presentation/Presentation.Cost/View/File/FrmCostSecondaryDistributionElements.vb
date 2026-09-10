'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 19-01-2014
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

Public Class FrmCostSecondaryDistributionElements
    Implements ICostSecondaryDistributionElements

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        model = New MCostDistributionSecondaryElements(Me.Tag)
        _presenter = New PCostSecondaryDistributionElements(Me)
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
    Public ReadOnly Property MyTag As Object Implements ICostSecondaryDistributionElements.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostSecondaryDistributionElements.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Variable que contiene el modelo
    ''' </summary>
    Private model As MCostDistributionSecondaryElements

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PCostSecondaryDistributionElements

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
    Private _sequence As Domain.Entities.CostSecuence

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As CostSecuence Implements ICostSecondaryDistributionElements.Sequence
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
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordCost

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
    ''' parametros de costos
    ''' </summary>
    Private _settingsCost As CostSetting

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As CostSetting Implements ICostSecondaryDistributionElements.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As CostSetting)
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' entidad de distribución secundaria
    ''' </summary>
    Private _secondaryDistribution As CostDistributionSecondary

    ''' <summary>
    ''' Listado del detalle de base de distribución
    ''' </summary>
    Property ListDistributionSecondaryBase As List(Of CostDistributionSecondaryBase)
        Get
            Return CType(INDgcDistribution.DataSource, List(Of CostDistributionSecondaryBase))
        End Get
        Set(value As List(Of CostDistributionSecondaryBase))
            INDgcDistribution.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de los centros de produccón a los cuales se les va a hacer la distribución
    ''' </summary>
    Private ListDistributionBaseDetail As List(Of CostDistributionSecondaryBaseDetail)

    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
    Private CurrencyAbbreviation As String

#End Region

#Region "Properties Entity"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostSecondaryDistributionElements.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleProductionCenter.Enabled = value
            INDgleDistribution.Enabled = value
            INDsbAddDistribution.Enabled = value
            INDgcDistribution.Enabled = value

            BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDtxtDescription.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de la distribucion segundaria
    ''' </summary>
    Public Property Code As String Implements ICostSecondaryDistributionElements.Code
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
    ''' Descripción de la distribución secundaria
    ''' </summary>
    Public Property Description As String Implements ICostSecondaryDistributionElements.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Centro de prtoducción de la distribución secundaria
    ''' </summary>
    Public Property ProductionCenterId As Integer Implements ICostSecondaryDistributionElements.ProductionCenterId
        Get
            Return CType(INDsleProductionCenter.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleProductionCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As Boolean Implements ICostSecondaryDistributionElements.Status
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
    Public Property MultipleBase As Byte
        Get
            Return CType(INDgleDistribution.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleDistribution.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasource Entity"

#Region "Tuples"

    ''' <summary>
    ''' The _list distribution options
    ''' </summary>
    Private _listDistributionOptions As List(Of Tuple(Of Integer, String))

#End Region

    ''' <summary>
    ''' Datasource de los centros de produccion
    ''' </summary>
    Public Property ProductionCenterDatasource As XPInstantFeedbackSource Implements ICostSecondaryDistributionElements.ProductionCenterDatasource
        Get
            Return CType(INDsleProductionCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProductionCenter.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmSecondaryDistribution control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSecondaryDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        'IndigoGridControl1.RefreshGrid(INDgcDistribution)
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        _listActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvDistributionBase, _listActions)
        For Each col As GridColumn In INDgvDistributionBase.Columns
            If col.Name = "colActions" Then
                col.Width = 80
            End If
        Next

        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        _presenter.LoadSettingCost()

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
    Private Sub FrmSecondaryDistribution_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model = Nothing
        _settingsCost = Nothing
        _listDistributionOptions = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        _secondaryDistribution = Nothing
        _record = Nothing
        ListDistributionBaseDetail = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmSecondaryDistribution_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
                    Me.NewDistributionSecondary()
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

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1729, Nothing, True)
            _presenter.LoadProductionCenter()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionCenter.QueryPopUp
        If INDsleProductionCenter.Properties.DataSource Is Nothing Then
            _presenter.LoadProductionCenter()
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

#Region "Double Click"

    ''' <summary>
    ''' Handles the DoubleClick event of the INDgcDsitribution control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDsitribution_DoubleClick(sender As Object, e As EventArgs) Handles INDgcDistribution.DoubleClick
        EditSecondaryDistributionBase()
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
                EditSecondaryDistributionBase()
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteSecondaryDistributionBase()
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
        If Me._secondaryDistribution IsNot Nothing AndAlso Me._secondaryDistribution.Id > 0 Then
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

#Region "Methods and Functions"

    ''' <summary>
    ''' Creates the distribution options.
    ''' </summary>
    Private Sub CreateDistributionOptions()
        _listDistributionOptions = New List(Of Tuple(Of Integer, String))()
        _listDistributionOptions.Add(New Tuple(Of Integer, String)(1, "1"))
        _listDistributionOptions.Add(New Tuple(Of Integer, String)(2, "2"))
        _listDistributionOptions.Add(New Tuple(Of Integer, String)(3, "3"))
        _listDistributionOptions.Add(New Tuple(Of Integer, String)(4, "4"))
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' News the general expenses.
    ''' </summary>
    Private Async Sub NewDistributionSecondary()
        If Me._settingsCost Is Nothing OrElse Me._settingsCost.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Costos"
            Exit Sub
        End If

        Me._secondaryDistribution = New CostDistributionSecondary() With {.Status = True}
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
        Status = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub

    ''' <summary>
    ''' Validates the distribution base.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDistributionBase() As Boolean
        If ListDistributionSecondaryBase IsNot Nothing AndAlso ListDistributionSecondaryBase.Count > 0 Then
            Dim numeroMaximo As Integer = ListDistributionSecondaryBase.Max(Function(x) x.MultipleBase)
            If numeroMaximo = 4 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DistributionBaseComplete", MODULE_NAME)
                Return False
            End If

            If ListDistributionSecondaryBase(0).DistributionType = Presentation.InteropCost.PopUpDistributionBase.eDistributionType.Direct Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("GeneralExpenseNoAddDistributionBase", MODULE_NAME)
                Return False
            End If

            If MultipleBase <= numeroMaximo OrElse (MultipleBase - numeroMaximo) <> 1 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectDistributionBaseType", MODULE_NAME), _listDistributionOptions.ElementAt(numeroMaximo).Item2)
                Return False
            End If
        Else
            If MultipleBase <> Presentation.InteropCost.FrmGeneralExpenses.eMultipleBase.DistributionA Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InitializeDistributionBase", MODULE_NAME)
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Abre el formulario de base distribución
    ''' </summary>
    Private Sub OpenFormDistributionBase(ByVal edit As Boolean, Optional ByVal distribBase As CostDistributionSecondaryBase = Nothing)
        If edit OrElse ValidateDistributionBase() Then
            Dim costDistributionSecondary = _secondaryDistribution.CloneEntity()
            Dim frmDistributionBase As New PopUpCostDistributionBase()
            With frmDistributionBase
                .FormBorderStyle = FormBorderStyle.Sizable
                .FormParentName = PopUpCostDistributionBase.eParent.DistributionSecondary
                If costDistributionSecondary.CostDistributionSecondaryBase IsNot Nothing AndAlso costDistributionSecondary.CostDistributionSecondaryBase.Count > 0 Then
                    .EditMode = edit
                    .CountDistributionBase = costDistributionSecondary.CostDistributionSecondaryBase.Count
                    .ListDistributionSecondaryBaseDetail = costDistributionSecondary.CostDistributionSecondaryBase(0).CostDistributionSecondaryBaseDetail.ToList()
                    .ListDistributionSecondaryMeasurementUnit = costDistributionSecondary.CostDistributionSecondaryBase(0).CostDistributionSecondaryMeasurementUnit.ToList()
                End If
                .CurrencyAbbreviation = CurrencyAbbreviation
                If edit Then
                    .LoadDistributionBase(Nothing, distribBase.CloneEntity())
                Else
                    .MultipleBase = MultipleBase
                End If
            End With
            AddHandler frmDistributionBase.AddDistributionBaseSecondary, AddressOf AddDistributionBase
            OpenFormLocal(frmDistributionBase)
        End If
    End Sub

    ''' <summary>
    ''' Adds the distribution base.
    ''' </summary>
    ''' <param name="distributionBase">The distribution base.</param>
    Private Sub AddDistributionBase(ByVal edit As Boolean, distributionBase As CostDistributionSecondaryBase)
        distributionBase.DistributionBaseName = String.Format(ResourceManager.GetString("DistributionBaseName", MODULE_NAME), _listDistributionOptions(distributionBase.MultipleBase - 1).Item2)
        If edit Then
            Dim costDistributionBase = _secondaryDistribution.CostDistributionSecondaryBase.Where(Function(b) b.MultipleBase = distributionBase.MultipleBase).FirstOrDefault
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
            costDistributionBase.CousinValue = distributionBase.CousinValue
            costDistributionBase.InvoiceValue = distributionBase.InvoiceValue

            For Each item In distributionBase.CostDistributionSecondaryBaseDetail
                Dim costDistributionBaseDetail = costDistributionBase.CostDistributionSecondaryBaseDetail.Where(Function(d) d.ProductionCenterId = item.ProductionCenterId).FirstOrDefault()
                If costDistributionBaseDetail Is Nothing Then
                    costDistributionBaseDetail = New CostDistributionSecondaryBaseDetail()
                    costDistributionBase.CostDistributionSecondaryBaseDetail.Add(costDistributionBaseDetail)
                End If

                costDistributionBaseDetail.ProductionCenterId = item.ProductionCenterId
                costDistributionBaseDetail.Quantity = item.Quantity
            Next

            For Each item In distributionBase.CostDistributionSecondaryMeasurementUnit
                Dim costDistributionBaseMeasurementUnit = costDistributionBase.CostDistributionSecondaryMeasurementUnit.Where(Function(d) d.MeasurementUnitId = item.MeasurementUnitId).FirstOrDefault()
                If costDistributionBaseMeasurementUnit Is Nothing Then
                    costDistributionBaseMeasurementUnit = New CostDistributionSecondaryMeasurementUnit()
                    costDistributionBaseMeasurementUnit.MeasurementUnitId = item.MeasurementUnitId
                    costDistributionBaseMeasurementUnit.CodeNameMeasureUnit = item.CodeNameMeasureUnit
                    costDistributionBase.CostDistributionSecondaryMeasurementUnit.Add(costDistributionBaseMeasurementUnit)
                End If
            Next
        Else
            _secondaryDistribution.CostDistributionSecondaryBase.Add(distributionBase)
        End If

        ListDistributionSecondaryBase = _secondaryDistribution.CostDistributionSecondaryBase.ToList()
        INDgcDistribution.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Edits the distribution base.
    ''' </summary>
    Private Sub EditSecondaryDistributionBase()
        Dim _distribBase As CostDistributionSecondaryBase = CType(INDgvDistributionBase.GetFocusedRow(), CostDistributionSecondaryBase)
        OpenFormDistributionBase(True, _distribBase)
    End Sub

    ''' <summary>
    ''' Deletes the distribution base.
    ''' </summary>
    Private Sub DeleteSecondaryDistributionBase()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _distribSecondaryBase As CostDistributionSecondaryBase = CType(INDgvDistributionBase.GetFocusedRow(), CostDistributionSecondaryBase)
            If ListDistributionSecondaryBase.Where(Function(x) x.MultipleBase > _distribSecondaryBase.MultipleBase).Count() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("Debe seleccionar una Distribución con una Base de Tipo ({0})", _listDistributionOptions.ElementAt(_distribSecondaryBase.MultipleBase).Item2)
            Else
                'Marca como eliminado los detalles si existen
                If _distribSecondaryBase.CostDistributionSecondaryBaseDetail IsNot Nothing AndAlso _distribSecondaryBase.CostDistributionSecondaryBaseDetail.Count > 0 Then
                    While _distribSecondaryBase.CostDistributionSecondaryBaseDetail.Count > 0
                        _distribSecondaryBase.CostDistributionSecondaryBaseDetail.Item(_distribSecondaryBase.CostDistributionSecondaryBaseDetail.Count() - 1).MarkAsDeleted()
                    End While
                End If
                While _distribSecondaryBase.CostDistributionSecondaryMeasurementUnit.Count > 0
                    _distribSecondaryBase.CostDistributionSecondaryMeasurementUnit(0).MarkAsDeleted()
                End While
                _distribSecondaryBase.MarkAsDeleted()
                If _secondaryDistribution.ChangeTracker.State <> ObjectState.Added Then
                    _secondaryDistribution.MarkAsModified()
                End If
                ListDistributionSecondaryBase = _secondaryDistribution.CostDistributionSecondaryBase.ToList()
                INDgcDistribution.RefreshDataSource()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Imports the previus data.
    ''' </summary>
    Private Sub ImportPreviusData()
        Dim _settingsCostQ As CostSetting
        Using ModelSetting As New MCostSetting(Me.Tag)
            _settingsCostQ = ModelSetting.GetCostSetting()
        End Using
        If _settingsCostQ IsNot Nothing Then

            Dim _previusMonth As Integer = _settingsCostQ.Month - 1
            Dim _previusYear As Integer = _settingsCostQ.Year
            If _previusMonth = 0 Then
                _previusMonth = 12
                _previusYear -= 1
            End If

            Dim _listPeriods As List(Of String) = model.ListPeriodWithDataByMaximumPeriodDistributionSecondarySimple(_previusYear, _previusMonth)
            If _listPeriods Is Nothing OrElse _listPeriods.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos de periodos anteriores para importar"
                Exit Sub
            End If

            Using Form As New FrmCostSelectPeriod
                Form.DatasourceType = FrmCostSelectPeriod.eDatasourceType.DistributionSecondary
                Form.PreviusMonth = _previusMonth
                Form.PreviusYear = _previusYear
                Form.LisPeriods = _listPeriods
                Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Form.MinimizeBox = False
                Form.MaximizeBox = False
                AddHandler Form.AddImportDataSecondary, AddressOf ImportData
                Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                Dim transparent As New FrmTransparent(Form, False)
                transparent.ShowDialog(Me)
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron parámetros de costos"
        End If
    End Sub

    ''' <summary>
    ''' Imports the data.
    ''' </summary>
    Private Async Sub ImportData(dataImpor As List(Of CostDistributionSecondary))
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
                For Each item As CostDistributionSecondary In dataImpor
                    Dim _distrib As New CostDistributionSecondary()
                    With _distrib
                        .ProductionCenterId = item.ProductionCenterId
                        .Description = item.Description

                        .Status = item.Status

                        For Each itemBase As CostDistributionSecondaryBase In item.CostDistributionSecondaryBase
                            Dim _itemBase As New CostDistributionSecondaryBase()
                            With _itemBase
                                .DistributionType = itemBase.DistributionType
                                .ImpactPoints = itemBase.ImpactPoints
                                .MeasurementUnit = itemBase.MeasurementUnit
                                .Description = itemBase.Description
                                .Area = itemBase.Area
                                .OfficialHours = itemBase.OfficialHours
                                .SupplyValue = itemBase.SupplyValue
                                .WorkmanshipValue = itemBase.WorkmanshipValue
                                .AssetValue = itemBase.AssetValue
                                .CousinValue = itemBase.CousinValue
                                .InvoiceValue = itemBase.InvoiceValue
                                .MultipleBase = itemBase.MultipleBase
                            End With
                            For Each itemBaseDetail As CostDistributionSecondaryBaseDetail In itemBase.CostDistributionSecondaryBaseDetail
                                Dim _itemBaseDetail As New CostDistributionSecondaryBaseDetail()
                                With _itemBaseDetail
                                    .ProductionCenterId = itemBaseDetail.ProductionCenterId
                                    .Quantity = itemBaseDetail.Quantity
                                End With
                                _itemBase.CostDistributionSecondaryBaseDetail.Add(_itemBaseDetail)
                            Next
                            _distrib.CostDistributionSecondaryBase.Add(_itemBase)
                        Next
                    End With
                    Dim result = Await model.SaveDistributionSecondary(_distrib, Me._idCurrentSequence)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionSecondaryImportCorrect", MODULE_NAME), item.FullNameProductionCenter), 1))
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-001" Then
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionSecondaryImportAdvertence", MODULE_NAME), item.FullNameProductionCenter), 3))
                        Else
                            _listMessage.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DistributionSecondaryImportError", MODULE_NAME), item.FullNameProductionCenter, result.Message), 2))
                        End If
                    End If
                Next
                If _listMessage IsNot Nothing AndAlso _listMessage.Count > 0 Then
                    Using Formulario As New FrmListErrors(_listMessage)
                        Formulario.Title = ResourceManager.GetString("ResultOperationMessage")
                        Formulario.MinimizeBox = False
                        Formulario.MaximizeBox = False
                        Formulario.Size = New Size(780, 500)
                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(Formulario, False)
                        transparent.ShowDialog()
                    End Using
                    If FormSearchObjects IsNot Nothing Then
                        FormSearchObjects.UpdateDatasource()
                    End If
                End If

            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements ICostSecondaryDistributionElements.AssigningValues
        With _secondaryDistribution
            .Code = Code
            .ProductionCenterId = ProductionCenterId
            .Description = Description
        End With
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._secondaryDistribution.Code, Me._secondaryDistribution.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._secondaryDistribution.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._secondaryDistribution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._secondaryDistribution.Code, Me._secondaryDistribution.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._secondaryDistribution.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements ICostSecondaryDistributionElements.CleanControls
        INDlcRoot.BeginUpdate()
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        Code = Nothing
        Description = Nothing
        ProductionCenterId = Nothing
        INDgleDistribution.EditValue = Nothing

        ListDistributionSecondaryBase = Nothing
        INDsleProductionCenter.Properties.NullText = String.Empty

        _secondaryDistribution = Nothing
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
    Public Async Sub LoadControls() Implements ICostSecondaryDistributionElements.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If

            AsyncLoader(True)
            Dim res = Await model.GetDistributionSecondary(Me.Code)
            If res.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(res.StatusCode) = res.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _secondaryDistribution = res.ObjectEmbbeded
            If _secondaryDistribution IsNot Nothing AndAlso _secondaryDistribution.Id > 0 Then
                Using ModelCommonTreasury As New MCommonCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _secondaryDistribution.Id)
                    'BarraBotones.StatusRecordVisible = True
                    With _secondaryDistribution
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        Description = .Description

                        ProductionCenterId = .ProductionCenterId
                        Status = .Status

                    End With

                    INDsleProductionCenter.Properties.NullText = _secondaryDistribution.FullNameProductionCenter
                    ListDistributionSecondaryBase = _secondaryDistribution.CostDistributionSecondaryBase.ToList()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._secondaryDistribution.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _secondaryDistribution.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_secondaryDistribution.Id, MyTag, Nothing, GetType(CostDistributionSecondary).Name)

                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                    Dim _settingsCostQ As CostSetting
                    Using ModelSetting As New MCostSetting(Me.Tag)
                        _settingsCostQ = ModelSetting.GetCostSetting()
                    End Using

                    AsyncLoader(False)
                    ActionsOnControls = True
                    INDbteCode.Enabled = False
                End Using
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewDistributionSecondary()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If

            INDgleDistribution.SelectedIndex = 0
        End If
    End Sub

#End Region

#Region "Icrud"

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
        If Me._secondaryDistribution IsNot Nothing AndAlso Me._secondaryDistribution.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteDistributionSecondary(Me._secondaryDistribution)
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
            If INDgvDistributionBase.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una base de distribución"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveDistributionSecondary(Me._secondaryDistribution, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _secondaryDistribution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.CostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._secondaryDistribution = result.ObjectEmbbeded
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
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                AsyncLoader(True)
                Dim state As Boolean = Not Me._secondaryDistribution.Status
                Dim result = Await model.UpdateStateDistributionSecondary(Me._secondaryDistribution.Code, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._secondaryDistribution = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                End If
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Mensaje(result.StatusCode) = result.Message
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
                INDbteCode.Enabled = False
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
            Me.NewDistributionSecondary()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Centro Producciòn", .FieldName = "ProductionCenterId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostDistributionSecondary
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
    ''' Barras the botones_ click_ importar informacion.
    ''' </summary>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        If Me._settingsCost Is Nothing OrElse Me._settingsCost.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Costos"
            Exit Sub
        End If

        ImportPreviusData()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
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