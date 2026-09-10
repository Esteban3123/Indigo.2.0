Imports System.Runtime.Serialization

Public Class InventoryProduct

#Region "Properties"

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property ProductTypeDescription As String

    ''' <summary>
    ''' Descripcion de la ATC
    ''' </summary>
    <DataMember()>
    Public Property ATCDescription As String

    ''' <summary>
    ''' Descripcion del Nivel de Riesgo
    ''' </summary>
    <DataMember()>
    Public Property InventoryRiskLevelDescription As String

    ''' <summary>
    ''' Descripcion de la unidad de empaque
    ''' </summary>
    <DataMember()>
    Public Property PackingUnitDescription As String

    ''' <summary>
    ''' Descripcion del grupo
    ''' </summary>
    <DataMember()>
    Public Property GroupDescription As String

    ''' <summary>
    ''' Descripcion del subgrupo
    ''' </summary>
    <DataMember()>
    Public Property SubGroupDescription As String

    ''' <summary>
    ''' Descripcion del fabricante
    ''' </summary>
    <DataMember()>
    Public Property ManufacturerDescription As String

    ''' <summary>
    ''' Descripcion del codigo de iva
    ''' </summary>
    <DataMember()>
    Public Property IvaCodeDescription As String

    ''' <summary>
    ''' Descripcion del codigo de iva
    ''' </summary>
    <DataMember()>
    Public Property IvaPercent As Decimal

    ''' <summary>
    ''' Descripcion del grupo de facturacion
    ''' </summary>
    <DataMember()>
    Public Property BillingGroupDescription As String

    ''' <summary>
    ''' Descripcion del grupo de facturacion No POS
    ''' </summary>
    <DataMember()>
    Public Property BillingGroupNoPOSDescription As String

    ''' <summary>
    ''' Abreviación de la unidad de empaque
    ''' </summary>
    <DataMember()>
    Public Property PackingUnitAbbreviation As String

    ''' <summary>
    ''' Porcentaje de rentencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property WithholdingSourcePercernt As Decimal?

    ''' <summary>
    ''' Id de la cuenta donde se asignara la cuenta por pagar de la retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property AccountWithholdingSourceId As Integer?
    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property ConceptAccountPayableWithholdingSourceId As Integer?
    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property RetentionConceptsWithholdingSourceId As Integer?

    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property AccountInventoryId As Integer?


    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property AccountInventoryHandlessCostCenter As Boolean

    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property ConceptAccountPayableInventory As Integer?

    ''' <summary>
    ''' indica si la cuenta asociada maneja tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property HandlesThirdPartyAccount As Boolean

    <DataMember()>
    Public Property AccountInventoryCodeName As String

    <DataMember()>
    Public Property HandlesCostCenterWithholdigSourceAccount As Boolean

    ''' <summary>
    ''' Propiedad que contiene el maximo stock por almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MaximumStockWarehouse As Integer

    ''' <summary>
    ''' Propiedad que contiene el minimo stock por almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MinimumStockWarehouse As Integer

    ''' <summary>
    ''' Propiedad que contiene el punto de reposicion stock por almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RepositionPointWarehouse As Integer

    ''' <summary>
    ''' Abreviación de la unidad de empaque
    ''' </summary>
    <DataMember()>
    Public Property MeasureUnitDescription As String

    ''' <summary>
    ''' indica si la el producto maneja lote
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property HandlesBatch As Boolean

    ''' <summary>
    ''' Porcentaje de IVA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property PercentageIVA As Decimal

    ''' <summary>
    ''' Id Concepto de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionIdTax As Integer

    ''' <summary>
    ''' Porcentaje de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionPercentageTax As Decimal

    ''' <summary>
    ''' Base de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseTax As Decimal

    ''' <summary>
    ''' Id Concepto de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionIdICA As Integer

    ''' <summary>
    ''' Porcentaje de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionPercentageICA As Decimal

    ''' <summary>
    ''' Base de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseICA As Decimal

    ''' <summary>
    ''' Descripcion de insumo
    ''' </summary>
    <DataMember()>
    Public Property SupplieDescription As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DefineProfessional As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ClinicalJustification As String

    ''' <summary>
    '''  Obtienen Descripcion de la Abreviacion de la Unidad
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property InventoryMeasureUnitAbreviation As String


    ''' <summary>
    ''' indica si la el producto maneja lote
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property HanddlesMovementKardex As Boolean

    ''' <summary>
    ''' Codigo y nombre del maestro tipo de medicamento
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property MedicationTypeCodeName As String

#End Region

#Region "Product Values"

    Public Function CodeName() As String
        Return $"{Me.Code} -{Me.Name}"
    End Function

#End Region


End Class
