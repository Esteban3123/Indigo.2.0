'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities.ObjectChangeTracker
Imports System.Windows.Forms
Imports Presentation.Payments.MVP

#End Region

Public Class FrmCompanySettings
    Implements IcrudBase

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"
#End Region

#Region "Fields"
    ''' <summary>
    ''' The model
    ''' </summary>
    Dim Model As MCompanySettings
    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' The company settings
    ''' </summary>
    Private companySettings As CompanySettings

    ''' <summary>
    ''' The load flag
    ''' </summary>
    Private _flagLoad As Boolean = False
#End Region

#Region "Datasource"

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos para lineas de negocio
    ''' </summary>
    Private _FillingBusinessLine As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingBusinessLine As List(Of Tuple(Of Byte, String))
        Get
            If _FillingBusinessLine Is Nothing Then
                _FillingBusinessLine = New List(Of Tuple(Of Byte, String))
                _FillingBusinessLine.Add(New Tuple(Of Byte, String)(1, "Aseguramiento Obligatorio"))
                _FillingBusinessLine.Add(New Tuple(Of Byte, String)(2, "Aseguramiento Voluntario"))
                _FillingBusinessLine.Add(New Tuple(Of Byte, String)(3, "Prestación de servicios"))
            End If
            Return _FillingBusinessLine
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos de control de ingresos laborales 
    ''' </summary>
    Private _WorkIncomeControlTuple As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property WorkIncomeControlTuple As List(Of Tuple(Of Byte, String))
        Get
            If _WorkIncomeControlTuple Is Nothing Then
                _WorkIncomeControlTuple = New List(Of Tuple(Of Byte, String))
                _WorkIncomeControlTuple.Add(New Tuple(Of Byte, String)(0, "Calcula UVT Anual"))
                _WorkIncomeControlTuple.Add(New Tuple(Of Byte, String)(1, "Calcula UVT Mensual"))
            End If
            Return _WorkIncomeControlTuple
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos de registro de impuestos 
    ''' </summary>
    Private _listTaxRegistration As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property TaxRegistrationTuple As List(Of Tuple(Of Byte, String))
        Get
            If _listTaxRegistration Is Nothing Then
                _listTaxRegistration = New List(Of Tuple(Of Byte, String))
                _listTaxRegistration.Add(New Tuple(Of Byte, String)(1, "IVA al Costo (Control Fiscal)"))
                _listTaxRegistration.Add(New Tuple(Of Byte, String)(2, "IVA Descontable"))
                _listTaxRegistration.Add(New Tuple(Of Byte, String)(3, "IVA Mixto"))
                _listTaxRegistration.Add(New Tuple(Of Byte, String)(4, "IVA al Costo"))
            End If
            Return _listTaxRegistration
        End Get
    End Property

    Private _listTransactionEconomicActivity As List(Of Tuple(Of Boolean, String))
    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla  del campo asociar actividad económica en transacciones
    ''' </summary>
    ''' <returns>_listTransactionEconomicActivity</returns>
    Private ReadOnly Property TransactionEconomicActivityTuple As List(Of Tuple(Of Boolean, String))
        Get
            If _listTransactionEconomicActivity Is Nothing Then
                _listTransactionEconomicActivity = New List(Of Tuple(Of Boolean, String))
                _listTransactionEconomicActivity.Add(New Tuple(Of Boolean, String)(1, "Si"))
                _listTransactionEconomicActivity.Add(New Tuple(Of Boolean, String)(0, "No"))
            End If
            Return _listTransactionEconomicActivity
        End Get
    End Property

#End Region

