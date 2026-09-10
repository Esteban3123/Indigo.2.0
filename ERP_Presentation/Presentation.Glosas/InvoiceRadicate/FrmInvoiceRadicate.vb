'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On :  
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports System.Text
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
#End Region

Public Class FrmInvoiceRadicate
    Implements IRadicateInvoice, ICustomizableForm

#Region "Variables y Propiedades"


    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Glosas"

    ''' <summary>
    ''' Nombre del modulo de Cuentas por cobrar
    ''' </summary>
    Private Const PortfolioNameModule As String = "Portfolio"

    Public Property DataSourceBranch As List(Of GlosasParametersInterface) Implements IRadicateInvoice.DataSourceBranch
        Get
            Return INDglCompany.Properties.DataSource
        End Get
        Set(value As List(Of GlosasParametersInterface))
            INDglCompany.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                INDglCompany.EditValue = value(0).ContainerName
            End If
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas
    ''' </summary>
    Public Property DataSourceInvoices As List(Of SP_invoiceList_Result) Implements IRadicateInvoice.DataSourceInvoices
        Get
            Return INDgcInvoices.DataSource
        End Get
        Set(value As List(Of SP_invoiceList_Result))
            INDgcInvoices.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas sin radicar
    ''' </summary>
    Public Property DataSourceObjectsInvoices As List(Of RadicateInvoiceD) Implements IRadicateInvoice.DataSourceObjectsInvoices
        Get
            Return INDgcRadicateD.DataSource
        End Get
        Set(value As List(Of RadicateInvoiceD))
            INDgcRadicateD.DataSource = value
            INDgcRadicateD.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Permite Asiganar la cantidad de factura radicadas por liberacion desde devolucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CountDevolution As Integer
        Get
            'Return INDlbCountInvoiceDevolution.Text
        End Get
        Set(value As Integer)
            'INDlbCountInvoiceDevolution.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id de clientes
    ''' </summary>
    ''' <remarks></remarks>
    Dim CustomerId As String

    ''' <summary>
    ''' objeto radicacion
    ''' </summary>
    Dim Objeto As RadicateInvoiceC

    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim CustomerTmp As Domain.Entities.Customer

    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo-
    ''' </summary>
    Dim Model As MRadicateInvoice

    ''' <summary>
    ''' Variable que se utiliza para instanciar el presentador
    ''' </summary>
    Dim Presenter As PRadicateInvoice

    ''' <summary>
    ''' Variable que contiene los valores de session
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Variable para almacenar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListObjectionsInvoice As List(Of RadicateInvoiceD)

    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String

    ''' <summary>
    ''' Variable Para almacenar la factura Seleccionada a glosar o reiterar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ObjetoD As RadicateInvoiceD

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Bandera para saber si el registro esta bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagBlockRecord As Boolean
    ''' <summary>
    ''' count de devoluciones
    ''' </summary>
    ''' <remarks></remarks>
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64
    ''' <summary>
    ''' Parametros de Glosas
    ''' </summary>
    Private _parameterGlosas As TimeParameters

    Public WriteOnly Property IdSequence As Int64 Implements IRadicateInvoice.IdSequence
        Set(value As Int64)
            _idCurrentSequense = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de mensajes de validacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim LIstMessage As New List(Of String)
    Private IdInvoiceRadicateConfirm As Integer = 0

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' propiedad que contiene el estado del oficio
    ''' </summary>
    Public Property StatusDocument As String Implements IRadicateInvoice.StatusDocument
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property
    ''' <summary>
    ''' Asigna la secuencia numerica de cartera para la realizacion de documentos de reclasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As Domain.Entities.PortfolioSequence Implements IRadicateInvoice.Sequense
        Get
            Return _sequence
        End Get
        Set(value As Domain.Entities.PortfolioSequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    ''' <summary>
    ''' obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IRadicateInvoice.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource
    ''' <summary>
    ''' Variable de Lista de Obj radicatedD XPO
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListViewRadicateD As XPCollection
    ''' <summary>
    ''' Lista de Radicado de rejilla principal
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListEntityRadicateD As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initialize
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDbteConsecutive".ToUpper()
    End Sub
#End Region

#Region "Metodos y Funciones"

    ''' <summary>
    ''' Retorna el mensaje de estado de una factura
    ''' </summary>
    ''' <param name="ObservationInvoiceCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StateErp(ObservationInvoiceCode) As String
        Dim StrMensaje As String = String.Empty
        Select Case ObservationInvoiceCode
            Case "1"
                StrMensaje = obtenerRecurso(FacturaSinRadicar, RecepcionObjeciones)
            Case "T"
                StrMensaje = obtenerRecurso(FacturaRadicadaSinConfirmar, RecepcionObjeciones)
            Case "6"
                StrMensaje = obtenerRecurso(FacturaAnulada, RecepcionObjeciones)
            Case Else
                StrMensaje = "Undefined"
        End Select
        Return StrMensaje
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StateInvalidate"), .StatusColor = System.Drawing.Color.OrangeRed})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()

        INDpceBuscaFactura.Text = String.Empty
        INDpceBuscaFactura.Hide()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecord = "1" 'Sin confirmar
        Objeto = Nothing
        CustomerTmp = Nothing
        INDbteConsecutive.Text = String.Empty
        CustomerId = Nothing
        INDbteNit.EditValue = Nothing
        INDdeDocumentDate.EditValue = Date.Now
        INDlblDateRadicate.Text = String.Empty
        INDglCompany.EditValue = Nothing
        INDmeComment.Text = String.Empty
        CountDevolution = 0
        RadicateInvoiceCId = 0
        ' _countdevolution = 0
        Me.ListEntityRadicateD = New List(Of RadicateInvoiceD)
        Me.ListViewRadicateD = Nothing 'Model.ListXPCollectionRadicateInvoiceD()
        Me.INDgcRadicateD.DataSource = Me.ListEntityRadicateD
        Me.INDgcRadicateD.RefreshDataSource()
        DataSourceInvoices = New List(Of SP_invoiceList_Result)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        Me.BarraBotones.StatusRecordVisible = False
        Me.INDbteNit.DisplayNullText = String.Empty
        Me.IndigoGridControl1.RefreshGrid(Me.INDgcRadicateD)
        Me.BarraBotones.CleanAuditBasic()

        If Indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        BlockedRecord()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRadicateInvoice.ActionsOnControls
        Set(ByVal value As Boolean)
            INDbteConsecutive.Enabled = Not value
            INDbteNit.Enabled = value
            INDdeDocumentDate.Enabled = value
            INDlblDateRadicate.Enabled = value
            INDglCompany.Enabled = value
            INDpceBuscaFactura.Enabled = value
            INDpceMore.Enabled = value
            INDgcRadicateD.Enabled = value
            If value = True Then
                Me.INDbteNit.Focus()
            Else
                Me.INDbteConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Elimina el Registro Bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Async Sub BlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
        Me.BarraBotones.EnableBarItems()
        Me.FlagBlockRecord = False
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Objeto
            .OperatingUnitId = Me._idOperativeUnit
            .DocumentDate = INDdeDocumentDate.EditValue
            .RadicatedDate = INDlblDateRadicate.Tag
            .CustomerId = INDbteNit.EditValue
            .State = 1
            .Comment = INDmeComment.Text
            .RadicatedUser = Indigo.UserIndigoId
            .RadicateInvoiceD.Clear()

            Dim list = (From e In ListEntityRadicateD Where e.Id = 0 Select e).ToList()
            For Each itemD As RadicateInvoiceD In list
                .RadicateInvoiceD.Add(itemD)
            Next
        End With
    End Sub

    Private Function RefreshXPOtoEntity(ByVal Objeto As RadicateInvoiceC) As Task
        Return Task.Factory.StartNew(Sub()
                                         ListEntityRadicateD = New List(Of RadicateInvoiceD)
                                         If Objeto.State = 4 Then
                                             ListViewRadicateD = Model.ListViewRadicateInvoiceDetail(Objeto.Id, True)
                                         Else
                                             ListViewRadicateD = Model.ListViewRadicateInvoiceDetail(Objeto.Id)
                                         End If

                                         If Me.ValidateVisibilityElectronicsRIPS(Objeto.State, ListViewRadicateD?.ToEntityList(Of PortfolioViewRadicateInvoiceDetailXpo)?. _
                                                                                                                    Select(Function(x) x.InvoiceDate)?.ToList()) Then

                                             Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
                                             Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Procesar,
                                                                               ResourceManager.GetString("GenerateERadication", PortfolioNameModule))
                                         Else
                                             Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
                                         End If
                                         Dim list = (From e In ListViewRadicateD Select e).ToList()
                                         For Each itemD As PortfolioViewRadicateInvoiceDetailXpo In list
                                             Dim _TmpObjectionsReceptionD As New RadicateInvoiceD
                                             With _TmpObjectionsReceptionD
                                                 .Id = itemD.Id
                                                 .CodeNameInvoiceCategory = itemD.CategoryName
                                                 .InvoiceNumber = itemD.InvoiceNumber
                                                 .InvoiceNumber = itemD.InvoiceNumber
                                                 .GlosasParametersInterfaceId = itemD.GlosasParametersInterfaceId
                                                 .BalanceInvoice = itemD.BalanceInvoice
                                                 .InvoiceValueEntity = itemD.InvoiceValueEntity
                                                 .InvoiceValuePacient = itemD.InvoiceValuePacient
                                                 .InvoiceValueFacade = .InvoiceValueEntity + IIf(.InvoiceValuePacient Is Nothing, 0, .InvoiceValuePacient)
                                                 .InvoiceDate = itemD.InvoiceDate
                                                 .IngressNumber = itemD.IngressNumber
                                                 .IngressDate = itemD.IngressDate
                                                 .UserNameInvoice = itemD.UserNameInvoice
                                                 .AccountantAccountCustomers = itemD.AccountantAccountCustomers
                                                 .PatientCode = itemD.PatientCode
                                                 .ContractCode = itemD.ContractCode
                                                 .PlanCode = itemD.PlanCode
                                                 .ContractEntity = itemD.ContractEntity
                                                 .PatientName = itemD.PatientName
                                                 .State = "1"
                                                 .CreditNoteValue = itemD.CreditNoteValue
                                                 .DebitNoteValue = itemD.DebitNoteValue
                                                 .Devolution = itemD.Devolution
                                                 .ConceptDevolution = itemD.ConceptDevolution
                                                 .CurrencyAbbreviation = If(itemD.CurrencyAbbreviation Is Nothing, Indigo.CurrencyISO4217, itemD.CurrencyAbbreviation)
                                                 If itemD.InvoiceDocumentType > 0 Then
                                                     .InvoiceDocumentType = itemD.InvoiceDocumentType
                                                 End If
                                                 .CUV = itemD.CUV
                                             End With
                                             ' Objeto.RadicateInvoiceD.Add(_TmpObjectionsReceptionD)
                                             ListEntityRadicateD.Add(_TmpObjectionsReceptionD)
                                         Next
                                         INDgcRadicateD.SafeInvoke(Sub(x) x.DataSource = ListEntityRadicateD)
                                         'INDgcRadicateD.SafeInvoke(Sub(x) x.RefreshDataSource())
                                     End Sub)

    End Function

    ''' <summary>
    ''' metodo que valida si debe o no mostrar el boton de generar RIPS electronico
    ''' </summary>
    ''' <param name="documentStatus"></param>
    ''' <param name="listInvoiceDate"></param>
    ''' <returns></returns>
    Private Function ValidateVisibilityElectronicsRIPS(documentStatus As ERadicateStatus, listInvoiceDate As List(Of DateTime)) As Boolean
        Return listInvoiceDate IsNot Nothing _
                AndAlso documentStatus = ERadicateStatus.Confirmed _
                AndAlso listInvoiceDate.Any(Function(x) DatePart(DateInterval.Year, x) >= 2024) _
                AndAlso Indigo?.LanguageCulture = "es-CO" AndAlso IdInvoiceRadicateConfirm > 0
    End Function


    Private Sub RefreshXPOtoEntity(ByVal listXpo As XPCollection)
        ListEntityRadicateD = New List(Of RadicateInvoiceD)
        Dim list = (From e In listXpo Select e).ToList()
        For Each itemD As Glosas_RadicateInvoiceDxpo In list
            Dim _TmpObjectionsReceptionD As New RadicateInvoiceD
            With _TmpObjectionsReceptionD
                .Id = itemD.Id
                .InvoiceNumber = itemD.InvoiceNumber
                .GlosasParametersInterfaceId = itemD.GlosasParametersInterfaceId
                .BalanceInvoice = itemD.BalanceInvoice
                .InvoiceValueEntity = itemD.InvoiceValueEntity
                .InvoiceValuePacient = itemD.InvoiceValuePacient
                .InvoiceValueFacade = .InvoiceValueEntity + IIf(.InvoiceValuePacient Is Nothing, 0, .InvoiceValuePacient)
                .InvoiceDate = itemD.InvoiceDate
                .IngressNumber = itemD.IngressNumber
                .IngressDate = itemD.IngressDate
                .UserNameInvoice = itemD.UserNameInvoice
                .AccountantAccountCustomers = itemD.AccountantAccountCustomers
                .PatientCode = itemD.PatientCode
                .ContractCode = itemD.ContractCode
                .PlanCode = itemD.PlanCode
                .ContractEntity = itemD.ContractEntity
                .PatientName = itemD.PatientName
                .State = "1"
                .CreditNoteValue = itemD.CreditNoteValue
                .DebitNoteValue = itemD.DebitNoteValue
                .Devolution = itemD.Devolution
                .ConceptDevolution = itemD.ConceptDevolution
            End With
            'Objeto.RadicateInvoiceD.Add(_TmpObjectionsReceptionD)
            ListEntityRadicateD.Add(_TmpObjectionsReceptionD)
        Next
        'ListEntityRadicateD = (From e In Objeto.RadicateInvoiceD Select e).ToList()
        INDgcRadicateD.DataSource = ListEntityRadicateD
        INDgcRadicateD.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Dim d As DateTime = GetServerDate()
        INDlblDateRadicate.Tag = d
        INDlblDateRadicate.Text = d.ToString("D", Indigo.Culture)
        ActionsOnControls = True
        INDbteConsecutive.Enabled = False
        If INDbteConsecutive.Text <> String.Empty Then
            AsyncLoader(True)
            Objeto = Await Model.GetRadicateInvoiceC(INDbteConsecutive.Text)
            If Objeto IsNot Nothing AndAlso Objeto.Id > 0 Then

                Dim result As New Domain.Entities.BlockRecord
                result = Await Model.GetBlockRecord(Me.Tag, Objeto.Id)
                With Objeto
                    RadicateInvoiceCId = .Id
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    INDbteNit.EditValue = .CustomerId
                    INDbteNit.DisplayNullText = .Customer.Nit & " - " & .Customer.Name
                    INDlblDateRadicate.Tag = .RadicatedDate
                    INDlblDateRadicate.Text = .RadicatedDate.ToString("D", Indigo.Culture)
                    INDdeDocumentDate.EditValue = .DocumentDate
                    INDglCompany.Enabled = True
                    Me.CustomerTmp = .Customer
                    INDmeComment.Text = .Comment
                    Me.BarraBotones.SetDocuments(Me.Objeto.Id)
                    CountDevolution = (From e In ListEntityRadicateD Where e.Devolution = True Select e).Count()
                    Me.StatusDocument = .State
                    If .State = "2" Then
                        IdInvoiceRadicateConfirm = .Id
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                        If FlagValidar = True Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
                        Else
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
                        End If
                        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                        Else
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = True
                        End If
                    ElseIf .State = "4" Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Else
                        IdInvoiceRadicateConfirm = .Id
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        If FlagValidar = True Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
                        Else
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
                        End If
                        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                        Else
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = True
                        End If
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                    End If
                End With
                INDbteNit.Enabled = False
                'envio a bloquear el registro
                If result IsNot Nothing Then
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.Indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.Indigo.UserIndigo, .IdRecord = Objeto.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                        FlagBlockRecord = False
                        Me.BarraBotones.PrintReport(PrintReportAction.None, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
                    Else
                        If result.CodUser.Trim().Equals(Me.Indigo.UserIndigo.Trim()) Then
                            Me.FlagBlockRecord = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                            Me.FlagBlockRecord = True
                        End If
                    End If
                End If
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Objeto.RadicatedConsecutive)
                Await RefreshXPOtoEntity(Objeto)
            Else
                If INDbteConsecutive.Text <> String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ConsecutivoObjecionNoExiste, RegisterObjection)
                    INDbteConsecutive.Text = String.Empty
                    ActionsOnControls = False
                    INDbteConsecutive.Enabled = True
                    INDbteConsecutive.Focus()
                Else
                    INDbteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
                    Objeto = New RadicateInvoiceC
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    'Me.BarraBotones.PrepareToolbar(eAction.Save)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                End If
            End If

            AsyncLoader(False)

            INDbteNit.Enabled = False
        Else
            INDbteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
            Objeto = New RadicateInvoiceC
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            ' Me.BarraBotones.PrepareToolbar(eAction.Save)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        End If
        If DataSourceBranch IsNot Nothing AndAlso DataSourceBranch.Count = 1 Then
            INDglCompany.EditValue = DataSourceBranch(0).ContainerName
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer

        Dim detailString As String = " [ "
        For Each d As RadicateInvoiceD In Me.ListEntityRadicateD
            If detailString.Length = 3 Then
                detailString &= String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.PatientCode.Trim(), d.PatientName.Trim().ToLower())
            Else
                detailString &= " - " & String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.PatientCode.Trim(), d.PatientName.Trim().ToLower())
            End If
        Next
        detailString &= " ]"

        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Objeto.Customer.Nit.Trim(), Me.Objeto.Customer.Name.Trim().ToLower(), Me.Objeto.RadicatedConsecutive.ToString, detailString), .CreationDate = dateServer, .CreationUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me.Objeto.RadicatedConsecutive & "#$", .IdForm = Me.Tag, .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Objeto.RadicatedConsecutive), .Update = dateServer, .UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Objeto.Customer.Nit.Trim(), Me.Objeto.Customer.Name.Trim().ToLower(), Me.Objeto.RadicatedConsecutive.ToString, detailString)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Objeto.RadicatedConsecutive)
            Return Me._doc
        End If
    End Function


    ''' <summary>
    ''' Metodo para cargar los campos del advanced Filter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAdvancedFilter(Optional ByVal ObjCompany As GlosasParametersInterface = Nothing)
        Dim ListObjectField As New List(Of ObjectField)

        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then


            If ObjCompany.AccountingMethod = eTypeInterface.FoxPublic Or ObjCompany.AccountingMethod = eTypeInterface.FoxPrivate Then
                ListObjectField.Add(New ObjectField("N° Factura", "car.cemnumfac", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Codigo Contrato", "sal.GECCODIGO", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Fecha Factura", "car.cemfecfac", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

                ListObjectField.Add(New ObjectField("Identificación", "com.gpacodigo", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Nombre Paciente", "com.gpanombre", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Apellido Paciente", "com.gpaapelli", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))


            ElseIf ObjCompany.AccountingMethod = eTypeInterface.NETPrivate Or ObjCompany.AccountingMethod = eTypeInterface.NETPublic Then


                ListObjectField.Add(New ObjectField("N° Factura", "car.cxcdocume", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Codigo Contrato", "CON.GDECODIGO", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Fecha Factura", "car.cxcdocfecha", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

                ListObjectField.Add(New ObjectField("Identificación", "pac.pacnumdoc", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Nombre Paciente", "pac.pacprinom", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Apellido Paciente", "pac.pacpriape", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))
            End If
        ElseIf Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then

            ListObjectField.Add(New ObjectField("N° Factura", "car.invoicenumber", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Grupo de atención", "careGr.code", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Entidad Administradora", "ha.code", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Categoría de factura", "Ic.code", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Fecha Factura", "car.accountreceivabledate", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

            ListObjectField.Add(New ObjectField("Identificación", "sal.PatientCode", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Nombre Paciente", "pac.IPPRINOMB", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Apellido Paciente", "pac.IPPRIAPEL", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

        End If

        CtrAdvancedFilter.ListTopResults.Clear()
        CtrAdvancedFilter.ListTopResults.Add(200)
        CtrAdvancedFilter.ListTopResults.Add(1000)
        CtrAdvancedFilter.ListTopResults.Add(2000)
        CtrAdvancedFilter.ListTopResults.Add(5000)

        Me.CtrAdvancedFilter.ListFields = ListObjectField

        RemoveHandler CtrAdvancedFilter.RunSearch, AddressOf RunShearch
        '  RemoveHandler CtrAdvancedFilter.InvalidValues, AddressOf InvalidFilter

        AddHandler CtrAdvancedFilter.RunSearch, AddressOf RunShearch
        AddHandler CtrAdvancedFilter.InvalidValues, AddressOf InvalidFilter

        INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarFiltros)
    End Sub


    ''' <summary>
    ''' Metodo que ejecuta la busqueda avanzada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub RunShearch(sender As Object, e As RunSearchEventArgs)
        If Me.INDbteNit.EditValue IsNot Nothing Then
            Dim _actionResult As New ActionResult(Of List(Of SP_invoiceList_Result))
            If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                If ObjCompany IsNot Nothing Then
                    AsyncLoader(True)
                    _actionResult = Await Model.GetInvoicesAll(ObjCompany.ContainerName, INDbteNit.EditValue, String.Empty, e.QueryString, e.TopResult, "1")
                    AsyncLoader(False)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                End If
            Else
                AsyncLoader(True)
                _actionResult = Await Model.GetInvoicesAll(String.Empty, INDbteNit.EditValue, String.Empty, e.QueryString, e.TopResult, "1")
                AsyncLoader(False)
            End If
            If _actionResult.StateResult = True Then
                If _actionResult.ObjectEmbbeded.Count > 0 Then
                    Me.DataSourceInvoices = _actionResult.ObjectEmbbeded
                    INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarListaFacturas)
                Else
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(NoHayRegistroCriteriosBusqueda, RecepcionObjeciones)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
        End If
    End Sub

    ''' <summary>
    ''' Metodo En caso de error al crear filtro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InvalidFilter()
        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.SeleccionesCamposParaFiltros)
    End Sub

    ''' <summary>
    ''' Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Limpiar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Confirmar oficio
    ''' </summary>
    ''' <param name="dateConfirm"></param>
    ''' <param name="comment"></param>
    ''' <remarks></remarks>
    Private Sub ConfirmRadicate(ByVal dateConfirm As DateTime, ByVal comment As String)
        If Me.Objeto Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
            Exit Sub
        End If
        Dim serverDate = Model.GetServerDate
        With Me.Objeto
            .DocumentDate = INDdeDocumentDate.EditValue
            .RadicatedDate = dateConfirm
            .ConfirmUser = Me.Indigo.UserIndigoId
            .ConfirmDateSystem = serverDate
            .ConfirmDate = dateConfirm
            .ConfirmComment = comment.Trim()
        End With
    End Sub


    ''' <summary>
    ''' Confirmar 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Confirm()
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim frmPopUp As New FrmConfirmationRadicate()
            frmPopUp.FechaDocumento = INDdeDocumentDate.EditValue
            frmPopUp.FechaServidor = Model.GetServerDate
            Using tras As New FrmTransparent(frmPopUp, False)
                If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    Me.ConfirmRadicate(frmPopUp.DateConfirm, frmPopUp.Comment)
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo base Guardar
    ''' </summary>
    Public Async Sub SaveOrUpdateAndConfirm(action As Integer)
        Try
            If Not Me.ValidateControls() Then
                Exit Sub
            End If
            If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                If _idOperativeUnit = 0 Then
                    Mensaje(EeventViewerImages.Informacion) = "Debe seleccionar una unidad operativa"
                    Exit Sub
                End If
            End If
            Me.AssigningValues()
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim frmPopUp As New FrmConfirmationRadicate()
                frmPopUp.FechaDocumento = INDdeDocumentDate.EditValue
                frmPopUp.FechaServidor = Model.GetServerDate
                Using tras As New FrmTransparent(frmPopUp, False)
                    If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                        Me.ConfirmRadicate(frmPopUp.DateConfirm, frmPopUp.Comment)
                    Else
                        Exit Sub
                    End If
                End Using
            Else
                Exit Sub
            End If

            'valido que la cantidad de facturas sea mayor a 2000 para enviarlas por lotes de 300
            If (From e In ListEntityRadicateD Where e.Id = 0 Select e).ToList().Count > 2000 Then
                AsyncLoader(True)
                Dim progress As CtrProgress
                progress = New CtrProgress
                progress.SetInfoFunction(AddressOf getInfo)
                progress.PrintInfo()
                progress.Dock = DockStyle.Fill
                AdditionalControlPanel.Controls.Add(progress)
                Dim listInfoProcessSaveImportFile As New List(Of Tuple(Of String, Integer))
                totalItems = ListEntityRadicateD.Count
                totalProcessedItems = 0
                progress.SetTitle = "Registros Guardados"
                progress.PrintInfo()



                Dim indexSend As Integer = 0
                Dim resultSave As ActionResult(Of String) = Nothing

                While ListEntityRadicateD.Count > 0
                    If RadicateInvoiceCId = 0 Then
                        With Objeto
                            .DocumentDate = INDdeDocumentDate.EditValue
                            .RadicatedDate = INDlblDateRadicate.Tag
                            .CustomerId = INDbteNit.EditValue
                            .State = 1
                            .Comment = INDmeComment.Text
                            .RadicatedUser = Indigo.UserIndigoId
                        End With
                    Else
                        Objeto = Model.GetRadicateInvoiceByIdSimple(RadicateInvoiceCId)
                        Objeto.MarkAsModified()
                    End If

                    Dim listSend = ListEntityRadicateD.Take(itemsSend).ToList()

                    Dim objLock As New Object()
                    Parallel.ForEach(listSend, Sub(x)
                                                   SyncLock objLock
                                                       Objeto.RadicateInvoiceD.Add(x)
                                                   End SyncLock
                                               End Sub)

                    Objeto.OperatingUnitId = Me._idOperativeUnit
                    Dim result = Await Model.SaveRadicateInvoiceC(Objeto)
                    If result.StateResult = False Then
                        Dim invoiceErros = String.Join("-", (From invoice In listSend Select invoice.InvoiceNumber).ToList())

                        listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Las facturas " + invoiceErros + " no se puedieron guardar", 2))
                    Else
                        RadicateInvoiceCId = CInt(result.ObjectEmbbeded.Id)
                    End If


                    If ListEntityRadicateD.Count < itemsSend Then
                        indexSend += ListEntityRadicateD.Count - 1
                        totalProcessedItems = totalItems
                        progress.PrintInfo()
                        ListEntityRadicateD.RemoveRange(0, ListEntityRadicateD.Count)
                    Else
                        indexSend += itemsSend
                        totalProcessedItems = indexSend
                        progress.PrintInfo()
                        ListEntityRadicateD.RemoveRange(0, itemsSend)
                    End If

                End While

                If listInfoProcessSaveImportFile.Count > 0 Then
                    Using formulario As New FrmListErrors(listInfoProcessSaveImportFile)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                    If Objeto.Id > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Se guardo la radicación, pero no se puedo confirmar"
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se guardo la radicación"
                    End If

                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    totalItems = 0
                    totalProcessedItems = 0
                    progress.PrintInfo()
                    AdditionalControlPanel.Controls.Clear()
                    Me.Deshacer()
                    RadicateInvoiceCId = 0
                    AsyncLoader(False)
                    Exit Sub
                End If

                Me.Cursor = System.Windows.Forms.Cursors.Default
                totalItems = 0
                totalProcessedItems = 0
                progress.PrintInfo()
                AdditionalControlPanel.Controls.Clear()
                RadicateInvoiceCId = 0
                Objeto.RadicateInvoiceD.Clear()
                ConfirmProcess()

            Else

                Me.AsyncLoader(True)
                ConfirmProcess()

            End If


        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' metodo encargado de confirmar el proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ConfirmProcess()
        Dim result = Await Model.SaveAndConfirmRadicateC(Me.Objeto, Me._idCurrentSequense)
        If result.StateResult = True Then
            Me.Objeto = Await Model.GetRadicateInvoiceC(result.ObjectEmbbeded.RadicatedConsecutive)
            Mensaje(EeventViewerImages.Informacion) = result.Message
            Me.BarraBotones.SetDocuments(Me.Objeto.Id)
            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
            If result.StateResultAux = True Then
                If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.ImprimirReporte) Then
                    If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesImprimir, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
                    End If
                End If
            End If
            Me.Deshacer()
        Else
            If result.Message IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
            Deshacer()
        End If
        Me.AsyncLoader(False)
        Me.INDbteConsecutive.Focus()
    End Sub


    Dim RadicateInvoiceCId As Integer = 0
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
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 2000
    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

    ''' <summary>
    ''' Guarda oficio
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of RadicateInvoiceC) = Nothing

            'valido que la cantidad de facturas sea mayor a 2000 para enviarlas por lotes de 300
            If (From e In ListEntityRadicateD Where e.Id = 0 Select e).ToList().Count > 2000 Then

                Dim progress As CtrProgress
                progress = New CtrProgress
                progress.SetInfoFunction(AddressOf getInfo)
                progress.PrintInfo()
                progress.Dock = DockStyle.Fill
                AdditionalControlPanel.Controls.Add(progress)
                Dim listInfoProcessSaveImportFile As New List(Of Tuple(Of String, Integer))
                totalItems = ListEntityRadicateD.Count
                totalProcessedItems = 0
                progress.SetTitle = "Registros Guardados"
                progress.PrintInfo()



                Dim indexSend As Integer = 0
                Dim resultSave As ActionResult(Of String) = Nothing

                While ListEntityRadicateD.Count > 0
                    If RadicateInvoiceCId = 0 Then
                        With Objeto
                            .DocumentDate = INDdeDocumentDate.EditValue
                            .RadicatedDate = INDlblDateRadicate.Tag
                            .CustomerId = INDbteNit.EditValue
                            .State = 1
                            .Comment = INDmeComment.Text
                            .RadicatedUser = Indigo.UserIndigoId
                        End With
                    Else
                        Objeto = Model.GetRadicateInvoiceByIdSimple(RadicateInvoiceCId)
                        Objeto.MarkAsModified()
                    End If

                    Dim listSend = ListEntityRadicateD.Take(itemsSend).ToList()

                    Dim objLock As New Object()
                    Parallel.ForEach(listSend, Sub(x)
                                                   SyncLock objLock
                                                       Objeto.RadicateInvoiceD.Add(x)
                                                   End SyncLock
                                               End Sub)

                    Objeto.OperatingUnitId = Me._idOperativeUnit
                    result = Await Model.SaveRadicateInvoiceC(Objeto)
                    If result.StateResult = False Then
                        Dim invoiceErros = String.Join("-", (From invoice In listSend Select invoice.InvoiceNumber).ToList())

                        listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Las facturas " + invoiceErros + " no se puedieron guardar", 2))
                    Else
                        RadicateInvoiceCId = CInt(result.ObjectEmbbeded.Id)
                        If ListEntityRadicateD.Count < itemsSend Then
                            listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (totalItems).ToString() + " se guardaron correctamente", 1))

                        Else
                            listInfoProcessSaveImportFile.Add(New Tuple(Of String, Integer)("Los items del " + (indexSend + 1).ToString() + " hasta " + (indexSend + itemsSend).ToString() + " se guardaron correctamente", 1))
                        End If

                    End If


                    If ListEntityRadicateD.Count < itemsSend Then
                        indexSend += ListEntityRadicateD.Count - 1
                        totalProcessedItems = totalItems
                        progress.PrintInfo()
                        ListEntityRadicateD.RemoveRange(0, ListEntityRadicateD.Count)
                    Else
                        indexSend += itemsSend
                        totalProcessedItems = indexSend
                        progress.PrintInfo()
                        ListEntityRadicateD.RemoveRange(0, itemsSend)
                    End If

                End While

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
                RadicateInvoiceCId = 0
            Else
                AssigningValues()
                result = Await Model.SaveRadicateInvoiceC(Objeto)
                If result.StateResult = True Then
                    INDbteConsecutive.Text = result.ObjectEmbbeded.RadicatedConsecutive
                    Objeto = Await Model.GetRadicateInvoiceC(result.ObjectEmbbeded.RadicatedConsecutive)
                    Await RefreshXPOtoEntity(Objeto)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                    ' Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                    ' Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
                    If Objeto IsNot Nothing Then
                        If varImp = 1 Then
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
                        Else
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
                        End If
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    End If
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    'Se indexa la información
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    If result.Message IsNot Nothing Then
                        If result.Message = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
                AsyncLoader(False)
                Deshacer()
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Informacion) = ex.Message.ToString
        End Try
    End Sub

    ''' <summary>
    ''' Logica de barra de botones
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Mensaje
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
    ''' Limpiar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Abrir busqueda de consecutivos como terceros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Select Case Me._openFindSenser.ToUpper
            Case Me.INDbteConsecutive.Name.ToUpper() 'Abre el frontal para buscar consecutivo
                AbrirBusquedaConsecutive()
                'Case Me.INDbteNit.Name.ToUpper() 'Abre el frontal para buscar nits
                '    AbrirBusquedaCustomers()
            Case Else 'Si esta sobre un control en donde no aplica el metodo buscar
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoAplicaBuscar, Eform.Comunes)
        End Select
    End Sub

    ''' <summary>
    ''' Abrirs the busqueda consecutive.
    ''' </summary>
    Public Sub AbrirBusquedaConsecutive()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValueObjection
        With FormSearchObjects
            .ValorSolicitado = "RadicatedConsecutive"
            .ListaColumnas = {New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName"},
                                New ColumnInfo With {.Caption = "N° Factura", .FieldName = "InvoiceNumber"},
                                New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate"},
                                New ColumnInfo With {.Caption = "N° Consecutivo", .FieldName = "RadicatedConsecutive"},
                                New ColumnInfo With {.Caption = "Validación", .FieldName = "RIPSStatusName"},
                                New ColumnInfo With {.Caption = "Estado", .FieldName = "HeaderState"}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceRadicateDetail
            BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            .FormParent = Me
            .ShowSearch(False)
        End With
    End Sub

    ' ''' <summary>
    ' ''' Abrirs the busqueda de terceros
    ' ''' </summary>
    'Public Sub AbrirBusquedaCustomers()
    '    FormSearchObjects = New FrmBusqueda
    '    AddHandler FormSearchObjects.ReturnValueWithList, AddressOf ReturnValueCustomers
    '    'lista de string de solicitados
    '    Dim listSolicitado As List(Of String) = New List(Of String)
    '    listSolicitado.Add("Nit")
    '    listSolicitado.Add("Name")
    '    listSolicitado.Add("Id")
    '    With FormSearchObjects
    '        .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
    '        .ListadoSolicitud = listSolicitado
    '        .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
    '        .FormParent = Me
    '        .ShowSearch(False)
    '    End With
    'End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValueObjection(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteConsecutive.Text = ReturnValue
        BlockedRecord()
        BarraBotones.RibbonPagEform.Visible = True
        LoadControls()
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    'Private Sub ReturnValueCustomers(ByVal ReturnValue As String, ByVal ReturnObject As Object, ByVal ReturnList As List(Of String))
    '    'variable de la lista de string devueltos
    '    Dim listDevuelto As List(Of String)
    '    listDevuelto = ReturnList
    '    Dim ctomerTmp = CType(ReturnObject, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
    '    Dim ctomer = CType(ctomerTmp.OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.GlosasCustomerXpo)
    '    Me.CustomerTmp = New Domain.Entities.Customer With {.Id = ctomer.Id, .Nit = ctomer.Nit.ToString().Trim(), .Name = ctomer.Name.ToString().Trim(), .State = ctomer.State}
    '    If (FormSearchObjects.ListadoDevolucion.Count > 0) Then
    '        INDbteNit.Text = FormSearchObjects.ListadoDevolucion.Item(0)
    '        INDlblEntity.Text = FormSearchObjects.ListadoDevolucion.Item(1)
    '        CustomerId = FormSearchObjects.ListadoDevolucion.Item(2)
    '        Me.INDgcInvoices.DataSource = Nothing
    '    End If
    'End Sub


    ''' <summary>
    ''' Funcion que agrega un item a la rejilla de detalle de recepcion
    ''' </summary>
    ''' <param name="itemInvoice"></param>
    ''' <remarks></remarks>
    Private Sub ItemAddRejilla(ByVal itemInvoice As SP_invoiceList_Result)
        'lista temporal que guardar las facturas agregadas para despues eliminarlas de la rejilla de facturas del PopUp
        Dim tmpListaSP_invoiceList_ResultBorrar As List(Of SP_invoiceList_Result) = New List(Of SP_invoiceList_Result)
        'validamos que la cuenta este configurada en los parametros de interfaz
        Dim _InterfaceId? As Integer
        Dim AccountValidate As Boolean
        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            AccountValidate = True
            _InterfaceId = Nothing
        ElseIf Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDglCompany.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                Exit Sub
            End If
            Dim ObjInterfaceParameter As GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, GlosasParametersInterface)
            _InterfaceId = ObjInterfaceParameter.Id
            If ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                AccountValidate = Model.ValidateAccountTableFOxPrivate(itemInvoice.AccountantAccountCustomers, False)
            Else
                AccountValidate = True
            End If
        End If
        'si no existe la cuenta en configuracion 
        If AccountValidate = False Then
            Dim str As String = obtenerRecurso(NoExisteCuentaConfigurada, Eform.ParametersInterfaz) & " " & itemInvoice.InvoiceNumber & " - " & itemInvoice.AccountantAccountCustomers
            LIstMessage.Add(str)
        Else
            If Not ValidateInvoiceType(Nothing, itemInvoice).StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = "No se pueden mezclar facturas capitadas con facturas de ventas"
                Exit Sub
            End If

            Dim _TmpObjectionsReceptionD As New RadicateInvoiceD
            With _TmpObjectionsReceptionD
                .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                .GlosasParametersInterfaceId = _InterfaceId
                .BalanceInvoice = itemInvoice.BalanceInvoice.ToString.Trim
                .InvoiceValueEntity = itemInvoice.InvoiceValueEntity.ToString.Trim
                .InvoiceValuePacient = itemInvoice.InvoiceValuePacient.ToString.Trim
                .InvoiceValueFacade = .InvoiceValueEntity + IIf(.InvoiceValuePacient Is Nothing, 0, .InvoiceValuePacient)
                .InvoiceDate = itemInvoice.InvoiceDate
                .IngressDate = itemInvoice.IngressDate
                .IngressNumber = itemInvoice.IngressNumber.Trim
                .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                .PatientCode = If(itemInvoice.PatientCode Is Nothing, String.Empty, itemInvoice.PatientCode.Trim)
                .ContractCode = itemInvoice.ContractCode.Trim
                .PlanCode = itemInvoice.CodePlan
                .ContractEntity = itemInvoice.ContractEntity
                .PatientName = If(itemInvoice.PatientName Is Nothing, String.Empty, itemInvoice.PatientName.Trim)
                .State = "1"
                .CreditNoteValue = itemInvoice.CreditNoteValue
                .DebitNoteValue = itemInvoice.DebitNoteValue
                .Devolution = itemInvoice.Devolution
                .ConceptDevolution = itemInvoice.ConceptDevolution
                .InvoiceDocumentType = itemInvoice.DocumentType
            End With
            'valido que la factura agregar no se encuentre ya en la lista
            Dim List = From c In ListEntityRadicateD Where c.InvoiceNumber = _TmpObjectionsReceptionD.InvoiceNumber
            If List.Count = 0 Then
                tmpListaSP_invoiceList_ResultBorrar.Add(itemInvoice)
                ListEntityRadicateD.Add(_TmpObjectionsReceptionD)
            End If
            CountDevolution = (From e As RadicateInvoiceD In ListEntityRadicateD Where e.Devolution = True Select e).Count()
            INDgcRadicateD.DataSource = ListEntityRadicateD
            INDgcRadicateD.RefreshDataSource()
            Me.INDbteNit.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Funcion que agrega un item a la rejilla de detalle de recepcion
    ''' </summary>
    ''' <param name="ListitemInvoice"></param>
    ''' <remarks></remarks>
    Private Async Sub ItemAddRejillamultiple(ByVal ListitemInvoice As List(Of SP_invoiceList_Result))
        Dim ListitemInvoiceTmp = (From e In ListitemInvoice Where e.Selection = True Select e).ToList()
        Dim tmpListaSP_invoiceList_ResultBorrar As List(Of SP_invoiceList_Result) = New List(Of SP_invoiceList_Result)
        Dim _InterfaceId? As Integer
        Dim ObjInterfaceParameter As GlosasParametersInterface = Nothing
        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            _InterfaceId = Nothing
        ElseIf Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDglCompany.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                Exit Sub
            End If
            ObjInterfaceParameter = CType(INDglCompany.GetSelectedDataRow, GlosasParametersInterface)
            _InterfaceId = ObjInterfaceParameter.Id
            If ObjInterfaceParameter.AccountingMethod = eTypeInterface.FoxPrivate Then
                Dim listStrAccounting As List(Of String) = (From e As SP_invoiceList_Result In ListitemInvoiceTmp Select e.AccountantAccountCustomers.Trim).Distinct().ToList()
                Dim ListAccountSettingsFOX_PrivateMethod As List(Of AccountSettingsFOX_PrivateMethod) = Await Model.ValidateListInvoiceAccountTableFOxPrivate()
                Dim ListaccountingNotConfigure As List(Of String) = (From e As AccountSettingsFOX_PrivateMethod In ListAccountSettingsFOX_PrivateMethod Where Not listStrAccounting.Contains(e.InvoiceNotRadicate) Select e.InvoiceNotRadicate).ToList()
                ListitemInvoiceTmp = (From e As SP_invoiceList_Result In ListitemInvoiceTmp Where Not ListaccountingNotConfigure.Contains(e.AccountantAccountCustomers.Trim) Select e).ToList()
                If ListaccountingNotConfigure.Count > 0 Then
                    Dim stringBuilder As New StringBuilder
                    For Each AccountingInvoice As String In ListaccountingNotConfigure
                        stringBuilder.AppendLine(obtenerRecurso(NoExisteCuentaConfigurada, Eform.ParametersInterfaz) & " - " & AccountingInvoice)
                    Next
                    Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString()
                End If
            End If
        End If
        'valido que la factura agregar no se encuentre ya en la lista
        Dim listtmp = (From e As SP_invoiceList_Result In ListitemInvoiceTmp Where Not Enumerable.Cast(Of RadicateInvoiceD)(Me.ListEntityRadicateD).Any(Function(a) a.InvoiceNumber.Equals(e.InvoiceNumber)) Select e).ToList()
        If Not ValidateInvoiceType(listtmp, Nothing).StateResult Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pueden mezclar facturas capitadas con facturas de ventas"
            Exit Sub
        End If
        AsyncLoader(True)
        For Each itemInvoice As SP_invoiceList_Result In listtmp
            ''si no existe la cuenta en configuracion 
            Dim _TmpObjectionsReceptionD As New RadicateInvoiceD
            With _TmpObjectionsReceptionD
                .InvoiceNumber = itemInvoice.InvoiceNumber
                .GlosasParametersInterfaceId = _InterfaceId
                .BalanceInvoice = itemInvoice.BalanceInvoice
                .CodeNameInvoiceCategory = itemInvoice.InvoiceCategory
                .InvoiceValueEntity = itemInvoice.InvoiceValueEntity
                .InvoiceValuePacient = itemInvoice.InvoiceValuePacient
                .InvoiceValueFacade = .InvoiceValueEntity + IIf(.InvoiceValuePacient Is Nothing, 0, .InvoiceValuePacient)
                .InvoiceDate = itemInvoice.InvoiceDate
                .IngressNumber = itemInvoice.IngressNumber
                .IngressDate = itemInvoice.IngressDate
                .UserNameInvoice = itemInvoice.UserNameInvoice.Trim
                .AccountantAccountCustomers = itemInvoice.AccountantAccountCustomers.Trim
                .PatientCode = If(itemInvoice.PatientCode Is Nothing, String.Empty, itemInvoice.PatientCode.Trim)
                .ContractCode = itemInvoice.ContractCode
                .PlanCode = itemInvoice.CodePlan
                .ContractEntity = itemInvoice.ContractEntity
                .PatientName = If(itemInvoice.PatientName Is Nothing, String.Empty, itemInvoice.PatientName.Trim)
                .State = "1"
                .CreditNoteValue = itemInvoice.CreditNoteValue
                .DebitNoteValue = itemInvoice.DebitNoteValue
                .Devolution = itemInvoice.Devolution
                .ConceptDevolution = itemInvoice.ConceptDevolution
                .InvoiceDocumentType = itemInvoice.DocumentType
                .CurrencyAbbreviation = itemInvoice.CurrencyAbbreviation
            End With
            tmpListaSP_invoiceList_ResultBorrar.Add(itemInvoice)
            ListEntityRadicateD.Add(_TmpObjectionsReceptionD)
        Next
        INDgcRadicateD.DataSource = ListEntityRadicateD
        INDgcRadicateD.RefreshDataSource()
        AsyncLoader(False)
        CountDevolution = (From e As RadicateInvoiceD In ListEntityRadicateD Where e.Devolution = True Select e).Count()
        Me.INDbteNit.Enabled = False
    End Sub

    ''' <summary>
    ''' valida que las facturas que se van a agregar sean del mismo tipo 
    ''' </summary>
    ''' <param name="listtmp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateInvoiceType(listtmp As List(Of SP_invoiceList_Result), itemInvoice As SP_invoiceList_Result) As ActionResult
        If listtmp Is Nothing Then
            listtmp = New List(Of SP_invoiceList_Result)
            listtmp.Add(itemInvoice)
        End If
        Dim listCaiptalInvoice As New List(Of RadicateInvoiceD)
        If ListEntityRadicateD IsNot Nothing AndAlso ListEntityRadicateD.Count > 0 Then
            listCaiptalInvoice = ListEntityRadicateD.FindAll(Function(x) x.InvoiceDocumentType IsNot Nothing AndAlso x.InvoiceDocumentType = 4)
        End If
        If listCaiptalInvoice.Count > 0 Then
            Dim listValidation = listtmp.FindAll(Function(x) x.DocumentType IsNot Nothing AndAlso x.DocumentType <> 4)
            If listValidation.Count > 0 Then
                Return New ActionResult With {.StateResult = False}
            End If
        Else
            Dim listDocumentType = (From e In listtmp Where e.DocumentType IsNot Nothing Select e.DocumentType).Distinct().ToList()
            If listDocumentType.Count > 1 Then
                If listDocumentType.Find(Function(x) x.Value = 4) IsNot Nothing Then
                    Return New ActionResult With {.StateResult = False}
                End If
            End If
        End If
        Return New ActionResult With {.StateResult = True}
    End Function
    ''' <summary>
    ''' Metodo para anular
    ''' </summary>
    Private Sub InvalidateOrConfirm(ByVal Confirm As Boolean)
        If Objeto Is Nothing Or Objeto.State = 2 Then
            Exit Sub
        Else
            Dim msj As String = obtenerRecurso(ComunesPreguntaAnular)
            If MessageIndigo.Show(msj, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                InvalidateObjc()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub InvalidateObjc()
        Try
            Dim StateTmp As String = Objeto.State
            Objeto.State = 4 'Anulo
            AsyncLoader(True)
            Objeto.OperatingUnitId = Me._idOperativeUnit
            Dim result As ActionResult(Of RadicateInvoiceC) = Await Model.SaveRadicateInvoiceC(Objeto)
            If result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesAnuladoCorrectamente)
                Me.BarraBotones.PrintReport(PrintReportAction.Cancel, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
                Deshacer()
            Else
                If result.Message IsNot Nothing Then
                    If result.Message = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador)
                End If
                Objeto.State = StateTmp
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' contar el numero de devolcuiones cargadas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CounDevolution()
        If ListEntityRadicateD IsNot Nothing AndAlso ListEntityRadicateD.Count > 0 Then
            CountDevolution = (From e As RadicateInvoiceD In ListEntityRadicateD Where e.Devolution = True Select e).Count()
        End If
    End Sub


    ''' <summary>
    ''' cargar un Tercero ya persistido
    ''' </summary>
    Private Function INDbteNit_KeyDown(ByVal nit As String) As Domain.Entities.Customer
        CustomerTmp = Model.GetCustomerByNitSimple(nit)
        If CustomerTmp IsNot Nothing AndAlso CustomerTmp.Id > 0 Then
            INDdeDocumentDate.Focus()
        Else
            Me.INDbteNit.DisplayNullText = String.Empty
            Me.INDbteNit.EditValue = Nothing
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ClienteNoExite, Eform.Conciliation)
            Me.INDbteNit.Focus()
        End If
        Return CustomerTmp
    End Function


    ''' <summary>
    ''' Pega en la rejilla de facturas los datos de la clipboard
    ''' </summary>
    Private Async Sub PasteToGridInvoices()
        If Me.INDbteNit IsNot Nothing AndAlso Me.INDbteNit.EditValue IsNot Nothing And Objeto IsNot Nothing Then
            If Objeto.State = 1 Or Objeto.State Is Nothing Then
                Dim list As List(Of String) = Me.GetInvoiceFromClipboard()
                If list.Count > 0 Then
                    Dim _result As New ActionResult(Of List(Of RadicateInvoiceD))
                    Dim _InterfaceId? As Integer
                    Me.AsyncLoader(True)
                    If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                        Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                        If ObjCompany IsNot Nothing Then
                            _InterfaceId = ObjCompany.Id
                            _result = Await Model.ValidateListInvoiceRadicateDSp(list, Me.INDbteNit.EditValue, ObjCompany.ContainerName)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                        End If
                    Else
                        _InterfaceId = Nothing
                        _result = Await Model.ValidateListInvoiceRadicateDSp(list, Me.INDbteNit.EditValue, String.Empty)
                    End If
                    Dim strMensaje As String = String.Empty
                    If _result.MessageResult IsNot Nothing AndAlso _result.MessageResult.Count > 0 Then
                        For Each itemMensaje As String In _result.MessageResult
                            strMensaje += itemMensaje + Environment.NewLine
                        Next
                        Mensaje(EeventViewerImages.Informacion) = strMensaje
                    End If
                    Dim listInvoice = _result.ObjectEmbbeded
                    If listInvoice IsNot Nothing AndAlso listInvoice.Count > 0 Then
                        For Each itemInvoice As RadicateInvoiceD In listInvoice
                            itemInvoice.GlosasParametersInterfaceId = _InterfaceId
                            ListEntityRadicateD.Add(itemInvoice)
                        Next
                    End If
                    INDgcRadicateD.DataSource = ListEntityRadicateD
                    INDgcRadicateD.RefreshDataSource()
                    Me.AsyncLoader(False)
                    If Me.ListEntityRadicateD.Count > 0 Then
                        Me.INDbteNit.Enabled = False
                    End If
                Else
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(NoExisteInformacionAPegar, RecepcionObjeciones)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene una lista de numeros de facturas
    ''' almacenada en la Clipboard
    ''' </summary>
    ''' <returns>Lista de numeros de facturas</returns>
    Private Function GetInvoiceFromClipboard() As List(Of String)
        Dim list As New List(Of String)()
        If Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If ObjCompany Is Nothing Then
                Return list
            End If
            If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length >= 1 Then
                        If Me.ListEntityRadicateD Is Nothing Then
                            Me.ListEntityRadicateD = New List(Of RadicateInvoiceD) ' Model.ListXPCollectionRadicateInvoiceD()
                        End If
                        If Me.IsValidString(item(0).Trim()) Then
                            If Not item(0).Trim().Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = item(0).Trim() Select i).Any AndAlso Not (From f As RadicateInvoiceD In Me.ListEntityRadicateD Where f.InvoiceNumber.Contains(item(0).Trim()) Select f).Any Then
                                Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(item(0).Trim(), ObjCompany.AccountingMethod)
                                list.Add(invoiceNumberFixed)
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If
        Else
            If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length >= 1 Then
                        If Me.ListEntityRadicateD Is Nothing Then
                            Me.ListEntityRadicateD = New List(Of RadicateInvoiceD)
                        End If
                        If Me.IsValidString(item(0).Trim()) Then
                            If Not item(0).Trim().Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = item(0).Trim() Select i).Any AndAlso Not (From f As RadicateInvoiceD In Me.ListEntityRadicateD Where f.InvoiceNumber.Contains(item(0).Trim()) Select f).Any Then
                                list.Add(item(0).Trim())
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If
        End If
        Return list
    End Function

    ''' <summary>
    ''' Valida una cadena de caracteres descartando que tenga caracteres especiales
    ''' </summary>
    ''' <param name="str">Cadena a validar</param>
    ''' <returns>Valor que indica si la cadena es valida</returns>
    Private Function IsValidString(ByVal str As String) As Boolean
        Dim pattern As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_ "
        For Each c As Char In str
            If Not pattern.Contains(c) Then
                Return False
            End If
        Next
        Return True
    End Function


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomers()
        Using msearch As New MBusqueda
            DatasourceCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Customers)
            INDbteNit.Datasource = DatasourceCustomers
        End Using
    End Sub


    ''' <summary>
    ''' Guardar fecha y cometario de confirmacion
    ''' </summary>
    ''' <remarks></remarks>
    'Private Async Sub ConfirmAcept()
    '    If Me.Objeto.Id > 0 Then
    '        Me.AsyncLoader(True)
    '        Dim result As ActionResult = Await Model.ConfirmRadicateInvoiceC(Me.Objeto, Me._sequense.PortfolioSequenceDetail(0).Id)
    '        Dim strMessageConcat As New StringBuilder
    '        If result.MessageResult.Count > 0 Then
    '            For Each item As String In result.MessageResult
    '                strMessageConcat.AppendLine(item.ToString)
    '            Next
    '            Mensaje(EeventViewerImages.MensajeError) = strMessageConcat.ToString
    '        End If
    '        Me.AsyncLoader(False)
    '    End If
    'End Sub


    ''' <summary>
    ''' Elimina Item de la rejilla de detalle de objeciones
    ''' </summary>
    Private Async Sub DeleteItem()
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        If Me.INDgvRadicateD.SelectedRowsCount > 1 Then '
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                'Dim listObjXPO = New List(Of Glosas_RadicateInvoiceDxpo)
                Dim listObjEntity = New List(Of RadicateInvoiceD)
                Dim listStrDelete As New List(Of String)
                For Each item As Integer In INDgvRadicateD.GetSelectedRows()
                    If item > -1 Then
                        Dim data As RadicateInvoiceD = TryCast(INDgvRadicateD.GetRow(item), RadicateInvoiceD)
                        If data IsNot Nothing Then
                            If Objeto.State <> "2" And Objeto.State <> "4" Then
                                listObjEntity.Add(data)
                                listStrDelete.Add(data.InvoiceNumber)
                            End If
                        End If
                    End If
                Next

                For Each Data As RadicateInvoiceD In listObjEntity 'limpiamos los iteam que aun no han sidio guardados
                    If Data.Id = 0 Then
                        If listStrDelete.Contains(Data.InvoiceNumber) Then
                            ListEntityRadicateD.Remove(Data)
                            listStrDelete.Remove(Data.InvoiceNumber)
                        End If
                    End If
                Next
                Me.INDgcRadicateD.RefreshDataSource()
                If listStrDelete.Count > 0 Then
                    Dim result As ActionResult
                    AsyncLoader(True)
                    result = Await Model.DeleteListInvoiceD(listStrDelete)
                    If result.StateResult = True Then
                        ListViewRadicateD = Model.ListXPCollectionRadicateInvoiceD(Objeto.Id)
                        RefreshXPOtoEntity(ListViewRadicateD)
                        'Me.INDgcRadicateD.DataSource = ListViewRadicateD
                        'Me.INDgcRadicateD.RefreshDataSource()
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            Dim strmessage As New StringBuilder
                            For Each item In result.MessageResult
                                strmessage.AppendLine(item.ToString)
                            Next
                            Mensaje(EeventViewerImages.Advertencia) = strmessage.ToString
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador, Eform.Comunes)
                        End If
                    End If
                    listStrDelete.Clear()
                    AsyncLoader(False)
                End If
            End If
        Else
            If Objeto.State <> "2" And Objeto.State <> "4" Then  'no se pueden eliminar facturas de oficio confirmado o anulados
                Dim obj As RadicateInvoiceD = Me.INDgvRadicateD.GetRow(Me.INDgvRadicateD.FocusedRowHandle)
                If obj.Id = 0 Then
                    ListEntityRadicateD.Remove(obj)
                    INDgcRadicateD.DataSource = ListEntityRadicateD
                    INDgcRadicateD.RefreshDataSource()
                Else
                    DeleteItemDetail(obj)
                End If
            End If
        End If
        CounDevolution()
    End Sub

    ''' <summary>
    ''' METODO: Eliminar De la Columna de Acciones
    ''' </summary>
    Public Async Sub DeleteItemDetail(ByVal _RadicateInvoiceD As RadicateInvoiceD)
        If _RadicateInvoiceD IsNot Nothing Then
            If _RadicateInvoiceD.Id > 0 Then
                If Objeto.State = 4 Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeRealizarAccionAnulado, RecepcionObjeciones)
                    Exit Sub
                End If
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim result As ActionResult
                    AsyncLoader(True)
                    Dim listStrDelete As New List(Of String)
                    listStrDelete.Add(_RadicateInvoiceD.InvoiceNumber)
                    result = Await Model.DeleteListInvoiceD(listStrDelete)
                    If result.StateResult = True Then
                        ListViewRadicateD = Model.ListXPCollectionRadicateInvoiceD(Objeto.Id)
                        RefreshXPOtoEntity(ListViewRadicateD)
                        'Me.INDgcRadicateD.DataSource = ListViewRadicateD
                        'Me.INDgcRadicateD.RefreshDataSource()
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeEliminarPorMovimiento, Eform.RecepcionObjeciones)
                    End If
                    AsyncLoader(False)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(NoPuedeEliminarPorMovimiento)
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesSeleccioneRegistroEliminar)
        End If
    End Sub

    ''' <summary>
    ''' Elimina Item de la rejilla de detalle de objeciones
    ''' </summary>
    Private Async Sub DeleteItemCheck()
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            ' Dim listObjXPO = New List(Of Glosas_RadicateInvoiceDxpo)
            Dim listObjEntity = New List(Of RadicateInvoiceD)
            Dim listStrDelete As New List(Of String)
            For Each data As RadicateInvoiceD In INDgvRadicateD.DataSource
                If data IsNot Nothing Then
                    If Objeto.State <> "2" And Objeto.State <> "4" And data.Selection = True Then  'no se pueden eliminar facturas de oficio confirmado o anulados
                        listObjEntity.Add(data)
                        listStrDelete.Add(data.InvoiceNumber)
                    End If
                End If
            Next

            For Each Data As RadicateInvoiceD In listObjEntity 'limpiamos los iteam que aun no han sidio guardados
                If Data.Id = 0 Then
                    If listStrDelete.Contains(Data.InvoiceNumber) Then
                        ListEntityRadicateD.Remove(Data)
                        listStrDelete.Remove(Data.InvoiceNumber)
                    End If
                End If
            Next
            Me.INDgcRadicateD.RefreshDataSource()
            If listStrDelete.Count > 0 Then
                Dim result As ActionResult
                AsyncLoader(True)
                result = Await Model.DeleteListInvoiceD(listStrDelete)
                If result.StateResult = True Then
                    ListViewRadicateD = Model.ListXPCollectionRadicateInvoiceD(Objeto.Id)
                    RefreshXPOtoEntity(ListViewRadicateD)
                    'Me.INDgcRadicateD.DataSource = ListViewRadicateD
                    'Me.INDgcRadicateD.RefreshDataSource()
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Dim strmessage As New StringBuilder
                        For Each item In result.MessageResult
                            strmessage.AppendLine(item.ToString)
                        Next
                        Mensaje(EeventViewerImages.Advertencia) = strmessage.ToString
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador, Eform.Comunes)
                    End If
                End If
                AsyncLoader(False)
            End If
        End If
        CounDevolution()
    End Sub
    ''' <summary>
    ''' Logica para cargar los controles dependiendo si maneja o no decimales.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadParameters() As Task
        Using model As New MTimeParameters(Me.Tag)
            Me._parameterGlosas = Await model.GetTimeParameters("0", Me._idOperativeUnit)
            If Me._parameterGlosas Is Nothing OrElse Me._parameterGlosas.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de glosas para la unidad operativa seleccionada"
                Exit Function
            ElseIf Me._parameterGlosas.ManageDecimals = False Then 'No maneja decimales.
                'Grid cabecera
                For Each item In INDgvRadicateD.Columns
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
            End If
        End Using
    End Function



