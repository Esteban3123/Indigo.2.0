Imports System.Runtime.Serialization

Partial Public Class AdjustmentConcept

#Region "Properties"

    ''' <summary>
    ''' Codigo y nombre del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CostCenterCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable debito
    ''' </summary>
    <DataMember()>
    Public Property MainAccountAdjustmentDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable credito
    ''' </summary>
    <DataMember()>
    Public Property MainAccountIvaDescription As String

    ''' <summary>
    ''' obtiene o establece la descripcion del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CostCenterDescription As String

#End Region

End Class
