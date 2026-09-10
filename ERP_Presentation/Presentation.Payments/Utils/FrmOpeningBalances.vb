'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports System.Text
Imports DevExpress.XtraEditors
Imports Domain.Entities.Service
Imports System.Windows.Forms
Imports Presentation.Maintenance
#End Region

Public Class FrmOpeningBalances
    Implements IOpeningBalance, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Establece el datasource de la cuenta contable del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOpeningBalance.AccountXpo
        Get
            Return INDsleMainAccountAdvance.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccountAdvance.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitId As Integer? Implements IOpeningBalance.FilingUnitId
        Get
            Return INDsleFilingUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitXpo As List(Of FilingUnit) Implements IOpeningBalance.FilingUnitXpo
        Get
            Return INDsleFilingUnit.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDsleFilingUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del servicio de periodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServicePeriodDate As Date? Implements IOpeningBalance.ServicePeriodDate
        Get
            Return INDdteServicePeriodDate.EditValue
        End Get
        Set(value As Date?)
            INDdteServicePeriodDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeId As Integer? Implements IOpeningBalance.SupplierTypeId
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
    Public Property SupplierTypeXpo As List(Of SupplierType) Implements IOpeningBalance.SupplierTypeXpo
        Get
            Return INDsleSupplierType.Properties.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDsleSupplierType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios del saldo inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IOpeningBalance.Observations
        Get
            Return INDmemoObservations.Text
        End Get
        Set(value As String)
            INDmemoObservations.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccountAdvance As Integer Implements IOpeningBalance.IdAccountAdvance
        Get
            Return CInt(INDsleMainAccountAdvance.EditValue)
        End Get
        Set(value As Integer)
            INDsleMainAccountAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccountBill As Integer Implements IOpeningBalance.IdAccountBill
        Get
            Return CInt(INDsleMainAccountBill.EditValue)
        End Get
        Set(value As Integer)
            INDsleMainAccountBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de centro de costo de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterAdvanceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOpeningBalance.CostCenterAdvanceXpo
        Get
            Return CType(INDsleCostCenterAdvance.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenterAdvance.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de centro de costo de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterBillXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOpeningBalance.CostCenterBillXpo
        Get
            Return CType(INDsleCostCenterBill.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenterBill.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenterAdvance As Integer? Implements IOpeningBalance.IdCostCenterAdvance
        Get
            Return CInt(INDsleCostCenterAdvance.EditValue)
        End Get
        Set(value As Integer?)
            INDsleCostCenterAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenterBill As Integer? Implements IOpeningBalance.IdCostCenterBill
        Get
            Return CInt(INDsleCostCenterBill.EditValue)
        End Get
        Set(value As Integer?)
            INDsleCostCenterBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del proveedor de anticipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplierAdvance As Integer Implements IOpeningBalance.IdSupplierAdvance
        Get
            Return _idSupplierAdvance
        End Get
        Set(value As Integer)
            _idSupplierAdvance = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del proveedor de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplierBill As Integer Implements IOpeningBalance.IdSupplierBill
        Get
            Return _idSupplierBill
        End Get
        Set(value As Integer)
            _idSupplierBill = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la linea de distribucion de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplierDistributionLinesAdvance As Integer Implements IOpeningBalance.IdSupplierDistributionLinesAdvance
        Get
            Return INDsleSupplierAdvance.EditValue
        End Get
        Set(value As Integer)
            INDsleSupplierAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la linea de distribucion de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplierDistributionLinesBill As Integer Implements IOpeningBalance.IdSupplierDistributionLinesBill
        Get
            Return INDsleSupplierBill.EditValue
        End Get
        Set(value As Integer)
            INDsleSupplierBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datsource de las lineas de distribucion del proveedor de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesAdvanceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOpeningBalance.SuppliersDistributionLinesAdvanceXpo
        Get
            Return CType(INDsleSupplierAdvance.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSupplierAdvance.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de las lineas de distribucion del proveedor de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesBillXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOpeningBalance.SuppliersDistributionLinesBillXpo
        Get
            Return CType(INDsleSupplierBill.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSupplierBill.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la observacion del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdvanceComments As String Implements IOpeningBalance.AdvanceComments
        Get
            Return INDmemoComments.Text
        End Get
        Set(value As String)
            INDmemoComments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdvanceDate As Date? Implements IOpeningBalance.AdvanceDate
        Get
            Return INDdteAdvanceDate.EditValue
        End Get
        Set(value As Date?)
            INDdteAdvanceDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueAdvance As Decimal Implements IOpeningBalance.ValueAdvance
        Get
            Return INDtxtValueAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValueAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueBill As Decimal Implements IOpeningBalance.ValueBill
        Get
            Return INDtxtValueBill.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValueBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BalanceBill As Decimal Implements IOpeningBalance.BalanceBill
        Get
            Return INDtxtBalanceBill.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBalanceBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IOpeningBalance.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PaymentsSecuence Implements IOpeningBalance.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PaymentsSecuence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequence.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del saldo inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeOpeningBalance As String Implements IOpeningBalance.CodeOpeningBalance
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Estado de la barra de botones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Integer Implements IOpeningBalance.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Integer)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga las definiciones del layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IOpeningBalance.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String Implements IOpeningBalance.BillNumber
        Get
            Return INDtxtBill.Text
        End Get
        Set(value As String)
            INDtxtBill.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el plazo del saldo inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Term As Integer Implements IOpeningBalance.Term
        Get
            Return CInt(INDseTerm.EditValue)
        End Get
        Set(value As Integer)
            INDseTerm.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' Listado de saldos iniciales anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListInitialBalanceAdvance As List(Of InitialBalanceAdvance)

    ''' <summary>
    ''' Listado de eliminados saldos iniciales anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteInitialBalanceAdvance As List(Of InitialBalanceAdvance)

    ''' <summary>
    ''' Representa la entidad de saldo inicial con anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim initialBalanceAdvance As InitialBalanceAdvance

    ''' <summary>
    ''' Representa la entidad de saldo inicial con cxp
    ''' </summary>
    ''' <remarks></remarks>
    Dim initialBalanceAccountPayable As InitialBalanceAccountPayable

    ''' <summary>
    ''' Listado de saldos iniciales cxp
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListInitialBalanceAccountPayable As List(Of InitialBalanceAccountPayable)

    ''' <summary>
    ''' Listado de eliminados saldos iniciales cxp
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteInitialBalanceAccountPayable As List(Of InitialBalanceAccountPayable)

    ''' <summary>
    ''' Variable que contiene la entidad de dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim openingBalance As InitialBalance

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As POpeningBalance

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    Dim ban As Boolean = False

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Listado de saldos iniciales 
    ''' </summary>
    ''' <remarks></remarks>
    Private ListAccountPayable As List(Of AccountPayable)

    ''' <summary>
    ''' Representa la entidad de cuenta x pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountP As AccountPayable

    ''' <summary>
    ''' Representa el valor original del numero de factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim originalValBill As String

    ''' <summary>
    ''' Identifica si de añade o se edita un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim identityRegister As Boolean = True

    ''' <summary>
    ''' Id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idSupplier As Integer

    ''' <summary>
    ''' Id del proveedor de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idSupplierAdvance As Integer

    ''' <summary>
    ''' Id del proveedor de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idSupplierBill As Integer

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThitdParty As Integer

    ''' <summary>
    ''' Id del tercero de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThirdPartyAdvance As Integer

    ''' <summary>
    ''' Id del tercero de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThirdPartyBill As Integer

    ''' <summary>
    ''' True = modificar y False = guardar en las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModify As Boolean = False

    ''' <summary>
    ''' False = solo confirma y True = guarda y confirma
    ''' </summary>
    ''' <remarks></remarks>
    Dim saveAndConfirm As Boolean

    ''' <summary>
    ''' Variable para editar
    ''' </summary>
    ''' <remarks></remarks>
    Dim iba As InitialBalanceAdvance

    ''' <summary>
    ''' Variable para editar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ibaccp As InitialBalanceAccountPayable

    ''' <summary>
    ''' Bandera para search de tipo de proveedor (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banSearchSupplierType As Boolean = True

    ''' <summary>
    ''' Bandera para search de unidad radicacion (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banFilingUnit As Boolean = True

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If openingBalance.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            If ValidateFields() = False Then
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MOpeningBalance(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveOpeningBalance(openingBalance, _idCurrentSequence)
                If Result.StateResult = True Then
                    If openingBalance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf openingBalance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If openingBalance.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.openingBalance = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Observación", .FieldName = "Observations", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInitialBalance
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If openingBalance IsNot Nothing AndAlso openingBalance.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MOpeningBalance(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteOpeningBalance(openingBalance)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' METODO: Item nuevo del control de usuario
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        Await NewOpeningBalance()
    End Sub

    ''' <summary>
    ''' Confirma la nota debito/credito
    ''' </summary>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Async Sub Confirmar()
        If ValidateFields() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MOpeningBalance(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.ConfirmOpeningBalance(openingBalance, saveAndConfirm, _idCurrentSequence, BarraBotones.OperatingUnit.Id)
                If Result.StateResult = True Then
                    openingBalance = Result.ObjectEmbbeded
                    If saveAndConfirm = False Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("Confirm", NAME_MODULE), Result.MessageResult(0))
                    Else
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("ConfirmWithCode", NAME_MODULE), Result.ObjectEmbbeded.Code, Result.MessageResult(0))
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.MessageResult IsNot Nothing Then
                        generateListError(Result.MessageResult(0))
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Genera los mensajes de error.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <remarks></remarks>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

#End Region

#Region "Events"

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de No. factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtBill_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtBill.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BillNumber IsNot String.Empty Then
                ValidateBill()
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberEmpty", NAME_MODULE)
                INDtxtBill.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceBill.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDtxtBill.Focus()
            INDpceBill.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el campo de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeOpeningBalance.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeOpeningBalance) Then
                    Await Me.NewOpeningBalance()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar las teclas enter o f4 al control de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAdvance_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAdvance.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDsleSupplierAdvance.Focus()
            INDpceAdvance.ShowPopup()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Evento que se dispara al presionar los botones del menu de acciones de la rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                ibaccp = CType(viewBills.GetFocusedRow, InitialBalanceAccountPayable)
                EditBill()
            Case "Remove"
                DeleteBill()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual de rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                ibaccp = CType(viewBills.GetFocusedRow, InitialBalanceAccountPayable)
                EditBill()
            Case "Remove"
                DeleteBill()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar los botones del menu de acciones de la rejilla de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                iba = CType(viewAdvance.GetFocusedRow, InitialBalanceAdvance)
                EditAdvance()
            Case "Remove"
                DeleteAdvance()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual de rejilla de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                iba = CType(viewAdvance.GetFocusedRow, InitialBalanceAdvance)
                EditAdvance()
            Case "Remove"
                DeleteAdvance()
        End Select
    End Sub

#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListInitialBalanceAdvance = Nothing
        ListDeleteInitialBalanceAdvance = Nothing
        initialBalanceAdvance = Nothing
        initialBalanceAccountPayable = Nothing
        ListInitialBalanceAccountPayable = Nothing
        ListDeleteInitialBalanceAccountPayable = Nothing
        openingBalance = Nothing
        record = Nothing
        _presenter = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ban = Nothing
        _idOperativeUnit = Nothing
        ListAccountPayable = Nothing
        accountP = Nothing
        originalValBill = Nothing
        identityRegister = Nothing
        _idSupplier = Nothing
        _idSupplierAdvance = Nothing
        _idSupplierBill = Nothing
        _idThitdParty = Nothing
        _idThirdPartyAdvance = Nothing
        _idThirdPartyBill = Nothing
        modeModify = Nothing
        saveAndConfirm = Nothing
        iba = Nothing
        ibaccp = Nothing
        banSearchSupplierType = Nothing
        banFilingUnit = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmOpeningBalances_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyOpeningBalance, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        indigo = SessionValues.Instance

        IndigoGridControl1.SetControlNextFocus(INDgcAdvance, INDpceBill)

        viewAdvance.OptionsFind.AlwaysVisible = True
        IndigoGridControl1.SetExportButton(INDgcAdvance, True)

        viewBills.OptionsFind.AlwaysVisible = True
        IndigoGridControl1.SetExportButton(INDgcBills, True)

        IndigoGridControl1.RefreshGrid(INDgcBills)
        IndigoGridControl1.RefreshGrid(INDgcAdvance)
        _presenter = New POpeningBalance(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        LoadStatus()
        Deshacer()

        IndigoGridView1.MoreInfoColunmns(viewBills)

        Dim ListAcctionsBill As New List(Of eAcciones)
        ListAcctionsBill.Add(eAcciones.Edit)
        ListAcctionsBill.Add(eAcciones.Remove)
        IndigoGridControl1.SetAddActions(INDgcBills, ListAcctionsBill)
        IndigoGridView1.SetListAcction(viewBills, ListAcctionsBill)

        Dim ListAcctionsAdvance As New List(Of eAcciones)
        ListAcctionsAdvance.Add(eAcciones.Edit)
        ListAcctionsAdvance.Add(eAcciones.Remove)
        IndigoGridControl1.SetAddActions(INDgcAdvance, ListAcctionsAdvance)
        IndigoGridView2.SetListAcction(viewAdvance, ListAcctionsAdvance)

        INDBtnExportBillsStructure.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Proveedor"},
                            New ExcelColumn With {.Name = "Factura"},
                            New ExcelColumn With {.Name = "Código Línea"},
                            New ExcelColumn With {.Name = "Centro Costo"},
                            New ExcelColumn With {.Name = "Fecha Factura"},
                            New ExcelColumn With {.Name = "Plazo"},
                            New ExcelColumn With {.Name = "Valor"},
                            New ExcelColumn With {.Name = "Tipo Proveedor"},
                            New ExcelColumn With {.Name = "Unidad Radicación"},
                            New ExcelColumn With {.Name = "Fecha Servicio"},
                            New ExcelColumn With {.Name = "Saldo"},
                            New ExcelColumn With {.Name = "Fecha de radidación"},
                            New ExcelColumn With {.Name = "Moneda", .Comment = "Usar la abreviación de la moneda a utilizar"}
                        }
                    })

        INDBtnExportAdvanceStructure.AddRangeColumns("Proveedor", "Código Línea", "Centro Costo", "Fecha", "Observación", "Valor")

        INDdteDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de unidad de radicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleFilingUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFilingUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("723", "", True)
            Await InitializeFilingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleSupplierType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("726", "", True)
            If IdSupplierBill > 0 Then
                Await InitializeSupplierType()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbtnConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que abre el form de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleSupplierBill_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierBill.ButtonClick, INDsleSupplierAdvance.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeSuppliers()
            If IdSupplierBill > 0 Then
                Await InitializeSupplierType()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountBill_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountBill.ButtonClick, INDsleMainAccountAdvance.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el from de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterBill_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenterBill.ButtonClick, INDsleCostCenterAdvance.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeCostCenters()
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmOpeningBalances_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmOpeningBalances_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Text = String.Empty Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad de radicación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnit.EditValueChanged
        If FilingUnitId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnit.ValidateFilingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del campo de fecha de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteBillDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteBillDate.EditValueChanged
        If INDdteBillDate.EditValue IsNot Nothing Then
            If Term > 0 Then
                AddDaysToDate()
            Else
                INDdteExpiredDate.Properties.MinValue = INDdteBillDate.EditValue
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de plazo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseTerm_EditValueChanged(sender As Object, e As EventArgs) Handles INDseTerm.EditValueChanged
        If Term > 0 AndAlso INDdteBillDate.EditValue IsNot Nothing Then
            AddDaysToDate()
        ElseIf Term = 0 Then
            INDdteExpiredDate.EditValue = INDdteBillDate.EditValue
            INDdteExpiredDate.Properties.MinValue = INDdteBillDate.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierAdvance.EditValueChanged
        If INDsleSupplierAdvance.EditValue > 0 Then
            If ban = False Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplierAdvance.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                IdSupplierAdvance = suppplierMainAccount.IdSupplier.Id
                IdAccountAdvance = suppplierMainAccount.IdDistributionLine.IdMainAccount.Id
                INDsleMainAccountAdvance.Properties.NullText = suppplierMainAccount.IdDistributionLine.IdMainAccount.NumberName
                _idThirdPartyAdvance = suppplierMainAccount.IdSupplier.IdThirdParty.Id
                If suppplierMainAccount.IdDistributionLine.IdMainAccount.HandlesCostCenter = True Then
                    INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    IdCostCenterAdvance = Nothing
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor del popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleSupplierBill_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierBill.EditValueChanged
        If INDsleSupplierBill.EditValue > 0 Then
            If ban = False Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplierBill.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                IdSupplierBill = suppplierMainAccount.IdSupplier.Id
                IdAccountBill = suppplierMainAccount.IdDistributionLine.IdMainAccount.Id
                INDsleMainAccountBill.Properties.NullText = suppplierMainAccount.IdDistributionLine.IdMainAccount.NumberName
                _idThirdPartyBill = suppplierMainAccount.IdSupplier.IdThirdParty.Id
                If suppplierMainAccount.IdDistributionLine.IdMainAccount.HandlesCostCenter = True Then
                    INDlyItemCostCenterBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyItemCostCenterBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    IdCostCenterAdvance = Nothing
                End If
                INDtxtBill.Focus()
            End If
            Await InitializeSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierType.EditValueChanged
        If SupplierTypeId IsNot Nothing AndAlso banSearchSupplierType = True Then
            INDsleSupplierType.ValidateSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de saldo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtBalanceBill_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtBalanceBill.EditValueChanged
        ValidateValueAndBalance()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtValueBill_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtValueBill.EditValueChanged
        ValidateValueAndBalance()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al hacer click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddBill()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de limpiar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnClean_Click(sender As Object, e As EventArgs) Handles INDbtnClean.Click
        CleanControlsPopoup(True)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de limpiar anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCleanAdvance_Click(sender As Object, e As EventArgs) Handles INDbtnCleanAdvance.Click
        CleanControlsPopupAdvance()
        GetDate(False, True, False)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAdvance_Click(sender As Object, e As EventArgs) Handles INDbtnAddAdvance.Click
        AddAdvance()
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAdvance_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAdvance.CloseUp
        If ListInitialBalanceAdvance Is Nothing Then
            viewAdvance.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcAdvance, False)
        ElseIf ListInitialBalanceAdvance.Count > 0 Then
            viewAdvance.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcAdvance, True)
        Else
            viewAdvance.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcAdvance, False)
        End If
        If e.CloseMode = PopupCloseMode.Cancel Then
            If ListInitialBalanceAdvance Is Nothing Then
                INDpceBill.Focus()
            ElseIf ListInitialBalanceAdvance.Count > 0 Then
                IndigoGridControl1.ControlNextFocus = True
            Else
                INDpceBill.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBill.CloseUp
        If ListInitialBalanceAccountPayable Is Nothing Then
            viewBills.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcBills, False)
        ElseIf ListInitialBalanceAccountPayable.Count > 0 Then
            viewBills.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDgcBills, True)
        Else
            viewBills.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDgcBills, False)
        End If
        If e.CloseMode = PopupCloseMode.Cancel Then

        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAdvance_Popup(sender As Object, e As EventArgs) Handles INDpceAdvance.Popup
        viewAdvance.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDgcAdvance, False)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_Popup(sender As Object, e As EventArgs) Handles INDpceBill.Popup
        viewBills.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDgcBills, False)
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de proveedor de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierAdvance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplierAdvance.QueryPopUp
        If INDsleSupplierAdvance.Properties.DataSource Is Nothing Then
            _presenter.InitializeSuppliers()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro de costo de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterAdvance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenterAdvance.QueryPopUp
        If INDsleCostCenterAdvance.Properties.DataSource Is Nothing Then
            _presenter.InitializeCostCenters()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de proveedor de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierBill_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplierBill.QueryPopUp
        If INDsleSupplierBill.Properties.DataSource Is Nothing Then
            _presenter.InitializeSuppliers()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro de costo de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterBill_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenterBill.QueryPopUp
        If INDsleCostCenterBill.Properties.DataSource Is Nothing Then
            _presenter.InitializeCostCenters()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_QueryPopUp(sender As Object, e As CancelEventArgs)
        If SupplierTypeXpo Is Nothing Then
            _presenter.InitializeSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountAdvance_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccountAdvance.QueryPopUp
        If AccountXpo Is Nothing Then
            _presenter.InitializeAccountXpo()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmOpeningBalances_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
        Await InitializeFilingUnit()
    End Sub

#End Region

#Region "PasteToGrid"

    ''' <summary>
    ''' Evento del copiar y pegar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Try
            Using model As New MOpeningBalance("")
                AsyncLoader(True)
                If sender.Name = INDgcAdvance.Name Then
                    Dim result As ActionResult(Of List(Of InitialBalanceAdvance))
                    Me.Cursor = ChangeCursorIndigo()
                    result = Await model.SetAdvanceInitialBalance(e.Rows)
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                        ListInitialBalanceAdvance = result.ObjectEmbbeded
                    End If
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Using formulario As New FrmListErrors(result.MessageResult)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgcAdvance.DataSource = Nothing
                    INDgcAdvance.DataSource = ListInitialBalanceAdvance
                    viewAdvance.OptionsFind.AlwaysVisible = True
                ElseIf sender.Name = INDgcBills.Name Then

                    Dim result As ActionResult(Of List(Of InitialBalanceAccountPayable))
                    Me.Cursor = ChangeCursorIndigo()
                    result = Await model.SetBillsInitialBalance(e.Rows)
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                        If ListInitialBalanceAccountPayable Is Nothing Then
                            ListInitialBalanceAccountPayable = result.ObjectEmbbeded
                        Else
                            Dim listCompareAP As List(Of InitialBalanceAccountPayable) = result.ObjectEmbbeded
                            For Each itemCompre As InitialBalanceAccountPayable In listCompareAP
                                Dim ban As Integer = ListInitialBalanceAccountPayable.FindAll(Function(item) item.BillNumber = itemCompre.BillNumber And item.SupplierId = itemCompre.SupplierId).Cast(Of InitialBalanceAccountPayable).ToList().Count
                                If ban > 0 Then
                                    result.MessageResult.Add(String.Format(ResourceManager.GetString("BillExistCopyPaste", NAME_MODULE), itemCompre.BillNumber))
                                Else
                                    ListInitialBalanceAccountPayable.Add(itemCompre)
                                End If
                            Next
                        End If
                    End If
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Using formulario As New FrmListErrors(result.MessageResult)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgcBills.DataSource = Nothing
                    INDgcBills.DataSource = ListInitialBalanceAccountPayable
                    viewBills.OptionsFind.AlwaysVisible = True
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeSupplierType() As Task
        Using model As New MSupplierType(Tag)
            Dim x As ActionResult(Of List(Of SupplierType)) = Await model.GetSupplierTypeBySupplierId(IdSupplierBill)
            Dim listSupplierType As List(Of SupplierType) = x.ObjectEmbbeded
            SupplierTypeXpo = listSupplierType
        End Using
    End Function

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de unidad de radicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeFilingUnit() As Task
        Using model As New MFilingUnit(Tag)
            Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
            Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
            FilingUnitXpo = listFilingUnit
        End Using
    End Function

    ''' <summary>
    ''' Metodo para editar la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditBill()
        modeModify = True
        Dim ibaccp As InitialBalanceAccountPayable = CType(viewBills.GetFocusedRow, InitialBalanceAccountPayable)
        ban = True
        IdSupplierBill = ibaccp.SupplierId
        _idThirdPartyBill = ibaccp.ThirdPartyId
        IdSupplierDistributionLinesBill = ibaccp.SupplierDistributionLinesID
        IdAccountBill = ibaccp.MainAccountId

        If ibaccp.CostCenterId > 0 Then
            INDlyItemCostCenterBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            IdCostCenterBill = ibaccp.CostCenterId
            INDsleCostCenterBill.Properties.NullText = ibaccp.CostCenterDescription
        Else
            INDlyItemCostCenterBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            IdCostCenterBill = Nothing
            INDsleCostCenterBill.Properties.NullText = String.Empty
        End If


        INDsleSupplierBill.Properties.NullText = ibaccp.SupplierDescription
        INDsleMainAccountBill.Properties.NullText = ibaccp.MainAccountDescription
        ban = False
        BillNumber = ibaccp.BillNumber
        INDdteBillDate.EditValue = ibaccp.BillDate
        Term = ibaccp.Term
        INDdteExpiredDate.EditValue = ibaccp.ExpiredDate
        ValueBill = ibaccp.Value
        BalanceBill = ibaccp.Balance

        banSearchSupplierType = False
        SupplierTypeId = ibaccp.SupplierTypeId
        banSearchSupplierType = True

        banFilingUnit = False
        FilingUnitId = ibaccp.FilingUnitId
        INDsleFilingUnit.Properties.NullText = ibaccp.FilingUnitDescription
        banFilingUnit = True

        ServicePeriodDate = ibaccp.ServicePeriodDate
        INDdteRadicatedDate.EditValue = ibaccp.RadicatedDate
        ActionsOnControlsPopupBill = True
        INDpceBill.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo para editar el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditAdvance()
        modeModify = True
        Dim advP As InitialBalanceAdvance = CType(viewAdvance.GetFocusedRow, InitialBalanceAdvance)
        ban = True
        IdSupplierAdvance = advP.SupplierId
        _idThirdPartyAdvance = advP.ThirdPartyId
        IdSupplierDistributionLinesAdvance = advP.SupplierDistributionLinesId
        INDsleSupplierAdvance.Properties.NullText = advP.SupplierDescription
        ban = False

        IdAccountAdvance = advP.MainAccountId
        INDsleMainAccountAdvance.Properties.NullText = advP.MainAccountDescription

        If advP.CostCenterId > 0 Then
            INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            IdCostCenterAdvance = advP.CostCenterId
            INDsleCostCenterAdvance.Properties.NullText = advP.CostCenterDescription
        Else
            INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            IdCostCenterAdvance = Nothing
            INDsleCostCenterAdvance.Properties.NullText = String.Empty
        End If

        AdvanceDate = advP.AdvancePaymentsDate
        AdvanceComments = advP.AdvancePaymentsDescription
        ValueAdvance = advP.Value
        INDpceAdvance.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAdvance()
        Dim iba As InitialBalanceAdvance = CType(viewAdvance.GetFocusedRow, InitialBalanceAdvance)
        If iba.Id <> 0 Then
            If ListDeleteInitialBalanceAdvance Is Nothing Then
                ListDeleteInitialBalanceAdvance = New List(Of InitialBalanceAdvance)
            End If
            iba.MarkAsDeleted()
            ListDeleteInitialBalanceAdvance.Add(iba)
        End If
        ListInitialBalanceAdvance.Remove(iba)
        INDgcAdvance.DataSource = Nothing
        INDgcAdvance.DataSource = ListInitialBalanceAdvance
    End Sub

    ''' <summary>
    ''' Metodo para eliminar la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteBill()
        Dim ibaccp As InitialBalanceAccountPayable = CType(viewBills.GetFocusedRow, InitialBalanceAccountPayable)
        If ibaccp.Id <> 0 Then
            If ListDeleteInitialBalanceAccountPayable Is Nothing Then
                ListDeleteInitialBalanceAccountPayable = New List(Of InitialBalanceAccountPayable)
            End If
            ibaccp.MarkAsDeleted()
            ListDeleteInitialBalanceAccountPayable.Add(ibaccp)
        End If
        ListInitialBalanceAccountPayable.Remove(ibaccp)
        INDgcBills.DataSource = Nothing
        INDgcBills.DataSource = ListInitialBalanceAccountPayable

    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.openingBalance IsNot Nothing AndAlso Me.openingBalance.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que valida si ya existe el numero de factura en la lista
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateBill() As Task
        If IdSupplierBill > 0 Then
            Dim account As AccountPayable
            Using modelAcc As New MAccountPayable(Tag.ToString)
                account = modelAcc.GetAccountPayableByBillNumberSimple(INDtxtBill.Text, IdSupplierBill)
            End Using
            If account.Id = 0 Then
                If ListInitialBalanceAccountPayable IsNot Nothing Then
                    Dim ban As Integer = ListInitialBalanceAccountPayable.FindAll(Function(item) item.BillNumber = BillNumber).Cast(Of InitialBalanceAccountPayable).ToList().Count
                    If ban = 0 Then
                        ActionsOnControlsPopupBill = True
                        GetDate(False, False, True)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberExisting", NAME_MODULE)
                        ActionsOnControlsPopupBill = False
                        INDtxtBill.Focus()
                    End If
                Else
                    ActionsOnControlsPopupBill = True
                    GetDate(False, False, True)
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberExistDataBase", NAME_MODULE)
                ActionsOnControlsPopupBill = False
                INDtxtBill.Focus()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ChooseSupplier", NAME_MODULE)
            INDsleSupplierBill.Focus()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetDate(ByVal optionHeader As Boolean, ByVal optionAdvance As Boolean, ByVal optionBill As Boolean)
        Dim dateServerVariable As DateTime
        Using model As New MAccountPayable(CStr(Tag))
            dateServerVariable = Await model.GetServerDate()
            If optionHeader Then
                INDdteDate.EditValue = dateServerVariable
            End If
            If optionBill Then
                INDdteBillDate.EditValue = dateServerVariable
                INDdteExpiredDate.EditValue = dateServerVariable
            End If
            If optionAdvance Then
                AdvanceDate = dateServerVariable
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.openingBalance.Code, Me.openingBalance.DocumentDate, Me.openingBalance.Observations),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.openingBalance.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.openingBalance.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.openingBalance.Code, Me.openingBalance.DocumentDate, Me.openingBalance.Observations)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.openingBalance.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

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
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyOpeningBalance.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        CleanControlsPopoup(True)
        CleanControlsPopupAdvance()
        CodeOpeningBalance = String.Empty
        INDdteDate.EditValue = Nothing
        Observations = String.Empty
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        ListAccountPayable = Nothing
        INDgcBills.DataSource = Nothing
        INDgcAdvance.DataSource = Nothing
        accountP = Nothing
        initialBalanceAdvance = Nothing
        ListInitialBalanceAdvance = Nothing
        initialBalanceAccountPayable = Nothing
        ListInitialBalanceAccountPayable = Nothing
        INDdteRadicatedDate.EditValue = GetDateServer()
        INDdteRadicatedDate.Properties.MaxValue = GetDateServer()

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        ListDeleteInitialBalanceAccountPayable = Nothing
        ListDeleteInitialBalanceAdvance = Nothing
        openingBalance = Nothing
        identityRegister = True
        originalValBill = String.Empty

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyOpeningBalance.EndUpdate()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    ''' 
    Public WriteOnly Property ActionsOnControls As Boolean Implements IOpeningBalance.ActionsOnControls
        Set(value As Boolean)
            INDlyOpeningBalance.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDdteDate.Enabled = value
            INDmemoObservations.Enabled = value
            INDpceBill.Enabled = value
            INDpceAdvance.Enabled = value
            INDgcBills.Enabled = value
            INDgcAdvance.Enabled = value
            INDBtnExportAdvanceStructure.Enabled = value
            INDBtnExportBillsStructure.Enabled = value
            INDlyOpeningBalance.EndUpdate()
            If value Then
                INDdteDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    ''' 
    Public WriteOnly Property ActionsOnControlsPopupBill As Boolean
        Set(value As Boolean)
            INDsleSupplierBill.Enabled = Not value
            INDtxtBill.Enabled = Not value
            INDsleMainAccountBill.Enabled = value
            INDsleCostCenterBill.Enabled = value
            INDdteBillDate.Enabled = value
            INDseTerm.Enabled = value
            INDdteExpiredDate.Enabled = value
            INDtxtValueBill.Enabled = value
            INDtxtBalanceBill.Enabled = value
            INDsleSupplierType.Enabled = value
            INDsleFilingUnit.Enabled = value
            INDdteServicePeriodDate.Enabled = value
            INDbtnAdd.Enabled = value
            INDbtnClean.Enabled = value
            INDdteRadicatedDate.Enabled = value
            If value Then
                INDsleMainAccountBill.Focus()
            Else
                INDsleSupplierBill.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeOpeningBalance) AndAlso Not String.IsNullOrWhiteSpace(CodeOpeningBalance) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MOpeningBalance(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetOpeningBalance(INDBteCode.Text.Trim)
                    openingBalance = resultOperation.ObjectEmbbeded
                    INDlyOpeningBalance.BeginUpdate()
                    If openingBalance IsNot Nothing AndAlso openingBalance.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(openingBalance.Id))
                            With openingBalance
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                CodeOpeningBalance = .Code
                                INDdteDate.EditValue = .DocumentDate
                                Observations = .Observations
                                BarraBotones.StatusRecord = .Status.ToString

                                If .Status = 1 Then
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                End If

                                ListInitialBalanceAdvance = .InitialBalanceAdvance.ToList
                                INDgcAdvance.DataSource = Nothing
                                INDgcAdvance.DataSource = ListInitialBalanceAdvance

                                ListInitialBalanceAccountPayable = .InitialBalanceAccountPayable.ToList
                                INDgcBills.DataSource = Nothing
                                INDgcBills.DataSource = ListInitialBalanceAccountPayable
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.openingBalance.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = openingBalance.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(openingBalance.Id, Me.Tag.ToString(), Nothing, GetType(InitialBalance).Name)
                            INDlyOpeningBalance.BeginUpdate()
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDlyOpeningBalance.EndUpdate()
                            If openingBalance.Status <> 1 Then
                                ReadOnlyControls(True)
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewOpeningBalance()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeOpeningBalance = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlyOpeningBalance.EndUpdate()
                End Using
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, openingBalance.Id, 0, openingBalance.Id)
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If

        'Me.BarraBotones.StatusRecordVisible = True
        'AsyncLoader(True)
        'Try
        '    Using Model As New MOpeningBalance(CStr(Me.Tag))
        '        Dim resultOperation = Await Model.GetOpeningBalance(INDBteCode.Text.Trim)
        '        openingBalance = resultOperation.ObjectEmbbeded
        '        If Not openingBalance Is Nothing Then
        '            If openingBalance.Id > 0 Then
        '                Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
        '                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(openingBalance.Id))
        '                    With openingBalance
        '                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

        '                        CodeOpeningBalance = .Code
        '                        INDdteDate.EditValue = .DocumentDate
        '                        Observations = .Observations
        '                        BarraBotones.StatusRecord = .Status.ToString

        '                        If .Status = 1 Then
        '                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
        '                        Else
        '                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                        End If

        '                        ListInitialBalanceAdvance = .InitialBalanceAdvance.ToList
        '                        INDgcAdvance.DataSource = Nothing
        '                        INDgcAdvance.DataSource = ListInitialBalanceAdvance

        '                        ListInitialBalanceAccountPayable = .InitialBalanceAccountPayable.ToList
        '                        INDgcBills.DataSource = Nothing
        '                        INDgcBills.DataSource = ListInitialBalanceAccountPayable

        '                    End With
        '                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.openingBalance.Code)
        '                    If result.Id = 0 Then
        '                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                        state.State = Domain.Base.Entities.ObjectState.Added
        '                        record = New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = openingBalance.Id}
        '                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                        record = operation.ObjectEmbbeded
        '                    Else
        '                        record = result
        '                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                    End If
        '                    Me.BarraBotones.SetDocuments(openingBalance.Id)
        '                    INDlyOpeningBalance.BeginUpdate()
        '                    AsyncLoader(False)
        '                    ActionsOnControls = True
        '                    INDlyOpeningBalance.EndUpdate()
        '                    If openingBalance.Status <> 1 Then
        '                        ReadOnlyControls(True)
        '                    End If
        '                End Using
        '            Else
        '                AsyncLoader(False)
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.CodeOpeningBalance = String.Empty
        '            End If
        '        Else
        '            AsyncLoader(False)
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.CodeOpeningBalance = String.Empty
        '        End If
        '    End Using

        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

        '    Me.BarraBotones.PrintReport(PrintReportAction.None, openingBalance.Id, 0, openingBalance.Id)
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateFields() As Boolean
        If (ListInitialBalanceAdvance Is Nothing OrElse ListInitialBalanceAdvance.Count = 0) AndAlso (ListInitialBalanceAccountPayable Is Nothing OrElse ListInitialBalanceAccountPayable.Count = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontListAdvanceOrListAccountPayable", NAME_MODULE)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With openingBalance
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeOpeningBalance
            .DocumentDate = INDdteDate.EditValue
            .Observations = Observations
            .Status = 1

            If ListInitialBalanceAdvance IsNot Nothing Then
                For Each itemAdvance As InitialBalanceAdvance In ListInitialBalanceAdvance
                    .InitialBalanceAdvance.Add(itemAdvance)
                Next
            End If
            If ListDeleteInitialBalanceAdvance IsNot Nothing Then
                For Each itemAdvanceDelete As InitialBalanceAdvance In ListDeleteInitialBalanceAdvance
                    .InitialBalanceAdvance.Add(itemAdvanceDelete)
                Next
            End If

            If ListInitialBalanceAccountPayable IsNot Nothing Then
                For Each itemAccountPayable As InitialBalanceAccountPayable In ListInitialBalanceAccountPayable
                    .InitialBalanceAccountPayable.Add(itemAccountPayable)
                Next
            End If
            If ListDeleteInitialBalanceAccountPayable IsNot Nothing Then
                For Each itemAccountPayableDelete As InitialBalanceAccountPayable In ListDeleteInitialBalanceAccountPayable
                    .InitialBalanceAccountPayable.Add(itemAccountPayableDelete)
                Next
            End If

        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewOpeningBalance() As Task
        openingBalance = New InitialBalance()
        GetDate(True, True, False)
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeOpeningBalance = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.ActionsOnControlsPopupBill = False
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = "1"
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeOpeningBalance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.ActionsOnControlsPopupBill = False
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeOpeningBalance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.ActionsOnControlsPopupBill = False
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Me.BarraBotones.StatusRecordVisible = True
                            Me.BarraBotones.StatusRecord = "1"
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeOpeningBalance = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.ActionsOnControlsPopupBill = False
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.BarraBotones.StatusRecord = "1"
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
                End If
            End If
        End If




        'Me.openingBalance = New InitialBalance()
        'GetDate(True, True, False)
        'If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '    Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
        'ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '    If Me.Sequense.PaymentsSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '        Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '        Exit Function
        '    End If
        'End If
        'If Not Me._sequence.Sequential Then
        '    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '        If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '            Me.CodeOpeningBalance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '            Me.ActionsOnControls = True
        '            Me.ActionsOnControlsPopupBill = False
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        Else
        '            Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
        '                Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '            End Using
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.CodeOpeningBalance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.ActionsOnControlsPopupBill = False
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '            End If
        '        End If
        '    Else
        '        Me.CodeOpeningBalance = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.ActionsOnControlsPopupBill = False
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        Me.BarraBotones.StatusRecordVisible = True
        '        Me.BarraBotones.StatusRecord = "1"
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
        '    End If
        'Else
        '    Me.CodeOpeningBalance = ResourceManager.GetString("LabelOrTextboxNew")
        '    Me.ActionsOnControls = True
        '    Me.ActionsOnControlsPopupBill = False
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    Me.BarraBotones.StatusRecordVisible = True
        '    Me.BarraBotones.StatusRecord = "1"
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
        'End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(CodeOpeningBalance) Then
            Dim state As Boolean
            Select Case Status
                Case CBool(eActionsStatusRecords.Active)
                    state = True
                Case CBool(eActionsStatusRecords.Inactive)
                    state = False
            End Select
            Using model As New MOpeningBalance(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.ChangeState(CodeOpeningBalance, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Método que agrega una factura a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddBill()
        Dim validate As String = ValidateControlsPopupBill()
        If validate.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = validate
            INDdteBillDate.Focus()
            Exit Sub
        End If

        If modeModify = False Then
            If ListInitialBalanceAccountPayable Is Nothing Then
                ListInitialBalanceAccountPayable = New List(Of InitialBalanceAccountPayable)
            Else
                Dim ban As Integer = ListInitialBalanceAccountPayable.FindAll(Function(item) item.BillNumber = BillNumber).Cast(Of InitialBalanceAccountPayable).ToList().Count
                If ban > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BillExist", NAME_MODULE)
                    CleanControlsPopoup(False)
                    INDtxtBill.Focus()
                    Exit Sub
                End If
            End If
            CreateInitialBalanceAccountPayable()
            ListInitialBalanceAccountPayable.Add(initialBalanceAccountPayable)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("BillAgregateSatisfactory", NAME_MODULE)
        Else
            ibaccp.SupplierId = IdSupplierBill
            ibaccp.ThirdPartyId = _idThirdPartyBill
            ibaccp.SupplierDistributionLinesID = IdSupplierDistributionLinesBill
            ibaccp.MainAccountId = IdAccountBill

            If IdCostCenterBill > 0 Then
                ibaccp.CostCenterId = IdCostCenterBill
            End If

            ibaccp.AccountPayableId = Nothing
            ibaccp.BillNumber = BillNumber
            ibaccp.BillDate = INDdteBillDate.EditValue
            ibaccp.Term = Term
            ibaccp.ExpiredDate = INDdteExpiredDate.EditValue
            ibaccp.Value = ValueBill
            ibaccp.Balance = BalanceBill
            ibaccp.SupplierTypeId = SupplierTypeId
            ibaccp.FilingUnitId = FilingUnitId
            ibaccp.ServicePeriodDate = ServicePeriodDate
            ibaccp.SupplierDescription = INDsleSupplierBill.Text
            ibaccp.MainAccountDescription = INDsleMainAccountBill.Text
            ibaccp.CostCenterDescription = INDsleCostCenterBill.Text
            ibaccp.FilingUnitDescription = INDsleFilingUnit.Text
            ibaccp.SupplierTypeDescription = INDsleSupplierType.Text
            ibaccp.RadicatedDate = INDdteRadicatedDate.EditValue

            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SuccessfullyModifiedRecord", NAME_MODULE)
        End If

        If openingBalance.Id > 0 Then
            openingBalance.MarkAsModified()
        End If

        modeModify = False
        INDgcBills.DataSource = Nothing
        INDgcBills.DataSource = ListInitialBalanceAccountPayable
        CleanControlsPopoup(True)
        ActionsOnControlsPopupBill = False
    End Sub

    ''' <summary>
    ''' Crea la entidad de saldo inicial con cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateInitialBalanceAccountPayable()
        initialBalanceAccountPayable = New InitialBalanceAccountPayable
        With initialBalanceAccountPayable
            .SupplierId = IdSupplierBill
            .ThirdPartyId = _idThirdPartyBill
            .SupplierDistributionLinesID = IdSupplierDistributionLinesBill
            .MainAccountId = IdAccountBill

            If INDlyItemCostCenterBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = IdCostCenterBill
            Else
                .CostCenterId = Nothing
            End If

            .AccountPayableId = Nothing
            .BillNumber = BillNumber
            .BillDate = INDdteBillDate.EditValue
            .Term = Term
            .ExpiredDate = INDdteExpiredDate.EditValue
            .Value = ValueBill
            .Balance = BalanceBill
            .SupplierTypeId = SupplierTypeId
            .FilingUnitId = FilingUnitId
            .ServicePeriodDate = ServicePeriodDate

            .SupplierDescription = INDsleSupplierBill.Text
            .MainAccountDescription = INDsleMainAccountBill.Text
            .CostCenterDescription = INDsleCostCenterBill.Text
            .FilingUnitDescription = INDsleFilingUnit.Text
            .SupplierTypeDescription = INDsleSupplierType.Text
            .RadicatedDate = INDdteRadicatedDate.EditValue
        End With
    End Sub

    ''' <summary>
    ''' Valida que los controles del popup de factura se encuentren diligenciados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupBill() As String
        Dim ListErrors As New StringBuilder
        If INDdteBillDate.EditValue Is Nothing Then
            ListErrors.AppendLine("- Ingrese Fecha Factura.")
        End If
        If Term = 0 Then
            ListErrors.AppendLine("- Ingrese Plazo.")
        End If
        If INDdteExpiredDate.EditValue Is Nothing Then
            ListErrors.AppendLine("- Ingrese Fecha Vencimiento.")
        End If
        If ValueBill = 0 Then
            ListErrors.AppendLine("- Ingrese Valor.")
        End If
        If BalanceBill = 0 Then
            ListErrors.AppendLine("- Ingrese Saldo.")
        End If
        If SupplierTypeId Is Nothing Then
            ListErrors.AppendLine("- Ingrese Tipo Proveedor.")
        End If
        If FilingUnitId Is Nothing Then
            ListErrors.AppendLine("- Ingrese Unidad Radicación.")
        End If
        If ServicePeriodDate Is Nothing Then
            ListErrors.AppendLine("- Ingrese Fecha Periodo Servicio.")
        End If
        If INDdteRadicatedDate.EditValue Is Nothing Then
            ListErrors.AppendLine("- Ingrese Fecha de Radicación.")
        End If
        Return ListErrors.ToString
    End Function

    ''' <summary>
    ''' Método que agrega un anticipo a la rejilla de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddAdvance()
        If ValidateControlsPopupAdvance() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_FieldEmpty", NAME_MODULE)
            INDsleSupplierAdvance.Focus()
            Exit Sub
        End If

        If modeModify = False Then
            If ListInitialBalanceAdvance Is Nothing Then
                ListInitialBalanceAdvance = New List(Of InitialBalanceAdvance)
            End If
            CreateInitialBalanceAdvance()
            ListInitialBalanceAdvance.Add(initialBalanceAdvance)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AdvanceAgregateSatisfactory", NAME_MODULE)
        Else
            iba.SupplierId = IdSupplierAdvance
            iba.ThirdPartyId = _idThirdPartyAdvance
            iba.SupplierDistributionLinesId = IdSupplierDistributionLinesAdvance
            iba.MainAccountId = IdAccountAdvance

            If IdCostCenterAdvance > 0 Then
                iba.CostCenterId = IdCostCenterAdvance
            End If
            iba.AdvancePaymentsDate = AdvanceDate
            iba.AdvancePaymentsDescription = AdvanceComments
            iba.Value = ValueAdvance
            iba.SupplierDescription = INDsleSupplierAdvance.Text
            iba.MainAccountDescription = INDsleMainAccountAdvance.Text
            iba.CostCenterDescription = INDsleCostCenterAdvance.Text
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SuccessfullyModifiedRecord", NAME_MODULE)
        End If

        If openingBalance.Id > 0 Then
            openingBalance.MarkAsModified()
        End If

        modeModify = False
        INDgcAdvance.DataSource = Nothing
        INDgcAdvance.DataSource = ListInitialBalanceAdvance
        CleanControlsPopupAdvance()
        GetDate(False, True, False)
    End Sub

    ''' <summary>
    ''' Crea un saldo inicial anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateInitialBalanceAdvance()
        initialBalanceAdvance = New InitialBalanceAdvance
        With initialBalanceAdvance
            .SupplierId = IdSupplierAdvance
            .ThirdPartyId = _idThirdPartyAdvance
            .SupplierDistributionLinesId = IdSupplierDistributionLinesAdvance
            .MainAccountId = IdAccountAdvance

            If INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = IdCostCenterAdvance
            Else
                .CostCenterId = Nothing
            End If


            .AdvancePaymentsId = Nothing
            .AdvancePaymentsDate = AdvanceDate
            .AdvancePaymentsDescription = AdvanceComments
            .Value = ValueAdvance

            .SupplierDescription = INDsleSupplierAdvance.Text
            .MainAccountDescription = INDsleMainAccountAdvance.Text
            .CostCenterDescription = INDsleCostCenterAdvance.Text
        End With
    End Sub

    ''' <summary>
    ''' Método para limpiar los controles del popup control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopoup(ByVal mode As Boolean)
        If mode Then
            IdSupplierBill = Nothing
            IdSupplierDistributionLinesBill = Nothing
            _idThirdPartyBill = Nothing
            INDsleSupplierBill.Properties.NullText = String.Empty
        End If
        IdAccountBill = Nothing
        INDsleMainAccountBill.Properties.NullText = String.Empty
        IdCostCenterBill = Nothing
        INDsleCostCenterBill.Properties.NullText = String.Empty
        INDdteBillDate.EditValue = Nothing
        BillNumber = String.Empty
        Term = Nothing
        INDdteExpiredDate.EditValue = Nothing
        ValueBill = 0
        BalanceBill = 0
        SupplierTypeId = Nothing
        FilingUnitId = Nothing
        INDsleFilingUnit.Properties.NullText = String.Empty
        ServicePeriodDate = Nothing
        ActionsOnControlsPopupBill = False
        modeModify = False
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupAdvance()
        AdvanceDate = Nothing
        AdvanceComments = String.Empty
        ValueAdvance = 0
        IdSupplierAdvance = Nothing
        IdSupplierDistributionLinesAdvance = Nothing
        INDsleSupplierAdvance.Properties.NullText = String.Empty
        _idThirdPartyAdvance = Nothing
        IdAccountAdvance = Nothing
        INDsleMainAccountAdvance.Properties.NullText = String.Empty
        IdCostCenterAdvance = Nothing
        INDsleCostCenterAdvance.Properties.NullText = String.Empty
        modeModify = False
        INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleSupplierAdvance.Focus()
    End Sub

    ''' <summary>
    ''' Valida que los controles del popup de anticipo esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupAdvance() As Boolean
        If AdvanceDate Is Nothing Then
            Return False
        End If
        If ValueAdvance = 0 Then
            Return False
        End If
        If IdSupplierAdvance = Nothing OrElse IdSupplierAdvance = 0 Then
            Return False
        End If
        If IdAccountAdvance = Nothing OrElse IdAccountAdvance = 0 Then
            Return False
        End If
        If INDlyItemCostCenterAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If IdCostCenterAdvance Is Nothing OrElse IdCostCenterAdvance = 0 Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Adiciona los dias de plazo a la fecha de vencimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDaysToDate()
        Dim dateExpired As DateTime
        dateExpired = PaymentServices.AddDaysDate(Term, INDdteBillDate.EditValue)
        INDdteExpiredDate.EditValue = dateExpired
        INDdteExpiredDate.Properties.MinValue = dateExpired
    End Sub

    ''' <summary>
    ''' Valida que el saldo de la factura no sea mayor que el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateValueAndBalance()
        If ValueBill <> 0 AndAlso BalanceBill <> 0 Then
            If BalanceBill > ValueBill Then
                'Se quita esta validación a petición de JhonRojas
                'Mensaje(EeventViewerImages.Advertencia) = "El saldo no puede ser mayor al valor."
                'BalanceBill = 0
            End If
        End If
    End Sub

#End Region

#Region "Bar buttons events"

    ''' <summary>
    ''' Evento de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barra botones: Click Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        openingBalance.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Click confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        saveAndConfirm = False
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barra botones: Click guardar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        saveAndConfirm = True
        Confirmar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, openingBalance.Id, 0, openingBalance.Id)
    End Sub

#End Region

End Class