#Region "Property"
    ''' <summary>
    ''' Propiedad de Moneda Oficial
    ''' </summary>
    ''' <returns></returns>
    Private Property OfficialCurrencyId As Integer?
        Get
            Return INDSleOfficialCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleOfficialCurrency.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad libro id libro contable que guardar beneficio debido al cambio de moneda
    ''' </summary>
    ''' <returns></returns>
    Private Property ProfitExchangeId As Integer?
        Get
            Return INDsleProfit.EditValue
        End Get
        Set(value As Integer?)
            INDsleProfit.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad id Tipo contable para revalorizacion
    ''' </summary>
    ''' <returns></returns>
    Private Property ProfitLostJournalVoucherTypeId As Integer?
        Get
            Return INDsleProfitLostJournalVoucherType.EditValue
        End Get
        Set(value As Integer?)
            INDsleProfitLostJournalVoucherType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad libro id libro contable que guardar perdidas debido al cambio de moneda
    ''' </summary>
    ''' <returns></returns>
    Private Property LostExchangeId As Integer?
        Get
            Return INDsleLostAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleLostAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad centro de costo
    ''' </summary>
    ''' <returns></returns>
    Private Property CostCenterExchangeId As Integer?
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de Registro IVA
    ''' </summary>
    ''' <returns></returns>
    Private Property TaxRegistration As Byte
        Get
            Return INDsleTaxRegistration.EditValue
        End Get
        Set(value As Byte)
            INDsleTaxRegistration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de  asociar la Actividad económica en transacciones
    ''' </summary>
    ''' <returns></returns>
    Private Property TransactionEconomicActivity As Byte
        Get
            Return INDSleTransactionEconomicActivity.EditValue
        End Get
        Set(value As Byte)
            INDSleTransactionEconomicActivity.EditValue = value
        End Set
    End Property

#End Region

#Region "Events"

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
        companySettings = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmCompanySettings control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmCompanySettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadControls()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the indRgConsolidate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub indRgConsolidate_EditValueChanged(sender As Object, e As EventArgs) Handles indRgConsolidate.EditValueChanged
        If indRgConsolidate.EditValue Is Nothing OrElse indRgConsolidate.EditValue = 0 Then
            indDtLastConsolidate.EditValue = Nothing
            indDtLastConsolidate.Enabled = False
            indLciLastConsolidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            indLciLastConsolidate.AllowHide = True
            indLciLastConsolidate.ShowInCustomizationForm = True
        Else
            indDtLastConsolidate.Enabled = True
            indLciLastConsolidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            indLciLastConsolidate.AllowHide = False
            indLciLastConsolidate.ShowInCustomizationForm = False
        End If
    End Sub

    ''' <summary>
    ''' Evento para cuando se pone en null la cuenta de ganancia/perdida  para que deje el texto del combo vacio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleProfit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProfit.EditValueChanged, INDsleLostAccount.EditValueChanged
        If Me.ProfitExchangeId Is Nothing AndAlso Not String.IsNullOrEmpty(INDsleProfit.Properties.NullText) Then
            INDsleProfit.Properties.NullText = String.Empty
        End If
        If Me.LostExchangeId Is Nothing AndAlso Not String.IsNullOrEmpty(INDsleLostAccount.Properties.NullText) Then
            INDsleLostAccount.Properties.NullText = String.Empty
        End If

        'Valido para el campo del centro de costo
        Await ValidateCostCenter(Me.ProfitExchangeId, Me.LostExchangeId, Me._flagLoad)
    End Sub

    ''' <summary>
    ''' Valido si debe ir a consultar el centro de costo por id de acuerdo a la bandera de load
    ''' </summary>
    ''' <param name="ProfitExchangeId"></param>
    ''' <param name="LostExchangeId"></param>
    ''' <param name="flag"></param>
    ''' <returns></returns>
    Private Async Function ValidateCostCenter(ProfitExchangeId As Integer?, LostExchangeId As Integer?, flag As Boolean) As Task
        If flag Then
            Return
        End If
        Await GetCostCenterByMainAccountId(Me.ProfitExchangeId, Me.LostExchangeId)
    End Function

    ''' <summary>
    ''' Obtiene si la cuenta contable maneja centro de costo
    ''' </summary>
    ''' <param name="IdProfit"></param>
    ''' <param name="IdLost"></param>
    ''' <returns></returns>
    Public Async Function GetCostCenterByMainAccountId(IdProfit As Integer?, IdLost As Integer?) As Task
        Using Model As New MCompanySettings(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetHadleCostCenterByMainAccountId(IdProfit, IdLost)
            AsyncLoader(False)

            If resultOperation.StateResult Then
                INDsleCostCenter.Enabled = True
                INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciCostCenter.AllowHide = False
                INDLciCostCenter.ShowInCustomizationForm = False
                LoadCostCenter()
            Else
                INDsleCostCenter.EditValue = Nothing
                INDsleCostCenter.Enabled = False
                INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCostCenter.AllowHide = True
                INDLciCostCenter.ShowInCustomizationForm = True
            End If
        End Using
    End Function

#Region "QueryPopUp"
    ''' <summary>
    ''' Evento para consultar el maestro de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleOfficialCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleOfficialCurrency.QueryPopUp
        If INDSleOfficialCurrency.Properties.DataSource Is Nothing Then
            Using Model As New MCompanySettings("")
                INDSleOfficialCurrency.Properties.DataSource = Model.GetCurrencyDatasource()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento para consulta de las cuentas de ganancias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProfitLost_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProfit.QueryPopUp
        If INDsleProfit.Properties.DataSource Is Nothing Then
            Using Model As New MCompanySettings("")
                INDsleProfit.Properties.DataSource = Model.GetMainAccountsDatasource
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento de consulta para tipo de comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProfitLostJournalVoucherType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProfitLostJournalVoucherType.QueryPopUp
        If INDsleProfitLostJournalVoucherType.Properties.DataSource Is Nothing Then
            Using Model As New MCompanySettings("")
                INDsleProfitLostJournalVoucherType.Properties.DataSource = Model.GetJournalVoucherTypes
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento de consulta para la cuenta de perdida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLostAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLostAccount.QueryPopUp
        If INDsleLostAccount.Properties.DataSource Is Nothing Then
            Using Model As New MCompanySettings("")
                INDsleLostAccount.Properties.DataSource = Model.GetMainAccountsDatasource
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento de consulta para el centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            LoadCostCenter()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del contro de Centro de costo ajuste diferencial cambiario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(517, Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del centro de costo
    ''' </summary>
    Private Sub LoadCostCenter()
        Using Model As New MCompanySettings("")
            INDsleCostCenter.Properties.DataSource = Model.GetCostCentersDatasource
        End Using
    End Sub
