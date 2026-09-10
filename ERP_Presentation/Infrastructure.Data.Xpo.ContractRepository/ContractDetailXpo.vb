'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.ContractDetail")>
Public Class ContractDetailXpo
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

    Dim fContractId As ContractXpo
    <Association("ContractDetailReferencesContract")>
    Public Property ContractId() As ContractXpo
        Get
            Return fContractId
        End Get
        Set(ByVal value As ContractXpo)
            SetPropertyValue(Of ContractXpo)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fContractName As String
    Public Property ContractName() As String
        Get
            Return fContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractName", fContractName, value)
        End Set
    End Property

    Dim fContractNumber As String
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
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

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fBillingInitialDate As DateTime
    Public Property BillingInitialDate() As DateTime
        Get
            Return fBillingInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BillingInitialDate", fBillingInitialDate, value)
        End Set
    End Property

    Dim fBillingEndDate As DateTime
    Public Property BillingEndDate() As DateTime
        Get
            Return fBillingEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BillingEndDate", fBillingEndDate, value)
        End Set
    End Property

    Dim fLegalized As Boolean
    Public Property Legalized() As Boolean
        Get
            Return fLegalized
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Legalized", fLegalized, value)
        End Set
    End Property

    Dim fDateLegalization As DateTime
    Public Property DateLegalization() As DateTime
        Get
            Return fDateLegalization
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateLegalization", fDateLegalization, value)
        End Set
    End Property

    Dim fRadicatedBillingDate As DateTime
    Public Property RadicatedBillingDate() As DateTime
        Get
            Return fRadicatedBillingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicatedBillingDate", fRadicatedBillingDate, value)
        End Set
    End Property

    Dim fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fPermanentObservationOfTheInvoice As String
    Public Property PermanentObservationOfTheInvoice() As String
        Get
            Return fPermanentObservationOfTheInvoice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PermanentObservationOfTheInvoice", fPermanentObservationOfTheInvoice, value)
        End Set
    End Property

    Dim fPrintingMode As Integer
    Public Property PrintingMode() As Integer
        Get
            Return fPrintingMode
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PrintingMode", fPrintingMode, value)
        End Set
    End Property

    Dim fTerminationControl As Integer
    Public Property TerminationControl() As Integer
        Get
            Return fTerminationControl
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TerminationControl", fTerminationControl, value)
        End Set
    End Property

    Dim fNotificationValueType As Integer
    Public Property NotificationValueType() As Integer
        Get
            Return fNotificationValueType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NotificationValueType", fNotificationValueType, value)
        End Set
    End Property

    Dim fPercentageNotification As Decimal
    Public Property PercentageNotification() As Decimal
        Get
            Return fPercentageNotification
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageNotification", fPercentageNotification, value)
        End Set
    End Property

    Dim fNotificationValue As Decimal
    Public Property NotificationValue() As Decimal
        Get
            Return fNotificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NotificationValue", fNotificationValue, value)
        End Set
    End Property

    Dim fNotificationTimeType As Integer
    Public Property NotificationTimeType() As Integer
        Get
            Return fNotificationTimeType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NotificationTimeType", fNotificationTimeType, value)
        End Set
    End Property

    Dim fNotificationDays As Integer
    Public Property NotificationDays() As Integer
        Get
            Return fNotificationDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NotificationDays", fNotificationDays, value)
        End Set
    End Property

    Dim fPercentageApplyPaymentSoon As Decimal
    Public Property PercentageApplyPaymentSoon() As Decimal
        Get
            Return fPercentageApplyPaymentSoon
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageApplyPaymentSoon", fPercentageApplyPaymentSoon, value)
        End Set
    End Property

    Dim fAgesPortfolioId As AgesPortfolioXpo
    <Association("ContractDetailReferencesAgePortfolio")>
    Public Property AgesPortfolioId() As AgesPortfolioXpo
        Get
            Return fAgesPortfolioId
        End Get
        Set(ByVal value As AgesPortfolioXpo)
            SetPropertyValue(Of AgesPortfolioXpo)("AgesPortfolioId", fAgesPortfolioId, value)
        End Set
    End Property

    Dim fValidRecord As Boolean
    Public Property ValidRecord() As Boolean
        Get
            Return fValidRecord
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ValidRecord", fValidRecord, value)
        End Set
    End Property

    <PersistentAlias("concat(ContractNumber,' - ',ContractName)")>
    Public ReadOnly Property ContractNumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContractNumberName"))
        End Get
    End Property

    <Association("ContractDetailNoveltyReferencesContractDetail", GetType(ContractDetailNoveltyXpo))>
    Public ReadOnly Property ContractDetailNoveltyXpo() As XPCollection(Of ContractDetailNoveltyXpo)
        Get
            Return GetCollection(Of ContractDetailNoveltyXpo)("ContractDetailNoveltyXpo")
        End Get
    End Property

    <Association("ContractDetailPolicyReferencesContractDetail", GetType(ContractDetailPolicyXpo))>
    Public ReadOnly Property ContractDetailPolicyXpo() As XPCollection(Of ContractDetailPolicyXpo)
        Get
            Return GetCollection(Of ContractDetailPolicyXpo)("ContractDetailPolicyXpo")
        End Get
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
