'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 18-08-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Text
#End Region


Public Class FrmOptionalParametersBankFile

#Region "PROPERTIES"
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' listado para donde se almacenan los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _optionalParameters As List(Of String)
    Public ReadOnly Property OptionalParameters As List(Of String)
        Get
            Return _optionalParameters
        End Get
    End Property

    ''' <summary>
    ''' propiedad para obtener saber con cual banco se esta trabajando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bankFileCode As String
    Public WriteOnly Property BankFileCode As String
        Set(value As String)
            _bankFileCode = value
        End Set
    End Property
#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para validar controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim errors As New StringBuilder
        Select Case _bankFileCode
            Case "002" 'banco Av Villas
                If INDTxtSourcePlace.Text Is String.Empty Then
                    errors.AppendLine(INDLciSourcePlace.Text + " vacio")
                End If
                If INDTxtDestinationPlace.Text Is String.Empty Then
                    errors.AppendLine(INDLciDestinationPlace.Text + " vacio")
                End If
        End Select
        Return errors.ToString()
    End Function
#End Region

#Region "HANDLES"
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Dim errors = ValidateControls()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Select Case _bankFileCode
            Case "002" 'banco Av Villas
                _optionalParameters = New List(Of String)
                _optionalParameters.Add(INDTxtSourcePlace.Text)
                _optionalParameters.Add(INDTxtDestinationPlace.Text)
        End Select

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub FrmOptionalParametersBankFile_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmOptionalParametersBankFile_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDTxtSourcePlace.Focus()
    End Sub
#End Region
    
End Class