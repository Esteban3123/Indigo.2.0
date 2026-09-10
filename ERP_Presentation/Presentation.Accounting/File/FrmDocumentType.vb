'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Diego Andrés Roldán
' Created          : 15-01-2013
'
' Last Modified By : Nicolas Pulido
' Last Modified On : 09/02/2016
' Description      : Se agrega la opción de agregar varios consecutivos a un mismo tipo de documento contable
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls

#End Region

''' <summary>
''' Formulario de tipos de documentos
''' </summary>
Public Class FrmDocumentType
    Implements IDocumentType, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable que contiene una secuencia
    ''' </summary>
    Private _sequence As GeneralLedgerSequence

    ''' <summary>
    ''' Contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IDocumentType.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As GeneralLedgerSequence Implements IDocumentType.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequence.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Encapsula la entidad del tipo de documento
    ''' </summary>
    Private _docType As New JournalVoucherTypes

    ''' <summary>
    ''' Encapsula la entidad del tipo de consecutivo del documento
    ''' </summary>
    Private _docTypeConsecutive As New JournalVoucherTypeConsecutive
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PDocumentType
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordGeneralLedger
    ''' <summary>
    ''' representa la entidad de tipo de consecutivo
    ''' </summary>
    ''' <remarks></remarks>
    Private typeConsecutive As JournalVoucherTypeConsecutive

    ''' <summary>
    ''' listado de los tipos de consecutivos
    ''' </summary>
    ''' <remarks></remarks>
    Private listTypeConsecutive As New List(Of JournalVoucherTypeConsecutive)

    ''' <summary>
    ''' listado de eliminados los tipos de consecutivos
    ''' </summary>
    ''' <remarks></remarks>
    Private listDeleteTypeConsecutive As New List(Of JournalVoucherTypeConsecutive)



#End Region

