'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-12
'
' Last Modified By : Diego Andrés Roldán Lozano
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Dynamic
Imports Domain.Entities
Imports Domain.Crystal.Entities
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports Presentation.Common
Imports System.Drawing
Imports DevExpress.XtraSplashScreen
Imports Infrastructure.CrossCutting.Exceptions
Imports DevExpress.XtraEditors.ButtonPanel
Imports Presentation.Inventory.MVP
Imports Presentation.Common.MVP
Imports Presentation.Billing.Entities
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmLiquidation
    Implements ILiquidation

    Public Sub New()
        InitializeComponent()
        Me.IsOncologycalMode = False
    End Sub

#Region "Consts"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Billing"

    ''' <summary>
    ''' Prefijo de los recursos de PlaceEntry
    ''' </summary>
    Private Const PREFIX_PLACEENTRY As String = "PlaceEntry_"

    ''' <summary>
    ''' Prefijo de los recursos de StayLiquidationType
    ''' </summary>
    Private Const PREFIX_STAYLIQUIDATIONTYPE As String = "StayLiquidationType_"

#End Region

#Region "Fileds"

    ''' <summary>
    ''' The _open admissions
    ''' </summary>
    Private _openAdmissions As Boolean

    ''' <summary>
    ''' Objeto de ingreso usado para recargar la el ingreso si es necesario
    ''' </summary>
    Private _auxAdmissionToReload As Infrastructure.Data.Xpo.CrystalRepository.ViewLiquidationGetAdmissionAll

    Public Property AuxAdmissionToReload As Object
        Get
            Return _auxAdmissionToReload
        End Get
        Set(value As Object)
            _auxAdmissionToReload = value
        End Set
    End Property

    ''' <summary>
    ''' Errores al consultar las estancias
    ''' </summary>
    ''' <value>
    ''' The stay errors.
    ''' </value>
    Private Property StayErrors As String

    ''' <summary>
    ''' bandera utilizada para saber si se esta refrescando el ingreso
    ''' </summary>
    Private _isRefreshing As Boolean

    ''' <summary>
    ''' modo de vista del documento
    ''' </summary>
    Private _modeViewDocument As eModeViewDocument = eModeViewDocument.Folios

    ''' <summary>
    ''' cantidad de facturas anuladas
    ''' </summary>
    Private _countAnnullateInvoices As Integer

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PLiquidation

    ''' <summary>
    ''' Sumatoria total del valor que le corresponde a la entidad
    ''' </summary>
    Private _totalEntity As Decimal

    ''' <summary>
    ''' Sumatoria total del valor que le corresponde a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _totalPatient As Decimal

    ''' <summary>
    ''' Colección de objetos de notificación
    ''' </summary>
    Private _notificationItemCollection As List(Of NotificationItem)

    ''' <summary>
    ''' Id del tercero asociado al paciente de la admisión
    ''' </summary>
    Private _thirdPartyPatientId As Integer

    ''' <summary>
    ''' id de la entidad administradora de salud asociada a la admisión
    ''' </summary>
    Private _healthAdministratorAdmissionId As Integer

    Public Property AdmissionNumber As String

    Private _loadingStays As Boolean

    ''' <summary>
    ''' varibale que obtiene si el folio es cuenta madre
    ''' </summary>
    Private _isMasterAccount As Boolean

    ''' <summary>
    ''' variable que determina si se muestra la accion de Datos de la liquidacion
    ''' </summary>
    Private _displayLiquidateData As Boolean

    ''' <summary>
    ''' Obtiene o Asigna los parametros de facturacion
    ''' </summary>
    Private settingBilling As SettingsBilling

    ''' <summary>
    ''' Obtiene o Asigna los valores de los folios 
    ''' </summary>
    Private _folio As IFolio

    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)

    ''' <summary>
    ''' Formulario padre, donde se encuentra alojado el control
    ''' </summary>
    Public Property FormOwner As ILiquidation

    ''' <summary>
    ''' Id Grupo de atencion de la cuenta madre
    ''' </summary>
    Private _careGroupIdMasterAccount As Integer?

#End Region

#Region "Properties"
    Private _isOncologicalMode As Boolean
    Public WriteOnly Property IsOncologycalMode As Boolean
        Set(value As Boolean)
            _isOncologicalMode = value
            Me.SuspendLayout()
            If value Then
                PnlCtrlHeader.SendToBack()
                PnlCtrlHeader.Dock = System.Windows.Forms.DockStyle.None
            Else
                PnlCtrlHeader.BringToFront()
                PnlCtrlHeader.Dock = System.Windows.Forms.DockStyle.Top
            End If
            Me.ResumeLayout()
        End Set
    End Property

    Public Property RevenueControlParentId As Integer?
    ''' <summary>
    ''' Evento que se dispara para enviar a refrescar el formulario de liquidacion padre
    ''' </summary>
    Public Event BeginReloadLiquidationForm()

    ''' <summary>
    ''' Obtiene o asigna los permisos que el usuario tiene asignados en éste formulario
    ''' </summary>
    ''' <value>Diccionario de permisos del usuario</value>
    ''' <returns>El diccionario de permisos del usuario</returns>
    Public Property PermissionsForm As Dictionary(Of Integer, String) Implements ILiquidation.PermissionsForm

    ''' <summary>
    ''' Obtiene el numero total de folios
    ''' </summary>
    ''' <returns>Número total de folios</returns>
    Public ReadOnly Property CountFolio As Integer
        Get
            Dim res As Integer = 0
            For Each doc In Me.DocumentManager.View.Documents
                If Not DirectCast(doc.Control, CtrFolio).IsInvoiced Then
                    res += 1
                End If
            Next
            Return res
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el numero total de facturas
    ''' </summary>
    ''' <returns>Número total de facturas</returns>
    Public ReadOnly Property CountInvoices As Integer
        Get
            Dim res As Integer = 0
            For Each doc In Me.DocumentManager.View.Documents
                If DirectCast(doc.Control, CtrFolio).IsInvoiced Then
                    res += 1
                End If
            Next
            Return res
        End Get
    End Property

    ''' <summary>
    ''' Obtiene una lista con los números de folio
    ''' </summary>
    ''' <returns>Lista con los números de folio</returns>
    Public ReadOnly Property ListFolioOrders As List(Of Integer)
        Get
            Dim res As New List(Of Integer)()
            For Each doc In Me.DocumentManager.View.Documents
                If Not DirectCast(doc.Control, CtrFolio).IsInvoiced Then
                    res.Add(DirectCast(doc.Control, CtrFolio).FolioOrder)
                End If
            Next
            Return res
        End Get
    End Property

    ''' <summary>
    ''' Obtiene una lista de pares Id/FolioOrder de los folios mostrados
    ''' </summary>
    ''' <returns>Lista de folios</returns>
    Public ReadOnly Property ListFoliosIdOrder As List(Of Object) Implements ILiquidation.ListFoliosIdOrder
        Get
            Dim res As New List(Of Object)()
            Dim obj1 As Object = New ExpandoObject()
            obj1.FolioId = -1
            obj1.FolioNumber = ResourceManager.GetString("NewFolio", MODULE_NAME)
            res.Add(obj1)
            For Each doc In Me.DocumentManager.View.Documents
                If Not DirectCast(doc.Control, CtrFolio).IsInvoiced Then
                    Dim obj As Object = New ExpandoObject()
                    obj.FolioId = DirectCast(doc.Control, CtrFolio).Id
                    obj.FolioNumber = String.Format(ResourceManager.GetString("StrFolio", GetType(CtrFolio)), DirectCast(doc.Control, CtrFolio).FolioOrder)
                    res.Add(obj)
                End If
            Next
            Return res
        End Get
    End Property

    ''' <summary>
    ''' Obtiene una lista con los números de factura
    ''' </summary>
    ''' <returns>Lista con los números de factura</returns>
    Public ReadOnly Property ListInvoiceNumbers As List(Of String)
        Get
            Dim res As New List(Of String)()
            For Each doc In Me.DocumentManager.View.Documents
                If DirectCast(doc.Control, CtrFolio).IsInvoiced Then
                    res.Add(DirectCast(doc.Control, CtrFolio).InvoiceNumber)
                End If
            Next
            Return res
        End Get
    End Property

    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el total de la entidad
    ''' </summary>
    ''' <value>Total de la entidad</value>
    ''' <returns>El total de la entidad</returns>
    Public Property TotalEntity As Decimal
        Get
            Return Me._totalEntity
        End Get
        Set(value As Decimal)
            Me._totalEntity = value
            Window.Utils.SetValueToProperty(Me.LblTotalEntity, "Text", Me._totalEntity.ToString("c2", SessionValues.Instance.Culture))
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el total del paciente
    ''' </summary>
    ''' <value>Total del paciente</value>
    ''' <returns>El total del paciente</returns>
    Public Property TotalPatient As Decimal
        Get
            Return Me._totalPatient
        End Get
        Set(value As Decimal)
            Me._totalPatient = value
            Window.Utils.SetValueToProperty(Me.LblTotalPatient, "Text", Me._totalPatient.ToString("c2", SessionValues.Instance.Culture))
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si
    ''' se encuentra ejecutando un proceso
    ''' </summary>
    ''' <value>Valor que indica si el frontal esta ocupado</value>
    ''' <returns>El valor que indica si el frontal esta ocupado</returns>
    Public Property IsBusy As Boolean Implements ILiquidation.IsBusy
        Get
            Return If(Me.LyciBusyIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always, True, False)
        End Get
        Set(value As Boolean)
            INDSleAdmissionNumber2.Enabled = Not value
            Me.LyciBusyIndicator.Visibility = If(value = True, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            If value Then
                PnlProgressPanel.ImageHorzOffset = (System.Windows.Forms.Screen.FromControl(Me).WorkingArea.Width / 2) - 150
                PnlProgressPanel.Visible = True
                PnlProgressPanel.BringToFront()
            Else
                PnlProgressPanel.Visible = False
                PnlProgressPanel.SendToBack()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tag del frontal
    ''' </summary>
    ''' <value>Tag del frontal</value>
    ''' <returns>El tag del frontal</returns>
    Public ReadOnly Property MyTag As String Implements ILiquidation.MyTag
        Get
            Return Me.Tag.ToString()
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la autorización seleccionada
    ''' </summary>
    ''' <returns>La autorización seleccionada</returns>
    Public ReadOnly Property BillingAuthorizationSelected As BillingAuthorization Implements ILiquidation.BillingAuthorizationSelected
        Get
            Dim res = (From b As BillingAuthorization In DirectCast(Me.SleBillingAuthorization.Properties.DataSource, List(Of BillingAuthorization)) Where b.Id = Convert.ToInt32(Me.SleBillingAuthorization.EditValue) Select b).ToList()
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res(0)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el Id de la autorización seleccionada
    ''' </summary>
    ''' <returns>El Id de la autorización seleccionada</returns>
    Public Property IdBillingAuthorizationSelected As Integer Implements ILiquidation.IdBillingAuthorizationSelected
        Get
            Return Me.SleBillingAuthorization.EditValue
        End Get
        Set(value As Integer)
            Me.SleBillingAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Id de la unidad operativa seleccionad</returns>
    Public Property IdOperatingUnitSelected As Integer Implements ILiquidation.IdOperatingUnitSelected
        Get
            Return Me.SleOperatingUnit.EditValue
        End Get
        Set(value As Integer)
            Me.SleOperatingUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de autorizaciones de facturación
    ''' asignadas al usuario
    ''' </summary>
    ''' <value>Lista de autorizaciones de facturación</value>
    ''' <returns>Lamlista de autorizaciones de facturación</returns>
    Public Property ListBillingAuthorization As List(Of BillingAuthorization) Implements ILiquidation.ListBillingAuthorization
        Get
            Return Me.SleBillingAuthorization.Properties.DataSource
        End Get
        Set(value As List(Of BillingAuthorization))
            Me.SleBillingAuthorization.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                Me.SleBillingAuthorization.EditValue = value(0).Id
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de unidades operativas
    ''' </summary>
    ''' <value>Lista de unidades operativa</value>
    ''' <returns>La lista de unidades operativas</returns>
    Public Property ListOperatingUnit As List(Of OperatingUnit) Implements ILiquidation.ListOperatingUnit
        Get
            Return Me.SleOperatingUnit.Properties.DataSource
        End Get
        Set(value As List(Of OperatingUnit))
            If value IsNot Nothing Then
                Dim listFilter = (From e In value Where e IsNot Nothing Select e).ToList()
                Me.SleOperatingUnit.Properties.DataSource = listFilter
                If listFilter.Count > 0 Then
                    Me.SleOperatingUnit.EditValue = SessionValues.Instance.IndigoOperatingUnitId
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Unidad operativa seleccionada</returns>
    Public ReadOnly Property OperatingUnitSelected As OperatingUnit Implements ILiquidation.OperatingUnitSelected
        Get
            If Me.SleOperatingUnit.EditValue IsNot Nothing AndAlso Me.SleOperatingUnit.Properties.DataSource IsNot Nothing Then
                Dim queryOperatingUnit = (From i In Me.ListOperatingUnit
                                          Where i IsNot Nothing AndAlso i.Id = CType(SleOperatingUnit.EditValue, Integer)
                                          Select i).ToList
                If queryOperatingUnit IsNot Nothing AndAlso queryOperatingUnit.Count > 0 Then
                    Dim operating = CType(queryOperatingUnit(0), OperatingUnit)
                    Return operating
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private ReadOnly Property ILiquidation_TxtPatientCode As Object Implements ILiquidation.TxtPatientCode
        Get
            Return TxtPatientCode
        End Get
    End Property

    Private ReadOnly Property ILiquidation_Name As String Implements ILiquidation.Name
        Get
            Return Me.Name
        End Get
    End Property

    Private ReadOnly Property ILiquidation_TxtPatientName As Object Implements ILiquidation.TxtPatientName
        Get
            Return TxtPatientName
        End Get
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el id del grupo de atencion de la cuenta madre
    ''' </summary>
    ''' <returns></returns>
    Private Property CareGroupIdMasterAccount As Integer?
        Get
            Return _careGroupIdMasterAccount
        End Get
        Set(value As Integer?)
            _careGroupIdMasterAccount = value
        End Set
    End Property

    Private Property _flagTaxInclude As Boolean
    ''' <summary>
    ''' Obtiene o establecer si el sistema es IVA incluido o no
    ''' </summary>
    Public Property FlagTaxInclude As Boolean Implements ILiquidation.FlagTaxInclude
        Get
            Return _flagTaxInclude
        End Get
        Set(value As Boolean)
            _flagTaxInclude = value
        End Set
    End Property

#End Region

