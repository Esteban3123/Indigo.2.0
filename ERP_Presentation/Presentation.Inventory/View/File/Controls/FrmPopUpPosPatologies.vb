#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Inventory.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel

#End Region

Public Class FrmPopUpPosPatologies

#Region "Fields"

    ''' <summary>
    ''' Objeto producto
    ''' </summary>
    Private _product As InventoryProduct
    ''' <summary>
    ''' Valor que indica si el formulario se encuentra cargando
    ''' </summary>
    Private _isLoading As Boolean = False

    ''' <summary>
    ''' Entidad de medicamento
    ''' </summary>
    Private MedicamentXpo As ATCXpo

    ''' <summary>
    ''' Presentador del producto
    ''' </summary>
    Private PresenterProduct As PInventoryProduct

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para controlar el retorno al proceso principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnToDeletes(sender As Object, e As ReturnToDeletes)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Muestra los mensajes
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Asigna la fuente de datos para la lista de grupos de facturación
    ''' </summary>
    Public WriteOnly Property BillingGroupNoPOSDatasource As XPInstantFeedbackSource
        Set(value As XPInstantFeedbackSource)
            Me.INDSlBillingGroupNoPOS.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna la fuente de datos para la lista de patologías
    ''' </summary>
    Public WriteOnly Property Patologies As XPInstantFeedbackSource
        Set(value As XPInstantFeedbackSource)
            Me.INDslePatologie.Properties.DataSource = value
        End Set
    End Property

    Public Property MinimumAge() As Byte
        Get
            Return INDSpeMinumumAge.EditValue
        End Get
        Set(ByVal value As Byte)
            INDSpeMinumumAge.EditValue = value
        End Set
    End Property

    Public Property MaximumAge() As Byte
        Get
            Return INDSpeMaximumAge.EditValue
        End Get
        Set(ByVal value As Byte)
            INDSpeMaximumAge.EditValue = value
        End Set
    End Property

    Public Property AgeMeasure() As Byte?
        Get
            Return INDSleAgeMeasure.EditValue
        End Get
        Set(ByVal value As Byte?)
            INDSleAgeMeasure.EditValue = value
        End Set
    End Property

    Private _posPathologies As POSPathologies
    Public Property ObjPOSPathologies() As POSPathologies
        Get
            Return _posPathologies
        End Get
        Set(ByVal value As POSPathologies)
            _posPathologies = value
        End Set
    End Property
    Private _editando As Boolean
    Public Property Editando() As Boolean
        Get
            Return _editando
        End Get
        Set(ByVal value As Boolean)
            _editando = value
        End Set
    End Property

    Public Property DefineProfessional() As Boolean
        Get
            Return INDCtrYesNoDefineProfessional.EditValue
        End Get
        Set(ByVal value As Boolean)
            INDCtrYesNoDefineProfessional.EditValue = value
        End Set
    End Property

    Public Property ClinicalJustification() As String
        Get
            Return INDMmeClinicalJustification.EditValue
        End Get
        Set(ByVal value As String)
            INDMmeClinicalJustification.EditValue = value
        End Set
    End Property


