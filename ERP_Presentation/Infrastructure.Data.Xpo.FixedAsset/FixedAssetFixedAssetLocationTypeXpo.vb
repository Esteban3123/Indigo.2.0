Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetLocationType")> _
Public Class FixedAssetFixedAssetLocationTypeXpo
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
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fOrderLocation As Integer
    Public Property OrderLocation() As Integer
        Get
            Return fOrderLocation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OrderLocation", fOrderLocation, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <PersistentAlias("Iif(State = 0, 'Inactivo', Iif(State = 1, 'Activo', ''))")>
    Public ReadOnly Property StateName As String
        Get
            'Select Case fState
            '    Case 0
            '        Return "Inactivo"
            '    Case 1
            '        Return "Activo"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property

    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocationType", GetType(FixedAssetFixedAssetLocationXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetLocations() As XPCollection(Of FixedAssetFixedAssetLocationXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetLocationXpo)("FixedAsset_FixedAssetLocations")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
