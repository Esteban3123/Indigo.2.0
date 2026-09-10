'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2014
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
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas
Imports Presentation.Portfolio.MVP
Imports eNature = Domain.Entities.Service.eNature

#End Region

Public Class FrmNotesDebitCreditPortfolio
    Implements INotesDebitCreditPortfolio, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' constante con el nombre del modulo de glosas
    ''' </summary>
    Private Const GLOSAS_MODULE = "Glosas"

    ''' <summary>
    ''' variable para validaciones sobre saldos de anticipos
    ''' </summary>
    Private Const creditNature As Integer = 2

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _currentSequenceId As Int64

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Private _presenter As PNotesDebitCreditPortfolio

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordPortfolio

    ''' <summary>
    ''' control de usuario para manejar los debitos y los creditos
    ''' </summary>
    Private _ctrDebitCredit As CtrDebitCredit

    ''' <summary>
    ''' control de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ctrAdvance As CtrAdvanceDistribution

    ''' <summary>
    ''' Formulario de Cruce de Anticipos vs CxC
    ''' </summary>
    Private _frmPorfolioTransfer As FrmPortfolioTransfers

    ''' <summary>
    ''' Entidad del documento
    ''' </summary>
    Private _portfolioNote As PortfolioNote

    ''' <summary>
    ''' entidad  donde se relacionan las facturas y anticipos con la nota
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioNoteAccountReceivableAdvance As PortfolioNoteAccountReceivableAdvance

    ''' <summary>
    ''' listado donde se relacionan las facturas y anticipos con la nota
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioNoteAccountReceivableAdvance As List(Of PortfolioNoteAccountReceivableAdvance)

    ''' <summary>
    ''' listado donde se relacionan las facturas y anticipos con la nota para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioNoteAccountReceivableAdvanceDelete As List(Of PortfolioNoteAccountReceivableAdvance)

    ''' <summary>
    ''' diccionario para almacenar las cuotas de las facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private _dictionaryAccountReceivableShares As Dictionary(Of String, List(Of PortfolioAccountReceivableShareXpo))

    ''' <summary>
    ''' representa la entidad donde se distribuye el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioNoteDistribution As PortfolioNoteDistribution

    ''' <summary>
    ''' detalle de los anticipos que se van a distribuir
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioNoteDistribution As List(Of PortfolioNoteDistribution)

    ''' <summary>
    ''' detalles de los anticipos distribuidos que se van a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioNoteDistributionDelete As List(Of PortfolioNoteDistribution)

    ''' <summary>
    ''' representa el detalla de la nota
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioNoteDetail As PortfolioNoteDetail

    ''' <summary>
    ''' listado de los detalles de la nota
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioNoteDetail As List(Of PortfolioNoteDetail)

    ''' <summary>
    ''' listado de los detalle de la nota que se van a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPortfolioNoteDetailDelete As List(Of PortfolioNoteDetail)

    ''' <summary>
    ''' Concepto actualmente seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private _portfolioNoteConcept As PortfolioNoteConcept

    ''' <summary>
    ''' entidad que representa los conceptos de retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _retentionConcept As RetentionConcepts

    ''' <summary>
    ''' variable para almacenar el id original del tercero asociado al cliente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _originalValueThirdPartyId As Integer?

    ''' <summary>
    ''' variable para almacenar el nombre original del tercero asociado al cliente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _originalValueThirdPartyName As String

    ''' <summary>
    ''' debitos
    ''' </summary>
    Dim _debitValue As Decimal

    ''' <summary>
    ''' creditos
    ''' </summary>
    Dim _creditValue As Decimal

    ''' <summary>
    ''' Avances
    ''' </summary>
    Private _advanceValue As Decimal = 0

    ''' <summary>
    '''
    ''' </summary>
    Dim _stateOpenPopUpPorfolioTransfer As Boolean

    ''' <summary>
    ''' Identifica si se esta editando el concepto
    ''' </summary>
    Dim _editModePopupConcepts As Boolean

    ''' <summary>
    ''' bandera para saber cuando se esta eliminando un item de la distribucion y sumar el valor al saldo del anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _flagDeleteItemDistribution As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

    ''' <summary>
    ''' cuenta contable del anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _mainAccountAdvanceDistribution As Integer

    ''' <summary>
    ''' Permite saber el tipo de documento de la factura, esta variable se utiliza para poder validar las facturas capitadas
    ''' </summary>
    Dim _invoiceDocumentType As Integer = 0

    ''' <summary>
    ''' Permite saber si la factura a agregar es un saldo inicial
    ''' </summary>
    Dim _isOpeningBalance As Boolean = False

    ''' <summary>
    ''' Establece el valor de la cuenta por cobrar
    ''' </summary>
    Dim _valueBill As Decimal

    ''' <summary>
    ''' Establece el saldo de la cuenta por cobrar
    ''' </summary>
    Dim _balanceBill As Decimal

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim settingPortfolio As SettingPortfolio

    Dim _electronicDocumentComment As New StringBuilder

    ''' <summary>
    ''' variable que muestra el estado de si se etsa cargando un registro
    ''' </summary>
    Dim _loadControl As Boolean = False

    ''' <summary>
    ''' variable que muestra el estado del registro 
    ''' 1-Registrado
    ''' 2-Confirmado
    ''' 3-Anulado
    ''' </summary>
    Dim _statusData As Integer = 1

    ''' <summary>
    ''' Porcentaje del IVA en Detalles
    ''' </summary>
    Dim _percentage As Decimal
    ''' <summary>
    ''' Variable que guarda la cuenta principal cuando el concepto de nota no carga la cuenta y esta toca colocarla manualmente
    ''' por medio del control INDSleMainAccount
    ''' </summary>
    Dim _auxMainAccount As MainAccounts

    ''' <summary>
    ''' datos de la cuenta  del iva descontable
    ''' </summary>
    ''' <remarks></remarks>
    Private _taxMainAccount As Infrastructure.Data.Xpo.PortfolioRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' datos de la tarifa de iva
    ''' </summary>
    ''' <remarks></remarks>
    Private _generalLedgerIVA As ActionResult(Of GeneralLedgerIVA)

#End Region

#Region "Fields"

    Public ReadOnly Property MyTag As Object Implements INotesDebitCreditPortfolio.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements INotesDebitCreditPortfolio.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public Property Sequense As PortfolioSequence Implements INotesDebitCreditPortfolio.Sequense
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements INotesDebitCreditPortfolio.ActionsOnControls
        Set(value As Boolean)
            INDlyNotes.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDdteDate.Enabled = value
            INDgleNature.Enabled = value
            INDGleNoteType.Enabled = value
            INDsleClient.Enabled = value
            INDsleAdvanceDistribution.Enabled = value
            INDSlePorfolioTransfer.Enabled = value
            INDmemoComments.Enabled = value
            INDsleCurrency.Enabled = value

            INDpceBill.Enabled = value
            INDEsbBillsOrShare.Enabled = False
            INDBtnImportFileBillsOrShare.Enabled = False
            INDgcBill.Enabled = value
            INDgcDetails.Enabled = value

            INDpceAdvance.Enabled = value
            INDEsbAdvance.Enabled = False
            INDBtnImportFileAdvance.Enabled = False
            INDgcAdvance.Enabled = value

            INDpceConcept.Enabled = value
            INDgcConcepts.Enabled = value

            INDPceAdvanceDistribution.Enabled = value
            INDGcAdvanceDistribution.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            If INDBteCode.Enabled = True Then
                INDBteCode.Focus()
            Else
                INDdteDate.Focus()
            End If

            INDlyNotes.EndUpdate()
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

#End Region

