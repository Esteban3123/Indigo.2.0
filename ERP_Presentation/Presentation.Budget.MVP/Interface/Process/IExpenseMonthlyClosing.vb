'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Juan Carlos Bermudez
' Created          : 25/09/2015
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

Public Interface IExpenseMonthlyClosing
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' listado de entidades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntitiesXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' listado de vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection

End Interface
