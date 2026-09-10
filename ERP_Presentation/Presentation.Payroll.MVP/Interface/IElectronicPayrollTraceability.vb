#Region "Imports"

Imports DevExpress.Xpo

#End Region

Public Interface IElectronicPayrollTraceability

#Region "Fields"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource de los soportes de pago de nomina electronica
    ''' </summary>
    ''' <returns></returns>
    Property ElectronicPayrollPaymentSupportXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las Notas de Ajuste
    ''' </summary>
    ''' <returns></returns>
    Property AdjustmentNoteXpo As XPInstantFeedbackSource

#End Region

End Interface
