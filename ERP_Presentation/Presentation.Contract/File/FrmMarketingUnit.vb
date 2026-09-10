'***********************************************************************
' Assembly         : Presentacion.Contracts
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
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

#End Region

Public Class FrmMarketingUnit
    Implements IMarketingUnit

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IMarketingUnit.Status
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
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As ContractSequence Implements IMarketingUnit.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As ContractSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequense.ContractSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMarketingUnit.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IMarketingUnit.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IMarketingUnit.Code
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

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameMU As String Implements IMarketingUnit.NameMU
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de entidades cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CupsEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMarketingUnit.CupsEntityXpo
        Get
            Return INDsleCupsEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCupsEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de entidades cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCupsEntity As Integer Implements IMarketingUnit.IdCupsEntity
        Get
            Return INDsleCupsEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleCupsEntity.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PMarketingUnit

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim marketingUnit As MarketingUnit

    ''' <summary>
    ''' Listado de entidades cups
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMarketingUnitCups As List(Of MarketingUnitCups)

    ''' <summary>
    ''' Listado de entidades cups eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteMarketingUnitCups As List(Of MarketingUnitCups)

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
    Private record As BlockRecordContract

    ''' <summary>
    ''' Entidades Cups xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupEntityXpo As Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo

    ''' <summary>
    ''' Controla el editvalueChanged del control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private ban As Boolean = False

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
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
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If marketingUnit IsNot Nothing AndAlso marketingUnit.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MMarketingUnit(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteMarketingUnit(marketingUnit)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
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
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Using model As New MMarketingUnit(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveMarketingUnit(marketingUnit, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If marketingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                        Me.DicSequense(Me._sequense.ContractSequenceDetail(0).Id).RemoveAt(0)
                    End If
                    If Me._sequense.Sequential Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    End If
                ElseIf marketingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.marketingUnit = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
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

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewMarketingUnit()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.88)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMarketingUnit
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMarketingUnit.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleCupsEntity.Enabled = value
            INDbtnAdd.Enabled = value
            INDgcCupsEntity.Enabled = value
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
        If Me.marketingUnit IsNot Nothing AndAlso Me.marketingUnit.Id > 0 Then
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
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
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

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.marketingUnit.Code, Me.marketingUnit.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.marketingUnit.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.marketingUnit.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.marketingUnit.Code, Me.marketingUnit.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.marketingUnit.Code)
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
        INDlyMarketingUnit.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameMU = String.Empty
        ban = True
        IdCupsEntity = Nothing
        ban = False
        INDgcCupsEntity.DataSource = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyMarketingUnit.EndUpdate()

        ListMarketingUnitCups = Nothing
        ListDeleteMarketingUnitCups = Nothing
        marketingUnit = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If ListMarketingUnitCups Is Nothing OrElse ListMarketingUnitCups.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontListCups", NAME_MODULE)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With marketingUnit
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameMU

            If ListDeleteMarketingUnitCups IsNot Nothing Then
                For Each item As MarketingUnitCups In ListDeleteMarketingUnitCups
                    .MarketingUnitCups.Add(item)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
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
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MMarketingUnit(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetMarketingUnit(INDbtnCode.Text.Trim)
            marketingUnit = resultOperation.ObjectEmbbeded
            If Not marketingUnit Is Nothing Then
                If marketingUnit.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(marketingUnit.Id))
                        With marketingUnit
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            NameMU = .Name
                            ListMarketingUnitCups = .MarketingUnitCups.ToList
                            INDgcCupsEntity.DataSource = Nothing
                            INDgcCupsEntity.DataSource = ListMarketingUnitCups

                            Status = .Status
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.marketingUnit.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = marketingUnit.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        AsyncLoader(False)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(marketingUnit.Id)
                        ActionsOnControls = True
                    End Using
                Else
                    'AsyncLoader(False)
                    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    'Me.Code = String.Empty
                    If Me._sequense.IsManual Then
                        Await Me.NewMarketingUnit()
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        INDbtnCode.Text = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
            Else
                'AsyncLoader(False)
                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                'Me.Code = String.Empty
                If Me._sequense.IsManual Then
                    Await Me.NewMarketingUnit()
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    INDbtnCode.Text = String.Empty
                    INDbtnCode.Focus()
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMarketingUnit() As Task
        marketingUnit = New MarketingUnit() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.ContractSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
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
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MMarketingUnit(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not marketingUnit.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    marketingUnit = Result.ObjectEmbbeded
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

    ''' <summary>
    ''' Crea el objeto para el listado de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateMarketingUnitCups()
        If ListMarketingUnitCups Is Nothing Then
            ListMarketingUnitCups = New List(Of MarketingUnitCups)
        Else
            Dim cont As Integer = ListMarketingUnitCups.FindAll(Function(item) item.CupsEntityId = IdCupsEntity).ToList().Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CupsEntityDuplicated", NAME_MODULE)
                INDsleCupsEntity.Focus()
                Exit Sub
            End If
        End If
        Dim marketingUnitCups As New MarketingUnitCups
        With marketingUnitCups
            .CupsEntityId = _cupEntityXpo.Id
            .CupsEntityCode = _cupEntityXpo.Code
            .CupsEntityDescription = _cupEntityXpo.Description
        End With
        ListMarketingUnitCups.Add(marketingUnitCups)
        marketingUnit.MarketingUnitCups.Add(marketingUnitCups)
        If marketingUnit.Id > 0 Then
            marketingUnit.MarkAsModified()
        End If
        INDgcCupsEntity.DataSource = Nothing
        INDgcCupsEntity.DataSource = ListMarketingUnitCups
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("CupsEntityAgregatedSatisfactory", NAME_MODULE)
        INDsleCupsEntity.EditValue = Nothing
        INDsleCupsEntity.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteMarketingUnitCups()
        Dim muc As MarketingUnitCups = CType(viewCups.GetFocusedRow, MarketingUnitCups)
        If muc.Id <> 0 Then
            If ListDeleteMarketingUnitCups Is Nothing Then
                ListDeleteMarketingUnitCups = New List(Of MarketingUnitCups)
            End If
            muc.MarkAsDeleted()
            ListDeleteMarketingUnitCups.Add(muc)
        End If
        ListMarketingUnitCups.Remove(muc)
        marketingUnit.MarketingUnitCups.Remove(muc)
        If marketingUnit.Id > 0 Then
            marketingUnit.MarkAsModified()
        End If
        INDgcCupsEntity.DataSource = Nothing
        INDgcCupsEntity.DataSource = ListMarketingUnitCups
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequense = Nothing
        marketingUnit = Nothing
        ListMarketingUnitCups = Nothing
        ListDeleteMarketingUnitCups = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        _cupEntityXpo = Nothing
        ban = Nothing
        SearchMode = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMarketingUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMarketingUnit, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PMarketingUnit(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDgcCupsEntity)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewCups, ListActions)
        SearchMode = False
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de entidades cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCupsEntity.QueryPopUp
        If INDsleCupsEntity.Properties.DataSource Is Nothing Then
            Presenter.InitializeCupsEntity()
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMarketingUnit_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
        '        Await Me.NewMarketingUnit()
        '    Else
        '        Await Me.LoadControls()
        '    End If
        'End If
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.NewMarketingUnit()
                Else
                    Await Me.LoadControls()
                End If
            End If
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
    Private Sub FrmMarketingUnit_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de entidades cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCupsEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsEntity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCupsEntity()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteMarketingUnitCups()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteMarketingUnitCups()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de entidades cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCupsEntity.EditValueChanged
        If INDsleCupsEntity.EditValue IsNot Nothing AndAlso ban = False Then
            _cupEntityXpo = DirectCast(DirectCast(viewSearchCups.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If INDsleCupsEntity.EditValue IsNot Nothing Then
            CreateMarketingUnitCups()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedCups", NAME_MODULE)
            INDsleCupsEntity.Focus()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

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
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.ContractSequenceDetail IsNot Nothing Then
            If Me._sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region
    
End Class