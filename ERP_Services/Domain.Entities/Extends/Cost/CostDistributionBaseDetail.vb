Imports System.Runtime.Serialization
Public Class CostDistributionBaseDetail

    <DataMember>
    Property CodeNameMainAccount As String
    <DataMember>
    Property CodeNameCostCenter As String

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostDistributionBaseDetail
        Dim entity As CostDistributionBaseDetail = DirectCast(MemberwiseClone(), CostDistributionBaseDetail)
        Return entity
    End Function
#End Region

End Class
