'***********************************************************************
' Assembly         : Presentacion.Accounting|
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 19-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports System.Text

#End Region
Public Class FrmRecalculateBalances
    Implements IRecalculateBalances

#Region "Properties"
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IRecalculateBalances.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' contiene el id de la cuenta
    ''' </summary>
    ''' <returns></returns>
    Public Property idAcounting As Long Implements IRecalculateBalances.idAcounting
        Get
            Return INDSleMainAccount.EditValue
        End Get
        Set(value As Long)
            INDSleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el periodo del balance
    ''' </summary>
    ''' <returns></returns>
    Public Property Period As Short Implements IRecalculateBalances.Period
        Get
            Return INDGlePeriod.EditValue
        End Get
        Set(value As Short)
            INDGlePeriod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' especifica si se debe o no validar la informacion
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidateInformation As Boolean Implements IRecalculateBalances.ValidateInformation
        Get
            Return INDGleValidateMovement.EditValue
        End Get
        Set(value As Boolean)
            INDGleValidateMovement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
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
#End Region


    ''' <summary>
    ''' Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Abre el frontal de búsqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Se ejecuta al abrirse el formulario, carga los meses y limpia los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRecalculateBalances_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using Model As New MDocumentAccount(MyTag)
            INDSleLegalBook.Properties.DataSource = Model.ListBook()
        End Using
        LoadMonths()
        CleanControls()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
        INDGlePeriod.EditValue = Nothing
        INDSleLegalBook.EditValue = Nothing
        INDSleMainAccount.EditValue = Nothing
        INDSleMainAccount.Enabled = False
        INDGleValidateMovement.EditValue = True
        LayoutControl1.EndUpdate()
        'Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Confirmar) = False
    End Sub

    ''' <summary>
    ''' metodo para cargar el datasource
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadMonths()
        Using mSearch As New MCloseMonth(Me.Tag)
            Dim year As Integer
            year = INDCtrDateNavigator.GetYear
            INDGlePeriod.Properties.DataSource = Await mSearch.GetAllMonth(year, False)
            INDGlePeriod.EditValue = Nothing
        End Using
    End Sub

    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Confirmar) = False
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando el valor del control "Periodo" cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGlePeriod_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePeriod.EditValueChanged
        If INDGlePeriod.EditValue IsNot Nothing AndAlso INDGlePeriod.EditValue IsNot String.Empty Then
            If GetDateServer().Month = INDGlePeriod.EditValue Then
                If MessageIndigo.Show("Se suspendera el mes actual y no se podran generar comprobantes durante el proceso", MessageType.Question, "Procesar", Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    INDGlePeriod.EditValue = Nothing
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando el valor del control "Libro Oficial" cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleLegalBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleLegalBook.EditValueChanged
        If INDSleLegalBook.EditValue IsNot Nothing Then
            INDSleMainAccount.Enabled = True
        End If
        INDSleMainAccount.Properties.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Carga dinámicamente el datasource de "Cuenta Contable"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleMainAccount.QueryPopUp
        If INDSleMainAccount.Properties.DataSource Is Nothing Then
            Using model As New MAccoutingBalance(MyTag)
                INDSleMainAccount.Properties.DataSource = model.ListAccountsByLegalBookId(INDSleLegalBook.EditValue)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al dar click sobre el control/botón Deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Se ejecuta al dar click sobre el control/botón Confirmar y llama al método Process()
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Process()
    End Sub

    ''' <summary>
    ''' Valida los campos y realiza operaciones relacionadas con el proceso de cálculo de balances
    ''' </summary>
    Private Async Sub Process()
        Dim errors = ValidateFields()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        AsyncLoader(True)
        If MessageIndigo.Show("Esta seguro que desea generar el proceso", MessageType.Question, "Procesar", Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            AsyncLoader(False)
            Exit Sub

        End If
        Using model As New MAccoutingBalance(MyTag)
            Dim result = Await model.RecalculateBalance(Period, INDSleLegalBook.EditValue, INDSleMainAccount.EditValue, INDGleValidateMovement.EditValue, INDCtrDateNavigator.GetYear)
            If result.StatusCode = Domain.Base.Entities.eStatusResult.SUCCESS Then
                Mensaje(EeventViewerImages.Informacion) = result.Message
            ElseIf result.StatusCode = Domain.Base.Entities.eStatusResult.WARNING Then
                Using formulario As New FrmListErrors(result.ObjectEmbbeded)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    transparent.ShowDialog(Me)
                End Using
            Else
                Mensaje(EeventViewerImages.MensajeError) = result.Message
            End If
        End Using
        AsyncLoader(False)
        Deshacer()
    End Sub

    ''' <summary>
    ''' Valida los campos
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As String
        Dim errors As New StringBuilder()
        If INDGlePeriod.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un periodo")
        End If
        If INDSleLegalBook.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un libro oficial")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Se ejecuta al cambiar la fecha en el control INDCtrDateNavigator
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCtrDateNavigator_OnChangeDate(sender As Object, e As EventArgs) Handles INDCtrDateNavigator.OnChangeDate
        LoadMonths()
    End Sub
End Class