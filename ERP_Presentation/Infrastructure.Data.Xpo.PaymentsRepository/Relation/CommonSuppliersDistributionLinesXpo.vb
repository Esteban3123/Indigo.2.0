Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.SuppliersDistributionLines")> _
Public Class CommonSuppliersDistributionLinesXpo
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
    Dim fIdSupplier As Maintenance_Supplier
    <Association("CommonSuppliersDistributionLinesXpoReferencesMaintenance_Supplier")> _
    Public Property IdSupplier() As Maintenance_Supplier
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("IdSupplier", fIdSupplier, value)
        End Set
    End Property
    Dim fIdDistributionLine As CommonDistributionLines
    <Association("CommonSuppliersDistributionLinesXpoReferencesCommonDistributionLines")> _
    Public Property IdDistributionLine() As CommonDistributionLines
        Get
            Return fIdDistributionLine
        End Get
        Set(ByVal value As CommonDistributionLines)
            SetPropertyValue(Of CommonDistributionLines)("IdDistributionLine", fIdDistributionLine, value)
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

    <Association("PaymentsAccountPayableReferencesCommonSuppliersDistributionLinesXpo", GetType(PaymentsAccountPayable))> _
    Public ReadOnly Property PaymentsAccountPayable() As XPCollection(Of PaymentsAccountPayable)
        Get
            Return GetCollection(Of PaymentsAccountPayable)("PaymentsAccountPayable")
        End Get
    End Property

    <Association("PaymentsPaymentNotesReferencesCommonSuppliersDistributionLinesXpo", GetType(PaymentsPaymentNotes))> _
    Public ReadOnly Property PaymentsPaymentNotes() As XPCollection(Of PaymentsPaymentNotes)
        Get
            Return GetCollection(Of PaymentsPaymentNotes)("PaymentsPaymentNotes")
        End Get
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
