#Region "Imports"

Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Security.MVP
Imports Presentation.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Controls

#End Region

Public Class FrmSequencePattern
    Implements ISequencePattern

#Region "Fields"

    Private _myModel As MSequence
    Private _fromStatusControls As Boolean
    Private _objSequence As Domain.Entities.Sequense
    Private _formSearchObjects As FrmBusqueda

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

    Public Property Pattern As String Implements ISequencePattern.Pattern

    Public Property PatternName As String Implements ISequencePattern.PatternName

#End Region

#Region "Builders"

    Public Sub New()
        InitializeComponent()
        Me._fromStatusControls = False
        Me._myModel = New MSequence()
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se prepara el formulario
    ''' </summary> 
    Private Sub FrmSequencePattern_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Aqui se calcula el patrón de ejmplo
    ''' </summary>
    Private Sub PcePattern_TextChanged(sender As Object, e As EventArgs) Handles PcePattern.TextChanged
        Me.LyciPattern.Text = String.Format(ResourceManager.GetString("LabelPattern"), Me.PcePattern.Text.Trim().Length)
        If Not Me.PcePattern.Text.Trim().Equals(String.Empty) AndAlso Segment.GetMax(Me.PcePattern.Text.Trim()) > 0 Then
            Me.LblMinValue.Text = "1 -> " & Sequense.GetSequense(Me.PcePattern.Text.Trim(), 1)
            Me.LblMaxValue.Text = Segment.GetMax(Me.PcePattern.Text.Trim()).ToString() & " -> " & Sequense.GetSequense(Me.PcePattern.Text.Trim(), Segment.GetMax(Me.PcePattern.Text.Trim()))
        Else
            Me.LblMinValue.Text = String.Empty
            Me.LblMaxValue.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla la inserción de caracteres en el campo del patrón
    ''' </summary>
    Private Sub Btn_Click(sender As Object, e As EventArgs) Handles Btn1.Click, BtnZ.Click, BtnY.Click, BtnX.Click, BtnW.Click, BtnV.Click, BtnU.Click, BtnT.Click, BtnS.Click, BtnRYB.Click, BtnRY.Click, BtnR.Click, BtnQ.Click, BtnPU.Click, BtnPI.Click, BtnPD.Click, BtnP.Click, BtnO.Click, BtnN.Click, BtnM.Click, BtnL.Click, BtnK.Click, BtnJ.Click, BtnI.Click, BtnH.Click, BtnG.Click, BtnF.Click, BtnEN.Click, BtnE.Click, BtnD.Click, BtnC.Click, BtnB.Click, BtnA.Click, Btn9.Click, Btn8.Click, Btn7.Click, Btn6.Click, Btn5.Click, Btn4.Click, Btn3.Click, Btn2.Click, Btn0.Click
        Me.PcePattern.Text &= sender.Text
    End Sub

    ''' <summary>
    ''' Aqui se borra el último carácter
    ''' </summary>
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        If Not Me.PcePattern.Text.Trim().Equals(String.Empty) Then
            If Me.PcePattern.Text.Trim().Length >= 2 Then
                Me.PcePattern.Text = If(Me.PcePattern.Text.Substring(Me.PcePattern.Text.Length - 2).Contains("%"), Me.PcePattern.Text.Substring(0, Me.PcePattern.Text.Length - 2), Me.PcePattern.Text.Substring(0, Me.PcePattern.Text.Length - 1))
            Else
                Me.PcePattern.Text = Me.PcePattern.Text.Substring(0, Me.PcePattern.Text.Length - 1)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Activa la escritura de letras en mayúscula
    ''' </summary>
    Private Sub BtnMayus_CheckedChanged(sender As Object, e As EventArgs) Handles BtnMayus.CheckedChanged
        Me.BtnA.Text = If(Me.BtnMayus.Checked, Me.BtnA.Text.ToUpper(), Me.BtnA.Text.ToLower())
        Me.BtnB.Text = If(Me.BtnMayus.Checked, Me.BtnB.Text.ToUpper(), Me.BtnB.Text.ToLower())
        Me.BtnC.Text = If(Me.BtnMayus.Checked, Me.BtnC.Text.ToUpper(), Me.BtnC.Text.ToLower())
        Me.BtnD.Text = If(Me.BtnMayus.Checked, Me.BtnD.Text.ToUpper(), Me.BtnD.Text.ToLower())
        Me.BtnE.Text = If(Me.BtnMayus.Checked, Me.BtnE.Text.ToUpper(), Me.BtnE.Text.ToLower())
        Me.BtnF.Text = If(Me.BtnMayus.Checked, Me.BtnF.Text.ToUpper(), Me.BtnF.Text.ToLower())
        Me.BtnG.Text = If(Me.BtnMayus.Checked, Me.BtnG.Text.ToUpper(), Me.BtnG.Text.ToLower())
        Me.BtnH.Text = If(Me.BtnMayus.Checked, Me.BtnH.Text.ToUpper(), Me.BtnH.Text.ToLower())
        Me.BtnI.Text = If(Me.BtnMayus.Checked, Me.BtnI.Text.ToUpper(), Me.BtnI.Text.ToLower())
        Me.BtnJ.Text = If(Me.BtnMayus.Checked, Me.BtnJ.Text.ToUpper(), Me.BtnJ.Text.ToLower())
        Me.BtnK.Text = If(Me.BtnMayus.Checked, Me.BtnK.Text.ToUpper(), Me.BtnK.Text.ToLower())
        Me.BtnL.Text = If(Me.BtnMayus.Checked, Me.BtnL.Text.ToUpper(), Me.BtnL.Text.ToLower())
        Me.BtnM.Text = If(Me.BtnMayus.Checked, Me.BtnM.Text.ToUpper(), Me.BtnM.Text.ToLower())
        Me.BtnN.Text = If(Me.BtnMayus.Checked, Me.BtnN.Text.ToUpper(), Me.BtnN.Text.ToLower())
        Me.BtnEN.Text = If(Me.BtnMayus.Checked, Me.BtnEN.Text.ToUpper(), Me.BtnEN.Text.ToLower())
        Me.BtnO.Text = If(Me.BtnMayus.Checked, Me.BtnO.Text.ToUpper(), Me.BtnO.Text.ToLower())
        Me.BtnP.Text = If(Me.BtnMayus.Checked, Me.BtnP.Text.ToUpper(), Me.BtnP.Text.ToLower())
        Me.BtnQ.Text = If(Me.BtnMayus.Checked, Me.BtnQ.Text.ToUpper(), Me.BtnQ.Text.ToLower())
        Me.BtnR.Text = If(Me.BtnMayus.Checked, Me.BtnR.Text.ToUpper(), Me.BtnR.Text.ToLower())
        Me.BtnS.Text = If(Me.BtnMayus.Checked, Me.BtnS.Text.ToUpper(), Me.BtnS.Text.ToLower())
        Me.BtnT.Text = If(Me.BtnMayus.Checked, Me.BtnT.Text.ToUpper(), Me.BtnT.Text.ToLower())
        Me.BtnU.Text = If(Me.BtnMayus.Checked, Me.BtnU.Text.ToUpper(), Me.BtnU.Text.ToLower())
        Me.BtnV.Text = If(Me.BtnMayus.Checked, Me.BtnV.Text.ToUpper(), Me.BtnV.Text.ToLower())
        Me.BtnW.Text = If(Me.BtnMayus.Checked, Me.BtnW.Text.ToUpper(), Me.BtnW.Text.ToLower())
        Me.BtnX.Text = If(Me.BtnMayus.Checked, Me.BtnX.Text.ToUpper(), Me.BtnX.Text.ToLower())
        Me.BtnY.Text = If(Me.BtnMayus.Checked, Me.BtnY.Text.ToUpper(), Me.BtnY.Text.ToLower())
        Me.BtnZ.Text = If(Me.BtnMayus.Checked, Me.BtnZ.Text.ToUpper(), Me.BtnZ.Text.ToLower())
    End Sub

    ''' <summary>
    ''' Inserta un número calculado
    ''' </summary>
    Private Sub BtnNUM_Click(sender As Object, e As EventArgs) Handles BtnNUM.Click
        Me.PcePattern.Text &= "#"
    End Sub

    ''' <summary>
    ''' Inserta una letra calculada
    ''' </summary>
    Private Sub BtnLETT_Click(sender As Object, e As EventArgs) Handles BtnLETT.Click
        Me.PcePattern.Text &= "&"
    End Sub

    ''' <summary>
    ''' Borra todo el patrón
    ''' </summary>
    Private Sub BtnSUPR_Click(sender As Object, e As EventArgs) Handles BtnSUPR.Click
        Me.PcePattern.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Inserta una variable prefijo
    ''' </summary>
    Private Sub BtnPrefix_Click(sender As Object, e As EventArgs) Handles BtnPrefix.Click
        Me.PcePattern.Text &= "%pfx"
    End Sub

    ''' <summary>
    ''' Inserta variable que indica el dia de la fecha actual
    ''' </summary>
    Private Sub BtnDAY_Click(sender As Object, e As EventArgs) Handles BtnDAY.Click
        Me.PcePattern.Text &= "%d"
    End Sub

    ''' <summary>
    ''' Inserta variable que indica el mes de la fecha actual
    ''' </summary>
    Private Sub BtnMONTH_Click(sender As Object, e As EventArgs) Handles BtnMONTH.Click
        Me.PcePattern.Text &= "%M"
    End Sub

    ''' <summary>
    ''' Inserta variable que indica el año de la fecha actual
    ''' </summary>
    Private Sub BtnYEAR_Click(sender As Object, e As EventArgs) Handles BtnYEAR.Click
        Me.PcePattern.Text &= "%y"
    End Sub

    ''' <summary>
    ''' Inserta variable que indica la hora de la fecha actual
    ''' </summary>
    Private Sub BtnHOUR_Click(sender As Object, e As EventArgs) Handles BtnHOUR.Click
        Me.PcePattern.Text &= "%h"
    End Sub

    ''' <summary>
    ''' Inserta variable que indica el minuto de la fecha actual
    ''' </summary>
    Private Sub BtnMINUTE_Click(sender As Object, e As EventArgs) Handles BtnMINUTE.Click
        Me.PcePattern.Text &= "%m"
    End Sub

    ''' <summary>
    ''' Inserta variable que indica el segundo de la fecha actual
    ''' </summary>
    Private Sub BtnSECOUND_Click(sender As Object, e As EventArgs) Handles BtnSECOUND.Click
        Me.PcePattern.Text &= "%s"
    End Sub

    Private Sub BteName_KeyDown(sender As Object, e As KeyEventArgs) Handles BteName.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not Me.BteName.Text.Trim().Equals(String.Empty) Then
            Me.LoadControls(Me.BteName.Text.Trim())
        End If
    End Sub

    Private Sub BteName_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BteName.ButtonClick
        Me.OpenSearch()
    End Sub

#End Region

#Region "Methods"

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Me.Deshacer()
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.ClearControls()
        Me.SetStatusControls()
        Me.BteName.Focus()
    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Me.LblMaxValue.Text.Trim().Equals(String.Empty) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El patrón no es válido, pues no genera ningúna secuencia númerica"
            Exit Sub
        End If
        If Me.LblMaxValue.Text.Trim().Equals(String.Empty) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El patrón no es válido, pues no genera ningúna secuencia númerica"
            Exit Sub
        End If
        Me._objSequence.Name = Me.BteName.Text.Trim()
        Me._objSequence.Pattern = Me.PcePattern.Text.Trim()
        Me.AsyncLoader(True)
        Dim res = Await Me._myModel.SavePatternSequence(Me._objSequence)
        If res IsNot Nothing AndAlso res.StateResult Then
            Me.Mensaje(EeventViewerImages.Informacion) = "El patrón se guardo con exito"
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = "El patrón no se ha podido guardar"
        End If
        Me.AsyncLoader(False)
        Me.Deshacer()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        _formSearchObjects = New FrmBusqueda
        AddHandler _formSearchObjects.ReturnValue, AddressOf ReturnValue
        With _formSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 100}, _
                              New ColumnInfo With {.Caption = "Patrón", .FieldName = "Pattern", .ColumnWidth = 300}}.ToList()
            .ValorSolicitado = "Name"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListSequencePatterns
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        If ReturnValue <> String.Empty Then
            Me.LoadControls(ReturnValue)
        End If
    End Sub

    Private Async Sub LoadControls(ByVal codeName As String)
        Me.AsyncLoader(True)
        Dim res = Await Me._myModel.GetPatternSequenceByName(codeName)
        Me._objSequence = res
        Me.AsyncLoader(False)
        If res Is Nothing OrElse res.Id = 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El patrón no existe, pero si desea puede crearlo"
            Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlySave)
        Else
            Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUpdate)
            Me.BteName.Text = Me._objSequence.Name
            Me.PcePattern.Text = Me._objSequence.Pattern
        End If
        Me.SetStatusControls(2)
    End Sub

    ''' <summary>
    ''' Limpia los controles en pantalla
    ''' </summary>
    Private Sub ClearControls()
        Me.BteName.EditValue = Nothing
        Me.PcePattern.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Asigna el estado a los controles correspondiente
    ''' al nivel pasado por parametro
    ''' </summary>
    ''' <param name="level">Nivel a asignar</param>
    Private Sub SetStatusControls(Optional ByVal level As Byte = 0)
        Me._fromStatusControls = True
        Me.BteName.Enabled = False
        Me.PcePattern.Enabled = False
        Select Case level
            Case 1
                Me.BteName.Enabled = True
            Case 2
                Me.PcePattern.Enabled = True
            Case Else
                Me.BteName.Enabled = True
        End Select
        Me._fromStatusControls = False
    End Sub

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

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

#End Region

End Class