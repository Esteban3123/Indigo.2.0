'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Cesar Augusto Collazos Perdomo
' Created          : 28-09-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.Payroll.MVP
Imports System.Linq
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab
Imports DevExpress.XtraLayout
Imports DevExpress.XtraBars.Docking2010.Views
Imports DevExpress.XtraBars.Docking2010.Views.Tabbed
Imports Newtonsoft.Json
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
#End Region


Public Class FrmHumanTalentParameterization
    Implements IHumanTalentParameterization

#Region "Fields"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As PayrollSequence

    ''' <summary>
    ''' Instancia de la clase Employee
    ''' </summary>
    Dim Employee As New FrmEmployee()

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Datasource que contiene los grupos y controles del formulario de talento humano
    ''' </summary>
    'Dim Datasource = Employee.GetControls()

    ''' <summary>
    ''' Variable que contiene el JSON de parametrización
    ''' </summary>
    Dim jsonDeserialize As HumanTalentParametrization

    ''' <summary>
    ''' Variable que contiene la entidad HumanTalentParameterization
    ''' </summary>
    Dim HumanTalentParameterization As HumanTalentParameterization
    ''' <summary>
    ''' Bandera para poder cerrar el frm sin el mensjae de advertencia cuando ya  se guardo
    ''' </summary>
    Dim SaveFlag As Boolean

    ''' <summary>
    ''' Guarda el Grid el cual tiene foco o está abierto
    ''' </summary>
    Private currentGrid As GridView

#End Region


