'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/10/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports Presentation.Contract
#End Region

Public Class FrmContract
    Implements IContract, ICustomizableForm

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ListTypes = New List(Of Tuple(Of String, Byte))
        ListTypes.Add(New Tuple(Of String, Byte)("EPS Contributivo", 1))
        ListTypes.Add(New Tuple(Of String, Byte)("EPS Subsidiado", 2))
        ListTypes.Add(New Tuple(Of String, Byte)("ET Vinculados Municipios", 3))
        ListTypes.Add(New Tuple(Of String, Byte)("ET Vinculados Departamentos", 4))
        ListTypes.Add(New Tuple(Of String, Byte)("ARL Riesgos Laborales", 5))
        ListTypes.Add(New Tuple(Of String, Byte)("MP Medicina Prepagada", 6))
        ListTypes.Add(New Tuple(Of String, Byte)("IPS Privada", 7))
        ListTypes.Add(New Tuple(Of String, Byte)("IPS Publica", 8))
        ListTypes.Add(New Tuple(Of String, Byte)("Regimen Especial", 9))
        ListTypes.Add(New Tuple(Of String, Byte)("Accidentes de transito", 10))
        ListTypes.Add(New Tuple(Of String, Byte)("Fosyga", 11))
        ListTypes.Add(New Tuple(Of String, Byte)("Otros", 12))

        Dim repositoryTmp = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        repositoryTmp.AutoHeight = False
        repositoryTmp.Name = "INDIcbSource"
        INDsleHealthAdministrator.Properties.RepositoryItems.Add(repositoryTmp)
        For Each item In ListTypes
            repositoryTmp.Items.Add(New DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Item1, item.Item2, -1))
        Next
        GridColumn7.ColumnEdit = repositoryTmp
    End Sub

#End Region

