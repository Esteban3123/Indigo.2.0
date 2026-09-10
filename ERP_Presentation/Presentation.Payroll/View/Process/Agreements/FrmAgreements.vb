'***********************************************************************
' Assembly         : Presentacion.Payrol
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"

Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Presentation.Portfolio.MVP

#End Region

Public Class FrmAgreements
    Implements IAgreements


#Region "Variable Globales Propiedades Interfaz y Load"

    ''' <summary>
    ''' variable para saber el modo de busqueda del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModoBusqueda As Boolean = False

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' variable que define el numero de factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim InvoiceNumber As String

    ''' <summary>
    ''' Obtiene o asigna una lista de compañias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListCompany As List(Of Domain.Payroll.Entities.Company) Implements IAgreements.ListCompany
        Get
            Return CType(Me.INDgleCompany.Properties.DataSource, List(Of Domain.Payroll.Entities.Company))
        End Get
        Set(value As List(Of Domain.Payroll.Entities.Company))
            Me.INDgleCompany.Properties.DataSource = value.Where(Function(x) x.AgreementType = True).ToList()
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o asigna lista de conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListConcept As List(Of Concept) Implements IAgreements.ListConcept
        Get
            Return CType(Me.INDgleConcept.Properties.DataSource, List(Of Concept))
        End Get
        Set(value As List(Of Concept))
            Me.INDgleConcept.Properties.DataSource = value.Where(Function(x) x.ConceptClass = "041").ToList()
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna lista de empleados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public WriteOnly Property ListEmployee As Object Implements IAgreements.ListEmployee
        Set(value As Object)
            Me.INDgleEmployee.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna lista de clases de convenios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListKindsAgreements As List(Of Domain.Payroll.Entities.KindsAgreements) Implements IAgreements.ListKindsAgreements
        Get
            Return CType(Me.INDgleKindsAgreements.Properties.DataSource, List(Of Domain.Payroll.Entities.KindsAgreements))
        End Get
        Set(value As List(Of Domain.Payroll.Entities.KindsAgreements))
            Me.INDgleKindsAgreements.Properties.DataSource = value
        End Set
    End Property

    Public Property ListAccountReceivableAccountingXPO As XPInstantFeedbackSource Implements IAgreements.ListAccountReceivableAccountingXPO
        Get
            Return CType(INDSleAccountReceivableAccounting.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDSleAccountReceivableAccounting.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que determina el estado de la clases de convenio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatusAgreements As Integer Implements IAgreements.StatusAgreements
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Integer)
            Me.BarraBotones.StatusRecord = value

            If value = EnumStateAgreements.eUnconfirmed Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = True
                Me.BarraBotones.RibbonPageProcesos.Visible = True

                INDlcgGeneralInfo.Enabled = True
                INDlcgAgreements.Enabled = True
                INDlcgLiquidationPayroll.Enabled = True
                INdlcgContractualInformation.Enabled = False
                LayoutControlGroup2.Enabled = True
                'INDRgVacationPaid.Enabled = True
            ElseIf value = EnumStateAgreements.eConfirmed Then

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                Me.BarraBotones.RibbonPageProcesos.Visible = True

                INDlcgGeneralInfo.Enabled = False
                INdlcgContractualInformation.Enabled = False
                LayoutControlGroup2.Enabled = False
                INDlcgAgreements.Enabled = False
                INDgcAgreementsD.Enabled = False
                INDlcgLiquidationPayroll.Enabled = False
                'INDRgVacationPaid.Enabled = True
            ElseIf value = EnumStateAgreements.einvalidated Or value = EnumStateAgreements.eFinished Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                Me.BarraBotones.RibbonPageProcesos.Visible = False

                INDlcgGeneralInfo.Enabled = False
                INdlcgContractualInformation.Enabled = False
                LayoutControlGroup2.Enabled = False
                INDgcAgreementsD.Enabled = False
                INDlcgAgreements.Enabled = False
                INDlcgLiquidationPayroll.Enabled = False
                'INDRgVacationPaid.Enabled = False
            ElseIf value = EnumStateAgreements.eSuspended Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True 'desactivado
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = False 'activado
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                Me.BarraBotones.RibbonPageProcesos.Visible = True
                INDlcgGeneralInfo.Enabled = False
                INdlcgContractualInformation.Enabled = False
                LayoutControlGroup2.Enabled = False
                INDgcAgreementsD.Enabled = False
                INDlcgAgreements.Enabled = False
                INDlcgLiquidationPayroll.Enabled = False
                ' INDRgVacationPaid.Enabled = False
            End If

        End Set
    End Property
    ''' <summary>
    ''' Propiedad que almacena el valor del convenio para sus respectivos calculos en la vista
    ''' </summary>
    ''' <returns></returns>
    Public Property AgreementValue As Decimal Implements IAgreements.AgreementValue
        Get
            Return CDec(INDtxtAgreementValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtAgreementValue.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que almacena el valor de cada liquidación en la vista
    ''' </summary>
    ''' <returns></returns>
    Public Property ShareValuePaid As Decimal Implements IAgreements.ShareValuePaid
        Get
            Return CDec(INDtxtShareValuePaid.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtShareValuePaid.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para controlar  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceAgreementsD As List(Of AgreementsD)
        Get
            Return CType(Me.INDgcAgreementsD.DataSource, List(Of AgreementsD))
        End Get
        Set(value As List(Of AgreementsD))
            Me.INDgcAgreementsD.DataSource = value
            Me.INDgcAgreementsD.RefreshDataSource()
        End Set
    End Property
    ''' <summary>
    ''' Propiedad de lectura que me permite obtener las cuotas pagadas.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CountAgreementsD As Integer
        Get
            Return DataSourceAgreementsD.Count
        End Get
    End Property
    ''' <summary>
    ''' DataTable
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene un convenio
    ''' </summary>
    Dim AgreementsC As AgreementsC
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MAgreements
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PAgreements
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Objeto Empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ObjEmployee As Employee
    ''' <summary>
    ''' Id Grupo de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim Idgroup As Integer

    Dim ThirdPartyId As Integer

    Dim InvoiceBalance As Decimal

    ''' <summary>
    ''' propiedad que contiene el afecta cuenta por cobrar
    ''' </summary>
    Property AffectsAccountsReceivable As Boolean


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        AgreementsC = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        ObjEmployee = Nothing
        Idgroup = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' Funcion de carga inicial del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAgreements_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MAgreements(Me.Tag)
        Me.indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        INDLciTransferType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDGleTransferType.Properties.DataSource = ListTransferType
        INDLciAccountReceivableAccounting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Presenter = New PAgreements(Me)
        Deshacer()
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        'Me.ActionsOnControls = False
        Presenter.LoadDataAsync()
        Presenter.LoadKindsAgreements()
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        BarraBotones.RibbonPageProcesos.Visible = False
        Me.INDdeDatePayment.EditValue = Date.Now
    End Sub

#End Region

#Region "ICRUD"
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If

        Dim listItemsColumnEdit As New List(Of Tuple(Of String, String))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Sin Confirmar", "1"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Confirmado", "2"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Suspendido", "3"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Terminado", "4"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Anulado", "5"))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "Consecutive"},
                              New ColumnInfo With {.Caption = "Nit", .FieldName = "EmployeeId.ThirdPartyId.Nit"},
                              New ColumnInfo With {.Caption = "Empleado", .FieldName = "EmployeeId.ThirdPartyId.Name"},
                              New ColumnInfo With {.Caption = "Compañia", .FieldName = "CompanyId.Descripcion"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "State", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEdit}}.ToList
            '.ValorSolicitado = "Consecutive"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAgreements
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
        ModoBusqueda = True
    End Sub


    Dim _listTransferType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListTransferType As List(Of Tuple(Of Byte, String))
        Get
            If _listTransferType Is Nothing Then
                _listTransferType = New List(Of Tuple(Of Byte, String))
                _listTransferType.Add(New Tuple(Of Byte, String)(1, "Mismo Cliente"))
                _listTransferType.Add(New Tuple(Of Byte, String)(2, "Diferente Cliente"))
            End If
            Return _listTransferType
        End Get
    End Property

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBteConsecutive.Text = ReturnValue
        If INDBteConsecutive.Text <> String.Empty Then
            LoadControls()
            If INDBteConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteConsecutive.Enabled = False
        End If
    End Sub


    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Try
            AssigningValues()
            AsyncLoader(True)
            Using modelSave As New MAgreements(MyBase.Tag)
                Dim result = Await modelSave.SaveAgreementsC(AgreementsC)
                AsyncLoader(False)
                If result.StateResult = True Then
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Mensaje(EeventViewerImages.Informacion) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    End If
                    Me.AgreementsC = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    ModoBusqueda = False
                    Deshacer()
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    INDBteConsecutive.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

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

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IAgreements.ActionsOnControls
        Set(value As Boolean)
            INDlyAgreements.BeginUpdate()

            INDBteConsecutive.Enabled = Not value
            INDgleEmployee.Enabled = value
            INDgleCompany.Enabled = value
            INDgleKindsAgreements.Enabled = value
            INDmeComment.Enabled = value

            INDrgLiquidationType.Enabled = value
            INDrgTermtype.Enabled = value
            If INDrgLiquidationType.EditValue = 2 Then
                INDtxtNumberShares.Enabled = Not value
                INDtxtQuoteValue.Enabled = Not value
            Else
                INDtxtQuoteValue.Enabled = value
                INDtxtNumberShares.Enabled = value
            End If
            INDtxtAgreementValue.Enabled = value
            INDgleConcept.Enabled = value
            INDdeStartingDate.Enabled = value

            INDRgVacationPaid.Enabled = value
            INDtxtShareValuePaid.Enabled = value
            INDdeDatePayment.Enabled = value
            INDmeCommentsD.Enabled = value

            LayoutControlGroup2.Enabled = value

            INDlyAgreements.EndUpdate()
            If value = True Then
                INDgleEmployee.Focus()
            Else
                INDBteConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IAgreements.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub


#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmFunctionalAgreementsMetaData, Eform.InfoMetaData), Me.AgreementsC.Consecutive, Me.INDgleEmployee.Text, Me.INDgleCompany.Text, Me.INDgleKindsAgreements.Text),
                                                .CreationDate = dateServer,
                                                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName,
                                                .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me.AgreementsC.Consecutive & "#$",
                                                .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmFunctionalAgreementsMetaDataTitle, Eform.InfoMetaData), Me.AgreementsC.Consecutive),
                                                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmKindsAgrementsMetaData, Eform.InfoMetaData), Me.AgreementsC.Consecutive, Me.INDgleEmployee.Text, Me.INDgleCompany.Text, Me.INDgleKindsAgreements.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFunctionalAgreementsMetaDataTitle, Eform.InfoMetaData), Me.AgreementsC.Consecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        AsyncLoader(True)
        Me.BarraBotones.StatusRecordVisible = True
        AgreementsC = Await Model.GetAgreementsC(Me.INDBteConsecutive.Text)
        If AgreementsC Is Nothing Then
            Me.AgreementsC = New AgreementsC()
            Me.LogicaBotonActualizar(False)
            AsyncLoader(False)
        Else
            If AgreementsC.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, AgreementsC.Id)
                With AgreementsC
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    INDBteConsecutive.Text = .Consecutive
                    INDgleEmployee.EditValue = .EmployeeId
                    DataSourceAgreementsD = .AgreementsD.ToList()
                    INDgleCompany.EditValue = .CompanyId
                    INDgleKindsAgreements.EditValue = .KindsAgreementsId
                    AffectsAccountsReceivable = .KindsAgreements.AffectsAccountsReceivable
                    INDSleAccountReceivableAccounting.EditValue = .AccountReceivableAccountingId
                    INDSleAccountReceivableAccounting.Properties.NullText = .InvoiceNumber
                    INDmeComment.Text = .Comments
                    INDrgLiquidationType.EditValue = CType(.LiquidationType, Int16)
                    INDrgTermtype.EditValue = CType(.TermType, Int16)
                    AgreementValue = .AgreementValue
                    INDtxtNumberShares.Text = .NumberShares
                    INDgleConcept.EditValue = .ConceptId
                    INDdeStartingDate.EditValue = .StartingDate
                    INDtxtCurrentBalance.EditValue = .CurrentBalance
                    INDtxtNumberDuesPaid.EditValue = CountAgreementsD
                    StatusAgreements = .State
                    INDRgVacationPaid.EditValue = .PaidVacation
                    INDGleTransferType.EditValue = If(.TransferType, CInt(.TransferType), Nothing)
                    CalculateShare()
                    If INDrgTermtype.EditValue = 2 And INDrgLiquidationType.EditValue = 2 Or INDrgTermtype.EditValue = 2 And INDrgLiquidationType.EditValue = 1 Then
                        INDtxtCurrentBalance.EditValue = 0
                    End If

                End With
                AsyncLoader(False)
                ActionsOnControls = True
                INDLciTransferType.Visibility = If(AffectsAccountsReceivable, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.AgreementsC.Consecutive)

                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(AgreementsC.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = AgreementsC.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    If Not (Me.record IsNot Nothing AndAlso Me.record.CodUser = Me.indigo.UserIndigo AndAlso Me.record.IdRecord = Me.AgreementsC.Id) Then
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                End If
            Else
                If INDBteConsecutive.Text = String.Empty Then
                    INDBteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Else
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesNoHayRegistros, Eform.Comunes)
                    INDBteConsecutive.Text = String.Empty
                    ActionsOnControls = False
                End If
                AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyAgreements.BeginUpdate()

        Await DeleteBlockedRecord()
        Me.AgreementsC = Nothing
        Me.ObjEmployee = Nothing
        INDlcgGeneralInfo.Enabled = True
        INdlcgContractualInformation.Enabled = False
        INDlcgAgreements.Enabled = False
        INDlcgLiquidationPayroll.Enabled = False

        Me.INDBteConsecutive.Text = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        INDgleEmployee.EditValue = Nothing
        INDgleCompany.EditValue = Nothing
        INDgleCompany.Properties.NullText = String.Empty
        INDgleKindsAgreements.EditValue = Nothing
        INDgleKindsAgreements.Properties.NullText = String.Empty
        INDmeComment.Text = String.Empty

        INDtxtContract.Text = String.Empty
        INDtxtEmployeeCompany.Text = String.Empty
        INDtxtBusinessUnit.Text = String.Empty
        INDtxtFunctionalUnit.Text = String.Empty
        INDtxtCharges.Text = String.Empty
        INDtxtDateContract.Text = String.Empty
        INDtxtBasicSalary.Text = String.Empty
        Idgroup = Nothing

        INDrgLiquidationType.EditValue = Nothing
        INDrgTermtype.EditValue = Nothing
        AgreementValue = 0
        ShareValuePaid = 0
        INDtxtNumberShares.Text = String.Empty
        INDgleConcept.EditValue = Nothing
        INDgleConcept.Properties.NullText = String.Empty
        INDdeStartingDate.EditValue = Nothing
        INDtxtQuoteValue.Text = String.Empty

        INDRgVacationPaid.SelectedIndex = 1
        INDtxtCurrentBalance.Text = String.Empty
        INDtxtNumberDuesPaid.Text = String.Empty
        INDgcAgreementsD.DataSource = New List(Of AgreementsD)

        INDGleTransferType.EditValue = Nothing
        INDSleAccountReceivableAccounting.EditValue = Nothing
        INDSleAccountReceivableAccounting.Properties.NullText = String.Empty
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        BarraBotones.RibbonPageProcesos.Visible = False
        INDlyAgreements.EndUpdate()
        Me.ActionsOnControls = False
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        'ValidateControls = True
        If INDBteConsecutive.EditValue = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiConsecutive.Text)
            Return False
        End If
        If INDgleEmployee.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiEmployee.Text)
            Return False
        End If
        If INDgleCompany.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiCompany.Text)
            Return False
        End If
        If INDgleKindsAgreements.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiKindsAgreements.Text)
            Return False
        End If

        If (Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eFixed And Me.INDrgTermtype.EditValue = EnumTermType.eFixed) Or (Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eVariable And Me.INDrgTermtype.EditValue = EnumTermType.eFixed) Then
            If INDtxtNumberShares.Text = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiNumberShares.Text)
                Return False
            End If
        End If

        If INDrgLiquidationType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlycLiquidationType.Text)
            Return False
        End If
        If INDrgTermtype.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiTermtype.Text)
            Return False
        End If
        If INDtxtAgreementValue.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiAgreementValue.Text)
            Return False
        End If
        If INDgleConcept.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiConcept.Text)
            Return False
        End If
        If INDdeStartingDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiStartingDate.Text)
            Return False
        End If

        If AgreementValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("El Valor Covenio no debe ser menor a 0")
            Return False
        End If
        If INDSleAccountReceivableAccounting.EditValue Is Nothing And AffectsAccountsReceivable Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciAccountReceivableAccounting.Text)
            Return False
        End If

        If AgreementValue > CDec(InvoiceBalance) And AffectsAccountsReceivable Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("El Valor Covenio no debe ser mayor al saldo de la factura")
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With AgreementsC
            Dim ObjConcept = CType(INDgleConcept.GetSelectedDataRow, Concept)
            .GroupId = Idgroup
            .EmployeeId = INDgleEmployee.EditValue
            .CompanyId = INDgleCompany.EditValue
            .ConceptId = INDgleConcept.EditValue
            .KindsAgreementsId = INDgleKindsAgreements.EditValue
            .Comments = INDmeComment.Text
            .LiquidationType = INDrgLiquidationType.EditValue
            .TermType = INDrgTermtype.EditValue
            .AgreementValue = AgreementValue
            .PaidVacation = INDRgVacationPaid.EditValue
            .TransferType = CByte(INDGleTransferType.EditValue)
            .AccountReceivableAccountingId = INDSleAccountReceivableAccounting.EditValue
            '.OperativeUnit = _idOperativeUnit 'asignar unidad operativa, para trabajar con el sp
            If (Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eFixed And Me.INDrgTermtype.EditValue = EnumTermType.eFixed) Or (Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eVariable And Me.INDrgTermtype.EditValue = EnumTermType.eFixed) Then
                .NumberShares = INDtxtNumberShares.Text
            Else
                .NumberShares = 0
            End If

            .State = Me.BarraBotones.StatusRecord
            .StartingDate = INDdeStartingDate.EditValue

            If AgreementsC.ChangeTracker.State = ObjectState.Added Then
				.CurrentBalance = INDtxtAgreementValue.EditValue
			End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Function


    Private Sub INDBteConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDBteConsecutive.Text.ToString) Then
                LoadControls()
                If INDBteConsecutive.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                'INDBteConsecutive.Enabled = False
            Else
                INDBteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                ActionsOnControls = True

                AgreementsC = New AgreementsC()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Sub INDSleAccountReceivableAccounting_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccountReceivableAccounting.QueryPopUp
        If INDGleTransferType.EditValue = 1 And ThirdPartyId <> 0 Then
            Presenter.InitializeAccountReceivableAccountingXPO(ThirdPartyId)
        Else
            Presenter.InitializeAccountReceivableAccountingXPO(0)
        End If
    End Sub

    Private Sub INDBteConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteConsecutive.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmAgreements_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = 1, .StatusName = "Sin confirmar", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(122, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 2, .StatusName = "Confirmado", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 3, .StatusName = "Suspendido", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 4, .StatusName = "Terminado", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 5, .StatusName = "Anulado", .StatusColor = System.Drawing.Color.OrangeRed})
        Me.BarraBotones.States = listStates
        Me.BarraBotones.StatusRecordEnabled = False
        ' Me.BarraBotones.Enabled = False
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    ''' Cargar datos del Empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadEmployeeData()
        Dim ObjContract As New Contract
        AsyncLoader(True)
        If Me.INDgleEmployee.EditValue IsNot Nothing Then
            ObjEmployee = Await Model.GetEmployee(Me.INDgleEmployee.EditValue)
            If ObjEmployee.Id > 0 Then
                With ObjEmployee
                    ThirdPartyId = ObjEmployee.ThirdParty.Id
                    If .Contract IsNot Nothing AndAlso .Contract.Count > 0 Then

                        If .Contract.Any(Function(item) item.Valid = True) Then
                            ObjContract = .Contract.Where(Function(item) item.Valid = True).FirstOrDefault()
                        Else
                            Dim MaxId = .Contract.Max(Function(x) x.Id)
                            ObjContract = .Contract.Where(Function(item) item.Id = MaxId).FirstOrDefault()
                        End If

                        Idgroup = ObjContract.GroupId

                        If ObjContract IsNot Nothing Then
                            INDtxtContract.Text = ObjContract.Id.ToString
                            If ObjContract.Group IsNot Nothing AndAlso ObjContract.Group.Company IsNot Nothing Then
                                INDtxtEmployeeCompany.Text = ObjContract.Group.Company.Name
                            End If
                            If ObjContract.FunctionalUnit IsNot Nothing AndAlso ObjContract.FunctionalUnit.BranchOffice IsNot Nothing Then
                                INDtxtBusinessUnit.Text = ObjContract.FunctionalUnit.BranchOffice.Name
                                INDtxtFunctionalUnit.Text = ObjContract.FunctionalUnit.Name
                            End If
                            If ObjContract.Position IsNot Nothing Then
                                INDtxtCharges.Text = ObjContract.Position.Name
                            End If
                            INDtxtDateContract.Text = ObjContract.ContractInitialDate
                            INDtxtBasicSalary.Text = ObjContract.BasicSalary.ToString
                        End If

                    End If
                End With
            End If
        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Cargar datos de la clase de convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadKindsAgreementsAsync()
        AsyncLoader(True)
        If Me.INDgleKindsAgreements.EditValue IsNot Nothing Then
            If Me.INDgleEmployee.EditValue Is Nothing Then
                AsyncLoader(False)
                INDgleKindsAgreements.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un empleado"
                Exit Sub
            End If

            Await Presenter.LoadKindsAgreements()
            Dim kindsAgreement = (From item In ListKindsAgreements Where item.Id = INDgleKindsAgreements.EditValue Select item).FirstOrDefault()
            If kindsAgreement IsNot Nothing Then
                INDlcgAgreements.Enabled = True
                AffectsAccountsReceivable = kindsAgreement.AffectsAccountsReceivable
                If AffectsAccountsReceivable Then
                    INDLciAccountReceivableAccounting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGleTransferType.EditValue = 2
                    INDLciTransferType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDrgLiquidationType.Properties.ReadOnly = True
                    INDrgLiquidationType.EditValue = CType(2, Int16)
                End If
            End If
        End If
        AsyncLoader(False)
    End Sub

    Private Sub LoadBills()
        AsyncLoader(True)
        If INDSleAccountReceivableAccounting.EditValue IsNot Nothing Then
            Using model As New MNotesDebitCreditPortfolio(Me.Tag)
                Dim accountReceivableAccounting = model.GetPortfolioAccountReceivableAccountingById(INDSleAccountReceivableAccounting.EditValue)
                AgreementValue = accountReceivableAccounting.Balance
                InvoiceBalance = AgreementValue
            End Using
        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento que consulta Info. Empleado una ves seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleEmployee.EditValueChanged
        If AgreementsC IsNot Nothing Then
            If Me.INDgleEmployee.EditValue IsNot Nothing Then
                INDLciAccountReceivableAccounting.Enabled = True
                INDLciTransferType.Enabled = True
                LoadEmployeeData()
            Else
                INDLciAccountReceivableAccounting.Enabled = False
                INDLciTransferType.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que consulta Info. factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountReceivableAccounting_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountReceivableAccounting.EditValueChanged
        If AgreementsC IsNot Nothing Then
            If Me.INDSleAccountReceivableAccounting.EditValue IsNot Nothing Then
                LoadBills()
            End If
        End If
    End Sub


    ''' <summary>
    ''' Control de bloqueo del numero de cuotas de acuerdo al tipo de liquidacion y plazo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BlockNumberShares()
        If Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eFixed And Me.INDrgTermtype.EditValue = EnumTermType.eFixed Then
            Me.INDtxtNumberShares.Enabled = True
            Me.INDtxtQuoteValue.Enabled = True
        ElseIf Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eVariable And Me.INDrgTermtype.EditValue = EnumTermType.eVariable Then
            Me.INDtxtNumberShares.Enabled = False
            INDtxtQuoteValue.Enabled = False
            Me.INDtxtNumberShares.Text = String.Empty
        ElseIf Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eFixed And Me.INDrgTermtype.EditValue = EnumTermType.eVariable Then
            Me.INDtxtNumberShares.Enabled = False
            INDtxtQuoteValue.Enabled = False
            Me.INDtxtNumberShares.Text = String.Empty
        ElseIf Me.INDrgLiquidationType.EditValue = EnumLiquidationType.eVariable And Me.INDrgTermtype.EditValue = EnumTermType.eFixed Then
            Me.INDtxtNumberShares.Enabled = True
            INDtxtQuoteValue.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo para calcular el valor total del numero de cuotas y el valor de la cuota
    ''' </summary>
    ''' <remarks></remarks>
    Sub CalculateTotal()
        If INDtxtNumberShares.EditValue.ToString() <> "" AndAlso INDtxtQuoteValue.EditValue.ToString() <> "" Then
			AgreementValue = CDec(INDtxtNumberShares.Text) * CDec(INDtxtQuoteValue.EditValue)
		End If
    End Sub

    Sub CalculateShare()
        If AgreementValue > 0 AndAlso INDtxtNumberShares.EditValue.ToString() <> "" Then
            If CDec(INDtxtNumberShares.EditValue) > 0 Then
                INDtxtQuoteValue.EditValue = AgreementValue / CDec(INDtxtNumberShares.EditValue)
            ElseIf CDec(INDtxtNumberShares.EditValue) = 0 Then
                INDtxtQuoteValue.EditValue = AgreementValue
            End If
        End If
    End Sub
    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
    End Sub

#End Region

#Region "Eventos Barra Botones"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
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
        Me.Deshacer()
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
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub
    ''' <summary>
    ''' Confirmar un convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If AgreementsC IsNot Nothing Then
            If MessageIndigo.Show(obtenerRecurso(ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                AgreementsC.State = EnumStateAgreements.eConfirmed
                AsyncLoader(True)
                AgreementsC.OperativeUnit = _idOperativeUnit 'utilización de unidad operativa para otro proceso en sp
                Dim result As ActionResult(Of AgreementsC) = Await Model.SaveAgreementsC(AgreementsC)
                If result.StateResult = True Then
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Mensaje(EeventViewerImages.Informacion) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    End If
                    CleanControls()
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End If
                AsyncLoader(False)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Anualr un convenio
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If AgreementsC IsNot Nothing Then
            Using _FrmCommentChangeState As New FrmCommentChangeState
                AgreementsC.State = EnumStateAgreements.einvalidated
                _FrmCommentChangeState.State = EnumStateAgreements.einvalidated
                _FrmCommentChangeState.AgreementsC = AgreementsC
                Dim transparent As New FrmTransparent(_FrmCommentChangeState, False)
                transparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                transparent.ShowDialog()
                LoadControls()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Suspender un contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender
        If AgreementsC IsNot Nothing Then
            Using _FrmCommentChangeState As New FrmCommentChangeState
                AgreementsC.State = EnumStateAgreements.eSuspended
                _FrmCommentChangeState.State = EnumStateAgreements.eSuspended
                _FrmCommentChangeState.AgreementsC = AgreementsC
                Dim transparent As New FrmTransparent(_FrmCommentChangeState, False)
                transparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                transparent.ShowDialog()
                LoadControls()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Click boton reactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickReactivar() Handles BarraBotones.ClickReactivar
        If AgreementsC IsNot Nothing Then
            AgreementsC.OperativeUnit = _idOperativeUnit 'utilización de unidad operativa para otro proceso en sp                            
            Using _FrmCommentChangeState As New FrmCommentChangeState
                AgreementsC.State = EnumStateAgreements.eConfirmed
                _FrmCommentChangeState.State = EnumStateAgreements.eConfirmed
                _FrmCommentChangeState.AgreementsC = AgreementsC
                Dim transparent As New FrmTransparent(_FrmCommentChangeState, False)
                transparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                transparent.ShowDialog()
                LoadControls()
            End Using
        End If
    End Sub

#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyAgreements.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyAgreements.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyAgreements.ShowCustomization
        Try
            'Ejecuatamos la consulta
            AsyncLoader(True)
            Dim dsFields As DataSet = Await Model.GetFieldsNULL
            AsyncLoader(False)
            If dsFields IsNot Nothing Then
                dtFieldsCustomizables = dsFields.Tables(0)
                For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                    For j As Integer = 0 To INDlyAgreements.Items.Count - 1
                        If Object.Equals(INDlyAgreements.Items.Item(j).Tag, Nothing) = False Then
                            If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyAgreements.Items.Item(j).Tag.ToString.Trim Then
                                INDlyAgreements.Items.Item(j).AllowHide = True
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyAgreements.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyAgreements.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyAgreements.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyAgreements.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "Handlers"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.AgreementsC IsNot Nothing AndAlso Me.AgreementsC.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.INDBteConsecutive.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteConsecutive.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Control de cambio del tipo de liquidacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgLiquidationType.EditValueChanged
        BlockNumberShares()
    End Sub
    ''' <summary>
    ''' Control de cambio de tipo de plazos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgTermtype_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgTermtype.EditValueChanged
        BlockNumberShares()
    End Sub

    ''' <summary>
    ''' Agregar un nuevo detalle de convenio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAgreementsD_Click(sender As Object, e As EventArgs) Handles INDbtnAddAgreementsD.Click
        Dim ObjAgreementsD As New AgreementsD
        If ShareValuePaid > 0 And INDdeDatePayment.EditValue IsNot Nothing Then
            If ShareValuePaid > INDtxtCurrentBalance.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El Valor del Pago no debe ser mayor al saldo actual")
            Else
                With ObjAgreementsD
                    .AgreementsCId = AgreementsC.Id
                    If INDtxtShareValuePaid.Text <> String.Empty Then
                        .ShareValuePaid = ShareValuePaid
                    End If
                    .DatePayment = INDdeDatePayment.EditValue
                    .TypePayment = 2
                    .StateShare = INDmeCommentsD.Text
                End With

                If ValidateCurrentBalance(ObjAgreementsD.ShareValuePaid) = False Then
                    'Mensaje(EeventViewerImages.MensajeError) = String.Format(obtenerRecurso(PaidValueExceeds, Eform.Agreements), Me.INDtxtCurrentBalance.Text)   '"El Valor pagado supera el saldo actual: " & Me.AgreementsC.AgreementValue.ToString() 
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(PaidValueExceeds, Eform.Agreements), Me.INDtxtCurrentBalance.Text)
                Else
                    DataSourceAgreementsD.Add(ObjAgreementsD)
                    AgreementsC.AgreementsD.Add(ObjAgreementsD)
                    Me.INDgcAgreementsD.RefreshDataSource()
                    CalculateCurrentBalance()
                    INDtxtShareValuePaid.Focus()
                End If
                INDtxtShareValuePaid.Text = String.Empty
                INDdeDatePayment.EditValue = Nothing
                INDmeCommentsD.Text = String.Empty
                INDtxtNumberDuesPaid.EditValue = CountAgreementsD
            End If
        End If
    End Sub

    ''' <summary>
    ''' Eliminar un detalle de convenio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteDelete_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteDelete.ButtonClick
        Dim ObjAgreeTmp = CType(INDgvAgreementsD.GetRow(INDgvAgreementsD.FocusedRowHandle), AgreementsD)
        If ObjAgreeTmp IsNot Nothing Then
            AgreementsC.AgreementsD.ToList().ForEach(Sub(item)
                                                         If item.Id = ObjAgreeTmp.Id Then
                                                             AgreementsC.CurrentBalance = AgreementsC.CurrentBalance + item.ShareValuePaid
                                                             Me.INDtxtCurrentBalance.EditValue = AgreementsC.CurrentBalance
                                                             item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                                                         End If
                                                     End Sub)
            DataSourceAgreementsD.Remove(ObjAgreeTmp)
            INDgcAgreementsD.RefreshDataSource()
            INDtxtNumberDuesPaid.EditValue = CountAgreementsD
        End If
    End Sub

    ''' <summary>
    ''' Calcula el saldo actual del convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateCurrentBalance()
        Dim currentBalance As Decimal
        Decimal.TryParse(AgreementValue, currentBalance)
        If AgreementsC IsNot Nothing Then
            If DataSourceAgreementsD.Count > 0 Then
                For i = 0 To DataSourceAgreementsD.Count - 1
                    currentBalance = currentBalance - DataSourceAgreementsD(i).ShareValuePaid
                Next
            End If
            AgreementsC.CurrentBalance = currentBalance
        End If
        Me.INDtxtCurrentBalance.EditValue = currentBalance
    End Sub
    ''' <summary>
    ''' Metodo para validar que los pagos no superen el valor del saldo
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateCurrentBalance(ByVal ValorAdd As Decimal)
        Dim SumD As Decimal = 0
        If DataSourceAgreementsD.Count > 0 Then
            For i = 0 To DataSourceAgreementsD.Count - 1
                SumD = SumD + DataSourceAgreementsD(i).ShareValuePaid
            Next
            If SumD + ValorAdd > AgreementsC.AgreementValue Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Cambia el tipo de pago en la rejilla de convenios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvAgreementsD_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvAgreementsD.CustomColumnDisplayText
        If e.Column.FieldName = "TypePayment" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "1"
                        e.DisplayText = obtenerRecurso(TipoPorNomina, Eform.Agreements)
                    Case "2"
                        e.DisplayText = obtenerRecurso(TipoManual, Eform.Agreements)
                    Case "3"
                        e.DisplayText = obtenerRecurso(TipoPorArchivo, Eform.Agreements)
                    Case "4"
                        e.DisplayText = "Pago por Vacaciones"
                    Case "5"
                        e.DisplayText = "Pago por Liquidación de Contrato"
                    Case Else
                        e.DisplayText = obtenerRecurso(TiponoReconocido, RecepcionObjeciones)
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmEmployee With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.LoadDataAsync()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de empresas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleCompany_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleCompany.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmCompany With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.LoadKindsAgreements()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de clase de convenio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleKindsAgreements_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleKindsAgreements.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmKindsAgreements With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmConcepts With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Presenter.LoadDataAsync()
        End If
    End Sub

    Private Sub INDgleKindsAgreements_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleKindsAgreements.EditValueChanged
        INDLciAccountReceivableAccounting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSleAccountReceivableAccounting.EditValue = Nothing
        INDLciTransferType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDGleTransferType.EditValue = Nothing
        INDrgLiquidationType.Properties.ReadOnly = False

        If Me.INDgleKindsAgreements.EditValue And AgreementsC IsNot Nothing Then
            LoadKindsAgreementsAsync()
        End If
    End Sub

    Private Sub INDGleTransferType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTransferType.EditValueChanged
        If INDGleTransferType.EditValue Is Nothing Then
            INDLciAccountReceivableAccounting.Enabled = False
            INDLciTransferType.Enabled = False
            INDSleAccountReceivableAccounting.EditValue = Nothing
        Else
            INDLciTransferType.Enabled = True
            INDLciAccountReceivableAccounting.Enabled = True
            ListAccountReceivableAccountingXPO = Nothing
        End If
    End Sub

    Private Sub INDtxtAgreementValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtAgreementValue.EditValueChanged
        CalculateCurrentBalance()
    End Sub

    Private Sub INDtxtNumberShares_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtNumberShares.KeyUp
        CalculateTotal()
    End Sub

    Private Sub INDtxtQuoteValue_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtQuoteValue.KeyUp
        CalculateTotal()
    End Sub

#End Region

End Class