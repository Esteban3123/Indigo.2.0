Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.ViewReportHistoricalDepreciation")>
Public Class FixedAssetViewReportHistoricalDepreciationXpo
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
    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property
    Dim fLegalBookCode As String
    <Size(20)>
    Public Property LegalBookCode() As String
        Get
            Return fLegalBookCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LegalBookCode", fLegalBookCode, value)
        End Set
    End Property
    Dim fLegalBookName As String
    <Size(300)>
    Public Property LegalBookName() As String
        Get
            Return fLegalBookName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LegalBookName", fLegalBookName, value)
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
    Dim fPlate As String
    <Size(50)>
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property
    Dim fSerie As String
    <Size(50)>
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
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
    Dim fMainAccountNumber As String
    <Size(50)>
    Public Property MainAccountNumber() As String
        Get
            Return fMainAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumber", fMainAccountNumber, value)
        End Set
    End Property
    Dim fMainAccountName As String
    Public Property MainAccountName() As String
        Get
            Return fMainAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountName", fMainAccountName, value)
        End Set
    End Property
    Dim fDepreciationAccountNumber As String
    <Size(50)>
    Public Property DepreciationAccountNumber() As String
        Get
            Return fDepreciationAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DepreciationAccountNumber", fDepreciationAccountNumber, value)
        End Set
    End Property
    Dim fDepreciationAccountName As String
    Public Property DepreciationAccountName() As String
        Get
            Return fDepreciationAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DepreciationAccountName", fDepreciationAccountName, value)
        End Set
    End Property
    Dim fCatalogCode As String
    <Size(20)>
    Public Property CatalogCode() As String
        Get
            Return fCatalogCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CatalogCode", fCatalogCode, value)
        End Set
    End Property
    Dim fCatalogDescription As String
    <Size(300)>
    Public Property CatalogDescription() As String
        Get
            Return fCatalogDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CatalogDescription", fCatalogDescription, value)
        End Set
    End Property
    Dim fItemCode As String
    <Size(20)>
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCode", fItemCode, value)
        End Set
    End Property
    Dim fItemDescription As String
    <Size(300)>
    Public Property ItemDescription() As String
        Get
            Return fItemDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemDescription", fItemDescription, value)
        End Set
    End Property
    Dim fTypeCode As String
    <Size(20)>
    Public Property TypeCode() As String
        Get
            Return fTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeCode", fTypeCode, value)
        End Set
    End Property
    Dim fTypeName As String
    <Size(300)>
    Public Property TypeName() As String
        Get
            Return fTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeName", fTypeName, value)
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
    Dim fAccumulatedDepreciation As Decimal
    Public Property AccumulatedDepreciation() As Decimal
        Get
            Return fAccumulatedDepreciation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccumulatedDepreciation", fAccumulatedDepreciation, value)
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
    Dim fRemainingLifeTime As Integer
    Public Property RemainingLifeTime() As Integer
        Get
            Return fRemainingLifeTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RemainingLifeTime", fRemainingLifeTime, value)
        End Set
    End Property
    Dim fLifeTimeInDays As Integer
    Public Property LifeTimeInDays() As Integer
        Get
            Return fLifeTimeInDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LifeTimeInDays", fLifeTimeInDays, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
