Imports System.Runtime.Serialization

Partial Public Class InitialBalanceAccountPayable

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del proveedor
    ''' </summary>
    <DataMember()>
    Public Property SupplierDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro de costo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad radicacion
    ''' </summary>
    <DataMember()>
    Public Property FilingUnitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de proveedor
    ''' </summary>
    <DataMember()>
    Public Property SupplierTypeDescription As String

#End Region

End Class
