'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2014
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
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports System.Windows.Forms
Imports System.Text
Imports Presentation.Controls.MVP
Imports Domain.Entities.Service
Imports DevExpress
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports System.Drawing
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.Spreadsheet
#End Region

Public Class FrmAccountsPayable
    Implements IAccountsPayable, ICustomizableForm

#Region "Constant"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim NatureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Bandera para saber si entra al editvaluechanged del control de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSupplier As Boolean = False

    ''' <summary>
    ''' Representa el id de la cuenta del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idMainAccountSupplier As Integer

    ''' <summary>
    ''' Representa el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idSupplier As Integer

    ''' <summary>
    ''' identifica si el proveedor es empleado independiente
    ''' </summary>
    Dim _independentEmployee As Boolean

    ''' <summary>
    ''' Representa el id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThirdParty As Integer
    ''' <summary>
    ''' Representa el tipo de contribuyentes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThirdPartyContributionType As Integer

    ''' <summary>
    ''' Representa el id del cargo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idPosition As Integer?

    ''' <summary>
    ''' Variable que representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAccountPayable

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Listado de causaciones diferidas de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public ListDeferredCausation As List(Of DeferredCausation)

    ''' <summary>
    ''' variable para la propiedad de causaciones diferidas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listAddDC As List(Of DeferredCausation)

    ''' <summary>
    ''' Listado para agregar una factura a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public ListAddBill As List(Of AccountPayable)

    ''' <summary>
    ''' Listado de facturas eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteBill As List(Of AccountPayable)

    ''' <summary>
    ''' Listado para agregar un concepto a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public ListAddConcept As List(Of AccountPayableDetailConcept)

    ''' <summary>
    ''' Variable que contiene la entidad de dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountPayable As AccountPayable

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Contiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Dim dateServerVariable As DateTime

    ''' <summary>
    ''' Permite saber si se guarda o se actualiza
    ''' </summary>
    ''' <remarks></remarks>
    Dim banRegister As Boolean

    ''' <summary>
    ''' Permite saber si guarda y confirma
    ''' </summary>
    ''' <remarks></remarks>
    Dim banGuardarConfirmar As Boolean

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Listado de detalles eliminados de la causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private _listDeleteDeferredCausationDetail As List(Of DeferredCausationDetails)

    ''' <summary>
    ''' Listado de eliminados de la causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private _listDeleteDeferredCausation As List(Of DeferredCausation)

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

    ''' <summary>
    ''' Codigo para el documento indexado
    ''' </summary>
    ''' <remarks></remarks>
    Private codeDocumentIndexed As String = String.Empty

    ''' <summary>
    ''' Bandera para el documento indexado
    ''' </summary>
    ''' <remarks></remarks>
    Private banCodeIndexed As Boolean

    ''' <summary>
    ''' Modo guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private modeSaveAndConfirm As Boolean = False

    ''' <summary>
    ''' bandera que define si el tercero esta marcado como facturador electronico
    ''' </summary>
    Private _thirdPartyElectronicBiller As Boolean = False

    ''' <summary>
    ''' maneja documento soporte
    ''' </summary>
    ''' <returns></returns>
    Private Property _handlesDocumentSupport As Boolean?

    ''' <summary>
    ''' maneja iva descontable
    ''' </summary>
    ''' <returns></returns>
    Private Property _hasDeductibleIva As Boolean?


    ''' <summary>
    ''' tipo de iva parametrizado
    ''' </summary>
    ''' <returns></returns>
    Private Property _TaxRegistration As Integer?

    ''' <summary>
    ''' Variable para saber si tiene permiso para confirmar 
    ''' </summary>
    ''' <remarks></remarks>
    Dim keyConfirmation

    ''' <summary>
    ''' Variable para saber si tiene permiso para causar
    ''' </summary>
    ''' <remarks></remarks>
    Dim keyCausation

    ''' <summary>
    ''' Listado de conceptos de pago de tipo retencion que trae la linea de distribucion asociada al proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim listConceptCxpXpo As List(Of Object)

    ''' <summary>
    ''' Permite saber si el proveedor es declarante (1=Declarante, 2=No Declarante)
    ''' </summary>
    ''' <remarks></remarks>
    Dim supplierDeclarant As Integer

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Permite saber si la cuenta por pagar esta en un traslado
    ''' se hace esto porque cuando le asignamos el id a la unidad de radicacion
    ''' me blanquea el nulltext y cuando el usuario no tiene permiso a esa unidad de radicacion
    ''' aparece en blanco
    ''' </summary>
    ''' <remarks></remarks>
    Dim IfTransferContainsAccountPayable As Integer

    ''' <summary>
    ''' Establece la unidad de radicacion al momento de eliminar una factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim PivotFilingUnit As Integer

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Dim PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo
    ''' <summary>
    ''' Se almacena el Id de la tabla accountPayable
    ''' </summary>
    Public IdAccountPayable As Integer
    ''' <summary>
    ''' Bandera para diferenciar cuando se importa a cuando se copia y pega a rejilla
    ''' </summary>
    Public importFlag As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre de la linea de distribucion escogida por el usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DistributionLineText As String Implements IAccountsPayable.DistributionLineText
        Get
            Return INDtxtDistributionLine.Text
        End Get
        Set(value As String)
            INDtxtDistributionLine.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre de la posicion asociada a la linea de distribucion escogida por el usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PositionText As String Implements IAccountsPayable.PositionText
        Get
            Return INDtxtPosition.Text
        End Get
        Set(value As String)
            INDtxtPosition.Text = value
            INDlyItemPosition.Visibility = If(value = String.Empty, XtraLayout.Utils.LayoutVisibility.Never, XtraLayout.Utils.LayoutVisibility.Always)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del periodo de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServicePeriodDate As Date? Implements IAccountsPayable.ServicePeriodDate
        Get
            Return INDdteServicePeriodDate.EditValue
        End Get
        Set(value As Date?)
            INDdteServicePeriodDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitId As Integer? Implements IAccountsPayable.FilingUnitId
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
    Public Property FilingUnitXpo As List(Of FilingUnit) Implements IAccountsPayable.FilingUnitXpo
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
    Public Property SupplierTypeId As Integer? Implements IAccountsPayable.SupplierTypeId
        Get
            Return INDsleSupplierType.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource el tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeXpo As List(Of SupplierType) Implements IAccountsPayable.SupplierTypeXpo
        Get
            Return INDsleSupplierType.Properties.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDsleSupplierType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el proveedor con sus lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesXpo As Xpo.XPInstantFeedbackSource Implements IAccountsPayable.SuppliersDistributionLinesXpo
        Get
            Return CType(INDgleProvider.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As Xpo.XPInstantFeedbackSource)
            INDgleProvider.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de eliminados de los detalles de las causaciones diferidas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property listDeleteDeferredCausationDetail As List(Of DeferredCausationDetails)
        Get
            Return _listDeleteDeferredCausationDetail
        End Get
        Set(value As List(Of DeferredCausationDetails))
            _listDeleteDeferredCausationDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de eliminados de causaciones diferidas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property listDeleteDeferredCausation As List(Of DeferredCausation)
        Get
            Return _listDeleteDeferredCausation
        End Get
        Set(value As List(Of DeferredCausation))
            _listDeleteDeferredCausation = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estbalece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplier As Integer? Implements IAccountsPayable.IdSupplier
        Get
            Return INDgleProvider.EditValue
        End Get
        Set(value As Integer?)
            INDgleProvider.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Consecutive As String Implements IAccountsPayable.Consecutive
        Get
            Return INDtxtConsecutive.Text
        End Get
        Set(value As String)
            INDtxtConsecutive.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateDocument As Date? Implements IAccountsPayable.DateDocument
        Get
            Return CDate(INDdteDateDocument.EditValue)
        End Get
        Set(value As Date?)
            INDdteDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IAccountsPayable.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PaymentsSecuence Implements IAccountsPayable.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As PaymentsSecuence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequense.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Estado de la barra de botones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IAccountsPayable.Status
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

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeAccountPayable As String Implements IAccountsPayable.CodeAccountPayable
        Get
            If (INDtxtConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDtxtConsecutive.Text
            End If
        End Get
        Set(value As String)
            INDtxtConsecutive.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAccountsPayable.SupplierXpo
        Get
            Return CType(INDgleProvider.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleProvider.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de cuentas del repositorio de la rejilla de causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return RepositoryItemSearchLookUpEditMainAccountDC.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            RepositoryItemSearchLookUpEditMainAccountDC.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de centros de costo de la rejilla de causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return RepositoryItemSearchLookUpEditCostCenterDC.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            RepositoryItemSearchLookUpEditCostCenterDC.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado que viene de la BD de causaciones diferidas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _listAddDeferredCausation As List(Of DeferredCausation) Implements IAccountsPayable._listAddDeferredCausation
        Get
            Return _listAddDC
        End Get
        Set(value As List(Of DeferredCausation))
            _listAddDC = value
        End Set
    End Property

    Public Property Supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub LoadPaymentsSetting()
        'Se obtiene los parámetros de pagos por unidad operativa
        If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
            PaymentsSettingPaymentsXpo = Presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de tipo de proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeSupplierType() As Task
        Using model As New MSupplierType(Tag)
            Dim x As ActionResult(Of List(Of SupplierType)) = Await model.GetSupplierTypeBySupplierId(_idSupplier)
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
    ''' Metodo para asignar los conceptos de pago de tipo retencion
    ''' que tiene la linea de distribucion del proveedor seleccionado a un listado
    ''' para agregarlos a los conceptos del detalle de la cxp
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <remarks></remarks>
    Private Sub GetListRetentionConcept(ByVal supplierDistributionLineId As Integer)
        Using model As New MBusqueda
            Dim _supplierDistributionLine = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLinesById, supplierDistributionLineId)
            Dim Supplier = Presenter.GetSupplierById(_idSupplier)
            If _supplierDistributionLine IsNot Nothing Then
                'Instanciamos el listado que se va a enviar al form modal para agregar los conceptos de tipo retencion
                listConceptCxpXpo = New List(Of Object)

                'Asignamos al listado que se envia al form modal los conceptos de retencion que tiene amarrado la linea de distribucion(DistributionLineDetail)
                If _supplierDistributionLine(0) IsNot Nothing AndAlso _supplierDistributionLine(0).IdDistributionLine IsNot Nothing AndAlso _supplierDistributionLine(0).IdDistributionLine.CommonDistibutionLineDetailXpo IsNot Nothing AndAlso
                _supplierDistributionLine(0).IdDistributionLine.CommonDistibutionLineDetailXpo.Count > 0 Then
                    For Each item In _supplierDistributionLine(0).IdDistributionLine.CommonDistibutionLineDetailXpo
                        'bandera para saber si se puede agregar el conceptos de retenciones a mostrar
                        Dim canAdd = True
                        If item.ConceptType = supplierDeclarant Then
                            'si el tercero tiene responsabilidad fiscal Gran contribuyente 
                            If Supplier.IdThirdParty.CommonThirdPartyFiscalResponsibilityXpoCollection.Any(Function(x) x.FiscalResponsibilityId.Code = "O-13") Then
                                'si la cuenta del concepto esta marcada como reteiva no se agrega ya que por el tipo de responsabilidad fiscal no se deberian listar
                                If DirectCast(item, Infrastructure.Data.Xpo.CommonRepository.CommonDistibutionLineDetailXpo)?.AccountPayableConceptId?.IdAccount?.RetencionType = 2 Then
                                    canAdd = False
                                End If
                            End If
                            'si el tercero tiene responsabilidad fiscal Autorretenedor 
                            If Supplier.IdThirdParty.CommonThirdPartyFiscalResponsibilityXpoCollection.Any(Function(x) x.FiscalResponsibilityId.Code = "O-15") Then
                                'si la cuenta del concepto esta marcada como retefuente no se agrega 
                                If DirectCast(item, Infrastructure.Data.Xpo.CommonRepository.CommonDistibutionLineDetailXpo)?.AccountPayableConceptId?.IdAccount?.RetencionType = 1 Then
                                    canAdd = False
                                End If
                            End If
                            'dependiendo del estado de la bandera se valida si ese conceopto esta apto para mostrarse o no
                            If canAdd Then
                                listConceptCxpXpo.Add(item.AccountPayableConceptId)
                            End If
                        End If
                    Next
                End If

                If BarraBotones.OperatingUnit Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No tiene seleccionada una unidad operativa"
                    Exit Sub
                End If
                'Se valida que se haya escogido la unidad operativa
                If BarraBotones.OperatingUnit.Id = 0 Then
                    Exit Sub
                End If

                'Asignamos al listado que se envia al form modal los conceptos de retencion que tiene amarrado la linea de distribucion(DistributionLineICARetention)
                If _supplierDistributionLine(0) IsNot Nothing AndAlso _supplierDistributionLine(0).IdDistributionLine IsNot Nothing AndAlso _supplierDistributionLine(0).IdDistributionLine.CommonDistributionLinesICARetentionXpo IsNot Nothing AndAlso
                _supplierDistributionLine(0).IdDistributionLine.CommonDistributionLinesICARetentionXpo.Count > 0 Then
                    For Each item In _supplierDistributionLine(0).IdDistributionLine.CommonDistributionLinesICARetentionXpo
                        If item.OperatingUnitId = BarraBotones.OperatingUnit.Id Then
                            listConceptCxpXpo.Add(item.AccountPayableConceptId)
                        End If
                    Next
                End If

            End If

        End Using
    End Sub

    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub CreateNature()
        NatureType = New List(Of Tuple(Of Integer, String))
        NatureType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDrepSleNature.DataSource = NatureType.ToList()
    End Sub

    ''' <summary>
    ''' Edita la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditBill()
        Dim acc As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        OpenAddBill(acc)
    End Sub

    ''' <summary>
    ''' Genera los mensajes de error.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <remarks></remarks>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar las facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteBill()
        Dim bill As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        DeleteDeferredCausation(bill.BillNumber, True)
        If bill.Id <> 0 Then
            If listDeleteBill Is Nothing Then
                listDeleteBill = New List(Of AccountPayable)
            End If
            While bill.AccountPayableDetailConcept.Count > 0

                If bill.AccountPayableDetailConcept.Item(0).AccountPayableDetailConceptLiquidation IsNot Nothing AndAlso bill.AccountPayableDetailConcept.Item(0).AccountPayableDetailConceptLiquidation.Count > 0 Then
                    bill.AccountPayableDetailConcept.Item(0).AccountPayableDetailConceptLiquidation(0).MarkAsDeleted()
                End If

                bill.AccountPayableDetailConcept.Item(0).MarkAsDeleted()
            End While
            While bill.AccountPayableShares.Count > 0
                bill.AccountPayableShares.Item(0).MarkAsDeleted()
            End While
            If IfTransferContainsAccountPayable > 0 Then
                If bill.FilingUnitId <> Nothing Then
                    PivotFilingUnit = bill.FilingUnitId
                End If
            End If
            bill.MarkAsDeleted()
            listDeleteBill.Add(bill)
        End If
        ListAddBill.Remove(bill)
        INDgcBills.DataSource = Nothing
        INDgcBills.DataSource = ListAddBill
        If ListAddBill Is Nothing OrElse ListAddBill.Count = 0 Then
            INDgleProvider.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga los combos de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRepositorySearch()
        Dim model As New MBusqueda
        Dim filter() As Object = {5, True}
        RepositoryItemSearchLookUpEditAccount.DataSource = model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        RepositoryItemSearchLookUpEditCostCenter.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True)
        CreateNature()
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        'Valida si el listado de facturas es vacio
        If ListAddBill Is Nothing OrElse ListAddBill.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmAccountsPayable_ListEmpty", NAME_MODULE)
            Return False
        End If

        'Valida si el valor de alguna factura es menor a 0
        Dim listError As New StringBuilder
        For Each item As AccountPayable In ListAddBill
            If item.Value < 0 Then
                listError.AppendLine(String.Format(ResourceManager.GetString("FrmAccountsPayable_ValueError", NAME_MODULE), item.BillNumber))
            End If
        Next
        If listError.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listError.ToString
            Return False
        End If
        'Valida si el valor de la factura es igual a 0
        If banGuardarConfirmar = True Then
            listError = New StringBuilder
            For Each item As AccountPayable In ListAddBill
                If item.Value = 0 Then
                    listError.AppendLine(String.Format(ResourceManager.GetString("FrmAccountsPayable_ValueEmpty", NAME_MODULE), item.BillNumber))
                End If
            Next
            If listError.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listError.ToString
                Return False
            End If
        End If
        'Valida si hay causacion diferida y si hay que hayan diferido todos los items
        If ListDeferredCausation IsNot Nothing Then
            Dim dontDeferred As Boolean = False
            For Each itemDeferredCausation As DeferredCausation In ListDeferredCausation
                If (itemDeferredCausation.DeferredCausationDetails.Count = 0 OrElse itemDeferredCausation.DeferredCausationDetails Is Nothing) _
                    AndAlso itemDeferredCausation.ChangeTracker.State <> ObjectState.Deleted Then
                    dontDeferred = True
                    Exit For
                End If
            Next
            If dontDeferred = True Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmAccountsPayable_EmptyDeferredCausation", NAME_MODULE)
                Return False
            End If
        End If
        'Valida los detalle de las facturas si el usuario tiene permiso de causacion
        If keyCausation > 0 AndAlso banGuardarConfirmar = True Then
            Dim contError = ListAddBill.FindAll(Function(item) item.AccountPayableDetailConcept Is Nothing OrElse item.AccountPayableDetailConcept.Count = 0).Count
            If contError > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DetailNothing", NAME_MODULE)
                Return False
            End If
        End If
        If BarraBotones.OperatingUnit.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una Unidad Operativa."
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que llena la entidad con los valores de los controles
    ''' </summary>
    Private Sub AssigningValues()
        For Each item As AccountPayable In ListAddBill
            item.IdSupplier = _idSupplier
            item.IdSuppliersDistributionLines = INDgleProvider.EditValue
            item.IdThirdParty = _idThirdParty
            item.PositionId = _idPosition
            item.DocumentDate = DateDocument
            item.ServicePeriodDate = ServicePeriodDate
            item.IndigoCompanyNit = Me.indigo.IndigoCompanyNit
            If IfTransferContainsAccountPayable = 0 Then
                item.FilingUnitId = FilingUnitId
            ElseIf IfTransferContainsAccountPayable > 0 AndAlso PivotFilingUnit > 0 Then
                item.FilingUnitId = PivotFilingUnit
            Else
                item.FilingUnitId = FilingUnitId
            End If
            item.SupplierTypeId = SupplierTypeId
            item.IdOperatingUnit = BarraBotones.OperatingUnit.Id
            If INDtxtConsecutive.Text <> "Nuevo" Then
                item.Code = INDtxtConsecutive.Text
            End If
            If item.TaxRegistration Is Nothing Or item.TaxRegistration = 0 Then
                item.TaxRegistration = _TaxRegistration
            End If
            item.Status = 1
        Next
        If listDeleteBill IsNot Nothing Then
            If listDeleteBill.Count <> 0 Then
                For Each item As AccountPayable In listDeleteBill
                    ListAddBill.Add(item)
                Next
            End If
        End If

        If listDeleteDeferredCausation IsNot Nothing Then
            For Each itemDelete As DeferredCausation In listDeleteDeferredCausation
                ListDeferredCausation.Add(itemDelete)
            Next
        End If

        If listDeleteDeferredCausation Is Nothing Then
            If listDeleteDeferredCausationDetail IsNot Nothing AndAlso listDeleteDeferredCausationDetail.Count > 0 Then
                For Each itemDeferred As DeferredCausation In ListDeferredCausation
                    For Each itemDelete As DeferredCausationDetails In listDeleteDeferredCausationDetail
                        If itemDeferred.Id = itemDelete.IdDeferredCausation Then
                            itemDelete.MarkAsDeleted()
                            itemDeferred.DeferredCausationDetails.Add(itemDelete)
                        End If
                    Next
                Next
            End If
        End If

    End Sub

    ''' <summary>
    ''' desbloquea el registro
    ''' </summary>
    Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayments(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que asigna el mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetDate()
        Using model As New MAccountPayable(CStr(Tag))
            dateServerVariable = Await model.GetServerDate()
            INDdteDateDocument.EditValue = dateServerVariable
            INDdteDateDocument.Properties.MaxValue = dateServerVariable
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de establecer las acciones de la rejilla de "Facturas" segun el estado del registro
    ''' </summary>
    Private Sub SetListActions()
        If ListAddBill Is Nothing Then
            IndigoGridView1.MoreInfoColunmns(viewBill)
            IndigoGridView1.SetListAcction(viewBill, {eAcciones.Remove, eAcciones.Edit}.ToList())
            IndigoGridView2.SetListAcction(viewDeferredCausation, {eAcciones.Defer}.ToList())
        Else
            IndigoGridView1.SetListAcction(viewBill, {eAcciones.View}.ToList())
        End If
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlycAccountPayable.BeginUpdate()

        ReadOnlyControls(False)
        ActionsOnControls = False
        Consecutive = String.Empty
        IdSupplier = Nothing
        INDgleProvider.Properties.NullText = String.Empty
        DistributionLineText = String.Empty
        PositionText = String.Empty
        DateDocument = Nothing
        ServicePeriodDate = Nothing
        FilingUnitId = Nothing
        INDsleFilingUnit.Properties.ReadOnly = False
        IfTransferContainsAccountPayable = 0
        PivotFilingUnit = 0
        INDsleFilingUnit.Properties.NullText = String.Empty
        SupplierTypeId = Nothing
        INDsleSupplierType.Properties.NullText = String.Empty
        ListAddBill = Nothing
        SetListActions()
        listDeleteBill = Nothing
        ListDeferredCausation = Nothing
        listDeleteDeferredCausation = Nothing
        INDgcBills.DataSource = Nothing
        ListAddConcept = Nothing
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        banGuardarConfirmar = False
        BarraBotones.CleanAuditBasic()
        BarraBotones.CleanStatusBar()
        Me.BarraBotones.ReassignOperatingUnit()
        ListDeferredCausation = Nothing
        INDgcDeferredCausation.DataSource = Nothing
        listDeleteDeferredCausation = Nothing
        listDeleteDeferredCausationDetail = Nothing
        _idMainAccountSupplier = Nothing
        _idSupplier = Nothing
        _independentEmployee = False
        SupplierTypeXpo = Nothing
        _TaxRegistration = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycAccountPayable.EndUpdate()
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
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAccountsPayable.ActionsOnControls
        Set(value As Boolean)
            INDlycAccountPayable.BeginUpdate()
            INDtxtConsecutive.Enabled = Not value
            INDgleProvider.Enabled = value
            INDtxtDistributionLine.Enabled = value
            INDdteDateDocument.Enabled = value
            INDbtnAddBill.Enabled = value
            INDgcBills.Enabled = value
            INDdteServicePeriodDate.Enabled = value
            INDsleFilingUnit.Enabled = value
            INDsleSupplierType.Enabled = value
            INDgcDeferredCausation.Enabled = value
            INDlycAccountPayable.EndUpdate()
            If value Then
                INDtxtDistributionLine.Properties.ReadOnly = True
                INDgleProvider.Focus()
            Else
                INDtxtConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para abrir el formulario para agregar la factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub OpenAddBill(account As AccountPayable)
        If _idSupplier <> 0 Then
            Me.Cursor = ChangeCursorIndigo()
            ''Validacion para que envie y habilite en al popup de facturas el campo de iva deductible en caso de que el proveedor sea persona natural y no sea responsable de iva 
            If _TaxRegistration Is Nothing Then
                Using model As New MCompanySettings(Tag)
                    Dim _companySettings = Await model.GetCompanySettings()
                    If _companySettings Is Nothing Then
                        Mensaje(EeventViewerImages.MensajeError) = "No se encontró campo parametrizado de Registro IVA"
                        Exit Sub
                    End If
                    _TaxRegistration = _companySettings.TaxRegistration
                End Using
            End If

            Dim showDeductibleIva As Boolean = False
            If account IsNot Nothing AndAlso account.Status > 1 Then
                _hasDeductibleIva = account.DeductibleIva
            Else
                Select Case _TaxRegistration
                    Case 1, 4 'IVA al costo (CONTROL FISCAL) O IVA al costo
                        showDeductibleIva = False
                        'si el parametro de iva *descontable* de la cxp tiene valor y este esta como true entonces es porque esta diferente con el del parametro general que estaria al costo
                        If account?.DeductibleIva IsNot Nothing AndAlso account?.DeductibleIva Then
                            Mensaje(EeventViewerImages.Advertencia) = "El parámetro de Registro IVA actual no coincide con el que previamente se hizo la factura. Por lo anterior, el iva descontable ha sido modificado"
                        End If
                        _hasDeductibleIva = False
                    Case 2 'IVA descontable
                        showDeductibleIva = False
                        If account?.DeductibleIva IsNot Nothing AndAlso Not account?.DeductibleIva Then
                            Mensaje(EeventViewerImages.Advertencia) = "El parámetro de Registro IVA actual no coincide con el que previamente se hizo la factura. Por lo anterior, el iva descontable ha sido modificado"
                        End If
                        _hasDeductibleIva = True
                    Case 3 'IVA Mixto
                        showDeductibleIva = True
                        If account?.DeductibleIva IsNot Nothing Then
                            _hasDeductibleIva = account?.DeductibleIva
                        Else
                            _hasDeductibleIva = Nothing
                        End If
                    Case Else
                        Mensaje(EeventViewerImages.MensajeError) = "No se encontró campo parametrizado de Registro IVA"
                        Exit Sub
                End Select
            End If

            Using Formulario As New FrmPopupBills(listConceptCxpXpo)
                Formulario.DocumentDate = DateDocument
                Formulario.ListDeferredCausation = ListDeferredCausation
                Formulario.IdSupplier = _idSupplier
                Formulario.ThirdPartyId = _idThirdParty
                Formulario.ThirdPartyIdContributionType = _idThirdPartyContributionType
                Formulario.PaymentsSettingPaymentsXpo = PaymentsSettingPaymentsXpo
                Formulario.IndependentEmployee = (_idPosition IsNot Nothing)
                Formulario.IdMainAccountSupplier = _idMainAccountSupplier
                Formulario.DocumentDateCxP = INDdteDateDocument.EditValue
                Formulario.accountPayableStatus = CByte(BarraBotones.StatusRecord)
                Formulario.accountPayableCode = Consecutive
                Formulario.CreationDate = If(account Is Nothing OrElse account.Id = 0, Date.Now(), account.CreationDate)
                Formulario.HandlesDocumentSupport = Await Me.ValidateHandlesDocumentSupport()
                Formulario.showDeductibleIva = showDeductibleIva
                Formulario.hasDeductibleIva = _hasDeductibleIva
                Formulario.TaxRegistration = If(account?.TaxRegistration Is Nothing Or account?.TaxRegistration = 0, _TaxRegistration, account.TaxRegistration)
                Formulario.editMode = IIf(account Is Nothing, False, True)
                Formulario.Width = Screen.PrimaryScreen.WorkingArea.Width * 0.8
                Formulario.Height = Screen.PrimaryScreen.WorkingArea.Height * 0.8
                Formulario.FormBorderStyle = FormBorderStyle.Sizable

                If account Is Nothing OrElse account?.Status = 1 Then
                    Formulario.ConfirmStatus = False
                Else
                    Formulario.ConfirmStatus = True
                End If

                If account IsNot Nothing Then
                    Formulario.accountPayable = account
                End If

                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                If ListAddBill IsNot Nothing Then
                    If ListAddBill.Any() Then
                        Formulario.ListBill = ListAddBill
                    End If
                End If
                Dim transparent As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)

                If Formulario.BanClose Then
                    accountPayable = Nothing
                    accountPayable = Formulario.accountPayable
                    accountPayable.CostDistributionDirectCostId = Formulario.CostDistributionDirectCostId
                    If Not Formulario.entity Then
                        If ListAddBill Is Nothing Then
                            ListAddBill = New List(Of AccountPayable)
                        End If
                        ListAddBill.Add(accountPayable)
                        INDgleProvider.Properties.ReadOnly = True
                    End If

                    INDgcBills.DataSource = Nothing
                    INDgcBills.DataSource = ListAddBill

                    If Formulario.RecaulculateDeferredCausation Then
                        DeleteDeferredCausation(accountPayable.BillNumber)
                        CalculateDeferredCausation()
                    End If
                End If
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmAccountsPayable_SupplierChange", NAME_MODULE)
        End If
    End Sub

    Private Async Function ValidateHandlesDocumentSupport() As Task(Of Boolean)
        'Consulto el proveedor por id con xpo
        Using Model As New MSettingsAccount(Me.Tag)
            Dim parameters = Await Model.GetSettingAccount(BarraBotones.OperatingUnitValue)
            ''se valida primero que este activo el parametro de Maneja Documento soporte desde configuracion de contabilidad y mensajeria electronica
            If parameters IsNot Nothing And parameters.HandlesSupportDocument Then
                ''se valida que el tercero seleccionado no sea facturador electronico
                Supplier = Presenter.GetSupplierById(_idSupplier)
                If Supplier IsNot Nothing Then
					If Not Supplier?.IdThirdParty?.ElectronicBiller Then
						Return True
					End If
				End If
            End If
        End Using
        Return False
    End Function

    ''' <summary>
    ''' Calcula la causacion diferida correspondiente
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateDeferredCausation()
        ListDeferredCausation = PaymentServices.GroupMainAccountsAndCostCenter(ListAddBill, ListDeferredCausation)
        AgregatedDescriptionToDeferredCausation()
        INDgcDeferredCausation.DataSource = Nothing
        INDgcDeferredCausation.DataSource = ListDeferredCausation
    End Sub

    ''' <summary>
    ''' Elimina la causacion diferida de la rejilla y si viene de la BD las marca como eliminadas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDeferredCausation(ByVal billNumber As String, Optional ByVal optionChangeDatasource As Boolean = False)
        If ListDeferredCausation IsNot Nothing AndAlso ListDeferredCausation.Count > 0 Then

            For Each itemDC As DeferredCausation In ListDeferredCausation
                If itemDC.BillNumber = billNumber AndAlso itemDC.Id > 0 Then
                    While itemDC.DeferredCausationDetails.Count > 0
                        itemDC.DeferredCausationDetails.Item(0).MarkAsDeleted()
                    End While
                    While itemDC.DeferredCausationShare.Count > 0
                        itemDC.DeferredCausationShare.Item(0).MarkAsDeleted()
                    End While
                    If listDeleteDeferredCausation Is Nothing Then
                        listDeleteDeferredCausation = New List(Of DeferredCausation)
                    End If
                    itemDC.MarkAsDeleted()
                    listDeleteDeferredCausation.Add(itemDC)
                End If
            Next

            ListDeferredCausation.RemoveAll(Function(x) x.BillNumber = billNumber)
            If optionChangeDatasource Then
                INDgcDeferredCausation.DataSource = Nothing
                INDgcDeferredCausation.DataSource = ListDeferredCausation
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que agrega las descripciones de cuenta contable y centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AgregatedDescriptionToDeferredCausation()
        For Each itemCabeceraCxP As AccountPayable In ListAddBill
            For Each itemDetalleCxP As AccountPayableDetailConcept In itemCabeceraCxP.AccountPayableDetailConcept
                If itemDetalleCxP.IdCostCenter IsNot Nothing Then
                    For Each itemDeferredCausation As DeferredCausation In ListDeferredCausation
                        If itemCabeceraCxP.BillNumber = itemDeferredCausation.BillNumber Then
                            If itemDetalleCxP.IdCostCenter = itemDeferredCausation.IdCostCenter AndAlso itemDetalleCxP.IdAccount = itemDeferredCausation.IdMainAccount Then
                                itemDeferredCausation.NumberNameMainAccount = itemDetalleCxP.NumberNameMainAccount
                                itemDeferredCausation.DescriptionCostCenter = itemDetalleCxP.DescriptionCostCenter
                            End If
                        End If
                    Next
                Else
                    For Each itemDeferredCausation As DeferredCausation In ListDeferredCausation
                        If itemCabeceraCxP.BillNumber = itemDeferredCausation.BillNumber Then
                            If itemDetalleCxP.IdAccount = itemDeferredCausation.IdMainAccount Then
                                itemDeferredCausation.NumberNameMainAccount = itemDetalleCxP.NumberNameMainAccount
                            End If
                        End If
                    Next
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva cuenta por pagar
    ''' </summary>
    Private Async Function NewAccountPayable() As Task

        accountPayable = New AccountPayable()
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Consecutive = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Consecutive = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        GetDate()
        If keyCausation = 0 Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
        End If
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.ListAddBill IsNot Nothing AndAlso Me.ListAddBill.Count > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                CodeAccountPayable = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            CodeAccountPayable = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), codeDocumentIndexed, INDgleProvider.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & codeDocumentIndexed & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), codeDocumentIndexed),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), codeDocumentIndexed, INDgleProvider.Text)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), codeDocumentIndexed)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que valida si el periodo esta abierto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ValidatePeriod() As Task(Of Boolean)
        If INDdteDateDocument.EditValue IsNot Nothing Then
            Using model As New MDocumentAccount(CStr(Me.Tag))
                If Await model.ValidatePeriod(CDate(INDdteDateDocument.EditValue).Month, CDate(INDdteDateDocument.EditValue).Year) = False Then
                    Dim periods As List(Of ClosedMonth) = model.GetOpenPeriod()
                    If periods IsNot Nothing AndAlso periods.Count > 0 Then
                        Dim perOpen As String = AccountingServices.GetListOpenPeriods(periods)
                        Mensaje(EeventViewerImages.Advertencia) = perOpen
                    End If
                    INDdteDateDocument.EditValue = Nothing
                    INDdteDateDocument.Focus()
                    Return False
                End If
            End Using
            Return True
        End If
    End Function

    ''' <summary>
    ''' Metodo para abrir el form de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenDeferredCausation(ByVal defer As DeferredCausation)
        If INDdteDateDocument.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una Fecha Documento."
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmPopupDeferredCausation
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Formulario.deferredCausation = defer
            Formulario.IdSupplier = _idSupplier
            Formulario.Tag = Me.Tag
            If listDeleteDeferredCausationDetail IsNot Nothing AndAlso listDeleteDeferredCausationDetail.Count > 0 Then
                Formulario.listDeleteDeferredCausationDetail = listDeleteDeferredCausationDetail
            End If

            'Se cambia la fecha de la factura por la fecha del documento
            'Formulario.dateBill = ListAddBill.Item(0).BillDate
            'Se cambia la fecha de la factura porque ya no se aumenta en un mes a la fecha actual 
            'sino que empieza desde el mismo mes que escoge el usuario en fecha del documento y puede escoger en el form de causación
            'Formulario.DateBill = DateAdd(DateInterval.Month, 1, INDdteDateDocument.EditValue)
            Formulario.INDdteInitialDate.Properties.MinValue = INDdteDateDocument.EditValue
            Formulario.DateBill = INDdteDateDocument.EditValue

            Dim transparent As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            INDgcDeferredCausation.RefreshDataSource()
            listDeleteDeferredCausationDetail = Formulario.listDeleteDeferredCausationDetail
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del registro de la rejilla de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Defer()
        Dim defer As DeferredCausation = CType(viewDeferredCausation.GetFocusedRow, DeferredCausation)
        OpenDeferredCausation(defer)
    End Sub

    Private Function SetAccountPayables(result As List(Of Domain.Entities.AccountPayable)) As List(Of String)
        Dim listErrors As New List(Of String)

        If ListAddBill IsNot Nothing AndAlso ListAddBill.Count > 0 Then
            For Each item In result
                If ListAddBill.Any(Function(ap) ap.BillNumber = item.BillNumber) Then
                    listErrors.Add("La factura " & item.BillNumber & " ya esta agregada.")
                    Continue For
                End If

                ListAddBill.Add(item)
            Next
        Else
            ListAddBill = result
        End If

        INDgcBills.DataSource = Nothing
        INDgcBills.DataSource = ListAddBill

        Return listErrors
    End Function

