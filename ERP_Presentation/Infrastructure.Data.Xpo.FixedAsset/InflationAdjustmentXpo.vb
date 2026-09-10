#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region

<Persistent("FixedAsset.InflationAdjustment")> _
Public Class InflationAdjustmentXpo
    Inherits XPLiteObject

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
    Dim fYearMonth As String
    <Size(6)> _
    <Persistent("YearMonth")> _
    Public Property YearMonth() As String
        Get
            Return fYearMonth
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("YearMonth", fYearMonth, value)
        End Set
    End Property
    Dim fPercent As Decimal
    <Size(5.2)> _
    <Persistent("Percent")> _
    Public Property Percent() As Decimal
        Get
            Return fPercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percent", fPercent, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
