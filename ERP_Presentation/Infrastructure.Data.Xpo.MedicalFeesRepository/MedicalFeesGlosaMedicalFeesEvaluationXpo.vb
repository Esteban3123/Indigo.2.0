#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.MedicalFeesRepository


#End Region

<Persistent("MedicalFees.GlosaMedicalFeesEvaluation")>
Public Class GlosaMedicalFeesEvaluationXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fGlosaMedicalFeesDetailId As GlosaMedicalFeesDetailXpo
    <Association("GlosaMedicalFeesEvaluationReferences_GlosaMedicalFeesDetail")>
    Public Property GlosaMedicalFeesDetailId() As GlosaMedicalFeesDetailXpo
        Get
            Return fGlosaMedicalFeesDetailId
        End Get
        Set(ByVal value As GlosaMedicalFeesDetailXpo)
            SetPropertyValue(Of GlosaMedicalFeesDetailXpo)("GlosaMedicalFeesDetailId", fGlosaMedicalFeesDetailId, value)
        End Set
    End Property

    Dim fAcceptedValueProv As Decimal
    Public Property AcceptedValueProv() As Decimal
        Get
            Return fAcceptedValueProv
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AcceptedValueProv", fAcceptedValueProv, value)
        End Set
    End Property

    Dim fRaisedValue As Decimal
    Public Property RaisedValue() As Decimal
        Get
            Return fRaisedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RaisedValue", fRaisedValue, value)
        End Set
    End Property

    Dim fPendingValue As Decimal
    Public Property PendingValue() As Decimal
        Get
            Return fPendingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PendingValue", fPendingValue, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
