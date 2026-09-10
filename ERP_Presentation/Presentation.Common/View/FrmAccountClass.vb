'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 16-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Formulario de clases de cuentas
''' </summary>
''' <remarks></remarks>
Public Class FrmAccountClass
    Implements IAccountClass, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Variables"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' The _sequence
    ''' </summary>
    Dim _sequence As GeneralLedgerSequence

    ''' <summary>
    ''' The _id current sequence
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' The list nature account
    ''' </summary>
    Dim ListNatureAccount As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' The list type account class
    ''' </summary>
    Dim ListTypeAccountClass As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IAccountClass.MyTag
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
    Public Property Sequense As GeneralLedgerSequence Implements IAccountClass.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As GeneralLedgerSequenceDetail In Me._sequence.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula la entidad de clase cuenta
    ''' </summary>
    Private accountClass As New MainAccountClasses
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PAccountClass
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordGeneralLedger

    ''' <summary>
    ''' The _search mode
    ''' </summary>
    'Dim _searchMode As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
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
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAccountClass.ActionsOnControls
        Set(value As Boolean)
            INDlycgRoot.BeginUpdate()

            Me.INDbteCode.Enabled = Not value
            Me.INDtxtName.Enabled = value
            Me.INDgleNature.Enabled = value
            Me.INDgleType.Enabled = value
            Me.INDrgbPatrimony.Enabled = value

            INDlycgRoot.EndUpdate()

            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el código de clase cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IAccountClass.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre de clase cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameAccountClass As String Implements IAccountClass.NameAccountClass
        Get
            Return Me.INDtxtName.Text.Trim()
        End Get
        Set(value As String)
            Me.INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la naturaleza de clase cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NatureAccountClass As String Implements IAccountClass.NatureAccountClass
        Get
            Return Me.INDgleNature.EditValue
        End Get
        Set(value As String)
            Me.INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el patrimonio de clase cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PatrimonyAccountClass As Boolean Implements IAccountClass.PatrimonyAccountClass
        Get
            Return Me.INDrgbPatrimony.EditValue
        End Get
        Set(value As Boolean)
            Me.INDrgbPatrimony.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tipo de clase cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeAccountClass As String Implements IAccountClass.TypeAccountClass
        Get
            Return Me.INDgleType.EditValue
        End Get
        Set(value As String)
            Me.INDgleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el estado de l registro en la tabla
    ''' </summary>
    ''' <value>Estado del registro en la tabla</value>
    ''' <returns>El estado del registro en la tabla</returns>
    Public Property StateAccountClass As Boolean Implements IAccountClass.StateAccountClass
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

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Se realiza la busqueda de clase cuenta
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' Se ejecuta la eliminación de clase cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        'Verificacion si Me.accountClass que no sea nulo, y que Id sea mayor que 0
        If Me.accountClass IsNot Nothing AndAlso Me.accountClass.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MAccountClass(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteAccountClass(Me.accountClass)
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
    ''' Se ejecuta el guardado o actualizado de clase cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()

        'Este bloque de código intenta guardar una instancia de una clase llamada MAccountClass utilizando una operación asincrónica.
        Try
            Using Model As New MAccountClass(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of MainAccountClasses) = Await Model.SaveAccountClass(Me.accountClass, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If accountClass.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.accountClass = result.ObjectEmbbeded
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
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Esta función, llamada GenerateDoc(), parece generar y retornar un objeto del tipo IndexedDocument2 que se 
    ''' utiliza para representar un documento indexado. Un documento indexado es un documento que se asocia con metadatos y 
    ''' puede ser organizado y buscado en un sistema.
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountClass.Code, Me.accountClass.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.accountClass.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountClass.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountClass.Code, Me.accountClass.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountClass.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Esta función, llamada OpenSearch(), parece estar relacionada con la apertura de 
    ''' una ventana de búsqueda en una interfaz de usuario. La función implementa
    ''' la interfaz IcrudBase.OpenSearch, lo que sugiere que es parte de una estructura 
    ''' de implementación de operaciones de creación, lectura, actualización y eliminación (CRUD) en una aplicación.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.8}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountClass
            .ValorSolicitado = "Code"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Esta función, llamada ReturnValue, parece manejar la respuesta
    ''' o el valor seleccionado desde un formulario de búsqueda
    ''' </summary>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlycgRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Me.accountClass = Nothing
        Me.INDbteCode.Text = String.Empty
        Me.INDtxtName.Text = String.Empty
        Me.INDgleNature.EditValue = Nothing
        Me.INDgleType.EditValue = Nothing
        Me.INDrgbPatrimony.EditValue = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycgRoot.EndUpdate()

        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Lo que hace esta función es verificar si existe un registro bloqueado (_record) y si el usuario actual
    ''' (Me.indigo.UserIndigo) es el mismo que bloqueó el registro. Si ambas condiciones son verdaderas, utiliza
    ''' la clase MAccountClass para eliminar el registro bloqueado mediante el método DeleteBlockRecord. Luego, 
    ''' establece _record en Nothing, lo que indica que no hay registro bloqueado activo.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MAccountClass(Me.Tag)
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Esta función, llamada AssigningValues, se encarga de asignar valores desde diversos controles de la interfaz 
    ''' de usuario a propiedades de un objeto accountClass. Esto es útil para recopilar la información ingresada por 
    ''' el usuario en la interfaz y asignarla a las propiedades relevantes del objeto.
    ''' </summary>
    Private Sub AssigningValues()
        With Me.accountClass
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Me.Code
            .Name = Me.NameAccountClass
            .Nature = Me.NatureAccountClass
            .Type = Me.TypeAccountClass
            .Patrimony = Me.PatrimonyAccountClass
        End With
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        'No se implementa
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewAccountClass()
        End If
    End Sub

    ''' <summary>
    ''' Esta función, llamada NewAccountClass, se encarga de inicializar la creación de un nuevo registro de la clase de cuenta 
    ''' (objeto accountClass) en una interfaz de usuario. Parece ser parte del proceso de preparación para ingresar datos para 
    ''' un nuevo registro. Dado que la función es asincrónica (utiliza Async), es posible que esté realizando llamadas a la base 
    ''' de datos u otras operaciones que requieran tiempo.
    ''' </summary>
    Private Async Function NewAccountClass() As Task
        accountClass = New MainAccountClasses() With {.Status = True}
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
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MAccountClass(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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

    ''' <summary>
    ''' Esta función, llamada GenerateNatureAccount, se encarga de generar y configurar las opciones para la naturaleza de la cuenta en un 
    ''' control desplegable (generalmente un ComboBox) en una interfaz de usuario. Esta función parece estar diseñada para preparar las 
    ''' opciones disponibles para que el usuario seleccione la naturaleza de una cuenta financiera, como "Débito" o "Crédito".
    ''' </summary>
    Private Sub GenerateNatureAccount()
        ListNatureAccount = New List(Of Tuple(Of Integer, String))
        ListNatureAccount.Add(New Tuple(Of Integer, String)(1, "Débito"))
        ListNatureAccount.Add(New Tuple(Of Integer, String)(2, "Crédito"))
        INDgleNature.Properties.DataSource = ListNatureAccount

    End Sub

    ''' <summary>
    ''' Esta función, llamada UpdateState, se encarga de actualizar el estado (activo o inactivo) de una cuenta financiera 
    ''' (objeto accountClass) en una interfaz de usuario. La función parece permitir al usuario activar o desactivar 
    ''' una cuenta específica.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.accountClass.Code) Then
            Try
                Using Model As New MAccountClass(Me.Tag)
                    AsyncLoader(True)
                    Dim stateCard As Boolean = Not Me.accountClass.Status
                    Dim Result = Await Model.UpdateStateCard(Me.accountClass.Code, stateCard)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me.accountClass = Result.ObjectEmbbeded
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
    ''' Esta función, llamada GeneretaTypeAccountClass, se encarga de generar y 
    ''' configurar las opciones para el tipo de cuenta en un control desplegable (generalmente un ComboBox)
    ''' en una interfaz de usuario. La función parece estar diseñada para preparar las opciones disponibles 
    ''' para que el usuario seleccione el tipo de una cuenta financiera.
    ''' </summary>
    Private Sub GeneretaTypeAccountClass()
        ListTypeAccountClass = New List(Of Tuple(Of Integer, String))
        ListTypeAccountClass.Add(New Tuple(Of Integer, String)(1, "Balance"))
        ListTypeAccountClass.Add(New Tuple(Of Integer, String)(2, "Resultado"))
        ListTypeAccountClass.Add(New Tuple(Of Integer, String)(3, "Orden"))
        ListTypeAccountClass.Add(New Tuple(Of Integer, String)(4, "Presupuesto"))
        INDgleType.Properties.DataSource = ListTypeAccountClass
    End Sub

    ''' <summary>
    ''' Esta función se ejecuta cuando se carga una entidad (probablemente en una interfaz de usuario) 
    ''' y maneja varias lógicas dependiendo de la situación. Esta función parece estar diseñada para cargar 
    ''' y manejar la edición de una cuenta financiera u otra entidad similar.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.accountClass IsNot Nothing AndAlso Me.accountClass.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta función, llamada LoadControls, carga y muestra los controles y datos relacionados con una entidad, 
    ''' posiblemente una cuenta financiera, en una interfaz de usuario. La función maneja diferentes casos según 
    ''' las circunstancias y la disponibilidad de permisos.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MAccountClass(CStr(Me.Tag))
                    AsyncLoader(True)
                    accountClass = Await Model.GetAccountClass(INDbteCode.Text.Trim, False)
                    INDlycgRoot.BeginUpdate()
                    If accountClass IsNot Nothing AndAlso accountClass.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        _record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(accountClass.Id))
                        With accountClass
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Code = .Code
                            NameAccountClass = .Name
                            PatrimonyAccountClass = .Patrimony
                            NatureAccountClass = .Nature
                            TypeAccountClass = .Type
                            StateAccountClass = .Status
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.accountClass.Code)
                        If _record.Id = 0 Then
                            _record = (Await Model.SaveBlockRecord(
                                New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = accountClass.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(accountClass.Id, Me.Tag.ToString(), Nothing, GetType(MainAccountClasses).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewAccountClass()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycgRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function
#End Region

#Region "Handlers"

    ''' <summary>
    ''' Esta función se ejecuta cuando el formulario (o clase derivada de Frm) 
    ''' está siendo destruido o eliminado de la memoria. La función se encarga de 
    ''' liberar recursos y realizar tareas de limpieza para prevenir problemas de 
    ''' memoria o filtración de recursos.
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListNatureAccount = Nothing
        ListTypeAccountClass = Nothing
        accountClass = Nothing
        _presenter = Nothing
        _record = Nothing
    End Sub


    ''' <summary>
    ''' Esta función se ejecuta cuando el formulario (o clase derivada de Frm) se carga, lo que 
    ''' significa que se está iniciando y mostrando al usuario. La función realiza varias tareas 
    ''' de configuración y preparación para garantizar que el formulario se muestre correctamente 
    ''' y que los recursos y datos necesarios estén disponibles.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PAccountClass(Me)
        _presenter.GetSequence()

        LoadStatus()
        Deshacer()
        GenerateNatureAccount()
        GeneretaTypeAccountClass()
    End Sub

    ''' <summary>
    ''' Esta función se ejecuta cuando el formulario (o una clase derivada de Frm) 
    ''' está a punto de cerrarse, ya sea porque el usuario ha decidido cerrarlo o 
    ''' debido a alguna otra acción que provoque el cierre del formulario. La función 
    ''' realiza una tarea específica antes de permitir que el formulario se cierre.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountClass_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Esta función se ejecuta cuando el formulario (o una clase derivada de Frm) 
    ''' se activa o se muestra al usuario. La función realiza una acción específica 
    ''' cuando el formulario se convierte en la ventana activa.
    ''' </summary>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento KeyDown del control INDbteCode, que es un 
    ''' campo de entrada de texto en el formulario. El evento KeyDown se activa 
    ''' cuando el usuario presiona una tecla mientras el foco está en el control 
    ''' INDbteCode. La función realiza varias acciones basadas en las teclas 
    ''' presionadas por el usuario.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewAccountClass()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Esta función maneja el evento ClickNuevo del control BarraBotones, 
    ''' que parece ser un conjunto de botones o una barra de herramientas 
    ''' en el formulario. El evento ClickNuevo se activa cuando el usuario 
    ''' hace clic en el botón de "Nuevo" en la barra de herramientas.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento Load del control BarraBotones, 
    ''' que parece ser una barra de herramientas en el formulario. 
    ''' El evento Load se activa cuando se carga el control en la interfaz de usuario.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Esta función maneja dos eventos: ClickBuscar del control BarraBotones y ButtonClick
    ''' del control INDbteCode (que parece ser un campo de entrada de texto con un botón asociado).
    ''' Estos eventos se activan cuando el usuario hace clic en el botón de "Buscar" en la barra de 
    ''' herramientas o en el botón asociado al campo de entrada de texto INDbteCode.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento ClickDeshacer del control BarraBotones, que parece ser una barra
    ''' de herramientas en el formulario. El evento ClickDeshacer se activa cuando el usuario hace clic 
    ''' en el botón de "Deshacer" en la barra de herramientas.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento ClickEliminar del control BarraBotones, que parece ser una barra 
    ''' de herramientas en el formulario. El evento ClickEliminar se activa cuando el usuario hace clic 
    ''' en el botón de "Eliminar" en la barra de herramientas.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento ClickGuardar del control BarraBotones, que parece ser una barra 
    ''' de herramientas en el formulario. El evento ClickGuardar se activa cuando el usuario hace clic
    ''' en el botón de "Guardar" en la barra de herramientas.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento ClickActualizar del control BarraBotones, que aparentemente es una barra
    ''' de herramientas en el formulario. El evento ClickActualizar se activa cuando el usuario hace clic en 
    ''' el botón de "Actualizar" en la barra de herramientas.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento Click_ActiveInactive del control BarraBotones, que parece ser una barra
    ''' de herramientas en el formulario. El evento Click_ActiveInactive se activa cuando el usuario hace clic
    ''' en un botón destinado a activar o desactivar un elemento, como por ejemplo, cambiar el estado activo/inactivo
    ''' de algún registro en el formulario.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    '''Esta función maneja el evento ChangueOperatingUnit del control BarraBotones, que aparentemente se refiere al cambio
    '''de la unidad operativa en el formulario. El evento se activa cuando el usuario cambia la unidad operativa seleccionada
    '''en la barra de herramientas.
    ''' </summary>
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

#End Region


End Class