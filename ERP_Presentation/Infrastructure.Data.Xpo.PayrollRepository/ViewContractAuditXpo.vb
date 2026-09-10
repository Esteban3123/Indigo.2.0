Imports System
Imports DevExpress.Xpo

<Persistent("Payroll.ViewContractAudit")> _
Public Class ViewContractAuditXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    ' Información del Contrato
    Dim fId As Integer
    <Key(True)>
    Public Property Id As Integer
        Get
            Return fId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fContractId As Integer
    Public Property ContractId() As Integer
        Get
            Return fContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fInitialContractNumber As Integer
    Public Property InitialContractNumber() As Integer
        Get
            Return fInitialContractNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialContractNumber", fInitialContractNumber, value)
        End Set
    End Property

    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fContractInitialDate As DateTime?
    Public Property ContractInitialDate() As DateTime?
        Get
            Return fContractInitialDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ContractInitialDate", fContractInitialDate, value)
        End Set
    End Property

    Dim fContractEndingDate As DateTime?
    Public Property ContractEndingDate() As DateTime?
        Get
            Return fContractEndingDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ContractEndingDate", fContractEndingDate, value)
        End Set
    End Property

    Dim fContractStatus As String
    <Size(100)>
    Public Property ContractStatus() As String
        Get
            Return fContractStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractStatus", fContractStatus, value)
        End Set
    End Property
    ' Información de Auditoría
    Dim fType As String
    <Size(100)> _
    Public Property Type() As String
        Get
            Return fType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
        End Set
    End Property

    Dim fFieldName As String
    <Size(100)> _
    Public Property FieldName() As String
        Get
            Return fFieldName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FieldName", fFieldName, value)
        End Set
    End Property

    Dim fValueOld As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property ValueOld() As String
        Get
            Return fValueOld
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ValueOld", fValueOld, value)
        End Set
    End Property

    Dim fValueNew As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property ValueNew() As String
        Get
            Return fValueNew
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ValueNew", fValueNew, value)
        End Set
    End Property

    ' Información del Usuario
    Dim fAuditDate As DateTime
    Public Property [Date]() As DateTime
        Get
            Return fAuditDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("Date", fAuditDate, value)
        End Set
    End Property

    Dim fUserCodeName As String
    <Size(300)>
    Public Property UserCodeName() As String
        Get
            Return fUserCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCodeName", fUserCodeName, value)
        End Set
    End Property
End Class