#Region "Properties"

    ''' <summary>
    ''' obtiene o establce el codigo de la nota
    ''' </summary>
    Public Property Code As String Implements INotesDebitCreditPortfolio.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' fecha de la nota
    ''' </summary>
    Public Property NoteDate As Date? Implements INotesDebitCreditPortfolio.NoteDate
        Get
            Return INDdteDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de nota
    ''' </summary>
    Public Property NoteType As Integer Implements INotesDebitCreditPortfolio.NoteType
        Get
            Return INDGleNoteType.EditValue
        End Get
        Set(value As Integer)
            INDGleNoteType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' naturaleza
    ''' </summary>
    Public Property Nature As Integer Implements INotesDebitCreditPortfolio.Nature
        Get
            Return INDgleNature.EditValue
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del cliente
    ''' </summary>
    Public Property CustomerId As Integer? Implements INotesDebitCreditPortfolio.CustomerId
        Get
            Return INDsleClient.EditValue
        End Get
        Set(value As Integer?)
            INDsleClient.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del avance a distribuir
    ''' </summary>
    ''' <returns></returns>
    Public Property AdvanceDistributionId As Integer? Implements INotesDebitCreditPortfolio.AdvanceDistributionId
        Get
            Return INDsleAdvanceDistribution.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdvanceDistribution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del cruce de anticipos
    ''' </summary>
    ''' <returns></returns>
    Public Property PortfolioTransferId As Integer? Implements INotesDebitCreditPortfolio.PortfolioTransferId
        Get
            Return INDSlePorfolioTransfer.EditValue
        End Get
        Set(value As Integer?)
            INDSlePorfolioTransfer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' observaciones
    ''' </summary>
    Public Property Observations As String Implements INotesDebitCreditPortfolio.Observations
        Get
            Return INDmemoComments.Text
        End Get
        Set(value As String)
            INDmemoComments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' id de la moneda selecionada, por defecto es la del sistema
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer Implements INotesDebitCreditPortfolio.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
    End Property

    Dim _currencyAbbreviation As String
    ''' <summary>
    ''' Propiedad que establece el codigo standar de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyAbbreviation As String Implements INotesDebitCreditPortfolio.CurrencyAbbreviation
        Get
            If INDsleCurrency.Properties.DataSource IsNot Nothing Then
                _currencyAbbreviation = TryCast(TryCast(INDGvCurrency.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)?.Abbreviation
            End If
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource que carga las monedas del maesttro moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDataSourceXpo As XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.CurrencyDataSourceXpo
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el valor del ajuste
    ''' </summary>
    Public Property AdjustmentValue As Decimal
        Get
            Return INDtxtAdjustment.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAdjustment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece u obtiene el valor del control
    ''' </summary>
    ''' <returns></returns>
    Property ValueDistribution As Decimal
        Get
            Return INDTxtValueDistribution.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValueDistribution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' concepto para la nota electronica
    ''' </summary>
    ''' <returns></returns>
    Property ElectronicConcept As Integer?
        Get
            Return INDSleConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de ajuste de avance
    ''' </summary>
    ''' <returns></returns>
    Property AdjustmentAdvance As Decimal
        Get
            Return INDtxtAdjustmentAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAdjustmentAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor base
    ''' </summary>
    ''' <returns></returns>
    Property BaseValue As Decimal
        Get
            Return INDTxtBaseValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtBaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor base para conceptos con retención
    ''' </summary>
    ''' <returns></returns>
    Property RetentionBaseValue As Decimal
        Get
            Return INDTxtBaseValueRetention.EditValue
        End Get
        Set(value As Decimal)
            INDTxtBaseValueRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el Id de la tarifa del Iva
    ''' </summary>
    ''' <returns></returns>
    Property IdGeneralLedgerIVA As Integer?
        Get
            Return INDSleIdGeneralLedgerIVA.EditValue
        End Get
        Set(value As Integer?)
            INDSleIdGeneralLedgerIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el valor aplicado del Iva
    ''' </summary>
    ''' <returns></returns>
    Property IvaRate As Decimal?
        Get
            Return INDtxtIvaRate.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtIvaRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la suma del campo Base más Iva
    ''' </summary>
    ''' <returns></returns>
    Property TotalConcept As Decimal?
        Get
            Return INDtxtTotalConcept.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtTotalConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga el data source de la tarifa del IVA
    ''' </summary>
    ''' <returns></returns>
    Property TaxRateDataSourceXpo As XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.TaxRateDataSourceXpo

        Get
            Return TryCast(INDRiSleTaxRate.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRiSleTaxRate.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece Id del detalle de la cxc, cuando se selecciona la factura
    ''' </summary>
    ''' <returns></returns>
    Property AccountReceivableAccountingId As Integer?
        Get
            Return INDSleBillsAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleBillsAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el saldo de la factura ya sea del accounting o del share
    ''' </summary>
    ''' <returns></returns>
    Property PopUpBalance As Decimal
        Get
            Return INDtxtBalance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el valor de la cxc del accounting o del share
    ''' </summary>
    ''' <returns></returns>
    Property PopUpValue As Decimal
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property
#End Region

#Region "Tuples"

    ''' <summary>
    ''' listado de los tipos de notas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listNoteType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' obtiene la lista los tipos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ListNoteType As List(Of Tuple(Of Byte, String))
        Get
            If _listNoteType Is Nothing Then
                _listNoteType = New List(Of Tuple(Of Byte, String))
                _listNoteType.Add(New Tuple(Of Byte, String)(1, "Factura Total"))
                _listNoteType.Add(New Tuple(Of Byte, String)(2, "Factura Cuota"))
                _listNoteType.Add(New Tuple(Of Byte, String)(6, "Factura Detallada"))
                _listNoteType.Add(New Tuple(Of Byte, String)(3, "Anticipo"))
                _listNoteType.Add(New Tuple(Of Byte, String)(4, "Distribuir Anticipo"))
                _listNoteType.Add(New Tuple(Of Byte, String)(5, "Reversión Anticipo vs CxC"))
            End If
            Return _listNoteType
        End Get
    End Property

    ''' <summary>
    ''' listado de los tipos de notas debito
    ''' </summary>
    ''' <remarks></remarks>
    Private _listConceptsNoteDebit As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' obtiene la lista los conceptos para la nota debito usadas en la factura electronica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ListConceptsNoteDebit As List(Of Tuple(Of Integer, String))
        Get
            If _listConceptsNoteDebit Is Nothing Then
                _listConceptsNoteDebit = New List(Of Tuple(Of Integer, String))
                _listConceptsNoteDebit.Add(New Tuple(Of Integer, String)(1, "Intereses"))
                _listConceptsNoteDebit.Add(New Tuple(Of Integer, String)(2, "Gastos por cobrar"))
                _listConceptsNoteDebit.Add(New Tuple(Of Integer, String)(3, "Cambio del valor"))
                _listConceptsNoteDebit.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("ElectronicConcept7", NAME_MODULE)))
            End If
            Return _listConceptsNoteDebit
        End Get
    End Property

    ''' <summary>
    ''' Lista de tarifas iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateIvaXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleIdGeneralLedgerIVA.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleIdGeneralLedgerIVA.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' listado de los tipos de notas crédito
    ''' </summary>
    ''' <remarks></remarks>
    Private _listConceptsNoteCredit As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' obtiene la lista los conceptos para la nota credito usadas en la factura electronica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ListConceptsNoteCredit As List(Of Tuple(Of Integer, String))
        Get
            If _listConceptsNoteCredit Is Nothing Then
                _listConceptsNoteCredit = New List(Of Tuple(Of Integer, String))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ElectronicConcept1", NAME_MODULE)))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("ElectronicConcept2", NAME_MODULE)))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("ElectronicConcept3", NAME_MODULE)))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("ElectronicConcept4", NAME_MODULE)))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("ElectronicConcept5", NAME_MODULE)))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("ElectronicConcept6", NAME_MODULE)))
                _listConceptsNoteCredit.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("ElectronicConcept7", NAME_MODULE)))
            End If
            Return _listConceptsNoteCredit
        End Get
    End Property

#End Region

#Region "XPO"

    Public Property CustomerXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.CustomerXPO
        Get
            Return CType(INDsleClient.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleClient.Properties.DataSource = value
        End Set
    End Property

    Public Property AdvanceDistributionXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.AdvanceDistributionXPO
        Get
            Return CType(INDsleAdvanceDistribution.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAdvanceDistribution.Properties.DataSource = value
        End Set
    End Property

    Public Property PorfolioTransferXPO As XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.PorfolioTransferXPO
        Get
            Return INDSlePorfolioTransfer.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePorfolioTransfer.Properties.DataSource = value
        End Set
    End Property

    Public Property BillsXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.BillsXPO
        Get
            If INDLciBillsAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Return CType(INDSleBillsAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
            Else
                Return CType(INDSleBillsShare.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
            End If

        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            If INDLciBillsAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleBillsAccount.Properties.DataSource = value
            Else
                INDSleBillsShare.Properties.DataSource = value
            End If
        End Set
    End Property

    Public Property AdvanceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.AdvanceXPO
        Get
            Return CType(INDsleAdvance.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAdvance.Properties.DataSource = value
        End Set
    End Property

    Public Property NoteConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.NoteConceptXpo
        Get
            Return CType(INDSleNoteConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleNoteConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountsXPO As XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.AccountsXPO
        Get
            Return CType(INDSleMainAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.ThirdPartyXPO
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    Public Property CostCenterConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.CostCenterConceptXPO
        Get
            Return CType(INDSLeCostCenterConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLeCostCenterConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property RetentionConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.RetentionConceptXPO
        Get
            Return CType(INDSLeRetentionConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSLeRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property CustomerDistributionXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.CustomerDistributionXPO
        Get
            Return CType(INDSleCustomerDistribution.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCustomerDistribution.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountsDistributionXPO As XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.AccountsDistributionXPO
        Get
            Return CType(INDSleMainAccountDistribution.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMainAccountDistribution.Properties.DataSource = value
        End Set
    End Property

    Public Property CostCenterConceptDistributionXPO As XPInstantFeedbackSource Implements INotesDebitCreditPortfolio.CostCenterConceptDistributionXPO
        Get
            Return CType(INDSleCostCenterDistribution.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenterDistribution.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Crud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "NoteDate", .ColumnWidth = 150},
                              New ColumnInfo() With {.Caption = "Tipo de Nota", .FieldName = "NoteTypeName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Naturaleza", .FieldName = "NatureName", .ColumnWidth = 150},
                              New ColumnInfo() With {.Caption = "Cliente", .FieldName = "CustomerName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "Value", .ColumnWidth = 200, .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .ColumnFormat = "N2"},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "Abbreviation", .ColumnWidth = 150},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 150}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioNote
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
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Me._portfolioNote.Status <> 3 Then
            If Not ValidateFields() Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using model As New MNotesDebitCreditPortfolio(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SavePortfolioNote(_portfolioNote, _currentSequenceId)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    _portfolioNote = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _portfolioNote.Id, 0, _portfolioNote.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _portfolioNote.Id, 0, _portfolioNote.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _portfolioNote.Id, 0, _portfolioNote.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _portfolioNote.Id, 0, _portfolioNote.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)

                    Dim messageResult As List(Of String) = result.Message.Split(New String() {"-"}, StringSplitOptions.RemoveEmptyEntries) _
                                     .Select(Function(s) s.Trim()) _
                                     .Where(Function(s) s.Length > 0) _
                                     .ToList()

                    If messageResult.Count > 10 Then
                        result.Message = "Ocurrió un error al guardar la nota"
                        Using formulario As New FrmListErrors(messageResult)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If

                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPortfolioNote()
        End If
    End Sub

#End Region

#Region "BarButton Events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._operativeUnitId = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        Me._portfolioNote.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar y confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BarraBotones.Focus()
            Me._portfolioNote.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Me._portfolioNote.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BarraBotones.Focus()
            Me._portfolioNote.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BarraBotones.Focus()
            Me._portfolioNote.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _portfolioNote.Id, 0, _portfolioNote.Id)
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _portfolioNote.Status = 3
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Async Sub FrmNotesDebitCreditPortfolio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyNotes, True)
        _operativeUnitId = BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PNotesDebitCreditPortfolio(Me)
        _presenter.LoadDefinitionLayout()


        AsyncLoader(True)
        Await _presenter.GetSequense()

        Using model As New MSettingPortfolio(MyTag)
            settingPortfolio = Await model.GetSettingPortfolioByIdOperatingUnitAsync(_operativeUnitId)
        End Using

        Deshacer()
        LoadStatus()
        AddActionsColumns()

        IndigoGridControl1.SetControlNextFocus(INDgcBill, INDpceConcept)
        IndigoGridControl1.SetControlNextFocus(INDgcAdvance, INDpceConcept)
        INDEsbAdvance.AddRangeColumns("Anticipo", "Valor")

        INDLciRateIva.HideLayout()
        INDlyItemIvaValue.HideLayout()
        INDlyTotalConcept.HideLayout()
        AsyncLoader(False)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _operativeUnitId = Nothing
        _sequence = Nothing
        _currentSequenceId = Nothing
        _presenter = Nothing
        _record = Nothing
        _ctrDebitCredit = Nothing
        _ctrAdvance = Nothing
        _frmPorfolioTransfer = Nothing
        _portfolioNote = Nothing
        _portfolioNoteAccountReceivableAdvance = Nothing
        _listPortfolioNoteAccountReceivableAdvance = Nothing
        _listPortfolioNoteAccountReceivableAdvanceDelete = Nothing
        _dictionaryAccountReceivableShares = Nothing
        _portfolioNoteDistribution = Nothing
        _listPortfolioNoteDistribution = Nothing
        _listPortfolioNoteDistributionDelete = Nothing
        _portfolioNoteDetail = Nothing
        _listPortfolioNoteDetail = Nothing
        _listPortfolioNoteDetailDelete = Nothing
        _portfolioNoteConcept = Nothing
        _retentionConcept = Nothing
        _originalValueThirdPartyId = Nothing
        _originalValueThirdPartyName = Nothing
        _debitValue = Nothing
        _creditValue = Nothing
        _advanceValue = Nothing
        _stateOpenPopUpPorfolioTransfer = Nothing
        _editModePopupConcepts = Nothing
        _flagDeleteItemDistribution = Nothing
        _varImp = Nothing
        _mainAccountAdvanceDistribution = Nothing
        _invoiceDocumentType = Nothing
        _isOpeningBalance = Nothing

        _listNoteType = Nothing
        _listConceptsNoteDebit = Nothing
        _listConceptsNoteCredit = Nothing
        settingPortfolio = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmNotesDebitCreditPortfolio_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._portfolioNote IsNot Nothing AndAlso Me._portfolioNote.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            Await LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDBteCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewPortfolioNote()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDTxtBaseValueRetention_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxtBaseValueRetention.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddConcept.Focus()
        End If
    End Sub

    Private Sub INDmemoComments_KeyDown(sender As Object, e As KeyEventArgs) Handles INDmemoComments.KeyDown
        If e.KeyCode = Keys.Enter Then
            If NoteType = 4 Then
                INDPceAdvanceDistribution.Focus()
                INDPceAdvanceDistribution.ShowPopup()
                INDSleCustomerDistribution.Focus()
            Else
                If INDlygBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDpceBill.Focus()
                    INDpceBill.ShowPopup()
                    INDSleBillsShare.Focus()
                Else
                    INDpceAdvance.Focus()
                    INDpceAdvance.ShowPopup()
                    INDsleAdvance.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub INDpceConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceConcept.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceConcept.ShowPopup()
            INDSleNoteConcept.Focus()
        End If
    End Sub

    Private Sub INDTxtBaseValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxtBaseValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddConcept.Focus()
        End If
    End Sub

#End Region

#Region "Leave"

    Private Sub INDRpTxtValueBills_Leave(sender As Object, e As EventArgs) Handles INDRpTxtValueBills.Leave
        Dim porfolioNoteAccountAdvanceTmp = DirectCast(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
        If porfolioNoteAccountAdvanceTmp IsNot Nothing Then
            Dim txtPaymentValue = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
            If Nature = 2 Then
                If txtPaymentValue.EditValue > porfolioNoteAccountAdvanceTmp.Balance Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AdjustmentValue", NAME_MODULE)
                    porfolioNoteAccountAdvanceTmp.AdjusmentValue = porfolioNoteAccountAdvanceTmp.Balance
                    txtPaymentValue.EditValue = porfolioNoteAccountAdvanceTmp.Balance
                End If
            End If
            INDgcBill.RefreshDataSource()
            getDebitCredit()
        End If
    End Sub

    Private Sub INDRpTxtValueAdvance_Leave(sender As Object, e As EventArgs) Handles INDRpTxtValueAdvance.Leave
        Dim porfolioNoteAccountAdvanceTmp = DirectCast(viewGridAdvance.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
        If porfolioNoteAccountAdvanceTmp IsNot Nothing Then
            Dim txtPaymentValue = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
            If Nature = 2 Then
                If txtPaymentValue.EditValue > porfolioNoteAccountAdvanceTmp.Balance Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AdjustmentValue", NAME_MODULE)
                    porfolioNoteAccountAdvanceTmp.AdjusmentValue = porfolioNoteAccountAdvanceTmp.Balance
                    txtPaymentValue.EditValue = porfolioNoteAccountAdvanceTmp.Balance
                End If
            End If
            INDgcAdvance.RefreshDataSource()
            getDebitCredit()
        End If
    End Sub

    Private Async Sub INDTxtBaseValueRetention_Leave(sender As Object, e As EventArgs) Handles INDTxtBaseValueRetention.Leave
        If _retentionConcept IsNot Nothing Then
            If RetentionBaseValue <> 0 Then
                Try
                    Select Case _retentionConcept.Retention
                        Case 1
                            INDTxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(RetentionBaseValue), _retentionConcept)
                        Case 2
                            Using model As New MCompanySettings(MyTag)
                                Dim companySetting = Await model.GetCompanySettings()
                                INDTxtRetentionValue.EditValue = Math.Round(AccountingServices.CalculateRetention(CDec(RetentionBaseValue), _retentionConcept, companySetting.UVT))
                            End Using
                        Case 3
                            INDTxtRetentionValue.EditValue = Math.Round(AccountingServices.CalculateRetention(CDec(RetentionBaseValue), _retentionConcept.MinBase, CDec(INDSePercent.EditValue)))
                    End Select
                Catch ex As InvalidOperationException
                    If INDpceConcept.IsPopupOpen Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.Message
                        INDpceConcept.ShowPopup()
                    End If
                Catch ex As IndexOutOfRangeException
                    If INDpceConcept.IsPopupOpen Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.Message
                        INDpceConcept.ShowPopup()
                    End If
                Catch ex As ArgumentNullException
                    If INDpceConcept.IsPopupOpen Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
                        INDpceConcept.ShowPopup()
                    End If
                Catch ex As ArgumentOutOfRangeException
                    If INDpceConcept.IsPopupOpen Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
                        BaseValue = _retentionConcept.MinBase
                        INDpceConcept.ShowPopup()
                    End If
                End Try
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectRetentionConcept", "Treasury")
            INDSLeRetentionConcept.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleClient_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleClient.QueryPopUp
        If INDsleClient.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If CustomerXPO Is Nothing Then
            _presenter.InitializeCustomerXPO()
        End If
    End Sub

    Private Sub INDsleAdvanceDistribution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdvanceDistribution.QueryPopUp
        If INDsleAdvanceDistribution.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If AdvanceDistributionXPO Is Nothing Then
            If CustomerId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectCustomer", NAME_MODULE)
                Exit Sub
            End If

            _presenter.InitializeAdvanceDistributionXPO(_originalValueThirdPartyId)
        End If
    End Sub

    Private Sub INDSlePorfolioTransfer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSlePorfolioTransfer.QueryPopUp
        If INDSlePorfolioTransfer.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If PorfolioTransferXPO Is Nothing Then
            _presenter.InitializePorfolioTransfer()
        End If
    End Sub

    Private Sub INDSleBillsAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBillsAccount.QueryPopUp
        If INDSleBillsAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If BillsXPO Is Nothing Then
            If CustomerId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectCustomer", NAME_MODULE)
                Exit Sub
            End If

            _presenter.InitializeBillsXPO(_originalValueThirdPartyId, NoteType, Nature, settingPortfolio.NotesDebitCreditPortfolio)
        End If
    End Sub

    Private Sub INDsleBills_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBillsShare.QueryPopUp
        If INDSleBillsShare.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If BillsXPO Is Nothing Then
            If CustomerId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectCustomer", NAME_MODULE)
                Exit Sub
            End If

            _presenter.InitializeBillsXPO(_originalValueThirdPartyId, NoteType, Nature, settingPortfolio.NotesDebitCreditPortfolio)
        End If
    End Sub

    Private Sub INDsleAdvance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdvance.QueryPopUp
        If INDsleAdvance.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If AdvanceXPO Is Nothing Then
            If CustomerId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectCustomer", NAME_MODULE)
                Exit Sub
            End If

            _presenter.InitializeAdvanceXPO(_originalValueThirdPartyId, Nature)
        End If
    End Sub

    Private Sub INDSleNoteConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNoteConcept.QueryPopUp
        If INDSleNoteConcept.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If NoteConceptXpo Is Nothing Then
            Dim Ids As String = String.Empty
            _presenter.InitializeNoteConceptXPO(NoteType, Ids)
        End If
    End Sub

    Private Sub INDSleMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMainAccount.QueryPopUp
        If INDSleMainAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If AccountsXPO Is Nothing Then
            Dim Ids As String = String.Empty
            _presenter.InitializeAccountXPO(Ids)
        End If
    End Sub

    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If ThirdPartyXPO Is Nothing Then
            _presenter.InitializeThirdPartyXPO()
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSLeCostCenterConcept.QueryPopUp
        If INDSLeCostCenterConcept.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If CostCenterConceptXPO Is Nothing Then
            Dim Ids As String = String.Empty
            _presenter.InitializeCostCenterXPO(Ids)
        End If
    End Sub

    Private Sub INDSLeRetentionConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSLeRetentionConcept.QueryPopUp
        If INDSLeRetentionConcept.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If RetentionConceptXPO Is Nothing Then
            _presenter.InitializeRetentionConceptXPO()
        End If
    End Sub

    Private Sub INDSleCustomerDistribution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCustomerDistribution.QueryPopUp
        If INDSleCustomerDistribution.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If CustomerDistributionXPO Is Nothing Then
            _presenter.InitializeCustomerDistributionXPO()
        End If
    End Sub

    Private Sub INDSleMainAccountDistribution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMainAccountDistribution.QueryPopUp
        If INDSleMainAccountDistribution.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If AccountsDistributionXPO Is Nothing Then
            _presenter.InitializeAccountDistributionXPO()
        End If
    End Sub

    Private Sub INDSleCostCenterDistribution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenterDistribution.QueryPopUp
        If INDSleCostCenterDistribution.Properties.ReadOnly = True Then
            Exit Sub
        End If

        If CostCenterConceptDistributionXPO Is Nothing Then
            _presenter.InitializeCostCenterDistributionXPO()
        End If
    End Sub

    Private Sub INDrptPceDetails_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceDetails.QueryPopUp
        If NoteType = 6 Then
            Dim portfolioNoteAccountReceivableAdvance = CType(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
            INDgcDetails.DataSource = portfolioNoteAccountReceivableAdvance.PortfolioNoteAccountReceivableDetail
        End If
    End Sub

    ''' <summary>
    ''' evento de consulta de las monedas parametrizadas en el sistema
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDataSourceXpo Is Nothing Then
            Me._presenter.InitializeCurrency()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    Private Sub INDbtnConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub INDsleClient_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleClient.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCustomers
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeCustomerXPO()
                INDsleClient.Focus()
            End Using
        End If
    End Sub

    Private Sub INDslePorfolioTransfer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlePorfolioTransfer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPortfolioTransfers
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializePorfolioTransfer()
            End Using
        End If
    End Sub

    Private Sub INDSleNoteConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleNoteConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPortfolioNoteConcepts
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                Dim value = INDSleNoteConcept.EditValue
                INDSleNoteConcept.EditValue = Nothing
                INDSleNoteConcept.EditValue = value
                INDpceConcept.ShowPopup()
                INDSleNoteConcept.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPopupPUC
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                INDpceConcept.ShowPopup()
                INDSleMainAccount.Focus()
                Dim value = INDSleMainAccount.EditValue
                INDSleMainAccount.EditValue = Nothing
                INDSleMainAccount.EditValue = value
            End Using
        End If
    End Sub

    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeThirdPartyXPO()
                INDpceConcept.ShowPopup()
                INDSleThirdParty.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeCostCenterConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                INDpceConcept.ShowPopup()
                INDSLeCostCenterConcept.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSLeRetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeRetentionConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmRetentionConcept
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeRetentionConceptXPO()
                Dim value = INDSLeRetentionConcept.EditValue
                INDSLeRetentionConcept.EditValue = Nothing
                INDSLeRetentionConcept.EditValue = value
                INDpceConcept.ShowPopup()
                INDSLeRetentionConcept.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleCustomerDistribution_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCustomerDistribution.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCustomers
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeCustomerDistributionXPO()
                INDPceAdvanceDistribution.ShowPopup()
                INDSleCustomerDistribution.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDSmbView control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSmbViewPortfolioTransfer_Click(sender As Object, e As EventArgs) Handles INDSmbViewPortfolioTransfer.Click
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceBill_Popup(sender As Object, e As EventArgs) Handles INDpceBill.Popup
        viewBill.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDgcBill, False)
    End Sub

    Private Sub INDpceAdvance_Popup(sender As Object, e As EventArgs) Handles INDpceAdvance.Popup
        viewGridAdvance.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDgcAdvance, False)
    End Sub

    Private Sub INDpceConcept_Popup(sender As Object, e As EventArgs) Handles INDpceConcept.Popup
        INDSleNoteConcept.Focus()
        viewConcept.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDgcConcepts, False)
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDpceBill_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBill.CloseUp
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
                IndigoGridControl1.ControlNextFocus = True
            Else
                INDpceConcept.Focus()
            End If
        End If
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            viewBill.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcBill, True)
        Else
            viewBill.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcBill, False)
        End If
    End Sub

    Private Sub INDpceAdvance_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAdvance.CloseUp
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
                IndigoGridControl1.ControlNextFocus = True
            Else
                INDpceConcept.Focus()
            End If
        End If
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            viewGridAdvance.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcAdvance, True)
        Else
            viewGridAdvance.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcAdvance, False)
        End If
    End Sub

    Private Sub INDpceConcept_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceConcept.CloseUp
        If _editModePopupConcepts = True Then
            CleanControlsPopupConcepts()
        End If
        If _listPortfolioNoteDetail IsNot Nothing AndAlso _listPortfolioNoteDetail.Count > 0 Then
            viewConcept.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcConcepts, True)
        Else
            viewConcept.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcConcepts, False)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleNoteType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleNoteType.EditValueChanged
        Select Case INDGleNoteType.EditValue
            Case 1 'factura total
                INDColAccount.Caption = "Cuenta Contable"
                INDColAccount.FieldName = "CodeNameMainAccount"
                INDColAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                INDBtnImportFileBillsOrShare.Enabled = True
                INDEsbBillsOrShare.Enabled = True
                If indigo.IndigoCompanyType = 1 Then 'Si el tipo de compañia es privada
                    INDEsbBillsOrShare.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Factura"},
                            New ExcelColumn With {.Name = "Estado Cartera", .Comment = "1 - Sin Radicar" & vbCrLf & "2 -  Radicada sin Confirmar" & vbCrLf & "3 - Radicada Entidad" & vbCrLf & "4 - Glosada sin Conciliar" & vbCrLf & "12 - Glosada Conciliada" & vbCrLf & "15 - Cuenta de Dificil Recaudo" & vbCrLf & "16 - Cobro Jurídico"},
                            New ExcelColumn With {.Name = "Concepto Facturación Electrónica", .Comment = _electronicDocumentComment.ToString()},
                            New ExcelColumn With {.Name = "Valor"}
                        }
                    })
                Else '2 si el tipo de compañia es publica
                    INDEsbBillsOrShare.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Factura"},
                            New ExcelColumn With {.Name = "Concepto Facturación Electrónica", .Comment = _electronicDocumentComment.ToString()},
                            New ExcelColumn With {.Name = "Valor"}
                        }
                    })
                End If
                Me.CleanControlsBills()
                Me.ChangeNoteType()
                AdvanceDistributionId = Nothing
                PortfolioTransferId = Nothing
            Case 2 'factura cuotas
                INDColAccount.Caption = "Cuota"
                INDColAccount.FieldName = "NumberShare"
                INDColAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                INDBtnImportFileBillsOrShare.Enabled = True
                INDEsbBillsOrShare.Enabled = True
                INDEsbBillsOrShare.AddRangeColumns("Factura", "Nº de Cuota", "Valor")
                Me.CleanControlsBills()
                Me.ChangeNoteType()
                AdvanceDistributionId = Nothing
                PortfolioTransferId = Nothing
            Case 3 'anticipos
                Me.CleanControlsAdvance()
                Me.ChangeNoteType()
                AdvanceDistributionId = Nothing
                PortfolioTransferId = Nothing
            Case 4 'distribucion de anticipos
                Me.ChangeNoteType()
                INDgleNature.EditValue = 1
                PortfolioTransferId = Nothing
            Case 5 'Cruce anticipos vs cxc'
                Me.ChangeNoteType()
                INDgleNature.EditValue = 1
                AdvanceDistributionId = Nothing
            Case 6 'Pre-auditoria
                INDBtnImportFileBillsOrShare.Enabled = False
                INDEsbBillsOrShare.Enabled = False
                Me.CleanControlsBills()
                Me.ChangeNoteType()
                AdvanceDistributionId = Nothing
                PortfolioTransferId = Nothing
                NoteConceptXpo = Nothing
                _presenter.InitializeTaxRate()
            Case 7
                _listNoteType.Add(New Tuple(Of Byte, String)(7, "Factura glosada"))
        End Select
    End Sub

    Private Sub INDgleNature_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleNature.EditValueChanged
        CleanControlsBills()
        CleanControlsAdvance()
        If INDgleNature.EditValue = 1 Then
            INDGLeNatureConcept.EditValue = 2
            INDGleNatureRetention.EditValue = 2
            INDSleConcept.Properties.DataSource = ListConceptsNoteDebit.ToList()
        Else
            INDGLeNatureConcept.EditValue = 1
            INDGleNatureRetention.EditValue = 1
            INDSleConcept.Properties.DataSource = ListConceptsNoteCredit.ToList()
        End If

        _electronicDocumentComment = New StringBuilder
        For Each concept In INDSleConcept.Properties.DataSource
            _electronicDocumentComment.AppendLine(String.Format("{0} - {1}", concept.Item1, concept.Item2))
        Next

        If INDGleNoteType.EditValue = 1 Then
            If indigo.IndigoCompanyType = 1 Then 'Si el tipo de compañia es privada
                INDEsbBillsOrShare.AddExcelSheets(New ExcelSheet With {
                    .Columns = New List(Of ExcelColumn) From {
                        New ExcelColumn With {.Name = "Factura"},
                        New ExcelColumn With {.Name = "Estado Cartera", .Comment = "1 - Sin Radicar" & vbCrLf & "2 -  Radicada sin Confirmar" & vbCrLf & "3 - Radicada Entidad" & vbCrLf & "4 - Glosada sin Conciliar" & vbCrLf & "12 - Glosada Conciliada" & vbCrLf & "15 - Cuenta de Dificil Recaudo" & vbCrLf & "16 - Cobro Jurídico"},
                        New ExcelColumn With {.Name = "Concepto Facturación Electrónica", .Comment = _electronicDocumentComment.ToString()},
                        New ExcelColumn With {.Name = "Valor"}
                    }
                })
            Else '2 si el tipo de compañia es publica
                INDEsbBillsOrShare.AddExcelSheets(New ExcelSheet With {
                    .Columns = New List(Of ExcelColumn) From {
                        New ExcelColumn With {.Name = "Factura"},
                        New ExcelColumn With {.Name = "Concepto Facturación Electrónica", .Comment = _electronicDocumentComment.ToString()},
                        New ExcelColumn With {.Name = "Valor"}
                    }
                })
            End If
        End If
    End Sub

    Private Sub INDsleClient_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleClient.EditValueChanged
        If NoteType <> 5 Then
            INDBtnImportFileBillsOrShare.Enabled = True
            INDEsbBillsOrShare.Enabled = True
            INDBtnImportFileAdvance.Enabled = True
            INDEsbAdvance.Enabled = True
            AdvanceDistributionXPO = Nothing
            INDsleAdvance.EditValue = Nothing
            INDSleMainAccountDistribution.EditValue = Nothing
            CleanControlsBills()
            CleanControlsAdvance()
            If INDsleClient.EditValue IsNot Nothing Then
                If CustomerXPO IsNot Nothing Then
                    'Dim customerTpm = DirectCast(DirectCast(INDGvSleCustomer.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CommonCustomerXpo)
                    Dim customerTpm = _presenter.GetClientById(INDsleClient.EditValue)
                    INDSleThirdParty.EditValue = customerTpm.ThirdPartyId.Id
                    INDSleThirdParty.Properties.NullText = customerTpm.ThirdPartyId.NitName
                    _originalValueThirdPartyId = customerTpm.ThirdPartyId.Id
                    _originalValueThirdPartyName = customerTpm.ThirdPartyId.NitName
                Else
                    INDSleThirdParty.EditValue = _portfolioNote.ThirdPartyId
                    INDSleThirdParty.Properties.NullText = _portfolioNote.NitNameThirParty
                    _originalValueThirdPartyId = _portfolioNote.ThirdPartyId
                    _originalValueThirdPartyName = _portfolioNote.NitNameThirParty
                End If
            Else
                INDSleThirdParty.EditValue = Nothing
                INDSleThirdParty.Properties.NullText = String.Empty
                INDsleClient.Properties.NullText = String.Empty
            End If
        End If
    End Sub

    Private Sub INDsleAdvanceDistribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvanceDistribution.EditValueChanged
        If Me.AdvanceDistributionId IsNot Nothing Then
            Using model As New MPortfolioAdvance(MyTag)
                Dim portfolioAdvanceTmp = model.GetPortfolioAdvanceByIdSimple(Me.AdvanceDistributionId).ObjectEmbbeded

                If If(portfolioAdvanceTmp?.CurrencyId Is Nothing OrElse portfolioAdvanceTmp?.CurrencyId = 0, indigo.OfficialCurrencyId, portfolioAdvanceTmp?.CurrencyId) <> Me.CurrencyId Then
                    Me.AdvanceDistributionId = Nothing
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyDocumentDifferent")
                    Exit Sub
                End If

                If portfolioAdvanceTmp.MainAccounts.Nature.Value = 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable del anticipo es de naturaleza Débito"
                End If

                INDPceAdvanceDistribution.Enabled = True
                _advanceValue = portfolioAdvanceTmp.Balance
                INDSleMainAccountDistribution.EditValue = portfolioAdvanceTmp.MainAccountId
                _mainAccountAdvanceDistribution = portfolioAdvanceTmp.MainAccountId
                _ctrAdvance.PrintInfo()
            End Using
        End If
    End Sub

    Private Sub INDslePorfolioTransfer_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePorfolioTransfer.EditValueChanged
        If PortfolioTransferId IsNot Nothing AndAlso PortfolioTransferId > 0 Then
            LoadControlsPorfolioTransferForm()
            ''se actualiza el campo de moneda con la cabecera del cruce
            Using model As New MPortfolioTransfers(MyTag)
                Dim result = model.GetPortfolioTransferById(PortfolioTransferId)
                If result IsNot Nothing Then
                    INDsleCurrency.EditValue = result.CurrencyId
                    Me.CurrencyAbbreviation = result.CurrencyAbbreviation
                    Me.INDsleCurrency.Properties.NullText = result.CurrencyAbbreviation
                    INDLciCurrency.Enabled = False

                    ''Traemos el mismo tercero que el tercero del cruce
                    _originalValueThirdPartyId = result.ThirdPartyId
                Else
                    INDLciCurrency.Enabled = True
                End If
            End Using

        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecutal al seleccionar una factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleBillsAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBillsAccount.EditValueChanged
        Try
            Me.AsyncLoader(True)
            ' se setea en nothing el concepto de Nota electronica y se oculta el control
            ElectronicConcept = Nothing
            INDLciConcepts.HideControl(True)

            'se valida que seleccione una factura
            If Me.AccountReceivableAccountingId Is Nothing Then
                INDtxtAdjustment.Enabled = False
                INDbtnAddBill.Enabled = False
                Exit Sub
            End If

            Using model As New MNotesDebitCreditPortfolio(MyTag)

                'Se consulta servicio de validacion de la factura seleccionada
                Dim result = Await model.ValidateSelectedByAccountReceivableAccounting(Me.AccountReceivableAccountingId, Me.NoteType, Me.CurrencyId, If(Me.NoteDate, Me.GetDateServer()))

                If result Is Nothing OrElse Not result?.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                    CleanControlsBills()
                    Exit Sub
                End If

                Dim accountReceivableAId As Integer = Me.AccountReceivableAccountingId
                'se consulta de forma asincrona la entidad
                Dim accountReceivableAccounting = Await Task.Factory.StartNew(Function() model.GetPortfolioAccountReceivableAccountingById(accountReceivableAId))

                'se establecen los valores al popup
                Me.PopUpValue = accountReceivableAccounting.Value
                Me.PopUpBalance = accountReceivableAccounting.Balance
                INDbtnAddBill.Enabled = True
                INDtxtAdjustment.Enabled = True
                _isOpeningBalance = accountReceivableAccounting.AccountReceivableId.OpeningBalance
                _valueBill = accountReceivableAccounting.AccountReceivableId.Value
                _balanceBill = accountReceivableAccounting.AccountReceivableId.Balance
                SetPortfolioNoteAccountReceivableAdvance(Nothing, accountReceivableAccounting, Nothing)

                Dim invoice As Invoice = result.ObjectEmbbeded

                If invoice Is Nothing Then
                    Exit Sub
                End If

                INDLciConcepts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                _invoiceDocumentType = invoice.DocumentType

                If Not String.IsNullOrEmpty(invoice?.CUFE?.Trim()) AndAlso INDgleNature.EditValue IsNot Nothing Then
                    ElectronicConcept = If(INDgleNature.EditValue = 1, 3, Nothing)
                End If
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            Me.AsyncLoader(False)
        End Try
    End Sub

    Private Sub INDSleBillsShare_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBillsShare.EditValueChanged
        ElectronicConcept = Nothing
        INDLciConcepts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If INDSleBillsShare.EditValue IsNot Nothing Then
            Using model As New MNotesDebitCreditPortfolio(MyTag)
                Dim accountRecivableShareTmp = model.GetPortfolioAccountReceivableShareById(INDSleBillsShare.EditValue)

                If If(accountRecivableShareTmp?.AccountReceivableId?.CurrencyId Is Nothing _
                    OrElse accountRecivableShareTmp?.AccountReceivableId?.CurrencyId = 0,
                    Me.indigo.OfficialCurrencyId, accountRecivableShareTmp?.AccountReceivableId?.CurrencyId) <> Me.CurrencyId Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyDocumentDifferent")
                    CleanControlsBills()
                    Exit Sub
                End If

                Me.PopUpValue = accountRecivableShareTmp.Value
                Me.PopUpBalance = accountRecivableShareTmp.Balance
                INDtxtAdjustment.Enabled = True
                INDbtnAddBill.Enabled = True
                _isOpeningBalance = accountRecivableShareTmp.AccountReceivableId.OpeningBalance
                SetPortfolioNoteAccountReceivableAdvance(accountRecivableShareTmp, Nothing, Nothing)

                If accountRecivableShareTmp.AccountReceivableId?.InvoiceId?.Id Is Nothing Then
                    Exit Sub
                End If

                Dim invoice = model.GetInvoiceById(accountRecivableShareTmp.AccountReceivableId.InvoiceId.Id)
                If invoice IsNot Nothing AndAlso Not String.IsNullOrEmpty(invoice?.CUFE?.Trim()) Then
                    INDLciConcepts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    ElectronicConcept = If(INDgleNature.EditValue = 1, 3, Nothing)
                End If

                'Se asigna el tipo de documento a la variable para cuando se va agregar la factura a la rejilla se valide si es capitada
                If invoice IsNot Nothing Then
                    _invoiceDocumentType = invoice.DocumentType
                End If
            End Using
        Else
            INDtxtAdjustment.Enabled = False
            INDbtnAddBill.Enabled = False
        End If
    End Sub

    Private Sub INDsleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvance.EditValueChanged
        If INDsleAdvance.EditValue IsNot Nothing Then
            Using model As New MNotesDebitCreditPortfolio(MyTag)
                Dim advanceTmp = model.GetPortfolioAdvanceById(INDsleAdvance.EditValue)

                If If(advanceTmp?.CurrencyId Is Nothing OrElse advanceTmp?.CurrencyId = 0,
                        Me.indigo.OfficialCurrencyId, advanceTmp?.CurrencyId) <> Me.CurrencyId Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyDocumentDifferent")
                    CleanControlsAdvance()
                    Exit Sub
                End If

                INDtxtValueAdvance.EditValue = advanceTmp.Value
                INDtxtBalanceAdvance.EditValue = advanceTmp.Balance
                INDtxtAdjustmentAdvance.Enabled = True
                INDbtnAddAdvance.Enabled = True
                SetPortfolioNoteAccountReceivableAdvance(Nothing, Nothing, advanceTmp)
            End Using
        Else
            INDtxtAdjustmentAdvance.Enabled = False
            INDbtnAddAdvance.Enabled = False
        End If
    End Sub

    Private Sub INDSleNoteConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleNoteConcept.EditValueChanged
        If INDSleNoteConcept.EditValue IsNot Nothing Then
            Using model As New MPortfolioNoteConcept(MyTag)
                _portfolioNoteConcept = model.GetPortfolioNoteConceptById(INDSleNoteConcept.EditValue)
            End Using
            If _portfolioNoteConcept.NoteType = 1 Then
                INDSleMainAccount.Properties.ReadOnly = False
                INDSleMainAccount.Properties.NullText = String.Empty
                INDSleMainAccount.EditValue = Nothing
            Else
                INDSleMainAccount.Properties.ReadOnly = True
                INDSleMainAccount.Properties.NullText = _portfolioNoteConcept.MainAccounts.Number + " - " + _portfolioNoteConcept.MainAccounts.Name
                INDSleMainAccount.EditValue = _portfolioNoteConcept.IdAccount
                If _portfolioNoteConcept.MainAccounts.HandlesCostCenter = True Then
                    INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
                If _portfolioNoteConcept.MainAccounts.RetencionType <> 0 Then
                    INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End If
            If _portfolioNoteConcept.HandleTax Then
                INDLciRateIva.HideControl(False)
                INDlyItemIvaValue.HideControl(False)
                INDlyTotalConcept.HideControl(False)
            Else
                INDLciRateIva.HideControl(True)
                INDlyItemIvaValue.HideControl(True)
                INDlyTotalConcept.HideControl(True)
            End If
        Else
            If INDpceConcept.IsPopupOpen = True Then
                CleanControlsPopupConcepts()
            End If
        End If
    End Sub

    Private Sub INDSleMainAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMainAccount.EditValueChanged
        If INDSleMainAccount.EditValue IsNot Nothing Then
            Using model As New MPUC(Me.Tag)
                Dim puc = model.GetAccountByIdSimple(INDSleMainAccount.EditValue, False)
                _auxMainAccount = puc
                If puc.HandlesCostCenter = True Then
                    INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If NoteType = 6 Then
                        CostCenterConceptXPO = Nothing
                    End If
                Else
                    INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
                If puc.RetencionType <> 0 Then
                    INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End Using
        End If
    End Sub

    Private Sub INDSLeRetentionConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSLeRetentionConcept.EditValueChanged
        If INDSLeRetentionConcept.EditValue IsNot Nothing Then
            Using model As New MRetentionConcept(MyTag)
                _retentionConcept = model.GetRetentionByIdSimple(INDSLeRetentionConcept.EditValue)
                Select Case _retentionConcept.Retention
                    Case 1
                        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDSePercent.EditValue = _retentionConcept.Rate
                        INDSePercent.Enabled = False
                        INDTxtBaseValueRetention.Enabled = True
                        RetentionBaseValue = _retentionConcept.MinBase
                    Case 2
                        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSePercent.Enabled = False
                        INDSePercent.EditValue = 0
                        INDTxtBaseValueRetention.Enabled = True
                    Case 3
                        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If _portfolioNote Is Nothing Then
                            INDSePercent.EditValue = 0
                        End If
                        INDSePercent.Enabled = True
                        INDTxtBaseValueRetention.Enabled = False
                        RetentionBaseValue = _retentionConcept.MinBase
                End Select
                If _portfolioNoteDetail Is Nothing Then
                    'INDTxtBaseValue.EditValue = 0
                    INDTxtRetentionValue.EditValue = 0
                    If Nature = 1 Then
                        INDGleNatureRetention.EditValue = 2
                    Else
                        INDGleNatureRetention.EditValue = 1
                    End If
                End If
            End Using
        Else
            RetentionBaseValue = 0
            INDTxtBaseValueRetention.Enabled = False
            INDTxtRetentionValue.Text = 0
            INDGleNatureRetention.EditValue = 2
            INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSePercent.EditValue = 0
        End If
    End Sub

    Private Sub INDSePercent_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSePercent.EditValueChanging
        If e.NewValue > 0 Then
            INDTxtBaseValueRetention.Enabled = True
        Else
            RetentionBaseValue = 0
            INDTxtBaseValueRetention.Enabled = False
        End If
        If RetentionBaseValue > 0 Then
            RetentionBaseValue = 0
            INDTxtRetentionValue.EditValue = 0
        End If
    End Sub

    Private Sub INDSleCustomerDistribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCustomerDistribution.EditValueChanged
        If INDSleCustomerDistribution.EditValue IsNot Nothing Then
            INDTxtValueDistribution.Enabled = True
        Else
            INDTxtValueDistribution.Enabled = False
        End If
    End Sub

    Private Sub INDSleMainAccountDistribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMainAccountDistribution.EditValueChanged
        If INDSleMainAccountDistribution.EditValue IsNot Nothing Then
            Using model As New MPUC(MyTag)
                Dim accountTmp = model.GetAccountByIdSimple(INDSleMainAccountDistribution.EditValue)
                If accountTmp.HandlesCostCenter Then
                    INDLciCostCenterDistribution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDLciCostCenterDistribution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSleCostCenterDistribution.EditValue = Nothing
                End If
            End Using
        Else
            INDLciCostCenterDistribution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleCostCenterDistribution.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda escogida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        'Se establece el formato de la moneda en base a la seleccionada en el combo moneda, esto cambia si solo es diferente a la parametrizada en companysettings
        If _ctrDebitCredit IsNot Nothing Then
            _ctrDebitCredit.CodeISO4217 = Me.CurrencyAbbreviation
            _ctrDebitCredit.RefreshDebitCredit()
        End If
        If _ctrAdvance IsNot Nothing Then
            _ctrAdvance.CodeISO4217 = Me.CurrencyAbbreviation
            _ctrAdvance.PrintInfo()
        End If
        '-------------------------------------------------------------------------------------------------------'
        'cambio en las columnas de las rejillas
        'Invoices
        Me.INDColInitialValue = Window.Utils.FormatGrid(Me.INDColInitialValue, Me.CurrencyAbbreviation)
        Me.INDColBalance = Window.Utils.FormatGrid(Me.INDColBalance, Me.CurrencyAbbreviation)
        Me.INDColValueBills = Window.Utils.FormatGrid(Me.INDColValueBills, Me.CurrencyAbbreviation)

        'InvoiceDetail
        Me.INDGcDetails_Price = Window.Utils.FormatGrid(Me.INDGcDetails_Price, Me.CurrencyAbbreviation)
        Me.INDGcDetails_TotalSalesPrice = Window.Utils.FormatGrid(Me.INDGcDetails_TotalSalesPrice, Me.CurrencyAbbreviation)
        Me.INDGcDetails_Balance = Window.Utils.FormatGrid(Me.INDGcDetails_Balance, Me.CurrencyAbbreviation)
        Me.INDGcDetails_AdjustmentBase = Window.Utils.FormatGrid(Me.INDGcDetails_AdjustmentBase, Me.CurrencyAbbreviation)
        Me.INDGcDetails_AdjustmentTax = Window.Utils.FormatGrid(Me.INDGcDetails_AdjustmentTax, Me.CurrencyAbbreviation)

        'Advances
        Me.INDColInitialValueAdvance = Window.Utils.FormatGrid(Me.INDColInitialValueAdvance, Me.CurrencyAbbreviation)
        Me.INDColAdvancesBalance = Window.Utils.FormatGrid(Me.INDColAdvancesBalance, Me.CurrencyAbbreviation)
        Me.INDColValueAdjustment = Window.Utils.FormatGrid(Me.INDColValueAdjustment, Me.CurrencyAbbreviation)
        'Concepts
        Me.INDColValue = Window.Utils.FormatGrid(INDColValue, Me.CurrencyAbbreviation)
        Me.INDColValueDetailConcept = Window.Utils.FormatGrid(Me.INDColValueDetailConcept, Me.CurrencyAbbreviation)
        'DistributionAdvances
        Me.INDColValueDistribution = Window.Utils.FormatGrid(Me.INDColValueDistribution, Me.CurrencyAbbreviation)
        '--------------------------------------------------------------------------------------------------------'
        'cambios en mascara de los campos numerico de moneda
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Me.CurrencyAbbreviation.GetNumberFormat
        Me.INDtxtValue.Properties.Mask.Culture = _culture
        Me.INDtxtBalance.Properties.Mask.Culture = _culture
        Me.INDtxtAdjustment.Properties.Mask.Culture = _culture
        Me.INDTxtBaseValue.Properties.Mask.Culture = _culture
        Me.INDTxtBaseValueRetention.Properties.Mask.Culture = _culture
        Me.INDtxtValueAdvance.Properties.Mask.Culture = _culture
        Me.INDtxtBalanceAdvance.Properties.Mask.Culture = _culture
        Me.INDtxtAdjustmentAdvance.Properties.Mask.Culture = _culture
        Me.INDTxtValueDistribution.Properties.Mask.Culture = _culture
        Me.INDRiteAdjustmentBaseValue.Mask.Culture = _culture
        Me.INDrptTxtValue.Mask.Culture = _culture
        '-----------------------------------------------------------------------------------------------------------'
        'se establece la distribucion de anticipo seleccionada en nothing cuando se cambia la moneda a fin de que tenga que volver a selecionar y valide la moneda
        Me.AdvanceDistributionId = Nothing
    End Sub

    ''' <summary>
    ''' Evento que calcula Valor del Iva y el Total Concept de acuerdo a la tarifa del IVA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdGeneralLedgerIVA_EditValueChanged(sender As Object, e As CancelEventArgs) Handles INDSleIdGeneralLedgerIVA.EditValueChanged
        If BaseValue <> 0 Then
            Using model As New MGeneralLedgerIVA(MyTag)
                _generalLedgerIVA = model.GetGeneralLedgerIVAById(IdGeneralLedgerIVA)
                _percentage = _generalLedgerIVA.ObjectEmbbeded.Percentage / 100
                IvaRate = Math.Round(BaseValue * _percentage, 2, MidpointRounding.AwayFromZero)
                TotalConcept = Math.Round(CDec(BaseValue + IvaRate), 2)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que calcula Valor del Iva y el Total Concept al cambiar el Valor Base
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtBaseValue_EditValueChanged(sender As Object, e As CancelEventArgs) Handles INDTxtBaseValue.EditValueChanged
        If Not IsNothing(_percentage) Then
            IvaRate = Math.Round(BaseValue * _percentage, 2)
            TotalConcept = Math.Round(CDec(BaseValue + IvaRate), 2)
        Else
            TotalConcept = Math.Round(BaseValue)
        End If
    End Sub

    ''' <summary>
    ''' event when then user change the value in the child gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiSleTaxRate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiSleTaxRate.EditValueChanged
        Try
            _flagEditing = True
            Dim invoiceDetail = CType(CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).GetFocusedRow(), PortfolioNoteAccountReceivableDetail)
            Dim headerInvoice = TryCast(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
            Dim searchLookUpEdit As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)
            Dim dataTaxRate = searchLookUpEdit.GetFocusedObject(Of Infrastructure.Data.Xpo.InventoryRepository.GeneralLedgerIVAXpo)
            invoiceDetail.TaxPercentage = dataTaxRate.Percentage
            Dim dictionaryResult = Utils.SetValueSalesPrice(True, invoiceDetail.Value, dataTaxRate.Percentage)

            With invoiceDetail
                .BaseValue = dictionaryResult.Item(Utils.eTypeTaxControl.GrossValue)
                .TaxValue = dictionaryResult.Item(Utils.eTypeTaxControl.TaxValue)
                .Value = dictionaryResult.Item(Utils.eTypeTaxControl.SubtotalSalesPrice)
                .Selector = IsTotalAdjustment(dictionaryResult.Item(Utils.eTypeTaxControl.SubtotalSalesPrice), invoiceDetail.Balance, invoiceDetail.TotalSalesPrice, Nature)
            End With

            Dim totalAdjustmentHeader As Decimal = headerInvoice.PortfolioNoteAccountReceivableDetail.Sum(Function(x) x.Value)
            headerInvoice.AdjusmentValue = Me.HeaderTotalAdjustmentValue(totalAdjustmentHeader, Me.Nature, headerInvoice.Balance, headerInvoice.Value)

            getDebitCredit()

            If CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).PostEditor() Then
                CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).UpdateCurrentRow()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            _flagEditing = False
        End Try
    End Sub
#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Private Variable to know if the user is editing the gridview
    ''' </summary>
    Private _flagEditing As Boolean = False

    ''' <summary>
    ''' event when the user modify the total adjustment or BaseValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrptTxtValue.EditValueChanging, INDRiteAdjustmentBaseValue.EditValueChanging
        Try
            If _flagEditing Then
                Exit Sub
            End If

            _flagEditing = True

            If e.NewValue = String.Empty Then
                Exit Sub
            End If

            'validating to know if the value is negative
            If e.NewValue.ToString.StartsWith("-") Then
                e.Cancel = True
                Exit Sub
            End If

            Dim newValue As Decimal
            Dim invoiceDetail = CType(CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).GetFocusedRow(), PortfolioNoteAccountReceivableDetail)
            Dim headerInvoice = TryCast(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)

            If headerInvoice.FlagTotalNote Then
                e.Cancel = True
                Exit Sub
            End If

            'flag to identify if the value has Taxinclusive or Not
            Dim flagtaxInclusive = TryCast(sender, TextEdit).Tag.Name = NameOf(INDrptTxtValue)
            Decimal.TryParse(e.NewValue.ToString().Replace(indigo.Culture.NumberFormat.CurrencyGroupSeparator, indigo.Culture.NumberFormat.NumberDecimalSeparator), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, newValue)

            'Dictionary to calculate GrossValue, TaxValue and TotalValue
            Dim dictionaryResult = Utils.SetValueSalesPrice(flagtaxInclusive, newValue, If(invoiceDetail.TaxPercentage, 0))

            'Validate adjustment
            Dim result = Me.ValidateAdjustmentInvoice(dictionaryResult.Item(Utils.eTypeTaxControl.SubtotalSalesPrice), invoiceDetail.TotalSalesPrice, invoiceDetail.Balance, Nature)

            If Not result.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                e.Cancel = True
                Exit Sub
            End If

            'assign the values to invoiceDetail
            With invoiceDetail
                .BaseValue = dictionaryResult.Item(Utils.eTypeTaxControl.GrossValue)
                .Value = dictionaryResult.Item(Utils.eTypeTaxControl.SubtotalSalesPrice)
                .TaxValue = dictionaryResult.Item(Utils.eTypeTaxControl.TaxValue)
                .Selector = IsTotalAdjustment(dictionaryResult.Item(Utils.eTypeTaxControl.SubtotalSalesPrice), invoiceDetail.Balance, invoiceDetail.TotalSalesPrice, Nature)
            End With

            AccountsXPO = Nothing
            NoteConceptXpo = Nothing
            Dim totalAdjustmentHeader = headerInvoice.PortfolioNoteAccountReceivableDetail.Sum(Function(x) x.Value)

            headerInvoice.AdjusmentValue = Me.HeaderTotalAdjustmentValue(totalAdjustmentHeader, Me.Nature, headerInvoice.Balance, headerInvoice.Value)
            getDebitCredit()

        Catch ex As Exception
            Throw ex
        Finally
            _flagEditing = False
        End Try
    End Sub

    ''' <summary>
    ''' event to identify if the user can change the tax rate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiSleTaxRate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRiSleTaxRate.EditValueChanging
        Dim headerInvoice = TryCast(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
        Dim invoiceDetail = CType(CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).GetFocusedRow(), PortfolioNoteAccountReceivableDetail)

        If headerInvoice.FlagTotalNote Then
            e.Cancel = True
            Exit Sub
        End If

        Dim stringBuilder = New StringBuilder
        Select Case Me.Nature
            Case eNature.Credit
                If invoiceDetail.TotalSalesPrice <> invoiceDetail.Balance Then
                    stringBuilder.AppendLine("El cambio de tarifa solo está disponible para ítems donde el valor total sea igual al saldo")
                End If

            Case eNature.Debit
                If invoiceDetail.Balance <> 0 Then
                    stringBuilder.AppendLine("El cambio de tarifa solo está disponible para ítems cuyo saldo sea 0, ya que permite el ajuste a su valor inicial")
                End If
        End Select

        If stringBuilder.Length > 0 Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString()
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Function to put the total value in adjustment value header, to use when note type is detail
    ''' </summary>
    ''' <param name="detailTotalAdjustment"></param>
    ''' <param name="nature"></param>
    ''' <param name="balanceHeaderInvoice"></param>
    ''' <param name="initialValueHeaderInvoice"></param>
    ''' <returns></returns>
    Private Function HeaderTotalAdjustmentValue(detailTotalAdjustment As Decimal, nature As eNature, balanceHeaderInvoice As Decimal, initialValueHeaderInvoice As Decimal) As Decimal
        Select Case nature
            Case eNature.Credit
                Return If(detailTotalAdjustment > balanceHeaderInvoice, balanceHeaderInvoice, detailTotalAdjustment)
            Case eNature.Debit
                Return If(detailTotalAdjustment > (initialValueHeaderInvoice - balanceHeaderInvoice), (initialValueHeaderInvoice - balanceHeaderInvoice), detailTotalAdjustment)
            Case Else
                Return 0
        End Select
    End Function

