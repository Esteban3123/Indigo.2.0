Imports System.Runtime.Serialization

Partial Public Class Person

#Region "Properties"

    <DataMember>
    Property CityDescription As String

    <DataMember>
    Property IdentificationTypeName As String
    ''' <summary>
    '''Abreviación del tipo de documento (Siglas)
    ''' </summary>
    <DataMember>
    Property DocumentTypeAbbreviation As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Tipo de adquirente, de acuerdo la tabla Tipos de Persona del «Anexo 001 Formato estándar XML de la Factura, notas débito y notas crédito electrónicos»
    ''' 8.1.1. Tipos de documentos de identidad
    ''' R-00-PN - No obligado a registrarse en el RUT PN
    ''' 10 - Certificado Nacido Vivo
    ''' 11 - Registro civil
    ''' 12 - Tarjeta de identidad
    ''' 13 - Cédula de ciudadanía
    ''' 21 - Tarjeta de extranjería
    ''' 22 - Cédula de extranjería
    ''' 31 - NIT
    ''' 41 - Pasaporte
    ''' 42 - Documento de identificación extranjero
    ''' 91 - NUIP
    ''' </summary>
    Public Function getAcquirerType() As String
        Dim AcquirerType As String = String.Empty
        If String.IsNullOrEmpty(DocumentTypeAbbreviation) Then
            Select Case IdentificationType
                Case 0
                    AcquirerType = "13"
                Case 1
                    AcquirerType = "22"
                Case 2
                    AcquirerType = "12"
                Case 3
                    AcquirerType = "11"
                Case 4
                    AcquirerType = "41"
                Case 5 To 6
                    AcquirerType = "91"
                Case 7
                    AcquirerType = "31"
                Case 8 To 9
                    AcquirerType = "91"
                Case 10 To 14
                    AcquirerType = "42"
            End Select
        Else
            Select Case DocumentTypeAbbreviation
                Case "CN"
                    AcquirerType = "10"
                Case "CC"
                    AcquirerType = "13"
                Case "TI"
                    AcquirerType = "12"
                Case "RC"
                    AcquirerType = "11"
                Case "CE"
                    AcquirerType = "22"
                Case "NI"
                    AcquirerType = "31"
                Case "PA"
                    AcquirerType = "41"
                Case "DE", "SC", "PE"
                    AcquirerType = "42"
                Case "NU"
                    AcquirerType = "91"
                Case "TE"
                    AcquirerType = "21"
                Case "PT"
                    AcquirerType = "48"
            End Select
        End If
        Return AcquirerType
    End Function

    ''' <summary>
    ''' Obtiene el tipo de identificación fiscal requerida por la DIAN
    ''' 21 Tarjeta de extranjería                   NoResidente
    ''' 22 Cédula de extranjería                    NoResidente
    ''' 31 NIT                                      Residente
    ''' 41 Pasaporte                                NoResidente
    ''' 42 Documento de identificación extranjero   NoResidente
    ''' 47 PEP                                      NoResidente
    ''' 50 NIT de otro país                         NoResidente
    ''' </summary>
    ''' <returns></returns>
    Public Function getAcquirerTypeSupportDocument() As String
        Select Case IdentificationType
            Case 1
                Return "22"
            Case 10 To 12
                Return "42"
            Case Else
                Return "31"
        End Select
    End Function

#End Region

End Class
