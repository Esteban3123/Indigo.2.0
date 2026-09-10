'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Carlos Ernesto Córdoba
' Created          : 19-01-2016
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Threading.Tasks
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class FrmMassiveConfirm
    Implements IMassiveConfirm

#Region "GLOBALS"

    ''' <summary>
    ''' Presentador de reconocimientos
    ''' </summary>
    Dim _presenter As PMassiveConfirm

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As String Implements IMassiveConfirm.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#End Region

#Region "Builder"

    Public Sub New()
        InitializeComponent()

        Me._presenter = New PMassiveConfirm(Me)
    End Sub

#End Region

#Region "METHODS"

    Private Sub CleanControls()
        INDSleForm.EditValue = Nothing
        INDGcDocuments.DataSource = Nothing
        ColCode.Caption = "Código"
        ColCode.FieldName = "DocumentNumber"
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        AdditionalControlPanel.Controls.Clear()
        Me.BarraBotones.StatusRecordVisible = False
    End Sub

    ''' <summary>
    ''' metodo para establecer los documentos de tesoreria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTreasuryDocuments()
        Using model As New MMassiveConfirm(Me.Tag)
            Select Case INDSleForm.EditValue
                Case 635 'Recibos de Caja
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(1)
                Case 636 'Comprobante de egreso
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(2)
                Case 637 'Notas
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(3)
                Case 638 'Consignaciones
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(4)
                Case 639 'Reembolso
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(5)
                Case 640 'Cruce de Cuentas
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(6)
                Case 642 'Dispersion de Fondos
                    INDGcDocuments.DataSource = model.ListTreasuryControlByDocumentType(7)
                Case Else
                    INDGcDocuments.DataSource = Nothing
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' establece los documentos sin confirmar de contabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAccountingDocumnets()
        Using model As New MMassiveConfirm(Me.Tag)
            INDGcDocuments.DataSource = model.ListJournalVourcherMassiveConfirm()
        End Using
    End Sub

    ''' <summary>
    ''' consulta documentos de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInventoryDocuments()
        Using model As New MMassiveConfirm(Me.Tag)
            Select Case INDSleForm.EditValue
                Case 316 'Orden de compra
                    INDGcDocuments.DataSource = Nothing
                Case 317 'Remision de Entrada
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(2)
                Case 1610 'Ajuste de inventario
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(3)
                Case 321 'Solicitudes
                    INDGcDocuments.DataSource = Nothing
                Case 322 'Dispensacion Farmaceutica
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(5)
                Case 323 'Remision de salida
                    INDGcDocuments.DataSource = Nothing
                Case 329 'Devolucion de Remisiones
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(7)
                Case 332 'Devolucion de orden de traslado
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(14)
                Case 1401 'Contrato
                    INDGcDocuments.DataSource = Nothing
                Case 1402 'Comprobante de entrada
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(8)
                Case 1403 'Devolución de Compra
                    INDGcDocuments.DataSource = Nothing
                Case 1514 'Préstamo de Mercancía
                    INDGcDocuments.DataSource = Nothing
                Case 1516 'Devolucion de Dispensacion Farmaceutica
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(11)
                Case 1518 'Devolucion de Prestamo
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(12)
                Case 1519 'Orden de Traslado
                    INDGcDocuments.DataSource = model.ListInventoryControlDocumentByDocumentType(13)
                Case 1561 'Factura de producto
                    INDGcDocuments.DataSource = Nothing
                Case Else
                    INDGcDocuments.DataSource = Nothing
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' consulta documentos de cuantas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPaymentDocumnents()
        Using model As New MMassiveConfirm(Me.Tag)
            Select Case INDSleForm.EditValue
                Case 730 'cuentas por pagar
                    INDGcDocuments.DataSource = model.ListPaymentsControlByDocumentType(1)
                Case 731 'Nota debito credito
                    INDGcDocuments.DataSource = model.ListPaymentsControlByDocumentType(2)
                Case 746 'Cruce de antipos vs cxp
                    INDGcDocuments.DataSource = model.ListPaymentsControlByDocumentType(3)
                Case 727 'Amortizacion mensual
                    INDGcDocuments.DataSource = Nothing
                Case 734 'Traslado de facturas
                    INDGcDocuments.DataSource = model.ListPaymentsControlByDocumentType(4)
                Case 1505 'Aceptacion traslado
                    INDGcDocuments.DataSource = Nothing
                Case Else
                    INDGcDocuments.DataSource = Nothing
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' consulta documentos de cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPortfolioDocuments()
        Using model As New MMassiveConfirm(Me.Tag)
            ColCode.FieldName = "DocumentNumber"
            Select Case INDSleForm.EditValue
                Case 509 'Radicacion de cuentas
                    ColCode.FieldName = "RadicatedConsecutive"
                    INDGcDocuments.DataSource = model.ListRadicateInvoiceCNotConfirm()
                Case 686 'Notas debito credito
                    INDGcDocuments.DataSource = model.ListPortfolioControlByDocumentType(1)
                Case 687 'Cruce de anticipos vs cxc
                    INDGcDocuments.DataSource = model.ListPortfolioControlByDocumentType(2)
                Case 1522 'Documento de cuenta por cobrar
                    INDGcDocuments.DataSource = model.ListPortfolioControlByDocumentType(3)
                Case Else
                    INDGcDocuments.DataSource = Nothing
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' consulta los documentos de glosas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGlosasDocuments()
        Using model As New MMassiveConfirm(Me.Tag)
            Select Case INDSleForm.EditValue
                Case 508 'recepcion de objeciones
                    ColCode.FieldName = "RadicatedConsecutive"
                    INDGcDocuments.DataSource = model.listGlosasObjectionCNotConfirm()
                Case 522 'conciliaciones
                    ColCode.FieldName = "ConciliationConsecutive"
                    INDGcDocuments.DataSource = model.ListGlosasConciliationCNotConfirm()
                Case 582 'traslado a cobro juridico
                    ColCode.FieldName = "JuridicalTransferConsecutive"
                    INDGcDocuments.DataSource = model.ListTransferJuridicalDebtCollectionCNotConfirm()
                Case Else
                    ColCode.FieldName = "DocumentNumber"
                    INDGcDocuments.DataSource = Nothing
            End Select
        End Using
    End Sub

    Private Sub ShowReport(listDocuments As List(Of String))
        Select Case INDSleModule.EditValue
            Case 42 '220 ' Adminstracion de Efectivo
                'Recibos de Caja
                If INDSleForm.EditValue = 635 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmCashReceipt
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'VoucherTransaction
                If INDSleForm.EditValue = 636 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmVoucherTransaction
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Notas
                If INDSleForm.EditValue = 637 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmNotes
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Consignaciones
                If INDSleForm.EditValue = 638 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmConsignment
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Reembolso
                If INDSleForm.EditValue = 639 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmReimbursements
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                ''Cruce de Cuentas
                'If INDSleForm.EditValue = 640 Then
                '    Dim reportdef As New Reporter.rptSubMassiveConfirmCrossing
                '    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                'End If

                ''Dispersion de Fondos
                ' If INDSleForm.EditValue = 642 Then
                '     Dim reportdef As New Reporter.rptSubMassiveConfirmDispersionFunds
                '     ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                ' End If
            Case 110 ' Glosas
                'Traslado a Cobro Juridico
                If INDSleForm.EditValue = 582 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmTransferJuridicalDebt
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'recepcion de objeciones
                If INDSleForm.EditValue = 508 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmObjectionDocu
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If
            Case 37 '130 ' Contabilidad General
                Dim reportdef As New Reporter.rptSubMassiveConfirmAccountVoucher
                ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments, BarraBotones.OperatingUnitValue)
            Case 21, 45 '190 ' Inventarios
                'Remision de Entrada
                If INDSleForm.EditValue = 317 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmRemissionEntrance
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Dispensación de Farmaceutica
                If INDSleForm.EditValue = 322 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmPharmaceuticalDispensing
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Devolucion de Remisiones
                If INDSleForm.EditValue = 329 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmRemissionDevolution
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Devolucion de Dispensacion Farmaceutica
                If INDSleForm.EditValue = 1516 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmPharmaceuticalDispensingDevolution
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Orden de Traslado
                If INDSleForm.EditValue = 1519 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmTransferOrder
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Devolucion de orden de traslado
                If INDSleForm.EditValue = 332 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmTransferOrderDevolution
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Comprobante de Entrada
                If INDSleForm.EditValue = 1402 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmEntranceVoucher
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Devolución de prestamo
                If INDSleForm.EditValue = 1518 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmLoanMerchandiseDevolution
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If
            Case 43 ' 150 'Cuentas por pagar
                'Cuentas por pagar
                If INDSleForm.EditValue = 730 Then
                    Dim reportdef As New Reporter.rptSubMassiveConfirmAccountPayable
                    ReportHelper.ExecuteReport(reportdef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Nota debito credito
                If INDSleForm.EditValue = 731 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmNotesDC
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Cruce de Anticipo vs CxP
                If INDSleForm.EditValue = 746 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmTransfer
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If
            Case 44 '160 'Cuentas por cobrar
                'Radicacion de cuentas
                If INDSleForm.EditValue = 509 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmAccountReceivable
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments, Me.BarraBotones.OperatingUnitValue)
                End If

                'Notas debito credito
                If INDSleForm.EditValue = 686 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmNotesDebitCredit
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Cruce de anticipos vs cxc
                If INDSleForm.EditValue = 687 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmTransferPortfolio
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If

                'Documento de cuenta por cobrar
                If INDSleForm.EditValue = 1522 Then
                    Dim reportDef As New Reporter.rptSubMassiveConfirmReceivableDocument
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listDocuments)
                End If
        End Select
    End Sub

#End Region

#Region "CRUD"

    Dim listDocuments As List(Of String)
    Dim result As List(Of Tuple(Of String, Integer))

    Private Async Sub Confirm()
        If INDGvDocuments.GetSelectedRows().Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar items para confirmar"
            Exit Sub
        End If

        If MessageIndigo.Show("Esta seguro que desea confirmar los documentos", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            AsyncLoader(True)
            listDocuments = New List(Of String)
            For Each item In INDGvDocuments.GetSelectedRows()
                Dim document = INDGvDocuments.GetRow(item)
                Select Case INDSleModule.EditValue
                    Case 110 'glosas
                        Select Case INDSleForm.EditValue
                            Case 508 'recepcion de objeciones
                                listDocuments.Add(document.RadicatedConsecutive)
                            Case 522 'conciliaciones
                                listDocuments.Add(document.ConciliationConsecutive)
                            Case 582 'traslado a cobro juridico
                                listDocuments.Add(document.JuridicalTransferConsecutive)
                        End Select
                    Case 37 '130 'contabilidad
                        listDocuments.Add(document.Id)
                    Case Else
                        If INDSleForm.EditValue = 509 Then 'radicacion de cuentas
                            listDocuments.Add(document.RadicatedConsecutive)
                        Else
                            listDocuments.Add(document.DocumentNumber)
                        End If
                End Select
            Next

            Await Me.ConfirmTask()

            If result.Count > 0 Then
                Using formulario As New FrmListErrors(result)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    transparent.ShowDialog(Me)
                End Using

                If result.Any(Function(d) d.Item2 = 1) Then
                    If MessageIndigo.Show("Desea visualizar el informe", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If

                    Me.ShowReport(listDocuments)
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No hubo resultados de la operación"
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
            CleanControls()
        End Try
    End Sub

    Dim progress As CtrProgress
    Dim totalItems As Integer
    Dim totalProcessedItems As Integer
    Const itemsSend As Integer = 50
    Dim listRows As New Concurrent.ConcurrentBag(Of String)()

    Private Function ConfirmTask() As Task
        totalProcessedItems = 0
        totalItems = listDocuments.Count
        result = New List(Of Tuple(Of String, Integer))

        progress = New CtrProgress
        progress.SetInfoFunction(AddressOf getInfo)
        progress.Dock = DockStyle.Fill
        AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))
        Me.BarraBotones.StatusRecordVisible = True
        progress.SafeInvoke(Sub(x)
                                x.SetTitle = "Registros Procesados"
                                x.PrintInfo()
                            End Sub)

        Return Task.Factory.StartNew(Sub()
                                         Using trasparent = New FrmTransparent(Nothing, False)
                                             trasparent.SafeInvoke(Sub(f) f.ShowDialog())
                                             Using model As New MMassiveConfirm(Me.Tag)
                                                 Dim indexSend As Integer = 0
                                                 While indexSend + 1 <= totalItems
                                                     listRows = New Concurrent.ConcurrentBag(Of String)()
                                                     If (indexSend + itemsSend) > totalItems Then
                                                         SetRow(indexSend, totalItems)
                                                     Else
                                                         SetRow(indexSend, indexSend + itemsSend)
                                                     End If

                                                     Dim resultRow As New ActionResult(Of List(Of Tuple(Of String, Integer)))
                                                     Select Case INDSleModule.EditValue
                                                         Case 42 '220 ' Adminstracion de Efectivo
                                                             resultRow = model.ConfirmDocumentsTreasury(INDSleForm.EditValue, listRows.ToList())
                                                         Case 110 ' Glosas
                                                             resultRow = model.ConfirmDocumentsGlosas(INDSleForm.EditValue, listRows.ToList(), Me.BarraBotones.OperatingUnitValue)
                                                         Case 37 '130 ' Contabilidad General
                                                             resultRow = model.ConfirmDocumentsAccounting(listRows.ToList())
                                                         Case 21, 45 '190 ' Inventarios
                                                             resultRow = model.ConfirmDocumentsInventory(INDSleForm.EditValue, listRows.ToList())
                                                         Case 43 '150 'Cuenrtas por pagar 
                                                             resultRow = model.ConfirmDocumentsPayments(INDSleForm.EditValue, listRows.ToList())
                                                         Case 44 '160 'Cuentas por cobrar
                                                             If INDSleForm.EditValue = 509 Then 'si es radicacion de cuentas utilizamos el servicio de confirmacion de glosas
                                                                 resultRow = model.ConfirmDocumentsGlosas(INDSleForm.EditValue, listRows.ToList(), Me.BarraBotones.OperatingUnitValue)
                                                             Else
                                                                 resultRow = model.ConfirmDocumentsPortfolio(INDSleForm.EditValue, listRows.ToList())
                                                             End If
                                                     End Select

                                                     result.AddRange(resultRow.ObjectEmbbeded)

                                                     indexSend += itemsSend
                                                     totalProcessedItems = If(indexSend > totalItems, totalItems, indexSend)
                                                     progress.SafeInvoke(Sub(x) x.PrintInfo())
                                                 End While
                                             End Using
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <remarks></remarks>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(listDocuments(x))
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

#End Region

#Region "HANDLERS"

    Private Sub FrmMassiveConfirm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDSleModule.Properties.DataSource = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules)
        INDSleModule.EditValue = Me.IdModuleSource
        Dim listFormsPermissionsConfirm As New List(Of VieForm)
        Using model As New MMassiveConfirm(Me.Tag)

            Dim listIdForms = model.ListPermissionFormsUser()
            If listIdForms IsNot Nothing Then
                For Each item In listIdForms
                    Dim form = indigo.ListFormPermission.Find(Function(x) x.Module.Id = Me.IdModuleSource And x.Type = 30 And x.Id = item)
                    If form IsNot Nothing AndAlso form.HandlesMassiveConfirm Then
                        listFormsPermissionsConfirm.Add(form)
                    End If
                Next
            End If
        End Using

        INDSleForm.Properties.DataSource = listFormsPermissionsConfirm
        If listFormsPermissionsConfirm.Count = 1 Then
            INDSleForm.EditValue = listFormsPermissionsConfirm(0).Id
        End If
        INDSleForm.Focus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
    End Sub

    Private Sub INDSleForm_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleForm.EditValueChanged
        SetDatasourceDocuments()
    End Sub

#End Region

#Region "BAR BUTTONS"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirm()
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

    Private Sub Frm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDSleForm.Enabled Then
            INDSleForm.Focus()
        End If
    End Sub

    Private Sub FrmMassiveConfirm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'SetDatasourceDocuments()
    End Sub

    Private Sub SetDatasourceDocuments()
        If INDSleForm.EditValue IsNot Nothing Then
            AsyncLoader(True)
            Select Case INDSleModule.EditValue
                Case 42 '220 ' Adminstracion de Efectivo
                    SetTreasuryDocuments()
                Case 110 'Glosas
                    SetGlosasDocuments()
                Case 37 '130 ' Contabilidad General
                    ColCode.Caption = "Consecutivo - Tipo de Comprobante"
                    SetAccountingDocumnets()
                Case 21, 45 '190 'Inventarios
                    SetInventoryDocuments()
                Case 43 '150 'Cuentas por pagar
                    SetPaymentDocumnents()
                Case 44 '160 'Cuentas por cobrar
                    SetPortfolioDocuments()
            End Select
            AsyncLoader(False)
        End If
    End Sub

End Class