#End Region

#Region "Click"

    Private Async Sub INDbtnAddBill_Click(sender As Object, e As EventArgs) Handles INDbtnAddBill.Click

        Dim natureValidate As Integer = 0
        Dim messageWarning = "Debe seleccionar una factura"
        Try
            Select Case NoteType
                Case 1, 6

                    INDSleBillsAccount.Focus()
                    If AccountReceivableAccountingId Is Nothing Then Throw New Exception(messageWarning)

                    Dim objectSelect = Await _presenter.GetAccountReceivableAccountingByIdAsync(AccountReceivableAccountingId)
                    natureValidate = objectSelect.FirstOrDefault.MainAccountId.Nature
                Case 2

                    INDSleBillsShare.Focus()
                    If INDSleBillsShare.EditValue Is Nothing Then Throw New Exception(messageWarning)

                    Dim objectSelect = Await _presenter.GetAccountReceivableShareByIdAsync(INDSleBillsShare.EditValue)
                    natureValidate = objectSelect.FirstOrDefault.AccountReceivableId.PortfolioAccountReceivableAccounting(0).MainAccountId.Nature
            End Select

            Await AddBillOrAdvance(AdjustmentValue, Me.PopUpValue, natureValidate, Me.PopUpBalance, ElectronicConcept)

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub INDgcBill_Click(sender As Object, e As EventArgs) Handles INDgcBill.Click
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            viewBill.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcBill, True)
            viewGridAdvance.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcAdvance, False)
        End If
    End Sub

    Private Async Sub INDbtnAddAdvance_Click(sender As Object, e As EventArgs) Handles INDbtnAddAdvance.Click
        If INDsleAdvance.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un anticipo"
            INDsleAdvance.Focus()
            Exit Sub
        End If

        Dim Position = INDsleAdvance.Properties.GetIndexByKeyValue(INDsleAdvance.EditValue)
        Dim mainAccountNature As Integer = 0

        If Position < 0 Then
            Dim advanceTmp As New PortfolioAdvance
            Using modelAdvance As New MPortfolioAdvance(MyTag)
                Dim result = Await modelAdvance.GetPortfolioAdvanceByIdSimpleAsync(INDsleAdvance.EditValue)
                advanceTmp = result?.ObjectEmbbeded
            End Using
            mainAccountNature = advanceTmp.MainAccounts.Nature
        Else
            Dim Obj = INDsleAdvance.Properties.View.GetRow(Position)
            Dim objectSelect = CType(CType(Obj, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, PortfolioAdvanceXpo)
            mainAccountNature = objectSelect.MainAccountId.Nature
        End If

        Await AddBillOrAdvance(AdjustmentAdvance, INDtxtValueAdvance.EditValue, mainAccountNature, INDtxtBalanceAdvance.EditValue)
    End Sub

    Private Sub INDgcAdvance_Click(sender As Object, e As EventArgs) Handles INDgcAdvance.Click
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            viewBill.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcBill, False)
            viewGridAdvance.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcAdvance, True)
        End If
    End Sub

    Private Sub INDbtnAddConcept_Click(sender As Object, e As EventArgs) Handles INDbtnAddConcept.Click
        Dim errors = ValidateControlsNoteDetail()
        Dim handleTax = _portfolioNoteConcept.HandleTax
        Dim noteType = INDGleNoteType.EditValue
        Dim billGridValidation = If(INDgcBill.DataSource IsNot Nothing, INDgcBill.DataSource.Count, 0)
        If (_portfolioNoteConcept.MainAccounts?.RetencionType <> 0 Or _auxMainAccount.RetencionType <> 0) AndAlso _portfolioNoteConcept.HandleTax = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Los concepto de nota tipo Retención no manejan impuesto de IVA, concepto de Nota: " + _portfolioNoteConcept.Name
            Exit Sub
        End If

        If billGridValidation > 1 Then
            If (noteType = 1 OrElse noteType = 2) And (handleTax = True) Then
                Mensaje(EeventViewerImages.Advertencia) = "El concepto seleccionado solo permite asociar una factura."
                Exit Sub
            End If
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If _portfolioNoteDetail Is Nothing Then
            _portfolioNoteDetail = New PortfolioNoteDetail
        End If
        With _portfolioNoteDetail
            .PortfolioNoteConceptId = INDSleNoteConcept.EditValue
            If INDSleNoteConcept.Text IsNot String.Empty Then
                .CodeNameNoteConcept = INDSleNoteConcept.Text
            Else
                .CodeNameNoteConcept = INDSleNoteConcept.Properties.NullText
            End If
            .MainAccountId = INDSleMainAccount.EditValue
            If INDSleMainAccount.Text IsNot String.Empty Then
                .CodeNameMainAccount = INDSleMainAccount.Text
            Else
                .CodeNameMainAccount = INDSleMainAccount.Properties.NullText
            End If
            .ThirdPartyId = INDSleThirdParty.EditValue
            If INDSleThirdParty.Text IsNot String.Empty Then
                .CodeNameThirdParty = INDSleThirdParty.Text
            Else
                .CodeNameThirdParty = INDSleThirdParty.Properties.NullText
            End If
            .CostCenterId = INDSLeCostCenterConcept.EditValue
            If INDSLeCostCenterConcept.Text IsNot String.Empty Then
                .CodeNameCostCenter = INDSLeCostCenterConcept.Text
            Else
                .CodeNameCostCenter = INDSLeCostCenterConcept.Properties.NullText
            End If
            .Observations = INDMeObservation.Text
            If INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Nature = INDGLeNatureConcept.EditValue
                .Value = BaseValue
                .BaseValue = BaseValue
            Else
                .Nature = INDGleNatureRetention.EditValue
                .Value = INDTxtRetentionValue.EditValue
                .RetentionConceptId = INDSLeRetentionConcept.EditValue
                If INDSLeRetentionConcept.Text IsNot String.Empty Then
                    .CodeNameRetentionConcept = INDSLeRetentionConcept.Text
                Else
                    .CodeNameRetentionConcept = INDSLeRetentionConcept.Properties.NullText
                End If
                .Percentage = INDSePercent.EditValue
                .BaseValue = RetentionBaseValue
                TotalConcept = INDTxtRetentionValue.EditValue
            End If
            If IdGeneralLedgerIVA.IsNotNull AndAlso IdGeneralLedgerIVA <> 0 Then
                .IdGeneralLedgerIVA = IdGeneralLedgerIVA
                .IvaRate = IvaRate
            End If
            .TotalConcept = TotalConcept
        End With
        If _listPortfolioNoteDetail Is Nothing Then
            _listPortfolioNoteDetail = New List(Of PortfolioNoteDetail)
        End If
        If _editModePopupConcepts = False Then
            If _listPortfolioNoteDetail.Count > 0 Then
                If INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                    Dim concept = _listPortfolioNoteDetail.Find(Function(x) x.PortfolioNoteConceptId = _portfolioNoteDetail.PortfolioNoteConceptId And x.CostCenterId = _portfolioNoteDetail.CostCenterId And x.ThirdPartyId = _portfolioNoteDetail.ThirdPartyId And x.IdGeneralLedgerIVA = _portfolioNoteDetail.Id And x.IvaRate = _portfolioNoteDetail.IvaRate And x.TotalConcept = _portfolioNoteDetail.TotalConcept)
                    If concept IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NoteConceptAdded", NAME_MODULE), _portfolioNoteDetail.CodeNameNoteConcept)
                        Exit Sub
                    End If
                Else
                    Dim concept = _listPortfolioNoteDetail.Find(Function(x) x.PortfolioNoteConceptId = _portfolioNoteDetail.PortfolioNoteConceptId AndAlso x.CostCenterId.Equals(_portfolioNoteDetail.CostCenterId) AndAlso x.MainAccountId.Equals(_portfolioNoteDetail.MainAccountId))
                    If concept IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NoteConceptCostCenterAndMainAcountAdded", NAME_MODULE), _portfolioNoteDetail.CodeNameNoteConcept, _portfolioNoteDetail.CodeNameCostCenter, _portfolioNoteDetail?.CodeNameMainAccount)
                        Exit Sub
                    End If
                End If
            End If
            _listPortfolioNoteDetail.Add(_portfolioNoteDetail)
        End If
        INDgcConcepts.DataSource = Nothing
        INDgcConcepts.DataSource = _listPortfolioNoteDetail
        AddConceptNoteDetail()
        getDebitCredit()
        CleanControlsPopupConcepts()
        viewConcept.OptionsView.ShowFooter = True
    End Sub

    ''' <summary>
    ''' Método para añadir los detalles de los conceptos al agregar conceptos
    ''' </summary>
    Private Sub AddConceptNoteDetail()
        'Detalle
        _portfolioNoteDetail.PortfolioNoteDetailChild.Clear()

        _portfolioNoteDetail.PortfolioNoteDetailChild.Add(New PortfolioNoteDetailChild With {
            .MainAccountCodeName = _portfolioNoteDetail.CodeNameMainAccount,
            .CostCenterCodeName = _portfolioNoteDetail.CodeNameCostCenter,
            .NatureName = IIf(_portfolioNoteDetail.Nature = 1, "Débito", "Crédito"),
            .ValueDetailConcept = _portfolioNoteDetail.Value
        })
        'Maneja Impuesto
        If _portfolioNoteConcept.HandleTax = True Then

            If _generalLedgerIVA?.ObjectEmbbeded?.IdAccountSale IsNot Nothing And _generalLedgerIVA?.ObjectEmbbeded?.IdAccountSale > 0 Then

                Using Model As New MAccountReceivable(Me.Tag)
                    _taxMainAccount = Model.GetMainAccountById(_generalLedgerIVA?.ObjectEmbbeded?.IdAccountSale)
                End Using
                _portfolioNoteDetail.PortfolioNoteDetailChild.Add(New PortfolioNoteDetailChild With {
            .MainAccountCodeName = _taxMainAccount.NumberName,
            .CostCenterCodeName = "",
            .NatureName = IIf(_portfolioNoteDetail.Nature = 1, "Débito", "Crédito"),
            .ValueDetailConcept = IvaRate
        })
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Error en parametrizacion de Impuestos Sobre Ventas "
                _listPortfolioNoteDetail.Remove(_portfolioNoteDetail)
            End If
        End If
    End Sub

    Private Sub INDgcConcepts_Click(sender As Object, e As EventArgs) Handles INDgcConcepts.Click
        If _listPortfolioNoteDetail IsNot Nothing AndAlso _listPortfolioNoteDetail.Count > 0 Then
            viewConcept.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcConcepts, True)
        End If
    End Sub

    Private Sub INDBtnAddDistribution_Click(sender As Object, e As EventArgs) Handles INDBtnAddDistribution.Click
        Dim errors As New StringBuilder
        If INDSleCustomerDistribution.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un cliente")
        End If
        If Me.ValueDistribution = 0 Then
            errors.AppendLine("El valor debe ser mayor a cero")
        End If
        If Me.ValueDistribution > _advanceValue Then
            errors.AppendLine("El valor es mayor al saldo del anticipo")
        End If
        If INDSleMainAccountDistribution.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una cuenta contable")
        End If
        If INDLciCostCenterDistribution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleCostCenterDistribution.EditValue Is Nothing Then
                errors.AppendLine("Debe seleccionar un centro de costo")
            End If
        End If
        If _listPortfolioNoteDistribution IsNot Nothing AndAlso _listPortfolioNoteDistribution.Count > 0 Then
            Dim customerAdded = Me._listPortfolioNoteDistribution.Find(Function(x) x.CustomerId = INDSleCustomerDistribution.EditValue)
            If customerAdded IsNot Nothing Then
                errors.AppendLine("El cliente ya se encuentra agregado")
            End If
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If
        _portfolioNoteDistribution = New PortfolioNoteDistribution
        With _portfolioNoteDistribution
            .CustomerId = INDSleCustomerDistribution.EditValue
            .Value = Me.ValueDistribution
            .NitNameCustomer = INDSleCustomerDistribution.Text
            .MainAccountId = INDSleMainAccountDistribution.EditValue
            .NumberNameMainAccount = INDSleMainAccountDistribution.Text
            .CostCenterId = INDSleCostCenterDistribution.EditValue
            .CodeNameCostCenter = INDSleCostCenterDistribution.Text
        End With

        If _listPortfolioNoteDistribution Is Nothing Then
            INDsleCurrency.ReadOnly = True
            _listPortfolioNoteDistribution = New List(Of PortfolioNoteDistribution)
        End If
        _listPortfolioNoteDistribution.Add(_portfolioNoteDistribution)
        INDGcAdvanceDistribution.DataSource = Nothing
        INDGcAdvanceDistribution.DataSource = _listPortfolioNoteDistribution
        _ctrAdvance.PrintInfo()
        CleanControlsDistribution()
        INDSleCustomerDistribution.Focus()

        INDgleNature.Enabled = False
        INDGleNoteType.Enabled = False
        INDsleCurrency.Enabled = False
        INDsleClient.Enabled = False
        INDsleAdvanceDistribution.Enabled = False
        INDSlePorfolioTransfer.Enabled = False
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _portfolioNoteAccountReceivableAdvance = DirectCast(viewBill.GetFocusedRow(), PortfolioNoteAccountReceivableAdvance)
            If _portfolioNoteAccountReceivableAdvance.Id > 0 Then
                If _listPortfolioNoteAccountReceivableAdvanceDelete Is Nothing Then
                    _listPortfolioNoteAccountReceivableAdvanceDelete = New List(Of PortfolioNoteAccountReceivableAdvance)
                End If
                _listPortfolioNoteAccountReceivableAdvanceDelete.Add(_portfolioNoteAccountReceivableAdvance)
            End If
            _listPortfolioNoteAccountReceivableAdvance.Remove(_portfolioNoteAccountReceivableAdvance)
            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = _listPortfolioNoteAccountReceivableAdvance
            _portfolioNoteAccountReceivableAdvance = Nothing
            getDebitCredit()
            If _listPortfolioNoteAccountReceivableAdvance.Count = 0 Then
                viewBill.OptionsView.ShowFooter = False
                IndigoGridControl1.RefreshGrid(INDgcBill)
                viewBill.OptionsFind.AlwaysVisible = False
                IndigoGridControl1.SetExportButton(INDgcBill, False)

                INDgleNature.Enabled = True
                INDGleNoteType.Enabled = True
                INDsleCurrency.Enabled = True
                INDsleClient.Enabled = True
                INDsleAdvanceDistribution.Enabled = True
                INDSlePorfolioTransfer.Enabled = True
            Else
                viewBill.OptionsFind.AlwaysVisible = True
                IndigoGridControl1.SetExportButton(INDgcBill, True)
            End If
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _portfolioNoteAccountReceivableAdvance = DirectCast(viewGridAdvance.GetFocusedRow(), PortfolioNoteAccountReceivableAdvance)
            If _portfolioNoteAccountReceivableAdvance.Id > 0 Then
                If _listPortfolioNoteAccountReceivableAdvanceDelete Is Nothing Then
                    _listPortfolioNoteAccountReceivableAdvanceDelete = New List(Of PortfolioNoteAccountReceivableAdvance)
                End If
                _listPortfolioNoteAccountReceivableAdvanceDelete.Add(_portfolioNoteAccountReceivableAdvance)
            End If
            _listPortfolioNoteAccountReceivableAdvance.Remove(_portfolioNoteAccountReceivableAdvance)
            INDgcAdvance.DataSource = Nothing
            INDgcAdvance.DataSource = _listPortfolioNoteAccountReceivableAdvance
            _portfolioNoteAccountReceivableAdvance = Nothing
            getDebitCredit()
            If _listPortfolioNoteAccountReceivableAdvance.Count = 0 Then
                viewGridAdvance.OptionsView.ShowFooter = False
                IndigoGridControl1.RefreshGrid(INDgcAdvance)
                viewGridAdvance.OptionsFind.AlwaysVisible = False
                IndigoGridControl1.SetExportButton(INDgcAdvance, False)

                INDgleNature.Enabled = True
                INDGleNoteType.Enabled = True
                INDsleCurrency.Enabled = True
                INDsleClient.Enabled = True
                INDsleAdvanceDistribution.Enabled = True
                INDSlePorfolioTransfer.Enabled = True
            Else
                viewGridAdvance.OptionsFind.AlwaysVisible = True
                IndigoGridControl1.SetExportButton(INDgcAdvance, True)
            End If
        End If
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        _portfolioNoteDetail = DirectCast(viewConcept.GetFocusedRow(), PortfolioNoteDetail)
        Select Case sender.Tag.ToString
            Case "Edit"
                _editModePopupConcepts = True
                LoadControlForEditConcept(_portfolioNoteDetail)
                INDpceConcept.Focus()
                INDpceConcept.ShowPopup()
                INDSleNoteConcept.Focus()
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If _portfolioNoteDetail.Id > 0 Then
                        If _listPortfolioNoteDetailDelete Is Nothing Then
                            _listPortfolioNoteDetailDelete = New List(Of PortfolioNoteDetail)
                        End If
                        _listPortfolioNoteDetailDelete.Add(_portfolioNoteDetail)
                    End If
                    _listPortfolioNoteDetail.Remove(_portfolioNoteDetail)
                    _portfolioNoteDetail = Nothing
                    INDgcConcepts.DataSource = Nothing
                    INDgcConcepts.DataSource = _listPortfolioNoteDetail
                    getDebitCredit()
                    If _listPortfolioNoteDetail.Count = 0 Then
                        viewConcept.OptionsView.ShowFooter = False
                        IndigoGridControl1.RefreshGrid(INDgcConcepts)
                        viewConcept.OptionsFind.AlwaysVisible = False
                        IndigoGridControl1.SetExportButton(INDgcConcepts, False)
                    Else
                        viewConcept.OptionsFind.AlwaysVisible = True
                        IndigoGridControl1.SetExportButton(INDgcConcepts, True)
                    End If
                End If
        End Select
    End Sub

    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._portfolioNoteDistribution = DirectCast(INDGvDistributionAdvance.GetFocusedRow, PortfolioNoteDistribution)
            If Me._portfolioNoteDistribution.Id > 0 Then
                If Me._listPortfolioNoteDistributionDelete Is Nothing Then
                    Me._listPortfolioNoteDistributionDelete = New List(Of PortfolioNoteDistribution)
                End If
                _listPortfolioNoteDistributionDelete.Add(Me._portfolioNoteDistribution)
            End If
            Me._listPortfolioNoteDistribution.Remove(Me._portfolioNoteDistribution)
            _advanceValue += Me._portfolioNoteDistribution.Value
            _flagDeleteItemDistribution = True
            _ctrAdvance.PrintInfo()
            _flagDeleteItemDistribution = False
            If Me._listPortfolioNoteDistribution.Count = 0 Then
                INDgleNature.Enabled = True
                INDGleNoteType.Enabled = True
                INDsleCurrency.Enabled = True
                INDsleClient.Enabled = True
                INDsleAdvanceDistribution.Enabled = True
                INDSlePorfolioTransfer.Enabled = True
            End If
            INDGcAdvanceDistribution.DataSource = Nothing
            INDGcAdvanceDistribution.DataSource = Me._listPortfolioNoteDistribution
        End If

    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If _portfolioNote.Status <> 1 OrElse NoteType = 6 Then
            Exit Sub
        End If

        If sender.Name = INDgcBill.Name OrElse sender.Name = INDgcAdvance.Name Then
            AsyncLoader(True)
            Using model As New MNotesDebitCreditPortfolio(MyTag)
                Dim result = Await model.SetCopyPasteOrImportFilePortfolioNote(Nothing, e.Rows, indigo.IndigoCompanyType, Me._operativeUnitId, New List(Of Object) From {NoteType, _originalValueThirdPartyId, Nature, Me.CurrencyId})

                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    AsyncLoader(False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                    If NoteType < 3 Then
                        SetBillsOrShare(result.ObjectEmbbeded)
                    Else
                        SetAdvance(result.ObjectEmbbeded)
                    End If

                    getDebitCredit()
                End If

                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using

            AsyncLoader(False)
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
                INDgleNature.Enabled = False
                INDGleNoteType.Enabled = False
                INDsleCurrency.Enabled = False
                INDsleClient.Enabled = False
                INDsleAdvanceDistribution.Enabled = False
                INDSlePorfolioTransfer.Enabled = False
                viewBill.OptionsView.ShowFooter = True
            End If
        End If
    End Sub

#End Region

#Region "ClickBack"

    ''' <summary>
    ''' CTRs the navigation1_ click back.
    ''' </summary>
    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        INDpcNoteData.Visible = True
        INDpcDocumentDetail.Visible = False
    End Sub

