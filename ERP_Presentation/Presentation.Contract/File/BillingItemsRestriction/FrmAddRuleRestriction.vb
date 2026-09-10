'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Payroll
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Accounting.MVP

#End Region

Public Class FrmAddRuleRestriction
    Implements IAddRuleRestriction

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
    End Sub

#End Region

#Region "PublicEvents"

    ''' <summary>
    ''' Evento publico para agregar una regla a
    ''' la rejilla del form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInfoToGridBillingItemsRestriction(sender As Object, e As AddInfoToGridBillingItemsRestriction)

#End Region

#Region "Properties"

    Private _cUPSEntityId As Integer?
    Private Property CUPSEntityId As Integer? Implements IAddRuleRestriction.CUPSEntityId
        Get
            Return _cUPSEntityId
        End Get
        Set(value As Integer?)
            _cUPSEntityId = value
        End Set
    End Property

    Private _cUPSSubgroupId As Integer?
    Private Property CUPSSubgroupId As Integer? Implements IAddRuleRestriction.CUPSSubgroupId
        Get
            Return _cUPSSubgroupId
        End Get
        Set(value As Integer?)
            _cUPSSubgroupId = value
        End Set
    End Property

    Private _cUPSGroupId As Integer?
    Private Property CUPSGroupId As Integer? Implements IAddRuleRestriction.CUPSGroupId
        Get
            Return _cUPSGroupId
        End Get
        Set(value As Integer?)
            _cUPSGroupId = value
        End Set
    End Property

    Private _productId As Integer?
    Private Property ProductId As Integer? Implements IAddRuleRestriction.ProductId
        Get
            Return _productId
        End Get
        Set(value As Integer?)
            _productId = value
        End Set
    End Property

    Private _productSubGroupId As Integer?
    Private Property ProductSubGroupId As Integer? Implements IAddRuleRestriction.ProductSubGroupId
        Get
            Return _productSubGroupId
        End Get
        Set(value As Integer?)
            _productSubGroupId = value
        End Set
    End Property

    Private _ProductGroupId As Integer?
    Private Property ProductGroupId As Integer? Implements IAddRuleRestriction.ProductGroupId
        Get
            Return _productSubGroupId
        End Get
        Set(value As Integer?)
            _productSubGroupId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el segundo tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ConditionTypeSecond As Integer? Implements IAddRuleRestriction.ConditionTypeSecond
        Get
            Return INDsleConditionTypeSecond.EditValue
        End Get
        Set(value As Integer?)
            INDsleConditionTypeSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el operador logico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LogicOperator As Integer? Implements IAddRuleRestriction.LogicOperator
        Get
            Return INDsleLogicOperator.EditValue
        End Get
        Set(value As Integer?)
            INDsleLogicOperator.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property RuleType As Integer? Implements IAddRuleRestriction.RuleType
        Get
            Return INDsleRuleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleRuleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ConditionType As Integer? Implements IAddRuleRestriction.ConditionType
        Get
            Return INDsleConditionType.EditValue
        End Get
        Set(value As Integer?)
            INDsleConditionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del cups en donde se incluira el servicio
    ''' </summary>
    ''' <returns></returns>
    Private Property IncludeToCUPSEntityId As Integer? Implements IAddRuleRestriction.IncludeToCUPSEntityId
        Get
            Return INDSleIncludeToCUPS.EditValue
        End Get
        Set(value As Integer?)
            INDSleIncludeToCUPS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource CUPS
    ''' </summary>
    ''' <returns></returns>
    Private Property DataSourceIncludeToCUPSEntity As XPInstantFeedbackSource Implements IAddRuleRestriction.DataSourceIncludeToCUPSEntity
        Get
            Return INDSleIncludeToCUPS.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleIncludeToCUPS.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se esta guardando o editando
    ''' </summary>
    ''' <remarks></remarks>
    Private _modeEdit As Boolean
    Public Property ModeEdit As Boolean
        Get
            Return _modeEdit
        End Get
        Set(value As Boolean)
            _modeEdit = value
        End Set
    End Property

    Public Property ListBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail)
        Get
            Return _listBillingItemsRestrictionDetail
        End Get
        Set(value As List(Of BillingItemsRestrictionDetail))
            _listBillingItemsRestrictionDetail = value
        End Set
    End Property

    Public Property ListBillingItemsRestrictionDetailCondition As List(Of BillingItemsRestrictionDetailCondition)
        Get
            Return _listBillingItemsRestrictionDetailCondition
        End Get
        Set(value As List(Of BillingItemsRestrictionDetailCondition))
            _listBillingItemsRestrictionDetailCondition = value
        End Set
    End Property

    Private WriteOnly Property DataSourceItemsRestrictionDetailCondition As List(Of BillingItemsRestrictionDetailCondition)
        Set(value As List(Of BillingItemsRestrictionDetailCondition))
            Me.INDGcBillingItemsRestrictionDetailCondition.DataSource = value
            Me.INDGcBillingItemsRestrictionDetailCondition.RefreshDataSource()
        End Set
    End Property

    Private WriteOnly Property EnableControlsModeEdit() As Boolean
        Set(value As Boolean)
            Me.INDsleRuleType.Enabled = value
            Me.INDpceControlRuleType.Enabled = value
            Me.INDsleConditionType.Enabled = value
            Me.INDsleLogicOperator.Enabled = value
            Me.INDsleConditionTypeSecond.Enabled = value
            Me.INDSleIncludeToCUPS.Enabled = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Nombre de la vista de la rejilla de los controles
    ''' </summary>
    Private Const VIEW_CONTROLS As String = "viewControlsRuleType"

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Dim ListRulesType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de condición
    ''' </summary>
    Dim ListConditionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el segundo tipo de condición
    ''' </summary>
    Dim ListConditionTypeSecond As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para los tipos de operadores lógicos
    ''' </summary>
    Dim ListLogicOperator As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Asyncrono
    ''' </summary>
    ''' <remarks></remarks>
    Private bw As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Listado para el datasource de los controles de tipo de regla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpCollection As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Presentador del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAddRuleRestriction

    ''' <summary>
    ''' Permite saber si se esta editando un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeEditGridRates As Boolean = False

    ''' <summary>
    ''' Obtiene o establece el nombre de la vista de la rejilla que esta con focus
    ''' </summary>
    ''' <remarks></remarks>
    Dim viewNameFocus As String

    ''' <summary>
    ''' entidad de detalle de servicio no facturable
    ''' </summary>
    Private _listBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail)

    ''' <summary>
    ''' condiciones de detalle de servicio no facturable
    ''' </summary>
    Private _listBillingItemsRestrictionDetailCondition As List(Of BillingItemsRestrictionDetailCondition)
#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListRulesType = Nothing
        ListConditionType = Nothing
        ListConditionTypeSecond = Nothing
        ListLogicOperator = Nothing
        bw = Nothing
        ListXpCollection = Nothing
        Presenter = Nothing
        modeEditGridRates = Nothing
        viewNameFocus = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddRuleRestriction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAddRuleRestriction, True)
        IndigoGridControl1.RefreshGrid(INDGcBillingItemsRestrictionDetailCondition)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvBillingItemsRestrictionDetailCondition, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvBillingItemsRestrictionDetailCondition.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        Presenter = New PAddRuleRestriction(Me)
        InitializeTuples()
        Deshacer()

        If ModeEdit Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddRuleRestriction_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleRuleType.Focus()
        'If ModeEdit = True AndAlso RuleType IsNot Nothing Then
        '    HideColumnsOfGridControlsRuleType()

        '    bw.RunWorkerAsync()
        'End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        Try
            If Me.ModeEdit AndAlso Me.ListBillingItemsRestrictionDetail Is Nothing OrElse Not Me.ListBillingItemsRestrictionDetail?.Any() Then
                Mensaje(EeventViewerImages.Advertencia) = "Se esta en modo de edicion pero no se cargó el detalle"
            End If
            Me.EnableControlsModeEdit = False
            Dim billingItemsRestrictionDetail = Me.ListBillingItemsRestrictionDetail.FirstOrDefault

            With billingItemsRestrictionDetail
                Me.RuleType = .RuleType
                INDpceControlRuleType.Text = "1 item seleccionado"
                Me.ConditionType = .ConditionType
                Me.LogicOperator = .LogicalOperator
                Me.ConditionTypeSecond = .ConditionType2
                Me.ListBillingItemsRestrictionDetailCondition = .BillingItemsRestrictionDetailCondition.ToList()
                Me.DataSourceItemsRestrictionDetailCondition = Me.ListBillingItemsRestrictionDetailCondition.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
                Me.IncludeToCUPSEntityId = .IncludeToCUPSEntityId
                Me.INDSleIncludeToCUPS.Properties.NullText = .IncludeToCUPSDescription
            End With

        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ListXpCollection = Nothing
        ListXpCollection = Presenter.InitializeDataSourceGridControlsRulesType(RuleType)
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        INDgcControlsRuleType.DataSource = Nothing
        INDgcControlsRuleType.DataSource = ListXpCollection
        INDgcControlsRuleType.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Carga el datasource de las tuplas
    ''' </summary>
    Private Sub InitializeTuples()
        'Tipo de Reglas
        ListRulesType = New List(Of Tuple(Of Integer, String))
        ListRulesType.Add(New Tuple(Of Integer, String)(1, "CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(2, "SubGrupos CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(3, "Grupo CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(4, "Producto"))
        ListRulesType.Add(New Tuple(Of Integer, String)(5, "Subgrupo de producto"))
        ListRulesType.Add(New Tuple(Of Integer, String)(6, "Grupo de producto"))
        ListRulesType.Add(New Tuple(Of Integer, String)(7, "Ninguno"))
        INDsleRuleType.Properties.DataSource = ListRulesType.ToList()

        'Tipo de Condiciones Primera
        ListConditionType = New List(Of Tuple(Of Integer, String))
        ListConditionType.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListConditionType.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
        ListConditionType.Add(New Tuple(Of Integer, String)(3, "Tipo de Unidad"))
        ListConditionType.Add(New Tuple(Of Integer, String)(4, "Tipo de Estancia"))
        ListConditionType.Add(New Tuple(Of Integer, String)(5, "Tipo de Manual"))
        ListConditionType.Add(New Tuple(Of Integer, String)(6, "Grupo Qx"))
        ListConditionType.Add(New Tuple(Of Integer, String)(7, "Rango UVR"))
        INDsleConditionType.Properties.DataSource = ListConditionType.ToList()

        'Tipo de Condiciones Segunda
        ListConditionTypeSecond = New List(Of Tuple(Of Integer, String))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(3, "Tipo de Unidad"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(4, "Tipo de Estancia"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(5, "Tipo de Manual"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(6, "Grupo Qx"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(7, "Rango UVR"))
        INDsleConditionTypeSecond.Properties.DataSource = ListConditionTypeSecond.ToList()

        'Operador Logico
        ListLogicOperator = New List(Of Tuple(Of Integer, String))
        ListLogicOperator.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("LogicOperatorNever", NAME_MODULE)))
        ListLogicOperator.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("LogicOperatorAnd", NAME_MODULE)))
        ListLogicOperator.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("LogicOperatorOr", NAME_MODULE)))
        INDsleLogicOperator.Properties.DataSource = ListLogicOperator.ToList()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        RuleType = Nothing
        ListXpCollection = Nothing
        CleanControlsPartial(False)
        Me.EnableControlsModeEdit = True
        INDsleRuleType.Focus()
    End Sub

    ''' <summary>
    ''' Limpia solo algunos controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPartial(Optional focus As Boolean = True)
        INDsleRuleType.Properties.ReadOnly = False
        INDsleLogicOperator.Properties.ReadOnly = False
        INDsleConditionType.Properties.ReadOnly = False
        INDsleConditionTypeSecond.Properties.ReadOnly = False
        ConditionType = Nothing
        LogicOperator = 1
        ConditionTypeSecond = Nothing
        Me.IncludeToCUPSEntityId = Nothing
        INDLciDetailConditions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.ListBillingItemsRestrictionDetailCondition = Nothing
        INDGcBillingItemsRestrictionDetailCondition.DataSource = Nothing
        INDGcBillingItemsRestrictionDetailCondition.RefreshDataSource()
        If focus Then
            INDsleConditionType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Muestra u oculta las columnas dependiendo del tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideColumnsOfGridControlsRuleType()

        Dim ListStringNames As New List(Of String)
        INDlyItemControlRuleType.Text = ResourceManager.GetString("ItemsRestrictionRuleType" + RuleType.ToString(), NAME_MODULE)
        If RuleType = Utils.EItemsRestrictionRuleType.General Then
            INDpceControlRuleType.Text = "1 item seleccionado"
            INDpceControlRuleType.Properties.ReadOnly = True
        Else
            ListStringNames.Add("INDcolSelectionOption")
            INDpceControlRuleType.Text = "0 item seleccionado"
            INDpceControlRuleType.Properties.ReadOnly = False
        End If

        Select Case RuleType
            Case Utils.EItemsRestrictionRuleType.CUPS 'CUPS
                ListStringNames.Add("INDcolGroupCups")
                ListStringNames.Add("INDcolSubGroupCups")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolNameCUPS")
            Case Utils.EItemsRestrictionRuleType.SubGroupCUPS 'CupsSubGroup
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolGroup")
            Case Utils.EItemsRestrictionRuleType.GroupCUPS, Utils.EItemsRestrictionRuleType.GroupProduct, Utils.EItemsRestrictionRuleType.SubGroupProduct 'CupsGroup
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
            Case Utils.EItemsRestrictionRuleType.Product
                ListStringNames.Add("INDcolGroupProduct")
                ListStringNames.Add("INDcolSubGroupProduct")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
        End Select
        FieldsGrid(ListStringNames)
    End Sub

    ''' <summary>
    ''' Metodo que recorre las columnas de la rejilla y las coloca visible 
    ''' dependiendo del listado de colName que le envien
    ''' </summary>
    ''' <param name="ListStringNames"></param>
    ''' <remarks></remarks>
    Private Sub FieldsGrid(ByVal ListStringNames As List(Of String))
        ''Asigno si la columna es visible o no dependiendo del listado que envien anteriormente
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            For iList = 0 To ListStringNames.Count - 1
                If viewControlsRuleType.Columns.Item(iColumns).Name = ListStringNames.Item(iList) Then
                    viewControlsRuleType.Columns.Item(iColumns).Visible = True
                    Exit For
                Else
                    viewControlsRuleType.Columns.Item(iColumns).Visible = False
                End If
            Next
        Next

        'Asigno los visibleIndex para que aparezcan en orden las columnas
        Dim cont As Integer = 0
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            If viewControlsRuleType.Columns.Item(iColumns).Visible = True Then
                viewControlsRuleType.Columns.Item(iColumns).VisibleIndex = cont
                cont += 1
            End If
        Next

        Select Case RuleType
            Case Utils.EItemsRestrictionRuleType.CUPS
                INDcolGroupCups.GroupIndex = 0
                INDcolSubGroupCups.GroupIndex = 1
            Case Utils.EItemsRestrictionRuleType.Product
                INDcolGroupProduct.GroupIndex = 0
                INDcolSubGroupProduct.GroupIndex = 1
            Case Else
                INDcolGroupCups.GroupIndex = -1
                INDcolSubGroupCups.GroupIndex = -1
                INDcolGroupProduct.GroupIndex = -1
                INDcolSubGroupProduct.GroupIndex = -1
        End Select
    End Sub

    ''' <summary>
    ''' funcion que valida la condicion 1 y el operador logico para saber si muestra la condicion #2
    ''' </summary>
    ''' <param name="conditionType"></param>
    ''' <param name="logicOperator"></param>
    Private Sub ValidateConditionAndLogicalOperator(conditionType As Integer?, logicOperator As Integer?)
        Dim flagCondition As Boolean = conditionType Is Nothing OrElse conditionType = 1
        INDlyitemConditionTypeSecond.HideControl(flagCondition OrElse logicOperator Is Nothing OrElse logicOperator = Utils.ELogicalOperators.Nothing)
        INDLciDetailConditions.HideControl(flagCondition)
        INDsleLogicOperator.ReadOnly = flagCondition
        If flagCondition Then
            Me.LogicOperator = Utils.ELogicalOperators.Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo de la rejilla de los controles
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridControls(optionCheck As Integer)
        Dim view As GridView = viewControlsRuleType
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), optionCheck)
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck
                End If
            Next
        End If
        INDgcControlsRuleType.RefreshDataSource()
        Dim cont = (From l In ListXpCollection Where l.SelectOption = True Select l).Count
        INDpceControlRuleType.Text = cont.ToString + " item seleccionado"

        If cont = ListXpCollection.Count Then
            Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
        Else
            Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    row.SelectOption = False
                Else
                    row.SelectOption = True
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsForm() As String
        Dim listErrors As New StringBuilder
        'Se valida que haya al menos un item en la rejilla siempre y cuando el grupo de tarifas esta visible
        Dim listControlRuleTypeSelected = ListXpCollection?.ToEntityList(Of Object)?.FindAll(Function(x) x.SelectOption = True)
        If Not Me.ModeEdit AndAlso (RuleType IsNot Nothing AndAlso RuleType <> Utils.EItemsRestrictionRuleType.General) _
            AndAlso (listControlRuleTypeSelected Is Nothing OrElse Not listControlRuleTypeSelected.Any()) Then
            listErrors.AppendLine($"Debe seleccionar por lo menos un {ResourceManager.GetString("ItemsRestrictionRuleType" + RuleType.ToString(), NAME_MODULE)} ")
        End If

        If Me.ConditionType Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una condición")
        End If

        If Me.LogicOperator Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una opción de operador lógico")
        End If

        If (Me.ConditionType <> Utils.EItemsRestrictionConditionType.Nothing AndAlso Me.LogicOperator <> Utils.ELogicalOperators.Nothing) AndAlso Me.ConditionTypeSecond Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una segunda condición si tiene seleccionado un operador lógico ")
        End If

        If Me.ConditionType <> Utils.EItemsRestrictionConditionType.Nothing AndAlso (Me.ListBillingItemsRestrictionDetailCondition Is Nothing OrElse Not Me.ListBillingItemsRestrictionDetailCondition?.Any()) Then
            listErrors.AppendLine("Debe agrega por lo menos un detalle ")
        End If

        If Me.IncludeToCUPSEntityId Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un CUPS, en donde se va incluir el servicio")
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que agrega la regla al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddRuleRestriction()
        'Se valida que los controles esten llenos
        If ValidateControls() = False Then
            Exit Sub
        End If

        Dim errors As String = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        Try
            AsyncLoader(True)
            If ModeEdit AndAlso (ListBillingItemsRestrictionDetail Is Nothing OrElse Not ListBillingItemsRestrictionDetail?.Any()) Then
                Mensaje(EeventViewerImages.Advertencia) = "Se esta trantando de editar un detalle pero este viene vacio"
                Exit Sub
            End If

            If Not ModeEdit Then
                Me.ListBillingItemsRestrictionDetail = New List(Of BillingItemsRestrictionDetail)
            End If

            Dim result = Me.AssingValues()

            If result Is Nothing OrElse Not result?.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = result?.Message
                Exit Sub
            End If

            'Se instancia el objeto que se envia al evento
            Dim args As New AddInfoToGridBillingItemsRestriction With {.ListBillingItemsRestrictionDetail = ListBillingItemsRestrictionDetail, .ReturnValueOk = True, .ModeEdit = ModeEdit}
            RaiseEvent AddInfoToGridBillingItemsRestriction(Nothing, args)

            AsyncLoader(False)

            If args.ReturnValueOk Then
                CleanControlsPartial()
                ModeEdit = False
                Me.EnableControlsModeEdit = True
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' funcion encargada por referencia de agrega objetos BillingItemsRestrictionDetail a una lista para agregarlos o modificarlos en la rejilla principal
    ''' </summary>
    ''' <returns></returns>
    Private Function AssingValues() As ActionResult
        Dim result As ActionResult(Of BillingItemsRestrictionDetail)

        If Me.ModeEdit Then
            Dim obj = ListBillingItemsRestrictionDetail?.FirstOrDefault
            result = CreateBillingItemsRestrictionDetailObject(obj)

            If result Is Nothing OrElse Not result?.StateResult Then
                Return New ActionResult With {.StateResult = False, .Message = result?.Message}
            End If

            Me.ListBillingItemsRestrictionDetail.Add(result.ObjectEmbbeded)
        End If

        If Me.RuleType = Utils.EItemsRestrictionRuleType.General Then
            result = CreateBillingItemsRestrictionDetailObject(Me.RuleType,
                                                                    Me.ConditionType,
                                                                    Me.LogicOperator,
                                                                    Me.ConditionTypeSecond,
                                                                    Me.IncludeToCUPSEntityId,
                                                                    Me.INDSleIncludeToCUPS.Text)
            If result Is Nothing OrElse Not result?.StateResult Then
                Return New ActionResult With {.StateResult = False, .Message = result?.Message}
            End If

            Me.ListBillingItemsRestrictionDetail.Add(result.ObjectEmbbeded)
        Else
            For Each item In (From l In ListXpCollection Where l.SelectOption Select l).ToList()
                result = CreateBillingItemsRestrictionDetailObject(Me.RuleType,
                                                                    Me.ConditionType,
                                                                    Me.LogicOperator,
                                                                    Me.ConditionTypeSecond,
                                                                    Me.IncludeToCUPSEntityId,
                                                                    Me.INDSleIncludeToCUPS.Text,
                                                                    item.Id,
                                                                    If(RuleType = Utils.EItemsRestrictionRuleType.CUPS, item.CodeDescription, item.CodeName))
                If result Is Nothing OrElse Not result?.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = result?.Message}
                End If

                Me.ListBillingItemsRestrictionDetail.Add(result.ObjectEmbbeded)
            Next
        End If

        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' funcion encargada de construir el objeto BillingItemsRestrictionDetail
    ''' </summary>
    ''' <param name="ruleType"></param>
    ''' <param name="conditionType"></param>
    ''' <param name="logicalOperator"></param>
    ''' <param name="conditionType2"></param>
    ''' <param name="entityId"></param>
    ''' <param name="entityDescription"></param>
    ''' <returns></returns>
    Private Function CreateBillingItemsRestrictionDetailObject(ruleType As Utils.EItemsRestrictionRuleType, conditionType As Utils.EItemsRestrictionConditionType,
                                                               logicalOperator As Utils.ELogicalOperators, conditionType2 As Utils.EItemsRestrictionConditionType?,
                                                               includeToCUPSEntityId As Integer, includeToCUPSDescription As String,
                                                               Optional entityId As Integer? = Nothing, Optional entityDescription As String = Nothing) As ActionResult(Of BillingItemsRestrictionDetail)


        If ruleType <> Utils.EItemsRestrictionRuleType.General AndAlso (entityId Is Nothing OrElse String.IsNullOrEmpty(entityDescription)) Then
            Return New ActionResult(Of BillingItemsRestrictionDetail) With {.StateResult = False, .Message = "No se puede crear la regla si es diferente a general y no tiene un Id o descripción"}
        End If
        Dim NewItem = New BillingItemsRestrictionDetail
        With NewItem
            .RuleType = ruleType
            .RuleTypeName = $"{ResourceManager.GetString($"ItemsRestrictionRuleType{ruleType.AsByte}", NAME_MODULE)}"
            .RuleDescription = If(entityDescription, .RuleTypeName)
            .IncludeToCUPSEntityId = includeToCUPSEntityId
            .IncludeToCUPSDescription = includeToCUPSDescription
            Select Case ruleType
                Case Utils.EItemsRestrictionRuleType.CUPS
            .CUPSEntityId = entityId
            Case Utils.EItemsRestrictionRuleType.GroupCUPS
            .CUPSGroupId = entityId
            Case Utils.EItemsRestrictionRuleType.SubGroupCUPS
            .CUPSSubgroupId = entityId
            Case Utils.EItemsRestrictionRuleType.Product
            .ProductId = entityId
            Case Utils.EItemsRestrictionRuleType.GroupProduct
            .ProductGroupId = entityId
            Case Utils.EItemsRestrictionRuleType.SubGroupProduct
            .ProductSubGroupId = entityId
            Case Else
            End Select
            .ConditionType = conditionType
            .LogicalOperator = logicalOperator
            .ConditionsName = ResourceManager.GetString($"ItemsRestrictionConditionType{conditionType.AsByte}", NAME_MODULE)
            .ConditionType2 = Utils.EItemsRestrictionConditionType.Nothing

            If conditionType <> Utils.EItemsRestrictionConditionType.Nothing AndAlso logicalOperator <> Utils.ELogicalOperators.Nothing Then
                .ConditionType2 = conditionType2
                .ConditionsName = $"{ .ConditionsName} {ListLogicOperator.Find(Function(x) x.Item1 = logicalOperator.AsByte).Item2} {ResourceManager.GetString($"ItemsRestrictionConditionType{ .ConditionType2}", NAME_MODULE)}"
            End If

            If ListBillingItemsRestrictionDetailCondition?.Any() Then
                For Each item In Me.ListBillingItemsRestrictionDetailCondition
                    .BillingItemsRestrictionDetailCondition.Add(item.Clone())
                Next
            End If
        End With

        Return New ActionResult(Of BillingItemsRestrictionDetail) With {.StateResult = True, .ObjectEmbbeded = NewItem}

    End Function

    ''' <summary>
    ''' edita el objeto de detalle de servicios no facturables, solo se edita las condiciones
    ''' </summary>
    ''' <param name="billingItemsRestrictionDetail"></param>
    ''' <returns></returns>
    Private Function CreateBillingItemsRestrictionDetailObject(billingItemsRestrictionDetail As BillingItemsRestrictionDetail) As ActionResult(Of BillingItemsRestrictionDetail)

        If billingItemsRestrictionDetail Is Nothing Then
            Return New ActionResult(Of BillingItemsRestrictionDetail) With {.StateResult = False, .Message = "No se puede editar por el objeto vienen vacio"}
        End If

        With billingItemsRestrictionDetail
            For Each item In Me.ListBillingItemsRestrictionDetailCondition
                .BillingItemsRestrictionDetailCondition.Add(item)
            Next
        End With

        Return New ActionResult(Of BillingItemsRestrictionDetail) With {.StateResult = True, .ObjectEmbbeded = billingItemsRestrictionDetail}
    End Function


    ''' <summary>
    ''' abre el form para agregar condiciones de servicios No facturables
    ''' </summary>
    ''' <param name="editMode"></param>
    Private Sub OpenFormDetailCondition(editMode As Boolean, ByRef Optional billingItemsRestrictionDetailCondition As BillingItemsRestrictionDetailCondition = Nothing)
        If ConditionType Is Nothing OrElse ConditionType = Utils.EItemsRestrictionConditionType.Nothing Then
            Exit Sub
        End If

        Dim Formulario As FrmAddRuleRestrictionCondition
        If Me.LogicOperator <> Utils.ELogicalOperators.Nothing AndAlso Me.ConditionTypeSecond IsNot Nothing AndAlso Me.ConditionTypeSecond <> Utils.EItemsRestrictionConditionType.Nothing Then
            Formulario = New FrmAddRuleRestrictionCondition(ConditionType, ConditionTypeSecond)
        Else
            Formulario = New FrmAddRuleRestrictionCondition(ConditionType)
        End If

        Using Formulario
            Me.Cursor = ChangeCursorIndigo()
            Formulario.ViewModeEditHold = True
            AddHandler Formulario.AddInfoToGridBillingItemsRestrictionDetailCondition, AddressOf AddInfoToGridBillingItemsRestrictionDetailCondition
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1090
            Formulario.Height = 768
            Formulario.EditMode = editMode
            Formulario.HeaderLogicOperator = Me.INDsleLogicOperator.Text
            Formulario.BillingItemsRestrictionDetailCondition = billingItemsRestrictionDetailCondition
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para agrega las condiciones a la  rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddInfoToGridBillingItemsRestrictionDetailCondition(sender As Object, e As AddInfoToGridBillingItemsRestrictionCondition)
        If e?.BillingItemsRestrictionDetailCondition Is Nothing Then
            e.ReturnValueOk = False
            Mensaje(EeventViewerImages.Advertencia) = "El objeto de condicion llegó vacia"
            Exit Sub
        End If

        If Me.ListBillingItemsRestrictionDetailCondition Is Nothing Then
            Me.ListBillingItemsRestrictionDetailCondition = New List(Of BillingItemsRestrictionDetailCondition)
        End If

        Dim Obj As BillingItemsRestrictionDetailCondition = INDGvBillingItemsRestrictionDetailCondition.GetFocusedRow()

        If e.ModeEdit And Obj IsNot Nothing AndAlso Obj.Id = 0 Then
            Me.ListBillingItemsRestrictionDetailCondition.Remove(Obj)
        End If

        If Obj Is Nothing OrElse Obj.Id = 0 OrElse Not e.ModeEdit Then
            Me.ListBillingItemsRestrictionDetailCondition.Add(e.BillingItemsRestrictionDetailCondition)

        End If

        Me.DataSourceItemsRestrictionDetailCondition = Me.ListBillingItemsRestrictionDetailCondition.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
        Me.EnableControlsModeEdit = False
        e.ReturnValueOk = True
        Mensaje(EeventViewerImages.Informacion) = $"La condición fue {If(e.ModeEdit, "editada", "agregada")} correctamente"
    End Sub

