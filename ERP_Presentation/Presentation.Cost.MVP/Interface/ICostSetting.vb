'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 30-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities

#End Region

Public Interface ICostSetting
    Inherits IcrudBase

#Region "Properties"


    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    Sub LoadControls()

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Sub CleanControls()

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Sub AssigningValues()

    ''' <summary>
    ''' Permite saber si contabiliza costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountingCosts As Boolean

    ''' <summary>
    ''' Id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptId As Integer

    ''' <summary>
    ''' datasource conceptos de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AverageStandardCostActivity As Boolean

    ''' <summary>
    ''' Id del tipo del comprobante contable (campo: provisiones)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProvisionJournalVoucherTypeId As Integer

    ''' <summary>
    ''' datasource comprobante contable (campo: provisiones)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProvisionJournalVoucherTypeXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Id del tipo de comprobante contable (campo: reversion de provisiones)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProvisionReversalJournalVoucherTypeId As Integer

    ''' <summary>
    ''' datasource comprobante contable  (campo: reversion de provisiones)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProvisionReversalJournalVoucherTypeXpo As XPInstantFeedbackSource

#End Region

End Interface