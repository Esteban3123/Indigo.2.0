'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 29-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 30-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports System.Configuration
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources


#End Region
''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal FrmConfigurarConexion
''' </summary>
Public Class PConfigurarConexion

#Region "Variables y Constructor"
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IConfigurarConexion
    ''' </summary>
    Dim vista As IConfigurarConexion

    ''' <summary>
    ''' Establece la comunicacion con la interfaz IconfigurarConexion.
    ''' </summary>
    ''' <param name="iview">The vista.</param>
    Public Sub New(ByRef iview As IConfigurarConexion)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo para validar los campos del formulariod de configuracion de conexion .
    ''' </summary>
    Public Function ValidarCampos() As Boolean
        If String.IsNullOrEmpty(CStr(vista.Ciudad)) Or String.IsNullOrEmpty(vista.UrlServidor) Or String.IsNullOrEmpty(vista.UrlServidorEntidades) Or String.IsNullOrEmpty(vista.UrlServidorNotificacion) Or String.IsNullOrEmpty(vista.UrlServidorIndexacion) Or String.IsNullOrEmpty(vista.UrlServidorSistemaDocumental) Or String.IsNullOrEmpty(vista.ProtocoloUrlServidor) Or String.IsNullOrEmpty(vista.ProtocoloUrlServidorEntidades) Or String.IsNullOrEmpty(vista.ProtocoloUrlServidorIndexacion) Or String.IsNullOrEmpty(vista.ProtocoloUrlServidorSistemaDocumental) Or String.IsNullOrEmpty(vista.idioma) Or String.IsNullOrEmpty(vista.EmpresaIndigoERP) Or String.IsNullOrEmpty(vista.RutaReportesPersonalizados) Then
            vista.Mensajes = obtenerRecurso(ComunesDigiteDatos, Conexion) ' "Debe Diligenciar los Datos Solicitados."
            Return False
        End If
        Return True
    End Function

#End Region





End Class
