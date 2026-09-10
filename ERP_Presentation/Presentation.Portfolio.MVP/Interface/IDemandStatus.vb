'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-08-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
#End Region

Public Interface IDemandStatus
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del estado de demanda
    ''' </summary>
    Property Code As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del estado de demanda
    ''' </summary>
    Property Description As String
    ''' <summary>
    ''' Esta Propiedad contiene el Estado del estado de demanda
    ''' </summary>
    Property Status As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

#End Region

End Interface
