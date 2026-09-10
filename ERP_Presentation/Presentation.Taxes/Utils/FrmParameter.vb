#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports Presentation.Taxes.MVP

#End Region

Public Class FrmParameter

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub
#End Region

#Region "PROPERTIES"
    Dim _listOption As List(Of Tuple(Of String, String, String))
    ReadOnly Property ListConcepts As List(Of Tuple(Of String, String, String))
        Get
            If _listOption Is Nothing Then
                _listOption = New List(Of Tuple(Of String, String, String))
                _listOption.Add(New Tuple(Of String, String, String)("%Municipio% Neiva", "Remplazar Tercero", "891180009"))
            End If
            Return _listOption
        End Get
    End Property
#End Region

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        Using formulario As New FrmPopupParameter
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub FrmParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDgc.DataSource = ListConcepts
    End Sub
End Class