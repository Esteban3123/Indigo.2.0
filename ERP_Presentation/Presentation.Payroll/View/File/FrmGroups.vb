'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 20-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports System.Drawing
Imports DevExpress.XtraEditors
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports DevExpress.XtraEditors.Design
Imports System.Windows.Forms
Imports DevExpress.Data

#End Region

''' <summary>
''' Manejo del frontal de grupos
''' </summary>
Public Class FrmGroups
    Implements IGroups

    Implements IDataColumnInfo

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
    ''' Variable que contiene la Grupo 
    ''' </summary>
    Dim Group As Group

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MGroups(MyBase.Tag)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PGroups

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que establece el diccionario de descripciones de las variables
    ''' </summary>
    Dim descriptions As Dictionary(Of String, String)

    ''' <summary>
    ''' Variable que contiene el nuevo listado de variables del formulario editor de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly m_columns As New List(Of IDataColumnInfo)()

    ''' <summary>
    ''' Variable para el formulario de edicion de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ExpressionEditForm As ExpressionEditorForm

    ''' <summary>
    ''' Bandera para abrir o no el PopUp las fórmulas
    ''' </summary>
    Dim FlagFormulate As Boolean = False

    Dim ModoBusqueda As Boolean

    Dim ListConceptType As New List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Lista que contiene los indicadores de tarifa especial de pension
    ''' </summary>
    Private ListSpecialPensionRate As New List(Of Tuple(Of Byte, String))

    Public Property DataSourceBranch As List(Of Domain.Entities.GlosasParametersInterface) Implements IGroups.DataSourceBranch
        Get
            Return INDglCompany.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.GlosasParametersInterface))
            INDglCompany.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Establece el datasource del listado de compañias
    ''' </summary>
    Public WriteOnly Property CompaniesDataSource As List(Of Domain.Payroll.Entities.Company) Implements IGroups.CompaniesDataSource
        Set(value As List(Of Domain.Payroll.Entities.Company))
            INDgleCompany.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del id de la compañia
    ''' </summary>
    Public Property CompanyId As String Implements IGroups.CompanyId
        Get
            Return INDgleCompany.EditValue
        End Get
        Set(value As String)
            INDgleCompany.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del codigo del grupo
    ''' </summary>
    Public Property GroupCode As String Implements IGroups.GroupCode
        Get
            Return INDBteCode.Text
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del tipo de liquidacion del grupo
    ''' </summary>
    Public Property GroupLiquidation As String Implements IGroups.GroupLiquidation
        Get
            Return INDrgLiquidation.EditValue
        End Get
        Set(value As String)
            INDrgLiquidation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del nombre del grupo
    ''' </summary>
    Public Property GroupName As String Implements IGroups.GroupName
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del estado del grupo
    ''' </summary>
    Public Property GroupStatus As Boolean Implements IGroups.GroupStatus
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de la fecha de la ultima liquidacion del grupo
    ''' </summary>
    Public Property GroupLastLiquidationDate As Object Implements IGroups.GroupLastLiquidationDate
        Get
            Return INDdeLastLiquidationDate.EditValue
        End Get
        Set(value As Object)
            If value Is Nothing Then
                INDdeLastLiquidationDate.EditValue = value
            Else
                INDdeLastLiquidationDate.EditValue = CType(value, DateTime)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de la proxima fecha de liquidacion del grupo
    ''' </summary>
    Public Property GroupNextLiquidationDate As Object Implements IGroups.GroupNextLiquidationDate
        Get
            Return INDdeNextLiquidationDate.EditValue
        End Get
        Set(value As Object)
            If value Is Nothing Then
                INDdeNextLiquidationDate.EditValue = value
            Else
                INDdeNextLiquidationDate.EditValue = CType(value, DateTime)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del parametro de meses de liquidacion del grupo
    ''' </summary>
    Public Property GroupMonths As String Implements IGroups.GroupMonths
        Get
            Return INDrgMonths.EditValue
        End Get
        Set(value As String)
            INDrgMonths.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del parametro de provisiones del grupo
    ''' </summary>
    Public Property GroupProvisions As Boolean Implements IGroups.GroupProvisions
        Get
            Return INDrgProvisions.EditValue
        End Get
        Set(value As Boolean)
            INDrgProvisions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del parametro de clase de contratos
    ''' </summary>
    Public Property ContractClasses As Byte Implements IGroups.ContractClasses
        Get
            Return INDgleContractClass.EditValue
        End Get
        Set(value As Byte)
            INDgleContractClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del parametro de Comprobante Retroactivo
    ''' </summary>
    Public Property RetroactiveReceipt As Byte Implements IGroups.RetroactiveReceipt
        Get
            Return INDSlRetroactiveReceipt.EditValue
        End Get
        Set(value As Byte)
            INDSlRetroactiveReceipt.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ConceptsDatasource As List(Of Concept) Implements IGroups.ConceptsDatasource
        Set(value As List(Of Concept))
            INDGleConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ConceptsAdjustDatasource As List(Of Concept) Implements IGroups.ConceptsAdjustDatasource
        Set(value As List(Of Concept))
            INDSlConceptAdjust.Properties.DataSource = value
        End Set
    End Property



    ''' <summary>
    ''' Propiedad que contiene el Día 31 de los Parámetros de Nómina
    ''' </summary>
    Public Property Day31 As Boolean? Implements IGroups.Day31
        Get
            Return INDrgDay31.EditValue
        End Get
        Set(value As Boolean?)
            INDrgDay31.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene Dias de vacaciones adicionales x periodos cumplidos
    ''' </summary>
    ''' <returns></returns>
    Public Property AdditionalVacationDays As Byte? Implements IGroups.AdditionalVacationDays
        Get
            Return INDseAdditionalVacationDays.EditValue
        End Get
        Set(value As Byte?)
            INDseAdditionalVacationDays.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el control INDSELimitSMMLContributive que representa 
    ''' Limite de SMMLV componente Contributivo de Prima Media
    ''' </summary>
    ''' <returns></returns>
    Public Property LimitSMMLContributive As Decimal?
        Get
            Return INDSELimitSMMLContributive.EditValue
        End Get
        Set(value As Decimal?)
            INDSELimitSMMLContributive.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Public Property DocumentTypePayrollVoucherXpo As XPInstantFeedbackSource Implements IGroups.DocumentTypePayrollVoucherXpo
        Get
            Return CType(INDSlPayrollVoucherVie.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlPayrollVoucherVie.Properties.DataSource = value
        End Set
    End Property

    Public Property DocumentTypeProvisionVoucherXpo As XPInstantFeedbackSource Implements IGroups.DocumentTypeProvisionVoucherXpo
        Get
            Return CType(INDSlProvisionVoucherVie.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlProvisionVoucherVie.Properties.DataSource = value
        End Set
    End Property

    Public Property DocumentTypePrestacionVoucherXpo As XPInstantFeedbackSource Implements IGroups.DocumentTypePrestacionVoucherXpo
        Get
            Return CType(INDSlPrestacionVoucherVie.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlPrestacionVoucherVie.Properties.DataSource = value
        End Set
    End Property

    Public Property DocumentTypeIncentiveVoucherXpo As XPInstantFeedbackSource Implements IGroups.DocumentTypeIncentiveVoucherXpo
        Get
            Return CType(INDSlIncentiveVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlIncentiveVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property DocumentTypeContractLiquidationVoucherXpo As XPInstantFeedbackSource Implements IGroups.DocumentTypeContractLiquidationVoucherXpo
        Get
            Return CType(INDSlContractLiquidationVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlContractLiquidationVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property DocumentTypeUnemploymentVoucherXpo As XPInstantFeedbackSource Implements IGroups.DocumentUnemploymentVoucherXpo
        Get
            Return CType(INDSlUnemploymentVoucherType.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlUnemploymentVoucherType.Properties.DataSource = value
        End Set
    End Property

    Public Property DocumentTypeRetroactiveReceiptXpo As XPInstantFeedbackSource Implements IGroups.DocumentTypeRetroactiveReceiptXpo
        Get
            Return CType(INDSlRetroactiveReceipt.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlRetroactiveReceipt.Properties.DataSource = value
        End Set
    End Property

    Public Property ListAccountXpo As XPInstantFeedbackSource Implements IGroups.ListAccountXpo
        Get
            Return CType(INDSlPayrollAccountVie.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlPayrollAccountVie.Properties.DataSource = value
        End Set
    End Property

    Public Property ListAccountContractLiquidationXpo As XPInstantFeedbackSource Implements IGroups.ListAccountContractLiquidationXpo
        Get
            Return CType(INDSlAccountContractLiquidation.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlAccountContractLiquidation.Properties.DataSource = value
        End Set
    End Property

    Public Property ListAccountVacationLiquidationXpo As XPInstantFeedbackSource Implements IGroups.ListAccountVacationLiquidationXpo
        Get
            Return CType(INDSlAccountVacationLiquidation.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlAccountVacationLiquidation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del estado del cargo
    ''' </summary>
    Public Property Status As Boolean

        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Número de primas al año
    ''' </summary>
    ''' <returns></returns>
    Public Property MaximunPremiumYear As Integer? Implements IGroups.MaximunPremiumYear
        Get
            Return INDtxtMaxPremiumByYear.EditValue
        End Get
        Set(value As Integer?)
            INDtxtMaxPremiumByYear.EditValue = value
        End Set
    End Property

    Public Property MonthVacationCompensation As Byte? Implements IGroups.MonthVacationCompensation
        Get
            If INDseAverageMonthCompensationVacation.EditValue Is Nothing Then
                Return INDseAverageMonthCompensationVacation.EditValue
            Else
                Return CType(INDseAverageMonthCompensationVacation.EditValue, Byte)
            End If

        End Get
        Set(value As Byte?)
            INDseAverageMonthCompensationVacation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del concepto de la prima 1
    ''' </summary>
    ''' <returns></returns>
    Private Property IdConceptIncentive1 As Integer?
        Get
            Return INDSlConceptIncentive1.EditValue
        End Get
        Set(value As Integer?)
            INDSlConceptIncentive1.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del concepto de la prima 2
    ''' </summary>
    ''' <returns></returns>
    Private Property IdConceptIncentive2 As Integer?
        Get
            Return INDSlConceptIncentive2.EditValue
        End Get
        Set(value As Integer?)
            INDSlConceptIncentive2.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Fórmula de la prima 1
    ''' </summary>
    ''' <returns></returns>
    Private Property IncentivePaymentFormula1 As String
        Get
            Return INDIncentivePayment1.EditValue
        End Get
        Set(value As String)
            INDIncentivePayment1.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Fórmula de la prima 2
    ''' </summary>
    ''' <returns></returns>
    Private Property IncentivePaymentFormula2 As String
        Get
            Return INDIncentivePayment2.EditValue
        End Get
        Set(value As String)
            INDIncentivePayment2.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Fecha de inicio de la prima 1
    ''' </summary>
    ''' <returns></returns>
    Private Property StartDateIncentivePayment1 As Date
        Get
            Return INDDeIncentiveStarDate1.EditValue
        End Get
        Set(value As Date)
            INDDeIncentiveStarDate1.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Fecha final de la prima 1
    ''' </summary>
    ''' <returns></returns>
    Private Property EndDateIncentivePayment1 As Date
        Get
            Return INDDeIncentiveEndDate1.EditValue
        End Get
        Set(value As Date)
            INDDeIncentiveEndDate1.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Fecha de inicio de la prima 1
    ''' </summary>
    ''' <returns></returns>
    Private Property StartDateIncentivePayment2 As Date
        Get
            Return INDDeIncentiveStarDate2.EditValue
        End Get
        Set(value As Date)
            INDDeIncentiveStarDate2.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Fecha final de la prima 1
    ''' </summary>
    ''' <returns></returns>
    Private Property EndDateIncentivePayment2 As Date
        Get
            Return INDDeIncentiveEndDate2.EditValue
        End Get
        Set(value As Date)
            INDDeIncentiveEndDate2.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Cantidad SMMLV Exoneración SENA, ICBF, Salud patrono
    ''' </summary>
    ''' <returns></returns>
    Private Property SMMLVAmountExemption As Integer?
        Get
            Return CInt(INDspSMMLVAmountExemption.EditValue)
        End Get
        Set(value As Integer?)
            INDspSMMLVAmountExemption.EditValue = value
        End Set
    End Property

#End Region

#Region "IDataColumnInfo Implementation"

    Public ReadOnly Property Caption() As String Implements IDataColumnInfo.Caption
        Get
            Return "MyExpression"
        End Get
    End Property

    Public ReadOnly Property Columns() As List(Of IDataColumnInfo) Implements IDataColumnInfo.Columns
        Get
            Return m_columns
        End Get
    End Property

    Public ReadOnly Property Controller() As DataControllerBase Implements IDataColumnInfo.Controller
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property FieldName() As String Implements IDataColumnInfo.FieldName
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property FieldType() As Type Implements IDataColumnInfo.FieldType
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property UnboundExpression() As String Implements IDataColumnInfo.UnboundExpression
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Name1 As String Implements IDataColumnInfo.Name
        Get
            Return Nothing
        End Get
    End Property


#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            If CompanyId <> Nothing Then
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GroupByCompany
                .FiltroBusqueda = CompanyId
            Else
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll

            End If
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
            ModoBusqueda = True
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub


    ''' <summary>
    ''' METODO: item buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        If ModoBusqueda = False Then
            CleanControls()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        Dim actionResult As ActionMessageResult(Of Group)

        If Group IsNot Nothing Then
            If Group.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MGroups(MyBase.Tag)
                        AsyncLoader(True)

                        actionResult = Await Model.DeleteGroupAsync(Group)
                        If actionResult.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            ModoBusqueda = False
                            Deshacer()
                        Else
                            For Each action As MessageResult In actionResult.MessageResult
                                If action.CodeMessage = "c-0000" Then
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                                    AsyncLoader(False)
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                                End If
                            Next
                        End If
                        AsyncLoader(False)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.seleccioneGrupo, Eform.groups)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.seleccioneGrupo, Eform.groups)
        End If
        Deshacer()
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        Try
            Dim controlsValidated As ActionResult = ValidateControls()
            If Not controlsValidated.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), controlsValidated.Message)
                Exit Sub
            End If

            If ValidateDates() = False Then
                Exit Sub
            End If

            If Not ValidateLiquidaSuperiorMinimum() Then
                Exit Sub
            End If
            Await AssigningValues()
            Using Model As New MGroups(MyBase.Tag)
                If Group.PayrollParameter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified AndAlso Group.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Group.MarkAsModified()
                End If
                If Group.ChangeTracker.State = ObjectState.Unchanged Then
                    If Group.GroupEventConcept.Where(Function(x) x.ChangeTracker.State = ObjectState.Added _
                                                     Or x.ChangeTracker.State = ObjectState.Modified Or x.ChangeTracker.State = ObjectState.Deleted) IsNot Nothing Then
                        Group.MarkAsModified()
                    End If
                End If
                For Each itemGEC As GroupEventConcept In Group.GroupEventConcept
                    itemGEC.ConceptId = itemGEC.Concept.Id
                    itemGEC.Concept = Nothing
                Next
                AsyncLoader(True)
                If Await Model.SaveGroupAsync(Group) = True Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If Group.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    ElseIf Group.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    End If
                    AsyncLoader(False)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
            CleanControls()
            ActionsOnControls = False
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' METODO: Item nuevo del control de usuario
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

#Region "Functions"
    Private Sub OpenFormulates(ByVal ChooseOption As Integer)
        FormulateFieldsLoad()

        If FlagFormulate = False Then

            ExpressionEditForm = New UnboundColumnExpressionEditorForm(Me, Nothing)
            AddHandler CType(ExpressionEditForm.Controls.Item(4), ListBoxControl).SelectedValueChanged, AddressOf selectedChangue
            ExpressionEditForm.StartPosition = FormStartPosition.CenterParent

            Select Case ChooseOption

                Case 1
                    'PRIMA 1
                    ExpressionEditForm.Controls.Item(0).Text = INDIncentivePayment1.Text
                    If ExpressionEditForm.ShowDialog(Me) = DialogResult.OK Then
                        INDIncentivePayment1.Text = ExpressionEditForm.Expression
                    End If
                Case 2
                    'PRIMA 2
                    ExpressionEditForm.Controls.Item(0).Text = INDIncentivePayment2.Text
                    If ExpressionEditForm.ShowDialog(Me) = DialogResult.OK Then
                        INDIncentivePayment2.Text = ExpressionEditForm.Expression
                    End If

            End Select

        End If


    End Sub

    ''' <summary>
    ''' Metodo que carga las variables de nómina para la construccion de formulas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FormulateFieldsLoad()

        descriptions = New Dictionary(Of String, String)
        descriptions.Add("Salario Mínimo", "Salario mínimo establecido para el grupo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Auxilio Transporte", "Auxilio de transporte establecido para el grupo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Sueldo Contrato", "Salario establecido en el contrato" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Días Vacaciones", "Días de Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Gastos de Representación", "Gastos de Representación" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Bonificacion Año Servicio", "Bonificacion Año Servicio" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Prima Servicio", "Valor Prima Servicio" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Valor Base Vacaciones", "Valor Base Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias del Periodo de Primas", "Dias del Periodo de Primas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Salario Variable Primas", "Salario Variable Primas" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Sueldo Promedio", "Sueldo Promedio" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Dias de Sanción", "Dias de Sanción" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias de Licencias No Remuneradas", "Dias de Licencias No Remuneradas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Auxilio Transporte Promedio", "Auxilio de transporte establecido para el grupo " & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Meses Prima", "Meses Prima" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Pagados Vacaciones", "Dias Pagados Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Valor Prima Vacaciones", "Valor Prima Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Fecha Ingreso", "Fecha en que ingreso el empleado" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())
        descriptions.Add("Fecha Contratacion", "Fecha en que se contrato el empleado" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())
        descriptions.Add("Meses Laborados Prima", "Variable que calcula los meses laborados por el empleado teninedo encuenta las licencias no remuneradas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Meses Bonificacion Año Servicio", "Variable que calcula los meses laborados por el empleado para la Bonificación por Año de Servicio (Liq. Contrato)" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Diarias Laboradas", "Variable Horas Diarias Laboradas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Trabajados Liq Contrato", "Variable Dias Trabajados Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Meses Vacaciones Liq Contrato", "Variable Meses Vacaciones Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Vacaciones x Pagar Liq Contrato", "Variable Dias a pagar de Vacaiones en Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Meses Primas Liq Contrato", "Variable Dias a pagar de Vacaiones en Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Bonificacion Año Servicio Liq Contrato", "Variable para obtener el valor de la Bonificación por Año de Servicio de Liq. de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Vacaciones Compensadas", "Variable para saber si las vacaciones van a ser compensadas o disfrutadas (Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Boolean).ToString())
        descriptions.Add("Promedio Recargos Nocturnos Normales", "Variable para obtener el valor promedio de los Recargos Nocturnos Normales (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Recargos Promedio Nocturnos N", "Variable para obtener el valor promedio de los Recargos Nocturnos Normales a partir del mes actual (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Promedio Recargos Nocturnos Festivos", "Variable para obtener el valor promedio de los Recargos Nocturnos Festivos (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Promedio Recargos Dominicales", "Variable para obtener el valor promedio de los Recargos Dominicales (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Promedio Horas Extras", "Variable para obtener el valor promedio de las Horas Extras (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Sueldo Promedio Contrato", "Variable para obtener el valor promedio de los Sueldos del Empleado (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Promedio Bonificaciones Salarias", "Variable para obtener el valor promedio de las Bonificaciones Salariales (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())
        descriptions.Add("Promedio Salario Variable Vacaciones", "Variable para obtener el valor promedio del Salario Variable Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Decimal).ToString())


        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALARIM", "Salario Mínimo", GetType(Decimal), m_columns, "Salario Minimo"))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARTRANSPO", "Auxilio Transporte", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVSUELDO", "Sueldo Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASVAC", "Días Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARGASTREP", "Gastos de Representación", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARBONANOS", "Bonificacion Año Servicio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARPRIMSER", "Valor Prima Servicio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARBASEVAC", "Valor Base Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIAPERP", "Dias del Periodo de Primas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALVARP", "Salario Variable Primas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARAUXIALM", "Auxilio Alimentos", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSUELPRO", "Sueldo Promedio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASANC", "Dias de Sanción", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASLIC", "Dias de Licencias No Remuneradas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARTRANSPR", "Auxilio Transporte Promedio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARMESPRIM", "Meses Prima", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVACPAGD", "Dias Pagados Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARPRIMVAC", "Valor Prima Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARFECHING", "Fecha Ingreso", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARFECHCON", "Fecha Contratacion", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARMONTHPR", "Meses Laborados Prima", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARMONTHBS", "Meses Bonificacion Año Servicio", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARHORASDI", "Horas Diarias Laboradas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASLAB", "Dias Trabajados Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARMESVACL", "Meses Vacaciones Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIAVACL", "Dias Vacaciones x Pagar Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARMESPRIL", "Meses Primas Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARBONIFLC", "Bonificacion Año Servicio Liq Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVACACOM", "Vacaciones Compensadas", GetType(Boolean), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARACURECN", "Promedio Recargos Nocturnos Normales", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARACURECN", "Recargos Promedio Nocturnos N", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARACURECF", "Promedio Recargos Nocturnos Festivos", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARACUREDO", "Promedio Recargos Dominicales", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARACUHOEX", "Promedio Horas Extras", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARACUSUEL", "Sueldo Promedio Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALVARP", "Promedio Salario Variable Vacaciones", GetType(Decimal), m_columns, [String].Empty))

    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Evento que se dispara cuando cambia el item seleccionado de la lista de campos, para establecer la correcta descripción!
    ''' </summary>
    Private Sub selectedChangue(sender As Object, e As EventArgs)
        Dim lista As ListBoxControl = CType(sender, ListBoxControl)
        If lista.ItemCount > 0 Then
            Dim item As String = lista.SelectedItem.ToString().Replace("[", "").Replace("]", "")
            If descriptions.ContainsKey(item) Then
                ExpressionEditForm.Controls.Item(6).Text = descriptions(item)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmGroupMetaData, Eform.InfoMetaData), Me.Group.Code, Me.Group.Name, INDgleCompany.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Group.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmGroupMetaDataTitle, Eform.InfoMetaData), Me.Group.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmGroupMetaData, Eform.InfoMetaData), Me.Group.Code, Me.Group.Name, INDgleCompany.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmGroupMetaDataTitle, Eform.InfoMetaData), Me.Group.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa los campos necesarios para el funcionamiento del frontal
    ''' </summary>
    Private Sub Initializes()
        INDgleContractClass.Properties.DataSource = EmployeeHelper.ContractClasses

        ListConceptType = New List(Of Tuple(Of Byte, String))
        ListConceptType.Add(New Tuple(Of Byte, String)(1, "Sueldo"))
        ListConceptType.Add(New Tuple(Of Byte, String)(2, "Auxilio de Transporte"))
        ListConceptType.Add(New Tuple(Of Byte, String)(3, "Aporte Salud"))
        ListConceptType.Add(New Tuple(Of Byte, String)(4, "Aporte Pensión"))
        ListConceptType.Add(New Tuple(Of Byte, String)(5, "Fondo de Solidaridad Pensional"))
        INDSlConceptType.Properties.DataSource = ListConceptType.ToList

        ListSpecialPensionRate = New List(Of Tuple(Of Byte, String))
        ListSpecialPensionRate.Add(New Tuple(Of Byte, String)(0, "Sin Riesgo"))
        ListSpecialPensionRate.Add(New Tuple(Of Byte, String)(1, "Actividades de alto riesgo"))
        ListSpecialPensionRate.Add(New Tuple(Of Byte, String)(2, "Senadores"))
        ListSpecialPensionRate.Add(New Tuple(Of Byte, String)(3, "CTI"))
        ListSpecialPensionRate.Add(New Tuple(Of Byte, String)(4, "Aviadores"))
        INDsleIndicatorSpecialFee.Properties.DataSource = ListSpecialPensionRate.ToList
    End Sub

    ''' <summary>
    ''' Metodo que define los campos que permiten nulos en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetFieldsNULL()
        Try
            'Ejecuatamos la consulta
            Using model As New MGroups(MyBase.Tag)
                Dim dsFields As DataSet = model.GetFieldsNULL
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCtlGroups.Items.Count - 1
                        INDlyCtlGroups.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCtlGroups.Items.Item(j).Tag, Nothing) = False Then
                                Dim LayoutItem = CType(INDlyCtlGroups.Items.Item(j), DevExpress.XtraLayout.LayoutControlItem)
                                Dim Control = CType(LayoutItem.Control, DevExpress.XtraEditors.BaseControl)
                                Control.Tag = ""
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtlGroups.Items.Item(j).Tag.ToString.Trim Then
                                    Control.Tag = "NULL"
                                    INDlyCtlGroups.Items.Item(j).AllowHide = True
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
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyCtlGroups.BeginUpdate()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        GroupCode = String.Empty
        GroupName = String.Empty
        INDgleCompany.Text = String.Empty
        INDglCompany.Properties.NullText = String.Empty
        GroupLiquidation = Nothing
        GroupLastLiquidationDate = Nothing
        GroupNextLiquidationDate = Nothing
        GroupMonths = Nothing
        GroupProvisions = Nothing
        Status = True
        INDteInitialTimeOrdinaryDay.EditValue = String.Empty
        INDteEndTimeOrdinaryDay.EditValue = String.Empty
        INDtxtLegalSalaryMinimum.Text = String.Empty
        INDtxtInstitutionalMinimumSalary.Text = String.Empty
        INDtxtTransportHelpValue.Text = String.Empty
        SMMLVAmountExemption = 0
        INDseEmployeePensionContributionPercentage.EditValue = String.Empty
        INDseEmployerPensionContributionPercentage.EditValue = String.Empty
        INDseEmployeeHealthContributionPercentage.EditValue = String.Empty
        INDseEmployerHealthContributionPercentage.EditValue = String.Empty
        INDseHealthContributionMaximunSalary.EditValue = String.Empty
        INDseRTFExcempt.EditValue = String.Empty
        INDrgHolidayPayment.EditValue = Nothing
        INDseAverageMonthVacation.EditValue = String.Empty
        INDseMaximumDiscountPercentage.EditValue = String.Empty
        INDseEducationHealthDiscountPercentage.EditValue = String.Empty
        INDtxtHousingDeductionMaximumValue.Text = String.Empty
        INDcheSaturdayBusinessDay.EditValue = False
        INDcheSundayBusinessDay.EditValue = False
        INDseSenaContributionPercentage.EditValue = String.Empty
        INDseICBFContributionPercentage.EditValue = String.Empty
        INDseCompensationFundContributionPercentage.EditValue = String.Empty
        INDSlConceptType.EditValue = String.Empty
        INDSlConceptAdjust.EditValue = String.Empty
        INDGleConcept.EditValue = String.Empty
        INDCbeTypeDay.EditValue = Nothing
        INDCbeTypeSchedule.EditValue = Nothing
        INDGleConcept.EditValue = Nothing
        INDgcEventConcetps.DataSource = Nothing
        INDGcConceptAdjust.DataSource = Nothing
        INDgleCompany.EditValue = Nothing
        INDtxtMaxPremiumByYear.EditValue = String.Empty
        IdConceptIncentive1 = Nothing
        IncentivePaymentFormula1 = Nothing
        StartDateIncentivePayment1 = Nothing
        EndDateIncentivePayment1 = Nothing
        IdConceptIncentive2 = Nothing
        IncentivePaymentFormula2 = Nothing
        StartDateIncentivePayment2 = Nothing
        EndDateIncentivePayment2 = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        INDrgLiquidation.EditValue = -1
        INDrgMonths.EditValue = -1
        INDrgProvisions.EditValue = -1
        INDgleCompany.EditValue = Nothing
        INDgleContractClass.EditValue = Nothing
        INDrgVacationAdvanced.EditValue = -1
        INDCbeValueAproximation.EditValue = Nothing
        INDglPrestacionesVoucher.EditValue = Nothing
        INDSlInabilityVoucherType.EditValue = Nothing
        INDglPayrollVoucher.EditValue = Nothing
        INDglProvisionVoucher.EditValue = Nothing
        INDglPayrollAccount.EditValue = Nothing
        INDrgContabilizacion.SelectedIndex = -1
        INDrgContabilizacionVie.SelectedIndex = -1
        INDrgDay31.SelectedIndex = 1
        INDSlPayrollAccountVie.EditValue = Nothing
        INDSlPayrollVoucherVie.EditValue = Nothing
        INDSlPrestacionVoucherVie.EditValue = Nothing
        INDSlProvisionVoucherVie.EditValue = Nothing
        INDSlContractLiquidationVoucher.EditValue = Nothing
        INDSlIncentiveVoucher.EditValue = Nothing
        INDSlUnemploymentVoucherType.EditValue = Nothing
        INDSlAccountContractLiquidation.EditValue = Nothing
        INDSlAccountVacationLiquidation.EditValue = Nothing
        RetroactiveReceipt = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        INDTxtUVTValue.EditValue = 0
        INDseAverageMonthCompensationVacation.EditValue = 0
        INDBteCode.Focus()
        INDrgLiquidaMinimumInstitucional.EditValue = False
        INDsleIndicatorSpecialFee.EditValue = Nothing
        AdditionalVacationDays = 0
        LimitSMMLContributive = 0
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

        INDlyCtlGroups.EndUpdate()
        Presenter.Initializes()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    ''' 
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGroups.ActionsOnControls
        Set(value As Boolean)
            INDlyCtlGroups.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDrgLiquidation.Enabled = value
            INDdeLastLiquidationDate.Enabled = value
            INDdeNextLiquidationDate.Enabled = value
            INDrgMonths.Enabled = value
            INDrgProvisions.Enabled = value
            INDgleCompany.Enabled = value
            INDgleContractClass.Enabled = value
            INDteInitialTimeOrdinaryDay.Enabled = value
            INDteEndTimeOrdinaryDay.Enabled = value
            INDrgLiquidaMinimumInstitucional.Enabled = value
            INDsleIndicatorSpecialFee.Enabled = value
            INDtxtLegalSalaryMinimum.Enabled = value
            INDtxtInstitutionalMinimumSalary.Enabled = value
            INDtxtTransportHelpValue.Enabled = value
            INDseEmployeePensionContributionPercentage.Enabled = value
            INDseEmployerPensionContributionPercentage.Enabled = value
            INDseEmployeeHealthContributionPercentage.Enabled = value
            INDseEmployerHealthContributionPercentage.Enabled = value
            INDseHealthContributionMaximunSalary.Enabled = value
            INDseRTFExcempt.Enabled = value
            INDrgHolidayPayment.Enabled = value
            INDseAverageMonthVacation.Enabled = value
            INDseMaximumDiscountPercentage.Enabled = value
            INDseEducationHealthDiscountPercentage.Enabled = value
            INDtxtHousingDeductionMaximumValue.Enabled = value
            INDcheSaturdayBusinessDay.Enabled = value
            INDcheSundayBusinessDay.Enabled = value
            INDseSenaContributionPercentage.Enabled = value
            INDseICBFContributionPercentage.Enabled = value
            INDseCompensationFundContributionPercentage.Enabled = value
            INDrgVacationAdvanced.Enabled = value
            INDtxtMaxVacationByYear.Enabled = value
            INDseVacationDays.Enabled = value
            INDtxtMaxPremiumByYear.Enabled = value
            INDCbeTypeDay.Enabled = value
            INDCbeTypeSchedule.Enabled = value
            INDGleConcept.Enabled = value
            INDSbAddConcepts.Enabled = value
            INDCbeValueAproximation.Enabled = value
            INDglCompany.Enabled = value
            INDglPayrollVoucher.Enabled = value
            INDglProvisionVoucher.Enabled = value
            INDglPrestacionesVoucher.Enabled = value
            INDSlInabilityVoucherType.Enabled = value
            INDglPayrollAccount.Enabled = value
            INDrgContabilizacion.Enabled = value
            INDrgContabilizacionVie.Enabled = value
            INDrgDay31.Enabled = value
            INDSlPayrollAccountVie.Enabled = value
            INDSlPayrollVoucherVie.Enabled = value
            INDSlPrestacionVoucherVie.Enabled = value
            INDSlProvisionVoucherVie.Enabled = value
            INDSlIncentiveVoucher.Enabled = value
            INDSlUnemploymentVoucherType.Enabled = value
            INDSlContractLiquidationVoucher.Enabled = value
            RetroactiveReceipt = value
            INDTxtUVTValue.Enabled = value
            INDseAverageMonthCompensationVacation.Enabled = value
            INDSlAccountContractLiquidation.Enabled = value
            INDSlAccountVacationLiquidation.Enabled = value
            INDSlConceptAdjust.Enabled = value
            INDSlConceptType.Enabled = value
            INDBtnAddConceptAdjust.Enabled = value
            INDGcConceptAdjust.Enabled = value
            INDgcEventConcetps.Enabled = value
            INDSlRetroactiveReceipt.Enabled = False
            INDIncentivePayment1.Enabled = value
            INDDeIncentiveStarDate1.Enabled = value
            INDDeIncentiveEndDate1.Enabled = value
            INDSlConceptIncentive1.Enabled = value
            INDIncentivePayment2.Enabled = value
            INDDeIncentiveStarDate2.Enabled = value
            INDDeIncentiveEndDate2.Enabled = value
            INDSlConceptIncentive2.Enabled = value
            INDseAdditionalVacationDays.Enabled = value
            INDspSMMLVAmountExemption.Enabled = value
            INDlciLimitSMMLContributive.Enabled = value
            INDLCLimitSMML.Enabled = value
            Me.BarraBotones.StatusRecordEnabled = value
            If value = False Then
                INDBteCode.Focus()
            Else
                INDgleCompany.Focus()
            End If
            INDlyCtlGroups.EndUpdate()

        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        FlagFormulate = False
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        Dim newGroupEventConcepts As TrackableCollection(Of GroupEventConcept) = New TrackableCollection(Of GroupEventConcept)
        AsyncLoader(True)
        Group = Await Model.GetGroupAsync(GroupCode)
        If Group Is Nothing Or Group.Id = 0 Then
            Me.Group = New Group()
            Me.LogicaBotonActualizar(False)
            AsyncLoader(False)
            ActionsOnControls = True
            Group.State = True
        Else
            If Not Group Is Nothing Then
                If Group.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(Me.Tag, Group.Id)
                    With Group
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Group.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Group.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Group.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(Group.ModificationDate Is Nothing, Nothing, Group.ModificationDate))
                        GroupCode = .Code
                        GroupName = .Name
                        CompanyId = .CompanyId
                        GroupLiquidation = .Liquidation
                        GroupLastLiquidationDate = .LastDateLiquidation
                        GroupNextLiquidationDate = .NextDateLiquidation
                        GroupMonths = .Month
                        GroupProvisions = .HandlingProvisions
                        ContractClasses = .ContractClass
                        Status = .State
                        Day31 = .PayrollParameter.Day31
                        INDteInitialTimeOrdinaryDay.EditValue = .PayrollParameter.InitialTimeOrdinaryDay
                        INDteEndTimeOrdinaryDay.EditValue = .PayrollParameter.EndTimeOrdinaryDay
                        INDtxtLegalSalaryMinimum.Text = .PayrollParameter.LegalSalaryMinimum
                        INDtxtInstitutionalMinimumSalary.Text = .PayrollParameter.InstitutionalMinimumSalary
                        INDtxtTransportHelpValue.Text = .PayrollParameter.TransportHelpValue
                        SMMLVAmountExemption = .SMMLVAmountExemption

                        INDseEmployeePensionContributionPercentage.EditValue = .PayrollParameter.EmployeePensionContributionPercentage
                        INDseEmployerPensionContributionPercentage.EditValue = .PayrollParameter.EmployerPensionContributionPercentage
                        INDseEmployeeHealthContributionPercentage.EditValue = .PayrollParameter.EmployeeHealthContributionPercentage
                        INDseEmployerHealthContributionPercentage.EditValue = .PayrollParameter.EmployerHealthContributionPercentage
                        INDseHealthContributionMaximunSalary.EditValue = .PayrollParameter.HealthContributionMaximunSalary
                        INDseRTFExcempt.EditValue = .PayrollParameter.RTFExemptPercentage
                        INDTxtUVTValue.EditValue = .PayrollParameter.UVTValue
                        INDrgHolidayPayment.EditValue = .PayrollParameter.LiquidateVacation
                        INDseAverageMonthVacation.EditValue = .PayrollParameter.AverageMonthVacation
                        INDseMaximumDiscountPercentage.EditValue = .PayrollParameter.MaximunDiscountPercentage
                        INDseEducationHealthDiscountPercentage.EditValue = .PayrollParameter.EducationHealthDiscountPercentage
                        INDtxtHousingDeductionMaximumValue.Text = .PayrollParameter.HousingDeductionMaximumValue
                        INDcheSaturdayBusinessDay.EditValue = .PayrollParameter.SaturdayBusinessDay
                        INDcheSundayBusinessDay.EditValue = .PayrollParameter.SundayBusinessDay
                        INDseSenaContributionPercentage.EditValue = .PayrollParameter.SenaContributionPercentage
                        INDseICBFContributionPercentage.EditValue = .PayrollParameter.ICBFContributionPercentage
                        INDseCompensationFundContributionPercentage.EditValue = .PayrollParameter.CompensationFundContributionPercentage
                        INDgcEventConcetps.DataSource = .GroupEventConcept
                        INDGcConceptAdjust.DataSource = .GroupAdjustConceptContractLiquidation
                        INDrgVacationAdvanced.EditValue = .PayrollParameter.VacationAdvanced
                        INDtxtMaxVacationByYear.EditValue = .PayrollParameter.MaxVacationByYear
                        MaximunPremiumYear = .PayrollParameter.MaxPremiumByYear
                        AdditionalVacationDays = .AdditionalVacationDays
                        LimitSMMLContributive = .PayrollParameter.LimitSMMLContributive
                        If .PayrollParameter.MaxPremiumByYear > 2 Then
                            Mensaje(EeventViewerImages.Advertencia) = "Su número de primas es: " & .PayrollParameter.MaxPremiumByYear & vbCrLf & "El número máximo de primas permitidas es de 2"
                            MaximunPremiumYear = 2
                        End If
                        If CInt(INDtxtMaxPremiumByYear.EditValue) > 0 Then
                            If .PayrollParameter.IdIncentivePaymentConcept1 IsNot Nothing Then
                                IncentivePaymentFormula1 = .PayrollParameter.IncentivePaymentFormula1
                                StartDateIncentivePayment1 = .PayrollParameter.StartDateIncentivePayment1
                                EndDateIncentivePayment1 = .PayrollParameter.EndDateIncentivePayment1
                                IdConceptIncentive1 = .PayrollParameter.IdIncentivePaymentConcept1
                            Else
                                Mensaje(EeventViewerImages.Informacion) = "Parametrice sus primas en este formulario"
                            End If
                            If CInt(INDtxtMaxPremiumByYear.EditValue) > 1 Then
                                If .PayrollParameter.IdIncentivePaymentConcept2 IsNot Nothing Then
                                    IncentivePaymentFormula2 = .PayrollParameter.IncentivePaymentFormula2
                                    StartDateIncentivePayment2 = .PayrollParameter.StartDateIncentivePayment2
                                    EndDateIncentivePayment2 = .PayrollParameter.EndDateIncentivePayment2
                                    IdConceptIncentive2 = .PayrollParameter.IdIncentivePaymentConcept2
                                End If
                            End If
                        End If
                        INDseVacationDays.EditValue = .PayrollParameter.VacationDays
                        INDCbeValueAproximation.EditValue = .PayrollParameter.AproximationValue
                        MonthVacationCompensation = .PayrollParameter.AverageMonthVacationCompensation
                        INDrgLiquidaMinimumInstitucional.EditValue = If(.PayrollParameter.LiquidaMinimumInstitutionalSalary, True, False)
                        INDsleIndicatorSpecialFee.EditValue = .PayrollParameter.SpecialPensionRateIndicator

                        If indigo.IndigoPayrollIntegration = 1 Then
                            INDrgContabilizacionVie.EditValue = .PayrollParameter.AccountedBy
                            If .PayrollParameter.PrestacionVoucherCode IsNot Nothing Then
                                INDSlPrestacionVoucherVie.EditValue = .PayrollParameter.PrestacionVoucherCode
                            End If

                            If .PayrollParameter.ProvisionVoucherCode IsNot Nothing Then
                                INDSlProvisionVoucherVie.EditValue = .PayrollParameter.ProvisionVoucherCode
                            End If

                            If .PayrollParameter.PayrollVoucherCode IsNot Nothing Then
                                INDSlPayrollVoucherVie.EditValue = .PayrollParameter.PayrollVoucherCode
                            End If

                            If .PayrollParameter.IdPayrollAccount IsNot Nothing Then
                                INDSlPayrollAccountVie.EditValue = .PayrollParameter.IdPayrollAccount
                            End If

                            If .PayrollParameter.IdLiquidationContractAccount IsNot Nothing Then
                                INDSlAccountContractLiquidation.EditValue = .PayrollParameter.IdLiquidationContractAccount
                            End If

                            If .PayrollParameter.IdLiquidationVacationAccount IsNot Nothing Then
                                INDSlAccountVacationLiquidation.EditValue = .PayrollParameter.IdLiquidationVacationAccount
                            End If

                            If .PayrollParameter.IdIncentiveVoucherType IsNot Nothing Then
                                INDSlIncentiveVoucher.EditValue = .PayrollParameter.IdIncentiveVoucherType
                            End If

                            If .PayrollParameter.IdContractLiquidationVoucherType IsNot Nothing Then
                                INDSlContractLiquidationVoucher.EditValue = .PayrollParameter.IdContractLiquidationVoucherType
                            End If

                            If .PayrollParameter.IdUnemploymentVoucherType IsNot Nothing Then
                                INDSlUnemploymentVoucherType.EditValue = .PayrollParameter.IdUnemploymentVoucherType
                            End If

                            If .PayrollParameter.IdPayrollVoucherType IsNot Nothing Then
                                RetroactiveReceipt = .PayrollParameter.IdPayrollVoucherType
                            End If

                        Else
                            INDrgContabilizacion.EditValue = .PayrollParameter.AccountedBy
                            If Group.PayrollParameter.InterfaceName IsNot Nothing And Group.PayrollParameter.InterfaceName <> "" Then

                                Model = New MGroups(Tag)
                                AsyncLoader(True)
                                Dim ListTypeDocument = Await Model.ListTypeDocument(Group.PayrollParameter.InterfaceName)

                                AsyncLoader(False)

                                Dim InterfaceAccount As List(Of Domain.Entities.GlosasParametersInterface) = INDglCompany.Properties.DataSource

                                If ListTypeDocument IsNot Nothing Then
                                    INDglPayrollVoucher.Properties.DataSource = ListTypeDocument
                                    INDglProvisionVoucher.Properties.DataSource = ListTypeDocument
                                    INDglPrestacionesVoucher.Properties.DataSource = ListTypeDocument
                                    INDSlInabilityVoucherType.Properties.DataSource = ListTypeDocument
                                End If

                                Dim GroupPayrollParameterDocuments = Group.PayrollParameter

                                If Group.PayrollParameter.InterfaceName IsNot Nothing Then

                                    If InterfaceAccount IsNot Nothing And InterfaceAccount.Count > 0 Then
                                        INDglCompany.EditValue = InterfaceAccount.Where(Function(x) x.ContainerName = Group.PayrollParameter.InterfaceName).FirstOrDefault().ContainerName
                                    End If

                                    If Group.PayrollParameter.PayrollVoucherCode IsNot Nothing Then
                                        If ListTypeDocument IsNot Nothing Then
                                            INDglPayrollVoucher.EditValue = ListTypeDocument.Where(Function(x) x.Code = Group.PayrollParameter.PayrollVoucherCode).FirstOrDefault().Code
                                        End If
                                    End If
                                    If Group.PayrollParameter.ProvisionVoucherName IsNot Nothing Then
                                        If ListTypeDocument IsNot Nothing Then
                                            INDglProvisionVoucher.EditValue = ListTypeDocument.Where(Function(x) x.CodeName = Group.PayrollParameter.ProvisionVoucherName).FirstOrDefault().Code
                                        End If
                                    End If
                                    If Group.PayrollParameter.PrestacionVoucherName IsNot Nothing Then
                                        If ListTypeDocument IsNot Nothing Then
                                            INDglPrestacionesVoucher.EditValue = ListTypeDocument.Where(Function(x) x.CodeName = Group.PayrollParameter.PrestacionVoucherName).FirstOrDefault().Code
                                        End If
                                    End If

                                End If
                            End If
                        End If


                    End With
                    AsyncLoader(False)
                    ActionsOnControls = True
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.Group.Code)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(Group.Id)
                        Dim state = New ObjectChangeTracker
                        state.State = ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Group.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                    INDgcEventConcetps.DataSource = Group.GroupEventConcept
                    INDGcConceptAdjust.DataSource = Group.GroupAdjustConceptContractLiquidation
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Group = New Group With {.State = True}
                INDgcEventConcetps.DataSource = Group.GroupEventConcept
                INDGcConceptAdjust.DataSource = Group.GroupAdjustConceptContractLiquidation
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida que esten registrado minimo 1 concepto de cada tipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateGroupEventConcept() As Boolean
        ValidateGroupEventConcept = True
        If Group.GroupEventConcept.Count < 4 Then
            INDCbeTypeDay.Focus()
            ValidateGroupEventConcept = False
            Exit Function
        End If

        If Group.GroupEventConcept.Where(Function(x) x.TypeDay = 1 And x.TypeSchedule = 1).FirstOrDefault Is Nothing Or
           Group.GroupEventConcept.Where(Function(x) x.TypeDay = 1 And x.TypeSchedule = 2).FirstOrDefault Is Nothing Or
           Group.GroupEventConcept.Where(Function(x) x.TypeDay = 2 And x.TypeSchedule = 1).FirstOrDefault Is Nothing Or
           Group.GroupEventConcept.Where(Function(x) x.TypeDay = 2 And x.TypeSchedule = 2).FirstOrDefault Is Nothing Then
            INDCbeTypeDay.Focus()
            ValidateGroupEventConcept = False
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As ActionResult
        Dim fieldsRequired As New List(Of String)

        If GroupCode.Equals(String.Empty) Then
            fieldsRequired.Add(INDlyItemCode.Text)
        End If
        If GroupName.Equals(String.Empty) Then
            fieldsRequired.Add(INDlyItemName.Text)
        End If
        If CompanyId.Equals(String.Empty) Then
            fieldsRequired.Add(INDlyItemCompany.Text)
        End If
        If GroupLiquidation = Nothing Then
            fieldsRequired.Add(INDlyItemLiquidation.Text)
        End If
        If GroupLiquidation = -1 Then
            fieldsRequired.Add(INDlyItemLiquidation.Text)
        End If
        If GroupLastLiquidationDate = Nothing Then
            fieldsRequired.Add(INDlyItemLastLiquidation.Text)
        End If
        If GroupNextLiquidationDate = Nothing Then
            fieldsRequired.Add(INDlyItemNextLiquidation.Text)
        End If
        If GroupMonths = Nothing Then
            fieldsRequired.Add(INDlyItemMonths.Text)
        End If
        If INDgleContractClass.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemContractClass.Text)
        End If

        If INDtxtLegalSalaryMinimum.Text.Equals(String.Empty) AndAlso CDbl(INDtxtLegalSalaryMinimum.Text) <= 0 Then
            fieldsRequired.Add(INDlyItemLegalSalaryMinimum.Text)
        End If

        If INDtxtTransportHelpValue.Text.Equals(String.Empty) AndAlso CDbl(INDtxtTransportHelpValue.Text) <= 0 Then
            fieldsRequired.Add(INDLyItemTransportHelpValue.Text)
        End If

        If INDseHealthContributionMaximunSalary.Text.Equals(String.Empty) AndAlso CDbl(INDseHealthContributionMaximunSalary.Text) <= 0 Then
            fieldsRequired.Add(INDlyItemHealthContributionMaximunSalary.Text)
        End If

        If INDtxtHousingDeductionMaximumValue.Text.Equals(String.Empty) AndAlso CDbl(INDtxtHousingDeductionMaximumValue.Text) <= 0 Then
            fieldsRequired.Add(INDlyItemHousingDeductionMaximumValue.Text)
        End If

        If INDteInitialTimeOrdinaryDay.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemInitialTimeOrdinaryDay.Text)
        End If

        If INDteEndTimeOrdinaryDay.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemEndTimeOrdinaryDay.Text)
        End If

        If INDtxtMaxVacationByYear.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemtMaxVacation.Text)
        End If

        If INDseVacationDays.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemVacationDay.Text)
        End If

        If INDtxtMaxPremiumByYear.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemMaxPremiumByYear.Text)
        End If

        If CInt(INDtxtMaxPremiumByYear.EditValue) >= 1 Then
            If IncentivePaymentFormula1 Is Nothing Then
                fieldsRequired.Add(INDLciIncentivePayment.Text)
            End If

            If IdConceptIncentive1 Is Nothing Then
                fieldsRequired.Add(INDSlChristmasIncentive1.Text)
            End If
        End If

        If CInt(INDtxtMaxPremiumByYear.EditValue) >= 2 Then
            If IncentivePaymentFormula2 Is Nothing Then
                fieldsRequired.Add(INDLciDecemberIncentivePayment1.Text)
            End If

            If IdConceptIncentive2 Is Nothing Then
                fieldsRequired.Add(INDSlChristmasIncentive2.Text)
            End If
        End If

        If INDCbeValueAproximation.EditValue Is Nothing Then
            fieldsRequired.Add(INDlyItemComboAproximacion.Text)
        End If

        If SMMLVAmountExemption Is Nothing Or SMMLVAmountExemption = 0 Then
            fieldsRequired.Add(INDLyItemSMMLVAmountExemption.Text)
        End If

        If fieldsRequired.Count() = 0 Then
            Return New ActionResult With {.StateResult = True}
        Else
            Dim message As String = String.Join(", ", fieldsRequired)
            Return New ActionResult With {.StateResult = False, .Message = message}
        End If
    End Function

    ''' <summary>
    ''' Función para validar las fechas de la prima
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDates() As Boolean

        If StartDateIncentivePayment1 >= EndDateIncentivePayment1 Then
            Mensaje(EeventViewerImages.Advertencia) = "La fecha de fin debe ser mayor a la de inicio"
            INDDeIncentiveEndDate1.Focus()
            Return False
        End If

        If CInt(INDtxtMaxPremiumByYear.EditValue) > 1 Then

            If StartDateIncentivePayment2 <= EndDateIncentivePayment1 Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha de inicio de la prima 2 debe ser mayor a la fecha de fin de la prima 1"
                INDDeIncentiveEndDate2.Focus()
                Return False
            End If

            If StartDateIncentivePayment2 >= EndDateIncentivePayment2 Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha de fin debe ser mayor a la de inicio"
                INDDeIncentiveEndDate2.Focus()
                Return False
            End If

        End If
        Return True
    End Function

    ''' <summary>
    ''' Valida que el salario minimo institucinal no sea inferior al salario minimo legal
    ''' </summary>
    Private Function ValidateLiquidaSuperiorMinimum() As Boolean
        If INDrgLiquidaMinimumInstitucional.EditValue = True Then
            If INDtxtInstitutionalMinimumSalary.EditValue < INDtxtLegalSalaryMinimum.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del Salario Mínimo Institucional, no debe ser inferior al Salario Mínimo Legal."
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Async Function AssigningValues() As Task
        FlagFormulate = True

        If IncentivePaymentFormula1 = String.Empty Then
            IncentivePaymentFormula1 = "0"
        End If

        If IncentivePaymentFormula2 = String.Empty Then
            IncentivePaymentFormula2 = "0"
        End If


        Try
            AsyncLoader(False)
            With Group
                .Code = GroupCode
                .Name = GroupName
                .CompanyId = CompanyId
                .Liquidation = GroupLiquidation
                .LastDateLiquidation = GroupLastLiquidationDate
                .NextDateLiquidation = GroupNextLiquidationDate
                .Month = GroupMonths
                .HandlingProvisions = GroupProvisions
                .ContractClass = ContractClasses
                .AdditionalVacationDays = INDseAdditionalVacationDays.EditValue
                .SMMLVAmountExemption = SMMLVAmountExemption

                Dim x = .AdditionalVacationDays
                Dim y = INDseAdditionalVacationDays.EditValue
                If .PayrollParameterId = 0 Then
                    .PayrollParameter = New PayrollParameter
                End If
                .PayrollParameterId = .PayrollParameter.Id
                .PayrollParameter.Day31 = Day31
                If INDteInitialTimeOrdinaryDay.EditValue.GetType.ToString() = "System.DateTime" Then
                    .PayrollParameter.InitialTimeOrdinaryDay = New TimeSpan(CType(INDteInitialTimeOrdinaryDay.EditValue, Date).Hour, CType(INDteInitialTimeOrdinaryDay.EditValue, Date).Minute, CType(INDteInitialTimeOrdinaryDay.EditValue, Date).Second)
                ElseIf INDteInitialTimeOrdinaryDay.EditValue.GetType.ToString() = "System.TimeSpan" Then
                    .PayrollParameter.InitialTimeOrdinaryDay = New TimeSpan(CType(INDteInitialTimeOrdinaryDay.EditValue, TimeSpan).Hours, CType(INDteInitialTimeOrdinaryDay.EditValue, TimeSpan).Minutes, CType(INDteInitialTimeOrdinaryDay.EditValue, TimeSpan).Seconds)
                End If
                If INDteEndTimeOrdinaryDay.EditValue.GetType.ToString() = "System.DateTime" Then
                    .PayrollParameter.EndTimeOrdinaryDay = New TimeSpan(CType(INDteEndTimeOrdinaryDay.EditValue, Date).Hour, CType(INDteEndTimeOrdinaryDay.EditValue, Date).Minute, CType(INDteEndTimeOrdinaryDay.EditValue, Date).Second)
                ElseIf INDteEndTimeOrdinaryDay.EditValue.GetType.ToString() = "System.TimeSpan" Then
                    .PayrollParameter.EndTimeOrdinaryDay = New TimeSpan(CType(INDteEndTimeOrdinaryDay.EditValue, TimeSpan).Hours, CType(INDteEndTimeOrdinaryDay.EditValue, TimeSpan).Minutes, CType(INDteEndTimeOrdinaryDay.EditValue, TimeSpan).Seconds)
                End If
                If INDtxtLegalSalaryMinimum.EditValue.ToString() = String.Empty Then
                    .PayrollParameter.LegalSalaryMinimum = 0
                Else
                    .PayrollParameter.LegalSalaryMinimum = INDtxtLegalSalaryMinimum.EditValue
                End If
                If INDtxtInstitutionalMinimumSalary.EditValue.ToString() = String.Empty Then
                    .PayrollParameter.InstitutionalMinimumSalary = 0
                Else
                    .PayrollParameter.InstitutionalMinimumSalary = INDtxtInstitutionalMinimumSalary.EditValue
                End If

                If INDtxtTransportHelpValue.EditValue.ToString() = String.Empty Then
                    .PayrollParameter.TransportHelpValue = 0
                Else
                    .PayrollParameter.TransportHelpValue = INDtxtTransportHelpValue.EditValue
                End If

                .PayrollParameter.EmployeePensionContributionPercentage = INDseEmployeePensionContributionPercentage.EditValue
                .PayrollParameter.EmployerPensionContributionPercentage = INDseEmployerPensionContributionPercentage.EditValue
                .PayrollParameter.EmployeeHealthContributionPercentage = INDseEmployeeHealthContributionPercentage.EditValue
                .PayrollParameter.EmployerHealthContributionPercentage = INDseEmployerHealthContributionPercentage.EditValue
                .PayrollParameter.HealthContributionMaximunSalary = INDseHealthContributionMaximunSalary.EditValue
                .PayrollParameter.RTFExemptPercentage = INDseRTFExcempt.EditValue
                .PayrollParameter.UVTValue = CDec(INDTxtUVTValue.EditValue)
                .PayrollParameter.LiquidateVacation = IIf(INDrgHolidayPayment.EditValue.ToString = String.Empty, False, INDrgHolidayPayment.EditValue)
                .PayrollParameter.AverageMonthVacation = INDseAverageMonthVacation.EditValue
                .PayrollParameter.MaximunDiscountPercentage = INDseMaximumDiscountPercentage.EditValue
                .PayrollParameter.EducationHealthDiscountPercentage = INDseEducationHealthDiscountPercentage.EditValue
                .PayrollParameter.LimitSMMLContributive = LimitSMMLContributive
                If INDtxtHousingDeductionMaximumValue.EditValue.ToString() = String.Empty Then
                    .PayrollParameter.HousingDeductionMaximumValue = 0
                Else
                    .PayrollParameter.HousingDeductionMaximumValue = INDtxtHousingDeductionMaximumValue.EditValue
                End If
                .PayrollParameter.LiquidaMinimumInstitutionalSalary = INDrgLiquidaMinimumInstitucional.EditValue
                .PayrollParameter.SpecialPensionRateIndicator = INDsleIndicatorSpecialFee.EditValue
                .PayrollParameter.SaturdayBusinessDay = INDcheSaturdayBusinessDay.EditValue
                .PayrollParameter.SundayBusinessDay = INDcheSundayBusinessDay.EditValue
                .PayrollParameter.SenaContributionPercentage = INDseSenaContributionPercentage.EditValue
                .PayrollParameter.ICBFContributionPercentage = INDseICBFContributionPercentage.EditValue
                .PayrollParameter.CompensationFundContributionPercentage = INDseCompensationFundContributionPercentage.EditValue
                .PayrollParameter.VacationAdvanced = INDrgVacationAdvanced.EditValue
                .PayrollParameter.MaxVacationByYear = INDtxtMaxVacationByYear.EditValue
                .PayrollParameter.VacationDays = INDseVacationDays.EditValue
                .PayrollParameter.MaxPremiumByYear = CInt(INDtxtMaxPremiumByYear.EditValue)
                If CInt(INDtxtMaxPremiumByYear.EditValue) > 0 Then
                    .PayrollParameter.IdIncentivePaymentConcept1 = IdConceptIncentive1
                    .PayrollParameter.IncentivePaymentFormula1 = IncentivePaymentFormula1
                    .PayrollParameter.StartDateIncentivePayment1 = StartDateIncentivePayment1
                    .PayrollParameter.EndDateIncentivePayment1 = EndDateIncentivePayment1
                    If CInt(INDtxtMaxPremiumByYear.EditValue) = 1 Then
                        .PayrollParameter.IdIncentivePaymentConcept2 = Nothing
                        .PayrollParameter.IncentivePaymentFormula2 = Nothing
                        .PayrollParameter.StartDateIncentivePayment2 = Nothing
                        .PayrollParameter.EndDateIncentivePayment2 = Nothing
                    Else
                        .PayrollParameter.IdIncentivePaymentConcept2 = IdConceptIncentive2
                        .PayrollParameter.IncentivePaymentFormula2 = IncentivePaymentFormula2
                        .PayrollParameter.StartDateIncentivePayment2 = StartDateIncentivePayment2
                        .PayrollParameter.EndDateIncentivePayment2 = EndDateIncentivePayment2
                    End If
                End If

                .PayrollParameter.AproximationValue = INDCbeValueAproximation.EditValue
                .GroupEventConcept = CType(INDgcEventConcetps.DataSource, TrackableCollection(Of GroupEventConcept))
                .GroupAdjustConceptContractLiquidation = CType(INDGcConceptAdjust.DataSource, TrackableCollection(Of GroupAdjustConceptContractLiquidation))

                .PayrollParameter.AverageMonthVacationCompensation = MonthVacationCompensation

                '' Parametros Contables

                Dim InterfaceName As String
                Dim PayrollVoucherCode As String
                Dim PayrollVoucherName As String
                Dim ProvisionVoucherCode As String
                Dim ProvisionVoucherName As String
                Dim PrestacionesVoucherCode As String
                Dim PrestacionesVoucherName As String
                Dim PayrollAccount As String
                Dim ContractLiquidationAccount As String
                Dim VacationLiquidationAccount As String

                Dim IdPayrollAccount As Integer?
                Dim IdContractLiquidationAccount As Integer?
                Dim IdVacationLiquidationAccount As Integer?
                Dim IdPayrollVoucherType As Integer?
                Dim IdProvisionVoucherType As Integer?
                Dim IdPrestacionVoucherType As Integer?

                If indigo.IndigoPayrollIntegration = 1 Then
                    InterfaceName = indigo.TransactionalContainer
                    If INDSlPayrollVoucherVie.EditValue IsNot Nothing Then
                        Dim objetoSeleccion = DirectCast(DirectCast(INDSlPayrollVoucherVie.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.DocumentTypeXpo)
                        PayrollVoucherCode = INDSlPayrollVoucherVie.EditValue
                        PayrollVoucherName = INDSlPayrollVoucherVie.Text
                        IdPayrollVoucherType = objetoSeleccion.Id
                    Else
                        PayrollVoucherCode = Nothing
                        PayrollVoucherName = Nothing
                        IdPayrollVoucherType = Nothing
                    End If

                    If INDSlProvisionVoucherVie.EditValue IsNot Nothing Then
                        Dim objetoSeleccion = DirectCast(DirectCast(INDSlProvisionVoucherVie.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.DocumentTypeXpo)
                        ProvisionVoucherCode = INDSlProvisionVoucherVie.EditValue
                        ProvisionVoucherName = INDSlProvisionVoucherVie.Text
                        IdProvisionVoucherType = objetoSeleccion.Id
                    Else
                        ProvisionVoucherCode = Nothing
                        ProvisionVoucherName = Nothing
                    End If

                    If INDSlPrestacionVoucherVie.EditValue IsNot Nothing Then
                        Dim objetoSeleccion = DirectCast(DirectCast(INDSlPrestacionVoucherVie.GetSelectedDataRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.DocumentTypeXpo)
                        PrestacionesVoucherCode = INDSlPrestacionVoucherVie.EditValue
                        PrestacionesVoucherName = INDSlPrestacionVoucherVie.Text
                        IdPrestacionVoucherType = objetoSeleccion.Id
                    Else
                        PrestacionesVoucherCode = Nothing
                        PrestacionesVoucherName = Nothing
                    End If

                    If INDSlAccountVacationLiquidation.EditValue IsNot Nothing Then
                        IdVacationLiquidationAccount = INDSlAccountVacationLiquidation.EditValue

                        Dim ObjAccount = Await Model.GetAccont(INDSlAccountVacationLiquidation.EditValue)

                        If ObjAccount Is Nothing Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontró la Cuenta de Vacaciones"
                            Exit Function
                        End If

                        VacationLiquidationAccount = ObjAccount.Number
                    Else
                        IdVacationLiquidationAccount = Nothing
                        VacationLiquidationAccount = Nothing
                    End If

                    If INDSlPayrollAccountVie.EditValue IsNot Nothing Then

                        Dim ObjAccount = Await Model.GetAccont(INDSlPayrollAccountVie.EditValue)

                        If ObjAccount Is Nothing Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontró la Cuenta de Nómina"
                            Exit Function
                        End If
                        IdPayrollAccount = INDSlPayrollAccountVie.EditValue
                        PayrollAccount = ObjAccount.Number
                    Else
                        PayrollAccount = Nothing
                        IdPayrollAccount = Nothing
                    End If

                    If INDSlAccountContractLiquidation.EditValue IsNot Nothing Then

                        Dim ObjAccount = Await Model.GetAccont(INDSlAccountContractLiquidation.EditValue)

                        If ObjAccount Is Nothing Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontró la Cuenta de Liquidación de Contrato"
                            Exit Function
                        End If

                        IdContractLiquidationAccount = INDSlAccountContractLiquidation.EditValue
                        ContractLiquidationAccount = ObjAccount.Number
                    Else
                        IdContractLiquidationAccount = Nothing
                        ContractLiquidationAccount = Nothing
                    End If

                    .PayrollParameter.AccountedBy = INDrgContabilizacionVie.EditValue

                    'Ahora almacenamos los Id's de los Comprobantes Contables
                    .PayrollParameter.IdPayrollVoucherType = IdPayrollVoucherType
                    .PayrollParameter.IdProvisionVoucherType = IdProvisionVoucherType
                    .PayrollParameter.IdPrestacionVoucherType = IdPrestacionVoucherType
                    .PayrollParameter.IdPayrollAccount = IdPayrollAccount
                    .PayrollParameter.IdLiquidationContractAccount = IdContractLiquidationAccount
                    .PayrollParameter.LiquidationContractAccount = ContractLiquidationAccount
                    .PayrollParameter.LiquidationVacationAccount = VacationLiquidationAccount
                    .PayrollParameter.IdLiquidationVacationAccount = IdVacationLiquidationAccount
                    .PayrollParameter.IdIncentiveVoucherType = INDSlIncentiveVoucher.EditValue
                    .PayrollParameter.IdContractLiquidationVoucherType = INDSlContractLiquidationVoucher.EditValue
                    .PayrollParameter.IdUnemploymentVoucherType = INDSlUnemploymentVoucherType.EditValue
                    '.PayrollParameter.IdPayrollVoucherType = RetroactiveReceipt
                Else
                    InterfaceName = IIf(INDglCompany.EditValue IsNot Nothing, INDglCompany.EditValue, Nothing)
                    If INDglPayrollVoucher.EditValue IsNot Nothing Then
                        PayrollVoucherCode = INDglPayrollVoucher.EditValue
                        PayrollVoucherName = INDglPayrollVoucher.Text
                    Else
                        PayrollVoucherCode = Nothing
                        PayrollVoucherName = Nothing
                    End If

                    If INDglProvisionVoucher.EditValue IsNot Nothing Then
                        ProvisionVoucherCode = INDglProvisionVoucher.EditValue
                        ProvisionVoucherName = INDglProvisionVoucher.Text
                    Else
                        ProvisionVoucherCode = Nothing
                        ProvisionVoucherName = Nothing
                    End If

                    If INDglPrestacionesVoucher.EditValue IsNot Nothing Then
                        PrestacionesVoucherCode = INDglPrestacionesVoucher.EditValue
                        PrestacionesVoucherName = INDglPrestacionesVoucher.Text
                    Else
                        PrestacionesVoucherCode = Nothing
                        PrestacionesVoucherName = Nothing
                    End If

                    If INDglPayrollAccount.EditValue IsNot Nothing Then
                        PayrollAccount = INDglPayrollAccount.EditValue
                    Else
                        PayrollAccount = Nothing
                    End If
                    .PayrollParameter.AccountedBy = INDrgContabilizacion.EditValue
                End If

                .PayrollParameter.InterfaceName = InterfaceName
                .PayrollParameter.PayrollVoucherCode = PayrollVoucherCode
                .PayrollParameter.PayrollVoucherName = PayrollVoucherName

                .PayrollParameter.ProvisionVoucherCode = ProvisionVoucherCode
                .PayrollParameter.ProvisionVoucherName = ProvisionVoucherName

                .PayrollParameter.PrestacionVoucherCode = PrestacionesVoucherCode
                .PayrollParameter.PrestacionVoucherName = PrestacionesVoucherName

                '.PayrollParameter.IdInabilitiesVoucherType = IdInabilityVoucherType

                .PayrollParameter.PayrollAccount = PayrollAccount

            End With
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Funcion para validar campos cuando se registran conceptos a el grupo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateEvent() As Boolean
        ValidateEvent = True
        If INDCbeTypeDay.EditValue Is Nothing Then
            ValidateEvent = False
            INDCbeTypeDay.Focus()
            Exit Function
        End If
        If INDCbeTypeSchedule.EditValue Is Nothing Then
            ValidateEvent = False
            INDCbeTypeSchedule.Focus()
            Exit Function
        End If
        If INDGleConcept.EditValue Is Nothing Then
            ValidateEvent = False
            Exit Function
        End If

        If Group.GroupEventConcept.Count > 0 Then
            If Group.GroupEventConcept.Where(Function(x) x.ConceptId = INDGleConcept.EditValue And x.TypeDay = INDCbeTypeDay.EditValue _
                                                 And x.TypeSchedule = INDCbeTypeSchedule.EditValue).FirstOrDefault IsNot Nothing Then
                ValidateEvent = False
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(conceptoYaAgregado, groups), CType(INDGleConcept.Properties.DataSource, List(Of Concept)).Find(Function(x) x.Id = INDGleConcept.EditValue).Name)
                Exit Function
            End If
        End If
    End Function

    ''' <summary>
    ''' Funcion para validar campos cuando se registran conceptos a el grupo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateConceptoAdjust() As Boolean
        ValidateConceptoAdjust = True
        If INDSlConceptType.EditValue Is Nothing Then
            ValidateConceptoAdjust = False
            INDSlConceptType.Focus()
            Exit Function
        End If
        If INDSlConceptAdjust.EditValue Is Nothing Then
            ValidateConceptoAdjust = False
            INDSlConceptAdjust.Focus()
            Exit Function
        End If

        If Group.GroupAdjustConceptContractLiquidation.Count > 0 Then
            If Group.GroupAdjustConceptContractLiquidation.Any(Function(x) x.ConceptType = INDSlConceptType.EditValue) Then
                ValidateConceptoAdjust = False
                Mensaje(EeventViewerImages.Advertencia) = "Ya está agregada al listado ese Tipo de Concepto"
                Exit Function
            End If
        End If
    End Function

    ''' <summary>
    ''' MEtodo que limpia los controles de la seccion que registra eventos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControlsEventConcept()
        INDCbeTypeDay.EditValue = Nothing
        INDCbeTypeSchedule.EditValue = Nothing
        INDGleConcept.EditValue = Nothing
    End Sub

    Public Sub CleanControlsConceptAdjuts()
        INDSlConceptAdjust.EditValue = Nothing
        INDSlConceptType.EditValue = Nothing
    End Sub


#End Region

#Region "Events"
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
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Group = Nothing
        Model = Nothing
        Presenter = Nothing
        ModoBusqueda = Nothing
        FlagFormulate = False
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmGroups_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        'Ocultamos los controles de la configuración de primas
        INDLcgIncentivePayment1.HideControl(True)
        INDLcgIncentivePayment2.HideControl(True)

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollGroups.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PGroups(Me)
        Await Presenter.Initializes()
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        ActionsOnControls = False
        Initializes()
        Deshacer()

        LoadXpoConceptIncentive1()
        LoadXpoConceptIncentive2()

        Me.LoadStatus()
        Dim Model As New MGroups(MyBase.Tag)

        INDgleCompany.Properties.PopupFormSize = New System.Drawing.Size(500, 200)
        INDGleConcept.Properties.PopupFormSize = New System.Drawing.Size(500, 200)

        If indigo.IndigoPayrollIntegration = 1 Then
            INDLyComprobantes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLyComprobantesVie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Using msearch As New MBusqueda()
                DocumentTypePayrollVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypePrestacionVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypeProvisionVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypeInabilityVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypeContractLiquidationVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypeIncentiveVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypeUnemploymentVoucherXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                DocumentTypeRetroactiveReceiptXpo = msearch.ConsultarEntidades(eDataSource.ListJournalVoucherByState, "True")
                Dim filter() As Object = {5, True}
                ListAccountXpo = msearch.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
                ListAccountContractLiquidationXpo = msearch.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
                ListAccountVacationLiquidationXpo = msearch.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
            End Using

        Else
            INDLyComprobantes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyComprobantesVie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvConceptAdjust, ListActions)
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Group IsNot Nothing AndAlso Group.Id > 0 Then
            If Not (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If

        If record IsNot Nothing AndAlso record.Id > 0 Then
            If record.CodUser = indigo.UserIndigo Then
                DeleteBlockedRecord()
            End If
        End If
        INDBteCode.Text = Me.IdEntity.Trim()
        Me.LoadControls()
        Me.IdEntity = String.Empty
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
    End Sub

    ''' <summary>
    ''' Regarga El Grid look Up Edit cuando se cierre el PopUp 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleCompany_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs)
        Presenter.Initializes()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If Not String.IsNullOrEmpty(INDBteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                LoadControls()
                If INDBteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
                INDBteCode.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

#Region "Click"

    ''' <summary>
    ''' Evento para controlar la accion de agregar conceptos a el grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAddConcepts_Click(sender As Object, e As EventArgs) Handles INDSbAddConcepts.Click
        If ValidateEvent() = False Then
            Exit Sub
        End If
        Dim newEventConcept As GroupEventConcept = New GroupEventConcept()
        With newEventConcept
            .TypeDay = INDCbeTypeDay.EditValue
            .TypeSchedule = INDCbeTypeSchedule.EditValue
            .ConceptId = INDGleConcept.EditValue
            .Concept = CType(INDGleConcept.Properties.DataSource, List(Of Concept)).Find(Function(x) x.Id = INDGleConcept.EditValue)
        End With
        Group.GroupEventConcept.Add(newEventConcept)
        INDgcEventConcetps.DataSource = Group.GroupEventConcept
        INDgcEventConcetps.RefreshDataSource()
        CleanControlsEventConcept()
    End Sub

    Private Sub INDBtnAddConceptAdjust_Click(sender As Object, e As EventArgs) Handles INDBtnAddConceptAdjust.Click

        If ValidateConceptoAdjust() = False Then
            Exit Sub
        End If
        Dim newAdjustConcept As GroupAdjustConceptContractLiquidation = New GroupAdjustConceptContractLiquidation()
        With newAdjustConcept
            .IdConcept = INDSlConceptAdjust.EditValue
            .ConceptType = INDSlConceptType.EditValue
            .CreationUser = indigo.UserIndigo
            .CreationDate = GetDateServer()
            .Concept = CType(INDSlConceptAdjust.Properties.DataSource, List(Of Concept)).Find(Function(x) x.Id = INDSlConceptAdjust.EditValue)
        End With

        Group.GroupAdjustConceptContractLiquidation.Add(newAdjustConcept)
        INDGcConceptAdjust.DataSource = Nothing
        INDGcConceptAdjust.DataSource = Group.GroupAdjustConceptContractLiquidation
        INDGcConceptAdjust.RefreshDataSource()
        CleanControlsConceptAdjuts()
    End Sub

#End Region

#Region "CustomColumnDisplayText"
    ''' <summary>
    ''' Evento que controla el dispay text de las celadas de la rejilla de conceptos por grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvEventConcepts_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvEventConcepts.CustomColumnDisplayText
        If e.Column.Name = INDColECTypeDay.Name Then
            If e.Value = 1 Then
                e.DisplayText = obtenerRecurso(ordinario, groups)
            End If

            If e.Value = 2 Then
                e.DisplayText = obtenerRecurso(feriado, groups)
            End If
        End If
        If e.Column.Name = INDColECTypeSchedule.Name Then
            If e.Value = CType(1, Byte) Then
                e.DisplayText = obtenerRecurso(normal, groups)
            End If

            If e.Value = CType(2, Byte) Then
                e.DisplayText = obtenerRecurso(nocturno, groups)
            End If
        End If
    End Sub

    Private Sub INDGvConceptAdjust_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvConceptAdjust.CustomColumnDisplayText
        If e.Column.Name = INDColConceptType.Name Then
            If e.Value = 1 Then
                e.DisplayText = "Sueldo"
            End If

            If e.Value = 2 Then
                e.DisplayText = "Auxilio de Transporte"
            End If

            If e.Value = 3 Then
                e.DisplayText = "Aporte Salud"
            End If

            If e.Value = 4 Then
                e.DisplayText = "Aporte Pensión"
            End If

            If e.Value = 5 Then
                e.DisplayText = "Fondo de Solidaridad"
            End If
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Pinta el "Eliminar Registro" en las celdas de la rejilla de eventos por grupos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvEventConcepts_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvEventConcepts.CustomDrawCell
        If e.Column.Name = INDColDelete.Name Then
            e.DisplayText = obtenerRecurso(EliminarRegistro)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para eliminar los conceptos Almacenados en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepBtnDelete_Click(sender As Object, e As EventArgs) Handles RepBtnDelete.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _row As GroupEventConcept = INDgvEventConcepts.GetFocusedRow()
            _row.MarkAsDeleted()
            INDgvEventConcepts.FocusedColumn = INDgvEventConcepts.Columns.Item(0)
            INDgcEventConcetps.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmGroups_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' abre el form de empresas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleCompany_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleCompany.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCompany
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim frm As New FrmTransparent(Formulario, False)
                frm.ShowDialog()
                Presenter.Initializes()
            End Using
        End If
    End Sub

    Private Sub INDgleContractClass_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleContractClass.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmContractType
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim frm As New FrmTransparent(Formulario, False)
                frm.ShowDialog()
                Presenter.Initializes()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De los Conceptos de Primas 1
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptIncentive1()
        Using msearch As New MBusqueda
            DatasourceDecemberIncentiveXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDSlConceptIncentive1.Properties.DataSource = DatasourceDecemberIncentiveXpo
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De los Conceptos de Primas 1
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptIncentive2()
        Using msearch As New MBusqueda
            DatasourceDecemberIncentiveXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDSlConceptIncentive2.Properties.DataSource = DatasourceDecemberIncentiveXpo
        End Using
    End Sub
#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
        FlagFormulate = True
        ModoBusqueda = False
        ActionsOnControls = False
        Deshacer()
        FlagFormulate = False
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
        CleanControls()
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
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

#End Region

#Region "Customization"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCtlGroups.ShowCustomizationForm()
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
            INDlyCtlGroups.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtlGroups.ShowCustomization
        GetFieldsNULL()
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtlGroups.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCtlGroups.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCtlGroups.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyCtlGroups.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region


    Private Sub INDdeLastLiquidationDate_DrawItem(sender As Object, e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs) Handles INDdeLastLiquidationDate.DrawItem
        If INDrgLiquidation.SelectedIndex = 0 Then
            If (e.Date.Day <> 1) Then
                e.Style.ForeColor = Color.LightGray
            End If
        Else
            If (e.Date.Day <> 1 And e.Date.Day <> 16) Then
                e.Style.ForeColor = Color.LightGray
            End If
        End If

    End Sub

    Private Sub INDdeLastLiquidationDate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDdeLastLiquidationDate.KeyDown
        GroupLastLiquidationDate = INDdeLastLiquidationDate.EditValue 'asignamos el nuevo valor
    End Sub

    Private Sub INDdeLastLiquidationDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdeLastLiquidationDate.EditValueChanging

        If INDrgLiquidation.SelectedIndex = 0 Then
            If (Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 1) Then
                e.Cancel = True
            End If
        Else
            If (Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 1 And Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 16) Then
                e.Cancel = True
            End If

        End If

    End Sub

    Private Sub INDdeNextLiquidationDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdeNextLiquidationDate.EditValueChanging
        If INDrgLiquidation.SelectedIndex = 0 Then
            If (Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 1) Then
                e.Cancel = True
            End If
        Else
            If (Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 1 And Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 16) Then
                e.Cancel = True
            End If

        End If
    End Sub
    Private Sub INDdeNextLiquidationDate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDdeNextLiquidationDate.KeyDown
        GroupNextLiquidationDate = INDdeNextLiquidationDate.EditValue 'asignamos el nuevo valor
    End Sub
    Private Sub INDdeNextLiquidationDate_DrawItem(sender As Object, e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs) Handles INDdeNextLiquidationDate.DrawItem
        If INDrgLiquidation.SelectedIndex = 0 Then
            If (e.Date.Day <> 1) Then
                e.Style.ForeColor = Color.LightGray
            End If
        Else
            If (e.Date.Day <> 1 And e.Date.Day <> 16) Then
                e.Style.ForeColor = Color.LightGray
            End If
        End If
    End Sub


    Private Async Sub INDglCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDglCompany.EditValueChanged
        'Dim Container As GridLookUpEdit
        'Container = sender

        'Dim InterfaceAccount As GlosasParametersInterface = Container.EditValue

        If INDglCompany.EditValue IsNot Nothing Then
            Model = New MGroups(Tag)
            AsyncLoader(True)
            Dim ListTypeDocument = Await Model.ListTypeDocument(INDglCompany.EditValue)
            Dim PayrollAccount = Await Model.ListAccounts(INDglCompany.EditValue)
            AsyncLoader(False)

            If ListTypeDocument IsNot Nothing Then
                INDglPayrollVoucher.Properties.DataSource = ListTypeDocument
                INDglProvisionVoucher.Properties.DataSource = ListTypeDocument
                INDglPrestacionesVoucher.Properties.DataSource = ListTypeDocument
            End If

            INDglPayrollAccount.Properties.DataSource = PayrollAccount
        End If



    End Sub

    Private Sub INDglPayrollVoucher_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglPayrollVoucher.Properties.ButtonClick
        ''Botón de Borrar
        If e.Button.Index = 1 Then
            INDglPayrollVoucher.EditValue = Nothing
        End If
    End Sub

    Private Sub INDglProvisionVoucher_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglProvisionVoucher.Properties.ButtonClick
        ''Botón de Borrar
        If e.Button.Index = 1 Then
            INDglProvisionVoucher.EditValue = Nothing
        End If
    End Sub

    Private Sub INDglPrestacionesVoucher_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglPrestacionesVoucher.Properties.ButtonClick
        ''Botón de Borrar
        If e.Button.Index = 1 Then
            INDglPrestacionesVoucher.EditValue = Nothing
        End If
    End Sub

    Private Sub INDglPayrollAccount_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglPayrollAccount.Properties.ButtonClick
        ''Botón de Borrar
        If e.Button.Index = 1 Then
            INDglPayrollAccount.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(INDBteCode.Text) Then
            Dim state As Boolean
            Select Case Status
                Case CBool(eActionsStatusRecords.Active)
                    state = True
                Case CBool(eActionsStatusRecords.Inactive)
                    state = False
            End Select
            Using model As New MGroups(Me.Tag)
                AsyncLoader(True)
                Dim Result = Await model.ChangeStateGroup(INDBteCode.Text, state)
                AsyncLoader(False)
                If Result = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function



#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text
            Case "Eliminar"
                RemoveDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

#End Region

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        Dim _row As GroupAdjustConceptContractLiquidation = INDGvConceptAdjust.GetFocusedRow()
        _row.MarkAsDeleted()
        Group.GroupAdjustConceptContractLiquidation.Remove(_row)
        INDGvConceptAdjust.FocusedColumn = INDGvConceptAdjust.Columns.Item(0)
        INDGcConceptAdjust.DataSource = Nothing
        INDGcConceptAdjust.DataSource = Group.GroupAdjustConceptContractLiquidation
        INDGcConceptAdjust.RefreshDataSource()
    End Sub

    Private Sub INDrgHolidayPayment_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgHolidayPayment.EditValueChanged
        If Not INDrgHolidayPayment.EditValue Then
            INDlyItemVacationAdvanced.Enabled = False
            INDlyItemAverageMonthVacation.Enabled = False
            INDLciCompensationVacationMonth.Enabled = False
            INDlyItemtMaxVacation.Enabled = False
            INDlyItemVacationDay.Enabled = False
            INDrgVacationAdvanced.EditValue = False
            INDseAverageMonthVacation.EditValue = String.Empty
            INDseAverageMonthCompensationVacation.EditValue = String.Empty
            INDtxtMaxVacationByYear.EditValue = String.Empty
            INDseVacationDays.EditValue = String.Empty
        Else
            INDlyItemVacationAdvanced.Enabled = True
            INDlyItemAverageMonthVacation.Enabled = True
            INDLciCompensationVacationMonth.Enabled = True
            INDlyItemtMaxVacation.Enabled = True
            INDlyItemVacationDay.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Evento para desplegar fórmulas prima 1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDIncentivePayment1_Enter(sender As Object, e As EventArgs) Handles INDIncentivePayment1.Enter
        OpenFormulates(1)
    End Sub

    ''' <summary>
    ''' Evento para desplegar fórmulas prima 2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDIncentivePayment2_Enter(sender As Object, e As EventArgs) Handles INDIncentivePayment2.Enter
        OpenFormulates(2)
    End Sub

    ''' <summary>
    ''' Evento para Ocultar o Mostrar el Layout de configuración de Primas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtMaxPremiumByYear_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtMaxPremiumByYear.EditValueChanged
        Dim Primas As Integer = CInt(INDtxtMaxPremiumByYear.EditValue)
        Select Case Primas
            Case 0
                INDLcgIncentivePayment1.HideControl(True)
                INDLcgIncentivePayment2.HideControl(True)
            Case 1
                INDLcgIncentivePayment1.HideControl(False)
                INDLcgIncentivePayment2.HideControl(True)
            Case 2
                INDLcgIncentivePayment1.HideControl(False)
                INDLcgIncentivePayment2.HideControl(False)
        End Select
    End Sub
    ''' <summary>
    ''' Evento para asignar la fecha mínima de Fin de primas 1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDeIncentiveStarDate1_EditValueChanged(sender As Object, e As EventArgs) Handles INDDeIncentiveStarDate1.EditValueChanged
        ' Hacer Focus a la fecha fin 1
        INDDeIncentiveEndDate1.Focus()
    End Sub
    ''' <summary>
    ''' Evento para asignar la fecha mínima de Fin de primas 1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDeIncentiveStarDate2_EditValueChanged(sender As Object, e As EventArgs) Handles INDDeIncentiveStarDate2.EditValueChanged
        ' Hacer Focus a la fecha fin 2
        INDDeIncentiveEndDate2.Focus()
    End Sub
End Class