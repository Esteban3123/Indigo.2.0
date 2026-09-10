'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 05-03-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.XtraEditors
Imports Presentation.Payroll.MVP
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities.Service
Imports System.Text.RegularExpressions
#End Region

''' <summary>
''' Clase que maneja el frontal de conceptos manuales
''' </summary>
''' <remarks></remarks>
Public Class FrmManualConcept
    Implements IManualConcept, ICustomizableForm

#Region "Globals Variables"
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
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para controlar el presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PManualConcept

    ''' <summary>
    ''' Variable que controla el modelo del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MManualConcept

    ''' <summary>
    ''' Variable para controlar el bloqueo de registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim BlockRecord As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable que contiene el concepto manual
    ''' </summary>
    ''' <remarks></remarks>
    Dim ManualConcept As ManualConcepts

    ''' <summary>
    ''' Variable para controlar el contrato del empleado que se va a manejar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractValid As Domain.Payroll.Entities.Contract

    ''' <summary>
    ''' variable para saber el modo de busqueda del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModoBusqueda As Boolean = False

    ''' <summary>
    ''' Variable para saber si estoy 
    ''' </summary>
    ''' <remarks></remarks>
    Dim ConceptFlag As String

    Dim FlagEnter = False

    Dim Time = 10

    Dim RetroactiveMode As Boolean = False

    Dim ModelPayrollSettings As MPayrollSettings

    Dim PayrollSettings As PayrollSettings

    Dim ListConcept As List(Of Concept)

    Dim ObjConceptClass As String

    Dim retentionConcept As Domain.Entities.RetentionConcepts

    Dim CompanySettings As Domain.Entities.CompanySettings

