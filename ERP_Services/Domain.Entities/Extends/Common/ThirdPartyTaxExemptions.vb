Imports System.Runtime.Serialization

Partial Public Class ThirdPartyTaxExemptions

    ''' <summary>
    ''' Código Y Nombre de la exoneracion tributaria de tipo Documento
    ''' </summary>
    <DataMember()>
    Public Property DocumentTypeCodeName As String

    ''' <summary>
    ''' Código Y Nombre de la exoneracion tributaria de tipo Institucion
    ''' </summary>
    <DataMember()>
    Public Property InstitutionCodeName As String

    ''' <summary>
    ''' codigo y nombre del registro de IVA seleccionado
    ''' </summary>
    <DataMember()>
    Public Property ExemptFeeCodeName As String

End Class
