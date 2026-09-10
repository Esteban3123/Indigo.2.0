'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 2022-05-05
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' Conceptos de recibos de caja usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.CashReceiptConceptUser")>
Public Class CashReceiptConceptUserXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    <PersistentAlias("CashReceipConcept.Id")>
    Public ReadOnly Property CashReceipConceptId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CashReceipConceptId"))
        End Get
    End Property

    Dim fCashReceipConcept As CashReceiptConceptXpo
    <Association("CashReceiptConceptUser_References_CashReceiptConcept")>
    <Persistent("CashReceipConceptId")>
    Public Property CashReceipConcept() As CashReceiptConceptXpo
        Get
            Return fCashReceipConcept
        End Get
        Set(ByVal value As CashReceiptConceptXpo)
            SetPropertyValue(Of CashReceiptConceptXpo)("CashReceipConcept", fCashReceipConcept, value)
        End Set
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
