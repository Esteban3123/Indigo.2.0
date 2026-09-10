Imports System.Runtime.Serialization

Partial Public Class ThirdParty

#Region "Properties"

    ''' <summary>
    ''' Código y nombre de la retención iva
    ''' </summary>
    <DataMember()>
    Public Property IVARetentionConceptDescription As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Tipo de Persona
    ''' 1 - Persona Natural
    ''' 2 - Persona Juridica
    ''' </summary>
    Public Function getPersonType() As String
        Dim PersonType As String = String.Empty
        Select Case Me.PersonType
            Case 1
                PersonType = "PN"
            Case 2
                PersonType = "PJ"
        End Select
        Return PersonType
    End Function

    ''' <summary>
    ''' Tipo: Debe referenciar a una lista de códigos con los siguientes valores: • persona natural; • jurídica; • gran contribuyente; • otros;
    ''' 2017-ago-10: los valores válidos son "1" y "2" para el concepto de "tipo de organización jurídica" que utiliza el Código de Comercio: "persona jurídica" y "persona natural o física"
    ''' 1 - Persona Juridica
    ''' 2 - Persona Natural
    ''' </summary>
    Public Function getAdditionalAccountID() As String
        Dim additionalAccountID As String = String.Empty
        Select Case Me.PersonType
            Case 1
                additionalAccountID = "2"
            Case 2
                additionalAccountID = "1"
        End Select
        Return additionalAccountID
    End Function

    ''' <summary>
    ''' Tipo de Regimen IVA, de acuerdo la tabla Tipos de Persona del «Anexo 001 Formato estándar XML de la Factura, notas débito y notas crédito electrónicos»
    ''' 8.8. Tipos de Régimen IVA
    ''' 0 - Simplificado
    ''' 2 - Común
    ''' R-00-PN - No obligado a registrarse en el RUT PN
    ''' </summary>
    Public Function getRegimeType() As String
        Dim RegimeType As String = String.Empty
        Select Case Me.PersonType
            Case 0
                RegimeType = "0"
            Case Else
                RegimeType = "2"
        End Select
        Return RegimeType
    End Function

    ''' <summary>
    ''' Obtiene los códigos de las responsabilidades fiscales asociados al tercero
    ''' </summary>
    Public Function getTaxLevelCode() As String
        Dim taxLevelCodes As String = String.Empty

        If Me.ThirdPartyFiscalResponsibility IsNot Nothing AndAlso Me.ThirdPartyFiscalResponsibility.Any() Then
            taxLevelCodes = String.Join(";", Me.ThirdPartyFiscalResponsibility.Select(Function(d) d.FiscalResponsibility.Code).Distinct().ToList())
        ElseIf Me.PersonType = 1 Then
            'Si el tipo de persona es natural y no tiene asignado una responsabilidad, envio por defecto No responsable PN
            taxLevelCodes = "R-99-PN"
        End If
        Return taxLevelCodes
    End Function

    ''' <summary>
    ''' Obtiene el regimen fiscal
    ''' </summary>
    Public Function getTaxLevelName() As String
        Dim taxLevelName As String = "49"

        If Me.ThirdPartyFiscalResponsibility IsNot Nothing AndAlso Me.ThirdPartyFiscalResponsibility.Any() Then
            If Me.ThirdPartyFiscalResponsibility.Any(Function(d) d.FiscalResponsibility.Code = "O-48") Then
                taxLevelName = "48"
            End If
        End If

        Return taxLevelName
    End Function

    ''' <summary>
    ''' Obtiene la identificación del detalle tributario
    ''' </summary>
    Public Function getTaxSchemeID() As String
        Select Case ContributionType
            Case 0, 2
                Return "ZZ"
            Case Else
                Return "01"
        End Select
    End Function

    ''' <summary>
    ''' Obtiene el nombre del detalle tributario
    ''' </summary>
    Public Function getTaxSchemeName() As String
        Select Case ContributionType
            Case 0, 2
                Return "No aplica"
            Case Else
                Return "IVA"
        End Select
    End Function

    ''' <summary>
    ''' Obtiene el dígito de verificación
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDigitVerification() As String
        If DigitVerification <> CalculateVerificationCode.ToString() Then
            DigitVerification = CalculateVerificationCode.ToString()
        End If
        Return DigitVerification
    End Function

    ''' <summary>
    ''' Calula el dígito de verificación
    ''' </summary>
    ''' <returns></returns>
    Public Function CalculateVerificationCode() As Integer
        Dim nit = Format(Val("" & Me.Nit), "000000000000000")
        Dim residue As Integer = 0
        Dim mul As Integer = 0

        For i As Integer = 15 To 1 Step -1
            If i = 15 Then
                mul = 3
            ElseIf i = 14 Then
                mul = 7
            ElseIf i = 13 Then
                mul = 13
            ElseIf i = 12 Then
                mul = 17
            ElseIf i = 11 Then
                mul = 19
            ElseIf i = 10 Then
                mul = 23
            ElseIf i = 9 Then
                mul = 29
            ElseIf i = 8 Then
                mul = 37
            ElseIf i = 7 Then
                mul = 41
            ElseIf i = 6 Then
                mul = 43
            ElseIf i = 5 Then
                mul = 47
            ElseIf i = 4 Then
                mul = 53
            ElseIf i = 3 Then
                mul = 59
            ElseIf i = 2 Then
                mul = 67
            Else
                mul = 71
            End If
            residue = residue + (Val(GetChar(nit, i)) * mul)
        Next
        residue = residue Mod 11

        If residue = 0 Then
            residue = 0
        ElseIf residue = 1 Then
            residue = 1
        Else
            residue = 11 - residue
        End If

        Return residue
    End Function

#End Region

End Class
