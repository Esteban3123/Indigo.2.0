Imports System.Runtime.Serialization

Partial Public Class AccountPayableConceptNotes

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el numero y el nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property NumberNameAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y el nombre del concepto de retención
    ''' </summary>
    <DataMember()>
    Public Property RetentionConceptCodeName As String

    ''' <summary>
    ''' Obtiene o establece el código y el nombre del concepto de retención 383
    ''' </summary>
    <DataMember()>
    Public Property RetentionConcept383CodeName As String

    ''' <summary>
    ''' Obtiene o establece el numero y el nombre de la cuenta contable de la retención 383
    ''' </summary>
    <DataMember()>
    Public Property RetentionMainAccount383NumberName As String

#End Region

End Class
