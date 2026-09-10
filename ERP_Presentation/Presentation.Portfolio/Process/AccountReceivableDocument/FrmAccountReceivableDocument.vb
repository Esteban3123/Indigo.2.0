'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 17/06/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports DevExpress
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Glosas
Imports Presentation.Glosas.MVP
Imports Presentation.Portfolio.MVP

#End Region

Public Class FrmAccountReceivableDocument
    Implements IAccountReceivableDocument, ICustomizableForm

#Region "Builder"

    Public ctrTmp As CtrValueAccountReceivableDocuments

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrValueAccountReceivableDocuments()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshTotalValues()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Decimal
        valueDebit = 0
        valueCredit = 0
        valueTotal = 0
        If ListAccountReceivableDocumentDetail IsNot Nothing AndAlso ListAccountReceivableDocumentDetail.Count > 0 Then
            valueDebit = (From l In ListAccountReceivableDocumentDetail Where l.Nature = 1 Select l.Value).Sum
            valueCredit = (From l In ListAccountReceivableDocumentDetail Where l.Nature = 2 Select l.Value).Sum
        End If

        valueTotal = valueCredit - valueDebit
        INDspnValueDebit.EditValue = valueDebit
        INDspnValueCredit.EditValue = valueCredit
        Return valueTotal
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAccountReceivableDocument.ActionsOnControls
        Set(value As Boolean)
            INDLcAccountReceivableDocument.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleCustomerId.Enabled = value
            INDtxtInvoiceNumber.Enabled = value
            INDmeObservation.Enabled = value
            INDsleMainAccountId.Enabled = value
            INDsleCostCenterId.Enabled = value
            INDspnTerm.Enabled = value
            INDdeExpiredDate.Enabled = value
            INDbtnAdd.Enabled = value
            INDGcConcepts.Enabled = value
            INDsleCurrency.Enabled = value
            INDLcAccountReceivableDocument.EndUpdate()
            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IAccountReceivableDocument.Code
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
    ''' Obtiene o establece el id del centro de costo del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer? Implements IAccountReceivableDocument.CostCenterId
        Get
            Return INDsleCostCenterId.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenterId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As XPInstantFeedbackSource Implements IAccountReceivableDocument.CostCenterXpo
        Get
            Return INDsleCostCenterId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenterId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del cliente del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CustomerId As Integer Implements IAccountReceivableDocument.CustomerId
        Get
            Return INDsleCustomerId.EditValue
        End Get
        Set(value As Integer)
            INDsleCustomerId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el listado de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CustomerXpo As XPInstantFeedbackSource Implements IAccountReceivableDocument.CustomerXpo
        Get
            Return INDsleCustomerId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCustomerId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date Implements IAccountReceivableDocument.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de vencimiento del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpiredDate As Date Implements IAccountReceivableDocument.ExpiredDate
        Get
            Return INDdeExpiredDate.EditValue
        End Get
        Set(value As Date)
            INDdeExpiredDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el numero de la factura del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceNumber As String Implements IAccountReceivableDocument.InvoiceNumber
        Get
            Return INDtxtInvoiceNumber.EditValue
        End Get
        Set(value As String)
            INDtxtInvoiceNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountId As Integer Implements IAccountReceivableDocument.MainAccountId
        Get
            Return INDsleMainAccountId.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountXpo As XPInstantFeedbackSource Implements IAccountReceivableDocument.MainAccountXpo
        Get
            Return INDsleMainAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAccountReceivableDocument.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IAccountReceivableDocument.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' obtiene o establece la observación del documento de cuotas x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements IAccountReceivableDocument.Observation
        Get
            Return INDmeObservation.EditValue
        End Get
        Set(value As String)
            INDmeObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _sequence As Domain.Entities.PortfolioSequence
    Public Property Sequense As PortfolioSequence Implements IAccountReceivableDocument.Sequense
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

    ''' <summary>
    ''' Obtiene o establece la cantidad de cuotas del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Share As Integer Implements IAccountReceivableDocument.Share
        Get
            Return INDspnShare.EditValue
        End Get
        Set(value As Integer)
            INDspnShare.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IAccountReceivableDocument.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el plazo del documento de cuentas x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Term As Integer Implements IAccountReceivableDocument.Term
        Get
            Return INDspnTerm.EditValue
        End Get
        Set(value As Integer)
            INDspnTerm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la moneda selecionada, por defecto es la del sistema
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IAccountReceivableDocument.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = _currencyAbbreviation
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Datasource que carga las monedas del maesttro moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDataSourceXpo As XPInstantFeedbackSource Implements IAccountReceivableDocument.CurrencyDataSourceXpo
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrency.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que establece el codigo standar de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String Implements IAccountReceivableDocument.CurrencyAbbreviation
        Get
            If Me.CurrencySelected IsNot Nothing Then
                Return Me.CurrencySelected?.Abbreviation
            Else
                Return Me.INDsleCurrency.Text
            End If
        End Get
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAccountReceivableDocument

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MAccountReceivableDocument

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountReceivableDocument As AccountReceivableDocument

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemAccountReceivableDocumentDetail As AccountReceivableDocumentDetail

    ''' <summary>
    ''' Listado de eliminados de los detalles de documento de cuenta x cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteAccountReceivableDocumentDetail As List(Of AccountReceivableDocumentDetail)

    ''' <summary>
    ''' Listado de los detalles de documento de cuenta x cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Property ListAccountReceivableDocumentDetail As List(Of AccountReceivableDocumentDetail)

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Entidad de cliente
    ''' </summary>
    ''' <remarks></remarks>
    Dim customer As Domain.Entities.Customer

    ''' <summary>
    ''' entidad de cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Dim mainAccount As MainAccounts

    ''' <summary>
    ''' obtiene o establece el valor total del debito
    ''' </summary>
    ''' <remarks></remarks>
    Dim valueDebit As Decimal

    ''' <summary>
    ''' Obtiene y establece el valor total del credito
    ''' </summary>
    ''' <remarks></remarks>
    Dim valueCredit As Decimal

    ''' <summary>
    ''' Establece o obtiene el valor de la cuenta por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Dim valueTotal As Decimal

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#End Region

#Region "ICRUD"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If accountReceivableDocument IsNot Nothing AndAlso accountReceivableDocument.Status < 3 Then
            If ValidateControls() = False Then
                Exit Sub
            Else
                If ListAccountReceivableDocumentDetail Is Nothing OrElse ListAccountReceivableDocumentDetail.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No hay Conceptos agregados en la rejilla."
                    Exit Sub
                End If
                If valueTotal < 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValueAccountReceivableDocument", "Portfolio"))
                    Exit Sub
                End If
                If INDspnShare.EditValue <= 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El numero de cuotas debe ser mayor a 0."
                    INDspnShare.Focus()
                    Exit Sub
                End If
            End If
        End If
        Try
            AssigningValues()
            Using model As New MAccountReceivableDocument(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveAccountReceivableDocument(accountReceivableDocument, _idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message

                    Me.accountReceivableDocument = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, accountReceivableDocument.Id, 0, accountReceivableDocument.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, accountReceivableDocument.Id, 0, accountReceivableDocument.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, accountReceivableDocument.Id, 0, accountReceivableDocument.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, accountReceivableDocument.Id, 0, accountReceivableDocument.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewAccountReceivableDocument()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Cliente", .FieldName = "CustomerId.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GetAllAccountReceivableDocument
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.accountReceivableDocument.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.accountReceivableDocument.Id}
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
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountReceivableDocument.Code, Me.accountReceivableDocument.OperatingUnitId, Me.accountReceivableDocument.DocumentDate, Me.accountReceivableDocument.CustomerId, Me.accountReceivableDocument.Observation),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.accountReceivableDocument.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountReceivableDocument.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountReceivableDocument.Code, Me.accountReceivableDocument.OperatingUnitId, Me.accountReceivableDocument.DocumentDate, Me.accountReceivableDocument.CustomerId, Me.accountReceivableDocument.Observation)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountReceivableDocument.Code)
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
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()

        INDLcAccountReceivableDocument.BeginUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        accountReceivableDocument = Nothing
        Code = String.Empty
        DocumentDate = Me.GetDateServer()
        CustomerId = Nothing
        INDsleCustomerId.Properties.NullText = String.Empty
        MainAccountId = Nothing
        INDsleMainAccountId.Properties.NullText = String.Empty
        CostCenterId = Nothing
        INDsleCostCenterId.Properties.NullText = String.Empty
        InvoiceNumber = Nothing
        Term = 0
        ExpiredDate = Nothing
        Share = 0
        Observation = Nothing
        Status = 0
        Me.CurrencyDataSourceXpo = Nothing
        Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        ReadOnlyControls(False)
        INDsleMainAccountId.Properties.ReadOnly = True
        INDspnTerm.Properties.ReadOnly = True
        INDdeExpiredDate.Properties.ReadOnly = True
        INDspnValueDebit.Properties.ReadOnly = True
        INDspnValueCredit.Properties.ReadOnly = True
        ItemAccountReceivableDocumentDetail = Nothing
        ListAccountReceivableDocumentDetail = Nothing
        ListDeleteAccountReceivableDocumentDetail = Nothing
        INDGcConcepts.DataSource = Nothing
        ctrTmp.RefreshTotalValues()
        INDLcAccountReceivableDocument.EndUpdate()
        ActionsOnControls = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewAccountReceivableDocument() As Task
        accountReceivableDocument = New AccountReceivableDocument()
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

    End Function

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With accountReceivableDocument
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DocumentDate = DocumentDate
            .CustomerId = CustomerId
            .MainAccountId = MainAccountId
            If INDlciCostCenterId.Visibility = XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = CostCenterId
            Else
                .CostCenterId = Nothing
            End If
            .InvoiceNumber = InvoiceNumber
            .Term = Term
            .ExpiredDate = ExpiredDate
            .Share = Share
            .Value = valueTotal
            .DebitValue = valueDebit
            .CreditValue = valueCredit
            .Observation = Observation
            .Status = accountReceivableDocument.Status
            .CurrencyId = CurrencyId
            .AccountReceivableDocumentDetail.Clear()
            If ListAccountReceivableDocumentDetail IsNot Nothing Then
                For Each itemdetail As AccountReceivableDocumentDetail In ListAccountReceivableDocumentDetail
                    .AccountReceivableDocumentDetail.Add(itemdetail)
                Next
            End If
            If ListDeleteAccountReceivableDocumentDetail IsNot Nothing Then
                For Each itemdetail As AccountReceivableDocumentDetail In ListDeleteAccountReceivableDocumentDetail
                    .AccountReceivableDocumentDetail.Add(itemdetail)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la orden de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                BarraBotones.StatusRecordVisible = True
                Using Model As New MAccountReceivableDocument(CStr(Me.Tag))
                    AsyncLoader(True)
                    accountReceivableDocument = Await Model.GetAccountReceivableDocumentByCode(INDbtnCode.Text.Trim)
                    INDLcAccountReceivableDocument.BeginUpdate()
                    If accountReceivableDocument IsNot Nothing AndAlso accountReceivableDocument.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New Presentation.Portfolio.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(accountReceivableDocument.Id))
                            With accountReceivableDocument
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select
                                Code = .Code
                                Me._idOperativeUnit = .OperatingUnitId
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                DocumentDate = .DocumentDate
                                CustomerId = .CustomerId
                                INDsleCustomerId.Properties.NullText = .DescriptionCustomer
                                MainAccountId = .MainAccountId
                                INDsleMainAccountId.Properties.NullText = .DescriptionAccount
                                CostCenterId = .CostCenterId
                                INDsleCostCenterId.Properties.NullText = .DescriptionCostCenter
                                InvoiceNumber = .InvoiceNumber
                                Term = .Term
                                ExpiredDate = .ExpiredDate
                                Share = .Share
                                Observation = .Observation
                                Me.CurrencyId(If(.CurrencyId Is Nothing,
                                              Me.indigo.CurrencyISO4217,
                                              .Currency?.Abbreviation)) = If(.CurrencyId Is Nothing,
                                                                                Me.indigo.OfficialCurrencyId, .CurrencyId)

                                Me.Status = .Status
                                ''validacion para bloquear el campo en caso de que este confirmado el registro
                                If .Status = 2 Then
                                    Me.INDsleCurrency.Enabled = False
                                Else
                                    Me.INDsleCurrency.Enabled = True
                                End If
                                ListAccountReceivableDocumentDetail = .AccountReceivableDocumentDetail.ToList()

                                INDsleMainAccountId.Properties.ReadOnly = True
                                INDspnTerm.Properties.ReadOnly = True
                                INDdeExpiredDate.Properties.ReadOnly = True
                                INDspnValueDebit.Properties.ReadOnly = True
                                INDspnValueCredit.Properties.ReadOnly = True

                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.accountReceivableDocument.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = accountReceivableDocument.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(accountReceivableDocument.Id, Me.Tag.ToString(), Nothing, GetType(AccountReceivableDocument).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDGcConcepts.DataSource = Nothing
                            INDGcConcepts.DataSource = ListAccountReceivableDocumentDetail
                            Me.EnableCurrencyControl()
                            If accountReceivableDocument.Status <> 1 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                INDbtnAdd.Enabled = False
                            End If
                            'Para la impresion
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, accountReceivableDocument.Id, 0, accountReceivableDocument.Id)
                            INDdeDocumentDate.Focus()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewAccountReceivableDocument()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDLcAccountReceivableDocument.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
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
        IndigoGridView1.SetListAcction(INDGvConcepts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvConcepts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddAccountReceivableDocumentDetail(sender As Object, e As AddConceptAccountReceivableDocumentDetailEventArgs)
        If ListAccountReceivableDocumentDetail Is Nothing Then
            ListAccountReceivableDocumentDetail = New List(Of AccountReceivableDocumentDetail)
        End If
        If e.EditMode = True Then
            ListAccountReceivableDocumentDetail.Remove(ItemAccountReceivableDocumentDetail)
            ListAccountReceivableDocumentDetail.Insert(indexEditRecord, e.ItemAccountReceivableDocumentDetail)
        Else
            ListAccountReceivableDocumentDetail.Add(e.ItemAccountReceivableDocumentDetail)
        End If
        INDGcConcepts.DataSource = Nothing
        INDGcConcepts.DataSource = ListAccountReceivableDocumentDetail
        EnableCurrencyControl()
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        ItemAccountReceivableDocumentDetail = DirectCast(INDGvConcepts.GetFocusedRow(), AccountReceivableDocumentDetail)
        indexEditRecord = ListAccountReceivableDocumentDetail.IndexOf(ItemAccountReceivableDocumentDetail)
        Using formulario As New FrmPopUpDetailAccountReceivableDocument
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddConceptAccountReceivableDocumentDetail, AddressOf ReturnAddAccountReceivableDocumentDetail
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.accountReceivableDocumentDetailEdit = ItemAccountReceivableDocumentDetail
            formulario.EditMode = True
            formulario.CurrencyAbbreviation = Me.CurrencyAbbreviation
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        ItemAccountReceivableDocumentDetail = DirectCast(INDGvConcepts.GetFocusedRow(), AccountReceivableDocumentDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ItemAccountReceivableDocumentDetail.Id > 0 Then
                If ListDeleteAccountReceivableDocumentDetail Is Nothing Then
                    ListDeleteAccountReceivableDocumentDetail = New List(Of AccountReceivableDocumentDetail)
                End If
                ItemAccountReceivableDocumentDetail.MarkAsDeleted()
                ListDeleteAccountReceivableDocumentDetail.Add(ItemAccountReceivableDocumentDetail)
            End If
            ListAccountReceivableDocumentDetail.Remove(ItemAccountReceivableDocumentDetail)
            INDGcConcepts.DataSource = Nothing
            INDGcConcepts.DataSource = ListAccountReceivableDocumentDetail
        End If
        EnableCurrencyControl()
    End Sub


    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        changeNumericFormatByCurrency(_culture.NumberFormat)
        If ctrTmp IsNot Nothing Then
            ctrTmp.CodeISO4217 = Me.CurrencyAbbreviation
            ctrTmp.RefreshTotalValues()
        End If
    End Sub

    ''' <summary>
    ''' habilita el control de la moneda si se tiene agregados detalles
    ''' </summary>
    Private Sub EnableCurrencyControl()
        INDsleCurrency.Enabled = If(ListAccountReceivableDocumentDetail?.Any(), False, True)
    End Sub
