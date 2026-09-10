Imports DevExpress.Xpo

''' <summary>
''' Entidad XPO para la tabla Billing.RIPSSupportRecord.
''' </summary>
<Persistent("Billing.RIPSSupportRecord")>
Public Class RIPSSupportRecordXpo
    Inherits XPLiteObject

#Region "Members"

    Private fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)(NameOf(Id), fId, value)
        End Set
    End Property

    Private fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)(NameOf(Code), fCode, value)
        End Set
    End Property

    Private fOrderDate As DateTime
    Public Property OrderDate() As DateTime
        Get
            Return fOrderDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)(NameOf(OrderDate), fOrderDate, value)
        End Set
    End Property

    Private fRIPSHospitalStayRecord As Boolean
    Public Property RIPSHospitalStayRecord() As Boolean
        Get
            Return fRIPSHospitalStayRecord
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)(NameOf(RIPSHospitalStayRecord), fRIPSHospitalStayRecord, value)
        End Set
    End Property

    Private fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)(NameOf(Status), fStatus, value)
        End Set
    End Property

    ''' <summary>
    ''' Nombre descriptivo del estado:
    ''' 0 = Registrado, 1 = Confirmado, 2 = Anulado
    ''' </summary>
    <PersistentAlias("Iif(Status = 0, 'Registrado', Iif(Status = 1, 'Confirmado', Iif(Status = 2, 'Anulado', '')))")>
    Public ReadOnly Property StatusFullName As String
        Get
            Return Convert.ToString(EvaluateAlias(NameOf(StatusFullName)))
        End Get
    End Property

    Private fServiceOrder As ServiceOrderXpo
    <Association("Billing_RIPSSupportRecordReferencesBilling_ServiceOrder")>
    Public Property ServiceOrderId() As ServiceOrderXpo
        Get
            Return fServiceOrder
        End Get
        Set(value As ServiceOrderXpo)
            SetPropertyValue(Of ServiceOrderXpo)(NameOf(ServiceOrderId), fServiceOrder, value)
        End Set
    End Property

    Private fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(value As String)
            SetPropertyValue(Of String)(NameOf(CreationUser), fCreationUser, value)
        End Set
    End Property

    Private fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)(NameOf(CreationDate), fCreationDate, value)
        End Set
    End Property

    Private fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(value As String)
            SetPropertyValue(Of String)(NameOf(ModificationUser), fModificationUser, value)
        End Set
    End Property

    Private fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)(NameOf(ModificationDate), fModificationDate, value)
        End Set
    End Property

    Private fTimeStamp As Byte()
    <DbType("timestamp")>
    Public Property TimeStamp() As Byte()
        Get
            Return fTimeStamp
        End Get
        Set(value As Byte())
            SetPropertyValue(Of Byte())(NameOf(TimeStamp), fTimeStamp, value)
        End Set
    End Property

    Private fAdmissionNumber As String
    <Size(10)>
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(value As String)
            SetPropertyValue(Of String)(NameOf(AdmissionNumber), fAdmissionNumber, value)
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class
