Imports System.Runtime.Serialization

Partial Public Class CostDistributionSecondary

    <DataMember()>
    Property FullNameProductionCenter As String
    <DataMember()>
    Property Checked As Boolean

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostDistributionSecondary
        Dim entity As CostDistributionSecondary = DirectCast(MemberwiseClone(), CostDistributionSecondary)
        Return entity
    End Function
#End Region

End Class
