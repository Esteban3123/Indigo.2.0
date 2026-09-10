'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05-04-2014
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
<Persistent("Payments.AccountPayable")> _
Public Class AccountPayableXpo
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

    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fEntityId As Integer
    <Persistent("EntityId")>
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fIdCostCenter As Integer
    <Persistent("IdCostCenter")> _
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property

    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("ThidrPartyReferencesAccountPayable")>
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property

    <PersistentAlias("IdThirdParty.NitName")> _
    Public ReadOnly Property ThirdPartyFullName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ThirdPartyFullName"))
        End Get
    End Property

    Dim fStatus As Byte
    <Persistent("Status")> _
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fBillNumber As String
    <Size(100)> _
    <Persistent("BillNumber")> _
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    <Size(100)> _
    <Persistent("DocumentDate")> _
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fBillDate As DateTime
    <Size(100)> _
    <Persistent("BillDate")> _
    Public Property BillDate() As DateTime
        Get
            Return fBillDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("BillDate", fBillDate, value)
        End Set
    End Property

    Dim fExpirationDate As DateTime
    <Size(100)> _
    <Persistent("ExpirationDate")> _
    Public Property ExpirationDate() As DateTime
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Size(100)> _
    <Persistent("Value")> _
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("Value", fValue, value)
        End Set
    End Property

    Dim fBalance As Decimal
    <Size(100)> _
    <Persistent("Balance")> _
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("Balance", fBalance, value)
        End Set
    End Property

    Dim fIdOperatingUnit As Integer
    <Persistent("IdOperatingUnit")> _
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
        End Set
    End Property

    Dim fInvoiceValue As Decimal
    Public Property InvoiceValue() As Decimal
        Get
            Return fInvoiceValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValue", fInvoiceValue, value)
        End Set
    End Property

    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),BillNumber)")>
    Public ReadOnly Property CodeBillNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeBillNumber"))
        End Get
    End Property

    Dim fSelectedItem As Boolean = False
    <NonPersistent()>
    Public Property SelectedItem As Boolean
        Get
            Return fSelectedItem
        End Get
        Set(value As Boolean)
            fSelectedItem = value
        End Set
    End Property

    <Association("GlosaMedicalFeesDetailReferences_AccountPayableXpo", GetType(GlosaMedicalFeesDetailXpo))>
    Public ReadOnly Property GlosaMedicalFeesDetailXpo() As XPCollection(Of GlosaMedicalFeesDetailXpo)
        Get
            Return GetCollection(Of GlosaMedicalFeesDetailXpo)("GlosaMedicalFeesDetailXpo")
        End Get
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
