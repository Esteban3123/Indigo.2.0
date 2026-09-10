Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.VRetentionsReports")> _
Public Class GeneralLedgerRetentionsReportXpo
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
    Dim fVoucherDate As DateTime
    Public Property VoucherDate() As DateTime
        Get
            Return fVoucherDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VoucherDate", fVoucherDate, value)
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
    Dim fCuenta As String
    Public Property Cuenta() As String
        Get
            Return fCuenta
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Cuenta", fCuenta, value)
        End Set
    End Property
    Dim fNit As String
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fTercero As String
    <Size(300)> _
    Public Property Tercero() As String
        Get
            Return fTercero
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tercero", fTercero, value)
        End Set
    End Property
    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property
    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
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
    Dim fRetencion As String
    Public Property Retencion() As String
        Get
            Return fRetencion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Retencion", fRetencion, value)
        End Set
    End Property
    Dim fRetentionRate As Decimal
    Public Property RetentionRate() As Decimal
        Get
            Return fRetentionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionRate", fRetentionRate, value)
        End Set
    End Property
    Dim fBase As Long
    Public Property Base() As Long
        Get
            Return fBase
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Base", fBase, value)
        End Set
    End Property
    Dim fEstado As String
    <Size(10)> _
    Public Property Estado() As String
        Get
            Return fEstado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Estado", fEstado, value)
        End Set
    End Property
    Dim fIdRetention As Integer
    Public Property IdRetention() As Integer
        Get
            Return fIdRetention
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetention", fIdRetention, value)
        End Set
    End Property
    Dim fCodeNameVoucherType As String
    <Size(150)> _
    Public Property CodeNameVoucherType() As String
        Get
            Return fCodeNameVoucherType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameVoucherType", fCodeNameVoucherType, value)
        End Set
    End Property
    Dim fConsecutive As Long
    Public Property Consecutive() As Long
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Consecutive", fConsecutive, value)
        End Set
    End Property
    Dim fOriginDocument As String
    <Size(100)> _
    Public Property OriginDocument() As String
        Get
            Return fOriginDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OriginDocument", fOriginDocument, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
