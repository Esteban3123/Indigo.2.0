'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 04-08-2013
'
' Last Modified By : Andres Alarcon
' Last Modified On : 12-10-2023
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Xpo
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
Imports Presentation.FixedAsset.MVP

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
Public Class FrmFixedAssetInsurance
    Implements IFixedAssetInsurance, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

#End Region

#Region "Variable Globales Propiedades Intefaz y Load"

    Public Property IdThirdPartyInsurance As Integer Implements IFixedAssetInsurance.IdThirdPartyInsurance
        Get
            Return INDSlThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSlThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la aseguradora
    ''' </summary>
    Public Property CodeInsurance As String Implements IFixedAssetInsurance.CodeInsurance
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre de la aseguradora
    ''' </summary>
    Public Property NameInsurance As String Implements IFixedAssetInsurance.NameInsurance
        Get
            Return INDtxtName.Text.Trim
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    Public Property CityInsurance As String Implements IFixedAssetInsurance.CityInsurance
        Get
            Return INDglCity.EditValue
        End Get
        Set(value As String)
            INDglCity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la direccion del sitio web de la aseguradora
    ''' </summary>
    Public Property WebSiteInsurance As String Implements IFixedAssetInsurance.WebSiteInsurance
        Get
            Return INDtxtWebSite.Text.Trim
        End Get
        Set(value As String)
            INDtxtWebSite.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga los departamentos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DepartmentDataSource As List(Of Department) Implements IFixedAssetInsurance.DepartmentDataSource
        Set(value As List(Of Department))
            INDglDepartamentos.Properties.DataSource = value
            INDglDepartamentos.Properties.PopupFormWidth = 400
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CityDataSource As List(Of City) Implements IFixedAssetInsurance.CityDataSource
        Set(value As List(Of City))
            INDglCity.Properties.DataSource = value
            INDglCity.Properties.PopupFormWidth = 400
        End Set
    End Property

    Public Property Status As Boolean Implements IFixedAssetInsurance.StateInsurance
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetInsurance.ActionsOnControls
        Set(value As Boolean)
            INDlyInsurance.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDSlThirdParty.Enabled = value
            INDtxtName.Enabled = value
            INDtxtWebSite.Enabled = value
            INDglCity.Enabled = value
            INDglDepartamentos.Enabled = value
            INDpceContactData.Enabled = value

            INDlyInsurance.EndUpdate()
            If value = True Then
                INDSlThirdParty.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    Property ThirdPartyXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Private ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Private PathFunctionalDefinitions As String

    ''' <summary>
    ''' Variable que contiene la entidad aseguradoras
    ''' </summary>
    Private Insurance As FixedAssetInsurance

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Private Model As New MFixedAssetInsurance

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private Presenter As PFixedAssetInsurance

    ''' <summary>
    ''' Variable que se utiliza para acceder al modelo de los departamentos
    ''' </summary>
    Private ModelDepartment As MDepartaments

    ''' <summary>
    ''' Variable para utilizar el modelo de las ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Private ModelCity As MCity

    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Private ListadoEliminadosTelefono As New List(Of Phone)

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Private ListadoEliminadosDireccion As New List(Of Address)

    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Private ListadoEliminadosEmail As New List(Of Email)

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private blockRecord As BlockRecordFixedAsset

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Gets or sets the sequense.
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetInsurance.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Insurance = Nothing
        Model = Nothing
        Presenter = Nothing
        ModelDepartment = Nothing
        ModelCity = Nothing
        ListadoEliminadosTelefono = Nothing
        ListadoEliminadosDireccion = Nothing
        ListadoEliminadosEmail = Nothing
        blockRecord = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub


    Private Async Sub FrMResponsible_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetInsurance(Me)
        Presenter.GetSequense()
        ModelDepartment = New MDepartaments(MDepartaments.TAG)
        DepartmentDataSource = Await ModelDepartment.ListAllDepartmentAsync()
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MFixedAssetInsurance
                AsyncLoader(True)
                Dim result As ActionResult(Of FixedAssetInsurance) = Await Model.SaveInsurance(Me.Insurance, Me._idCurrentSequence, Me._sequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Insurance = result.ObjectEmbbeded
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

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me.Insurance IsNot Nothing AndAlso Me.Insurance.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFixedAssetInsurance
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteInsurance(Me.Insurance)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
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
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInsurance()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetInsurance
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
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                Await model.DeleteBlockRecord(blockRecord)
                blockRecord = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.Insurance.Code, Me.Insurance.Name, Me.Insurance.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Insurance.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.Insurance.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.Insurance.Code, Me.Insurance.Name, Me.Insurance.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.Insurance.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewEntity()
        Insurance = New FixedAssetInsurance() With {.Status = True}
        If Me._sequence.Scope IsNot Nothing Then
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                        PrepareToolbar(Me.DicSequense(Me._idCurrentSequence)(0))
                    Else
                        Using model As New MBlockRecordAndSequenceFixedAsset("1702")
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            PrepareToolbar(Me.DicSequense(Me._idCurrentSequence)(0))
                        Else
                            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                End If
            Else
                PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
            End If
        Else
            Me.ActionsOnControls = False
            Mensaje(EeventViewerImages.Advertencia) = "Revise la Secuencia Numérica por favor. Comunicar con el Administrador."
        End If
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        INDbteCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset("1702")
            Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyInsurance.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDbteCode.Text = String.Empty
        INDSlThirdParty.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtWebSite.Text = String.Empty
        INDglCity.EditValue = Nothing
        INDglDepartamentos.EditValue = Nothing
        INDSlThirdParty.Properties.NullText = String.Empty
        CtrContacts.LimpiarControles()
        Insurance = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyInsurance.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeInsurance) AndAlso Not String.IsNullOrWhiteSpace(CodeInsurance) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetInsurance
                    AsyncLoader(True)
                    Insurance = Await Model.GetInsurance(INDbteCode.Text.Trim)
                    INDlyInsurance.BeginUpdate()
                    If Insurance IsNot Nothing AndAlso Insurance.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        blockRecord = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Insurance.Id))
                        With Insurance
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            CodeInsurance = .Code
                            INDSlThirdParty.Properties.NullText = .NameThirdParty
                            IdThirdPartyInsurance = .IdThirdParty
                            NameInsurance = .Name
                            WebSiteInsurance = .WebSite
                            Dim modelCity = New MCity(MCity.TAG)
                            Dim CityEntidad = modelCity.GetCityById(.IdCity)
                            INDglDepartamentos.EditValue = CityEntidad.DepartamentId
                            CityInsurance = .IdCity

                            'Llenar Entidad
                            'Control Datos de Contacto
                            If .Person IsNot Nothing Then
                                If .Person.Address IsNot Nothing Then
                                    CtrContacts.EstablecerDataSourceDireccion = .Person.Address.ToList
                                End If
                                If .Person.Phone IsNot Nothing Then
                                    CtrContacts.EstablecerDataSourceTelefono = .Person.Phone.ToList
                                End If
                                If .Person.Email IsNot Nothing Then
                                    CtrContacts.EstablecerDataSourceEmail = .Person.Email.ToList
                                End If
                            End If
                            Status = .Status
                        End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Insurance.Code)
                        If blockRecord.Id = 0 Then
                            blockRecord = (Await Model.SaveBlockRecord(
                                    New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Insurance.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), blockRecord.CodUser, blockRecord.NameUser, blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, blockRecord.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(Insurance.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetInsurance).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInsurance()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeInsurance = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyInsurance.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForm() As Boolean
        ValidateControlsForm = True
        If INDbteCode.Text = String.Empty Then
            ValidateControlsForm = False
        End If
        If INDtxtName.Text = String.Empty Then
            ValidateControlsForm = False
        End If
        If Object.Equals(CityInsurance, Nothing) = True Then
            ValidateControlsForm = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Insurance
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeInsurance
            .IdThirdParty = IdThirdPartyInsurance
            .Name = NameInsurance
            .WebSite = WebSiteInsurance
            .IdCity = CityInsurance
        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeInsurance.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeInsurance) Then
                    Await Me.NewInsurance()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
    ''' <summary>
    ''' Metodo que carga ciudades dependiendo el departamento
    ''' </summary>
    Private Sub INDGleDepartment_EditValueChanged(sender As Object, e As EventArgs) Handles INDglDepartamentos.EditValueChanged
        If Object.Equals(INDglDepartamentos.EditValue, Nothing) = False Then
            ModelCity = New Presentation.Common.MVP.MCity(MCity.TAG)
            CityDataSource = ModelCity.ListAllCityDepartment(INDglDepartamentos.EditValue)
        End If
    End Sub

    Private Sub INDtxtName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtName.KeyDown
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If Insurance.Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(Insurance.Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            Insurance.Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = Insurance.Person.Address
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If Insurance.Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(Insurance.Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            Insurance.Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = Insurance.Person.Phone
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If Insurance.Person.Email.Count > 0 Then
            ListadoEliminadosEmail.Add(Insurance.Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            Insurance.Person.Email.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = Insurance.Person.Email
        End If
    End Sub
    ''' <summary>
    ''' Evento para cambia el tipo Email
    ''' </summary>
    Private Sub CtrContactos1_ChangeEmailType(Type As Byte) Handles CtrContacts.ChangeEmailType
        If Insurance.Person.Email.Count > 0 Then
            Dim email = Insurance.Person.Email.ElementAt(CtrContacts.ItemSelecionadoEmail)
            email.Type = Type
            If email.Id > 0 Then
                email.ChangeTracker.State = ObjectState.Modified
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If Insurance.Person.Address Is Nothing Then
            Insurance.Person.Address = New Domain.Entities.TrackableCollection(Of Address)
        End If
        If Insurance.Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
            Insurance.Person.Address.Add(New Address With 
                                            {
                                                .DepartmentId = CtrContacts.DepartmentId, 
                                                .DepartmentName = CtrContacts.DepartmentName, 
                                                .CityId = CtrContacts.CityId, 
                                                .CityName = CtrContacts.CityName, 
                                                .Addresss = CtrContacts.Direccion, 
                                                .Synchronized = "1", 
                                                .State = True
                                            }
                                         )
        End If
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceDireccion = Insurance.Person.Address
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If Insurance.Person.Email Is Nothing Then
            Insurance.Person.Email = New Domain.Entities.TrackableCollection(Of Email)
        End If
        If Insurance.Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
            Insurance.Person.Email.Add(New Email With {.Email1 = CtrContacts.Email, .Synchronized = 2, .Type = CtrContacts.EmailType, .State = True})
        End If
        CtrContacts.EstablecerDataSourceEmail = Nothing
        CtrContacts.EstablecerDataSourceEmail = Insurance.Person.Email
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If Insurance.Person.Phone Is Nothing Then
            Insurance.Person.Phone = New Domain.Entities.TrackableCollection(Of Phone)
        End If
        If Insurance.Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
            Insurance.Person.Phone.Add(New Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1"})
        End If
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceTelefono = Insurance.Person.Phone
    End Sub

    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmBudgetConcept_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled = True Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmBudgetConcept_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Insurance IsNot Nothing AndAlso Me.Insurance.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
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
    ''' Metodo para marcar la entidad a guardar si se insertan detallados
    ''' </summary>
    ''' <remarks></remarks>
    Sub MarkEntity()
        If Me.Insurance.ChangeTracker.State = ObjectState.Unchanged Then
            If Me.Insurance.Person IsNot Nothing AndAlso Me.Insurance.Person.Email.Count > 0 Then
                For Each item In Me.Insurance.Person.Email
                    If item.ChangeTracker.State = ObjectState.Modified Or item.ChangeTracker.State = ObjectState.Deleted Or item.ChangeTracker.State = ObjectState.Modified Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                        Exit Sub
                    End If
                Next
            End If
            If Me.Insurance.Person IsNot Nothing AndAlso Me.Insurance.Person.Address.Count > 0 Then
                For Each item In Me.Insurance.Person.Address
                    If item.ChangeTracker.State = ObjectState.Modified Or item.ChangeTracker.State = ObjectState.Deleted Or item.ChangeTracker.State = ObjectState.Modified Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                        Exit Sub
                    End If
                Next
            End If
            If Me.Insurance.Person IsNot Nothing AndAlso Me.Insurance.Person.Phone.Count > 0 Then
                For Each item In Me.Insurance.Person.Phone
                    If item.ChangeTracker.State = ObjectState.Modified Or item.ChangeTracker.State = ObjectState.Deleted Or item.ChangeTracker.State = ObjectState.Modified Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                        Exit Sub
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region "Eventos Barra Botones"

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
        Nuevo()
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
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.Insurance.Code) Then
            Try
                Using model As New MFixedAssetInsurance
                    AsyncLoader(True)
                    Dim state As Boolean = Not Insurance.Status
                    Dim result As ActionResult(Of FixedAssetInsurance) = Await model.ChangeState(Me.Insurance.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Insurance = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyInsurance.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub
    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront Then
            INDlyInsurance.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyInsurance.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyInsurance.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyInsurance.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
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
            INDlyInsurance.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewInsurance() As Task
        Insurance = New FixedAssetInsurance() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeInsurance = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeInsurance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeInsurance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeInsurance = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    Private Sub INDSlThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlThirdParty.QueryPopUp
        If INDSlThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ThirdPartyXPO Is Nothing Then
            Using model As New MFixedAssetInsurance()
                ThirdPartyXPO = model.ListThirdParty()
            End Using
        End If
        INDSlThirdParty.Properties.DataSource = ThirdPartyXPO
    End Sub


End Class