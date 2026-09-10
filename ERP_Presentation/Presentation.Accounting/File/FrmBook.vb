'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/11/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
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

Public Class FrmBook
    Implements IBook

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IBook.Code
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
    ''' Obtiene o establece la descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IBook.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece la propiedad para los LayoutControls
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBook.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IBook.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name1 As String Implements IBook.Name
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As GeneralLedgerSequence Implements IBook.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequense.GeneralLedgerSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IBook.Status
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
    ''' Obtiene o establece el tipo de libro contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeBook As Integer? Implements IBook.TypeBook
        Get
            Return INDsleTypeBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleTypeBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el libro es uno solo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OfficialBook As Boolean? Implements IBook.OfficialBook
        Get
            Return INDsleOfficialBook.EditValue
        End Get
        Set(value As Boolean?)
            INDsleOfficialBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Variable del id de la moneda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OfficialCurrencyId As Integer Implements IBook.OfficialCurrencyId
        Get
            Return CType(INDsleddlCurrency.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleddlCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene las monedas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Currency As XPInstantFeedbackSource Implements IBook.Currency
        Get
            Return CType(INDsleddlCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleddlCurrency.Properties.DataSource = value
        End Set
    End Property

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
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBook.ActionsOnControls
        Set(value As Boolean)
            INDlyBook.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleTypeBook.Enabled = value
            INDsleOfficialBook.Enabled = value
            INDsleddlCurrency.Enabled = value
            INDmemoDescription.Enabled = value
            INDlyBook.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Presentador 
    ''' </summary>
    Dim Presenter As PBook

    ''' <summary>
    ''' Variable que contiene la entidad de conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim Book As LegalBook

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordGeneralLedger

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable que contiene la lista de tipos de libro contable
    ''' </summary>
    Dim ListTypeBook As New List(Of Tuple(Of Integer, String))

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

        If OfficialBook Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar el registro porque es un libro oficial."
            INDtxtName.Focus()
            Exit Sub
        End If
        If Book IsNot Nothing AndAlso Book.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MBook(Me.Tag.ToString())
                    AsyncLoader(True)
                    Book.MarkAsDeleted()
                    Dim result = Await Model.DeleteBook(Book)
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
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = True Then
            If OfficialBook AndAlso Not {1, 2}.Contains(TypeBook) Then
                Mensaje(EeventViewerImages.Advertencia) = "El libro oficial debe ser de tipo COLGAAP o NIIF."
                Exit Sub
            End If

            AssigningValues()
            Using model As New MBook(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveBook(Book, _idCurrentSequense)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If Book.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.GeneralLedgerSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequense.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf Book.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.Book = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    SearchMode = False
                    Me.Deshacer()
                    OfficialBook = False
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult(0) = "ErrorOfficialBook" Then
                        Mensaje(EeventViewerImages.Advertencia) = "El registro no se puede guardar porque ya existe un libro oficial."
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            NewBook()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.Book IsNot Nothing AndAlso Me.Book.Id > 0 Then
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
    ''' Llena el control de comportamiento con el listado
    ''' </summary>
    Private Sub InitializeTuples()
        ListTypeBook = New List(Of Tuple(Of Integer, String))
        ListTypeBook.Add(New Tuple(Of Integer, String)(1, "COLGAAP"))
        ListTypeBook.Add(New Tuple(Of Integer, String)(2, "NIIF"))
        ListTypeBook.Add(New Tuple(Of Integer, String)(3, "Cuentas Estados Financieros"))
        INDsleTypeBook.Properties.DataSource = ListTypeBook.ToList()
        INDsleTypeBook.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' Método para establecer la lógica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        'No se implementa
    End Sub


    ''' <summary>
    ''' Este método abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim ListStatus As New List(Of Tuple(Of String, Boolean))
        ListStatus.Add(New Tuple(Of String, Boolean)("Inactivo", 0))
        ListStatus.Add(New Tuple(Of String, Boolean)("Activo", 1))

        Dim ListTypeOS As New List(Of Tuple(Of String, Integer))
        ListTypeOS.Add(New Tuple(Of String, Integer)("COLGAAP", 1))
        ListTypeOS.Add(New Tuple(Of String, Integer)("NIIF", 2))
        ListTypeOS.Add(New Tuple(Of String, Integer)("Cuentas Estados Financieros", 3))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "TypeBook", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListTypeOS},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CommonCurrency.CurrencyName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListStatus}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllBook
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Método que retorna el valor de la busqueda
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
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Book.Code, Me.Book.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.Book.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Book.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Book.Code, Me.Book.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Book.Code)
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
    ''' Método que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyBook.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        Name1 = String.Empty
        TypeBook = Nothing
        'OfficialBook = Nothing
        OfficialCurrencyId = Nothing
        Description = String.Empty
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyBook.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        With Book
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name1
            .TypeBook = TypeBook
            .OfficialBook = OfficialBook
            .OfficialCurrencyId = If(.OfficialBook, 1, OfficialCurrencyId)
            .Description = Description

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MAccountLevel(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Método que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        
        Using Model As New MBook(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetBookByCode(INDbtnCode.Text.Trim)
            INDlyBook.BeginUpdate()
            Book = resultOperation.ObjectEmbbeded
            If Not Book Is Nothing Then
                If Book.Id > 0 Then
                    Using ModelRecord As New MAccountLevel(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Book.Id))
                        With Book
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            Name1 = .Name
                            TypeBook = .TypeBook
                            OfficialBook = .OfficialBook
                            Description = .Description
                            OfficialCurrencyId = .OfficialCurrencyId
                            Status = .Status
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Book.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = Book.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(Book.Id)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        If (OfficialBook) Then
                            INDsleddlCurrency.Enabled = False
                        ElseIf Book.hasJournalVouchers Then
                            '' se valida que exista al menos un movimiento con ese libro y moneda para inhabilitar el campo
                            INDsleddlCurrency.Enabled = False
                        Else
                            INDsleddlCurrency.Enabled = True
                        End If
                    End Using
                Else
                    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    'Me.CodeConceptsNotes = String.Empty
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Me.NewBook()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        Deshacer()
                        INDbtnCode.Focus()
                    End If
                End If
            Else
                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                'Me.CodeConceptsNotes = String.Empty
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewBook()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDbtnCode.Focus()
                End If
            End If
        End Using
        INDlyBook.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Sub NewBook()
        If _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El formulario no tiene parametrizado la secuencia numerica."
            Exit Sub
        End If
        Me.Book = New LegalBook() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequense.GeneralLedgerSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.GeneralLedgerSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
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
                        Using model As New MRetentionConcept(CStr(Me.Tag))
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
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If OfficialBook = True AndAlso Status = CBool(eActionsStatusRecords.Inactive) Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede inactivar el registro porque es un libro oficial."
            Status = True
            INDtxtName.Focus()
            Exit Function
        End If
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MBook(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Me.Book.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.Book = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
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

#End Region

#Region "Events"

#Region "EditValueChanged"
    ''' <summary>
    ''' Cambia la moneda seleccionada
    ''' </summary>
    Private Sub INDsleddlCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleddlCurrency.EditValueChanged
        If INDsleddlCurrency.EditValue IsNot Nothing Then
            Presenter.InitializeCurrency()
        Else
            OfficialCurrencyId = Nothing
        End If
    End Sub
