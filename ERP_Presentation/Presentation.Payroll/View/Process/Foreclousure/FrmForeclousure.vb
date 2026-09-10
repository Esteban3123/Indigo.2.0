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
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FrmForeclousure
    Implements IForeclousure

#Region "Variable Globales Propiedades Interfaz y Load"

    ''' <summary>
    ''' variable para saber el modo de busqueda del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModoBusqueda As Boolean = False

    Public Property ListThirdParty As XPInstantFeedbackSource Implements IForeclousure.ListThirdParty
        Get
            Return INDSlApplicant.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlApplicant.Properties.DataSource = value
        End Set
    End Property

    Public Property ListBeneficiaryThirdParty As XPInstantFeedbackSource Implements IForeclousure.ListBeneficiaryThirdParty
        Get
            Return INDSlBeneficiary.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlBeneficiary.Properties.DataSource = value
        End Set
    End Property


    Public Property ListCity As XPInstantFeedbackSource Implements IForeclousure.ListCity
        Get
            Return INDSlCity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlCity.Properties.DataSource = value
        End Set
    End Property

    '' <summary>
    '' Obtiene o asigna una lista de compañias
    '' </summary>
    '' <value></value>
    '' <returns></returns>
    '' <remarks></remarks>
    Public Property ListCompany As List(Of Domain.Payroll.Entities.Company) Implements IForeclousure.ListCompany
        Get
            Return CType(Me.INDSlJudgment.Properties.DataSource, List(Of Domain.Payroll.Entities.Company))
        End Get
        Set(value As List(Of Domain.Payroll.Entities.Company))
            Me.INDSlJudgment.Properties.DataSource = value.Where(Function(x) x.ForeclousureType = True).ToList()
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o asigna lista de conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListConcept As List(Of Domain.Payroll.Entities.Concept) Implements IForeclousure.ListConcept
        Get
            Return CType(Me.INDgleConcept.Properties.DataSource, List(Of Domain.Payroll.Entities.Concept))
        End Get
        Set(value As List(Of Domain.Payroll.Entities.Concept))
            Me.INDgleConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Asigna lista de empleados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public WriteOnly Property ListEmployee As Object Implements IForeclousure.ListEmployee
        Set(value As Object)
            Me.INDgleEmployee.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad para controlar  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Property DataSourceForeclousureDetail As List(Of ForeclousureDetail)
        Get
            Return CType(Me.INDgcForeclousureDetail.DataSource, List(Of ForeclousureDetail))
        End Get
        Set(value As List(Of ForeclousureDetail))
            Me.INDgcForeclousureDetail.DataSource = value
            Me.INDgcForeclousureDetail.RefreshDataSource()
        End Set
    End Property

    Public Property Sequense As PayrollSequence Implements IForeclousure.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PayrollSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequence.PayrollSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Code As String Implements IForeclousure.Code
        Get
            If (INDBteConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteConsecutive.Text
            End If
        End Get
        Set(value As String)
            INDBteConsecutive.Text = value
        End Set
    End Property

    Public Property QuoteValue As Decimal? Implements IForeclousure.QuoteValue
        Get
            Return INDtxtQuoteValue.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtQuoteValue.EditValue = value
        End Set
    End Property

    Public Property StatusForeclousure As Integer Implements IForeclousure.StatusForeclousure
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Integer)

        End Set
    End Property

    ''' Propiedad de valor compartido
    ''' </summary>
    ''' <returns></returns>
    Public Property ShareValuePaid As Decimal
        Get
            Return INDtxtShareValuePaid.EditValue
        End Get
        Set(value As Decimal)
            INDtxtShareValuePaid.EditValue = value
        End Set
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
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MForeclousure
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PForeclousure
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
    Dim ObjEmployee As Domain.Payroll.Entities.Employee
    ''' <summary>
    ''' Id Grupo de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim Idgroup As Integer

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListForeclousureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de Descuentos
    ''' </summary>
    Dim ListDiscountClass As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PayrollSequence

    Dim Foreclousure As Foreclousure


    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Dim IdConcept As Integer?

    Dim ObjContract As New Domain.Payroll.Entities.Contract

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        ObjEmployee = Nothing
        Idgroup = Nothing
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
        Me.Model = New MForeclousure(Me.Tag)
        Me.indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PForeclousure(Me)
        Presenter.GetSequense()
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        Deshacer()
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        Me.ActionsOnControls = False
        Presenter.LoadDataAsync()
        BarraBotones.RibbonPageProcesos.Visible = False
        Me.INDdeDatePayment.EditValue = Date.Now
        InitializeTuple()

    End Sub

    Private Sub InitializeTuple()

        'Lista de Tipos de Embargos
        ListForeclousureType = New List(Of Tuple(Of Integer, String))
        ListForeclousureType.Add(New Tuple(Of Integer, String)(1, "Embargos por Obligaciones Comunes"))
        ListForeclousureType.Add(New Tuple(Of Integer, String)(2, "Embargos por Obligaciones Alimentarias y/o Cooperativas"))
        ListForeclousureType.Add(New Tuple(Of Integer, String)(3, "Embargos Judiciales de Prestaciones Sociales"))
        INDgleKindsForeclousure.Properties.DataSource = ListForeclousureType.ToList

        'Lista de Clases de Descuentos
        ListDiscountClass = New List(Of Tuple(Of Integer, String))
        ListDiscountClass.Add(New Tuple(Of Integer, String)(1, "Porcentaje"))
        ListDiscountClass.Add(New Tuple(Of Integer, String)(2, "Valor Fijo"))
        ListDiscountClass.Add(New Tuple(Of Integer, String)(3, "Valor Fijo con Saldo"))
        ListDiscountClass.Add(New Tuple(Of Integer, String)(4, "Porcentaje con Saldo"))
        INDSLDiscountClass.Properties.DataSource = ListDiscountClass.ToList


    End Sub

