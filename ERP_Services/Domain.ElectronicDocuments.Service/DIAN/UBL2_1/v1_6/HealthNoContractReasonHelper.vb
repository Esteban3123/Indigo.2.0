Imports Domain.ElectronicDocuments.Entities.UBL2_1.maindoc

Namespace DIAN.UBL2_1.v1_6

    Friend NotInheritable Class HealthNoContractReasonHelper

        Private Sub New()
        End Sub

        Public Shared Function BuildValue(contractNumber As String, noContractReason As String) As List(Of ValueTypeHealth)
            If Not String.IsNullOrEmpty(contractNumber) OrElse String.IsNullOrEmpty(noContractReason) Then
                Return Nothing
            End If

            Dim noContractReasonCode = NormalizeCode(noContractReason)

            Return New List(Of ValueTypeHealth) From
            {
                New ValueTypeHealth With
                {
                    .schemeID = noContractReasonCode,
                    .schemeName = "salud_facturaSinContrato.gc",
                    .Value = GetDescription(noContractReasonCode)
                }
            }
        End Function

        Private Shared Function NormalizeCode(noContractReasonCode As String) As String
            Dim normalizedCode = noContractReasonCode.Trim()

            If normalizedCode.Length = 1 AndAlso IsNumeric(normalizedCode) Then
                Return normalizedCode.PadLeft(2, "0"c)
            End If

            Return normalizedCode
        End Function

        Private Shared Function GetDescription(noContractReasonCode As String) As String
            Select Case noContractReasonCode
                Case "01"
                    Return "ATENCION DE URGENCIAS"
                Case "02"
                    Return "ATENCION A CARGO DE ADRES O DE ASEGURADORA SOAT"
                Case "03"
                    Return "ATENCION EN SALUD POR FALLOS DE TUTELA/ORDENES JUDICIALES"
                Case "04"
                    Return "ATENCION EN SALUD POR PORTABILIDAD O EN LOS CASOS DE ASIGNACION MASIVA DE AFILIADOS - ARTICULO 2.5.3.4.7.9 DEL DECRETO 780/2016"
                Case "05"
                    Return "ATENCION EN SALUD EN CASOS EXCEPCIONALES POR COTIZACIONES O AUTORIZACIONES SIN CONTRATO ATENCIONES ADICIONALES EXCEPCIONALES"
                Case "06"
                    Return "GESTION RECUPERACION DE ORGANOS PARA TRASPLANTE EN CUMPLIMIENTO LEY ESTATUTARIA 1751 DE 2015"
                Case Else
                    Return noContractReasonCode
            End Select
        End Function

    End Class

End Namespace
