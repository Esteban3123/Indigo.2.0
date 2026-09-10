
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ViewConsultinRoomgByProfessional")>
Partial Public Class ViewConsultinRoomgByProfessionalXpo
    Inherits XPLiteObject

    Dim fId As String
    <Key()>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fConsultingRoomCode As String
    Public Property ConsultingRoomCode As String
        Get
            Return fConsultingRoomCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ConsultingRoomCode", fConsultingRoomCode, value)
        End Set
    End Property

    Dim fConsultingRoomName As String
    Public Property ConsultingRoomName As String
        Get
            Return fConsultingRoomName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ConsultingRoomName", fConsultingRoomName, value)
        End Set
    End Property

    Dim fConsultingRoomCenterAttentionCode As String
    Public Property ConsultingRoomCenterAttentionCode As String
        Get
            Return fConsultingRoomCenterAttentionCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ConsultingRoomCenterAttentionCode", fConsultingRoomCenterAttentionCode, value)
        End Set
    End Property

    Dim fAppointmentDate As Date
    Public Property AppointmentDate As Date
        Get
            Return fAppointmentDate
        End Get
        Set(value As Date)
            SetPropertyValue(Of Date)("AppointmentDate", fAppointmentDate, value)
        End Set
    End Property

    Dim fAppointmentCenterAttentionCode As String
    Public Property AppointmentCenterAttentionCode As String
        Get
            Return fAppointmentCenterAttentionCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("AppointmentCenterAttentionCode", fAppointmentCenterAttentionCode, value)
        End Set
    End Property

    <PersistentAlias("Concat(ConsultingRoomCode, ' - ', ConsultingRoomName)")>
    Public ReadOnly Property CodeDescription As String
        Get
            Return Convert.ToString(EvaluateAlias("CodeDescription"))
        End Get
    End Property
#Region "Association"

#End Region

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
