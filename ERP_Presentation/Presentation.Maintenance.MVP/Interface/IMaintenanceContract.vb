#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IMaintenanceContract
    Inherits ICrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del contrato
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el ID del tipo de contrato
    ''' </summary>
    Property ContractTypeId As Integer

    ''' <summary>
    ''' Fecha del documento con el cual se hará interfaces
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Fecha inicial del contrato
    ''' </summary>
    ''' <returns></returns>
    Property InitialDate As DateTime?

    ''' <summary>
    ''' Fecha final del contrato
    ''' </summary>
    ''' <returns></returns>
    Property EndDate As DateTime?

    ''' <summary>
    ''' Descripcion
    ''' </summary>
    ''' <returns></returns>
    Property Description As String

    ''' <summary>
    ''' Esta propiedad contiene el numero de contrato
    ''' </summary>
    Property ContractNumber As String

    ''' <summary>
    ''' Esta propiedad contiene el Id del proveedor
    ''' </summary>
    Property SupplierId As Integer

    ''' <summary>
    '''  Esta propiedad contiene el Id de la linea de distribucion del proveedor
    ''' </summary>
    ''' <returns></returns>
    Property SupplierDistributionLineId As Integer

    ''' <summary>
    ''' si el contrato tiene exclusividad o no
    ''' </summary>
    Property Exclusivity As Boolean

    ''' <summary>
    ''' Esta Propiedad  Especifica el origen segun de la Cuantia
    ''' </summary>
    Property SourceOrder As Byte

    ''' <summary>
    ''' Especifica si el contrato maneja garantia unica
    ''' </summary>
    ''' <returns></returns>
    Property OnlyGuarantee As Boolean

    ''' <summary>
    '''  Esta Propiedad contiene Supervicion Técnica
    ''' </summary>
    ''' <returns></returns>
    Property TechnicalSupervicion As String


    ''' <summary>
    ''' Esta Propiedad contiene Supervicion de la ejecución
    ''' </summary>
    ''' <returns></returns>
    Property SupervisionExecution As String


    ''' <summary>
    ''' esta propiedad contiene las clausulas del contrato
    ''' </summary>
    ''' <returns></returns>
    Property Clauses As String

    ''' <summary>
    ''' esta propiedad contiene los anexos del contrato
    ''' </summary>
    ''' <returns></returns>
    Property Attachments As String

    ''' <summary>
    ''' contiene el estado del contrato
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
    Property Sequense As Domain.Entities.MaintenanceSequence

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
    Property ListSupplier As XPInstantFeedbackSource

    ''' <summary>
    ''' lista de tipo de contratos
    ''' </summary>
    ''' <returns></returns>
    Property ListContractType As XPInstantFeedbackSource


    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

#End Region

End Interface
