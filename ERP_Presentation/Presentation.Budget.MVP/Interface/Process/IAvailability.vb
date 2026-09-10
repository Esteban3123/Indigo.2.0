'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jeisson Herrera Peña
' Created          : 26-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls
#End Region

Public Interface IAvailability
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
    Property BudgetEntityId As Integer?

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
    Property AvailabilityDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el tipo de disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AvailabilityType As Integer

    ''' <summary>
    ''' Obtiene o establece los días de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpirationDays As Integer

    ''' <summary>
    ''' Obtiene o establece la dependencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DependencyId As Integer

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
    Property BudgetEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetEntityXpoPopup As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityXpo As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValidityXpoPopup As DevExpress.Xpo.XPCollection

    ''' <summary>
    ''' Estable el datasource de los dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListDependency() As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de CPCCatalog
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListCPCCatalog As DevExpress.Xpo.XPCollection
#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
