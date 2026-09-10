'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Dayan Mauricio Sabi Ospina
' Created          : 31/01/2020
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region
Public Interface IAnnuallyClosingIncome
    Inherits ICrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    Property BudgetEntitiesXpo As XPInstantFeedbackSource

    Property BudgetaryValidityXpo As XPCollection
End Interface
