Imports System.Runtime.Serialization

Partial Public Class InitialBalanceAdvance

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

#End Region

End Class
