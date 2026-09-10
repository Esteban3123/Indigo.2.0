'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 25-09-2013
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
''' Manejo del Frontal de tipo de empleados
''' </summary>
Public Class FrmEmployeeType
    Implements IEmployeeType

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payroll"

#Region "Globals"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

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
    ''' Variable que contiene la representacion del tipo de empleado
    ''' </summary>
    Dim EmployeeType As EmployeeType

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MEmployeeType(Me.Tag)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PEmployeeType

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    Dim searchMode As Boolean = False

    ''' <summary>
    ''' Listado de los registros de Clase de tipos de empleados
    ''' </summary>
    Private _listEmployeeClass As List(Of Tuple(Of String, String))
#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene el codigo de la clase del tipo de empleado
    ''' </summary>
    ''' <returns></returns>
    Public Property EmployeeClass As String Implements IEmployeeType.EmployeeClass
        Get
            Return INDsleEmployeeClass.EditValue
        End Get
        Set(value As String)
            INDsleEmployeeClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el codigo de la clase del tipo de empleado
    ''' </summary>
    ''' <returns></returns>
    Public Property ContributorSubtypeId As Integer Implements IEmployeeType.ContributorSubtypeId
        Get
            Return INDsleContributorSubtype.EditValue
        End Get
        Set(value As Integer)
            INDsleContributorSubtype.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado del subtipo de cotizante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContributorSubtypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployeeType.ContributorSubtypeXpo
        Get
            Return CType(INDsleContributorSubtype.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContributorSubtype.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        searchMode = True
        DeleteBlockedRecord()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.EmployeeType

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
    ''' METODO: item buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        Dim actionResult As ActionMessageResult(Of EmployeeType)
        If EmployeeType IsNot Nothing Then
            If EmployeeType.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    searchMode = False
                    Using Model As New MEmployeeType(Me.Tag)
                        actionResult = Await Model.DeleteEmployeeTypeAsync(EmployeeType)
                        If actionResult.StateResult = True Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Deshacer()
                            Await Me.DeleteDocumentIndexed()
                        Else
                            If actionResult.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                                AsyncLoader(False)
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                            Else
                                AsyncLoader(False)
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesContacteAdministrador)
                            End If

                        End If
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneTipoEmpleado, Eform.TipoEmpleados)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneTipoEmpleado, Eform.TipoEmpleados)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AsyncLoader(True)
        AssigningValues()
        searchMode = False
        Using Model As New MEmployeeType(Me.Tag)
            If Await Model.SaveEmployeeTypeAsync(EmployeeType) = True Then

                If EmployeeType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf EmployeeType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or EmployeeType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                AsyncLoader(False)
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
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
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' METODO: Item nuevo del frontal del tipo de empleado
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaData, Eform.InfoMetaData), Me.EmployeeType.Code, Me.EmployeeType.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.EmployeeType.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaDataTitle, Eform.InfoMetaData), Me.EmployeeType.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaData, Eform.InfoMetaData), Me.EmployeeType.Code, Me.EmployeeType.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaDataTitle, Eform.InfoMetaData), Me.EmployeeType.Code)
            Return Me._doc
        End If
    End Function

#End Region

