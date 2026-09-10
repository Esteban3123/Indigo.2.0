'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Billing.POCO
Imports Presentation.Billing.MVP
Imports DevExpress.XtraBars.Docking2010.Views.Widget
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports Presentation.Base.BaseClass
Imports Presentation.Base
Imports System.Dynamic
Imports DevExpress.Xpo
Imports DevExpress.Data.PLinq
Imports DevExpress.XtraGrid
Imports System.Drawing
Imports Presentation.Controls
Imports System.Collections.Concurrent
Imports DevExpress.Utils.Menu
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DevExpress.XtraGrid.Columns
Imports System.ComponentModel
Imports Domain.Crystal.Entities
Imports DevExpress.XtraSplashScreen
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Billing.Entities

#End Region

''' <summary>
''' Representa la información de un folio o factura
''' </summary>
Public Class CtrFolio
    Implements ICtrFolio

#Region "Fields"

    ''' <summary>
    ''' Id del folio
    ''' </summary>
    Private _id As Integer

    ''' <summary>
    ''' Id del registro del ingreso en la tabla de control
    ''' </summary>
    Private _idReveneuControl As Integer

    ''' <summary>
    ''' Código de la causa de ingreso
    ''' </summary>
    Private _admissionReason As Byte

    ''' <summary>
    ''' Tipo de admisión
    ''' </summary>
    Private _admissionType As Int32

    ''' <summary>
    ''' Numero de Admision
    ''' </summary>
    Private _admissionNumber As String

    ''' <summary>
    ''' Numero de autorización
    ''' </summary>
    Private _authorizationNumber As String

    ''' <summary>
    ''' tipo de entidad del grupo de atención
    ''' </summary>
    Private _careGroupEntityType As Byte

    ''' <summary>
    ''' permisio para cambiar los datos del egreso en la liquidacion de folios
    ''' </summary>
    ''' <remarks></remarks>
    Private _egressChange As Boolean

    ''' <summary>
    ''' Genero del paciente usado para la retarificación
    ''' </summary>
    Private _patientGenus As Integer

    ''' <summary>
    ''' Fecha de cumpleaños del paciente usada para la retarificación
    ''' </summary>
    Private _patientBirth As Date

    ''' <summary>
    ''' Estado del folio: 1-Registrado, 2-Facturado, 3-Bloqueado, 4-Facturas anuladas
    ''' </summary>
    Private _status As Byte

    ''' <summary>
    ''' Valor que indica si el folio es unico en el documento
    ''' </summary>
    Private _isUnique As Boolean

    ''' <summary>
    ''' Valor que indica si el folio es madre 
    ''' </summary>
    Private _isMasterAccount As Byte

    ''' <summary>
    ''' Valor que indica si el folio se encuentra maximizado
    ''' </summary>
    Private _isMaximi As Boolean

    ''' <summary>
    ''' Referencia al documento padre quien aloja el control
    ''' </summary>
    Private WithEvents _documentParent As Document

    ''' <summary>
    ''' Referencia al popUp de los datos del ingreso
    ''' </summary>
    Private WithEvents _popUp As PopupContainerControl

    ''' <summary>
    ''' Fuente de datos
    ''' </summary>
    Private _folio As IFolio

    ''' <summary>
    ''' Parrametros de Facturacion
    ''' </summary>
    Private _settingsBilling As SettingsBilling

    ''' <summary>
    ''' Salario minimo legal vigente
    ''' </summary>
    Private _smlv As Decimal

    ''' <summary>
    ''' Sumatoria total del valor que le corresponde a la entidad
    ''' </summary>
    Private _totalEntity As Decimal

    ''' <summary>
    ''' Sumatoria total del valor que le corresponde al paciente
    ''' </summary>
    ''' <remarks></remarks>
    Private _totalPatient As Decimal

    ''' <summary>
    ''' responssable de cuota moderadora
    ''' </summary>
    Private _patientQuotaResponsibleId As Integer?

    ''' <summary>
    ''' nombre responsable
    ''' </summary>
    Private _patientQuotaResponsibleText As String

    ''' <summary>
    ''' Sumatoria total
    ''' </summary>
    Private _total As Decimal

    ''' <summary>
    ''' Valor que me identifica si el folio ya se encuentra facturado
    ''' </summary>
    Private _isInvoiced As Boolean

    ''' <summary>
    ''' Cursor de carga indigo
    ''' </summary>
    Private _indigoCursor As System.Windows.Forms.Cursor

    ''' <summary>
    ''' Formulario padre, donde se encuentra alojado el control
    ''' </summary>
    Public Property FormOwner As ILiquidation Implements ICtrFolio.FormOwner

    ''' <summary>
    ''' Valor previo del grupo de atención por si hay errores en la retarificación
    ''' </summary>
    Private _previusValueCareGroup As Integer

    ''' <summary>
    ''' Valor unitario anterior para validar que cuando se pierda el foco del repositorio si se haya modificado el valor
    ''' </summary>
    Private _previewValueUnitValue As Decimal

    ''' <summary>
    ''' Número de autorización anterior para validar que cuando se pierda el foco del repositorio si se haya modificado el valor
    ''' </summary>
    Private _previewValueAuthorizationNumber As String

    ''' <summary>
    ''' bandera utilizada para saber si se está actualizando el valor unitario para no volver a entrar en el método
    ''' </summary>
    Private _isUpdatingUnitValue As Boolean

    ''' <summary>
    ''' bandera utilizada para saber si se está actualizando el valor del número de autorización para no volver a entrar en el método
    ''' </summary>
    Private _isUpdatingAuthorizationNumber As Boolean

    ''' <summary>
    ''' The list portfolio advace crossing
    ''' </summary>
    Private ListPortfolioAdvaceCrossing As List(Of PortfolioAdvance)

    ''' <summary>
    ''' The _health administrator patient identifier
    ''' </summary>
    Private _healthAdministratorAdmissiontId As Integer

    ''' <summary>
    ''' The _is loading datasource
    ''' </summary>
    Private _isLoadingDatasource As Boolean

    ''' <summary>
    ''' The m y_ type
    ''' </summary>
    Public Const MY_TYPE As String = "GridControl"

    ''' <summary>
    ''' Valor que me identifica si se estan cargando las estancias
    ''' </summary>
    Private _loadingStays As Boolean

    Private SettingBilling As SettingsBilling

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Sumatoria total del valor que le corresponde al descuento
    ''' </summary>
    ''' <remarks></remarks>
    Private _totalDiscount As Decimal

    ''' <summary>
    ''' Sumatoria total del valor que le corresponde al sub total (grossvalue*quantity)
    ''' </summary>
    ''' <remarks></remarks>
    Private _grandSubTotal As Decimal

    ''' <summary>
    ''' Indica si el folio aplica para la logica de tercero beneficiario
    ''' </summary>
    ''' <remarks></remarks>
    Private _applylogicthirdpartybeneficiary As Decimal

    ''' <summary>
    ''' Propiedad que indica el tercero del folio
    ''' </summary>
    ''' <remarks></remarks>
    Private _thirdpartyid As Integer

    Public Property IdOperativeUnit As Int32
        Get
            Return Me._idOperativeUnit
        End Get
        Set(value As Int32)
            Me._idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que asigna el valor del total del descuento
    ''' </summary>
    ''' <returns></returns>
    Private Property TotalDiscount As Decimal
        Get
            Return _totalDiscount
        End Get
        Set(value As Decimal)
            _totalDiscount = value
            Me.INDLblDiscount.Text = Utils.GetMoneyWithISO4217(value, If(String.IsNullOrEmpty(_folio?.CurrencyAbbreviation),
                                                                           SessionValues.Instance.CurrencyISO4217, _folio?.CurrencyAbbreviation))
            Me.INDLciDiscount.HideControl(value = 0)
        End Set
    End Property

    ''' <summary>
    ''' valor  subtotal del folio
    ''' </summary>
    ''' <returns></returns>
    Private Property GrandSubTotal As Decimal
        Get
            Return _grandSubTotal
        End Get
        Set(value As Decimal)
            _grandSubTotal = value
            Me.INDLblSubtotal.Text = Utils.GetMoneyWithISO4217(value, If(String.IsNullOrEmpty(_folio?.CurrencyAbbreviation),
                                                                           SessionValues.Instance.CurrencyISO4217, _folio?.CurrencyAbbreviation))
        End Set
    End Property

    ''' <summary>
    ''' Indica si el folio aplica para la logica de tercero beneficiario
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyLogicThirdPartyBeneficiary As Boolean Implements ICtrFolio.ApplyLogicThirdPartyBeneficiary
        Get
            Return _applylogicthirdpartybeneficiary
        End Get
        Set(value As Boolean)
            _applylogicthirdpartybeneficiary = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que indica el tercero del folio
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyId As Integer Implements ICtrFolio.ThirdPartyId
        Get
            Return _thirdpartyid
        End Get
        Set(value As Integer)
            _thirdpartyid = value
        End Set
    End Property

    ''' <summary>
    ''' Override del tercero paciente para validación de mayoría de edad
    ''' </summary>
    Private _thirdPartyPatientIdOverride As Integer?

    ''' <summary>
    ''' Propiedad que indica el tercero paciente del folio (para facturación)
    ''' Si se ha establecido un override (por validación de mayoría de edad), usa ese valor
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyPatientId As Integer
        Get
            If _thirdPartyPatientIdOverride.HasValue Then
                Return _thirdPartyPatientIdOverride.Value
            End If
            Return If(_folio IsNot Nothing, _folio.ThirdPartyPatientId, 0)
        End Get
        Set(value As Integer)
            _thirdPartyPatientIdOverride = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el ID del tercero paciente ORIGINAL del ingreso, sin aplicar ningún override.
    ''' Este valor SIEMPRE representa al paciente del ingreso y no cambia cuando se modifica
    ''' el tercero responsable del folio. Se usa para búsquedas de anticipos donde siempre
    ''' se deben postular los anticipos del paciente asociados al ingreso.
    ''' </summary>
    Public ReadOnly Property OriginalThirdPartyPatientId As Integer
        Get
            Return If(_folio IsNot Nothing, _folio.ThirdPartyPatientId, 0)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tercero efectivo para validación de mayoría de edad.
    ''' Para folios con Responsable Cuota Paciente asignado, usa ese tercero.
    ''' Para folios particulares, usa el tercero del folio.
    ''' En otros casos, usa el tercero paciente.
    ''' </summary>
    Public ReadOnly Property ThirdPartyIdForAgeValidation As Integer
        Get
            ' Si hay un responsable de cuota paciente asignado, validar ese tercero
            If _patientQuotaResponsibleId.HasValue AndAlso _patientQuotaResponsibleId.Value > 0 Then
                Return _patientQuotaResponsibleId.Value
            End If
            ' Para folios particulares o cuenta madre, usar el ThirdPartyPatientId (que puede tener override)
            Return ThirdPartyPatientId
        End Get
    End Property
#End Region

#Region "Consts"

#End Region

#Region "Events"

    ''' <summary>
    ''' Ocurre cuando finaliza la carga del datasource
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event LoadDatasourceEnd(ByVal sender As Object, ByVal e As LoadDatasourceEndEventArgs)

    ''' <summary>
    ''' Evento que se dispara para que el formulario padre solicite a cada folio las líneas marcadas
    ''' </summary>
    Public Event BeginReclasificateSelectedLines()

    ''' <summary>
    ''' Ocurre cuando inicia la recarga del datasource
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event BeginReloadDatasource(ByVal sender As Object, ByVal e As BeginReloadDatasourceEventArgs)

    ''' <summary>
    ''' Ocurre cuando al cargar el folio éste se encuentra facturado y se obtiene
    ''' el Id de la resolución
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event BillingAuthotization(ByVal sender As Object, ByVal e As BillingAuthotizationEventArgs)

    ''' <summary>
    ''' Ocurre cuando se ha persistido una nueva definicion del layout para una vista
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event GridViewLayoutsChanged(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Ocurre cuando se requiere mandar a recargar un folio
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event RequiereReloadFolio(ByVal sender As Object, ByVal e As RequiereReloadFolioEventArgs)

    ''' <summary>
    ''' Ocurre cuando se requiere mandar a recargar un listado de  folios
    ''' </summary>
    Public Event RequiereReloadFolioList(ByVal sender As Object, ByVal e As RequiereReloadFolioListEventArgs)

    ''' <summary>
    ''' Occurs when [requiere reload folio list with print].
    ''' </summary>
    Public Event RequiereReloadFolioWithPrint(ByVal sender As Object, ByVal e As RequiereReloadFolioEventArgs)

    ''' <summary>
    ''' Ocurre cuando se requiere mandar a recargar el ingreso completo
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event RequiereReloadAdmission(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Evento que usado para obtener el estado de la consulta de estancias
    ''' </summary>
    Public Event GetStatusLoadStays(ByVal sender As Object)

#End Region

#Region "Properties"

    Private _isOncologicalMode As Boolean
    Public WriteOnly Property IsOncologycalMode As Boolean Implements ICtrFolio.IsOncologycalMode
        Set(value As Boolean)
            _isOncologicalMode = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el Id del folio
    ''' </summary>
    ''' <returns>Id del folio</returns>
    Public ReadOnly Property Id As Integer Implements ICtrFolio.Id
        Get
            Return Me._id
        End Get
    End Property

    ''' <summary>
    ''' Gets the revenue control identifier.
    ''' </summary>
    ''' <value>
    ''' The revenue control identifier.
    ''' </value>
    Public ReadOnly Property RevenueControlId As Integer Implements ICtrFolio.RevenueControlId
        Get
            Return _idReveneuControl
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tipo de admisión
    ''' </summary>
    ''' <value>Tipo de admisnion</value>
    ''' <returns>El tipo de admision</returns>
    Public Property AdmissionType As Int32 Implements ICtrFolio.AdmissionType
        Get
            Return Me._admissionType
        End Get
        Set(value As Int32)
            Me._admissionType = value
        End Set
    End Property

    Public Property AdmissionNumber As String Implements ICtrFolio.AdmissionNumber
        Get
            Return Me._admissionNumber
        End Get
        Set(value As String)
            Me._admissionNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Número de autorización del ingreso
    ''' </summary>
    ''' <returns></returns>
    Public Property AuthorizationNumber As String Implements ICtrFolio.AuthorizationNumber
        Get
            Return Me._authorizationNumber
        End Get
        Set(value As String)
            Me._authorizationNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de entidad asociado al grupo de atención
    ''' </summary>
    ''' <returns></returns>
    Public Property CareGroupEntityType As Byte Implements ICtrFolio.CareGroupEntityType
        Get
            Return Me._careGroupEntityType
        End Get
        Set(value As Byte)
            Me._careGroupEntityType = value
        End Set
    End Property

    Public WriteOnly Property EgressChange As Boolean Implements ICtrFolio.EgressChange
        Set(value As Boolean)
            _egressChange = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene el estado del folio
    ''' </summary>
    ''' <returns>Estado del folio</returns>
    Public Property Status As Byte Implements ICtrFolio.Status
        Get
            Return Me._status
        End Get
        Private Set(value As Byte)
            Me._status = value

            GdvLargeServices.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
            GdvSmallServices.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
            Select Case value
                Case 1 'Registrado
                    Me.DdbActions.Appearance.BackColor = Color.FromArgb(0, 70, 109)
                    Me.LblFolioTitle.Appearance.BackColor = Color.FromArgb(0, 70, 109)
                    Me.LblAccountMaster.Appearance.BackColor = Color.FromArgb(0, 70, 109)
                    Me.LblFolioTitle.Appearance.ImageIndex = 0
                    If _isOncologicalMode Then
                        GdvLargeServices.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                        GdvSmallServices.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                    End If
                Case 2 'Facturado
                    Me.DdbActions.Appearance.BackColor = Color.Green
                    Me.LblFolioTitle.Appearance.BackColor = Color.Green
                    Me.LblFolioTitle.Appearance.ImageIndex = 1
                    Me.LblAccountMaster.Appearance.BackColor = Color.Green
                Case 3 ', 4 'Bloqueado
                    Me.DdbActions.Appearance.BackColor = Color.FromArgb(202, 81, 0)
                    Me.LblFolioTitle.Appearance.BackColor = Color.FromArgb(202, 81, 0)
                    Me.LblFolioTitle.Appearance.ImageIndex = 1
                    Me.LblAccountMaster.Appearance.BackColor = Color.FromArgb(202, 81, 0)
                Case 4 'Anulado
                    Me.DdbActions.Appearance.BackColor = Color.FromArgb(249, 100, 0)
                    Me.LblFolioTitle.Appearance.BackColor = Color.FromArgb(249, 100, 0)
                    Me.LblFolioTitle.Appearance.ImageIndex = 1
                    Me.LblAccountMaster.Appearance.BackColor = Color.FromArgb(249, 100, 0)
                Case 5, 7 'Reconocimiento de Ingreso
                    Me.DdbActions.Appearance.BackColor = Color.FromArgb(0, 121, 107)
                    Me.LblFolioTitle.Appearance.BackColor = Color.FromArgb(0, 121, 107)
                    Me.LblFolioTitle.Appearance.ImageIndex = 1
                    Me.LblAccountMaster.Appearance.BackColor = Color.FromArgb(0, 121, 107)
                Case 6 'Factura Asociada
                    Me.DdbActions.Appearance.BackColor = Color.YellowGreen
                    Me.LblFolioTitle.Appearance.BackColor = Color.YellowGreen
                    Me.LblFolioTitle.Appearance.ImageIndex = 1
                    Me.LblAccountMaster.Appearance.BackColor = Color.YellowGreen
                Case Else
                    Me.DdbActions.Appearance.BackColor = Color.FromArgb(0, 70, 109)
                    Me.LblFolioTitle.Appearance.BackColor = Color.FromArgb(0, 70, 109)
                    Me.LblFolioTitle.Appearance.ImageIndex = 2
                    Me.LblAccountMaster.Appearance.BackColor = Color.FromArgb(0, 70, 109)
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el Id del registro de la factura
    ''' </summary>
    ''' <returns>Id del registro de la factura</returns>
    Public ReadOnly Property InvoiceId As Integer
        Get
            Return Me._folio.InvoiceId
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el Id del grupo de atención
    ''' </summary>
    ''' <returns>Id del grupo de atención</returns>
    Public ReadOnly Property CareGroupId As Integer Implements ICtrFolio.CareGroupId
        Get
            Return IIf(Me.SleCareGroup.EditValue Is Nothing, _folio.CareGroupId, Me.SleCareGroup.EditValue)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el Id del centro de costo del grupo de atención
    ''' </summary>
    ''' <returns>Id del grupo de atención</returns>
    Public ReadOnly Property CareGroupCostCenterId As Integer Implements ICtrFolio.CareGroupCostCenterId
        Get
            Return Me._folio.CareGroupCostCenterId
        End Get
    End Property

    ''' <summary>
    ''' mainview
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MainView As GridView
        Get
            Return CType(Me.GdcServices.MainView, GridView)
        End Get
    End Property
    ''' <summary>
    ''' Gets the selected rows.
    ''' </summary>
    ''' <value>
    ''' The selected rows.
    ''' </value>
    Public ReadOnly Property SelectedRows As List(Of IFolioDetail)
        Get
            Dim view As GridView = CType(GdcServices.MainView, GridView)
            Dim selected As New ConcurrentBag(Of IFolioDetail)()
            Parallel.ForEach(view.GetSelectedRows().Where(Function(o) o > -1).ToList(),
                             Sub(i)
                                 selected.Add(CType(view.GetRow(i), IFolioDetail))
                             End Sub)
            Return selected.ToList()
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el número de la factura
    ''' </summary>
    ''' <returns>Número de la factura</returns>
    Public ReadOnly Property InvoiceNumber As String
        Get
            Return Me._folio.InvoiceNumber
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el Tipo de Folio
    ''' 1 - EAPB con contrato
    ''' 2 - EAPB sin contrato
    ''' 3 - Particulares
    ''' 4 - Aseguradoras
    ''' </summary>
    ''' <returns>Número de la factura</returns>
    Public ReadOnly Property FolioType As Byte Implements ICtrFolio.FolioType
        Get
            Return Me._folio.FolioType
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el Tipo de liquidacion
    ''' 1 - Pago por Servicios
    ''' 2 - Capitacion
    ''' 3 - Factura Global
    ''' 4 - Capitacion Global
    ''' 5 - Pago Global Prospectivo - PGP
    ''' </summary>
    ''' <returns>Número de la factura</returns>
    Public ReadOnly Property LiquidationType As Byte Implements ICtrFolio.LiquidationType
        Get
            Return Me._folio.LiquidationType
        End Get
    End Property

    ''' <summary>
    ''' Columna de aplica procedimiento
    ''' </summary>
    ''' <value>
    ''' The grid col surcharge apply.
    ''' </value>
    Public ReadOnly Property GridColSurchargeApply As GridColumn
        Get
            Return Me.ColSurchargeApply
        End Get
    End Property

    ''' <summary>
    ''' Columna tipo distribución
    ''' </summary>
    ''' <value>
    ''' The type of the grid col distribution.
    ''' </value>
    Public ReadOnly Property GridColDistributionType As GridColumn
        Get
            Return Me.ColDistributionType
        End Get
    End Property

    ''' <summary>
    ''' Columna Aplicar cuota paciente
    ''' </summary>
    ''' <value>
    ''' The grid col apply recovery fee.
    ''' </value>
    Public ReadOnly Property GridColApplyRecoveryFee As GridColumn
        Get
            Return Me.ColApplyRecoveryFee
        End Get
    End Property

    ''' <summary>
    ''' Columna de tipo aplicado
    ''' </summary>
    ''' <value>
    ''' The type of the grid col recovery fee.
    ''' </value>
    Public ReadOnly Property GridColRecoveryFeeType As GridColumn
        Get
            Return Me.ColRecoveryFeeType
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
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me.FormOwner)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me.FormOwner)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the show message.
    ''' </summary>
    ''' <value>
    ''' The show message.
    ''' </value>
    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me.FormOwner)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me.FormOwner)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene un valor que indica si el folio ya esta facturado
    ''' </summary>
    ''' <returns>Valor que indica si ya esta facturado el folio</returns>
    Public ReadOnly Property IsInvoiced As Boolean Implements ICtrFolio.IsInvoiced
        Get
            Return Me._isInvoiced
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el documento padre que alojará el control
    ''' </summary>
    ''' <value>Documento padre</value>
    ''' <returns>El documento padre</returns>
    Public Property DocumentParent As Document
        Get
            Return Me._documentParent
        End Get
        Set(value As Document)
            Me._documentParent = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el popUp que contiene la información
    ''' adicional del ingreso
    ''' </summary>
    ''' <value>PopUp del ingreso</value>
    ''' <returns>El popUp del ingreso</returns>
    Public Property PopUp As PopupContainerControl
        Get
            Return Me._popUp
        End Get
        Set(value As PopupContainerControl)
            Me._popUp = value
            If Me._popUp IsNot Nothing Then
                Me.SleFindAdmission.PopupContainerControl = Me._popUp
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el folio es unico en el documento
    ''' </summary>
    ''' <value>Valor que indica si el folio es unico</value>
    ''' <returns>El valor</returns>
    Public Property IsUnique As Boolean Implements ICtrFolio.IsUnique
        Get
            Return Me._isUnique
        End Get
        Set(value As Boolean)
            Me._isUnique = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene un valor que indica si el folio se encuentra facturado
    ''' </summary>
    ''' <returns>Valor que indica si el folio se encuentra facturado</returns>
    Public ReadOnly Property IdInvoiced As Boolean
        Get
            Return Me._isInvoiced
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor total de la entidad
    ''' </summary>
    ''' <returns>El valor total de la entidad</returns>
    Public ReadOnly Property TotalEntity As Decimal Implements ICtrFolio.TotalEntity
        Get
            Return Me._totalEntity
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor total del paciente
    ''' </summary>
    ''' <returns>El valor total del paciente</returns>
    Public ReadOnly Property TotalPatient As Decimal Implements ICtrFolio.TotalPatient
        Get
            Return Me._totalPatient
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el número del folio
    ''' </summary>
    ''' <returns>Número del folio</returns>
    Public ReadOnly Property FolioOrder As Integer Implements ICtrFolio.FolioOrder
        Get
            Return Me._folio.FolioOrder
        End Get
    End Property

    Public ReadOnly Property ThirdPartySalesPrice As Decimal Implements ICtrFolio.ThirdPartySalesPrice
        Get
            Return _folio.Details.Sum(Function(o) o.ThirdPartySalesPrice)
        End Get
    End Property

    Public ReadOnly Property VoucherValue As Decimal Implements ICtrFolio.VoucherValue
        Get
            Return _folio.VoucherValue
        End Get
    End Property

    Public ReadOnly Property TotalPatientWithDiscount As Decimal Implements ICtrFolio.TotalPatientWithDiscount
        Get
            Return _folio.TotalPatientWithDiscount
        End Get
    End Property

    Public ReadOnly Property PatientDiscount As Decimal Implements ICtrFolio.PatientDiscount
        Get
            Return _folio.PatientDiscount
        End Get
    End Property

    Public ReadOnly Property GrandTotalDiscount As Decimal Implements ICtrFolio.GrandTotalDiscount
        Get
            Return _folio.Details.Sum(Function(o) o.GrandTotalDiscount)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna si el folio es la cuenta madre
    ''' </summary>
    ''' <value>Valor que indica si el folio es unico</value>
    ''' <returns>El valor</returns>
    Public Property IsMasterAccount As Byte Implements ICtrFolio.IsMasterAccount
        Get
            Return Me._isMasterAccount
        End Get
        Set(value As Byte)
            Me._isMasterAccount = value
        End Set
    End Property

    Public Property AdmissionObject As Object

    ''' <summary>
    ''' Obtiene un valor que indica si estan cargando las estancias
    ''' </summary>
    ''' <returns>Valor que indica si ya esta facturado el folio</returns>
    Public Property LoadingStays As Boolean
        Get
            Return Me._loadingStays
        End Get
        Set(value As Boolean)
            Me._loadingStays = value
        End Set
    End Property

#End Region

#Region "Cross-Thread Properties"

    ''' <summary>
    ''' Obtiene el LayoutControlGroup del cuerpo del folio
    ''' </summary>
    ''' <returns>LayoutControlGroup del cuerpo del folio</returns>
    Public ReadOnly Property LayoutBodyFolio As LayoutControlGroup
        Get
            Return Me.LycgBodyFolio
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del panel de carga
    ''' </summary>
    ''' <returns>LayoutControlItem del panel de carga</returns>
    Public ReadOnly Property LayoutProgressPanel As LayoutControlItem
        Get
            Return Me.LyciProgressPanel
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del Label usado para bloquear el control
    ''' </summary>
    ''' <returns>LayoutControlItem del Label usado para bloquear el control</returns>
    Public ReadOnly Property LayoutBlockFolio As LayoutControlItem
        Get
            Return Me.LyciLblBlockFolio
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del AsyncOperationBar
    ''' </summary>
    ''' <returns>LayoutControlItem del AsyncOperationBar</returns>
    Public ReadOnly Property LayoutAsyncOperationBar As LayoutControlItem
        Get
            Return Me.LyciAsyncOperationBar
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del SearchLookUpEdit de tercero
    ''' </summary>
    ''' <returns>LayoutControlItem del SearchLookUpEdit de tercero</returns>
    Public ReadOnly Property LayoutThirdParty As LayoutControlItem
        Get
            Return Me.LyciThirdParty
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del TotalPatient
    ''' </summary>
    ''' <returns>LayoutControlItem del TotalPatient</returns>
    Public ReadOnly Property LayoutTotalPatient As LayoutControlItem
        Get
            Return Me.LyciTotalPatient
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del TotalEntity
    ''' </summary>
    ''' <returns>LayoutControlItem del TotalEntity</returns>
    Public ReadOnly Property LayoutTotalEntity As LayoutControlItem
        Get
            Return Me.LyciTotalEntity
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu Liquidación de cuota
    ''' </summary>
    ''' <returns>Boton de liquidación de cuota</returns>
    Public ReadOnly Property Recalculate As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnRecalculate
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu Liquidación de cuota
    ''' </summary>
    ''' <returns>Boton de liquidación de cuota</returns>
    Public ReadOnly Property LiquidateQuotaMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnLiquidateQuota
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del SpaceAsyncOperationBar
    ''' </summary>
    ''' <returns>LayoutControlItem del SpaceAsyncOperationBar</returns>
    Public ReadOnly Property LayoutSpaceAsyncOperationBar As LayoutControlItem
        Get
            Return Me.LyciSpaceAsyncOperationBar
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el LayoutControlItem del AdmissionControl
    ''' </summary>
    ''' <returns>LayoutControlItem del AdmissionControl</returns>
    Public ReadOnly Property LayoutAdmissionControl As LayoutControlItem
        Get
            Return Me.LyciAdmission
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu Liquidación de folio
    ''' </summary>
    ''' <returns>Boton de liquidación de folio</returns>
    Public ReadOnly Property LiquidateFolioMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnLiquidateFolio
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu Asociar Factura
    ''' </summary>
    ''' <returns>Boton de Asociar Factura</returns>
    Public ReadOnly Property AssociateInvoiceMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnAssociateInvoice
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu anular de factura
    ''' </summary>
    ''' <returns>Boton de anular de factura</returns>
    Public ReadOnly Property CancelInvoiceMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnCancelInvoice
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu bloquear folio
    ''' </summary>
    ''' <returns>Boton de bloquear folio</returns>
    Public ReadOnly Property BlockFolioMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnBlockFolio
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu desbloquear folio
    ''' </summary>
    ''' <returns>Boton de desbloquear folio</returns>
    Public ReadOnly Property UnblockFolioMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnUnblockFolio
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el submenu de imprimir
    ''' </summary>
    ''' <returns>Submenu de imprimir</returns>
    Public ReadOnly Property PrintAllMenuButton As DevExpress.XtraBars.BarSubItem
        Get
            Return Me.MbtnPrint
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el submenu de imprimir
    ''' </summary>
    ''' <returns>Submenu de imprimir</returns>
    Public ReadOnly Property PrintAnnullateInvoice As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnPrintAnnullateInvoice
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de imprimir tirilla
    ''' </summary>
    ''' <returns>Boton de imprimir tirilla</returns>
    Public ReadOnly Property SmallPrintMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnSmallPrint
        End Get
    End Property

    Public ReadOnly Property ReclasificateLines As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnReclasificarLineas
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de imprimir detallado
    ''' </summary>
    ''' <returns>Boton de imprimir detallado</returns>
    Public ReadOnly Property LargePrintMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnLargePrint
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de imprimir factura básica copago
    ''' </summary>
    ''' <returns>Boton de imprimir detallado</returns>
    Public ReadOnly Property BasicBillingCopayMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.MbtnBasicBillCopayPrint
        End Get
    End Property

    ''' <summary>
    ''' obtiene el boton de cerrar folio
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CloseFolioButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.INDBbiCloseFolio
        End Get
    End Property

    ''' <summary>
    ''' obtiene el boton de abrir folio
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OpenFolioButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.INDBbiOpenFolio
        End Get
    End Property

    ''' <summary>
    ''' obtiene el boton de separar cuenta
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SeparateAccountButton As DevExpress.XtraBars.BarBaseButtonItem
        Get
            Return Me.INDBbiSeparateAccount
        End Get
    End Property

    ''' <summary>
    ''' obtiene el boton de unificar cuenta
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property UnifyAccountButton As DevExpress.XtraBars.BarBaseButtonItem
        Get
            Return Me.INDBbiUnifyAccount
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu para registrar un Sin recaudo de cuota
    ''' </summary>
    ''' <returns>Boton de Sin recaudo de cuota</returns>
    Public ReadOnly Property FeeNotCollectedMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.INDBbiFeeNotCollected
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el boton de menu para Eliminar registrar un Sin recaudo de cuota
    ''' </summary>
    ''' <returns>Boton de Sin recaudo de cuota</returns>
    Public ReadOnly Property DeleteFeeNotCollectedMenuButton As DevExpress.XtraBars.BarButtonItem
        Get
            Return Me.INDBbiDeleteFeeNotCollected
        End Get
    End Property
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="formOwner">Frontal padre del document</param>
    ''' <param name="document">Documento padre donde se aloja el folio</param>
    ''' <param name="popUp">PopUp que contiene los datos del ingreso y del paciente</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal formOwner As ILiquidation, admissionObject As Object, Optional ByVal document As Document = Nothing,
                   Optional ByVal popUp As PopupContainerControl = Nothing)
        InitializeComponent()
        Me.FormOwner = formOwner
        Me._admissionReason = admissionObject.AdmissionReason
        Me._documentParent = document
        Me.AdmissionObject = admissionObject
        Me._id = 0
        Me._idReveneuControl = admissionObject.Id
        Me._smlv = admissionObject.SMLV
        Me._patientBirth = admissionObject.PatientBirth
        Me._patientGenus = admissionObject.PatientGenus
        Me._healthAdministratorAdmissiontId = admissionObject.HealthAdministratorId
        Me.Status = 0
        Me._isUnique = False
        Me._isMaximi = False
        Me._folio = Nothing
        Me._totalEntity = 0
        Me._totalPatient = 0
        Me.TotalDiscount = 0
        Me.GrandSubTotal = 0
        Me._downHitInfo = Nothing
        Me.PopUp = popUp
        If Not Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarNumeroAutorizacion)) Then
            ColAuthorizationNumber.OptionsColumn.AllowEdit = False
        End If
    End Sub

#End Region

