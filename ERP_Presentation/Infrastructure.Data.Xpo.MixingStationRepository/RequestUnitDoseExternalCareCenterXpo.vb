'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/02/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.RequestUnitDoseExternalCareCenter")>
Partial Public Class RequestUnitDoseExternalCareCenterXpo
    Inherits XPLiteObject

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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fCMConfigurationId As MixinStationCMConfigXpo
    <Association("RequestUnitDoseExternalCareCenterReferencesCMConfiguration")>
    Public Property CMConfigurationId() As MixinStationCMConfigXpo
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As MixinStationCMConfigXpo)
            SetPropertyValue(Of MixinStationCMConfigXpo)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fExternalCareCenterId As ExternalCareCenterXpo
    <Association("RequestUnitDoseExternalCareCenterReferencesExternalCareCenter")>
    Public Property ExternalCareCenterId() As ExternalCareCenterXpo
        Get
            Return fExternalCareCenterId
        End Get
        Set(ByVal value As ExternalCareCenterXpo)
            SetPropertyValue(Of ExternalCareCenterXpo)("ExternalCareCenterId", fExternalCareCenterId, value)
        End Set
    End Property

    Dim fRequestType As Integer
    Public Property RequestType() As Integer
        Get
            Return fRequestType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestType", fRequestType, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fContractExternalClientsId As Integer
    Public Property ContractExternalClientsId() As Integer
        Get
            Return fContractExternalClientsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractExternalClientsId", fContractExternalClientsId, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Registrado', IIF(Status = 2, 'Confirmado', 'Anulado'))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(RequestType = 1, 'Solicitud de Pacientes', IIF(RequestType = 2, 'Solicitud de Maquila', 'Solicitud de Nutrición'))")>
    Public ReadOnly Property RequestTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RequestTypeName"))
        End Get
    End Property

    <Association("MaquilaReferencesCMConfigurationRequestUnitDoseExternalCareCenter", GetType(RequestUnitDoseExternalCareCenterMaquilaXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterMaquilaXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)("RequestUnitDoseExternalCareCenterMaquilaXpo")
        End Get
    End Property

    <Association("PatientReferencesCMConfigurationRequestUnitDoseExternalCareCenter", GetType(RequestUnitDoseExternalCareCenterPatientXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterPatientXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)("RequestUnitDoseExternalCareCenterPatientXpo")
        End Get
    End Property
#End Region

End Class