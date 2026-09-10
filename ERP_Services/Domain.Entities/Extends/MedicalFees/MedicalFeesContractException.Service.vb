Imports System.Runtime.Serialization
Partial Public Class MedicalFeesContractException

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de excepcion
    ''' </summary>
    <DataMember()>
    Public Property ExceptionDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la tarifa
    ''' </summary>
    <DataMember()>
    Public Property RateDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la tarifa
    ''' </summary>
    <DataMember()>
    Public Property RateManualDescription As String

End Class
