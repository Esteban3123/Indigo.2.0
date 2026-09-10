Imports System.Runtime.Serialization

Public Class ATCConcentrationByDCI

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de DCI
    ''' </summary>
    <DataMember()>
    Public Property DCICodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida de la concentración
    ''' </summary>
    <DataMember()>
    Public Property ConcentrationMeasureUnitCodeName As String

    ''' <summary>
    ''' Obtiene o establece la abreviación de la unidad de medida de la concentración
    ''' </summary>
    <DataMember()>
    Public Property Abbreviation As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la unidad de medida del medicamento combinado
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Name As String

#End Region

End Class
