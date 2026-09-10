'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Rafael Patiño
' Created          : 29-08-2018
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Windows.Forms
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Common.MVP
Imports Domain.Crystal.Entities
Imports DevExpress.XtraGrid.Columns
Imports Domain.Entities
Imports System.Text
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Dynamic
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid.Views.Grid

#End Region

Public Class FrmPopUpDashBoardPharmacySurgycalPackage
    Implements IDashBoardPharmacyDetail

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

#End Region

#Region "EVENTS"

    ''' <summary>
    ''' evento que se dispara cuando el formulario se cierre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event DashBoardPharmacySurgicalPackageSuccessEventArgs(sender As Object, e As DashBoardPharmacyEventArgs)

#End Region

#Region "BUILDER"

    Public Sub New(Optional _dispensingIntegration As Boolean = False)
        InitializeComponent()
        ctrTmp = New CtrMoreInfoDashboardPharmacy()
        ctrTmp.SetInfoFunction(AddressOf getInfoDashBoardPharmacy)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        INDGvDashboardPharmacyDetail.OptionsView.ShowAutoFilterRow = False
        DispensingIntegration = _dispensingIntegration
    End Sub

#End Region

#Region "GLOBALS"

    Private ctrTmp As CtrMoreInfoDashboardPharmacy

    Dim listDashboardPharmacyDetail As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)

    ''' <summary>
    ''' listado del detalle de la dispensacion farmaceutica
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPharmaceuticalDispensingDetail As List(Of PharmaceuticalDispensingDetail)

    ''' <summary>
    ''' listado de la cabecera de la dispensacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPharmaceuticalDispensing As List(Of PharmaceuticalDispensing)

    Dim applyProcedureId As Integer?

    Dim codeNameApplyProcedure As String

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PDashBoardPharmacyDetail

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Id de la Unidad Operativa Seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    Dim listPhysicalInventoryTmp As List(Of PhysicalInventory)

    Dim dictionaryPhysicalInventoryBarCode As Dictionary(Of String, List(Of PhysicalInventory))
    Dim dictionaryProduct As Dictionary(Of Integer, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
    Dim dictionaryAtcCode As Dictionary(Of Integer, String)
    Dim dictionarySuppliedCode As Dictionary(Of Integer, String)

    Dim _currentDate As Date
    Dim _settingInventory As SettingInventory
    Dim _settingsContractXpo As Infrastructure.Data.Xpo.ContractRepository.SettingsContractXpo
    Dim _admission As Infrastructure.Data.Xpo.CrystalRepository.AdmissionXpo
    Dim _functionalUnit As Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit
    Dim _careGroup As Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupXpo
    Dim _errors As New StringBuilder
    Dim healthAdministratorId As Integer? = Nothing
    Dim thirdPartyId As Integer

    ''' <summary>
    ''' filtros para detalles que llegan desde dashboard farmacia
    ''' </summary>
    Public typeFilter As String

    ''' <summary>
    ''' Obtiene o establece la configuracion de la compañia
    ''' </summary>
    Private _companySettings As CompanySettings

    ''' <summary>
    ''' Tipo de redondeo de la moneda parametrizada
    ''' </summary>
    Private _roundingType As Byte

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Permite saber si esta en integración con Heon
    ''' </summary>
    Private DispensingIntegration As Boolean

    Private _webServiceObject As WebServiceObject

    ''' <summary>
    ''' Objeto que es devuelto por el api de Heon
    ''' </summary>
    ''' <returns></returns>
    Public Property WebServiceObject As WebServiceObject
        Get
            Return _webServiceObject
        End Get
        Set(value As WebServiceObject)
            _webServiceObject = value
        End Set
    End Property

    Private _args As Object
    Public Property Args As Object
        Get
            Return _args
        End Get
        Set(value As Object)
            _args = value
        End Set
    End Property

    WriteOnly Property SetFocusCUM As Boolean
        Set(value As Boolean)
            If value Then
                INDMeCUM.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' codico del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientCode As String

    Public WriteOnly Property PatientCode As String
        Set(value As String)
            _patientCode = value
        End Set
    End Property

    Private _store As String

    Public Property Store As String
        Get
            Return _store
        End Get
        Set(value As String)
            _store = value
            If _settingInventory IsNot Nothing Then
                LoadWarehouses()
            End If
        End Set
    End Property

    ''' <summary>
    ''' nombre del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientName As String

    Public WriteOnly Property PatientName As String
        Set(value As String)
            _patientName = value
        End Set
    End Property

    ''' <summary>
    ''' numero del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim _admissionNumber As String

    Public WriteOnly Property AdmissionNumber As String
        Set(value As String)
            _admissionNumber = value
        End Set
    End Property

    Dim _Procedimiento As String
    Public WriteOnly Property Procedimiento As String
        Set(value As String)
            INDTxtProcedureQx.Text = value

        End Set
    End Property

    ''' <summary>
    ''' fecha de nacimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _birthDay As Date?
    Public WriteOnly Property BirthDay As Date?
        Set(value As Date?)
            _birthDay = value
        End Set
    End Property

    ''' <summary>
    ''' consecutivo de la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Dim _consecutive As Decimal

    Public WriteOnly Property Consecutive As Decimal
        Set(value As Decimal)
            _consecutive = value
        End Set
    End Property

    ''' <summary>
    ''' bodega
    ''' </summary>
    ''' <remarks></remarks>
    Dim _functionalUnitCode As String

    Public WriteOnly Property FunctionalUnitCode As String
        Set(value As String)
            INDTxtFunctionalUnit.Text = value
            Dim functionalUnitTmp() = value.Split("-")
            _functionalUnitCode = functionalUnitTmp(0).Trim()
        End Set
    End Property

    ''' <summary>
    ''' codigo del centro de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _careCenter As String

    Public WriteOnly Property CareCenter As String
        Set(value As String)
            _careCenter = value
        End Set
    End Property

    ''' <summary>
    ''' consecutivo de prescripcion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _consecutivePescription As String

    Public WriteOnly Property ConsecutivePescription As Integer
        Set(value As Integer)
            _consecutivePescription = value
        End Set
    End Property

    ''' <summary>
    ''' consecutivo de insumos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _consecutiveInputs As String

    Public WriteOnly Property ConsecutiveInputs As String
        Set(value As String)
            _consecutiveInputs = value
        End Set
    End Property

    ''' <summary>
    ''' consecutivo de farmacia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _consecutivePharmacy As String

    Public WriteOnly Property ConsecutivePharmacy As Integer
        Set(value As Integer)
            _consecutivePharmacy = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de historia para saber quien la hizo
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Dim _historyType As String

    Public WriteOnly Property HistoryType As String
        Set(value As String)
            _historyType = value
        End Set
    End Property

    Dim _officeType As Integer

    Public WriteOnly Property OfficeType As Integer
        Set(value As Integer)
            _officeType = value
        End Set
    End Property

    Dim _logisticOperator As Integer

    Public WriteOnly Property LogisticOperator As Integer
        Set(value As Integer)
            _logisticOperator = value
        End Set
    End Property

    Dim _medicalOrderRecipe As String

    Public WriteOnly Property MedicalOrderRecipe As String
        Set(value As String)
            _medicalOrderRecipe = value
        End Set
    End Property

    Dim _costCenterId As Integer?

    Public WriteOnly Property CostCenterId As Integer?
        Set(value As Integer?)
            _costCenterId = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDashBoardPharmacyDetail.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IDashBoardPharmacyDetail.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequence As Domain.Entities.InventorySequence Implements IDashBoardPharmacyDetail.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InventorySequence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
            GetNewSequence()
        End Set
    End Property

    ''' <summary>
    ''' Primer nombre del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientFirstName As String

    Public WriteOnly Property PatientFirstName As String
        Set(value As String)
            _patientFirstName = value
        End Set
    End Property

    ''' <summary>
    ''' Otros nombres del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientMiddleName As String

    Public WriteOnly Property PatientMiddleName As String
        Set(value As String)
            _patientMiddleName = value
        End Set
    End Property

    ''' <summary>
    ''' Primer apellido del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientLastName As String

    Public WriteOnly Property PatientLastName As String
        Set(value As String)
            _patientLastName = value
        End Set
    End Property

    ''' <summary>
    ''' Segundo Apellido del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patientSecondLastName As String

    Public WriteOnly Property PatientSecondLastName As String
        Set(value As String)
            _patientSecondLastName = value
        End Set
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PharmacyDashboardFromRequestWarehouse As Boolean
        Get
            Return If(_settingInventory Is Nothing, False, _settingInventory.PharmacyDashboardFromRequestWarehouse)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteOutpatientProducts As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteOutpatientProducts)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteIntrahospitalProducts As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteIntrahospitalProducts)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteProducts As Boolean
        Get
            Dim result As Boolean
            If Me._admission IsNot Nothing Then
                If Me._admission.TIPOINGRE = 1 Then
                    result = Me.RequestQuoteOutpatientProducts
                Else
                    result = Me.RequestQuoteIntrahospitalProducts
                End If
            End If
            Return result
        End Get
    End Property
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task

        ' se consulta los parametros de empresa
        Using model As New MCompanySettings(Me.Tag)
            _companySettings = Await model.GetCompanySettings()
        End Using

        Using Model As New MSettingInventory(Me.Tag)
            'parámetos de contratos
            _settingsContractXpo = _presenter.GetSettingsContractByOperatingUnitId(_idOperativeUnit)

            'parámetros de inventario
            Dim result = Await Model.GetInventorySettingsRegister(_idOperativeUnit)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Exit Function
            End If

            _settingInventory = result.ObjectEmbbeded
            If _settingInventory Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Exit Function
            End If

            LoadWarehouses()
        End Using
    End Function

    ''' <summary>
    ''' Método utilizado para cargar los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadWarehouses()
        Using model As New MDashBoardPharmacy(Me.MyTag)
            Dim warehouseCode = _store.Split("-").ElementAt(0).Trim()
            Dim listWarehouse = model.ListWarehouseUser(If(PharmacyDashboardFromRequestWarehouse, warehouseCode, String.Empty))
            INDTxtStore.Properties.DataSource = listWarehouse
            Dim warehouse = (From w In listWarehouse Select w).FirstOrDefault()
            If warehouse IsNot Nothing Then
                INDTxtStore.EditValue = warehouse.Id
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para pintar la informacion en el control de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoDashBoardPharmacy() As Tuple(Of String, String, Date?)
        Dim patientPrint = _patientCode + " - " + _patientName
        Return New Tuple(Of String, String, Date?)(patientPrint, _admissionNumber, _birthDay)
    End Function

    ''' <summary>
    ''' metodo que obtiene el servicio seleccionado para aplicar a procedimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnSelectProcedure(sender As Object, e As SelectProcedureEventArgs)
        If e.StatusResult = True Then
            applyProcedureId = e.ApplyProcedureId
            codeNameApplyProcedure = e.CodeNameApplyProcedure
        End If
    End Sub

    ''' <summary>
    ''' metodo que asigna valores cuando se han seleccionado cantidades a entregar desde el popup de CUM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetCUM(sender As Object, e As GetCUMEventArgs)
        Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashBoardPharmacy_SurgicalPackageDeatils)
        rowDetail.ListPhysicalInventory = Nothing
        rowDetail.ListPhysicalInventory = e.ListPhysicalInventory
        If rowDetail.ListPhysicalInventory.Count > 0 Then
            rowDetail.CantidadEntregada = e.ListPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
            rowDetail.Action = 1

            If rowDetail.ListPhysicalInventory.FindAll(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) <> INDTxtStore.Text.Split("-").ElementAt(0)).Count = 0 Then
                rowDetail.CUMSource = System.Drawing.Color.Green.ToArgb()
            Else
                rowDetail.CUMSource = System.Drawing.Color.Yellow.ToArgb()
            End If
        Else
            rowDetail.CantidadEntregada = 0
            rowDetail.Action = Nothing
            rowDetail.CUMSource = System.Drawing.Color.Transparent.ToArgb()
        End If

        'Si viene con integración con HEON y la cantidad solicitada está en cero, se asigna la que se ingreso por el usuario
        If DispensingIntegration = True AndAlso rowDetail.QuantityAsignedByHEON = 0 Then
            rowDetail.CantidadSolicitada = e.RequestQuantity
            rowDetail.CantidadPendiente = rowDetail.CantidadSolicitada - rowDetail.CantidadEntregada
        End If

        INDGcDashboardPharmacyDetail.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' metodo para obtener una secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GetNewSequence()
        If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                Dim res = (From ou As InventorySequenceDetail In Me._sequence.InventorySequenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
                If res IsNot Nothing AndAlso res.Count > 0 Then
                    Me._idCurrentSequence = res(0).Id
                Else
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequense(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else

                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' metodo para establecer los valores iniciales a la vista
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Private Sub setValuesView(item As ViewDashBoardPharmacy_SurgicalPackageDeatils)
        item.PattientSupplyOrder = 1
        item.AdmissionNumeber = _admissionNumber
        item.PattientCode = _patientCode
        item.CareCenterCode = _careCenter
        item.FunctionUnitCode = _functionalUnitCode
        item.ConsecutivePescription = _consecutivePescription
        item.ConsecutiveInputs = _consecutiveInputs
        item.ConsecutivePharmacy = _consecutivePharmacy
    End Sub

    ''' <summary>
    ''' metodo para establecer los valores cuando se va anular
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Private Sub SetValueViewAnnular(item As ViewDashBoardPharmacy_SurgicalPackageDeatils)
        item.Action = 2
        item.CantidadEntregada = 0
    End Sub

    ''' <summary>
    ''' metodo para cargar manualmente las cantidades que se van a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ManualDelivery()
        If listDashboardPharmacyDetail.Any(Function(x) x.Action Is Nothing) Then
            Dim errors As New StringBuilder

            Dim parameters As New Dictionary(Of String, String)
            parameters.Add("CareGroupId", _careGroup.Id)
            parameters.Add("UserId", indigo.UserIndigoId)

            Dim listCodes As New List(Of String)
            For Each item In listDashboardPharmacyDetail.FindAll(Function(x) x.Action Is Nothing)
                listCodes.Add(item.CodProducto.Trim())
            Next

            Using model As New MDashBoardPharmacy(Me.Tag)
                Dim listPhysicalInventoryByCode = model.ListPhysicalInventoryByCode(parameters, listCodes)

                For Each item In listDashboardPharmacyDetail.FindAll(Function(x) x.Action Is Nothing)
                    Dim listPhysicalInventoryCrystalProduct = listPhysicalInventoryByCode.Where(Function(p) p.Code = item.CodProducto.Trim()).ToList()
                    If listPhysicalInventoryCrystalProduct.Count = 0 Then
                        errors.AppendLine("No se encontro inventario para el producto " + item.CodProducto + "-" + item.Producto)
                        Continue For
                    End If

                    'Se realiza el ordenamiento de acuerdo al almacen y los productos con la fecha de vencimiento mas próxima
                    Dim warehouseId As Integer? = INDTxtStore.EditValue
                    listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
                            OrderBy(Function(d) If(warehouseId IsNot Nothing AndAlso warehouseId = d.WarehouseId, 0, 1)).
                            ThenBy(Function(d) If(d.Covered, 0, 1)).
                            ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).
                            ThenByDescending(Function(d) d.Quantity).ToList()

                    Dim listPhysicalInventory As New List(Of PhysicalInventory)
                    Dim outStandingQuantity = item.CantidadPendiente
                    For Each itemPhysical In listPhysicalInventoryCrystalProduct
                        Dim itemPhysicalCopy = itemPhysical.Clone()

                        If PharmacyDashboardFromRequestWarehouse AndAlso itemPhysical.WarehouseId <> If(warehouseId IsNot Nothing, warehouseId, 0) Then
                            Continue For
                        End If

                        If itemPhysical.Quantity = itemPhysical.QuantityDeliver Then
                            Continue For
                        End If

                        If (itemPhysical.Quantity - itemPhysical.QuantityDeliver) >= outStandingQuantity Then
                            item.CantidadEntregada += outStandingQuantity
                            itemPhysical.QuantityDeliver += outStandingQuantity
                            itemPhysicalCopy.QuantityDeliver = outStandingQuantity
                            listPhysicalInventory.Add(itemPhysicalCopy)
                            Exit For
                        Else
                            item.CantidadEntregada += (itemPhysicalCopy.Quantity - itemPhysical.QuantityDeliver)
                            itemPhysicalCopy.QuantityDeliver = (itemPhysicalCopy.Quantity - itemPhysical.QuantityDeliver)
                            outStandingQuantity -= (itemPhysicalCopy.Quantity - itemPhysical.QuantityDeliver)
                            itemPhysical.QuantityDeliver += (itemPhysicalCopy.Quantity - itemPhysical.QuantityDeliver)
                            listPhysicalInventory.Add(itemPhysicalCopy)
                        End If
                    Next

                    If listPhysicalInventory.Any() Then
                        item.ListPhysicalInventory = listPhysicalInventory
                        item.Action = 1
                        If item.ListPhysicalInventory.FindAll(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) <> INDTxtStore.Text.Split("-").ElementAt(0)).Count = 0 Then
                            item.CUMSource = System.Drawing.Color.Green.ToArgb()
                        Else
                            item.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                        End If
                    End If
                Next
            End Using

            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If
        End If

        INDGcDashboardPharmacyDetail.RefreshDataSource()
    End Sub

    Private Sub ClosePopupInfo(sender As Object, e As EventArgs)
        INDMeCUM.Focus()
    End Sub

