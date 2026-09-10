Imports DevExpress.Xpo

<Persistent("Scheduling.AppointmentHemocomponentsCUPS")>
Partial Public Class AppointmentHemocomponentsCUPSXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    <PersistentAlias("AppointmentHemocomponents.Id")>
    Public ReadOnly Property IdAppointmentHemocomponents() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("IdAppointmentHemocomponents"))
        End Get
    End Property

    Dim fAppointmentHemocomponents As AppointmentHemocomponentsXpo
    <Persistent("IdAppointmentHemocomponents")>
    <Association("AppointmentHemocomponentsReferencesAppointmentHemocomponentsCUPSXpo")>
    Public Property AppointmentHemocomponents() As AppointmentHemocomponentsXpo
        Get
            Return fAppointmentHemocomponents
        End Get
        Set(ByVal value As AppointmentHemocomponentsXpo)
            SetPropertyValue("AppointmentHemocomponents", fAppointmentHemocomponents, value)
        End Set
    End Property


    Dim fCODSERIPS As String
    Public Property CODSERIPS() As String
        Get
            Return fCODSERIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODSERIPS", fCODSERIPS, value)
        End Set
    End Property

    Dim fTypeLoad As Byte
    Public Property TypeLoad() As Byte
        Get
            Return fTypeLoad
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeLoad", fTypeLoad, value)
        End Set
    End Property

    <PersistentAlias("CUPSEntityContractDescriptions.Id")>
    Public ReadOnly Property IdRelatedDescription As Integer?
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("IdRelatedDescription"))
        End Get
    End Property

    Dim fCUPSEntityContractDescriptions As XpoCUPSEntityContractDescriptions
    <Persistent("IdRelatedDescription")>
    <Association("AppointmentHemocomponentsCUPSXpo_Reference")>
    Public Property CUPSEntityContractDescriptions() As XpoCUPSEntityContractDescriptions
        Get
            Return fCUPSEntityContractDescriptions
        End Get
        Set(ByVal value As XpoCUPSEntityContractDescriptions)
            SetPropertyValue("CUPSEntityContractDescriptions", fCUPSEntityContractDescriptions, value)
        End Set
    End Property

    <PersistentAlias("CUPSEntityContractDescriptions.ContractDescriptionId")>
    Public ReadOnly Property ContractDescriptionId As Integer?
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("ContractDescriptionId"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

End Class
