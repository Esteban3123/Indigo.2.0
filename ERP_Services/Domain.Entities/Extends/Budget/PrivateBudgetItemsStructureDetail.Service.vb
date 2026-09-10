Imports System.Runtime.Serialization

Public Class PrivateBudgetItemsStructureDetail

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro costo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tercero
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyDescription As String

End Class