#End Region

#Region "CRUD"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If BarraBotones.OperatingUnitValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado una unidad operativa"
            Exit Sub
        End If
        Dim listApplyProcedure = listDashboardPharmacyDetail.FindAll(Function(x) x.CantidadEntregada = 0 AndAlso x.Action IsNot Nothing AndAlso x.Action = 3)
        If listApplyProcedure.Count > 0 Then
            If MessageIndigo.Show("Se encontraron items que se aplicaran a un procedimiento sin cantidad a entregar y no se tendran en cuenta para la dispensación, desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If Me._settingInventory Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se han cargado los parámetros de inventario"
            Exit Sub
        End If

        Dim PharmaceuticalDispensing As New PharmaceuticalDispensing
        Dim errors As New StringBuilder
        Dim listDeliver = listDashboardPharmacyDetail.FindAll(Function(x) x.CantidadEntregada > 0)
        Dim listPharmaceuticalDispensing As List(Of PharmaceuticalDispensing) = Nothing
        If listDeliver.Count > 0 Then
            INDMeCUM.Enabled = False

            If DispensingIntegration Then
                BarraBotones.StatusRecord = "1"
            End If

            If _errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = _errors.ToString()
                Exit Sub
            End If

            'Validación de entraga total a productos en paquetes de enfermería
            If Me._settingInventory.PackageDispensingMethod Then
                Dim namePackingNursing As New StringBuilder
                Dim listPackingNursing = listDashboardPharmacyDetail.Where(Function(m) m.CantidadSolicitada <> m.CantidadEntregada).Select(Function(f) f.ServicioIPS).Distinct.ToList()
                If listPackingNursing?.Any() Then
                    For Each _namePackingNursing In listPackingNursing
                        namePackingNursing.AppendLine($"{_namePackingNursing.Remove(0, 23)}, ")
                    Next
                End If
                If namePackingNursing.Length > 0 Then
                    errors.Append(String.Format("Se deben dispensar la totalidad de los productos correspondientes al paquete de enfermería {0}", namePackingNursing.ToString()))
                End If
            End If

            AsyncLoader(True)
            listPharmaceuticalDispensing = New List(Of PharmaceuticalDispensing)
            Dim professional() As String
            Dim specialty() As String
            Dim thirdPartyProfessionalId As Integer

            Dim dictionaryProfessional As New Dictionary(Of String, Integer)
            Dim listProductsId As New List(Of Integer)

            Dim listPhysical = (From e In listDeliver
                                Where e.ListPhysicalInventory IsNot Nothing Select e.ListPhysicalInventory).ToList
            For Each item In listPhysical
                listProductsId.AddRange((From e In item Select e.ProductId).ToList())
            Next

            Dim listProductRateDetail As List(Of ProductRateDetail)
            Using model As New MProductRate(Me.Tag)
                listProductRateDetail = Await model.GetListProductRateDetailByCareGroupIdProductIdServiceDate(_careGroup.Id, listProductsId, GetDateServer())
            End Using

            'se obtiene cuantos documentos se van a crear dependiendo del orden que se dio en la columna suministro unico
            Dim listGroupByPattientSupply = (From e In listDeliver Select e.PattientSupplyOrder).Distinct().ToList()

            For i As Integer = 0 To listGroupByPattientSupply.Count - 1 Step 1

                PharmaceuticalDispensing = New PharmaceuticalDispensing
                With PharmaceuticalDispensing
                    .CodeNameWarehouse = Me.Store.Split(" - ")(0).Trim()
                    .OperatingUnitId = BarraBotones.OperatingUnitValue
                    .AdmissionNumber = _admissionNumber
                    .DocumentDate = GetDateServer()
                    .Status = 1
                    .PantientName = _patientName
                    .PantientFirstName = _patientFirstName
                    .PantientMiddleName = _patientMiddleName
                    .PantientLastName = _patientLastName
                    .PantientSecondLastName = _patientSecondLastName
                End With

                For Each item In listDeliver.FindAll(Function(x) x.PattientSupplyOrder = listGroupByPattientSupply.ElementAt(i) And x.CantidadEntregada > 0)
                    professional = item.Medico.Split("-")
                    specialty = item.Especialidad.Split("-")

                    If DispensingIntegration = False Then
                        If dictionaryProfessional.ContainsKey(item.NitMedico.Trim().TrimStart("0")) Then
                            thirdPartyProfessionalId = dictionaryProfessional(item.NitMedico.Trim().TrimStart("0"))
                        Else
                            Using model As New MDashBoardPharmacy(Me.Tag)
                                Dim third = model.GetCommonThirdPartyXpo(item.NitMedico.Trim().TrimStart("0"))
                                If third Is Nothing Then
                                    'si el medico no esta creado como tercero en la BD no continua el proceso
                                    errors.AppendLine(String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), item.Medico))
                                    Continue For
                                End If
                                dictionaryProfessional.Add(item.NitMedico.Trim().TrimStart("0"), third.Id)
                                thirdPartyProfessionalId = third.Id
                            End Using
                        End If
                    End If

                    'estos campos se asignana para poder generar el kardex de crystal
                    If PharmaceuticalDispensing.CodePatient = String.Empty Then
                        With PharmaceuticalDispensing
                            .CodePatient = item.PattientCode
                            .FunctionUnitName = If(DispensingIntegration, INDTxtFunctionalUnit.Text, INDTxtFunctionalUnit.Text.Split("-").ElementAt(1))
                            .CareCenterCode = item.CareCenterCode
                            .FunctionUnitCode = item.FunctionUnitCode
                            .ConsecutivePescription = item.ConsecutivePescription
                            .ConsecutiveInputs = If(item.ConsecutiveInputs IsNot String.Empty, CInt(item.ConsecutiveInputs), 0)
                            .ConsecutivePharmacy = If(item.ConsecutivePharmacy IsNot String.Empty, CInt(item.ConsecutivePharmacy), 0)
                            .ConsecutiveCrystal = item.Consecutivo
                            .HistoryType = _historyType
                            .OfficeType = _officeType
                            .LogisticOperator = _logisticOperator
                            .MedicalOrderRecipe = _medicalOrderRecipe
                        End With
                    End If

                    If item?.ListPhysicalInventory IsNot Nothing AndAlso item?.ListPhysicalInventory?.Any() Then
                        For Each itemPhysical In item?.ListPhysicalInventory
                            'creo el objeto del detalla del inventaio fisico
                            Dim PharmaceuticalDispensingDetailBatchSerial = New PharmaceuticalDispensingDetailBatchSerial
                            With PharmaceuticalDispensingDetailBatchSerial
                                .PhysicalInventoryId = itemPhysical.Id
                                .OutstandingQuantity = itemPhysical.QuantityDeliver
                                .Quantity = itemPhysical.QuantityDeliver
                            End With

                            Dim despensigDetailAdded = PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Where(Function(x) x.WarehouseId = itemPhysical.WarehouseId And x.ProductId = itemPhysical.ProductId And x.PackageId.Equals(item.PackageId)).FirstOrDefault()

                            If despensigDetailAdded IsNot Nothing Then
                                despensigDetailAdded.Quantity += itemPhysical.QuantityDeliver
                                despensigDetailAdded.GrandTotalSalesPrice = despensigDetailAdded.SalePrice * despensigDetailAdded.Quantity
                                despensigDetailAdded.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                            Else
                                Dim PharmaceuticalDispensingDetail As New PharmaceuticalDispensingDetail
                                With PharmaceuticalDispensingDetail
                                    .CareGroupId = _careGroup.Id
                                    .ProductId = itemPhysical.ProductId
                                    .WarehouseId = itemPhysical.WarehouseId
                                    .Quantity = itemPhysical.QuantityDeliver
                                    .ServiceDate = PharmaceuticalDispensing.DocumentDate
                                    .FunctionalUnitId = _functionalUnit.Id
                                    .CostCenterId = item.IdCostCenter
                                    .OrderedHealthProfessionalCode = professional(0).Trim()
                                    .OrderedProfessionalSpecialty = specialty(0).Trim()
                                    .OrderedHealthProfessionalThirdPartyId = thirdPartyProfessionalId
                                    .CodeProduct = item.CodProducto.Trim()
                                    .NameProduct = item.Producto.Substring(item.Producto.IndexOf(" - ") + 3)
                                    .CantidadPendiente = item.CantidadPendiente
                                    .CantidadSolicitada = item.CantidadSolicitada
                                    .ProductType = item.Tipo
                                    .HealthAdministratorId = healthAdministratorId
                                    .ThirdPartyId = thirdPartyId

                                    .idProductoHeon = 0
                                    .recetarioOMedica = ""
                                    .GuardaGastoQX = item.GuardaGastoQX
                                    .IdProgramacionQXPrincipal = item.IdProgramacionQXPrincipal
                                    .PackageId = item.PackageId

                                    If DispensingIntegration Then
                                        .idProductoHeon = item.IdProductHeon
                                        .recetarioOMedica = item.recetarioOMedica
                                    End If

                                    If item.Action = 3 Then 'aplicar a procedimiento
                                        .LiquidationType = 2
                                        .CupsEntityId = item.ApplyProcedureId
                                    Else 'entregar
                                        .LiquidationType = 1
                                    End If

                                    Dim _productRateDetail As ProductRateDetail = listProductRateDetail.Find(Function(x) x.ProductId = .ProductId)

                                    Dim ivaPercent As Decimal = 0
                                    'validamos que el producto no este exento de iva
                                    If ValidateIfTaxedProduct(_productRateDetail?.InventoryProduct) Then
                                        ivaPercent = _productRateDetail?.InventoryProduct?.GeneralLedgerIVA?.Percentage
                                    End If

                                    If _productRateDetail IsNot Nothing AndAlso _companySettings IsNot Nothing Then
                                        _roundingType = _companySettings.Currency?.RoundingType

                                        Dim dictionarySalesPrice = Utils.SetValueSalesPrice(_companySettings?.SalePriceIncludeTax,
                                                                                            _productRateDetail.GetSalePriceByRateType(), ivaPercent)

                                        .GrossValue = RoundValue(dictionarySalesPrice.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value, _roundingType)
                                        .TaxValue = RoundValue(dictionarySalesPrice.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value, _roundingType)
                                        .SalePrice = RoundValue(dictionarySalesPrice.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value, _roundingType)
                                        .TotalSalesPrice = .SalePrice
                                        .GrandTotalSalesPrice = RoundValue(.TotalSalesPrice * .Quantity, _roundingType)
                                    Else
                                        errors.AppendLine("No se encontro tarifa para el producto " + itemPhysical.CodeNameProduct)
                                        Continue For
                                    End If
                                End With
                                PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                                PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(PharmaceuticalDispensingDetail)
                            End If
                        Next
                    End If
                Next
                listPharmaceuticalDispensing.Add(PharmaceuticalDispensing)
            Next
        End If
        If errors.Length = 0 Then
            Try
                Using model As New MDashBoardPharmacy(MyTag)
                    Dim listAnnular = listDashboardPharmacyDetail.FindAll(Function(x) x.Action IsNot Nothing AndAlso x.Action = 2)
                    If listPharmaceuticalDispensing IsNot Nothing OrElse listAnnular.Count > 0 Then
                        Dim result = Await model.SaveDashboardPharmacySurgicalPackage(listPharmaceuticalDispensing, listAnnular, _idCurrentSequence)
                        INDMeCUM.Enabled = True
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Dim Message As String = String.Empty
                            If PharmaceuticalDispensing IsNot Nothing Then
                                PharmaceuticalDispensing.Code = result?.ObjectEmbbeded
                                If result?.MessageResult IsNot Nothing Then
                                    Message = result?.MessageResult.ElementAt(0)
                                Else
                                    Message = result?.Message
                                End If
                                PharmaceuticalDispensing.CodeNameUser = Message
                                PharmaceuticalDispensing.CreationUser = indigo.UserIndigo + " - " + indigo.UserIndigoName
                                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                            End If

                            Dim MessageReturn As String = Message

                            'Se ejecuta el proceso de integración con HEON despues de realizar satisfactoriamente la confirmación de dispensación de VIE
                            If DispensingIntegration Then
                                BarraBotones.StatusRecord = "2"
                                Args.JsonSolicitud = WebServiceObject.JsonSolicitud
                                Dim resultIntegration = Await model.PostConfirmIntegration(Args, PharmaceuticalDispensing)

                                MessageReturn = MessageReturn + vbCrLf + resultIntegration.Message

                                AsyncLoader(False)
                                RaiseEvent DashBoardPharmacySurgicalPackageSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs With {.PharmaceuticalDispensing = PharmaceuticalDispensing, .StatusTransactionVie = result.StateResult, .StatusTransactionHeon = resultIntegration.StateResult})
                                If resultIntegration.StateResult = False Then
                                    Mensaje(EeventViewerImages.Advertencia) = MessageReturn
                                Else
                                    Mensaje(EeventViewerImages.Informacion) = MessageReturn
                                End If

                                Me.Close()

                            Else 'Si no maneja integración con HEON
                                Mensaje(EeventViewerImages.Informacion) = MessageReturn
                                AsyncLoader(False)
                                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                                RaiseEvent DashBoardPharmacySurgicalPackageSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs With {.PharmaceuticalDispensing = PharmaceuticalDispensing})
                            End If
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result?.Message
                            AsyncLoader(False)
                            listPharmaceuticalDispensing = New List(Of PharmaceuticalDispensing)
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron items para dispensar"
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDMeCUM.Enabled = True
                Throw ex
            End Try
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            listPharmaceuticalDispensing = New List(Of PharmaceuticalDispensing)
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' funcion para valida si un producto es gravado
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateIfTaxedProduct(inventoryProduct As InventoryProduct) As Boolean
        Return inventoryProduct IsNot Nothing _
                AndAlso inventoryProduct?.TaxedProduct _
                AndAlso inventoryProduct?.LiquidateSalesTaxes _
                AndAlso inventoryProduct?.GeneralLedgerIVA IsNot Nothing
    End Function


    ''' <summary>
    ''' Redondea el valor por el tipo de redondeo
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="RoundingType"></param>
    ''' <returns></returns>
    Function RoundValue(value As Decimal, RoundingType As Integer) As Decimal
        Return Utils.RoundValueByTypeCurrency(value, RoundingType)
    End Function

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = "", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = "Indigo VIE", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = "HEON", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        listDashboardPharmacyDetail = Nothing
        listPharmaceuticalDispensingDetail = Nothing
        listPharmaceuticalDispensing = Nothing
        applyProcedureId = Nothing
        codeNameApplyProcedure = Nothing
        _presenter = Nothing
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        listPhysicalInventoryTmp = Nothing
        dictionaryPhysicalInventoryBarCode = Nothing
        dictionaryProduct = Nothing
        dictionaryAtcCode = Nothing
        dictionarySuppliedCode = Nothing
        _settingInventory = Nothing
        _Procedimiento = Nothing
    End Sub

    Private Async Sub FrmPopUpDashBoardPharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        BarraBotones.StatusRecordVisible = True
        IndigoGridView1.MoreInfoColunmns(INDGvDashboardPharmacyDetail)
        INDColSource.SortOrder = DevExpress.Data.ColumnSortOrder.Descending
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Deliver)
        ListActions.Add(eAcciones.Annular)
        ListActions.Add(eAcciones.ApplyProcedure)
        IndigoGridView1.SetListAcction(INDGvDashboardPharmacyDetail, ListActions)
        INDGvDashboardPharmacyDetail.Columns.ColumnByName("colActions").Caption = "Opciones"
        INDGvDashboardPharmacyDetail.Columns.ColumnByName("colActions").Width = 150
        _presenter = New PDashBoardPharmacyDetail(Me)

        Try
            AsyncLoader(True)
            Await LoadParameters()
            _presenter.GetSequence()
            Me._currentDate = GetDateServer()
            Execute()
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
        End Try

        AddHandler ctrTmp.ClosePopupInfo, AddressOf ClosePopupInfo
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPopUpDashBoardPharmacy_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    End Sub

#End Region


#Region "FormClosing"

    Private Sub FrmPopUpDashBoardPharmacySurgycalPackage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = DialogResult.OK Then
            Exit Sub
        End If

        Dim listApplyProcedure = listDashboardPharmacyDetail.FindAll(Function(x) x.CantidadEntregada = 0 AndAlso x.Action IsNot Nothing AndAlso x.Action = 3)

        If listApplyProcedure.Count > 0 Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If INDGvDashboardPharmacyDetail.GetSelectedRows().Count = 1 Then
            Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashBoardPharmacy_SurgicalPackageDeatils)
            Select Case sender.Tag
                Case eAcciones.Deliver.ToString()
                    rowDetail.Action = 1
                Case eAcciones.Annular.ToString()
                    '''se ejecuta el popup de la razon de anulaciion
                    Dim listDashboardPharmacyDetail As New List(Of ViewDashboardPharmacyDetail)
                    Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()
                    If ResultAnnulmentReason?.StateResult Then
                        rowDetail.Action = 2
                        rowDetail.ListPhysicalInventory = Nothing
                        rowDetail.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                        rowDetail.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                    End If
                Case eAcciones.ApplyProcedure.ToString()
                    Using Form As New PopupApplyProcedure
                        Form.StartPosition = FormStartPosition.CenterParent
                        Form.Admission = _admissionNumber
                        AddHandler Form.SelectProcedure, AddressOf ReturnSelectProcedure
                        Dim trasparent As New FrmTransparent(Form, False)
                        trasparent.ShowDialog()
                    End Using
                    If applyProcedureId IsNot Nothing Then
                        rowDetail.Action = 3
                        rowDetail.ApplyProcedureId = applyProcedureId
                        rowDetail.CodeNameApplyProcedure = codeNameApplyProcedure
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se selecciono ningun procedimiento"
                        rowDetail.Action = Nothing
                        rowDetail.ApplyProcedureId = Nothing
                        rowDetail.CodeNameApplyProcedure = String.Empty
                    End If
                    rowDetail.ListPhysicalInventory = Nothing
            End Select
        Else
            Select Case sender.Tag
                Case eAcciones.Deliver.ToString()
                    For Each index In INDGvDashboardPharmacyDetail.GetSelectedRows()
                        If index < 0 Then
                            Continue For
                        End If
                        listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).Action = 1
                    Next
                Case eAcciones.Annular.ToString()
                    For Each index In INDGvDashboardPharmacyDetail.GetSelectedRows()
                        If index < 0 Then
                            Continue For
                        End If
                        listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).Action = 2
                        listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).ListPhysicalInventory = Nothing
                    Next
                Case eAcciones.ApplyProcedure.ToString()
                    Using Form As New PopupApplyProcedure
                        Form.StartPosition = FormStartPosition.CenterParent
                        Form.Admission = _admissionNumber
                        AddHandler Form.SelectProcedure, AddressOf ReturnSelectProcedure
                        Dim trasparent As New FrmTransparent(Form, False)
                        trasparent.ShowDialog()
                    End Using
                    If applyProcedureId IsNot Nothing Then
                        For Each index In INDGvDashboardPharmacyDetail.GetSelectedRows()
                            If index < 0 Then
                                Continue For
                            End If
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).Action = 3
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).ApplyProcedureId = applyProcedureId
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).CodeNameApplyProcedure = codeNameApplyProcedure
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).ListPhysicalInventory = Nothing
                        Next
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se selecciono ningun procedimiento"
                        For Each index In INDGvDashboardPharmacyDetail.GetSelectedRows()
                            If index < 0 Then
                                Continue For
                            End If
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).Action = Nothing
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).ApplyProcedureId = Nothing
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).CodeNameApplyProcedure = String.Empty
                            listDashboardPharmacyDetail.ElementAt(INDGvDashboardPharmacyDetail.GetDataSourceRowIndex(index)).ListPhysicalInventory = Nothing
                        Next
                    End If
            End Select
        End If
        INDGcDashboardPharmacyDetail.RefreshDataSource()
        INDMeCUM.Focus()
    End Sub

