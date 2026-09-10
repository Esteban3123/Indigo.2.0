'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 19-04-2013
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
Imports Domain.Entities
Imports Presentation.Payroll.MVP
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Security.Entities

#End Region
''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario sucursales
''' </summary>
Public Class FrmBranchOffice
    Implements IBranchOffice

#Region "Variables globales, Propiedades y Load"
    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Varaible que contiene la entidad de las sucursales
    ''' </summary> 
    Dim BranchOffice As Domain.Payroll.Entities.BranchOffice

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MBranchOffice
    ''' <summary>
    ''' Variable para acceder al modelo de departamentos
    ''' </summary>
    Dim ModelDepartment As MDepartaments
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PBranchOffice
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Propiedad que contiene el codigo de la unidad de negocio
    ''' </summary>
    Public Property Code As String Implements IBranchOffice.Code
        Get
            Return INDBteCode.Text
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el nombre de la unidad de negocio
    ''' </summary>
    Public Property NameBu As String Implements IBranchOffice.Name
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el ID de el tercero
    ''' </summary>
    Public Property CompanyId As Integer Implements IBranchOffice.CompanyId
        Get
            Return INDGleCompanyId.EditValue
        End Get
        Set(value As Integer)
            INDGleCompanyId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene la direccion
    ''' </summary>
    Public Property Address As String Implements IBranchOffice.Address
        Get
            Return INDTxtAddress.Text
        End Get
        Set(value As String)
            INDTxtAddress.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene ID del departamento
    ''' </summary>
    Public Property DepartmentId As Integer Implements IBranchOffice.DepartmentId
        Get
            Return INDGleDepartment.EditValue
        End Get
        Set(value As Integer)
            INDGleDepartment.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el ID de la ciudad
    ''' </summary>
    Public Property CityId As Integer Implements IBranchOffice.CityId
        Get
            Return INDGleCityId.EditValue
        End Get
        Set(value As Integer)
            INDGleCityId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el telefono fijo
    ''' </summary>
    Public Property Phone As String Implements IBranchOffice.Phone
        Get
            Return INDTxtPhone.Text
        End Get
        Set(value As String)
            INDTxtPhone.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga el grid look up de departamentos
    ''' </summary>
    Public WriteOnly Property Departments As List(Of Domain.Entities.Department) Implements IBranchOffice.Departments
        Set(value As List(Of Domain.Entities.Department))
            INDGleDepartment.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga el grid look up de ciudades
    ''' </summary>
    Public WriteOnly Property City As List(Of Domain.Entities.City) Implements IBranchOffice.City
        Set(value As List(Of Domain.Entities.City))
            INDGleCityId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga los terceros
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Company As List(Of Domain.Payroll.Entities.Company) Implements IBranchOffice.Company
        Set(value As List(Of Domain.Payroll.Entities.Company))
            INDGleCompanyId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que Contiene el estado de la unidad de negocio
    ''' </summary>
    Public Property State As Boolean Implements IBranchOffice.State
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBranchOffice.ActionsOnControls
        Set(value As Boolean)
            INDGleCompanyId.Enabled = Not value
            INDBteCode.Enabled = Not value
            INDTxtAddress.Enabled = value
            INDTxtName.Enabled = value
            INDTxtPhone.Enabled = value
            INDGleDepartment.Enabled = value
            INDGleCityId.Enabled = value
            If value Then
                INDTxtName.Focus()
            Else
                FocusInitial()
            End If
        End Set
    End Property

    Private Async Sub FrmBusinessUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim x = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules)
        Me.indigo = SessionValues.Instance
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollBranchOffice.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PBranchOffice(Me)
        Presenter.Initializes()
        GLESize()
        Me.BarraBotones.StatusRecordVisible = True
        If Me.IdModuleSource = 53 Then 'Si el id del modulo es nómina se habilita el control de empresa
            INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        If Me.Code IsNot Nothing AndAlso Not Me.Code.Trim().Equals(String.Empty) Then
            Await LoadControls()
        Else
            Deshacer()
        End If
    End Sub
#End Region

#Region "Metodos Funciones"
    Public Sub GLESize()
        Dim newSize As Drawing.Size = New Drawing.Size(500, 200)
        INDGleCompanyId.Properties.PopupFormSize = newSize
        INDGleCityId.Properties.PopupFormSize = newSize
        INDGleDepartment.Properties.PopupFormSize = newSize
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With BranchOffice
            .Code = Code

            If INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CompanyId = CompanyId
            Else
                .CompanyId = Nothing
            End If

            .Name = NameBu
            .Telephone = Phone
            .CityId = CityId
            .Address = Address
            .State = State
        End With
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Dim modelDepartment = New MDepartaments(MDepartaments.TAG)
        Departments = Await modelDepartment.ListAllDepartmentAsync()
        AsyncLoader(True)
        Using Model As New MBranchOffice(Me.Tag)
            BranchOffice = Model.GetBranchOffice(INDBteCode.Text)
            Company = Model.ListAllCompany
            AsyncLoader(False)
            ActionsOnControls = True
            If Not BranchOffice Is Nothing Then
                If BranchOffice.Id > 0 Then

                    Dim result = Await Model.GetBlockRecord(Me.Tag, BranchOffice.Id)
                    With BranchOffice
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Dim modelCity = New MCity(MCity.TAG)
                        Dim CityEntidad = modelCity.GetCityById(.CityId)
                        City = modelCity.ListAllCityDepartment(CityEntidad.DepartamentId)
                        LogicaBotonActualizar(True)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), BranchOffice.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), BranchOffice.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), BranchOffice.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(BranchOffice.ModificationDate Is Nothing, Nothing, BranchOffice.ModificationDate))
                        Code = .Code
                        NameBu = .Name

                        If .CompanyId IsNot Nothing Then
                            CompanyId = .CompanyId
                        End If

                        Address = .Address
                        Phone = .Telephone
                        CityId = .CityId
                        DepartmentId = CityEntidad.DepartamentId
                        State = .State
                    End With

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.BranchOffice.Code)

                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(BranchOffice.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = BranchOffice.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If


                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    'LogicaBotonActualizar(False)
                    'Me.BarraBotones.StatusRecordVisible = True
                    'Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                End If
            Else
                BranchOffice = New Domain.Payroll.Entities.BranchOffice
            End If
        End Using
    End Function
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyBusinessUnit.BeginUpdate()
        Code = String.Empty
        NameBu = String.Empty
        Address = String.Empty
        Phone = String.Empty
        State = Nothing
        CityId = Nothing
        INDGleCompanyId.Text = String.Empty
        CompanyId = Nothing
        DepartmentId = Nothing
        City = Nothing
        Departments = Nothing
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        BranchOffice = Nothing
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = False
        INDlyBusinessUnit.EndUpdate()
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
        If INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If CompanyId = Nothing Then
                INDGleCompanyId.Focus()
                ValidateControls = False
                Exit Function
            End If
        End If
        If Code = String.Empty Then
            INDBteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If NameBu = String.Empty Then
            INDTxtName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If DepartmentId = Nothing Then
            INDGleDepartment.Focus()
            ValidateControls = False
            Exit Function
        End If
        If CityId = Nothing Then
            INDGleCityId.Focus()
            ValidateControls = False
            Exit Function
        End If
        If Address = String.Empty Then
            INDTxtAddress.Focus()
            ValidateControls = False
            Exit Function
        End If
        If Phone = String.Empty Then
            INDTxtPhone.Focus()
            ValidateControls = False
            Exit Function
        End If
    End Function
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCodeBU_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' Metodo para cargar las ciudades cada que elijan un departamento
    ''' </summary>
    Private Sub INDGleDepartment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDepartment.EditValueChanged
        If DepartmentId <> Nothing Then
            Dim modelCity = New MCity(MCity.TAG)
            City = modelCity.ListAllCityDepartment(DepartmentId)
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar el nivel de cargo en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteCodeBU_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If Not String.IsNullOrEmpty(INDBteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then

                'Se valida si la compañia esta visible validarla antes de continuar
                If INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If INDGleCompanyId.EditValue Is Nothing OrElse INDGleCompanyId.EditValue = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una compañía"
                        INDGleCompanyId.Focus()
                        Exit Sub
                    End If
                End If

                Await LoadControls()
                If INDBteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
                INDBteCode.Enabled = False
            End If
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
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmBranchOfficeMetaData, Eform.InfoMetaData), Me.BranchOffice.Code, Me.BranchOffice.Name, INDGleCompanyId.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.BranchOffice.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmBranchOfficeMetaDataTitle, Eform.InfoMetaData), Me.BranchOffice.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmBranchOfficeMetaData, Eform.InfoMetaData), Me.BranchOffice.Code, BranchOffice.Name, INDGleCompanyId.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmBranchOfficeMetaDataTitle, Eform.InfoMetaData), Me.BranchOffice.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBranchOffice(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de sucursales.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de sucursales.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de sucursales.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If BranchOffice IsNot Nothing Then
            If BranchOffice.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MBranchOffice
                        AsyncLoader(True)
                        Dim result As ActionMessageResult(Of Domain.Payroll.Entities.BranchOffice)
                        result = Await Model.DeleteBranchOfficeAsync(BranchOffice)
                        If result.StateResult = True Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            AsyncLoader(False)
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
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        End If
        'Me.Close()
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de sucursales.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AsyncLoader(True)
        AssigningValues()
        Using Model As New MBranchOffice(Me.Tag)
            If Await Model.SaveBranchOfficeAsync(BranchOffice) = True Then
                If BranchOffice.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf BranchOffice.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        End Using
        AsyncLoader(False)
        'Me.Close()
        Deshacer()
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
            If INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDGleCompanyId.EditValue <> Nothing Then
                    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.BranchOfficeByCompany
                    .FiltroBusqueda = INDGleCompanyId.EditValue
                Else
                    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.BranchOffice
                End If
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}, New ColumnInfo() With {.Caption = "Dirección", .FieldName = "Address"}, New ColumnInfo() With {.Caption = "Teléfono", .FieldName = "Telephone"}, New ColumnInfo() With {.Caption = "Nit Empresa", .FieldName = "CompanyId.Codigo"}, New ColumnInfo() With {.Caption = "Nombre Empresa", .FieldName = "CompanyId.Descripcion"}}.ToList
            Else
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBranchOfficeByCompanyIsNull
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}, New ColumnInfo() With {.Caption = "Dirección", .FieldName = "Address"}, New ColumnInfo() With {.Caption = "Teléfono", .FieldName = "Telephone"}}.ToList
            End If
            BarraBotones.PrepareToolbar(eAction.New)
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
    ''' METODO: Item Deshacer del control de sucursales.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   
    ''' </summary>
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
    ''' <summary>
    ''' Abre el formulario de departamentos en un pop-up
    ''' </summary>
    Private Sub INDGleDepartment_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleDepartment.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDepartaments
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Abre el formulario de departamentos en un pop-up
    ''' </summary>
    Private Sub INDGleCityId_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleCityId.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCity
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Metodo para abrir form company en popup
    ''' </summary>
    Private Sub INDGleCompanyId_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleCompanyId.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCompany
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyBusinessUnit.ShowCustomizationForm()
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
            INDlyBusinessUnit.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyBusinessUnit.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MBranchOffice
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyBusinessUnit.Items.Count - 1
                        INDlyBusinessUnit.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyBusinessUnit.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyBusinessUnit.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyBusinessUnit.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyBusinessUnit.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyBusinessUnit.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyBusinessUnit.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyBusinessUnit.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

#Region "Handlers"

#Region "Shown"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        BranchOffice = Nothing
        Model = Nothing
        ModelDepartment = Nothing
        Presenter = Nothing
        record = Nothing
        PathFunctionalDefinitions = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBranchOffice_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        FocusInitial()
    End Sub

    ''' <summary>
    ''' Establece el foco inicial del control
    ''' </summary>
    Private Sub FocusInitial()
        If INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDGleCompanyId.Focus()
        Else
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBranchOffice_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#End Region

End Class