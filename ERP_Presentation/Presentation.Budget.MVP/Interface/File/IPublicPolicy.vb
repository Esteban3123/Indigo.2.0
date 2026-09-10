'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2021-01-13
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IPublicPolicy
    Inherits ICrudBase

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
    Property Sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <returns></returns>
    Property NamePP As String

    ''' <summary>
    ''' Obtiene o establece la observación
    ''' </summary>
    ''' <returns></returns>
    Property Observation As String

End Interface
