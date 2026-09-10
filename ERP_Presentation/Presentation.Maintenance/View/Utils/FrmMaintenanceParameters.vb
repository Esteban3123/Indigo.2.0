'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-03-2015
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Controls
Imports Presentation.Base
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Maintenance.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class FrmMaintenanceParameters

    Implements IMaintenanceParameters

    Dim FirstFlagEquipo As Boolean = False
    Dim FirstFlagMarca As Boolean = False
    Dim FirstFlagModelo As Boolean = False
    Dim FirstFlagSerial As Boolean = False

    Dim MaintenanceParameters As MaintenanceParameter


    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MMaintenanceParameter(MyBase.Tag)

    ''' <summary>
    '''Formula a utilizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DescriptionMaintenanceParameters As String Implements IMaintenanceParameters.DescriptionMaintenanceParameters
        Get
            Return INDtxtMuestra.Text
        End Get
        Set(value As String)
            INDtxtMuestra.Text = value
        End Set
    End Property



#Region "CheckedChanged"
    Private Sub INDChkEquipment_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkEquipment.CheckedChanged
        If INDChkEquipment.Checked = True Then

            If INDtxtMuestra.Text <> String.Empty And INDtxtMuestra.Text.IndexOf("Equipo") >= 0 Then
                Exit Sub
            End If

            If INDtxtMuestra.Text = String.Empty Then
                FirstFlagEquipo = True
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + INDChkEquipment.Text
            Else
                FirstFlagEquipo = False
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + " - " + INDChkEquipment.Text
            End If
        Else

            VerifiedFirst("Equipo")
            If INDtxtMuestra.EditValue IsNot Nothing Then
                If FirstFlagEquipo = True Then
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace("Equipo - ", "")
                Else
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace(" - Equipo", "")
                End If
            End If
        End If

        NotChecked()
    End Sub

    Private Sub INDChkTrademark_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTrademark.CheckedChanged
        If INDChkTrademark.Checked = True Then

            If INDtxtMuestra.Text <> String.Empty And INDtxtMuestra.Text.IndexOf("Marca") >= 0 Then
                Exit Sub
            End If

            If INDtxtMuestra.Text = String.Empty Then
                FirstFlagMarca = True
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + INDChkTrademark.Text
            Else
                FirstFlagMarca = False
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + " - " + INDChkTrademark.Text
            End If
        Else
            VerifiedFirst("Marca")
            If INDtxtMuestra.EditValue IsNot Nothing Then

                If FirstFlagMarca = True Then
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace("Marca - ", "")
                Else
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace(" - Marca", "")
                End If
            End If
        End If

        NotChecked()
    End Sub

    Private Sub INDChkModel_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkModel.CheckedChanged
        If INDChkModel.Checked = True Then

            If INDtxtMuestra.Text <> String.Empty And INDtxtMuestra.Text.IndexOf("Modelo") >= 0 Then
                Exit Sub
            End If

            If INDtxtMuestra.Text = String.Empty Then
                FirstFlagModelo = True
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + INDChkModel.Text
            Else
                FirstFlagModelo = False
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + " - " + INDChkModel.Text
            End If
        Else
            VerifiedFirst("Modelo")
            If INDtxtMuestra.EditValue IsNot Nothing Then

                If FirstFlagModelo = True Then
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace("Modelo - ", "")
                Else
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace(" - Modelo", "")
                End If
            End If
        End If

        NotChecked()
    End Sub

    Private Sub INDChkSerial_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkSerial.CheckedChanged
        If INDChkSerial.Checked = True Then

            If INDtxtMuestra.Text <> String.Empty And INDtxtMuestra.Text.IndexOf("Serial") >= 0 Then
                Exit Sub
            End If

            If INDtxtMuestra.Text = String.Empty Then
                FirstFlagSerial = True
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + INDChkSerial.Text
            Else
                FirstFlagSerial = False
                INDtxtMuestra.EditValue = INDtxtMuestra.EditValue + " - " + INDChkSerial.Text
            End If
        Else
            VerifiedFirst("Serial")
            If INDtxtMuestra.EditValue IsNot Nothing Then

                If FirstFlagSerial = True Then
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace("Serial - ", "")
                Else
                    INDtxtMuestra.EditValue = INDtxtMuestra.EditValue.ToString.Replace(" - Serial", "")
                End If
            End If
        End If

        NotChecked()
    End Sub

