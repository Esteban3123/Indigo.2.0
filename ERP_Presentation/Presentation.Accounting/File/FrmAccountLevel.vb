'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 20-01-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Accounting.MVP
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
#End Region

''' <summary>
''' clase que contiene la interfaz grafica del formulario de niveles de cuenta
''' </summary>
''' <remarks></remarks>
Public Class FrmAccountLevel
    Implements IAccountLevel

#Region "Consts"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"
#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula la entidad del tipo de documento
    ''' </summary>
    Private _AccountLevel As New MainAccountLevels
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PAccountLevel
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordGeneralLedger
    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private _indigoSession As SessionValues

    Dim ModeSearch As Boolean

#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad creada para habilitar o deshabilitar los controles
    ''' </summary>
    ''' <value>true or false</value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAccountLevel.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDrgbAux.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece si aplica o no nivel auxiliar
    ''' </summary>
    ''' <value> true si maneja auxiliar</value>
    ''' <returns>true o false</returns>
    ''' <remarks></remarks>
    Public Property AuxAccountLevel As Boolean Implements IAccountLevel.AuxAccountLevel
        Get
            Return CBool(INDrgbAux.EditValue)
        End Get
        Set(value As Boolean)
            INDrgbAux.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o estabelce el código del nivel
    ''' </summary>
    ''' <value>el codigo del nivel</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeAccountLevel As String Implements IAccountLevel.CodeAccountLevel
        Get
            Return INDbteCode.Text
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del nivel
    ''' </summary>
    ''' <value>el nombre del nivel</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameAccountLevel As String Implements IAccountLevel.NameAccountLevel
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Obtiene o asigna el estado del registro en la tabla
    ''' </summary>
    ''' <value>Estado del registro en la tabla</value>
    ''' <returns>El estado del registro en la tabla</returns>
    Public Property StateAccountLevel As Boolean Implements IAccountLevel.StateAccountLevel
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

#Region "Crud Operations"

    ''' <summary>
    ''' metodo diseñado para eliminar los niveles de contabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        Dim actionResult As ActionMessageResult(Of MainAccountLevels)
        If _AccountLevel IsNot Nothing Then
            If _AccountLevel.Id > 0 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MAccountLevel(Me.Tag)
                        AsyncLoader(True)
                        actionResult = Await Model.DeleteAccountLevel(_AccountLevel)
                        If actionResult.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Await Me.DeleteDocumentIndexed()
                        Else
                            For Each action As MessageResult In actionResult.MessageResult
                                If action.CodeMessage = "c-0000" Then
                                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ErrorDependence")
                                    AsyncLoader(False)
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                                End If
                            Next
                        End If
                        AsyncLoader(False)
                    End Using
                Else
                    Exit Sub
                End If
            End If
        End If
        Deshacer()
    End Sub

    ''' <summary>
    ''' metodo diseñado para guardar los niveles de contabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
              If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Using model As New MAccountLevel(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveAccountLevel(_AccountLevel)
            AsyncLoader(False)
            If Result = True Then
                If _AccountLevel.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf _AccountLevel.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or _AccountLevel.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Deshacer()
            Else
                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
            End If
        End Using
    End Sub

#End Region

#Region "Methods"

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
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With {.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _AccountLevel.Code, _AccountLevel.Name), .CreationDate = dateServer, .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName, .JournalVoucher = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & _AccountLevel.Code & "#$", .IdForm = Me.Tag, .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _AccountLevel.Code), .Update = dateServer, .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), _AccountLevel.Code, _AccountLevel.Name)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), _AccountLevel.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UnblockeRecord()
        If Me._record IsNot Nothing AndAlso Me._record.Id > 0 Then
            Using model As New MAccountLevel(Me.Tag.ToString())
                Await model.DeleteBlockRecord(Me._record)
                Me._record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Función para validar los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As Boolean
        If (INDbteCode.Text = String.Empty) Then
            Return False
        ElseIf (INDtxtName.Text = "") Then
            Return False
        ElseIf (INDrgbAux.EditValue Is Nothing) Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Método para asignar los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        'With _AccountLevel
        '    .Code = CodeAccountLevel
        '    .Name = NameAccountLevel
        '    .Aux = AuxAccountLevel
        '    .Status = StateAccountLevel
        'End With
    End Sub

    ''' <summary>
    ''' Método diseñado para abrir el formulario de busquedas 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 30}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 330}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountLevel
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
            ModeSearch = True
        End With
    End Sub

    ''' <summary>
    ''' Método para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Método para iniciar la busqueda de niveles de cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Buscar() Implements IcrudBase.Buscar
        
    End Sub

    ''' <summary>
    ''' Método para deshacer operaciones del form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        If ModeSearch = False Then
            CleanControls()
        End If
    End Sub

    ''' <summary>
    ''' Método que implementa la interfaz IcrudBase para establecer la lógica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Interfaz sobre el botón nuevo, llama al método para limpiar los controles del formulario
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Método para limpiar los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        _AccountLevel = Nothing
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDbteCode.Focus()
        INDrgbAux.EditValue = -1
        UnblockeRecord()
    End Sub

    ''' <summary>
    ''' Método que carga los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'AsyncLoader(True)
        'If (INDbteCode.Text.Trim <> String.Empty) Then
        '    If Me.BarraBotones.PermiteConsultar = False Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '        Exit Function
        '    End If
        '    Using Model As New MAccountLevel(Me.Tag.ToString())
        '        _AccountLevel = Await Model.GetAccountLevelByCode(INDbteCode.Text, True)
        '        If _AccountLevel IsNot Nothing And _AccountLevel.Id > 0 Then
        '            Dim result = Await Model.GetBlockRecord(Tag, _AccountLevel.Id)
        '            With _AccountLevel
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                INDtxtName.Text = .Name
        '                INDrgbAux.EditValue = .Aux
        '                StateAccountLevel = .Status

        '            End With
        '            Me.GetDocumentIndexed(Me.Tag & "_" & _AccountLevel.Code)
        '            If result.Id = 0 Then
        '                BarraBotones.SetDocuments(_AccountLevel.Id)
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                _record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = _AccountLevel.Id}
        '                Dim operation = Await Model.SaveBlockRecord(_record)
        '                _record = operation.ObjectEmbbeded
        '            Else
        '                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '                BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning)
        '            End If
        '        Else
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            _AccountLevel = New AccountLevel
        '            Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        '        End If
        '        Me.AsyncLoader(False)
        '    End Using
        '    ActionsOnControls = True
        '    INDtxtName.Focus()
        'End If
    End Function
#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountLevel_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        UnblockeRecord()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountLevel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LoadStatus()
        Me._doc = Nothing
        Me._indigoSession = SessionValues.Instance
        Me._presenter = New PAccountLevel(Me)
        Funct = AddressOf GenerateDoc
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = True
        Deshacer()

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar la tecla enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _AccountLevel = Nothing
        _presenter = Nothing
        _record = Nothing
        _indigoSession = Nothing
        ModeSearch = Nothing
    End Sub


    ''' <summary>
    ''' Aquí se hace la lógica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If _AccountLevel IsNot Nothing AndAlso _AccountLevel.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.Buscar()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.Buscar()
        End If
        Me.IdEntity =  String.Empty
    End Sub

#End Region

#Region "Toolbars Event"
    ''' <summary>
    ''' Evento load de la barra de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton buscar de la barra y abra el frm de búsqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento que limpia los campos del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        ModeSearch = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que guarda el nivel de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que limpia los controles del frm
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Evento que actualiza el nivel de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que elimina el nivel de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

End Class