#Region "Handlers"
    ''' <summary>
    ''' Aqui se valida los permisos que tiene el usuario
    ''' y se muestra solo las opciones permitidas
    ''' </summary>
    Private Sub DdbMenu_ShowDropDownControl(sender As Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs) Handles DdbMenu.ShowDropDownControl
        Dim documento As DevExpress.XtraBars.Docking2010.Views.BaseDocumentCollection = Me.DocumentManager.View.Documents
        'Datos de la liquidacion
        Me.MbtnLiquidateData.Visibility = If(_displayLiquidateData, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        'Modificar Autorización del ingreso
        MBtnModifyAuthorization.Visibility = If(_auxAdmissionToReload IsNot Nothing AndAlso Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarNumeroAutorizacion)), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón Ver Ingresos Confirmados
        Me.MbtnConfirmAdmission.Visibility = If(Not _openAdmissions, DevExpress.XtraBars.BarItemVisibility.Never, DevExpress.XtraBars.BarItemVisibility.Always)
        'Botón Ver Ingresos Abiertos
        Me.MbtnOpenAdmission.Visibility = If(_openAdmissions, DevExpress.XtraBars.BarItemVisibility.Never, DevExpress.XtraBars.BarItemVisibility.Always)
        'Botón de refrescar
        Me.MbtnRefresh.Visibility = If(Me._auxAdmissionToReload Is Nothing, DevExpress.XtraBars.BarItemVisibility.Never, DevExpress.XtraBars.BarItemVisibility.Always)
        'Botón de liquidar todo
        Me.MbtnLiquidateAll.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.Liquidar)), If(_modeViewDocument = eModeViewDocument.Folios, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón de Anular Facturas Ambulatorias
        Me.MBtnAnulateAllAmbulatory.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.AnularFactura)), If(_modeViewDocument = eModeViewDocument.Folios, If(_auxAdmissionToReload.Status.ToString.Equals("F"), If(documento.Where(Function(d) CType(d.Control, ICtrFolio).AdmissionType = 1).Count > 0, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón de liquidar estancias
        Me.MbtnLiquidateStays.Visibility = DevExpress.XtraBars.BarItemVisibility.Never 'If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.LiquidarEstancias)), If((Me.MbtnLiquidateStays.Tag IsNot Nothing AndAlso Me.MbtnLiquidateStays.Tag = True) OrElse (Me.GdcStays.DataSource IsNot Nothing AndAlso CType(Me.GdcStays.DataSource, List(Of CHREGESTA)).Any(Function(o) o.CHREGESTADET.Any(Function(m) m.CANTIDADLIQ > 0))), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón de ver facturas anuladas
        Me.MbtnViewVoidInvoices.Caption = String.Format("Facturas Anuladas ({0})", _countAnnullateInvoices)
        Me.MbtnViewVoidInvoices.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.VerFacturasAnuladas)), If(_countAnnullateInvoices > 0 AndAlso _modeViewDocument = eModeViewDocument.Folios, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón de ver folios
        Me.MbtnViewFolios.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.VerFacturasAnuladas)), If(_modeViewDocument = eModeViewDocument.AnnullateInvoices, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón de imprimir tirilla
        Me.MbtnSmallPrintAll.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ImprimirTirilla)), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'Botón de imprimir detallado
        Me.MbtnLargePrintAll.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ImprimirDetallado)), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)

        'Botón de imprimir Imprimir Detalle y Cuenta Madre
        Me.MbtnPrintDetailandParentAccount.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(_auxAdmissionToReload.Status.ToString.Equals("F"), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)

        'Sub menu de imprimir
        Me.MbtnPrintAll.Visibility = If(Me.MbtnSmallPrintAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always OrElse Me.MbtnLargePrintAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        'folios sin liquidar:
        Dim allFoliosFactured As Boolean = documento.Count = documento.Where(Function(d) CType(d.Control, ICtrFolio).Status = 2).Count()
        MbtnFacturarIngreso.Caption = If(documento.Count = 0, "Cerrar Ingreso", "Facturar Ingreso")
        MbtnFacturarIngreso.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.Liquidar)), If(Not _auxAdmissionToReload.Status.ToString.Equals("F"), If(allFoliosFactured, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        MBtnIngresosRelacionados.Visibility = If(Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.RelationsAdmissions)), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        'bloquear ingreso
        Me.MbtnIncomeLock.Visibility = If(Me._auxAdmissionToReload IsNot Nothing AndAlso Me._auxAdmissionToReload.Status IsNot Nothing, If(Me._auxAdmissionToReload.Status = " " Or Me._auxAdmissionToReload.Status = "P", DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'desbloqueaer ingreso
        Me.MbtnIncomeUnlock.Visibility = If(Me._auxAdmissionToReload IsNot Nothing AndAlso Me._auxAdmissionToReload.Status IsNot Nothing, If(Me._auxAdmissionToReload.Status = "B", DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
        'visualizar el reporte de cuenta madre
        Me.MbtnShowMotherAccountReport.Visibility = If(Me._auxAdmissionToReload IsNot Nothing, If(Me._auxAdmissionToReload.IsMasterAccount, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never), DevExpress.XtraBars.BarItemVisibility.Never)
    End Sub

    ''' <summary>
    ''' Aqui se agrega un nuevo folio vacío
    ''' </summary>
    Private Async Sub BteCountFolio_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BteCountFolio.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.IsBusy = True
            Using model As New MLiquidation()
                Dim objParams As Object = New ExpandoObject()
                objParams.IdAdmission = Me._auxAdmissionToReload.Id
                objParams.CreationUser = Infrastructure.CrossCutting.Base.SessionValues.Instance.UserIndigo
                Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.AddNewFolio, objParams)
                Me.IsBusy = False
                If res.StateResult Then
                    Me.RefreshAdmission()
                Else
                    If res.Message IsNot Nothing AndAlso Not res.Message.Equals(String.Empty) Then
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageDontCanDeleteFolio", MODULE_NAME)
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Aqui se recalcula el ancho del documento
    ''' </summary>
    Private Sub WidgetView_DocumentAdded(sender As Object, e As DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs) Handles WidgetView.DocumentAdded
        Me.RecalcWithFolios()
        Me.BteCountFolio.EditValue += 1
    End Sub

    ''' <summary>
    ''' Aqui se recalcula el ancho del documento
    ''' </summary>
    Private Sub WidgetView_DocumentClosed(sender As Object, e As DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs) Handles WidgetView.DocumentClosed

    End Sub

    ''' <summary>
    ''' Se manda a eliminar el folio
    ''' </summary>
    Private Async Sub WidgetView_DocumentClosing(sender As Object, e As DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventArgs) Handles WidgetView.DocumentClosing
        Dim currentFolioId = CType(e.Document.Control, CtrFolio).Id
        Using model As New MDashBoardPharmacy(Me.Tag)
            Dim details = model.ListDashBoardPharmacyByPatient(_auxAdmissionToReload.PatientCode, _auxAdmissionToReload.AdmissionCode)
            'Validamos que hayan más folios disponibles
            Dim hasFolios = _auxAdmissionToReload.ListRevenueControlDetails?.Any(Function(a) a.Id <> currentFolioId AndAlso a.Status = 1)
            If Not hasFolios AndAlso details IsNot Nothing AndAlso details.Count > 0 Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "No se puede eliminar el folio por que existen solicitudes de farmacia, pendientes por dispensar"
                Me.RefreshAdmission()
                Exit Sub
            End If
        End Using
        If MessageIndigo.Show(ResourceManager.GetString("BodyMessageQuestionDeleteFolio", MODULE_NAME), MessageType.Question, ResourceManager.GetString("TitleMessageQuestionDeleteFolio", MODULE_NAME), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.IsBusy = True
            Using model As New MLiquidation()
                Dim objParams As Object = New ExpandoObject()
                objParams.IdFolio = currentFolioId 'CType(e.Document.Control, CtrFolio).Id
                Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.DeleteEmptyFolio, objParams)
                Me.IsBusy = False
                If Not res.StateResult Then
                    If res.Message IsNot Nothing AndAlso Not res.Message.Equals(String.Empty) Then
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageDontCanDeleteFolio", MODULE_NAME)
                    End If
                End If
                Me.PnlCtrlHeader.Visible = True
                Me.RefreshAdmission()
            End Using
        Else
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Recarga propiedades de los folios de acuerdo a eventos en el contenedor
    ''' </summary>
    Private Sub CtrFolio_GetStatusLoadStays(sender As Object)
        DirectCast(sender, CtrFolio).LoadingStays = _loadingStays
    End Sub

    ''' <summary>
    ''' Aqui se manda a recargas las definiciones de los demás control de folio
    ''' cuando uno de ellos ha modificado el layout de sus vistas
    ''' </summary>
    Private Sub CtrFolio_GridViewLayoutsChanged(sender As Object, e As EventArgs)
        For Each doc In Me.DocumentManager.View.Documents
            If DirectCast(doc.Control, CtrFolio).Id <> DirectCast(sender, CtrFolio).Id Then
                DirectCast(doc.Control, CtrFolio).LoadGridViewDefinitions()
            End If
        Next
    End Sub

    ''' <summary>
    ''' Aqui se realiza lógica cuando finaliza la carga de datos de un folio
    ''' </summary>
    Private Sub CtrFolio_LoadDatasourceEnd(sender As Object, e As LoadDatasourceEndEventArgs)
        If Not e.CtrFolio.IsInvoiced OrElse e.CtrFolio.Status = 3 Then
            Me.TotalEntity = Me.TotalEntity + e.CtrFolio.TotalEntity
            Me.TotalPatient = Me.TotalPatient + e.CtrFolio.TotalPatient
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la resta de los totales del folio antes de recargar el datasource de nuevo
    ''' </summary>
    Private Sub CtrFolio_BeginReloadDatasource(sender As Object, e As BeginReloadDatasourceEventArgs)
        If Not e.CtrFolio.IsInvoiced OrElse e.CtrFolio.Status = 3 Then
            Me.TotalEntity = If((Me.TotalEntity - e.CtrFolio.TotalEntity) < 0, 0, Me.TotalEntity - e.CtrFolio.TotalEntity)
            Me.TotalPatient = If((Me.TotalPatient - e.CtrFolio.TotalPatient) < 0, 0, Me.TotalPatient - e.CtrFolio.TotalPatient)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se manda a recargar el folio requerido
    ''' </summary>
    Private Async Sub CtrFolio_RequiereReloadFolio(sender As Object, e As RequiereReloadFolioEventArgs)
        Dim doc = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id = e.IdTargetFolio OrElse CType(d.Control, CtrFolio).Id = CType(sender, CtrFolio).Id).ToList()
        Using model As New MLiquidation()
            Dim res = Await model.GetControlPOCOByCode(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
            If res.StateResult Then
                If res.ObjectEmbbeded IsNot Nothing Then
                    Dim listCurrent As List(Of FolioDataDetail) = res.ObjectEmbbeded.ListRevenueControlDetails
                    For Each docFolio In doc
                        Dim folio As CtrFolio = CType(docFolio.Control, CtrFolio)
                        If folio IsNot Nothing Then
                            folio.SetSleNullText(Me.INDSleAdmissionNumber2.DisplayNullText)
                            Dim listCurrentFolio = listCurrent.Where(Function(x) x.Id = folio.Id).FirstOrDefault()
                            If listCurrentFolio IsNot Nothing Then
                                folio.SetDatasourceAsync(listCurrentFolio.Id, listCurrentFolio.Status, False, e.NullCategories)
                                folio.IsAsyncOperation(False)
                            Else
                                INDSleAdmissionNumber2_KeyDown(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            Else
                ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Aqui se manda a recargar el folio requerido
    ''' </summary>
    Private Async Sub CtrFolio_RequiereReloadFolioList(sender As Object, e As RequiereReloadFolioListEventArgs)
        For Each folioId As Integer In e.ListIdTargetFolio
            Dim doc = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id = folioId).First()
            Using model As New MLiquidation()
                Dim res = Await model.GetControlPOCOByCode(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
                If res.StateResult Then
                    If res.ObjectEmbbeded IsNot Nothing Then
                        Dim currentFolio As CtrFolio = CType(doc.Control, CtrFolio)
                        currentFolio.SetSleNullText(Me.INDSleAdmissionNumber2.DisplayNullText)
                        Dim listCurrent As List(Of FolioDataDetail) = res.ObjectEmbbeded.ListRevenueControlDetails
                        Dim listCurrentFolio = listCurrent.Where(Function(x) x.Id = currentFolio.Id).FirstOrDefault()
                        currentFolio.SetDatasourceAsync(listCurrentFolio.Id, listCurrentFolio.Status)
                        currentFolio.IsAsyncOperation(False)
                    End If
                    LoadCountAnnullateInvoices(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
                    UpdateStatusAdmission()
                Else
                    ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            End Using
        Next
    End Sub

    Public Async Sub UpdateStatusAdmission()
        If _auxAdmissionToReload Is Nothing Then
            Exit Sub
        End If
        Using model As New MLiquidation
            Dim NewStatus As String = Await model.GetAdmissionStatusByNumIngresAsync(AdmissionNumber)
            _auxAdmissionToReload.Status = NewStatus
            Me.TxtStatusAdmission.Text = _statusName.Item(NewStatus.Trim())
            Me.TxtStatusAdmission.BackColor = GetStatusByCode(_auxAdmissionToReload.Status.ToString())
        End Using
    End Sub

    ''' <summary>
    ''' Aqui se recarga el ingreso completo
    ''' </summary>
    Private Sub CtrFolio_RequiereReloadAdmission(sender As Object, e As EventArgs)
        Me.RefreshAdmission()
    End Sub

    ''' <summary>
    ''' Handles the RequiereReloadFolioWithPrint event of the CtrFolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RequiereReloadFolioEventArgs"/> instance containing the event data.</param>
    Private Async Sub CtrFolio_RequiereReloadFolioWithPrint(sender As Object, e As RequiereReloadFolioEventArgs)
        Dim doc = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id = e.IdTargetFolio).FirstOrDefault()
        Using model As New MLiquidation()
            Dim res = Await model.GetControlPOCOByCode(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
            If res.StateResult Then
                If res.ObjectEmbbeded IsNot Nothing Then
                    Dim currentFolio As CtrFolio = CType(doc.Control, CtrFolio)
                    currentFolio.SetSleNullText(Me.INDSleAdmissionNumber2.DisplayNullText)
                    Dim listCurrent As List(Of FolioDataDetail) = res.ObjectEmbbeded.ListRevenueControlDetails
                    Dim listCurrentFolio = listCurrent.Where(Function(x) x.Id = currentFolio.Id).FirstOrDefault()
                    currentFolio.SetDatasourceAsync(listCurrentFolio.Id, listCurrentFolio.Status, True)
                    'currentFolio.PrintDetailReport()
                    UpdateStatusAdmission()
                End If
            Else
                ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Aqui se controla el evento maximizar de cada uno de los documentos
    ''' </summary>
    Private Sub Document_Maximized(sender As Object, e As EventArgs)
        Me.PnlCtrlHeader.Visible = False
        Dim doc = DirectCast(sender, DevExpress.XtraBars.Docking2010.Views.Widget.Document)
        doc.CustomHeaderButtons(0).Properties.Visible = False
        DirectCast(doc.Control, CtrFolio).PopUp = Me.PccAdmission
        DirectCast(doc.Control, CtrFolio).SetSleNullText(Me.INDSleAdmissionNumber2.DisplayNullText)
    End Sub

    ''' <summary>
    ''' Hides the folios.
    ''' </summary>
    Private Sub HideFolios()
        Dim needResize As Boolean = False
        Try
            For Each control In hideContainerBottom.Controls
                Dim folio As CtrFolio = CType(CType(control, CtrMinimizedFolio).Tag, CtrFolio)
                CType(DocumentManager.View.Documents.Where(Function(o) CType(o.Control, CtrFolio).Id = folio.Id).FirstOrDefault().Control, CtrFolio).Hide()
                control.Tag = DocumentManager.View.Documents.Where(Function(o) CType(o.Control, CtrFolio).Id = folio.Id).FirstOrDefault().Control
                needResize = True
            Next
        Catch ex As Exception

        End Try
        If needResize Then
            ResizeFolioWindow()
        End If
    End Sub

    Dim locationFolioMinimize As Integer
    ''' <summary>
    ''' Minimiza el folio
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.Docking2010.ButtonEventArgs"/> instance containing the event data.</param>
    Private Sub Document_Minimized(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs)
        Dim folio As CtrFolio = CType(CType(DocumentManager.View.ActiveDocument, DevExpress.XtraBars.Docking2010.Views.Widget.Document).Control, CtrFolio)
        folio.Hide()
        Dim minimizedFolio As New CtrMinimizedFolio()
        minimizedFolio.Status = folio.Status
        minimizedFolio.Tag = folio
        If locationFolioMinimize < 120 Then
            locationFolioMinimize = 120
        End If
        minimizedFolio.Location = New System.Drawing.Point(locationFolioMinimize, 2)
        locationFolioMinimize += minimizedFolio.Width + 3
        AddHandler minimizedFolio.LblInvoice.Click, AddressOf RestoreFolioWindow
        Try
            If folio.Status = 2 OrElse folio.Status = 4 Then
                minimizedFolio.FolioNumber = folio.InvoiceNumber
            Else
                minimizedFolio.FolioNumber = folio.LblFolioTitle.Text
            End If
            minimizedFolio.TotalEntity = folio.TotalEntity
            minimizedFolio.TotalPatient = folio.TotalPatient
            hideContainerBottom.Controls.Add(minimizedFolio)
        Catch ex As Exception
        End Try
        ResizeFolioWindow()
    End Sub

    Private Sub Document_Minimized_MouseEnter(sender As Object, e As EventArgs)
        CType(sender, ButtonDarl).Image = Global.Presentation.Billing.My.Resources.Resources.minimizar_negro
    End Sub

    Private Sub Document_Minimized_Leave(sender As Object, e As EventArgs)
        CType(sender, ButtonDarl).Image = Global.Presentation.Billing.My.Resources.Resources.minimizar_gris
    End Sub

    Private Sub Document_Minimized_GotFocus(sender As Object, e As EventArgs)
        CType(sender, ButtonDarl).Image = Global.Presentation.Billing.My.Resources.Resources.minimizar_gris
    End Sub

    Private Sub CtrFolio_GotFocus(sender As Object, e As EventArgs)
        CType(sender, ButtonDarl).Image = Global.Presentation.Billing.My.Resources.Resources.minimizar_blanco
    End Sub

    ''' <summary>
    ''' Restores the folio window.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RestoreFolioWindow(sender As Object, e As EventArgs)
        Dim minimizeControl As CtrMinimizedFolio = CType(CType(sender, LabelControl).Parent, CtrMinimizedFolio)
        CType(CType(CType(sender, LabelControl).Parent, Presentation.Billing.CtrMinimizedFolio).Tag, CtrFolio).Show()
        hideContainerBottom.Controls.Remove(CType(CType(sender, LabelControl).Parent, Presentation.Billing.CtrMinimizedFolio))
        ResizeFolioWindow()
        RePositionItemsMinimizeToolbar(minimizeControl.Width)
    End Sub

    ''' <summary>
    ''' Res the position items minimize toolbar.
    ''' </summary>
    ''' <param name="withMinimizeControl">The with minimize control.</param>
    Private Sub RePositionItemsMinimizeToolbar(withMinimizeControl As Integer)
        locationFolioMinimize = 120
        For Each control In hideContainerBottom.Controls
            control.Location = New System.Drawing.Point(locationFolioMinimize, 2)
            locationFolioMinimize += withMinimizeControl + 3
        Next
    End Sub

    ''' <summary>
    ''' Aqui se control el evento restaurar de cada uno de los documentos
    ''' </summary>
    Private Sub Document_Restored(sender As Object, e As EventArgs)
        Me.INDSleAdmissionNumber2.PopupContainerControl = Me.PccAdmission
        Me.PnlCtrlHeader.Visible = True
        Dim doc = DirectCast(sender, DevExpress.XtraBars.Docking2010.Views.Widget.Document)
        doc.CustomHeaderButtons(0).Properties.Visible = True
        DirectCast(doc.Control, CtrFolio).SetSleNullText(String.Empty)
    End Sub

    Private Sub ResizeFolioWindow()
        For Each doc As DevExpress.XtraBars.Docking2010.Views.Widget.Document In Me.DocumentManager.View.Documents
            Me.SuspendLayout()
            If doc.IsVisible Then
                If (Me.DocumentManager.View.Documents.Count - (hideContainerBottom.Controls.Count)) = 1 Then
                    CType(doc.Control, CtrFolio).IsUnique = True
                    CType(doc.Control, CtrFolio).SetMainView()
                Else
                    CType(doc.Control, CtrFolio).IsUnique = False
                    CType(doc.Control, CtrFolio).SetMainView()
                End If
                doc.Width = Me.CalcWidthFolios(Me.DocumentManager.View.Documents.Count - (hideContainerBottom.Controls.Count))
            End If
            Me.ResumeLayout()
        Next
    End Sub

    Private Sub FrmLiquidation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleAdmissionNumber2.Focus()
    End Sub

    Private Sub FrmLiquidation_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        BaseClass.FreeMemory()
    End Sub

    ''' <summary>
    ''' Se realiza la carga del frontal
    ''' </summary>
    Private Async Sub FrmLiquidation_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()
        Me.LoadGridViewDefinitions()
        _openAdmissions = True
        Me._notificationItemCollection = New List(Of NotificationItem)()
        Me.GdcNotifications.DataSource = Me._notificationItemCollection
        Me.DdbMenu.StyleController = Nothing
        Me.DdbMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.DdbMenu.LookAndFeel.UseDefaultLookAndFeel = False
        'Inicializamos el presenter
        Me._presenter = New PLiquidation(Me)
        Me._presenter.LoadPermissionsForm()
        Await Me._presenter.LoadListOperatingUnit()
        Await Me._presenter.LoadFlagTaxInclude()
        Me._presenter.LoadListBillingAuthorization()
        'Configuramos los controles CtrButtonEditWithPopUp
        Me.PrepareCtrButtonEditWithPopUp()
        'Cargamos el Datasource
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSleAdmissionNumber2_KeyDown
        'Verificamos si el modo busqueda esta configurado
        If SessionValues.Instance.UserViewMode AndAlso _auxAdmissionToReload Is Nothing Then
            Me.ShowSearch()
        End If
        If _auxAdmissionToReload IsNot Nothing Then
            INDSleAdmissionNumber2.EditValue = _auxAdmissionToReload.AdmissionCode
            Dim evt As New EditValueChangedEventArgs(Nothing, AuxAdmissionToReload)
            INDSleAdmissionNumber2_NewSelectedValue(Me, evt)
        End If
        Me.ResumeLayout()
    End Sub

    Public Function GetStatusByCode(code As String) As Color
        Select Case code
            Case " "
                Return Color.FromArgb(0, 70, 109)
            Case "F"
                Return Color.Green
            Case "A"
                Return Color.OrangeRed
            Case "C"
                Return Color.FromArgb(128, 128, 128)
            Case "P"
                Return Color.FromArgb(168, 188, 52)
            Case "B"
                Return Color.FromArgb(202, 81, 0)
            Case Else
                Return Color.Transparent
        End Select
    End Function

    Private Async Sub INDSleAdmissionNumber2_NewSelectedValue(sender As Object, e As EditValueChangedEventArgs) Handles INDSleAdmissionNumber2.EditValueChanged
        Try
            If e.NewObject IsNot Nothing AndAlso e.NewObject.GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
                If OperatingUnitSelected Is Nothing Then
                    ShowMessage(EeventViewerImages.Advertencia) = "Por favor seleccione una unidad operativa"
                    INDSleAdmissionNumber2.EditValue = Nothing
                    Me.IsBusy = False
                    Exit Sub
                End If

                Me.IsBusy = True
                Me.PnlCtrlHeader.Visible = True
                Me.PnlBodyDashboard.Visible = False
                Me.PnlBodySearch.Visible = True
                Me.PnlBodyDashboard.BringToFront()
                'Consultamos de uns sp los ingresos
                _auxAdmissionToReload = Await Task.Factory.StartNew(Function() Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                                            .CrystalService.Liquidation_GetAdmission(e.NewObject.AdmissionCode.ToString()))
                If _auxAdmissionToReload.Status <> "F" AndAlso _auxAdmissionToReload.ThirdPartyPatientId Is Nothing Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = $"El paciente {e.NewObject.PatientCode.ToString()} - {e.NewObject.PatientName.ToString()} no está creado como tercero en Indigo VIE"
                    INDSleAdmissionNumber2.EditValue = Nothing
                    Me.IsBusy = False
                    Exit Sub
                End If
                BteCountFolio.Enabled = Not (_auxAdmissionToReload.Status = "F" OrElse _auxAdmissionToReload.Status = "C")
                Dim errorList As New StringBuilder()
                If _auxAdmissionToReload.AdmissionCaregroupId Is Nothing OrElse CInt(_auxAdmissionToReload.AdmissionCaregroupId) = 0 Then
                    errorList.AppendLine("La admisión no tiene asociada un grupo de atención para Indigo VIE")
                End If
                If _auxAdmissionToReload.PatientCareGroupId Is Nothing OrElse CInt(_auxAdmissionToReload.PatientCareGroupId) = 0 Then
                    errorList.AppendLine("El paciente no tiene asociado un grupo de atención para Indigo VIE")
                End If
                If _auxAdmissionToReload.HealthAdministratorId Is Nothing OrElse CInt(_auxAdmissionToReload.HealthAdministratorId) = 0 Then
                    errorList.AppendLine("La admisión no tiene asociado una entidad administradora para Indigo VIE")
                End If

                If errorList.Length > 0 Then
                    Me.IsBusy = False
                    ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
                    Exit Sub
                End If
                If sender.GetType().Name = GetType(SearchLookUpEditExAdmission).Name Then
                    'reestablecemos los folios minimizados cuando se selecciona otro ingreso
                    hideContainerBottom.Controls.Clear()
                    locationFolioMinimize = 0
                End If

                Me.TxtStatusAdmission.Text = _auxAdmissionToReload.StatusName
                Me.TxtStatusAdmission.BackColor = GetStatusByCode(_auxAdmissionToReload.Status.ToString())
                Me.INDSleAdmissionNumber2.DisplayNullText = String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"),
                        _auxAdmissionToReload.AdmissionCode.ToString().Trim(),
                        _auxAdmissionToReload.PatientCode.ToString().Trim(),
                        _auxAdmissionToReload.PatientName.ToString().Trim())

                'Y aqui consultamos el numero de ingreso seleccionado
                Using m As New MLiquidation()
                    'Limpiamos los controles
                    Me.CleanControls(False)
                    'Buscamos si el item es de tipo oncología si tiene ingresos relacionados
                    Using model As New MLiquidation()
                        Dim dtResult As DataTable = Await model.ExecuteCommandDt(
                            $"SELECT * FROM {SessionValues.Instance.HisContainer}.dbo.ADINGRESO WHERE IPCODPACI = '{_auxAdmissionToReload.PatientCode.ToString()}' And (IESTADOIN = ' ' Or IESTADOIN = 'P')", SessionValues.Instance.HisContainer)
                        If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then
                            MBtnIngresosRelacionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        Else
                            MBtnIngresosRelacionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                        End If
                    End Using

                    'Cargamos las facturas anuladas si existen
                    Me.LoadCountAnnullateInvoices(Me._auxAdmissionToReload.AdmissionCode)
                    Me._healthAdministratorAdmissionId = _auxAdmissionToReload.HealthAdministratorId
                    'Asignamos los datos a los diferentes controles
                    AdmissionNumber = _auxAdmissionToReload.AdmissionCode.ToString()
                    'DATOS DEL PACIENTE
                    Dim documentType As String = String.Empty
                    Select Case _auxAdmissionToReload.PatientDocumentType.ToString().Trim()
                        Case "1"
                            documentType = "CC"
                        Case "2"
                            documentType = "CE"
                        Case "3"
                            documentType = "TI"
                        Case "4"
                            documentType = "RC"
                        Case "5"
                            documentType = "PA"
                        Case "6"
                            documentType = "AS"
                        Case "7"
                            documentType = "MS"
                        Case "8"
                            documentType = "NU"
                    End Select
                    Me.TxtPatientCode.Text = If(_auxAdmissionToReload.PatientCode Is Nothing, String.Empty, String.Concat(documentType, " - ", _auxAdmissionToReload.PatientCode.ToString().Trim()))
                    Me.TxtPatientName.Text = If(_auxAdmissionToReload.PatientName Is Nothing, String.Empty, _auxAdmissionToReload.PatientName.ToString().Trim())
                    Me.TxtPatientBirth.Text = Convert.ToDateTime(Me._auxAdmissionToReload.PatientBirth).ToString(SessionValues.Instance.Culture)
                    Me.TxtPatientAge.Text = Utils.AgeToString(Convert.ToDateTime(Me._auxAdmissionToReload.PatientBirth))
                    Me.TxtPatientType.EditValue = Convert.ToInt32(Me._auxAdmissionToReload.PatientType)
                    Me.TxtAfiliationType.EditValue = Convert.ToInt32(Me._auxAdmissionToReload.PatientAfiliation)
                    Me.TxtPatientEstrato.Text = (Me._auxAdmissionToReload.NivelCode & " - " & Me._auxAdmissionToReload.NivelName)
                    'Me.TxtContacto

                    'DATOS DEL INGRESO
                    Me.TxtAdmissionCode.Text = Me._auxAdmissionToReload.AdmissionCode.ToString().Trim()
                    Me.TxtAdmissionDate.Text = Convert.ToDateTime(Me._auxAdmissionToReload.AdmissionDate).ToString(SessionValues.Instance.Culture)
                    Me.TxtEntityNameAdmission.Text = Me._auxAdmissionToReload.EntityName
                    Select Case _auxAdmissionToReload.AdmissionRiskType
                        Case "1"
                            Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
                        Case "2"
                            Me.TxtRiskType.Text = "Accidente de Tránsito"
                        Case "3"
                            Me.TxtRiskType.Text = "Catástrofe"
                        Case "4"
                            Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
                        Case "5"
                            Me.TxtRiskType.Text = "Accidente de Trabajo"
                        Case "6"
                            Me.TxtRiskType.Text = "Enfermedad Profesional"
                        Case "7"
                            Me.TxtRiskType.Text = "Atención Inicial de Urgencias"
                        Case "8"
                            Me.TxtRiskType.Text = "Otro Tipo de Accidente"
                        Case "9"
                            Me.TxtRiskType.Text = "Lesión Por Agresión"
                        Case "10"
                            Me.TxtRiskType.Text = "Lesión AutoInfligida"
                        Case "11"
                            Me.TxtRiskType.Text = "Maltrato Físico"
                        Case "12"
                            Me.TxtRiskType.Text = "Promoción y Prevención"
                        Case "13"
                            Me.TxtRiskType.Text = "Otro"
                        Case "14"
                            Me.TxtRiskType.Text = "Accidente Rabico"
                        Case "15"
                            Me.TxtRiskType.Text = "Accidente Ofídico"
                        Case "16"
                            Me.TxtRiskType.Text = "Sopecha de Abuso Sexual"
                        Case "17"
                            Me.TxtRiskType.Text = "Sopecha de Violencia Sexual"
                        Case "18"
                            Me.TxtRiskType.Text = "Sopecha de Maltrato Emocional"
                    End Select
                    Me.TxtPlaceEntry.Text = ResourceManager.GetString(PREFIX_PLACEENTRY & Me._auxAdmissionToReload.PlaceEntry.ToString(), "IndigoCrystalHis")
                    Me.TxtAdmissionType.EditValue = Convert.ToInt32(_auxAdmissionToReload.AdmissionType)
                    Me.TxtBedStay.Text = If(_auxAdmissionToReload.BedStay Is Nothing, String.Empty, _auxAdmissionToReload.BedStay.ToString().Trim())
                    Me.TxtLiquidationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.LiquidationType)
                    Me.TxtAuthorization.Text = Me._auxAdmissionToReload.AuthorizationNumber.ToString().Trim()
                    Me.TxtAtentionCenter.Text = Me._auxAdmissionToReload.AdmissionCentAtencCodeName
                    Me.TxtFunctionalUnitAdmission.Text = Me._auxAdmissionToReload.AdmissionUniFuncCodeName.ToString().Trim()
                    Me.TxtResponsibleName.Text = If(_auxAdmissionToReload.ResponsibleName Is Nothing, String.Empty, _auxAdmissionToReload.ResponsibleName.ToString().Trim())
                    Me.TxtResponsiblePhone.Text = Me._auxAdmissionToReload.ResponsiblePhone

                    'OTROS
                    Me.TxtAdmissionDate1.Text = Convert.ToDateTime(Me._auxAdmissionToReload.AdmissionDate).ToString(SessionValues.Instance.Culture)
                    Me.TxtAdmissionType1.EditValue = Convert.ToInt32(_auxAdmissionToReload.AdmissionType)
                    Me.TxtPlaceEntry1.Text = ResourceManager.GetString(PREFIX_PLACEENTRY & Me._auxAdmissionToReload.PlaceEntry.ToString(), "IndigoCrystalHis")

                    Dim res = Await m.GetRevenueControlPOCOByCodeAsync(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
                    If res.StateResult Then
                        _auxAdmissionToReload.Id = res.ObjectEmbbeded.Id
                        _auxAdmissionToReload.FolioQuantity = res.ObjectEmbbeded.FolioQuantity
                        _auxAdmissionToReload.LiquidationTypeName = res.ObjectEmbbeded.LiquidationTypeName
                        _auxAdmissionToReload.ContractCodeName = res.ObjectEmbbeded.ContractCodeName
                        _auxAdmissionToReload.ListRevenueControlDetails = res.ObjectEmbbeded.ListRevenueControlDetails
                        If Me._isOncologicalMode AndAlso _auxAdmissionToReload.ListRevenueControlDetails IsNot Nothing Then
                            _auxAdmissionToReload.ListRevenueControlDetails = _auxAdmissionToReload.ListRevenueControlDetails.Where(Function(rcd) rcd.Status = 1).ToList()
                        End If
                    Else
                        Me.IsBusy = False
                        ShowMessage(EeventViewerImages.Advertencia) = res.Message
                        Exit Sub
                    End If
                    If Me._auxAdmissionToReload IsNot Nothing Then
                        If CInt(_auxAdmissionToReload.CareGroupTypePatient) <> -1 Then
                            Select Case CByte(_auxAdmissionToReload.CareGroupTypePatient)
                                Case 1, 2, 4 'EAPB con contrato
                                    Me.TxtPatientEntityName.Text = String.Concat(Me._auxAdmissionToReload.PatientEntityCode, " - ", Me._auxAdmissionToReload.PatientEntity)
                                Case 3 'Particulares
                                    LiEntity.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", GetType(CtrFolio).Name)
                                    'tercero
                                    Me.TxtPatientEntityName.Text = _auxAdmissionToReload.PatientEntity
                            End Select
                            Me.TxtCareGroupPatient.Text = _auxAdmissionToReload.CareGroupCodeName
                        End If
                        Me.TxtCareGroupAdmission.Text = _auxAdmissionToReload.AdmissionCareGroupCodeName
                        'Cargamos los folios
                        If _modeViewDocument = eModeViewDocument.AnnullateInvoices Then
                            LoadAnnullateInvoices()
                        Else
                            _modeViewDocument = eModeViewDocument.Folios
                            Me.LoadFoliosAsync(Me._auxAdmissionToReload.ListRevenueControlDetails)
                        End If
                    End If
                    Me.IsBusy = False
                    Me.PnlBodyDashboard.Visible = True
                    Me.PnlBodySearch.Visible = False
                End Using
            End If
        Catch
            Me.IsBusy = False
            Me.PnlBodyDashboard.Visible = True
            Me.PnlBodySearch.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmLiquidation control.
    ''' </summary>
    Private Sub FrmLiquidation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape AndAlso Me.INDSleAdmissionNumber2.EditValue IsNot Nothing Then
            If MessageIndigo.Show("Esta seguro que desea cerrar el formulario?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub TxtPatientCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles TxtPatientCode.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPatient
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    Private Sub TxtAdmissionCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles TxtAdmissionCode.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmAdmissions
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

#Region "Stays"

    ''' <summary>
    ''' Aqui se le da formato a las celdas
    ''' </summary>
    Private Sub GdvBeds_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GdvStays.CustomDrawCell, GdvStaysLiquidated.CustomDrawCell, GdvStayDetails.CustomDrawCell
        If DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView).Equals(Me.GdvStaysLiquidated) Then 'Estancias liquidadas
            Dim obj = CType(Me.GdvStaysLiquidated.GetRow(e.RowHandle), CHREGESTA)
            If e.Column.Equals(Me.ColEstLiqInitDate) Then
                e.DisplayText = Convert.ToDateTime(obj.FECINIEST).ToString(SessionValues.Instance.Culture)
            ElseIf e.Column.Equals(Me.ColEstLiqEndDate) Then
                e.DisplayText = Convert.ToDateTime(obj.FECFINEST).ToString(SessionValues.Instance.Culture)
            ElseIf e.Column.Equals(Me.ColEstLiqLiqType) Then
                e.DisplayText = (ResourceManager.GetString(PREFIX_STAYLIQUIDATIONTYPE & obj.GENESTLIQ, MODULE_NAME))
            ElseIf e.Column.Equals(Me.ColEstLiqType) Then
                e.DisplayText = If(obj.CHTIPESTA.DESTIPEST IsNot Nothing, obj.CHTIPESTA.DESTIPEST.Trim(), String.Empty)
            End If
        ElseIf DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView).Equals(Me.GdvStays) Then 'No liquidadas
            Dim obj = CType(Me.GdvStays.GetRow(e.RowHandle), CHREGESTA)
            If e.Column.Equals(Me.ColEstInitDate) Then
                e.DisplayText = Convert.ToDateTime(obj.FECINIEST).ToString(SessionValues.Instance.Culture)
            ElseIf e.Column.Equals(Me.ColEstEndDate) Then
                'Si es el ultimo registro
                If Object.ReferenceEquals(obj, CType(Me.GdcStays.DataSource, List(Of CHREGESTA))(CType(Me.GdcStays.DataSource, List(Of CHREGESTA)).Count - 1)) Then
                    If obj.FECFINEST < obj.FECINIEST Then
                        e.DisplayText = ResourceManager.GetString("MessageStayOpened", MODULE_NAME)
                    Else
                        e.DisplayText = Convert.ToDateTime(obj.FECFINEST).ToString(SessionValues.Instance.Culture)
                    End If
                Else
                    e.DisplayText = Convert.ToDateTime(obj.FECFINEST).ToString(SessionValues.Instance.Culture)
                End If
            ElseIf e.Column.Equals(Me.ColEstType) Then
                e.DisplayText = If(obj.CHTIPESTA.DESTIPEST IsNot Nothing, obj.CHTIPESTA.DESTIPEST.Trim(), String.Empty)
            End If
        Else 'Detalle de estancias
            Dim obj = CType(Me.GdvStayDetails.GetRow(e.RowHandle), CHREGESTADET)
            If e.Column.Equals(Me.ColEstLiqDetDate) Then
                e.DisplayText = Convert.ToDateTime(obj.GENLIQUIDA).ToString(SessionValues.Instance.Culture)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se asigna el popUp al repositorio de la rejilla que corresponda,
    ''' asi como el datasource al popUp de la rejilla que corresponda
    ''' </summary>
    Private Sub RepEstShowDetails_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles RepEstShowDetails.QueryPopUp, RepEstLiqShowDetails.QueryPopUp
        Dim rep = CType(sender, DevExpress.XtraEditors.PopupContainerEdit)
        rep.Properties.PopupControl = Me.PccStayDetails
        Dim obj = If(rep.Properties.Tag.Equals(Me.RepEstShowDetails.Tag), CType(Me.GdvStays.GetFocusedRow(), CHREGESTA), CType(Me.GdvStaysLiquidated.GetFocusedRow(), CHREGESTA))
        Me.GdcStayDetails.DataSource = obj.CHREGESTADET
        Me.GdcStayDetails.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Aqui se asigna el datasource al popUp de la rejilla
    ''' </summary>
    Private Sub RepEstLiqShowFolios_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles RepEstLiqShowFolios.QueryPopUp
        Dim obj = CType(Me.GdvStaysLiquidated.GetFocusedRow(), CHREGESTA)
        Me.GdcStayFolios.DataSource = obj.ListFolios
        Me.GdcStayFolios.RefreshDataSource()
    End Sub

    Private _oldStateOpen As Boolean = False
    Private Sub SleFindAdmission_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAdmissionNumber2.QueryPopUp
        If _openAdmissions Then
            If _oldStateOpen <> _openAdmissions Then
                Using m As New MLiquidation()
                    Me.INDSleAdmissionNumber2.Datasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                        .CrystalService.Liquidation_GetAdmission(DevExpress.Data.Filtering.CriteriaOperator.Parse("Status = ' ' Or Status = 'P' or Status= 'B'"))
                End Using
            End If
        Else
            If (_oldStateOpen <> _openAdmissions) OrElse Me.INDSleAdmissionNumber2.Datasource Is Nothing Then
                Using m As New MLiquidation()
                    Me.INDSleAdmissionNumber2.Datasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                        .CrystalService.Liquidation_GetAdmission(DevExpress.Data.Filtering.CriteriaOperator.Parse("Status = 'F'"))
                End Using
            End If
        End If
        _oldStateOpen = _openAdmissions
    End Sub

#End Region

#Region "Menu Actions"

    ''' <summary>
    ''' Aqui se ejecuta la liquidación de estancias
    ''' </summary>
    Private Sub MbtnLiquidateStays_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLiquidateStays.ItemClick
        'If String.IsNullOrEmpty(StayErrors) Then
        Dim frmLiquidateStays As New FrmLiquidateStays(_auxAdmissionToReload, Me.OperatingUnitSelected.Id)

        'si tiene permiso para liquidar manual las estancias
        If PermissionsForm.ContainsKey(76) Then
            frmLiquidateStays.AllowManualLiquidation = True
        Else
            frmLiquidateStays.AllowManualLiquidation = False
        End If

        'Se verifica si hubo errores al calcular las estancias, para inhabilitar el botón de liquidación automática
        If MbtnLiquidateStays.Tag Is Nothing OrElse MbtnLiquidateStays.Tag = True Then
            frmLiquidateStays.AllowLiquidation = False
        Else
            frmLiquidateStays.AllowLiquidation = True
        End If

        Using tras As New FrmTransparent(frmLiquidateStays, False)
            If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Me.ShowMessage(EeventViewerImages.Informacion) = "Las estancias se liquidaron correctamente"
                Me.RefreshAdmission()
            End If
        End Using
        'Else
        '    ShowMessage(EeventViewerImages.Advertencia) = "Necesita solucionar los problemas con las Estancias: " + vbCrLf + StayErrors
        'End If
    End Sub

    Private Sub MBtnIngresosRelacionados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnIngresosRelacionados.ItemClick
        If AuxAdmissionToReload Is Nothing Then
            Exit Sub
        End If
        If AuxAdmissionToReload.RevenueControlId Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron datos en Facturación para el ingreso seleccionado."
            Exit Sub
        End If

        'Se valida si el ingreso esta en estado cerrado
        If AuxAdmissionToReload.Status = "C" Then
            ShowMessage(EeventViewerImages.Advertencia) = "No se puede ver ingresos relacionados con el ingreso cerrado"
            Exit Sub
        End If

        Using frmR As New FrmAdmissionRelated()
            frmR.AdmissionCode = AuxAdmissionToReload.AdmissionCode
            frmR.PatientCode = AuxAdmissionToReload.PatientCode
            frmR.RevenueControlParentId = AuxAdmissionToReload.RevenueControlId 'AuxAdmissionToReload.Id
            AddHandler frmR.BeginReloadLiquidationForm, AddressOf RefreshAdmission
            Dim transparent As New FrmTransparent(frmR, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the MbtnFlow control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnFlow_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnFlow.ItemClick
        CType(Me.DocumentManager.View, DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView).LayoutMode = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.FlowLayout
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the MbtnNavigation control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnNavigation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnNavigation.ItemClick
        CType(Me.DocumentManager.View, DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView).LayoutMode = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.StackLayout
    End Sub

    Private Async Sub MBtnModifyAuthorization_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnModifyAuthorization.ItemClick
        Using frmAuthorization As New FrmAuthorizationNumber
            frmAuthorization.AuthorizationNumber = Me.TxtAuthorization.Text
            Dim tr As New FrmTransparent(frmAuthorization, False)
            If tr.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Using model As New MLiquidation()
                    Me.PnlCtrlHeader.Visible = True
                    Me.PnlBodyDashboard.Visible = False
                    Me.PnlBodySearch.Visible = True
                    Me.PnlBodyDashboard.BringToFront()
                    Me.IsBusy = True
                    Dim res = Await model.ModifyAuthorizationAdmission(AdmissionNumber, frmAuthorization.AuthorizationNumber)
                    Me.IsBusy = False
                    Me.PnlBodyDashboard.Visible = True
                    Me.PnlBodySearch.Visible = False
                    If res IsNot Nothing Then
                        If res.StateResult Then
                            Me.TxtAuthorization.Text = frmAuthorization.AuthorizationNumber.Trim()
                            Me.ShowMessage(EeventViewerImages.Informacion) = "El número de autorización del Ingreso se ha modificado correctamente"
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                        End If
                    End If
                End Using
            End If
        End Using
    End Sub

    Private Async Sub MbtnFacturarIngreso_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnFacturarIngreso.ItemClick
        Dim newStatusAdmission = If(MbtnFacturarIngreso.Caption = "Cerrar Ingreso", "C", "F")

        If MessageIndigo.Show(String.Format("¿Está seguro que desea {0}?", MbtnFacturarIngreso.Caption), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using model As New MLiquidation()
                Me.PnlCtrlHeader.Visible = True
                Me.PnlBodyDashboard.Visible = False
                Me.PnlBodySearch.Visible = True
                Me.PnlBodyDashboard.BringToFront()
                Dim res = Await model.CloseAdmission(AdmissionNumber)
                Me.IsBusy = False
                Me.PnlBodyDashboard.Visible = True
                Me.PnlBodySearch.Visible = False
                If res IsNot Nothing Then
                    If res.StateResult Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = String.Format("El Ingreso se {0} Correctamente.", If(newStatusAdmission = "C", "cerró", "facturó"))
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                End If
                If _auxAdmissionToReload IsNot Nothing Then
                    _auxAdmissionToReload.Status = newStatusAdmission
                End If
                Me.RefreshAdmission()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Aqui se manda a refrescar el ingreso completo
    ''' </summary>
    Private Sub MbtnRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRefresh.ItemClick
        Me.RefreshAdmission()
    End Sub

    ''' <summary>
    ''' Aqui se muestra el formulario de busqueda
    ''' </summary>
    Private Sub MbtnFind_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnFind.ItemClick
        Me.ShowSearch()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la acción deshacer o limpiar controles
    ''' </summary>
    Private Sub MbtnUndo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnUndo.ItemClick
        Me._auxAdmissionToReload = Nothing
        Me.CleanControls()
    End Sub

    Private Sub MBtnAnulateAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnAnulateAllAmbulatory.ItemClick

        '' ESTE CODIGO PUEDE FUNCIONAR PARA LA FUNCIONALIDAD, REVISARLO.
        'Dim errorList As New StringBuilder()
        'If IdOperatingUnitSelected = 0 Then
        '    errorList.AppendLine("Seleccione una Unidad Operativa")
        'End If
        'If errorList.Length > 0 Then
        '    Me.ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
        '    Exit Sub
        'End If
        'Dim foliosToAnulate = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Status = 2 AndAlso CType(d.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso
        '                                                      CType(CType(d.Control, CtrFolio).GdcServices.DataSource, IList).Count > 0).ToList().Select(Function(o) CType(o.Control, CtrFolio)).ToList()
        'If foliosToAnulate.Count > 0 Then
        '    Dim stringData As String = String.Join("-", foliosToAnulate.OrderBy(Function(x) x.FolioOrder).Select(Function(o) o.InvoiceNumber).ToList().ToArray())
        '    If MessageIndigo.Show(String.Format(ResourceManager.GetString("AnulateFoliosTotal", GetType(CtrFolio).Name), stringData), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        foliosToAnulate(0).AnulateFolio(foliosToAnulate)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la acción bloquear los ingresos
    ''' </summary>}
    Private Async Sub MbtnIncomeLock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnIncomeLock.ItemClick
        'Se trae los parámetros para saber que tipo de bloqueo tiene
        Using model As New MBillingSetting(Me.Tag)
            settingBilling = Await model.GetSettingsBillingByIdUnitOperative(IdOperatingUnitSelected, False)
        End Using

        'En caso de que esté seleccionado tipo farmacia
        If settingBilling IsNot Nothing AndAlso settingBilling.IncomeLockType = 1 Then
            Using model As New MDashBoardPharmacy(Me.Tag)
                Dim details = model.ListDashBoardPharmacyByPatient(_auxAdmissionToReload.PatientCode, _auxAdmissionToReload.AdmissionCode, "0,1,3")
                If details IsNot Nothing AndAlso details.Count > 0 Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "No se puede bloquear el ingreso porque existen solicitudes de farmacia pendientes por dispensar"
                    Exit Sub
                End If
            End Using
        End If

        'En caso de que esté seleccionado tipo facturación
        If settingBilling IsNot Nothing AndAlso settingBilling.IncomeLockType = 2 Then
            Using model As New MAccountControl(Me.Tag)
                Dim details = model.ListAccountControlValidations(_auxAdmissionToReload.AdmissionCode)
                If details IsNot Nothing AndAlso details.Count > 0 Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "No se puede bloquear el ingreso porque existen ítems por generar en control de cuentas hospitalario"
                    Exit Sub
                End If
            End Using
        End If

        'En caso de que esté seleccionado tipo farmacia y facturación
        If settingBilling IsNot Nothing AndAlso settingBilling.IncomeLockType = 3 Then
            Using model As New MDashBoardPharmacy(Me.Tag)
                Dim details = model.ListDashBoardPharmacyByPatient(_auxAdmissionToReload.PatientCode, _auxAdmissionToReload.AdmissionCode, "0,1,3")
                If details IsNot Nothing AndAlso details.Count > 0 Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "No se puede bloquear el ingreso porque existen solicitudes de farmacia pendientes por dispensar"
                    Exit Sub
                End If
            End Using
            Using model As New MAccountControl(Me.Tag)
                Dim details = model.ListAccountControlValidations(_auxAdmissionToReload.AdmissionCode)
                If details IsNot Nothing AndAlso details.Count > 0 Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "No se puede bloquear el ingreso porque existen ítems por generar en control de cuentas hospitalario"
                    Exit Sub
                End If
            End Using
        End If

        Try
            Dim Justification = "El ingreso fue bloqueado por el usuario " + SessionValues.Instance.UserIndigo
            Using m As New MAdmissions(Me.Tag)
                Dim res = Await m.UpdateStatusAdmission(_auxAdmissionToReload.AdmissionCode, "B", Justification)
                If res.StateResult = True Then
                    If res.StateResultAux = False Then
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    Else
                        Me.ShowMessage(EeventViewerImages.Informacion) = "Se actualizó de manera correcta"
                    End If
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Me.RefreshAdmission()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la acción desbloquear los ingresos
    ''' </summary>}
    Private Async Sub MbtnIncomeUnlock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnIncomeUnlock.ItemClick
        Dim admissionStatus As String = ""
        Using model As New MLiquidation()
            Dim res = Await model.GetControlPOCOByCode(_auxAdmissionToReload.AdmissionCode.ToString().Trim())
            If res.StateResult Then
                If res.ObjectEmbbeded IsNot Nothing Then
                    Dim statusValidation = res.ObjectEmbbeded.ListRevenueControlDetails.FindAll(Function(x) x.Status = 2)
                    If statusValidation.Count > 0 Then
                        admissionStatus = "P"
                    Else
                        admissionStatus = " "
                    End If
                End If
                Try
                    Dim Justification = "El ingreso fue desbloqueado por el usuario " + SessionValues.Instance.UserIndigo
                    Using m As New MAdmissions(Me.Tag)
                        Dim resUpdate = Await m.UpdateStatusAdmission(_auxAdmissionToReload.AdmissionCode, admissionStatus, Justification)
                        If resUpdate.StateResult = True Then
                            If resUpdate.StateResultAux = False Then
                                Me.ShowMessage(EeventViewerImages.Advertencia) = resUpdate.Message
                            Else
                                Me.ShowMessage(EeventViewerImages.Informacion) = "Se actualizó de manera correcta"
                            End If
                        Else
                            Me.ShowMessage(EeventViewerImages.MensajeError) = resUpdate.Message
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
        Me.RefreshAdmission()
    End Sub

    ''' <summary>
    ''' Muestra el Reporte de la cuenta madre en caso de tenerlo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnShowMotherAccountReport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnShowMotherAccountReport.ItemClick
        Try
            Using formulario As New FrmPopUpMasterAccount
                formulario.SuggestedDateTRM = Me._auxAdmissionToReload.DateTRM
                Dim tr As New FrmTransparent(formulario, False)
                Dim response = tr.ShowDialog(Me)
                If response = System.Windows.Forms.DialogResult.OK Then
                    '' para mostrar el reporte
                    Dim reportDefPartial = New Reporter.rptInvoicePartialMotherAccount()
                    reportDefPartial.ParametrosReporte = New Object() {Me._auxAdmissionToReload.MasterAccountId, Me._auxAdmissionToReload.AdmissionCode, formulario.CurrencyId, formulario.DateTRM}
                    AddHandler reportDefPartial.AfterPrint, Sub()
                                                                If waitForm.IsSplashFormVisible Then
                                                                    waitForm.CloseWaitForm()
                                                                End If
                                                            End Sub
                    ReportHelper.ExecuteReport(reportDefPartial, Me, Me.PermissionsForm)
                    Exit Sub
                End If

                If response = System.Windows.Forms.DialogResult.Yes Then
                    ''para descargar el excel automaticamente tambien
                    Using model As New MCtrFolio()
                        Dim _indigoSessionValues As SessionValues = SessionValues.Instance
                        If MessageIndigo.Show("Este reporte en excel no tiene multimoneda los datos están en la moneda oficial. Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                            Exit Sub
                        End If
                        If Me._auxAdmissionToReload.MasterAccountId Is Nothing AndAlso Not Me._auxAdmissionToReload.MasterAccountId > 0 Then
                            ShowMessage(EeventViewerImages.Advertencia) = "No se encontró información del Folio de Cuenta Madre"
                            Exit Sub
                        End If

                        Dim revenueControlDetail = model.ListRevenueControlAsync(Me._auxAdmissionToReload.MasterAccountId, Me._auxAdmissionToReload.AdmissionCode)
                        If Not revenueControlDetail?.Details.Any() Then
                            ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron detalles en el Folio de Cuenta Madre"
                            Exit Sub
                        End If

                        Dim dt As New DataTable
                        dt.Columns.Add("Código")
                        dt.Columns.Add("Descripción")
                        dt.Columns.Add("Fecha")
                        dt.Columns.Add("Cantidad")
                        dt.Columns.Add("Valor Unitario", GetType(Decimal))
                        dt.Columns.Add("% IVA")
                        dt.Columns.Add("Subtotal", GetType(Decimal))
                        dt.Columns.Add("Valor Descuento", GetType(Decimal))
                        dt.Columns.Add("Valor Neto", GetType(Decimal))
                        dt.Columns.Add("Valor Impuesto", GetType(Decimal))
                        dt.Columns.Add("Total", GetType(Decimal))
                        dt.Columns.Add("Copago", GetType(Decimal))

                        For Each item In revenueControlDetail.Details
                            Dim row As DataRow = dt.NewRow()
                            row.Item("Código") = item.ServiceCode
                            row.Item("Descripción") = item.ServiceName
                            row.Item("Fecha") = item.ServiceDate
                            row.Item("Cantidad") = item.InvoicedQuantity
                            row.Item("Valor Unitario") = Math.Round(item.GrossValue, 2)
                            row.Item("% IVA") = item.IvaPercentage
                            row.Item("Subtotal") = Math.Round(item.SubTotalSalesPrice, 2)
                            row.Item("Valor Descuento") = Math.Round(item.GrandTotalDiscount, 2)
                            row.Item("Valor Neto") = Math.Round(item.SubTotalSalesPrice - item.GrandTotalDiscount, 2)
                            row.Item("Valor Impuesto") = Math.Round(item.GrandTotalTaxes, 2)
                            row.Item("Total") = Math.Round(item.GrandTotalSalesPrice, 2)
                            row.Item("Copago") = Math.Round(item.SubTotalPatientSalesPrice, 2)
                            dt.Rows.Add(row)
                        Next

                        INDGCExportExcel.DataSource = dt
                        INDGCExportExcel.MainView.PopulateColumns()
                        Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
                        Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
                        INDGCExportExcel.ExportToXlsx(fileName, param)
                        If System.IO.File.Exists(fileName) Then
                            System.Diagnostics.Process.Start(fileName)
                        End If
                    End Using
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Liquida todos los folios que no esten liquidados
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnLiquidateAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLiquidateAll.ItemClick
        Dim errorList As New StringBuilder()
        If IdBillingAuthorizationSelected = 0 Then
            errorList.AppendLine("Seleccione una Autorización")
        End If
        If IdOperatingUnitSelected = 0 Then
            errorList.AppendLine("Seleccione una Unidad Operativa")
        End If
        Dim FolioWithOutcategory = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Status <> 2 AndAlso CType(d.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso
                                                              CType(CType(d.Control, CtrFolio).GdcServices.DataSource, IList).Count > 0 AndAlso CType(d.Control, CtrFolio).SleCategories.EditValue Is Nothing).ToList().Select(Function(o) CType(o.Control, CtrFolio)).ToList()

        If FolioWithOutcategory IsNot Nothing AndAlso FolioWithOutcategory.Count > 0 Then
            errorList.AppendLine(String.Format("Los folios ({0}) no tienen asignada una Categoría", String.Join("-", FolioWithOutcategory.OrderBy(Function(x) x.FolioOrder).Select(Function(o) o.FolioOrder).ToList().ToArray())))
        End If
        If errorList.Length > 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
            Exit Sub
        End If
        Dim foliosToLiquidate = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Status <> 2 AndAlso CType(d.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso
                                                              CType(CType(d.Control, CtrFolio).GdcServices.DataSource, IList).Count > 0).ToList().Select(Function(o) CType(o.Control, CtrFolio)).ToList()
        If foliosToLiquidate.Count > 0 Then
            Dim stringData As String = String.Join("-", foliosToLiquidate.OrderBy(Function(x) x.FolioOrder).Select(Function(o) o.FolioOrder).ToList().ToArray())
            If MessageIndigo.Show(String.Format(ResourceManager.GetString("LiquidateFoliosTotal", GetType(CtrFolio).Name), stringData), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                foliosToLiquidate(0).LiquidateFolio(foliosToLiquidate)
            End If
        End If
    End Sub

    Private Sub MbtnOpenAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnOpenAdmission.ItemClick
        _openAdmissions = True
    End Sub

    Private Sub MbtnConfirmAdmission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnConfirmAdmission.ItemClick
        _openAdmissions = False
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the MbtnViewFolios control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnViewFolios_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnViewFolios.ItemClick
        'Cargamos los folios
        Me.BteCountFolio.EditValue = 0
        Me.TotalEntity = 0
        Me.TotalPatient = 0
        locationFolioMinimize = 0
        hideContainerBottom.Controls.Clear()
        While Me.DocumentManager.View.Documents.Count > 0
            Me.DocumentManager.View.Documents(0).Dispose()
        End While
        'Me.DocumentManager.View.Documents.Clear()
        _modeViewDocument = eModeViewDocument.Folios
        BteCountFolio.Enabled = True
        Me.LoadFoliosAsync(Me._auxAdmissionToReload.ListRevenueControlDetails)
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the MbtnViewFolios control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnLiquidateData_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLiquidateData.ItemClick
        Using formulario As New FrmLiquidationData(AdmissionNumber, If(Me.CareGroupIdMasterAccount, _folio.CareGroupId))
            formulario.Size = New System.Drawing.Size(1024, 780)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            AddHandler formulario.RecalculateMasterAccountFolios, AddressOf RecalculateMasterAccountFolios
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the MbtnViewVoidInvoices control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnViewVoidInvoices_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnViewVoidInvoices.ItemClick
        'Cargamos las facturas anuladas
        Me.BteCountFolio.EditValue = 0
        Me.TotalEntity = 0
        Me.TotalPatient = 0
        locationFolioMinimize = 0
        hideContainerBottom.Controls.Clear()
        While Me.DocumentManager.View.Documents.Count > 0
            Me.DocumentManager.View.Documents(0).Dispose()
        End While
        'Me.DocumentManager.View.Documents.Clear()
        _modeViewDocument = eModeViewDocument.AnnullateInvoices
        BteCountFolio.Enabled = False
        Me.LoadAnnullateInvoices()
    End Sub

    ''' <summary>
    ''' Reportes a mostrar se declara un nuevo hilo
    ''' </summary>
    Private bgw_printReport As BackgroundWorker
    Private bgw_printSmallReport As BackgroundWorker
    Private bgw_PrintDetailAndParentAccount As BackgroundWorker

    ''' <summary>
    ''' craga los datos a los informes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_printReport_DoWork(sender As Object, e As DoWorkEventArgs)

        Dim foliosToLiquidate = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso
                                                             CType(CType(d.Control, CtrFolio).GdcServices.DataSource, IList).Count > 0).ToList().Select(Function(o) CType(o.Control, CtrFolio)).ToList()
        e.Result = (From x In foliosToLiquidate
                    Select New With
                {
                    .IsInvoiced = IIf(x.InvoiceId > 0, True, False),
                    .InvoiceId = x.InvoiceId,
                    .RevenueControlDetailId = x.Id,
                    .AdmissionNumber = Me.AdmissionNumber,
                    .LiquidateMasterAccount = settingBilling.LiquidateMasterAccount,
                    .IsMasterAccount = False
                }).ToList
    End Sub

    ''' <summary>
    ''' Al completar la carga de datos para los informes a mostrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_printReport_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Result.Count > 0 Then
            Dim reportDef As New Reporter.rptSubLargePrintAll
            ReportHelper.ExecuteReport(reportDef, Me, PermissionsForm, e.Result)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la impresion de las facturas o prefacturas Detallada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MbtnLargePrintAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLargePrintAll.ItemClick
        If bgw_printReport Is Nothing Then
            bgw_printReport = New BackgroundWorker()
            AddHandler bgw_printReport.DoWork, AddressOf bgw_printReport_DoWork
            AddHandler bgw_printReport.RunWorkerCompleted, AddressOf bgw_printReport_RunWorkerCompleted
        End If

        If Not bgw_printReport.IsBusy Then
            bgw_printReport.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la impresion de la cuenta madre con sus facturas salud (Aseguradora - Paciente)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MbtnPrintDetailandParentAccount_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnPrintDetailandParentAccount.ItemClick
        If bgw_PrintDetailAndParentAccount Is Nothing Then
            bgw_PrintDetailAndParentAccount = New BackgroundWorker()
            AddHandler bgw_PrintDetailAndParentAccount.DoWork, AddressOf bgw_PrintDetailAndParentAccount_DoWork
            AddHandler bgw_PrintDetailAndParentAccount.RunWorkerCompleted, AddressOf bgw_PrintDetailAndParentAccount_RunWorkerCompleted
        End If
        If Not bgw_PrintDetailAndParentAccount.IsBusy Then
            bgw_PrintDetailAndParentAccount.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' carga los datos a los informes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_PrintDetailAndParentAccount_DoWork(sender As Object, e As DoWorkEventArgs)

        Dim foliosToLiquidate = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso
                                                             CType(CType(d.Control, CtrFolio).GdcServices.DataSource, IList).Count > 0).ToList().Select(Function(o) CType(o.Control, CtrFolio)).Where(Function(y) y.TotalEntity > 0 OrElse y.TotalPatient > 0).ToList()
        Dim resultList = (From x In foliosToLiquidate
                          Select New With
                                    {
                                        .IsInvoiced = True,
                                        .InvoiceId = x.InvoiceId,
                                        .RevenueControlDetailId = Nothing,
                                        .AdmissionNumber = Me.AdmissionNumber,
                                        .LiquidateMasterAccount = settingBilling.LiquidateMasterAccount,
                                        .IsMasterAccount = x.IsMasterAccount
                                    }).ToList

        Dim MotherAccountTmp = New With
                                {
                                        .IsInvoiced = False,
                                        .InvoiceId = 0,
                                        .RevenueControlDetailId = CObj(AuxAdmissionToReload.MasterAccountId),
                                        .AdmissionNumber = Me.AdmissionNumber,
                                        .LiquidateMasterAccount = settingBilling.LiquidateMasterAccount,
                                        .IsMasterAccount = CByte(1)
                                }

        resultList.Add(MotherAccountTmp)

        e.Result = resultList.OrderBy(Function(x) x.IsMasterAccount).ToList()
    End Sub

    ''' <summary>
    ''' Al completar la carga de datos para los informes a mostrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_PrintDetailAndParentAccount_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Result.Count > 0 Then
            Dim reportDef As New Reporter.rptSubLargePrintAll
            ReportHelper.ExecuteReport(reportDef, Me, PermissionsForm, e.Result)
        End If
    End Sub

    ''' <summary>
    ''' craga los datos a los informes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_printReportSmall_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim foliosToLiquidate = Me.DocumentManager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso
                                                             CType(CType(d.Control, CtrFolio).GdcServices.DataSource, IList).Count > 0).ToList().Select(Function(o) CType(o.Control, CtrFolio)).ToList()
        e.Result = (From x In foliosToLiquidate
                    Select New With
                {
                    .IsInvoiced = IIf(x.InvoiceId > 0, True, False),
                    .InvoiceId = x.InvoiceId,
                    .RevenueControlDetailId = x.Id,
                    .AdmissionNumber = Me.AdmissionNumber,
                    .LiquidateMasterAccount = settingBilling.LiquidateMasterAccount,
                    .IsMasterAccount = False
                }).ToList
    End Sub

    ''' <summary>
    ''' Al completar la carga de datos para los informes a mostrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub bgw_printReportSmall_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Result.Count > 0 Then
            Dim reportDef As New Reporter.rptSubLargePrintAll
            ReportHelper.ExecuteReport(reportDef, Me, PermissionsForm, e.Result)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la impresion de las facturas o prefacturas
    ''' </summary>
    Private Sub MbtnSmallPrintAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnSmallPrintAll.ItemClick
        If bgw_printSmallReport Is Nothing Then
            bgw_printSmallReport = New BackgroundWorker()
            AddHandler bgw_printSmallReport.DoWork, AddressOf bgw_printReportSmall_DoWork
            AddHandler bgw_printSmallReport.RunWorkerCompleted, AddressOf bgw_printReportSmall_RunWorkerCompleted
        End If
        If Not bgw_printSmallReport.IsBusy Then
            bgw_printSmallReport.RunWorkerAsync()
        End If
    End Sub

#End Region

#Region "Save Definition To Xml"

    ''' <summary>
    ''' En cada uno de estos eventos se ejecuta el método que persiste
    ''' la definición de la vista a un archivo xml
    ''' </summary>
    Private Sub GdvSmallServices_ColumnPositionChanged(sender As Object, e As EventArgs) Handles GdvStaysLiquidated.ColumnPositionChanged, GdvStays.ColumnPositionChanged, GdvOperatingUnit.ColumnPositionChanged, GdvStayDetails.ColumnPositionChanged, GdvBillingAuthorization.ColumnPositionChanged, GdvNotifications.ColumnPositionChanged
        Me.SaveDefinitionToXml(sender.View)
    End Sub
    Private Sub GdvSmallServices_ColumnWidthChanged(sender As Object, e As Views.Base.ColumnEventArgs) Handles GdvStaysLiquidated.ColumnWidthChanged, GdvStays.ColumnWidthChanged, GdvOperatingUnit.ColumnWidthChanged, GdvStayDetails.ColumnWidthChanged, GdvBillingAuthorization.ColumnWidthChanged, GdvNotifications.ColumnWidthChanged
        Me.SaveDefinitionToXml(sender)
    End Sub
    Private Sub GdvSmallServices_HideCustomizationForm(sender As Object, e As EventArgs) Handles GdvStaysLiquidated.HideCustomizationForm, GdvStays.HideCustomizationForm, GdvOperatingUnit.HideCustomizationForm, GdvStayDetails.HideCustomizationForm, GdvBillingAuthorization.HideCustomizationForm, GdvNotifications.HideCustomizationForm
        Me.SaveDefinitionToXml(sender)
    End Sub

#End Region

#End Region

#Region "Methods"

    ' ''' <summary>
    ' ''' Obtiene el id de la secuencia numerica
    ' ''' </summary>
    ' ''' <returns>Id de la secuencia numerica</returns>
    'Public Function GetIdSequenceServiceOrder() As Integer
    '    Dim id As Integer = -1
    '    If Me.OperatingUnitSelected Is Nothing Then
    '        Me.ShowMessage(EeventViewerImages.Advertencia) = "Seleccione una unidad operativa"
    '    Else
    '        If Me.SequenceServiceOrder IsNot Nothing AndAlso Me.SequenceServiceOrder.BillingSequenceDetail IsNot Nothing AndAlso Me.SequenceServiceOrder.BillingSequenceDetail.Count > 0 Then
    '            If Me.SequenceServiceOrder.Scope.Equals("OU") Then
    '                If Me.SequenceServiceOrder.BillingSequenceDetail.Any(Function(s) s.IdOperatingUnit.Value = Me.OperatingUnitSelected.Id) Then
    '                    id = Me.SequenceServiceOrder.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit.Value = Me.OperatingUnitSelected.Id).First().Id
    '                Else
    '                    Me.ShowMessage(EeventViewerImages.Advertencia) = "No existe coniguracion de secuencia para la unidad operativa seleccionada"
    '                End If
    '            Else
    '                id = Me.SequenceServiceOrder.BillingSequenceDetail(0).Id
    '            End If
    '        Else
    '            Me.ShowMessage(EeventViewerImages.Advertencia) = "No existe configuracion de secuencia"
    '        End If
    '    End If
    '    Return id
    'End Function

    ''' <summary>
    ''' Recarga el ingreso
    ''' </summary>
    Public Sub RefreshAdmission()
        hideContainerBottom.Controls.Clear()
        locationFolioMinimize = 0
        _isRefreshing = True
        MbtnLiquidateStays.Tag = Nothing
        INDSleAdmissionNumber2.EditValue = _auxAdmissionToReload.AdmissionCode
        Dim evt As New EditValueChangedEventArgs(Nothing, AuxAdmissionToReload)
        INDSleAdmissionNumber2_NewSelectedValue(Me, evt)
        'Using model As New MLiquidation
        '    Dim admission As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidation) = model.GetAdmissionByNumIngres(AdmissionNumber)
        '    If admission IsNot Nothing AndAlso admission.Count > 0 Then
        '        Me.INDSleAdmissionNumber2_NewSelectedValue(Me, New EditValueChangedEventArgs(Nothing, admission.ElementAt(0)))
        '    Else
        '        Dim admissionConfirm As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidationConfirm) = model.GetAdmissionConfirmByNumIngres(AdmissionNumber)
        '        If admissionConfirm IsNot Nothing AndAlso admissionConfirm.Count > 0 Then
        '            _auxAdmissionToReload.Status = admissionConfirm(0).Status
        '        End If
        '        'Me._auxAdmissionToReload = Nothing
        '        'Me.CleanControls()
        '        'ShowMessage(EeventViewerImages.Advertencia) = ""
        '        Me.INDSleAdmissionNumber2_NewSelectedValue(Me, New EditValueChangedEventArgs(Nothing, Me._auxAdmissionToReload))
        '    End If
        'End Using
    End Sub

    ''' <summary>
    ''' Carga las definiciones de todas la vistas en el control
    ''' </summary>
    Public Sub LoadGridViewDefinitions()
        Me.LoadDefinitionFromXml(Me.GdvStaysLiquidated)
        Me.LoadDefinitionFromXml(Me.GdvStays)
        Me.LoadDefinitionFromXml(Me.GdvNotifications)
        Me.LoadDefinitionFromXml(Me.GdvOperatingUnit)
        Me.LoadDefinitionFromXml(Me.GdvBillingAuthorization)
        Me.LoadDefinitionFromXml(Me.GdvStayDetails)
    End Sub

    Public Const MY_TYPE As String = "GridControl"
    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefinitionFromXml(ByVal view As GridView)
        If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
        End If
    End Sub

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Sub SaveDefinitionToXml(ByVal view As GridView)
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, ""))
        End If
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
    End Sub

    ''' <summary>
    ''' Agrega un nuevo objeto de notificación
    ''' </summary>
    ''' <param name="item">Objeto de notificación</param>
    Public Sub AddNotificationItem(ByVal item As NotificationItem)
        Me._notificationItemCollection.Add(item)
        Me.GdcNotifications.RefreshDataSource()
        Me.PnlNotifications.Text = String.Format(ResourceManager.GetString("NotificationPanel_Text", MODULE_NAME), Me._notificationItemCollection.Count)
        Me.PnlNotifications.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
    End Sub

    ''' <summary>
    ''' Limpia la colección de notificaciones
    ''' </summary>
    Public Sub CleanNotificationItem()
        Me._notificationItemCollection.Clear()
        Me.GdcNotifications.RefreshDataSource()
        Me.PnlNotifications.Text = String.Format(ResourceManager.GetString("NotificationPanel_Text", MODULE_NAME), Me._notificationItemCollection.Count)
        Me.PnlNotifications.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
    End Sub

    ''' <summary>
    ''' Loads the count annullate invoices.
    ''' </summary>
    ''' <param name="admission">The admission.</param>
    Public Async Sub LoadCountAnnullateInvoices(admission As String)
        Using model As New MLiquidation
            Dim res = Await model.ListAnnullateInvoiceIdByAdmissionAsync(admission)
            _countAnnullateInvoices = res.Count
        End Using
    End Sub

    ''' <summary>
    ''' Carga las estancias si existen
    ''' </summary>
    ''' <param name="admissionCode">Numero del ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    Public Async Sub LoadStays(ByVal admissionCode As String, ByVal caregroupId As Integer)
        _loadingStays = True

        Using model As New MLiquidation()
            StayErrors = String.Empty
            Me.MbtnLiquidateStays.Tag = Nothing

            'Cargamos las estancias liquidadas
            Dim res1 = Await model.ListLiquidatedStaysByAdmissionCode(admissionCode)
            If res1.StateResult Then
                Me.GdcStaysLiquidated.DataSource = res1.ObjectEmbbeded
                If Me.GdcStaysLiquidated.DataSource IsNot Nothing AndAlso CType(Me.GdcStaysLiquidated.DataSource, List(Of CHREGESTA)).Count > 0 Then
                    Me.LycgStaysLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    Me.LycgStaysLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Else
                If res1.Message IsNot Nothing AndAlso res1.Message.Trim().Equals(String.Empty) AndAlso res1.MessageResult IsNot Nothing AndAlso res1.MessageResult.Count > 0 Then
                    Select Case res1.Message
                        Case Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = String.Join(vbCrLf, res1.MessageResult.ToArray())
                    End Select
                End If
            End If

            'Cargamos las estancias sin liquidar
            Dim res = Await model.ListDontLiquidatedStaysByAdmissionCode(admissionCode, caregroupId, eLiquidateStayOption.DefectoManualGrupoAtencion, Nothing, Nothing, False)
            If res.StateResult Then
                'Sin errores al consultar estancias
                Me.MbtnLiquidateStays.Tag = False
            ElseIf Not String.IsNullOrEmpty(res.Message) Then
                'Ocurrio un error y existen estancias por liquidar, se asigna la bandera para permitir habilitar el botón de liquidar stancias
                Me.MbtnLiquidateStays.Tag = If(res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Any(), True, Nothing)
                If Not String.IsNullOrEmpty(res.Message) Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message.Trim()
                    StayErrors = res.Message.Trim()
                End If
            End If

            Me.GdcStays.DataSource = res.ObjectEmbbeded
            If Me.GdcStays.DataSource IsNot Nothing AndAlso CType(Me.GdcStays.DataSource, List(Of CHREGESTA)).Count > 0 Then
                Me.LycgStaysDontLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'Mostramos el mensaje de notificación
                Dim total As Integer = res.ObjectEmbbeded.Sum(Function(est) est.TotalUnits)
                If total > 1 Then 'Mensaje plural
                    Me.AddNotificationItem(New NotificationItem(My.Resources.LiquidateStays_24_24, ResourceManager.GetString("TitleMessageStaysDontLiquidated", MODULE_NAME), String.Format(ResourceManager.GetString("PluralMessageStaysDontLiquidated", MODULE_NAME), total)))
                ElseIf total > 0 Then
                    Me.AddNotificationItem(New NotificationItem(My.Resources.LiquidateStays_24_24, ResourceManager.GetString("TitleMessageStaysDontLiquidated", MODULE_NAME), String.Format(ResourceManager.GetString("SingleMessageStaysDontLiquidated", MODULE_NAME), total)))
                Else
                    Me.AddNotificationItem(New NotificationItem(My.Resources.LiquidateStays_24_24, ResourceManager.GetString("TitleMessageStaysDontLiquidated", MODULE_NAME), res.Message.Trim()))
                End If
            Else
                Me.LycgStaysDontLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            'Miramos si es necesario mostrar la pestaña de estancias
            If Me.LycgStaysLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always OrElse Me.LycgStaysDontLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Me.LycgStays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.LycgStays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Using

        _loadingStays = False
    End Sub

    ''' <summary>
    ''' Carga los folios y facturas dinamicamente
    ''' </summary>
    ''' <param name="list">Lista de objetos que contiene los ids de los folios a cargar</param>
    Private Async Sub LoadFoliosAsync(ByVal list As List(Of FolioDataDetail))
        For i As Int32 = 0 To list.Count - 1
            'se reinicia la variable cuando se slecciona un nuevo ingreso
            If i = 0 Then
                _displayLiquidateData = False
                Me.CareGroupIdMasterAccount = Nothing
            End If

            Dim newFolio As ICtrFolio = Me.AddNewFolio()
            newFolio.IsOncologycalMode = Me._isOncologicalMode
            newFolio.SetSleNullText(Me.INDSleAdmissionNumber2.DisplayNullText)
            newFolio.IsUnique = (list.Count = 1)
            newFolio.AdmissionType = CInt(Me.TxtAdmissionType1.EditValue)
            newFolio.AdmissionNumber = AdmissionNumber
            newFolio.AuthorizationNumber = TxtAuthorization.Text

            If PermissionsForm.ContainsKey(80) Then
                newFolio.EgressChange = True
            Else
                newFolio.EgressChange = False
            End If
            newFolio.SetDatasourceAsync(list(i).Id, list(i).Status)

            'se obtienen los datos para validar si es cuenta madre y si el parametro de facturacion esta activo
            Using model As New MBillingSetting(Me.Tag)
                settingBilling = Await model.GetSettingsBillingByIdUnitOperative(IdOperatingUnitSelected, False)
            End Using

            Using model As New MCtrFolio()
                _folio = model.ListRevenueControlAsync(list(i).Id, AdmissionNumber)
            End Using

            newFolio.CareGroupEntityType = _folio.CaregroupEntityType
            newFolio.ApplyLogicThirdPartyBeneficiary = _folio.ApplyLogicThirdPartyBeneficiary
            'cg.LiquidationType NOT IN (2,5) AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount IN (2,0)
            'se validan el # max de items por folio para generar la notificacion
            If _folio?.Status <> 2 AndAlso settingBilling?.MaxInvoiceItems > 0 AndAlso _folio?.Details?.Any() AndAlso (_folio.Details.Count() > settingBilling?.MaxInvoiceItems) Then
                AddNotificationItem(New NotificationItem(My.Resources.warning, "Limite de Lineas", $"El {list(i).FolioName} supera el limite de lineas"))
            End If

            'validacion para activar el boton datos de liquidacion si el parametro de liquidacion Liquida cuenta madre
            If settingBilling?.LiquidateMasterAccount Then
                _displayLiquidateData = True
            End If

            'si el folio es cuenta madre o folio aseguradora tomo el caregroup para poder consultar el procentaje contrado
            If {1, 2}.Contains(_folio.IsMasterAccount) Then
                Me.CareGroupIdMasterAccount = _folio.CareGroupId
            End If

        Next
        HideFolios()
    End Sub

    ''' <summary>
    ''' Carga los folios anulados de la admision seleccionada
    ''' </summary>
    Private Sub LoadAnnullateInvoices()
        Me.BteCountFolio.Enabled = False
        Using model As New MLiquidation
            Dim list = model.ListAnnullateInvoiceIdByAdmission(_auxAdmissionToReload.AdmissionCode)
            For i As Int32 = 0 To list.Count - 1
                Dim newFolio As CtrFolio = Me.AddNewFolio()
                newFolio.SetSleNullText(Me.INDSleAdmissionNumber2.DisplayNullText)
                newFolio.IsUnique = (list.Count = 1)
                newFolio.AdmissionType = CInt(Me.TxtAdmissionType1.EditValue)
                If PermissionsForm.ContainsKey(80) Then
                    newFolio.EgressChange = True
                Else
                    newFolio.EgressChange = False
                End If
                newFolio.SetDatasourceAnnulateInvoiceAsync(list(i), 4) 'Estado anulado
            Next
        End Using
        HideFolios()
    End Sub

    ''' <summary>
    ''' Agrega un nuevo folio
    ''' </summary>
    ''' <returns>Nuevo folio agregado</returns>
    Public Function AddNewFolio() As CtrFolio
        DocumentManager.View.BeginUpdate()
        Dim doc = Me.DocumentManager.View.AddDocument(New CtrFolio(Me, Me._auxAdmissionToReload))
        Dim minimizeButton As New DevExpress.XtraBars.Docking.CustomHeaderButton()
        With minimizeButton
            .Caption = ""
            .Image = Global.Presentation.Billing.My.Resources.Resources.minimizebuttonglyph_glyph
            .Style = DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton
            .ToolTip = "Minimizar"
        End With

        DirectCast(doc, DevExpress.XtraBars.Docking2010.Views.Widget.Document).CustomHeaderButtons.Add(minimizeButton)
        DirectCast(doc, DevExpress.XtraBars.Docking2010.Views.Widget.Document).Caption = "-"

        AddHandler DirectCast(doc, DevExpress.XtraBars.Docking2010.Views.Widget.Document).Maximized, AddressOf Document_Maximized
        AddHandler DirectCast(doc, DevExpress.XtraBars.Docking2010.Views.Widget.Document).Restored, AddressOf Document_Restored
        AddHandler DirectCast(doc, DevExpress.XtraBars.Docking2010.Views.Widget.Document).CustomButtonClick, AddressOf Document_Minimized

        AddHandler DirectCast(doc.Control, CtrFolio).LoadDatasourceEnd, AddressOf CtrFolio_LoadDatasourceEnd
        AddHandler DirectCast(doc.Control, CtrFolio).BeginReloadDatasource, AddressOf CtrFolio_BeginReloadDatasource
        AddHandler DirectCast(doc.Control, CtrFolio).GridViewLayoutsChanged, AddressOf CtrFolio_GridViewLayoutsChanged
        AddHandler DirectCast(doc.Control, CtrFolio).RequiereReloadFolio, AddressOf CtrFolio_RequiereReloadFolio
        AddHandler DirectCast(doc.Control, CtrFolio).RequiereReloadFolioList, AddressOf CtrFolio_RequiereReloadFolioList
        AddHandler DirectCast(doc.Control, CtrFolio).RequiereReloadFolioWithPrint, AddressOf CtrFolio_RequiereReloadFolioWithPrint
        AddHandler DirectCast(doc.Control, CtrFolio).RequiereReloadAdmission, AddressOf CtrFolio_RequiereReloadAdmission
        AddHandler DirectCast(doc.Control, CtrFolio).BeginReclasificateSelectedLines, AddressOf CtrFolio_BeginReclasificateSelectedLines
        AddHandler DirectCast(doc.Control, CtrFolio).GetStatusLoadStays, AddressOf CtrFolio_GetStatusLoadStays

        DirectCast(doc.Control, CtrFolio).IdOperativeUnit = OperatingUnitSelected.Id
        DirectCast(doc.Control, CtrFolio).DocumentParent = doc
        DirectCast(doc.Control, CtrFolio).DocumentParent.Caption = If(Not Me._auxAdmissionToReload.ContractCodeName.Equals(String.Empty), String.Format(ResourceManager.GetString("StrTitleDocumentFolioContractLiq"), Me._auxAdmissionToReload.ContractCodeName, Me._auxAdmissionToReload.LiquidationTypeName), String.Format(ResourceManager.GetString("StrTitleDocumentFolioLiq"), Me._auxAdmissionToReload.LiquidationTypeName))
        DocumentManager.View.EndUpdate()
        Return DirectCast(doc.Control, CtrFolio)
    End Function

    ''' <summary>
    ''' Obtiene los ids de los servicios seleccionados de cada folio
    ''' </summary>
    Private Sub CtrFolio_BeginReclasificateSelectedLines()
        Try
            IsBusy = True
            Task.Factory.StartNew(Async Sub()
                                      Dim serviciosAReclasificar As New List(Of Integer)()
                                      For Each folio As CtrFolio In DocumentManager.View.Documents.Select(Function(o) o.Control)
                                          'folio.IsAsyncOperation()
                                          Dim result As List(Of Integer) = folio.GetLinesSelected()
                                          If result IsNot Nothing AndAlso result.Any() Then
                                              serviciosAReclasificar.AddRange(result)
                                          End If
                                      Next
                                      Using model As New MLiquidation()
                                          Dim resul As Domain.Base.Entities.ActionResult = Await model.ReclasificateDistributions(serviciosAReclasificar, RevenueControlParentId.Value)
                                          Me.SafeInvoke(Sub()
                                                            If resul.StateResult Then
                                                                ShowMessage(EeventViewerImages.Informacion) = "El proceso se ejecutó correctamente."
                                                                RaiseEvent BeginReloadLiquidationForm()
                                                                Me.RefreshAdmission()
                                                            Else
                                                                ShowMessage(EeventViewerImages.Advertencia) = resul.Message
                                                            End If
                                                            IsBusy = False
                                                        End Sub)
                                      End Using
                                  End Sub)
        Catch ex As Exception
            IsBusy = False
            ShowMessage(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Recalcula el ancho de los folios
    ''' </summary>
    Private Sub RecalcWithFolios()
        Me.SuspendLayout()
        For Each doc As DevExpress.XtraBars.Docking2010.Views.Widget.Document In Me.DocumentManager.View.Documents
            doc.BeginUpdate()
            doc.Width = Me.CalcWidthFolios(Me.DocumentManager.View.Documents.Count)
            doc.EndUpdate()
        Next
        Me.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Calcula el ancho para los folios
    ''' </summary>
    ''' <param name="countFolios">Cantidad de folios</param>
    ''' <returns>Ancho de los folios</returns>
    Private Function CalcWidthFolios(ByVal countFolios As Int32) As Int32
        Dim div As Int32 = (If(countFolios >= 3, 3, countFolios))
        If div = 0 Then
            div = 1
        End If
        Return ((Me.Width / div) - 7)
        'Return ((System.Windows.Forms.Screen.FromControl(Me).WorkingArea.Width / div) - 7)
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls(Optional cleanNullText As Boolean = True)
        If cleanNullText Then
            Me.INDSleAdmissionNumber2.DisplayNullText = String.Empty
        End If
        Me.BteCountFolio.EditValue = 0
        Me.MbtnLiquidateStays.Tag = Nothing
        Me.LycgStaysDontLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LycgStaysLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LycgStays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.MBtnIngresosRelacionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Me.GdcStays.DataSource = Nothing
        Me.GdcStays.RefreshDataSource()
        Me.GdcStaysLiquidated.DataSource = Nothing
        Me.GdcStaysLiquidated.RefreshDataSource()

        Me.TxtAdmissionCode.Text = String.Empty
        Me.TxtFunctionalUnitAdmission.Text = String.Empty

        Me.TxtAdmissionDate.Text = String.Empty
        Me.TxtAdmissionDate1.Text = String.Empty
        Me.TxtBedStay.Text = String.Empty

        Me.TxtAdmissionType.Text = String.Empty
        Me.TxtAdmissionType1.Text = String.Empty
        Me.TxtPlaceEntry.Text = String.Empty
        Me.TxtPlaceEntry1.Text = String.Empty

        Me.TxtLiquidationType.Text = String.Empty
        Me.TxtAuthorization.Text = String.Empty

        Me.TxtAtentionCenter.Text = String.Empty
        Me.TxtEntityNameAdmission.Text = String.Empty

        Me.TxtResponsiblePhone.Text = String.Empty
        Me.TxtResponsibleName.Text = String.Empty

        Me.TxtPatientCode.Text = String.Empty
        Me.TxtPatientName.Text = String.Empty
        Me.TxtPatientBirth.Text = String.Empty
        Me.TxtPatientAge.Text = String.Empty
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtPatientEntityName.Text = String.Empty
        Me.TxtPatientEstrato.Text = String.Empty

        Me.TxtPatientType.EditValue = Nothing
        Me.TxtAfiliationType.EditValue = Nothing
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtCareGroupAdmission.Text = String.Empty
        Me.TxtRiskType.Text = String.Empty
        Me.TxtContact.Text = String.Empty

        INDSleAdmissionNumber2.EditValue = Nothing
        AdmissionNumber = Nothing

        Me.TotalEntity = 0
        Me.TotalPatient = 0
        _healthAdministratorAdmissionId = 0
        _thirdPartyPatientId = 0
        _countAnnullateInvoices = 0
        Me.CareGroupIdMasterAccount = Nothing

        _isRefreshing = False
        While Me.DocumentManager.View.Documents.Count > 0
            Me.DocumentManager.View.Documents(0).Dispose()
        End While
        'Me.DocumentManager.View.Documents.Clear()
        Me.CleanNotificationItem()
    End Sub

    ''' <summary>
    ''' Muestra el frontal en modo busqueda si asi se encuentra configurado
    ''' </summary>
    Public Sub ShowSearch()
        Dim frmSearch As New FrmSearchAdmission()
        AddHandler frmSearch.SearchAdmissionClosing, AddressOf INDSleAdmissionNumber2_NewSelectedValue
        If SessionValues.Instance.UserViewMode Then
            Me.PnlCtrlHeader.Visible = False
            Me.PnlBodyDashboard.Visible = False
            Me.PnlBodySearch.Visible = True
            Me.PnlBodySearch.BringToFront()
            frmSearch.TopLevel = False
            frmSearch.Dock = System.Windows.Forms.DockStyle.Fill
            frmSearch.Parent = Me.PnlBodySearch
            frmSearch.Show()
            frmSearch.BringToFront()
        Else
            Me.PnlCtrlHeader.Visible = True
            Me.PnlBodyDashboard.Visible = True
            Me.PnlBodySearch.Visible = False
            Me.PnlBodyDashboard.BringToFront()
            Using frmTras As New FrmTransparent(frmSearch, False)
                frmTras.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles CtrButtonEditWithPopUp, eliminando o agregando botones
    ''' </summary>
    Private Sub PrepareCtrButtonEditWithPopUp()
        Me.INDSleAdmissionNumber2.PopupContainerControl = Me.PccAdmission
        'Aqui falta configurar el frontal de creación de ingresos
        'Me.INDSleAdmissionNumber2.TagForm = String.Empty
        'Me.SleFindAdmission.OpenFormAction = Nothing
    End Sub

    Private Function INDSleAdmissionNumber2_KeyDown(code As String) As Object
        Me.IsBusy = True
        Task.Factory.StartNew(Sub()
                                  Using model As New MLiquidation()
                                      Dim admissionTempKeyDown = Nothing
                                      If _openAdmissions Then
                                          admissionTempKeyDown = model.GetAdmissionsToLiquidationCollection(code)
                                      Else
                                          admissionTempKeyDown = model.GetAdmissionsToLiquidationConfirmCollection(code)
                                      End If
                                      INDSleAdmissionNumber2.SafeInvoke(Sub()
                                                                            Try
                                                                                If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown.Count > 0 Then
                                                                                    If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown(0).GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
                                                                                        Dim evt As New EditValueChangedEventArgs(Nothing, admissionTempKeyDown(0))
                                                                                        INDSleAdmissionNumber2_NewSelectedValue(Me, evt)
                                                                                    End If
                                                                                    'Me.IsBusy = False
                                                                                Else
                                                                                    Me.IsBusy = False
                                                                                    ShowMessage(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                                                                                    INDSleAdmissionNumber2.EditValue = Nothing
                                                                                    INDSleAdmissionNumber2.DisplayNullText = String.Empty
                                                                                    INDSleAdmissionNumber2.Focus()
                                                                                End If
                                                                            Catch ex As Exception
                                                                                ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
                                                                                Me.IsBusy = False
                                                                                INDSleAdmissionNumber2.EditValue = Nothing
                                                                                INDSleAdmissionNumber2.DisplayNullText = String.Empty
                                                                                INDSleAdmissionNumber2.Focus()
                                                                            End Try
                                                                        End Sub)
                                  End Using
                              End Sub)
        Return Nothing
    End Function

    ''' <summary>
    ''' metodo para recalcular todos los folios derivados del madre o el folio madre
    ''' </summary>
    Private Sub RecalculateMasterAccountFolios(sender As Object, e As EventArgs)
        IsBusy = True
        Using model As New MLiquidation()
            Dim stringBuilderErrors = New StringBuilder
            Dim stringBuilderSuccess = New StringBuilder

            Parallel.ForEach(_auxAdmissionToReload.ListRevenueControlDetails.FindAll(Function(s) s.Status = 1), Sub(x)
                                                                                                                    Dim result = model.RecalculateFolioRest(Me.AdmissionNumber, x.Id)
                                                                                                                    If result Is Nothing OrElse Not result?.StateResult Then
                                                                                                                        stringBuilderErrors.AppendLine($"Validación: {result?.Message}.")
                                                                                                                    Else
                                                                                                                        stringBuilderSuccess.AppendLine($"{result?.Message}.")
                                                                                                                    End If
                                                                                                                End Sub)
            If stringBuilderErrors.Length > 0 Then
                ShowMessage(EeventViewerImages.Advertencia) = stringBuilderErrors.ToString()
            End If

            If stringBuilderSuccess.Length > 0 Then
                ShowMessage(EeventViewerImages.Informacion) = stringBuilderSuccess.ToString()
            End If

            IsBusy = False
            RefreshAdmission()
        End Using
    End Sub

    Private statusName As Dictionary(Of String, String)
    ''' <summary>
    ''' propiedad obtener los nombre de los estados de un ingreso segun estado, datos sacados de la tabla adingreso
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property _statusName As Dictionary(Of String, String)
        Get
            statusName = New Dictionary(Of String, String)
            statusName.Add("F", "Facturado")
            statusName.Add("A", "Anulado")
            statusName.Add("C", "Cerrado")
            statusName.Add("P", "Parcial")
            statusName.Add("B", "Bloqueado")
            statusName.Add("", "Abierto")
            Return statusName
        End Get
    End Property

