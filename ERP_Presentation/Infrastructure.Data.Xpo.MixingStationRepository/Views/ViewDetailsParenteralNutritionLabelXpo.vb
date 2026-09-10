'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2025-05-12
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewDetailsParenteralNutritionLabel")>
Partial Public Class ViewDetailsParenteralNutritionLabelXpo
    Inherits XPLiteObject

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fParenteralNutritionId As ViewMainInformationParenteralNutritionLabelXpo
    <Association("ViewMainInformationParenteralNutritionLabel_References_ViewDetailsParenteralNutritionLabel")>
    Public Property ParenteralNutritionId() As ViewMainInformationParenteralNutritionLabelXpo
        Get
            Return fParenteralNutritionId
        End Get
        Set(ByVal value As ViewMainInformationParenteralNutritionLabelXpo)
            SetPropertyValue(Of ViewMainInformationParenteralNutritionLabelXpo)("ParenteralNutritionId", fParenteralNutritionId, value)
        End Set
    End Property

    Dim fAbbreviationAtc As String
    Public Property AbbreviationAtc() As String
        Get
            Return fAbbreviationAtc
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AbbreviationAtc", fAbbreviationAtc, value)
        End Set
    End Property

    Dim fVolume As Decimal
    Public Property Volume() As Decimal
        Get
            Return fVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Volume", fVolume, value)
        End Set
    End Property

    Dim fNPTItemOrder As Byte?
    Public Property NPTItemOrder() As Byte?
        Get
            Return fNPTItemOrder
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("NPTItemOrder", fNPTItemOrder, value)
        End Set
    End Property

    Dim fComponentType As Byte
    Public Property ComponentType() As Byte
        Get
            Return fComponentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ComponentType", fComponentType, value)
        End Set
    End Property

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