#End Region

#Region "Events"
#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRuleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRuleType.EditValueChanged
        If RuleType Is Nothing Then
            Exit Sub
        End If
        Me.Cursor = ChangeCursorIndigo()
        HideColumnsOfGridControlsRuleType()
        If bw.IsBusy Then
            Exit Sub
        End If
        bw.RunWorkerAsync()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de operador lógico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLogicOperator_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLogicOperator.EditValueChanged, INDsleConditionType.EditValueChanged
        ValidateConditionAndLogicalOperator(Me.ConditionType, Me.LogicOperator)
        If Me.ConditionType IsNot Nothing AndAlso Me.ConditionTypeSecond IsNot Nothing AndAlso Me.ConditionType = Me.ConditionTypeSecond Then
            Me.ConditionTypeSecond = Nothing
        End If
    End Sub
#End Region

#Region "DoubleClick"
    ''' <summary>
    ''' Evento que se dispara al presionar doble click sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcControlsRuleType_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcControlsRuleType.MouseDoubleClick
        If ListXpCollection IsNot Nothing AndAlso ListXpCollection.Count > 0 Then
            Dim hitPoint = Me.viewControlsRuleType.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelectionOption") Then

                    Dim listFilterXpCollection = viewControlsRuleType.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcControlsRuleType.RefreshDataSource()
                    Dim contItems = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
                    INDpceControlRuleType.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcControlsRuleType.Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al hacer popup sobre el control de la segunda condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConditionTypeSecond_Popup(sender As Object, e As EventArgs) Handles INDsleConditionTypeSecond.Popup
        If ConditionType IsNot Nothing Then
            viewSearchFirstCondition.ActiveFilterString = "Item1<>'" & ConditionType & "' And Item1<>1"
        Else
            viewSearchFirstCondition.ActiveFilterString = String.Empty
        End If
        viewSearchFirstCondition.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' evento click para agrega una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddRestrictionCondition_Click(sender As Object, e As EventArgs) Handles INDSbAddRestrictionCondition.Click
        OpenFormDetailCondition(False)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRuleRestriction_Click(sender As Object, e As EventArgs) Handles INDbtnAddRuleRestriction.Click
        AddRuleRestriction()
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento que se dispara para pintar el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewControlsRuleType_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles viewControlsRuleType.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            Dim view = CType(sender, GridView)
            viewNameFocus = view.Name
            INDbarButtonSelectAll.Caption = "Seleccionar"
            INDbarButtonUnSelectAll.Caption = "Quitar Selección"
            PopupMenuActions.Manager = BarManager
            PopupMenuActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
        End If
    End Sub
