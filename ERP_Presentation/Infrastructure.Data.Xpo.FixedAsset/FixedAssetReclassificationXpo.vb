Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetReclassification")>
Public Class FixedAssetReclassificationXpo
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

    Dim fReclassificationType As Byte
    Public Property ReclassificationType() As Byte
        Get
            Return fReclassificationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ReclassificationType", fReclassificationType, value)
        End Set
    End Property

    <PersistentAlias("iif(ReclassificationType = 1, 'Corrección Catálogo', iif(ReclassificationType = 2, 'Corrección Artículo', ''))")>
    Public ReadOnly Property ReclassificationTypeName As String
        Get
            If (Me.IsInvalidated) Then
                Select Case fReclassificationType
                    Case 1
                        Return "Corrección Catálogo"
                    Case 2
                        Return "Corrección Artículo"
                    Case Else
                        Return String.Empty
                End Select
            End If
            Return Me.EvaluateAlias("ReclassificationTypeName")
        End Get
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("iif(Status = 1, 'Sin Confirmar', iif(Status = 2, 'Confirmado', iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            If (Me.IsInvalidated) Then
                Select Case fStatus
                    Case 1
                        Return "Sin Confirmar"
                    Case 2
                        Return "Confirmado"
                    Case 3
                        Return "Anulado"
                    Case Else
                        Return String.Empty
                End Select
            End If
            Return Me.EvaluateAlias("StatusName")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