#Region "Properties"

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
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
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value>Valor que indica si se activan o inactivan</value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDocumentType.ActionsOnControls
        Set(value As Boolean)
            INDlyDocumetType.BeginUpdate()
            INDbteCode.Enabled = Not value
            Me.INDbteName.Enabled = value
            Me.INDtxtDescription.Enabled = value
            Me.INDseConsecutive.Enabled = value
            Me.INDPceConsecutive.Enabled = value
            INDGcListConsecutive.Enabled = value
            INDlyDocumetType.EndUpdate()
            If value Then
                INDbteName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el código del tipo de documento
    ''' </summary>
    ''' <value>Código del tipo de documento</value>
    ''' <returns>El código del tipo de documento</returns>
    Public Property CodeDocumentType As String Implements IDocumentType.CodeDocumentType
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                    Return String.Empty
                Else
                    Return INDbteCode.Text
                End If
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el número de consecutivo del tipo de documento
    ''' </summary>
    ''' <value>Número consecutivo del tipo de documento</value>
    ''' <returns>El número consecutivo del tipo de documento</returns>
    Public Property ConsecutiveDocumentType As Long Implements IDocumentType.ConsecutiveDocumentType
        Get
            Return Me.INDseConsecutive.EditValue
        End Get
        Set(value As Long)
            Me.INDseConsecutive.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la descripción del tipo de documento
    ''' </summary>
    ''' <value>Descripción del tipo de documento</value>
    ''' <returns>La descripción del tipo de documento</returns>
    Public Property DescriptionDocumentType As String Implements IDocumentType.DescriptionDocumentType
        Get
            Return Me.INDtxtDescription.Text.Trim()
        End Get
        Set(value As String)
            Me.INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el estado del documento
    ''' </summary>
    ''' <value>Estado del documento</value>
    ''' <returns>El estado del documento</returns>
    Public Property StateDocumentType As Boolean Implements IDocumentType.StateDocumentType
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
    ''' <summary>
    ''' Obtiene o establece el nombre del tipo de documento
    ''' </summary>
    ''' <value>
    ''' The type of the name document.
    ''' </value>
    Public Property NameDocumentType As String Implements IDocumentType.NameDocumentType
        Get
            Return INDbteName.Text
        End Get
        Set(value As String)
            INDbteName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <value>
    ''' The type of the name document.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDocumentType.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property


#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Abre el formulario de busqueda
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Se deshacen los cambios y se prepara el frontal para una nueva consulta
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Se ejecuta la eliminación del tipo de documento
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        If Me._docType IsNot Nothing AndAlso Me._docType.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MDocumentType(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteDocumentType(Me._docType)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If

    End Sub

    ''' <summary>
    ''' Se ejecuta el guardado o actualizado del tipo de documento
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MDocumentType(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of JournalVoucherTypes) = Await Model.SaveDocumentType(Me._docType, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _docType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._docType = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metododo para consultar el tipo de documento 
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If String.IsNullOrEmpty(CodeDocumentType) OrElse String.IsNullOrWhiteSpace(CodeDocumentType) Then
            Exit Function
        End If

        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Try
            Using Model As New MDocumentType(CStr(Me.Tag))
                AsyncLoader(True)
                INDlyDocumetType.BeginUpdate()

                _docType = Await Model.GetDocumentType(INDbteCode.Text.Trim)
                INDlyDocumetType.BeginUpdate()

                If _docType IsNot Nothing AndAlso _docType.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True
                    Await CargarInformacionEntidad()
                    Await ManejarBloqueoRegistro(Model)
                    ConfigurarBarraHerramientas()
                    AsyncLoader(False)
                    ActionsOnControls = True
                Else
                    AsyncLoader(False)
                    Await ManejarSecuenciaNoEncontrada()
                End If

                INDlyDocumetType.EndUpdate()
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Function

    ' Métodos auxiliares extraídos para claridad y responsabilidad única

    Private Async Function CargarInformacionEntidad() As Task
        With _docType
            LayoutControls.SetCustomFieldsValue(.CustomProperties)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

            CodeDocumentType = .Code
            NameDocumentType = .Name
            ConsecutiveDocumentType = .Consecutive
            DescriptionDocumentType = .Description
            StateDocumentType = .Status
            listTypeConsecutive = .JournalVoucherTypeConsecutive.ToList()
            INDGcListConsecutive.DataSource = Nothing
            INDGcListConsecutive.DataSource = listTypeConsecutive
        End With

        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._docType.Code)
    End Function

    Private Async Function ManejarBloqueoRegistro(Model As MDocumentType) As Task
        _record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(_docType.Id))
        If _record.Id = 0 Then
            _record = (Await Model.SaveBlockRecord(
            New BlockRecordGeneralLedger With {
                .BlockDate = Date.Now,
                .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                .NameUser = Me.indigo.UserIndigoName,
                .IdForm = Me.Tag,
                .CodUser = Me.indigo.UserIndigo,
                .IdRecord = _docType.Id
            }
        )).ObjectEmbbeded
        Else
            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
        End If
        Me.BarraBotones.SetDocuments(_docType.Id, Me.Tag.ToString(), Nothing, GetType(JournalVoucherTypes).Name)
    End Function

    Private Sub ConfigurarBarraHerramientas()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
    End Sub

    Private Async Function ManejarSecuenciaNoEncontrada() As Task
        If Me._sequence.IsManual Then
            Await Me.NewDocumentType()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
            CodeDocumentType = String.Empty
            INDbteCode.Focus()
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Genera el documento indexado
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._docType.Code, Me._docType.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._docType.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._docType.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._docType.Code, Me._docType.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._docType.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Me._docType
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = CodeDocumentType
            .Name = Me.NameDocumentType
            .Description = Me.DescriptionDocumentType
            If ConsecutiveDocumentType < 0 Then
                .Consecutive = Me.ConsecutiveDocumentType * -1
            Else
                .Consecutive = Me.ConsecutiveDocumentType
            End If

            'asigna los valores agregados en el grid a una lista: listTypeConsecutive
            If listTypeConsecutive IsNot Nothing AndAlso listTypeConsecutive.Count > 0 Then
                For Each item In listTypeConsecutive
                    .JournalVoucherTypeConsecutive.Add(item)
                Next
            End If

            'asigna los valores eliminados en el grid a una lista: listTypeConsecutive
            If listDeleteTypeConsecutive IsNot Nothing AndAlso listDeleteTypeConsecutive.Count > 0 Then
                For Each item In listDeleteTypeConsecutive
                    .JournalVoucherTypeConsecutive.Add(item)
                Next
            End If

        End With
    End Sub



    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()

        INDlyDocumetType.BeginUpdate()
        ActionsOnControls = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        StateDocumentType = True
        'Limpiar controles

        INDbteCode.Text = String.Empty
        Me.INDbteName.Text = String.Empty
        Me.INDseConsecutive.EditValue = Nothing
        Me.INDtxtDescription.Text = String.Empty
        ConsecutiveDocumentType = 0
        _docType = Nothing
        Me.INDGcListConsecutive.DataSource = Nothing
        listTypeConsecutive = Nothing
        listDeleteTypeConsecutive = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyDocumetType.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MDocumentType(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 30},
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "Consecutive", .ColumnWidth = 150}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes
            .ValorSolicitado = "Code"
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
        CodeDocumentType = ReturnValue
        If CodeDocumentType IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        'No se implementa
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewDocumentType()
        End If
    End Sub

    ''' <summary>
    ''' Actualiza el estado.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me._docType.Code) Then
            Try
                Using Model As New MDocumentType(Me.Tag)
                    AsyncLoader(True)
                    Dim stateCard As Boolean = Not Me._docType.Status
                    Dim Result = Await Model.UpdateStateDocumentType(Me._docType.Code, stateCard)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me._docType = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo concepto de retencion
    ''' </summary>
    Private Async Function NewDocumentType() As Task
        _docType = New JournalVoucherTypes() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeDocumentType = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeDocumentType = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MDocumentType(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeDocumentType = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeDocumentType = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Método usado para validar la eliminación de un registro del grid
    ''' </summary>
    Private Sub DeleteDetail()

        'Valida si se elimina el registro seleccionado del grid
        If MessageIndigo.Show("Desea eliminar el registro?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        'Obtiene el campo a eliminar del grid
        Dim row As JournalVoucherTypeConsecutive = INDGvListConsecutive.GetFocusedRow()
        If row.Id > 0 Then
            If listDeleteTypeConsecutive Is Nothing Then
                listDeleteTypeConsecutive = New List(Of JournalVoucherTypeConsecutive)
            End If
            row.MarkAsDeleted()
            listDeleteTypeConsecutive.Add(row)
        End If
        If row IsNot Nothing Then
            listTypeConsecutive.Remove(row)
            INDGcListConsecutive.DataSource = Nothing
            INDGcListConsecutive.DataSource = listTypeConsecutive
        End If
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceConsecutive_Popup(sender As Object, e As EventArgs) Handles INDPceConsecutive.Popup
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDPceConsecutive.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceConsecutive.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If INDsleLegalBook.Properties.DataSource Is Nothing Then
            INDsleLegalBook.Properties.DataSource = _presenter.InitializeBook()
        End If
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _docType = Nothing
        _docTypeConsecutive = Nothing
        _presenter = Nothing
        _record = Nothing
        typeConsecutive = Nothing
        listTypeConsecutive = Nothing
        listDeleteTypeConsecutive = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el mas del search del control de libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            INDsleLegalBook.Properties.DataSource = _presenter.InitializeBook()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se inicializa el formulario y sus valiables
    ''' </summary>
    Private Sub FrmDocumentType_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PDocumentType(Me)
        _presenter.GetSequence()

        IndigoGridControl1.RefreshGrid(INDGcListConsecutive)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvListConsecutive, ListActions)
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Aqui se realiza el desbloqueo del registro antes de cerrar el frontal
    ''' </summary>
    Private Sub FrmDocumentType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el codigo y consulta el registro
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeDocumentType.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeDocumentType) Then
                    Await Me.NewDocumentType()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al hacer click en el boton del control y lanza el form de busqueda
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._docType IsNot Nothing AndAlso Me._docType.Id > 0 Then
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

#Region "ToolBar Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        '_searchMode = False
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
        Console.Write(adder)
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub
    ''' <summary>
    ''' Cambiar Unidad Operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Activated y Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de agregar datos al Grid cuando se oprime el boton INDSbAddConsecutive
    ''' </summary>
    Dim DataSet As System.Data.DataSet = New DataSet()
    Dim table As System.Data.DataTable = New DataTable("ParentTable")
    Dim column As DataColumn
    Dim row As DataRow
    Dim adder As Integer = 0

    Private Sub INDSbAddConsecutive_Click(sender As Object, e As EventArgs) Handles INDSbAddConsecutive.Click

        'Valida que los campos del popup esten diligenciados
        If INDTxtYear.EditValue = Nothing Or INDTxtConsecutive.EditValue = Nothing Or INDsleLegalBook.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe llenar todos los datos del popup"
            INDsleLegalBook.Focus()
            Exit Sub
        End If

        'Si la lista esta vacia me crea una nueva lista
        If listTypeConsecutive Is Nothing Then
            listTypeConsecutive = New List(Of JournalVoucherTypeConsecutive)
        Else
            'Si la lista contiene datos me valida que el año que está ingresando no se encuentre actualmente en el grid
            If (From x In listTypeConsecutive Where x.Year = INDTxtYear.EditValue AndAlso x.LegalBookId = INDsleLegalBook.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya contiene el año " + INDTxtYear.Text + " en el listado"
                INDsleLegalBook.Focus()
                Exit Sub
            End If
        End If

        'Asigna los datos de los textbox a los campos de la entidad
        typeConsecutive = New JournalVoucherTypeConsecutive
        With typeConsecutive
            .JournalVoucherTypeId = _docType.Id
            .Year = INDTxtYear.EditValue
            .Consecutive = INDTxtConsecutive.EditValue
            .LegalBookId = INDsleLegalBook.EditValue
            .LegalBookDescription = INDsleLegalBook.Text
        End With

        'Agrega los datos de la entidad a una lista llamada listTypeConsecutive
        listTypeConsecutive.Add(typeConsecutive)
        INDGcListConsecutive.DataSource = Nothing

        'Agrega los datos de la lista al grid: INDGcListConsecutive
        INDGcListConsecutive.DataSource = listTypeConsecutive

        'Vacia los campos del popup
        INDsleLegalBook.EditValue = Nothing
        INDTxtYear.EditValue = Nothing
        INDTxtConsecutive.EditValue = Nothing

        'Carga el foco en INDTxtYear del popup
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Evento que llama al método DeleteDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

    ''' <summary>
    ''' Evento que llama al método DeleteDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub

#End Region

End Class