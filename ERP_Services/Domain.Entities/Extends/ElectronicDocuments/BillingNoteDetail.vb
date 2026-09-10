Partial Public Class BillingNoteDetail

#Region "Properties ElectronicBilling"

    ''' <summary>
    ''' Obtiene la version de facturación de la factura
    ''' </summary>
    Public DianVersion As Decimal

    ''' <summary>
    ''' Obtiene el estado del documento electronico
    ''' </summary>
    Public Status As Byte

    ''' <summary>
    ''' Detalle de las notas de cartera de tipo detalle
    ''' </summary>
    Public NoteTypeDetails As List(Of NoteTypeDetail)

    ''' <summary>
    ''' 
    ''' </summary>
    Public InvoiceMoreInformation As SP_GetInvoiceMoreInformationByInvoiceId_Result

#End Region

#Region "Methods ElectronicBilling"

    Public Function GetResponseCodeName(Nature As Integer) As String
        Dim responseCodeName = String.Empty

        If Nature = 1 Then
            Select Case Me.ConceptId
                Case 1
                    responseCodeName = "Intereses"
                Case 2
                    responseCodeName = "Gastos por cobrar"
                Case 3
                    responseCodeName = "Cambio del valor"
            End Select
        ElseIf Nature = 2 Then
            Select Case Me.ConceptId
                Case 1
                    responseCodeName = "Devolución de parte de los bienes; no aceptación de partes del servicio"
                Case 2
                    responseCodeName = "Anulación de factura electrónica"
                Case 3
                    responseCodeName = "Rebaja total aplicada"
                Case 4
                    responseCodeName = "Descuento total aplicado"
                Case 5
                    responseCodeName = "Rescisión: nulidad por falta de requisitos"
                Case 6
                    responseCodeName = "Otros"
            End Select
        End If

        Return responseCodeName
    End Function

#End Region

End Class
