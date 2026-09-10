Imports DevExpress.Xpo

<Persistent("Payments.ViewThirdPartyRetentionAccumulated")>
Public Class ViewThirdPartyRetentionAccumulatedXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
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

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fExemptIncome As Decimal
    Public Property ExemptIncome() As Decimal
        Get
            Return fExemptIncome
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExemptIncome", fExemptIncome, value)
        End Set
    End Property

    Dim fMaxDeductionsAndRentExents As Decimal
    Public Property MaxDeductionsAndRentExents() As Decimal
        Get
            Return fMaxDeductionsAndRentExents
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaxDeductionsAndRentExents", fMaxDeductionsAndRentExents, value)
        End Set
    End Property

#End Region

#Region "Builder"

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
