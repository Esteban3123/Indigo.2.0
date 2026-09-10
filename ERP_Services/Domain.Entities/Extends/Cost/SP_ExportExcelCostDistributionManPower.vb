Imports System.Runtime.Serialization

Partial Public Class SP_ExportExcelCostDistributionManPower_Result

#Region "Properties"

    <DataMember()>
    Public ReadOnly Property GroupCodeName As String
        Get
            Return String.Format("{0} - {1}", Me.GroupCode, Me.GroupName)
        End Get
    End Property

    <DataMember()>
    Public ReadOnly Property ThirdPartyNitName As String
        Get
            Return String.Format("{0} - {1}", Me.ThirdPartyNit, Me.ThirdPartyName)
        End Get
    End Property

    <DataMember()>
    Public ReadOnly Property PositionCodeName As String
        Get
            Return String.Format("{0} - {1}", Me.PositionCode, Me.PositionName)
        End Get
    End Property

#End Region

End Class
