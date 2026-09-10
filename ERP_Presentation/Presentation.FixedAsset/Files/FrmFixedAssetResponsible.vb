Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

Public Class FrmFixedAssetResponsible
    Implements IFixedAssetResponsible
    Private Const NAME_MODULE As String = "FixedAssets"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetResponsible

    Dim Responsible As FixedAssetResponsible

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecordFixedAsset

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFixedAssetResponsible(MyBase.Tag)

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    Dim searchMode As Boolean = False

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32



#Region "Properties"
    ''' <summary>
    ''' datasource de Tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlThirdParty.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Tipo de Vinculación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VinculationTypeXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlVinculationType.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlVinculationType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Tipo de responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleTypeXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlResponsibleType.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlResponsibleType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Unidades Funcionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlFunctionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetResponsible.ActionsOnControls
        Set(value As Boolean)
            INDLcResponsible.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDSlFunctionalUnit.Enabled = value
            INDSlResponsibleType.Enabled = value
            INDSlThirdParty.Enabled = value
            INDSlVinculationType.Enabled = value
            INDGcFunctionalUnit.Enabled = value
            INDBtnAdd.Enabled = value
            INDSpnCapacity.Enabled = value

            INDLcResponsible.EndUpdate()

            If value Then
                INDSlThirdParty.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    Public Property CodeResponsible As String Implements IFixedAssetResponsible.CodeResponsible
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public Property IdResponsibleType As Integer Implements IFixedAssetResponsible.IdResponsibleType
        Get
            Return INDSlResponsibleType.EditValue
        End Get
        Set(value As Integer)
            INDSlResponsibleType.EditValue = value
        End Set
    End Property

    Public Property IdThirdParty As Integer Implements IFixedAssetResponsible.IdThirdParty
        Get
            Return INDSlThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSlThirdParty.EditValue = value
        End Set
    End Property

    Public Property IdVinculationType As Integer Implements IFixedAssetResponsible.IdVinculationType
        Get
            Return INDSlVinculationType.EditValue
        End Get
        Set(value As Integer)
            INDSlVinculationType.EditValue = value
        End Set
    End Property

    Public Property Sequence As Domain.Entities.FixedAssetSequence Implements IFixedAssetResponsible.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Status As Boolean Implements IFixedAssetResponsible.StateResponsible
        Get
            Return CBool(Me.BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property

#End Region

#Region "Query POP UP"
    Private Sub INDSlThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlThirdParty.QueryPopUp
        If INDSlThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ThirdPartyXPO Is Nothing Then
            Using model As New MFixedAssetResponsible(MyBase.Tag)
                ThirdPartyXPO = model.ListThirdPartyXpo()
            End Using
        End If
    End Sub

    Private Sub INDSlVinculationType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlVinculationType.QueryPopUp
        If INDSlVinculationType.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If VinculationTypeXPO Is Nothing Then
            Using model As New MFixedAssetResponsible(MyBase.Tag)
                VinculationTypeXPO = model.ListVinculationType()
            End Using
        End If
    End Sub

    Private Sub INDSlResponsibleType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlResponsibleType.QueryPopUp
        If INDSlResponsibleType.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ResponsibleTypeXPO Is Nothing Then
            Using model As New MFixedAssetResponsible(MyBase.Tag)
                ResponsibleTypeXPO = model.ListResponsibleType()
            End Using
        End If
    End Sub

    Private Sub INDSlFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlFunctionalUnit.QueryPopUp
        If INDSlFunctionalUnit.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If FunctionalUnitXPO Is Nothing Then
            Using model As New MFixedAssetResponsible(MyBase.Tag)
                FunctionalUnitXPO = model.ListFunctionalUnit()
            End Using
        End If
    End Sub
#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        Presenter = Nothing
        Responsible = Nothing
        record = Nothing
        Model = Nothing
        _idCurrentSequence = Nothing
        searchMode = Nothing
        _idOperativeUnit = Nothing
    End Sub


    Private Sub FrmFixedAssetResponsible_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Presenter = New PFixedAssetResponsible(Me)
        ' Presenter.GetSequense()
        'AddActionsColumns()
        ' CleanControls()

        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetResponsible(Me)
        Presenter.GetSequense()
        'Presenter.LoadDefinitionLayout()
        AddActionsColumns()
        LoadStatus()
        Deshacer()
    End Sub
#End Region

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "ThirdPartyId.Name"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Marcas.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        'If searchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'If Responsible IsNot Nothing And INDBteCode.Enabled = False Then
        '    If Responsible.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Try
        '                AsyncLoader(False)
        '                Using Model As New MFixedAssetResponsible(MyBase.Tag)
        '                    If Await Model.DeleteResponsibleAsync(Responsible) = True Then
        '                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '                        searchMode = False
        '                        Deshacer()
        '                        Await Me.DeleteDocumentIndexed()
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                    End If
        '                End Using
        '                AsyncLoader(False)
        '            Catch ex As Exception
        '                Throw ex
        '                AsyncLoader(False)
        '            End Try
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        'End If



        If Me.Responsible IsNot Nothing AndAlso Me.Responsible.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFixedAssetResponsible(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteResponsibleAsync(Me.Responsible)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de Fondos.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        '    Exit Sub
        'End If
        'AsyncLoader(True)
        'AssigningValues()
        'Using Model As New MFixedAssetResponsible(MyBase.Tag)
        '    If Await Model.SaveResponsibleAsync(Responsible, _idCurrentSequense) = True Then
        '        If Responsible.ChangeTracker.State = ObjectState.Added Then
        '            'Se descarta la secuencia numerica usada
        '            If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                If Me.DicSequense(Me._sequense.FixedAssetSequenceDetail(0).Id).Count > 0 Then
        '                    Me.DicSequense(Me._sequense.FixedAssetSequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '            End If
        '        End If
        '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '        If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
        '        ElseIf Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '        End If
        '        AsyncLoader(False)
        '        searchMode = False
        '        Deshacer()
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '        AsyncLoader(False)
        '    End If
        'End Using


        If Not ValidateControls() Then

            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        If INDSlThirdParty.Text Is String.Empty OrElse INDSlVinculationType.Text Is String.Empty OrElse INDSlResponsibleType.Text Is String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        AssigningValues()

        Try
            Using Model As New MFixedAssetResponsible(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of FixedAssetResponsible) = Await Model.SaveResponsibleAsync(Me.Responsible, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Responsible = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewResponsible()
        End If
    End Sub


    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ' BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        'INDBteCode.Text = String.Empty
        'INDSlFunctionalUnit.EditValue = 0
        'INDSlResponsibleType.EditValue = 0
        'INDSlThirdParty.EditValue = 0
        'INDSlVinculationType.EditValue = 0
        'INDSlResponsibleType.Properties.NullText = String.Empty
        'INDSlThirdParty.Properties.NullText = String.Empty
        'INDSlVinculationType.Properties.NullText = String.Empty
        'INDGcFunctionalUnit.DataSource = Nothing
        'Me.BarraBotones.StatusRecordVisible = False
        ' DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.CleanAuditBasic()
        'Responsible = Nothing
        'ActionsOnControls = False


        INDLcResponsible.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDBteCode.Text = String.Empty
        INDSlFunctionalUnit.EditValue = 0
        INDSlResponsibleType.EditValue = 0
        INDSlThirdParty.EditValue = 0
        INDSlVinculationType.EditValue = 0
        INDSpnCapacity.EditValue = 0
        INDSlResponsibleType.Properties.NullText = String.Empty
        INDSlThirdParty.Properties.NullText = String.Empty
        INDSlVinculationType.Properties.NullText = String.Empty
        INDGcFunctionalUnit.DataSource = Nothing
        'Limpiar controles
        Responsible = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcResponsible.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'StateResponsible = True
        'AsyncLoader(True)
        'Using Model As New MFixedAssetResponsible(MyBase.Tag)
        '    Responsible = Await Model.GetResponsibleAsync(INDBteCode.Text)
        'End Using
        'If Not Responsible Is Nothing Then
        '    If Responsible.Id > 0 Then
        '        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '            Dim result = Await ModelRecord.GetBlockRecord(Me.Tag, Responsible.Id)
        '            With Responsible
        '                LogicaBotonActualizar(True)
        '                Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Responsible.CreationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Responsible.CreationDate)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Responsible.ModificationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Responsible.ModificationDate)
        '                CodeResponsible = .Code
        '                IdThirdParty = .ThirdPartyId
        '                INDSlThirdParty.Properties.NullText = .ThirdPartyName
        '                IdVinculationType = .VinculationTypeId
        '                INDSlVinculationType.Properties.NullText = .VinculationName
        '                IdResponsibleType = .ReponsibleTypeId
        '                INDSlResponsibleType.Properties.NullText = .ResponsibleTypeName
        '                StateResponsible = .Status
        '                INDGcFunctionalUnit.DataSource = .ResponsibleFunctionalUnit
        '            End With
        '            Me.GetDocumentIndexed(Me.Tag & "_" & Me.Responsible.Code)
        '            If result.Id = 0 Then
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                record = New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = Responsible.Id}
        '                Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                record = operation.ObjectEmbbeded
        '            Else
        '                record = result
        '                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '            End If
        '        End Using
        '    Else
        '        LogicaBotonActualizar(False)
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        'Else
        '    Responsible = New FixedAssetResponsible
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'AsyncLoader(False)
        'ActionsOnControls = True



        If Not String.IsNullOrEmpty(CodeResponsible) AndAlso Not String.IsNullOrWhiteSpace(CodeResponsible) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetResponsible(CStr(Me.Tag))
                    AsyncLoader(True)
                    Responsible = Await Model.GetResponsibleAsync(INDBteCode.Text)
                    INDLcResponsible.BeginUpdate()
                    If Responsible IsNot Nothing AndAlso Responsible.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Responsible.Id))
                            With Responsible
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                CodeResponsible = .Code
                                IdThirdParty = .ThirdPartyId
                                INDSlThirdParty.Properties.NullText = .ThirdPartyName
                                IdVinculationType = .VinculationTypeId
                                INDSlVinculationType.Properties.NullText = .VinculationName
                                IdResponsibleType = .ReponsibleTypeId
                                INDSlResponsibleType.Properties.NullText = .ResponsibleTypeName
                                INDGcFunctionalUnit.DataSource = .ResponsibleFunctionalUnit

                                INDSpnCapacity.EditValue = .Capacity

                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Responsible.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Responsible.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(Responsible.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetResponsible).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewResponsible()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeResponsible = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcResponsible.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.Responsible.Code, INDSlThirdParty.Text, INDSlThirdParty.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.Responsible.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.Responsible.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.Responsible.Code, INDSlThirdParty.Text, INDSlThirdParty.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.Responsible.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub
