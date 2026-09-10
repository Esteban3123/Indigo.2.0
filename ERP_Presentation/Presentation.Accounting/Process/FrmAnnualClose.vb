#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Accounting.MVP
Imports Domain.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FrmAnnualClose
    Implements IAnnualClose

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IAnnualClose.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' contiene el id del libro contable
    ''' </summary>
    ''' <returns></returns>
    Public Property LegalBookId As Integer Implements IAnnualClose.LegalBookId
        Get
            Return INDSleLegalBook.EditValue
        End Get
        Set(value As Integer)
            INDSleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el año a cerrar
    ''' </summary>
    ''' <returns></returns>
    Public Property Year As Integer Implements IAnnualClose.Year
        Get
            Return INDCtrDateNavigator.GetYear
        End Get
        Set(value As Integer)
            INDCtrDateNavigator.SetYear = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona una forma de mostrar mensajes con diferentes estilos e iconos en función del tipo de mensaje proporcionado a través del parámetro Icono
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

#Region "Fields"
    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Accounting"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private OperativeUnitId As Integer?
    ''' <summary>
    ''' Fecha actual
    ''' </summary>
    Dim dateNow As Date
#End Region

#Region "Icrud"

    ''' <summary>
    ''' Item Nuevo del control de usuarios
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Método que abre el frontal de búsqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Item buscar del control de usuarios
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Item Deshacer del control de usuarios
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método para guardar relacionado al proceso de cierre anual fiscal y manejar los mensajes correspondientes según el resultado del proceso
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Dim errors = ValidateFields()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Try
            AsyncLoader(True)
            If MessageIndigo.Show("Esta seguro que desea generar el proceso", MessageType.Question, "Procesar", Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                AsyncLoader(False)
                Exit Sub
            End If
            Using model As New MAnnualClose(MyTag)
                Dim result = Await model.FiscalYearClose(LegalBookId, Year, OperativeUnitId)
                If result.StatusCode = Domain.Base.Entities.eStatusResult.SUCCESS Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                ElseIf result.StatusCode = Domain.Base.Entities.eStatusResult.WARNING Then
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count() > 1 Then
                        Using formulario As New FrmListErrors(result.ObjectEmbbeded)
                            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            transparent.ShowDialog(Me)
                        End Using
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                End If
            End Using
            AsyncLoader(False)
            Deshacer()
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me.OperativeUnitId = operatingUnit.Id
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se ejecuta cuando se abre el frm "AnnualClose", configura los botones y carga los datos iniciales.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAnnualClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear
        BarraBotones.Minimizar(True)
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Copiar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Pegar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cortar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        BarraBotones.RibbonPagEform.Visible = False

        OperativeUnitId = BarraBotones.OperatingUnitValue
        dateNow = Me.GetDateServer()
        LoadLegalBooks()
        CleanControls()
    End Sub

#End Region

#Region "Disposed"

    ''' <summary>
    ''' Evento que se ejecuta cuando el formulario está siendo eliminado, reinicia las variables y demás objetos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
        OperativeUnitId = Nothing
        dateNow = Nothing
    End Sub

#End Region

#Region "EditValuedChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia de valor en el control de "Libro Oficial"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleLegalBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleLegalBook.EditValueChanged
        Year = dateNow.Year
        If Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
            Dim legalBook = If(SearchLookUpEdit1View.DataSource IsNot Nothing, DirectCast(DirectCast(INDSleLegalBook.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.BookXpo), New Infrastructure.Data.Xpo.AccountingRepository.BookXpo)
            Year = legalBook.LastYearClose + 1
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que llama al método "Guardar" cuando se presiona el btn "Procesar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnProcess_Click(sender As Object, e As EventArgs) Handles BtnProcess.Click
        Guardar()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que trae los datos de libro oficial
    ''' </summary>
    Private Sub LoadLegalBooks()
        Using Model As New MDocumentAccount(MyTag)
            INDSleLegalBook.Properties.DataSource = Nothing
            INDSleLegalBook.Properties.DataSource = Model.ListBookByStatus()
        End Using
    End Sub

    ''' <summary>
    ''' Método que limpia los controles 
    ''' </summary>
    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
        INDSleLegalBook.EditValue = Nothing
        INDCtrDateNavigator.SetYear = dateNow.Year
        LayoutControl1.EndUpdate()
        LoadLegalBooks()
    End Sub

    ''' <summary>
    ''' Método que valida los campos de unidad operativa y libro oficial
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As String
        Dim errors As New StringBuilder()

        If OperativeUnitId Is Nothing Then
            errors.AppendLine("Seleccione una unidad operativa.")
        End If
        If INDSleLegalBook.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un libro oficial")
        End If

        Return errors.ToString()
    End Function

#End Region

End Class