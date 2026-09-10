'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
'
' Modified         : Carlos Mario Arias Rubiano
' DateModified     : 22/07/2015
' Description      : Se refactoriza todo el formulario
'
' Modified         : Jeisson Herrera Peña
' DateModified     : 10/08/2015
' Description      : Se refactoriza todo el formulario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Windows.Forms
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
''' Contiene la vista de el frontal tipos de ingreso
''' </summary>
''' <remarks></remarks>
Public Class FrmEarningsType
    Implements IEarningsType, ICustomizableForm

#Region "Builder"

    Public ctrTmp As CtrInfoEntity

    Sub New()
        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoEntity()
        ' This call is required by the designer.
        InitializeComponent()
        ctrTmp.SetTotalValues(AddressOf GetValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpccChangeEntity
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Integer, String, Integer, String, String)
        Return New Tuple(Of Integer, String, Integer, String, String)(BudgetEntitiesId, _EntityDescription, BudgetaryValiditiesId, _Year, statusValidities)
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
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' DataTable
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim _Indigo As SessionValues

    ''' <summary>
    ''' Variable para controlar la entidad presente en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim RevenueType As RevenueType

    ''' <summary>
    ''' Contiene el modelo del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MEarningsType

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PEarningsType

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' descripcion de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _EntityDescription As String

    ''' <summary>
    ''' Año vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _Year As String

    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim statusValidities As String

    ''' <summary>
    ''' Establece el id de la vigencia tanto cuando se escoge del control del form
    ''' como cuando se escoge del control del popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim validityIdStandard As Integer

    ''' <summary>
    ''' Controla el changed de los search
    ''' </summary>
    ''' <remarks></remarks>
    Dim controlerChanged As Boolean = False

#End Region

