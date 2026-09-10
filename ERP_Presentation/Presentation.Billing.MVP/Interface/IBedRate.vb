'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego A. Roldán
' Created          : 23-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

Public Interface IBedRate
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Id de la Cama
    ''' </summary>
    Property CODICAMAS As Integer

    ''' <summary>
    ''' Codigo Tipo de Estancia
    ''' </summary>
    Property CODTIPEST As String

    ''' <summary>
    ''' Tipo de liquidacion de estancia
    '''1: Observacion Urgencias
    '''2: Recuperacion Post-Quirurgico
    '''3: Hospitalaria
    ''' </summary>
    Property TIPLIQEST As Byte

    ''' <summary>
    ''' Numero de Horas Minima de Estancia
    ''' </summary>
    Property NUMHOREST As Byte

    ''' <summary>
    ''' Codigo CUPS de Vie
    '''1. Si el tipo de liquidacion es 1 - Urgencias, se especifica el codigo CUPS para liquidacion de Observacion. Solo se diligencia este campo en tipo liquidacion 1
    ''' </summary>
    Property GENCUPS As Integer?

    ''' <summary>
    ''' Codigo CUPS Vie
    '''1. Si el tipo de liquidacion es 1 - Urgencias, se especifica el codigo CUPS para la liquidacion de Hospitalizacion de urgencias.
    '''2. Si el tipo de liquidacion es 2 - Hospitalizacion, se especifica el codigo CUPS para la liquidacion de Hospitalizacion.
    ''' </summary>
    Property GENCUPS2 As Integer

    ''' <summary>
    ''' Codigo del Centro de Atencion (Crystal)
    ''' </summary>
    Property CODCENATE As String

    ''' <summary>
    ''' Codigo de la Unidad Funcional (Crystal)
    ''' </summary>
    Property UFUCODIGO As String

    Property CODTIPESTXpo As XPInstantFeedbackSource

    Property GENCUPSXpo As XPInstantFeedbackSource

    Property GENCUPS2Xpo As XPInstantFeedbackSource

    Property CODCENATEXpo As XPInstantFeedbackSource

    Property UFUCODIGOXpo As XPInstantFeedbackSource

#End Region


End Interface