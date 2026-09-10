#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Cost.MVP

#End Region

Public Class FrmCostInventoryGroup
    Implements ICostInventoryGroup

#Region "Builder"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Globals"

    Public Const MODULE_NAME As String = "Cost"

    Private _operativeUnitId As Int32

    Private _sequence As Domain.Entities.CostSecuence

    Private _currentSequenceId As Int64

    Dim _presenter As PCostInventoryGroup

    Dim _record As BlockRecordCost

    Private _CostInventoryGroup As CostInventoryGroup

    Private _listCostInventoryGroupDetail As List(Of CostInventoryGroupDetail)

#End Region

#Region "Fields"

    Public ReadOnly Property MyTag As Object Implements ICostInventoryGroup.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostInventoryGroup.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostInventoryGroup.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbeCode.Enabled = Not value
            INDteName.Enabled = value
            INDsleMeasurementUnit.Enabled = value            
            INDmeDescription.Enabled = value

            INDpceAddInventory.Enabled = value
            INDgcInventory.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDteName.Focus()
            Else
                INDbeCode.Focus()
            End If
        End Set
    End Property

    Public Property Sequence As CostSecuence Implements ICostInventoryGroup.Sequence
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

#End Region

#Region "Properties"

    Public Property Code As String Implements ICostInventoryGroup.Code
        Get
            If INDbeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbeCode.Text
            End If
        End Get
        Set(value As String)
            INDbeCode.Text = value
        End Set
    End Property

    Public Property InventoryGroupName As String Implements ICostInventoryGroup.Name
        Get
            Return INDteName.Text
        End Get
        Set(value As String)
            INDteName.Text = value
        End Set
    End Property

    Public Property MeasurementUnitId As Integer Implements ICostInventoryGroup.MeasurementUnitId
        Get
            Return INDsleMeasurementUnit.EditValue
        End Get
        Set(value As Integer)
            INDsleMeasurementUnit.EditValue = value
        End Set
    End Property

    Public Property Description As String Implements ICostInventoryGroup.Description
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    Public Property Status As Boolean Implements ICostInventoryGroup.Status
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

#End Region