#End Region

#Region "Import Data"

    Private Async Sub INDBtnImportFileBillsOrShare_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileBillsOrShare.Click, INDBtnImportFileAdvance.Click
        If _portfolioNote.Status <> 1 OrElse NoteType = 6 Then
            Exit Sub
        End If

        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la ruta del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Using model As New MNotesDebitCreditPortfolio(MyTag)
                        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                        SetRow(1, rows.LastUsedIndex + 1)

                        Dim result = Await model.SetCopyPasteOrImportFilePortfolioNote(listRows.ToList(), Nothing, indigo.IndigoCompanyType, Me._operativeUnitId, New List(Of Object) From {NoteType, _originalValueThirdPartyId, Nature, Me.CurrencyId})

                        If NoteType < 3 Then
                            SetBillsOrShare(result.ObjectEmbbeded)
                        Else
                            SetAdvance(result.ObjectEmbbeded)
                        End If
                        'si establecindo documentos se genero algun error estos se agregan a los que ocurrieron generando el proceso de copiar y pegar
                        If listErrors.Count > 0 Then
                            result.MessageResult.AddRange(listErrors)
                        End If
                        getDebitCredit()
                        If result.MessageResult.Count > 0 Then
                            Using formulario As New FrmListErrors(result.MessageResult)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                        End If
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                    End Using
                    AsyncLoader(False)
                    If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
                        INDgleNature.Enabled = False
                        INDGleNoteType.Enabled = False
                        INDsleCurrency.Enabled = False
                        INDsleClient.Enabled = False
                        INDsleAdvanceDistribution.Enabled = False
                        INDSlePorfolioTransfer.Enabled = False
                        viewBill.OptionsView.ShowFooter = True
                    End If

                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  If rows.Item(x).SpreadsheetRowToList(1)?(0) Is Nothing Then
                                                      Exit Sub
                                                  End If
                                                  Select Case NoteType
                                                      Case 1
                                                          If indigo.IndigoCompanyType = 1 Then
                                                              listRows.Add(New ImportFileRow With {.IndexRow = x, .Row = rows.Item(x).SpreadsheetRowToList(4)})
                                                          Else
                                                              listRows.Add(New ImportFileRow With {.IndexRow = x, .Row = rows.Item(x).SpreadsheetRowToList(3)})
                                                          End If
                                                      Case 2
                                                          listRows.Add(New ImportFileRow With {.IndexRow = x, .Row = rows.Item(x).SpreadsheetRowToList(3)})
                                                      Case 3
                                                          listRows.Add(New ImportFileRow With {.IndexRow = x, .Row = rows.Item(x).SpreadsheetRowToList(2)})
                                                  End Select
                                              End SyncLock
                                          End Sub)
    End Sub

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

