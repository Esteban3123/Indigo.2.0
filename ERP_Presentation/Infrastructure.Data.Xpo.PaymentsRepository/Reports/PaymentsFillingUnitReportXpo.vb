Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.FilingUnit")> _
Public Class PaymentsFillingUnitReportXpo
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
    Dim fParentId As PaymentsFillingUnitReportXpo
    <Association("PaymentsFillingUnitReportXpoReferencesPaymentsFillingUnitReportXpo")> _
    Public Property ParentId() As PaymentsFillingUnitReportXpo
        Get
            Return fParentId
        End Get
        Set(ByVal value As PaymentsFillingUnitReportXpo)
            SetPropertyValue(Of PaymentsFillingUnitReportXpo)("ParentId", fParentId, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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
    <Association("PaymentsAccountPayableTransferReportXpoReferencesPaymentsFillingUnitReportXpo", GetType(PaymentsAccountPayableTransferReportXpo))> _
    Public ReadOnly Property PaymentsAccountPayableTransferReportXpo() As XPCollection(Of PaymentsAccountPayableTransferReportXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableTransferReportXpo)("PaymentsAccountPayableTransferReportXpo")
        End Get
    End Property
    <Association("PaymentsAccountPayableTransferReportXpoReferencesPaymentsFillingUnitReportXpo1", GetType(PaymentsAccountPayableTransferReportXpo))> _
    Public ReadOnly Property PaymentsAccountPayableTransferReportXpo1() As XPCollection(Of PaymentsAccountPayableTransferReportXpo)
        Get
            Return GetCollection(Of PaymentsAccountPayableTransferReportXpo)("PaymentsAccountPayableTransferReportXpo1")
        End Get
    End Property
    <Association("PaymentsFillingUnitReportXpoReferencesPaymentsFillingUnitReportXpo", GetType(PaymentsFillingUnitReportXpo))> _
    Public ReadOnly Property PaymentsFillingUnitReportXpo() As XPCollection(Of PaymentsFillingUnitReportXpo)
        Get
            Return GetCollection(Of PaymentsFillingUnitReportXpo)("PaymentsFillingUnitReportXpo")
        End Get
    End Property
    <Association("PaymentsFillingUnitReportXpoReferencesPaymentsFilingUnitUserReportXpo", GetType(PaymentsFilingUnitUserReportXpo))> _
    Public ReadOnly Property PaymentsFilingUnitUserReportXpo() As XPCollection(Of PaymentsFilingUnitUserReportXpo)
        Get
            Return GetCollection(Of PaymentsFilingUnitUserReportXpo)("PaymentsFilingUnitUserReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
