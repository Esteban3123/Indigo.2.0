'***********************************************************************
' Assembly         : Application.Security
' Author           : Jhon Tovar
' Created          : 09-03-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Application.Security
Imports Domain.Base.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class MenuService
    Implements IMenuService

    Private _productcatalogRepository As IProductCatalogRepository
    Private _moduleRepository As IModuleRepository
    Private _formRepository As IFormRepository
    Private _titleRepository As ITitleRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="MenuService" />.
    ''' </summary>
    ''' <param name="productcatalogRepository">el repositorio para el manejo de los productcatalogRepository.</param>
    ''' <param name="moduleRepository">el repositorio para el manejo de los moduleRepository.</param>
    ''' <param name="titleRepository">el repositorio para el manejo de de TitleRepository.</param>
    Public Sub New(ByVal productcatalogRepository As IProductCatalogRepository, ByVal moduleRepository As IModuleRepository, ByVal formRepository As IFormRepository, ByVal titleRepository As ITitleRepository)
        If productcatalogRepository Is Nothing Then
            Throw New ArgumentNullException("productcatalogRepository Vacio")
        End If
        If productcatalogRepository Is Nothing Then
            Throw New ArgumentNullException("menuRepository Vacio")
        End If

        _productcatalogRepository = productcatalogRepository
        _moduleRepository = moduleRepository
        _formRepository = formRepository
        _titleRepository = titleRepository
    End Sub

