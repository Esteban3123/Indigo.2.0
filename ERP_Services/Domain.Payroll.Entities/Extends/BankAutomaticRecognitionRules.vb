Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class BankAutomaticRecognitionRules
    Inherits Entity(Of BankAutomaticRecognitionRules)

#Region "Properties"
    ''' <summary>
    ''' Me obtiene el codigo y el nombre de la Nota de concepto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property NoteConceptCodeName As String

    ''' <summary>
    ''' Obtiene el Numero y el nombre de la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property AccountingAccountNumberName As String

    ''' <summary>
    ''' Obtiene el Código y el nombre del centro de costo
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CostCenterCodeName As String



#End Region

End Class