#End Region

End Class

Public Enum eModeViewDocument
    AnnullateInvoices
    Folios
End Enum

''' <summary>
''' Encapsula un objeto de notificación
''' </summary>
Public Class NotificationItem

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el icono del objeto
    ''' </summary>
    ''' <value>Icono del objeto</value>
    ''' <returns>El icono del objeto</returns>
    Public Property Icon As System.Drawing.Image

    ''' <summary>
    ''' Obtiene o asigna el titulo del objeto
    ''' </summary>
    ''' <value>Titulo del objeto</value>
    ''' <returns>El titulo del objeto</returns>
    Public Property Title As String

    ''' <summary>
    ''' Obtiene o asigna el mensaje del objeto
    ''' </summary>
    ''' <value>Mensaje del objeto</value>
    ''' <returns>El mensaje del objeto</returns>
    Public Property Message As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancias de la clase
    ''' </summary>
    ''' <param name="icon">Icono del objeto</param>
    ''' <param name="title">Titulo del objeto</param>
    ''' <param name="message">Mensaje del objeto</param>
    Public Sub New(ByVal icon As System.Drawing.Image, ByVal title As String, ByVal message As String)
        Me.Icon = icon
        Me.Title = title
        Me.Message = message
    End Sub

#End Region

End Class

