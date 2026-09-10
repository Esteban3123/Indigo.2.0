'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Jose Luis Rojas 
' Created          : 04-07-2011
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Common.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Contiene el comportamiento de la vista del frontal discapacidades
''' </summary>
Public Class FrmDisability
    Implements IDisability

#Region "Globals & Properties"

    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Propiedad que contiene el estado de lo controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDisability.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            If value = True Then
                INDtxtDescription.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Entidad
    ''' </summary>
    Dim Disability As Disability

    ''' <summary>
    ''' Variable para acceder al modelo 
    ''' </summary>
    Dim Model As MDisability

    ''' <summary>
    ''' Variable para acceder al presentador
    ''' </summary>
    Dim Presenter As PDisability

    ''' <summary>
    ''' Variable de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Varaible para controlar el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As BlockRecord

    ''' <summary>
    ''' Propiedad que establece el codigo de la discapacidad
    ''' </summary>
    Public Property DisabilityCode As String Implements IDisability.DisabilityCode
        Get
            Return INDbteCode.Text
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el nombre de la discapacidad
    ''' </summary>
    Public Property DisabilityDescription As String Implements IDisability.DisabilityDescription
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el estado de la discapacidad
    ''' </summary>
    Public Property Status As Boolean Implements IDisability.Status
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property


    Private ModoBusqueda As Boolean
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Disability = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDisability_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MDisability(Me.Tag)
        Me.Indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc

        PathFunctionalDefinitions = String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloCommon.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        LoadStatus()
        Presenter = New PDisability(Me)
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                    Exit Sub
                End If
                Await LoadControls()
                If INDbteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDbteCode.Enabled = False
            End If
            INDtxtDescription.Focus()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para capturar el cierre del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDepartaments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Disability IsNot Nothing AndAlso Disability.Id > 0 Then

            If Not (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If

        If record IsNot Nothing AndAlso record.Id > 0 Then
            If record.CodUser = Indigo.UserIndigo Then
                DeleteBlockedRecord()
            End If
        End If
        INDbteCode.Text = Me.IdEntity.Trim()
        Await Me.LoadControls()
        Me.IdEntity =  String.Empty
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
    End Sub

#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el formulario de busquedas
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Disability
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
            ModoBusqueda = True
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
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
    ''' Ejecuta el metodo de abrirbusqueda
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        If ModoBusqueda = False Then
            CleanControls()
        End If
    End Sub

    ''' <summary>
    ''' Elimina el discapacidad seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        Dim actionResult As ActionMessageResult(Of Disability)
        If Disability IsNot Nothing Then
            If Disability.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MDisability(Me.Tag)
                        AsyncLoader(True)
                        actionResult = Await Model.DeleteDisabilityAsync(Disability)
                        If actionResult.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Await Me.DeleteDocumentIndexed()
                            ModoBusqueda = False
                            AsyncLoader(False)
                            Deshacer()
                        Else
                            For Each action As MessageResult In actionResult.MessageResult
                                If action.CodeMessage = "c-0000" Then
                                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesErrorDependencia)
                                    AsyncLoader(False)
                                Else
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador)
                                End If
                            Next
                        End If

                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaDiscapacidad, Eform.Discapacidad)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaDiscapacidad, Eform.Discapacidad)
        End If
    End Sub

    ''' <summary>
    ''' Guarda el discapacidad
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Using Model As New MDisability(Me.Tag)
            AsyncLoader(True)
            If Await Model.SaveDisabilityAsync(Disability) = True Then
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                If Disability.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf Disability.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
            AsyncLoader(False)
            ModoBusqueda = False
            Deshacer()
        End Using
    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Muestre y escribe el mensaje de retorno por las operaciones
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

    ''' <summary>
    ''' Limpia el formulario para iniciar 
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmDisabilityMetaData, Eform.InfoMetaData), Me.Disability.Code, Me.Disability.Description), _
                .CreationDate = dateServer, .CreationUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me.Disability.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmDisabilityMetaDataTitle, Eform.InfoMetaData), Me.Disability.Code), _
                .Update = dateServer, .UpdateUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmDisabilityMetaData, Eform.InfoMetaData), Me.Disability.Code, Me.Disability.Description)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmDisabilityMetaDataTitle, Eform.InfoMetaData), Me.Disability.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.Indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        Status = True
        INDbteCode.Text = String.Empty
        INDtxtDescription.Text = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        Disability = New Disability
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        INDbteCode.Focus()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtDescription.Text = String.Empty Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Disability
            .Code = DisabilityCode
            .Description = DisabilityDescription
            .State = Status
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        Status = True
        ActionsOnControls = True
        Using Model As New MDisability(Me.Tag)
            Disability = Await Model.GetDisabilityAsync(INDbteCode.Text)
        End Using
        AsyncLoader(True)
        If Not Disability Is Nothing Then
            If Disability.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, Disability.Id)
                With Disability
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Disability.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Disability.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Disability.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Disability.ModificationDate)
                    DisabilityCode = .Code
                    DisabilityDescription = .Description
                    Status = .State
                End With

                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Disability.Code)

                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(Disability.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.Indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.Indigo.UserIndigo, .IdRecord = Disability.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            Else
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            Disability = New Disability
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        AsyncLoader(False)
    End Function

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    ''' esta función garantiza que cuando el formulario se activa o se muestra, el control INDbteCode
    ''' recibirá automáticamente el enfoque si está habilitado, lo que mejora la usabilidad y facilita
    ''' al usuario iniciar la interacción con ese control.
    ''' </summary>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
        ModoBusqueda = False
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
        CleanControls()
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

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCtrDisability.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyCtrDisability.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrDisability.ShowCustomization
        'Try
        '    'Ejecuatamos la consulta
        '    Using model As New MDisability(Me.Tag)
        '        Dim dsFields As DataSet = Await model.GetFieldsNULLAsync()
        '        If dsFields IsNot Nothing Then
        '            dtFieldsCustomizables = dsFields.Tables(0)
        '            For j As Integer = 0 To INDlyCtrDisability.Items.Count - 1
        '                INDlyCtrDisability.Items.Item(j).AllowHide = False
        '                For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
        '                    If Object.Equals(INDlyCtrDisability.Items.Item(j).Tag, Nothing) = False Then
        '                        If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtrDisability.Items.Item(j).Tag.ToString.Trim Then
        '                            INDlyCtrDisability.Items.Item(j).AllowHide = True
        '                        End If
        '                    End If
        '                Next
        '            Next
        '        End If
        '    End Using
        'Catch ex As Exception
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        '    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        'End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrDisability.HideCustomization
        ''Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        'If INDlyCtrDisability.IsModified = True Then
        '    Try
        '        If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
        '            INDlyCtrDisability.SaveLayoutToXml(PathFunctionalDefinitions)
        '        Else
        '            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        '        End If
        '    Catch ex As Exception
        '        IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        '    End Try
        'End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyCtrDisability.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region


End Class