#Region "XPO"

    Public Property MeasurementUnitXpo As XPInstantFeedbackSource Implements ICostInventoryGroup.MeasurementUnitXpo
        Get
            Return CType(INDsleMeasurementUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMeasurementUnit.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Crud"

    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"},
                              New ColumnInfo() With {.Caption = "Unidad de Medida", .FieldName = "InventoryMeasurementUnitId.CodeName"},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostInventoryGroups
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
    End Sub

    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewCostInventoryGroup()
        End If
    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If Me._listCostInventoryGroupDetail Is Nothing OrElse Me._listCostInventoryGroupDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un detalle"
                Exit Sub
            End If
        End If

        AssigningValues()

        Try
            Using model As New MCostInventoryGroup(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveCostInventoryGroup(_CostInventoryGroup, _listCostInventoryGroupDetail)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me._CostInventoryGroup = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    INDbeCode.Enabled = False
                    If _CostInventoryGroup.ChangeTracker.State = ObjectState.Added Then
                        If Result.ObjectEmbbeded IsNot Nothing AndAlso Result.ObjectEmbbeded.Id > 0 Then
                            Me._CostInventoryGroup = Result.ObjectEmbbeded
                            Me._CostInventoryGroup.ChangeTracker.State = ObjectState.Modified
                            Code = Me._CostInventoryGroup.Code
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
    End Sub

#End Region

#Region "BarButton Events"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._operativeUnitId = operatingUnit.Id
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbeCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Async Sub FrmInvoiceEntityCapitatedDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._operativeUnitId = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PCostInventoryGroup(Me)
        _presenter.LoadDefinitionLayout()

        AsyncLoader(True)
        Await _presenter.GetSequence()
        AsyncLoader(False)

        AddHandler CtrInventoryProduct.AddCostInventoryGroupDetail, AddressOf AddCostInventoryGroupDetailPopup

        Deshacer()
        LoadStatus()
        SetActionsGrid()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _operativeUnitId = Nothing
        _sequence = Nothing
        _currentSequenceId = Nothing
        _presenter = Nothing
        _record = Nothing
        _CostInventoryGroup = Nothing
        _listCostInventoryGroupDetail = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbeCode.Enabled Then
            INDbeCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmInvoiceEntityCapitatedDistribution_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbeCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
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
                    Await Me.NewCostInventoryGroup()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        If MeasurementUnitXpo Is Nothing Then
            Me._presenter.InitializeMeasurementUnitXpo()
        End If
    End Sub

    Private Sub INDpceAddInventory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddInventory.QueryPopUp        
        CtrInventoryProduct.ListCostInventoryGroupDetail = _listCostInventoryGroupDetail
    End Sub

#End Region

#Region "Closed"

   Private Sub INDpceAddInventory_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddInventory.Closed
        CtrInventoryProduct.CleanControls()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("970", Nothing, True)
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Sub IndigoGridViewInventory_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewInventory.ContexMenuActions, IndigoGridViewInventory.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostInventoryGroupDetail()
            Case "Remove"
                DeleteCostInventoryGroupDetail()
        End Select
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If Not Me._CostInventoryGroup.Status Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento no puede ser modificado"
            Exit Sub
        End If
        If e.Rows.Count = 0 Then
            Exit Sub
        End If
        If e.Rows.Count > 300 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se puede procesar máximo 300 registros"
            Exit Sub
        End If

        Try
            If sender.Name = INDgcInventory.Name Then
                Await Me.CopyAndPasteInventory(e.Rows)            
            End If
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}}

        IndigoGridViewInventory.SetListAcction(INDgvInventory, _listActions)
    End Sub

    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbeCode.Text = ReturnValue
        If INDbeCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbeCode.Enabled = False
        End If
    End Sub

    Private Sub CleanControls()
        INDlcRoot.BeginUpdate()

        _doc = Nothing
        DeleteBlockedRecord()
        ReadOnlyControls(False)
        BarraBotones.EnableBarItems()
        BarraBotones.DisableBarDocument()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Code = String.Empty
        InventoryGroupName = String.Empty        
        INDsleMeasurementUnit.EditValue = Nothing
        INDsleMeasurementUnit.Properties.NullText = String.Empty
        INDmeDescription.Text = String.Empty
        Status = True

        _CostInventoryGroup = Nothing
        _listCostInventoryGroupDetail = Nothing

        INDgcInventory.DataSource = Nothing

        ActionsOnControls = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlcRoot.EndUpdate()
    End Sub

    Private Async Function NewCostInventoryGroup() As Task
        Me._CostInventoryGroup = New CostInventoryGroup() With {.Status = True}
        Me._listCostInventoryGroupDetail = New List(Of CostInventoryGroupDetail)

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._currentSequenceId = Me._sequence.CostSecuenceDetail.SingleOrDefault(Function(s) s.IdOperatingUnit = Me._operativeUnitId).Id
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
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MCommonCost(Me.Tag)
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Function
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MCostInventoryGroup(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim result = Await Model.GetCostInventoryGroup(INDbeCode.Text.Trim)
                    If Not result.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        AsyncLoader(False)
                        Exit Function
                    End If
                    _CostInventoryGroup = result.ObjectEmbbeded
                    INDlcRoot.BeginUpdate()
                End Using

                If _CostInventoryGroup IsNot Nothing AndAlso _CostInventoryGroup.Id > 0 Then
                    Using mCommonCost As New MCommonCost(Me.Tag)
                        Dim result = Await mCommonCost.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _CostInventoryGroup.Id)

                        With _CostInventoryGroup
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Code = .Code
                            InventoryGroupName = .Name
                            INDsleMeasurementUnit.EditValue = .InventoryMeasurementUnitId
                            INDsleMeasurementUnit.Properties.NullText = .MeasurementUnitCodeName
                            Description = .Description
                            Status = .Status

                            _listCostInventoryGroupDetail = New List(Of CostInventoryGroupDetail)
                            If .CostInventoryGroupDetail IsNot Nothing Then
                                For Each item In .CostInventoryGroupDetail
                                    _listCostInventoryGroupDetail.Add(item)
                                Next
                            End If

                            INDgcInventory.DataSource = Nothing
                            INDgcInventory.DataSource = _listCostInventoryGroupDetail
                        End With

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._CostInventoryGroup.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _CostInventoryGroup.Id}
                            Dim operation = Await mCommonCost.SaveBlockRecordCost(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            _record = result
                            Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(_CostInventoryGroup.Id, MyTag, Nothing, GetType(CostInventoryGroup).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewCostInventoryGroup()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Deshacer()
                    End If
                End If                
                INDlcRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDbeCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MCommonCost(CStr(Me.Tag))
                Await Model.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._CostInventoryGroup.Code, Me._CostInventoryGroup.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._CostInventoryGroup.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._CostInventoryGroup.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._CostInventoryGroup.Code, Me._CostInventoryGroup.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._CostInventoryGroup.Code)
        End If
        Return Me._doc
    End Function

    Private Sub AssigningValues()
        With _CostInventoryGroup
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .Code = Code
            .Name = InventoryGroupName
            .InventoryMeasurementUnitId = MeasurementUnitId
            .Description = Description

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MCostInventoryGroup(Me.Tag.ToString())
		            AsyncLoader(True)
                    _CostInventoryGroup.Status = Not _CostInventoryGroup.Status
		            Dim Result = Await model.ChangeStateCostInventoryGroup(_CostInventoryGroup)
		            If Result.StateResult = True Then
			            Mensaje(EeventViewerImages.Informacion) = "El estado se ha actualizado correctamente"
			            Me.Status = Result.ObjectEmbbeded.Status
                        Me._CostInventoryGroup.Status = Me.Status
			            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
			            AsyncLoader(False)
		            Else
			            AsyncLoader(False)
			            Mensaje(EeventViewerImages.Advertencia) = Result.Message
			            INDbeCode.Enabled = False
		            End If
	            End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbeCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

#Region "Inventory"

    Private Sub AddCostInventoryGroupDetailPopup(ByVal CostInventoryGroupDetail As CostInventoryGroupDetail)
        If Not CtrInventoryProduct.EditMode Then 'Si se esta guardando
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostInventoryGroupDetail(CostInventoryGroupDetail, CtrInventoryProduct.EditMode)
    End Sub

    Private Sub EditCostInventoryGroupDetail()
        Dim selected = CType(INDgvInventory.GetFocusedRow, CostInventoryGroupDetail)
        INDpceAddInventory.ShowPopup()
        CtrInventoryProduct.CostInventoryGroupDetail = selected
        CtrInventoryProduct.LoadControls()
    End Sub

    Private Sub DeleteCostInventoryGroupDetail()
        Dim selected = CType(INDgvInventory.GetFocusedRow, CostInventoryGroupDetail)
        _listCostInventoryGroupDetail.Remove(selected)

        AddCostInventoryGroupDetail(Nothing, True)
    End Sub

    Private Sub AddCostInventoryGroupDetail(ByVal CostInventoryGroupDetail As CostInventoryGroupDetail, ByVal editMode As Boolean)
        If Not editMode Then
            _listCostInventoryGroupDetail.Add(CostInventoryGroupDetail)
        End If

        INDgcInventory.DataSource = Nothing
        INDgcInventory.DataSource = _listCostInventoryGroupDetail
    End Sub

    Private Async Function CopyAndPasteInventory(listInfo As List(Of List(Of String))) As Task
        INDgvInventory.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()

        Dim errors As New List(Of String)
        Dim data As New List(Of List(Of String))

        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            For Each item In listInfo
                line = line + 1                
                Dim cantidad As Decimal

                If item.Count <> 2 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Producto - Cantidad Equivalente", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(0)) Then
                    errors.Add(String.Format("La linea {0} no contiene un codigo de Producto", line))
                    Continue For
                End If
                If Not Decimal.TryParse(item(1), cantidad) Then
                    errors.Add(String.Format("La linea {0} tiene una cantidad no numérica", line))
                    Continue For
                End If
                If cantidad <= 0 Then
                    errors.Add(String.Format("La cantidad de la linea {0} no puede ser menor o igual a 0", line))
                    Continue For
                End If

                data.Add(item)
            Next
        End If

        If data.Count > 0 Then
            Using model As New MCostInventoryGroup(MyTag)
                Dim result = Await model.CopyAndPasteCostInventoryGroupDetail(data)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgvInventory.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each CostInventoryGroupInventory In result.ObjectEmbbeded
                        If Not Me._listCostInventoryGroupDetail.Any(Function(d) d.InventoryProductId = CostInventoryGroupInventory.InventoryProductId) Then                            
                            Me.AddCostInventoryGroupDetail(CostInventoryGroupInventory, False)
                        End If
                    Next
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    errors.AddRange(result.MessageResult)
                End If
            End Using
        End If

        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvInventory.HideLoadingPanel()
    End Function

#End Region

#End Region

End Class