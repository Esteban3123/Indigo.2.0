#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Domain.Base.Entities

#End Region

Public Class FrmPopupSupplieMedicine
#Region "Variables"
    Dim _ATC As ATC
    Private _ListSupplieMedicine As List(Of RelatedSupplieMedicine)
    Private _ListSupplieMedicineDelete As List(Of RelatedSupplieMedicine)
#End Region

#Region "Properties"

    ''' <summary>
    ''' Muestra los mensajes
    ''' </summary>
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
    ''' Obtiene o establece el tipo de elemento
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemId As Byte?
        Get
            Return INDGleItem.EditValue
        End Get
        Set(value As Byte?)
            INDGleItem.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del medicamento
    ''' </summary>
    ''' <returns></returns>
    Public Property MedicineId As Integer?
        Get
            Return INDSleMedicine.EditValue
        End Get
        Set(value As Integer?)
            INDSleMedicine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del medicamento
    ''' </summary>
    ''' <returns></returns>
    Public Property SupplieId As Integer?
        Get
            Return INDSleSupplie.EditValue
        End Get
        Set(value As Integer?)
            INDSleSupplie.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ListSupplieMedicine() As List(Of RelatedSupplieMedicine)
        Get
            Return _ListSupplieMedicine
        End Get
        Set(ByVal value As List(Of RelatedSupplieMedicine))
            _ListSupplieMedicine = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ListSupplieMedicineDelete() As List(Of RelatedSupplieMedicine)
        Get
            Return _ListSupplieMedicineDelete
        End Get
        Set(ByVal value As List(Of RelatedSupplieMedicine))
            _ListSupplieMedicineDelete = value
        End Set
    End Property


