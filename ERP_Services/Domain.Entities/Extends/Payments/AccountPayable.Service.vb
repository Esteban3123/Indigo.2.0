Imports System.Runtime.Serialization

Partial Public Class AccountPayable

#Region "Properties"

    ''' <summary>
    ''' Identifica el tipo de contribuyente del tercero
    ''' </summary>
    <DataMember()>
    Public Property ContributionType As Integer

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    <DataMember()>
    Public Property Percentage As Decimal

    ''' <summary>
    ''' Obtiene o establece el ajuste
    ''' </summary>
    <DataMember()>
    Public Property Adjustment As Decimal

    ''' <summary>
    ''' Id del concepto electrónico cuando se aplica una nota
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ConceptAdjustmentId As Integer?

    ''' <summary>
    ''' Obtiene o establece la bandera para saber si la factura fue modificada
    ''' </summary>
    <DataMember()>
    Public Property HandlesAddModifyDelete As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la relacion de paymentNotesAccountPayableAdvance
    ''' </summary>
    <DataMember()>
    Public Property IdPaymentNotesAccountPayableAdvance As Integer

    ''' <summary>
    ''' Obtiene o establece el valor del cruce (Cruce de comprobante de egreso - Tesoreria)
    ''' </summary>
    <DataMember()>
    Public Property CrossingValue As Decimal

    ''' <summary>
    ''' Obtiene o establece la descripcion del proveedor con su linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionSupplier As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionDistributionLine As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del cargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property PositionCodeName As String

    ''' <summary>
    ''' Obtiene o establece el numero y nombre de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property NumberNameMainAccount As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionCostCenter As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionFilingUnit As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionSupplierType As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la resolucion de documento soporte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionDocumentSupport As String

    ''' <summary>
    ''' Obtiene o establece si el check esta activo o no,
    ''' En el formulario de traslado de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SelectedItem As Boolean

    ''' <summary>
    ''' Obtiene o establece si el proveedor es declarante
    ''' (1=Declarante, 2=No Declarante)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property Declarant As Integer

    ''' <summary>
    ''' Obtiene o estables si el proveedor es empleado independiente
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IndependentEmployee As Boolean

    ''' <summary>
    ''' Tipo de comprobante que envian en el form de comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property JournalVoucherId As Integer?

    ''' <summary>
    ''' Establece si la cuenta por pagar esta en un traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property IfTransferContainsAccountPayable As Boolean

    ''' <summary>
    ''' Auxiliar de nombre entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AuxEntityName As String

    ''' <summary>
    ''' Auxiliar de código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AuxEntityCode As String

    ''' <summary>
    ''' Auxiliar de id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AuxEntityId As Integer

    ''' <summary>
    ''' Permite saber si se cambian las propiedades del entityName, entityCode, entityId del comprobante contable desde ingreso de activos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ChangeProperties As Boolean = False

    ''' <summary>
    ''' Agrega detalles que se contabilizarán en otros libros que no sean homologables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AccountPayableDetailConceptOthersNotHomologatedBooks As New Dictionary(Of Integer, List(Of AccountPayableDetailConcept))()

    <DataMember()>
    Public Property IndigoCompanyNit As String

    ''' <summary>
    ''' Abreviacion de la moneda segun la ISO4217
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CurrencyAbbreviation As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la actividad económica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CodeNameEconomicActivity As String
#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

#End Region

#End Region

End Class
