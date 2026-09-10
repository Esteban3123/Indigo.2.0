'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Anthony Ocampo
' Created          : 26-03-2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importaciones"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Security.MVP
Imports Presentation.Controls.FormBase
Imports Presentation.Base
Imports Domain.Security.Entities

#End Region

''' <summary>
''' Clase que controla los comportamientos del formulario FrmElectronicSignatureToken.
''' </summary>
''' 
Public Class FrmElectronicSignatureToken
    Implements IElectronicSignatureToken


#Region "Variables y y Contructor"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Esta variable es la comunicacion con el presentador.
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PUsuario

    ''' <summary>
    ''' ID del usuario logeado en el aplicativo
    ''' </summary>
    Dim _userId As Integer

    ''' <summary>
    ''' Token de firma electronica del usuario guardado en variables de sesión
    ''' </summary>
    Dim _cachedToken As Guid

    ''' <summary>
    ''' Inicializa los componentes del formulario y captura en el appconfig el idioma establecido
    ''' </summary>
    Public Sub New(ByVal userId As Integer, token As Guid)
        InitializeComponent()
        _userId = userId
        _cachedToken = token
    End Sub




#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el token de la firma electronica
    ''' </summary>
    ''' <returns></returns>
    Public Property Token As String Implements IElectronicSignatureToken.Token
        Get
            Return INDTeToken.EditValue
        End Get
        Set(value As String)
            INDTeToken.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para mostrar mensaje en pantalla
    ''' </summary>
    Public WriteOnly Property Mensaje As String Implements IElectronicSignatureToken.Mensajes
        Set(value As String)
            MessageIndigo.Show(value, MessageType.Information, "Resultado", Botones.Ok)
        End Set
    End Property





#End Region

#Region "Metodos"
    ''' <summary>
    ''' Obtiene el token del usuario si existe
    ''' </summary>
    Private Async Sub LoadToken()
        If Not _cachedToken.Equals(Guid.Empty) Then
            Token = _cachedToken.ToString()
            Exit Sub
        End If
        Using model As New MUsuario
            Dim res = Await model.GetElectronicSignatureTokenAsync(_userId)
            If Not String.IsNullOrEmpty(res) Then
                Token = res
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Guarda el token ingresado por el usuario
    ''' </summary>
    Public Async Sub Guardar()
        Try
            Using model As New MUsuario()
                Dim user As New User With {.ElectronicSignatureToken = Token}
                Dim saveResult = Await model.SaveElectronicSignatureToken(Token, _userId)

                If saveResult.StateResult Then
                    If Not String.IsNullOrEmpty(user.ElectronicSignatureToken) Then
                        IndigoSingleton.IndigoValoresSesion.Instancia.User_token = user.ParseToken()
                        Token = Nothing
                        Mensaje = saveResult.MessageResult(0)
                        Me.Close()
                    Else
                        IndigoSingleton.IndigoValoresSesion.Instancia.User_token = Nothing
                        Token = Nothing
                        Mensaje = "Token guardado sin contenido."
                        Me.Close()
                    End If
                Else
                    Mensaje = saveResult.MessageResult.ToString()
                End If
            End Using
        Catch ex As Exception
            INDTeToken.Enabled = False
            Throw ex
        End Try
    End Sub
#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Evento que se dispara al abrirse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmElectronicSignatureToken_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadToken()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el botón Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbSave_Click(sender As Object, e As EventArgs) Handles INDSbSave.Click
        Guardar()
    End Sub
#End Region
#End Region

End Class