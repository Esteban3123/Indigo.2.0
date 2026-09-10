Imports System.Runtime.Serialization

Partial Public Class PaymentsNoteDetails

#Region "Properties"

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
    Public Property DescriptionNoteConcept As String

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
    ''' Establece si el concepto es de tipo general o especifico (1.General, 2.Especifico)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property HandlesMainAccountByConcept As Integer

    ''' <summary>
    ''' Obtiene o establece la lista de los detalles de un concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property PaymentsNoteDetailsAccountInfo As New List(Of PaymentsNoteDetailsAccountInfo)()
#End Region

End Class

''' <summary>
''' Propiedades extendidas para poder visualizar su informacion en la childlist
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
<Serializable()>
Public Class PaymentsNoteDetailsAccountInfo
    <DataMember>
    Public Property MainAccountCodeName As String
    <DataMember>
    Public Property CostCenterCodeName As String
    <DataMember>
    Public Property Observations As String
    <DataMember>
    Public Property NatureName As String
    <DataMember>
    Public Property ValueDetailConcept As Decimal
    <DataMember>
    Public Property Nature As Boolean
    <DataMember>
    Public Property DebitValue As Decimal
    <DataMember>
    Public Property CreditValue As Decimal

End Class