#End Region

    Private Sub NotChecked()
        If INDChkTrademark.Checked = False And INDChkEquipment.Checked = False And INDChkModel.Checked = False And INDChkSerial.Checked = False Then
            CleanAll()
        End If

    End Sub

    Private Sub VerifiedFirst(Check As String)

        If INDtxtMuestra.EditValue = Check Then
            CleanAll()
        End If

    End Sub

    Private Sub CleanAll()
        INDtxtMuestra.EditValue = Nothing
        INDChkTrademark.Checked = False
        INDChkEquipment.Checked = False
        INDChkModel.Checked = False
        INDChkSerial.Checked = False
        FirstFlagMarca = False
        FirstFlagEquipo = False
        FirstFlagModelo = False
        FirstFlagSerial = False
        MaintenanceParameters = Nothing
    End Sub

    Private Sub AssigningValues()
        With MaintenanceParameters
            .Description = INDtxtMuestra.EditValue
        End With
    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        MaintenanceParameters = Nothing
        record = Nothing
        Model = Nothing
    End Sub
    Private Sub INDBtnBorrar_Click(sender As Object, e As EventArgs) Handles INDBtnBorrar.Click
        CleanAll()
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanAll()
    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If MaintenanceParameters IsNot Nothing Then
            If MaintenanceParameters.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        AsyncLoader(True)
                        Using Model As New MMaintenanceParameter(MyBase.Tag)
                            If Await Model.DeleteMaintenanceParameterAsync(MaintenanceParameters) = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                                Deshacer()
                                Await Me.DeleteDocumentIndexed()
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                            AsyncLoader(False)
                        End Using
                    Catch ex As Exception
                        Throw ex
                        AsyncLoader(False)
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de Fondos.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        AsyncLoader(True)
        AssigningValues()
        Using Model As New MMaintenanceParameter(MyBase.Tag)
            If Await Model.SaveMaintenanceParameterAsync(MaintenanceParameters) = True Then
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                If MaintenanceParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                ElseIf MaintenanceParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or MaintenanceParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                AsyncLoader(False)
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                AsyncLoader(False)
            End If
        End Using

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        AsyncLoader(True)
        Using Model As New MMaintenanceParameter(MyBase.Tag)
            MaintenanceParameters = Await Model.ListMaintenanceParameterAsync()
        End Using
        If Not MaintenanceParameters Is Nothing Then
            If MaintenanceParameters.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, MaintenanceParameters.Id)
                With MaintenanceParameters
                    LogicaBotonActualizar(True)
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), MaintenanceParameters.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), MaintenanceParameters.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), MaintenanceParameters.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), MaintenanceParameters.ModificationDate)
                    DescriptionMaintenanceParameters = .Description

                    If DescriptionMaintenanceParameters.IndexOf("Equipo") >= 0 Then
                        If DescriptionMaintenanceParameters.IndexOf("Equipo", 0) = 0 Then
                            FirstFlagEquipo = True
                        End If
                        INDChkEquipment.Checked = True
                    End If

                    If DescriptionMaintenanceParameters.IndexOf("Marca") >= 0 Then
                        If DescriptionMaintenanceParameters.IndexOf("Marca", 0) = 0 Then
                            FirstFlagMarca = True
                        End If
                        INDChkTrademark.Checked = True
                    End If

                    If DescriptionMaintenanceParameters.IndexOf("Serial") >= 0 Then
                        If DescriptionMaintenanceParameters.IndexOf("Serial", 0) = 0 Then
                            FirstFlagSerial = True
                        End If
                        INDChkSerial.Checked = True
                    End If

                    If DescriptionMaintenanceParameters.IndexOf("Modelo") >= 0 Then
                        If DescriptionMaintenanceParameters.IndexOf("Modelo", 0) = 0 Then
                            FirstFlagModelo = True
                        End If
                        INDChkModel.Checked = True
                    End If

                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.MaintenanceParameters.Description)
                If result.Id = 0 Or result.CodUser = indigo.UserIndigo Then
                    Me.BarraBotones.SetDocuments(MaintenanceParameters.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = MaintenanceParameters.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            Else
                LogicaBotonActualizar(False)
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            MaintenanceParameters = New MaintenanceParameter
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        AsyncLoader(False)
    End Function

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.MaintenanceParameters.Description), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.MaintenanceParameters.Description & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.MaintenanceParameters.Description), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.MaintenanceParameters.Description)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.MaintenanceParameters.Description)
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
#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de fondos.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        'Me.Deshacer()
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
#End Region

#Region "Customizacion"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyMaintenanceParameters.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLyMaintenanceParameters.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region


    Private Async Sub FrmMaintenanceParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadControls()
    End Sub


End Class