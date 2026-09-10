Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.ViewPortfolioNotesDebitCreditReport")>
Public Class PortfolioNoteReportXpo
    Inherits XPLiteObject
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
    Dim fCustomerId As CommonCustomerReportXpo
    <Association("PortfolioNoteReportXpoReferencesCommonCustomerReportXpo")>
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
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
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fNoteType As Byte
    Public Property NoteType() As Byte
        Get
            Return fNoteType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("NoteType", fNoteType, value)
        End Set
    End Property
    Dim fPortfolioAdvanceId As PortfolioAdvanceReportXpo
    <Association("PortfolioNoteReferencesPortfolioAdvanceReportXpo")>
    Public Property PortfolioAdvanceId() As PortfolioAdvanceReportXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceReportXpo)
            SetPropertyValue(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
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
    Dim fCUDE As String
    <Size(20)>
    Public Property CUDE() As String
        Get
            Return fCUDE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUDE", fCUDE, value)
        End Set
    End Property
    Dim fQR As String
    <Size(20)>
    Public Property QR() As String
        Get
            Return fQR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QR", fQR, value)
        End Set
    End Property
    Dim fConsecutive As String
    Public Property Consecutive() As String
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Consecutive", fConsecutive, value)
        End Set
    End Property
    Dim fValidationDate As DateTime
    Public Property ValidationDate() As DateTime
        Get
            Return fValidationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ValidationDate", fValidationDate, value)
        End Set
    End Property
    Dim fElectronicDocumentStatus As Integer
    Public Property ElectronicDocumentStatus() As Integer
        Get
            Return fElectronicDocumentStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ElectronicDocumentStatus", fElectronicDocumentStatus, value)
        End Set
    End Property
    Dim fShippingDate As DateTime
    Public Property ShippingDate() As DateTime
        Get
            Return fShippingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ShippingDate", fShippingDate, value)
        End Set
    End Property

    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()>
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    'Propiedad Añadida valor total en letras
    Dim fValueLetters As String
    <NonPersistent()>
    Public Property ValueLetters() As String
        Get
            Return fValueLetters
        End Get
        Set(ByVal value As String)
            Me.fValueLetters = value
        End Set
    End Property

    'Propiedad Añadida valor total 
    Dim fValueTotal As Decimal
    <NonPersistent()>
    Public Property ValueTotal() As Decimal
        Get
            Return fValueTotal
        End Get
        Set(ByVal value As Decimal)
            Me.fValueTotal = value
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

    Dim fCurrencyName As String
    Public Property CurrencyName() As String
        Get
            Return fCurrencyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyName", fCurrencyName, value)
        End Set
    End Property



    Dim fPortfolioTransferId As PortfolioTransferXpo
    <Association("Portfolio_PortfolioNoteReportReferences_PortfolioTransfer")>
    Public Property PortfolioTransferId() As PortfolioTransferXpo
        Get
            Return fPortfolioTransferId
        End Get
        Set(ByVal value As PortfolioTransferXpo)
            SetPropertyValue(Of PortfolioTransferXpo)("PortfolioTransferId", fPortfolioTransferId, value)
        End Set
    End Property


    <Association("Portfolio_PortfolioNoteAccountReceivableAdvanceReferencesPortfolio_PortfolioNote", GetType(PortfolioNoteAccountReceivableAdvanceReportXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteAccountReceivableAdvances() As XPCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)("Portfolio_PortfolioNoteAccountReceivableAdvances")
        End Get
    End Property
    <Association("Portfolio_PortfolioNoteDetailReferencesPortfolio_PortfolioNote", GetType(PortfolioNoteDetailReportXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteDetails() As XPCollection(Of PortfolioNoteDetailReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteDetailReportXpo)("Portfolio_PortfolioNoteDetails")
        End Get
    End Property
    <Association("Portfolio_PortfolioNoteDistributionReferencesPortfolio_PortfolioNote", GetType(PortfolioNoteDistributionXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteDistribution() As XPCollection(Of PortfolioNoteDistributionXpo)
        Get
            Return GetCollection(Of PortfolioNoteDistributionXpo)("Portfolio_PortfolioNoteDistribution")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
