'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Juan Carlos Bermudez
' Created          : 21/09/2015
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

Public Interface IAvailabilityExtension
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityPopUpXpo As XPCollection

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntitiesId As Integer

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales
    ''' </summary>
    ''' <value>
    ''' The budget entities.
    ''' </value>
    Property BudgetEntitiesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value>
    ''' The budget entities.
    ''' </value>
    Property BudgetEntitiesPopUpXpo As XPInstantFeedbackSource

End Interface
