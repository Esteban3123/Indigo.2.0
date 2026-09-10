'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Diego Andres Roldan Lozano
' Created          : 10-11-2014
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
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Domain.Entities.Service
Imports Presentation.Treasury.MVP
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Presentation.Common.MVP
Imports System.Text

#End Region

Public Class CtrNoteConcept

#Region "Properties"

    ''' <summary>
    ''' Constante con el nombre del modulo de tesoreria
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Constante con el nombre del modulo de contabilidad
    ''' </summary>
    Public Const NAME_MODULE_ACCOUNTING As String = "Accounting"

    ''' <summary>
    ''' tag del frontal que contiene el control
    ''' </summary>
    Private _myTag As String = "637"

    ''' <summary>
    ''' Evento para cerrar el popup del control
    ''' </summary>
    Public Event AddNoteConcept(sender As Object, e As EventArgs)

    ''' <summary>
    ''' variable que contiene la entidad de concepto de nota
    ''' </summary>
    Public Property NoteConcept As TreasuryNoteDetail

    ''' <summary>
    ''' Contiene el listado de las cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountListXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleMainAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor base
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BaseValue As Decimal
        Get
            Return CDec(INDtxtBaseValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtBaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor facturado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoicedValue As Decimal
        Get
            Return CDec(INDtxtInvoicedValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtInvoicedValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nature As Integer
        Get
            Return CInt(INDgleNature.EditValue)
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Percentage As Decimal
        Get
            Return CDec(INDsePercentage.EditValue)
        End Get
        Set(value As Decimal)
            INDsePercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionValue As Decimal
        Get
            Return CDec(INDtxtRetentionValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtRetentionValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los conceptos de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NoteConceptDatasource As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los conceptos de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptRetentionListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleConceptRetention.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConceptRetention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountId As Integer
        Get
            Return CInt(INDsleMainAccount.EditValue)
        End Get
        Set(value As Integer)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NoteConceptId As Integer
        Get
            Return INDsleConcept.EditValue()
        End Get
        Set(value As Integer)
            INDsleConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdConceptRetention As Integer
        Get
            Return CInt(INDsleConceptRetention.EditValue)
        End Get
        Set(value As Integer)
            INDsleConceptRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer?
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer?
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para los mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptCodeName() As String
        Get
            Return INDLbCashFlowConcept.Text
        End Get
        Set(ByVal value As String)
            INDLbCashFlowConcept.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptId() As Integer
        Get
            If IsNumeric(INDLbCashFlowConcept.Tag) Then
                Return CInt(INDLbCashFlowConcept.Tag)
            End If
            Return 0
        End Get
        Set(ByVal value As Integer)
            INDLbCashFlowConcept.Tag = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim NatureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Establece si la entidad viene para modificar o agregar
    ''' </summary>
    ''' <remarks></remarks>
    Public Property IsEdit As Boolean

    ''' <summary>
    ''' Obtiene o establece el objeto retencion
    ''' </summary>
    ''' <returns></returns>
    Public Property retConcetp As RetentionConcepts

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Metodo para agregar un concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddConcept()
        Dim resultValidation As ActionResult = ValidateControls()
        If resultValidation.StateResult Then
            AssigningValues()
            RaiseEvent AddNoteConcept(NoteConcept, EventArgs.Empty)
            CleanControls()
            INDsleConcept.Focus()
        Else
            Dim messageError As New StringBuilder()
            messageError.AppendLine("Campos sin diligenciar:")
            messageError.AppendLine(resultValidation.Message)
            Mensaje(EeventViewerImages.Advertencia) = messageError.ToString()
        End If
    End Sub

    ''' <summary>
    ''' Valida que los controles esten correctamente diligenciados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As ActionResult
        Dim validationErrors As New StringBuilder()
        If NoteConceptId = 0 Then
            validationErrors.AppendLine("Concepto de Nota")
        End If
        If MainAccountId = 0 Then
            validationErrors.AppendLine("Cuenta Contable")
        End If
        'If ThirdPartyId Is Nothing Then
        '    validationErrors.AppendLine("Tercero")
        'End If
        If Nature = 0 Then
            validationErrors.AppendLine("Naturaleza")
        End If
        If BaseValue = 0 Then
            validationErrors.AppendLine("Valor")
        End If
        If INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ThirdPartyId Is Nothing Then
                validationErrors.AppendLine("Tercero")
            End If
        End If
        If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleCostCenter.Enabled = True Then
                If CostCenterId = 0 Then
                    validationErrors.AppendLine("Centro de Costo")
                End If
            End If
        End If
        If validationErrors.Length > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = validationErrors.ToString()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Asigna los valores para el concepto de una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If NoteConcept Is Nothing Then
            NoteConcept = New TreasuryNoteDetail()
        End If
        With NoteConcept
            .NoteConceptId = NoteConceptId
            .MainAccountId = MainAccountId
            .FullNameCostCenter = INDsleCostCenter.Text
            .FullNameMainAccount = INDsleMainAccount.Text
            .FullNameNature = INDgleNature.Text
            .FullNameThird = INDsleThirdParty.Text
            .NoteConceptCode = INDsleConcept.Text.Split("-").ElementAt(0).Trim()
            .NoteConceptName = INDsleConcept.Text.Split("-").ElementAt(1).Trim()
            .ThirdPartyId = ThirdPartyId
            .CostCenterId = CostCenterId
            .Nature = Nature
            If CashFlowConceptId > 0 Then
                .IdCashFlowConcept = CashFlowConceptId
                .CodeNameCashFlowConcept = CashFlowConceptCodeName
            Else
                .IdCashFlowConcept = Nothing
                .CodeNameCashFlowConcept = String.Empty
            End If

            If RetentionValue > 0 Then
                .Value = RetentionValue
            Else
                .Value = BaseValue
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        NoteConceptId = Nothing
        MainAccountId = Nothing
        ThirdPartyId = Nothing
        CostCenterId = Nothing
        IdConceptRetention = Nothing
        Percentage = 0
        Nature = Nothing
        IsEdit = False
        BaseValue = 0
        InvoicedValue = 0
        RetentionValue = 0
        NoteConcept = Nothing
        CleanControlsRetention()
        INDsleMainAccount.Properties.NullText = String.Empty
        INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        CashFlowConceptId = 0
        CashFlowConceptCodeName = String.Empty
    End Sub

    ''' <summary>
    ''' carga la entidad en los controles
    ''' </summary>
    Public Sub LoadControls()
        If NoteConcept IsNot Nothing Then
            With NoteConcept
                NoteConceptId = .NoteConceptId
                ThirdPartyId = .ThirdPartyId
                CostCenterId = .CostCenterId
                Nature = .Nature
                BaseValue = .Value
                CashFlowConceptId = .IdCashFlowConcept.GetValueOrDefault
                CashFlowConceptCodeName = .CodeNameCashFlowConcept
            End With
        End If
    End Sub

    ''' <summary>
    ''' Activa o desactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDsleConcept.Enabled = Not value
            INDsleMainAccount.Enabled = value
            INDsleThirdParty.Enabled = value
            INDgleNature.Enabled = value
            INDtxtBaseValue.Enabled = value
            INDbtnAdd.Enabled = value
            ActionsOnControlsConceptRetention = False
            If value Then
                INDsleMainAccount.Focus()
            Else
                INDsleConcept.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Activa o desactiva los controles de concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControlsConceptRetention As Boolean
        Set(value As Boolean)
            INDsleConceptRetention.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub CreateNature()
        NatureType = New List(Of Tuple(Of Integer, String))
        NatureType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDgleNature.Properties.DataSource = NatureType.ToList()
    End Sub

    ''' <summary>
    ''' Metodo para cargar de informacion los combos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UploadSearchLookUp()
        If Not DesignMode Then
            InitializeNoteConcept()
            InitializeAccount()
            InitializeThirdParty()
            InitializeCostCenter()
            InitializeConceptRetention()
            CreateNature()
        End If
    End Sub

    ''' <summary>
    ''' Inicia el datasource de conceptos de nota
    ''' </summary>
    Private Sub InitializeNoteConcept()
        Using msearch As New MBusqueda
            NoteConceptDatasource = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListNoteConcept), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de cuentas contables
    ''' </summary>
    Private Sub InitializeAccount()
        Using msearch As New MBusqueda
            Dim filter() As Object = {"5", True}
            AccountListXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de terceros
    ''' </summary>
    Private Sub InitializeThirdParty()
        Using msearch As New MBusqueda
            ThirdPartyListXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenter()
        'Using msearch As New MBusqueda
        CostCenterListXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter), XPInstantFeedbackSource)
        'End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de conceptos de retencion
    ''' </summary>
    Private Sub InitializeConceptRetention()
        Using msearch As New MBusqueda
            ConceptRetentionListXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConcept), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Consulta que se hizo debido a que cuando se consultaba del modelo botaba un error desconocido
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetNoteConceptById(ByVal Id As Integer) As Task(Of NoteConcepts)
        If Not DesignMode Then
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNoteConceptByIdAsync(Id, SessionValues.Instance.AuditMessageWcf)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Envía el foco al primer control
    ''' </summary>
    Public Sub FirstControlFocus()
        INDsleConcept.Focus()
    End Sub

    ''' <summary>
    ''' establece el formato moneda segun la que reciba el metodo
    ''' </summary>
    ''' <param name="_CurrencyAbbreviation"></param>
    Public Sub SetCurrencyUI(_CurrencyAbbreviation As String)
        If String.IsNullOrEmpty(_CurrencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(_CurrencyAbbreviation.GetCultureId()).NumberFormat
        INDtxtBaseValue.Properties.Mask.Culture = _culture
        INDtxtInvoicedValue.Properties.Mask.Culture = _culture
        INDtxtRetentionValue.Properties.Mask.Culture = _culture
    End Sub

#End Region

#Region "Events"

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConcept.EditValueChanged
        If Not DesignMode Then
            If INDsleConcept.EditValue IsNot Nothing AndAlso NoteConceptId > 0 Then
                Dim concept As NoteConcepts = Await Me.GetNoteConceptById(NoteConceptId)
                If concept IsNot Nothing AndAlso concept.Id > 0 Then
                    INDgleNature.EditValue = concept.Nature
                    INDsleMainAccount.EditValue = concept.IdMainAccount
                    If concept.MainAccounts.HandlesCostCenter Then
                        INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDsleCostCenter.EditValue = Nothing
                    End If
                    If concept.CashFlowConcept IsNot Nothing Then
                        CashFlowConceptCodeName = String.Format("{0} - {1}", concept.CashFlowConcept.Code, concept.CashFlowConcept.NameConcept)
                        CashFlowConceptId = concept.CashFlowConcept.Id
                    Else
                        CashFlowConceptCodeName = String.Empty
                        CashFlowConceptId = 0
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMainAccount.EditValueChanged
        If Not DesignMode Then
            If MainAccountId > 0 Then
                Using model As New MPUC(CStr(Tag))
                    Dim puc As MainAccounts
                    puc = model.GetAccountId(MainAccountId)
                    If puc.HandlesThirdParty Then
                        INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDsleThirdParty.EditValue = Nothing
                    End If
                    If puc.HandlesCostCenter = True Then
                        INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDsleCostCenter.Enabled = True
                    Else
                        INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDsleCostCenter.EditValue = Nothing
                    End If
                    If puc.RetencionType <> 0 Then
                        INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        ActionsOnControlsConceptRetention = True
                    Else
                        INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDsleConceptRetention.EditValue = Nothing
                        ActionsOnControlsConceptRetention = False
                    End If
                End Using
            End If
        End If
    End Sub

    Private _minBaseRetention As Decimal
    Private _retentiontype As Integer
    Private _listAccountingRetention As List(Of RetentionConceptRanges)

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de conceptos de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptRetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptRetention.EditValueChanged
        If INDsleConceptRetention.EditValue IsNot Nothing AndAlso INDsleConceptRetention.EditValue > 0 Then
            Using Model As New MRetentionConcept(Me._myTag)
                CleanControlsRetention()
                retConcetp = Model.GetRetentionByIdSimple(INDsleConceptRetention.EditValue)
                _minBaseRetention = retConcetp.MinBase
                _retentiontype = retConcetp.Retention
                _listAccountingRetention = retConcetp.RetentionConceptRanges.ToList()
                If retConcetp.Id > 0 Then
                    Select Case retConcetp.Retention
                        Case CInt(eRetentionType.Base)
                            INDsePercentage.EditValue = retConcetp.Rate
                            INDtxtInvoicedValue.Focus()
                        Case CInt(eRetentionType.Rango)
                            INDtxtInvoicedValue.Focus()
                        Case CInt(eRetentionType.Variable)
                            INDsePercentage.Properties.ReadOnly = False
                            INDsePercentage.Focus()
                    End Select
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles de tipo de retencion
    ''' </summary>
    Public Sub CleanControlsRetention()
        _minBaseRetention = 0
        _retentiontype = 0
        INDsePercentage.Properties.ReadOnly = True
        INDtxtInvoicedValue.EditValue = Nothing
        INDtxtRetentionValue.EditValue = Nothing
        INDsePercentage.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Gets the percentage retention by invoiced value.
    ''' </summary>
    Private Function GetPercentageRetentionByInvoicedValue(invoicevalue As Decimal) As Decimal
        For Each accRet As RetentionConceptRanges In _listAccountingRetention
            If accRet.ValueInitial <= invoicevalue AndAlso accRet.ValueFinish >= invoicevalue Then
                Return accRet.Percentage
            End If
        Next
        Return 0
    End Function

#End Region

#Region "LostFocus"

    ''' <summary>
    ''' Handles the LostFocus event of the INDtxtInvoicedValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtInvoicedValue_LostFocus(sender As Object, e As EventArgs) Handles INDtxtInvoicedValue.LostFocus
        If INDtxtInvoicedValue.EditValue IsNot Nothing AndAlso INDtxtInvoicedValue.EditValue > 0 Then
            If _minBaseRetention > INDtxtInvoicedValue.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvoiceValueLessMinBase", NAME_MODULE)
                INDtxtInvoicedValue.EditValue = Nothing
                INDtxtRetentionValue.EditValue = Nothing
                Exit Sub
            End If
            Select Case _retentiontype
                Case CInt(eRetentionType.Base)
                    If INDsePercentage.EditValue > 0 Or INDtxtInvoicedValue.EditValue > 0 Then
                        INDtxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(INDtxtInvoicedValue.EditValue), retConcetp)
                    End If
                Case CInt(eRetentionType.Rango)
                    If INDtxtInvoicedValue.EditValue > 0 Then
                        Dim percent As Decimal = GetPercentageRetentionByInvoicedValue(CDec(INDtxtInvoicedValue.EditValue))
                        INDsePercentage.EditValue = Nothing
                        INDtxtRetentionValue.EditValue = Nothing
                        If percent = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoIndexRetention", NAME_MODULE_ACCOUNTING)
                            Exit Sub
                        End If
                        INDsePercentage.EditValue = percent
                        INDtxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(INDtxtInvoicedValue.EditValue), retConcetp)
                    End If
                Case CInt(eRetentionType.Variable)
                    If INDsePercentage.EditValue > 0 Or INDtxtInvoicedValue.EditValue > 0 Then
                        INDtxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(INDtxtInvoicedValue.EditValue), retConcetp)
                    End If
            End Select
        End If
    End Sub

#End Region

#Region "Enum"
    Public Enum eRetentionType
        Base = 1
        Rango = 2
        Variable = 3
    End Enum
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddConcept()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs)
        CleanControls()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmNoteConcepts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                'NoteConceptDatasource.FixedFilterCriteria = ""
                Me.InitializeNoteConcept()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeAccount()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeThirdParty()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeCostCenter()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptRetention_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConceptRetention.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmRetentionConcept
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeConceptRetention()
            End Using
        End If
    End Sub

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDtxtBaseValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtBaseValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtBaseValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConceptRetention.Focus()
            Else
                INDbtnAdd.Focus()
            End If
        End If
    End Sub
#End Region

#End Region

End Class
