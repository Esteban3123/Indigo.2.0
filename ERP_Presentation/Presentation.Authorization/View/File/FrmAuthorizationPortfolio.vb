'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/04/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Contract
Imports Presentation.Controls
Imports CupsEntityXpo = Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo

#End Region

Public Class FrmAuthorizationPortfolio
    Implements IAuthorizationPortfolio, ICustomizableForm

    Public Sub New()

        InitializeComponent()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        IndigoGridControl1.SetExportButton(INDgcCUPS, True)
        IndigoGridControl2.SetExportButton(INDgcProducts, True)
        IndigoGridControl3.SetExportButton(INDgcCareCenter, True)
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl3, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

#Region "Globals"

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAuthorizationPortfolio

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Authorization"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.AuthorizationSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordAuthorization

    ''' <summary>
    ''' representa la entidad de plantillas de procedimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private AuthorizationPortfolio As AuthorizationPortfolio

    Private ListAuthorizationPortfolioCUPSEntity As List(Of AuthorizationPortfolioCUPSEntity)

    Private ListDeleteAuthorizationPortfolioCUPSEntity As List(Of AuthorizationPortfolioCUPSEntity)

    Private ListAuthorizationPortfolioInventoryProduct As List(Of AuthorizationPortfolioInventoryProduct)

    Private ListDeleteAuthorizationPortfolioInventoryProduct As List(Of AuthorizationPortfolioInventoryProduct)

    Private ListAuthorizationPortfolioCareCenter As List(Of AuthorizationPortfolioCareCenter)

    Private ListDeleteAuthorizationPortfolioCareCenter As List(Of AuthorizationPortfolioCareCenter)

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsyncCUPS As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsyncProducts As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsyncCareCenter As CancellationTokenSource

#End Region

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAuthorizationPortfolio.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDpceAddCups.Enabled = value
            INDEsbExportCUPS.Enabled = value
            INDgcCUPS.Enabled = value
            INDpceAddProducts.Enabled = value
            INDEsbExportProducts.Enabled = value
            INDgcProducts.Enabled = value
            INDsleCareCenter.Enabled = value
            INDbtnAddCareCenter.Enabled = value
            INDgcCareCenter.Enabled = value
            INDlyRoot.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    Public Property Code As String Implements IAuthorizationPortfolio.Code
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
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAuthorizationPortfolio.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IAuthorizationPortfolio.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    Public Property NamePortfolio As String Implements IAuthorizationPortfolio.Name
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequense As AuthorizationSequence Implements IAuthorizationPortfolio.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As AuthorizationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.AuthorizationSequenceDetail In Me._sequence.AuthorizationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IAuthorizationPortfolio.Status
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
    ''' datasource de entidades CUPS
    ''' </summary>
    Public Property CupsEntityXPO As DevExpress.Xpo.XPCollection
        Get
            Return INDgcCupsEntity.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDgcCupsEntity.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad asigna valor al botón INDgcPopupProducts
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductsXpo As DevExpress.Xpo.XPCollection(Of InventoryProductXpo)
        Get
            Return INDgcPopupProducts.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of InventoryProductXpo))
            INDgcPopupProducts.DataSource = value
        End Set
    End Property

#End Region

