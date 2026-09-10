Imports System.Runtime.Serialization

Partial Public Class AccountPayableDetailConcept

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el modo: editar o agregar
    ''' </summary>
    <DataMember()>
    Public Property HandlesDeferredCausation As Boolean

    ''' <summary>
    ''' Obtiene o establece el nombre y el numero de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property NumberNameMainAccount As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionCostCenter As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionPaymentConcept As String

    ''' <summary>
    ''' Obtiene o establece la descipción del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionThirdParty As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionRetentionConcept As String

    ''' <summary>
    ''' Obtiene o establece el nombre del IVA 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property NameIVA As String

    ''' <summary>
    ''' Id del concepto de retencion 383
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RetentionConceptId383 As Integer?

    ''' <summary>
    ''' Codigo y nombre del concepto de retencion 383
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RetentionConceptDescription383 As String

    ''' <summary>
    ''' Id del concepto de retencion 384
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RetentionConceptId384 As Integer?

    ''' <summary>
    ''' Codigo y nombre del concepto de retencion 384
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RetentionConceptDescription384 As String

    ''' <summary>
    ''' Permite saber si el concepto se agrego con el porcentaje IVA
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IsIvaValue As Boolean = False

    ''' <summary>
    ''' En este diccionario se guarda el libro no oficial y su respectiva cuenta contable
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DictionaryConfigurationBooks As New Dictionary(Of Integer, Integer)()

    ''' <summary>
    ''' En esta lista se guardan las conceptos una vez han sido agrupado por este
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property AccountPayabbleDetailChild As List(Of AccountPayableDetailConcept)


    ''' <summary>
    ''' Nombre de la naturaleza
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property NatureName As String

    ''' <summary>
    ''' Abreviacion de la moneda segun la ISO4217
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CurrencyAbbreviation As String


#End Region

#Region "Cloneable"

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As AccountPayableDetailConcept
        Dim entity As AccountPayableDetailConcept = DirectCast(MemberwiseClone(), AccountPayableDetailConcept)
        Return entity
    End Function

#End Region

End Class
