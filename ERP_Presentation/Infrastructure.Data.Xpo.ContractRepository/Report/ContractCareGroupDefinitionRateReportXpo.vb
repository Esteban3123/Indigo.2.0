Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.CareGroupDefinitionRate")> _
Public Class ContractCareGroupDefinitionRateReportXpo
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
    Dim fCareGroupId As ContractCareGroupReportXpo
    <Association("Contract_CareGroupDefinitionRateReferencesContract_CareGroup")> _
    Public Property CareGroupId() As ContractCareGroupReportXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As ContractCareGroupReportXpo)
            SetPropertyValue(Of ContractCareGroupReportXpo)("CareGroupId", fCareGroupId, value)
        End Set
    End Property
    Dim fDefinitionRateId As ContractDefinitionRateReportXpo
    <Association("Contract_CareGroupDefinitionRateReferencesContract_DefinitionRate")> _
    Public Property DefinitionRateId() As ContractDefinitionRateReportXpo
        Get
            Return fDefinitionRateId
        End Get
        Set(ByVal value As ContractDefinitionRateReportXpo)
            SetPropertyValue(Of ContractDefinitionRateReportXpo)("DefinitionRateId", fDefinitionRateId, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
