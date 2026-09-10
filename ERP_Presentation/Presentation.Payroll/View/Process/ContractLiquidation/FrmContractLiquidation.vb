Imports Presentation.Payroll.MVP
Imports Presentation.Payroll.MVP.PContractLiquidation
Imports Domain.Payroll.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Drawing
Imports System.Windows.Forms
Imports Presentation.CloudAgent

'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojasf
' Created          : 08-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Class FrmContractLiquidation
    Implements IContractLiquidation

#Region "Globals & Properties"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

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
    ''' Variable que contiene la representacion del tipo de empleado
    ''' </summary>
    'Dim EmployeeType As EmployeeType

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MContractLiquidation(Me.Tag)

    ''' <summary>
    ''' Registro actual del control de navegacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim RecordMessageLiquidation As ActionMessageResult(Of ContractLiquidation)

    Dim ListContractLiquidation As List(Of ActionMessageResult(Of ContractLiquidation))

    Dim ContractEmployee As Contract

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private _presenter As PContractLiquidation
    Private ReadOnly Property Presenter As PContractLiquidation
        Get
            If _presenter Is Nothing Then
                _presenter = New PContractLiquidation(Me)
            End If
            Return _presenter
        End Get
    End Property

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    Public Property Employee As Domain.Payroll.Entities.Employee Implements IContractLiquidation.Employee

    ''' <summary>
    ''' Propiedad que contiene el listado de empleados que se usa en la rejilla
    ''' </summary>
    Private _employeeList As List(Of Domain.Payroll.Entities.Employee)
    Public Property EmployeeList As List(Of Domain.Payroll.Entities.Employee) Implements IContractLiquidation.EmployeeList
        Get
            If _employeeList Is Nothing Then
                _employeeList = New List(Of Domain.Payroll.Entities.Employee)
            End If

            Return _employeeList
        End Get
        Set(value As List(Of Domain.Payroll.Entities.Employee))
            _employeeList = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de empleados
    ''' </summary>
    Public Property HumanTalentList As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IContractLiquidation.HumanTalentList
        Get
            Return INDsleEmployees.Properties.DataSource
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleEmployees.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la lista de razones de retiro
    ''' </summary>
    Public Property RetirementReasonList As List(Of RetirementReason) Implements IContractLiquidation.RetirementReasonList
        Get
            Return INDrepRetirementReason.DataSource
        End Get
        Set(value As List(Of RetirementReason))
            INDrepRetirementReason.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Lista de liquidaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationList As List(Of Liquidation)
    Private Property LiquidationList As List(Of Liquidation)
        Get
            If _liquidationList Is Nothing Then
                _liquidationList = New List(Of Liquidation)
            End If

            Return _liquidationList
        End Get
        Set(value As List(Of Liquidation))
            _liquidationList = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de Id's de empleados a liquidar con la fecha de retiro y si se confirma o no la liquidacion, el primero parametro hace referencia al id del empleado, 
    ''' y la tupla tiene si se confirma o no, la fecha de retiro y id del motivo de retiro
    ''' </summary>
    Private _employeesToLiquidate As Dictionary(Of Integer, Tuple(Of Date, Integer))
    Public Property EmployeesToLiquidate() As Dictionary(Of Integer, Tuple(Of Date, Integer))
        Get

            If _employeesToLiquidate Is Nothing Then
                _employeesToLiquidate = New Dictionary(Of Integer, Tuple(Of Date, Integer))
            End If

            Return _employeesToLiquidate
        End Get
        Set(ByVal value As Dictionary(Of Integer, Tuple(Of Date, Integer)))
            _employeesToLiquidate = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de restricciones de fecha por empleado
    ''' </summary>
    Private _employeesRetirementDateRestrictions As Dictionary(Of Integer, Dictionary(Of retirementDates, Date))
    Private Property EmployeesRetirementDateRestrictions As Dictionary(Of Integer, Dictionary(Of retirementDates, Date))
        Get
            If _employeesRetirementDateRestrictions Is Nothing Then
                _employeesRetirementDateRestrictions = New Dictionary(Of Integer, Dictionary(Of retirementDates, Date))
            End If
            Return _employeesRetirementDateRestrictions
        End Get
        Set(value As Dictionary(Of Integer, Dictionary(Of retirementDates, Date)))
            _employeesRetirementDateRestrictions = value
        End Set
    End Property

