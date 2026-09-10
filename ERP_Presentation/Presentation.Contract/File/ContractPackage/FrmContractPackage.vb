'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Giovanny Plazas Lozano
' Created          : 23/08/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class FrmContractPackage
    Implements IContractPackage

#Region "Builder"

    Sub New()
        InitializeComponent()
        _CtrTotalValue = New CtrTotalValue()
        _CtrTotalValue.Dock = DockStyle.Top
        AdditionalControlPanel.Controls.Add(_CtrTotalValue)
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PContractPackage

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Representa la entidad 
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractPackage As ContractPackage

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Entidad detalle Servicio
    ''' </summary>
    Private ContractPackageService As ContractPackageService

    ''' <summary>
    ''' entidad detalle producto
    ''' </summary>
    Private ContractPackageProduct As ContractPackageProduct

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As ContractSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' control de la parte superior para mostrar el valor total de los paquetes
    ''' </summary>
    Dim _CtrTotalValue As CtrTotalValue
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IContractPackage.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PackageName As String Implements IContractPackage.Name
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la entidad del Cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSEntityId As Integer? Implements IContractPackage.CUPSEntityId
        Get
            Return INDsleCups.EditValue
        End Get
        Set(value As Integer?)
            INDsleCups.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la descripcion relacionada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractDescriptionId As Integer? Implements IContractPackage.ContractDescriptionId
        Get
            Return INDsleRelatedDescription.EditValue
        End Get
        Set(value As Integer?)
            INDsleRelatedDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceId As Integer? Implements IContractPackage.IPSServiceId
        Get
            Return INDsleService.EditValue
        End Get
        Set(value As Integer?)
            INDsleService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IContractPackage.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de CupsEntity
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CupsEntityDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IContractPackage.XpoCupsEntity
        Get
            Return INDsleCups.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCups.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de CupsEntity para la rejilla de servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CupsEntityServiceDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IContractPackage.XpoCupsEntityService
        Get
            Return INDSleCupsEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCupsEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de Contract Description
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractDescriptionDataSource As List(Of CUPSEntityContractDescriptionsXpo) Implements IContractPackage.XpoContractDescription
        Get
            Return INDsleRelatedDescription.Properties.DataSource
        End Get
        Set(value As List(Of CUPSEntityContractDescriptionsXpo))
            INDsleRelatedDescription.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de Contract Description rejilla servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractDescriptionServiceDataSource As List(Of CUPSEntityContractDescriptionsXpo) Implements IContractPackage.XpoContractDescriptionService
        Get
            Return INDSleRelatedDescriptionD.Properties.DataSource
        End Get
        Set(value As List(Of CUPSEntityContractDescriptionsXpo))
            INDSleRelatedDescriptionD.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de IPS SERVICE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServices As DevExpress.Xpo.XPInstantFeedbackSource Implements IContractPackage.XpoIPSServiceByPackage
        Get
            Return INDsleService.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleService.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de productos activos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductByStatusDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IContractPackage.XpoProductByStatus
        Get
            Return INDSleProduct.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleProduct.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IContractPackage.Status
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IContractPackage.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IContractPackage.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequense As ContractSequence Implements IContractPackage.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As ContractSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequense.ContractSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor asignado al producto
    ''' </summary>
    ''' <returns></returns>
    Property ValorAssignInventory As Decimal
        Get
            Return INDSeValorAssignInventory.EditValue
        End Get
        Set(value As Decimal)
            INDSeValorAssignInventory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establce el valor asginado al servicio
    ''' </summary>
    ''' <returns></returns>
    Property ValorAssignService As Decimal
        Get
            Return INDSeValorAssignService.EditValue
        End Get
        Set(value As Decimal)
            INDSeValorAssignService.EditValue = value
        End Set
    End Property
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        If ContractPackage IsNot Nothing AndAlso ContractPackage.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MContractPackage(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteContractPackage(ContractPackage)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        SearchMode = False
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        ElseIf result.MessageResult(0) = "-001" Then
                            Mensaje(EeventViewerImages.MensajeError) = "El registro no se puede eliminar porque tiene relación con Servicio IPS."
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
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        AssigningValues()
        Using model As New MContractPackage(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveContractPackage(ContractPackage, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If ContractPackage.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                        Me.DicSequense(Me._sequense.ContractSequenceDetail(0).Id).RemoveAt(0)
                    End If
                    If Me._sequense.Sequential Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    End If

                ElseIf ContractPackage.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.ContractPackage = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.Message = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense IsNot Nothing AndAlso Me._sequense.IsManual Then
            Await ValidateCode()
        Else
            Await CreateNew()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListContractPackages
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' este metodo utilizado para la importacion de detalles 
    ''' </summary>
    Public Sub OpenImport() Handles BarraBotones.Click_ImportarInformacion
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnImport
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListContractPackages
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida el codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ValidateCode() As Task
        If INDbtnCode.Text = String.Empty Then
            If Me._sequense IsNot Nothing AndAlso Me._sequense.IsManual Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty", NAME_MODULE)
                INDbtnCode.Focus()
            Else
                Await CreateNew()
            End If
        Else
            Await Me.LoadControls()
        End If
    End Function

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IContractPackage.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtDescription.Enabled = value
            INDsleCups.Enabled = value
            INDsleRelatedDescription.Enabled = value
            INDsleService.Enabled = value
            INDtxtDescription.Enabled = value

            INDPceAddProduct.Enabled = value
            INDPceAddService.Enabled = value
            INDgcInventory.Enabled = value
            INDgcServices.Enabled = value
            INDEsbBillsOrShareInventory.Enabled = value
            INDEsbBillsOrShareService.Enabled = value

            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.ContractPackage IsNot Nothing AndAlso Me.ContractPackage.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnImport(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Try
            Using Model As New MContractPackage(MyTag)
                AsyncLoader(True)
                Dim Result = Await Model.GetContractPackage(ReturnValue)
                AsyncLoader(False)

                If Result Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Hubo Problemas con la Conexion a Servicios"
                    Exit Sub
                End If
                If Result.StateResult Then
                    Await MassiveAdd(Result.ObjectEmbbeded)
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = IIf(Result.Message = String.Empty, "Hubo Problemas en la Importación", Result.Message)
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ContractPackage.Code, Me.ContractPackage.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.ContractPackage.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ContractPackage.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ContractPackage.Code, Me.ContractPackage.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ContractPackage.Code)
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
        INDlyContractPackage.BeginUpdate()

        ActionsOnControls = False
        Code = String.Empty
        PackageName = String.Empty
        Description = String.Empty
        CUPSEntityId = Nothing
        INDsleCups.Properties.NullText = String.Empty
        ContractDescriptionId = Nothing
        INDsleRelatedDescription.Properties.NullText = String.Empty
        IPSServiceId = Nothing
        INDsleService.Properties.NullText = String.Empty
        CupsEntityDataSource = Nothing
        ContractDescriptionDataSource = Nothing
        IPSServices = Nothing
        Status = True
        BarraBotones.CleanAuditBasic()
        INDgcInventory.DataSource = Nothing
        ContractPackage = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        INDtxtName.ReadOnly = False

        DeleteBlockedRecord()
        Me._doc = Nothing

        INDgcServices.DataSource = Nothing
        INDgcInventory.DataSource = Nothing
        _CtrTotalValue.TotalValue = 0
        CleanPopUpMenu()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDlyContractPackage.EndUpdate()

    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With ContractPackage
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .CUPSEntityId = CUPSEntityId
            .Name = PackageName
            .ContractDescriptionId = ContractDescriptionId
            .IPSServiceId = IPSServiceId
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Try
            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MContractPackage(CStr(Me.Tag))
                AsyncLoader(True)
                Dim resultOperation = Await Model.GetContractPackage(INDbtnCode.Text.Trim)
                ContractPackage = resultOperation.ObjectEmbbeded
                CalculatePercentage()
                If ContractPackage IsNot Nothing AndAlso ContractPackage.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(ContractPackage.Id))
                        With ContractPackage
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            PackageName = .Name
                            CUPSEntityId = .CUPSEntityId
                            INDsleCups.Properties.NullText = .CupsCodeName
                            ContractDescriptionId = .ContractDescriptionId
                            INDsleRelatedDescription.Properties.NullText = .ContractDescriptionName
                            IPSServiceId = .IPSServiceId
                            INDsleService.Properties.NullText = .IPSServicesName
                            Status = .Status
                            Description = .Description
                            INDgcInventory.DataSource = .ContractPackageProduct.ToList()
                            INDgcServices.DataSource = .ContractPackageService.ToList()
                            INDviewInventory.RefreshData()
                            INDviewServices.RefreshData()
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ContractPackage.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = ContractPackage.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(ContractPackage.Id)
                        ActionsOnControls = True
                        INDbtnCode.Enabled = False
                        INDtxtName.ReadOnly = True
                    End Using
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await CreateNew()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = "El codigo ingresado No existe o esta mal Digitado"
                        Code = String.Empty
                        Await CreateNew()
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Crea la entidad cups
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CreateNew() As Task
        Status = True
        ContractPackage = New ContractPackage With {.Status = True}
        If Me.Sequense Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Function
        End If
        If Me._sequense IsNot Nothing AndAlso Me._sequense.IsManual Then
            ActionsOnControls = True
            INDtxtName.Focus()
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.ContractSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MContractPackage(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not ContractPackage.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    ContractPackage = Result.ObjectEmbbeded
                    CleanControls()
                Else
                    If Result.Message = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Establece el ancho de la columna mas info
    ''' </summary>
    Private Sub WidthActionsColumns()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewServices.Columns
            If col.Name = "colActions" Then
                col.Width = 20
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewInventory.Columns
            If col.Name = "colActions" Then
                col.Width = 20
            End If
        Next
    End Sub

    Private Sub CleanPopUpMenu()
        INDSleCupsEntity.EditValue = Nothing
        INDSleCupsEntity.Properties.NullText = String.Empty
        INDSleRelatedDescriptionD.EditValue = Nothing
        INDSleRelatedDescriptionD.Properties.NullText = String.Empty
        INDSeQuantity.EditValue = 0
        INDSleApplyCondition.EditValue = Nothing
        Me.ValorAssignService = 0
        INDSleProduct.EditValue = Nothing
        INDSleProduct.Properties.NullText = String.Empty
        INDSeQuantityP.EditValue = 0
        INDSleApplyConditionP.EditValue = Nothing
        ValorAssignInventory = 0
        Me.ContractPackageProduct = Nothing
        Me.ContractPackageService = Nothing
    End Sub
    Function ValidateAdd(Type As Integer) As Boolean
        Select Case Type
            Case 1
                If INDSleCupsEntity.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo CUPS no puede estar vacio"
                    Return False
                ElseIf INDSeQuantity.EditValue Is Nothing OrElse INDSeQuantity.EditValue = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Cantidad no puede estar vacio"
                    Return False
                ElseIf INDSleApplyCondition.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Aplica Condición no puede estar vacio"
                    Return False
                ElseIf INDSeValorAssignService.EditValue Is Nothing OrElse Me.ValorAssignService = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo del valor asignado no puede estar vacio"
                    Return False
                End If
            Case 2
                If INDSleProduct.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Producto no puede estar vacio"
                    Return False
                ElseIf INDSeQuantityP.EditValue Is Nothing OrElse INDSeQuantityP.EditValue = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Cantidad no puede estar vacio"
                    Return False
                ElseIf INDSleApplyConditionP.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo Aplica Condición no puede estar vacio"
                    Return False
                ElseIf INDSeValorAssignInventory.EditValue Is Nothing OrElse ValorAssignInventory = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El campo del valor asignado no puede estar vacio"
                    Return False
                End If
        End Select

        Return True
    End Function

    Private Sub EditDetail(ContractPackageService As ContractPackageService, ContractPackageProduct As ContractPackageProduct)
        If ContractPackageService IsNot Nothing Then
            With ContractPackageService
                INDSleCupsEntity.EditValue = .CUPSEntityId
                INDSleCupsEntity.Properties.NullText = .CupsCodeName
                INDSleRelatedDescriptionD.EditValue = .ContractDescriptionId
                INDSleRelatedDescriptionD.Properties.NullText = .ContractDescriptionName
                INDSeQuantity.EditValue = .Quantity
                INDSleApplyCondition.EditValue = .ApplyCondition
                Me.ValorAssignService = .UnitValue
                .UnitValueTotal = .UnitValue * .Quantity
            End With
            Me.ContractPackageService = ContractPackageService
            INDPceAddService.ShowPopup()

        ElseIf ContractPackageProduct IsNot Nothing Then
            With ContractPackageProduct
                INDSleProduct.EditValue = .ProductId
                INDSleProduct.Properties.NullText = .ProductCodName
                INDSeQuantityP.EditValue = .Quantity
                INDSleApplyConditionP.EditValue = .ApplyCondition
                ValorAssignInventory = .UnitValue
                .UnitValueTotal = .UnitValue * .Quantity
            End With
            Me.ContractPackageProduct = ContractPackageProduct
            INDPceAddProduct.ShowPopup()

        End If

    End Sub

    ''' <summary>
    ''' Elimina el activo
    ''' </summary>
    Private Sub DeleteItem(ListContractPackageService As List(Of ContractPackageService), ListContractPackageProduct As List(Of ContractPackageProduct))
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ListContractPackageService IsNot Nothing Then
                ListContractPackageService.ForEach(Sub(i As ContractPackageService)
                                                       i.MarkAsDeleted()
                                                       Me.ContractPackage.ContractPackageService.Remove(i)
                                                   End Sub)



                INDgcServices.DataSource = Me.ContractPackage.ContractPackageService
                INDviewServices.RefreshData()
                CalculatePercentage()

            ElseIf ListContractPackageProduct IsNot Nothing Then

                ListContractPackageProduct.ForEach(Sub(i As ContractPackageProduct)
                                                       i.MarkAsDeleted()
                                                       Me.ContractPackage.ContractPackageProduct.Remove(i)
                                                   End Sub)

                INDgcInventory.DataSource = Me.ContractPackage.ContractPackageProduct
                INDviewInventory.RefreshData()
                CalculatePercentage()
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        Presenter = Nothing
        ContractPackage = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContractPackage, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PContractPackage(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        SearchMode = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDviewServices, ListActions)
        IndigoGridView3.SetListAcction(INDviewInventory, ListActions)
        WidthActionsColumns()

        INDEsbBillsOrShareService.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "CodigoCups", .Comment = "El codigo CUPS"},
                            New ExcelColumn With {.Name = "CodigoDescripcionR", .Comment = "El Codigo de la Descripcion Relacionada"},
                            New ExcelColumn With {.Name = "Cantidad"},
                            New ExcelColumn With {.Name = "AplicaCondicion", .Comment = "si = 1 o No =0"},
                            New ExcelColumn With {.Name = "ValorAsignado", .Comment = "El valor asignado al CUPS"}
                        }
                    })

        INDEsbBillsOrShareInventory.AddExcelSheets(New ExcelSheet With {
                .Columns = New List(Of ExcelColumn) From {
                    New ExcelColumn With {.Name = "CodigoProducto", .Comment = "El codigo del producto"},
                    New ExcelColumn With {.Name = "Cantidad"},
                    New ExcelColumn With {.Name = "AplicaCondicion", .Comment = "si = 1 o No =0"},
                    New ExcelColumn With {.Name = "ValorAsignado", .Comment = "El valor asignado al CUPS"}
                }
            })
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractPackage_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await ValidateCode()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractPackage_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"
    Private Sub INDsleCups_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCupsEntity.QueryPopUp, INDsleCups.QueryPopUp
        If INDsleCups.Properties.DataSource Is Nothing Then
            Presenter.InitializeCups()
        End If
    End Sub

    Private Sub INDsleRelatedDescription_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRelatedDescription.QueryPopUp

    End Sub

    Private Sub INDsleRelatedDescriptionD_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRelatedDescriptionD.QueryPopUp

    End Sub

    Private Sub INDsleService_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleService.QueryPopUp
        If INDsleService.Properties.DataSource Is Nothing Then
            Presenter.InitializeIPSServices()
        End If
    End Sub

    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            Presenter.InitializeProductByStatus()
        End If
    End Sub

