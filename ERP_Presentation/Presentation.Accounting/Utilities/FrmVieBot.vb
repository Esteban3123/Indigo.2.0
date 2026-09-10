'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraTreeList.Nodes
Imports Presentation.Base
Imports DevExpress.XtraTreeList.Columns
Imports Presentation.Common.MVP
Imports Presentation.Common
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraEditors.Design
Imports System.Windows.Forms

#End Region

Public Class FrmVieBot
    Implements IVieBot

#Region "Builders"

    ''' <summary>
    ''' Crea una lista de formularios que no deben ser homologados
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        'Se especifican los formularios que no serán homologables
        ListFormsWithoutHomologation = New List(Of String)
        'Facturacion
        ListFormsWithoutHomologation.Add("BasicBilling")
        'Activos Fijos
        ListFormsWithoutHomologation.Add("FixedAssetEntry")
        ListFormsWithoutHomologation.Add("FixedAssetEntryDevolution")
        ListFormsWithoutHomologation.Add("FixedAssetDepreciation")
        ListFormsWithoutHomologation.Add("FixedAssetTransaction")
        ListFormsWithoutHomologation.Add("LeasingContractsFinalization")
        ListFormsWithoutHomologation.Add("FixedAssetReclassification")
        ListFormsWithoutHomologation.Add("FixedAssetActiveOutput")
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de formularios
    ''' </summary>
    Dim ListForms As List(Of EntityForms)

    ''' <summary>
    ''' Listado de VieBot dependiendo de la cantidad de libros oficiales que hayan en la BD
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListVieBot As List(Of VieBot)

    ''' <summary>
    ''' Listado que guarda los VieBot
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListSaveVieBot As List(Of VieBot)

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PVieBot

    ''' <summary>
    ''' Formularios no Homologables
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFormsWithoutHomologation As List(Of String)


#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que carga el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDatasource()
        'Se inicializa el listado que se va a desplegar en el Popup de la rejilla
        InitializeListVieBot()
        'Listado de formularios
        ListForms = New List(Of EntityForms)
        'Tesoreria
        ListForms.Add(New EntityForms With {.ModuleName = "Tesorería", .FormName = "CashReceipts", .Description = "Recibos de Caja", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Tesorería", .FormName = "VoucherTransaction", .Description = "Comprobante de egreso", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Tesorería", .FormName = "TreasuryNote", .Description = "Notas de tesoreria", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Tesorería", .FormName = "Consignment", .Description = "Consignaciones", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Tesorería", .FormName = "CrossingAccount", .Description = "Cruce de cuentas", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Tesorería", .FormName = "ConstitutionCashSmaller", .Description = "Fondo de Caja Menor", .ListVieBot = Nothing})
        'Cuentas por pagar
        ListForms.Add(New EntityForms With {.ModuleName = "Pagos", .FormName = "DeferredCausation", .Description = "Amortizacion Mensual", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Pagos", .FormName = "AccountPayable", .Description = "Cuentas por Pagar", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Pagos", .FormName = "PaymentNotes", .Description = "Notas Debito/Credito", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Pagos", .FormName = "PaymentTransfer", .Description = "Cruce de Anticipos Vs CxP", .ListVieBot = Nothing})
        'Cuentas por cobrar
        ListForms.Add(New EntityForms With {.ModuleName = "Cartera", .FormName = "RadicateInvoiceC", .Description = "Radicacion de Cuentas", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Cartera", .FormName = "PortfolioNote", .Description = "Notas Debito/Credito", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Cartera", .FormName = "PortfolioTransfer", .Description = "Cruce de Anticipo Vs CxC", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Cartera", .FormName = "AccountReceivableDocument", .Description = "Documento Cuenta por Cobrar", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Cartera", .FormName = "PortfolioReclassification", .Description = "Reclasificación de Documentos de Cartera", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Cartera", .FormName = "PortfolioProvisionAndDeterioration", .Description = "Provisión/Deterioro Cartera", .ListVieBot = Nothing})
        'Facturación      
        ListForms.Add(New EntityForms With {.ModuleName = "Facturación", .FormName = "Invoice", .Description = "Factura", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Facturación", .FormName = "DocumentInvoiceProductSales", .Description = "Factura de Productos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Facturación", .FormName = "InvoiceEntityCapitated", .Description = "Factura Entidad Capitada", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Facturación", .FormName = "RevenueRecognition", .Description = "Reconocimiento de Ingresos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Facturación", .FormName = "BasicBilling", .Description = "Facturación Básica", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Facturación", .FormName = "InvoiceEntityCapitatedDistribution", .Description = "Distribución Ingresos Monto Fijo", .ListVieBot = Nothing})
        'Iventarios       
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "RemissionEntrance", .Description = "Remision de Entrada", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "ConsignmentInventoryRemission", .Description = "Remision de Inventario en Consignación", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "RemissionOutput", .Description = "Remision de Salida", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "InventoryAdjustment", .Description = "Ajuste de Inventario", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "PharmaceuticalDispensing", .Description = "Dispensacion Farmaceutica", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "PharmaceuticalDispensingDevolution", .Description = "Devolucion de Dispensacion", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "TransferOrder", .Description = "Orden de Traslado", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "TransferOrderDevolution", .Description = "Devolucion de Orden de Traslado", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "EntranceVoucher", .Description = "Comprobate de Entrada", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "EntranceVoucherDevolution", .Description = "Devolucion de Comprobate de Entrada", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "LoanMerchandise", .Description = "Prestamo de Mercancia", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "LoanMerchandiseDevolution", .Description = "Devolucion de Prestamo de Mercancia", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Inventario", .FormName = "ConsignmentCostList", .Description = "Lista de costos - Consignación", .ListVieBot = Nothing})
        'Nomina
        ListForms.Add(New EntityForms With {.ModuleName = "Nómina", .FormName = "PayrollLiquidation", .Description = "Liquidación de Nómina", .ListVieBot = Nothing})
        'Activos Fijos
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetTransfer", .Description = "Traslado de Activos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetActiveOutput", .Description = "Salida de Activos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetDepreciation", .Description = "Depreciación", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetEntry", .Description = "Ingreso de Activos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetEntryDevolution", .Description = "Devolución Ingreso de Activos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetTransaction", .Description = "Transacciones", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "LeasingContractsFinalization", .Description = "Finalización Contratos Leasing", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Activos Fijos", .FormName = "FixedAssetReclassification", .Description = "Reclasificación de Activos", .ListVieBot = Nothing})
        'Impuestos
        ListForms.Add(New EntityForms With {.ModuleName = "Impuestos", .FormName = "TaxesLiquidation", .Description = "Liquidación de Impuestos", .ListVieBot = Nothing})
        ListForms.Add(New EntityForms With {.ModuleName = "Impuestos", .FormName = "LowTaxLiquidation", .Description = "Liquidación de Impuestos Menores", .ListVieBot = Nothing})
        'Contabilidad
        ListForms.Add(New EntityForms With {.ModuleName = "Contabilidad", .FormName = "MassiveReplication", .Description = "Replicación Masiva", .ListVieBot = Nothing})
        'Glosas
        ListForms.Add(New EntityForms With {.ModuleName = "Glosas", .FormName = "GlosaObjectionsReceptionD", .Description = "Recepción de Objeciones", .ListVieBot = Nothing})


        INDgcForms.DataSource = Nothing
        INDgcForms.DataSource = ListForms
    End Sub

    ''' <summary>
    ''' Inicializa el listado de VieBot
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeListVieBot()
        'Inicializa un nuevo listado para agregar a la rejilla de los formulario
        ListVieBot = New List(Of VieBot)
        'Se consulta con xpo los libros oficiales de la BD
        Dim ListXpo = Presenter.ListLegalBook()
        If ListXpo Is Nothing OrElse ListXpo.Count = 0 Then 'Si no hay datos de libros oficiales
            Mensaje(EeventViewerImages.Advertencia) = "No existen libros oficiales en la BD"
            Exit Sub
        End If
        'Se recorre la cantidad de libros oficiales para poder crear los objetos de VieBot y
        'agregarlos al listado que va en la rejilla de los formularios
        For Each itemXpo As BookXpo In ListXpo
            Dim VieBot As New VieBot
            With VieBot
                .Form = String.Empty
                .LegalBookId = itemXpo.Id
                .CodeNameLegalBook = itemXpo.CodeName
                .Allow = False
            End With
            ListVieBot.Add(VieBot)
        Next
    End Sub

    ''' <summary>
    ''' Asigna los valores para enviar a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        ListSaveVieBot = New List(Of VieBot)
        For Each itemForm In ListForms
            If itemForm.ListVieBot IsNot Nothing AndAlso itemForm.ListVieBot.Count > 0 Then
                For Each itemVieBot In itemForm.ListVieBot
                    If itemVieBot.Allow = True Or itemVieBot.Id > 0 Then
                        ListSaveVieBot.Add(itemVieBot)
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Valida que el listado se hayan escogido al menos un item
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateList() As String
        Dim errors As New StringBuilder

        Dim count = (From itemForm In ListForms Where itemForm.ListVieBot IsNot Nothing AndAlso itemForm.ListVieBot.FindAll(Function(itemVieBot) itemVieBot.Allow = True Or itemVieBot.Id > 0).Count > 0 Select itemForm).Count
        If count = 0 Then
            errors.AppendLine("Debe permitir al menos un item en los libros oficiales.")
        End If
        Return errors.ToString
    End Function

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Slide de mensajes
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

    ''' <summary>
    ''' Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Metodo que guarda la configuración VieBot
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Dim errors As String = ValidateList()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Using model As New MVieBot(Me.Tag.ToString())
                Dim Result = Await model.SaveVieBot(ListSaveVieBot)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    InitializeDatasource()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListForms = Nothing
        ListVieBot = Nothing
        ListSaveVieBot = Nothing
        Presenter = Nothing
        ListFormsWithoutHomologation = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmVieBot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PVieBot(Me)
        IndigoGridControl1.RefreshGrid(INDgcForms)
        InitializeDatasource()
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el popup de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepPceLegalBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceLegalBook.QueryPopUp
        'Obtengo el item en el cual tengo el foco
        Dim Item As EntityForms = INDViewForms.GetFocusedRow()

        If Item IsNot Nothing Then
            'Vacio el datasource de la rejilla del popup
            INDgcLegalBook.DataSource = Nothing

            If ListVieBot IsNot Nothing AndAlso ListVieBot.Count > 0 Then 'Si hay registros de libro oficial en la BD
                If Not (Item.ListVieBot IsNot Nothing AndAlso Item.ListVieBot.Count > 0) Then 'Si se abre por primera vez el popup se agrega los registros de la BD al listado del item
                    'Se instancia el listado del item
                    Item.ListVieBot = New List(Of VieBot)

                    'Se recorre el listado de la BD y se agrega al listado del item
                    For Each itemBD As VieBot In ListVieBot
                        Dim VieBot As New VieBot
                        With VieBot
                            .Form = Item.FormName
                            .LegalBookId = itemBD.LegalBookId
                            .CodeNameLegalBook = itemBD.CodeNameLegalBook
                            .Allow = False
                            .HandlesHomologation = True
                            If ListFormsWithoutHomologation.Contains(Item.FormName) Then
                                .HandlesHomologation = False
                            End If
                        End With
                        Item.ListVieBot.Add(VieBot)
                    Next

                    'Se consulta los VieBot por formulario que esten en la BD para asignarles el id
                    If Item.ListVieBot IsNot Nothing AndAlso Item.ListVieBot.Count > 0 Then
                        'Se consulta los VieBot de la BD
                        Dim ListXpoVieBot = Presenter.ListVieBotByForm(Item.FormName)
                        If ListXpoVieBot IsNot Nothing AndAlso ListXpoVieBot.Count > 0 Then
                            'Se recorre el listado de vieBot para asignarle el id a los diferentes items
                            For Each itemXpo As VieBotXpo In ListXpoVieBot
                                'Buscamos el item que viene de la base de datos lo buscamos en el listado que esta en la rejilla de legalBook para poderle asignar los campos de Id y Allow
                                Dim itemInfo As VieBot = (From x In Item.ListVieBot Where x.Form = itemXpo.Form AndAlso x.LegalBookId = itemXpo.LegalBookId.Id Select x).FirstOrDefault
                                If itemInfo IsNot Nothing Then
                                    itemInfo.Id = itemXpo.Id
                                    itemInfo.Allow = itemXpo.Allow
                                End If
                            Next
                        End If
                    End If

                End If

                INDgcLegalBook.DataSource = Item.ListVieBot

            End If
        End If

    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla de libros oficiales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckAllow_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckAllow.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim item As VieBot = INDviewLegalBook.GetFocusedRow()
            If item IsNot Nothing Then
                item.Allow = e.NewValue
                INDgcLegalBook.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el hyperLink
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepHiperLink_Click(sender As Object, e As EventArgs) Handles INDrepHiperLink.Click
        'Se dispara con el evento click del hyperLink el simulador de expresiones de devexpress
        Dim ExpressionEditForm As ExpressionEditorForm
        ExpressionEditForm = New UnboundColumnExpressionEditorForm(Me, Nothing)
        ExpressionEditForm.Controls.Item(0).Text = String.Empty
        ExpressionEditForm.StartPosition = FormStartPosition.CenterParent
        If ExpressionEditForm.ShowDialog() = DialogResult.OK Then

        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

End Class