#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IGlosaMedicalFees
    Inherits ICrudBase

#Region "properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del Registro
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Fecha del documento con el cual se hará interfaces
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Esta propiedad contiene el ID del proveedor
    ''' </summary>
    Property SupplierId As Integer

    ''' <summary>
    ''' Id de las lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    Property SupplierDistributionLineId As Integer

    ''' <summary>
    ''' Esta propiedad contiene el ID del proveedor
    ''' </summary>
    Property GlosaMedicalFeesConceptId As Integer

    ''' <summary>
    ''' Esta propiedad contiene cuadro de Observacion
    ''' </summary>
    Property Observation As String

    ''' <summary>
    ''' Valor Pendiente
    ''' </summary>
    ''' <returns></returns>
    Property PendingValue As Decimal

    ''' <summary>
    ''' Valor Pendiente
    ''' </summary>
    ''' <returns></returns>
    Property UnitValue As Decimal

    ''' <summary>
    ''' Valor Pendiente
    ''' </summary>
    ''' <returns></returns>
    Property Quantity As Integer

    ''' <summary>
    ''' Valor Glosado
    ''' </summary>
    ''' <returns></returns>
    Property GlossedValue As Decimal

    ''' <summary>
    ''' Valor total
    ''' </summary>
    ''' <returns></returns>
    Property TotalValue As Decimal

    ''' <summary>
    ''' contiene el estado de la Glosa de Honorarios Medicos
    ''' </summary>
    ''' <returns></returns>
    Property Status As Byte

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MedicalFeesSecuence

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

#End Region

#Region "XPO"

    ''' <summary>
    ''' Lista de proveedores
    ''' </summary>
    ''' <returns></returns>
    Property ListAccountPayable As XPInstantFeedbackSource

    ''' <summary>
    ''' lista de tipo de conceptos
    ''' </summary>
    ''' <returns></returns>
    Property ListGlosaMedicalFeesConcepts As XPInstantFeedbackSource

    ''' <summary>
    ''' Lista los supplier por lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

#End Region

End Interface
