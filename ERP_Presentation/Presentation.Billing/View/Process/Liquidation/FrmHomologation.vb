Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class FrmHomologation

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se muestra el botón cancelar
    ''' </summary>
    ''' <value>Valor que indica si se muestra el botón cancelar</value>
    ''' <returns>Valor que indica si se muestra el botón cancelar</returns>
    Public Property CancelButtonVisible As Boolean
        Get
            Return Me.BtnCancel.Visible
        End Get
        Set(value As Boolean)
            Me.BtnCancel.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene las homologaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property Homologations As List(Of Domain.Entities.Homologation)
        Get
            Return Me.BarraBotones.FilterDataSource
        End Get
        Set(value As List(Of Domain.Entities.Homologation))
            Me.BarraBotones.FilterDataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private _listHomologationOneByOne As List(Of Domain.Entities.Homologation)

    Private Property CurrentHomologation As Domain.Entities.Homologation

    Public Property OnlySelectOne As Boolean = False

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="listErrorCups">Lista de cups sin homologar o errores encontrados al retarificar</param>
    Public Sub New(ByVal listErrorCups As List(Of Object))
        InitializeComponent()
        Me.GdcWithoutCounterpart.DataSource = listErrorCups
        Me.ControlBox = True
        Me.LycgErrors.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="homologations">Objeto que contiene la lista de servicios a homologar</param>
    Public Sub New(ByVal homologations As List(Of Domain.Entities.Homologation))
        InitializeComponent()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNavigationControl)
        Me.LycgHomologations.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LyciButtons.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.PrepareToolBar(homologations)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna un identificador único a cada grupo de homologaciones
    ''' </summary>
    Private Sub SetGuidHomologation()
        CType(Me.BarraBotones.FilterDataSource, List(Of Domain.Entities.Homologation)).ForEach(Sub(h)
                                                                                                   Dim guidHomologation As String = Guid.NewGuid().ToString().Replace("-", "")
                                                                                                   h.Homologations.ForEach(Sub(homo)
                                                                                                                               If h.Homologations.Where(Function(c) c.Activated).ToList().Count > 1 AndAlso homo.Activated Then
                                                                                                                                   homo.GuidHomologation = guidHomologation
                                                                                                                               Else
                                                                                                                                   homo.GuidHomologation = Nothing
                                                                                                                               End If
                                                                                                                           End Sub)
                                                                                               End Sub)
    End Sub

    ''' <summary>
    ''' Prepara la barra de botones para usar el control de registros
    ''' </summary>
    ''' <param name="listDatasource">Lista de objetos</param>
    Private Sub PrepareToolBar(ByVal listDatasource As List(Of Domain.Entities.Homologation))
        Dim list As New List(Of Domain.Entities.Homologation)()
        listDatasource.ForEach(Sub(o)
                                   If o.Homologations.Count > 1 Then
                                       list.Add(o)
                                   Else
                                       If _listHomologationOneByOne Is Nothing Then
                                           _listHomologationOneByOne = New List(Of Domain.Entities.Homologation)()
                                       End If
                                       _listHomologationOneByOne.Add(o)
                                   End If
                               End Sub)
        Me.BarraBotones.FilterDataSource = list
        Me.ToolBars.Visible = (list.Count > 1)
        Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "CUPS", .FieldName = "Service.CodeNameCups"}, New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "Service.CodeNameIpsService"}}.ToList()
        If list.Count = 1 Then
            Me.TxtServiceDate.EditValue = list(0).Service.ServiceDate
            Me.TxtSpeciality.EditValue = list(0).Service.CodeNameSpeciality
            Me.TxtFunctionalUnit.EditValue = list(0).Service.CodeNameFunctionalUnit '(list(0).Service.FunctionalUnit.Code & " - " & list(0).Service.FunctionalUnit.Name)
            Me.TxtProfessional.EditValue = list(0).Service.CodeNameHealthAdministrator '(list(0).Service.ThirdParty.Nit & " - " & list(0).Service.ThirdParty.Name)
            Me.GdcCounterpart.DataSource = list(0).Homologations
            CurrentHomologation = list(0)
        End If
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se realiza la homologación
    ''' </summary>
    Private Sub BtnHomologate_Click(sender As Object, e As EventArgs) Handles BtnHomologate.Click

        'If OnlySelectedOne AndAlso Homologations.FindAll(Function(x) x.Homologations. = True).Count > 1 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar solo una homologación"
        '    Exit Sub
        'End If

        If Me.ValidateHomologations() Then
            Me.SetGuidHomologation()
            If _listHomologationOneByOne IsNot Nothing AndAlso _listHomologationOneByOne.Count > 0 Then
                Me.Homologations = Me.Homologations.Union(_listHomologationOneByOne).ToList()
            End If
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            Me.ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una homologación por cada servicio"
        End If
    End Sub

    ''' <summary>
    ''' Aqui se carga el objeto seleccionado en el control de mavegacion a los controles
    ''' de visualización
    ''' </summary>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        CurrentHomologation = CType(Record, Domain.Entities.Homologation)
        Me.TxtServiceDate.EditValue = Record.Service.ServiceDate
        Me.TxtSpeciality.EditValue = Record.Service.CodeNameSpeciality
        Me.TxtFunctionalUnit.EditValue = Record.Service.CodeNameFunctionalUnit
        Me.TxtProfessional.EditValue = Record.Service.CodeNameHealthAdministrator
        Me.GdcCounterpart.DataSource = Record.Homologations
        Me.GdcCounterpart.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Deshabilita el autofilterRow de la rejilla de homologaciones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmHomologation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        GdvWithoutCounterpart.OptionsView.ShowAutoFilterRow = False
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Realiza la validación de homologaciones y retorna un valor que indica si
    ''' la validación ha sido exitosa
    ''' </summary>
    ''' <returns>Valor que indica si la validación ha sido exitosa</returns>
    Public Function ValidateHomologations() As Boolean
        For Each h In CType(Me.BarraBotones.FilterDataSource, List(Of Domain.Entities.Homologation))
            If Not h.Homologations.Any(Function(hm) hm.Activated) Then
                Return False
            End If
        Next
        Return True
    End Function

#End Region

    Private Sub RptChkSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RptChkSelect.EditValueChanging
        Dim obj As Domain.Entities.CupsHomologation = CType(GdvCounterpart.GetFocusedRow(), Domain.Entities.CupsHomologation)
        If (CurrentHomologation.Service.SettlementType <> 1 AndAlso CType(GdcCounterpart.DataSource, List(Of Domain.Entities.CupsHomologation)).Find(Function(x) x.Activated = True AndAlso obj.Id <> x.Id) IsNot Nothing) OrElse (CType(GdcCounterpart.DataSource, List(Of Domain.Entities.CupsHomologation)).Find(Function(x) x.Activated = True AndAlso obj.Id <> x.Id) IsNot Nothing AndAlso OnlySelectOne) Then
            e.Cancel = True
            If OnlySelectOne Then
                ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar solo una holologación"
            Else
                Me.ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar solo una homologación debido a que el item esta incluido dentro de otro servicio"
            End If
        End If
    End Sub

    Private Sub RptChkSelect_EditValueChanged(sender As Object, e As EventArgs) Handles RptChkSelect.EditValueChanged
        PanelControl1.Focus()
    End Sub
End Class