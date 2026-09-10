'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2025-01-17
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewSumQuantityByPreparationStatus")>
Partial Public Class ViewSumQuantityByPreparationStatusXpo
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fRequestedQuantity As Integer
    Public Property RequestedQuantity() As Integer
        Get
            Return fRequestedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestedQuantity", fRequestedQuantity, value)
        End Set
    End Property

    Dim fProductionQuantity As Integer
    Public Property ProductionQuantity() As Integer
        Get
            Return fProductionQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionQuantity", fProductionQuantity, value)
        End Set
    End Property

    Dim fFinishedQuantity As Integer
    Public Property FinishedQuantity() As Integer
        Get
            Return fFinishedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FinishedQuantity", fFinishedQuantity, value)
        End Set
    End Property

    Dim fReleasedQuantity As Integer
    Public Property ReleasedQuantity() As Integer
        Get
            Return fReleasedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReleasedQuantity", fReleasedQuantity, value)
        End Set
    End Property

    Dim fReprocessedQuantity As Integer
    Public Property ReprocessedQuantity() As Integer
        Get
            Return fReprocessedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReprocessedQuantity", fReprocessedQuantity, value)
        End Set
    End Property

    Dim fRejectedQuantity As Integer
    Public Property RejectedQuantity() As Integer
        Get
            Return fRejectedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RejectedQuantity", fRejectedQuantity, value)
        End Set
    End Property

    Dim fCancelledQuantity As Integer
    Public Property CancelledQuantity() As Integer
        Get
            Return fCancelledQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CancelledQuantity", fCancelledQuantity, value)
        End Set
    End Property

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class