#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="p">Producto a editar</param>
    Public Sub New(ByVal p As InventoryProduct, Optional _medicamentXpo As ATCXpo = Nothing)
        InitializeComponent()
        Me._product = p
        Me.MedicamentXpo = _medicamentXpo
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui inicializamos los controles con los valores del producto
    ''' </summary>
    Private Sub FrmPopUpPosPatologies_Load(sender As Object, e As EventArgs) Handles Me.Load
        PresenterProduct = New PInventoryProduct()
        _isLoading = True
        If _product IsNot Nothing AndAlso MedicamentXpo Is Nothing Then
            'If _product.POSProduct.HasValue Then
            '    Me.INDgleAllPatologiesPOS.EditValue = _product.POSProduct.Value
            'Else
            '    Me.INDgleAllPatologiesPOS.EditValue = False
            'End If

            Me.INDSlBillingGroupNoPOS.EditValue = Me._product.BillingGroupNoPosId

            'If Me._product.AllPOSPathologies.HasValue Then
            '    Me.INDgleAllPatologiesPOS.EditValue = Me._product.AllPOSPathologies.Value
            'Else
            '    Me.INDgleAllPatologiesPOS.EditValue = False
            'End If
            Me.INDgleAllPatologiesPOS.EditValue = False

            Me.INDgcPOSPatologies.DataSource = _product.POSPathologies
            HideControlsPatologies(Me.INDgleAllPatologiesPOS.EditValue)
            DefineProfessional = Me._product.DefineProfessional
            ClinicalJustification = Me._product.ClinicalJustification
        ElseIf MedicamentXpo IsNot Nothing Then
            Me.INDSlBillingGroupNoPOS.EditValue = MedicamentXpo.BillingGroupNoPosId
            'Me.INDgleAllPatologiesPOS.EditValue = MedicamentXpo.AllPOSPathologies
            Me.INDgleAllPatologiesPOS.EditValue = False
            DefineProfessional = MedicamentXpo.DefineProfessional
            ClinicalJustification = MedicamentXpo.ClinicalJustification
            GetPathologiesByMedicamentId(MedicamentXpo.Id)
            HideControlsPatologies(Me.INDgleAllPatologiesPOS.EditValue)
            INDgleAllPatologiesPOS.Properties.ReadOnly = True
            INDSlBillingGroupNoPOS.Properties.ReadOnly = True
            INDCtrYesNoDefineProfessional.Properties.ReadOnly = True
            INDMmeClinicalJustification.Properties.ReadOnly = True
            INDslePatologie.Properties.ReadOnly = True
            INDSpeMinumumAge.Properties.ReadOnly = True
            INDSpeMaximumAge.Properties.ReadOnly = True
            INDSleAgeMeasure.Properties.ReadOnly = True
            BtnOk.Enabled = False
        End If

        IndigoGridControl1.RefreshGrid(INDgcPOSPatologies)
        Dim _listAction As New List(Of eAcciones)()
        _listAction.Add(eAcciones.Remove)
        _listAction.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvPOSPatologies, _listAction)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvPOSPatologies.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        _isLoading = False
    End Sub

    Private Sub GetPathologiesByMedicamentId(medicamentId As Integer)
        Dim listPathologiesXpo = PresenterProduct.GetListPathologiesByMedicamentId(medicamentId)
        Dim listPathologies As New List(Of POSPathologies)
        If listPathologiesXpo IsNot Nothing AndAlso listPathologiesXpo.Count > 0 Then
            For Each item In listPathologiesXpo
                Dim pathologies As New POSPathologies
                With pathologies
                    .Id = item.Id
                    .MedicamentId = item.MedicamentId
                    .DiagnosticId = item.DiagnosticId.Id
                    .DiagnosticCode = item.DiagnosticId.Code
                    .DiagnosticName = item.DiagnosticId.Name
                    .MinimumAge = item.MinimumAge
                    .MaximumAge = item.MaximumAge
                    .AgeMeasure = item.AgeMeasure
                    .AgeMeasureName = item.AgeMeasureName
                End With
                listPathologies.Add(pathologies)
            Next
            Me.INDgcPOSPatologies.DataSource = listPathologies
        End If
    End Sub

    ''' <summary>
    ''' Aqui se agrega la patología seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbAddPathologie_Click(sender As Object, e As EventArgs) Handles INDsbAddPathologie.Click
        If INDslePatologie.EditValue IsNot Nothing Then
            Dim _diagnosticId As Integer = Convert.ToInt32(INDslePatologie.EditValue)
            If AgeMeasure Is Nothing Then
                AgeMeasure = 1
            End If
            If Me.MinimumAge > Me.MaximumAge Then
                Mensaje(EeventViewerImages.Advertencia) = "La edad mínima debe ser menor a la edad máxima"
                Return
            End If
            Dim _diagnostic As XPCollection(Of DiagnosticXpo) = Nothing
            If Editando OrElse Not _product.POSPathologies.Any(Function(x) x.DiagnosticId = _diagnosticId AndAlso x.MinimumAge = Me.MinimumAge AndAlso x.MaximumAge = Me.MaximumAge AndAlso x.AgeMeasure = Me.AgeMeasure) Then
                Using model As New MInventoryProduct("304")
                    _diagnostic = model.ListDiagnosticById(INDslePatologie.EditValue)
                End Using
            End If
            If Editando Then
                If _product.POSPathologies.Any(Function(x) x.DiagnosticId = _diagnosticId AndAlso x.MinimumAge = Me.MinimumAge AndAlso x.MaximumAge = Me.MaximumAge AndAlso x.AgeMeasure = Me.AgeMeasure AndAlso Not x.Equals(ObjPOSPathologies)) Then
                    Mensaje(EeventViewerImages.Advertencia) = "Existe un aclaración con los mismos datos"
                    Return
                Else
                    With ObjPOSPathologies
                        .DiagnosticId = CType(INDslePatologie.EditValue, Integer)
                        .DiagnosticCode = _diagnostic(0).Code
                        .DiagnosticName = _diagnostic(0).Name
                        .MinimumAge = Me.MinimumAge
                        .MaximumAge = Me.MaximumAge
                        .AgeMeasure = Me.AgeMeasure
                        .AgeMeasureName = IIf(AgeMeasure = 1, "Años", IIf(AgeMeasure = 2, "Meses", IIf(AgeMeasure = 3, "Dias", "")))
                    End With
                    If ObjPOSPathologies.Id > 0 Then
                        ObjPOSPathologies.MarkAsModified
                    End If
                End If
            Else
                If Not _product.POSPathologies.Any(Function(x) x.DiagnosticId = _diagnosticId AndAlso x.MinimumAge = Me.MinimumAge AndAlso x.MaximumAge = Me.MaximumAge AndAlso x.AgeMeasure = Me.AgeMeasure) Then
                    Dim _pathologie As New POSPathologies()
                    With _pathologie
                        .DiagnosticId = CType(INDslePatologie.EditValue, Integer)
                        .DiagnosticCode = _diagnostic(0).Code
                        .DiagnosticName = _diagnostic(0).Name
                        .MinimumAge = Me.MinimumAge
                        .MaximumAge = Me.MaximumAge
                        .AgeMeasure = Me.AgeMeasure
                        .AgeMeasureName = IIf(AgeMeasure = 1, "Años", IIf(AgeMeasure = 2, "Meses", IIf(AgeMeasure = 3, "Dias", "")))
                    End With
                    _product.POSPathologies.Add(_pathologie)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Ya existe la aclaración"
                    Return
                End If
            End If

            LimpiarControles()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una patología"
        End If
    End Sub

    Private Sub LimpiarControles()
        INDslePatologie.EditValue = Nothing
        Me.MinimumAge = 0
        Me.MaximumAge = 120
        Me.AgeMeasure = 1
        INDgcPOSPatologies.RefreshDataSource()
        Editando = False
        INDsbAddPathologie.Text = "Agregar"
    End Sub

    ''' <summary>
    ''' Aqui se elimina la potología seleccionada
    ''' </summary>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim btn As DevExpress.XtraEditors.SimpleButton
        Dim btnEdith As DevExpress.XtraEditors.ButtonEdit
        Dim barbtn As DevExpress.XtraBars.BarButtonItem
        Dim _btnTag As String = String.Empty

        btn = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If btn Is Nothing Then
            btnEdith = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If btnEdith IsNot Nothing Then
                _btnTag = btnEdith.Text
            Else
                barbtn = TryCast(sender, DevExpress.XtraBars.BarButtonItem)
                If barbtn IsNot Nothing Then
                    _btnTag = barbtn.Caption
                End If
            End If
        Else
            _btnTag = btn.Tag
        End If

        Select Case _btnTag
            Case "Remove", "Eliminar"
                If MedicamentXpo IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item no se puede eliminar porque viene desde el medicamento"
                    Exit Sub
                End If
                Dim _posPathologies As POSPathologies = CType(INDgvPOSPatologies.GetFocusedRow(), POSPathologies)
                _posPathologies.MarkAsDeleted()
                'INDgcPOSPatologies.RefreshDataSource()
                LimpiarControles()
                RaiseEvent ReturnToDeletes(Nothing, New ReturnToDeletes With {.POSPathologiesDelete = _posPathologies})
            Case "Edit", "Editar"
                If MedicamentXpo IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item no se puede editar porque viene desde el medicamento"
                    Exit Sub
                End If
                ObjPOSPathologies = CType(INDgvPOSPatologies.GetFocusedRow(), POSPathologies)
                If ObjPOSPathologies IsNot Nothing Then
                    INDslePatologie.EditValue = ObjPOSPathologies.DiagnosticId
                    Me.MinimumAge = ObjPOSPathologies.MinimumAge
                    Me.MaximumAge = ObjPOSPathologies.MaximumAge
                    INDSleAgeMeasure_QueryPopUp(Nothing, Nothing)
                    AgeMeasure = ObjPOSPathologies.AgeMeasure
                    INDsbAddPathologie.Text = "Modificar"
                    Editando = True
                End If
        End Select

    End Sub

    ''' <summary>
    ''' Aqui se asigna el valor al objeto
    ''' </summary>
    Private Sub INDgleAllPatologiesPOS_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAllPatologiesPOS.EditValueChanged
        If Not _isLoading Then
            Me._product.AllPOSPathologies = INDgleAllPatologiesPOS.EditValue
            HideControlsPatologies(INDgleAllPatologiesPOS.EditValue)
            If INDgleAllPatologiesPOS.EditValue = True Then
                While Me._product.POSPathologies.Count > 0
                    If Me._product.POSPathologies(0).Id > 0 Then
                        Me._product.POSPathologies(0).MarkAsDeleted()
                    Else
                        Me._product.POSPathologies.Remove(Me._product.POSPathologies(0))
                    End If
                End While
                INDgcPOSPatologies.RefreshDataSource()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCtrYesNoDefineProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDCtrYesNoDefineProfessional.EditValueChanged
        If INDCtrYesNoDefineProfessional.EditValue IsNot Nothing AndAlso CBool(INDCtrYesNoDefineProfessional.EditValue) Then
            INDLciClinicalJustification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciClinicalJustification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ClinicalJustification = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de asociacion de insumos / medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAgeMeasure_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAgeMeasure.QueryPopUp
        If INDSleAgeMeasure.Properties.DataSource Is Nothing Then
            Dim ListAgeMeasure As New List(Of Tuple(Of Byte, String))
            ListAgeMeasure.Add(New Tuple(Of Byte, String)(1, "Años"))
            ListAgeMeasure.Add(New Tuple(Of Byte, String)(2, "Meses"))
            ListAgeMeasure.Add(New Tuple(Of Byte, String)(3, "Dias"))
            INDSleAgeMeasure.Properties.DataSource = ListAgeMeasure
        End If
    End Sub

    ''' <summary>
    ''' Oculta o muestra los controles
    ''' </summary>
    Private Sub HideControlsPatologies(AllPOSPatologies As Boolean)
        If AllPOSPatologies Then
            INDLciBillingGroupNoPOS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSlBillingGroupNoPOS.EditValue = Nothing
            INDliPathologie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliAddPathologie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliListPatologies.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLciBillingGroupNoPOS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliPathologie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliAddPathologie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliListPatologies.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Aqui se asigna el valor al objeto
    ''' </summary>
    Private Sub INDSlBillingGroupNoPOS_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlBillingGroupNoPOS.EditValueChanged
        If Not _isLoading Then
            Me._product.BillingGroupNoPosId = INDSlBillingGroupNoPOS.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Aqui se cierra el formulario
    ''' </summary>
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        If ValidateFields() Then
            If Me._product IsNot Nothing Then
                Me._product.DefineProfessional = DefineProfessional
                Me._product.ClinicalJustification = ClinicalJustification
            End If
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

    ''' <summary>
    ''' Aqui se borra el grupo de facturación
    ''' </summary>
    Private Sub INDSlBillingGroupNoPOS_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlBillingGroupNoPOS.ButtonClick
        INDSlBillingGroupNoPOS.EditValue = Nothing
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida que se haya seleccionado un grupo de facturación si se pretende agregar patologías
    ''' </summary>
    ''' <returns>Valor que indica si pasa la validación</returns>
    Private Function ValidateFields() As Boolean
        If Not Me._product.AllPOSPathologies.HasValue OrElse Not Me._product.AllPOSPathologies.Value Then
            If Me._product.POSPathologies.Count > 0 AndAlso Not Me._product.BillingGroupNoPosId.HasValue Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un grupo de facturación No POS"
                Me.INDSlBillingGroupNoPOS.Focus()
                Return False
            ElseIf DefineProfessional AndAlso ClinicalJustification Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Ingrese justificación clínica"
                Me.INDMmeClinicalJustification.Focus()
                Return False
            End If
        End If

        Return True
    End Function

#End Region

End Class

Public Class ReturnToDeletes
    Inherits EventArgs

    ''' <summary>
    ''' Representa a la entidad que se esta eliminando
    ''' </summary>
    ''' <returns></returns>
    Property POSPathologiesDelete As POSPathologies

End Class