#End Region

#Region "Click"

    Private Sub INDRptBteCUM_Click(sender As Object, e As EventArgs) Handles INDRptBteCUM.Click
        If _careGroup Is Nothing Then
            _errors.AppendLine("El grupo de atención asociado al ingreso no existe en Indigo VIE")
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Form As New PopupCUM(Me._settingInventory, Me._currentDate, INDTxtStore.EditValue, DispensingIntegration)
            Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashBoardPharmacy_SurgicalPackageDeatils)
            Form.StartPosition = FormStartPosition.CenterParent
            Form.CareGroupId = _careGroup.Id
            Form.Product = rowDetail.Producto
            Form.QuantityDeliver = rowDetail.CantidadPendiente
            Form.ListPhysicalInventory = rowDetail.ListPhysicalInventory
            Form.ProductType = rowDetail.Tipo
            Form.ProductCode = rowDetail.CodProducto
            If DispensingIntegration Then
                Form.QuantityDeliver = rowDetail.CantidadSolicitada
                Form.QuantityAsignedByHEON = rowDetail.QuantityAsignedByHEON
                Form.solicitudDetalle = rowDetail.solicitudDetalle
            End If
            AddHandler Form.GetCUM, AddressOf ReturnGetCUM
            Dim trasparent As New FrmTransparent(Form, False)
            Me.Cursor = Cursors.Default
            trasparent.ShowDialog(Me)

            INDMeCUM.Focus()
        End Using
    End Sub