#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene lista de formas de pago en forma de una tupla(enum, idBd, descripcion)
    ''' </summary>
    Private Shared _paidFormatDatasource As List(Of Tuple(Of Integer, Integer, String))
    Public Shared ReadOnly Property PaidFormatDatasource(ByVal GroupTypeLiquidation As EGroupLiquidationType) As List(Of Tuple(Of Integer, Integer, String))
        Get
            _paidFormatDatasource = New List(Of Tuple(Of Integer, Integer, String))()
            If GroupTypeLiquidation = 0 Then
                _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoPrimeraQuincena, 1, obtenerRecurso(Eresources.FormPagoPrimeraQuincena, Eform.ConceptosManuales)))
                _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoSegundaQuincena, 2, obtenerRecurso(Eresources.FormPagoSegundaQuincena, Eform.ConceptosManuales)))
                _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoAmbas, 3, obtenerRecurso(Eresources.FormPagoAmbas, Eform.ConceptosManuales)))
                _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoMensual, 4, obtenerRecurso(Eresources.FormPagoMensual, Eform.ConceptosManuales)))
            Else
                If GroupTypeLiquidation = EGroupLiquidationType.Fortnightly Then
                    _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoPrimeraQuincena, 1, obtenerRecurso(Eresources.FormPagoPrimeraQuincena, Eform.ConceptosManuales)))
                    _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoSegundaQuincena, 2, obtenerRecurso(Eresources.FormPagoSegundaQuincena, Eform.ConceptosManuales)))
                    _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoAmbas, 3, obtenerRecurso(Eresources.FormPagoAmbas, Eform.ConceptosManuales)))

                ElseIf GroupTypeLiquidation = EGroupLiquidationType.Monthly Then
                    _paidFormatDatasource.Add(New Tuple(Of Integer, Integer, String)(Eresources.FormPagoMensual, 4, obtenerRecurso(Eresources.FormPagoMensual, Eform.ConceptosManuales)))
                End If
            End If

            Return _paidFormatDatasource
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource de los empleados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Employee_Datasource As Object Implements IManualConcept.Employee_Datasource
        Set(value As Object)
            INDSleEmployee.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Concept_Datasource As Object Implements IManualConcept.Concept_Datasource
        Set(value As Object)
            ListConcept = value
            INDgleConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property RetentionConcept_Datasource As Object Implements IManualConcept.RetentionConcept_Datasource
        Set(value As Object)
            INDSleRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece la accion que se realiza sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDtxtConsecutive.Enabled = Not value
            INDSleEmployee.Enabled = value
            INDgleConcept.Enabled = value
            INDrgPaidEndContract.Enabled = value
            INDtxtQuoteNumber.Enabled = value
            INDtxtQuoteValue.Enabled = value
            INDtxtHour.Enabled = value
            INDGlePaidFormat.Enabled = value
            INDmeDescription.Enabled = value
            INDgcManualConcepts.Enabled = value
            INDdeInitialDate.Enabled = value
            INDSlProcess.Enabled = value
            INDDnYearMonth.Enabled = value
            INDSleRetentionConcept.Enabled = value
            INDTxtRetentionBase.Enabled = value
            If value = True Then
                INDSlProcess.Focus()
            Else
                INDtxtConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingRetroactive As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingRetroactive As List(Of Tuple(Of Integer, String))
        Get
            If _FillingRetroactive Is Nothing Then
                _FillingRetroactive = New List(Of Tuple(Of Integer, String))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(1, "Nómina"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(2, "Retroactivo"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(3, "Prima de Junio - Servicios"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(4, "Prima de Diciembre - Navidad"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(5, "Liquidación de Contrato"))
                _FillingRetroactive.Add(New Tuple(Of Integer, String)(6, "Vacaciones"))
            End If
            Return _FillingRetroactive
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para obtener/establecer el valor de la cuota
    ''' </summary>
    Private Property QuoteValue As Decimal
        Get
            If INDtxtQuoteValue.EditValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(INDtxtQuoteValue.EditValue.ToString()) Then
                Dim result As Decimal = 0
                If Decimal.TryParse(INDtxtQuoteValue.EditValue.ToString(), result) Then
                    Return result
                End If
            End If
            Return 0
        End Get
        Set(value As Decimal)
            INDtxtQuoteValue.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = CByte(1), .StatusName = obtenerRecurso(EstadoActivo, Eform.ConceptosManuales), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(2), .StatusName = obtenerRecurso(EstadoCompletado, Eform.ConceptosManuales), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(3), .StatusName = obtenerRecurso(EstadoSuspendido, Eform.ConceptosManuales), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
        Me.BarraBotones.StatusRecordEnabled = False
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmManualConceptsMetaData, Eform.InfoMetaData), Me.ManualConcept.Consecutive, Me.INDSleEmployee.Text, Me.INDgleConcept.Text, Me.INDmeDescription.Text),
                                                .CreationDate = dateServer,
                                                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName,
                                                .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me.ManualConcept.Consecutive & "#$",
                                                .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmManualConceptsMetaDataTitle, Eform.InfoMetaData), Me.ManualConcept.Consecutive),
                                                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmManualConceptsMetaData, Eform.InfoMetaData), Me.ManualConcept.Consecutive, Me.INDSleEmployee.Text, Me.INDgleConcept.Text, Me.INDmeDescription.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmManualConceptsMetaDataTitle, Eform.InfoMetaData), Me.ManualConcept.Consecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        ContractValid = Nothing
        ManualConcept = Nothing
        Me.ActionsOnControls = False
        DeleteBlockedRecord()
        'INDlyManualConcept.BeginUpdate()
        INDtxtConsecutive.Text = String.Empty
        INDSleEmployee.EditValue = Nothing
        INDSleEmployee.Properties.NullText = String.Empty
        INDtxtCodeContract.Text = String.Empty
        INDtxtCompany.Text = String.Empty
        INDtxtBranchOffice.Text = String.Empty
        INDtxtFunctionalUnit.Text = String.Empty
        INDtxtCostCenter.Text = String.Empty
        INDtxtPosition.Text = String.Empty
        INDtxtSalary.Text = String.Empty
        INDgleConcept.EditValue = Nothing
        INDgleConcept.Text = String.Empty
        INDdeInitialDate.EditValue = Nothing
        INDrgPaidEndContract.SelectedIndex = -1
        INDtxtQuoteNumber.EditValue = 1
        INDtxtHour.Text = String.Empty
        INDGlePaidFormat.EditValue = Nothing
        INDGlePaidFormat.Text = String.Empty
        INDmeDescription.Text = String.Empty
        INDtxtGroup.Text = String.Empty
        INDtxtTotal.Text = String.Empty
        INDgcManualConcepts.DataSource = Nothing
        INDSlProcess.EditValue = 0
        QuoteValue = 0
        RetroactiveMode = False
        INDSleRetentionConcept.EditValue = Nothing
        INDTxtRetentionBase.EditValue = Nothing
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.StatusRecordVisible = False
        'INDlyManualConcept.EndUpdate()

        INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyItemQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyItemHourValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        Me.ActionsOnControls = False
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If BlockRecord IsNot Nothing AndAlso BlockRecord.Id > 0 AndAlso BlockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MManualConcept
                Await model.DeleteBlockRecord(BlockRecord)
            End Using
            BlockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Cargar Controles en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
        Using model As New MManualConcept
            AsyncLoader(True)
            ManualConcept = Await model.GetManualConcepts(CInt(Me.INDtxtConsecutive.Text))
            AsyncLoader(False)
            If ManualConcept Is Nothing Then
                PrepareToSave()
            Else
                If ManualConcept.Id > 0 Then
                    INDGlePaidFormat.Properties.DataSource = PaidFormatDatasource(0) ' lleno el datasource de tipo de pago
                    Dim result = Await model.GetBlockRecord(Me.Tag, ManualConcept.Id)
                    With ManualConcept
                        Me.BarraBotones.RibbonPagEform.Visible = False
                        If .State = EStatusManualConceps.Active Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySuspend)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                        End If
                        INDtxtConsecutive.Text = .Consecutive
                        INDSlProcess.EditValue = .Process
                        INDSleEmployee.Properties.NullText = .Contract.Employee.ThirdParty.Name
                        INDSleEmployee.EditValue = .EmployeeId
                        INDtxtCodeContract.Text = .ContractNumber
                        INDtxtCompany.Text = .Group.Company.Name
                        INDtxtGroup.Text = .Group.Name
                        INDtxtBranchOffice.Text = .BranchOffice.Name
                        INDtxtFunctionalUnit.Text = .FunctionalUnit.Name
                        INDtxtCostCenter.Text = .CostCenter.Name
                        INDtxtPosition.Text = .Contract.Position.Name
                        INDtxtSalary.Text = .Contract.BasicSalary
                        INDgleConcept.EditValue = .ConceptId
                        INDdeInitialDate.EditValue = CDate(.InitialDate)
                        INDrgPaidEndContract.EditValue = .PaidEndContract
                        INDtxtQuoteNumber.EditValue = .QuoteNumber
                        INDSleRetentionConcept.EditValue = .RetentionId
                        INDTxtRetentionBase.EditValue = .RetentionBase
                        INDtxtQuoteValue.Text = .QuoteValue
                        If .PaidEndContract = False Then
                            INDtxtTotal.Text = .QuoteNumber * .QuoteValue
                        End If
                        INDGlePaidFormat.EditValue = .PaidFormat
                        INDmeDescription.Text = .Description
                        INDgcManualConcepts.DataSource = .ManualConceptsDetail
                        Me.BarraBotones.StatusRecord = .State
                        If ConceptFlag = "Hour" Then
                            INDtxtHour.EditValue = .QuoteValue
                            INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Else
                            INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        End If
                    End With
                    ModeSuspendForm()
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.ManualConcept.Consecutive)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(ManualConcept.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        BlockRecord = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ManualConcept.Id}
                        Dim operation = Await model.SaveBlockRecord(BlockRecord)
                        BlockRecord = operation.ObjectEmbbeded
                    Else
                        If Not (Me.BlockRecord IsNot Nothing AndAlso Me.BlockRecord.CodUser = Me.indigo.UserIndigo AndAlso Me.BlockRecord.IdRecord = Me.ManualConcept.Id) Then
                            BlockRecord = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                    End If
                Else
                    If INDtxtConsecutive.Text = String.Empty Then
                        PrepareToSave()
                    Else
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesNoHayRegistros, Eform.Comunes)
                        PrepareToSave()
                    End If
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para suspender un concepto manual
    ''' </summary>
    ''' <remarks></remarks>
    Sub SuspendManualConcept()
        If ManualConcept IsNot Nothing AndAlso ManualConcept.Id > 0 Then
            ''''''''''''Cambiar recurso
            If MessageIndigo.Show(obtenerRecurso(DeseaSuspender, ConceptosManuales), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If ManualConcept.State <> EStatusManualConceps.Suspended Then
                    ManualConcept.State = EStatusManualConceps.Suspended
                    Guardar()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para asignar valores antes de guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignValues()
        Try
            Dim VarInitialDate As Date
            Dim VarDescription As String
            Dim VarPaidEndContract As Boolean
            Dim VarPaidFormat As Byte
            Dim VarQuoteNumber As Byte
            Dim VarQuoteValue As Decimal

            If RetroactiveMode = False Then
                VarInitialDate = INDdeInitialDate.EditValue
                VarDescription = INDmeDescription.Text
                If ConceptFlag = "Money" Then
                    VarPaidEndContract = INDrgPaidEndContract.EditValue
                    VarPaidFormat = INDGlePaidFormat.EditValue
                    VarQuoteNumber = INDtxtQuoteNumber.EditValue
                    VarQuoteValue = INDtxtQuoteValue.EditValue

                Else
                    VarPaidEndContract = False
                    VarPaidFormat = INDGlePaidFormat.EditValue
                    VarQuoteNumber = 1
                    VarQuoteValue = Convert.ToDecimal(INDtxtHour.EditValue, Globalization.CultureInfo.InvariantCulture)
                End If

            Else
                VarPaidEndContract = False
                INDGlePaidFormat.EditValue = 4
                VarPaidFormat = 4
                VarQuoteNumber = 1
                VarQuoteValue = INDtxtQuoteValue.EditValue
                VarInitialDate = New Date(INDDnYearMonth.GetYear, 1, 1)
                VarDescription = "Concepto para Retroactivo"
                If INDSlProcess.EditValue = 3 Or INDSlProcess.EditValue = 4 Then
                    VarPaidEndContract = False
                    INDGlePaidFormat.EditValue = 4
                    VarPaidFormat = 4
                    VarQuoteNumber = 1
                    VarQuoteValue = INDtxtQuoteValue.EditValue
                    If INDSlProcess.EditValue = 3 Then
                        If PayrollSettings.InitialDateServicesIncentivePayment.ToString() = String.Empty Then
                            Mensaje(EeventViewerImages.Advertencia) = "Verifique la Fecha de Inicio de la Prima de Servicios - Junio"
                            Return
                        End If
                        VarInitialDate = PayrollSettings.InitialDateServicesIncentivePayment
                        VarDescription = "Concepto para Pago x Primas (Junio - Servicios)"
                    Else
                        If PayrollSettings.InitialDateServicesIncentivePayment.ToString() = String.Empty Then
                            Mensaje(EeventViewerImages.Advertencia) = "Verifique la Fecha de Inicio de la Prima de Navidad - Diciembre"
                            Return
                        End If
                        VarInitialDate = PayrollSettings.InitialDateChristmasIncentivePayment
                        VarDescription = "Concepto para Pago x Primas (Diciembre - Navidad)"
                    End If
                End If
                If INDSlProcess.EditValue = 5 Then
                    VarPaidEndContract = False
                    INDGlePaidFormat.EditValue = 4
                    VarPaidFormat = 4
                    VarQuoteNumber = 1
                    VarQuoteValue = INDtxtQuoteValue.EditValue
                    VarInitialDate = INDdeInitialDate.EditValue
                    VarDescription = "Concepto para Pago x Liq. de Contrato"
                End If
            End If



            With ManualConcept
                .Process = INDSlProcess.EditValue
                .ConceptId = INDgleConcept.EditValue
                .BranchOfficeId = ContractValid.FunctionalUnit.BranchOfficeId
                .EmployeeId = ContractValid.EmployeeId
                .ContractId = ContractValid.Id
                .ContractNumber = ContractValid.InitialContractNumber
                .CostCenterId = ContractValid.Employee.CostCenterId
                .FunctionalUnitId = ContractValid.FunctionalUnitId
                .GroupId = ContractValid.GroupId

                .InitialDate = VarInitialDate
                .Description = VarDescription
                .PaidEndContract = VarPaidEndContract
                .PaidFormat = VarPaidFormat
                .QuoteNumber = VarQuoteNumber
                If {"020", "048"}.Contains(ObjConceptClass) AndAlso retentionConcept IsNot Nothing Then
                    .RetentionId = INDSleRetentionConcept.EditValue
                    .RetentionBase = INDTxtRetentionBase.EditValue
                    .RetentionPercentage = retentionConcept.Rate
                Else
                    .RetentionId = Nothing
                    .RetentionBase = Nothing
                    .RetentionPercentage = Nothing
                End If
                .QuoteValue = VarQuoteValue
                .State = 1

                If VarPaidEndContract = False Then 'si no va a pagar hasta el final de contrato, entoncs armo los detallados de manual concept
                    .ManualConceptsDetail = ArmManualConceptsDetail(.PaidFormat, .QuoteNumber, .QuoteValue, .InitialDate)
                End If

                If .ManualConceptsDetail.Count > 0 Then
                    'calculamos la fecha del ultimo dia que se va a pagar el concepto manual
                    .PayrollEndingDate = .ManualConceptsDetail.Item(.ManualConceptsDetail.Count - 1).PayrollDateLiquidated
                    Dim TmpInitialDate = New Date(.PayrollEndingDate.Year, .PayrollEndingDate.Month, 1)
                    Dim TmpEndDate = TmpInitialDate.AddMonths(1).AddDays(-1)
                    Select Case VarPaidFormat
                        Case Is = 1 'primera quincena
                            .PayrollEndingDate = New Date(.PayrollEndingDate.Year, .PayrollEndingDate.Month, 15)
                        Case Is = 2 'segunda quincena
                            .PayrollEndingDate = New Date(.PayrollEndingDate.Year, .PayrollEndingDate.Month, DateTime.DaysInMonth(.PayrollEndingDate.Year, .PayrollEndingDate.Month))
                        Case Is = 3 'ambas
                            If .PayrollEndingDate.Day = 1 Then
                                .PayrollEndingDate = New Date(.PayrollEndingDate.Year, .PayrollEndingDate.Month, 15)
                            ElseIf .PayrollEndingDate.Day = 16 Then
                                .PayrollEndingDate = New Date(.PayrollEndingDate.Year, .PayrollEndingDate.Month, DateTime.DaysInMonth(.PayrollEndingDate.Year, .PayrollEndingDate.Month))
                            End If
                        Case Is = 4 ' mensual
                            '.PayrollEndingDate = New Date(.PayrollEndingDate.Year, .PayrollEndingDate.Month, DateTime.DaysInMonth(.PayrollEndingDate.Year, .PayrollEndingDate.Month))
                            .PayrollEndingDate = TmpEndDate
                    End Select
                    'calculamos la fecha inicio de pago de nomina
                    .PayrollInitialDate = .ManualConceptsDetail.Item(0).PayrollDateLiquidated


                Else
                    .PayrollEndingDate = ContractValid.ContractEndingDate 'no se conoce el final del concepto manual

                    'calculamos la fecha inicio de pago de nomina
                    Select Case VarPaidFormat
                        Case Is = 1 ' primera quincena
                            If .InitialDate.Day > 15 Then
                                .PayrollInitialDate = New Date(.InitialDate.Year, .InitialDate.Month + 1, 1)
                            Else
                                .PayrollInitialDate = New Date(.InitialDate.Year, .InitialDate.Month, 1)
                            End If
                        Case Is = 2 'segunda quincena
                            .PayrollInitialDate = New Date(.InitialDate.Year, .InitialDate.Month, 16)
                        Case Is = 3 'ambas
                            If .InitialDate.Day > 15 Then
                                .PayrollInitialDate = New Date(.InitialDate.Year, .InitialDate.Month, 16)
                            Else
                                .PayrollInitialDate = New Date(.InitialDate.Year, .InitialDate.Month, 1)
                            End If
                        Case Is = 4 ' mensual
                            .PayrollInitialDate = New Date(.InitialDate.Year, .InitialDate.Month, 1)
                    End Select
                End If
                'End With

                If INDSlProcess.EditValue = 3 Or INDSlProcess.EditValue = 4 Then
                    If INDSlProcess.EditValue = 3 Then
                        .PayrollEndingDate = PayrollSettings.EndDateServicesIncentivePayment
                    Else
                        .PayrollEndingDate = PayrollSettings.EndDateChristmasIncentivePayment
                    End If
                End If

            End With
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para armar los detallados en caso de que se especifiquen numero de cuotas
    ''' </summary>
    ''' <param name="paidFormat">forma de pago, 1 - primera quincena, 2 - segunda quincena,3 - ambas,4 - mensual </param>
    ''' <param name="quoteNumber">numero de cuotas a pagar</param>
    ''' <param name="dateInitial">fecha incio del concepto manual</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ArmManualConceptsDetail(paidFormat As Integer, quoteNumber As Integer, quoteValue As Decimal, dateInitial As Date) As TrackableCollection(Of Domain.Payroll.Entities.ManualConceptsDetail)
        ArmManualConceptsDetail = New TrackableCollection(Of Domain.Payroll.Entities.ManualConceptsDetail)
        Select Case paidFormat
            'Primera Quincena o Mensual.... Toma el yyyy-mm-01
            Case Is = 1, 4
                Dim _date = New Date(dateInitial.Year, dateInitial.Month, 1)
                If INDGlePaidFormat.EditValue = 1 Then ' si escogen a pagar la primera quincena
                    If dateInitial.Day > 15 Then
                        _date = New Date(dateInitial.Year, (dateInitial.Month + 1), 1)
                    End If
                End If
                For i As Integer = 0 To quoteNumber - 1
                    Dim manualConceptDetail As ManualConceptsDetail = AddManualConceptsDetail(_date, quoteValue)
                    If manualConceptDetail IsNot Nothing Then
                        ArmManualConceptsDetail.Add(AddManualConceptsDetail(_date, quoteValue))
                    Else
                        Exit For
                    End If
                    _date = _date.AddMonths(1)
                Next


                'Segundo Quincena Toma el yyyy-mm-16
            Case Is = 2
                Dim _date As Date
                _date = New Date(dateInitial.Year, (dateInitial.Month), 16)
                For i As Integer = 0 To quoteNumber - 1
                    Dim manualConceptDetail As ManualConceptsDetail = AddManualConceptsDetail(_date, quoteValue)
                    If manualConceptDetail IsNot Nothing Then
                        ArmManualConceptsDetail.Add(AddManualConceptsDetail(_date, quoteValue))
                    Else
                        Exit For
                    End If
                    _date = _date.AddMonths(1)
                Next

                'Ambas
            Case Is = 3
                Dim _date As Date
                If dateInitial.Day < 16 Then
                    _date = New Date(dateInitial.Year, (dateInitial.Month), 1)
                ElseIf dateInitial.Day >= 16 Then
                    _date = New Date(dateInitial.Year, (dateInitial.Month), 16)
                End If
                For i As Integer = 0 To quoteNumber - 1
                    Dim manualConceptDetail As ManualConceptsDetail = AddManualConceptsDetail(_date, quoteValue)
                    If manualConceptDetail IsNot Nothing Then
                        ArmManualConceptsDetail.Add(AddManualConceptsDetail(_date, quoteValue))
                    Else
                        Exit For
                    End If
                    If _date.Day = 16 Then
                        _date = _date.AddMonths(1) ' sumo un mes primero, por si el siguiente mes es de otro año
                        _date = New Date(_date.Year, _date.Month, 1) 'despues convierto la fecha a el primero del mes
                    Else
                        _date = New Date(_date.Year, _date.Month, 16) 'sino , simplemente convierto el dia de ese mes a 16
                    End If
                Next
        End Select
    End Function

    ''' <summary>
    ''' Retorna el objeto detallado de concepto manual, para agragar a listado de la entidad principal
    ''' </summary>
    ''' <param name="_date">fecha que ingresa</param>
    ''' <param name="_value">valor de la cuota</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddManualConceptsDetail(_date As Date, _value As Decimal) As ManualConceptsDetail
        If _date <= ContractValid.ContractEndingDate Then
            Dim newManualConceptsDetail As ManualConceptsDetail = New ManualConceptsDetail()
            With newManualConceptsDetail
                .PayrollDateLiquidated = _date
                .State = 1 ' Esperando pago
                .Value = _value
            End With
            Return newManualConceptsDetail
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Function para validar controles del formulario antes de guardar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateControls() As Boolean
        ValidateControls = True

        If INDSlProcess.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha Seleccionado el Proceso a Afectar"
            Return False
        End If
        If INDSleEmployee.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemEmployee.Text)
            Return False
        End If
        If INDgleConcept.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemConcept.Text)
            Return False
        Else
            'Si la clase de concepto es retencion se debe validar que se haya parametrizado un concepto de retencion y una base
            If {"020", "048"}.Contains(ObjConceptClass) Then
                If INDSleRetentionConcept.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciRetentionConcept.Text)
                    Return False
                End If
                If INDTxtRetentionBase.EditValue Is Nothing OrElse INDTxtRetentionBase.EditValue <= 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciRetentionBase.Text)
                    Return False
                End If
            End If
        End If
        If RetroactiveMode = False Then
            If INDdeInitialDate.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemDateInitial.Text)
                Return False
            End If
            If INDrgPaidEndContract.EditValue = False Then
                If INDtxtQuoteNumber.Text Is String.Empty Or INDtxtQuoteNumber.EditValue < 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemQuoteNumber.Text)
                    Return False
                End If
            End If
            If ConceptFlag = "Money" Then
                If INDtxtQuoteValue.Text Is String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemQuoteValue.Text)
                    Return False
                End If

                If INDGlePaidFormat.EditValue Is Nothing OrElse INDGlePaidFormat.EditValue.ToString() = "" Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemPaidFormat.Text)
                    Return False
                End If

                If INDrgPaidEndContract.SelectedIndex = -1 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemPaidEndContract.Text)
                    Return False
                End If
            End If
            If ConceptFlag = "Hour" Then
                If INDtxtHour.Text Is String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDtxtHour.Text)
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Funcion para validar que el registro se pueda insertar, verificando que no se crucen las fechas con los mismos conceptos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function Validateinsert() As Task(Of Boolean)
        Using model As New MManualConcept
            If ManualConcept.PaidEndContract = True Then
                Dim result = Await model.GetManualConceptsByConceptAndEndContractTrueAsync(ManualConcept.ConceptId, ManualConcept.ContractNumber, INDSlProcess.EditValue)
                If result = True Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(YaTieneEsteConceptoManualHastaFin, ConceptosManuales)
                    Return False
                Else
                    Dim result2 = Await model.GetManualConceptsByConceptAndDate(ManualConcept.ConceptId, ManualConcept.ContractNumber, New List(Of Date), INDSlProcess.EditValue, ManualConcept.PayrollInitialDate.Date)
                    If result2 IsNot Nothing AndAlso result2.Id > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(YaTieneConcManConEsteConcDesdeHasta, ConceptosManuales), result2.PayrollInitialDate.Date, result2.PayrollEndingDate.Date)
                        Return False
                    End If
                End If
            Else
                Dim ListDates As List(Of Date) = New List(Of Date)
                For Each item In ManualConcept.ManualConceptsDetail
                    ListDates.Add(item.PayrollDateLiquidated)
                Next
            End If
        End Using
        Return True
    End Function

    ''' <summary>
    ''' Metodo que prepara los controles para ingresar un nuevo concepto manual
    ''' </summary>
    ''' <remarks></remarks>
    Sub PrepareToSave()
        INDtxtConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
        ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        ManualConcept = New ManualConcepts()
    End Sub

    ''' <summary>
    ''' Metodo que pone el formulario en modo para suspender el concepto manual
    ''' </summary>
    ''' <remarks></remarks>
    Sub ModeSuspendForm()
        INDtxtConsecutive.Enabled = False
        INDSleEmployee.Enabled = False
        INDgleConcept.Enabled = False
        INDrgPaidEndContract.Enabled = False
        INDtxtQuoteNumber.Enabled = False
        INDtxtQuoteValue.Enabled = False
        INDtxtHour.Enabled = False
        INDGlePaidFormat.Enabled = False
        INDmeDescription.Enabled = False
        INDgcManualConcepts.Enabled = True
        INDdeInitialDate.Enabled = False
        INDSleRetentionConcept.Enabled = False
        INDTxtRetentionBase.Enabled = False
    End Sub

    ''' <summary>
    ''' Metodo para calcular el valor total del numero de cuotas y el valor de la cuota
    ''' </summary>
    ''' <remarks></remarks>
    Sub CalculateTotal()
        If INDtxtQuoteNumber.Text <> "" AndAlso INDtxtQuoteValue.Text <> "" Then
            Dim valueText As String = INDtxtQuoteValue.EditValue?.ToString()
            Dim numberText As String = INDtxtQuoteNumber.Text
            Dim quoteValue As Decimal
            Dim quoteNumber As Decimal
            If Decimal.TryParse(valueText, quoteValue) AndAlso Decimal.TryParse(numberText, quoteNumber) Then
                INDtxtTotal.Text = (quoteNumber * quoteValue).ToString()
            End If
        End If
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        presenter = Nothing
        model = Nothing
        BlockRecord = Nothing
        ManualConcept = Nothing
        ContractValid = Nothing
        ModoBusqueda = Nothing
        ConceptFlag = Nothing
        FlagEnter = Nothing
        ModelPayrollSettings = Nothing
        PayrollSettings = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmManualConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloNomina.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        presenter = New PManualConcept(Me)
        SetCurrencyFormat(presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        AsyncLoader(True)
        presenter.ConceptsDatasource()
        presenter.RetentionConceptsDatasource()
        AsyncLoader(False)
        Deshacer()
        Me.INDSlProcess.Properties.DataSource = FillingRetroactive
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        IndigoGridControl1.SetHoldSize(INDgcManualConcepts, True)
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
    ''' <summary>
    ''' Evento Click del boton busqueda del buttonedit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtConsecutive_Properties_Click(sender As Object, e As EventArgs) Handles INDtxtConsecutive.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Evento para controlar cuando se presione enter en el btnedit del consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDtxtConsecutive.Text.ToString) Then
                LoadControls()
                If INDtxtConsecutive.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
            Else
                PrepareToSave()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Evento para cargar el datasource de los empleados cuando se abra el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Properties.DataSource Is Nothing Then
            presenter.EmployeeDatasource()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmEmployee With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmConcepts With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            presenter.ConceptsDatasource()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se elige un empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleEmployee.EditValueChanged
        If INDSleEmployee.Properties.DataSource IsNot Nothing Then
            If INDSleEmployee.EditValue IsNot Nothing Then
                Dim _employee As Employee
                Using modelEmployee As New MEmployee(MEmployee.TAG)
                    AsyncLoader(True)
                    _employee = Await modelEmployee.GetEmployeeByIdAsync(CInt(INDSleEmployee.EditValue))
                    AsyncLoader(False)
                    If _employee IsNot Nothing AndAlso _employee.Id > 0 Then
                        ContractValid = Nothing
                        If _employee.Contract.Count > 0 Then
                            ContractValid = _employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault()
                            If ContractValid IsNot Nothing AndAlso ContractValid.Id > 0 Then
                                'Despues de todas las validaciones, procedemos a llenar los datos de informacion del contrato
                                INDtxtCodeContract.Text = ContractValid.InitialContractNumber
                                INDtxtCompany.Text = ContractValid.Group.Company.Name
                                INDtxtGroup.Text = ContractValid.Group.Name
                                INDtxtBranchOffice.Text = ContractValid.FunctionalUnit.BranchOffice.Name
                                INDtxtFunctionalUnit.Text = ContractValid.FunctionalUnit.Name
                                INDtxtCostCenter.Text = ContractValid.Employee.CostCenter.Name
                                INDtxtPosition.Text = ContractValid.Position.Name
                                INDtxtSalary.Text = ContractValid.BasicSalary
                                If ContractValid.Group.Liquidation = 1 Then ' mensual
                                    INDGlePaidFormat.Properties.DataSource = PaidFormatDatasource(EGroupLiquidationType.Monthly)
                                ElseIf ContractValid.Group.Liquidation = 2 Then 'quincenal
                                    INDGlePaidFormat.Properties.DataSource = PaidFormatDatasource(EGroupLiquidationType.Fortnightly)
                                End If

                                If INDSlProcess.EditValue <> 3 Or INDSlProcess.EditValue <> 4 Then
                                    INDdeInitialDate.Properties.MinValue = ContractValid.JobBondingDate
                                    INDdeInitialDate.Properties.MaxValue = ContractValid.ContractEndingDate
                                End If

                                'Adaptado para Retiros
                                If INDSlProcess.EditValue = 5 Then
                                    INDdeInitialDate.Properties.MinValue = ContractValid.JobBondingDate
                                    INDdeInitialDate.Properties.MaxValue = ContractValid.ContractEndingDate
                                End If
                            End If
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para mostrar u ocultar el campo numero de cuotas, dependiendo de si se paga hasta final de contrato o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgPaidEndContract_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDrgPaidEndContract.SelectedIndexChanged
        If INDrgPaidEndContract.EditValue IsNot Nothing Then
            If INDrgPaidEndContract.EditValue = True Then
                INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento para controlar el display text del estado de la rejilla de detalle de conceptos manuales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbgvManualConceptsDetail_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDbgvManualConceptsDetail.CustomColumnDisplayText
        If e.Column.Name = INDColState.Name Then
            If e.Value = 1 Then
                e.DisplayText = obtenerRecurso(Eresources.EstadoEsperandoPago, Eform.ConceptosManuales)
            ElseIf e.Value = 2 Then
                e.DisplayText = obtenerRecurso(Eresources.EstadoPagado, Eform.ConceptosManuales)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmManualConcepts_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento para controlar el display text de los campos del frm busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridViewBusquedas_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)
        If e.Value IsNot Nothing AndAlso e.Value.GetType.ToString <> "DevExpress.Data.NotLoadedObject" Then
            If e.Column.FieldName = "PaidFormat" Then
                If e.Value = 1 Then
                    e.DisplayText = obtenerRecurso(FormPagoPrimeraQuincena, ConceptosManuales)
                ElseIf e.Value = 2 Then
                    e.DisplayText = obtenerRecurso(FormPagoSegundaQuincena, ConceptosManuales)
                ElseIf e.Value = 3 Then
                    e.DisplayText = obtenerRecurso(FormPagoAmbas, ConceptosManuales)
                ElseIf e.Value = 4 Then
                    e.DisplayText = obtenerRecurso(FormPagoMensual, ConceptosManuales)
                End If
            ElseIf e.Column.FieldName = "PaidEndContract" Then
                If e.Value = 1 Then
                    e.DisplayText = obtenerRecurso(Si)
                ElseIf e.Value = 0 Then
                    e.DisplayText = obtenerRecurso(No)
                End If
            ElseIf e.Column.FieldName = "State" Then
                If e.Value = 1 Then
                    e.DisplayText = obtenerRecurso(EstadoActivo, ConceptosManuales)
                ElseIf e.Value = 2 Then
                    e.DisplayText = obtenerRecurso(EstadoCompletado, ConceptosManuales)
                ElseIf e.Value = 3 Then
                    e.DisplayText = obtenerRecurso(EstadoSuspendido, ConceptosManuales)
                End If
            End If
            If e.Column.FieldName = "QuoteValue" Then
                e.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                e.Column.DisplayFormat.FormatString = "C2"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla la seleccion o cambio de fecha validando que no este ya liquidada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDdeInitialDate_Leave(sender As Object, e As EventArgs) Handles INDdeInitialDate.Leave
        If INDdeInitialDate.EditValue IsNot Nothing AndAlso INDdeInitialDate.Text <> "" Then
            If ContractValid IsNot Nothing AndAlso ContractValid.Id > 0 Then
                If INDSlProcess.EditValue <> 5 Then

                    If CType(INDdeInitialDate.EditValue, Date).Date <= ContractValid.LastLiquidationDate Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(YaTieneNominaLiquidada, ConceptosManuales), ContractValid.LastLiquidationDate)
                        INDdeInitialDate.Text = ""
                        INDdeInitialDate.EditValue = Nothing
                        INDdeInitialDate.Focus()
                        Exit Sub
                    End If

                    If INDgleConcept.EditValue IsNot Nothing AndAlso INDdeInitialDate.Text <> "" Then
                        Using model As New MManualConcept
                            'validar que la fecha inicio no este cruzandose con otro registro
                            Dim listDates = New List(Of Date)
                            listDates.Add(CType(INDdeInitialDate.EditValue, Date).Date)
                            Dim result = Await model.GetManualConceptsByConceptAndDate(INDgleConcept.EditValue, ContractValid.InitialContractNumber, listDates, INDSlProcess.EditValue)
                            If result IsNot Nothing AndAlso result.Id > 0 Then
                                If result.PaidEndContract = True Then
                                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(YaTieneConcManConEsteConcDesdeHastaFinal, ConceptosManuales), result.PayrollInitialDate)
                                End If

                                Exit Sub
                            End If
                        End Using
                    End If
                End If
            Else
                If INDtxtConsecutive.Enabled = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemEmployee.Text)
                    INDdeInitialDate.Text = ""
                    INDdeInitialDate.EditValue = Nothing
                    INDdeInitialDate.Focus()
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Para lanzar el metodo que calcula cada que cambien de numero de cuotas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtQuoteNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtQuoteNumber.EditValueChanged
        CalculateTotal()
    End Sub

    ''' <summary>
    ''' Para lanzar el metodo que calcula cada vez que cambien el valor de la cuota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtQuoteValue_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtQuoteValue.KeyUp
        CalculateTotal()
    End Sub

    ''' <summary>
    ''' Evento para inhabilitar el control de empleado cuando seleccionen uno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_Leave(sender As Object, e As EventArgs) Handles INDSleEmployee.Leave
        If ContractValid IsNot Nothing AndAlso ContractValid.Id > 0 Then
            INDSleEmployee.Enabled = False
            INDgleConcept.EditValue = Nothing
            INDgleConcept.Text = String.Empty
            INDdeInitialDate.EditValue = Nothing
            INDrgPaidEndContract.SelectedIndex = -1
            INDtxtQuoteNumber.EditValue = 1
            INDtxtHour.Text = String.Empty
            INDGlePaidFormat.EditValue = Nothing
            INDGlePaidFormat.Text = String.Empty
            INDmeDescription.Text = String.Empty
            INDtxtTotal.Text = String.Empty
            INDSleRetentionConcept.EditValue = Nothing
            INDTxtRetentionBase.EditValue = Nothing
            INDgcManualConcepts.DataSource = Nothing
        End If
    End Sub
#End Region

#Region "ICRUD Base"
    ''' <summary>
    ''' Icrud Base Buscar 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Icrud Base Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    ''' <summary>
    ''' Icrud Base Eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Icrud Base Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        Try
            INDtxtHour.Enabled = False
            If ManualConcept IsNot Nothing AndAlso ManualConcept.Id = 0 Then
                If ValidateControls() = False Then
                    Exit Sub
                End If
                AssignValues()
                If Await Validateinsert() = False Then
                    Exit Sub
                End If
            End If
            Dim _message As String = ""
            If ManualConcept.PaidEndContract = False AndAlso ManualConcept.QuoteNumber > ManualConcept.ManualConceptsDetail.Count Then
                _message = String.Format(obtenerRecurso(NoSeRegistraronTodasCuotas, ConceptosManuales), ManualConcept.ManualConceptsDetail.Count, ContractValid.ContractEndingDate)
                ManualConcept.QuoteNumber = ManualConcept.ManualConceptsDetail.Count
            Else
                _message = obtenerRecurso(ComunesGuardado)
            End If
            Using model As New MManualConcept()
                AsyncLoader(True)
                Dim result = Await model.SaveManualConcepts(ManualConcept)
                If result.StateResult = True Then
                    INDtxtHour.Enabled = True
                    If ManualConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = _message
                    Else
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    ModoBusqueda = False
                    AsyncLoader(False)
                    Deshacer()
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Else
                    INDtxtHour.Enabled = True
                    AsyncLoader(False)
                    INDtxtConsecutive.Enabled = False
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
        End Try

    End Sub

    ''' <summary>
    ''' Icrud Base Log Btn Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Icrud Base Propiedad del mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Icrud Base Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Icrud Base Abrir Busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Activo", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Finalizado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Suspendido", 3))
        With FormSearchObjects
            FormSearchObjects.Width = 1100
			Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
			_culture.NumberFormat = (presenter.LoadPayrollSettings().CurrencyId.Abbreviation).GetNumberFormat()
			AddHandler CType(.GridViewBusquedas, DevExpress.XtraGrid.Views.Grid.GridView).CustomColumnDisplayText, AddressOf GridViewBusquedas_CustomColumnDisplayText
			.ListaColumnas = {New ColumnInfo With {.Caption = INDlyItemConsecutive.Text, .FieldName = "Consecutive"},
				New ColumnInfo With {.Caption = "Nit", .FieldName = "EmployeeId.ThirdPartyId.Nit"},
				New ColumnInfo With {.Caption = "Empleado", .FieldName = "EmployeeId.ThirdPartyId.Name"},
				New ColumnInfo With {.Caption = "Concepto", .FieldName = "ConceptId.Descripcion"},
				New ColumnInfo With {.Caption = "Valor De Cuota", .FieldName = "QuoteValue", .FormatCulture = _culture},
				New ColumnInfo With {.Caption = "Forma de Pago", .FieldName = "PaidFormat"},
				New ColumnInfo With {.Caption = "Hasta Fin De Contrato", .FieldName = "PaidEndContract"},
				New ColumnInfo With {.Caption = "Descripción", .FieldName = "Description"},
				New ColumnInfo With {.Caption = "Fecha Inicio", .FieldName = "InitialDate"},
				New ColumnInfo() With {.Caption = "Estado", .FieldName = "State", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus}}.ToList()
			.ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListManualConcepts
            .FormParent = Me
            .ShowSearch()
        End With
        ModoBusqueda = True
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDtxtConsecutive.Text = ReturnValue
        DeleteBlockedRecord()
        If INDtxtConsecutive.Text <> String.Empty Then
            LoadControls()
            If INDtxtConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDtxtConsecutive.Enabled = False
        End If
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
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
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

    Private Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender
        SuspendManualConcept()
    End Sub
#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyManualConcept.ShowCustomizationForm()
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
            INDlyManualConcept.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyManualConcept.ShowCustomization
        Try
            'Ejecuatamos la consulta
            AsyncLoader(True)
            Dim dsFields As DataSet = Nothing
            AsyncLoader(False)
            If dsFields IsNot Nothing Then
                dtFieldsCustomizables = dsFields.Tables(0)
                For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                    For j As Integer = 0 To INDlyManualConcept.Items.Count - 1
                        If Object.Equals(INDlyManualConcept.Items.Item(j).Tag, Nothing) = False Then
                            If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyManualConcept.Items.Item(j).Tag.ToString.Trim Then
                                INDlyManualConcept.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyManualConcept.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyManualConcept.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyManualConcept.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyManualConcept.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

    Private Sub INDgleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleConcept.EditValueChanged
        ObjConceptClass = String.Empty
        If Not String.IsNullOrEmpty(INDgleConcept.EditValue) Then
            If ListConcept IsNot Nothing Then
                Dim concept = ListConcept.FirstOrDefault(Function(c) c.Id = INDgleConcept.EditValue)
                If concept IsNot Nothing Then
                    ObjConceptClass = concept.ConceptClass
                End If
            End If
        End If

        If RetroactiveMode = False Then
            If ObjConceptClass = "001" Or ObjConceptClass = "005" Or ObjConceptClass = "012" Or ObjConceptClass = "013" Or ObjConceptClass = "042" Or ObjConceptClass = "043" Or ObjConceptClass = "050" Or ObjConceptClass = "052" Or ObjConceptClass = "051" Then
                ConceptFlag = "Hour"
                INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemHourValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                ConceptFlag = "Money"
                INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemHourValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If

        If INDSlProcess.EditValue = 3 Or INDSlProcess.EditValue = 4 Then
            INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemHourValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            RetroactiveMode = True
        End If

        INDLciRetentionConcept.HideControl(True)
        INDLciRetentionBase.HideControl(True)

        INDrgPaidEndContract.Properties.ReadOnly = False
        INDtxtQuoteNumber.Properties.ReadOnly = False
        INDtxtQuoteValue.Properties.ReadOnly = False
        INDGlePaidFormat.Properties.ReadOnly = False

        INDSleRetentionConcept.EditValue = Nothing
        INDTxtRetentionBase.EditValue = Nothing

        'Si es un concepto de retencion
        If {"020", "048"}.Contains(ObjConceptClass) Then
            INDLciRetentionConcept.HideControl(False)
            INDLciRetentionBase.HideControl(False)

            INDrgPaidEndContract.Properties.ReadOnly = True
            INDtxtQuoteNumber.Properties.ReadOnly = True
            INDtxtQuoteValue.Properties.ReadOnly = True
            'Validación de liquidacion quincenal o mensual para habiliar control
            If ContractValid?.Group?.Liquidation = 2 Then
                INDGlePaidFormat.Properties.ReadOnly = False
            Else
                INDGlePaidFormat.Properties.ReadOnly = True
            End If

            INDrgPaidEndContract.EditValue = True
            INDtxtQuoteNumber.EditValue = 1
            INDtxtQuoteValue.Text = String.Empty
            INDGlePaidFormat.EditValue = 4
        End If
    End Sub

#Region "Retention"

    Private Async Sub INDSleRetentionConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRetentionConcept.EditValueChanged
        If INDSleRetentionConcept.EditValue IsNot Nothing Then
            INDtxtQuoteValue.Text = 0
            Using model As New Accounting.MVP.MRetentionConcept(CStr(Tag))
                retentionConcept = model.GetRetentionByIdSimple(INDSleRetentionConcept.EditValue)
                If retentionConcept IsNot Nothing Then
                    Select Case retentionConcept.Retention
                        Case 2 'rango
                            retentionConcept.Rate = Await CalculatePercentageRange()
                    End Select
                End If
                ValidateCalculateRetention()
            End Using
        End If
    End Sub

    Private Sub INDTxtRetentionBase_Leave(sender As Object, e As EventArgs) Handles INDTxtRetentionBase.Leave
        If ManualConcept Is Nothing OrElse ManualConcept.Id = 0 Then
            ValidateCalculateRetention()
        End If
    End Sub

    ''' <summary>
    ''' Calcula el valor del porcentaje del rango
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function CalculatePercentageRange() As Task(Of Decimal)
        Dim percentageRange As Decimal = 0
        If retentionConcept IsNot Nothing Then
            If retentionConcept.RetentionConceptRanges IsNot Nothing AndAlso retentionConcept.RetentionConceptRanges.Count > 0 Then
                If CompanySettings Is Nothing Then
                    Using model As New Accounting.MVP.MCompanySettings("")
                        CompanySettings = Await model.GetCompanySettings()
                    End Using
                End If

                Dim UVTBaseValue As Decimal
                If CompanySettings IsNot Nothing AndAlso CompanySettings.Id > 0 Then
                    UVTBaseValue = Decimal.Round(INDTxtRetentionBase.EditValue / CompanySettings.UVT, 1)
                Else
                    IdConceptRetention = Nothing
                    Mensaje(EeventViewerImages.Advertencia) = "No existe parámetros de empresa para realizar el cálculo de retenciones."
                    Return 0
                End If

                Dim rcr As Domain.Entities.RetentionConceptRanges = (From r In retentionConcept.RetentionConceptRanges Where UVTBaseValue >= r.ValueInitial AndAlso UVTBaseValue <= r.ValueFinish Select r).FirstOrDefault
                If rcr IsNot Nothing Then
                    percentageRange = rcr.Percentage

                    'Se calcula el porcentaje para poder sacar el valor a cobrar
                    Dim resulCalculateRetention As Decimal = Utils.RoundValue((((UVTBaseValue - rcr.ValueDeducted) * rcr.Percentage / 100) + rcr.UVTIncrement) * CompanySettings.UVT, retentionConcept.TypeRounding)
                    INDtxtQuoteValue.Text = resulCalculateRetention
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El valor base ingresado no existe en los rangos de la retención."
                    percentageRange = 0
                End If
            End If
        End If
        Return percentageRange
    End Function

    ''' <summary>
    ''' Metodo que calcula la retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ValidateCalculateRetention()
        If retentionConcept IsNot Nothing AndAlso INDTxtRetentionBase.EditValue > 0 Then
            If retentionConcept.Retention = 2 Then
                retentionConcept.Rate = Await CalculatePercentageRange()
            Else
                CalculateRetention()
            End If
        End If
        CalculateTotal()
    End Sub

    ''' <summary>
    ''' Calculates the retention.
    ''' </summary>
    Private Sub CalculateRetention()
        Try
            Dim resulCalculateRetention As Decimal = 0
            If retentionConcept.Retention = 1 Then 'base
                resulCalculateRetention = AccountingServices.CalculateRetention(INDTxtRetentionBase.EditValue, retentionConcept)
            ElseIf retentionConcept.Retention = 3 Then 'variable
                If retentionConcept.Rate > 0 Then
                    resulCalculateRetention = AccountingServices.CalculateRetention(INDTxtRetentionBase.EditValue, 0, retentionConcept.Rate)
                End If
            End If
            INDtxtQuoteValue.Text = resulCalculateRetention
        Catch ex As ArgumentNullException
            Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
        Catch ex As ArgumentOutOfRangeException
            Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
        Catch ex As InvalidOperationException
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Catch ex As IndexOutOfRangeException
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

#End Region

    Private Sub INDtxtHour_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtHour.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If FlagEnter = True Then
                TmrDelay.Enabled = False
                FlagEnter = False

                Guardar()
            Else
                FlagEnter = True
                TmrDelay.Enabled = True

            End If
        End If
    End Sub

    Private Sub TmrDelay_Tick(sender As Object, e As EventArgs) Handles TmrDelay.Tick
        FlagEnter = False
        TmrDelay.Enabled = False
    End Sub

    Private Sub INDdeInitialDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdeInitialDate.EditValueChanging
        If ContractValid IsNot Nothing Then
            If ContractValid.Group.Liquidation = 2 And ConceptFlag = "Hour" Then
                If e.NewValue IsNot Nothing Then

                    If (Day(INDdeInitialDate.EditValue) <> 1 And Day(INDdeInitialDate.EditValue) <> 16) Then
                        e.Cancel = True
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub INDdeInitialDate_DrawItem(sender As Object, e As Calendar.CustomDrawDayNumberCellEventArgs) Handles INDdeInitialDate.DrawItem
        If ContractValid IsNot Nothing Then
            If ContractValid.Group.Liquidation = 2 And ConceptFlag = "Hour" Then
                If (e.Date.Day <> 1 And e.Date.Day <> 16) Then
                    e.Style.ForeColor = Color.LightGray
                End If
            End If
        End If
    End Sub

#Region "EditValueChanged"

    Private Sub INDdeInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeInitialDate.EditValueChanged
        If ContractValid IsNot Nothing Then
            If ConceptFlag = "Hour" Then

                If ContractValid.Group.Liquidation = 2 Then
                    If Day(INDdeInitialDate.EditValue) = 1 Then
                        INDGlePaidFormat.EditValue = 1
                    ElseIf Day(INDdeInitialDate.EditValue) = 16 Then
                        INDGlePaidFormat.EditValue = 2
                    End If
                Else
                    INDGlePaidFormat.EditValue = 4
                End If
            End If
        End If
    End Sub

    Private Sub INDSlProcess_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlProcess.EditValueChanged
        If INDSlProcess.EditValue IsNot Nothing Then
            If INDSlProcess.EditValue = 1 Or INDSlProcess.EditValue = 3 Or INDSlProcess.EditValue = 4 Or INDSlProcess.EditValue = 5 Or INDSlProcess.EditValue = 6 Then
                'Nomina
                INDlyItemDateInitial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciYearRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                RetroactiveMode = False
                LoadPayrollSettings()

                If INDSlProcess.EditValue = 3 Or INDSlProcess.EditValue = 4 Or INDSlProcess.EditValue = 5 Then
                    INDdeInitialDate.Properties.ReadOnly = True
                    INDdeInitialDate.Enabled = False
                    INDlyItemHourValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    RetroactiveMode = True

                    If INDSlProcess.EditValue = 5 Then
                        INDdeInitialDate.Properties.ReadOnly = False
                        INDdeInitialDate.Enabled = True
                        INDdeInitialDate.Text = String.Empty
                    End If


                Else
                    INDdeInitialDate.Properties.ReadOnly = False
                    INDdeInitialDate.Enabled = True

                    RetroactiveMode = False
                End If

                If INDSlProcess.EditValue = 5 Then
                    INDlyItemDateInitial.Text = "Fecha Retiro"
                Else
                    INDlyItemDateInitial.Text = "Fecha Inicio"
                End If

            Else
                'Retroactivo
                RetroactiveMode = True
                INDlyItemDateInitial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemHourValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPaidEndContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemQuoteNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPaidFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciYearRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDdeInitialDate.Properties.ReadOnly = False
                INDdeInitialDate.Enabled = True

            End If
        End If
    End Sub
#End Region

#Region "Funciones"
    Private Async Function LoadPayrollSettings() As Task
        Using ModelPayrollSettings As New MPayrollSettings(Me.Tag)
            PayrollSettings = Await ModelPayrollSettings.GetParameters()
        End Using

        If PayrollSettings Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = "Falta Crear los Parámetros de Nómina"
            Return
        End If

        If INDSlProcess.EditValue = 3 Then
            INDdeInitialDate.EditValue = PayrollSettings.InitialDateServicesIncentivePayment
        ElseIf INDSlProcess.EditValue = 4 Then
            INDdeInitialDate.EditValue = PayrollSettings.InitialDateChristmasIncentivePayment
        End If

    End Function
#End Region

End Class


