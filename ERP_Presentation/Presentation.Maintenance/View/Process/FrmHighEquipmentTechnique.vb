Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class FrmHighEquipmentTechnique

#Region "Properties"

#End Region

#Region "Handlers"
    ''' <summary>
    ''' Handles the Load event of the FrmHighEquipmentTechnique control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmHighEquipmentTechnique_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Actualizar) = False
        AddActionColumns()
        GetData()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Adds the action columns.
    ''' </summary>
    Private Sub AddActionColumns()
        Dim ListActions As New List(Of Base.eAcciones)
        ListActions.Add(Base.eAcciones.HighTechnical)
        IndigoGridView1.SetListAcction(INDGvFixedAssetPhysicalAsset, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvFixedAssetPhysicalAsset.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Gets the data.
    ''' </summary>
    Public Sub GetData()
        INDGcFixedAssetPhysicalAsset.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).FixedAsset.ListAllFixedAssetPhysicalAssetWithOutHigh()
    End Sub
#End Region

#Region "BarButton Events"
    ''' <summary>
    ''' Barras the botones click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        GetData()
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        'Dar de alta

        Dim frm As New FrmEquipmentReceptions()
        AddHandler frm.Shown, AddressOf ShownEquipmentReceptions
        Dim transparent As New FrmTransparent(frm, False)
        transparent.ShowDialog(Me)

    End Sub

    Private Sub ShownEquipmentReceptions(sender As Object, e As EventArgs)
        Dim frm As FrmEquipmentReceptions = CType(sender, FrmEquipmentReceptions)
        Dim obj = DirectCast(CType(INDGcFixedAssetPhysicalAsset.MainView, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then

            frm.CodeEquipmentReception = CType(obj.OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetXpo).Plate
            frm.LoadControls()

            AddHandler frm.Saved, Sub()
                                      frm.Close()
                                      GetData()
                                  End Sub

        End If
    End Sub
#End Region

End Class