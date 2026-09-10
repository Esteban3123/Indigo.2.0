Imports DevExpress.Xpo

<Persistent("Payroll.ViewElectronicPayroll")>
Public Class ElectronicPayrollXpo
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

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Dim fMonth As Byte
    Public Property Month() As Byte
        Get
            Return fMonth
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Month", fMonth, value)
        End Set
    End Property

    Dim fPeriod As String
    Public Property Period() As String
        Get
            Return fPeriod
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Period", fPeriod, value)
        End Set
    End Property

    Dim fEmployeePartyId As Integer
    Public Property EmployeePartyId() As Integer
        Get
            Return fEmployeePartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeePartyId", fEmployeePartyId, value)
        End Set
    End Property

    Dim fEmployeePartyNitName As String
    Public Property EmployeePartyNitName() As String
        Get
            Return fEmployeePartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeePartyNitName", fEmployeePartyNitName, value)
        End Set
    End Property



    Dim fPrefix As String
    Public Property Prefix() As String
        Get
            Return fPrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Prefix", fPrefix, value)
        End Set
    End Property

    Dim fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property

    Dim fCUNE As String
    Public Property CUNE() As String
        Get
            Return fCUNE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUNE", fCUNE, value)
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

    Dim fShippingDate As DateTime
    Public Property ShippingDate() As DateTime
        Get
            Return fShippingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ShippingDate", fShippingDate, value)
        End Set
    End Property

    Dim fValidationDate As DateTime
    Public Property ValidationDate() As DateTime
        Get
            Return fValidationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ValidationDate", fValidationDate, value)
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

    Dim fMessage As String
    Public Property Message() As String
        Get
            Return fMessage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Message", fMessage, value)
        End Set
    End Property

#End Region

#Region "Attributes Extends"

    <PersistentAlias("Iif(Status = 0, 'Erróneo', Status = 1, 'Registrado', Status = 2, 'Enviado', Status = 3, 'Valido', Status = 4, 'Invalido', 'Procesando')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(
DocumentType = 1, 'Soporte de pago de nomina electronica', 
DocumentType = 2, 'Nota de Ajuste', 
'')")>
    Public ReadOnly Property DocumentTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentTypeName"))
        End Get
    End Property

    <PersistentAlias("Concat(Prefix, DocumentNumber)")>
    Public ReadOnly Property DocumentNumberWithPrefix As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentNumberWithPrefix"))
        End Get
    End Property

    <PersistentAlias("Iif(Message IS NULL, 0, 1)")>
    Public ReadOnly Property Alert As Boolean
        Get
            Return Convert.ToBoolean(Me.EvaluateAlias("Alert"))
        End Get
    End Property

#End Region

#Region "Navigation"

    <Association("ElectronicPayrollDetail_References_ElectronicPayroll", GetType(ElectronicPayrollDetailXpo))>
    Public ReadOnly Property ElectronicPayrollDetails() As XPCollection(Of ElectronicPayrollDetailXpo)
        Get
            Return GetCollection(Of ElectronicPayrollDetailXpo)("ElectronicPayrollDetails")
        End Get
    End Property

    <Association("ElectronicPayrollNotification_References_ElectronicPayroll", GetType(ElectronicPayrollNotificationXpo))>
    Public ReadOnly Property ElectronicPayrollNotifications() As XPCollection(Of ElectronicPayrollNotificationXpo)
        Get
            Return GetCollection(Of ElectronicPayrollNotificationXpo)("ElectronicPayrollNotifications")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class