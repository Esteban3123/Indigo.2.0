Imports System.Text
Imports DevExpress.Xpo
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

Public Class FrmCostAddStructure
    Implements ICostOrganizationalStruct

    Public Sub New()
        InitializeComponent()
        model = New MCostOrganizationalStructure(Me.MyTag)
        _presenter = New PCostOrganizationalStruct(Me)
    End Sub

#Region "Properties and Variables"
    Private model As MCostOrganizationalStructure
    ''' <summary>
    ''' Evento que se ejecuta para actualizar los niveles de la estructura
    ''' </summary>
    Public Event RefreshDatasourceStruct()

    ''' <summary>
    ''' The modul e_ name
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    Private _modeEdit As Boolean
    ''' <summary>
    ''' Indica si esta en modo edición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModeEdit As Boolean
        Get
            Return _modeEdit
        End Get
        Set(value As Boolean)
            _modeEdit = value
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostOrganizationalStruct.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICostOrganizationalStruct.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el código de la estructura organizacional
    ''' </summary>
    Public Property Code As String Implements ICostOrganizationalStruct.Code
        Get
            Return Me.INDbteCode.Text
        End Get
        Set(value As String)
            Me.INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre de la estructura organizacional
    ''' </summary>
    Public Property NameStruct As String Implements ICostOrganizationalStruct.NameStruct
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del registro padre
    ''' </summary>
    Public Property ParentId As Integer? Implements ICostOrganizationalStruct.ParentId
        Get
            Return If(String.IsNullOrEmpty(INDsleOrganizationalStruct.EditValue), Nothing, INDsleOrganizationalStruct.EditValue)
        End Get
        Set(value As Integer?)
            INDsleOrganizationalStruct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nivel del registro
    ''' </summary>
    Public Property Level As Integer Implements ICostOrganizationalStruct.Level
        Get
            Return INDtxtLevel.EditValue
        End Get
        Set(value As Integer)
            INDtxtLevel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements ICostOrganizationalStruct.Status
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
    ''' Gets or sets the organizational structure datasourse.
    ''' </summary>
    ''' <value>
    ''' The organizational structure datasourse.
    ''' </value>
    Public Property OrganizationalStructureDatasourse As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostOrganizationalStruct.OrganizationalStructureDatasourse
        Get
            Return CType(INDsleOrganizationalStruct.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleOrganizationalStruct.Properties.DataSource = value
        End Set
    End Property

    Private _stateOpenPopUpStructure As Boolean

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    '''' <summary>
    '''' variable que contiene la entidad
    '''' </summary>
    Dim _organizationStruct As CostOrganizationalStructureOfCosts

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Dim _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PCostOrganizationalStruct

    ' ''' <summary>
    ' ''' entidad que almacena el registro bloqueado
    ' ''' </summary>
    Dim _record As BlockRecordCost

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model = Nothing
        _modeEdit = Nothing
        _stateOpenPopUpStructure = Nothing
        _idOperativeUnit = Nothing
        _organizationStruct = Nothing
        _searchMode = Nothing
        _presenter = Nothing
        _record = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmOrganizationalStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        _presenter.LoadDefinitionLayout()
        OrganizationalStructureDatasourse = model.ListCostOrganizationalStructure()
        If FormSearchObjects Is Nothing Then
            _searchMode = False
        End If

        LoadStatus()
        If ModeEdit Then 'Si esta en modo edición
            LoadControls()
        Else 'Si se va a guardar
            Deshacer()
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmOrganizationalStructure_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
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
            If Not String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
                LoadControls()
                INDtxtName.Focus()
            End If
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
        If Me._organizationStruct IsNot Nothing AndAlso Me._organizationStruct.Id > 0 Then
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

#Region "QueryPopUp"
    Private Sub INDsleOrganizationalStruct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleOrganizationalStruct.QueryPopUp
        'If Not _stateOpenPopUpStructure Then
        '    _presenter.InitializeOrganizationalStructure()
        '    _stateOpenPopUpStructure = True
        'End If
    End Sub
#End Region

#Region "QueryPopUp"

    Private Sub INDsleOrganizationalStruct_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleOrganizationalStruct.EditValueChanged
        If ParentId IsNot Nothing Then
            Dim organizationalStruct = DirectCast(INDsleOrganizationalStruct.GetSelectedObject(), Infrastructure.Data.Xpo.CostRepository.CostOrganizationalStructureOfCostsXpo)
            If organizationalStruct Is Nothing Then
                organizationalStruct = _presenter.GetCostOrganizationalStructById(ParentId)
            End If
            Level = organizationalStruct.Level + 1
        End If
    End Sub
#End Region

#End Region

#Region "Methods and Functions"
    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsPopup() As Boolean
        If Code = String.Empty OrElse NameStruct = String.Empty OrElse ParentId Is Nothing Then
            Return False
        End If
        Return True
        'Dim res = Me.LayoutControls.ValidateFields()
        'If res IsNot Nothing AndAlso res.Count > 0 Then
        '    Dim sb As New StringBuilder()
        '    For Each s As String In res
        '        sb.AppendLine(s)
        '    Next
        '    Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), sb.ToString())
        '    Return False
        'Else
        '    Return True
        'End If
    End Function

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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        'FormSearchObjects = New FrmBusqueda
        'AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        'With FormSearchObjects
        '    .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
        '                      New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6},
        '                      New ColumnInfo() With {.Caption = "Fecha Inicial", .FieldName = "InitialDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
        '    .ValorSolicitado = "Code"
        '    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCashRegister
        '    .FormParent = Me
        '    .ShowSearch()
        'End With
        '_searchMode = True
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostOrganizationalStruct.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleOrganizationalStruct.Enabled = value
            INDtxtLevel.Enabled = value

            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Public Sub AssigningValues() Implements ICostOrganizationalStruct.AssigningValues
        With _organizationStruct
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameStruct
            .ParentId = ParentId
            .Level = If(ParentId Is Nothing, 1, Level)
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements ICostOrganizationalStruct.CleanControls
        INDlcRoot.BeginUpdate()
        ActionsOnControls = False
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtLevel.EditValue = 1
        'INDsleOrganizationalStruct.EditValue = Nothing
        INDlcRoot.EndUpdate()

        _stateOpenPopUpStructure = False

        _organizationStruct = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    Public Async Sub LoadControls() Implements ICostOrganizationalStruct.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If

            Me.BarraBotones.StatusRecordVisible = True
            AsyncLoader(True)
            Dim resultOperation = Await model.GetOrganizationalStructure(Me.Code)
            AsyncLoader(False)
            _organizationStruct = resultOperation.ObjectEmbbeded
            If Not _organizationStruct Is Nothing AndAlso _organizationStruct.Id > 0 Then
                Using mCommonCost As New MCommonCost(Me.Tag)
                    Dim result = Await mCommonCost.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _organizationStruct.Id)

                    If _organizationStruct.ParentId Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "Este es el registro principal, por lo tanto no se puede modificar"
                    End If

                    With _organizationStruct
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        NameStruct = .Name
                        ParentId = .ParentId
                        Level = .Level
                        Status = .Status
                    End With
                    OrganizationalStructureDatasourse = model.InitializeOrganizationalStructureWithOut(Code)
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._organizationStruct.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _organizationStruct.Id}
                        Dim operation = Await mCommonCost.SaveBlockRecordCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_organizationStruct.Id, MyTag, Nothing, GetType(CostOrganizationalStructureOfCosts).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    ActionsOnControls = True
                    INDbteCode.Enabled = False
                End Using
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                ActionsOnControls = True
                INDbteCode.Enabled = False
                _organizationStruct.Status = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._organizationStruct.Code, Me._organizationStruct.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._organizationStruct.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._organizationStruct.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._organizationStruct.Code, Me._organizationStruct.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._organizationStruct.Code)
        End If
        Return Me._doc
    End Function
