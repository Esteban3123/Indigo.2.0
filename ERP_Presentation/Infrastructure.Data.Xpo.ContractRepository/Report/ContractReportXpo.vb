Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.Contract")> _
Public Class ContractReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fContractEntityId As ContractEntityReportXpo
    <Association("Contract_ContractReferencesContract_ContractEntity")> _
    Public Property ContractEntityId() As ContractEntityReportXpo
        Get
            Return fContractEntityId
        End Get
        Set(ByVal value As ContractEntityReportXpo)
            SetPropertyValue(Of ContractEntityReportXpo)("ContractEntityId", fContractEntityId, value)
        End Set
    End Property
    Dim fHealthAdministratorId As HealthAdministratorReportXpo
    <Association("Contract_ContractReferencesContract_HealthAdministrator")> _
    Public Property HealthAdministratorId() As HealthAdministratorReportXpo
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As HealthAdministratorReportXpo)
            SetPropertyValue(Of HealthAdministratorReportXpo)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_Contract", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    <PersistentAlias("concat(concat(Code,' - '),ContractName)")>
    Public ReadOnly Property CodeContractName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeContractName"))
        End Get
    End Property

    Dim fContractNumber As String
    <Size(30)> _
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property
    Dim fContractValue As Decimal
    Public Property ContractValue() As Decimal
        Get
            Return fContractValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ContractValue", fContractValue, value)
        End Set
    End Property
    Dim fExecuteValue As Decimal
    Public Property ExecuteValue() As Decimal
        Get
            Return fExecuteValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExecuteValue", fExecuteValue, value)
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
    Dim fObservations As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fContractObject As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property ContractObject() As String
        Get
            Return fContractObject
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractObject", fContractObject, value)
        End Set
    End Property
    Dim fPrintingMode As Byte
    Public Property PrintingMode() As Byte
        Get
            Return fPrintingMode
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PrintingMode", fPrintingMode, value)
        End Set
    End Property
    Dim fTerminationControl As Byte
    Public Property TerminationControl() As Byte
        Get
            Return fTerminationControl
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TerminationControl", fTerminationControl, value)
        End Set
    End Property
    Dim fNotificationValueType As Byte
    Public Property NotificationValueType() As Byte
        Get
            Return fNotificationValueType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("NotificationValueType", fNotificationValueType, value)
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
    Dim fNotificationTimeType As Byte
    Public Property NotificationTimeType() As Byte
        Get
            Return fNotificationTimeType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("NotificationTimeType", fNotificationTimeType, value)
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
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fInForceUser As String
    <Size(20)> _
    Public Property InForceUser() As String
        Get
            Return fInForceUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InForceUser", fInForceUser, value)
        End Set
    End Property
    Dim fInForceDate As DateTime
    Public Property InForceDate() As DateTime
        Get
            Return fInForceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InForceDate", fInForceDate, value)
        End Set
    End Property
    Dim fSuspendedUser As String
    <Size(20)> _
    Public Property SuspendedUser() As String
        Get
            Return fSuspendedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SuspendedUser", fSuspendedUser, value)
        End Set
    End Property
    Dim fSuspendedDate As DateTime
    Public Property SuspendedDate() As DateTime
        Get
            Return fSuspendedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("SuspendedDate", fSuspendedDate, value)
        End Set
    End Property
    Dim fFinishedUser As String
    <Size(20)> _
    Public Property FinishedUser() As String
        Get
            Return fFinishedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinishedUser", fFinishedUser, value)
        End Set
    End Property
    Dim fFinishedDate As DateTime
    Public Property FinishedDate() As DateTime
        Get
            Return fFinishedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinishedDate", fFinishedDate, value)
        End Set
    End Property

    <Association("Contract_CareGroupReferencesContract_Contract", GetType(ContractCareGroupReportXpo))> _
    Public ReadOnly Property Contract_CareGroup() As XPCollection(Of ContractCareGroupReportXpo)
        Get
            Return GetCollection(Of ContractCareGroupReportXpo)("Contract_CareGroup")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