#Region "Handlers"
    Private Sub CtrFolio_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        GC.Collect(GC.GetGeneration(Me))
        GC.WaitForPendingFinalizers()
        SleStatusFolio.Properties.DataSource = Nothing
        IdOperativeUnit = 0
    End Sub

    ''' <summary>
    ''' Aqui se configura el control al cargar
    ''' </summary>
    Private Sub CtrFolio_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._indigoCursor = ChangeCursorIndigo()
        Me.SleFindAdmission.IsReadOnly = True
        'Cambiamos el estilo del botón de menú
        Me.DdbActions.StyleController = Nothing
        Me.DdbActions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.DdbActions.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LoadGridViewDefinitions()
    End Sub

    ''' <summary>
    ''' Aqui se control el evento maximizar del documento padre
    ''' </summary>
    Private Sub DocumentParent_Maximized(sender As Object, e As EventArgs) Handles _documentParent.Maximized
        Window.Utils.SetValueToProperty(Me, "LayoutAdmissionControl.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)

        Me._isMaximi = True
        Me.SetMainView()
    End Sub

    ''' <summary>
    ''' Aqui se control el evento restaurar del documento padre
    ''' </summary>
    Private Sub DocumentParent_Restored(sender As Object, e As EventArgs) Handles _documentParent.Restored
        Window.Utils.SetValueToProperty(Me, "LayoutAdmissionControl.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Me._isMaximi = False
        Me.SetMainView()
    End Sub

#Region "Handlers"

#Region "QueryPopUp"

    Private Sub INDrptPceMoreInfo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDrptPceMoreInfo.QueryPopUp
        Dim popUp As DevExpress.XtraEditors.PopupContainerEdit = CType(sender, DevExpress.XtraEditors.PopupContainerEdit)
        If popUp.Parent IsNot Nothing Then
            Dim view As GridView = CType(CType(popUp.Parent, GridControl).MainView, GridView)
            Dim vgrid As DevExpress.XtraVerticalGrid.VGridControl = popUp.Properties.PopupControl.Controls(0).Controls(4)
            vgrid.Rows.Clear()
            For Each colInvisible In (From c In view.Columns Where c.Visible = False Select c).ToList()
                Dim rowV = New DevExpress.XtraVerticalGrid.Rows.EditorRow()
                rowV.OptionsRow.AllowFocus = False
                rowV.OptionsRow.AllowMove = False
                rowV.Properties.Caption = colInvisible.Caption
                rowV.Properties.FieldName = colInvisible.FieldName
                rowV.Properties.Format.FormatString = colInvisible.DisplayFormat.FormatString
                rowV.Properties.Format.FormatType = colInvisible.DisplayFormat.FormatType
                rowV.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
                rowV.Appearance.Options.UseFont = True
                vgrid.Rows.Add(rowV)
            Next
            vgrid.DataSource = {view.GetFocusedRow()}.ToList()
            vgrid.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Se carga el datasource
    ''' </summary>
    Private Sub SleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles SleCareGroup.QueryPopUp, SleThirdParty.QueryPopUp
        If DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit).Equals(Me.SleCareGroup) Then
            If Me.SleCareGroup.Properties.DataSource Is Nothing Then
                Using model As New MCtrFolio()
                    Me.SleCareGroup.Properties.DataSource = model.ListCareGroupByStatus()
                End Using
            End If
        Else
            If SleThirdParty.Properties.ReadOnly Then
                Exit Sub
            End If
            Dim model As New MCtrFolio()
            Select Case Me._folio.FolioType
                Case 1, 2, 4
                    'Hacemos visibles las columnas de entidad administradora
                    Me.SleThirdParty.Properties.DataSource = model.ListHealthAdministrator(CType(_folio.CaregroupEntityType, Byte))
                    Me.SetSleThirdPartyColumns(False)
                Case 3
                    'Particulares
                    'Cargar solo la entidad 999
                    Me.SleThirdParty.Properties.DataSource = Nothing
                    Me.SleThirdParty.Properties.DataSource = model.ListThirdParty()
                    Me.SetSleThirdPartyColumns()
            End Select
            model.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Evento para identificar que popup mostrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GdcServices_MouseDown(sender As Object, e As Windows.Forms.MouseEventArgs) Handles GdcServices.MouseDown
        Dim hi = MainView.CalcHitInfo(e.Location)

        If hi.InRowCell Then
            Dim obj = MainView.GetRow(hi.RowHandle)

            If obj IsNot Nothing Then
                If obj.FlagProductServiceDetail = 1 Then
                    INDRptPceQx.PopupControl = INDPccProductSD
                End If
                If obj.Presentation = 2 Then
                    INDRptPceQx.PopupControl = INDPccDetailQx
                End If
            End If
        End If
    End Sub


    Private Sub INDRptPceQx_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDRptPceQx.QueryPopUp
        Dim obj = CType(CType(Me.GdcServices.MainView, GridView).GetFocusedRow(), IFolioDetail)

        If obj IsNot Nothing Then
            CType(sender, PopupContainerEdit).Text = obj.IPSServiceCode
        End If
        If obj.Presentation = 2 Then
            Using m As New MLiquidation
                Dim surgicalLst = m.ListServiceOrderDetailSurgicalByServiceOrderDetailId(CInt(obj.ServiceOrderDetailId))
                INDGcSurgicalDetail.DataSource = Nothing
                INDGcSurgicalDetail.DataSource = surgicalLst
                INDTxtEventNumber.EditValue = obj.SurgeryNumber
                CType(INDGcEvents.MainView, GridView).ShowLoadingPanel()
                Task.Factory.StartNew(Sub()
                                          Dim listEventsTmp As XPCollection(Of ServiceOrderDetailXpo) =
                                            m.ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(obj.SurgeryNumber, CInt(obj.ServiceOrderId))
                                          INDGcEvents.SafeInvoke(Sub()
                                                                     INDGcEvents.DataSource = Nothing
                                                                     INDGcEvents.DataSource = listEventsTmp
                                                                     Dim listFoliosServiceOrder As New List(Of Integer)()
                                                                     For Each i In listEventsTmp
                                                                         listFoliosServiceOrder.AddRange(i.ServiceOrderDetailDistributions.Select(Function(o) o.RevenueControlDetailId.Id).ToList())
                                                                     Next
                                                                     INDGcEvents.Tag = listFoliosServiceOrder.Distinct().ToList()
                                                                     CType(INDGcEvents.MainView, GridView).HideLoadingPanel()
                                                                 End Sub)
                                      End Sub)
            End Using
        ElseIf obj.FlagProductServiceDetail = 1 Then
            INDRIPCEProductService_QueryPopUp()
        End If
    End Sub
    Private Sub SleStatusFolio_QueryPopUp(sender As Object, e As CancelEventArgs) Handles SleStatusFolio.QueryPopUp
        If SleStatusFolio.Properties.DataSource Is Nothing Then
            'Dim Status As Boolean = True
            Using Model As New MCtrFolio()
                Dim Datasource As XPInstantFeedbackSource = Model.ListConceptsCausesStatusFolioUsers()
                SleStatusFolio.Properties.DataSource = Datasource
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Logica para cargar las rejillas del PopUp de ProductServiceDetail
    ''' </summary>
    Private Sub INDRIPCEProductService_QueryPopUp()
        Using Model As New MCtrFolio()
            Dim ServiceOrderDetailId = SelectedRows?.Select(Function(x) x.ServiceOrderDetailId).FirstOrDefault
            INDGcServiceDetail.DataSource = Nothing
            INDGcProductRate.DataSource = Nothing
            INDGvRate.ShowLoadingPanel()
            INDGvServiceDetail.ShowLoadingPanel()

            Task.Factory.StartNew(Sub()

                                      Dim Datasource = Model.ListProductServiceDetail(ServiceOrderDetailId)
                                      Me.SafeInvoke(Sub()
                                                        If (From x In Datasource Where x.CUPSEntity IsNot Nothing Select x).ToList().Count = 0 Then
                                                            INDLycServiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                        Else
                                                            INDLycServiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                            INDGcServiceDetail.DataSource = (From x In Datasource Where x.CUPSEntity IsNot Nothing Select x).ToList()
                                                        End If

                                                        If (From h In Datasource Where h.ProductId IsNot Nothing Select h).ToList().Count = 0 Then
                                                            INDLycRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                        Else
                                                            INDLycRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                            INDGcProductRate.DataSource = (From h In Datasource Where h.ProductId IsNot Nothing Select h).ToList()
                                                        End If
                                                        INDGvRate.HideLoadingPanel()
                                                        INDGvServiceDetail.HideLoadingPanel()
                                                    End Sub)

                                  End Sub)
        End Using
    End Sub
#End Region

#Region "Enter"
    Private Sub INDRptPceQx_Enter(sender As Object, e As EventArgs) Handles INDRptPceQx.Enter
        'CType(sender, PopupContainerEdit).ShowPopup()
    End Sub
#End Region

#Region "ShowinEditor"
    ''' <summary>
    ''' Aqui se controla la edición en el valor unitario segun lo permita el manual
    ''' </summary>
    Private Sub GdvLargeServices_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles GdvLargeServices.ShowingEditor, GdvSmallServices.ShowingEditor
        Dim obj As IFolioDetail = Nothing
        obj = DirectCast(DirectCast(sender, GridView).GetFocusedRow(), IFolioDetail)

        If (DirectCast(sender, GridView).FocusedColumn.Equals(Me.ColSmallMoreInfoQx) _
                OrElse (DirectCast(sender, GridView).FocusedColumn.Equals(Me.ColLargeMoreInfoQx))) AndAlso obj.Presentation <> 2 AndAlso obj.FlagProductServiceDetail <> 1 Then
            e.Cancel = True
            Exit Sub
        End If
        If Not DirectCast(sender, GridView).FocusedColumn.Equals(Me.ColSmallMoreInfoQx) AndAlso Not DirectCast(sender, GridView).FocusedColumn.Equals(Me.ColLargeMoreInfoQx) Then
            If Me.Status <> 1 AndAlso Not (DirectCast(sender, GridView).FocusedColumn.Equals(Me.ColMoreInfo)) Then 'Si es diferente a registrado, no se permite la edición
                e.Cancel = True
                Exit Sub
            End If
            If CDec(_folio.VoucherValue) > 0 Then
                e.Cancel = True
                Exit Sub
            End If
        Else

            If Status <> 1 Then
                INDGcEvents.Enabled = False
            Else
                INDGcEvents.Enabled = True
            End If

        End If
    End Sub
#End Region

#Region "Click"



    ''' <summary>
    ''' Aqui se lanza la distribución de todo el folio, y si el grupo de atención es de tipo aseguradora
    ''' se debe distribuir la diferencia de VALOR_TOTAL - (SLMLV * 800)
    ''' </summary>
    Private Sub LblTotalEntity_Click(sender As Object, e As EventArgs) Handles LblTotalEntity.Click
        If Me.Status = 1 AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.DistribuirFolio)) _
            AndAlso Me.GdcServices.DataSource IsNot Nothing Then 'Si esta en estado registrado y el datasource no es nulo. Además debe tener permiso para distribuir
            Dim dragObj As Object = Me.GetItemsToDistribute(Me.GdvSmallServices, True)
            If CType(dragObj.ProductsAndServices, List(Of Object)).Any(Function(o) o.SourceDistribType <> 1) Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageItemDistributed", Me.GetType())
            Else
                Me.DistributeItemsFolio(If(Me._folio.FolioType = 4 AndAlso Me._admissionReason = 6,
                                        DistributionType.Insurance, DistributionType.All), Me.FilterHomologations(dragObj), False, Nothing, Nothing)
            End If
        End If
    End Sub
#End Region

