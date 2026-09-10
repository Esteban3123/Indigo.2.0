'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/11/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

''' <summary>
''' Define las propiedades y métodos de la vista
''' </summary>
Public Interface IAccountingHomologations
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookId As Integer?

    ''' <summary>
    ''' Establece el datasource del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el id del libro de homologacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HomologationBookId As Integer?

    ''' <summary>
    ''' Establece el datasource del libro de homologacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HomologationBookXpo As XPInstantFeedbackSource

#End Region

End Interface
