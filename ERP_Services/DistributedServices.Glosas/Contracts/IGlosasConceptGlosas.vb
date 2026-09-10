'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Diego A. Roldán
' Created          : 2022-04-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IGlosasConceptGlosas
    ''' <summary>
    ''' Obtiene un concepto de glosas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetConceptGlosasById(id As Integer, session As SessionValues) As ConceptGlosas

    ''' <summary>
    ''' Consulta por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetConceptGlosasByCode(code As String, session As SessionValues) As ConceptGlosas

    ''' <summary>
    ''' Guarda o actualiza un concepto de glosas
    ''' </summary>
    ''' <param name="conceptGlosa"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract>
    Function SaveConceptGlosas(conceptGlosa As ConceptGlosas, session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of ConceptGlosas)

    ''' <summary>
    ''' Elimina un concepto de glosas
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteConceptGlosas(conceptGlosa As ConceptGlosas, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    <OperationContract>
    Function ChangeConceptGlosas(code As String, state As Boolean, session As SessionValues) As ActionResult(Of ConceptGlosas)
End Interface