#Region "PopUpMenuShowing"
    Private Delegate Sub MyDelegate(view As GridView)

    ''' <summary>
    ''' Aqui se construye y muestra el menu personalizado de la rejilla
    ''' </summary>
    Private Sub GdvSmallServices_PopupMenuShowing(sender As Object, e As Views.Grid.PopupMenuShowingEventArgs) Handles GdvSmallServices.PopupMenuShowing, GdvLargeServices.PopupMenuShowing
        Dim pMouse As Point = System.Windows.Forms.Control.MousePosition
        Dim view As GridView = CType(sender, GridView)

        Dim hitInfo As GridHitInfo = view.CalcHitInfo(view.GridControl.PointToClient(pMouse))
        If Not hitInfo.InGroupRow AndAlso hitInfo.InRow AndAlso hitInfo.HitTest <> GridHitTest.RowIndicator Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If

            e.Menu.Items.Clear()

            Dim IsGroup As Boolean = True
            Dim MenuItem As DXMenuItem

            If Me.Status = 1 Then
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.DistribuirFolio)) Then 'Solo si es un folio y no esta bloquedado. Además debe tener permiso para distribuir
                    'Agrego la opcion de distribuir
                    e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("MenuItem_Distribute", Me.GetType()), Sub() Me.MenuItem_Distribute(view), My.Resources.Distribute_30x30_Blue))
                End If

                Dim lstSelectedItems As List(Of IFolioDetail) = SelectedRows
                If lstSelectedItems.FindAll(Function(o) o.IsPackage = True).Count = lstSelectedItems.Count Then
                    If lstSelectedItems.FindAll(Function(o) o.IsPackage = True).FindAll(Function(x) x.DistributionType = 1) _
                        .Count = lstSelectedItems.Count AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.DesempaquetarItems)) Then
                        'Agrego la opcion de Desempaquetar
                        e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("MenuItem_UnPackage", Me.GetType()), Sub() Me.MenuItem_UnPackage(view), My.Resources.Ic_Empaquetar_Distribuido))
                    End If
                ElseIf lstSelectedItems.FindAll(Function(o) o.IsPackage = False).Count = lstSelectedItems.Count Then
                    If lstSelectedItems.FindAll(Function(o) o.IsPackage = False).FindAll(Function(x) x.DistributionType = 1) _
                        .Count = lstSelectedItems.Count AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.EmpaquetarItems)) Then
                        'Agrego la opcion de Empaquetar
                        e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("MenuItem_Package", Me.GetType()), Sub() Me.MenuItem_Package(view), My.Resources.Ic_Empaquetar))
                    End If
                End If

                AddLiquidateProductionDetail(e, sender)
                AddLiquidateProductionItem(e, sender)

                'Agrego la opcion de Ver Items para un item empaquetado (solo esta opcion está disponible para un solo item empaquetado no para varios)
                If lstSelectedItems.Count = 1 Then
                    If lstSelectedItems(0).IsPackage Then
                        e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("MenuItem_View_Items", Me.GetType()), Sub() Me.MenuItem_View_Items(view), My.Resources.Ic_Empaquetar))
                    End If
                End If
                'Incluir en otro servicio (que no esten distribuidos y que sean solo servicios)
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.IncluirDentroDeOtroServicio)) _
                    AndAlso lstSelectedItems.FindAll(Function(o) o.DistributionType = 1 AndAlso o.SettlementType = 1).Count = lstSelectedItems.Count Then
                    e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("IncludeOtherService", Me.GetType()), Sub() Me.MenuItem_IncludeInOtherService(view), My.Resources.LiquidateQuota_24x24_blue))
                End If
                'Desincluir del servicio (que no esten distribuidos y que sean solo servicios)
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.IncluirDentroDeOtroServicio)) _
                    AndAlso lstSelectedItems.FindAll(Function(o) o.DistributionType = 1 AndAlso o.SettlementType <> 1).Count = lstSelectedItems.Count Then
                    e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("UnIncludeOtherService", Me.GetType()), Sub() Me.MenuItem_UnIncludeInOtherService(view), My.Resources.LiquidateQuota_24x24_blue))
                End If

                'Cambiar # Autorización
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarNumeroAutorizacion)) Then
                    MenuItem = New DXMenuItem(ResourceManager.GetString("ChangeAuthorizationNumber", Me.GetType()), Sub() Me.MenuItem_ChangeAuthorizationNumber(view), My.Resources.ICONOGRAFIA_Eliminar_Cuota_Paciente__24x24__Azul)
                    MenuItem.BeginGroup = IsGroup
                    e.Menu.Items.Add(MenuItem)
                    IsGroup = False
                End If

                'Ver Orden se servicio
                MenuItem = New DXMenuItem(ResourceManager.GetString("ViewServiceOrder", Me.GetType()), Sub() Me.MenuItem_ViewServiceOrder(view), My.Resources.LiquidateQuota_24x24_blue)
                MenuItem.BeginGroup = IsGroup
                e.Menu.Items.Add(MenuItem)

                'Solo se muestra la opción Mipres si el item está marcado como NO PBS (NO-POS)
                Dim detail As ViewListServiceOrderDetailXpo = GdcServices.MainView.GetRow(view.GetSelectedRows().Where(Function(r) r > -1).FirstOrDefault())
                If detail IsNot Nothing Then
                    If (detail.IsPOSProduct = False OrElse detail.IsPOSService = False) Then
                        e.Menu.Items.Add(New DXMenuItem(ResourceManager.GetString("MenuItem_Mipres", Me.GetType()), Sub() Me.MenuItem_Mipres(view), My.Resources.ingresos_abierto))
                    End If
                End If
                IsGroup = True

                'Liquidar cuota paciente a seleccionados
                'si a alguno de los seleccionados esta disponible para aplicar cuota de recuperacion. (aparece la opcion de calcular cuopta de recuperacion)
                If (view.GetFocusedRow() IsNot Nothing AndAlso CDec(view.GetFocusedRow().VoucherValue) = 0) _
                    AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.LiquidarCuotaPaciente)) _
                    AndAlso lstSelectedItems.FindAll(Function(o) o.ApplyRecoveryFee = 1 OrElse o.ApplyRecoveryFee = 0).Count > 0 _
                    AndAlso (Me._folio.FolioType = 1 OrElse Me._folio.FolioType = 2) Then
                    MenuItem = New DXMenuItem(ResourceManager.GetString("MenuItem_LiquidateSharePatient", Me.GetType()), Sub() Me.MenuItem_LiquidatePatientQuota(view), My.Resources.LiquidateQuota_24x24_blue)
                    MenuItem.BeginGroup = IsGroup
                    e.Menu.Items.Add(MenuItem)
                    IsGroup = False
                End If
                'si a alguno de los seleccionados se esta aplicando cuota de recuperacion. (aparece la opcion de eliminar cuota de recuperacion)
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.LiquidarCuotaPaciente)) _
                    AndAlso lstSelectedItems.FindAll(Function(o) o.ApplyRecoveryFee = 2).Count > 0 AndAlso (Me._folio.FolioType = 1 _
                    OrElse Me._folio.FolioType = 2) Then
                    MenuItem = New DXMenuItem(ResourceManager.GetString("MenuItem_RemoveSharePatient", Me.GetType()), Sub() Me.MenuItem_RemoveSharePatient(view), My.Resources.ICONOGRAFIA_Eliminar_Cuota_Paciente__24x24__Azul)
                    MenuItem.BeginGroup = IsGroup
                    e.Menu.Items.Add(MenuItem)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Liquidar item producción
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="view"></param>
    Private Sub AddLiquidateProductionItem(e As Views.Grid.PopupMenuShowingEventArgs, view As GridView)
        If SelectedRows.All(Function(m) m.IsItemProduction AndAlso m.ProductLiquidationType = 2) Then
            e.Menu.Items.Add(New DXMenuItem("Liquidar Item Producción", Sub() Me.MenuItem_LiquidateProductionItem(view), My.Resources.LiquidateInvoice_24x24_blue))
        End If
    End Sub

    ''' <summary>
    ''' Liquidar detalle producción
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="view"></param>
    Private Sub AddLiquidateProductionDetail(e As Views.Grid.PopupMenuShowingEventArgs, view As GridView)
        If SelectedRows.All(Function(m) m.IsItemProduction AndAlso m.ProductLiquidationType = 1) Then
            e.Menu.Items.Add(New DXMenuItem("Liquidar Detalle Producción", Sub() Me.MenuItem_LiquidateProductionDetail(view), My.Resources.LiquidateInvoice_24x24_blue))
        End If
    End Sub

    ''' <summary>
    ''' Liquidar detalle producción action
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub MenuItem_LiquidateProductionDetail(view As GridView)
        If MessageIndigo.Show("¿Desea liquidar detalle Producción?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            LiquidarDetailProduccion()
        End If
    End Sub

    ''' <summary>
    ''' Item de produccion
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub MenuItem_LiquidateProductionItem(view As GridView)
        If MessageIndigo.Show("¿Desea liquidar Item Producción?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            LiquidarItemProduccion()
        End If
    End Sub
#End Region

#Region "MenuItem_Clik"
    ''' <summary>
    ''' Incluir servicio dentro de otro servicio
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Sub MenuItem_IncludeInOtherService(view As GridView)
        Using frmSelectIpsService As New FrmItemToInclude
            frmSelectIpsService.AdmissionNumber = Me._folio.AdmissionNumber
            frmSelectIpsService.ListServiceOrderDetailId = New List(Of Integer)(SelectedRows.Select(Function(x) x.ServiceOrderDetailId).ToArray())
            AddHandler frmSelectIpsService.ServiceOrderDetailSelected, AddressOf serviceOrderDetail_Selected
            Dim frmTransparent As New FrmTransparent(frmSelectIpsService, False)
            frmTransparent.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub MenuItem_UnIncludeInOtherService(view As GridView)
        Dim args As Object = New ExpandoObject()
        args.ServiceOrderDetailToInclude = New List(Of Integer)(SelectedRows.Select(Function(x) x.ServiceOrderDetailId).ToArray())
        args.FolioId = Me.Id
        args.CareGroupId = Me.CareGroupId
        args.PatientGenus = Me._patientGenus
        args.PatientBirth = _patientBirth
        args.AdmissionNumber = Me.AdmissionNumber

        Using model As New MLiquidation()
            Me.IsAsyncOperation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.ExcludeOutService, args)
            If res IsNot Nothing Then
                If res.StateResult Then
                    Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                    'Se manda a recargar este folio y el destino
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Menus the item_ liquidate share patient.
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Sub MenuItem_LiquidatePatientQuota(view As GridView)
        Dim args As Object = New ExpandoObject()
        Dim list = DirectCast(GdcServices.DataSource, List(Of Infrastructure.Data.Xpo.BillingRepository.ViewListServiceOrderDetailXpo))
        args.AdmissionCode = Me._folio.AdmissionNumber.Trim()
        args.IdFolio = Me._folio.RevenueControlDetailId
        args.IdDetail = -1
        args.liquidateRecovery = True
        args.LiquidationType = 3 'MultiSelectItems
        args.ServiceDistributionList = New List(Of Integer)(SelectedRows.Where(Function(o) o.ApplyRecoveryFee <> 2) _
                                                            .Select(Function(x) x.Id).ToList().ToArray())
        args.TotalsItemsApplyRecoveryFee = DirectCast(SelectedRows, IEnumerable(Of IFolioDetail)).ToList() _
            .FindAll(Function(x) x.ApplyRecoveryFee > 0).Sum(Function(y) y.GrandTotalSalesPrice)
        args.ListItemsApplyRecoveryFee = (From z In list Where z.ApplyRecoveryFee > 0 Select z.Id).ToList()
        If args.ListItemsApplyRecoveryFee.Count = 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron items seleccionados para liquidar cuota paciente"
            Exit Sub
        End If
        LiquidateRecoveryFee(args)
    End Sub

    ''' <summary>
    ''' Menus the item_ remove share patient.
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Sub MenuItem_RemoveSharePatient(view As GridView)
        Dim args As Object = New ExpandoObject()
        args.AdmissionCode = Me._folio.AdmissionNumber.Trim()
        args.IdFolio = Me._folio.RevenueControlDetailId
        args.LiquidationType = 3 'MultiSelectItems
        args.liquidateRecovery = False
        args.IdDetail = -1
        'Dim listDetailToRemovePatientQuota As New ConcurrentBag(Of Object)()
        'Parallel.ForEach(view.GetSelectedRows().Where(Function(o) o > -1).ToList(), Sub(i)
        '                                                                                Dim obj As ViewListServiceOrderDetailXpo = CType(view.GetRow(i), ViewListServiceOrderDetailXpo)
        '                                                                                If obj IsNot Nothing AndAlso obj.ApplyRecoveryFee = 2 Then
        '                                                                                    listDetailToRemovePatientQuota.Add(obj.Id)
        '                                                                                End If
        '                                                                            End Sub)
        args.ServiceDistributionList = New List(Of Integer)(SelectedRows _
                                                            .Where(Function(o) o.ApplyRecoveryFee = 2).Select(Function(x) CInt(x.Id)).ToList().ToArray())
        LiquidateRecoveryFee(args)
    End Sub

    ''' <summary>
    ''' Aqui se realiza la distribución para los items seleccionados
    ''' </summary>
    Private Sub MenuItem_Distribute(ByVal view As GridView)
        Dim dragObj As Object = Me.GetItemsToDistribute(view)
        If CType(dragObj.ProductsAndServices, List(Of Object)).Any(Function(o) o.SourceDistribType <> 1) Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageItemDistributed", Me.GetType())
        Else
            Me.DistributeItemsFolio(If(CType(dragObj.ProductsAndServices,
                                    List(Of Object)).Count > 1, DistributionType.OneByMany, DistributionType.OneByOne),
                                    Me.FilterHomologations(dragObj), False, Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Muestra el detalle de los items empaquetados
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Sub MenuItem_View_Items(ByVal view As GridView)
        OpenViewItemsPackage(view)
    End Sub

#If DEBUG Then
    Private _timerQuery As New Stopwatch()
#End If

    Private Sub MenuItem_ViewServiceOrder(view As GridView)
        Try
            Dim idEntity As String = view.GetFocusedRow().ServiceOrderCode
            If Not String.IsNullOrEmpty(idEntity) Then
#If DEBUG Then
                Me._timerQuery.Reset()
                Me._timerQuery.Start()
#End If
                Using frmObj As New FrmOrdersService()
                    frmObj.CloseBox = False
                    frmObj.LoadInParentForm = True
                    CType(frmObj, FormBase).BarraBotones.ActualizarPermisosBarra("755")
                    Dim tmpfrmObj = CType(frmObj, System.Windows.Forms.Form)
                    tmpfrmObj.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    CType(frmObj, FormBase).IdEntity = idEntity.Trim()
                    'CType(frmObj, FormBase).OnIdEntityLoaded()

                    frmObj.Size = New Size(1080, frmObj.Size.Height)
                    AddHandler frmObj.LoadEnded, AddressOf LoadEndServiceOrder
                    AddHandler frmObj.FormClosed, AddressOf ServiceOrderDetailClosing
                    AddHandler frmObj.Shown, AddressOf FormShown
                    Using tras As New FrmTransparent(tmpfrmObj, False)
                        tras.ShowDialog(Me)
                    End Using
                End Using
            Else
                FormShown(Nothing, Nothing)
            End If
        Catch ex As Exception
            FormShown(Nothing, Nothing)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Abre popup con opciones para agregar códigos Mipres
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub MenuItem_Mipres(view As GridView)
        Dim serviceOrderDetailIds = SelectedRows?.Select(Function(x) x.ServiceOrderDetailId).ToList()

        If serviceOrderDetailIds.Count > 1 AndAlso MessageIndigo.Show("Se reemplazará número Mipres asignados a los registros. ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Using frm As New PopupMipres()
            frm.ServiceOrderDetailIds = serviceOrderDetailIds
            Using tras As New FrmTransparent(frm, False)
                If tras.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                End If
            End Using
        End Using
    End Sub

    Private Async Sub FormShown(sender As Object, e As EventArgs)
        If sender IsNot Nothing AndAlso TypeOf sender Is FrmOrdersService Then
            Await CType(sender, FrmOrdersService).OnInitForm()
            CType(sender, FrmOrdersService).INDBteCode.Text = CType(sender, FormBase).IdEntity.Trim()
            Await CType(sender, FrmOrdersService).LoadControls()
        End If
        If waitForm.IsSplashFormVisible Then
            waitForm.CloseWaitForm()
        End If
    End Sub

    Private Sub MenuItem_ChangeAuthorizationNumber(view As GridView)
        OpenFormAuthorizationNumber()
    End Sub

    Private Sub LoadEndServiceOrder(sender As Object, e As EventArgs)
        CType(sender, FrmOrdersService).CloseBox = True
        CType(sender, FrmOrdersService).INDBtnAddService.Focus()
#If DEBUG Then
        Me._timerQuery.Stop()
        LblFolioTitle.Text = Me._timerQuery.Elapsed.ToString()
#End If
    End Sub

    Private Sub ServiceOrderDetailClosing(sender As Object, e As System.Windows.Forms.FormClosedEventArgs)
        MbtnRecalculate_ItemClick(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Aca se realiza el empaquetamiento de los items seleccionado
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Sub MenuItem_Package(ByVal view As GridView)
        'Validar que se empaqueten todos los homologos
        Dim validateErrors As New List(Of String)
        Dim listToPackage As New ConcurrentBag(Of IFolioDetail)()

        Parallel.ForEach(view.GetSelectedRows().Where(Function(r) r > -1).ToList(),
                         Sub(i)
                             listToPackage.Add(CType(view.GetRow(i), IFolioDetail))
                         End Sub)

        For Each item As IFolioDetail In listToPackage
            If item.CodeAssociateService IsNot Nothing AndAlso
                listToPackage.ToList().FindAll(Function(x) x.CodeAssociateService = item.CodeAssociateService).Count <>
                CType(GdcServices.DataSource, IEnumerable(Of IFolioDetail)).ToList() _
                .FindAll(Function(o) o.CodeAssociateService = item.CodeAssociateService).Count Then
                validateErrors.Add(String.Format("El item {0} no puede ser empaquetado porque no se está incluyendo sus homólogos",
                                                 String.Concat(item.IPSServiceCode, " - ", item.IPSServiceName)))
            End If
        Next

        If validateErrors.Count > 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = String.Join(vbCrLf, validateErrors.Distinct().ToArray())
            Exit Sub
        End If

        'Mostrar formulario de seleccion del CUPSEntity
        Me.Cursor = ChangeCursorIndigo()
        Me.IsAsyncOperation()
        Using popupService As New FrmServiceOrderDetailLiquidation(settingsBillign:=SettingBilling)
            popupService.ViewToPackageItems = view
            popupService.CareGroupParentId = Me.CareGroupId
            popupService.OperativeUnitId = Me.IdOperativeUnit
            popupService.Size = New System.Drawing.Size(1000, 800)
            popupService.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            popupService.Admission = _folio.AdmissionNumber
            popupService.Patient = String.Concat(FormOwner.TxtPatientCode.Text.Split("-")(1).Trim(), " - ", FormOwner.TxtPatientName.Text)
            popupService.Stay = ""
            popupService.PatientDateBirth = AdmissionObject.PatientBirth '"17/09/2008"
            popupService.PatientGenus = AdmissionObject.PatientGenus
            popupService.AdmissionDate = AdmissionObject.AdmissionDate
            If Me.LayoutThirdParty.Text = ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()) Then
                popupService.ThirdPartyId = Nothing
                popupService.HealthAdministratorId = SleThirdParty.EditValue
            Else
                popupService.ThirdPartyId = SleThirdParty.EditValue
                popupService.HealthAdministratorId = Nothing
            End If
            AddHandler popupService.AddServiceOrderDetail, AddressOf ReturServiceOrderDetailToPackage

            popupService.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
            popupService.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
            'Using model As New MServiceOrder(Me.Tag)
            '    popupService.ListServiceOrderDetailDatasourceIncludeService.AddRange(Await model.ListServiceOrderDetailsByAdmissionNumberAsync(AdmissionObject.AdmissionCode))
            'End Using
            Dim frmTransparent As New FrmTransparent(popupService, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frmTransparent.ShowDialog(Me)
            Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Aca se realiza el empaquetamiento de los items seleccionado
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Sub MenuItem_UnPackage(ByVal view As GridView)
        If MessageIndigo.Show(ResourceManager.GetString("MessagePacket", Me.GetType()), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            UnPackageItem(view)
        End If
    End Sub
#End Region

#Region "ServiceOrderDetailSelected"
    ''' <summary>
    ''' Services the order detail_ selected.
    ''' </summary>
    ''' <param name="serviceOrderDetailSelected">The service order detail selected.</param>
    ''' <param name="serviceOrderDetailInclude">The service order detail include.</param>
    Private Async Sub serviceOrderDetail_Selected(serviceOrderDetailSelected As Integer, serviceOrderDetailInclude As List(Of Integer))
        Dim args As Object = New ExpandoObject()
        args.RevenueControlDetailId = Me.Id
        args.ServiceOrderDetailMaster = serviceOrderDetailSelected
        args.ServiceOrderDetailToInclude = serviceOrderDetailInclude
        args.AdmissionNumber = Me.AdmissionNumber
        Using model As New MLiquidation()
            Me.IsAsyncOperation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.IncludeInOtherService, args)
            If res IsNot Nothing Then
                If res.StateResult Then
                    Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                    'Se manda a recargar este folio y el destino
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            Me.IsAsyncOperation(False)
        End Using
    End Sub
#End Region

#Region "EditvalueChanged"
    'Private _resultChangeCareGroup As Boolean = True
    ''' <summary>
    ''' Aqui se realiza la retarificación
    ''' </summary>
    Private Sub SleCareGroup_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles SleCareGroup.EditValueChanging
        If _previusValueCareGroup <> e.NewValue AndAlso Not _isLoadingDatasource Then

            Dim itemsFolio = CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail))? _
                .Where(Function(x) x.Id > -1)? _
                .ToList()

            If itemsFolio IsNot Nothing AndAlso itemsFolio.Any() AndAlso ValidatePackageItems(itemsFolio.Select(Of Object)(Function(m) m).ToList(), e.NewValue) = False Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "El folio no se puede reclasificar por contener Items Empaquetados en su detalle"
                e.Cancel = True
                Exit Sub
            End If

            _previusValueCareGroup = Me.CareGroupId
            Me.IsAsyncOperation()
            Me.ChangeRateServices(e.NewValue, False, Nothing, Nothing, Nothing, True, True)

            Me.SleCategories.EditValue = Nothing
            Me.SleCategories.Properties.NullText = String.Empty
            Me.SleCategories.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica para pedir tercero p entidad administradora
    ''' </summary>
    Private Sub SleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles SleCareGroup.EditValueChanged
        'If Not _resultChangeCareGroup Then
        '    Me.SleCareGroup.EditValue = _previusValueCareGroup
        '    _previusValueCareGroup = 0
        'End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the SleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub SleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles SleThirdParty.EditValueChanged
        If SleThirdParty.EditValue IsNot Nothing AndAlso Not _isLoadingDatasource Then
            Me.IsAsyncOperation()
            Dim args As Object = New ExpandoObject()
            args.FolioId = Me.Id
            If FolioType = 3 OrElse IsMasterAccount = 4 Then 'Particulares o Cuenta Madre
                args.ThirdPartyId = SleThirdParty.EditValue
                Using model As New MLiquidation
                    Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.UpdateThirdPartyFolio, args)
                    If res IsNot Nothing Then
                        If res.StateResult Then
                            ' Sincronizar el override para que la validación de mayoría de edad
                            ' use el tercero que el usuario acaba de seleccionar
                            _thirdPartyPatientIdOverride = CInt(SleThirdParty.EditValue)
                            Me.ShowMessage(EeventViewerImages.Informacion) = "El tercero cambió satisfactoriamente"
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                        End If
                    Else
                        If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                            Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        Else
                            Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                        End If
                    End If
                End Using
            Else
                args.HealthAdministratorId = SleThirdParty.EditValue
                Using model As New MLiquidation
                    Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.UpdateHealthAdministratorFolio, args)
                    If res IsNot Nothing Then
                        If res.StateResult Then
                            Me.ShowMessage(EeventViewerImages.Informacion) = "La entidad administradora de salud cambió satisfactoriamente"
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                        End If
                    Else
                        If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                            Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        Else
                            Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                        End If
                    End If
                End Using
            End If

            ThirdPartyId = GetThirdPartyId()
            Me.IsAsyncOperation(False)
        End If
    End Sub

    Private Async Sub SleStatusFolio_EditValueChanged(sender As Object, e As EventArgs) Handles SleStatusFolio.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If
        If SleStatusFolio.EditValue IsNot Nothing And _folio.StatusFolioId <> SleStatusFolio.EditValue Then
            Me.IsAsyncOperation()
            Dim args As Object = New ExpandoObject()
            args.FolioId = Me.Id
            args.StatusFolio = SleStatusFolio.EditValue
            Using model As New MLiquidation
                Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.UpdateStatusFolio, args)
                If res IsNot Nothing Then
                    If res.StateResult Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = "La acción se ejecutó correctamente"
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
            Me.IsAsyncOperation(False)
        End If
    End Sub
#End Region

#Region "EditvalueChanging"
    Private Async Sub INDRptRgIsFirstEvent_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptRgIsFirstEvent.EditValueChanging
        'AsyncLoader(True)
        LockFoliosToLiquidate(CType(INDGcEvents.Tag, List(Of Integer)), True)
        Dim serviceOrderDatailFirstEventNewXpo = DirectCast(INDGvEvents.GetFocusedRow, ServiceOrderDetailXpo)
        Dim listServiceOrderDetail As List(Of ServiceOrderDetail) = Nothing
        Using model As New MServiceOrder(Me.Tag)
            listServiceOrderDetail = Await model.GetServiceOrderDetailByOrderServiceIdAsync(serviceOrderDatailFirstEventNewXpo.ServiceOrderId.Id)
        End Using
        Dim serviceOrderDatailFirstEventNew = listServiceOrderDetail _
            .Where(Function(o) o.Id = serviceOrderDatailFirstEventNewXpo.Id).FirstOrDefault()
        Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber =
                                                               serviceOrderDatailFirstEventNew.SurgeryNumber And x.Presentation = 2 And x.Id > 0)
        For Each itemEvent In listEventsTmp
            If itemEvent.IsPackage Then
                ShowMessage(EeventViewerImages.Advertencia) = "El servicio " +
                    serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el servicio " +
                    itemEvent.CodeNameIpsService + " esta empaquetado"
                e.Cancel = True
                'AsyncLoader(False)
                LockFoliosToLiquidate(CType(INDGcEvents.Tag, List(Of Integer)), False)
                Exit Sub
            End If
            Using model As New MServiceOrder(Me.Tag)
                'valido que los items no esten distribuidos
                Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                If listDistribution.Count > 1 Then
                    ShowMessage(EeventViewerImages.Advertencia) = "El servicio " +
                        serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el servicio " +
                        itemEvent.CodeNameIpsService + " esta distribuido"
                    e.Cancel = True
                    'AsyncLoader(False)
                    LockFoliosToLiquidate(CType(INDGcEvents.Tag, List(Of Integer)), False)
                    Exit Sub
                End If

                Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                If revenueControlDetail.Status > 1 Then
                    If revenueControlDetail.Status = 2 Then
                        ShowMessage(EeventViewerImages.Advertencia) = "El servicio " +
                            serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el folio #" +
                            (revenueControlDetail.FolioOrder).ToString() + " esta facturado"
                    ElseIf revenueControlDetail.Status = 5 Then
                        ShowMessage(EeventViewerImages.Advertencia) = "El servicio " +
                            serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el folio #" +
                            (revenueControlDetail.FolioOrder).ToString() + " esta en estado de Reconocimiento de Ingresos"
                    Else
                        ShowMessage(EeventViewerImages.Advertencia) = "El servicio " +
                            serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el folio #" +
                            (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado"
                    End If
                    e.Cancel = True
                    'AsyncLoader(False)
                    LockFoliosToLiquidate(CType(INDGcEvents.Tag, List(Of Integer)), False)
                    Exit Sub
                End If
            End Using
        Next

        Dim serviceOrderDetailFirstEventOld = listServiceOrderDetail.
            Find(Function(x) x.SurgeryNumber = INDTxtEventNumber.EditValue And x.Presentation = 2 And x.IsFirstEvent = True)
        Dim indexOld = listServiceOrderDetail.IndexOf(serviceOrderDetailFirstEventOld)
        serviceOrderDatailFirstEventNew.IsFirstEvent = e.NewValue
        serviceOrderDetailFirstEventOld.IsFirstEvent = False
        Await RecalculateSurgicalItems(listServiceOrderDetail)
        'AsyncLoader(False)
        LockFoliosToLiquidate(CType(INDGcEvents.Tag, List(Of Integer)), False)
    End Sub

    Public Async Function RecalculateSurgicalItems(listServiceOrderDetail As List(Of ServiceOrderDetail)) As Task
        'obtengo los items que son quirurgicos para hacer los calculos
        Dim listServiceOrderDetailSurgicaTmp = listServiceOrderDetail.FindAll(Function(x) x.Presentation = 2)

        For Each item In listServiceOrderDetailSurgicaTmp
            'si el item no es basico ni cruento se recalcula
            If item.SurgicalInterventionType > 1 And item.SurgicalInterventionType < 9 Then
                Using model As New MServiceOrder(Me.Tag)
                    Dim index = listServiceOrderDetail.IndexOf(item)
                    Dim detailTmp = Await model.RecalculateSurgicalEventsAsync(item)
                    listServiceOrderDetail.Remove(item)
                    listServiceOrderDetail.Insert(index, detailTmp)
                    GetValueSurgicalEvenst(listServiceOrderDetail(index))
                End Using
            End If
        Next
        ValidateMIVIE(listServiceOrderDetail)
        Using m As New MServiceOrder(Me.Tag)
            Dim ressult As ActionResult = Await m.UpdateServiceOrderDetailList(listServiceOrderDetail)
            If ressult.StateResult Then
                RaiseEvent RequiereReloadFolioList(Me, New RequiereReloadFolioListEventArgs(CType(INDGcEvents.Tag, List(Of Integer))))
            Else
                ShowMessage(EeventViewerImages.Advertencia) = ressult.Message
            End If
        End Using
    End Function

    Private Sub RgNoPos_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If Not _isLoading AndAlso Status <> 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub TxtDescription_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If Not _isLoading AndAlso Status <> 1 Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' si ya se genero una cuota a paciente no se puede cambiar el estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub RepChkApplyRecoveryFee_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepChkApplyRecoveryFee.EditValueChanging
        If _folio.TotalPatientWithDiscount <> 0 Then
            e.Cancel = True
            Exit Sub
        End If
        Using model As New MLiquidation()
            Dim objView = DirectCast(IIf(GdcServices.MainView.Name.Equals(GdvSmallServices.Name), GdvSmallServices, GdvLargeServices).
                GetFocusedRow(), IFolioDetail)
            If objView IsNot Nothing Then
                Dim result = Await model.SaveApplyRecoveryFeeServiceOrderDetailDistribution(objView.Id, e.NewValue)
                If result.StateResult = False Then
                    ShowMessage(EeventViewerImages.Advertencia) = result.Message
                End If
            End If
        End Using
    End Sub
#End Region

#Region "Leave"
    ''' <summary>
    ''' Evento cuando el valor pierde el foco para actualizar el valor en la base de datos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepTxtUnitValue_Leave(sender As Object, e As EventArgs) Handles RepTxtUnitValue.Leave
        If Not _isUpdatingUnitValue Then
            _isUpdatingUnitValue = True
            UpdateUnitValue(CDec(CType(sender, TextEdit).EditValue))
        End If
    End Sub

    Private Sub RepTxtUnitValue_Enter(sender As Object, e As EventArgs) Handles RepTxtUnitValue.Enter
        _isUpdatingUnitValue = True
    End Sub

    Private Sub INDrptTxtAuthorizationNumber_Enter(sender As Object, e As EventArgs) Handles INDrptTxtAuthorizationNumber.Enter
        _isUpdatingAuthorizationNumber = True
    End Sub

    ''' <summary>
    ''' Evento cuando el valor pierde el foco para actualizar el valor en la base de datos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDrptTxtAuthorizationNumber_Leave(sender As Object, e As EventArgs) Handles INDrptTxtAuthorizationNumber.Leave
        If Not _isUpdatingAuthorizationNumber Then
            _isUpdatingAuthorizationNumber = True
            UpdateAuthorizationNumberValue(CType(sender, TextEdit).EditValue)
        End If
    End Sub

    Private Async Sub TxtDescription_Leave(sender As Object, e As EventArgs)
        'If _isLoading OrElse Status <> 1 Then
        '    Exit Sub
        'End If
        '_isLoading = True
        'Me.IsAsyncOperation()
        'Dim args As Object = New ExpandoObject()
        'args.FolioId = Me.Id
        'args.Description = TxtDescription.Text
        'Using model As New MLiquidation
        '    Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.UpdateDescriptionFolio, args)
        '    _isLoading = False
        '    If res IsNot Nothing Then
        '        If res.StateResult Then
        '            Me.ShowMessage(EeventViewerImages.Informacion) = "La acción se ejecutó correctamente"
        '        Else
        '            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
        '        End If
        '    Else
        '        If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
        '            Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '        Else
        '            Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
        '        End If
        '    End If
        'End Using
        'Me.IsAsyncOperation(False)
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Al dar Enter en el repositorio de valor unitario se actualiza el valor en la base de datos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub RepTxtUnitValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles RepTxtUnitValue.KeyDown
        Dim _view As GridView = IIf(GdcServices.MainView.Name.Equals(GdvSmallServices.Name), GdvSmallServices, GdvLargeServices)
        If _view.GetFocusedRow() IsNot Nothing Then
            _previewValueUnitValue = CType(_view.GetFocusedRow(), IFolioDetail).TotalSalesPrice
            _isUpdatingUnitValue = False
        End If
    End Sub

    Private Sub RepTxtUnitValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepTxtUnitValue.EditValueChanging
        Dim obj = DirectCast(DirectCast(GdcServices.MainView, GridView).GetFocusedRow(), IFolioDetail)

        If obj IsNot Nothing AndAlso Not obj.AllowValueChange Then 'Si no esta permitido modi modificar en el manual, se cancela la edición
            e.Cancel = True
            'Aqui se debe informar al usuario
            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.
                GetString(If(obj.RecordType = 1, "MessageDontAllowValueChangeService", "MessageDontAllowValueChangeProduct"), Me.GetType())
        End If
    End Sub

    ''' <summary>
    ''' Al dar Enter en el repositorio de valor unitario se actualiza el valor en la base de datos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDrptTxtAuthorizationNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDrptTxtAuthorizationNumber.KeyDown
        Dim _view As GridView = GdvLargeServices
        _previewValueAuthorizationNumber = CType(_view.GetFocusedRow(), IFolioDetail).AuthorizationNumber
        _isUpdatingAuthorizationNumber = False
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Desempaqueta el item seleccionado
    ''' </summary>
    ''' <param name="view">The view.</param>
    Private Async Sub UnPackageItem(view As GridView)
        Me.IsAsyncOperation()
        Dim objs As IFolioDetail = CType(view.GetFocusedRow(), IFolioDetail)

        Dim myList As New ConcurrentBag(Of Object)()
        Dim myListSOrder As New ConcurrentBag(Of Object)()
        Parallel.ForEach(view.GetSelectedRows().Where(Function(r) r > -1).ToList(),
                         Sub(i)
                             Dim obj As IFolioDetail = DirectCast(view.GetRow(i), IFolioDetail)
                             myList.Add(obj.ServiceOrderDetailId)
                             myListSOrder.Add(obj.ServiceOrderId)
                         End Sub)
        Dim objParams As Object = New ExpandoObject()
        objParams.FolioId = Me.Id
        objParams.AdmissionNumber = _folio.AdmissionNumber
        objParams.PatientCode = FormOwner.TxtPatientCode.Text.Split("-")(1).Trim()
        objParams.OperativeUnitId = FormOwner.IdOperatingUnitSelected
        objParams.CareGroupId = Me.CareGroupId
        objParams.lstServiceOrderDetailId = New List(Of Object)(myList.ToArray())
        objParams.lstServiceOrderId = New List(Of Object)(myListSOrder.ToArray())
        objParams.audit = SessionValues.Instance.AuditMessageWcf

        Using model As New MLiquidation()
            Me.IsAsyncOperation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.UnPackageItems, objParams)
            Me.IsAsyncOperation(False)
            If res IsNot Nothing Then
                If res.StateResult Then
                    Me.ShowMessage(EeventViewerImages.Informacion) = ResourceManager.GetString("MessageUnPacketCorrect", Me.GetType())
                    'Se manda a recargar este folio y el destino
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Método que obtiene el detalle de la orden de servicio de tipo paquete que sirve para empaquetar los items seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturServiceOrderDetailToPackage(sender As Object, e As AddServiceEventArgs)
        Dim serviceOrderDetail As ServiceOrderDetail = e.ListServiceOrderDetail(0)
        With serviceOrderDetail
            .IsPackage = True
            .Packaging = False
            .PackageServiceOrderDetailId = Nothing
        End With
        Dim dragObj As Object = Me.GetItemsToPackage(e.ViewToPackageItems)
        dragObj.ContractPackageId = e.ContractPackageId
        Dim frmServiceOrderDetail = CType(sender, FrmServiceOrderDetailLiquidation)
        Me.PackageItems(serviceOrderDetail, dragObj, frmServiceOrderDetail)
    End Sub

    ' ''' <summary>
    ' ''' Aqui se valida que el nuevo valor de la celda no sea vacio
    ' ''' </summary>
    'Private Sub GdvSmallServices_CellValueChanging(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles GdvSmallServices.CellValueChanging, GdvLargeServices.CellValueChanging
    '    Dim view As GridView = CType(sender, GridView)
    '    Dim obj = CType(view.GetFocusedRow(), ViewListServiceOrderDetailXpo)
    '    If e.Value Is Nothing OrElse e.Value.ToString().Equals(String.Empty) OrElse CDec(e.Value) = obj.TotalSalesPrice Then
    '        Me._updateField = False
    '        view.HideEditor()
    '    End If
    'End Sub

    '''' <summary>
    '''' Valor que indica si se debe actualizar el campo modificado en la rejilla
    '''' </summary>
    'Private _updateField As Boolean

    ' ''' <summary>
    ' ''' Aqui se realiza la actualización del campo en los respositorios
    ' ''' y se recarga el folio
    ' ''' </summary>
    'Private Sub GdvSmallServices_HiddenEditor(sender As Object, e As EventArgs) Handles GdvSmallServices.HiddenEditor, GdvLargeServices.HiddenEditor
    '    Dim view As GridView = CType(sender, GridView)
    '    Dim obj = CType(view.GetFocusedRow(), ViewListServiceOrderDetailXpo)

    'End Sub
#End Region

#Region "Menu Actions"

    ''' <summary>
    ''' Aqui ocultamos o mostramos opciones dependiendo del estado del folio
    ''' </summary>
    Private Sub DdbActions_ShowDropDownControl(sender As Object, e As ShowDropDownControlEventArgs) Handles DdbActions.ShowDropDownControl

        'Valido si tiene permiso para imprimir tirilla
        If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ImprimirTirilla)) AndAlso (Status <> 4 AndAlso Status <> 5) Then
            Window.Utils.SetValueToProperty(Me, "SmallPrintMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "SmallPrintMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        End If
        'Valido si tiene permiso para imprimir detallado
        If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ImprimirDetallado)) AndAlso (Status <> 5) Then
            Window.Utils.SetValueToProperty(Me, "LargePrintMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "LargePrintMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        End If
        If Me._folio.BasicbillingCopayId IsNot Nothing AndAlso Me._folio.BasicbillingCopayId > 0 And Status = 2 Then
            Window.Utils.SetValueToProperty(Me, "BasicBillingCopayMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "BasicBillingCopayMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        End If
        'Si no tiene permiso de imprimir nunguna, ocultamos el submenu de impresión
        If Me.MbtnSmallPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always OrElse Me.MbtnLargePrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then 'tienen que estar una de las dos en siempre
            Window.Utils.SetValueToProperty(Me, "PrintAllMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "PrintAllMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        End If
        If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ImprimirFacturaAnulada)) AndAlso Status = 4 Then
            Window.Utils.SetValueToProperty(Me, "PrintAnnullateInvoice.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "PrintAnnullateInvoice.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        End If

        If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ImprimirFacturaAnulada)) AndAlso Status = 5 Then
            Window.Utils.SetValueToProperty(Me, "PrintAnnullateInvoice.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me, "PrintAnnullateInvoice.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        End If

        Window.Utils.SetValueToProperty(Me, "ReclasificateLines.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
        MbtnPatientQuotaResponsible.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Select Case Me.Status
            Case 1 'Si esta registrado
                If _isOncologicalMode Then
                    Window.Utils.SetValueToProperty(Me, "ReclasificateLines.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                End If

                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                If _datasourceType = eDatasourceType.RevenueControlDetail Then
                    If _folio.Details.
                        Any(Function(x) x.RecordType = 2 _
                        AndAlso x.IsPOSProduct IsNot Nothing AndAlso ((Not x.IsPOSProduct) OrElse (x.IsPOSProduct AndAlso x.HasPathologies)) _
                        AndAlso x.DistributionType = 1) _
                        AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.LiquidarProductosNoPOS)) Then
                        MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Else
                        MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    End If
                    If _folio.Details.
                        Any(Function(x) x.RecordType = 2 AndAlso x.DistributionType = 5) AndAlso Me.FormOwner.PermissionsForm.
                        ContainsKey(CInt(PermissionsActionsForm.LiquidarProductosNoPOS)) Then
                        MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Else
                        MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    End If
                End If

                'se habilita el boton de recaudo de cuota cuando no exista un registro creado y cuando la cuota paciente sea 0, de lo contrario se oculta
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility",
                                         If((Me._folio?.FeeNotCollectedId Is Nothing OrElse Me._folio.FeeNotCollectedId = 0) AndAlso Me._folio.TotalPatientWithDiscount = 0,
                                            DevExpress.XtraBars.BarItemVisibility.Always,
                                            DevExpress.XtraBars.BarItemVisibility.Never))

                'se habilita el boton de eliminar sin recuado de cuota unicamente cuando exista un registro asociado
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility",
                                         If(Me._folio.FeeNotCollectedId > 0,
                                         DevExpress.XtraBars.BarItemVisibility.Always,
                                         DevExpress.XtraBars.BarItemVisibility.Never))

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", If(Me._folio.IsMasterAccount = 1, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never))
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility",
                                         If({eMasterAccount.PatientAccount, eMasterAccount.EntityAccount}.Contains(Me._folio.IsMasterAccount) AndAlso (Me._folio?.Details Is Nothing OrElse Me._folio.Details.Sum(Function(s) s.GrandTotalSalesPrice) = 0),
                                                DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never))

                Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                'Valido si tiene permiso para liquidar folio
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.LiquidarFolio)) Then
                    Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                End If
                'Si el folio no tiene valor valido si tiene permiso para asociar factura
                If Me._total = 0 AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.AssociateInvoice)) Then
                    Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                End If
                'Validamos si tiene permiso para bloquear el folio
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.BloquearFolio)) Then
                    Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                End If
                Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                'Validamos si tiene permiso para liquidar cuota paciente
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.LiquidarCuotaPaciente)) AndAlso (SettingBilling Is Nothing OrElse Not SettingBilling?.LiquidateMasterAccount) Then
                    If Me._folio.FolioType = 1 OrElse Me._folio.FolioType = 2 Then 'Se valida que se pueda liquidar cuota dependiendo del tipo de folio
                        If _datasourceType = eDatasourceType.RevenueControlDetail Then
                            If _folio IsNot Nothing AndAlso
                                 _folio.Details.Where(Function(x) x.Id > -1).Count > 0 AndAlso _folio.FeeNotCollectedId Is Nothing Then
                                If _folio.VoucherValue <> 0 Then
                                    MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                    MbtnDeletePatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                                    MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                    LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                ElseIf _folio.TotalPatientWithDiscount <> 0 Then
                                    MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                    MbtnDeletePatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                                    MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                                    LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                Else
                                    MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                                    MbtnDeletePatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                                    MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                    LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                                End If
                            Else
                                MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                MbtnDeletePatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If
                        End If
                    Else
                        MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                        LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    End If
                Else
                    MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If

                If _totalPatient > 0 Then
                    MbtnPatientQuotaResponsible.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

            Case 2 'Si esta facturado
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                'Validamos si tiene permiso para anular la factura
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.AnularFactura)) Then
                    Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                End If
                MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnDeletePatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
            Case 3 'Bloqueado
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", If({2, 4}.Contains(Me._folio.IsMasterAccount),
                                                                                DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never))
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                'Validamos si tiene permiso para desbloquear el folio
                If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.DesbloquearFolio)) Then
                    Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                End If
                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Case 4 'Anulado
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Case 5 'Reconocimiento de Ingreso
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Case 6 'Si tiene asociada una factura
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                MbtnPatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnDeletePatientBonus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
            Case 7 'folio cerrado
                Window.Utils.SetValueToProperty(Me, "FeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "DeleteFeeNotCollectedMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "SeparateAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnifyAccountButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CloseFolioButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "Recalculate.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "AssociateInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "CancelInvoiceMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "BlockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "UnblockFolioMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)

                Window.Utils.SetValueToProperty(Me, "OpenFolioButton.Visibility",
                                         If(OpenFolioValidityViewButton(Me._folio), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never))
                MbtnDeletePatientQuota.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                LiquidateQuotaMenuButton.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Case Else
                e.Allow = False
        End Select
        'MbtnLiquidateNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        'MbtnRemoveNoPOS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    End Sub

    Private Sub MbtnExcelExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnExcelExport.ItemClick
        If _folio.Details.Count > 0 Then
            'SaveFileDialogExcel.InitialDirectory = Environment.SpecialFolder.Desktop
            SaveFileDialogExcel.InitialDirectory = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder()
            SaveFileDialogExcel.FileName = Me.LblFolioTitle.Text.Trim().Replace(" ", "_")
            If SaveFileDialogExcel.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If SaveFileDialogExcel.FileName <> "" Then
                    ' Saves the Image via a FileStream created by the OpenFile method.
                    'Dim fs As System.IO.FileStream = CType(SaveFileDialogExcel.OpenFile(), System.IO.FileStream)
                    ' Saves the Image in the appropriate ImageFormat based upon the
                    ' file type selected in the dialog box.
                    ' NOTE that the FilterIndex property is one-based.
                    Select Case SaveFileDialogExcel.FilterIndex
                        Case 1
                            GdvLargeServices.ExportToXlsx(SaveFileDialogExcel.FileName, New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False))
                            'Me.button2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Case 2
                            GdvLargeServices.ExportToXls(SaveFileDialogExcel.FileName, New DevExpress.XtraPrinting.XlsExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False))
                            'Me.button2.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp)
                    End Select
                    'fs.Close()
                End If
                If System.IO.File.Exists(SaveFileDialogExcel.FileName) Then
                    System.Diagnostics.Process.Start(SaveFileDialogExcel.FileName)
                End If
            End If
        End If
    End Sub


    Private Sub MbtnReclasificarLineas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnReclasificarLineas.ItemClick
        If MessageIndigo.Show("¿Esta seguro que desea reclasificar las líneas seleccionadas?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            RaiseEvent BeginReclasificateSelectedLines()

        End If
    End Sub

    Private Async Sub MbtnLiquidateNoPOS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLiquidateNoPOS.ItemClick
        Me.IsAsyncOperation()
        Dim objParams As Object = New ExpandoObject()
        Dim myList As New ConcurrentBag(Of Object)()
        Dim listProductATC As List(Of ProductATC) = (From o In CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)) Where o.RecordType = 2 AndAlso
                                                             o.DistributionType = 1 AndAlso o.IsPOSProduct IsNot Nothing AndAlso Not o.IsPOSProduct
                                                     Select New ProductATC With {.ServiceOrderDetailId = o.ServiceOrderDetailId, .ProductCodeNoPOS = o.ProductCode, .ATCCodeNoPOS = o.ProductATCCode, .Quantity = o.InvoicedQuantity, .ProductIdNoPOS = o.ProductId}).ToList()

        If listProductATC IsNot Nothing Then
            listProductATC.RemoveAll(Function(o) o.ATCCodeNoPOS Is Nothing)
            If listProductATC.Count = 0 Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron productos No POS a aplicar"
                Me.IsAsyncOperation(False)
                Exit Sub
            End If
        End If
        Using model As New MLiquidation()
            Dim careGroupId As Integer = Me.CareGroupId
            Dim res = Await model.GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC, _folio.AdmissionNumber, careGroupId)
            If res IsNot Nothing AndAlso res.StatusCode = eStatusResult.SUCCESS Then
                If res.ObjectEmbbeded Is Nothing OrElse res.ObjectEmbbeded.Count = 0 Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron productos homologos POS"
                    Me.IsAsyncOperation(False)
                    Exit Sub
                End If
                For Each obj As IFolioDetail In CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Where(Function(x) x.Id > -1 AndAlso
                                                                            res.ObjectEmbbeded.Select(Function(o) o.ServiceOrderDetailId).ToList().Contains(x.ServiceOrderDetailId)).ToList()

                    Dim dyObj As Object = New ExpandoObject()
                    dyObj.Id = obj.Id
                    dyObj.FunctionalUnitCodeName = obj.PerformsFunctionalUnitCodeName
                    dyObj.ServiceOrderDetailId = obj.ServiceOrderDetailId
                    dyObj.SourceDistribType = CByte(5)
                    dyObj.TargetDistribType = CByte(5)
                    dyObj.Code = If(obj.RecordType = 1, obj.IPSServiceCode, obj.ProductCode)
                    dyObj.Description = If(obj.RecordType = 1, obj.IPSServiceName, obj.ProductName)
                    dyObj.TotalFolioValue = CDec(obj.GrandTotalSalesPrice)
                    dyObj.SourceFolioValue = CDec(obj.GrandTotalSalesPrice)

                    dyObj.InvoiceQuantity = obj.InvoicedQuantity
                    dyObj.SourceFolioGrandTotalDiscountValue = CDec(obj.GrandTotalDiscount)
                    dyObj.TargetFolioGrandTotalDiscountValue = CDec(0)
                    dyObj.TotalPatientSalesPrice = CDec(_folio.TotalPatientSalesPrice)
                    dyObj.TotalPatientWithDiscount = CDec(_folio.TotalPatientWithDiscount)
                    dyObj.SubTotalPatientSalesPrice = CDec(obj.SubTotalPatientSalesPrice)
                    dyObj.PatientDiscountPercentage = CDec(_folio.PatientDiscountPercentage)
                    dyObj.IsPackage = obj.IsPackage

                    dyObj.ItemDate = obj.ServiceDate
                    dyObj.GuidHomologation = obj.GuidHomologation

                    'EXTRASSSS
                    Dim productATC As ProductATC = res.ObjectEmbbeded.Where(Function(o) o.ServiceOrderDetailId = obj.ServiceOrderDetailId).FirstOrDefault()
                    dyObj.UnitValue = CDec(obj.TotalSalesPrice)
                    dyObj.ProductDefaultPOSCode = productATC.DefaultProductCodePOS
                    dyObj.ProductDefaultPOSId = productATC.DefaultProductIdPOS
                    If productATC.ATCCodePOS IsNot Nothing Then
                        dyObj.ATCCodePOS = productATC.ATCCodePOS.Trim()
                    Else
                        dyObj.ATCCodePOS = ""
                    End If
                    dyObj.ProductPOSUnitValue = productATC.DefaultProductUnitValue
                    dyObj.ProductPOSTotalValue = productATC.DefaultProductTotalValue
                    dyObj.ProductPOSName = productATC.DefaultProductNamePOS
                    dyObj.UnitValueProductNoPOS = productATC.UnitValueProductNoPOS
                    dyObj.UnitValueDifference = productATC.UnitValueDifference
                    dyObj.TargetFolioValue = productATC.TotalValueDifference 'TotalValueDifference 'Valor del item que va a quedar en el nuevo folio
                    myList.Add(dyObj)
                Next

                Me.IsAsyncOperation(False)
                objParams.ProductsAndServices = New List(Of Object)(myList.ToArray())
                objParams.SourceFolioId = Me.Id
                objParams.CareGroupId = careGroupId
                objParams.CareGroupCodeName = SleCareGroup.Text
                objParams.RevenueControlId = Me._idReveneuControl
                'objParams.FolioOrder = Me.FormOwner.NextFolioOrder
                objParams.FolioType = Me._folio.FolioType
                objParams.ChangeRateServicesNeccesary = False

                Dim frmDist As New FrmDistribution(DistributionType.NoPOSProduct, Me.FormOwner, Me.FormOwner.ListFoliosIdOrder.Where(Function(f) f.FolioId <> Me.Id).ToList(), objParams, Me._smlv)
                Using form As New FrmTransparent(frmDist, False)
                    If form.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                        Exit Sub
                    End If
                End Using
                CType(objParams.ProductsAndServices, List(Of Object)).RemoveAll(Function(o) o.ProductPOSTotalValue = 0)
                If CType(objParams.ProductsAndServices, List(Of Object)).Count = 0 Then
                    Me.IsAsyncOperation(False)
                    Exit Sub
                End If
                objParams.TargetFolioId = frmDist.TargetFolioIdSelected
                If Me._folio.ContractEntityId > 0 Then
                    objParams.ContractEntityId = Me._folio.ContractEntityId
                Else
                    objParams.ContractEntityId = Nothing
                End If
                If Me._folio.HealthAdministratorId > 0 Then
                    objParams.HealthAdministratorId = Me._folio.HealthAdministratorId
                Else
                    objParams.HealthAdministratorId = Nothing
                End If
                objParams.ThirdPartyId = Me.GetThirdPartyId()
                objParams.CareGroupId = Me.CareGroupId
                objParams.ThirdPartyPatientId = _folio.ThirdPartyPatientId
                objParams.User = SessionValues.Instance.UserIndigo
                objParams.DistribType = 5 'Distribución de productos No POS

                Dim foliosToLiquidate As List(Of Integer) = {Me.Id, CInt(objParams.SourceFolioId)}.ToList()
                LockFoliosToLiquidate(foliosToLiquidate, True)
                Dim resultDistribute As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) = Await model.DistributeFolio(objParams, Nothing, Nothing)
                If resultDistribute IsNot Nothing AndAlso resultDistribute.StateResult Then
                    If frmDist.TargetFolioIdSelected = -1 Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = "El proceso se ejecutó correctamente"
                        RaiseEvent RequiereReloadAdmission(Me, New EventArgs())
                    Else
                        'Se manda a recargar este folio y el destino
                        foliosToLiquidate.Add(objParams.TargetFolioId)
                        RaiseEvent RequiereReloadFolioList(Me, New RequiereReloadFolioListEventArgs(foliosToLiquidate.Distinct().ToList()))
                    End If
                Else
                    If resultDistribute Is Nothing OrElse resultDistribute.Message.Equals(String.Empty) Then
                        Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    Else
                        Me.ShowMessage(EeventViewerImages.MensajeError) = resultDistribute.Message
                    End If
                End If
                LockFoliosToLiquidate(foliosToLiquidate, False)
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            Me.IsAsyncOperation(False)
        End Using
    End Sub


    Private Async Sub MbtnRemoveNoPOS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRemoveNoPOS.ItemClick
        If MessageIndigo.Show("Esta seguro que desea eliminar la liquidación Resolucion 1479 para este folio?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        Dim servicesToRemoveNoPOSLiquidation As List(Of IFolioDetail) = CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Where(Function(o) o.Id > -1 _
                                And o.RecordType = 2 AndAlso o.DistributionType = 5 AndAlso o.CodeAssociateService IsNot Nothing).ToList()
        Dim foliosToBlock As New List(Of CtrFolio)() 'Contiene los folios donde se encuentran distribuidos los items y el número del folio
        For Each codeassociate As String In servicesToRemoveNoPOSLiquidation.Select(Function(x) x.CodeAssociateService).ToList()
            For Each item In Me.DocumentParent.Manager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id <> Me._id AndAlso CType(d.Control, CtrFolio).Id <> 0).ToList()
                Dim Foliox As CtrFolio = CType(item.Control, CtrFolio)
                Dim ItemDistribute = _folio.Details.Where(Function(x) x.CodeAssociateService IsNot Nothing AndAlso x.CodeAssociateService.Equals(codeassociate)).ToList()
                If ItemDistribute.Count > 0 Then
                    foliosToBlock.Add(Foliox)
                End If
            Next
        Next
        foliosToBlock = foliosToBlock.Distinct().ToList()
        foliosToBlock.Add(Me)
        LockFoliosToLiquidate(foliosToBlock.Select(Function(x) x.Id).ToList(), True)
        Dim args As Object = New ExpandoObject()
        args.FolioId = Me.Id
        Dim myList As New ConcurrentBag(Of Object)()

        If Me.DocumentParent.Manager.View IsNot Nothing Then
            Parallel.ForEach(servicesToRemoveNoPOSLiquidation, Sub(i As IFolioDetail)
                                                                   If Me.DocumentParent.Manager.View.Documents.Where(Function(o) CType(o.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso CType(CType(o.Control, CtrFolio).GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Any(Function(x) x.CodeAssociateService IsNot Nothing AndAlso x.CodeAssociateService.Equals(i.CodeAssociateService) AndAlso x.Id <> i.Id)).FirstOrDefault() IsNot Nothing Then
                                                                       Dim d As Object = New ExpandoObject()
                                                                       d.ServiceOrderDetailDistributionId = i.Id
                                                                       d.ServiceOrderDetailId = i.Id
                                                                       d.CodeAssociate = i.CodeAssociateService
                                                                       Dim ctr As CtrFolio = CType(Me.DocumentParent.Manager.View.Documents.Where(Function(o) CType(o.Control, CtrFolio).GdcServices.DataSource IsNot Nothing AndAlso CType(CType(o.Control, CtrFolio).GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Any(Function(x) x.CodeAssociateService IsNot Nothing AndAlso x.CodeAssociateService.Equals(i.CodeAssociateService) AndAlso x.Id <> i.Id)).FirstOrDefault().Control, CtrFolio)
                                                                       d.ServiceOrderDetailDistributionIdFromDelete = CType(ctr.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Where(Function(o) o.CodeAssociateService IsNot Nothing AndAlso o.CodeAssociateService.Equals(i.CodeAssociateService) AndAlso o.Id <> i.Id).Select(Function(o) o.Id).FirstOrDefault()
                                                                       myList.Add(d)
                                                                   End If
                                                               End Sub)
        End If

        args.Details = New List(Of Object)(myList.ToArray())
        Using model As New MLiquidation()
            Me.IsAsyncOperation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.RemoveNoPOSLiquidation, args)
            LockFoliosToLiquidate(foliosToBlock.Select(Function(x) x.Id).ToList(), False)
            If res IsNot Nothing Then
                If res.StatusCode = eStatusResult.SUCCESS Then
                    Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                    'Se manda a recargar este folio y el destino
                    RaiseEvent RequiereReloadFolioList(Me, New RequiereReloadFolioListEventArgs(foliosToBlock.Select(Function(x) x.Id).ToList()))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Método que retorna los ids de los servicios seleccionados
    ''' </summary>
    ''' <returns></returns>
    Friend Function GetLinesSelected() As List(Of Integer)
        Dim idsServices As New List(Of Integer)()
        Dim selectedIndexs As Integer() = CType(GdcServices.MainView, GridView).GetSelectedRows()
        If selectedIndexs IsNot Nothing AndAlso selectedIndexs.Length > 0 Then
            For Each idx In selectedIndexs
                Dim Idselected As Integer = CType(CType(GdcServices.MainView, GridView).GetRow(idx), IFolioDetail).Id
                If Not idsServices.Contains(Idselected) Then
                    idsServices.Add(Idselected)
                End If
            Next
        End If
        Return idsServices
    End Function

    ''' <summary>
    ''' Ejecuta la liquidación de la cuota de recuperación
    ''' </summary>
    Private Sub MbtnLiquidateQuota_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLiquidateQuota.ItemClick
        Dim list = DirectCast(GdcServices.DataSource, IEnumerable(Of IFolioDetail))
        Dim args As Object = New ExpandoObject()
        args.AdmissionCode = Me._folio.AdmissionNumber.Trim()
        args.IdFolio = Me._folio.RevenueControlDetailId
        args.ListItemsApplyRecoveryFee = (From z In list Where z.ApplyRecoveryFee > 0 Select z.Id).ToList()
        If args.ListItemsApplyRecoveryFee.Count = 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron items seleccionados para liquidar cuota paciente"
            Exit Sub
        End If
        args.TotalsItemsApplyRecoveryFee = DirectCast(GdcServices.DataSource, IEnumerable(Of IFolioDetail)).ToList() _
            .FindAll(Function(x) x.ApplyRecoveryFee > 0).Sum(Function(y) y.GrandTotalSalesPrice)
        args.IdDetail = -1
        args.LiquidationType = 1 'AllItems
        args.liquidateRecovery = True
        LiquidateRecoveryFee(args)
    End Sub

    Private Sub MbtnDeletePatientQuota_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnDeletePatientQuota.ItemClick
        If Me.Status = 1 Then 'Si esta en estado registrado, permite mosdificar la cuota
            Dim args As Object = New ExpandoObject()
            args.AdmissionCode = Me._folio.AdmissionNumber.Trim()
            args.IdFolio = Me._folio.RevenueControlDetailId
            args.IdDetail = -1
            args.LiquidationType = 1 'AllItems
            args.liquidateRecovery = False
            LiquidateRecoveryFee(args)
        End If
    End Sub

    Private Async Sub MbtnPatientBonus_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnPatientBonus.ItemClick
        Using frmBonus As New FrmPatientBonus
            frmBonus.TotalEntity = Me.TotalEntity
            frmBonus.CareGroupId = Me.CareGroupId
            Dim list = DirectCast(GdcServices.DataSource, IEnumerable(Of IFolioDetail))
            If (From z In list Where z.ApplyRecoveryFee > 0 Select z.Id).ToList().Count = 0 Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "No se encontraron items seleccionados para liquidar bono paciente"
                Exit Sub
            End If
            Dim transparent As New FrmTransparent(frmBonus, False)
            If transparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim args As Object = New ExpandoObject()
                args.OperativeUnitId = FormOwner.IdOperatingUnitSelected
                args.FolioId = Me.Id
                args.BonusValue = frmBonus.BonusValue
                args.ShareType = frmBonus.ShareType
                args.TotalEntity = Me.TotalEntity
                args.ListItemsApplyRecoveryFee = (From z In list Where z.ApplyRecoveryFee > 0 Select z.Id).ToList()
                args.TotalsItemsApplyRecoveryFee = DirectCast(GdcServices.DataSource, IEnumerable(Of IFolioDetail)).ToList() _
                    .FindAll(Function(x) x.ApplyRecoveryFee > 0).Sum(Function(y) y.GrandTotalSalesPrice)

                Using model As New MLiquidation()
                    Me.IsAsyncOperation()
                    Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.SavePatientBonus, args)
                    If res IsNot Nothing Then
                        If res.StatusCode = eStatusResult.SUCCESS Then
                            Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                            'Se manda a recargar este folio y el destino
                            RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                        End If
                    Else
                        If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                            Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        Else
                            Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                        End If
                    End If
                    Me.IsAsyncOperation(False)
                End Using
            End If
        End Using
    End Sub

    Private Async Sub MbtnDeletePatientBonus_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnDeletePatientBonus.ItemClick
        Dim args As Object = New ExpandoObject()
        args.FolioId = Me.Id

        Using model As New MLiquidation()
            Me.IsAsyncOperation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.RemovePatientBonus, args)
            If res IsNot Nothing Then
                If res.StatusCode = eStatusResult.SUCCESS Then
                    Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                    'Se manda a recargar este folio y el destino
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica para la impresión de la factura o pre-factura, cual sea el caso
    ''' </summary>
    Private Sub MbtnSmallPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnSmallPrint.ItemClick
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        If Me._isInvoiced Then 'Si esta facturado, se imprime la factura
            Dim reportDef As New Reporter.rptSaleInvoiceReduced
            AddHandler reportDef.AfterPrint, Sub()
                                                 If waitForm.IsSplashFormVisible Then
                                                     waitForm.CloseWaitForm()
                                                 End If
                                             End Sub
            ReportHelper.ExecuteReport(reportDef, Me.FormOwner, Me.FormOwner.PermissionsForm, Me.InvoiceId, Me._folio.AdmissionNumber)
        Else 'Si no, la pre-factura
            Dim reportDefTirilla As New Reporter.rptInvoicePartial()
            AddHandler reportDefTirilla.AfterPrint, Sub()
                                                        If waitForm.IsSplashFormVisible Then
                                                            waitForm.CloseWaitForm()
                                                        End If
                                                    End Sub
            ReportHelper.ExecuteReport(reportDefTirilla, Me.FormOwner, Me.FormOwner.PermissionsForm, Me._id, Me._folio.AdmissionNumber)
        End If
    End Sub


    Private bgw_printAnullateReport As BackgroundWorker
    Dim reportDef As Reporter.rptCanceledInvoice

    Private Sub bgw_printAnullateReport_DoWork(sender As Object, e As DoWorkEventArgs)
        e.Result = reportDef.LoadDatasource()
    End Sub

    Private Sub bgw_printAnullateReport_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        reportDef.DataSource = e.Result
        ReportHelper.ExecuteReport(reportDef, Me.FormOwner, Me.FormOwner.PermissionsForm)
    End Sub

    ''' <summary>
    ''' Aqui se realiza la impresion del la factura anulada
    ''' </summary>
    Private Sub MbtnPrintAnnullateInvoice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnPrintAnnullateInvoice.ItemClick

        If Status = 5 Then
            MbtnLargePrint_ItemClick(Nothing, Nothing)
            Return
        End If

        'Aca se lanza la factura anulada, el id del Invoice se saca mediante (Me._id)
        If bgw_printAnullateReport Is Nothing Then
            bgw_printAnullateReport = New BackgroundWorker()
            AddHandler bgw_printAnullateReport.DoWork, AddressOf bgw_printAnullateReport_DoWork
            AddHandler bgw_printAnullateReport.RunWorkerCompleted, AddressOf bgw_printAnullateReport_RunWorkerCompleted
        End If
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        reportDef = New Reporter.rptCanceledInvoice()
        reportDef.ParametrosReporte = New Object() {Me._id}
        AddHandler reportDef.AfterPrint, Sub()
                                             If waitForm.IsSplashFormVisible Then
                                                 waitForm.CloseWaitForm()
                                             End If
                                         End Sub
        If Not bgw_printAnullateReport.IsBusy Then
            bgw_printAnullateReport.RunWorkerAsync()
        End If
    End Sub

    Private Async Sub MbtnLargePrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLargePrint.ItemClick
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        If SettingBilling Is Nothing Then
            Using model As New MBillingSetting(Me.Tag)
                SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(FormOwner.IdOperatingUnitSelected, False)
            End Using
        End If

        Dim printingMode As Byte = 0
        Using model As New MCtrFolio()
            printingMode = model.GetPrintgModeByIdRevenueControl(Me._id)
        End Using

        If Me._isInvoiced And SettingBilling?.LiquidateMasterAccount Then 'si esta facturado y si liquida cuenta madre
            Dim reportDefSaleInvoice = New Reporter.rptSaleInvoiceMotherAccount()
            reportDefSaleInvoice.SetValueCodingServices = CInt(printingMode)
            reportDefSaleInvoice.ParametrosReporte = New Object() {Me.InvoiceId, CInt(printingMode), SettingBilling.requiresConditionsSale}
            AddHandler reportDefSaleInvoice.AfterPrint, Sub()
                                                            If waitForm.IsSplashFormVisible Then
                                                                waitForm.CloseWaitForm()
                                                            End If
                                                        End Sub
            ReportHelper.ExecuteReport(reportDefSaleInvoice, Me.FormOwner, Me.FormOwner.PermissionsForm)

        ElseIf Me._isInvoiced And Not SettingBilling?.LiquidateMasterAccount Then 'si esta facturado y no liquida cuenta madre
            Dim reportDefSaleInvoice = New Reporter.rptSaleInvoice()
            reportDefSaleInvoice.SetValueCodingServices = CInt(printingMode)
            reportDefSaleInvoice.ParametrosReporte = New Object() {Me.InvoiceId, CInt(printingMode)}
            AddHandler reportDefSaleInvoice.AfterPrint, Sub()
                                                            If waitForm.IsSplashFormVisible Then
                                                                waitForm.CloseWaitForm()
                                                            End If
                                                        End Sub
            ReportHelper.ExecuteReport(reportDefSaleInvoice, Me.FormOwner, Me.FormOwner.PermissionsForm)

        ElseIf SettingBilling?.LiquidateMasterAccount Then 'no factura y si liquida cuenta madre

            If waitForm.IsSplashFormVisible Then
                waitForm.CloseWaitForm()
            End If

            Await PrintInvoicePartialbyMasterAccount(Me._id, Me._folio.AdmissionNumber)
        Else 'no factura y no liquida cuenta madre
            Dim reportDefPartial = New Reporter.rptInvoicePartial()
            reportDefPartial.SetValueCodingServices = CInt(printingMode)
            reportDefPartial.ParametrosReporte = New Object() {Me._id, Me._folio.AdmissionNumber}
            AddHandler reportDefPartial.AfterPrint, Sub()
                                                        If waitForm.IsSplashFormVisible Then
                                                            waitForm.CloseWaitForm()
                                                        End If
                                                    End Sub
            ReportHelper.ExecuteReport(reportDefPartial, Me.FormOwner, Me.FormOwner.PermissionsForm)
        End If

    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica para la impresión detallado cargos de la factura o pre-factura, cual sea el caso
    ''' </summary>
    Private Sub MbtnDetailLoadPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnDetailLoadPrint.ItemClick
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        If Me._isInvoiced Then 'Si esta facturado, se imprime la factura
            Dim reportDefSaleInvoiceLoad = New Reporter.rptSaleInvoiceLoad()
            Dim printingMode As Byte = 0
            Using model As New MCtrFolio()
                printingMode = model.GetPrintgModeByIdRevenueControl(_idReveneuControl)
            End Using
            reportDefSaleInvoiceLoad.ParametrosReporte = New Object() {Me.InvoiceId, CInt(printingMode)}
            AddHandler reportDefSaleInvoiceLoad.AfterPrint, Sub()
                                                                If waitForm.IsSplashFormVisible Then
                                                                    waitForm.CloseWaitForm()
                                                                End If
                                                            End Sub
            ReportHelper.ExecuteReport(reportDefSaleInvoiceLoad, Me.FormOwner, Me.FormOwner.PermissionsForm)

            'Si no, la pre-factura
        ElseIf _folio.IsMasterAccount <> 0 Then
            Dim reportDefPartialLoad = New Reporter.rptInvoicePartialMotherAccount()
            reportDefPartialLoad.ParametrosReporte = New Object() {Me._id, Me._folio.AdmissionNumber}
            AddHandler reportDefPartialLoad.AfterPrint, Sub()
                                                            If waitForm.IsSplashFormVisible Then
                                                                waitForm.CloseWaitForm()
                                                            End If
                                                        End Sub
            ReportHelper.ExecuteReport(reportDefPartialLoad, Me.FormOwner, Me.FormOwner.PermissionsForm)
        Else
            Dim reportDefPartialLoad = New Reporter.rptInvoicePartial()
            reportDefPartialLoad.ParametrosReporte = New Object() {Me._id, Me._folio.AdmissionNumber}
            AddHandler reportDefPartialLoad.AfterPrint, Sub()
                                                            If waitForm.IsSplashFormVisible Then
                                                                waitForm.CloseWaitForm()
                                                            End If
                                                        End Sub
            ReportHelper.ExecuteReport(reportDefPartialLoad, Me.FormOwner, Me.FormOwner.PermissionsForm)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica para la impresión de la factura basica relacionada al ingreso
    ''' </summary>
    Private Sub MbtnBasicBillCopayPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnBasicBillCopayPrint.ItemClick
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim reportDef As New Reporter.rptBasicBilling
        reportDef.ParametrosReporte = New Object() {_folio.BasicbillingCopayId}
        ReportHelper.ExecuteReport(reportDef, Me.FormOwner, Me.FormOwner.PermissionsForm)
    End Sub

    ''' <summary>
    ''' Aqui se bloquea y desbloquea el folio
    ''' </summary>
    Private Async Sub MbtnBlockUnblockFolio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnBlockFolio.ItemClick, MbtnUnblockFolio.ItemClick
        Me.IsAsyncOperation()
        Dim args As Object = New ExpandoObject()
        args.IdFolio = Me._folio.RevenueControlDetailId
        Using model As New MLiquidation()
            Dim res = Await model.ExecuteActionMethod(If(e.Item.Equals(Me.MbtnBlockFolio), Domain.Entities.LiquidationActionMethod.BlockFolio, Domain.Entities.LiquidationActionMethod.UnblockFolio), args)
            Me.IsAsyncOperation(False)
            If res IsNot Nothing Then
                If res.StateResult Then
                    'RaiseEvent BeginReloadDatasource(Me, New BeginReloadDatasourceEventArgs(Me))
                    Me.SetDatasourceAsync(Me._id, If(e.Item.Equals(Me.MbtnBlockFolio), 3, 1))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' evento que se encarga de separar la cuenta de un folio madre, a folio paciente - aseguradora
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiSeparateAccount_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiSeparateAccount.ItemClick
        Try
            Using Model As New MCtrFolio
                Me.IsAsyncOperation(True)
                Dim Result = Await Model.SeparateMasterAccountRestAsync(Me.AdmissionNumber, Me.Id)
                Me.ShowMessage(If(Result Is Nothing OrElse Not Result?.StateResult, EeventViewerImages.Advertencia, EeventViewerImages.Informacion)) = Result?.Message
                Me.IsAsyncOperation(False)
            End Using
            Me.RefreshAdmission()
        Catch ex As Exception
            Me.ShowMessage(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' boton que ejecuta la accion de unificar cuentas paciente-aseguradora
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiUnifyAccount_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiUnifyAccount.ItemClick
        Try
            Using Model As New MCtrFolio
                Me.IsAsyncOperation(True)
                Dim Result = Await Model.UnifyAccountRestAsync(Me.AdmissionNumber)
                Me.ShowMessage(If(Result Is Nothing OrElse Not Result?.StateResult, EeventViewerImages.Advertencia, EeventViewerImages.Informacion)) = Result?.Message
                Me.IsAsyncOperation(False)
            End Using
            Me.RefreshAdmission()
        Catch ex As Exception
            Me.ShowMessage(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' evento cuando oprimen el boton de cerrar folio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiCloseFolio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCloseFolio.ItemClick
        Try
            Using Model As New MCtrFolio
                Me.IsAsyncOperation(True)
                Dim Result = Await Model.CloseAccountRestAsync(Me.Id)
                Me.ShowMessage(If(Result Is Nothing OrElse Not Result?.StateResult, EeventViewerImages.Advertencia, EeventViewerImages.Informacion)) = Result?.Message
                Me.IsAsyncOperation(False)
            End Using
            RaiseEvent RequiereReloadAdmission(Me, New EventArgs())
        Catch ex As Exception
            Me.ShowMessage(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiOpenFolio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiOpenFolio.ItemClick
        Me.IsAsyncOperation()
        Dim args As Object = New ExpandoObject()
        args.IdFolio = Me._folio.RevenueControlDetailId
        Using model As New MLiquidation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.UnblockFolio, args)
            Me.IsAsyncOperation(False)
            If res IsNot Nothing Then
                If res.StateResult Then
                    Me.SetDatasourceAsync(Me._id, 1)
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo que se encarga de Agregar la opcion Sin recaudo de cuota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiFeeNotCollected_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiFeeNotCollected.ItemClick
        Me.IsAsyncOperation()
        Using formulario As New FrmPopUpFeeNotCollected(Me._folio.RevenueControlDetailId)
            Dim tr As New FrmTransparent(formulario, False)
            If tr.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Me.IsAsyncOperation(False)
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Exit Sub
            End If
        End Using
        Me.IsAsyncOperation(False)
    End Sub

    ''' <summary>
    ''' metodo que se encarga de eliminar el registro Sin recaudo de cuota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiDeleteFeeNotCollected_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiDeleteFeeNotCollected.ItemClick
        Me.IsAsyncOperation()
        Using model As New MLiquidation()
            Dim objParams As Object = New ExpandoObject()
            objParams.RevenueControlDetailId = Me._folio.RevenueControlDetailId
            objParams.FeeNotCollectedId = Me._folio.FeeNotCollectedId
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.DeleteFeeNotCollected, objParams)
            If res Is Nothing OrElse Not res.StateResult Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = If(res?.Message, "No se pudo guardar")
                Me.IsAsyncOperation(False)
                Exit Sub
            End If
            RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
            Me.ShowMessage(EeventViewerImages.Informacion) = res?.Message
        End Using
        Me.IsAsyncOperation(False)
    End Sub

#End Region

#Region "Drag & Drop"

    ''' <summary>
    ''' Información del hit sobre la rejilla
    ''' </summary>
    Private _downHitInfo As GridHitInfo

    ''' <summary>
    ''' Aqui inicia el arrastrado
    ''' </summary>
    Private Sub GdvSmallServices_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GdvSmallServices.MouseDown
        Dim view As GridView = CType(sender, GridView)

        Me._downHitInfo = Nothing
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))

        If Not System.Windows.Forms.Control.ModifierKeys = System.Windows.Forms.Keys.None Then
            Exit Sub
        End If

        If e.Button = System.Windows.Forms.MouseButtons.Left AndAlso hitInfo.InRow AndAlso hitInfo.HitTest <> GridHitTest.RowIndicator Then
            Me._downHitInfo = hitInfo
        End If
    End Sub

    ''' <summary>
    ''' Aqui se selecciona los objetos a arrastrar
    ''' </summary>
    Private Sub GdvSmallServices_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GdvSmallServices.MouseMove
        If Me._status = 1 AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.DistribuirFolio)) Then 'Solo si es un folio y no esta bloquedado. Además debe tener permiso para distribuir
            Dim view As GridView = CType(sender, GridView)
            If e.Button = System.Windows.Forms.MouseButtons.Left And Not Me._downHitInfo Is Nothing Then
                Dim dragSize As Size = System.Windows.Forms.SystemInformation.DragSize
                Dim DragRect As Rectangle = New Rectangle(New Point(Me._downHitInfo.HitPoint.X - dragSize.Width / 2, Me._downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize)

                If Not DragRect.Contains(New Point(e.X, e.Y)) Then
                    Dim itemsMove = Me.GetItemsToDistribute(view)
                    If itemsMove IsNot Nothing Then
                        view.GridControl.DoDragDrop(itemsMove, System.Windows.Forms.DragDropEffects.Move)
                        Me._downHitInfo = Nothing
                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = True
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se verifica si el tipo de objeto a soltar corresponde al esperado
    ''' </summary>
    Private Sub GdcServices_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles GdcServices.DragOver
        If Me._status = 1 AndAlso e.Data.GetDataPresent(GetType(ExpandoObject)) Then 'Solo si es un folio y no esta bloquedado
            Dim dragObj As Object = e.Data.GetData(GetType(ExpandoObject))
            If CType(dragObj.ProductsAndServices, List(Of Object)).Count > 0 Then
                e.Effect = System.Windows.Forms.DragDropEffects.Move
            Else
                e.Effect = System.Windows.Forms.DragDropEffects.None
            End If
        Else
            e.Effect = System.Windows.Forms.DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' Aqui se recupera los objetos arrastrados y se aplica la logica para lanzar
    ''' el modal de distribución
    ''' </summary>
    Private Sub GdcServices_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles GdcServices.DragDrop
        If Me.Status = 1 Then 'Solo si es un folio y no esta bloquedado
            Dim grid As GridControl = CType(sender, GridControl)
            Dim dragObj As Object = e.Data.GetData(GetType(ExpandoObject))
            If Me._id <> dragObj.SourceFolioId AndAlso ValidateItemCareGroup(dragObj) Then 'AndAlso Await ValidateItemMomologation(dragObj) Then
                Me.DistributeItemsFolio(DistributionType.OneByOne, dragObj, True, Nothing, Nothing, Me.Id, True)
            End If
        End If
    End Sub
#End Region

#Region "Save Definition To Xml"

    ''' <summary>
    ''' En cada uno de estos eventos se ejecuta el método que persiste
    ''' la definición de la vista a un archivo xml
    ''' </summary>
    Private Async Sub GdvSmallServices_ColumnPositionChanged(sender As Object, e As EventArgs) Handles GdvSmallServices.ColumnPositionChanged, GdvLargeServices.ColumnPositionChanged
        Await Me.SaveDefinitionToXmlAsync(sender.View)
    End Sub
    Private Async Sub GdvSmallServices_ColumnWidthChanged(sender As Object, e As Views.Base.ColumnEventArgs) Handles GdvSmallServices.ColumnWidthChanged, GdvLargeServices.ColumnWidthChanged
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub
    Private Async Sub GdvSmallServices_HideCustomizationForm(sender As Object, e As EventArgs) Handles GdvSmallServices.HideCustomizationForm, GdvLargeServices.HideCustomizationForm
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Liquida los items de producción
    ''' </summary>
    Private Async Sub LiquidarItemProduccion()
        'SelectedRows
        Using model As New MLiquidation
            IsAsyncOperation()
            Dim res = Await model.LiquidateItemProductionAsync(SelectedRows.Select(Function(m) m.ServiceOrderDetailId).ToList())

            If res.StateResult Then
                ShowMessage(EeventViewerImages.Informacion) = "Acción ejecutada correctamente"
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
            Else
                ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
            IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Liquida detalle de produccion
    ''' </summary>
    Private Async Sub LiquidarDetailProduccion()
        Using model As New MLiquidation
            IsAsyncOperation()
            Dim res = Await model.LiquidateDetailProductionAsync(SelectedRows.Select(Function(m) m.ServiceOrderDetailId).ToList())

            If res.StateResult Then
                ShowMessage(EeventViewerImages.Informacion) = "Acción ejecutada correctamente"
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
            Else
                ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
            IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Carga las definiciones de todas la vistas en el control
    ''' </summary>
    Public Async Sub LoadGridViewDefinitions()
        Await Me.LoadDefinitionFromXmlAsync(Me.GdvSmallServices)
        Await Me.LoadDefinitionFromXmlAsync(Me.GdvLargeServices)
    End Sub

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If Me.GdcServices.InvokeRequired Then
                                             Me.GdcServices.BeginInvoke(Sub()
                                                                            Me.LoadDefinitionFromXml(view)
                                                                        End Sub)
                                         Else
                                             Me.LoadDefinitionFromXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefinitionFromXml(ByVal view As GridView)
        If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            Try
                view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
            Catch

            End Try
        End If
    End Sub

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Function SaveDefinitionToXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If Me.GdcServices.InvokeRequired Then
                                             Me.GdcServices.BeginInvoke(Sub()
                                                                            Me.SaveDefinitionToXml(view)
                                                                        End Sub)
                                         Else
                                             Me.SaveDefinitionToXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Sub SaveDefinitionToXml(ByVal view As GridView)
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, ""))
        End If
        Dim filter As DevExpress.Data.Filtering.CriteriaOperator = view.ActiveFilterCriteria
        Dim filtertext As String = view.FindFilterText
        view.ActiveFilterCriteria = Nothing
        view.FindFilterText = ""
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.Name, MY_TYPE, Me.Name & "." & view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
        view.ActiveFilterCriteria = filter
        view.FindFilterText = filtertext
        RaiseEvent GridViewLayoutsChanged(Me, New EventArgs())
    End Sub

    ''' <summary>
    ''' Filtra los detalles que son homologaciones en el objeto de distribución
    ''' </summary>
    ''' <param name="toDistribute">Objeto a distribuir</param>
    ''' <returns>Objeto a distribuir sin homologaciones</returns>
    Private Function FilterHomologations(ByVal toDistribute As Object) As Object
        Dim homos = CType(toDistribute.ProductsAndServices, List(Of Object)).Where(Function(s) Not s.GuidHomologation.ToString().Equals(String.Empty)).ToList()
        Dim sb As New StringBuilder()
        For Each s In CType(homos, List(Of Object))
            sb.AppendLine(String.Format(ResourceManager.GetString("RemovedHomologationObject"), (s.Code & "-" & s.Description)))
        Next
        toDistribute.ProductsAndServices = CType(toDistribute.ProductsAndServices, List(Of Object)).Where(Function(s) s.GuidHomologation.ToString().Equals(String.Empty)).ToList()
        If Not sb.ToString().Trim().Equals(String.Empty) Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = sb.ToString()
        End If
        Return toDistribute
    End Function

    ''' <summary>
    ''' Función que empaqueta los items seleccionados
    ''' </summary>
    Private Async Sub PackageItems(serviceOrderDetail As ServiceOrderDetail, ByVal objParams As Object, frmServiceOrderDetail As FrmServiceOrderDetailLiquidation)
        If CType(objParams.lstServiceOrderDetailId, List(Of Object)).Count = 0 Then 'Si No hay items a empaquetar
            Exit Sub
        End If
        Using model As New MLiquidation()
            Dim res = Await model.PackageItems(serviceOrderDetail, objParams)
            frmServiceOrderDetail.AsyncLoader(False)
            If res IsNot Nothing Then
                If res.StateResult Then
                    frmServiceOrderDetail.Close()
                    Me.ShowMessage(EeventViewerImages.Informacion) = ResourceManager.GetString("MessagePacketCorrect", Me.GetType())
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            'Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene los id de los items a empaquetar
    ''' </summary>
    ''' <param name="view">The view.</param>
    ''' <returns></returns>
    Private Function GetItemsToPackage(ByVal view As GridView) As Object
        Dim myList As New ConcurrentBag(Of Object)()
        Dim myListSODD As New ConcurrentBag(Of Object)()
        Parallel.ForEach(view.GetSelectedRows().Where(Function(r) r > -1).ToList(), Sub(i)
                                                                                        Dim obj As IFolioDetail = CType(view.GetRow(i), IFolioDetail)
                                                                                        myList.Add(obj.ServiceOrderDetailId)
                                                                                        myListSODD.Add(obj.Id)
                                                                                    End Sub)
        Dim dragObj As Object = New ExpandoObject()
        dragObj.FolioId = Me.Id
        dragObj.AdmissionNumber = _folio.AdmissionNumber
        dragObj.PatientCode = FormOwner.TxtPatientCode.Text.Split("-")(1).Trim()
        dragObj.OperativeUnitId = FormOwner.IdOperatingUnitSelected
        dragObj.CareGroupId = Me.CareGroupId
        dragObj.AdmissionPatientType = AdmissionObject.PatientType
        dragObj.AdmissionType = AdmissionObject.AdmissionType
        Dim objView = CType(view.GetFocusedRow(), IFolioDetail)
        dragObj.SourceFolioWithPatientValue = IIf(_folio.TotalPatientWithDiscount > 0, 1, 0)
        dragObj.NivelModeratorSharePercentage = AdmissionObject.NivelModeratorSharePercentage
        dragObj.NivelModeratorShareTop = AdmissionObject.NivelModeratorShareTop

        dragObj.lstServiceOrderDetailId = New List(Of Object)(myList.ToArray())
        dragObj.lstServiceOrderDetailDistributionId = New List(Of Object)(myListSODD.ToArray())
        Return dragObj
    End Function

    ''' <summary>
    ''' Obtiene una lista de objetos anónimos con la información para distribuir en otro folio
    ''' </summary>
    ''' <param name="view">Vista que contiene los items</param>
    ''' <param name="all">Valor que indica si se obtienen todos los items</param>
    ''' <returns>Items a distribuir</returns>
    Private Function GetItemsToDistribute(ByVal view As GridView, Optional ByVal all As Boolean = False) As Object
        If Me.GdcServices.DataSource IsNot Nothing AndAlso CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Count > 0 Then
            Dim myList As New ConcurrentBag(Of Object)()

            If all Then
                Parallel.ForEach(CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)), Sub(obj As IFolioDetail)
                                                                                                     Dim dyObj As Object = New ExpandoObject()

                                                                                                     dyObj.Id = obj.Id
                                                                                                     dyObj.FunctionalUnitCodeName = obj.PerformsFunctionalUnitCodeName
                                                                                                     dyObj.ServiceOrderDetailId = obj.ServiceOrderDetailId
                                                                                                     dyObj.SourceDistribType = CByte(obj.DistributionType)
                                                                                                     dyObj.TargetDistribType = CByte(obj.DistributionType)
                                                                                                     dyObj.Code = If(obj.RecordType = 1, obj.IPSServiceCode, obj.ProductCode)
                                                                                                     dyObj.Description = If(obj.RecordType = 1, obj.IPSServiceName, obj.ProductName)
                                                                                                     dyObj.TotalFolioValue = CDec(obj.GrandTotalSalesPrice)
                                                                                                     dyObj.SourceFolioValue = CDec(obj.GrandTotalSalesPrice)

                                                                                                     dyObj.InvoiceQuantity = obj.InvoicedQuantity
                                                                                                     dyObj.SourceFolioGrandTotalDiscountValue = CDec(obj.GrandTotalDiscount)
                                                                                                     dyObj.TargetFolioGrandTotalDiscountValue = CDec(0)
                                                                                                     dyObj.TotalPatientSalesPrice = CDec(_folio.TotalPatientSalesPrice)
                                                                                                     dyObj.TotalPatientWithDiscount = CDec(_folio.TotalPatientWithDiscount)
                                                                                                     dyObj.SubTotalPatientSalesPrice = CDec(obj.SubTotalPatientSalesPrice)
                                                                                                     dyObj.PatientDiscountPercentage = CDec(_folio.PatientDiscountPercentage)
                                                                                                     dyObj.IsPackage = obj.IsPackage

                                                                                                     dyObj.TargetFolioValue = CDec(0)
                                                                                                     dyObj.ItemDate = obj.ServiceDate
                                                                                                     dyObj.GuidHomologation = obj.GuidHomologation
                                                                                                     dyObj.ApplyRIAS = obj.ApplyRIAS
                                                                                                     dyObj.RIASCupsId = obj.RIASCupsId
                                                                                                     myList.Add(dyObj)
                                                                                                 End Sub)
            Else
                Dim listGuidHomologations As New List(Of String)()
                If Me._downHitInfo IsNot Nothing AndAlso Me._downHitInfo.InGroupRow Then
                    Dim count As Integer = view.GetChildRowCount(Me._downHitInfo.RowHandle)
                    Parallel.For(0, count, Sub(i)
                                               Dim obj As IFolioDetail = DirectCast(view.GetRow(view.GetChildRowHandle(Me._downHitInfo.RowHandle, i)), IFolioDetail)
                                               If Not obj.GuidHomologation.Equals(String.Empty) AndAlso Not listGuidHomologations.Contains(obj.GuidHomologation) Then
                                                   listGuidHomologations.Add(obj.GuidHomologation)
                                               End If
                                               Dim dyObj As Object = New ExpandoObject()
                                               dyObj.Id = obj.Id
                                               dyObj.FunctionalUnitCodeName = obj.PerformsFunctionalUnitCodeName
                                               dyObj.ServiceOrderDetailId = obj.ServiceOrderDetailId
                                               dyObj.SourceDistribType = CByte(obj.DistributionType)
                                               dyObj.TargetDistribType = CByte(obj.DistributionType)
                                               dyObj.Code = If(obj.RecordType = 1, obj.IPSServiceCode, obj.ProductCode)
                                               dyObj.Description = If(obj.RecordType = 1, obj.IPSServiceName, obj.ProductName)
                                               dyObj.TotalFolioValue = CDec(obj.GrandTotalSalesPrice)
                                               dyObj.SourceFolioValue = CDec(obj.GrandTotalSalesPrice)

                                               dyObj.InvoiceQuantity = obj.InvoicedQuantity
                                               dyObj.SourceFolioGrandTotalDiscountValue = CDec(obj.GrandTotalDiscount)
                                               dyObj.TargetFolioGrandTotalDiscountValue = CDec(0)
                                               dyObj.TotalPatientSalesPrice = CDec(_folio.TotalPatientSalesPrice)
                                               dyObj.TotalPatientWithDiscount = CDec(_folio.TotalPatientWithDiscount)
                                               dyObj.SubTotalPatientSalesPrice = CDec(obj.SubTotalPatientSalesPrice)
                                               dyObj.PatientDiscountPercentage = CDec(_folio.PatientDiscountPercentage)
                                               dyObj.IsPackage = obj.IsPackage

                                               dyObj.TargetFolioValue = CDec(0)
                                               dyObj.ItemDate = obj.ServiceDate
                                               dyObj.GuidHomologation = obj.GuidHomologation
                                               dyObj.ApplyRIAS = obj.ApplyRIAS
                                               dyObj.RIASCupsId = obj.RIASCupsId
                                               myList.Add(dyObj)
                                           End Sub)
                Else
                    Parallel.ForEach(view.GetSelectedRows().Where(Function(r) r > -1).ToList(), Sub(i)
                                                                                                    Dim obj As IFolioDetail = DirectCast(view.GetRow(i), IFolioDetail)
                                                                                                    If Not obj.GuidHomologation.Equals(String.Empty) AndAlso Not listGuidHomologations.Contains(obj.GuidHomologation) Then
                                                                                                        listGuidHomologations.Add(obj.GuidHomologation)
                                                                                                    End If
                                                                                                    Dim dyObj As Object = New ExpandoObject()
                                                                                                    dyObj.Id = obj.Id
                                                                                                    dyObj.FunctionalUnitCodeName = obj.PerformsFunctionalUnitCodeName
                                                                                                    dyObj.ServiceOrderDetailId = obj.ServiceOrderDetailId
                                                                                                    dyObj.SourceDistribType = CByte(obj.DistributionType)
                                                                                                    dyObj.TargetDistribType = CByte(obj.DistributionType)
                                                                                                    dyObj.Code = If(obj.RecordType = 1, obj.IPSServiceCode, obj.ProductCode)
                                                                                                    dyObj.Description = If(obj.RecordType = 1, obj.IPSServiceName, obj.ProductName)
                                                                                                    dyObj.TotalFolioValue = CDec(obj.GrandTotalSalesPrice)
                                                                                                    dyObj.SourceFolioValue = CDec(obj.GrandTotalSalesPrice)

                                                                                                    dyObj.InvoiceQuantity = obj.InvoicedQuantity
                                                                                                    dyObj.SourceFolioGrandTotalDiscountValue = CDec(obj.GrandTotalDiscount)
                                                                                                    dyObj.TargetFolioGrandTotalDiscountValue = CDec(0)
                                                                                                    dyObj.TotalPatientSalesPrice = CDec(_folio.TotalPatientSalesPrice)
                                                                                                    dyObj.TotalPatientWithDiscount = CDec(_folio.TotalPatientWithDiscount)
                                                                                                    dyObj.SubTotalPatientSalesPrice = CDec(obj.SubTotalPatientSalesPrice)
                                                                                                    dyObj.PatientDiscountPercentage = CDec(_folio.PatientDiscountPercentage)
                                                                                                    dyObj.IsPackage = obj.IsPackage

                                                                                                    dyObj.TargetFolioValue = CDec(0)
                                                                                                    dyObj.ItemDate = obj.ServiceDate
                                                                                                    dyObj.GuidHomologation = obj.GuidHomologation
                                                                                                    dyObj.ApplyRIAS = obj.ApplyRIAS
                                                                                                    dyObj.RIASCupsId = obj.RIASCupsId
                                                                                                    myList.Add(dyObj)
                                                                                                End Sub)
                End If
                'Agrego los homologos que no fueron seleccionados
                For Each g In listGuidHomologations
                    For Each obj In CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).Where(Function(s) s.GuidHomologation.Equals(g)).ToList()
                        If Not myList.Any(Function(o) o.GuidHomologation.ToString().Equals(obj.GuidHomologation) AndAlso o.Id = obj.Id) Then
                            Dim dyObj As Object = New ExpandoObject()
                            dyObj.Id = obj.Id
                            dyObj.FunctionalUnitCodeName = obj.PerformsFunctionalUnitCodeName
                            dyObj.ServiceOrderDetailId = obj.ServiceOrderDetailId
                            dyObj.SourceDistribType = CByte(obj.DistributionType)
                            dyObj.TargetDistribType = CByte(obj.DistributionType)
                            dyObj.Code = If(obj.RecordType = 1, obj.IPSServiceCode, obj.ProductCode)
                            dyObj.Description = If(obj.RecordType = 1, obj.IPSServiceName, obj.ProductName)
                            dyObj.TotalFolioValue = CDec(obj.GrandTotalSalesPrice)
                            dyObj.SourceFolioValue = CDec(obj.GrandTotalSalesPrice)

                            dyObj.InvoiceQuantity = obj.InvoicedQuantity
                            dyObj.SourceFolioGrandTotalDiscountValue = CDec(obj.GrandTotalDiscount)
                            dyObj.TargetFolioGrandTotalDiscountValue = CDec(0)
                            dyObj.TotalPatientSalesPrice = CDec(_folio.TotalPatientSalesPrice)
                            dyObj.TotalPatientWithDiscount = CDec(_folio.TotalPatientWithDiscount)
                            dyObj.SubTotalPatientSalesPrice = CDec(obj.SubTotalPatientSalesPrice)
                            dyObj.PatientDiscountPercentage = CDec(_folio.PatientDiscountPercentage)
                            dyObj.IsPackage = obj.IsPackage

                            dyObj.TargetFolioValue = CDec(0)
                            dyObj.ItemDate = obj.ServiceDate
                            dyObj.GuidHomologation = obj.GuidHomologation
                            dyObj.ApplyRIAS = obj.ApplyRIAS
                            dyObj.RIASCupsId = obj.RIASCupsId
                            myList.Add(dyObj)
                        End If
                    Next
                Next
            End If

            Dim dragObj As Object = New ExpandoObject()
            dragObj.SourceFolioId = Me._id
            dragObj.TargetFolioId = 0
            dragObj.RevenueControlId = Me._idReveneuControl
            'dragObj.FolioOrder = Me.FormOwner.NextFolioOrder
            dragObj.FolioType = Me._folio.FolioType
            If Me._folio.ContractEntityId > 0 Then
                dragObj.ContractEntityId = Me._folio.ContractEntityId
            Else
                dragObj.ContractEntityId = Nothing
            End If
            If Me._folio.HealthAdministratorId > 0 Then
                dragObj.HealthAdministratorId = Me._folio.HealthAdministratorId
            Else
                dragObj.HealthAdministratorId = Nothing
            End If
            dragObj.ThirdPartyId = Me.GetThirdPartyId()
            dragObj.CareGroupId = Me.CareGroupId
            dragObj.User = SessionValues.Instance.UserIndigo

            dragObj.ProductsAndServices = New List(Of Object)(myList.ToArray())

            Dim objView = DirectCast(IIf(GdcServices.MainView.Name.Equals(GdvSmallServices.Name), GdvSmallServices, GdvLargeServices).GetFocusedRow(), IFolioDetail)
            dragObj.SourceFolioWithPatientValue = 0
            If objView IsNot Nothing Then
                dragObj.SourceFolioWithPatientValue = IIf(_folio.TotalPatientWithDiscount > 0, 1, 0)
            End If

            Return dragObj
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Muestra las columnas de tercero o de entidad administradora en el SearchLookUpEdit
    ''' </summary>
    ''' <param name="isThirdParty">Valor que indica si es tercero</param>
    Private Sub SetSleThirdPartyColumns(Optional ByVal isThirdParty As Boolean = True)
        If isThirdParty Then
            Me.SleThirdParty.Properties.DisplayMember = "NitName"
            Me.SleThirdParty.Properties.ValueMember = "Id"
            Me.ColThirdPartyNit.Visible = True
            Me.ColThirdPartyName.Visible = True
            Me.ColHealthAdministratorCode.Visible = False
            Me.ColHealthAdministratorName.Visible = False
        Else
            Me.SleThirdParty.Properties.DisplayMember = "CodeName"
            Me.SleThirdParty.Properties.ValueMember = "Id"
            Me.ColHealthAdministratorCode.Visible = True
            Me.ColHealthAdministratorName.Visible = True
            Me.ColThirdPartyNit.Visible = False
            Me.ColThirdPartyName.Visible = False
        End If
    End Sub

    Private _datasourceType As eDatasourceType

    Private _withPrintReport As Boolean

    ''' <summary>
    ''' Permite saber si se debe nulear la categoria al cambiar el grupo de atención
    ''' </summary>
    Private _nullCategories As Boolean = False

    ''' <summary>
    ''' Recarga y refresca la fuente de datos del folio
    ''' </summary>
    Public Sub ReloadDatasource()
        Me.SetDatasourceAsync(Me._id, Me._status)
    End Sub

    ''' <summary>
    ''' Asigna el id y el estado para realizar la carga del datasource asíncronamente
    ''' </summary>
    ''' <param name="id">Id del folio</param>
    ''' <param name="status">Estado del folio</param>
    Public Sub SetDatasourceAsync(ByVal id As Integer, ByVal status As Byte, Optional withPrintReport As Boolean = False, Optional nullCategories As Boolean = False) Implements ICtrFolio.SetDatasourceAsync
        _datasourceType = eDatasourceType.RevenueControlDetail
        RaiseEvent BeginReloadDatasource(Me, New BeginReloadDatasourceEventArgs(Me))
        Me.IsLoading()
        Me._id = id
        Me.Status = status
        repositoryItems.Clear()
        _withPrintReport = withPrintReport
        Me._nullCategories = nullCategories
        If Not BgwSetDatasourceAsync.IsBusy Then
            BgwSetDatasourceAsync.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' Asigna el id y el estado para realizar la carga del datasource asíncronamente
    ''' </summary>
    ''' <param name="id">Id del folio</param>
    ''' <param name="status">Estado del folio</param>
    Public Sub SetDatasourceAnnulateInvoiceAsync(ByVal id As Integer, ByVal status As Byte)
        _datasourceType = eDatasourceType.AnnullateInvoice
        RaiseEvent BeginReloadDatasource(Me, New BeginReloadDatasourceEventArgs(Me))
        'Return Task.Factory.StartNew(Sub()
        '                                 SetDatasourceAnnulateInvoice(id, status)
        '                             End Sub)
        Me.IsLoading()
        Me._id = id
        Me.Status = status
        If Not BgwSetDatasourceAnullateAsync.IsBusy Then
            BgwSetDatasourceAnullateAsync.RunWorkerAsync()
        End If
    End Sub

    Private Async Sub BgwSetDatasourceAsync_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BgwSetDatasourceAsync.DoWork
        'Ponemos el control en carga
        Using model As New MCtrFolio()
            _folio = model.ListRevenueControlAsync(Id, AdmissionNumber)
        End Using
    End Sub

    Private Async Sub BgwSetDatasourceAsync_RunWorkerCompletedAsync(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BgwSetDatasourceAsync.RunWorkerCompleted
        _isLoadingDatasource = True

        'Se trae los parámetros de facturacion
        If SettingBilling Is Nothing OrElse SettingBilling?.IdOperatingUnit <> FormOwner?.IdOperatingUnitSelected Then
            Using model As New MBillingSetting(Me.Tag)
                SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(FormOwner.IdOperatingUnitSelected, False)
            End Using
        End If

        'Asignamos el numero de factura o folio segun sea el caso
        If Me._folio.InvoiceNumber IsNot Nothing AndAlso Not Me._folio.InvoiceNumber.Trim().Equals(String.Empty) Then 'Si es factura
            Dim stringFormat = If(Me.Status = 6, "StrInvoiceAssociate", "StrInvoice")
            LblFolioTitle.Text = String.Format(ResourceManager.GetString(stringFormat, Me.GetType()), Me._folio.InvoiceNumber) & " - (" & CDate(Me._folio.InvoiceDate).ToString("yyyy-MM-dd hh:mm:ss tt") & " / " & Me._folio.InvoicedUser & ")"
            LblFolioTitle.ToolTip = String.Format(ResourceManager.GetString(stringFormat, Me.GetType()), Me._folio.InvoiceNumber) & " - (" & CDate(Me._folio.InvoiceDate).ToString("yyyy-MM-dd hh:mm:ss tt") & " / " & Me._folio.InvoicedUser & ")"
            Me._isInvoiced = True
            ''Se toma el tipo de moneda registrado en el folio/liquidacion
            LblFolioTitle.Text = $"{LblFolioTitle.Text} {Me._folio.CurrencyAbbreviation}"
            Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = Me._folio.CurrencyAbbreviation.GetNumberFormat
            Me.ColSmallGrandTotalSalesPrice = Window.Utils.FormatGrid(Me.ColSmallGrandTotalSalesPrice, Me._folio.CurrencyAbbreviation)
            Me.ColSmallGrandTotalSalesPrice.SummaryItem.Format = _culture
            Me.GdvSmallServices.GroupSummary.Item(0).Format = _culture
            Me.GdvLargeServices.GroupSummary.Item(0).Format = _culture
            Me.ColSmallTotalSalesPrice = Window.Utils.FormatGrid(Me.ColSmallTotalSalesPrice, Me._folio.CurrencyAbbreviation)
            Me.ColLargeTotalSalesPrice = Window.Utils.FormatGrid(Me.ColLargeTotalSalesPrice, Me._folio.CurrencyAbbreviation)
            Me.ColThirdPartySalesPrice = Window.Utils.FormatGrid(Me.ColThirdPartySalesPrice, Me._folio.CurrencyAbbreviation)
            Me.ColThirdPartyDiscount = Window.Utils.FormatGrid(Me.ColThirdPartyDiscount, Me._folio.CurrencyAbbreviation)
            Me.INDColTaxesL = Window.Utils.FormatGrid(Me.INDColTaxesL, Me._folio.CurrencyAbbreviation)
            Me.INDColTaxValue = Window.Utils.FormatGrid(INDColTaxValue, Me._folio.CurrencyAbbreviation)
            Me.ColLargeGrandTotalSalesPrice = Window.Utils.FormatGrid(Me.ColLargeGrandTotalSalesPrice, Me._folio.CurrencyAbbreviation)
            Me.INDColSubTotal = Window.Utils.FormatGrid(Me.INDColSubTotal, Me._folio.CurrencyAbbreviation)
            Me.INDColDiscount = Window.Utils.FormatGrid(Me.INDColDiscount, Me._folio.CurrencyAbbreviation)
            Me.INDColNetoValue = Window.Utils.FormatGrid(Me.INDColNetoValue, Me._folio.CurrencyAbbreviation)
            Me.INDColPatientSalesPrice = Window.Utils.FormatGrid(Me.INDColPatientSalesPrice, Me._folio.CurrencyAbbreviation)
            Me.ColSubTotalPatientSalesPrice = Window.Utils.FormatGrid(Me.ColSubTotalPatientSalesPrice, Me._folio.CurrencyAbbreviation)
            Me.INDColSubTotalL = Window.Utils.FormatGrid(Me.INDColSubTotalL, Me._folio.CurrencyAbbreviation)
            Me.INDColDiscountL = Window.Utils.FormatGrid(Me.INDColDiscountL, Me._folio.CurrencyAbbreviation)
            Me.INDColNetoValueL = Window.Utils.FormatGrid(Me.INDColNetoValueL, Me._folio.CurrencyAbbreviation)
            Me.RepTxtUnitValue.Mask.Culture = _culture
        Else 'Si es folio
            Me.LblFolioTitle.Text = String.Format(ResourceManager.GetString("StrFolio", Me.GetType()), Me._folio.FolioOrder)
            _patientQuotaResponsibleId = _folio.PatientQuotaResponsibleThirdPartyId
            _patientQuotaResponsibleText = _folio.ThirdPartyResponsibleQuotaNitName

            'validacion para mostrar el label de cuenta madre
            If SettingBilling?.LiquidateMasterAccount Then
                LayoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LblAccountMaster.Text = Utils.FolioMasterAccountNames.Item(Me._folio.IsMasterAccount)
                If Not {eMasterAccount.MasterAccount, eMasterAccount.EntityAccount, eMasterAccount.PatientAccount}.Contains(Me._folio.IsMasterAccount) Then
                    Me.LblAccountMaster.Text = String.Empty
                    LayoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

            End If

            If _patientQuotaResponsibleId > 0 Then
                LblFolioTitle.Text &= $" ({_patientQuotaResponsibleText})"
            End If

            Me._isInvoiced = False
        End If
        If _folio.Details.Count > 0 Then
            'Asignamos el Datasource al grid
            Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", _folio.Details)
        Else
            Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", Nothing)
        End If
        Me.SetMainView()
        'Aplicamos logica del boton cerrar en el documento (Folio)
        Me.SetCloseButton()
        'Realisamos la sumatoria de totales
        Me.SetTotals()
        'Asignamos los valores de grupo de atención y tercero
        SleCareGroup.Properties.NullText = Me._folio.CareGroupCodeName
        SleCareGroup.EditValue = _folio.CareGroupId
        DirectCast(Me, CtrFolio).DocumentParent.Caption = If(String.IsNullOrEmpty(Me._folio.ContractCodeName),
                String.Format(ResourceManager.GetString("StrTitleDocumentFolioLiq"), ResourceManager.GetString("LiquidationType_" & Me._folio.LiquidationType, "Billing")),
                String.Format(ResourceManager.GetString("StrTitleDocumentFolioContractLiq"), Me._folio.ContractCodeName, ResourceManager.GetString("LiquidationType_" & Me._folio.LiquidationType, "Billing")))
        Me.SleThirdParty.Properties.DataSource = Nothing
        Me.MeObservation.Text = _folio.Observation
        LoadStatusFolioDefault()
        If _nullCategories = False Then
            Me.SleCategories.Properties.NullText = _folio.InvoiceCategoryCodeName
            Me.SleCategories.EditValue = _folio.InvoiceCategoryId
        End If
        If Status = 1 Then
            If _folio.Details.Any(Function(x) x.Id > -1) _
                AndAlso _folio.VoucherValue <> 0 Then
                LayoutTotalPatient.Text = "Bono Paciente"
            Else
                LayoutTotalPatient.Text = "Total Paciente"
            End If
        End If
        If Me._folio.FolioType = 1 AndAlso Me._folio.IsMasterAccount <> 4 Then 'EAPB con Contrato
            Me.SleThirdParty.EditValue = Me._folio.HealthAdministratorId
            Me.SleThirdParty.Properties.NullText = Me._folio.HealthAdministratorCodeName
            Me.SleThirdParty.Properties.ReadOnly = True
            Me.LayoutThirdParty.Text = ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType())
            'Mostramos el label de total paciente si tiene valor
            If Me._totalPatient > 0 Then
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            Else
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End If
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        ElseIf Me._folio.FolioType = 2 AndAlso Me._folio.IsMasterAccount <> 4 Then ' EAPB sin contrato
            'Se pone la entidad administradora
            Me.SleThirdParty.EditValue = Me._folio.HealthAdministratorId
            Me.SleThirdParty.Properties.NullText = Me._folio.HealthAdministratorCodeName
            Me.SleThirdParty.Properties.ReadOnly = False
            Me.LayoutThirdParty.Text = ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType())
            'Mostramos el label de total paciente si tiene valor
            If Me._totalPatient > 0 Then
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            Else
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End If
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        ElseIf Me._folio.FolioType = 3 OrElse Me._folio.IsMasterAccount = 4 Then 'Particulares
            Me.SleThirdParty.EditValue = Me._folio.ThirdPartyId
            Me.SleThirdParty.Properties.NullText = Me._folio.ThirdPartyNitName
            Me.SleThirdParty.Properties.ReadOnly = False
            Me.LayoutThirdParty.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", Me.GetType())
            'Ocultamos el label de total paciente
            Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_ThirdParty", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        ElseIf Me._folio.FolioType = 4 Then '4 Aseguradoras
            'Se pone la entidad filtrada por tipo
            Me.SleThirdParty.EditValue = Me._folio.HealthAdministratorId
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
            Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
            'Ocultamos el label de total paciente
            Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        Else 'Tipo no definido, se bloquea todo el folio
            'Terminamos la carga del folio
            Me.IsLoading(False)
            Me.SetBlockFolio()
        End If

        If Me._isInvoiced Then 'Si esta facturado, ponemos en solo lectura los SearchLookUpEdit
            Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", True)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
        Else
            Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", False)
        End If
        If _withPrintReport Then
            MbtnLargePrint_ItemClick(Me, Nothing)
            _withPrintReport = False
        End If
        If Me._folio.BillingAuthorizationId > -1 Then
            'Lanzamos el evento que determina la resolución de facturación
            RaiseEvent BillingAuthotization(Me, New BillingAuthotizationEventArgs(Me._folio.BillingAuthorizationId))
        End If
        'Lanzamos el evento de terminación de carga
        RaiseEvent LoadDatasourceEnd(Me, New LoadDatasourceEndEventArgs(Me))
        _isLoadingDatasource = False
    End Sub

    Private Async Function LoadStatusFolioDefault() As Task
        Using Model As New MCtrFolio()
            If _folio.StatusFolioId Is Nothing Then
                Dim resulOperation = Await Model.GetSettingsBillingByIdUnitOperative(Me._idOperativeUnit)
                SettingBilling = resulOperation.ObjectEmbbeded
                If Status = 1 Then
                    SleStatusFolio.EditValue = SettingBilling.StatusFolioNewId
                    SleStatusFolio.Properties.NullText = SettingBilling.StatusFolioNewDescription
                ElseIf Status = 2 Then
                    SleStatusFolio.EditValue = SettingBilling.StatusFolioClosedId
                    SleStatusFolio.Properties.NullText = SettingBilling.StatusFolioClosedDescription
                    SleStatusFolio.ReadOnly = True
                Else
                    SleStatusFolio.EditValue = SettingBilling.StatusFolioNewId
                    SleStatusFolio.Properties.NullText = SettingBilling.StatusFolioNewDescription
                End If
            ElseIf Status = 2 Then
                Dim resulOperation = Await Model.GetSettingsBillingByIdUnitOperative(Me._idOperativeUnit)
                SettingBilling = resulOperation.ObjectEmbbeded
                SleStatusFolio.EditValue = SettingBilling.StatusFolioClosedId
                SleStatusFolio.Properties.NullText = SettingBilling.StatusFolioClosedDescription
                SleStatusFolio.ReadOnly = True
            Else
                SleStatusFolio.EditValue = _folio.StatusFolioId
                SleStatusFolio.Properties.NullText = _folio.StatusFolioName
            End If
        End Using
    End Function

    Private Sub BgwSetDatasourceAnullateAsync_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BgwSetDatasourceAnullateAsync.DoWork
        Using model As New MCtrFolio()
            Me._folio = model.ListRevenueControlForAnnullateInvoice(Id)
        End Using
    End Sub

    Private Sub BgwSetDatasourceAnullateAsync_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BgwSetDatasourceAnullateAsync.RunWorkerCompleted
        If _folio IsNot Nothing Then
            'Asignamos el numero de factura o folio segun sea el caso
            If Me._folio.InvoiceNumber IsNot Nothing AndAlso Not Me._folio.InvoiceNumber.Trim().Equals(String.Empty) Then 'Si es factura
                If Status = 3 OrElse Status = 4 Then
                    LblFolioTitle.Text = String.Format(ResourceManager.GetString("StrAnnullateInvoice", Me.GetType()), Me._folio.InvoiceNumber) & " - (" & CDate(Me._folio.InvoiceDate).ToString("yyyy-MM-dd hh:mm:ss tt") & " / " & Me._folio.InvoicedUser & ")"
                    LblFolioTitle.ToolTip = String.Format(ResourceManager.GetString("StrAnnullateInvoice", Me.GetType()), Me._folio.InvoiceNumber) & " - (" & CDate(Me._folio.InvoiceDate).ToString("yyyy-MM-dd hh:mm:ss tt") & " / " & Me._folio.InvoicedUser & ")"
                ElseIf Status = 5 Then
                    LblFolioTitle.Text = "Folio Reconocido"
                    LblFolioTitle.ToolTip = "Folio Reconocido"
                Else
                    Dim stringFormat = If(Me.Status = 6, "StrInvoiceAssociate", "StrInvoice")
                    Me.LblFolioTitle.Text = String.Format(ResourceManager.GetString(stringFormat, Me.GetType()), Me._folio.InvoiceNumber)
                End If
                Me._isInvoiced = True
            Else 'Si es folio
                Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", String.Format(ResourceManager.GetString("StrFolio", Me.GetType()), Me._folio.FolioOrder))
                Me._isInvoiced = False
            End If
            If Me._folio.Details.Count > 0 Then
                'Asignamos el Datasource al grid
                Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", _folio.Details)
            Else
                Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", Nothing)
            End If
            Me.SetMainView()

            'Aplicamos logica del boton cerrar en el documento (Folio)
            Me.SetCloseButton()
            'Realisamos la sumatoria de totales
            Me.SetTotalsAnnullateInvoice()
            'Asignamos los valores de grupo de atención y tercero
            SleCareGroup.Properties.NullText = Me._folio.CareGroupCodeName
            Me.SleThirdParty.Properties.DataSource = Nothing
            Me.MeObservation.Text = _folio.Observation
            Me.SleCategories.EditValue = _folio.InvoiceCategoryId
            Me.SleCategories.Properties.NullText = _folio.InvoiceCategoryCodeName

            If _folio.Details.Where(Function(x) x.Id > -1).Count > 0 AndAlso _folio.VoucherValue <> 0 Then
                LayoutTotalPatient.Text = "Bono Paciente"
            Else
                LayoutTotalPatient.Text = "Total Paciente"
            End If

            If Me._folio.FolioType = 1 Then 'EAPB con Contrato
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
                'Mostramos el label de total paciente si tiene valor
                If Me._totalPatient > 0 Then
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                End If
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            ElseIf Me._folio.FolioType = 2 Then ' EAPB sin contrato
                'Se pone la entidad administradora
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
                'Mostramos el label de total paciente si tiene valor
                If Me._totalPatient > 0 Then
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                End If
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            ElseIf Me._folio.FolioType = 3 Then 'Particulares
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.ThirdPartyNitName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", Me.GetType()))
                'Ocultamos el label de total paciente
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_ThirdParty", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            ElseIf Me._folio.FolioType = 4 Then '4 Aseguradoras
                'Se pone la entidad filtrada por tipo
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
                'Ocultamos el label de total paciente
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            Else 'Tipo no definido, se bloquea todo el folio
                'Terminamos la carga del folio
                Me.IsLoading(False)
                Me.SetBlockFolio()
            End If

            If Me._isInvoiced Then 'Si esta facturado, ponemos en solo lectura los SearchLookUpEdit
                Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", True)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
            Else
                Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", False)
                'Dim evnt As New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, SleCareGroup.EditValue)
                'SleCareGroup_EditValueChanging(SleCareGroup, evnt)
            End If

            'Lanzamos el evento de terminación de carga
            RaiseEvent LoadDatasourceEnd(Me, New LoadDatasourceEndEventArgs(Me))
        End If
    End Sub

    ''' <summary>
    ''' Asigna el id de la factura y el estado para realizar la carga del datasource
    ''' </summary>
    ''' <param name="status">Estado del folio</param>
    Public Sub SetDatasourceAnnulateInvoice(ByVal invoiceId As Integer, ByVal status As Byte)
        'Ponemos el control en carga
        'Me.IsLoading()

        Me._id = invoiceId
        Me.Status = status
        Using model As New MCtrFolio()
            Me._folio = model.ListRevenueControlForAnnullateInvoice(invoiceId)
        End Using
        If _folio IsNot Nothing Then
            'Asignamos el numero de factura o folio segun sea el caso
            If Me._folio.InvoiceNumber IsNot Nothing AndAlso Not Me._folio.InvoiceNumber.Trim().Equals(String.Empty) Then 'Si es factura
                If status = 3 OrElse status = 4 Then
                    Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", String.Format(ResourceManager.GetString("StrAnnullateInvoice", Me.GetType()), Me._folio.InvoiceNumber))
                ElseIf status = 5 Then
                    Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", "Folio Reconocido")
                Else
                    Dim stringFormat = If(Me.Status = 6, "StrInvoiceAssociate", "StrInvoice")
                    Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", String.Format(ResourceManager.GetString(stringFormat, Me.GetType()), Me._folio.InvoiceNumber))
                End If
                Me._isInvoiced = True
            Else 'Si es folio
                Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", String.Format(ResourceManager.GetString("StrFolio", Me.GetType()), Me._folio.FolioOrder))
                Me._isInvoiced = False
            End If
            If Me._folio.Details.Count > 0 Then
                'Asignamos el Datasource al grid
                Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", _folio.Details)
            Else
                Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", Nothing)
            End If
            Me.SetMainView()
            'Aplicamos logica del boton cerrar en el documento (Folio)
            Me.SetCloseButton()
            'Realisamos la sumatoria de totales
            Me.SetTotalsAnnullateInvoice()
            'Asignamos los valores de grupo de atención y tercero
            Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.NullText", Me._folio.CareGroupCodeName)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.DataSource", Nothing)
            'Window.Utils.SetValueToProperty(Me.TxtDescription, "Text", _dataHeader.Description)
            'Window.Utils.SetValueToProperty(Me.RgNoPos, "EditValue", _dataHeader.IsNoPos)
            If Me._folio.FolioType = 1 Then 'EAPB con Contrato
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
                'Mostramos el label de total paciente si tiene valor
                If Me._totalPatient > 0 Then
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                End If
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            ElseIf Me._folio.FolioType = 2 Then ' EAPB sin contrato
                'Se pone la entidad administradora
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
                'Mostramos el label de total paciente si tiene valor
                If Me._totalPatient > 0 Then
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                Else
                    Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                End If
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            ElseIf Me._folio.FolioType = 3 Then 'Particulares
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.ThirdPartyNitName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", Me.GetType()))
                'Ocultamos el label de total paciente
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_ThirdParty", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            ElseIf Me._folio.FolioType = 4 Then '4 Aseguradoras
                'Se pone la entidad filtrada por tipo
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
                Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
                'Ocultamos el label de total paciente
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
                Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
                'Terminamos la carga del folio
                Me.IsLoading(False)
            Else 'Tipo no definido, se bloquea todo el folio
                'Terminamos la carga del folio
                Me.IsLoading(False)
                Me.SetBlockFolio()
            End If

            If Me._isInvoiced Then 'Si esta facturado, ponemos en solo lectura los SearchLookUpEdit
                Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", True)
                Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
                Window.Utils.SetValueToProperty(Me.SleStatusFolio, "Properties.ReadOnly", True)
            Else
                Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", False)
                'Dim evnt As New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, SleCareGroup.EditValue)
                'SleCareGroup_EditValueChanging(SleCareGroup, evnt)
            End If

            'Lanzamos el evento de terminación de carga
            RaiseEvent LoadDatasourceEnd(Me, New LoadDatasourceEndEventArgs(Me))
        End If
    End Sub

    ''' <summary>
    ''' Asigna el id y el estado para realizar la carga del datasource
    ''' </summary>
    ''' <param name="id">Id del folio</param>
    ''' <param name="status">Estado del folio</param>
    Public Sub SetDatasource(ByVal id As Integer, ByVal status As Byte)
        'Ponemos el control en carga
        Me.IsLoading()

        Me._id = id
        Me.Status = status
        Using model As New MCtrFolio()
            Me._folio = model.ListRevenueControlAsync(id, AdmissionNumber)
        End Using
        'Asignamos el numero de factura o folio segun sea el caso
        If _folio.InvoiceNumber IsNot Nothing AndAlso Not Me._folio.InvoiceNumber.Trim().Equals(String.Empty) Then 'Si es factura
            Dim stringFormat = If(Me.Status = 6, "StrInvoiceAssociate", "StrInvoice")
            Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", String.Format(ResourceManager.GetString(stringFormat, Me.GetType()), Me._folio.InvoiceNumber))
            Me._isInvoiced = True
        Else 'Si es folio
            Window.Utils.SetValueToProperty(Me.LblFolioTitle, "Text", String.Format(ResourceManager.GetString("StrFolio", Me.GetType()), Me._folio.FolioOrder))
            Me._isInvoiced = False
        End If
        If Me._folio.Details.Count > 0 Then
            'Asignamos el Datasource al grid
            Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", _folio.Details)
        Else
            Window.Utils.SetValueToProperty(Me.GdcServices, "DataSource", Nothing)
        End If
        Me.SetMainView()

        'Aplicamos logica del boton cerrar en el documento (Folio)
        Me.SetCloseButton()
        'Realisamos la sumatoria de totales
        Me.SetTotals()
        'Asignamos los valores de grupo de atención y tercero
        Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.NullText", Me._folio.CareGroupCodeName)
        Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.DataSource", Nothing)
        'Window.Utils.SetValueToProperty(Me.TxtDescription, "Text", _dataHeader.Description)
        'Window.Utils.SetValueToProperty(Me.RgNoPos, "EditValue", _dataHeader.IsNoPos)
        If Me._folio.FolioType = 1 Then 'EAPB con Contrato
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
            Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
            'Mostramos el label de total paciente si tiene valor
            If Me._totalPatient > 0 Then
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            Else
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End If
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        ElseIf Me._folio.FolioType = 2 Then ' EAPB sin contrato
            'Se pone la entidad administradora
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
            Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
            'Mostramos el label de total paciente si tiene valor
            If Me._totalPatient > 0 Then
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            Else
                Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End If
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Always)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        ElseIf Me._folio.FolioType = 3 Then 'Particulares
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.ThirdPartyNitName)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
            Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", Me.GetType()))
            'Ocultamos el label de total paciente
            Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_ThirdParty", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        ElseIf Me._folio.FolioType = 4 Then '4 Aseguradoras
            'Se pone la entidad filtrada por tipo
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.NullText", Me._folio.HealthAdministratorCodeName)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", False)
            Window.Utils.SetValueToProperty(Me, "LayoutThirdParty.Text", ResourceManager.GetString("LyciSleThirdPartyText_Entity", Me.GetType()))
            'Ocultamos el label de total paciente
            Window.Utils.SetValueToProperty(Me, "LayoutTotalPatient.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LiquidateQuotaMenuButton.Visibility", DevExpress.XtraBars.BarItemVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LayoutTotalEntity.Text", ResourceManager.GetString("LyciTotalEntity_Entity", Me.GetType()))
            'Terminamos la carga del folio
            Me.IsLoading(False)
        Else 'Tipo no definido, se bloquea todo el folio
            'Terminamos la carga del folio
            Me.IsLoading(False)
            Me.SetBlockFolio()
        End If

        If Me._isInvoiced Then 'Si esta facturado, ponemos en solo lectura los SearchLookUpEdit
            Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", True)
            Window.Utils.SetValueToProperty(Me.SleThirdParty, "Properties.ReadOnly", True)
            Window.Utils.SetValueToProperty(Me.SleStatusFolio, "Properties.ReadOnly", True)
        Else
            Window.Utils.SetValueToProperty(Me.SleCareGroup, "Properties.ReadOnly", False)
            'Dim evnt As New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, SleCareGroup.EditValue)
            'SleCareGroup_EditValueChanging(SleCareGroup, evnt)
        End If

        If Me._folio.BillingAuthorizationId > -1 Then
            'Lanzamos el evento que determina la resolución de facturación
            RaiseEvent BillingAuthotization(Me, New BillingAuthotizationEventArgs(Me._folio.BillingAuthorizationId))
        End If

        'Lanzamos el evento de terminación de carga
        RaiseEvent LoadDatasourceEnd(Me, New LoadDatasourceEndEventArgs(Me))
    End Sub

    ''' <summary>
    ''' Asigna los totales tanto de la entidad como del paciente
    ''' </summary>
    Private Sub SetTotalsAnnullateInvoice()
        If Me._folio IsNot Nothing Then
            Me._totalEntity = 0
            Me._totalPatient = 0
            Me._total = 0
            Me.TotalDiscount = 0
            Me.GrandSubTotal = 0
            For Each reg As IFolioDetail In _folio.Details
                'Me._totalEntity += reg.ThirdPartySalesPrice
                Me._totalEntity += reg.ThirdPartySalesPrice
            Next

            If _folio.VoucherValue <> 0 Then
                Me._totalPatient = _folio.VoucherValue
                Window.Utils.SetValueToProperty(Me.LblTotalEntity, "Text", (_totalEntity - _totalPatient).ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture))
            Else
                Me._totalPatient = _folio.TotalPatientWithDiscount
                Window.Utils.SetValueToProperty(Me.LblTotalEntity, "Text", Me._totalEntity.ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture))
            End If

            Me._total = _folio.TotalFolio
            Window.Utils.SetValueToProperty(Me.PceTotalPatient, "Text", Me._totalPatient.ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture))

            Window.Utils.SetValueToProperty(Me.LblTotal, "Text", Me._total.ToString("C2", Infrastructure.CrossCutting.Base.SessionValues.Instance.Culture))
        End If
    End Sub
    ''' <summary>
    ''' Asigna los totales tanto de la entidad como del paciente
    ''' </summary>
    Private Sub SetTotals()
        If Me._folio IsNot Nothing Then
            Me._totalEntity = 0
            Me._totalPatient = 0
            Me._total = 0
            Me.TotalDiscount = 0
            Me.ApplyLogicThirdPartyBeneficiary = _folio.ApplyLogicThirdPartyBeneficiary
            Me.ThirdPartyId = GetThirdPartyId()
            Me.GrandSubTotal = _folio.Details.Sum(Function(s) s.SubTotalSalesPrice)
            Me._total = _folio.Details.Sum(Function(o) o.GrandTotalSalesPrice)
            Me.IsMasterAccount = _folio.IsMasterAccount
            If _folio?.Details?.Any(Function(s) s.GrandTotalDiscount > 0) Then
                Me.TotalDiscount = _folio.Details.Sum(Function(s) s.GrandTotalDiscount)
            End If

            If _folio.VoucherValue <> 0 Then
                Me._totalPatient = _folio.VoucherValue
            Else
                Me._totalPatient = _folio.TotalPatientWithDiscount
            End If

            If (FolioType = 3 AndAlso _isInvoiced) OrElse Me.SettingBilling?.LiquidateMasterAccount Then
                _totalEntity = Me._total
            Else
                Me._totalEntity += _folio.Details.Sum(Function(o) o.ThirdPartySalesPrice)
            End If

            Me.LblTotalEntity.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(Me._totalEntity, Me._folio.CurrencyAbbreviation)
            Me.PceTotalPatient.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(Me._totalPatient, Me._folio.CurrencyAbbreviation)
            Me.LblTotal.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(Me._total, Me._folio.CurrencyAbbreviation)
        End If
    End Sub

    ''' <summary>
    ''' Aplica la logica necesaria para mostrar o no el botón de cerrar el folio
    ''' </summary>
    Private Sub SetCloseButton()
        If Me._folio IsNot Nothing Then
            If Me._folio.Status = 1 AndAlso Me._folio.Details.Count = 0 Then
                Window.Utils.SetValueToProperty(Me, "DocumentParent.Properties.AllowClose", DevExpress.Utils.DefaultBoolean.True)
            Else
                Window.Utils.SetValueToProperty(Me, "DocumentParent.Properties.AllowClose", DevExpress.Utils.DefaultBoolean.False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna el texto al SearchLookUpEdit interno
    ''' </summary>
    ''' <param name="text">Texto a asignar</param>
    Public Sub SetSleNullText(ByVal text As String) Implements ICtrFolio.SetSleNullText
        Me.SleFindAdmission.SetNullText(text)
    End Sub

    ''' <summary>
    ''' Asigna la vista a la rejilla que corresponda si el folio es unico
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetMainView()
        If Me._isUnique OrElse Me._isMaximi Then
            If _datasourceType = eDatasourceType.AnnullateInvoice Then
                GridColSurchargeApply.Visible = False
                GridColDistributionType.Visible = False
                GridColApplyRecoveryFee.Visible = False
                GridColRecoveryFeeType.Visible = False
            Else
                GridColSurchargeApply.Visible = True
                GridColDistributionType.Visible = True
                GridColApplyRecoveryFee.Visible = True
                GridColRecoveryFeeType.Visible = True
            End If

            Me.LoadDefinitionFromXmlAsync(Me.GdvLargeServices)
            Me.GdcServices.MainView = Me.GdvLargeServices

        Else
            Me.LoadDefinitionFromXmlAsync(Me.GdvSmallServices)
            Me.GdcServices.MainView = Me.GdvSmallServices
        End If
        ShowOrHideGridColumns(SettingBilling?.LiquidateMasterAccount)
    End Sub

    ''' <summary>
    ''' Indica si el folio esta realizando una operación asíncrona
    ''' </summary>
    ''' <param name="isAsyncOperation">Valor que indica si se esta realizando una operación asíncrona</param>
    Public Sub IsAsyncOperation(Optional ByVal isAsyncOperation As Boolean = True)
        If isAsyncOperation Then
            Cursor = Me._indigoCursor
            DdbActions.Enabled = False
            LayoutBodyFolio.Enabled = False
            LayoutAsyncOperationBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Cursor = System.Windows.Forms.Cursors.Default
            Me.DdbActions.Enabled = True
            LayoutBodyFolio.Enabled = True
            LayoutAsyncOperationBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' indica si el folio se está cargado o ya terminó
    ''' </summary>
    Private _isLoading As Boolean

    ''' <summary>
    ''' Indica si el folio en estado de carga
    ''' </summary>
    ''' <param name="isLoading">Valor que indica si el folio esta cargando</param>
    Public Sub IsLoading(Optional ByVal isLoading As Boolean = True)
        _isLoading = isLoading
        If isLoading Then
            Cursor = Me._indigoCursor
            Me.DdbActions.Enabled = False
            LayoutBodyFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutProgressPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            Cursor = System.Windows.Forms.Cursors.Default
            DdbActions.Enabled = True
            LayoutProgressPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutBodyFolio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Bloquea o no el control de folio
    ''' </summary>
    ''' <param name="blockFolio">Valor que indica si se bloquea el control</param>
    Public Sub SetBlockFolio(Optional ByVal blockFolio As Boolean = True)
        If blockFolio Then
            Window.Utils.SetValueToProperty(Me.DdbActions, "Enabled", False)
            Window.Utils.SetValueToProperty(Me, "LayoutBodyFolio.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LayoutBlockFolio.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        Else
            Window.Utils.SetValueToProperty(Me.DdbActions, "Enabled", True)
            Window.Utils.SetValueToProperty(Me, "LayoutBlockFolio.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Window.Utils.SetValueToProperty(Me, "LayoutBodyFolio.Visibility", DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el Id del tercero
    ''' </summary>
    ''' <returns>Id del tercero</returns>
    Public Function GetThirdPartyId() As Integer
        If (Me._folio.FolioType = 1) OrElse (Me._folio.FolioType = 2) Then
            Return Me._folio.ThirdPartyId
        Else
            If Me.SleThirdParty.Properties.DataSource IsNot Nothing Then
                Return Me.SleThirdParty.EditValue
            Else
                Return Me._folio.ThirdPartyId
            End If
        End If
    End Function

    ''' <summary>
    ''' Realiza la retarificación
    ''' </summary>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="fromDistribution">Valor que indica si la orden de retarificar viene de una distribución</param>
    Private Async Sub ChangeRateServices(ByVal caregroupId As Integer, Optional ByVal fromDistribution As Boolean = False, Optional homologations As List(Of Homologation) = Nothing, Optional listServiceOrderDetailWithQx As List(Of ServiceOrderDetail) = Nothing, Optional frmQx As FrmPopUpServiceOrderDetailQx = Nothing, Optional nullCategories As Boolean = False, Optional IsEditValueChangedCareGroup As Boolean = False)

        'Si este método es llamado desde el cambio de grupo de atención se valida que si hay algun cups que aplique a rias, el grupo de atención seleccionado maneje rias
        If IsEditValueChangedCareGroup Then
            Dim dataServices = CType(GdcServices.DataSource, IEnumerable(Of IFolioDetail))
            If dataServices IsNot Nothing AndAlso dataServices.Count > 0 Then
                If (From x In dataServices Where x.ApplyRIAS = True Select x).Count > 0 Then
                    Dim careGroupXpo As ContractCareGroupReportXpo = Nothing
                    Await Task.Factory.StartNew(Sub()
                                                    Using model As New MLiquidation()
                                                        careGroupXpo = model.GetCareGroupById(caregroupId)
                                                    End Using
                                                End Sub)
                    If careGroupXpo IsNot Nothing AndAlso careGroupXpo.ApplyRIAS = False Then
                        Me.ShowMessage(EeventViewerImages.Advertencia) = "Hay CUPS en la rejilla que aplican a RIAS pero el grupo de atención seleccionado no aplica a RIAS"
                        If _previusValueCareGroup <> 0 Then
                            Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                        End If
                        Me.IsAsyncOperation(False)
                        Exit Sub
                    End If
                End If
            End If
        End If

        Using model As New MLiquidation()
            Dim onlyRateChange As Boolean = Not fromDistribution 'Si solo se ha llevado a cabo la retarificacion: true (cuando se cambia el caregroup), false(cuando se hace distribucion y luego retarifica)
            Dim res As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) = Await model.ChangeRateServices(Me.Id, caregroupId, Me._patientGenus, Me._patientBirth, homologations, onlyRateChange, _folio.ThirdPartyPatientId, _healthAdministratorAdmissiontId, listServiceOrderDetailWithQx, FormOwner.IdOperatingUnitSelected)
            If res.StateResult Then
                _previusValueCareGroup = 0
                If frmQx IsNot Nothing Then
                    frmQx.CanForceClose = True
                    frmQx.Close()
                End If
                Me.ShowMessage(EeventViewerImages.Informacion) = "La Retarificación se realizó correctamente"
                RaiseEvent RequiereReloadAdmission(Me, New EventArgs())
                Me.IsAsyncOperation(False)
            Else
                'OptionChangeRateServices(res.Message, caregroupId, res.MessageResult, res.ObjectEmbbeded, fromDistribution)
                OptionChangeRateServices(res, caregroupId, fromDistribution)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Options the change rate services.
    ''' </summary>
    ''' Public Sub OptionChangeRateServices(optionRate As String, caregroupId As Integer, messageResult As List(Of String), homologations As List(Of Homologation), fromDistribution As Boolean, Optional redistribute As Boolean = False)
    Public Sub OptionChangeRateServices(res As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)), caregroupId As Integer, fromDistribution As Boolean, Optional redistribute As Boolean = False)
        Select Case res.Message
            Case "{ERR1}" 'Errores en la homologación
                Dim list As New List(Of Object)()
                res.MessageResult.Distinct().ToList().ForEach(Sub(m)
                                                                  list.Add(New ExpandoObject())
                                                                  list(list.Count - 1).CupsCode = m
                                                              End Sub)
                Using tras As New FrmTransparent(New FrmHomologation(list), False)
                    tras.ShowDialog(Me)
                End Using
                If _previusValueCareGroup <> 0 Then
                    Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                End If
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Me.IsAsyncOperation(False)
            Case "{ERR2}" 'Multiples homologaciones
                Dim frmHomo As New FrmHomologation(res.ObjectEmbbeded)
                frmHomo.CancelButtonVisible = (Not fromDistribution)
                Using tras As New FrmTransparent(frmHomo, False)
                    If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                        Me.ChangeRateServices(caregroupId, fromDistribution, frmHomo.Homologations)
                    Else
                        If _previusValueCareGroup <> 0 Then
                            Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                        End If
                        RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                        Me.IsAsyncOperation(False)
                    End If
                End Using
            Case "{ERR3}" 'Errores en la retarificación 
                Dim list As New List(Of Object)()
                res.MessageResult.Distinct().ToList().ForEach(Sub(m)
                                                                  list.Add(New ExpandoObject())
                                                                  list(list.Count - 1).CupsCode = m
                                                              End Sub)
                Using tras As New FrmTransparent(New FrmHomologation(list), False)
                    tras.ShowDialog(Me)
                End Using
                If _previusValueCareGroup <> 0 Then
                    Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                End If
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Me.IsAsyncOperation(False)
            Case "{ERR4}" 'Quirurgico (Necesita ingresar los datos de medicos)
                Me.ShowMessage(EeventViewerImages.Informacion) = "Por favor llene la información de los detalles Quirugicos"
                Dim objParams As Object = New ExpandoObject()
                objParams.SourceFolioId = Me._id
                objParams.NewCareGroupToRetarific = caregroupId
                Dim myList As New ConcurrentBag(Of Object)()
                Parallel.ForEach(CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)), Sub(obj As IFolioDetail)
                                                                                                     Dim dyObj As Object = New ExpandoObject()
                                                                                                     dyObj.Id = obj.Id
                                                                                                     dyObj.ServiceOrderDetailId = obj.ServiceOrderDetailId
                                                                                                     myList.Add(dyObj)
                                                                                                 End Sub)
                objParams.ProductsAndServices = New List(Of Object)(myList.ToArray())
                OpenFormServiceOrderDetail(res.ObjectEmbbededAux, objParams, True)
            Case Else 'Error desconocido
                Me.ShowMessage(eStatusResult.WARNING) = res.Message
                'Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageDontChangeRateServices", Me.GetType())
                If _previusValueCareGroup <> 0 Then
                    Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                End If
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                Me.IsAsyncOperation(False)
        End Select
    End Sub

    ''' <summary>
    ''' Valida que si de los items a distribuir existe alguno empaquetado, que el grupo de atención destino sea el mismo de origen
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidatePackageItems(listItemsToDistribute As List(Of Object), caregroupSouce As Integer) As Boolean
        'If listItemsToDistribute.Where(Function(o) o.IsPackage = True AndAlso o.SourceDistribType = 1).Count() > 0 AndAlso CareGroupId <> caregroupSouce Then
        '    Return False
        'End If

        If listItemsToDistribute.Where(Function(o) o.IsPackage = True).Any() AndAlso CareGroupId <> caregroupSouce Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Realiza la distribución del folio
    ''' </summary>
    ''' <param name="objParams">Parámetros a usar en la distribución</param>
    ''' <param name="IsTotal">Valor que indica si se va a realizar una distribución al 100% de los detalles seleccionados</param>
    Public Async Sub DistributeItemsFolio(ByVal distribType As DistributionType, ByVal objParams As Object, ByVal IsTotal As Boolean, listHomologation As List(Of Homologation), listServiceOrderDetailWithQx As List(Of ServiceOrderDetail), Optional ByVal targetFolioId As Integer = -1, Optional IsDragAndDrop As Boolean = False)
        If CType(objParams.ProductsAndServices, List(Of Object)).Count = 0 Then 'Si al filtrar los homologos la lista quedo vacia
            Exit Sub
        End If

        Dim foliosToLiquidate As List(Of Integer) = {Me.Id, CInt(objParams.SourceFolioId)}.ToList()
        LockFoliosToLiquidate(foliosToLiquidate, True)
        'Dim canReloadFolios As Boolean = True
        objParams.ChangeRateServicesNeccesary = False
        objParams.HealthAdministratorId = _healthAdministratorAdmissiontId
        objParams.ThirdPartyPatientId = _folio.ThirdPartyPatientId

        'Si cuando se realice el dragAndDrop hay cups que manejen rias, se valida que el grupo de atención al cual van a ir aplique a rias
        If IsDragAndDrop AndAlso (From x In CType(objParams.ProductsAndServices, List(Of Object)) Where x.ApplyRIAS = True Select x).Count > 0 Then
            'Consultamos el grupo de atención del folio al cual va a recaer los cups
            Dim careGroupXpo As ContractCareGroupReportXpo = Nothing
            Await Task.Factory.StartNew(Sub()
                                            Using model As New MLiquidation()
                                                careGroupXpo = model.GetCareGroupById(Me._folio.CareGroupId)
                                            End Using
                                        End Sub)
            If careGroupXpo IsNot Nothing AndAlso careGroupXpo.ApplyRIAS = False Then
                LockFoliosToLiquidate(foliosToLiquidate, False)
                Me.ShowMessage(EeventViewerImages.Advertencia) = "Hay CUPS seleccionados que aplican a RIAS pero el grupo de atención destino no aplica a RIAS"
                Exit Sub
            End If
        End If

        If IsTotal Then
            If ValidatePackageItems(CType(objParams.ProductsAndServices, List(Of Object)), objParams.CareGroupId) = False Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "Los items empaquetados no se pueden distribuir a un folio con diferente grupo de atención"
                LockFoliosToLiquidate(foliosToLiquidate, False)
                Exit Sub
            End If

            objParams.TotalFolioValue = CDec(0)
            If listHomologation Is Nothing OrElse Not listHomologation.Any() Then
                Parallel.ForEach(CType(objParams.ProductsAndServices, List(Of Object)), Sub(d)
                                                                                            d.TargetFolioValue = CDec(d.SourceFolioValue)
                                                                                            d.TargetFolioGrandTotalDiscountValue = CDec(d.SourceFolioGrandTotalDiscountValue)
                                                                                            d.SourceFolioValue = CDec(0)
                                                                                            objParams.TotalFolioValue += CDec(d.TargetFolioValue)
                                                                                        End Sub)
            End If
            objParams.TargetFolioId = targetFolioId
            objParams.EnumType = CInt(distribType)
            objParams.DistribType = 2
            objParams.CareGroupIdTarget = IIf(Me.SleCareGroup.EditValue Is Nothing, Me._folio.CareGroupId, Me.SleCareGroup.EditValue)
            Dim myCaregroupId As Integer = If(Me.SleCareGroup.EditValue Is Nothing, Me._folio.CareGroupId, Me.SleCareGroup.EditValue)
            If myCaregroupId <> objParams.CareGroupId Then
                objParams.PatientGenus = Me._patientGenus
                objParams.PatientBirth = Me._patientBirth
                objParams.OnlyRateChange = False
            End If
        Else
            'Open FrmDistribution
            If listHomologation Is Nothing AndAlso listServiceOrderDetailWithQx Is Nothing Then
                Dim frmDist As New FrmDistribution(distribType, Me.FormOwner, Me.FormOwner.ListFoliosIdOrder.Where(Function(f) f.FolioId <> objParams.SourceFolioId).ToList(), objParams, Me._smlv)
                Using form As New FrmTransparent(frmDist, False)
                    If form.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                        Dim targetFolio = GetFolioCtrById(frmDist.TargetFolioIdSelected)

                        If targetFolio IsNot Nothing Then
                            If ValidatePackageItems(CType(objParams.ProductsAndServices, List(Of Object)), targetFolio.CareGroupId) = False Then
                                Me.ShowMessage(EeventViewerImages.Advertencia) = "Los items empaquetados no se pueden distribuir a un folio con diferente grupo de atención"
                                LockFoliosToLiquidate(foliosToLiquidate, False)
                                Exit Sub
                            End If
                        End If

                        objParams.TargetFolioIdSelected = frmDist.TargetFolioIdSelected
                        If frmDist.TargetFolioIdSelected > 0 Then
                            LockFoliosToLiquidate({frmDist.TargetFolioIdSelected}.ToList(), True)
                        End If
                    Else
                        LockFoliosToLiquidate(foliosToLiquidate, False)
                        Exit Sub
                    End If
                End Using
            End If
            objParams.TotalFolioValue = CDec(0)
            For Each d In objParams.ProductsAndServices
                objParams.TotalFolioValue += CDec(d.TargetFolioValue)
            Next
            If objParams.DistribType = 4 Then
                'en este caso se necesita retarificar también
                objParams.TargetFolioId = -1 'porque es el mismo folio origen
            End If
            CType(objParams.ProductsAndServices, List(Of Object)).RemoveAll(Function(o) o.TargetFolioValue = 0)
            objParams.PatientGenus = Me._patientGenus
            objParams.PatientBirth = Me._patientBirth
            objParams.OnlyRateChange = False
            objParams.HealthAdministratorId = IIf(LayoutThirdParty.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", Me.GetType()), Nothing, _folio.HealthAdministratorId)
        End If
        objParams.AdmissionPatientType = AdmissionObject.PatientType
        objParams.AdmissionType = AdmissionObject.AdmissionType
        objParams.NivelModeratorSharePercentage = AdmissionObject.NivelModeratorSharePercentage
        objParams.NivelModeratorShareTop = AdmissionObject.NivelModeratorShareTop
        Dim obj = DirectCast(CType(GdcServices.MainView, GridView).GetFocusedRow(), IFolioDetail)
        objParams.TargetFolioWithPatientValue = 0
        If obj IsNot Nothing Then
            objParams.TargetFolioWithPatientValue = IIf(_folio.TotalPatientWithDiscount > 0, 1, 0)
        End If
        Using model As New MLiquidation()
            Dim res As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) = Await model.DistributeFolio(objParams, listHomologation, listServiceOrderDetailWithQx)
            If res IsNot Nothing Then
                If res.StateResult Then
                    If IsTotal Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = "La Distribución se realizó correctamente"
                        RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(objParams.SourceFolioId))
                    Else
                        If objParams.TargetFolioIdSelected = -1 Then
                            RaiseEvent RequiereReloadAdmission(Me, New EventArgs())
                        Else
                            'Se manda a recargar este folio y el destino
                            foliosToLiquidate.Add(objParams.TargetFolioId)
                            RaiseEvent RequiereReloadFolioList(Me, New RequiereReloadFolioListEventArgs(foliosToLiquidate.Distinct().ToList()))
                        End If
                    End If
                    'LockFoliosToLiquidate(foliosToLiquidate, False)
                Else
                    Select Case res.Message
                        Case "{ERR1}" 'Errores en la homologación
                            Dim list As New List(Of Object)()
                            res.MessageResult.Distinct().ToList().ForEach(Sub(m)
                                                                              list.Add(New ExpandoObject())
                                                                              list(list.Count - 1).CupsCode = m
                                                                          End Sub)
                            Using tras As New FrmTransparent(New FrmHomologation(list), False)
                                tras.ShowDialog(Me)
                            End Using
                            LockFoliosToLiquidate(foliosToLiquidate, False)
                        Case "{ERR2}" 'Multiples homologaciones 
                            Dim frmHomo As New FrmHomologation(res.ObjectEmbbeded)
                            frmHomo.CancelButtonVisible = True
                            If Not IsTotal Then
                                frmHomo.OnlySelectOne = True
                            End If
                            Using tras As New FrmTransparent(frmHomo, False)
                                If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                                    DistributeItemsFolio(distribType, objParams, IsTotal, frmHomo.Homologations, Nothing, targetFolioId)
                                Else
                                    LockFoliosToLiquidate(foliosToLiquidate, False)
                                End If
                            End Using
                        Case "{ERR3}" 'Errores en la retarificación 
                            Dim list As New List(Of Object)()
                            res.MessageResult.Distinct().ToList().ForEach(Sub(m)
                                                                              list.Add(New ExpandoObject())
                                                                              list(list.Count - 1).CupsCode = m
                                                                          End Sub)
                            Using tras As New FrmTransparent(New FrmHomologation(list), False)
                                tras.ShowDialog(Me)
                            End Using
                            LockFoliosToLiquidate(foliosToLiquidate, False)
                        Case "{ERR4}" 'Quirurgico (Necesita ingresar los datos de medicos)
                            Me.ShowMessage(EeventViewerImages.Informacion) = "Por favor llene la información de los detalles Quirugicos"
                            OpenFormServiceOrderDetail(res.ObjectEmbbededAux, objParams, False)
                        Case Else 'Error desconocido
                            If res.MessageResult Is Nothing OrElse res.MessageResult.Count = 0 Then
                                Me.ShowMessage(EeventViewerImages.Advertencia) = String.Format("No se pudo realizar la retarificación de servicios por {0}", res.Message)
                            Else
                                Me.ShowMessage(EeventViewerImages.Advertencia) = String.Join(vbCrLf, res.MessageResult.ToArray())
                            End If
                            'Me.ShowMessage(EeventViewerImages.Advertencia) = String.Format("No se pudo realizar la retarificación de servicios por {0}", res.Message)
                            LockFoliosToLiquidate(foliosToLiquidate, False)
                    End Select
                End If
            Else
                LockFoliosToLiquidate(foliosToLiquidate, False)
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene un folio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Private Function GetFolioCtrById(id As Integer) As CtrFolio
        Return CType(Me.DocumentParent.Manager.View.Documents _
                            .Where(Function(o) CType(o.Control, CtrFolio)._id = id)? _
                            .FirstOrDefault()?.Control, CtrFolio)
    End Function

    ''' <summary>
    ''' abre el formulario de detalles de ordenes de servicios cuando hay quirurgicos en la retarificacion
    ''' </summary>
    ''' <param name="onlyChangeRates">Indica si solo se esta enviando a retarificar. (false : distribuir)</param>
    ''' <param name="listServiceOrderDetailPopup">The list service order detail.</param>
    Private Async Sub OpenFormServiceOrderDetail(listServiceOrderDetailPopup As List(Of ServiceOrderDetail), objParams As Object, onlyChangeRates As Boolean)
        objParams.OnlyRateChanges = onlyChangeRates
        Dim listServiceOrderDetail As List(Of ServiceOrderDetail) = Nothing
        Using model As New MServiceOrder(Me.Tag)
            listServiceOrderDetail = Await model.GetServiceOrderDetailByOrderServiceIdAsync(listServiceOrderDetailPopup(0).ServiceOrderId)
        End Using
        For Each item In CType(objParams.ProductsAndServices, List(Of Object))
            Dim ItemToliquidateMVIE = listServiceOrderDetailPopup?.Find(Function(x) x.Id = item.ServiceOrderDetailId)
            If ItemToliquidateMVIE IsNot Nothing Then

                ItemToliquidateMVIE.LiquidateAllMIVIE = listServiceOrderDetail?.Find(Function(x) x.Id = item.ServiceOrderDetailId).LiquidateAllMIVIE
            End If
            listServiceOrderDetail.Remove(listServiceOrderDetail.Where(Function(x) x.Id = item.ServiceOrderDetailId).FirstOrDefault())
        Next
        'Consultar los detalles de ordenes de servicio por id serviceorder, y que no se encuentre en objParams.ProductsAndServices
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New FrmPopUpServiceOrderDetailQx
            formulario.ServiceOrderDetailNoQx = listServiceOrderDetailPopup.Where(Function(o) o.Presentation <> 2 OrElse o.SettlementType <> 1).ToList()
            formulario.ParametersLiquidation = objParams
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.FormOwnerName = Me.GetType().Name
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.listServiceOrderDetailPopup = listServiceOrderDetailPopup.Where(Function(o) o.Presentation = 2 AndAlso o.SettlementType = 1).ToList()
            formulario.Admission = AdmissionObject.AdmissionCode
            formulario.Patient = String.Concat(FormOwner.TxtPatientCode.Text.Split("-")(1).Trim(), " - ", FormOwner.TxtPatientName.Text) 'AdmissionObject.PatientCode
            formulario.Stay = AdmissionObject.BedStay
            formulario.PatientDateBirth = _patientBirth
            formulario.PatientGenus = _patientGenus
            formulario.AdmissionDate = CDate(AdmissionObject.AdmissionDate)
            formulario.EditMode = False
            formulario.HealthAdministratorIdtmp = SleThirdParty.EditValue
            AddHandler formulario.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail
            If listServiceOrderDetail IsNot Nothing Then
                formulario.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetail
                ' formulario.ListServiceOrderDetailDatasourceIncludeService = listServiceOrderDetail
            Else
                formulario.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
                'formulario.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
            End If
            Using model As New MServiceOrder(Me.Tag)
                'formulario.ListServiceOrderDetailDatasourceIncludeService.AddRange(model.ListServiceOrderDetailsByAdmissionNumber(AdmissionObject.AdmissionCode))
            End Using
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.Cancel Then
                If CBool(onlyChangeRates) Then
                    If _previusValueCareGroup <> 0 Then
                        Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                    End If
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(objParams.SourceFolioId))
                End If
                LockFoliosToLiquidate({Me.Id, CInt(objParams.SourceFolioId)}.ToList(), False)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Returs the add service order detail.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="AddServiceEventArgs"/> instance containing the event data.</param>
    Private Async Sub ReturAddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
        Dim paramters As Object = CType(sender, FrmPopUpServiceOrderDetailQx).ParametersLiquidation

        Dim resultRecalculate = RecalculateValueQxByEventOrder(e, CType(sender, FrmPopUpServiceOrderDetailQx).ListServiceOrderDetailSurgicalIntervention)
        If Not resultRecalculate Then
            If CBool(paramters.OnlyRateChanges) Then
                If _previusValueCareGroup <> 0 Then
                    Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                End If
                RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(paramters.SourceFolioId))
            End If
        End If

        If CBool(paramters.OnlyRateChanges) Then
            ChangeRateServices(CInt(paramters.NewCareGroupToRetarific), False, Nothing, e.ListServiceOrderDetail, CType(sender, FrmPopUpServiceOrderDetailQx))
        Else
            Using model As New MLiquidation
                Dim res1 As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) = Await model.DistributeFolio(paramters, Nothing, e.ListServiceOrderDetail)
                CType(sender, FrmPopUpServiceOrderDetailQx).AsyncLoader(False)
                If res1.StateResult Then
                    CType(sender, FrmPopUpServiceOrderDetailQx).CanForceClose = True
                    CType(sender, FrmPopUpServiceOrderDetailQx).Close()
                    Me.ShowMessage(EeventViewerImages.Informacion) = "La distribución se realizó correctamente"
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(paramters.SourceFolioId))
                Else
                    If CBool(paramters.OnlyRateChanges) Then
                        If _previusValueCareGroup <> 0 Then
                            Window.Utils.SetValueToProperty(Me.SleCareGroup, "EditValue", _previusValueCareGroup)
                        End If
                        RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(paramters.SourceFolioId))
                    End If
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res1.Message
                End If
            End Using
            LockFoliosToLiquidate({Me.Id, CInt(paramters.SourceFolioId)}.ToList(), False)
        End If
    End Sub

    Private Function RecalculateValueQxByEventOrder(e As AddServiceEventArgs, currentServiceOrderDetail As List(Of ServiceOrderDetail)) As Boolean
        Dim listServiceOrderDetail = currentServiceOrderDetail
        If listServiceOrderDetail Is Nothing Then
            listServiceOrderDetail = New List(Of ServiceOrderDetail)
        End If
        Dim errorsEvent As New StringBuilder
        'recalculamos para saber que item va coomo primer evento y asi aplicar o no los porcentajes
        For Each item In e.ListServiceOrderDetail
            If item.Presentation = 2 Then

                If item.SurgicalInterventionType <> 1 Then
                    Dim detailFirstEvent = listServiceOrderDetail.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.IsFirstEvent = True)
                    If detailFirstEvent IsNot Nothing Then
                        If item.SubTotalSalesPrice > detailFirstEvent.SubTotalSalesPrice Then

                            'valido que los items no esten bloqueados o facturados en los folios  
                            Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.Id > 0)
                            For Each itemEvent In listEventsTmp
                                If itemEvent.IsPackage Then
                                    errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el servicio " + itemEvent.CodeNameIpsService + " esta empaquetado")
                                    Exit For
                                End If
                                Using model As New MServiceOrder(Me.Tag)
                                    'valido que los items no esten distribuidos
                                    Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                                    If listDistribution.Count > 1 Then
                                        errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el servicio " + itemEvent.CodeNameIpsService + " esta distribuido")
                                        Exit For
                                    End If
                                    'valido que los folios no esten bloqueado o facturados
                                    Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                                    If revenueControlDetail.Status > 1 Then
                                        If revenueControlDetail.Status = 2 Then
                                            errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta facturado")
                                        ElseIf revenueControlDetail.Status = 5 Then
                                            errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta en estado de Reconocimiento de Ingresos")
                                        Else
                                            errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado")
                                        End If
                                        Exit For
                                    End If
                                End Using
                            Next

                            If errorsEvent.Length > 0 Then
                                Continue For
                            End If
                            item.IsFirstEvent = True
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item)
                            End If
                            detailFirstEvent.IsFirstEvent = False
                            If detailFirstEvent.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(detailFirstEvent)
                            End If
                        Else
                            item.IsFirstEvent = False
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item)
                            End If
                        End If
                    Else
                        item.IsFirstEvent = True
                        If item.SurgicalInterventionType < 9 Then
                            GetValueSurgicalEvenst(item)
                        End If
                    End If
                End If
            End If
            listServiceOrderDetail.Add(item)
        Next

        ValidateMIVIE(listServiceOrderDetail)
        If errorsEvent.Length > 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = errorsEvent.ToString()
            Return False
        End If
        Return True
    End Function

    Private Sub GetValueSurgicalEvenst(serviceOrdeDetailItem As ServiceOrderDetail)
        Using model As New MServiceOrder(Me.Tag)
            Dim SurgeriesPercentageManual = model.GetSurgeriesPercetageManualByRateManualIdInterventionType(serviceOrdeDetailItem.RateManualId, serviceOrdeDetailItem.SurgicalInterventionType)
            If (serviceOrdeDetailItem.IsFirstEvent = True AndAlso SurgeriesPercentageManual.MainHundredPercent = False) OrElse (serviceOrdeDetailItem.IsFirstEvent = False) Then
                For Each item In serviceOrdeDetailItem.ServiceOrderDetailSurgical

                    Select Case item.ClassServiceIps?.ToUpper()
                        Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.SurgeonPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AssistantPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.RoomPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                            item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.MaterialsPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                    End Select

                Next
            End If

            With serviceOrdeDetailItem
                '/**************--Segmento Impuestos--**********************/
                'se suman el detalle de los qx
                .SubTotalSalesPrice = .ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                .GrossValue = .SubTotalSalesPrice
                .TaxValue = 0
                'independientmente si viene de detalles qx o no al final en base a la parametrizacion del servicio y el sistema se establece el valor bruto, el iva y el subtotal
                Dim _dictionaryValues = Utils.SetValueSalesPrice(Me.FormOwner.FlagTaxInclude,
                                                                  .SubTotalSalesPrice,
                                                                  ?.TaxPercent)
                If?.TaxedService AndAlso _dictionaryValues?.Any() Then
                    .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                    .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                    .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                    If .SubTotalSalesPrice > .RoundService Then
                        .SubTotalSalesPrice = Utils.RoundValue(.SubTotalSalesPrice, .RoundService)
                    End If
                End If
                '/***************************************************************/
                .TotalSalesPrice = .SubTotalSalesPrice - .ThirdPartyDiscount
                .GrandTotalSalesPrice = .TotalSalesPrice * .InvoicedQuantity
            End With
        End Using
    End Sub

    ''' <summary>
    ''' metodo para validar que los items que son MIVIE y sean mas de 2, los dos primeros se liquiden como dice el manual y los demas no se cobren
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateMIVIE(listServiceOrderDetail As List(Of ServiceOrderDetail))
        Dim listEvents = (From e In listServiceOrderDetail Where e.SettlementType = 1 Select e.SurgeryNumber).Distinct().ToList()
        For item As Integer = 0 To listEvents.Count - 1 Step 1
            Dim firstEvent = listServiceOrderDetail.Find(Function(x) x.IsFirstEvent = True And x.SurgeryNumber = listEvents(item))
            Dim index = 2
            Dim listMIVIE = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = listEvents(item) _
                                                               AndAlso x.RateManualType < 3 _
                                                               AndAlso x.SurgicalInterventionType IsNot Nothing _
                                                               AndAlso x.SurgicalInterventionType = 3 _
                                                               AndAlso (x.LiquidateAllMIVIE Is Nothing OrElse Not x.LiquidateAllMIVIE))
            If listMIVIE.Count > 2 Then
                listMIVIE = (From l In listMIVIE Order By l.RateManualSalePrice Descending).ToList()
                If listMIVIE.Exists(Function(x) x.IsFirstEvent = True) Then
                    listMIVIE.Remove(firstEvent)
                    index = 1
                End If
                For i As Integer = index To listMIVIE.Count - 1 Step 1
                    listMIVIE.ElementAt(i).SubTotalSalesPrice = 0
                    listMIVIE.ElementAt(i).TotalSalesPrice = 0
                    listMIVIE.ElementAt(i).GrandTotalSalesPrice = 0
                    For Each itemSurgical In listMIVIE.ElementAt(i).ServiceOrderDetailSurgical
                        itemSurgical.TotalSalesPrice = 0
                    Next
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Liquidates the recovery fee.
    ''' </summary>
    ''' <param name="args">The arguments.</param>
    Public Async Sub LiquidateRecoveryFee(args As Object)
        'Validar que hayan items en el folio
        If Me.GdcServices.DataSource Is Nothing Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("FolioEmpty", Me.GetType())
            Exit Sub
        End If

        Me.IsAsyncOperation()
        Using model As New MLiquidation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.LiquidateRecoveryFee, args)
            Me.IsAsyncOperation(False)
            If res IsNot Nothing AndAlso res.StateResult Then
                'RaiseEvent BeginReloadDatasource(Me, New BeginReloadDatasourceEventArgs(Me))
                'Await Me.SetDatasourceAsync(Me._id, Me.Status)
                Me.ShowMessage(EeventViewerImages.Informacion) = "Se liquidó correctamente la cuota a paciente"
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            End If
            Me.SetDatasourceAsync(Me._id, Me.Status)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el resultado de saber si hay items distribuidos
    ''' </summary>
    Private ReadOnly Property DistributeItems As Boolean
        Get
            If CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)) IsNot Nothing Then
                Return CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).
                Where(Function(o) o.DistributionType <> 1).ToList().Count > 0
            Else
                Return False
            End If
        End Get
    End Property



    ''' <summary>
    ''' Obtiene el listado de idRevenuecontrolDetail(Folio) y los números del folio de los items que están distribuidos
    ''' </summary>
    ''' <returns>
    ''' Listado de los id de los items distribuidos
    ''' </returns>
    Public Function GetListFolioToLiquidate(ServiceOrderDetailIdsDistributed As List(Of Integer), Optional previusFolioId As Integer = 0) As List(Of CtrFolio)
        Dim ListFolios As New List(Of CtrFolio)() 'Contiene los folios donde se encuentran distribuidos los items y el número del folio
        For Each serviceOrderDetailId As Integer In ServiceOrderDetailIdsDistributed
            For Each item In Me.DocumentParent.Manager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id <> Me._id AndAlso CType(d.Control, CtrFolio).Id <> previusFolioId).ToList()
                Dim Foliox As CtrFolio = CType(item.Control, CtrFolio)
                Dim ItemDistribute = _folio.Details.Where(Function(x) x.ServiceOrderDetailId = serviceOrderDetailId).ToList()
                If ItemDistribute.Count > 0 Then
                    Dim distributeItems As List(Of Integer) = _folio.Details.
                                                                Where(Function(o) o.DistributionType <> 1).ToList().Where(Function(o) o.ServiceOrderDetailId <> serviceOrderDetailId).ToList().
                                                                Select(Function(x) x.ServiceOrderDetailId).ToList()
                    Dim ListRevenueControlDetailIdInOtherFolio = Foliox.GetListFolioToLiquidate(distributeItems, Me._id)
                    If ListRevenueControlDetailIdInOtherFolio IsNot Nothing AndAlso ListRevenueControlDetailIdInOtherFolio.Count > 0 Then
                        ListFolios.AddRange(ListRevenueControlDetailIdInOtherFolio)
                    End If
                    ListFolios.Add(Foliox)
                    Exit For
                End If
            Next
        Next
        Return ListFolios.Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de idRevenuecontrolDetail(Folio) y los números del folio de los items que están distribuidos
    ''' </summary>
    ''' <returns>
    ''' Listado de los id de los items distribuidos
    ''' </returns>
    Public Function GetListFolioToLiquidate(codeAssociateServices As List(Of String), Optional previusFolioId As Integer = 0) As List(Of CtrFolio)
        Dim ListFolios As New List(Of CtrFolio)() 'Contiene los folios donde se encuentran distribuidos los items y el número del folio
        For Each codeAssociate As String In codeAssociateServices
            For Each item In Me.DocumentParent.Manager.View.Documents.Where(Function(d) CType(d.Control, CtrFolio).Id <> Me._id AndAlso CType(d.Control, CtrFolio).Id <> previusFolioId).ToList()
                Dim Foliox As CtrFolio = CType(item.Control, CtrFolio)
                Dim ItemDistribute = _folio.Details.Where(Function(x) x.CodeAssociateService = codeAssociate).ToList()
                If ItemDistribute.Count > 0 Then
                    Dim distributeNoPOS As List(Of String) = _folio.Details.
                                                                Where(Function(o) o.DistributionType <> 1).ToList().Where(Function(o) o.CodeAssociateService <> codeAssociate).ToList().
                                                                Select(Function(x) x.CodeAssociateService).ToList()
                    Dim ListRevenueControlDetailIdInOtherFolio = Foliox.GetListFolioToLiquidate(distributeNoPOS, Me._id)
                    If ListRevenueControlDetailIdInOtherFolio IsNot Nothing AndAlso ListRevenueControlDetailIdInOtherFolio.Count > 0 Then
                        ListFolios.AddRange(ListRevenueControlDetailIdInOtherFolio)
                    End If
                    ListFolios.Add(Foliox)
                    Exit For
                End If
            Next
        Next
        Return ListFolios.Distinct().ToList()
    End Function

    ''' <summary>
    ''' metodo para ocultar o mostrar ciertas columnas dependiendo si en el parametro de facturacion tienen liquida cuenta madre
    ''' </summary>
    ''' <param name="LiquidateMasterA"></param>
    Private Sub ShowOrHideGridColumns(LiquidateMasterA As Boolean?)
        If LiquidateMasterA Is Nothing Then
            Exit Sub
        End If

        Me.INDColSubTotal.Visible = LiquidateMasterA
        Me.INDColSubTotalL.Visible = LiquidateMasterA
        Me.INDColDiscount.Visible = LiquidateMasterA
        Me.INDColDiscountL.Visible = LiquidateMasterA
        Me.INDColNetoValue.Visible = LiquidateMasterA
        Me.INDColNetoValueL.Visible = LiquidateMasterA
        Me.INDColPatientSalesPrice.Visible = LiquidateMasterA
        Me.ColRecoveryFeeType.Visible = Not LiquidateMasterA
        Me.ColApplyRecoveryFee.Visible = Not LiquidateMasterA
        Me.ColThirdPartySalesPrice.Visible = Not LiquidateMasterA
        Me.ColSurchargeApply.Visible = Not LiquidateMasterA
        Me.ColApplyRecoveryFee.Visible = Not LiquidateMasterA
        Me.INFClIdMipres.Visible = Not LiquidateMasterA
        Me.ColThirdPartyDiscount.Visible = Not LiquidateMasterA

        Me.INDLciSubTotal.HideControl(Not LiquidateMasterA)
        Me.LyciTotalEntity.HideControl(LiquidateMasterA)
        Me.LyciTotalPatient.HideControl(LiquidateMasterA)
        Me.ColSubTotalPatientSalesPrice.Caption = If(LiquidateMasterA, "Copago", "V. Cuota Paciente")

        If LiquidateMasterA Then
            Me.INDColSubTotal.VisibleIndex = Me.INDColPercentIva.VisibleIndex + 1
            Me.INDColSubTotalL.VisibleIndex = Me.INDColPercentIvaL.VisibleIndex + 1

            Me.INDColDiscount.VisibleIndex = Me.INDColSubTotal.VisibleIndex + 1
            Me.INDColDiscountL.VisibleIndex = Me.INDColSubTotalL.VisibleIndex + 1

            Me.INDColNetoValue.VisibleIndex = Me.INDColDiscount.VisibleIndex + 1
            Me.INDColNetoValueL.VisibleIndex = Me.INDColDiscountL.VisibleIndex + 1

            Me.INDColTaxValue.VisibleIndex = Me.INDColNetoValue.VisibleIndex + 1
            Me.INDColTaxesL.VisibleIndex = Me.INDColNetoValueL.VisibleIndex + 1

            Me.ColSmallGrandTotalSalesPrice.VisibleIndex = Me.INDColTaxValue.VisibleIndex + 1
            Me.ColLargeGrandTotalSalesPrice.VisibleIndex = Me.INDColTaxesL.VisibleIndex + 1

            Me.INDColPatientSalesPrice.VisibleIndex = Me.ColSmallGrandTotalSalesPrice.VisibleIndex + 1
            Me.ColSubTotalPatientSalesPrice.VisibleIndex = Me.ColLargeGrandTotalSalesPrice.VisibleIndex + 1
        End If
    End Sub

    ''' <summary>
    ''' metodo que genera la pre factura dependiendo de la moneda seleccionada y fecha de TRM
    ''' </summary>
    Private Async Function PrintInvoicePartialbyMasterAccount(revenueControlDetailId As Integer, admissionNumber As String,
                                                                Optional dateTRM As DateTime? = Nothing) As Task
        Using formulario As New FrmPopUpMasterAccount

            If dateTRM Is Nothing Then
                Using model As New MCtrFolio()
                    formulario.SuggestedDateTRM = Await model.GetServerDate()
                End Using
            Else
                formulario.SuggestedDateTRM = dateTRM
            End If
            formulario.INDLciExportExcel.HideControl()
            formulario.Text = "Pre-Factura"
            Dim tr As New FrmTransparent(formulario, False)
            Dim response = tr.ShowDialog(Me)
            If response = System.Windows.Forms.DialogResult.OK Then
                '' para mostrar el reporte
                If Not waitForm.IsSplashFormVisible Then
                    waitForm.ShowWaitForm()
                End If
                Dim reportDefPartial = New Reporter.rptInvoicePartialMotherAccount()
                reportDefPartial.ParametrosReporte = New Object() {revenueControlDetailId, admissionNumber, formulario.CurrencyId, formulario.DateTRM}
                AddHandler reportDefPartial.AfterPrint, Sub()
                                                            If waitForm.IsSplashFormVisible Then
                                                                waitForm.CloseWaitForm()
                                                            End If
                                                        End Sub
                ReportHelper.ExecuteReport(reportDefPartial, Me.FormOwner, Me.FormOwner.PermissionsForm)
                Return
            End If
        End Using
    End Function

    ''' <summary>
    ''' Funcion que valida si se debe mostrar o no el boton "Abrir folio"
    ''' </summary>
    ''' <param name="folio"></param>
    ''' <returns></returns>
    Private Function OpenFolioValidityViewButton(folio As IFolio) As Boolean
        Return {eMasterAccount.PatientAccount, eMasterAccount.EntityAccount}.Contains(Me._folio.IsMasterAccount) _
                AndAlso (Me._folio?.Details Is Nothing OrElse Me._folio.Details.Sum(Function(s) s.GrandTotalSalesPrice) = 0) _
                AndAlso Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.AbrirFolio))
    End Function
