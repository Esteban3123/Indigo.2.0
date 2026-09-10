'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-11-16
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewDefectClassificationToReport")>
Partial Public Class ViewDefectClassificationToReportXpo
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

    Dim fDefectClassificationGroupId As Integer
    Public Property DefectClassificationGroupId() As Integer
        Get
            Return fDefectClassificationGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DefectClassificationGroupId", fDefectClassificationGroupId, value)
        End Set
    End Property

    Dim fDefectClassificationGroupName As String
    Public Property DefectClassificationGroupName() As String
        Get
            Return fDefectClassificationGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DefectClassificationGroupName", fDefectClassificationGroupName, value)
        End Set
    End Property

    Dim fDefectClassificationGroupWeight As Short
    Public Property DefectClassificationGroupWeight() As Short
        Get
            Return fDefectClassificationGroupWeight
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("DefectClassificationGroupWeight", fDefectClassificationGroupWeight, value)
        End Set
    End Property

    Dim fItemCode As String
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCode", fItemCode, value)
        End Set
    End Property

    Dim fItemDescription As String
    Public Property ItemDescription() As String
        Get
            Return fItemDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemDescription", fItemDescription, value)
        End Set
    End Property

    Dim fItemWeight As String
    Public Property ItemWeight() As String
        Get
            Return fItemWeight
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemWeight", fItemWeight, value)
        End Set
    End Property

    Dim fCritical As Boolean
    Public Property Critical() As Boolean
        Get
            Return fCritical
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Critical", fCritical, value)
        End Set
    End Property

    Dim fLess As Boolean
    Public Property Less() As Boolean
        Get
            Return fLess
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Less", fLess, value)
        End Set
    End Property

    Dim fProduction As Integer
    Public Property Production() As Integer
        Get
            Return fProduction
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Production", fProduction, value)
        End Set
    End Property

    Dim fQuality As Integer
    Public Property Quality() As Integer
        Get
            Return fQuality
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quality", fQuality, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fUserSupervisor As String
    Public Property UserSupervisor() As String
        Get
            Return fUserSupervisor
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserSupervisor", fUserSupervisor, value)
        End Set
    End Property

    Private fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fTheoreticalWeight As Decimal
    Public Property TheoreticalWeight() As Decimal
        Get
            Return fTheoreticalWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TheoreticalWeight", fTheoreticalWeight, value)
        End Set
    End Property

    Dim fInputsWeight As Decimal
    Public Property InputsWeight() As Decimal
        Get
            Return fInputsWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InputsWeight", fInputsWeight, value)
        End Set
    End Property

    Dim fTheoreticalAndInputsWeight As Decimal
    Public Property TheoreticalAndInputsWeight() As Decimal
        Get
            Return fTheoreticalAndInputsWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TheoreticalAndInputsWeight", fTheoreticalAndInputsWeight, value)
        End Set
    End Property

    Dim fMaximunWeight As Decimal
    Public Property MaximunWeight() As Decimal
        Get
            Return fMaximunWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximunWeight", fMaximunWeight, value)
        End Set
    End Property

    Dim fMinimunWeight As Decimal
    Public Property MinimunWeight() As Decimal
        Get
            Return fMinimunWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MinimunWeight", fMinimunWeight, value)
        End Set
    End Property

    Dim fActualWeight As Decimal
    Public Property ActualWeight() As Decimal
        Get
            Return fActualWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualWeight", fActualWeight, value)
        End Set
    End Property

    Dim fValidationResult As Boolean
    Public Property ValidationResult() As Boolean
        Get
            Return fValidationResult
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ValidationResult", fValidationResult, value)
        End Set
    End Property

    Dim fUnitDoseTypeMSClass As Integer
    Public Property UnitDoseTypeMSClass() As Integer
        Get
            Return fUnitDoseTypeMSClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeMSClass", fUnitDoseTypeMSClass, value)
        End Set
    End Property

    Private fBatchCodes As String
    Public Property BatchCodes() As String
        Get
            Return fBatchCodes
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCodes", fBatchCodes, value)
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
End Class