'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-08-01
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
Public Interface IAnnualClose
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets or sets the legal book.
    ''' </summary>
    ''' <value>
    ''' The month.
    ''' </value>
    Property LegalBookId As Integer

    ''' <summary>
    ''' Gets or sets the year.
    ''' </summary>
    ''' <value>
    ''' The year.
    ''' </value>
    Property Year As Integer

End Interface
