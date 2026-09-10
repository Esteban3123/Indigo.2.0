Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities

Public Class FrmEthnicGroups
    Implements IEthnicGroups

    Public WriteOnly Property ActionsOnControls As Boolean Implements IEthnicGroups.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            If value Then
                INDtxtDescription.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    Public Property Code As String Implements IEthnicGroups.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements IEthnicGroups.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Description As String Implements IEthnicGroups.Description
        Get
            Return INDtxtDescription.EditValue
        End Get
        Set(value As String)
            INDtxtDescription.EditValue = value
        End Set
    End Property

    Public Property Sequense As PayrollSequence Implements IEthnicGroups.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As PayrollSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequense.PayrollSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    Public Property Status As Boolean Implements IEthnicGroups.Status
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

    Dim presenter As PEthnicGroups

    Private Const NAME_MODULE As String = "Payroll"

    Private _sequense As Domain.Entities.PayrollSequence

    Dim _EthnicGroups As EthnicGroups

    Private _idOperativeUnit As Int32

    Private _idCurrentSequense As Int64

    Private _searchMode As Boolean

    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If _searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If
    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If _EthnicGroups IsNot Nothing AndAlso _EthnicGroups.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MEthnicGroups(Me.Tag.ToString())
                    AsyncLoader(True)
                    _EthnicGroups.MarkAsDeleted()
                    Dim result = Await Model.DeleteEthnicGroups(_EthnicGroups)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Using model As New MEthnicGroups(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveEthnicGroups(_EthnicGroups, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If _EthnicGroups.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then

                    If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                        Me.DicSequense(Me._sequense.PayrollSequenceDetail(0).Id).RemoveAt(0)
                    End If
                    If Me._sequense.Sequential Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    End If
                ElseIf _EthnicGroups.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me._EthnicGroups = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

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

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewEthnicGroups()
        End If
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        _searchMode = True
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.34)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListEthnicGroups
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._EthnicGroups IsNot Nothing AndAlso Me._EthnicGroups.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmSportPracticeMetaData, Eform.InfoMetaData), Me._EthnicGroups.Code, Me._EthnicGroups.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._EthnicGroups.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmSportPracticeMetaDataTitle, Eform.InfoMetaData), Me._EthnicGroups.Description),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmSportPracticeMetaData, Eform.InfoMetaData), Me._EthnicGroups.Code, Me._EthnicGroups.Description)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmSportPracticeMetaDataTitle, Eform.InfoMetaData), Me._EthnicGroups.Description)
            Return Me._doc
        End If

    End Function

    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Private Sub CleanControls()
        INDlcEthnicGroups.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        Description = String.Empty
        Status = True

        BarraBotones.CleanAuditBasic()
        INDlcEthnicGroups.EndUpdate()

        _EthnicGroups = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.ReassignOperatingUnit()
    End Sub

    Private Sub AssigningValues()
        With _EthnicGroups
            .Code = Code
            .Description = Description
        End With
    End Sub

    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MEthnicGroups(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetEthnicGroups(INDbtnCode.Text.Trim, True)
            AsyncLoader(False)
            _EthnicGroups = resultOperation.ObjectEmbbeded
            If Not _EthnicGroups Is Nothing Then
                If _EthnicGroups.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_EthnicGroups.Id))
                        With _EthnicGroups

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            Description = .Description
                            Status = .Status
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._EthnicGroups.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordPayroll With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = _EthnicGroups.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(_EthnicGroups.Id)
                        ActionsOnControls = True
                    End Using
                Else

                    If Me._sequense.IsManual Then
                        Await Me.NewEthnicGroups()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else

                If Me._sequense.IsManual Then
                    Await Me.NewEthnicGroups()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    INDbtnCode.Focus()
                End If
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        End Using
    End Function

    Private Async Function NewEthnicGroups() As Task
        _EthnicGroups = New EthnicGroups() With {.Status = True}
        If Me._sequense.Id > 0 Then
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = Me._sequense.PayrollSequenceDetail(0).Id
                ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me.Sequense.PayrollSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                        Me._idCurrentSequense = Me._sequense.PayrollSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Exit Function
                    End If
                End If
                If Not Me._sequense.Sequential Then
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                                Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                            End Using
                            If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = "No ha parametrizado la Secuencia Numérica"
        End If
    End Function

    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MEthnicGroups(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not _EthnicGroups.Status
                Dim Result = Await model.ChangeStateEthnicGroups(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    _EthnicGroups = Result.ObjectEmbbeded
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
    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        _sequense = Nothing
        _EthnicGroups = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        _searchMode = Nothing
    End Sub

    Private Sub FrmEthnicGroups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcEthnicGroups, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        presenter = New PEthnicGroups(Me)
        presenter.GetSequense()
        LoadStatus()
        Deshacer()
    End Sub

    Private Sub FrmEthnicGroups_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

    Private Sub FrmEthnicGroups_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.NewEthnicGroups()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PayrollSequenceDetail IsNot Nothing Then
            If Me._sequense.PayrollSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.PayrollSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

End Class