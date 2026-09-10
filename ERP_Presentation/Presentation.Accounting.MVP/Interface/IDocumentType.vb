'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Diego Andrés Roldán
' Created          : 15-01-2014
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

#End Region

''' <summary>
''' Define las propiedades y métodos de la vista
''' </summary>
Public Interface IDocumentType
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Obtiene o coloca el valor del Codigo
    ''' </summary>
    Property CodeDocumentType As String

    ''' <summary>
    ''' Obtiene o establece el valor del nombre
    ''' </summary>
    Property DescriptionDocumentType As String

    ''' <summary>
    ''' Obtiene o establece el valor de Número
    ''' </summary>
    Property ConsecutiveDocumentType As Int64
    ''' <summary>
    ''' obtiene o establece el nombre del tipo de documento
    ''' </summary>
    ''' <value>
    ''' The type of the name document.
    ''' </value>
    Property NameDocumentType As String

    ''' <summary>
    ''' Obtiene o asigna el estado del documento
    ''' </summary>
    ''' <value>Estado del documento</value>
    ''' <returns>El estado del documento</returns>
    Property StateDocumentType As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ReadOnly Property MyLayoutControl As IndigoLayoutControl


#End Region

End Interface
