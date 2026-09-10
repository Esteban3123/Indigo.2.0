'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 10-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base
Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Design.DesignTimeTools
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo

''' <summary>
''' Clase que maneja el control del visualizador de contratos, permite ver el historico de contratos, hacer comparaciones entre 
''' el contrato actual y uno historico, tambien permite realizar acciones sobre el contrato actual
''' </summary>
''' <remarks></remarks>
Public Class CtrContractViewer
    Implements ICtrContractViewer

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

#Region "Fields N Properties"

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PCtrContractViewer

    ''' <summary>
    ''' Propiedad que contiene el listado de razones de retiro
    ''' </summary>
    Public WriteOnly Property RetirementReasonDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ICtrContractViewer.RetirementReasonDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleActualRetirementReason.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que recibe el listado de los contratos que tenga el empleado
    ''' </summary>
    'Private _contracts As List(Of Contract)
    'Private Property Contracts() As List(Of Contract)
    '    Get
    '        Return _contracts
    '    End Get
    '    Set(ByVal value As List(Of Contract))
    '        _contracts = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Propiedad que recibe el empleado 
    ''' </summary>
    Private _employee As Employee
    Public Sub Employee(ByRef employee As Employee)
        _employee = employee
    End Sub

    ''' <summary>
    ''' Almacena el contrato que se muestra como contrato actual, este puede ser el contrato actual vigente o el ultimo 
    ''' contrato que haya tenido activo en caso de no haber uno vigente
    ''' </summary>
    Private _currentContract As Contract

    ''' <summary>
    ''' Propiedad que tiene referencia a la barrabotones para el manejo de los estados
    ''' </summary>
    Private _barraBotones As CtrBarraBotones
    Public WriteOnly Property BarraBotones() As CtrBarraBotones
        Set(ByVal value As CtrBarraBotones)
            _barraBotones = value
        End Set
    End Property

    Private _valid As Boolean
    Public Property Valid() As Boolean
        Private Get
            Return _valid
        End Get
        Set(ByVal value As Boolean)
            _valid = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor  de la moneda parametrizada
    ''' </summary>
    Private _currencyAbbreviation As String
    Public Property CurrencyAbbreviation As String
        Get
            Return If(String.IsNullOrEmpty(_currencyAbbreviation), indigo?.CurrencyISO4217, _currencyAbbreviation)
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property


#End Region