#End Region

#Region "ICRUD"
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If

        Dim listItemsColumnEdit As New List(Of Tuple(Of String, String))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Sin Confirmar", "1"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Confirmado", "2"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Suspendido", "3"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Terminado", "5"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Anulado", "4"))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo", .FieldName = "Code"},
                              New ColumnInfo With {.Caption = "Nit", .FieldName = "IdEmployee.ThirdPartyId.Nit"},
                              New ColumnInfo With {.Caption = "Empleado", .FieldName = "IdEmployee.ThirdPartyId.Name"},
                              New ColumnInfo With {.Caption = "Juzgado", .FieldName = "IdJudgment.Descripcion"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "State", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEdit}}.ToList
            '.ValorSolicitado = "Consecutive"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListForeclousure
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
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
        INDBteConsecutive.Text = ReturnValue
        If INDBteConsecutive.Text <> String.Empty Then
            LoadControls()
            If INDBteConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteConsecutive.Enabled = False
        End If
    End Sub


    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            If ValidateControls() = True Then
                If Foreclousure.State <> 3 Then
                    AssigningValues()
                End If
                Using model As New MForeclousure(Tag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveForeclousure(Foreclousure, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If Foreclousure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If Foreclousure.State = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf Foreclousure.State = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf Foreclousure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If Foreclousure.State = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf Foreclousure.State = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf Foreclousure.State = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If

                        Me.Foreclousure = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBteConsecutive.Enabled = False
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf Result.MessageResult(0) IsNot Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString()
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBteConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

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

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewForeclousure()
        End If
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IForeclousure.ActionsOnControls
        Set(value As Boolean)

            'INDlyForeclousure.Enabled = True
            INDlyForeclousure.BeginUpdate()
            INDBteConsecutive.Enabled = Not value
            INDgleEmployee.Enabled = value
            'INDgleCompany.Enabled = value
            INDgleKindsForeclousure.Enabled = value
            INDmeComment.Enabled = value
            INDSLDiscountClass.Enabled = value
            INDSlApplicant.Enabled = value

            INDrgLiquidationType.Enabled = value
            ' INDrgTermtype.Enabled = value
            INDtxtAgreementValue.Enabled = value
            INDtxtNumberShares.Enabled = value
            INDgleConcept.Enabled = value
            INDdeStartingDate.Enabled = value
            INDtxtQuoteValue.Enabled = value

            INDSpPercentage.Enabled = value
            INDtxtShareValuePaid.Enabled = value
            INDdeDatePayment.Enabled = value
            INDmeCommentsD.Enabled = value

            INDbtnAddAgreementsD.Enabled = value
            BarraBotones.RibbonPageProcesos.Visible = False
            INDRgAffectVacation.Enabled = value
            INDRgAffect1erIncentive.Enabled = value
            INDRgAffect2doIncentive.Enabled = value
            INDRgAffectUnemployment.Enabled = value
            INDSlJudgment.Enabled = value
            INDSlCity.Enabled = value
            INDTxtTradeNumber.Enabled = value
            INDDeTradeDate.Enabled = value
            INDSlBeneficiary.Enabled = value
            INDTxtProcessNumber.Enabled = value
            INDTxtCodeOffice.Enabled = value
            INDgcForeclousureDetail.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value

            INDlyForeclousure.EndUpdate()
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
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IForeclousure.AsyncLoader
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
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmFunctionalAgreementsMetaData, Eform.InfoMetaData), Me.Foreclousure.Code, Me.INDgleEmployee.Text, Me.INDSlApplicant.Text, Me.INDgleKindsForeclousure.Text),
                                                .CreationDate = dateServer,
                                                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName,
                                                .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me.Foreclousure.Code & "#$",
                                                .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmFunctionalAgreementsMetaDataTitle, Eform.InfoMetaData), Me.Foreclousure.Code),
                                                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmKindsAgrementsMetaData, Eform.InfoMetaData), Me.Foreclousure.Code, Me.INDgleEmployee.Text, Me.INDSlApplicant.Text, Me.INDgleKindsForeclousure.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFunctionalAgreementsMetaDataTitle, Eform.InfoMetaData), Me.Foreclousure.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task

        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MForeclousure(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlcgAgreements.BeginUpdate()
                    Foreclousure = Await Model.GetForeclousure(Me.INDBteConsecutive.Text)
                    If Foreclousure IsNot Nothing AndAlso Foreclousure.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            Dim record = Await ModelRecord.GetBlockRecord(Me.Tag, Foreclousure.Id)
                            With Foreclousure
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)

                                'Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                                INDBteConsecutive.Text = .Code
                                INDgleEmployee.EditValue = .IdEmployee
                                INDgleKindsForeclousure.EditValue = .ForeclousureType
                                INDmeComment.Text = .Comment
                                INDSlApplicant.EditValue = .IdApplicant
                                INDSlApplicant.Properties.NullText = .NameThirdPartyApplicant
                                INDrgLiquidationType.EditValue = .DiscountType
                                INDtxtNumberShares.EditValue = .QuoteNumber
                                INDSLDiscountClass.EditValue = .DiscountClass
                                QuoteValue = .QuoteValue
                                INDtxtQuoteValue.EditValue = .QuoteValue
                                INDSpPercentage.EditValue = .Percentage
                                INDtxtAgreementValue.EditValue = .TotalValue
                                IdConcept = .IdConcept
                                INDgleConcept.EditValue = .IdConcept
                                INDgleConcept.Properties.NullText = .NameConcept
                                'INDgleConcept.Text = .NameConcept
                                INDdeStartingDate.EditValue = .InitialDate
                                INDRgAffectVacation.EditValue = .AffectVacation
                                INDRgAffect1erIncentive.EditValue = .Affect1erIncentivePayment
                                INDRgAffect2doIncentive.EditValue = .Affect2doIncentivePayment
                                INDRgAffectUnemployment.EditValue = .AffectUnemploymentValue
                                INDSlJudgment.EditValue = .IdJudgment
                                INDSlJudgment.Properties.NullText = .NameCompanyJudgment
                                INDSlCity.EditValue = .IdCity
                                INDSlCity.Properties.NullText = .NameCity
                                INDTxtTradeNumber.EditValue = .TradeNumber
                                INDDeTradeDate.EditValue = .TradeDate
                                INDSlBeneficiary.EditValue = .IdBeneficiary
                                INDSlBeneficiary.Properties.NullText = .NameThirdPartyBeneficiary
                                INDTxtProcessNumber.EditValue = .ProcessNumber
                                INDtxtCurrentBalance.EditValue = .CurrentBalance
                                BarraBotones.StatusRecord = .State.ToString
                                StatusForeclousure = .State
                                INDTxtCodeOffice.EditValue = .CodeDestinationOffice
                                DataSourceForeclousureDetail = .ForeclousureDetail.ToList()

                                If .DiscountClass = 3 Or .DiscountClass = 4 Then
                                    CalculateShare()
                                End If

                            End With
                            INDgcForeclousureDetail.DataSource = Nothing
                            INDgcForeclousureDetail.DataSource = Foreclousure.ForeclousureDetail.ToList()

                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Foreclousure.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPayroll With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = Foreclousure.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(Foreclousure.Id, Me.Tag.ToString(), Nothing, GetType(Foreclousure).Name)
                            ' Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewForeclousure()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = "El Código no existe"
                            Code = String.Empty
                            INDBteConsecutive.Focus()
                        End If
                    End If
                    INDlcgAgreements.EndUpdate()
                End Using

                If StatusForeclousure = 1 Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                ElseIf StatusForeclousure = 2 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = False
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                    Me.BarraBotones.RibbonPageProcesos.Visible = True

                    INDlcgGeneralInfo.Enabled = False
                    INdlcgContractualInformation.Enabled = False
                    INDlcgAgreements.Enabled = False
                    INDLcgAfectations.Enabled = True
                    INDLcgJudgment.Enabled = False
                    INDlcgLiquidationPayroll.Enabled = True
                ElseIf StatusForeclousure = 3 Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = False

                    INDlcgGeneralInfo.Enabled = False
                    INdlcgContractualInformation.Enabled = False
                    INDlcgAgreements.Enabled = False
                    INDLcgAfectations.Enabled = False
                    INDLcgJudgment.Enabled = False
                    INDlcgLiquidationPayroll.Enabled = False
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Reactivar) = True

                    INDlcgGeneralInfo.Enabled = False
                    INdlcgContractualInformation.Enabled = False
                    INDlcgAgreements.Enabled = False
                    INDLcgAfectations.Enabled = False
                    INDLcgJudgment.Enabled = False
                    INDlcgLiquidationPayroll.Enabled = False
                End If

            Catch ex As Exception
                AsyncLoader(False)
                INDBteConsecutive.Enabled = False
                Throw ex
            End Try
        End If

    End Function

    Private Async Function NewForeclousure() As Task
        Me.Foreclousure = New Foreclousure()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = "1"
        StatusForeclousure = 1
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If


    End Function

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyForeclousure.BeginUpdate()
        Me.Foreclousure = Nothing
        Me.ObjEmployee = Nothing
        'ListEmployee = Nothing

        INDlcgGeneralInfo.Enabled = True
        INdlcgContractualInformation.Enabled = True
        INDlcgAgreements.Enabled = True
        INDlcgLiquidationPayroll.Enabled = True
        INDLcgAfectations.Enabled = True
        INDLcgJudgment.Enabled = True
        INDlcgLiquidationPayroll.Enabled = True

        Me.INDBteConsecutive.Text = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        INDgleEmployee.EditValue = Nothing

        INDgleKindsForeclousure.EditValue = Nothing
        INDmeComment.Text = String.Empty

        INDtxtContract.Text = String.Empty

        INDtxtBusinessUnit.Text = String.Empty
        INDtxtFunctionalUnit.Text = String.Empty
        INDtxtCharges.Text = String.Empty
        INDtxtDateContract.Text = String.Empty
        INDtxtBasicSalary.Text = String.Empty
        Idgroup = Nothing
        INDSlApplicant.EditValue = String.Empty
        INDSlApplicant.Text = String.Empty
        INDSlApplicant.Properties.NullText = String.Empty
        INDtxtNumberShares.EditValue = String.Empty
        INDrgLiquidationType.EditValue = String.Empty
        INDSLDiscountClass.EditValue = String.Empty
        INDSpPercentage.EditValue = String.Empty
        INDRgAffectVacation.SelectedIndex = 1
        INDRgAffect1erIncentive.SelectedIndex = 1
        INDRgAffect2doIncentive.SelectedIndex = 1
        INDRgAffectUnemployment.SelectedIndex = 1
        INDSlJudgment.EditValue = String.Empty
        INDSlJudgment.Properties.NullText = String.Empty
        INDSlJudgment.Text = String.Empty
        INDSlCity.EditValue = String.Empty
        INDSlCity.Properties.NullText = String.Empty
        INDSlCity.Text = String.Empty
        INDTxtTradeNumber.EditValue = String.Empty
        INDDeTradeDate.EditValue = String.Empty
        INDSlBeneficiary.EditValue = String.Empty
        INDSlBeneficiary.Text = String.Empty
        INDSlBeneficiary.Properties.NullText = String.Empty
        INDTxtProcessNumber.EditValue = String.Empty

        INDtxtAgreementValue.Text = String.Empty
        INDgleConcept.Text = String.Empty
        INDgleConcept.Properties.NullText = String.Empty
        INDgleConcept.EditValue = String.Empty
        INDdeStartingDate.EditValue = String.Empty
        INDtxtQuoteValue.Text = String.Empty
        INDTxtCodeOffice.EditValue = String.Empty
        INDTxtCodeOffice.Text = String.Empty
        INDtxtCurrentBalance.Text = String.Empty
        INDgcForeclousureDetail.DataSource = New List(Of ForeclousureDetail)

        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyForeclousure.EndUpdate()
        Me.ActionsOnControls = False
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        Dim errores As New List(Of String)

        'Helper para validar null / vacío (incluye strings con espacios)
        Dim IsEmpty As Func(Of Object, Boolean) =
        Function(v As Object) As Boolean
            If v Is Nothing OrElse v Is DBNull.Value Then Return True
            Dim s = TryCast(v, String)
            If s IsNot Nothing Then Return String.IsNullOrWhiteSpace(s)
            Return False
        End Function

        If IsEmpty(INDBteConsecutive.EditValue) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiConsecutive.Text))
        End If

        If IsEmpty(INDgleEmployee.EditValue) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiEmployee.Text))
        End If

        If IsEmpty(INDgleKindsForeclousure.EditValue) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiForeclousureType.Text))
        End If

        If IsEmpty(INDSlApplicant.EditValue) Then
            errores.Add("Falta diligenciar el campo Demandante")
        End If

        If IsEmpty(INDrgLiquidationType.EditValue) Then
            errores.Add("Falta diligenciar el campo Tipo de Descuento")
        End If

        ' Si el tipo de liquidación = 1 -> requiere Número de Cuotas
        If Not IsEmpty(INDrgLiquidationType.EditValue) Then
            Dim tipo As Integer
            If Integer.TryParse(CStr(INDrgLiquidationType.EditValue), tipo) AndAlso tipo = 1 Then
                If IsEmpty(INDtxtNumberShares.EditValue) Then
                    errores.Add("Falta diligenciar el campo Número de Cuotas")
                End If
            End If
        End If

        ' Clase de Descuento
        If IsEmpty(INDSLDiscountClass.EditValue) Then
            errores.Add("Falta diligenciar el campo Clase de Descuentos")
        Else
            Dim clase As Integer
            If Integer.TryParse(CStr(INDSLDiscountClass.EditValue), clase) Then
                Select Case clase
                    Case 1 ' Porcentaje
                        If IsEmpty(INDSpPercentage.EditValue) Then
                            errores.Add("Falta diligenciar el campo Porcentaje")
                        End If
                    Case 2 ' Valor cuota
                        If IsEmpty(INDtxtQuoteValue.EditValue) Then
                            errores.Add("Falta diligenciar el campo Valor Cuota")
                        End If
                    Case 3, 4 ' Valor total
                        If IsEmpty(INDtxtAgreementValue.EditValue) AndAlso String.IsNullOrWhiteSpace(INDtxtAgreementValue.Text) Then
                            errores.Add("Falta diligenciar el campo Valor Total")
                        End If
                End Select
            End If
        End If

        If String.IsNullOrWhiteSpace(INDtxtAgreementValue.Text) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiTotalValue.Text))
        End If

        If IsEmpty(INDgleConcept.EditValue) Then
            errores.Add("Falta diligenciar el campo Concepto")
        End If

        If IsEmpty(INDdeStartingDate.EditValue) Then
            errores.Add("Falta diligenciar el campo Fecha Inicial")
        End If

        If IsEmpty(INDRgAffectVacation.EditValue) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDRgAffectVacation.Text))
        End If

        If IsEmpty(INDRgAffect1erIncentive.EditValue) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDRgAffect1erIncentive.Text))
        End If

        If IsEmpty(INDRgAffect2doIncentive.EditValue) Then
            errores.Add(String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDRgAffect2doIncentive.Text))
        End If

        If IsEmpty(INDSlJudgment.EditValue) Then
            errores.Add("Falta diligenciar el campo Juzgado")
        End If

        If IsEmpty(INDSlCity.EditValue) Then
            errores.Add("Falta diligenciar el campo Municipio")
        End If

        If IsEmpty(INDTxtTradeNumber.EditValue) Then
            errores.Add("Falta diligenciar el campo Número de Oficio")
        End If

        If IsEmpty(INDDeTradeDate.EditValue) Then
            errores.Add("Falta diligenciar el campo Fecha de Oficio")
        End If

        If IsEmpty(INDSlBeneficiary.EditValue) Then
            errores.Add("Falta diligenciar el campo Beneficiario")
        End If

        If IsEmpty(INDTxtProcessNumber.EditValue) Then
            errores.Add("Falta diligenciar el campo Número de Proceso")
        End If

        If IsEmpty(INDTxtCodeOffice.EditValue) Then
            errores.Add("Falta diligenciar el campo Código Oficina Destino, necesario para el Archivo Plano del Banco")
        End If

        ' --- Resultado unificado ---
        If errores.Count > 0 Then
            ' Evitar mensajes repetidos y mostrarlos en un bloque
            Dim unico = errores.Distinct().ToList()
            Mensaje(EeventViewerImages.Advertencia) =
            "Por favor complete los siguientes campos:" & Environment.NewLine &
            "- " & String.Join(Environment.NewLine & "- ", unico)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Foreclousure
            .Code = Code
            .IdEmployee = INDgleEmployee.EditValue
            .InitialContract = ObjContract.InitialContractNumber
            .IdContract = ObjContract.Id
            .ForeclousureType = INDgleKindsForeclousure.EditValue
            .Comment = INDmeComment.EditValue
            ' .State = Me.BarraBotones.StatusRecord
            .IdApplicant = INDSlApplicant.EditValue
            .DiscountType = INDrgLiquidationType.EditValue
            .DiscountClass = INDSLDiscountClass.EditValue


            If INDlyiNumberShares.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .QuoteNumber = CInt(INDtxtNumberShares.EditValue)
            Else
                .QuoteNumber = 0
            End If

            If INDlyiQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .QuoteValue = CDbl(INDtxtQuoteValue.EditValue)
            Else
                .QuoteValue = 0
            End If

            If INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Percentage = CDec(INDSpPercentage.EditValue)
            Else
                .Percentage = 0
            End If

            If INDlyiTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TotalValue = CDbl(INDtxtAgreementValue.EditValue)
            Else
                .TotalValue = 0
            End If

            If IdConcept Is Nothing Then
                IdConcept = INDgleConcept.EditValue
            End If

            .IdConcept = IdConcept
            .InitialDate = INDdeStartingDate.EditValue
            .AffectVacation = INDRgAffectVacation.EditValue
            .Affect1erIncentivePayment = INDRgAffect1erIncentive.EditValue
            .Affect2doIncentivePayment = INDRgAffect2doIncentive.EditValue
            .AffectUnemploymentValue = INDRgAffectUnemployment.EditValue
            .IdJudgment = INDSlJudgment.EditValue
            .IdCity = INDSlCity.EditValue
            .TradeNumber = INDTxtTradeNumber.EditValue
            .TradeDate = INDDeTradeDate.EditValue
            .IdBeneficiary = INDSlBeneficiary.EditValue
            .ProcessNumber = INDTxtProcessNumber.EditValue
            .CodeDestinationOffice = INDTxtCodeOffice.EditValue

            If Foreclousure.Id = 0 Then
                If INDSLDiscountClass.EditValue = 3 Or INDSLDiscountClass.EditValue = 4 Then
                    .CurrentBalance = INDtxtAgreementValue.EditValue
                End If
            End If

        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    Private Async Sub INDBteConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewForeclousure()
                Else
                    Await LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
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

        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = "Suspendido", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = "Anulado", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "5", .StatusName = "Terminado", .StatusColor = System.Drawing.Color.OrangeRed})


        Me.BarraBotones.States = listStates
        'Me.BarraBotones.StatusRecordEnabled = False
        ' Me.BarraBotones.Enabled = False
    End Sub

    ''' <summary>
    ''' Cargar datos del Empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadEmployeeData()

        AsyncLoader(True)
        If Me.INDgleEmployee.EditValue IsNot Nothing Then
            ObjEmployee = Await Model.GetEmployee(Me.INDgleEmployee.EditValue)
            If ObjEmployee.Id > 0 Then
                With ObjEmployee
                    If .Contract IsNot Nothing AndAlso .Contract.Count > 0 Then
                        ObjContract = .Contract.Where(Function(item) item.Valid = True).FirstOrDefault 'cargo solo informacion del contrato activo
                        Idgroup = ObjContract.GroupId

                        If ObjContract IsNot Nothing Then
                            INDtxtContract.Text = ObjContract.Id.ToString
                            'If ObjContract.Group IsNot Nothing AndAlso ObjContract.Group.Company IsNot Nothing Then
                            '    INDtxtEmployeeCompany.Text = ObjContract.Group.Company.Name
                            'End If
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

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que consulta Info. Empleado una ves seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleEmployee.EditValueChanged
        If Foreclousure IsNot Nothing Then
            If Me.INDgleEmployee.EditValue IsNot Nothing Then
                LoadEmployeeData()
            End If
        End If
    End Sub

    Private Sub INDSLDiscountClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDSLDiscountClass.EditValueChanged
        If INDSLDiscountClass.EditValue.ToString() IsNot Nothing AndAlso INDSLDiscountClass.EditValue.ToString() <> "" Then

            If INDSLDiscountClass.EditValue = 1 Or INDSLDiscountClass.EditValue = 4 Then
                'Por porcentaje
                INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If INDSLDiscountClass.EditValue = 4 Then
                    INDlyiTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyiCurrentBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyiTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyiCurrentBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Else
                INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiQuoteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                If INDSLDiscountClass.EditValue = 3 Then
                    INDlyiTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyiCurrentBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyiCurrentBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyiTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If
        End If

    End Sub

    Private Sub INDrgLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgLiquidationType.EditValueChanged

        If INDrgLiquidationType.EditValue.ToString() IsNot String.Empty AndAlso INDrgLiquidationType.EditValue.ToString() <> "" Then

            If INDrgLiquidationType.EditValue = 2 Then
                INDlyiNumberShares.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlyiNumberShares.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If

    End Sub