#End Region

#Region "Crud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Consecutive) AndAlso Not String.IsNullOrWhiteSpace(Consecutive) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using Model As New MAccountPayable(CStr(Me.Tag))
                AsyncLoader(True)
                ListAddBill = Await Model.GetAccountPayableByCode(Consecutive)

                INDlycAccountPayable.BeginUpdate()
                If Not ListAddBill Is Nothing Then
                    Me.BarraBotones.StatusRecordVisible = True

                    banSupplier = True
                    For Each item As AccountPayable In ListAddBill
                        Me.IdAccountPayable = item.Id
                        _idSupplier = item.IdSupplier
                        _independentEmployee = item.IndependentEmployee
                        supplierDeclarant = item.Declarant
                        INDgleProvider.EditValue = item.IdSuppliersDistributionLines
                        GetListRetentionConcept(item.IdSuppliersDistributionLines)
                        INDgleProvider.Properties.NullText = item.DescriptionSupplier
                        _idThirdParty = item.IdThirdParty
                        _idThirdPartyContributionType = item.ContributionType
                        Me._handlesDocumentSupport = item.HandlesDocumentSupport
                        _idPosition = item.PositionId
                        DistributionLineText = item.DescriptionDistributionLine
                        PositionText = item.PositionCodeName
                        _idMainAccountSupplier = item.IdAccount
                        INDdteDateDocument.EditValue = item.DocumentDate
                        ServicePeriodDate = item.ServicePeriodDate
                        _hasDeductibleIva = item.DeductibleIva
                        _TaxRegistration = item.TaxRegistration

                        IfTransferContainsAccountPayable = (From l In ListAddBill Where l.IfTransferContainsAccountPayable = True Select l).Count
                        If IfTransferContainsAccountPayable > 0 Then 'Cuando la cuenta por pagar esta en un traslado
                            INDsleFilingUnit.ReadOnly = True
                            INDsleFilingUnit.DisplayNullText = item.DescriptionFilingUnit
                            FilingUnitId = item.FilingUnitId
                        Else 'Cuando la cuenta por pagar no se encuentra en un traslado
                            banFilingUnit = False
                            FilingUnitId = item.FilingUnitId
                            banFilingUnit = True
                        End If

                        LayoutControls.SetCustomFieldsValue(item.CustomProperties)

                        banSearchSupplierType = False
                        SupplierTypeId = item.SupplierTypeId
                        banSearchSupplierType = True
                        BarraBotones.StatusRecord = item.Status.ToString

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), item.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), item.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), item.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), item.ModificationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), item.ConfirmationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), item.ConfirmationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), item.AnnulmentUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), item.AnnulmentDate)
                        Exit For
                    Next
                    banSupplier = False

                    INDgcBills.DataSource = Nothing
                    INDgcBills.DataSource = ListAddBill

                    'Causación Diferida
                    ListDeferredCausation = New List(Of DeferredCausation)
                    Await Presenter.ConsultListDeferredCausationByCode(ListAddBill.Item(0).Code)
                    If _listAddDeferredCausation IsNot Nothing Then
                        For Each itemDC As DeferredCausation In _listAddDeferredCausation
                            itemDC.Status = 0
                            ListDeferredCausation.Add(itemDC)
                        Next
                    End If

                    Dim banControls As Boolean = ListAddBill.Any(Function(d) d.Status = 2 OrElse d.Status = 3)
                    If banControls Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        SetListActions()
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                    End If

                    If keyCausation = 0 Then
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
                    End If

                    Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                        Me.GetDocumentIndexed(Me.Tag & "_" & ListAddBill.Item(0).Code)
                        Dim result = Await ModelRecord.GetBlockRecordByConsecutive(CStr(Me.Tag), ListAddBill.Item(0).Code)
                        If result Is Nothing OrElse result.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added}, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ListAddBill.Item(0).Id})
                            ).ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                    End Using

                    Me.BarraBotones.SetDocuments(ListAddBill.Item(0).Id)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, ListAddBill.Item(0).Id, 0, ListAddBill.Item(0).Code)
                    AsyncLoader(False)
                    ActionsOnControls = True
                    INDgcDeferredCausation.DataSource = Nothing
                    INDgcDeferredCausation.DataSource = ListDeferredCausation
                    INDgleProvider.Properties.ReadOnly = True
                    If banControls Then
                        ReadOnlyControls(True)
                        ActionsOnControlsPopupConcept = True
                        For iColumns = 0 To viewBill.Columns.Count - 1
                            If viewBill.Columns(iColumns).Name = "MoreInfo" Or viewBill.Columns(iColumns).Name = "INDcolConcepts" Then
                                viewBill.Columns(iColumns).OptionsColumn.AllowEdit = True
                            End If
                        Next
                    End If
                Else

                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await Me.NewAccountPayable()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Consecutive = String.Empty
                        INDtxtConsecutive.Focus()
                    End If

                End If
            End Using
        End If

        INDlycAccountPayable.EndUpdate()
    End Function

    ''' <summary>
    ''' Establece la visibilidad de los controles del popup
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsOnControlsPopupConcept As Boolean
        Set(value As Boolean)
            INDpopupConcepts.Enabled = value
            INDlyDetailConcepts.Enabled = value
            LayoutControlGroup1.Enabled = value
            INDgcDetailConcepts.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDtxtConsecutive.Text = ReturnValue
        If INDtxtConsecutive.Text <> String.Empty Then
            Await LoadControls()
            If INDtxtConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDtxtConsecutive.Enabled = False
        End If
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Metodo: Eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If ListAddBill IsNot Nothing Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MAccountPayable(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteAccountPayable(ListAddBill)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDtxtConsecutive.Enabled = False
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
                    INDtxtConsecutive.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If IfTransferContainsAccountPayable > 0 Then
            INDlyItemFilingUnit.ShowInCustomizationForm = True
        End If
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        If IfTransferContainsAccountPayable > 0 Then
            INDlyItemFilingUnit.ShowInCustomizationForm = False
        End If
        If banGuardarConfirmar = True Then
            'Se valida que los items del listado de CxP tengan el # de factura lleno
            If ListAddBill IsNot Nothing AndAlso ListAddBill.Count > 0 Then
                Dim cont = (From l In ListAddBill Where l.BillNumber Is String.Empty).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Algunas facturas agregadas no tienen el No. Factura diligenciado."
                    Exit Sub
                End If
            End If
            'If Await ValidatePeriod() = False Then
            '    Exit Sub
            'End If
        End If
        If _TaxRegistration Is Nothing Then
            Using model As New MCompanySettings(Tag)
                Dim _companySettings = Await model.GetCompanySettings()
                If _companySettings Is Nothing Then
                    Mensaje(EeventViewerImages.MensajeError) = "No se encontró campo parametrizado de Registro IVA"
                    Exit Sub
                End If
                _TaxRegistration = _companySettings.TaxRegistration
            End Using
        End If
        AssigningValues()
        Try
            Using Model As New MAccountPayable(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveListAccountPayable(ListAddBill, ListDeferredCausation, modeSaveAndConfirm, Me._idCurrentSequense)
                If Result.StateResult = True Then
                    If ListAddBill.Any(Function(o) o.Id = 0) Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Dim codesMessage As String = String.Empty
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 Then
                        codesMessage = Result.MessageResult(0).ToString()
                    Else
                        codesMessage = codesMessage + vbCrLf + "Consecutivo CxP: " + Result.ObjectEmbbeded.Item(0)
                        codesMessage = codesMessage + vbCrLf + "No. Radicado: " + Result.Message
                    End If

                    If banRegister = True Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), codesMessage)
                    Else
                        If banGuardarConfirmar = True Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), codesMessage)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, ListAddBill.Item(0).Id, 0, Result.ObjectEmbbeded.Item(0))
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, ListAddBill.Item(0).Id, 0, Result.ObjectEmbbeded.Item(0))
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, ListAddBill.Item(0).Id, 0, Result.ObjectEmbbeded.Item(0))
                    End Select
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDtxtConsecutive.Enabled = False
                    If Result.MessageResult IsNot Nothing Then
                        generateListError(Result.MessageResult(0))
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDtxtConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Método: Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequense Is Nothing OrElse _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewAccountPayable()
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If

        Dim ListItems As New List(Of Tuple(Of String, Byte))
        ListItems.Add(New Tuple(Of String, Byte)("Sin Confirmar", 1))
        ListItems.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "No. Factura", .FieldName = "BillNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Proveedor", .FieldName = "IdSupplier.Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nit Tercero", .FieldName = "IdSupplier.IdThirdParty.Nit", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "Abbreviation", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "Value", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2), .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .ColumnFormat = "N2"},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListItems}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayable
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo: Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub Anular()
        Try
            Using model As New MAccountPayable(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.AnnularAccountPayable(ListAddBill, _idCurrentSequense)
                If Result?.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    Me.BarraBotones.PrintReport(PrintReportAction.Cancel, ListAddBill.Item(0).Id, 0, ListAddBill.Item(0).Code)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDtxtConsecutive.Enabled = False
                    If Result?.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")

                    ElseIf Result?.MessageResult IsNot Nothing Then
                        generateListError(Result?.MessageResult(0))
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDtxtConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        NatureType = Nothing
        banSupplier = Nothing
        _idMainAccountSupplier = Nothing
        _idSupplier = Nothing
        _independentEmployee = Nothing
        _idThirdParty = Nothing
        _idThirdPartyContributionType = Nothing
        _idPosition = Nothing
        Presenter = Nothing
        _sequense = Nothing
        ListDeferredCausation = Nothing
        _listAddDC = Nothing
        ListAddBill = Nothing
        listDeleteBill = Nothing
        ListAddConcept = Nothing
        accountPayable = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        dateServerVariable = Nothing
        banRegister = Nothing
        banGuardarConfirmar = Nothing
        record = Nothing
        _listDeleteDeferredCausationDetail = Nothing
        _listDeleteDeferredCausation = Nothing
        banSearchSupplierType = Nothing
        banFilingUnit = Nothing
        codeDocumentIndexed = Nothing
        banCodeIndexed = Nothing
        modeSaveAndConfirm = Nothing
        keyConfirmation = Nothing
        keyCausation = Nothing
        listConceptCxpXpo = Nothing
        supplierDeclarant = Nothing
        varImp = Nothing
        IfTransferContainsAccountPayable = Nothing
        PivotFilingUnit = Nothing
        Me._thirdPartyElectronicBiller = False
        Me._handlesDocumentSupport = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAccountsPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycAccountPayable, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAccountPayable(Me)
        IndigoGridControl1.RefreshGrid(INDgcBills)
        IndigoGridControl1.RefreshGrid(INDgcDeferredCausation)
        AsyncLoader(True)
        Await Presenter.GetSequense()
        AsyncLoader(False)
        LoadStatus()
        Deshacer()
        LoadRepositorySearch()
        SetListActions()


        INDdteServicePeriodDate.Properties.MaxValue = GetDateServer()

        INDEsbAccountPayable.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Iva Descontable", .Comment = "0 - No, 1 - Si"},
                            New ExcelColumn With {.Name = "No. Factura"},
                            New ExcelColumn With {.Name = "Fecha Factura", .Comment = "El formato de la fecha es dd/mm/aaaa", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Moneda", .Comment = "Escriba la abreviación de la moneda", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Valor Facturado"},
                            New ExcelColumn With {.Name = "Observaciones"},
                            New ExcelColumn With {.Name = "Concepto de Pago", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Nit Tercero"},
                            New ExcelColumn With {.Name = "Centro Costo", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Observaciones"},
                            New ExcelColumn With {.Name = "Naturaleza", .Comment = "1 - Debito, 2 - Credito"},
                            New ExcelColumn With {.Name = "Valor Base"},
                            New ExcelColumn With {.Name = "Tarifa IVA", .Comment = "Escriba el código de la tarifa en caso de que el concepto maneje iva", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Concepto Retención", .Comment = "Escriba el código del concepto de retención", .Type = ExcelColumnType.Text}
                        }
                    })
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If PaymentsSettingPaymentsXpo Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No existe parámtros de pagos para la unidad operativa escogida"
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Consecutive.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Consecutive) Then
                    Await Me.NewAccountPayable()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountsPayable_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtConsecutive.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDgleProvider_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleProvider.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            Presenter.InitializeSupplier()
            If IdSupplier IsNot Nothing AndAlso IdSupplier > 0 Then
                Await InitializeSupplierType()
                GetListRetentionConcept(IdSupplier)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleFilingUnit_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFilingUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("723", "", True)
            Await InitializeFilingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleSupplierType_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("726", "", True)
            If IdSupplier > 0 Then
                Await InitializeSupplierType()
            End If
        End If
    End Sub




