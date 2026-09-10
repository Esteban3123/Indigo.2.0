'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 09-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Treasury.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports DevExpress.Xpo
Imports Presentation.Accounting
Imports Presentation.Common
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Formulario de Parametros
''' </summary>
Public Class FrmSettingsTreasury
    Implements ISettingsTreasury

#Region "Tuples"
    Dim _listGetThirdPartyCashRegister As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListGetThirdPartyCashRegister As List(Of Tuple(Of Byte, String))
        Get
            If _listGetThirdPartyCashRegister Is Nothing Then
                _listGetThirdPartyCashRegister = New List(Of Tuple(Of Byte, String))
                _listGetThirdPartyCashRegister.Add(New Tuple(Of Byte, String)(1, "Tercero Caja"))
                _listGetThirdPartyCashRegister.Add(New Tuple(Of Byte, String)(2, "Tercero Movimiento"))
            End If
            Return _listGetThirdPartyCashRegister
        End Get
    End Property

    Dim _listGetThirdPartyBank As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListGetThirdPartyBank As List(Of Tuple(Of Byte, String))
        Get
            If _listGetThirdPartyBank Is Nothing Then
                _listGetThirdPartyBank = New List(Of Tuple(Of Byte, String))
                _listGetThirdPartyBank.Add(New Tuple(Of Byte, String)(1, "Tercero Cuenta Bancaria"))
                _listGetThirdPartyBank.Add(New Tuple(Of Byte, String)(2, "Tercero Movimiento"))
            End If
            Return _listGetThirdPartyBank
        End Get
    End Property
#End Region

#Region "Properties and Variables"

#Region "Bands QueryPopUp"

    Private _bandQueryPopUpCashReceipt As Boolean
    Private _bandQueryPopUpVoucherTransaction As Boolean
    Private _bandQueryPopUpVoucherTransactionCrossing As Boolean
    Private _bandQueryPopUpBankAppropriation As Boolean
    Private _bandQueryPopUpTreasuryNote As Boolean
    Private _bandQueryPopUpMainAccountPayment As Boolean
    Private _bandQueryPopUpMainAccountExpense As Boolean
    Private _bandQueryPopUpConstitutionCash As Boolean

#End Region

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' sabado habil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [saturday]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Saturday As Boolean Implements ISettingsTreasury.Saturday
        Get
            Return CBool(INDrgSaturday.EditValue)
        End Get
        Set(value As Boolean)
            INDrgSaturday.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' domingo habil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [sunday]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Sunday As Boolean Implements ISettingsTreasury.Sunday
        Get
            Return CBool(INDrgSunday.EditValue)
        End Get
        Set(value As Boolean)
            INDrgSunday.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si permite modificar las fechas de los documentos
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [allow modify documents dates]; otherwise, <c>false</c>.
    ''' </value>
    Public Property AllowModifyDocumentsDates As Boolean Implements ISettingsTreasury.AllowModifyDocumentsDates
        Get
            Return INDrgAllowModifyDocumentsDates.EditValue
        End Get
        Set(value As Boolean)
            INDrgAllowModifyDocumentsDates.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si confirma presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [budget confirm]; otherwise, <c>false</c>.
    ''' </value>
    Public Property BudgetConfirm As Boolean Implements ISettingsTreasury.BudgetConfirm
        Get
            Return INDrgBudgetConfirm.EditValue
        End Get
        Set(value As Boolean)
            INDrgBudgetConfirm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si genera orden de pago automaticamente
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [build payment order automatic]; otherwise, <c>false</c>.
    ''' </value>
    Public Property BuildPaymentOrderAutomatic As Boolean Implements ISettingsTreasury.BuildPaymentOrderAutomatic
        Get
            Return INDrgBuildPaymentOrderAutomatic.EditValue
        End Get
        Set(value As Boolean)
            INDrgBuildPaymentOrderAutomatic.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si controla el consecutivo de las chequeras de cuentas bancarias
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [ckeck book control]; otherwise, <c>false</c>.
    ''' </value>
    Public Property CheckBookControl As Boolean Implements ISettingsTreasury.CheckBookControl
        Get
            Return INDrgCheckBookControl.EditValue
        End Get
        Set(value As Boolean)
            INDrgCheckBookControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Gravamen Movimientos Financieros (4x1000) -  Id Cuenta Contable Gastos
    ''' </summary>
    ''' <value>
    ''' The FMG main account expenses.
    ''' </value>
    Public Property FMGMainAccountExpenses As Integer Implements ISettingsTreasury.FMGMainAccountExpenses
    '    Get
    '        Return INDsleFMGMainAccountExpenses.EditValue
    '    End Get
    '    Set(value As Integer)
    '        INDsleFMGMainAccountExpenses.EditValue = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obtiene o establece el gravemen Movimientos financieros (4x1000) - Id Cuenta Contable Cuenta por pagar
    ''' </summary>
    ''' <value>
    ''' The FMG main account payment.
    ''' </value>
    Public Property FMGMainAccountPayment As Integer Implements ISettingsTreasury.FMGMainAccountPayment
    '    Get
    '        Return INDsleFMGMainAccountPayment.EditValue
    '    End Get
    '    Set(value As Integer)
    '        INDsleFMGMainAccountPayment.EditValue = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad operativa
    ''' </summary>
    ''' <value>
    ''' The identifier operating unit.
    ''' </value>
    Public Property IdOperatingUnit As Integer Implements ISettingsTreasury.IdOperatingUnit
        Get
            Return Me._idOperativeUnit
        End Get
        Set(value As Integer)
            _idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de consignaciones bancarias
    ''' </summary>
    ''' <value>
    ''' The journal voucher type bank appropriations.
    ''' </value>
    Public Property JournalVoucherTypeBankAppropriations As Integer Implements ISettingsTreasury.JournalVoucherTypeBankAppropriations
        Get
            Return INDsleJournalVoucherTypeBankAppropriations.EditValue
        End Get
        Set(value As Integer)
            INDsleJournalVoucherTypeBankAppropriations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The journal voucher type cash receipts.
    ''' </value>
    Public Property JournalVoucherTypeCashReceipts As Integer Implements ISettingsTreasury.JournalVoucherTypeCashReceipts
        Get
            Return INDsleJournalVoucherTypeCashReceipts.EditValue
        End Get
        Set(value As Integer)
            INDsleJournalVoucherTypeCashReceipts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de notas de tesoreria
    ''' </summary>
    ''' <value>
    ''' The journal voucher type treasury notes.
    ''' </value>
    Public Property JournalVoucherTypeTreasuryNotes As Integer Implements ISettingsTreasury.JournalVoucherTypeTreasuryNotes
        Get
            Return INDsleJournalVoucherTypeTreasuryNotes.EditValue
        End Get
        Set(value As Integer)
            INDsleJournalVoucherTypeTreasuryNotes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de comprobantes de egreso cruce
    ''' </summary>
    ''' <value>
    ''' The journal voucher type voucher transaction crossing.
    ''' </value>
    Public Property JournalVoucherTypeVoucherTransactionCrossing As Integer Implements ISettingsTreasury.JournalVoucherTypeVoucherTransactionCrossing
        Get
            Return INDsleJournalVoucherTypeVoucherTransactionCrossing.EditValue
        End Get
        Set(value As Integer)
            INDsleJournalVoucherTypeVoucherTransactionCrossing.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The journal voucher type voucher transaction.
    ''' </value>
    Public Property JournalVoucherTypeVoucherTransaction As Integer Implements ISettingsTreasury.JournalVoucherTypeVoucherTransaction
        Get
            Return INDsleJournalVoucherTypeVoucherTransaction.EditValue
        End Get
        Set(value As Integer)
            INDsleJournalVoucherTypeVoucherTransaction.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si parametriza presupuesto por concepto
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [setting budgetfor concept]; otherwise, <c>false</c>.
    ''' </value>
    Public Property SettingBudgetforConcept As Boolean Implements ISettingsTreasury.SettingBudgetforConcept
        Get
            Return INDrgSettingBudgetforConcept.EditValue
        End Get
        Set(value As Boolean)
            INDrgSettingBudgetforConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de consignaciones
    ''' </summary>
    ''' <value>
    ''' The datasource bank appropriations.
    ''' </value>
    Public Property DatasourceBankAppropriation As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceBankAppropriation
        Get
            Return CType(INDsleJournalVoucherTypeBankAppropriations.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleJournalVoucherTypeBankAppropriations.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The datasource cash receip.
    ''' </value>
    Public Property DatasourceCashReceipt As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceCashReceipt
        Get
            Return CType(INDsleJournalVoucherTypeCashReceipts.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleJournalVoucherTypeCashReceipts.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de notas de tesoreria
    ''' </summary>
    ''' <value>
    ''' The datasource treasury notes.
    ''' </value>
    Public Property DatasourceTreasuryNote As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceTreasuryNote
        Get
            Return CType(INDsleJournalVoucherTypeTreasuryNotes.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleJournalVoucherTypeTreasuryNotes.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The datasource voucher transaction.
    ''' </value>
    Public Property DatasourceVoucherTransaction As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceVoucherTransaction
        Get
            Return CType(INDsleJournalVoucherTypeVoucherTransaction.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleJournalVoucherTypeVoucherTransaction.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the datasource voucher transaction crossing.
    ''' </summary>
    ''' <value>
    ''' The datasource voucher transaction crossing.
    ''' </value>
    Public Property DatasourceVoucherTransactionCrossing As XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceVoucherTransactionCrossing
        Get
            Return CType(INDsleJournalVoucherTypeVoucherTransactionCrossing.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleJournalVoucherTypeVoucherTransactionCrossing.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables de gastos
    ''' </summary>
    ''' <value>
    ''' The datasource main account expense.
    ''' </value>
    Public Property DatasourceMainAccountExpense As XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceMainAccountExpense
    '    Get
    '        Return CType(INDsleFMGMainAccountExpenses.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDsleFMGMainAccountExpenses.Properties.DataSource = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables de pagos
    ''' </summary>
    ''' <value>
    ''' The datasource main account payment.
    ''' </value>
    Public Property DatasourceMainAccountPayment As XPInstantFeedbackSource Implements ISettingsTreasury.DatasourceMainAccountPayment
    '    Get
    '        Return CType(INDsleFMGMainAccountPayment.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDsleFMGMainAccountPayment.Properties.DataSource = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ISettingsTreasury.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordTreasury

    ''' <summary>
    ''' The presenter
    ''' </summary>
    Dim _presenter As PParameters

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _settingTreasury As SettingsTreasury

    ''' <summary>
    ''' Id del fondo de caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JournalVoucherTypeConstitutionCashId As Integer? Implements ISettingsTreasury.JournalVoucherTypeConstitutionCashId
        Get
            Return INDsleJournalVoucherTypeConstitutionCash.EditValue
        End Get
        Set(value As Integer?)
            INDsleJournalVoucherTypeConstitutionCash.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del fondo de caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JournalVoucherTypeConstitutionCashXpo As XPInstantFeedbackSource Implements ISettingsTreasury.JournalVoucherTypeConstitutionCashXpo
        Get
            Return INDsleJournalVoucherTypeConstitutionCash.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleJournalVoucherTypeConstitutionCash.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Me._settingTreasury IsNot Nothing AndAlso Me._settingTreasury.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MSettingsTreasury(Me.Tag.ToString())
                        Me._settingTreasury.MarkAsDeleted()
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteSettingTreasury(Me._settingTreasury)
                        AsyncLoader(False)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
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
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MSettingsTreasury(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveSettingTreasury(Me._settingTreasury)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _settingTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf _settingTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._settingTreasury = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Deshacer()
                    LoadControls()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Deshacer()
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    ' ''' <summary>
    ' ''' Esta propiedad establece el valor ControlAcciones
    ' ''' </summary>
    ' ''' <value>
    ' '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ' ''' </value>
    'Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingsTreasury.ActionsOnControls
    '    Set(value As Boolean)
    '        INDrgBudgetConfirm.Enabled = True
    '        INDrgSettingBudgetforConcept.Enabled = True
    '        INDrgCheckBookControl.Enabled = True
    '        INDrgBuildPaymentOrderAutomatic.Enabled = True
    '        INDrgAllowModifyDocumentsDates.Enabled = True
    '        INDsleJournalVoucherTypeCashReceipts.Enabled = True
    '        INDsleJournalVoucherTypeVoucherTransaction.Enabled = True
    '        INDsleJournalVoucherTypeBankAppropriations.Enabled = True
    '        INDsleJournalVoucherTypeTreasuryNotes.Enabled = True
    '        INDsleFMGMainAccountPayment.Enabled = True
    '        INDsleFMGMainAccountExpenses.Enabled = True
    '        INDrgBudgetConfirm.Focus()
    '    End Set
    'End Property

    ''' <summary>
    ''' Generates the document indexed.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._idOperativeUnit), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._idOperativeUnit & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._idOperativeUnit), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._idOperativeUnit)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._idOperativeUnit)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()
        'ActionsOnControls = False
        INDrgBudgetConfirm.Focus()
        INDrgBudgetConfirm.EditValue = Nothing
        INDrgSettingBudgetforConcept.EditValue = Nothing
        INDrgCheckBookControl.EditValue = Nothing
        INDrgBuildPaymentOrderAutomatic.EditValue = Nothing
        INDrgSaturday.EditValue = Nothing
        INDrgSunday.EditValue = Nothing

        INDrgAllowModifyDocumentsDates.EditValue = Nothing
        INDSeChangeDays.EditValue = 1
        INDLciChangeDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleJournalVoucherTypeCashReceipts.EditValue = Nothing
        INDsleJournalVoucherTypeVoucherTransaction.EditValue = Nothing
        INDsleJournalVoucherTypeBankAppropriations.EditValue = Nothing
        INDsleJournalVoucherTypeTreasuryNotes.EditValue = Nothing
        JournalVoucherTypeConstitutionCashId = Nothing
        'INDsleFMGMainAccountPayment.EditValue = Nothing
        'INDsleFMGMainAccountExpenses.EditValue = Nothing
        INDSleGetThirdPartyBank.EditValue = Nothing
        INDSleGetThirdPartyCash.EditValue = Nothing
        Me._settingTreasury = Nothing
        INDlycRoot.EndUpdate()

        INDsleJournalVoucherTypeCashReceipts.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeVoucherTransaction.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeVoucherTransactionCrossing.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeBankAppropriations.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeTreasuryNotes.Properties.NullText = String.Empty
        INDsleJournalVoucherTypeConstitutionCash.Properties.NullText = String.Empty
        'INDsleFMGMainAccountPayment.Properties.NullText = String.Empty
        'INDsleFMGMainAccountExpenses.Properties.NullText = String.Empty

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Using Model As New MSettingsTreasury(Me.Tag)
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetSettingsTreasuryByIdUnitOperative(IdOperatingUnit)
            AsyncLoader(False)
            If resultOperation.StateResult Then
                _settingTreasury = resultOperation.ObjectEmbbeded
                If Not _settingTreasury Is Nothing AndAlso _settingTreasury.Id > 0 Then
                    Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                        Dim result = Await ModelCommonTreasury.GetBlockRecordTreasury(Me.Tag, _settingTreasury.Id)

                        With Me._settingTreasury
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            '.IdOperatingUnit = Me._idOperativeUnit
                            BudgetConfirm = .BudgetConfirm
                            SettingBudgetforConcept = .SettingBudgetforConcept
                            CheckBookControl = .CheckBookControl
                            BuildPaymentOrderAutomatic = .BuildPaymentOrderAutomatic
                            AllowModifyDocumentsDates = .AllowModifyDocumentsDateBank
                            INDSeChangeDays.EditValue = .NumberDayBank
                            INDSleGetThirdPartyBank.EditValue = .GetThirdPartyBank
                            INDSleGetThirdPartyCash.EditValue = .GetThirdPartyCashRegister
                            JournalVoucherTypeCashReceipts = .JournalVoucherTypeCashReceipts
                            JournalVoucherTypeVoucherTransaction = .JournalVoucherTypeVoucherTransaction
                            JournalVoucherTypeBankAppropriations = .JournalVoucherTypeBankAppropriations
                            JournalVoucherTypeTreasuryNotes = .JournalVoucherTypeTreasuryNotes
                            JournalVoucherTypeVoucherTransactionCrossing = .JournalVoucherTypeVoucherTransactionCrossing
                            JournalVoucherTypeConstitutionCashId = .JournalVoucherTypeConstitutionCashId
                            Saturday = .SaturdaySkillful
                            Sunday = .SundaySkillful
                        End With

                        LoadNullText()

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._settingTreasury.IdOperatingUnit)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _settingTreasury.Id}
                            Dim operation = Await ModelCommonTreasury.SaveBlockRecordTreasury(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            _record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(_settingTreasury.Id)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    End Using
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                End If
                INDrgBudgetConfirm.Focus()
            End If
        End Using
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        If _settingTreasury Is Nothing Then
            _settingTreasury = New SettingsTreasury
        End If
        With Me._settingTreasury
            .IdOperatingUnit = Me._idOperativeUnit
            .BudgetConfirm = BudgetConfirm
            .SettingBudgetforConcept = SettingBudgetforConcept
            .CheckBookControl = CheckBookControl
            .BuildPaymentOrderAutomatic = BuildPaymentOrderAutomatic
            .AllowModifyDocumentsDateBank = AllowModifyDocumentsDates
            If INDLciChangeDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NumberDayBank = INDSeChangeDays.EditValue
            Else
                .NumberDayBank = 0
            End If
            .GetThirdPartyBank = INDSleGetThirdPartyBank.EditValue
            .GetThirdPartyCashRegister = INDSleGetThirdPartyCash.EditValue
            .JournalVoucherTypeCashReceipts = JournalVoucherTypeCashReceipts
            .JournalVoucherTypeVoucherTransaction = JournalVoucherTypeVoucherTransaction
            .JournalVoucherTypeVoucherTransactionCrossing = JournalVoucherTypeVoucherTransactionCrossing
            .JournalVoucherTypeBankAppropriations = JournalVoucherTypeBankAppropriations
            .JournalVoucherTypeTreasuryNotes = JournalVoucherTypeTreasuryNotes
            .JournalVoucherTypeConstitutionCashId = JournalVoucherTypeConstitutionCashId
            .SaturdaySkillful = Saturday
            .SundaySkillful = Sunday
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga los null Text de los controles
    ''' </summary>
    Private Sub LoadNullText()
        INDsleJournalVoucherTypeCashReceipts.Properties.NullText = Me._settingTreasury.FullNameJournaVoucherTypeCashReceipts
        INDsleJournalVoucherTypeVoucherTransaction.Properties.NullText = Me._settingTreasury.FullNameJournaVoucherTypeVoucherTransaction
        INDsleJournalVoucherTypeVoucherTransactionCrossing.Properties.NullText = Me._settingTreasury.FullNameJournaVoucherTypeVoucherTransactionCrossing
        INDsleJournalVoucherTypeBankAppropriations.Properties.NullText = Me._settingTreasury.FullNameJournaVoucherTypeBankAppropriations
        INDsleJournalVoucherTypeTreasuryNotes.Properties.NullText = Me._settingTreasury.FullNameJournaVoucherTypeTreasuryNotes
        INDsleJournalVoucherTypeConstitutionCash.Properties.NullText = Me._settingTreasury.FullNameJournalVoucherTypeConstitutionCash
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _record = Nothing
        _presenter = Nothing
        _settingTreasury = Nothing
    End Sub
    Private Sub INDrgAllowModifyDocumentsDates_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgAllowModifyDocumentsDates.EditValueChanged
        If INDrgAllowModifyDocumentsDates.EditValue IsNot Nothing Then
            If INDrgAllowModifyDocumentsDates.EditValue = True Then
                INDLciChangeDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSeChangeDays.EditValue = 1
            Else
                INDLciChangeDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSeChangeDays.EditValue = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmThirdPartyAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'

        _bandQueryPopUpCashReceipt = True
        _bandQueryPopUpVoucherTransaction = True
        _bandQueryPopUpVoucherTransactionCrossing = True
        _bandQueryPopUpConstitutionCash = True
        _bandQueryPopUpBankAppropriation = True
        _bandQueryPopUpTreasuryNote = True
        _bandQueryPopUpMainAccountPayment = True
        _bandQueryPopUpMainAccountExpense = True
        INDSleGetThirdPartyBank.Properties.DataSource = ListGetThirdPartyBank
        INDSleGetThirdPartyCash.Properties.DataSource = ListGetThirdPartyCashRegister

        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PParameters(Me)
        _presenter.LoadDefinitionLayout()
        Deshacer()
        LoadControls()
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleJournalVoucherTypeCashReceipts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeCashReceipts_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeCashReceipts.QueryPopUp
        If _bandQueryPopUpCashReceipt Then
            _presenter.InitializeJournarVoucherTypeCashReceipt()
            _bandQueryPopUpCashReceipt = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleJournalVoucherTypeVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeVoucherTransaction_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeVoucherTransaction.QueryPopUp
        If _bandQueryPopUpVoucherTransaction Then
            _presenter.InitializeJournarVoucherTypeVoucherTransaction()
            _bandQueryPopUpVoucherTransaction = False
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del fondo de caja menor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleJournalVoucherTypeConstitutionCash_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeConstitutionCash.QueryPopUp
        If _bandQueryPopUpConstitutionCash Then
            _presenter.InitializeConstitutionCash()
            _bandQueryPopUpConstitutionCash = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleJournalVoucherTypeVoucherTransactionCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeVoucherTransactionCrossing_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeVoucherTransactionCrossing.QueryPopUp
        If _bandQueryPopUpVoucherTransactionCrossing Then
            _presenter.InitializeJournarVoucherTypeVoucherTransactionCrossing()
            _bandQueryPopUpVoucherTransactionCrossing = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleJournalVoucherTypeBankAppropriations control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeBankAppropriations_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeBankAppropriations.QueryPopUp
        If _bandQueryPopUpBankAppropriation Then
            _presenter.InitializeJournarVoucherBankAppropriation()
            _bandQueryPopUpBankAppropriation = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleJournalVoucherTypeTreasuryNotes control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeTreasuryNotes_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherTypeTreasuryNotes.QueryPopUp
        If _bandQueryPopUpTreasuryNote Then
            _presenter.InitializeJournarVoucherTreasuryNote()
            _bandQueryPopUpTreasuryNote = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleFMGMainAccountPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountPayment_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If _bandQueryPopUpMainAccountPayment Then
            _presenter.InitializeMainAccountPayment()
            _bandQueryPopUpMainAccountPayment = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleFMGMainAccountExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountExpenses_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If _bandQueryPopUpMainAccountExpense Then
            _presenter.InitializeMainAccountExpenses()
            _bandQueryPopUpMainAccountExpense = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmThirdPartyAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmParameters_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleJournalVoucherTypeCashReceipts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeCashReceipts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherTypeCashReceipts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDocumentType()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeJournarVoucherTypeCashReceipt()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleJournalVoucherTypeVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeVoucherTransaction_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherTypeVoucherTransaction.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDocumentType()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeJournarVoucherTypeVoucherTransaction()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleJournalVoucherTypeVoucherTransactionCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeVoucherTransactionCrossing_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherTypeVoucherTransactionCrossing.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDocumentType()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeJournarVoucherTypeVoucherTransactionCrossing()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleJournalVoucherTypeBankAppropriations control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeBankAppropriations_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherTypeBankAppropriations.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDocumentType()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeJournarVoucherBankAppropriation()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleJournalVoucherTypeTreasuryNotes control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleJournalVoucherTypeTreasuryNotes_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherTypeTreasuryNotes.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDocumentType()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeJournarVoucherTreasuryNote()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleFMGMainAccountPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountPayment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeMainAccountPayment()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleFMGMainAccountExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountExpenses_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeMainAccountExpenses()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleJournalVoucherTypeConstitutionCash_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherTypeConstitutionCash.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            _presenter.InitializeConstitutionCash()
        End If
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
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            DeleteBlockedRecord()
            CleanControls()
            Me._idOperativeUnit = operatingUnit.Id
            LoadControls()
        End If
    End Sub

#End Region

End Class