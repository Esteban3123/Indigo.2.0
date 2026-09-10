Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.DistributionLinesDetail")> _
Public Class CommonDistributionLinesDetail
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
    Dim fDistributionLineId As CommonDistributionLines
    <Association("CommonDistributionLinesDetailReferencesCommonDistributionLines")> _
    Public Property DistributionLineId() As CommonDistributionLines
        Get
            Return fDistributionLineId
        End Get
        Set(ByVal value As CommonDistributionLines)
            SetPropertyValue(Of CommonDistributionLines)("DistributionLineId", fDistributionLineId, value)
        End Set
    End Property
    Dim fRetentionConceptId As Integer
    Public Property RetentionConceptId() As Integer
        Get
            Return fRetentionConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionConceptId", fRetentionConceptId, value)
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
