'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Juan Carlos Bermudez
' Created          : 02/09/2015
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

Public Interface ICommitment
    Inherits IcrudBase

#Region "Properties"

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
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
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
    ''' Obtiene o establece el id de la vigencia al cual pertenece
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
    ''' codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Tipo de Compromiso
    ''' </summary>
    ''' <value>Compromiso = 1, Reserva = 2, Vigencia Futura = 3</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CommitmentType As Byte

    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyId As Integer

    ''' <summary>
    ''' Origen del Documento 
    ''' </summary>
    ''' <value>Otro = 1, Orden de Trabajo = 2, Contrato = 3</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentSource As Byte

    ''' <summary>
    ''' numero del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Document As String

    ''' <summary>
    ''' observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String

    ''' <summary>
    ''' observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AutomaticObligation As Boolean

    ''' <summary>
    ''' observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AutomaticPaymentOrder As Boolean

#End Region

#Region "Datasources"

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
    ''' Estable el datasource de los terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListThirdParty As XPInstantFeedbackSource

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
