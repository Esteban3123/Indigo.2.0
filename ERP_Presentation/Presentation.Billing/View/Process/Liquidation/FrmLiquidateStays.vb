#Region "Imports"

Imports Domain.Crystal.Entities
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Contract.MVP
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

Public Class FrmLiquidateStays

#Region "Consts"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Billing"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Número del ingreso
    ''' </summary>
    Private _admissionCode As String

    ''' <summary>
    ''' Id del grupo de atención, usado para almacenar
    ''' el id del grupo de atención inicial
    ''' </summary>
    Private _caregroupId As Integer

    ''' <summary>
    ''' Id de la unidad operativa
    ''' </summary>
    Private _idOperatingUnit As Integer
    ''' <summary>
    ''' Código del paciente
    ''' </summary>
    Private _patientCode As String

    ''' <summary>
    ''' Mensaje de auditoria
    ''' </summary>
    Private _audit As AuditMessage

    ''' <summary>
    ''' Bandera para saber si el frontal ya fue cargado
    ''' </summary>
    Private _isLoaded As Boolean

#End Region

#Region "Properties"

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

    ''' <summary>
    ''' Propidad para establecer si puede hacer la liquidacion manual
    ''' </summary>
    ''' <remarks></remarks>
    Dim _allowManualLiquidation As Boolean
    Public WriteOnly Property AllowManualLiquidation As Boolean
        Set(value As Boolean)
            If value Then
                INDBtnManualLiquidation.Visible = True
                INDBtnManualLiquidation.Location = New Drawing.Point(110, 6)
                BtnCancel.Location = New Drawing.Point(214, 6)
            Else
                INDBtnManualLiquidation.Visible = False
                BtnCancel.Location = New Drawing.Point(110, 6)
            End If

        End Set
    End Property

    Private _allowLiquidation As Boolean
    ''' <summary>
    ''' Asigna un valor que indica si se puede realizar la liquidación automática
    ''' </summary>
    ''' <value>Valor que indica si se puede realizar la liquidación</value>
    Public WriteOnly Property AllowLiquidation As Boolean
        Set(value As Boolean)
            _allowLiquidation = value
            'BtnLiquidate.Enabled = value
            'If value Then
            '    'BtnLiquidate.Visible = True
            '    'INDBtnManualLiquidation.Location = New Drawing.Point(110, 6)
            '    'BtnCancel.Location = New Drawing.Point(214, 6)
            'Else
            '    'BtnLiquidate.Visible = False
            '    'INDBtnManualLiquidation.Location = New Drawing.Point(6, 6)
            '    'BtnCancel.Location = New Drawing.Point(110, 6)
            'End If
        End Set
    End Property

    Public ReadOnly Property GetLiquidationType As List(Of Tuple(Of Byte, String))
        Get
            Dim lt As New List(Of Tuple(Of Byte, String))
            lt.Add(New Tuple(Of Byte, String)(1, "Cama Mayor Valor"))
            lt.Add(New Tuple(Of Byte, String)(2, "Cama Ultimo Ingreso"))
            'lt.Add(New Tuple(Of Byte, String)(3, "Defecto del Grupo Atención"))
            Return lt
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva estancia de la clase
    ''' </summary>
    Public Sub New()
        Me.New(Nothing, 0)
    End Sub

    Property AdmissionObject As Object

    ''' <summary>
    ''' Inicializa una nueva estancia de la clase
    ''' </summary>
    ''' <param name="idOperatingUnit">Id de la unidad operativa</param>
    Public Sub New(_auxAdmissionToReload As Object, ByVal idOperatingUnit As Integer)
        InitializeComponent()
        Me._isLoaded = False
        AdmissionObject = _auxAdmissionToReload
        Me._idOperatingUnit = idOperatingUnit
        Me._admissionCode = _auxAdmissionToReload.AdmissionCode
        Me._caregroupId = _auxAdmissionToReload.AdmissionCaregroupId
        Me._patientCode = _auxAdmissionToReload.PatientCode.ToString().Trim()
        Me.SleCareGroup.Properties.NullText = _auxAdmissionToReload.AdmissionCaregroupCodeName
        GleLiquidationType.Properties.DataSource = GetLiquidationType
        GleLiquidationType.EditValue = CByte(_auxAdmissionToReload.AdmissionTypeLiquidationEmergencyStays)
        Me._audit = New AuditMessage() With {.CodeUser = Infrastructure.CrossCutting.Base.SessionValues.Instance.UserIndigo, .IdUser = Infrastructure.CrossCutting.Base.SessionValues.Instance.UserIndigoId, .Functional = "756"}
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Prepara el formulario para una operación asincrona
    ''' </summary>
    ''' <param name="loading">Valor que indica si esta cargando</param>
    Public Sub AsyncLoader(Optional ByVal loading As Boolean = True)
        If loading Then
            Me.BtnLiquidate.Enabled = False
            Me.INDBtnManualLiquidation.Enabled = False
            Me.LycgBody.Enabled = False
            Me.LyciAsycLoader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            If _allowLiquidation Then Me.BtnLiquidate.Enabled = True
            Me.INDBtnManualLiquidation.Enabled = True
            Me.LyciAsycLoader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.LycgBody.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Carga las estancias si existen
    ''' </summary>
    ''' <param name="admissionCode">Numero del ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    Public Async Sub LoadStays(ByVal admissionCode As String, ByVal caregroupId As Integer, ByVal medicalOrderDate As DateTime?, ByVal endDate As DateTime?)
        Using model As New MLiquidation()
            Me.AsyncLoader()
            'Cargamos las estancias sin liquidar
            Dim liquidationType As eLiquidateStayOption = eLiquidateStayOption.DefectoManualGrupoAtencion
            If CByte(GleLiquidationType.EditValue) = 1 Then
                liquidationType = eLiquidateStayOption.MayorValor
            Else
                liquidationType = eLiquidateStayOption.CamaUltimoIngreso
            End If

            Dim res = Await model.ListDontLiquidatedStaysByAdmissionCode(admissionCode, caregroupId, liquidationType, medicalOrderDate, endDate, False)
            If res.StateResult Then
                If res.ObjectEmbbeded IsNot Nothing Then
                    DteLiqDate.Properties.MinValue = res.ObjectEmbbeded(0).FECINIEST
                End If
                'Si es el ultimo registro
                'Mostramos el campo de corte
                Dim obj = res.ObjectEmbbeded.LastOrDefault()
                If obj.FECFINEST < obj.FECINIEST AndAlso Not Me._isLoaded Then
                    Me._isLoaded = True
                    Me.DteLiqDate.EditValue = DateTime.Now.AddDays(-1)
                    Me.DteLiqDate.Properties.MaxValue = DateTime.Now.AddDays(-1)
                    Me.DteLiqDate.Invalidate()
                Else
                    Me.GdcStays.DataSource = res.ObjectEmbbeded
                    Me.GdcStays.RefreshDataSource()
                    If res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Count > 0 AndAlso res.ObjectEmbbeded(0).ADINGRESO.GENULTLIQUI IsNot Nothing Then
                        INDliLastLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDdeLastLiquidate.EditValue = res.ObjectEmbbeded(0).ADINGRESO.GENULTLIQUI
                    End If
                    If Not String.IsNullOrEmpty(res.Message.Trim()) Then
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                    Me.AsyncLoader(False)
                End If
            Else
                Me.AsyncLoader(False)
                Select Case res.Message
                    Case "{ERR0}" 'Error desconocido
                        Me.ShowMessage(EeventViewerImages.MensajeError) = String.Join(vbCrLf, res.MessageResult.ToArray())
                    Case "{ERR1}" 'No hay estancias por liquidar
                        Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontStays", MODULE_NAME)
                    Case "{ERR2}" 'Faltan tarifas por configurar en algunas camas
                        Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontRateOnBeds", MODULE_NAME) & vbCrLf & String.Join(vbCrLf, res.MessageResult.ToArray())
                    Case "{ERR3}" 'No existen parámetros para el centro de atención
                        Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontParamsToAtentionCenter", MODULE_NAME)
                    Case "{ERR4}" 'Errores obteniendo los precios
                        Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("ErrorQueryValueInRateManual", MODULE_NAME)
                    Case Else
                        If String.IsNullOrEmpty(res.Message.Trim()) Then
                            Me.ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontStays", MODULE_NAME)
                        Else
                            Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message.Trim()
                            Me.GdcStays.DataSource = res.ObjectEmbbeded
                            Me.GdcStays.RefreshDataSource()
                            If res.ObjectEmbbeded IsNot Nothing AndAlso res.ObjectEmbbeded.Count > 0 AndAlso res.ObjectEmbbeded(0).ADINGRESO.GENULTLIQUI IsNot Nothing Then
                                INDliLastLiquidate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDdeLastLiquidate.EditValue = res.ObjectEmbbeded(0).ADINGRESO.GENULTLIQUI
                            End If
                        End If
                End Select
            End If
        End Using
    End Sub

