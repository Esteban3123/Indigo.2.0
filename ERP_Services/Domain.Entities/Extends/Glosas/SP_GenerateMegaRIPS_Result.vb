Imports System.Runtime.Serialization
Imports System.Text

Partial Public Class SP_GenerateMegaRIPS_Result

#Region "Manual Properties"



#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera una cadena concatenada de todos los campos
    ''' </summary>
    ''' <returns>Cadena concatenada de los campos</returns>
    Public Function FieldsToString() As String
        Try
            Dim result As New StringBuilder()
            result.Append(IsNullToEmpty(Me.Prefijo))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Nume_Fac))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Tipo_Doc_IPS))
            result.Append("|")
            result.Append(If(Me.Num_Cod_IPS.HasValue, Me.Num_Cod_IPS.Value.ToString("0"), ""))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Cod_Hab_IPS))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Cod_Eps))
            result.Append("|")
            result.Append(If(Me.Cod_Cuenta.HasValue, Me.Cod_Cuenta.Value, "0"))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Cod_Contrato))
            result.Append("|")
            result.Append(Me.Fecha_Fact.ToString("dd/MM/yyyy"))
            result.Append("|")
            result.Append(Me.ValorBruto.ToString("0.##"))
            result.Append("|")
            result.Append(If(Me.Copago.HasValue, Me.Copago.Value.ToString("0.##"), ""))
            result.Append("|")
            result.Append(Convert.ToDecimal(Me.Valor_Copago_Compartido).ToString("0.##"))
            result.Append("|")
            result.Append(Convert.ToDecimal(Me.Valor_Iva).ToString("0.##"))
            result.Append("|")
            result.Append(Convert.ToDecimal(Me.Valor_Ico).ToString("0.##"))
            result.Append("|")
            result.Append(Me.Valor_Moderadora.ToString("0.##"))
            result.Append("|")
            result.Append(Me.Descuento.ToString("0.##"))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Con_Des))
            result.Append("|")
            result.Append(Me.Valor_Neto.ToString("0.##"))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Periodo))
            result.Append("|")
            result.Append(Me.Cod_Regional.ToString())
            result.Append("|")
            result.Append(Me.Clasificacion_Origen.ToString())
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Tipo_Servicio))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Tipo_Paquete))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Fin_Consulta))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Dias_Trat))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Tdoc_Paciente))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Ndoc_Paciente))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Nombre))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.SegNombre))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Apellido))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.SegApellido))
            result.Append("|")
            result.Append(If(Me.Edad.HasValue, Me.Edad.Value, 0))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Sexo))
            result.Append("|")
            result.Append(Me.Estado_Paciente.ToString())
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Discapacidad))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Tipo_Prestacion))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Codigo_facturacion_principal))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Cod_procedi_Detalle))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.Descripcion_procedi))
            result.Append("|")
            result.Append(Me.FechaProcedi.ToString("dd/MM/yyyy"))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.HoraProcedi))
            result.Append("|")
            result.Append(Me.CantidadProcedi.ToString())
            result.Append("|")
            result.Append(Me.ValorUnitario.Value.ToString("0.##"))
            result.Append("|")
            result.Append(Me.VALOR_COMPARTIDO_PACIENTE.ToString())
            result.Append("|")
            result.Append(If(Me.VALOR_MODERADORA_PACIENTE.HasValue, Me.VALOR_MODERADORA_PACIENTE.ToString(), ""))
            result.Append("|")
            result.Append(If(Me.VALOR_COPAGO_PACIENTE.HasValue, Me.VALOR_COPAGO_PACIENTE.ToString(), ""))
            result.Append("|")
            result.Append(Me.ValorTotalServicio.Value.ToString("0.##"))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.CodAutorizacion))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.DiagnosticoPrincipal))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.TIPO_DIAG))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.DIAGNOSTICO_SECUNDARIO_1))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.DIAGNOSTICO_SECUNDARIO_2))
            result.Append("|")
            result.Append(Me.FECHA_ENTRADA.ToString("dd/MM/yyyy"))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.HORA_ENTRADA))
            result.Append("|")
            result.Append(If(Me.FECHA_SALIDA.HasValue, Me.FECHA_SALIDA.Value.ToString("dd/MM/yyyy"), ""))
            result.Append("|")
            result.Append(IsNullToEmpty(Me.HORA_SALIDA))
            Return result.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Si la cadena de entrada es nula, retorna un vacio.
    ''' </summary>
    ''' <param name="text">Cadena de entrada</param>
    ''' <returns>Cadena de salida sin espacios en blanco al inicio y al final</returns>
    Private Function IsNullToEmpty(ByVal text As String) As String
        If text Is Nothing Then
            Return String.Empty
        Else
            Return text.Trim()
        End If
    End Function

#End Region

End Class