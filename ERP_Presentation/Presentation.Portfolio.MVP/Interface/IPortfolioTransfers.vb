'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq

#End Region

Public Interface IPortfolioTransfers
    Inherits ICrudBase

#Region "Properties"
    ''' <summary>
    ''' codigo de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As Date?
    ''' <summary>
    ''' id del Cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerId As Integer?
    ''' <summary>
    ''' id del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PortfolioAdvanceId As Integer?
    ''' <summary>
    ''' id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountId As Integer?
    ''' <summary>
    ''' id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterId As Integer?
    ''' <summary>
    ''' observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String
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
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Id de la moneda en la que maneja el anticipo
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer?

    ''' <summary>
    ''' Codigo standart de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property CurrencyAbbreviation As String
#End Region
#Region "DataSource"
    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center xpo.
    ''' </value>
    Property CostCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' obtiene o estable las facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillsXPO As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' obtiene o establece los anticipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdvanceXPO As DevExpress.Xpo.XPInstantFeedbackSource

    Property CustomerXPO As DevExpress.Xpo.XPInstantFeedbackSource

    Property NoteConceptXpo As XPInstantFeedbackSource

    Property AccountsXPO As XPInstantFeedbackSource

    Property CostCenterConceptXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyXpo As XPInstantFeedbackSource
#End Region
End Interface
