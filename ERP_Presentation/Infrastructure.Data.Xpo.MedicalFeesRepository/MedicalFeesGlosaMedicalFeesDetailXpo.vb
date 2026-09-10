#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.MedicalFeesRepository


#End Region

<Persistent("MedicalFees.GlosaMedicalFeesDetail")>
Public Class GlosaMedicalFeesDetailXpo
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

    Dim fGlosaMedicalFeesId As GlosaMedicalFeesXpo
    <Association("GlosaMedicalFeesDetailReferences_GlosaMedicalFees")>
    Public Property GlosaMedicalFeesId() As GlosaMedicalFeesXpo
        Get
            Return fGlosaMedicalFeesId
        End Get
        Set(ByVal value As GlosaMedicalFeesXpo)
            SetPropertyValue(Of GlosaMedicalFeesXpo)("GlosaMedicalFeesId", fGlosaMedicalFeesId, value)
        End Set
    End Property

    Dim fAccountPayableId As AccountPayableXpo
    <Association("GlosaMedicalFeesDetailReferences_AccountPayableXpo")>
    Public Property AccountPayableId() As AccountPayableXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As AccountPayableXpo)
            SetPropertyValue(Of AccountPayableXpo)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fGlosaMedicalFeesConceptsId As GlosaMedicalFeesConceptsXpo
    <Association("GlosaMedicalFeesDetailReferences_GlosaMedicalFeesConcepts")>
    Public Property GlosaMedicalFeesConceptsId() As GlosaMedicalFeesConceptsXpo
        Get
            Return fGlosaMedicalFeesConceptsId
        End Get
        Set(ByVal value As GlosaMedicalFeesConceptsXpo)
            SetPropertyValue(Of GlosaMedicalFeesConceptsXpo)("GlosaMedicalFeesConceptsId", fGlosaMedicalFeesConceptsId, value)
        End Set
    End Property

    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
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

    Dim fEvaluated As Boolean
    Public Property Evaluated() As Boolean
        Get
            Return fEvaluated
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Evaluated", fEvaluated, value)
        End Set
    End Property
#End Region

#Region "Associations"
    <Association("GlosaMedicalFeesEvaluationReferences_GlosaMedicalFeesDetail", GetType(GlosaMedicalFeesEvaluationXpo))>
    Public ReadOnly Property GlosaMedicalFeesEvaluationXpo() As XPCollection(Of GlosaMedicalFeesEvaluationXpo)
        Get
            Return GetCollection(Of GlosaMedicalFeesEvaluationXpo)("GlosaMedicalFeesEvaluationXpo")
        End Get
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