#Region "Properties"

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
    ''' Evento publico para actualizar el datasource del treeList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event UpdateDatasource(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Obtiene el id de la vigencia que viene del formulario
    ''' principal
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetaryValidityId As Integer
    Public Property BudgetaryValidityId As Integer
        Get
            Return _budgetaryValidityId
        End Get
        Set(value As Integer)
            _budgetaryValidityId = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo del tipo de ingreso que viene desde el form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private _codeEarningTypeFormPrincipal As String
    Public Property CodeEarningTypeFormPrincipal As String
        Get
            Return _codeEarningTypeFormPrincipal
        End Get
        Set(value As String)
            _codeEarningTypeFormPrincipal = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IEarningsType.Code
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
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameRT As String Implements IEarningsType.NameRT
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Source As Integer? Implements IEarningsType.Source
        Get
            Return INDgleSource.EditValue
        End Get
        Set(value As Integer?)
            INDgleSource.EditValue = value
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

    Public ReadOnly Property MyTag As Object Implements IEarningsType.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequence As BudgetSequence Implements IEarningsType.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BudgetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BudgetSequenceDetail In Me._sequence.BudgetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IEarningsType.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer Implements IEarningsType.BudgetEntitiesId
        Get
            Return INDsleEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValiditiesId As Integer Implements IEarningsType.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IEarningsType.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEarningsType.BudgetEntitiesXpo
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements IEarningsType.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEarningsType.BudgetEntitiesPopUpXpo
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Handles"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        _Indigo = Nothing
        RevenueType = Nothing
        model = Nothing
        presenter = Nothing
        blockRecord = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _EntityDescription = Nothing
        _Year = Nothing
        statusValidities = Nothing
        validityIdStandard = Nothing
        controlerChanged = Nothing
    End Sub

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmEarningsType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyEarningsType, True)
        '****Inicializar variables*****'
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PEarningsType(Me)
        'presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        '******************************'
        InitializeTuples()
        ctrTmp.RefreshInfo()
        LoadStatus()
        If CodeEarningTypeFormPrincipal = String.Empty Then
            'SearchMode = False
            DeshacerTodo()
        Else
            Code = CodeEarningTypeFormPrincipal
            Await LoadControls()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que abre el form de busqueda desde el click del boton buscar del control codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Abre el formulario de Entidades Presupuestales en un pop-up
    ''' </summary>
    Private Sub INDsleBudgetEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que abre el formulario de entidad presupuestal en popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                presenter.InitializeBudgetEntity()
                If BudgetEntitiesId <> Nothing Then
                    presenter.InitializeValidity(BudgetEntitiesId)
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que abre el formulario de entidad presupuestal en popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                presenter.InitializeBudgetEntity()
                If BudgetEntitiesId <> Nothing Then
                    presenter.InitializeValidity(BudgetEntitiesId)
                End If
            End Using
        End If
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
            If validityIdStandard = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
                Exit Sub
            End If
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewBudgetConcept()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmEarningsType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmEarningsType_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled = True Then
            INDbteCode.Focus()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' metodo que se dispara cuando el form se inicia
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmEarningsType_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            _EntityDescription = INDsleEntity.Text
            BudgetaryValiditiesId = Nothing
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
            SetFirstOrDefaultValidity()

            presenter.InitializeBudgetEntityPopUp()
            controlerChanged = True
            INDsleEntityPopUp.EditValue = BudgetEntitiesId
            controlerChanged = False
            presenter.InitializeValidityPopUp(INDsleEntityPopUp.EditValue)

            ctrTmp.RefreshInfo()
        End If
    End Sub

    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityPopUp.EditValueChanged
        If INDsleEntityPopUp.EditValue IsNot Nothing AndAlso controlerChanged = False Then
            _EntityDescription = INDsleEntityPopUp.Text
            BudgetEntitiesId = INDsleEntityPopUp.EditValue
            INDsleValidityPopUp.EditValue = Nothing
            BudgetaryValidityPopUpXpo = Nothing
            presenter.InitializeValidityPopUp(INDsleEntityPopUp.EditValue)
            SetFirstOrDefaultValidity()
            INDsleValidityPopUp.EditValue = BudgetaryValiditiesId
            validityIdStandard = 0
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' evento para cargar la vigencia.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValiditiesId <> Nothing AndAlso BudgetaryValiditiesId <> 0 Then
            validityIdStandard = BudgetaryValiditiesId
            controlerChanged = True
            INDsleValidityPopUp.EditValue = BudgetaryValiditiesId
            controlerChanged = False

            _Year = INDsleValidity.Text
            statusValidities = INDgvValidity.GetFocusedRowCellValue("StatusText")
            ctrTmp.RefreshInfo()
            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.ControlHideStatus = False
            BarraBotones.PrepareToolbar(eAction.NewAndFind)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
            CleanGroups(True)
            INDbteCode.Focus()
        End If
    End Sub

    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If INDsleValidityPopUp.EditValue IsNot Nothing AndAlso controlerChanged = False Then
            validityIdStandard = INDsleValidityPopUp.EditValue

            BudgetaryValiditiesId = INDsleValidityPopUp.EditValue
            _Year = INDsleValidityPopUp.Text
            statusValidities = INDGvValidityPopUp.GetFocusedRowCellValue("StatusText")
            ctrTmp.RefreshInfo()
        End If
    End Sub

#End Region

#Region "Query Popup"

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub

    Private Sub INDsleEntityPopUp_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntityPopUp.QueryPopUp
        If BudgetEntitiesPopUpXpo Is Nothing Then
            presenter.InitializeBudgetEntityPopUp()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        'Try
        '    If Not String.IsNullOrEmpty(Code) Then
        '        Dim state As Boolean
        '        Select Case Status
        '            Case CBool(eActionsStatusRecords.Active)
        '                state = True
        '            Case CBool(eActionsStatusRecords.Inactive)
        '                state = False
        '        End Select
        '        Using model As New MEarningsType()
        '            AsyncLoader(True)
        '            Dim Result = Await model.ChangeState(Code, validityIdStandard, 1, state)
        '            AsyncLoader(False)
        '            If Result.StateResult = True Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '                Deshacer()
        '                Code = Result.ObjectEmbbeded.Code
        '                Await LoadControls()
        '                RaiseEvent UpdateDatasource(Nothing, EventArgs.Empty)
        '            Else
        '                If Result.MessageResult(0) = ErrorConcurrencia Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                End If
        '            End If
        '        End Using
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        '    End If
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try

        If Not String.IsNullOrEmpty(Me.RevenueType.Code) Then
            Try
                Using model As New MEarningsType
                    AsyncLoader(True)
                    Dim state As Boolean = Not RevenueType.Status
                    Dim result As ActionResult(Of RevenueType) = Await model.ChangeState(RevenueType.Code, validityIdStandard, 1, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.RevenueType = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Metodo que inicializa las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        INDgleSource.Properties.DataSource = BudgetHelper.EarningSource
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.RevenueType IsNot Nothing AndAlso Me.RevenueType.Id > 0 Then
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

    ''' <summary>
    ''' Propiedad para controlar la accion que se hace sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyEarningsType.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDgleSource.Enabled = value
            INDlyEarningsType.EndUpdate()
            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Function NewBudgetConcept() As Task
        'RevenueType = New Domain.Entities.RevenueType()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.StatusRecordVisible = True
        '    Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    INDsleEntityPopUp.Properties.ReadOnly = True
        '    INDsleValidityPopUp.Properties.ReadOnly = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '            Else
        '                Using model As New ModelBaseBudget(Me.Tag)
        '                    Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
        '                End Using
        '                If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                    PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '        End If
        '    Else
        '        PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '    End If
        'End If


        RevenueType = New RevenueType() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            INDsleEntityPopUp.Properties.ReadOnly = True
            INDsleValidityPopUp.Properties.ReadOnly = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.BudgetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BudgetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.BudgetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New ModelBaseBudget(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        INDbteCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDsleEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If

        'Using Model As New MEarningsType
        '    AsyncLoader(True)
        '    INDlyEarningsType.BeginUpdate()
        '    RevenueType = Await Model.GetEarningsTypeAsync(Me.INDbteCode.Text, validityIdStandard, 1)
        '    If Not RevenueType Is Nothing AndAlso RevenueType.Id > 0 Then
        '        Me.BarraBotones.StatusRecordVisible = True
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, RevenueType.Id)
        '        With RevenueType
        '            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '            INDsleEntityPopUp.Properties.ReadOnly = True
        '            INDsleValidityPopUp.Properties.ReadOnly = True

        '            Code = .Code
        '            NameRT = .Name
        '            Source = .IncomeSource
        '            Me.Status = .Status
        '        End With
        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.RevenueType.Code)
        '        If result.Id = 0 Then
        '            Me.BarraBotones.SetDocuments(RevenueType.Id)
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = RevenueType.Id}
        '            Dim operation = Await Model.SaveBlockRecord(blockRecord)
        '            blockRecord = operation.ObjectEmbbeded
        '        Else
        '            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '            blockRecord = result
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '    Else
        '        AsyncLoader(False)
        '        If Me._sequense.IsManual Then
        '            Me.NewBudgetConcept()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.INDbteCode.Text = String.Empty
        '            Deshacer()
        '            INDbteCode.Focus()
        '        End If
        '    End If

        'End Using
        'INDlyEarningsType.EndUpdate()



        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MEarningsType
                    AsyncLoader(True)
                    INDlyEarningsType.BeginUpdate()
                    RevenueType = Await Model.GetEarningsTypeAsync(Me.INDbteCode.Text, validityIdStandard, 1)
                    If RevenueType IsNot Nothing AndAlso RevenueType.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        blockRecord = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(RevenueType.Id))
                        With RevenueType
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                INDsleEntityPopUp.Properties.ReadOnly = True
                                INDsleValidityPopUp.Properties.ReadOnly = True

                                Code = .Code
                                NameRT = .Name
                                Source = .IncomeSource
                                Me.Status = .Status
                            End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.RevenueType.Code)
                        If blockRecord.Id = 0 Then
                            blockRecord = (Await Model.SaveBlockRecord(
                                    New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = RevenueType.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), blockRecord.CodUser, blockRecord.NameUser, blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, blockRecord.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(RevenueType.Id, Me.Tag.ToString(), Nothing, GetType(RevenueType).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        'End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBudgetConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyEarningsType.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodos para asignar valores a la entidad financial source
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With RevenueType
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .BudgetaryValidityId = BudgetaryValidityId
            .Code = Code
            .Type = 1 ' Ingresos
            .IncomeSource = CInt(INDgleSource.EditValue)
            .Name = NameRT
            .BudgetaryValidityId = validityIdStandard
        End With
    End Sub

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls(aux As Boolean)
        'INDlyEarningsType.BeginUpdate()
        'INDsleEntityPopUp.Properties.ReadOnly = False
        'INDsleValidityPopUp.Properties.ReadOnly = False
        'Code = String.Empty
        'NameRT = String.Empty
        'Source = Nothing
        'Me.BarraBotones.ControlHideStatus = False
        'DeleteBlockedRecord()
        ' Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        ' Me.BarraBotones.CleanAuditBasic()
        'ActionsOnControls = False
        ' RevenueType = Nothing
        'If (aux) Then
        '    BudgetEntitiesPopUpXpo = Nothing
        '    BudgetaryValidityPopUpXpo = Nothing
        '    BudgetEntitiesXpo = Nothing
        '    BudgetaryValidityXpo = Nothing
        '    BudgetaryValiditiesId = 0
        '    BudgetEntitiesId = 0
        '    INDsleEntity.EditValue = 0
        '    INDsleValidity.EditValue = 0
        'End If
        ' INDlyEarningsType.EndUpdate()

        INDlyEarningsType.BeginUpdate()

        ActionsOnControls = False
        'Me.BarraBotones.StatusRecordVisible = False
        'Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.ControlHideStatus = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        INDsleEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False
        Code = String.Empty
        NameRT = String.Empty
        Source = Nothing

        If (aux) Then
            BudgetaryValiditiesId = 0
        End If
        'Limpiar controles
        RevenueType = Nothing

        'If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        'Else
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'End If

        INDlyEarningsType.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmEarningsTypeMetaData, Eform.InfoMetaData), Me.RevenueType.Code, Me.RevenueType.Name, INDgleSource.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.RevenueType.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmEarningsTypeMetaDataTitle, Eform.InfoMetaData), Me.RevenueType.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEarningsTypeMetaData, Eform.InfoMetaData), Me.RevenueType.Code, Me.RevenueType.Name, INDgleSource.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEarningsTypeMetaDataTitle, Eform.InfoMetaData), Me.RevenueType.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MEarningsType
                Await model.DeleteBlockRecord(blockRecord)
                blockRecord = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValiditiesId = item.Id
                _Year = item.Year
                statusValidities = item.StatusText
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para ocultar los grupos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanGroups(ByVal Bandera As Boolean)
        If Bandera Then
            INDlyGrGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGrEarningsType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyGrGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGrEarningsType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' MEtodo para buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls(False)
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If


        'If SearchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'INDbteCode.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeshacerTodo()
        CleanControls(True)
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
        Me.BarraBotones.StatusRecordVisible = False
        CleanGroups(False)
        INDsleEntity.Focus()

        'If SearchMode = False Then
        '    'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'Me.BarraBotones.StatusRecordVisible = False
        'Me.BarraBotones.ControlHideStatus = True
        'CleanGroups(False)
        'INDsleEntity.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'Try
        '    If RevenueType IsNot Nothing AndAlso RevenueType.Id > -1 Then
        '        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Using model As New MEarningsType
        '                AsyncLoader(True)
        '                RevenueType.MarkAsDeleted()
        '                Dim result = Await model.DeleteEarningsType(RevenueType)
        '                If result.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    AsyncLoader(False)
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                    SearchMode = False
        '                    Me.Deshacer()
        '                    RaiseEvent UpdateDatasource(Nothing, EventArgs.Empty)
        '                Else
        '                    AsyncLoader(False)
        '                    If result.MessageResult(0) = "-999" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                    ElseIf result.MessageResult(0) = "-000" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                    End If
        '                End If
        '            End Using
        '        End If
        '    End If
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try


        If Me.RevenueType IsNot Nothing AndAlso Me.RevenueType.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEarningsType
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEarningsType(Me.RevenueType)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
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
    ''' metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Exit Sub
        'End If
        'Try
        '    AssigningValues()
        '    Using model As New MEarningsType
        '        AsyncLoader(True)
        '        Dim Result = Await model.SaveEarningsType(RevenueType, _idCurrentSequense)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            If RevenueType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                'Se descarta la secuencia numerica usada
        '                If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                    Me.DicSequense(Me._sequense.BudgetSequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '                If Me._sequense.Sequential Then
        '                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
        '                Else
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
        '                End If
        '            ElseIf RevenueType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            Me.RevenueType = Result.ObjectEmbbeded
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            AsyncLoader(False)
        '            SearchMode = False
        '            Me.Deshacer()
        '            RaiseEvent UpdateDatasource(Nothing, EventArgs.Empty)
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try


        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MEarningsType
                AsyncLoader(True)
                Dim result As ActionResult(Of RevenueType) = Await Model.SaveEarningsType(Me.RevenueType, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If RevenueType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.RevenueType = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Obsoleto
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad que establece los mensajes 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' MEtodo Cuando se da click en boton nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
            ' Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            ' Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Else
            NewBudgetConcept()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para abrir busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo With {.Caption = "Descripción", .FieldName = "Name"}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {BudgetEntitiesId, BudgetaryValiditiesId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListEarningsType
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
        'SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
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

#Region "Eventos Barra Botones"

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        SearchMode = False
        DeshacerTodo()
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit

        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BudgetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BudgetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class