#End Region

#Region "Barra Botones"
    Dim FlagValidar As Boolean = False

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Validar) = True Then
            FlagValidar = True
        Else
            FlagValidar = False
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Clcik Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
        LoadControls()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de personalizacion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        '  Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de reestablecer la definicion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'Me.ResetLayout()
    End Sub

    ''' <summary>
    ''' Activar o desactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        InvalidateOrConfirm(False)
    End Sub


    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        DeleteItemCheck()
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Dim reportDef As New Reporter.rptAccountsReceivable(Me.BarraBotones.OperatingUnit)
        'reportDef.INDRadicateCId = Me.Objeto.Id
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Me.Objeto.Id, 0, {Me.Objeto.Id, Me.BarraBotones.OperatingUnitValue})
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_GenerarArchivo() Handles BarraBotones.Click_GenerateFile
        Dim frmPopUpRips As FrmPopupRIPS = New FrmPopupRIPS()
        frmPopUpRips.StartPosition = FormStartPosition.CenterScreen
        frmPopUpRips.Size = New System.Drawing.Size(620, 450)
        Dim frmTransparent As New FrmTransparent(frmPopUpRips, False)
        frmPopUpRips.IdInvoiceRadicateConfirm = IdInvoiceRadicateConfirm
        frmPopUpRips.ConsecutiveRadicateInvoice = INDbteConsecutive.EditValue
        If ListEntityRadicateD IsNot Nothing Then
            Dim List = ListEntityRadicateD.Where(Function(d) d.Selection).Select(Function(i) i.InvoiceNumber).ToList()
            Dim ListInvoices As New List(Of String)
            List.ForEach(Sub(a)
                             a = String.Format("'{0}'", a)
                             ListInvoices.Add(a)
                         End Sub)

            Dim IdsInvoices = String.Join(",", ListInvoices)
            Dim invoice = Model.ListInvoiceIds(IdsInvoices)
            frmPopUpRips.InvoicesList = invoice.Select(Function(i) New RIPSBilling With {.InvoiceId = i.Id,
                                                                                         .InvoiceDate = i.InvoiceDate,
                                                                                         .AdmissionNumber = i.AdmissionNumber,
                                                                                         .InvoiceNumber = i.InvoiceNumber}).ToList()
        End If
        frmTransparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1) 'Insert
    End Sub


    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de actualizar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2) 'Update
    End Sub

    ''' <summary>
    ''' Click en validar facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Validar() Handles BarraBotones.Click_Validar
        validar()
    End Sub

    ''' <summary>
    ''' Evento click, para generar Radicacion Electronica
    ''' </summary>
    Private Async Sub BarraBotones_GenerateElectronicsRIPS() Handles BarraBotones.ClickProcesar
        Try
            AsyncLoader(True)
            Dim listSelectInvoice = ListEntityRadicateD?.FindAll(Function(d) d.Selection AndAlso DatePart(DateInterval.Year, d.InvoiceDate) >= 2024)?.Select(Function(i) i.InvoiceNumber)?.ToList()

            If IdInvoiceRadicateConfirm = 0 OrElse listSelectInvoice Is Nothing OrElse Not listSelectInvoice?.Any() Then
                Throw New Exception("El documento de radicacion No esta Confirmado")
            End If

            Using model As New MRadicateInvoice("")
                Dim result = Await model.GenerateERadication(listSelectInvoice,
                                                       NameOf(RadicateInvoiceC), IdInvoiceRadicateConfirm)

                If result Is Nothing OrElse Not result?.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                Else
                    Mensaje(EeventViewerImages.Informacion) = result?.Message
                End If

            End Using

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Generar Archivos Planos"

    Private Sub validar()
        If INDbteNit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
            Exit Sub
        End If
        Dim ObjCompany As Domain.Entities.GlosasParametersInterface = Nothing
        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            ObjCompany = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If ObjCompany Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                Exit Sub
            End If
        End If
        Using frmValidate As New FrmInvoiceValidate
            frmValidate.Nit = INDbteNit.EditValue
            frmValidate.Containers = ObjCompany
            frmValidate.DatasourceListRadicateD = ListEntityRadicateD
            AddHandler frmValidate.RemoveFromRadicateList, AddressOf RemoveInvoiceList
            Dim fr = New FrmTransparent(frmValidate, False)
            fr.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub RemoveInvoiceList(sender As Object, e As RemoveListRadicateEventArgs)
        If Objeto.State <> "2" And Objeto.State <> "4" Then  'no se pueden eliminar facturas de oficio confirmado o anulados
            If e.ListToRemoveInvoiceNumber Is Nothing OrElse e.ListToRemoveInvoiceNumber.Count = 0 Then
                Exit Sub
            End If
            Dim errorDelete As New StringBuilder()
            AsyncLoader(True)
            For Each InvoiceNumber In e.ListToRemoveInvoiceNumber
                Dim obj As RadicateInvoiceD = ListEntityRadicateD.Where(Function(x) x.InvoiceNumber.Equals(InvoiceNumber)).FirstOrDefault()
                If obj.Id = 0 Then
                    ListEntityRadicateD.Remove(obj)
                    INDgcRadicateD.DataSource = ListEntityRadicateD
                    INDgcRadicateD.RefreshDataSource()
                Else
                    If obj.Id > 0 Then
                        Dim result As ActionResult

                        Dim listStrDelete As New List(Of String)
                        listStrDelete.Add(obj.InvoiceNumber)
                        result = Await Model.DeleteListInvoiceD(listStrDelete)
                        If result.StateResult = True Then
                            ListViewRadicateD = Model.ListXPCollectionRadicateInvoiceD(Objeto.Id)
                            RefreshXPOtoEntity(ListViewRadicateD)
                        Else
                            errorDelete.AppendLine(String.Format("No se pudo eliminar la factura {0}", InvoiceNumber))
                        End If
                    Else
                        errorDelete.AppendLine(String.Format("La factura {0} no se puede eliminar porque tiene movimientos", InvoiceNumber))
                    End If
                End If
            Next
            AsyncLoader(False)
            If errorDelete.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorDelete.ToString()
            Else
                Mensaje(EeventViewerImages.Informacion) = "Las facturas se eliminaron con éxito"
            End If
        End If
        CounDevolution()
    End Sub

#End Region

#Region "Handles"

#Region "FormClosing"
    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        BlockedRecord()
    End Sub

#End Region

#Region "Constructor"

#End Region

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        CustomerId = Nothing
        Objeto = Nothing
        CustomerTmp = Nothing
        Model = Nothing
        Presenter = Nothing
        ListObjectionsInvoice = Nothing
        _openFindSenser = Nothing
        ObjetoD = Nothing
        record = Nothing
        FlagBlockRecord = Nothing
        _sequence = Nothing
        _idCurrentSequense = Nothing
        DatasourceCustomers = Nothing
        ListViewRadicateD = Nothing
        ListEntityRadicateD = Nothing
        varImp = Nothing
    End Sub


    Private Async Sub FrmInvoiceRadicate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._funct = AddressOf GenerateDoc
        IndigoGridView1.MoreInfoColunmns(INDgvRadicateD)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MRadicateInvoice(Me.Tag)
        Me.Indigo = SessionValues.Instance
        '******************************'
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = AddressOf Me.INDbteNit_KeyDown
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecordEnabled = False

        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRadicateD, True)
        Me.IndigoGridControl1.RefreshGrid(Me.INDgcRadicateD)

        Presenter = New PRadicateInvoice(Me)
        Presenter.Initializes()
        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.Presenter.GetSequense()
            '_idCurrentSequense = Me._sequence.PortfolioSequenceDetail(0).Id
        Else
            INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            _idCurrentSequense = 0
        End If
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Deshacer()
        Await LoadParameters()
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' cambio de fecha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeDocumentDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeDocumentDate.EditValueChanged
        If CDate(INDdeDocumentDate.EditValue) > Date.Now Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(seleccioneFechafininvalida, RecepcionObjeciones)
            INDdeDocumentDate.EditValue = Date.Now
        End If
    End Sub

    ''' <summary>
    ''' Aplicar Filtro Cada Ves Que se Escriba en el Txt de Buscar Factura
    ''' </summary>
    Private Sub INDpceBuscaFactura_EditValueChanged(sender As Object, e As EventArgs) Handles INDpceBuscaFactura.EditValueChanged
        INDgcvInvoices.ApplyFindFilter(INDpceBuscaFactura.Text)
    End Sub
    ''' <summary>
    ''' Evento de eliminar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnEliminar_Click(sender As Object, e As EventArgs) Handles INDbtnEliminar.Click, RepositoryItemButtonEditActions.Click
        If FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
        Else
            DeleteItem()
        End If
    End Sub

    ''' <summary>
    ''' Cambio de compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDglCompany.EditValueChanged
        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If ObjCompany IsNot Nothing Then
                LoadAdvancedFilter(ObjCompany)
            End If
        End If
    End Sub
