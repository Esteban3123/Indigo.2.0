'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 06/08/2024
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewReadjustmentsToBeAssigned")>
Partial Public Class ViewReadjustmentsToBeAssignedXpo
    Inherits XPLiteObject

#Region "Builder"
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

    Dim fIsReadjustment As Boolean
    Public Property IsReadjustment() As Boolean
        Get
            Return fIsReadjustment
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsReadjustment", fIsReadjustment, value)
        End Set
    End Property

    Dim fRequestPackageDetailStatusId As Integer
    Public Property RequestPackageDetailStatusId() As Integer
        Get
            Return fRequestPackageDetailStatusId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestPackageDetailStatusId", fRequestPackageDetailStatusId, value)
        End Set
    End Property

    Dim fProductCode As String
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property

    Dim fProductName As String
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fTechnicalConceptDate As DateTime?
    Public Property TechnicalConceptDate() As DateTime?
        Get
            Return fTechnicalConceptDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("TechnicalConceptDate", fTechnicalConceptDate, value)
        End Set
    End Property

    Dim fDosageDescription As String
    Public Property DosageDescription() As String
        Get
            Return fDosageDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DosageDescription", fDosageDescription, value)
        End Set
    End Property

    Dim fStandardPackageId As Integer?
    Public Property StandardPackageId() As Integer?
        Get
            Return fStandardPackageId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("StandardPackageId", fStandardPackageId, value)
        End Set
    End Property

    Dim fAtcMainMedicine As Integer?
    Public Property AtcMainMedicine() As Integer?
        Get
            Return fAtcMainMedicine
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcMainMedicine", fAtcMainMedicine, value)
        End Set
    End Property

    Dim fAtcVehicle As Integer?
    Public Property AtcVehicle() As Integer?
        Get
            Return fAtcVehicle
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcVehicle", fAtcVehicle, value)
        End Set
    End Property

    Dim fAtcThinner As Integer?
    Public Property AtcThinner() As Integer?
        Get
            Return fAtcThinner
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcThinner", fAtcThinner, value)
        End Set
    End Property

    Dim fQuantityMainMedicine As Decimal?
    Public Property QuantityMainMedicine() As Decimal?
        Get
            Return fQuantityMainMedicine
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("QuantityMainMedicine", fQuantityMainMedicine, value)
        End Set
    End Property

    Dim fQuantityVehicle As Decimal?
    Public Property QuantityVehicle() As Decimal?
        Get
            Return fQuantityVehicle
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("QuantityVehicle", fQuantityVehicle, value)
        End Set
    End Property

    Dim fQuantityThinner As Decimal?
    Public Property QuantityThinner() As Decimal?
        Get
            Return fQuantityThinner
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("QuantityThinner", fQuantityThinner, value)
        End Set
    End Property

    Dim fMeasurementUnitIdMainMedicine As Integer?
    Public Property MeasurementUnitIdMainMedicine() As Integer?
        Get
            Return fMeasurementUnitIdMainMedicine
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MeasurementUnitIdMainMedicine", fMeasurementUnitIdMainMedicine, value)
        End Set
    End Property

    Dim fMeasurementUnitIdVehicle As Integer?
    Public Property MeasurementUnitIdVehicle() As Integer?
        Get
            Return fMeasurementUnitIdVehicle
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MeasurementUnitIdVehicle", fMeasurementUnitIdVehicle, value)
        End Set
    End Property

    Dim fMeasurementUnitIdThinner As Integer?
    Public Property MeasurementUnitIdThinner() As Integer?
        Get
            Return fMeasurementUnitIdThinner
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MeasurementUnitIdThinner", fMeasurementUnitIdThinner, value)
        End Set
    End Property

    Dim fExpirationDate As DateTime?
    Public Property ExpirationDate() As DateTime?
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ExpirationDate", fExpirationDate, value)
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

    '-----------------------------------------------------------

    Dim fConcentration As Integer?
    Public Property Concentration() As Integer?
        Get
            Return fConcentration
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fConcentrationMeasurementUnitId As Integer?
    Public Property ConcentrationMeasurementUnitId() As Integer?
        Get
            Return fConcentrationMeasurementUnitId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ConcentrationMeasurementUnitId", fConcentrationMeasurementUnitId, value)
        End Set
    End Property

    Dim fVolumeTotalOrder As Decimal?
    Public Property VolumeTotalOrder() As Decimal?
        Get
            Return fVolumeTotalOrder
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("VolumeTotalOrder", fVolumeTotalOrder, value)
        End Set
    End Property

    Dim fVolumeTotalOrderMeasurementUnitId As Integer?
    Public Property VolumeTotalOrderMeasurementUnitId() As Integer?
        Get
            Return fVolumeTotalOrderMeasurementUnitId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("VolumeTotalOrderMeasurementUnitId", fVolumeTotalOrderMeasurementUnitId, value)
        End Set
    End Property

#End Region

#Region "PersistentAlias"

    <PersistentAlias("Concat(ProductCode,'-',ProductName)")>
    Public ReadOnly Property ProductCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ProductCodeName"))
        End Get
    End Property
#End Region

End Class