#End Region

#Region "MasterRowGet"
    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewBill_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles viewBill.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            If NoteType = 6 Then
                Dim portfolioNoteAccountReceivableAdvance = CType(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
                'Se oculta o se muestra las columnas de IVA
                ShowOrHideInvoiceDetailGridColumns(portfolioNoteAccountReceivableAdvance?.PortfolioNoteAccountReceivableDetail.ToList())
                e.ChildList = portfolioNoteAccountReceivableAdvance.PortfolioNoteAccountReceivableDetail
            End If
        End If
    End Sub

    Private Sub viewBill_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles viewBill.MasterRowGetRelationName
        e.RelationName = "viewInvoiceDetail"
    End Sub


    ''' <summary>
    ''' Agrega los detalles del concepto al subnivel del grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewConcept_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs) Handles viewConcept.MasterRowGetChildList
        Dim abr = _listPortfolioNoteDetail
        Dim accountReceivableConceptDetail = viewConcept.GetFocusedObject(Of PortfolioNoteDetail)
        If e.ChildList Is Nothing Then
            If _listPortfolioNoteDetail IsNot Nothing Then
                If accountReceivableConceptDetail.Id > 0 Then
                    viewConceptDetail.ShowLoadingPanel()
                    e.ChildList = _listPortfolioNoteDetail.ToList().Where(Function(d) d.Id = accountReceivableConceptDetail.Id).FirstOrDefault.PortfolioNoteDetailChild.ToList()
                    viewConceptDetail.HideLoadingPanel()
                Else
                    viewConceptDetail.ShowLoadingPanel()
                    Dim x = _listPortfolioNoteDetail.Where(Function(d) d.PortfolioNoteConceptId = accountReceivableConceptDetail.PortfolioNoteConceptId AndAlso d.MainAccountId = accountReceivableConceptDetail.MainAccountId AndAlso d.ThirdPartyId = accountReceivableConceptDetail.ThirdPartyId).FirstOrDefault()
                    e.ChildList = x.PortfolioNoteDetailChild
                    viewConceptDetail.HideLoadingPanel()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Relaciona la rejilla principal con el subnivel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewConcept_MasterRowGetRelationName(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs) Handles viewConcept.MasterRowGetRelationName
        e.RelationName = "AccountReceivableDetailed"
    End Sub

    ''' <summary>
    ''' Establece la relación que tiene la rejilla maestra con el detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewConcept_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles viewConcept.MasterRowGetRelationCount, viewBill.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

#End Region

#Region "CheckStateChanged"
    ''' <summary>
    ''' Invoice detail column selector event changes state
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceSelector_CheckStateChanged(sender As Object, e As EventArgs) Handles INDRiceSelector.CheckStateChanged
        Dim selector = TryCast(sender, CheckEdit)

        If selector?.EditValue Is Nothing Then
            Exit Sub
        End If

        Dim invoiceDetail = CType(CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).GetFocusedRow(), PortfolioNoteAccountReceivableDetail)
        Dim headerInvoice = TryCast(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
        Me.TotalAdjustmentInvoiceDetail(invoiceDetail, Nature, selector.EditValue)

        Dim totalAdjustmentHeader = headerInvoice.PortfolioNoteAccountReceivableDetail.Sum(Function(x) x.Value)
        headerInvoice.AdjusmentValue = Me.HeaderTotalAdjustmentValue(totalAdjustmentHeader, Me.Nature, headerInvoice.Balance, headerInvoice.Value)

        headerInvoice.FlagTotalNote = Not headerInvoice.PortfolioNoteAccountReceivableDetail.Any(Function(x) Not x.Selector)

        getDebitCredit()

        If CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).PostEditor() Then
            CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).UpdateCurrentRow()
        End If
    End Sub

    ''' <summary>
    ''' Event of the "Total note" selector of the invoice header column changes state
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRICETotalNote_CheckedChanged(sender As Object, e As EventArgs) Handles INDRICETotalNote.CheckedChanged
        Try

            viewBill.ExpandMasterRow(viewBill.FocusedRowHandle, "viewInvoiceDetail")

            CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).BeginDataUpdate()
            Dim selector = TryCast(sender, CheckEdit)

            If selector?.EditValue Is Nothing Then
                Exit Sub
            End If

            Dim gridChild = CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView)

            Dim headerInvoice = TryCast(viewBill.GetFocusedRow, PortfolioNoteAccountReceivableAdvance)
            Parallel.ForEach(headerInvoice.PortfolioNoteAccountReceivableDetail, Sub(item)
                                                                                     Me.TotalAdjustmentInvoiceDetail(item, Nature, selector.EditValue)
                                                                                 End Sub)
            Dim totalAdjustmentHeader = headerInvoice.PortfolioNoteAccountReceivableDetail.Sum(Function(x) x.Value)
            headerInvoice.AdjusmentValue = Me.HeaderTotalAdjustmentValue(totalAdjustmentHeader, Me.Nature, headerInvoice.Balance, headerInvoice.Value)
            getDebitCredit()
            viewBill.UpdateCurrentRow()
        Catch ex As Exception
            Throw ex
        Finally
            CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).RefreshData()
            CType(viewBill.GetDetailView(viewBill.FocusedRowHandle, 0), GridView).EndDataUpdate()
        End Try
    End Sub

