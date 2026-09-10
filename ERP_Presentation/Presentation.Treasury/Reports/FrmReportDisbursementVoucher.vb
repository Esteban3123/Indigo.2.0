#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Domain.Entities

#End Region

Public Class FrmReportDisbursementVoucher

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"


    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoTransactionVouchers As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCreationUsers As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoScheduled As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStartBank As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEndBank As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStartCash As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEndCash As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCurrency As XPInstantFeedbackSource
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrados (Sin Confirmar)"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Reversados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(5, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Formato"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Anexo Facturas"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(4, "Detallado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingGroupingReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupingReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupingReport Is Nothing Then
                _FillingGroupingReport = New List(Of Tuple(Of Integer, String))
                _FillingGroupingReport.Add(New Tuple(Of Integer, String)(1, "Cuenta"))
                _FillingGroupingReport.Add(New Tuple(Of Integer, String)(2, "Caja"))
                _FillingGroupingReport.Add(New Tuple(Of Integer, String)(3, "Tercero"))
                _FillingGroupingReport.Add(New Tuple(Of Integer, String)(4, "Usuario"))
                _FillingGroupingReport.Add(New Tuple(Of Integer, String)(5, "Banco"))
            End If
            Return _FillingGroupingReport
        End Get
    End Property

    Private _FillingClassVoucherReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingClassVoucherReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingClassVoucherReport Is Nothing Then
                _FillingClassVoucherReport = New List(Of Tuple(Of Integer, String))
                _FillingClassVoucherReport.Add(New Tuple(Of Integer, String)(1, "Pago"))
                _FillingClassVoucherReport.Add(New Tuple(Of Integer, String)(2, "Reembolso"))
                _FillingClassVoucherReport.Add(New Tuple(Of Integer, String)(3, "Traslado"))
            End If
            Return _FillingClassVoucherReport
        End Get
    End Property

    Private criteria As String = Nothing

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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
    Public Property CurrencyId As Integer
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDSleCurrency.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTransactionVouchersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTransactionVouchersStart()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 5) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoTransactionVouchers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherTranscationReportTreasuryFilter, criteria)
            INDSleTransactionVouchersStart.Datasource = ProoftCloseXpoTransactionVouchers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTransactionVouchersEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTransactionVouchersEnd()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 5) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoTransactionVouchers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherTranscationReportTreasuryFilter, criteria)
            INDSleVouchersTransactionEnd.Datasource = ProoftCloseXpoTransactionVouchers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueStartBank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoStartBank()
        Using msearch As New MBusqueda
            ProoftCloseXpoStartBank = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountsReport)
            INDSleBankStarta.Datasource = ProoftCloseXpoStartBank
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueEndBank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEndBank()
        Using msearch As New MBusqueda
            ProoftCloseXpoEndBank = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountsReport)
            INDSleBankEnd1a.Datasource = ProoftCloseXpoEndBank
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueStartCash
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoStartCash = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegistersEntity)
            INDSleCashStart1a.Datasource = ProoftCloseXpoStartCash
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueEndCash
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoEndCash = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegistersEntity)
            INDSleCashEnd1a.Datasource = ProoftCloseXpoEndCash
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCreationUsersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsersStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleCreationUsersStart.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCreationUsersEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsersEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleCreationUsersEnd.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleScheduled
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoScheduled()
        Using msearch As New MBusqueda
            ProoftCloseXpoScheduled = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSchedulePaymentConfirm)
            INDSleScheduled.Datasource = ProoftCloseXpoScheduled
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrency
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrency()
        ProoftCloseXpoCurrency = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.GetCurrency()
        INDsleCurrency.Properties.DataSource = ProoftCloseXpoCurrency
    End Sub
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True

        'Original
        'If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
        '    Me.INDDateEnd.Focus()
        '    Validations = False
        'End If

        'Valida si las fechas son nulas y si la planilla esta vacia
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            If Me.INDSleScheduled.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateStart.Focus()
                Validations = False
            End If
        End If

        'Valida si la fecha inicial es mayor a la inicial
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            If Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If
        ElseIf (INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleScheduled.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'Valida Terceros
        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        'Valida Comprobantes De Egreso
        If INDSleTransactionVouchersStart.EditValue Is Nothing And INDSleVouchersTransactionEnd.EditValue IsNot Nothing Or INDSleVouchersTransactionEnd.EditValue Is Nothing And INDSleTransactionVouchersStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblVouchersTransaction.Text)
            Me.INDSleTransactionVouchersStart.Focus()
            Validations = False
        ElseIf INDSleVouchersTransactionEnd.EditValue < INDSleTransactionVouchersStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblVouchersTransaction.Text)
            Me.INDSleTransactionVouchersStart.Focus()
            Validations = False
        End If

        'Valida Usuarios
        If INDSleCreationUsersStart.EditValue Is Nothing And INDSleCreationUsersEnd.EditValue IsNot Nothing Or INDSleCreationUsersEnd.EditValue Is Nothing And INDSleCreationUsersStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCreationUsers.Text)
            Me.INDSleCreationUsersStart.Focus()
            Validations = False
        ElseIf INDSleCreationUsersEnd.EditValue < INDSleCreationUsersStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCreationUsers.Text)
            Me.INDSleCreationUsersStart.Focus()
            Validations = False
            'valida Cuentas
        ElseIf INDSleCashStart1a.EditValue IsNot Nothing And INDSleCashEnd1a.EditValue Is Nothing Or INDSleCashStart1a.EditValue Is Nothing And INDSleCashEnd1a.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDAccountLbl1a.Text)
            Me.INDSleCashStart1a.Focus()
            Validations = False
        ElseIf INDSleCashStart1a.EditValue > INDSleCashEnd1a.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDAccountLbl1a.Text)
            Me.INDSleCashStart1a.Focus()
            Validations = False
            'valida Bancos
        ElseIf INDSleBankStarta.EditValue IsNot Nothing And INDSleBankEnd1a.EditValue Is Nothing Or INDSleBankStarta.EditValue Is Nothing And INDSleBankEnd1a.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblBank1a.Text)
            Me.INDSleBankStarta.Focus()
            Validations = False
        ElseIf INDSleBankStarta.EditValue > INDSleBankEnd1a.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblBank1a.Text)
            Me.INDSleBankStarta.Focus()
            Validations = False
        ElseIf CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
                Me.INDSleCurrency.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable
        Dim filtroConsulta As String = Nothing
        'Se filtra por Fechas
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "GetDate(DocumentDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"
        End If

        'Se filtra por Terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                filtroConsulta &= "IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        'Se filtra por Comprobantes
        If INDSleTransactionVouchersStart.EditValue IsNot Nothing And INDSleVouchersTransactionEnd.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND Code >= '" & INDSleTransactionVouchersStart.EditValue & "' AND Code <= '" & INDSleVouchersTransactionEnd.EditValue & "'"
            Else
                filtroConsulta &= "Code >= '" & INDSleTransactionVouchersStart.EditValue & "' AND Code <= '" & INDSleVouchersTransactionEnd.EditValue & "'"
            End If
        End If

        'Se filtra por Usuarios
        If INDSleCreationUsersStart.EditValue IsNot Nothing And INDSleCreationUsersEnd.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND CreationUser >= '" & INDSleCreationUsersStart.EditValue & "' AND CreationUser <= '" & INDSleCreationUsersEnd.EditValue & "'"
            Else
                filtroConsulta &= "CreationUser >= '" & INDSleCreationUsersStart.EditValue & "' AND CreationUser <= '" & INDSleCreationUsersEnd.EditValue & "'"
            End If
        End If

        'Se filtra por Plantilla
        If INDSleScheduled.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND SchedulePaymentId.Id = " & INDSleScheduled.EditValue
            Else
                filtroConsulta &= "SchedulePaymentId.Id = " & INDSleScheduled.EditValue
            End If
        End If

        'Se filtra por Bancos
        If INDSleBankStarta.EditValue IsNot Nothing And INDSleBankEnd1a.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdEntityBankAccount.Code >= '" & INDSleBankStarta.EditValue & "' AND IdEntityBankAccount.Code <= '" & INDSleBankEnd1a.EditValue & "'"
            Else
                filtroConsulta &= "IdEntityBankAccount.Code >= '" & INDSleBankStarta.EditValue & "' AND IdEntityBankAccount.Code <= '" & INDSleBankEnd1a.EditValue & "'"
            End If
        End If

        'Se filtra por Cuenta
        If INDSleCashStart1a.EditValue IsNot Nothing And INDSleCashEnd1a.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdCashRegister.Code >= '" & INDSleCashStart1a.EditValue & "' AND IdCashRegister.Code <= '" & INDSleCashEnd1a.EditValue & "'"
            Else
                filtroConsulta &= "IdCashRegister.Code >= '" & INDSleCashStart1a.EditValue & "' AND IdCashRegister.Code <= '" & INDSleCashEnd1a.EditValue & "'"
            End If
        End If

        'INDGleGroupingReport

        If INDGleClassVoucher.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND VoucherClass = " & INDGleClassVoucher.EditValue
            Else
                filtroConsulta &= "VoucherClass = " & INDGleClassVoucher.EditValue
            End If
        End If
        'Moneda
        If CurrencyId > 0 Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND CurrencyId = " & INDsleCurrency.EditValue
            Else
                filtroConsulta &= "CurrencyId = " & INDsleCurrency.EditValue
            End If
        End If


        If INDGleStatusReport.EditValue <> 5 Then 'Le agrego la condicion de "ParametrosReporte(3) <> 5" porque el 5 representa a Todos en el estado pero en la BD no existe lo cual antes no me traia nada cuando le daba el estado de Todos=5.
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND Status = " & INDGleStatusReport.EditValue
            Else
                filtroConsulta &= "Status = " & INDGleStatusReport.EditValue
            End If
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Código Caja / Banco")
        dt.Columns.Add("Nombre Caja / Banco")
        dt.Columns.Add("No Cheque")
        dt.Columns.Add("Forma Pago")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Valor", GetType(Decimal))
        dt.Columns.Add("Moneda")

        For Each itemView In IndList
            Dim codeCashBank As String
            Dim nameCashBank As String
            If itemView.IdCashRegister Is Nothing Then
                codeCashBank = itemView.IdEntityBankAccount.Code
                nameCashBank = itemView.IdEntityBankAccount.IdBank.Name
            Else
                codeCashBank = itemView.IdCashRegister.Code
                nameCashBank = itemView.IdCashRegister.Name
            End If
            Dim paymentMethodText As String = String.Empty
            Select Case itemView.PaymentMethod
                Case 1
                    paymentMethodText = "Cheque"
                Case 2
                    paymentMethodText = "Nota Débito"
                Case Else
                    paymentMethodText = "Efectivo"
            End Select

            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.Code
            row.Item("Fecha") = itemView.DocumentDate
            row.Item("Nit Tercero") = itemView.IdThirdParty?.Nit
            row.Item("Nombre Tercero") = itemView.IdThirdParty?.Name
            row.Item("Código Cuenta Contable") = itemView.IdMainAccount.Number
            row.Item("Nombre Cuenta Contable") = itemView.IdMainAccount.Name
            row.Item("Código Caja / Banco") = codeCashBank
            row.Item("Nombre Caja / Banco") = nameCashBank
            row.Item("No Cheque") = itemView.CheckNumber
            row.Item("Forma Pago") = paymentMethodText
            row.Item("Estado") = itemView.Status
            row.Item("Valor") = itemView.Value
            row.Item("Moneda") = itemView.CurrencyAbbreviation
            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasourceDetailed() As DataTable
        Dim filtroConsulta As String = Nothing
        'Se filtra por Fechas
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "GetDate(IdVoucherTransaction.DocumentDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(IdVoucherTransaction.DocumentDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"
        End If

        'Se filtra por Terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdVoucherTransaction.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                filtroConsulta &= "IdVoucherTransaction.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdVoucherTransaction.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        'Se filtra por Comprobantes
        If INDSleTransactionVouchersStart.EditValue IsNot Nothing And INDSleVouchersTransactionEnd.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.Code >= '" & INDSleTransactionVouchersStart.EditValue & "' AND IdVoucherTransaction.Code <= '" & INDSleVouchersTransactionEnd.EditValue & "'"
            Else
                filtroConsulta &= "IdVoucherTransaction.Code >= '" & INDSleTransactionVouchersStart.EditValue & "' AND IdVoucherTransaction.Code <= '" & INDSleVouchersTransactionEnd.EditValue & "'"
            End If
        End If

        'Se filtra por Usuarios
        If INDSleCreationUsersStart.EditValue IsNot Nothing And INDSleCreationUsersEnd.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.CreationUser >= '" & INDSleCreationUsersStart.EditValue & "' AND IdVoucherTransaction.CreationUser <= '" & INDSleCreationUsersEnd.EditValue & "'"
            Else
                filtroConsulta &= "IdVoucherTransaction.CreationUser >= '" & INDSleCreationUsersStart.EditValue & "' AND IdVoucherTransaction.CreationUser <= '" & INDSleCreationUsersEnd.EditValue & "'"
            End If
        End If

        'Se filtra por Plantilla
        If INDSleScheduled.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.SchedulePaymentId.Id = " & INDSleScheduled.EditValue
            Else
                filtroConsulta &= "IdVoucherTransaction.SchedulePaymentId.Id = " & INDSleScheduled.EditValue
            End If
        End If

        'Se filtra por Bancos
        If INDSleBankStarta.EditValue IsNot Nothing And INDSleBankEnd1a.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.IdEntityBankAccount.Code >= '" & INDSleBankStarta.EditValue & "' AND IdVoucherTransaction.IdEntityBankAccount.Code <= '" & INDSleBankEnd1a.EditValue & "'"
            Else
                filtroConsulta &= "IdVoucherTransaction.IdEntityBankAccount.Code >= '" & INDSleBankStarta.EditValue & "' AND IdVoucherTransaction.IdEntityBankAccount.Code <= '" & INDSleBankEnd1a.EditValue & "'"
            End If
        End If

        'Se filtra por Cuenta
        If INDSleCashStart1a.EditValue IsNot Nothing And INDSleCashEnd1a.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.IdCashRegister.Code >= '" & INDSleCashStart1a.EditValue & "' AND IdVoucherTransaction.IdCashRegister.Code <= '" & INDSleCashEnd1a.EditValue & "'"
            Else
                filtroConsulta &= "IdVoucherTransaction.IdCashRegister.Code >= '" & INDSleCashStart1a.EditValue & "' AND IdVoucherTransaction.IdCashRegister.Code <= '" & INDSleCashEnd1a.EditValue & "'"
            End If
        End If

        'INDGleGroupingReport

        If INDGleClassVoucher.EditValue IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.VoucherClass = " & INDGleClassVoucher.EditValue
            Else
                filtroConsulta &= "IdVoucherTransaction.VoucherClass = " & INDGleClassVoucher.EditValue
            End If
        End If


        If INDGleStatusReport.EditValue <> 5 Then 'Le agrego la condicion de "ParametrosReporte(3) <> 5" porque el 5 representa a Todos en el estado pero en la BD no existe lo cual antes no me traia nada cuando le daba el estado de Todos=5.
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.Status = " & INDGleStatusReport.EditValue
            Else
                filtroConsulta &= "IdVoucherTransaction.Status = " & INDGleStatusReport.EditValue
            End If
        End If
        'Moneda
        If CurrencyId > 0 Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdVoucherTransaction.CurrencyId = " & INDsleCurrency.EditValue
            Else
                filtroConsulta &= "IdVoucherTransaction.CurrencyId = " & INDsleCurrency.EditValue
            End If
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionDetailsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Código Caja / Banco")
        dt.Columns.Add("Nombre Caja / Banco")
        dt.Columns.Add("No Cheque")
        dt.Columns.Add("Forma Pago")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Código Concepto")
        dt.Columns.Add("Nombre Concepto")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))
        dt.Columns.Add("Moneda")

        For Each itemView In IndList
            Dim codeCashBank As String
            Dim nameCashBank As String
            If itemView.IdVoucherTransaction.IdCashRegister Is Nothing Then
                codeCashBank = itemView.IdVoucherTransaction.IdEntityBankAccount.Code
                nameCashBank = itemView.IdVoucherTransaction.IdEntityBankAccount.IdBank.Name
            Else
                codeCashBank = itemView.IdVoucherTransaction.IdCashRegister.Code
                nameCashBank = itemView.IdVoucherTransaction.IdCashRegister.Name
            End If

            Dim thirdPartyNit As String = String.Empty
            Dim thirdPartyName As String = String.Empty
            If itemView.IdThirdParty IsNot Nothing Then
                thirdPartyNit = itemView.IdThirdParty.Nit
                thirdPartyName = itemView.IdThirdParty.Name
            ElseIf itemView.IdVoucherTransaction.IdThirdParty IsNot Nothing Then
                thirdPartyNit = itemView.IdVoucherTransaction.IdThirdParty.Nit
                thirdPartyName = itemView.IdVoucherTransaction.IdThirdParty.Name
            End If

            Dim paymentMethodText As String = String.Empty
            Select Case itemView.IdVoucherTransaction.PaymentMethod
                Case 1
                    paymentMethodText = "Cheque"
                Case 2
                    paymentMethodText = "Nota Débito"
                Case Else
                    paymentMethodText = "Efectivo"
            End Select

            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.IdVoucherTransaction.Code
            row.Item("Fecha") = itemView.IdVoucherTransaction.DocumentDate
            row.Item("Código Caja / Banco") = codeCashBank
            row.Item("Nombre Caja / Banco") = nameCashBank
            row.Item("No Cheque") = itemView.IdVoucherTransaction.CheckNumber
            row.Item("Forma Pago") = paymentMethodText
            row.Item("Estado") = itemView.IdVoucherTransaction.Status
            row.Item("Código Concepto") = If(IsNothing(itemView.IdExpenseConcept) = True, "", itemView.IdExpenseConcept.Code)
            row.Item("Nombre Concepto") = If(IsNothing(itemView.IdExpenseConcept) = True, "", itemView.IdExpenseConcept.Description)
            row.Item("Código Cuenta Contable") = itemView.IdMainAccount.Number
            row.Item("Nombre Cuenta Contable") = itemView.IdMainAccount.Name
            row.Item("Nit Tercero") = thirdPartyNit
            row.Item("Nombre Tercero") = thirdPartyName
            row.Item("Naturaleza") = If(itemView.Nature = 1, "Debito", "Credito")
            row.Item("Valor") = itemView.Value
            row.Item("Moneda") = itemView.IdVoucherTransaction.CurrencyAbbreviation
            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