#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDgleProvider_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleProvider.EditValueChanged
        Try
            AsyncLoader(True)
            If IdSupplier IsNot Nothing AndAlso IdSupplier > 0 Then
                If banSupplier = False Then
                    'Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                    Dim suppplierMainAccount = Presenter.GetSupplierDistributionLineById(INDgleProvider.EditValue)
                    _idMainAccountSupplier = suppplierMainAccount.IdDistributionLine.IdMainAccount.Id
                    _idSupplier = suppplierMainAccount.IdSupplier.Id
                    _independentEmployee = suppplierMainAccount.IdSupplier.IndependentEmployee
                    _idThirdParty = suppplierMainAccount.IdSupplier.IdThirdParty.Id
                    _idThirdPartyContributionType = suppplierMainAccount.IdSupplier.IdThirdParty.ContributionType
                    Me._thirdPartyElectronicBiller = suppplierMainAccount.IdSupplier.IdThirdParty.ElectronicBiller
                    _idPosition = Nothing
                    If suppplierMainAccount.PositionId IsNot Nothing Then
                        _idPosition = suppplierMainAccount.PositionId.Id
                    End If
                    DistributionLineText = suppplierMainAccount.IdDistributionLine.CodeName
                    PositionText = If(_idPosition Is Nothing, String.Empty, suppplierMainAccount.PositionId.CodeName)
                    If suppplierMainAccount.IdSupplier.Declarant Then
                        supplierDeclarant = 1
                    Else
                        supplierDeclarant = 2
                    End If
                    GetListRetentionConcept(IdSupplier)
                End If
                Await InitializeSupplierType()
            Else
                _idMainAccountSupplier = 0
                _idSupplier = 0
                _independentEmployee = False
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de proveedor
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
    ''' Evento que se dispara al cambiar el valor del control de unidad de radicacion
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

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Select Case (sender.Tag.ToString)
            Case "Edit", "View"
                EditBill()
            Case "Remove"
                DeleteBill()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Defer()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al hacer click en el boton de agregar factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddBill_Click(sender As Object, e As EventArgs) Handles INDbtnAddBill.Click
        OpenAddBill(Nothing)
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountsPayable_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Consecutive = String.Empty Then
            INDtxtConsecutive.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara cuando se despliega el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleProvider_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleProvider.QueryPopUp
        If INDgleProvider.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tipo de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_QueryPopUp(sender As Object, e As CancelEventArgs)
        'If SupplierTypeXpo Is Nothing Then
        '    Presenter.InitializeSupplierType()
        'End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de unidad de radicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnit_QueryPopUp(sender As Object, e As CancelEventArgs)
        If FilingUnitXpo Is Nothing Then
            'Presenter.InitializeFilingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de repositorio para ver los conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepPceConcepts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceConcepts.QueryPopUp
        Dim _ap As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        If _ap.AccountPayableDetailConcept.Count > 0 Then
            INDgcDetailConcepts.DataSource = Nothing
            For Each itemDetail In _ap.AccountPayableDetailConcept
                itemDetail.CurrencyAbbreviation = _ap.CurrencyAbbreviation
            Next
            INDgcDetailConcepts.DataSource = _ap.AccountPayableDetailConcept.ToList
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
    Private Async Sub FrmAccountsPayable_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await InitializeFilingUnit()
        'Se valida si solo hay un hijo para seleccionarlo automaticamente(Para pitalito que solo hay una unidad de radicacion hijo y no quieren seleccionarla sino que ya aparezca de una vez)
        If FilingUnitXpo IsNot Nothing AndAlso FilingUnitXpo.Count > 0 Then
            Dim cont = (From l In FilingUnitXpo Where l.IsSon = True Select l).Count
            If cont = 1 Then
                Dim item = (From l In FilingUnitXpo Where l.IsSon = True Select l).FirstOrDefault
                FilingUnitId = item.Id
            End If
        End If

        'Lógica del control de compromiso
        LoadPaymentsSetting()

        INDtxtConsecutive.Focus()
    End Sub

#End Region

#Region "PasteToGrid"


    ''' <summary>
    ''' Evento que se dispara al copiar y pegar en la rejilla de faturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        AsyncLoader(True)
        importFlag = False
        passInformation(e.Rows, Nothing)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento para que consulte y valide los rejillos importados o copiados a rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub passInformation(rows As List(Of List(Of String)), importRow As List(Of ImportFileRow))
        If accountPayable?.Id > 0 AndAlso accountPayable?.Status > 1 Then
            Exit Sub
        End If

        Try
            If _idSupplier = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un proveedor."
                Exit Sub
            End If

            Dim listErrors As New List(Of String)
            Using model As New MAccountPayable(MyTag)
                Dim result
                If importFlag Then
                    result = Await model.ImportBillsToAccountPayable(Nothing, importRow, New List(Of Object) From {_idSupplier, _idMainAccountSupplier})
                Else
                    result = Await model.ImportBillsToAccountPayable(rows, Nothing, New List(Of Object) From {_idSupplier, _idMainAccountSupplier})
                End If
                AsyncLoader(False)
                listErrors = result.MessageResult
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    result.MessageResult.AddRange(SetAccountPayables(result.ObjectEmbbeded))
                End If
            End Using

            If listErrors.Count > 0 Then
                Using formulario As New FrmListErrors(listErrors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try

    End Sub


#End Region
#Region "Import"
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing
    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String)
    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If accountPayable.Id > 0 AndAlso accountPayable.Status > 1 Then
            Exit Sub
        End If
        If _idSupplier = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un proveedor."
            Exit Sub
        End If
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        Dim Dresult = openFileDialog1.ShowDialog()
        If Dresult OrElse Dresult = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows

                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    ImportExcelFile()
                    rows.ClearOutline()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(14)})
                                              End SyncLock
                                          End Sub)
    End Sub

    Private Sub ImportExcelFile()
        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
        SetRow(1, rows.LastUsedIndex + 1)
        importFlag = True
        passInformation(Nothing, listRows.ToList())
        AsyncLoader(False)
    End Sub


#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Evento que se dispara la presionar click en la barra de botones en buscar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        banRegister = True
        banGuardarConfirmar = True
        banCodeIndexed = True
        modeSaveAndConfirm = True
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))

        Dim _presenter = New PAccountPayable()
        Dim listPermission As List(Of Integer) = _presenter.Permission(BarraBotones.PermissionsForm)
        keyCausation = listPermission(0)
        keyConfirmation = listPermission(1)
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        banGuardarConfirmar = False
        banRegister = False
        banCodeIndexed = False
        modeSaveAndConfirm = False
        varImp = 2
        Guardar()
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
        banGuardarConfirmar = False
        banRegister = True
        banCodeIndexed = False
        modeSaveAndConfirm = False
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If ListAddBill IsNot Nothing AndAlso ListAddBill.Count > 0 Then
            For Each item As AccountPayable In ListAddBill
                item.IdOperatingUnit = BarraBotones.OperatingUnit.Id
            Next
        End If
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PaymentsSecuenceDetail IsNot Nothing Then
            If Me._sequense.PaymentsSecuenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.PaymentsSecuenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
        'Se genera el listado de conceptos de retencion que se envia al form modal
        If INDgleProvider IsNot Nothing AndAlso IdSupplier IsNot Nothing AndAlso IdSupplier > 0 Then
            GetListRetentionConcept(IdSupplier)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de actualizar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        banGuardarConfirmar = True
        banRegister = False
        banCodeIndexed = False
        modeSaveAndConfirm = True
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Click anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, ListAddBill.Item(0).Id, 0, ListAddBill.Item(0).Code)
    End Sub

#End Region

End Class