'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 11-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class ATCEntityRepository

    Inherits GenericRepository(Of ATCEntity)
    Implements IATCEntityRepository

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
    ''' Obtiene una via de admistracion
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetATCEntity(code As String) As ATCEntity Implements IATCEntityRepository.GetATCEntity
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ATCEntity In Me._context.ATCEntity
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim pharma = (From p In _context.PharmacologicalGroup.AsNoTracking Where p.Id = res.IdPharmacologicalGroup Select p).FirstOrDefault
            res.PharmaceuticalGroupDescription = pharma.Code + " - " + pharma.Name

            res.OriginalValue = (From g In _context.ATCEntity.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New ATCEntity()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una via de administracion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetATCEntityById(id As Integer) As ATCEntity Implements IATCEntityRepository.GetATCEntityById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ATCEntity Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As ATCEntity In Me._context.ATCEntity.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res.FirstOrDefault()
        Else
            Return New ATCEntity
        End If
    End Function


End Class