#Region "Eventes"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoCreationUsers = Nothing
        ProoftCloseXpoEndBank = Nothing
        ProoftCloseXpoEndCash = Nothing
        ProoftCloseXpoScheduled = Nothing
        ProoftCloseXpoStartBank = Nothing
        ProoftCloseXpoStartCash = Nothing
        ProoftCloseXpoThirdParty = Nothing
        ProoftCloseXpoTransactionVouchers = Nothing
        ProoftCloseXpoCurrency = Nothing
    End Sub
    Private Sub INDSleBankStarta_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBankStarta.QueryPopUp
        If INDSleBankStarta.Datasource Is Nothing Then
            LoadXpoStartBank()
        End If
    End Sub

    Private Sub INDSleBankEnd1a_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBankEnd1a.QueryPopUp
        If INDSleBankEnd1a.Datasource Is Nothing Then
            LoadXpoEndBank()
        End If
    End Sub

    Private Sub INDSleCashStart1a_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashStart1a.QueryPopUp
        If INDSleCashStart1a.Datasource Is Nothing Then
            LoadXpoCashStart()
        End If
    End Sub

    Private Sub INDSleCashEnd1a_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashEnd1a.QueryPopUp
        If INDSleCashEnd1a.Datasource Is Nothing Then
            LoadXpoCashEnd()
        End If
    End Sub
    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            LoadXpoCurrency()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerareReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerareReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            If INDGleTypeReport.EditValue = 1 Then
                Dim reporte As New rptDisbursementVoucher

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue,
                                             INDGleGroupingReport.EditValue,
                                             INDGleStatusReport.EditValue,
                                             INDSleThirdPartyStart.EditValue,
                                             INDSleThirdPartyEnd.EditValue,
                                             INDSleTransactionVouchersStart.EditValue,
                                             INDSleVouchersTransactionEnd.EditValue,
                                             INDSleCreationUsersStart.EditValue,
                                             INDSleCreationUsersEnd.EditValue,
                                             INDSleScheduled.EditValue,
                                             INDSleBankStarta.EditValue,
                                             INDSleBankEnd1a.EditValue, INDSleCashStart1a.EditValue,
                                             INDSleCashEnd1a.EditValue,
                                             INDGleClassVoucher.EditValue,
                                             CurrencyId}


                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                'reporte.CreateDocument(True)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            Else
                If INDGleTypeReport.EditValue = 2 Then
                    Dim reporte As New rptDisbursementVoucherFormat

                    reporte.ParametrosReporte = {INDDateStart.EditValue,
                                                 INDDateEnd.EditValue,
                                                 INDGleStatusReport.EditValue,
                                                 INDSleThirdPartyStart.EditValue,
                                                 INDSleThirdPartyEnd.EditValue,
                                                 INDSleTransactionVouchersStart.EditValue,
                                                 INDSleVouchersTransactionEnd.EditValue,
                                                 INDSleCreationUsersStart.EditValue,
                                                 INDSleCreationUsersEnd.EditValue,
                                                 INDSleScheduled.EditValue,
                                                 CurrencyId}

                    INDDvViewReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    'reporte.CreateDocument(True)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                Else
                    If INDGleTypeReport.EditValue = 3 Then
                        Dim reporte As New rptDisbursementVoucherAttachedBill

                        reporte.ParametrosReporte = {INDDateStart.EditValue,
                                                     INDDateEnd.EditValue,
                                                     INDGleStatusReport.EditValue,
                                                     INDSleThirdPartyStart.EditValue,
                                                     INDSleThirdPartyEnd.EditValue,
                                                     INDSleTransactionVouchersStart.EditValue,
                                                     INDSleVouchersTransactionEnd.EditValue,
                                                     INDSleCreationUsersStart.EditValue,
                                                     INDSleCreationUsersEnd.EditValue,
                                                     INDSleScheduled.EditValue,
                                                     CurrencyId}


                        INDDvViewReport.DocumentSource = reporte
                        reporte.CargarDataSource()
                        'reporte.CreateDocument(True)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    Else
                        Dim reporte As New rptSubVoucherTransaction

                        reporte.ParametrosReporte = {INDDateStart.EditValue,
                                                     INDDateEnd.EditValue,
                                                     INDGleGroupingReport.EditValue,
                                                     INDGleStatusReport.EditValue,
                                                     INDSleThirdPartyStart.EditValue,
                                                     INDSleThirdPartyEnd.EditValue,
                                                     INDSleTransactionVouchersStart.EditValue,
                                                     INDSleVouchersTransactionEnd.EditValue,
                                                     INDSleCreationUsersStart.EditValue,
                                                     INDSleCreationUsersEnd.EditValue,
                                                     INDSleScheduled.EditValue,
                                                     INDSleBankStarta.EditValue,
                                                     INDSleBankEnd1a.EditValue,
                                                     INDSleCashStart1a.EditValue,
                                                     INDSleCashEnd1a.EditValue,
                                                     INDGleClassVoucher.EditValue,
                                                     CurrencyId}


                        INDDvViewReport.DocumentSource = reporte
                        reporte.CargarDataSource()
                        'reporte.CreateDocument(True)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            reporte.CreateDocument(True)
                        End If
                        AsyncLoader(False)
                        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                            AsyncLoader(False)
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcViewReport.Visible = True
                            INDDvViewReport.Show()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                            Me.INDDateStart.Focus()
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del Control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDisbursementVoucher_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleGroupingReport.Properties.DataSource = FillingGroupingReport
        Me.INDGleClassVoucher.Properties.DataSource = FillingClassVoucherReport

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 5
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleGroupingReport.EditValue = 1
        LoadXpoCurrency()
        CurrencyId = IndigoSessionValues.OfficialCurrencyId

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleTransactionVoucherStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTransactionVouchersStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTransactionVouchersStart.QueryPopUp
        If INDSleTransactionVouchersStart.Datasource Is Nothing Then
            LoadXpoTransactionVouchersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleTransactionVoucherEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleVouchersTransactionEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleVouchersTransactionEnd.QueryPopUp
        If INDSleVouchersTransactionEnd.Datasource Is Nothing Then
            LoadXpoTransactionVouchersEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCreationUserStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCreationUsersStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCreationUsersStart.QueryPopUp
        If INDSleCreationUsersStart.Datasource Is Nothing Then
            LoadXpoCreationUsersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCreationUserEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCreationUsersEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCreationUsersEnd.QueryPopUp
        If INDSleCreationUsersEnd.Datasource Is Nothing Then
            LoadXpoCreationUsersEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleScheduled
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleScheduled_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleScheduled.QueryPopUp
        If INDSleScheduled.Datasource Is Nothing Then
            LoadXpoScheduled()
        End If
    End Sub

    Private Sub INDGleStatusReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatusReport.EditValueChanged
        LoadXpoTransactionVouchersStart()
        LoadXpoTransactionVouchersEnd()
    End Sub

    Private Sub INDSleThirdPartyStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyStart.EditValueChanged
        LoadXpoTransactionVouchersStart()
        LoadXpoTransactionVouchersEnd()
    End Sub

    Private Sub INDSleThirdPartyEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyEnd.EditValueChanged
        LoadXpoTransactionVouchersStart()
        LoadXpoTransactionVouchersEnd()
    End Sub

    Private Sub FrmReportDisbursementVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleTransactionVouchersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVoucherTransactionByCode
        Me.INDSleVouchersTransactionEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVoucherTransactionByCode
        Me.INDSleCreationUsersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
        Me.INDSleCreationUsersEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
        Me.INDSleScheduled.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSchedulePaymentTreasury
        Me.INDSleBankStarta.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleBankEnd1a.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleCashStart1a.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCashEnd1a.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub INDGleGroupingReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGroupingReport.EditValueChanged
        If INDGleGroupingReport.EditValue = 1 Then
            INDLciClassVoucher.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDGleClassVoucher.EditValue = 1
            'INDLciStartAccount1a.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'Me.INDSleCashStart1a.EditValue = Nothing
            'INDLciEndAccount1a.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'Me.INDSleCashEnd1a.EditValue = Nothing
            'INDLblAccount1a.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciClassVoucher.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDGleClassVoucher.EditValue = Nothing
            'INDLciStartAccount1a.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'Me.INDSleCashStart1a.EditValue = Nothing
            'INDLciEndAccount1a.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'Me.INDSleCashEnd1a.EditValue = Nothing
            'INDLblAccount1a.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            If INDGleTypeReport.EditValue = 4 Then
                INDGcExportExcell.DataSource = chargueDatasourceDetailed()
            Else
                INDGcExportExcell.DataSource = chargueDatasource()
            End If
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged

        If INDGleTypeReport.EditValue = 4 Then
            INDLciGroupingReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleGroupingReport.EditValue = Nothing
        Else
            INDLciGroupingReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDGleGroupingReport.EditValue = 1
        End If

    End Sub
#End Region

End Class