#Region "ProductCatalog"

    Public Function SaveProductCatalog(productCatalog As ProductCatalog, companyCode As String) As Boolean Implements IMenuService.SaveProductCatalog
        If productCatalog Is Nothing Then
            Throw New ArgumentNullException("productCatalog Vacio")
        End If

        Try
            If (productCatalog.Crud = ECrud.Modified) Then
                Return _productcatalogRepository.UpdateProductCatalog(productCatalog, companyCode)
            ElseIf (productCatalog.Crud = ECrud.Added) Then
                Return _productcatalogRepository.SaveProductCatalog(productCatalog, companyCode)
            Else
                Return False
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try

    End Function

    Public Function GetProductCatalog(IdProductCatalog As Integer, companyCode As String) As ProductCatalog Implements IMenuService.GetProductCatalog
        If IdProductCatalog.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _productcatalogRepository.GetProductCatalog(IdProductCatalog, companyCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function DeleteProductCatalog(IdProductCatalog As Integer, companyCode As String) As Boolean Implements IMenuService.DeleteProductCatalog
        If IdProductCatalog.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _productcatalogRepository.DeleteProductCatalog(IdProductCatalog, companyCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function ChangeStateProductCatalog(IdProductCatalog As Integer, state As Byte, companyCode As String) As Boolean Implements IMenuService.ChangeStateProductCatalog
        If IdProductCatalog.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _productcatalogRepository.ChangeStateProductCatalog(IdProductCatalog, state, companyCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function ListProductCatalog() As List(Of ProductCatalog) Implements IMenuService.ListProductCatalog
        Try
            Return _productcatalogRepository.ListProductCatalog()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "Module"

    ''' <summary>
    ''' Proceso de guardar o actualizar modelo Modules del frmModulos. procesa en tablas relacionadas
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function SaveModule(modules As Modules, companyCode As String) As Boolean Implements IMenuService.SaveModule
        If modules Is Nothing Then
            Throw New ArgumentNullException("modules Vacio")
        End If

        Try
            'Proceso de modulo
            If (modules.Crud = ECrud.Modified) Then
                _moduleRepository.UpdateModule(modules, companyCode)
            ElseIf (modules.Crud = ECrud.Added) Then
                _moduleRepository.SaveModule(modules, companyCode)
            End If
            'Proceso de ProductModule
            Dim existProductModule = _moduleRepository.GetProductModuleByModule(modules.IdModule).FirstOrDefault
            If (existProductModule?.IdModule > 0) Then
                _moduleRepository.UpdateProductModule(modules)
            Else
                _moduleRepository.SaveProductModule(modules)
            End If

            If modules.ModuleTitle.Any() Then
                'Consulta de registros para validar ModuleTitle que no existan en db
                Dim listGetModuleTitleByModule = _moduleRepository.ListAllModuleTitleByModule(modules.IdModule)
                'adicionar ModuleTitle
                For Each item As ModuleTitle In modules.ModuleTitle.Where(Function(x) x.Crud = ECrud.Added AndAlso listGetModuleTitleByModule.Any(Function(y) y.IdTitle = x.IdTitle) = False).ToList()
                    _moduleRepository.SaveModuleTitle(item)
                Next
                'Eliminar ModuleTitle
                For Each item As ModuleTitle In modules.ModuleTitle.Where(Function(x) x.Crud = ECrud.Deleted).ToList()
                    _moduleRepository.DeleteModuleTitle(item)
                Next
                'Actualizar ModuleTitle
                For Each item As ModuleTitle In modules.ModuleTitle.Where(Function(x) x.Crud = ECrud.Modified).ToList()
                    _moduleRepository.UpdateModuleTitle(item)
                Next
            End If

            If modules.ModuleForm.Any() Then
                'Consulta de registros para validar ModuleForm que no existan en db
                Dim listGetModuleFormByModule = _moduleRepository.ListAllModuleFormByModule(modules.IdModule)
                'adicionar ModuleForm
                For Each item As ModuleForm In modules.ModuleForm.Where(Function(x) x.Crud = ECrud.Added AndAlso listGetModuleFormByModule.Any(Function(y) y.IdForm = x.IdForm AndAlso x.IdTitle = y.IdTitle) = False).ToList()
                    _moduleRepository.SaveModuleForm(item)
                Next
                'adicionar ModuleForm
                For Each item As ModuleForm In modules.ModuleForm.Where(Function(x) x.Crud = ECrud.Deleted).ToList()
                    _moduleRepository.DeleteModuleForm(item)
                Next
                'Actualizar ModuleForm
                For Each item As ModuleForm In modules.ModuleForm.Where(Function(x) x.Crud = ECrud.Modified).ToList()
                    _moduleRepository.UpdateModuleForm(item)
                Next
            End If

            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try

    End Function

    Public Function GetModule(IdModule As Integer, companyCode As String) As Modules Implements IMenuService.GetModule
        If IdModule.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Dim Modules = New Modules()
            Modules = _moduleRepository.GetModule(IdModule, companyCode)
            Modules.ModuleTitle = _moduleRepository.ListModuleTitleByModule(IdModule)
            Modules.ModuleForm = _moduleRepository.ListModuleFormByModule(IdModule)
            Return Modules
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function DeleteModule(IdModule As Integer, companyCode As String) As Boolean Implements IMenuService.DeleteModule
        If IdModule.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _moduleRepository.DeleteModule(IdModule, companyCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function ChangeStateModule(IdModule As Integer, state As Byte, companyCode As String) As Boolean Implements IMenuService.ChangeStateModule
        If IdModule.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _moduleRepository.ChangeStateModule(IdModule, state, companyCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    Public Function ListAllModule() As List(Of Modules) Implements IMenuService.ListAllModule
        Try
            Return _moduleRepository.ListAllModule()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "Form"

    ''' <summary>
    ''' Proceso de guardar o actualizar modelo VieDBForm del frmFormulario. procesa en tablas relacionadas
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function SaveForm(form As VieDBForm, companyCode As String) As Boolean Implements IMenuService.SaveForm
        If form Is Nothing Then
            Throw New ArgumentNullException("modules Vacio")
        End If

        Try
            'Proceso de modulo
            If (form.Crud = ECrud.Modified) Then
                _formRepository.UpdateForm(form)
            ElseIf (form.Crud = ECrud.Added) Then
                _formRepository.SaveForm(form)
            End If

            If form.ListFormAction.Any() Then
                'adicionar ModuleTitle
                For Each item As FormAction In form.ListFormAction.Where(Function(x) x.Crud = ECrud.Added AndAlso x.Id = 0).ToList()
                    _formRepository.SaveFormAction(item)
                Next
                'Eliminar ModuleTitle
                For Each item As FormAction In form.ListFormAction.Where(Function(x) x.Crud = ECrud.Deleted).ToList()
                    _formRepository.DeleteFormAction(item)
                Next
            End If

            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Consultar formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function GetForm(IdForm As Integer, companyCode As String) As VieDBForm Implements IMenuService.GetForm
        If IdForm.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Dim form = New VieDBForm()
            form = _formRepository.GetForm(IdForm)
            form.ListFormAction = _formRepository.ListFormActionByForm(IdForm)
            Return form
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function DeleteForm(IdForm As Integer, companyCode As String) As Boolean Implements IMenuService.DeleteForm
        If IdForm.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _formRepository.DeleteForm(IdForm)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Cambiar estado formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="state"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function ChangeStateForm(IdForm As Integer, state As Byte, companyCode As String) As Boolean Implements IMenuService.ChangeStateForm
        If IdForm.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _formRepository.ChangeStateForm(IdForm, state)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Listado de form action por formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function ListFormActionByForm(IdForm As Integer, companyCode As String) As List(Of FormAction) Implements IMenuService.ListFormActionByForm
        If IdForm.Equals(0) Then
            Throw New ArgumentNullException("IdProductCatalog Vacio")
        End If

        Try
            Return _formRepository.ListFormActionByForm(IdForm)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Consualtar forms con acciones relacionadas
    ''' </summary>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function ListVieDBFormsImport(companyCode As String) As List(Of VieDBForm) Implements IMenuService.ListVieDBFormsImport
        Try
            Return _formRepository.ListVieDBFormsImport()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Proceso de guardar forms al importar
    ''' </summary>
    ''' <param name="forms"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function SaveFormImport(forms As List(Of VieDBForm), companyCode As String) As Boolean Implements IMenuService.SaveFormImport
        If forms Is Nothing Then
            Throw New ArgumentNullException("modules Vacio")
        End If

        Try
            'Consualtar forms actuales
            Dim ListForm As List(Of VieDBForm) = _formRepository.ListVieDBFormsImport()
            'validar forms a insertar con los actuales, para que no existan.
            For Each form As VieDBForm In forms.Where(Function(x) Not ListForm.Any(Function(y) x.IdForm = y.IdForm)).ToList
                Dim createdForm = _formRepository.SaveForm(form)
                If createdForm AndAlso form.ListFormAction.Any() Then
                    'Consulta de registros para validar FormAction que no existan en db
                    Dim listGetFormActionByForm As List(Of FormAction) = _formRepository.ListFormActionByForm(form.IdForm)
                    'adicionar ModuleTitle
                    For Each item As FormAction In form.ListFormAction.Where(Function(x) listGetFormActionByForm.Any(Function(y) x.IdAction = y.IdAction And y.Id = 0)).ToList()
                        _formRepository.SaveFormAction(item)
                    Next
                End If
            Next
            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try

    End Function

#End Region

#Region "Title"

    ''' <summary>
    ''' Proceso de guardar o actualizar modelo Title del formTitulo
    ''' </summary>
    ''' <param name="title"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function SaveTitle(title As Title, companyCode As String) As Boolean Implements IMenuService.SaveTitle
        If title Is Nothing Then
            Throw New ArgumentNullException("Title Vacio")
        End If

        Try
            'Proceso de titulo
            If (title.Crud = ECrud.Modified) Then
                _titleRepository.UpdateTitle(title)
            ElseIf (title.Crud = ECrud.Added) Then
                _titleRepository.SaveTitle(title)
            End If

            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Consultar Titulo
    ''' </summary>
    ''' <param name="idTitle"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function GetTitle(IdTitle As Integer, companyCode As String) As Title Implements IMenuService.GetTitle
        If IdTitle.Equals(0) Then
            Throw New ArgumentNullException("Title Vacio")
        End If

        Try
            Return _titleRepository.GetTitle(IdTitle)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Titulo
    ''' </summary>
    ''' <param name="idTitle"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function DeleteTitle(IdTitle As Integer, companyCode As String) As Boolean Implements IMenuService.DeleteTitle
        If IdTitle.Equals(0) Then
            Throw New ArgumentNullException("Title Vacio")
        End If

        Try
            Return _titleRepository.DeleteTitle(IdTitle)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Cambiar estado Titulo
    ''' </summary>
    ''' <param name="idTitle"></param>
    ''' <param name="state"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Public Function ChangeStateTitle(idTitle As Integer, state As Byte, companyCode As String) As Boolean Implements IMenuService.ChangeStateTitle
        If idTitle.Equals(0) Then
            Throw New ArgumentNullException("Title Vacio")
        End If

        Try
            Return _titleRepository.ChangeStateTitle(idTitle, state)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            _productcatalogRepository = Nothing
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