#End Region
#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga y configura los controles en el formulario
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Try
            _flagLoad = True
            AsyncLoader(True)
            INDGleBusinessLine.Properties.DataSource = FillingBusinessLine
            INDGleWorkIncomeControl.Properties.DataSource = WorkIncomeControlTuple
            INDsleTaxRegistration.Properties.DataSource = TaxRegistrationTuple
            INDSleTransactionEconomicActivity.Properties.DataSource = TransactionEconomicActivityTuple

            Using Model = New MCompanySettings(Me.Tag)
                companySettings = Await Model.GetCompanySettings()
                If (companySettings IsNot Nothing And companySettings.Id <> 0) Then
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), companySettings.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), companySettings.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), companySettings.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), companySettings.ModificationDate)

                    indDtLastClose.EditValue = companySettings.LastClosingDate
                    indRgConsolidate.EditValue = companySettings.Consolidate
                    indDtLastConsolidate.EditValue = companySettings.ConsolidateDate
                    indTxtSMLV.Text = companySettings.SMLV
                    IndtxtUVT.Text = companySettings.UVT
                    INDGleBusinessLine.EditValue = companySettings.BusinessLine
                    INDGleWorkIncomeControl.EditValue = companySettings.WorkIncomeControl
                    TaxRegistration = companySettings.TaxRegistration
                    TransactionEconomicActivity = companySettings.TransactionEconomicActivity
                    BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    BarraBotones.SetDocuments(companySettings.Id)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    indDtLastClose.Enabled = False
                    indDtLastConsolidate.Enabled = False
                    Me.OfficialCurrencyId = companySettings.OfficialCurrencyId
                    INDSleOfficialCurrency.Properties.NullText = companySettings.CurrencyCodName
                    INDRgSalePrice.EditValue = companySettings.SalePriceIncludeTax
                    ProfitExchangeId = companySettings.ProfitLostByExchangeCurrencyAccountId
                    INDsleProfit.Properties.NullText = companySettings.ProfitAccountName
                    ProfitLostJournalVoucherTypeId = companySettings.ProfitLostJournalVoucherTypeId
                    INDsleProfitLostJournalVoucherType.Properties.NullText = companySettings.ProfitJournalVoucherName
                    INDRgThirdPartyCheckDigit.EditValue = companySettings.ThirdPartyCheckDigit
                    Me.LostExchangeId = companySettings.LostByExchangeCurrencyAccountId
                    Me.INDsleLostAccount.Properties.NullText = companySettings.LostAccountName
                    CostCenterExchangeId = companySettings?.CostCenterId
                    INDsleCostCenter.Properties.NullText = companySettings?.CostCenter?.Name
                    Await Me.ValidateCostCenter(Me.ProfitExchangeId, Me.LostExchangeId, False)
                Else
                    companySettings = New CompanySettings
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            End Using

            Using modelAccountPayable As New MAccountPayable(Tag)
                Dim resultBanAccountPayable As ActionResult(Of AccountPayable) = Await modelAccountPayable.GetCheckExistAccountPayable()

                If companySettings.Id = 0 AndAlso resultBanAccountPayable.StateResult = False Then
                    INDtxtConsecutiveFiling.EditValue = 1
                    INDtxtConsecutiveFiling.Properties.ReadOnly = False
                ElseIf companySettings.Id > 0 AndAlso resultBanAccountPayable.StateResult = False Then
                    INDtxtConsecutiveFiling.EditValue = companySettings.ConsecutiveFiling
                    INDtxtConsecutiveFiling.Properties.ReadOnly = False
                ElseIf companySettings.Id > 0 AndAlso resultBanAccountPayable.StateResult = True Then
                    INDtxtConsecutiveFiling.EditValue = companySettings.ConsecutiveFiling
                    INDtxtConsecutiveFiling.Properties.ReadOnly = True
                ElseIf companySettings.Id = 0 AndAlso resultBanAccountPayable.StateResult = True Then
                    Dim resultUltimate As ActionResult(Of AccountPayable) = Await modelAccountPayable.GetUltimateRegisterConsecutiveFiling()
                    If resultUltimate.StateResult = True Then
                        INDtxtConsecutiveFiling.EditValue = resultUltimate.ObjectEmbbeded.NumberFiling + 1
                        INDtxtConsecutiveFiling.Properties.ReadOnly = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _flagLoad = False
            AsyncLoader(False)
        End Try
    End Function
