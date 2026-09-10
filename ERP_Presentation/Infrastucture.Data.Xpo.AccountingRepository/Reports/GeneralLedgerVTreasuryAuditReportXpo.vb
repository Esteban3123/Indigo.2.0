Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.VTreasutyAudit")> _
Public Class GeneralLedgerVTreasuryAuditReportXpo
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
    Dim fEntity As String
    <Size(21)> _
    Public Property Entity() As String
        Get
            Return fEntity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Entity", fEntity, value)
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
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
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
    Dim fTreasuryValue As Decimal
    Public Property TreasuryValue() As Decimal
        Get
            Return fTreasuryValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TreasuryValue", fTreasuryValue, value)
        End Set
    End Property
    Dim fGeneralValue As Decimal
    Public Property GeneralValue() As Decimal
        Get
            Return fGeneralValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GeneralValue", fGeneralValue, value)
        End Set
    End Property
    Dim fCash As String
    <Size(123)> _
    Public Property Cash() As String
        Get
            Return fCash
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Cash", fCash, value)
        End Set
    End Property
    Dim fEntityBank As String
    <Size(123)> _
    Public Property EntityBank() As String
        Get
            Return fEntityBank
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBank", fEntityBank, value)
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
    Dim fMainAccount As String
    Public Property MainAccount() As String
        Get
            Return fMainAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccount", fMainAccount, value)
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
