#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region

<Persistent("GeneralLedger.CompanySettings")>
Public Class CompanySettingsXpo
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
    Dim fSMLV As Decimal
    Public Property SMLV() As Decimal
        Get
            Return fSMLV
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SMLV", fSMLV, value)
        End Set
    End Property
    Dim fUVT As Decimal
    Public Property UVT() As Decimal
        Get
            Return fUVT
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UVT", fUVT, value)
        End Set
    End Property

    Dim fTaxRegistration As Byte
    Public Property TaxRegistration() As Byte
        Get
            Return fTaxRegistration
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TaxRegistration", fTaxRegistration, value)
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
