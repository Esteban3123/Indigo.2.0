#Region "imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class InventoryRequest

    ''' <summary>
    ''' propiedad que contiene el codigo y el nombre de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionFunctionalUnit As String

    ''' <summary>
    ''' Propiedad que contiene el codigo y el nombre del almacen de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionSourceWarehouse As String

    ''' <summary>
    ''' Propiedad que contiene el codigo y el nombre del almacen de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionTargetWarehouse As String

    <DataMember()>
    Public Property Prefix As String

    ''' <summary>
    ''' Propiedad que contiene el Id de la Central de Mezcla
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CMConfigurationId As Integer

    ''' <summary>
    ''' Propiedad que contiene el Id de campaign Detail
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CampaignDetailId As Integer

    ''' <summary>
    ''' Propiedad que contiene el Número de la Campaña 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CampaignNumber As Integer

End Class
