'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 24-02-2015
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Dynamic
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports DevExpress.Utils.Menu
#End Region

Public Class FrmPopUpDashBoardPharmacyRequest
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
    Public Event DashBoardPharmacyRequestSuccessEventArgs(sender As Object, e As DashBoardPharmacyEventArgs)

#End Region

#Region "BUILDER"

    ''' <summary>
    ''' Constructor del modal
    ''' </summary>
    ''' <param name="_dispensingIntegration">0 = No hay integración, 1 = Hay integración con Heon, 2 = Hay integración entre Medilaser y FarmaQx</param>
    Public Sub New(Optional _dispensingIntegration As Integer = 0)
        InitializeComponent()
        ctrTmp = New CtrMoreInfoDashboardPharmacy()
        ctrTmp.SetInfoFunction(AddressOf getInfoDashBoardPharmacy)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        INDGvDashboardPharmacyDetail.OptionsView.ShowAutoFilterRow = False
        DispensingIntegration = _dispensingIntegration

        INDcolDeferred.Visible = False
        INDcolGridDeferred.Visible = False
        'Si el tipo de dispensación viene desde la integración entre Medilaser y FarmaQx DispensingIntegration = 2, se muestra la columna de diferido
        If DispensingIntegration = 2 Then
            INDcolDeferred.Visible = True
            INDcolDeferred.VisibleIndex = 8
            INDcolGridDeferred.Visible = True
            INDcolGridDeferred.VisibleIndex = 9
        End If
    End Sub

#End Region

#Region "GLOBALS"

#Region "Enums"
    Public Enum eTypeProduct
        paqueteEnfermeria = 5
    End Enum
