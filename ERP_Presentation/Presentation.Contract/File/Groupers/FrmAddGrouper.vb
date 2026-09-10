#Region "Imports"

Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Contract.MVP
Imports System.Math
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

#End Region

Public Class FrmAddGrouper
    Implements IGroupers

#Region "Consts"

    ''' <summary>
    ''' The modul e_ name
    ''' </summary>
    Public Const MODULE_NAME As String = "Contract"

#End Region

#Region "Variables"

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PGroupers

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Dim _idOperativeUnit As Int32

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _blockRecord As BlockRecordContract

    '''' <summary>
    '''' variable que contiene la entidad
    '''' </summary>
    Dim _grouper As Groupers

    ''' <summary>
    ''' Representa el detalle del cups
    ''' </summary>
    ''' <remarks></remarks>
    Dim _grouperCups As GroupersCups

    ''' <summary>
    ''' Listado de cups
    ''' </summary>
    Dim _listGroupersCups As List(Of GroupersCups)

    ''' <summary>
    ''' Listado de cups eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteGrouperCups As List(Of GroupersCups)

    ''' <summary>
    ''' Selector de EntityCups
    ''' </summary>
    Dim _selectorCupsEntity As SelectorCache = New SelectorCache("Id", "CodeDescription", "SubGroupCodeName", "GroupCodeName")

    ''' <summary>
    ''' Representa el detalle de la actividad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _grouperActivities As GroupersActivities

    ''' <summary>
    ''' Listado de actividades
    ''' </summary>
    Dim _listGroupersActivities As List(Of GroupersActivities)

    ''' <summary>
    ''' Listado de actividades eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteGrouperActivities As List(Of GroupersActivities)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IGroupers.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IGroupers.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGroupers.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDslePattern.Enabled = value
            INDTxtUserNumber.Enabled = value
            INDTxtUserMin.Enabled = value
            INDTxtUserMax.Enabled = value
            INDTxtCME.Enabled = value
            INDTxtFrequence.Enabled = value
            INDTxtTotalContract.Enabled = value
            INDTxtUserValue.Enabled = value

            INDTxtMinimunRange.Enabled = value
            INDtxtMaximunRange.Enabled = value
            INDSlMetricUnit.Enabled = value
            INDtxtWarningFor.Enabled = value
            INDMeWarningMessage.Enabled = value
            INDslMaximunRangeRestrict.Enabled = value
            INDMeRestrictMessage.Enabled = value

            INDPceAddCups.Enabled = value
            INDEsbExport.Enabled = value
            INDGcCups.Enabled = value

            INDSlActivities.Enabled = value
            INDBtnAddActivities.Enabled = value
            INDgcActivities.Enabled = value

            INDlcRoot.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el código de la estructura organizacional
    ''' </summary>
    Public Property Code As String Implements IGroupers.Code
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
    Public Property NameGroupers As String Implements IGroupers.NameGroupers
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
    Public Property ParentId As Integer? Implements IGroupers.ParentId
        Get
            Return INDslePattern.EditValue
        End Get
        Set(value As Integer?)
            INDslePattern.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si esta en modo edición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModeEdit As Boolean

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IGroupers.Status
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