#End Region

#Region "GetActiveObjectInfo"

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If e.Info Is Nothing AndAlso e.SelectedControl Is INDGcDashboardPharmacyDetail Then
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(INDGcDashboardPharmacyDetail.FocusedView, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
            If info.InRowCell Then
                If info.Column.Name = "INDColSource" Then
                    Dim text As String
                    If view.GetRowCellDisplayText(info.RowHandle, info.Column) = "255, 255, 0" Then
                        text = "Productos Despachados de Diferentes Almacenes"
                    ElseIf view.GetRowCellDisplayText(info.RowHandle, info.Column) = "0, 128, 0" Then
                        text = "Productos Despachados del Almacen " + INDTxtStore.Text
                    Else
                        Exit Sub
                    End If

                    Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()
                    e.Info = New DevExpress.Utils.ToolTipControlInfo(cellKey, text)
                End If
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDMeCUM_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeCUM.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDMeCUM.EditValue Is Nothing Then
                Exit Sub
            End If

            Dim resultPhysical As ActionResult(Of List(Of PhysicalInventory)) = Nothing
            Dim productATC = INDMeCUM.EditValue.ToString.ToUpper().Trim().Split("*IND*")

            If dictionaryPhysicalInventoryBarCode.ContainsKey(productATC.ElementAt(0)) Then
                listPhysicalInventoryTmp = dictionaryPhysicalInventoryBarCode(productATC.ElementAt(0))
            Else
                Using model As New MDashBoardPharmacy(Me.Tag)
                    If productATC.Length > 1 Then
                        resultPhysical = model.GetPhysicalInventoryBarCode(productATC.ElementAt(0), productATC.ElementAt(2))
                    Else
                        resultPhysical = model.GetPhysicalInventoryBarCode(productATC.ElementAt(0), "")
                    End If
                End Using

                If resultPhysical.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultPhysical.Message
                    INDMeCUM.EditValue = Nothing
                    INDMeCUM.Focus()
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
                listPhysicalInventoryTmp = resultPhysical.ObjectEmbbeded
                dictionaryPhysicalInventoryBarCode.Add(productATC.ElementAt(0), resultPhysical.ObjectEmbbeded)
            End If

            Dim ATCProductCode As String
            Dim ATCTemp As ViewDashBoardPharmacy_SurgicalPackageDeatils = Nothing
            Dim product As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

            If dictionaryProduct.ContainsKey(listPhysicalInventoryTmp.ElementAt(0).ProductId) Then
                product = dictionaryProduct(listPhysicalInventoryTmp.ElementAt(0).ProductId)
            Else
                Using model As New MDashBoardPharmacy(MyTag)
                    product = model.GetInventoryProductXpo(listPhysicalInventoryTmp.ElementAt(0).ProductId)
                    dictionaryProduct.Add(listPhysicalInventoryTmp.ElementAt(0).ProductId, product)
                End Using
            End If

            If product.Code <> productATC(0) Then
                ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.CodProducto.Trim() = productATC(0))
            End If

            If ATCTemp Is Nothing Then
                If product.ATCId IsNot Nothing Then
                    Using model As New MDashBoardPharmacy(MyTag)

                        If dictionaryAtcCode.ContainsKey(product.ATCId.Id) Then
                            ATCProductCode = dictionaryAtcCode(product.ATCId.Id)
                        Else
                            ATCProductCode = model.GetATCXpo(product.ATCId.Id).Code.Trim()
                            dictionaryAtcCode.Add(product.ATCId.Id, ATCProductCode)
                        End If

                        ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.CodProducto.Trim() = ATCProductCode)
                    End Using
                ElseIf product.SupplieId IsNot Nothing Then
                    Using model As New MDashBoardPharmacy(MyTag)

                        If dictionarySuppliedCode.ContainsKey(product.SupplieId.Id) Then
                            ATCProductCode = dictionarySuppliedCode(product.SupplieId.Id)
                        Else
                            ATCProductCode = model.GetInventorySuppliedXpo(product.SupplieId.Id).Code.Trim()
                            dictionarySuppliedCode.Add(product.SupplieId.Id, ATCProductCode)
                        End If

                        ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.CodProducto.Trim() = ATCProductCode)
                    End Using
                Else
                    ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.CodProducto.Trim() = product.Code)
                End If
            End If

            If ATCTemp IsNot Nothing Then
                If ATCTemp.ListPhysicalInventory IsNot Nothing AndAlso ATCTemp.ListPhysicalInventory.Count > 0 Then
                    Dim currentPhysical = ATCTemp.ListPhysicalInventory.ElementAt(ATCTemp.ListPhysicalInventory.Count - 1)
                    Dim quantityDeleiver = ATCTemp.ListPhysicalInventory.Sum(Function(x) x.QuantityDeliver) + 1
                    If quantityDeleiver > ATCTemp.CantidadPendiente Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar mas cantidad del producto " + ATCTemp.Producto + " porque supera la cantidad solicitada"
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    If currentPhysical.QuantityDeliver < currentPhysical.Quantity Then
                        currentPhysical.QuantityDeliver += 1
                        ATCTemp.CantidadEntregada += 1
                    Else
                        Dim listPhysicalInventoryId = (From p In ATCTemp.ListPhysicalInventory Select p.Id).ToList()
                        listPhysicalInventoryTmp = (From p In listPhysicalInventoryTmp Where Not listPhysicalInventoryId.Contains(p.Id) Select p).ToList()
                        If listPhysicalInventoryTmp.Count = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontro mas inventario fisico para el producto " + ATCTemp.Producto
                            INDMeCUM.EditValue = Nothing
                            INDMeCUM.Focus()
                            e.SuppressKeyPress = True
                            Exit Sub
                        End If
                        ATCTemp.CantidadEntregada += 1
                        listPhysicalInventoryTmp.ElementAt(0).QuantityDeliver = 1
                        ATCTemp.ListPhysicalInventory.Add(listPhysicalInventoryTmp.ElementAt(0))
                        ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                    End If
                Else
                    ATCTemp.CantidadEntregada = 1
                    Dim physicalTmp = listPhysicalInventoryTmp.Find(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) = INDTxtStore.Text.Split("-").ElementAt(0))
                    If physicalTmp IsNot Nothing Then
                        physicalTmp.QuantityDeliver = 1
                        ATCTemp.ListPhysicalInventory = New List(Of PhysicalInventory)
                        ATCTemp.ListPhysicalInventory.Add(physicalTmp)
                        ATCTemp.Action = 1
                        ATCTemp.CUMSource = System.Drawing.Color.Green.ToArgb()
                    Else
                        listPhysicalInventoryTmp.ElementAt(0).QuantityDeliver = 1
                        ATCTemp.ListPhysicalInventory = New List(Of PhysicalInventory)
                        ATCTemp.ListPhysicalInventory.Add(listPhysicalInventoryTmp.ElementAt(0))
                        ATCTemp.Action = 1
                        ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                    End If

                End If
                INDGcDashboardPharmacyDetail.RefreshDataSource()
                INDGvDashboardPharmacyDetail.ClearSelection()

                ''obtengo el item que se le asigno la cantidad para que se posicione en el
                Dim listTmp = INDGvDashboardPharmacyDetail.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)()
                Dim item = listTmp.Find(Function(x) x.Producto.Split(" - ")(0) = ATCTemp.Producto.Split(" - ")(0))

                INDGvDashboardPharmacyDetail.FocusedRowHandle = listTmp.IndexOf(item)
                INDGvDashboardPharmacyDetail.SelectRow(INDGvDashboardPharmacyDetail.FocusedRowHandle)

                INDMeCUM.EditValue = Nothing
                INDMeCUM.Focus()
                e.SuppressKeyPress = True
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encontro el producto en el listado"
                INDMeCUM.EditValue = Nothing
                INDMeCUM.Focus()
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