#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de fondos.
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
        searchMode = False
        Me.Deshacer()
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
        ' StateResponsible = True
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

#End Region

#Region "Customizacion"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLcResponsible.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLcResponsible.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If INDSlFunctionalUnit.EditValue IsNot Nothing Then

            If Responsible.ResponsibleFunctionalUnit.Where(Function(x) x.IdFunctionalUnit = INDSlFunctionalUnit.EditValue).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La Unidad Funcional ya se encuentra Agregada"
                Return
            End If

            Dim ResponsibleFunctionalUnit As New ResponsibleFunctionalUnit()
            ResponsibleFunctionalUnit.IdFunctionalUnit = INDSlFunctionalUnit.EditValue
            Using Modelo As New MFixedAssetResponsible(MyBase.Tag)
                Dim FunctionalUnit As FunctionalUnit = Await Modelo.GetFunctionalUnitByIdAsync(INDSlFunctionalUnit.EditValue)
                If FunctionalUnit IsNot Nothing Then
                    FunctionalUnit.ChangeTracker.State = ObjectState.Unchanged
                    ResponsibleFunctionalUnit.FunctionalUnit = FunctionalUnit
                End If

            End Using

            Responsible.ResponsibleFunctionalUnit.Add(ResponsibleFunctionalUnit)
            If Responsible.ChangeTracker.State = ObjectState.Unchanged Then
                Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDSlFunctionalUnit.EditValue = Nothing
            INDGcFunctionalUnit.DataSource = Responsible.ResponsibleFunctionalUnit
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUsuario, UnidadesFuncionales)
        End If
    End Sub

    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDBteCode.Text.Trim()) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDBteCode.Text.Trim()) Then
        '            Await Me.NewResponsible()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeResponsible.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeResponsible) Then
                    Await Me.NewResponsible()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewResponsible() As Task
        'If _sequense Is Nothing OrElse _sequense.Id = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "No existe secuencia numérica para este formulario"
        '    Exit Function
        'End If
        'Responsible = New FixedAssetResponsible()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Function
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                    Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If

        'End If


        Responsible = New FixedAssetResponsible() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function


    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.Responsible.Code) Then
            Try
                Using model As New MFixedAssetResponsible(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.Responsible.Status
                    Dim result As ActionResult(Of FixedAssetResponsible) = Await model.ChangeState(Me.Responsible.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Responsible = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Responsible
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeResponsible
            .ThirdPartyId = IdThirdParty
            .VinculationTypeId = IdVinculationType
            .ReponsibleTypeId = IdResponsibleType
            .Capacity = CByte(INDSpnCapacity.EditValue)
        End With
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(GridView1, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.ButtonEdit = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case button.Text
            Case "Eliminar"
                DeleteDetail()
        End Select
    End Sub

    Private Sub DeleteDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim Objresponsible = CType(GridView1.GetFocusedRow(), ResponsibleFunctionalUnit)
            Objresponsible.MarkAsDeleted()
            Responsible.MarkAsModified()
        End If
    End Sub

    Private Sub INDSlThirdParty_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
                Using model As New MFixedAssetResponsible(MyBase.Tag)
                    ThirdPartyXPO = model.ListThirdPartyXpo()
                End Using
        End If
    End Sub

    Private Sub INDSlVinculationType_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlVinculationType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1710, Nothing, True)
            Using model As New MFixedAssetResponsible(MyBase.Tag)
                VinculationTypeXPO = model.ListVinculationType()
            End Using
        End If
    End Sub

    Private Sub INDSlResponsibleType_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlResponsibleType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1709, Nothing, True)
            Using model As New MFixedAssetResponsible(MyBase.Tag)
                ResponsibleTypeXPO = model.ListResponsibleType()
            End Using
        End If
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Responsible IsNot Nothing AndAlso Me.Responsible.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
End Class