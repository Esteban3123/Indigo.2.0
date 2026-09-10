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
Public Interface IFormRepository

    ''' <summary>
    ''' Retorna lista de Formularios, modulos, productos y acciones.
    ''' </summary>
    ''' <returns></returns>
    Function ListFormModule() As List(Of VieForm)

    ''' <summary>
    ''' Guardar Formulario
    ''' </summary>
    ''' <param name="form"></param>
    ''' <returns></returns>
    Function SaveForm(ByVal form As VieDBForm) As Boolean

    ''' <summary>
    ''' Actualizar Formulario
    ''' </summary>
    ''' <param name="form"></param>
    ''' <returns></returns>
    Function UpdateForm(ByVal form As VieDBForm) As Boolean

    ''' <summary>
    ''' Consulta un Formulario
    ''' </summary>
    ''' <param name="IdForm">el codigo del rol.</param>
    ''' <returns></returns>
    Function GetForm(ByVal IdForm As Integer) As VieDBForm

    ''' <summary>
    ''' elimina el Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <returns></returns>
    Function DeleteForm(ByVal IdForm As Integer) As Boolean

    ''' <summary>
    ''' Cambia estado el Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Function ChangeStateForm(ByVal IdForm As Integer, ByVal state As Byte) As Boolean

    ''' <summary>
    ''' Lista de formularios para usar en funcionalidad de importar
    ''' </summary>
    ''' <returns></returns>
    Function ListVieDBFormsImport() As List(Of VieDBForm)

#Region "FormAction"

    ''' <summary>
    ''' Consulta lista de FormAction por Formulario
    ''' </summary>
    ''' <param name="IdForm">el codigo del rol.</param>
    ''' <returns></returns>
    Function ListFormActionByForm(ByVal IdForm As Integer) As List(Of FormAction)

    ''' <summary>
    ''' Guardar FormAction
    ''' </summary>
    ''' <param name="form"></param>
    ''' <returns></returns>
    Function SaveFormAction(ByVal form As FormAction) As Boolean

    ''' <summary>
    ''' Eliminar FormAction
    ''' </summary>
    ''' <param name="form"></param>
    ''' <returns></returns>
    Function DeleteFormAction(ByVal form As FormAction) As Boolean
#End Region


End Interface
