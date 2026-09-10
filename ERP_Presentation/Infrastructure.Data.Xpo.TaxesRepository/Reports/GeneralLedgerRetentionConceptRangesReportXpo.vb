Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.RetentionConceptRanges")> _
Public Class GeneralLedgerRetentionConceptRangesReportXpo
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
    Dim fRetentionId As GeneralLedgerRetentionConceptsReportXpo
    <Association("GeneralLedger_RetentionConceptRangesReferencesGeneralLedger_RetentionConcepts")> _
    Public Property RetentionId() As GeneralLedgerRetentionConceptsReportXpo
        Get
            Return fRetentionId
        End Get
        Set(ByVal value As GeneralLedgerRetentionConceptsReportXpo)
            SetPropertyValue(Of GeneralLedgerRetentionConceptsReportXpo)("RetentionId", fRetentionId, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property
    Dim fValueInitial As Decimal
    Public Property ValueInitial() As Decimal
        Get
            Return fValueInitial
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueInitial", fValueInitial, value)
        End Set
    End Property
    Dim fValueFinish As Decimal
    Public Property ValueFinish() As Decimal
        Get
            Return fValueFinish
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueFinish", fValueFinish, value)
        End Set
    End Property
    Dim fValueDeducted As Decimal
    Public Property ValueDeducted() As Decimal
        Get
            Return fValueDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDeducted", fValueDeducted, value)
        End Set
    End Property
    Dim fValueIncrement As Decimal
    Public Property ValueIncrement() As Decimal
        Get
            Return fValueIncrement
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueIncrement", fValueIncrement, value)
        End Set
    End Property
    Dim fUVTIncrement As Decimal
    Public Property UVTIncrement() As Decimal
        Get
            Return fUVTIncrement
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UVTIncrement", fUVTIncrement, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
