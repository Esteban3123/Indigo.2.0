Imports DevExpress.Xpo

<Persistent("Maintenance.ViewMaintenanceProgramming")>
Partial Public Class Maintenance_ViewMaintenanceProgramming
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key()>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fPhysetAssetId As Integer
    Public Property PhysetAssetId() As Integer
        Get
            Return fPhysetAssetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PhysetAssetId", fPhysetAssetId, value)
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

    Dim fSerie As String
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property

    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
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

    Dim fItemTypeId As Integer
    Public Property ItemTypeId() As Integer
        Get
            Return fItemTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemTypeId", fItemTypeId, value)
        End Set
    End Property

    Dim fArticleCode As String
    Public Property ArticleCode() As String
        Get
            Return fArticleCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ArticleCode", fArticleCode, value)
        End Set
    End Property

    Dim fArticleName As String
    Public Property ArticleName() As String
        Get
            Return fArticleName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ArticleName", fArticleName, value)
        End Set
    End Property

    Dim fInventoryTypeId As Integer
    Public Property InventoryTypeId() As Integer
        Get
            Return fInventoryTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InventoryTypeId", fInventoryTypeId, value)
        End Set
    End Property

    Dim fInventoryType As Byte
    Public Property InventoryType() As Byte
        Get
            Return fInventoryType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("InventoryType", fInventoryType, value)
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

    Dim fLocationCode As String
    Public Property LocationCode() As String
        Get
            Return fLocationCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LocationCode", fLocationCode, value)
        End Set
    End Property

    Dim fLocationName As String
    Public Property LocationName() As String
        Get
            Return fLocationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LocationName", fLocationName, value)
        End Set
    End Property

    Dim fLocationCodeName As String
    Public Property LocationCodeName() As String
        Get
            Return fLocationCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LocationCodeName", fLocationCodeName, value)
        End Set
    End Property

    Dim fTrademarkCodeName As String
    Public Property TrademarkCodeName() As String
        Get
            Return fTrademarkCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TrademarkCodeName", fTrademarkCodeName, value)
        End Set
    End Property

    Dim fProgramingId As Nullable(Of Integer)
    Public Property ProgramingId() As Nullable(Of Integer)
        Get
            Return fProgramingId
        End Get
        Set(ByVal value As Nullable(Of Integer))
            SetPropertyValue(Of Nullable(Of Integer))("ProgramingId", fProgramingId, value)
        End Set
    End Property

    Dim fProgramado As String
    Public Property Programado() As String
        Get
            Return fProgramado
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Programado", fProgramado, value)
        End Set
    End Property

    Dim fPeriodicidad As String
    Public Property Periodicidad() As String
        Get
            Return fPeriodicidad
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Periodicidad", fPeriodicidad, value)
        End Set
    End Property

    Dim fProtocolColor As Integer
    Public Property ProtocolColor() As Integer
        Get
            Return fProtocolColor
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProtocolColor", fProtocolColor, value)
        End Set
    End Property

    Dim fProtocolCode As String
    Public Property ProtocolCode() As String
        Get
            Return fProtocolCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProtocolCode", fProtocolCode, value)
        End Set
    End Property

    Dim fProtocolName As String
    Public Property ProtocolName() As String
        Get
            Return fProtocolName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProtocolName", fProtocolName, value)
        End Set
    End Property

    Dim fProtocolCodeName As String
    Public Property ProtocolCodeName() As String
        Get
            Return fProtocolCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProtocolCodeName", fProtocolCodeName, value)
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

    Dim fReponsibleTypeId As Integer
    Public Property ReponsibleTypeId() As Integer
        Get
            Return fReponsibleTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReponsibleTypeId", fReponsibleTypeId, value)
        End Set
    End Property

    Dim fResponsibleNit As String
    Public Property ResponsibleNit() As String
        Get
            Return fResponsibleNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleNit", fResponsibleNit, value)
        End Set
    End Property

    Dim fResponsibleName As String
    Public Property ResponsibleName() As String
        Get
            Return fResponsibleName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleName", fResponsibleName, value)
        End Set
    End Property

    Dim fResponsibleCodeName As String
    Public Property ResponsibleCodeName() As String
        Get
            Return fResponsibleCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResponsibleCodeName", fResponsibleCodeName, value)
        End Set
    End Property

    Dim fProgramatedDateId As Integer
    Public Property ProgramatedDateId() As Integer
        Get
            Return fProgramatedDateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProgramatedDateId", fProgramatedDateId, value)
        End Set
    End Property

    Dim fUltimoMantenimiento As DateTime
    Public Property UltimoMantenimiento() As DateTime
        Get
            Return fUltimoMantenimiento
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("UltimoMantenimiento", fUltimoMantenimiento, value)
        End Set
    End Property

    Dim fProximoMantenimiento As DateTime
    Public Property ProximoMantenimiento() As DateTime
        Get
            Return fProximoMantenimiento
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ProximoMantenimiento", fProximoMantenimiento, value)
        End Set
    End Property

    Dim fTypeName As String
    Public Property TypeName() As String
        Get
            Return fTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeName", fTypeName, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("concat('(', Plate, ') ', ArticleCode, ' - ', ArticleName)")>
    Public ReadOnly Property PlateArticleFullname() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PlateArticleFullname"))
        End Get
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class