#End Region

#Region "Handlers"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _admissionCode = Nothing
        _caregroupId = Nothing
        _idOperatingUnit = Nothing
        _patientCode = Nothing
        _audit = Nothing
        _isLoaded = Nothing
    End Sub

    Private Sub GleLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles GleLiquidationType.EditValueChanged
        If Me._admissionCode IsNot Nothing AndAlso Not Me._admissionCode.Trim().Equals(String.Empty) Then
            Me.LoadStays(Me._admissionCode, Me._caregroupId, Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Realiza la persistencia de la liquidación y genera la orden de servicio
    ''' </summary>
    Private Async Sub BtnLiquidate_Click(sender As Object, e As EventArgs) Handles BtnLiquidate.Click
        Using model As New MLiquidation()
            If LyciLiquidateDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If DteLiqDate.EditValue Is Nothing Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar una fecha de corte"
                    Exit Sub
                End If
            End If
            Me.AsyncLoader()

            Dim healthAdministratorId As Integer = 0
            Dim thirdPartyId As Integer = 0
            If AdmissionObject.HealthAdministratorId IsNot Nothing AndAlso AdmissionObject.HealthAdministratorId > 0 Then
                healthAdministratorId = Convert.ToInt32(AdmissionObject.HealthAdministratorId)
                If AdmissionObject.HealthAdministratorId IsNot Nothing Then
                    Using md As New MHealthAdministrator(Me.Tag)
                        Dim healthAdm As ActionResult(Of HealthAdministrator) = md.GetHealthAdministratorByIdSimple(AdmissionObject.HealthAdministratorId)
                        If healthAdm.StateResult AndAlso healthAdm.ObjectEmbbeded IsNot Nothing Then
                            thirdPartyId = healthAdm.ObjectEmbbeded.ThirdPartyId
                        End If
                    End Using
                End If
            End If
            Dim liquidationType As eLiquidateStayOption = eLiquidateStayOption.DefectoManualGrupoAtencion
            If CByte(GleLiquidationType.EditValue) = 1 Then
                liquidationType = eLiquidateStayOption.MayorValor
            Else
                liquidationType = eLiquidateStayOption.CamaUltimoIngreso
            End If
            Dim res = Await model.LiquidateStays(Me._admissionCode, healthAdministratorId, thirdPartyId, Me._caregroupId, liquidationType, Me.DteMedicalOrder.EditValue, Me.DteLiqDate.EditValue, Me._patientCode, 0, Me._idOperatingUnit, Me._audit)
            Me.AsyncLoader(False)
            If res.StateResult Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Sub
    ''' <summary>
    ''' liquida manualmente las estancias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnManualLiquidation_Click(sender As Object, e As EventArgs) Handles INDBtnManualLiquidation.Click
        If LyciLiquidateDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If DteLiqDate.EditValue Is Nothing Then
                Me.ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar una fecha de corte"
                Exit Sub
            End If
        End If
        If MessageIndigo.Show("Esta seguro que desea liquidar manualmente las estancias", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using model As New MLiquidation()
                Me.AsyncLoader()
                Dim res = Await model.StayManualLiquidation(Me._admissionCode, Me.DteLiqDate.EditValue)
                Me.AsyncLoader(False)
                If res.StateResult Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Aqui se da formato a las fechas
    ''' </summary>
    Private Sub GdvStays_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GdvStays.CustomDrawCell
        Dim obj = CType(Me.GdvStays.GetRow(e.RowHandle), CHREGESTA)
        If e.Column.Equals(Me.ColEstInitDate) Then
            e.DisplayText = Convert.ToDateTime(obj.FECINIEST).ToString(SessionValues.Instance.Culture)
        ElseIf e.Column.Equals(Me.ColEstEndDate) Then
            'Si es el primer registro
            'If Object.ReferenceEquals(obj, CType(Me.GdcStays.DataSource, List(Of CHREGESTA))(0)) Then
            '    Me.DteLiqDate.Properties.MinValue = obj.FECINIEST
            'End If
            'Si es el ultimo registro
            If Object.ReferenceEquals(obj, CType(Me.GdcStays.DataSource, List(Of CHREGESTA)).LastOrDefault()) Then
                'Mostramos el campo de corte
                If obj.FECFINEST < obj.FECINIEST Then
                    'If Not Me._isLoaded Then
                    '    Me._isLoaded = True
                    '    Me.DteLiqDate.EditValue = DateTime.Now.AddDays(-1)
                    '    Me.DteLiqDate.Properties.MaxValue = DateTime.Now.AddDays(-1)
                    '    Me.DteLiqDate.Invalidate()
                    'End If
                    e.DisplayText = ResourceManager.GetString("MessageStayOpened", MODULE_NAME)
                    Me.LyciLiquidateDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    e.DisplayText = Convert.ToDateTime(obj.FECFINEST).ToString(SessionValues.Instance.Culture)
                    Me.LyciLiquidateDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Else
                e.DisplayText = Convert.ToDateTime(obj.FECFINEST).ToString(SessionValues.Instance.Culture)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se reliquida sin tracking para ver como queda
    ''' </summary>
    Private Sub DteLiqDate_EditValueChanged(sender As Object, e As EventArgs) Handles DteLiqDate.EditValueChanged, DteMedicalOrder.EditValueChanged, SleCareGroup.EditValueChanged
        If Me._isLoaded Then
            Me.LoadStays(Me._admissionCode, (If(Me.SleCareGroup.EditValue Is Nothing, Me._caregroupId, Me.SleCareGroup.EditValue)), Me.DteMedicalOrder.EditValue, Me.DteLiqDate.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se asigna el datasource al popUp de la rejilla
    ''' </summary>
    Private Sub RepEstShowDetails_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles RepEstShowDetails.QueryPopUp
        Dim obj = CType(Me.GdvStays.GetFocusedRow(), CHREGESTA)
        Me.GdcStayDetails.DataSource = obj.CHREGESTADET
        Me.GdcStayDetails.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Aqui se asigna el datasource de los grupos de atención
    ''' </summary>
    Private Sub SleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles SleCareGroup.QueryPopUp
        If Me.SleCareGroup.Properties.DataSource Is Nothing Then
            Using model As New MCtrFolio()
                Me.SleCareGroup.Properties.DataSource = model.ListCareGroupByStatus()
            End Using
        End If
    End Sub

#End Region

End Class