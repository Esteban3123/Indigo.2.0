Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.VReportSubAccount")> _
Public Class FixedAssetVReportSubAccountReportXpo
    Inherits XPLiteObject
    Dim fid As Integer
    <Key(True)> _
    Public Property id() As Integer
        Get
            Return fid
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("id", fid, value)
        End Set
    End Property
    Dim fNumberAccount As String
    <Size(50)> _
    Public Property NumberAccount() As String
        Get
            Return fNumberAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberAccount", fNumberAccount, value)
        End Set
    End Property
    Dim fNameAccount As String
    Public Property NameAccount() As String
        Get
            Return fNameAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameAccount", fNameAccount, value)
        End Set
    End Property
    Dim fPlate As String
    <Size(50)> _
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fCodeType As String
    <Size(20)> _
    Public Property CodeType() As String
        Get
            Return fCodeType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeType", fCodeType, value)
        End Set
    End Property
    Dim fNameType As String
    <Size(50)> _
    Public Property NameType() As String
        Get
            Return fNameType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameType", fNameType, value)
        End Set
    End Property
    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property
    Dim fValorization As Decimal
    Public Property Valorization() As Decimal
        Get
            Return fValorization
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Valorization", fValorization, value)
        End Set
    End Property
    Dim fDevaluation As Decimal
    Public Property Devaluation() As Decimal
        Get
            Return fDevaluation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Devaluation", fDevaluation, value)
        End Set
    End Property
    Dim fHasOutput As Boolean
    Public Property HasOutput() As Boolean
        Get
            Return fHasOutput
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasOutput", fHasOutput, value)
        End Set
    End Property
    Dim fDeprecationAccount As String
    <Size(50)> _
    Public Property DeprecationAccount() As String
        Get
            Return fDeprecationAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DeprecationAccount", fDeprecationAccount, value)
        End Set
    End Property
    Dim fDepreciatedValue As Decimal
    Public Property DepreciatedValue() As Decimal
        Get
            Return fDepreciatedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciatedValue", fDepreciatedValue, value)
        End Set
    End Property
    Dim fResidualValue As Decimal
    Public Property ResidualValue() As Decimal
        Get
            Return fResidualValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResidualValue", fResidualValue, value)
        End Set
    End Property
    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
