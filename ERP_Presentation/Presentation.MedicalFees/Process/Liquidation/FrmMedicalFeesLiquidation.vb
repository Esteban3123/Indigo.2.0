'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Maintenance.MVP
Imports Presentation.MedicalFees.MVP
Imports Presentation.Payments.MVP

#End Region

Public Class FrmMedicalFeesLiquidation
    Implements IMedicalFeesLiquidation

#Region "Builder"

    Public ctrTmp As CtrValuePaymentsDeductions

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrValuePaymentsDeductions()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshTotalValues()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)

        ' Inicializar CancellationTokenSource
        _globalCts = New CancellationTokenSource()
    End Sub

    Private Function getValues() As Tuple(Of Decimal, Decimal, Decimal)
        Dim valuePayment As Decimal = 0
        Dim valueDeduction As Decimal = 0
        Dim valueGlosa As Decimal = 0
        Dim valueTotalCxP As Decimal = 0
        Dim valueTotalDeductions As Decimal = 0
        If _listMedicalFeesLiquidationDetail IsNot Nothing AndAlso _listMedicalFeesLiquidationDetail.Count > 0 Then
            valuePayment = _listMedicalFeesLiquidationDetail.Sum(Function(item) item.TotalAmountPayable)
        End If
        If ListMedicalFeesLiquidationDetailForDeductions IsNot Nothing AndAlso ListMedicalFeesLiquidationDetailForDeductions.Count > 0 Then
            valueDeduction = ListMedicalFeesLiquidationDetailForDeductions.Sum(Function(item) item.TotalAmountPayable)
        End If
        If ListMedicalFeesLiquidationDetailForGlosas IsNot Nothing AndAlso ListMedicalFeesLiquidationDetailForGlosas.Count > 0 Then
            valueGlosa = ListMedicalFeesLiquidationDetailForGlosas.Sum(Function(item) item.TotalAmountPayable)
        End If

        valueTotalDeductions = valueDeduction + valueGlosa
        valueTotalCxP = valuePayment - valueTotalDeductions
        Return New Tuple(Of Decimal, Decimal, Decimal)(valueTotalCxP, valuePayment, valueTotalDeductions)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Listado de homologaciones del showPopup
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCupsHomologation As List(Of CupsHomologation)

    ''' <summary>
    ''' Obtiene o establece el tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type As Integer? Implements IMedicalFeesLiquidation.Type
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer? Implements IMedicalFeesLiquidation.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As XPInstantFeedbackSource Implements IMedicalFeesLiquidation.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _thirdPartyid As Integer?
    Public Property ThirdPartyId As Integer?
        Get
            Return _thirdPartyid
        End Get
        Set(value As Integer?)
            _thirdPartyid = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessional As String Implements IMedicalFeesLiquidation.HealthProfessional
        Get
            Return INDsleHealthProfessional.EditValue
        End Get
        Set(value As String)
            INDsleHealthProfessional.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessionalXpo As XPInstantFeedbackSource Implements IMedicalFeesLiquidation.HealthProfessionalXpo
        Get
            Return INDsleHealthProfessional.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthProfessional.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As DateTime? Implements IMedicalFeesLiquidation.EndDate
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As DateTime?)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As DateTime? Implements IMedicalFeesLiquidation.InitialDate
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As DateTime?)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As MedicalFeesSecuence Implements IMedicalFeesLiquidation.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As MedicalFeesSecuence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.MedicalFeesSecuenceDetail In Me._sequense.MedicalFeesSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IMedicalFeesLiquidation.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del contrato profesional de la salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MedicalFeesContractId As Integer? Implements IMedicalFeesLiquidation.MedicalFeesContractId
        Get
            Return INDsleMedicalFeesContract.EditValue
        End Get
        Set(value As Integer?)
            INDsleMedicalFeesContract.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del contrato profesional de la salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MedicalFeesContractXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesLiquidation.MedicalFeesContractXpo
        Get
            Return INDsleMedicalFeesContract.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMedicalFeesContract.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IMedicalFeesLiquidation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String Implements IMedicalFeesLiquidation.BillNumber
        Get
            Return INDtxtBillNumber.EditValue
        End Get
        Set(value As String)
            INDtxtBillNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IMedicalFeesLiquidation.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitId As Integer? Implements IMedicalFeesLiquidation.FilingUnitId
        Get
            Return INDsleFilingUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitXpo As List(Of FilingUnit) Implements IMedicalFeesLiquidation.FilingUnitXpo
        Get
            Return INDsleFilingUnit.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDsleFilingUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeId As Integer? Implements IMedicalFeesLiquidation.SupplierTypeId
        Get
            Return INDsleSupplierType.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeXpo As List(Of SupplierType) Implements IMedicalFeesLiquidation.SupplierTypeXpo
        Get
            Return INDsleSupplierType.Properties.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDsleSupplierType.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private ListCausation As XPCollection

    ''' <summary>
    ''' CancellationTokenSource para operaciones asíncronas de carga de pagos
    ''' </summary>
    Private _listPaymentsCts As CancellationTokenSource

    ''' <summary>
    ''' CancellationTokenSource para operaciones asíncronas de carga de controles
    ''' </summary>
    Private _loadControlsCts As CancellationTokenSource

    ''' <summary>
    ''' Task actual de carga de pagos (para rastrear si está en progreso)
    ''' </summary>
    Private _listPaymentsTask As Task

    ''' <summary>
    ''' Task actual de carga de controles (para rastrear si está en progreso)
    ''' </summary>
    Private _loadControlsTask As Task

    ''' <summary>
    ''' Indica si la carga inicial de datos está en progreso
    ''' </summary>
    Private isLoadingData As Boolean = False

    ''' <summary>
    ''' Indica si _listMedicalFeesLiquidationDetail está sincronizada con la rejilla
    ''' </summary>
    Private isListSynchronized As Boolean = False

    ''' <summary>
    ''' Bandera para search de unidad radicacion (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banFilingUnit As Boolean = True

    ''' <summary>
    ''' Representa el presentador de contratos profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PMedicalFeesLiquidation

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MedicalFees"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMedicalFees

    ''' <summary>
    ''' Lista de los detalles de la liquidacion para pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _listMedicalFeesLiquidationDetail As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Listado de eliminados de los detalles de la liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteMedicalFeesLiquidationDetail As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Lista de los detalles de la liquidacion para deducciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMedicalFeesLiquidationDetailForDeductions As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Lista de los detalles de la liquidacion para glosas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMedicalFeesLiquidationDetailForGlosas As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Bandera para search de tipo de proveedor (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banSearchSupplierType As Boolean = True

    ''' <summary>
    ''' Representa a la entidad de liquidaciones de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim medicalFeesLiquidation As MedicalFeesLiquidation

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.MedicalFeesSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable para controlar el editValueChanged de los controles de search
    ''' </summary>
    ''' <remarks></remarks>
    Private banSearch As Boolean = True

    ''' <summary>
    ''' Variable pivot para sacar los registros que esten con invoiceReversal = True
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listMFLD As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Variable para controlar el editValueChanged del control del medico y las fechas
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSearchChanged As Boolean = True

    ''' <summary>
    ''' Id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierId As Integer?

    ''' <summary>
    ''' Id de supplierDistributionLine
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierDistributionLine As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del tercero que se utilizara solo en la consulta xpo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _thirdPartyIdConsult As Integer?

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos
    ''' </summary>
    Dim ListType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de id de contratos para la consulta por medico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMedicalFeesContractId As List(Of Integer)

    ''' <summary>
    ''' CancellationTokenSource global para operaciones como Deshacer
    ''' </summary>
    Private _globalCts As CancellationTokenSource

#End Region

#Region "Task Helpers"

    ''' <summary>
    ''' Verifica si la carga de pagos está en progreso
    ''' </summary>
    Private Function IsListPaymentsRunning() As Boolean
        Return _listPaymentsTask IsNot Nothing AndAlso
               Not _listPaymentsTask.IsCompleted AndAlso
               Not _listPaymentsTask.IsCanceled AndAlso
               Not _listPaymentsTask.IsFaulted
    End Function

    ''' <summary>
    ''' Verifica si la carga de controles está en progreso
    ''' </summary>
    Private Function IsLoadControlsRunning() As Boolean
        Return _loadControlsTask IsNot Nothing AndAlso
               Not _loadControlsTask.IsCompleted AndAlso
               Not _loadControlsTask.IsCanceled AndAlso
               Not _loadControlsTask.IsFaulted
    End Function

    ''' <summary>
    ''' Cancela todas las operaciones asíncronas en progreso
    ''' </summary>
    Private Sub CancelAllAsyncOperations()
        Try
            ' Cancelar operaciones específicas
            _listPaymentsCts?.Cancel()
            _loadControlsCts?.Cancel()
            _globalCts?.Cancel()

            System.Diagnostics.Debug.WriteLine("✅ Todas las operaciones asíncronas canceladas")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error cancelando operaciones: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Espera a que terminen todas las operaciones asíncronas
    ''' </summary>
    Private Async Function WaitForAllAsyncOperations() As Task
        Dim tasks As New List(Of Task)()

        If IsListPaymentsRunning() Then tasks.Add(_listPaymentsTask)
        If IsLoadControlsRunning() Then tasks.Add(_loadControlsTask)

        If tasks.Any() Then
            Try
                Await Task.WhenAll(tasks)
            Catch ex As OperationCanceledException
                System.Diagnostics.Debug.WriteLine("Operaciones canceladas correctamente")
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error esperando operaciones: {ex.Message}")
            End Try
        End If
    End Function

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        ' ✅ Verificar si hay operaciones async en progreso usando el estado real de las Tasks
        If IsListPaymentsRunning() OrElse IsLoadControlsRunning() Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede guardar porque no ha terminado de cargar los pagos"
            Exit Sub
        End If
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Using model As New MMedicalFeesLiquidation(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveMedicalFeesLiquidation(medicalFeesLiquidation, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If medicalFeesLiquidation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                        Me.DicSequense(Me._sequense.MedicalFeesSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                    If Me._sequense.Sequential Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    End If
                ElseIf medicalFeesLiquidation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.medicalFeesLiquidation = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para guardar y confrimar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GuardarConfirmar()
        ' ✅ Verificar si hay operaciones async en progreso usando el estado real de las Tasks
        If IsListPaymentsRunning() OrElse IsLoadControlsRunning() Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede confirmar porque no ha terminado de cargar los pagos"
            Exit Sub
        End If
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        ValidateCausationReversal()
        AssigningValues()
        Using model As New MMedicalFeesLiquidation(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveAndConfirmMedicalFeesLiquidation(medicalFeesLiquidation, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Me.medicalFeesLiquidation = Result.ObjectEmbbeded
                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontAccountPayable", NAME_MODULE), Result.ObjectEmbbeded.Code, Result.MessageResult(0))

                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.MessageResult(0) IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que anula la liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function Anular() As Task
        ' ✅ Verificar si hay operaciones async en progreso usando el estado real de las Tasks
        If IsListPaymentsRunning() OrElse IsLoadControlsRunning() Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede anular porque no ha terminado de cargar los pagos"
            Exit Function
        End If
        Using model As New MMedicalFeesLiquidation(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.AnnularMedicalFeesLiquidation(medicalFeesLiquidation)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.MessageResult(0) IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString
                End If
            End If
        End Using
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewMedicalFeesLiquidation()
        End If
    End Sub

    ''' <summary>
    ''' Agrega columna de acciones
    ''' </summary>
    Private Sub AddColumnActions()
        IndigoGridView1.SetListAcction(viewGridPayment, {eAcciones.Edit}.ToList())
        IndigoGridView2.SetListAcction(INDGvCausationPending, {eAcciones.Process}.ToList())

        ' Configurar columna de acciones en rejilla de pagos
        Dim col = viewGridPayment.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then
            col.Width = 120
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim ListItems As New List(Of Tuple(Of String, Integer))
        ListItems.Add(New Tuple(Of String, Integer)("Registrado", 1))
        ListItems.Add(New Tuple(Of String, Integer)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Integer)("Anulado", 3))

        Dim ListTypes As New List(Of Tuple(Of String, Integer))
        ListTypes.Add(New Tuple(Of String, Integer)("Agremiación", 1))
        ListTypes.Add(New Tuple(Of String, Integer)("Médico", 2))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "LiquidationType", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListTypes},
                              New ColumnInfo() With {.Caption = "Médico", .FieldName = "HealthProfessionalCode", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Contrato", .FieldName = "MedicalFeesContractId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Fecha Inicial", .FieldName = "InitialDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Fecha Final", .FieldName = "EndDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListItems}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMedicalFeesLiquidation
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles de deducciones y glosas de forma ASÍNCRONA PURA
    ''' </summary>
    ''' <param name="cancellationToken">Token para cancelar la operación</param>
    Private Async Function LoadControlsAsync(cancellationToken As CancellationToken) As Task
        Try
            isLoadingData = True
            isListSynchronized = False

            ' *** OPERACIONES EN BACKGROUND (sin acceso a UI) ***
            Await Task.Run(Async Function()
                               'Se convierte lo consultado por xpo a entidad
                               Await ConvertXpoToEntityAsync(cancellationToken)

                               cancellationToken.ThrowIfCancellationRequested()

                               'Pagos
                               _listMedicalFeesLiquidationDetail = (From mfl In medicalFeesLiquidation.MedicalFeesLiquidationDetail Where mfl.LiquidationType = 1 Select mfl).ToList
                               'Deducciones
                               ListMedicalFeesLiquidationDetailForDeductions = (From mfl In medicalFeesLiquidation.MedicalFeesLiquidationDetail Where mfl.LiquidationType = 2 Select mfl).ToList
                               'Glosas
                               ListMedicalFeesLiquidationDetailForGlosas = (From mfl In medicalFeesLiquidation.MedicalFeesLiquidationDetail Where mfl.LiquidationType = 3 Select mfl).ToList
                           End Function, cancellationToken)

            cancellationToken.ThrowIfCancellationRequested()

            ' *** ACTUALIZAR UI EN UI THREAD ***
            'Deducciones
            If ListMedicalFeesLiquidationDetailForDeductions IsNot Nothing AndAlso ListMedicalFeesLiquidationDetailForDeductions.Count > 0 Then
                INDlygDeductions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDgcDeductions.DataSource = Nothing
                INDgcDeductions.DataSource = ListMedicalFeesLiquidationDetailForDeductions
            End If

            'Glosas
            If ListMedicalFeesLiquidationDetailForGlosas IsNot Nothing AndAlso ListMedicalFeesLiquidationDetailForGlosas.Count > 0 Then
                INDlygGlosas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDgcGlosas.DataSource = Nothing
                INDgcGlosas.DataSource = ListMedicalFeesLiquidationDetailForGlosas
            End If

            ' Marcar que la carga inicial ha terminado y la lista está sincronizada
            isLoadingData = False
            isListSynchronized = True
            INDdteEndDate.Enabled = True
            ctrTmp.RefreshTotalValues()

            System.Diagnostics.Debug.WriteLine("✅ LoadControlsAsync completado exitosamente")

        Catch ex As OperationCanceledException
            System.Diagnostics.Debug.WriteLine("LoadControlsAsync cancelado por el usuario")
            INDdteEndDate.Enabled = True
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"LoadControlsAsync Error: {ex.Message}")
            INDdteEndDate.Enabled = True
            isLoadingData = False
        End Try
    End Function

    ''' <summary>
    ''' Carga el listado de causaciones de forma ASÍNCRONA PURA
    ''' </summary>
    ''' <param name="cancellationToken">Token para cancelar la operación</param>
    Private Async Function LoadListPaymentsAsync(cancellationToken As CancellationToken) As Task
        Try
            ' *** OPERACIONES EN BACKGROUND (sin acceso a UI) ***
            Await Task.Run(Sub()
                               Using model As New MMedicalFeesLiquidation(Me.Tag)
                                   ListCausation = Nothing
                                   ListCausation = model.ListCausationByMedicalFeesContractIdViewXpo(ListMedicalFeesContractId, MedicalFeesContractId, InitialDate, EndDate, HealthProfessional)
                               End Using

                               cancellationToken.ThrowIfCancellationRequested()

                               LoadCausations(cancellationToken) ' Versión optimizada con CancellationToken
                           End Sub, cancellationToken)

            ' *** ACTUALIZAR UI EN UI THREAD ***
            INDdteEndDate.Enabled = True
            ctrTmp.RefreshTotalValues()

            System.Diagnostics.Debug.WriteLine("✅ LoadListPaymentsAsync completado exitosamente")

        Catch ex As OperationCanceledException
            System.Diagnostics.Debug.WriteLine("LoadListPaymentsAsync cancelado por el usuario")
            INDdteEndDate.Enabled = True
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"LoadListPaymentsAsync Error: {ex.Message}")
            INDdteEndDate.Enabled = True
        End Try
    End Function

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeSupplierType() As Task
        Using model As New MSupplierType(Tag)
            Dim x As ActionResult(Of List(Of SupplierType)) = Await model.GetSupplierTypeBySupplierId(_supplierId)
            If x.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = x.MessageResult(0).ToString
                SupplierTypeXpo = Nothing
                Exit Function
            End If
            Dim listSupplierType As List(Of SupplierType) = x.ObjectEmbbeded
            SupplierTypeXpo = listSupplierType
        End Using
    End Function

    ''' <summary>
    ''' Llena los datasource de los controles de modo de impresion y control terminacion contrato
    ''' </summary>
    Private Sub InitializeSearch()
        ListType = New List(Of Tuple(Of Integer, String))
        ListType.Add(New Tuple(Of Integer, String)(1, "Agremiación"))
        ListType.Add(New Tuple(Of Integer, String)(2, "Médico"))
        INDsleType.Properties.DataSource = ListType.ToList
        INDsleType.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' Metodo que valida si existe una liquidacion sin confirmar typeConsult = 1 (Consulta solo por el contrato), typeConsult = 2 (Consulta por contrato y por médico)
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ValidateExistLiquidation(typeConsult As Integer) As Task
        Try
            Using Model As New MMedicalFeesLiquidation(CStr(Me.Tag))
                Dim resultOperation
                AsyncLoader(True)
                If typeConsult = 1 Then '1 = Consulta solo por contrato
                    resultOperation = Await (Model.GetMedicalFeesLiquidation(String.Empty, MedicalFeesContractId, 1))
                Else '2 = Consulta por médico
                    resultOperation = Await (Model.GetMedicalFeesLiquidation(String.Empty, 0, 2, HealthProfessional))
                End If
                AsyncLoader(False)
                medicalFeesLiquidation = resultOperation.ObjectEmbbeded
                If medicalFeesLiquidation IsNot Nothing AndAlso medicalFeesLiquidation.Id > 0 Then
                    Dim labelQuestion As String
                    If INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        labelQuestion = ResourceManager.GetString("ExistLiquidationWithHealthProfessional", NAME_MODULE)
                    Else
                        labelQuestion = ResourceManager.GetString("ExistLiquidationWithContract", NAME_MODULE)
                    End If
                    If MessageIndigo.Show(labelQuestion, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Await LoadControls(False)
                    Else
                        InitialDate = Nothing
                        medicalFeesLiquidation = New MedicalFeesLiquidation
                        banSearch = False
                        MedicalFeesContractId = Nothing
                        HealthProfessional = Nothing
                    End If
                Else
                    Await ValidateLoadCausation()
                    Await LoadDeductionsAndGlosasAsync()
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Valida si las causaciones obtenidas tienen la bandera de invoiceReversal en True
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateCausationReversal()
        If medicalFeesLiquidation IsNot Nothing AndAlso medicalFeesLiquidation.Id > 0 Then
            Dim cont As Integer = _listMedicalFeesLiquidationDetail.FindAll(Function(item) item.InvoiceReversal = True).Count
            If cont > 0 Then
                _listMFLD = (From mfld In _listMedicalFeesLiquidationDetail Where mfld.InvoiceReversal = True Select mfld).ToList
                Me.Cursor = ChangeCursorIndigo()
                Using Formulario As New FrmInvoiceReversalCausation
                    Formulario.ListMedicalFeesLiquidationDetail = _listMFLD
                    Formulario.ToolBar.Dock = DockStyle.None
                    Formulario.ViewModeEditHold = True
                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    Dim size As System.Drawing.Size
                    size.Width = 800
                    size.Height = 440
                    Formulario.Size = size
                    Dim transparent As New FrmTransparent(Formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog()

                    If Formulario.BanClose Then
                        For Each itemDetail As MedicalFeesLiquidationDetail In _listMFLD
                            itemDetail.MarkAsDeleted()
                        Next
                    Else
                        For Each itemDetail As MedicalFeesLiquidationDetail In _listMFLD
                            itemDetail.MarkAsUnchanged()
                        Next
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles de causación médica
    ''' </summary>
    Private Sub ClearCausationControls()
        _listMedicalFeesLiquidationDetail = Nothing
        INDgcCausation.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Valida si las fechas están completas
    ''' </summary>
    ''' <returns>True si InitialDate y EndDate tienen valores</returns>
    Private Function AreDatesValid() As Boolean
        Return InitialDate IsNot Nothing AndAlso EndDate IsNot Nothing
    End Function

    ''' <summary>
    ''' Determina si debe usar el modo de profesional de la salud
    ''' </summary>
    ''' <returns>True si el layout del profesional de la salud está visible</returns>
    Private Function IsHealthProfessionalMode() As Boolean
        Return INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Function

    ''' <summary>
    ''' Valida las condiciones para cargar causaciones en modo profesional de la salud
    ''' </summary>
    ''' <returns>True si se puede cargar data en este modo</returns>
    Private Function CanLoadInHealthProfessionalMode() As Boolean
        Return AreDatesValid() AndAlso HealthProfessional IsNot Nothing
    End Function

    ''' <summary>
    ''' Valida las condiciones para cargar causaciones en modo contrato médico
    ''' </summary>
    ''' <returns>True si se puede cargar data en este modo</returns>
    Private Function CanLoadInContractMode() As Boolean
        Return MedicalFeesContractId IsNot Nothing AndAlso AreDatesValid()
    End Function

    ''' <summary>
    ''' Ejecuta la carga de datos y configura controles relacionados
    ''' </summary>
    Private Async Function ExecuteDataLoad() As Task
        ' *** DESHABILITAR CONTROL EN UI THREAD ANTES DE INICIAR OPERACIÓN ASYNC ***
        INDdteEndDate.Enabled = False

        Await RefreshCausationDatasourceAsync(True)

        ' ✅ Iniciar carga de pagos async si no está en progreso
        If Not IsListPaymentsRunning() Then
            _listPaymentsCts?.Cancel()
            _listPaymentsCts = New CancellationTokenSource()
            _listPaymentsTask = LoadListPaymentsAsync(_listPaymentsCts.Token)
        End If
    End Function

    ''' <summary>
    ''' Valida que los controles estén llenos para poder hacer la consulta con XPInstantFeedbackSource (optimizada)
    ''' </summary>
    ''' <remarks>Optimizada para mayor legibilidad y mantenibilidad</remarks>
    Private Async Function ValidateLoadCausation() As Task
        Try
            Dim shouldLoadData As Boolean = False

            If IsHealthProfessionalMode() Then
                shouldLoadData = CanLoadInHealthProfessionalMode()
            Else
                shouldLoadData = CanLoadInContractMode()
            End If

            If shouldLoadData Then
                Await ExecuteDataLoad()
            Else
                ClearCausationControls()
            End If

        Catch ex As Exception
            ClearCausationControls()
            Mensaje(EeventViewerImages.MensajeError) = $"Error validando carga de causaciones: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' Mapea un objeto ViewListPaymentsMedicalFeesLiquidationXpo a MedicalFeesLiquidationDetail
    ''' </summary>
    ''' <param name="itemXpo">El objeto XPO a mapear</param>
    ''' <param name="liquidationType">Tipo de liquidación (1=Causación, 2=Deducción, 3=Glosa)</param>
    ''' <returns>Objeto MedicalFeesLiquidationDetail mapeado</returns>
    Private Function MapToMedicalFeesLiquidationDetail(itemXpo As ViewListPaymentsMedicalFeesLiquidationXpo, liquidationType As Integer) As MedicalFeesLiquidationDetail
        Return New MedicalFeesLiquidationDetail With {
            .LiquidationType = liquidationType,
            .MedicalFeesCausationId = itemXpo.MedicalFeesCausationId,
            .AdmissionNumber = itemXpo.AdmissionNumber,
            .PatientCode = itemXpo.PatientDescription,
            .ThirdPartyDescription = itemXpo.ThirdPartyDescription,
            .ServiceOrderCode = itemXpo.CodeServiceOrder,
            .AmountPayable = itemXpo.AmountPayable,
            .InvoiceQuantity = itemXpo.InvoiceQuantity,
            .TotalAmountPayable = itemXpo.TotalAmountPayable,
            .MedicalFeesContractValue = itemXpo.MedicalFeesContractValue,
            .InvoiceReversal = itemXpo.InvoiceReversal,
            .StatusCausation = itemXpo.Status,
            .CausationDate = itemXpo.CausationDate,
            .IPSServiceName = itemXpo.IPSServiceName,
            .ServiceDate = itemXpo.ServiceDate,
            .InvoiceDetailId = itemXpo.InvoiceDetailId,
            .InvoiceId = itemXpo.InvoiceId
        }
    End Function

    ''' <summary>
    ''' Maneja los elementos marcados para eliminación
    ''' </summary>
    ''' <param name="currentList">Lista actual de elementos</param>
    ''' <param name="liquidationType">Tipo de liquidación</param>
    Private Sub ProcessDeletedItems(currentList As List(Of MedicalFeesLiquidationDetail), liquidationType As Integer)
        If medicalFeesLiquidation?.Id <= 0 OrElse medicalFeesLiquidation.MedicalFeesLiquidationDetail?.Count <= 0 Then Return

        Dim existingItems = medicalFeesLiquidation.MedicalFeesLiquidationDetail.Where(Function(l) l.LiquidationType = liquidationType)

        For Each itemDetail In existingItems
            If Not currentList.Contains(itemDetail) Then
                If ListDeleteMedicalFeesLiquidationDetail Is Nothing Then
                    ListDeleteMedicalFeesLiquidationDetail = New List(Of MedicalFeesLiquidationDetail)()
                End If

                If Not ListDeleteMedicalFeesLiquidationDetail.Contains(itemDetail) Then
                    itemDetail.ChangeStatusMedicalFeesCausation = True
                    ListDeleteMedicalFeesLiquidationDetail.Add(itemDetail)
                End If
            Else
                ListDeleteMedicalFeesLiquidationDetail?.Remove(itemDetail)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga causaciones con soporte de cancelación - Versión optimizada para grandes volúmenes (87k+ registros)
    ''' Cambio de O(n²) a O(n) usando Dictionary lookup
    ''' </summary>
    ''' <param name="cancellationToken">Token para cancelar la operación</param>
    Private Sub LoadCausations(cancellationToken As CancellationToken)
        Try
            System.Diagnostics.Debug.WriteLine("LoadCausations INICIADO")

            ' ✅ Verificar cancelación antes de iniciar
            cancellationToken.ThrowIfCancellationRequested()

            ' Verificar datos
            If ListCausation?.Count <= 0 Then
                System.Diagnostics.Debug.WriteLine("LoadCausations cancelado - no hay datos")
                Return
            End If

            ' *** OPTIMIZACIÓN 1: Pre-size con capacidad conocida ***
            _listMedicalFeesLiquidationDetail = New List(Of MedicalFeesLiquidationDetail)(ListCausation.Count)

            ' *** OPTIMIZACIÓN 2: Cache/Dictionary lookup - O(1) vs O(n) ***
            Dim existingDetailsLookup = CreateExistingDetailsLookup()

            ' *** OPTIMIZACIÓN 3: Procesar en lotes para mejor memory management ***
            Dim batchSize = Math.Min(5000, ListCausation.Count)
            Dim totalBatches = Math.Ceiling(ListCausation.Count / batchSize)

            System.Diagnostics.Debug.WriteLine($"LoadCausations: Procesando {ListCausation.Count} registros en {totalBatches} lotes de {batchSize}")

            For batchIndex = 0 To totalBatches - 1
                ' ✅ Verificar cancelación entre lotes
                cancellationToken.ThrowIfCancellationRequested()

                Dim startIndex = batchIndex * batchSize
                Dim endIndex = Math.Min(startIndex + batchSize - 1, ListCausation.Count - 1)

                ' Procesar lote actual
                For i = startIndex To endIndex
                    Dim itemXpo = ListCausation(i)

                    ' Búsqueda O(1) en Dictionary vs O(n) en FirstOrDefault
                    If existingDetailsLookup.ContainsKey(itemXpo.MedicalFeesCausationId) Then
                        ' Usar el elemento existente
                        _listMedicalFeesLiquidationDetail.Add(existingDetailsLookup(itemXpo.MedicalFeesCausationId))
                    Else
                        ' Crear nuevo elemento
                        Dim newDetail = MapToMedicalFeesLiquidationDetail(itemXpo, 1)
                        _listMedicalFeesLiquidationDetail.Add(newDetail)
                    End If
                Next
            Next

            ' ✅ Verificar cancelación antes de procesar eliminaciones
            cancellationToken.ThrowIfCancellationRequested()

            ' Procesar elementos marcados para eliminación
            ProcessDeletedItems(_listMedicalFeesLiquidationDetail, 1)

            System.Diagnostics.Debug.WriteLine($"✅ LoadCausations completado: {_listMedicalFeesLiquidationDetail?.Count} elementos procesados")

        Catch ex As OperationCanceledException
            System.Diagnostics.Debug.WriteLine("LoadCausations CANCELADO por el usuario")
            Throw ' Re-lanzar para que el caller lo maneje
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ LoadCausations ERROR: {ex.Message}")
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Crea un Dictionary lookup para existing details - O(n) una sola vez vs O(n) por cada búsqueda
    ''' </summary>
    Private Function CreateExistingDetailsLookup() As Dictionary(Of Integer, MedicalFeesLiquidationDetail)
        Dim lookup = New Dictionary(Of Integer, MedicalFeesLiquidationDetail)()

        If medicalFeesLiquidation?.Id > 0 AndAlso medicalFeesLiquidation.MedicalFeesLiquidationDetail?.Count > 0 Then
            ' Crear lookup una sola vez - O(n)
            For Each detail In medicalFeesLiquidation.MedicalFeesLiquidationDetail
                If Not lookup.ContainsKey(detail.MedicalFeesCausationId) Then
                    lookup(detail.MedicalFeesCausationId) = detail
                End If
            Next

            System.Diagnostics.Debug.WriteLine($"Created lookup with {lookup.Count} existing details")
        End If

        Return lookup
    End Function

    ''' <summary>
    ''' Procesa una colección XPO y la convierte en lista de MedicalFeesLiquidationDetail
    ''' </summary>
    ''' <param name="collection">Colección XPO a procesar</param>
    ''' <param name="liquidationType">Tipo de liquidación (2=Deducción, 3=Glosa)</param>
    ''' <returns>Lista de MedicalFeesLiquidationDetail</returns>
    Private Function ProcessXpoCollection(collection As XPCollection, liquidationType As Integer) As List(Of MedicalFeesLiquidationDetail)
        If collection?.Count <= 0 Then Return Nothing

        Dim resultList = New List(Of MedicalFeesLiquidationDetail)()

        For Each itemXpo In collection
            Dim detail = New MedicalFeesLiquidationDetail With {
                .LiquidationType = liquidationType,
                .MedicalFeesCausationId = itemXpo.Id,
                .AdmissionNumber = itemXpo.AdmissionNumber,
                .PatientCode = itemXpo.PatientCode,
                .ThirdPartyDescription = itemXpo.ThirdPartyId.NitName,
                .ServiceOrderCode = itemXpo.ServiceOrderId.Code,
                .AmountPayable = itemXpo.AmountPayable,
                .InvoiceQuantity = itemXpo.InvoiceQuantity,
                .TotalAmountPayable = itemXpo.TotalAmountPayable,
                .MedicalFeesContractValue = itemXpo.MedicalFeesContractValue,
                .InvoiceReversal = itemXpo.InvoiceReversal,
                        .StatusCausation = itemXpo.Status
            }
            resultList.Add(detail)
        Next

        Return resultList
    End Function

    ''' <summary>
    ''' Configura el DataSource y la visibilidad de un control grid
    ''' </summary>
    ''' <param name="gridControl">Control grid a configurar</param>
    ''' <param name="layoutGroupVisibility">Grupo layout para visibilidad</param>
    ''' <param name="dataSource">Fuente de datos</param>
    Private Sub ConfigureGridDataSource(gridControl As Object, layoutGroupVisibility As Object, dataSource As Object)
        gridControl.DataSource = Nothing
        gridControl.DataSource = dataSource

        If dataSource IsNot Nothing Then
            layoutGroupVisibility.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            layoutGroupVisibility.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Versión asíncrona: Lista en las rejillas correspondientes las deducciones y glosas (NO bloquea la UI)
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadDeductionsAndGlosasAsync() As Task
        ' Validaciones iniciales
        If INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If HealthProfessional Is Nothing Then Return
        Else
            If MedicalFeesContractId Is Nothing Then Return
        End If

        Try
            Using model As New MMedicalFeesLiquidation(Me.Tag)
                ' *** EJECUTAR CONSULTAS EN PARALELO para mejor rendimiento ***
                Dim taskDeductions = Task.Run(Function() model.ListCausationByMedicalFeesContractIdForDeductions(ListMedicalFeesContractId, MedicalFeesContractId, HealthProfessional))
                Dim taskGlosas = Task.Run(Function() model.ListCausationByMedicalFeesContractIdForGlosas(ListMedicalFeesContractId, MedicalFeesContractId, HealthProfessional))

                ' Esperar a que ambas consultas terminen
                Await Task.WhenAll(taskDeductions, taskGlosas)

                ' Procesar Deducciones
                Dim listCausationDeductions = Await taskDeductions
                ListMedicalFeesLiquidationDetailForDeductions = ProcessXpoCollection(listCausationDeductions, 2)
                ConfigureGridDataSource(INDgcDeductions, INDlygDeductions, ListMedicalFeesLiquidationDetailForDeductions)

                ' Procesar Glosas
                Dim listCausationGlosas = Await taskGlosas
                ListMedicalFeesLiquidationDetailForGlosas = ProcessXpoCollection(listCausationGlosas, 3)
                ConfigureGridDataSource(INDgcGlosas, INDlygGlosas, ListMedicalFeesLiquidationDetailForGlosas)
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"LoadDeductionsAndGlosasAsync Error: {ex.Message}")
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMedicalFeesLiquidation.ActionsOnControls
        Set(value As Boolean)
            INDlyMedicalFeesLiquidation.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleType.Enabled = value
            INDsleMedicalFeesContract.Enabled = value
            INDdteInitialDate.Enabled = value
            INDdteEndDate.Enabled = value
            INDtxtBillNumber.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDsleFilingUnit.Enabled = value
            INDsleSupplierType.Enabled = value
            INDgcCausation.Enabled = value
            INDGcCausationPending.Enabled = value
            INDlyMedicalFeesLiquidation.EndUpdate()
            If value Then
                INDsleType.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.medicalFeesLiquidation IsNot Nothing AndAlso Me.medicalFeesLiquidation.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.medicalFeesLiquidation.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.medicalFeesLiquidation.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.medicalFeesLiquidation.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.medicalFeesLiquidation.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.medicalFeesLiquidation.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Limpia todos los controles del formulario - OPTIMIZADO para evitar parpadeos
    ''' </summary>
    ''' <remarks>
    ''' Usa SuspendLayout/ResumeLayout y BeginUpdate/EndUpdate para eliminar el parpadeo visual
    ''' </remarks>
    Private Sub CleanControls()
        Try
            ' *** SUSPENDER TODO EL REPAINT DEL FORMULARIO ***
            Me.SuspendLayout()
            INDlyMedicalFeesLiquidation.BeginUpdate()

            ' *** SUSPENDER ACTUALIZACIONES DE REJILLAS ***
            INDgcCausation.BeginUpdate()
            INDgcDeductions.BeginUpdate()
            INDgcGlosas.BeginUpdate()
            INDGcCausationPending.BeginUpdate()

            ' === LIMPIEZA DE CONTROLES Y DATOS ===
            ReadOnlyControls(False)
            INDdteInitialDate.Properties.ReadOnly = True

            ThirdPartyId = Nothing
            _thirdPartyIdConsult = Nothing
            ActionsOnControls = False
            BarraBotones.CleanAuditBasic()
            Me.BarraBotones.ReassignOperatingUnit()
            Code = String.Empty
            Type = Nothing
            MedicalFeesContractId = Nothing
            INDsleMedicalFeesContract.Properties.NullText = String.Empty
            CostCenterId = Nothing
            INDsleCostCenter.Properties.NullText = String.Empty
            InitialDate = Nothing
            EndDate = Nothing
            BillNumber = String.Empty
            DocumentDate = Nothing
            FilingUnitId = Nothing
            INDsleFilingUnit.Properties.NullText = String.Empty
            _supplierId = Nothing
            _supplierDistributionLine = Nothing
            SupplierTypeId = Nothing
            INDsleSupplierType.Properties.NullText = String.Empty

            ' === LIMPIAR LISTAS Y DATASOURCES ===
            _listMedicalFeesLiquidationDetail = Nothing
            _listMFLD = Nothing
            ListDeleteMedicalFeesLiquidationDetail = Nothing
            ListMedicalFeesLiquidationDetailForDeductions = Nothing
            ListMedicalFeesLiquidationDetailForGlosas = Nothing
            ListMedicalFeesContractId = Nothing
            ListCupsHomologation = Nothing

            INDgcCausation.DataSource = Nothing
            INDgcDeductions.DataSource = Nothing
            INDgcGlosas.DataSource = Nothing
            INDGcCausationPending.DataSource = Nothing

            medicalFeesLiquidation = Nothing

            ' === CONFIGURACIÓN DE CONTROLES ===
            INDsleMedicalFeesContract.Properties.ReadOnly = False
            INDsleHealthProfessional.Properties.ReadOnly = False
            INDsleType.Properties.ReadOnly = False
            INDdteEndDate.Properties.ReadOnly = False
            Me.BarraBotones.StatusRecordVisible = False

            INDlyItemMedicalFeesContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemMedicalFeesContract.AllowHide = True
            HealthProfessional = Nothing
            INDsleHealthProfessional.Properties.NullText = String.Empty
            INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemHealthProfessional.AllowHide = True
            INDlygDeductions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygGlosas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCostCenter.AllowHide = True

            DeleteBlockedRecord()
            Me._doc = Nothing
            Me.BarraBotones.EnableBarItems()
            Me.BarraBotones.DisableBarDocument()

            ctrTmp.RefreshTotalValues()
            SupplierTypeXpo = Nothing

        Finally
            ' *** REANUDAR ACTUALIZACIONES EN ORDEN INVERSO ***
            Try
                INDGcCausationPending.EndUpdate()
                INDgcGlosas.EndUpdate()
                INDgcDeductions.EndUpdate()
                INDgcCausation.EndUpdate()
                INDlyMedicalFeesLiquidation.EndUpdate()
                Me.ResumeLayout(True) ' True = forzar un layout inmediato
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error resuming layout: {ex.Message}")
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        'Se valida manualmente mientras funciona el metodo de validar controles
        Dim listErrors As New StringBuilder
        If _listMedicalFeesLiquidationDetail Is Nothing OrElse _listMedicalFeesLiquidationDetail.Count = 0 Then
            listErrors.AppendLine("No hay detalles de pagos.")
        End If
        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With medicalFeesLiquidation
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .LiquidationType = Type
            .InitialDate = InitialDate
            .EndDate = EndDate
            .BillNumber = BillNumber
            .DocumentDate = DocumentDate
            .FilingUnitId = FilingUnitId
            .SupplierTypeId = SupplierTypeId
            .SupplierId = _supplierId
            .SuppliersDistributionLineId = _supplierDistributionLine
            .OperatingUnitId = BarraBotones.OperatingUnit.Id

            If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = CostCenterId
            Else
                .CostCenterId = Nothing
            End If

            If INDlyItemMedicalFeesContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .MedicalFeesContractId = MedicalFeesContractId
            Else
                .MedicalFeesContractId = Nothing
            End If

            If INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .HealthProfessionalCode = HealthProfessional
            Else
                .HealthProfessionalCode = Nothing
            End If
        End With

        If _listMedicalFeesLiquidationDetail IsNot Nothing AndAlso _listMedicalFeesLiquidationDetail.Count > 0 Then
            For Each itemDetail In _listMedicalFeesLiquidationDetail
                medicalFeesLiquidation.MedicalFeesLiquidationDetail.Add(itemDetail)
            Next

            If _listMFLD IsNot Nothing AndAlso _listMFLD.Count > 0 Then
                For Each itemDetailDelete In _listMFLD
                    medicalFeesLiquidation.MedicalFeesLiquidationDetail.Add(itemDetailDelete)
                Next
            End If

        End If

        If ListDeleteMedicalFeesLiquidationDetail IsNot Nothing AndAlso ListDeleteMedicalFeesLiquidationDetail.Count > 0 Then
            For Each itemDelete In ListDeleteMedicalFeesLiquidationDetail
                medicalFeesLiquidation.MedicalFeesLiquidationDetail.Add(itemDelete.MarkAsDeleted)
            Next
        End If

        If ListMedicalFeesLiquidationDetailForDeductions IsNot Nothing AndAlso ListMedicalFeesLiquidationDetailForDeductions.Count > 0 Then
            For Each itemDetail In ListMedicalFeesLiquidationDetailForDeductions
                medicalFeesLiquidation.MedicalFeesLiquidationDetail.Add(itemDetail)
            Next
        End If

        If ListMedicalFeesLiquidationDetailForGlosas IsNot Nothing AndAlso ListMedicalFeesLiquidationDetailForGlosas.Count > 0 Then
            For Each itemDetail In ListMedicalFeesLiquidationDetailForGlosas
                medicalFeesLiquidation.MedicalFeesLiquidationDetail.Add(itemDetail)
            Next
        End If

        If medicalFeesLiquidation.Id > 0 Then
            medicalFeesLiquidation.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls(Optional ByVal banConsult As Boolean = True) As Task
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MMedicalFeesLiquidation(CStr(Me.Tag))
            Dim resultOperation
            If banConsult Then
                AsyncLoader(True)
                resultOperation = Await (Model.GetMedicalFeesLiquidation(INDbtnCode.Text.Trim, 0, 0)) 'Obtiene una liquidacion por código
                medicalFeesLiquidation = resultOperation.ObjectEmbbeded
                AsyncLoader(False)
            End If
            If Not medicalFeesLiquidation Is Nothing Then
                If medicalFeesLiquidation.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(medicalFeesLiquidation.Id))
                        With medicalFeesLiquidation
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                            Code = .Code
                            Type = .LiquidationType
                            banSearch = False
                            MedicalFeesContractId = .MedicalFeesContractId
                            INDsleMedicalFeesContract.Properties.NullText = .MedicalFeesContractDescription
                            banSearch = True

                            If .HealthProfessionalCode IsNot Nothing Then
                                banSearchChanged = False
                                INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDlyItemHealthProfessional.AllowHide = False
                                HealthProfessional = .HealthProfessionalCode
                                INDsleHealthProfessional.Properties.NullText = .HealthProfessionalDescription
                                banSearchChanged = True
                            End If

                            _supplierId = .SupplierId
                            _supplierDistributionLine = .SuppliersDistributionLineId
                            Await InitializeSupplierType()

                            If .CostCenterId IsNot Nothing Then
                                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDlyItemCostCenter.AllowHide = False
                            End If

                            CostCenterId = .CostCenterId
                            INDsleCostCenter.Properties.NullText = .CostCenterDescription
                            InitialDate = .InitialDate
                            banSearchChanged = False
                            EndDate = .EndDate
                            banSearchChanged = True
                            BillNumber = .BillNumber
                            DocumentDate = .DocumentDate

                            banFilingUnit = False
                            FilingUnitId = .FilingUnitId
                            banFilingUnit = True

                            banSearchSupplierType = False
                            SupplierTypeId = .SupplierTypeId
                            banSearchSupplierType = True
                            INDsleType.Properties.ReadOnly = True
                            INDsleMedicalFeesContract.Properties.ReadOnly = True
                            INDsleHealthProfessional.Properties.ReadOnly = True
                        End With

                        ActionsOnControls = True


                        Await RefreshCausationDatasourceAsync()

                        ' *** DESHABILITAR CONTROL EN UI THREAD ANTES DE INICIAR OPERACIÓN ASYNC ***
                        INDdteEndDate.Enabled = False

                        ' ✅ Iniciar carga de controles async
                        _loadControlsCts?.Cancel()
                        _loadControlsCts = New CancellationTokenSource()
                        _loadControlsTask = LoadControlsAsync(_loadControlsCts.Token)

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.medicalFeesLiquidation.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordMedicalFees With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = medicalFeesLiquidation.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        If medicalFeesLiquidation.Status <> 1 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            ReadOnlyControls(True)
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        End If
                        Me.BarraBotones.SetDocuments(medicalFeesLiquidation.Id)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                        Me.BarraBotones.PrintReport(PrintReportAction.None, medicalFeesLiquidation.Id, 0, medicalFeesLiquidation.Id)

                    End Using
                Else
                    If Me._sequense.IsManual Then
                        Await Me.NewMedicalFeesLiquidation()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
            Else
                If Me._sequense.IsManual Then
                    Await Me.NewMedicalFeesLiquidation()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    INDbtnCode.Focus()
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Versión ASÍNCRONA de RefreshCausationDatasource que NO bloquea la UI
    ''' </summary>
    Private Async Function RefreshCausationDatasourceAsync(Optional _NewParameters As Boolean = False) As Task
        Try
            ' *** MOSTRAR INDICADOR DE CARGA ***
            viewGridPayment.ShowLoadingPanel()

            ' *** EJECUTAR CONSULTAS EN BACKGROUND THREAD ***
            Dim datasourceResult = Await Task.Run(Function()
                                                      Try
                                                          If medicalFeesLiquidation.Id > 0 AndAlso Not _NewParameters Then
                                                              Return _presenter.ListViewMedicalFeesLiquidationPayByMedicalFeesLiquidationId(medicalFeesLiquidation.Id)
                                                          Else
                                                              Return _presenter.ListCausationByMedicalFeesContractIdViewXpoXpInstantFeedBackSource(ListMedicalFeesContractId, MedicalFeesContractId, InitialDate, EndDate, HealthProfessional)
                                                          End If
                                                      Catch ex As Exception
                                                          System.Diagnostics.Debug.WriteLine($"RefreshCausationDatasourceAsync Error: {ex.Message}")
                                                          Return Nothing
                                                      End Try
                                                  End Function)

            ' *** ACTUALIZAR UI EN EL HILO PRINCIPAL ***
            If datasourceResult IsNot Nothing Then
                INDgcCausation.DataSource = datasourceResult
                viewGridPayment.ExpandAllGroups()
                System.Diagnostics.Debug.WriteLine("RefreshCausationDatasourceAsync completado exitosamente")
            Else
                INDgcCausation.DataSource = Nothing
                System.Diagnostics.Debug.WriteLine("RefreshCausationDatasourceAsync - No se pudo obtener datos")
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"RefreshCausationDatasourceAsync Error general: {ex.Message}")
            INDgcCausation.DataSource = Nothing
        Finally
            viewGridPayment.HideLoadingPanel()
            viewGridPayment.ExpandAllGroups()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todos los elementos visibles de la rejilla XPO (solo elementos cargados)
    ''' </summary>
    ''' <returns>Lista de objetos XPO visibles en la rejilla</returns>
    Public Function GetVisibleItemsFromGrid() As List(Of Object)
        Try
            Dim items = New List(Of Object)()

            For i As Integer = 0 To viewGridPayment.DataRowCount - 1
                Dim obj = viewGridPayment.GetRow(i)

                If TypeOf obj Is DevExpress.Data.NotLoadedObject OrElse obj Is Nothing Then
                    Continue For
                End If

                If TypeOf obj.OriginalRow Is ViewListMedicalFeesLiquidationDetailXpo Then
                    items.Add(DirectCast(obj.OriginalRow, ViewListMedicalFeesLiquidationDetailXpo))
                End If

                If TypeOf obj.OriginalRow Is ViewListPaymentsMedicalFeesLiquidationXpo Then
                    items.Add(DirectCast(obj.OriginalRow, ViewListPaymentsMedicalFeesLiquidationXpo))
                End If
            Next

            Return items
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error obteniendo elementos visibles: {ex.Message}"
            Return New List(Of Object)()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene y sincroniza _listMedicalFeesLiquidationDetail desde la rejilla XPO (optimizada)
    ''' </summary>
    ''' <param name="forceFullSync">Si es True, sincroniza todos los elementos; si es False, solo visibles</param>
    Public Sub SyncMedicalFeesListFromGrid(Optional forceFullSync As Boolean = False)
        Try
            isLoadingData = True

            ' Solo elementos visibles (para operaciones normales)
            Dim visibleXpoItems = GetVisibleItemsFromGrid()

            ' Si no hay lista o está vacía, crear nueva
            If _listMedicalFeesLiquidationDetail Is Nothing Then
                _listMedicalFeesLiquidationDetail = New List(Of MedicalFeesLiquidationDetail)()
            End If

            ' Sincronizar solo elementos visibles
            SyncVisibleItemsToList(visibleXpoItems)

            isListSynchronized = True

            ' Refrescar totales
            ctrTmp.RefreshTotalValues()

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error sincronizando lista desde rejilla: {ex.Message}"
        Finally
            isLoadingData = False
        End Try
    End Sub

    ''' <summary>
    ''' Sincroniza elementos visibles con la lista existente (más eficiente)
    ''' </summary>
    ''' <param name="visibleXpoItems">Items visibles de la rejilla</param>
    Private Sub SyncVisibleItemsToList(visibleXpoItems As List(Of Object))
        For Each xpoItem In visibleXpoItems
            ' Buscar si ya existe en la lista
            Dim existingItem = _listMedicalFeesLiquidationDetail.FirstOrDefault(Function(x) x.MedicalFeesCausationId = xpoItem.MedicalFeesCausationId)

            If existingItem IsNot Nothing Then
                ' Actualizar item existente con datos de la rejilla
                UpdateMedicalFeesDetailFromXpo(existingItem, xpoItem)
            Else
                ' Agregar nuevo item
                Dim newItem = ConvertSingleXpoToMedicalFeesDetail(xpoItem)
                _listMedicalFeesLiquidationDetail.Add(newItem)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Actualiza un MedicalFeesLiquidationDetail específico con datos de un objeto XPO
    ''' </summary>
    ''' <param name="target">Item a actualizar</param>
    ''' <param name="xpoSource">Fuente XPO con datos actualizados</param>
    Private Sub UpdateMedicalFeesDetailFromXpo(target As MedicalFeesLiquidationDetail, xpoSource As Object) 'ViewListMedicalFeesLiquidationDetailXpo
        target.AdmissionNumber = xpoSource.AdmissionNumber
        target.PatientCode = xpoSource.PatientCode
        target.ThirdPartyDescription = xpoSource.ThirdPartyDescription
        target.ServiceOrderCode = If(TypeOf xpoSource Is ViewListMedicalFeesLiquidationDetailXpo, xpoSource.ServiceOrderCode, xpoSource.CodeServiceOrder)
        target.AmountPayable = xpoSource.AmountPayable
        target.InvoiceQuantity = xpoSource.InvoiceQuantity
        target.TotalAmountPayable = xpoSource.TotalAmountPayable
        target.MedicalFeesContractValue = xpoSource.MedicalFeesContractValue
        target.InvoiceReversal = xpoSource.InvoiceReversal
        target.StatusCausation = If(TypeOf xpoSource Is ViewListMedicalFeesLiquidationDetailXpo, xpoSource.StatusCausation, target.StatusCausation)
        target.CausationDate = xpoSource.CausationDate
        target.IPSServiceName = xpoSource.IPSServiceName
        target.ServiceDate = xpoSource.ServiceDate
        target.InvoiceDetailId = xpoSource.InvoiceDetailId
        target.InvoiceId = xpoSource.InvoiceId
    End Sub

    ''' <summary>
    ''' Convierte un solo objeto XPO a MedicalFeesLiquidationDetail
    ''' </summary>
    ''' <param name="xpoItem">Objeto XPO a convertir</param>
    ''' <returns>MedicalFeesLiquidationDetail convertido</returns>
    Private Function ConvertSingleXpoToMedicalFeesDetail(xpoItem As Object) As MedicalFeesLiquidationDetail
        Return New MedicalFeesLiquidationDetail With {
            .LiquidationType = 1,
            .MedicalFeesCausationId = xpoItem.MedicalFeesCausationId,
            .AdmissionNumber = xpoItem.AdmissionNumber,
            .PatientCode = xpoItem.PatientCode,
            .ThirdPartyDescription = xpoItem.ThirdPartyDescription,
            .ServiceOrderCode = xpoItem.ServiceOrderCode,
            .AmountPayable = xpoItem.AmountPayable,
            .InvoiceQuantity = xpoItem.InvoiceQuantity,
            .TotalAmountPayable = xpoItem.TotalAmountPayable,
            .MedicalFeesContractValue = xpoItem.MedicalFeesContractValue,
            .InvoiceReversal = xpoItem.InvoiceReversal,
            .StatusCausation = xpoItem.StatusCausation,
            .CausationDate = xpoItem.CausationDate,
            .IPSServiceName = xpoItem.IPSServiceName,
            .ServiceDate = xpoItem.ServiceDate,
            .InvoiceDetailId = xpoItem.InvoiceDetailId,
            .InvoiceId = xpoItem.InvoiceId
        }
    End Function

    ''' <summary>
    ''' Sincroniza un item específico después de edición
    ''' </summary>
    ''' <param name="causationId">ID de la causación editada</param>
    Public Sub SyncSpecificItemAfterEdit(causationId As Integer)
        Try
            If Not EnsureListIsLoaded() Then Return

            ' Buscar el item XPO actualizado en la rejilla
            Dim updatedXpoItem = FindXpoItemInGrid(causationId)
            If updatedXpoItem Is Nothing Then Return

            ' Buscar el item correspondiente en la lista
            Dim existingItem = _listMedicalFeesLiquidationDetail?.FirstOrDefault(Function(x) x.MedicalFeesCausationId = causationId)

            If existingItem IsNot Nothing Then
                ' Actualizar item existente
                UpdateMedicalFeesDetailFromXpo(existingItem, updatedXpoItem)
            Else
                ' Si no existe, agregar nuevo item
                Dim newItem = ConvertSingleXpoToMedicalFeesDetail(updatedXpoItem)
                If _listMedicalFeesLiquidationDetail Is Nothing Then
                    _listMedicalFeesLiquidationDetail = New List(Of MedicalFeesLiquidationDetail)()
                End If
                _listMedicalFeesLiquidationDetail.Add(newItem)
            End If

            ' Refrescar totales
            ctrTmp.RefreshTotalValues()

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error sincronizando item específico: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Busca un item XPO específico en la rejilla por ID de causación
    ''' </summary>
    ''' <param name="causationId">ID de causación a buscar</param>
    ''' <returns>Objeto XPO encontrado o Nothing</returns>
    Private Function FindXpoItemInGrid(causationId As Integer) As Object 'ViewListMedicalFeesLiquidationDetailXpo
        Try
            For i As Integer = 0 To viewGridPayment.DataRowCount - 1
                Dim obj = viewGridPayment.GetRow(i)
                If obj.OriginalRow.MedicalFeesCausationId = causationId Then
                    Return obj.OriginalRow
                End If
            Next
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Asegura que _listMedicalFeesLiquidationDetail esté cargada y sincronizada
    ''' </summary>
    ''' <returns>True si la lista está disponible</returns>
    Private Function EnsureListIsLoaded() As Boolean
        ' Si está cargando en background, esperar
        If isLoadingData Then
            ' Mostrar mensaje o esperar un poco
            Threading.Thread.Sleep(100)
            Return isListSynchronized
        End If

        ' Si no está sincronizada, sincronizar ahora
        If Not isListSynchronized OrElse _listMedicalFeesLiquidationDetail Is Nothing Then
            SyncMedicalFeesListFromGrid(False) ' Usar sincronización rápida
        End If

        Return _listMedicalFeesLiquidationDetail IsNot Nothing
    End Function

    ''' <summary>
    ''' Convierte los detalles que se consultan con xpo a entidad de forma asíncrona con soporte de cancelación
    ''' </summary>
    ''' <param name="cancellationToken">Token para cancelar la operación</param>
    ''' <returns>Task que representa la operación asíncrona</returns>
    ''' <remarks>Optimizada para mejor rendimiento y manejo asíncrono</remarks>
    Private Async Function ConvertXpoToEntityAsync(cancellationToken As CancellationToken) As Task
        Try
            System.Diagnostics.Debug.WriteLine("ConvertXpoToEntityAsync INICIADO")

            ' ✅ Verificar cancelación antes de iniciar
            cancellationToken.ThrowIfCancellationRequested()

            ' Ejecutar la consulta de datos en background thread
            Dim listXpo As XPCollection = Await Task.Run(Function()
                                                             Return _presenter.ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(medicalFeesLiquidation.Id)
                                                         End Function, cancellationToken)

            ' ✅ Verificar cancelación después de la consulta
            cancellationToken.ThrowIfCancellationRequested()

            If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                ' Inicializar colección si es necesario
                If medicalFeesLiquidation.MedicalFeesLiquidationDetail Is Nothing Then
                    medicalFeesLiquidation.MedicalFeesLiquidationDetail = New Domain.Entities.TrackableCollection(Of MedicalFeesLiquidationDetail)
                End If

                ' Convertir en background thread para no bloquear UI
                Dim convertedItems = Await Task.Run(Function()
                                                        Return listXpo.Cast(Of ViewListMedicalFeesLiquidationDetailXpo)().
                        Select(Function(itemXpo) CreateMedicalFeesLiquidationDetail(itemXpo)).
                        ToList()
                                                    End Function, cancellationToken)

                ' ✅ Verificar cancelación antes de actualizar UI
                cancellationToken.ThrowIfCancellationRequested()

                ' Añadir elementos convertidos a la colección (en UI thread)
                For Each item In convertedItems
                    medicalFeesLiquidation.MedicalFeesLiquidationDetail.Add(item)
                Next
            End If

            System.Diagnostics.Debug.WriteLine("✅ ConvertXpoToEntityAsync COMPLETADO")

        Catch ex As OperationCanceledException
            System.Diagnostics.Debug.WriteLine("ConvertXpoToEntityAsync CANCELADO por el usuario")
            Throw ' Re-lanzar para que el caller lo maneje
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ ConvertXpoToEntityAsync ERROR: {ex.Message}")
            Throw New InvalidOperationException($"Error convirtiendo datos XPO a entidad: {ex.Message}", ex)
        End Try
    End Function

    ''' <summary>
    ''' Función helper optimizada para crear MedicalFeesLiquidationDetail desde XPO
    ''' </summary>
    ''' <param name="itemXpo">Item XPO de origen</param>
    ''' <returns>MedicalFeesLiquidationDetail convertido</returns>
    ''' <remarks>Mapeo optimizado de propiedades</remarks>
    Private Function CreateMedicalFeesLiquidationDetail(itemXpo As ViewListMedicalFeesLiquidationDetailXpo) As MedicalFeesLiquidationDetail
        Dim mfld As New MedicalFeesLiquidationDetail With {
            .Id = itemXpo.Id,
            .LiquidationType = itemXpo.LiquidationType,
            .MedicalFeesLiquidacionId = itemXpo.MedicalFeesLiquidacionId,
            .MedicalFeesCausationId = itemXpo.MedicalFeesCausationId,
            .AdmissionNumber = itemXpo.AdmissionNumber,
            .PatientCode = itemXpo.PatientCode,
            .ThirdPartyDescription = itemXpo.ThirdPartyDescription,
            .ServiceOrderCode = itemXpo.ServiceOrderCode,
            .IPSServiceName = itemXpo.IPSServiceName,
            .AmountPayable = itemXpo.AmountPayable,
            .InvoiceQuantity = itemXpo.InvoiceQuantity,
            .TotalAmountPayable = itemXpo.TotalAmountPayable,
            .MedicalFeesContractValue = itemXpo.MedicalFeesContractValue,
            .InvoiceReversal = itemXpo.InvoiceReversal,
            .StatusCausation = itemXpo.StatusCausation,
            .CausationDate = itemXpo.CausationDate,
            .ServiceDate = itemXpo.ServiceDate
        }

        ' Inicializar tracking y marcar como unchanged
        mfld.StartTracking()
        Return mfld.MarkAsUnchanged()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMedicalFeesLiquidation() As Task
        medicalFeesLiquidation = New MedicalFeesLiquidation
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.MedicalFeesSecuenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequense.MedicalFeesSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.MedicalFeesSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Not Me._sequense.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
            'Obtiene la fecha del servidor y la postula en la fecha final
            EndDate = Me.GetDateServer
            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.StatusRecord = "1"
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        End If
    End Function

    ''' <summary>
    ''' Metodo que realiza todas las validaciones del médico
    ''' y si pasa todas las validaciones carga los pagos segun
    ''' corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ValidateHealthProfessional() As Task
        Dim _xpoHealthProfessional = DirectCast(DirectCast(viewSearchHealthProfessional.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, HealthCareProfessionalXpo)

        'Se consulta si el medico escogido esta creado como tercero
        Using model As New MThirdParty("")
            Dim ThirdParty As Domain.Entities.ThirdParty = model.GetThirdParty(_xpoHealthProfessional.CODIGONIT.TrimStart("0"))
            If ThirdParty.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), _xpoHealthProfessional.CodeName)
                _thirdPartyIdConsult = Nothing
                HealthProfessional = Nothing
                Exit Function
            End If
            _thirdPartyIdConsult = ThirdParty.Id
        End Using

        'Se valida que el medico tenga el campo de proveedor
        If _xpoHealthProfessional.GENPROVEE = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("DontSupplierHealthProfessional", NAME_MODULE), _xpoHealthProfessional.CodeName)
            INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCostCenter.AllowHide = True
            _thirdPartyIdConsult = Nothing
            HealthProfessional = Nothing
            Exit Function
        End If
        'Se valida que el medico tenga el campo de linea de distribucion
        If _xpoHealthProfessional.GENLINDIST = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("DontSupplierHealthProfessional", NAME_MODULE), _xpoHealthProfessional.CodeName)
            INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCostCenter.AllowHide = True
            _thirdPartyIdConsult = Nothing
            HealthProfessional = Nothing
            Exit Function
        End If
        'Se valida que el medico tenga el campo de fecha de ultima liquidacion
        If _xpoHealthProfessional.FECULTLIQ = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("DontDateEndLiquidationHealthProfessional", NAME_MODULE), _xpoHealthProfessional.CodeName)
            _thirdPartyIdConsult = Nothing
            HealthProfessional = Nothing
            Exit Function
        End If

        Dim resultValidate = Await ValidateContractAssociatedToHealthProfessional(_xpoHealthProfessional.CODPROSAL)
        If resultValidate.StateResult = False Then
            Mensaje(EeventViewerImages.Advertencia) = resultValidate.Message
            _thirdPartyIdConsult = Nothing
            HealthProfessional = Nothing
            Exit Function
        End If
        ListMedicalFeesContractId = resultValidate.ObjectEmbbeded

        'Se valida que la cuenta contable que esta asociada a la linea de distribucion maneje centro costo
        Await ValidateHandlesCostCenterSupplierDistributionLine(_xpoHealthProfessional.GENLINDIST)

        _supplierId = _xpoHealthProfessional.GENPROVEE
        _supplierDistributionLine = _xpoHealthProfessional.GENLINDIST
        Await InitializeSupplierType()
        InitialDate = DateAdd(DateInterval.Second, 1, _xpoHealthProfessional.FECULTLIQ)
        'Se consulta el proveedor que viene amarrado al medico para sacar el id del tercero
        Using model As New MSupplier("")
            Dim supplier As Domain.Entities.Supplier = model.GetSupplierById(_xpoHealthProfessional.GENPROVEE)
            If supplier.Id > 0 Then
                ThirdPartyId = supplier.IdThirdParty
            End If
        End Using
        Await ValidateExistLiquidation(2)
        Await LoadCausationPendingsAsync(_xpoHealthProfessional.CODPROSAL)

    End Function

    ''' <summary>
    ''' Versión asíncrona de LoadCausationPendings que NO bloquea la UI
    ''' </summary>
    Private Async Function LoadCausationPendingsAsync(performsHealthProfessionalCode As String) As Task
        Try
            If String.IsNullOrEmpty(performsHealthProfessionalCode) Then Return

            ' *** MOSTRAR INDICADOR DE CARGA ***
            INDGcCausationPending.UseWaitCursor = True
            INDGvCausationPending.ShowLoadingPanel()

            ' *** EJECUTAR CONSULTA EN BACKGROUND THREAD ***
            Dim causationPendingData = Await Task.Run(Function()
                                                          Try
                                                              Return XpoServiceEx.Instance(indigo.TransactionalContainer).MedicalFeesService.ListXPInstantFeedbackSource(Of CausationPendingXpo)(filter:=$"PerformsHealthProfessionalCode='{performsHealthProfessionalCode}'")
                                                          Catch
                                                              Return Nothing
                                                          End Try
                                                      End Function)

            ' *** ACTUALIZAR UI EN EL HILO PRINCIPAL ***
            If causationPendingData IsNot Nothing Then
                INDGcCausationPending.DataSource = causationPendingData
                INDGcCausationPending.RefreshDataSource()
                System.Diagnostics.Debug.WriteLine($"LoadCausationPendingsAsync completado")
            Else
                INDGcCausationPending.DataSource = Nothing
                INDGcCausationPending.RefreshDataSource()
                System.Diagnostics.Debug.WriteLine("LoadCausationPendingsAsync - No se encontraron datos")
            End If

        Catch ex As Exception
            Debug.WriteLine($"LoadCausationPendingsAsync Error: {ex.Message}")
            ' En caso de error, limpiar la rejilla
            INDGcCausationPending.DataSource = Nothing
            INDGcCausationPending.RefreshDataSource()
        Finally
            ' *** QUITAR INDICADOR DE CARGA ***
            INDGvCausationPending.HideLoadingPanel()
            INDGcCausationPending.UseWaitCursor = False
        End Try
    End Function

    ''' <summary>
    ''' Versión asíncrona de LoadCausationPendingsByContract que NO bloquea la UI
    ''' </summary>
    Private Async Function LoadCausationPendingsByContractAsync(contractId As Integer) As Task
        Try
            If contractId <= 0 Then Return

            ' *** MOSTRAR INDICADOR DE CARGA ***
            INDGcCausationPending.UseWaitCursor = True
            INDGvCausationPending.ShowLoadingPanel()

            ' *** EJECUTAR CONSULTAS EN BACKGROUND THREAD ***
            Dim causationPendingData = Await Task.Run(Function()
                                                          Try
                                                              ' Consulta 1: Obtener contrato
                                                              Dim contract = XpoServiceEx.Instance(indigo.TransactionalContainer).MedicalFeesService.GetCollectionAsList(Of MedicalFeesContractXpo)(Nothing, $"Id={contractId}")

                                                              If contract?.Count > 0 Then
                                                                  ' Consulta 2: Obtener códigos de profesionales
                                                                  Dim healthProfessionals = contract(0).HealthProfessionalContractXpo.Select(Function(m) m.HealthProfessionalCode)?.ToList()

                                                                  If healthProfessionals IsNot Nothing AndAlso healthProfessionals.Any() Then
                                                                      ' Consulta 3: Obtener causaciones pendientes
                                                                      Dim causations = XpoServiceEx.Instance(indigo.TransactionalContainer).MedicalFeesService.ListXPInstantFeedbackSource(Of CausationPendingXpo)(filter:=$"PerformsHealthProfessionalCode In ('{String.Join("','", healthProfessionals)}')")
                                                                      Return causations
                                                                  End If
                                                              End If

                                                              Return Nothing
                                                          Catch
                                                              Return Nothing
                                                          End Try
                                                      End Function)

            ' *** ACTUALIZAR UI EN EL HILO PRINCIPAL ***
            If causationPendingData IsNot Nothing Then
                INDGcCausationPending.DataSource = causationPendingData
                INDGcCausationPending.RefreshDataSource()
                System.Diagnostics.Debug.WriteLine($"LoadCausationPendingsByContractAsync completado")
            Else
                INDGcCausationPending.DataSource = Nothing
                INDGcCausationPending.RefreshDataSource()
                System.Diagnostics.Debug.WriteLine("LoadCausationPendingsByContractAsync - No se encontraron datos")
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"LoadCausationPendingsByContractAsync Error: {ex.Message}")
            ' En caso de error, limpiar la rejilla
            INDGcCausationPending.DataSource = Nothing
            INDGcCausationPending.RefreshDataSource()
        Finally
            ' *** QUITAR INDICADOR DE CARGA ***
            INDGvCausationPending.HideLoadingPanel()
            INDGcCausationPending.UseWaitCursor = False
        End Try
    End Function

    ''' <summary>
    ''' Se valida que el médico seleccionado tenga asociado contratos,
    ''' y ademas que los contratos asociados sean de tipo estandar.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ValidateContractAssociatedToHealthProfessional(codeHealthProfessional As String) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Integer)))
        'Se valida que el médico seleccionado tenga asociado contratos,
        'y ademas que los contratos asociados sean de tipo estandar.
        Using model As New MMedicalFeesLiquidation(Me.Tag)
            Dim result As ActionResult(Of List(Of HealthProfessionalContract)) = Await model.ListHealthProfessionalContractWithTypeStandard(codeHealthProfessional)
            If result.StateResult = False Then
                Return New ActionResult(Of List(Of Integer)) With {.StateResult = False, .Message = result.Message}
            End If
            'Se capturan todos los id de medicalFeesContract para la consulta con el listado
            Return New ActionResult(Of List(Of Integer)) With {.StateResult = True, .ObjectEmbbeded = (From r In result.ObjectEmbbeded Select r.MedicalFeesContractId).ToList}
        End Using
    End Function

    ''' <summary>
    ''' Metodo que valida que la cuenta contable que esta asociada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ValidateHandlesCostCenterSupplierDistributionLine(supplierDistributionLineId As Integer) As Task
        Using model As New MMedicalFeesLiquidation(Tag)
            Dim result As ActionResult(Of MedicalFeesLiquidation) = Await model.ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                _thirdPartyIdConsult = Nothing
                HealthProfessional = Nothing
                Exit Function
            End If
            If result.StateResultAux Then
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemCostCenter.AllowHide = False
            Else
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCostCenter.AllowHide = True
            End If
        End Using
    End Function

    ''' <summary>
    ''' Se valida que el médico seleccionado tenga asociado contratos,
    ''' y ademas que los contratos asociados sean de tipo estandar.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ContractAssociatedToHealthProfessional(codeHealthProfessional As String) As Task(Of ActionResult(Of List(Of Integer)))
        'Se valida que el médico seleccionado tenga asociado contratos,
        'y ademas que los contratos asociados sean de tipo estandar.
        Using model As New MMedicalFeesLiquidation(Me.Tag)
            Dim result As ActionResult(Of List(Of HealthProfessionalContract)) = Await model.ContractWithTypeStandardAsync(codeHealthProfessional)
            If result.StateResult = False Then
                Return New ActionResult(Of List(Of Integer)) With {.StateResult = False, .Message = result.Message}
            End If
            'Se capturan todos los id de medicalFeesContract para la consulta con el listado
            Return New ActionResult(Of List(Of Integer)) With {.StateResult = True, .ObjectEmbbeded = (From r In result.ObjectEmbbeded Select r.MedicalFeesContractId).ToList}
        End Using
    End Function

#End Region

#Region "Events"
    ''' <summary>
    ''' Acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        EditDetailCausation()
    End Sub

    ''' <summary>
    ''' Edita un detalle de la causación (optimizada con sincronización inteligente)
    ''' </summary>
    Private Sub EditDetailCausation()
        Try
            ' Verificar que la lista esté cargada antes de proceder
            If Not EnsureListIsLoaded() Then
                Mensaje(EeventViewerImages.Advertencia) = "Los datos aún se están cargando. Intente nuevamente en unos momentos."
                Return
            End If

            Dim obj = viewGridPayment.GetFocusedRow()

            If TypeOf obj.OriginalRow Is ViewListMedicalFeesLiquidationDetailXpo Then
                obj = DirectCast(obj.OriginalRow, ViewListMedicalFeesLiquidationDetailXpo)

            ElseIf TypeOf obj.OriginalRow Is ViewListPaymentsMedicalFeesLiquidationXpo Then
                obj = DirectCast(obj.OriginalRow, ViewListPaymentsMedicalFeesLiquidationXpo)
            End If

            If obj Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay ningún elemento seleccionado para editar."
                Return
            End If

            Dim causationId = obj.MedicalFeesCausationId

            Using frm As New FrmMedicalFeesCausation()
                frm.StartPosition = FormStartPosition.CenterParent
                AddHandler frm.Shown, Sub()
                                          frm.LoadData(obj.AdmissionNumber, obj.InvoiceId, obj.InvoiceDetailId)
                                      End Sub

                ' Manejar el cierre del formulario para sincronizar cambios
                AddHandler frm.FormClosed, Sub(sender, e)
                                               ' Sincronizar el item específico editado
                                               SyncSpecificItemAfterEdit(causationId)
                                           End Sub

                Dim transaparent As New FrmTransparent(frm, False)
                transaparent.ShowDialog(Me)
            End Using

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error editando detalle de causación: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Evento que maneja el clic en el botón "Process" de la rejilla de pendientes
    ''' Procesa una causación de forma asíncrona (Quirúrgica o No Quirúrgica)
    ''' </summary>
    ''' <param name="sender">Control que disparó el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    ''' <remarks>
    ''' Maneja correctamente los proxies thread-safe de XPInstantFeedbackSource
    ''' </remarks>
    Private Async Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Try
            ' *** PASO 1: Obtener la fila enfocada de forma segura ***
            Dim focusedRow = INDGvCausationPending.GetFocusedRow()
            If focusedRow Is Nothing Then
                System.Diagnostics.Debug.WriteLine("⚠️ No hay fila enfocada")
                Return
            End If

            ' *** PASO 2: Validar que no sea NotLoadedObject ***
            If TypeOf focusedRow Is DevExpress.Data.NotLoadedObject Then
                Mensaje(EeventViewerImages.Advertencia) = "Por favor espere a que se cargue el registro"
                Return
            End If

            ' *** PASO 3: Extraer el objeto CausationPendingXpo del proxy ***
            Dim causation As CausationPendingXpo = Nothing

            If TypeOf focusedRow Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                ' XPInstantFeedbackSource usa un proxy - extraer el objeto original
                Dim proxy = DirectCast(focusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If proxy.OriginalRow IsNot Nothing Then
                    causation = TryCast(proxy.OriginalRow, CausationPendingXpo)
                End If
            Else
                ' Objeto directo (fallback)
                causation = TryCast(focusedRow, CausationPendingXpo)
            End If

            ' *** PASO 4: Validar que se obtuvo el objeto correctamente ***
            If causation Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo obtener la causación seleccionada"
                System.Diagnostics.Debug.WriteLine("❌ Error: No se pudo extraer CausationPendingXpo del proxy")
                Return
            End If

            ' *** PASO 5: Mostrar loading panel ***
            INDGvCausationPending.ShowLoadingPanel()
            INDGcCausationPending.UseWaitCursor = True


            Try
                ' *** PASO 6: Procesar causación según tipo (Quirúrgico o No Quirúrgico) ***
                Dim result = Await ProcessCausationAsync(causation)

                ' *** PASO 7: Manejar resultado de la causación ***
                Await HandleCausationResultAsync(result, causation)

            Finally
                INDGcCausationPending.UseWaitCursor = False
                INDGvCausationPending.HideLoadingPanel()
            End Try

        Catch ex As InvalidCastException
            ' Error específico de conversión de tipo
            Mensaje(EeventViewerImages.MensajeError) = "Error al obtener los datos de la causación"
            System.Diagnostics.Debug.WriteLine($"❌ InvalidCastException: {ex.Message}")
        Catch ex As NullReferenceException
            ' Error específico de referencia nula
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado una causación válida"
            System.Diagnostics.Debug.WriteLine($"❌ NullReferenceException: {ex.Message}")
        Catch ex As Exception
            ' Error genérico
            Mensaje(EeventViewerImages.MensajeError) = $"Error procesando causación: {ex.Message}"
            System.Diagnostics.Debug.WriteLine($"❌ Exception: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ' Limpiar y cancelar operaciones asíncronas
        Try
            _listPaymentsCts?.Cancel()
            _listPaymentsCts?.Dispose()
            _loadControlsCts?.Cancel()
            _loadControlsCts?.Dispose()
            _globalCts?.Cancel()
            _globalCts?.Dispose()
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error disposing CancellationTokenSources: {ex.Message}")
        End Try

        ctrTmp = Nothing
        ListCausation = Nothing
        _listPaymentsCts = Nothing
        _loadControlsCts = Nothing
        _globalCts = Nothing
        _listPaymentsTask = Nothing
        _loadControlsTask = Nothing
        banFilingUnit = Nothing
        _presenter = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        _listMedicalFeesLiquidationDetail = Nothing
        ListDeleteMedicalFeesLiquidationDetail = Nothing
        ListMedicalFeesLiquidationDetailForDeductions = Nothing
        ListMedicalFeesLiquidationDetailForGlosas = Nothing
        banSearchSupplierType = Nothing
        medicalFeesLiquidation = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        banSearch = Nothing
        _listMFLD = Nothing
        banSearchChanged = Nothing
        _supplierId = Nothing
        _supplierDistributionLine = Nothing
        _thirdPartyIdConsult = Nothing
        SearchMode = Nothing
        ListType = Nothing
        ListMedicalFeesContractId = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMedicalFeesLiquidation, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PMedicalFeesLiquidation(Me)
        _presenter.GetSequense()
        LoadStatus()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDgcCausation)
        IndigoGridControl1.RefreshGrid(INDgcDeductions)
        INDsleHealthProfessional.Properties.Buttons.Item(1).Visible = False
        InitializeSearch()
        'InitializeRepository()
        AddColumnActions()
        SearchMode = False
        IndigoGridView1.MoreInfoColunmns(viewGridPayment)

        viewGridPayment.OptionsView.ShowFooter = True
        viewGridDeductions.OptionsView.ShowFooter = True
        viewGridGlosas.OptionsView.ShowFooter = True
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.NewMedicalFeesLiquidation()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesLiquidation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If Type IsNot Nothing Then
            If Type = 1 Then 'Agremiación
                INDlyItemMedicalFeesContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemMedicalFeesContract.AllowHide = False
                INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemHealthProfessional.AllowHide = True
            Else 'Médico
                INDlyItemMedicalFeesContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemMedicalFeesContract.AllowHide = True
                INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemHealthProfessional.AllowHide = False
            End If
        Else
            INDlyItemMedicalFeesContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemMedicalFeesContract.AllowHide = True
            INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemHealthProfessional.AllowHide = True
        End If
        HealthProfessional = Nothing
        INDsleHealthProfessional.Properties.NullText = String.Empty
        MedicalFeesContractId = Nothing
        INDsleMedicalFeesContract.Properties.NullText = String.Empty
        InitialDate = Nothing
        _listMedicalFeesLiquidationDetail = Nothing
        ListMedicalFeesLiquidationDetailForDeductions = Nothing
        ListMedicalFeesLiquidationDetailForGlosas = Nothing
        INDgcCausation.DataSource = Nothing
        INDgcDeductions.DataSource = Nothing
        INDgcGlosas.DataSource = Nothing
        INDGcCausationPending.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de causacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleMedicalFeesContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMedicalFeesContract.EditValueChanged
        If MedicalFeesContractId IsNot Nothing AndAlso banSearch = True Then
            Dim _xpoMedicalFeesContract = Await _presenter.GetMedicalFeesContractAsync(MedicalFeesContractId)
            If _xpoMedicalFeesContract.SupplierId IsNot Nothing Then
                _supplierId = _xpoMedicalFeesContract.SupplierId.Id
                _supplierDistributionLine = _xpoMedicalFeesContract.SupplierDistributionLineId
                Await InitializeSupplierType()
            End If

            If _xpoMedicalFeesContract.ContractType = 1 Then 'Estandar
                INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemHealthProfessional.AllowHide = False
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCostCenter.AllowHide = True
                Await ValidateLoadCausation()
            Else 'Agremiaciones
                InitialDate = DateAdd(DateInterval.Second, 1, _xpoMedicalFeesContract.LastLiquidationDate)
                INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemHealthProfessional.AllowHide = True
                Await ValidateHandlesCostCenterSupplierDistributionLine(_supplierDistributionLine)
                ThirdPartyId = Nothing
                _thirdPartyIdConsult = Nothing
                Await ValidateExistLiquidation(1)
            End If

            Await LoadCausationPendingsByContractAsync(CInt(INDsleMedicalFeesContract.EditValue))
        Else
            banSearch = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_EditValueChanged_1(sender As Object, e As EventArgs) Handles INDsleSupplierType.EditValueChanged
        If SupplierTypeId IsNot Nothing AndAlso banSearchSupplierType = True Then
            INDsleSupplierType.ValidateSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged
        If InitialDate IsNot Nothing Then
            INDdteEndDate.Properties.MinValue = InitialDate
            INDdteEndDate.Properties.MaxValue = GetDateServer()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDdteEndDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteEndDate.EditValueChanged
        If EndDate IsNot Nothing Then
            INDdteEndDate.Properties.MaxValue = GetDateServer()
        End If
        If EndDate IsNot Nothing AndAlso banSearchChanged = True Then

            If medicalFeesLiquidation.Id > 0 AndAlso INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Dim result = Await ContractAssociatedToHealthProfessional(medicalFeesLiquidation.HealthProfessionalCode)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
                ListMedicalFeesContractId = result.ObjectEmbbeded
            End If

            Await ValidateLoadCausation()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control del medico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHealthProfessional.EditValueChanged
        If HealthProfessional IsNot Nothing AndAlso banSearchChanged = True Then
            Await ValidateHealthProfessional()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad radicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnit.EditValueChanged
        If FilingUnitId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnit.ValidateFilingUnit()
        End If
    End Sub
#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de contratos profesionales de la salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMedicalFeesContract_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMedicalFeesContract.QueryPopUp
        If MedicalFeesContractXpo Is Nothing Then
            _presenter.InitializeMedicalFeesContract()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthProfessional_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthProfessional.QueryPopUp
        If HealthProfessionalXpo Is Nothing Then
            _presenter.InitializeHealthProfessional()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            _presenter.InitializeCostCenter()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleMedicalFeesContract_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMedicalFeesContract.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmMedicalFeesContract With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeMedicalFeesContract()
            If MedicalFeesContractId IsNot Nothing Then
                Using model As New MBusqueda
                    Dim _xpoMFC = model.ConsultarEntidades(eDataSource.GetMedicalFeesContractById, MedicalFeesContractId)
                    If _xpoMFC IsNot Nothing Then
                        If _xpoMFC(0).ContractType = 1 Then 'Estandar
                            HealthProfessionalXpo = Nothing
                            INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDlyItemHealthProfessional.AllowHide = False
                            INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDlyItemCostCenter.AllowHide = True
                            _presenter.InitializeHealthProfessional(MedicalFeesContractId)
                            HealthProfessional = Nothing
                            INDsleHealthProfessional.Properties.NullText = String.Empty
                        Else 'Agremiaciones
                            INDlyItemHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDlyItemHealthProfessional.AllowHide = True
                            Await ValidateHandlesCostCenterSupplierDistributionLine(_xpoMFC(0).SupplierDistributionLineId)
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMedicalFeesCausation_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmMedicalFeesCausation With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeMedicalFeesCausation(MedicalFeesContractId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeCostCenter()
        End If
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de las rejillas de pagos, deducciones y glosas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewGridPayment_DataSourceChanged(sender As Object, e As EventArgs) Handles viewGridDeductions.DataSourceChanged, viewGridGlosas.DataSourceChanged
        ctrTmp.RefreshTotalValues()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form por primera vez
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmMedicalFeesLiquidation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MFilingUnit(Tag)
            Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
            Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
            FilingUnitXpo = listFilingUnit
        End Using
        INDbtnCode.Focus()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        medicalFeesLiquidation.Status = 1
        Guardar()
    End Sub

    Private Async Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        ' ✅ SOLUCIÓN CON TASK/ASYNC PURO - Sin BackgroundWorker ni banderas custom
        Try
            System.Diagnostics.Debug.WriteLine("=== DESHACER INICIADO ===")

            ' *** DETECTAR Y CANCELAR OPERACIONES ASYNC EN PROGRESO ***
            Dim hasAsyncOperations = IsListPaymentsRunning() OrElse IsLoadControlsRunning()

            If hasAsyncOperations Then
                System.Diagnostics.Debug.WriteLine("🔄 Operaciones async detectadas - Cancelando...")

                ' ✅ Cancelar todas las operaciones con CancellationToken
                CancelAllAsyncOperations()

                ' ✅ Esperar a que terminen o se cancelen (con timeout)
                Dim waitTask = Task.WhenAny(
                    WaitForAllAsyncOperations(),
                    Task.Delay(2000) ' Timeout de 2 segundos
                )

                Await waitTask

                System.Diagnostics.Debug.WriteLine("✅ Operaciones canceladas o timeout alcanzado")
            Else
                System.Diagnostics.Debug.WriteLine("✅ No hay operaciones async en progreso - Deshacer inmediato")
            End If

            SearchMode = False
            Deshacer()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"❌ Error durante deshacer: {ex.Message}")
            ' Continuar con deshacer de todas maneras
            SearchMode = False
            Deshacer()
        Finally
            System.Diagnostics.Debug.WriteLine("=== DESHACER COMPLETADO ===")
        End Try
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
        medicalFeesLiquidation.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, medicalFeesLiquidation.Id, 0, medicalFeesLiquidation.Id)
    End Sub


    ''' <summary>
    ''' Barras the botones_click guardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        medicalFeesLiquidation.Status = 2
        GuardarConfirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_click actualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        medicalFeesLiquidation.Status = 2
        GuardarConfirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_click Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Await Anular()
    End Sub

    ''' <summary>
    ''' Evento para manejar columnas no vinculadas (unbound) en la rejilla de pendientes
    ''' NOTA: Necesario con XPInstantFeedbackSource para propiedades [NonPersistent]
    ''' </summary>
    ''' <remarks>
    ''' Maneja la columna IPSServiceDescription (descripción del servicio IPS)
    ''' que se calcula deserializando el campo Data del objeto CausationPendingXpo
    ''' </remarks>
    Private Sub INDGvCausationPending_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCausationPending.CustomUnboundColumnData
        Try
            ' Solo procesar la columna IPSServiceDescription en modo GET
            If e.Column.FieldName <> "IPSServiceDescription" OrElse Not e.IsGetData Then
                Return
            End If

            ' Verificar que el objeto no sea NotLoadedObject (específico de XPInstantFeedbackSource)
            If TypeOf e.Row Is DevExpress.Data.NotLoadedObject Then
                e.Value = "Cargando..."
                Return
            End If

            ' Intentar obtener el objeto CausationPendingXpo
            Dim pending As CausationPendingXpo = Nothing

            If TypeOf e.Row Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                ' XPInstantFeedbackSource usa un proxy - extraer el objeto original
                Dim proxy = DirectCast(e.Row, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If proxy.OriginalRow IsNot Nothing Then
                    pending = TryCast(proxy.OriginalRow, CausationPendingXpo)
                End If
            Else
                ' Objeto directo (fallback)
                pending = TryCast(e.Row, CausationPendingXpo)
            End If

            ' Asignar el valor de la descripción
            If pending IsNot Nothing Then
                e.Value = pending.IPSServiceDescription
            Else
                e.Value = String.Empty
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error en CustomUnboundColumnData: {ex.Message}")
            e.Value = "[Error]"
        End Try
    End Sub

    ''' <summary>
    ''' Procesa una causación de forma asíncrona (Quirúrgica o No Quirúrgica)
    ''' </summary>
    ''' <param name="causation">Causación a procesar</param>
    ''' <returns>Resultado de la operación</returns>
    Private Async Function ProcessCausationAsync(causation As CausationPendingXpo) As Task(Of ActionResult(Of List(Of CupsHomologation)))
        Using model As New MMedicalFeesCausation("")
            If causation.IsQx Then
                ' Procesar causación quirúrgica - VERSIÓN ASÍNCRONA
                Dim invoiceDetail = Utils.DeserializeJsonToEntity(Of ViewListSurgicalAndPackage)(causation.Data)
                invoiceDetail.CausationPendingId = causation.Id

                ' *** ENVOLVER OPERACIONES SÍNCRONAS EN TASK.RUN ***
                Return Await ProcessHomologationsIfNeeded(
                    Function() Task.Run(Function() model.CauseInvoiceQx(invoiceDetail)),
                    Function() Task.Run(Function() model.CauseInvoiceQx(invoiceDetail, ListCupsHomologation))
                )
            Else
                ' Procesar causación no quirúrgica - VERSIÓN ASÍNCRONA
                Dim invoiceDetail = Utils.DeserializeJsonToEntity(Of ViewListNoSurgical)(causation.Data)

                ' *** ENVOLVER OPERACIONES SÍNCRONAS EN TASK.RUN ***
                Return Await ProcessHomologationsIfNeeded(
                    Function() Task.Run(Function() model.CauseInvoice(invoiceDetail)),
                    Function() Task.Run(Function() model.CauseInvoice(invoiceDetail, ListCupsHomologation))
                )
            End If
        End Using
    End Function

    ''' <summary>
    ''' Procesa homologaciones si son necesarias - VERSIÓN ASÍNCRONA
    ''' </summary>
    ''' <param name="primaryAction">Acción principal a ejecutar</param>
    ''' <param name="homologationAction">Acción con homologaciones si es necesario</param>
    ''' <returns>Resultado de la operación</returns>
    Private Async Function ProcessHomologationsIfNeeded(primaryAction As Func(Of Task(Of ActionResult(Of List(Of CupsHomologation)))), homologationAction As Func(Of Task(Of ActionResult(Of List(Of CupsHomologation))))) As Task(Of ActionResult(Of List(Of CupsHomologation)))
        Dim result = Await primaryAction()

        If Not result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
            ' *** MOSTRAR POPUP DE HOMOLOGACIONES DE FORMA ASÍNCRONA ***
            Await OpenHomologationsAsync(result.ObjectEmbbeded)
            result = Await homologationAction()
        End If

        Return result
    End Function

    ''' <summary>
    ''' Maneja el resultado de una causación procesada
    ''' </summary>
    ''' <param name="result">Resultado de la causación</param>
    ''' <param name="causation">Información de la causación procesada</param>
    Private Async Function HandleCausationResultAsync(result As ActionResult(Of List(Of CupsHomologation)), causation As CausationPendingXpo) As Task
        Try
            If result.StateResult Then
                ' ✅ Causación exitosa - El item desaparecerá al refrescar
                Await HandleSuccessfulCausationAsync(causation)

            ElseIf result.StatusCode = eStatusResult.WARNING Then
                ' ⚠️ Advertencia - Mostrar mensaje
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Await RefreshCausationPendingsAsync()

            Else
                ' ❌ Error - Mostrar mensaje
                If Not String.IsNullOrEmpty(result.Message) Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Await RefreshCausationPendingsAsync()
                End If
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"HandleCausationResultAsync Error: {ex.Message}")
            Mensaje(EeventViewerImages.MensajeError) = $"Error procesando resultado de causación: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' Maneja una causación exitosa (actualiza datos y refrescos) - VERSIÓN REALMENTE ASÍNCRONA
    ''' </summary>
    ''' <param name="causation">Información de la causación procesada</param>
    Private Async Function HandleSuccessfulCausationAsync(causation As CausationPendingXpo) As Task
        Try
            Mensaje(EeventViewerImages.Informacion) = "Ítem causado correctamente"

            ' *** OBTENER FECHA DEL SERVIDOR DE FORMA ASÍNCRONA ***
            Dim serverDate = Await GetDateServerAsync()
            INDdteEndDate.EditValue = serverDate

            ' *** REFRESCAR DATASOURCE DE FORMA ASÍNCRONA ***
            Await RefreshCausationDatasourceAsync(True)

            ' *** EJECUTAR CARGA DE PAGOS EN PARALELO ***
            Dim loadPaymentsTask = Task.Run(Async Function()
                                                If Not IsListPaymentsRunning() Then
                                                    _listPaymentsCts?.Cancel()
                                                    _listPaymentsCts = New CancellationTokenSource()
                                                    _listPaymentsTask = LoadListPaymentsAsync(_listPaymentsCts.Token)
                                                    Await _listPaymentsTask
                                                End If
                                            End Function)

            Dim refreshTask = RefreshCausationPendingsAsync()

            ' *** ESPERAR QUE AMBAS TAREAS TERMINEN ***
            Await Task.WhenAll(loadPaymentsTask, refreshTask)

            System.Diagnostics.Debug.WriteLine("HandleSuccessfulCausationAsync completado exitosamente")

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"HandleSuccessfulCausationAsync Error: {ex.Message}")
            Mensaje(EeventViewerImages.Advertencia) = $"Error al procesar causación exitosa: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' Versión asíncrona de GetDateServer - obtiene la fecha del servidor sin bloquear la UI
    ''' </summary>
    Private Async Function GetDateServerAsync() As Task(Of DateTime)
        Try
            Return Await Task.Run(Function() GetDateServer())
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"GetDateServerAsync Error: {ex.Message}")
            ' Fallback a fecha local si hay problema con el servidor
            Return DateTime.Now
        End Try
    End Function

    ''' <summary>
    ''' Versión REALMENTE asíncrona que no bloquea la UI - Actualiza las causaciones pendientes según el tipo
    ''' </summary>
    Private Async Function RefreshCausationPendingsAsync() As Task
        Try
            If Type = 1 Then
                ' Modo contrato - VERSIÓN ASÍNCRONA
                Await LoadCausationPendingsByContractAsync(CInt(INDsleMedicalFeesContract.EditValue))
            Else
                ' Modo médico - VERSIÓN ASÍNCRONA  
                Dim healthProfessional = GetCurrentHealthProfessional()
                If healthProfessional IsNot Nothing Then
                    Await LoadCausationPendingsAsync(healthProfessional.CODPROSAL)
                End If
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"Error actualizando causaciones pendientes: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el profesional de la salud actual de forma optimizada
    ''' </summary>
    ''' <returns>HealthCareProfessionalXpo o Nothing si no se encuentra</returns>
    ''' <remarks>Elimina 4x duplicación de código para obtener el profesional</remarks>
    Private Function GetCurrentHealthProfessional() As HealthCareProfessionalXpo
        Try
            If TypeOf (viewSearchHealthProfessional.GetFocusedRow) Is DevExpress.Data.NotLoadedObject Then
                ' Objeto no cargado - obtener por código
                Return XpoServiceEx.Instance(indigo.TransactionalContainer).CrystalService.GetXPOObject(Of HealthCareProfessionalXpo)($"CODPROSAL='{INDsleHealthProfessional.EditValue}'")
            Else
                ' Objeto ya cargado - convertir directamente
                Return DirectCast(DirectCast(viewSearchHealthProfessional.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, HealthCareProfessionalXpo)
            End If
        Catch ex As Exception
            ' Si falla, devolver Nothing y loggear error
            Mensaje(EeventViewerImages.Advertencia) = $"Error obteniendo profesional: {ex.Message}"
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Versión ASÍNCRONA de OpenHomologations que NO bloquea la UI
    ''' </summary>
    Private Function OpenHomologationsAsync(ListCupsHomologation As List(Of CupsHomologation)) As Task
        Dim tcs As New TaskCompletionSource(Of Boolean)()

        Try
            ' *** CREAR FORMULARIO EN EL HILO PRINCIPAL ***
            Dim Formulario As New PopupHomologation()
            Formulario.OnlySelectedOne = True
            Formulario.StartPosition = FormStartPosition.CenterParent
            Formulario.ListHomologation = ListCupsHomologation

            ' *** CONFIGURAR EVENTO DE HOMOLOGACIÓN ***
            AddHandler Formulario.SetHomologation, AddressOf ReturnSetHomologation

            ' *** CONFIGURAR EVENTOS PARA COMPLETAR TAREA ***
            AddHandler Formulario.FormClosed, Sub(sender, e)
                Try
                    tcs.SetResult(True)
                Catch ex As Exception
                    tcs.SetException(ex)
                End Try
            End Sub

            ' *** MOSTRAR FORMULARIO NO MODAL ***
            Dim transParent As New FrmTransparent(Formulario, False)
            transParent.Show(Me) ' ← ✅ Show() en lugar de ShowDialog() - NO BLOQUEA

            System.Diagnostics.Debug.WriteLine("OpenHomologationsAsync - Formulario mostrado de forma no bloqueante")

        Catch ex As Exception
            tcs.SetException(ex)
            System.Diagnostics.Debug.WriteLine($"OpenHomologationsAsync Error: {ex.Message}")
        End Try

        Return tcs.Task
    End Function

    ''' <summary>
    ''' metodo que recibe la homologacion seleccionada en el popup cuando un servicio tiene mas de una
    ''' </summary>
    ''' <param name="Senders"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnSetHomologation(Senders As Object, e As SetHomologationEventArgs)
        ListCupsHomologation = Nothing
        ListCupsHomologation = e.ListHomologations
    End Sub

    Private Sub INDGvCausationPending_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvCausationPending.CustomColumnDisplayText
        If e.Column.Name = INDColIsQx.Name Then
            If e.Value Then
                e.DisplayText = "Quirúrgico"
            Else
                e.DisplayText = "No Quirúrgico"
            End If
        End If
    End Sub
#End Region

End Class