'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Security.Entities

Public Class ConceptsCausesStatusFolioRepository
    Inherits GenericRepository(Of ConceptsCausesStatusFolio)
    Implements IConceptsCausesStatusFolioRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un almacen por codigo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConceptsCausesStatusFolioById(Id As Integer) As ConceptsCausesStatusFolio Implements IConceptsCausesStatusFolioRepository.GetConceptsCausesStatusFolioById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As ConceptsCausesStatusFolio

        res = (From d As ConceptsCausesStatusFolio In Me._context.ConceptsCausesStatusFolio.AsNoTracking().Include("ConceptsCausesStatusFolioUsers").AsNoTracking()
               Where d.Id.Equals(Id)
               Select d).FirstOrDefault

        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.ConceptsCausesStatusFolio.AsNoTracking.Include("ConceptsCausesStatusFolioUsers").AsNoTracking
                                 Where g.Id.Equals(Id)
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New ConceptsCausesStatusFolio()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code">codigo de la causa de devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConceptsCausesStatusFolioByCode(code As String) As ConceptsCausesStatusFolio Implements IConceptsCausesStatusFolioRepository.GetConceptsCausesStatusFolioByCode
        If code Is Nothing OrElse code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As ConceptsCausesStatusFolio In Me._context.ConceptsCausesStatusFolio.Include("ConceptsCausesStatusFolioUsers") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.ConceptsCausesStatusFolio.AsNoTracking().Include("ConceptsCausesStatusFolioUsers").AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New ConceptsCausesStatusFolio()
        End If
    End Function

End Class