#Region "Properties"

    ''' <summary>
    ''' Propiedad que muestra un mensaje en la interfaz de usuario
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IHumanTalentParameterization.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As PayrollSequence Implements IHumanTalentParameterization.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As PayrollSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequense.PayrollSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmHumanTalentParameterization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cerrar) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cortar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Pegar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Copiar) = True

        Me.LayoutControls.SetIsCustomizable(Me.INDlcHumanTalentParameterization, True)

        '****Inicializar variables*****'
        Me.indigo = SessionValues.Instance
        presenter = New PHumanTalentParameterization(Me)
        presenter.GetSequense()
        ChargeDatasource()
    End Sub


    ''' <summary>
    ''' Carga el datasource del GridNavigator y muestra la información del primer tab
    ''' </summary>
    Public Async Sub ChargeDatasource()
        Try
            Using Model As New MHumanTalentParameterization(Me.Tag)
                AsyncLoader(True)
                result = Await Model.GetHumanTalentParameterizationAsync()
                HumanTalentParameterization = result
                jsonDeserialize = JsonConvert.DeserializeObject(Of HumanTalentParametrization)(result.JSONdata)
                INDgcTabNavigator.DataSource = jsonDeserialize.Tabs
                INDgcTabNavigator.RefreshDataSource()
                INDtcgFieldCustomization.SelectedTabPage = INDtcgFieldCustomization.TabPages(0)
                currentGrid = GridView1
                INDgcIdentificationFields.DataSource = DirectCast(jsonDeserialize, HumanTalentParametrization).Items.Where(Function(item) item.LcgName = "Información de Identificación")
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Sub


    ''' <summary>
    ''' Evento que permite navegar entre los tabs del tabbedcontrolgroup desde el gridcontrol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcTabNavigator_Click(sender As Object, e As EventArgs) Handles INDgcTabNavigator.Click
        Dim view As GridView = TryCast(INDgcTabNavigator.MainView, GridView)
        Dim focusedRowHandle As Integer = view.FocusedRowHandle

        If focusedRowHandle >= 0 AndAlso focusedRowHandle < jsonDeserialize.Tabs.Count Then
            Dim selectedTab As HumanTalentParametrization.Tab = jsonDeserialize.Tabs(focusedRowHandle)
            Dim tabName As String = selectedTab.TabName

            ' Busca el TabbedControlGroup correspondiente y se selecciona como página activa
            For Each layoutGroup As LayoutControlGroup In INDtcgFieldCustomization.TabPages
                If layoutGroup.Text = tabName Then
                    INDtcgFieldCustomization.SelectedTabPage = layoutGroup
                    Exit For
                End If
            Next
        End If
    End Sub

    ''' <summary>
    '''  Evento que controla el cambio de tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgFieldCustomization_SelectedPageChanged(sender As Object, e As LayoutTabPageChangedEventArgs) Handles INDtcgFieldCustomization.SelectedPageChanged
        ' Obtiene el nombre del tab activo
        Dim tabName As String = INDtcgFieldCustomization.SelectedTabPage.Text

        ' Filtra los items correspondientes al tab seleccionado
        Dim tabItems = DirectCast(jsonDeserialize, HumanTalentParametrization).Items.Where(Function(item) item.LcgName = tabName)

        ' Asigna la lista de items filtrada como DataSource del GridControl
        If INDtcgFieldCustomization.SelectedTabPageIndex = 0 Then
            INDgcIdentificationFields.DataSource = tabItems
            currentGrid = GridView1

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 1 Then
            INDgcPersionalInfoFields.DataSource = tabItems
            currentGrid = GridView3

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 2 Then
            INDgcGeneralInfoFields.DataSource = tabItems
            currentGrid = GridView4

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 3 Then
            INDgcRetiredInfoFields.DataSource = tabItems
            currentGrid = GridView5

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 4 Then
            INDgcDeductionsFields.DataSource = tabItems
            currentGrid = GridView8

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 5 Then
            INDgcDisabiltyAndLanguajeInfoFields.DataSource = tabItems
            currentGrid = GridView9

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 6 Then
            INDgcStudiesAndProfessionsInfoFields.DataSource = tabItems
            currentGrid = GridView10

        ElseIf INDtcgFieldCustomization.SelectedTabPageIndex = 7 Then
            INDgcFamilyGroupInfoFields.DataSource = tabItems
            currentGrid = GridView11
        End If
    End Sub

    ''' <summary>
    ''' Implementa el método Buscar de la interfaz ICrudBase
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    ''' <summary>
    ''' Implementa el método Nuevo de la interfaz ICrudBase
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Implementa el método Guardar la interfaz ICrudBase
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        AssignValues()
        Try
            Using Model As New MHumanTalentParameterization(MyBase.Tag)
                AsyncLoader(True)
                If Await Model.SaveHumanTalentParameterization(HumanTalentParameterization) = True Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If HumanTalentParameterization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    ElseIf HumanTalentParameterization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    End If
                    AsyncLoader(False)
                    If HumanTalentParameterization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                        Mensaje(EeventViewerImages.Informacion) = "No se pudo actualizar el registro"

                        AsyncLoader(False)
                        Exit Sub
                    End If
                    SaveFlag = True
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    AsyncLoader(False)
                    Exit Sub
                End If
            End Using
            ActionsOnControls = False
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Método que asigna el valor a la entidad que se va a guardar
    ''' </summary>
    Private Sub AssignValues()
        Dim serializedJSON As String = JsonConvert.SerializeObject(jsonDeserialize)
        Try
            With HumanTalentParameterization
                .JSONdata = serializedJSON
            End With
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Sub

    ''' <summary>
    ''' Método que se ejecuta cuando se da click en deshacer en la barra de botones
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        If MessageIndigo.Show("Perderá todos los cambios, ¿desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Customizar) = True
            ChargeDatasource()
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Customizar) = True
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Método que implementa la interfaz ICrudBase
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
    End Sub

    ''' <summary>
    ''' Método que implementa la interfaz ICrudBase
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Método para establecer la lógica para los permisos de Guardar y Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el botón guardad de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el botón deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub


