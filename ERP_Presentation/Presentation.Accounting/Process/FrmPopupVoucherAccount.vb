#Region "Imports"
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP

#End Region

Public Class FrmPopupVoucherAccount
    Implements Presentation.Base.IcrudBase

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="_journalVouchers"></param>
    Public Sub New(_journalVouchers As JournalVouchers)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        JournalVouchers = _journalVouchers
    End Sub

#End Region

#Region "Fields"
    ''' <summary>
    ''' Nombre del módulo al cual pertenece el frm 
    ''' </summary>
    Private Const NAME_MODULE = "Accounting"

    ''' <summary>
    ''' Permite saber si se cambia el valor de los search, ya que cuando se asigna desde el loadControls debe traer lo que ya se ha registrado
    ''' </summary>
    Private _isLoad As Boolean = False

    ''' <summary>
    ''' Representa la entidad de la cabecera del documento contable
    ''' </summary>
    ''' <remarks></remarks>
    Dim JournalVouchers As JournalVouchers

    ''' <summary>
    ''' evento para agregar un detalle del documento contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddDocumentAccounting(sender As Object, e As AddDocumentAccountingEventArgs)

    ''' <summary>
    ''' evento para poner en null la entidad de doucumento contable cuando se presione el bton de deshacer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event DocumentAccountingNothing(sender As Object, e As EventArgs)

    ''' <summary>
    ''' propiedad para establecer el datasource de las cuentas
    ''' </summary>
    ''' <value>
    ''' The accoun xpo.
    ''' </value>
    Private Property AccounXpo As XPInstantFeedbackSource
        Get
            Return CType(indGlPuc.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            indGlPuc.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the third xpo.
    ''' </summary>
    ''' <value>
    ''' The third xpo.
    ''' </value>
    Private Property ThirdXpo As XPInstantFeedbackSource
        Get
            Return CType(indglThird.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            indglThird.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the cos center.
    ''' </summary>
    ''' <value>
    ''' The cos center.
    ''' </value>
    Private Property CosCenterXpo As XPInstantFeedbackSource
        Get
            Return CType(indglCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            indglCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the retention xpo.
    ''' </summary>
    ''' <value>
    ''' The retention xpo.
    ''' </value>
    Private Property RetentionXpo As XPInstantFeedbackSource
        Get
            Return CType(indGlRetention.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            indGlRetention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the _ puc.
    ''' </summary>
    ''' <value>
    ''' The _ puc.
    ''' </value>
    Private Property _Puc As MainAccounts

    Private _NameDocument As String

    ''' <summary>
    ''' Gets or sets the name document.
    ''' </summary>
    ''' <value>
    ''' The name document.
    ''' </value>
    Public Property NameDocument As String
        Get
            Return _NameDocument
        End Get
        Set(value As String)
            _NameDocument = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de comprobantes diarios detallado
    ''' </summary>
    Public listDetail As List(Of JournalVoucherDetails)

    ''' <summary>
    ''' Propiedad para acceder a un objeto específico de la clase JournalVoucherDetails
    ''' </summary>
    ''' <returns></returns>
    Public Property RowItem As JournalVoucherDetails

    ''' <summary>
    ''' Variable que almacena un objeto de la clase JournalVoucherDetails
    ''' </summary>
    Dim AccountDocument As JournalVoucherDetails

    ''' <summary>
    ''' The list nature account
    ''' </summary>
    Dim ListNatureAccount As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que almacena un objeto de la clase RetentionConcepts
    ''' </summary>
    Dim retention As RetentionConcepts

    ''' <summary>
    ''' Variable que será del tipo FrmAccountingVoucher
    ''' </summary>
    Public formParent As FrmAccountingVoucher

    ''' <summary>
    ''' Establece el valor de la variable _legalBook
    ''' </summary>
    Dim _legalBookId As Integer
    Public WriteOnly Property LegalBookId As Integer
        Set(value As Integer)
            _legalBookId = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Id de la cultura para setear las mascaras con el formato de la momenda que corresponda
    ''' </summary>
    Private _cultureId As Integer
    Public WriteOnly Property CultureId As Integer
        Set(value As Integer)
            _cultureId = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para agregar un item a la rejilla
    ''' </summary>
    Private Sub AddItem()
        If RowItem IsNot Nothing Then
            AccountDocument = RowItem
        Else
            AccountDocument = New JournalVoucherDetails
        End If

        With AccountDocument
            .CodeNameMainAccount = _Puc.Number & "-" & _Puc.Name
            .IdMainAccount = _Puc.Id
            If _Puc.HandlesThirdParty = True Then
                .IdThirdParty = indglThird.EditValue
                .CodeNameThirdParty = indglThird.Text
            End If
            If _Puc.HandlesCostCenter = True Then
                .idCostCenter = indglCostCenter.EditValue
                .CodeNameCostCenter = indglCostCenter.Text
            End If
            If indGlNatureVal.EditValue = 1 Then
                .DebitValue = Convert.ToDecimal(indTxtDebit.EditValue)
                .CreditValue = 0
            Else
                .DebitValue = 0
                .CreditValue = Convert.ToDecimal(indTxtDebit.EditValue)
            End If
            If _Puc.HandleBase = True Then
                .BaseValue = Convert.ToDecimal(indTxtBaseValue.EditValue)
            End If
            .Nature = indGlNatureVal.EditValue
            If groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IdRetention = indGlRetention.EditValue
                .CodeNameRetention = indGlRetention.Text
                .RetentionRate = indSpinPercentage.EditValue
                .BaseValue = CDec(indTxtBaseVoice.EditValue)
                .BillingValue = CDec(indGlInvoicedValue.EditValue)
            Else
                .IdRetention = Nothing
                .CodeNameRetention = String.Empty
                .RetentionRate = Nothing
            End If
            .Detail = indMemoObservations.Text
        End With
        Dim args As New AddDocumentAccountingEventArgs
        args.DocumentAccounting = AccountDocument
        RaiseEvent AddDocumentAccounting(Nothing, args)
        ClearOnlyValues()
    End Sub

    ''' <summary>
    '''Metodo para validar el item que se quiere agregar
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateItem() As Boolean
        If indGlPuc.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectAccount")
            indGlPuc.Focus()
            Return False
        End If
        If _Puc.HandlesThirdParty = True Then
            If indglThird.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectThird")
                indglThird.Focus()
                Return False
            End If
        End If
        If _Puc.HandlesCostCenter = True Then
            If indglCostCenter.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectCostCenter")
                indglCostCenter.Focus()
                Return False
            End If
        End If

        If _Puc.HandleBase = True Then
            If indTxtBaseValue.EditValue > indTxtDebit.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectBaseValue")
                indTxtBaseValue.Focus()
                Return False
            End If
        End If

        If groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If indGlRetention.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectRetention", "Accounting")
                Return False
            End If
            If indGlNature.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NatureRetention")
                Return False
            End If
            If indTxtBaseVoice.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BaseVal", "Accounting")
                Return False
            End If
        End If

        If indTxtDebit.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DebitCreditDistinc")
            indGlNatureVal.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Limpia el grupal de retención.
    ''' </summary>
    Private Sub CleanGroupRetention()
        If RowItem Is Nothing Then
            indGlRetention.Properties.ReadOnly = False
            indGlRetention.Properties.Buttons(0).Enabled = True
            indGlRetention.EditValue = Nothing
            indGlRetention.Properties.NullText = Nothing
            indSpinPercentage.EditValue = Nothing
            indGlNature.EditValue = 2
            indGlInvoicedValue.EditValue = Nothing
            indTxtBaseVoice.EditValue = Nothing
            indTxtVoiceRetention.EditValue = 0
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar los items
    ''' </summary>
    Private Sub GenerateNatureAccount()
        ListNatureAccount = New List(Of Tuple(Of Integer, String))
        ListNatureAccount.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        ListNatureAccount.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        indGlNature.Properties.DataSource = ListNatureAccount
        indGlNatureVal.Properties.DataSource = ListNatureAccount
    End Sub

    ''' <summary>
    ''' Método que calcula la retención.
    ''' </summary>
    Private Async Sub CalculateRetention(ByVal valueCalculate As Decimal)
        Try
            If valueCalculate = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BaseVal", "Accounting")
                Exit Sub
            End If
            Dim resulCalculateRetention As Decimal
            Select Case retention.Retention
                Case 1 'Fija
                    resulCalculateRetention = Math.Round(AccountingServices.CalculateRetention(valueCalculate, retention))
                Case 2 'Rangos
                    Using modelCompanySettings As New MCompanySettings("620")
                        Dim companySettings = Await modelCompanySettings.GetCompanySettings()
                        resulCalculateRetention = Math.Round(AccountingServices.CalculateRetention(valueCalculate, retention, companySettings.UVT))
                    End Using
                Case 3 'Variable
                    resulCalculateRetention = Math.Round(AccountingServices.CalculateRetention(valueCalculate, retention.MinBase, indSpinPercentage.EditValue))
            End Select
            indTxtVoiceRetention.EditValue = resulCalculateRetention
            indTxtDebit.EditValue = resulCalculateRetention
            If _Puc.FreelancerCategory = False Then
                indGlInvoicedValue.EditValue = valueCalculate
            End If
        Catch exindex As IndexOutOfRangeException
            Mensaje(EeventViewerImages.Advertencia) = exindex.Message
        Catch ex As ArgumentOutOfRangeException
            Mensaje(EeventViewerImages.Advertencia) = "El valor base no puede ser menor a la base mínima de la retención."
            indTxtBaseVoice.EditValue = Nothing
        Catch ex As ArgumentNullException
            Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
        End Try
    End Sub

#End Region

#Region "ICrud"
    ''' <summary>
    ''' Método del item buscar
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles de valor
    ''' </summary>
    Private Sub ClearOnlyValues()
        RowItem = Nothing
        indTxtDebit.EditValue = Nothing
        indTxtBaseValue.EditValue = Nothing
        indGlRetention.Properties.ReadOnly = False
        indGlRetention.Properties.Buttons(0).Enabled = True
        indGlInvoicedValue.EditValue = Nothing
        indTxtBaseVoice.EditValue = Nothing
        indTxtVoiceRetention.EditValue = 0
        indGlPuc.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de restablecer y limpiar valores en diferentes controles dentro del formulario
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        RowItem = Nothing
        retention = Nothing

        indGlPuc.EditValue = Nothing
        indGlPuc.Properties.NullText = String.Empty
        indGlPuc.Properties.ReadOnly = False
        indGlPuc.Properties.Buttons(0).Enabled = True

        indglThird.EditValue = Nothing
        indglThird.Properties.NullText = String.Empty
        indglThird.Properties.ReadOnly = False
        indglThird.Properties.Buttons(0).Enabled = True

        indglCostCenter.EditValue = Nothing
        indglCostCenter.Properties.NullText = String.Empty
        indglCostCenter.Properties.ReadOnly = False
        indglCostCenter.Properties.Buttons(0).Enabled = True

        indGlNatureVal.EditValue = 2
        indTxtDebit.EditValue = Nothing
        indTxtBaseValue.EditValue = Nothing
        indMemoObservations.Text = String.Empty
        CleanGroupRetention()
        indGlPuc.Focus()
        groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lyItemThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        indGlPuc.Enabled = True
        indglThird.Enabled = True
        indglCostCenter.Enabled = True
        indGlRetention.Enabled = True
        indSpinPercentage.Enabled = True
        indGlNature.Enabled = True
        'indGlInvoicedValue.Enabled = True
        indTxtBaseVoice.Enabled = True
        'indTxtVoiceRetention.Enabled = True
        indGlNatureVal.Enabled = True
        indTxtDebit.Enabled = True
        indMemoObservations.Enabled = True
        indGlNatureVal.Enabled = True
        indTxtDebit.Enabled = True
        indTxtBaseValue.Enabled = True
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Metodo para establecer la lógica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub
#End Region

#Region "Events"

#Region "Activated"

    ''' <summary>
    ''' Evento que se ejecuta cuando se activa el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupVoucherAccount_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'Valida el control cuenta contable
        If indGlPuc.EditValue Is Nothing Then
            indGlPuc.Focus()
        End If
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmPopupVoucherAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopupVoucherAccount_Load(sender As Object, e As EventArgs) Handles Me.Load
        indTxtNameVoucher.Text = _NameDocument
        '******************************************************************************************************
        'se establece la cultura especifica Solo a los controles de texto que muestran valores con formato moneda
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Me._cultureId).NumberFormat
        indGlInvoicedValue.Properties.Mask.Culture = _culture
        indTxtBaseVoice.Properties.Mask.Culture = _culture
        indTxtVoiceRetention.Properties.Mask.Culture = _culture
        indTxtDebit.Properties.Mask.Culture = _culture
        indTxtBaseValue.Properties.Mask.Culture = _culture
        '******************************************************************************************************
        GenerateNatureAccount()

        If RowItem IsNot Nothing Then

            'Se asigna el valor a esta bandera para no cambiar el valor del control de la naturaleza
            _isLoad = True

            indGlPuc.EditValue = RowItem.IdMainAccount
            indGlPuc.Properties.NullText = RowItem.CodeNameMainAccount

            indglThird.EditValue = RowItem.IdThirdParty
            indglThird.Properties.NullText = RowItem.CodeNameThirdParty

            indglCostCenter.EditValue = RowItem.IdCostCenter
            indglCostCenter.Properties.NullText = RowItem.CodeNameCostCenter

            If JournalVouchers Is Nothing Then
                indglThird.Properties.ReadOnly = False
                indglThird.Properties.Buttons(0).Enabled = True

                indGlPuc.Properties.ReadOnly = False
                indGlPuc.Properties.Buttons(0).Enabled = True

                indglCostCenter.Properties.ReadOnly = False
                indglCostCenter.Properties.Buttons(0).Enabled = True
            ElseIf JournalVouchers.Status <> 1 Then
                indglThird.Properties.ReadOnly = True
                indglThird.Properties.Buttons(0).Enabled = False

                indGlPuc.Properties.ReadOnly = True
                indGlPuc.Properties.Buttons(0).Enabled = False

                indglCostCenter.Properties.ReadOnly = True
                indglCostCenter.Properties.Buttons(0).Enabled = False
            Else
                indglThird.Properties.ReadOnly = False
                indglThird.Properties.Buttons(0).Enabled = True

                indGlPuc.Properties.ReadOnly = False
                indGlPuc.Properties.Buttons(0).Enabled = True

                indglCostCenter.Properties.ReadOnly = False
                indglCostCenter.Properties.Buttons(0).Enabled = True
            End If

            indGlNatureVal.EditValue = RowItem.Nature
            If RowItem.CreditValue = 0 Then
                indTxtDebit.EditValue = RowItem.DebitValue
            Else
                indTxtDebit.EditValue = RowItem.CreditValue
            End If

            indTxtBaseValue.EditValue = RowItem.BaseValue
            indMemoObservations.Text = RowItem.Detail

            If RowItem.IdRetention <> 0 Then
                indGlRetention.EditValue = CInt(RowItem.IdRetention)
                indGlRetention.Properties.NullText = RowItem.CodeNameRetention
                indGlRetention.Properties.ReadOnly = True
                indGlRetention.Properties.Buttons(0).Enabled = False
                indSpinPercentage.EditValue = RowItem.RetentionRate
                indTxtBaseVoice.EditValue = RowItem.BaseValue
                indGlInvoicedValue.EditValue = RowItem.BillingValue
                indTxtVoiceRetention.EditValue = indTxtDebit.EditValue
                indGlNature.EditValue = RowItem.Nature

                indGlPuc.Focus()
            End If
        End If
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.Minimizar(True)
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que llama al método Deshacer al hacer clic en el control de limpiar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indBtnClear_Click(sender As Object, e As EventArgs)
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the indBtnAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub indBtnAdd_Click(sender As Object, e As EventArgs) Handles indBtnAdd.Click
        If ValidateItem() = True Then
            AddItem()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el campo "Observaciones"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indMemoObservations_KeyDown(sender As Object, e As KeyEventArgs) Handles indMemoObservations.KeyDown
        If e.KeyCode = Keys.Enter Then
            indBtnAdd.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el control "Naturaleza"
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub indGlNatureVal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indGlNatureVal.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If indTxtBaseValue.Visible Then
                indTxtBaseValue.Focus()
            Else
                indTxtDebit.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el campo "Valor Base" del grupo Valores
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub indTxtBaseValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indTxtBaseValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            indTxtDebit.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el campo "Observaciones"
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub indTxtDebit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indTxtDebit.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            indMemoObservations.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuata cuando se presiona la tecla "ESC" la cual llama el método Close.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopupVoucherAccount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el campo "Valor Base" del grupo Retenciones.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub indTxtBaseVoice_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indTxtBaseVoice.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If indGlNature.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectNature", "Accounting")
                Exit Sub
            End If
            'If retention IsNot Nothing Then
            'CalculateRetention()
            indMemoObservations.Focus()
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el control "Cuenta Contable"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlPuc_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indGlPuc.KeyDown
        If _Puc IsNot Nothing Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If _Puc.HandlesThirdParty = True Then
                    indglThird.Focus()
                    Exit Sub
                End If
                If _Puc.HandlesCostCenter = True Then
                    indglCostCenter.Focus()
                    Exit Sub
                End If
                If _Puc.HandlesCostCenter = False And _Puc.HandlesThirdParty = False And _Puc.RetencionType <> 0 Then
                    indGlRetention.Focus()
                Else
                    indGlNatureVal.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el control "Tercero"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indglThird_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indglThird.KeyDown
        If _Puc IsNot Nothing Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If lyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    indglCostCenter.Focus()
                ElseIf groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    indGlRetention.Focus()
                Else
                    indGlNatureVal.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se teclea sobre el control "Centro de Costo" 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indglCostCenter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indglCostCenter.KeyDown
        If _Puc IsNot Nothing Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If _Puc.RetencionType <> 0 Then
                    indGlRetention.Focus()
                Else
                    indGlNatureVal.Focus()
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor en el control "Cuenta Contable".
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub indGlPuc_EditValueChanged(sender As Object, e As EventArgs) Handles indGlPuc.EditValueChanged
        If indGlPuc.EditValue Is Nothing Then
            Exit Sub
        End If
        Dim modelPuc As New MPUC("602")
        If indGlPuc.EditValue.ToString() = "" Then
            Exit Sub
        End If
        _Puc = Await modelPuc.GetAccountById(indGlPuc.EditValue)
        If _Puc IsNot Nothing Then

            If _Puc.HandlesThirdParty = True Then
                lyItemThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lyItemThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                indglThird.EditValue = Nothing
            End If
            If _Puc.HandlesCostCenter = True Then
                lyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                indglCostCenter.EditValue = Nothing
            End If
            If _Puc.HandleBase = True Then
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                indTxtBaseValue.EditValue = 0
            End If

            If _Puc.RetencionType <> 0 Then
                groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                indGlNatureVal.Enabled = False
                indTxtDebit.Enabled = False
                indTxtBaseValue.Enabled = False
                CleanGroupRetention()
                If _Puc.RetencionType = 1 Or _Puc.RetencionType = 2 Then
                    indGlInvoicedValue.Enabled = True
                    If _Puc.FreelancerCategory Then
                        LayoutControlItem8.Text = "Total Ingresos"
                    Else
                        LayoutControlItem8.Text = "Valor Facturado"
                    End If
                End If
            Else
                indGlNatureVal.Enabled = True
                indTxtDebit.Enabled = True
                groupRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                indGlNatureVal.Enabled = True
                indTxtDebit.Enabled = True
                indTxtBaseValue.Enabled = True
                CleanGroupRetention()
            End If

            'Si la asignación del valor viene desde el cambio del control y no desde el loadControls
            If _isLoad = False Then
                'Se asigna la naturaleza de la cuenta
                indGlNature.EditValue = _Puc.Nature
            End If

            'Se vuelve asignar false ya que cuando el registro es cargado desde el loadControl primero asigna el valor al control y despues entra en este evento por el async
            _isLoad = False

        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Naturaleza"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlNature_EditValueChanged(sender As Object, e As EventArgs) Handles indGlNature.EditValueChanged
        indGlNatureVal.EditValue = indGlNature.EditValue
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Retención".
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub indGlRetention_EditValueChanged(sender As Object, e As EventArgs) Handles indGlRetention.EditValueChanged
        If indGlRetention.EditValue IsNot Nothing Then
            Using modelRetention As New MRetentionConcept("")
                retention = modelRetention.GetRetentionByIdSimple(indGlRetention.EditValue)
                If retention IsNot Nothing Then
                    Select Case retention.Retention
                        Case 1 'fija
                            indGlInvoicedValue.EditValue = Nothing
                            indTxtVoiceRetention.EditValue = 0
                            indSpinPercentage.EditValue = retention.Rate
                            indSpinPercentage.Enabled = False
                            indTxtBaseVoice.Enabled = True
                            indTxtBaseVoice.EditValue = Nothing
                            INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Case 2 'rangos
                            indGlInvoicedValue.EditValue = Nothing
                            indTxtBaseVoice.EditValue = Nothing
                            indTxtVoiceRetention.EditValue = 0
                            indTxtBaseVoice.Enabled = True
                            INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Case 3 'variable
                            indSpinPercentage.Enabled = True
                            indSpinPercentage.EditValue = Nothing
                            indTxtBaseVoice.EditValue = Nothing
                            indTxtBaseVoice.Enabled = False
                            INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End Select
                    If RowItem Is Nothing Then
                        indTxtDebit.EditValue = Nothing
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Valor Facturado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlInvoicedValue_EditValueChanged(sender As Object, e As EventArgs) Handles indGlInvoicedValue.EditValueChanged
        If indGlInvoicedValue.EditValue IsNot Nothing Then
            If _Puc IsNot Nothing AndAlso _Puc.FreelancerCategory = True Then
                If retention IsNot Nothing Then
                    CalculateRetention(indGlInvoicedValue.EditValue)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Valor Base" del grupo Retenciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indTxtBaseVoice_EditValueChanged(sender As Object, e As EventArgs) Handles indTxtBaseVoice.EditValueChanged
        If indTxtBaseVoice.EditValue IsNot Nothing Then
            If _Puc IsNot Nothing AndAlso _Puc.FreelancerCategory = False Then
                If retention IsNot Nothing Then
                    CalculateRetention(indTxtBaseVoice.EditValue)
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Porcentaje"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indSpinPercentage_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles indSpinPercentage.EditValueChanging
        If indSpinPercentage.EditValue IsNot Nothing Then
            If e.NewValue > 0 Then
                indTxtBaseVoice.Enabled = True
            Else
                indTxtBaseVoice.Enabled = False
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Este evento se activa cuando se hace clic en el botón dentro del control "Retencion"   
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlRetention_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indGlRetention.ButtonClick

        'Si el botón presionado es el botón "+", se crea un formulario para agregar un nuevo concepto de retención
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmRetentionConcept
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                Dim id = indGlRetention.EditValue
                indGlRetention.EditValue = Nothing
                indGlRetention.EditValue = id
                Using msearchas As New MBusqueda
                    RetentionXpo = msearchas.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando se hace clic en el botón dentro del control "Centro de Costo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indglCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indglCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                Using msearchas As New MBusqueda
                    RetentionXpo = msearchas.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando se hace clic en el botón dentro del control "Tercero"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indglThird_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indglThird.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                Dim size As System.Drawing.Size
                size.Width = 800
                size.Height = 700
                formulario.Size = size
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                Using msearch As New MBusqueda
                    ThirdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando se hace clic en el botón dentro del control "Cuenta Contable".
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub indGlPuc_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indGlPuc.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = FormStartPosition.CenterParent
                Formulario.Size = New System.Drawing.Size(800, 700)
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using msearch As New MBusqueda
                    Dim filter() As Object = {5, True}
                    AccounXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
                End Using
            End Using
        End If
    End Sub

#End Region

#Region "Leave"

    ''' <summary>
    '''  Este evento se activa cuando el control "Valor Base" del grupo retenciones pierde la propiedad focus
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indTxtBaseVoice_Leave(sender As Object, e As EventArgs) Handles indTxtBaseVoice.Leave
        If indTxtBaseVoice.EditValue = 0 And indGlRetention.EditValue Is Nothing Then
            'Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BaseVal", "Accounting")
            'Exit Sub
        Else
            If indGlNature.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectNature", "Accounting")
                Exit Sub
            End If
            'If _Puc IsNot Nothing AndAlso _Puc.FreelancerCategory = True Then
            '    If retention IsNot Nothing Then
            '        CalculateRetention()
            '    End If
            'End If
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Este evento se activa cuando el formulario está en proceso de cerrarse.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupVoucherAccount_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If temRowItem IsNot Nothing Then
        '    listDetail.Add(temRowItem)
        'End If

        If indGlPuc.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Querypopup"
    ''' <summary>
    ''' Handles the QueryPopUp event of the SearchLookUpEdit1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub SearchLookUpEdit1_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles indGlPuc.QueryPopUp
        If AccounXpo Is Nothing Then
            If indGlPuc.Properties.ReadOnly = False Then
                Using msearch As New MBusqueda
                    Dim filter() As Object = {5, True, _legalBookId}
                    AccounXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indglThird control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indglThird_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles indglThird.QueryPopUp
        If ThirdXpo Is Nothing Then
            If indglThird.Properties.ReadOnly = False Then
                Using msearch As New MBusqueda
                    ThirdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indglCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indglCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles indglCostCenter.QueryPopUp
        If CosCenterXpo Is Nothing Then
            If indglCostCenter.Properties.ReadOnly = False Then
                Using msearch As New MBusqueda
                    CosCenterXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlRetention control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlRetention_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles indGlRetention.QueryPopUp
        If RetentionXpo Is Nothing Then
            If indGlRetention.Properties.ReadOnly = False Then
                Using msearchas As New MBusqueda
                    RetentionXpo = msearchas.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
                End Using
            End If
        End If
    End Sub
#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Evento que llama al método Deshacer al hacer clic en el control "Deshacer"
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        RaiseEvent DocumentAccountingNothing(Nothing, EventArgs.Empty)
        Deshacer()
    End Sub

#End Region

End Class