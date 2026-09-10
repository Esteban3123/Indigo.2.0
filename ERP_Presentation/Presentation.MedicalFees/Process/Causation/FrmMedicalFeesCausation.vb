'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
Imports Presentation.Contract.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.MedicalFees.MVP

#End Region

Public Class FrmMedicalFeesCausation
    Implements IMedicalFeesCausation

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfo
        ctrTmp.PopupContainerControlTotalValue = INDpopupInfo
        ctrTmp.SetInfo(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    ''' <summary>
    ''' Retorna los valores a mostrar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getValues() As Tuple(Of String, String, String)
        Return New Tuple(Of String, String, String)(patientDescription, AdmissionNumber, entityDescription)
    End Function

#End Region

#Region "Properties"
    Private _invoiceDetailId As Integer?
    Private _asModal As Boolean = False
    ''' <summary>
    ''' Obtiene o establece la fecha de causación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CausationDate As Date Implements IMedicalFeesCausation.CausationDate
        Get
            Return INDdteCausationDate.EditValue
        End Get
        Set(value As Date)
            INDdteCausationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion de la nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MemoNotes As String Implements IMedicalFeesCausation.MemoNotes
        Get
            Return INDmemoNotes.Text
        End Get
        Set(value As String)
            INDmemoNotes.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceId As Integer? Implements IMedicalFeesCausation.InvoiceId
        Get
            Return INDsleInvoice.EditValue
        End Get
        Set(value As Integer?)
            INDsleInvoice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de las facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesCausation.InvoiceXpo
        Get
            Return INDsleInvoice.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleInvoice.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Layout del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMedicalFeesCausation.MyLayoutControl
        Get

        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IMedicalFeesCausation.MyTag
        Get

        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource del control de ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdmissionXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IMedicalFeesCausation.AdmissionXpo
        Get
            Return INDsleAdmission.Datasource
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleAdmission.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal
    ''' </summary>
    Private _admissionNumber As String
    Public ReadOnly Property AdmissionNumber As String Implements IMedicalFeesCausation.AdmissionNumber
        Get
            Return _admissionNumber
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al control de usuario que muestra la información
    ''' del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Public ctrTmp As CtrInfo

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PMedicalFeesCausation

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MedicalFees"

    ''' <summary>
    ''' Representa la entidad de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim medicalFeesCausation As MedicalFeesCausation

    ''' <summary>
    ''' Listado de homologaciones del showPopup
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCupsHomologation As List(Of CupsHomologation)

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMedicalFees

    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object

    ''' <summary>
    ''' representa el codigo del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Private _patientCode As String

    ''' <summary>
    ''' Listado de causaciones de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private ListMedicalFeesCausation As List(Of MedicalFeesCausation)

    ''' <summary>
    ''' True = Guardar , False = Modificar
    ''' </summary>
    ''' <remarks></remarks>
    Private banSaveAndModify As Boolean

    ''' <summary>
    ''' Listado de servicios no quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListViewNoSurgical As List(Of Domain.Entities.ViewListNoSurgical)

    ''' <summary>
    ''' Listado de servicios quirurgicos y paquetes
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListViewSurgicalAndPackage As XPCollection

    ''' <summary>
    ''' Representa el listado de las notas de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMedicalFeesNotes As List(Of MedicalFeesNote)

    ''' <summary>
    ''' Representa el listado de las notas eliminadas de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote)

    ''' <summary>
    ''' Variable para saber si se modifica o se agrega en la rejilla (True=Agrega, False=Modifica)
    ''' </summary>
    ''' <remarks></remarks>
    Dim BanAddOrModify As Boolean = True

    ''' <summary>
    ''' Representa a la entidad de notas de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _medicalFeesNotes As MedicalFeesNote

    ''' <summary>
    ''' Variable me dice si el form contiene el permiso de agregar honorario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContainsPermissionAddFees As Integer

    ''' <summary>
    ''' Variable me dice si el form contiene el permiso de causar manualmente en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContainsPermissionManualCausation As Integer

    ''' <summary>
    ''' Variable me dice si el form contiene el permiso de cambiar el médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContainsPermissionChangeHealthProfessional As Integer

    ''' <summary>
    ''' Variable me dice si el form contiene el permiso de eliminar causación
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContainsPermissionDelete As Integer

    ''' <summary>
    ''' No quirurgico
    ''' </summary>
    ''' <remarks></remarks>
    Dim itemNoSurgical As Domain.Entities.ViewListNoSurgical

    ''' <summary>
    ''' Quirurgico
    ''' </summary>
    ''' <remarks></remarks>
    Dim itemXpoSurgical As ViewListSurgicalAndPackageXpo

    ''' <summary>
    ''' Variable para saber en que rejilla realizaron clicDerecho/Cambiar Contrato
    ''' (True=Rejilla de Quirurgicos, False=Rejilla de NoQuirurgicos)
    ''' </summary>
    ''' <remarks></remarks>
    Dim SelectionSurgical As Boolean

    ''' <summary>
    ''' Nombre del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim patientDescription As String

    ''' <summary>
    ''' Entidad del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim entityDescription As String

    ''' <summary>
    ''' Numero de ingreso de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim AdmissionNumberInvoice As String

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls(False)
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        INDsleInvoice.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que deshace los cambios del form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeshacerTodo()
        CleanControls(True)
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        INDsleAdmission.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        'If cupsGroup IsNot Nothing AndAlso cupsGroup.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Using Model As New MCupsGroup(Me.Tag.ToString())
        '            AsyncLoader(True)
        '            cupsGroup.MarkAsDeleted()
        '            Dim result = Await Model.DeleteCupsGroup(cupsGroup)
        '            If result.StateResult = True Then
        '                Await Me.DeleteDocumentIndexed()
        '                AsyncLoader(False)
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                Me.Deshacer()
        '            Else
        '                AsyncLoader(False)
        '                If result.MessageResult(0) = "-999" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                ElseIf result.MessageResult(0) = "-000" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                End If
        '            End If
        '        End Using
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Using model As New MMedicalFeesCausation(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveMedicalFeesCausation(ListMedicalFeesCausation, ListMedicalFeesNotes, ListDeleteMedicalFeesNotes)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If banSaveAndModify Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                Else
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.ListMedicalFeesCausation = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        Await NewMedicalFeesCausation()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCupsGroup
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que actualiza el datasource del repositorio de médico
    ''' dependiendo si el serviceOrderDetail maneja el tipo de liquidación
    ''' especialidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Private Sub RefreshQueryPopup(sender As Object, view As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim controlSearch As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim viewSearch As GridView = controlSearch.Properties.View
        Dim itemCollection = view.GetFocusedRow()
        If itemCollection.LiquidationType = 2 Then
            viewSearch.ActiveFilterString = "CODESPEC1.CODESPECI='" & itemCollection.PerformsProfessionalSpecialty & "' Or CODESPEC2.CODESPECI='" & itemCollection.PerformsProfessionalSpecialty & "' Or CODESPEC3.CODESPECI='" & itemCollection.PerformsProfessionalSpecialty & "'"
        Else
            viewSearch.ActiveFilterString = String.Empty
        End If
        viewSearch.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el control de usuario que muestra
    ''' la informacion en la barra de botones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeUserControl()
        Dim invoiceXpo As InvoiceXpo = Nothing
        If viewSearchInvoice.GetFocusedRow Is Nothing Then
            invoiceXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.GetInvoiceById(INDsleInvoice.EditValue)
            viewSearchInvoice.Tag = invoiceXpo
        Else
            invoiceXpo = DirectCast(DirectCast(viewSearchInvoice.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, InvoiceXpo)
        End If

        INDtxtBillPopup.Text = invoiceXpo.InvoiceNumber
        INDtxtTotalEntityPopup.Text = invoiceXpo.ThirdPartySalesValue
        INDtxtTotalCopayment.Text = invoiceXpo.TotalPatientWithDiscount

        BarraBotones.StatusRecordVisible = True
        BarraBotones.ControlHideStatus = False
        ctrTmp.RefreshInfo()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup que se 
    ''' despliega en la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDtxtAdmissionNumberPopup.Text = String.Empty
        INDtxtPatientPopup.Text = String.Empty
        INDtxtHealthAdministratorPopup.Text = String.Empty
        INDtxtBillPopup.Text = String.Empty
        INDtxtTotalEntityPopup.EditValue = 0
        INDtxtTotalCopayment.EditValue = 0
    End Sub

    ''' <summary>
    ''' Valida que el id del contrato no venga vacio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateMedicalFeesContractId(MedicalFeesCausationId As Integer, StatusMedicalFeesCausation As Byte) As Boolean
        If MedicalFeesCausationId <> Nothing Then
            If StatusMedicalFeesCausation = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya se encuentra en una liquidación registrada."
                Return False
            ElseIf StatusMedicalFeesCausation = 3 Then
                Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya se encuentra en una liquidación confirmada."
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Calcula el valor causado para Surgical
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function CalculateValueSurgical(NewValue As Boolean, itemCollection As BillingRepository.ViewListSurgicalAndPackageXpo) As Task(Of ActionResult)
        Dim bAmountPayable As Decimal = itemCollection.AmountPayable
        Dim bMedicalFeesContractId As Integer = itemCollection.MedicalFeesContractId
        Dim bTotalAmountPayable As Decimal = itemCollection.TotalAmountPayable
        Dim bMedicalFeesContractCodeName As String = itemCollection.MedicalFeesContractCodeName
        Dim bTotalAmountPayableReal As Decimal = itemCollection.TotalAmountPayableReal
        Dim bPercentageCashed As Decimal = itemCollection.PercentageCashed

        If NewValue Then

            'Se valida que el item seleccionado no exista en una liquidacion como registrado o confirmado
            If ValidateMedicalFeesContractId(itemCollection.MedicalFeesCausationId, itemCollection.StatusMedicalFeesCausation) = False Then
                itemCollection.SelectOption = False
                Return New ActionResult With {.StateResult = False, .Message = String.Empty}
            End If

            Using model As New MMedicalFeesCausation("")
                Dim resultValue = Await model.CausedValue(itemCollection.MedicalFeesCausationId, itemCollection.RateManualType, itemCollection.CupsEntityId, itemCollection.CareGroupId, itemCollection.RateManualId, itemCollection.TotalSalesPrice, itemCollection.Presentation,
                                                          itemCollection.IPSServiceId, itemCollection.IPSServiceDescription, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyDescription, itemCollection.IPSServiceSODId,
                                                          itemCollection.ServiceOrderDetailId, itemCollection.ServiceOrderDetailSurgicalId, itemCollection.MedicalFeesContractId)
                If resultValue.StateResult = False Then
                    Return New ActionResult With {.StateResult = False, .Message = resultValue.Message}
                Else
                    If resultValue.ObjectEmbbeded IsNot Nothing AndAlso resultValue.ObjectEmbbeded.Count > 0 Then
                        OpenHomologations(resultValue.ObjectEmbbeded)
                        resultValue = Await model.CausedValue(itemCollection.MedicalFeesCausationId, itemCollection.RateManualType, itemCollection.CupsEntityId, itemCollection.CareGroupId, itemCollection.RateManualId, itemCollection.TotalSalesPrice, itemCollection.Presentation,
                                                          itemCollection.IPSServiceId, itemCollection.IPSServiceDescription, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyDescription, itemCollection.IPSServiceSODId,
                                                          itemCollection.ServiceOrderDetailId, itemCollection.ServiceOrderDetailSurgicalId, itemCollection.MedicalFeesContractId, ListCupsHomologation)
                        If resultValue.StateResult = False Then
                            Return New ActionResult With {.StateResult = False, .Message = resultValue.Message}
                        End If
                    End If

                    itemCollection.AmountPayable = resultValue.MessageResult(0)
                    itemCollection.MedicalFeesContractId = resultValue.Message
                    Using modelContract As New MMedicalFeesContract("")
                        Dim medicalFeesContract = Await modelContract.GetMedicalFeesContractById(itemCollection.MedicalFeesContractId)
                        If medicalFeesContract.ObjectEmbbeded.Status <> 1 Then
                            Return New ActionResult With {.StateResult = False, .Message = "No se puede causar el Item seleccionado porque el contrato " & medicalFeesContract.ObjectEmbbeded.Code & " esta terminado o suspendido"}
                        End If
                    End Using
                    itemCollection.TotalAmountPayable = itemCollection.AmountPayable * itemCollection.InvoicedQuantity
                    itemCollection.MedicalFeesContractCodeName = resultValue.MessageResult(1)
                    itemCollection.TotalAmountPayableReal = itemCollection.TotalAmountPayable
                    itemCollection.PercentageCashed = 100
                End If
            End Using
        Else
            If itemCollection.MedicalFeesCausationId <> Nothing Then
                itemCollection.AmountPayable = bAmountPayable
                itemCollection.MedicalFeesContractId = bMedicalFeesContractId
                itemCollection.TotalAmountPayable = bTotalAmountPayable
                itemCollection.MedicalFeesContractCodeName = bMedicalFeesContractCodeName
                itemCollection.TotalAmountPayableReal = bTotalAmountPayableReal
                itemCollection.PercentageCashed = bPercentageCashed
            Else
                itemCollection.AmountPayable = 0
                itemCollection.MedicalFeesContractId = Nothing
                itemCollection.TotalAmountPayable = 0
                itemCollection.MedicalFeesContractCodeName = String.Empty
                itemCollection.TotalAmountPayableReal = 0
                itemCollection.PercentageCashed = 0
            End If
        End If
        itemCollection.SelectOption = NewValue
        Dim rowHandle = ViewSurgicalAndPackage.FocusedRowHandle
        INDgcMedicalFeesCausationSurgical.RefreshDataSource()
        ViewSurgicalAndPackage.ExpandMasterRow(rowHandle)
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Calcula el valor causado para NoSurgical
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function CalculateValueNoSurgical(NewValue As Boolean, itemCollection As Domain.Entities.ViewListNoSurgical) As Task(Of ActionResult)
        Dim bAmountPayable As Decimal = itemCollection.AmountPayable
        Dim bMedicalFeesContractId As Integer = itemCollection.MedicalFeesContractId
        Dim bTotalAmountPayable As Decimal = itemCollection.TotalAmountPayable
        Dim bMedicalFeesContractCodeName As String = itemCollection.MedicalFeesContractCodeName
        Dim bTotalAmountPayableReal As Decimal = itemCollection.TotalAmountPayableReal
        Dim bPercentageCashed As Decimal = itemCollection.PercentageCashed

        If NewValue Then

            'Se valida que el item seleccionado no exista en una liquidacion como registrado o confirmado
            If ValidateMedicalFeesContractId(itemCollection.MedicalFeesCausationId, itemCollection.StatusMedicalFeesCausation) = False Then
                itemCollection.SelectOption = False
                Return New ActionResult With {.StateResult = False, .Message = String.Empty}
            End If

            Using model As New MMedicalFeesCausation("")
                Dim resultValue = Await model.CausedValue(itemCollection.MedicalFeesCausationId, itemCollection.RateManualType, itemCollection.CupsEntityId, itemCollection.CareGroupId, itemCollection.RateManualId, itemCollection.TotalSalesPrice, itemCollection.Presentation,
                                                          itemCollection.IPSServiceId, itemCollection.IPSServiceDescription, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyDescription, Nothing,
                                                          itemCollection.ServiceOrderDetailId, 0, itemCollection.MedicalFeesContractId)
                If resultValue.StateResult = False Then
                    Return New ActionResult With {.StateResult = False, .Message = resultValue.Message}
                Else
                    If resultValue.ObjectEmbbeded IsNot Nothing AndAlso resultValue.ObjectEmbbeded.Count > 0 Then
                        OpenHomologations(resultValue.ObjectEmbbeded)
                        resultValue = Await model.CausedValue(itemCollection.MedicalFeesCausationId, itemCollection.RateManualType, itemCollection.CupsEntityId, itemCollection.CareGroupId, itemCollection.RateManualId, itemCollection.TotalSalesPrice, itemCollection.Presentation,
                                                          itemCollection.IPSServiceId, itemCollection.IPSServiceDescription, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyDescription, Nothing,
                                                          itemCollection.ServiceOrderDetailId, 0, itemCollection.MedicalFeesContractId, ListCupsHomologation)
                        If resultValue.StateResult = False Then
                            Return New ActionResult With {.StateResult = False, .Message = resultValue.Message}
                        End If
                    End If

                    itemCollection.AmountPayable = resultValue.MessageResult(0)
                    itemCollection.MedicalFeesContractId = resultValue.Message
                    Using modelContract As New MMedicalFeesContract("")
                        Dim medicalFeesContract = Await modelContract.GetMedicalFeesContractById(itemCollection.MedicalFeesContractId)
                        If medicalFeesContract.ObjectEmbbeded.Status <> 1 Then
                            Return New ActionResult With {.StateResult = False, .Message = "No se puede causar el Item seleccionado porque el contrato " & medicalFeesContract.ObjectEmbbeded.Code & " esta terminado o suspendido"}
                        End If
                    End Using
                    itemCollection.TotalAmountPayable = itemCollection.AmountPayable * itemCollection.InvoicedQuantity
                    itemCollection.MedicalFeesContractCodeName = resultValue.MessageResult(1)
                    itemCollection.TotalAmountPayableReal = itemCollection.TotalAmountPayable
                    itemCollection.PercentageCashed = 100
                End If
            End Using
        Else
            If itemCollection.MedicalFeesCausationId <> 0 Then
                itemCollection.AmountPayable = bAmountPayable
                itemCollection.MedicalFeesContractId = bMedicalFeesContractId
                itemCollection.TotalAmountPayable = bTotalAmountPayable
                itemCollection.MedicalFeesContractCodeName = bMedicalFeesContractCodeName
                itemCollection.TotalAmountPayableReal = bTotalAmountPayableReal
                itemCollection.PercentageCashed = bPercentageCashed
            Else
                itemCollection.AmountPayable = 0
                itemCollection.MedicalFeesContractId = Nothing
                itemCollection.TotalAmountPayable = 0
                itemCollection.MedicalFeesContractCodeName = String.Empty
                itemCollection.TotalAmountPayableReal = 0
                itemCollection.PercentageCashed = 0
            End If
        End If
        itemCollection.SelectOption = NewValue
        Dim rowHandle = ViewNoSurgical.FocusedRowHandle
        INDgcMedicalFeesCausation.RefreshDataSource()
        ViewNoSurgical.ExpandMasterRow(rowHandle)
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Metodo que abre el showPopup para cambiar el contrato para causar el valor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenSelectContract(healthProfessionalCode As String, medicalFeesContractId As Integer, thirdPartyDescription As String)
        Dim model As New MMedicalFeesCausation(Me.Tag)
        Dim ListHealthProfessionalContractXpo As XPCollection = model.ListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
        If ListHealthProfessionalContractXpo IsNot Nothing AndAlso ListHealthProfessionalContractXpo.Count > 0 Then
            Using Formulario As New FrmSelectContract(healthProfessionalCode, medicalFeesContractId, ListHealthProfessionalContractXpo)
                AddHandler Formulario.SelectOptionEvent, AddressOf SelectOptionEvent
                Formulario.ToolBar.Visible = False
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim size As System.Drawing.Size
                size.Width = 675
                size.Height = 400
                Formulario.Size = size
                Dim transparent As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog()
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("DontListContract", NAME_MODULE), thirdPartyDescription)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se dispara al presionar click del form SelectContract
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub SelectOptionEvent(sender As Object, e As SelectContractEventArgs)
        If SelectionSurgical Then 'Rejilla de Quirurgicos
            itemXpoSurgical.MedicalFeesContractId = e.MedicalFeesContractId
            Dim result As ActionResult = Await CalculateValueSurgical(True, itemXpoSurgical)
            If result.StateResult = False Then
                If result.Message IsNot String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                itemXpoSurgical.SelectOption = False
                INDgcMedicalFeesCausationSurgical.RefreshDataSource()
                Exit Sub
            End If
        Else 'Rejilla de NoQuirurgicos
            itemNoSurgical.MedicalFeesContractId = e.MedicalFeesContractId
            Dim result As ActionResult = Await CalculateValueNoSurgical(True, itemNoSurgical)
            If result.StateResult = False Then
                If result.Message IsNot String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                itemNoSurgical.SelectOption = False
                INDgcMedicalFeesCausation.RefreshDataSource()
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que abre el showPopup de homologaciones
    ''' cuando tiene mas de uno
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenHomologations(ListCupsHomologation As List(Of CupsHomologation))
        Using Formulario As New PopupHomologation
            Formulario.OnlySelectedOne = True
            AddHandler Formulario.SetHomologation, AddressOf ReturnSetHomologation
            Formulario.StartPosition = FormStartPosition.CenterParent
            Formulario.ListHomologation = ListCupsHomologation
            Dim transParent As New FrmTransparent(Formulario, False)
            transParent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo que recibe la homologacion seleccionada en el popup cuando un servicio tiene mas de una
    ''' </summary>
    ''' <param name="Senders"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnSetHomologation(Senders As Object, e As SetHomologationEventArgs)
        ListCupsHomologation = Nothing
        ListCupsHomologation = e.ListHomologations
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el search de los profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeRepository()
        Using model As New MMedicalFeesCausation(Me.Tag)
            INDrepSleHealthProfessionalGridNoSurgical.DataSource = model.ListHealthCareProfessionalXpInstantFeedBackSource()
            INDrepSleHealthProfessionalSurgical.DataSource = model.ListHealthCareProfessionalXpInstantFeedBackSource()
        End Using
    End Sub

    ''' <summary>
    ''' Modifica las columnas de las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModifiedColumns()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewNotes.Columns
            If col.Name = "colActions" Then
                col.Width = 20
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewSurgicalAndPackage.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ViewNoSurgical.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Consulta las notas de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ConsultMedicalFeesNotes() As Task
        Using model As New MMedicalFeesNote(Tag)
            Dim result As ActionResult(Of List(Of MedicalFeesNote)) = Await model.GetMedicalFeesNotesByAdmissionNumberAndInvoiceIdAsync(_admissionNumber, InvoiceId)
            If result.StateResult = True Then
                ListMedicalFeesNotes = result.ObjectEmbbeded
                INDgcNotes.DataSource = Nothing
                INDgcNotes.DataSource = ListMedicalFeesNotes
            End If
        End Using
    End Function

    ''' <summary>
    ''' Agrega una nota a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddNote()
        If InvoiceId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una factura."
            INDmemoNotes.Focus()
            Exit Sub
        End If
        If MemoNotes Is String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = "Escriba una nota para agregar."
            INDmemoNotes.Focus()
            Exit Sub
        End If

        If BanAddOrModify Then
            If ListMedicalFeesNotes Is Nothing Then
                ListMedicalFeesNotes = New List(Of MedicalFeesNote)
            End If
            _medicalFeesNotes = New MedicalFeesNote
            With _medicalFeesNotes
                .AdmissionNumber = _admissionNumber
                .InvoiceId = InvoiceId
                .Note = MemoNotes
            End With
            ListMedicalFeesNotes.Add(_medicalFeesNotes)
            Mensaje(EeventViewerImages.Informacion) = "Nota agregada correctamente."
        Else
            With _medicalFeesNotes
                .AdmissionNumber = _admissionNumber
                .InvoiceId = InvoiceId
                .Note = MemoNotes
            End With
            Mensaje(EeventViewerImages.Informacion) = "Nota modificada correctamente."
        End If

        INDgcNotes.DataSource = Nothing
        INDgcNotes.DataSource = ListMedicalFeesNotes
        BanAddOrModify = True
        MemoNotes = String.Empty
        INDmemoNotes.Focus()
    End Sub

    ''' <summary>
    ''' Edita la nota de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditNote()
        _medicalFeesNotes = CType(viewNotes.GetFocusedRow, MedicalFeesNote)
        BanAddOrModify = False
        MemoNotes = _medicalFeesNotes.Note
        INDpceNotes.ShowPopup()
        INDmemoNotes.Focus()
    End Sub

    ''' <summary>
    ''' Elimina la nota
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteNote()
        _medicalFeesNotes = CType(viewNotes.GetFocusedRow, MedicalFeesNote)
        ListMedicalFeesNotes.Remove(_medicalFeesNotes)

        If _medicalFeesNotes.Id > 0 Then
            If ListDeleteMedicalFeesNotes Is Nothing Then
                ListDeleteMedicalFeesNotes = New List(Of MedicalFeesNote)
            End If
            _medicalFeesNotes.MarkAsDeleted()
            ListDeleteMedicalFeesNotes.Add(_medicalFeesNotes)
        End If

        INDgcNotes.DataSource = Nothing
        INDgcNotes.DataSource = ListMedicalFeesNotes
    End Sub

    ''' <summary>
    ''' Establece la informacion al popup
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Sub SetAdmission(record As Object)
        admission = record
        With admission
            Me.INDsleAdmission.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim(), admission.PatientName.ToString().Trim()))
            patientDescription = admission.PatientCode.ToString().Trim() + " - " + admission.PatientName.ToString().Trim()
            INDtxtPatientPopup.Text = patientDescription
            _admissionNumber = admission.AdmissionCode.ToString().Trim()
            INDtxtAdmissionNumberPopup.Text = _admissionNumber
            INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
            If .AdmissionDate IsNot Nothing Then
                INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()
            End If
            If .AdmissionType IsNot Nothing Then
                INDTxtAdmissionType.Text = ResourceManager.GetString(String.Concat("AdmissionType", .AdmissionType.ToString().Trim()))
            End If
            If .AuthorizationNumber IsNot Nothing Then
                INDTxtAuthorizationNumber.Text = .AuthorizationNumber.ToString().Trim()
            End If
            If .AdmissionType.ToString().Trim() <> "Ambulatorio" Then
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                If .BedStay IsNot Nothing Then
                    INDTxtStay.Text = .BedStay.ToString().Trim()
                End If
            Else
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            If admission.CareGroupId > 0 Then
                Using model As New MCareGroup(MyTag)
                    Dim careGroup = model.GetCareGroupByIdSimple(admission.CareGroupId).ObjectEmbbeded
                    If careGroup IsNot Nothing Then
                        INDTxtBenefitsPlan.Text = careGroup.Code + " - " + careGroup.Name
                    End If
                End Using
            End If
            If admission.HealthAdministratorId > 0 Then
                Using model As New MHealthAdministrator(MyTag)
                    Dim healthAdministrator = model.GetHealthAdministratorByIdSimple(admission.HealthAdministratorId).ObjectEmbbeded
                    INDTxtEntity.Text = healthAdministrator.Code + " - " + healthAdministrator.Name
                    INDtxtHealthAdministratorPopup.Text = healthAdministrator.Code + " - " + healthAdministrator.Name
                    entityDescription = healthAdministrator.Code + " - " + healthAdministrator.Name
                End Using
            End If
            If .LiquidationType IsNot Nothing Then
                INDTxtLiquidationType.Text = ResourceManager.GetString(String.Concat("LiquidationType", .LiquidationType.ToString().Trim()))
            End If
            If .PatientCode IsNot Nothing And .PatientName IsNot Nothing Then
                INDTxtPatient.Text = .PatientCode.ToString().Trim() + " - " + .PatientName.ToString().Trim()
            End If
            If .PlaceEntry IsNot Nothing Then
                INDTxtAdmissionPlace.Text = ResourceManager.GetString(String.Concat("PlaceEntry", .PlaceEntry.ToString().Trim()))
            End If
            If .ResponsibleName IsNot Nothing Then
                INDTxtResponsibleName.Text = .ResponsibleName.ToString().Trim()
            End If
            If .ResponsiblePhone IsNot Nothing Then
                INDTxtResponsiblePhone.Text = .ResponsiblePhone.ToString().Trim()
            End If
            _patientCode = .PatientCode.ToString().Trim()

            'Se establece la fecha minima y maxima de la fecha de causación
            INDdteCausationDate.Properties.MinValue = .AdmissionDate
            INDdteCausationDate.Properties.MaxValue = GetDateServer()
        End With
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMedicalFeesCausation.ActionsOnControls
        Set(value As Boolean)
            INDlyMedicalFeesCausation.BeginUpdate()
            INDsleAdmission.IsReadOnly = value
            INDsleInvoice.Enabled = value
            INDdteCausationDate.Enabled = value
            INDgcMedicalFeesCausation.Enabled = value
            INDgcMedicalFeesCausationSurgical.Enabled = value
            INDmemoNotes.Enabled = value
            INDbtnAddNote.Enabled = value
            INDgcNotes.Enabled = value
            INDpceNotes.Enabled = value
            INDlyMedicalFeesCausation.EndUpdate()
            If value Then
                INDsleInvoice.Focus()
            Else
                INDsleAdmission.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'If Me.cupsGroup IsNot Nothing AndAlso Me.cupsGroup.Id > 0 Then
        '    If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
        '        DeleteBlockedRecord()
        '        Me.INDbtnCode.Text = Me.IdEntity.Trim()
        '        Me.LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Me.INDbtnCode.Text = Me.IdEntity.Trim()
        '    Me.LoadControls()
        '    If FormSearchObjects IsNot Nothing Then
        '        FormSearchObjects.Close()
        '    End If
        'End If
        'Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        'DeleteBlockedRecord()
        'INDbtnCode.Text = ReturnValue
        'If INDbtnCode.Text <> String.Empty Then
        '    LoadControls()
        '    If INDbtnCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    End If
        '    INDbtnCode.Enabled = False
        'End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsGroup.Code, Me.cupsGroup.Name), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.cupsGroup.Code & "#$", .IdForm = CStr(Me.Tag), _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsGroup.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsGroup.Code, Me.cupsGroup.Name)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsGroup.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls(OptionAllClean As Boolean)
        INDlyMedicalFeesCausation.BeginUpdate()
        If OptionAllClean Then
            ActionsOnControls = False
            INDsleAdmission.SetNullText(String.Empty)
            CleanControlsAdminssion()
            BarraBotones.CleanAuditBasic()
            admission = Nothing
            _patientCode = String.Empty
            patientDescription = String.Empty
            entityDescription = String.Empty
            Me._doc = Nothing
            Me.BarraBotones.EnableBarItems()
            Me.BarraBotones.DisableBarDocument()
            CleanControlsPopup()
        End If
        BarraBotones.StatusRecordVisible = False
        InvoiceId = Nothing
        INDsleInvoice.Properties.NullText = String.Empty
        INDgcMedicalFeesCausation.DataSource = Nothing
        INDgcMedicalFeesCausationSurgical.DataSource = Nothing
        ListMedicalFeesCausation = Nothing
        ListViewNoSurgical = Nothing
        ListViewSurgicalAndPackage = Nothing
        INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygDetailsSurgical.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ListMedicalFeesNotes = Nothing
        ListDeleteMedicalFeesNotes = Nothing
        _medicalFeesNotes = Nothing
        MemoNotes = String.Empty
        INDgcNotes.DataSource = Nothing
        INDlyMedicalFeesCausation.EndUpdate()
        itemNoSurgical = Nothing
        itemXpoSurgical = Nothing
        Await DeleteBlockedRecord()

        Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleInvoice.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
        If info IsNot Nothing Then
            INDsleInvoice.Properties.Buttons.RemoveAt(info.Index)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del control de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdminssion()
        INDTxtAdmissionCode.Text = String.Empty
        INDTxtStay.Text = String.Empty
        INDTxtPatient.Text = String.Empty
        INDTxtAdmissionDate.Text = String.Empty
        INDTxtAdmissionType.Text = String.Empty
        INDTxtAdmissionPlace.Text = String.Empty
        INDTxtLiquidationType.Text = String.Empty
        INDTxtEntity.Text = String.Empty
        INDTxtBenefitsPlan.Text = String.Empty
        INDTxtAuthorizationNumber.Text = String.Empty
        INDTxtResponsibleName.Text = String.Empty
        INDTxtResponsiblePhone.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        If ListMedicalFeesCausation Is Nothing Then
            ListMedicalFeesCausation = New List(Of MedicalFeesCausation)
        End If

        'Asigno los valores para los datos que esten en la rejilla de No Quirurgicos
        If ListViewNoSurgical IsNot Nothing AndAlso ListViewNoSurgical.Count > 0 Then
            Dim ListPivotNoSurgical = (From e In ListViewNoSurgical Where e.SelectOption = True Select e).ToList()
            If ListPivotNoSurgical IsNot Nothing AndAlso ListPivotNoSurgical.Count > 0 Then
                For Each itemXpo In ListPivotNoSurgical
                    Dim medicalFeesCausation As New MedicalFeesCausation
                    With medicalFeesCausation
                        .Id = itemXpo.MedicalFeesCausationId
                        .AdmissionNumber = _admissionNumber
                        .PatientCode = _patientCode
                        .HealthProfessionalCode = itemXpo.PerformsHealthProfessionalCode.ToString.Trim
                        .ThirdPartyId = itemXpo.ThirdPartyId
                        .MedicalFeesContractId = itemXpo.MedicalFeesContractId
                        .ServiceOrderId = itemXpo.ServiceOrderId
                        .ServiceOrderDetailId = itemXpo.ServiceOrderDetailId
                        .ServiceOrderDetailSurgicalId = Nothing
                        .AmountPayable = itemXpo.AmountPayable
                        .MedicalFeesContractValue = itemXpo.AmountPayable
                        .InvoiceQuantity = itemXpo.InvoicedQuantity
                        .TotalAmountPayable = itemXpo.TotalAmountPayable
                        .PercentageCashed = itemXpo.PercentageCashed
                        .MedicalFeePaid = False
                        .InvoiceDetailId = itemXpo.InvoiceDetailId
                        If itemXpo.ServiceOrderDetailSurgicalId > 0 Then
                            .ServiceOrderDetailSurgicalId = itemXpo.ServiceOrderDetailSurgicalId
                        Else
                            .ServiceOrderDetailSurgicalId = Nothing
                        End If
                        .CausationDate = CausationDate
                    End With
                    If medicalFeesCausation.Id > 0 Then
                        medicalFeesCausation.MarkAsModified()
                    End If
                    ListMedicalFeesCausation.Add(medicalFeesCausation)
                Next
            End If
        End If

        'Asigno los valores para los datos que esten en la rejilla de Quirurgicos y Paquetes
        If ListViewSurgicalAndPackage IsNot Nothing AndAlso ListViewSurgicalAndPackage.Count > 0 Then
            Dim ListPivotSurgicalAndPackage = (From e In ListViewSurgicalAndPackage Where e.SelectOption = True Select e).ToList()
            If ListPivotSurgicalAndPackage IsNot Nothing AndAlso ListPivotSurgicalAndPackage.Count > 0 Then
                For Each itemXpo In ListPivotSurgicalAndPackage
                    Dim medicalFeesCausation As New MedicalFeesCausation
                    With medicalFeesCausation
                        .Id = itemXpo.MedicalFeesCausationId
                        .AdmissionNumber = _admissionNumber
                        .PatientCode = _patientCode
                        .HealthProfessionalCode = itemXpo.PerformsHealthProfessionalCode.ToString.Trim
                        .ThirdPartyId = itemXpo.ThirdPartyId
                        .MedicalFeesContractId = itemXpo.MedicalFeesContractId
                        .ServiceOrderId = itemXpo.ServiceOrderId
                        .ServiceOrderDetailId = itemXpo.ServiceOrderDetailId
                        If itemXpo.ServiceOrderDetailSurgicalId = 0 Then
                            .ServiceOrderDetailSurgicalId = Nothing
                        Else
                            .ServiceOrderDetailSurgicalId = itemXpo.ServiceOrderDetailSurgicalId
                        End If
                        .AmountPayable = itemXpo.AmountPayable
                        .MedicalFeesContractValue = itemXpo.AmountPayable
                        .InvoiceQuantity = itemXpo.InvoicedQuantity
                        .TotalAmountPayable = itemXpo.TotalAmountPayable
                        .PercentageCashed = itemXpo.PercentageCashed
                        .MedicalFeePaid = False
                        .InvoiceDetailId = itemXpo.InvoiceDetailId
                        .CausationDate = CausationDate
                    End With
                    If medicalFeesCausation.Id > 0 Then
                        medicalFeesCausation.MarkAsModified()
                    End If
                    ListMedicalFeesCausation.Add(medicalFeesCausation)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New Presentation.MedicalFees.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'Using Model As New MCupsGroup(CStr(Me.Tag))
        '    Dim resultOperation = Await RunAsyncOperation(Model.GetCupsGroup(INDbtnCode.Text.Trim))
        '    cupsGroup = resultOperation.ObjectEmbbeded
        '    If Not cupsGroup Is Nothing Then
        '        If cupsGroup.Id > 0 Then
        '            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(cupsGroup.Id))
        '                With cupsGroup
        '                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                    Code = .Code
        '                    NameCG = .Name
        '                    Description = .Description
        '                    Status = .Status
        '                End With
        '                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.cupsGroup.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = cupsGroup.Id}
        '                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                    record = operation.ObjectEmbbeded
        '                Else
        '                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning)
        '                End If
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.SetDocuments(cupsGroup.Id)
        '                ActionsOnControls = True
        '            End Using
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.Code = String.Empty
        '        End If
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        Me.Code = String.Empty
        '    End If
        'End Using
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMedicalFeesCausation() As Task
        'cupsGroup = New CupsGroup()
        'If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '    Me._idCurrentSequense = Me._sequense.ContractSequenceDetail(0).Id
        'ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '    If Me.Sequense.ContractSequenceDetail.Any(Function(S) S.OperatingUnitId = Me.BarraBotones.OperatingUnitValue) Then
        '        Me._idCurrentSequense = Me._sequense.ContractSequenceDetail.Where(Function(s) s.OperatingUnitId = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '        Exit Function
        '    End If
        'End If
        'If Not Me._sequense.Sequential Then
        '    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '        If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        Else
        '            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '            End Using
        '            If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '            End If
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'Else
        '    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'End If
    End Function

    ''' <summary>
    ''' Metodo que carga la rejilla cuando los detalles de las ordenes no son quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadGridNoSurgical() As Task
        Using Model As New MMedicalFeesCausation(Me.Tag)
            ListViewNoSurgical = Await Model.GetViewListNoSurgical(InvoiceId, _invoiceDetailId)
            If ListViewNoSurgical IsNot Nothing AndAlso ListViewNoSurgical.Count > 0 Then
                If ListViewNoSurgical.Any(Function(x) x.ServiceType = 3 And x.ThirdPartyId Is Nothing) Then
                    Mensaje(EeventViewerImages.Advertencia) = $"Los siguientes codigos de Profesional no existen en la tabla terceros del ERP : {String.Join(",", ListViewNoSurgical.Where(Function(x) x.ServiceType = 3 And x.ThirdPartyId Is Nothing).GroupBy(Function(a) a.PerformsHealthProfessionalCode).Select(Function(s) s.Key).ToList())}"
                End If
                INDgcMedicalFeesCausation.DataSource = Nothing
                INDgcMedicalFeesCausation.DataSource = ListViewNoSurgical
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Else
                INDgcMedicalFeesCausation.DataSource = Nothing
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            ViewNoSurgical.ExpandAllGroups()
        End Using
    End Function

    ''' <summary>
    ''' Metodo que carga la rejilla cuando los detalles de las ordenes son quirurgicos o paquetes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadGridSurgicalAndPackage()
        Task.Factory.StartNew(Sub()
                                  Using model As New MBusqueda
                                      ListViewSurgicalAndPackage = model.ConsultarEntidades(eDataSource.ListViewSurgicalAndPackageByInvoiceId, InvoiceId)
                                      Me.SafeInvoke(Sub()
                                                        If ListViewSurgicalAndPackage IsNot Nothing AndAlso ListViewSurgicalAndPackage.Count > 0 Then
                                                            INDgcMedicalFeesCausationSurgical.DataSource = Nothing
                                                            INDgcMedicalFeesCausationSurgical.DataSource = ListViewSurgicalAndPackage
                                                            INDlygDetailsSurgical.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                        Else
                                                            INDgcMedicalFeesCausationSurgical.DataSource = Nothing
                                                            INDlygDetailsSurgical.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                        End If
                                                    End Sub)
                                  End Using
                              End Sub)
    End Sub

    ''' <summary>
    ''' Metodo que abre el formulario modal para agregar el honorario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenAddFees()
        Me.Cursor = ChangeCursorIndigo()
        Dim session As DevExpress.Xpo.Session
        If SelectionSurgical Then 'Rejilla Qx
            session = ListViewSurgicalAndPackage.Session
        Else 'Rejilla NoQx
            session = Nothing
        End If
        Using Formulario As New FrmAddFees(itemXpoSurgical, itemNoSurgical, session, SelectionSurgical)
            AddHandler Formulario.SaveAddFees, AddressOf SaveAddFees
            Formulario.ToolBar.Visible = False
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim size As System.Drawing.Size
            size.Width = 473
            size.Height = 285
            Formulario.Size = size
            Dim transparent As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para guardar un honorario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub SaveAddFees(sender As Object, e As AddFeesEventArgs)
        Try
            Dim ServiceOrderDetailSurgical As ServiceOrderDetailSurgical = e.ServiceOrderDetailSurgical
            If ServiceOrderDetailSurgical IsNot Nothing Then
                AsyncLoader(True)
                Using model As New MServiceOrder(Me.Tag)
                    Dim result As ActionResult(Of ServiceOrderDetailSurgical) = Await model.SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical)
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = "Error al guardar el detalle quirúrgico."
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Mensaje(EeventViewerImages.Informacion) = "Detalle quirúrgico guardado correctamente."
                    If SelectionSurgical Then 'Rejilla Qx
                        Dim entityXpo As ViewListSurgicalAndPackageXpo = e.ViewListSurgicalAndPackage
                        entityXpo.ServiceOrderDetailSurgicalId = result.ObjectEmbbeded.Id
                        ListViewSurgicalAndPackage.Add(entityXpo)
                        INDgcMedicalFeesCausationSurgical.DataSource = ListViewSurgicalAndPackage
                        INDgcMedicalFeesCausationSurgical.RefreshDataSource()
                    Else 'Rejilla NoQx
                        Dim entityViewList As Domain.Entities.ViewListNoSurgical = e.DomainViewListNoSurgical
                        entityViewList.ServiceOrderDetailSurgicalId = result.ObjectEmbbeded.Id
                        ListViewNoSurgical.Add(entityViewList)
                        INDgcMedicalFeesCausation.DataSource = ListViewNoSurgical
                        INDgcMedicalFeesCausation.RefreshDataSource()
                    End If
                End Using
                AsyncLoader(False)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Elimina un honorario siempre y cuando el honorario haya sido agregado manualmente
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function RemoveFeeds() As Task
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Function
        End If
        Try
            AsyncLoader(True)
            'Se declaran las variables para poder realizar las validaciones
            Dim entityXpoSurgical As ViewListSurgicalAndPackageXpo = Nothing
            Dim entityNoSurgical As Domain.Entities.ViewListNoSurgical = Nothing
            Dim OnlyMedicalFees As Boolean
            Dim MedicalFeesCausationId As Integer
            Dim ServiceOrderDetailSurgicalId As Integer

            If SelectionSurgical Then 'Rejilla Qx
                entityXpoSurgical = DirectCast(ViewSurgicalAndPackage.GetFocusedRow, ViewListSurgicalAndPackageXpo)
                OnlyMedicalFees = entityXpoSurgical.OnlyMedicalFees
                MedicalFeesCausationId = entityXpoSurgical.MedicalFeesCausationId
                ServiceOrderDetailSurgicalId = entityXpoSurgical.ServiceOrderDetailSurgicalId
            Else 'Rejilla NoQx
                entityNoSurgical = DirectCast(ViewNoSurgical.GetFocusedRow, Domain.Entities.ViewListNoSurgical)
                OnlyMedicalFees = entityNoSurgical.OnlyMedicalFees
                MedicalFeesCausationId = entityNoSurgical.MedicalFeesCausationId
                ServiceOrderDetailSurgicalId = entityNoSurgical.ServiceOrderDetailSurgicalId
            End If

            'Se valida si el item fue agregado manualmente
            If OnlyMedicalFees = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontRemove", NAME_MODULE)
                AsyncLoader(False)
                Exit Function
            End If
            'Se valida que el item tenga causación registrada
            If MedicalFeesCausationId > 0 Then
                Using model As New MServiceOrder(Me.Tag)
                    'Se valida que la causacion no exista en ninguna liquidacion
                    Dim result As ActionResult(Of String) = Await model.ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId)
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                        AsyncLoader(False)
                        Exit Function
                    End If
                    If result.StateResultAux Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontRemoveBecauseLiquidation", NAME_MODULE)
                        AsyncLoader(False)
                        Exit Function
                    End If
                End Using
                Using model As New MMedicalFeesCausation(Me.Tag)
                    'Se elimina la causacion si existe el registro
                    Dim result As ActionResult = Await model.DeleteMedicalFeesCausationFromRemoveFees(MedicalFeesCausationId)
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                        AsyncLoader(False)
                        Exit Function
                    End If
                End Using
            End If
            Using model As New MServiceOrder(Me.Tag)
                'Se elimina el detalle quirurgico
                Dim result As ActionResult = Await model.DeleteServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                    AsyncLoader(False)
                    Exit Function
                End If
            End Using

            If SelectionSurgical Then 'Rejilla Qx
                ListViewSurgicalAndPackage.Remove(entityXpoSurgical)
                INDgcMedicalFeesCausationSurgical.RefreshDataSource()
            Else 'Rejilla NoQx
                ListViewNoSurgical.Remove(entityNoSurgical)
                INDgcMedicalFeesCausation.RefreshDataSource()
            End If

            AsyncLoader(False)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ItemDelete", NAME_MODULE)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina una causación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function RemoveCausation() As Task

        'Vista de las rejillas
        Dim ViewGrid As GridView
        If SelectionSurgical Then 'Rejilla Qx
            ViewGrid = ViewSurgicalAndPackage
        Else 'Rejilla NoQx
            ViewGrid = ViewNoSurgical
        End If
        'Items seleccionados
        Dim rowSelectCount = ViewGrid.SelectedRowsCount

        'Listado que se envia para eliminar
        Dim ListInfo As New List(Of Tuple(Of Integer, Integer))

        'Listado para mostrar en el form de errores
        Dim ListMessage As New List(Of Tuple(Of String, Integer))

        Dim position As Integer = 1
        For i = 0 To rowSelectCount - 1
            If ViewGrid.GetSelectedRows()(i) >= 0 Then
                Dim row = ViewGrid.GetRow(ViewGrid.GetSelectedRows()(i))
                If row IsNot Nothing AndAlso row.MedicalFeesCausationId > 0 Then
                    ListInfo.Add(New Tuple(Of Integer, Integer)(row.MedicalFeesCausationId, position))
                Else
                    ListMessage.Add(New Tuple(Of String, Integer)("El item " + position.ToString + " no tiene una causación guardada.", 2))
                End If
                position += 1
            End If
        Next

        If ListInfo IsNot Nothing AndAlso ListInfo.Count > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            Try
                Using Model As New MMedicalFeesCausation(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteMedicalFeesCausation(ListInfo, indigo.TransactionalContainer)
                    If result.StateResult = True Then
                        AsyncLoader(False)

                        If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then

                            result.ObjectEmbbeded.ForEach(Sub(item)
                                                              ListMessage.Add(New Tuple(Of String, Integer)(item.Item1, item.Item2))
                                                          End Sub)

                            Me.Cursor = ChangeCursorIndigo()
                            Using Formulario As New FrmListErrors(ListMessage)
                                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                Formulario.Width = 920
                                Formulario.Height = 600
                                Dim frm As New FrmTransparent(Formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                frm.ShowDialog(Me)
                            End Using

                            RefreshItems((From l In result.ObjectEmbbeded Where l.Item3 > 0 Select l.Item3).ToList)

                        End If
                    Else
                        AsyncLoader(False)
                        If result.MessageResult IsNot Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = result.MessageResult(0).ToString
                        ElseIf result.Message = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.Message = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        ElseIf ListMessage IsNot Nothing AndAlso ListMessage.Count > 0 Then
            Me.Cursor = ChangeCursorIndigo()
            Using Formulario As New FrmListErrors(ListMessage)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 920
                Formulario.Height = 600
                Dim frm As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)
            End Using
        End If
    End Function

    ''' <summary>
    ''' Actualiza los datos del item que se elimino
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshItems(ListMedicalFeesCausationId As List(Of Integer))
        If SelectionSurgical Then 'Si selecciono la rejilla de Qx
            If ListMedicalFeesCausationId IsNot Nothing AndAlso ListMedicalFeesCausationId.Count > 0 Then
                ListMedicalFeesCausationId.ForEach(Sub(itemId)
                                                       Dim info = (From item As ViewListSurgicalAndPackageXpo In ListViewSurgicalAndPackage Where item.MedicalFeesCausationId = itemId Select item).FirstOrDefault
                                                       If info IsNot Nothing Then
                                                           With info
                                                               .AmountPayable = 0
                                                               .ConfirmationDate = Nothing
                                                               .ConfirmationUser = Nothing
                                                               .CreationDate = Nothing
                                                               .CreationUser = Nothing
                                                               .MedicalFeesCausationId = 0
                                                               .MedicalFeesContractCodeName = " - "
                                                               .MedicalFeesContractId = 0
                                                               .ModificationDate = Nothing
                                                               .ModificationUser = Nothing
                                                               .SelectOption = False
                                                               .StatusMedicalFeesCausation = 0
                                                               .TotalAmountPayable = 0
                                                           End With
                                                       End If
                                                   End Sub)
            End If
            INDgcMedicalFeesCausationSurgical.RefreshDataSource()
        Else 'Si selecciono la rejilla de No Qx
            If ListMedicalFeesCausationId IsNot Nothing AndAlso ListMedicalFeesCausationId.Count > 0 Then
                ListMedicalFeesCausationId.ForEach(Sub(itemId)
                                                       Dim info = (From item As Domain.Entities.ViewListNoSurgical In ListViewNoSurgical Where item.MedicalFeesCausationId = itemId Select item).FirstOrDefault
                                                       If info IsNot Nothing Then
                                                           With info
                                                               .AmountPayable = 0
                                                               .ConfirmationDate = Nothing
                                                               .ConfirmationUser = Nothing
                                                               .CreationDate = Nothing
                                                               .CreationUser = Nothing
                                                               .MedicalFeesCausationId = 0
                                                               .MedicalFeesContractCodeName = " - "
                                                               .MedicalFeesContractId = 0
                                                               .ModificationDate = Nothing
                                                               .ModificationUser = Nothing
                                                               .SelectOption = False
                                                               .StatusMedicalFeesCausation = 0
                                                               .TotalAmountPayable = 0
                                                           End With
                                                       End If
                                                   End Sub)
            End If
            INDgcMedicalFeesCausation.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que abre el modal para cambiar el médico
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenChangeHealthProfessional()
        If SelectionSurgical Then 'Se valida que si esta en la rejilla de Qx no se deje seleccionar mas de dos items del mismo grupo
            'Vista de las rejilla Qx
            Dim view As GridView = ViewSurgicalAndPackage
            'Se captura los item que se hayan seleccionado
            Dim listHandlesSelected = view.GetSelectedRows
            If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
                'Listado para validar
                Dim ListValidate As List(Of ViewListSurgicalAndPackageXpo) = Nothing
                'Listado de errores
                Dim ListErrors As New StringBuilder
                'Se recorre los items seleccionados
                For i = 0 To listHandlesSelected.Count - 1
                    If Not view.IsGroupRow(listHandlesSelected(i)) Then 'Si el item no es un grupo, sino que es un registro como tal
                        If ListValidate Is Nothing Then
                            ListValidate = New List(Of ViewListSurgicalAndPackageXpo)
                            ListValidate.Add(view.GetRow(listHandlesSelected(i)))
                        Else
                            Dim row As ViewListSurgicalAndPackageXpo = view.GetRow(listHandlesSelected(i))
                            Dim cont = (From l In ListValidate Where l.IPSServiceDescriptionSOD = row.IPSServiceDescriptionSOD Select l).Count
                            If cont > 0 Then
                                ListErrors.AppendLine("No puede seleccionar dos items del grupo " + row.IPSServiceDescriptionSOD + ".")
                                Continue For
                            End If
                            ListValidate.Add(row)
                        End If
                    End If
                Next
                If ListErrors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
                    Exit Sub
                End If
            End If
        End If
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmChangeHealthProfessional()
            AddHandler Formulario.ChangeHealthProfessionalEvent, AddressOf ChangeHealthProfessionalEvent
            Formulario.ToolBar.Visible = False
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim size As System.Drawing.Size
            size.Width = 473
            size.Height = 285
            Formulario.Size = size
            Dim transparent As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para guardar un honorario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub ChangeHealthProfessionalEvent(sender As Object, e As ChangeHealthProfessionalEventArgs)
        If e Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Error al escoger el médico."
            Exit Sub
        End If
        Try
            AsyncLoader(True)

            'Listado de
            'Item1: ServiceOrderDetailId
            'Item2: ServiceOrderDetailSurgicalId
            'Item3: PerformsHealthProfessionalCode
            'Item4: ThirdPartyId
            Dim ListTuple As New List(Of Tuple(Of Integer, Integer, String, Integer))

            'Vista de las rejillas
            Dim view As GridView

            If SelectionSurgical Then 'Rejilla Qx
                view = ViewSurgicalAndPackage
            Else 'Rejilla NoQx
                view = ViewNoSurgical
            End If

            'Se captura los item que se hayan seleccionado
            Dim listHandlesSelected = view.GetSelectedRows
            If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
                'Se recorre los items seleccionados
                For i = 0 To listHandlesSelected.Count - 1
                    If Not view.IsGroupRow(listHandlesSelected(i)) Then 'Si el item no es un grupo, sino que es un registro como tal
                        If SelectionSurgical Then 'Rejilla Qx
                            Dim row As ViewListSurgicalAndPackageXpo = view.GetRow(listHandlesSelected(i))

                            'Se valida que el médico a cambiar no exista ya en el mismo ServiceOrderDetail
                            Dim cont = (From list In ListViewSurgicalAndPackage
                                        Where list.ServiceOrderDetailId = row.ServiceOrderDetailId And list.PerformsHealthProfessionalCode = e.PerformsHealthProfessionalCode
                                        Select list).Count
                            If cont > 0 Then
                                AsyncLoader(False)
                                Mensaje(EeventViewerImages.Advertencia) = "El médico " + e.PerformsHealthProfessionalDescription + " ya existe en el concepto " + row.IPSServiceDescriptionSOD + "."
                                Exit Sub
                            End If

                            ListTuple.Add(New Tuple(Of Integer, Integer, String, Integer)(row.ServiceOrderDetailId, row.ServiceOrderDetailSurgicalId, e.PerformsHealthProfessionalCode, e.ThirdPartyId))
                        Else 'Rejilla NoQx
                            Dim row As Domain.Entities.ViewListNoSurgical = view.GetRow(listHandlesSelected(i))
                            ListTuple.Add(New Tuple(Of Integer, Integer, String, Integer)(row.ServiceOrderDetailId, row.ServiceOrderDetailSurgicalId, e.PerformsHealthProfessionalCode, e.ThirdPartyId))
                        End If

                    End If
                Next
            End If

            'Se procede a actualizar las tablas de ServiceOrderDetail o ServiceOrderDetailSurgical dependiendo el caso
            If ListTuple.Count > 0 Then
                Using modelServiceOrder As New MServiceOrder(Tag)
                    Dim result As ActionResult = Await modelServiceOrder.UpdateHealthProfessionalForMedicalFeesCausation(ListTuple, SelectionSurgical, indigo.TransactionalContainer())
                    If result.StateResult = False Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                        Exit Sub
                    End If

                    'Se procede a cambiar los medicos de la rejilla
                    For Each item In ListTuple
                        If SelectionSurgical Then 'Rejilla Qx
                            'Se obtiene el row para actualizar los campos
                            Dim row As ViewListSurgicalAndPackageXpo = (From list In ListViewSurgicalAndPackage
                                                                        Where list.ServiceOrderDetailId = item.Item1 AndAlso list.ServiceOrderDetailSurgicalId = item.Item2
                                                                        Select list).FirstOrDefault
                            'Se asignan los nuevos campos al row
                            row.PerformsHealthProfessionalCode = e.PerformsHealthProfessionalCode
                            row.ThirdPartyId = e.ThirdPartyId
                            row.ThirdPartyDescription = e.PerformsHealthProfessionalDescription
                        Else 'Rejilla NoQx
                            'Se obtiene el row para actualizar los campos
                            Dim row As Domain.Entities.ViewListNoSurgical = (From list In ListViewNoSurgical
                                                                             Where list.ServiceOrderDetailId = item.Item1 AndAlso list.ServiceOrderDetailSurgicalId = item.Item2
                                                                             Select list).FirstOrDefault
                            'Se asignan los nuevos campos
                            row.PerformsHealthProfessionalCode = e.PerformsHealthProfessionalCode
                            row.ThirdPartyId = e.ThirdPartyId
                            row.ThirdPartyDescription = e.PerformsHealthProfessionalDescription
                        End If
                    Next

                    If SelectionSurgical Then 'Rejilla Qx
                        INDgcMedicalFeesCausationSurgical.RefreshDataSource()
                    Else 'Rejilla NoQx
                        INDgcMedicalFeesCausation.RefreshDataSource()
                    End If

                    Mensaje(EeventViewerImages.Informacion) = "Médicos cambiados correctamente."
                End Using
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para causar masivamente
    ''' </summary>
    ''' <param name="ListNoSurgical">Listado de NoQx que se envia para causar el valor masivamente</param>
    ''' <param name="ListSurgical">Listado de Qx que se envia para causar el valor masivamente</param>
    ''' <param name="withHomologations">Variable para saber si el metodo es llamado desde el contextMenu(False) o desde el retorno de las homologaciones(True)</param>
    ''' <remarks></remarks>
    Private Async Function CauseMassively(ListNoSurgical As List(Of NoQxEntity), ListSurgical As List(Of QxEntity), withHomologations As Boolean) As Task
        Dim ListErrors As New StringBuilder
        If withHomologations = False Then 'Si viene desde el contextMenu
            'Vista de las rejillas
            Dim ViewGrid As GridView
            If SelectionSurgical Then 'Rejilla Qx
                ViewGrid = ViewSurgicalAndPackage
                ListSurgical = New List(Of QxEntity)
            Else 'Rejilla NoQx
                ViewGrid = ViewNoSurgical
                ListNoSurgical = New List(Of NoQxEntity)
            End If

            Dim rowSelectCount = ViewGrid.SelectedRowsCount

            'Listado de errores al validar que el item no se encuentre en una liquidación
            For i = 0 To rowSelectCount - 1
                If ViewGrid.GetSelectedRows()(i) >= 0 Then
                    Dim row = ViewGrid.GetRow(ViewGrid.GetSelectedRows()(i))
                    ''Se valida que el item no este en una liquidación
                    'If row.MedicalFeesCausationId <> Nothing Then
                    '    If row.StatusMedicalFeesCausation = 2 Then 'Liquidación registrada
                    '        ListErrors.AppendLine("El item " + row.IPSServiceDescription + " ya se encuentra en una liquidación registrada.")
                    '    ElseIf row.StatusMedicalFeesCausation = 3 Then 'Liquidación confirmada
                    '        ListErrors.AppendLine("El item " + row.IPSServiceDescription + " ya se encuentra en una liquidación confirmada.")
                    '    End If
                    'End If
                    If SelectionSurgical Then 'Rejilla Qx
                        Dim entity As New QxEntity With {.MedicalFeesCausationId = row.MedicalFeesCausationId, .RateManualType = row.RateManualType, .CupsEntityId = row.CupsEntityId, .CareGroupId = row.CareGroupId, .RateManualId = row.RateManualId, .TotalSalesPrice = row.TotalSalesPrice,
                                                           .Presentation = row.Presentation, .MedicalFeesContractId = row.MedicalFeesContractId, .IPSServiceDescription = row.IPSServiceDescription,
                                                           .PerformsHealthProfessionalCode = row.PerformsHealthProfessionalCode, .ThirdPartyDescription = row.ThirdPartyDescription, .IPSServiceSODId = row.IPSServiceSODId,
                                                           .ServiceOrderDetailId = row.ServiceOrderDetailId, .ServiceOrderDetailSurgicalId = row.ServiceOrderDetailSurgicalId, .IPSServiceId = row.IPSServiceId}
                        ListSurgical.Add(entity)
                    Else 'Rejilla NoQx
                        Dim entity As New NoQxEntity With {.MedicalFeesCausationId = row.MedicalFeesCausationId, .RateManualType = row.RateManualType, .CupsEntityId = row.CupsEntityId, .CareGroupId = row.CareGroupId, .RateManualId = row.RateManualId, .TotalSalesPrice = row.TotalSalesPrice,
                                                           .Presentation = row.Presentation, .MedicalFeesContractId = row.MedicalFeesContractId, .IPSServiceDescription = row.IPSServiceDescription,
                                                           .PerformsHealthProfessionalCode = row.PerformsHealthProfessionalCode, .ThirdPartyDescription = row.ThirdPartyDescription, .IPSServiceSODId = Nothing,
                                                           .ServiceOrderDetailId = row.ServiceOrderDetailId, .ServiceOrderDetailSurgicalId = row.ServiceOrderDetailSurgicalId, .IPSServiceId = row.IPSServiceId}
                        ListNoSurgical.Add(entity)
                    End If
                End If
            Next
            ''Se devuelve el mensaje de error si lo hay
            'If ListErrors.Length > 0 Then
            '    Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
            '    Exit Function
            'End If
        End If

        'Se envia la info al metodo que causa masivamente
        Using model As New MMedicalFeesCausation(Tag)
            Dim result = Await model.CauseMassively(ListNoSurgical, ListSurgical)
            If result.StateResult = False Then 'Si ocurrio algun error
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Function
            End If

            'Se instancia de nuevo el listado Qx para agregar los items que tienen homologacion
            ListSurgical = New List(Of QxEntity)
            'Se instancia de nuevo el listado NoQx para agregar los items que tienen homologacion
            ListNoSurgical = New List(Of NoQxEntity)

            'Si no ocurrio ningun error sigue el proceso
            If SelectionSurgical Then 'Rejilla Qx

                'Se recorre el item que representa al ListSurgical devuelto con la info tramitada
                For Each item In result.ObjectEmbbeded.Item2
                    If item.ListCupsHomologation IsNot Nothing AndAlso item.ListCupsHomologation.Count > 0 AndAlso item.IsHomologations = True AndAlso item.StatusField = 1 Then 'Se asignan las homologaciones
                        ListSurgical.Add(item)
                    ElseIf item.StatusField = 1 Then 'Se asignan los valores
                        'Se obtiene el item de la rejilla para poder asignar los nuevos valores
                        Dim infoGrid = (From l In ListViewSurgicalAndPackage
                                        Where l.ServiceOrderDetailId = item.ServiceOrderDetailId AndAlso l.ServiceOrderDetailSurgicalId = item.ServiceOrderDetailSurgicalId
                                        Select l).FirstOrDefault
                        If infoGrid IsNot Nothing Then

                            'Se valida si el item es paquete o hijo para asi establecer si se puede causar o no
                            Dim resultValidatePackage As ActionResult = ValidatePackage(infoGrid)
                            If resultValidatePackage.StateResult = False Then
                                ListErrors.AppendLine(resultValidatePackage.Message)
                                Continue For
                            End If

                            'Se asignan los nuevos valores
                            infoGrid.AmountPayable = item.AmountPayable
                            infoGrid.MedicalFeesContractId = item.MedicalFeesContractId
                            infoGrid.TotalAmountPayable = item.AmountPayable * infoGrid.InvoicedQuantity
                            infoGrid.MedicalFeesContractCodeName = item.MedicalFeesContractCodeName
                            infoGrid.TotalAmountPayableReal = infoGrid.TotalAmountPayable
                            infoGrid.PercentageCashed = 100
                            infoGrid.SelectOption = True
                        End If
                    Else 'Sino es porque ocurrio algun error con el item
                        ListErrors.AppendLine(item.MessageField)
                    End If
                Next
                'Se refresca la rejilla con los nuevos valores
                INDgcMedicalFeesCausationSurgical.RefreshDataSource()

            Else 'Rejilla NoQx

                'Se recorre el item que representa al ListNoSurgical devuelto con la info tramitada
                For Each item In result.ObjectEmbbeded.Item1
                    If item.ListCupsHomologation IsNot Nothing AndAlso item.ListCupsHomologation.Count > 0 AndAlso item.IsHomologations = True AndAlso item.StatusField = 1 Then 'Se asignan las homologaciones
                        ListNoSurgical.Add(item)
                    ElseIf item.StatusField = 1 Then 'Se asignan los valores
                        'Se obtiene el item de la rejilla para poder asignar los nuevos valores
                        Dim infoGrid = (From l In ListViewNoSurgical
                                        Where l.ServiceOrderDetailId = item.ServiceOrderDetailId AndAlso l.ServiceOrderDetailSurgicalId = item.ServiceOrderDetailSurgicalId
                                        Select l).FirstOrDefault
                        If infoGrid IsNot Nothing Then
                            'Se asignan los nuevos valores
                            infoGrid.AmountPayable = item.AmountPayable
                            infoGrid.MedicalFeesContractId = item.MedicalFeesContractId
                            infoGrid.TotalAmountPayable = item.AmountPayable * infoGrid.InvoicedQuantity
                            infoGrid.MedicalFeesContractCodeName = item.MedicalFeesContractCodeName
                            infoGrid.TotalAmountPayableReal = infoGrid.TotalAmountPayable
                            infoGrid.PercentageCashed = 100
                            infoGrid.SelectOption = True
                        End If
                    Else 'Sino es porque ocurrio algun error con el item
                        ListErrors.AppendLine(item.MessageField)
                    End If
                Next
                'Se refresca la rejilla con los nuevos valores
                INDgcMedicalFeesCausation.RefreshDataSource()

            End If

            'Se devuelve el mensaje de error si lo hay
            If ListErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
            End If

            'Se envia las homologaciones de los item siempre y cuando existan en los listados de ListNoSurgical o ListSurgical
            If (ListNoSurgical IsNot Nothing AndAlso ListNoSurgical.Count > 0) OrElse (ListSurgical IsNot Nothing AndAlso ListSurgical.Count > 0) Then
                'Metodo que abre el form de las homologaciones para que el usuario escoja
                OpenSelectHomologations(ListNoSurgical, ListSurgical)
            End If

        End Using


    End Function

    ''' <summary>
    ''' Metodo que abre el form donde se escogen las homologaciones para los diferentes items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenSelectHomologations(ListNoQx As List(Of NoQxEntity), ListQx As List(Of QxEntity))
        Using Formulario As New FrmSelectHomologations
            AddHandler Formulario.SetSelectHomologations, AddressOf ReturnSelectHomologations
            Formulario.ListNoQx = ListNoQx
            Formulario.ListQx = ListQx
            Formulario.StartPosition = FormStartPosition.CenterParent
            Dim transParent As New FrmTransparent(Formulario, False)
            transParent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que recibe los item con las homologaciones seleccionada en el popup cuando un servicio tiene mas de una
    ''' </summary>
    ''' <param name="Senders"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnSelectHomologations(Senders As Object, e As SetSelectHomologationsEventArgs)
        Await CauseMassively(e.ListNoQx, e.ListQx, True)
    End Sub

    ''' <summary>
    ''' Metodo que abre el form de seleccionar el porcentaje
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenSelectPercentage()
        'Variable para validar
        Dim SelectOption As Boolean
        If SelectionSurgical Then 'Rejilla Qx
            SelectOption = itemXpoSurgical.SelectOption
        Else 'Rejilla NoQx
            SelectOption = itemNoSurgical.SelectOption
        End If

        'Se valida que el item que se selecciono este checkiado(Hayan causado el valor previamente) o sino no se deja desplegar el form
        If SelectOption = False Then 'No se deja pasar y se devuelve un mensaje
            Mensaje(EeventViewerImages.Advertencia) = "Debe causar primero."
            Exit Sub
        End If

        Using Formulario As New FrmSelectPercentage
            AddHandler Formulario.SetPercentage, AddressOf ReturnPercentage
            Formulario.ToolBar.Visible = False
            Formulario.StartPosition = FormStartPosition.CenterParent
            Dim size As System.Drawing.Size
            size.Width = 260
            size.Height = 230
            Formulario.Size = size
            Dim transParent As New FrmTransparent(Formulario, False)
            transParent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorna el porcentaje que se le aplica al item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReturnPercentage(Senders As Object, e As SelectContractEventArgs)
        If SelectionSurgical Then 'Rejilla Qx
            itemXpoSurgical.PercentageCashed = e.Percentage
            itemXpoSurgical.TotalAmountPayable = itemXpoSurgical.TotalAmountPayable * e.Percentage / 100
            INDgcMedicalFeesCausationSurgical.RefreshDataSource()
        Else 'Rejilla NoQx
            itemNoSurgical.PercentageCashed = e.Percentage
            itemNoSurgical.TotalAmountPayable = itemNoSurgical.TotalAmountPayable * e.Percentage / 100
            INDgcMedicalFeesCausation.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Carga la infor
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    Public Sub LoadData(admissionNumber As String, invoiceId As Integer, invoiceDetailId As Integer)
        _asModal = True
        _invoiceDetailId = invoiceDetailId
        Using model As New MLiquidation()
            Dim admissionTempKeyDown As Object = model.GetAdmissionsToLiquidationCollection(admissionNumber)

            If admissionTempKeyDown Is Nothing OrElse admissionTempKeyDown.Count = 0 Then
                admissionTempKeyDown = model.GetAdmissionsToLiquidationConfirmCollection(admissionNumber)
            End If

            Dim admissionObject = admissionTempKeyDown(0)
            Dim adm As New With {.AdmissionCode = admissionNumber,
                                .PatientCode = admissionObject.PatientCode,
                                .PatientName = admissionObject.PatientName,
                                .PatientDateBirth = admissionObject.PatientBirth,
                                .PatientGenus = admissionObject.PatientGenus,
                                .AdmissionDate = admissionObject.AdmissionDate,
                                .AdmissionType = admissionObject.AdmissionType,
                                .BedStay = admissionObject.BedStay,
                                .PlaceEntry = admissionObject.PlaceEntry,
                                .LiquidationType = admissionObject.LiquidationType,
                                .BenefitPlan = admissionObject.BenefitPlan,
                                .EntityCode = admissionObject.EntityCode,
                                .EntityName = admissionObject.EntityName,
                                .AuthorizationNumber = admissionObject.AuthorizationNumber,
                                .ResponsibleName = admissionObject.ResponsibleName,
                                .ResponsiblePhone = admissionObject.ResponsiblePhone,
                                .CareGroupId = admissionObject.AdmissionCaregroupId,
                                .HealthAdministratorId = admissionObject.HealthAdministratorId,
                                .FunctionalUnitCode = admissionObject.AdmissionUniFuncCodeName.Split("-")(0), '.UFUCODIGO,
                                .Status = admissionObject.Status,
                                .StatusName = If(admissionObject.Status = "F", "Facturado", "Parcialmente Facturado"),
                                .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admissionObject.AdmissionCode.ToString().Trim(), admissionObject.PatientCode.ToString().Trim(), admissionObject.PatientName.ToString().Trim())
            }
            INDsleAdmission_NewSelectedValue(Me, New SearchAdmissionClosingEventArgs(adm))
            INDsleInvoice.EditValue = invoiceId
            INDsleInvoice.ReadOnly = True
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el boton de visualizar el reporte de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddButtonSeeReportInvoice()
        Dim xpo As InvoiceXpo = Nothing

        If viewSearchInvoice.GetFocusedRow Is Nothing Then
            xpo = viewSearchInvoice.Tag
        Else
            xpo = DirectCast(DirectCast(viewSearchInvoice.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BillingRepository.InvoiceXpo)
        End If

        If xpo IsNot Nothing Then
            AdmissionNumberInvoice = xpo.AdmissionNumber
        End If

        Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleInvoice.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
        If info Is Nothing Then
            Dim editor As New DevExpress.XtraEditors.Controls.EditorButton
            editor.Image = Presentation.Controls.My.Resources.Resources.permiso_16x16
            editor.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            editor.Visible = True
            INDsleInvoice.Properties.Buttons.Add(editor)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que abre el reporte de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SeeReportInvoice()
        If InvoiceId IsNot Nothing AndAlso AdmissionNumberInvoice IsNot String.Empty Then
            Dim reportDefSaleInvoice = New Reporter.rptSaleInvoice()
            reportDefSaleInvoice.ParametrosReporte = New Object() {InvoiceId, AdmissionNumberInvoice}
            ReportHelper.ExecuteReport(reportDefSaleInvoice, Me, BarraBotones.PermissionsForm)
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        Presenter = Nothing
        medicalFeesCausation = Nothing
        ListCupsHomologation = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        admission = Nothing
        _patientCode = Nothing
        ListMedicalFeesCausation = Nothing
        banSaveAndModify = Nothing
        ListViewNoSurgical = Nothing
        ListViewSurgicalAndPackage = Nothing
        ListMedicalFeesNotes = Nothing
        BanAddOrModify = Nothing
        _medicalFeesNotes = Nothing
        ContainsPermissionAddFees = Nothing
        ContainsPermissionManualCausation = Nothing
        ContainsPermissionChangeHealthProfessional = Nothing
        ContainsPermissionDelete = Nothing
        itemXpoSurgical = Nothing
        itemNoSurgical = Nothing
        SelectionSurgical = Nothing
        patientDescription = Nothing
        entityDescription = Nothing
        AdmissionNumberInvoice = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesCausation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMedicalFeesCausation, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PMedicalFeesCausation(Me)
        Presenter.InitializeAdmission()
        INDsleAdmission.PopupContainerControl = INDPccMoreInfoAdmission
        LoadStatus()
        DeshacerTodo()

        INDsleInvoice.Properties.Buttons.Item(1).Visible = False
        IndigoGridControl1.RefreshGrid(INDgcMedicalFeesCausation)
        IndigoGridControl1.RefreshGrid(INDgcMedicalFeesCausationSurgical)

        'Rejilla de No Quirurgicos
        IndigoGridView1.MoreInfoColunmns(ViewNoSurgical)
        'Rejilla de Quirurgicos
        IndigoGridView2.MoreInfoColunmns(ViewSurgicalAndPackage)

        'Acciones de rejilla de No Quirurgicos
        Dim ListActionsNoSurgical As New List(Of eAcciones)
        If ContainsPermissionAddFees > 0 Then
            ListActionsNoSurgical.Add(eAcciones.AddFees)
        End If
        ListActionsNoSurgical.Add(eAcciones.ChangeContract)
        If ContainsPermissionDelete > 0 Then
            ListActionsNoSurgical.Add(eAcciones.Remove)
            ListActionsNoSurgical.Add(eAcciones.DeleteCausation)
        End If
        If ContainsPermissionChangeHealthProfessional > 0 Then
            ListActionsNoSurgical.Add(eAcciones.ChangeHealthProfessional)
        End If
        ListActionsNoSurgical.Add(eAcciones.CausedValue)
        ListActionsNoSurgical.Add(eAcciones.SelectPercentage)
        IndigoGridView1.SetListAcction(ViewNoSurgical, ListActionsNoSurgical)

        'Acciones de rejilla de Quirurgicos
        Dim ListActionsSurgical As New List(Of eAcciones)
        If ContainsPermissionAddFees > 0 Then
            ListActionsSurgical.Add(eAcciones.AddFees)
        End If
        ListActionsSurgical.Add(eAcciones.ChangeContract)
        If ContainsPermissionDelete > 0 Then
            ListActionsSurgical.Add(eAcciones.Remove)
            ListActionsSurgical.Add(eAcciones.DeleteCausation)
        End If
        If ContainsPermissionChangeHealthProfessional > 0 Then
            ListActionsSurgical.Add(eAcciones.ChangeHealthProfessional)
        End If
        ListActionsSurgical.Add(eAcciones.CausedValue)
        ListActionsSurgical.Add(eAcciones.SelectPercentage)
        IndigoGridView2.SetListAcction(ViewSurgicalAndPackage, ListActionsSurgical)

        'Rejilla de Notas
        IndigoGridControl1.RefreshGrid(INDgcNotes)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        If ContainsPermissionDelete > 0 Then
            ListActions.Add(eAcciones.Remove)
        End If
        IndigoGridView3.SetListAcction(viewNotes, ListActions)

        ModifiedColumns()

        ' Make the group footers always visible.
        ViewNoSurgical.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
        ' Create and setup the second summary item.
        Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
        item1.FieldName = "TotalAmountPayable"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "Total: {0:c0}"
        item1.ShowInGroupColumnFooter = ViewNoSurgical.Columns("TotalAmountPayable")
        ViewNoSurgical.GroupSummary.Add(item1)

        ' Make the group footers always visible.
        ViewSurgicalAndPackage.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
        ' Create and setup the second summary item.
        item1 = New GridGroupSummaryItem()
        item1.FieldName = "TotalAmountPayable"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "Total: {0:c0}"
        item1.ShowInGroupColumnFooter = ViewSurgicalAndPackage.Columns("TotalAmountPayable")
        ViewSurgicalAndPackage.GroupSummary.Add(item1)

        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        CausationDate = GetDateServer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmMedicalFeesCausation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesCausation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleAdmission.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvoice.EditValueChanged
        If InvoiceId IsNot Nothing Then
            Try
                AsyncLoader(True)
                Using ModelRecord As New MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(InvoiceId.Value))
                    If result.Id = 0 Or result.CodUser = indigo.UserIndigo Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New BlockRecordMedicalFees With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = InvoiceId.Value}
                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                        INDgcMedicalFeesCausation.DataSource = Nothing
                        INDgcMedicalFeesCausation.RefreshDataSource()
                        Await LoadGridNoSurgical()
                        LoadGridSurgicalAndPackage()
                        Await ConsultMedicalFeesNotes()
                        InitializeRepository()
                        InitializeUserControl()
                        AddButtonSeeReportInvoice()

                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True

                        If _asModal Then
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = True
                        Else
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                        End If
                    Else
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Mensaje(EeventViewerImages.Advertencia) = xtraMessage
                    End If
                End Using
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de la rejilla de valor causado
    ''' de la rejilla quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtCausedValueSurgical_EditValueChanged(sender As Object, e As EventArgs) Handles INDrepTxtCausedValueSurgical.EditValueChanged
        Dim itemCollection As ViewListSurgicalAndPackageXpo = ViewSurgicalAndPackage.GetFocusedRow()
        Dim control As DevExpress.XtraEditors.TextEdit = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
        itemCollection.AmountPayable = control.EditValue
        itemCollection.TotalAmountPayable = ((itemCollection.AmountPayable * itemCollection.InvoicedQuantity) * itemCollection.PercentageCashed / 100)
        itemCollection.TotalAmountPayableReal = itemCollection.AmountPayable * itemCollection.InvoicedQuantity
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de la rejilla de valor causado
    ''' de la rejilla no quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtCausedValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDrepTxtCausedValue.EditValueChanged
        Dim itemCollection As Domain.Entities.ViewListNoSurgical = ViewNoSurgical.GetFocusedRow()
        Dim control As DevExpress.XtraEditors.TextEdit = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
        itemCollection.AmountPayable = control.EditValue
        itemCollection.TotalAmountPayable = ((itemCollection.AmountPayable * itemCollection.InvoicedQuantity) * itemCollection.PercentageCashed / 100)
        itemCollection.TotalAmountPayableReal = itemCollection.AmountPayable * itemCollection.InvoicedQuantity
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de seleccionar opcion en la rejilla de No-Quirurgicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        Dim itemCollection As Domain.Entities.ViewListNoSurgical = ViewNoSurgical.GetFocusedRow()
        If itemCollection IsNot Nothing And e.NewValue Then
            Dim result As ActionResult = Await CalculateValueNoSurgical(e.NewValue, itemCollection)
            If result.StateResult = False Then
                If result.Message IsNot String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Dim control As DevExpress.XtraEditors.CheckEdit = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
                control.EditValue = False
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de seleccionar opcion en la rejilla de Quirurgicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrepCheckSelectOptionSurgical_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOptionSurgical.EditValueChanging
        Dim itemCollection As ViewListSurgicalAndPackageXpo = ViewSurgicalAndPackage.GetFocusedRow()
        If itemCollection IsNot Nothing Then
            Dim result As ActionResult
            'Primero se valida si el item es paquete o es hijo del paquete
            result = ValidatePackage(itemCollection)
            If result.StateResult = False Then
                If result.Message IsNot String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Dim control As DevExpress.XtraEditors.CheckEdit = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
                control.EditValue = False
                e.Cancel = True
                Exit Sub
            End If

            'Se calcula el valor a causar
            result = Await CalculateValueSurgical(e.NewValue, itemCollection)
            If result.StateResult = False Then
                If result.Message IsNot String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Dim control As DevExpress.XtraEditors.CheckEdit = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
                control.EditValue = False
                e.Cancel = True
                Exit Sub
            End If
            'itemCollection.SelectOption = e.NewValue
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida si hay paquetes y adicionalmente los hijos de ese paquete
    ''' para no dejar que se seleccionen los dos
    ''' (si causan el padre no pueden causar los hijos, si causan los hijos no pueden causar el padre)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidatePackage(itemCollection As ViewListSurgicalAndPackageXpo) As ActionResult
        Dim message As String = String.Empty
        If itemCollection.IncludeServiceOrderDetailId = 0 And itemCollection.Presentation = 3 Then 'Si el item es un paquete padre
            'Verifico si el ServiceOrderDetailId del paquete padre tiene incluidos otros items validandolo contra el campo de IncludeServiceOrderDetailId y ademas que este causado
            Dim ListCount = (From x As ViewListSurgicalAndPackageXpo In ListViewSurgicalAndPackage
                             Where (x.IncludeServiceOrderDetailId = itemCollection.ServiceOrderDetailId AndAlso x.SelectOption = True) _
                             OrElse (x.IncludeServiceOrderDetailId = itemCollection.ServiceOrderDetailId AndAlso x.MedicalFeesCausationId > 0)
                             Select x).ToList
            If ListCount IsNot Nothing AndAlso ListCount.Count > 0 Then 'Si es mayor a cero es porque no se puede causar el padre ya que el hijo esta causado
                message = BuildMessageError(ListCount)
                Return New ActionResult With {.StateResult = False, .Message = "El item " + itemCollection.IPSServiceDescription + " no puede causarse porque hay items incluidos causados: " + message}
            End If
        Else 'Si el item no es un paquete padre
            'Valido que el item este incluido dentro de un paquete
            If itemCollection.IncludeServiceOrderDetailId > 0 Then
                'Obtengo el paquete padre y valido si esta causado
                Dim ListCount = (From x As ViewListSurgicalAndPackageXpo In ListViewSurgicalAndPackage
                                 Where (x.ServiceOrderDetailId = itemCollection.IncludeServiceOrderDetailId AndAlso x.SelectOption = True) _
                                 OrElse (x.ServiceOrderDetailId = itemCollection.IncludeServiceOrderDetailId AndAlso x.MedicalFeesCausationId > 0)
                                 Select x).ToList
                If ListCount IsNot Nothing AndAlso ListCount.Count > 0 Then 'Si es mayor a cero es porque no se puede causar el hijo ya que el padre esta causado
                    message = BuildMessageError(ListCount)
                    Return New ActionResult With {.StateResult = False, .Message = "El item " + itemCollection.IPSServiceDescription + " no puede causarse porque el código del paquete está causado: " + message}
                End If
            End If
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Metodo que construye el mensaje de error
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BuildMessageError(List As List(Of ViewListSurgicalAndPackageXpo)) As String
        Dim message As String = String.Empty
        Dim counter As Integer = 0
        For Each item In List
            If counter <> 0 Then
                message += ",  "
            Else
                counter = 1
            End If
            message += item.IPSServiceDescription
        Next
        Return message
    End Function

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de cambiar el médico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrepSleHealthProfessionalGridNoSurgical_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepSleHealthProfessionalGridNoSurgical.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Try
                AsyncLoader(True)
                Dim healthProfessional As Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo
                Dim itemCollection As Domain.Entities.ViewListNoSurgical
                Using modelServiceOrder As New MServiceOrder(Me.Tag)
                    Dim healthProfessionalTmp = modelServiceOrder.GetCareProfessionalByCode(e.NewValue.ToString.Trim)
                    healthProfessional = healthProfessionalTmp(0)

                    Using modelThirdParty As New MThirdParty(Me.Tag)
                        Dim thirdParty = modelThirdParty.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                        If thirdParty.Id = 0 Then
                            AsyncLoader(False)
                            'si el medico no esta creado como tercero en la BD no continua el proceso
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), healthProfessional.CodeName)
                            e.Cancel = True
                            Exit Sub
                        End If

                        itemCollection = ViewNoSurgical.GetFocusedRow()
                        itemCollection.PerformsHealthProfessionalCode = e.NewValue
                        itemCollection.ThirdPartyId = thirdParty.Id
                        itemCollection.ThirdPartyDescription = healthProfessional.CodeName
                    End Using

                    Dim result As ActionResult(Of ServiceOrderDetail) = Await modelServiceOrder.UpdateFieldsServiceOrderDetail(itemCollection.ServiceOrderDetailId, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyId)
                    If result.StateResult = False Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                        Exit Sub
                    End If
                End Using
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de cambiar el médico en la rejilla de quirurgicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrepSleHealthProfessionalSurgical_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepSleHealthProfessionalSurgical.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Try
                AsyncLoader(True)
                Dim healthProfessional As Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo
                Dim itemCollection As ViewListSurgicalAndPackageXpo
                Using modelServiceOrder As New MServiceOrder(Me.Tag)
                    Dim healthProfessionalTmp = modelServiceOrder.GetCareProfessionalByCode(e.NewValue.ToString.Trim)
                    healthProfessional = healthProfessionalTmp(0)

                    Using modelThirdParty As New MThirdParty(Me.Tag)
                        Dim thirdParty = modelThirdParty.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                        If thirdParty.Id = 0 Then
                            AsyncLoader(False)
                            'si el medico no esta creado como tercero en la BD no continua el proceso
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), healthProfessional.CodeName)
                            e.Cancel = True
                            Exit Sub
                        End If

                        itemCollection = ViewSurgicalAndPackage.GetFocusedRow()

                        'Se valida que el médico a cambiar no exista ya en el mismo ServiceOrderDetail
                        Dim cont = (From list In ListViewSurgicalAndPackage Where list.ServiceOrderDetailId = itemCollection.ServiceOrderDetailId And list.PerformsHealthProfessionalCode = e.NewValue Select list).Count
                        If cont > 0 Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = "El médico " + healthProfessional.CodeName + " ya existe en este grupo."
                            e.Cancel = True
                            Exit Sub
                        End If

                        itemCollection.PerformsHealthProfessionalCode = e.NewValue
                        itemCollection.ThirdPartyId = thirdParty.Id
                        itemCollection.ThirdPartyDescription = healthProfessional.CodeName
                    End Using

                    If itemCollection.ServiceOrderDetailSurgicalId > 0 Then 'Preguntamos por el id del quirurgico para actualizar la tabla ServiceOrderDetailSurgical
                        Dim result As ActionResult(Of ServiceOrderDetailSurgical) = Await modelServiceOrder.UpdateFieldsServiceOrderDetailSurgical(itemCollection.ServiceOrderDetailSurgicalId, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyId)
                        If result.StateResult = False Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                            Exit Sub
                        End If
                    Else 'Sino se actualiza la tabla ServiceOrderDetail
                        Dim result As ActionResult(Of ServiceOrderDetail) = Await modelServiceOrder.UpdateFieldsServiceOrderDetail(itemCollection.ServiceOrderDetailId, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyId)
                        If result.StateResult = False Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                            Exit Sub
                        End If
                    End If

                End Using
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

#End Region

#Region "NewSelectedValue"

    ''' <summary>
    ''' Evento que se dispara al seleccionar un valor en el search de ingresos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdmission_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDsleAdmission.NewSelectedValue
        SetAdmission(e.AdmissionObject)
        Presenter.InitializeInvoice(admission.AdmissionCode.ToString().Trim())

        ActionsOnControls = True
    End Sub

#End Region

#Region "ShowingEditor"

    ''' <summary>
    ''' Evento que se dispara cuando interactuo con la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ViewNoSurgical_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ViewNoSurgical.ShowingEditor
        Dim pMouse As System.Drawing.Point = INDgcMedicalFeesCausation.PointToClient(Control.MousePosition)
        Dim hit = ViewNoSurgical.CalcHitInfo(pMouse)
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolHealthProfessional") Then
            If ContainsPermissionChangeHealthProfessional > 0 Then
                e.Cancel = False
            Else
                e.Cancel = True
                Exit Sub
            End If
            Dim itemCollection As Domain.Entities.ViewListNoSurgical = ViewNoSurgical.GetFocusedRow()
            If itemCollection IsNot Nothing Then
                If itemCollection.StatusMedicalFeesCausation = 2 Or itemCollection.StatusMedicalFeesCausation = 3 Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        End If
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolCausedValueNoSurgical") Then
            If ContainsPermissionManualCausation > 0 Then
                Dim itemCollection As Domain.Entities.ViewListNoSurgical = ViewNoSurgical.GetFocusedRow()
                If itemCollection.SelectOption Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando interactuo con la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ViewSurgicalAndPackage_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ViewSurgicalAndPackage.ShowingEditor
        Dim pMouse As System.Drawing.Point = INDgcMedicalFeesCausationSurgical.PointToClient(Control.MousePosition)
        Dim hit = ViewSurgicalAndPackage.CalcHitInfo(pMouse)
        'If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("MoreInfo") Then
        '    Dim itemCollection As ViewListSurgicalAndPackage = ViewSurgicalAndPackage.GetFocusedRow()
        '    If itemCollection.MedicalFeesCausationId > 0 Then
        '        e.Cancel = False
        '    Else
        '        e.Cancel = True
        '    End If
        'End If
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolHealthProfessionalSurgical") Then
            If ContainsPermissionChangeHealthProfessional > 0 Then
                e.Cancel = False
            Else
                e.Cancel = True
                Exit Sub
            End If
            Dim itemCollection As ViewListSurgicalAndPackageXpo = ViewSurgicalAndPackage.GetFocusedRow()
            If itemCollection IsNot Nothing Then
                If itemCollection.StatusMedicalFeesCausation = 2 Or itemCollection.StatusMedicalFeesCausation = 3 Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        End If
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolCausedValueSurgical") Then
            If ContainsPermissionManualCausation > 0 Then
                Dim itemCollection As ViewListSurgicalAndPackageXpo = ViewSurgicalAndPackage.GetFocusedRow()
                If itemCollection.SelectOption Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "MenuContextual"

#Region "IndigoGridView1 NoSurgical"

    Private Async Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "AddFees"
                SelectionSurgical = False
                itemNoSurgical = Nothing
                itemNoSurgical = ViewNoSurgical.GetFocusedRow()
                itemXpoSurgical = Nothing
                OpenAddFees()
            Case "Remove"
                SelectionSurgical = False
                Await RemoveFeeds()
            Case "ChangeContract"
                SelectionSurgical = False
                itemNoSurgical = Nothing
                itemNoSurgical = ViewNoSurgical.GetFocusedRow()
                OpenSelectContract(itemNoSurgical.PerformsHealthProfessionalCode, itemNoSurgical.MedicalFeesContractId, itemNoSurgical.ThirdPartyDescription)
            Case "DeleteCausation"
                SelectionSurgical = False
                itemNoSurgical = Nothing
                itemNoSurgical = ViewNoSurgical.GetFocusedRow()
                Await RemoveCausation()
            Case "ChangeHealthProfessional"
                SelectionSurgical = False
                OpenChangeHealthProfessional()
            Case "CausedValue"
                SelectionSurgical = False
                Await CauseMassively(Nothing, Nothing, False)
            Case "SelectPercentage"
                itemNoSurgical = Nothing
                itemNoSurgical = ViewNoSurgical.GetFocusedRow()
                SelectionSurgical = False
                OpenSelectPercentage()
        End Select
    End Sub

#End Region

#Region "IndigoGridView2 Surgical"

    Private Async Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "AddFees"
                SelectionSurgical = True
                itemXpoSurgical = Nothing
                itemXpoSurgical = ViewSurgicalAndPackage.GetFocusedRow()
                itemNoSurgical = Nothing
                OpenAddFees()
            Case "Remove"
                SelectionSurgical = True
                Await RemoveFeeds()
            Case "ChangeContract"
                SelectionSurgical = True
                itemXpoSurgical = Nothing
                itemXpoSurgical = ViewSurgicalAndPackage.GetFocusedRow()
                OpenSelectContract(itemXpoSurgical.PerformsHealthProfessionalCode, itemXpoSurgical.MedicalFeesContractId, itemXpoSurgical.ThirdPartyDescription)
            Case "DeleteCausation"
                SelectionSurgical = True
                itemXpoSurgical = Nothing
                itemXpoSurgical = ViewSurgicalAndPackage.GetFocusedRow()
                Await RemoveCausation()
            Case "ChangeHealthProfessional"
                SelectionSurgical = True
                OpenChangeHealthProfessional()
            Case "CausedValue"
                SelectionSurgical = True
                Await CauseMassively(Nothing, Nothing, False)
            Case "SelectPercentage"
                itemXpoSurgical = Nothing
                itemXpoSurgical = ViewSurgicalAndPackage.GetFocusedRow()
                SelectionSurgical = True
                OpenSelectPercentage()
        End Select
    End Sub

#End Region

#Region "IndigoGridView3 Notes"

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditNote()
            Case "Remove"
                DeleteNote()
        End Select
    End Sub

    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditNote()
            Case "Remove"
                DeleteNote()
        End Select
    End Sub

#End Region

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddNote_Click(sender As Object, e As EventArgs) Handles INDbtnAddNote.Click
        AddNote()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar click o f4 sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceNotes_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceNotes.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceNotes.ShowPopup()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepSleHealthProfessionalGridNoSurgical_Popup(sender As Object, e As EventArgs) Handles INDrepSleHealthProfessionalGridNoSurgical.Popup
        RefreshQueryPopup(sender, ViewNoSurgical)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepSleHealthProfessionalSurgical_Popup(sender As Object, e As EventArgs) Handles INDrepSleHealthProfessionalSurgical.Popup
        RefreshQueryPopup(sender, ViewSurgicalAndPackage)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceNotes_Popup(sender As Object, e As EventArgs) Handles INDpceNotes.Popup
        INDmemoNotes.Focus()
    End Sub

#End Region

#Region "InvalidRowException"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ViewNoSurgical_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles ViewNoSurgical.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ViewSurgicalAndPackage_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles ViewSurgicalAndPackage.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el repositorio de la rejilla
    ''' profesionales de la salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepSleHealthProfessionalGridNoSurgical_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepSleHealthProfessionalGridNoSurgical.QueryPopUp
        'RefreshQueryPopup(sender, ViewNoSurgical)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el repositorio de la rejilla
    ''' profesionales de la salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepSleHealthProfessionalSurgical_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepSleHealthProfessionalSurgical.QueryPopUp
        'RefreshQueryPopup(sender, ViewSurgicalAndPackage)
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que dispara el reporte de la factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInvoice_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleInvoice.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            SeeReportInvoice()
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
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)

        Dim _presenter = New PMedicalFeesCausation(Me)
        Dim ListKeys As List(Of Integer) = _presenter.Permission(BarraBotones.PermissionsForm)
        ContainsPermissionAddFees = ListKeys(0)
        ContainsPermissionManualCausation = ListKeys(1)
        ContainsPermissionChangeHealthProfessional = ListKeys(2)
        ContainsPermissionDelete = ListKeys(3)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        banSaveAndModify = False
        Guardar()
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
        banSaveAndModify = True
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.ContractSequenceDetail IsNot Nothing Then
        '    If Me._sequense.ContractSequenceDetail.Any(Function(S) S.OperatingUnitId = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.ContractSequenceDetail.Where(Function(s) s.OperatingUnitId = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacerTodo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        DeshacerTodo()
    End Sub

#End Region

End Class