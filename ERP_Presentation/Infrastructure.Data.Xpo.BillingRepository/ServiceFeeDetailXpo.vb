'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Andres Alarcon
' Created          : 2023-05-30
'
' Copyright        : (c) . All rights reserved.
'*************************************

Imports DevExpress.Xpo

<Persistent("Billing.ServiceFeeDetail")>
Public Class ServiceFeeDetailXpo
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

    Dim fProductAndServiceFeeId As Integer
    Public Property ProductAndServiceFeeId() As Integer
        Get
            Return fProductAndServiceFeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductAndServiceFeeId", fProductAndServiceFeeId, value)
        End Set
    End Property

    Dim fServiceId As BillingConceptXpo
    Public Property ServiceId() As BillingConceptXpo
        Get
            Return fServiceId
        End Get
        Set(ByVal value As BillingConceptXpo)
            SetPropertyValue(Of BillingConceptXpo)("ServiceId", fServiceId, value)
        End Set
    End Property

    <PersistentAlias("ServiceId.Code")>
    Public ReadOnly Property Code() As String
        Get
            Return Me.EvaluateAlias("Code").ToString()
        End Get
    End Property

    <PersistentAlias("ServiceId.CodeName")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Me.EvaluateAlias("CodeName").ToString()
        End Get
    End Property


    <PersistentAlias("ServiceId.Name")>
    Public ReadOnly Property Name() As String
        Get
            Return Me.EvaluateAlias("Name").ToString()
        End Get
    End Property

    <PersistentAlias("ServiceId.AlternativeCode")>
    Public ReadOnly Property AlternativeCode() As String
        Get
            Return Me.EvaluateAlias("AlternativeCode").ToString()
        End Get
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fFinalDate As DateTime
    Public Property FinalDate() As DateTime
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinalDate ", fFinalDate, value)
        End Set
    End Property

    Dim fSalePrice As Decimal
    Public Property SalePrice() As Decimal
        Get
            Return fSalePrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SalePrice", fSalePrice, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region
End Class
