Imports DevExpress.Xpo
Imports Presentation.Billing.Entities

<Persistent("Billing.ViewListServiceOrderDetail")>
Partial Public Class ViewServiceOrderDetailXpo
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

    Dim fCodeNameIpsService As String
    Public Property CodeNameIpsService() As String
        Get
            Return fCodeNameIpsService
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameIpsService", fCodeNameIpsService, value)
        End Set
    End Property

    Dim fCodeNameCareGroup As String
    Public Property CodeNameCareGroup() As String
        Get
            Return fCodeNameCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeNameCareGroup", fCodeNameCareGroup, value)
        End Set
    End Property

    Dim fRecordType As Integer
    Public Property RecordType() As Integer
        Get
            Return fRecordType
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("RecordType", fRecordType, value)
        End Set
    End Property

    Dim fPackaging As Boolean
    Public Property Packaging() As Boolean
        Get
            Return fPackaging
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Packaging", fPackaging, value)
        End Set
    End Property

    Dim fInvoicedQuantity As Integer
    Public Property InvoicedQuantity() As Integer
        Get
            Return fInvoicedQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoicedQuantity", fInvoicedQuantity, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class