'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/10/2014
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
<Persistent("Contract.RateManualDetail")> _
Public Class RateManualDetailXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fRateManualId As RateManualXpo
    <Association("RateManualDetailReferencesRateManual")> _
    Public Property RateManualId() As RateManualXpo
        Get
            Return fRateManualId
        End Get
        Set(ByVal value As RateManualXpo)
            SetPropertyValue(Of RateManualXpo)("RateManualId", fRateManualId, value)
        End Set
    End Property

    Dim fIPSServiceId As ContractIPSServiceXPO
    <Association("RateManualDetailReferencesIPSService")>
    Public Property IPSServiceId() As ContractIPSServiceXPO
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXPO)
            SetPropertyValue(Of ContractIPSServiceXPO)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fSurgicalGroupId As SurgicalGroupXpo
    <Association("RateManualDetailReferencesSurgicalGroup")> _
    Public Property SurgicalGroupId() As SurgicalGroupXpo
        Get
            Return fSurgicalGroupId
        End Get
        Set(ByVal value As SurgicalGroupXpo)
            SetPropertyValue(Of SurgicalGroupXpo)("SurgicalGroupId", fSurgicalGroupId, value)
        End Set
    End Property

    Dim fScoreProcedure As Integer
    <Persistent("ScoreProcedure")> _
    Public Property ScoreProcedure() As Integer
        Get
            Return fScoreProcedure
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ScoreProcedure", fScoreProcedure, value)
        End Set
    End Property

    Dim fDiscountPercentage As Decimal
    <Size(100)> _
    <Persistent("DiscountPercentage")> _
    Public Property DiscountPercentage() As Decimal
        Get
            Return fDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("DiscountPercentage", fDiscountPercentage, value)
        End Set
    End Property

    Dim fOutPatientRecoveryFeeType As Integer
    <Persistent("OutPatientRecoveryFeeType")> _
    Public Property OutPatientRecoveryFeeType() As Integer
        Get
            Return fOutPatientRecoveryFeeType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OutPatientRecoveryFeeType", fOutPatientRecoveryFeeType, value)
        End Set
    End Property

    Dim fInPatientRecoveryFeeType As Integer
    <Persistent("InPatientRecoveryFeeType")> _
    Public Property InPatientRecoveryFeeType() As Integer
        Get
            Return fInPatientRecoveryFeeType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InPatientRecoveryFeeType", fInPatientRecoveryFeeType, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property OutPatientRecoveryFeeTypeName As String
        Get
            Return NamePatient(fOutPatientRecoveryFeeType)
        End Get
    End Property

    <NonPersistent()> _
    Public ReadOnly Property InPatientRecoveryFeeTypeName As String
        Get
            Return NamePatient(fInPatientRecoveryFeeType)
        End Get
    End Property

    Private Function NamePatient(id As Integer) As String
        Select Case id
            Case 1
                Return "Ninguna"
            Case 2
                Return "Cuota Moderadora"
            Case 3
                Return "Copago"
            Case 4
                Return "Bono"
            Case 5
                Return "Franquicia"
            Case 6
                Return "Otra"
            Case Else
                Return String.Empty
        End Select
    End Function

    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            'If fStatus = False Then
            '    Return "Inactivo"
            'Else
            '    Return "Activo"
            'End If
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("ServiceFeesReferencesRateManualDetail", GetType(ServiceFeesXpo))> _
    Public ReadOnly Property ServiceFeesXpo() As XPCollection(Of ServiceFeesXpo)
        Get
            Return GetCollection(Of ServiceFeesXpo)("ServiceFeesXpo")
        End Get
    End Property

    Dim fCreationUser As String
    <Size(20)> _
    <Persistent("CreationUser")> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    <Size(100)> _
    <Persistent("CreationDate")> _
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)> _
    <Persistent("ModificationUser")> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    <Size(100)> _
    <Persistent("ModificationDate")> _
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("ModificationDate", fModificationDate, value)
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
