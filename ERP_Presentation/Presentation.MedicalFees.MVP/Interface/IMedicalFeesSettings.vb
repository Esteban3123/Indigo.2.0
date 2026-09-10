'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
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
#End Region

Public Interface IMedicalFeesSettings
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource de los conceptos de pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobantes de reconocimiento de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostRecognitionVoucherId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobantes de reversion de reconocimiento de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostRecognitionReversalVoucherId As Integer?

End Interface
