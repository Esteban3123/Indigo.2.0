'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Presentation.Controls

Public Interface ILevelCategory
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ' ''' <summary>
    ' ''' Obtiene o asigna la secuencia numerica del formulario
    ' ''' </summary>
    ' ''' <value>Secuencia numerica del formulario</value>
    ' ''' <returns>La secuencia numerica del formulario</returns>
    'Property Sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
End Interface
