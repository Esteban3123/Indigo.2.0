'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Dayan Mauricio Sabi Ospina
' Created          : 30/01/2020

'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region
Public Interface IExpenseAnnuallyClosing
    Inherits ICrudBase


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
