Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("MedicalFees.ViewListMedicalFeesLiquidationReport")> _
Public Class ViewMedicalFeesReport
    Inherits XPLiteObject
    Dim fRow As Integer
    <Key(True)> _
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property
    Dim fId As Integer
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fHealthProfessionalCode As String
    <Size(20)> _
    Public Property HealthProfessionalCode() As String
        Get
            Return fHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthProfessionalCode", fHealthProfessionalCode, value)
        End Set
    End Property
    Dim fTercero As String
    <Size(318)> _
    Public Property Tercero() As String
        Get
            Return fTercero
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tercero", fTercero, value)
        End Set
    End Property
    Dim fTerceroCausation As String
    <Size(318)> _
    Public Property TerceroCausation() As String
        Get
            Return fTerceroCausation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TerceroCausation", fTerceroCausation, value)
        End Set
    End Property
    Dim fContratoAgremiacion As String
    <Size(300)> _
    Public Property ContratoAgremiacion() As String
        Get
            Return fContratoAgremiacion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContratoAgremiacion", fContratoAgremiacion, value)
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
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fTipoLiquidation As Byte
    Public Property TipoLiquidation() As Byte
        Get
            Return fTipoLiquidation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TipoLiquidation", fTipoLiquidation, value)
        End Set
    End Property
    Dim fUnitCode As String
    <Size(20)> _
    Public Property UnitCode() As String
        Get
            Return fUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitCode", fUnitCode, value)
        End Set
    End Property
    Dim fUnitName As String
    <Size(50)> _
    Public Property UnitName() As String
        Get
            Return fUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitName", fUnitName, value)
        End Set
    End Property
    Dim fCostCode As String
    <Size(20)> _
    Public Property CostCode() As String
        Get
            Return fCostCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCode", fCostCode, value)
        End Set
    End Property
    Dim fCostName As String
    <Size(200)> _
    Public Property CostName() As String
        Get
            Return fCostName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostName", fCostName, value)
        End Set
    End Property
    Dim fAdmissionNumber As String
    <Size(10)> _
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property
    Dim fPatientDescription As String
    <Size(268)> _
    Public Property PatientDescription() As String
        Get
            Return fPatientDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientDescription", fPatientDescription, value)
        End Set
    End Property
    Dim fServiceCode As String
    <Size(20)> _
    Public Property ServiceCode() As String
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
        End Set
    End Property
    Dim fServiceName As String
    <Size(300)> _
    Public Property ServiceName() As String
        Get
            Return fServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceName", fServiceName, value)
        End Set
    End Property
    Dim fCupsCode As String
    <Size(20)> _
    Public Property CupsCode() As String
        Get
            Return fCupsCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsCode", fCupsCode, value)
        End Set
    End Property
    Dim fCupsDescripcion As String
    <Size(300)> _
    Public Property CupsDescripcion() As String
        Get
            Return fCupsDescripcion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsDescripcion", fCupsDescripcion, value)
        End Set
    End Property
    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property
    Dim fCausaTotal As Decimal
    Public Property CausaTotal() As Decimal
        Get
            Return fCausaTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CausaTotal", fCausaTotal, value)
        End Set
    End Property
    Dim fUserPrint As String
    <Size(20)> _
    Public Property UserPrint() As String
        Get
            Return fUserPrint
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserPrint", fUserPrint, value)
        End Set
    End Property
    Dim fNumberAccount As String
    <Size(50)> _
    Public Property NumberAccount() As String
        Get
            Return fNumberAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberAccount", fNumberAccount, value)
        End Set
    End Property
    Dim fNameAccount As String
    <Size(100)> _
    Public Property NameAccount() As String
        Get
            Return fNameAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameAccount", fNameAccount, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
