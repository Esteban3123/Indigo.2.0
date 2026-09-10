'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
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
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Presentation.Common.MVP
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario centros de estudio
''' </summary>
Public Class FrmTimeUnit
    Implements ITimeUnit

#Region "Variables"
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    ''' 
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Varaible que contiene la entidad de los tipos de vinculacion
    ''' </summary> 
    Dim TimeUnit As TimeUnit

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MTimeUnit

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PTimeUnit
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para manejar el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As BlockRecord

    Dim ModoBusqueda As Boolean
#End Region

#Region "Properties And Load"
    ''' <summary>
    ''' Propiedad que establece la accion de los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITimeUnit.ActionsOnControls
        Set(value As Boolean)
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value

            If value = False Then
                INDBteCode.Focus()
            Else
                INDTxtName.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el codigo del tipo de vinculacion
    ''' </summary>
    Public Property CodeTU As String Implements ITimeUnit.CodeTU
        Get
            Return INDBteCode.Text
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre del tipo de vinculacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Property NameTU As String Implements ITimeUnit.NameTU
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el estado del tipo de vinculacion
    ''' </summary>
    Public Property StatusTU As Boolean Implements ITimeUnit.StatusTU
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        TimeUnit = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        record = Nothing
        ModoBusqueda = Nothing
    End Sub



    ''' <summary>
    ''' Metodo que carga el load del formulario 
    ''' </summary>
    Private Sub FrmTimeUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollTimeUnit.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PTimeUnit(Me)
        Deshacer()
        LoadStatus()
    End Sub
#End Region

#Region "Metodos Funciones"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmTimeUnitMetaData, Eform.InfoMetaData), Me.TimeUnit.Code, Me.TimeUnit.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me.TimeUnit.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmTimeUnitMetaDataTitle, Eform.InfoMetaData), Me.TimeUnit.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmTimeUnitMetaData, Eform.InfoMetaData), Me.TimeUnit.Code, Me.TimeUnit.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmTimeUnitMetaDataTitle, Eform.InfoMetaData), Me.TimeUnit.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MThirdParty(MThirdParty.TAG)
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
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
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With TimeUnit
            .Code = CodeTU
            .Name = NameTU
            .State = StatusTU
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        StatusTU = True
        AsyncLoader(True)
        Using Model As New MTimeUnit(Me.Tag)
            TimeUnit = Await Model.GetTimeUnitAsync(INDBteCode.Text)

            If Not TimeUnit Is Nothing Then
                If TimeUnit.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(Me.Tag, TimeUnit.Id)
                    With TimeUnit
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), TimeUnit.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), TimeUnit.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), TimeUnit.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), TimeUnit.ModificationDate)
                        CodeTU = .Code
                        NameTU = .Name
                        StatusTU = .State
                    End With

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.TimeUnit.Code)

                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(Me.TimeUnit.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Me.TimeUnit.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else
                TimeUnit = New TimeUnit
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
            AsyncLoader(False)
        End Using
        ActionsOnControls = True
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDBteCode.Text = String.Empty
        INDTxtName.Text = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        TimeUnit = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDTxtName.Text = String.Empty Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar el centro de estudio en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteDepCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If Not String.IsNullOrEmpty(INDBteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Await LoadControls()
                If INDBteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
                'INDBteCode.Enabled = False
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub
#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Centros de estudio.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de Centros de estudio.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de Centros de estudio.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        Dim actionResult As ActionMessageResult(Of TimeUnit)
        If TimeUnit IsNot Nothing Then
            If TimeUnit.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MTimeUnit(Me.Tag)
                        AsyncLoader(True)
                        actionResult = Await Model.DeleteTimeUnitAsync(TimeUnit)
                        If actionResult.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Await Me.DeleteDocumentIndexed()

                        Else
                            For Each action As MessageResult In actionResult.MessageResult
                                If action.CodeMessage = "c-0000" Then
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                                    AsyncLoader(False)
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                                End If
                            Next
                        End If
                        AsyncLoader(False)
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaUnidaddeTiempo, Eform.UnidaddeTiempo)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaUnidaddeTiempo, Eform.UnidaddeTiempo)
        End If
        ModoBusqueda = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de Profesiones.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Using Model As New MTimeUnit(Me.Tag)
            AsyncLoader(True)
            Dim result = Await Model.SaveTimeUnitAsync(TimeUnit)
            AsyncLoader(False)
            If result = True Then
                If TimeUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf TimeUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or TimeUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                ModoBusqueda = False
                Deshacer()
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.TimeUnit
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
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Profesiones.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        If ModoBusqueda = False Then
            CleanControls()
        End If
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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
#End Region

#Region "bar buttons and events"
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

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmTimeUnit_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.TimeUnit IsNot Nothing AndAlso Me.TimeUnit.Id > 0 Then

            If Not (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If

        If record IsNot Nothing AndAlso record.Id > 0 Then
            If record.CodUser = indigo.UserIndigo Then
                DeleteBlockedRecord()
            End If
        End If
        INDBteCode.Text = Me.IdEntity.Trim()
        Await Me.LoadControls()
        Me.IdEntity =  String.Empty
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
    End Sub

#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyTimeUnit.ShowCustomizationForm()
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
            INDLyTimeUnit.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDLyTimeUnit_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyTimeUnit.ShowCustomization
        'Try
        '    'Ejecuatamos la consulta
        '    Using model As New MTimeUnit(Me.Tag)
        '        Dim dsFields As DataSet = model.GetNullFields()
        '        If dsFields IsNot Nothing Then
        '            dtFieldsCustomizables = dsFields.Tables(0)
        '            For j As Integer = 0 To INDLyTimeUnit.Items.Count - 1
        '                INDLyTimeUnit.Items.Item(j).AllowHide = False
        '                For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
        '                    If Object.Equals(INDLyTimeUnit.Items.Item(j).Tag, Nothing) = False Then
        '                        If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLyTimeUnit.Items.Item(j).Tag.ToString.Trim Then
        '                            INDLyTimeUnit.Items.Item(j).AllowHide = True
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
    Private Sub INDLyTimeUnit_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyTimeUnit.HideCustomization
        ''Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        'If INDLyTimeUnit.IsModified = True Then
        '    Try
        '        If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
        '            INDLyTimeUnit.SaveLayoutToXml(PathFunctionalDefinitions)
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
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLyTimeUnit.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

End Class