#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Menu de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag.ToString
            Case "Edit"
                Dim Obj As BillingItemsRestrictionDetailCondition = INDGvBillingItemsRestrictionDetailCondition.GetFocusedRow()
                OpenFormDetailCondition(True, Obj)
            Case "Remove"
                Dim Obj As BillingItemsRestrictionDetailCondition = INDGvBillingItemsRestrictionDetailCondition.GetFocusedRow()
                If Obj IsNot Nothing AndAlso Obj.Id > 0 Then
                    Obj.MarkAsDeleted()
                Else
                    Me.ListBillingItemsRestrictionDetailCondition.Remove(Obj)
                End If
                Me.DataSourceItemsRestrictionDetailCondition = Me.ListBillingItemsRestrictionDetailCondition.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
                Me.EnableControlsModeEdit = Not Me.ListBillingItemsRestrictionDetailCondition.Any() AndAlso Not ModeEdit
        End Select
    End Sub
#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Selecciona
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        SelectOptionsGridControls(1)
    End Sub

    ''' <summary>
    ''' Deselecciona
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        SelectOptionsGridControls(0)
    End Sub

#End Region

#Region "EditValueChanging"
    Private Sub INDrepCheckControlsLiquidationType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckControlsLiquidationType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewControlsRuleType.GetFocusedRow()

            item.SelectOption = e.NewValue
            Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
            INDpceControlRuleType.Text = cont.ToString + " item seleccionado"

            If cont = ListXpCollection.Count Then
                Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' evento para cargar el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIncludeToCUPS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleIncludeToCUPS.QueryPopUp
        If Me.DataSourceIncludeToCUPSEntity Is Nothing Then
            Presenter.InitializateCUPDataSource()
        End If
    End Sub
#End Region

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        ModeEdit = False
    End Sub
#End Region

End Class

Public Class AddInfoToGridBillingItemsRestriction
    Inherits EventArgs

    ''' <summary>
    ''' lista  detalle de restricciones
    ''' </summary>
    ''' <returns></returns>
    Property ListBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail)

    ''' <summary>
    ''' Establece si el retorno es satisfactorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReturnValueOk As Boolean

    ''' <summary>
    ''' Establece si el registro es para modificar o guardar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ModeEdit As Boolean

End Class