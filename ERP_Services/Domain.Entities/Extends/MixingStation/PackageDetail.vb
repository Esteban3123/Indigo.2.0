Imports System.Runtime.Serialization

Partial Public Class PackageDetail
    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del componente: medicamento, insumo o producto
    ''' </summary>
    <DataMember()>
    Public Property SourceCodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del componente de reconstitucion o vehiculo
    ''' </summary>
    <DataMember>
    Public Property SourceCodeNameSub As String

    ''' <summary>
    ''' Obtiene o establece el nombre del componente: medicamento, insumo o producto
    ''' </summary>
    <DataMember()>
    Public Property SourceName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida
    ''' </summary>
    <DataMember()>
    Public Property MeasureUnitDescription As String

    ''' <summary>
    ''' Obtiene la dosis del medicamento
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Dosis As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida
    ''' </summary>
    <DataMember()>
    Public Property VolumeMeasureUnitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de componente
    ''' </summary>
    <DataMember()>
    Public Property ComponentTypeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de preparación
    ''' </summary>
    <DataMember()>
    Public Property PreparationTypeName As String

    ''' <summary>
    ''' Obtiene o establece el item agregado en dilución
    ''' </summary>
    <DataMember()>
    Public Property ItemDilutionXpo As Object

    ''' <summary>
    ''' Obtiene o establece el item agregado en reconstitución
    ''' </summary>
    <DataMember()>
    Public Property ItemReconstitutionXpo As Object

    ''' <summary>
    ''' Obtiene o establece si el item agregado viene desde la dashboard de confirmación dosis unitarias
    ''' </summary>
    <DataMember()>
    Public Property IsDashboardConfirmationUnitDose As Boolean = False

    <DataMember()>
    Public Property SourceType As Boolean = False

    <DataMember()>
    Public Property UnitType As Byte = 0

    <DataMember>
    Public Property IsPrescribed As Boolean

    <DataMember>
    Public Property ComponentName As String

    <DataMember>
    Public Property DCIName As String

    <DataMember>
    Public Property MeasurementUnitAbbreviation As String

    ''' <summary>
    ''' Obtiene o establece la cantidad junto con la abreviacion de unidad de medida
    ''' </summary>
    <DataMember>
    Public Property QuantityMeasureunitname As String

    ''' <summary>
    ''' Obtiene o establece la concentracion junto con la abreviacion unidad de medida
    ''' </summary>
    <DataMember>
    Public Property ConcentrationName As String

    ''' <summary>
    ''' Indica si el item fué ordenado en la prescripción
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property WasOrdened As Boolean


    ''' <summary>
    ''' Indica la abreviacion de la unidad de medida de tipo volumen
    ''' </summary>
    ''' <returns></returns>
    Public Property VolumeMeasureUnitAbbreviation As String
End Class

