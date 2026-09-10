'***********************************************************************
' Assembly         : Application.Security
' Author           : Jhon Tovar
' Created          : 10-03-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Security.Entities

Public Interface IMenuService
    Inherits IDisposable

#Region "ProductCatalog"
    ''' <summary>
    ''' Guardar o Actualizar ProductCatalog
    ''' </summary>
    ''' <param name="productCatalog"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveProductCatalog(ByVal productCatalog As ProductCatalog, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta un ProductCatalog
    ''' </summary>
    ''' <param name="IdProductCatalog">el codigo del rol.</param>
    ''' <returns></returns>
    Function GetProductCatalog(ByVal IdProductCatalog As Integer, companyCode As String) As ProductCatalog

    ''' <summary>
    ''' elimina el ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    Function DeleteProductCatalog(ByVal IdProductCatalog As Integer, companyCode As String) As Boolean

    ''' <summary>
    ''' Cambia estado el ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    Function ChangeStateProductCatalog(ByVal IdProductCatalog As Integer, ByVal state As Byte, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta lista de ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    Function ListProductCatalog() As List(Of ProductCatalog)
#End Region

#Region "Modules"
    ''' <summary>
    ''' Guardar o Actualizar modulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveModule(ByVal modules As Modules, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta un modulo
    ''' </summary>
    ''' <param name="IdModule">el codigo del rol.</param>
    ''' <returns></returns>
    Function GetModule(ByVal IdModule As Integer, companyCode As String) As Modules

    ''' <summary>
    ''' elimina el Module
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function DeleteModule(ByVal IdModule As Integer, companyCode As String) As Boolean

    ''' <summary>
    ''' Cambia estado el Module
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="state"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function ChangeStateModule(ByVal IdModule As Integer, ByVal state As Byte, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta toda lista de Module
    ''' </summary>
    ''' <returns></returns>
    Function ListAllModule() As List(Of Modules)
#End Region

#Region "Form"
    ''' <summary>
    ''' Guardar o Actualizar Formulario
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveForm(ByVal form As VieDBForm, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta un Formulario
    ''' </summary>
    ''' <param name="IdForm">el codigo del rol.</param>
    ''' <returns></returns>
    Function GetForm(ByVal IdForm As Integer, companyCode As String) As VieDBForm

    ''' <summary>
    ''' elimina el Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function DeleteForm(ByVal IdForm As Integer, companyCode As String) As Boolean

    ''' <summary>
    ''' Cambia estado el Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="state"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function ChangeStateForm(ByVal IdForm As Integer, ByVal state As Byte, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta lista de FormAction por Formulario
    ''' </summary>
    ''' <param name="IdForm">el codigo del rol.</param>
    ''' <returns></returns>
    Function ListFormActionByForm(ByVal IdForm As Integer, companyCode As String) As List(Of FormAction)

    ''' <summary>
    ''' Lista de formularios para usar en funcionalidad de importar
    ''' </summary>
    ''' <returns></returns>
    Function ListVieDBFormsImport(companyCode As String) As List(Of VieDBForm)

    ''' <summary>
    ''' Guardar importar
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveFormImport(ByVal form As List(Of VieDBForm), companyCode As String) As Boolean
#End Region

#Region "FrmTitle"

    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="title"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveTitle(ByVal title As Title, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta un Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function GetTitle(ByVal IdTitle As Integer, companyCode As String) As Title

    ''' <summary>
    ''' elimina el Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function DeleteTitle(ByVal IdTitle As Integer, companyCode As String) As Boolean

    ''' <summary>
    '''  Cambia estado del Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="state"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function ChangeStateTitle(ByVal IdTitle As Integer, ByVal state As Byte, companyCode As String) As Boolean

#End Region

End Interface
