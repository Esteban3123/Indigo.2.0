'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 09-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Portfolio.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.Windows.Forms
Imports System.Text
Imports DevExpress.Xpo
Imports Presentation.Accounting
Imports System.ComponentModel

#End Region
Public Class FrmSettingPortfolio
    Implements ISettingPortfolio

#Region "GLOBALS"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordPortfolio

    ''' <summary>
    ''' Parametros de Facturación
    ''' </summary>
    Private _parameterBilling As SettingsBilling

    ''' <summary>
    ''' The presenter
    ''' </summary>
    Dim presenter As PSettingPortfolio

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim settingPortfolio As SettingPortfolio
    ''' <summary>
    ''' entidad de las edades de cartera
    ''' </summary>
    ''' <remarks></remarks>
    Dim agesPortfolio As AgesPortfolio
    ''' <summary>
    ''' listado de las edades de cartera
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPortfolioAge As List(Of AgesPortfolio)
    ''' <summary>
    ''' listado de las edades de cartera eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPortfolioAgeDelete As List(Of AgesPortfolio)
    ''' <summary>
    ''' bandera para saber si se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim editPopup As Boolean

    ''' <summary>
    ''' Lista de legal collection
    ''' </summary>
    Dim legalcollection As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Objeto con las propiedades del segmento Deterioro de Cartera Factura Básica
    ''' </summary>
    Private _deteriorationBasicBillingPortfolio As DeteriorationBasicBillingPortfolio

    ''' <summary>
    ''' Objeto con las propiedades del segmento Reglas de Deterioro por Clasificación
    ''' </summary>
    Private _rulesDeteriorationClassification As RulesDeteriorationClassification

#End Region

#Region "Properties"

    Public Property JournalVoucherTypeDeteriorationAccountId As Integer? Implements ISettingPortfolio.JournalVoucherTypeDeteriorationAccountId
        Get
            Return INDsleJournalVoucherTypeDeteriorationAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleJournalVoucherTypeDeteriorationAccountId.EditValue = value
        End Set
    End Property

    Public Property JournalVoucherTypeDeteriorationAccountIdXpo As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeDeteriorationAccountIdXpo
        Get
            Return INDsleJournalVoucherTypeDeteriorationAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleJournalVoucherTypeDeteriorationAccountId.Properties.DataSource = value
        End Set
    End Property

    Public Property ProvisionPercentage As Decimal Implements ISettingPortfolio.ProvisionPercentage
        Get
            Return INDseProvisionPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseProvisionPercentage.EditValue = value
        End Set
    End Property

    Public Property DeteriorationPercentage As Decimal Implements ISettingPortfolio.DeteriorationPercentage
        Get
            Return INDseDeteriorationPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseDeteriorationPercentage.EditValue = value
        End Set
    End Property

    Public Property NameMaximumAgeRange As String Implements ISettingPortfolio.NameMaximumAgeRange
        Get
            Return INDTxtNameMaximumAgeRange.Text
        End Get
        Set(value As String)
            INDTxtNameMaximumAgeRange.Text = value
        End Set
    End Property

    Public Property NameMinimumAgeRange As String Implements ISettingPortfolio.NameMinimumAgeRange
        Get
            Return INDTxtNameMinimumAgeRange.Text
        End Get
        Set(value As String)
            INDTxtNameMinimumAgeRange.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingPortfolio.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' id de la nota credito
    ''' </summary>
    Public Property JournalVoucherTypeCreditNotesId As Integer? Implements ISettingPortfolio.JournalVoucherTypeCreditNotesId
        Get
            Return INDSleCreditNote.EditValue
        End Get
        Set(value As Integer?)
            INDSleCreditNote.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' id de la nota debito
    ''' </summary>
    Public Property JournalVoucherTypeDebitNotesId As Integer? Implements ISettingPortfolio.JournalVoucherTypeDebitNotesId
        Get
            Return INDSleDebitNote.EditValue
        End Get
        Set(value As Integer?)
            INDSleDebitNote.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' id de radicacion de cuentas
    ''' </summary>
    Public Property JournalVoucherTypeFilingAccountId As Integer? Implements ISettingPortfolio.JournalVoucherTypeFilingAccountId
        Get
            Return INDSleAccountFiling.EditValue
        End Get
        Set(value As Integer?)
            INDSleAccountFiling.EditValue = value
        End Set
    End Property
    Public Property JournalVoucherTypeProvisionId As Integer? Implements ISettingPortfolio.JournalVoucherTypeProvisionId
        Get
            Return INDSleProvision.EditValue
        End Get
        Set(value As Integer?)
            INDSleProvision.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' id del traslado
    ''' </summary>
    Public Property JournalVoucherTypeTranslationId As Integer? Implements ISettingPortfolio.JournalVoucherTypeTranslationId
        Get
            Return INDSleTransfer.EditValue
        End Get
        Set(value As Integer?)
            INDSleTransfer.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece el estado
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As Boolean Implements ISettingPortfolio.State

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante para los documentos de cuenta por cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JournalVoucherTypeDocumentAccountReceivableId As Integer Implements ISettingPortfolio.JournalVoucherTypeDocumentAccountReceivableId
        Get
            Return INDsleJournalVoucherTypeDocumentAccountReceivableId.EditValue
        End Get
        Set(value As Integer)
            INDsleJournalVoucherTypeDocumentAccountReceivableId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el listado de tipos de comprobantes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JournalVoucherTypeDocumentAccountReceivableXpo As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeDocumentAccountReceivableXpo
        Get
            Return INDsleJournalVoucherTypeDocumentAccountReceivableId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleJournalVoucherTypeDocumentAccountReceivableId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los tipos de comprobante
    ''' </summary>
    Public Property JournalVoucherTypeCreditNotesXPO As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeCreditNotesXPO
        Get
            Return CType(INDSleCreditNote.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCreditNote.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece los tipos de comprobante
    ''' </summary>
    Public Property JournalVoucherTypeDebitNotesXPO As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeDebitNotesXPO
        Get
            Return CType(INDSleDebitNote.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDebitNote.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece los tipos de comprobante
    ''' </summary>
    Public Property JournalVoucherTypeFilingAccountXPO As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeFilingAccountXPO
        Get
            Return CType(INDSleAccountFiling.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountFiling.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece los tipos de comprobante
    ''' </summary>
    Public Property JournalVoucherTypeProvisionXPO As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeProvisionXPO
        Get
            Return CType(INDSleProvision.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleProvision.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece los tipos de comprobante
    ''' </summary>
    Public Property JournalVoucherTypeTranslationXPO As XPInstantFeedbackSource Implements ISettingPortfolio.JournalVoucherTypeTranslationXPO
        Get
            Return CType(INDSleTransfer.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleTransfer.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Indica si aplica o no el deterioro por clasificación
    ''' </summary>
    ''' <returns></returns>
    Public Property DeteriorationByClasification As Boolean Implements ISettingPortfolio.DeteriorationByClasification
        Get
            Return INDSleDeteriorationByClasification.EditValue
        End Get
        Set(value As Boolean)
            INDSleDeteriorationByClasification.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id de la cuenta contable para los movimientos débitos de deterioro 
    ''' </summary>
    ''' <returns></returns>
    Public Property DebitDeteriorationAccount As Integer Implements ISettingPortfolio.DebitDeteriorationAccount
        Get
            Return INDSleAccountDebitDeterioration.EditValue
        End Get
        Set(value As Integer)
            INDSleAccountDebitDeterioration.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id de la cuenta contable para los movimiento créditos de deterioro
    ''' </summary>
    ''' <returns></returns>
    Public Property CreditDeteriorationAccount As Integer Implements ISettingPortfolio.CreditDeteriorationAccount
        Get
            Return INDSleAccountCreditDeterioration.EditValue
        End Get
        Set(value As Integer)
            INDSleAccountCreditDeterioration.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id de la cuenta contable para la reversión del deterioro de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Property ReversalDeteriorationAccount As Integer Implements ISettingPortfolio.ReversalDeteriorationAccount
        Get
            Return INDSleAccountReversionDeterioration.EditValue
        End Get
        Set(value As Integer)
            INDSleAccountReversionDeterioration.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id de la cuenta contable para el periodo previo a la reversión del deterioro de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Property PreviousPeriodReversalAccount As Integer Implements ISettingPortfolio.PreviousPeriodReversalAccount
        Get
            Return INDSleAccountPreviousPeriodDeterioration.EditValue
        End Get
        Set(value As Integer)
            INDSleAccountPreviousPeriodDeterioration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Clasificación de Cartera Salud: 1. Cliente, 2. Entidad Responsable de Pago
    ''' </summary>
    ''' <returns></returns>
    Public Property HealthPortfolioClassification As Byte?
        Get
            If String.IsNullOrEmpty(INDSleHealthPortfolioClasification.EditValue) Then
                Return Nothing
            Else
                Return INDSleHealthPortfolioClasification.EditValue
            End If
        End Get
        Set(value As Byte?)
            INDSleHealthPortfolioClasification.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Indica la fecha a tener en cuenta para el Libro Niif: 1. Fecha de Radicación de la Factura, 2. Fecha de Emisión de la Factura
    ''' </summary>
    ''' <returns></returns>
    Public Property DateNiifBook As Byte?
        Get
            If String.IsNullOrEmpty(INDSleNiifBook.EditValue) Then
                Return Nothing
            Else
                Return INDSleNiifBook.EditValue
            End If
        End Get
        Set(value As Byte?)
            INDSleNiifBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica la fecha a tener en cuenta para el Libro Fiscal: 1. Fecha de Radicación de la Factura, 2. Fecha de Emisión de la Factura
    ''' </summary>
    ''' <returns></returns>
    Public Property DateFiscalBook As Byte?
        Get
            If String.IsNullOrEmpty(INDSleFiscalBook.EditValue) Then
                Return Nothing
            Else
                Return INDSleFiscalBook.EditValue
            End If
        End Get
        Set(value As Byte?)
            INDSleFiscalBook.EditValue = value
        End Set
    End Property

#Region "Budget Interface"

    Public Property BudgetaryEntityId As Integer? Implements ISettingPortfolio.BudgetaryEntityId
        Get
            Return INDSleBudgetaryEntityId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As XPInstantFeedbackSource Implements ISettingPortfolio.BudgetaryEntityXpo
        Get
            Return INDSleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer? Implements ISettingPortfolio.BudgetaryValidityId
        Get
            Return INDSleBudgetaryValidityId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPInstantFeedbackSource Implements ISettingPortfolio.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property DependencyId As Integer? Implements ISettingPortfolio.DependencyId
        Get
            Return INDSleDependencyId.EditValue
        End Get
        Set(value As Integer?)
            INDSleDependencyId.EditValue = value
        End Set
    End Property

    Public Property DependencyXpo As XPInstantFeedbackSource Implements ISettingPortfolio.DependencyXpo
        Get
            Return INDSleDependencyId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDependencyId.Properties.DataSource = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ISettingPortfolio.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Carga el datasource del search que maneja tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDataSources()
        INDSleLegalCollection.Properties.DataSource = ListLegalCollection
        INDSleHealthPortfolioClasification.Properties.DataSource = HealthPortfolioClassificationOptions
        INDSleNiifBook.Properties.DataSource = DateBookOptions
        INDSleFiscalBook.Properties.DataSource = DateBookOptions
        INDSleAccountCreditDeterioration.Properties.DataSource = AccountCreditDeteriorationXpo
        INDSleAccountDebitDeterioration.Properties.DataSource = AccountDebitDeteriorationXpo
        INDSleAccountReversionDeterioration.Properties.DataSource = AccountReversionDeteriorationXpo
        INDSleAccountPreviousPeriodDeterioration.Properties.DataSource = AccountPreviousPeriodDeteriorationXpo
        INDSleDeteriorationByClasification.Properties.DataSource = ListYesOrNo
    End Sub

    Private _ListLegalCollection As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListLegalCollection As List(Of Tuple(Of Byte, String))
        Get
            If _ListLegalCollection Is Nothing Then
                _ListLegalCollection = New List(Of Tuple(Of Byte, String))
                _ListLegalCollection.Add(New Tuple(Of Byte, String)(0, "No"))
                _ListLegalCollection.Add(New Tuple(Of Byte, String)(1, "Si"))
            End If
            Return _ListLegalCollection
        End Get
    End Property

    Private _listYesOrNo As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Tupla para los campos con opciones Sí y No
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListYesOrNo As List(Of Tuple(Of Byte, String))
        Get
            If _listYesOrNo Is Nothing Then
                _listYesOrNo = New List(Of Tuple(Of Byte, String))
                _listYesOrNo.Add(New Tuple(Of Byte, String)(0, "No"))
                _listYesOrNo.Add(New Tuple(Of Byte, String)(1, "Si"))
            End If
            Return _listYesOrNo
        End Get
    End Property

    Private _healthPortfolioClassificationOptions As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Tupla para las opciones de la Clasificación Cartera Salud
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property HealthPortfolioClassificationOptions As List(Of Tuple(Of Byte, String))
        Get
            If _healthPortfolioClassificationOptions Is Nothing Then
                _healthPortfolioClassificationOptions = New List(Of Tuple(Of Byte, String))
                _healthPortfolioClassificationOptions.Add(New Tuple(Of Byte, String)(1, "Entidad Responsable de Pago"))
                _healthPortfolioClassificationOptions.Add(New Tuple(Of Byte, String)(2, "Cliente"))
            End If
            Return _healthPortfolioClassificationOptions
        End Get
    End Property

    Private _dateBookOptions As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Opciones de las fechas a tomar para el deterioro de cartera según cada libro
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property DateBookOptions As List(Of Tuple(Of Byte, String))
        Get
            If _dateBookOptions Is Nothing Then
                _dateBookOptions = New List(Of Tuple(Of Byte, String))
                _dateBookOptions.Add(New Tuple(Of Byte, String)(1, "Fecha de Radicación de la Factura"))
                _dateBookOptions.Add(New Tuple(Of Byte, String)(2, "Fecha de Emisión de la Factura"))
            End If
            Return _dateBookOptions
        End Get
    End Property

    Private _accountCreditDeteriorationXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad para el DataSource de la cuenta de crédito de deterioro
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property AccountCreditDeteriorationXpo As XPInstantFeedbackSource
        Get
            If _accountCreditDeteriorationXpo Is Nothing Then
                Using model As New MSettingPortfolio(CStr(MyTag))
                    _accountCreditDeteriorationXpo = model.ListAccount()
                End Using
            End If
            Return _accountCreditDeteriorationXpo
        End Get
    End Property

    Private _accountDebitDeteriorationXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad para el DataSource de la cuenta de débito de deterioro
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property AccountDebitDeteriorationXpo As XPInstantFeedbackSource
        Get
            If _accountDebitDeteriorationXpo Is Nothing Then
                Using model As New MSettingPortfolio(CStr(MyTag))
                    _accountDebitDeteriorationXpo = model.ListAccount()
                End Using
            End If
            Return _accountDebitDeteriorationXpo
        End Get
    End Property

    Private _accountReversionDeteriorationXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad para el DataSource de la cuenta de reversión de deterioro
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property AccountReversionDeteriorationXpo As XPInstantFeedbackSource
        Get
            If _accountReversionDeteriorationXpo Is Nothing Then
                Using model As New MSettingPortfolio(CStr(MyTag))
                    _accountReversionDeteriorationXpo = model.ListAccount()
                End Using
            End If
            Return _accountReversionDeteriorationXpo
        End Get
    End Property

    Private _accountPreviousPeriodDeteriorationXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad para el DataSource de la cuenta de período anterior de deterioro
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property AccountPreviousPeriodDeteriorationXpo As XPInstantFeedbackSource
        Get
            If _accountPreviousPeriodDeteriorationXpo Is Nothing Then
                Using model As New MSettingPortfolio(CStr(MyTag))
                    _accountPreviousPeriodDeteriorationXpo = model.ListAccount()
                End Using
            End If
            Return _accountPreviousPeriodDeteriorationXpo
        End Get
    End Property


    ''' <summary>
    ''' obtiene o establece si o no se permite factura glosada sin conciliar en traslado a cobro juridico
    ''' </summary>
    ''' <returns></returns>
    Public Property UnReconciledInvoice As Byte? Implements ISettingPortfolio.UnReconciledInvoice
        Get
            Return INDGleUnReconciledInvoice.EditValue
        End Get
        Set(value As Byte?)
            INDGleUnReconciledInvoice.EditValue = value
        End Set
    End Property
#End Region

#Region "CRUD"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If INDSleLegalCollection.EditValue IsNot Nothing Then
            Dim errors = ValidateControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssingValues()
            Try
                Using model As New MSettingPortfolio(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveSettingPortfolio(Me.settingPortfolio)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = Result.Message
                        Me.settingPortfolio = Result.ObjectEmbbeded
                        CleanControls()
                        LoadControls()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            MessageIndigo.Show("Campo Reclasificar cartera en Traslado a Cobro Jurídico no se puede dejar Vacio", MessageType.Information, Botones.Ok)
            INDSleLegalCollection.Focus()
            Exit Sub
        End If

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub
#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        record = Nothing
        _parameterBilling = Nothing
        presenter = Nothing
        settingPortfolio = Nothing
        agesPortfolio = Nothing
        listPortfolioAge = Nothing
        listPortfolioAgeDelete = Nothing
        editPopup = Nothing
        UnReconciledInvoice = Nothing
    End Sub

    Private Async Sub FrmSettingPortfolio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me.indigo = SessionValues.Instance
        presenter = New PSettingPortfolio(Me)

        AsyncLoader(True)
        Using model As New MSettingPortfolio(MyTag)
            INDSleHardCollection.Properties.DataSource = model.ListDocumentTypes()
        End Using
        Await Me.LoadParameterBilling()
        AsyncLoader(False)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvPortfolioAge, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvPortfolioAge.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDGcPortfolioAge)

        LoadControls()
        CleanControlsPopup()
        InitializeDataSources()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleCreditNote_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCreditNote.ButtonClick, INDSleDebitNote.ButtonClick, INDSleTransfer.ButtonClick, INDSleProvision.ButtonClick, INDSleAccountFiling.ButtonClick, INDsleJournalVoucherTypeDocumentAccountReceivableId.ButtonClick, INDSleHardCollection.ButtonClick, INDsleJournalVoucherTypeDeteriorationAccountId.ButtonClick, INDSleAccountDebitDeterioration.ButtonClick, INDSleAccountCreditDeterioration.ButtonClick, INDSleAccountReversionDeterioration.ButtonClick, INDSleAccountPreviousPeriodDeterioration.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            ' Determinar qué control disparó el evento
            Dim senderControl As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)

            ' Si es uno de los controles de cuentas contables de deterioro, abrir el formulario de cuentas
            If senderControl Is INDSleAccountDebitDeterioration OrElse
               senderControl Is INDSleAccountCreditDeterioration OrElse
               senderControl Is INDSleAccountReversionDeterioration OrElse
               senderControl Is INDSleAccountPreviousPeriodDeterioration Then
                If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
                    OpenForm(602, Nothing, True)
                End If
            Else
                ' Para los demás controles, abrir el formulario de tipos de comprobantes
                Using Formulario As New FrmDocumentType()
                    Formulario.ViewModeEditHold = True
                    Formulario.MinimizeBox = False
                    Formulario.MaximizeBox = False
                    Formulario.Size = New Size(780, 700)
                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(Formulario, False)
                    transparent.ShowDialog()
                    presenter.InitializeJournalVoucherTypeCreditNotesXPO()
                    presenter.InitializeJournalVoucherTypeDebitNotesXPO()
                    presenter.InitializeJournalVoucherTypeFilingAccountXPO()
                    presenter.InitializeJournalVoucherTypeProvisionXPO()
                    presenter.InitializeJournalVoucherTypeTranslationXPO()
                    presenter.InitializeJournalVoucherTypeDocumentAccountReceivableXPO()
                    presenter.InitializeJournalVoucherTypeDeteriorationAccountXpo()
                End Using
            End If
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleCreditNote_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCreditNote.QueryPopUp
        If JournalVoucherTypeCreditNotesXPO Is Nothing Then
            presenter.InitializeJournalVoucherTypeCreditNotesXPO()
        End If
    End Sub

    Private Sub INDSleDebitNote_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDebitNote.QueryPopUp
        If JournalVoucherTypeDebitNotesXPO Is Nothing Then
            presenter.InitializeJournalVoucherTypeDebitNotesXPO()
        End If
    End Sub

    Private Sub INDSleTransfer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTransfer.QueryPopUp
        If JournalVoucherTypeTranslationXPO Is Nothing Then
            presenter.InitializeJournalVoucherTypeTranslationXPO()
        End If
    End Sub

    Private Sub INDSleProvision_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleProvision.QueryPopUp
        If JournalVoucherTypeProvisionXPO Is Nothing Then
            presenter.InitializeJournalVoucherTypeProvisionXPO()
        End If
    End Sub

    Private Sub INDSleAccountFiling_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountFiling.QueryPopUp
        If JournalVoucherTypeFilingAccountXPO Is Nothing Then
            presenter.InitializeJournalVoucherTypeFilingAccountXPO()
        End If
    End Sub

    Private Sub INDPcePortfolioAge_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDPcePortfolioAge.QueryPopUp
        If editPopup = False Then
            If listPortfolioAge IsNot Nothing AndAlso listPortfolioAge.Count > 1 Then
                Dim age = listPortfolioAge.ElementAt(listPortfolioAge.Count - 1)
                If age.EndRange = 999999 Then 'se deja quemado este valor de edad maxima
                    e.Cancel = True
                    Mensaje(EeventViewerImages.Advertencia) = "El valor maximo para las edades ya esta en uso, no se pueden agregar mas edades"
                End If
            End If
        End If
    End Sub

    Private Sub INDsleJournalVoucherTypeDocumentAccountReceivableId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeDocumentAccountReceivableId.QueryPopUp
        If JournalVoucherTypeDocumentAccountReceivableXpo Is Nothing Then
            presenter.InitializeJournalVoucherTypeDocumentAccountReceivableXPO()
        End If
    End Sub

    Private Sub INDsleJournalVoucherTypeDeteriorationAccountId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeDeteriorationAccountId.QueryPopUp
        If JournalVoucherTypeDeteriorationAccountIdXpo Is Nothing Then
            presenter.InitializeJournalVoucherTypeDeteriorationAccountXpo()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryValidityId.QueryPopUp
        If BudgetaryValidityXpo Is Nothing Then
            presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
        End If
    End Sub

    Private Sub INDsleBillingBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDependencyId.QueryPopUp
        If DependencyXpo Is Nothing Then
            presenter.InitializeDependency(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleUnReconciledInvoice_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleUnReconciledInvoice.QueryPopUp
        If INDGleUnReconciledInvoice.Properties.DataSource Is Nothing Then
            INDGleUnReconciledInvoice.Properties.DataSource = Me.ListLegalCollection
        End If
    End Sub
    ''' <summary>
    ''' Evento para asignar el DataSource de Clasificación Cartera Salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleHealthPortfolioClasification_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleHealthPortfolioClasification.QueryPopUp
        If INDSleHealthPortfolioClasification.Properties.DataSource Is Nothing Then
            INDSleHealthPortfolioClasification.Properties.DataSource = HealthPortfolioClassificationOptions
        End If
    End Sub
    ''' <summary>
    ''' Evento para asignar las opciones de Fecha a tomar para el deterioro de cartera en el Libro Niif
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleNiifBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNiifBook.QueryPopUp
        If INDSleNiifBook.Properties.DataSource Is Nothing Then
            INDSleNiifBook.Properties.DataSource = DateBookOptions
        End If
    End Sub
    ''' <summary>
    ''' Evento para asignar las opciones de Fecha a tomar para el deterioro de cartera en el Libro Fiscal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFiscalBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleFiscalBook.QueryPopUp
        If INDSleFiscalBook.Properties.DataSource Is Nothing Then
            INDSleFiscalBook.Properties.DataSource = DateBookOptions
        End If
    End Sub

    ''' <summary>
    ''' Evento para inicializar el DataSource de la cuenta contable débito de deterioro
    ''' </summary>
    Private Sub INDSleAccountDebitDeterioration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccountDebitDeterioration.QueryPopUp
        If INDSleAccountDebitDeterioration.Properties.DataSource Is Nothing Then
            INDSleAccountDebitDeterioration.Properties.DataSource = AccountDebitDeteriorationXpo
        End If
    End Sub

    ''' <summary>
    ''' Evento para inicializar el DataSource de la cuenta contable crédito de deterioro
    ''' </summary>
    Private Sub INDSleAccountCreditDeterioration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccountCreditDeterioration.QueryPopUp
        If INDSleAccountCreditDeterioration.Properties.DataSource Is Nothing Then
            INDSleAccountCreditDeterioration.Properties.DataSource = AccountCreditDeteriorationXpo
        End If
    End Sub

    ''' <summary>
    ''' Evento para inicializar el DataSource de la cuenta de reversión de deterioro
    ''' </summary>
    Private Sub INDSleAccountReversionDeterioration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccountReversionDeterioration.QueryPopUp
        If INDSleAccountReversionDeterioration.Properties.DataSource Is Nothing Then
            INDSleAccountReversionDeterioration.Properties.DataSource = AccountReversionDeteriorationXpo
        End If
    End Sub

    ''' <summary>
    ''' Evento para inicializar el DataSource de la cuenta de periodo previo de deterioro
    ''' </summary>
    Private Sub INDSleAccountPreviousPeriodDeterioration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccountPreviousPeriodDeterioration.QueryPopUp
        If INDSleAccountPreviousPeriodDeterioration.Properties.DataSource Is Nothing Then
            INDSleAccountPreviousPeriodDeterioration.Properties.DataSource = AccountPreviousPeriodDeteriorationXpo
        End If
    End Sub

    ''' <summary>
    ''' Evento para inicializar el DataSource de la cuenta de periodo previo de deterioro
    ''' </summary>
    Private Sub INDSleDeteriorationByClasification_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDeteriorationByClasification.QueryPopUp
        If INDSleDeteriorationByClasification.Properties.DataSource Is Nothing Then
            INDSleDeteriorationByClasification.Properties.DataSource = ListYesOrNo
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)
    End Sub

#End Region

#Region "KeyDown"
    Private Sub INDSleAccountFiling_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleAccountFiling.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPcePortfolioAge.Focus()
            INDPcePortfolioAge.ShowPopup()
            INDTxtName.Focus()
        End If
    End Sub
#End Region

#Region "CloseUp"
    Private Sub INDPcePortfolioAge_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPcePortfolioAge.CloseUp
        If editPopup = True Then
            CleanControlsPopup()
        End If
        If listPortfolioAge IsNot Nothing AndAlso listPortfolioAge.Count > 0 Then
            'INDGvPortfolioAge.OptionsFind.AlwaysVisible = True
            'IndigoGridControl1.SetExportButton(INDGcPortfolioAge, True)
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPcePortfolioAge_Popup(sender As Object, e As EventArgs) Handles INDPcePortfolioAge.Popup
        'INDGvPortfolioAge.OptionsFind.AlwaysVisible = False
        'IndigoGridControl1.SetExportButton(INDGcPortfolioAge, False)
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors = ValidateAges()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If editPopup = False Then
            agesPortfolio = New AgesPortfolio
        End If
        With agesPortfolio
            .Name = INDTxtName.Text
            .InitialRange = INDSeInitialRange.EditValue
            .EndRange = INDSeEndRange.EditValue
            .Color = INDCpeColor.Color.ToArgb()
            .DeteriorationPercentage = DeteriorationPercentage
            .ProvisionPercentage = ProvisionPercentage
        End With
        If listPortfolioAge Is Nothing Then
            listPortfolioAge = New List(Of AgesPortfolio)
        End If
        If editPopup = False Then
            listPortfolioAge.Add(agesPortfolio)
        Else
            Dim index = listPortfolioAge.IndexOf(agesPortfolio)
            For i = index + 1 To listPortfolioAge.Count - 1 Step 1
                Dim diference = listPortfolioAge.ElementAt(i).EndRange - listPortfolioAge.ElementAt(i).InitialRange
                If i = index + 1 Then
                    listPortfolioAge.ElementAt(i).InitialRange = agesPortfolio.EndRange + 1
                Else
                    listPortfolioAge.ElementAt(i).InitialRange = listPortfolioAge.ElementAt(i - 1).EndRange + 1
                End If
                listPortfolioAge.ElementAt(i).EndRange = listPortfolioAge.ElementAt(i).InitialRange + diference
            Next
        End If
        INDGcPortfolioAge.DataSource = Nothing
        INDGcPortfolioAge.DataSource = listPortfolioAge
        INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, agesPortfolio.EndRange.ToString())
        If agesPortfolio.EndRange = 999999 Then
            INDPcePortfolioAge.ClosePopup()
        Else
            CleanControlsPopup()
            INDTxtName.Focus()
        End If

    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        agesPortfolio = DirectCast(INDGvPortfolioAge.GetFocusedRow(), AgesPortfolio)
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
        agesPortfolio = DirectCast(INDGvPortfolioAge.GetFocusedRow(), AgesPortfolio)
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
        editPopup = True
        With agesPortfolio
            INDTxtName.Text = .Name
            INDSeInitialRange.EditValue = .InitialRange
            INDSeEndRange.EditValue = .EndRange
            If .EndRange = 999999 Then
                INDSeEndRange.Properties.MaxValue = 999999
            Else
                INDSeEndRange.Properties.MinValue = .InitialRange + 1
            End If
            INDCpeColor.EditValue = .Color
            DeteriorationPercentage = .DeteriorationPercentage
            ProvisionPercentage = .ProvisionPercentage
        End With
        INDBtnAdd.Text = ResourceManager.GetString("Edit")
        INDPcePortfolioAge.Focus()
        INDPcePortfolioAge.ShowPopup()
        INDTxtName.Focus()
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If agesPortfolio.Id > 0 Then
                If listPortfolioAgeDelete Is Nothing Then
                    listPortfolioAgeDelete = New List(Of AgesPortfolio)
                End If
                listPortfolioAgeDelete.Add(agesPortfolio)
            End If
            Dim index = listPortfolioAge.IndexOf(agesPortfolio)
            listPortfolioAge.Remove(agesPortfolio)
            If listPortfolioAge.Count > 1 Then
                For i = index To listPortfolioAge.Count - 1 Step 1
                    If i = index Then
                        listPortfolioAge.ElementAt(i).InitialRange = agesPortfolio.InitialRange
                    Else
                        Dim diference = listPortfolioAge.ElementAt(i).EndRange - listPortfolioAge.ElementAt(i).InitialRange
                        listPortfolioAge.ElementAt(i).InitialRange = listPortfolioAge.ElementAt(i - 1).EndRange + 1
                        listPortfolioAge.ElementAt(i).EndRange = listPortfolioAge.ElementAt(i).InitialRange + diference
                    End If
                Next
            ElseIf listPortfolioAge.Count = 1 Then
                listPortfolioAge.ElementAt(0).InitialRange = 1
            End If
            INDGcPortfolioAge.DataSource = Nothing
            INDGcPortfolioAge.DataSource = listPortfolioAge
            CleanControlsPopup()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmSettingPortfolio_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#End Region

#Region "METHODS"

    Private Async Function LoadParameterBilling() As Task
        Using model As New Presentation.Portfolio.MVP.MBilling(MyTag)
            Dim budgetInterface As Boolean = False
            Me._parameterBilling = Await model.GetSettingsBillingByIdUnitOperative(_idOperativeUnit, False)
            If Me._parameterBilling IsNot Nothing Then
                budgetInterface = Me._parameterBilling.BudgetInterface
            End If

            INDLciBudgetaryEntityId.AllowHide = Not budgetInterface
            INDLciBudgetaryEntityId.ShowInCustomizationForm = Not budgetInterface
            INDLciBudgetaryValidityId.AllowHide = Not budgetInterface
            INDLciBudgetaryValidityId.ShowInCustomizationForm = Not budgetInterface
            INDLciDependencyId.AllowHide = Not budgetInterface
            INDLciDependencyId.ShowInCustomizationForm = Not budgetInterface
            INDlygBudgetInterface.Visibility = If(budgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag)
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    Private Sub CleanControls()
        DeleteBlockedRecord()
        INDSleCreditNote.EditValue = Nothing
        INDSleDebitNote.EditValue = Nothing
        INDSleTransfer.EditValue = Nothing
        INDSleProvision.EditValue = Nothing
        INDsleJournalVoucherTypeDeteriorationAccountId.EditValue = Nothing
        INDSleAccountFiling.EditValue = Nothing
        INDGcPortfolioAge.DataSource = Nothing
        INDSleHardCollection.EditValue = Nothing
        JournalVoucherTypeDocumentAccountReceivableId = Nothing
        INDSleCreditNote.Properties.NullText = String.Empty
        INDSleDebitNote.Properties.NullText = String.Empty
        INDSleTransfer.Properties.NullText = String.Empty
        INDSleAccountFiling.Properties.NullText = String.Empty
        INDSleProvision.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeDocumentAccountReceivableId.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeDeteriorationAccountId.Properties.NullText = String.Empty
        listPortfolioAge = Nothing
        listPortfolioAgeDelete = Nothing
        INDSleLegalCollection.EditValue = Nothing
        CleanControlsPopup()

        BudgetaryEntityId = Nothing
        INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        BudgetaryValidityId = Nothing
        INDSleBudgetaryValidityId.Properties.NullText = String.Empty
        DependencyId = Nothing
        INDSleDependencyId.Properties.NullText = String.Empty
        INDGleUnReconciledInvoice.EditValue = Nothing
        INDGleUnReconciledInvoice.Properties.NullText = String.Empty
        INDSleDeteriorationByClasification.EditValue = Nothing
        INDSleAccountDebitDeterioration.EditValue = Nothing
        INDSleAccountDebitDeterioration.Properties.NullText = String.Empty
        INDSleAccountCreditDeterioration.EditValue = Nothing
        INDSleAccountCreditDeterioration.Properties.NullText = String.Empty
        INDSleAccountReversionDeterioration.EditValue = Nothing
        INDSleAccountReversionDeterioration.Properties.NullText = String.Empty
        INDSleAccountPreviousPeriodDeterioration.EditValue = Nothing
        INDSleAccountPreviousPeriodDeterioration.Properties.NullText = String.Empty
        INDSleHealthPortfolioClasification.EditValue = Nothing
        INDSleHealthPortfolioClasification.Properties.NullText = String.Empty
        INDSleNiifBook.EditValue = Nothing
        INDSleNiifBook.Properties.NullText = String.Empty
        INDSleFiscalBook.EditValue = Nothing
        INDSleFiscalBook.Properties.NullText = String.Empty

        BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        Using model As New MSettingPortfolio(MyTag)
            settingPortfolio = Await model.GetSettingPortfolioByIdOperatingUnitAsync(_idOperativeUnit)
        End Using
        If settingPortfolio IsNot Nothing AndAlso settingPortfolio.Id > 0 Then
            With settingPortfolio
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                JournalVoucherTypeCreditNotesId = .JournalVoucherTypeCreditNotesId
                JournalVoucherTypeDebitNotesId = .JournalVoucherTypeDebitNotesId
                JournalVoucherTypeFilingAccountId = .JournalVoucherTypeFilingAccountId
                JournalVoucherTypeProvisionId = .JournalVoucherTypeProvisionId
                JournalVoucherTypeTranslationId = .JournalVoucherTypeTranslationId
                JournalVoucherTypeDocumentAccountReceivableId = .JournalVoucherTypeDocumentAccountReceivableId
                JournalVoucherTypeDeteriorationAccountId = .JournalVoucherTypeDeteriorationAccountId
                INDSleHardCollection.EditValue = .JournalVoucherTypeHardCollectionId
                INDSleCreditNote.Properties.NullText = .CodeNameJournalVoucerTypeCreditNote
                INDSleDebitNote.Properties.NullText = .CodeNameJournalVoucerTypeDebitNote
                INDSleTransfer.Properties.NullText = .CodeNameJournalVoucerTypeTransfer
                INDSleAccountFiling.Properties.NullText = .CodeNameJournalVoucerTypeFilingAccount
                INDSleProvision.Properties.NullText = .CodeNameJournalVoucerTypeProvision
                INDsleJournalVoucherTypeDeteriorationAccountId.Properties.NullText = .CodeNameJournalVoucherTypeDeteriorationAccount
                INDsleJournalVoucherTypeDocumentAccountReceivableId.Properties.NullText = .CodeNameJournalVoucherTypeDocumentAccountReceivable
                NameMaximumAgeRange = .NameMaximumAgeRange
                NameMinimumAgeRange = .NameMinimumAgeRange
                INDSleLegalCollection.EditValue = IIf(.Legalcol = True, 1, 0)
                Dim _AllowUnReconciledInvoice = .UnReconciledInvoice.GetValueOrDefault
                UnReconciledInvoice = CByte(IIf(_AllowUnReconciledInvoice, 1, 0))
                Select Case UnReconciledInvoice
                    Case 1
                        INDGleUnReconciledInvoice.Properties.NullText = "Si"
                    Case 0
                        INDGleUnReconciledInvoice.Properties.NullText = "No"
                End Select
                INDSleTransfers.EditValue = .Transfers
                INDSleNotesDebitCreditPortfolio.EditValue = .NotesDebitCreditPortfolio
                DeteriorationByClasification = .ApplyDeteriorationByClassification

                ' DeteriorationBasicBillingPortfolio
                _deteriorationBasicBillingPortfolio = .DeteriorationBasicBillingPortfolio.FirstOrDefault()
                DebitDeteriorationAccount = If(_deteriorationBasicBillingPortfolio?.DebitDeteriorationAccount, 0)
                CreditDeteriorationAccount = If(_deteriorationBasicBillingPortfolio?.CreditDeteriorationAccount, 0)
                ReversalDeteriorationAccount = If(_deteriorationBasicBillingPortfolio?.ReversalDeteriorationAccount, 0)
                PreviousPeriodReversalAccount = If(_deteriorationBasicBillingPortfolio?.PreviousPeriodReversalAccount, 0)
                ' RulesDeteriorationClassification
                If DeteriorationByClasification Then
                    _rulesDeteriorationClassification = .RulesDeteriorationClassification.FirstOrDefault()
                    HealthPortfolioClassification = _rulesDeteriorationClassification?.HealthPortfolioClassification
                    DateNiifBook = _rulesDeteriorationClassification?.NiifBook
                    DateFiscalBook = _rulesDeteriorationClassification?.FiscalBook
                End If

                If _parameterBilling IsNot Nothing AndAlso _parameterBilling.BudgetInterface Then
                    BudgetaryEntityId = .BudgetaryEntityId
                    INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                    BudgetaryValidityId = .BudgetaryValidityId
                    INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                    DependencyId = .DependencyId
                    INDSleDependencyId.Properties.NullText = .DependencyDescription
                End If
            End With
            Using model As New MAgesPortfolio(MyTag)
                listPortfolioAge = model.ListAgesPortfolioByIdSettingPortfolio(settingPortfolio.Id)
            End Using
            INDGcPortfolioAge.DataSource = Nothing
            INDGcPortfolioAge.DataSource = listPortfolioAge
            SetRanges()
            BarraBotones.SetDocuments(Me.settingPortfolio.Id)
            Using model As New MBlockRecordAndSequense(MyTag)
                Dim result = Await model.GetBlockRecord(Me.Tag, Me.settingPortfolio.Id)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Me.settingPortfolio.Id}
                    Dim operation = Await model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            End Using
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmSettingPortfolio_DontExists", NAME_MODULE)
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        End If
        INDSleCreditNote.Focus()
    End Sub

    Private Sub AssingValues()
        With settingPortfolio
            .OperatingUnitId = _idOperativeUnit
            .JournalVoucherTypeCreditNotesId = JournalVoucherTypeCreditNotesId
            .JournalVoucherTypeDebitNotesId = JournalVoucherTypeDebitNotesId
            .JournalVoucherTypeFilingAccountId = JournalVoucherTypeFilingAccountId
            .JournalVoucherTypeProvisionId = JournalVoucherTypeProvisionId
            .JournalVoucherTypeTranslationId = JournalVoucherTypeTranslationId
            .JournalVoucherTypeDocumentAccountReceivableId = JournalVoucherTypeDocumentAccountReceivableId
            .JournalVoucherTypeHardCollectionId = INDSleHardCollection.EditValue
            .JournalVoucherTypeDeteriorationAccountId = JournalVoucherTypeDeteriorationAccountId
            .NameMaximumAgeRange = NameMaximumAgeRange
            .NameMinimumAgeRange = NameMinimumAgeRange
            .MaximunAgeRange = listPortfolioAge.ElementAt(listPortfolioAge.Count - 1).EndRange
            .LegalCollection = IIf(INDSleLegalCollection.EditValue = 1, True, False)
            .State = True
            .UnReconciledInvoice = CBool(UnReconciledInvoice.GetValueOrDefault)
            .Transfers = INDSleTransfers.EditValue
            .NotesDebitCreditPortfolio = INDSleNotesDebitCreditPortfolio.EditValue
            .ApplyDeteriorationByClassification = DeteriorationByClasification

            ' Manejo de DeteriorationBasicBillingPortfolio
            ' Si no existe en la colección, lo creamos y agregamos
            If Not .DeteriorationBasicBillingPortfolio.Any() Then
                _deteriorationBasicBillingPortfolio = New DeteriorationBasicBillingPortfolio()
                .DeteriorationBasicBillingPortfolio.Add(_deteriorationBasicBillingPortfolio)
            Else
                ' Si ya existe, usamos el existente
                _deteriorationBasicBillingPortfolio = .DeteriorationBasicBillingPortfolio.FirstOrDefault()
            End If

            ' Actualizamos las propiedades (ya sea nuevo o existente)
            _deteriorationBasicBillingPortfolio.DebitDeteriorationAccount = DebitDeteriorationAccount
            _deteriorationBasicBillingPortfolio.CreditDeteriorationAccount = CreditDeteriorationAccount
            _deteriorationBasicBillingPortfolio.ReversalDeteriorationAccount = ReversalDeteriorationAccount
            _deteriorationBasicBillingPortfolio.PreviousPeriodReversalAccount = PreviousPeriodReversalAccount

            ' Manejo de RulesDeteriorationClassification
            If DeteriorationByClasification Then
                ' Si está habilitado el deterioro por clasificación
                ' Si no existe en la colección, lo creamos y agregamos
                If Not .RulesDeteriorationClassification.Any() Then
                    _rulesDeteriorationClassification = New RulesDeteriorationClassification()
                    .RulesDeteriorationClassification.Add(_rulesDeteriorationClassification)
                Else
                    ' Si ya existe, usamos el existente
                    _rulesDeteriorationClassification = .RulesDeteriorationClassification.FirstOrDefault()
                End If

                ' Actualizamos las propiedades (ya sea nuevo o existente)
                _rulesDeteriorationClassification.HealthPortfolioClassification = HealthPortfolioClassification
                _rulesDeteriorationClassification.NiifBook = DateNiifBook
                _rulesDeteriorationClassification.FiscalBook = DateFiscalBook
            Else
                ' Si está deshabilitado el deterioro por clasificación
                ' Verificamos si existe un objeto en la colección
                If .RulesDeteriorationClassification.Any() Then
                    _rulesDeteriorationClassification = .RulesDeteriorationClassification.FirstOrDefault()
                    If _rulesDeteriorationClassification IsNot Nothing Then
                        _rulesDeteriorationClassification.MarkAsDeleted()
                    End If
                End If
            End If

            For Each item In listPortfolioAge
                .AgesPortfolio.Add(item)
            Next
            If listPortfolioAgeDelete IsNot Nothing AndAlso listPortfolioAgeDelete.Count > 0 Then
                For Each item In listPortfolioAgeDelete
                    .AgesPortfolio.Add(item.MarkAsDeleted())
                Next
            End If
            If _parameterBilling IsNot Nothing AndAlso _parameterBilling.BudgetInterface Then
                .DependencyId = DependencyId
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Function ValidateControls() As String
        Dim errors As New StringBuilder
        If INDSleAccountFiling.EditValue = Nothing Then
            errors.AppendLine(INDLciAccountFiling.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSleCreditNote.EditValue = Nothing Then
            errors.AppendLine(INDLciCreditNote.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSleDebitNote.EditValue = Nothing Then
            errors.AppendLine(INDLciDebitNote.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSleProvision.EditValue = Nothing Then
            errors.AppendLine(INDLciProvision.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDsleJournalVoucherTypeDeteriorationAccountId.EditValue = Nothing Then
            errors.AppendLine(INDlyItemJournalVoucherTypeDeteriorationAccountId.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSleTransfer.EditValue = Nothing Then
            errors.AppendLine(INDLciTransfer.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDsleJournalVoucherTypeDocumentAccountReceivableId.EditValue Is Nothing Then
            errors.AppendFormat(INDlciJournalVoucherTypeDocumentAccountReceivableId.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If listPortfolioAge Is Nothing Then
            errors.AppendLine("Se debe crear minimo una edad de cartera")
        End If
        If listPortfolioAge IsNot Nothing AndAlso listPortfolioAge.Count = 0 Then
            errors.AppendLine("Se debe crear minimo una edad de cartera")
        End If

        If _parameterBilling IsNot Nothing AndAlso _parameterBilling.BudgetInterface Then
            If INDSleBudgetaryEntityId.EditValue = Nothing Then
                errors.AppendLine(INDLciBudgetaryEntityId.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If INDSleBudgetaryValidityId.EditValue = Nothing Then
                errors.AppendLine(INDLciBudgetaryValidityId.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If INDSleDependencyId.EditValue = Nothing Then
                errors.AppendLine(INDLciDependencyId.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If

        If DebitDeteriorationAccount = 0 OrElse DebitDeteriorationAccount = Nothing Then
            errors.AppendLine(INDlciAccountDebitDeterioration.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If CreditDeteriorationAccount = 0 OrElse CreditDeteriorationAccount = Nothing Then
            errors.AppendLine(INDLciAccountCreditDeterioration.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If ReversalDeteriorationAccount = 0 OrElse ReversalDeteriorationAccount = Nothing Then
            errors.AppendLine(INDLciAccountReversion.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If PreviousPeriodReversalAccount = 0 OrElse PreviousPeriodReversalAccount = Nothing Then
            errors.AppendLine(INDLciAccountPreviousPeriod.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If

        ' Validaciones para deterioro por clasificación (solo si está habilitado)
        If DeteriorationByClasification Then
            If HealthPortfolioClassification = 0 OrElse HealthPortfolioClassification Is Nothing Then
                errors.AppendLine(INDlciHealthPortfolioClasification.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If DateNiifBook = 0 OrElse DateNiifBook Is Nothing Then
                errors.AppendLine(INDLciGleNiifBook.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If DateFiscalBook = 0 OrElse DateFiscalBook Is Nothing Then
                errors.AppendLine(INDLciGleFiscalBook.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para validar las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateAges() As String
        Dim errors As New StringBuilder
        If INDTxtName.Text = String.Empty Then
            errors.AppendLine(INDLciName.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSeEndRange.EditValue = 0 Then
            errors.AppendLine(INDLciEndRange.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDCpeColor.Color.ToArgb() = 0 Then
            errors.AppendLine(INDLciColor.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSeInitialRange.EditValue > INDSeEndRange.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("Range", NAME_MODULE), INDLciInitialRange.CustomizationFormText, INDLciEndRange.CustomizationFormText))
        End If
        If listPortfolioAge IsNot Nothing AndAlso listPortfolioAge.Where(Function(a) a.Name = INDTxtName.Text).Count > 0 Then
            errors.AppendLine(String.Format("Ya existe una edad de cartera con la descripción {0}", INDTxtName.Text))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para limpiar controles del popup de edades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDTxtName.Text = String.Empty
        SetRanges()
        INDCpeColor.EditValue = Nothing
        agesPortfolio = Nothing
        INDBtnAdd.Text = ResourceManager.GetString("Add")
        editPopup = False
    End Sub

    ''' <summary>
    ''' metodo para establecer el rango inicial y final de las edades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRanges()
        If listPortfolioAge IsNot Nothing AndAlso listPortfolioAge.Count > 0 Then
            INDSeInitialRange.EditValue = listPortfolioAge.ElementAt(listPortfolioAge.Count - 1).EndRange + 1
            INDSeEndRange.EditValue = INDSeInitialRange.EditValue + 1
            If INDSeEndRange.EditValue >= 999999 Then
                INDSeEndRange.EditValue = 999999
                INDSeEndRange.Properties.MaxValue = 999999
                INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, "999999")
            Else
                INDSeEndRange.Properties.MinValue = INDSeInitialRange.EditValue + 1
                INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, listPortfolioAge.ElementAt(listPortfolioAge.Count - 1).EndRange.ToString())
            End If
        Else
            INDSeInitialRange.EditValue = 1
            INDSeEndRange.EditValue = 2
            INDSeEndRange.Properties.MinValue = 2
            INDLciNameMaximumAgeRange.Text = String.Format(INDLciNameMaximumAgeRange.Tag, "1")
        End If
    End Sub

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDSleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            DependencyId = Nothing
            INDSleDependencyId.Properties.NullText = String.Empty
            DependencyXpo = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento para mostrar u ocultar el segmento de Reglas de Deterioro por Clasificación
    ''' </summary>
    Private Sub ShowDeteriorationByClassification(ByVal applyDeteriorationByClassification As Boolean)
        INDlygDeteriorationRules.HideControl(Not applyDeteriorationByClassification)
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            DeleteBlockedRecord()
            CleanControls()
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameterBilling()
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando el campo Aplica Deterioro por Clasificación cambiar de valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleDeteriorationByClasification_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleDeteriorationByClasification.EditValueChanged
        ShowDeteriorationByClassification(DeteriorationByClasification)
    End Sub

#End Region

End Class