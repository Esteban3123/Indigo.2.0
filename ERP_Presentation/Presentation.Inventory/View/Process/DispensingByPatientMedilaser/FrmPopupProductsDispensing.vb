'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2018
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Columns
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Domain.Base.Entities
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Billing.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.XtraSplashScreen
Imports System.ComponentModel

#End Region

Public Class FrmPopupProductsDispensing

#Region "Builder"

    Sub New(_editMode As Boolean, _patient As PatientXpo)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        EditMode = _editMode
        Patient = _patient
    End Sub
    Sub New(_editMode As Boolean, _patient As PatientXpo, _dateDispensing As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        EditMode = _editMode
        Patient = _patient
        dateDispensing = _dateDispensing
    End Sub
#End Region

#Region "Variables"

    ''' <summary>
    ''' Almacen del registro
    ''' </summary>
    Public WareHouseId As Integer

    ''' <summary>
    ''' listado del inventario fisico por el codigo atc
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventoryCrystalProduct As List(Of PhysicalInventory)

    ''' <summary>
    ''' Código del producto
    ''' </summary>
    Public ProductCode As String

    ''' <summary>
    ''' Tipo de producto
    ''' </summary>
    Public ProductType As Integer

    ''' <summary>
    ''' Código de paciente
    ''' </summary>
    Public PatientCode As String

    ''' <summary>
    ''' Permite saber si esta en modo de edición
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Entidad del paciente que llega desde el formulario padre
    ''' </summary>
    Public Patient As PatientXpo

    ''' <summary>
    ''' Datasource search diferido
    ''' </summary>
    Private ListIsDeferred As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Entidad del detalle de la rejilla
    ''' </summary>
    Public ViewDashboardPharmacyDetail As Domain.Crystal.Entities.ViewDashboardPharmacyDetail

    ''' <summary>
    ''' Permite saber si cambia o no las asignaciones
    ''' </summary>
    Public IsLoad As Boolean = False

    ''' <summary>
    ''' Edad del paciente en  años
    ''' </summary>
    Private PatientAge As Integer

    ''' <summary>
    ''' Edad del paciente en dias
    ''' </summary>
    ''' <returns></returns>
    Private Property PatientAgeInDays As Integer

    ''' <summary>
    ''' Edad del paciente en meses
    ''' </summary>
    ''' <returns></returns>
    Private Property PatientAgeInMonths As Integer

    ''' <summary>
    ''' Sexo del paciente
    ''' </summary>
    Private PatientSex As Integer

    ''' <summary>
    ''' Producto seleccionado en el search
    ''' </summary>
    Private ProductXpo As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventory As List(Of PhysicalInventory)

#End Region

#Region "Properties"
    Public dateDispensing As String
    Public Property RequestQuantity As Integer
        Get
            Return INDseRequestQuantity.EditValue
        End Get
        Set(value As Integer)
            INDseRequestQuantity.EditValue = value
        End Set
    End Property

    Public Property DeliveryQuantity As Integer
        Get
            Return INDseDeliveryQuantity.EditValue
        End Get
        Set(value As Integer)
            INDseDeliveryQuantity.EditValue = value
        End Set
    End Property

    Public Property PendingQuantity As Integer
        Get
            Return INDsePendingQuantity.EditValue
        End Get
        Set(value As Integer)
            INDsePendingQuantity.EditValue = value
        End Set
    End Property

    Public Property RealPreviousDeliveryQuantity As Integer

    Public Property IsDeferred As Boolean
        Get
            Return INDsleIsDeferred.EditValue
        End Get
        Set(value As Boolean)
            INDsleIsDeferred.EditValue = value
        End Set
    End Property

    Public Property ProductText As String
        Get
            Return INDpceProducts.Text
        End Get
        Set(value As String)
            INDpceProducts.Text = value
        End Set
    End Property

    Public Property TreatmentDays As Integer
        Get
            Return INDseTreatmentDays.EditValue
        End Get
        Set(value As Integer)
            INDseTreatmentDays.EditValue = value
        End Set
    End Property

    Public Property DiagnosticXpo As XPInstantFeedbackSource
        Get
            Return INDSleDiagnostic.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDiagnostic.Properties.DataSource = value
        End Set
    End Property

    Public Property DiagnosticCode As String
        Get
            Return INDSleDiagnostic.EditValue
        End Get
        Set(value As String)
            INDSleDiagnostic.EditValue = value
        End Set
    End Property

    Public Property AuthorizationNumber As String
        Get
            Return INDtxtAuthorizationNumber.EditValue
        End Get
        Set(value As String)
            INDtxtAuthorizationNumber.EditValue = value
        End Set
    End Property

    Public Property IDMipres As String
        Get
            Return INDtxtIDMipress.EditValue
        End Get
        Set(value As String)
            INDtxtIDMipress.EditValue = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event GetCUM(senser As Object, e As GetCUMEventArgs)

#End Region

#Region "Methods"

    Private Sub InitializeTuple()
        ListIsDeferred = New List(Of Tuple(Of Boolean, String))()
        ListIsDeferred.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListIsDeferred.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleIsDeferred.Properties.DataSource = ListIsDeferred
    End Sub

    Private Function ValidateControlsForm() As String
        Dim errors As New StringBuilder

        If String.IsNullOrEmpty(INDpceProducts.Text.Trim()) Then
            errors.AppendLine("Debe ingresar un producto")
        End If

        If String.IsNullOrEmpty(INDsleIsDeferred.Text) Then
            errors.AppendLine("Debe seleccionar si es diferido")
        End If

        If INDseRequestQuantity.EditValue = Nothing OrElse INDseRequestQuantity.EditValue <= 0 Then
            errors.AppendLine("Debe ingresar cantidad solicitada válida")
        End If

        If INDsePendingQuantity.EditValue IsNot Nothing AndAlso INDsePendingQuantity.EditValue < 0 Then
            errors.AppendLine("La cantidad pendiente no es válida")
        End If

        If INDsleIsDeferred.EditValue = True AndAlso ViewDashboardPharmacyDetail IsNot Nothing Then
            If ViewDashboardPharmacyDetail.ListDeferred Is Nothing OrElse ViewDashboardPharmacyDetail.ListDeferred.Count = 0 Then
                errors.AppendLine("El producto es diferido pero no se ha realizado el cálculo de las entregas")
            End If
        End If

        If INDSleDiagnostic.EditValue = Nothing OrElse String.IsNullOrEmpty(INDSleDiagnostic.EditValue) Then
            errors.AppendLine("Debe seleccionar un diagnóstico válido.")
        End If

        If INDseTreatmentDays.EditValue = Nothing OrElse INDseTreatmentDays.EditValue <= 0 Then
            errors.AppendLine("El registro del campo debe ser diferente a cero.")
        End If

        Return errors.ToString()
    End Function

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

    Private Sub AssignProductWithCode()
        If INDpceProducts.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDpceProducts.Text.Split(" - ")
            'Capturo el codigo del producto
            ProductCode = arrayCodeProduct(0).Trim

            If ProductCode Is String.Empty Then
                Exit Sub
            End If

            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                ProductXpo = model.GetProductXpoByCode(ProductCode)
                'Valido que exista el producto
                If ProductXpo Is Nothing Then
                    INDpceProducts.Text = String.Empty
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto no existe")
                    Exit Sub
                End If
                'Valido que el producto no este inactivo
                If ProductXpo.Status = False Then
                    INDpceProducts.Text = String.Empty
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + ProductXpo.Code + " - " + ProductXpo.Name + " está inactivo")
                    Exit Sub
                End If
            End Using

            INDseDeliveryQuantity.EditValue = 0
            INDPceBatchSerialOutput.EditValue = Nothing
            CtrPhysicalInventory1.CleanControls()
            listPhysicalInventory = Nothing

            If ProductXpo.ProductSubGroupId.HandlesBatch = True Then
                INDseDeliveryQuantity.Properties.ReadOnly = True
                INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                CtrPhysicalInventory1.Product = New InventoryProduct() With {.Id = ProductXpo.Id, .Code = ProductXpo.Code, .Name = ProductXpo.Name}
                CtrPhysicalInventory1.FormOwner = Me
                CtrPhysicalInventory1.SetListPhysicalInventory()

                INDPceBatchSerialOutput.Focus()
                INDPceBatchSerialOutput.ShowPopup()
            Else
                INDseDeliveryQuantity.Properties.ReadOnly = False
                INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDseDeliveryQuantity.Focus()
            End If

            INDpceProducts.Text = ProductXpo.Code + " - " + ProductXpo.Name
            INDseRequestQuantity.Focus()
        End If
    End Sub

    Private Sub AddProduct()
        'Se validan los controles
        Dim errors As String = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        AsyncLoader(True)
        Using model As New MDashBoardPharmacy(Me.Tag)
            'Se asigna el código del medicamento
            If ProductXpo.ATCId IsNot Nothing Then
                ProductCode = ProductXpo.ATCId.Code
                ProductType = 1
            End If

            'Se asigna el código del insumo
            If ProductXpo.SupplieId IsNot Nothing Then
                ProductCode = ProductXpo.SupplieId.Code
                ProductType = 2
            End If

            If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                listPhysicalInventoryCrystalProduct = listPhysicalInventory
            Else
                listPhysicalInventoryCrystalProduct = model.ListPhysicalInventoryByATCNumber(ProductCode, ProductType)

                If listPhysicalInventoryCrystalProduct Is Nothing OrElse listPhysicalInventoryCrystalProduct.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No existe inventario físico con el producto seleccionado"
                    listPhysicalInventoryCrystalProduct = New List(Of PhysicalInventory)
                Else
                    listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.Where(Function(d) d.WarehouseId = WareHouseId).ToList()
                    If listPhysicalInventoryCrystalProduct Is Nothing OrElse listPhysicalInventoryCrystalProduct.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No existe inventario físico con el producto en el almacén seleccionado"
                        listPhysicalInventoryCrystalProduct = New List(Of PhysicalInventory)
                    End If
                End If

                If listPhysicalInventoryCrystalProduct IsNot Nothing AndAlso listPhysicalInventoryCrystalProduct.Count > 0 And INDseDeliveryQuantity.EditValue = 0 Then 'Se valida la cantidad a entregar si hay cantidades en el inventario fisico
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = "Existe inventario físico por lo tanto debe ingresar cantidad a entregar"
                    Exit Sub
                End If
            End If
        End Using

        'Se valida que la cantidad en el inventario fisico sea menor o igual a la cantidad a entregar
        If INDseDeliveryQuantity.EditValue > listPhysicalInventoryCrystalProduct.Sum(Function(d) d.Quantity) Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = String.Format("La cantidad en el inventario {0} es menor a la cantidad a entregar {1}", listPhysicalInventoryCrystalProduct.Sum(Function(d) d.Quantity), INDseDeliveryQuantity.EditValue)
            Exit Sub
        End If

        If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            'Se asigna la cantidad a entregar
            Dim outStandingQuantity = INDseDeliveryQuantity.EditValue
            For Each itemPhysical In listPhysicalInventoryCrystalProduct
                If itemPhysical.Quantity >= outStandingQuantity Then
                    itemPhysical.QuantityDeliver = outStandingQuantity
                    Exit For
                Else
                    itemPhysical.QuantityDeliver = itemPhysical.Quantity
                    outStandingQuantity -= itemPhysical.Quantity
                End If
            Next
        End If

        AsyncLoader(False)
        Dim args As New GetCUMEventArgs

        If INDsleIsDeferred.EditValue = False Then
            ViewDashboardPharmacyDetail.ListDeferred = Nothing
        End If

        If EditMode = False Then
            With ViewDashboardPharmacyDetail
                .Ingreso = ""
                .Entidad = ""
                .CodigoEntidad = ""
                .CodigoContrato = ""
                .CodigoPlan = ""

                .ProductId = ProductXpo.Id
                .Producto = INDpceProducts.Text
                If ProductXpo.ATCId IsNot Nothing Then 'Si el producto seleccionado tiene atc asociado
                    .MedicamentCode = ProductXpo.ATCId.Code
                    .MedicamentName = ProductXpo.ATCId.Name
                    .TypeProduct = 1
                Else 'Si no tiene atc asociado
                    .MedicamentCode = ProductXpo.SupplieId.Code
                    .MedicamentName = ProductXpo.SupplieId.SupplieName
                    .TypeProduct = 2
                End If

                .Tipo = "1"
                .CantidadSolicitada = INDseRequestQuantity.EditValue
                .CantidadEntregada = DeliveryQuantity
                .CantidadPendiente = .CantidadSolicitada - DeliveryQuantity
                .Unico = False
                .NOPOS = False
                .UnidadMedida = ""
                .Medico = ""
                .NitMedico = ""
                .FilaSeleccionada = 0
                .IDETIPHIS = ""
                .NUMEFOLIO = ""
                .Opcion = "0"
                .OpcionAnulado = "0"
                .Procedimiento = ""
                .MarcarOpcion = False
                .CodigoPaciente = PatientCode
                .Consecutivo = 0
                .Estado = 1
                .Especialidad = ""
                .MedicalFormulaDetailId = 0
                .ListPhysicalInventory = Nothing
                .TypeProduct = ProductType
                .DiagnosticCode = Me.DiagnosticCode
                .TreatmentDays = Me.TreatmentDays
                .AuthorizationNumber = Me.AuthorizationNumber
                .IDMipres = Me.IDMipres
                If listPhysicalInventoryCrystalProduct IsNot Nothing AndAlso listPhysicalInventoryCrystalProduct.Count > 0 Then
                    .ListPhysicalInventory = listPhysicalInventoryCrystalProduct.FindAll(Function(x) x.QuantityDeliver > 0)
                End If
            End With
            args.ViewDashboardPharmacyDetail = ViewDashboardPharmacyDetail

        Else
            args.RequestQuantity = INDseRequestQuantity.EditValue
            args.DeliveryQuantity = INDseDeliveryQuantity.EditValue
            args.PendingQuantity = INDseRequestQuantity.EditValue - RealPreviousDeliveryQuantity - INDseDeliveryQuantity.EditValue
            args.ListPhysicalInventory = Nothing
            If listPhysicalInventoryCrystalProduct IsNot Nothing AndAlso listPhysicalInventoryCrystalProduct.Count > 0 Then
                args.ListPhysicalInventory = listPhysicalInventoryCrystalProduct.FindAll(Function(x) x.QuantityDeliver > 0)
            End If

            args.ListDeferred = ViewDashboardPharmacyDetail.ListDeferred
        End If

        args.EditMode = EditMode
        RaiseEvent GetCUM(Nothing, args)
        Me.Close()
    End Sub

    Private Sub CleanControls()
        Me.TreatmentDays = 0
        Me.DiagnosticCode = String.Empty
        Me.AuthorizationNumber = String.Empty
        Me.IDMipres = String.Empty
        Me.DiagnosticXpo = Nothing
        Me.INDSleDiagnostic.Properties.NullText = Nothing
    End Sub

    ''' <summary>
    ''' Establece la edad en dias, meses y años del paciente, y el sexo.
    ''' </summary>
    Private Sub SetPatientInformation()
        PatientSex = Patient.IPSEXOPAC

        ' Fecha de nacimiento y fecha actual
        Dim birthDate As DateTime = Patient.IPFECNACI
        Dim currentDate As DateTime = DateTime.Now

        ' Cálculo de edad en años (para mantener la compatibilidad con código existente)
        Dim years As Integer = currentDate.Year - birthDate.Year
        If currentDate.Month < birthDate.Month OrElse
      (currentDate.Month = birthDate.Month AndAlso currentDate.Day < birthDate.Day) Then
            years -= 1
        End If
        PatientAge = years

        ' Cálculo de edad en días
        Dim days As Integer = CInt(currentDate.Subtract(birthDate).TotalDays)
        PatientAgeInDays = days

        ' Cálculo de edad en meses (aproximado)
        Dim months As Integer = (years * 12) + (currentDate.Month - birthDate.Month)
        If currentDate.Day < birthDate.Day Then
            months -= 1
        End If
        PatientAgeInMonths = months
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmPopupProductsDispensing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        SetPatientInformation()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleIsDeferred_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIsDeferred.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            If String.IsNullOrEmpty(INDpceProducts.Text) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un producto"
                Exit Sub
            End If

            If INDseRequestQuantity.EditValue = Nothing OrElse INDseRequestQuantity.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar la cantidad solicitada"
                Exit Sub
            End If

            Me.Cursor = ChangeCursorIndigo()
            With ViewDashboardPharmacyDetail
                .MedicamentCode = If(ProductXpo.ATCId IsNot Nothing, ProductXpo.ATCId.Code, ProductXpo.Code)
            End With
            Using Form As New FrmPopupDeferredDispensing(ViewDashboardPharmacyDetail, dateDispensing)
                AddHandler Form.SuccessDeferredEventArgs, AddressOf ReturnDeferred
                Form.ProductCode = If(ProductXpo.ATCId IsNot Nothing, ProductXpo.ATCId.Code, ProductXpo.Code)
                Form.StartPosition = FormStartPosition.CenterParent
                Dim trasparent As New FrmTransparent(Form, False)
                Me.Cursor = Cursors.Default
                trasparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    Private Sub ReturnDeferred(sender As Object, e As SuccessEventArgs)
        If e IsNot Nothing Then
            ViewDashboardPharmacyDetail.ListDeferred = e.ListDeferred

            Dim listTemp = (From x In e.ListDeferred Where x.DeliveryDate <= GetDateServer() Select x).ToList()
            ViewDashboardPharmacyDetail.CantidadSolicitada = listTemp.Sum(Function(x) x.DeliveryQuantity)
            ViewDashboardPharmacyDetail.CantidadPendiente = listTemp.Sum(Function(x) x.PendingQuantity)

            INDseRequestQuantity.EditValue = ViewDashboardPharmacyDetail.CantidadSolicitada
            If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDseDeliveryQuantity.EditValue = 0
            End If
            INDsePendingQuantity.EditValue = ViewDashboardPharmacyDetail.CantidadPendiente

            INDpceProducts.Enabled = False
            INDseRequestQuantity.Enabled = False
            INDseDeliveryQuantity.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDpceProducts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceProducts.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDSleDiagnostic_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDiagnostic.QueryPopUp
        If DiagnosticXpo Is Nothing Then
            Using model As New MDispensingByPatientMedilaser(Me.Tag)
                DiagnosticXpo = model.ListINDIAGNOSPerFilter(PatientSex, PatientAge, PatientAgeInMonths, PatientAgeInDays)
                DiagnosticXpo.Refresh()
            End Using
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDpceProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            AssignProductWithCode()
        End If
    End Sub

    Private Sub FrmPopupProductsDispensing_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDseDeliveryQuantity_KeyDown(sender As Object, e As KeyEventArgs) Handles INDseDeliveryQuantity.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "Selected"

    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDpceProducts.Text = e.CodeNameProduct
        INDpceProducts.Focus()
        INDpceProducts.ClosePopup()
        AssignProductWithCode()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPopupProductsDispensing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If EditMode = False Then
            INDpceProducts.Focus()
        Else
            INDsleIsDeferred.Focus()
        End If

        InitializeTuple()
        CtrPhysicalInventory1.MovementType = 2
        CtrPhysicalInventory1.WareHouseId = WareHouseId
        Me.CleanControls()
        If EditMode Then
            RealPreviousDeliveryQuantity = ViewDashboardPharmacyDetail.QuantityAsignedByHEON
            PendingQuantity = ViewDashboardPharmacyDetail.CantidadPendiente
            DeliveryQuantity = ViewDashboardPharmacyDetail.CantidadEntregada
            RequestQuantity = ViewDashboardPharmacyDetail.CantidadSolicitada
            IsLoad = True
            IsDeferred = ViewDashboardPharmacyDetail.IsDeferred
            IsLoad = False
            ProductText = ViewDashboardPharmacyDetail.Producto
            ProductCode = ViewDashboardPharmacyDetail.Producto.Split(" - ").ElementAt(0).Trim()
            Me.TreatmentDays = ViewDashboardPharmacyDetail.TreatmentDays
            Me.DiagnosticCode = ViewDashboardPharmacyDetail.DiagnosticCode
            Me.INDSleDiagnostic.Properties.NullText = ViewDashboardPharmacyDetail.DiagnosticCode
            Me.IDMipres = ViewDashboardPharmacyDetail.IDMipres
            Me.AuthorizationNumber = ViewDashboardPharmacyDetail.AuthorizationNumber

            INDpceProducts.Enabled = False
            If (ViewDashboardPharmacyDetail.ListDeferred IsNot Nothing AndAlso ViewDashboardPharmacyDetail.ListDeferred.Count > 0) OrElse ViewDashboardPharmacyDetail.MedicalFormulaDetailId > 0 Then
                INDseRequestQuantity.Enabled = False
            End If
            If ViewDashboardPharmacyDetail.MedicalFormulaDetailId > 0 Then
                INDsleIsDeferred.Properties.ReadOnly = True
            End If

            Using model As New MInventoryProduct(Me.Tag)
                ProductXpo = model.GetProductXpoByCode(ProductCode)
            End Using

            If ProductXpo?.ProductSubGroupId?.HandlesBatch = True Then
                INDseDeliveryQuantity.Properties.ReadOnly = True
                INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                If ViewDashboardPharmacyDetail.ListPhysicalInventory IsNot Nothing AndAlso ViewDashboardPharmacyDetail.ListPhysicalInventory.Count > 0 Then
                    INDPceBatchSerialOutput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", "Inventory"), ViewDashboardPharmacyDetail.ListPhysicalInventory.Count.ToString())
                Else
                    INDPceBatchSerialOutput.EditValue = String.Empty
                End If

                CtrPhysicalInventory1.Product = New InventoryProduct() With {.Id = ProductXpo.Id, .Code = ProductXpo.Code, .Name = ProductXpo.Name}
                CtrPhysicalInventory1.FormOwner = Me
                CtrPhysicalInventory1.SetListPhysicalInventory()

                If ViewDashboardPharmacyDetail.ListPhysicalInventory IsNot Nothing AndAlso ViewDashboardPharmacyDetail.ListPhysicalInventory.Count > 0 Then
                    CtrPhysicalInventory1.SetQuantityPhysicalInventoryByManualDispensing(ViewDashboardPharmacyDetail.ListPhysicalInventory.ToList())
                    listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
                End If
            Else
                INDseDeliveryQuantity.Properties.ReadOnly = False
                INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDseDeliveryQuantity.Focus()
            End If
        Else
            ViewDashboardPharmacyDetail = New Domain.Crystal.Entities.ViewDashboardPharmacyDetail
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddProduct()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDseRequestQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDseRequestQuantity.EditValueChanged
        INDsePendingQuantity.EditValue = INDseRequestQuantity.EditValue - RealPreviousDeliveryQuantity - INDseDeliveryQuantity.EditValue

        If EditMode = False Then
            ViewDashboardPharmacyDetail.CantidadSolicitada = INDseRequestQuantity.EditValue
        End If
    End Sub

    Private Sub INDseDeliveryQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDseDeliveryQuantity.EditValueChanged
        INDsePendingQuantity.EditValue = INDseRequestQuantity.EditValue - RealPreviousDeliveryQuantity - INDseDeliveryQuantity.EditValue
    End Sub

    Private Sub INDsleIsDeferred_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIsDeferred.EditValueChanged
        If INDsleIsDeferred.EditValue = True Then
            Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleIsDeferred.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
            If info Is Nothing Then
                Dim editor As New DevExpress.XtraEditors.Controls.EditorButton
                editor.Image = Presentation.Inventory.My.Resources.Resources.BuscarMetro
                editor.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                editor.Visible = True
                INDsleIsDeferred.Properties.Buttons.Add(editor)
            End If
        Else
            If INDsleIsDeferred.Properties.Buttons.Count = 2 Then
                Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleIsDeferred.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                If info IsNot Nothing Then
                    INDsleIsDeferred.Properties.Buttons.RemoveAt(info.Index)
                End If
            End If
            If IsLoad = False Then
                INDseRequestQuantity.Enabled = True
                If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                    INDseDeliveryQuantity.EditValue = 0
                End If
            End If
        End If
        If EditMode = False Then
            ViewDashboardPharmacyDetail.IsDeferred = INDsleIsDeferred.EditValue
        End If
    End Sub

#End Region

#Region "Popup"

    Private Sub INDPceBatchSerialOutput_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerialOutput.Popup
        CtrPhysicalInventory1.SetFocusGrid()
    End Sub

#End Region

#Region "Closed"

    Private Sub INDPceQuantity_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerialOutput.Closed
        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If listPhysicalInventory.Count > 0 Then
            INDPceBatchSerialOutput.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", "Inventory"), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceBatchSerialOutput.EditValue = Nothing
        End If
    End Sub

#End Region

#Region "ChangeQuantity"

    Private Sub CtrPhysicalInventory1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrPhysicalInventory1.ChangeQuantity
        INDseDeliveryQuantity.EditValue = e.Quantity
    End Sub

#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra("2033")
    End Sub

#End Region

End Class