Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Security.MVP

Public Class PopupModuleForms

#Region "Propiedades"
    ''' <summary>
    ''' Lista de formularios del modulo actual
    ''' </summary>
    Private _ListForms As Modules
    Public Property ListModuleForm As Modules
        Get
            Return _ListForms
        End Get
        Set(ByVal value As Modules)
            _ListForms = value
            _WorkListModuleForm = value.Clone()
        End Set
    End Property

    ''' <summary>
    ''' Lista de trabajo de formularios del modulo actual, se usa para modificar en memoria en el popup
    ''' </summary>
    Private _WorkListModuleForm As Modules
    Public Property WorkListModuleForm As Modules
        Get
            Return _WorkListModuleForm
        End Get
        Set(ByVal value As Modules)
            _WorkListModuleForm = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad hace el data source al control de Formulario
    ''' </summary>
    ''' <value></value>
    Public Property SleFormDataSource As DevExpress.Xpo.XPServerCollectionSource
        Get
            Return INDSleForm.Properties.DataSource
        End Get
        Set(ByVal value As DevExpress.Xpo.XPServerCollectionSource)
            INDSleForm.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id modulo
    ''' </summary>
    Private _IdModule As Integer
    Public Property IdModule() As Integer
        Get
            Return _IdModule
        End Get
        Set(ByVal value As Integer)
            _IdModule = value
        End Set
    End Property

    ''' <summary>
    ''' Id Titulo
    ''' </summary>
    Private _IdTitle As Integer
    Public Property IdTitle() As Integer
        Get
            Return _IdTitle
        End Get
        Set(ByVal value As Integer)
            _IdTitle = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#Region "Constructor"
    ''' <summary>
    ''' Inicia una instancia del formulario
    ''' </summary>
    Public Sub New()
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        IndigoGridControl1.SetHoldSize(INDGcForms, True)
    End Sub
#End Region


#Region "Eventos"
    ''' <summary>
    ''' Evento cuando se carga el control
    ''' </summary>
    Private Sub PopupModuleForms_Load() Handles Me.Load
        INDGvForms.OptionsView.ShowAutoFilterRow = False
        INDGvForms.ShowLoadingPanel()

        Task.Factory.StartNew(Sub()
                                  INDGcForms.SafeInvoke(Sub()
                                                            INDGcForms.DataSource = WorkListModuleForm.ModuleForm.Where(Function(x) x.IdTitle = IdTitle AndAlso x.Crud <> ECrud.Deleted).ToList()
                                                            INDGvForms.HideLoadingPanel()
                                                        End Sub)
                              End Sub)
    End Sub
#End Region


#Region "Procesos"
    ''' <summary>
    ''' Refresca la informacion de la rejilla Forms
    ''' </summary>
    Private Sub RefreshGridForms()
        INDGcForms.Invalidate()
        INDGcForms.DataSource = WorkListModuleForm.ModuleForm.Where(Function(x) x.IdTitle = IdTitle AndAlso x.Crud <> ECrud.Deleted).ToList()
        INDGcForms.RefreshDataSource()
    End Sub

#End Region

#Region "handles"

    ''' <summary>
    ''' Adiciona registro de form a gird
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddForm_Click(sender As Object, e As EventArgs) Handles INDSbAddForm.Click
        Dim _form As VieDBForm
        Dim _formSelect = TryCast(INDSleForm.GetSelectedDataRow(), Infrastructure.Data.Xpo.SecurityRepository.VieFormXpo)
        If _formSelect IsNot Nothing Then
            Dim TeOrder As Integer
            If String.IsNullOrEmpty(INDTeOrder.Text) Then
                Dim ListOrder = WorkListModuleForm.ModuleForm.Where(Function(x) x.IdTitle = IdTitle).ToList
                If ListOrder.Count > 0 Then
                    TeOrder = ListOrder.Max(Function(x) x.FormOrder) + 1
                Else
                    TeOrder = 1
                End If

            Else
                TeOrder = INDTeOrder.Text
            End If

            _form = New VieDBForm() With {.IdForm = _formSelect.Id, .FormName = _formSelect.Name, .FormOrder = TeOrder, .ClassName = _formSelect.ClassName, .AssemblyName = _formSelect.AssemblyName}
            Dim _AlterModuleForm = New ModuleForm() With {
            .IdModule = IdModule,
            .IdTitle = IdTitle,
            .IdForm = _form.IdForm,
            .FormOrder = _form.FormOrder,
            .Form = _form,
            .Crud = ECrud.Added
            }
            WorkListModuleForm.ModuleForm.Add(_AlterModuleForm)
        End If
        RefreshGridForms()
        INDSleForm.EditValue = Nothing
        INDTeOrder.Text = Nothing
    End Sub

    '' <summary>
    '' Evento para eliminar registro form en grid
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    Private Sub INDRibeDeleteForm_Click(sender As Object, e As EventArgs) Handles INDRibeDeleteForm.Click
        Dim _form = TryCast(INDGvForms.GetFocusedRow, ModuleForm)
        If _form IsNot Nothing Then
            If _form.IdForm > 0 Then
                Dim objDelete = WorkListModuleForm.ModuleForm.FirstOrDefault(Function(x) x.IdForm = _form.IdForm AndAlso x.IdTitle = IdTitle AndAlso (x.Crud = 0 Or x.Crud = ECrud.Unchanged))
                If objDelete IsNot Nothing Then
                    objDelete.Crud = ECrud.Deleted
                End If

                WorkListModuleForm.ModuleForm.RemoveAll(Function(x) x.IdForm = _form.IdForm AndAlso x.IdTitle = IdTitle AndAlso x.Crud = ECrud.Added)
            End If
            RefreshGridForms()
        End If
    End Sub

    ''' <summary>
    ''' Actualiza valor de orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRitxtUpdateForm_EditValueChanged(sender As Object, e As EventArgs) Handles INDRitxtOrderUpdateForm.EditValueChanged
        Dim _form = TryCast(INDGvForms.GetFocusedRow, ModuleForm)
        Dim value = DirectCast(sender, TextEdit)
        If _form IsNot Nothing AndAlso String.IsNullOrEmpty(value.Text) = False Then
            Dim objUpdate = WorkListModuleForm.ModuleForm.FirstOrDefault(Function(x) x.IdForm = _form.IdForm AndAlso x.IdTitle = IdTitle)
            If objUpdate IsNot Nothing Then
                If objUpdate.Crud = ECrud.Added Then
                    objUpdate.FormOrder = value.Text
                End If

                If (objUpdate.Crud = 0 Or objUpdate.Crud = ECrud.Unchanged Or objUpdate.Crud = ECrud.Modified) Then
                    objUpdate.FormOrder = value.Text
                    objUpdate.Crud = ECrud.Modified
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Cancela la edicion de permisos y cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbCancel_Click(sender As Object, e As EventArgs) Handles INDSbCancel.Click
        WorkListModuleForm = Nothing
        Me.Close()
    End Sub

    ''' <summary>
    '''Btn Aceptar cambios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAccept_Click(sender As Object, e As EventArgs) Handles INDSbAccept.Click
        ListModuleForm.ModuleForm = WorkListModuleForm.ModuleForm
        Me.Close()
    End Sub

#End Region


End Class