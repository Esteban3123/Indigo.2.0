'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 08-07-2013
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
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario Plantilla de contrato
''' </summary>
Public Class FrmRetention
    Implements IRetention

#Region "Variables"
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Varaible que contiene la entidad de la Retencion
    ''' </summary> 
    Dim retention As Retention

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PRetention

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' variable para controlar el bloqueo de registros
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable utilizada para saber si entra por modo busqueda
    ''' </summary>
    Dim ModoBusqueda As Boolean = False

#End Region

#Region "Properties And Load"
    ''' <summary>
    ''' Propiedad que establece la accion de los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRetention.ActionsOnControls
        Set(value As Boolean)
            INDBteCode.Enabled = Not value
            INDTxtYear.Enabled = value
            INDTxtUVTValue.Enabled = value
            INDTxtUVTMin.Enabled = value
            INDTxtUVTMax.Enabled = value
            INDTxtRate.Enabled = value
            INDTxtAdditionalRate.Enabled = value
            If value = False Then
                INDBteCode.Focus()
            Else
                INDTxtYear.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el codigo de la retencion
    ''' </summary>
    Public Property Code As String Implements IRetention.Code
        Get
            Return INDBteCode.Text
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el año de la retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Year As Integer Implements IRetention.Year
        Get
            If INDTxtYear.EditValue Is Nothing Then
                Return 0
            End If
            Return CType(INDTxtYear.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDTxtYear.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la tarifa adicional de la retencion
    ''' </summary>
    Public Property Rate As Decimal Implements IRetention.Rate
        Get
            Return INDTxtRate.EditValue
        End Get
        Set(value As Decimal)
            INDTxtRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el valor de UVT
    ''' </summary>
    Public Property UVTValue As Double Implements IRetention.UVTValue
        Get
            If INDTxtUVTValue.EditValue Is Nothing Then
                Return 0
            End If
            Return CType(INDTxtUVTValue.EditValue, Double)
        End Get
        Set(value As Double)
            INDTxtUVTValue.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el valor de UVT Minimo
    ''' </summary>
    Public Property UVTMin As Double Implements IRetention.UVTMin
        Get
            If INDTxtUVTMin.EditValue Is Nothing Then
                Return 0
            End If
            Return CType(INDTxtUVTMin.EditValue, Double)
        End Get
        Set(value As Double)
            INDTxtUVTMin.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el valor de UVT Máximo
    ''' </summary>
    Public Property UVTMax As Double Implements IRetention.UVTMax
        Get
            If INDTxtUVTMax.EditValue Is Nothing Then
                Return 0
            End If
            Return CType(INDTxtUVTMax.EditValue, Double)
        End Get
        Set(value As Double)
            INDTxtUVTMax.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la tarifa de la retencion
    ''' </summary>
    Public Property AdditionalRate As Decimal Implements IRetention.AdditionalRate
        Get
            Return INDTxtAdditionalRate.EditValue
        End Get
        Set(value As Decimal)
            INDTxtAdditionalRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el consecutivo
    ''' </summary>
    Public Property Consecutive As Integer Implements IRetention.Consecutive
        Get
            Return CType(INDLcConsecutive.Text, Integer)
        End Get
        Set(value As Integer)
            INDLcConsecutive.Text = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que contiene el estado de la retencion
    ''' </summary>
    Public Property StatusRetention As Boolean Implements IRetention.StatusRetention
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo que carga el load del formulario 
    ''' </summary>
    Private Sub FrmContractTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Presenter = New PRetention(Me)
        Me.indigo = SessionValues.Instance

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollRetention.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If

        Presenter.initialize()
        LoadStatus()
        If ModoBusqueda Then
            CleanControls()
        Else
            Deshacer()
        End If
    End Sub
#End Region

#Region "Metodos Funciones Propiedades"

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
        With retention
            .Code = Code
            .AdditionalRate = AdditionalRate
            .Rate = Rate
            .UVTValue = UVTValue
            .UVTMin = UVTMin
            .UVTMax = UVTMax
            .Year = Year
            .State = StatusRetention
        End With
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        StatusRetention = True
        INDBteCode.Enabled = False
        AsyncLoader(True)
        Using Model As New MRetention
            retention = Await Model.GetRetentionAsync(INDBteCode.Text)
            If Not retention Is Nothing Then
                If retention.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(Me.Tag, retention.Id)
                    With retention
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), retention.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), retention.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), retention.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), retention.ModificationDate)
                        Code = .Code
                        Consecutive = .Consecutive
                        AdditionalRate = .AdditionalRate
                        Rate = .Rate
                        UVTValue = .UVTValue
                        UVTMin = .UVTMin
                        UVTMax = .UVTMax
                        Year = .Year
                        StatusRetention = .State
                    End With
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.retention.Code)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(retention.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = retention.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                Else
                    'LogicaBotonActualizar(False)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    INDTxtUVTValue.EditValue = 0
                    INDTxtUVTMin.EditValue = 0
                    INDTxtUVTMax.EditValue = 0
                    INDTxtRate.EditValue = 0
                    INDTxtAdditionalRate.EditValue = 0
                End If
            Else
                retention = New Retention
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                INDTxtUVTValue.EditValue = 0
                INDTxtUVTMin.EditValue = 0
                INDTxtUVTMax.EditValue = 0
                INDTxtRate.EditValue = 0
                INDTxtAdditionalRate.EditValue = 0
            End If
        End Using
        AsyncLoader(False)
        ActionsOnControls = True
    End Function
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDLyRetention.BeginUpdate()
        ActionsOnControls = False
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDBteCode.Text = String.Empty
        INDTxtAdditionalRate.Text = String.Empty
        INDTxtRate.Text = String.Empty
        INDTxtUVTMax.Text = String.Empty
        INDTxtUVTMin.Text = String.Empty
        INDTxtUVTValue.Text = String.Empty
        INDTxtYear.Text = String.Empty
        INDLcConsecutive.Text = String.Empty
        retention = Nothing
        INDLyRetention.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
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
            INDBteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtAdditionalRate.Text = String.Empty Then
            INDTxtAdditionalRate.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtRate.Text = String.Empty Then
            INDTxtRate.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtUVTMax.Text = String.Empty Then
            INDTxtUVTMax.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDTxtUVTMin.Text = String.Empty Then
            INDTxtUVTMin.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtUVTValue.Text = String.Empty Then
            INDTxtUVTValue.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDTxtYear.Text = String.Empty Then
            INDTxtYear.Focus()
            ValidateControls = False
            Exit Function
        End If
    End Function
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' Evento para consultar el Plantilla de contrato en el evento keydown del codigo
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
        End If
    End Sub
    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MRetention
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        retention = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        record = Nothing
        ModoBusqueda = Nothing
    End Sub

    ''' <summary>
    ''' evento que controla cuando se cierre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRetention_Leave(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Plantilla de contratos.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Plantilla de contratos.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Plantilla de contratos.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If retention IsNot Nothing Then
            If retention.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MRetention
                        AsyncLoader(True)
                        Dim result As ActionMessageResult(Of Retention)
                        result = Await Model.DeleteRetentionAsync(retention)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            ModoBusqueda = False
                            AsyncLoader(False)
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
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneRetencion, Eform.Retenciones)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneRetencion, Eform.Retenciones)
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de Plantilla de contratos.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        AssigningValues()
        Using Model As New MRetention
            AsyncLoader(True)
            Dim result = Await Model.SaveRetentionAsync(retention)
            If result = True Then
                If retention.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf retention.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
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
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Retention
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Consecutivo", .FieldName = "Consecutive"}, New ColumnInfo() With {.Caption = "Año", .FieldName = "Year"}, New ColumnInfo() With {.Caption = "Tarifa de Retención", .FieldName = "Rate"}}.ToList
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
    ''' <summary>
    ''' METODO: Item Deshacer del control de Plantilla de contratos.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        Deshacer()
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

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyRetention.ShowCustomizationForm()
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
            INDLyRetention.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDLyRetention_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyRetention.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MRetention
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDLyRetention.Items.Count - 1
                        INDLyRetention.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDLyRetention.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLyRetention.Items.Item(j).Tag.ToString.Trim Then
                                    INDLyRetention.Items.Item(j).AllowHide = True
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
    Private Sub INDLyRetention_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyRetention.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLyRetention.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDLyRetention.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDLyRetention.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

End Class