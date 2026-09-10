'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Angi Camila Durán Vargas
' Created          : 23-02-2023
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.AgreementsRedemptionPointsDetail")>
Public Class AgreementsRedemptionPointsDetailXpo
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

    Dim fAgreementsRedemptionPointsId As AgreementsRedemptionPointsXpo
    <Persistent("AgreementsRedemptionPointsId")>
    <Association("AgreementsRedemptionPointsReferenceAgreementsRedemptionPointsDetail")>
    Public Property AgreementsRedemptionPointsId() As AgreementsRedemptionPointsXpo
        Get
            Return fAgreementsRedemptionPointsId
        End Get
        Set(ByVal value As AgreementsRedemptionPointsXpo)
            SetPropertyValue(Of AgreementsRedemptionPointsXpo)("AgreementsRedemptionPointsId", fAgreementsRedemptionPointsId, value)
        End Set
    End Property

    Dim fPointsAmount As Integer
    <Persistent("PointsAmount")>
    Public Property PointsAmount() As Integer
        Get
            Return fPointsAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PointsAmount", fPointsAmount, value)
        End Set
    End Property

    Dim fPointsValue As Decimal
    <Persistent("PointsValue")>
    Public Property PointsValue() As Decimal
        Get
            Return fPointsValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PointsValue", fPointsValue, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    <Persistent("InitialDate")>
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    <Persistent("EndDate")>
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
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
