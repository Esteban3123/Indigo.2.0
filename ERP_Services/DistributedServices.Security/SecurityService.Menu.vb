'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : Jhon Tovar
' Created          : 09-03-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
#End Region


Partial Public Class SecurityService

#Region "ProductCatalog"
    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="productCatalog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveProductCatalog(productCatalog As ProductCatalog, session As SessionValues) As Boolean Implements ISecurityService.SaveProductCatalog
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.SaveProductCatalog(productCatalog, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <param name="IdProductCatalog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function DeleteProductCatalog(IdProductCatalog As Integer, session As SessionValues) As Boolean Implements ISecurityService.DeleteProductCatalog
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.DeleteProductCatalog(IdProductCatalog, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar product catalog
    ''' </summary>
    ''' <param name="IdProductCatalog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetProductCatalog(IdProductCatalog As Integer, session As SessionValues) As ProductCatalog Implements ISecurityService.GetProductCatalog
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.GetProductCatalog(IdProductCatalog, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' cambiar estado product catalog
    ''' </summary>
    ''' <param name="IdProductCatalog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ChangeStateProductCatalog(ByVal IdProductCatalog As Integer, ByVal state As Byte, session As SessionValues) As Boolean Implements ISecurityService.ChangeStateProductCatalog
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ChangeStateProductCatalog(IdProductCatalog, state, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar listado de producto
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListProductCatalog(session As SessionValues) As List(Of ProductCatalog) Implements ISecurityService.ListProductCatalog
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ListProductCatalog()
        End Using
    End Function

#End Region

#Region "Modulos"
    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveModule(modules As Modules, session As SessionValues) As Boolean Implements ISecurityService.SaveModule
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.SaveModule(modules, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function DeleteModule(IdModule As Integer, session As SessionValues) As Boolean Implements ISecurityService.DeleteModule
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.DeleteModule(IdModule, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar modulo
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetModule(IdModule As Integer, session As SessionValues) As Modules Implements ISecurityService.GetModule
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.GetModule(IdModule, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' cambiar estado
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ChangeStateModule(ByVal IdModule As Integer, ByVal state As Byte, session As SessionValues) As Boolean Implements ISecurityService.ChangeStateModule
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ChangeStateModule(IdModule, state, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar listado de producto
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListAllModule(session As SessionValues) As List(Of Modules) Implements ISecurityService.ListAllModule
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ListAllModule()
        End Using
    End Function

#End Region

#Region "Formularios"
    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveForm(form As VieDBForm, session As SessionValues) As Boolean Implements ISecurityService.SaveForm
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.SaveForm(form, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function DeleteForm(IdForm As Integer, session As SessionValues) As Boolean Implements ISecurityService.DeleteForm
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.DeleteForm(IdForm, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar modulo
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetForm(IdForm As Integer, session As SessionValues) As VieDBForm Implements ISecurityService.GetForm
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.GetForm(IdForm, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' cambiar estado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ChangeStateForm(ByVal IdForm As Integer, ByVal state As Byte, session As SessionValues) As Boolean Implements ISecurityService.ChangeStateForm
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ChangeStateForm(IdForm, state, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consulta lista de FormAction por Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListFormActionByForm(ByVal IdForm As Integer, session As SessionValues) As List(Of FormAction) Implements ISecurityService.ListFormActionByForm
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ListFormActionByForm(IdForm, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar Lista de formularios para usar en funcionalidad de importar
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListVieDBFormsImport(session As SessionValues) As List(Of VieDBForm) Implements ISecurityService.ListVieDBFormsImport
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ListVieDBFormsImport(session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Guardar Importar
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveFormImport(form As List(Of VieDBForm), session As SessionValues) As Boolean Implements ISecurityService.SaveFormImport
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.SaveFormImport(form, session.IndigoCompany)
        End Using
    End Function

#End Region

#Region "Titulos"
    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="title"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveTitle(title As Title, session As SessionValues) As Boolean Implements ISecurityService.SaveTitle
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.SaveTitle(title, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function DeleteTitle(IdTitle As Integer, session As SessionValues) As Boolean Implements ISecurityService.DeleteTitle
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.DeleteTitle(IdTitle, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Consultar Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetTitle(IdTitle As Integer, session As SessionValues) As Title Implements ISecurityService.GetTitle
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.GetTitle(IdTitle, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' cambiar estado
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ChangeStateTitle(ByVal IdTitle As Integer, ByVal state As Byte, session As SessionValues) As Boolean Implements ISecurityService.ChangeStateTitle
        Using loginAdmin As IMenuService = IocFactory.Instance().CurrentContainer.Resolve(Of IMenuService)()
            Return loginAdmin.ChangeStateTitle(IdTitle, state, session.IndigoCompany)
        End Using
    End Function

#End Region

End Class
