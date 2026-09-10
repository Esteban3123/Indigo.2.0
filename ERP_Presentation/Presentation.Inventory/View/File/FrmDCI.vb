'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/09/2014
'
' Last Modified By : Henry Alejandro Vargas Polania 
' Last Modified On : 23/12/2014
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmDCI
    Implements IDCI, ICustomizableForm

#Region "Properties"
    Private _isLoading As Boolean = False
    ''' <summary>
    ''' presentacion de servcios
    ''' </summary>
    ''' <remarks></remarks>
    Private _listRiskLevel As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListPresentationProduct As List(Of Tuple(Of Integer, String))
        Get
            If _listRiskLevel Is Nothing Then
                _listRiskLevel = New List(Of Tuple(Of Integer, String))
                _listRiskLevel.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("DCI_RiskLevel_Severe", NAME_MODULE)))
                _listRiskLevel.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("DCI_RiskLevel_Moderate", NAME_MODULE)))
                _listRiskLevel.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("DCI_RiskLevel_Minor", NAME_MODULE)))
            End If
            Return _listRiskLevel
        End Get
    End Property

    ''' <summary>
    ''' Lista el tipo de medicamento LASA
    ''' </summary>
    ''' <remarks></remarks>
    Private _TypesSimilarity As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListTypesSimilarity As List(Of Tuple(Of Integer, String))
        Get
            If _TypesSimilarity Is Nothing Then
                _TypesSimilarity = New List(Of Tuple(Of Integer, String))
                _TypesSimilarity.Add(New Tuple(Of Integer, String)(1, "Suena igual"))
                _TypesSimilarity.Add(New Tuple(Of Integer, String)(2, "Se parece"))
                _TypesSimilarity.Add(New Tuple(Of Integer, String)(3, "Suena igual y se parece"))
            End If
            Return _TypesSimilarity
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IDCI.Status
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
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As InventorySequence Implements IDCI.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDCI.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IDCI.MyTag
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
    Public Property Code As String Implements IDCI.Code
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
    ''' Obtiene o establece el nombre del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameDCI As String Implements IDCI.NameDCI
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si le medicamento es combinado
    ''' </summary>
    ''' <returns></returns>
    Public Property Combined As Boolean Implements IDCI.Combined
        Get
            Return INDSleCombined.EditValue
        End Get
        Set(value As Boolean)
            INDSleCombined.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de advertencia
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeWarning As Boolean
        Get
            Return INDSleTypeWarning.EditValue
        End Get
        Set(value As Boolean)
            INDSleTypeWarning.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDCI.ActionsOnControls
        Set(value As Boolean)
            INDlyDCI.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDbtnCode.Enabled = Not value
            INDSleCombined.Enabled = value
            INDSleTypeWarning.Enabled = value
            INDtxtName.Enabled = value
            INDPceDrugInteractions.Enabled = value
            INDPceLethalDoseParams.Enabled = value
            INDPceRisksDescription.Enabled = value
            INDPceDCIRiskFactor.Enabled = value
            INDPceLASADrugs.Enabled = value
            INDPceHighRiskDrugs.Enabled = value
            INDgcDrugInteractions.Enabled = value
            INDgcLethalDoseParameters.Enabled = value
            INDgcDCIRiskFactor.Enabled = value
            INDgcRisksDescription.Enabled = value
            INDgcLASADrug.Enabled = value
            INDgcHighRiskDRugs.Enabled = value
            INDGcDrugActive.Enabled = value
            INDsleATCActive.Enabled = value
            INDBtAdd.Enabled = value
            INDSbAddAtc.Enabled = value
            INDGcATCs.Enabled = value
            INDSleATC.Enabled = value
            INDlyDCI.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la forma farmaceutica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ATCParentsXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IDCI.ATCParentsXpo
        Get
            Return CType(INDsleATCActive.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleATCActive.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PDCI

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As InventorySequence

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim dci As DCI

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
    Private record As BlockRecordInventory

    ''' <summary>
    ''' variable que contiene el drug interaction actual
    ''' </summary>
    ''' <remarks></remarks>
    Dim CurrentDrugInteraction As DrugInteraction

    ''' <summary>
    ''' Variable que contiene los parametros de dosis maximas letales actuales
    ''' </summary>
    Dim CurrentLethalDoseLimits As LethalDoseLimits

    ''' <summary>
    ''' Variable que contiene las descripciones de los riesgos actuales
    ''' </summary>
    Dim CurrentRisksDescription As RisksDescription

    ''' <summary>
    ''' variable que contiene el drug alto riesgo actual
    ''' </summary>
    ''' <remarks></remarks>
    Dim CurrentDrugHighRisk As HighRiskDrugs

    ''' <summary>
    ''' variable que contiene el drug LASA actual
    ''' </summary>
    ''' <remarks></remarks>
    Dim CurrentDrugsLASA As IHPARAMDCI

    ''' <summary>
    ''' Lista de Medicamentos eliminados
    ''' </summary>
    Dim ListDeleteDrugInteraction As List(Of DrugInteraction)

    ''' <summary>
    ''' Lista de obtejos a Eliminar de la entidad
    ''' </summary>
    Dim ListDeleteDrugActive As List(Of DrugActive)

    ''' <summary>
    ''' Lista de parametros de dosis letales a eliminar
    ''' </summary>
    Dim ListDeleteLethalDoseLimits As List(Of LethalDoseLimits)

    ''' <summary>
    ''' Lista de factores de riesgo a eliminar
    ''' </summary>
    Dim ListDeleteDCIRiskFactor As List(Of DCIRiskFactors)

    ''' <summary>
    ''' Lista de descripción de riesgos a eliminar
    ''' </summary>
    Dim ListDeleteRisksDescription As List(Of RisksDescription)

    Dim CurrentDrugActive As DrugActive

    ''' <summary>
    ''' Lista de tipos de riesgos
    ''' </summary>
    Dim ListRisksDescription As List(Of Tuple(Of Decimal, String))

    ''' <summary>
    ''' Lista de parametros de edades para dosis letales/maximas
    ''' </summary>
    Dim ListAgeRange As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de parametros de peso para dosis letales/maximas
    ''' </summary>
    Dim ListWeightRange As List(Of Tuple(Of Integer, String))



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
        If Me.dci IsNot Nothing AndAlso Me.dci.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MDCI(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteDCI(Me.dci)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            'Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
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
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        If dci.Combined AndAlso Not dci.DrugActive1.Any() Then
            Mensaje(EeventViewerImages.MensajeError) = "Aún no registra ninguna sustancia activa, por favor agregue las sustancias que integran el medicamento a parametrizar"
            Return
        End If

        Try
            Using Model As New MDCI(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of DCI) = Await Model.SaveDCI(Me.dci, ListDeleteDrugInteraction, ListDeleteDrugActive, ListDeleteLethalDoseLimits, ListDeleteRisksDescription, ListDeleteDCIRiskFactor, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If dci.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.dci = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
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
            Await NewDCI()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)},
                              New ColumnInfo() With {.Caption = "Combinado", .FieldName = "CombinedName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDCI
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.dci IsNot Nothing AndAlso Me.dci.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
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
        Await DeleteBlockedRecord()
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.dci.Code, Me.dci.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.dci.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.dci.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.dci.Code, Me.dci.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.dci.Code)
            Return Me._doc
        End If
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
    Private Sub CleanControls()
        INDlyDCI.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        Code = String.Empty
        Combined = False
        TypeWarning = False
        NameDCI = String.Empty
        INDgcDrugInteractions.DataSource = Nothing
        INDgcLethalDoseParameters.DataSource = Nothing
        INDgcRisksDescription.DataSource = Nothing
        INDgcDCIRiskFactor.DataSource = Nothing
        INDgcLASADrug.DataSource = Nothing
        INDgcHighRiskDRugs.DataSource = Nothing
        INDSleDCIRiskFactor.Properties.DataSource = Nothing
        INDSleATCInteraction.EditValue = Nothing
        INDSleDCILASA.EditValue = Nothing
        INDSleHighRiskDrugs.EditValue = Nothing
        INDGleRiskLevel.EditValue = Nothing
        INDlciTypeWarning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDMeDescriptionInteraction.Text = String.Empty
        INDSleATCInteraction.Enabled = True
        INDGcATCs.DataSource = Nothing
        INDSleATC.EditValue = Nothing
        'Limpiar controles
        dci = Nothing
        ListDeleteDrugInteraction = New List(Of DrugInteraction)
        ListDeleteDrugActive = New List(Of DrugActive)
        ListDeleteLethalDoseLimits = New List(Of LethalDoseLimits)
        ListDeleteRisksDescription = New List(Of RisksDescription)
        ListDeleteDCIRiskFactor = New List(Of DCIRiskFactors)
        INDGcDrugActive.DataSource = Nothing
        INDSleATCInteraction.Properties.DataSource = Nothing
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyDCI.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With dci
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameDCI
            .Combined = Combined
            .TypeWarning = TypeWarning
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
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
                Using Model As New MDCI(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetDCI(INDbtnCode.Text.Trim)

                    If Not resultOperation.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = resultOperation.MessageResult?.FirstOrDefault().ToString()
                        AsyncLoader(False)
                        Exit Function
                    End If

                    INDlyDCI.BeginUpdate()
                    dci = resultOperation.ObjectEmbbeded
                    If dci IsNot Nothing AndAlso dci.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(dci.Id))
                            _isLoading = True
                            With dci
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                NameDCI = .Name
                                Combined = .Combined
                                TypeWarning = .TypeWarning
                                Status = .Status
                                INDGcDrugActive.DataSource = dci.DrugActive1.ToList()
                                INDgcHighRiskDRugs.DataSource = dci.HighRiskDrugs.ToList()
                                INDgcLASADrug.DataSource = dci.IHPARAMDCIs.ToList()
                                ValidationsShowTypeWarning()

                                INDgcLethalDoseParameters.DataSource = dci.LethalDoseLimits.ToList()
                                INDgcLethalDoseParameters.RefreshDataSource()

                                For Each item In dci.RisksDescription
                                    item.NameRiskType = ListRisksDescription.FirstOrDefault(Function(t) t.Item1 = item.TagRiskType)?.Item2
                                Next

                                INDgcRisksDescription.DataSource = dci.RisksDescription.ToList()
                                INDgcRisksDescription.RefreshDataSource()

                                INDgcDCIRiskFactor.DataSource = dci.DCIRiskFactors.ToList()
                                INDgcDCIRiskFactor.Refresh()

                                INDgcDrugInteractions.DataSource = dci.DrugInteraction1.ToList()
                                INDgcDrugInteractions.RefreshDataSource()
                                INDGcATCs.DataSource = dci.DCIATCEntity.ToList()
                            End With

                            _isLoading = False
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.dci.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = dci.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(dci.Id, Me.Tag.ToString(), Nothing, GetType(DCI).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewDCI()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If

                    INDlyDCI.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewDCI() As Task
        dci = New DCI() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.dci.Code) Then
            Try
                Using model As New MDCI(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not dci.Status
                    Dim result As ActionResult(Of DCI) = Await model.ChangeState(Me.dci.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.dci = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
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

    ''' <summary>
    ''' Cargar DCI Del popup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadPopupDCI()
        Using model As New MBusqueda
            Dim listATCXpo As XPInstantFeedbackSource
            listATCXpo = model.ConsultarEntidades(eDataSource.ListATCEntity)
            INDSleATCInteraction.Properties.DataSource = listATCXpo
        End Using
    End Sub

    ''' <summary>
    ''' Cargar DCI en medicamentos LASA Del popup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadPopupDCIDrugsLASA()
        Using model As New MBusqueda
            Dim listDCIDurgsLASAXpo As XPInstantFeedbackSource
            If dci IsNot Nothing AndAlso dci.Id > 0 Then
                listDCIDurgsLASAXpo = model.ConsultarEntidades(eDataSource.ListDCI, CStr(dci.Id))
            Else
                listDCIDurgsLASAXpo = model.ConsultarEntidades(eDataSource.ListDCI)
            End If
            INDSleDCILASA.Properties.DataSource = listDCIDurgsLASAXpo
        End Using
    End Sub

    ''' <summary>
    ''' Cargar Nivel de riesgos en medicamentos alto riesgo Del popup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadPopupDCIHighRiskDRugs()
        Using model As New MBusqueda
            Dim listDCIHighRiskDRugsXpo As XPInstantFeedbackSource
            If dci IsNot Nothing AndAlso dci.Id > 0 Then
                listDCIHighRiskDRugsXpo = model.ConsultarEntidades(eDataSource.ListInventoryRiskLevel)
            Else
                listDCIHighRiskDRugsXpo = model.ConsultarEntidades(eDataSource.ListInventoryRiskLevel)
            End If
            INDSleHighRiskDrugs.Properties.DataSource = listDCIHighRiskDRugsXpo
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Sub ClearPopup()
        CurrentDrugsLASA = Nothing
        CurrentDrugInteraction = Nothing
        CurrentLethalDoseLimits = Nothing
        CurrentRisksDescription = Nothing
        CurrentDrugHighRisk = Nothing
        INDSleATCInteraction.EditValue = Nothing
        INDSleDCILASA.EditValue = Nothing
        INDSleDCILASA.Properties.NullText = String.Empty
        INDSleHighRiskDrugs.EditValue = Nothing
        INDSleHighRiskDrugs.Properties.NullText = String.Empty
        INDSleHighRiskDrugs.Text = String.Empty
        INDMeDescriptionHighRiskDrugs.EditValue = Nothing
        INDGleRiskLevel.EditValue = Nothing
        INDSleTypesSimilarity.EditValue = Nothing
        INDSleTypesSimilarity.EditValue = Nothing
        INDMeDescriptionInteraction.Text = String.Empty
        INDSleATCInteraction.Enabled = True
        INDSleATCInteraction.Properties.NullText = String.Empty
    End Sub


    ''' <summary>
    ''' Función para validar cada uno de los campos del modal y avisar si falta diligenciar alguno.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateLethalParams()
        Dim validate As Boolean = True
        If INDTeAgeRangeBottom.EditValue Is Nothing OrElse INDTeAgeRangeTop.EditValue Is Nothing OrElse INDLeBottomAgeMeasureUnit.EditValue Is Nothing OrElse INDLeTopAgeMeasureUnit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar todos los campos de Rango Edad"
            validate = False
        ElseIf (INDTeWeightRangeBottom.EditValue IsNot Nothing OrElse INDTeWeightRangeTop.EditValue IsNot Nothing) AndAlso (INDLeBottomWeightMeasureUnit.EditValue Is Nothing OrElse INDLeTopWeightMeasureUnit.EditValue Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar todos los campos de Rango Peso"
            validate = False
        ElseIf INDTeMaxConcDose.EditValue Is Nothing OrElse INDLeMaxConcDoseUnit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar todos los campos de Concentración maxima por dosis"
            validate = False
        ElseIf INDTeMaxConc24.EditValue Is Nothing OrElse INDLeMaxConc24Unit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar todos los campos de Concentración maxima en 24 horas"
            validate = False
        ElseIf INDTeLethalConcDose.EditValue Is Nothing OrElse INDLeLethalConcDoseUnit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar todos los campos de Concentración letal por dosis"
            validate = False
        End If
        Return validate
    End Function

    Private Function ValidationsAddToAtcDrugActive()
        If INDsleATCActive.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un DCI"
            Return False
        End If

        If dci.DrugActive1.Any(Function(x) x.DCIId = INDsleATCActive.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DCIAdded", NAME_MODULE)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Esta función es dependiente a los DCIs mostrados, necesitamos consultar con el cambio a ATC, qué vendría luego.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadDrugInteractions() As Task
        'Cargamos el datasource con la lista de interacciones del DCI padre agregado
        Using model As New MDCI(Me.Tag)
            Dim drugInteraction = Await model.GetDrugInteractionByDCIParentId(INDsleATCActive.EditValue)
            If drugInteraction?.Any() Then
                For Each item In drugInteraction
                    If dci.DrugInteraction1.Any(Function(f) f.Id = item.Id) Then
                        Continue For
                    Else
                        dci.DrugInteraction1.Add(item)
                        INDgcDrugInteractions.DataSource = dci.DrugInteraction1.ToList()
                    End If
                Next
            Else
                Mensaje(EeventViewerImages.Informacion) = "No se encontraron interacciones de medicamentos con el DCI/ATC agregado"
            End If
        End Using
    End Function

    Private Function ValidateATCControls() As Boolean
        If INDSleATC.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un ATC"
            INDSleATC.Focus()
            Return False
        End If

        If dci.DCIATCEntity.Any(Function(m) m.IdATCEntity = INDSleATC.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "El ATC ya se encuentra en el listado"
            INDSleATC.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Valida que los campos a gregar a la rejilla de Drug alto riesgo no esten vacios y agregados
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidationsHighRisk() As Boolean
        If INDSleHighRiskDrugs.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un nivel de riesgo"
            Return False
        End If

        If INDMeDescriptionHighRiskDrugs.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El mensaje de advertencia no puede estar vacio"
            Return False
        End If

        If dci.HighRiskDrugs.Any(Function(x) x.InventoryRiskLevelId = INDSleHighRiskDrugs.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = $"El nivel de riesgo seleccionado ya está agregado"
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Valida que los campos a gregar a la rejilla de  Drug LASA no esten vacios o agregados
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidationsDrugsLASA() As Boolean
        If INDSleDCILASA.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un medicamento"
            Return False
        End If

        If INDSleTypesSimilarity.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El tipo no puede estar vacio"
            Return False
        End If

        If dci.IHPARAMDCIs.Any(Function(x) x.CODDCIMED = INDSleDCILASA.EditValue And x.ChangeTracker.State <> ObjectState.Deleted) Then
            Mensaje(EeventViewerImages.Advertencia) = $"El medicamento con codigo {INDSleDCILASA.EditValue} ya está agregado"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Concatena en un string los rangos de edades y pesos que se establecen desde parametros de dosis letales y maximas
    ''' </summary>
    ''' <returns></returns>
    Private Function ConcatParamsRange(bottom As Integer, bottomUnit As String, top As Integer, topUnit As String) As String
        If bottom > 1 Then
            If bottomUnit = "mes" Then
                bottomUnit = "meses"
            Else
                bottomUnit &= "s"
            End If
        End If

        If top > 1 Then
            If topUnit = "mes" Then
                topUnit = "meses"
            Else
                topUnit &= "s"
            End If
        End If

        If bottomUnit = topUnit Then
            Return $"{bottom} a {top} {topUnit}"
        Else
            Return $"{bottom} {bottomUnit} a {top} {topUnit}"
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequence = Nothing
        dci = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        CurrentDrugInteraction = Nothing
        CurrentLethalDoseLimits = Nothing
        CurrentRisksDescription = Nothing
        CurrentDrugHighRisk = Nothing
        CurrentDrugsLASA = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDCI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.LayoutControls.SetIsCustomizable(Me.INDlyDCI, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PDCI(Me)
        Presenter.GetSequense()
        InitializeTuples()
        IntializeMeasureUnitsDatasource()
        HideControls()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDrugInteractions, ListActions)
        IndigoGridView6.SetListAcction(INDgvLethalDoseParameters, ListActions)
        IndigoGridView7.SetListAcction(INDgvRisksDescription, ListActions)
        IndigoGridView8.SetListAcction(INDgvDCIRiskFactor, {eAcciones.Remove}.ToList())
        IndigoGridView4.SetListAcction(INDgvDrugsHighRisk, ListActions)
        IndigoGridView3.SetListAcction(INDgvDrugsLASA, ListActions)
        IndigoGridView5.SetListAcction(INDGvATC, {eAcciones.Remove}.ToList())
        INDgcDrugInteractions.RefreshDataSource()
        INDgcLethalDoseParameters.RefreshDataSource()
        INDgcDCIRiskFactor.RefreshDataSource()
        INDgcRisksDescription.RefreshDataSource()
        INDgcHighRiskDRugs.RefreshDataSource()
        INDgcLASADrug.RefreshDataSource()

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvLethalDoseParameters.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvRisksDescription.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDCIRiskFactor.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDrugInteractions.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDrugsHighRisk.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDrugsLASA.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Dim ListActionsATC As New List(Of eAcciones)
        ListActionsATC.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDGvDrugActive, ListActionsATC)
        IndigoGridControl2.RefreshGrid(INDGcDrugActive)

        Dim colD = INDGvDrugActive.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If colD IsNot Nothing Then
            colD.Width = 300
        End If

        Dim colATC = INDGvATC.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If colATC IsNot Nothing Then
            colATC.Width = 100
        End If

        LoadStatus()
        Deshacer()
    End Sub

    Private Sub InitializeTuples()
        Dim listYesNo = New List(Of Tuple(Of Boolean, String))
        listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        listYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        Dim _listTypeWarning = New List(Of Tuple(Of Boolean, String))
        _listTypeWarning.Add(New Tuple(Of Boolean, String)(False, "No restrictiva "))
        _listTypeWarning.Add(New Tuple(Of Boolean, String)(True, "Restrictiva "))
        INDSleCombined.Properties.DataSource = listYesNo
        INDSleTypeWarning.Properties.DataSource = _listTypeWarning


        ListAgeRange = New List(Of Tuple(Of Integer, String))
        ListAgeRange.Add(New Tuple(Of Integer, String)(1, "días"))
        ListAgeRange.Add(New Tuple(Of Integer, String)(2, "mes"))
        ListAgeRange.Add(New Tuple(Of Integer, String)(3, "año"))
        If INDLeBottomAgeMeasureUnit.Properties.DataSource Is Nothing Then
            INDLeBottomAgeMeasureUnit.Properties.DataSource = ListAgeRange
        End If
        If INDLeTopAgeMeasureUnit.Properties.DataSource Is Nothing Then
            INDLeTopAgeMeasureUnit.Properties.DataSource = ListAgeRange
        End If

        ListRisksDescription = New List(Of Tuple(Of Decimal, String))
        ListRisksDescription.Add(New Tuple(Of Decimal, String)(1, "8.1 Embarazo"))
        ListRisksDescription.Add(New Tuple(Of Decimal, String)(2, "8.2 Lactancia"))
        ListRisksDescription.Add(New Tuple(Of Decimal, String)(3, "8.3 Mujeres y hombres en edad reproductiva"))
        If INDLeTagRiskType.Properties.DataSource Is Nothing Then
            INDLeTagRiskType.Properties.DataSource = ListRisksDescription
        End If

        ListWeightRange = New List(Of Tuple(Of Integer, String))
        ListWeightRange.Add(New Tuple(Of Integer, String)(1, "miligramos"))
        ListWeightRange.Add(New Tuple(Of Integer, String)(2, "gramos"))
        ListWeightRange.Add(New Tuple(Of Integer, String)(3, "kilogramos"))
        If INDLeBottomWeightMeasureUnit.Properties.DataSource Is Nothing Then
            INDLeBottomWeightMeasureUnit.Properties.DataSource = ListWeightRange
        End If
        If INDLeTopWeightMeasureUnit.Properties.DataSource Is Nothing Then
            INDLeTopWeightMeasureUnit.Properties.DataSource = ListWeightRange
        End If

    End Sub

    Private Sub IntializeMeasureUnitsDatasource()
        If INDLeMaxConcDoseUnit.Properties.DataSource Is Nothing Then
            INDLeMaxConcDoseUnit.Properties.DataSource = Presenter.GetMeasureUnitList()
        End If

        If INDLeMaxConc24Unit.Properties.DataSource Is Nothing Then
            INDLeMaxConc24Unit.Properties.DataSource = Presenter.GetMeasureUnitList()
        End If

        If INDLeLethalConcDoseUnit.Properties.DataSource Is Nothing Then
            INDLeLethalConcDoseUnit.Properties.DataSource = Presenter.GetMeasureUnitList()
        End If

        If INDLeLethalConc24Unit.Properties.DataSource Is Nothing Then
            INDLeLethalConc24Unit.Properties.DataSource = Presenter.GetMeasureUnitList()
        End If
    End Sub

    Private Sub HideControls()
        INDgcDrugInteractions.SuspendLayout()
        INDgcDrugInteractions.BeginUpdate()
        If Me.Combined Then
            INDColnherited.ShowColumn()
            INDColnherited.GroupIndex = 0
            INDLcgDrugActive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'INDLcgDrugInteraction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDColnherited.HideColumn(-1)
            INDColnherited.GroupIndex = -1
            INDLcgDrugActive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgDrugInteraction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgLASADrugs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgHighRiskDrugs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        INDgcDrugInteractions.EndUpdate()
        INDgcDrugInteractions.ResumeLayout()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmDCI_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
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
                    Await Me.NewDCI()
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
    Private Sub FrmDCI_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Evento click para añadir un nuevo drugActive al DCI y la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtAdd_Click(sender As Object, e As EventArgs) Handles INDBtAdd.Click
        Try
            If Not ValidationsAddToAtcDrugActive() Then Return

            Dim ATCEntityId As Integer = INDsleATCActive.EditValue
            AsyncLoader(True)
            Await LoadDrugInteractions()

            Dim _drugActive As New DrugActive()
            With _drugActive
                .ATCEntityId = ATCEntityId
                .ATCCode = INDGvAtcActive.GetFocusedRowCellValue("Code")
                .ATCName = INDGvAtcActive.GetFocusedRowCellValue("Name")
            End With

            dci.DrugActive1.Add(_drugActive) 'agregamos en el listado
            INDGcDrugActive.DataSource = dci.DrugActive1.ToList()

            AsyncLoader(False)
            INDsleATCActive.EditValue = Nothing
            INDGcDrugActive.RefreshDataSource()
            INDsleATCActive.Focus()

        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Evento click que agregar los factores de riesgo a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddRiskFactor_Click(sender As Object, e As EventArgs) Handles INDSbAddRiskFactor.Click
        If INDSleDCIRiskFactor.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un factor de riesgo"
            Exit Sub
        End If
        Dim RiskFactor As RiskFactorXpo = TryCast(TryCast(GridView6.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, RiskFactorXpo)

        If dci.DCIRiskFactors.Any(Function(x) x.RiskFactorId = RiskFactor.Id) Then
            Mensaje(EeventViewerImages.Advertencia) = "El factor de riesgo ya está agregado"
            Exit Sub
        End If

        Dim dciRiskFactor = New DCIRiskFactors
        With dciRiskFactor
            .RiskFactorId = INDSleDCIRiskFactor.EditValue
            .CodeRiskFactor = RiskFactor.Code
            .NameRiskFactor = RiskFactor.Description
        End With

        dci.DCIRiskFactors.Add(dciRiskFactor)
        INDgcDCIRiskFactor.DataSource = dci.DCIRiskFactors.ToList()
        INDgcDCIRiskFactor.RefreshDataSource()
        INDPceDCIRiskFactor.ClosePopup()
    End Sub

    ''' <summary>
    ''' Evento click que agrega las descripciones de riesgo a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddRisksDescription_Click(sender As Object, e As EventArgs) Handles INDSbAddRisksDescription.Click
        If INDLeTagRiskType.EditValue Is Nothing Or INDMeTagIncludedDescription.Text = "" Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar todos los campos de la descripción de riesgos"
            Exit Sub
        End If

        Dim risksDescription As RisksDescription
        If CurrentRisksDescription IsNot Nothing Then
            risksDescription = CurrentRisksDescription
        Else
            risksDescription = New RisksDescription()
        End If

        With risksDescription
            .TagRiskType = INDLeTagRiskType.EditValue
            .TagIncludedDescription = INDMeTagIncludedDescription.Text
            .NameRiskType = INDLeTagRiskType.Text
        End With

        dci.RisksDescription.Add(risksDescription)
        INDgcRisksDescription.DataSource = dci.RisksDescription.ToList()
        INDgcRisksDescription.RefreshDataSource()
        INDPceRisksDescription.ClosePopup()
    End Sub

    ''' <summary>
    ''' Evento click para agregar parametros de dosis letales/maximas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddLethalParams_Click(sender As Object, e As EventArgs) Handles INDSbAddLethalParams.Click
        Dim isValid = ValidateLethalParams()
        If Not isValid Then
            Exit Sub
        End If

        Dim lethalDoseLimits As LethalDoseLimits

        'Si existe un CurrentLethalDoseLimits entonces se estaba editando el registro desde el evento ContexMenuActions y por lo tanto viene lleno con el contexto de la fila que se seleccionó.
        If CurrentLethalDoseLimits IsNot Nothing Then
            lethalDoseLimits = CurrentLethalDoseLimits
        Else
            lethalDoseLimits = New LethalDoseLimits()
        End If

        'Se llena la entidad
        With lethalDoseLimits
            .StartAge = INDTeAgeRangeBottom.EditValue
            .StartAgeUnit = INDLeBottomAgeMeasureUnit.EditValue
            .EndAge = INDTeAgeRangeTop.EditValue
            .EndAgeUnit = INDLeTopAgeMeasureUnit.EditValue
            .StartWeight = INDTeWeightRangeBottom.EditValue
            .StartWeightUnit = INDLeBottomWeightMeasureUnit.EditValue
            .EndWeight = INDTeWeightRangeTop.EditValue
            .EndWeightUnit = INDLeTopWeightMeasureUnit.EditValue
            .MaxDoseConcentration = INDTeMaxConcDose.EditValue
            .MaxDoseConcentrationUnitId = INDLeMaxConcDoseUnit.EditValue
            .Max24HourConcentration = INDTeMaxConc24.EditValue
            .Max24HourConcentrationUnitId = INDLeMaxConc24Unit.EditValue
            .Lethal24HourConcentration = INDTeLethalConc24.EditValue
            .Lethal24HourConcentrationUnitId = INDLeLethalConc24Unit.EditValue
            .LethalDoseConcentration = INDTeLethalConcDose.EditValue
            .LethalDoseConcentrationUnitId = INDLeLethalConcDoseUnit.EditValue

            'Propiedades para las columnas de la rejilla
            .AgeRange = ConcatParamsRange(CType(INDTeAgeRangeBottom.EditValue, Integer), CType(INDLeBottomAgeMeasureUnit.Text, String), CType(INDTeAgeRangeTop.EditValue, Integer), CType(INDLeTopAgeMeasureUnit.Text, String))
            .WeightRange = ConcatParamsRange(CType(INDTeWeightRangeBottom.EditValue, Integer), CType(INDLeBottomWeightMeasureUnit.Text, String), CType(INDTeWeightRangeTop.EditValue, Integer), CType(INDLeTopWeightMeasureUnit.Text, String))
            .LethalConcPer24 = $"{INDTeLethalConc24.EditValue} {INDLeLethalConc24Unit.Text}"
            .LethalConcPerDose = $"{INDTeLethalConcDose.EditValue} {INDLeLethalConcDoseUnit.Text}"
            .MaxConcPer24 = $"{INDTeMaxConc24.EditValue} {INDLeMaxConc24Unit.Text}"
            .MaxConcPerDose = $"{INDTeMaxConcDose.EditValue} {INDLeMaxConcDoseUnit.Text}"
        End With

        'Se guarda en la entidad DCI y se actualiza el datasource
        dci.LethalDoseLimits.Add(lethalDoseLimits)
        INDgcLethalDoseParameters.DataSource = dci.LethalDoseLimits.ToList()
        INDgcLethalDoseParameters.RefreshDataSource()
        INDPceLethalDoseParams.ClosePopup()
    End Sub

    ''' <summary>
    ''' Agrega un drug Interaction a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAddInteraction.Click
        '********************* Valida los Campos de la interaccion este diligenciados ****************'
        If INDSleATCInteraction.EditValue Is Nothing OrElse INDGleRiskLevel.EditValue Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DrugInteractionFieldsEmpty", NAME_MODULE)
            Exit Sub
        End If
        '********************* Agregar Drug Interaction a la rejilla****************'
        If dci.DrugInteraction1.Any(Function(x) x.Id <> Convert.ToInt32(CurrentDrugInteraction?.Id) AndAlso x.ATCEntityId = INDSleATCInteraction.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ATCAdded", NAME_MODULE)
            Exit Sub
        End If

        If CurrentDrugInteraction IsNot Nothing Then 'es por que se va a modificar
            CurrentDrugInteraction.RiskLevel = INDGleRiskLevel.EditValue
            CurrentDrugInteraction.Description = INDMeDescriptionInteraction.Text
        Else
            Dim _drugInteraction As New DrugInteraction() ' creamos entidad drug Interaction
            With _drugInteraction 'asignamos valores a al entidad a agregar en la coleccion
                .RiskLevel = INDGleRiskLevel.EditValue
                .Description = INDMeDescriptionInteraction.Text
                '.DCIId = INDSleATCInteraction.EditValue
                .ATCCode = INDSlevDCI.GetFocusedRowCellValue("Code")
                .ATCName = INDSlevDCI.GetFocusedRowCellValue("Name")
                .DCIParentCodeName = NameDCI
                .ATCEntityId = INDSleATCInteraction.EditValue
                .isInherited = False
                .isInheritedName = "Interacciones en combinación"
            End With
            dci.DrugInteraction1.Add(_drugInteraction) 'agregamos en el listado
            INDgcDrugInteractions.DataSource = dci.DrugInteraction1.ToList()
        End If

        INDgcDrugInteractions.RefreshDataSource()
        INDPceDrugInteractions.ClosePopup()
    End Sub

    Private Sub INDSbAddAtc_Click(sender As Object, e As EventArgs) Handles INDSbAddAtc.Click
        If Not ValidateATCControls() Then
            Return
        End If

        Dim dciAct As New DCIATCEntity With {
            .IdATCEntity = INDSleATC.EditValue,
            .ATCCode = INDGvATCS.GetFocusedRowCellValue("Code"),
            .ATCName = INDGvATCS.GetFocusedRowCellValue("Name")
        }

        dci.DCIATCEntity.Add(dciAct)
        INDGcATCs.DataSource = dci.DCIATCEntity.ToList()
        INDGcATCs.RefreshDataSource()
        INDSleATC.EditValue = Nothing
        INDSleATC.Focus()
    End Sub




    ''' <summary>
    ''' Valida si hay drug alto riesgo en la rejilla para  mostrar el campo advertencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidationsShowTypeWarning()
        If dci.HighRiskDrugs.Count > 0 Then
            'INDlciTypeWarning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'INDSleTypeWarning.EditValue = False
            INDlciTypeWarning.ShowLayout()
            If INDSleTypeWarning.EditValue Is Nothing Then
                INDSleTypeWarning.EditValue = False
            End If
        Else
            INDlciTypeWarning.HideLayout()
            INDSleTypeWarning.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Agrega un drug LASA a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAddDrugsLASA_Click(sender As Object, e As EventArgs) Handles INDSbAddDrugsLASA.Click
        If CurrentDrugsLASA IsNot Nothing Then
            CurrentDrugsLASA.TIPO = INDSleTypesSimilarity.EditValue
            CurrentDrugsLASA.TypeSimilirityName = INDSleTypesSimilarity.Text
        Else
            If Not ValidationsDrugsLASA() Then Return
            CurrentDrugsLASA = New IHPARAMDCI
            With CurrentDrugsLASA
                .TIPO = INDSleTypesSimilarity.EditValue
                .TypeSimilirityName = INDSleTypesSimilarity.Text
                .CODDCIMEDPADRE = dci.Id
                .CODDCIMED = INDSleDCILASA.EditValue
                .NameMED = INDSleDCILASA.Text
            End With
            dci.IHPARAMDCIs.Add(CurrentDrugsLASA)
            INDgcLASADrug.DataSource = dci.IHPARAMDCIs.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        End If
        INDgcLASADrug.RefreshDataSource()
        INDPceLASADrugs.ClosePopup()
    End Sub

    ''' <summary>
    ''' Agrega un drug alto riesgo a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAddHighRiskDrugs_Click(sender As Object, e As EventArgs) Handles INDSbAddHighRiskDrugs.Click
        If CurrentDrugHighRisk IsNot Nothing Then
            CurrentDrugHighRisk.Observation = INDMeDescriptionHighRiskDrugs.Text
        Else
            If Not ValidationsHighRisk() Then Return
            CurrentDrugHighRisk = New HighRiskDrugs
            With CurrentDrugHighRisk
                .InventoryRiskLevelId = INDSleHighRiskDrugs.EditValue
                .RiskLevelCodeName = INDSleHighRiskDrugs.Text
                .Observation = INDMeDescriptionHighRiskDrugs.Text
                .DCIId = dci.Id
            End With
            dci.HighRiskDrugs.Add(CurrentDrugHighRisk)
            INDgcHighRiskDRugs.DataSource = dci.HighRiskDrugs.ToList()
        End If
        INDgcHighRiskDRugs.RefreshDataSource()
        INDPceHighRiskDrugs.ClosePopup()
        ValidationsShowTypeWarning()
    End Sub

    ''' <summary>
    ''' Evento click del menu contextual de acciones editar o eliminar en la rejilla de descripcion de riesgos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView8_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView8.Click_ButtonAction, IndigoGridView8.ContexMenuActions
        Dim CurrentDCIRiskFactor = INDgvDCIRiskFactor.GetRow(INDgvDCIRiskFactor.FocusedRowHandle)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        If CurrentDCIRiskFactor IsNot Nothing Then
            RemoveDetailDCIRiskFactor(CurrentDCIRiskFactor)
        End If
    End Sub

    ''' <summary>
    ''' Evento click del menu contextual de acciones editar o eliminar en la rejilla de factores de riesgo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView7_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView7.Click_ButtonAction, IndigoGridView7.ContexMenuActions
        CurrentRisksDescription = INDgvRisksDescription.GetRow(INDgvRisksDescription.FocusedRowHandle)
        Select Case sender.tag
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If
                If CurrentRisksDescription IsNot Nothing Then
                    RemoveDetailRisksDescription(CurrentRisksDescription)
                End If
            Case "Edit"
                If CurrentRisksDescription IsNot Nothing Then
                    INDLeTagRiskType.EditValue = CurrentRisksDescription.TagRiskType
                    INDMeTagIncludedDescription.Text = CurrentRisksDescription.TagIncludedDescription
                    Me.INDPceRisksDescription.ShowPopup()
                End If
        End Select
    End Sub


    ''' <summary>
    ''' Evento click del menu contextual de acciones editar o eliminar en la rejilla de parametros de dosis letales / maximas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView6_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView6.Click_ButtonAction, IndigoGridView6.ContexMenuActions
        CurrentLethalDoseLimits = INDgvLethalDoseParameters.GetRow(INDgvLethalDoseParameters.FocusedRowHandle)
        Select Case sender.Tag
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                If CurrentLethalDoseLimits IsNot Nothing Then
                    RemoveDetailDoseParameters(CurrentLethalDoseLimits)
                End If

            Case "Edit"
                If CurrentLethalDoseLimits IsNot Nothing Then
                    INDTeAgeRangeBottom.EditValue = CurrentLethalDoseLimits.StartAge
                    INDTeAgeRangeBottom.Properties.NullText = CurrentLethalDoseLimits.StartAge
                    INDLeBottomAgeMeasureUnit.EditValue = CurrentLethalDoseLimits.StartAgeUnit

                    INDTeAgeRangeTop.EditValue = CurrentLethalDoseLimits.EndAge
                    INDTeAgeRangeTop.Properties.NullText = CurrentLethalDoseLimits.EndAge
                    INDLeTopAgeMeasureUnit.EditValue = CurrentLethalDoseLimits.EndAgeUnit

                    INDTeWeightRangeBottom.EditValue = CurrentLethalDoseLimits.StartWeight
                    INDTeWeightRangeBottom.Properties.NullText = CurrentLethalDoseLimits.StartWeight
                    INDLeBottomWeightMeasureUnit.EditValue = CurrentLethalDoseLimits.StartWeightUnit

                    INDTeWeightRangeTop.EditValue = CurrentLethalDoseLimits.EndWeight
                    INDTeWeightRangeTop.Properties.NullText = CurrentLethalDoseLimits.EndWeight
                    INDLeTopWeightMeasureUnit.EditValue = CurrentLethalDoseLimits.EndWeightUnit

                    INDTeMaxConcDose.EditValue = CurrentLethalDoseLimits.MaxDoseConcentration
                    INDTeMaxConcDose.Properties.NullText = CurrentLethalDoseLimits.MaxDoseConcentration
                    INDLeMaxConcDoseUnit.EditValue = CurrentLethalDoseLimits.MaxDoseConcentrationUnitId

                    INDTeMaxConc24.EditValue = CurrentLethalDoseLimits.Max24HourConcentration
                    INDTeMaxConc24.Properties.NullText = CurrentLethalDoseLimits.Max24HourConcentration
                    INDLeMaxConc24Unit.EditValue = CurrentLethalDoseLimits.Max24HourConcentrationUnitId

                    INDTeLethalConc24.EditValue = CurrentLethalDoseLimits.Lethal24HourConcentration
                    INDTeLethalConc24.Properties.NullText = CurrentLethalDoseLimits.Lethal24HourConcentration
                    INDLeLethalConc24Unit.EditValue = CurrentLethalDoseLimits.Lethal24HourConcentrationUnitId

                    INDTeLethalConcDose.EditValue = CurrentLethalDoseLimits.LethalDoseConcentration
                    INDTeLethalConcDose.Properties.NullText = CurrentLethalDoseLimits.LethalDoseConcentration
                    INDLeLethalConcDoseUnit.EditValue = CurrentLethalDoseLimits.LethalDoseConcentrationUnitId
                    Me.INDPceLethalDoseParams.ShowPopup()
                End If
        End Select
    End Sub


    ''' <summary>
    ''' Acciones de la rejilla medicamentos LASA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        CurrentDrugsLASA = INDgvDrugsLASA.GetRow(INDgvDrugsLASA.FocusedRowHandle)
        Select Case sender.Tag
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                If CurrentDrugsLASA IsNot Nothing Then
                    RemoveDetailDrugsLASA(CurrentDrugsLASA)
                End If
            Case "Edit"
                If CurrentDrugsLASA IsNot Nothing Then
                    INDSleDCILASA.Properties.NullText = CurrentDrugsLASA.NameMED
                    INDSleDCILASA.EditValue = CurrentDrugsLASA.CODDCIMED
                    INDSleTypesSimilarity.EditValue = CurrentDrugsLASA.TIPO
                    INDSleDCILASA.Enabled = False
                    Me.INDPceLASADrugs.ShowPopup()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Acciones de la rejilla medicamentos alto riesgo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        CurrentDrugHighRisk = INDgvDrugsHighRisk.GetRow(INDgvDrugsHighRisk.FocusedRowHandle)
        Select Case sender.Tag
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                If CurrentDrugHighRisk IsNot Nothing Then
                    RemoveDetailHighRisk(CurrentDrugHighRisk)
                    ValidationsShowTypeWarning()
                End If
            Case "Edit"
                If CurrentDrugHighRisk IsNot Nothing Then
                    INDSleHighRiskDrugs.Properties.NullText = CurrentDrugHighRisk.RiskLevelCodeName
                    INDSleHighRiskDrugs.EditValue = CurrentDrugHighRisk.InventoryRiskLevelId
                    INDMeDescriptionHighRiskDrugs.Text = CurrentDrugHighRisk.Observation
                    INDSleHighRiskDrugs.Enabled = False
                    Me.INDPceHighRiskDrugs.ShowPopup()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Acciones de la rejilla de medicamentos de interaccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        CurrentDrugInteraction = INDgvDrugInteractions.GetRow(INDgvDrugInteractions.FocusedRowHandle)
        If CurrentDrugInteraction.isInherited Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar una interacción heredada"
            Exit Sub
        End If

        Select Case sender.Tag
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                If CurrentDrugInteraction IsNot Nothing Then
                    RemoveDetail(CurrentDrugInteraction)
                End If
            Case "Edit"
                If CurrentDrugInteraction IsNot Nothing Then
                    INDSleATCInteraction.EditValue = CurrentDrugInteraction.ATCEntityId
                    INDSleATCInteraction.Properties.NullText = $"{CurrentDrugInteraction.ATCCode} - {CurrentDrugInteraction.ATCName}"
                    INDGleRiskLevel.EditValue = CurrentDrugInteraction.RiskLevel
                    INDMeDescriptionInteraction.Text = CurrentDrugInteraction.Description
                    INDSleATCInteraction.Enabled = False
                    Me.INDPceDrugInteractions.ShowPopup()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        CurrentDrugActive = INDGvDrugActive.GetRow(INDGvDrugActive.FocusedRowHandle)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        If CurrentDrugActive IsNot Nothing Then
            RemoveDetailDrugActive(CurrentDrugActive)
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles dentro del popup de descripcion de riesgos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceRisksDescription_Click(sender As Object, e As EventArgs) Handles INDPceRisksDescription.Click
        INDLeTagRiskType.EditValue = Nothing
        INDMeTagIncludedDescription.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Limpia el control dentro del popup de factores de riesgo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceDCIRiskFactor_Click(sender As Object, e As EventArgs) Handles INDPceDCIRiskFactor.Click
        INDSleDCIRiskFactor.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Limpia los controles del PopUp de parametros de dosis letales/maximas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceLethalDoseParams_Click(sender As Object, e As EventArgs) Handles INDPceLethalDoseParams.Click
        INDTeAgeRangeBottom.EditValue = Nothing
        INDTeAgeRangeBottom.Properties.NullText = String.Empty
        INDLeBottomAgeMeasureUnit.EditValue = Nothing

        INDTeAgeRangeTop.EditValue = Nothing
        INDTeAgeRangeTop.Properties.NullText = String.Empty
        INDLeTopAgeMeasureUnit.EditValue = Nothing

        INDTeWeightRangeBottom.EditValue = Nothing
        INDTeWeightRangeBottom.Properties.NullText = String.Empty
        INDLeBottomWeightMeasureUnit.EditValue = Nothing

        INDTeWeightRangeTop.EditValue = Nothing
        INDTeWeightRangeTop.Properties.NullText = String.Empty
        INDLeTopWeightMeasureUnit.EditValue = Nothing

        INDTeMaxConcDose.EditValue = Nothing
        INDTeMaxConcDose.Properties.NullText = String.Empty
        INDLeMaxConcDoseUnit.EditValue = Nothing

        INDTeMaxConc24.EditValue = Nothing
        INDTeMaxConc24.Properties.NullText = String.Empty
        INDLeMaxConc24Unit.EditValue = Nothing

        INDTeLethalConc24.EditValue = Nothing
        INDTeLethalConc24.Properties.NullText = String.Empty
        INDLeLethalConc24Unit.EditValue = Nothing

        INDTeLethalConcDose.EditValue = Nothing
        INDTeLethalConcDose.Properties.NullText = String.Empty
        INDLeLethalConcDoseUnit.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Limpia los campos del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceDrugInteractions_Click(sender As Object, e As EventArgs) Handles INDPceDrugInteractions.Click
        INDSleATCInteraction.EditValue = Nothing
        INDGleRiskLevel.EditValue = Nothing
        INDMeDescriptionInteraction.Text = String.Empty
        INDSleATCInteraction.Enabled = True
    End Sub

    ''' <summary>
    ''' Limpia los campos del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceLASADrugs_Click(sender As Object, e As EventArgs) Handles INDPceLASADrugs.Click
        INDSleDCILASA.EditValue = Nothing
        INDSleDCILASA.Properties.NullText = String.Empty
        INDSleTypesSimilarity.EditValue = Nothing
        INDSleDCILASA.Enabled = True
    End Sub

    ''' <summary>
    ''' Limpia los campos del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceHighRiskDRugs_Click(sender As Object, e As EventArgs) Handles INDPceHighRiskDrugs.Click
        INDSleHighRiskDrugs.EditValue = Nothing
        INDSleHighRiskDrugs.Text = String.Empty
        INDSleHighRiskDrugs.Properties.NullText = String.Empty
        INDMeDescriptionHighRiskDrugs.Text = String.Empty
        INDSleHighRiskDrugs.Enabled = True
    End Sub

    Private Sub RemoveDetail(CurrentDrugInteraction As DrugInteraction)
        CurrentDrugInteraction.MarkAsDeleted()
        If CurrentDrugInteraction.Id > 0 Then
            If ListDeleteDrugInteraction Is Nothing Then
                ListDeleteDrugInteraction = New List(Of DrugInteraction)
            End If
            ListDeleteDrugInteraction.Add(CurrentDrugInteraction)
        End If
        INDgcDrugInteractions.DataSource = dci.DrugInteraction1.ToList()
        INDgcDrugInteractions.RefreshDataSource()
    End Sub

    Private Sub RemoveDetailHighRisk(CurrentDrugHighRisk As HighRiskDrugs)
        If CurrentDrugHighRisk IsNot Nothing Then
            CurrentDrugHighRisk.MarkAsDeleted()
            INDgcHighRiskDRugs.DataSource = dci.HighRiskDrugs.ToList()
            INDgcHighRiskDRugs.RefreshDataSource()
            ClearPopup()
        End If
    End Sub

    Private Sub RemoveDetailDrugsLASA(CurrentDrugsLASA As IHPARAMDCI)
        If CurrentDrugsLASA IsNot Nothing Then
            CurrentDrugsLASA.MarkAsDeleted()
            INDgcLASADrug.DataSource = dci.IHPARAMDCIs.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
            INDgcLASADrug.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Elimina los detalle de la fila seleccionada en la rejilla  de descripcion de riesgos
    ''' </summary>
    ''' <param name="CurrentRisksDescription"></param>
    Private Sub RemoveDetailRisksDescription(CurrentRisksDescription As RisksDescription)
        CurrentRisksDescription.MarkAsDeleted()
        If CurrentRisksDescription.Id > 0 Then
            If ListDeleteRisksDescription Is Nothing Then
                ListDeleteRisksDescription = New List(Of RisksDescription)
            End If
            ListDeleteRisksDescription.Add(CurrentRisksDescription)
        End If
        INDgcRisksDescription.DataSource = dci.RisksDescription.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        INDgcRisksDescription.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Elimina los detalle de la fila seleccionada en la rejilla factores de riesgo
    ''' </summary>
    ''' <param name="CurrentDCIRiskFactor"></param>
    Private Sub RemoveDetailDCIRiskFactor(CurrentDCIRiskFactor As DCIRiskFactors)
        CurrentDCIRiskFactor.MarkAsDeleted()
        If CurrentDCIRiskFactor.Id > 0 Then
            If ListDeleteDCIRiskFactor Is Nothing Then
                ListDeleteDCIRiskFactor = New List(Of DCIRiskFactors)
            End If
            ListDeleteDCIRiskFactor.Add(CurrentDCIRiskFactor)
        End If
        INDgcDCIRiskFactor.DataSource = dci.DCIRiskFactors.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        INDgcDCIRiskFactor.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Elimina los detalles de la fila seleccionada para eliminar.
    ''' </summary>
    ''' <param name="CurrentDoseParamaters"></param>
    Private Sub RemoveDetailDoseParameters(CurrentDoseParamaters As LethalDoseLimits)
        CurrentDoseParamaters.MarkAsDeleted()
        If CurrentDoseParamaters.Id > 0 Then
            If ListDeleteLethalDoseLimits Is Nothing Then
                ListDeleteLethalDoseLimits = New List(Of LethalDoseLimits)
            End If
            ListDeleteLethalDoseLimits.Add(CurrentDoseParamaters)
        End If
        INDgcLethalDoseParameters.DataSource = dci.LethalDoseLimits.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        INDgcLethalDoseParameters.RefreshDataSource()
    End Sub

    Private Sub RemoveDetailDrugActive(Detail As DrugActive)
        CurrentDrugActive.MarkAsDeleted()
        INDGcDrugActive.DataSource = dci.DrugActive1.ToList()
        If dci.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DrugActive") Then
            ListDeleteDrugActive = dci.ChangeTracker.ObjectsRemovedFromCollectionProperties("DrugActive").Cast(Of DrugActive)().ToList()
            INDGcDrugActive.RefreshDataSource()
        End If
        Dim inheritableInteractionsToDelete = dci.DrugInteraction1.Where(Function(m) m.ParentDCIOriginId = CurrentDrugActive.DCIId)
        While inheritableInteractionsToDelete.Any()
            inheritableInteractionsToDelete(0).MarkAsDeleted()
        End While
        INDgcDrugInteractions.DataSource = dci.DrugInteraction1.ToList()
        INDgcDrugInteractions.RefreshDataSource()
    End Sub

    Private Sub INDSleCombined_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCombined.EditValueChanged
        HideControls()
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleDCIRiskFactor_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDCIRiskFactor.QueryPopUp
        If INDSleDCIRiskFactor.Properties.DataSource Is Nothing Then
            INDSleDCIRiskFactor.Properties.DataSource = Presenter.GetAllRiskFactor()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDCIParents_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATCActive.QueryPopUp
        If INDsleATCActive.Properties.DataSource Is Nothing Then
            Presenter.InitializeATCEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se abre el pop up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit1_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceDrugInteractions.QueryPopUp
        INDSleATCInteraction.Focus()
        INDGleRiskLevel.Properties.DataSource = ListPresentationProduct
    End Sub

    ''' <summary>
    ''' Evento cuando se abre el pop up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit2_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceLASADrugs.QueryPopUp
        INDSleDCILASA.Focus()
        INDSleTypesSimilarity.Properties.DataSource = ListTypesSimilarity
    End Sub

    Private Sub INDSleDCI_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleATCInteraction.QueryPopUp
        If INDSleATCInteraction.Properties.DataSource Is Nothing Then
            LoadPopupDCI()
        End If
    End Sub

    Private Sub INDSleDCILASA_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDCILASA.QueryPopUp
        LoadPopupDCIDrugsLASA()
    End Sub

    Private Sub INDSleHighRiskDrugs_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleHighRiskDrugs.QueryPopUp
        LoadPopupDCIHighRiskDRugs()
    End Sub

    Private Sub INDSleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleATC.QueryPopUp
        If INDSleATC.Properties.DataSource Is Nothing Then
            INDSleATC.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListATCEntity()
        End If
    End Sub

    Private Sub RemoveATC()
        Dim atc = INDGvATC.GetFocusedObject(Of DCIATCEntity)()

        If atc IsNot Nothing Then

            atc.MarkAsDeleted()
        End If
        INDGcATCs.DataSource = dci.DCIATCEntity.ToList()
        INDGcATCs.RefreshDataSource()
    End Sub

#End Region

#Region "Close"
    Private Sub INDPceHighRiskDrugs_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceHighRiskDrugs.Closed, INDPceLASADrugs.Closed, INDPceDrugInteractions.Closed, INDPceLethalDoseParams.Closed, INDPceRisksDescription.Closed, INDPceDCIRiskFactor.Closed
        ClearPopup()
    End Sub
#End Region

#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
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
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub IndigoGridView5_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView5.Click_ButtonAction, IndigoGridView5.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        RemoveATC()
    End Sub

    ''' <summary>
    ''' Devuelve en el cuadro de texto asociado a la rejilla la descripción que se asignó en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvRisksDescription_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDgvRisksDescription.ShowingEditor
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        If view.FocusedColumn.Name = "GcTagDescription" Then
            Dim selectedValue As String = view.GetFocusedRowCellValue(view.FocusedColumn).ToString()
            INDmeRiskDescription.Text = selectedValue
        End If
    End Sub


#End Region

End Class