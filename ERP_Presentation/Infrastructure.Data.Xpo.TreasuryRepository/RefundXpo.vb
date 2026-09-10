'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
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
<Persistent("Treasury.Refunds")> _
Public Class RefundXpo
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
    Dim fIdCashRegister As CashRegisterXpo
    <Association("Treasury_RefundsReferencesTreasury_CashRegisters")>
    Public Property IdCashRegister() As CashRegisterXpo
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As CashRegisterXpo)
            SetPropertyValue(Of CashRegisterXpo)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property

    Dim fFilingUnitId As FilingUnitXpo
    <Association("RefundReferencesFilingUnit")>
    Public Property FilingUnitId() As FilingUnitXpo
        Get
            Return fFilingUnitId
        End Get
        Set(ByVal value As FilingUnitXpo)
            SetPropertyValue(Of FilingUnitXpo)("FilingUnitId", fFilingUnitId, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    <Persistent("InitialDate")> _
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fFinalDate As DateTime
    <Persistent("FinalDate")>
    Public Property FinalDate() As DateTime
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinalDate", fFinalDate, value)
        End Set
    End Property

    Dim fValue As Decimal
    <Persistent("Value")>
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fRefunded As Boolean
    <Persistent("Refunded")> _
    Public Property Refunded() As Boolean
        Get
            Return fRefunded
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Refunded", fRefunded, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")> _
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property
    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, Iif(Refunded = 1, 'Reembolsado', 'Confirmado'), Iif(Status = 3, 'Anulado', '' )))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("IdCashRegister.CurrencyAbbreviation")>
    Public ReadOnly Property CurrencyAbbreviation()
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    'p.Properties.Mask.Culture = ConfigurationFile.Instance.Culture
    'p.Properties.Mask.EditMask = "c0"
    'p.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    'p.Properties.Appearance.Options.UseTextOptions = True
    'p.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    'p.Properties.Mask.UseMaskAsDisplayFormat = True

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
