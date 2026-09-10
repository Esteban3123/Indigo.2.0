#Region "Imports"

Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Security.MVP
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Encapsula la creación y modificación de secuencias numericas por cada frontal
''' </summary>
Public Class FrmSequences
    Implements ISequences

#Region "Fields"

    Private _myModel As MSequence
    Private _objSequence As Object
    Private _objSequenceD As Object
    Private _listScopes As List(Of Tuple(Of String, String))
    Private _listIsManual As List(Of Tuple(Of Boolean, String))
    Private _listIsSequential As List(Of Tuple(Of Boolean, String))
    Private _listExceptionsTreasury As List(Of String)
    Private _listExceptionsInventory As List(Of String)
    Private _additionalScopes As Dictionary(Of String, List(Of Tuple(Of String, String)))
    Private _additionalScopeOptions As Dictionary(Of Tuple(Of String, String), List(Of Tuple(Of Integer, String)))

#End Region

#Region "Properties"

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Property Form As String Implements ISequences.Form
        Get
            Return If(Me.GleForm.EditValue Is Nothing, 0, Me.GleForm.EditValue)
        End Get
        Set(value As String)
            Me.GleForm.EditValue = value
        End Set
    End Property

    Public Property IsManual As Boolean Implements ISequences.IsManual
        Get
            Return If(Me.RbtnIsManual.EditValue Is Nothing, False, CBool(Me.RbtnIsManual.EditValue))
        End Get
        Set(value As Boolean)
            Me.RbtnIsManual.EditValue = value
        End Set
    End Property

    Public Property IsSequential As Boolean Implements ISequences.IsSequential
        Get
            Return If(Me.RbtnIsSequential.EditValue Is Nothing, False, CBool(Me.RbtnIsSequential.EditValue))
        End Get
        Set(value As Boolean)
            Me.RbtnIsSequential.EditValue = value
        End Set
    End Property

    Public Property [Module] As Integer Implements ISequences.Module
        Get
            Return If(Me.GleModule.EditValue Is Nothing, 0, CInt(Me.GleModule.EditValue))
        End Get
        Set(value As Integer)
            Me.GleModule.EditValue = value
        End Set
    End Property

    Public Property NextNumber As Integer Implements ISequences.NextNumber
        Get
            Return If(Me.SpnNextNumber.EditValue Is Nothing, 0, CInt(Me.SpnNextNumber.EditValue))
        End Get
        Set(value As Integer)
            Me.SpnNextNumber.EditValue = value
        End Set
    End Property

    Public Property OperatingUnit As Integer Implements ISequences.OperatingUnit
        Get
            Return If(Me.GleOperatingUnit.EditValue Is Nothing, 0, CInt(Me.GleOperatingUnit.EditValue))
        End Get
        Set(value As Integer)
            Me.GleOperatingUnit.EditValue = value
        End Set
    End Property

    Public Property Rate As Integer Implements ISequences.Rate
        Get
            Return If(Me.SpnRate.EditValue Is Nothing, 0, CInt(Me.SpnRate.EditValue))
        End Get
        Set(value As Integer)
            Me.SpnRate.EditValue = value
        End Set
    End Property

    Public Property Scope As String Implements ISequences.Scope
        Get
            Return If(Me.GleScope.EditValue Is Nothing, String.Empty, Me.GleScope.EditValue.ToString())
        End Get
        Set(value As String)
            Me.GleScope.EditValue = value
        End Set
    End Property

    Public Property SequencePattern As Integer Implements ISequences.SequencePattern
        Get
            Return If(Me.GleSequencePattern.EditValue Is Nothing, 0, CInt(Me.GleSequencePattern.EditValue))
        End Get
        Set(value As Integer)
            Me.GleSequencePattern.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidLenght As Boolean Implements ISequences.ValidLenght
        Get
            Return If(Me.INDGleValidLenght.EditValue Is Nothing, False, CBool(Me.INDGleValidLenght.EditValue))
        End Get
        Set(value As Boolean)
            Me.INDGleValidLenght.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property MinimumChar As Byte Implements ISequences.MinimumChar
        Get
            Return If(Me.INDSeMinimumChar.EditValue Is Nothing, 1, CByte(Me.INDSeMinimumChar.EditValue))
        End Get
        Set(value As Byte)
            Me.INDSeMinimumChar.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property MaximumChar As Byte Implements ISequences.MaximumChar
        Get
            Return If(Me.INDSeMaximumChar.EditValue Is Nothing, 20, CByte(Me.INDSeMaximumChar.EditValue))
        End Get
        Set(value As Byte)
            Me.INDSeMaximumChar.EditValue = value
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New()
        InitializeComponent()
        Me._myModel = New MSequence()
        Me._listScopes = New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("O", "Organización"), New Tuple(Of String, String)("OU", "Unidad Operativa")}
        Me._listIsManual = New List(Of Tuple(Of Boolean, String)) From {New Tuple(Of Boolean, String)(True, "Si"), New Tuple(Of Boolean, String)(False, "No")}
        Me._listIsSequential = New List(Of Tuple(Of Boolean, String)) From {New Tuple(Of Boolean, String)(True, "Si"), New Tuple(Of Boolean, String)(False, "No")}
        Me._listExceptionsTreasury = New List(Of String) From {"635", "636"}
        Me._listExceptionsInventory = New List(Of String) From {"316", "317", "318", "321", "322", "323", "329", "332", "1402", "1403", "1514", "1516", "1518", "1519", "1977"}
        Me.GenerateAdditionalScopes()
    End Sub

#End Region

#Region "Handlers"

    Private Async Sub FrmSequences_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Deshacer()
        Me.BarraBotones.OperatingUnitVisible = False
        Me.AddActionsColumns()
        Dim res = Await Me._myModel.ListSequencePatterns()
        Me.GlePrefixSequencePattern.Properties.DataSource = res.Where(Function(p) p.Pattern.Contains("%pfx")).ToList()
        Me.RbtnIsManual.Properties.DataSource = Me._listIsManual
        Me.RbtnIsSequential.Properties.DataSource = Me._listIsSequential
        Me.INDGleValidLenght.Properties.DataSource = Me._listIsSequential
    End Sub

    Private Sub GleModule_EditValueChanged(sender As Object, e As EventArgs) Handles GleModule.EditValueChanged
        If Not String.IsNullOrEmpty(Me.GleModule.EditValue) Then
            Me.SetStatusControls(1)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
        AjustarOcultarValidLenght()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub AjustarOcultarValidLenght()

        Dim visibilidad As DevExpress.XtraLayout.Utils.LayoutVisibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        If String.IsNullOrEmpty(GleModule.EditValue) OrElse ((Not String.IsNullOrEmpty(GleModule.EditValue)) AndAlso GleModule.EditValue <> "45" AndAlso GleModule.EditValue <> "21") Then 'Si el modulo que eligen no es inventarios
            visibilidad = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf String.IsNullOrEmpty(GleForm.EditValue) OrElse ((Not String.IsNullOrEmpty(GleForm.EditValue)) AndAlso GleForm.EditValue <> "2059") Then 'no es ATC
            visibilidad = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf String.IsNullOrEmpty(RbtnIsManual.EditValue) OrElse ((Not String.IsNullOrEmpty(RbtnIsManual.EditValue)) AndAlso Not CBool(Me.RbtnIsManual.EditValue)) Then 'NO ES MANUAL
            visibilidad = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        INDGleValidLenght.EditValue = False
        INDLciValidLength.Visibility = visibilidad
    End Sub

    Private Sub GleForm_EditValueChanged(sender As Object, e As EventArgs) Handles GleForm.EditValueChanged
        If Me.GleForm.EditValue IsNot Nothing Then
            Me.LoadControls()
            If (Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString())) Then 'Administración de efectivo
                Me._listScopes = New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("O", "Cajas / Bancos"), New Tuple(Of String, String)("OU", "Unidad Operativa")}
            ElseIf (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString())) Then 'Inventario
                Me._listScopes = New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("O", "Almacenes"), New Tuple(Of String, String)("OU", "Unidad Operativa")}
            Else
                Me._listScopes = New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("O", "Organización"), New Tuple(Of String, String)("OU", "Unidad Operativa")}
            End If
            AjustarOcultarValidLenght()
            If Me._additionalScopes.ContainsKey(Me.GleForm.EditValue) Then
                Me._listScopes.AddRange(Me.GetScopes())
            End If
            Me.GleScope.Properties.DataSource = Me._listScopes
            End If
    End Sub

    Private Sub GleModule_QueryPopUpAsync(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GleModule.QueryPopUp
        If Me.GleModule.Properties.DataSource Is Nothing Then
            Me.GleModule.Properties.DataSource = Me._myModel.ListModulesAsync()
        End If
    End Sub

    Private Sub GleForm_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GleForm.QueryPopUp
        Me.GleForm.Properties.DataSource = Nothing
        Me.GleForm.Properties.DataSource = Me._myModel.ListForms(CInt(Me.GleModule.EditValue))
    End Sub

    Private Async Sub GleOperatingUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GleOperatingUnit.QueryPopUp
        If Me.GleOperatingUnit.Properties.DataSource Is Nothing Then
            Me.GleOperatingUnit.Properties.DataSource = Await Me._myModel.ListOperatingUnits()
        End If
    End Sub

    Private Async Sub GleSequencePattern_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GleSequencePattern.QueryPopUp, GlePrefixSequencePattern.QueryPopUp
        If CType(sender, DevExpress.XtraEditors.GridLookUpEdit).Properties.DataSource Is Nothing Then
            Dim res = Await Me._myModel.ListSequencePatterns()
            CType(sender, DevExpress.XtraEditors.GridLookUpEdit).Properties.DataSource = If(sender.Name.Equals("GleSequencePattern"), res, res.Where(Function(p) p.Pattern.Contains("%pfx")).ToList())
        End If
    End Sub

    Private Sub RbtnIsManual_EditValueChanged(sender As Object, e As EventArgs) Handles RbtnIsManual.EditValueChanged
        If Me.RbtnIsManual.EditValue IsNot Nothing Then
            Me.LycRoot.BeginUpdate()
            Me.GleScope.Enabled = Not CBool(Me.RbtnIsManual.EditValue)
            Me.RbtnIsSequential.Enabled = Not CBool(Me.RbtnIsManual.EditValue)
            Me.PceAddSequenceDetail.Enabled = Not CBool(Me.RbtnIsManual.EditValue)
            'Me.SpnRate.Enabled = Not CBool(Me.RbtnIsManual.EditValue)

            Me.RbtnIsSequential.EditValue = True
            Me.SpnRate.Enabled = False

            If CBool(Me.RbtnIsManual.EditValue) Then
                If Me._objSequence IsNot Nothing Then
                    Dim list As IList = CType(Me._objSequence.GetType().GetProperties().Where(Function(p) p.Name.EndsWith("Detail")).First().GetValue(Me._objSequence), IList)
                    If list IsNot Nothing AndAlso list.Count > 0 Then
                        For index = 0 To list.Count - 1
                            list.RemoveAt(0)
                        Next
                    End If
                End If
            End If
            AjustarOcultarValidLenght()
            Me.PceAddSequenceDetail.Update()
            Me.LycRoot.EndUpdate()
            System.Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Private Sub GleScope_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GleScope.EditValueChanging
        If e.NewValue IsNot Nothing And Me.GleForm.EditValue IsNot Nothing Then
            If Me._objSequence IsNot Nothing Then
                Dim list As IList = CType(Me._objSequence.GetType().GetProperties().Where(Function(p) p.Name.EndsWith("Detail")).First().GetValue(Me._objSequence), IList)
                If e.NewValue.Equals("O") AndAlso Not Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) AndAlso Not Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()) AndAlso list.Count > 1 Then
                    e.Cancel = True
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Para establecer el ámbito de la secuencia a tipo (Organización), debe tener un solo detalle"
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleValidLenght_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleValidLenght.EditValueChanged
        INDSeMinimumChar.EditValue = 1
        INDSeMaximumChar.EditValue = 20
        If ValidLenght Then
            INDLciMinimumChar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciMaximumChar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciMinimumChar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciMaximumChar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeMinimumChar_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeMinimumChar.EditValueChanged
        If MinimumChar > MaximumChar Then
            MinimumChar = MaximumChar
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeMaximumChar_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeMaximumChar.EditValueChanged
        If MaximumChar < MinimumChar Then
            MaximumChar = MinimumChar
        End If
    End Sub

    Private Sub GleScope_EditValueChanged(sender As Object, e As EventArgs) Handles GleScope.EditValueChanged
        If Me.GleScope.EditValue IsNot Nothing And Me.GleForm.EditValue IsNot Nothing Then
            Me.LycRoot.BeginUpdate()

            If Me.GleScope.EditValue.ToString().Equals("O") AndAlso (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString())) Then
                Me.LyciIsManual.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciIsSequential.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciPrefixSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciLoadPrefixs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Me.LyciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciOperatingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Me.ColPrefix.Visible = True
                Me.ColPrefix.VisibleIndex = 0
                Me.ColOperatingUnit.Visible = False
                Me.colType.Visible = False
                Me.ColSequencePattern.Visible = False

                Me.RbtnIsSequential.EditValue = True
                Me.RbtnIsManual.EditValue = False

                Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
                If det IsNot Nothing Then
                    det.Clear()
                    Me.BtnLoadPrefixs_Click(Me, New EventArgs())
                End If
            ElseIf Not (Me.GleScope.EditValue.ToString().Equals("O") OrElse Me.GleScope.EditValue.ToString().Equals("OU")) Then
                Me.LyciIsManual.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciIsSequential.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciPrefixSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciLoadPrefixs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.LyciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciOperatingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.LyciSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.ColPrefix.Visible = False
                Me.ColOperatingUnit.Visible = False
                Me.colType.Visible = True
                Me.colType.VisibleIndex = 0
                Me.ColSequencePattern.Visible = True

                Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
                If det IsNot Nothing AndAlso (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString())) Then
                    While det.Count > 0
                        If det(0).GetType().Equals(GetType(Domain.Entities.TreasurySequenceDetail)) Then
                            CType(det(0), Domain.Entities.TreasurySequenceDetail).MarkAsDeleted()
                        ElseIf det(0).GetType().Equals(GetType(Domain.Entities.InventorySequenceDetail)) Then
                            CType(det(0), Domain.Entities.InventorySequenceDetail).MarkAsDeleted()
                        End If
                    End While
                End If

                Me.GleType.Properties.DataSource = Me.GetListOptions()
            Else
                Me.LyciIsManual.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciIsSequential.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciPrefixSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciLoadPrefixs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.LyciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LyciOperatingUnit.Visibility = If(Me.GleScope.EditValue.ToString().Equals("O"), DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                Me.LyciSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LyciPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.ColPrefix.Visible = False
                Me.ColOperatingUnit.Visible = If(Me.GleScope.EditValue.ToString().Equals("O"), False, True)
                Me.ColOperatingUnit.VisibleIndex = If(Me.GleScope.EditValue.ToString().Equals("O"), -1, 0)
                Me.colType.Visible = False
                Me.ColSequencePattern.Visible = True

                Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
                If det IsNot Nothing AndAlso (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString())) Then
                    While det.Count > 0
                        If det(0).GetType().Equals(GetType(Domain.Entities.TreasurySequenceDetail)) Then
                            CType(det(0), Domain.Entities.TreasurySequenceDetail).MarkAsDeleted()
                        ElseIf det(0).GetType().Equals(GetType(Domain.Entities.InventorySequenceDetail)) Then
                            CType(det(0), Domain.Entities.InventorySequenceDetail).MarkAsDeleted()
                        End If
                    End While
                End If
            End If

            Me.ApllyStyleColActions()

            Me.LycRoot.EndUpdate()
            System.Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Private Sub RbtnIsSequential_EditValueChanged(sender As Object, e As EventArgs) Handles RbtnIsSequential.EditValueChanged, RbtnIsSequential.EnabledChanged
        If Me.RbtnIsSequential.EditValue IsNot Nothing Then
            Me.LycRoot.BeginUpdate()
            Me.SpnRate.EditValue = 1
            Me.SpnRate.Enabled = Not CBool(Me.RbtnIsSequential.EditValue)
            Me.LycRoot.EndUpdate()
            System.Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Private Sub GleSequencePattern_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GleSequencePattern.ButtonClick, GlePrefixSequencePattern.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using frmTras As New FrmTransparent(New FrmSequencePattern(), False)
                frmTras.ShowDialog(Me)
                CType(sender, DevExpress.XtraEditors.GridLookUpEdit).Properties.DataSource = Nothing
            End Using
        End If
    End Sub

    Private Sub PceAddSequenceDetail_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles PceAddSequenceDetail.CloseUp
        Me._objSequenceD = Nothing
        Me.GleOperatingUnit.EditValue = Nothing
        Me.GleSequencePattern.EditValue = Nothing
        Me.SpnNextNumber.EditValue = Nothing
    End Sub

    Private Sub PceAddSequenceDetail_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PceAddSequenceDetail.QueryPopUp
        If Me._objSequenceD Is Nothing Then
            Me.BtnAddSequenceDetail.Text = "Agregar"
            Me._objSequenceD = Me._myModel.GetObjSequenceDInstance(Me.GleForm.GetSelectedDataRow().SequenceModule)
            Me.AssingDValuesToControls()
        Else
            Me.BtnAddSequenceDetail.Text = "Actualizar"
        End If
    End Sub

    Private Sub BtnAddSequenceDetail_Click(sender As Object, e As EventArgs) Handles BtnAddSequenceDetail.Click
        If String.IsNullOrEmpty(Me.GleScope.EditValue) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar el Ambito de la secuencia"
            Exit Sub
        End If

        Dim list = CType(Me._objSequence.GetType().GetProperties().Where(Function(p) p.Name.EndsWith("Detail")).First().GetValue(Me._objSequence), IList)
        If Not CBool(Me.RbtnIsManual.EditValue) AndAlso Not Me.GleScope.EditValue.ToString().Equals("O") Then
            If Me.GleScope.EditValue.ToString().Equals("OU") Then
                If Me.GleOperatingUnit.EditValue Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la unidad operativa"
                    Exit Sub
                End If

                If Me.BtnAddSequenceDetail.Text = "Agregar" Then
                    If list IsNot Nothing AndAlso CType(list, IEnumerable).Cast(Of Object).Any(Function(o) o.IdOperatingUnit = CInt(Me.GleOperatingUnit.EditValue)) Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = "La unidad operativa seleccionada ya se encuentra dentro del listado"
                        Exit Sub
                    End If
                Else
                    If list IsNot Nothing AndAlso CType(list, IEnumerable).Cast(Of Object).Any(Function(o) o.Id <> Me._objSequenceD.Id AndAlso o.IdOperatingUnit = CInt(Me.GleOperatingUnit.EditValue)) Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = "La unidad operativa seleccionada ya se encuentra dentro del listado"
                        Exit Sub
                    End If
                End If

                If Me.GleSequencePattern.EditValue Is Nothing OrElse Me.GleSequencePattern.EditValue = 0 Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un patron de secuencia"
                    Exit Sub
                End If
            Else
                If Me.GleType.EditValue Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo"
                    Exit Sub
                End If

                If Me.BtnAddSequenceDetail.Text = "Agregar" Then
                    If list IsNot Nothing AndAlso CType(list, IEnumerable).Cast(Of Object).Any(Function(o) o.Type = CInt(Me.GleType.EditValue)) Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = "El tipo seleccionada ya se encuentra dentro del listado"
                        Exit Sub
                    End If
                Else
                    If list IsNot Nothing AndAlso CType(list, IEnumerable).Cast(Of Object).Any(Function(o) o.Id <> Me._objSequenceD.Id AndAlso o.Type = CInt(Me.GleType.EditValue)) Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = "El tipo seleccionada ya se encuentra dentro del listado"
                        Exit Sub
                    End If
                End If

                If Me.GleSequencePattern.EditValue Is Nothing OrElse Me.GleSequencePattern.EditValue = 0 Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un patron de secuencia"
                    Exit Sub
                End If
            End If
        End If


        If Me.GleSequencePattern.EditValue Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar el patrón a usar"
            Exit Sub
        End If

        If Me.BtnAddSequenceDetail.Text = "Agregar" Then
            Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
            If Me.GleScope.EditValue.ToString().Equals("O") AndAlso det IsNot Nothing AndAlso det.Count > 0 AndAlso Not (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString())) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Una secuencia con un ámbito de tipo (Organización) no puede tener más de un detalle de secuencia"
            ElseIf Me.GleScope.EditValue IsNot Nothing AndAlso (Me.GleForm.EditValue IsNot Nothing AndAlso (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()))) OrElse (Me.GleScope.EditValue.ToString().Equals("OU") OrElse list.Count = 0) Then
                Me.AssingDValuesToObject()
                list.Add(Me._objSequenceD)
            End If
        Else
            Dim obj = (From l In list Where l.Id = Me._objSequenceD.Id Select l).ToList()
            If obj IsNot Nothing Then
                obj(0).IdSequense = Me.GleSequencePattern.EditValue
                obj(0).PatternName = Me.GleSequencePattern.Text
                obj(0).IdOperatingUnit = Me.GleOperatingUnit.EditValue
                If obj(0).GetType().GetProperties().Any(Function(p) p.Name.Equals("Type")) Then
                    obj(0).Type = Me.GleType.EditValue
                End If
                obj(0).Next = Me.SpnNextNumber.EditValue
                If obj(0).GetType().GetProperties().Any(Function(p) p.Name.Equals("Prefix")) Then
                    obj(0).Prefix = Me.TxtPrefix.Text.Trim()
                End If
                GdcSequenceDetails.RefreshDataSource()
            End If
        End If
        Me.PceAddSequenceDetail.ClosePopup()
    End Sub

    Private Async Sub BtnLoadPrefixs_Click(sender As Object, e As EventArgs) Handles BtnLoadPrefixs.Click
        Try
            If Me.GleForm.EditValue IsNot Nothing AndAlso (Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString())) AndAlso (Me.GleScope.EditValue IsNot Nothing AndAlso Me.GleScope.EditValue.ToString().Equals("O")) Then
                Me.AsyncLoader(True)
                Dim list = Await Me._myModel.ListTreasuryPrefixs(True)
                If list IsNot Nothing AndAlso list.Count > 0 Then
                    Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
                    If det IsNot Nothing Then
                        For Each p In list
                            If (From d As Domain.Entities.TreasurySequenceDetail In det Where d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(p.Trim()) Select d).ToList().Count > 0 Then
                                Dim dd = (From d As Domain.Entities.TreasurySequenceDetail In det Where d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(p.Trim()) Select d).FirstOrDefault()
                                dd.IdSequense = If(Me.GlePrefixSequencePattern.EditValue IsNot Nothing, CInt(Me.GlePrefixSequencePattern.EditValue), 0)
                            Else
                                det.Add(New Domain.Entities.TreasurySequenceDetail With {.IdSequenseTreasuryC = Me._objSequence.Id, .Prefix = p.Trim(), .Next = 1, .IdSequense = If(Me.GlePrefixSequencePattern.EditValue IsNot Nothing, CInt(Me.GlePrefixSequencePattern.EditValue), 0)})
                            End If
                        Next
                    End If
                End If
            ElseIf Me.GleForm.EditValue IsNot Nothing AndAlso (Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString())) AndAlso (Me.GleScope.EditValue IsNot Nothing AndAlso Me.GleScope.EditValue.ToString().Equals("O")) Then
                Me.AsyncLoader(True)
                Dim list = Await Me._myModel.ListInventoryPrefixs()
                If list IsNot Nothing AndAlso list.Count > 0 Then
                    Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
                    If det IsNot Nothing Then
                        For Each p In list
                            If (From d As Domain.Entities.InventorySequenceDetail In det Where d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(p.Trim()) Select d).ToList().Count > 0 Then
                                Dim dd = (From d As Domain.Entities.InventorySequenceDetail In det Where d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(p.Trim()) Select d).FirstOrDefault()
                                dd.IdSequense = If(Me.GlePrefixSequencePattern.EditValue IsNot Nothing, CInt(Me.GlePrefixSequencePattern.EditValue), 0)
                            Else
                                det.Add(New Domain.Entities.InventorySequenceDetail With {.InventorySequenceId = Me._objSequence.Id, .Prefix = p.Trim(), .Next = 1, .IdSequense = If(Me.GlePrefixSequencePattern.EditValue IsNot Nothing, CInt(Me.GlePrefixSequencePattern.EditValue), 0)})
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.AsyncLoader(False)
        End Try
    End Sub

    Private Sub GlePrefixSequencePattern_EditValueChanged(sender As Object, e As EventArgs) Handles GlePrefixSequencePattern.EditValueChanged
        If Me.GlePrefixSequencePattern.EditValue IsNot Nothing AndAlso Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()) Then
            Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
            If det IsNot Nothing Then
                For Each d As Domain.Entities.TreasurySequenceDetail In det
                    d.IdSequense = Me.GlePrefixSequencePattern.EditValue
                Next
            End If
            'Me.BtnLoadPrefixs_Click(Me, New EventArgs())
        ElseIf Me.GlePrefixSequencePattern.EditValue IsNot Nothing AndAlso Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) Then
            Dim det As IList = CType(Me.GdcSequenceDetails.DataSource, IList)
            If det IsNot Nothing Then
                For Each d As Domain.Entities.InventorySequenceDetail In det
                    d.IdSequense = Me.GlePrefixSequencePattern.EditValue
                Next
            End If
            Me.BtnLoadPrefixs_Click(Me, New EventArgs())
        End If
    End Sub

    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag
            Case "Edit"
                If CBool(Me.RbtnIsManual.EditValue) = False AndAlso Me.PceAddSequenceDetail.Enabled Then
                    If Me.GleOperatingUnit.Properties.DataSource Is Nothing Then
                        Me.GleOperatingUnit.Properties.DataSource = Await Me._myModel.ListOperatingUnits()
                    End If
                    If Me.GleSequencePattern.Properties.DataSource Is Nothing Then
                        Me.GleSequencePattern.Properties.DataSource = Await Me._myModel.ListSequencePatterns()
                    End If
                    Me._objSequenceD = Me.GdvSequenceDetails.GetFocusedRow()
                    Me.AssingDValuesToControls()
                    Me.PceAddSequenceDetail.ShowPopup()
                End If
            Case Else 'Remove
                Dim list As IList = CType(Me._objSequence.GetType().GetProperties().Where(Function(p) p.Name.EndsWith("Detail")).First().GetValue(Me._objSequence), IList)
                Dim obj = Me.GdvSequenceDetails.GetFocusedRow()
                list.Remove(obj)
        End Select
    End Sub

#End Region

#Region "Methods"

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Me.Deshacer()
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Dim withModule = If(String.IsNullOrEmpty(GleModule.EditValue), True, False)
        Deshacer(withModule)
    End Sub

    Private Sub Deshacer(Optional withModule As Boolean = False)
        CleanControls(withModule)
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not CBool(Me.RbtnIsManual.EditValue) AndAlso CType(Me.GdcSequenceDetails.DataSource, IList).Count = 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Debe agregar un patrón de secuencia"
            Exit Sub
        End If
        Try
            Me.AssingValuesToObject()
            Me.AsyncLoader(True)
            Dim res = Await Me._myModel.SaveSequence(Me.GleForm.GetSelectedDataRow().SequenceModule, Me._objSequence)
            If res IsNot Nothing AndAlso res.StateResult Then
                Me.Mensaje(EeventViewerImages.Informacion) = "La secuencia se ha guardado con éxito"
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido guardar los cambios en la secuencia"
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.AsyncLoader(False)
            Me.Deshacer()
        End Try
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    Private Async Sub LoadControls()
        Try
            Me.AsyncLoader(True)
            Me._objSequence = Await Me._myModel.GetSequence(Me.GleForm.GetSelectedDataRow().SequenceModule, Me.GleForm.EditValue)
            If Me._objSequence Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = $"Al Formulario {Me.GleForm.Properties.GetDisplayText(Me.GleForm.EditValue)} No se le puede parametrizar secuencia númerica "
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Exit Sub
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            Me.AsyncLoader(False)
        End Try

        If Me._objSequence Is Nothing OrElse Me._objSequence.Id = 0 Then
            Me._objSequence = Me._myModel.GetObjSequenceCInstanceByVal(Me.GleForm.GetSelectedDataRow().SequenceModule, Me.GleForm.EditValue)
            Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
        Else
            Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUpdate)
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False

        Me.SetStatusControls(2)
        Me.AssingValuesToControls()
        Me.ValidateEnabled()

        If Me._additionalScopes.ContainsKey(Me.GleForm.EditValue) Then
            If Me.GleScope.EditValue IsNot Nothing Then
                If Me._additionalScopeOptions.ContainsKey(New Tuple(Of String, String)(Me.GleForm.EditValue, Me.GleScope.EditValue)) Then
                    Dim scopeOptions = Me.GetListOptions()
                    If Me._objSequence.GetType().Equals(GetType(Domain.Entities.TreasurySequence)) Then
                        For Each detail In CType(Me._objSequence, Domain.Entities.TreasurySequence).TreasurySequenceDetail
                            detail.TypeName = scopeOptions.Where(Function(o) o.Item1 = detail.Type).Select(Function(o) o.Item2).FirstOrDefault()
                        Next
                    ElseIf Me._objSequence.GetType().Equals(GetType(Domain.Entities.InventorySequence)) Then
                        For Each detail In CType(Me._objSequence, Domain.Entities.InventorySequence).InventorySequenceDetail
                            detail.TypeName = scopeOptions.Where(Function(o) o.Item1 = detail.Type).Select(Function(o) o.Item2).FirstOrDefault()
                        Next
                    End If
                    GdcSequenceDetails.RefreshDataSource()
                End If
            End If
        End If
    End Sub

    Private Function GetSequenceDetails(ByVal seq As Object) As Object
        If seq IsNot Nothing AndAlso seq.GetType().GetProperties().Any(Function(p) p.Name.EndsWith("Detail")) Then
            Dim prop = seq.GetType().GetProperties().Where(Function(p) p.Name.EndsWith("Detail")).First()
            Return prop.GetValue(seq)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Limpia los controles en pantalla
    ''' </summary>
    Private Sub CleanControls(withModule As Boolean)
        If withModule Then
            Me.GleModule.EditValue = Nothing
            Me.SetStatusControls(3)
        Else
            Me.SetStatusControls(1)
        End If

        Me.GleForm.EditValue = Nothing
        Me.RbtnIsManual.EditValue = Nothing
        Me.GleScope.EditValue = Nothing
        Me.RbtnIsSequential.EditValue = Nothing
        Me.SpnRate.EditValue = Nothing
        Me.GleOperatingUnit.EditValue = Nothing
        Me.GleSequencePattern.EditValue = Nothing
        Me.SpnNextNumber.EditValue = Nothing
        Me.GdcSequenceDetails.DataSource = Nothing
        Me.GlePrefixSequencePattern.EditValue = Nothing
        Me.ValidLenght = False
        Me.MinimumChar = 1
        Me.MaximumChar = 20

        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        If withModule Then
            Me.GleModule.Focus()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = True
        Else
            Me.GleForm.Focus()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub

    ''' <summary>
    ''' Asigna el estado a los controles correspondiente
    ''' al nivel pasado por parametro
    ''' </summary>
    ''' <param name="level">Nivel a asignar</param>
    Private Sub SetStatusControls(Optional ByVal level As Byte = 0)
        Me.LycRoot.BeginUpdate()
        Me.GleModule.Enabled = False
        Me.GleForm.Enabled = False
        Me.RbtnIsManual.Enabled = False
        Me.GleScope.Enabled = False
        Me.RbtnIsSequential.Enabled = False
        Me.SpnRate.Enabled = False
        Me.GleSequencePattern.Enabled = False
        Me.SpnNextNumber.Enabled = False
        Me.GdcSequenceDetails.Enabled = False
        Me.PceAddSequenceDetail.Enabled = False
        Me.INDGleValidLenght.Enabled = False
        Me.INDSeMaximumChar.Enabled = False
        Me.INDSeMinimumChar.Enabled = False
        Me.LyciPrefixSequencePattern.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LyciLoadPrefixs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LyciPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LyciOperatingUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.ColPrefix.Visible = False
        Me.ColOperatingUnit.Visible = True
        Me.ColSequencePattern.Visible = True
        Select Case level
            Case 1
                Me.GleForm.Enabled = True
            Case 2
                Me.RbtnIsManual.Enabled = True
                Me.GleScope.Enabled = True
                Me.RbtnIsSequential.Enabled = True
                Me.SpnRate.Enabled = True
                Me.GleSequencePattern.Enabled = True
                Me.SpnNextNumber.Enabled = True
                Me.GdcSequenceDetails.Enabled = True
                Me.PceAddSequenceDetail.Enabled = True
                Me.INDGleValidLenght.Enabled = True
                Me.INDSeMaximumChar.Enabled = True
                Me.INDSeMinimumChar.Enabled = True
            Case 3
                Me.GleModule.Enabled = True
        End Select
        Me.LycRoot.EndUpdate()
    End Sub

    Private Sub AssingValuesToControls()
        If Me._objSequence IsNot Nothing Then
            Me.RbtnIsManual.EditValue = Me._objSequence.IsManual
            Me.RbtnIsSequential.EditValue = Me._objSequence.Sequential
            Me.GleScope.EditValue = Me._objSequence.Scope
            Me.SpnRate.EditValue = Me._objSequence.Rate

            If Me._objSequence.GetType().Equals(GetType(Domain.Entities.InventorySequence)) Then
                Me.ValidLenght = Me._objSequence.RequireValidateLength
                Me.MinimumChar = Me._objSequence.MinimumLength
                Me.MaximumChar = Me._objSequence.MaximumLength
            End If

            If Me._objSequence.GetType().GetProperties().Any(Function(p) p.Name.Equals("IdSequence")) Then
                Me.GlePrefixSequencePattern.EditValue = Me._objSequence.IdSequence
            End If
            Me.GdcSequenceDetails.DataSource = Me.GetSequenceDetails(Me._objSequence)
        End If
    End Sub

    Private Sub AssingValuesToObject()
        Me._objSequence.IsManual = Me.RbtnIsManual.EditValue
        Me._objSequence.Scope = Me.GleScope.EditValue
        Me._objSequence.Sequential = Me.RbtnIsSequential.EditValue
        Me._objSequence.Rate = Me.SpnRate.EditValue
        If Me._objSequence.GetType().Equals(GetType(Domain.Entities.InventorySequence)) Then
            Me._objSequence.RequireValidateLength = Me.ValidLenght
            Me._objSequence.MinimumLength = Me.MinimumChar
            Me._objSequence.MaximumLength = Me.MaximumChar
        End If

        If Me._objSequence.GetType().GetProperties().Any(Function(p) p.Name.Equals("IdSequence")) Then
            Me._objSequence.IdSequence = Me.GlePrefixSequencePattern.EditValue
        End If
    End Sub

    Private Sub AssingDValuesToControls()
        If Me._objSequenceD IsNot Nothing Then
            Me.GleOperatingUnit.EditValue = Me._objSequenceD.IdOperatingUnit
            If Me._objSequenceD.GetType().GetProperties().Any(Function(p) p.Name.Equals("Type")) Then
                Me.GleType.EditValue = Me._objSequenceD.Type
            End If
            Me.GleSequencePattern.EditValue = Me._objSequenceD.IdSequense
            If Me._objSequenceD.GetType().GetProperties().Any(Function(p) p.Name.Equals("Prefix")) Then
                Me.TxtPrefix.Text = Me._objSequenceD.Prefix
            End If
            Me.SpnNextNumber.EditValue = Me._objSequenceD.Next
        End If
    End Sub

    Private Sub AssingDValuesToObject()
        If Me._objSequenceD IsNot Nothing Then
            Me._objSequenceD.IdOperatingUnit = Me.GleOperatingUnit.EditValue
            Me._objSequenceD.OperatingUnitName = If(Me.GleOperatingUnit.Properties.DataSource IsNot Nothing AndAlso Me._objSequenceD.IdOperatingUnit IsNot Nothing, CType(Me.GleOperatingUnit.Properties.DataSource, List(Of Domain.Entities.OperatingUnit)).Where(Function(op) op.Id = CInt(Me._objSequenceD.IdOperatingUnit)).FirstOrDefault().UnitName, String.Empty)
            If Me._objSequenceD.GetType().GetProperties().Any(Function(p) p.Name.Equals("Type")) Then
                Me._objSequenceD.Type = Me.GleType.EditValue
                Me._objSequenceD.TypeName = Me.GleType.Text
            End If
            Me._objSequenceD.IdSequense = If((Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString())) AndAlso Me.GleScope.EditValue.ToString().Equals("O"), Me.GlePrefixSequencePattern.EditValue, Me.GleSequencePattern.EditValue)
            If CType(Me.GleSequencePattern.Properties.DataSource, List(Of Domain.Entities.Sequense)) IsNot Nothing Then
                Me._objSequenceD.PatternName = If(Me._objSequenceD.IdSequense IsNot Nothing AndAlso Me._objSequenceD.IdSequense > 0, CType(Me.GleSequencePattern.Properties.DataSource, List(Of Domain.Entities.Sequense)).Where(Function(sq) sq.Id = Me._objSequenceD.IdSequense).FirstOrDefault().Pattern, String.Empty)
                If Me._objSequenceD.GetType().GetProperties().Any(Function(p) p.Name.Equals("Prefix")) Then
                    Me._objSequenceD.Prefix = Me.TxtPrefix.Text
                End If
            ElseIf CType(Me.GlePrefixSequencePattern.Properties.DataSource, List(Of Domain.Entities.Sequense)) IsNot Nothing Then
                Me._objSequenceD.PatternName = If(Me._objSequenceD.IdSequense IsNot Nothing AndAlso Me._objSequenceD.IdSequense > 0, CType(Me.GlePrefixSequencePattern.Properties.DataSource, List(Of Domain.Entities.Sequense)).Where(Function(sq) sq.Id = Me._objSequenceD.IdSequense).FirstOrDefault().Pattern, String.Empty)
                If Me._objSequenceD.GetType().GetProperties().Any(Function(p) p.Name.Equals("Prefix")) Then
                    Me._objSequenceD.Prefix = Me.TxtPrefix.Text
                End If
            End If
            Me._objSequenceD.Next = Me.SpnNextNumber.EditValue
        End If
    End Sub

    Private Sub ValidateEnabled()
        If Me._objSequence IsNot Nothing And Me.GleForm.EditValue IsNot Nothing Then
            Me.LycRoot.BeginUpdate()
            Me.GleScope.Enabled = If(Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()), True, Not CBool(Me._objSequence.IsManual))
            Me.RbtnIsSequential.Enabled = If(Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()), True, Not CBool(Me._objSequence.IsManual))
            Me.PceAddSequenceDetail.Enabled = If(Me._listExceptionsInventory.Contains(Me.GleForm.EditValue.ToString()) OrElse Me._listExceptionsTreasury.Contains(Me.GleForm.EditValue.ToString()), True, Not CBool(Me._objSequence.IsManual))
            Me.SpnRate.Enabled = If(CBool(Me._objSequence.IsManual), False, Not CBool(Me._objSequence.Sequential))
            Me.LycRoot.EndUpdate()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(GdvSequenceDetails, ListActions)
        Me.ApllyStyleColActions()
    End Sub

    Private Sub ApllyStyleColActions()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GdvSequenceDetails.Columns
            If col.Name = "colActions" Then
                col.VisibleIndex = 5
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub GenerateAdditionalScopes()
        Me._additionalScopes = New Dictionary(Of String, List(Of Tuple(Of String, String)))
        Me._additionalScopeOptions = New Dictionary(Of Tuple(Of String, String), List(Of Tuple(Of Integer, String)))

        Me.AddAdditionalScopeForVoucherTransaction()
        Me.AddAdditionalScopeForInventoryContract()
    End Sub

    Private Function GetScopes() As List(Of Tuple(Of String, String))
        Dim scopes = New List(Of Tuple(Of String, String))
        If Me._additionalScopes.ContainsKey(Me.GleForm.EditValue) Then
            scopes = Me._additionalScopes(Me.GleForm.EditValue)
        End If
        Return scopes
    End Function

    Private Function GetListOptions() As List(Of Tuple(Of Integer, String))
        Dim scopeKey = New Tuple(Of String, String)(Me.GleForm.EditValue, Me.GleScope.EditValue)
        Dim scopeOptions = New List(Of Tuple(Of Integer, String))
        If Me._additionalScopeOptions.ContainsKey(scopeKey) Then
            scopeOptions = Me._additionalScopeOptions(scopeKey)
        End If
        Return scopeOptions
    End Function

#Region "Additional Scopes"

    Private Sub AddAdditionalScopeForVoucherTransaction()
        'Comprobantes de Egreso
        Dim idForm As String = "636"
        Dim scopes = New List(Of Tuple(Of String, String))

        'Ambito Adicional
        Dim scopeKey As String = "CC"
        Dim scope = New Tuple(Of String, String)(scopeKey, "Clase de Comprobante")
        scopes.Add(scope)

        'Opciones del Ambito Adicional
        Dim options = New List(Of Tuple(Of Integer, String)) From {New Tuple(Of Integer, String)(1, "Pago"), New Tuple(Of Integer, String)(2, "Reembolso"), New Tuple(Of Integer, String)(3, "Traslado")}

        'Agregamos el ambito adicional con sus opciones
        Me._additionalScopes.Add(idForm, scopes)
        Me._additionalScopeOptions.Add(New Tuple(Of String, String)(idForm, scopeKey), options)
    End Sub

    Private Sub AddAdditionalScopeForInventoryContract()
        'Comprobantes de Egreso
        Dim idForm As String = "316"
        Dim scopes = New List(Of Tuple(Of String, String))

        'Ambito Adicional
        Dim scopeKey As String = "TO"
        Dim scope = New Tuple(Of String, String)(scopeKey, "Tipo de Orden")
        scopes.Add(scope)

        'Opciones del Ambito Adicional
        Dim options = New List(Of Tuple(Of Integer, String)) From
        {
            New Tuple(Of Integer, String)(1, "Orden de Compra de Productos basada en un Contrato"),
            New Tuple(Of Integer, String)(2, "Orden de Compra de Productos NO basada en un Contrato"),
            New Tuple(Of Integer, String)(3, "Orden de Compra de Servicios basada en un Contrato"),
            New Tuple(Of Integer, String)(4, "Orden de Compra de Servicios NO basada en un Contrato")
        }

        'Agregamos el ambito adicional con sus opciones
        Me._additionalScopes.Add(idForm, scopes)
        Me._additionalScopeOptions.Add(New Tuple(Of String, String)(idForm, scopeKey), options)
    End Sub

#End Region

#End Region

#Region "ToolBar's Handlers"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag.ToString())
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Nuevo()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        Deshacer(True)
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

#End Region

End Class