#End Region

#Region "CustomRepository"

    ''' <summary>
    ''' This event allows custom text to be placed in the TaxRate column
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvInvoiceDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvInvoiceDetail.CustomColumnDisplayText
        If e.Column.FieldName = "TaxId" Then
            If e.ListSourceRowIndex >= 0 Then
                Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                e.DisplayText = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "TaxPercentage")?.ToString()
            End If
        End If
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Carga la lista a los gridloockupedit
    ''' </summary>
    Private Sub InitializeTuples()
        If _listNoteType Is Nothing Then
            INDGleNoteType.Properties.DataSource = ListNoteType
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)

        Dim ListActionsBillsAvance As New List(Of eAcciones)
        ListActionsBillsAvance.Add(eAcciones.Remove)

        IndigoGridView1.SetListAcction(viewBill, ListActionsBillsAvance)
        IndigoGridView2.SetListAcction(viewGridAdvance, ListActionsBillsAvance)
        IndigoGridView3.SetListAcction(viewConcept, ListActions)
        IndigoGridView4.SetListAcction(INDGvDistributionAdvance, ListActionsBillsAvance)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewBill.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewGridAdvance.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewConcept.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDistributionAdvance.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' metodo para establecer los debitos y creditos del documento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setDebitCredit() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(_debitValue, _creditValue)
    End Function

    ''' <summary>
    ''' obtener debitos y creditos
    ''' </summary>
    Private Sub getDebitCredit()
        _debitValue = 0
        _creditValue = 0
        If NoteType = 5 Then 'Reversion anticipo vs cxc
            Me.CalculateReverseTransfer()
        Else
            Me.CalculatePortfolioNoteAccountReceivableAdvance()
            Me.CalculatePortfolioNoteDetail()
        End If
        _ctrDebitCredit.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de sumar los debitos/ creditos segun corresponda la naturaleza
    ''' </summary>
    ''' <param name="sumAdjustment"></param>
    ''' <param name="Nature"></param>
    Private Sub SumAdjustmentByNature(sumAdjustment As Decimal, Nature As eNature)

        If Nature = eNature.Credit Then
            _creditValue += sumAdjustment
        Else
            _debitValue += sumAdjustment
        End If
    End Sub

    ''' <summary>
    ''' metodo que calcula los debitos y los creditos en el tipo de nota de reversion de un anticipo
    ''' </summary>
    Private Sub CalculateReverseTransfer()
        If _frmPorfolioTransfer IsNot Nothing Then
            _debitValue = _frmPorfolioTransfer.GetCredit()
            _creditValue = _frmPorfolioTransfer.GetDebit()

            Dim diff = _debitValue - _creditValue

            _debitValue -= If(diff < 0, diff, 0)
            _creditValue += If(diff > 0, diff, 0)
        End If
    End Sub

    ''' <summary>
    ''' metodo que calcula los debitos y creditos de las facturas
    ''' </summary>
    Private Sub CalculatePortfolioNoteAccountReceivableAdvance()
        If _listPortfolioNoteAccountReceivableAdvance?.Any() Then
            Dim sumAdjustment = _listPortfolioNoteAccountReceivableAdvance.Sum(Function(x) x.AdjusmentValue)
            Me.SumAdjustmentByNature(sumAdjustment, Nature)

            If Me.NoteType = 6 AndAlso _listPortfolioNoteAccountReceivableAdvance?.Any(Function(x) x.PortfolioNoteAccountReceivableDetail?.Any()) Then
                Dim AdjustmentDetails = _listPortfolioNoteAccountReceivableAdvance.SelectMany(Function(x) x.PortfolioNoteAccountReceivableDetail).Sum(Function(d) d.Value)
                Me.SumAdjustmentByNature(AdjustmentDetails, If(Nature = eNature.Debit, eNature.Credit, eNature.Debit))
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo que calcula los debitos y creditos de los conceptos
    ''' </summary>
    Private Sub CalculatePortfolioNoteDetail()
        If _listPortfolioNoteDetail?.Any() Then
            Me.SumAdjustmentByNature(_listPortfolioNoteDetail.Where(Function(x) x.Nature = 1).Sum(Function(y) If(y.TotalConcept Is Nothing, y.Value, y.TotalConcept.Value)), eNature.Debit)
            Me.SumAdjustmentByNature(_listPortfolioNoteDetail.Where(Function(x) x.Nature = 2).Sum(Function(y) If(y.TotalConcept Is Nothing, y.Value, y.TotalConcept.Value)), eNature.Credit)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Function getAdvance() As Tuple(Of String, String)
        Dim _distributionValue As Decimal = 0
        If _flagDeleteItemDistribution = False Then
            If _listPortfolioNoteDistribution IsNot Nothing AndAlso _listPortfolioNoteDistribution.Count > 0 Then
                _distributionValue = _listPortfolioNoteDistribution.Sum(Function(x) x.Value)

                'se valida si el estado del registro es solo registrado (1) se toma en cuenta si se esta cargando la informacion 
                'o Si solo se esta creando uno nuevo para asi dependiendo se muestra el valor del avance
                If _statusData = 1 Then
                    If _loadControl Then
                        'si se esta cargando el registro se resta el valor de las distribuciones al balance general del anticipo
                        _advanceValue -= _distributionValue
                    Else
                        'si se estan agregando mas distribuciones se va restando al balance que se tiene
                        _advanceValue -= _listPortfolioNoteDistribution.ElementAt(_listPortfolioNoteDistribution.Count - 1).Value
                    End If
                End If
            End If
        Else
            _distributionValue = _listPortfolioNoteDistribution.Sum(Function(x) x.Value)
        End If
        Return New Tuple(Of String, String)(_advanceValue.ToString(), _distributionValue.ToString())
    End Function

    ''' <summary>
    ''' Funcion que actualiza los controles de acuerdo con el tipo de nota seleccionada
    ''' </summary>
    Private Sub ChangeNoteType()
        INDgleNature.Properties.ReadOnly = ({4, 5}.Contains(NoteType))
        INDlyItemAdjustment.HideControl(NoteType = 6)

        INDlygBills.Visibility = If({1, 2, 6}.Contains(NoteType), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlygAdvance.Visibility = If(NoteType = 3, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlygConcepts.Visibility = If({1, 2, 3, 6}.Contains(NoteType), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLcgAdvanceDistribution.Visibility = If(NoteType = 4, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        viewBill.OptionsView.ShowFooter = ({1, 2, 6}.Contains(NoteType))
        viewGridAdvance.OptionsView.ShowFooter = (NoteType = 3)
        viewConcept.OptionsView.ShowFooter = ({1, 2, 3, 6}.Contains(NoteType))

        INDLciBillsAccount.Visibility = If({1, 6}.Contains(NoteType), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlciBillsShare.Visibility = If(NoteType = 2, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        INDlyItemClient.HideControl(NoteType = 5)
        INDLciAdvanceDistribution.HideControl(Not (NoteType = 4))
        INDLciPorfolioTransfer.HideControl(Not (NoteType = 5))
        INDLciViewPortfolioTransfer.HideControl(Not (NoteType = 5))
        INDGcViewBillTotalNote.HideControl(Not (NoteType = 6), 6)

        AdditionalControlPanel.Controls.Clear()
        If NoteType = 4 Then
            _ctrAdvance = New CtrAdvanceDistribution()
            _ctrAdvance.CodeISO4217 = Me.CurrencyAbbreviation
            _ctrAdvance.SetInfoFunction(AddressOf getAdvance)
            _ctrAdvance.PrintInfo()
            _ctrAdvance.Dock = DockStyle.Fill
            AdditionalControlPanel.Controls.Add(_ctrAdvance)
        Else
            _ctrDebitCredit = New CtrDebitCredit()
            _ctrDebitCredit.CodeISO4217 = Me.CurrencyAbbreviation
            _ctrDebitCredit.SetDebitAndCredit(AddressOf setDebitCredit)
            _ctrDebitCredit.RefreshDebitCredit()
            _ctrDebitCredit.Dock = DockStyle.Fill
            AdditionalControlPanel.Controls.Add(_ctrDebitCredit)
        End If
    End Sub

    ''' <summary>
    ''' Validates the fields.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If Not ValidateControls() Then
            Return False
        End If

        Dim errors As New StringBuilder

        'validations for specific Note Type
        Select Case Me.NoteType
            Case 5
                If PortfolioTransferId Is Nothing Then
                    errors.AppendLine("Se debe seleccionar un Anticipo vs CxC")
                End If
            Case 6
                Dim errorDetail = ValidateDetails()
                If errorDetail.Length > 0 Then
                    errors.AppendLine(errorDetail)
                End If
            Case 4
                If AdvanceDistributionId Is Nothing Then
                    errors.AppendLine("Se debe seleccionar un Anticipo a distribuir")
                End If

                If INDGvDistributionAdvance.RowCount = 0 Then
                    errors.AppendLine("Se debe agregar mínimo una distribución")
                End If
            Case 3
                If viewGridAdvance.RowCount = 0 Then
                    errors.AppendLine("Se debe agregar mínimo un anticipo")
                End If
            Case 2
                Dim errorShared = ValidateShareds()
                If errorShared.Length > 0 Then
                    errors.AppendLine(errorShared)
                End If
        End Select

        'validation to note diferents to 5,4,6
        If Not {5, 4, 6}.ToList().Contains(NoteType) AndAlso viewConcept.RowCount = 0 Then
            errors.AppendLine("Se debe agregar mínimo un concepto")
        End If

        'validation to note diferents to 5,4,3
        If Not {5, 4, 3}.ToList().Contains(NoteType) AndAlso viewBill.RowCount = 0 Then
            errors.AppendLine("Se debe agregar mínimo una factura")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' metodo para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _portfolioNote
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .NoteDate = NoteDate
            .CustomerId = IIf(CustomerId IsNot Nothing AndAlso CustomerId = 0, Nothing, CustomerId)
            .Observations = Observations
            .Nature = Nature
            .NoteType = NoteType
            .PortfolioAdvanceId = Me.AdvanceDistributionId
            .OperatingUnitId = _operativeUnitId
            .PortfolioTransferId = Me.PortfolioTransferId
            .CurrencyId = Me.CurrencyId
            .PortfolioNoteDistribution.Clear()
            .PortfolioNoteAccountReceivableAdvance.Clear()
            .PortfolioNoteDetail.Clear()

            If NoteType = 4 Then
                If Me._listPortfolioNoteDistribution IsNot Nothing AndAlso Me._listPortfolioNoteDistribution.Count > 0 Then
                    For Each item In Me._listPortfolioNoteDistribution
                        .PortfolioNoteDistribution.Add(item)
                    Next
                End If
                If Me._listPortfolioNoteDistributionDelete IsNot Nothing AndAlso Me._listPortfolioNoteDistributionDelete.Count > 0 Then
                    For Each item In Me._listPortfolioNoteDistributionDelete
                        .PortfolioNoteDistribution.Add(item.MarkAsDeleted())
                    Next
                End If
            Else
                If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Any() Then
                    For Each item In _listPortfolioNoteAccountReceivableAdvance
                        .PortfolioNoteAccountReceivableAdvance.Add(item)

                        If item.PortfolioNoteAccountReceivableDetail IsNot Nothing AndAlso item.PortfolioNoteAccountReceivableDetail.Any Then
                            Dim removeDetails = New List(Of PortfolioNoteAccountReceivableDetail)
                            For Each detail In item.PortfolioNoteAccountReceivableDetail
                                If Not detail.Value > 0 Then
                                    If detail.Id > 0 Then
                                        detail.MarkAsDeleted()
                                    Else
                                        removeDetails.Add(detail)
                                    End If
                                End If
                            Next
                            For Each detail In removeDetails
                                item.PortfolioNoteAccountReceivableDetail.Remove(detail)
                            Next
                        End If
                    Next
                End If

                If _listPortfolioNoteAccountReceivableAdvanceDelete IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvanceDelete.Count > 0 Then
                    For Each item As PortfolioNoteAccountReceivableAdvance In _listPortfolioNoteAccountReceivableAdvanceDelete
                        .PortfolioNoteAccountReceivableAdvance.Add(item.MarkAsDeleted())
                    Next
                End If

                If _listPortfolioNoteDetail IsNot Nothing AndAlso _listPortfolioNoteDetail.Count > 0 Then
                    For Each item In _listPortfolioNoteDetail
                        .PortfolioNoteDetail.Add(item)
                    Next
                End If

                If _listPortfolioNoteDetailDelete IsNot Nothing AndAlso _listPortfolioNoteDetailDelete.Count > 0 Then
                    For Each item As PortfolioNoteDetail In _listPortfolioNoteDetailDelete
                        .PortfolioNoteDetail.Add(item.MarkAsDeleted())
                    Next
                End If
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyNotes.BeginUpdate()

        Me._doc = Nothing
        DeleteBlockedRecord()
        ReadOnlyControls(False, INDlyNotes)
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        INDBteCode.Text = String.Empty
        INDdteDate.EditValue = GetDateServer()
        INDGleNoteType.EditValue = 1
        INDgleNature.EditValue = 2
        INDsleClient.EditValue = Nothing
        INDsleClient.Properties.NullText = String.Empty
        Me.AdvanceDistributionId = Nothing
        INDsleAdvanceDistribution.Properties.NullText = String.Empty
        INDSlePorfolioTransfer.EditValue = Nothing
        INDSlePorfolioTransfer.Properties.NullText = ""
        INDmemoComments.Text = String.Empty
        Me.CurrencyDataSourceXpo = Nothing
        Me.CurrencyAbbreviation = Me.indigo.CurrencyISO4217
        Me.CurrencyId = Me.indigo.OfficialCurrencyId
        Me.INDsleCurrency.Properties.NullText = Me.indigo.CurrencyISO4217

        INDgcBill.DataSource = Nothing
        viewBill.OptionsView.ShowFooter = True
        IndigoGridControl1.RefreshGrid(INDgcBill)

        INDgcAdvance.DataSource = Nothing
        viewGridAdvance.OptionsView.ShowFooter = False
        IndigoGridControl1.RefreshGrid(INDgcAdvance)

        INDgcConcepts.DataSource = Nothing
        viewConcept.OptionsView.ShowFooter = True
        IndigoGridControl1.RefreshGrid(INDgcConcepts)

        INDGcAdvanceDistribution.DataSource = Nothing
        INDGvDistributionAdvance.OptionsView.ShowFooter = False
        IndigoGridControl1.RefreshGrid(INDGcAdvanceDistribution)

        _listPortfolioNoteAccountReceivableAdvance = Nothing
        _listPortfolioNoteAccountReceivableAdvanceDelete = Nothing
        _listPortfolioNoteDetail = Nothing
        _listPortfolioNoteDetailDelete = Nothing
        _listPortfolioNoteDistribution = Nothing
        _listPortfolioNoteDistributionDelete = Nothing
        _statusData = 1
        CustomerXPO = Nothing
        _frmPorfolioTransfer = Nothing

        INDgleNature.Properties.ReadOnly = False
        INDColAccount.Caption = "Cuenta Contable"
        INDColAccount.FieldName = "CodeNameMainAccount"

        getDebitCredit()
        If _ctrAdvance IsNot Nothing Then
            _ctrAdvance.PrintInfo()
        End If

        _advanceValue = 0
        _originalValueThirdPartyId = Nothing
        _originalValueThirdPartyName = Nothing
        _stateOpenPopUpPorfolioTransfer = False

        CleanControlsBills()
        CleanControlsAdvance()
        CleanControlsPopupConcepts()
        INDLciCurrency.Enabled = True
        ActionsOnControls = False
        AllowControlEditingInInvoiceDetail(True)
        INDcolDetails.Visible = False
        INDGleNoteType.Properties.DataSource = Nothing
        _listNoteType = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        InitializeTuples()
        INDlyNotes.EndUpdate()
    End Sub

    ''' <summary>
    ''' this function allow or disable editing column value in note type detail
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub AllowControlEditingInInvoiceDetail(value As Boolean)
        INDGcViewBillTotalNote.OptionsColumn.AllowEdit = value
        INDGcDetails_TotalAdjustment.OptionsColumn.AllowEdit = value
        INDGcDetails_TaxRate.OptionsColumn.AllowEdit = value
        INDGcDetails_AdjustmentBase.OptionsColumn.AllowEdit = value
        INDGcDetails_Selector.OptionsColumn.AllowEdit = value
    End Sub

    ''' <summary>
    ''' metodo para limpiar el popup de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsBills()
        INDSleBillsShare.EditValue = Nothing
        Me.AccountReceivableAccountingId = Nothing
        Me.PopUpValue = 0
        Me.PopUpBalance = 0
        AdjustmentValue = 0
        BillsXPO = Nothing
        _invoiceDocumentType = 0
        _isOpeningBalance = False
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del popup de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdvance()
        INDsleAdvance.EditValue = Nothing
        INDtxtValueAdvance.EditValue = 0
        INDtxtBalanceAdvance.EditValue = 0
        AdjustmentAdvance = 0
        AdvanceXPO = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del popup de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupConcepts()
        INDSleNoteConcept.EditValue = Nothing
        INDSleNoteConcept.Properties.NullText = String.Empty
        INDSleNoteConcept.Properties.ReadOnly = False
        INDSleNoteConcept.Properties.Buttons(0).Enabled = True
        INDSleMainAccount.EditValue = Nothing
        INDSleMainAccount.Properties.NullText = String.Empty
        INDSLeCostCenterConcept.EditValue = Nothing
        INDSLeCostCenterConcept.Properties.NullText = String.Empty
        INDSleThirdParty.EditValue = _originalValueThirdPartyId
        INDSleThirdParty.Properties.NullText = _originalValueThirdPartyName
        INDMeObservation.Text = String.Empty
        BaseValue = 0
        IdGeneralLedgerIVA = Nothing
        IvaRate = Nothing
        TotalConcept = Nothing
        _percentage = Nothing
        INDSLeRetentionConcept.EditValue = Nothing
        INDSLeRetentionConcept.Properties.NullText = String.Empty
        INDSLeRetentionConcept.Properties.ReadOnly = False
        INDSLeRetentionConcept.Properties.Buttons(0).Enabled = True
        INDSePercent.EditValue = 0
        If Nature = 1 Then
            INDGLeNatureConcept.EditValue = 2
            INDGleNatureRetention.EditValue = 2
        Else
            INDGLeNatureConcept.EditValue = 1
            INDGleNatureRetention.EditValue = 1
        End If
        RetentionBaseValue = 0
        INDTxtRetentionValue.EditValue = 0
        INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _portfolioNoteDetail = Nothing
        _editModePopupConcepts = False
        INDSleNoteConcept.Focus()

        INDLciRateIva.HideLayout()
        INDlyItemIvaValue.HideLayout()
        INDlyTotalConcept.HideLayout()
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles de la distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsDistribution()
        INDSleCustomerDistribution.EditValue = Nothing
        INDSleCostCenterDistribution.EditValue = Nothing
        INDLciCostCenterDistribution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleMainAccountDistribution.EditValue = Nothing
        INDSleMainAccountDistribution.EditValue = _mainAccountAdvanceDistribution
        Me.ValueDistribution = 0
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewPortfolioNote() As Task
        _portfolioNote = New PortfolioNote() With {.Status = 1}
        _dictionaryAccountReceivableShares = New Dictionary(Of String, List(Of PortfolioAccountReceivableShareXpo))

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._currentSequenceId = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._operativeUnitId).Id
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
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
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
    ''' metodo para cargar un registro
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MNotesDebitCreditPortfolio(CStr(Me.Tag))
                    AsyncLoader(True)
                    _loadControl = True
                    _portfolioNote = Await Model.GetPortfolioNoteByCode(INDBteCode.Text.Trim)
                    INDlyNotes.BeginUpdate()
                    If _portfolioNote IsNot Nothing AndAlso _portfolioNote.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        _dictionaryAccountReceivableShares = New Dictionary(Of String, List(Of PortfolioAccountReceivableShareXpo))
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_portfolioNote.Id))
                            With _portfolioNote
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
                                _statusData = .Status
                                Code = .Code
                                NoteDate = .NoteDate
                                NoteType = .NoteType
                                Nature = .Nature
                                CustomerId = .CustomerId
                                INDsleClient.Properties.NullText = .CodeNameCustomer
                                If .NoteType = 4 Then
                                    _listPortfolioNoteDistribution = Model.GetPortfolioNoteDistributionByPortfolioNoteId(_portfolioNote.Id)
                                    INDGcAdvanceDistribution.DataSource = _listPortfolioNoteDistribution
                                Else
                                    _listPortfolioNoteAccountReceivableAdvance = Model.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(_portfolioNote.Id)
                                    If .NoteType = 6 OrElse .EntityName = GLOSAS_MODULE Then
                                        For Each portfolioNoteAccountReceivableAdvance In _listPortfolioNoteAccountReceivableAdvance
                                            Await Me.GetInvoiceDetailsByAccountReceivable(portfolioNoteAccountReceivableAdvance)
                                        Next
                                    End If

                                    INDgcBill.DataSource = If(NoteType = 3, Nothing, _listPortfolioNoteAccountReceivableAdvance)
                                    INDgcAdvance.DataSource = If(NoteType = 3, _listPortfolioNoteAccountReceivableAdvance, Nothing)
                                    viewBill.OptionsView.ShowFooter = True
                                    IndigoGridControl1.SetExportButton(INDgcBill, True)

                                    _listPortfolioNoteDetail = Model.GetPortfolioNoteDetailByIdPortfolioNote(_portfolioNote.Id)

                                    For Each detail In _listPortfolioNoteDetail
                                        If detail.TotalConcept Is Nothing Then
                                            detail.TotalConcept = detail.Value
                                        End If
                                    Next

                                    INDgcConcepts.DataSource = _listPortfolioNoteDetail
                                End If

                                Me.CurrencyAbbreviation = .Currency?.Abbreviation
                                Me.INDsleCurrency.Properties.NullText = Me.CurrencyAbbreviation
                                Me.CurrencyId = .CurrencyId

                                Me.AdvanceDistributionId = .PortfolioAdvanceId
                                INDsleAdvanceDistribution.Properties.NullText = .CodeNameAdvanceDistribution
                                INDSlePorfolioTransfer.EditValue = .PortfolioTransferId
                                INDSlePorfolioTransfer.Properties.NullText = .PortfolioTransferCodeNameCustomer
                                Observations = .Observations
                                BarraBotones.StatusRecord = .Status.ToString()
                            End With

                            INDsleCurrency.ReadOnly = True
                            getDebitCredit()
                            ActionsOnControls = True
                            INDBteCode.Enabled = False
                            INDgleNature.Enabled = False
                            INDGleNoteType.Enabled = False
                            INDsleClient.Enabled = False
                            INDsleAdvanceDistribution.Enabled = False
                            INDSlePorfolioTransfer.Enabled = False

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._portfolioNote.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _portfolioNote.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_portfolioNote.Id, Me.Tag.ToString(), Nothing, GetType(PortfolioNote).Name)
                            If _portfolioNote.Status = 2 OrElse _portfolioNote.Status = 3 Then 'estado confirmado
                                Me.AllowControlEditingInInvoiceDetail(False)
                                ReadOnlyControls(True, INDlyNotes)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                INDgleNature.Properties.ReadOnly = True
                                INDSmbViewPortfolioTransfer.Enabled = True
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _portfolioNote.Id, 0, _portfolioNote.Id)
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPortfolioNote()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", "Portfolio")
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlyNotes.EndUpdate()
                    _loadControl = False
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Generar el bloqueo del registro
    ''' </summary>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._portfolioNote.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._portfolioNote.Id}
                Dim operation = Await model.SaveBlockRecord(_record)
                _record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                _record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Generar documento de vituel
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content As String = String.Empty
        Select Case _portfolioNote.NoteType
            Case 1
                Dim bills = String.Join("-", (From a In _portfolioNote.PortfolioNoteAccountReceivableAdvance Select a.InvoiceNumber).ToList.Distinct.ToList())
                Dim accounts = String.Join("-", (From e In _portfolioNote.PortfolioNoteAccountReceivableAdvance Select e.CodeNameMainAccount).ToList.Distinct.ToList())
                content = String.Format(ResourceManager.GetString("FrmNotesDebitCreditPortfolio_IndexContent_TotalBills", NAME_MODULE), _portfolioNote.Code, INDgleNature.Text, INDGleNoteType.Text, bills, accounts)
            Case 2
                Dim bills = String.Join("-", (From a In _portfolioNote.PortfolioNoteAccountReceivableAdvance Select a.InvoiceNumber).ToList.Distinct.ToList())
                content = String.Format(ResourceManager.GetString("FrmNotesDebitCreditPortfolio_IndexContent_ShareBills", NAME_MODULE), _portfolioNote.Code, INDgleNature.Text, INDGleNoteType.Text, bills)
            Case 3
                Dim advance = String.Join("-", (From a In _portfolioNote.PortfolioNoteAccountReceivableAdvance Select a.CodeAdvance).ToList())
                content = String.Format(ResourceManager.GetString("FrmNotesDebitCreditPortfolio_IndexContent_Advances", NAME_MODULE), _portfolioNote.Code, INDgleNature.Text, INDGleNoteType.Text, advance)
            Case 5
                content = String.Format(ResourceManager.GetString("FrmNotesDebitCreditPortfolio_IndexContent_PortfolioTransfer", NAME_MODULE), _portfolioNote.Code, INDgleNature.Text, INDGleNoteType.Text, _portfolioNote.PortfolioTransferCode)
        End Select

        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._portfolioNote.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._portfolioNote.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._portfolioNote.Code)
            Return Me._doc
        End If
    End Function

    Dim listErrors As List(Of String)

    Private Sub SetBillsOrShare(result As List(Of PortfolioNoteAccountReceivableAdvance))
        listErrors = New List(Of String)
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            For Each item In result
                If NoteType = 1 Then 'factura total
                    Dim billAdded = _listPortfolioNoteAccountReceivableAdvance.Find(Function(x) x.InvoiceNumber = item.InvoiceNumber And x.MainAccountId = item.MainAccountId)
                    If billAdded IsNot Nothing Then
                        listErrors.Add("La factura " & item.InvoiceNumber & " ya esta agregada con la cuenta contable " & item.CodeNameMainAccount)
                        Continue For
                    End If
                Else 'factura cuota
                    Dim shareAdded = _listPortfolioNoteAccountReceivableAdvance.Find(Function(x) x.InvoiceNumber = item.InvoiceNumber And x.NumberShare = item.NumberShare)
                    If shareAdded IsNot Nothing Then
                        listErrors.Add("La factura " & item.InvoiceNumber & " ya esta agregada con la cuota " & item.NumberShare)
                        Continue For
                    End If
                End If
                _listPortfolioNoteAccountReceivableAdvance.Add(item)
            Next
        Else
            _listPortfolioNoteAccountReceivableAdvance = result
        End If

        If NoteType = 1 Then
            INDColAccount.Caption = "Cuenta Contable"
            INDColAccount.FieldName = "CodeNameMainAccount"
            INDColAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
        Else
            INDColAccount.Caption = "Cuota"
            INDColAccount.FieldName = "NumberShare"
            INDColAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        End If

        INDgcBill.DataSource = Nothing
        INDgcBill.DataSource = _listPortfolioNoteAccountReceivableAdvance
    End Sub

    Private Sub SetAdvance(result As List(Of PortfolioNoteAccountReceivableAdvance))
        listErrors = New List(Of String)
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            For Each item In result
                Dim advanceAdded = _listPortfolioNoteAccountReceivableAdvance.Find(Function(x) x.CodeAdvance = item.CodeAdvance)
                If advanceAdded IsNot Nothing Then
                    listErrors.Add("El anticipo " & item.CodeAdvance & " ya esta agregado")
                    Continue For
                End If
                _listPortfolioNoteAccountReceivableAdvance.Add(item)
            Next
        Else
            _listPortfolioNoteAccountReceivableAdvance = result
        End If
        INDgcAdvance.DataSource = Nothing
        INDgcAdvance.DataSource = _listPortfolioNoteAccountReceivableAdvance
    End Sub

    Private Function IsValidAdjustment(adjusmentValue As Decimal, valueBill As Decimal, natureValidate As Integer, balanceBill As Decimal) As Boolean

        If NoteType = 3 Then ''Si es anticipo
            If Nature = 1 Then ''Si es Debito entonces resta el valor
                If (adjusmentValue > balanceBill) Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al valor del saldo del anticipo"
                    Return False
                End If
            End If
            Return True
        End If

        If Nature = natureValidate Then 'Si la naturaleza de la cuenta es igual a la naturaleza de la nota
            Dim value = valueBill
            Dim balance = balanceBill
            Dim isOpeningBalance = _isOpeningBalance

            If NoteType = 1 Then
                value = _valueBill
                balance = _balanceBill
                isOpeningBalance = False
            End If

            If Not (adjusmentValue <= (value - balance)) AndAlso _invoiceDocumentType <> 4 AndAlso Not isOpeningBalance Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al valor inicial de la factura"
                Return False
            End If
        Else 'Si la naturaleza de la cuenta es diferente a la naturaleza de la nota
            If Not (adjusmentValue <= balanceBill) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al valor del saldo de la factura"
                Return False
            End If
        End If
        If NoteType <> 6 And adjusmentValue <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AdjustmentValueVoid", NAME_MODULE)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' this method add the Invoice or Advance to gridview
    ''' </summary>
    ''' <param name="adjustmentValue"></param>
    ''' <param name="valueBill"></param>
    ''' <param name="natureValidate"></param>
    ''' <param name="balanceBill"></param>
    ''' <param name="conceptId"></param>
    ''' <returns></returns>
    Private Async Function AddBillOrAdvance(adjustmentValue As Decimal, valueBill As Decimal, natureValidate As Integer, balanceBill As Decimal, Optional conceptId As Integer? = Nothing) As Task
        Try
            AsyncLoader(True)
            If Not IsValidAdjustment(adjustmentValue, valueBill, natureValidate, balanceBill) Then
                Return
            End If

            If _listPortfolioNoteAccountReceivableAdvance Is Nothing Then
                _listPortfolioNoteAccountReceivableAdvance = New List(Of PortfolioNoteAccountReceivableAdvance)
            End If

            Me._portfolioNoteAccountReceivableAdvance.AdjusmentValue = adjustmentValue
            Me._portfolioNoteAccountReceivableAdvance.ConceptId = If(conceptId.HasValue AndAlso conceptId > 0, conceptId, Nothing)
            INDColValueBills.OptionsColumn.AllowEdit = True

            If IsDuplicateEntry(NoteType) Then
                Return
            End If

            If NoteType = 6 Then ' Pre-auditoría

                If Me.NoteDate.Value.Year > Me._portfolioNoteAccountReceivableAdvance?.AccountReceivable?.AccountReceivableDate.Year Then
                    Return
                End If

                Await GetInvoiceDetailsByAccountReceivable(Me._portfolioNoteAccountReceivableAdvance)
                If Me._portfolioNoteAccountReceivableAdvance.PortfolioNoteAccountReceivableDetail.Any() Then
                    INDColValueBills.OptionsColumn.AllowEdit = False
                    Mensaje(EeventViewerImages.Advertencia) = "Para realizar el ajuste de la factura debe abrir sus detalles."
                End If
            End If

            _listPortfolioNoteAccountReceivableAdvance.Add(_portfolioNoteAccountReceivableAdvance)
            getDebitCredit()
            UpdateDataSource(NoteType)
            DisableControls()

            viewBill.OptionsView.ShowFooter = True
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' this is the first function to managed the different type note to validate duplicate in gridview
    ''' </summary>
    ''' <param name="noteType"></param>
    ''' <returns></returns>
    Private Function IsDuplicateEntry(noteType As Integer) As Boolean
        Select Case noteType
            Case 1, 6 ' Factura total y Pre-auditoría
                Return CheckForDuplicate(Function(x) x.InvoiceNumber = _portfolioNoteAccountReceivableAdvance.InvoiceNumber And x.MainAccountId = _portfolioNoteAccountReceivableAdvance.MainAccountId, "AddedBillsMainAccount", noteType)
            Case 2 ' Factura cuotas
                Return CheckForDuplicate(Function(x) x.InvoiceNumber = _portfolioNoteAccountReceivableAdvance.InvoiceNumber And x.NumberShare = _portfolioNoteAccountReceivableAdvance.NumberShare, "ShareBillAdded", noteType)
            Case 3 ' Anticipo
                Return CheckForDuplicate(Function(x) x.CodeAdvance = _portfolioNoteAccountReceivableAdvance.CodeAdvance, "AddedAdvance", noteType)
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' This function checks if the element to be added to the gridview is already added
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="messageResourceKey"></param>
    ''' <param name="noteType"></param>
    ''' <returns></returns>
    Private Function CheckForDuplicate(predicate As Func(Of PortfolioNoteAccountReceivableAdvance, Boolean), messageResourceKey As String, noteType As Integer) As Boolean
        Dim duplicate = _listPortfolioNoteAccountReceivableAdvance.FirstOrDefault(predicate)
        If duplicate IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString(messageResourceKey, NAME_MODULE), If(noteType = 3, duplicate.CodeAdvance, duplicate.InvoiceNumber))
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' this function update the datasource in AddBillOrAdvance function
    ''' </summary>
    ''' <param name="noteType"></param>
    Private Sub UpdateDataSource(noteType As Integer)
        If noteType = 3 Then
            INDgcAdvance.DataSource = Nothing
            INDgcAdvance.DataSource = _listPortfolioNoteAccountReceivableAdvance
            CleanControlsAdvance()
        Else
            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = _listPortfolioNoteAccountReceivableAdvance
            CleanControlsBills()
        End If
    End Sub

    ''' <summary>
    ''' this function disable control related AddBillOrAdvance
    ''' </summary>
    Private Sub DisableControls()
        INDgleNature.Enabled = False
        INDGleNoteType.Enabled = False
        INDsleCurrency.Enabled = False
        INDsleClient.Enabled = False
        INDsleAdvanceDistribution.Enabled = False
        INDSlePorfolioTransfer.Enabled = False
    End Sub

    ''' <summary>
    ''' metodo para establcer los datos si se esta haciendo una factura o un anticipo
    ''' </summary>
    ''' <param name="share"></param>
    ''' <param name="account"></param>
    ''' <param name="advance"></param>
    ''' <remarks></remarks>
    Private Sub SetPortfolioNoteAccountReceivableAdvance(share As PortfolioAccountReceivableShareXpo, account As PortfolioAccountReceivableAccountingXpo, advance As Portfolio_PortfolioAdvance)
        _portfolioNoteAccountReceivableAdvance = New PortfolioNoteAccountReceivableAdvance
        If share IsNot Nothing Then
            With Me._portfolioNoteAccountReceivableAdvance
                .AccountReceivableId = share.AccountReceivableId.Id
                .AccountReceivableShareId = share.Id
                .InvoiceNumber = share.AccountReceivableId.InvoiceNumber
                .AccountReceivableAccountingId = share.AccountReceivableId.PortfolioAccountReceivableAccounting.ToList().ElementAt(0).Id
                .NumberShare = share.Number
                .Balance = share.Balance
                .Value = share.Value
                .Nature = share.AccountReceivableId.PortfolioAccountReceivableAccounting(0).MainAccountId.Nature
                INDColAccount.Caption = "Cuota"
                INDColAccount.FieldName = "NumberShare"
                INDColAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                SetDictionaryInvoiceShared(share.AccountReceivableId)
            End With
        ElseIf account IsNot Nothing Then
            With Me._portfolioNoteAccountReceivableAdvance
                .MainAccountId = account.MainAccountId.Id
                .AccountReceivableId = account.AccountReceivableId.Id
                .AccountReceivableShareId = account.AccountReceivableId.PortfolioAccountReceivableShare.ToList().ElementAt(0).Id
                .AccountReceivableAccountingId = account.Id
                .CodeNameMainAccount = account.MainAccountId.NumberName
                .Balance = account.Balance
                .Value = account.Value
                .InvoiceNumber = account.AccountReceivableId.InvoiceNumber
                .Nature = account.MainAccountId.Nature
                INDColAccount.Caption = "Cuenta Contable"
                INDColAccount.FieldName = "CodeNameMainAccount"
                INDColAccount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                Select Case account.MainAccountId.Id
                    Case account.AccountReceivableId.MainAccountWithoutFilingId
                        .PortfolioStatusName = "1-Sin Radicar"

                    Case account.AccountReceivableId.AccountRadicateId
                        .PortfolioStatusName = "3-Radicada Entidad"

                    Case account.AccountReceivableId.AccountObjectionRemediedId
                        .PortfolioStatusName = "4-Glosada sin Conciliar"

                    Case account.AccountReceivableId.AccountConciliationId
                        .PortfolioStatusName = "12-Glosada Conciliada"

                    Case account.AccountReceivableId.AccountHardCollectionId
                        .PortfolioStatusName = "15-Cuenta de Dificil Recaudo"

                    Case account.AccountReceivableId.AccountLegalCollectionId
                        .PortfolioStatusName = "16-Cobro Jurídico"
                    Case Else
                        .PortfolioStatusName = "N/A"

                End Select
            End With
        Else
            With Me._portfolioNoteAccountReceivableAdvance
                .PortfolioAdvanceId = advance.Id
                .Value = advance.Value
                .Balance = advance.Balance
                .CodeAdvance = advance.Code
                .Nature = advance.MainAccountId.Nature
            End With
        End If
    End Sub

    ''' <summary>
    ''' metodo para establecer la relacion de las cuotas con el detalle del recibo de caja y guardar en el diccionario
    ''' </summary>
    ''' <param name="invoice"></param>
    ''' <remarks></remarks>
    Private Sub SetDictionaryInvoiceShared(invoice As PortfolioAccountReceivableXpo)
        If Not _dictionaryAccountReceivableShares.ContainsKey(invoice.InvoiceNumber) Then
            _dictionaryAccountReceivableShares.Add(invoice.InvoiceNumber, invoice.PortfolioAccountReceivableShare.ToList)
        Else
            _dictionaryAccountReceivableShares(invoice.InvoiceNumber) = invoice.PortfolioAccountReceivableShare.ToList
        End If
    End Sub

    ''' <summary>
    ''' metodo para establecer en el diccionario las cuotas ya creadas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDictionaryWhitExistingShares()
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso _listPortfolioNoteAccountReceivableAdvance.Count > 0 Then
            Dim listInvoiceNumber = (From e In _listPortfolioNoteAccountReceivableAdvance
                                     Select e.InvoiceNumber).ToList.Distinct.ToList()
            Using modelBusqueda As New MBusqueda
                Dim result = CType(modelBusqueda.ConsultarEntidades(eDataSource.ListPortfolioAccountReceivableValidation, listInvoiceNumber), XPCollection)
                For Each objInvoice As PortfolioAccountReceivableXpo In result
                    SetDictionaryInvoiceShared(objInvoice)
                Next
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para validar las cutas de las facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateShareds() As String
        Dim listError As New StringBuilder()
        For Each accountSharedTmp In _listPortfolioNoteAccountReceivableAdvance
            If _dictionaryAccountReceivableShares.ContainsKey(accountSharedTmp.InvoiceNumber) Then
                Dim listShared = _dictionaryAccountReceivableShares(accountSharedTmp.InvoiceNumber)
                Dim index As Integer
                For index = accountSharedTmp.NumberShare - 1 To 1 Step -1
                    Dim query = _listPortfolioNoteAccountReceivableAdvance.Where(Function(y) y.NumberShare = index And y.InvoiceNumber = accountSharedTmp.InvoiceNumber)
                    If query.Count > 0 Then
                        Dim sharedComplexTmp = query.ToList.Item(0)
                        If sharedComplexTmp.AdjusmentValue <> sharedComplexTmp.Balance Then
                            listError.AppendLine(String.Format(ResourceManager.GetString("ShareWithBalance", "Treasury"), accountSharedTmp.NumberShare.ToString(), accountSharedTmp.InvoiceNumber, sharedComplexTmp.NumberShare.ToString()))
                            Continue For
                        End If
                    Else
                        Dim sharedXpoTmp = listShared.Where(Function(x) x.Number = index).ToList.Item(0)
                        If sharedXpoTmp.Balance <> 0 Then
                            listError.AppendLine(String.Format(ResourceManager.GetString("ShareWithBalance", "Treasury"), accountSharedTmp.NumberShare.ToString(), accountSharedTmp.InvoiceNumber, sharedXpoTmp.Number.ToString()))
                            Continue For
                        End If
                    End If
                Next
            End If
        Next
        Return listError.ToString()
    End Function

    ''' <summary>
    ''' metodo para validar los detalles de las facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDetails() As String
        Dim listError As New StringBuilder()
        If _listPortfolioNoteAccountReceivableAdvance IsNot Nothing Then
            For Each accountSharedTmp In _listPortfolioNoteAccountReceivableAdvance
                If accountSharedTmp.PortfolioNoteAccountReceivableDetail Is Nothing OrElse Not accountSharedTmp.PortfolioNoteAccountReceivableDetail.Any(Function(d) d.Value > 0) Then
                    listError.AppendLine(String.Format("Debe ajustar al menos un detalle de la factura {0}", accountSharedTmp.InvoiceNumber))
                End If
            Next
        End If
        Return listError.ToString()
    End Function

    ''' <summary>
    ''' metodo para validar los controles del popup de conceptos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsNoteDetail() As String
        Dim listError As New StringBuilder
        If INDSleNoteConcept.EditValue = Nothing Then
            listError.AppendLine(INDLciNoteConcept.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSleMainAccount.EditValue = Nothing Then
            listError.AppendLine(INDLciMainAccount.Text + ResourceManager.GetString("Empty"))
        End If
        If INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSLeCostCenterConcept.EditValue = Nothing Then
                listError.AppendLine(INDLciCostCenterConcept.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDSleThirdParty.EditValue = Nothing Then
            listError.AppendLine(INDLciThirdParty.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If BaseValue = 0 Then
                listError.AppendLine(INDLciBaseValue.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        Else
            If INDSLeRetentionConcept.EditValue = Nothing Then
                listError.AppendLine(INDLciRetentionConcept.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
            If INDSePercent.EditValue = 0 Then
                listError.AppendLine(INDLciPercent.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If

        If listError.Length = 0 Then
            If NoteType = 6 Then
                If _listPortfolioNoteAccountReceivableAdvance Is Nothing Then
                    listError.AppendLine("Debe agregar al menos un detalle para poder agregar conceptos")
                    'Else
                    '    Dim value = _listPortfolioNoteAccountReceivableAdvance.SelectMany(Function(a) a.PortfolioNoteAccountReceivableDetail).Where(Function(d) d.MainAccountId = INDSleMainAccount.EditValue AndAlso d.CostCenterId.Equals(INDSLeCostCenterConcept.EditValue)).Sum(Function(d) d.Value)
                    '    If INDTxtBaseValue.EditValue > value Then
                    '        listError.AppendLine(String.Format("El valor del concepto ({0}) no puede superar el valor de los detalles para la cuenta contable y el centro de costo ({1})", String.Format("{0:c2}", INDTxtBaseValue.EditValue), String.Format("{0:c2}", value)))
                    '    End If
                End If
            End If
        End If

        Return listError.ToString()
    End Function

    ''' <summary>
    ''' metodo para cargar los controles del popup de conceptos
    ''' </summary>
    ''' <param name="portfolioNoteDetailTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControlForEditConcept(portfolioNoteDetailTmp As PortfolioNoteDetail)
        INDSleNoteConcept.EditValue = portfolioNoteDetailTmp.PortfolioNoteConceptId
        INDSleNoteConcept.Properties.NullText = portfolioNoteDetailTmp.CodeNameNoteConcept
        INDSleNoteConcept.Properties.ReadOnly = True
        INDSleNoteConcept.Properties.Buttons(0).Enabled = False
        INDSleMainAccount.EditValue = portfolioNoteDetailTmp.MainAccountId
        INDSleMainAccount.Properties.NullText = portfolioNoteDetailTmp.CodeNameMainAccount
        INDSLeCostCenterConcept.EditValue = portfolioNoteDetailTmp.CostCenterId
        INDSLeCostCenterConcept.Properties.NullText = portfolioNoteDetailTmp.CodeNameCostCenter
        INDSleThirdParty.EditValue = portfolioNoteDetailTmp.ThirdPartyId
        INDSleThirdParty.Properties.NullText = portfolioNoteDetailTmp.CodeNameThirdParty
        INDMeObservation.Text = portfolioNoteDetailTmp.Observations
        If portfolioNoteDetailTmp.RetentionConceptId Is Nothing Then
            INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGLeNatureConcept.EditValue = portfolioNoteDetailTmp.Nature
            BaseValue = portfolioNoteDetailTmp.Value
            If IsNothing(portfolioNoteDetailTmp.IdGeneralLedgerIVA) Then
                INDLciRateIva.HideLayout()
                INDlyItemIvaValue.HideLayout()
                INDlyTotalConcept.HideLayout()
            Else
                INDLciRateIva.ShowLayout()
                INDlyItemIvaValue.ShowLayout()
                INDlyTotalConcept.ShowLayout()
                IdGeneralLedgerIVA = portfolioNoteDetailTmp.IdGeneralLedgerIVA
                IvaRate = portfolioNoteDetailTmp.IvaRate
                TotalConcept = portfolioNoteDetailTmp.TotalConcept
            End If
        Else
            INDlyValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSLeRetentionConcept.EditValue = portfolioNoteDetailTmp.RetentionConceptId
            INDSLeRetentionConcept.Properties.NullText = portfolioNoteDetailTmp.CodeNameRetentionConcept
            INDSLeRetentionConcept.Properties.ReadOnly = True
            INDSLeRetentionConcept.Properties.Buttons(0).Enabled = False
            INDSePercent.EditValue = portfolioNoteDetailTmp.Percentage
            INDGleNatureRetention.EditValue = portfolioNoteDetailTmp.Nature
            RetentionBaseValue = portfolioNoteDetailTmp.BaseValue
            INDTxtRetentionValue.EditValue = portfolioNoteDetailTmp.Value
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub LoadControlsPorfolioTransferForm()
        INDpcDocuments.Controls.Clear()
        INDlblNameVoucher.Text = "Cruce Anticipo vs CxC"
        AsyncLoader(True)
        If _frmPorfolioTransfer Is Nothing Then
            _frmPorfolioTransfer = New FrmPortfolioTransfers()
            _frmPorfolioTransfer.CallNote = True
            _frmPorfolioTransfer.TopLevel = False
            _frmPorfolioTransfer.Parent = INDpcDocuments
            _frmPorfolioTransfer.ToolBar.Dock = DockStyle.None
            _frmPorfolioTransfer.ViewModeEditHold = False
            _frmPorfolioTransfer.Dock = DockStyle.Fill
            _frmPorfolioTransfer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            AddHandler _frmPorfolioTransfer.LoadControlsFinish, AddressOf LoadControlsFinishPorfolioTransfer
            AddHandler _frmPorfolioTransfer.Shown, AddressOf PorfolioTransfer_Shown
            _frmPorfolioTransfer.Show()
        Else
            _frmPorfolioTransfer.Parent = INDpcDocuments
            _frmPorfolioTransfer.Deshacer()
            PorfolioTransfer_Shown(Nothing, Nothing)
        End If
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub PorfolioTransfer_Shown(sender As Object, e As EventArgs)
        If _portfolioNote.Status = 2 OrElse _portfolioNote.Status = 3 Then
            _frmPorfolioTransfer.Code = _portfolioNote.PortfolioTransferCode
            INDSlePorfolioTransfer.Properties.NullText = _portfolioNote.PortfolioTransferCodeNameCustomer
        Else
            If INDSlePorfolioTransfer.Text.Trim().Equals("") Then
                _frmPorfolioTransfer.Code = _portfolioNote.PortfolioTransferCode
                INDSlePorfolioTransfer.Properties.NullText = _portfolioNote.PortfolioTransferCodeNameCustomer
            Else
                _frmPorfolioTransfer.Code = INDSlePorfolioTransfer.Text.Trim().Split("-")(0)
            End If
        End If
        Await _frmPorfolioTransfer.LoadControls()
        INDLciViewPortfolioTransfer.HideControl(False)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Public Sub LoadControlsFinishPorfolioTransfer()
        'ctrTmp.Title = "Valor Total"
        'ctrTmp.PrintValue()
        getDebitCredit()
        If _frmPorfolioTransfer IsNot Nothing AndAlso (CustomerId Is Nothing Or CustomerId = 0) Then
            CustomerId = _frmPorfolioTransfer.GetCustomer
        End If
        If CustomerId Is Nothing OrElse CustomerId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El cliente no existe. Para continuar, por favor crea uno que corresponda al mismo tercero."
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Consulta las obligaciones presupuestales asociadas a una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function GetInvoiceDetailsByAccountReceivable(portfolioNoteAccountReceivableAdvance As PortfolioNoteAccountReceivableAdvance) As Task
        Using model As New MNotesDebitCreditPortfolio(Me.Tag)
            Dim viewinvoiceDetails = Await Task.Factory.StartNew(Function() As XPCollection(Of ViewInvoiceDetailsXpo)
                                                                     Dim noteId As Integer? = Nothing
                                                                     If Me._portfolioNote?.Id > 0 Then
                                                                         noteId = Me._portfolioNote.Id
                                                                     End If
                                                                     Return _presenter.GetInvoiceDetailsByAccountReceivable(portfolioNoteAccountReceivableAdvance.AccountReceivableId.Value, If(Me._portfolioNote.Status = 1, Nature, 0), noteId)
                                                                 End Function
                                                                      )
            If viewinvoiceDetails IsNot Nothing Then
                For Each viewinvoiceDetail In viewinvoiceDetails
                    Dim detail = portfolioNoteAccountReceivableAdvance.PortfolioNoteAccountReceivableDetail.FirstOrDefault(Function(d) d.EntityName = viewinvoiceDetail.EntityName AndAlso d.EntityId = viewinvoiceDetail.EntityId AndAlso (viewinvoiceDetail.noteId Is Nothing OrElse Me._portfolioNote.Id = viewinvoiceDetail.noteId))
                    If detail Is Nothing Then
                        If (Me._portfolioNote.Status <> 1) OrElse (viewinvoiceDetail.EntityName = NameOf(ServiceOrderDetailSurgical)) Then
                            Continue For
                        End If

                        detail = New PortfolioNoteAccountReceivableDetail() With
                        {
                            .EntityName = viewinvoiceDetail.EntityName,
                            .EntityId = viewinvoiceDetail.EntityId,
                            .MainAccountId = viewinvoiceDetail.MainAccountId,
                            .CostCenterId = viewinvoiceDetail.CostCenterId
                        }

                        portfolioNoteAccountReceivableAdvance.PortfolioNoteAccountReceivableDetail.Add(detail)
                    End If

                    With detail
                        .BillingGroupCodeName = viewinvoiceDetail.BillingGroupCodeName
                        .ServiceDate = viewinvoiceDetail.ServiceDate
                        .CodeCups = viewinvoiceDetail.CodeCups
                        .CodeName = viewinvoiceDetail.CodeName
                        .AlternativeCodeName = viewinvoiceDetail.AlternativeCodeName
                        .MainAccountNumberName = viewinvoiceDetail.MainAccountNumberName
                        .CostCenterCodeName = viewinvoiceDetail.CostCenterCodeName
                        .Quantity = viewinvoiceDetail.Quantity
                        .UnitSalesPrice = viewinvoiceDetail.UnitSalesPrice
                        .TotalSalesPrice = viewinvoiceDetail.TotalSalesPrice
                        .Balance = viewinvoiceDetail.Balance
                        .TaxValueName = viewinvoiceDetail.TaxValueName
                        .TaxPercentage = viewinvoiceDetail.TaxPercentage
                        .TaxId = viewinvoiceDetail.TaxId
                    End With
                Next
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo para cargar de informacion los combos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRateIva()
        Using msearch As New MBusqueda
            RateIvaXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListGeneralLedgerIva), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' funcion que se encarga de mostrar u ocultar las columnas de la rejilla concerciente a los impuesto (IVA)
    ''' </summary>
    ''' <param name="obj"></param>
    Private Sub ShowOrHideInvoiceDetailGridColumns(obj As List(Of PortfolioNoteAccountReceivableDetail))
        Dim flag = obj IsNot Nothing AndAlso obj?.Exists(Function(x) x.TaxPercentage IsNot Nothing AndAlso x.TaxPercentage > 0)
        INDGcDetails_TaxCodeName.HideControl(Not flag, INDGcDetails_TotalSalesPrice.VisibleIndex - 1)
        INDGcDetails_AdjustmentTax.HideControl(Not flag, INDGcDetails_AdjustmentBase.VisibleIndex + 1)
        INDGcDetails_TaxRate.HideControl(Not flag, INDGcDetails_AdjustmentTax.VisibleIndex + 1)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tarifa iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateIva_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleIdGeneralLedgerIVA.QueryPopUp
        If INDSleIdGeneralLedgerIVA.Properties.DataSource Is Nothing Then
            InitializeRateIva()
        End If
    End Sub

    ''' <summary>
    ''' Function to validate the adjustment value in an invoice to make the Note
    ''' </summary>
    ''' <param name="adjustmentValue">The value to adjust</param>
    ''' <param name="totalValue">The total value of the invoice</param>
    ''' <param name="balanceValue">The remaining balance of the invoice</param>
    ''' <param name="nature">The nature of the Note (Debit or Credit)</param>
    ''' <returns>An ActionResult indicating whether the adjustment is valid or not</returns>
    Private Function ValidateAdjustmentInvoice(adjustmentValue As Decimal, totalValue As Decimal, balanceValue As Decimal, nature As eNature) As ActionResult

        If adjustmentValue < 0 Then
            Return New ActionResult With {.StateResult = False, .Message = "El valor del ajuste no puede ser negativo"}
        End If

        Select Case nature
            Case eNature.Debit
                If adjustmentValue > (totalValue - balanceValue) Then
                    Return New ActionResult With {.StateResult = False, .Message = "El valor del ajuste no puede ser mayor al valor inicial"}
                End If
            Case eNature.Credit
                If adjustmentValue > balanceValue Then
                    Return New ActionResult With {.StateResult = False, .Message = "El valor del ajuste no puede ser mayor al valor del saldo"}
                End If
            Case Else
                Return New ActionResult With {.StateResult = False, .Message = "Naturaleza inválida"}
        End Select

        ' Si se pasa todas las validaciones, el ajuste es válido
        Return New ActionResult With {.StateResult = True, .Message = "El ajuste es válido"}
    End Function


    ''' <summary>
    ''' funcion que ajusta el saldo del item 
    ''' </summary>
    ''' <param name="invoiceDetail"></param>
    Private Sub TotalAdjustmentInvoiceDetail(ByRef invoiceDetail As PortfolioNoteAccountReceivableDetail, nature As eNature, Optional check As Boolean = False)

        If invoiceDetail Is Nothing Then
            Exit Sub
        End If

        Dim adjustmentValue As Decimal

        If check Then
            Select Case nature
                Case eNature.Debit
                    adjustmentValue = (invoiceDetail.TotalSalesPrice - invoiceDetail.Balance)
                Case eNature.Credit
                    adjustmentValue = invoiceDetail.Balance
            End Select
        End If

        Dim result = Utils.SetValueSalesPrice(True, adjustmentValue, If(invoiceDetail.TaxPercentage, 0))

        With invoiceDetail
            .Selector = check
            .BaseValue = result.Item(Utils.eTypeTaxControl.GrossValue)
            .TaxValue = result.Item(Utils.eTypeTaxControl.TaxValue)
            .Value = result.Item(Utils.eTypeTaxControl.SubtotalSalesPrice)
        End With
    End Sub

    ''' <summary>
    ''' function to identify if the adjustment is  total or parcial
    ''' </summary>
    ''' <returns></returns>
    Private Function IsTotalAdjustment(adjustmentValue As Decimal, balanceValue As Decimal, totalValueItem As Decimal, nature As eNature) As Boolean
        Return (nature = eNature.Debit AndAlso (totalValueItem - balanceValue) <= adjustmentValue) Or (nature = eNature.Credit AndAlso balanceValue <= adjustmentValue)
    End Function


#End Region


End Class