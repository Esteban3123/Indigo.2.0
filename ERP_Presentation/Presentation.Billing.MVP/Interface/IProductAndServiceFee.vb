#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
#End Region

Public Interface IProductAndServiceFee
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>PortfolioSequence
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Obtiene o establece el consecutivo 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor del control de acciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource
#End Region
End Interface
