#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' Lista de activos por reclasificar
''' </summary>
<Persistent("FixedAsset.ViewListFixedAssetForReclassification")>
Public Class ViewListFixedAssetForReclassificationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fItemCatalogId As Integer
    Public Property ItemCatalogId() As Integer
        Get
            Return fItemCatalogId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemCatalogId", fItemCatalogId, value)
        End Set
    End Property

    Dim fItemId As Integer
    Public Property ItemId() As Integer
        Get
            Return fItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemId", fItemId, value)
        End Set
    End Property

    Dim fItemCodeDescription As String
    Public Property ItemCodeDescription() As String
        Get
            Return fItemCodeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeDescription", fItemCodeDescription, value)
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

    Dim fSerie As String
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fPlate As String
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
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

    <PersistentAlias("Iif(AdquisitionType = 1, 'Compra Directa', Iif(AdquisitionType = 3, 'Comodato', Iif(AdquisitionType = 4, 'Donado por Particulares', Iif(AdquisitionType = 5, 'Traspaso de Bienes', Iif(AdquisitionType = 6, 'Otro Concepto', Iif(AdquisitionType = 7, 'Leasing Financiero', Iif(AdquisitionType = 8, 'Comodato Tercerizado', Iif(AdquisitionType = 10, 'Renting Operativo', ''))))))))")>
    Public ReadOnly Property AdquisitionTypeName As String
        Get
            Return EvaluateAlias("AdquisitionTypeName")
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

    <PersistentAlias("Iif(HasOutput = True, 'Inactivo', Iif(OutputRefund = True, 'Inactivo', Iif(Status = 0, 'Inactivo', 'Activo')))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return EvaluateAlias("StatusName")
        End Get
    End Property

    Dim fLegalBookId As BookXpo
    <Association("FixedAssetForReclassificationReferenceBook")>
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fLegalBookCodeName As String
    Public Property LegalBookCodeName() As String
        Get
            Return fLegalBookCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LegalBookCodeName", fLegalBookCodeName, value)
        End Set
    End Property

    Dim fDepreciatedValue As Decimal
    Public Property DepreciatedValue() As Decimal
        Get
            Return fDepreciatedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciatedValue", fDepreciatedValue, value)
        End Set
    End Property

    Dim fResidualValue As Decimal
    Public Property ResidualValue() As Decimal
        Get
            Return fResidualValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResidualValue", fResidualValue, value)
        End Set
    End Property

    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property

    <PersistentAlias("Iif(HasOutput = True, 0, Iif(OutputRefund = True, 0, Iif(Status = 0, 0, Iif(AdquisitionType = 8, 0, Iif(AdquisitionType = 10, 0, HistoricalValue)))))")>
    Public ReadOnly Property ReclasificatedValue() As Decimal
        Get
            Return EvaluateAlias("ReclasificatedValue")
        End Get
    End Property

    Dim fAdquisitionTypeReal As Byte
    Public Property AdquisitionTypeReal() As Byte
        Get
            Return fAdquisitionTypeReal
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionTypeReal", fAdquisitionTypeReal, value)
        End Set
    End Property

    Dim fHasReclassified As Boolean
    Public Property HasReclassified() As Boolean
        Get
            Return fHasReclassified
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasReclassified", fHasReclassified, value)
        End Set
    End Property

    Dim fHasOutput As Byte
    Public Property HasOutput() As Byte
        Get
            Return fHasOutput
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("HasOutput", fHasOutput, value)
        End Set
    End Property

    Dim fOutputRefund As Byte
    Public Property OutputRefund() As Byte
        Get
            Return fOutputRefund
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("OutputRefund", fOutputRefund, value)
        End Set
    End Property

#End Region

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