Public Class ButtonDarl
    Inherits DevExpress.XtraEditors.PictureEdit
    Implements DevExpress.XtraBars.Docking2010.IButton


    Public Sub New()
        Me.Enabled = True
        Me.Visible = True
        'Me = Me.Parent
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

    End Sub

    Public Event Changed(sender As Object, e As EventArgs) Implements ButtonPanel.IBaseButton.Changed

    Public Event CheckedChanged(sender As Object, e As EventArgs) Implements ButtonPanel.IBaseButton.CheckedChanged

    Public ReadOnly Property IsChecked As Boolean? Implements ButtonPanel.IBaseButton.IsChecked
        Get

        End Get
    End Property

    Public Sub SetMerged(mergedOwner As ButtonPanel.IButtonsPanel) Implements ButtonPanel.IBaseButton.SetMerged

    End Sub

    Public Sub SetOwner(owner As ButtonPanel.IButtonsPanel) Implements ButtonPanel.IBaseButton.SetOwner

    End Sub

    Public Event Disposed1(sender As Object, e As EventArgs) Implements ButtonPanel.IBaseButton.Disposed

    Public ReadOnly Property Properties1 As ButtonPanel.IButtonProperties Implements ButtonPanel.IBaseButton.Properties
        Get
            Return New MyButtonProperties(Me.Image)
        End Get
    End Property

    Private ReadOnly Property IBaseButton_IsDisposing As Boolean Implements IBaseButton.IsDisposing
        Get
            Throw New NotImplementedException()
        End Get
    End Property
