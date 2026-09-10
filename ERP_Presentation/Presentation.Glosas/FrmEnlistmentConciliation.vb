'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : rafael Patiño
' Created          : 17-09-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"

Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraEditors

#End Region



''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
Public Class FrmEnlistmentConciliation
    Implements IenlistmentConciliation


    Dim Model As Menlistmentconciliation
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PenlistMentconciliation
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigoAux As SessionValues

    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim CustomerTmp As Domain.Entities.Customer
    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    Private Sub FrmEnlistmentConciliation_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New Menlistmentconciliation(Me.Tag)
        Me.indigoAux = SessionValues.Instance
        '******************************'
        Me.ActionReport = AddressOf Me.BarraBotones.PrintReport
        Presenter = New PenlistMentconciliation(Me)
        Me.ActionsOnControls = False
        'Me.IndigoGridControl1.SetHoldSize(Me.INDgcInvocieList, True)
        'Me.IndigoGridControl1.SetHotTrack(Me.INDgcInvocieList, True)
        Me.IndigoGridControl1.RefreshGrid(Me.INDgcInvocieList)
        contadorCantidadFacturas = 0
        Deshacer()
        SearchMode = False
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AsyncLoader(True)
        Dim listtmp As New List(Of GlosaPortfolioGlosada)
        listtmp = Model.ListGlosaPortfolioExportExcel(Me.INDbteEntity.Text, Me.INDdteInicial.EditValue, Me.INDdteEnd.EditValue)
        If listtmp IsNot Nothing AndAlso listtmp.Count > 0 Then
            Me.INDgcInvocieList.DataSource = listtmp
            Me.INDgcInvocieList.RefreshDataSource()
        End If
        contadorCantidadFacturas = 0
        AsyncLoader(False)
    End Sub


    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If INDbteEntity.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiNit.Text)
            Return False
        End If
        If INDdteInicial.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiDateIni.Text)
            Return False
        End If
        If INDdteEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiDateEnd.Text)
            Return False
        End If
        Return True
    End Function


    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException
    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValueWithList, AddressOf ReturnValueCustomers
        'lista de string de solicitados
        Dim listSolicitado As List(Of String) = New List(Of String)
        listSolicitado.Add("Nit")
        listSolicitado.Add("Name")
        listSolicitado.Add("Id")
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
            .ListadoSolicitud = listSolicitado
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
            .FormParent = Me
            .ShowSearch(False)
        End With
        SearchMode = True
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IenlistmentConciliation.ActionsOnControls
        Set(value As Boolean)
            INDbteEntity.Enabled = Not value
            INDdteInicial.Enabled = value
            INDdteEnd.Enabled = value
            INDbteEntity.Focus()
        End Set
    End Property


#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        ' Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Guardar()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub


