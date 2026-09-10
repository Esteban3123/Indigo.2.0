Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetTransfer")> _
Public Class FixedAssetTransferXpo
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

    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
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

    Dim fTransferType As Byte
    Public Property TransferType() As Byte
        Get
            Return fTransferType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TransferType", fTransferType, value)
        End Set
    End Property

    <PersistentAlias("Iif(TransferType = 1, 'Localización', Iif(TransferType = 2, 'Responsable',Iif(TransferType = 3, 'Localización y Responsable', '')))")>
    Public ReadOnly Property TransferTypeName As String
        Get
            'Select Case fTransferType
            '    Case 1
            '        Return "Localización"
            '    Case 2
            '        Return "Responsable"
            '    Case 3
            '        Return "Localización y Responsable"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("TransferTypeName"))
        End Get
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

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return "Sin Confirmar"
            '    Case 2
            '        Return "Confirmado"
            '    Case 3
            '        Return "Anulado"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fSourceResponsibleId As FixedAssetResponsibleXpo
    <Association("TransferReferencesSourceResponsible")>
    Public Property SourceResponsibleId() As FixedAssetResponsibleXpo
        Get
            Return fSourceResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("SourceResponsibleId", fSourceResponsibleId, value)
        End Set
    End Property

    Dim fTargetResponsibleId As FixedAssetResponsibleXpo
    <Association("TransferReferencesTargetResponsible")>
    Public Property TargetResponsibleId() As FixedAssetResponsibleXpo
        Get
            Return fTargetResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleXpo)
            SetPropertyValue(Of FixedAssetResponsibleXpo)("TargetResponsibleId", fTargetResponsibleId, value)
        End Set
    End Property

    Dim fSourceLocationId As FixedAssetFixedAssetLocationXpo
    <Association("TransferReferencesSourceLocation")>
    Public Property SourceLocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fSourceLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("SourceLocationId", fSourceLocationId, value)
        End Set
    End Property

    Dim fTargetLocationId As FixedAssetFixedAssetLocationXpo
    <Association("TransferReferencesTargetLocation")>
    Public Property TargetLocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fTargetLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("TargetLocationId", fTargetLocationId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


