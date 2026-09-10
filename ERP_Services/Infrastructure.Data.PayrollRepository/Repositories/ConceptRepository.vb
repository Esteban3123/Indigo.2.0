'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class ConceptRepository
    Inherits GenericRepository(Of Concept)
    Implements IConceptRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un Concepto
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    Public Function GetConcept(code As String, Optional tracking As Boolean = True) As Concept Implements IConceptRepository.GetConcept

        Dim Concept As IQueryable(Of Concept)
        Concept = From e In _context.Concept
        Where e.Code = code
            Select e
        If tracking = True Then
            'Concept = From e In _context.Concept.Include("ConceptGroup").Include("ConceptGroup.Group").Include("ConceptAccountingStructure").Include("ConceptAccountingStructure.AccountingStructure")
            '              Where e.Code = code
            '              Select e


            Dim res = (From d As Concept In Me._context.Concept.Include("ConceptGroup").Include("ConceptGroup.Group").Include("ConceptAccountingStructure") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
            If res IsNot Nothing Then

                If res.ConceptAccountingStructure IsNot Nothing And res.ConceptAccountingStructure.Count > 0 Then
                    For Each ObjConceptAccountingStructure As ConceptAccountingStructure In res.ConceptAccountingStructure
                        Dim AccountingStructure = (From a In _context.AccountingStructure.AsNoTracking Where a.Id = ObjConceptAccountingStructure.AccountingStructureId Select a).FirstOrDefault()

                        If AccountingStructure IsNot Nothing Then
                            ObjConceptAccountingStructure.CodeAccountingStructure = AccountingStructure.Code
                            ObjConceptAccountingStructure.NameAccountingStructure = AccountingStructure.Description
                        End If

                    Next
                End If

            End If
        Else
            Concept = From e In _context.Concept
            Where e.Code = code
                          Select e
        End If
        If Concept.Count > 0 Then
            Dim objConcept As Concept = Nothing
            If tracking = False Then
                objConcept = (From e In _context.Concept.AsNoTracking
                             Where e.Code = code
                             Select e).SingleOrDefault
            Else
                objConcept = Concept.SingleOrDefault
            End If
            Return objConcept
        Else
            Return New Concept
        End If

    End Function

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Public Function ListAllConcept() As List(Of Concept) Implements IConceptRepository.ListAllConcept
        Dim concept = (From e In _context.Concept.Include("ConceptGroup")
                         Select e).ToList()
        concept.ToList().ForEach(Sub(i) i.CodeNameConcatenated = i.Code & " - " & i.Name)
        Return concept
    End Function

    ''' <summary>
    ''' Obtiene una lista de Concepto dependiendo de la lista de class Concept, para el frontal se ScheduleTemplate
    ''' </summary>
    ''' <param name="listClassConcept">Lista de codigos de clase de concepto</param>
    ''' <returns>Lista de Concepto</returns>
    ''' <remarks></remarks>
    Public Function GetConceptByConceptClass(listClassConcept As List(Of String)) As List(Of Concept) Implements IConceptRepository.GetConceptByConceptClass
        Dim concept = From e In _context.Concept.Include("ConceptGroup")
                      Where listClassConcept.Contains(e.ConceptClass)
                         Select e
        Return concept.ToList()
    End Function

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Public Function GetConceptId(Id As Integer) As Concept Implements IConceptRepository.GetConceptId
        Dim concept = (From e In _context.Concept.Include("ConceptGroup") Where e.Id = Id
                       Select e).FirstOrDefault()

        Return concept
    End Function

    Public Function GetAdjustmentConceptId(Id As Integer) As Concept Implements IConceptRepository.GetAdjustmentConceptId
        Dim concept = (From e In _context.Concept.Include("ConceptGroup") Where e.IdAdjustmentConcept = Id
                       Select e).FirstOrDefault()

        Return concept
    End Function

    ''' <summary>
    ''' Lista Todos los Conceptos que se envien como IDS
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Public Function GetConceptIds(Ids As List(Of Integer)) As List(Of Concept) Implements IConceptRepository.GetConceptIds
        Dim listConcept = (From e In _context.Concept Where Ids.Any(Function(x) x = e.Id)
                         Select e).ToList()

        Return listConcept
    End Function

    ''' <summary>
    ''' Obtiene el concepto por clase
    ''' </summary>
    ''' <param name="conceptClass"></param>
    ''' <returns></returns>
    Public Function GetConceptByClass(conceptClass As String) As Concept Implements IConceptRepository.GetConceptByClass
        Return (From c In _context.Concept.AsNoTracking() Where c.ConceptClass = conceptClass Select c).FirstOrDefault()
    End Function

End Class
