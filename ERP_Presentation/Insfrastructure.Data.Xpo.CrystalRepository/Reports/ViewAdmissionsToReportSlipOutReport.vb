Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources

#Region "Structure"

Public Structure StrucKey

    <Persistent("AdmissionCode")> _
    Public Property AdmissionCode As String

    <Persistent("CodeSlipOut")> _
    Public Property CodeSlipOut As String

End Structure

#End Region

<Persistent("dbo.ViewAdmissionsToReportSlipOutReport")> _
Public Class ViewAdmissionsToReportSlipOutReport
    Inherits XPLiteObject

    <Key(), Persistent()> _
    Public Property Key As StrucKey

    Dim fAdmissionCode As String
    Public Property AdmissionCode() As String
        Get
            Return fAdmissionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCode", fAdmissionCode, value)
        End Set
    End Property
    Dim fAdmissionCodeWithOutTrim As String
    <Size(10)> _
    Public Property AdmissionCodeWithOutTrim() As String
        Get
            Return fAdmissionCodeWithOutTrim
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCodeWithOutTrim", fAdmissionCodeWithOutTrim, value)
        End Set
    End Property
    Dim fAdmissionDate As DateTime
    Public Property AdmissionDate() As DateTime
        Get
            Return fAdmissionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property
    Dim fAdmissionType As Integer
    Public Property AdmissionType() As Integer
        Get
            Return fAdmissionType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdmissionType", fAdmissionType, value)
        End Set
    End Property
    <PersistentAlias("AdmissionType")> _
    Public ReadOnly Property AdmissionTypeName() As String
        Get
            Select Case fAdmissionType
                Case 1
                    Return ResourceManager.GetString("AdmissionType1")
                Case 2
                    Return ResourceManager.GetString("AdmissionType2")
                Case Else
                    Return String.Empty
            End Select
        End Get
    End Property
    Dim fAdmissionCaregroupId As Integer
    Public Property AdmissionCaregroupId() As Integer
        Get
            Return fAdmissionCaregroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdmissionCaregroupId", fAdmissionCaregroupId, value)
        End Set
    End Property
    Dim fAdmissionReason As Integer
    Public Property AdmissionReason() As Integer
        Get
            Return fAdmissionReason
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdmissionReason", fAdmissionReason, value)
        End Set
    End Property
    Dim fAdmissionRiskType As Integer
    Public Property AdmissionRiskType() As Integer
        Get
            Return fAdmissionRiskType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdmissionRiskType", fAdmissionRiskType, value)
        End Set
    End Property
    Dim fAdmissionCentAtencCodeName As String
    <Size(113)> _
    Public Property AdmissionCentAtencCodeName() As String
        Get
            Return fAdmissionCentAtencCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionCentAtencCodeName", fAdmissionCentAtencCodeName, value)
        End Set
    End Property
    Dim fAdmissionUniFuncCodeName As String
    <Size(73)> _
    Public Property AdmissionUniFuncCodeName() As String
        Get
            Return fAdmissionUniFuncCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionUniFuncCodeName", fAdmissionUniFuncCodeName, value)
        End Set
    End Property
    Dim fAdmissionUniFuncCode As String
    <Size(10)> _
    Public Property AdmissionUniFuncCode() As String
        Get
            Return fAdmissionUniFuncCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionUniFuncCode", fAdmissionUniFuncCode, value)
        End Set
    End Property
    Dim fAdmissionUniFuncName As String
    <Size(60)> _
    Public Property AdmissionUniFuncName() As String
        Get
            Return fAdmissionUniFuncName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionUniFuncName", fAdmissionUniFuncName, value)
        End Set
    End Property
    Dim fPlaceEntry As Integer
    Public Property PlaceEntry() As Integer
        Get
            Return fPlaceEntry
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PlaceEntry", fPlaceEntry, value)
        End Set
    End Property
    Dim fBenefitPlan As String
    <Size(2)> _
    Public Property BenefitPlan() As String
        Get
            Return fBenefitPlan
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BenefitPlan", fBenefitPlan, value)
        End Set
    End Property
    Dim fAuthorizationNumber As String
    <Size(15)> _
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property
    Dim fResponsiblePhone As String
    <Size(15)> _
    Public Property ResponsiblePhone() As String
        Get
            Return fResponsiblePhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsiblePhone", fResponsiblePhone, value)
        End Set
    End Property
    Dim fResponsibleName As String
    <Size(80)> _
    Public Property ResponsibleName() As String
        Get
            Return fResponsibleName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleName", fResponsibleName, value)
        End Set
    End Property
    Dim fPatientCode As String
    <Size(15)> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property
    Dim fPatientBirth As DateTime
    Public Property PatientBirth() As DateTime
        Get
            Return fPatientBirth
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PatientBirth", fPatientBirth, value)
        End Set
    End Property
    Dim fPatientGenus As Integer
    Public Property PatientGenus() As Integer
        Get
            Return fPatientGenus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientGenus", fPatientGenus, value)
        End Set
    End Property
    Dim fPatientEstrato As Integer
    Public Property PatientEstrato() As Integer
        Get
            Return fPatientEstrato
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientEstrato", fPatientEstrato, value)
        End Set
    End Property
    Dim fPatientType As Integer
    Public Property PatientType() As Integer
        Get
            Return fPatientType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientType", fPatientType, value)
        End Set
    End Property
    Dim fPatientAfiliation As Integer
    Public Property PatientAfiliation() As Integer
        Get
            Return fPatientAfiliation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientAfiliation", fPatientAfiliation, value)
        End Set
    End Property
    Dim fPatientDocumentType As Integer
    Public Property PatientDocumentType() As Integer
        Get
            Return fPatientDocumentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientDocumentType", fPatientDocumentType, value)
        End Set
    End Property
    Dim fNivelCode As String
    <Size(2)> _
    Public Property NivelCode() As String
        Get
            Return fNivelCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NivelCode", fNivelCode, value)
        End Set
    End Property
    Dim fNivelName As String
    Public Property NivelName() As String
        Get
            Return fNivelName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NivelName", fNivelName, value)
        End Set
    End Property
    Dim fNivelModeratorSharePercentage As Decimal
    Public Property NivelModeratorSharePercentage() As Decimal
        Get
            Return fNivelModeratorSharePercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelModeratorSharePercentage", fNivelModeratorSharePercentage, value)
        End Set
    End Property
    Dim fNivelCoPayContribPercentage As Decimal
    Public Property NivelCoPayContribPercentage() As Decimal
        Get
            Return fNivelCoPayContribPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayContribPercentage", fNivelCoPayContribPercentage, value)
        End Set
    End Property
    Dim fNivelCoPaySubsiPercentage As Decimal
    Public Property NivelCoPaySubsiPercentage() As Decimal
        Get
            Return fNivelCoPaySubsiPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPaySubsiPercentage", fNivelCoPaySubsiPercentage, value)
        End Set
    End Property
    Dim fNivelCoPayVincuPercentage As Decimal
    Public Property NivelCoPayVincuPercentage() As Decimal
        Get
            Return fNivelCoPayVincuPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayVincuPercentage", fNivelCoPayVincuPercentage, value)
        End Set
    End Property
    Dim fNivelSisben As Integer
    Public Property NivelSisben() As Integer
        Get
            Return fNivelSisben
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NivelSisben", fNivelSisben, value)
        End Set
    End Property
    Dim fNivelModeratorShareTop As Decimal
    Public Property NivelModeratorShareTop() As Decimal
        Get
            Return fNivelModeratorShareTop
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelModeratorShareTop", fNivelModeratorShareTop, value)
        End Set
    End Property
    Dim fNivelCoPayContribTop As Decimal
    Public Property NivelCoPayContribTop() As Decimal
        Get
            Return fNivelCoPayContribTop
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayContribTop", fNivelCoPayContribTop, value)
        End Set
    End Property
    Dim fNivelCoPaySubsibTop As Decimal
    Public Property NivelCoPaySubsibTop() As Decimal
        Get
            Return fNivelCoPaySubsibTop
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPaySubsibTop", fNivelCoPaySubsibTop, value)
        End Set
    End Property
    Dim fNivelCoPayVincuTop As Decimal
    Public Property NivelCoPayVincuTop() As Decimal
        Get
            Return fNivelCoPayVincuTop
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayVincuTop", fNivelCoPayVincuTop, value)
        End Set
    End Property
    Dim fNivelModeratorShareTopYear As Decimal
    Public Property NivelModeratorShareTopYear() As Decimal
        Get
            Return fNivelModeratorShareTopYear
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelModeratorShareTopYear", fNivelModeratorShareTopYear, value)
        End Set
    End Property
    Dim fNivelCoPayContribTopYear As Decimal
    Public Property NivelCoPayContribTopYear() As Decimal
        Get
            Return fNivelCoPayContribTopYear
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayContribTopYear", fNivelCoPayContribTopYear, value)
        End Set
    End Property
    Dim fNivelCoPaySubsibTopYear As Decimal
    Public Property NivelCoPaySubsibTopYear() As Decimal
        Get
            Return fNivelCoPaySubsibTopYear
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPaySubsibTopYear", fNivelCoPaySubsibTopYear, value)
        End Set
    End Property
    Dim fNivelCoPayVincuTopYear As Decimal
    Public Property NivelCoPayVincuTopYear() As Decimal
        Get
            Return fNivelCoPayVincuTopYear
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NivelCoPayVincuTopYear", fNivelCoPayVincuTopYear, value)
        End Set
    End Property
    Dim fStatus As Char
    Public Property Status() As Char
        Get
            Return fStatus
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Status", fStatus, value)
        End Set
    End Property
    Dim fEntityCode As String
    <Size(20)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property
    Dim fEntityName As String
    <Size(300)> _
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property
    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property
    Dim fPatientEntityCode As String
    <Size(1)> _
    Public Property PatientEntityCode() As String
        Get
            Return fPatientEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientEntityCode", fPatientEntityCode, value)
        End Set
    End Property
    Dim fPatientEntityName As String
    <Size(1)> _
    Public Property PatientEntityName() As String
        Get
            Return fPatientEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientEntityName", fPatientEntityName, value)
        End Set
    End Property
    Dim fPatientEntityId As Integer
    Public Property PatientEntityId() As Integer
        Get
            Return fPatientEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientEntityId", fPatientEntityId, value)
        End Set
    End Property
    Dim fPatientPhone As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property PatientPhone() As String
        Get
            Return fPatientPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientPhone", fPatientPhone, value)
        End Set
    End Property
    Dim fPatientCareGroupId As Integer
    Public Property PatientCareGroupId() As Integer
        Get
            Return fPatientCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientCareGroupId", fPatientCareGroupId, value)
        End Set
    End Property
    Dim fPatientName As String
    <Indexed(Name:="IDX_V3", Unique:=False)> _
    <Size(250)> _
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property
    Dim fBedStay As String
    <Size(10)> _
    Public Property BedStay() As String
        Get
            Return fBedStay
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BedStay", fBedStay, value)
        End Set
    End Property
    Dim fLiquidationType As Integer
    Public Property LiquidationType() As Integer
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    <PersistentAlias("LiquidationType")> _
    Public ReadOnly Property LiquidationTypeName() As String
        Get
            Select Case fLiquidationType
                Case 1
                    Return ResourceManager.GetString("LiquidationType1")
                Case 2
                    Return ResourceManager.GetString("LiquidationType2")
                Case 3
                    Return ResourceManager.GetString("LiquidationType3")
                Case 4
                    Return ResourceManager.GetString("LiquidationType4")
                Case Else
                    Return String.Empty
            End Select
        End Get
    End Property
    Dim fStatusName As String
    <Size(10)> _
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property
    Dim fEgressDate As DateTime
    Public Property EgressDate() As DateTime
        Get
            Return fEgressDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EgressDate", fEgressDate, value)
        End Set
    End Property
    Dim fNameMedic As String
    <Size(60)> _
    Public Property NameMedic() As String
        Get
            Return fNameMedic
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameMedic", fNameMedic, value)
        End Set
    End Property
    Dim fCodeSlipOut As String
    <Size(20)> _
    Public Property CodeSlipOut() As String
        Get
            Return fCodeSlipOut
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSlipOut", fCodeSlipOut, value)
        End Set
    End Property
    Dim fDateRequest As DateTime
    Public Property DateRequest() As DateTime
        Get
            Return fDateRequest
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateRequest", fDateRequest, value)
        End Set
    End Property
    Dim fUserCreate As String
    <Size(20)>
    Public Property UserCreate() As String
        Get
            Return fUserCreate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCreate", fUserCreate, value)
        End Set
    End Property
    Dim fTotalFolio As Decimal
    Public Property TotalFolio() As Decimal
        Get
            Return fTotalFolio
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalFolio", fTotalFolio, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