End Class

Public Class MyButtonProperties
    Implements ButtonPanel.IButtonProperties

    Public Sub New(image As System.Drawing.Image)
        Me.Visible = True
        Me.UseImage = True
        Me.Image = image
        Me.ImageIndex = 0
        Me.Enabled = True
    End Sub

    Public Event Changed(sender As Object, e As EventArgs) Implements ButtonPanel.IBaseButton.Changed

    Public Event CheckedChanged(sender As Object, e As EventArgs) Implements ButtonPanel.IBaseButton.CheckedChanged

    Public Event Disposed(sender As Object, e As EventArgs) Implements ButtonPanel.IBaseButton.Disposed

    Public ReadOnly Property IsChecked As Boolean? Implements ButtonPanel.IBaseButton.IsChecked
        Get

        End Get
    End Property

    Public ReadOnly Property Properties As ButtonPanel.IButtonProperties Implements ButtonPanel.IBaseButton.Properties
        Get

        End Get
    End Property

    Public Sub SetMerged(mergedOwner As ButtonPanel.IButtonsPanel) Implements ButtonPanel.IBaseButton.SetMerged

    End Sub

    Public Sub SetOwner(owner As ButtonPanel.IButtonsPanel) Implements ButtonPanel.IBaseButton.SetOwner

    End Sub

    Public ReadOnly Property Appearance As DevExpress.Utils.AppearanceObject Implements ButtonPanel.IButtonProperties.Appearance
        Get

        End Get
    End Property

    Public Sub BeginUpdate() Implements ButtonPanel.IButtonProperties.BeginUpdate

    End Sub

    Public Sub CancelUpdate() Implements ButtonPanel.IButtonProperties.CancelUpdate

    End Sub

    Public Property Caption As String Implements ButtonPanel.IButtonProperties.Caption

    Public Property Checked As Boolean Implements ButtonPanel.IButtonProperties.Checked

    Public Property Enabled As Boolean Implements ButtonPanel.IButtonProperties.Enabled

    Public Sub EndUpdate() Implements ButtonPanel.IButtonProperties.EndUpdate

    End Sub

    Public Property Glyphs As Object Implements ButtonPanel.IButtonProperties.Glyphs

    Public Property GroupIndex As Integer Implements ButtonPanel.IButtonProperties.GroupIndex

    Public Property Image As System.Drawing.Image Implements ButtonPanel.IButtonProperties.Image

    Public Property ImageIndex As Integer Implements ButtonPanel.IButtonProperties.ImageIndex

    Public Property ImageLocation As ButtonPanel.ImageLocation Implements ButtonPanel.IButtonProperties.ImageLocation

    Public ReadOnly Property Images As Object Implements ButtonPanel.IButtonProperties.Images
        Get

        End Get
    End Property

    Public ReadOnly Property IsUpdateLocked As Boolean Implements ButtonPanel.IButtonProperties.IsUpdateLocked
        Get

        End Get
    End Property

    Public Property Style As DevExpress.XtraBars.Docking2010.ButtonStyle Implements ButtonPanel.IButtonProperties.Style

    Public Property SuperTip As DevExpress.Utils.SuperToolTip Implements ButtonPanel.IButtonProperties.SuperTip

    Public Property Tag As Object Implements ButtonPanel.IButtonProperties.Tag

    Public Property ToolTip As String Implements ButtonPanel.IButtonProperties.ToolTip

    Public Property UseCaption As Boolean Implements ButtonPanel.IButtonProperties.UseCaption

    Public Property UseImage As Boolean Implements ButtonPanel.IButtonProperties.UseImage

    Public Property Visible As Boolean Implements ButtonPanel.IButtonProperties.Visible

    Public Property VisibleIndex As Integer Implements ButtonPanel.IButtonProperties.VisibleIndex

    Public ReadOnly Property ImageOptions As BaseButtonImageOptions Implements IButtonProperties.ImageOptions
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property ImageUri As String Implements IButtonProperties.ImageUri
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property IsDisposing As Boolean Implements IBaseButton.IsDisposing
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub LockCheckEvent() Implements ButtonPanel.ISupportGroupUpdate.LockCheckEvent

    End Sub

    Public Sub UnlockCheckEvent() Implements ButtonPanel.ISupportGroupUpdate.UnlockCheckEvent

    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class