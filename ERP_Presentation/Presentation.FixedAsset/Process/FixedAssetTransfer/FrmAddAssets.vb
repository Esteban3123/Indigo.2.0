'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/05/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Payments.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class FrmAddAssets

#Region "Globals"

    Dim Presenter As PFixedAssetTransfer

    Dim ListFixedAssetPhysicalAssetXpo As List(Of FixedAssetPhysicalAssetXpo)

#End Region

#Region "Event"

    Public Event AddAssetsEventArgs(sender As Object, e As AddAssets)

#End Region

#Region "Properties"

    Private _transferType As Integer?
    Public Property TransferType As Integer?
        Get
            Return _transferType
        End Get
        Set(value As Integer?)
            _transferType = value
        End Set
    End Property

    Private _sourceLocationId As Integer?
    Public Property SourceLocationId As Integer?
        Get
            Return _sourceLocationId
        End Get
        Set(value As Integer?)
            _sourceLocationId = value
        End Set
    End Property

    Private _sourceResponsibleId As Integer?
    Public Property SourceResponsibleId As Integer?
        Get
            Return _sourceResponsibleId
        End Get
        Set(value As Integer?)
            _sourceResponsibleId = value
        End Set
    End Property

    Private _listCompare As List(Of FixedAssetTransferDetail)
    Public Property ListCompare As List(Of FixedAssetTransferDetail)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetTransferDetail))
            _listCompare = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#End Region

#Region "Methods"

    Private Sub LoadInfo()
        ListFixedAssetPhysicalAssetXpo = Presenter.ListFixedAssetPhysicalAssetXpo(TransferType, SourceLocationId, SourceResponsibleId)
        INDgcAssets.DataSource = Nothing
        INDgcAssets.DataSource = ListFixedAssetPhysicalAssetXpo
    End Sub

    Private Sub AddAssets()
        'Se valida que al menos haya un item seleccionado
        If ListFixedAssetPhysicalAssetXpo Is Nothing OrElse ListFixedAssetPhysicalAssetXpo.Count = 0 OrElse ListFixedAssetPhysicalAssetXpo.Where(Function(item) item.SelectOption = True).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item"
            Exit Sub
        End If

        'Se valida que no se haya ingresado ya el activo en el form principal
        If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
            Dim ListErrors As New StringBuilder
            For Each item In (From l In ListFixedAssetPhysicalAssetXpo Where l.SelectOption = True Select l).ToList
                If ListCompare.Where(Function(itemCompare) itemCompare.PhysicalAssetId = item.Id).Count > 0 Then
                    ListErrors.AppendLine("El activo con el articulo " + item.ItemId.CodeDescription + " ya existe en la lista.")
                End If
            Next
            If ListErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
                Exit Sub
            End If
        End If

        'Se crea el listado que se va a devolver
        Dim ListFixedAssetTransferDetail As New List(Of FixedAssetTransferDetail)
        For Each item In (From l In ListFixedAssetPhysicalAssetXpo Where l.SelectOption = True Select l).ToList
            Dim FixedAssetTransferDetail As New FixedAssetTransferDetail
            With FixedAssetTransferDetail
                .PhysicalAssetId = item.Id
                .ItemCodeName = item.ItemId.CodeDescription
                .Plate = item.Plate
                .Serie = item.Serie
            End With
            ListFixedAssetTransferDetail.Add(FixedAssetTransferDetail)
        Next

        Dim args As New AddAssets
        args.ListFixedAssetTransferDetail = ListFixedAssetTransferDetail
        RaiseEvent AddAssetsEventArgs(Nothing, args)
        Me.Close()
    End Sub



#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListFixedAssetPhysicalAssetXpo = Nothing
    End Sub

    Private Sub FrmAddAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDgcAssets)
        Presenter = New PFixedAssetTransfer()
        LoadInfo()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmAddAssets_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcAssets_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcAssets.MouseDoubleClick
        If ListFixedAssetPhysicalAssetXpo IsNot Nothing AndAlso ListFixedAssetPhysicalAssetXpo.Count > 0 Then
            Dim hitPoint = Me.INDviewAssets.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                    Dim listFilterXpCollection = INDviewAssets.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                    End If
                    Me.INDgcAssets.RefreshDataSource()
                    Me.INDgcAssets.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim itemXpo As FixedAssetPhysicalAssetXpo = INDviewAssets.GetFocusedRow()
            If itemXpo IsNot Nothing Then
                itemXpo.SelectOption = e.NewValue
                INDgcAssets.RefreshDataSource()
                If ListFixedAssetPhysicalAssetXpo.Where(Function(item) item.SelectOption = True).Count = ListFixedAssetPhysicalAssetXpo.Count Then
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                Else
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                End If
            End If
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddAssets_Click(sender As Object, e As EventArgs) Handles INDbtnAddAssets.Click
        AddAssets()
    End Sub

#End Region

#End Region

End Class