#Region "Crud"
    ''' <summary>
    ''' Se le da funcionalidad a Buscar implementando el ICrudBase
    ''' Invoca el OpenSearch donde se encuentra la lógica
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Limpia controles por medio del CleanControls
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Valida autorización de portafolio y muestra mensaje para confirmar la eliminación
    ''' Utiliza el modelo para MAuthorizationPortalio para validar permisos
    ''' Si todo está correcto Elimina y si no, envía los mensajes de error correspondientes
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If AuthorizationPortfolio IsNot Nothing AndAlso AuthorizationPortfolio.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MAuthorizationPortfolio(Me.Tag.ToString())
                    AsyncLoader(True)
                    AuthorizationPortfolio.TransactionalContainer = indigo.TransactionalContainer
                    Dim result = Await Model.DeleteAuthorizationPortfolio(AuthorizationPortfolio)
                    If result.StateResult Then
                        Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        ElseIf Not String.IsNullOrEmpty(result.Message) Then
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna valores al objeto, valida autorización, valida si la secuencia es manual o automática
    ''' Llama la función SaveAuthorizationPortfolio y le envía parámetros para realizar el guardado
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MAuthorizationPortfolio(Me.Tag.ToString())
                Dim result = Await model.SaveAuthorizationPortfolio(AuthorizationPortfolio, _idCurrentSequence)
                If result.StateResult Then
                    If AuthorizationPortfolio.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me.Sequense.IsManual AndAlso Not Me.Sequense.Sequential Then
                            Me.DicSequense(Me.Sequense.AuthorizationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me.Sequense.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf AuthorizationPortfolio.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.AuthorizationPortfolio = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    Me.Deshacer()
                Else
                    Me.AsyncLoader(False)
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Llama la función para guardar o actualizar dependiendo si existen o no datos
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Valida la secuencia, si es manual limpia controles 
    ''' Si todo es correcto llama a la función NewAuthorizationPortfolio
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")

            If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            Else
                Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            End If

            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewAuthorizationPortfolio()
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los detalles de las rejillas
    ''' </summary>
    Private Sub GetDetails()
        INDviewCUPS.ShowLoadingPanel()
        tokenAsyncCUPS = New CancellationTokenSource()
        Task.Factory.StartNew(Sub() GetListCUPS(), tokenAsyncCUPS.Token)

        INDviewProducts.ShowLoadingPanel()
        tokenAsyncProducts = New CancellationTokenSource()
        Task.Factory.StartNew(Sub() GetListProducts(), tokenAsyncProducts.Token)

        INDviewCareCenter.ShowLoadingPanel()
        tokenAsyncCareCenter = New CancellationTokenSource()
        Task.Factory.StartNew(Sub() GetListCareCenter(), tokenAsyncCareCenter.Token)
    End Sub

    ''' <summary>
    ''' Método que obtiene el listado de cups
    ''' </summary>
    Private Sub GetListCUPS()
        Dim result = Presenter.GetListCUPS(AuthorizationPortfolio.Id)

        If result IsNot Nothing AndAlso result.Count > 0 Then
            If ListAuthorizationPortfolioCUPSEntity Is Nothing Then
                ListAuthorizationPortfolioCUPSEntity = New List(Of AuthorizationPortfolioCUPSEntity)
            End If
            For Each item In result
                Dim entity = New AuthorizationPortfolioCUPSEntity
                entity.Id = item.Id
                entity.AuthorizationPortfolioId = item.AuthorizationPortfolioId.Id
                entity.CUPSEntityId = item.CUPSEntityId.Id
                entity.CUPSEntityDescription = item.CUPSEntityId.CodeDescription
                entity.CUPSEntityCode = item.CUPSEntityId.Code
                entity.CUPSEntityName = item.CUPSEntityId.Description
                entity.ContractDescriptionId = If(item.ContractDescriptionId Is Nothing, Nothing, item.ContractDescriptionId.Id)
                entity.ContractDescriptionDescription = If(item.ContractDescriptionId Is Nothing, Nothing, item.ContractDescriptionId.CodeName)
                entity.AuthorizationGroupId = item.AuthorizationGroupId.Id
                entity.AuthorizationGroupDescription = item.AuthorizationGroupId.CodeName
                ListAuthorizationPortfolioCUPSEntity.Add(entity)
            Next
        End If

        If Not tokenAsyncCUPS.IsCancellationRequested Then
            INDgcCUPS.SafeInvoke(Sub()
                                     INDviewCUPS.HideLoadingPanel()
                                     INDgcCUPS.DataSource = ListAuthorizationPortfolioCUPSEntity
                                     INDviewCUPS.ExpandAllGroups()
                                 End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Método que obtiene el listado de productos
    ''' </summary>
    Private Sub GetListProducts()
        Dim result = Presenter.GetListProducts(AuthorizationPortfolio.Id)

        If result IsNot Nothing AndAlso result.Count > 0 Then
            If ListAuthorizationPortfolioInventoryProduct Is Nothing Then
                ListAuthorizationPortfolioInventoryProduct = New List(Of AuthorizationPortfolioInventoryProduct)
            End If
            For Each item In result
                Dim entity = New AuthorizationPortfolioInventoryProduct
                entity.Id = item.Id
                entity.AuthorizationPortfolioId = item.AuthorizationPortfolioId.Id
                entity.InventoryProductId = item.InventoryProductId.Id
                entity.InventoryProductDescription = item.InventoryProductId.CodeName
                entity.ProductCode = item.InventoryProductId.Code
                entity.ProductName = item.InventoryProductId.Name
                entity.AuthorizationGroupId = item.AuthorizationGroupId.Id
                entity.AuthorizationGroupDescription = item.AuthorizationGroupId.CodeName
                ListAuthorizationPortfolioInventoryProduct.Add(entity)
            Next
        End If

        If Not tokenAsyncProducts.IsCancellationRequested Then
            INDgcProducts.SafeInvoke(Sub()
                                         INDviewProducts.HideLoadingPanel()
                                         INDgcProducts.DataSource = ListAuthorizationPortfolioInventoryProduct
                                         INDviewProducts.ExpandAllGroups()
                                     End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Método que obtiene el listado de centros de atención
    ''' </summary>
    Private Sub GetListCareCenter()
        Dim result = Presenter.GetListCareCenter(AuthorizationPortfolio.Id)

        If result IsNot Nothing AndAlso result.Count > 0 Then
            If ListAuthorizationPortfolioCareCenter Is Nothing Then
                ListAuthorizationPortfolioCareCenter = New List(Of AuthorizationPortfolioCareCenter)
            End If
            For Each item In result
                Dim careCenterXpo = Presenter.GetCareCenterByCode(item.CareCenterCode)

                Dim entity = New AuthorizationPortfolioCareCenter
                entity.Id = item.Id
                entity.AuthorizationPortfolioId = item.AuthorizationPortfolioId.Id
                entity.CareCenterCode = item.CareCenterCode
                entity.CareCenterDescription = If(careCenterXpo IsNot Nothing, item.CareCenterCode + " - " + careCenterXpo.NOMCENATE.Trim, item.CareCenterCode)
                ListAuthorizationPortfolioCareCenter.Add(entity)
            Next
        End If

        If Not tokenAsyncCareCenter.IsCancellationRequested Then
            INDgcCareCenter.SafeInvoke(Sub()
                                           INDviewCareCenter.HideLoadingPanel()
                                           INDgcCareCenter.DataSource = ListAuthorizationPortfolioCareCenter
                                       End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptions(optionCheck As Integer, view As GridView)
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                GetChildsRows(view, listHandlesSelected(i), optionCheck)
            Next
        End If
        If view.Name = viewGridCupsEntity.Name Then
            Dim cont = (From l In CupsEntityXPO Where l.SelectOption = True Select l).Count
            INDSleCups.Text = cont.ToString + " item seleccionado"
            INDgcCupsEntity.RefreshDataSource()
        Else
            Dim cont = (From l In ProductsXpo Where l.SelectOption = True Select l).Count
            INDsleProducts.Text = cont.ToString + " item seleccionado"
            INDgcPopupProducts.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If view.IsGroupRow(groupRowHandle) Then
            Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
            For i As Integer = 0 To childCount - 1
                Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
                If view.IsGroupRow(childHandle) Then
                    GetChildsRows(view, childHandle, optionCheck)
                Else
                    Dim row As Object = view.GetRow(childHandle)
                    SetCheck(optionCheck, childHandle, view)
                End If
            Next
        Else
            SetCheck(optionCheck, groupRowHandle, view)
        End If
    End Sub

    ''' <summary>
    ''' Asigna el check
    ''' </summary>
    Private Sub SetCheck(optionCheck As Integer, handle As Integer, view As GridView)
        Dim row As Object = view.GetRow(handle)
        If optionCheck = 0 Then
            row.SelectOption = False
        Else
            row.SelectOption = True
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los childsRowHandles
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function

    ''' <summary>
    ''' metodo para abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAuthorizationPortfolio
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NamePortfolio = String.Empty
        Status = True
        INDSleCups.EditValue = Nothing
        INDsleProducts.EditValue = Nothing
        INDsleCareCenter.EditValue = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        AuthorizationPortfolio = Nothing
        INDgcCUPS.DataSource = Nothing
        INDgcProducts.DataSource = Nothing
        INDgcCareCenter.DataSource = Nothing
        SelectOrUnSelectAllGroup(False)
        ListAuthorizationPortfolioCUPSEntity = Nothing
        ListDeleteAuthorizationPortfolioCUPSEntity = Nothing
        ListAuthorizationPortfolioInventoryProduct = Nothing
        ListDeleteAuthorizationPortfolioInventoryProduct = Nothing
        ListAuthorizationPortfolioCareCenter = Nothing
        ListDeleteAuthorizationPortfolioCareCenter = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDlyRoot.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(MyTag)
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(MyTag, Me.AuthorizationPortfolio.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordAuthorization With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.AuthorizationPortfolio.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.AuthorizationPortfolio.Code, Me.AuthorizationPortfolio.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.AuthorizationPortfolio.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.AuthorizationPortfolio.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.AuthorizationPortfolio.Code, Me.AuthorizationPortfolio.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.AuthorizationPortfolio.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewAuthorizationPortfolio() As Task
        AuthorizationPortfolio = New AuthorizationPortfolio With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.AuthorizationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.AuthorizationSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.AuthorizationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
            Try
                Using model As New MAuthorizationPortfolio(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.AuthorizationPortfolio.Status
                    Dim Result = Await model.ChangeState(Code, state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me.AuthorizationPortfolio = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' metodo para cargar los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MAuthorizationPortfolio(CStr(Me.Tag))
                AsyncLoader(True)
                AuthorizationPortfolio = (Await Model.GetAuthorizationPortfolio(INDbtnCode.Text.Trim)).ObjectEmbbeded
                If AuthorizationPortfolio IsNot Nothing AndAlso AuthorizationPortfolio.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(AuthorizationPortfolio.Id))
                        With AuthorizationPortfolio
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            NamePortfolio = .Name
                            Status = .Status

                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.AuthorizationPortfolio.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordAuthorization With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = AuthorizationPortfolio.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(AuthorizationPortfolio.Id)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                    GetDetails()
                Else
                    AsyncLoader(False)
                    If Me.Sequense.IsManual Then
                        Await Me.NewAuthorizationPortfolio()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
            End Using
        End If
    End Function

    ''' <summary>
    ''' asiganar valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With AuthorizationPortfolio
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .Code = Code
            .Name = NamePortfolio

            .AuthorizationPortfolioCUPSEntity.Clear()
            .AuthorizationPortfolioInventoryProduct.Clear()
            .AuthorizationPortfolioCareCenter.Clear()

            If ListAuthorizationPortfolioCUPSEntity IsNot Nothing AndAlso ListAuthorizationPortfolioCUPSEntity.Count > 0 Then
                ListAuthorizationPortfolioCUPSEntity.ForEach(Sub(item) .AuthorizationPortfolioCUPSEntity.Add(item))
            End If

            If ListDeleteAuthorizationPortfolioCUPSEntity IsNot Nothing AndAlso ListDeleteAuthorizationPortfolioCUPSEntity.Count > 0 Then
                ListDeleteAuthorizationPortfolioCUPSEntity.ForEach(Sub(item) .AuthorizationPortfolioCUPSEntity.Add(item))
            End If

            If ListAuthorizationPortfolioInventoryProduct IsNot Nothing AndAlso ListAuthorizationPortfolioInventoryProduct.Count > 0 Then
                ListAuthorizationPortfolioInventoryProduct.ForEach(Sub(item) .AuthorizationPortfolioInventoryProduct.Add(item))
            End If

            If ListDeleteAuthorizationPortfolioInventoryProduct IsNot Nothing AndAlso ListDeleteAuthorizationPortfolioInventoryProduct.Count > 0 Then
                ListDeleteAuthorizationPortfolioInventoryProduct.ForEach(Sub(item) .AuthorizationPortfolioInventoryProduct.Add(item))
            End If

            If ListAuthorizationPortfolioCareCenter IsNot Nothing AndAlso ListAuthorizationPortfolioCareCenter.Count > 0 Then
                ListAuthorizationPortfolioCareCenter.ForEach(Sub(item) .AuthorizationPortfolioCareCenter.Add(item))
            End If

            If ListDeleteAuthorizationPortfolioCareCenter IsNot Nothing AndAlso ListDeleteAuthorizationPortfolioCareCenter.Count > 0 Then
                ListDeleteAuthorizationPortfolioCareCenter.ForEach(Sub(item) .AuthorizationPortfolioCareCenter.Add(item))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el detalle de la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAgregatedCUPS()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim rowSelectCount = INDviewCUPS.SelectedRowsCount
            If rowSelectCount > 1 Then
                Dim ListRows As New List(Of Object)
                For i = 0 To rowSelectCount - 1
                    If INDviewCUPS.GetSelectedRows()(i) >= 0 Then
                        Dim row As AuthorizationPortfolioCUPSEntity = INDviewCUPS.GetRow(INDviewCUPS.GetSelectedRows()(i))
                        ListRows.Add(row)
                        If row.Id > 0 Then
                            If ListDeleteAuthorizationPortfolioCUPSEntity Is Nothing Then
                                ListDeleteAuthorizationPortfolioCUPSEntity = New List(Of AuthorizationPortfolioCUPSEntity)
                            End If
                            row.MarkAsDeleted()
                            ListDeleteAuthorizationPortfolioCUPSEntity.Add(row)
                        End If
                    End If
                Next
                ListRows.ForEach(Sub(item) ListAuthorizationPortfolioCUPSEntity.Remove(item))
            Else
                Dim entity = DirectCast(INDviewCUPS.GetFocusedRow, AuthorizationPortfolioCUPSEntity)
                ListAuthorizationPortfolioCUPSEntity.Remove(entity)
                If entity.Id > 0 Then
                    If ListDeleteAuthorizationPortfolioCUPSEntity Is Nothing Then
                        ListDeleteAuthorizationPortfolioCUPSEntity = New List(Of AuthorizationPortfolioCUPSEntity)
                    End If
                    entity.MarkAsDeleted()
                    ListDeleteAuthorizationPortfolioCUPSEntity.Add(entity)
                End If
            End If

            INDgcCUPS.DataSource = Nothing
            INDgcCUPS.DataSource = ListAuthorizationPortfolioCUPSEntity
            INDviewCUPS.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el detalle de la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAgregatedProducts()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim rowSelectCount = INDviewProducts.SelectedRowsCount
            If rowSelectCount > 1 Then
                Dim ListRows As New List(Of Object)
                For i = 0 To rowSelectCount - 1
                    If INDviewProducts.GetSelectedRows()(i) >= 0 Then
                        Dim row As AuthorizationPortfolioInventoryProduct = INDviewProducts.GetRow(INDviewProducts.GetSelectedRows()(i))
                        ListRows.Add(row)
                        If row.Id > 0 Then
                            If ListDeleteAuthorizationPortfolioInventoryProduct Is Nothing Then
                                ListDeleteAuthorizationPortfolioInventoryProduct = New List(Of AuthorizationPortfolioInventoryProduct)
                            End If
                            row.MarkAsDeleted()
                            ListDeleteAuthorizationPortfolioInventoryProduct.Add(row)
                        End If
                    End If
                Next
                ListRows.ForEach(Sub(item) ListAuthorizationPortfolioInventoryProduct.Remove(item))
            Else
                Dim entity = DirectCast(INDviewProducts.GetFocusedRow, AuthorizationPortfolioInventoryProduct)
                ListAuthorizationPortfolioInventoryProduct.Remove(entity)
                If entity.Id > 0 Then
                    If ListDeleteAuthorizationPortfolioInventoryProduct Is Nothing Then
                        ListDeleteAuthorizationPortfolioInventoryProduct = New List(Of AuthorizationPortfolioInventoryProduct)
                    End If
                    entity.MarkAsDeleted()
                    ListDeleteAuthorizationPortfolioInventoryProduct.Add(entity)
                End If
            End If

            INDgcProducts.DataSource = Nothing
            INDgcProducts.DataSource = ListAuthorizationPortfolioInventoryProduct
            INDviewProducts.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el detalle de la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAgregatedCareCenter()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim rowSelectCount = INDviewCareCenter.SelectedRowsCount
            If rowSelectCount > 1 Then
                Dim ListRows As New List(Of Object)
                For i = 0 To rowSelectCount - 1
                    If INDviewCareCenter.GetSelectedRows()(i) >= 0 Then
                        Dim row As AuthorizationPortfolioCareCenter = INDviewCareCenter.GetRow(INDviewCareCenter.GetSelectedRows()(i))
                        ListRows.Add(row)
                        If row.Id > 0 Then
                            If ListDeleteAuthorizationPortfolioCareCenter Is Nothing Then
                                ListDeleteAuthorizationPortfolioCareCenter = New List(Of AuthorizationPortfolioCareCenter)
                            End If
                            row.MarkAsDeleted()
                            ListDeleteAuthorizationPortfolioCareCenter.Add(row)
                        End If
                    End If
                Next
                ListRows.ForEach(Sub(item) ListAuthorizationPortfolioCareCenter.Remove(item))
            Else
                Dim entity = DirectCast(INDviewCareCenter.GetFocusedRow, AuthorizationPortfolioCareCenter)
                ListAuthorizationPortfolioCareCenter.Remove(entity)
                If entity.Id > 0 Then
                    If ListDeleteAuthorizationPortfolioCareCenter Is Nothing Then
                        ListDeleteAuthorizationPortfolioCareCenter = New List(Of AuthorizationPortfolioCareCenter)
                    End If
                    entity.MarkAsDeleted()
                    ListDeleteAuthorizationPortfolioCareCenter.Add(entity)
                End If
            End If

            INDgcCareCenter.DataSource = Nothing
            INDgcCareCenter.DataSource = ListAuthorizationPortfolioCareCenter
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
    End Sub

    Private Sub FrmAuthorizationPortfolio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        'Me.BarraBotones.OperatingUnitVisible = False
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAuthorizationPortfolio(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()

        IndigoGridControl1.RefreshGrid(INDgcCUPS)
        IndigoGridControl2.RefreshGrid(INDgcProducts)
        IndigoGridControl3.RefreshGrid(INDgcCareCenter)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewCUPS, ListActions)
        IndigoGridView2.SetListAcction(INDviewProducts, ListActions)
        IndigoGridView4.SetListAcction(INDviewCareCenter, ListActions)

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.CheckOptions)
        ListActions2.Add(eAcciones.UnCheckOptions)
        IndigoGridView3.SetListAcction(viewGridProducts, ListActions2)

        INDEsbExportCUPS.AddRangeColumns("Código Grupo", "Código CUPS", "Código Descripción")
        INDEsbExportProducts.AddRangeColumns("Código Grupo", "Código Producto")

        viewGridProducts.Columns.ColumnByName("colActions").Visible = False
        viewGridProducts.Columns.ColumnByName("colActions").VisibleIndex = -1

        INDviewCUPS.Columns.ColumnByName("colActions").Width = 80
        INDviewProducts.Columns.ColumnByName("colActions").Width = 80
        INDviewCareCenter.Columns.ColumnByName("colActions").Width = 80
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmAuthorizationPortfolio_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsyncCUPS IsNot Nothing Then
            tokenAsyncCUPS.Cancel()
        End If
        If tokenAsyncProducts IsNot Nothing Then
            tokenAsyncProducts.Cancel()
        End If
        If tokenAsyncCareCenter IsNot Nothing Then
            tokenAsyncCareCenter.Cancel()
        End If
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceAddProducts_Popup(sender As Object, e As EventArgs) Handles INDpceAddProducts.Popup
        INDsleProducts.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara despues de desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddCups_Popup(sender As Object, e As EventArgs) Handles INDpceAddCups.Popup
        INDSleCups.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDpceAddProducts_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddProducts.KeyDown
        If e.KeyCode = Keys.F4 OrElse e.KeyCode = Keys.Enter Then
            INDpceAddProducts.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddCups_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddCups.KeyDown
        If e.KeyCode = Keys.F4 OrElse e.KeyCode = Keys.Enter Then
            INDpceAddCups.ShowPopup()
        End If
    End Sub

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewAuthorizationPortfolio()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            INDsleCareCenter.Properties.DataSource = Presenter.InitializeCentersHIS()
        End If
    End Sub

    Private Sub INDsleProducts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProducts.QueryPopUp
        If ProductsXpo Is Nothing Then
            ProductsXpo = Presenter.InitializeProducts()
        End If
    End Sub

    Private Sub INDsleAuthorizationGroupProducts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAuthorizationGroupProducts.QueryPopUp
        If INDsleAuthorizationGroupProducts.Properties.DataSource Is Nothing Then
            INDsleAuthorizationGroupProducts.Properties.DataSource = Presenter.InitializeAuthorizationGroup()
        End If
    End Sub

    Private Sub INDsleAuthorizationGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAuthorizationGroup.QueryPopUp
        If INDsleAuthorizationGroup.Properties.DataSource Is Nothing Then
            INDsleAuthorizationGroup.Properties.DataSource = Presenter.InitializeAuthorizationGroup()
        End If
    End Sub

    Private Sub INDSleCups_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCups.QueryPopUp
        If CupsEntityXPO Is Nothing Then
            CupsEntityXPO = Presenter.InitializeCUPS()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddCareCenter_Click(sender As Object, e As EventArgs) Handles INDbtnAddCareCenter.Click
        If INDsleCareCenter.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDsleCareCenter.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un centro de atención"
            Exit Sub
        End If

        If ListAuthorizationPortfolioCareCenter Is Nothing Then
            ListAuthorizationPortfolioCareCenter = New List(Of AuthorizationPortfolioCareCenter)
        End If

        Dim temp = ListAuthorizationPortfolioCareCenter.Where(Function(x) x.CareCenterCode = INDsleCareCenter.EditValue).FirstOrDefault()
        If temp IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ItemDuplicated", "Contract"), INDsleCareCenter.Text)
            Exit Sub
        End If

        Dim entity = New AuthorizationPortfolioCareCenter
        entity.CareCenterCode = INDsleCareCenter.EditValue
        entity.CareCenterDescription = INDsleCareCenter.Text
        ListAuthorizationPortfolioCareCenter.Add(entity)

        INDgcCareCenter.DataSource = Nothing
        INDgcCareCenter.DataSource = ListAuthorizationPortfolioCareCenter
        INDsleCareCenter.EditValue = Nothing
        INDsleCareCenter.Focus()
    End Sub

    Private Sub INDBtnAddCups_Click(sender As Object, e As EventArgs) Handles INDBtnAddCups.Click
        Dim errorsControls As New StringBuilder

        If CupsEntityXPO IsNot Nothing Then
            Dim cont = (From l In CupsEntityXPO Where l.SelectOption = True Select l).Count
            If cont = 0 Then
                errorsControls.AppendLine("Seleccione al menos un item de entidad CUPS")
            End If
        Else
            errorsControls.AppendLine("Seleccione al menos un item de entidad CUPS")
        End If

        If INDsleAuthorizationGroup.EditValue Is Nothing Then
            errorsControls.AppendLine("Seleccione un grupo de autorización")
        End If

        If errorsControls.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorsControls.ToString()
            Exit Sub
        End If

        Dim listSelectEntity = (From l In CupsEntityXPO Where l.SelectOption = True Select l).ToList
        Dim listErrors As StringBuilder

        If ListAuthorizationPortfolioCUPSEntity Is Nothing Then
            ListAuthorizationPortfolioCUPSEntity = New List(Of AuthorizationPortfolioCUPSEntity)
        End If

        'Se valida que los cups seleccionados tengan descripciones asociadas
        Dim listIds = (From x As CupsEntityXpo In listSelectEntity Select x.Id).ToList()
        If Presenter.CountDescriptions(listIds) > 0 Then
            Me.Cursor = ChangeCursorIndigo()
            Using Formulario As New FrmSelectCupsDescription()
                Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 600
                Formulario.Height = 500
                Formulario.ListCupsIds = listIds
                Dim frm As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)

                If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                    INDpceAddCups.ShowPopup()
                    Exit Sub
                End If

                If Formulario.ListCupsEntityWithDescriptionsXpo IsNot Nothing AndAlso Formulario.ListCupsEntityWithDescriptionsXpo.Count > 0 Then
                    listErrors = New StringBuilder

                    For Each item In Formulario.ListCupsEntityWithDescriptionsXpo
                        Dim cupsTmp = ListAuthorizationPortfolioCUPSEntity.Where(Function(x) x.CUPSEntityId = item.CUPSEntityId AndAlso x.ContractDescriptionId IsNot Nothing AndAlso x.ContractDescriptionId = item.ContractDescriptionId).FirstOrDefault()
                        If cupsTmp IsNot Nothing Then
                            listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", "Contract"), item.CUPSEntityCodeName))
                        End If
                    Next
                    If listErrors.Length > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                        Exit Sub
                    End If

                    For Each item In Formulario.ListCupsEntityWithDescriptionsXpo
                        Dim entity = New AuthorizationPortfolioCUPSEntity
                        entity.CUPSEntityId = item.CUPSEntityId
                        entity.CUPSEntityDescription = item.CUPSEntityCodeName
                        entity.CUPSEntityCode = item.CUPSEntityCode
                        entity.CUPSEntityName = item.CUPSEntityName
                        entity.ContractDescriptionId = item.ContractDescriptionId
                        entity.ContractDescriptionDescription = item.ContractDescriptionCodeName
                        entity.AuthorizationGroupId = INDsleAuthorizationGroup.EditValue
                        entity.AuthorizationGroupDescription = INDsleAuthorizationGroup.Text
                        ListAuthorizationPortfolioCUPSEntity.Add(entity)
                    Next
                End If
            End Using
        End If

        If listSelectEntity IsNot Nothing AndAlso listSelectEntity.Count > 0 Then
            listErrors = New StringBuilder
            For Each itemXpo In listSelectEntity
                Dim cupsTmp = ListAuthorizationPortfolioCUPSEntity.Where(Function(x) x.CUPSEntityId = itemXpo.Id AndAlso x.ContractDescriptionId Is Nothing).FirstOrDefault()
                If cupsTmp IsNot Nothing Then
                    listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", "Contract"), itemXpo.CodeDescription))
                End If
            Next
            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Exit Sub
            End If

            For Each itemXpo In listSelectEntity
                Dim entity = New AuthorizationPortfolioCUPSEntity
                entity.CUPSEntityId = itemXpo.Id
                entity.CUPSEntityDescription = itemXpo.CodeDescription
                entity.CUPSEntityCode = itemXpo.Code
                entity.CUPSEntityName = itemXpo.Description
                entity.AuthorizationGroupId = INDsleAuthorizationGroup.EditValue
                entity.AuthorizationGroupDescription = INDsleAuthorizationGroup.Text
                ListAuthorizationPortfolioCUPSEntity.Add(entity)
            Next
        End If

        INDgcCUPS.DataSource = Nothing
        INDgcCUPS.DataSource = ListAuthorizationPortfolioCUPSEntity
        INDviewCUPS.ExpandAllGroups()
        INDSleCups.Text = "0 item seleccionado"
        CupsEntityXPO = Nothing
        INDsleAuthorizationGroup.EditValue = Nothing
        INDSleCups.Focus()
    End Sub

    Private Sub INDbtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDbtnAddProducts.Click
        Dim errorsControls As New StringBuilder

        If ProductsXpo IsNot Nothing Then
            Dim cont = (From l In ProductsXpo Where l.SelectOption = True Select l).Count
            If cont = 0 Then
                errorsControls.AppendLine("Seleccione al menos un item de productos")
            End If
        Else
            errorsControls.AppendLine("Seleccione al menos un item de productos")
        End If

        If INDsleAuthorizationGroupProducts.EditValue Is Nothing Then
            errorsControls.AppendLine("Seleccione un grupo de autorización")
        End If

        If errorsControls.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorsControls.ToString()
            Exit Sub
        End If

        Dim listSelectEntity = (From l In ProductsXpo Where l.SelectOption = True Select l).ToList
        Dim listErrors As StringBuilder

        If ListAuthorizationPortfolioInventoryProduct Is Nothing Then
            ListAuthorizationPortfolioInventoryProduct = New List(Of AuthorizationPortfolioInventoryProduct)
        End If

        If listSelectEntity IsNot Nothing AndAlso listSelectEntity.Count > 0 Then
            listErrors = New StringBuilder
            For Each itemXpo In listSelectEntity
                Dim cupsTmp = ListAuthorizationPortfolioInventoryProduct.Where(Function(x) x.InventoryProductId = itemXpo.Id).FirstOrDefault()
                If cupsTmp IsNot Nothing Then
                    listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", "Contract"), itemXpo.CodeName))
                End If
            Next
            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Exit Sub
            End If

            For Each itemXpo In listSelectEntity
                Dim entity = New AuthorizationPortfolioInventoryProduct
                entity.InventoryProductId = itemXpo.Id
                entity.InventoryProductDescription = itemXpo.CodeName
                entity.ProductCode = itemXpo.Code
                entity.ProductName = itemXpo.Name
                entity.AuthorizationGroupId = INDsleAuthorizationGroupProducts.EditValue
                entity.AuthorizationGroupDescription = INDsleAuthorizationGroupProducts.Text
                ListAuthorizationPortfolioInventoryProduct.Add(entity)
            Next
        End If

        INDgcProducts.DataSource = Nothing
        INDgcProducts.DataSource = ListAuthorizationPortfolioInventoryProduct
        INDviewProducts.ExpandAllGroups()
        INDsleProducts.Text = "0 item seleccionado"
        ProductsXpo = Nothing
        INDsleAuthorizationGroupProducts.EditValue = Nothing
        INDsleProducts.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleAuthorizationGroupProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAuthorizationGroupProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2129, Nothing, True)
            INDsleAuthorizationGroupProducts.Properties.DataSource = Presenter.InitializeAuthorizationGroup()
        End If
    End Sub

    Private Sub INDsleAuthorizationGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAuthorizationGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2129, Nothing, True)
            INDsleAuthorizationGroup.Properties.DataSource = Presenter.InitializeAuthorizationGroup()
        End If
    End Sub

    Private Sub INDSleCups_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCupsEntity
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                CupsEntityXPO = Presenter.InitializeCUPS()
                INDSleCups.Focus()
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e.NewValue Then
            Dim cont = (From x In CupsEntityXPO Where x.SelectOption = True Select x).Count
            cont = cont + 1
            INDSleCups.Text = cont.ToString + " item seleccionado"
        Else
            Dim cont = (From x In CupsEntityXPO Where x.SelectOption = True Select x).Count
            If cont > 0 Then
                cont = cont - 1
            End If
            INDSleCups.Text = cont.ToString + " item seleccionado"
        End If
    End Sub

    Private Sub INDrptCheckOptionProducts_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrptCheckOptionProducts.EditValueChanging
        If e.NewValue Then
            Dim cont = (From x In ProductsXpo Where x.SelectOption = True Select x).Count
            cont = cont + 1
            INDsleProducts.Text = cont.ToString + " item seleccionado"
        Else
            Dim cont = (From x In ProductsXpo Where x.SelectOption = True Select x).Count
            If cont > 0 Then
                cont = cont - 1
            End If
            INDsleProducts.Text = cont.ToString + " item seleccionado"
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub viewGridCupsEntity_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles viewGridCupsEntity.PopupMenuShowing
        ShowMenuInGrid(sender, e)
    End Sub

    ''' <summary>
    ''' Método que muestra el menu dependiendo de la rejilla
    ''' </summary>
    Private Sub ShowMenuInGrid(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        Dim View = CType(sender, GridView)

        INDbarButtonSelectAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        INDbarButtonUnSelectAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        PopupMenuActions.Manager = BarManager
        PopupMenuActions.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Seleccionar todo el grupo o subGrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        SelectOptions(1, viewGridCupsEntity)
    End Sub

    ''' <summary>
    ''' Quitar seleccion de todo el grupo o subGrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        SelectOptions(0, viewGridCupsEntity)
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todos los grupos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOrUnSelectAllGroup(selectOption As Boolean)
        If CupsEntityXPO IsNot Nothing AndAlso CupsEntityXPO.Count > 0 Then
            For Each item In CupsEntityXPO
                item.SelectOption = selectOption
            Next
            Dim cont = (From l In CupsEntityXPO Where l.SelectOption = True Select l).Count
            INDSleCups.Text = cont.ToString + " item seleccionado"
            INDgcCupsEntity.RefreshDataSource()
        End If
        If ProductsXpo IsNot Nothing AndAlso ProductsXpo.Count > 0 Then
            For Each item In ProductsXpo
                item.SelectOption = selectOption
            Next
            Dim cont = (From l In ProductsXpo Where l.SelectOption = True Select l).Count
            INDsleProducts.Text = cont.ToString + " item seleccionado"
            INDgcPopupProducts.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.AuthorizationPortfolio IsNot Nothing AndAlso Me.AuthorizationPortfolio.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDbtnCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub
#End Region

#Region "PasteToGrid"

#Region "CUPS"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGridCUPS(sender, e.Rows)
    End Sub

    Private Async Function PasteToGridCUPS(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDgcCUPS.Name Then
            INDviewCUPS.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MAuthorizationPortfolio(MyTag)
                Dim result = Await model.CopyAndPasteAuthorizationPortfolioCUPSEntity(ListInfo)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDviewCUPS.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If ListAuthorizationPortfolioCUPSEntity IsNot Nothing AndAlso ListAuthorizationPortfolioCUPSEntity.Count > 0 Then
                        For Each item In result.ObjectEmbbeded
                            Dim share = ListAuthorizationPortfolioCUPSEntity.Where(Function(x) x.CUPSEntityId = item.CUPSEntityId AndAlso (If(x.ContractDescriptionId Is Nothing, 0, x.ContractDescriptionId) = If(item.ContractDescriptionId Is Nothing, 0, item.ContractDescriptionId))).FirstOrDefault()
                            If share IsNot Nothing Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El CUPS " + share.CUPSEntityDescription + " ya existe en la lista", 2))
                            Else
                                ListAuthorizationPortfolioCUPSEntity.Add(item)
                            End If
                        Next
                    Else
                        ListAuthorizationPortfolioCUPSEntity = result.ObjectEmbbeded
                    End If
                End If
                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDgcCUPS.DataSource = Nothing
            INDgcCUPS.DataSource = ListAuthorizationPortfolioCUPSEntity
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDviewCUPS.HideLoadingPanel()
            INDviewCUPS.ExpandAllGroups()
        End If
    End Function

#End Region

#Region "Product"

    Private Async Sub IndigoGridControl2_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl2.PasteToGrid
        Await PasteToGridProduct(sender, e.Rows)
    End Sub

    Private Async Function PasteToGridProduct(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDgcProducts.Name Then
            INDviewProducts.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MAuthorizationPortfolio(MyTag)
                Dim result = Await model.CopyAndPasteAuthorizationPortfolioInventoryProduct(ListInfo)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDviewProducts.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If ListAuthorizationPortfolioInventoryProduct IsNot Nothing AndAlso ListAuthorizationPortfolioInventoryProduct.Count > 0 Then
                        Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.InventoryProductId).ToList.Distinct.ToList()
                        Dim listBillsNotExist As New List(Of String)
                        For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
                            Dim share = ListAuthorizationPortfolioInventoryProduct.Where(Function(x) x.InventoryProductId = listBillsTmp.Item(i)).FirstOrDefault()
                            If share IsNot Nothing Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El Producto " + share.InventoryProductDescription + " ya existe en la lista", 2))
                            Else
                                listBillsNotExist.Add(listBillsTmp.Item((i)))
                            End If
                        Next
                        For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
                            ListAuthorizationPortfolioInventoryProduct.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.InventoryProductId = listBillsNotExist.Item(i)))
                        Next

                    Else
                        ListAuthorizationPortfolioInventoryProduct = result.ObjectEmbbeded
                    End If
                End If
                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDgcProducts.DataSource = Nothing
            INDgcProducts.DataSource = ListAuthorizationPortfolioInventoryProduct
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDviewProducts.HideLoadingPanel()
            INDviewProducts.ExpandAllGroups()
        End If
    End Function

#End Region

#Region "CareCenter"

    Private Async Sub IndigoGridControl3_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl3.PasteToGrid
        Await PasteToGridCareCenter(sender, e.Rows)
    End Sub

    Private Async Function PasteToGridCareCenter(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDgcCareCenter.Name Then
            INDviewCareCenter.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MAuthorizationPortfolio(MyTag)
                Dim result = Await model.CopyAndPasteAuthorizationPortfolioCareCenter(ListInfo)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDviewCareCenter.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If ListAuthorizationPortfolioCareCenter IsNot Nothing AndAlso ListAuthorizationPortfolioCareCenter.Count > 0 Then
                        Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.CareCenterCode).ToList.Distinct.ToList()
                        Dim listBillsNotExist As New List(Of String)
                        For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
                            Dim share = ListAuthorizationPortfolioCareCenter.Where(Function(x) x.CareCenterCode = listBillsTmp.Item(i)).FirstOrDefault()
                            If share IsNot Nothing Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El centro atención " + share.CareCenterDescription + " ya existe en la lista", 2))
                            Else
                                listBillsNotExist.Add(listBillsTmp.Item((i)))
                            End If
                        Next
                        For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
                            ListAuthorizationPortfolioCareCenter.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.CareCenterCode = listBillsNotExist.Item(i)))
                        Next

                    Else
                        ListAuthorizationPortfolioCareCenter = result.ObjectEmbbeded
                    End If
                End If
                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDgcCareCenter.DataSource = Nothing
            INDgcCareCenter.DataSource = ListAuthorizationPortfolioCareCenter
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDviewCareCenter.HideLoadingPanel()
        End If
    End Function

#End Region

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteAgregatedCUPS()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteAgregatedCUPS()
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        DeleteAgregatedProducts()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        DeleteAgregatedProducts()
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "CheckOptions"
                SelectOptions(1, viewGridProducts)
            Case "UnCheckOptions"
                SelectOptions(0, viewGridProducts)
        End Select
    End Sub

    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "CheckOptions"
                SelectOptions(1, viewGridProducts)
            Case "UnCheckOptions"
                SelectOptions(0, viewGridProducts)
        End Select
    End Sub

    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction
        DeleteAgregatedCareCenter()
    End Sub

    Private Sub IndigoGridView4_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView4.ContexMenuActions
        DeleteAgregatedCareCenter()
    End Sub

#End Region

#End Region

#Region "Bar BUttons"

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
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
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
    ''' Cambia el Id del operador  
    ''' revisa la secuencia
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.AuthorizationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.AuthorizationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class