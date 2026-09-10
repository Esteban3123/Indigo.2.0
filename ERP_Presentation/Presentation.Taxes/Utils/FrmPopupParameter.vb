#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP
#End Region

Public Class FrmPopupParameter

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

#Region "PROPERTIES"
    Dim _listOperation As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListConcepts As List(Of Tuple(Of Byte, String))
        Get
            If _listOperation Is Nothing Then
                _listOperation = New List(Of Tuple(Of Byte, String))
                _listOperation.Add(New Tuple(Of Byte, String)(1, "Remplazar Tercero"))
                _listOperation.Add(New Tuple(Of Byte, String)(2, "Remplazar Nombre"))
                _listOperation.Add(New Tuple(Of Byte, String)(3, "Remplazar Dirección"))
            End If
            Return _listOperation
        End Get
    End Property
#End Region

  
    Private Sub SearchLookUpEdit1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles SearchLookUpEdit1.QueryPopUp
        If SearchLookUpEdit1.Properties.DataSource Is Nothing Then
            SearchLookUpEdit1.Properties.DataSource = ListConcepts
        End If
    End Sub

    Private Sub FrmPopupParameter_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        TextEdit1.Focus()
    End Sub
End Class