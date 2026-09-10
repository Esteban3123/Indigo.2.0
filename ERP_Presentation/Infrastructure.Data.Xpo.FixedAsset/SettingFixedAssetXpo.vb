Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.SettingFixedAsset")>
Public Class SettingFixedAssetXpo
    Inherits XPLiteObject

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

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fProcessDate As Date
    Public Property ProcessDate() As Date
        Get
            Return fProcessDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessDate", fProcessDate, value)
        End Set
    End Property

    Dim fCommitmentBudgetInterface As Boolean
    Public Property CommitmentBudgetInterface() As Boolean
        Get
            Return fCommitmentBudgetInterface
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CommitmentBudgetInterface", fCommitmentBudgetInterface, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("CurrencyReferenceSettingFixedAsset")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