#Region "Datasource"

    ''' <summary>
    ''' Gets or sets the organizational structure datasourse.
    ''' </summary>
    ''' <value>
    ''' The organizational structure datasourse.
    ''' </value>
    Public Property GroupersXpo As XPInstantFeedbackSource Implements IGroupers.GroupersXpo
        Get
            Return CType(INDslePattern.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePattern.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    Public Property CupsEntityXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroupers.CupsEntityXPO
        Get
            Return CType(INDSleCupsEntity.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCupsEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    Public Property ActivitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroupers.ActivitiesXpo
        Get
            Return CType(INDSlActivities.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlActivities.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListGroupers
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        Deshacer()
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MGroupers(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveGroupers(Me._grouper, 0)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _grouper.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf _grouper.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If

                    Me._grouper = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                    RaiseEvent RefreshDatasourceStruct()
                Else
                    If Result.Message IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me._grouper IsNot Nothing AndAlso Me._grouper.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Me._grouper.MarkAsDeleted()
                    Using model As New MGroupers(MyTag)
                        Dim result = Await model.DeleteGroupers(Me._grouper)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                            RaiseEvent RefreshDatasourceStruct()
                        Else
                            AsyncLoader(False)
                            If result.Message IsNot Nothing Then
                                Mensaje(EeventViewerImages.MensajeError) = result.Message
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)

        IndigoGridView1.SetListAcction(ViewCups, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewCups.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        IndigoGridView2.SetListAcction(ViewActivities, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewActivities.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub InitializeTuples()
        Dim ListMeasurementUnit = New List(Of Tuple(Of Integer, String))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(1, "Diario"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(2, "Semanal"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(3, "Mensual"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(4, "Bimensual"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(5, "Trimestral"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(6, "Semestral"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(7, "Anual"))
        INDSlMetricUnit.Properties.DataSource = ListMeasurementUnit.ToList
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Async Sub CleanControls()
        INDlcRoot.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDslePattern.EditValue = Nothing
        INDslePattern.Properties.NullText = String.Empty
        INDTxtUserNumber.EditValue = 0
        INDTxtUserMin.EditValue = 0
        INDTxtUserMax.EditValue = 0
        INDTxtCME.EditValue = 0
        INDTxtFrequence.EditValue = 0
        INDTxtTotalContract.EditValue = 0
        INDTxtUserValue.EditValue = 0

        INDTxtMinimunRange.EditValue = 0
        INDtxtMaximunRange.EditValue = 0
        INDSlMetricUnit.EditValue = Nothing
        INDtxtWarningFor.EditValue = 0
        INDMeWarningMessage.Text = String.Empty
        INDslMaximunRangeRestrict.EditValue = Nothing
        INDMeRestrictMessage.Text = String.Empty

        _selectorCupsEntity.Clear()
        INDGvCupsEntity.RefreshData()
        INDGcCups.DataSource = Nothing

        INDSlActivities.EditValue = Nothing
        INDSlActivities.Properties.NullText = String.Empty
        INDgcActivities.DataSource = Nothing

        _doc = Nothing
        _grouper = Nothing
        _listGroupersCups = Nothing
        _listDeleteGrouperCups = Nothing
        _listGroupersActivities = Nothing
        _listDeleteGrouperActivities = Nothing

        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDlcRoot.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using mCommonCost As New MBlockRecordAndSequense(Me.Tag)
                Await mCommonCost.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._grouper.Code, Me._grouper.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._grouper.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._grouper.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._grouper.Code, Me._grouper.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._grouper.Code)
        End If
        Return Me._doc
    End Function

    Public Async Function LoadControls() As Task
        If String.IsNullOrEmpty(Code) OrElse String.IsNullOrWhiteSpace(Code) Then
            Exit Function
        End If
        Try
            Me.BarraBotones.StatusRecordVisible = True
            Using model As New MGroupers(MyTag)
                AsyncLoader(True)
                Dim resultOperation = Await model.GetGroupers(Me.Code)
                If Not resultOperation.StateResult Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                    Exit Function
                End If
                _grouper = resultOperation.ObjectEmbbeded
                If Not _grouper Is Nothing AndAlso _grouper.Id > 0 Then
                    Using mCommonCost As New MBlockRecordAndSequense(Me.Tag)
                        _blockRecord = Await mCommonCost.GetBlockRecord(Me.Tag, _grouper.Id)
                        With _grouper
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            NameGroupers = .Description
                            ParentId = .ParentId
                            INDTxtUserNumber.EditValue = .UserNumber
                            INDTxtUserMin.EditValue = .UserMin
                            INDTxtUserMax.EditValue = .UserMax
                            INDTxtCME.EditValue = .ProjectCME
                            INDTxtFrequence.EditValue = .Frequence
                            INDTxtTotalContract.EditValue = .TotalContract
                            INDTxtUserValue.EditValue = .UserValue

                            INDTxtMinimunRange.EditValue = .MinimunRange
                            INDtxtMaximunRange.EditValue = .MaximunRange
                            INDSlMetricUnit.EditValue = .MeasurementUnit
                            INDtxtWarningFor.EditValue = .WarningFor
                            INDMeWarningMessage.Text = .WarningMessage
                            INDslMaximunRangeRestrict.EditValue = .MaximunRangeRestrict
                            INDMeRestrictMessage.Text = .RestrictMessage
                            Status = .Status

                            _listGroupersCups = _grouper.GroupersCups.ToList()
                            INDGcCups.DataSource = Nothing
                            INDGcCups.DataSource = _listGroupersCups
                            ViewCups.ExpandAllGroups()

                            _listGroupersActivities = _grouper.GroupersActivities.ToList()
                            INDgcActivities.DataSource = Nothing
                            INDgcActivities.DataSource = _listGroupersActivities
                        End With
                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._grouper.Code)
                        If _blockRecord.Id = 0 Then
                            _blockRecord = (Await mCommonCost.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _grouper.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(_grouper.Id, MyTag, Nothing, GetType(CostOrganizationalStructureOfCosts).Name)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    End Using
                Else
                    AsyncLoader(False)

                    _grouper.Status = True
                    Status = True
                    BarraBotones.StatusRecordVisible = True

                    ActionsOnControls = True

                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Public Sub AssigningValues()
        With _grouper
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = NameGroupers
            .ParentId = ParentId
            .UserNumber = CInt(INDTxtUserNumber.EditValue)
            .UserMin = CInt(INDTxtUserMin.EditValue)
            .UserMax = CInt(INDTxtUserMax.EditValue)
            .ProjectCME = CDec(INDTxtCME.EditValue)
            .Frequence = CDec(INDTxtFrequence.EditValue)
            .TotalContract = CDec(INDTxtTotalContract.EditValue)
            .UserValue = CDec(Round(INDTxtUserValue.EditValue, 6))

            .MinimunRange = CInt(INDTxtMinimunRange.EditValue)
            .MaximunRange = CInt(INDtxtMaximunRange.EditValue)
            .MeasurementUnit = CByte(INDSlMetricUnit.EditValue)
            .WarningFor = CInt(INDtxtWarningFor.EditValue)
            .WarningMessage = INDMeWarningMessage.Text
            .MaximunRangeRestrict = CBool(INDslMaximunRangeRestrict.EditValue)
            .RestrictMessage = INDMeRestrictMessage.Text
        End With

        _grouper.GroupersCups.Clear()
        If _listGroupersCups IsNot Nothing Then
            For Each i In _listGroupersCups
                If i.Id = 0 Then
                    _grouper.GroupersCups.Add(i)
                End If
            Next
        End If
        If _listDeleteGrouperCups IsNot Nothing Then
            For Each i In _listDeleteGrouperCups
                _grouper.GroupersCups.Add(i)
            Next
        End If

        _grouper.GroupersActivities.Clear()
        If _listGroupersActivities IsNot Nothing Then
            For Each i In _listGroupersActivities
                If i.Id = 0 Then
                    _grouper.GroupersActivities.Add(i)
                End If
            Next
        End If
        If _listDeleteGrouperActivities IsNot Nothing Then
            For Each i In _listDeleteGrouperActivities
                _grouper.GroupersActivities.Add(i)
            Next
        End If

        If _grouper.Id > 0 Then
            _grouper.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MGroupers(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim state As Boolean = Not _grouper.Status
                    Dim Result = Await model.ChangeState(Code, state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        _grouper = Result.ObjectEmbbeded
                    Else
                        If Result.Message = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        End If
                    End If
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

#Region "Details"

    ''' <summary>
    ''' Deletes the cups.
    ''' </summary>
    Private Sub DeleteCups()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _grouperCups = CType(ViewCups.GetFocusedRow, GroupersCups)
            _listGroupersCups.Remove(_grouperCups)
            If _grouperCups.Id > 0 Then
                If _listDeleteGrouperCups Is Nothing Then
                    _listDeleteGrouperCups = New List(Of GroupersCups)
                End If
                _grouperCups.MarkAsDeleted()
                _listDeleteGrouperCups.Add(_grouperCups)
            End If

            INDGcCups.DataSource = Nothing
            INDGcCups.DataSource = _listGroupersCups
            ViewCups.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Deletes the activities.
    ''' </summary>
    Private Sub DeleteActivities()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _grouperActivities = CType(ViewActivities.GetFocusedRow, GroupersActivities)
            _listGroupersActivities.Remove(_grouperActivities)
            If _grouperActivities.Id > 0 Then
                If _listDeleteGrouperActivities Is Nothing Then
                    _listDeleteGrouperActivities = New List(Of GroupersActivities)
                End If
                _grouperActivities.MarkAsDeleted()
                _listDeleteGrouperActivities.Add(_grouperActivities)
            End If

            INDgcActivities.DataSource = Nothing
            INDgcActivities.DataSource = _listGroupersActivities
        End If
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Public Events"

    ''' <summary>
    ''' Evento que se ejecuta para actualizar los niveles de la estructura
    ''' </summary>
    Public Event RefreshDatasourceStruct()

#End Region

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmOrganizationalStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _presenter = New PGroupers(Me)
        _presenter.LoadDefinitionLayout()
        '******************************
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        INDEsbExport.AddRangeColumns("Código CUPS", "Código Descripción")

        AddActionsColumns()
        InitializeTuples()
        LoadStatus()
        If ModeEdit Then 'Si esta en modo edición
            Await LoadControls()
        Else 'Si se va a guardar
            Deshacer()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _grouperCups = Nothing
        _grouperActivities = Nothing
        _listDeleteGrouperCups = Nothing
        _listDeleteGrouperActivities = Nothing
        _idOperativeUnit = Nothing
        _grouper = Nothing
        _presenter = Nothing
        _blockRecord = Nothing
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Handles the FormClosing event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmOrganizationalStructure_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
                Await LoadControls()
                INDtxtName.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddCups_KeyDown(sender As Object, e As KeyEventArgs) Handles INDPceAddCups.KeyDown
        If e.KeyCode = Keys.F4 OrElse e.KeyCode = Keys.Enter Then
            INDPceAddCups.ShowPopup()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara despues de desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddCups_Popup(sender As Object, e As EventArgs) Handles INDPceAddCups.Popup
        INDSleCupsEntity.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDslePattern_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePattern.QueryPopUp
        If GroupersXpo Is Nothing Then
            _presenter.InitializeGroupers()
        End If
    End Sub

    Private Sub INDSleCupsEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCupsEntity.QueryPopUp
        If CupsEntityXPO Is Nothing Then
            _presenter.InitializeCUPSEntity()
        End If
    End Sub

    Private Sub INDSlActivities_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlActivities.QueryPopUp
        If ActivitiesXpo Is Nothing Then
            _presenter.InitializeActivities()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddCups_Click(sender As Object, e As EventArgs) Handles INDBtnAddCups.Click
        If _selectorCupsEntity.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione al menos un item de entidad CUPS."
            Exit Sub
        End If

        If Me._listGroupersCups Is Nothing Then
            _listGroupersCups = New List(Of GroupersCups)
        End If

        Dim listCupsWithDescriptionsIds = _presenter.CupsWithDescriptionsId(_selectorCupsEntity.GetKeys())
        Dim listCupsWithoutDescriptionsIds = _selectorCupsEntity.GetKeysToArray.Where(Function(s) Not listCupsWithDescriptionsIds.Contains(s)).Select(Function(d) CInt(d)).ToList()

        If listCupsWithDescriptionsIds.Any() Then
            Me.Cursor = ChangeCursorIndigo()
            Using Formulario As New FrmSelectCupsDescription()
                Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 600
                Formulario.Height = 500
                Formulario.ListCupsIds = listCupsWithDescriptionsIds
                Dim frm As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)

                If Formulario.DialogResult = System.Windows.Forms.DialogResult.OK Then
                    If Formulario.ListCupsEntityWithDescriptionsXpo IsNot Nothing AndAlso Formulario.ListCupsEntityWithDescriptionsXpo.Count > 0 Then
                        For Each item In Formulario.ListCupsEntityWithDescriptionsXpo
                            Dim cupsTmp = _listGroupersCups.Where(Function(x) x.CUPSEntityId = item.CUPSEntityId AndAlso x.ContractDescriptionId IsNot Nothing AndAlso x.ContractDescriptionId = item.ContractDescriptionId).FirstOrDefault()
                            If cupsTmp IsNot Nothing Then
                                Continue For
                            End If

                            _grouperCups = New GroupersCups
                            _grouperCups.CUPSEntityId = item.CUPSEntityId
                            _grouperCups.DescriptionCups = item.CUPSEntityCodeName
                            _grouperCups.CupsSubGroupCodeName = _selectorCupsEntity.GetValueByKey(item.CUPSEntityId, "SubGroupCodeName")
                            _grouperCups.CupsGroupCodeName = _selectorCupsEntity.GetValueByKey(item.CUPSEntityId, "GroupCodeName")
                            _grouperCups.ContractDescriptionId = item.ContractDescriptionId
                            _grouperCups.CUPSEntityContractDescriptionId = item.CUPSEntityContractDescriptionId
                            _grouperCups.ContractDescriptionCodeName = item.ContractDescriptionCodeName
                            _listGroupersCups.Add(_grouperCups)
                        Next
                    End If
                End If
            End Using
        End If

        For Each cupsEntityId In listCupsWithoutDescriptionsIds
            Dim cupsTmp = _listGroupersCups.Where(Function(x) x.CUPSEntityId = cupsEntityId AndAlso x.ContractDescriptionId Is Nothing).FirstOrDefault()
            If cupsTmp IsNot Nothing Then
                Continue For
            End If

            _grouperCups = New GroupersCups
            _grouperCups.CUPSEntityId = cupsEntityId
            _grouperCups.DescriptionCups = _selectorCupsEntity.GetValueByKey(cupsEntityId, "CodeDescription")
            _grouperCups.CupsSubGroupCodeName = _selectorCupsEntity.GetValueByKey(cupsEntityId, "SubGroupCodeName")
            _grouperCups.CupsGroupCodeName = _selectorCupsEntity.GetValueByKey(cupsEntityId, "GroupCodeName")
            _listGroupersCups.Add(_grouperCups)
        Next

        _selectorCupsEntity.Clear()
        INDSleCupsEntity.Properties.NullText = "0 item seleccionado"
        INDGvCupsEntity.RefreshData()
        INDSleCupsEntity.Focus()

        INDGcCups.DataSource = Nothing
        INDGcCups.DataSource = _listGroupersCups
        ViewCups.ExpandAllGroups()
    End Sub

    Private Sub INDBtnAddActivities_Click(sender As Object, e As EventArgs) Handles INDBtnAddActivities.Click
        _grouperActivities = New GroupersActivities()
        With _grouperActivities
            .AGACTIMEDCode = INDSlActivities.EditValue
            .AGACTIMEDName = INDSlActivities.Text
        End With
        If _listGroupersActivities Is Nothing Then
            _listGroupersActivities = New List(Of GroupersActivities)
        End If
        If _listGroupersActivities.Where(Function(x) x.AGACTIMEDCode = INDSlActivities.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Esta Actividad ya se encuentra en el listado."
            Exit Sub
        End If
        _listGroupersActivities.Add(_grouperActivities)

        INDSlActivities.EditValue = Nothing
        INDSlActivities.Focus()

        INDgcActivities.DataSource = Nothing
        INDgcActivities.DataSource = _listGroupersActivities
    End Sub

#End Region

#Region "Selection"

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCupsEntity.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCupsEntity" Then
                e.Value = _selectorCupsEntity.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCupsEntity.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvCupsEntity" Then
                selector = _selectorCupsEntity
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
                view.RefreshRow(e.RowHandle)
            Else
                selector.Clear()
                view.RefreshData()
            End If
        End If
    End Sub

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCupsEntity.Closed
        Dim selector As SelectorCache = Nothing
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing

        If searchLookupEdit.Name = "INDSleCupsEntity" Then
            selector = _selectorCupsEntity
        End If

        If selector.Count = 1 Then
            searchLookupEdit.Properties.NullText = "1 Item Seleccionado"
        Else
            searchLookupEdit.Properties.NullText = String.Format("{0} Items Seleccionados", selector.Count)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDslMaximunRangeRestrict_EditValueChanged(sender As Object, e As EventArgs) Handles INDslMaximunRangeRestrict.EditValueChanged
        If INDslMaximunRangeRestrict.EditValue IsNot Nothing AndAlso CBool(INDslMaximunRangeRestrict.EditValue) Then
            INDLciMessageRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciMessageRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDMeRestrictMessage.Text = String.Empty
        End If
    End Sub

    Private Sub INDTxtUserNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtUserNumber.EditValueChanged, INDTxtTotalContract.EditValueChanged
        If INDTxtUserNumber.EditValue IsNot Nothing AndAlso INDTxtTotalContract.EditValue IsNot Nothing Then
            If CDec(INDTxtUserNumber.EditValue) = 0 OrElse CDec(INDTxtTotalContract.EditValue) = 0 Then
                INDTxtUserValue.EditValue = 0
            Else
                INDTxtUserValue.EditValue = CDec(CDec(INDTxtTotalContract.EditValue) / CDec(INDTxtUserNumber.EditValue))
            End If
        Else
            INDTxtUserValue.EditValue = 0
        End If
    End Sub

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteCups()
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        DeleteActivities()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._grouper IsNot Nothing AndAlso Me._grouper.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDGcCups.Name Then
            ViewCups.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MGroupers(MyTag)
                Dim result = Await model.CopyAndPasteGroupersCups(ListInfo)
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If _listGroupersCups IsNot Nothing AndAlso _listGroupersCups.Count > 0 Then
                        For Each item In result.ObjectEmbbeded
                            If _listGroupersCups.Any(Function(x) x.CUPSEntityId = item.CUPSEntityId AndAlso (If(x.ContractDescriptionId Is Nothing, 0, x.ContractDescriptionId) = If(item.ContractDescriptionId Is Nothing, 0, item.ContractDescriptionId))) Then
                                Continue For
                            End If
                            _listGroupersCups.Add(item)
                        Next
                    Else
                        _listGroupersCups = result.ObjectEmbbeded
                    End If
                End If
                If Not String.IsNullOrEmpty(result.Message) Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDGcCups.DataSource = Nothing
            INDGcCups.DataSource = _listGroupersCups
            ViewCups.HideLoadingPanel()
            ViewCups.ExpandAllGroups()
        End If
    End Function

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
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
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

End Class