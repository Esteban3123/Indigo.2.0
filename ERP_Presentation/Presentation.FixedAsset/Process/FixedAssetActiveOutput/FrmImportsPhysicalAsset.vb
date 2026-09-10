'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2016
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

Public Class FrmImportsPhysicalAsset
    Implements IImportsPhysicalAsset

#Region "Properties"

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

    Private _listCompare As List(Of FixedAssetActiveOutputDetail)
    Public Property ListCompare As List(Of FixedAssetActiveOutputDetail)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetActiveOutputDetail))
            _listCompare = value
        End Set
    End Property

    Public Property LowType As Integer? Implements IImportsPhysicalAsset.LowType
        Get
            Return INDsleLowType.EditValue
        End Get
        Set(value As Integer?)
            INDsleLowType.EditValue = value
        End Set
    End Property

    Public Property OutputType As Integer? Implements IImportsPhysicalAsset.OutputType
        Get
            Return INDsleOutputType.EditValue
        End Get
        Set(value As Integer?)
            INDsleOutputType.EditValue = value
        End Set
    End Property

    Public Property SalesValue As Decimal Implements IImportsPhysicalAsset.SalesValue
        Get
            Return INDseSalesValue.EditValue
        End Get
        Set(value As Decimal)
            INDseSalesValue.EditValue = value
        End Set
    End Property

    Public Property ThirdPartyId As Integer? Implements IImportsPhysicalAsset.ThirdPartyId
        Get
            Return INDsleThirdPartyId.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdPartyId.EditValue = value
        End Set
    End Property

    Public Property ThirdPartyXpo As XPInstantFeedbackSource Implements IImportsPhysicalAsset.ThirdPartyXpo
        Get
            Return INDsleThirdPartyId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdPartyId.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Event"

    Public Event AddPhysicalAssetsEventArgs(sender As Object, e As AddPhysicalAssets)

#End Region

#Region "Globals"

    Dim ListFixedAssetPhysicalAssetXpo As List(Of FixedAssetPhysicalAssetXpo)

    Dim Presenter As PFixedAssetActiveOutput

    Dim ListOutputType As New List(Of Tuple(Of Integer, String))

    Dim ListLowType As New List(Of Tuple(Of Integer, String))

    Dim FlagChangedSearch As Boolean = True

#End Region

#Region "ICrud"

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