#End Region

#Region "Crud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssingValues()
        Try
            Using Model As New MCompanySettings(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveCompanySettings(companySettings)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    companySettings = Result.ObjectEmbbeded
                    If companySettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveMessage"))
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    BarraBotones.CleanAuditBasic()
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), companySettings.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), companySettings.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), companySettings.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), companySettings.ModificationDate)
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) IsNot Nothing Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.MessageResult(0).ToString()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
            Await LoadControls()
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssingValues()
        With companySettings
            .LastClosingDate = indDtLastClose.EditValue
            .Consolidate = indRgConsolidate.EditValue
            .ConsolidateDate = indDtLastConsolidate.EditValue
            .SMLV = indTxtSMLV.EditValue
            .UVT = IndtxtUVT.EditValue
            .ConsecutiveFiling = INDtxtConsecutiveFiling.EditValue
            .BusinessLine = INDGleBusinessLine.EditValue
            .WorkIncomeControl = INDGleWorkIncomeControl.EditValue
            .SalePriceIncludeTax = INDRgSalePrice.EditValue
            .OfficialCurrencyId = Me.OfficialCurrencyId
            .ProfitLostByExchangeCurrencyAccountId = Me.ProfitExchangeId
            .ProfitLostJournalVoucherTypeId = Me.ProfitLostJournalVoucherTypeId
            .ThirdPartyCheckDigit = INDRgThirdPartyCheckDigit.EditValue
            .LostByExchangeCurrencyAccountId = Me.LostExchangeId
            .CostCenterId = Me.CostCenterExchangeId
            .TaxRegistration = TaxRegistration
            .TransactionEconomicActivity = TransactionEconomicActivity
        End With
    End Sub

#End Region

#Region "Barra Botones"
    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub


#End Region

End Class