#End Region

#Region "ButtonClick"
#End Region

#Region "Closed"
    Private Sub INDPce_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceAddProduct.Closed, INDPceAddService.Closed
        CleanPopUpMenu()
    End Sub
#End Region
#Region "Click"
    Private Sub INDBtnAddService_Click(sender As Object, e As EventArgs) Handles INDBtnAddService.Click
        If ValidateAdd(1) = False Then
            Exit Sub
        End If
        If (From x In ContractPackage.ContractPackageService Where (ContractPackageService Is Nothing OrElse Not x.Equals(ContractPackageService)) AndAlso x.CUPSEntityId = INDSleCupsEntity.EditValue Select x).Count >= 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El Cups ya se encuentra agregado"
            Exit Sub
        End If

        Dim ContractPackageServiceObj As ContractPackageService = Nothing
        If Me.ContractPackageService Is Nothing Then
            ContractPackageServiceObj = New ContractPackageService
        Else
            ContractPackageServiceObj = ContractPackageService
        End If

        With ContractPackageServiceObj
            If ContractPackage IsNot Nothing AndAlso ContractPackage.Id > 0 Then
                .ContractPackageId = ContractPackage.Id
            End If
            .CUPSEntityId = INDSleCupsEntity.EditValue
            .CupsCodeName = INDSleCupsEntity.Text
            .ContractDescriptionId = INDSleRelatedDescriptionD.EditValue
            .ContractDescriptionName = INDSleRelatedDescriptionD.Text
            .Quantity = INDSeQuantity.EditValue
            .ApplyCondition = INDSleApplyCondition.EditValue
            .UnitValue = Me.ValorAssignService
            .UnitValueTotal = (Me.ValorAssignService * INDSeQuantity.EditValue)
        End With

        ContractPackage.ContractPackageService.Add(ContractPackageServiceObj)
        CalculatePercentage()
        INDgcServices.DataSource = ContractPackage.ContractPackageService.ToList()
        INDviewServices.RefreshData()
    End Sub

    Private Sub INDSbAddProduct_Click(sender As Object, e As EventArgs) Handles INDSbAddProduct.Click
        If ValidateAdd(2) = False Then
            Exit Sub
        End If

        If (From x In ContractPackage.ContractPackageProduct Where (ContractPackageProduct Is Nothing OrElse Not x.Equals(ContractPackageProduct)) AndAlso x.ProductId = INDSleProduct.EditValue Select x).Count >= 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El Producto ya se encuentra agregado"
            Exit Sub
        End If

        Dim ContractPackageProductObj As ContractPackageProduct = Nothing

        If ContractPackageProduct Is Nothing Then
            ContractPackageProductObj = New ContractPackageProduct
        Else
            ContractPackageProductObj = ContractPackageProduct
        End If

        With ContractPackageProductObj
            If ContractPackage IsNot Nothing AndAlso ContractPackage.Id > 0 Then
                .ContractPackageId = ContractPackage.Id
            End If
            .ProductId = INDSleProduct.EditValue
            .ProductCodName = INDSleProduct.Text
            .Description = Presenter.GetProductDescription(INDSleProduct.EditValue)
            .Quantity = INDSeQuantityP.EditValue
            .ApplyCondition = INDSleApplyConditionP.EditValue
            .UnitValue = ValorAssignInventory
            .UnitValueTotal = ValorAssignInventory * INDSeQuantityP.EditValue
        End With

        ContractPackage.ContractPackageProduct.Add(ContractPackageProductObj)
        CalculatePercentage()
        INDgcInventory.DataSource = ContractPackage.ContractPackageProduct.ToList()
        INDviewInventory.RefreshData()
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                If (From x In INDviewServices.GetSelectedRows() Select DirectCast(INDviewServices.GetRow(x), ContractPackageService)).ToList().Count = 1 Then
                    EditDetail(CType(INDviewServices.GetFocusedRow(), ContractPackageService), Nothing)
                End If
            Case "Remove"
                DeleteItem((From x In INDviewServices.GetSelectedRows() Select DirectCast(INDviewServices.GetRow(x), ContractPackageService)).ToList(), Nothing)

        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                If (From x In INDviewInventory.GetSelectedRows() Select DirectCast(INDviewInventory.GetRow(x), ContractPackageProduct)).ToList().Count = 1 Then
                    EditDetail(Nothing, CType(INDviewInventory.GetFocusedRow(), ContractPackageProduct))
                End If
            Case "Remove"
                DeleteItem(Nothing, (From x In INDviewInventory.GetSelectedRows() Select DirectCast(INDviewInventory.GetRow(x), ContractPackageProduct)).ToList())

        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                If (From x In INDviewServices.GetSelectedRows() Select DirectCast(INDviewServices.GetRow(x), ContractPackageService)).ToList().Count = 1 Then
                    EditDetail(CType(INDviewServices.GetFocusedRow(), ContractPackageService), Nothing)
                End If
            Case "Remove"
                DeleteItem((From x In INDviewServices.GetSelectedRows() Select DirectCast(INDviewServices.GetRow(x), ContractPackageService)).ToList(), Nothing)
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                If (From x In INDviewInventory.GetSelectedRows() Select DirectCast(INDviewInventory.GetRow(x), ContractPackageProduct)).ToList().Count = 1 Then
                    EditDetail(Nothing, CType(INDviewInventory.GetFocusedRow(), ContractPackageProduct))
                End If
            Case "Remove"
                DeleteItem(Nothing, (From x In INDviewInventory.GetSelectedRows() Select DirectCast(INDviewInventory.GetRow(x), ContractPackageProduct)).ToList())

        End Select
    End Sub