#End Region

#Region "ICRUD"


    ''' <summary>
    ''' METODO: item buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta al abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CtrNavigation1_ClickBack()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If EmployeeType IsNot Nothing Then
        '    If EmployeeType.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            AsyncLoader(True)
        '            Using Model As New MEmployeeType(Me.Tag)
        '                If Await Model.DeleteEmployeeTypeAsync(EmployeeType) = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                    CleanControls()
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                End If
        '            End Using
        '            AsyncLoader(False)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneTipoEmpleado, Eform.TipoEmpleados)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneTipoEmpleado, Eform.TipoEmpleados)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        '    Exit Sub
        'End If
        'AsyncLoader(True)
        'AssigningValues()
        'Using Model As New MEmployeeType(Me.Tag)
        '    If Await Model.SaveEmployeeTypeAsync(EmployeeType) = True Then
        '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '        If EmployeeType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
        '        ElseIf EmployeeType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '        End If
        '        AsyncLoader(False)
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '    End If
        'End Using
        'Deshacer()
    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' METODO: Item nuevo del frontal del tipo de empleado
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaData, Eform.InfoMetaData), Me.EmployeeType.Code, Me.EmployeeType.Name), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .JournalVoucher = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & Me.Tag & "_" & Me.EmployeeType.Code & "#$", .IdForm = Me.Tag, _
        '        .Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaDataTitle, Eform.InfoMetaData), Me.EmployeeType.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaData, Eform.InfoMetaData), Me.EmployeeType.Code, Me.EmployeeType.Name)
        '    Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeTypeMetaDataTitle, Eform.InfoMetaData), Me.EmployeeType.Code)
        '    Return Me._doc
        'End If
    End Function

#End Region

