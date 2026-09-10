Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ViewPartAccesoryConsumables")>
Partial Public Class Maintenance_ViewPartAccesoryConsumables
        Inherits XPLiteObject
        Dim fId As String
        <Key()>
        <Size(24)>
        Public Property Id() As String
            Get
                Return fId
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Id", fId, value)
            End Set
        End Property
        Dim fDocumentId As Integer
        Public Property DocumentId() As Integer
            Get
                Return fDocumentId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("DocumentId", fDocumentId, value)
            End Set
        End Property
        Dim fCode As String
        <Size(20)>
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
        Dim fTypeDetail As Integer
        Public Property TypeDetail() As Integer
            Get
                Return fTypeDetail
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("TypeDetail", fTypeDetail, value)
            End Set
        End Property
        Dim fTypeName As String
        <Size(11)>
        Public Property TypeName() As String
            Get
                Return fTypeName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("TypeName", fTypeName, value)
            End Set
        End Property
        Dim fPhysicalAssetId As Integer
        Public Property PhysicalAssetId() As Integer
            Get
                Return fPhysicalAssetId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("PhysicalAssetId", fPhysicalAssetId, value)
            End Set
        End Property
End Class