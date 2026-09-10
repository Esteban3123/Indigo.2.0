'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IProcedureTemplateRepository
    Inherits IRepository(Of ProcedureTemplate)

    ''' <summary>
    ''' Obtiene una plantilla de procedimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProcedureTemplate(code As String) As ProcedureTemplate

    ''' <summary>
    ''' Obtiene una plantilla de procedimiento por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProcedureTemplateById(id As Integer) As ProcedureTemplate

    ''' <summary>
    ''' Guarda el listado en la BD
    ''' </summary>
    ''' <param name="ListProcedureCups"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveList(ListProcedureCups As List(Of ProcedureCups)) As List(Of ProcedureCups)

    ''' <summary>
    ''' Valida el CopyPaste del form de plantilla de procedimientos
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteProcedureTemplate(xmlObject As String) As List(Of SP_CopyAndPasteProcedureTemplate_Result)

End Interface
