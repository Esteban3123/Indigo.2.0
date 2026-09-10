'************************************************************
' Assembly         : Infraestructure.FisedAssets.FixedAssetItemCatalog
' Author           : Felix Camilo Salazar Roldan
' Created          : 05-02-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports DevExpress.Xpo
#End Region

<Persistent("FixedAsset.FixedAssetItemCatalog")>
Public Class FixedAssetItemCatalogXpo
    Inherits XPLiteObject

    Dim _id As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", _id, value)
        End Set
    End Property

    Dim _classification As Byte
    Public Property Classification() As Byte
        Get
            Return _classification
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("Classification", _classification, value)
        End Set
    End Property

    <Association("FixedAssetFixedAssetItemReferences", GetType(FixedAssetFixedAssetItemXpo))>
    Public ReadOnly Property FixedAssetFixedAssetItemXpo() As XPCollection(Of FixedAssetFixedAssetItemXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetItemXpo)("FixedAssetFixedAssetItemXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