#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Query popup de mas informacion del detalle del radicado 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemPopupContainerEditMoreInfo_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEditMoreInfo.QueryPopUp
        Dim mainView1 As GridView = INDgvRadicateD
        Dim ObjD As RadicateInvoiceD = TryCast(INDgvRadicateD.GetRow(INDgvRadicateD.FocusedRowHandle), RadicateInvoiceD)
        CtrXtraInfoConceptDevolution.ListProperties.Clear()
        If ObjD.ConceptDevolution IsNot Nothing Then
            CtrXtraInfoConceptDevolution.ListProperties.Add(New XtraInfoProperty("Devolución por:", ObjD.ConceptDevolution, 80))
        End If
    End Sub
    ''' <summary>
    ''' abrir busqueda de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBuscaFactura_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceBuscaFactura.QueryPopUp
        If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDglCompany.EditValue IsNot Nothing Then
                INDpceBuscaFactura.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                e.Cancel = True
            End If
        Else
            LoadAdvancedFilter()
            INDpceBuscaFactura.Focus()
        End If
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteNit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDbteNit.QueryPopUp
        If INDbteNit.Datasource Is Nothing Then
            LoadXpoCustomers()
        End If
    End Sub
#End Region

#Region "GotFocus"
    ''' <summary>
    ''' Pone la variable _openFindSender en vacio para evitar
    ''' abrir el formulario de busqueda en un control que no aplique la funcionalidad
    ''' </summary>
    Private Sub OpenFindSender_LostFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.LostFocus
        Me._openFindSenser = String.Empty
    End Sub
    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Aqui se implementa la funcionalidad de pegar facturas desde la clipboard
    ''' </summary>
    Private Sub INDgcvObjetions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgvRadicateD.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Me.PasteToGridInvoices()
        End If
    End Sub
    ''' <summary>
    ''' Evento keydown del control de consecutivo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                Exit Sub
            End If
            LoadControls()
            INDbteConsecutive.Enabled = False
        ElseIf e.KeyCode = Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub
    ''' <summary>
    ''' Buscar Una Factura del ERP
    ''' </summary>
    Private Async Sub INDTxtBuscaFactura_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceBuscaFactura.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            Dim Invoice As SP_invoiceList_Result = Nothing
            If INDpceBuscaFactura.Text <> String.Empty Then
                If INDbteNit.EditValue IsNot Nothing Then
                    If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                        Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDglCompany.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                        If ObjCompany IsNot Nothing Then
                            Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(INDpceBuscaFactura.Text.Trim(), ObjCompany.AccountingMethod)
                            AsyncLoader(True)
                            Invoice = Await Model.GetInvoice(ObjCompany.ContainerName, INDbteNit.EditValue, invoiceNumberFixed, String.Empty, 1)
                            AsyncLoader(False)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                        End If
                    Else
                        AsyncLoader(True)
                        Invoice = Await Model.GetInvoice(String.Empty, INDbteNit.EditValue, INDpceBuscaFactura.Text.Trim(), String.Empty, 1)
                        AsyncLoader(False)
                    End If
                    If Invoice IsNot Nothing AndAlso Invoice.InvoiceNumber IsNot Nothing Then
                        If Invoice.StateCurrentInvoice <> "1" Then
                            Dim ComplementoMensaje As String = StateErp(Invoice.StateCurrentInvoice)
                            Mensaje(EeventViewerImages.Informacion) = String.Format(obtenerRecurso(NoSePuedeAgregarFacturaEstado, RecepcionObjeciones), ComplementoMensaje)
                            INDpceBuscaFactura.Focus()
                        Else
                            LIstMessage = New List(Of String)
                            ItemAddRejilla(Invoice)
                            If LIstMessage.Count > 0 Then
                                Dim stringBuilder As New System.Text.StringBuilder()
                                For Each item As String In LIstMessage
                                    stringBuilder.AppendLine(item.ToString)
                                Next
                                Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString
                            End If
                            LIstMessage = New List(Of String)
                            INDpceBuscaFactura.EditValue = Nothing
                        End If
                    Else
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(NoseEncuentraFactura, RecepcionObjeciones)
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FaltaNitTercero, RecepcionObjeciones)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Popup"

