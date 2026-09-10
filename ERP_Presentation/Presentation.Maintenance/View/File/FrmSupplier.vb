'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 10-08-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Maintenance.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Presentation.Common.MVP
Imports System.Runtime.CompilerServices
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Presentation.Common
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports DevExpress.Utils.Menu
Imports Presentation.Payments.MVP

#End Region


''' <summary>
''' Clase que contiene todo el comportamiento de la vista......
''' </summary>
Public Class FrmSupplier
    Implements ISupplier

#Region "Properties"
    ''' <summary>
    ''' Obtiene el Estado del cuentas bancarias del proveedor
    ''' </summary>
    Public Property State As Byte Implements ISupplier.State
        Get
            Return INDGleState.EditValue
        End Get
        Set(value As Byte)
            INDGleState.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de bancos
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Public Property BankDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ISupplier.BankDatasource
        Get
            Return CType(INDsleBankEntityAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBankEntityAccount.Properties.DataSource = value
        End Set
    End Property

    Private _idCurrentSequence As Long

    ''' <summary>
    ''' Obtiene o establece el id del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeId As Integer Implements ISupplier.SupplierTypeId
        Get
            Return INDsleSupplierType.EditValue
        End Get
        Set(value As Integer)
            INDsleSupplierType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeXpo As XPCollection Implements ISupplier.SupplierTypeXpo
        Get
            Return INDsleSupplierType.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleSupplierType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DistribDistributionLineXpoutionLine As XPInstantFeedbackSource Implements ISupplier.DistributionLineXpo
        Get
            Return CType(INDsleDistributionLineId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDistributionLineId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de cargos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PositionXpo As XPInstantFeedbackSource Implements ISupplier.PositionXpo
        Get
            Return CType(INDslePositionId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePositionId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party cards.
    ''' </value>
    Public Property IdThirdParty As Integer? Implements ISupplier.IdThirdParty
        Get
            Return CInt(INDsleThirdParty.EditValue)
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Public Property ThirdPartyDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ISupplier.ThirdPartyDatasource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeLimitDays As Integer Implements ISupplier.TimeLimitDays
        Get
            Return INDseTimeLimitDays.EditValue
        End Get
        Set(value As Integer)
            INDseTimeLimitDays.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo del fabricante
    ''' </summary>
    Public Property Code As String Implements ISupplier.Code
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del fabricante
    ''' </summary>
    Public Property NameSupplier As String Implements ISupplier.NameSupplier
        Get
            Return INDtxtName.Text.Trim
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo CMMS
    ''' </summary>
    Public Property CodeCMMS As String Implements ISupplier.CodeCMMS
        Get
            Return INDtxtCodeCMMS.Text.Trim
        End Get
        Set(value As String)
            INDtxtCodeCMMS.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la direccion del sitio web del fabricante
    ''' </summary>
    Public Property WebSiteSupplier As String Implements ISupplier.WebSiteSupplier
        Get
            Return INDtxtWebSite.Text.Trim
        End Get
        Set(value As String)
            INDtxtWebSite.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el tipo de proveedor 
    ''' </summary>
    Public Property Status As Boolean Implements ISupplier.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    Private _sequence As PaymentsSecuence
    Public Property Sequence As PaymentsSecuence Implements ISupplier.Sequence
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
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements ISupplier.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Contiene el listado de las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISupplier.CitiesXpo
        Get
            Return CType(INDsleCity.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la ciudad de la dependencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCity As Integer? Implements ISupplier.IdCity
        Get
            Return CInt(INDsleCity.EditValue)
        End Get
        Set(value As Integer?)
            INDsleCity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor es declarante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Declarant As Boolean Implements ISupplier.Declarant
        Get
            Return INDsleDeclarant.EditValue
        End Get
        Set(value As Boolean)
            INDsleDeclarant.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor maneja retencion permanente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PermanentRetention As Boolean Implements ISupplier.PermanentRetention
        Get
            Return (INDckRetentionOptions.Items(0).CheckState = CheckState.Checked)
        End Get
        Set(value As Boolean)
            INDckRetentionOptions.Items(0).CheckState = IIf(value, CheckState.Checked, CheckState.Unchecked)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor es autorretenedor a título de renta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelfWithholding As Boolean Implements ISupplier.SelfWithholding
        Get
            Return (INDckRetentionOptions.Items(1).CheckState = CheckState.Checked)
        End Get
        Set(value As Boolean)
            INDckRetentionOptions.Items(1).CheckState = IIf(value, CheckState.Checked, CheckState.Unchecked)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor es autorretenedor a título de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelfWithholdingICA As Boolean Implements ISupplier.SelfWithholdingICA
        Get
            Return (INDckRetentionOptions.Items(2).CheckState = CheckState.Checked)
        End Get
        Set(value As Boolean)
            INDckRetentionOptions.Items(2).CheckState = IIf(value, CheckState.Checked, CheckState.Unchecked)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor no maneja IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NotIva As Boolean Implements ISupplier.NotIva
        Get
            Return (INDckRetentionOptions.Items(3).CheckState = CheckState.Checked)
        End Get
        Set(value As Boolean)
            INDckRetentionOptions.Items(3).CheckState = IIf(value, CheckState.Checked, CheckState.Unchecked)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor es fabricante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Manufacturer As Boolean Implements ISupplier.Manufacturer
        Get
            Return (INDckRetentionOptions.Items(4).CheckState = CheckState.Checked)
        End Get
        Set(value As Boolean)
            INDckRetentionOptions.Items(4).CheckState = IIf(value, CheckState.Checked, CheckState.Unchecked)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el proveedor es vendedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Seller As Boolean Implements ISupplier.Seller
        Get
            Return (INDckRetentionOptions.Items(5).CheckState = CheckState.Checked)
        End Get
        Set(value As Boolean)
            INDckRetentionOptions.Items(5).CheckState = IIf(value, CheckState.Checked, CheckState.Unchecked)
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que se le asigna al datasource de pronto pago
    ''' </summary>
    ''' <returns></returns>
    Public Property ListPromptPaymentDiscount As List(Of PromptPaymentDiscount) Implements ISupplier.ListPromptPaymentDiscount
        Get
            Return TryCast(INDGcPromptPaymentDiscount.DataSource, List(Of PromptPaymentDiscount))
        End Get
        Set(value As List(Of PromptPaymentDiscount))
            INDGcPromptPaymentDiscount.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  propiedad que obtiene o establece el datasource que lista las monedas
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyXpo As XPInstantFeedbackSource Implements ISupplier.CurrencyXpo
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad privada que obtiene o establece el id de la moneda de la cuenta bancaria
    ''' que se agregará al detalle de cuentas del proveedor
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyId As Integer?
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece  Costeo inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentInventoryCosting As Integer Implements ISupplier.ConsignmentInventoryCosting
        Get
            Return INDSleConsignmentInventoryCosting.EditValue
        End Get
        Set(value As Integer)
            INDSleConsignmentInventoryCosting.EditValue = value
        End Set
    End Property
#End Region

#Region "Variable"

    ''' <summary>
    ''' contiene la lista a llenar Estado de cuentas bancarias
    ''' </summary>
    Dim ListState As New List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' contiene la lista a llenar en el tipo de cuenta
    ''' </summary>
    Dim TypeFile As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de categorias tributarias
    ''' </summary>
    Private TaxCategories As New List(Of Tuple(Of Byte, String))()

    Dim listAdress As List(Of Domain.Entities.Address)

    Dim Pivote As Boolean = False

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PSupplier

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMaintenance

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

    ''' <summary>
    ''' Variable que contiene la entidad aseguradoras
    ''' </summary>
    Dim Supplier As Domain.Entities.Supplier

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Dim ListadoEliminadosTelefono As New List(Of Domain.Entities.Phone)

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoEliminadosDireccion As New List(Of Domain.Entities.Address)

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoAgregados As New List(Of Domain.Entities.Address)

    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Dim ListadoEliminadosEmail As New List(Of Domain.Entities.Email)

    ''' <summary>
    ''' Listado de tipos de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListSupplierDetailType As List(Of SupplierDetailType)

    ''' <summary>
    ''' Listado de eliminados de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteDistributionLine As List(Of DistributionLines)

    ''' <summary>
    ''' Listado de eliminados de tipos de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteSupplierDetailType As List(Of SupplierDetailType)

    ''' <summary>
    ''' Representa la entidad de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim distributionLine As DistributionLines

    ''' <summary>
    ''' Representa la entidad de la relacion de lineas con proveedor
    ''' </summary>
    Dim supplierDistributionLine As SuppliersDistributionLines

    ''' <summary>
    ''' Representa la entidad de tipos de prtoveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim supplierDetailType As SupplierDetailType

    ''' <summary>
    ''' Listado de las lineas de distribucion que tiene el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim listSupplierDistributionLines As List(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Listado de eliminados de las lineas de distribucion que tiene el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteSupplierDistributionLines As List(Of SuppliersDistributionLines)

    Dim mode As Boolean

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditDistributionLineMode As Boolean

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    Dim FlagEditPromptPaymentDiscount As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListCostingInvConsignment As New List(Of Tuple(Of Integer, String))

#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using Model As New MSupplier(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of Domain.Entities.Supplier) = Await Model.SaveSupplier(Me.Supplier, listSupplierDistributionLines, ListSupplierDetailType, mode, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Supplier = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar un fabricante.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.Supplier IsNot Nothing AndAlso Me.Supplier.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MSupplier(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteSupplier(Me.Supplier, listSupplierDistributionLines, ListSupplierDetailType)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewProvider()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Supplier
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
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        CleanControlsBankAccount()
        CleanControlsDistributionLine()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
#End Region

#Region "Methods"

    ''' <summary>
    '''  Metodo que cambia el estado a Inactivo de cuentas bancarias del proveedor
    ''' </summary>
    Private Sub InactiveSupplierAccount()
        Dim supplierBankAccount = DirectCast(INDGvBankAccount.GetFocusedRow(), Domain.Entities.SupplierBankAccount)
        If supplierBankAccount.State = 1 Then
            supplierBankAccount.State = 2
            INDGcBanckAccount.RefreshDataSource()
        End If

    End Sub
    ''' <summary>
    '''  Metodo que cambia el estado a Activo de cuentas bancarias del proveedor
    ''' </summary>
    Private Sub ActivateSupplierAccount()
        Dim supplierBankAccount = DirectCast(INDGvBankAccount.GetFocusedRow(), Domain.Entities.SupplierBankAccount)
        If supplierBankAccount.State = 2 Then
            supplierBankAccount.State = 1
        End If
        INDGcBanckAccount.RefreshDataSource()
    End Sub
    ''' <summary>
    '''  Metodo que Elimina cuentas bancarias del proveedor
    ''' </summary>
    Private Async Sub DeleteSupplierAccount()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim supplierBankAccount = DirectCast(INDGvBankAccount.GetFocusedRow(), Domain.Entities.SupplierBankAccount)

            Using m As New MSupplier(MyTag)
                Dim hasMovement = Await m.HasAccountingMovementsForSupplierBankAccount(supplierBankAccount.Id)
                If hasMovement Then
                    Mensaje(EeventViewerImages.Advertencia) = "La cuenta seleccionada no se puede eliminar ya que tiene movimientos contables asociados."
                    Exit Sub
                End If
            End Using

            If supplierBankAccount.Id > 0 Then
                supplierBankAccount.MarkAsDeleted()
            Else
                Supplier.SupplierBankAccount.Remove(supplierBankAccount)
            End If

            INDGlePrioritizeBankAccount.Enabled = True
            INDGcBanckAccount.DataSource = Nothing
            INDGcBanckAccount.DataSource = Supplier.SupplierBankAccount
        End If
    End Sub

    ''' <summary>
    '''  Metodo que crea la columna de Acciones del Segmento Cuentas Bancarias Descuento por pronto pago
    ''' </summary>
    Private Sub PromptPaymentDiscountColumn()
        Dim ListActionPaymentD = New List(Of eAcciones)
        ListActionPaymentD.Add(eAcciones.Edit)
        ListActionPaymentD.Add(eAcciones.Remove)
        IndigoGridView4.SetListAcction(INDGvPromptPaymentDiscount, ListActionPaymentD)
    End Sub
    ''' <summary>
    ''' Metodo que crea la columna de Acciones del Segmento Cuentas Bancarias
    ''' </summary>
    Private Sub StartColumnAdjustment()
        Dim supplierBankAccount = DirectCast(INDGvBankAccount.GetFocusedRow(), Domain.Entities.SupplierBankAccount)
        Dim SetListAcction As New List(Of eAcciones)

        If supplierBankAccount IsNot Nothing AndAlso supplierBankAccount.State <> 0 Then

            If supplierBankAccount.State = 1 Then
                SetListAcction.Add(eAcciones.Inactivate)
            ElseIf supplierBankAccount.State = 2 Then
                SetListAcction.Add(eAcciones.Activate)
            End If
            SetListAcction.Add(eAcciones.Remove)
        End If

        IndigoGridView3.SetListAcction(INDGvBankAccount, SetListAcction)
        INDGvBankAccount.Columns.ColumnByName("colActions").Caption = "Acciones"
        INDGvBankAccount.Columns.ColumnByName("colActions").Width = 120

        Dim col = INDGvBankAccount.Columns.ColumnByName("colActions")
        col.VisibleIndex = 4

    End Sub
    ''' <summary>
    ''' Carga las listado de estado banco
    ''' </summary>
    Private Sub LoadState()
        ListState.Add(New Tuple(Of Byte, String)(1, "Activo"))
        ListState.Add(New Tuple(Of Byte, String)(2, "Inactivo"))
        INDGleState.Properties.DataSource = ListState.ToList
    End Sub

    ''' <summary>
    ''' Carga las categorias tributarias
    ''' </summary>
    Private Sub LoadTaxCategories()
        TaxCategories.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("TaxCategoryNone")))
        TaxCategories.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("TaxCategoryEmployed")))
        TaxCategories.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("TaxCategoryWorkerAccountSelf")))
        TaxCategories.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("TaxCategoryOther")))

        gleTaxCategory.Properties.DataSource = TaxCategories
    End Sub

    Private Sub LoadCostingInvConsignment()
        ListCostingInvConsignment = New List(Of Tuple(Of Integer, String))
        ListCostingInvConsignment.Add(New Tuple(Of Integer, String)(0, "Costo promedio"))
        ListCostingInvConsignment.Add(New Tuple(Of Integer, String)(1, "Lista de costos"))
        INDSleConsignmentInventoryCosting.Properties.DataSource = ListCostingInvConsignment.ToList
    End Sub

    Private Sub CleanControlsBankAccount()
        INDsleBankEntityAccount.EditValue = Nothing
        INDgleType.EditValue = Nothing
        INDtxtNumber.EditValue = Nothing
        INDGlePaymentDefault.EditValue = Nothing
        CurrencyId = indigo.OfficialCurrencyId
        State = Nothing
        INDsleCurrency.Properties.NullText = indigo.CurrencyISO4217
    End Sub

    Private Sub CleanControlsDistributionLine()
        FlagEditDistributionLineMode = False
        INDsleDistributionLineId.EditValue = Nothing
        INDsleDistributionLineId.Properties.NullText = String.Empty
        INDslePositionId.EditValue = Nothing
        INDslePositionId.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Crea la lista de tipo de cuenta
    ''' </summary>
    Public Sub CreateTypeFile()
        TypeFile.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountTypeCurrent")))
        TypeFile.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountTypeSaving")))

        INDgleType.Properties.DataSource = TypeFile
    End Sub

    ''' <summary>
    ''' Activa o inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISupplier.ActionsOnControls
        Set(value As Boolean)
            INDlySupplier.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleThirdParty.Enabled = value
            INDseTimeLimitDays.Enabled = value
            INDsleSupplierType.Enabled = value
            INDbtnAddType.Enabled = value
            INDgcType.Enabled = value
            INDGlePrioritizeBankAccount.Enabled = value
            INDPceBanckAccount.Enabled = value
            INDGcBanckAccount.Enabled = value
            INDckRetentionOptions.Enabled = value
            INDtxtCodeCMMS.Enabled = value
            INDsleDeclarant.Enabled = value
            INDSleIndependentEmployee.Enabled = value
            INDtxtWebSite.Enabled = value
            INDsleCity.Enabled = value
            gleTaxCategory.Enabled = value
            INDccbeTaxIndentification.Enabled = value
            INDpceDistributionLine.Enabled = value
            INDgcLines.Enabled = value
            INDGcPromptPaymentDiscount.Enabled = value
            INDpcePromptPaymentDiscount.Enabled = value
            INDSleConsignmentInventoryCosting.Enabled = value

            INDpceContactData.Enabled = value
            INDlySupplier.EndUpdate()
            If value Then
                INDsleThirdParty.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlySupplier.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Code = String.Empty
        NameSupplier = String.Empty
        IdThirdParty = Nothing
        TimeLimitDays = 0
        SupplierTypeId = Nothing
        listDeleteDistributionLine = Nothing
        listDeleteSupplierDistributionLines = Nothing
        listSupplierDistributionLines = Nothing
        supplierDistributionLine = Nothing
        ListSupplierDetailType = Nothing
        ListDeleteSupplierDetailType = Nothing
        INDgcType.DataSource = Nothing
        INDgcLines.DataSource = Nothing
        INDGcBanckAccount.DataSource = Nothing

        INDckRetentionOptions.UnCheckAll()
        CodeCMMS = String.Empty
        Status = True
        INDGlePrioritizeBankAccount.EditValue = False
        Declarant = Nothing
        INDSleIndependentEmployee.EditValue = False
        If CtrContacts IsNot Nothing Then
            CtrContacts.LimpiarControles()
        End If
        WebSiteSupplier = String.Empty
        INDsleCity.EditValue = Nothing
        Supplier = Nothing
        listAdress = Nothing
        IndigoGridControl1.RefreshGrid(INDGcBanckAccount)
        Pivote = False
        CleanPromptPopup()
        ListPromptPaymentDiscount = New List(Of PromptPaymentDiscount)

        INDccbeTaxIndentification.Properties.Items(0).CheckState = CheckState.Unchecked
        INDccbeTaxIndentification.Properties.Items(1).CheckState = CheckState.Unchecked
        INDccbeTaxIndentification.Properties.Items(2).CheckState = CheckState.Unchecked
        INDccbeTaxIndentification.Properties.Items(3).CheckState = CheckState.Unchecked
        INDccbeTaxIndentification.Properties.Items(4).CheckState = CheckState.Unchecked

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlySupplier.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MSupplier(CStr(Me.Tag))
                    AsyncLoader(True)
                    Supplier = Await Model.GetSupplier(INDbteCode.Text.Trim)
                    INDlySupplier.BeginUpdate()
                    If Supplier IsNot Nothing AndAlso Supplier.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Supplier.Id))
                            With Supplier
                                'LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                NameSupplier = .Name
                                IdThirdParty = .IdThirdParty
                                TimeLimitDays = .TimeLimitDays
                                CodeCMMS = .CodeCMMS
                                gleTaxCategory.EditValue = .TaxCategory
                                ConsignmentInventoryCosting = .ConsignmentInventoryCosting
                                Declarant = .Declarant
                                INDSleIndependentEmployee.EditValue = .IndependentEmployee

                                WebSiteSupplier = .WebSite
                                PermanentRetention = .PermanentRetention
                                SelfWithholding = .SelfWithholding
                                SelfWithholdingICA = .SelfWithholdingICA
                                NotIva = .NotIva
                                Manufacturer = .Manufacturer
                                Seller = .Seller

                                IdCity = .IdCity
                                INDGcBanckAccount.DataSource = .SupplierBankAccount
                                Using modelST As New MSuppliersDetailType("")
                                    ListSupplierDetailType = modelST.GetSuppliersDetailTypeByIdSupplier(.Id)
                                End Using
                                INDgcType.DataSource = Nothing
                                INDgcType.DataSource = ListSupplierDetailType

                                Using ModelDl As New MSuppliersDistributionLines("")
                                    listSupplierDistributionLines = ModelDl.GetSuppliersDistributionLinesByIdSupplier(.Id)
                                End Using
                                If listSupplierDistributionLines Is Nothing Then
                                    listSupplierDistributionLines = New List(Of SuppliersDistributionLines)()
                                End If

                                INDgcLines.DataSource = Nothing
                                INDgcLines.DataSource = listSupplierDistributionLines

                                Status = .Status
                                'Control Datos de Contacto
                                If CtrContacts IsNot Nothing Then
                                    CtrContacts.EstablecerDataSourceDireccion = .ThirdParty.Person.Address.ToList
                                    CtrContacts.EstablecerDataSourceTelefono = .ThirdParty.Person.Phone.ToList
                                    CtrContacts.EstablecerDataSourceEmail = .ThirdParty.Person.Email.ToList
                                End If
                                Me.ListPromptPaymentDiscount = .PromptPaymentDiscount.ToList()

                                INDccbeTaxIndentification.Properties.Items(0).CheckState = IIf(.WorkRent, CheckState.Checked, CheckState.Unchecked)
                                INDccbeTaxIndentification.Properties.Items(1).CheckState = IIf(.Pensions, CheckState.Checked, CheckState.Unchecked)
                                INDccbeTaxIndentification.Properties.Items(2).CheckState = IIf(.CapitalRent, CheckState.Checked, CheckState.Unchecked)
                                INDccbeTaxIndentification.Properties.Items(3).CheckState = IIf(.NotLaborRent, CheckState.Checked, CheckState.Unchecked)
                                INDccbeTaxIndentification.Properties.Items(4).CheckState = IIf(.DividendsAndParticipations, CheckState.Checked, CheckState.Unchecked)
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Supplier.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Supplier.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(Supplier.Id, Me.Tag.ToString(), Nothing, GetType(Domain.Entities.Supplier).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If Supplier.SupplierBankAccount.Count > 0 Then
                                INDGlePrioritizeBankAccount.Enabled = False
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewProvider()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlySupplier.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        Dim errors As New StringBuilder
        If gleTaxCategory.EditValue Is Nothing OrElse CByte(gleTaxCategory.EditValue) = 0 Then
            errors.AppendLine(LayoutControlItem9.Text + ResourceManager.GetString("Empty"))
        End If
        If PermanentRetention AndAlso (SelfWithholding) Then
            errors.AppendLine("El proveedor no puede manejar retención permanente y a la vez se autorretenedor.")
        End If
        If ListSupplierDetailType Is Nothing OrElse ListSupplierDetailType.Count = 0 Then
            errors.AppendLine("No hay detalles de tipo de proveedor.")
        End If
        If listSupplierDistributionLines Is Nothing OrElse listSupplierDistributionLines.Count = 0 Then
            errors.AppendLine("No hay detalles de líneas de distribución.")
        ElseIf INDSleIndependentEmployee.EditValue Then
            If Not listSupplierDistributionLines.Any(Function(d) d.PositionId IsNot Nothing) Then
                errors.AppendLine("El proveedor es empleado Independiente y debe manejar al menos una línea de distribución con un cargo asignado.")
            End If
        ElseIf Not INDSleIndependentEmployee.EditValue Then
            For Each dl In listSupplierDistributionLines
                dl.PositionId = Nothing
            Next
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' funcion para añadir un registro a la rejilla de pronto pago
    ''' </summary>
    Private Sub AddPromptPaymentDiscount()
        Dim errors = ValidateAges(FlagEditPromptPaymentDiscount)
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Dim PromptPaymentDiscount As PromptPaymentDiscount
        If FlagEditPromptPaymentDiscount Then
            PromptPaymentDiscount = TryCast(INDGvPromptPaymentDiscount.GetFocusedRow, PromptPaymentDiscount)
        Else
            PromptPaymentDiscount = New PromptPaymentDiscount
            ListPromptPaymentDiscount.Add(PromptPaymentDiscount)
        End If
        With PromptPaymentDiscount
            .RangeName = INDTxtRangeName.EditValue
            .InitialRank = INDSpeInitialRank.EditValue
            .EndRank = INDSpeEndRank.EditValue
            .DiscountRate = INDSpeDiscountRate.EditValue
        End With
        INDGcPromptPaymentDiscount.RefreshDataSource()
        INDpcePromptPaymentDiscount.ClosePopup()
    End Sub

    ''' <summary>
    ''' Funcion para limpiar el popup de pronto pago
    ''' </summary>
    Private Sub CleanPromptPopup()
        INDTxtRangeName.EditValue = Nothing
        INDSpeInitialRank.EditValue = 0
        INDSpeEndRank.EditValue = 0
        INDSpeDiscountRate.EditValue = 0
        FlagEditPromptPaymentDiscount = False
        INDSpeInitialRank.Enabled = True
    End Sub

    ''' <summary>
    ''' funcion para editar el registro de la rejilla de pronto pago
    ''' </summary>
    Private Sub EditPromptPaymentDiscount()
        Dim _itemSelected As PromptPaymentDiscount = TryCast(INDGvPromptPaymentDiscount.GetFocusedRow(), PromptPaymentDiscount)
        If _itemSelected IsNot Nothing Then
            INDSpeInitialRank.Enabled = False
            With _itemSelected
                INDTxtRangeName.EditValue = .RangeName
                INDSpeInitialRank.EditValue = .InitialRank
                INDSpeEndRank.EditValue = .EndRank
                INDSpeDiscountRate.EditValue = .DiscountRate
            End With
            INDpcePromptPaymentDiscount.ShowPopup()
        Else
            MsgBox("No se ha seleccionado ningún descuento por pronto pago para editar.", MsgBoxStyle.Information, "Editar Descuento por Pronto Pago")
        End If
    End Sub

    ''' <summary>
    ''' funcion para eliminar el registro de la rejilla de pronto pago
    ''' </summary>
    Private Sub RemovePromptPaymentDiscount()
        Dim _itemSelected = TryCast(INDGvPromptPaymentDiscount.GetFocusedRow, PromptPaymentDiscount)
        If _itemSelected.Id > 0 Then
            _itemSelected.MarkAsDeleted()
        End If
        ListPromptPaymentDiscount.Remove(_itemSelected)
        INDGcPromptPaymentDiscount.RefreshDataSource()

    End Sub

    ''' <summary>
    ''' Funcion para validacion de los campos a agregar
    ''' </summary>
    ''' <param name="FlagEditPromptPaymentDiscount"></param>
    ''' <returns></returns>
    Private Function ValidateAges(FlagEditPromptPaymentDiscount) As String
        Dim errors As New StringBuilder
        If INDTxtRangeName.Text = String.Empty Then
            errors.AppendLine(INDLciRangeName.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSpeEndRank.EditValue = 0 Then
            errors.AppendLine(INDLciEndRank.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDSpeInitialRank.EditValue > INDSpeEndRank.EditValue Then
            errors.AppendLine(String.Format("El rango Inicial {0} no puede ser mayor al rango Final {1}", INDSpeInitialRank.EditValue, INDSpeEndRank.EditValue))
        End If
        Dim ValidateList As List(Of PromptPaymentDiscount) = IIf(FlagEditPromptPaymentDiscount, ListPromptPaymentDiscount.Where(Function(x) Not TryCast(INDGvPromptPaymentDiscount.GetFocusedRow, PromptPaymentDiscount).Equals(x)).ToList(), ListPromptPaymentDiscount)
        If ValidateList.Any(Function(g) _
        ((INDSpeInitialRank.EditValue >= g.InitialRank AndAlso INDSpeInitialRank.EditValue <= g.EndRank) OrElse (INDSpeEndRank.EditValue >= g.InitialRank AndAlso INDSpeEndRank.EditValue <= g.EndRank) _
        OrElse (INDSpeInitialRank.EditValue < g.InitialRank AndAlso INDSpeEndRank.EditValue > g.EndRank)) AndAlso g.ChangeTracker.State <> ObjectState.Deleted) Then
            errors.AppendLine($"Los rangos {INDSpeInitialRank.EditValue}-{INDSpeEndRank.EditValue} ya se encuentran parametrizados ")
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Supplier
            .Code = Code
            .Name = NameSupplier
            .IdThirdParty = IdThirdParty
            .TimeLimitDays = TimeLimitDays

            .PermanentRetention = PermanentRetention
            .SelfWithholding = SelfWithholding
            .SelfWithholdingICA = SelfWithholdingICA
            .NotIva = NotIva
            .Manufacturer = Manufacturer
            .Seller = Seller

            .CodeCMMS = CodeCMMS
            .PrioritizeBankAccount = INDGlePrioritizeBankAccount.EditValue
            .Declarant = Declarant
            .IndependentEmployee = INDSleIndependentEmployee.EditValue
            .WebSite = WebSiteSupplier
            .IdCity = IdCity

            .TaxCategory = CByte(gleTaxCategory.EditValue)

            .ConsignmentInventoryCosting = ConsignmentInventoryCosting

            .WorkRent = INDccbeTaxIndentification.Properties.Items(0).CheckState.AsBoolean
            .Pensions = INDccbeTaxIndentification.Properties.Items(1).CheckState.AsBoolean
            .CapitalRent = INDccbeTaxIndentification.Properties.Items(2).CheckState.AsBoolean
            .NotLaborRent = INDccbeTaxIndentification.Properties.Items(3).CheckState.AsBoolean
            .DividendsAndParticipations = INDccbeTaxIndentification.Properties.Items(4).CheckState.AsBoolean

            If Pivote = False AndAlso Supplier.Id > 0 Then
                Supplier.MarkAsModified()
            End If

            If ListDeleteSupplierDetailType IsNot Nothing AndAlso ListDeleteSupplierDetailType.Count > 0 Then
                For Each item As SupplierDetailType In ListDeleteSupplierDetailType
                    item.MarkAsDeleted()
                Next
                For Each item As SupplierDetailType In ListDeleteSupplierDetailType
                    ListSupplierDetailType.Add(item)
                Next
            End If

            If listDeleteSupplierDistributionLines IsNot Nothing AndAlso listDeleteSupplierDistributionLines.Count > 0 Then
                For Each item As SuppliersDistributionLines In listDeleteSupplierDistributionLines
                    item.MarkAsDeleted()
                Next
                For Each item As SuppliersDistributionLines In listDeleteSupplierDistributionLines
                    listSupplierDistributionLines.Add(item)
                Next
            End If

            '*********Datos Contacto
            If Supplier.ThirdParty IsNot Nothing Then
                If Object.Equals(.ThirdParty.Person.Address, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosDireccion.Count - 1
                        ListadoEliminadosDireccion.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .ThirdParty.Person.Address.Add(ListadoEliminadosDireccion.Item(i))
                        If Supplier.Id > 0 Then
                            Supplier.MarkAsModified()
                        End If
                    Next
                    ListadoEliminadosDireccion.Clear()
                End If
                If Object.Equals(.ThirdParty.Person.Phone, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosTelefono.Count - 1
                        ListadoEliminadosTelefono.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .ThirdParty.Person.Phone.Add(ListadoEliminadosTelefono.Item(i))
                        If Supplier.Id > 0 Then
                            Supplier.MarkAsModified()
                        End If
                    Next
                    ListadoEliminadosTelefono.Clear()
                End If
                If Object.Equals(.ThirdParty.Person.Email, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosEmail.Count - 1
                        ListadoEliminadosEmail.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .ThirdParty.Person.Email.Add(ListadoEliminadosEmail.Item(i))
                        If Supplier.Id > 0 Then
                            Supplier.MarkAsModified()
                        End If
                    Next
                    ListadoEliminadosEmail.Clear()
                End If
            End If

            If ListPromptPaymentDiscount.Any Then
                For Each Item In ListPromptPaymentDiscount
                    Supplier.PromptPaymentDiscount.Add(Item)
                Next
            End If

        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para agregar un nuevo elemento a la rejilla de descuento pronto pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddPrompt_Click(sender As Object, e As EventArgs) Handles INDSbAddPrompt.Click
        AddPromptPaymentDiscount()
    End Sub

    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If Supplier.ThirdParty.Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(Supplier.ThirdParty.Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            Supplier.ThirdParty.Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = Supplier.ThirdParty.Person.Address
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If Supplier.ThirdParty.Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(Supplier.ThirdParty.Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            Supplier.ThirdParty.Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = Supplier.ThirdParty.Person.Phone
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If Supplier.ThirdParty.Person.Email.Count > 0 Then
            ListadoEliminadosEmail.Add(Supplier.ThirdParty.Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            Supplier.ThirdParty.Person.Email.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = Supplier.ThirdParty.Person.Email
        End If
    End Sub
    ''' <summary>
    ''' Evento para cambia el tipo Email
    ''' </summary>
    Private Sub CtrContactos1_ChangeEmailType(Type As Byte) Handles CtrContacts.ChangeEmailType
        If Supplier.ThirdParty.Person.Email.Count > 0 Then
            Dim email = Supplier.ThirdParty.Person.Email.ElementAt(CtrContacts.ItemSelecionadoEmail)
            email.Type = Type
            If email.Id > 0 Then
                email.ChangeTracker.State = ObjectState.Modified
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If Supplier.ThirdParty IsNot Nothing Then
            If Supplier.ThirdParty.Person.Address Is Nothing Then
                Supplier.ThirdParty.Person.Address = New Domain.Entities.TrackableCollection(Of Domain.Entities.Address)
            End If
            If Supplier.ThirdParty.Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
                Supplier.ThirdParty.Person.Address.Add(New Domain.Entities.Address With
                                                          {
                                                            .DepartmentId = CtrContacts.DepartmentId,
                                                            .DepartmentName = CtrContacts.DepartmentName,
                                                            .CityId = CtrContacts.CityId,
                                                            .CityName = CtrContacts.CityName,
                                                            .Addresss = CtrContacts.Direccion,
                                                            .Synchronized = "1",
                                                            .State = True
                                                          }
                                                       )
            End If
            CtrContacts.EstablecerDataSourceDireccion = Nothing
            CtrContacts.EstablecerDataSourceDireccion = Supplier.ThirdParty.Person.Address
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If Supplier.ThirdParty IsNot Nothing Then
            If Supplier.ThirdParty.Person.Email Is Nothing Then
                Supplier.ThirdParty.Person.Email = New Domain.Entities.TrackableCollection(Of Email)() ' New Domain.Base.Entities.TrackableCollection(Of Domain.Entities.Email)
            End If
            If Supplier.ThirdParty.Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
                Supplier.ThirdParty.Person.Email.Add(New Domain.Entities.Email With {.Email1 = CtrContacts.Email, .Synchronized = "1", .Type = CtrContacts.EmailType, .State = True})
            End If
            CtrContacts.EstablecerDataSourceEmail = Nothing
            CtrContacts.EstablecerDataSourceEmail = Supplier.ThirdParty.Person.Email
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If Supplier.ThirdParty IsNot Nothing Then
            If Supplier.ThirdParty.Person.Phone Is Nothing Then
                Supplier.ThirdParty.Person.Phone = New Domain.Entities.TrackableCollection(Of Domain.Entities.Phone)
            End If
            If Supplier.ThirdParty.Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
                Supplier.ThirdParty.Person.Phone.Add(New Domain.Entities.Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1"})
            End If
            CtrContacts.EstablecerDataSourceTelefono = Nothing
            CtrContacts.EstablecerDataSourceTelefono = Supplier.ThirdParty.Person.Phone
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewProvider() As Task
        Supplier = New Domain.Entities.Supplier() With {.Status = True}
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
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        Pivote = True
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Supplier IsNot Nothing AndAlso Me.Supplier.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.Supplier.Code) Then
            Try

                If MessageIndigo.Show("Está a punto de actualizar el estado del registro. Tenga en cuenta que cualquier cambio no guardado se perderá. ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    Dim status As Boolean = If(Me.Supplier?.Status, True)
                    BarraBotones.StatusRecord = If(Not status, eActionsStatusRecords.Inactive, eActionsStatusRecords.Active)
                    Return
                End If

                Using model As New MSupplier(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Supplier.Status
                    Dim result As ActionResult(Of Domain.Entities.Supplier) = Await model.ChangeState(Me.Supplier.Code, state)
                    AsyncLoader(False)

                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Dim code As String = result.ObjectEmbbeded?.Code

                        Me.CleanControls()
                        If String.IsNullOrEmpty(code) Then
                            Return
                        End If

                        Me.Code = code
                        Await Me.LoadControls()
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Supplier.Code, Me.Supplier.Name, Me.Supplier.ThirdParty.Nit, Me.Supplier.ThirdParty.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.Supplier.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Supplier.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Supplier.Code, Me.Supplier.Name, Me.Supplier.ThirdParty.Nit, Me.Supplier.ThirdParty.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Supplier.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As Boolean
        If SupplierTypeId = 0 OrElse SupplierTypeId = Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo para agregar tipos de proveedor a la rejilla correspondiente
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddSupplierType()
        If ValidateControlsPopup() = True Then
            Dim ban As Integer = 0
            If ListSupplierDetailType Is Nothing Then
                ListSupplierDetailType = New List(Of SupplierDetailType)
            Else
                ban = ListSupplierDetailType.FindAll(Function(item) item.SupplierTypeId = SupplierTypeId).Cast(Of SupplierDetailType).ToList().Count
            End If
            If ban = 0 Then
                CreateSupplierDetailType()
                ListSupplierDetailType.Add(supplierDetailType)
                If Supplier.Id > 0 Then
                    Supplier.MarkAsModified()
                End If
                INDgcType.DataSource = Nothing
                INDgcType.DataSource = ListSupplierDetailType
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DistributionLinesDetailAgregateSatisfactory")
                SupplierTypeId = Nothing
                INDsleSupplierType.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DistributionLineDetailExist")
                INDsleSupplierType.Focus()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            INDsleSupplierType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Crea el objeto para el listado de detalles de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSupplierDetailType()
        supplierDetailType = New SupplierDetailType
        With supplierDetailType
            .SupplierTypeId = SupplierTypeId
            .SupplierTypeDescription = INDsleSupplierType.Text
        End With
    End Sub

    Private Function ValidateControlsDistributionLinePopup() As String
        Dim listErrors As New StringBuilder
        If INDsleDistributionLineId.EditValue Is Nothing Then
            listErrors.AppendLine(ResourceManager.GetString("SelectedDistributionLine", "Commons"))
            INDsleDistributionLineId.Focus()
        Else
            If listSupplierDistributionLines Is Nothing Then
                listSupplierDistributionLines = New List(Of SuppliersDistributionLines)
            Else
                Dim supplierDistributionLineId As Integer = If(supplierDistributionLine Is Nothing OrElse supplierDistributionLine.Id = 0, -1, supplierDistributionLine.Id)
                If listSupplierDistributionLines.Any(Function(x) x.IdDistributionLine = INDsleDistributionLineId.EditValue AndAlso x.Id <> supplierDistributionLineId) Then
                    listErrors.AppendLine(ResourceManager.GetString("DistributionLineDuplicated", "Commons"))
                    INDsleDistributionLineId.EditValue = Nothing
                    INDsleDistributionLineId.Focus()
                End If
            End If
        End If
        If INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDslePositionId.EditValue Is Nothing Then
                listErrors.AppendLine("Debe seleccionar un cargo para la línea de distribución.")
                If listErrors.Length = 0 Then
                    INDslePositionId.Focus()
                End If
            End If
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Crea la linea de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDistributionLine()
        'Se valida que los controles esten diligenciados
        Dim errors As String = ValidateControlsDistributionLinePopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditDistributionLineMode = False Then 'Si se esta guardando
            supplierDistributionLine = New SuppliersDistributionLines
            Dim distributionLineXpoEntity As Infrastructure.Data.Xpo.CommonRepository.CommonDistibutionLineXpo
            If INDsleDistributionLineId.GetSelectedDataRow() Is Nothing Then
                distributionLineXpoEntity = _presenter.GetDistributionLineById(INDsleDistributionLineId.EditValue)
            Else
                distributionLineXpoEntity = DirectCast(DirectCast(INDsleDistributionLineId.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonDistibutionLineXpo)
            End If
            With distributionLineXpoEntity
                supplierDistributionLine.IdDistributionLine = .Id
                supplierDistributionLine.DistributionLineDescription = .Code + " - " + .Name
                supplierDistributionLine.MainAccountDescription = .IdMainAccount.NumberName
                supplierDistributionLine.Status = True
                If INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    supplierDistributionLine.PositionId = INDslePositionId.EditValue
                    supplierDistributionLine.PositionCodeName = INDslePositionId.Text
                End If
            End With

            listSupplierDistributionLines.Add(supplierDistributionLine)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DistributionLineSatisfactory", "Commons")
        Else
            With supplierDistributionLine
                If INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    supplierDistributionLine.PositionId = INDslePositionId.EditValue
                    supplierDistributionLine.PositionCodeName = INDslePositionId.Text
                End If
            End With

            Mensaje(EeventViewerImages.Informacion) = "Linea de distribución editada correctamente."
        End If

        INDgcLines.DataSource = Nothing
        INDgcLines.DataSource = listSupplierDistributionLines
        CleanControlsDistributionLine()
        INDpceDistributionLine.ShowPopup()
        INDsleDistributionLineId.Focus()
    End Sub

    ''' <summary>
    ''' Edita la linea de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDistributionLine()
        supplierDistributionLine = CType(viewLinesGrid.GetFocusedRow, SuppliersDistributionLines)
        FlagEditDistributionLineMode = True
        With supplierDistributionLine
            INDsleDistributionLineId.EditValue = .IdDistributionLine
            INDsleDistributionLineId.Properties.NullText = supplierDistributionLine.DistributionLineDescription
            INDsleDistributionLineId.Properties.ReadOnly = True
            INDsbDistributionLineAdd.Focus()

            If INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDslePositionId.EditValue = supplierDistributionLine.PositionId
                INDslePositionId.Properties.NullText = supplierDistributionLine.PositionCodeName
                INDslePositionId.Focus()
            End If
        End With
        INDpceDistributionLine.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo que elimina una linea de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDistributionLine()
        'Se obtiene el registro en el cual esta el foco
        Dim info As SuppliersDistributionLines = viewLinesGrid.GetFocusedRow()

        'Si el estado del registro esta en true, se valida que no sea el ultimo activo
        If info.Status AndAlso (From x In listSupplierDistributionLines Where x.Status = True Select x).Count = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El registro no se puede eliminar porque debe haber al menos una linea de distribución activa"
            Exit Sub
        End If

        'Se remueve del listado principal
        listSupplierDistributionLines.Remove(info)
        If info.Id > 0 Then 'Si el registro ya fue guardado se asigna al listado de eliminados
            If listDeleteSupplierDistributionLines Is Nothing Then
                listDeleteSupplierDistributionLines = New List(Of SuppliersDistributionLines)
            End If
            listDeleteSupplierDistributionLines.Add(info)
        End If

        'Se carga el datasource
        INDgcLines.DataSource = Nothing
        INDgcLines.DataSource = listSupplierDistributionLines
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteTypes()
        Dim dld As SupplierDetailType = CType(viewTypes.GetFocusedRow, SupplierDetailType)
        If dld.Id <> 0 Then
            If ListDeleteSupplierDetailType Is Nothing Then
                ListDeleteSupplierDetailType = New List(Of SupplierDetailType)
            End If
            dld.MarkAsDeleted()
            ListDeleteSupplierDetailType.Add(dld)
        End If
        ListSupplierDetailType.Remove(dld)
        INDgcType.DataSource = Nothing
        INDgcType.DataSource = ListSupplierDetailType
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        TypeFile = Nothing
        TaxCategories = Nothing
        listAdress = Nothing
        Pivote = Nothing
        _idOperativeUnit = Nothing
        _presenter = Nothing
        record = Nothing
        Supplier = Nothing
        ListadoEliminadosTelefono = Nothing
        ListadoEliminadosDireccion = Nothing
        ListadoAgregados = Nothing
        ListadoEliminadosEmail = Nothing
        ListSupplierDetailType = Nothing
        listDeleteDistributionLine = Nothing
        listDeleteSupplierDistributionLines = Nothing
        ListDeleteSupplierDetailType = Nothing
        distributionLine = Nothing
        supplierDetailType = Nothing
        supplierDistributionLine = Nothing
        listSupplierDistributionLines = Nothing
        FlagEditDistributionLineMode = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlySupplier, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        indigo = SessionValues.Instance
        IndigoGridControl1.RefreshGrid(INDgcLines)
        IndigoGridControl1.RefreshGrid(INDgcType)
        _presenter = New PSupplier(Me)
        _presenter.Initialize()
        _presenter.GetSequence()
        LoadStatus()
        Deshacer()
        CreateTypeFile()
        LoadTaxCategories()
        LoadState()
        LoadCostingInvConsignment()
        IndigoGridView2.SetListAcction(viewTypes, New List(Of eAcciones)() From {{eAcciones.Remove}})
        IndigoGridView1.SetListAcction(viewLinesGrid, New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}})
    End Sub

#End Region

#Region "Shown"

    'Protected Overrides Async Sub OnShown(e As EventArgs)
    '    Await Me.LayoutControls.LoadDefinitionAsync()
    '    MyBase.OnShown(e)
    'End Sub
    Private Async Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
        Await Me.LayoutControls.LoadDefinitionAsync()
    End Sub

#End Region
#Region "FocusedRowChanged"
    ''' <summary>
    ''' Evento se dispara cuando se selecciona una fila del GridView de Cuentas Bancarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvBankAccount_FocusedRowChanged(sender As Object, e As EventArgs) Handles INDGvBankAccount.FocusedRowChanged
        StartColumnAdjustment()
    End Sub
    ''' <summary>
    ''' Evento se dispara cuando se selecciona una fila del GridView de Descuento por pronto pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvPromptPaymentDiscount_FocusedRowChanged(sender As Object, e As EventArgs) Handles INDGvPromptPaymentDiscount.FocusedRowChanged
        PromptPaymentDiscountColumn()
    End Sub
#End Region
#Region "KeyDown"

    ''' <summary>
    ''' Evento enter del control codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewProvider()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDckRetentionIVA_KeyDown(sender As Object, e As KeyEventArgs) Handles INDckRetentionOptions.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDsleSupplierType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar las teclas de enter y F4
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceContactData_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceContactData.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceContactData.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al control de tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleSupplierType.KeyDown
        If e.KeyCode = Keys.Escape Then
            INDpceDistributionLine.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar la tecla enter en el control de lineas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDistributionLineId_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleDistributionLineId.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDsbDistributionLineAdd.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar la tecla enter en el control de cargos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePositionId_KeyDown(sender As Object, e As KeyEventArgs) Handles INDslePositionId.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDsbDistributionLineAdd.Focus()
        End If
    End Sub

    Private Sub INDsleDeclarant_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleDeclarant.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceBanckAccount.Focus()
            INDPceBanckAccount.ShowPopup()
            INDsleBankEntityAccount.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Elimina el registro bloqueado al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSupplier_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Evento que se utiliza para abrir el formulario Lineas De Distribución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDistributionLineId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDistributionLineId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("584", Nothing, True)
        End If
    End Sub

    Private Sub INDsleBankEntityAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBankEntityAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("507", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de lineas de distribución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDistributionLine_OpenFormButtonClick(sender As Object, e As EventArgs)
        OpenForm(584, Nothing, True)
        _presenter.Initialize()
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de ciudad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmCity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.ShowDialog(Me)
            End Using
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.ShowDialog(Me)
            End Using
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de lineas de distribucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDistributionLine_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("584", Nothing, True)
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("726", "", True)
            _presenter.InitializeSupplierType()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDRptRgPaymentDefault_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptRgPaymentDefault.EditValueChanged
        Dim supplierBankAccount = DirectCast(INDGvBankAccount.GetFocusedRow(), Domain.Entities.SupplierBankAccount)
        If supplierBankAccount Is Nothing Then
            Exit Sub
        End If
        For Each item In Supplier.SupplierBankAccount
            item.PaymentDefault = False
        Next
        supplierBankAccount.PaymentDefault = True
        INDGcBanckAccount.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleThirdParty.EditValueChanged
        If Pivote = True Then
            If Not IdThirdParty = 0 Then
                Dim third As Domain.Entities.ThirdParty
                Using modelThird As New MSupplier(Tag)
                    third = modelThird.GetThirdPartyById(INDsleThirdParty.EditValue)
                End Using
                If Not third Is Nothing Then
                    INDtxtName.Text = third.Name
                    Supplier.ThirdParty = third
                    CtrContacts.EstablecerDataSourceDireccion = Nothing
                    CtrContacts.EstablecerDataSourceDireccion = third.Person.Address.ToList
                    CtrContacts.EstablecerDataSourceTelefono = Nothing
                    CtrContacts.EstablecerDataSourceTelefono = third.Person.Phone.ToList
                    CtrContacts.EstablecerDataSourceEmail = Nothing
                    CtrContacts.EstablecerDataSourceEmail = third.Person.Email.ToList
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleThirdParty.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Using modelThird As New MSupplier(Tag)
                Dim _supplier As Domain.Entities.Supplier = modelThird.GetSupplierByIdThirdPartyWithThirdAdded(e.NewValue)
                If _supplier IsNot Nothing AndAlso _supplier.Id > 0 AndAlso _supplier.Id <> Supplier.Id Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("Este tercero ya está asociado al proveedor {0}, por favor seleccione otro", String.Concat(_supplier.Code, " - ", _supplier.Name))
                    e.Cancel = True
                End If
            End Using
        End If
    End Sub

    Private Sub CtrSupplierType1_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierType.EditValueChanged
        If SupplierTypeId <> 0 Then
            INDsleSupplierType.ValidateSupplierType()
        End If
    End Sub

    Private Sub INDSleIndependentEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleIndependentEmployee.EditValueChanged
        If INDSleIndependentEmployee.EditValue Then
            INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            colLinesGridPosition.Visible = True
            colLinesGridPosition.VisibleIndex = 2
            colLinesGridStatus.VisibleIndex = 3
        Else
            INDlciPositionId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            colLinesGridPosition.Visible = False
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckStatus_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckStatus.EditValueChanging
        If e IsNot Nothing Then 'Si no arroja error

            'Se valida que hayan registros en la rejilla
            If listSupplierDistributionLines Is Nothing OrElse listSupplierDistributionLines.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos en la rejilla de lineas de distribución"
                Exit Sub
            End If

            'Se valida que al menos haya una linea activa
            If ((From x In listSupplierDistributionLines Where x.Status = True Select x).Count = 0 OrElse (From x In listSupplierDistributionLines Where x.Status = True Select x).Count = 1) AndAlso e.NewValue = False Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "Debe haber al menos una línea de distribución activa"
                Exit Sub
            End If

            'Se obtiene el registro en el cual esta el foco
            Dim info As SuppliersDistributionLines = viewLinesGrid.GetFocusedRow()
            If info IsNot Nothing Then 'Valido que traiga datos
                info.Status = e.NewValue
                info.MarkAsModified()
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors As New StringBuilder
        If INDsleBankEntityAccount.EditValue Is Nothing Then
            errors.AppendLine("Banco Vacio")
        End If
        If INDgleType.EditValue Is Nothing Then
            errors.AppendLine("Tipo Vacio")
        End If
        If INDtxtNumber.EditValue Is Nothing Then
            errors.AppendLine("Numero Vacio")
        End If
        If INDGlePaymentDefault.EditValue Is Nothing Then
            errors.AppendLine("Por Defecto Vacio")
        End If
        If CurrencyId Is Nothing Then
            errors.AppendLine("Moneda Vacia")
        End If
        If INDGlePrioritizeBankAccount.EditValue = True Then
            Dim bankAccountAdded = Supplier.SupplierBankAccount.Where(Function(x) x.BankId = INDsleBankEntityAccount.EditValue).FirstOrDefault()
            If bankAccountAdded IsNot Nothing Then
                errors.AppendLine("El parametro para priorizar pago esta activo y solo se puede agregar una cuenta del mismo banco")
            End If
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If
        Dim supplierBankAccount As New Domain.Entities.SupplierBankAccount
        With supplierBankAccount
            .BankId = INDsleBankEntityAccount.EditValue
            .CodeNameBank = INDsleBankEntityAccount.Text
            .Type = INDgleType.EditValue
            .Number = INDtxtNumber.EditValue
            .PaymentDefault = INDGlePaymentDefault.EditValue
            .CurrencyId = Me.CurrencyId
            .CurrencyAbbreviation = Me.INDsleCurrency.Text
            .State = 1
        End With
        If supplierBankAccount.PaymentDefault Then
            For Each item In Supplier.SupplierBankAccount
                item.PaymentDefault = False
            Next
        End If
        Supplier.SupplierBankAccount.Add(supplierBankAccount)
        INDGlePrioritizeBankAccount.Enabled = False
        INDGcBanckAccount.DataSource = Nothing
        INDGcBanckAccount.DataSource = Supplier.SupplierBankAccount
        CleanControlsBankAccount()
        INDsleBankEntityAccount.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddType_Click(sender As Object, e As EventArgs) Handles INDbtnAddType.Click
        AddSupplierType()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddLine_Click(sender As Object, e As EventArgs) Handles INDsbDistributionLineAdd.Click
        AddDistributionLine()
    End Sub

#End Region

#Region "MenuContextual"
    ''' <summary>
    ''' Menu contextual y acciones de rejilla Cuentas Bancarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions, IndigoGridView3.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Activate"
                ActivateSupplierAccount()
            Case "Inactivate"
                InactiveSupplierAccount()
            Case "Remove"
                DeleteSupplierAccount()
        End Select
    End Sub
    ''' <summary>
    ''' Menu de las lineas de distribucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditDistributionLine()
            Case "Remove"
                DeleteDistributionLine()
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        DeleteTypes()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        DeleteTypes()
    End Sub

    ''' <summary>
    ''' menu contextual y col acciones de rejilla pronto pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView4.ContexMenuActions, IndigoGridView4.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditPromptPaymentDiscount()
            Case "Remove"
                RemovePromptPaymentDiscount()
        End Select
    End Sub
#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceContactData_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceContactData.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            INDtxtWebSite.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDistributionLine_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDistributionLine.CloseUp
        If FlagEditDistributionLineMode = True Then
            CleanControlsDistributionLine()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleBankEntityAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBankEntityAccount.QueryPopUp
        If BankDatasource Is Nothing Then
            _presenter.InitializeBank()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_QueryPopUp(sender As Object, e As CancelEventArgs)
        If SupplierTypeXpo Is Nothing Then
            _presenter.InitializeSupplierType()
        End If
    End Sub

    Private Sub CtrSupplierType1_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplierType.QueryPopUp
        If SupplierTypeXpo Is Nothing Then
            _presenter.InitializeSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' evento que carga el datasource de la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If CurrencyXpo Is Nothing Then
            _presenter.InitializeCurrency()
        End If
    End Sub

#End Region

#End Region

#Region "Barra Botones"

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
        mode = True
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        mode = False
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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

#End Region

#Region "ClosePopup"
    ''' <summary>
    ''' evento que limpia el popup de pronto pago cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpcePromptPaymentDiscount_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpcePromptPaymentDiscount.CloseUp
        CleanPromptPopup()
    End Sub

    Private Sub INDsleBankEntityAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBankEntityAccount.EditValueChanged
        If INDsleBankEntityAccount.EditValue IsNot Nothing Then
            Using ModelBank As New Presentation.Common.MVP.MBank(Me.Tag)
                Dim bank As Domain.Entities.Bank = ModelBank.GetBankById(INDsleBankEntityAccount.EditValue)
                Select Case bank.BankAccountRegistration
                    Case 0
                        INDtxtNumber.Properties.Mask.EditMask = "[0-9]+"
                        INDtxtNumber.Properties.Mask.MaskType = Mask.MaskType.RegEx
                    Case 1
                        INDtxtNumber.Properties.Mask.EditMask = ""
                        INDtxtNumber.Properties.Mask.MaskType = Mask.MaskType.None
                End Select
                INDtxtNumber.Properties.MaxLength = 22
            End Using
        End If
    End Sub

#End Region

End Class