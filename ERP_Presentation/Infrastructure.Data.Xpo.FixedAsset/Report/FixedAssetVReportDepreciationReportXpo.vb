Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.VReportDepreciation")> _
Public Class FixedAssetVReportDepreciationReportXpo
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
    Dim fCodeItem As String
    <Size(20)> _
    Public Property CodeItem() As String
        Get
            Return fCodeItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeItem", fCodeItem, value)
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
    Dim fNumber As String
    <Size(50)> _
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
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
    Dim fSerie As String
    <Size(50)> _
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
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
    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
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
    Dim fName As String
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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
    Dim fResidualValue As Decimal
    Public Property ResidualValue() As Decimal
        Get
            Return fResidualValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResidualValue", fResidualValue, value)
        End Set
    End Property
    Dim fDepreciationValue As Decimal
    Public Property DepreciationValue() As Decimal
        Get
            Return fDepreciationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciationValue", fDepreciationValue, value)
        End Set
    End Property
    Dim fDepreciatedDays As Integer
    Public Property DepreciatedDays() As Integer
        Get
            Return fDepreciatedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepreciatedDays", fDepreciatedDays, value)
        End Set
    End Property
    Dim fClosingYear As Integer
    Public Property ClosingYear() As Integer
        Get
            Return fClosingYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ClosingYear", fClosingYear, value)
        End Set
    End Property
    Dim fClosingMonth As Integer
    Public Property ClosingMonth() As Integer
        Get
            Return fClosingMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ClosingMonth", fClosingMonth, value)
        End Set
    End Property
    Dim fCodeLegalBook As String
    <Size(20)> _
    Public Property CodeLegalBook() As String
        Get
            Return fCodeLegalBook
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeLegalBook", fCodeLegalBook, value)
        End Set
    End Property
    Dim fNameLegalBook As String
    Public Property NameLegalBook() As String
        Get
            Return fNameLegalBook
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameLegalBook", fNameLegalBook, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
