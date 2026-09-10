'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "imports"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

''' <summary>
''' Interfaz que contiene las propiedades del frontal de Ciudades
''' </summary>
Public Interface IDistributionLines
    Inherits IcrudBase

    ''' <summary>
    ''' Codigo de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Nombre de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameDL As String

    ''' <summary>
    ''' Xpo de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount As Integer?

    ''' <summary>
    ''' Xpo de conceptos de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PaymentConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdPaymentConcept As Integer?

    ''' <summary>
    ''' Xpo de conceptos de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptId As Integer?

    '''<sumary>
    ''' XPO de cuentas contable provision
    ''' </sumary>
    '''
    Property AccountCostProvision As XPInstantFeedbackSource

    '''<sumary>
    '''  Id del cuenta contable provision
    ''' </sumary>
    '''
    Property IdAccountCostProvisionId As Integer?


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
    Property Sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatingUnitId As Integer?

    ''' <summary>
    ''' Establece el datasource de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatingUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConceptType As Integer?

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Concepto de acreencia
    ''' </summary>
    ''' <returns></returns>
    Property AccusationConcept As Integer?

    ''' <summary>
    ''' Medicion posterior del instrumento financiero
    ''' </summary>
    ''' <returns></returns>
    Property FinancialInstrument As Integer?

    ''' <summary>
    ''' Propiedad que alberga el datasource de la rejilla de 
    ''' concepto de nota de cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    Property AccountPayableConceptNote As XPInstantFeedbackSource

    Property MainAccountsXpo As XPInstantFeedbackSource

    Property MainAccountAccountPayableConceptNotesId As Integer

End Interface
