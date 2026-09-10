'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 14/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.DocumentalSystem.MPV
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

Imports System.Text

#End Region

Public Class FrmAddOperatingUnit
    Implements IOperatingUnit

#Region "Properties"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IOperatingUnit.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IOperatingUnit.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' obtiene o establece la dirección
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Address As String Implements IOperatingUnit.Address
        Get
            Return INDtxtAddress.Text
        End Get
        Set(value As String)
            INDtxtAddress.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el email
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Email As String Implements IOperatingUnit.Email
        Get
            Return INDtxtEmail.Text
        End Get
        Set(value As String)
            INDtxtEmail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el email del auditor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmailAudit As String Implements IOperatingUnit.EmailAudit
        Get
            Return INDtxtEmailAudit.Text
        End Get
        Set(value As String)
            INDtxtEmailAudit.Text = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOperatingUnit.cityXpo
        Get
            Return INDsleIdCity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIdCity.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' obtiene o establece el id de lacuidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCity As Integer? Implements IOperatingUnit.IdCity
        Get
            Return INDsleIdCity.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdCity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdUnit As Integer? Implements IOperatingUnit.IdUnit

    ''' <summary>
    ''' obtiene y establece el codigo de la ips 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSCode As String Implements IOperatingUnit.IPSCode
        Get
            Return INDtxtIPSCode.Text
        End Get
        Set(value As String)
            INDtxtIPSCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el telefono
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Phone As String Implements IOperatingUnit.Phone
        Get
            Return INDtxtPhone.Text
        End Get
        Set(value As String)
            INDtxtPhone.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el codigo de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitCode As String Implements IOperatingUnit.UnitCode
        Get
            Return INDbtnUnitCode.Text
        End Get
        Set(value As String)
            INDbtnUnitCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el nombre de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitName As String Implements IOperatingUnit.UnitName
        Get
            Return INDtxtUnitName.Text
        End Get
        Set(value As String)
            INDtxtUnitName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la estructura organizacional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StructId As Integer? Implements IOperatingUnit.StructId
        Get
            Return INDsleStruct.EditValue
        End Get
        Set(value As Integer?)
            INDsleStruct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StructXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOperatingUnit.StructXpo
        Get
            Return INDsleStruct.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleStruct.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto del search de la estructura organizacional
    ''' </summary>
    ''' <remarks></remarks>
    Public TextSearchStruct As String

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <remarks></remarks>
    Public CodeST As String

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As POperatingUnit

    ''' <summary>
    ''' Variable que contiene la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim operatingUnit As OperatingUnit

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Commons"

    ''' <summary>
    ''' Evento que se ejecuta para actualizar los niveles de la estructura
    ''' </summary>
    Public Event RefreshDatasourceStruct()

#End Region

#Region "ICrud"


    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'If Not SearchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MOperatingUnit(Me.Tag.ToString())
                    AsyncLoader(True)
                    operatingUnit.MarkAsDeleted()
                    Dim result = Await Model.DeleteOperatingUnit(operatingUnit)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                        RaiseEvent RefreshDatasourceStruct()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If operatingUnit.Id <> 0 AndAlso operatingUnit.IdUnit Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Este es el registro principal, por lo tanto no se puede modificar la gerarquia"
            Exit Sub
        End If
        If ValidateControls() = True Then
            AssigningValues()
            Using model As New MOperatingUnit(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveOperatingUnit(operatingUnit)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If operatingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf operatingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.operatingUnit = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                    RaiseEvent RefreshDatasourceStruct()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IOperatingUnit.ActionsOnControls
        Set(value As Boolean)
            INDbtnUnitCode.Enabled = Not value
            INDtxtUnitName.Enabled = value
            INDtxtIPSCode.Enabled = value
            INDtxtAddress.Enabled = value
            INDtxtPhone.Enabled = value
            INDtxtEmail.Enabled = value
            INDtxtEmailAudit.Enabled = value
            INDsleIdCity.Enabled = value
            INDsleStruct.Enabled = value
            If value Then
                INDtxtUnitName.Focus()
            Else
                INDbtnUnitCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "UnitCode", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "UnitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.88)}}.ToList
            .ValorSolicitado = "UnitCode"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListOperatingUnit
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnUnitCode.Text = ReturnValue
        If INDbtnUnitCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnUnitCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnUnitCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.operatingUnit.UnitCode, Me.operatingUnit.UnitName), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.operatingUnit.UnitCode & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.operatingUnit.UnitCode), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.operatingUnit.UnitCode, Me.operatingUnit.UnitName)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.operatingUnit.UnitCode)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcBase.BeginUpdate()
        ActionsOnControls = False
        UnitCode = String.Empty
        UnitName = String.Empty
        IPSCode = String.Empty
        Address = String.Empty
        Phone = String.Empty
        Email = String.Empty
        EmailAudit = String.Empty
        IdCity = Nothing
        INDsleIdCity.Properties.NullText = String.Empty
        cityXpo = Nothing
        StructId = Nothing
        INDsleStruct.Properties.NullText = String.Empty
        StructXpo = Nothing
        BarraBotones.CleanAuditBasic()
        INDLcBase.EndUpdate()
        operatingUnit = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With operatingUnit
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .UnitCode = UnitCode
            .UnitName = UnitName
            .IPSCode = IPSCode
            .Address = Address
            .Phone = Phone
            .Email = Email
            .EmailAudit = EmailAudit
            .IdCity = IdCity
            .IdUnit = StructId
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MOperatingUnit(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        ActionsOnControls = True
        Using Model As New MOperatingUnit(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetOperatingUnitByCode(INDbtnUnitCode.Text.Trim)
            operatingUnit = resultOperation.ObjectEmbbeded
            If Not operatingUnit Is Nothing Then
                If operatingUnit.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(operatingUnit.Id))
                    With operatingUnit
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        UnitCode = .UnitCode
                        UnitName = .UnitName
                        IPSCode = .IPSCode
                        Address = .Address
                        Phone = .Phone
                        Email = .Email
                        EmailAudit = .EmailAudit
                        IdCity = .IdCity
                        StructId = .IdUnit

                        INDsleStruct.Properties.NullText = .OperatingUnitDescription
                        INDsleIdCity.Properties.NullText = .CityDescription
                    End With
                    AsyncLoader(False)
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.operatingUnit.UnitCode)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(operatingUnit.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = operatingUnit.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.SetDocuments(operatingUnit.Id)
                    ActionsOnControls = True

                Else
                    AsyncLoader(False)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else
                AsyncLoader(False)
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        End Using
    End Function

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddOperatingUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New POperatingUnit(Me)
        Presenter.LoadDefinitionLayout()
        Deshacer()
        INDsleStruct.Properties.Buttons(1).Visible = False

        If IdUnit IsNot Nothing Then
            StructId = IdUnit
            INDsleStruct.Properties.NullText = TextSearchStruct
        Else
            If CodeST <> String.Empty Then
                UnitCode = CodeST
                LoadControls()
            End If
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddOperatingUnit_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        DeleteBlockedRecord()
    End Sub

#End Region

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.operatingUnit IsNot Nothing AndAlso Me.operatingUnit.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnUnitCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnUnitCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity =  String.Empty
    End Sub

#Region "Keydown"

    ''' <summary>
    ''' se dispara al dar enter en el boton del codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnUnitCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnUnitCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDbtnUnitCode.Text.Trim()) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un Código."
                Exit Sub
            Else
                If INDbtnUnitCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                Await Me.LoadControls()
            End If
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddOperatingUnit_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnUnitCode.Text Is String.Empty Then
            INDbtnUnitCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de ciudad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIdCity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIdCity.QueryPopUp
        If cityXpo Is Nothing Then
            Presenter.InitializeCity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de estructura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleStruct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleStruct.QueryPopUp
        If StructXpo Is Nothing Then
            Presenter.InitializeStruct()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnUnitCode.ButtonClick
        OpenSearch()
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
        Me.Nuevo()
    End Sub


#End Region


End Class