#End Region

#Region "Click"

    ''' <summary>
    ''' Metodo para Agregar una Factura a la rejilla de detalle
    ''' </summary>
    Private Sub INDbtnAgregarFactura_Click(sender As Object, e As EventArgs) Handles INDbtnAgregarFactura.Click
        'lista temporal que guardar las facturas agregadas para despues eliminarlas de la rejilla de facturas del PopUp
        Dim tmpListaSP_invoiceList_ResultBorrar As List(Of SP_invoiceList_Result) = New List(Of SP_invoiceList_Result)
        LIstMessage = New List(Of String)
        'recorro las facturas
        If DataSourceInvoices IsNot Nothing Then
            '  AsyncLoader(True)
            ItemAddRejillamultiple(DataSourceInvoices)
            If LIstMessage.Count > 0 Then
                Dim stringBuilder As New System.Text.StringBuilder()
                For Each item As String In LIstMessage
                    stringBuilder.AppendLine(item.ToString)
                Next
                Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString
            End If
            LIstMessage = New List(Of String)
            Parallel.For(0, Me.DataSourceInvoices.Count, Sub(i)
                                                             Me.DataSourceInvoices(i).Selection = False
                                                         End Sub)

            'Actualizo la el datasource del PopPup de facturas
            For Each item As SP_invoiceList_Result In tmpListaSP_invoiceList_ResultBorrar
                Me.DataSourceInvoices.Remove(item)
            Next
            Me.INDgcInvoices.RefreshDataSource()
        End If
        INDchkSelectAll.Checked = False
        INDchkSelectFiltered.Checked = False
    End Sub

    ''' <summary>
    ''' Cick Sobre el boton para oculatr filtros o rejilla en la busqueda avanzada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtndisplayAdvancedSearch_Click(sender As Object, e As EventArgs) Handles INDBtndisplayAdvancedSearch.Click
        If INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarFiltros)
        Else

            INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarListaFacturas)
        End If
    End Sub

    Private Sub INDbtnValidateInvoice_Click(sender As Object, e As EventArgs)
        validar()
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Abrir busqueda de consecutivos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteConsecutive.ButtonClick
        Me.AbrirBusquedaConsecutive()
    End Sub
#End Region

#Region "PopupMenuShowing"


    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgcvObjetions_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgvRadicateD.PopupMenuShowing
        If e.Menu Is Nothing Then
            e.Menu = New DevExpress.XtraGrid.Menu.GridViewMenu(sender)
        End If

        If Objeto Is Nothing Then
            Exit Sub
        End If

        If e.HitInfo.RowHandle < 0 Then
            If Objeto IsNot Nothing Then
                If Objeto.State = 1 Or Objeto.State Is Nothing Then
                    e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuPegarFacturaConciliacion, Eform.Conciliation), AddressOf PasteToGridInvoices, My.Resources.pegar32))
                    Exit Sub
                End If
            End If
        End If

        If Objeto IsNot Nothing Then
            If Objeto.State = 1 Then
                e.Menu.Items.Add(New DXMenuItem((obtenerRecurso(MenuPegarFacturaConciliacion, Eform.Conciliation)), AddressOf PasteToGridInvoices, My.Resources.pegar32))
            End If
        End If
        If Objeto.State = 1 Or Objeto.State Is Nothing Then
            e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuEliminar, RecepcionObjeciones), AddressOf DeleteItem, My.Resources.eliminarLineaAzul))
        End If
    End Sub
#End Region

#Region "MouseDoubleClick"
    ''' <summary>
    ''' Evento para chekear todos los item de la rejilaa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvRadicateD_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcRadicateD.MouseDoubleClick
        Dim hitPoint = Me.INDgvRadicateD.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("Clselection") Then
                Dim count As Integer
                For Each item As RadicateInvoiceD In Me.INDgvRadicateD.DataSource
                    If item.Selection = True Then
                        count += 1
                    End If
                Next
                If count = Me.ListEntityRadicateD.Count Then
                    For Each item As RadicateInvoiceD In ListEntityRadicateD
                        item.Selection = False
                    Next
                Else
                    For Each item As RadicateInvoiceD In ListEntityRadicateD
                        item.Selection = True
                    Next
                End If
                Me.INDgcRadicateD.DataSource = Me.ListEntityRadicateD
                Me.INDgcRadicateD.RefreshDataSource()
                Me.INDgvRadicateD.Invalidate()
            End If
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Objeto IsNot Nothing AndAlso Me.Objeto.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDbteConsecutive.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Activated"
    Private Sub FrmInvoiceRadicate_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteConsecutive.Enabled Then
            INDbteConsecutive.Focus()
        End If
    End Sub

#End Region

#Region "OpenFormButtonClick"
    Private Sub INDbteNit_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDbteNit.OpenFormButtonClick
        OpenForm(503, Nothing, True)
        LoadXpoCustomers()
    End Sub
#End Region

#Region "SelectionChanged"
    ''' <summary>
    ''' Evento cuando se seleccionan celdas de la rejilla de detalle de factura donde hacemos la sumatioria de las celdas seleccionada
    ''' siempre y la columna sea numerica
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
    Private Sub INDgvRadicateD_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDgvRadicateD.SelectionChanged
        Dim Currency As Boolean = True
        Dim sum As Decimal = 0
        For Each c As GridCell In Me.INDgvRadicateD.GetSelectedCells()
            If c.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric Then
                sum += Convert.ToDecimal(Me.INDgvRadicateD.GetRowCellValue(c.RowHandle, c.Column))
                If c.Column.DisplayFormat.FormatString = String.Empty Then
                    Currency = False
                End If
            End If
        Next
        If Currency = True Then
            'Me.INDlblSumValue.Text = sum.MoneyFormat
        Else
            'Me.INDlblSumValue.Text = sum.MoneyFormat(0)
        End If
    End Sub

