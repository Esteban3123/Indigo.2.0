Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.PortfolioNote")> _
Public Class PortfolioNoteXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fNoteDate As DateTime
    Public Property NoteDate() As DateTime
        Get
            Return fNoteDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("NoteDate", fNoteDate, value)
        End Set
    End Property

    Dim fObservations As String
    <Size(SizeAttribute.Unlimited)>
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fNature As Integer
    Public Property Nature() As Integer
        Get
            Return fNature
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Nature", fNature, value)
        End Set
    End Property

    Dim fNoteType As Integer
    Public Property NoteType() As Integer
        Get
            Return fNoteType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NoteType", fNoteType, value)
        End Set
    End Property

    Dim fPortfolioAdvanceId As PortfolioAdvanceXpo
    <Association("Portfolio_PortfolioNoteReferences_PortfolioAdvance")>
    Public Property PortfolioAdvanceId() As PortfolioAdvanceXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceXpo)
            SetPropertyValue(Of PortfolioAdvanceXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property

    Dim fPortfolioTransferId As PortfolioTransferXpo
    <Association("Portfolio_PortfolioNoteReferences_PortfolioTransfer")>
    Public Property PortfolioTransferId() As PortfolioTransferXpo
        Get
            Return fPortfolioTransferId
        End Get
        Set(ByVal value As PortfolioTransferXpo)
            SetPropertyValue(Of PortfolioTransferXpo)("PortfolioTransferId", fPortfolioTransferId, value)
        End Set
    End Property

    Dim fCustomerId As CustomerXpo
    <Association("Portfolio_PortfolioNoteReferencesCommon_Customer")>
    Public Property CustomerId() As CustomerXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CustomerXpo)
            SetPropertyValue(Of CustomerXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)>
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
    <Size(20)>
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
    <Size(20)>
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
    <Size(20)>
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

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferencePortfolioNoteXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property
#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(
Nature = 1, 'Débito', 
Nature = 2, 'Crédito',
'')")>
    Public ReadOnly Property NatureName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
    End Property

    <PersistentAlias("Iif(
NoteType = 1, 'Factura Total', 
NoteType = 2, 'Factura Cuota', 
NoteType = 3, 'Anticipo', 
NoteType = 4, 'Distribucion de Anticipo', 
NoteType = 5, 'Reversión Anticipo vs CxC',
NoteType = 6, 'Factura Detallada',
NoteType = 7, 'Factura glosada',
'')")>
    Public ReadOnly Property NoteTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NoteTypeName"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property Abbreviation As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Abbreviation"))
        End Get
    End Property

    <PersistentAlias("Iif(
Status = 1, 'Registrado', 
Status = 2, 'Confirmado',
Status = 3, 'Anulado', 
'')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif( 
    NoteType = 4, Portfolio_PortfolioNoteDistribution.Sum(Value) ,
    NoteType = 5, PortfolioTransferId.PortfolioTransferDetailXpo.Sum(Value) ,
    NoteType = 6,  Portfolio_PortfolioNoteAccountReceivableAdvance.sum(AdjusmentValue), 
    Portfolio_PortfolioNoteDetail.Sum(Value))")>
    Public ReadOnly Property Value() As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("Value"))
        End Get
    End Property

    <PersistentAlias("Iif(CustomerId.Id > 0, CustomerId.NitName, PortfolioTransferId.Id > 0, PortfolioTransferId.CodeNameCustomer,'')")>
    Public ReadOnly Property CustomerName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CustomerName"))
        End Get
    End Property
#End Region

#Region "Navigations Properties"

    <Association("Portfolio_PortfolioNoteAccountReceivableAdvanceReferencesPortfolio_PortfolioNote", GetType(PortfolioNoteAccountReceivableAdvanceXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteAccountReceivableAdvance() As XPCollection(Of PortfolioNoteAccountReceivableAdvanceXpo)
        Get
            Return GetCollection(Of PortfolioNoteAccountReceivableAdvanceXpo)("Portfolio_PortfolioNoteAccountReceivableAdvance")
        End Get
    End Property

    <Association("Portfolio_PortfolioNoteDetailReferencesPortfolio_PortfolioNote", GetType(PortfolioNoteDetailXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteDetail() As XPCollection(Of PortfolioNoteDetailXpo)
        Get
            Return GetCollection(Of PortfolioNoteDetailXpo)("Portfolio_PortfolioNoteDetail")
        End Get
    End Property

    <Association("Portfolio_PortfolioNoteDistributionReferencesPortfolio_PortfolioNoteDistribution", GetType(PortfolioNoteDistributionOriginalXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteDistribution() As XPCollection(Of PortfolioNoteDistributionOriginalXpo)
        Get
            Return GetCollection(Of PortfolioNoteDistributionOriginalXpo)("Portfolio_PortfolioNoteDistribution")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