#Region "Eventos que controlan y valida los checkedit de visible y obligatorio en cada pestaña"
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de identificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory1_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory1.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de informacion personal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory2_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory2.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de informacion general
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory3_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory3.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de informacion pensionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory6_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory6.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de informacion deduciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory7_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory7.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de discapaciades e idiomas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory8_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory8.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de estudios y profesiones
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory9_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory9.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de grupo familiar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceObligatory10_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceObligatory10.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Obligatory = sender.EditValue

            If objFinal.Obligatory Then
                rowData.Visible = True
                objFinal.Visible = True
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de identificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCeFields_EditValueChanged(sender As Object, e As EventArgs) Handles INDCeFields.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False

                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de informacion personal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit1.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False
                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de obligatorio de informacion general
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit2.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False

                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de informacion pensionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit3_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit3.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)
        Dim selectedRowHandles As Int32() = currentGrid.GetSelectedRows()
        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False
                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de informacion deduciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit6__EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit6_.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)
        Dim p = jsonDeserialize
        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue
            p = jsonDeserialize
            If Not objFinal.Visible Then
                rowData.Obligatory = False
                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
        Dim o = objFinal
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de discapaciades e idiomas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit7_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit7.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False
                'currentGrid.SetRowCellValue(rowData, "Visible", True)
                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de estudios y profesiones
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit8_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit8.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False
                'currentGrid.SetRowCellValue(rowData, "Visible", True)
                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
    ''' <summary>
    ''' Método que controla y valida los checkedit de visible de grupo familiar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemCheckEdit9_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit9.EditValueChanged
        DataSelected = New List(Of HumanTalentParametrization)

        Dim rowData = currentGrid.GetFocusedRow()
        Dim objFinal = jsonDeserialize.Items.Find(Function(x) x.ItemName = DirectCast(rowData, HumanTalentParametrization.Item).ItemName)

        If objFinal IsNot Nothing Then
            objFinal.Visible = sender.EditValue

            If Not objFinal.Visible Then
                rowData.Obligatory = False
                'currentGrid.SetRowCellValue(rowData, "Visible", True)
                objFinal.Obligatory = False
            End If

            If objFinal.Mandatory Then
                currentGrid.OptionsSelection.EnableAppearanceFocusedRow = False
            End If
        End If
        currentGrid.RefreshData()
    End Sub
#End Region



    ''' <summary>
    ''' Método que genera el código de cada fila del Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles GridView9.CustomUnboundColumnData, GridView8.CustomUnboundColumnData, GridView5.CustomUnboundColumnData, GridView4.CustomUnboundColumnData, GridView3.CustomUnboundColumnData, GridView11.CustomUnboundColumnData, GridView10.CustomUnboundColumnData, GridView1.CustomUnboundColumnData
        If e.IsGetData AndAlso e.Column.FieldName = "Código" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    ''' <summary>
    ''' Método que cambia de apariencia los registros que no son modificables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GridView1_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
        If e.RowHandle >= 0 Then
            Dim mandatoryValue As Boolean = CBool(GridView1.GetRowCellValue(e.RowHandle, "Mandatory"))
            If mandatoryValue Then
                e.Appearance.BackColor = Color.LightGray
                e.Appearance.ForeColor = Color.Gray
                sender.GridControl.Cursor = Cursors.Default
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que cancela el evento de editar para los registros que tengan parametrizado el campo Mandatory como true
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GridView1_ShowingEditor(sender As Object, e As CancelEventArgs) Handles GridView1.ShowingEditor
        Dim isMandatory As Boolean = CBool(GridView1.GetRowCellValue(sender.FocusedRowHandle, "Mandatory"))
        If isMandatory Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que al ejecutarse marca todos los campos de la columna visible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAllFieldsVisible_Click(sender As Object, e As EventArgs) Handles INDbtnAllFieldsVisible.Click
        For i As Integer = 0 To currentGrid.RowCount - 1
            currentGrid.SetRowCellValue(i, "Visible", True)
        Next
    End Sub

    ''' <summary>
    ''' Evento que controla cuando se está cerrando el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmHumanTalentParameterization_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If SaveFlag = False Then
            If MessageIndigo.Show("Perderá todos los cambios, ¿desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequense = Nothing
        Employee = Nothing
        _idOperativeUnit = Nothing
        Datasource = Nothing
        jsonDeserialize = Nothing
        HumanTalentParameterization = Nothing
        currentGrid = Nothing
        SaveFlag = False
    End Sub


End Class