#Region "Methods"



    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            '
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que muestra el contrato vigente o de no haber uno vigente muestra el ultimo contrato de acuerdo a la fecha
    ''' </summary>
    Public Async Sub ShowLatestOrActualContract()
        Dim vig = _employee.Contract.Where(Function(c) c.Valid = True AndAlso c.Status = 1 AndAlso Not c.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted).FirstOrDefault 'Busco un contrato vigente y activo
        If vig Is Nothing Then 'No existe contrato vigente y activo se debe permitir crear uno nuevo
            Dim newest = _employee.Contract.OrderByDescending(Function(c) c.ContractInitialDate).FirstOrDefault

            If newest IsNot Nothing Then
                _currentContract = newest
                LoadInfoInUI(newest)
                ShowHideHistoryContracts(newest)

                _barraBotones.StatusRecord = _currentContract.Status
                Presenter = New PCtrContractViewer(Me)
                Dim confirmedLiq = Await Presenter.CheckConfirmedLiquidationByContractId(_currentContract.Id)
                'Si tiene nominas confirmadas no se permite la eliminacion
                Dim allowToDelete = If(confirmedLiq = True, False, True)
                Dim allowToEdit = If(confirmedLiq = True, False, True)

                'TODO Verificacion de estado para mostrarlo en la barra de estados, y para mostrar las opciones
                Select Case _currentContract.Status
                    Case 2 'Liquidado
                        EnableControlsFor(True, False, False, False)
                    Case 3 'Anulado
                        EnableControlsFor(True, allowToEdit, False, allowToDelete)
                    Case 4 'Reemplazado
                        EnableControlsFor(True, allowToEdit, False, allowToDelete)
                    Case 5
                        ActivarContratos()

                    Case Else

                End Select
            Else
                'Contrato nuevo
                CleanControls()

                EnableControlsFor(True, False, False, False)
            End If

        Else 'Existe contrato vigente se debe permitir modificar sino tiene nominas confirmadas
            _currentContract = vig
            LoadInfoInUI(vig)
            ShowHideHistoryContracts(vig)
            _barraBotones.StatusRecord = _currentContract.Status
            Presenter = New PCtrContractViewer(Me)
            Dim confirmedLiq = Await Presenter.CheckConfirmedLiquidationByContractId(_currentContract.Id)
            'Si tiene nominas confirmadas no se permite la eliminacion
            Dim allowToDelete = If(confirmedLiq = True, False, True)

            If _currentContract.Id = 0 Then
                EnableControlsFor(False, False, False, allowToDelete)
            Else
                If EmployeeHelper.ContractClasses.Where(Function(i) i.Item2 = _currentContract.ContractType.ContractClass).FirstOrDefault.Item1 = Eresources.ClaseContratoAprendizaje Then
                    'Permite terminacion de contrato
                    EnableControlsFor(False, True, True, allowToDelete)
                    'EnableControlsFor(False, False, False, allowToDelete)
                Else
                    'No se permite la terminacion de contrato desde el modulo
                    EnableControlsFor(False, True, True, allowToDelete)
                    'EnableControlsFor(False, False, False, allowToDelete)
                End If
            End If

        End If
    End Sub

    Private Sub ActivarContratos()
        INDbbiAddNewContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDbbiRenewContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDbbiFinishContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDbbiDeleteContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDbbiActivateContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    End Sub

    ''' <summary>
    ''' Metodo que habilita opciones del menu de acciones sobre el contrato que se encuentra en la vista principal del visor
    ''' </summary>
    ''' <param name="allowNewAction">Verdadero para permitir Nuevo contrato, Falso en caso contrario</param>
    ''' <param name="allowEditAction">Verdadero para permitir Editar contrato, Falso en caso contrario</param>
    ''' <param name="allowFinishAction">Verdadero para permitir Terminar contrato, Falso en caso contrario</param>
    ''' <param name="allowDeleteAction">Verdadero para permitir Eliminar contrato, Falso en caso contrario</param>
    ''' <remarks></remarks>
    Public Sub EnableControlsFor(allowNewAction As Boolean, allowEditAction As Boolean, allowFinishAction As Boolean, allowDeleteAction As Boolean)
        INDbbiActivateContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        'Permite Nuevo
        If allowNewAction = True Then
            INDbbiAddNewContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDbbiAddNewContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        'Permite Editar
        If allowEditAction = True Then
            INDbbiRenewContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDbbiRenewContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        'Permite Terminar
        If allowFinishAction = True Then
            INDbbiFinishContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDbbiFinishContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        'Permite Eliminar
        If allowDeleteAction = True Then
            INDbbiDeleteContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDbbiDeleteContract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra la opcion de ver historico de contratos 
    ''' </summary>
    ''' <param name="con">Numero de contrato actual</param>
    Private Sub ShowHideHistoryContracts(con As Contract)
        INDlciShowContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Metodo que muestra el contrato por el numero que se envia en el parametro
    ''' </summary>
    ''' <param name="contractNo">Numero del contrato a mostrar</param>
    Public Sub ShowContractByNumber(contractNo As Integer)
        'TODO Implementar funcionalidad
    End Sub

    ''' <summary>
    ''' Carga la rejilla con el listado historico de contratos que ha tenido el contrato actual
    ''' </summary>
    ''' <param name="parentContractNo">Numero del contrato padre</param>
    Public Sub LoadContractHistoryByParentContract(parentContractNo As Integer, currentContractNo As Integer)
        Dim ds = _employee.Contract.Where(Function(c) Not c.Id = currentContractNo).ToList
        INDgrdContractHistory.DataSource = ds
    End Sub

    ''' <summary>
    ''' Carga el historial de cambios de contratos filtrado por EmployeeId
    ''' </summary>
    Public Sub LoadHistoryChanges()
        If _employee IsNot Nothing AndAlso _employee.Id > 0 Then
            Try
                Dim auditDataCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.ListContractAuditByEmployeeId(_employee.Id).OrderByDescending(Function(n) n.Id)
                ' Crear lista wrapper con propiedades de agrupación
                Dim listaWrapper As New List(Of HistoryChangesWrapper)
                For Each item As ViewContractAuditXpo In auditDataCollection.ToList()
                    Dim wrapper As New HistoryChangesWrapper()
                    wrapper.ContractStatus = item.ContractStatus
                    wrapper.ContractId = item.ContractId
                    wrapper.Type = item.Type
                    wrapper.ValueOld = item.ValueOld
                    wrapper.ValueNew = item.ValueNew
                    wrapper.UserCodeName = item.UserCodeName
                    wrapper.[Date] = item.[Date]
                    listaWrapper.Add(wrapper)
                Next
                INDgrdHistoryChanges.DataSource = listaWrapper
            Catch ex As Exception
                INDgrdHistoryChanges.DataSource = Nothing
            End Try
        Else
            INDgrdHistoryChanges.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Limpia los labels que se encuentran con datos, y limpia el datasource de la rejilla del historial de contratos
    ''' </summary>
    Public Sub CleanControls()
        INDlblResolutionNumber.Text = String.Empty
        INDlblPosesionDate.Text = String.Empty
        INDlblResolutionDate.Text = String.Empty
        INDlblPosesionNumber.Text = String.Empty
        INDlblContractNo.Text = String.Empty
        INDlblRowType.Text = String.Empty
        INDlblPosition.Text = String.Empty
        INDlblSalaryType.Text = String.Empty
        INDlblGroup.Text = String.Empty
        INDlblFunctionalUnit.Text = String.Empty
        INDlblCostCenter.Text = String.Empty
        INDlblStartDate.Text = String.Empty
        INDlblEndingDate.Text = String.Empty
        INDlblSalary.Text = String.Empty
        INDlblJobBondingDate.Text = String.Empty

        'Mas informacion
        INDlblContractType.Text = String.Empty
        INDlblJobBondingType.Text = String.Empty
        INDlblRetirementReason.Text = String.Empty
        INDlblPaymentPeriod.Text = String.Empty
        INDlblRetirementDate.Text = String.Empty
        INDlblPaymentType.Text = String.Empty
        INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'Historicoñ
        INDgrdContractHistory.DataSource = New List(Of Contract)
        INDgrdContractHistory.RefreshDataSource()
        INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'Ocultar historial de cambios
        INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDgrdHistoryChanges.DataSource = Nothing

        INDctrContractEdit.CleanControls()

        EnableControlsFor(False, False, True, False)
    End Sub

    ''' <summary>
    ''' Metodo que carga la informacion a la interfaz para ser mostrada 
    ''' </summary>
    Private Sub LoadInfoInUI(contract As Contract)
        INDlblContractNo.Text = If(contract.Id = 0, obtenerRecurso(LabelNuevo, RecepcionObjeciones), contract.Id.ToString)
        INDlblContractNo.ToolTip = INDlblContractNo.Text

        INDlblResolutionNumber.Text = If(contract.ResolutionNumber IsNot Nothing, contract.ResolutionNumber.ToString(), String.Empty)
        INDlblResolutionDate.Text = If(contract.ResolutionDate IsNot Nothing, String.Format("{0:dd \de MMMM \de yyyy}", contract.ResolutionDate), String.Empty)
        INDlblPosesionNumber.Text = If(contract.CertificateOfficeNumber IsNot Nothing, contract.CertificateOfficeNumber.ToString(), String.Empty)
        INDlblPosesionDate.Text = If(contract.PosesionDate IsNot Nothing, String.Format("{0:dd \de MMMM \de yyyy}", contract.PosesionDate), String.Empty)

        ' ANTES: Solo mostraba "Contrato Base" o "Novedad"
        ' DESPUÉS: Ahora también muestra "Novedad extemporánea" cuando IsExtemporaneousChange = True
        If contract.RowType = CType(1, Byte) Then
            INDlblRowType.Text = obtenerRecurso(Eresources.ContratoBase, Contrato)
            INDlblRowType.ToolTip = obtenerRecurso(Eresources.ContratoBase, Contrato)
            INDlblRowType.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(196, Byte), Integer))
        Else
            ' Es una novedad (RowType = 2), verificar si es extemporánea
            If contract.IsExtemporaneousChange Then
                INDlblRowType.Text = "Novedad extemporánea"
                INDlblRowType.ToolTip = "Ajuste extemporáneo de Cargo y/o Salario Básico (no genera retroactivos)"
                INDlblRowType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(179, Byte), Integer)) ' Color naranja claro
            Else
                INDlblRowType.Text = obtenerRecurso(Eresources.Novedad, Contrato)
                INDlblRowType.ToolTip = obtenerRecurso(Eresources.Novedad, Contrato)
                INDlblRowType.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(194, Byte), Integer))
            End If
        End If

        'INDlblRowType.Text = If(contract.RowType = CType(1, Byte), obtenerRecurso(Eresources.ContratoBase, Contrato), obtenerRecurso(Eresources.Novedad, Contrato))
        If contract.Position?.Name IsNot Nothing Then
            INDlblPosition.Text = contract.Position.Name.ToString
            INDlblPosition.ToolTip = contract.Position.Name.ToString
        Else
            INDlblPosition.Text = contract.PositionName.ToString
            INDlblPosition.ToolTip = contract.PositionName.ToString
        End If
        INDlblPosition.Tag = contract.PositionId

        INDpcePosition.Text = contract.Status
        If contract.ContractType?.SalaryType > 0 Then
            INDlblSalaryType.Text = EmployeeHelper.SalaryType.Where(Function(i) i.Item2 = contract.ContractType.SalaryType).FirstOrDefault.Item3.ToString
            INDlblSalaryType.ToolTip = INDlblSalaryType.Text
        Else
            INDlblSalaryType.Text = EmployeeHelper.SalaryType.Where(Function(i) i.Item2 = contract.SalaryType).FirstOrDefault.Item3.ToString
            INDlblSalaryType.ToolTip = INDlblSalaryType.Text
        End If
        If contract.Group?.Name IsNot Nothing Then
            INDlblGroup.Text = contract.Group.Name.ToString
            INDlblGroup.ToolTip = contract.Group.Name.ToString
        Else
            INDlblGroup.Text = contract.GroupName.ToString
            INDlblGroup.ToolTip = contract.GroupName.ToString
        End If
        If contract.FunctionalUnit?.Name IsNot Nothing Then
            INDlblFunctionalUnit.Text = contract.FunctionalUnit.Name.ToString
            INDlblFunctionalUnit.ToolTip = contract.FunctionalUnit.Name.ToString
        Else
            INDlblFunctionalUnit.Text = contract.FunctionalUnitName.ToString
            INDlblFunctionalUnit.ToolTip = contract.FunctionalUnitName.ToString
        End If
        INDlblCostCenter.Text = contract.Employee.CostCenter.Name.ToString
        INDlblCostCenter.ToolTip = contract.Employee.CostCenter.Name.ToString
        INDlblStartDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", contract.ContractInitialDate)
        INDlblStartDate.ToolTip = INDlblStartDate.Text
        INDlblEndingDate.Text = If(contract.ContractEndingDate.Equals(#12/31/9999#), obtenerRecurso(Eresources.Indefinido, Contrato), String.Format("{0:dd \de MMMM \de yyyy}", contract.ContractEndingDate))
        INDlblEndingDate.ToolTip = INDlblEndingDate.Text
        INDlblJobBondingDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", contract.JobBondingDate)
        INDlblJobBondingDate.ToolTip = "Fecha de Contratación del Empleado"
        INDlblSalary.Text = Utils.GetMoneyWithISO4217(contract.BasicSalary, CurrencyAbbreviation)
        INDlblSalary.ToolTip = String.Format("{0:C0}", contract.BasicSalary)

        'Mas informacion
        If contract.ContractType?.Name IsNot Nothing Then
            INDlblContractType.Text = contract.ContractType.Name.ToString
            INDlblContractType.ToolTip = contract.ContractType.Name.ToString
        Else
            INDlblContractType.Text = contract.ContractTypeName.ToString
            INDlblContractType.ToolTip = contract.ContractTypeName.ToString
        End If

        If contract.ContractType?.JobBondingType IsNot Nothing Then
            INDlblJobBondingType.Text = contract.ContractType.JobBondingType.Name
            INDlblJobBondingType.ToolTip = contract.ContractType.JobBondingType.Name
        Else

            INDlblJobBondingType.Text = contract.JobBondingName
            INDlblJobBondingType.ToolTip = contract.JobBondingName
        End If

        'Verifica si hay informacion de retiro del empleado para mostrarla
        If contract.RetirementReason IsNot Nothing Then
            INDlciRetirementReason.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlblRetirementReason.Text = contract.RetirementReason.Name.ToString
            INDlblRetirementReason.ToolTip = contract.RetirementReason.Name.ToString
            INDlciRetirementDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlblRetirementDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", contract.RetirementDate)
            INDlblRetirementDate.ToolTip = String.Format("{0:dd \de MMMM \de yyyy}", contract.RetirementDate)
        Else
            INDlciRetirementReason.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlblRetirementReason.Text = String.Empty
            INDlblRetirementReason.ToolTip = String.Empty
            INDlciRetirementDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlblRetirementDate.Text = String.Empty
            INDlblRetirementDate.ToolTip = String.Empty
        End If

        INDlblPaymentPeriod.Text = EmployeeHelper.PaymentPeriod.Where(Function(i) i.Item2 = contract.PaymentPeriod).FirstOrDefault.Item3.ToString
        INDlblPaymentPeriod.ToolTip = INDlblPaymentPeriod.Text

        INDlblPaymentType.Text = EmployeeHelper.PaymentType.Where(Function(i) i.Item2 = contract.PaymentType).FirstOrDefault.Item3.ToString
        INDlblPaymentType.ToolTip = INDlblPaymentType.Text

        'Historico
        LoadContractHistoryByParentContract(contract.InitialContractNumber, contract.Id)

        'Ocultar historial de cambios por defecto
        INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Carga la informacion del contrato seleccionado de la rejilla al visualizador de mas informacion 
    ''' </summary>
    ''' <param name="con">Contrato seleccionado en la rejilla</param>
    Private Sub LoadInfoInCompleteContract(con As Contract)

        ' ANTES: Solo mostraba "Contrato Base" o "Novedad"
        ' DESPUÉS: Ahora también muestra "Novedad extemporánea" cuando IsExtemporaneousChange = True
        If con.RowType = CType(1, Byte) Then
            INDlblOldRowType.Text = obtenerRecurso(Eresources.ContratoBase, Contrato)
            INDlblOldRowType.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(196, Byte), Integer))
        Else
            ' Es una novedad (RowType = 2), verificar si es extemporánea
            If con.IsExtemporaneousChange Then
                INDlblOldRowType.Text = "Novedad extemporánea"
                INDlblOldRowType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(179, Byte), Integer)) ' Color naranja claro
            Else
                INDlblOldRowType.Text = obtenerRecurso(Eresources.Novedad, Contrato)
                INDlblOldRowType.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(194, Byte), Integer))
            End If
        End If

        'INDlblOldRowType.Text = If(con.RowType = CType(1, Byte), obtenerRecurso(Eresources.ContratoBase, Contrato), obtenerRecurso(Eresources.Novedad, Contrato))
        INDlblOldPosition.Text = con.Position.Name.ToString
        INDlblOldFunctionalUnit.Text = con.FunctionalUnit.Name.ToString
        INDlblOldContractType.Text = con.ContractType.Name.ToString
        INDlblOldJobBondingType.Text = con.ContractType.JobBondingType.Name.ToString
        INDlblOldJobBondingDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", con.JobBondingDate)
        INDlblOldContractInitialDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", con.ContractInitialDate)
        INDlblOldContractEndingDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", con.ContractEndingDate)
        INDlblOldBasicSalary.Text = Utils.GetMoneyWithISO4217(con.BasicSalary, CurrencyAbbreviation)
        INDlciSalary.Text = Utils.GetMoneyWithISO4217(con.BasicSalary, CurrencyAbbreviation)

        If con.RetirementReason IsNot Nothing Then
            INDlblOldRetirementReason.Text = con.RetirementReason.Name.ToString
            INDlblOldRetirementDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", con.RetirementDate)
        Else
            INDlblOldRetirementReason.Text = String.Empty
            INDlblOldRetirementDate.Text = String.Empty
        End If

        INDlblOldPaymentPeriod.Text = EmployeeHelper.PaymentPeriod.Where(Function(i) i.Item2 = con.PaymentPeriod).FirstOrDefault.Item3.ToString
        INDlblOldPaymentType.Text = EmployeeHelper.PaymentType.Where(Function(i) i.Item2 = con.PaymentType).FirstOrDefault.Item3.ToString
        INDlblOldSalaryType.Text = EmployeeHelper.SalaryType.Where(Function(i) i.Item2 = con.ContractType.SalaryType).FirstOrDefault.Item3.ToString
        'Envia datos para que se carguen en la rejilla de comparacion
        LoadDataInComparationGrid(con)
    End Sub

    ''' <summary>
    ''' Metodo que recibe el contrato seleccionado para que se compare con el contrato vigente o mas reciente
    ''' </summary>
    ''' <param name="con">Contrato Seleccionado</param>
    Private Sub LoadDataInComparationGrid(con As Contract)
        Dim concepts As New List(Of Tuple(Of String, String, String))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.Id, Contrato), con.Id.ToString, _currentContract.Id.ToString))
        ' ANTES: Solo mostraba "Contrato Base" o "Novedad" en la comparación
        ' DESPUÉS: Ahora también muestra "Novedad extemporánea" cuando corresponda
        Dim conRowTypeText As String = If(con.RowType = CType(1, Byte), obtenerRecurso(Eresources.ContratoBase, Contrato), If(con.IsExtemporaneousChange, "Novedad extemporánea", obtenerRecurso(Eresources.Novedad, Contrato)))
        Dim currentRowTypeText As String = If(_currentContract.RowType = CType(1, Byte), obtenerRecurso(Eresources.ContratoBase, Contrato), If(_currentContract.IsExtemporaneousChange, "Novedad extemporánea", obtenerRecurso(Eresources.Novedad, Contrato)))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.RowType, Contrato), conRowTypeText, currentRowTypeText))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.InitialContractNumber, Contrato), con.InitialContractNumber.ToString, _currentContract.InitialContractNumber.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.Position, Contrato), con.Position.Name.ToString, _currentContract.Position.Name.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.Group, Contrato), con.Group.Name.ToString, _currentContract.Group.Name.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.FunctionalUnit, Contrato), con.FunctionalUnit.Name.ToString, _currentContract.FunctionalUnit.Name.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.ContractType, Contrato), con.ContractType.Name.ToString, _currentContract.ContractType.Name.ToString))
        'concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.JobBondingType, Contrato), con.JobBondingType.Name.ToString, _currentContract.JobBondingType.Name.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.JobBondingDate, Contrato), String.Format("{0:dd \de MMMM \de yyyy}", con.JobBondingDate), String.Format("{0:dd \de MMMM \de yyyy}", _currentContract.JobBondingDate)))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.ContractInitialDate, Contrato), String.Format("{0:dd \de MMMM \de yyyy}", con.ContractInitialDate), String.Format("{0:dd \de MMMM \de yyyy}", _currentContract.ContractInitialDate)))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.ContractEndingDate, Contrato), If(con.ContractEndingDate.Equals(#12/31/9999#), obtenerRecurso(Eresources.Indefinido, Contrato), String.Format("{0:dd \de MMMM \de yyyy}", con.ContractEndingDate)), If(_currentContract.ContractEndingDate.Equals(#12/31/9999#), obtenerRecurso(Eresources.Indefinido, Contrato), String.Format("{0:dd \de MMMM \de yyyy}", _currentContract.ContractEndingDate))))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.BasicSalary, Contrato), Utils.GetMoneyWithISO4217(con.BasicSalary, CurrencyAbbreviation), Utils.GetMoneyWithISO4217(_currentContract.BasicSalary, CurrencyAbbreviation)))
        '-----------------------------------------
        'concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.Status, Contrato), con.Status.ToString, _currentContract.Status.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.RetirementReason, Contrato), If(con.RetirementReason IsNot Nothing, con.RetirementReason.Name.ToString, String.Empty), If(_currentContract.RetirementReason IsNot Nothing, _currentContract.RetirementReason.Name.ToString, String.Empty)))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.RetirementDate, Contrato), If(con.RetirementDate IsNot Nothing, String.Format("{0:dd \de MMMM \de yyyy}", con.RetirementDate), String.Empty), If(_currentContract.RetirementDate IsNot Nothing, String.Format("{0:dd \de MMMM \de yyyy}", _currentContract.RetirementDate), String.Empty)))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.PaymentPeriod, Contrato), EmployeeHelper.PaymentPeriod.Where(Function(i) i.Item2 = con.PaymentPeriod).FirstOrDefault.Item3.ToString, EmployeeHelper.PaymentPeriod.Where(Function(i) i.Item2 = _currentContract.PaymentPeriod).FirstOrDefault.Item3.ToString))
        'concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.SalaryType, Contrato), EmployeeHelper.SalaryType.Where(Function(i) i.Item2 = con.SalaryType).FirstOrDefault.Item3.ToString, EmployeeHelper.SalaryType.Where(Function(i) i.Item2 = _currentContract.SalaryType).FirstOrDefault.Item3.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.PaymentType, Contrato), EmployeeHelper.PaymentType.Where(Function(i) i.Item2 = con.PaymentType).FirstOrDefault.Item3.ToString, EmployeeHelper.PaymentType.Where(Function(i) i.Item2 = _currentContract.PaymentType).FirstOrDefault.Item3.ToString))
        'concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.TrialPeriod, Contrato), con.TrialPeriod.ToString, _currentContract.TrialPeriod.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.TrialPeriodTime, Contrato), con.TrialPeriodTime.ToString, _currentContract.TrialPeriodTime.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.TrialPeriodSalaryPercentage, Contrato), String.Format("{0:##0.00}%", con.TrialPeriodSalaryPercentage), String.Format("{0:##0.00}%", _currentContract.TrialPeriodSalaryPercentage)))
        'concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.AutoRenew, Contrato), EmployeeHelper.YesNoBoolean.Where(Function(i) i.Item2 = con.AutoRenew).FirstOrDefault.Item3.ToString, EmployeeHelper.YesNoBoolean.Where(Function(i) i.Item2 = _currentContract.AutoRenew).FirstOrDefault.Item3.ToString))
        '----------------------------------------
        'concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.Bank, Contrato), con.Bank.ToString, _currentContract.Bank.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.BankAccountNumber, Contrato), con.BankAccountNumber.ToString, _currentContract.BankAccountNumber.ToString))
        concepts.Add(New Tuple(Of String, String, String)(obtenerRecurso(Eresources.BankAccountType, Contrato), EmployeeHelper.BankAccountType.Where(Function(i) i.Item2 = con.BankAccountType).FirstOrDefault.Item3.ToString, EmployeeHelper.BankAccountType.Where(Function(i) i.Item2 = _currentContract.BankAccountType).FirstOrDefault.Item3.ToString))

        INDgrdContractCompare.DataSource = concepts
        INDgrdContractCompare.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Limpia los controles que se encuentran dentro del popup de terminacion de contratos
    ''' </summary>
    Private Sub CleanFinishContractPopupControls()
        INDsleActualRetirementReason.EditValue = Nothing
        INDdteActualRetirementDate.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que valida que los campos necesarios para la terminacion de contrato hayan sido dilegenciados
    ''' </summary>
    Private Function ValidateRetirementControls() As Boolean
        ValidateRetirementControls = True
        'si no tiene razon de retiro 
        If INDsleActualRetirementReason.EditValue Is Nothing Then
            ValidateRetirementControls = False
            Exit Function
        End If

        'si no tiene fecha de retiro 
        If INDdteActualRetirementDate.EditValue Is Nothing Then
            ValidateRetirementControls = False
            Exit Function
        End If
    End Function
