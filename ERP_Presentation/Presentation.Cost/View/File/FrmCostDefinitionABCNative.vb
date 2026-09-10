Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base

Public Class FrmCostDefinitionABCNative
    Implements Presentation.Base.IcrudBase

    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    Private Sub FrmCostDefinitionABC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        GridLookUpEdit1.Enabled = False
        Mensaje(Base.EeventViewerImages.Advertencia) = "Faltan parámetros para realizar este tipo de distribución"
    End Sub
End Class