#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Muestra las monedas
    ''' </summary>
    Private Sub INDsleddlCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleddlCurrency.QueryPopUp
        If INDsleddlCurrency.Properties.DataSource Is Nothing Then
            Presenter.InitializeCurrency()
        End If
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBook, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PBook(Me)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
        InitializeTuples()
        SearchMode = False
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        Presenter = Nothing
        Book = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        ListTypeBook = Nothing
    End Sub


#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBook_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar una tecla, sirve para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Me.NewBook()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmBook_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Se consulta si en la tabla LegalBook ya existe un libro oficial,
        'si existe el campo de libro oficial se deja en false
        Using model As New MBook(Tag)
            Dim result As ActionResult(Of LegalBook) = Await model.ValidateOfficialBook()
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
            If result.ObjectEmbbeded IsNot Nothing Then
                OfficialBook = False
            Else
                OfficialBook = True
            End If
        End Using
        INDbtnCode.Focus()
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
    ''' Evento que se dispara al presionar el botón de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton buscar de la barra y abra el frm de búsqueda
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que deshace los cambios.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el botón de eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Evento que guarda la entidad creada
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.GeneralLedgerSequenceDetail IsNot Nothing Then
            If Me._sequense.GeneralLedgerSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.GeneralLedgerSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub


#End Region

End Class