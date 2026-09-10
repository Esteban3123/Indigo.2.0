
'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Kevin Garay Rodriguez
' Created          : 10-04-2013
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
Imports Presentation.Common.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region
''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario Departamentos
''' </summary>
Public Class FrmDepartaments
    Implements IDepartaments, ICustomizableForm

#Region "Variables Globales Propiedades Intefaz y Load"
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Varaible que contiene la entidad del Departamento
    ''' </summary> 
    Dim Department As Department
    ''' <summary>
    ''' Varaible que contiene la entidad del País
    ''' </summary> 
    Dim Country As Country

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MDepartaments

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PDepartaments
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
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDepartaments.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el codigo del Departamento
    ''' </summary>
    Public Property CodeDepartament As String Implements IDepartaments.CodeDepartament
        Get
            If (INDBteDepCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteDepCode.Text
            End If
        End Get
        Set(value As String)
            INDBteDepCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el nombre del Departamento
    ''' </summary>
    Public Property NameDepartament As String Implements IDepartaments.NameDepartament
        Get
            Return INDTxtDepName.Text
        End Get
        Set(value As String)
            INDTxtDepName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene todos los paises
    ''' </summary>
    Public WriteOnly Property ListAllCountry As List(Of Country) Implements IDepartaments.ListAllCountry
        Set(value As List(Of Country))
            INDGleCountry.Properties.DataSource = value
            'INDGdlookUpCountry.EditValue = Nothing
        End Set
    End Property
    ''' <summary>
    ''' Esta función define una propiedad llamada GetIdCountry que implementa la interfaz
    ''' IDepartaments y está diseñada para manejar la obtención y configuración del identificador
    ''' de un país en el contexto de los departamentos.
    ''' </summary>
    Public Property GetIdCountry As Integer Implements IDepartaments.GetIdCountry
        Get
            Return INDGleCountry.EditValue
        End Get
        Set(value As Integer)
            INDGleCountry.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    Public Property StateDepartament As Boolean Implements IDepartaments.StateDepartament
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
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDepartaments.ActionsOnControls
        Set(value As Boolean)
            INDlyDepartamentos.BeginUpdate()
            INDGleCountry.Enabled = Not value
            INDBteDepCode.Enabled = value
            INDTxtDepName.Enabled = False
            INDlyDepartamentos.EndUpdate()
            If value = True Then
                INDBteDepCode.Focus()
            Else
                INDGleCountry.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    ''' <summary>
    ''' Metodo que carga el load del formulario 
    ''' </summary>
    Private Sub FrmDepartments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        'Me._doc = Nothing
        'Me.Model = New MDepartaments(Me.Tag)
        'Me.indigo = SessionValues.Instance
        '******************************'
        ' Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        'PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollDepartment.", Me.Name, ".xml")
        'LoadhronousDefinitions = New BackgroundWorker
        'If LoadhronousDefinitions.IsBusy = False Then
        '    LoadhronousDefinitions.RunWorkerAsync()
        'End If
        'Presenter = New PDepartaments(Me)
        'LoadStatus()
        'GLESize()
        'Deshacer()
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PDepartaments(Me)
        LoadStatus()
        GLESize()
        Deshacer()
    End Sub

#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmDepartmentMetaData, Eform.InfoMetaData), Me.Department.Code, Me.Department.Name, INDGleCountry.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Department.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmDepartmentMetaDataTitle, Eform.InfoMetaData), Me.Department.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmDepartmentMetaData, Eform.InfoMetaData), Me.Department.Code, Me.Department.Name, INDGleCountry.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmDepartmentMetaDataTitle, Eform.InfoMetaData), Me.Department.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MDepartaments(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates

        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub


    ''' <summary>
    ''' Metodo para dar tamaño al popup de los gridlookup control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GLESize()
        INDGleCountry.Properties.PopupFormSize = New Drawing.Size(500, 200)
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Department
            .Code = CodeDepartament
            .CountryId = GetIdCountry
            .Name = NameDepartament
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls(Optional idEntity As Integer = 0) As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'StateDepartament = True
        'INDTxtDepName.Enabled = True
        'AsyncLoader(True)
        'Using Model As New MDepartaments(Me.Tag)
        '    If idEntity > 0 Then
        '        Department = Await Model.GetDepartmentByIdAsync(idEntity)
        '    Else
        '        Department = Await Model.GetDepartmentAsync(INDBteDepCode.Text, GetIdCountry)
        '    End If
        'End Using
        'If Not Department Is Nothing Then
        '    If Department.Id > 0 Then
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Department.Id)
        '        With Department
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Department.CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Department.CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Department.ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Department.ModificationDate)
        '            CodeDepartament = .Code
        '            NameDepartament = .Name
        '            GetIdCountry = .CountryId
        '            StateDepartament = .State
        '        End With

        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.Department.Id)

        '        If result.Id = 0 Then
        '            Me.BarraBotones.SetDocuments(Department.Id)
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Department.Id}
        '            Dim operation = Await Model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            record = result
        '            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    Else
        '        Me.BarraBotones.StatusRecordVisible = True
        '        Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        'Else
        '    Department = New Department
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'AsyncLoader(False)
        INDTxtDepName.Enabled = True
        If Not String.IsNullOrEmpty(CodeDepartament) AndAlso Not String.IsNullOrWhiteSpace(CodeDepartament) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MDepartaments(CStr(Me.Tag))
                    AsyncLoader(True)
                    If idEntity > 0 Then
                        Department = Await Model.GetDepartmentByIdAsync(idEntity)
                    Else
                        Department = Await Model.GetDepartmentAsync(INDBteDepCode.Text, GetIdCountry)
                    End If
                    INDlyDepartamentos.BeginUpdate()
                    If Department IsNot Nothing AndAlso Department.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Department.Id))
                        With Department
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad

                            CodeDepartament = .Code
                            NameDepartament = .Name
                            GetIdCountry = .CountryId
                            StateDepartament = .State
                        End With
                        'Llenar NullText

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Department.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Department.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(Department.Id, Me.Tag.ToString(), Nothing, GetType(Department).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        'ActionsOnControls = True
                        'End Using
                    Else
                        AsyncLoader(False)
                        Department = New Department With {.State = True}
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    End If
                    INDBteDepCode.Enabled = False
                    INDlyDepartamentos.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                'INDBteCode.Enabled = False
                Throw ex
            End Try
        End If

    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        'Using Model As New MDepartaments(MDepartaments.TAG)
        '    ListAllCountry = Model.ListAllCountry()
        'End Using
        'INDGleCountry.EditValue = Nothing
        'INDBteDepCode.Text = String.Empty
        'INDTxtDepName.Text = String.Empty
        'Me.BarraBotones.StatusRecordVisible = False
        'Department = Nothing
        'ActionsOnControls = False
        ' DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        ' Me.BarraBotones.DisableBarDocument()
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        ' Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'Me.BarraBotones.CleanAuditBasic()





        INDlyDepartamentos.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing
        StateDepartament = True
        'Limpiar controles
        Department = Nothing
        Using Model As New MDepartaments(MDepartaments.TAG)
            ListAllCountry = Model.ListAllCountry()
        End Using
        INDGleCountry.EditValue = Nothing
        INDBteDepCode.Text = String.Empty
        INDTxtDepName.Text = String.Empty

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyDepartamentos.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteDepCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDTxtDepName.Text = String.Empty Then
            INDTxtDepName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If GetIdCountry = Nothing Then
            INDGleCountry.Focus()
            ValidateControls = False
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteDepCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteDepCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar el nivel de cargo en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteDepCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteDepCode.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If Not String.IsNullOrEmpty(CodeDepartament.Trim()) Then
                    Await Me.LoadControls()
                Else
                Mensaje(EeventViewerImages.Advertencia) = "Por favor digite un código"
            End If
                If INDBteDepCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
            'INDBteDepCode.Enabled = False
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
                AbrirBusqueda()
            End If

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
        'If Me.Department IsNot Nothing AndAlso Me.Department.Id > 0 Then
        '    If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Me.INDBteDepCode.Text = Me.IdEntity.Trim()
        '        Await Me.LoadControls(CInt(Me.IdEntity.Trim()))
        '    End If
        'Else 'Realiza la consulta normal
        '    Me.INDBteDepCode.Text = Me.IdEntity.Trim()
        '    Await Me.LoadControls(CInt(Me.IdEntity.Trim()))
        'End If
        'INDGleCountry.Enabled = False
        'INDBteDepCode.Enabled = False
        'INDTxtDepName.Enabled = True
        'INDTxtDepName.Focus()
        'Me.IdEntity = String.Empty



        If Me.Department IsNot Nothing AndAlso Me.Department.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteDepCode.Text = Me.IdEntity.Trim()
                Me.LoadControls(CInt(Me.IdEntity.Trim()))
            End If
        Else 'Realiza la consulta normal
            Me.INDBteDepCode.Text = Me.IdEntity.Trim()
            Me.LoadControls(CInt(Me.IdEntity.Trim()))
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        INDGleCountry.Enabled = False
        INDBteDepCode.Enabled = False
        INDTxtDepName.Enabled = True
        INDTxtDepName.Focus()
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Departamentos.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Deshacer()
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Departamentos.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Departamentos.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Department IsNot Nothing Then
            If Department.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        Using model As New MDepartaments(MyBase.Tag)
                            AsyncLoader(True)
                            Department.MarkAsDeleted()
                            Dim result As ActionMessageResult(Of Department)
                            result = Await model.DeleteDepartmentAsync(Department)
                            If result.StateResult = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                                Await Me.DeleteDocumentIndexed()
                                AsyncLoader(False)
                                Deshacer()
                            Else
                                If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                                End If
                                AsyncLoader(False)
                            End If
                        End Using
                    Catch ex As Exception
                        AsyncLoader(False)
                        Throw ex
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
            End If
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de Departamentos.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MDepartaments(MyBase.Tag)
                AsyncLoader(True)
                Dim res = Await Model.SaveDepartmentAsync(Department)
                If res.StateResult = True Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Department = res.ObjectEmbbeded
                    If Department.ChangeTracker.State = ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    ElseIf Department.ChangeTracker.State = ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    End If
                    AsyncLoader(False)
                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteDepCode.Enabled = False
            Throw ex
        End Try
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
            .FiltroBusqueda = GetIdCountry
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Department
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
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
        CodeDepartament = ReturnValue
        If CodeDepartament IsNot String.Empty Then
            Await LoadControls()
            If Not INDBteDepCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteDepCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Departaments.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
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
    ''' Abre el formulario de niveles de Paises en un pop-up
    ''' </summary>
    Private Sub INDGdlookUpCountry_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleCountry.Properties.ButtonClick
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
        'Using Formulario As New FrmCountry
        'Formulario.ShowDialog()
        'End Using
        'Presenter.Initializes()
        'End If
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
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

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyDepartamentos.ShowCustomizationForm()
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
            INDlyDepartamentos.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyDepartamentos.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MDepartaments(MDepartaments.TAG)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyDepartamentos.Items.Count - 1
                        INDlyDepartamentos.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyDepartamentos.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyDepartamentos.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyDepartamentos.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyDepartamentos.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyDepartamentos.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyDepartamentos.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyDepartamentos.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

#Region "Events"

    ''' <summary>
    ''' esta función se encarga de liberar recursos y limpiar variables relacionadas
    ''' con el formulario y su funcionamiento antes de que el formulario se cierre y se elimine.
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        Department = Nothing
        Country = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        record = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del pais
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleCountry_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleCountry.EditValueChanged
        If INDGleCountry.EditValue <> Nothing Then
            ActionsOnControls = True
        End If
    End Sub

    ''' <summary>
    ''' Evento click del boton del control pais
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleCountry_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleCountry.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCountry
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using listCountry As New MDepartaments(MyBase.Tag)
                    ListAllCountry = listCountry.ListAllCountry()
                End Using
            End Using
        End If

    End Sub


    ''' <summary>
    ''' esta función maneja la lógica para cambiar el estado (activo/inactivo) de un departamento
    ''' y maneja la visualización de mensajes apropiados en función del resultado de la operación.
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

        If Not String.IsNullOrEmpty(CodeDepartament) Then
            Using model As New MDepartaments(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Department.State
                Dim Result = Await model.ChangeState(CodeDepartament, GetIdCountry, state)
                AsyncLoader(False)
                If Result.StateResult Then
                    Me.Department = Result.ObjectEmbbeded
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

#End Region

End Class