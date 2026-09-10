
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListCMCenterAttention")>
Public Class ViewListCMCenterAttentionXpo
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

    Dim fMixingStation As Integer
    Public Property MixingStation() As Integer
        Get
            Return fMixingStation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MixingStation", fMixingStation, value)
        End Set
    End Property

    Dim fCodeCenterAttention As String
    Public Property CodeCenterAttention() As String
        Get
            Return fCodeCenterAttention
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCenterAttention", fCodeCenterAttention, value)
        End Set
    End Property

    Dim fCenterAttention As String
    Public Property CenterAttention() As String
        Get
            Return fCenterAttention
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttention", fCenterAttention, value)
        End Set
    End Property

    Dim fCenterAttentionDescription As String
    Public Property CenterAttentionDescription() As String
        Get
            Return fCenterAttentionDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttentionDescription", fCenterAttentionDescription, value)
        End Set
    End Property

    Dim fProductionLineIds As String
    Public Property ProductionLineIds() As String
        Get
            Return fProductionLineIds
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineIds", fProductionLineIds, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