#End Region

#Region "Handles"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        Presenter = Nothing
        Model = Nothing
        accountReceivableDocument = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        ItemAccountReceivableDocumentDetail = Nothing
        ListDeleteAccountReceivableDocumentDetail = Nothing
        ListAccountReceivableDocumentDetail = Nothing
        OnlyRead = Nothing
        indexEditRecord = Nothing
        customer = Nothing
        mainAccount = Nothing
        valueDebit = Nothing
        valueCredit = Nothing
        valueTotal = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountReceivableDocument_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcAccountReceivableDocument, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        Presenter = New PAccountReceivableDocument(Me)
        Presenter.LoadDefinitionLayout()
        Presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        AddActionsColumns()
        Deshacer()
        LoadStatus()
        IndigoGridControl1.RefreshGrid(INDGcConcepts)
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountReceivableDocument_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    '''  Abre el popup para agregar el detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Using formulario As New FrmPopUpDetailAccountReceivableDocument
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddConceptAccountReceivableDocumentDetail, AddressOf ReturnAddAccountReceivableDocumentDetail
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.CurrencyAbbreviation = Me.CurrencyAbbreviation
            If customer Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Selecciona un tercero"
                Exit Sub
            Else
                formulario.thirdPartyId = customer.ThirdPartyId
            End If
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCustomerId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCustomerId.QueryPopUp
        If CustomerXpo Is Nothing Then
            Presenter.InitializeCustomerXPO()
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccountId.QueryPopUp
        If MainAccountXpo Is Nothing Then
            Presenter.InitializeAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenterId.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenterXPO()
        End If
    End Sub

    ''' <summary>
    ''' evento de consulta de las monedas parametrizadas en el sistema
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDataSourceXpo Is Nothing Then
            Presenter.InitializeCurrency()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCustomerId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCustomerId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCustomers With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCustomerXPO()
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterId_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenterId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAccountXPO()
        End If
    End Sub

#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewAccountReceivableDocument()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se dispara al cambiar el valor del cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCustomerId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCustomerId.EditValueChanged
        If INDsleCustomerId.EditValue Is Nothing OrElse INDsleCustomerId.EditValue = 0 Then
            Exit Sub
        End If
        'Cargar Campos que se obtienen del cliente
        Using modelCustomer As New MCustomers(Me.Tag)
            AsyncLoader(True)
            'cargamos la entidad del cliente para sacar el id de la cuenta y el plazo
            customer = modelCustomer.GetCustomerById(CustomerId)
            Using ModelAccount As New MPUC(Me.Tag)
                'cargamos la entidad de la cuenta
                mainAccount = ModelAccount.GetAccountByIdSimple(customer.MainAccountReceivableId, False)
                MainAccountId = mainAccount.Id
                INDsleMainAccountId.Properties.NullText = mainAccount.Number + " - " + mainAccount.Name
                Term = customer.Term
                ExpiredDate = DateAdd(DateInterval.Day, customer.Term, DocumentDate)
                If mainAccount.HandlesCostCenter Then
                    INDlciCostCenterId.Visibility = XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlciCostCenterId.Visibility = XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using
            AsyncLoader(False)
        End Using
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda escogida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If INDsleCurrency.EditValue IsNot Nothing AndAlso Me.CurrencyId > 0 AndAlso Me.CurrencySelected IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencyAbbreviation)
        End If
    End Sub

#End Region

#Region "DataSourceChanged"

    Private Sub INDGcConcepts_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcConcepts.DataSourceChanged
        ctrTmp.RefreshTotalValues()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.accountReceivableDocument IsNot Nothing AndAlso Me.accountReceivableDocument.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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
        accountReceivableDocument.Status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        accountReceivableDocument.Status = 1
        varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            varImp = 4
            accountReceivableDocument.Status = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            accountReceivableDocument.Status = 2
            varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            accountReceivableDocument.Status = 2
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, accountReceivableDocument.Id, 0, accountReceivableDocument.Id)
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


#End Region

End Class