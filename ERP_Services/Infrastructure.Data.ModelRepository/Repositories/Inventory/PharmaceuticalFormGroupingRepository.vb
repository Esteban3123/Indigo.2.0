'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Cesar Augusto Collazos
' Created          : 29-02-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class PharmaceuticalFormGroupingRepository
    Inherits GenericRepository(Of PharmaceuticalFormGrouping)
    Implements IPharmaceuticalFormGroupingRepository

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
    ''' Obtiene una forma farmaceutica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalFormGrouping(code As String) As PharmaceuticalFormGrouping Implements IPharmaceuticalFormGroupingRepository.GetPharmaceuticalFormGrouping
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PharmaceuticalFormGrouping In Me._context.PharmaceuticalFormGrouping
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.PharmaceuticalFormGrouping.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New PharmaceuticalFormGrouping()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una forma farmaceutica por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalFormGroupingById(id As Integer) As PharmaceuticalFormGrouping Implements IPharmaceuticalFormGroupingRepository.GetPharmaceuticalFormGroupingById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PharmaceuticalFormGrouping Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As PharmaceuticalFormGrouping In Me._context.PharmaceuticalFormGrouping.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New PharmaceuticalFormGrouping()
        End If
    End Function
End Class
