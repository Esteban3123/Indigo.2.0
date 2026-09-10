'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
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
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports System.IO
Imports DevExpress.Spreadsheet
Imports System.Collections.Concurrent

#End Region

Public Class FrmDefinitionRate
    Implements IDefinitionRate, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IDefinitionRate.Code
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
    ''' Obtiene o establece la descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IDefinitionRate.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDefinitionRate.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IDefinitionRate.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameDefinition As String Implements IDefinitionRate.NameDefinition
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Integer Implements IDefinitionRate.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Integer)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As ContractSequence Implements IDefinitionRate.Sequense
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

    Private _exportCleanDefinitionRateDetailStructure As Byte()
    ''' <summary>
    ''' propiedad que Obtiene o establece la estructura limpia de detalle de definicion de tarifa
    ''' </summary>
    ''' <returns></returns>
    Public Property ExportCleanDefinitionRateDetailStructure As Byte() Implements IDefinitionRate.ExportCleanDefinitionRateDetailStructure
        Get
            Return _exportCleanDefinitionRateDetailStructure
        End Get
        Set(value As Byte())
            _exportCleanDefinitionRateDetailStructure = value
        End Set
    End Property


    Private _exportDefinitionRateDetailConditionStructure As Byte()
    ''' <summary>
    ''' Obtiene o establece la estructura de exportar la estructura del detalle condicion de la definicion de tarifa
    ''' </summary>
    ''' <returns></returns>
    Public Property ExportDefinitionRateDetailConditionStructure As Byte() Implements IDefinitionRate.ExportDefinitionRateDetailConditionStructure
        Get
            Return _exportDefinitionRateDetailConditionStructure
        End Get
        Set(value As Byte())
            _exportDefinitionRateDetailConditionStructure = value
        End Set
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' Presentador de definición tarifas
    ''' </summary>
    Private Presenter As PDefinitionRate

    ''' <summary>
    ''' Entidad de definición de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private definitionRate As DefinitionRate

    ''' <summary>
    ''' Representa el detalle de la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private definitionRateDetail As DefinitionRateDetail

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As ContractSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Representa el listado del detalle de la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDefinitionRateDetail As List(Of DefinitionRateDetail)

    ''' <summary>
    ''' Representa el listado de eliminados del detalle de la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail)

    ''' <summary>
    ''' Representa al listado que valida cuando se edita un item
    ''' de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private ListValidate As List(Of DefinitionRateDetail)

    ''' <summary>
    ''' Listado de eliminados de condiciones de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition)

    ''' <summary>
    ''' constante que designara la lotificacion para el cargue masivo
    ''' </summary>
    Private Const DefaultBatchSize As Integer = 500

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

        If definitionRate IsNot Nothing AndAlso definitionRate.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MDefinitionRate(Me.Tag.ToString())
                        AsyncLoader(True)
                        definitionRate.Company = indigo.TransactionalContainer()
                        Dim result = Await Model.DeleteDefinitionRate(definitionRate)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
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
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        If ListDefinitionRateDetail Is Nothing OrElse ListDefinitionRateDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para la definición de tarifa."
            Exit Sub
        End If

        Try
            AssigningValues()
            Dim totalRegisters = definitionRate.DefinitionRateDetail.Count
            Dim logs = New List(Of Tuple(Of String, Integer))
            Dim listDrDetail = definitionRate.DefinitionRateDetail.ToList()
            Dim drSaved = definitionRate

            'Se declara el control de progreso
            Dim progressRecord = New CtrProgress
            If totalRegisters > DefaultBatchSize Then
                progressRecord.Dock = DockStyle.Fill
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progressRecord))
                progressRecord.SafeInvoke(Sub(x) x.PrintInfo("0", totalRegisters.ToString()))
            End If

            Using model As New MDefinitionRate(Me.Tag.ToString())
                AsyncLoader(True)

                'Creamos la cabecera
                Dim Result = New ActionResult(Of DefinitionRate)
                If Not drSaved.Id > 0 Then
                    Dim drFirst = GenerateDefinitionRate(Nothing, drSaved)
                    Result = Await model.SaveDefinitionRate(drFirst, Nothing, Nothing, indigo.TransactionalContainer, _idCurrentSequence)
                    Mensaje(EeventViewerImages.Informacion) = GetSavedMessage(Result, drFirst.ChangeTracker.State)
                    drSaved = Result.ObjectEmbbeded
                End If

                If totalRegisters > 0 Then
                    'Segmentamos la entidad para evitar errores de Entity Too Large
                    For batchStart = 0 To totalRegisters - 1 Step DefaultBatchSize
                        Dim batchEnd As Integer = Math.Min(batchStart + DefaultBatchSize, totalRegisters)
                        Dim dataBatch = listDrDetail.Skip(batchStart).Take(batchEnd - batchStart).ToList()

                        'Se genera el objeto a guardar y se cambia el estado a modificado
                        Dim drBatch = GenerateDefinitionRate(dataBatch, drSaved)
                        drBatch.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified

                        'Actualizamos la cabecera ya existente con los detalles
                        Result = Await model.SaveDefinitionRate(drBatch, ListDeleteDefinitionRateDetail, ListDeleteDefinitionRateDetailCondition, indigo.TransactionalContainer, _idCurrentSequence)

                        'Limpiamos las listas después de haber sido enviadas
                        ListDeleteDefinitionRateDetail = Nothing
                        ListDeleteDefinitionRateDetailCondition = Nothing

                        'Si existe el control se continua alimentando la secuencia
                        If AdditionalControlPanel.SafeInvoke(Function(x) x.Contains(progressRecord)) Then
                            progressRecord.SafeInvoke(Sub(x) x.PrintInfo(batchEnd.ToString(), totalRegisters.ToString()))
                        End If

                        If Result.StateResult Then
                            Mensaje(EeventViewerImages.Informacion) = GetSavedMessage(Result, drBatch.ChangeTracker.State)

                            drSaved = Result.ObjectEmbbeded
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            logs.Add(New Tuple(Of String, Integer)($"Se guardaron {drSaved.DefinitionRateDetail.Count} DefinitionRateDetail para: {drSaved.Code}", 1))
                        Else
                            logs.Add(New Tuple(Of String, Integer)($"No se guardaron {Result?.ObjectEmbbeded.DefinitionRateDetail.Count} DefinitionRateDetail para: {Result?.ObjectEmbbeded.Code}", 2))
                            INDbtnCode.Enabled = False
                            Mensaje(EeventViewerImages.Advertencia) = GetSavedMessage(Result, Nothing)
                        End If
                    Next
                ElseIf ListDeleteDefinitionRateDetail IsNot Nothing Or ListDeleteDefinitionRateDetailCondition IsNot Nothing Then
                    Result = Await model.SaveDefinitionRate(drSaved, ListDeleteDefinitionRateDetail, ListDeleteDefinitionRateDetailCondition, indigo.TransactionalContainer, _idCurrentSequence)
                    Mensaje(EeventViewerImages.Informacion) = GetSavedMessage(Result, drSaved.ChangeTracker.State)
                End If
                AsyncLoader(False)
            End Using

            'Informe de los detalles que fueron agregados a la cabecera
            If logs.Any() Then
                Using formulario As New FrmListErrors(logs)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If

            'Limpiamos el formulario
            AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
            Deshacer()
        Catch ex As Exception
            AsyncLoader(False)
            AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewDefinitionRate()
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que se encarga de obtener los procedimientos quirurgicos y asignarlos a la entidad enviada del detalle de definición
    ''' </summary>
    ''' <param name="item"></param>
    Private Async Function GetSurgicalProcedures(item As DefinitionRateDetail) As Task
        Dim list = Await Presenter.ListViewListDefinitionRateDetailSurgicalProceduresAsync(item.Id)
        If list IsNot Nothing AndAlso list.Any() Then
            For Each itemSurgical In list
                Dim drdsp As New DefinitionRateDetailSurgicalProcedures
                drdsp.SurgicalProcedureServiceId = itemSurgical.SurgicalProcedureServiceId
                drdsp.IPSServiceId = itemSurgical.IPSServiceId
                drdsp.QxCode = itemSurgical.IPSServiceCode
                drdsp.QxName = itemSurgical.IPSServiceName
                drdsp.QxClassName = itemSurgical.ServiceClassName
                drdsp.Id = itemSurgical.Id
                drdsp.DefinitionRateDetailId = itemSurgical.DefinitionRateDetailId
                drdsp.MarkAsUnchanged()
                item.DefinitionRateDetailSurgicalProcedures.Add(drdsp)
            Next
        End If
    End Function

    ''' <summary>
    ''' Metodo que abre el form de importar información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenImportInfo()
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportInfo()
            AddHandler Formulario.ImportInfoEvent, AddressOf ImportInfo
            Formulario.ToolBar.Dock = DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = FormStartPosition.CenterParent
            Formulario.Width = 920
            Formulario.Height = 600
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que importa la información
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Function ImportInfo(e As AddInfo) As Task
        If e IsNot Nothing AndAlso e.ListId IsNot Nothing AndAlso e.ListId.Any() Then
            Try
                AsyncLoader(True)
                For Each item In e.ListId
                    Await LoadListDetail(item, True)
                Next

                DataSourceGridDescending()
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que abre el form de agregar reglas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenAddRule(modeEdit As Boolean, Optional definitionRateDetail As DefinitionRateDetail = Nothing)
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmAddRule()
            AddHandler Formulario.AddInfoToGridFormPrincipal, AddressOf AddInfoToGridFormPrincipal
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = FormStartPosition.CenterParent
            Formulario.Width = Screen.PrimaryScreen.WorkingArea.Width * 0.8
            Formulario.Height = Screen.PrimaryScreen.WorkingArea.Height * 0.8
            Formulario.ModeEdit = modeEdit

            If definitionRateDetail IsNot Nothing Then 'Se envian estos dos parametros solo cuando se va a editar
                Formulario.DefinitionRateDetail = definitionRateDetail
                Formulario.ListValidate = ListValidate
            End If

            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega la regla que viene del form
    ''' que se lanza de forma modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddInfoToGridFormPrincipal(sender As Object, e As AddInfoToGridFormPrincipal)
        If e IsNot Nothing AndAlso e.ListDefinitionRateDetail IsNot Nothing AndAlso e.ListDefinitionRateDetail.Any() Then

            If ListDefinitionRateDetail Is Nothing Then
                ListDefinitionRateDetail = New List(Of DefinitionRateDetail)
            Else
                Dim result = ValidateListDetail(e.ListDefinitionRateDetail, e.ModeEdit)
                If result Is Nothing OrElse Not result.StateResult Then
                    e.ReturnValueOk = False
                    Exit Sub
                End If
                e.ListDefinitionRateDetail = result.ObjectEmbbeded
            End If

            If Not e.ModeEdit Then 'Cuando se va a agregar
                ListDefinitionRateDetail.AddRange(e.ListDefinitionRateDetail)
                Mensaje(EeventViewerImages.Informacion) = "Reglas agregadas correctamente."

            Else 'Cuando se va a modificar
                Dim index = ListDefinitionRateDetail.IndexOf(definitionRateDetail)
                ListDefinitionRateDetail.Remove(definitionRateDetail)
                ListDefinitionRateDetail.Insert(index, e.ListDefinitionRateDetail(0))
                Mensaje(EeventViewerImages.Informacion) = "Regla editada correctamente."

                If e.ListDeleteDefinitionRateDetailCondition IsNot Nothing AndAlso e.ListDeleteDefinitionRateDetailCondition.Any() Then
                    If ListDeleteDefinitionRateDetailCondition Is Nothing Then
                        ListDeleteDefinitionRateDetailCondition = New List(Of DefinitionRateDetailCondition)
                    End If

                    ListDeleteDefinitionRateDetailCondition.AddRange(e.ListDeleteDefinitionRateDetailCondition)
                End If
            End If

            DataSourceGridDescending()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el datasource de la rejilla de detalles y el listado
    ''' que va en esa se ordena de forma descendente con respecto al peso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataSourceGridDescending()
        'Se ordena el listado de forma descendente
        If ListDefinitionRateDetail IsNot Nothing AndAlso ListDefinitionRateDetail.Any() Then
            ListDefinitionRateDetail = ListDefinitionRateDetail.OrderByDescending(Function(item) item.Weight).ToList
        End If

        'Se le asigna el listado
        INDgcRules.DataSource = Nothing
        INDgcRules.DataSource = ListDefinitionRateDetail
    End Sub

    ''' <summary>
    ''' Metodo que valida el listado que viene desde el form modal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListDetail(ListDetail As List(Of DefinitionRateDetail), editMode As Boolean) As ActionResult(Of List(Of DefinitionRateDetail))
        Dim list As List(Of DefinitionRateDetail)
        If editMode Then
            list = ListValidate
        Else
            list = ListDefinitionRateDetail
        End If
        Dim listErrors As New StringBuilder
        Dim listRulesExcept As New List(Of DefinitionRateDetail)
        ListDetail.ForEach(Sub(item)
                               Dim cont As Integer = 0
                               Select Case item.RuleType
                                   Case 1 'IPSService
                                       If Not editMode Then
                                           If item.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso item.DefinitionRateDetailSurgicalProcedures.Any() Then
                                               Dim listDRDSP As New List(Of DefinitionRateDetailSurgicalProcedures)
                                               For Each itemTemp In (From x In list Where x.IPSServiceId = item.IPSServiceId AndAlso x.CUPSEntityId = item.CUPSEntityId AndAlso x.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso x.DefinitionRateDetailSurgicalProcedures.Any() Select x).ToList()
                                                   listDRDSP.AddRange(itemTemp.DefinitionRateDetailSurgicalProcedures)
                                               Next

                                               If listDRDSP IsNot Nothing AndAlso listDRDSP.Count > 0 Then
                                                   Dim listQxId = (From x In listDRDSP Select x.IPSServiceId).ToList()
                                                   cont = (From x In item.DefinitionRateDetailSurgicalProcedures Where listQxId.Contains(x.IPSServiceId)).Count()
                                               End If
                                           Else
                                               cont = (From l In list Where l.IPSServiceId = item.IPSServiceId AndAlso l.CUPSEntityId = item.CUPSEntityId AndAlso l.ConditionType = item.ConditionType AndAlso l.ConditionType2 = item.ConditionType2 Select l).Count
                                           End If
                                       End If
                                   Case 2 'CUPS
                                       cont = (From l In list Where l.CUPSEntityId = item.CUPSEntityId AndAlso l.ConditionType = item.ConditionType AndAlso l.ConditionType2 = item.ConditionType2 AndAlso l.RuleType = item.RuleType Select l).Count
                                   Case 3 'SubGroup
                                       cont = (From l In list Where l.CUPSSubgroupId = item.CUPSSubgroupId AndAlso l.ConditionType = item.ConditionType AndAlso l.ConditionType2 = item.ConditionType2 AndAlso l.RuleType = item.RuleType Select l).Count
                                   Case 4 'Group
                                       cont = (From l In list Where l.CUPSGroupId = item.CUPSGroupId AndAlso l.ConditionType = item.ConditionType AndAlso l.ConditionType2 = item.ConditionType2 AndAlso l.RuleType = item.RuleType Select l).Count
                                   Case 5 'General
                                       cont = (From l In list Where l.RuleType = item.RuleType AndAlso l.ConditionType = item.ConditionType AndAlso l.ConditionType2 = item.ConditionType2 AndAlso l.RuleType = item.RuleType Select l).Count
                               End Select

                               If cont > 0 Then
                                   'Separo el string del nombre de la regla
                                   Dim arrayName As String() = item.RuleTypeName.Split("-")
                                   'Capturo el nombre de la regla sin el identificador
                                   Dim nameRule As String = arrayName(1).Trim
                                   'Lo agrego en el stringBuilder
                                   listErrors.AppendLine("El " + nameRule + " " + item.RuleDescription + " ya existe en la lista con la condición " + item.ConditionName + ".")
                               Else
                                   listRulesExcept.Add(item)
                               End If

                           End Sub)


        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString()
        End If

        If listRulesExcept.Any() Then
            Return New ActionResult(Of List(Of DefinitionRateDetail)) With {.StateResult = True, .ObjectEmbbeded = listRulesExcept, .Message = listErrors.ToString()}
        End If

        Return New ActionResult(Of List(Of DefinitionRateDetail)) With {.StateResult = False, .Message = listErrors.ToString()}
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.definitionRate IsNot Nothing AndAlso Me.definitionRate.Id > 0 Then
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
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDefinitionRate.ActionsOnControls
        Set(value As Boolean)
            INDlyDefinitionRate.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDmemoDescription.Enabled = value
            INDbtnAddRules.Enabled = value
            INDgcRules.Enabled = value
            INDddBtnExport.Enabled = value
            INDddBtnImport.Enabled = value
            INDlyDefinitionRate.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

        If Not BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listItemsColumnEdit As New List(Of Tuple(Of String, Boolean))
        listItemsColumnEdit.Add(New Tuple(Of String, Boolean)("Activo", True))
        listItemsColumnEdit.Add(New Tuple(Of String, Boolean)("Inactivo", False))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15), .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEdit}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListDefinitionRate
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.definitionRate.Code, Me.definitionRate.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.definitionRate.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.definitionRate.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.definitionRate.Code, Me.definitionRate.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.definitionRate.Code)
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
    Private Async Sub CleanControls()

        INDlyDefinitionRate.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameDefinition = String.Empty
        Description = String.Empty
        ListDefinitionRateDetail = Nothing
        ListDeleteDefinitionRateDetail = Nothing
        ListDeleteDefinitionRateDetailCondition = Nothing
        INDgcRules.DataSource = Nothing
        definitionRateDetail = Nothing
        Me.ExportDefinitionRateDetailConditionStructure = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        INDlyDefinitionRate.EndUpdate()
        Me.HideConditionButton()

        Me.BarraBotones.StatusRecordVisible = False
        Await DeleteBlockedRecord()
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
        With definitionRate
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameDefinition
            .Description = Description

            If ListDefinitionRateDetail IsNot Nothing AndAlso ListDefinitionRateDetail.Any() Then
                Dim ListSave As List(Of DefinitionRateDetail) = (From l In ListDefinitionRateDetail Where l.ChangeTracker.State = ObjectState.Added OrElse l.ChangeTracker.State = ObjectState.Modified Select l).ToList
                ListSave.ForEach(Sub(item)
                                     .DefinitionRateDetail.Add(item)
                                 End Sub)
            End If

            If ListDeleteDefinitionRateDetailCondition IsNot Nothing AndAlso ListDeleteDefinitionRateDetailCondition.Any() Then
                For Each itemCondition As DefinitionRateDetailCondition In ListDeleteDefinitionRateDetailCondition
                    For Each itemDetail As DefinitionRateDetail In .DefinitionRateDetail
                        If itemCondition.DefinitionRateDetailId = itemDetail.Id Then
                            itemDetail.DefinitionRateDetailCondition.Add(itemCondition)
                        End If
                    Next
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Crea un nuevo objeto "DefinitionRate" limitando la cantidad de detalles
    ''' </summary>
    Private Function GenerateDefinitionRate(definitionBatch As List(Of DefinitionRateDetail), drSaved As DefinitionRate)
        'Limpiamos los detalles del objeto
        drSaved.DefinitionRateDetail.Clear()

        'Le agregamos los detalles nuevos si los trae en la lista
        If definitionBatch IsNot Nothing AndAlso definitionBatch.Count > 0 Then
            With drSaved
                For Each item In definitionBatch
                    .DefinitionRateDetail.Add(item)
                Next
            End With
        End If

        Return drSaved
    End Function

    ''' <summary>
    ''' Genera el mensaje de guardado dependiendo al estado del ChangeTracker o el StateResult
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="objState"></param>
    ''' <returns></returns>
    Function GetSavedMessage(result As ActionResult(Of DefinitionRate), objState As ObjectState) As String
        Dim msg As New StringBuilder()
        If result.StateResult Then
            If objState = Domain.Base.Entities.ObjectState.Added Then
                'Se descarta la secuencia numerica usada
                If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                    Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                End If
                If Me._sequence.Sequential Then
                    msg.AppendLine(String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code))
                Else
                    msg.AppendLine(ResourceManager.GetString("SaveMessage"))
                End If
            ElseIf objState = Domain.Base.Entities.ObjectState.Modified Then
                If String.IsNullOrEmpty(result.Message) Then
                    msg.AppendLine(ResourceManager.GetString("UpdateMessage"))
                Else
                    msg.AppendLine(ResourceManager.GetString("UpdateMessage") & " pero: ")
                    msg.AppendLine(result.Message)
                End If
            End If
        Else
            If result.MessageResult(0) = ErrorConcurrencia Then
                msg.AppendLine(ResourceManager.GetString("ErrorConcurrence"))
            ElseIf result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                msg.AppendLine(result.MessageResult(0))
            Else
                msg.AppendLine(ResourceManager.GetString("ErrorUnknown"))
            End If
        End If
        Return msg.ToString()
    End Function

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
    ''' Metodo que carga el listado del detalle de la definicion de tarifa
    ''' se le envia el id de la definicion y el 
    ''' optionImportInfo(True=Viene desde el form ImportInfo, False=Viene desde el LoadControls)
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadListDetail(DefinitionRateId As Integer, optionImportInfo As Boolean) As Task

        Dim ListXpCollection = Await Presenter.ListViewListDefinitionRateDetailAsync(DefinitionRateId)

        If ListXpCollection IsNot Nothing AndAlso ListXpCollection.Any() Then

            'Listado de errores que validan que la informacion que viene de ImportInfo no exista en el listado principal
            Dim ListErrors As New List(Of Tuple(Of String, Integer))

            For Each itemXpo In ListXpCollection
                Dim _definitionRateDetail As New DefinitionRateDetail
                _definitionRateDetail.StartTracking()
                With _definitionRateDetail

                    'Si es desde el loadControls se ingresan los Id de lo contrario no
                    If Not optionImportInfo Then
                        .Id = itemXpo.Id
                        .DefinitionRateId = itemXpo.DefinitionRateId
                    End If

                    .RuleType = itemXpo.RuleType
                    .RuleTypeName = itemXpo.RuleTypeName
                    .RuleDescription = itemXpo.RuleDescription
                    .IPSServiceId = itemXpo.IPSServiceId
                    .CUPSEntityId = itemXpo.CUPSEntityId
                    .CUPSSubgroupId = itemXpo.CUPSSubgroupId
                    .CUPSGroupId = itemXpo.CUPSGroupId

                    .ConditionType = itemXpo.ConditionType
                    .LogicalOperator = itemXpo.LogicalOperator
                    .ConditionType2 = itemXpo.ConditionType2

                    .ConditionName = itemXpo.ConditionName
                    .Weight = itemXpo.Weight
                    .AllowValueChange = itemXpo.AllowValueChange

                    .LiquidationType = itemXpo.LiquidationType
                    Select Case itemXpo.LiquidationType
                        Case 1 'Fija
                            .RateManualId = itemXpo.RateManualId
                            .RateManualDescription = itemXpo.RateManualDescription
                            .SalesValue = itemXpo.SalesValue
                            .SalesValueWithSurcharge = itemXpo.SalesValueWithSurcharge

                        Case 2 'Estandar
                            .ManualType = itemXpo.ManualType
                            .RateManualId = itemXpo.RateManualId
                            .RateVariation = itemXpo.RateVariation
                            .RateManualDescription = itemXpo.RateManualDescription

                        Case 3 'Vigencia
                            .RateManualValidityId = itemXpo.RateManualValidityId
                            .RateManualValidityDescription = itemXpo.RateManualValidityDescription
                            .RateVariation = itemXpo.RateVariation

                        Case Else
                            .RateManualId = itemXpo.RateManualId
                            .RateManualDescription = itemXpo.RateManualDescription
                    End Select

                    If Not optionImportInfo Then 'Si viene desde el loadControls
                        .MarkAsUnchanged()
                    Else 'Si viene desde ImportInfo

                        'Primero se valida que el item que se esta creando no exista en el listado de la rejilla (ListDefinitionRateDetail)
                        Dim contError As Integer = ValidateImportInfo(_definitionRateDetail)
                        If contError > 0 Then
                            ListErrors.Add(New Tuple(Of String, Integer)("La regla " + .RuleDescription + " ya existe en la lista con la condición: " + .ConditionName, 2))
                            Continue For
                        End If

                        'Se consultan los detalles para asignarselos al detalle de la definicion de tarifa
                        Dim result As ActionResult = Await GenerateDetailToDefinitionRateDetail(_definitionRateDetail, itemXpo.Id)
                        If result.StateResult = False Then
                            ListErrors.Add(New Tuple(Of String, Integer)(result.Message, 2))
                            Continue For
                        End If

                        'Si no se encontro el item en el listado se procede a marcarlo como agregado y posteriormente a agregarlo al listado
                        .MarkAsAdded()
                    End If
                End With

                If ListDefinitionRateDetail Is Nothing Then
                    ListDefinitionRateDetail = New List(Of DefinitionRateDetail)
                End If
                ListDefinitionRateDetail.Add(_definitionRateDetail)

            Next

            Me.HideConditionButton(Not ListXpCollection.Exists(Function(itemXpo) itemXpo.ConditionType <> 5))
            'Si viene de ImportInfo y el listErrors es mayor a cero es porque hubo errores y no se pueden insertar los registros que esten en este listado
            If optionImportInfo Then
                If ListErrors IsNot Nothing AndAlso ListErrors.Any() Then
                    INDgcRules.SafeInvoke(Sub()
                                              OpenImportInfoErrors(ListErrors)
                                          End Sub)
                End If
            End If

        End If
    End Function

    ''' <summary>
    ''' Metodo que consulta las condiciones que va a importar y posteriormente crea
    ''' los objetos que se van a agregar a la nueva entidad de definitionRateDetail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GenerateDetailToDefinitionRateDetail(_definitionRateDetail As DefinitionRateDetail, definitionRateDetailId As Integer) As Task(Of Domain.Base.Entities.ActionResult)
        Try
            Using model As New MDefinitionRate(Me.Tag)
                'Se consultan las condiciones
                Dim result As ActionResult(Of List(Of DefinitionRateDetailCondition)) = Await model.GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId)
                If Not result.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = result.Message}
                End If

                'Se procede a crear los objetos de las condiciones y posteriormente agregarselas a la entidad que se envio como parametro
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                    'Se recorre el listado que devuelve la consulta para poder crear los objetos de las condiciones que van en el detalle
                    For Each item As DefinitionRateDetailCondition In result.ObjectEmbbeded
                        Dim _definitionRateDetailCondition As New DefinitionRateDetailCondition
                        With _definitionRateDetailCondition
                            'Se coloca el operador logico
                            .LogicOperator = item.LogicOperator

                            'Se coloca el operador de la primera condicion
                            .Operator = item.Operator

                            'Primera Condicion
                            .FirstCondition = item.FirstCondition

                            'Se llena los campos de horario de la primera condicion si vienen llenos
                            If item.StartTime IsNot Nothing Then
                                .StartTime = item.StartTime
                                .EndTime = item.EndTime
                            End If

                            'Se llena los campos de especialidad de la primera condicion si viene lleno
                            If item.SpecialtyId IsNot Nothing Then
                                .SpecialtyId = item.SpecialtyId
                                .SpecialtyDescriptionFirst = item.SpecialtyDescriptionFirst
                            End If

                            'Se llena los campos de unidad funcional de la primera condicion si viene lleno
                            If item.FunctionalUnitId IsNot Nothing Then
                                .FunctionalUnitId = item.FunctionalUnitId
                                .FunctionalUnitDescriptionFirst = item.FunctionalUnitDescriptionFirst
                            End If

                            'Se llena los campos de tipo de unidad de la primera condicion si viene lleno
                            If item.UnitTypeId IsNot Nothing Then
                                .UnitTypeId = item.UnitTypeId
                            End If

                            'Se llena los campos de RIAS de la primera condicion si viene lleno
                            If item.RIASId IsNot Nothing Then
                                .RIASId = item.RIASId
                                .RIASDescriptionFirst = item.RIASDescriptionFirst
                            End If

                            'Se llena los campos de Descripción de la primera condicion si viene lleno
                            If item.ContractDescriptionId IsNot Nothing Then
                                .ContractDescriptionId = item.ContractDescriptionId
                                .DescriptionCodeNameFirst = item.DescriptionCodeNameFirst
                            End If

                            'Se coloca el operador de la segunda condicion
                            .Operator2 = item.Operator2

                            'Segunda condicion
                            .SecondCondition = item.SecondCondition

                            'Se llena los campos de horario de la segunda condicion si viene lleno
                            If item.StartTime2 IsNot Nothing Then
                                .StartTime2 = item.StartTime2
                                .EndTime2 = item.EndTime2
                            End If

                            'Se llena los campos de especialidad de la segunda condicion si viene lleno
                            If item.SpecialtyId2 IsNot Nothing Then
                                .SpecialtyId2 = item.SpecialtyId2
                                .SpecialtyDescriptionSecond = item.SpecialtyDescriptionSecond
                            End If

                            'Se llena los campos de la unidad funcional de la segunda condicion si viene lleno
                            If item.FunctionalUnitId2 IsNot Nothing Then
                                .FunctionalUnitId2 = item.FunctionalUnitId2
                                .FunctionalUnitDescriptionSecond = item.FunctionalUnitDescriptionSecond
                            End If

                            'Se llena los campos del tipo de unidad de la segunda condicion si viene lleno
                            If item.UnitTypeId2 IsNot Nothing Then
                                .UnitTypeId2 = item.UnitTypeId2
                            End If

                            'Se llena los campos de la rias de la segunda condicion si viene lleno
                            If item.RIASId2 IsNot Nothing Then
                                .RIASId2 = item.RIASId2
                                .RIASDescriptionSecond = item.RIASDescriptionSecond
                            End If

                            'Se llena los campos de descripción de la segunda condicion si viene lleno
                            If item.ContractDescriptionId2 IsNot Nothing Then
                                .ContractDescriptionId2 = item.ContractDescriptionId2
                                .DescriptionCodeNameSecond = item.DescriptionCodeNameSecond
                            End If

                            'Se coloca el nombre de la condicion completa
                            .ConditionName = item.ConditionName

                            'Se llena el tipo de liquidacion
                            .LiquidationType = item.LiquidationType
                            'Se coloca el nombre de la tarifa
                            .RateName = item.RateName

                            'Si el tipo de liquidacion es fija se llenan los respectivos campos
                            If item.SalesValue IsNot Nothing Then
                                .ManualType = item.ManualType
                                .SalesValue = item.SalesValue
                                .SalesValueWithSurcharge = item.SalesValueWithSurcharge
                            End If

                            'Si el tipo de liquidacion es estandard se llenan los respectivos campos
                            If item.RateManualValidity IsNot Nothing OrElse item.LiquidationType = 3 Then
                                .RateManualValidityId = item.RateManualValidityId
                                .RateManualValidityDescription = item.RateManualValidityDescription
                                .RateVariation = item.RateVariation
                            End If

                            If item.RateManualId IsNot Nothing Then
                                .RateManualId = item.RateManualId
                                .RateManualDescription = item.RateManualDescription
                                .RateVariation = item.RateVariation
                            End If
                        End With

                        'Se agrega la condicion creada al detalle de la definicion de tarifa
                        _definitionRateDetail.DefinitionRateDetailCondition.Add(_definitionRateDetailCondition)
                    Next
                End If
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Metodo que abre el form de errores de la importación
    ''' </summary>
    ''' <param name="ListErrors"></param>
    ''' <remarks></remarks>
    Private Sub OpenImportInfoErrors(ListErrors As List(Of Tuple(Of String, Integer)))
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmListErrors(ListErrors)
            Formulario.StartPosition = FormStartPosition.CenterParent
            Formulario.Width = 920
            Formulario.Height = 600
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Valida que la iformación que estoy importando no exista en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateImportInfo(definitionRateDetail As DefinitionRateDetail) As Integer
        Dim cont As Integer = 0
        If ListDefinitionRateDetail IsNot Nothing AndAlso ListDefinitionRateDetail.Any() Then
            Select Case definitionRateDetail.RuleType
                Case 1 'IPSService
                    cont = (From l In ListDefinitionRateDetail Where l.IPSServiceId = definitionRateDetail.IPSServiceId AndAlso l.ConditionType = definitionRateDetail.ConditionType AndAlso l.ConditionType2 = definitionRateDetail.ConditionType2 Select l).Count
                Case 2 'CUPS
                    cont = (From l In ListDefinitionRateDetail Where l.CUPSEntityId = definitionRateDetail.CUPSEntityId AndAlso l.ConditionType = definitionRateDetail.ConditionType AndAlso l.ConditionType2 = definitionRateDetail.ConditionType2 Select l).Count
                Case 3 'SubGroup
                    cont = (From l In ListDefinitionRateDetail Where l.CUPSSubgroupId = definitionRateDetail.CUPSSubgroupId AndAlso l.ConditionType = definitionRateDetail.ConditionType AndAlso l.ConditionType2 = definitionRateDetail.ConditionType2 Select l).Count
                Case 4 'Group
                    cont = (From l In ListDefinitionRateDetail Where l.CUPSGroupId = definitionRateDetail.CUPSGroupId AndAlso l.ConditionType = definitionRateDetail.ConditionType AndAlso l.ConditionType2 = definitionRateDetail.ConditionType2 Select l).Count
                Case 5 'General
                    cont = (From l In ListDefinitionRateDetail Where l.RuleType = definitionRateDetail.RuleType AndAlso l.ConditionType = definitionRateDetail.ConditionType AndAlso l.ConditionType2 = definitionRateDetail.ConditionType2 Select l).Count
            End Select
        End If
        Return cont
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
                Using Model As New MDefinitionRate(CStr(Me.Tag))
                    AsyncLoader(True)
                    definitionRate = (Await Model.GetDefinitionRate(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyDefinitionRate.BeginUpdate()
                    If definitionRate IsNot Nothing AndAlso definitionRate.Id > 0 Then

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(definitionRate.Id))
                            With definitionRate
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                NameDefinition = .Name
                                Description = .Description
                                Status = .Status


                                Await LoadListDetail(.Id, False)
                                DataSourceGridDescending()
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.definitionRate.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = definitionRate.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            AsyncLoader(False)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                            Me.BarraBotones.SetDocuments(definitionRate.Id, Me.Tag.ToString(), Nothing, GetType(DefinitionRate).Name)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewDefinitionRate()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyDefinitionRate.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Deshacer()
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewDefinitionRate() As Task
        definitionRate = New DefinitionRate With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                    Exit Function
                End If
            End If

            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
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
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
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
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                End If
            End If
        End If

        Status = True
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MDefinitionRate(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not definitionRate.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)

                If Result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.definitionRate = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If

            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Edita una regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub EditRule()
        definitionRateDetail = CType(viewRules.GetFocusedRow, DefinitionRateDetail)

        If definitionRateDetail.DefinitionRateDetailSurgicalProcedures Is Nothing OrElse Not definitionRateDetail.DefinitionRateDetailSurgicalProcedures.Any() Then
            Await GetSurgicalProcedures(definitionRateDetail)
        End If

        If ListDefinitionRateDetail IsNot Nothing AndAlso ListDefinitionRateDetail.Count > 0 Then
            Select Case definitionRateDetail.RuleType
                Case 1 'IPSSerive
                    If definitionRateDetail.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso definitionRateDetail.DefinitionRateDetailSurgicalProcedures.Any() Then
                        ListValidate = (From l In ListDefinitionRateDetail Where l.IPSServiceId = definitionRateDetail.IPSServiceId AndAlso l.CUPSEntityId = definitionRateDetail.CUPSEntityId Select l).ToList
                    Else
                        ListValidate = (From l In ListDefinitionRateDetail Where l.IPSServiceId = definitionRateDetail.IPSServiceId AndAlso l.CUPSEntityId = definitionRateDetail.CUPSEntityId AndAlso l.ConditionType <> definitionRateDetail.ConditionType Select l).ToList
                    End If
                Case 2 'CUPS
                    ListValidate = (From l In ListDefinitionRateDetail Where l.CUPSEntityId = definitionRateDetail.CUPSEntityId AndAlso l.ConditionType <> definitionRateDetail.ConditionType Select l).ToList
                Case 3 'SubGroup
                    ListValidate = (From l In ListDefinitionRateDetail Where l.CUPSSubgroupId = definitionRateDetail.CUPSSubgroupId AndAlso l.ConditionType <> definitionRateDetail.ConditionType Select l).ToList
                Case 4 'Group
                    ListValidate = (From l In ListDefinitionRateDetail Where l.CUPSGroupId = definitionRateDetail.CUPSGroupId AndAlso l.ConditionType <> definitionRateDetail.ConditionType Select l).ToList
                Case 5 'General
                    ListValidate = (From l In ListDefinitionRateDetail Where l.RuleType = definitionRateDetail.RuleType AndAlso l.ConditionType <> definitionRateDetail.ConditionType Select l).ToList
            End Select
        End If

        OpenAddRule(True, definitionRateDetail)
    End Sub

    ''' <summary>
    ''' Elimina una regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteRule()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim listHandlesSelected = viewRules.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            Dim ListRows As New List(Of Object)

            For i = 0 To listHandlesSelected.Count - 1
                Dim row As DefinitionRateDetail = viewRules.GetRow(listHandlesSelected(i))
                If row.Id > 0 Then
                    If ListDeleteDefinitionRateDetail Is Nothing Then
                        ListDeleteDefinitionRateDetail = New List(Of DefinitionRateDetail)
                    End If
                    ListDeleteDefinitionRateDetail.Add(row)
                End If
                ListRows.Add(row)
            Next

            ListRows.ForEach(Sub(item) ListDefinitionRateDetail.Remove(item))
            INDgcRules.DataSource = Nothing
            INDgcRules.DataSource = ListDefinitionRateDetail
        End If
    End Sub

    ''' <summary>
    ''' abre el archivo de Byte()
    ''' </summary>
    ''' <param name="fileBytes"></param>
    Private Sub OpenExcel(fileBytes As Byte())
        ' Ruta donde se guardará el archivo en el cliente
        Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
        ' Guardar el archivo localmente
        File.WriteAllBytes(fileName, fileBytes)
        ' Abrir el archivo
        Process.Start(fileName)
    End Sub

    ''' <summary>
    ''' Lee el archivo excel para obtener las filas a procesar
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <returns></returns>
    Private Function SetRow(rows As RowCollection, indexSend As Integer, indexEnd As Integer, fieldStructure As Integer) As Concurrent.ConcurrentBag(Of ImportFileRow)
        Dim objLock As New Object()
        Dim listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              If rows.Item(x).SpreadsheetRowToList(1)?(0) Is Nothing Then
                                                  Exit Sub
                                              End If
                                              listRows.Add(New ImportFileRow With {.IndexRow = x, .Row = rows.Item(x).SpreadsheetRowToList(fieldStructure)})
                                          End Sub)
        Return listRows
    End Function

    ''' <summary>
    ''' Lee el Copy Paste para obtener las filas a procesar
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <returns></returns>
    Private Function SetRow(rows As List(Of List(Of String)), fieldStructure As Integer) As Task(Of ActionResult(Of List(Of ImportFileRow)))

        Return Task.Factory.StartNew(Function() As ActionResult(Of List(Of ImportFileRow))
                                         Dim listToImport = New ConcurrentBag(Of ImportFileRow)
                                         If rows.Exists(Function(x) x.Count > fieldStructure) Then
                                             Return New ActionResult(Of List(Of ImportFileRow)) With {.StateResult = False, .Message = "La estructura del archivo detalle no es la recomendada"}
                                         End If

                                         Dim lastIndex = rows.Last

                                         Parallel.For(0, rows.Count, Sub(index)
                                                                         Dim item = rows(index)
                                                                         If item.Count < fieldStructure Then
                                                                             Dim diff = fieldStructure - item.Count
                                                                             For i = 0 To diff - 1
                                                                                 item.Add(String.Empty)
                                                                             Next
                                                                         End If
                                                                         Dim listOfObj = New List(Of Object)
                                                                         listOfObj.AddRange(item)
                                                                         listToImport.Add(New ImportFileRow With {.IndexRow = index + 1, .Row = listOfObj})
                                                                     End Sub)

                                         Return New ActionResult(Of List(Of ImportFileRow)) With {.StateResult = True, .ObjectEmbbeded = listToImport.ToList()}
                                     End Function)
    End Function

    ''' <summary>
    ''' funcion para procesar la informacion a Importar
    ''' </summary>
    ''' <param name="listRows"></param>
    ''' <returns></returns>
    Private Async Function ProcessDefinitionDetailImport(listRows As List(Of ImportFileRow)) As Task
        Try
            AsyncLoader(True)
            Dim listErrors = New List(Of String)
            Using model As New MDefinitionRate(MyTag)

                'se establece la cantidad de filas a procesas
                Dim totalRows = listRows.Count
                Dim progressRecord = New CtrProgress

                If totalRows > DefaultBatchSize Then
                    progressRecord.Dock = DockStyle.Fill
                    AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progressRecord))
                    progressRecord.SafeInvoke(Sub(x) x.PrintInfo("0", totalRows.ToString()))
                End If

                For batchStart = 0 To totalRows - 1 Step DefaultBatchSize
                    Dim batchEnd As Integer = Math.Min(batchStart + DefaultBatchSize, totalRows)
                    'se toman la cantidad de registros a procesar por lotes (para evitar timeout)
                    Dim dataBatch = listRows.Skip(batchStart).Take(batchEnd - batchStart).ToList()

                    Dim result = Await model.ImportDataExcelStructure(dataBatch)

                    'se verifica si el control esta añadido para seguir alimentando la secuencia
                    If AdditionalControlPanel.SafeInvoke(Function(x) x.Contains(progressRecord)) Then
                        progressRecord.SafeInvoke(Sub(x) x.PrintInfo(batchEnd.ToString(), totalRows.ToString()))
                    End If

                    If result Is Nothing OrElse Not result.StateResult Then
                        listErrors.Add($"Error en la respuesta del servicio: {result?.Message}")
                        Continue For
                    End If

                    'se añaden los mensajes de error a la lista de mensaje para mostrar el modal
                    If result.MessageResult?.Any() Then
                        listErrors.AddRange(result.MessageResult)
                    End If

                    'si se retornan items se añaden a la rejilla
                    If result.ObjectEmbbeded?.Any() Then
                        Dim args As New AddInfoToGridFormPrincipal With {.ListDefinitionRateDetail = result.ObjectEmbbeded, .ReturnValueOk = True, .ModeEdit = False, .ListDeleteDefinitionRateDetailCondition = Nothing}
                        Me.AddInfoToGridFormPrincipal(Nothing, args)
                    End If
                Next

                If listErrors.Any() Then
                    Using formulario As New FrmListErrors(listErrors)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"No se pudo leer el archivo por {Environment.NewLine} {ex.Message}"
        Finally
            AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' funcion que se encarga de importar el excel de structura condicion
    ''' </summary>
    ''' <param name="listRows"></param>
    ''' <returns></returns>
    Private Async Function ProcessDefinitionDetailConditionImport(listRows As List(Of ImportFileRow)) As Task
        Try
            AsyncLoader(True)
            Dim listErrors = New List(Of String)
            Using model As New MDefinitionRate(MyTag)

                If ListDefinitionRateDetail Is Nothing OrElse Not ListDefinitionRateDetail.Any() Then
                    Mensaje(EeventViewerImages.Advertencia) = $"Debes agregar y guardar reglas con condiciones antes de importar este archivo."
                    Return
                End If

                If ListDefinitionRateDetail.Exists(Function(x) x.Id = 0) Then
                    Mensaje(EeventViewerImages.Advertencia) = $"Hay reglas que aún no se han guardado. Por favor, guárdalas antes de importar las condiciones."
                    Return
                End If

                'se establece la cantidad de filas a procesas
                Dim totalRows = listRows.Count
                Dim progressRecord = New CtrProgress

                If totalRows > DefaultBatchSize Then
                    progressRecord.Dock = DockStyle.Fill
                    AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progressRecord))
                    progressRecord.SafeInvoke(Sub(x) x.PrintInfo("0", totalRows.ToString()))
                End If

                Dim listDefinitionRateDetailCondition As New List(Of DefinitionRateDetailCondition)

                For batchStart = 0 To totalRows - 1 Step DefaultBatchSize
                    Dim batchEnd As Integer = Math.Min(batchStart + DefaultBatchSize, totalRows)
                    'se toman la cantidad de registros a procesar por lotes (para evitar timeout)
                    Dim dataBatch = listRows.Skip(batchStart).Take(batchEnd - batchStart).ToList()

                    Dim result = Await model.ImportDataExcelStructureCondition(dataBatch)

                    'se verifica si el control esta añadido para seguir alimentando la secuencia
                    If AdditionalControlPanel.SafeInvoke(Function(x) x.Contains(progressRecord)) Then
                        progressRecord.SafeInvoke(Sub(x) x.PrintInfo(batchEnd.ToString(), totalRows.ToString()))
                    End If

                    If result Is Nothing OrElse Not result.StateResult Then
                        listErrors.Add($"Error en la respuesta del servicio: {result?.Message}")
                        Continue For
                    End If

                    'se añaden los mensajes de error a la lista de mensaje para mostrar el modal
                    If result.MessageResult?.Any() Then
                        listErrors.AddRange(result.MessageResult)
                    End If

                    'si se retornan items se añaden a la rejilla
                    If result.ObjectEmbbeded?.Any() Then
                        listDefinitionRateDetailCondition.AddRange(result.ObjectEmbbeded)
                    End If
                Next

                If listDefinitionRateDetailCondition.Any() Then
                    Dim addResult = Me.AddConditionToDefinitionRateDetail(listDefinitionRateDetailCondition)
                    If addResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = $"Error al añadir las condiciones a sus respectivas reglas"
                        Return
                    End If

                    If addResult.MessageResult.Any() Then
                        listErrors.AddRange(addResult.MessageResult)
                    End If

                    If Not addResult.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = $"No se logró añadir ninguna condición"
                    Else
                        Mensaje(EeventViewerImages.Informacion) = addResult.Message
                    End If
                End If
                If listErrors.Any() Then
                    Using formulario As New FrmListErrors(listErrors)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"No se pudo leer el archivo por {Environment.NewLine} {ex.Message}"
        Finally
            AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Muestra u oculta los botones de exportar e importar estructura Condicion
    ''' </summary>
    ''' <param name="hide"></param>
    Private Sub HideConditionButton(Optional hide As Boolean = True)
        If hide Then
            BarButtonConditionExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            BarButtonConditionImport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            BarButtonConditionExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            BarButtonConditionImport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' funcion que se encarga de añadir las condiciones a las reglas
    ''' </summary>
    Private Function AddConditionToDefinitionRateDetail(listToAdd As List(Of DefinitionRateDetailCondition)) As ActionResult
        Dim listMessage = New List(Of String)
        Dim group = listToAdd.GroupBy(Function(r) New With {Key r.DefinitionRateDetailId, Key r.ConditionName}).Where(Function(item) item.Count > 1)

        For Each item In group
            Dim message = $"La regla {item.Key.DefinitionRateDetailId} tiene la condición {item.Key.ConditionName} duplicada"
            listMessage.Add(message)
            listToAdd.Remove(item.FirstOrDefault)
        Next

        If Not listToAdd.Any() Then
            Return New ActionResult With {.StateResult = False, .MessageResult = listMessage}
        End If

        Dim listDefinitionDetailsId = listToAdd.Select(Function(x) x.DefinitionRateDetailId)
        Parallel.ForEach(ListDefinitionRateDetail.FindAll(Function(item) listDefinitionDetailsId.Contains(item.Id)),
                            Sub(detail)
                                Dim listCondtionsToAdd = listToAdd.FindAll(Function(x) x.DefinitionRateDetailId = detail.Id)
                                detail.MarkAsModified()
                                For Each condition In listCondtionsToAdd
                                    detail.DefinitionRateDetailCondition.Add(condition)
                                Next
                            End Sub)
        Dim count As Integer = listToAdd.Count
        Return New ActionResult With {.StateResult = True, .MessageResult = listMessage, .Message = $"se añadió: {count} condición(es)"}
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        definitionRate = Nothing
        definitionRateDetail = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListDefinitionRateDetail = Nothing
        ListDeleteDefinitionRateDetail = Nothing
        ListValidate = Nothing
        ListDeleteDefinitionRateDetailCondition = Nothing
        Me._exportDefinitionRateDetailConditionStructure = Nothing
        Me._exportCleanDefinitionRateDetailStructure = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDefinitionRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyDefinitionRate, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PDefinitionRate(Me)
        Presenter.GetSequense()

        IndigoGridControl1.RefreshGrid(INDgcRules)
        IndigoGridView1.SetListAcction(viewRules, {eAcciones.Edit, eAcciones.Remove}.ToList())

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRules.Columns
            If col.Name = "colActions" Then
                col.Width = 80
            End If
        Next
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmDefinitionRate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewDefinitionRate()
                Else
                    Await Me.LoadControls()
                End If
            End If

        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDefinitionRate_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRules_Click(sender As Object, e As EventArgs) Handles INDbtnAddRules.Click
        definitionRateDetail = Nothing
        OpenAddRule(False)
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Evento que se dispara al escoger sobre el menu alguna accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditRule()
            Case "Remove"
                DeleteRule()
        End Select
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcRules_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcRules.DataSourceChanged
        viewRules.ExpandAllGroups()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control para ver los items qx de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDrepPceSurgical_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceSurgical.QueryPopUp
        Dim itemCollection As DefinitionRateDetail = viewRules.GetFocusedRow()

        If itemCollection.DefinitionRateDetailSurgicalProcedures Is Nothing OrElse Not itemCollection.DefinitionRateDetailSurgicalProcedures.Any() Then
            Await GetSurgicalProcedures(itemCollection)
        End If

        INDgcSurgical.DataSource = Nothing
        INDgcSurgical.DataSource = itemCollection.DefinitionRateDetailSurgicalProcedures
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento para exportar la estructura detalle de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub BarButtonDetailExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonDetailExport.ItemClick
        Try
            AsyncLoader(True)

            If ExportCleanDefinitionRateDetailStructure Is Nothing Then
                Dim result As ActionResult(Of Byte()) = Nothing

                Using model As New MDefinitionRate(Me.Tag.ToString())
                    result = Await model.ExportCleanStructure()
                End Using

                If result Is Nothing OrElse Not result.StateResult Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = $"No se logro exportar la estructura intente nuevamente: {result?.Message}"
                    Exit Sub
                End If

                ExportCleanDefinitionRateDetailStructure = result.ObjectEmbbeded
            End If

            Me.OpenExcel(ExportCleanDefinitionRateDetailStructure)
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = $"Ocurrió un error al exportar la estructura: {Utils.GetInnerExceptionMessageToString(ex)}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Evento cuando se da click en el boton importar para importar el archivo excel con la estructura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub BarButtonDetailImport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonDetailImport.ItemClick
        Try
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.InitialDirectory = "c:\"
            openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
            openFileDialog1.FilterIndex = 2
            openFileDialog1.RestoreDirectory = True
            openFileDialog1.Title = "Importar Archivo"
            AsyncLoader(True)

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'obtengo la ruta del archivo
                Dim myStream = openFileDialog1.FileName

                If String.IsNullOrEmpty(myStream) Then
                    Exit Sub
                End If

                Dim spreadSheet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                spreadSheet.AllowDrop = False
                spreadSheet.LoadDocument(myStream)
                Dim workBook As IWorkbook = spreadSheet.Document

                Dim rows = workBook.Worksheets(0).Rows
                If rows.LastUsedIndex = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                    Exit Sub
                End If

                'se obtienen las filas del excel
                Dim listRows = SetRow(rows, 1, rows.LastUsedIndex + 1, 18).ToList().OrderBy(Function(x) x.IndexRow).ToList()
                Await ProcessDefinitionDetailImport(listRows)
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"No se pudo leer el archivo por {Environment.NewLine} {ex.Message}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Evento para exportar la estructura detalle de condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub BarButtonConditionExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonConditionExport.ItemClick
        Try
            AsyncLoader(True)

            If Me.definitionRate Is Nothing OrElse Me.definitionRate.Id = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = $"para exportar la estructura condición del detalle, debe primero guardar la definición de tarifas"
                Exit Sub
            End If

            If ExportDefinitionRateDetailConditionStructure Is Nothing Then
                Dim result As ActionResult(Of Byte()) = Nothing

                Using model As New MDefinitionRate(Me.Tag.ToString())
                    result = Await model.ExportConditionStructureByIdAsync(Me.definitionRate.Id)
                End Using

                If result Is Nothing OrElse Not result.StateResult Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = $"No se logro exportar la estructura intente nuevamente: {result?.Message}"
                    Exit Sub
                End If

                ExportDefinitionRateDetailConditionStructure = result.ObjectEmbbeded
            End If

            Me.OpenExcel(ExportDefinitionRateDetailConditionStructure)
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = $"Ocurrió un error al exportar la estructura: {Utils.GetInnerExceptionMessageToString(ex)}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' evento para importar la structura condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub BarButtonConditionImport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonConditionImport.ItemClick
        Try
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.InitialDirectory = "c:\"
            openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
            openFileDialog1.FilterIndex = 2
            openFileDialog1.RestoreDirectory = True
            openFileDialog1.Title = "Importar Archivo"
            AsyncLoader(True)

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'obtengo la ruta del archivo
                Dim myStream = openFileDialog1.FileName

                If String.IsNullOrEmpty(myStream) Then
                    Exit Sub
                End If

                Dim spreadSheet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                spreadSheet.AllowDrop = False
                spreadSheet.LoadDocument(myStream)
                Dim workBook As IWorkbook = spreadSheet.Document

                Dim rows = workBook.Worksheets(0).Rows
                If rows.LastUsedIndex = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                    Exit Sub
                End If

                'se obtienen las filas del excel
                Dim listRows = SetRow(rows, 1, rows.LastUsedIndex + 1, 30).ToList().OrderBy(Function(x) x.IndexRow).ToList()
                Await ProcessDefinitionDetailConditionImport(listRows)
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = $"No se pudo leer el archivo por {Environment.NewLine} {ex.Message}"
        Finally
            AsyncLoader(False)
        End Try
    End Sub
#End Region
#Region "PasteToGrid"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Try
            Dim result = Await SetRow(e.Rows, 18)
            If result Is Nothing OrElse Not result.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = $"No se pudo leer el archivo por: {result?.Message}"
                Exit Sub
            End If

            Await ProcessDefinitionDetailImport(result.ObjectEmbbeded)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub
#End Region
#End Region

#Region "Bar Button Events"

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
        Me.Nuevo()
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
    ''' Barra Botones: ImportarInformación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        OpenImportInfo()
    End Sub

#End Region

End Class