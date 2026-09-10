Imports System.Runtime.Serialization

Partial Public Class SuppliersDistributionLines

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el numero y nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto de tesoreria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DistributionLineDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del cargo asociado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property PositionCodeName As String

#End Region

End Class
