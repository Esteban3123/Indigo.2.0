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

<Persistent("Inventory.InventorySupplie")>
Partial Public Class MixingStationSupplieXpo
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

    Dim fSupplieName As String
    Public Property SupplieName() As String
        Get
            Return fSupplieName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SupplieName", fSupplieName, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),SupplieName)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("PackageDetailReferencesSupplie", GetType(MixinStationPackageDetailXpo))>
    Public ReadOnly Property MixinStationPackageDetailXpo() As XPCollection(Of MixinStationPackageDetailXpo)
        Get
            Return GetCollection(Of MixinStationPackageDetailXpo)("MixinStationPackageDetailXpo")
        End Get
    End Property

    <Association("PackagePersonalizedDetailReferencesSupplie", GetType(PackagePersonalizedDetailXpo))>
    Public ReadOnly Property PackagePersonalizedDetailXpo() As XPCollection(Of PackagePersonalizedDetailXpo)
        Get
            Return GetCollection(Of PackagePersonalizedDetailXpo)("PackagePersonalizedDetailXpo")
        End Get
    End Property

    <Association("ExternalPatientPreparationDetail_Reference_InventorySupplie", GetType(ExternalPatientPreparationDetailXpo))>
    Public ReadOnly Property ExternalPatientPreparationDetailsXpo() As XPCollection(Of ExternalPatientPreparationDetailXpo)
        Get
            Return GetCollection(Of ExternalPatientPreparationDetailXpo)("ExternalPatientPreparationDetailsXpo")
        End Get
    End Property

End Class