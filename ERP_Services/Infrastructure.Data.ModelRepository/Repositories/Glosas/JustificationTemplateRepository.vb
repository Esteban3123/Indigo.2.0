Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class JustificationTemplateRepository

    Inherits GenericRepository(Of JustificationTemplate)
    Implements IJustificationTemplateRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' consulta una plantilla de justificación especifica
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns>Objeto JustificationTemplate</returns>
    Public Function getJustificationTemplate(code As String, Optional tracking As Boolean = True) As JustificationTemplate Implements IJustificationTemplateRepository.getJustificationTemplate
        Dim JustificationTemplate = (From e In _context.JustificationTemplate.Include("ConceptGlosas")
                 Where e.Code = code
                 Select e).FirstOrDefault
        If JustificationTemplate IsNot Nothing Then
            JustificationTemplate.OriginalValue = (From e In _context.JustificationTemplate.AsNoTracking.Include("ConceptGlosas").AsNoTracking
             Where e.Code = code
             Select e).SingleOrDefault
            JustificationTemplate.ConceptGlosas.ConceptCodeName = JustificationTemplate.ConceptGlosas.Code + " - " + JustificationTemplate.ConceptGlosas.NameSpecific
            Return JustificationTemplate
        End If
        Return New JustificationTemplate
    End Function

    ''' <summary>
    ''' consulta una plantilla de justificación por código
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Public Function listJustificationTemplateByCode(code As String) As List(Of JustificationTemplate) Implements IJustificationTemplateRepository.listJustificationTemplateByCode
        Dim JustificationTemplate = From e In _context.JustificationTemplate.Include("ConceptGlosas")
             Where e.ConceptGlosas.Code = code
             Select e
        For Each item As JustificationTemplate In JustificationTemplate
            item.ConceptGlosas.ConceptCodeName = item.ConceptGlosas.Code + " - " + item.ConceptGlosas.NameSpecific
        Next
        Return JustificationTemplate.ToList
    End Function

    ''' <summary>
    ''' consulta una plantilla de justificación por concepto
    ''' </summary>
    ''' <param name="Concept">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Public Function listJustificationTemplateByConcept(Concept As String) As List(Of JustificationTemplate) Implements IJustificationTemplateRepository.listJustificationTemplateByConcept
        Dim JustificationTemplate = From e In _context.JustificationTemplate.Include("ConceptGlosas")
             Where e.IdConcept = Concept
             Select e
        For Each item As JustificationTemplate In JustificationTemplate
            item.ConceptGlosas.ConceptCodeName = item.ConceptGlosas.Code + " - " + item.ConceptGlosas.NameSpecific
            item.OriginalValue = (From e In _context.JustificationTemplate.AsNoTracking.Include("ConceptGlosas").AsNoTracking Where e.Id = item.Id).SingleOrDefault
        Next
        Return JustificationTemplate.ToList
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de justificación.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllJustificationTemplate() As List(Of JustificationTemplate) Implements IJustificationTemplateRepository.ListAllJustificationTemplate
        Dim Busqueda = From e In _context.JustificationTemplate.Include("ConceptGlosas")
                    Select e
        For Each item As JustificationTemplate In Busqueda
            item.ConceptGlosas.ConceptCodeName = item.ConceptGlosas.Code + " - " + item.ConceptGlosas.NameSpecific
        Next
        Return Busqueda.ToList
    End Function
End Class
