'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 26-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Manejo del Frontal de Razones de Modificacion de contrato
''' </summary>
Public Class FrmContractModificationReason
    Implements IContractModificationReason

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
    ''' Variable que contiene la representacion de razones de modificacion de contratos
    ''' </summary>
    Dim ContractModificationReason As ContractModificationReason

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PContractModificationReason

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable que sirve para saber si el formulario entra en modo busqueda
    ''' </summary>
    Dim ModoBusqueda As Boolean

    ''' <summary>
    ''' Propiedad que almacena el tipo de novedad elegido
    ''' </summary>
    ''' <returns></returns>
    Public Property NoveltyType As Byte Implements IContractModificationReason.NoveltyType
        Get
            Return INDSleNoveltyType.EditValue
        End Get
        Set(value As Byte)
            INDSleNoveltyType.EditValue = value
        End Set
    End Property

#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ContractModificationReason
            .FormParent = Me
            .ShowSearch()
        End With
        ModoBusqueda = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: item buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If ContractModificationReason IsNot Nothing Then
            If ContractModificationReason.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim result As ActionMessageResult(Of ContractModificationReason)
                    Using Model As New MContractModificationReason(Me.Tag)
                        AsyncLoader(True)
                        result = Await Model.DeleteContractModificationReasonAsync(ContractModificationReason)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            ModoBusqueda = False
                            Deshacer()
                        Else
                            AsyncLoader(False)
                            If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End If
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneRazonMoficacionContrato, Eform.RazonModificacionContrato)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneRazonMoficacionContrato, Eform.RazonModificacionContrato)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        AssigningValues()
        Using Model As New MContractModificationReason(Me.Tag)
            AsyncLoader(True)
            Dim result = Await Model.SaveContractModificationReasonAsync(ContractModificationReason)
            If result = True Then
                If ContractModificationReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf ContractModificationReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                ModoBusqueda = False
                Deshacer()
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                AsyncLoader(False)
            End If
        End Using

    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        'BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
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
    ''' METODO: Item nuevo del frontal de razones de modificacion de contratos
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
                .Content = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaData, Eform.InfoMetaData), Me.ContractModificationReason.Code, Me.ContractModificationReason.Name, Me.ContractModificationReason.Description), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.ContractModificationReason.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaDataTitle, Eform.InfoMetaData), Me.ContractModificationReason.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaData, Eform.InfoMetaData), Me.ContractModificationReason.Code, Me.ContractModificationReason.Name, Me.ContractModificationReason.Description)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmContractModificationReasonMetaDataTitle, Eform.InfoMetaData), Me.ContractModificationReason.Code)
            Return Me._doc
        End If
    End Function


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
        INDlyCtlContractModificationReason.BeginUpdate()
        ActionsOnControls = False
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDbteCode.EditValue = Nothing
        INDtxtName.EditValue = Nothing
        INDmemDescription.EditValue = Nothing
        ContractModificationReason = Nothing
        NoveltyType = CByte(0)
        INDlyCtlContractModificationReason.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IContractModificationReason.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDmemDescription.Enabled = value
            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
            INDSleNoveltyType.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        INDbteCode.Enabled = False
        AsyncLoader(True)
        Using Model As New MContractModificationReason(Me.Tag)
            ContractModificationReason = Await Model.GetContractModificationReasonAsync(INDbteCode.Text.Trim)
            If Not ContractModificationReason Is Nothing Then
                If ContractModificationReason.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(Me.Tag, ContractModificationReason.Id)
                    With ContractModificationReason
                        'LogicaBotonActualizar(True)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), ContractModificationReason.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), ContractModificationReason.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), ContractModificationReason.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), ContractModificationReason.ModificationDate)
                        INDbteCode.EditValue = .Code
                        INDtxtName.EditValue = .Name
                        INDmemDescription.EditValue = .Description
                        NoveltyType = .NoveltyType
                        Me.BarraBotones.StatusRecord = .State
                    End With
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.ContractModificationReason.Code)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(ContractModificationReason.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ContractModificationReason.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else
                ContractModificationReason = New ContractModificationReason
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        End Using
        AsyncLoader(False)
        ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        If INDbteCode.EditValue Is Nothing OrElse INDbteCode.Text.Equals(String.Empty) Then
            Return False
        End If
        If INDtxtName.EditValue Is Nothing OrElse INDtxtName.Text.Equals(String.Empty) Then
            Return False
        End If
        If INDmemDescription.EditValue Is Nothing OrElse INDmemDescription.Text.Equals(String.Empty) Then
            Return False
        End If
        If NoveltyType = CByte(0) Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With ContractModificationReason
            .Code = INDbteCode.Text.Trim
            .Name = INDtxtName.Text.Trim
            .Description = INDmemDescription.Text.Trim
            .State = Me.BarraBotones.StatusRecord
            .NoveltyType = NoveltyType
        End With
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        ContractModificationReason = Nothing
        Presenter = Nothing
        record = Nothing
        ModoBusqueda = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmContractModificationReason_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PContractModificationReason(Me)
        InitializeTuple()
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollContractModificationReason.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        If FormSearchObjects IsNot Nothing Then
            ModoBusqueda = True
        Else
            ModoBusqueda = False
        End If
        LoadStatus()
        Deshacer()

    End Sub

    ''' <summary>
    ''' Metodo que inicializa el data source de los controles que manejan tuplas
    ''' </summary>
    Private Sub InitializeTuple()
        INDSleNoveltyType.Properties.DataSource = {
            New Tuple(Of Byte, String)(1, "Cambio de Sueldo"),
            New Tuple(Of Byte, String)(2, "Cambio de Cargo"),
            New Tuple(Of Byte, String)(3, "Otros")
        }.ToList()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MContractModificationReason(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar el razon de modificacion de contrato en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                LoadControls()
                If INDbteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.ContractModificationReason IsNot Nothing AndAlso Me.ContractModificationReason.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractModificationReason_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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

#Region "Customization"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCtlContractModificationReason.ShowCustomizationForm()
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
            INDlyCtlContractModificationReason.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtlContractModificationReason.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MContractModificationReason(Me.Tag)
                Dim dsFields As DataSet = model.GetFieldsNULL
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCtlContractModificationReason.Items.Count - 1
                        INDlyCtlContractModificationReason.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCtlContractModificationReason.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtlContractModificationReason.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCtlContractModificationReason.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtlContractModificationReason.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCtlContractModificationReason.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCtlContractModificationReason.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyCtlContractModificationReason.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

End Class