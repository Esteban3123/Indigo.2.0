Imports System.Runtime.Serialization
Partial Public Class Category

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la fuente de financiación
    ''' </summary>
    <DataMember()>
    Public Property FinancialSourceDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del rubro padre
    ''' </summary>
    <DataMember()>
    Public Property ParentDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del CCPET
    ''' </summary>
    <DataMember()>
    Public Property CCPETDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del CPC
    ''' </summary>
    <DataMember()>
    Public Property CPCDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del política pública
    ''' </summary>
    <DataMember()>
    Public Property PublicPolicyDescription As String

    ''' <summary>
    ''' Propiedad ficticia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MoreInfo As String

    ''' <summary>
    ''' Obtiene o establece el listado de tipos de ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ListRevenueType As List(Of RevenueType)

    <DataMember()>
    Public Property Value As Decimal

    <DataMember()>
    Public Property TotalBudget As Decimal
#End Region

End Class
