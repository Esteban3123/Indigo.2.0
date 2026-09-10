'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 2017-07-24
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports DevExpress.Xpo

#End Region

''' <summary>
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("Billing.ViewListRecognitionReverse")>
Public Class ViewListRecognitionReverse
    Inherits XPLiteObject

#Region "Members"

    Dim fRecognitionId As Integer
    <Key(True)>
    Public Property RecognitionId() As Integer
        Get
            Return fRecognitionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecognitionId", fRecognitionId, value)
        End Set
    End Property

    <PersistentAlias("RecognitionId")>
    Public Property CareGroupId() As Integer

    Dim fStateOperation As Byte
    Public Property StateOperation() As Byte
        Get
            Return fStateOperation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StateOperation", fStateOperation, value)
        End Set
    End Property

    Dim fMessageInfo As String
    Public Property MessageInfo() As String
        Get
            Return fMessageInfo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MessageInfo", fMessageInfo, value)
        End Set
    End Property


    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    Dim fTotalCareGroup As Decimal
    Public Property TotalCareGroup() As Decimal
        Get
            Return fTotalCareGroup
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalCareGroup", fTotalCareGroup, value)
        End Set
    End Property

    Dim fFolioQuantity As Integer
    Public Property FolioQuantity() As Integer
        Get
            Return fFolioQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FolioQuantity", fFolioQuantity, value)
        End Set
    End Property

    Dim fOperativeUnitId As Integer
    Public Property OperativeUnitId() As Integer
        Get
            Return fOperativeUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperativeUnitId", fOperativeUnitId, value)
        End Set
    End Property

    Dim fJournalVoucherTypeCodeName As String
    Public Property JournalVoucherTypeCodeName() As String
        Get
            Return fJournalVoucherTypeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JournalVoucherTypeCodeName", fJournalVoucherTypeCodeName, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
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


    Dim fVoucherDate As DateTime
    Public Property VoucherDate() As DateTime
        Get
            Return fVoucherDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VoucherDate", fVoucherDate, value)
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

