Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IRawMaterialDevolution
    Inherits ICrudBase

    ''' <summary>
    ''' Layout del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As Date

    ''' <summary>
    ''' Campaña
    ''' </summary>
    ''' <returns></returns>
    Property CampaignDetailId As Integer?

    ''' <summary>
    ''' id del almacén de producción
    ''' </summary>
    ''' <returns></returns>
    Property ProductionWarehouseId As Integer?

    ''' <summary>
    ''' id del almacén de stock
    ''' </summary>
    ''' <returns></returns>
    Property StockWarehouseId As Integer?

    ''' <summary>
    ''' Detale
    ''' </summary>
    ''' <returns></returns>
    Property Detail As String

    ''' <summary>
    ''' status
    ''' </summary>
    ''' <returns></returns>
    Property Status As Byte

    ''' <summary>
    ''' Details
    ''' </summary>
    ''' <returns></returns>
    Property RawMaterialDevolutionDetails As List(Of RawMaterialDevolutionDetail)

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequence As MixingStationSequence

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Function LoadControls() As Task

    ''' <summary>
    ''' Asigna los valores a guardar
    ''' </summary>
    Sub AssigningValues()
End Interface
