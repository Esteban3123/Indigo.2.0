'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Oscar Astudillo reyes
' Created          : 2024-10-10
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
#End Region

Public Interface IPortfolioDeteriorationClassification
    Inherits ICrudBase

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value>Una cadena que representa el código de la clasificación.</value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value>Una cadena que representa el nombre asociado con la clasificación.</value>
    Property Name As String

    ''' <summary>
    ''' Obtiene o establece una descripción
    ''' </summary>
    ''' <value>Una cadena que proporciona información adicional sobre la clasificación.</value>
    Property Description As String

    ''' <summary>
    ''' Indica el estado actual del registro.
    ''' </summary>
    ''' <value>Un valor booleano que especifica si el registro está activo o inactivo.</value>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene el control de diseño asociado con esta interfaz.
    ''' </summary>
    ''' <value>Una instancia de IndigoLayoutControl utilizada para gestionar el diseño.</value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl


    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequence As PortfolioSequence

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Lista para los detalles de la aplicación de porcentajes de deterioro de cartera
    ''' </summary>
    ''' <returns></returns>
    Property ListPortfolioDeteriorationClassificationDetails As List(Of PortfolioDeteriorationClassificationDetails)
#End Region


End Interface
