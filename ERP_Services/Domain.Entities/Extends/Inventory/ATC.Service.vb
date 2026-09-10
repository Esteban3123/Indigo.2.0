Imports System.Runtime.Serialization

Public Class ATC

#Region "Properties and Variables"
    ''' <summary>
    ''' Obtiene o establece la descripcion de DCI
    ''' </summary>
    <DataMember()>
    Public Property NullTextDCI As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la ruta de administración
    ''' </summary>
    <DataMember()>
    Public Property NullTextAdministrationRoute As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del grupo farmacologico
    ''' </summary>
    <DataMember()>
    Public Property NullTextPharmacologicalGroup As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del nivel de riesgo
    ''' </summary>
    <DataMember()>
    Public Property NullTextRiskLevel As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida
    ''' </summary>
    <DataMember()>
    Public Property NullTextUnitMeasure As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida del volumen
    ''' </summary>
    <DataMember()>
    Public Property NullTextUnitMeasureVolumen As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de administracion
    ''' </summary>
    <DataMember()>
    Public Property NullTextUnitAdministration As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida de la concentracion
    ''' </summary>
    <DataMember()>
    Public Property NullTextUnitMeasureConcentration As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del ATC
    ''' </summary>
    <DataMember()>
    Public Property NullTextATCEntity As String

    ''' <summary>
    ''' Entidad que representa al producto y es utilizada para sacar las patologias cuando el producto es pos
    ''' </summary>
    <DataMember()>
    Public Property ProductPathologies As InventoryProduct

    ''' <summary>
    ''' Descripcion del grupo de facturacion No POS
    ''' </summary>
    <DataMember()>
    Public Property BillingGroupNoPOSDescription As String

    ''' <summary>
    ''' Descripcion de forma farmacéutica
    ''' </summary>
    <DataMember()>
    Public Property PharmaceuticalFormDescription As String

    ''' <summary>
    ''' Indica si la forma farmaceutica requiere o no estabilidad
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property RequireStability As Boolean?

    ''' <summary>
    ''' Obtiene la abreviacion de la unidad de medida de la concentración del medicamento
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ATCMeasureAbbreviation As String
#End Region

End Class
