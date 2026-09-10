Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Treasury.CheckCashing")> _
Public Class TreasuryCheckCashingXpo
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
    Dim fVoucherTransactionId As TreasuryVoucherTransactionXpo
    <Association("TreasuryCheckCashingXpoReferencesTreasuryVoucherTransactionXpo")> _
    Public Property VoucherTransactionId() As TreasuryVoucherTransactionXpo
        Get
            Return fVoucherTransactionId
        End Get
        Set(ByVal value As TreasuryVoucherTransactionXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionXpo)("VoucherTransactionId", fVoucherTransactionId, value)
        End Set
    End Property
    Dim fCurrentCheckNumber As Long
    Public Property CurrentCheckNumber() As Long
        Get
            Return fCurrentCheckNumber
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("CurrentCheckNumber", fCurrentCheckNumber, value)
        End Set
    End Property
    Dim fNextCheckNumber As Long
    Public Property NextCheckNumber() As Long
        Get
            Return fNextCheckNumber
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("NextCheckNumber", fNextCheckNumber, value)
        End Set
    End Property
    Dim fCancellationDate As DateTime
    Public Property CancellationDate() As DateTime
        Get
            Return fCancellationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CancellationDate", fCancellationDate, value)
        End Set
    End Property
    Dim fCancellationDescription As String
    <Size(300)> _
    Public Property CancellationDescription() As String
        Get
            Return fCancellationDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CancellationDescription", fCancellationDescription, value)
        End Set
    End Property
    Dim fCancellationCheckId As Integer
    Public Property CancellationCheckId() As Integer
        Get
            Return fCancellationCheckId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CancellationCheckId", fCancellationCheckId, value)
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
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