#End Region

    Private ctrTmp As CtrMoreInfoDashboardPharmacy

    Dim listDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetail)

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

    ''' <summary>
    ''' Lista de detalle del servicio del producto
    ''' </summary>
    Dim ListProductServiceDetail As List(Of ProductServiceDetail)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Dim listPhysicalInventoryCustodyTmp As List(Of PhysicalInventoryCustody)

    Dim dictionaryPhysicalInventoryBarCode As Dictionary(Of String, List(Of PhysicalInventory))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR 3410</remarks>
    Dim dictionaryPhysicalInventoryCustodyBarCode As Dictionary(Of String, List(Of PhysicalInventoryCustody))
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
    ''' Fecha de Ncimiento del paciente
    ''' </summary>
    Dim ThirdPartyPatienDate As DateTime

    ''' <summary>
    ''' Genero de l paciente
    ''' </summary>
    Dim GenderThirdParty As Byte

    ''' <summary>
    ''' diccionario para descontar cantidades en entrega manual
    ''' </summary>
    Dim dictionaryManualDelivery As Dictionary(Of Integer, Integer)

    ''' <summary>
    ''' contiene el Id del iva del producto que se esta dispensando
    ''' </summary>
    Dim IvaIdProduct As Decimal

    ''' <summary>
    ''' conteo de la advertencia para medicamentos de alto riesgo
    ''' </summary>
    Private WarningCount As Integer
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Fecha registrada en el documento
    ''' </summary>
    Public DocumentDate As DateTime

    Public LoadForm As Boolean

    ''' <summary>
    ''' Centro de atención que viene de la integración con Medilaser
    ''' </summary>
    Public CareCenterCodeIntegrationMedilaser As String

    ''' <summary>
    ''' Almacen que viene de la integración con Medilaser
    ''' </summary>
    Public WareHouseIdIntegrationMedilaser As Integer

    ''' <summary>
    ''' Grupo de atención que viene de la integración con Medilaser
    ''' </summary>
    Public CareGroupIdIntegrationMedilaser As String

    ''' <summary>
    ''' Autorización de facturación que viene de la integración con Medilaser
    ''' </summary>
    Public BillingAuthorizationIdIntegrationMedilaser As String

    ''' <summary>
    ''' Fecha que viene de la integración con Medilaser
    ''' </summary>
    Public DateIntegrationMedilaser As String

    ''' <summary>
    ''' No. fórmula médica que viene de la integración con Medilaser
    ''' </summary>
    Public NumberIntegrationMedilaser As String

    ''' <summary>
    ''' Listado que viene desde dispensación por paciente Medilaser
    ''' </summary>
    Public ListProductsByCODCONCEC As List(Of SP_ListHCPRESCRDByCODCONCEC_Result)

    ''' <summary>
    ''' Listado de medicamentos de las tablas propias de Vie
    ''' </summary>
    Public MedicalFormulaDetailXpo As List(Of MedicalFormulaDetailXpo)

    ''' <summary>
    ''' 0 = No hay integración, 1 = Hay integración con Heon, 2 = Hay integración entre Medilaser y FarmaQx
    ''' </summary>
    Private DispensingIntegration As Integer

    ''' <summary>
    ''' filtros para detalles que llegan desde dashboard farmacia
    ''' </summary>
    Public typeFilter As String

    Private _webServiceObject As WebServiceObject

    ''' <summary>
    ''' Obtiene o estabece la tarifa de producto
    ''' </summary>
    Private _productRateDetail As ProductRateDetail

    ''' <summary>
    ''' Obtiene o establece la configuracion de la compañia
    ''' </summary>
    Private _companySettings As CompanySettings


    ''' <summary>
    ''' Tipo de redondeo de la moneda parametrizada
    ''' </summary>
    Public _roundingType As Byte

    ''' <summary>
    ''' Obtiene o establece los iva 
    ''' </summary>
    Private _generalLedgerIva As New ActionResult(Of GeneralLedgerIVA)
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

    Private _AffectInventory As Boolean
    ''' <summary>
    ''' Retorna si se afecta o no inventario de acuerdo a si el almacen es virtual o NO
    ''' </summary>
    ''' <returns></returns>
    Public Property AffectInventory As Boolean
        Get
            Return _AffectInventory
        End Get
        Set(value As Boolean)
            _AffectInventory = value
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
    ''' Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
    ''' </summary>
    Private _IDHCORDPRON As Integer?
    Public WriteOnly Property IDHCORDPRON As Integer?
        Set(value As Integer?)
            _IDHCORDPRON = value
        End Set
    End Property

	''' <summary>
	''' consecutivo de la cabecera
	''' </summary>
	''' <remarks></remarks>
	Dim _consecutive As String

	Public WriteOnly Property Consecutive As String
		Set(value As String)
			_consecutive = value
		End Set
	End Property

	''' <summary>
	''' bodega
	''' </summary>
	''' <value></value>
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

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ViewDashboardPharmacyDetail)
        Get
            Dim selected = INDGvDashboardPharmacyDetail.GetSelectedRows()
            Dim focused = INDGvDashboardPharmacyDetail.GetFocusedObject(Of ViewDashboardPharmacyDetail)

            If Not selected.Any() AndAlso focused IsNot Nothing Then
                Return {focused}.ToList()
            ElseIf selected IsNot Nothing Then
                Return INDGvDashboardPharmacyDetail.GetSelectedRows() _
                   .Where(Function(m) Not INDGvDashboardPharmacyDetail.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDGvDashboardPharmacyDetail.GetRow(m), ViewDashboardPharmacyDetail)) _
                   .ToList()
            Else
                Return Nothing
            End If
        End Get
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
    ''' Variable de parametrizacion de permiso de ingreso manual de codigo de barras
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PermisoBarCode As Boolean
        Get
            Return If(_settingInventory Is Nothing, False, _settingInventory.NomanualentybarCode)
        End Get
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
            Dim warehouse = (From w In listWarehouse Where w.Code = warehouseCode Select w).FirstOrDefault()
            If warehouse IsNot Nothing Then
                INDTxtStore.EditValue = warehouse.Id
                If warehouse.VirtualStore = True Then
                    AffectInventory = False 'si es virtual, NO afecta inventario
                Else
                    AffectInventory = True ' Si No es virtual es porque es fisico y afecta inventario
                End If
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
        Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
        If rowDetail.Tipo = 4 Then
            rowDetail.ListPhysicalInventoryCustody = Nothing
            rowDetail.ListPhysicalInventoryCustody = e.ListPhysicalInventoryCustody
            If rowDetail.ListPhysicalInventoryCustody.Count > 0 Then
                rowDetail.CantidadEntregada = e.ListPhysicalInventoryCustody.Sum(Function(x) x.QuantityDeliver)
                rowDetail.Action = 1

                If rowDetail.ListPhysicalInventoryCustody.FindAll(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) <> INDTxtStore.Text.Split("-").ElementAt(0)).Count = 0 Then
                    rowDetail.CUMSource = System.Drawing.Color.Green.ToArgb()
                Else
                    rowDetail.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                End If
            Else
                rowDetail.CantidadEntregada = 0
                rowDetail.Action = Nothing
                rowDetail.CUMSource = System.Drawing.Color.Transparent.ToArgb()
            End If
        Else
            rowDetail.ListPhysicalInventory = Nothing
            rowDetail.ListPhysicalInventory = e.ListPhysicalInventory
            rowDetail.ListDeliveriesByPharmacyProductType = e.ListDeliveriesByPharmacyProductType
            If rowDetail.ListPhysicalInventory.Count > 0 Then
                Using formulario As New PopUpNotificationLASA
                    formulario.TopMost = True
                    AddHandler formulario.AcceptMessage, AddressOf PopUpNotificationLASA_Accept
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

                    formulario.Source = DashboardPharmacySource.CUM
                    formulario.ListDashboardPharmacyDetail = New List(Of ViewDashboardPharmacyDetail) From {rowDetail}
                    If formulario.LoadLASAMedications() Then
                        Dim transparent = New Base.FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End If
                End Using
            Else
                rowDetail.CantidadEntregada = 0
                rowDetail.Action = Nothing
                rowDetail.CUMSource = System.Drawing.Color.Transparent.ToArgb()
            End If
        End If
        'Si viene con integración con HEON y la cantidad solicitada está en cero, se asigna la que se ingreso por el usuario
        If DispensingIntegration = 1 AndAlso rowDetail.QuantityAsignedByHEON = 0 Then
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
                        Me.SaveAndConfirmObligatory()
                    Else
                        Using model As New MBlockRecordAndSequense(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Me.SaveAndConfirmObligatory()
                        Else
                            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else

                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Me.SaveAndConfirmObligatory()
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.SaveAndConfirmObligatory()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        End If
    End Sub

    ''' <summary>
    ''' si tiene permisos de guardar y confirmar, se muestra
    ''' </summary>
    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' metodo para establecer los valores iniciales a la vista
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Private Sub setValuesView(item As ViewDashboardPharmacyDetail)
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
    Private Sub SetValueViewAnnular(item As ViewDashboardPharmacyDetail)
        item.Action = 2
        item.CantidadEntregada = 0
    End Sub

    Private Sub ManualDeliveryPhysicalInventory(item As ViewDashboardPharmacyDetail, errors As StringBuilder)
        If _errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = _errors.ToString()
            Exit Sub
        End If

		Using model As New MDashBoardPharmacy(Me.Tag)

			If String.IsNullOrEmpty(item.CodProducto) Then
				item.CodProducto = item.Producto.Split(" - ").ElementAt(0).Trim()
			End If
			Dim listPhysicalInventoryCrystalProduct = model.ListPhysicalInventoryByATCNumberWithAdditionalInformation(item.CodProducto, item.TypeProduct, _careGroup.Id)
			If listPhysicalInventoryCrystalProduct.Count = 0 Then
				errors.AppendLine("No se encontro inventario para el producto " + item.CodProducto + "-" + item.Producto)
				Exit Sub
			End If

			'Se realiza el ordenamiento de acuerdo al almacen y los productos con la fecha de vencimiento mas próxima
			Dim warehouseId As Integer? = INDTxtStore.EditValue
			listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
				OrderBy(Function(d) If(warehouseId IsNot Nothing AndAlso warehouseId = d.WarehouseId, 0, 1)).
				ThenBy(Function(d) If(d.Covered, 0, 1)).
				ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()

			Dim listPhysicalInventory As New List(Of PhysicalInventory)
			Dim outStandingQuantity = If(item.AllowBillingWithoutAuthorization _
				AndAlso If(item.NOPOS, False), If(item?.CantidadAutorizada, 0), item.CantidadPendiente)

			For Each itemPhysical In listPhysicalInventoryCrystalProduct

				'se verifica si el manual esta agregado al diccionario si no descontar cantidades
				If Not dictionaryManualDelivery.Any(Function(x) x.Key = itemPhysical.Id) Then
					dictionaryManualDelivery.Add(itemPhysical.Id, itemPhysical.Quantity)
				Else
					Dim value = dictionaryManualDelivery.Where(Function(f) f.Key = itemPhysical.Id).Sum(Function(d) d.Value)
					If value = 0 Then
						Continue For
					End If
					itemPhysical.QuantityDeliver = itemPhysical.Quantity - value
					itemPhysical.Quantity = value
				End If

				If itemPhysical.Quantity >= outStandingQuantity Then
					item.CantidadEntregada += outStandingQuantity
					itemPhysical.QuantityDeliver = outStandingQuantity
					listPhysicalInventory.Add(itemPhysical)
					dictionaryManualDelivery.Item(itemPhysical.Id) -= outStandingQuantity
					Exit For
				Else
					item.CantidadEntregada += itemPhysical.Quantity
					itemPhysical.QuantityDeliver = itemPhysical.Quantity
					outStandingQuantity -= itemPhysical.Quantity
					dictionaryManualDelivery.Item(itemPhysical.Id) -= outStandingQuantity
					listPhysicalInventory.Add(itemPhysical)
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
		End Using
	End Sub

    Private Sub ManualDeliveryPhysicalInventoryCustody(item As ViewDashboardPharmacyDetail, errors As StringBuilder)
		Using model As New MDashBoardPharmacy(Me.Tag)
			If String.IsNullOrEmpty(item.CodProducto) Then
				item.CodProducto = item.Producto.Split(" - ").ElementAt(0).Trim()
			End If
			Dim listPhysicalInventoryCrystalProduct = model.ListPhysicalInventoryCustodyByATCNumber(item.CodProducto.Trim(), item.TypeProduct, item.Ingreso)
			If listPhysicalInventoryCrystalProduct.Count = 0 Then
				errors.AppendLine("No se encontro inventario para el producto " + item.Producto)
				'Continue For
				Exit Sub
			End If
			Dim listPhysicalInventory As New List(Of PhysicalInventoryCustody)

			Dim outStandingQuantity = item.CantidadPendiente
			For Each itemPhysical In listPhysicalInventoryCrystalProduct
				Dim itemPhysicalTmp = listPhysicalInventoryCrystalProduct.Find(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) = INDTxtStore.Text.Split("-").ElementAt(0))
				If itemPhysicalTmp IsNot Nothing Then
					If PharmacyDashboardFromRequestWarehouse Then
						Continue For
					End If

					If itemPhysicalTmp.Quantity >= outStandingQuantity Then
						item.CantidadEntregada += outStandingQuantity
						itemPhysicalTmp.QuantityDeliver = outStandingQuantity
						listPhysicalInventory.Add(itemPhysicalTmp)
						Exit For
					Else
						item.CantidadEntregada += itemPhysicalTmp.Quantity
						itemPhysicalTmp.QuantityDeliver = itemPhysicalTmp.Quantity
						outStandingQuantity -= itemPhysicalTmp.Quantity
						listPhysicalInventory.Add(itemPhysicalTmp)
					End If
				Else
					If itemPhysical.Quantity >= outStandingQuantity Then
						item.CantidadEntregada += outStandingQuantity
						itemPhysical.QuantityDeliver = outStandingQuantity
						listPhysicalInventory.Add(itemPhysical)
						Exit For
					Else
						item.CantidadEntregada += itemPhysical.Quantity
						itemPhysical.QuantityDeliver = itemPhysical.Quantity
						outStandingQuantity -= itemPhysical.Quantity
						listPhysicalInventory.Add(itemPhysical)
					End If
				End If
			Next
			item.ListPhysicalInventoryCustody = listPhysicalInventory
			item.Action = 1
			If item.ListPhysicalInventoryCustody.FindAll(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) <> INDTxtStore.Text.Split("-").ElementAt(0)).Count = 0 Then
				item.CUMSource = System.Drawing.Color.Green.ToArgb()
			Else
				item.CUMSource = System.Drawing.Color.Yellow.ToArgb()
			End If
		End Using
	End Sub

    ''' <summary>
    ''' metodo para cargar manualmente las cantidades que se van a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ManualDelivery()
        Using formulario As New PopUpNotificationLASA
            formulario.TopMost = True
            AddHandler formulario.AcceptMessage, AddressOf PopUpNotificationLASA_Accept
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

            formulario.Source = DashboardPharmacySource.ManualDelivery
            formulario.ListDashboardPharmacyDetail = listDashboardPharmacyDetail.FindAll(Function(x) x.Action Is Nothing AndAlso x.SENDTO = 1)
            If formulario.LoadLASAMedications() Then
                Dim transparent = New Base.FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Evento al aceptar dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpNotificationLASA_Accept(sender As Object, e As ResponseEventArgs)
        dictionaryManualDelivery = New Dictionary(Of Integer, Integer)
        If e.AcceptResponse Then
            If e.Source = DashboardPharmacySource.ManualDelivery Then
                Dim errors As New StringBuilder
                For Each item In listDashboardPharmacyDetail.FindAll(Function(x) x.Action Is Nothing AndAlso x.SENDTO = 1)
                    If item.Tipo = 4 Then
                        ManualDeliveryPhysicalInventoryCustody(item, errors)
                    Else
                        ManualDeliveryPhysicalInventory(item, errors)
                    End If
                Next

                INDGcDashboardPharmacyDetail.RefreshDataSource()
                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                End If
            ElseIf e.Source = DashboardPharmacySource.CUM Then
                Dim rowDetail = e.ListDashboardPharmacyDetail.FirstOrDefault
                rowDetail.CantidadEntregada = rowDetail.ListPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
                rowDetail.Action = 1

                If rowDetail.ListPhysicalInventory.FindAll(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) <> INDTxtStore.Text.Split("-").ElementAt(0)).Count = 0 Then
                    rowDetail.CUMSource = System.Drawing.Color.Green.ToArgb()
                Else
                    rowDetail.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                End If
            End If
        End If
    End Sub

    Private Sub ClosePopupInfo(sender As Object, e As EventArgs)
        INDMeCUM.Focus()
    End Sub

    ''' <summary>
    ''' modal para ingresar la descripción y la razon del enrutamiento
    ''' </summary>
    Private Async Sub ShowObservationsPopup()
        Using formulario As New FrmPopupObservations()
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 800, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 500)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = DialogResult.OK Then
                Dim Result = Await ChangedRouteToPharmacy(formulario.Description, formulario.IdHCMOANULB)
                If Result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Exit Sub
                End If
                INDGvDashboardPharmacyDetail.ShowLoadingPanel()
                Execute()
            Else
                Exit Sub
            End If
        End Using
    End Sub

    ''' <summary>
    ''' funcion para hacer el cambio de enrutamiento
    ''' </summary>
    ''' <param name="Description"></param>
    ''' <param name="IdHCMOANULB"></param>
    ''' <returns></returns>
    Private Async Function ChangedRouteToPharmacy(Description As String, IdHCMOANULB As String) As Task(Of ActionResult)
        Dim RoutingLog = New RoutingLog
        With RoutingLog
            .Description = Description
            .IDCODMOTANU = IdHCMOANULB
        End With
        Using Model As New MDashBoardPharmacy(Me.Tag)
            Dim result = Model.RouteToPharmacy(SelectedItems, RoutingLog)
            If result Is Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = "Error al hacer el enrutamiento"}
            End If
            Return Await result
        End Using
    End Function

    Private Function GetDetail() As Task
        If DispensingIntegration = 0 Then 'Si no hay integración con Heon
            listDashboardPharmacyDetail = New List(Of ViewDashboardPharmacyDetail)
            Return Task.Factory.StartNew(Sub()
                                             'listDashboardPharmacyDetail = New List(Of ViewDashboardPharmacyDetail)
                                             'Dim tasks As New List(Of Task)
                                             'tasks.Add(Task.Factory.StartNew(Sub()
                                             Using model As New MDashBoardPharmacy(Me.Tag)
                                                 Dim collect As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetail)
                                                 If String.IsNullOrEmpty(typeFilter) Then
                                                     collect = model.ListDashBoardPharmacyDetailCollection(_consecutive, _patientCode, _admissionNumber)
                                                 Else
                                                     collect = model.ListDashBoardPharmacyDetailByTypeFilter(_consecutive, _patientCode, _admissionNumber, typeFilter)
                                                 End If
                                                 For Each item In collect
                                                     Dim detail As Domain.Crystal.Entities.ViewDashboardPharmacyDetail
                                                     detail = New Domain.Crystal.Entities.ViewDashboardPharmacyDetail
                                                     With detail
                                                         .Ingreso = item.Ingreso
                                                         .Entidad = item.Entidad
                                                         .CodigoEntidad = item.CodigoEntidad
                                                         .CodigoContrato = item.CodigoContrato
                                                         .CodigoPlan = item.CodigoPlan
                                                         .Producto = item.Producto
                                                         .NombrePaqueteEnf = item.NombrePaqueteEnf
                                                         .Tipo = IIf(item.Custodia, "4", item.Tipo)
                                                         .TipoAnterior = item.TipoAnterior
                                                         .CantidadPaqueteEnf = item.CantidadPaqueteEnf
                                                         .CantidadSolicitada = item.CantidadSolicitada
                                                         .CantidadEntregada = item.CantidadEntregada
                                                         .CantidadPendiente = item.CantidadPendiente
                                                         .Unico = item.Unico
                                                         .NOPOS = item.NOPOS
                                                         .PBS = item.PBS
                                                         .UNIRS = item.UNIRS
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
                                                         .IDAGEPROGQX = item.IDAGEPROGQX
                                                         .EXTRAMURAL = item.Extramural
                                                         .Custodia = item.Custodia
                                                         .TypeProduct = item.Tipo
                                                         .ADMINISTRACION = item.ADMINISTRACION
                                                         .EntityId = item.EntityId
                                                         .EntityName = item.EntityName
                                                         .MedicamentCode = detail.MedicamentCode
                                                         .RoutedTo = item.RoutedTo
                                                         .SENDTO = item.SENDTO
                                                         .DosisPrescrita = item.DosisPrescrita
                                                         .CantidadPrescrita = item.CantidadPrescrita
                                                         .CodProducto = item.CodProducto
                                                         .CantidadAutorizada = item.CantidadAutorizada
                                                         .NumeroNoPBS = item.NumeroNoPBS
                                                         .ColorAlertaPBS = item.ColorAlertaPBS
                                                         .SaldoAutorizadoMipres = item.SaldoAutorizadoMipres
                                                         .CantidadDispensada = item.CantidadDispensada
                                                         .AllowBillingWithoutAuthorization = item.AllowBillingWithoutAuthorization
                                                         .Note = item.Note
                                                         .Observation = item.Observation
                                                         .TotalDose = item.TotalDose
                                                         .TotalDoseMeasurement = item.TotalDoseMeasurement
                                                     End With
                                                     listDashboardPharmacyDetail.Add(detail)
                                                 Next
                                                 listDashboardPharmacyDetail.ForEach(AddressOf setValuesView)
                                             End Using
                                         End Sub)
        Else 'Si hay integración entre Medilaser y FarmaQx
            Return Task.Factory.StartNew(Sub()
                                             Using model As New MDashBoardPharmacy(Me.Tag)
                                                 listDashboardPharmacyDetail = New List(Of ViewDashboardPharmacyDetail)
                                                 If ListProductsByCODCONCEC IsNot Nothing Then 'Si el resultado viene de los medicamentos de Medilaser
                                                     For Each item In ListProductsByCODCONCEC
                                                         Dim detail As New Domain.Crystal.Entities.ViewDashboardPharmacyDetail
                                                         With detail
                                                             .Ingreso = item.NUMINGRES
                                                             .Entidad = IIf(item.GENCONENTITY Is Nothing, "", item.GENCONENTITY.ToString())
                                                             .CodigoEntidad = .Entidad
                                                             .CodigoContrato = item.CODCONTRA
                                                             .CodigoPlan = item.CODPANATE
                                                             .Producto = item.CODPRODUC.Trim() + " - " + item.DESPRODUC.Trim()
                                                             .Tipo = "1"
                                                             .CantidadSolicitada = item.CANPEDPRO
                                                             .CantidadEntregada = 0
                                                             .CantidadPendiente = item.CANPEDPRO
                                                             .Unico = False
                                                             .NOPOS = item.NOPOSPROD
                                                             .UnidadMedida = item.CODUNIMED
                                                             .Medico = item.CODPROSAL.Trim() + " - " + item.NOMMEDICO.Trim()
                                                             .NitMedico = item.CODIGONITPROFSAL
                                                             .FilaSeleccionada = 0
                                                             .IDETIPHIS = item.IDETIPHIS
                                                             .NUMEFOLIO = item.NUMEFOLIO
                                                             .Opcion = "0"
                                                             .OpcionAnulado = "0"
                                                             .Procedimiento = ""
                                                             .MarcarOpcion = False
                                                             .CodigoPaciente = item.IPCODPACI.Trim()
                                                             .Consecutivo = 0
                                                             .Estado = 1
                                                             .Especialidad = item.CODESPEC1.Trim() + " - " + item.DESESPECI.Trim()
                                                             .ListDeferred = Nothing
                                                             .MedicalFormulaDetailId = 0
                                                             .TypeProduct = "1"
                                                             .MedicamentCode = item.CODPRODUC.Trim
                                                             .SENDTO = 1
                                                         End With
                                                         listDashboardPharmacyDetail.Add(detail)
                                                     Next
                                                 Else 'Si el resultado viene de las tablas propias de Vie
                                                     ListProductsByCODCONCEC = New List(Of SP_ListHCPRESCRDByCODCONCEC_Result)
                                                     For Each item In MedicalFormulaDetailXpo
                                                         Dim detail As New Domain.Crystal.Entities.ViewDashboardPharmacyDetail
                                                         With detail
                                                             .Ingreso = item.AdmissionNumber
                                                             .Entidad = item.EntityId
                                                             .CodigoEntidad = .Entidad
                                                             .CodigoContrato = item.ContractCode
                                                             .CodigoPlan = item.PlanCode
                                                             .Producto = item.ProductCode + " - " + item.ProductName
                                                             .Tipo = "1"
                                                             .CantidadSolicitada = item.RequestQuantity
                                                             .CantidadEntregada = 0
                                                             .CantidadPendiente = item.PendingQuantity
                                                             .Unico = False
                                                             .NOPOS = item.NoPos
                                                             .UnidadMedida = item.MeasurementUnitCode
                                                             .Medico = item.HealthProfessionalCode + " - " + item.HealthProfessionalName
                                                             .NitMedico = item.HealthProfessionalNit
                                                             .FilaSeleccionada = 0
                                                             .IDETIPHIS = item.IdeTipHis
                                                             .NUMEFOLIO = item.NumFolio
                                                             .Opcion = "0"
                                                             .OpcionAnulado = "0"
                                                             .Procedimiento = ""
                                                             .MarcarOpcion = False
                                                             .CodigoPaciente = item.PatientCode
                                                             .Consecutivo = 0
                                                             .Estado = 1
                                                             .Especialidad = item.SpecialtyCode + " - " + item.SpecialtyName
                                                             .MedicalFormulaDetailId = item.Id
                                                             .IsDeferred = item.IsDeferred
                                                             .TypeProduct = "1"
                                                             .MedicamentCode = item.ProductCode
                                                             .SENDTO = 1
                                                             'Si el producto que se va recorriendo de la formula tiene diferidos
                                                             If item.MedicalFormulaDetailDeferredXpo IsNot Nothing AndAlso item.MedicalFormulaDetailDeferredXpo.Count > 0 Then
                                                                 'Se realiza el listado de diferidos
                                                                 Dim listDefrred As New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred)
                                                                 For Each itemDeferred In item.MedicalFormulaDetailDeferredXpo
                                                                     Dim deferred As New Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred
                                                                     deferred.Id = itemDeferred.Id
                                                                     deferred.FirstDeliveryDate = itemDeferred.FirstDeliveryDate
                                                                     deferred.DeliveryQuantityDeferred = itemDeferred.DeliveryQuantityDeferred
                                                                     deferred.Periodicity = itemDeferred.Periodicity
                                                                     deferred.ProductCode = item.ProductCode
                                                                     deferred.Number = itemDeferred.Number
                                                                     deferred.DeliveryDate = itemDeferred.DeliveryDate
                                                                     deferred.DeliveryQuantity = itemDeferred.DeliveryQuantity
                                                                     deferred.PendingQuantity = itemDeferred.PendingQuantity
                                                                     listDefrred.Add(deferred)
                                                                 Next
                                                                 .ListDeferred = listDefrred

                                                                 Dim listTemp = (From x In listDefrred Where x.DeliveryDate <= DateTime.Now() Select x).ToList()
                                                                 '.CantidadSolicitada = listTemp.Sum(Function(x) x.DeliveryQuantity)
                                                                 .CantidadPendiente = listTemp.Sum(Function(x) x.PendingQuantity)
                                                             End If

                                                         End With
                                                         ListProductsByCODCONCEC.Add(GetInfo(item))
                                                         listDashboardPharmacyDetail.Add(detail)
                                                     Next
                                                 End If
                                                 listDashboardPharmacyDetail.ForEach(AddressOf setValuesView)
                                             End Using
                                         End Sub)
        End If
    End Function

    Private Function GetInfo(detail As MedicalFormulaDetailXpo) As SP_ListHCPRESCRDByCODCONCEC_Result
        Dim item As New SP_ListHCPRESCRDByCODCONCEC_Result
        With item
            .NUMINGRES = detail.AdmissionNumber
            .IPCODPACI = detail.PatientCode
            .GENCONENTITY = detail.EntityId
            .CODCONTRA = detail.ContractCode
            .CODPANATE = detail.PlanCode
            .CODPRODUC = detail.ProductCode
            .DESPRODUC = detail.ProductName
            .CANPEDPRO = detail.RequestQuantity
            .NOPOSPROD = detail.NoPos
            .CODUNIMED = detail.MeasurementUnitCode
            .CODPROSAL = detail.HealthProfessionalCode
            .NOMMEDICO = detail.HealthProfessionalName
            .CODIGONITPROFSAL = detail.HealthProfessionalNit
            .IDETIPHIS = detail.IdeTipHis
            .NUMEFOLIO = detail.NumFolio
            .CODESPEC1 = detail.SpecialtyCode
            .DESESPECI = detail.SpecialtyName
            .IsDeferred = detail.IsDeferred
            .IPSCode = detail.MedicalFormulaId.IPSCode
        End With
        Return item
    End Function

    Public Async Sub Execute()
        dictionaryPhysicalInventoryBarCode = New Dictionary(Of String, List(Of PhysicalInventory))
        dictionaryPhysicalInventoryCustodyBarCode = New Dictionary(Of String, List(Of PhysicalInventoryCustody))
        dictionaryProduct = New Dictionary(Of Integer, Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        dictionaryAtcCode = New Dictionary(Of Integer, String)
        dictionarySuppliedCode = New Dictionary(Of Integer, String)
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
            INDTxtAge.Text = Utils.AgeToString(patient.IPFECNACI)
            INDTxtBirthday.EditValue = patient.IPFECNACI
            If patient.IPSEXOPAC = 1 Then
                INDTxtGenus.Text = "Masculino"
            Else
                INDTxtGenus.Text = "Femenino"
            End If
            INDTxtBloodGroup.Text = patient.IPGRUPSAN
            INDTxtAddress.Text = patient.IPDIRECCI
            INDTxtPhone.Text = patient.IPTELEFON
        End Using

        GetNewSequence()
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
        If DispensingIntegration = 0 Then
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
                    ThirdPartyPatienDate = thirdPatient.PersonId.BirthDate
                    GenderThirdParty = thirdPatient.PersonId.Gender
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
                    GenderThirdParty = healthAdministrator.ThirdPartyId.Person.Gender
                    ThirdPartyPatienDate = healthAdministrator.ThirdPartyId.Person.BirthDate
                End If
            End Using
        Else
            'Asigno el nombre de la unidad operativa obtenida del servicio
            _functionalUnit = New Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit()
            _functionalUnit.Descripcion = INDTxtFunctionalUnit.Text.Trim()

            ''De acuerdo con la tabla creada identifico el grupo de atencion correspondiente al centro de atencion indicado en el formulario anterior
            'Using model As New MDashBoardPharmacy(Me.Tag)
            '    Dim careGroupByCareCenter = model.GetCareGroupByCareCenterXpo(_careCenter)
            '    If careGroupByCareCenter Is Nothing Then
            '        _errors.AppendLine("No se encontró ningun grupo de atención asociado al centro de atención")
            '        Exit Sub
            '    End If
            '    _careGroup = model.GetContractCareGroupXpo(careGroupByCareCenter.CareGroupId)
            '    If _careGroup Is Nothing Then
            '        _errors.AppendLine("El grupo de atención asociado al ingreso no existe en Indigo VIE")
            '        Exit Sub
            '    End If
            'End Using

            'Se obtiene el grupo de atención con el id enviado desde el form principal
            Using model As New MDashBoardPharmacy(Me.Tag)
                _careGroup = model.GetContractCareGroupXpo(CareGroupIdIntegrationMedilaser)
            End Using
        End If
    End Sub

    Private Sub INDTxtStore_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtStore.EditValueChanged
        If INDgvSearchWareHouse.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If INDTxtStore.EditValue IsNot Nothing Then
            Dim Virtual As Boolean = INDgvSearchWareHouse.GetFocusedRowCellValue("VirtualStore")
            If Virtual = True Then
                AffectInventory = False
            Else
                AffectInventory = True
            End If
        End If
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
        If listDashboardPharmacyDetail.Any(Function(x) x.SENDTO <> 1 AndAlso x.CantidadEntregada > 0) Then
            Dim StrProduct = String.Join(" , ", (From item In listDashboardPharmacyDetail Where item.SENDTO <> 1 Select item.Producto).ToList())
            Mensaje(EeventViewerImages.Advertencia) = $"No se pueden dispensar productos({StrProduct}) con enrutamiento pendiente o a central de mezclas, es necesario cambiarlo primero "
            Exit Sub
        End If
        Dim PharmaceuticalDispensing As New PharmaceuticalDispensing
        Dim errors As New StringBuilder
        Dim listDeliver = listDashboardPharmacyDetail.FindAll(Function(x) x.CantidadEntregada > 0 OrElse (x.ListDeferred IsNot Nothing AndAlso
                                                              x.ListDeferred.Count > 0 AndAlso x.ListDeferred.Any(Function(d) d.Id = 0)))
        Dim listPharmaceuticalDispensing As List(Of PharmaceuticalDispensing) = Nothing
        Dim TempListDeferred As List(Of ViewDashboardPharmacyDetailDeferred) = Nothing

        If listDeliver.Count > 0 Then
            INDMeCUM.Enabled = False
            If DispensingIntegration = 1 Then
                BarraBotones.StatusRecord = "1"
            End If

            If _errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = _errors.ToString()
                Exit Sub
            End If

            AsyncLoader(True)
            listPharmaceuticalDispensing = New List(Of PharmaceuticalDispensing)
            Dim professional() As String = {"", ""}
            Dim specialty() As String = {"", ""}
            Dim thirdPartyProfessionalId As Integer?
            Dim dictionaryProfessional As New Dictionary(Of String, Integer)
            Dim listProductsId As New List(Of Integer)

            Dim listPhysical = (From e In listDeliver.Where(Function(i) i.ListPhysicalInventory IsNot Nothing AndAlso i.ListPhysicalInventory.Count > 0) Select e.ListPhysicalInventory).ToList
            For Each item In listPhysical
                listProductsId.AddRange((From e In item Select e.ProductId).ToList())
            Next

            Dim listPhysicalCustody = (From e In listDeliver.Where(Function(i) i.ListPhysicalInventoryCustody IsNot Nothing AndAlso i.ListPhysicalInventoryCustody.Count > 0) Select e.ListPhysicalInventoryCustody).ToList
            For Each item In listPhysicalCustody
                listProductsId.AddRange((From e In item Select e.ProductId).ToList())
            Next

            'Se realiza el distinct al listado de ids porque pueden venir repetidos
            listProductsId = (From x In listProductsId Select x).Distinct().ToList()

            'Listado de tupla para validar si los productos seleccionados manejan cotización
            Dim listTuple As New List(Of Tuple(Of Integer, Integer, Date))
            listProductsId.ForEach(Sub(y) listTuple.Add(New Tuple(Of Integer, Integer, Date)(y, _careGroup.Id, GetDateServer())))

            'Esta variable se llena cuando se dispara el modal de cotizaciones y seleccionan items
            Dim listQuotationPharmaceuticalDispensingDetailXpo As List(Of QuotationPharmaceuticalDispensingDetailXpo) = Nothing

            'Se valida que los productos manejen cotización
            Dim presenterPharmaceuticalDispensingDetail As New PPharmaceuticalDispensingDetail()
            Dim messageQuoted = presenterPharmaceuticalDispensingDetail.GetProductsWithQuoted(listTuple)
            If Not String.IsNullOrEmpty(messageQuoted) Then
                If MessageIndigo.Show(messageQuoted, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    If RequestQuoteProducts Then
                        AsyncLoader(False)
                        Exit Sub
                    End If
                Else
                    Me.Cursor = ChangeCursorIndigo()
                    Using Formulario As New FrmImportQuotationPharmaceutical()
                        'AddHandler Formulario.ImportEvent, AddressOf ImportInfo
                        Formulario.PatientCode = _patientCode
                        Formulario.ListProductId = listProductsId
                        Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                        Formulario.ViewModeEditHold = True
                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Formulario.Width = 1054
                        Formulario.Height = 500
                        Dim frm As New FrmTransparent(Formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        frm.ShowDialog(Me)

                        If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        listQuotationPharmaceuticalDispensingDetailXpo = Formulario.ListQuotationPharmaceuticalDispensingDetailXpo
                    End Using
                End If
            End If

            Dim listProductRateDetail As List(Of ProductRateDetail)
            Dim Product As List(Of InventoryProduct)
            Using model As New MProductRate(Me.Tag)
                listProductRateDetail = Await model.GetListProductRateDetailByCareGroupIdProductIdServiceDate(_careGroup.Id, listProductsId, GetDateServer())
                Product = Await model.GetInventoryProduct(listProductsId)
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
                    .AffectInventory = AffectInventory
                    .IDHCORDPRON = _IDHCORDPRON
                End With
                'Validación de entraga total a productos en paquetes de enfermería
                If Me._settingInventory.PackageDispensingMethod Then
                    Dim namePackingNursing As New StringBuilder
                    Dim listPackingNursing = listDashboardPharmacyDetail.Where(Function(m) m.Tipo = eTypeProduct.paqueteEnfermeria AndAlso m.CantidadSolicitada <> m.CantidadEntregada).Select(Function(f) f.NombrePaqueteEnf).Distinct.ToList()
                    If listPackingNursing?.Any() Then
                        For Each _namePackingNursing In listPackingNursing
                            namePackingNursing.AppendLine($"{_namePackingNursing}, ")
                        Next
                    End If
                    If namePackingNursing.Length > 0 Then
                        errors.Append(String.Format("Se deben dispensar la totalidad de los productos correspondientes al paquete de enfermería {0}", namePackingNursing.ToString()))
                    End If
                End If

                For Each item In listDeliver.FindAll(Function(x) x.PattientSupplyOrder = listGroupByPattientSupply.ElementAt(i) And x.CantidadEntregada > 0)
                    If String.IsNullOrEmpty(item.Medico) Then
                        professional(0) = ""
                        professional(1) = ""
                    Else
                        professional = item.Medico.Split("-")
                    End If
                    If String.IsNullOrEmpty(item.Especialidad) Then
                        specialty(0) = ""
                        specialty(1) = ""
                    Else
                        specialty = item.Especialidad.Split("-")
                    End If
                    thirdPartyProfessionalId = Nothing

                    If {0, 2}.Contains(DispensingIntegration) And item.Tipo <> 4 Then 'medicamentos en custodia no tienen profesional
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
                            .FunctionUnitName = If(DispensingIntegration = 1, INDTxtFunctionalUnit.Text, INDTxtFunctionalUnit.Text.Split("-").ElementAt(1))
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

                    If item.ListPhysicalInventory IsNot Nothing Then

                        ' Agrupa por cada combinación distinta de producto y bodega (y nombre)
                        Dim gruposProductos = item.ListPhysicalInventory.
                        GroupBy(Function(x) New With {Key x.ProductId, Key x.WarehouseId, Key x.CodeNameProduct}).
                        ToList()

                        For Each grupo In gruposProductos
                            Dim PharmaceuticalDispensingDetail As New PharmaceuticalDispensingDetail

                            ' Por cada físico en el grupo, agrega el batch serial detail
                            For Each itemPhysical In grupo
                                Dim PharmaceuticalDispensingDetailBatchSerial = New PharmaceuticalDispensingDetailBatchSerial _
                                With {
                                        .PhysicalInventoryId = itemPhysical.Id,
                                        .OutstandingQuantity = itemPhysical.QuantityDeliver,
                                        .Quantity = itemPhysical.QuantityDeliver,
                                        .ProductId = itemPhysical.ProductId,
                                        .IdWarehouse = itemPhysical.WarehouseId,
                                        .BatchSerialId = itemPhysical.BatchSerialId,
                                        .UserIndigo = Me.indigo.UserIndigo,
                                        .CodeNameProduct = itemPhysical.CodeNameProduct
                                }
                                PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                            Next

                            ' Resto de la lógica igual, pero usando la información del grupo
                            Dim itemPhysicalGroup = grupo.Key

                            If String.IsNullOrEmpty(item.CodProducto) Then
                                item.CodProducto = item.Producto.Split(" - ").ElementAt(0).Trim()
                            End If

                            With PharmaceuticalDispensingDetail
                                If item.ListDeliveriesByPharmacyProductType?.Any() Then
                                    .ListDeliveriesByPharmacyProductType = item.ListDeliveriesByPharmacyProductType.Where(Function(x) x.ProductId = itemPhysicalGroup.ProductId).ToList()
                                End If

                                .CareGroupId = _careGroup.Id
                                .ProductId = itemPhysicalGroup.ProductId
                                .WarehouseId = itemPhysicalGroup.WarehouseId
                                .Quantity = grupo.Sum(Function(x) x.QuantityDeliver)
                                .ServiceDate = PharmaceuticalDispensing.DocumentDate
                                .FunctionalUnitId = _functionalUnit.Id
                                .OrderedHealthProfessionalCode = professional(0).Trim()
                                .OrderedProfessionalSpecialty = specialty(0).Trim()
                                .OrderedHealthProfessionalThirdPartyId = thirdPartyProfessionalId
                                .CodeProduct = itemPhysicalGroup.CodeNameProduct.Split(" - ").ElementAt(0).Trim()
                                .NameProduct = itemPhysicalGroup.CodeNameProduct.Substring(itemPhysicalGroup.CodeNameProduct.IndexOf(" - ") + 3)
                                .CantidadPendiente = item.CantidadPendiente
                                .CantidadSolicitada = item.CantidadSolicitada
                                .UnidadMedida = item.UnidadMedida
                                .ProductType = item.TypeProduct
                                .HealthAdministratorId = healthAdministratorId
                                .ThirdPartyId = thirdPartyId
                                .MedicamentCode = item.CodProducto.Trim()
                                .EntityId = item.EntityId
                                .EntityName = item.EntityName
                                .TypeProduct = item.TypeProduct
                                .idProductoHeon = 0
                                .recetarioOMedica = ""
                                .Note = item.Note
                                If item.IDAGEPROGQX > 0 Then
                                    .GuardaGastoQX = True
                                    .IdProgramacionQXPrincipal = item.IDAGEPROGQX
                                End If
                                If DispensingIntegration = 1 Then
                                    .idProductoHeon = item.IdProductHeon
                                    .recetarioOMedica = item.recetarioOMedica
                                End If
                                If item.Action = 3 Then
                                    .LiquidationType = 2
                                    .CupsEntityId = item.ApplyProcedureId
                                Else
                                    .LiquidationType = 1
                                End If
                                .Extramural = item.EXTRAMURAL = True


                                Using model As New MCompanySettings(Me.Tag)
                                    _companySettings = Await model.GetCompanySettings()
                                End Using
                                _roundingType = _companySettings.Currency?.RoundingType


                                'Se obtiene la dispensación que se realizó en la cotización para poder sacar el valor
                                Dim qpddXpo As QuotationPharmaceuticalDispensingDetailXpo = Nothing
                                Dim salesPrice As Decimal = 0

                                If listQuotationPharmaceuticalDispensingDetailXpo?.Any() Then
                                    qpddXpo = (From x In listQuotationPharmaceuticalDispensingDetailXpo Where x.ProductId.Id = .ProductId Select x).FirstOrDefault()
                                End If

                                Dim SalePriceRateType As Decimal = 0
                                Dim SalePriceCUPS As Decimal = 0

                                If qpddXpo IsNot Nothing Then
                                    salesPrice = RoundValue(qpddXpo.SalePrice, _roundingType)
                                    .QuotationPharmaceuticalDispensingDetailId = qpddXpo.Id
                                ElseIf Product.Any(Function(x) x.Id = itemPhysicalGroup.ProductId AndAlso x.ProductType.Class <> 5) Then
                                    _productRateDetail = New ProductRateDetail
                                    _productRateDetail = listProductRateDetail.Find(Function(x) x.ProductId = .ProductId)
                                    If _productRateDetail Is Nothing Then
                                        errors.AppendLine("No se encontro tarifa para el producto " + itemPhysicalGroup.CodeNameProduct)
                                        Continue For
                                    End If

                                    SalePriceRateType = RoundValue(_productRateDetail.GetSalePriceByRateType(), _roundingType)

                                    If {2, 3}.Contains(_productRateDetail.LiquidationType) Then
                                        Using model As New MProductRate(Me.Tag)

                                            Dim result = model.GetHomologationCups(_careGroup.Id, _productRateDetail.CupsId, _functionalUnit.Id, specialty(0), GetDateServer(), 0, 0, 0, _productRateDetail.ContractDescriptionId)
                                            If result.StateResult = False OrElse result.ObjectEmbbeded.Count > 1 Then
                                                salesPrice = 0
                                                If result.Message.Length > 0 Then
                                                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                                                Else
                                                    Mensaje(EeventViewerImages.Advertencia) = "El CUPS asociado al Producto tiene más de una Homologación"
                                                End If
                                                Exit Sub
                                            Else
                                                Dim listHomologation = result.ObjectEmbbeded
                                                Dim ListSerivceOrderDetail = GetServiceValue(_admissionNumber, _functionalUnit.BranchOfficeId.Codigo, listHomologation, _careGroup.Id, _functionalUnit.Id, specialty(0) _
                                                                        , GetDateServer(), GenderThirdParty, ThirdPartyPatienDate, professional(0), thirdPartyProfessionalId, _productRateDetail.ContractDescriptionId)
                                                If ListSerivceOrderDetail Is Nothing OrElse ListSerivceOrderDetail.Count = 0 Then
                                                    salesPrice = 0
                                                    Mensaje(EeventViewerImages.Advertencia) = "No se ha encontrado Tarifa para el CUPS asociado al producto"
                                                    Exit Sub
                                                End If

                                                SalePriceCUPS = RoundValue(ListSerivceOrderDetail(0).SubTotalSalesPrice, _roundingType)
                                            End If

                                            .ProductServiceDetail.Add(GenerateProductServiceDetail(CUPSId:=_productRateDetail.CupsId, ContractDescriptionsId:=_productRateDetail.ContractDescriptionId, ProductId:=Nothing, Price:=SalePriceCUPS, LiquidationType:=_productRateDetail.LiquidationType _
                                                                                                    , RateType:=0))
                                            If _productRateDetail.RateType <> 0 Then
                                                .ProductServiceDetail.Add(GenerateProductServiceDetail(CUPSId:=Nothing, ContractDescriptionsId:=Nothing, ProductId:=_productRateDetail.ProductId, Price:=SalePriceRateType, LiquidationType:=_productRateDetail.LiquidationType _
                                                                                                       , RateType:=_productRateDetail.RateType))
                                            End If
                                        End Using
                                    End If
                                End If
                                salesPrice = RoundValue(SalePriceRateType + SalePriceCUPS, _roundingType)
                                'se busca el producto de la lista de los lotes
                                Dim FindProduct = Product.Find(Function(x) x.Id = itemPhysicalGroup.ProductId)

                                IvaIdProduct = 0

                                'validamos que el producto no este exento de iva
                                If FindProduct IsNot Nothing Then
                                    If FindProduct.TaxedProduct AndAlso FindProduct.LiquidateSalesTaxes Then
                                        IvaIdProduct = FindProduct.PercentageIVA
                                    End If
                                End If

                                ''Se evalua dependiendo de si el iva esta incluido o no en el valor de los productos
                                If _companySettings.SalePriceIncludeTax Then
                                    ''Si el iva esta incluido en el precio, se obtiene primero se obtiene el valor original del producto sin iva y luego si se obtiene el valor del iva a partir del valor original
                                    .GrossValue = RoundValue(IIf(IvaIdProduct <> 0, salesPrice / (1 + (IvaIdProduct / 100)), salesPrice), _roundingType)
                                    .TaxValue = RoundValue(IIf(IvaIdProduct <> 0, .GrossValue * ((IvaIdProduct / 100)), 0), _roundingType)
                                    .SalePrice = salesPrice
                                    .TotalSalesPrice = salesPrice
                                    .GrandTotalSalesPrice = RoundValue(.TotalSalesPrice * .Quantity, _roundingType)
                                Else
                                    .TaxValue = RoundValue(IIf(IvaIdProduct <> 0, salesPrice * (IvaIdProduct / 100), 0), _roundingType)
                                    .GrossValue = salesPrice
                                    .SalePrice = RoundValue(.GrossValue + .TaxValue, _roundingType)
                                    .TotalSalesPrice = .SalePrice
                                    .GrandTotalSalesPrice = RoundValue(.TotalSalesPrice * .Quantity, _roundingType)
                                End If
                            End With

                            PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(PharmaceuticalDispensingDetail)
                        Next
                    End If

                    If item.ListPhysicalInventoryCustody IsNot Nothing Then
                        For Each itemPhysicalCustody In item.ListPhysicalInventoryCustody
                            PharmaceuticalDispensing.AdmissionNumber = itemPhysicalCustody.AdmissionNumber
                            'creo el objeto del detalla del inventaio fisico
                            Dim PharmaceuticalDispensingDetailBatchSerial = New PharmaceuticalDispensingDetailBatchSerial
                            With PharmaceuticalDispensingDetailBatchSerial
                                '.PhysicalInventoryId = itemPhysicalCustody.Id
                                .PhysicalInventoryCustodyId = itemPhysicalCustody.Id
                                .OutstandingQuantity = itemPhysicalCustody.QuantityDeliver
                                .Quantity = itemPhysicalCustody.QuantityDeliver
                            End With
                            Dim despensigDetailAdded = PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Where(Function(x) x.WarehouseId = itemPhysicalCustody.WarehouseId And x.ProductId = itemPhysicalCustody.ProductId AndAlso x.Custody).FirstOrDefault()
                            If despensigDetailAdded IsNot Nothing Then
                                despensigDetailAdded.Quantity += itemPhysicalCustody.QuantityDeliver
                                despensigDetailAdded.GrandTotalSalesPrice = 0
                                despensigDetailAdded.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                            Else
                                Dim PharmaceuticalDispensingDetail As New PharmaceuticalDispensingDetail
                                With PharmaceuticalDispensingDetail
                                    .Custody = True
                                    .CareGroupId = _careGroup.Id
                                    .ProductId = itemPhysicalCustody.ProductId
                                    .WarehouseId = itemPhysicalCustody.WarehouseId
                                    .Quantity = itemPhysicalCustody.QuantityDeliver
                                    .ServiceDate = PharmaceuticalDispensing.DocumentDate
                                    .FunctionalUnitId = _functionalUnit.Id
                                    .OrderedHealthProfessionalCode = professional(0).Trim()
                                    .OrderedProfessionalSpecialty = specialty(0).Trim()
                                    .OrderedHealthProfessionalThirdPartyId = thirdPartyProfessionalId
                                    .CodeProduct = item.CodProducto.Trim()
                                    .NameProduct = item.Producto.Substring(item.Producto.IndexOf(" - ") + 3)
                                    .CantidadPendiente = item.CantidadPendiente
                                    .CantidadSolicitada = item.CantidadSolicitada
                                    .UnidadMedida = item.UnidadMedida
                                    .ProductType = item.TypeProduct
                                    .HealthAdministratorId = healthAdministratorId
                                    .ThirdPartyId = thirdPartyId
                                    .MedicamentCode = item.CodProducto.Trim()
                                    .idProductoHeon = 0
                                    .recetarioOMedica = ""
                                    .Note = item.Note
                                    .EntityId = item.EntityId
                                    .EntityName = item.EntityName
                                    If item.IDAGEPROGQX > 0 Then
                                        .GuardaGastoQX = True
                                        .IdProgramacionQXPrincipal = item.IDAGEPROGQX
                                    End If


                                    If DispensingIntegration = 1 Then
                                        .idProductoHeon = item.IdProductHeon
                                        .recetarioOMedica = item.recetarioOMedica
                                    End If

                                    If item.Action = 3 Then 'aplicar a procedimiento
                                        .LiquidationType = 2
                                        .CupsEntityId = item.ApplyProcedureId
                                    Else 'entregar
                                        .LiquidationType = 1
                                    End If

                                    If item.EXTRAMURAL = True Then
                                        .Extramural = True
                                    Else
                                        .Extramural = False
                                    End If

                                    .SalePrice = 0
                                    .TotalSalesPrice = 0
                                    .GrandTotalSalesPrice = 0
                                    .TaxValue = 0
                                    .GrossValue = 0
                                    .TotalSalesPrice = 0
                                    .GrandTotalSalesPrice = 0
                                End With
                                PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(PharmaceuticalDispensingDetailBatchSerial)
                                PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(PharmaceuticalDispensingDetail)
                            End If
                        Next
                    End If

                    If item.ListDeferred IsNot Nothing Then
                        If TempListDeferred Is Nothing Then
                            TempListDeferred = New List(Of ViewDashboardPharmacyDetailDeferred)
                        End If
                        TempListDeferred.AddRange(item.ListDeferred)
                    End If

                Next
                For Each item In listDeliver.FindAll(Function(x) x.PattientSupplyOrder = listGroupByPattientSupply.ElementAt(i) And x.CantidadEntregada = 0)
                    If item.ListDeferred IsNot Nothing Then
                        If TempListDeferred Is Nothing Then
                            TempListDeferred = New List(Of ViewDashboardPharmacyDetailDeferred)
                        End If
                        TempListDeferred.AddRange(item.ListDeferred)
                    End If
                Next
                If PharmaceuticalDispensing.PharmaceuticalDispensingDetail IsNot Nothing AndAlso
                        PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count > 0 Then
                    listPharmaceuticalDispensing.Add(PharmaceuticalDispensing)
                End If
            Next
        End If

        If errors.Length = 0 Then
            Try
                Using model As New MDashBoardPharmacy(MyTag)
                    Dim listAnnular = listDashboardPharmacyDetail.FindAll(Function(x) x.Action IsNot Nothing AndAlso x.Action = 2)
                    If listPharmaceuticalDispensing IsNot Nothing OrElse listAnnular.Count > 0 OrElse (TempListDeferred IsNot Nothing AndAlso TempListDeferred.Count > 0) Then

                        'Si viene una dispensación normal o de heon
                        If DispensingIntegration = 0 OrElse DispensingIntegration = 1 Then
                            Dim atcCodes = listDashboardPharmacyDetail.Where(Function(m) m.CantidadEntregada > 0).Select(Function(m) m.CodProducto).ToList()
                            Dim warnings = Await model.GetHighRiskDrugsByAtcCodeAsync(atcCodes)

                            If warnings IsNot Nothing AndAlso warnings.Any() Then
                                Dim messages = warnings.GroupBy(Function(m) m.ATCId).Select(Function(m)
                                                                                                Dim atcCodeName = $"{m.FirstOrDefault().ATCCode} - {m.FirstOrDefault().ATCName}"
                                                                                                Dim riskAlerts = String.Join($"{vbCrLf}", m.Select(Function(o) $"* {o.RiskName}: {o.Alert}").ToArray())
                                                                                                Return $"El medicamento {atcCodeName} tiene los siguientes niveles de riesgo:{vbCrLf}{riskAlerts}"
                                                                                            End Function).ToArray()
                                WarningCount += 1
                                Dim concatMessages = String.Join(vbCrLf, messages)
                                'Cambiar los nombres de los botones
                                AsyncLoader(False)
                                If warnings.Any(Function(m) m.IsRestrictive) And WarningCount = 1 Then
                                    MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.Continuar, "", {"Regresar al formulario"})
                                    Return
                                ElseIf warnings.Any(Function(m) m.IsRestrictive) And WarningCount > 1 Then
                                    If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Aceptar", "Regresar al formulario"}) <> DialogResult.Yes Then
                                        Return
                                    End If
                                    WarningCount = 0
                                Else
                                    WarningCount = 0
                                    If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Aceptar", "Regresar al formulario"}) <> DialogResult.Yes Then
                                        Return
                                    End If
                                End If
                            End If

                            Dim result = Await model.SaveDashBoardPharmacy(listPharmaceuticalDispensing, listAnnular, _idCurrentSequence)
                            INDMeCUM.Enabled = True
                            If result.StatusCode = eStatusResult.SUCCESS Then
                                If PharmaceuticalDispensing IsNot Nothing Then
                                    PharmaceuticalDispensing.Code = result.ObjectEmbbeded
                                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                        PharmaceuticalDispensing.CodeNameUser = result.MessageResult.ElementAt(0)
                                    End If
                                    PharmaceuticalDispensing.CreationUser = indigo.UserIndigo + " - " + indigo.UserIndigoName
                                End If

                                Dim MessageReturn As String = result.Message.Trim()

                                'Se ejecuta el proceso de integración con HEON despues de realizar satisfactoriamente la confirmación de dispensación de VIE
                                If DispensingIntegration = 1 Then
                                    BarraBotones.StatusRecord = "2"
                                    Args.JsonSolicitud = WebServiceObject.JsonSolicitud
                                    Dim resultIntegration = Await model.PostConfirmIntegration(Args, PharmaceuticalDispensing)

                                    MessageReturn = MessageReturn + vbCrLf + resultIntegration.Message

                                    AsyncLoader(False)
                                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                                    RaiseEvent DashBoardPharmacyRequestSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs With {.PharmaceuticalDispensing = PharmaceuticalDispensing, .StatusTransactionVie = result.StateResult, .StatusTransactionHeon = resultIntegration.StateResult})
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
                                    RaiseEvent DashBoardPharmacyRequestSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs With {.PharmaceuticalDispensing = PharmaceuticalDispensing})
                                End If
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                AsyncLoader(False)
                                listPharmaceuticalDispensing = New List(Of PharmaceuticalDispensing)
                            End If

                        Else 'Si viene de la integración de medilaser y farmaqx

                            Dim result = Await model.SaveDispensingByPatientMedilaser(ListProductsByCODCONCEC, listPharmaceuticalDispensing, listAnnular, CareCenterCodeIntegrationMedilaser,
                                                                              WareHouseIdIntegrationMedilaser, CareGroupIdIntegrationMedilaser, BillingAuthorizationIdIntegrationMedilaser,
                                                                              DateIntegrationMedilaser, NumberIntegrationMedilaser, TempListDeferred, False)
                            INDMeCUM.Enabled = True
                            If result.StatusCode = eStatusResult.SUCCESS Then
                                Mensaje(EeventViewerImages.Informacion) = result.Message
                                AsyncLoader(False)
                                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                                RaiseEvent DashBoardPharmacyRequestSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs With {.SP_SaveDispensingByPatientMedilaser_Result = result.ObjectEmbbeded})
                                Me.Close()
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                AsyncLoader(False)
                                listPharmaceuticalDispensing = New List(Of PharmaceuticalDispensing)
                            End If
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
    ''' funcion para obtener el valor de un CUPS
    ''' </summary>
    ''' <param name="Admission"></param>
    ''' <param name="FunctionalUnitCenterAttentionCode"></param>
    ''' <param name="listHomologation"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="FuncionalUnitId"></param>
    ''' <param name="HealthProfessionalSpecialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="GenderThirdParty"></param>
    ''' <param name="_patientDate"></param>
    ''' <param name="ProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="ContractDescriptionId"></param>
    ''' <returns></returns>
    Private Function GetServiceValue(Admission As String, FunctionalUnitCenterAttentionCode As String, listHomologation As List(Of CupsHomologation), CareGroupId As Integer, FuncionalUnitId As Integer, HealthProfessionalSpecialty As String,
                                ServiceDate As DateTime, GenderThirdParty As Byte, _patientDate As DateTime, ProfessionalCode As String, ThirdPartyId As Integer, ContractDescriptionId As Integer?) As List(Of ServiceOrderDetail)
        Using model As New Billing.MVP.MServiceOrder(Me.Tag)
            Dim result = model.GetServiceValue(Admission, FunctionalUnitCenterAttentionCode, listHomologation, CareGroupId, FuncionalUnitId, HealthProfessionalSpecialty, ServiceDate, GenderThirdParty, _patientDate, 1, ProfessionalCode, ThirdPartyId, 0, ContractDescriptionId)
            If result Is Nothing OrElse result.StateResult = False Then
                If result.Message.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Return New List(Of ServiceOrderDetail)
            End If
            Return result.ObjectEmbbeded
        End Using
    End Function

    ''' <summary>
    ''' Crea el detalle para guardar en la tabla
    ''' </summary>
    ''' <param name="CUPSId"></param>
    ''' <param name="ContractDescriptionsId"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="Price"></param>
    Private Function GenerateProductServiceDetail(CUPSId As Integer?, ContractDescriptionsId As Integer?, ProductId As Integer?, Price As Decimal, LiquidationType As Byte, RateType As Byte) As ProductServiceDetail
        Dim ProductServiceDetail = New ProductServiceDetail

        With ProductServiceDetail
            .CUPSEntityId = CUPSId
            .ContractDescriptionsId = ContractDescriptionsId
            .ProductId = ProductId
            .Price = Price
            .LiquidationType = LiquidationType
            .RateType = RateType
            .DiscountPercentage = 0
        End With

        Return ProductServiceDetail
    End Function

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = "", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = "Indigo VIE", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = "HEON", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Redondea el valor por el tipo de redondeo
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="RoundingType"></param>
    ''' <returns></returns>
    Function RoundValue(value As Decimal, RoundingType As Integer) As Decimal
        Return Utils.RoundValueByTypeCurrency(value, RoundingType)
    End Function

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
        listPhysicalInventoryCustodyTmp = Nothing
        dictionaryPhysicalInventoryBarCode = Nothing
        dictionaryPhysicalInventoryCustodyBarCode = Nothing
        dictionaryProduct = Nothing
        dictionaryAtcCode = Nothing
        dictionarySuppliedCode = Nothing
        _settingInventory = Nothing
        ListProductServiceDetail = Nothing
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
        ListActions.Add(eAcciones.RouteToPharmacy)
        IndigoGridView1.SetListAcction(INDGvDashboardPharmacyDetail, ListActions)
        INDGvDashboardPharmacyDetail.Columns.ColumnByName("colActions").Caption = "Opciones"
        INDGvDashboardPharmacyDetail.Columns.ColumnByName("colActions").Width = 80
        _presenter = New PDashBoardPharmacyDetail(Me)
        INDGvDashboardPharmacyDetail.ShowLoadingPanel()
        Try
            AsyncLoader(True)
            Await LoadParameters()
            _presenter.GetSequence()
            Me._currentDate = GetDateServer()
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
        End Try

        AddHandler ctrTmp.ClosePopupInfo, AddressOf ClosePopupInfo
        LoadForm = True
        Execute()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPopUpDashBoardPharmacy_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Execute()
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmPopUpDashBoardPharmacyRequest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = DialogResult.OK Then
            Exit Sub
        End If

        Dim listDeliver = listDashboardPharmacyDetail.FindAll(Function(x) x.CantidadEntregada > 0 OrElse (x.ListDeferred IsNot Nothing AndAlso
                                                              x.ListDeferred.Count > 0 AndAlso x.ListDeferred.Any(Function(d) d.Id = 0)))
        If listDeliver.Count > 0 Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Async Sub IndigoGridView1_Click_ButtonActionAsync(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If INDGvDashboardPharmacyDetail.GetSelectedRows().Count = 1 Then
            Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
            Select Case sender.Tag
                Case eAcciones.Deliver.ToString()
                    rowDetail.Action = 1
                Case eAcciones.Annular.ToString()

                    rowDetail.Action = 2
                    rowDetail.ListPhysicalInventory = Nothing

                    If DispensingIntegration = 0 Then
                        rowDetail.CantidadPendiente = 0
                        '''se ejecuta el popup de la razon de anulaciion
                        Dim listDashboardPharmacyDetail As New List(Of ViewDashboardPharmacyDetail)
                        Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

                        If ResultAnnulmentReason?.StateResult Then
                            AsyncLoader(True)

                            ''se obtiene informacion de la fila seleccionada
                            Dim rowData = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)

                            Using model As New MDashBoardPharmacy(Me.Tag)

                                '''creo el objeto con la informacion necesaria para su anulacion individual
                                listDashboardPharmacyDetail.Add(New Domain.Crystal.Entities.ViewDashboardPharmacyDetail With
                                        {
                                            .Ingreso = _admissionNumber,
                                            .CodigoPaciente = _patientCode,
                                            .CareCenterCode = _careCenter,
                                            .FunctionUnitCode = _functionalUnitCode,
                                            .ConsecutivePescription = _consecutivePescription,
                                            .ConsecutiveInputs = _consecutiveInputs,
                                            .ConsecutivePharmacy = rowData.ConsecutivePharmacy,
                                            .Consecutivo = rowData.Consecutivo,
                                            .HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.IdHCMOANULB,
                                            .Description = ResultAnnulmentReason?.ObjectEmbbeded?.Description,
                                            .Producto = rowData.Producto,
                                            .AnulateOnly = 1,
                                            .Medico = rowData.NitMedico,
                                            .CodProducto = rowData.CodProducto
                                        })

                                Dim result = Await model.SaveDashBoardPharmacy(Nothing, listDashboardPharmacyDetail, 0)
                                If result.StateResult Then
                                    Mensaje(EeventViewerImages.Informacion) = result.Message.Trim()
                                    '''evento para recargar los datos despues de la anulacion
                                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                                    RaiseEvent DashBoardPharmacyRequestSuccessEventArgs(Nothing, New DashBoardPharmacyEventArgs)
                                Else
                                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                                End If
                            End Using

                            AsyncLoader(False)
                        End If
                    End If


                Case eAcciones.ApplyProcedure.ToString()
                    Using Form As New PopupApplyProcedure
                        Form.StartPosition = FormStartPosition.CenterParent
                        Form.Admission = _admissionNumber
                        AddHandler Form.SelectProcedure, AddressOf ReturnSelectProcedure
                        Dim trasparent As New FrmTransparent(Form, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        trasparent.ShowDialog(Me)
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
                    rowDetail.ListPhysicalInventoryCustody = Nothing
                Case eAcciones.RouteToPharmacy.ToString()
                    ShowObservationsPopup()
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

                Case eAcciones.RouteToPharmacy.ToString()
                    ShowObservationsPopup()
            End Select
        End If

        INDGcDashboardPharmacyDetail.RefreshDataSource()
        INDMeCUM.Focus()
    End Sub

#End Region

#Region "PopUpMenuShowing"
    Private Sub INDGvDashboardPharmacyDetail_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvDashboardPharmacyDetail.PopupMenuShowing
        Dim ButtonRouteToPharmacy = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.RouteToPharmacy)))
        If Me.SelectedItems.Any(Function(x) {0, 2}.Contains(x.SENDTO)) Then
            IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
            If ButtonRouteToPharmacy IsNot Nothing Then ButtonRouteToPharmacy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            If ButtonRouteToPharmacy IsNot Nothing Then ButtonRouteToPharmacy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub
#End Region

#Region "QueryPopUpActionButtons"
    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        Dim ButtonRouteToPharmacy = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.RouteToPharmacy)))
        Dim count As Integer = 0

        If Me.SelectedItems.Any(Function(x) {0, 2}.Contains(x.SENDTO)) Then
            e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)
            If ButtonRouteToPharmacy IsNot Nothing Then ButtonRouteToPharmacy.Visible = True
            count = 1
        Else
            If ButtonRouteToPharmacy IsNot Nothing Then ButtonRouteToPharmacy.Visible = False
            count = 3
        End If
        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub
#End Region

#Region "Click"

    Private Sub INDRptBteCUM_Click(sender As Object, e As EventArgs) Handles INDRptBteCUM.Click
        If _careGroup Is Nothing Then
            _errors.AppendLine("El grupo de atención asociado al ingreso no existe en Indigo VIE")
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
        If {0, 2}.Contains(rowDetail.SENDTO) Then
            Mensaje(EeventViewerImages.Advertencia) = $"El medicamento no se puede procesar ya que esta enrutado a : {rowDetail.RoutedTo}. Es necesario cambiarlo "
            Exit Sub
        End If
        Using Form As New PopupCUM(Me._settingInventory, Me._currentDate, INDTxtStore.EditValue, DispensingIntegration, rowDetail.Tipo = 4, rowDetail.Ingreso)
            Form.StartPosition = FormStartPosition.CenterParent
            Form.CareGroupId = _careGroup.Id
            Form.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.8
            Form.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7
            Form.Product = rowDetail.Producto
            If String.IsNullOrEmpty(rowDetail.CodProducto) Then
                Form.ProductCode = rowDetail.Producto.Split(" - ").ElementAt(0).Trim()
            Else
                Form.ProductCode = rowDetail.CodProducto
            End If
            Form.QuantityDeliver = If(Not rowDetail.AllowBillingWithoutAuthorization _
                AndAlso If(rowDetail.NOPOS, False), If(rowDetail?.CantidadAutorizada, 0), rowDetail.CantidadPendiente)
            Form.Consecutive = rowDetail.ConsecutivePharmacy
            Form.DoseDeliver = rowDetail.TotalDose
            Form.DoseDeliverMeasurement = rowDetail.TotalDoseMeasurement
            If (rowDetail.Tipo = 4) Then
                Form.ListPhysicalInventoryCustody = rowDetail.ListPhysicalInventoryCustody
            Else
                Form.ListPhysicalInventory = rowDetail.ListPhysicalInventory
            End If

            Form.ProductType = rowDetail.TypeProduct
            If DispensingIntegration = 1 Then
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

    Private rowDetailDeferred As ViewDashboardPharmacyDetail

    Private Sub INDrptBtnDeferred_Click(sender As Object, e As EventArgs) Handles INDrptBtnDeferred.Click
        Me.Cursor = ChangeCursorIndigo()
        rowDetailDeferred = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
        Using Form As New FrmPopupDeferredDispensing(rowDetailDeferred, DocumentDate)
            AddHandler Form.SuccessDeferredEventArgs, AddressOf ReturnDeferred
            Form.StartPosition = FormStartPosition.CenterParent
            Dim trasparent As New FrmTransparent(Form, False)
            Me.Cursor = Cursors.Default
            trasparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnDeferred(sender As Object, e As SuccessEventArgs)
        If e IsNot Nothing Then
            rowDetailDeferred.ListDeferred = e.ListDeferred
            rowDetailDeferred.IsDeferred = True

            'Obtengo los registros de la fecha actual hacia atras
            Dim listTemp = (From x In e.ListDeferred Where x.DeliveryDate <= DateTime.Now() Select x).ToList()
            'rowDetailDeferred.CantidadSolicitada = listTemp.Sum(Function(x) x.DeliveryQuantity)
            rowDetailDeferred.CantidadPendiente = listTemp.Sum(Function(x) x.PendingQuantity)
            INDGcDashboardPharmacyDetail.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "GetActiveObjectInfo"

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If e.Info Is Nothing AndAlso e.SelectedControl Is INDGcDashboardPharmacyDetail Then
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(INDGcDashboardPharmacyDetail.FocusedView, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
            If info.InRowCell Then
                If info.Column.Name = INDColSource.Name Then
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
                    INDFlyMipres.HideBeakForm()
                ElseIf info.Column.Name = INDColSemaforoAlerta.Name Then
                    Dim row As ViewDashboardPharmacyDetail = view.GetRow(info.RowHandle)
                    INDLblCotnrolEntrega.Text = $"{row.CantidadDispensada}/{row.CantidadAutorizada}"
                    If row.ColorAlertaPBS IsNot Nothing Then
                        INDLblTitle.BackColor = Color.FromArgb(row.ColorAlertaPBS)
                    End If
                    INDFlyMipres.ShowBeakForm(view.GridControl.PointToScreen(e.ControlMousePosition))
                Else
                    INDFlyMipres.HideBeakForm()
                End If
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"
    Private Sub INDMeCUM_KeyDownPhysicalInventory(e As KeyEventArgs)
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

        'Se realiza el ordenamiento de acuerdo al almacen seleccionado para priorizar el almacen actual
        Dim warehouseId As Integer? = INDTxtStore.EditValue
        If warehouseId IsNot Nothing Then
            listPhysicalInventoryTmp = listPhysicalInventoryTmp.
                OrderBy(Function(d) If(warehouseId = d.WarehouseId, 0, 1)).
                ThenBy(Function(d) If(d.Covered, 0, 1)).
                ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()
        End If

        ' Filtrar los productos vencidos de la lista temporal (usar .Date para comparar solo fechas sin hora)
        Dim currentDate As Date = Date.Today.Date
        Dim listPhysicalInventoryNotExpired = listPhysicalInventoryTmp.Where(Function(p) _
            p.BatchSerialExpiredDate Is Nothing OrElse _
            CDate(p.BatchSerialExpiredDate).Date >= currentDate).ToList()

        If listPhysicalInventoryNotExpired.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Todos los lotes disponibles del producto escaneado están vencidos"
            INDMeCUM.EditValue = Nothing
            INDMeCUM.Focus()
            e.SuppressKeyPress = True
            Exit Sub
        End If

        Dim ATCProductCode As String
        Dim ATCTemp As ViewDashboardPharmacyDetail
        Dim product As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

        ' Usar la lista filtrada sin productos vencidos
        If dictionaryProduct.ContainsKey(listPhysicalInventoryNotExpired.ElementAt(0).ProductId) Then
            product = dictionaryProduct(listPhysicalInventoryNotExpired.ElementAt(0).ProductId)
        Else
            Using model As New MDashBoardPharmacy(MyTag)
                product = model.GetInventoryProductXpo(listPhysicalInventoryNotExpired.ElementAt(0).ProductId)
                dictionaryProduct.Add(listPhysicalInventoryNotExpired.ElementAt(0).ProductId, product)
            End Using
        End If

        If product.Code <> productATC(0) Then
            ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = productATC(0) AndAlso x.Tipo <> 4)
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

                    ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = ATCProductCode AndAlso x.Tipo <> 4)
                End Using
            ElseIf product.SupplieId IsNot Nothing Then
                Using model As New MDashBoardPharmacy(MyTag)

                    If dictionarySuppliedCode.ContainsKey(product.SupplieId.Id) Then
                        ATCProductCode = dictionarySuppliedCode(product.SupplieId.Id)
                    Else
                        ATCProductCode = model.GetInventorySuppliedXpo(product.SupplieId.Id).Code.Trim()
                        dictionarySuppliedCode.Add(product.SupplieId.Id, ATCProductCode)
                    End If

                    ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = ATCProductCode AndAlso x.Tipo <> 4)
                End Using
            Else
                ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = product.Code AndAlso x.Tipo <> 4)
            End If
        End If

        If ATCTemp IsNot Nothing Then
            ' Obtener el código del almacén seleccionado para priorizar
            Dim selectedWarehouseCode As String = INDTxtStore.Text.Split("-").ElementAt(0).Trim()

            If ATCTemp.ListPhysicalInventory IsNot Nothing AndAlso ATCTemp.ListPhysicalInventory.Count > 0 Then
                ' Validar primero si no se excede la cantidad pendiente
                Dim quantityDeliver = ATCTemp.ListPhysicalInventory.Sum(Function(x) x.QuantityDeliver) + 1
                If quantityDeliver > ATCTemp.CantidadPendiente Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar mas cantidad del producto " + ATCTemp.Producto + " porque supera la cantidad solicitada"
                    INDMeCUM.EditValue = Nothing
                    INDMeCUM.Focus()
                    e.SuppressKeyPress = True
                    Exit Sub
                End If

                ' Obtener los IDs de inventario físico ya asignados
                Dim listPhysicalInventoryId = (From p In ATCTemp.ListPhysicalInventory Select p.Id).ToList()

                ' PRIORIDAD 1: Buscar lotes del PRODUCTO ESCANEADO ya asignados del MISMO ALMACÉN con capacidad
                Dim existingFromScannedSameWarehouse = ATCTemp.ListPhysicalInventory.FirstOrDefault(Function(x) _
                    listPhysicalInventoryNotExpired.Any(Function(p) p.Id = x.Id) AndAlso _
                    x.QuantityDeliver < x.Quantity AndAlso _
                    x.CodeNameWarehouse.Split("-").ElementAt(0).Trim() = selectedWarehouseCode AndAlso _
                    (x.BatchSerialExpiredDate Is Nothing OrElse CDate(x.BatchSerialExpiredDate).Date >= currentDate))

                ' PRIORIDAD 2: Buscar lotes NUEVOS del PRODUCTO ESCANEADO del MISMO ALMACÉN
                Dim availablePhysicalSameWarehouse = listPhysicalInventoryNotExpired.FirstOrDefault(Function(p) _
                    Not listPhysicalInventoryId.Contains(p.Id) AndAlso _
                    p.Quantity > 0 AndAlso _
                    p.CodeNameWarehouse.Split("-").ElementAt(0).Trim() = selectedWarehouseCode AndAlso _
                    (p.BatchSerialExpiredDate Is Nothing OrElse CDate(p.BatchSerialExpiredDate).Date >= currentDate))

                ' PRIORIDAD 3: Buscar lotes del PRODUCTO ESCANEADO ya asignados de CUALQUIER ALMACÉN con capacidad
                Dim existingFromScannedAnyWarehouse = ATCTemp.ListPhysicalInventory.FirstOrDefault(Function(x) _
                    listPhysicalInventoryNotExpired.Any(Function(p) p.Id = x.Id) AndAlso _
                    x.QuantityDeliver < x.Quantity AndAlso _
                    (x.BatchSerialExpiredDate Is Nothing OrElse CDate(x.BatchSerialExpiredDate).Date >= currentDate))

                ' PRIORIDAD 4: Buscar lotes NUEVOS del PRODUCTO ESCANEADO de CUALQUIER ALMACÉN
                Dim availablePhysicalAnyWarehouse = listPhysicalInventoryNotExpired.FirstOrDefault(Function(p) _
                    Not listPhysicalInventoryId.Contains(p.Id) AndAlso _
                    p.Quantity > 0 AndAlso _
                    (p.BatchSerialExpiredDate Is Nothing OrElse CDate(p.BatchSerialExpiredDate).Date >= currentDate))

                If existingFromScannedSameWarehouse IsNot Nothing Then
                    ' CASO 1: Incrementar lote del producto escaneado (mismo almacén)
                    existingFromScannedSameWarehouse.QuantityDeliver += 1
                    ATCTemp.CantidadEntregada += 1
                ElseIf availablePhysicalSameWarehouse IsNot Nothing Then
                    ' CASO 2: Agregar nuevo lote del producto escaneado (mismo almacén)
                    availablePhysicalSameWarehouse.QuantityDeliver = 1
                    ATCTemp.CantidadEntregada += 1
                    ATCTemp.ListPhysicalInventory.Add(availablePhysicalSameWarehouse)
                    ATCTemp.Action = 1
                    ' Mantener color verde si todos son del mismo almacén
                    If ATCTemp.ListPhysicalInventory.All(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0).Trim() = selectedWarehouseCode) Then
                        ATCTemp.CUMSource = System.Drawing.Color.Green.ToArgb()
                    Else
                        ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                    End If
                ElseIf existingFromScannedAnyWarehouse IsNot Nothing Then
                    ' CASO 3: Incrementar lote del producto escaneado (otro almacén)
                    existingFromScannedAnyWarehouse.QuantityDeliver += 1
                    ATCTemp.CantidadEntregada += 1
                    ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                ElseIf availablePhysicalAnyWarehouse IsNot Nothing Then
                    ' CASO 4: Agregar nuevo lote del producto escaneado (otro almacén)
                    availablePhysicalAnyWarehouse.QuantityDeliver = 1
                    ATCTemp.CantidadEntregada += 1
                    ATCTemp.ListPhysicalInventory.Add(availablePhysicalAnyWarehouse)
                    ATCTemp.Action = 1
                    ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró más inventario físico disponible (no vencido) para el producto escaneado: " + product.Code + " - " + product.Name
                    INDMeCUM.EditValue = Nothing
                    INDMeCUM.Focus()
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
            Else
                ' Primera vez que se agrega inventario para este ATC
                ATCTemp.CantidadEntregada = 1
                ' PRIORIDAD: Buscar primero en el mismo almacén, solo lotes no vencidos y con cantidad disponible
                Dim physicalTmp = listPhysicalInventoryNotExpired.Find(Function(x) _
                    x.CodeNameWarehouse.Split("-").ElementAt(0).Trim() = selectedWarehouseCode AndAlso _
                    x.Quantity > 0 AndAlso _
                    (x.BatchSerialExpiredDate Is Nothing OrElse CDate(x.BatchSerialExpiredDate).Date >= currentDate))

                If physicalTmp IsNot Nothing Then
                    physicalTmp.QuantityDeliver = 1
                    ATCTemp.ListPhysicalInventory = New List(Of PhysicalInventory)
                    ATCTemp.ListPhysicalInventory.Add(physicalTmp)
                    ATCTemp.Action = 1
                    ATCTemp.CUMSource = System.Drawing.Color.Green.ToArgb()
                Else
                    ' Si no hay en el mismo almacén, tomar el primer lote no vencido disponible de otro almacén
                    Dim physicalOtherWarehouse = listPhysicalInventoryNotExpired.Find(Function(x) _
                        x.Quantity > 0 AndAlso _
                        (x.BatchSerialExpiredDate Is Nothing OrElse CDate(x.BatchSerialExpiredDate).Date >= currentDate))

                    If physicalOtherWarehouse IsNot Nothing Then
                        physicalOtherWarehouse.QuantityDeliver = 1
                        ATCTemp.ListPhysicalInventory = New List(Of PhysicalInventory)
                        ATCTemp.ListPhysicalInventory.Add(physicalOtherWarehouse)
                        ATCTemp.Action = 1
                        ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontró inventario físico disponible (no vencido) para el producto escaneado"
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                End If

            End If
            INDGcDashboardPharmacyDetail.RefreshDataSource()
            INDGvDashboardPharmacyDetail.ClearSelection()

            ''obtengo el item que se le asigno la cantidad para que se posicione en el
            Dim listTmp = INDGvDashboardPharmacyDetail.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewDashboardPharmacyDetail)()

            Dim item = listTmp.Find(Function(x) x.Producto.Split(" - ")(0) = ATCTemp.Producto.Split(" - ")(0) AndAlso x.Tipo <> 4)

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

    End Sub

    Private Sub INDMeCUM_KeyDownPhysicalCustody(e As KeyEventArgs, admissionNumber As String)
        Dim resultPhysical As ActionResult(Of List(Of PhysicalInventoryCustody)) = Nothing
        Dim productATC = INDMeCUM.EditValue.ToString.ToUpper().Trim().Split("*IND*")

        If dictionaryPhysicalInventoryCustodyBarCode.ContainsKey(productATC.ElementAt(0)) Then
            listPhysicalInventoryCustodyTmp = dictionaryPhysicalInventoryCustodyBarCode(productATC.ElementAt(0))
        Else
            Using model As New MDashBoardPharmacy(Me.Tag)
                If productATC.Length > 1 Then
                    resultPhysical = model.GetPhysicalInventoryCustodyBarCode(productATC.ElementAt(0), productATC.ElementAt(2), admissionNumber)
                Else
                    resultPhysical = model.GetPhysicalInventoryCustodyBarCode(productATC.ElementAt(0), "", admissionNumber)
                End If
            End Using

            If resultPhysical.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = resultPhysical.Message
                INDMeCUM.EditValue = Nothing
                INDMeCUM.Focus()
                e.SuppressKeyPress = True
                Exit Sub
            End If
            listPhysicalInventoryCustodyTmp = resultPhysical.ObjectEmbbeded
            dictionaryPhysicalInventoryCustodyBarCode.Add(productATC.ElementAt(0), resultPhysical.ObjectEmbbeded)
        End If

        Dim ATCProductCode As String
        Dim ATCTemp As ViewDashboardPharmacyDetail
        Dim product As Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

        If dictionaryProduct.ContainsKey(listPhysicalInventoryCustodyTmp.ElementAt(0).ProductId) Then
            product = dictionaryProduct(listPhysicalInventoryCustodyTmp.ElementAt(0).ProductId)
        Else
            Using model As New MDashBoardPharmacy(MyTag)
                product = model.GetInventoryProductXpo(listPhysicalInventoryCustodyTmp.ElementAt(0).ProductId)
                dictionaryProduct.Add(listPhysicalInventoryCustodyTmp.ElementAt(0).ProductId, product)
            End Using
        End If

        If product.Code <> productATC(0) Then
            ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = productATC(0) AndAlso x.Tipo = 4)
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

                    ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = ATCProductCode AndAlso x.Tipo = 4)
                End Using
            ElseIf product.SupplieId IsNot Nothing Then
                Using model As New MDashBoardPharmacy(MyTag)

                    If dictionarySuppliedCode.ContainsKey(product.SupplieId.Id) Then
                        ATCProductCode = dictionarySuppliedCode(product.SupplieId.Id)
                    Else
                        ATCProductCode = model.GetInventorySuppliedXpo(product.SupplieId.Id).Code.Trim()
                        dictionarySuppliedCode.Add(product.SupplieId.Id, ATCProductCode)
                    End If

                    ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = ATCProductCode AndAlso x.Tipo = 4)
                End Using
            Else
                ATCTemp = listDashboardPharmacyDetail.Find(Function(x) x.Producto.Split(" - ").ElementAt(0).Trim() = product.Code AndAlso x.Tipo = 4)
            End If
        End If

        If ATCTemp IsNot Nothing Then

            If ATCTemp.ListPhysicalInventoryCustody IsNot Nothing AndAlso ATCTemp.ListPhysicalInventoryCustody.Count > 0 Then
                Dim currentPhysical = ATCTemp.ListPhysicalInventoryCustody.ElementAt(ATCTemp.ListPhysicalInventoryCustody.Count - 1)
                Dim quantityDeleiver = ATCTemp.ListPhysicalInventoryCustody.Sum(Function(x) x.QuantityDeliver) + 1
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

                    Dim listPhysicalInventoryId = (From p In ATCTemp.ListPhysicalInventoryCustody Select p.Id).ToList()

                    listPhysicalInventoryCustodyTmp = (From p In listPhysicalInventoryCustodyTmp Where Not listPhysicalInventoryId.Contains(p.Id) Select p).ToList()

                    If listPhysicalInventoryCustodyTmp.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontro mas inventario fisico para el producto " + ATCTemp.Producto
                        INDMeCUM.EditValue = Nothing
                        INDMeCUM.Focus()
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    ATCTemp.CantidadEntregada += 1
                    listPhysicalInventoryCustodyTmp.ElementAt(0).QuantityDeliver = 1
                    ATCTemp.ListPhysicalInventoryCustody.Add(listPhysicalInventoryCustodyTmp.ElementAt(0))
                    ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                End If
            Else
                ATCTemp.CantidadEntregada = 1
                Dim physicalTmp = listPhysicalInventoryCustodyTmp.Find(Function(x) x.CodeNameWarehouse.Split("-").ElementAt(0) = INDTxtStore.Text.Split("-").ElementAt(0))
                If physicalTmp IsNot Nothing Then
                    physicalTmp.QuantityDeliver = 1
                    ATCTemp.ListPhysicalInventoryCustody = New List(Of PhysicalInventoryCustody)
                    ATCTemp.ListPhysicalInventoryCustody.Add(physicalTmp)
                    ATCTemp.Action = 1
                    ATCTemp.CUMSource = System.Drawing.Color.Green.ToArgb()
                Else
                    listPhysicalInventoryCustodyTmp.ElementAt(0).QuantityDeliver = 1
                    ATCTemp.ListPhysicalInventoryCustody = New List(Of PhysicalInventoryCustody)
                    ATCTemp.ListPhysicalInventoryCustody.Add(listPhysicalInventoryCustodyTmp.ElementAt(0))
                    ATCTemp.Action = 1
                    ATCTemp.CUMSource = System.Drawing.Color.Yellow.ToArgb()
                End If

            End If
            INDGcDashboardPharmacyDetail.RefreshDataSource()
            INDGvDashboardPharmacyDetail.ClearSelection()

            ''obtengo el item que se le asigno la cantidad para que se posicione en el
            Dim listTmp = INDGvDashboardPharmacyDetail.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewDashboardPharmacyDetail)()

            Dim item = listTmp.Find(Function(x) x.Producto.Split(" - ")(0) = ATCTemp.Producto.Split(" - ")(0) AndAlso x.Tipo = 4)

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
    End Sub

    Dim Start1 As Integer
    Dim s As Integer
    Dim duration1 As Integer
    Dim flag As Boolean = True


    Private Sub INDMeCUM_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeCUM.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDMeCUM.EditValue Is Nothing Then
                Exit Sub
            End If
            Dim rowDetail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
            If rowDetail IsNot Nothing AndAlso rowDetail.Tipo = 4 Then
                'INDMeCUM_KeyDownPhysicalCustody(e, rowDetail.NUMEFOLIO)
                INDMeCUM_KeyDownPhysicalCustody(e, rowDetail.Ingreso)
            Else
                INDMeCUM_KeyDownPhysicalInventory(e)
            End If
        End If

        If PermisoBarCode = False Then
            Start1 = DateTime.Now.Millisecond
        End If
    End Sub

    Private Sub INDMeCUM_KeyUp(sender As Object, e As KeyEventArgs) Handles INDMeCUM.KeyUp

        If PermisoBarCode = False Then
            s = DateTime.Now.Millisecond

            duration1 = s - Start1
            If flag = True Then
                flag = False
            End If
            If duration1 > 15 Then

                If INDMeCUM.Text <> String.Empty Then

                    INDMeCUM.Text = String.Empty

                End If

                MessageIndigo.Show("No se permite Digitar Manualmente", MessageType.Warning, Me.Text, Botones.Aceptar)


            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDrepPceDeferred_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDrepPceDeferred.QueryPopUp
        Dim detail = DirectCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
        INDgcDeferred.DataSource = Nothing
        INDgcDeferred.DataSource = detail.ListDeferred
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
        Anulate()
    End Sub

    'Funcion utilizada para anular los productos
    Private Function Anulate()
        'Informacion del popup de la razon de anulacion
        Dim ResultAnnulmentReason = ShowAnnulmentReasonsPopup()

        If ResultAnnulmentReason?.StateResult Then
            AsyncLoader(True)

            'se cambia el estado del action a 2 para anular y se le agrega las razones de anulacion a cada item 
            If listDashboardPharmacyDetail?.Any() Then
                For Each item In listDashboardPharmacyDetail
                    item.PattientSupplyOrder = 1
                    item.AdmissionNumeber = _admissionNumber
                    item.PattientCode = _patientCode
                    item.CareCenterCode = _careCenter
                    item.FunctionUnitCode = _functionalUnitCode
                    item.ConsecutivePescription = If(_consecutivePescription = String.Empty, 0, Convert.ToInt32(_consecutivePescription))
                    item.ConsecutiveInputs = _consecutiveInputs
                    item.ConsecutivePharmacy = _consecutivePharmacy
                    item.Action = 2
                    item.CantidadEntregada = 0
                    item.HCMOANULBId = ResultAnnulmentReason?.ObjectEmbbeded?.idhcmoanulb
                    item.Description = ResultAnnulmentReason?.ObjectEmbbeded?.description
                Next
                Guardar()
            End If

            AsyncLoader(False)
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
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = DialogResult.OK Then
                Dim AnnulmentReasons As Object = New ExpandoObject()
                AnnulmentReasons.IdHCMOANULB = formulario.IdHCMOANULB
                AnnulmentReasons.Description = formulario.Description

                'variable donde se almacena el mensaje que muestra la alerta
                Dim concatMessages As String

                'segun el tipo de filtro elejido asi se muestra el mensaje
                Select Case typeFilter
                    Case 1 'medicamentos
                        concatMessages = "Se va a realizar la anulación de los medicamentos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 2 'insumos
                        concatMessages = "Se va a realizar la anulación de los insumos filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case 3 'medicamentos tipos insumos
                        concatMessages = "Se va a realizar la anulación de los Medicamentos tipo insumo filtrado, ¿está seguro que desea anular?"
                        If MessageIndigo.Show(concatMessages, MessageType.Warning, Me.Text, Botones.AceptarCancelar, "", {"Si", "No"}) <> DialogResult.Yes Then
                            Return New ActionResult(Of Object) With {.StateResult = False}
                        End If
                    Case Else 'en caso de elegir todos o no seleccionar ninguno no se muestra la alerta
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

#Region "GridViewEvents"
    Private Sub INDGvDashboardPharmacyDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvDashboardPharmacyDetail.CustomColumnDisplayText
        If e.Column.Name = INDColDosisPres.Name AndAlso e.Value = -1 Then
            e.DisplayText = "Continuo"
        End If
    End Sub
    Private Sub INDGvDashboardPharmacyDetail_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles INDGvDashboardPharmacyDetail.CustomUnboundColumnData
        Dim View As GridView = sender
        If e.Column.FieldName = "INDColProduct" AndAlso e.IsGetData Then
            Dim Resultado As String = String.Empty
            Resultado = $"{View.GetListSourceRowCellValue(e.ListSourceRowIndex, "CodProducto")}"
            e.Value = Resultado
        End If
        If e.Column.FieldName = "INDColPresProduct" AndAlso e.IsGetData Then
            Dim Resultado As String = String.Empty
            Resultado = $"{View.GetListSourceRowCellValue(e.ListSourceRowIndex, "Producto")}<br>"
            Resultado = $"{Resultado}{View.GetListSourceRowCellValue(e.ListSourceRowIndex, "ADMINISTRACION")}"
            e.Value = Resultado
        End If
        If e.Column.FieldName = "INDColTipo" Then
            Dim Description As String = String.Empty
            Dim Obj = e.Row
            Select Case Obj.Tipo
                Case "1"
                    Description = "Medicamento"
                Case "2"
                    Description = "Materiales e Insumos"
                Case "3"
                    Description = "Código Heon"
                Case "4"
                    Description = "Medicamento en Custodia"
                Case "5"
                    Description = String.Concat("Paquete de enfermería - ", Obj.NombrePaqueteEnf)
            End Select
            e.Value = Description
        End If
    End Sub

    ''' <summary>
    ''' evento para mostrar dependiendo del tipo de item el popup del memo edit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRIMEENote_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDRIMEENote.QueryPopUp
        Dim obj = TryCast(INDGvDashboardPharmacyDetail.GetFocusedRow(), ViewDashboardPharmacyDetail)
        Dim Memo = CType(sender, DevExpress.XtraEditors.MemoExEdit)
        If Not {"1", "4"}.Contains(obj.Tipo) Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' evento cuando cambia el valor del repository para que actualice el valor en datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRIMEENote_EditValueChanged(sender As Object, e As EventArgs) Handles INDRIMEENote.EditValueChanged
        If INDGvDashboardPharmacyDetail.PostEditor() Then
            INDGvDashboardPharmacyDetail.UpdateCurrentRow()
        End If
    End Sub

    Private Sub btnKardex_Click(sender As Object, e As EventArgs) Handles btnKardex.Click
        Using Kardex As New IndigoComponents.frmHCKardexProductos(_patientCode, _admissionNumber, _careCenter, _functionalUnitCode, IndigoComponents.frmHCKardexProductos.eTipoProducto.Medicamentos, IndigoComponents.frmHCKardexProductos.eTipoKardex.Todos)
           Kardex.ShowDialog(Me)
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
#End Region

End Class