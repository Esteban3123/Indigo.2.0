'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/03/2017
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

Public Class FrmLeasingContractsFinalization
    Implements ILeasingContractsFinalization

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoYearMonth()
        ctrTmp.SetInfo(AddressOf getInfo)
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    ''' <summary>
    ''' Devuelve el mes y el año en el contros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of Integer, Integer)
        Return New Tuple(Of Integer, Integer)(Month, Year)
    End Function

#End Region

#Region "Properties"

    Private _year As Integer
    Private Property Year As Integer Implements ILeasingContractsFinalization.Year
        Get
            Return _year
        End Get
        Set(value As Integer)
            _year = value
        End Set
    End Property

    Private _month As Integer
    Private Property Month As Integer Implements ILeasingContractsFinalization.Month
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
        End Set
    End Property

    Private _settingsFixedAssetXpo As SettingFixedAssetXpo
    Public Property SettingsFixedAssetXpo As SettingFixedAssetXpo Implements ILeasingContractsFinalization.SettingsFixedAssetXpo
        Get
            Return _settingsFixedAssetXpo
        End Get
        Set(value As SettingFixedAssetXpo)
            _settingsFixedAssetXpo = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

#Region "Variables"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrInfoYearMonth

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PLeasingContractsFinalization

    ''' <summary>
    ''' Listado de leasing cuando el contrato ya ha terminado
    ''' </summary>
    Dim ListFixedAssetPhysicalAssetXpo As List(Of FixedAssetPhysicalAssetXpo)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Metodo que se dispara para consultar el listado de ingresos de activos que sean de tipo leasing y que el mes y el año de la fecha final de leasing sean igual al de parámetros
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        If Year = 0 AndAlso Month = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No existen parámetros de activos fijos para la unidad operativa escogida"
            Exit Sub
        End If
        GetListFixedAssetPhysicalAsset()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Se obtiene el listado de tipo leasing de ingreso de activos
    ''' </summary>
    Private Sub GetListFixedAssetPhysicalAsset()
        ListFixedAssetPhysicalAssetXpo = Nothing
        Task.Factory.StartNew(Sub()
                                  ListFixedAssetPhysicalAssetXpo = Presenter.GetListFixedAssetPhysicalAssetLeasingByMonthAndYear(Month, Year)
                                  If ListFixedAssetPhysicalAssetXpo IsNot Nothing AndAlso ListFixedAssetPhysicalAssetXpo.Count > 0 Then
                                      If INDgcData.InvokeRequired Then
                                          INDgcData.BeginInvoke(Sub()
                                                                    INDgcData.DataSource = ListFixedAssetPhysicalAssetXpo
                                                                End Sub)
                                      Else
                                          INDgcData.DataSource = ListFixedAssetPhysicalAssetXpo
                                      End If
                                  End If
                              End Sub)
    End Sub

    ''' <summary>
    ''' Metodo que modifica la visibilidad de la columna de acciones
    ''' </summary>
    Private Sub ModifiedColumn()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewData.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que refresca la información
    ''' </summary>
    Private Sub RefreshInfo()
        Deshacer()
        Dim result As ActionResult = Presenter.GetSettingsFixedAssetXpo(Me.BarraBotones.OperatingUnitValue)
        If result.StateResult = False Then
            Mensaje(EeventViewerImages.Advertencia) = result.Message
        End If
        ctrTmp.RefreshInfo()
    End Sub

    ''' <summary>
    ''' Limpia los controles del form
    ''' </summary>
    Private Sub CleanControls()
        ListFixedAssetPhysicalAssetXpo = Nothing
        INDgcData.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que realiza check o uncheck de los items de la rejilla
    ''' </summary>
    Private Sub CheckOptions(optionCheck As Boolean)
        For i = 0 To INDviewData.SelectedRowsCount - 1
            If INDviewData.GetSelectedRows()(i) >= 0 Then
                Dim row As FixedAssetPhysicalAssetXpo = INDviewData.GetRow(INDviewData.GetSelectedRows()(i))
                row.SelectOption = optionCheck
            End If
        Next
        INDgcData.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Confirma la finalización de contratos leasing
    ''' </summary>
    ''' <returns></returns>
    Private Async Function Confirm() As Task
        'Se valida que al menos hayan elegido un item para confirmar
        If ListFixedAssetPhysicalAssetXpo Is Nothing OrElse (From x In ListFixedAssetPhysicalAssetXpo Where x.SelectOption = True Select x).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay registros seleccionados para poder confirmar"
            Exit Function
        End If

        'Se realiza la pregunta para confirmar
        If MessageIndigo.Show("Desea confirmar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Function
        End If

        Using model As New MFixedAssetPhysicalAsset(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.ConfirmLeasingContractsFinalization((From x In ListFixedAssetPhysicalAssetXpo Where x.SelectOption = True Select x.Id).ToList, BarraBotones.OperatingUnitValue, Year, Month, indigo.IndigoCompanyNit)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = Result.Message
                Me.Deshacer()
                GetListFixedAssetPhysicalAsset()
            Else
                If Result.StatusCode = eStatusResult.EXCEPTION Then
                    Mensaje(EeventViewerImages.MensajeError) = Result.Message
                ElseIf Result.StatusCode = eStatusResult.WARNING Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End If
        End Using
    End Function

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        ctrTmp = Nothing
        Presenter = Nothing
        ListFixedAssetPhysicalAssetXpo = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmLeasingContractsFinalization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PLeasingContractsFinalization(Me)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.CheckOptions)
        ListActions.Add(eAcciones.UnCheckOptions)
        IndigoGridView1.SetListAcction(INDviewData, ListActions)
        ModifiedColumn()

        IndigoGridControl1.RefreshGrid(INDgcData)
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.StatusRecordVisible = True
        RefreshInfo()
    End Sub

#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' Evento que se dispara al hacer doble click sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcData_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcData.MouseDoubleClick
        If ListFixedAssetPhysicalAssetXpo IsNot Nothing AndAlso ListFixedAssetPhysicalAssetXpo.Count > 0 Then
            Dim hitPoint = Me.INDviewData.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                    Dim listFilterXpCollection = INDviewData.DataController.GetAllFilteredAndSortedRows()
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
                    Me.INDgcData.RefreshDataSource()
                    Me.INDgcData.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim itemXpo As FixedAssetPhysicalAssetXpo = INDviewData.GetFocusedRow()
            If itemXpo IsNot Nothing Then
                itemXpo.SelectOption = e.NewValue
                INDgcData.RefreshDataSource()
                If ListFixedAssetPhysicalAssetXpo.Where(Function(item) item.SelectOption = True).Count = ListFixedAssetPhysicalAssetXpo.Count Then
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                Else
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                End If
            End If
        End If
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Evento que se dispara al realizar click derecho sobre los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "CheckOptions"
                CheckOptions(True)
            Case "UnCheckOptions"
                CheckOptions(False)
        End Select
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcData_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcData.DataSourceChanged
        If ListFixedAssetPhysicalAssetXpo Is Nothing OrElse ListFixedAssetPhysicalAssetXpo.Count = 0 Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Actualiza los permisos de la barra cuando carga.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await Confirm()
    End Sub

    ''' <summary>
    ''' Barra Botones: Buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            RefreshInfo()
        End If
    End Sub

#End Region

End Class