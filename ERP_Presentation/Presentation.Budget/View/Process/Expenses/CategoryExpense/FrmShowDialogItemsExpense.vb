#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Contiene la vista de el showdialog para registrar rubros
''' </summary>
''' <remarks></remarks>
Public Class FrmShowDialogItemsExpense
    Implements IShowDialogItems

#Region "Builder"

    Public ctrTmp As CtrInfo

    Sub New(_budgetaryValidityId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        BudGetaryValidityId = _budgetaryValidityId

        ctrTmp = New CtrInfo()
        ctrTmp.SetTotalValues(AddressOf getInfo)
        ctrTmp.RefreshInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getInfo() As Tuple(Of String, String, String)
        Return New Tuple(Of String, String, String)(BudgetaryEntityCodeName, ValidityYear, StatusValidity)
    End Function

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

#End Region

#Region "Globals"

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim BudGetaryValidityId As Integer

    ''' <summary>
    ''' Variable para controlar la entidad presente en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim Category As Category

    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim _Indigo As SessionValues

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PShowDialogItem

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Se utiliza para tener en cuenta la accion a realizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim actionToDo As EActionsToDo

    ''' <summary>
    ''' Representa a la entidad de parametro de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Dim settingBudget As SettingsBudget

    ''' <summary>
    ''' Variable que controla el tipo de rubro para el funcional en general
    ''' </summary>
    ''' <remarks></remarks>
    Dim _itemType As EItemType

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IShowDialogItems.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IShowDialogItems.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el codigo y el nombre de la entidad presupuestal 
    ''' que se va a mostrar en el control
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetaryEntityCodeName As String
    Public Property BudgetaryEntityCodeName As String
        Get
            Return _budgetaryEntityCodeName
        End Get
        Set(value As String)
            _budgetaryEntityCodeName = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IShowDialogItems.Code
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece el codigo alternativo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AlternativeCode As String Implements IShowDialogItems.AlternativeCode
        Get
            Return INDtxtAlternativeCode.EditValue
        End Get
        Set(value As String)
            INDtxtAlternativeCode.EditValue = value
        End Set
    End Property

    ''' <summary>
	''' Obtiene o establece el Codigo CCPET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CCPETCodeId As Integer? Implements IShowDialogItems.CCPETCodeId
        Get
            Return INDsleCCPETCode.EditValue
        End Get
        Set(value As Integer?)
            INDsleCCPETCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Codigo CPC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CPCCodeId As Integer? Implements IShowDialogItems.CPCCodeId
        Get
            Return INDsleCPCCode.EditValue
        End Get
        Set(value As Integer?)
            INDsleCPCCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryName As String Implements IShowDialogItems.CategoryName
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParentId As Integer? Implements IShowDialogItems.ParentId
        Get
            Return INDsleParentItem.EditValue
        End Get
        Set(value As Integer?)
            INDsleParentItem.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinancialSourceId As Integer? Implements IShowDialogItems.FinancialSourceId
        Get
            Return INDsleFinancialSource.EditValue
        End Get
        Set(value As Integer?)
            INDsleFinancialSource.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PublicPolicyId As Integer? Implements IShowDialogItems.PublicPolicyId
        Get
            Return INDslePublicPolicy.EditValue
        End Get
        Set(value As Integer?)
            INDslePublicPolicy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si maneja control de PAC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PAC As Boolean? Implements IShowDialogItems.PAC
        Get
            Return INDslePAC.EditValue
        End Get
        Set(value As Boolean?)
            INDslePAC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Representa al objeto seleccionado en el form principal
    ''' para poder saber el codigo y si es padre
    ''' </summary>
    ''' <remarks></remarks>
    Private _FocusedBudgetItem As Category
    Public Property FocusedBudgetItem As Category
        Get
            Return _FocusedBudgetItem
        End Get
        Set(value As Category)
            _FocusedBudgetItem = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el rubro pertenece al reporte de deficit o
    ''' equilibrio ptal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BalanceDeficit As Boolean? Implements IShowDialogItems.BalanceDeficit
        Get
            Return INDsleBalanceDeficit.EditValue
        End Get
        Set(value As Boolean?)
            INDsleBalanceDeficit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el rubro ingreso es de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncomeCxP As Boolean? Implements IShowDialogItems.IncomeCxP
        Get
            Return INDsleIncomeCxP.EditValue
        End Get
        Set(value As Boolean?)
            INDsleIncomeCxP.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la Situación de Fondos
    ''' </summary>
    ''' <returns></returns>
    Public Property FundSituation As Char Implements IShowDialogItems.FundSituation
        Get
            Return INDGleFundSituation.EditValue
        End Get
        Set(value As Char)
            INDGleFundSituation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la vigencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Validity As Integer Implements IShowDialogItems.Validity
        Get
            Return INDGleValidity.EditValue
        End Get
        Set(value As Integer)
            INDGleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean
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
    ''' Obtiene el estado de la vigencia para visualizarla en el control
    ''' de información
    ''' </summary>
    ''' <remarks></remarks>
    Private _StatusValidity As String
    Public Property StatusValidity As String
        Get
            Return _StatusValidity
        End Get
        Set(value As String)
            _StatusValidity = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el año que se va a mostrar en el control
    ''' </summary>
    ''' <remarks></remarks>
    Private _validityYear As String
    Public Property ValidityYear As String
        Get
            Return _validityYear
        End Get
        Set(value As String)
            _validityYear = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el id y el codigo-nombre de la categoria padre
    ''' al momento de realizar click derecho agregar rubro hijo
    ''' en el form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private _tuple As Tuple(Of Integer, String)
    Public Property TupleIdAndCodeNameCategoryParent As Tuple(Of Integer, String)
        Get
            Return _tuple
        End Get
        Set(value As Tuple(Of Integer, String))
            _tuple = value
        End Set
    End Property



#End Region

#Region "DataSource"

    ''' <summary>
    ''' Establece el datasource de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinancialSourceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IShowDialogItems.FinancialSourceXpo
        Get
            Return INDsleFinancialSource.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFinancialSource.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la política pública
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PublicPolicyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IShowDialogItems.PublicPolicyXpo
        Get
            Return INDslePublicPolicy.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePublicPolicy.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del rubro padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParentXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IShowDialogItems.ParentXpo
        Get
            Return INDsleParentItem.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleParentItem.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los codigo CCPET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CCPETXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IShowDialogItems.CCPETXpo
        Get
            Return INDsleCCPETCode.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCCPETCode.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los codigo CPC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CPCCatalogXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IShowDialogItems.CPCCatalogXpo
        Get
            Return INDsleCPCCode.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCPCCode.Properties.DataSource = value
        End Set
    End Property

    Private _FillingFundSituation As List(Of Tuple(Of Char, String))
    ''' <summary>
    ''' Propiedad para capturar el tipo de situacion de Fondo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingFundSituation As List(Of Tuple(Of Char, String))
        Get
            If _FillingFundSituation Is Nothing Then
                _FillingFundSituation = New List(Of Tuple(Of Char, String))
                _FillingFundSituation.Add(New Tuple(Of Char, String)("C", "Con Situación de Fondos"))
                _FillingFundSituation.Add(New Tuple(Of Char, String)("S", "Sin Situación de Fondos"))
            End If
            Return _FillingFundSituation
        End Get
    End Property

    Private _FillingValidity As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Propiedad para capturar el tipo de Vigencia
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingValidity As List(Of Tuple(Of Byte, String))
        Get
            If _FillingValidity Is Nothing Then
                _FillingValidity = New List(Of Tuple(Of Byte, String))
                _FillingValidity.Add(New Tuple(Of Byte, String)(1, "Vigencia Actual"))
                _FillingValidity.Add(New Tuple(Of Byte, String)(2, "Reservas"))
                _FillingValidity.Add(New Tuple(Of Byte, String)(3, "Cuentas por pagar"))
                _FillingValidity.Add(New Tuple(Of Byte, String)(4, "Vigencias futuras - Vigencia Actual"))
                _FillingValidity.Add(New Tuple(Of Byte, String)(5, "Vigencias futuras - Reservas"))
                _FillingValidity.Add(New Tuple(Of Byte, String)(6, "Vigencias futuras - Cuentas por pagar"))
            End If
            Return _FillingValidity
        End Get
    End Property
#End Region

#Region "Events"

    ''' <summary>
    ''' Evento publico para actualizar el datasource del treeList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event UpdateDatasourceTreeList(sender As Object, e As EventArgs)

#Region "Load"

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmShowDialogItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetItem, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PShowDialogItem(Me)
        presenter.LoadDefinitionLayout()
        '******************************'
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        INDGleFundSituation.Properties.DataSource = FillingFundSituation
        INDGleValidity.Properties.DataSource = FillingValidity
        LoadStatus()
        ctrTmp.RefreshInfo()
        If FocusedBudgetItem Is Nothing Then
            Deshacer()
            If TupleIdAndCodeNameCategoryParent IsNot Nothing Then
                ParentId = TupleIdAndCodeNameCategoryParent.Item1
                INDsleParentItem.Properties.NullText = TupleIdAndCodeNameCategoryParent.Item2
                INDsleParentItem.Properties.ReadOnly = True
            End If
        Else
            FinancialSourceId = FocusedBudgetItem.FinancialSourceId
            Code = FocusedBudgetItem.Code
            Await ValidateCode()
            ControllerReadOnly()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        BudGetaryValidityId = Nothing
        Category = Nothing
        SearchMode = Nothing
        blockRecord = Nothing
        presenter = Nothing
        _idOperativeUnit = Nothing
        actionToDo = Nothing
        settingBudget = Nothing
        _itemType = Nothing
    End Sub

#End Region

#Region "Activated"
    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmShowDialogItems_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDsleFinancialSource.Enabled = True Then
            INDsleFinancialSource.Focus()
        Else
            INDtxtName.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmShowDialogItems_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub


#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Captura la tecla enter, para buscar un regitro por codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await ValidateCode()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de fuente de financiacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFinancialSource_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFinancialSource.QueryPopUp
        If FinancialSourceXpo Is Nothing Then
            presenter.InitializeFinancialSource(BudGetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rubro padre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleParentItem_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleParentItem.QueryPopUp
        If ParentXpo Is Nothing Then
            presenter.InitializeParent(BudGetaryValidityId, 2)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos padre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCCPETCode_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCCPETCode.QueryPopUp
        If CCPETXpo Is Nothing Then
            presenter.InitializeCCPETCode(2)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos padre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCPCCode_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCPCCode.QueryPopUp
        If CPCCatalogXpo Is Nothing Then
            presenter.InitializeCPCCode()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de política pública
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePublicPolicy_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePublicPolicy.QueryPopUp
        If PublicPolicyXpo Is Nothing Then
            presenter.InitializePublicPolicy()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fuente de financiacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFinancialSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFinancialSource.EditValueChanged
        If FinancialSourceId IsNot Nothing Then
            INDsleBalanceDeficit.Properties.ReadOnly = False
            INDsleIncomeCxP.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del codigo de busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbteCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDbteCode.EditValueChanged
        CCPETXpo = Nothing
        INDsleCCPETCode.EditValue = Nothing
        INDsleCCPETCode.Properties.NullText = String.Empty

        CPCCatalogXpo = Nothing
        INDsleCPCCode.EditValue = Nothing
        INDsleCPCCode.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del codigo CCPET para saber si vincula o no CPC
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCCPETCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCCPETCode.EditValueChanged
        If INDsleCCPETCode.EditValue IsNot Nothing Then
            Using Model As New MBudgetCCPET
                Dim resultCCPET As ActionResult(Of CCPET) = Await Model.GetCCPETById(CCPETCodeId)
                If resultCCPET.StateResult = False Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = resultCCPET.Message
                    Exit Sub
                End If
                Dim _cCPET As CCPET = resultCCPET.ObjectEmbbeded
                If Not _cCPET Is Nothing AndAlso _cCPET.Id > 0 Then
                    If _cCPET.LinkAccount Then
                        INDlyCPCCatalog.HideControl(False)
                    Else
                        INDsleCPCCode.Properties.NullText = String.Empty

                        INDlyCPCCatalog.HideControl(True)
                        CPCCodeId = Nothing

                    End If
                End If
            End Using
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyBudgetItem.BeginUpdate()

        FinancialSourceId = Nothing
        INDsleFinancialSource.Properties.NullText = String.Empty
        INDsleFinancialSource.Properties.ReadOnly = False

        INDsleCCPETCode.Properties.NullText = String.Empty
        INDsleCCPETCode.Properties.ReadOnly = False

        INDslePublicPolicy.Properties.NullText = String.Empty
        INDslePublicPolicy.Properties.ReadOnly = False

        INDsleCPCCode.Properties.NullText = String.Empty
        INDlyCPCCatalog.HideControl(True)

        Code = String.Empty
        CategoryName = String.Empty
        AlternativeCode = String.Empty
        ParentId = Nothing
        CCPETCodeId = Nothing
        CPCCodeId = Nothing

        INDsleParentItem.Properties.NullText = String.Empty
        INDsleParentItem.Properties.ReadOnly = False

        BalanceDeficit = False
        INDsleBalanceDeficit.Properties.ReadOnly = True
        PAC = False
        IncomeCxP = False
        INDsleIncomeCxP.Properties.ReadOnly = True

        ParentXpo = Nothing
        Category = Nothing

        INDGleFundSituation.Text = String.Empty
        INDGleFundSituation.Properties.ReadOnly = False

        INDGleValidity.Text = String.Empty
        INDGleValidity.Properties.ReadOnly = False

        INDslePublicPolicy.Properties.ReadOnly = False
        INDslePublicPolicy.Text = String.Empty
        DeleteBlockedRecord()

        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        ActionsOnControls = False

        INDlyBudgetItem.EndUpdate()
    End Sub

    ''' <summary>
    ''' Propiedad para controlar la accion que se hace sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyBudgetItem.BeginUpdate()

            INDsleFinancialSource.Enabled = Not value
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtAlternativeCode.Enabled = value
            INDsleParentItem.Enabled = value
            INDsleBalanceDeficit.Enabled = value
            INDslePAC.Enabled = value
            INDsleIncomeCxP.Enabled = value
            INDsleCCPETCode.Enabled = value
            INDsleCPCCode.Enabled = value
            INDGleFundSituation.Enabled = value
            INDGleValidity.Enabled = value
            INDslePublicPolicy.Enabled = value

            INDlyBudgetItem.EndUpdate()

            If value = True Then
                INDtxtName.Focus()
            Else
                INDsleFinancialSource.Focus()
            End If
        End Set
    End Property


    ''' <summary>
    ''' Metodo que valida el codigo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ValidateCode() As Task
        If String.IsNullOrEmpty(Code) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un Código."
            INDbteCode.Focus()
            Exit Function
        End If
        Await LoadControls()
    End Function


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using Model As New MBudgetItem
            AsyncLoader(True)
            INDlyBudgetItem.BeginUpdate()
            Dim resultCategory As ActionResult(Of Category) = Await Model.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId, Code, BudGetaryValidityId, 2)
            If resultCategory.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = resultCategory.Message
                Exit Function
            End If
            Category = resultCategory.ObjectEmbbeded
            If Not Category Is Nothing AndAlso Category.Id > 0 Then
                Me.BarraBotones.StatusRecordVisible = True
                Dim result = Await Model.GetBlockRecord(Me.Tag, Category.Id)
                With Category
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                    FinancialSourceId = .FinancialSourceId
                    INDsleFinancialSource.Properties.NullText = .FinancialSourceDescription
                    INDslePublicPolicy.Properties.NullText = .PublicPolicyDescription

                    Code = .Code
                    CategoryName = .Name
                    AlternativeCode = .AlternativeCode
                    CCPETCodeId = .CCPETCodeId
                    CPCCodeId = .CPCCodeId
                    INDsleCCPETCode.Properties.NullText = .CCPETDescription
                    INDsleCPCCode.Properties.NullText = .CPCDescription
                    ParentId = .CategoryOwnerId
                    INDsleParentItem.Properties.NullText = .ParentDescription
                    BalanceDeficit = .BalanceDeficit
                    PAC = .PAC
                    IncomeCxP = .IncomeCxP
                    Me.Status = .Status
                    FundSituation = .FundSituation
                    Validity = .Validity
                    PublicPolicyId = .PublicPolicyId

                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Category.Code)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(Category.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Category.Id}
                    Dim operation = Await Model.SaveBlockRecord(blockRecord)
                    blockRecord = operation.ObjectEmbbeded
                Else
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    blockRecord = result
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True

                FocusedBudgetItem = Category.Clone
                ControllerReadOnly()

                AsyncLoader(False)
                ActionsOnControls = True
            Else
                AsyncLoader(False)
                Category = New Category With {.Status = True}
                AlternativeCode = Code
                Me.ActionsOnControls = True
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End Using
        INDlyBudgetItem.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodos para asignar valores a la entidad financial source
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With Category
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .BudgetaryValidityId = BudGetaryValidityId
            .ItemType = 2
            .Code = Code
            .AlternativeCode = AlternativeCode
            .CCPETCodeId = CCPETCodeId
            .CPCCodeId = CPCCodeId
            .Name = CategoryName
            .CategoryOwnerId = ParentId
            .FinancialSourceId = FinancialSourceId
            .Auxiliary = IIf(.FinancialSourceId IsNot Nothing, True, False)
            .BalanceDeficit = BalanceDeficit
            .PAC = PAC
            .IncomeCxP = IncomeCxP
            .FundSituation = FundSituation
            .Validity = Validity
            .PublicPolicyId = PublicPolicyId
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MEarningsType
                Await Model.DeleteBlockRecord(blockRecord)
            End Using

            blockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Category IsNot Nothing AndAlso Me.Category.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Category.Code, Me.Category.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Category.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Category.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Category.Code, Me.Category.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Category.Code)
            Return Me._doc
        End If
    End Function


#End Region

#Region "Icrud Base"

    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If SearchMode = False Then
            'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
        INDbteCode.Focus()
    End Sub

    ''' <summary>
    ''' MEtodo Cuando se da click en boton nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        Await ValidateCode()
    End Sub

    ''' <summary>
    ''' MEtodo para buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para abrir busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo With {.Caption = "Código", .FieldName = "Code"},
                New ColumnInfo With {.Caption = "Descripción", .FieldName = "Name"},
                New ColumnInfo With {.Caption = "Recurso o Fuente", .FieldName = "FinancialSourceId.NameCode"}}.ToList()
            .ValorSolicitado = "Code"
            Dim itemType As Integer = 2
            Dim filter As String = BudGetaryValidityId.ToString + " - " + itemType.ToString
            .FiltroBusqueda = filter
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCategoryByBudgetaryValidityIdAndItemType
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        If ReturnObject IsNot Nothing AndAlso ReturnObject.FinancialSourceId IsNot Nothing Then
            FinancialSourceId = ReturnObject.FinancialSourceId.Id
            INDsleFinancialSource.Properties.NullText = ReturnObject.FinancialSourceId.NameCode
        End If
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If INDsleCPCCode.EditValue Is Nothing Then
            INDlyCPCCatalog.HideControl()
        End If
        If ValidateControls() = False Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MBudgetItem
                AsyncLoader(True)
                Dim Result = Await model.SaveBudgetItemAsync(Category)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If Category.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    ElseIf Category.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.Category = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    SearchMode = False
                    Me.Deshacer()
                    RaiseEvent UpdateDatasourceTreeList(Nothing, EventArgs.Empty)
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        Try
            If Not String.IsNullOrEmpty(Code) Then
                Using model As New MBudgetItem()
                    AsyncLoader(True)
                    Dim state As Boolean = Not Category.Status
                    Dim Result = Await model.ChangeState(Category, state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Category = Result.ObjectEmbbeded
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obsoleto
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Metodo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        Try
            If Category IsNot Nothing AndAlso Category.Id > -1 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using model As New MBudgetItem
                        AsyncLoader(True)
                        Category.MarkAsDeleted()
                        Dim result = Await model.DeleteBudgetItemAsync(Category)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            SearchMode = False
                            Me.Deshacer()
                            RaiseEvent UpdateDatasourceTreeList(Nothing, EventArgs.Empty)
                        Else
                            AsyncLoader(False)
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            ElseIf result.MessageResult(0) = "-111" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Propiedad que establece los mensajes 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Metodo que permite poner en readOnly algunos controles
    ''' dependiendo si el rubro es padre o hijo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ControllerReadOnly()
        If FocusedBudgetItem IsNot Nothing Then
            INDsleFinancialSource.Properties.ReadOnly = True
            INDsleParentItem.Properties.ReadOnly = True
            If FocusedBudgetItem.CategoryOwnerId IsNot Nothing Then
                If FocusedBudgetItem.FinancialSourceId IsNot Nothing Then
                    INDsleBalanceDeficit.Properties.ReadOnly = False
                    INDsleIncomeCxP.Properties.ReadOnly = False
                Else
                    INDsleBalanceDeficit.Properties.ReadOnly = True
                    INDsleIncomeCxP.Properties.ReadOnly = True
                End If
            Else
                INDsleBalanceDeficit.Properties.ReadOnly = True
                INDsleIncomeCxP.Properties.ReadOnly = True
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de agregar en el search de fuente de financiacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFinancialSource_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFinancialSource.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
                Dim size As System.Drawing.Size
                size.Width = 780
                size.Height = 768
                Using pop As New FrmTransparent(New FrmFinancialSource With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                    pop.Show()
                End Using
                presenter.InitializeFinancialSource(BudGetaryValidityId)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de agregar en el search de política Pública 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePublicPolicy_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePublicPolicy.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
                Dim size As System.Drawing.Size
                size.Width = 780
                size.Height = 768
                Using pop As New FrmTransparent(New FrmPublicPolicy With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, .Size = size}, False)
                    pop.Show()
                End Using
                presenter.InitializePublicPolicy()
            End If
        End If
    End Sub
#End Region

#Region "Evento Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

End Class













