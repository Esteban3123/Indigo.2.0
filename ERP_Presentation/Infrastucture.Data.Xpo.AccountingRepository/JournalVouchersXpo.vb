'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.JournalVouchers")> _
Public Class JournalVouchersXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fAccountingMovementId As Integer
    <Persistent("AccountingMovementId")> _
    Public Property AccountingMovementId() As Integer
        Get
            Return fAccountingMovementId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountingMovementId", fAccountingMovementId, value)
        End Set
    End Property
   
    Dim fConsecutive As Integer
    <Persistent("Consecutive")> _
    Public Property Consecutive() As Integer
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Consecutive", fConsecutive, value)
        End Set
    End Property

    Dim fLegalBookId As BookXpo
    <Association("JournalVouchersXpoReferencesBookXpo")> _
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fIdJournalVoucher As DocumentTypeXpo
    <Association("JournalVouchersTypeJournalVouchers")> _
    Public Property IdJournalVoucher() As DocumentTypeXpo
        Get
            Return fIdJournalVoucher
        End Get
        Set(ByVal value As DocumentTypeXpo)
            SetPropertyValue(Of DocumentTypeXpo)("IdJournalVoucher", fIdJournalVoucher, value)
        End Set
    End Property

    Dim fVoucherDate As DateTime
    <Persistent("VoucherDate")> _
    Public Property VoucherDate() As DateTime
        Get
            Return fVoucherDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VoucherDate", fVoucherDate, value)
        End Set
    End Property

    Dim fDetail As String
    <Size(500)> _
    <Persistent("Detail")> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fStatus As String
    <Persistent("Status")> _
    Public Property Status() As String
        Get
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
        End Set
    End Property

    Dim fEntityCode As String
    <Size(20)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityName As String
    <Size(250)> _
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fIsClosedYear As Boolean
    Public Property IsClosedYear() As Boolean
        Get
            Return fIsClosedYear
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsClosedYear", fIsClosedYear, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property

    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property

    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    
#End Region

#Region "Customs Members"

    <PersistentAlias("Iif(
EntityName = 'AccountPayable', 'Cuenta por Pagar',
EntityName = 'DeferredCausation', 'Causación Diferida',
EntityName = 'PaymentNotes', 'Notas de Pago',
EntityName = 'PaymentTransfer', 'Traslados de Pago',

EntityName = 'Consignment', 'Consignaciones',
EntityName = 'CashReceipts', 'Recibo de Caja', 
EntityName = 'VoucherTransaction', 'Comprobante de Egreso',
EntityName = 'TreasuryNote', 'Nota de Tesoreria',

EntityName = 'PortfolioNote', 'Nota de Cartera',
EntityName = 'PortfolioTransfer', 'Traslado de Cartera', 
EntityName = 'PortfolioReclassification', 'Reclasificación de Documentos de Cartera',
EntityName = 'PortfolioProvisionAndDeterioration', 'Provisión/Deterioro de Cartera',
EntityName = 'GlosaObjectionsReceptionD', 'Recepción de Objeciones',

EntityName = 'RevenueRecognition', 'Reconocimiento de Ingresos',
EntityName = 'ReverseRevenueRecognition', 'Reversión de Reconocimiento de Ingresos',
EntityName = 'Invoice', 'Factura',
EntityName = 'BasicBilling', 'Facturación Básica',
EntityName = 'DocumentInvoiceProductSales', 'Venta De Productos',
EntityName = 'InvoiceEntityCapitatedDistribution', 'Distribución Ingresos Monto Fijo',
EntityName = 'RadicateInvoiceC', 'Radicación de Cuentas',
EntityName = 'InvoiceEntityCapitated', 'Factura Entidades Capitadas',

EntityName = 'RemissionDevolution', 'Devolución de Remisiones',
EntityName = 'LoanMerchandise', 'Prestamo de Inventario',
EntityName = 'LoanMerchandiseDevolution', 'Devolucion de Prestamo de Inventario',
EntityName = 'RemissionEntrance', 'Remisión de Entrada',
EntityName = 'RemissionEntranceDevolution', 'Devolución de Remision de Entrada',
EntityName = 'RemissionReclassification', 'Reclasificación de Remisión de Entrada',
EntityName = 'RemissionOutput', 'Remisión de Salida',
EntityName = 'RemissionOutputDevolution', 'Devolución de Remisión de Salida',
EntityName = 'ConsignmentInventoryRemission', 'Remisión de Inventario en consignación',
EntityName = 'ConsignmentInventoryRemissionDevolution', 'Devolución de Remisión de Inventario en Consignación',
EntityName = 'DocumentInvoiceProductSalesReclassification', 'Reclasificación por uso de la Remisión de Inventario en Consignación',
EntityName = 'PharmaceuticalDispensingReclassification', 'Reclasificación por uso de la Remisión de Inventario en Consignación',
EntityName = 'PharmaceuticalDispensingDevolutionReclassification', 'Reclasificación por devolución del uso de la Remisión de Inventario en Consignación',
EntityName = 'TransferOrder', 'Orden de Traslado',
EntityName = 'TransferOrderDevolution', 'Devolución de Orden de Traslado',
EntityName = 'PharmaceuticalDispensing', 'Dispensación Farmaceutica',
EntityName = 'PharmaceuticalDispensingDevolution', 'Devolución de Dispensación Farmaceutica',
EntityName = 'InventoryAdjustment', 'Ajuste de Inventario',

EntityName = 'FixedAssetEntry', 'Ingreso de Activos',
EntityName = 'FixedAssetEntryDevolution', 'Devolución Ingreso de Activos',
EntityName = 'FixedAssetReclassification', 'Reclasificación de Activos',
EntityName = 'FixedAssetTransaction', 'Transacciones',
EntityName = 'FixedAssetTransfer', 'Traslado de Activos',
EntityName = 'LeasingContractsFinalization', 'Finalización Contratos Leasing',
EntityName = 'FixedAssetDepreciation', 'Depreciación',

EntityName = 'PayrollLiquidation', 'Nomina',

EntityName = 'TaxesLiquidation', 'Liquidación de Impuestos',
'Ninguno')")>
    Public ReadOnly Property EntityNameDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("EntityNameDescription"))
        End Get
    End Property

    <PersistentAlias("concat(concat(EntityCode,' - '),EntityName)")>
    Public ReadOnly Property CodeNameEntity() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameEntity"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Status = 2, 'Confirmado', Status = 3, 'Anulado', '')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("GeneralLedger_JournalVoucherDetailsCollection.Sum(DebitValue)")>
    Public ReadOnly Property Value As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("Value"))
        End Get
    End Property

#End Region

#Region "NonPersistent"

    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property    

    Private fACEntityCodeName As String
    <NonPersistent()> _
    Public Property ACEntityCodeName() As String
        Get
            Return Me.fACEntityCodeName
        End Get
        Set(value As String)
            Me.fACEntityCodeName = value
        End Set
    End Property

    Private fEntityCodeNameText As String
    <NonPersistent()> _
    Public Property EntityCodeNameText() As String
        Get
            Return Me.fEntityCodeNameText
        End Get
        Set(value As String)
            Me.fEntityCodeNameText = value
        End Set
    End Property

#End Region

#Region "Associations"

    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_JournalVouchers", GetType(JournalVoucherDetailsXpo))> _
    Public ReadOnly Property GeneralLedger_JournalVoucherDetailsCollection() As XPCollection(Of JournalVoucherDetailsXpo)
        Get
            Return GetCollection(Of JournalVoucherDetailsXpo)("GeneralLedger_JournalVoucherDetailsCollection")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