#End Region

#Region "New"

    ''' <summary>
    ''' Valida que el item a modificar no sea un producto
    ''' </summary>
    ''' <param name="objView">The object view.</param>
    ''' <param name="view">The view.</param>
    ''' <returns></returns>
    Private Function ValidateItemProductModified(objView As ViewListServiceOrderDetailXpo, view As GridView) As Boolean
        If objView.RecordType = eRecordType.Drugs Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageDontUnitValueProductModified", Me.GetType())
            CType(view.GetFocusedRow(), IFolioDetail).TotalSalesPrice = _previewValueUnitValue
            GdcServices.RefreshDataSource()
            _isUpdatingUnitValue = False
            _previewValueUnitValue = -1
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Confirma si se desea modificar el valor unitario porque se pierden los datos del cobro de la cuota de recuperación
    ''' </summary>
    ''' <param name="objView">The object view.</param>
    ''' <param name="view">The view.</param>
    ''' <returns></returns>
    Private Function ConfirmModifiedUnitValueWithPatientValue(objView As ViewListServiceOrderDetailXpo, view As GridView)
        If objView.SubTotalPatientSalesPrice <> 0 AndAlso objView.PatientPercentage <> 0 AndAlso Not MessageIndigo.Show(ResourceManager.GetString("MessageUnitValueWithPatientValue", Me.GetType()), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(view.GetFocusedRow(), IFolioDetail).TotalSalesPrice = _previewValueUnitValue
            GdcServices.RefreshDataSource()
            _isUpdatingUnitValue = False
            _previewValueUnitValue = -1
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Actualiza el valor unitario en la base de datos
    ''' </summary>
    Private Async Sub UpdateUnitValue(newValue As Decimal)
        Dim view As GridView = If(GdcServices.MainView.Name.Equals(GdvSmallServices.Name), GdvSmallServices, GdvLargeServices)
        Dim objView As IFolioDetail = CType(view.GetFocusedRow(), IFolioDetail)
        If _previewValueUnitValue <> -1 AndAlso newValue <> _previewValueUnitValue Then
            'Se comenta este codigo ya que en pitalito permitimos cambiar los valores de los productos para resolver el problema de los No POS
            'If Not ValidateItemProductModified(objView, view) OrElse Not ConfirmModifiedUnitValueWithPatientValue(objView, view) Then
            '    Exit Sub
            'End If
            If objView.DistributionType <> 1 Then ' Indica que el item se encuentra distribuido
                Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageDontUnitValueModified", Me.GetType())
                CType(view.GetFocusedRow(), IFolioDetail).TotalSalesPrice = _previewValueUnitValue
                GdcServices.RefreshDataSource()
            Else
                Me.IsAsyncOperation(True)
                Dim objParams As Object = New ExpandoObject()
                objParams.ServiceOrderDetailDistributionId = objView.Id
                objParams.TotalSalesPrice = newValue
                Using model As New MLiquidation()
                    Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.UpdateUnitValue, objParams)
                    If res IsNot Nothing Then
                        If res.StateResult Then
                            Me.IsAsyncOperation(False)
                            Me.ReloadDatasource()
                        Else
                            Me.IsAsyncOperation(False)
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                            CType(view.GetFocusedRow(), IFolioDetail).TotalSalesPrice = _previewValueUnitValue
                        End If
                    Else
                        Me.IsAsyncOperation(False)
                        If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                            Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        Else
                            Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                        End If
                    End If
                End Using
            End If
        End If
        _isUpdatingUnitValue = False
        _previewValueUnitValue = -1
    End Sub

    ''' <summary>
    ''' Actualiza el valor unitario en la base de datos
    ''' </summary>
    Private Async Sub UpdateAuthorizationNumberValue(authorizationNumber As String)
        Dim view As GridView = IIf(GdcServices.MainView.Name = GdvSmallServices.Name, GdvSmallServices, GdvLargeServices)
        'Dim objView As ViewListServiceOrderDetailXpo = CType(view.GetFocusedRow(), ViewListServiceOrderDetailXpo)
        If Not String.IsNullOrEmpty(_previewValueAuthorizationNumber) AndAlso authorizationNumber <> _previewValueAuthorizationNumber Then
            Me.IsAsyncOperation(True)
            Dim objParams As Object = New ExpandoObject()

            Dim serviceOrderDetailList As New ConcurrentBag(Of Object)()
            Parallel.ForEach(view.GetSelectedRows().Where(Function(o) o > -1).ToList(), Sub(i)
                                                                                            Dim obj As IFolioDetail = CType(view.GetRow(i), IFolioDetail)
                                                                                            serviceOrderDetailList.Add(obj.ServiceOrderDetailId)
                                                                                        End Sub)
            objParams.serviceOrderDetailList = serviceOrderDetailList
            objParams.AuthorizationNumber = authorizationNumber

            Using model As New MLiquidation()
                Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.UpdateAuthorizationNumber, objParams)
                If res IsNot Nothing Then
                    If res.StateResult Then
                        Me.IsAsyncOperation(False)
                        Me.ReloadDatasource()
                    Else
                        Me.IsAsyncOperation(False)
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                Else
                    Me.IsAsyncOperation(False)
                    If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                        Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    Else
                        Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                    End If
                End If
            End Using
        End If
        _isUpdatingAuthorizationNumber = False
        _previewValueAuthorizationNumber = String.Empty
        'Dim view As GridView = GdvLargeServices
        'Dim objView As ViewListServiceOrderDetailXpo = CType(view.GetFocusedRow(), ViewListServiceOrderDetailXpo)
        'If Not String.IsNullOrEmpty(_previewValueAuthorizationNumber) AndAlso objView.AuthorizationNumber <> _previewValueAuthorizationNumber Then
        '    Me.IsAsyncOperation(True)
        '    Dim objParams As Object = New ExpandoObject()
        '    objParams.ServiceOrderDetailId = objView.ServiceOrderDetailId
        '    objParams.AuthorizationNumber = objView.AuthorizationNumber
        '    Using model As New MLiquidation()
        '        Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.UpdateAuthorizationNumber, objParams)
        '        If res IsNot Nothing Then
        '            If res.StateResult Then
        '                Me.IsAsyncOperation(False)
        '                Me.ReloadDatasource()
        '            Else
        '                Me.IsAsyncOperation(False)
        '                Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
        '            End If
        '        Else
        '            Me.IsAsyncOperation(False)
        '            If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
        '                Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            Else
        '                Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
        '            End If
        '        End If
        '    End Using
        'End If
        '_isUpdatingAuthorizationNumber = False
        '_previewValueAuthorizationNumber = String.Empty
    End Sub

    ''' <summary>
    ''' Valida que al mover un item a otro folio si tiene un grupo de atención distinto al folio origen entonces que no se encuentre distribuído (Esta validación ya no se toma)
    ''' </summary>
    ''' <param name="dragObj">Objeto a mover al nuevo folio</param>
    ''' <returns></returns>
    Private Function ValidateItemCareGroup(dragObj As Object) As Boolean
        'If CType(dragObj.ProductsAndServices, List(Of Object)).Any(Function(o) o.SourceDistribType = 2) _
        '    AndAlso dragObj.CareGroupId <> If(Me.SleCareGroup.EditValue Is Nothing, Me._dataHeader.CareGroupId, Me.SleCareGroup.EditValue) Then 'si se cumple entonces es por que este item ya está distribuido y el caregroup del folio que viene es distinto al actual
        '    Me.ShowMessage(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ItemDontChange", Me.GetType()), Me._dataHeader.FolioOrder) '"No se Puede Distribuir este item debido a que ya está distribuido y el grupo de atencion del folio es diferente al origen"
        '    'RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(dragObj.SourceFolioId))
        '    Return False
        'End If
        Return True
    End Function

    ''' <summary>
    ''' Validates the item momologation.
    ''' </summary>
    ''' <param name="dragObj">The drag object.</param>
    ''' <returns></returns>
    Private Async Function ValidateItemMomologation(dragObj As Object) As Task(Of Boolean)
        Using model As New MLiquidation()
            Dim args As Object = New ExpandoObject()
            args.RevenueControlDetail = Me._id
            args.CaregroupId = Me.CareGroupId
            args.ProductsAndServices = dragObj.ProductsAndServices
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.ValidateItemDistribution, args)
            If Not res.StateResult Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
            Return res.StateResult
        End Using
    End Function

    ''' <summary>
    ''' Liquida el folio actual
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnLiquidateFolio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnLiquidateFolio.ItemClick
        LiquidateFolio()
    End Sub

    ''' <summary>
    ''' Liquida el folio actual
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnAssociateInvoice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnAssociateInvoice.ItemClick
        AssociateInvoice()
    End Sub

    Private Sub OpenFormAuthorizationNumber()
        Using form As New FrmAuthorizationNumber
            Dim transp As New FrmTransparent(form, False)
            If transp.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                _previewValueAuthorizationNumber = form.AuthorizationNumber & "0"
                UpdateAuthorizationNumberValue(form.AuthorizationNumber)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Valida que si existen items distribuidos, solicita los números de los folios donde esta la distribución
    ''' <para>liquidate : indica si se va a facturar o si se va a anular</para>
    ''' </summary>
    Private Async Function ValidateLiquidateFolios(Optional ByVal liquidate As Boolean = True) As Task(Of ActionResult(Of List(Of CtrFolio)))
        Dim result As New ActionResult(Of List(Of CtrFolio))()
        Dim errorList As New StringBuilder()
        result.StateResult = True

        If liquidate Then
            'valido el folio paciente de cuenta madre
            If _folio.IsMasterAccount = 4 Then
                Using Model As New MCtrFolio
                    Me.IsAsyncOperation(True)
                    Dim resultValidation = Await Model.ValidateLiquidateFolioAsync(_folio?.RevenueControlDetailId)
                    Me.IsAsyncOperation(False)
                    If resultValidation Is Nothing OrElse Not resultValidation.StateResult Then
                        errorList.AppendLine($" {resultValidation?.Message}")
                    End If
                End Using
            End If
            If FormOwner.IdBillingAuthorizationSelected = 0 Then
                errorList.AppendLine("Seleccione una Autorización")
            End If
            If FormOwner.IdOperatingUnitSelected = 0 Then
                errorList.AppendLine("Seleccione una Unidad Operativa")
            End If
            If SleCategories.EditValue Is Nothing Then
                errorList.AppendLine("Debe seleccionar una categoría para la factura a generar")
            End If
            If _folio.ThirdPartyId <> _folio.ThirdPartyHealthAdministrator AndAlso _folio.FolioType <> 3 AndAlso Me._folio.IsMasterAccount <> 4 Then
                errorList.AppendLine("No se puede liquidar el folio por que el tercero del folio no es igual al de la Entidad Administradora")
            End If

            If errorList.Length > 0 Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
                result.StateResult = False
                Return result
            End If
        End If

        'Verificar en que otros folios estan distribuitos los items distribuidos
        If Me.GdcServices.DataSource Is Nothing Then
            result.StateResult = False
            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("FolioEmpty", Me.GetType())
            Return result
        End If

        'Obtengo los ids de los items que se encuentran distribuidos en otros folios
        Dim serviceOrderDetailIdsDistributed As List(Of Integer) = CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).
            Where(Function(o) o.DistributionType <> 1 And o.CodeAssociateService <> Nothing).ToList().Select(Function(x) x.ServiceOrderDetailId).ToList()

        Dim serviceOrderDetailIdsDistributedNoPOS As List(Of String) = CType(Me.GdcServices.DataSource, IEnumerable(Of IFolioDetail)).
            Where(Function(o) o.DistributionType <> 1 And o.CodeAssociateService <> Nothing).ToList().Select(Function(x) x.CodeAssociateService).ToList()

        'Obtengo los ids de los Items que se encuentran distribuidos para encontrar los folios que contienen la distribución
        'Obtengo la lista de ServiceOrderDetailId ya que al hacer la distribución se crea otro registro con el mismo ServiceOrderDetailId y este contiene el id del folio
        If serviceOrderDetailIdsDistributed IsNot Nothing AndAlso serviceOrderDetailIdsDistributed.Count > 0 Then
            Dim FoliosToLiquidate As List(Of CtrFolio) = GetListFolioToLiquidate(serviceOrderDetailIdsDistributed)
            Dim FoliosToLiquidateNoPOS As List(Of CtrFolio) = GetListFolioToLiquidate(serviceOrderDetailIdsDistributedNoPOS)
            FoliosToLiquidate.AddRange(FoliosToLiquidateNoPOS)
            Dim stringData As String = String.Join("-", FoliosToLiquidate.Distinct().Select(Function(o) o.FolioOrder).ToList().ToArray())
            Dim resultDialog As System.Windows.Forms.DialogResult
            If liquidate Then
                resultDialog = MessageIndigo.Show(String.Format(ResourceManager.GetString("LiquidateFoliosNecesary", Me.GetType()), stringData), MessageType.Question, Me.Text, Botones.SiNo)
            Else
                resultDialog = MessageIndigo.Show(String.Format(ResourceManager.GetString("AnulateFoliosNecesary", Me.GetType()), stringData), MessageType.Question, Me.Text, Botones.SiNo)
            End If
            If resultDialog = System.Windows.Forms.DialogResult.Yes Then
                result.ObjectEmbbeded = FoliosToLiquidate
                Return result
            Else
                result.StateResult = False
                Return result
            End If
        Else
            Return result
        End If
    End Function

    ''' <summary>
    ''' Método para validar que si el grupo de atención asociado al folio es de tipo 13. Aseguradoras, valide el número de la autorización
    ''' </summary>
    ''' <param name="listFoliosToLiquidate"></param>
    ''' <returns></returns>
    Public Function ValidatePolicy(listFoliosToLiquidate As List(Of CtrFolio)) As Boolean
        If listFoliosToLiquidate.Exists(Function(x) {13}.Contains(x.CareGroupEntityType)) Then
            If String.IsNullOrEmpty(listFoliosToLiquidate(0).AuthorizationNumber) Then
                Return False
            End If
        End If
        Return True
    End Function
    ''' <summary>
    ''' Evento que se ejecuta para anular una factura
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Sub MbtnCancelInvoice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnCancelInvoice.ItemClick
        AnulateFolio()
    End Sub

    Public Async Sub AnulateFolio(Optional listFoliosToLiquidate As List(Of CtrFolio) = Nothing)
        If listFoliosToLiquidate Is Nothing Then
            Dim resultValidate As ActionResult(Of List(Of CtrFolio)) = Await ValidateLiquidateFolios(False)
            If Not resultValidate.StateResult Then
                Exit Sub
            End If
            If resultValidate.ObjectEmbbeded Is Nothing Then
                Dim response = MessageIndigo.Show(ResourceManager.GetString("MessageAnnullate", Me.GetType()), MessageType.Question, Me.Text, Botones.SiNo)
                If response <> System.Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
                resultValidate.ObjectEmbbeded = New List(Of CtrFolio)()
            End If
            resultValidate.ObjectEmbbeded.Add(Me)
            listFoliosToLiquidate = resultValidate.ObjectEmbbeded
        End If


        Dim reversalReasonId As Integer = 0
        Dim reversalDescription As String = String.Empty
        Using PopUpAnnulmentReason As New PopUpAnnulmentReason()
            Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
            If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                Exit Sub
            Else
                reversalReasonId = PopUpAnnulmentReason.ReversalReasonId
                reversalDescription = PopUpAnnulmentReason.ReversalDescription
            End If
        End Using


        LockFoliosToLiquidate(listFoliosToLiquidate.Select(Function(o) o.Id).Distinct().ToList(), True)
        Dim args As Object = New ExpandoObject()
        args.audit = SessionValues.Instance.AuditMessageWcf
        args.OperativeUnitId = FormOwner.IdOperatingUnitSelected
        args.RevenueControlDetailIdsToLiquidate = listFoliosToLiquidate.Select(Function(o) o.Id).Distinct().ToList()
        args.ReversalReasonId = reversalReasonId
        args.ReversalDescription = reversalDescription
        args.ContainerHis = SessionValues.Instance.HisContainer
        args.PatientCode = _folio.PatientCode
        Using model As New MLiquidation()
            Dim res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.AnulateFolio, args)
            LockFoliosToLiquidate(listFoliosToLiquidate.Select(Function(o) o.Id).Distinct().ToList(), False)
            If res IsNot Nothing AndAlso res.StateResult Then
                'Mensaje de Liquidado con consecutivo res.ObjectEmbbeded
                Me.ShowMessage(EeventViewerImages.Informacion) = String.Join(vbCrLf, res.MessageResult)
                RaiseEvent RequiereReloadFolioList(Me, New RequiereReloadFolioListEventArgs(listFoliosToLiquidate.Select(Function(o) o.Id).Distinct().ToList()))
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            End If
        End Using
    End Sub


    ''' <summary>
    ''' Método que liquida el folio actual
    ''' </summary>
    Public Async Sub LiquidateFolio(Optional listFoliosToLiquidate As IEnumerable(Of CtrFolio) = Nothing)
        RaiseEvent GetStatusLoadStays(Me)
        If _loadingStays Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = "Se estan cargando las estancias, por favor espere..."
            Exit Sub
        End If

        Me.IsAsyncOperation()

        If listFoliosToLiquidate Is Nothing Then
            Dim resultValidate As ActionResult(Of List(Of CtrFolio)) = Await ValidateLiquidateFolios()
            If Not resultValidate.StateResult Then
                Me.IsAsyncOperation(False)
                Exit Sub
            End If
            'agrego el folio actual
            If resultValidate.ObjectEmbbeded Is Nothing Then
                resultValidate.ObjectEmbbeded = New List(Of CtrFolio)()
            End If
            resultValidate.ObjectEmbbeded.Add(Me)
            listFoliosToLiquidate = resultValidate.ObjectEmbbeded.Distinct().ToList
        Else
            'Revisar cuales de los folios no tienen asignada una categoría
        End If
        If Not ValidatePolicy(listFoliosToLiquidate) Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = "Falta número de autorización de aseguradora"
            Me.IsAsyncOperation(False)
            Exit Sub
        End If
        'Consulto si hay dispensaciones o devoluciones sin confirmar
        Using model As New MLiquidation
            Dim resultPharmaceutical As ActionResult = Await model.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(_folio.AdmissionNumber)
            If resultPharmaceutical IsNot Nothing AndAlso resultPharmaceutical.StatusCode <> eStatusResult.SUCCESS Then
                If resultPharmaceutical.StatusCode = eStatusResult.EXCEPTION Then
                    ShowMessage(eStatusResult.EXCEPTION) = resultPharmaceutical.Message
                    Me.IsAsyncOperation(False)
                    Exit Sub
                End If
                Using form As New FrmDispensingConfirmation
                    form.MessagePharmaceuticalDispensing = resultPharmaceutical.MessageResult(0)
                    form.MessageDevolutionDispensing = resultPharmaceutical.MessageResult(1)
                    Dim transparent As New FrmTransparent(form, False)
                    If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                        Me.IsAsyncOperation(False)
                        Exit Sub
                    End If
                End Using
            End If
        End Using

        ' Validar mayoría de edad antes de continuar con la liquidación
        Dim ageValidationPassed = Await ValidateAgeOfMajorityBeforeLiquidation(listFoliosToLiquidate)
        If Not ageValidationPassed Then
            Me.IsAsyncOperation(False)
            Exit Sub
        End If

        ListPortfolioAdvaceCrossing = New List(Of PortfolioAdvance)()
        Using formAdvance As New FrmAdvanceCrossing
            formAdvance.IdOperatingUnitSelected = FormOwner.IdOperatingUnitSelected
            formAdvance.IdBillingAuthorizationSelected = FormOwner.IdBillingAuthorizationSelected
            formAdvance.ThirdPartyPatientId = Me.OriginalThirdPartyPatientId
            formAdvance.AdmissionType = AdmissionType
            formAdvance.EgressChange = _egressChange
            formAdvance.FolioQuantity = Me.AdmissionObject.FolioQuantity
            formAdvance.AdmissionNumber = _folio.AdmissionNumber
            formAdvance.PatientIdentification = If(Me.AdmissionObject.PatientCode IsNot Nothing, Me.AdmissionObject.PatientCode.ToString().Trim(), String.Empty)
            formAdvance.PatientName = If(Me.AdmissionObject.PatientName IsNot Nothing, Me.AdmissionObject.PatientName.ToString().Trim(), String.Empty)
            formAdvance.SettingLiquidateMasterAccount = Me.SettingBilling?.LiquidateMasterAccount
            formAdvance.SettingBilling = Me.SettingBilling
            formAdvance.LstFoliosToLiquidate = listFoliosToLiquidate.Select(Of ICtrFolio)(Function(m) m).ToList()
            formAdvance.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9

            AddHandler formAdvance.RunLiquidateFolio, AddressOf RunLiquidateFolio
            Dim transparent As New FrmTransparent(formAdvance, False)
            transparent.ShowDialog(Me)
            Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Valida la mayoría de edad de los terceros pacientes antes de continuar con la liquidación
    ''' </summary>
    ''' <param name="listFoliosToLiquidate">Lista de folios a liquidar</param>
    ''' <returns>True si la validación pasó o no aplica, False si se debe cancelar la liquidación</returns>
    Private Async Function ValidateAgeOfMajorityBeforeLiquidation(listFoliosToLiquidate As IEnumerable(Of CtrFolio)) As Task(Of Boolean)
        Try
            Using model As New MLiquidation()
                ' Verificar si la validación está habilitada
                If Not model.IsAgeValidationEnabled(FormOwner.IdOperatingUnitSelected) Then
                    Return True
                End If

                ' Obtener los terceros únicos de los folios a liquidar
                Dim foliosToValidate = listFoliosToLiquidate.GroupBy(Function(f) f.ThirdPartyIdForAgeValidation).Select(Function(g) g.First()).ToList()

                For Each folio In foliosToValidate
                    ' Obtener el tercero efectivo a validar
                    Dim thirdPartyToValidate = folio.ThirdPartyIdForAgeValidation

                    Dim validationResult = Await model.ValidateAgeOfMajorityForLiquidationAsync(
                        thirdPartyToValidate,
                        FormOwner.IdOperatingUnitSelected,
                        folio.AdmissionNumber)

                    If Not validationResult.StateResult Then
                        Me.ShowMessage(EeventViewerImages.Advertencia) = validationResult.Message
                        Return False
                    End If

                    If validationResult.ObjectEmbbeded IsNot Nothing AndAlso validationResult.ObjectEmbbeded.RequiresResponsible Then
                        ' El tercero es menor de edad, mostrar popup para seleccionar responsable
                        Dim newResponsibleId As Integer? = Nothing
                        Dim newResponsibleName As String = String.Empty
                        Dim newResponsibleNit As String = String.Empty

                        Using frmResponsible As New FrmPopupPatientQuotaResponsible(
                            "Responsable Paciente",
                            validationResult.ObjectEmbbeded.SuggestedResponsibleThirdPartyId,
                            validationResult.ObjectEmbbeded.SuggestedResponsibleName,
                            validationResult.ObjectEmbbeded.SuggestedResponsibleNit)

                            frmResponsible.ShowMessage(eStatusResult.WARNING) = validationResult.ObjectEmbbeded.ValidationMessage
                            Dim transparent As New FrmTransparent(frmResponsible, False)

                            If transparent.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                newResponsibleId = frmResponsible.PatientQuotaResponsible
                                newResponsibleName = frmResponsible.PatientQuotaResponsibleText

                                Dim args As Object = New ExpandoObject()
                                args.FolioId = Me.Id
                                args.PatientQuotaResponsibleId = newResponsibleId

                                Me.IsAsyncOperation()
                                Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.SavePatientQuotaResponsible, args)
                                If res IsNot Nothing Then
                                    If res.StatusCode = eStatusResult.SUCCESS Then
                                        Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                                    Else
                                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                                    End If
                                Else
                                    If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                                        Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                                    Else
                                        Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                                    End If
                                End If
                                Me.IsAsyncOperation(False)

                                LblFolioTitle.Text = String.Format(ResourceManager.GetString("StrFolio", Me.GetType()), Me._folio.FolioOrder)

                                If newResponsibleId > 0 Then
                                    LblFolioTitle.Text &= $" ({newResponsibleName})"
                                End If
                            End If
                        End Using

                        If Not newResponsibleId.HasValue Then
                            Me.ShowMessage(EeventViewerImages.Advertencia) = "Es necesario asignar un tercero mayor de edad como responsable legal para continuar con la liquidación."
                            Return False
                        End If

                        ' Validar que el nuevo responsable sea mayor de edad
                        Dim newResponsibleValidation = Await model.ValidateAgeOfMajorityForLiquidationAsync(
                            newResponsibleId.Value,
                            FormOwner.IdOperatingUnitSelected,
                            folio.AdmissionNumber)

                        If Not newResponsibleValidation.StateResult Then
                            Me.ShowMessage(EeventViewerImages.Advertencia) = newResponsibleValidation.Message
                            Return False
                        End If

                        If newResponsibleValidation.ObjectEmbbeded IsNot Nothing AndAlso newResponsibleValidation.ObjectEmbbeded.RequiresResponsible Then
                            Me.ShowMessage(EeventViewerImages.Advertencia) = "El responsable seleccionado es menor de edad. Seleccione un responsable mayor de 18 años."
                            Return False
                        End If

                        ' Actualizar el responsable en todos los folios que tenían este tercero
                        Dim originalThirdPartyId = thirdPartyToValidate
                        For Each f In listFoliosToLiquidate.Where(Function(x) x.ThirdPartyIdForAgeValidation = originalThirdPartyId)
                            ' Actualizar según el tipo de folio
                            If f.FolioType = 3 OrElse f.IsMasterAccount = 4 Then
                                ' Folio particular: actualizar el campo Tercero
                                f.ThirdPartyPatientId = newResponsibleId.Value
                                f.SleThirdParty.EditValue = newResponsibleId.Value
                                f.SleThirdParty.Properties.NullText = newResponsibleName
                            Else
                                ' Otros tipos de folio: actualizar el ThirdPartyPatientId
                                f.ThirdPartyPatientId = newResponsibleId.Value
                            End If
                        Next
                    End If
                Next

                Return True
            End Using

        Catch ex As Exception
            Me.ShowMessage(EeventViewerImages.MensajeError) = $"Error al validar mayoría de edad: {ex.Message}"
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Runs the liquidate folio.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub RunLiquidateFolio(sender As Object, e As RunLiquidateFolioEventArgs, Optional skipAccountControlValidations As Boolean = False)
        Dim formAdvance As FrmAdvanceCrossing = CType(sender, FrmAdvanceCrossing)
        Try
            Using model As New MLiquidation()
                formAdvance.AsyncLoader(True)
                formAdvance.LoaderAsync(True)
                LockFoliosToLiquidate(e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().ToList(), True)

                Dim res = Await model.LiquidateFolioAsync(e.revenueControlDetailCrossingList, _folio.PatientCode, _folio.AdmissionNumber, FormOwner.IdBillingAuthorizationSelected, FormOwner.IdOperatingUnitSelected, Me.ThirdPartyPatientId, skipAccountControlValidations)
                If res IsNot Nothing AndAlso res.StateResult Then
                    LockFoliosToLiquidate(e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().ToList(), False)
                    formAdvance.AsyncLoader(False)

                    formAdvance.LoaderAsync(False)
                    formAdvance.Close()
                    'Mensaje de Liquidado con consecutivo res.ObjectEmbbeded
                    Me.ShowMessage(EeventViewerImages.Informacion) = String.Join(vbCrLf, res.MessageResult)
                    If Not res.Message.Trim().Equals(String.Empty) Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = res.Message
                    End If
                    If e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().ToList().Count = 1 Then
                        RaiseEvent RequiereReloadFolioWithPrint(Me, New RequiereReloadFolioEventArgs(e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().FirstOrDefault()))
                    Else
                        RaiseEvent RequiereReloadFolioList(Me, New RequiereReloadFolioListEventArgs(e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().ToList()))
                    End If
                Else
                    LockFoliosToLiquidate(e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().ToList(), False)
                    Me.IsAsyncOperation()
                    formAdvance.AsyncLoader(False)
                    formAdvance.LoaderAsync(False)
                    formAdvance.INDsbAcept.Enabled = True

                    If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                        Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    ElseIf res IsNot Nothing AndAlso res.Message = "-001" Then
                        If Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.CerrarConPendiente)) Then
                            Dim response = MessageIndigo.Show("Existen ítems por generar en control de cuentas hospitalario, el ingreso no se puede cerrar, ¿desea continuar?", MessageType.Question, Me.Text, Botones.SiNo)
                            If response = Windows.Forms.DialogResult.Yes Then
                                RunLiquidateFolio(sender, e, True)
                            End If
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = "Existen ítems por generar en control de cuentas hospitalario, el ingreso no se puede cerrar."
                        End If
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            formAdvance.AsyncLoader(False)
            formAdvance.LoaderAsync(False)
            LockFoliosToLiquidate(e.revenueControlDetailCrossingList.Select(Function(o) o.RevenueControlDetailId).Distinct().ToList(), False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Bloquea los folios a liquidar
    ''' </summary>
    ''' <param name="foliosId">Id de los folios a liquidar</param>
    Private Sub LockFoliosToLiquidate(foliosId As List(Of Integer), lock As Boolean)
        foliosId.Distinct().ToList().ForEach(Sub(folioId As Integer)
                                                 Dim ctrFolio As CtrFolio = CType(Me.DocumentParent.Manager.View.Documents _
                                                    .Where(Function(o) CType(o.Control, CtrFolio)._id = folioId).FirstOrDefault().Control, CtrFolio)
                                                 ctrFolio.IsAsyncOperation(lock)
                                             End Sub)
    End Sub

    ''' <summary>
    ''' Método que asocia una factura al folio
    ''' </summary>
    Public Sub AssociateInvoice(Optional listFoliosToLiquidate As List(Of CtrFolio) = Nothing)
        Using frmR As New FrmRelatedInvoices()
            frmR.AdmissionNumber = Me._folio.AdmissionNumber.Trim()
            frmR.RevenueControlDetailId = Me._folio.RevenueControlDetailId
            AddHandler frmR.BeginReloadLiquidationForm, AddressOf RefreshAdmission
            Dim transparent As New FrmTransparent(frmR, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Recarga el ingreso
    ''' </summary>
    Public Sub RefreshAdmission()
        RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
    End Sub

    ''' <summary>
    ''' Handles the ItemClick event of the MbtnRecalculate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
    Private Async Sub MbtnRecalculate_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRecalculate.ItemClick
        Dim args As Object = New ExpandoObject()
        args.FolioId = Me.Id
        Using model As New MLiquidation()
            Me.IsAsyncOperation()
            Dim res As ActionResult(Of String)

            If SettingBilling?.LiquidateMasterAccount Then
                res = Await Task.Factory.StartNew(Function() As ActionResult(Of String)
                                                      Return model.RecalculateFolioRest(Me.AdmissionNumber, Me.Id)
                                                  End Function)
            Else
                res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.RecalculateFolio, args)
            End If
            If res IsNot Nothing Then
                If res.StateResult Then
                    Me.ShowMessage(EeventViewerImages.Informacion) = ResourceManager.GetString("MessageRecalculate", Me.GetType())
                    'Se manda a recargar este folio y el destino
                    RaiseEvent RequiereReloadFolio(Me, New RequiereReloadFolioEventArgs(Me.Id))
                    Exit Sub
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            Else
                If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                End If
            End If
            Me.IsAsyncOperation(False)
        End Using
    End Sub

    ''' <summary>
    ''' Abre el formulario del detalle de los items empaquetados
    ''' </summary>
    Private Sub OpenViewItemsPackage(view As GridView)
        Me.Cursor = ChangeCursorIndigo()
        Me.IsAsyncOperation()
        If waitForm.IsSplashFormVisible Then
            waitForm.CloseWaitForm()
        End If
        waitForm.ShowWaitForm()
        Dim obj = Nothing
        If _datasourceType = eDatasourceType.RevenueControlDetail Then
            obj = CType(view.GetFocusedRow(), IFolioDetail)
        Else
            obj = CType(view.GetFocusedRow(), ViewListRevenueControlForAnnullateInvoiceXpo)
        End If
        Using frm As New FrmItemPackageDetail()
            If obj.RecordType = 1 Then
                frm.ItemPackaged = String.Concat(obj.IPSServiceCode, " - ", obj.IPSServiceName)
            Else
                frm.ItemPackaged = String.Concat(obj.ProductCode, " - ", obj.ProductName)
            End If
            frm.ServiceOrderDetailDistributionId = obj.Id
            frm.ServiceOrderDetailId = obj.ServiceOrderDetailId
            AddHandler frm.Shown, AddressOf FormShown
            Dim frmTransparent As New FrmTransparent(frm, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frmTransparent.ShowDialog(Me)
        End Using
        Me.IsAsyncOperation(False)
    End Sub

    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)
    ''' <summary>
    ''' Handles the MouseDoubleClick event of the GdcServices control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
    Private Sub GdcServices_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GdcServices.MouseDoubleClick

        If Status <> 3 Then
            If waitForm.IsSplashFormVisible Then
                waitForm.CloseWaitForm()
            End If
            waitForm.ShowWaitForm()
            'SplashScreenManager.ShowForm(Me, GetType(Presentation.Controls.wfMain), False, True)
            Dim lstSelectedItems = Nothing
            If _datasourceType = eDatasourceType.RevenueControlDetail Then
                lstSelectedItems = New List(Of IFolioDetail)()
            Else
                lstSelectedItems = New List(Of ViewListRevenueControlForAnnullateInvoiceXpo)()
            End If
            Dim view As GridView = IIf(GdcServices.MainView.Name = GdvSmallServices.Name, GdvSmallServices, GdvLargeServices)
            Parallel.ForEach(view.GetSelectedRows().Where(Function(r) r > -1).ToList(), Sub(i)
                                                                                            Dim itemList = Nothing
                                                                                            If _datasourceType = eDatasourceType.RevenueControlDetail Then
                                                                                                itemList = DirectCast(view.GetRow(i), IFolioDetail)
                                                                                            Else
                                                                                                itemList = DirectCast(view.GetRow(i), ViewListRevenueControlForAnnullateInvoiceXpo)
                                                                                            End If
                                                                                            lstSelectedItems.Add(itemList)
                                                                                        End Sub)
            'Agrego la opcion de Ver Items para un item empaquetado (solo esta opcion está disponible para un solo item empaquetado no para varios)
            If lstSelectedItems.Count = 1 Then
                If lstSelectedItems(0).IsPackage Then
                    Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
                    If hitInfo.InRow Then
                        OpenViewItemsPackage(view)
                    End If
                Else
                    Me.MenuItem_ViewServiceOrder(view)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the MouseUp event of the GdvSmallServices control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
    Private Sub GdvSmallServices_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GdvSmallServices.MouseUp, GdvLargeServices.MouseUp
        If Status <> 3 Then
            Dim lstSelectedItems = Nothing
            If _datasourceType = eDatasourceType.RevenueControlDetail Then
                lstSelectedItems = New List(Of IFolioDetail)()
            Else
                lstSelectedItems = New List(Of ViewListRevenueControlForAnnullateInvoiceXpo)()
            End If
            Dim view As GridView = CType(sender, GridView)
            view.GetSelectedRows().Where(Function(r) r > -1).ToList().ForEach(Sub(i)
                                                                                  Dim itemList = Nothing
                                                                                  If _datasourceType = eDatasourceType.RevenueControlDetail Then
                                                                                      itemList = DirectCast(view.GetRow(i), IFolioDetail)
                                                                                  Else
                                                                                      itemList = DirectCast(view.GetRow(i), ViewListRevenueControlForAnnullateInvoiceXpo)
                                                                                  End If
                                                                                  lstSelectedItems.Add(itemList)
                                                                              End Sub)
            'Agrego la opcion de Ver Items para un item empaquetado (solo esta opcion está disponible para un solo item empaquetado no para varios)
            If lstSelectedItems.Count = 1 Then
                If lstSelectedItems(0).IsPackage Then
                    Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
                    If hitInfo.InRowCell Then
                        Dim rowHandle As Integer = hitInfo.RowHandle
                        Dim column As GridColumn = hitInfo.Column
                        If column.FieldName = ColSmallIcon.FieldName Then
                            OpenViewItemsPackage(view)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the PceTotalPatient control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub PceTotalPatient_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles PceTotalPatient.QueryPopUp
        If _folio Is Nothing OrElse _folio.PatientDiscount = 0 Then
            e.Cancel = True
        Else
            LblPatientWithDiscount.Text = _folio.TotalPatientWithDiscount.MoneyFormat(2)
            LblPatientDiscount.Text = _folio.PatientDiscount.MoneyFormat(2)
        End If
    End Sub

    ''' <summary>
    ''' Enumeración RecordType
    ''' </summary>
    Public Enum eRecordType
        Services = 1
        Drugs = 2
    End Enum

    Public Enum eDatasourceType
        RevenueControlDetail
        AnnullateInvoice
    End Enum

#End Region

    Private Sub SleCategories_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles SleCategories.EditValueChanging
        If Not _isLoading AndAlso (Status <> 1 AndAlso Not Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarCategoriaFactura))) Then
            e.Cancel = True
            If Status = 2 AndAlso Not Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarCategoriaFactura)) Then
                ShowMessage(eStatusResult.WARNING) = "No tiene permisos para realizar esta acción"
            End If
        End If
    End Sub

    Private Async Sub SleCategories_EditValueChanged(sender As Object, e As EventArgs) Handles SleCategories.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If
        If SleCategories.EditValue IsNot Nothing Then
            Me.IsAsyncOperation()
            Dim args As Object = New ExpandoObject()
            args.FolioId = Me.Id
            args.CategoryId = CInt(SleCategories.EditValue)
            Using model As New MLiquidation
                Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.UpdateCategoryFolio, args)
                If res IsNot Nothing Then
                    Me.ShowMessage(res.StatusCode) = res.Message
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
            Me.IsAsyncOperation(False)
        End If
    End Sub

    Private Sub MeObservation_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles MeObservation.EditValueChanging
        If Not _isLoading AndAlso (Status <> 1 AndAlso Not Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarObservacionFactura))) Then
            e.Cancel = True
            If Status = 2 AndAlso Not Me.FormOwner.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarObservacionFactura)) Then
                ShowMessage(eStatusResult.WARNING) = "No tiene permisos para realizar esta acción"
            End If
        End If
    End Sub

    Private Async Sub MeObservation_EditValueChanged(sender As Object, e As EventArgs) Handles MeObservation.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If
        If SleCategories.EditValue IsNot Nothing Then
            Me.IsAsyncOperation()
            Dim args As Object = New ExpandoObject()
            args.FolioId = Me.Id
            args.Observation = MeObservation.Text
            Using model As New MLiquidation
                Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.UpdateObservationFolio, args)
                If res IsNot Nothing Then
                    If res.StateResult Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = "La acción se ejecutó correctamente"
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                Else
                    Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
            Me.IsAsyncOperation(False)
        Else
            If MeObservation.EditValue IsNot Nothing Then
                MeObservation.EditValue = Nothing
                Me.ShowMessage(EeventViewerImages.MensajeError) = "No se guardó la observación, debe de seleccionar una categoría primero"
            End If
        End If
    End Sub

    Private Sub SleCategories_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles SleCategories.QueryPopUp
        If SleCategories.Properties.DataSource Is Nothing Then
            'InvoiceCategoriesXpo
            Using model As New MLiquidation()
                Dim setting = model.GetSettingsBillingByOperatingUnitId(FormOwner.IdOperatingUnitSelected)
                If setting Is Nothing Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "Actualmente no existe parametros de facturacion"
                    Return
                End If
                SleCategories.Properties.DataSource = model.ListInvoiceCategoryByPermissionCategories(setting.PermissionCategories, SleCareGroup.EditValue)
            End Using
        End If
    End Sub

    '''' <summary>
    '''' Tooltip
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    'Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
    '    If e.Info Is Nothing AndAlso e.SelectedControl Is GdcServices Then
    '        Dim view As GridView = TryCast(GdcServices.FocusedView, GridView)
    '        Dim info As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
    '        If info.InRowCell Then
    '            Dim text_Renamed As String = view.GetRowCellDisplayText(info.RowHandle, info.Column)
    '            Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()
    '            'If info.Column Is ColSmallIsItemProduction OrElse info.Column Is ColIsItemProduction Then text_Renamed = "Es Item Producción"
    '            e.Info = New ToolTipControlInfo(cellKey, text_Renamed)
    '        End If
    '    End If
    'End Sub

#Region "CustomDrawCell"
    ''' <summary>
    ''' Aqui se asigna el dato que le corresponde a la celda dependiendo de las condiciones
    ''' </summary>
    Private Sub GdvSmallServices_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GdvSmallServices.CustomDrawCell, GdvLargeServices.CustomDrawCell
        Dim view = DirectCast(sender, GridView)
        Dim obj = view.GetRow(e.RowHandle)
        If obj IsNot Nothing Then
            If e.Column.Name.Equals(Me.ColLargeMoreInfoQx.Name) OrElse e.Column.Name.Equals(ColSmallMoreInfoQx.Name) Then

                If obj.Presentation <> 2 AndAlso obj.FlagProductServiceDetail <> 1 Then
                    e.DisplayText = String.Empty
                    e.Handled = True
                End If

            ElseIf e.Column.Equals(Me.ColServiceDate) Then
                e.DisplayText = Convert.ToDateTime(obj.ServiceDate).ToString(SessionValues.Instance.Culture)
            End If
        End If
    End Sub
#End Region

    Private repositoryItems As New Dictionary(Of String, RepositoryItem)()

    ''' <summary>
    ''' Custom Row Cell Edit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GdvSmallServices_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles GdvSmallServices.CustomRowCellEdit, GdvLargeServices.CustomRowCellEdit
        If e.Column.FieldName = ColSmallIcon.FieldName Then
            Dim view As GridView = TryCast(sender, GridView)
            Dim value = e.CellValue
            Dim be As RepositoryItem = Nothing

            If value IsNot Nothing Then
                'If Not repositoryItems.TryGetValue(value, be) Then
                Dim row = view.GetRow(e.RowHandle)
                be = CreateButtonEditByString(value, row)
                view.GridControl.RepositoryItems.Add(be)
                repositoryItems(value) = be
                'End If
                e.RepositoryItem = be
            End If
        End If
    End Sub

    ''' <summary>
    ''' Crea los íconos
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="row"></param>
    ''' <returns></returns>
    Private Function CreateButtonEditByString(ByVal value As Byte, row As Object) As RepositoryItem
        Dim iconIndex = CByte(row.DistributionType) - 1
        Dim tooltip = row.DistributionTypeName

        If row.IsPackage Then
            If row.DistributionType = 2 Then
                iconIndex = 6
                tooltip = "Empaquetado"
            Else
                iconIndex = 5
                tooltip = "Empaquetado y Distribuido"
            End If
        End If

        Dim buttonEdit As New RepositoryItemButtonEdit()
        buttonEdit.Buttons.Clear()
        buttonEdit.TextEditStyle = TextEditStyles.HideTextEditor
        buttonEdit.ButtonsStyle = BorderStyles.NoBorder
        buttonEdit.BorderStyle = BorderStyles.NoBorder
        buttonEdit.Appearance.BorderColor = Color.Transparent
        buttonEdit.AppearanceDisabled.BorderColor = Color.Transparent
        buttonEdit.AppearanceFocused.BorderColor = Color.Transparent
        buttonEdit.AppearanceReadOnly.BorderColor = Color.Transparent

        If iconIndex > 0 Then
            Dim button As New EditorButton()
            button.Kind = ButtonPredefines.Glyph
            button.ToolTip = row.DistributionTypeName
            button.ImageOptions.Image = IMCDistributionType.Images(iconIndex)
            button.Appearance.BackColor = Color.Transparent
            button.Appearance.BackColor2 = Color.Transparent
            button.Appearance.BorderColor = Color.Transparent
            button.AppearanceDisabled.BorderColor = Color.Transparent
            button.AppearanceHovered.BorderColor = Color.Transparent
            button.AppearancePressed.BorderColor = Color.Transparent
            buttonEdit.Buttons.Add(button)
        End If

        If CBool(row.IsItemProduction) Then
            Dim button As New EditorButton()
            button.Kind = ButtonPredefines.Glyph
            button.ToolTip = "Es Item Producción"
            button.ImageOptions.Image = IMCDistributionType.Images(7)
            button.Appearance.BackColor = Color.Transparent
            button.Appearance.BackColor2 = Color.Transparent
            button.Appearance.BorderColor = Color.Transparent
            button.AppearanceDisabled.BorderColor = Color.Transparent
            button.AppearanceHovered.BorderColor = Color.Transparent
            button.AppearancePressed.BorderColor = Color.Transparent
            buttonEdit.Buttons.Add(button)
        End If

        'Dim view = CType(GdcServices.MainView, GridView)
        'If buttonEdit.Buttons.Count * 30 > columnWidth Then
        '    GdcServices.BeginUpdate()
        '    view.BeginUpdate()
        '    columnWidth = buttonEdit.Buttons.Count * 30
        '    view.Columns("IconType").Width = columnWidth
        '    view.Columns("IconType").OptionsColumn.FixedWidth = True
        '    view.OptionsView.ColumnAutoWidth = True
        '    view.EndUpdate()
        '    GdcServices.EndUpdate()
        'End If

        Return buttonEdit
    End Function

    Private columnWidth As Integer = 0

    Private Sub MbtnPatientQuotaResponsible_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnPatientQuotaResponsible.ItemClick
        OpenFrmPatientResponsible()
    End Sub

    Private Async Sub OpenFrmPatientResponsible()
        Using frm As New FrmPopupPatientQuotaResponsible()
            frm.PatientQuotaResponsible = _patientQuotaResponsibleId
            frm.PatientQuotaResponsibleText = _patientQuotaResponsibleText
            Dim transparent As New FrmTransparent(frm, False)
            If transparent.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim selectedResponsibleId = frm.PatientQuotaResponsible
                Dim selectedResponsibleText = frm.PatientQuotaResponsibleText

                ' Validar mayoría de edad del tercero seleccionado
                Using model As New MLiquidation()
                    If model.IsAgeValidationEnabled(FormOwner.IdOperatingUnitSelected) AndAlso selectedResponsibleId.HasValue Then
                        Me.IsAsyncOperation()
                        Dim validationResult = Await model.ValidateAgeOfMajorityForLiquidationAsync(
                            selectedResponsibleId.Value,
                            FormOwner.IdOperatingUnitSelected,
                            _folio.AdmissionNumber)
                        Me.IsAsyncOperation(False)

                        If Not validationResult.StateResult Then
                            Me.ShowMessage(EeventViewerImages.Advertencia) = validationResult.Message
                            Exit Sub
                        End If

                        If validationResult.ObjectEmbbeded IsNot Nothing AndAlso validationResult.ObjectEmbbeded.RequiresResponsible Then
                            Me.ShowMessage(EeventViewerImages.Advertencia) = "El responsable seleccionado es menor de edad. Seleccione un responsable mayor de 18 años."
                            Exit Sub
                        End If
                    End If
                End Using

                _patientQuotaResponsibleId = selectedResponsibleId
                _patientQuotaResponsibleText = selectedResponsibleText

                Dim args As Object = New ExpandoObject()
                args.FolioId = Me.Id
                args.PatientQuotaResponsibleId = _patientQuotaResponsibleId

                Using model As New MLiquidation()
                    Me.IsAsyncOperation()
                    Dim res = Await model.ExecuteActionMethod(LiquidationActionMethod.SavePatientQuotaResponsible, args)
                    If res IsNot Nothing Then
                        If res.StatusCode = eStatusResult.SUCCESS Then
                            Me.ShowMessage(EeventViewerImages.Informacion) = "La operación se ejecutó correctamente"
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                        End If
                    Else
                        If res Is Nothing OrElse res.Message.Equals(String.Empty) Then
                            Me.ShowMessage(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        Else
                            Me.ShowMessage(EeventViewerImages.MensajeError) = res.Message
                        End If
                    End If
                    Me.IsAsyncOperation(False)
                End Using

                LblFolioTitle.Text = String.Format(ResourceManager.GetString("StrFolio", Me.GetType()), Me._folio.FolioOrder)

                If _patientQuotaResponsibleId > 0 Then
                    LblFolioTitle.Text &= $" ({_patientQuotaResponsibleText})"
                End If
            End If
        End Using
    End Sub

End Class

''' <summary>
''' Encapsula los argumentos del evento RequiereReloadFolio
''' </summary>
Public Class RequiereReloadFolioEventArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el Id del folio a recargar
    ''' </summary>
    ''' <value>Id del folio a recargar</value>
    ''' <returns>Id del folio a recargar</returns>
    Public Property IdTargetFolio As Integer

    ''' <summary>
    ''' Permite saber si se debe nulear la categoria al momento de cambiar el grupo de atención
    ''' </summary>
    ''' <returns></returns>
    Public Property NullCategories As Boolean

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="idTargetFolio">Id del folio a recargar</param>
    Public Sub New(ByVal idTargetFolio As Integer, Optional nullCategories As Boolean = False)
        Me.IdTargetFolio = idTargetFolio
        Me.NullCategories = nullCategories
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento RequiereReloadFolio
''' </summary>
Public Class RequiereReloadFolioListEventArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene los Ids de los folios a recargar
    ''' </summary>
    ''' <value>
    ''' The list identifier target folio.
    ''' </value>
    Public Property ListIdTargetFolio As List(Of Integer)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal listIdTargetFolio As List(Of Integer))
        Me.ListIdTargetFolio = listIdTargetFolio
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento BillingAuthotization
''' </summary>
Public Class BillingAuthotizationEventArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el id de la resolución de facturación usada
    ''' </summary>
    ''' <value>Id de la resolución</value>
    ''' <returns>El id de la resolución</returns>
    Public Property IdBillingAuthorization As Integer

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="idBillingAuthorization">Id de la resolución</param>
    Public Sub New(ByVal idBillingAuthorization As Integer)
        Me.IdBillingAuthorization = idBillingAuthorization
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento LoadDatasourceEnd
''' </summary>
Public Class LoadDatasourceEndEventArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el control de folio quien finalizó la carga de datasource
    ''' </summary>
    ''' <value>Control de folio</value>
    ''' <returns>El control de folio quien finalizó la carga</returns>
    Public Property CtrFolio As ICtrFolio

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="ctrFolio">Control de folio quien finalizó la carga</param>
    Public Sub New(ByVal ctrFolio As ICtrFolio)
        Me.CtrFolio = ctrFolio
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento BeginReloadDatasource
''' </summary>
Public Class BeginReloadDatasourceEventArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el control de folio quien inicia la recarga de datasource
    ''' </summary>
    ''' <value>Control de folio</value>
    ''' <returns>El control de folio quien inicia la recarga</returns>
    Public Property CtrFolio As ICtrFolio

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="ctrFolio">Control de folio quien inicia la recarga</param>
    Public Sub New(ByVal ctrFolio As ICtrFolio)
        Me.CtrFolio = ctrFolio
    End Sub

#End Region

End Class