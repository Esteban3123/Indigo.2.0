Imports System.Runtime.Serialization
Partial Public Class DistributionBaseDetail

    <DataMember>
    Property CodeNameMainAccount As String
    <DataMember>
    Property CodeNameCostCenter As String

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As DistributionBaseDetail
        Dim entity As DistributionBaseDetail = DirectCast(MemberwiseClone(), DistributionBaseDetail)
        Return entity
    End Function
#End Region

End Class
