Imports Presentation.Base
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class FrmUpdateExpirationDate
    Implements IUpdateExpirationDate



#Region "Constant"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

#End Region

#Region "Variables"
    ''' <summary>
    ''' Variable que representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PUpdateExpirationDate
    ''' <summary>
    ''' Lista de lotes a actualizar fecha de vencimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListUpdateExpirateDate As List(Of BatchSerial)
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IUpdateExpirationDate.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property
    ''' <summary>
    ''' Carga los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceProduct As DevExpress.Xpo.XPInstantFeedbackSource Implements IUpdateExpirationDate.DatasourceProduct
        Get
            Return Me.INDsleProducts.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDsleProducts.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Id del producto seleccionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdProduct As Integer? Implements IUpdateExpirationDate.IdProduct
        Get
            Return Me.INDsleProducts.EditValue
        End Get
        Set(value As Integer?)
            Me.INDsleProducts.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IUpdateExpirationDate.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Asiganao obtiene el listado de lotes del producto seleccionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceBatchSerial As List(Of BatchSerial)
        Get
            Return Me.INDgcBachtSerial.DataSource
        End Get
        Set(value As List(Of BatchSerial))
            If value IsNot Nothing Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
            Me.INDgcBachtSerial.DataSource = value
            Me.INDgcBachtSerial.RefreshDataSource()
        End Set
    End Property
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        INDsleProducts.ShowPopup()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            AssigningValues()
            If ListUpdateExpirateDate.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoRegistrationListUpdateExpirateDate", NAME_MODULE)
                Exit Sub
            End If
            Using model As New MUpdateExpirationDate(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveBatchSerial(ListUpdateExpirateDate)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(True)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub


    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Limpiar Controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        IdProduct = Nothing
        INDsleProducts.EditValue = Nothing
        DataSourceBatchSerial = Nothing
        INDsleProducts.Focus()
    End Sub
    ''' <summary>
    ''' Asignar valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If DataSourceBatchSerial IsNot Nothing AndAlso DataSourceBatchSerial.Count > 0 Then
            ListUpdateExpirateDate = DataSourceBatchSerial.Where(Function(x) x.ExpirationDateNew IsNot Nothing).ToList()
            For Each item As BatchSerial In ListUpdateExpirateDate
                Dim _updateExpiratedate As New UpdateExpirationDate
                With _updateExpiratedate
                    .BatchSerialId = item.Id
                    .OldExpirationDate = item.ExpirationDate
                    .NewExpirationDate = item.ExpirationDateNew
                    .Observation = ""
                    .CreationDate = DateTime.Now
                    .CreationUser = indigo.AuditMessageWcf.CodeUser
                End With
                item.ExpirationDate = item.ExpirationDateNew
                item.ModificationDate = DateTime.Now
                item.ModificationUser = indigo.AuditMessageWcf.CodeUser
                item.UpdateExpirationDate.Add(_updateExpiratedate)
            Next
        End If
    End Sub

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Evento que se dispara la presionar click en la barra de botones en buscar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub


    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de actualizar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
    End Sub

    ''' <summary>
    ''' Barra botones: Click anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListUpdateExpirateDate = Nothing
    End Sub

    Private Sub FrmUpdateExpirationDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycUpdateExpirationDate, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PUpdateExpirationDate(Me)
        IndigoGridControl1.RefreshGrid(INDgcBachtSerial)
        Deshacer()
        IndigoGridView1.MoreInfoColunmns(INDgvBachtSerial)
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProducts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProducts.QueryPopUp
        If DatasourceProduct Is Nothing Then
            Presenter.InitializeProduct()
        End If
    End Sub

    ''' <summary>
    ''' evento que carga el historico de update 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDripccHistoryUpdateBatchSerial_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDripccHistoryUpdateBatchSerial.QueryPopUp
        Dim record As BatchSerial = DirectCast(INDgvBachtSerial.GetFocusedRow, BatchSerial)
        If record IsNot Nothing Then
            INDGcUpdateHistoryBatchSerial.DataSource = Nothing
            INDGcUpdateHistoryBatchSerial.DataSource = record.UpdateExpirationDate
            INDGcUpdateHistoryBatchSerial.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(304, Nothing, True)
            Presenter.InitializeProduct()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleProducts_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProducts.EditValueChanged
        If IdProduct IsNot Nothing Then
            Using _model As New MUpdateExpirationDate(Me.Tag)
                DataSourceBatchSerial = _model.ListBatchSerialByProductIdIncludeExpirationDate(IdProduct)
            End Using
        End If
    End Sub

    Private Sub INDdeUpdateExpitaeDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdeUpdateExpitaeDate.EditValueChanging
        Dim record As BatchSerial = DirectCast(INDgvBachtSerial.GetFocusedRow, BatchSerial)
        If record IsNot Nothing Then
            If e.NewValue <= record.ExpirationDate Then
                e.Cancel = True
                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ExpirateDateLargetsDateUpdate", NAME_MODULE)
            End If
        End If
    End Sub

#End Region

#Region "Activated"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDsleProducts.Enabled Then
            INDsleProducts.Focus()
        End If
    End Sub

#End Region

#End Region


End Class