Imports DevExpress.XtraEditors
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering.Helpers
Imports System.ComponentModel
Imports DevExpress.Data.Filtering

Public Class FrmConceptsExecuteFormulate

    Private txtResult As TextEdit
    Private _formulate As String
    Public listVariables As List(Of String)
    Public dictionaryTextEdit As Dictionary(Of String, TextEdit)

    Public Sub New(formulate As String, listVariable As List(Of String))
        listVariables = listVariable
        Me._formulate = formulate
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    ''' <summary>
    ''' creamos los controles dinamicamente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConceptsExecuteFormulate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dictionaryTextEdit = New Dictionary(Of String, TextEdit)()
        INDMemoEditFormulate.Text = _formulate
        For Each variable As String In listVariables
            'Creo el textbox
            Dim INDtxtTmp As New DevExpress.XtraEditors.TextEdit
            'INDtxtTmp.Location = New System.Drawing.Point(158, 60)
            INDtxtTmp.Name = "IND" & variable
            INDtxtTmp.Size = New System.Drawing.Size(400, 21)
            INDtxtTmp.TabIndex = 4
            Me.INDlyControlExecute.Controls.Add(INDtxtTmp)
            IndigoTextEdit1.SetCampoObligatorio(INDtxtTmp, True)
            IndigoTextEdit1.SetMascara(INDtxtTmp, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
            IndigoTextEdit1.EndInit()
            'Creo el layout control item
            Dim INDlyItemTmp As DevExpress.XtraLayout.LayoutControlItem = New DevExpress.XtraLayout.LayoutControlItem()
            INDlyItemTmp.Control = INDtxtTmp
            INDlyItemTmp.CustomizationFormText = variable
            'INDlyItemTmp.Location = New System.Drawing.Point(0, 0)
            INDlyItemTmp.MaxSize = New System.Drawing.Size(400, 36)
            INDlyItemTmp.MinSize = New System.Drawing.Size(400, 36)
            INDlyItemTmp.Name = "IND" & variable
            INDlyItemTmp.Size = New System.Drawing.Size(437, 278)
            INDlyItemTmp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            INDlyItemTmp.Text = variable.Replace("[", "").Replace("]", "")
            INDlyItemTmp.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
            INDlyItemTmp.TextSize = New System.Drawing.Size(165, 21)
            INDlyItemTmp.TextToControlDistance = 12
            INDlyGroupExecuteFormulate.AddItem(INDlyItemTmp)
            dictionaryTextEdit.Add(variable, INDtxtTmp)
        Next
        'Agrego la caja de texto donde muestro el resultado
        txtResult = New TextEdit()
        txtResult.Name = "INDResult"
        txtResult.Size = New System.Drawing.Size(400, 21)
        txtResult.TabIndex = 4
        txtResult.Enabled = False
        Me.INDlyControlExecute.Controls.Add(txtResult)
        IndigoTextEdit1.SetCampoObligatorio(txtResult, False)
        IndigoTextEdit1.SetMascara(txtResult, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        IndigoTextEdit1.EndInit()
        'Creo el layout control item
        Dim INDlyItemResultTmp As DevExpress.XtraLayout.LayoutControlItem = New DevExpress.XtraLayout.LayoutControlItem()
        INDlyItemResultTmp.Control = txtResult
        INDlyItemResultTmp.CustomizationFormText = "Resultado"
        'INDlyItemTmp.Location = New System.Drawing.Point(0, 0)
        INDlyItemResultTmp.MaxSize = New System.Drawing.Size(400, 36)
        INDlyItemResultTmp.MinSize = New System.Drawing.Size(400, 36)
        INDlyItemResultTmp.Name = "INDResult"
        INDlyItemResultTmp.Size = New System.Drawing.Size(437, 278)
        INDlyItemResultTmp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        INDlyItemResultTmp.Text = "Resultado"
        INDlyItemResultTmp.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        INDlyItemResultTmp.TextSize = New System.Drawing.Size(165, 21)
        INDlyItemResultTmp.TextToControlDistance = 12
        INDlyGroupExecuteFormulate.AddItem(INDlyItemResultTmp)

        Dim space As New DevExpress.XtraLayout.EmptySpaceItem()
        space.AllowHotTrack = False
        space.CustomizationFormText = "Espacio Formula"
        'space.Location = New System.Drawing.Point(0, 0)
        space.Name = "INDSpaceFormulate"
        space.Size = New System.Drawing.Size(538, 189)
        space.Text = "INDSpaceFormulate"
        space.TextSize = New System.Drawing.Size(0, 0)
        INDlyGroupExecuteFormulate.AddItem(space)

        Dim btnOk As New DevExpress.XtraEditors.SimpleButton()
        'btnOk.Location = New System.Drawing.Point(24, 60)
        btnOk.Name = "INDbtnOk"
        btnOk.Size = New System.Drawing.Size(534, 22)
        btnOk.StyleController = Me.INDlyControlExecute
        btnOk.TabIndex = 5
        btnOk.Text = "Ejecutar"
        AddHandler btnOk.Click, AddressOf click_aceptar
        Me.INDlyControlExecute.Controls.Add(btnOk)
        IndigoSimpleButton1.SetModernUiIndigo(btnOk, True)
        IndigoSimpleButton1.EndInit()

        Dim INDlyItemOk As DevExpress.XtraLayout.LayoutControlItem = New DevExpress.XtraLayout.LayoutControlItem()
        INDlyItemOk.Control = btnOk
        INDlyItemOk.CustomizationFormText = "Boton Aceptar"
        'INDlyItemOk.Location = New System.Drawing.Point(0, 0)
        INDlyItemOk.Name = "INDlyItemOk"
        INDlyItemOk.Size = New System.Drawing.Size(538, 215)
        INDlyItemOk.Text = "Ejecutar"
        INDlyItemOk.TextSize = New System.Drawing.Size(0, 0)
        INDlyItemOk.TextToControlDistance = 0
        INDlyItemOk.TextVisible = False
        INDlyGroupExecuteFormulate.AddItem(INDlyItemOk)

        Dim btnCancel As New DevExpress.XtraEditors.SimpleButton()
        'btnOk.Location = New System.Drawing.Point(24, 60)
        btnCancel.Name = "INDbtnCancel"
        btnCancel.Size = New System.Drawing.Size(534, 22)
        btnCancel.StyleController = Me.INDlyControlExecute
        btnCancel.TabIndex = 5
        btnCancel.Text = "Cancelar"
        AddHandler btnCancel.Click, AddressOf click_cancelar
        Me.INDlyControlExecute.Controls.Add(btnCancel)
        IndigoSimpleButton1.SetModernUiIndigo(btnCancel, True)
        IndigoSimpleButton1.EndInit()

        Dim INDlyItemCancel As DevExpress.XtraLayout.LayoutControlItem = New DevExpress.XtraLayout.LayoutControlItem()
        INDlyItemCancel.Control = btnCancel
        INDlyItemCancel.CustomizationFormText = "Boton Cancelar"
        'INDlyItemOk.Location = New System.Drawing.Point(0, 0)
        INDlyItemCancel.Name = "INDlyItemCancel"
        INDlyItemCancel.Size = New System.Drawing.Size(538, 215)
        INDlyItemCancel.Text = "Cancelar"
        INDlyItemCancel.TextSize = New System.Drawing.Size(0, 0)
        INDlyItemCancel.TextToControlDistance = 0
        INDlyItemCancel.TextVisible = False
        INDlyGroupExecuteFormulate.AddItem(INDlyItemCancel, INDlyItemOk, DevExpress.XtraLayout.Utils.InsertType.Right)
    End Sub

    ''' <summary>
    ''' Cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub click_cancelar(sender As Object, e As System.EventArgs)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Ejecuta la funcion de la formula
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub click_aceptar(sender As Object, e As System.EventArgs)
        'Valido que todos los campos esten llenos
        Dim mensajes As New StringBuilder()
        For Each variable As String In listVariables
            Dim textEdit = dictionaryTextEdit(variable)
            If textEdit.Text.Length = 0 Then
                If mensajes.Length > 0 Then
                    mensajes.Append("," & variable)
                Else
                    mensajes.Append(variable)
                End If
            End If
        Next
        If mensajes.Length > 0 Then
            MessageIndigo.Show(String.Format(ResourceManager.GetString("InvalidFields"), " " & mensajes.ToString()), MessageType.Warning, Me.Text)
            Return
        End If
        Try
            Dim formulateTmp = _formulate
            For Each variable As String In listVariables
                formulateTmp = formulateTmp.Replace(variable, dictionaryTextEdit(variable).Text.Replace(",", "."))
            Next
            Dim expression = New ExpressionEvaluator(TypeDescriptor.GetProperties(GetType(String)), CriteriaOperator.Parse(formulateTmp)).Evaluate(formulateTmp)
            txtResult.Text = expression
        Catch ex As Exception
            MessageIndigo.Show(ex.Message, MessageType.Warning, Me.Text)
        End Try
    End Sub

End Class