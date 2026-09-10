#Region "Librerias Importadas"

Imports Domain.Entities
Imports Presentation.Base

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
Public Interface IPortfolioConciliation
    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que contien el consecutivo
    ''' </summary>
    ''' <returns></returns>
    Property Consecutive As String

    ''' <summary>
    ''' contiene o almacena el id del tercero
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyNit As Integer

    ''' <summary>
    ''' Obtiene o almacena el nombre del tercero
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyNitName As String

    ''' <summary>
    ''' contiene el numero del oficio
    ''' </summary>
    ''' <returns></returns>
    Property DocumentNumber As String

    ''' <summary>
    ''' Obtiene o almacena la fecha de la conciliacion
    ''' </summary>
    ''' <returns></returns>
    Property ConciliationDate As DateTime

    ''' <summary>
    ''' Propiedad que contiene la fecha de oficio
    ''' </summary>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Propiedad que contiene el comentario del oficio
    ''' </summary>
    Property Comment As String

    ''' <summary>
    ''' Contiene el estado de la conciliacion
    ''' </summary>
    ''' <returns></returns>
    Property State As Byte

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As PortfolioSequence

    ''' <summary>
    ''' Propiedad que contiene la fecha de corte
    ''' </summary>
    Property ClosingDate As DateTime

    ''' <summary>
    ''' Obtiene o almacena los participantes
    ''' </summary>
    ''' <returns></returns>
    Property Participants As Object

    ''' <summary>
    ''' contiene o almacena un listado de facturas seleccionadas
    ''' </summary>
    ''' <returns></returns>
    Property SelectedInvoices As Object

    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas
    ''' </summary>
    Property DataSourceInvoicesConciliation As List(Of PortfolioConciliationDetail)

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface