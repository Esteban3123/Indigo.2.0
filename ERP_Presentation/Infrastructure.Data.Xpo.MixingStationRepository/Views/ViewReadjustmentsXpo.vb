'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Giovanny Plazas Lozano
' Created          : 29/08/2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewReadjustments")>
Partial Public Class ViewReadjustmentsXpo
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

    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fNumberDocument As String
    Public Property NumberDocument() As String
        Get
            Return fNumberDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberDocument", fNumberDocument, value)
        End Set
    End Property

    Dim fNamePacient As String
    Public Property NamePacient() As String
        Get
            Return fNamePacient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NamePacient", fNamePacient, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
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

    Dim fExpirationDate As DateTime?
    Public Property ExpirationDate() As DateTime?
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ExpirationDate", fExpirationDate, value)
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

    Dim fPackageDescription As String
    Public Property PackageDescription() As String
        Get
            Return fPackageDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageDescription", fPackageDescription, value)
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

    Dim fUnitDoseClass As Integer
    Public Property UnitDoseClass() As Integer
        Get
            Return fUnitDoseClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseClass", fUnitDoseClass, value)
        End Set
    End Property

    Dim fNumber As Integer
    Public Property Number() As Integer
        Get
            Return fNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Number", fNumber, value)
        End Set
    End Property

    Dim fSendTo As Byte
    Public Property SendTo() As Byte
        Get
            Return fSendTo
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SendTo", fSendTo, value)
        End Set
    End Property

    Dim fCMConfigurationId As Integer
    Public Property CMConfigurationId() As Integer
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fProductionLineId As Integer
    Public Property ProductionLineId() As Integer
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionLineId", fProductionLineId, value)
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

    Dim fIsReadjustment As Boolean
    Public Property IsReadjustment() As Boolean
        Get
            Return fIsReadjustment
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsReadjustment", fIsReadjustment, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fReasonDevolution As String
    Public Property ReasonDevolution() As String
        Get
            Return fReasonDevolution
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReasonDevolution", fReasonDevolution, value)
        End Set
    End Property

    Dim fDevolutionObservations As String
    Public Property DevolutionObservations() As String
        Get
            Return fDevolutionObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DevolutionObservations", fDevolutionObservations, value)
        End Set
    End Property

    Dim fOperatingUnitDescription As String
    Public Property OperatingUnitDescription() As String
        Get
            Return fOperatingUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OperatingUnitDescription", fOperatingUnitDescription, value)
        End Set
    End Property

    Dim fFunctionalUnitDescription As String
    Public Property FunctionalUnitDescription() As String
        Get
            Return fFunctionalUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitDescription", fFunctionalUnitDescription, value)
        End Set
    End Property

    Dim fFunctionalUnitCode As String
    Public Property FunctionalUnitCode() As String
        Get
            Return fFunctionalUnitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCode", fFunctionalUnitCode, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
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


    Dim fBatchCodeOriginal As String
    Public Property BatchCodeOriginal() As String
        Get
            Return fBatchCodeOriginal
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCodeOriginal", fBatchCodeOriginal, value)
        End Set
    End Property

    Dim fCreationUser As String
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
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime?
    Public Property ModificationDate() As DateTime?
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ModificationDate", fModificationDate, value)
        End Set
    End Property

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

    Dim fTechnicalConceptDate As DateTime?
    Public Property TechnicalConceptDate() As DateTime?
        Get
            Return fTechnicalConceptDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("TechnicalConceptDate", fTechnicalConceptDate, value)
        End Set
    End Property

    Dim fPackageId As Integer?
    Public Property PackageId() As Integer?
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PackageId", fPackageId, value)
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