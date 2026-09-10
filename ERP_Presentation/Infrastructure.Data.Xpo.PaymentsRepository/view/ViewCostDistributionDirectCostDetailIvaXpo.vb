'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Andrea Pahola Coqueco
' Created          : 01-01-2024
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Payments.ViewCostDistributionDirectCostDetailIva")>
Public Class ViewCostDistributionDirectCostDetailIvaXpo
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

    Dim fAccountPayableId As Integer
    Public Property AccountPayableId() As Integer
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fAccountPayableDetailConceptId As Integer
    Public Property AccountPayableDetailConceptId() As Integer
        Get
            Return fAccountPayableDetailConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountPayableDetailConceptId", fAccountPayableDetailConceptId, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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

    Dim fIvaValue As Decimal
    Public Property IvaValue() As Decimal
        Get
            Return fIvaValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaValue", fIvaValue, value)
        End Set
    End Property

#End Region

#Region "Builder"

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