#End Region

#Region "CheckedChanged"
    ''' <summary>
    ''' seleccionar todos los registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDchkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles INDchkSelectAll.CheckedChanged
        If DataSourceInvoices IsNot Nothing AndAlso DataSourceInvoices.Count > 0 Then
            If INDchkSelectAll.EditValue = True Then
                For Each item As SP_invoiceList_Result In DataSourceInvoices
                    item.Selection = True
                Next
            Else
                For Each item As SP_invoiceList_Result In DataSourceInvoices
                    item.Selection = False
                Next
            End If
            INDgcInvoices.RefreshDataSource()
        End If
    End Sub

    Private Sub INDchkSelectFiltered_CheckedChanged(sender As Object, e As EventArgs) Handles INDchkSelectFiltered.CheckedChanged
        If DataSourceInvoices IsNot Nothing AndAlso DataSourceInvoices.Count > 0 Then
            If INDchkSelectFiltered.EditValue = True Then
                For Each item As SP_invoiceList_Result In Me.INDgcvInvoices.DataController.GetAllFilteredAndSortedRows()
                    item.Selection = True
                Next
            Else
                For Each item As SP_invoiceList_Result In Me.INDgcvInvoices.DataController.GetAllFilteredAndSortedRows()
                    item.Selection = False
                Next
            End If
            INDgcInvoices.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "CustomColumnSort"

    Private Sub INDgcvInvoices_CustomColumnSort(sender As Object, e As CustomColumnSortEventArgs) Handles INDgcvInvoices.CustomColumnSort
        If e.Column.FieldName <> "InvoiceNumber" Then
            Return
        End If

        Dim Value1_NumberPart As Integer
        Dim Value1_TextPart As String

        Dim Value1_NumberString As String = RegularExpressions.Regex.Match(e.Value1.ToString(), "\d+").Value
        If Value1_NumberString <> "" Then
            Value1_NumberPart = Convert.ToInt32(Value1_NumberString)
            Value1_TextPart = e.Value1.ToString().Replace(Value1_NumberString, "").ToLower()
        Else
            Value1_NumberPart = 0
            Value1_TextPart = e.Value1.ToString().ToLower()
        End If

        Dim Value2_NumberPart As Integer
        Dim Value2_TextPart As String
        Dim Value2_NumberString As String = RegularExpressions.Regex.Match(e.Value2.ToString(), "\d+").Value
        If Value2_NumberString <> "" Then
            Value2_NumberPart = Convert.ToInt32(Value2_NumberString)
            Value2_TextPart = e.Value2.ToString().Replace(Value2_NumberString, "").ToLower()
        Else
            Value2_NumberPart = 0
            Value2_TextPart = e.Value2.ToString().ToLower()
        End If

        e.Handled = True
        If Value1_TextPart <> Value2_TextPart Then
            e.Result = System.Collections.Comparer.Default.Compare(e.Value1, e.Value2)
        Else
            If Value1_NumberPart > Value2_NumberPart Then
                e.Result = 1
            Else
                e.Result = -1
            End If
        End If
    End Sub
#End Region

#Region "Enum"
    ''' <summary>
    ''' enumerador con los estado de la radicacion
    ''' </summary>
    Private Enum ERadicateStatus
        Unconfirmed = 1
        Confirmed = 2
        Cancelled = 4
    End Enum
#End Region

#End Region

End Class