#End Region
#Region "GridViewEvents"
    ''' <summary>
    ''' evento para establecer dos columnas en una fila, contruida en formato html
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvDashboardPharmacyDetail_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvDashboardPharmacyDetail.CustomUnboundColumnData
        Dim View As GridView = sender
        If e.Column.FieldName = "INDColProduct" AndAlso e.IsGetData Then
            Dim Resultado As String = String.Empty
            Resultado = $"{View.GetListSourceRowCellValue(e.ListSourceRowIndex, "CodProducto")}"
            e.Value = Resultado
        End If

    End Sub
#End Region


#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        LoadStatus()
        BarraBotones.StatusRecord = "0"
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        'If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '    listDashboardPharmacyDetail.ForEach(AddressOf SetValueViewAnnular)
        '    INDGcDashboardPharmacyDetail.RefreshDataSource()
        '    Guardar()
        'End If
        Anulate()
    End Sub

    'Funcion utilizada para anular los productos
    Private Function Anulate()
        'Informacion del popup de la razon de anulzaion
        Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()
        If ResultAnnulmentReason?.StateResult Then
            AsyncLoader(True)
            listDashboardPharmacyDetail.ForEach(Sub(item)
                                                    item.Action = 2
                                                    item.CantidadEntregada = 0
                                                    item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB
                                                    item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description
                                                End Sub)
            INDGcDashboardPharmacyDetail.RefreshDataSource()
            Guardar()
        End If
    End Function

    ''' <summary>
    ''' popup para determinar el motivo de la anulacion
    ''' </summary>
    Private Function ShowAnnulmentReasonsPopup() As ActionResult(Of Object)
        Using formulario As New FrmPopupObservations()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.41, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.5)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Text = ResourceManager.GetString("AnnularMessage")
            formulario.FilterType = 28
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = DialogResult.OK Then
                Dim AnnulmentReasons As Object = New ExpandoObject()
                AnnulmentReasons.IdHCMOANULB = formulario.IdHCMOANULB
                AnnulmentReasons.Description = formulario.Description
                Dim concatMessages As String
                Select Case typeFilter
                    Case 0
                    Case 1
                        concatMessages = "Se va a realizar la anulación de los medicamentos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If

                    Case 2
                        concatMessages = "Se va a realizar la anulación de los insumos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 3
                        concatMessages = "Se va a realizar la anulación de los Medicamentos tipo insumo filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case Else
                End Select
                Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = AnnulmentReasons}
            Else
                Return New ActionResult(Of Object) With {.StateResult = False}
            End If
        End Using
    End Function

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_EntregaManual() Handles BarraBotones.Click_EntregaManual
        Me.Cursor = ChangeCursorIndigo()
        AsyncLoader(True)
        ManualDelivery()
        AsyncLoader(False)
        Me.Cursor = Cursors.Default
        INDMeCUM.Focus()
    End Sub

#End Region

    Private Function GetDetail() As Task
        If DispensingIntegration = False Then 'Si no hay integración con Heon
            Return Task.Factory.StartNew(Sub()

                                             Using model As New MDashBoardPharmacy(Me.Tag)
                                                 listDashboardPharmacyDetail = New List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)
                                                 Dim collect As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashBoardPharmacy_SurgicalPackageDeatils)
                                                 If String.IsNullOrEmpty(typeFilter) Then
                                                     collect = model.ListDashBoardPharmacy_SurgicalPackageDetailsCollection(_consecutive, _patientCode, _admissionNumber)
                                                 Else
                                                     collect = model.ListDashBoardPharmacy_SurgicalPackageDetailsByTypeCollection(_consecutive, _patientCode, _admissionNumber, typeFilter)
                                                 End If
                                                 For Each item In collect
                                                     Dim detail As New Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils
                                                     With detail
                                                         .Ingreso = item.Ingreso
                                                         .Entidad = item.Entidad
                                                         .CodigoEntidad = item.CodigoEntidad
                                                         .CodigoContrato = item.CodigoContrato
                                                         .CodigoPlan = item.CodigoPlan
                                                         .Producto = item.Producto
                                                         .Tipo = item.Tipo
                                                         .CantidadSolicitada = If(item.CantidadPaqueteEnf > 0, item.CantidadPaqueteEnf, item.CantidadSolicitada)
                                                         .CantidadPaqueteEnf = item.CantidadPaqueteEnf
                                                         .CantidadEntregada = item.CantidadEntregada
                                                         .CantidadPendiente = If(item.CantidadPaqueteEnf > 0, item.CantidadPaqueteEnf, item.CantidadPendiente)
                                                         .Unico = item.Unico
                                                         .NOPOS = .NOPOS
                                                         .UnidadMedida = item.UnidadMedida
                                                         .Medico = item.Medico
                                                         .NitMedico = item.NitMedico
                                                         .FilaSeleccionada = item.FilaSeleccionada
                                                         .IDETIPHIS = item.IDETIPHIS
                                                         .NUMEFOLIO = item.NUMEFOLIO
                                                         .Opcion = item.Opcion
                                                         .OpcionAnulado = item.OpcionAnulado
                                                         .Procedimiento = item.Procedimiento
                                                         .MarcarOpcion = item.MarcarOpcion
                                                         .CodigoPaciente = item.CodigoPaciente
                                                         .Consecutivo = item.Consecutivo
                                                         .Estado = item.Estado
                                                         .Especialidad = item.Especialidad
                                                         .ServicioIPS = item.ServicioIPS
                                                         .GuardaGastoQX = True
                                                         .IdProgramacionQXPrincipal = item.IDAGEPROGQX
                                                         .PackageCodeName = item.PackageCodeName
                                                         .PackageId = item.PackageId
                                                         .CodProducto = item.CodProducto
                                                         .IdCostCenter = If(item.IdCostCenter Is Nothing, _costCenterId, item.IdCostCenter)
                                                     End With
                                                     listDashboardPharmacyDetail.Add(detail)
                                                 Next
                                                 ' listDashboardPharmacyDetail = model.ListDashboardPharmacyDetail(_consecutive, _patientCode, _admission)
                                                 listDashboardPharmacyDetail.ForEach(AddressOf setValuesView)
                                             End Using
                                         End Sub)
        End If
    End Function

    Public Async Sub Execute()
        dictionaryPhysicalInventoryBarCode = New Dictionary(Of String, List(Of PhysicalInventory))
        dictionaryProduct = New Dictionary(Of Integer, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        dictionaryAtcCode = New Dictionary(Of Integer, String)
        dictionarySuppliedCode = New Dictionary(Of Integer, String)
        INDGvDashboardPharmacyDetail.ShowLoadingPanel()
        ctrTmp.PopupContainerControl = INDPccMoreInfo
        ctrTmp.PrintInfo()

        'Se setea una nueva instancia de la varible para que cuando se cargue otro registro No persista informacion de las validacion del anterior ingreso
        _errors = New StringBuilder()

        GetAdmissionInformation()
        If _errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = _errors.ToString()
        End If

        Await GetDetail()
        INDGcDashboardPharmacyDetail.DataSource = listDashboardPharmacyDetail

        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim stay = model.GetStayType(_patientCode, _admissionNumber)
            If stay Is Nothing OrElse stay Is String.Empty Then
                INDTxtStayType.Text = "N/A"
            Else
                INDTxtStayType.Text = stay.Trim()
            End If
        End Using

        Using model As New MPatient(Me.Tag)
            Dim result = Await model.GetPacientByIdentification(_patientCode.Trim())
            Dim patient = result.ObjectEmbbeded
            If _careGroup IsNot Nothing Then
                INDTxtCareGroup.Text = _careGroup.CodeName
            Else
                INDTxtCareGroup.Text = String.Empty
            End If
            If patient.GENCONENTITY IsNot Nothing Then
                Using modelHealth As New Presentation.Contract.MVP.MHealthAdministrator(Me.Tag)
                    Dim healthTmp = modelHealth.GetHealthAdministratorByIdSimple(patient.GENCONENTITY).ObjectEmbbeded
                    If healthTmp IsNot Nothing AndAlso healthTmp.Id > 0 Then
                        INDTxtEntity.Text = healthTmp.Code + " - " + healthTmp.Name
                    Else
                        INDTxtEntity.Text = String.Empty
                    End If
                End Using
            Else
                INDTxtEntity.Text = String.Empty
            End If
            INDTxtBirthday.Text = Utils.AgeToString(patient.IPFECNACI)
            If patient.IPSEXOPAC = 1 Then
                INDTxtGenus.Text = "Masculino"
            Else
                INDTxtGenus.Text = "Femenino"
            End If
            INDTxtBloodGroup.Text = patient.IPGRUPSAN
            INDTxtAddress.Text = patient.IPDIRECCI
            INDTxtPhone.Text = patient.IPTELEFON
        End Using

        If BarraBotones.PermissionsForm.ContainsKey(75) Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = False
            ColCUM.OptionsColumn.AllowEdit = True
            ColCUM.OptionsColumn.AllowFocus = True
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = True
            ColCUM.OptionsColumn.AllowEdit = False
            ColCUM.OptionsColumn.AllowFocus = False
        End If
        If BarraBotones.PermissionsForm.ContainsKey(143) Then
            BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.AddGrid, "Solicitar")
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.AddGrid) = False
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
        INDGvDashboardPharmacyDetail.HideLoadingPanel()
        INDMeCUM.Focus()
    End Sub

    Private Sub GetAdmissionInformation()
        If DispensingIntegration = False Then
            Using model As New MDashBoardPharmacy(Me.Tag)
                _admission = model.GetAdmissionXpo(_admissionNumber)
                _functionalUnit = model.GetFunctionalUnitXpo(_functionalUnitCode)
                If _functionalUnit Is Nothing Then
                    _errors.AppendLine("La unidad funcional " + INDTxtFunctionalUnit.Text + " no se encuentra homologada")
                    Exit Sub
                End If
                _careGroup = model.GetContractCareGroupXpo(_admission.GENCAREGROUP)
                If _careGroup Is Nothing Then
                    _errors.AppendLine("El grupo de atención asociado al ingreso no existe en Indigo VIE")
                    Exit Sub
                End If
                If _careGroup.CareGroupType = 3 Then 'particulares consulto el tercero que esta como paciente
                    Dim patient = model.GetPatientXpo(_admission.IPCODPACI)
                    Dim thirdPatient = model.GetCommonThirdPartyXpo(patient.IPCODPACI)
                    If thirdPatient Is Nothing Then
                        _errors.AppendLine("El paciente no esta creado como tercero en Indigo VIE")
                        Exit Sub
                    End If
                    thirdPartyId = thirdPatient.Id
                Else
                    'consulto le entidad administradora del ingreso para obteenr el tercero que tiene asociado
                    Dim healthAdministrator As Infrastructure.Data.Xpo.ContractRepository.HealthAdministratorXpo = Nothing
                    If _admission.GENCONENTITY = 0 Then
                        _errors.AppendLine("El ingreso no tiene una entidad administradora de salud asociada")
                        Exit Sub
                    End If
                    healthAdministrator = model.GetHealthAdministratorXpo(_admission.GENCONENTITY)
                    If healthAdministrator Is Nothing Then
                        _errors.AppendLine("La entidad administradora de salud asociada al ingreso no existe en Indigo VIE")
                        Exit Sub
                    End If
                    healthAdministratorId = healthAdministrator.Id
                    thirdPartyId = healthAdministrator.ThirdPartyId.Id
                End If
            End Using
        Else
            'Asigno el nombre de la unidad operativa obtenida del servicio
            _functionalUnit = New Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit()
            _functionalUnit.Descripcion = INDTxtFunctionalUnit.Text.Trim()

            'De acuerdo con la tabla creada identifico el grupo de atencion correspondiente al centro de atencion indicado en el formulario anterior
            Using model As New MDashBoardPharmacy(Me.Tag)
                Dim careGroupByCareCenter = model.GetCareGroupByCareCenterXpo(_careCenter)
                If careGroupByCareCenter Is Nothing Then
                    _errors.AppendLine("No se encontró ningun grupo de atención asociado al centro de atención")
                    Exit Sub
                End If
                _careGroup = model.GetContractCareGroupXpo(careGroupByCareCenter.CareGroupId)
                If _careGroup Is Nothing Then
                    _errors.AppendLine("El grupo de atención asociado al ingreso no existe en Indigo VIE")
                    Exit Sub
                End If
            End Using
        End If
    End Sub

    Private Sub btnKardex_Click(sender As Object, e As EventArgs) Handles btnKardex.Click
        Using Kardex As New IndigoComponents.frmHCKardexProductos(_patientCode, _admissionNumber, _careCenter, _functionalUnitCode, IndigoComponents.frmHCKardexProductos.eTipoProducto.Medicamentos, IndigoComponents.frmHCKardexProductos.eTipoKardex.Todos)
           Kardex.ShowDialog()
           Kardex.Dispose()
        End Using
    End Sub

    ''' <summary>
    ''' popup de solicitudes del EHR
    ''' </summary>
    Private Sub btnMakeRequest_Click() Handles BarraBotones.ClickAdicionarRejilla
        Using RequestCrystalForm As New IndigoeHistorias.frmHCSolicitudMedicamentosInsumos(_patientCode, _admissionNumber, _careCenter, _functionalUnitCode, Nothing, IndigoeHistorias.frmHCSolicitudMedicamentosInsumos.eORIGEN.DashboardPacientesEnfermeria)
           RequestCrystalForm.ShowDialog(Me)
           RequestCrystalForm.Dispose()
        End Using
    End Sub
End Class