Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VReportConsignmentTransfer")> _
Public Class TreasuryVReportConsignmentTransfer
    Inherits XPLiteObject

    Dim fRow As Integer
    <Key(True)> _
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property
    Dim fType As String
    <Size(12)> _
    Public Property Type() As String
        Get
            Return fType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fGroupByBankCash As String
    <Size(423)> _
    Public Property GroupByBankCash() As String
        Get
            Return fGroupByBankCash
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupByBankCash", fGroupByBankCash, value)
        End Set
    End Property
    Dim fCodeBank As String
    <Size(20)> _
    Public Property CodeBank() As String
        Get
            Return fCodeBank
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeBank", fCodeBank, value)
        End Set
    End Property
    Dim fNameBank As String
    <Size(320)> _
    Public Property NameBank() As String
        Get
            Return fNameBank
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameBank", fNameBank, value)
        End Set
    End Property
    Dim fCodeCash As String
    <Size(20)> _
    Public Property CodeCash() As String
        Get
            Return fCodeCash
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCash", fCodeCash, value)
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
