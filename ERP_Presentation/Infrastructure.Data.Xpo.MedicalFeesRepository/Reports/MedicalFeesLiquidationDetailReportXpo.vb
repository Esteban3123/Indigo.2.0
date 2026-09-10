Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("MedicalFees.MedicalFeesLiquidationDetail")> _
Public Class MedicalFeesLiquidationDetailReportXpo
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
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fMedicalFeesLiquidacionId As MedicalFeesLiquidationReportXpo
    <Association("MedicalFees_MedicalFeesLiquidationDetailReferencesMedicalFees_MedicalFeesLiquidation")> _
    Public Property MedicalFeesLiquidacionId() As MedicalFeesLiquidationReportXpo
        Get
            Return fMedicalFeesLiquidacionId
        End Get
        Set(ByVal value As MedicalFeesLiquidationReportXpo)
            SetPropertyValue(Of MedicalFeesLiquidationReportXpo)("MedicalFeesLiquidacionId", fMedicalFeesLiquidacionId, value)
        End Set
    End Property
    Dim fMedicalFeesCausationId As MedicalFeesCausationReportXpo
    <Association("MedicalFees_MedicalFeesLiquidationDetailReferencesMedicalFees_MedicalFeesCausation")> _
    Public Property MedicalFeesCausationId() As MedicalFeesCausationReportXpo
        Get
            Return fMedicalFeesCausationId
        End Get
        Set(ByVal value As MedicalFeesCausationReportXpo)
            SetPropertyValue(Of MedicalFeesCausationReportXpo)("MedicalFeesCausationId", fMedicalFeesCausationId, value)
        End Set
    End Property
    'Campo añadido Número Cuenta Contable
    Dim fAccountNumber As String
    <NonPersistent()> _
    Public Property AccountNumber() As String
        Get
            Return fAccountNumber
        End Get
        Set(ByVal value As String)
            Me.fAccountNumber = value
        End Set
    End Property
    'Campo añadido Nombre Cuenta Contable
    Dim fAccountName As String
    <NonPersistent()> _
    Public Property AccountName() As String
        Get
            Return fAccountName
        End Get
        Set(ByVal value As String)
            Me.fAccountName = value
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
