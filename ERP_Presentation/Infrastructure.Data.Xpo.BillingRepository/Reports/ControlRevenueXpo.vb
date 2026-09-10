Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.RevenueControl")> _
Public Class ControlRevenueXpo
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
    Dim fAdmissionNumber As String
    '<Indexed(Name:="IX_RevenueControl", Unique:=True)> _
    <Size(10)> _
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property
    Dim fPatientCode As String
    <Size(15)> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property
    Dim fFolioQuantity As Byte
    Public Property FolioQuantity() As Byte
        Get
            Return fFolioQuantity
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FolioQuantity", fFolioQuantity, value)
        End Set
    End Property
    Dim fTopEventFeeModerator As Decimal
    Public Property TopEventFeeModerator() As Decimal
        Get
            Return fTopEventFeeModerator
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TopEventFeeModerator", fTopEventFeeModerator, value)
        End Set
    End Property
    Dim fTopEventCopay As Decimal
    Public Property TopEventCopay() As Decimal
        Get
            Return fTopEventCopay
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TopEventCopay", fTopEventCopay, value)
        End Set
    End Property
    Dim fTopEventFeeRecovery As Decimal
    Public Property TopEventFeeRecovery() As Decimal
        Get
            Return fTopEventFeeRecovery
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TopEventFeeRecovery", fTopEventFeeRecovery, value)
        End Set
    End Property
    <Association("Billing_RevenueControlDetailReferencesBilling_RevenueControl", GetType(ControlRevenueDetailXpo))> _
    Public ReadOnly Property Billing_RevenueControlDetail() As XPCollection(Of ControlRevenueDetailXpo)
        Get
            Return GetCollection(Of ControlRevenueDetailXpo)("Billing_RevenueControlDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