#End Region
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
        'BarraBotones.RibbonPageProcesos.Visible = False
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
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
        Foreclousure.State = EnumStateAgreements.eUnconfirmed
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
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Foreclousure.State = EnumStateAgreements.eConfirmed
        Guardar()
        'If Foreclousure IsNot Nothing Then
        '    If MessageIndigo.Show(obtenerRecurso(ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Foreclousure.State = EnumStateAgreements.eConfirmed
        '        AsyncLoader(True)
        '        Dim result As ActionResult(Of Foreclousure) = Await Model.SaveForeclousure(Foreclousure, _idCurrentSequence)
        '        AsyncLoader(False)
        '        If result.StateResult = True Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '            CleanControls()
        '        Else
        '            If result.MessageResult.Count > 0 Then
        '                If result.MessageResult(0) = "-999" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub
    ''' <summary>
    ''' Guardar y Confirmar un convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Foreclousure.State = EnumStateAgreements.eConfirmed
        Guardar()
    End Sub
    ''' <summary>
    ''' Anualr un convenio
    ''' </summary>
    Private Async Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If Foreclousure IsNot Nothing Then
            Using _FrmCommentChangeState As New FrmForeclousureCommentChangeState
                _FrmCommentChangeState.State = 4
                _FrmCommentChangeState.Foreclusure = Foreclousure
                Dim transparent As New FrmTransparent(_FrmCommentChangeState, False)
                transparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                transparent.ShowDialog()
                Await LoadControls()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Suspender un contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender
        If Foreclousure IsNot Nothing Then
            Using _FrmCommentChangeState As New FrmForeclousureCommentChangeState
                _FrmCommentChangeState.State = EnumStateAgreements.eSuspended
                _FrmCommentChangeState.Foreclusure = Foreclousure
                Dim transparent As New FrmTransparent(_FrmCommentChangeState, False)
                transparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                transparent.ShowDialog()
                Await LoadControls()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Click boton reactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickReactivar() Handles BarraBotones.ClickReactivar
        If Foreclousure IsNot Nothing Then
            Using _FrmCommentChangeState As New FrmForeclousureCommentChangeState
                Foreclousure.State = 2
                _FrmCommentChangeState.State = 2
                _FrmCommentChangeState.Foreclusure = Foreclousure
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
        INDlyForeclousure.ShowCustomizationForm()
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
            INDlyForeclousure.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyForeclousure.ShowCustomization
        'Try
        '    'Ejecuatamos la consulta
        '    AsyncLoader(True)
        '    Dim dsFields As DataSet = Await Model.GetFieldsNULL
        '    AsyncLoader(False)
        '    If dsFields IsNot Nothing Then
        '        dtFieldsCustomizables = dsFields.Tables(0)
        '        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
        '            For j As Integer = 0 To INDlyAgreements.Items.Count - 1
        '                If Object.Equals(INDlyAgreements.Items.Item(j).Tag, Nothing) = False Then
        '                    If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyAgreements.Items.Item(j).Tag.ToString.Trim Then
        '                        INDlyAgreements.Items.Item(j).AllowHide = True
        '                    End If
        '                End If
        '            Next
        '        Next
        '    End If
        'Catch ex As Exception
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        '    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        'End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyForeclousure.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyForeclousure.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyForeclousure.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyForeclousure.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "Handlers"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Foreclousure IsNot Nothing AndAlso Me.Foreclousure.Id > 0 Then
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
    ''' Agregar un nuevo detalle de convenio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAgreementsD_Click(sender As Object, e As EventArgs) Handles INDbtnAddAgreementsD.Click
        Dim ObjForeclousureDetail As New ForeclousureDetail
        If INDtxtShareValuePaid.EditValue IsNot Nothing And INDdeDatePayment.EditValue IsNot Nothing Then

            With ObjForeclousureDetail
                .IdForeclousure = Foreclousure.Id
                If INDtxtShareValuePaid.Text <> String.Empty Then
                    .ShareValuePaid = ShareValuePaid
                End If
                .DatePayment = INDdeDatePayment.EditValue
                .TypePayment = 2
                .StateShare = INDmeCommentsD.Text
            End With

            If INDSLDiscountClass.EditValue = 1 Or INDSLDiscountClass.EditValue = 2 Then
                If ValidateCurrentBalance(ObjForeclousureDetail.ShareValuePaid) = False Then
                    Mensaje(EeventViewerImages.MensajeError) = String.Format(obtenerRecurso(PaidValueExceeds, Eform.Agreements), Me.INDtxtCurrentBalance.Text)
                Else
                    CalculateCurrentBalance()
                End If
            End If

            DataSourceForeclousureDetail.Add(ObjForeclousureDetail)
            Foreclousure.ForeclousureDetail.Add(ObjForeclousureDetail)
            Me.INDgcForeclousureDetail.RefreshDataSource()

            INDtxtShareValuePaid.Focus()

            INDtxtShareValuePaid.Text = String.Empty
            INDdeDatePayment.EditValue = Nothing
            INDmeCommentsD.Text = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' Eliminar un detalle de convenio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteDelete_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteDelete.ButtonClick
        Dim ObjAgreeTmp = CType(INDgvAgreementsD.GetRow(INDgvAgreementsD.FocusedRowHandle), ForeclousureDetail)
        If ObjAgreeTmp IsNot Nothing Then
            Foreclousure.ForeclousureDetail.ToList().ForEach(Sub(item)
                                                                 If item.Id = ObjAgreeTmp.Id Then
                                                                     Foreclousure.CurrentBalance = Foreclousure.CurrentBalance + item.ShareValuePaid
                                                                     Me.INDtxtCurrentBalance.EditValue = Foreclousure.CurrentBalance
                                                                     item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                                                                 End If
                                                             End Sub)
            DataSourceForeclousureDetail.Remove(ObjAgreeTmp)
            INDgcForeclousureDetail.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Calcula el saldo actual del convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateCurrentBalance()
        Dim SumD As Decimal = 0
        If DataSourceForeclousureDetail.Count > 0 Then
            For i = 0 To DataSourceForeclousureDetail.Count - 1
                SumD = SumD + DataSourceForeclousureDetail(i).ShareValuePaid
            Next
            Foreclousure.CurrentBalance = Foreclousure.TotalValue - SumD
            Me.INDtxtCurrentBalance.EditValue = Foreclousure.CurrentBalance
        End If
    End Sub
    ''' <summary>
    ''' Metodo para validar que los pagos no superen el valor del saldo
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateCurrentBalance(ByVal ValorAdd As Decimal)
        Dim SumD As Decimal = 0
        If DataSourceForeclousureDetail.Count > 0 Then
            For i = 0 To DataSourceForeclousureDetail.Count - 1
                SumD = SumD + DataSourceForeclousureDetail(i).ShareValuePaid
            Next
            If SumD + ValorAdd > Foreclousure.TotalValue Then
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
                        e.DisplayText = "Pago por Cesantias"
                    Case "6"
                        e.DisplayText = "Pago por Primas 1er Semestre"
                    Case "7"
                        e.DisplayText = "Pago por Primas 2do Semestre"
                    Case Else
                        e.DisplayText = obtenerRecurso(TiponoReconocido, RecepcionObjeciones)
                End Select
            End If
        End If
    End Sub



