'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2024-02-07
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewParenteralNutritionLabelComponents")>
Partial Public Class ViewParenteralNutritionLabelComponentsXpo
    Inherits XPLiteObject

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

    ''' <summary>
    ''' Id de la prescripción de nutrición parenteral (HCNUTPAREC.ID)
    ''' </summary>
    Dim fParenteralNutritionId As Integer
    Public Property ParenteralNutritionId() As Integer
        Get
            Return fParenteralNutritionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ParenteralNutritionId", fParenteralNutritionId, value)
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

    Dim fNutritionName As String
    Public Property NutritionName() As String
        Get
            Return fNutritionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NutritionName", fNutritionName, value)
        End Set
    End Property

    Dim fRequest As Decimal
    Public Property Request() As Decimal
        Get
            Return fRequest
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Request", fRequest, value)
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

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class