#Region "Methods"

    Private Sub Initializes()

        INDgrdHumanTalent.DataSource = EmployeeList

    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()

    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IContractLiquidation.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' Funcion que liquida los contratos con los parametros seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LiquidateContracts()
        Try
            If ValidateControls() = False Then
                Return
            End If
            AsyncLoader(True)
            Using m As New MContractLiquidation(MContractLiquidation.TAG)
                ListContractLiquidation = Await m.LiquidateContracts(EmployeesToLiquidate)
                If ListContractLiquidation IsNot Nothing AndAlso ListContractLiquidation.Count > 0 Then

                    If ListContractLiquidation.Item(0).StateResult = True Then
                        ShowLiquidationDetail(True)
                        Me.BarraBotones.FilterDataSource = ListContractLiquidation
                        LoadEmployeeLiquidation(0)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
                    Else

                        Dim ListString As New List(Of String)
                        For Each ObjListContractLiquidation As MessageResult In ListContractLiquidation.Item(0).MessageResult
                            ListString.Add(ObjListContractLiquidation.Parameters(0))
                        Next

                        If ListContractLiquidation.Item(0).Message <> String.Empty Then
                            ListString.Add(ListContractLiquidation.Item(0).Message)
                        End If

                        Using formulario As New FrmListErrors(ListString)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                    Me.BarraBotones.SetDocuments(ListContractLiquidation.Item(0).ObjectEmbbeded.ContractId)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, ListContractLiquidation.Item(0).ObjectEmbbeded.ContractId, 0, ListContractLiquidation.Item(0).ObjectEmbbeded, Me.BarraBotones.OperatingUnit)
                End If
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Funcion que valida que se haya llenado los campos necesarios para la liquidacion de contrato
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If EmployeesToLiquidate.Count <> EmployeeList.Count Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Return False
        End If
        For Each itemEmployee In EmployeesToLiquidate
            If itemEmployee.Value.Item2 = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Carga un indice de los empleados liquidados
    ''' </summary>
    ''' <param name="index">indice</param>
    ''' <remarks></remarks>
    Private Sub LoadEmployeeLiquidation(index As Integer)
        Dim itemMessageLiquidation = ListContractLiquidation(index)
        LoadDataLiquidation(itemMessageLiquidation)
    End Sub

    ''' <summary>
    ''' Funcion para cargar los datos del contrato de liquidacion
    ''' </summary>
    ''' <param name="MessageLiquidation"></param>
    ''' <remarks></remarks>
    Private Sub LoadDataLiquidation(MessageLiquidation As ActionMessageResult(Of ContractLiquidation))
        RecordMessageLiquidation = MessageLiquidation
        Dim listError As New List(Of Domain.Payroll.Entities.Message)
        For Each itemMessage In MessageLiquidation.MessageResult
            Dim ObjMessaje = New Domain.Payroll.Entities.Message()
            ObjMessaje.Description = itemMessage.CodeMessage
            ObjMessaje.Error = False
            If itemMessage.Parameters.Length = 1 Then
                ObjMessaje.Error = itemMessage.Parameters(0)
            End If
            listError.Add(ObjMessaje)
        Next
        INDgcMessage.DataSource = listError
        Dim Heigth As Integer = 40 + (40 * listError.Count)

        Dim Liquidation = MessageLiquidation.ObjectEmbbeded
        ContractEmployee = Liquidation.Contract
        INDtxtGroup.Text = Liquidation.Contract.Group.Name
        INDtxtEmployee.Text = Liquidation.Employee.ThirdParty.Name
        INDtxtInitialDate.Text = Liquidation.Contract.JobBondingDate
        INDtxtEndDate.Text = Liquidation.RetirementDate
        INDtxtRetirementReason.Text = Liquidation.RetirementReason.Name
        INDtxtPosition.Text = Liquidation.Contract.Position.Name
        INDtxtSalaryBase.Text = Liquidation.Contract.BasicSalary
        INDtxtTotalPaid.Text = Liquidation.TotalPaid
        INDDeResolutionDate.Properties.ReadOnly = True
        INDtxtResolutionNumber.Properties.ReadOnly = True
        INDDeResolutionDate.EditValue = Liquidation.ResolutionDate
        INDtxtResolutionNumber.EditValue = Liquidation.ResolutionNumber
        INDlbEmployeeName.Text = Liquidation.Employee.ThirdParty.Nit & " - " & Liquidation.Employee.ThirdParty.Name
        INDgcLiquidationDetail.DataSource = Liquidation.ContractLiquidationDetail.Where(Function(x) x.ConceptType <> 3).ToList()
        INDGcEmployerLiquidation.DataSource = Liquidation.ContractLiquidationDetail.Where(Function(x) x.ConceptType = 3).ToList()
    End Sub

    ''' <summary>
    ''' Funcion que muestra u oculta los controles para ver el detalle de la liquidacion
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowLiquidationDetail(value As Boolean)
        If value = True Then
            INDlyItemEmployees.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemAddEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemHumanTalent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGroupHumanTalent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDlyItemEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemEmployeeBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemPanelLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyGroupHumanTalent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemEmployees.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemAddEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemHumanTalent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDlyItemEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemEmployeeBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPanelLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Function

    ''' <summary>
    ''' Retorna la imagen de error del mensje
    ''' </summary>
    ''' <param name="value">error si o no</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function returnImageError(value As Boolean) As Bitmap
        If value = True Then
            Return My.Resources.rojo_16x16
        Else
            Return My.Resources.verde_16x16
        End If
    End Function

    Private Async Sub SearchLiquidation()

        Dim resultContractLiquidation As New ActionMessageResult(Of ContractLiquidation)


        Using Form1 As New FrmListContractLiquidation
            Dim transparent As New FrmTransparent(Form1, False)
            With Form1
                transparent.ShowDialog()
                resultContractLiquidation.ObjectEmbbeded = .ListContractLiquidationEmploye
                .Dispose()
            End With
        End Using

        If resultContractLiquidation.ObjectEmbbeded IsNot Nothing Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            ShowLiquidationDetail(True)
            LoadDataLiquidation(resultContractLiquidation)

            Me.BarraBotones.PrintReport(PrintReportAction.None, ContractEmployee.Id, 0, ContractEmployee.Id, Me.BarraBotones.OperatingUnit)
        Else
            BarraBotones.ActualizarPermisosBarra(Me.Tag)
            Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
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
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        _culture.NumberFormat = numberFormat
        changeNumericFormatByCurrency(numberFormat)
        changeNumericFormatByCurrency(numberFormat, INDPopupContainerControl1.Controls)
        changeNumericFormatByCurrency(numberFormat, INDPopUpContainerControl2.Controls)
        INDGcValue = Window.Utils.FormatGrid(INDGcValue, _currencyAbbreviation)
        INDgrcBasicSalary = Window.Utils.FormatGrid(INDgrcBasicSalary, _currencyAbbreviation)
        INDColumAccrued = Window.Utils.FormatGrid(INDColumAccrued, _currencyAbbreviation)
        INDcolDeducted = Window.Utils.FormatGrid(INDcolDeducted, _currencyAbbreviation)
        INDgrcLiquidationTotalAccrued = Window.Utils.FormatGrid(INDgrcLiquidationTotalAccrued, _currencyAbbreviation)
        INDgrcLiquidationTotalDeducted = Window.Utils.FormatGrid(INDgrcLiquidationTotalDeducted, _currencyAbbreviation)
        INDgrcLiquidationTotalPaid = Window.Utils.FormatGrid(INDgrcLiquidationTotalPaid, _currencyAbbreviation)
        INDgrcLiquidationPeriodIBC = Window.Utils.FormatGrid(INDgrcLiquidationPeriodIBC, _currencyAbbreviation)
        INDtxtSalaryBase.Properties.Mask.Culture = _culture
        INDtxtTotalPaid.Properties.Mask.Culture = _culture

    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Model = Nothing
        RecordMessageLiquidation = Nothing
        ListContractLiquidation = Nothing
        ContractEmployee = Nothing
        _presenter = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se ejecuta al dar click en atras cuando estan viendo los empleados liquidados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        ShowLiquidationDetail(False)
        Me.BarraBotones.FilterDataSource = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        _presenter.Initializes()

        EmployeeList.Clear()
        EmployeesRetirementDateRestrictions.Clear()
        EmployeesToLiquidate.Clear()
        INDgrdHumanTalent.RefreshDataSource()
        INDDeResolutionDate.Properties.ReadOnly = False
        INDtxtResolutionNumber.Properties.ReadOnly = False
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar de registro en el control de navegacion
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        Dim objectMessageLiquidation = CType(Record, ActionMessageResult(Of ContractLiquidation))
        LoadDataLiquidation(objectMessageLiquidation)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se va dibujar una columna
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView2_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvMessage.CustomDrawCell
        If e.Column.Name = INDcolInfoMessage.Name Then
            Dim imagenDia = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
            imagenDia.Image = returnImageError(CBool(e.CellValue))
        End If
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmContractLiquidation_Load(sender As Object, e As EventArgs) Handles Me.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollEmployeeType.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter.Initializes()
        LoadStatus()
        Initializes()
        Deshacer()
        Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Identificación", .FieldName = "ObjectEmbbeded.Employee.ThirdParty.Nit"}, New ColumnInfo With {.Caption = "Empleado", .FieldName = "ObjectEmbbeded.Employee.ThirdParty.Name"}}.ToList()

        'Valido si la entidad es pública para Mostrar el Control del Número de Resolución
        If indigo.IndigoCompanyType = "2" Then
            INDlyItemResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        Dim ContractPermiso = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(indigo.UserIndigoId, indigo.UserRol, CStr(MyBase.Tag), indigo)
        For i As Integer = 0 To ContractPermiso.Count() - 1
            If ContractPermiso.Item(i).TagButton = 84 Then
                ActivateControls(True)
            Else
                ActivateControls(False)
            End If
        Next
    End Sub

    Public Sub ActivateControls(value As Boolean)
        INDtxtTotalPaid.Enabled = value
        INDtxtTotalPaid.ReadOnly = Not value
        INDcolDeducted.OptionsColumn.AllowEdit = value
        INDColumAccrued.OptionsColumn.AllowEdit = value
    End Sub

    Private Sub FrmContractLiquidation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If INDsleEmployees.Enabled Then
            INDsleEmployees.Focus()
        End If
    End Sub


    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad<
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded

    End Sub

    ''' <summary>
    ''' Metodo que busca el detalle de datos del empleado y lo añade a la rejilla
    ''' </summary>
    Private Async Sub INDbtnAddEmployee_Click(sender As Object, e As EventArgs) Handles INDbtnAddEmployee.Click

        Dim selectedEmployeeId = INDsleEmployees.EditValue

        Dim alreadyAddedEmployeeCount = EmployeeList.Where(Function(i) i.Id = selectedEmployeeId).Count

        If selectedEmployeeId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un empleado"
            Return
        End If

        If alreadyAddedEmployeeCount = 0 Then
            AsyncLoader(True)
            Dim employee = Await Presenter.GetSelectedEmployeeById(selectedEmployeeId)
            If employee Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El emplado no tiene contratos activos"
                Return
            End If
            Dim r = Await Presenter.GetMaximunMinimumRetirementDates(employee)
            EmployeeList.Add(employee)
            EmployeesRetirementDateRestrictions.Add(employee.Id, r)
            INDgrdHumanTalent.RefreshDataSource()
            INDsleEmployees.EditValue = 0
            AsyncLoader(False)
        Else

            Mensaje(EeventViewerImages.Advertencia) = "El empleado ya se encuentra agregado en el listado"

        End If

    End Sub

    ''' <summary>
    ''' Metodo para asignar el texto de eliminacion al boton de la rejilla
    ''' </summary>
    Private Sub INDrepDeleteAction_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepDeleteAction.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.EliminarRegistro)
    End Sub

    ''' <summary>
    ''' Metodo que remueve el empleado seleccionado del listado de empleados a liquidar
    ''' </summary>
    Private Sub INDrepDeleteAction_Click(sender As Object, e As EventArgs) Handles INDrepDeleteAction.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdHumanTalent.DefaultView.GetRow(CType(INDgrdHumanTalent.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), Employee)
            EmployeeList.Remove(ToDelete)
            EmployeesToLiquidate.Remove(ToDelete.Id)
            EmployeesRetirementDateRestrictions.Remove(ToDelete.Id)
            INDgrdHumanTalent.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el valor de la fecha de retiro, confirmacion y empleado en el listado de empleados a liquidar
    ''' </summary>
    Private Sub INDgrvHumanTalent_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDgrvHumanTalent.CustomUnboundColumnData


        Dim currentEmployee As Employee = CType(e.Row, Employee)
        Dim currentEmployeeId As Integer = If(currentEmployee IsNot Nothing, currentEmployee.Id, 0)

        If currentEmployeeId = 0 Then
            Return
        End If

        If e.Column.Name = INDgrcRetirementDate.Name Then

            If e.IsGetData Then

                If EmployeesToLiquidate.ContainsKey(currentEmployeeId) = True Then
                    e.Value = EmployeesToLiquidate(currentEmployeeId).Item1
                Else
                    e.Value = EmployeesRetirementDateRestrictions(currentEmployee.Id)(retirementDates.minimumRetirementDate)
                End If

            End If

            If e.IsSetData Then

                If EmployeesToLiquidate.ContainsKey(currentEmployeeId) = False Then
                    EmployeesToLiquidate.Add(currentEmployeeId, New Tuple(Of Date, Integer)(e.Value, 0))
                Else
                    Dim OldValue = EmployeesToLiquidate(currentEmployeeId)
                    EmployeesToLiquidate(currentEmployeeId) = New Tuple(Of Date, Integer)(e.Value, OldValue.Item2)
                End If

            End If
        ElseIf e.Column.Name = INDgrcRetirementReason.Name Then
            If e.IsGetData Then
                If EmployeesToLiquidate.ContainsKey(currentEmployeeId) = True Then
                    e.Value = EmployeesToLiquidate(currentEmployeeId).Item2
                Else
                    e.Value = 0
                End If
            End If
            If e.IsSetData Then
                If EmployeesToLiquidate.ContainsKey(currentEmployeeId) = False Then
                    EmployeesToLiquidate.Add(currentEmployeeId, New Tuple(Of Date, Integer)(EmployeesRetirementDateRestrictions(currentEmployee.Id)(retirementDates.minimumRetirementDate), e.Value))
                Else
                    Dim OldValue = EmployeesToLiquidate(currentEmployeeId)
                    EmployeesToLiquidate(currentEmployeeId) = New Tuple(Of Date, Integer)(OldValue.Item1, e.Value)
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' Metodo para limitar las fechas de retiro basadas en el contrato
    ''' </summary>
    Private Sub INDgrvHumanTalent_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles INDgrvHumanTalent.FocusedRowChanged
        Dim selectedEmployee As Employee = INDgrvHumanTalent.GetFocusedRow()

        If selectedEmployee IsNot Nothing Then
            INDrepRetirementDate.MinValue = EmployeesRetirementDateRestrictions(selectedEmployee.Id)(retirementDates.minimumRetirementDate)
            INDrepRetirementDate.MaxValue = EmployeesRetirementDateRestrictions(selectedEmployee.Id)(retirementDates.maximumRetiremenDate)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra el id del contrato como contrato inicial cuando el contrato es base y tiene como contrato inicial "0"
    ''' </summary>
    Private Sub INDgrvContract_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgrvContract.CustomColumnDisplayText

        If e.Column.Name = INDgrcInitialContractNumber.Name Then

            If CInt(e.Value) = 0 Then

                Dim c = CType(sender, GridView).GetFocusedRow
                If c IsNot Nothing Then
                    e.DisplayText = c.Id
                End If

            End If

        End If

    End Sub

