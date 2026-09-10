'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/05/2014
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

#End Region

Public Interface IParameters
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece el id del comprobante cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdVoucherCxp As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del comprobante traslados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdVoucherTransfers As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del comprobante notas credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdVoucherCreditNotes As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del comprobante notas debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdVoucherDebitNotes As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del comprobante de la amortizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdVoucherAmortization As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta aprovechamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountAchievement As Long

    ''' <summary>
    ''' Contiene el listado de comprobante diario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VoucherTypeCxpXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Contiene el listado de comprobante de amortizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VoucherAmortizationXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Contiene el listado de comprobante traslados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VoucherTransfersXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Contiene el listado de comprobante notas credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VoucherCreditNotesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Contiene el listado de comprobante notas debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VoucherDebitNotesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Contiene el listado de cuenta aporvechamiento
    ''' </summary>
    ''' <value>
    ''' The account deficit xpo.
    ''' </value>
    Property AccountAchievementXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica si se encuentra habilitada la interfaz con presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetInterface As Boolean

    ''' <summary>
    ''' Especifica si es obligatoria la asociación del compromiso / obligación en la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ObligationBudgetInterface As Boolean

    ''' <summary>
    ''' Indica si la obligacion se crea usando solo los valores debitos de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ObligationDebitValue As Boolean

    ''' <summary>
    ''' Especifica si se crea una sola obligacion por el grupo de facturas asociada a la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OnlyObligation As Boolean

    ''' <summary>
    ''' Especifica si la interfaz presupuestal se postula en:
    '''     1 - La Factura
    '''     2 - La Cuenta por Pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PostulateBudgetInterfaceBy As Byte

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    Property NameMinimumAgeRange As String

    Property NameMaximumAgeRange As String
End Interface