#Region "Methods"

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
        INDlyCtlEmployeeType.BeginUpdate()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDbteCode.EditValue = Nothing
        INDtxtName.EditValue = Nothing
        EmployeeClass = "01"
        ContributorSubtypeId = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        EmployeeType = New EmployeeType
        INDlyCtlEmployeeType.EndUpdate()
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEmployeeType.ActionsOnControls
        Set(value As Boolean)

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleEmployeeClass.Enabled = value
            INDsleContributorSubtype.Enabled = value
            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If

        End Set
    End Property


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        AsyncLoader(True)
        Using Model As New MEmployeeType(Me.Tag)
            EmployeeType = Await Model.GetEmployeeTypeAsync(INDbteCode.Text.Trim)
        End Using
        If Not EmployeeType Is Nothing AndAlso EmployeeType.Id > 0 Then
            Dim result = Await Model.GetBlockRecord(Me.Tag, EmployeeType.Id)
            With EmployeeType
                Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), EmployeeType.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), EmployeeType.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), EmployeeType.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), EmployeeType.ModificationDate)
                INDbteCode.EditValue = .Code
                INDtxtName.EditValue = .Name
                Me.BarraBotones.StatusRecord = .State
                EmployeeClass = .EmployeeClass

                If .ContributorSubtypeId IsNot Nothing Then
                    ContributorSubtypeId = .ContributorSubtypeId
                Else
                    ContributorSubtypeId = 3
                End If

            End With
            Me.GetDocumentIndexed(Me.Tag & "_" & Me.EmployeeType.Code)
            If result.Id = 0 Then
                Me.BarraBotones.SetDocuments(EmployeeType.Id)
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = EmployeeType.Id}
                Dim operation = Await Model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                record = result
                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        Else
            Me.BarraBotones.StatusRecord = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            EmployeeType = New EmployeeType
        End If
        AsyncLoader(False)
        ActionsOnControls = True
    End Function

    Private Sub InitializeTuple()
        _listEmployeeClass = New List(Of Tuple(Of String, String))
        _listEmployeeClass.Add(New Tuple(Of String, String)("01", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass01", NAME_MODULE))) 'Dependiente
        _listEmployeeClass.Add(New Tuple(Of String, String)("02", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass02", NAME_MODULE))) 'Servicio Doméstico
        _listEmployeeClass.Add(New Tuple(Of String, String)("04", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass04", NAME_MODULE))) 'Madre Comunitaria
        _listEmployeeClass.Add(New Tuple(Of String, String)("12", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass12", NAME_MODULE))) 'Aprendices del SENA en etapa lectiva
        _listEmployeeClass.Add(New Tuple(Of String, String)("18", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass18", NAME_MODULE))) 'Funcionarios públicos sin tope máximo de IBC
        _listEmployeeClass.Add(New Tuple(Of String, String)("19", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass19", NAME_MODULE))) 'Aprendices del SENA en etapa productiva
        _listEmployeeClass.Add(New Tuple(Of String, String)("21", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass21", NAME_MODULE))) 'Estudiante de Postgrado en salud
        _listEmployeeClass.Add(New Tuple(Of String, String)("22", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass22", NAME_MODULE))) 'Profesor de establecimiento particular
        _listEmployeeClass.Add(New Tuple(Of String, String)("23", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass23", NAME_MODULE))) 'Estudiantes aportes solo riesgos laborales
        _listEmployeeClass.Add(New Tuple(Of String, String)("30", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass30", NAME_MODULE))) 'Dependiente entidades o universidades públicas con régimen especial en salud
        _listEmployeeClass.Add(New Tuple(Of String, String)("31", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass31", NAME_MODULE))) 'Cooperados o pre cooperativas de trabajo asociado
        _listEmployeeClass.Add(New Tuple(Of String, String)("47", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass47", NAME_MODULE))) 'Trabajador dependiente de entidad beneficiaria del Sistema General de Participaciones - Aportes Patronal
        _listEmployeeClass.Add(New Tuple(Of String, String)("51", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass51", NAME_MODULE))) 'Trabajador de tiempo parcial
        _listEmployeeClass.Add(New Tuple(Of String, String)("54", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass54", NAME_MODULE))) 'Pre pensionado de entidad en liquidación
        _listEmployeeClass.Add(New Tuple(Of String, String)("56", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass56", NAME_MODULE))) 'Pre pensionado con aporte voluntario a salud
        _listEmployeeClass.Add(New Tuple(Of String, String)("58", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("EmployeeClass58", NAME_MODULE))) 'Estudiante de prácticas laborales en el sector público
        INDsleEmployeeClass.Properties.DataSource = _listEmployeeClass
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteCode.EditValue Is Nothing OrElse INDbteCode.Text.Equals(String.Empty) Then
            ValidateControls = False
        End If
        If INDtxtName.EditValue Is Nothing OrElse INDtxtName.Text.Equals(String.Empty) Then
            ValidateControls = False
        End If
        If INDsleEmployeeClass.EditValue Is Nothing OrElse INDsleEmployeeClass.Text.Equals(String.Empty) Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With EmployeeType
            .Code = INDbteCode.Text.Trim
            .Name = INDtxtName.Text.Trim
            .EmployeeClass = EmployeeClass
            .ContributorSubtypeId = ContributorSubtypeId
            .State = Me.BarraBotones.StatusRecord
        End With
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        EmployeeType = Nothing
        Model = Nothing
        Presenter = Nothing
        searchMode = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmEmployeeType_Load(sender As Object, e As EventArgs) Handles Me.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollEmployeeType.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PEmployeeType(Me)
        LoadStatus()
        Deshacer()
        InitializeTuple()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar el tipo de empleado en el evento keydown del codigo
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
                INDbteCode.Enabled = False
            End If
        End If
    End Sub

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContributorSubtype_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContributorSubtype.QueryPopUp
        If INDsleContributorSubtype.Properties.DataSource Is Nothing Then
            Presenter.InitializeContributorSubtype()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContributorSubtype_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContributorSubtype.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmContributorSubtype With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContributorSubtype()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

#End Region

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.EmployeeType IsNot Nothing AndAlso Me.EmployeeType.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.IdEntity =  String.Empty
    End Sub

#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEmployeeType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        searchMode = False
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
        INDlyCtlEmployeeType.ShowCustomizationForm()
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
            INDlyCtlEmployeeType.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtlEmployeeType.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MEmployeeType(Me.Tag)
                Dim dsFields As DataSet = model.GetFieldsNULL
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCtlEmployeeType.Items.Count - 1
                        INDlyCtlEmployeeType.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCtlEmployeeType.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtlEmployeeType.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCtlEmployeeType.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtlEmployeeType.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCtlEmployeeType.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCtlEmployeeType.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyCtlEmployeeType.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

End Class