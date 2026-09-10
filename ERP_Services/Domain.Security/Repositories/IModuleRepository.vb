'***********************************************************************
' Assembly         : Domain.Security
' Author           : Jhon Tovar
' Created          : 25-02-2022
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Security.Entities
#End Region


''' <summary>
''' Metodos y funciones necesarias para el manejo de usuarios
''' </summary>
Public Interface IModuleRepository

#Region "Module"
    ''' <summary>
    ''' Retorna lista de Formularios, modulos, productos y acciones.
    ''' </summary>
    ''' <returns></returns>
    Function ListVieModule() As List(Of VieModule)

    ''' <summary>
    ''' Guardar modulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveModule(ByVal modules As Modules, companyCode As String) As Boolean

    ''' <summary>
    ''' Actualizar modulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function UpdateModule(ByVal modules As Modules, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta un Modules
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
    ''' Consulta toda la lista de Module
    ''' </summary>
    ''' <returns></returns>
    Function ListAllModule() As List(Of Modules)
#End Region

#Region "ProductModule"
    ''' <summary>
    ''' Consulta ProductModule por modulo
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <returns></returns>
    Function GetProductModuleByModule(IdModule As Integer) As List(Of ProductModule)

    ''' <summary>
    ''' Actualizar Producto modulo por modulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function UpdateProductModule(ByVal modules As Modules) As Boolean

    ''' <summary>
    ''' Guardar Producto modulo por modulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function SaveProductModule(ByVal modules As Modules) As Boolean
#End Region

#Region "ModuleTitle"

    ''' <summary>
    ''' Consulta listado de titulos por modulo
    ''' </summary>
    ''' <returns></returns>
    Function ListModuleTitleByModule(IdModule As Integer) As List(Of ModuleTitle)

    ''' <summary>
    ''' Consulta todos los titulos por modulo
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <returns></returns>
    Function ListAllModuleTitleByModule(ByVal IdModule As Integer) As List(Of ModuleTitle)

    ''' <summary>
    ''' Guardar modulo titulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function SaveModuleTitle(ByVal modules As ModuleTitle) As Boolean

    ''' <summary>
    ''' Elimina modulo titulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function DeleteModuleTitle(ByVal modules As ModuleTitle) As Boolean

    ''' <summary>
    ''' Actualiza modulo titulo
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function UpdateModuleTitle(ByVal modules As ModuleTitle) As Boolean

#End Region

#Region "ModuleForm"
    ''' <summary>
    ''' Consultar listado de formulario por modulo y titulo
    ''' </summary>
    ''' <returns></returns>
    Function ListModuleFormByModule(IdModule As Integer) As List(Of ModuleForm)

    ''' <summary>
    ''' Consulta todos los formularios por modulo
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <returns></returns>
    Function ListAllModuleFormByModule(ByVal IdModule As Integer) As List(Of ModuleForm)

    ''' <summary>
    ''' Guardar modulo Formulario
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function SaveModuleForm(ByVal modules As ModuleForm) As Boolean

    ''' <summary>
    ''' Elimina modulo Formulario
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function DeleteModuleForm(ByVal modules As ModuleForm) As Boolean

    ''' <summary>
    ''' Actualiza modulo Formulario
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <returns></returns>
    Function UpdateModuleForm(ByVal modules As ModuleForm) As Boolean
#End Region


End Interface
