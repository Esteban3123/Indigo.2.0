'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 05/01/2021

'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region
Public Interface IExpenseRecalculateBalances
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
