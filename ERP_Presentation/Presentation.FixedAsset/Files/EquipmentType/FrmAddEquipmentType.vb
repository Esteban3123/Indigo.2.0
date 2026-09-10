Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

Public Class FrmAddEquipmentType
    Implements IcrudBase


    Private _FixedAssetItemType As FixedAssetItemType

    ''' <summary>
    ''' 
    ''' </summary>
    Public Event RefreshDatasourceEquipmentType()

    ''' <summary>
    ''' Obtiene o establece el id del registro padre
    ''' </summary>
    Public Property ParentId As Integer?
        Get
            Return INDSleParent.EditValue
        End Get
        Set(value As Integer?)
            INDSleParent.EditValue = value
        End Set
    End Property

    Private _modeEdit As Boolean

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Dim _searchMode As Boolean

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
    ''' Gets or sets the organizational structure datasourse.
    ''' </summary>
    ''' <value>
    ''' The organizational structure datasourse.
    ''' </value>
    Public WriteOnly Property EquipmentTypeDatasourse As DevExpress.Xpo.XPInstantFeedbackSource
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleParent.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MEquipamentType

    Dim _equipmenttype As List(Of Tuple(Of Integer, String))

    Property INDAceptar As Boolean

    ReadOnly Property EquipmentType As List(Of Tuple(Of Integer, String))
        Get
            If _equipmenttype Is Nothing Then
                _equipmenttype = New List(Of Tuple(Of Integer, String))
                _equipmenttype.Add(New Tuple(Of Integer, String)(0, ResourceManager.GetString("Biomedico", "Maintenance")))
                _equipmenttype.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Industrial", "Maintenance")))
                _equipmenttype.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("MueblesEquiposOficina", "Maintenance")))
                _equipmenttype.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Otros", "Maintenance")))
            End If
            Return _equipmenttype
        End Get
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

    Private Sub INDsbAddRoot_Click(sender As Object, e As EventArgs)
        If INDtxtCode.Text = String.Empty Then
            MessageIndigo.Show("Especifique el codigo", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            INDtxtCode.Focus()
            Exit Sub
        End If
        If INDtxtDescription.Text = String.Empty Then
            MessageIndigo.Show("Especifique la descripcion", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            INDtxtDescription.Focus()
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        INDAceptar = True
        Me.Close()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
        _equipmenttype = Nothing
        INDAceptar = Nothing
    End Sub

    Private Sub FrmAddEquipmentType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeXpo()
        LoadStatus()
        ActionsOnControls = False
        If ModeEdit Then 'Si esta en modo edición
            LoadControls()
        Else 'Si se va a guardar
            Deshacer()
        End If
    End Sub

    Private Sub InitializeXpo()
        INDSleParent.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentType()
        INDSleInventoryType.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetInventoryType()
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Private Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If Not _searchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    Private Async Sub LoadControls()
        If Not String.IsNullOrEmpty(INDtxtCode.Text.Trim) AndAlso Not String.IsNullOrWhiteSpace(INDtxtCode.Text.Trim) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MEquipamentType()
                AsyncLoader(True)
                _FixedAssetItemType = Await Model.GetEquipamentTypeAsync(Me.INDtxtCode.Text.Trim)
                AsyncLoader(False)
                If Not _FixedAssetItemType Is Nothing AndAlso _FixedAssetItemType.Id > 0 Then
                    Using ModelCommonTreasury As New MBlockRecordAndSequenceFixedAsset(Me.Tag)
                        Dim result = Await ModelCommonTreasury.GetBlockRecord(Me.Tag, _FixedAssetItemType.Id)
                        If _FixedAssetItemType.ParentId Is Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = "Este es el registro principal, por lo tanto solo se puede modificar la descripción y el tipo de inventario"
                        End If
                        With _FixedAssetItemType
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            INDtxtDescription.Text = .Name
                            INDSleInventoryType.EditValue = .InventoryTypeId
                            ParentId = .ParentId
                        End With
                        InitializeXpo()
                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._FixedAssetItemType.Code)
                        'If result.Id = 0 Then
                        '    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        '    state.State = Domain.Base.Entities.ObjectState.Added
                        '    _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _organizationStruct.Id}
                        '    Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                        '    _record = operation.ObjectEmbbeded
                        'Else
                        '    _record = result
                        '    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        '    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        'End If
                        Me.BarraBotones.SetDocuments(_FixedAssetItemType.Id, Me.Tag, Nothing, GetType(FixedAssetItemType).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        ActionsOnControls = True
                        INDtxtCode.Enabled = False
                    End Using
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    ActionsOnControls = True
                    INDtxtCode.Enabled = False
                    _FixedAssetItemType = New FixedAssetItemType
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDtxtCode.Text.Trim()) Then
                LoadControls()
                INDtxtDescription.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Public Sub AssigningValues()
        With _FixedAssetItemType
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = INDtxtCode.Text.Trim()
            .Name = INDtxtDescription.Text
            .InventoryTypeId = INDSleInventoryType.EditValue
            .ParentId = ParentId
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        INDlyMain.BeginUpdate()
        ActionsOnControls = False
        INDtxtCode.Text = String.Empty
        INDtxtDescription.Text = String.Empty
        INDSleInventoryType.EditValue = Nothing
        'INDSleParent.EditValue = Nothing
        INDlyMain.EndUpdate()

        _FixedAssetItemType = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        'DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDtxtCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDSleInventoryType.Enabled = value
            INDSleParent.Enabled = value
            If value Then
                INDtxtDescription.Focus()
            Else
                INDtxtCode.Focus()
            End If
        End Set
    End Property

    Public Property Code As String
        Get
            Return Me.INDtxtCode.Text
        End Get
        Set(value As String)
            Me.INDtxtCode.Text = value
        End Set
    End Property

    Private Sub INDtxtCode_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtCode.Properties.ButtonClick
        OpenSearch()
    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        'Me.BarraBotones.PermiteConsultar = True
        'Dim FormSearchObjects = New FrmBusqueda
        ''AddHandler CType(FormSearchObjects.GridViewBusquedas, DevExpress.XtraGrid.Views.Grid.GridView).CustomColumnDisplayText, AddressOf CustomColumGrid
        'With FormSearchObjects
        '    .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 400}}.ToList
        '    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListEquipmentTypeMaintenance
        '    '.ValorSolicitado = "Codigo"
        '    .FormParent = Me
        '    .ShowSearch()
        'End With
    End Sub

    Private Sub CustomColumGrid(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs)
        If e.Value IsNot Nothing AndAlso e.Value.GetType.ToString <> "DevExpress.Data.NotLoadedObject" AndAlso CStr(e.Value) <> "" Then
            If e.Column.FieldName = "InventoryType" Then
                Select Case e.Value
                    Case Is = 1
                        e.DisplayText = ResourceManager.GetString("Biomedico", "Maintenance")
                    Case Is = 2
                        e.DisplayText = ResourceManager.GetString("Infraestructura", "Maintenance")
                    Case Is = 3
                        e.DisplayText = ResourceManager.GetString("MueblesEquiposOficina", "Maintenance")
                    Case Is = 4
                        e.DisplayText = ResourceManager.GetString("Industriales", "Maintenance")
                End Select
            End If
        End If
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsPopup() As Boolean
        If INDtxtCode.Text.Trim().Equals(String.Empty) OrElse INDtxtDescription.Text.Trim().Equals(String.Empty) OrElse ParentId Is Nothing OrElse INDSleInventoryType.EditValue Is Nothing Then
            Return False
        End If
        Return True
    End Function

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If _FixedAssetItemType.Id <> 0 AndAlso _FixedAssetItemType.ParentId Is Nothing AndAlso ParentId IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Este es el registro principal, por lo tanto no se puede modificar la jerarquía"
        Else
            If Not (_FixedAssetItemType.Id <> 0 AndAlso _FixedAssetItemType.ParentId) Then
                If ParentId IsNot Nothing Then
                    If ValidateControlsPopup() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        Exit Sub
                    End If
                End If
            End If
            AssigningValues()
            Try
                Using Model As New MEquipamentType()
                    AsyncLoader(True)
                    Dim Result = Await Model.SaveEquipamentTypeAsync(Me._FixedAssetItemType)
                    If Result.StateResult = True Then
                        If _FixedAssetItemType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        ElseIf _FixedAssetItemType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        _searchMode = False
                        AsyncLoader(False)
                        Me.Deshacer()
                        RaiseEvent RefreshDatasourceEquipmentType()
                        'Evento de Actualizar estructura
                    Else
                        AsyncLoader(False)
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDtxtCode.Enabled = False
                Throw ex
            End Try
        End If
        'Try
        '    If ValidateControls() = False Then
        '        Mensaje(EeventViewerImages.Informacion) = "Por favor llene todos los campos"
        '        Exit Sub
        '    End If
        '    Using Model As New MEquipamentType
        '        AsyncLoader(True)
        '        Dim resultSave = Await Model.SaveEquipamentTypeAsync(_FixedAssetItemType)
        '        AsyncLoader(False)
        '        If resultSave = True Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '        Else
        '            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '        End If
        '    End Using
        '    INDTreeListEquipmentType.RefreshDataSource()
        '    Deshacer()
        'Catch ex As Exception
        '    AsyncLoader(False)
        'End Try
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Me._FixedAssetItemType IsNot Nothing AndAlso Me._FixedAssetItemType.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEquipamentType()
                        AsyncLoader(True)
                        Me._FixedAssetItemType.MarkAsDeleted()
                        Dim result = Await Model.DeleteEquipamentTypeAsync(Me._FixedAssetItemType)
                        If result Then
                            'Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            _searchMode = False
                            Me.Deshacer()
                            RaiseEvent RefreshDatasourceEquipmentType()
                        Else
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error inesperado al intentar eliminar el registro"
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub


    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

#Region "BarButton Events"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            'Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

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

    Private Sub INDSleParent_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleParent.QueryPopUp
        If INDSleParent.Properties.DataSource Is Nothing Then
            'EquipmentTypeDatasourse = Nothing
        End If
    End Sub

    Private Sub INDSleInventoryType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInventoryType.QueryPopUp
        If INDSleInventoryType.Properties.DataSource Is Nothing Then
            'INDSleInventoryType.Properties.DataSource = Await Model.ListAllInventoryType()
        End If
    End Sub
#End Region
End Class