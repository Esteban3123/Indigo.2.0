Imports System.Runtime.Serialization

Partial Public Class DistributionLines

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el numero y nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property NumberNameMainAccount As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto de tesoreria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ExpensiveConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece variable pivot
    ''' </summary>
    <DataMember()>
    Public Property HandlesDelete As Boolean

    ''' <summary>
    ''' NOMBRE DEL CONCEPTO DE LA NOTA DE CXP
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property AccountPayableConceptNoteName As String

    ''' <summary>
    ''' nombre  de cuenta contable
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IdAccountName As String

#End Region

End Class
