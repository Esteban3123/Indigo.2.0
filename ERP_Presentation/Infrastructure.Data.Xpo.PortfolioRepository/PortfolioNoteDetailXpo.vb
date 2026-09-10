Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.PortfolioNoteDetail")> _
Partial Public Class PortfolioNoteDetailXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fPortfolioNoteId As PortfolioNoteXpo
    <Association("Portfolio_PortfolioNoteDetailReferencesPortfolio_PortfolioNote")> _
    Public Property PortfolioNoteId() As PortfolioNoteXpo
        Get
            Return fPortfolioNoteId
        End Get
        Set(ByVal value As PortfolioNoteXpo)
            SetPropertyValue(Of PortfolioNoteXpo)("PortfolioNoteId", fPortfolioNoteId, value)
        End Set
    End Property
    Dim fPortfolioNoteConceptId As Integer
    Public Property PortfolioNoteConceptId() As Integer
        Get
            Return fPortfolioNoteConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioNoteConceptId", fPortfolioNoteConceptId, value)
        End Set
    End Property
    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
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
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fRetentionConceptId As Integer
    Public Property RetentionConceptId() As Integer
        Get
            Return fRetentionConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionConceptId", fRetentionConceptId, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(3000)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class