#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que indica que se inicializo un nuevo contrato, para que se envien los datos basicos del empleado por medio del metodo <seealso cref="GetEmployeeOnEditing">GetEmployeeOnEditing</seealso>
    ''' </summary>
    Public Event OnNewContractInitialized As EventHandler

    ''' <summary>
    ''' Metodo que inicializa lo necesario para que el control trabaje
    ''' </summary>
    Private Sub CtrContractViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If indigo.IndigoCompanyType = "2" Then
            INDlciResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciCertificateOfficeNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciPosesionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        If IsDesignMode = False Then
            Presenter = New PCtrContractViewer(Me)
            Presenter.Initializes()
        End If

    End Sub

    ''' <summary>
    ''' Evento que muestra el grupo de mas informacion del contrato
    ''' </summary>
    Private Sub INDlblShowMoreInfo_Click(sender As Object, e As EventArgs) Handles INDlblShowMoreInfo.Click
        If INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then

            'Cierra el grupo de historico de contratos si se encuentra abierto
            If INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Reposiciona INDlcgHistoryChanges debajo de INDlcgContractHistory sin espacio entre ellos
    ''' </summary>
    Private Sub RepositionHistoryChanges()
        If INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso
           INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Try
                ' Calcular la posición Y basándose en la posición y altura de INDlcgContractHistory
                ' Sin espacio adicional, directamente debajo (sin padding ni spacing)
                Dim contractHistoryBottom As Integer = INDlcgContractHistory.Location.Y + INDlcgContractHistory.Size.Height

                ' Reposicionar INDlcgHistoryChanges para que esté exactamente debajo sin ningún espacio
                INDlcgHistoryChanges.Location = New System.Drawing.Point(INDlcgContractHistory.Location.X, contractHistoryBottom)

                ' Asegurar que ambos grupos tengan el mismo tamaño proporcional
                INDlcgContractHistory.Size = New System.Drawing.Size(INDlcgContractHistory.Size.Width, INDlcgContractHistory.Size.Height)
                INDlcgHistoryChanges.Size = New System.Drawing.Size(INDlcgContractHistory.Size.Width, INDlcgContractHistory.Size.Height)
            Catch ex As Exception
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que muestra el grupo de historico de contratos
    ''' </summary>
    Private Sub INDlblShowContractHistory_Click(sender As Object, e As EventArgs) Handles INDlblShowContractHistory.Click
        If INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            ' Ocultar ambos grupos juntos
            INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            'Cierra el grupo de mas informacion si se encuentra abierto
            If INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            LoadHistoryChanges()
            RepositionHistoryChanges()
        End If

    End Sub

    ''' <summary>
    ''' Evento que cambia el texto a mostrar del tipo de registro, tambien basado en el tipo de registro da color a la fila
    ''' </summary>
    ''' <remarks>
    ''' ANTES: Solo mostraba "Contrato Base" o "Novedad"
    ''' DESPUÉS: Ahora también muestra "Novedad extemporánea" cuando IsExtemporaneousChange = True
    ''' </remarks>
    Private Sub INDrepRowType_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepRowType.CustomDisplayText
        If e.Value = 1 Then
            e.DisplayText = obtenerRecurso(Eresources.ContratoBase, Contrato)
        Else
            e.DisplayText = obtenerRecurso(Eresources.Novedad, Contrato)
            ' Es una novedad (RowType = 2), verificar si es extemporánea
            ' Nota: Para mostrar "Novedad extemporánea" necesitamos acceso al contrato completo
            ' Esto se maneja mejor en LoadInfoInUI y LoadInfoInCompleteContract
        End If
    End Sub

    ''' <summary>
    ''' Evento que cambia el texto a mostrar en el repositorio de accion de la rejilla de historico de contratos
    ''' </summary>
    Private Sub INDrepContractHistoryMoreAction_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDrepContractHistoryMoreAction.CustomDisplayText, INDrepContractHistoryMoreActionPop.CustomDisplayText
        e.DisplayText = obtenerRecurso(Eresources.Mas, Contrato) + "..."
    End Sub

    ''' <summary>
    ''' Metodo que muestra la rejilla de comparacion de contratos cuando se da click en el boton
    ''' </summary>
    Private Sub INDlblCompareAction_Click(sender As Object, e As EventArgs) Handles INDlblCompareAction.Click

        If INDlcgContractCompare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            INDlcgContractCompare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgOldContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDgrdContractCompare.Focus()
        Else
            INDlcgContractCompare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgOldContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

    End Sub

    ''' <summary>
    ''' Metodo que envia la informacion completa del contrato que se selecciono de la rejilla para que se muestre en el popup de informacion de contrato
    ''' </summary>
    Private Sub INDrepContractHistoryMoreActionPop_Click(sender As Object, e As System.EventArgs) Handles INDrepContractHistoryMoreActionPop.Click
        'Carga los datos en el visualizador
        Dim toView = CType(INDgrdContractHistory.DefaultView.GetRow(CType(INDgrdContractHistory.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), Contract)
        LoadInfoInCompleteContract(toView)
    End Sub

    ''' <summary>
    ''' Metodo que oculta el comparador de contratos y muestra la informacion del contrato colocandolos en su posicion por defecto
    ''' </summary>
    Private Sub INDrepContractHistoryMoreActionPop_Closed(sender As Object, e As System.EventArgs) Handles INDrepContractHistoryMoreActionPop.Closed
        'Reinicia la posicion de la rejilla de la comparacion
        INDlcgContractCompare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlcgOldContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Metodo que muestra el popup de contrato nuevo para poder registrar un nuevo contrato
    ''' </summary>
    Private Sub INDbbiAddNewContract_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiAddNewContract.ItemClick
        INDbtnContractViewerActions.HideDropDown()
        'ejecuta el evento de inicializacion de nuevo contrato
        RaiseEvent OnNewContractInitialized(Me, EventArgs.Empty)

        If Valid Then
            'INDbtnContractViewerActions.ShowDropDown()
            Using formulario As New FrmContractEdit(_employee)
                AddHandler formulario.OnContractAdded, AddressOf OnContractAddedEventHandler
                formulario.Size = New System.Drawing.Size(1024, 780)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.CurrencyAbbreviation = CurrencyAbbreviation
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        INDbtnContractViewerActions.DropDownControl = INDppmContractViewerActions
        RemoveHandler INDctrContractEdit.OnContractAdded, AddressOf OnContractAddedEventHandler
    End Sub

    ''' <summary>
    ''' Maneja los datos devueltos por el formulario de contratos
    ''' </summary>
    Private Sub OnContractAddedEventHandler(sender As Object, e As OnContractAddedEventArgs)
        INDbtnContractViewerActions.HideDropDown()
        ShowLatestOrActualContract()
    End Sub

    ''' <summary>
    ''' Agrega detalle a la entidad
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub ActionExecuted(ActionExecuted)
        INDbtnContractViewerActions.DropDownControl = INDppmContractViewerActions

        If ActionExecuted = False And _currentContract.Id > 0 Then
            '_currentContract.MarkAsUnchanged()
            For Each item As FundContract In _currentContract.FundContract
                item.MarkAsUnchanged()
            Next
        End If
        RemoveHandler INDCtrContractModify.OnContractAdded, AddressOf OnContractAddedEventHandler
    End Sub

    ''' <summary>
    ''' Metodo que muestra el popup de Terminacion de contrato para poder terminar el contrato
    ''' </summary>
    Private Sub INDbbiFinishContract_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiFinishContract.ItemClick
        'Establecer la fecha máxima permitida como la fecha de terminación del contrato
        If _currentContract IsNot Nothing Then
            INDdteActualRetirementDate.Properties.MaxValue = _currentContract.ContractEndingDate
        Else
            'Si no hay contrato usar la fecha de hoy como máximo
            INDdteActualRetirementDate.Properties.MaxValue = Date.Today
        End If
        
        INDbtnContractViewerActions.DropDownControl = INDpccFinishContract
        INDbtnContractViewerActions.ShowDropDown()
    End Sub

    ''' <summary>
    ''' Metodo que retorna el boton de acciones del formulario a sus valores por defecto una vez se cierra el popup de terminacion de contrato
    ''' </summary>
    Private Sub INDpccFinishContract_CloseUp(sender As Object, e As EventArgs) Handles INDpccFinishContract.CloseUp
        INDbtnContractViewerActions.DropDownControl = INDppmContractViewerActions
        CleanFinishContractPopupControls()
        'INDbtnContractViewerActions.Text = "Acciones"
    End Sub

    ''' <summary>
    ''' Metodo que retorna el boton de acciones del formulario a sus valores por defecto una vez se cierra el popup de terminacion de contrato
    ''' </summary>
    Private Sub INDpccFinishContract_GotFocus(sender As Object, e As EventArgs) Handles INDpccFinishContract.GotFocus
        INDsleActualRetirementReason.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que actualiza los datos de terminacion del contrato actual
    ''' </summary>
    Private Sub INDbtnConfirmRetirement_Click(sender As Object, e As EventArgs) Handles INDbtnConfirmRetirement.Click
        'valida que se hayan llenado los campos necesarios
        If ValidateRetirementControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        _currentContract.Status = 5
        _currentContract.Valid = 1

        'Asigno los valores al contrato actual
        _currentContract.RetirementDate = INDdteActualRetirementDate.EditValue
        _currentContract.RetirementReasonId = INDsleActualRetirementReason.EditValue

        If _currentContract.ContractType.ContractClass = 2 Or _currentContract.ContractType.ContractClass = 5 Then
            If _currentContract.LastLiquidationDate >= _currentContract.ContractEndingDate AndAlso _currentContract.Status = 5 AndAlso _currentContract.Valid = True Then
                _currentContract.Status = 2
                _currentContract.Valid = 0
            End If
        End If

        _currentContract.ModificationDate = Date.Now()
        _currentContract.ModificationUserId = indigo.UserIndigoId
        _currentContract.LastModificationDate = Date.Now()

        'muestro los nuevos datos en el visor
        INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciRetirementReason.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlciRetirementDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        INDlblRetirementReason.Text = INDsleActualRetirementReason.Text.ToString
        INDlblRetirementReason.ForeColor = Drawing.Color.YellowGreen
        INDlblRetirementReason.Focus()
        INDlblRetirementDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", INDdteActualRetirementDate.EditValue)
        INDlblRetirementDate.ForeColor = Drawing.Color.YellowGreen

        'Cierra el control
        INDbtnContractViewerActions.HideDropDown()

    End Sub

    ''' <summary>
    ''' Metodo que muestra el popup de Modificacion de contrato para poder modificar la contratacion
    ''' </summary>
    Private Async Sub INDbbiRenewContract_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiRenewContract.ItemClick
        Dim os = False
        If MessageIndigo.Show(String.Format(obtenerRecurso(ContratosCrearNuevo, Contrato), _currentContract.Id.ToString), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            os = True
        Else
            os = False
        End If

        Dim confirmedLiq = Await Presenter.CheckConfirmedLiquidationByContractId(_currentContract.Id)

        If confirmedLiq = False Then
            Mensaje(EeventViewerImages.Advertencia) = "Usted realizará una modificación de un Contrato que no ha sido nunca liquidado. Le recomendamos eliminarlo y volverlo a crear con las modificaciones pertinentes, para evitar errores de proceso y creacíón de contratos erróneos"
        End If
        INDbtnContractViewerActions.HideDropDown()

        'ejecuta el evento de inicializacion de nuevo contrato
        RaiseEvent OnNewContractInitialized(Me, EventArgs.Empty)

        If Valid Then
            INDbtnContractViewerActions.ShowDropDown()
            Using formulario As New FrmContracModify(_employee, _currentContract.Id, ContractActions.EditContract)
                formulario.ContractModification = os
                AddHandler formulario.OnContractAdded, AddressOf OnContractAddedEventHandler
                AddHandler formulario.OnAddContractModify, AddressOf ActionExecuted
                formulario.Size = New System.Drawing.Size(1024, 780)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.CurrencyAbbreviation = CurrencyAbbreviation
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
            INDbtnContractViewerActions.DropDownControl = INDppmContractViewerActions
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el foco al control de acciones cuando recibe el foco el control
    ''' </summary>
    Private Sub CtrContractViewer_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        INDbtnContractViewerActions.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el contrato que se encuentra como actual, siempre que no tenga nominas confirmadas
    ''' </summary>
    Private Async Sub INDbbiDeleteContract_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiDeleteContract.ItemClick
        Dim msj As String = String.Empty

        Dim cn = Await Presenter.CheckConfirmedLiquidationByContractId(_currentContract.Id)
        If cn = False Then

            Dim sd = Await Presenter.CheckSchedulesByContractId(_currentContract.Id)

            If sd = True Then
                msj += obtenerRecurso(ContratosConCuadroTurnos, Contrato)
            End If

            msj += String.Format(obtenerRecurso(ContratosEliminarAdvertencia, Contrato), _currentContract.Id.ToString)

            If MessageIndigo.Show(msj, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim liqDel = Await Presenter.DeleteNotConfirmedLiquidations(_currentContract.Id)

                While _currentContract.FundContract.Count > 0
                    _currentContract.FundContract(_currentContract.FundContract.Count - 1).MarkAsDeleted()
                End While
                _currentContract.MarkAsDeleted()
                ShowLatestOrActualContract()

            End If
        Else
            'El contrato no se puede eliminar xq tiene nominas confirmadas
            Mensaje(EeventViewerImages.Informacion) = String.Format(obtenerRecurso(ContratosConNominaPagada, Contrato), _currentContract.Id.ToString)
        End If
    End Sub

    Private Sub xxx() Handles INDpccContractEdit.GotFocus
        INDpccContractEdit.Controls(0).Focus()
    End Sub

    Private Sub INDbbiActivateContract_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiActivateContract.ItemClick
        _currentContract.RetirementDate = Nothing
        _currentContract.RetirementReasonId = Nothing
        _currentContract.Valid = 1
        _currentContract.Status = 1
        _currentContract.ModificationDate = Date.Now()
        _currentContract.ModificationUserId = indigo.UserIndigoId
        _currentContract.LastModificationDate = Date.Now()

        INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciRetirementReason.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlciRetirementDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

#End Region

End Class