#Region "Methods"

    Private Sub LoadInfo()
        ListFixedAssetPhysicalAssetXpo = Presenter.ListFixedAssetPhysicalAssetXpo()
        INDgcPhysicalAsset.DataSource = Nothing
        INDgcPhysicalAsset.DataSource = ListFixedAssetPhysicalAssetXpo
    End Sub

    Private Sub AddPhysicalAsset()
        If ValidateControls() = False Then
            Exit Sub
        End If
        'Se valida que al menos haya un item seleccionado
        If ListFixedAssetPhysicalAssetXpo Is Nothing OrElse ListFixedAssetPhysicalAssetXpo.Count = 0 OrElse ListFixedAssetPhysicalAssetXpo.Where(Function(item) item.SelectOption = True).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item"
            Exit Sub
        End If
        If INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDseSalesValue.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El " + INDlyItemSalesValue.Text + " no puede ser cero"
                Exit Sub
            End If
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
        Dim ListFixedAssetActiveOutputDetail = GenerateList()

        Dim args As New AddPhysicalAssets
        args.ListFixedAssetActiveOutputDetail = ListFixedAssetActiveOutputDetail
        RaiseEvent AddPhysicalAssetsEventArgs(Nothing, args)
        Me.Close()
    End Sub

    Private Function GenerateList() As List(Of FixedAssetActiveOutputDetail)
        Dim ListFixedAssetActiveOutputDetail As New List(Of FixedAssetActiveOutputDetail)
        For Each item In (From l In ListFixedAssetPhysicalAssetXpo Where l.SelectOption = True Select l).ToList
            Dim FixedAssetActiveOutputDetail As New FixedAssetActiveOutputDetail
            With FixedAssetActiveOutputDetail
                .HistoricalValue = item.HistoricalValue
                .ActiveType = 1 'Activo
                If INDlyItemPhysicalAsset.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PhysicalAssetId = item.Id
                    .PhysicalAssetDescription = item.Plate + " - " + item.ItemId.CodeDescription
                Else
                    .PhysicalAssetId = Nothing
                    .PhysicalAssetDescription = String.Empty
                End If

                .MainAccountId = item.MainAccountId.Id
                .MainAccountNumberName = item.MainAccountId.NumberName
                .OutputType = OutputType
                If INDlyItemLowType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .LowType = LowType
                Else
                    .LowType = 0
                End If
                If INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .SalesValue = SalesValue
                Else
                    .SalesValue = 0
                End If
                If INDlyItemThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ThirdPartyId = ThirdPartyId
                    .ThirdPartyNitName = INDsleThirdPartyId.Text
                Else
                    .ThirdPartyId = Nothing
                    .ThirdPartyNitName = String.Empty
                End If
            End With
            ListFixedAssetActiveOutputDetail.Add(FixedAssetActiveOutputDetail)
        Next
        Return ListFixedAssetActiveOutputDetail
    End Function

    Private Sub InitializeTuple()
        'Tipo de salida
        ListOutputType = New List(Of Tuple(Of Integer, String))
        ListOutputType.Add(New Tuple(Of Integer, String)(1, "Baja"))
        ListOutputType.Add(New Tuple(Of Integer, String)(2, "Venta"))
        INDsleOutputType.Properties.DataSource = ListOutputType.ToList
        'Tipo de baja
        ListLowType = New List(Of Tuple(Of Integer, String))
        ListLowType.Add(New Tuple(Of Integer, String)(4, "Bienes Inservibles"))
        ListLowType.Add(New Tuple(Of Integer, String)(1, "Perdida"))
        ListLowType.Add(New Tuple(Of Integer, String)(2, "Siniestro"))
        ListLowType.Add(New Tuple(Of Integer, String)(3, "Perdida Reposicion"))
        INDsleLowType.Properties.DataSource = ListLowType.ToList
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListFixedAssetPhysicalAssetXpo = Nothing
        Presenter = Nothing
        ListOutputType = Nothing
        ListLowType = Nothing
        FlagChangedSearch = Nothing
    End Sub

    Private Sub FrmImportsPhysicalAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyImports, True)
        InitializeTuple()
        IndigoGridControl1.RefreshGrid(INDgcPhysicalAsset)
        Presenter = New PFixedAssetActiveOutput()
        LoadInfo()
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcPhysicalAsset_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcPhysicalAsset.MouseDoubleClick
        If ListFixedAssetPhysicalAssetXpo IsNot Nothing AndAlso ListFixedAssetPhysicalAssetXpo.Count > 0 Then
            Dim hitPoint = Me.INDviewPhysicalAsset.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                    Dim listFilterXpCollection = INDviewPhysicalAsset.DataController.GetAllFilteredAndSortedRows()
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
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = ListFixedAssetPhysicalAssetXpo.Count Then
                            Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDgcPhysicalAsset.RefreshDataSource()
                    Me.INDgcPhysicalAsset.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmImportsPhysicalAsset_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckItem_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckItem.EditValueChanging
        If e IsNot Nothing Then
            Dim itemXpo As FixedAssetPhysicalAssetXpo = INDviewPhysicalAsset.GetFocusedRow()
            If itemXpo IsNot Nothing Then
                itemXpo.SelectOption = e.NewValue
                INDgcPhysicalAsset.RefreshDataSource()
                If ListFixedAssetPhysicalAssetXpo.Where(Function(item) item.SelectOption = True).Count = ListFixedAssetPhysicalAssetXpo.Count Then
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                Else
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleOutputType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleOutputType.EditValueChanged
        If OutputType IsNot Nothing Then
            Select Case OutputType
                Case 1 'Baja
                    INDlyItemLowType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemLowType.AllowHide = False

                    INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemSalesValue.AllowHide = True

                    INDlyItemThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemThirdPartyId.AllowHide = True

                    LowType = Nothing
                Case 2 'Venta
                    INDlyItemLowType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemLowType.AllowHide = True

                    INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemSalesValue.AllowHide = False

                    INDlyItemThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemThirdPartyId.AllowHide = False

                    INDlyItemSalesValue.Text = "Valor de Venta"
            End Select
        End If
    End Sub

    Private Sub INDsleLowType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLowType.EditValueChanged
        If LowType IsNot Nothing Then
            If LowType = 3 Then 'Perdida Reposición
                INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSalesValue.Text = "Valor de Reposición"
            ElseIf FlagChangedSearch = True Then
                INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddPhysical_Click(sender As Object, e As EventArgs) Handles INDbtnAddPhysical.Click
        AddPhysicalAsset()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmImportsPhysicalAsset_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleOutputType.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleThirdPartyId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdPartyId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            ThirdPartyXpo = Presenter.ListThirdParty()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleThirdPartyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdPartyId.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = Presenter.ListThirdParty()
        End If
    End Sub

#End Region

#End Region

End Class