#End Region

    Private Sub INDbteEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteEntity.ButtonClick
        OpenSearch()
    End Sub


    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        contadorCantidadFacturas = 0
        ActionsOnControls = False
        INDbteEntity.Text = String.Empty
        INDdteInicial.EditValue = Nothing
        INDdteEnd.EditValue = Nothing
        Me.INDlblEntity.Text = String.Empty
        Me.INDgcInvocieList.DataSource = Nothing
        Me.INDgcInvocieList.RefreshDataSource()
        Me.INDgCInvoiceExport.DataSource = Nothing
        Me.INDgCInvoiceExport.RefreshDataSource()
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValueCustomers(ByVal ReturnValue As String, ByVal ReturnObject As Object, ByVal ReturnList As List(Of String))
        'variable de la lista de string devueltos
        Dim listDevuelto As List(Of String)
        listDevuelto = ReturnList

        Dim ctomerTmp = CType(ReturnObject, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Dim ctomer = CType(ctomerTmp.OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.GlosasCustomerXpo)
        Me.CustomerTmp = New Domain.Entities.Customer With {.Id = ctomer.Id, .Nit = ctomer.Nit.ToString().Trim(), .Name = ctomer.Name.ToString().Trim(), .State = ctomer.State}
        If (FormSearchObjects.ListadoDevolucion.Count > 0) Then
            INDbteEntity.Text = FormSearchObjects.ListadoDevolucion.Item(0)
            INDlblEntity.Text = FormSearchObjects.ListadoDevolucion.Item(1)
            Me.INDgcInvocieList.DataSource = Nothing
            Await ConsultarEmpresa()
        End If
    End Sub

    Private Async Sub INDbteEntity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteEntity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                Exit Sub
            End If
            Await ConsultarEmpresa()
        End If
    End Sub

    Private Sub LoadControls()
        ActionsOnControls = True
    End Sub


    ''' <summary>
    ''' metodo asincrono para consultar la empresa.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ConsultarEmpresa() As Task
        If Not String.IsNullOrEmpty(INDbteEntity.Text.ToString) Then
            'AsyncLoader(True)clickdeshacegr
            CustomerTmp = Await Model.GetCustomerByNit(INDbteEntity.Text)
            'AsyncLoader(False)
            If CustomerTmp IsNot Nothing Then
                If CustomerTmp.Nit = 0 Then
                    CustomerTmp = Nothing
                    INDbteEntity.Text = String.Empty
                    INDbteEntity.Focus()
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesNoSeEncontroDatoERP, RecepcionObjeciones)
                Else
                    INDlblEntity.Text = CustomerTmp.Name
                    INDbteEntity.Text = CustomerTmp.Nit
                    LoadControls()
                    INDdteInicial.Focus()
                End If
            End If
        End If
    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
        Presenter = Nothing
        CustomerTmp = Nothing
        SearchMode = Nothing
    End Sub



    Private Sub INDbtnExportExcel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDgvInvoiceExport
        If _gridView IsNot Nothing AndAlso _gridView.VisibleColumns.Count > 0 Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDgCInvoiceExport.DataSource = Nothing
        Me.INDgCInvoiceExport.RefreshDataSource()
    End Sub


    Dim contadorCantidadFacturas As Integer = 0
    Private Sub INDgcInvocieList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcInvocieList.MouseDoubleClick
        Dim objHit = Me.INDgvInvoiceList.CalcHitInfo(e.Location)
        If objHit.InColumn AndAlso objHit.Column IsNot Nothing AndAlso objHit.Column.Name.Equals("clSelection") Then
            If Me.INDgcInvocieList.DataSource IsNot Nothing Then
                If DirectCast(Me.INDgcInvocieList.DataSource, List(Of GlosaPortfolioGlosada)).Where(Function(p) p.Selection).ToList().Count = DirectCast(Me.INDgcInvocieList.DataSource, List(Of GlosaPortfolioGlosada)).Count Then
                    For Each p As GlosaPortfolioGlosada In DirectCast(Me.INDgcInvocieList.DataSource, List(Of GlosaPortfolioGlosada))
                        p.Selection = False
                    Next
                    contadorCantidadFacturas = 0
                Else
                    For Each p As GlosaPortfolioGlosada In DirectCast(Me.INDgcInvocieList.DataSource, List(Of GlosaPortfolioGlosada))
                        contadorCantidadFacturas += 1
                        p.Selection = True
                        If contadorCantidadFacturas >= 1000 Then
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmEnlistonciliation_NoPuedeCargarMasRegistro", "Glosas")
                            Exit For
                        End If
                    Next
                End If
            End If
            Me.INDgcInvocieList.RefreshDataSource()
            INDgcInvocieList.Invalidate()
        End If
    End Sub

    Private Sub INDdteEnd_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDdteEnd.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Buscar()
        End If
    End Sub

    Private Sub RepositoryItemCheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit1.CheckedChanged
        Dim obj As CheckEdit = CType(sender, CheckEdit)
        If obj.EditValue = True Then
            contadorCantidadFacturas = contadorCantidadFacturas + 1
        Else
            contadorCantidadFacturas = contadorCantidadFacturas - 1
        End If
        If contadorCantidadFacturas > 1000 Then
            obj.Checked = False
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmEnlistonciliation_NoPuedeCargarMasRegistro", "Glosas")
            Exit Sub
        End If
    End Sub

 

    Private Sub INDExportOnlyMovement_Click(sender As Object, e As EventArgs) Handles INDExportOnlyMovement.Click
        Excel(True)
    End Sub

    Private Sub INDExportAll_Click(sender As Object, e As EventArgs) Handles INDExportAll.Click
        Excel(False)
    End Sub

    Private Sub Excel(ByVal OnlyMovements As Boolean)
        If Me.INDgcInvocieList.DataSource IsNot Nothing AndAlso Me.INDgvInvoiceList.DataSource.count > 0 Then
            Dim ListstrInvoice As New List(Of String)
            For Each itemInvoice As Domain.Entities.GlosaPortfolioGlosada In Me.INDgcInvocieList.DataSource
                If itemInvoice.Selection = True Then
                    ListstrInvoice.Add(itemInvoice.Id)
                End If
            Next
            If ListstrInvoice.Count > 0 Then
                Me.AsyncLoader(True)
                Dim a As Object = Nothing
                If OnlyMovements = True Then
                    a = Model.ListXpoInvoicesByNitExportExcel(ListstrInvoice, True)
                Else
                    a = Model.ListXpoInvoicesByNitExportExcel(ListstrInvoice, False)
                End If
                Me.INDgCInvoiceExport.DataSource = a
                Me.INDgCInvoiceExport.RefreshDataSource()
                Me.AsyncLoader(False)
                If Me.INDgCInvoiceExport.DataSource IsNot Nothing Then
                    generateExcel()
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmEnlistonciliation_NoDatosSeleccionado", "Glosas")
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmEnlistonciliation_NoDatosRejilla", "Glosas")
        End If
    End Sub

End Class