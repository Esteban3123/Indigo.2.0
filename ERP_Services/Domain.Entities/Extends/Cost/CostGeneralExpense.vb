Imports System.Runtime.Serialization

Partial Public Class CostGeneralExpense

#Region "Properties"

    <DataMember()> _
    Property FullNameMainAccount As String

    <DataMember()>
    Property CategoryCodeName As String

    <DataMember()>
    Property DirectLaborDistributionCodeName As String

    <DataMember()>
    Property ReversalDirectLaborDistributionCodeName As String

#End Region

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostGeneralExpense
        Dim entity As CostGeneralExpense = DirectCast(MemberwiseClone(), CostGeneralExpense)
        Return entity
    End Function
#End Region

End Class
