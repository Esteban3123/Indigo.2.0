Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioNoteDetail
    Inherits Entity(Of Domain.Entities.PortfolioNoteDetail)
    ''' <summary>
    ''' Propiedad que almacena el código/nombre del concepto de la nota
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameNoteConcept As String

    ''' <summary>
    '''  Propiedad que almacena el código/nombre del centro de costo
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameCostCenter As String

    ''' <summary>
    ''' Propiedad que almacena el código de la cuenta principal
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameMainAccount As String

    ''' <summary>
    ''' Propiedad que almacena el código del tercero
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameThirdParty As String
    ''' <summary>
    ''' Propiedad que almacena el código del concepto de retención
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameRetentionConcept As String

    ''' <summary>
    ''' En esta lista se guardan las conceptos una vez han sido agrupado por este
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property PortfolioNoteDetailChild As New List(Of PortfolioNoteDetailChild)
End Class

''' <summary>
''' Propiedades extendidas para poder visualizar su informacion en la childlist
''' </summary>
''' <remarks></remarks>
<Serializable()>
    Public Class PortfolioNoteDetailChild
    <DataMember>
    Public Property MainAccountCodeName As String
    <DataMember>
    Public Property CostCenterCodeName As String
    <DataMember>
    Public Property NatureName As String
    <DataMember>
    Public Property ValueDetailConcept As Decimal
    <DataMember>
    Public Property Nature As Boolean
End Class
