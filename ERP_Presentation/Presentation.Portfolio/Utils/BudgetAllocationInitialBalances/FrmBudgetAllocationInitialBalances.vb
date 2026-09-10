'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 17/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.CrossThreadExtentions
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms
Imports System.Text
Imports Presentation.Controls.MVP
Imports DevExpress.Data.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Class FrmBudgetAllocationInitialBalances

#Region "GLOBALS"
    ''' <summary>
    ''' listado de los items donde se almacena el Id del PortfolioInitialBalanceAccountReceivable y el id del presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Dim listItemsProcess As New List(Of Tuple(Of Integer, Integer))
#End Region

#Region "PROPERTIES"
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
#End Region

#Region "CRUD"

#End Region

#Region "METHODS"
    Private Sub CleanControls()
        listItemsProcess = New List(Of Tuple(Of Integer, Integer))
        INDSleInitialBalance.EditValue = Nothing
        INDGcBills.DataSource = Nothing
        INDGvBills.ClearColumnsFilter()
        IndigoGridControl1.RefreshGrid(INDGcBills)
    End Sub
#End Region

#Region "HANDLES"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listItemsProcess = Nothing
    End Sub

    Private Sub FrmBudgetAllocationInitialBalances_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        CleanControls()
    End Sub

    Private Sub FrmBudgetAllocationInitialBalances_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Using model As New MBudgetAllocationInitialBalances(Me.Tag)
            INDSleInitialBalance.Properties.DataSource = model.GetInitialBalanceByStatus()
        End Using
        INDSleInitialBalance.Properties.PopupFormMinSize = New System.Drawing.Size(INDSleInitialBalance.Size.Width - 11, 0)
        INDSleInitialBalance.Properties.PopupFormSize = New System.Drawing.Size(INDSleInitialBalance.Size.Width - 11, 0)
        INDSleInitialBalance.Focus()
    End Sub

    Private Sub INDSleInitialBalance_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleInitialBalance.EditValueChanged
        If INDSleInitialBalance.EditValue IsNot Nothing Then
            Using model As New MBudgetAllocationInitialBalances(Me.Tag)
                Dim listTmp = model.ListViewBudgetAllocationInitialBalances(INDSleInitialBalance.EditValue)
                If listTmp.Count > 0 Then
                    INDGcBills.DataSource = listTmp
                Else
                    INDGcBills.DataSource = Nothing
                    IndigoGridControl1.RefreshGrid(INDGcBills)
                End If


            End Using
        End If
    End Sub

    Private Async Sub INDBbiProcess_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcess.ItemClick
        Dim save = BarraBotones.PermissionsForm.Where(Function(x) x.Key = 2).FirstOrDefault()
        If save.Key = 0 And save.Value Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No tiene permiso para procesar la información"
            Exit Sub
        End If
        If listItemsProcess.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se han seleccionado items para procesar"
            Exit Sub
        End If
        Try
            Using model As New MBudgetAllocationInitialBalances(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.SaveBudgetAllocationInitialBalances(listItemsProcess, INDSleInitialBalance.EditValue)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se procesaron correctamente los items seleccionados"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Occurrio un error al procesar los items"
                End If
                AsyncLoader(False)
                CleanControls()
                INDSleInitialBalance.Properties.DataSource = Nothing
                INDSleInitialBalance.Properties.DataSource = model.GetInitialBalanceByStatus()
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    Private Sub INDBbiUndo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiUndo.ItemClick
        CleanControls()
    End Sub

    Private Sub INDGvBills_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvBills.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            PopupMenuActions.ShowPopup(INDGvBills.GridControl.PointToScreen(e.Point))
        End If
    End Sub

    Private Sub INDbarButtonBudget_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonBudget.ItemClick
        Using Popup As New PopupBudget
            Popup.StartPosition = FormStartPosition.CenterParent
            Dim transparent = New FrmTransparent(Popup, False)
            listItemsProcess = New List(Of Tuple(Of Integer, Integer))
            If transparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                For Each item In INDGvBills.GetSelectedRows()
                    Dim itemList = INDGvBills.GetRow(item)
                    listItemsProcess.Add(New Tuple(Of Integer, Integer)(itemList.Id, Popup.BudgetId))
                Next
            End If
        End Using
    End Sub
#End Region

#Region "BARBUTTONS"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

#End Region

End Class