#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If Not _searchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me._organizationStruct IsNot Nothing AndAlso Me._organizationStruct.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Me._organizationStruct.MarkAsDeleted()
                    Dim result = Await model.DeleteOrganizationalStructure(Me._organizationStruct)
                    If result.StateResult = True Then
                        'Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        _searchMode = False
                        Me.Deshacer()
                        RaiseEvent RefreshDatasourceStruct()
                    Else
                        AsyncLoader(False)
                        If result.Message IsNot Nothing Then
                            generateListError(result.Message)
                        End If
                    End If
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If _organizationStruct.Id <> 0 AndAlso _organizationStruct.ParentId Is Nothing AndAlso ParentId IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Este es el registro principal, por lo tanto no se puede modificar la jerarquía"
        Else
            If Not (_organizationStruct.Id <> 0 AndAlso _organizationStruct.ParentId) Then
                If ParentId IsNot Nothing Then
                    If ValidateControlsPopup() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        Exit Sub
                    End If
                End If
            End If
            AssigningValues()
            Try
                AsyncLoader(True)
                Dim Result = Await model.SaveOrganizationalStructure(Me._organizationStruct)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _organizationStruct.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf _organizationStruct.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._organizationStruct = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    _searchMode = False
                    Me.Deshacer()
                    RaiseEvent RefreshDatasourceStruct()
                    'Evento de Actualizar estructura
                Else
                    If Result.Message IsNot Nothing Then
                        generateListError(Result.Message)
                    End If
                End If
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.MensajeError) = errors
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                AsyncLoader(True)
                Dim state As Boolean = Not Me._organizationStruct.Status
                Dim Result = Await model.UpdateState(Me._organizationStruct.Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me._organizationStruct = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If _organizationStruct IsNot Nothing Then
            _organizationStruct.Status = Not _organizationStruct.Status
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
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

End Class