#Region "Properties"
    Private ListTypes As List(Of Tuple(Of String, Byte))
    ''' <summary>
    ''' Obtiene o establece el id de la entidad de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractEntityId As Integer? Implements IContract.ContractEntityId
        Get
            Return INDsleContractEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleContractEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de entidades de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.ContractEntityXpo
        Get
            Return INDsleContractEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContractEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Integer Implements IContract.Status
        Get
            Return CInt(BarraBotones.StatusRecord)
        End Get
        Set(value As Integer)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As ContractSequence Implements IContract.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As ContractSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequence.ContractSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IContract.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IContract.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IContract.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el objeto del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractObject As String Implements IContract.ContractObject
        Get
            Return INDmemoContractObject.Text
        End Get
        Set(value As String)
            INDmemoContractObject.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractValue As Decimal Implements IContract.ContractValue
        Get
            Return INDtxtContractValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtContractValue.EditValue = value
        End Set
    End Property

    Public Property ExecuteValue As Decimal
        Get
            Return INDTxtExecuteValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtExecuteValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad administradora de salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthAdministratorXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.HealthAdministratorXpo
        Get
            Return INDsleHealthAdministrator.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleHealthAdministrator.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad administradora de salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdHealthAdministrator As Integer? Implements IContract.IdHealthAdministrator
        Get
            Return INDsleHealthAdministrator.EditValue
        End Get
        Set(value As Integer?)
            INDsleHealthAdministrator.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PContract

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Representa la entidad de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim contract As Domain.Entities.Contract

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListPrintingMode As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListTerminationControl As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractDetail As ContractDetail

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractDetailNovelty As ContractDetailNovelty

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractDetailPolicy As ContractDetailPolicy

    ''' <summary>
    ''' Listado de detalles del contrato
    ''' </summary>
    Public ListContractDetail As List(Of ContractDetail)

    ''' <summary>
    ''' Listado de detalles de novedades del contrato
    ''' </summary>
    Public ListContractDetailNovelty As List(Of ContractDetailNovelty)

    ''' <summary>
    ''' Listado de detalles eliminados del contrato
    ''' </summary>
    Public ListDeleteContractDetail As List(Of ContractDetail)

    ''' <summary>
    ''' Listado de detalles de polizas del contrato
    ''' </summary>
    Public ListContractDetailPolicy As List(Of ContractDetailPolicy)

    ''' <summary>
    ''' Listado de detalles eliminados del contrato
    ''' </summary>
    Public ListDeleteContractDetailPolicy As List(Of ContractDetailPolicy)

    ''' <summary>
    ''' Listado de detalles eliminados del contrato
    ''' </summary>
    Public ListDeleteContractDetailNovelty As List(Of ContractDetailNovelty)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

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
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If contract IsNot Nothing AndAlso contract.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MContract(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteContract(contract)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
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
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        If contract.ContractValue > ContractValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El el nuevo valor del contrato no puede ser menor al valor anterior"
            Exit Sub
        End If
        Try
            AssigningValues()
            If (From x In contract.ContractDetail Where x.ValidRecord = True Select x).Count <> 1 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe existir un contrato vigente."
                Exit Sub
            End If

            Using model As New MContract(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveContract(contract, _idCurrentSequence)
                If Result.StateResult = True Then
                    If contract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf contract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.contract = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Not Result.Message.Equals("") Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewContract()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Entidad", .FieldName = "HealthAdministratorCodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "ContractName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Objeto", .FieldName = "ContractObject", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListContract
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que carga los detalles en las diferentes rejillas
    ''' </summary>
    Private Sub LoadDetails()
        INDviewDetails.ShowLoadingPanel()
        INDviewNovelty.ShowLoadingPanel()
        INDviewPolicy.ShowLoadingPanel()

        ListContractDetail = New List(Of ContractDetail)
        ListContractDetailNovelty = New List(Of ContractDetailNovelty)
        ListContractDetailPolicy = New List(Of ContractDetailPolicy)

        Task.Factory.StartNew(Sub()
                                  Dim listXPO = Presenter.GetContractDetailsByContractId(contract.Id)

                                  If listXPO IsNot Nothing AndAlso listXPO.Count > 0 Then
                                      For Each itemDetail In listXPO
                                          Dim detail As New ContractDetail
                                          With detail
                                              .Id = itemDetail.Id
                                              .ContractId = itemDetail.ContractId.Id
                                              .ContractNumber = itemDetail.ContractNumber
                                              .ContractName = itemDetail.ContractName
                                              .ContractNumberName = .ContractNumber + " - " + .ContractName
                                              .Type = itemDetail.Type
                                              .InitialDate = itemDetail.InitialDate
                                              .EndDate = itemDetail.EndDate
                                              .Legalized = itemDetail.Legalized
                                              .DateLegalization = itemDetail.DateLegalization
                                              .BillingInitialDate = itemDetail.BillingInitialDate
                                              .BillingEndDate = itemDetail.BillingEndDate
                                              .RadicatedBillingDate = itemDetail.RadicatedBillingDate
                                              .PercentageApplyPaymentSoon = itemDetail.PercentageApplyPaymentSoon
                                              If itemDetail.AgesPortfolioId IsNot Nothing Then
                                                  .AgesPortfolioId = itemDetail.AgesPortfolioId.Id
                                                  .AgesPortfolioName = itemDetail.AgesPortfolioId.Name
                                              End If
                                              .ValidRecord = itemDetail.ValidRecord
                                              .PrintingMode = itemDetail.PrintingMode
                                              .TerminationControl = itemDetail.TerminationControl
                                              .Observations = itemDetail.Observations
                                              .PermanentObservationOfTheInvoice = itemDetail.PermanentObservationOfTheInvoice
                                              .NotificationValueType = itemDetail.NotificationValueType
                                              .PercentageNotification = itemDetail.PercentageNotification
                                              .NotificationValue = itemDetail.NotificationValue
                                              .NotificationTimeType = itemDetail.NotificationTimeType
                                              .NotificationDays = itemDetail.NotificationDays
                                          End With

                                          If itemDetail.ContractDetailNoveltyXpo IsNot Nothing AndAlso itemDetail.ContractDetailNoveltyXpo.Count > 0 Then
                                              For Each itemDetailNovelty In itemDetail.ContractDetailNoveltyXpo
                                                  Dim detailNovelty As New ContractDetailNovelty
                                                  With detailNovelty
                                                      .Id = itemDetailNovelty.Id
                                                      .ContractDetailId = itemDetailNovelty.ContractDetailId.Id
                                                      .ContractNumberName = itemDetailNovelty.ContractDetailId.ContractNumberName
                                                      .NoveltyDate = itemDetailNovelty.NoveltyDate
                                                      .NoveltySource = itemDetailNovelty.NoveltySource
                                                      .Name = itemDetailNovelty.Name
                                                      .Status = itemDetailNovelty.Status
                                                      .Description = itemDetailNovelty.Description
                                                  End With
                                                  ListContractDetailNovelty.Add(detailNovelty)
                                              Next
                                          End If

                                          If itemDetail.ContractDetailPolicyXpo IsNot Nothing AndAlso itemDetail.ContractDetailPolicyXpo.Count > 0 Then
                                              For Each itemDetailPolicy In itemDetail.ContractDetailPolicyXpo
                                                  Dim detailPolicy As New ContractDetailPolicy
                                                  With detailPolicy
                                                      .Id = itemDetailPolicy.Id
                                                      .ContractDetailId = itemDetailPolicy.ContractDetailId.Id
                                                      .ContractNumberName = itemDetailPolicy.ContractDetailId.ContractNumberName
                                                      .FixedAssetPolicyId = itemDetailPolicy.FixedAssetPolicyId.Id
                                                      .FixedAssetPolicyDescription = itemDetailPolicy.FixedAssetPolicyId.CodeName
                                                      .FixedAssetInsuranceId = itemDetailPolicy.FixedAssetInsuranceId.Id
                                                      .FixedAssetInsuranceDescription = itemDetailPolicy.FixedAssetInsuranceId.CodeName
                                                      .PolicyNumber = itemDetailPolicy.PolicyNumber
                                                      .EmissionDate = itemDetailPolicy.EmissionDate
                                                      .AmountInsured = itemDetailPolicy.AmountInsured
                                                      .CoveragePercentage = itemDetailPolicy.CoveragePercentage
                                                      .Status = itemDetailPolicy.Status
                                                      .Observation = itemDetailPolicy.Observation
                                                  End With
                                                  ListContractDetailPolicy.Add(detailPolicy)
                                              Next
                                          End If

                                          ListContractDetail.Add(detail)
                                      Next
                                  End If

                                  INDgcDetails.BeginInvoke(Sub()
                                                               INDgcDetails.DataSource = ListContractDetail
                                                               INDviewDetails.HideLoadingPanel()
                                                           End Sub)

                                  INDgcNovelty.BeginInvoke(Sub()
                                                               INDgcNovelty.DataSource = ListContractDetailNovelty
                                                               INDviewNovelty.HideLoadingPanel()
                                                               INDviewNovelty.ExpandAllGroups()
                                                           End Sub)

                                  INDgcPolicy.BeginInvoke(Sub()
                                                              INDgcPolicy.DataSource = ListContractDetailPolicy
                                                              INDviewPolicy.HideLoadingPanel()
                                                              INDviewPolicy.ExpandAllGroups()
                                                          End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Edita un detalle
    ''' </summary>
    Private Sub EditDetail()
        ContractDetail = DirectCast(INDviewDetails.GetFocusedRow(), ContractDetail)
        IndexEditRecord = ListContractDetail.IndexOf(ContractDetail)
        OpenFormDetails(True)
    End Sub

    ''' <summary>
    ''' Edita un detalle de novedad
    ''' </summary>
    Private Sub EditDetailNovelty()
        ContractDetailNovelty = DirectCast(INDviewNovelty.GetFocusedRow(), ContractDetailNovelty)
        IndexEditRecord = ListContractDetailNovelty.IndexOf(ContractDetailNovelty)
        OpenFormDetailsNovelty(True, ContractDetailNovelty.ContractNumberName)
    End Sub

    ''' <summary>
    ''' Edita un detalle de poliza
    ''' </summary>
    Private Sub EditDetailPolicy()
        ContractDetailPolicy = DirectCast(INDviewPolicy.GetFocusedRow(), ContractDetailPolicy)
        IndexEditRecord = ListContractDetailPolicy.IndexOf(ContractDetailPolicy)
        OpenFormDetailsPolicy(True, ContractDetailPolicy.ContractNumberName)
    End Sub

    ''' <summary>
    ''' Elimina un detalle
    ''' </summary>
    Private Sub DeleteDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entityDelete = DirectCast(INDviewDetails.GetFocusedRow(), ContractDetail)

        Dim errors As New StringBuilder

        If ListContractDetailNovelty IsNot Nothing AndAlso ListContractDetailNovelty.Count > 0 Then
            If (From x In ListContractDetailNovelty Where x.ContractNumberName = entityDelete.ContractNumberName).Count > 0 Then
                errors.AppendLine("No se puede eliminar el registro porque tiene novedades asociadas")
            End If
        End If

        If ListContractDetailPolicy IsNot Nothing AndAlso ListContractDetailPolicy.Count > 0 Then
            If (From x In ListContractDetailPolicy Where x.ContractNumberName = entityDelete.ContractNumberName).Count > 0 Then
                errors.AppendLine("No se puede eliminar el registro porque tiene polizas asociadas")
            End If
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        If entityDelete.Id > 0 Then
            If ListDeleteContractDetail Is Nothing Then
                ListDeleteContractDetail = New List(Of ContractDetail)
            End If
            entityDelete.MarkAsDeleted()
            ListDeleteContractDetail.Add(entityDelete)
        End If
        ListContractDetail.Remove(entityDelete)
        INDgcDetails.DataSource = Nothing
        INDgcDetails.DataSource = ListContractDetail
        Mensaje(EeventViewerImages.Informacion) = "Item eliminado de la rejilla correctamente"
    End Sub

    ''' <summary>
    ''' Establece el ancho de la columna mas info
    ''' </summary>
    Private Sub WidthActionsColumns()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewDetails.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewNovelty.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewPolicy.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que abre el modal de detalles para agregar los contratos
    ''' </summary>
    Private Sub OpenFormDetails(editMode As Boolean)
        Using formulario As New FrmContractDetail()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddDetailsArgs, AddressOf ReturnAddDetailsEventArgs
            formulario.EditMode = editMode
            formulario.ContractDetail = ContractDetail
            formulario.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            formulario.FrmContract = Me
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(830, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que recibe el retornno del modal de detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddDetailsEventArgs(sender As Object, e As AddDetailsEventArgs)
        If e IsNot Nothing Then
            If e.EditMode = False Then
                If ListContractDetail Is Nothing Then
                    ListContractDetail = New List(Of ContractDetail)
                End If
                ListContractDetail.Add(e.ContractDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else
                ListContractDetail.Remove(ContractDetail)
                ListContractDetail.Insert(IndexEditRecord, e.ContractDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcDetails.DataSource = Nothing
            INDgcDetails.DataSource = ListContractDetail
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el modal de detalles de novedades para agregar los contratos
    ''' </summary>
    Private Sub OpenFormDetailsNovelty(editMode As Boolean, contractNumberName As String)
        Using formulario As New FrmContractDetailNovelty()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddDetailsNoveltyArgs, AddressOf ReturnAddDetailsNoveltyEventArgs
            formulario.EditMode = editMode
            formulario.ContractDetailNovelty = ContractDetailNovelty
            formulario.ContractNumberName = contractNumberName
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(830, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que recibe el retornno del modal de detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddDetailsNoveltyEventArgs(sender As Object, e As AddDetailsNoveltyEventArgs)
        If e IsNot Nothing Then
            If e.EditMode = False Then
                If ListContractDetailNovelty Is Nothing Then
                    ListContractDetailNovelty = New List(Of ContractDetailNovelty)
                End If
                ListContractDetailNovelty.Add(e.ContractDetailNovelty)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else
                ListContractDetailNovelty.Remove(ContractDetailNovelty)
                ListContractDetailNovelty.Insert(IndexEditRecord, e.ContractDetailNovelty)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcNovelty.DataSource = Nothing
            INDgcNovelty.DataSource = ListContractDetailNovelty
            INDviewNovelty.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el modal de detalles de polizas para agregar los contratos
    ''' </summary>
    Private Sub OpenFormDetailsPolicy(editMode As Boolean, contractNumberName As String)
        Using formulario As New FrmContractDetailPolicy()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddDetailsPolicyArgs, AddressOf ReturnAddDetailsPolicyEventArgs
            formulario.EditMode = editMode
            formulario.ContractDetailPolicy = ContractDetailPolicy
            formulario.ContractNumberName = contractNumberName
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(830, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que recibe el retornno del modal de detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddDetailsPolicyEventArgs(sender As Object, e As AddDetailsPolicyEventArgs)
        If e IsNot Nothing Then
            If e.EditMode = False Then
                If ListContractDetailPolicy Is Nothing Then
                    ListContractDetailPolicy = New List(Of ContractDetailPolicy)
                End If
                ListContractDetailPolicy.Add(e.ContractDetailPolicy)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else
                ListContractDetailPolicy.Remove(ContractDetailPolicy)
                ListContractDetailPolicy.Insert(IndexEditRecord, e.ContractDetailPolicy)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcPolicy.DataSource = Nothing
            INDgcPolicy.DataSource = ListContractDetailPolicy
            INDviewPolicy.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IContract.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDsleHealthAdministrator.Enabled = value
            INDsleContractEntity.Enabled = value
            INDtxtContractValue.Enabled = value
            INDmemoContractObject.Enabled = value
            INDbtnAddDetails.Enabled = value
            INDgcDetails.Enabled = value
            INDgcNovelty.Enabled = value
            INDgcPolicy.Enabled = value
            If value Then
                INDsleHealthAdministrator.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.contract IsNot Nothing AndAlso Me.contract.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.contract.Code, Me.contract.ContractName),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.contract.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.contract.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.contract.Code, Me.contract.ContractName)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.contract.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("Current"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("Suspended"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("Finished"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyContract.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        IdHealthAdministrator = Nothing
        INDsleHealthAdministrator.Properties.NullText = String.Empty
        ContractEntityId = Nothing
        INDsleContractEntity.Properties.NullText = String.Empty
        ContractValue = 0
        ContractObject = String.Empty
        ExecuteValue = 0
        INDgcDetails.DataSource = Nothing
        INDgcNovelty.DataSource = Nothing
        INDgcPolicy.DataSource = Nothing
        ListContractDetail = Nothing
        ListDeleteContractDetail = Nothing
        ListContractDetailNovelty = Nothing
        ListDeleteContractDetailNovelty = Nothing
        ListContractDetailPolicy = Nothing
        ListDeleteContractDetailPolicy = Nothing
        ContractDetail = Nothing
        ContractDetailNovelty = Nothing
        ContractDetailPolicy = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyContract.EndUpdate()

        INDLciExecuteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        contract = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With contract
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = Me._idOperativeUnit
            .Code = Code
            .HealthAdministratorId = IdHealthAdministrator
            .ContractEntityId = ContractEntityId
            .ContractValue = ContractValue
            .ContractObject = ContractObject

            If ListContractDetail IsNot Nothing AndAlso ListContractDetail.Count > 0 Then
                For Each itemDetail In ListContractDetail
                    If ListContractDetailNovelty IsNot Nothing AndAlso ListContractDetailNovelty.Count > 0 Then
                        Dim list = ListContractDetailNovelty.Where(Function(item) item.ContractNumberName = itemDetail.ContractNumberName).ToList()
                        list.ForEach(Sub(item) itemDetail.ContractDetailNovelty.Add(item))
                    End If

                    If ListContractDetailPolicy IsNot Nothing AndAlso ListContractDetailPolicy.Count > 0 Then
                        Dim list = ListContractDetailPolicy.Where(Function(item) item.ContractNumberName = itemDetail.ContractNumberName).ToList()
                        list.ForEach(Sub(item) itemDetail.ContractDetailPolicy.Add(item))
                    End If

                    .ContractDetail.Add(itemDetail)
                Next
            End If

            If ListDeleteContractDetail IsNot Nothing AndAlso ListDeleteContractDetail.Count > 0 Then
                ListDeleteContractDetail.ForEach(Sub(item) .ContractDetail.Add(item.MarkAsDeleted()))
            End If

            If contract.ChangeTracker.State = ObjectState.Added Then
                .Status = 1
            End If

            If .Id > 0 Then
                .ExecuteValue = ExecuteValue
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
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
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MContract(CStr(Me.Tag))
                    AsyncLoader(True)
                    contract = (Await Model.GetContract(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyContract.BeginUpdate()
                    If contract IsNot Nothing AndAlso contract.Id > 0 Then
                        LoadDetails()
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(contract.Id))
                            With contract
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                IdHealthAdministrator = .HealthAdministratorId
                                INDsleHealthAdministrator.Properties.NullText = .HealthAdministratorDescription
                                ContractEntityId = .ContractEntityId
                                INDsleContractEntity.Properties.NullText = .ContractEntityDescription
                                ContractValue = .ContractValue
                                ContractObject = .ContractObject
                                ExecuteValue = .ExecuteValue
                                Status = .Status.ToString
                                PrepareTool(.Status)
                            End With
                            INDLciExecuteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.contract.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = contract.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            'Me.BarraBotones.SetDocuments(contract.Id, Me.Tag.ToString(), Nothing, GetType(Domain.Entities.Contract).Name)
                            'Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewContract()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyContract.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDviewDetails.HideLoadingPanel()
                INDviewNovelty.HideLoadingPanel()
                INDviewPolicy.HideLoadingPanel()
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If



        'Me.BarraBotones.StatusRecordVisible = True
        'Using Model As New MContract(CStr(Me.Tag))
        '    AsyncLoader(True)
        '    Dim resultOperation = Await Model.GetContract(INDbtnCode.Text.Trim)
        '    AsyncLoader(False)
        '    contract = resultOperation.ObjectEmbbeded
        '    If Not contract Is Nothing Then
        '        If contract.Id > 0 Then
        '            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(contract.Id))
        '                With contract
        '                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                    Code = .Code
        '                    IdHealthAdministrator = .HealthAdministratorId
        '                    INDsleHealthAdministrator.Properties.NullText = .HealthAdministratorDescription
        '                    ContractEntityId = .ContractEntityId
        '                    INDsleContractEntity.Properties.NullText = .ContractEntityDescription
        '                    ContractName = .ContractName
        '                    ContractNumber = .ContractNumber
        '                    ContractValue = .ContractValue
        '                    ContractObject = .ContractObject
        '                    InitialDate = .InitialDate
        '                    EndDate = .EndDate
        '                    Legalized = .Legalized
        '                    DateLegalization = .DateLegalization
        '                    PrintingMode = .PrintingMode
        '                    TerminationControl = .TerminationControl
        '                    Observations = .Observations
        '                    ExecuteValue = .ExecuteValue

        '                    NotificationValueType = .NotificationValueType
        '                    PercentageNotification = .PercentageNotification
        '                    NotificationValue = .NotificationValue
        '                    NotificationTimeType = .NotificationTimeType
        '                    NotificationDays = .NotificationDays

        '                    If .Status = 1 Then
        '                        BarraBotones.StatusRecord = eActionsStatusRecords.Active
        '                    Else
        '                        BarraBotones.StatusRecord = .Status.ToString
        '                    End If
        '                    PrepareTool(.Status)
        '                End With
        '                INDLciExecuteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.contract.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = contract.Id}
        '                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                    record = operation.ObjectEmbbeded
        '                Else
        '                    record = result
        '                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                End If
        '                Me.BarraBotones.SetDocuments(contract.Id)
        '                ActionsOnControls = True
        '            End Using
        '        Else
        '            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            'Me.Code = String.Empty
        '            If Me._sequence.IsManual Then
        '                Await Me.NewContract()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                INDbtnCode.Focus()
        '            End If
        '        End If
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        'Me.Code = String.Empty
        '        If Me._sequence.IsManual Then
        '            Await Me.NewContract()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.Code = String.Empty
        '            INDbtnCode.Focus()
        '        End If
        '    End If
        'End Using
    End Function

    ''' <summary>
    ''' Prepara la barra de acciones
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub PrepareTool(state As Integer)
        BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Select Case state
            Case 1
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = False
            Case 2
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = False
            Case 3
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = True
        End Select
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewContract() As Task
        contract = New Domain.Entities.Contract()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If



        'contract = New Domain.Entities.Contract
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Function
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState(state As Integer) As Task
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MContract(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await model.ChangeState(Code, state)
                    If Result.StateResult = True Then
                        Status = state
                        Me.contract = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                        'PrepareTool(state)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequence = Nothing
        contract = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        ListPrintingMode = Nothing
        ListTerminationControl = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContract, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PContract(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.AddNovelty)
        ListActions.Add(eAcciones.AddPolicy)
        IndigoGridView1.SetListAcction(INDviewDetails, ListActions)

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(INDviewNovelty, ListActions2)
        IndigoGridView3.SetListAcction(INDviewPolicy, ListActions2)

        WidthActionsColumns()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContract_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewContract()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del mas del control de entidades administradoras de salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthAdministrator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleHealthAdministrator.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmHealthAdministrator With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeHealtAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del mas del control de entidades de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContractEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmContractEntity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContractEntity()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.DataSource Is Nothing Then
            Presenter.InitializeHealtAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de entidades de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractEntity.QueryPopUp
        If INDsleContractEntity.Properties.DataSource Is Nothing Then
            Presenter.InitializeContractEntity()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDetails_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetails.Click
        OpenFormDetails(False)
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual rejilla detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
            Case "AddNovelty"
                Dim entity = DirectCast(INDviewDetails.GetFocusedRow(), ContractDetail)
                OpenFormDetailsNovelty(False, entity.ContractNumberName)
            Case "AddPolicy"
                Dim entity = DirectCast(INDviewDetails.GetFocusedRow(), ContractDetail)
                OpenFormDetailsPolicy(False, entity.ContractNumberName)
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual rejilla detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
            Case "AddNovelty"
                Dim entity = DirectCast(INDviewDetails.GetFocusedRow(), ContractDetail)
                OpenFormDetailsNovelty(False, entity.ContractNumberName)
            Case "AddPolicy"
                Dim entity = DirectCast(INDviewDetails.GetFocusedRow(), ContractDetail)
                OpenFormDetailsPolicy(False, entity.ContractNumberName)
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual rejilla detalles de novedades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        EditDetailNovelty()
    End Sub

    ''' <summary>
    ''' Menu contextual rejilla detalles de novedades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        EditDetailNovelty()
    End Sub

    ''' <summary>
    ''' Menu contextual rejilla detalles de polizas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        EditDetailPolicy()
    End Sub

    ''' <summary>
    ''' Menu contextual rejilla detalles de polizas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        EditDetailPolicy()
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewNovelty_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewNovelty.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolNoveltySource.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Cliente"
                Case 2
                    e.DisplayText = "Prestador"
                Case Else
                    e.DisplayText = ""
            End Select
        End If

        If e.Column.Name = INDcolNoveltyStatus.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Aprobada"
                Case 2
                    e.DisplayText = "Rechazada"
                Case 3
                    e.DisplayText = "Pendiente"
                Case 4
                    e.DisplayText = "En Proceso"
                Case 5
                    e.DisplayText = "Terminada"
                Case Else
                    e.DisplayText = ""
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GridView3_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewPolicy.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolPolicyStatus.Name Then
            Select Case e.Value
                Case 0
                    e.DisplayText = "Inactivo"
                Case 1
                    e.DisplayText = "Activo"
                Case Else
                    e.DisplayText = ""
            End Select
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState(1)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.ContractSequenceDetail IsNot Nothing Then
                If Not Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Click terminar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_Terminar() Handles BarraBotones.Click_Terminar
        Await ChangeState(3)
    End Sub

    ''' <summary>
    ''' Click Suspender
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender
        Await ChangeState(2)
    End Sub

#End Region

End Class