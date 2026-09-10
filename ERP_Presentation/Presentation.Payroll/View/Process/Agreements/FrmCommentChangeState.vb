'***********************************************************************
' Assembly         : Presentacion.Payrol
' Author           : Rafael Eduardo Patiño
' Created          : 16-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : Frontal para registrar comentarios y Fecha cuando se realice cambio de estado a un convenio
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"

Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities

#End Region


Public Class FrmCommentChangeState
    Implements IcrudBase


#Region "Propiedades y Variable"

    ''' <summary>
    ''' Control de Mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Estado a Actualizar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property State As String
        Set(value As String)
            Me._state = value
            'Si es dierente a suspendido
            If value <> 3 Then
                Me.INDlciDateEndSuspend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                Me.INDlciDateEndSuspend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End Set
    End Property
    ''' <summary>
    ''' Asigna un convenio
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AgreementsC As AgreementsC
        Set(value As AgreementsC)
            Me._AgreementsC = value
        End Set
    End Property
    ''' <summary>
    ''' Variable que almacena el estado a actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _state As String

    ''' <summary>
    ''' Variable que contiene un convenio a actaulizar estado
    ''' </summary>
    Dim _AgreementsC As AgreementsC

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MAgreements

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Actualiza el estado de un convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub InvalidateConfirm()
        If ValidateControls() = False Then
            Exit Sub
        End If
        _AgreementsC.CommentChangeState = Me.INDmeCommentChangeState.Text
        If Me._state = 3 Then
            _AgreementsC.EndDateSuspend = Me.INDdeEndSuspend.EditValue
        End If
        AsyncLoader(True)
        Dim result As ActionResult(Of AgreementsC) = Await Model.SaveAgreementsC(_AgreementsC)
        If result.StateResult = True Then
            'Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
            'Me.Close()

            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Mensaje(EeventViewerImages.Informacion) = String.Join(vbCrLf, result.MessageResult)
            Else
                Mensaje(EeventViewerImages.Informacion) = result.Message
            End If

            'Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else

            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If

        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If Me._state = 3 Then
            If Me.INDdeEndSuspend.EditValue Is Nothing Then
                ValidateControls = False
            End If
        End If
        If Me.INDmeCommentChangeState.Text = String.Empty Then
            ValidateControls = False
        End If
    End Function


#End Region

#Region "Eventos"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _state = Nothing
        _AgreementsC = Nothing
        Model = Nothing
    End Sub
    ''' <summary>
    ''' Load del Formulario de Comentario Cambio Estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCommentChangeState_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDdeEndSuspend.EditValue = Date.Now.ToLongDateString
        Model = New MAgreements("595")
        Me.INDdeEndSuspend.EditValue = Date.Now
    End Sub
    ''' <summary>
    ''' Click En el boton de Aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAceptar_Click(sender As Object, e As EventArgs) Handles INDbtnAceptar.Click
        InvalidateConfirm()
    End Sub
#End Region

#Region "ICRUD Base"
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje1(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region
    
End Class