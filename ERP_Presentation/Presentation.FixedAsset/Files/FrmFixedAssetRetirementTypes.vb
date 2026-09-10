'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports Presentation.FixedAsset.MVP

#End Region

Public Class FrmFixedAssetRetirementTypes
    Implements IFixedAssetRetirementTypes

#Region "Properties"

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetRetirementTypes.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As FixedAssetSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As FixedAssetSequenceDetail In Me._sequense.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetRetirementTypes.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetRetirementTypes.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetRetirementTypes.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text.Trim()
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameFT As String Implements IFixedAssetRetirementTypes.NameFT
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecordReplacementValue As Boolean Implements IFixedAssetRetirementTypes.RecordReplacementValue
        Get
            Return INDrgRecordReplacementValue.EditValue
        End Get
        Set(value As Boolean)
            INDrgRecordReplacementValue.EditValue = value
        End Set
    End Property
#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetRetirementTypes

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As FixedAssetSequence

    ''' <summary>
    ''' Representa la entidad de grupo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim fixedAssetRetirementTypes As FixedAssetRetirementTypes

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If FixedAssetRetirementTypes IsNot Nothing AndAlso FixedAssetRetirementTypes.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MFixedAssetRetirementTypes(Me.Tag.ToString())
                    AsyncLoader(True)
                    fixedAssetRetirementTypes.MarkAsDeleted()
                    Dim result = Await Model.DeleteFixedAssetRetirementTypes(fixedAssetRetirementTypes)
                    If result.StateResult = True Then
                        'Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        SearchMode = False
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

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues()
        Try
            Using model As New MFixedAssetRetirementTypes(Me.Tag.ToString())
                Dim Result As ActionResult(Of FixedAssetRetirementTypes) = Await model.SaveFixedAssetRetirementTypes(fixedAssetRetirementTypes, _idCurrentSequense)
                If Result.StatusCode = eStatusResult.SUCCESS Then
                    If fixedAssetRetirementTypes.ChangeTracker.State = ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    ElseIf FixedAssetRetirementTypes.ChangeTracker.State = ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.fixedAssetRetirementTypes = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    SearchMode = False
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequense Is Nothing OrElse _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequense.IsManual Then
Deshacer()
        Else
            Await NewFixedAssetRetirementTypes()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Registra Valor de Reposición", .FieldName = "RecordReplacementValue", .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListFixedAssetRetirementTypes
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa las tuplas
    ''' </summary>
    Private Sub InitializeTuple()

    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetRetirementTypes.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDrgRecordReplacementValue.Enabled = value
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.fixedAssetRetirementTypes IsNot Nothing AndAlso Me.fixedAssetRetirementTypes.Id > 0 Then
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

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.fixedAssetRetirementTypes.Code, Me.fixedAssetRetirementTypes.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.fixedAssetRetirementTypes.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.fixedAssetRetirementTypes.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.fixedAssetRetirementTypes.Code, Me.fixedAssetRetirementTypes.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.fixedAssetRetirementTypes.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyFixedAssetRetirementTypes.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameFT = Nothing
        RecordReplacementValue = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyFixedAssetRetirementTypes.EndUpdate()
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With fixedAssetRetirementTypes
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameFT
            .RecordReplacementValue = RecordReplacementValue
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetRetirementTypes(CStr(Me.Tag))
                    AsyncLoader(True)

                    Dim resultOperation = Await Model.GetFixedAssetRetirementTypesByCode(INDbtnCode.Text.Trim)
                    fixedAssetRetirementTypes = resultOperation.ObjectEmbbeded

                    If fixedAssetRetirementTypes IsNot Nothing AndAlso fixedAssetRetirementTypes.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(fixedAssetRetirementTypes.Id))
                            With fixedAssetRetirementTypes
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                NameFT = .Name
                                RecordReplacementValue = .RecordReplacementValue
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.fixedAssetRetirementTypes.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = fixedAssetRetirementTypes.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequense.IsManual Then
                            Await Me.NewFixedAssetRetirementTypes()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValidateCode", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Crea la entidad cups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateNew()
        fixedAssetRetirementTypes = New FixedAssetRetirementTypes
        ActionsOnControls = True
        INDtxtName.Focus()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewFixedAssetRetirementTypes() As Task
        fixedAssetRetirementTypes = New FixedAssetRetirementTypes()

        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
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
            End If
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        Presenter = Nothing
        _sequense = Nothing
        fixedAssetRetirementTypes = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCompanyType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyFixedAssetRetirementTypes, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetRetirementTypes(Me)
        Presenter.GetSequense()
        InitializeTuple()
        LoadStatus()
        Deshacer()
        SearchMode = False
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetRetirementTypes_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewFixedAssetRetirementTypes()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCompanyType_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.FixedAssetSequenceDetail IsNot Nothing Then
            If Me._sequense.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region

End Class