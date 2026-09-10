'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 19-12-2014
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

Public Class FrmGeneralExpenses
    Implements IGeneralExpenses


#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmGeneralExpenses"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()

    End Sub
#End Region

#Region "Properties and Variables"

#Region "Properties Entity"
    ''' <summary>
    ''' Obtiene o establece el codigo del gasto
    ''' </summary>
    Public Property Code As String Implements IGeneralExpenses.Code
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
    ''' Obtiene o establece el tipo de gasto
    ''' </summary>
    Public Property ExpendidureType As Byte Implements IGeneralExpenses.ExpendidureType
        Get
            Return INDgleExpenseType.EditValue
        End Get
        Set(value As Byte)
            INDgleExpenseType.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el nombre del gasto
    ''' </summary>
    Public Property NameGeneralExpense As String Implements IGeneralExpenses.NameGeneralExpense
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
    Public Property Status As String Implements IGeneralExpenses.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
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
    Public Property MultipleBase As Byte Implements IGeneralExpenses.MultipleBase
        Get
            Return CType(INDgleDistribution.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleDistribution.EditValue = value
        End Set
    End Property
#End Region

#Region "Datasource"

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
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As InteropCostSetting Implements IGeneralExpenses.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As InteropCostSetting)
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IGeneralExpenses.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IGeneralExpenses.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para almacenar el xpo de las categorias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GeneralExpenseCategoryXpo As XPCollection Implements IGeneralExpenses.GeneralExpenseCategoryXpo
        Get
            Return INDgleCategory.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDgleCategory.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As InteropCostSecuence Implements IGeneralExpenses.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As InteropCostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InteropCostSecuenceDetail In Me._sequence.InteropCostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' opciones de distribución
    ''' </summary>
    Private _distributionOptions As List(Of Tuple(Of Integer, Tuple(Of Image, String)))

    ''' <summary>
    ''' tipos de gasto
    ''' </summary>
    Private _expenseType As List(Of Tuple(Of Integer, String))

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
    Dim _presenter As PGeneralExpenses

    Private model As MGeneralExpenses

    ''' <summary>
    ''' entidad de gastos generales
    ''' </summary>
    Private _generalExpenses As GeneralExpense

    Private _listDistributionOptions As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordInteropCost

    ''' <summary>
    ''' Listado del detalle de base de distribución
    ''' </summary>
    Property ListDistributionBase As List(Of DistributionBase)
        Get
            Return CType(INDgcDsitribution.DataSource, List(Of DistributionBase))
        End Get
        Set(value As List(Of DistributionBase))
            INDgcDsitribution.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de los centros de produccón a los cuales se les va a hacer la distribución
    ''' </summary>
    Private ListDistributionBaseDetail As List(Of DistributionBaseDetail)

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGeneralExpenses.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value

            INDgleExpenseType.Enabled = value
            INDgleCategory.Enabled = value
            INDgleDistribution.Enabled = value
            INDsbAddDistribution.Enabled = value
            INDSleElementCostType.Enabled = value
            INDgcDsitribution.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDtxtName.Focus()
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
        _distributionOptions = Nothing
        _expenseType = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        model = Nothing
        _generalExpenses = Nothing
        _listDistributionOptions = Nothing
        _record = Nothing
        ListDistributionBaseDetail = Nothing
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
        model = New MGeneralExpenses(Me.Tag)

        IndigoGridControl1.RefreshGrid(INDgcDsitribution)
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        _listActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvDistributionBase, _listActions)

        INDgvDistributionBase.Columns.ColumnByName("colActions").Width = 80

        _presenter = New PGeneralExpenses(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()

        CreateDistributionOptions()
        CreateExpenseType()
        CreateElementCostType()
        LoadStatus()
        Deshacer()
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

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmGeneralExpenses_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
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

#Region "EditValueChanged"
    'Private MainAccountXpo As Object
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleMainAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

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

#Region "QueryPopUp"

    Private Sub INDgleCategory_QueryPopUp(sender As Object, e As CancelEventArgs)
        If GeneralExpenseCategoryXpo Is Nothing Then

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
    ''' News the general expenses.
    ''' </summary>
    Private Async Sub NewGeneralExpenses()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        Using Model As New MInteropCostSetting(Me.MyTag)
            _settingsCost = Await Model.GetInteropCostSettingAsync()
            If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingCostNotFound", MODULE_NAME)
                Exit Sub
            End If
            Me.SettingsCost = _settingsCost
        End Using
        Me._generalExpenses = New GeneralExpense()
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.InteropCostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
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
                        Using model As New MCommonInteropCost(Me.Tag)
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


    ''' <summary>
    ''' Abre el formulario de base distribución
    ''' </summary>
    Private Sub OpenFormDistributionBase(ByVal edit As Boolean, Optional ByVal distribBase As DistributionBase = Nothing)
        If edit OrElse ValidateDistributionBase() Then
            Dim frmDistributionBase As New PopUpDistributionBase()
            With frmDistributionBase
                .FormParentName = PopUpDistributionBase.eParent.GeneralExpenses
                .CostClass = INDgleExpenseType.EditValue
                If _generalExpenses.DistributionBase IsNot Nothing AndAlso _generalExpenses.DistributionBase.Count > 0 Then
                    .CountDistributionBase = _generalExpenses.DistributionBase.Count
                    'Dim listDBDetail = _generalExpenses.DistributionBase(0).DistributionBaseDetail.Select(Function(x) x.CloneEntity()).ToList()
                    'For Each item As DistributionBaseDetail In listDBDetail
                    '    item.Quantity = 0
                    'Next
                    '.ListDistributionBaseDetail = listDBDetail
                    .ListDistributionBaseDetail = _generalExpenses.DistributionBase(0).DistributionBaseDetail.ToList()
                    Dim listDBMeasureUnit = _generalExpenses.DistributionBase(0).DistributionBaseMeasurementUnit.Select(Function(x) x.Clone()).ToList()

                    .ListDistributionBaseMeasurementUnit = listDBMeasureUnit
                End If

                If edit Then
                    .LoadDistributionBase(distribBase)
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
    Private Sub AddDistributionBase(distributionBase As DistributionBase, listToDelete As List(Of DistributionBaseDetail))
        distributionBase.DistributionBaseName = String.Format(ResourceManager.GetString("DistributionBaseName", MODULE_NAME), _listDistributionOptions(distributionBase.MultipleBase - 1).Item2)

        _generalExpenses.DistributionBase.Add(distributionBase)
        ListDistributionBase = _generalExpenses.DistributionBase.ToList()
        INDgcDsitribution.RefreshDataSource()
        If _generalExpenses.ChangeTracker.State <> ObjectState.Added Then
            _generalExpenses.MarkAsModified()
        End If
        If listToDelete IsNot Nothing Then
            For Each itemToDelete As DistributionBaseDetail In listToDelete
                For Each distrBase As DistributionBase In _generalExpenses.DistributionBase
                    If distrBase.DistributionBaseDetail IsNot Nothing AndAlso distrBase.DistributionBaseDetail.Count > 0 Then
                        Dim _dBaseDetail As DistributionBaseDetail = distrBase.DistributionBaseDetail.Where(Function(x) x.ProductionCenterId = itemToDelete.ProductionCenterId).FirstOrDefault()
                        If _dBaseDetail IsNot Nothing Then
                            _dBaseDetail.MarkAsDeleted()
                        End If
                    End If
                Next
            Next
        End If
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
        'form.MdiParent = Me.MdiParent
        'form.Owner = Me.Owner
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

            If ListDistributionBase(0).DistributionType = Presentation.InteropCost.PopUpDistributionBase.eDistributionType.Direct Then
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
    ''' Creates the type of the expense.
    ''' </summary>
    Private Sub CreateExpenseType()
        _expenseType = New List(Of Tuple(Of Integer, String))()
        _expenseType.Add(New Tuple(Of Integer, String)(1, "Fijo"))
        _expenseType.Add(New Tuple(Of Integer, String)(2, "Variable"))
        INDgleExpenseType.Properties.DataSource = _expenseType
    End Sub

    Private Sub CreateElementCostType()
        Dim _listElementCostType As New List(Of Tuple(Of Integer, String))
        _listElementCostType.Add(New Tuple(Of Integer, String)(1, "Mano de obra Directa"))
        _listElementCostType.Add(New Tuple(Of Integer, String)(2, "Mano de obra Indirecta"))
        _listElementCostType.Add(New Tuple(Of Integer, String)(3, "Materiales Directos"))
        _listElementCostType.Add(New Tuple(Of Integer, String)(4, "Materiales Indirectos"))
        _listElementCostType.Add(New Tuple(Of Integer, String)(5, "Otros Gastos"))
        INDSleElementCostType.Properties.DataSource = _listElementCostType
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
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._generalExpenses.Code, Me._generalExpenses.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._generalExpenses.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._generalExpenses.Code), _
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
    Public Sub AssigningValues() Implements IGeneralExpenses.AssigningValues
        With _generalExpenses
            .Code = Code
            .Name = NameGeneralExpense
            .GeneralExpenseCategoryId = INDgleCategory.EditValue
            .ElementCostType = INDSleElementCostType.EditValue
            .ExpenditureType = ExpendidureType
            If .Id = 0 Then
                .Status = True
            Else
                Select Case Status
                    Case eActionsStatusRecords.Active
                        .Status = True
                    Case eActionsStatusRecords.Inactive
                        .Status = False
                End Select
            End If

        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IGeneralExpenses.CleanControls
        INDlcRoot.BeginUpdate()

        ReadOnlyControls(False, INDlcRoot)
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDSleElementCostType.EditValue = Nothing
        INDgleExpenseType.EditValue = Nothing
        INDgleDistribution.EditValue = Nothing
        INDgleCategory.Properties.NullText = String.Empty
        INDgleCategory.EditValue = Nothing

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

        INDlcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements IGeneralExpenses.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            Dim resGeneralExpense = Await model.GetGeneralExpense(Me.Code)
            If resGeneralExpense.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(resGeneralExpense.StatusCode) = resGeneralExpense.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _generalExpenses = resGeneralExpense.ObjectEmbbeded
            If _generalExpenses IsNot Nothing AndAlso _generalExpenses.Id > 0 Then
                Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, _generalExpenses.Id)
                    With _generalExpenses
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        NameGeneralExpense = .Name


                        INDSleElementCostType.EditValue = .ElementCostType
                        INDgleCategory.Properties.NullText = .CategoryCodeName
                        INDgleCategory.EditValue = .GeneralExpenseCategoryId
                        ExpendidureType = .ExpenditureType
                        Status = .Status
                    End With


                    ListDistributionBase = _generalExpenses.DistributionBase.ToList()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._generalExpenses.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _generalExpenses.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_generalExpenses.Id, MyTag, Nothing, GetType(GeneralExpense).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                    Dim _settingsCostQ As InteropCostSetting
                    Using ModelSetting As New MInteropCostSetting(Me.Tag)
                        _settingsCostQ = ModelSetting.GetInteropCostSetting()
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
        Dim _distribBase As DistributionBase = CType(INDgvDistributionBase.GetFocusedRow(), DistributionBase)
        If ListDistributionBase.Where(Function(x) x.MultipleBase > _distribBase.MultipleBase).Count() > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Debe seleccionar una Distribución con una Base de Tipo ({0})", _listDistributionOptions.ElementAt(_distribBase.MultipleBase).Item2)
        Else
            'Marca como eliminado los detalles si existen
            If _distribBase.DistributionBaseDetail IsNot Nothing AndAlso _distribBase.DistributionBaseDetail.Count > 0 Then
                While _distribBase.DistributionBaseDetail.Count > 0
                    _distribBase.DistributionBaseDetail.Item(_distribBase.DistributionBaseDetail.Count() - 1).MarkAsDeleted()
                End While
            End If
            While _distribBase.DistributionBaseMeasurementUnit.Count > 0
                _distribBase.DistributionBaseMeasurementUnit(0).MarkAsDeleted()
            End While
            _distribBase.MarkAsDeleted()
            If _generalExpenses.ChangeTracker.State <> ObjectState.Added Then
                _generalExpenses.MarkAsModified()
            End If
            ListDistributionBase = _generalExpenses.DistributionBase.ToList()
            INDgcDsitribution.RefreshDataSource()
        End If
    End Sub

    Private Sub EditDistributionBase()
        Dim _distribBase = CType(INDgvDistributionBase.GetFocusedRow(), DistributionBase)
        OpenFormDistributionBase(True, _distribBase)
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Me._generalExpenses IsNot Nothing AndAlso Me._generalExpenses.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteGeneralExpense(Me._generalExpenses)
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
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = True Then
            If INDgvDistributionBase.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar mínimo una base de distribucion"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of GeneralExpense) = Await model.SaveGeneralExpense(Me._generalExpenses, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _generalExpenses.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
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
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
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
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda

        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Boolean))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Boolean)("Activo", True))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Boolean)("Inactivo", False))


        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.65},
                              New ColumnInfo() With {.Caption = "Categoria", .FieldName = "GeneralExpenseCategoryId.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListGeneralExpenses
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
                Using model As New MGeneralExpenses(Me.Tag)
                    Dim stateGeneralExpense As Boolean
                    Select Case Status
                        Case eActionsStatusRecords.Active
                            stateGeneralExpense = True
                        Case eActionsStatusRecords.Inactive
                            stateGeneralExpense = False
                    End Select
                    AsyncLoader(True)
                    Dim Result = Await model.UpdateStateGeneralExpense(Me._generalExpenses.Code, stateGeneralExpense)
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

    

    Private Sub INDgleCategory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            ''OpenForm(1926, Nothing, True) Falta crear el formulario
            '_presenter.InitializeCategories()
        End If
    End Sub
End Class