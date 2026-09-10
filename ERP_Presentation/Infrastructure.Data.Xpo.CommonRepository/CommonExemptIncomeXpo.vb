Imports DevExpress.Xpo

<Persistent("Common.ThirdpartyAccumulatedExemptIncome ")>
Public Class CommonExemptIncomeXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
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

    Dim fAccumulatedValue As Decimal
    Public Property AccumulatedValue() As Decimal
        Get
            Return fAccumulatedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccumulatedValue", fAccumulatedValue, value)
        End Set
    End Property

    Dim fAccumulatedMaxDeductionsAndRentExents As Decimal
    Public Property AccumulatedMaxDeductionsAndRentExents() As Decimal
        Get
            Return fAccumulatedMaxDeductionsAndRentExents
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccumulatedMaxDeductionsAndRentExents", fAccumulatedMaxDeductionsAndRentExents, value)
        End Set
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
