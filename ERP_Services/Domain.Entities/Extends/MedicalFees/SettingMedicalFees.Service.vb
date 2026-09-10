Imports System.Runtime.Serialization
Partial Public Class SettingMedicalFees

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto de CxP
    ''' </summary>
    <DataMember()>
    Public Property AccountPayableConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de comprobante
    ''' </summary>
    <DataMember()>
    Public Property JournalVoucherTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece de la descripcion del tipo de comprobantes de reconocimiento de costos
    ''' </summary>
    <DataMember()>
    Public Property CostRecognitionVoucherDescription As String

    ''' <summary>
    ''' Obtiene o establece de la descripcion del tipo de comprobantes de reversion de reconocimiento de costos
    ''' </summary>
    <DataMember()>
    Public Property CostRecognitionReversalVoucherDescription As String

End Class
