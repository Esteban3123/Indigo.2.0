'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
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

Public Interface IModificationObligations
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntitiesId As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntityIdPopup As Integer?

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityIdPopup As Integer?

    ''' <summary>
    ''' Obtiene o estable el código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As Date

    ''' <summary>
    ''' Obtiene o establece el id de la obligación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ObligationId As Integer

    ''' <summary>
    ''' Tipo de documento hasta el cual se modifica el recurso
    ''' </summary>
    ''' <value>
    ''' 1 - Obligacion
    ''' 2 - Compromiso
    ''' 3 - Disponibilidad
    ''' 4 - Presupuesto
    ''' </value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UpTo As Byte

    ''' <summary>
    '''  Obtiene o establece el documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Document As String

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Obtiene o establece el listado de las disponibilidades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ObligationXpo As XPInstantFeedbackSource

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
