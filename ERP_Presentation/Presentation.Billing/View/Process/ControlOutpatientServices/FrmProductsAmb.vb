Imports DevExpress.XtraLayout
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Class FrmProductsAmb

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(Permissions As Dictionary(Of Integer, String), FACMECONINS As Byte?,
                   CODBODEGA As String, CODCENCOS As String,
                   Patient As Domain.Crystal.Entities.INPACIENT, AdmissionNumber As String,
                   OperatingUnitId As Integer, ProductCitas As List(Of Domain.Entities.ProductCita),
                   ThirdPartyPatientId As Integer?, FunctionalUnitId As Integer?, CodCentroAtencion As String,
                   CodUnifunc As String)
        InitializeComponent()
        Me.Permissions = Permissions
        Me.FACMECONINS = FACMECONINS
        Me.CODBODEGA = CODBODEGA
        Me.CODCENCOS = CODCENCOS
        Me.Patient = Patient
        Me.AdmissionNumber = AdmissionNumber
        Me.OperatingUnitId = OperatingUnitId
        Me.ProductCitas = ProductCitas
        Me.ThirdPartyPatientId = ThirdPartyPatientId
        Me.FunctionalUnitId = FunctionalUnitId
        Me.CodCentroAtencion = CodCentroAtencion
        Me.CodUnifunc = CodUnifunc
    End Sub

#Region "Properties"
    Private _params As List(Of Tuple(Of Integer, String))

    Private model As Presentation.Billing.MVP.MControlOutpatientServices

    Private Property ProductXpo As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

    Public ReadOnly Property Parametros As List(Of Tuple(Of Integer, String))
        Get
            If _params Is Nothing Then
                _params = New List(Of Tuple(Of Integer, String))()
                _params.Add(New Tuple(Of Integer, String)(1, "Generar Solicitud de Dispensación"))
                _params.Add(New Tuple(Of Integer, String)(2, "Facturar Productos Como Servicios"))
                '_params.Add(New Tuple(Of Integer, String)(3, "Por selección del usuario"))
            End If
            Return _params
        End Get
    End Property

    Public WriteOnly Property FACMECONINS As Byte?
        Set(value As Byte?)
            If value <> 3 Then
                GleMedios.EditValue = value
                GleMedios.Properties.ReadOnly = True
            Else
                GleMedios.EditValue = Nothing
                GleMedios.Properties.ReadOnly = False
            End If
        End Set
    End Property

    Public Property OptionMedios As Byte
        Get
            Return GleMedios.EditValue
        End Get
        Set(value As Byte)
            GleMedios.EditValue = value
        End Set
    End Property

    'Property CitasNoMedicas As List(Of Domain.Crystal.Entities.SP_AD_ListarCitasMedicasNativo_Result)
    Private _productCitas As List(Of Domain.Entities.ProductCita)
    Public Property ProductCitas As List(Of Domain.Entities.ProductCita)
        Get
            Return _productCitas
        End Get
        Set(value As List(Of Domain.Entities.ProductCita))
            _productCitas = value
            GcProducts.DataSource = value
        End Set
    End Property

    Property Permissions As Dictionary(Of Integer, String)
    Property Patient As Domain.Crystal.Entities.INPACIENT
    Property AdmissionNumber As String
    Property OperatingUnitId As Integer
    Property ThirdPartyPatientId As Integer?
    Property FunctionalUnitId As Integer?
    Property CodCentroAtencion As String
    Property CodUnifunc As String
    Property CODBODEGA As String
    Property CODCENCOS As String
#End Region

