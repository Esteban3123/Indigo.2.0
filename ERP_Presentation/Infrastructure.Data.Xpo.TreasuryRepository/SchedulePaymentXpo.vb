'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-09-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.SchedulePayment")>
Public Class SchedulePaymentXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fScheduledDate As Date
    <Persistent("ScheduledDate")>
    Public Property ScheduledDate() As Date
        Get
            Return fScheduledDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ScheduledDate", fScheduledDate, value)
        End Set
    End Property

    Dim fEntityBankAccountId As Integer
    <Persistent("EntityBankAccountId")>
    Public Property EntityBankAccountId() As Integer
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property
    Dim fPaymentMethod As Byte
    <Persistent("PaymentMethod")>
    Public Property PaymentMethod() As Byte
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property
    Dim fTaxByMil As Boolean
    <Persistent("TaxByMil")>
    Public Property TaxByMil() As Boolean
        Get
            Return fTaxByMil
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("TaxByMil", fTaxByMil, value)
        End Set
    End Property
    Dim fStatus As Byte
    <Persistent("Status")>
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', Iif(Status = 4, 'Generado Parcial', Iif(Status = 5, 'Generado Total', '')))))")>
    Public ReadOnly Property StatusName() As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return ResourceManager.GetString("StateUnconfirmed")
            '    Case 2
            '        Return ResourceManager.GetString("StateConfirmed")
            '    Case 3
            '        Return ResourceManager.GetString("StatusCanceled")
            '    Case 4
            '        Return ResourceManager.GetString("StatePartialPaid")
            '    Case 5
            '        Return ResourceManager.GetString("StateFullPaid")
            'End Select
            'Return ""
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Sin Generar',Iif(Status = 3, 'Anulado', Iif(Status = 4, 'Generado Parcial', Iif(Status = 5, 'Generado Total', '')))))")>
    Public ReadOnly Property StatusNameDispersionFund() As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return ResourceManager.GetString("StateUnconfirmed")
            '    Case 2
            '        Return ResourceManager.GetString("StateUnGenetated")
            '    Case 3
            '        Return ResourceManager.GetString("StatusCanceled")
            '    Case 4
            '        Return ResourceManager.GetString("StatePartialPaid")
            '    Case 5
            '        Return ResourceManager.GetString("StateFullPaid")
            'End Select
            'Return ""
            Return Convert.ToString(Me.EvaluateAlias("StatusNameDispersionFund"))
        End Get
    End Property

    <Association("SchedulePaymentDetailXpoReferencesSchedulePaymentXpo", GetType(SchedulePaymentDetailXpo))>
    Public ReadOnly Property SchedulePaymentDetailXpo() As XPCollection(Of SchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of SchedulePaymentDetailXpo)("SchedulePaymentDetailXpo")
        End Get
    End Property

    <PersistentAlias("SchedulePaymentDetailXpo.Sum(AmountPaid)")>
    Public ReadOnly Property AmountPaid() As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("AmountPaid"))
        End Get
    End Property

    <NonPersistent()>
    Public ReadOnly Property AmountPaidByCurrency() As String
        Get
            Dim result = (From item In SchedulePaymentDetailXpo.ToList()
                          Group By item.CurrencyAbbreviation Into Group
                          Select SumCurrency = Utils.GetMoneyWithISO4217(Group.Sum(Function(f) f.AmountPaid), CurrencyAbbreviation))

            Return If(result Is Nothing, "", String.Join(" - ", result))
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