'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 11-04-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports System.Security.Cryptography
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Base   
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Resources
#End Region
''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal FrmCambiarContraseña
''' </summary>
Public Class PCambiarContrasena


#Region "variable y Constructor"

    ''' <summary>
    ''' Constructor que permite la comunicacion con la interfaz ICambiarContraseña
    ''' </summary>
    Public Sub New(ByRef iview As ICambiarContraseña)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub

    ''' <summary>
    ''' Variable utilizada para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IDesbloquearUsuario
    ''' </summary>
    Dim vista As ICambiarContraseña
    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo 
    ''' </summary>
    Dim modelo As MCambiarContraseña
#End Region

#Region "metodos"

    ''' <summary>
    ''' Este metodo Cambia la contraseña si se realizo todo correctamente.
    ''' </summary>
    Public Async Function CambiarContrasena(ByVal idUser As String) As Threading.Tasks.Task
        Using modelo As New MCambiarContraseña
            With vista
                If String.IsNullOrEmpty(vista.ContraseñaAnterior) And String.IsNullOrEmpty(.NuevaContraseña) And String.IsNullOrEmpty(.ConfirmarContraseña) Then
                    .Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaAnteriorVacia, Eform.CambiarContrasena)
                Else
                    If Not String.IsNullOrEmpty(.NuevaContraseña) Or Not String.IsNullOrEmpty(.ConfirmarContraseña) Then
                        If .NuevaContraseña <> .ConfirmarContraseña Then
                            vista.Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenasNoCoinciden, Eform.CambiarContrasena)
                            .NuevaContraseña = String.Empty
                            .ConfirmarContraseña = String.Empty
                            Exit Function
                        End If
                    Else

                        If Await modelo.ValidarUsuario(idUser, .ContraseñaAnterior) = True Then
                            vista.LogicaBotonActualizar(True)
                            .HabilitarControles = True
                            Exit Function
                        Else
                            .Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaNoCorrecta, Eform.CambiarContrasena)
                            Exit Function
                        End If

                    End If
                    If .ContraseñaAnterior = String.Empty Then
                        .ContraseñaAnterior = Await modelo.GetPasswordAsync(idUser.Trim)
                        If .ContraseñaAnterior = String.Empty Then
                            .Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("FaildPasswordChangue")
                            Exit Function
                        End If
                    End If
                    Dim resultado = Await modelo.CambiarContraseña(idUser.Trim, .ContraseñaAnterior, .NuevaContraseña)
                    If resultado = True Then
                        .Mensaje(EeventViewerImages.Informacion) = BaseClass.obtenerRecurso(Eresources.ContrasenaGuardadaCorrectamente, Eform.CambiarContrasena)
                    Else
                        .Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("FaildPasswordChangue")
                    End If
                    Deshacer()
                End If
            End With
        End Using
    End Function

    ''' <summary>
    ''' Este metodo deja los campos vacios del frontal.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer()
        With vista
            .ConfirmarContraseña = String.Empty
            .NuevaContraseña = String.Empty
            .ContraseñaAnterior = String.Empty
            .HabilitarControles = False

        End With
    End Sub

    ''' <summary>
    ''' Inicializa los componentes iniciales.
    ''' </summary>
    Public Sub InicializarComponentes()
        vista.HabilitarControles = False
        vista.EstablecerFoco("INDTxtCoAnterior") = True
    End Sub

#End Region


End Class