#End Region

    ''' <summary>
    ''' Metodo para calcular el valor total del numero de cuotas y el valor de la cuota
    ''' </summary>
    ''' <remarks></remarks>
    Sub CalculateTotal()
        If INDtxtNumberShares.EditValue.ToString() <> "" AndAlso INDtxtQuoteValue.EditValue.ToString() <> "" Then
            INDtxtAgreementValue.EditValue = CDec(INDtxtNumberShares.Text) * CDec(INDtxtQuoteValue.Text)
        End If
    End Sub

    Private Sub INDtxtNumberShares_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtNumberShares.KeyUp
        CalculateTotal()
    End Sub

    Sub CalculateShare()
        If INDtxtAgreementValue.EditValue.ToString() <> "" AndAlso INDtxtNumberShares.EditValue.ToString() <> "" Then
            If CDec(INDtxtNumberShares.EditValue) > 0 Then
                INDtxtQuoteValue.EditValue = CDec(INDtxtAgreementValue.EditValue) / CDec(INDtxtNumberShares.EditValue)
            ElseIf CDec(INDtxtNumberShares.EditValue) = 0 Then
                INDtxtQuoteValue.EditValue = CDec(INDtxtAgreementValue.EditValue)
            End If
        End If
    End Sub

#Region "QueryPopUp"
    Private Sub INDSlApplicant_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlApplicant.QueryPopUp
        If INDSlApplicant.Properties.DataSource Is Nothing Then
            Presenter.LoadListThirdParty()
        End If
    End Sub

    Private Sub INDSlCity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlCity.QueryPopUp
        If INDSlCity.Properties.DataSource Is Nothing Then
            Presenter.LoadListCity()
        End If
    End Sub

    Private Sub INDSlBeneficiary_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlBeneficiary.QueryPopUp
        If INDSlBeneficiary.Properties.DataSource Is Nothing Then
            Presenter.LoadListThirdPartyBeneficiary()
        End If
    End Sub

    Private Sub INDSlJudgment_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlJudgment.QueryPopUp
        If INDSlJudgment.Properties.DataSource Is Nothing Then
            Presenter.LoadCompany()
        End If
    End Sub

    Private Sub INDgleConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleConcept.QueryPopUp
        If INDgleConcept.Properties.DataSource Is Nothing Then
            Presenter.LoadConcept()
        End If
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub INDSlApplicant_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlApplicant.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.LoadListThirdParty()
        End If
    End Sub

    Private Sub INDSlCity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlCity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(513, Nothing, True)
            Presenter.LoadListCity()
        End If
    End Sub

    Private Sub INDSlBeneficiary_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlBeneficiary.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.LoadListThirdPartyBeneficiary()
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
            OpenForm(529, Nothing, True)
            Presenter.LoadDataAsync()
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
            OpenForm(549, Nothing, True)
            Presenter.LoadConcept()
        End If
    End Sub

    Private Sub INDSlJudgment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlJudgment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(525, Nothing, True)
            Presenter.LoadCompany()
        End If
    End Sub



#End Region

End Class