#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal _objATC As ATC)
        InitializeComponent()
        _ATC = _objATC
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Aqui inicializamos los controles con los valores del producto
    ''' </summary>
    Private Sub FrmPopupSupplieMedicine_Load(sender As Object, e As EventArgs) Handles Me.Load
        IndigoGridControl1.RefreshGrid(INDGcDetail)
        Dim _listAction As New List(Of eAcciones)()
        _listAction.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvDetail, _listAction)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        Dim ListYesNo As New List(Of Tuple(Of Byte, String))()
        ListYesNo.Add(New Tuple(Of Byte, String)(1, "Insumo"))
        ListYesNo.Add(New Tuple(Of Byte, String)(2, "Medicamento"))
        INDGleItem.Properties.DataSource = ListYesNo
        ItemId = 2

        ListSupplieMedicine = New List(Of RelatedSupplieMedicine)
        ListSupplieMedicineDelete = New List(Of RelatedSupplieMedicine)
        If _ATC IsNot Nothing AndAlso _ATC.RelatedSupplieMedicine IsNot Nothing Then
            For Each sm In _ATC.RelatedSupplieMedicine.Where(Function(d) Not d.ChangeTracker.State = ObjectState.Deleted)
                ListSupplieMedicine.Add(sm)
            Next
        End If

        ActualizarDataSource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMedicine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMedicine.QueryPopUp
        If INDSleMedicine.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDSleMedicine.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListATC)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplie_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplie.QueryPopUp
        If INDSleSupplie.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDSleSupplie.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListInventorySupplie)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control tipo de elemento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleItem_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleItem.EditValueChanged
        If (Not String.IsNullOrEmpty(ItemId)) AndAlso ItemId = 1 Then 'Insumo
            INDLciMedicine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            MedicineId = Nothing
        Else
            INDLciMedicine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            SupplieId = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSmbAddItem_Click(sender As Object, e As EventArgs) Handles INDSmbAddItem.Click
        Dim _mensaje As New StringBuilder
        If ItemId Is Nothing Then
            _mensaje.Append("Seleccione Elemento")
        ElseIf ItemId = 2 And MedicineId Is Nothing Then
            _mensaje.Append("Seleccione Medicamento")
        ElseIf ItemId = 1 And SupplieId Is Nothing Then
            _mensaje.Append("Seleccione Insumo")
        ElseIf ItemId = 2 AndAlso MedicineId = _ATC.Id Then
            _mensaje.Append("No se puede agregar el medicamento, el medicamento debe ser distinto al medicamento que se esta configurando.")
        ElseIf ListSupplieMedicine IsNot Nothing _
            AndAlso ListSupplieMedicine.Count > 0 _
            AndAlso ListSupplieMedicine.Any(Function(d) d.ItemType = ItemId And d.SourceId = IIf(ItemId = 1, SupplieId, MedicineId)) Then
            _mensaje.Append("Ya se agrego el elemento")
        End If
        If _mensaje.Length = 0 Then

            Dim d As New RelatedSupplieMedicine
            Dim _presenter As New PInventoryProduct
            With d
                .Agregado = True
                .ATCId = _ATC.Id
                .ItemType = ItemId
                .NameItemType = IIf(ItemId = 1, "Insumo", "Medicamento")
                If ItemId = 1 Then
                    .SourceId = SupplieId
                    Dim _supplie As InventorySupplieXpo = Await _presenter.GetSupplieById(SupplieId)
                    If _supplie IsNot Nothing Then
                        .SourceCode = _supplie.Code
                        .SourceName = _supplie.SupplieName
                    End If
                ElseIf ItemId = 2 Then
                    .SourceId = MedicineId
                    Dim _ATCXpo As ATCXpo = Await _presenter.GetMedicamentById(MedicineId)
                    If _ATCXpo IsNot Nothing Then
                        .SourceCode = _ATCXpo.Code
                        .SourceName = _ATCXpo.Name
                    End If
                End If

            End With
            _presenter = Nothing
            ListSupplieMedicine.Add(d)
            ActualizarDataSource()

        Else
            Mensaje(EeventViewerImages.Advertencia) = _mensaje.ToString
        End If
    End Sub

    ''' <summary>
    ''' Aqui se elimina el elemento seleccionada
    ''' </summary>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim _SupplieMedicine As RelatedSupplieMedicine = CType(INDGvDetail.GetFocusedRow(), RelatedSupplieMedicine)
        If _SupplieMedicine IsNot Nothing Then
            Dim _sm As RelatedSupplieMedicine = Nothing
            If _SupplieMedicine.Id > 0 Then
                _sm = ListSupplieMedicineDelete.Where(Function(d) d.Id = _SupplieMedicine.Id).FirstOrDefault
            ElseIf _SupplieMedicine.Id = 0 Then
                _sm = ListSupplieMedicineDelete.Where(Function(d) d.ItemType = _SupplieMedicine.ItemType AndAlso d.SourceId = _SupplieMedicine.SourceId).FirstOrDefault
            End If
            If _sm Is Nothing Then
                ListSupplieMedicineDelete.Add(_SupplieMedicine)
            End If
            ListSupplieMedicine.Remove(_SupplieMedicine)
            ActualizarDataSource()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAccept_Click(sender As Object, e As EventArgs) Handles INDSmbAccept.Click
        If ValidateFields() Then
            If ListSupplieMedicineDelete.Count > 0 Then
                Dim _rsm As RelatedSupplieMedicine
                For Each o In ListSupplieMedicineDelete.Where(Function(d) d.Id > 0)
                    _rsm = _ATC.RelatedSupplieMedicine.Where(Function(sm) sm.Id = o.Id).FirstOrDefault
                    If _rsm IsNot Nothing Then
                        _rsm.MarkAsDeleted()
                        _ATC.RelatedSupplieMedicine.Add(_rsm)
                    End If
                Next
                For Each o In ListSupplieMedicineDelete.Where(Function(d) d.Id = 0)
                    _ATC.RelatedSupplieMedicine.Remove(o)
                Next
            End If
            If ListSupplieMedicine.Any(Function(d) d.Agregado) Then
                For Each sm In ListSupplieMedicine.Where(Function(d) d.Agregado)
                    _ATC.RelatedSupplieMedicine.Add(sm)
                Next
            End If


            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida que se haya seleccionado un grupo de facturación si se pretende agregar patología
    ''' </summary>
    ''' <returns>Valor que indica si pasa la validación</returns>
    Private Function ValidateFields() As Boolean
        If ListSupplieMedicine.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asociar como mínimo un insumo/medicamento"
            Me.INDGleItem.Focus()
            Return False
        End If

        Return True
    End Function

#End Region

#Region "procesos"
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub ActualizarDataSource()
        INDGcDetail.DataSource = ListSupplieMedicine
        INDGcDetail.RefreshDataSource()
    End Sub


#End Region

End Class