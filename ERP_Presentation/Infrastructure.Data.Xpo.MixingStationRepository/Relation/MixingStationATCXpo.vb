'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Inventory.ATC")>
Partial Public Class MixingStationATCXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    <PersistentAlias("DCI.Id")>
    Public ReadOnly Property DCIId As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("DCIId"))
        End Get
    End Property

    Dim fDCI As InventoryDCIXpo
    <Persistent("DCIId")>
    <Association("ATCReferencesDCI")>
    Public Property DCI() As InventoryDCIXpo
        Get
            Return fDCI
        End Get
        Set(ByVal value As InventoryDCIXpo)
            SetPropertyValue("DCI", fDCI, value)
        End Set
    End Property


    Dim fProductNPT As Boolean
    Public Property ProductNPT() As Boolean
        Get
            Return fProductNPT
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ProductNPT", fProductNPT, value)
        End Set
    End Property

    Dim fFormulationType As Byte
    Public Property FormulationType() As Byte
        Get
            Return fFormulationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("FormulationType", fFormulationType, value)
        End Set
    End Property

    Private fWeight As Decimal
    Public Property Weight() As Decimal
        Get
            Return fWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Weight", fWeight, value)
        End Set
    End Property

    Private fVolume As Decimal
    Public Property Volume() As Decimal
        Get
            Return fVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Volume", fVolume, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("StabilityTableDetailferencesATC", GetType(StabilityTableDetailXpo))>
    Public ReadOnly Property StabilityTableDetailXpo() As XPCollection(Of StabilityTableDetailXpo)
        Get
            Return GetCollection(Of StabilityTableDetailXpo)("StabilityTableDetailXpo")
        End Get
    End Property

    <Association("StabilityTableDetailDilutionferencesATC", GetType(StabilityTableDetailDilutionXpo))>
    Public ReadOnly Property StabilityTableDetailDilutionXpo() As XPCollection(Of StabilityTableDetailDilutionXpo)
        Get
            Return GetCollection(Of StabilityTableDetailDilutionXpo)("StabilityTableDetailDilutionXpo")
        End Get
    End Property

    <Association("StabilityTableDetailReconstitutionferencesATC", GetType(StabilityTableDetailReconstitutionXpo))>
    Public ReadOnly Property StabilityTableDetailReconstitutionXpo() As XPCollection(Of StabilityTableDetailReconstitutionXpo)
        Get
            Return GetCollection(Of StabilityTableDetailReconstitutionXpo)("StabilityTableDetailReconstitutionXpo")
        End Get
    End Property

    <Association("MedicinesProductionReferencesATC", GetType(MedicinesProductionXpo))>
    Public ReadOnly Property MedicinesProductionXpo() As XPCollection(Of MedicinesProductionXpo)
        Get
            Return GetCollection(Of MedicinesProductionXpo)("MedicinesProductionXpo")
        End Get
    End Property

    <Association("PackageDetailReferencesATC", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailXpo")
        End Get
    End Property

    <Association("PackagePersonalizedDetailReferencesATC", GetType(PackagePersonalizedDetailXpo))>
    Public ReadOnly Property PackagePersonalizedDetailXpo() As XPCollection(Of PackagePersonalizedDetailXpo)
        Get
            Return GetCollection(Of PackagePersonalizedDetailXpo)("PackagePersonalizedDetailXpo")
        End Get
    End Property

    <Association("RequestUnitDoseInventoryDetailReferencesATC", GetType(RequestUnitDoseInventoryDetailXpo))>
    Public ReadOnly Property RequestUnitDoseInventoryDetailXpo() As XPCollection(Of RequestUnitDoseInventoryDetailXpo)
        Get
            Return GetCollection(Of RequestUnitDoseInventoryDetailXpo)("RequestUnitDoseInventoryDetailXpo")
        End Get
    End Property

    <Association("MaquilaReferencesATC", GetType(RequestUnitDoseExternalCareCenterMaquilaXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterMaquilaXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)("RequestUnitDoseExternalCareCenterMaquilaXpo")
        End Get
    End Property

    <Association("DilutionFactors_References_ATC", GetType(DilutionFactorsXpo))>
    Public ReadOnly Property DilutionFactorsXpo() As XPCollection(Of DilutionFactorsXpo)
        Get
            Return GetCollection(Of DilutionFactorsXpo)("DilutionFactorsXpo")
        End Get
    End Property

    <Association("DilutionFactorsDetail_References_ATC", GetType(DilutionFactorsDetailXpo))>
    Public ReadOnly Property DilutionFactorsDetailXpo() As XPCollection(Of DilutionFactorsDetailXpo)
        Get
            Return GetCollection(Of DilutionFactorsDetailXpo)("DilutionFactorsDetailXpo")
        End Get
    End Property

    <Association("ExternalPatientPreparation_Reference_ATC", GetType(ExternalPatientPreparationDetailXpo))>
    Public ReadOnly Property ExternalPatientPreparationDetailsXpo() As XPCollection(Of ExternalPatientPreparationDetailXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationDetailXpo)("ExternalPatientPreparationDetailsXpo")
        End Get
    End Property

    <Association("ATCAdministrationRouteReferencesATC", GetType(MixingStationATCAdministrationRouteXpo))>
    Public ReadOnly Property MixingStationATCAdministrationRouteXpo() As XPCollection(Of MixingStationATCAdministrationRouteXpo)
        Get
            Return GetCollection(Of MixingStationATCAdministrationRouteXpo)("MixingStationATCAdministrationRouteXpo")
        End Get
    End Property

End Class