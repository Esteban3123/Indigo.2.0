'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
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

Public Interface IProvisionRanges
    Inherits IcrudBase

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
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
    Property Sequense As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' obtiene o establece el codigo
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String
    ''' <summary>
    ''' obtiene o establece el numero inicial
    ''' </summary>
    ''' <value>
    ''' The initial.
    ''' </value>
    Property Initial As Integer
    ''' <summary>
    ''' obtiene o establece el numero final
    ''' </summary>
    ''' <value>
    ''' The final.
    ''' </value>
    Property Final As Integer
    ''' <summary>
    ''' obtiene o establece el estado
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As Boolean
    ''' <summary>
    ''' establece el eestado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
End Interface
