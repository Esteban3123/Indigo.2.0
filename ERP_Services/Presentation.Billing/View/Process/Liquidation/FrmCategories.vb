'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán
' Created          : 26-10-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Infrastructure.Data.Xpo.SecurityRepository

Public Class FrmCategories
    Implements ICategories, ICustomizableForm

#Region "Builder"
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' Nombre del módulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Billing"

    ''' <summary>
    ''' The _sequence
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence

    ''' <summary>
    ''' The _presenter
    ''' </summary>
    Private _presenter As PCategories

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' The _invoice categories
    ''' </summary>
    Private _invoiceCategories As Domain.Entities.InvoiceCategories

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordBilling

    ''' <summary>
    ''' The model
    ''' </summary>
    Dim model As MCategories

    ''' <summary>
    ''' Habilita o deshabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            LcRoot.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDSleUsers.Enabled = value
            INDsbAdd.Enabled = value
            INDGcUsers.Enabled = value

            LcRoot.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICategories.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICategories.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequence As Domain.Entities.BillingSequence Implements ICategories.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.BillingSequence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

#Region "Properties Entity"

    ''' <summary>
    ''' Código de la categoría
    ''' </summary>
    Public Property Code As String
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text.Trim()
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la categoria
    ''' </summary>
    Public Property NameCategories As String
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [status].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Status As Boolean
        Get
            Return BarraBotones.StatusRecord
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
    ''' Gets or sets the list users.
    ''' </summary>
    ''' <value>
    ''' The list users.
    ''' </value>
    Public Property ListInvoiceCategoriesUsers As List(Of Domain.Entities.InvoiceCategoriesUser)
        Get
            Return CType(INDGcUsers.DataSource, List(Of Domain.Entities.InvoiceCategoriesUser))
        End Get
        Set(value As List(Of Domain.Entities.InvoiceCategoriesUser))
            INDGcUsers.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Handles the Load event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCategories_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _presenter = New PCategories(Me)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()
        model = New MCategories(Me.Tag)

        Dim listActions As New List(Of eAcciones)()
        listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvUsers, listActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvUsers.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDGcUsers)
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "ButtonClick"

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDBteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDBteCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDBteCode.Text.Trim()) Then
                    Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(INDBteCode.Text.Trim()) Then
                    Me.NewCategories()
                Else
                    Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCategories_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.F4 AndAlso INDBteCode.Focused Then

        End If
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Handles the Activated event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCategories_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDSleUsers.EditValue IsNot Nothing Then
            If ListInvoiceCategoriesUsers Is Nothing Then
                ListInvoiceCategoriesUsers = New List(Of InvoiceCategoriesUser)()
            End If
            Dim user As UserXpo = DirectCast(DirectCast(INDGvUser.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, UserXpo) 'CType(INDGvUser.GetFocusedRow(), UserXpo)
            If ListInvoiceCategoriesUsers.Any(Function(x) x.UserId = user.Id) Then
                Mensaje(eStatusResult.WARNING) = "Este usuario ya está agregado"
                Exit Sub
            End If

            Dim categoriesUser As New InvoiceCategoriesUser() With {.UserCode = user.UserCode, .UserId = user.Id, .UserFullName = user.CodeName}
            _invoiceCategories.InvoiceCategoriesUser.Add(categoriesUser)
            ListInvoiceCategoriesUsers = _invoiceCategories.InvoiceCategoriesUser.ToList()

            If _invoiceCategories.ChangeTracker.State <> ObjectState.Added Then
                _invoiceCategories.MarkAsModified()
            End If

            INDSleUsers.EditValue = Nothing
            INDSleUsers.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleUsers control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleUsers_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUsers.QueryPopUp
        If INDSleUsers.Properties.DataSource Is Nothing Then
            INDSleUsers.Properties.DataSource = model.ListUsers()
        End If
    End Sub
#End Region

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCategories_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
            Dim CategoriesUser As InvoiceCategoriesUser = CType(INDGvUsers.GetFocusedRow(), InvoiceCategoriesUser)
            CategoriesUser.MarkAsDeleted()
            ListInvoiceCategoriesUsers = _invoiceCategories.InvoiceCategoriesUser.ToList()

            If _invoiceCategories.ChangeTracker.State <> ObjectState.Added Then
                _invoiceCategories.MarkAsModified()
            End If
        End If
    End Sub
#End Region

#Region "Identity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._invoiceCategories IsNot Nothing AndAlso _invoiceCategories.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' News the categories.
    ''' </summary>
    Private Async Sub NewCategories()
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
            Me._invoiceCategories = New Domain.Entities.InvoiceCategories()
            If Me._sequence.IsManual Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
                ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me.Sequence.BillingSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                        Me._idCurrentSequence = Me._sequence.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
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
                            Using model As New MBlockRecordAndSequense(Me.Tag)
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
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            Me.BarraBotones.StatusRecordVisible = True
            AsyncLoader(True)
            _invoiceCategories = Await model.GetInvoiceCategoryAsync(Me.Code)
            LcRoot.BeginUpdate()
            If _invoiceCategories IsNot Nothing AndAlso _invoiceCategories.Id > 0 Then
                Using modelBlock As New MBlockRecordAndSequense(Me.Tag)
                    Dim result = Await modelBlock.GetBlockRecord(Me.Tag, _invoiceCategories.Id)
                    With _invoiceCategories
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        NameCategories = .Name
                        Status = .Status
                    End With

                    ListInvoiceCategoriesUsers = _invoiceCategories.InvoiceCategoriesUser.ToList()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._invoiceCategories.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _invoiceCategories.Id}
                        Dim operation = Await modelBlock.SaveBlockRecord(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_invoiceCategories.Id, Me.Tag.ToString(), Nothing, GetType(InvoiceCategories).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.PrintReport(PrintReportAction.None, _invoiceCategories.Id, 0, _invoiceCategories.Id)

                    AsyncLoader(False)
                    ActionsOnControls = True
                End Using
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewCategories()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            LcRoot.EndUpdate()
        End If
    End Sub

    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._invoiceCategories.Code, _invoiceCategories.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .Id = 0, .IdDb = -1, .IdEntity = "$#" & Me.Tag & "_" & Me._invoiceCategories.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._invoiceCategories.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._invoiceCategories.Code, Me._invoiceCategories.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._invoiceCategories.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        LcRoot.BeginUpdate()

        Code = String.Empty
        NameCategories = String.Empty
        INDSleUsers.EditValue = Nothing
        INDGcUsers.DataSource = Nothing
        'INDGcUsers.RefreshDataSource()

        ActionsOnControls = False

        _invoiceCategories = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()

        If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
        LcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 Then
            Using model As New MBlockRecordAndSequense(Me.Tag)
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Returns the value.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _invoiceCategories
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCategories
            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .Status = True
                Case eActionsStatusRecords.Inactive
                    .Status = False
            End Select
        End With
    End Sub
#End Region

#Region "Crud"
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
        If Me._invoiceCategories IsNot Nothing AndAlso Me._invoiceCategories.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteInvoiceCategoryAsync(Me._invoiceCategories)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                    End If
                    Mensaje(result.StatusCode) = result.Message
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of InvoiceCategories) = Await model.SaveInvoiceCategoryAsync(Me._invoiceCategories, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _invoiceCategories.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.BillingSequenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._invoiceCategories = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Deshacer()
            Else
                AsyncLoader(False)
                INDBteCode.Enabled = False
            End If
            Mensaje(result.StatusCode) = result.Message
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
            INDBteCode.Enabled = False
        End Try
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me._invoiceCategories.Code) Then
            Try
                Dim stateCard As Boolean
                Select Case Status
                    Case eActionsStatusRecords.Active
                        stateCard = True
                    Case eActionsStatusRecords.Inactive
                        stateCard = False
                End Select
                AsyncLoader(True)
                Dim result = Await model.UpdateStateInvoiceCategoryAsync(Me._invoiceCategories.Code, stateCard)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me._invoiceCategories = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                Mensaje(result.StatusCode) = result.Message
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
                INDBteCode.Enabled = False
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

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

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If _sequence Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewCategories()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceCategories
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub
#End Region

#Region "BarButtons"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
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
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _invoiceCategories.Id, _invoiceCategories.Id)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
#End Region

End Class