#End Region

#Region "PasteToGrid"
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            AsyncLoader(True)
            Using Model As New MContractPackage(MyTag)

                Dim result = Await Model.SetCopyPasteOrImportFile(e.Rows, sender.Name)
                If result Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La accion No se pudo Completar"
                    AsyncLoader(False)
                    Exit Sub
                End If

                If result.StateResult Then
                    Await MassiveAdd(result.ObjectEmbbeded)
                End If
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Function MassiveAdd(ContractPackageM As ContractPackage) As Task
        If ContractPackageM.ContractPackageProduct IsNot Nothing AndAlso ContractPackageM.ContractPackageProduct.Count > 0 Then
            ContractPackageM.ContractPackageProduct.ToList().ForEach(Sub(x As ContractPackageProduct)
                                                                         If (From i In ContractPackage.ContractPackageProduct Where i.ProductId = x.ProductId Select i).Count = 0 Then
                                                                             If Me.ContractPackage IsNot Nothing AndAlso Me.ContractPackage.Id > 0 Then
                                                                                 x.ContractPackageId = Me.ContractPackage.Id
                                                                             End If
                                                                             ContractPackage.ContractPackageProduct.Add(x)
                                                                         Else
                                                                             Mensaje(EeventViewerImages.Advertencia) = $"El prodcuto {x.ProductCodName} ya esta Agregado"
                                                                         End If
                                                                     End Sub)
            INDgcInventory.DataSource = ContractPackage.ContractPackageProduct.ToList()
            INDviewInventory.RefreshData()
        End If
        If ContractPackageM.ContractPackageService IsNot Nothing AndAlso ContractPackageM.ContractPackageService.Count > 0 Then
            ContractPackageM.ContractPackageService.ToList().ForEach(Sub(x As ContractPackageService)
                                                                         If (From i In ContractPackage.ContractPackageService Where i.CUPSEntityId = x.CUPSEntityId Select i).Count = 0 Then
                                                                             If Me.ContractPackage IsNot Nothing AndAlso Me.ContractPackage.Id > 0 Then
                                                                                 x.ContractPackageId = Me.ContractPackage.Id
                                                                             End If
                                                                             ContractPackage.ContractPackageService.Add(x)
                                                                         Else
                                                                             Mensaje(EeventViewerImages.Advertencia) = $"El prodcuto {x.CupsCodeName} ya esta Agregado"
                                                                         End If
                                                                     End Sub)
            INDgcServices.DataSource = ContractPackage.ContractPackageService.ToList()
            INDviewServices.RefreshData()
        End If
        Me.CalculatePercentage()
    End Function

    Private Sub CalculatePercentage()
        If ContractPackage.ContractPackageService IsNot Nothing OrElse ContractPackage.ContractPackageProduct IsNot Nothing Then
            Dim TotalService, TotalProduct, Total As Decimal

            'Total inventario
            TotalProduct = ContractPackage.ContractPackageProduct.Sum(Function(o) o.UnitValueTotal)

            'Total servicios
            TotalService = ContractPackage.ContractPackageService.Sum(Function(x) x.UnitValueTotal)

            'Total Paquete
            Total = TotalProduct + TotalService
            _CtrTotalValue.TotalValue = Total

            For Each item In ContractPackage.ContractPackageService
                With item
                    item.Percentage = Math.Round(item.UnitValueTotal / Total, 3)
                End With
            Next

            For Each item In ContractPackage.ContractPackageProduct
                With item
                    item.Percentage = Math.Round(item.UnitValueTotal / Total, 3)
                End With
            Next
            INDgcInventory.RefreshDataSource()
            INDgcServices.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "EditValuechanged"

    Private Sub INDsleCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCups.EditValueChanged
        ContractDescriptionDataSource = Nothing
        If CUPSEntityId > 0 Then
            Presenter.InitializeContractDescription(CUPSEntityId)
        End If
        If ContractDescriptionDataSource IsNot Nothing AndAlso ContractDescriptionDataSource.Count > 0 Then
            INDlyIRelatedDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            ContractDescriptionId = Nothing
            INDlyIRelatedDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDSleCupsEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCupsEntity.EditValueChanged
        ContractDescriptionServiceDataSource = Nothing
        If INDSleCupsEntity.EditValue IsNot Nothing AndAlso INDSleCupsEntity.EditValue > 0 Then
            Presenter.InitializeContractDescriptionPopUp(INDSleCupsEntity.EditValue)
        End If
        If ContractDescriptionServiceDataSource IsNot Nothing AndAlso ContractDescriptionServiceDataSource.Count > 0 Then
            INDLciRelatedDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDSleRelatedDescriptionD.EditValue = Nothing
            INDLciRelatedDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        End If
    End Sub
#End Region
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
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
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub
#End Region

End Class