#Region "Handlers"

    Private Sub FrmProductsAmb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GleMedios.Properties.DataSource = Parametros
        model = New MVP.MControlOutpatientServices(Me.Tag)
        ConsultarProductos()
        If Not Permissions.ContainsKey(CInt(PermissionsActionsForm.ModificarCantidadProducto)) Then
            ColQuantity.OptionsColumn.AllowEdit = False
            ColQuantity.OptionsColumn.AllowFocus = False
            INDLciAddMoreProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDSnAcept_Click(sender As Object, e As EventArgs) Handles INDSnAcept.Click
        Dim errors As New System.Text.StringBuilder()
        If INDSleProduct.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un producto")
        End If
        If INDSpnQuantity.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una cantidad")
        End If
        If errors.Length > 0 Then
            ShowMessage(eStatusResult.WARNING) = errors.ToString()
            Exit Sub
        End If
        If _productCitas.Any(Function(x) x.Product.Code = ProductXpo.Code) Then
            ShowMessage(eStatusResult.WARNING) = "El producto ya existe en el listado"
            Exit Sub
        End If
        Dim prod As New Domain.Entities.InventoryProduct()
        prod.Id = ProductXpo.Id
        prod.Code = ProductXpo.Code
        prod.Name = ProductXpo.Name
        If prod.ProductType Is Nothing Then
            prod.ProductType = New Domain.Entities.ProductType()
        End If
        prod.ProductType.Class = ProductXpo.ProductTypeId.Class
        prod.ProductGroupId = ProductXpo.ProductGroupId.Id
        prod.ProductCost = ProductXpo.ProductCost

        Dim p As New Domain.Entities.ProductCita()
        p.Product = prod
        p.CANTRECUR = CInt(INDSpnQuantity.EditValue)

        _productCitas.Add(p)
        ProductCitas = _productCitas
        GcProducts.RefreshDataSource()

        INDSleProduct.EditValue = Nothing
        INDSpnQuantity.EditValue = 1
        INDSleProduct.Focus()

    End Sub

    Private Sub INDSleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProduct.EditValueChanged
        If INDSleProduct.EditValue IsNot Nothing Then
            ProductXpo = CType(CType(INDSleProduct.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        Else
            ProductXpo = Nothing
        End If
    End Sub

    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            INDSleProduct.Properties.DataSource = model.GetAllProductsByClasses("2", "3")
        End If
    End Sub

    Private Async Sub BtnAcept_Click(sender As Object, e As EventArgs) Handles BtnAcept.Click
        Try
            If GleMedios.EditValue Is Nothing Then
                ShowMessage(EeventViewerImages.Advertencia) = "Seleccione un medio para distribuir los productos"
                Exit Sub
            End If
            If GvProducts.SelectedRowsCount = 0 Then
                ShowMessage(EeventViewerImages.Advertencia) = "Seleccione al menos un producto"
                Exit Sub
            End If

            AsyncLoader()
            Dim args As Object = New Dynamic.ExpandoObject()
            args.OptionMedios = OptionMedios
            args.PatientCode = Patient.IPCODPACI
            args.AdmissionNumber = AdmissionNumber

            If OptionMedios = 1 Then


                args.CodCentroAtencion = CodCentroAtencion
                args.CodUnifunc = CodUnifunc
                args.CODBODEGA = CODBODEGA
                args.CODCENCOS = CODCENCOS

                Dim details As New Concurrent.ConcurrentBag(Of Object)()
                For Each row As Integer In GvProducts.GetSelectedRows()
                    Dim p As Domain.Entities.ProductCita = CType(GvProducts.GetRow(row), Domain.Entities.ProductCita)
                    Dim obj As Object = New Dynamic.ExpandoObject()
                    obj.ProductId = p.Product.Id
                    If p.Product.ATC IsNot Nothing Then
                        obj.ProductCode = p.Product.ATC.Code
                        obj.ProductName = p.Product.ATC.Name
                    Else
                        obj.ProductCode = p.Product.Code
                        obj.ProductName = p.Product.Name
                    End If

                    obj.Quantity = p.CANTRECUR
                    obj.TipoRegist = p.Product.ProductType.Class
                    details.Add(obj)
                Next
                args.Products = New List(Of Object)(details.ToArray())


            ElseIf OptionMedios = 2 Then

                If OperatingUnitId = 0 Then
                    ShowMessage(EeventViewerImages.Advertencia) = "Seleccione una unidad Operativa"
                    AsyncLoader(False)
                    Exit Sub
                End If

                args.OperatingUnitId = OperatingUnitId
                args.CareGroupId = Patient.GENCAREGROUP
                args.HealthAdministratorId = Patient.GENCONENTITY
                args.ThirdPartyId = ThirdPartyPatientId
                args.FunctionalUnitId = FunctionalUnitId

                Dim details As New Concurrent.ConcurrentBag(Of Object)()
                For Each row As Integer In GvProducts.GetSelectedRows()
                    Dim p As Domain.Entities.ProductCita = CType(GvProducts.GetRow(row), Domain.Entities.ProductCita)
                    Dim obj As Object = New Dynamic.ExpandoObject()
                    obj.ProductId = p.Product.Id
                    obj.ProductGroupId = p.Product.ProductGroupId
                    obj.ProductCode = p.Product.Code
                    obj.ProductName = p.Product.Name
                    obj.Quantity = p.CANTRECUR
                    obj.ProductCost = p.Product.ProductCost
                    details.Add(obj)
                Next
                args.Products = New List(Of Object)(details.ToArray())

            End If

            Dim result = Await model.GenerateServiceOrderProductsAsync(args)
            If result IsNot Nothing Then
                ShowMessage(result.StatusCode) = result.Message
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me.Close()
                End If
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
#End Region

#Region "Methods"
    Public Sub AsyncLoader(Optional State As Boolean = True)
        If State Then
            INDLciAsyncLoader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciAsyncLoader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        For i = 0 To Me.PcRoot.Controls.Count - 1
            If Me.PcRoot.Controls(i).GetType Is GetType(LayoutControl) Then
                Me.PcRoot.Controls(i).Enabled = Not State
                Exit For
            End If
        Next
    End Sub

    Private Sub ConsultarProductos()
        If ProductCitas IsNot Nothing AndAlso ProductCitas.Count > 0 Then
            Dim validationMessage As New System.Text.StringBuilder()
            For Each pc As Domain.Entities.ProductCita In ProductCitas
                If pc.REACALAUT IsNot Nothing AndAlso pc.REACALAUT AndAlso Patient.PESO IsNot Nothing Then
                    If pc.Product.ATC Is Nothing Then
                        validationMessage.AppendLine(String.Format("El producto {0} no tiene relacionado un ATC por tanto no se pudo hacer el calculo automático de la cantidad", String.Concat(pc.Product.Code, " - ", pc.Product.Name)))
                    ElseIf pc.Product.ATC.Volume Is Nothing Then
                        validationMessage.AppendLine(String.Format("El producto {0} no configurado Volumen por tanto no se pudo hacer el calculo automático de la cantidad", String.Concat(pc.Product.Code, " - ", pc.Product.Name)))
                    Else
                        Dim quantity As Decimal = Math.Ceiling(CDec((pc.MLPRODUCT * Patient.PESO) / CDec(pc.KGPRODUCT * 1000)) / CDec(pc.Product.ATC.Volume))
                        pc.CANTRECUR = quantity
                    End If
                End If
            Next
            If validationMessage.Length > 0 Then
                ShowMessage(eStatusResult.WARNING) = validationMessage.ToString()
            End If
            GvProducts.SelectAll()
        End If
    End Sub

    Public WriteOnly Property ShowMessage(Icono As Base.EeventViewerImages) As String
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

    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
#End Region

End Class