'***********************************************************************
' Assembly         : Presentacion.Porfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 17/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.CrossThreadExtentions
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports System.Text
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports DevExpress.Xpo
Imports Presentation.Glosas.MVP
Imports DevExpress.Spreadsheet
Imports System.IO
Imports DevExpress.XtraSpreadsheet
Imports Presentation.Controls.MVP
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

#End Region

Public Class FrmInitialBalancePortfolio
    Implements IPortfolioInitialBalance, ICustomizableForm


#Region "GLOBALS"
    Private Const MODULE_NAME As String = "Portfolio"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio
    ''' <summary>
    ''' presenter de saldos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PPortfolioInitialBalance
    ''' <summary>
    ''' representa la entidad de saldos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    Dim portfolioInitialBalance As PortfolioInitialBalance
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' bandera para saber si se esta editando en el popup de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private popupEditMode As Boolean
    ''' <summary>
    ''' representa la entidad donde se relacionan el saldo inicial con el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private portfolioInitialBalanceAdvance As PortfolioInitialBalanceAdvance
    ''' <summary>
    ''' lista de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAdvance As List(Of PortfolioInitialBalanceAdvance)
    ''' <summary>
    ''' lista de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAdvanceDelete As List(Of PortfolioInitialBalanceAdvance)
    ''' <summary>
    ''' representa la entidad donde se relacionan las facturas con el saldo inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private portfolioInitialBalanceAccountReceivable As PortfolioInitialBalanceAccountReceivable
    ''' <summary>
    ''' listado de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAccountReceivable As List(Of PortfolioInitialBalanceAccountReceivable)
    ''' <summary>
    ''' listado de facturas a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private listPortfolioInitialBalanceAccountReceivableDelete As List(Of PortfolioInitialBalanceAccountReceivable)
    ''' <summary>
    ''' bandera para saber si se cambio la cuenta en el modo edicion del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private AcconunValueChangeEditMode As Boolean
    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress
    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer
    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer = 0
    ''' <summary>
    ''' datasource para cargar la rejilla cuando se hizo desde importar 
    ''' </summary>
    ''' <remarks></remarks>
    Dim datasourceImporFile As List(Of PortfolioInitialBalanceAccountReceivableXpo) = New List(Of PortfolioInitialBalanceAccountReceivableXpo)
    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing
    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String)
    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 800
    ''' <summary>
    ''' id de la cabecera del saldo inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim portfolioInitialBalanceId As Integer = 0
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' datasource de centro de costo
    ''' </summary>
    Public Property CostCenterXPO As XPInstantFeedbackSource Implements IPortfolioInitialBalance.CostCenterXPO
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de clientes
    ''' </summary>
    Public Property CustomerXPO As XPInstantFeedbackSource Implements IPortfolioInitialBalance.CustomerXPO
        Get
            Return CType(INDSleCustomer.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCustomer.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de cuenta contable
    ''' </summary>
    Public Property MainAccountXPO As XPInstantFeedbackSource Implements IPortfolioInitialBalance.MainAccountXPO
        Get
            Return CType(INDSleAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccount.Properties.DataSource = value
        End Set
    End Property
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioInitialBalance.ActionsOnControls
        Set(value As Boolean)
            INDLcInitialBalance.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDteInitialBalance.Enabled = value
            INDMeObservationInitialBalance.Enabled = value
            INDPceAdvance.Enabled = value
            INDGcAdvance.Enabled = value
            INDBtnAddBills.Enabled = value
            INDEsbBills.Enabled = value
            INDBtnImportFile.Enabled = False
            INDGcBills.Enabled = value
            INDEsbAdvance.Enabled = value
            If INDBtnCode.Enabled = True Then
                INDBtnCode.Focus()
            Else
                INDDteInitialBalance.Focus()
            End If
            INDLcInitialBalance.EndUpdate()
        End Set
    End Property

    Public Property Code As String Implements IPortfolioInitialBalance.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    Public Property DocumentDate As Date? Implements IPortfolioInitialBalance.DocumentDate
        Get
            Return INDDteInitialBalance.EditValue
        End Get
        Set(value As Date?)
            INDDteInitialBalance.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPortfolioInitialBalance.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IPortfolioInitialBalance.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Observations As String Implements IPortfolioInitialBalance.Observations
        Get
            Return INDMeObservationInitialBalance.Text
        End Get
        Set(value As String)
            INDMeObservationInitialBalance.Text = value
        End Set
    End Property

    Public Property Sequence As PortfolioSequence Implements IPortfolioInitialBalance.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property


#End Region

#Region "CRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        CleanControlsPopupAdvance()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Me.portfolioInitialBalance IsNot Nothing AndAlso portfolioInitialBalance.Status < 3 Then
            Dim errors = ValidateControlsInitialBalance()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MPortfolioInitialBalance(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SavePortfolioInitialBalance(portfolioInitialBalance, _idCurrentSequence)
                If result.StateResult = True Then
                    portfolioInitialBalance = result.ObjectEmbbeded
                    Dim actionMessage As String = String.Empty
                    If portfolioInitialBalance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), portfolioInitialBalance.Code)
                    Else
                        If portfolioInitialBalance.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Private Async Sub Confirmar()
        Dim resultOption As DialogResult
        If portfolioInitialBalance.ChangeTracker.State = ObjectState.Unchanged Then
            resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo)
        Else
            resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageWithUpdate"), MessageType.Question, Me.Text, Botones.SiNo)
        End If
        If resultOption = System.Windows.Forms.DialogResult.Yes Then
            Try
                Using model As New MPortfolioInitialBalance(MyTag)
                    AsyncLoader(True)
                    Dim result = Await model.ConfirmPortfolioInitialBalance(Me.portfolioInitialBalance.Id)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("Confirm", MODULE_NAME), result.Message)
                        Me.Deshacer()
                    Else
                        INDBtnCode.Enabled = False
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Sub

    Public Async Sub SaveAndConfirm()
        Dim errors = ValidateControlsInitialBalance()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Using model As New MPortfolioInitialBalance(MyTag)
            If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                AssigningValues()
                Try
                    AsyncLoader(True)
                    Dim result = Await model.SaveAndConfirmPortfolioInitialBalance(portfolioInitialBalance, _idCurrentSequence)
                    AsyncLoader(False)
                    If result.StateResult = True And result.StateResultAux = True Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    ElseIf result.StateResult = True And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPorfolioInitialBalance()
        End If
    End Sub

#End Region

#Region "METHODS"
    Private Function ValidateControlsInitialBalance() As String
        Dim errors As New StringBuilder
        If DocumentDate Is Nothing Then
            errors.AppendLine(INDLciDate.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDGvBills.RowCount = 0 And INDGvAdvance.RowCount = 0 Then
            errors.AppendLine(ResourceManager.GetString("BillOrAdvance", MODULE_NAME))
        End If
        Return errors.ToString()
    End Function

    Private Sub AssigningValues()
        With Me.portfolioInitialBalance
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .Observations = Observations
            .OperatingUnitId = Me._idOperativeUnit
            .Status = 1
            If Me.listPortfolioInitialBalanceAccountReceivable IsNot Nothing AndAlso Me.listPortfolioInitialBalanceAccountReceivable.Count > 0 Then
                For Each item In listPortfolioInitialBalanceAccountReceivable
                    .PortfolioInitialBalanceAccountReceivable.Add(item)
                Next
            End If
            If Me.listPortfolioInitialBalanceAccountReceivableDelete IsNot Nothing AndAlso Me.listPortfolioInitialBalanceAccountReceivableDelete.Count > 0 Then
                For Each item In listPortfolioInitialBalanceAccountReceivableDelete
                    While item.PortfolioInitialBalanceAccountReceivableAccounting.Count > 0
                        item.PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).MarkAsDeleted()
                    End While
                    While item.PortfolioInitialBalanceAccountReceivableShare.Count > 0
                        item.PortfolioInitialBalanceAccountReceivableShare.ElementAt(0).MarkAsDeleted()
                    End While
                    .PortfolioInitialBalanceAccountReceivable.Add(item.MarkAsDeleted())
                Next
            End If
            If Me.listPortfolioInitialBalanceAdvance IsNot Nothing AndAlso Me.listPortfolioInitialBalanceAdvance.Count > 0 Then
                For Each item In listPortfolioInitialBalanceAdvance
                    .PortfolioInitialBalanceAdvance.Add(item)
                Next
            End If
            If Me.listPortfolioInitialBalanceAdvanceDelete IsNot Nothing AndAlso Me.listPortfolioInitialBalanceAdvanceDelete.Count > 0 Then
                For Each item In listPortfolioInitialBalanceAdvanceDelete
                    .PortfolioInitialBalanceAdvance.Add(item.MarkAsDeleted())
                Next
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        INDLcInitialBalance.BeginUpdate()
        ReadOnlyControls(False)
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDBtnCode.Text = String.Empty
        INDDteInitialBalance.EditValue = Nothing
        INDMeObservationInitialBalance.Text = String.Empty
        portfolioInitialBalanceAdvance = Nothing
        listPortfolioInitialBalanceAdvance = Nothing
        listPortfolioInitialBalanceAdvanceDelete = Nothing
        listPortfolioInitialBalanceAccountReceivable = Nothing
        listPortfolioInitialBalanceAccountReceivableDelete = Nothing
        portfolioInitialBalanceAccountReceivable = Nothing
        INDColCustomer.FieldName = "CodeNameCustomer"
        INDGcAdvance.DataSource = Nothing
        INDGcBills.DataSource = Nothing
        INDGcBills.RefreshDataSource()
        datasourceImporFile.Clear()


        IndigoGridControl1.RefreshGrid(INDGcAdvance)
        IndigoGridControl1.RefreshGrid(INDGcBills)
        ActionsOnControls = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcInitialBalance.EndUpdate()
    End Sub

    ''' <summary>
    ''' limpia los controles del popup de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupAdvance()
        INDPceAdvance.Properties.PopupSizeable = True
        INDPccAdvance.Size = New Size(415, 287)
        INDPceAdvance.Properties.PopupSizeable = False
        INDGleAdvanceType.EditValue = Nothing
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleCustomer.EditValue = Nothing
        INDSleCustomer.Properties.NullText = String.Empty
        INDSleThirdParty.EditValue = Nothing
        INDSleAccount.EditValue = Nothing
        INDSleAccount.Properties.NullText = String.Empty
        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty
        INDDteAdvanceDate.EditValue = Nothing
        INDMeObservation.Text = String.Empty
        INDTxtAdvanceValue.EditValue = 0
        popupEditMode = False
        AcconunValueChangeEditMode = False
        Me.portfolioInitialBalanceAdvance = Nothing
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Observaciones", .FieldName = "Observations", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GetAllInitialBalance
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
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim content As String = String.Format(ResourceManager.GetString("FrmInitialBalancePortfolio_IndexContent", MODULE_NAME), portfolioInitialBalance.Code, portfolioInitialBalance.DocumentDate, portfolioInitialBalance.Observations)
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.portfolioInitialBalance.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.portfolioInitialBalance.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.portfolioInitialBalance.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvAdvance, ListActions)
        IndigoGridView2.SetListAcction(INDGvBills, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvAdvance.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvBills.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewPorfolioInitialBalance() As Task
        portfolioInitialBalance = New PortfolioInitialBalance()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"


        'Me.portfolioInitialBalance = New PortfolioInitialBalance
        'If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
        'ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '    If Me._sequence.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '        Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '        Exit Sub
        '    End If
        'End If
        'If Not Me._sequence.Sequential Then
        '    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '        If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        Else
        '            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '            End Using
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '            End If
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'Else
        '    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'End If
        'BarraBotones.StatusRecordVisible = True
        'BarraBotones.StatusRecord = "1"
    End Function

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.portfolioInitialBalance.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.portfolioInitialBalance.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPortfolioInitialBalance(CStr(Me.Tag))
                    AsyncLoader(True)
                    portfolioInitialBalance = Await Model.GetPortfolioInitialBalanceByCode(INDBtnCode.Text.Trim)
                    INDLcInitialBalance.BeginUpdate()
                    If portfolioInitialBalance IsNot Nothing AndAlso portfolioInitialBalance.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(portfolioInitialBalance.Id))

                            If Me.portfolioInitialBalance.Import Then

                                INDColCustomer.FieldName = "CustomerId.NitName"
                                datasourceImporFile = Model.ListPortfolioInitialBalanceAccountReceivable(Me.portfolioInitialBalance.Id)


                            Else
                                INDColCustomer.FieldName = "CodeNameCustomer"
                                Me.listPortfolioInitialBalanceAccountReceivable = Model.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(Me.portfolioInitialBalance.Id)
                                Me.listPortfolioInitialBalanceAdvance = Model.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(Me.portfolioInitialBalance.Id)
                            End If

                            With Me.portfolioInitialBalance
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                        INDPccAccount.Enabled = True
                                        LayoutControl1.Enabled = True
                                        LayoutControlGroup3.Enabled = True
                                        INDGcAccount.Enabled = True
                                        INDGcShare.Enabled = True
                                        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvBills.Columns
                                            If col.Name = "INDColMainAccount" Then
                                                col.OptionsColumn.AllowEdit = True
                                            End If
                                        Next
                                End Select
                                Code = .Code
                                DocumentDate = .DocumentDate
                                Observations = .Observations
                                BarraBotones.StatusRecord = .Status.ToString()
                                If Me.portfolioInitialBalance.Import = False Then
                                    If Me.listPortfolioInitialBalanceAccountReceivable.Count > 0 Then
                                        INDGcBills.DataSource = Nothing
                                        INDGcBills.DataSource = Me.listPortfolioInitialBalanceAccountReceivable
                                        INDGvBills.OptionsView.ShowFooter = True
                                    End If
                                    If Me.listPortfolioInitialBalanceAdvance.Count > 0 Then
                                        INDGcAdvance.DataSource = Nothing
                                        INDGcAdvance.DataSource = Me.listPortfolioInitialBalanceAdvance
                                        INDGvAdvance.OptionsView.ShowFooter = True
                                    End If
                                    BarraBotones.SetDocuments(.Id)
                                    Me.GetDocumentIndexed(MyTag & "_" & .Code)
                                Else
                                    INDGcBills.DataSource = Nothing
                                    IndigoGridControl1.AcceptXPO = True
                                    INDGcBills.RefreshDataSource()
                                    INDGcBills.DataSource = datasourceImporFile

                                    INDGvBills.OptionsView.ShowFooter = True
                                End If
                                If .Status > 1 Then
                                    INDBtnAddBills.Enabled = False
                                End If
                                INDDteInitialBalance.Focus()

                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.portfolioInitialBalance.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = portfolioInitialBalance.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, portfolioInitialBalance.Id, 0, portfolioInitialBalance.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPorfolioInitialBalance()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLcInitialBalance.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If

    End Function

    ''' <summary>
    ''' valida que se llenaron todos los controles para agregar un anticipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function validatePopupAdvance() As String
        Dim errors As New StringBuilder
        If INDGleAdvanceType.EditValue Is Nothing Then
            errors.AppendLine("Tipo Vacio")
        End If
        If INDGleAdvanceType.EditValue IsNot Nothing Then
            If INDGleAdvanceType.EditValue = 1 Then
                If INDSleCustomer.EditValue = Nothing Then
                    errors.AppendLine(INDLciCustomer.CustomizationFormText + ResourceManager.GetString("Empty"))
                End If
            Else
                If INDSleThirdParty.EditValue = Nothing Then
                    errors.AppendLine(INDLciThirdParty.CustomizationFormText + ResourceManager.GetString("Empty"))
                End If
            End If
        End If
        If INDSleAccount.EditValue = Nothing Then
            errors.AppendLine(INDLciAccount.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleCostCenter.EditValue = Nothing Then
                errors.AppendLine(INDLciCostCenter.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDDteAdvanceDate.EditValue = Nothing Then
            errors.AppendLine(INDLciAdvanceDate.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDTxtAdvanceValue.EditValue = 0 Then
            errors.AppendLine(INDLciAdvanceValue.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' carga los datos en los controles para editar el registro
    ''' </summary>
    ''' <param name="_portfolioInitialBalanceAdvance"></param>
    ''' <remarks></remarks>
    Private Sub LoadControlsAdvancePopup(_portfolioInitialBalanceAdvance As PortfolioInitialBalanceAdvance)
        With _portfolioInitialBalanceAdvance
            INDGleAdvanceType.EditValue = .Type
            If .Type = 1 Then
                INDSleCustomer.EditValue = .CustomerId
                INDSleCustomer.Properties.NullText = .CodeNameCustomer
            Else
                INDSleThirdParty.EditValue = .ThirdPartyId
            End If
            INDSleAccount.EditValue = .MainAccountId
            INDSleAccount.Properties.NullText = .CodeNameMainAccount
            INDSleCostCenter.EditValue = .CostCenterId
            INDSleCostCenter.Properties.NullText = .CodeNameCostCenter
            INDDteAdvanceDate.EditValue = .DocumentDate
            INDMeObservation.Text = .Observations
            INDTxtAdvanceValue.EditValue = .Value
        End With
    End Sub

    Private Sub AddBill(sender As Object, e As AddBillEventArgs)
        If listPortfolioInitialBalanceAccountReceivable Is Nothing Then
            listPortfolioInitialBalanceAccountReceivable = New List(Of PortfolioInitialBalanceAccountReceivable)
        End If
        If portfolioInitialBalanceAccountReceivable Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AddBill", MODULE_NAME)
            listPortfolioInitialBalanceAccountReceivable.Add(e.PortfolioInitialBalanceAccountReceivable)
        Else
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("EditBill", MODULE_NAME)
        End If
        INDGcBills.DataSource = Nothing
        INDGcBills.DataSource = listPortfolioInitialBalanceAccountReceivable
        INDGvBills.OptionsView.ShowFooter = True
    End Sub

    ''' <summary>
    ''' metodo para instanciar el formulairo de facturas
    ''' </summary>
    ''' <param name="_portfolioInitialBalanceAccountReceivable"></param>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup(_portfolioInitialBalanceAccountReceivable As PortfolioInitialBalanceAccountReceivable)
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New FrmPopupBills()
            AddHandler formulario.AddBill, AddressOf AddBill
            AddHandler formulario.SetNothingPortfolioInitialBalanceAccountReceivable, AddressOf SetNothingPortfolioInitialBalanceAccountReceivable
            formulario.Bill = _portfolioInitialBalanceAccountReceivable
            formulario.ListBills = Me.listPortfolioInitialBalanceAccountReceivable
            formulario.Size = New Size(900, 700)
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            portfolioInitialBalanceAccountReceivable = Nothing
        End Using
    End Sub

    ''' <summary>
    ''' metodo para poner en null la entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetNothingPortfolioInitialBalanceAccountReceivable(sender As Object, e As EventArgs)
        portfolioInitialBalanceAccountReceivable = Nothing
    End Sub

    ''' <summary>
    ''' metodo que se encarga de validar el archivo de excel
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadImportFile(ByVal fileName As String) As Task
        'Dim sw As New Stopwatch
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String)
                                         progress = New CtrProgress
                                         progress.SetInfoFunction(AddressOf getInfo)
                                         progress.PrintInfo()
                                         progress.Dock = DockStyle.Fill
                                         AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))
                                         Dim sddf = New SpreadsheetControl()
                                         sddf.AllowDrop = False
                                         sddf.LoadDocument(myStream)
                                         Dim workBook As IWorkbook = sddf.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex = 0 Then
                                             listErrosImportFile.Add("No se encontraron registros en el archivo")
                                             Exit Sub
                                         End If
                                         totalItems = rows.LastUsedIndex
                                         progress.SafeInvoke(Sub(x)
                                                                 x.SetTitle = "Registros Procesados"
                                                                 x.PrintInfo()
                                                             End Sub)
                                         Using trasparent = New FrmTransparent(Nothing, False)
                                             trasparent.SafeInvoke(Sub(f) f.ShowDialog())
                                             Using model As New MPortfolioInitialBalance(MyTag)
                                                 Dim indexSend As Integer = 1
                                                 While indexSend + 1 <= rows.LastUsedIndex + 1
                                                     listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                                     Dim result As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
                                                     If (indexSend + itemsSend) >= rows.LastUsedIndex Then
                                                         SetRow(indexSend, rows.LastUsedIndex + 1)
                                                     Else
                                                         SetRow(indexSend, indexSend + itemsSend)
                                                     End If
                                                     result = model.ValidateFileBillsInitialBalance(listRows.ToList(), _indigoSession.IndigoCompanyType)

                                                     If indexSend = 1 Then
                                                         listErrosImportFile = result.MessageResult
                                                     Else
                                                         listErrosImportFile.AddRange(result.MessageResult)
                                                     End If
                                                     'obtenemos el resultado
                                                     If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                                                         If listPortfolioInitialBalanceAccountReceivable IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivable.Count > 0 Then
                                                             Dim listBillsTmp = (From o In result.ObjectEmbbeded Select New With {
                                                                                    Key .InvoiceNumber = o.InvoiceNumber,
                                                                                    Key .NumberShare = o.PortfolioInitialBalanceAccountReceivableShare(0).Number,
                                                                                    Key .AccountId = o.PortfolioInitialBalanceAccountReceivableAccounting(0).MainAccountId}).Distinct.ToList()
                                                             Dim listBillsNotExist As New List(Of String)
                                                             Dim previusRecords = listPortfolioInitialBalanceAccountReceivable.Select(Function(o) New _
                                                                                             With {
                                                                                             Key .InvoiceNumber = o.InvoiceNumber,
                                                                                             Key .NumberShare = o.PortfolioInitialBalanceAccountReceivableShare(0).Number,
                                                                                             Key .AccountId = o.PortfolioInitialBalanceAccountReceivableAccounting(0).MainAccountId}).ToList()
                                                             Dim sharesError = previusRecords.Where(Function(x) listBillsTmp.Contains(x)).Distinct().ToList()
                                                             If sharesError IsNot Nothing AndAlso sharesError.Any() Then
                                                                 listErrosImportFile.AddRange(sharesError.Select(Function(m) $"La factura {m.InvoiceNumber} se repite con número de cuota y cuenta contable saldos.").Distinct().ToList())
                                                             End If
                                                             listBillsNotExist = listBillsTmp.Where(Function(x) Not previusRecords.Contains(x)).Select(Function(o) o.InvoiceNumber).Distinct().ToList()
                                                             If listBillsNotExist IsNot Nothing AndAlso listBillsNotExist.Any() Then
                                                                 listPortfolioInitialBalanceAccountReceivable.AddRange(result.ObjectEmbbeded.FindAll(Function(x) listBillsNotExist.Contains(x.InvoiceNumber)))
                                                             End If
                                                         Else
                                                             listPortfolioInitialBalanceAccountReceivable = result.ObjectEmbbeded
                                                         End If
                                                     End If
                                                     If (indexSend + itemsSend) >= rows.LastUsedIndex Then
                                                         indexSend = rows.LastUsedIndex + 1
                                                     Else
                                                         indexSend += itemsSend
                                                     End If
                                                     totalProcessedItems = indexSend
                                                     progress.SafeInvoke(Sub(x) x.PrintInfo())
                                                 End While
                                             End Using
                                         End Using
                                     End Sub)
    End Function



    ''' <summary>
    ''' metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <remarks></remarks>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Dim columnsNumberShare = 20
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  'If IsNumeric(rows.Item(x).Item(11).Value) Then
                                                  If rows.Item(x).Item(12).Value.IsEmpty Then
                                                      columnsNumberShare = 12
                                                  Else
                                                      columnsNumberShare = 20
                                                  End If
                                                  'End If
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(columnsNumberShare)})
                                              End SyncLock
                                          End Sub)
    End Sub
    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function
