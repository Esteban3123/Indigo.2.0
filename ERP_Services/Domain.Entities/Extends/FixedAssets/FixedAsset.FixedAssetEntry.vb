Imports System.Runtime.Serialization

Partial Public Class FixedAssetEntry

#Region "Properties"

    ''' <summary>
    ''' Código nombre articulo
    ''' </summary>
    <DataMember()>
    Public Property SupplierCodeName As String

    ''' <summary>
    ''' Código nombre articulo
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleCodeName As String

    ''' <summary>
    ''' Código nombre centro costo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la resolucion de documento soporte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionDocumentSupport As String

#End Region

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

#End Region

End Class
