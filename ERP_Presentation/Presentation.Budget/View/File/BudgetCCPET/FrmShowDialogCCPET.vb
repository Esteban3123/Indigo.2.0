'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 29-04-2014
'
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
''' Contiene la vista de el showdialog para registrar rubros
''' </summary>
''' <remarks></remarks>
Public Class FrmShowDialogCCPET
    Implements IShowDialogCCPET

#Region "Builder"

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

#End Region

#Region "Globals"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PShowDialogCCPET

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private _blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Variable para controlar la entidad presente en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _cCPET As CCPET

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IShowDialogCCPET.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IShowDialogCCPET.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Representa al objeto seleccionado en el form principal
    ''' para poder saber el codigo y si es padre
    ''' </summary>
    ''' <remarks></remarks>
    Public Property CCPET As CCPET
        Get
            Return _cCPET
        End Get
        Set(value As CCPET)
            _cCPET = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IShowDialogCCPET.Code
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
    ''' Obtiene o establece Nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property txName As String Implements IShowDialogCCPET.Name
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
    Public Property CCPETOwnerId As Integer? Implements IShowDialogCCPET.CCPETOwnerId
        Get
            Return INDsleParentCCPET.EditValue
        End Get
        Set(value As Integer?)
            INDsleParentCCPET.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece OBTIENE Ó ESTABLECE EL TIPO DE RUBRO (INGRESO=1,GASTO = 2)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemType As Byte Implements IShowDialogCCPET.ItemType
        Get
            Return INDGleItemType.EditValue
        End Get
        Set(value As Byte)
            INDGleItemType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Tipo de Cuenta: Agregación. (A). Cuentas de Captura (C)  (Solo permite vincular a otras funcionalidades los marcados como tipo C.)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountType As Boolean Implements IShowDialogCCPET.AccountType
        Get
            Return If(INDGleAccountType.EditValue Is Nothing, False, INDGleAccountType.EditValue)
        End Get
        Set(value As Boolean)
            INDGleAccountType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Vincula Cuenta CPC: Si / No .Esta opción debe estar disponible si el campo Tipo de Cuenta es (C). dejar todas en No.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LinkAccount As Boolean Implements IShowDialogCCPET.LinkAccount
        Get
            Return INDSleLinkAccountCPC.EditValue
        End Get
        Set(value As Boolean)
            INDSleLinkAccountCPC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IShowDialogCCPET.Status
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

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Establece el datasource del rubro padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParentXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IShowDialogCCPET.ParentXpo
        Get
            Return INDsleParentCCPET.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleParentCCPET.Properties.DataSource = value
        End Set
    End Property

    Private _FillingItemType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingItemType As List(Of Tuple(Of Byte, String))
        Get
            If _FillingItemType Is Nothing Then
                _FillingItemType = New List(Of Tuple(Of Byte, String))
                _FillingItemType.Add(New Tuple(Of Byte, String)(1, "Ingreso"))
                _FillingItemType.Add(New Tuple(Of Byte, String)(2, "Gasto"))
            End If
            Return _FillingItemType
        End Get
    End Property

    Private _FillingAccountType As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property FillingAccountType As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingAccountType Is Nothing Then
                _FillingAccountType = New List(Of Tuple(Of Boolean, String))
                _FillingAccountType.Add(New Tuple(Of Boolean, String)(True, "Captura (C)"))
                _FillingAccountType.Add(New Tuple(Of Boolean, String)(False, "Agregacion (A)"))
            End If
            Return _FillingAccountType
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
    Private Async Sub FrmShowDialogCCPET_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetItem, True)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PShowDialogCCPET(Me)
        _presenter.LoadDefinitionLayout()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        '******************************'
        LoadStatus()
        LoadDatasources()
        '******************************'
        If CCPET Is Nothing Then
            Deshacer()
        Else
            Code = CCPET.Code
            Await ValidateCode()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _presenter = Nothing
        _blockRecord = Nothing
        _cCPET = Nothing

        _FillingItemType = Nothing
        _FillingAccountType = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmShowDialogCCPET_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbteCode.Enabled = True Then
            INDbteCode.Focus()
        Else
            INDtxtName.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmShowDialogCCPET_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos padre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleParentItem_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleParentCCPET.QueryPopUp
        If ParentXpo Is Nothing Then
            _presenter.InitializeParent(ItemType)
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' evento al modificar el tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleItemType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleItemType.EditValueChanged
        ParentXpo = Nothing
        INDsleParentCCPET.EditValue = Nothing
        INDsleParentCCPET.Properties.NullText = String.Empty
    End Sub
    ''' <summary>
    ''' evento cargado al editar el tipo de cuenta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleAccountType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAccountType.EditValueChanged
        If AccountType = False Then
            INDSleLinkAccountCPC.Enabled = False
            LinkAccount = False
        Else

            INDSleLinkAccountCPC.Enabled = True
            LinkAccount = True
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
    ''' Se carga la data 
    ''' </summary>
    Private Sub LoadDatasources()
        Me.INDGleItemType.Properties.DataSource = FillingItemType
        Me.INDGleItemType.EditValue = 1

        Me.INDGleAccountType.Properties.DataSource = FillingAccountType
        Me.INDGleAccountType.EditValue = True
    End Sub

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyBudgetItem.BeginUpdate()

        Code = String.Empty
        txName = String.Empty
        CCPETOwnerId = Nothing
        INDsleParentCCPET.Properties.NullText = String.Empty
        INDsleParentCCPET.Properties.ReadOnly = False
        ItemType = 1
        AccountType = True
        LinkAccount = True

        _cCPET = Nothing

        ActionsOnControls = False
        DeleteBlockedRecord()

        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)

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

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleParentCCPET.Enabled = value
            INDGleItemType.Enabled = value
            INDGleAccountType.Enabled = value
            INDSleLinkAccountCPC.Enabled = value
            INDlyBudgetItem.EndUpdate()

            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
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

        Using Model As New MBudgetCCPET
            AsyncLoader(True)
            INDlyBudgetItem.BeginUpdate()

            Dim resultCCPET As ActionResult(Of CCPET) = Await Model.GetBudgetCCPETByCode(Code)
            If resultCCPET.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = resultCCPET.Message
                Exit Function
            End If

            _cCPET = resultCCPET.ObjectEmbbeded
            If Not _cCPET Is Nothing AndAlso _cCPET.Id > 0 Then
                Me.BarraBotones.StatusRecordVisible = True
                Dim result = Await Model.GetBlockRecord(Me.Tag, _cCPET.Id)
                With _cCPET
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False

                    Code = .Code
                    txName = .Name
                    ItemType = .ItemType
                    CCPETOwnerId = .CCPETOwnerId
                    INDsleParentCCPET.Properties.NullText = .ParentDescription
                    AccountType = .AccountType
                    LinkAccount = .LinkAccount
                    Status = .Status

                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                End With

                Me.GetDocumentIndexed(Me.Tag & "_" & Me._cCPET.Code)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(_cCPET.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _cCPET.Id}
                    Dim operation = Await Model.SaveBlockRecord(_blockRecord)
                    _blockRecord = operation.ObjectEmbbeded
                Else
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    _blockRecord = result
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If

                AsyncLoader(False)
                ActionsOnControls = True
            Else
                AsyncLoader(False)
                _cCPET = New CCPET With {.Status = True}
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
        With _cCPET
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = txName
            .ItemType = ItemType
            .CCPETOwnerId = CCPETOwnerId
            .AccountType = AccountType
            .LinkAccount = LinkAccount
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MEarningsType
                Await Model.DeleteBlockRecord(_blockRecord)
            End Using

            _blockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._cCPET IsNot Nothing AndAlso Me._cCPET.Id > 0 Then
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._cCPET.Code, Me._cCPET.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._cCPET.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._cCPET.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._cCPET.Code, Me._cCPET.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._cCPET.Code)
            Return Me._doc
        End If
    End Function

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
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
                New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"},
                New ColumnInfo With {.Caption = "Tipo Cuenta", .FieldName = "AccountTypeName"},
                New ColumnInfo With {.Caption = "CPC", .FieldName = "LinkAccountName"},
                New ColumnInfo With {.Caption = "Rubro", .FieldName = "ItemTypeName"}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCCPET
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
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
        If ValidateControls() = False Then
            Exit Sub
        End If
        If CCPETOwnerId IsNot Nothing AndAlso CCPETOwnerId = CCPET.Id Then
            Mensaje(EeventViewerImages.Advertencia) = "No puede seleccionar el mismo catalogo como padre"
            Exit Sub
        End If

        Try
            AssigningValues()
            Using model As New MBudgetCCPET
                AsyncLoader(True)
                Dim Result = Await model.SaveBudgetCCPETAsync(_cCPET)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _cCPET.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    ElseIf _cCPET.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._cCPET = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
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
                Using model As New MBudgetCCPET()
                    AsyncLoader(True)
                    Dim state As Boolean = Not _cCPET.Status
                    Dim Result = Await model.ChangeState(_cCPET, state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        _cCPET = Result.ObjectEmbbeded
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
            If _cCPET IsNot Nothing AndAlso _cCPET.Id > 0 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using model As New MBudgetCCPET
                        AsyncLoader(True)
                        _cCPET.MarkAsDeleted()
                        Dim result = Await model.DeleteBudgetCCPETAsync(CCPET)
                        If result.StateResult = True Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
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