#End Region

#Region "HANDLES"
#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        presenter = Nothing
        portfolioInitialBalance = Nothing
        popupEditMode = Nothing
        portfolioInitialBalanceAdvance = Nothing
        listPortfolioInitialBalanceAdvance = Nothing
        listPortfolioInitialBalanceAdvanceDelete = Nothing
        portfolioInitialBalanceAccountReceivable = Nothing
        listPortfolioInitialBalanceAccountReceivable = Nothing
        listPortfolioInitialBalanceAccountReceivableDelete = Nothing
        AcconunValueChangeEditMode = Nothing
        progress = Nothing
        totalItems = Nothing
        totalProcessedItems = Nothing
        datasourceImporFile = Nothing
        rows = Nothing
        listRows = Nothing
        myStream = Nothing
        listErrosImportFile = Nothing
        portfolioInitialBalanceId = Nothing
    End Sub

    Private Sub FrmInitialBalancePortfolio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcInitialBalance, True)

        INDEsbBills.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Cliente"},'0
                            New ExcelColumn With {.Name = "Nº Factura"},'1
                            New ExcelColumn With {.Name = "¿Es factura electrónica?", .Comment = "Número 0 = equivale a la opción NO" & vbCrLf & "Número 1 = equivale a la opción SI"},'2
                            New ExcelColumn With {.Name = "CUFE", .Comment = "Código CUFE de la factura del saldo inicial"},'3
                            New ExcelColumn With {.Name = "Estado Factura", .Comment = "Estado de Cartera" & vbCrLf & "Ingrese valor númerico" & vbCrLf & "Sin Radicar = 1" & vbCrLf & "Radicada = 2" & vbCrLf & "Radicada Entidad = 3" & vbCrLf & "Objetada = 4" & vbCrLf & "Contestada Radicada = 5" & vbCrLf & "Aceptada = 6" & vbCrLf & "Certificada Parcial = 7" & vbCrLf & "Certificada Total = 8" & vbCrLf & "No Subsanable = 9" & vbCrLf & "Difícil Recaudo =10" & vbCrLf & "Factura Devuelta = 11" & vbCrLf & "Glosa Ratificada = 12" & vbCrLf & "Radicación Tramite Objeción = 13" & vbCrLf & "Devolución Factura = 14"},
                            New ExcelColumn With {.Name = "Categoria de Factura"},'5
                            New ExcelColumn With {.Name = "Fecha"},'6
                            New ExcelColumn With {.Name = "Plazo"},'7
                            New ExcelColumn With {.Name = "Numero Cuota"},'8
                            New ExcelColumn With {.Name = "Cuenta Contable Saldos"},'9
                            New ExcelColumn With {.Name = "Centro de Costo"},'10
                            New ExcelColumn With {.Name = "Observacion"},'11
                            New ExcelColumn With {.Name = "Valor Factura"},'12
                            New ExcelColumn With {.Name = "Saldo Cuenta"},'13
                            New ExcelColumn With {.Name = "Centro de Costos Glosas"},'14
                            New ExcelColumn With {.Name = "Cuenta Contable sin Radicar"},'15
                            New ExcelColumn With {.Name = "Cuenta Contable Radicada"},'16
                            New ExcelColumn With {.Name = "Cuenta Contable Glosa Subsanable (Empresa Privada)"},'17
                            New ExcelColumn With {.Name = "Cuenta Contable Conciliación (Empresa Privada)"},'18
                            New ExcelColumn With {.Name = "Cuenta Contable Cobro Juridico o Dificil Recaudo (Empresa Privada)"},'19
                            New ExcelColumn With {.Name = "Cuenta Contable Orden de Glosa (Empresa Publica)"},'20
                            New ExcelColumn With {.Name = "Cuenta Contable Acreedores Glosas (Empresa Publica)"}'21
                        }
                    })

        INDEsbAdvance.AddRangeColumns("Tipo 1 - Anticipo Entidad, 2 - Anticipo Tercero", "Tercero", "Cliente", "Fecha", "Cuenta Contable", "Centro de Costo", "Observacion", "Valor")
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _indigoSession = SessionValues.Instance
        AddActionsColumns()
        presenter = New PPortfolioInitialBalance(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        INDGleAdvanceType.Properties.DataSource = ListAdvanceType
        Using model As New MBusqueda
            INDSleThirdParty.Properties.DataSource = model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
        Deshacer()
        LoadStatus()
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmInitialBalancePortfolio_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Activated"
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAddBills_Click(sender As Object, e As EventArgs) Handles INDBtnAddBills.Click

        InstantiatePopup(Nothing)
    End Sub

    Private Sub INDBtnAddAdvance_Click(sender As Object, e As EventArgs) Handles INDBtnAddAdvance.Click
        Dim errors = validatePopupAdvance()
        If errors.Length = 0 Then
            If Me.portfolioInitialBalanceAdvance Is Nothing Then
                Me.portfolioInitialBalanceAdvance = New PortfolioInitialBalanceAdvance()
            End If
            With Me.portfolioInitialBalanceAdvance
                .Type = INDGleAdvanceType.EditValue
                If INDGleAdvanceType.EditValue = 1 Then
                    Using modelCustomer As New MCustomers(Me.Tag)
                        Dim customerTpm = modelCustomer.GetCustomerById(INDSleCustomer.EditValue)
                        .ThirdPartyId = customerTpm.ThirdPartyId
                    End Using
                Else
                    .ThirdPartyId = INDSleThirdParty.EditValue
                End If
                Using modelThird As New MThirdParty(MyTag)
                    Dim third = modelThird.GetThirdPartyByIdSimple(.ThirdPartyId)
                    .NitNameThirdParty = third.Nit + " - " + third.Name
                End Using
                .MainAccountId = INDSleAccount.EditValue
                If INDSleAccount.Text IsNot String.Empty Then
                    .CodeNameMainAccount = INDSleAccount.Text
                Else
                    .CodeNameMainAccount = INDSleAccount.Properties.NullText
                End If
                .CostCenterId = INDSleCostCenter.EditValue
                If INDSleCostCenter.Text IsNot String.Empty Then
                    .CodeNameCostCenter = INDSleCostCenter.Text
                Else
                    .CodeNameCostCenter = INDSleCostCenter.Properties.NullText
                End If
                .DocumentDate = INDDteAdvanceDate.EditValue
                .CustomerId = INDSleCustomer.EditValue
                If INDSleCustomer.Text IsNot String.Empty Then
                    .CodeNameCustomer = INDSleCustomer.Text
                Else
                    .CodeNameCustomer = INDSleCustomer.Properties.NullText
                End If
                .Value = INDTxtAdvanceValue.EditValue
                .Observations = INDMeObservation.Text
            End With
            If Me.listPortfolioInitialBalanceAdvance Is Nothing Then
                Me.listPortfolioInitialBalanceAdvance = New List(Of PortfolioInitialBalanceAdvance)
            End If
            If popupEditMode = False Then
                Me.listPortfolioInitialBalanceAdvance.Add(Me.portfolioInitialBalanceAdvance)
            End If
            INDGcAdvance.DataSource = Nothing
            INDGcAdvance.DataSource = Me.listPortfolioInitialBalanceAdvance
            INDGvAdvance.OptionsView.ShowFooter = True
            CleanControlsPopupAdvance()
            INDSleCustomer.Focus()
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors
        End If
    End Sub

    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If listPortfolioInitialBalanceAccountReceivable IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivable.Count > 0 Then
            If MessageIndigo.Show("Se perderan los datos que estan en la rejilla, desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            Else
                listPortfolioInitialBalanceAccountReceivable = Nothing
                INDGcBills.DataSource = Nothing
            End If
        End If
        'Dim sw As New Stopwatch
        'configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then

                    'validamos los datos
                    If MessageIndigo.Show("Desea continuar con el proceso de importación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Await Me.LoadImportFile(myStream)
                        'si existen errores informamos al usurio y el proceso no continua
                        If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                            Mensaje(EeventViewerImages.MensajeError) = "El archivo presento los siguientes errores y no se podra confirmar el documento"
                            Using formulario As New FrmListErrors(listErrosImportFile)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                            listPortfolioInitialBalanceAccountReceivable = Nothing
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            INDGvBills.HideLoadingPanel()
                            INDGcBills.DataSource = Nothing
                            totalItems = 0
                            totalProcessedItems = 0
                            progress.PrintInfo()
                            AdditionalControlPanel.Controls.Clear()
                        End If

                        If listPortfolioInitialBalanceAccountReceivable Is Nothing OrElse listPortfolioInitialBalanceAccountReceivable.Count = 0 Then
                            AsyncLoader(False)
                            Return
                        End If

                        'si no hubo errores de validacion
                        Dim listInfoProcessSaveImportFile As New List(Of Tuple(Of String, Integer))
                        totalProcessedItems = 0
                        progress.SetTitle = "Registros Guardados"
                        progress.PrintInfo()

                        Dim indexSend As Integer = 0
                        Dim result As ActionResult(Of String) = Nothing

                        'enviamos a guardar de 300
                        While listPortfolioInitialBalanceAccountReceivable.Count > 0
                            If portfolioInitialBalanceId = 0 Then
                                With Me.portfolioInitialBalance
                                    .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
                                    .Code = Code
                                    .DocumentDate = DocumentDate
                                    .Observations = Observations
                                    .OperatingUnitId = Me._idOperativeUnit
                                    .Import = True
                                    .Status = 1
                                End With
                            Else
                                Using Model As New MPortfolioInitialBalance(MyTag)
                                    portfolioInitialBalance = Model.GetPortfolioInitialBalanceById(portfolioInitialBalanceId)
                                    portfolioInitialBalance.MarkAsModified()
                                End Using
                            End If
                            Dim listSend = listPortfolioInitialBalanceAccountReceivable.Take(itemsSend).ToList()
                            Dim objLock As New Object()
                            Parallel.ForEach(listSend, Sub(x)
                                                           SyncLock objLock
                                                               portfolioInitialBalance.PortfolioInitialBalanceAccountReceivable.Add(x)
                                                           End SyncLock
                                                       End Sub)
                            Using model As New MPortfolioInitialBalance(MyTag)
                                result = Await model.SaveAndConfirmPortfolioInitialBalance(portfolioInitialBalance, _idCurrentSequence)
                            End Using
                            'almacenamos los reultados del proceso para luego informar al usuario
                            If result.StateResult = False Or result.StateResultAux = False Then
                                If listPortfolioInitialBalanceAccountReceivable.Count < itemsSend Then
                                    listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (totalItems).ToString() + " no se pudieron guardar porque : " + result.Message, 2))
                                Else
                                    listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (indexSend + itemsSend).ToString() + " no se pudieron guardar porque : " + result.Message, 2))
                                End If
                            Else
                                portfolioInitialBalanceId = CInt(result.ObjectEmbbeded)
                                If listPortfolioInitialBalanceAccountReceivable.Count < itemsSend Then
                                    listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (totalItems).ToString() + " se guardaron correctamente", 1))

                                Else
                                    listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (indexSend + itemsSend).ToString() + " se guardaron correctamente", 1))
                                End If
                            End If
                            If listPortfolioInitialBalanceAccountReceivable.Count < itemsSend Then
                                indexSend += listPortfolioInitialBalanceAccountReceivable.Count - 1
                                totalProcessedItems = totalItems
                                progress.PrintInfo()
                                listPortfolioInitialBalanceAccountReceivable.RemoveRange(0, listPortfolioInitialBalanceAccountReceivable.Count)
                            Else
                                indexSend += itemsSend
                                totalProcessedItems = indexSend
                                progress.PrintInfo()
                                listPortfolioInitialBalanceAccountReceivable.RemoveRange(0, itemsSend)
                            End If
                        End While
                        'mostramos los resultados del proceso
                        Using formulario As New FrmListErrors(listInfoProcessSaveImportFile)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                        AsyncLoader(False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        totalItems = 0
                        totalProcessedItems = 0
                        progress.PrintInfo()
                        AdditionalControlPanel.Controls.Clear()
                        Me.Deshacer()
                    End If
                End If
                AsyncLoader(False)
            Catch Ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + Ex.Message
                listPortfolioInitialBalanceAccountReceivable = Nothing
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDGvBills.HideLoadingPanel()
                INDGcBills.DataSource = Nothing
                totalItems = 0
                totalProcessedItems = 0
                progress.PrintInfo()
                AdditionalControlPanel.Controls.Clear()
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

#End Region

#Region "KeyDown"
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewPorfolioInitialBalance()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If CustomerXPO Is Nothing Then
            presenter.InitializeCustomerXPO()
        End If
    End Sub

    Private Sub INDSleAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccount.QueryPopUp
        If MainAccountXPO Is Nothing Then
            presenter.InitializeMainAccountXPO()
        End If
    End Sub

    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If CostCenterXPO Is Nothing Then
            presenter.InitializeCostCenterXPO()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPopupPUC
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                INDPceAdvance.ShowPopup()
                INDSleAccount.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                presenter.InitializeCostCenterXPO()
                INDPceAdvance.ShowPopup()
                INDSleCostCenter.Focus()
            End Using
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Me.portfolioInitialBalanceAdvance = DirectCast(INDGvAdvance.GetFocusedRow(), PortfolioInitialBalanceAdvance)
        Select Case button.Tag.ToString()
            Case "Edit"
                popupEditMode = True
                LoadControlsAdvancePopup(Me.portfolioInitialBalanceAdvance)
                INDPceAdvance.Focus()
                INDSleCustomer.Focus()
                INDPceAdvance.ShowPopup()
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If Me.portfolioInitialBalanceAdvance.Id > 0 Then
                        If Me.listPortfolioInitialBalanceAdvanceDelete Is Nothing Then
                            Me.listPortfolioInitialBalanceAdvanceDelete = New List(Of PortfolioInitialBalanceAdvance)
                        End If
                        Me.listPortfolioInitialBalanceAdvanceDelete.Add(Me.portfolioInitialBalanceAdvance)
                    End If
                    Me.listPortfolioInitialBalanceAdvance.Remove(Me.portfolioInitialBalanceAdvance)
                    INDGcAdvance.DataSource = Nothing
                    INDGcAdvance.DataSource = Me.listPortfolioInitialBalanceAdvance

                    CleanControlsPopupAdvance()
                End If
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim button = DirectCast(sender, SimpleButton)
        Select Case button.Tag.ToString()
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub
#End Region

#Region "Context Menu"
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        Me.Cursor = ChangeCursorIndigo()
        Me.portfolioInitialBalanceAccountReceivable = DirectCast(INDGvBills.GetFocusedRow, PortfolioInitialBalanceAccountReceivable)
        Using formulario As New FrmPopupBills()
            AddHandler formulario.AddBill, AddressOf AddBill
            AddHandler formulario.SetNothingPortfolioInitialBalanceAccountReceivable, AddressOf SetNothingPortfolioInitialBalanceAccountReceivable
            formulario.Bill = portfolioInitialBalanceAccountReceivable
            formulario.ListBills = Me.listPortfolioInitialBalanceAccountReceivable
            formulario.Size = New Size(900, 700)
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            portfolioInitialBalanceAccountReceivable = Nothing
        End Using
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Me.portfolioInitialBalanceAccountReceivable = DirectCast(INDGvBills.GetFocusedRow, PortfolioInitialBalanceAccountReceivable)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If Me.portfolioInitialBalanceAccountReceivable.Id > 0 Then
                If listPortfolioInitialBalanceAccountReceivableDelete Is Nothing Then
                    listPortfolioInitialBalanceAccountReceivableDelete = New List(Of PortfolioInitialBalanceAccountReceivable)
                End If
                listPortfolioInitialBalanceAccountReceivableDelete.Add(portfolioInitialBalanceAccountReceivable)
            End If
            Me.listPortfolioInitialBalanceAccountReceivable.Remove(portfolioInitialBalanceAccountReceivable)
            INDGcBills.DataSource = Nothing
            INDGcBills.DataSource = listPortfolioInitialBalanceAccountReceivable
            If listPortfolioInitialBalanceAccountReceivable.Count = 0 Then
                IndigoGridControl1.RefreshGrid(INDGcAccount)
            End If
            portfolioInitialBalanceAccountReceivable = Nothing
        End If
    End Sub

#End Region

#Region "Popup"
    Private Sub INDPceAdvance_Popup(sender As Object, e As EventArgs) Handles INDPceAdvance.Popup
        INDSleCustomer.Focus()
    End Sub

    Private Sub INDRpPceAccount_Popup(sender As Object, e As EventArgs) Handles INDRpPceAccount.Popup
        If Me.portfolioInitialBalance.Status = 2 AndAlso Me.portfolioInitialBalance.Import Then
            INDLciAccountPopupXpo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSharePopupXpo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSharePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Dim bill = INDGvBills.GetFocusedRow()
            Using model As New MPortfolioInitialBalance(MyTag)
                INDGcAccountXpo.DataSource = model.ListPortfolioInitialBalanceAccountReceivableAccounting(bill.Id)
                INDGcShareXpo.DataSource = model.ListPortfolioInitialBalanceAccountReceivableShare(bill.Id)
            End Using
        Else
            INDLciAccountPopupXpo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSharePopupXpo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAccountPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSharePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Dim bill = DirectCast(INDGvBills.GetFocusedRow, PortfolioInitialBalanceAccountReceivable)
            INDGcAccount.DataSource = Nothing
            INDGcAccount.DataSource = bill.PortfolioInitialBalanceAccountReceivableAccounting
            INDGcShare.DataSource = Nothing
            INDGcShare.DataSource = bill.PortfolioInitialBalanceAccountReceivableShare
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub INDSleAccount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleAccount.EditValueChanging
        If e.NewValue IsNot Nothing Then
            If popupEditMode = True Then
                AcconunValueChangeEditMode = True
            End If
            Using model As New MPUC(Me.Tag)
                Dim accountNew = model.GetAccountByIdSimple(e.NewValue, False)
                Dim accountOld As MainAccounts = Nothing
                If e.OldValue IsNot Nothing Then
                    accountOld = model.GetAccountByIdSimple(e.OldValue, False)
                Else
                    accountOld = New MainAccounts With {.HandlesCostCenter = False}
                End If
                If accountNew.HandlesCostCenter = True Then
                    INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If accountOld IsNot Nothing AndAlso accountOld.HandlesCostCenter = False Then
                        INDPceAdvance.Properties.PopupSizeable = True
                        INDPccAdvance.Size = New Size(415, 322)
                        INDPceAdvance.Properties.PopupSizeable = False
                        INDPceAdvance.Focus()
                        INDPceAdvance.ShowPopup()
                    End If
                Else
                    INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSleCostCenter.EditValue = Nothing
                    INDSleCostCenter.Properties.NullText = String.Empty
                    If accountOld IsNot Nothing AndAlso accountOld.HandlesCostCenter = True Then
                        INDPceAdvance.Properties.PopupSizeable = True
                        INDPccAdvance.Size = New Size(415, 287)
                        INDPceAdvance.Properties.PopupSizeable = False
                        INDPceAdvance.Focus()
                        INDPceAdvance.ShowPopup()
                    End If
                End If
            End Using
        Else
            INDSleCostCenter.EditValue = Nothing
            INDSleCostCenter.Properties.NullText = String.Empty
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDPceAdvance.Properties.PopupSizeable = True
            INDPccAccount.Size = New Size(415, 287)
            INDPceAdvance.Properties.PopupSizeable = False
            INDPceAdvance.Focus()
            INDPceAdvance.ShowPopup()
        End If
        AcconunValueChangeEditMode = False
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDDteInitialBalance_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteInitialBalance.EditValueChanged
        If DocumentDate IsNot Nothing Then
            INDBtnImportFile.Enabled = True
        End If
    End Sub
#End Region

#Region "CloseUp"
    Private Sub INDPceAdvance_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceAdvance.CloseUp
        If popupEditMode = True And AcconunValueChangeEditMode = False Then
            CleanControlsPopupAdvance()
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.portfolioInitialBalance IsNot Nothing AndAlso Me.portfolioInitialBalance.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            Await LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDBtnCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub
#End Region

#Region "PasteToGrid"
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If sender.Name = INDGcBills.Name Then
            If e.Rows.Count = 0 Then
                Exit Sub
            End If
            If e.Rows.Count > 300 Then
                Mensaje(EeventViewerImages.Advertencia) = "Para procesar esta cantidad de información se debe hacer por medio de importación de archivos"
                Exit Sub
            End If
            INDGvBills.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MPortfolioInitialBalance(MyTag)
                If e.Rows(0).Item(0).Contains("Cliente") Then
                    e.Rows.Remove(e.Rows.ElementAt(0))
                End If
                Dim result = Await model.SetBillsCopyPaste(e.Rows, indigo.IndigoCompanyType)
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If listPortfolioInitialBalanceAccountReceivable IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivable.Count > 0 Then
                        Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.InvoiceNumber).ToList.Distinct.ToList()
                        Dim listBillsNotExist As New List(Of String)
                        For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
                            Dim item = i
                            Dim share = listPortfolioInitialBalanceAccountReceivable.Where(Function(x) x.InvoiceNumber = listBillsTmp.Item((item))).FirstOrDefault()
                            If share IsNot Nothing Then
                                result.MessageResult.Add(String.Format(ResourceManager.GetString("InvoiceListExist", "Treasury"), share.InvoiceNumber))
                            Else
                                listBillsNotExist.Add(listBillsTmp.Item((item)))
                            End If
                        Next
                        For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
                            Dim item = i
                            listPortfolioInitialBalanceAccountReceivable.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.InvoiceNumber = listBillsNotExist.Item(item)))
                        Next
                    Else
                        listPortfolioInitialBalanceAccountReceivable = result.ObjectEmbbeded
                    End If
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                If listPortfolioInitialBalanceAccountReceivable IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivable.Count > 1 Then
                    listPortfolioInitialBalanceAccountReceivable = listPortfolioInitialBalanceAccountReceivable.OrderBy(Function(x) x.InvoiceNumber).ToList()
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDGvBills.HideLoadingPanel()
                INDGcBills.DataSource = Nothing
                INDGcBills.DataSource = listPortfolioInitialBalanceAccountReceivable
                If listPortfolioInitialBalanceAccountReceivable IsNot Nothing AndAlso listPortfolioInitialBalanceAccountReceivable.Count > 0 Then
                    INDGvBills.OptionsFind.AlwaysVisible = True
                    IndigoGridControl1.SetExportButton(INDGcBills, True)
                Else
                    INDGvBills.OptionsFind.AlwaysVisible = False
                    IndigoGridControl1.SetExportButton(INDGcBills, False)
                End If
            End Using
        End If
        If sender.Name = INDGcAdvance.Name Then
            INDGvAdvance.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MPortfolioInitialBalance(MyTag)
                Dim result = Await model.SetAdvancesCopyPaste(e.Rows)
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If listPortfolioInitialBalanceAdvance IsNot Nothing AndAlso listPortfolioInitialBalanceAdvance.Count > 0 Then
                        listPortfolioInitialBalanceAdvance.AddRange(result.ObjectEmbbeded)
                    Else
                        listPortfolioInitialBalanceAdvance = result.ObjectEmbbeded
                    End If
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDGvAdvance.HideLoadingPanel()
                INDGcAdvance.DataSource = Nothing
                INDGcAdvance.DataSource = listPortfolioInitialBalanceAdvance

            End Using
        End If
    End Sub
#End Region


#End Region

#Region "BAR BUTTONS"

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
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveAndConfirm()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            portfolioInitialBalance.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, portfolioInitialBalance.Id, 0, portfolioInitialBalance.Id)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveAndConfirm()
    End Sub
#End Region


    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("532", Nothing, True)
        End If
    End Sub

    Dim _listAdvanceType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListAdvanceType As List(Of Tuple(Of Byte, String))
        Get
            If _listAdvanceType Is Nothing Then
                _listAdvanceType = New List(Of Tuple(Of Byte, String))
                _listAdvanceType.Add(New Tuple(Of Byte, String)(1, "Anticipo Entidad"))
                _listAdvanceType.Add(New Tuple(Of Byte, String)(2, "Anticipo Tercero"))

            End If
            Return _listAdvanceType
        End Get
    End Property


    Private Sub INDGleAdvanceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAdvanceType.EditValueChanged
        If INDGleAdvanceType.EditValue IsNot Nothing Then
            If INDGleAdvanceType.EditValue = 1 Then
                INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSleThirdParty.EditValue = Nothing
                INDLciCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSleCustomer.EditValue = Nothing
                INDLciCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
End Class