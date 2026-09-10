Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetRemissionEntrance")> _
Public Class FixedAssetRemissionEntranceXpo
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
    Dim fRemisionDate As DateTime
    Public Property RemisionDate() As DateTime
        Get
            Return fRemisionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RemisionDate", fRemisionDate, value)
        End Set
    End Property
    Dim fAdquisitionType As Byte
    Public Property AdquisitionType() As Byte
        Get
            Return fAdquisitionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionType", fAdquisitionType, value)
        End Set
    End Property
    Dim fSupplierDistributionLineId As Integer
    Public Property SupplierDistributionLineId() As Integer
        Get
            Return fSupplierDistributionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLineId", fSupplierDistributionLineId, value)
        End Set
    End Property
    Dim fRemisionNumber As String
    <Size(20)> _
    Public Property RemisionNumber() As String
        Get
            Return fRemisionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RemisionNumber", fRemisionNumber, value)
        End Set
    End Property
    Dim fGetLocationResponsible As Byte
    Public Property GetLocationResponsible() As Byte
        Get
            Return fGetLocationResponsible
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GetLocationResponsible", fGetLocationResponsible, value)
        End Set
    End Property
    Dim fLocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return fLocationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LocationId", fLocationId, value)
        End Set
    End Property
    Dim fResponsibleId As Integer
    Public Property ResponsibleId() As Integer
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(300)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
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

    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    <PersistentAlias("Iif(AdquisitionType = 1, 'Compra Directa', Iif(AdquisitionType = 2, 'N/A',Iif(AdquisitionType = 3, 'Comodato',Iif(AdquisitionType = 4, 'Donado por Particulares',Iif(AdquisitionType = 5, 'Traspaso de Bienes',Iif(AdquisitionType = 6, 'Otro Concepto',Iif(AdquisitionType = 7, 'Leasing Financiero',Iif(AdquisitionType = 8, 'Comodato Tercerizado',Iif(AdquisitionType = 9, 'Renting Financiero',Iif(AdquisitionType = 10, 'Renting Operativo', ''))))))))))")>
    Public ReadOnly Property AdquisitionTypeName() As String
        Get
            'Select Case fAdquisitionType
            '    Case 1
            '        Return "Compra Directa"
            '    Case 2
            '        Return "N/A"
            '    Case 3
            '        Return "Comodato"
            '    Case 4
            '        Return "Donado por Particulares"
            '    Case 5
            '        Return "Traspaso de Bienes"
            '    Case 6
            '        Return "Otro Concepto"
            '    Case 7
            '        Return "Leasing Financiero"
            '    Case 8
            '        Return "Comodato Tercerizado"
            '    Case 9
            '        Return "Renting Financiero"
            '    Case 10
            '        Return "Renting Operativo"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString("AdquisitionTypeName")
        End Get
    End Property

    <Association("RemissionEntranceItemReferenceRemissionEntrance", GetType(FixedAssetRemissionEntranceItemXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemXpo() As XPCollection(Of FixedAssetRemissionEntranceItemXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemXpo)("FixedAssetRemissionEntranceItemXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