#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.RibbonPagEform.Visible = False
        Me.BarraBotones.RibbonPageRejillas.Visible = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' Barra botones boton liquidar
    ''' </summary>
    Private Sub BarraBotones_ClicLiquidar() Handles BarraBotones.ClickLiquidar
        LiquidateContracts()
    End Sub

    Private Sub BarraBotones_ClickConsultarLiquidacion() Handles BarraBotones.ClickConsultLiquidar
        SearchLiquidation()
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, ContractEmployee.Id, 0, ContractEmployee.Id, Me.BarraBotones.OperatingUnit)
    End Sub

#End Region

#Region "Customization"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCtrContractLiquidation.ShowCustomizationForm()
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
            INDlyCtrContractLiquidation.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrContractLiquidation.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MContractLiquidation(Me.Tag)
                Dim dsFields As DataSet = model.GetNullFields
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCtrContractLiquidation.Items.Count - 1
                        INDlyCtrContractLiquidation.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCtrContractLiquidation.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtrContractLiquidation.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCtrContractLiquidation.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrContractLiquidation.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCtrContractLiquidation.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCtrContractLiquidation.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyCtrContractLiquidation.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "Dynamic Grid Loading"

    ''' <summary>
    ''' Listado de relaciones que tiene el contrato 
    ''' </summary>
    Private relations As String() = {"Liquidations", "Novelties"}

    ''' <summary>
    ''' Metodo para asignar las relaciones a la fila que dispara el evento
    ''' </summary>
    Private Sub INDgrvContract_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles INDgrvContract.MasterRowGetRelationCount
        e.RelationCount = 2
    End Sub

    ''' <summary>
    ''' Metodo para definir si la fila es vacia
    ''' </summary>
    Private Sub INDgrvContract_MasterRowEmpty(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs) Handles INDgrvContract.MasterRowEmpty
        e.IsEmpty = IsRelationEmpty(e.RowHandle, e.RelationIndex)
    End Sub

    ''' <summary>
    ''' Metodo que indica sino hay relacion en esa fila
    ''' </summary>
    Function IsRelationEmpty(ByVal rowHandle As Integer, ByVal relationIndex As Integer) As Boolean

        If (rowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle) Then
            Return True
        End If

        Return relations(relationIndex) Is Nothing
    End Function

    ''' <summary>
    ''' Metodo que consulta dinamicamente los datos del detalle del contrato seleccionado
    ''' </summary>
    Private Sub INDgrvContract_MasterRowGetChildList(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs) Handles INDgrvContract.MasterRowGetChildList
        AsyncLoader(True)
        If IsRelationEmpty(e.RowHandle, e.RelationIndex) Then Return

        Dim s As String = relations(e.RelationIndex).ToString()

        Dim currentView = CType(INDgrdHumanTalent.FocusedView, GridView)

        If currentView.Name = INDgrvContract.Name Then

            Dim selectedRow = currentView.FocusedRowHandle
            Dim selectedContract = CType(currentView.GetRow(selectedRow), Contract)

            If selectedContract IsNot Nothing Then
                LiquidationList = Presenter.GetPaymentsByContractId(selectedContract.Id)
            End If

        End If

        Select Case s
            Case "Liquidations"

                e.ChildList = LiquidationList

            Case "Novelties"

                e.ChildList = LiquidationList
        End Select

        INDgrvContract.RefreshData()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo que coloca los nombres de las relaciones en las filas
    ''' </summary>
    Private Sub INDgrvContract_MasterRowGetRelationName(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs) Handles INDgrvContract.MasterRowGetRelationName
        If IsRelationEmpty(e.RowHandle, e.RelationIndex) Then
            e.RelationName = ""
        Else
            e.RelationName = relations(e.RelationIndex).ToString()
        End If
    End Sub

#End Region

    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If RecordMessageLiquidation.StateResult = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(EmpleadoTieneErrores, Eform.LiquidacionContrato)
            Return
        End If
        AsyncLoader(True)
        Using modelContract As New MContractLiquidation(MyBase.Tag)
            RecordMessageLiquidation.ObjectEmbbeded.ResolutionDate = INDDeResolutionDate.EditValue
            RecordMessageLiquidation.ObjectEmbbeded.ResolutionNumber = INDtxtResolutionNumber.EditValue
            RecordMessageLiquidation.ObjectEmbbeded.TotalPaid = INDtxtTotalPaid.EditValue

            Dim result = Await modelContract.ConfirmLiquidationContract(RecordMessageLiquidation.ObjectEmbbeded)
            If result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = "Se confirmó la Liquidación de Contrato. " + result.Message
                If ListContractLiquidation.Count = 1 Then
                    Deshacer()
                Else
                    ListContractLiquidation.Remove(RecordMessageLiquidation)
                    Me.BarraBotones.FilterDataSource = ListContractLiquidation
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If

        End Using
        AsyncLoader(False)
    End Sub

    Private Async Sub BarraBotones_ClickConfirmarTodos() Handles BarraBotones.ClickConfirmarTodos
        Dim queryError = From e In ListContractLiquidation
                         Where e.StateResult = False
                         Select e
        If queryError.Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(AlgunEmpleadoTieneErrores, Eform.LiquidacionContrato)
            Return
        End If
        Dim listContractTmp = (From e In ListContractLiquidation
                               Select e.ObjectEmbbeded).ToList()
        AsyncLoader(True)
        Using modelContract As New MContractLiquidation(MyBase.Tag)
            Dim result = Await modelContract.ConfirmListLiquidationContract(listContractTmp)
            If result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(SeConfirmaronTodos)
                Deshacer()
            End If
        End Using
        AsyncLoader(False)
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit1.Click
        Dim FormulasToShow As ContractLiquidationDetail = GridView1.GetFocusedRow()
        AssignValues(FormulasToShow)
    End Sub

    Private Sub AssignValues(ByVal FormulasToShow As ContractLiquidationDetail)
        INDteUsedFormula.Text = String.Empty
        INDteReplaceFormula.Text = String.Empty
        INDteResultFormula.Text = String.Empty
        INDteUsedFormula.Text = FormulasToShow.ConceptFormulate
        INDteReplaceFormula.Text = FormulasToShow.ReplaceConceptFormulate
        INDteResultFormula.Text = IIf(FormulasToShow.Accrued > 0, FormulasToShow.Accrued, FormulasToShow.Deducted)
    End Sub

    Private Sub AssignValuesEmployer(ByVal FormulasToShow As ContractLiquidationDetail)
        INDteUsedFormulaEmployer.Text = String.Empty
        INDteReplaceFormulaEmployer.Text = String.Empty
        INDteResultFormulaEmployer.Text = String.Empty
        INDteUsedFormulaEmployer.Text = FormulasToShow.ConceptFormulate
        INDteReplaceFormulaEmployer.Text = FormulasToShow.ReplaceConceptFormulate
        INDteResultFormulaEmployer.Text = IIf(FormulasToShow.Accrued > 0, FormulasToShow.Accrued, FormulasToShow.Deducted)
    End Sub

    Private Sub RepositoryItemPopupContainerEdit2_Click(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit2.Click
        Dim FormulasToShow As ContractLiquidationDetail = GridView2.GetFocusedRow()
        AssignValuesEmployer(FormulasToShow)
    End Sub

End Class